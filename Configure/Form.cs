#region Using
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using MimeKit;
using MailKit.Security;
using System.Net;
#endregion

namespace Configure
{
    public partial class Config : Form
    {
        #region Class members
        public static string configPath = Path.Combine(Directory.GetCurrentDirectory(), "config.dat");
        public static string logPath = Path.Combine(Directory.GetCurrentDirectory(), "log.dat");
        public static string key = "rdAwVcVQj5N6y1Fo";
        public static string iv = "tDiiTOISAEabnoGR";
        public static string countriesTwoLetterISO = "AD,AE,AF,AG,AI,AL,AM,AO,AQ,AR,AS,AT,AU,AW,AX,AZ,BA,BB,BD,BE,BF,BG,BH,BI,BJ,BL,BM,BN,BO,BQ,BR,BS,BT,BV,BW,BY,BZ,CA,CC,CD,CF,CG,CH,CI,CK,CL,CM,CN,CO,CR,CU,CV,CW,CX,CY,CZ,DE,DJ,DK,DM,DO,DZ,EC,EE,EG,EH,ER,ES,ET,FI,FJ,FK,FM,FO,FR,GA,GB,GD,GE,GF,GG,GH,GI,GL,GM,GN,GP,GQ,GR,GS,GT,GU,GW,GY,HK,HM,HN,HR,HT,HU,ID,IE,IL,IM,IN,IO,IQ,IR,IS,IT,JE,JM,JO,JP,KE,KG,KH,KI,KM,KN,KP,KR,KW,KY,KZ,LA,LB,LC,LI,LK,LR,LS,LT,LU,LV,LY,MA,MC,MD,ME,MF,MG,MH,MK,ML,MM,MN,MO,MP,MQ,MR,MS,MT,MU,MV,MW,MX,MY,MZ,NA,NC,NE,NF,NG,NI,NL,NO,NP,NR,NU,NZ,OM,PA,PE,PF,PG,PH,PK,PL,PM,PN,PR,PS,PT,PW,PY,QA,RE,RO,RS,RU,RW,SA,SB,SC,SD,SE,SG,SH,SI,SJ,SK,SL,SM,SN,SO,SR,SS,ST,SV,SX,SY,SZ,TC,TD,TF,TG,TH,TJ,TK,TL,TM,TN,TO,TR,TT,TV,TW,TZ,UA,UG,UM,US,UY,UZ,VA,VC,VE,VG,VI,VN,VU,WF,WS,YE,YT,ZA,ZM,ZW";
        #endregion

        #region Class init
        public Config()
        {
            InitializeComponent();
        }

