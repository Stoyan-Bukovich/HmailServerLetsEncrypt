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
            chkDNSApiSecret = new CheckBox();
            tbDNSApiSecret = new TextBox();
            lblDNSApiSecret = new Label();
            cmbDNSProvider = new ComboBox();
            lblDNSProvider = new Label();
            btnTestDNS = new Button();
            chkDNSApi = new CheckBox();
            cmbDNSTTL = new ComboBox();
            lblTTLDesc = new Label();
            lblDNSTTL = new Label();
            tbDNSRecordType = new TextBox();
            lblDNSType = new Label();
            tbDNSRecord = new TextBox();
            lblDNSRecord = new Label();
            tbDNSDomain = new TextBox();
            lblDNSDomain = new Label();
            tbDNSEmail = new TextBox();
            lblDNSEmail = new Label();
            tbDNSApiKey = new TextBox();
            lblDNSApiKey = new Label();
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
            gbCloudFlare.Controls.Add(chkDNSApiSecret);
            gbCloudFlare.Controls.Add(tbDNSApiSecret);
            gbCloudFlare.Controls.Add(lblDNSApiSecret);
            gbCloudFlare.Controls.Add(cmbDNSProvider);
            gbCloudFlare.Controls.Add(lblDNSProvider);
            gbCloudFlare.Controls.Add(btnTestDNS);
            gbCloudFlare.Controls.Add(chkDNSApi);
            gbCloudFlare.Controls.Add(cmbDNSTTL);
            gbCloudFlare.Controls.Add(lblTTLDesc);
            gbCloudFlare.Controls.Add(lblDNSTTL);
            gbCloudFlare.Controls.Add(tbDNSRecordType);
            gbCloudFlare.Controls.Add(lblDNSType);
            gbCloudFlare.Controls.Add(tbDNSRecord);
            gbCloudFlare.Controls.Add(lblDNSRecord);
            gbCloudFlare.Controls.Add(tbDNSDomain);
            gbCloudFlare.Controls.Add(lblDNSDomain);
            gbCloudFlare.Controls.Add(tbDNSEmail);
            gbCloudFlare.Controls.Add(lblDNSEmail);
            gbCloudFlare.Controls.Add(tbDNSApiKey);
            gbCloudFlare.Controls.Add(lblDNSApiKey);
            gbCloudFlare.Location = new Point(12, 12);
            gbCloudFlare.Name = "gbCloudFlare";
            gbCloudFlare.Size = new Size(500, 369);
            gbCloudFlare.TabIndex = 0;
            gbCloudFlare.TabStop = false;
            gbCloudFlare.Text = "DNS API";
            // 
            // chkDNSApiSecret
            // 
            chkDNSApiSecret.AutoSize = true;
            chkDNSApiSecret.Location = new Point(475, 106);
            chkDNSApiSecret.Name = "chkDNSApiSecret";
            chkDNSApiSecret.Size = new Size(15, 14);
            chkDNSApiSecret.TabIndex = 21;
            chkDNSApiSecret.UseVisualStyleBackColor = true;
            chkDNSApiSecret.CheckedChanged += chkDNSApiSecret_CheckedChanged;
            // 
            // tbDNSApiSecret
            // 
            tbDNSApiSecret.Location = new Point(90, 102);
            tbDNSApiSecret.MaxLength = 1000;
            tbDNSApiSecret.Name = "tbDNSApiSecret";
            tbDNSApiSecret.Size = new Size(379, 23);
            tbDNSApiSecret.TabIndex = 20;
            tbDNSApiSecret.UseSystemPasswordChar = true;
            // 
            // lblDNSApiSecret
            // 
            lblDNSApiSecret.AutoSize = true;
            lblDNSApiSecret.Location = new Point(6, 105);
            lblDNSApiSecret.Name = "lblDNSApiSecret";
            lblDNSApiSecret.Size = new Size(70, 15);
            lblDNSApiSecret.TabIndex = 19;
            lblDNSApiSecret.Text = "API secret: *";
            // 
            // cmbDNSProvider
            // 
            cmbDNSProvider.FormattingEnabled = true;
            cmbDNSProvider.Location = new Point(90, 26);
            cmbDNSProvider.Name = "cmbDNSProvider";
            cmbDNSProvider.Size = new Size(206, 23);
            cmbDNSProvider.TabIndex = 1;
            cmbDNSProvider.SelectedIndexChanged += cmbDNSProvider_SelectedIndexChanged;
            // 
            // lblDNSProvider
            // 
            lblDNSProvider.AutoSize = true;
            lblDNSProvider.Location = new Point(6, 29);
            lblDNSProvider.Name = "lblDNSProvider";
            lblDNSProvider.Size = new Size(62, 15);
            lblDNSProvider.TabIndex = 18;
            lblDNSProvider.Text = "Provider: *";
            // 
            // btnTestDNS
            // 
            btnTestDNS.BackColor = SystemColors.Control;
            btnTestDNS.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnTestDNS.ForeColor = Color.Red;
            btnTestDNS.Location = new Point(205, 330);
            btnTestDNS.Name = "btnTestDNS";
            btnTestDNS.Size = new Size(91, 27);
            btnTestDNS.TabIndex = 8;
            btnTestDNS.Text = "Test";
            btnTestDNS.UseVisualStyleBackColor = false;
            btnTestDNS.Click += btnTestDNS_Click;
            // 
            // chkDNSApi
            // 
            chkDNSApi.AutoSize = true;
            chkDNSApi.Location = new Point(475, 67);
            chkDNSApi.Name = "chkDNSApi";
            chkDNSApi.Size = new Size(15, 14);
            chkDNSApi.TabIndex = 2;
            chkDNSApi.UseVisualStyleBackColor = true;
            chkDNSApi.CheckedChanged += chkCloudFlareApi_CheckedChanged;
            // 
            // cmbDNSTTL
            // 
            cmbDNSTTL.FormattingEnabled = true;
            cmbDNSTTL.Location = new Point(90, 292);
            cmbDNSTTL.Name = "cmbDNSTTL";
            cmbDNSTTL.Size = new Size(121, 23);
            cmbDNSTTL.TabIndex = 7;
            // 
            // lblTTLDesc
            // 
            lblTTLDesc.AutoSize = true;
            lblTTLDesc.Location = new Point(225, 295);
            lblTTLDesc.Name = "lblTTLDesc";
            lblTTLDesc.Size = new Size(71, 15);
            lblTTLDesc.TabIndex = 16;
            lblTTLDesc.Text = "(in seconds)";
            // 
            // lblDNSTTL
            // 
            lblDNSTTL.AutoSize = true;
            lblDNSTTL.Location = new Point(6, 295);
            lblDNSTTL.Name = "lblDNSTTL";
            lblDNSTTL.Size = new Size(36, 15);
            lblDNSTTL.TabIndex = 10;
            lblDNSTTL.Text = "TTL: *";
            // 
            // tbDNSRecordType
            // 
            tbDNSRecordType.Location = new Point(90, 254);
            tbDNSRecordType.MaxLength = 4;
            tbDNSRecordType.Name = "tbDNSRecordType";
            tbDNSRecordType.ReadOnly = true;
            tbDNSRecordType.Size = new Size(121, 23);
            tbDNSRecordType.TabIndex = 6;
            tbDNSRecordType.Text = "TXT";
            // 
            // lblDNSType
            // 
            lblDNSType.AutoSize = true;
            lblDNSType.Location = new Point(6, 257);
            lblDNSType.Name = "lblDNSType";
            lblDNSType.Size = new Size(81, 15);
            lblDNSType.TabIndex = 8;
            lblDNSType.Text = "Record type: *";
            // 
            // tbDNSRecord
            // 
            tbDNSRecord.CharacterCasing = CharacterCasing.Lower;
            tbDNSRecord.Location = new Point(90, 215);
            tbDNSRecord.MaxLength = 1000;
            tbDNSRecord.Name = "tbDNSRecord";
            tbDNSRecord.Size = new Size(209, 23);
            tbDNSRecord.TabIndex = 5;
            tbDNSRecord.Text = "_acme-challenge.mail";
            // 
            // lblDNSRecord
            // 
            lblDNSRecord.AutoSize = true;
            lblDNSRecord.Location = new Point(6, 218);
            lblDNSRecord.Name = "lblDNSRecord";
            lblDNSRecord.Size = new Size(78, 15);
            lblDNSRecord.TabIndex = 6;
            lblDNSRecord.Text = "DNS record: *";
            // 
            // tbDNSDomain
            // 
            tbDNSDomain.CharacterCasing = CharacterCasing.Lower;
            tbDNSDomain.Location = new Point(90, 177);
            tbDNSDomain.MaxLength = 1000;
            tbDNSDomain.Name = "tbDNSDomain";
            tbDNSDomain.Size = new Size(400, 23);
            tbDNSDomain.TabIndex = 4;
            // 
            // lblDNSDomain
            // 
            lblDNSDomain.AutoSize = true;
            lblDNSDomain.Location = new Point(6, 180);
            lblDNSDomain.Name = "lblDNSDomain";
            lblDNSDomain.Size = new Size(60, 15);
            lblDNSDomain.TabIndex = 4;
            lblDNSDomain.Text = "Domain: *";
            // 
            // tbDNSEmail
            // 
            tbDNSEmail.CharacterCasing = CharacterCasing.Lower;
            tbDNSEmail.Location = new Point(90, 139);
            tbDNSEmail.MaxLength = 1000;
            tbDNSEmail.Name = "tbDNSEmail";
            tbDNSEmail.Size = new Size(400, 23);
            tbDNSEmail.TabIndex = 3;
            // 
            // lblDNSEmail
            // 
            lblDNSEmail.AutoSize = true;
            lblDNSEmail.Location = new Point(6, 142);
            lblDNSEmail.Name = "lblDNSEmail";
            lblDNSEmail.Size = new Size(47, 15);
            lblDNSEmail.TabIndex = 2;
            lblDNSEmail.Text = "Email: *";
            // 
            // tbDNSApiKey
            // 
            tbDNSApiKey.Location = new Point(90, 63);
            tbDNSApiKey.MaxLength = 1000;
            tbDNSApiKey.Name = "tbDNSApiKey";
            tbDNSApiKey.Size = new Size(379, 23);
            tbDNSApiKey.TabIndex = 1;
            tbDNSApiKey.UseSystemPasswordChar = true;
            // 
            // lblDNSApiKey
            // 
            lblDNSApiKey.AutoSize = true;
            lblDNSApiKey.Location = new Point(6, 66);
            lblDNSApiKey.Name = "lblDNSApiKey";
            lblDNSApiKey.Size = new Size(57, 15);
            lblDNSApiKey.TabIndex = 0;
            lblDNSApiKey.Text = "API key: *";
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.Location = new Point(358, 1135);
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
            gbSSL.Location = new Point(13, 394);
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
            gbNotification.Location = new Point(18, 694);
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
            gbHmail.Location = new Point(18, 985);
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
            lblSpacer.Location = new Point(246, 1180);
            lblSpacer.Name = "lblSpacer";
            lblSpacer.Size = new Size(0, 15);
            lblSpacer.TabIndex = 27;
            // 
            // lblEncrypt
            // 
            lblEncrypt.AutoSize = true;
            lblEncrypt.Location = new Point(24, 1149);
            lblEncrypt.Name = "lblEncrypt";
            lblEncrypt.Size = new Size(144, 15);
            lblEncrypt.TabIndex = 27;
            lblEncrypt.Text = "Encrypt configuration file:";
            // 
            // chkEncryptConfig
            // 
            chkEncryptConfig.AutoSize = true;
            chkEncryptConfig.Location = new Point(174, 1150);
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
        private Label lblDNSApiKey;
        private TextBox tbDNSApiKey;
        private TextBox tbDNSEmail;
        private Label lblDNSEmail;
        private TextBox tbDNSDomain;
        private Label lblDNSDomain;
        private TextBox tbDNSRecord;
        private Label lblDNSRecord;
        private TextBox tbDNSRecordType;
        private Label lblDNSType;
        private Label lblDNSTTL;
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
        private ComboBox cmbDNSTTL;
        private CheckBox chkDNSApi;
        private CheckBox chkNotifyPassword;
        private CheckBox chkHmailPassword;
        private Label lblEncrypt;
        private CheckBox chkEncryptConfig;
        private ComboBox cmbSSLCountry;
        private Label lblSSLCountryDesc;
        private Button btnTestHmail;
        private Button btnTestEmail;
        private Button btnTestDNS;
        private Label label1;
        private ComboBox cmbDNSProvider;
        private Label lblDNSProvider;
        private CheckBox chkDNSApiSecret;
        private TextBox tbDNSApiSecret;
        private Label lblDNSApiSecret;
    }
}