namespace Configure
{
    partial class Config
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            gbCloudFlare = new GroupBox();
            btnTestCloudFlate = new Button();
            chkCloudFlareApi = new CheckBox();
            cmbCloudFlareTTL = new ComboBox();
            lblTTLDesc = new Label();
            lblCloudFlareTTL = new Label();
            tbCloudFlareRecordType = new TextBox();
            lblCloudFlareType = new Label();
            tbCloudFlareDNSRecord = new TextBox();
            lblCloudFlareDNSRecord = new Label();
            tbCloudFlareDomain = new TextBox();
            lblCloudFlareDomain = new Label();
            tbCloudFlareEmail = new TextBox();
            lblCloudFlareEmail = new Label();
            tbCloudFlareApiKey = new TextBox();
            lblCloudFlareApiKey = new Label();
            btnSave = new Button();
            gbSSL = new GroupBox();
            lblSSLCountryDesc = new Label();
            cmbSSLCountry = new ComboBox();
            tbSSLCommonName = new TextBox();
            lblSSLCommonName = new Label();
            tbSSLOU = new TextBox();
            lblSSLOU = new Label();
            tbSSLOrganization = new TextBox();
            lblSSLOrganization = new Label();
            tbSSLLocality = new TextBox();
            lblSSLLocality = new Label();
            tbSSLState = new TextBox();
            lblSSLState = new Label();
            lblDBPort = new Label();
            tbSSLPath = new TextBox();
            lblSSLPath = new Label();
            gbNotification = new GroupBox();
            label1 = new Label();
            btnTestEmail = new Button();
            chkNotifyPassword = new CheckBox();
            tbToEmail = new TextBox();
            lblToEmail = new Label();
            tbSMTPPassword = new TextBox();
            lblSMTPPassword = new Label();
            tbSMTPUser = new TextBox();
            lblSMTPUser = new Label();
            tbSMTPPort = new TextBox();
            lblSMTPPort = new Label();
            tbSMTPServer = new TextBox();
            lblSMTPServer = new Label();
            chkNotify = new CheckBox();
            lblNotify = new Label();
            gbHmail = new GroupBox();
            btnTestHmail = new Button();
            chkHmailPassword = new CheckBox();
            tbHmailPassword = new TextBox();
            lblHmailPassword = new Label();
            tbHmailUser = new TextBox();
            lblHmailUser = new Label();
            lblSpacer = new Label();
            lblEncrypt = new Label();
            chkEncryptConfig = new CheckBox();
            gbCloudFlare.SuspendLayout();
            gbSSL.SuspendLayout();
            gbNotification.SuspendLayout();
            gbHmail.SuspendLayout();
            SuspendLayout();
            // 
            // gbCloudFlare
            // 
            gbCloudFlare.BackColor = SystemColors.Control;
            gbCloudFlare.Controls.Add(btnTestCloudFlate);
            gbCloudFlare.Controls.Add(chkCloudFlareApi);
            gbCloudFlare.Controls.Add(cmbCloudFlareTTL);
            gbCloudFlare.Controls.Add(lblTTLDesc);
            gbCloudFlare.Controls.Add(lblCloudFlareTTL);
            gbCloudFlare.Controls.Add(tbCloudFlareRecordType);
            gbCloudFlare.Controls.Add(lblCloudFlareType);
            gbCloudFlare.Controls.Add(tbCloudFlareDNSRecord);
            gbCloudFlare.Controls.Add(lblCloudFlareDNSRecord);
            gbCloudFlare.Controls.Add(tbCloudFlareDomain);
            gbCloudFlare.Controls.Add(lblCloudFlareDomain);
            gbCloudFlare.Controls.Add(tbCloudFlareEmail);
            gbCloudFlare.Controls.Add(lblCloudFlareEmail);
            gbCloudFlare.Controls.Add(tbCloudFlareApiKey);
            gbCloudFlare.Controls.Add(lblCloudFlareApiKey);
            gbCloudFlare.Location = new Point(12, 12);
            gbCloudFlare.Name = "gbCloudFlare";
            gbCloudFlare.Size = new Size(500, 295);
            gbCloudFlare.TabIndex = 0;
            gbCloudFlare.TabStop = false;
            gbCloudFlare.Text = "CloudFlare API";
            // 
            // btnTestCloudFlate
            // 
            btnTestCloudFlate.BackColor = SystemColors.Control;
            btnTestCloudFlate.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnTestCloudFlate.ForeColor = Color.Red;
            btnTestCloudFlate.Location = new Point(205, 253);
            btnTestCloudFlate.Name = "btnTestCloudFlate";
            btnTestCloudFlate.Size = new Size(91, 27);
            btnTestCloudFlate.TabIndex = 8;
            btnTestCloudFlate.Text = "Test";
            btnTestCloudFlate.UseVisualStyleBackColor = false;
            btnTestCloudFlate.Click += btnTestCloudFlate_Click;
            // 
            // chkCloudFlareApi
            // 
            chkCloudFlareApi.AutoSize = true;
            chkCloudFlareApi.Location = new Point(475, 28);
            chkCloudFlareApi.Name = "chkCloudFlareApi";
            chkCloudFlareApi.Size = new Size(15, 14);
            chkCloudFlareApi.TabIndex = 2;
            chkCloudFlareApi.UseVisualStyleBackColor = true;
            chkCloudFlareApi.CheckedChanged += chkCloudFlareApi_CheckedChanged;
            // 
            // cmbCloudFlareTTL
            // 
            cmbCloudFlareTTL.FormattingEnabled = true;
            cmbCloudFlareTTL.Location = new Point(90, 215);
            cmbCloudFlareTTL.Name = "cmbCloudFlareTTL";
            cmbCloudFlareTTL.Size = new Size(121, 23);
            cmbCloudFlareTTL.TabIndex = 7;
            // 
            // lblTTLDesc
            // 
            lblTTLDesc.AutoSize = true;
            lblTTLDesc.Location = new Point(225, 218);
            lblTTLDesc.Name = "lblTTLDesc";
            lblTTLDesc.Size = new Size(71, 15);
            lblTTLDesc.TabIndex = 16;
            lblTTLDesc.Text = "(in seconds)";
            // 
            // lblCloudFlareTTL
            // 
            lblCloudFlareTTL.AutoSize = true;
            lblCloudFlareTTL.Location = new Point(6, 218);
            lblCloudFlareTTL.Name = "lblCloudFlareTTL";
            lblCloudFlareTTL.Size = new Size(36, 15);
            lblCloudFlareTTL.TabIndex = 10;
            lblCloudFlareTTL.Text = "TTL: *";
            // 
            // tbCloudFlareRecordType
            // 
            tbCloudFlareRecordType.Location = new Point(90, 177);
            tbCloudFlareRecordType.MaxLength = 4;
            tbCloudFlareRecordType.Name = "tbCloudFlareRecordType";
            tbCloudFlareRecordType.ReadOnly = true;
            tbCloudFlareRecordType.Size = new Size(121, 23);
            tbCloudFlareRecordType.TabIndex = 6;
            tbCloudFlareRecordType.Text = "TXT";
            // 
            // lblCloudFlareType
            // 
            lblCloudFlareType.AutoSize = true;
            lblCloudFlareType.Location = new Point(6, 180);
            lblCloudFlareType.Name = "lblCloudFlareType";
            lblCloudFlareType.Size = new Size(81, 15);
            lblCloudFlareType.TabIndex = 8;
            lblCloudFlareType.Text = "Record type: *";
            // 
            // tbCloudFlareDNSRecord
            // 
            tbCloudFlareDNSRecord.CharacterCasing = CharacterCasing.Lower;
            tbCloudFlareDNSRecord.Location = new Point(90, 138);
            tbCloudFlareDNSRecord.MaxLength = 1000;
            tbCloudFlareDNSRecord.Name = "tbCloudFlareDNSRecord";
            tbCloudFlareDNSRecord.Size = new Size(209, 23);
            tbCloudFlareDNSRecord.TabIndex = 5;
            tbCloudFlareDNSRecord.Text = "_acme-challenge.mail";
            // 
            // lblCloudFlareDNSRecord
            // 
            lblCloudFlareDNSRecord.AutoSize = true;
            lblCloudFlareDNSRecord.Location = new Point(6, 141);
            lblCloudFlareDNSRecord.Name = "lblCloudFlareDNSRecord";
            lblCloudFlareDNSRecord.Size = new Size(78, 15);
            lblCloudFlareDNSRecord.TabIndex = 6;
            lblCloudFlareDNSRecord.Text = "DNS record: *";
            // 
            // tbCloudFlareDomain
            // 
            tbCloudFlareDomain.CharacterCasing = CharacterCasing.Lower;
            tbCloudFlareDomain.Location = new Point(90, 100);
            tbCloudFlareDomain.MaxLength = 1000;
            tbCloudFlareDomain.Name = "tbCloudFlareDomain";
            tbCloudFlareDomain.Size = new Size(400, 23);
            tbCloudFlareDomain.TabIndex = 4;
            // 
            // lblCloudFlareDomain
            // 
            lblCloudFlareDomain.AutoSize = true;
            lblCloudFlareDomain.Location = new Point(6, 103);
            lblCloudFlareDomain.Name = "lblCloudFlareDomain";
            lblCloudFlareDomain.Size = new Size(60, 15);
            lblCloudFlareDomain.TabIndex = 4;
            lblCloudFlareDomain.Text = "Domain: *";
            // 
            // tbCloudFlareEmail
            // 
            tbCloudFlareEmail.CharacterCasing = CharacterCasing.Lower;
            tbCloudFlareEmail.Location = new Point(90, 62);
            tbCloudFlareEmail.MaxLength = 1000;
            tbCloudFlareEmail.Name = "tbCloudFlareEmail";
            tbCloudFlareEmail.Size = new Size(400, 23);
            tbCloudFlareEmail.TabIndex = 3;
            // 
            // lblCloudFlareEmail
            // 
            lblCloudFlareEmail.AutoSize = true;
            lblCloudFlareEmail.Location = new Point(6, 65);
            lblCloudFlareEmail.Name = "lblCloudFlareEmail";
            lblCloudFlareEmail.Size = new Size(47, 15);
            lblCloudFlareEmail.TabIndex = 2;
            lblCloudFlareEmail.Text = "Email: *";
            // 
            // tbCloudFlareApiKey
            // 
            tbCloudFlareApiKey.Location = new Point(90, 24);
            tbCloudFlareApiKey.MaxLength = 1000;
            tbCloudFlareApiKey.Name = "tbCloudFlareApiKey";
            tbCloudFlareApiKey.Size = new Size(379, 23);
            tbCloudFlareApiKey.TabIndex = 1;
            tbCloudFlareApiKey.UseSystemPasswordChar = true;
            // 
            // lblCloudFlareApiKey
            // 
            lblCloudFlareApiKey.AutoSize = true;
            lblCloudFlareApiKey.Location = new Point(6, 27);
            lblCloudFlareApiKey.Name = "lblCloudFlareApiKey";
            lblCloudFlareApiKey.Size = new Size(57, 15);
            lblCloudFlareApiKey.TabIndex = 0;
            lblCloudFlareApiKey.Text = "API key: *";
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.Location = new Point(358, 1061);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(154, 40);
            btnSave.TabIndex = 29;
            btnSave.Text = "Save changes";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // gbSSL
            // 
            gbSSL.BackColor = SystemColors.Control;
            gbSSL.Controls.Add(lblSSLCountryDesc);
            gbSSL.Controls.Add(cmbSSLCountry);
            gbSSL.Controls.Add(tbSSLCommonName);
            gbSSL.Controls.Add(lblSSLCommonName);
            gbSSL.Controls.Add(tbSSLOU);
            gbSSL.Controls.Add(lblSSLOU);
            gbSSL.Controls.Add(tbSSLOrganization);
            gbSSL.Controls.Add(lblSSLOrganization);
            gbSSL.Controls.Add(tbSSLLocality);
            gbSSL.Controls.Add(lblSSLLocality);
            gbSSL.Controls.Add(tbSSLState);
            gbSSL.Controls.Add(lblSSLState);
            gbSSL.Controls.Add(lblDBPort);
            gbSSL.Controls.Add(tbSSLPath);
            gbSSL.Controls.Add(lblSSLPath);
            gbSSL.Location = new Point(13, 317);
            gbSSL.Name = "gbSSL";
            gbSSL.Size = new Size(499, 287);
            gbSSL.TabIndex = 2;
            gbSSL.TabStop = false;
            gbSSL.Text = "Certificate information";
            // 
            // lblSSLCountryDesc
            // 
            lblSSLCountryDesc.AutoSize = true;
            lblSSLCountryDesc.Location = new Point(231, 63);
            lblSSLCountryDesc.Name = "lblSSLCountryDesc";
            lblSSLCountryDesc.Size = new Size(135, 15);
            lblSSLCountryDesc.TabIndex = 17;
            lblSSLCountryDesc.Text = "(country two letters ISO)";
            // 
            // cmbSSLCountry
            // 
            cmbSSLCountry.FormattingEnabled = true;
            cmbSSLCountry.Location = new Point(104, 60);
            cmbSSLCountry.Name = "cmbSSLCountry";
            cmbSSLCountry.Size = new Size(121, 23);
            cmbSSLCountry.TabIndex = 10;
            // 
            // tbSSLCommonName
            // 
            tbSSLCommonName.CharacterCasing = CharacterCasing.Lower;
            tbSSLCommonName.Location = new Point(104, 248);
            tbSSLCommonName.MaxLength = 1000;
            tbSSLCommonName.Name = "tbSSLCommonName";
            tbSSLCommonName.Size = new Size(383, 23);
            tbSSLCommonName.TabIndex = 15;
            // 
            // lblSSLCommonName
            // 
            lblSSLCommonName.AutoSize = true;
            lblSSLCommonName.Location = new Point(5, 251);
            lblSSLCommonName.Name = "lblSSLCommonName";
            lblSSLCommonName.Size = new Size(102, 15);
            lblSSLCommonName.TabIndex = 15;
            lblSSLCommonName.Text = "Common name: *";
            // 
            // tbSSLOU
            // 
            tbSSLOU.Location = new Point(104, 210);
            tbSSLOU.MaxLength = 1000;
            tbSSLOU.Name = "tbSSLOU";
            tbSSLOU.Size = new Size(383, 23);
            tbSSLOU.TabIndex = 14;
            // 
            // lblSSLOU
            // 
            lblSSLOU.AutoSize = true;
            lblSSLOU.Location = new Point(5, 213);
            lblSSLOU.Name = "lblSSLOU";
            lblSSLOU.Size = new Size(35, 15);
            lblSSLOU.TabIndex = 13;
            lblSSLOU.Text = "OU: *";
            // 
            // tbSSLOrganization
            // 
            tbSSLOrganization.Location = new Point(104, 172);
            tbSSLOrganization.MaxLength = 1000;
            tbSSLOrganization.Name = "tbSSLOrganization";
            tbSSLOrganization.Size = new Size(383, 23);
            tbSSLOrganization.TabIndex = 13;
            // 
            // lblSSLOrganization
            // 
            lblSSLOrganization.AutoSize = true;
            lblSSLOrganization.Location = new Point(5, 175);
            lblSSLOrganization.Name = "lblSSLOrganization";
            lblSSLOrganization.Size = new Size(86, 15);
            lblSSLOrganization.TabIndex = 11;
            lblSSLOrganization.Text = "Organization: *";
            // 
            // tbSSLLocality
            // 
            tbSSLLocality.Location = new Point(104, 134);
            tbSSLLocality.MaxLength = 1000;
            tbSSLLocality.Name = "tbSSLLocality";
            tbSSLLocality.Size = new Size(383, 23);
            tbSSLLocality.TabIndex = 12;
            // 
            // lblSSLLocality
            // 
            lblSSLLocality.AutoSize = true;
            lblSSLLocality.Location = new Point(5, 137);
            lblSSLLocality.Name = "lblSSLLocality";
            lblSSLLocality.Size = new Size(59, 15);
            lblSSLLocality.TabIndex = 9;
            lblSSLLocality.Text = "Locality: *";
            // 
            // tbSSLState
            // 
            tbSSLState.Location = new Point(104, 97);
            tbSSLState.MaxLength = 1000;
            tbSSLState.Name = "tbSSLState";
            tbSSLState.Size = new Size(383, 23);
            tbSSLState.TabIndex = 11;
            // 
            // lblSSLState
            // 
            lblSSLState.AutoSize = true;
            lblSSLState.Location = new Point(5, 100);
            lblSSLState.Name = "lblSSLState";
            lblSSLState.Size = new Size(77, 15);
            lblSSLState.TabIndex = 7;
            lblSSLState.Text = "State name: *";
            // 
            // lblDBPort
            // 
            lblDBPort.AutoSize = true;
            lblDBPort.Location = new Point(5, 63);
            lblDBPort.Name = "lblDBPort";
            lblDBPort.Size = new Size(94, 15);
            lblDBPort.TabIndex = 4;
            lblDBPort.Text = "Country name: *";
            // 
            // tbSSLPath
            // 
            tbSSLPath.Location = new Point(104, 22);
            tbSSLPath.Name = "tbSSLPath";
            tbSSLPath.Size = new Size(383, 23);
            tbSSLPath.TabIndex = 9;
            // 
            // lblSSLPath
            // 
            lblSSLPath.AutoSize = true;
            lblSSLPath.Location = new Point(5, 25);
            lblSSLPath.Name = "lblSSLPath";
            lblSSLPath.Size = new Size(63, 15);
            lblSSLPath.TabIndex = 2;
            lblSSLPath.Text = "SSL path: *";
            // 
            // gbNotification
            // 
            gbNotification.BackColor = SystemColors.Control;
            gbNotification.Controls.Add(label1);
            gbNotification.Controls.Add(btnTestEmail);
            gbNotification.Controls.Add(chkNotifyPassword);
            gbNotification.Controls.Add(tbToEmail);
            gbNotification.Controls.Add(lblToEmail);
            gbNotification.Controls.Add(tbSMTPPassword);
            gbNotification.Controls.Add(lblSMTPPassword);
            gbNotification.Controls.Add(tbSMTPUser);
            gbNotification.Controls.Add(lblSMTPUser);
            gbNotification.Controls.Add(tbSMTPPort);
            gbNotification.Controls.Add(lblSMTPPort);
            gbNotification.Controls.Add(tbSMTPServer);
            gbNotification.Controls.Add(lblSMTPServer);
            gbNotification.Controls.Add(chkNotify);
            gbNotification.Controls.Add(lblNotify);
            gbNotification.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            gbNotification.Location = new Point(18, 619);
            gbNotification.Name = "gbNotification";
            gbNotification.Size = new Size(494, 281);
            gbNotification.TabIndex = 3;
            gbNotification.TabStop = false;
            gbNotification.Text = "Email notification";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Red;
            label1.Location = new Point(17, 253);
            label1.Name = "label1";
            label1.Size = new Size(458, 15);
            label1.TabIndex = 25;
            label1.Text = "Do not utilize an email address or the SMTP server you are updating the certificate for.";
            // 
            // btnTestEmail
            // 
            btnTestEmail.BackColor = SystemColors.Control;
            btnTestEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnTestEmail.ForeColor = Color.Red;
            btnTestEmail.Location = new Point(199, 217);
            btnTestEmail.Name = "btnTestEmail";
            btnTestEmail.Size = new Size(91, 27);
            btnTestEmail.TabIndex = 23;
            btnTestEmail.Text = "Test";
            btnTestEmail.UseVisualStyleBackColor = false;
            btnTestEmail.Click += btnTestEmail_Click;
            // 
            // chkNotifyPassword
            // 
            chkNotifyPassword.AutoSize = true;
            chkNotifyPassword.Location = new Point(471, 151);
            chkNotifyPassword.Name = "chkNotifyPassword";
            chkNotifyPassword.Size = new Size(15, 14);
            chkNotifyPassword.TabIndex = 21;
            chkNotifyPassword.UseVisualStyleBackColor = true;
            chkNotifyPassword.CheckedChanged += chkNotifyPassword_CheckedChanged;
            // 
            // tbToEmail
            // 
            tbToEmail.CharacterCasing = CharacterCasing.Lower;
            tbToEmail.Location = new Point(103, 182);
            tbToEmail.MaxLength = 1000;
            tbToEmail.Name = "tbToEmail";
            tbToEmail.Size = new Size(383, 23);
            tbToEmail.TabIndex = 22;
            // 
            // lblToEmail
            // 
            lblToEmail.AutoSize = true;
            lblToEmail.Location = new Point(6, 185);
            lblToEmail.Name = "lblToEmail";
            lblToEmail.Size = new Size(54, 15);
            lblToEmail.TabIndex = 24;
            lblToEmail.Text = "To email:";
            // 
            // tbSMTPPassword
            // 
            tbSMTPPassword.Location = new Point(103, 147);
            tbSMTPPassword.MaxLength = 1000;
            tbSMTPPassword.Name = "tbSMTPPassword";
            tbSMTPPassword.Size = new Size(356, 23);
            tbSMTPPassword.TabIndex = 20;
            tbSMTPPassword.UseSystemPasswordChar = true;
            // 
            // lblSMTPPassword
            // 
            lblSMTPPassword.AutoSize = true;
            lblSMTPPassword.Location = new Point(6, 150);
            lblSMTPPassword.Name = "lblSMTPPassword";
            lblSMTPPassword.Size = new Size(93, 15);
            lblSMTPPassword.TabIndex = 22;
            lblSMTPPassword.Text = "SMTP password:";
            // 
            // tbSMTPUser
            // 
            tbSMTPUser.CharacterCasing = CharacterCasing.Lower;
            tbSMTPUser.Location = new Point(103, 112);
            tbSMTPUser.MaxLength = 1000;
            tbSMTPUser.Name = "tbSMTPUser";
            tbSMTPUser.Size = new Size(383, 23);
            tbSMTPUser.TabIndex = 19;
            // 
            // lblSMTPUser
            // 
            lblSMTPUser.AutoSize = true;
            lblSMTPUser.Location = new Point(6, 115);
            lblSMTPUser.Name = "lblSMTPUser";
            lblSMTPUser.Size = new Size(65, 15);
            lblSMTPUser.TabIndex = 20;
            lblSMTPUser.Text = "SMTP user:";
            // 
            // tbSMTPPort
            // 
            tbSMTPPort.Location = new Point(103, 79);
            tbSMTPPort.MaxLength = 5;
            tbSMTPPort.Name = "tbSMTPPort";
            tbSMTPPort.Size = new Size(81, 23);
            tbSMTPPort.TabIndex = 18;
            tbSMTPPort.KeyPress += tbSMTPPort_KeyPress;
            // 
            // lblSMTPPort
            // 
            lblSMTPPort.AutoSize = true;
            lblSMTPPort.Location = new Point(6, 82);
            lblSMTPPort.Name = "lblSMTPPort";
            lblSMTPPort.Size = new Size(65, 15);
            lblSMTPPort.TabIndex = 18;
            lblSMTPPort.Text = "SMTP port:";
            // 
            // tbSMTPServer
            // 
            tbSMTPServer.CharacterCasing = CharacterCasing.Lower;
            tbSMTPServer.Location = new Point(103, 45);
            tbSMTPServer.MaxLength = 1000;
            tbSMTPServer.Name = "tbSMTPServer";
            tbSMTPServer.Size = new Size(383, 23);
            tbSMTPServer.TabIndex = 17;
            // 
            // lblSMTPServer
            // 
            lblSMTPServer.AutoSize = true;
            lblSMTPServer.Location = new Point(6, 48);
            lblSMTPServer.Name = "lblSMTPServer";
            lblSMTPServer.Size = new Size(74, 15);
            lblSMTPServer.TabIndex = 16;
            lblSMTPServer.Text = "SMTP server:";
            // 
            // chkNotify
            // 
            chkNotify.AutoSize = true;
            chkNotify.Location = new Point(103, 23);
            chkNotify.Name = "chkNotify";
            chkNotify.Size = new Size(15, 14);
            chkNotify.TabIndex = 16;
            chkNotify.UseVisualStyleBackColor = true;
            chkNotify.CheckedChanged += chkNotify_CheckedChanged;
            // 
            // lblNotify
            // 
            lblNotify.AutoSize = true;
            lblNotify.Location = new Point(6, 22);
            lblNotify.Name = "lblNotify";
            lblNotify.Size = new Size(43, 15);
            lblNotify.TabIndex = 3;
            lblNotify.Text = "Notify:";
            // 
            // gbHmail
            // 
            gbHmail.Controls.Add(btnTestHmail);
            gbHmail.Controls.Add(chkHmailPassword);
            gbHmail.Controls.Add(tbHmailPassword);
            gbHmail.Controls.Add(lblHmailPassword);
            gbHmail.Controls.Add(tbHmailUser);
            gbHmail.Controls.Add(lblHmailUser);
            gbHmail.Location = new Point(18, 910);
            gbHmail.Name = "gbHmail";
            gbHmail.Size = new Size(494, 133);
            gbHmail.TabIndex = 4;
            gbHmail.TabStop = false;
            gbHmail.Text = "HmailServer access";
            // 
            // btnTestHmail
            // 
            btnTestHmail.BackColor = SystemColors.Control;
            btnTestHmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnTestHmail.ForeColor = Color.Red;
            btnTestHmail.Location = new Point(199, 96);
            btnTestHmail.Name = "btnTestHmail";
            btnTestHmail.Size = new Size(91, 27);
            btnTestHmail.TabIndex = 27;
            btnTestHmail.Text = "Test";
            btnTestHmail.UseVisualStyleBackColor = false;
            btnTestHmail.Click += btnTestHmail_Click;
            // 
            // chkHmailPassword
            // 
            chkHmailPassword.AutoSize = true;
            chkHmailPassword.Location = new Point(466, 64);
            chkHmailPassword.Name = "chkHmailPassword";
            chkHmailPassword.Size = new Size(15, 14);
            chkHmailPassword.TabIndex = 26;
            chkHmailPassword.UseVisualStyleBackColor = true;
            chkHmailPassword.CheckedChanged += chkHmailPassword_CheckedChanged;
            // 
            // tbHmailPassword
            // 
            tbHmailPassword.Location = new Point(98, 60);
            tbHmailPassword.MaxLength = 1000;
            tbHmailPassword.Name = "tbHmailPassword";
            tbHmailPassword.Size = new Size(356, 23);
            tbHmailPassword.TabIndex = 25;
            tbHmailPassword.UseSystemPasswordChar = true;
            // 
            // lblHmailPassword
            // 
            lblHmailPassword.AutoSize = true;
            lblHmailPassword.Location = new Point(6, 63);
            lblHmailPassword.Name = "lblHmailPassword";
            lblHmailPassword.Size = new Size(68, 15);
            lblHmailPassword.TabIndex = 26;
            lblHmailPassword.Text = "Password: *";
            // 
            // tbHmailUser
            // 
            tbHmailUser.Location = new Point(98, 25);
            tbHmailUser.MaxLength = 1000;
            tbHmailUser.Name = "tbHmailUser";
            tbHmailUser.Size = new Size(383, 23);
            tbHmailUser.TabIndex = 24;
            tbHmailUser.Text = "Administrator";
            // 
            // lblHmailUser
            // 
            lblHmailUser.AutoSize = true;
            lblHmailUser.Location = new Point(6, 28);
            lblHmailUser.Name = "lblHmailUser";
            lblHmailUser.Size = new Size(74, 15);
            lblHmailUser.TabIndex = 25;
            lblHmailUser.Text = "User name: *";
            // 
            // lblSpacer
            // 
            lblSpacer.AutoSize = true;
            lblSpacer.Location = new Point(246, 1100);
            lblSpacer.Name = "lblSpacer";
            lblSpacer.Size = new Size(0, 15);
            lblSpacer.TabIndex = 27;
            // 
            // lblEncrypt
            // 
            lblEncrypt.AutoSize = true;
            lblEncrypt.Location = new Point(24, 1075);
            lblEncrypt.Name = "lblEncrypt";
            lblEncrypt.Size = new Size(144, 15);
            lblEncrypt.TabIndex = 27;
            lblEncrypt.Text = "Encrypt configuration file:";
            // 
            // chkEncryptConfig
            // 
            chkEncryptConfig.AutoSize = true;
            chkEncryptConfig.Location = new Point(174, 1076);
            chkEncryptConfig.Name = "chkEncryptConfig";
            chkEncryptConfig.Size = new Size(15, 14);
            chkEncryptConfig.TabIndex = 28;
            chkEncryptConfig.UseVisualStyleBackColor = true;
            // 
            // Config
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(534, 561);
            Controls.Add(chkEncryptConfig);
            Controls.Add(lblEncrypt);
            Controls.Add(lblSpacer);
            Controls.Add(gbHmail);
            Controls.Add(gbNotification);
            Controls.Add(gbSSL);
            Controls.Add(btnSave);
            Controls.Add(gbCloudFlare);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Config";
            Text = "SSL Renew Configurator";
            Load += Config_Load;
            gbCloudFlare.ResumeLayout(false);
            gbCloudFlare.PerformLayout();
            gbSSL.ResumeLayout(false);
            gbSSL.PerformLayout();
            gbNotification.ResumeLayout(false);
            gbNotification.PerformLayout();
            gbHmail.ResumeLayout(false);
            gbHmail.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbCloudFlare;
        private Button btnSave;
        private Label lblCloudFlareApiKey;
        private TextBox tbCloudFlareApiKey;
        private TextBox tbCloudFlareEmail;
        private Label lblCloudFlareEmail;
        private TextBox tbCloudFlareDomain;
        private Label lblCloudFlareDomain;
        private TextBox tbCloudFlareDNSRecord;
        private Label lblCloudFlareDNSRecord;
        private TextBox tbCloudFlareRecordType;
        private Label lblCloudFlareType;
        private Label lblCloudFlareTTL;
        private GroupBox gbSSL;
        private TextBox tbSSLPath;
        private Label lblSSLPath;
        private Label lblDBPort;
        private TextBox tbSSLState;
        private Label lblSSLState;
        private TextBox tbSSLLocality;
        private Label lblSSLLocality;
        private TextBox tbSSLOrganization;
        private Label lblSSLOrganization;
        private TextBox tbSSLOU;
        private Label lblSSLOU;
        private TextBox tbSSLCommonName;
        private Label lblSSLCommonName;
        private GroupBox gbNotification;
        private Label lblNotify;
        private CheckBox chkNotify;
        private TextBox tbSMTPServer;
        private Label lblSMTPServer;
        private TextBox tbSMTPPort;
        private Label lblSMTPPort;
        private TextBox tbSMTPUser;
        private Label lblSMTPUser;
        private TextBox tbSMTPPassword;
        private Label lblSMTPPassword;
        private TextBox tbToEmail;
        private Label lblToEmail;
        private GroupBox gbHmail;
        private TextBox tbHmailPassword;
        private Label lblHmailPassword;
        private TextBox tbHmailUser;
        private Label lblHmailUser;
        private Label lblSpacer;
        private Label lblTTLDesc;
        private ComboBox cmbCloudFlareTTL;
        private CheckBox chkCloudFlareApi;
        private CheckBox chkNotifyPassword;
        private CheckBox chkHmailPassword;
        private Label lblEncrypt;
        private CheckBox chkEncryptConfig;
        private ComboBox cmbSSLCountry;
        private Label lblSSLCountryDesc;
        private Button btnTestHmail;
        private Button btnTestEmail;
        private Button btnTestCloudFlate;
        private Label label1;
    }
}