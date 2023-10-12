#region Using
using System.Net;
using Newtonsoft.Json.Linq;
using System.ServiceProcess;
using Certes;
using Certes.Acme;
using System.Security.Cryptography.X509Certificates;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using hMailServer;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
#endregion

namespace LetsEncrypt
{
    class Program
    {
        #region Class members
        public static string configPath = Path.Combine(Directory.GetCurrentDirectory(), "config.dat");
        public static string logPath = Path.Combine(Directory.GetCurrentDirectory(), "log.dat");
        public static string key = "rdAwVcVQj5N6y1Fo";
        public static string iv = "tDiiTOISAEabnoGR";
        #endregion
        static async Task Main(string[] args)
        {
            DateTime crtDate = await HmailGetActiveCrtExpDateAsync();

            if (crtDate.Date == DateTime.Today || crtDate.Date < DateTime.Today)
            {
                await DoCertAsync();
            }

            await LogWriteAsync("Security certificate check run on: " + DateTime.Now);
        }

        // For each domain name in config.dat request TXT record value from Let's Encrypt, validate, issue and install certificate.
        #region Methods Let's Encrypt
        protected static async Task DoCertAsync()
        {
            if (File.Exists(configPath))
            {
                string configuration = await File.ReadAllTextAsync(configPath);

                dynamic data = JObject.Parse(configuration);
                dynamic domains = data.Domains;

                foreach (dynamic domain in domains)
                {
                    try
                    {
                        string cfEmail = domain.Email;
                        string domainCommonName = domain.CommonName;
                        string CountryName = domain.CountryName;
                        string State = domain.State;
                        string Locality = domain.Locality;
                        string Organization = domain.Organization;
                        string OU = domain.OU;
                        string letsEncryptPemAccount = domain.LetsEncryptPemAccount.ToString().Trim();
                        string sslPath = domain.SSLPath;
                        string dnsProvider = domain.DNSProvider;

                        bool isEncryptedConfig = bool.Parse(domain.EncryptConfig.ToString());

                        if(isEncryptedConfig)
                        {
                            cfEmail = AesDecrypt(cfEmail);
                            domainCommonName = AesDecrypt(domainCommonName);
                            CountryName = AesDecrypt(CountryName);
                            State = AesDecrypt(State);
                            Locality = AesDecrypt(Locality);
                            Organization = AesDecrypt(Organization);
                            OU = AesDecrypt(OU);
                            sslPath = AesDecrypt(sslPath);

                            if (!String.IsNullOrEmpty(letsEncryptPemAccount))
                            {
                                try
                                {
                                    letsEncryptPemAccount = AesDecrypt(letsEncryptPemAccount);
                                }
                                catch { }
                            }
                        }

                        AcmeContext acme;

                        if (!String.IsNullOrEmpty(letsEncryptPemAccount))
                        {
                            // Load previously saved account key (no new account creation is necessary)
                            var accountKey = KeyFactory.FromPem(letsEncryptPemAccount);
                            acme = new AcmeContext(WellKnownServers.LetsEncryptV2, accountKey);
                            var account = await acme.Account();
                        }
                        else
                        {
                            acme = new AcmeContext(WellKnownServers.LetsEncryptV2);
                            var account = await acme.NewAccount(cfEmail, true);

                            // Save the account key for later use (after new account has been created)
                            if (isEncryptedConfig)
                            {
                                data["Domains"][0]["LetsEncryptPemAccount"] = AesEncrypt(acme.AccountKey.ToPem());
                            }
                            else
                            {
                                data["Domains"][0]["LetsEncryptPemAccount"] = acme.AccountKey.ToPem();
                            }

                            File.WriteAllText(configPath, data.ToString(Formatting.Indented));
                        }

                        var order = await acme.NewOrder(new[] { domainCommonName });

                        var authz = (await order.Authorizations()).First();
                        var dnsChallenge = await authz.Dns();
                        var dnsTxt = acme.AccountKey.DnsTxt(dnsChallenge.Token);

                        // Update DNS TXT record.
                        bool dnsStatus = false;

                        if(dnsProvider == "CloudFlare")
                        {
                            dnsStatus = await UpdateCloudFlareAsync(dnsTxt);
                        }
                        else
                        {
                            dnsStatus = await UpdateGoDaddyAsync(dnsTxt);
                        }

                        if(dnsStatus)
                        {
                            Thread.Sleep(60000); // Wait for 1 min to propagate the DNS record change. Set time as it fits.
                            await dnsChallenge.Validate();

                            var privateKey = KeyFactory.NewKey(KeyAlgorithm.RS256);

                            var cert = await order.Generate(new CsrInfo
                            {
                                CountryName = CountryName,
                                State = State,
                                Locality = Locality,
                                Organization = Organization,
                                OrganizationUnit = OU,
                                CommonName = domainCommonName,
                            }, privateKey);

                            string certPem = cert.ToPem();
                            string keyFile = privateKey.ToPem();

                            // Export PFX if needed.
                            // var pfxBuilder = cert.ToPfx(privateKey);
                            // var pfx = pfxBuilder.Build("my-cert", "abcd1234");

                            // Save files and update hmailserver.
                            string certFileName = DateTime.Now.ToString("dd-MM-yyyy");
                            

                            File.WriteAllBytes(Path.Combine(sslPath, certFileName + ".crt"), Encoding.UTF8.GetBytes(certPem));
                            File.WriteAllBytes(Path.Combine(sslPath, certFileName + ".key"), Encoding.UTF8.GetBytes(keyFile));

                            X509Certificate2 certificate = new X509Certificate2(Path.Combine(sslPath, certFileName + ".crt"));
                            DateTime expirationDate = certificate.NotAfter;

                            bool hmailStatus = await HmailUpdateAsync(expirationDate.ToString("dd-MM-yyyy"));
                            bool sendNotification = bool.Parse(domain.SMTPSendNotification.ToString().Trim().ToLower());

                            if(hmailStatus)
                            {
                                await LogWriteAsync("Certificate successfully updated and deployed. " + DateTime.Now);
                            }
                            else
                            {
                                await LogWriteAsync("Certificate couldn't be installed on HmailServer. " + DateTime.Now);
                            }

                            if (sendNotification)
                            {
                                await SendEmailAsync(hmailStatus);
                            }
                        }
                        else
                        {
                            await LogWriteAsync("CloudFlare exception couldn't update TXT record (DoCertAsync). " + DateTime.Now);
                        }
                    }
                    catch (Exception ex)
                    {
                        await LogWriteAsync("General exception (DoCertAsync): " + ex.Message + " " + DateTime.Now);
                    }
                }
            }
            else
            {
                await LogWriteAsync("Configuration file not found (DoCertAsync): " + configPath + " " + DateTime.Now);
            }
        }
        #endregion