        private async void Config_Load(object sender, EventArgs e)
        {
            cmbDNSTTL.Items.Add("60"); // 1 min
            cmbDNSTTL.Items.Add("120"); // 2 min
            cmbDNSTTL.Items.Add("300"); // 5 min
            cmbDNSTTL.Items.Add("600"); // 10 min
            cmbDNSTTL.Items.Add("900"); // 15 min
            cmbDNSTTL.Items.Add("1800"); // 30 min
            cmbDNSTTL.Items.Add("3600"); // 1 hour
            cmbDNSTTL.Items.Add("7200"); // 2 hours
            cmbDNSTTL.Items.Add("18000"); // 5 hours
            cmbDNSTTL.Items.Add("43200"); // 12 hours
            cmbDNSTTL.Items.Add("86400"); // 1 day

            cmbDNSTTL.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (string country in countriesTwoLetterISO.Split(','))
            {
                cmbSSLCountry.Items.Add(country);
            }

            cmbSSLCountry.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbDNSProvider.Items.Add("CloudFlare");
            cmbDNSProvider.Items.Add("GoDaddy");

            cmbDNSProvider.DropDownStyle = ComboBoxStyle.DropDownList;

            if (File.Exists(configPath))
            {
                string configuration = await File.ReadAllTextAsync(configPath);

                dynamic data;

                try
                {
                    data = JObject.Parse(configuration);
                }
                catch
                {
                    MessageBox.Show("Invalid json format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dynamic domains = data.Domains;

                foreach (dynamic domain in domains)
                {
                    try
                    {
                        // Encrypt configuration file
                        bool encryptConfig = bool.Parse(domain.EncryptConfig.ToString());

                        if (encryptConfig)
                        {
                            // DNS
                            cmbDNSProvider.SelectedItem = domain.DNSProvider.ToString().Trim();
                            tbDNSApiSecret.Text = AesDecrypt(domain.APISecret.ToString());
                            tbDNSApiKey.Text = AesDecrypt(domain.APIKey.ToString());
                            tbDNSEmail.Text = AesDecrypt(domain.Email.ToString());
                            tbDNSDomain.Text = AesDecrypt(domain.Domain_Name.ToString());
                            tbDNSRecord.Text = AesDecrypt(domain.DNS_Record.ToString());
                            tbDNSRecordType.Text = domain.Type;
                            cmbDNSTTL.SelectedItem = domain.TTL.ToString().Trim().ToLower();

                            // SSL
                            tbSSLPath.Text = AesDecrypt(domain.SSLPath.ToString());
                            cmbSSLCountry.SelectedItem = AesDecrypt(domain.CountryName.ToString());
                            tbSSLState.Text = AesDecrypt(domain.State.ToString());
                            tbSSLLocality.Text = AesDecrypt(domain.Locality.ToString());
                            tbSSLOrganization.Text = AesDecrypt(domain.Organization.ToString());
                            tbSSLOU.Text = AesDecrypt(domain.OU.ToString());
                            tbSSLCommonName.Text = AesDecrypt(domain.CommonName.ToString());

                            // Notify
                            bool notify = bool.Parse(domain.SMTPSendNotification.ToString());

                            if (notify)
                            {
                                chkNotify.Checked = true;

                                lblSMTPServer.Text = "SMTP server: *";
                                lblSMTPPort.Text = "SMTP port: *";
                                lblSMTPUser.Text = "SMTP user: *";
                                lblSMTPPassword.Text = "SMTP password: *";
                                lblToEmail.Text = "To email: *";
                            }

                            tbSMTPServer.Text = AesDecrypt(domain.SMTPServer.ToString());
                            tbSMTPPort.Text = AesDecrypt(domain.SMTPPort.ToString());
                            tbSMTPUser.Text = AesDecrypt(domain.SMTPUser.ToString());
                            tbSMTPPassword.Text = AesDecrypt(domain.SMTPPassword.ToString());
                            tbToEmail.Text = AesDecrypt(domain.SMTPTo.ToString());

                            // Hmail
                            tbHmailPassword.Text = AesDecrypt(domain.hMailPassword.ToString());
                            tbHmailUser.Text = AesDecrypt(domain.hMailUser.ToString());

                            chkEncryptConfig.Checked = true;
                        }
                        else
                        {
                            // DNS
                            cmbDNSProvider.SelectedItem = domain.DNSProvider.ToString().Trim();
                            tbDNSApiSecret.Text = domain.APISecret.ToString();
                            tbDNSApiKey.Text = domain.APIKey;
                            tbDNSEmail.Text = domain.Email;
                            tbDNSDomain.Text = domain.Domain_Name;
                            tbDNSRecord.Text = domain.DNS_Record;
                            tbDNSRecordType.Text = domain.Type;
                            cmbDNSTTL.SelectedItem = domain.TTL.ToString().Trim().ToLower();

                            // SSL
                            tbSSLPath.Text = domain.SSLPath;
                            cmbSSLCountry.SelectedItem = domain.CountryName.ToString();
                            tbSSLState.Text = domain.State;
                            tbSSLLocality.Text = domain.Locality;
                            tbSSLOrganization.Text = domain.Organization;
                            tbSSLOU.Text = domain.OU;
                            tbSSLCommonName.Text = domain.CommonName;

                            // Notify
                            bool notify = bool.Parse(domain.SMTPSendNotification.ToString());

                            if (notify)
                            {
                                chkNotify.Checked = true;

                                lblSMTPServer.Text = "SMTP server: *";
                                lblSMTPPort.Text = "SMTP port: *";
                                lblSMTPUser.Text = "SMTP user: *";
                                lblSMTPPassword.Text = "SMTP password: *";
                                lblToEmail.Text = "To email: *";
                            }

                            tbSMTPServer.Text = domain.SMTPServer;
                            tbSMTPPort.Text = domain.SMTPPort;
                            tbSMTPUser.Text = domain.SMTPUser;
                            tbSMTPPassword.Text = domain.SMTPPassword;
                            tbToEmail.Text = domain.SMTPTo;

                            // Hmail
                            tbHmailPassword.Text = domain.hMailPassword;
                            tbHmailUser.Text = domain.hMailUser;

                            chkEncryptConfig.Checked = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                await CreateInitialConfigFileAsync();
            }
        }
        #endregion

        #region Event Handlers
        private async void btnTestEmail_Click(object sender, EventArgs e)
        {
            btnTestEmail.Text = "Sending...";
            btnTestEmail.Enabled = false;

            try
            {
                string smtpServer = tbSMTPServer.Text.Trim();
                string port = tbSMTPPort.Text.Trim();
                string smtpUser = tbSMTPUser.Text.Trim();
                string smtpPassword = tbSMTPPassword.Text;
                string SMTPTo = tbToEmail.Text.Trim();

                int smtpPort = int.Parse(port);

                MimeMessage msg = new MimeMessage();

                msg.To.Add(MailboxAddress.Parse(smtpUser));
                msg.From.Add(new MailboxAddress("SSL Notification", smtpUser));

                string message = "Successful Configuration of Email Notifications.";
                string subject = "Successful Configuration";
                msg.Priority = MessagePriority.Urgent;

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

                MailKit.Net.Smtp.SmtpClient smtp = new MailKit.Net.Smtp.SmtpClient();

                smtp.Timeout = 30000; // 30 seconds
                smtp.Connect(smtpServer, smtpPort, SecureSocketOptions.Auto);
                smtp.Authenticate(smtpUser, smtpPassword);

                await smtp.SendAsync(msg);
                smtp.Dispose();

                MessageBox.Show("Test email message sent successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not send a test email message. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnTestEmail.Text = "Test";
            btnTestEmail.Enabled = true;
        }
        private void btnTestHmail_Click(object sender, EventArgs e)
        {
            btnTestHmail.Text = "Testing...";
            btnTestHmail.Enabled = false;

            try
            {
                hMailServer.Application hMailApp = new hMailServer.Application();
                var isAuthenticated = hMailApp.Authenticate(tbHmailUser.Text.Trim(), tbHmailPassword.Text);

                if (isAuthenticated == null)
                {
                    MessageBox.Show("Could not connect to HmailServer. Verify user name and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("HmailServer connection is successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect to HmailServer. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnTestHmail.Text = "Test";
            btnTestHmail.Enabled = true;
        }
        private async void btnTestDNS_Click(object sender, EventArgs e)
        {
            if (cmbDNSTTL.SelectedItem == null)
            {
                MessageBox.Show("Please select TTL time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbDNSProvider.SelectedItem == null)
            {
                MessageBox.Show("Please select DNS provider.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (String.IsNullOrEmpty(tbDNSEmail.Text))
            {
                MessageBox.Show("Please provide valid DNS email address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnTestDNS.Text = "Testing...";
            btnTestDNS.Enabled = false;

            string apiKey = tbDNSApiKey.Text.Trim();
            string apiSecret = tbDNSApiSecret.Text.Trim();
            string email = tbDNSEmail.Text.Trim().ToLower();

            string domainName = tbDNSDomain.Text.Trim().ToLower();
            string dnsRecord = tbDNSRecord.Text.Trim();
            string type = tbDNSRecordType.Text.Trim();           
            string ttl = cmbDNSTTL.SelectedItem.ToString();

            if (cmbDNSProvider.SelectedItem.ToString() == "GoDaddy")
            {
                Dictionary<string, object> pushRecord = new Dictionary<string, object>()
                {
                    { "data", "test value" },
                    { "name", dnsRecord },
                    { "ttl", int.Parse(ttl) },
                    { "type", type },
                    { "port", 1 },
                    { "priority", 0 },
                    { "protocol", "string" },
                    { "service", "string" },
                    { "weight", 0 },
                };

                try
                {
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

                    MessageBox.Show("GoDaddy DNS test is successfully. A test value has been added to the DNS record " + dnsRecord + ".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not update GoDaddy DNS record. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
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

                    HttpWebResponse resp = (HttpWebResponse)await req.GetResponseAsync();
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

                    resp = (HttpWebResponse)await req.GetResponseAsync();
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

                    string jsonData = "{\"type\":\"" + type + "\",\"name\":\"" + dnsRecord + "." + domainName + "\",\"content\":\"" + "test value" + "\",\"ttl\":" + ttl + "}";

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

                    MessageBox.Show("CloudFlare DNS test is successfully. A test value has been added to the DNS record " + dnsRecord + ".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not update CloudFlare DNS record. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            btnTestDNS.Text = "Test";
            btnTestDNS.Enabled = true;
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if(cmbDNSProvider.SelectedItem.ToString() == "GoDaddy")
            {
                if (String.IsNullOrEmpty(tbDNSApiKey.Text.Trim()) || String.IsNullOrEmpty(tbDNSApiSecret.Text.Trim()) ||                     
                    String.IsNullOrEmpty(tbDNSDomain.Text.Trim()) || String.IsNullOrEmpty(tbDNSRecord.Text.Trim()) ||
                     String.IsNullOrEmpty(tbDNSEmail.Text.Trim()) ||
                    String.IsNullOrEmpty(tbDNSRecordType.Text.Trim()) || String.IsNullOrEmpty(cmbDNSTTL.SelectedItem.ToString()) ||
                    String.IsNullOrEmpty(tbSSLPath.Text.Trim()) || String.IsNullOrEmpty(cmbSSLCountry.SelectedItem.ToString()) ||
                    String.IsNullOrEmpty(tbSSLState.Text.Trim()) || String.IsNullOrEmpty(tbSSLLocality.Text.Trim()) ||
                    String.IsNullOrEmpty(tbSSLOrganization.Text.Trim()) || String.IsNullOrEmpty(tbSSLOU.Text.Trim()) ||
                    String.IsNullOrEmpty(tbSSLCommonName.Text.Trim()) || String.IsNullOrEmpty(tbHmailUser.Text.Trim()) ||
                    String.IsNullOrEmpty(tbHmailPassword.Text))
                {
                    MessageBox.Show("Required information is missing. All fields marked with * are obligatory. Please, review the configurations.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(tbDNSApiKey.Text.Trim()) || String.IsNullOrEmpty(tbDNSEmail.Text.Trim()) ||
                   String.IsNullOrEmpty(tbDNSDomain.Text.Trim()) || String.IsNullOrEmpty(tbDNSRecord.Text.Trim()) ||
                   String.IsNullOrEmpty(tbDNSRecordType.Text.Trim()) || String.IsNullOrEmpty(cmbDNSTTL.SelectedItem.ToString()) ||
                   String.IsNullOrEmpty(tbSSLPath.Text.Trim()) || String.IsNullOrEmpty(cmbSSLCountry.SelectedItem.ToString()) ||
                   String.IsNullOrEmpty(tbSSLState.Text.Trim()) || String.IsNullOrEmpty(tbSSLLocality.Text.Trim()) ||
                   String.IsNullOrEmpty(tbSSLOrganization.Text.Trim()) || String.IsNullOrEmpty(tbSSLOU.Text.Trim()) ||
                   String.IsNullOrEmpty(tbSSLCommonName.Text.Trim()) || String.IsNullOrEmpty(tbHmailUser.Text.Trim()) ||
                   String.IsNullOrEmpty(tbHmailPassword.Text))
                {
                    MessageBox.Show("Required information is missing. All fields marked with * are obligatory. Please, review the configurations.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


            if (chkNotify.Checked)
            {
                if (String.IsNullOrEmpty(tbSMTPServer.Text.Trim()) || String.IsNullOrEmpty(tbSMTPPort.Text.Trim()) ||
                    String.IsNullOrEmpty(tbSMTPUser.Text.Trim()) || String.IsNullOrEmpty(tbSMTPPassword.Text) ||
                    String.IsNullOrEmpty(tbToEmail.Text.Trim()))
                {
                    MessageBox.Show("Required information is missing. All fields marked with * are obligatory. Please, review the configurations.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (File.Exists(configPath))
            {
                try
                {
                    string configuration = await File.ReadAllTextAsync(configPath);

                    dynamic data;

                    try
                    {
                        data = JObject.Parse(configuration);
                    }
                    catch
                    {
                        MessageBox.Show("Invalid json format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dynamic domains = data.Domains;
                    string letsEncryptPemAccount = String.Empty;
                    bool isEncryptedConfig = false;

                    foreach (dynamic domain in domains)
                    {
                        letsEncryptPemAccount = domain.LetsEncryptPemAccount.ToString();
                        isEncryptedConfig = bool.Parse(domain.EncryptConfig.ToString().Trim().ToLower());

                        if (!String.IsNullOrEmpty(letsEncryptPemAccount))
                        {
                            try
                            {
                                if (!chkEncryptConfig.Checked && isEncryptedConfig)
                                {
                                    letsEncryptPemAccount = AesDecrypt(letsEncryptPemAccount);
                                }
                            }
                            catch { }
                        }
                    }

                    if (!chkEncryptConfig.Checked)
                    {                        
                        data["Domains"][0]["DNSProvider"] = cmbDNSProvider.SelectedItem.ToString();
                        data["Domains"][0]["APIKey"] = tbDNSApiKey.Text.Trim();
                        data["Domains"][0]["APISecret"] = tbDNSApiSecret.Text.Trim();
                        data["Domains"][0]["Email"] = tbDNSEmail.Text.Trim().ToLower();
                        data["Domains"][0]["Domain_Name"] = tbDNSDomain.Text.Trim().ToLower();
                        data["Domains"][0]["DNS_Record"] = tbDNSRecord.Text.Trim().ToLower();
                        data["Domains"][0]["Type"] = tbDNSRecordType.Text.Trim().ToUpper();
                        data["Domains"][0]["TTL"] = cmbDNSTTL.SelectedItem.ToString();

                        data["Domains"][0]["SSLPath"] = tbSSLPath.Text.Trim();
                        data["Domains"][0]["CountryName"] = cmbSSLCountry.SelectedItem.ToString();
                        data["Domains"][0]["State"] = tbSSLState.Text.Trim();
                        data["Domains"][0]["Locality"] = tbSSLLocality.Text.Trim();
                        data["Domains"][0]["Organization"] = tbSSLOrganization.Text.Trim();
                        data["Domains"][0]["OU"] = tbSSLOU.Text.Trim();
                        data["Domains"][0]["CommonName"] = tbSSLCommonName.Text.Trim().ToLower();
                        data["Domains"][0]["LetsEncryptPemAccount"] = letsEncryptPemAccount.Trim();

                        if (chkNotify.Checked)
                        {
                            data["Domains"][0]["SMTPSendNotification"] = "true";
                        }
                        else
                        {
                            data["Domains"][0]["SMTPSendNotification"] = "false";
                        }

                        data["Domains"][0]["SMTPServer"] = tbSMTPServer.Text.Trim().ToLower();
                        data["Domains"][0]["SMTPPort"] = tbSMTPPort.Text.Trim().ToLower();
                        data["Domains"][0]["SMTPUser"] = tbSMTPUser.Text.Trim();
                        data["Domains"][0]["SMTPPassword"] = tbSMTPPassword.Text;
                        data["Domains"][0]["SMTPTo"] = tbToEmail.Text.Trim().ToLower();

                        data["Domains"][0]["hMailUser"] = tbHmailUser.Text.Trim();
                        data["Domains"][0]["hMailPassword"] = tbHmailPassword.Text;

                        data["Domains"][0]["EncryptConfig"] = "false";
                    }
                    else
                    {
                        data["Domains"][0]["DNSProvider"] = cmbDNSProvider.SelectedItem.ToString();
                        data["Domains"][0]["APIKey"] = AesEncrypt(tbDNSApiKey.Text.Trim());
                        data["Domains"][0]["APISecret"] = AesEncrypt(tbDNSApiSecret.Text.Trim());
                        data["Domains"][0]["Email"] = AesEncrypt(tbDNSEmail.Text.Trim().ToLower());
                        data["Domains"][0]["Domain_Name"] = AesEncrypt(tbDNSDomain.Text.Trim().ToLower());
                        data["Domains"][0]["DNS_Record"] = AesEncrypt(tbDNSRecord.Text.Trim().ToLower());
                        data["Domains"][0]["Type"] = tbDNSRecordType.Text.Trim().ToUpper();
                        data["Domains"][0]["TTL"] = cmbDNSTTL.SelectedItem.ToString();

                        data["Domains"][0]["SSLPath"] = AesEncrypt(tbSSLPath.Text.Trim());
                        data["Domains"][0]["CountryName"] = AesEncrypt(cmbSSLCountry.SelectedItem.ToString());
                        data["Domains"][0]["State"] = AesEncrypt(tbSSLState.Text.Trim());
                        data["Domains"][0]["Locality"] = AesEncrypt(tbSSLLocality.Text.Trim());
                        data["Domains"][0]["Organization"] = AesEncrypt(tbSSLOrganization.Text.Trim());
                        data["Domains"][0]["OU"] = AesEncrypt(tbSSLOU.Text.Trim());
                        data["Domains"][0]["CommonName"] = AesEncrypt(tbSSLCommonName.Text.Trim().ToLower());
                        data["Domains"][0]["LetsEncryptPemAccount"] = AesEncrypt(letsEncryptPemAccount.Trim());

                        if (chkNotify.Checked)
                        {
                            data["Domains"][0]["SMTPSendNotification"] = "true";
                        }
                        else
                        {
                            data["Domains"][0]["SMTPSendNotification"] = "false";
                        }

                        data["Domains"][0]["SMTPServer"] = AesEncrypt(tbSMTPServer.Text.Trim().ToLower());
                        data["Domains"][0]["SMTPPort"] = AesEncrypt(tbSMTPPort.Text.Trim().ToLower());
                        data["Domains"][0]["SMTPUser"] = AesEncrypt(tbSMTPUser.Text.Trim());
                        data["Domains"][0]["SMTPPassword"] = AesEncrypt(tbSMTPPassword.Text);
                        data["Domains"][0]["SMTPTo"] = AesEncrypt(tbToEmail.Text.Trim().ToLower());

                        data["Domains"][0]["hMailUser"] = AesEncrypt(tbHmailUser.Text.Trim());
                        data["Domains"][0]["hMailPassword"] = AesEncrypt(tbHmailPassword.Text);

                        data["Domains"][0]["EncryptConfig"] = "true";
                    }

                    File.WriteAllText(configPath, data.ToString(Formatting.Indented));

                    MessageBox.Show("All changes saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not update the configuration file. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                await CreateInitialConfigFileAsync();
            }
        }
        private void chkCloudFlareApi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDNSApi.Checked == true)
            {
                tbDNSApiKey.UseSystemPasswordChar = false;
            }
            else
            {
                tbDNSApiKey.UseSystemPasswordChar = true;
            }
        }
        private void chkNotifyPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNotifyPassword.Checked == true)
            {
                tbSMTPPassword.UseSystemPasswordChar = false;
            }
            else
            {
                tbSMTPPassword.UseSystemPasswordChar = true;
            }
        }
        private void chkHmailPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHmailPassword.Checked == true)
            {
                tbHmailPassword.UseSystemPasswordChar = false;
            }
            else
            {
                tbHmailPassword.UseSystemPasswordChar = true;
            }
        }
        private void tbSMTPPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void chkNotify_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNotify.Checked)
            {
                lblSMTPServer.Text = "SMTP server: *";
                lblSMTPPort.Text = "SMTP port: *";
                lblSMTPUser.Text = "SMTP user: *";
                lblSMTPPassword.Text = "SMTP password: *";
                lblToEmail.Text = "To email: *";
            }
            else
            {
                lblSMTPServer.Text = "SMTP server:";
                lblSMTPPort.Text = "SMTP port:";
                lblSMTPUser.Text = "SMTP user:";
                lblSMTPPassword.Text = "SMTP password:";
                lblToEmail.Text = "To email:";
            }
        }
        private void chkDNSApiSecret_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDNSApiSecret.Checked)
            {
                tbDNSApiSecret.UseSystemPasswordChar = false;
            }
            else
            {
                tbDNSApiSecret.UseSystemPasswordChar = true;
            }
        }
        private void cmbDNSProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDNSProvider.SelectedItem.ToString() == "GoDaddy")
            {
                tbDNSApiSecret.Enabled = true;
                lblDNSApiSecret.Text = "API secret: *";

                chkDNSApiSecret.Enabled = true;
                chkDNSApiSecret.Checked = false;               

                cmbDNSTTL.Items.Clear();

                cmbDNSTTL.Items.Add("600"); // 10 min
                cmbDNSTTL.Items.Add("1800"); // 30 min
                cmbDNSTTL.Items.Add("3600"); // 1 hour
                cmbDNSTTL.Items.Add("43200"); // 12 hours
                cmbDNSTTL.Items.Add("86400"); // 1 day
                cmbDNSTTL.Items.Add("604800"); // 1 week
            }
            else
            {
                tbDNSApiSecret.Enabled = false;
                tbDNSApiSecret.Text = String.Empty;
                lblDNSApiSecret.Text = "API secret:";

                chkDNSApiSecret.Enabled = false;
                chkDNSApiSecret.Checked = false;

                cmbDNSTTL.Items.Clear();

                cmbDNSTTL.Items.Add("60"); // 1 min
                cmbDNSTTL.Items.Add("120"); // 2 min
                cmbDNSTTL.Items.Add("300"); // 5 min
                cmbDNSTTL.Items.Add("600"); // 10 min
                cmbDNSTTL.Items.Add("900"); // 15 min
                cmbDNSTTL.Items.Add("1800"); // 30 min
                cmbDNSTTL.Items.Add("3600"); // 1 hour
                cmbDNSTTL.Items.Add("7200"); // 2 hours
                cmbDNSTTL.Items.Add("18000"); // 5 hours
                cmbDNSTTL.Items.Add("43200"); // 12 hours
                cmbDNSTTL.Items.Add("86400"); // 1 day
            }
        }
        #endregion

        #region Methods
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
        protected static async Task CreateInitialConfigFileAsync()
        {
            string fileContent = "{\"Domains\":[{\"DNSProvider\":\"\",\"APIKey\":\"\",\"APISecret\":\"\",\"Email\":\"\",\"Domain_Name\":\"\",\"DNS_Record\":\"_acme-challenge.mail\",\"Type\":\"TXT\",\"TTL\":\"\",\"SSLPath\":\"\",\"CountryName\":\"\",\"State\":\"\",\"Locality\":\"\",\"Organization\":\"\",\"OU\":\"\",\"CommonName\":\"\",\"LetsEncryptPemAccount\":\"\",\"SMTPSendNotification\":\"false\",\"SMTPServer\":\"\",\"SMTPPort\":\"\",\"SMTPUser\":\"\",\"SMTPPassword\":\"\",\"SMTPTo\":\"\",\"hMailUser\":\"Administrator\",\"hMailPassword\":\"\",\"EncryptConfig\":\"false\"}]}";

            try
            {
                await File.WriteAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "config.dat"), fileContent);
                MessageBox.Show("The initial configuration file has been successfully created, as no existing configuration file was found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not create initial configuration file. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion        
    }
}