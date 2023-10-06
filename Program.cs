#region Using
using System.Net;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.ServiceProcess;
using Certes;
using Certes.Acme;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
#endregion

namespace LetsEncryptWithCloudflare
{
    class Program
    {
        #region Class members
        public static string configPath = Path.Combine(Directory.GetCurrentDirectory(), "config.dat");
        public static string logPath = Path.Combine(Directory.GetCurrentDirectory(), "log.dat");
        #endregion
        static async Task Main(string[] args)
        {
            DateTime crtDate = await HmailGetActiveCrtExpDateAsync();

            if(crtDate.Date == DateTime.Today || crtDate.Date < DateTime.Today)
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

                        var acme = new AcmeContext(WellKnownServers.LetsEncryptV2);
                        var account = await acme.NewAccount(cfEmail, true);

                        // // Save the account key for later use
                        // var pemKey = acme.AccountKey.ToPem();

                        // // Load the saved account key
                        // var accountKey = KeyFactory.FromPem(pemKey);
                        // var acme = new AcmeContext(WellKnownServers.LetsEncryptStagingV2, accountKey);
                        // var account = await acme.Account();

                        var order = await acme.NewOrder(new[] { domainCommonName });

                        var authz = (await order.Authorizations()).First();
                        var dnsChallenge = await authz.Dns();
                        var dnsTxt = acme.AccountKey.DnsTxt(dnsChallenge.Token);

                        // Update Cloud Flare TXT record.
                        bool cloudFlareStatus = await UpdateCloudFlareAsync(dnsTxt);

                        if(cloudFlareStatus)
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
                            string sslPath = domain.SSLPath;

                            File.WriteAllBytes(Path.Combine(sslPath, certFileName + ".crt"), System.Text.Encoding.UTF8.GetBytes(certPem));
                            File.WriteAllBytes(Path.Combine(sslPath, certFileName + ".key"), System.Text.Encoding.UTF8.GetBytes(keyFile));

                            X509Certificate2 certificate = new X509Certificate2(Path.Combine(sslPath, certFileName + ".crt"));
                            DateTime expirationDate = certificate.NotAfter;

                            bool hmailStatus = await HmailUpdateAsync(expirationDate.ToString("dd-MM-yyyy"));

                            if(hmailStatus)
                            {
                                await LogWriteAsync("Certificate successfully updated and deployed. " + DateTime.Now);
                                await SendEmailAsync(true);
                            }
                            else
                            {
                                await LogWriteAsync("Certificate couldn't be installed on HmailServer. " + DateTime.Now);
                                await SendEmailAsync(false);
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
                        string activeCrtId = String.Empty;
                        string activeCrtPath = String.Empty;

                        string dbType = domain.DBType;
                        string sslPath = domain.SSLPath;

                        string connStrMSSQL = "Server=" + domain.DBIpAddress + "," + domain.DBPort + ";Database=" + domain.DBName + ";User Id=" + domain.DBUser + ";Password=" + domain.DBPassword + ";";
                        string connStrMySQL = "Server=" + domain.DBIpAddress + "; Database=" + domain.DBName + "; Uid=" + domain.DBUser + "; Pwd=" + domain.DBPassword + "; Port=" + domain.DBPort + ";";

                        string queryMSSQL = "select top 1 portsslcertificateid from hm_tcpipports where portsslcertificateid <> 0;";
                        string queryMySQL = "select portsslcertificateid from hm_tcpipports where portsslcertificateid <> 0 limit 1;";

                        if(dbType == "mysql")
                        {
                            MySqlConnection conn = new MySqlConnection(connStrMySQL);
                            await conn.OpenAsync();

                            MySqlCommand comm = new MySqlCommand(queryMySQL, conn);
                            MySqlDataReader dr = comm.ExecuteReader();

                            if(dr.HasRows)
                            {
                                while(await dr.ReadAsync())
                                {
                                    activeCrtId = dr["portsslcertificateid"].ToString();
                                }
                            }

                            dr.Close();

                            queryMySQL = "select sslcertificatefile from hm_sslcertificates where sslcertificateid = @activeCrtId;";

                            comm = new MySqlCommand(queryMySQL, conn);

                            comm.Parameters.Clear();
                            comm.Parameters.AddWithValue("@activeCrtId", activeCrtId);

                            dr = comm.ExecuteReader();

                            if(dr.HasRows)
                            {
                                while(await dr.ReadAsync())
                                {
                                    activeCrtPath = dr["sslcertificatefile"].ToString();
                                }
                            }

                            dr.Close();

                            await conn.CloseAsync();
                        }
                        else
                        {
                            SqlConnection conn = new SqlConnection(connStrMSSQL);
                            await conn.OpenAsync();

                            SqlCommand comm = new SqlCommand(queryMSSQL, conn);
                            SqlDataReader dr = comm.ExecuteReader();

                            if(dr.HasRows)
                            {
                                while(await dr.ReadAsync())
                                {
                                    activeCrtId = dr["portsslcertificateid"].ToString();
                                }
                            }

                            dr.Close();

                            queryMSSQL = "select sslcertificatefile from hm_sslcertificates where sslcertificateid = @activeCrtId;";

                            comm = new SqlCommand(queryMSSQL, conn);

                            comm.Parameters.Clear();
                            comm.Parameters.AddWithValue("@activeCrtId", activeCrtId);

                            dr = comm.ExecuteReader();

                            if(dr.HasRows)
                            {
                                while(await dr.ReadAsync())
                                {
                                    activeCrtPath = dr["sslcertificatefile"].ToString();
                                }
                            }

                            dr.Close();

                            await conn.CloseAsync();
                        }

                        if(!String.IsNullOrEmpty(activeCrtPath))
                        {
                            X509Certificate2 certificate = new X509Certificate2(activeCrtPath);
                            expDate = certificate.NotAfter;
                        }
                        else
                        {
                            await LogWriteAsync("activeCrtPath is empty (HmailGetActiveCrtExpDateAsync). " + DateTime.Now);
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
                        string dbType = domain.DBType;
                        string[] skipPort = domain.SkipPorts.ToString().Split(';');

                        string connStrMSSQL = "Server=" + domain.DBIpAddress + "," + domain.DBPort + ";Database=" + domain.DBName + ";User Id=" + domain.DBUser + ";Password=" + domain.DBPassword + ";";
                        string connStrMySQL = "Server=" + domain.DBIpAddress + "; Database=" + domain.DBName + "; Uid=" + domain.DBUser + "; Pwd=" + domain.DBPassword + "; Port=" + domain.DBPort + ";";

                        Int64 lastID = 0;
                        string sslPath = domain.SSLPath;

                        string queryMSSQL = "insert into hm_sslcertificates (sslcertificatename, sslcertificatefile, sslprivatekeyfile) output INSERTED.sslcertificateid values (@Name, @CertificateFile, @PrivateKeyFile);";
                        string queryMySQL = "insert into hm_sslcertificates (sslcertificatename, sslcertificatefile, sslprivatekeyfile) values (@Name, @CertificateFile, @PrivateKeyFile); select LAST_INSERT_ID();";

                        SqlConnection connMSSQL = new SqlConnection(connStrMSSQL);
                        SqlCommand commMSSQL;

                        MySqlConnection connMySQL = new MySqlConnection(connStrMySQL);
                        MySqlCommand commMySQL;

                        if(dbType.Trim().ToLower() == "mysql")
                        {
                            await connMySQL.OpenAsync();

                            commMySQL = new MySqlCommand(queryMySQL, connMySQL);

                            commMySQL.Parameters.Clear();
                            commMySQL.Parameters.AddWithValue("@Name", certName);
                            commMySQL.Parameters.AddWithValue("@CertificateFile", Path.Combine(sslPath, certName + ".crt"));
                            commMySQL.Parameters.AddWithValue("@PrivateKeyFile", Path.Combine(sslPath, certName + ".key"));

                            var result = await commMySQL.ExecuteScalarAsync();

                            if (result != null)
                            {
                                lastID = Convert.ToInt64(result);
                            }
                            else
                            {
                                await LogWriteAsync("Database exception (HmailUpdateAsync): could not insert into hm_sslcertificates" + " " + DateTime.Now);
                                isAllOk = false;
                            }
                        }
                        else
                        {
                            await connMSSQL.OpenAsync();

                            commMSSQL = new SqlCommand(queryMSSQL, connMSSQL);

                            commMSSQL.Parameters.Clear();
                            commMSSQL.Parameters.AddWithValue("@Name", certName);
                            commMSSQL.Parameters.AddWithValue("@CertificateFile", Path.Combine(sslPath, certName + ".crt"));
                            commMSSQL.Parameters.AddWithValue("@PrivateKeyFile", Path.Combine(sslPath, certName + ".key"));

                            var result = await commMSSQL.ExecuteScalarAsync();

                            if (result != null)
                            {
                                lastID = Convert.ToInt64(result);
                            }
                            else
                            {
                                await LogWriteAsync("Database exception (HmailUpdateAsync): could not insert into hm_sslcertificates" + " " + DateTime.Now);
                                isAllOk = false;
                            }
                        }

                        #region Update hm_tcpipports
                        if(lastID > 0 && isAllOk)
                        {
                            string query = "update hm_tcpipports set portsslcertificateid = @certId";

                            if(skipPort.Count() > 0)
                            {
                                query += " where";
                                int i = 0;

                                foreach(string port in skipPort)
                                {
                                    if (i == 0)
                                    {
                                        query += " portnumber <> " + port;
                                    }
                                    else
                                    {
                                        query += " and portnumber <> " + port;
                                    }

                                    i++;
                                }
                            }

                            query += ";";

                            if(dbType.Trim().ToLower() == "mysql")
                            {
                                commMySQL = new MySqlCommand(query, connMySQL);

                                commMySQL.Parameters.Clear();
                                commMySQL.Parameters.AddWithValue("@certId", lastID);

                                await commMySQL.ExecuteNonQueryAsync();
                                await connMySQL.CloseAsync();
                            }
                            else
                            {
                                commMSSQL = new SqlCommand(query, connMSSQL);

                                commMSSQL.Parameters.Clear();
                                commMSSQL.Parameters.AddWithValue("@certId", lastID);

                                await commMSSQL.ExecuteNonQueryAsync();
                                await connMSSQL.CloseAsync();
                            }
                        }
                        else
                        {
                            await LogWriteAsync("Database exception (HmailUpdateAsync): could not update hm_tcpipports" + " " + DateTime.Now);
                            isAllOk = false;
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

        #region Methods CloudFlare
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
                        int smtpPort = int.Parse(port);
                        string smtpUser = domain.SMTPUser;
                        string smtpPassword = domain.SMTPPassword;
                        string SMTPTo = domain.SMTPTo;

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
        #endregion
    }
}