        #region Methods HmailServer
        protected static async Task<DateTime> HmailGetActiveCrtExpDateAsync()
        {
            DateTime expDate = DateTime.Now.AddYears(-1);

            if (File.Exists(configPath))
            {
                string configuration = await File.ReadAllTextAsync(configPath);

                dynamic data = JObject.Parse(configuration);
                dynamic domains = data.Domains;

                foreach (dynamic domain in domains)
                {
                    try
                    {
                        string hMailUser = domain.hMailUser;
                        string hMailPassword = domain.hMailPassword;

                        bool isEncryptedConfig = bool.Parse(domain.EncryptConfig.ToString());

                        if (isEncryptedConfig)
                        {
                            hMailUser = AesDecrypt(hMailUser);
                            hMailPassword = AesDecrypt(hMailPassword);
                        }

                        Application hMailApp = new Application();
                        hMailApp.Authenticate(hMailUser, hMailPassword);

                        List<int> activeCertsIds = new List<int>();
                        TCPIPPorts ports = hMailApp.Settings.TCPIPPorts;

                        for(int i = 0; i < ports.Count; i++)
                        {
                            TCPIPPort port = ports[i];
                            eConnectionSecurity security = port.ConnectionSecurity;

                            if (security == eConnectionSecurity.eCSSTARTTLSOptional || security == eConnectionSecurity.eCSSTARTTLSRequired || security == eConnectionSecurity.eCSTLS)
                            {
                                activeCertsIds.Clear(); // Hold only the last found row cert ID, as only single cert would be applied. No multiple certs are supported currently.
                                activeCertsIds.Add(port.SSLCertificateID);
                            }
                        }

                        if (activeCertsIds.Count > 0)
                        {
                            SSLCertificates certificates = hMailApp.Settings.SSLCertificates;
                            string activeCrtPath = certificates.ItemByDBID[activeCertsIds[0]].CertificateFile;

                            if (!String.IsNullOrEmpty(activeCrtPath))
                            {
                                X509Certificate2 certificate = new X509Certificate2(activeCrtPath);
                                expDate = certificate.NotAfter;
                            }
                            else
                            {
                                await LogWriteAsync("activeCrtPath is empty (HmailGetActiveCrtExpDateAsync). " + DateTime.Now);
                            }
                        }
                        else
                        {
                            await LogWriteAsync("No SSL enabled ports found on the server (HmailGetActiveCrtExpDateAsync). " + DateTime.Now);
                        }
                    }
                    catch (Exception ex)
                    {
                        await LogWriteAsync("Exception executing (HmailGetActiveCrtExpDateAsync): " + ex.Message + " " + DateTime.Now);
                    }
                }
            }
            else
            {
                await LogWriteAsync("Configuration file not found (HmailGetActiveCrtExpDateAsync): " + configPath + " " + DateTime.Now);
            }

            return expDate;
        }
        protected static async Task<bool> HmailUpdateAsync(string certName)
        {
            bool isAllOk = true;

            if (File.Exists(configPath))
            {
                string configuration = await File.ReadAllTextAsync(configPath);

                dynamic data = JObject.Parse(configuration);
                dynamic domains = data.Domains;

                foreach (dynamic domain in domains)
                {
                    try
                    {
                        string sslPath = domain.SSLPath;

                        string hMailUser = domain.hMailUser;
                        string hMailPassword = domain.hMailPassword;

                        bool isEncryptedConfig = bool.Parse(domain.EncryptConfig.ToString());

                        if (isEncryptedConfig)
                        {
                            hMailUser = AesDecrypt(hMailUser);
                            hMailPassword = AesDecrypt(hMailPassword);
                            sslPath = AesDecrypt(sslPath);
                        }

                        Application hMailApp = new Application();
                        hMailApp.Authenticate(hMailUser, hMailPassword);

                        string certFileName = DateTime.Now.ToString("dd-MM-yyyy");
                        SSLCertificate certificate = hMailApp.Settings.SSLCertificates.Add();

                        certificate.Name = certName;
                        certificate.CertificateFile = Path.Combine(sslPath, certFileName + ".crt");
                        certificate.PrivateKeyFile = Path.Combine(sslPath, certFileName + ".key");
                        certificate.Save();

                        int lastID = certificate.ID;                       

                        #region Update hm_tcpipports
                        TCPIPPorts ports = hMailApp.Settings.TCPIPPorts;

                        for (int i = 0; i < ports.Count; i++)
                        {
                            TCPIPPort portInfo = ports[i];
                            eConnectionSecurity connectionSecurity = portInfo.ConnectionSecurity;

                            if (connectionSecurity == eConnectionSecurity.eCSSTARTTLSOptional || connectionSecurity == eConnectionSecurity.eCSSTARTTLSRequired || connectionSecurity == eConnectionSecurity.eCSTLS)
                            {
                                TCPIPPort port = hMailApp.Settings.TCPIPPorts.ItemByDBID[portInfo.ID];
                                port.SSLCertificateID = lastID;
                                port.Save();
                            }
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        await LogWriteAsync("Database exception (HmailUpdateAsync): " + ex.Message + " " + DateTime.Now);
                        isAllOk = false;
                    }

                    #region Restart the HmailServer windows service
                    if(isAllOk)
                    {
                        string hmailWindowsServiceName = "hMailServer";

                        try
                        {
                            using (ServiceController serviceController = new ServiceController(hmailWindowsServiceName))
                            {
                                if (serviceController.Status == ServiceControllerStatus.Running)
                                {
                                    await LogWriteAsync("Stopping hmailserver service. " + DateTime.Now);
                                    serviceController.Stop();
                                    serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));

                                    await LogWriteAsync("Staring hmailserver service. " + DateTime.Now);
                                    serviceController.Start();
                                    serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                                }
                                else
                                {
                                    await LogWriteAsync("Staring hmailserver service. " + DateTime.Now);
                                    serviceController.Start();
                                    serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            await LogWriteAsync("Windows service exception (HmailUpdateAsync): " + ex.Message + " " + DateTime.Now);
                            isAllOk = false;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                await LogWriteAsync("Configuration file not found (HmailUpdateAsync): " + configPath + " " + DateTime.Now);
                isAllOk = false;
            }

            return isAllOk;
        }
        #endregion

        #region Methods DNS
        protected static async Task<bool> UpdateGoDaddyAsync(string txtRecord)
        {
            bool reply = true;
            if (File.Exists(configPath))
            {
                string configuration = await File.ReadAllTextAsync(configPath);

                dynamic data = JObject.Parse(configuration);
                dynamic domains = data.Domains;

                foreach (dynamic domain in domains)
                {
                    try
                    {
                        string apiKey = Convert.ToString(domain.APIKey);
                        string apiSecret = Convert.ToString(domain.APISecret);

                        string domainName = Convert.ToString(domain.Domain_Name);
                        string dnsRecord = Convert.ToString(domain.DNS_Record);
                        string type = Convert.ToString(domain.Type);
                        string ttl = Convert.ToString(domain.TTL);

                        bool isEncryptedConfig = bool.Parse(domain.EncryptConfig.ToString());

                        if (isEncryptedConfig)
                        {
                            apiKey = AesDecrypt(apiKey);
                            apiSecret = AesDecrypt(apiSecret);
                            domainName = AesDecrypt(domainName);
                            dnsRecord = AesDecrypt(dnsRecord);
                        }

                        Dictionary<string, object> pushRecord = new Dictionary<string, object>()
                        {
                            { "data", txtRecord },
                            { "name", dnsRecord },
                            { "ttl", int.Parse(ttl) },
                            { "type", type },
                            { "port", 1 },
                            { "priority", 0 },
                            { "protocol", "string" },
                            { "service", "string" },
                            { "weight", 0 },
                        };

                        string jsonData = JsonConvert.SerializeObject(new object[1] { pushRecord });

                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://api.godaddy.com/v1/domains/" + domainName + "/records/TXT/" + dnsRecord);

                        req.Headers.Add("Authorization", "sso-key " + apiKey + ":" + apiSecret);
                        req.ContentType = "application/json";
                        req.Accept = "application/json";
                        req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";
                        req.Method = "PUT";
                        req.ContentLength = jsonData.Length;

                        using (var writer = new StreamWriter(req.GetRequestStream()))
                        {
                            writer.Write(jsonData);
                        }

                        Dictionary<string, string> headers = new Dictionary<string, string>();

                        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                        Stream strm = resp.GetResponseStream();
                        StreamReader Reader = new StreamReader(strm, Encoding.Default);

                        string content = Reader.ReadToEnd();
                        headers.Clear();

                        foreach (string headkey in resp.Headers.AllKeys)
                        {
                            headers.Add(headkey, resp.Headers[headkey]);
                        }

                        resp.Close();
                        strm.Close();

                        headers.Clear();

                        await LogWriteAsync("Updated TXT record " + dnsRecord + "." + domainName + " to " + txtRecord + " on " + DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        await LogWriteAsync("General exception (UpdateGoDaddyAsync): " + ex.Message + " " + DateTime.Now);
                        reply = false;
                    }
                }
            }
            else
            {
                await LogWriteAsync("Configuration file not found (UpdateGoDaddyAsync): " + configPath + " " + DateTime.Now);
                reply = false;
            }

            return reply;
        }
        protected static async Task<bool> UpdateCloudFlareAsync(string txtRecord)
        {
            bool reply = true;

            if (File.Exists(configPath))
            {
                string configuration = await File.ReadAllTextAsync(configPath);

                dynamic data = JObject.Parse(configuration);
                dynamic domains = data.Domains;

                foreach (dynamic domain in domains)
                {
                    try
                    {
                        string apiKey = Convert.ToString(domain.APIKey);
                        string email = Convert.ToString(domain.Email);

                        string domainName = Convert.ToString(domain.Domain_Name);
                        string dnsRecord = Convert.ToString(domain.DNS_Record);
                        string type = Convert.ToString(domain.Type);
                        string ttl = Convert.ToString(domain.TTL);

                        bool isEncryptedConfig = bool.Parse(domain.EncryptConfig.ToString());

                        if (isEncryptedConfig)
                        {
                            apiKey = AesDecrypt(apiKey);
                            email = AesDecrypt(email);
                            domainName = AesDecrypt(domainName);
                            dnsRecord = AesDecrypt(dnsRecord);
                        }

                        ServicePointManager.DefaultConnectionLimit = 10;
                        ServicePointManager.Expect100Continue = false;

                        #region Get zone-id for the domain name, which is used in the update statement below.
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://api.cloudflare.com/client/v4/zones?&name=" + domainName);
                        req.Proxy = null;
                        req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";
                        req.Timeout = 5000; // time spent trying to establish a connection (not including lookup time) before give up.
                        req.Accept = "*/*";
                        req.Method = "GET";
                        req.Headers.Add("X-Auth-Email:" + email);
                        req.Headers.Add("X-Auth-Key:" + apiKey);
                        req.ContentType = "application/json";

                        HttpWebResponse resp = (HttpWebResponse) await req.GetResponseAsync();
                        Stream dataStream = resp.GetResponseStream();
                        StreamReader reader = new StreamReader(dataStream);

                        string zones = String.Empty;
                        dynamic zones_data = JObject.Parse(await reader.ReadToEndAsync());
                        dynamic results = zones_data.result;

                        reader.Close();
                        resp.Close();

                        foreach (dynamic result in results)
                        {
                            zones = Convert.ToString(result.id);
                        }
                        #endregion

                        #region Get dns_record id for the DNS record, which is used in the update statement below.
                        req = (HttpWebRequest)WebRequest.Create("https://api.cloudflare.com/client/v4/zones/" + zones + "/dns_records?type=" + type + "&name=" + dnsRecord + "." + domainName);
                        req.Proxy = null;
                        req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";
                        req.Timeout = 5000; // time spent trying to establish a connection (not including lookup time) before give up.
                        req.Accept = "*/*";
                        req.Method = "GET";
                        req.Headers.Add("X-Auth-Email:" + email);
                        req.Headers.Add("X-Auth-Key:" + apiKey);
                        req.ContentType = "application/json";

                        resp = (HttpWebResponse) await req.GetResponseAsync();
                        dataStream = resp.GetResponseStream();
                        reader = new StreamReader(dataStream);

                        string dns_records = String.Empty;
                        dynamic zone_data = JObject.Parse(await reader.ReadToEndAsync());
                        dynamic zone_results = zone_data.result;

                        reader.Close();
                        resp.Close();

                        foreach (dynamic result in zone_results)
                        {
                            dns_records = Convert.ToString(result.id);
                        }
                        #endregion

                        #region Send zone update.
                        req = (HttpWebRequest)WebRequest.Create("https://api.cloudflare.com/client/v4/zones/" + zones + "/dns_records/" + dns_records);
                        req.Proxy = null;
                        req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";
                        req.Timeout = 5000; // time spent trying to establish a connection (not including lookup time) before give up.
                        req.Accept = "*/*";
                        req.Method = "PUT";
                        req.Headers.Clear();
                        req.Headers.Add("X-Auth-Email:" + email);
                        req.Headers.Add("X-Auth-Key:" + apiKey);
                        req.ContentType = "application/json";

                        string jsonData = "{\"type\":\"" + type + "\",\"name\":\"" + dnsRecord + "." + domainName + "\",\"content\":\"" + txtRecord + "\",\"ttl\":" + ttl + "}";

                        StreamWriter sw = new StreamWriter(await req.GetRequestStreamAsync());
                        sw.Write(jsonData);
                        sw.Flush();
                        sw.Close();

                        resp = (HttpWebResponse)await req.GetResponseAsync();
                        dataStream = resp.GetResponseStream();
                        reader = new StreamReader(dataStream);

                        await reader.ReadToEndAsync();

                        reader.Close();
                        resp.Close();
                        #endregion

                        await LogWriteAsync("Updated TXT record " + dnsRecord + "." + domainName  + " to " + txtRecord + " on " + DateTime.Now);

                    }
                    catch (Exception ex)
                    {
                        await LogWriteAsync("General exception (UpdateCloudFlareAsync): " + ex.Message + " " + DateTime.Now);
                        reply = false;
                    }
                }
            }
            else
            {
                await LogWriteAsync("Configuration file not found (UpdateCloudFlareAsync): " + configPath + " " + DateTime.Now);
                reply = false;
            }

            return reply;
        }
        #endregion

        #region Methods
        protected static async Task LogWriteAsync(string message)
        {
            if (File.Exists(logPath))
            {
                try
                {
                    FileInfo fi = new FileInfo(logPath);

                    if (fi.Length >= 1024 * 1024 * 10) // 10 MB max file size.
                    {
                        await File.WriteAllTextAsync(logPath, message + Environment.NewLine);
                    }
                    else
                    {
                        await File.AppendAllTextAsync(logPath, message + Environment.NewLine);
                    }
                }
                catch { }
            }
            else
            {
                try
                {
                    await File.WriteAllTextAsync(logPath, message + Environment.NewLine);
                }
                catch { }
            }
        }
        #endregion

        #region Send email
        protected static async Task SendEmailAsync(bool success)
        {
            if (File.Exists(configPath))
            {
                string configuration = await File.ReadAllTextAsync(configPath);

                dynamic data = JObject.Parse(configuration);
                dynamic domains = data.Domains;

                foreach (dynamic domain in domains)
                {
                    try
                    {
                        string commonName = domain.CommonName;
                        string smtpServer = domain.SMTPServer;
                        string port = domain.SMTPPort;
                        string smtpUser = domain.SMTPUser;
                        string smtpPassword = domain.SMTPPassword;
                        string SMTPTo = domain.SMTPTo;

                        bool isEncryptedConfig = bool.Parse(domain.EncryptConfig.ToString());

                        if (isEncryptedConfig)
                        {
                            commonName = AesDecrypt(commonName);
                            smtpServer = AesDecrypt(smtpServer);
                            port = AesDecrypt(port);
                            smtpUser = AesDecrypt(smtpUser);
                            smtpPassword = AesDecrypt(smtpPassword);
                            SMTPTo = AesDecrypt(SMTPTo);
                        }

                        int smtpPort = int.Parse(port);

                        MimeMessage msg = new MimeMessage();

                        msg.To.Add(MailboxAddress.Parse(smtpUser));
                        msg.From.Add(new MailboxAddress("SSL Notification", smtpUser));

                        string message = "SSL update for domain: " + commonName + " has FAILED.";
                        string subject = "FAILED " + commonName + " SSL notification";
                        msg.Priority = MessagePriority.Urgent;

                        if(success)
                        {
                            message = "SSL updated for domain: " + commonName + " has SUCCEEDED.";
                            subject = commonName + " SSL notification";
                            msg.Priority = MessagePriority.Normal;
                        }

                        string messageHTML = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">" +
                                                "<html><head><meta http-equiv=Content-Type content=\"text/html; charset=utf-8\"></head><body>" +
                                                message + "<br /><br />" +
                                                "</span><br /><br /><br /><i><small>This is an automated message. Please do not reply to this email.</small></i><br /><br />" +
                                                "<i><small>This message and attachments may contain confidential information. If it appears that this message was sent to you by mistake, any retention, dissemination, distribution or copying of this message and attachments is strictly prohibited. Please immediately and permanently delete the message and any attachments.</small></i>" +
                                                "<br /><br /><small>Copyright © " + DateTime.UtcNow.Year.ToString() + " " + "All rights reserved." +
                                                "</body></html>";

                        string messageText = message + Environment.NewLine + Environment.NewLine +
                                            "This is an automated message. Please do not reply to this email." + Environment.NewLine +
                                            "This message and attachments may contain confidential information. If it appears that this message was sent to you by mistake, any retention, dissemination, distribution or copying of this message and attachments is strictly prohibited. Please immediately and permanently delete the message and any attachments." + Environment.NewLine + Environment.NewLine +
                                            "Copyright © " + DateTime.UtcNow.Year.ToString() + " All rights reserved.";

                        msg.Subject = subject;

                        BodyBuilder bodyBuilder = new BodyBuilder();

                        bodyBuilder.HtmlBody = messageHTML;
                        bodyBuilder.TextBody = messageText;
                        msg.Body = bodyBuilder.ToMessageBody();

                        SmtpClient smtp = new SmtpClient();

                        smtp.Timeout = 30000; // 30 seconds
                        smtp.Connect(smtpServer, smtpPort, SecureSocketOptions.Auto);
                        smtp.Authenticate(smtpUser, smtpPassword);

                        await smtp.SendAsync(msg);
                        smtp.Dispose();
                    }
                    catch (Exception ex)
                    { 
                        await LogWriteAsync("Could not send email notification (SendEmailAsync): " + ex.Message + " " + DateTime.Now);
                    }
                }
            }
        }
        protected static string AesEncrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
        protected static string AesDecrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        #endregion
    }
}
