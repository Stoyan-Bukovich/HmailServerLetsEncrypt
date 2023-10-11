# HMail Server, Let's Encrypt and CloudFlare Auto-Updater

Automate the renewal of SSL certificates for your HMailServer with Let's Encrypt and CloudFlare. This tool is designed to effortlessly monitor SSL certificate expiration and, if necessary, request, install, and apply new SSL certificates to your HMailServer instances.


**!!! Important consideration: Please note that this tool only renews a single SSL certificate and applies it to all SSL-enabled ports. It does not support multiple certificates !!!**


**Key Features:**
* GUI configuration file generator, validator and editor.
* Configuration file encryption.
* Support of all database types supported by HmailServer.

**Prerequisites:**

This tool was developed and tested with the following components:

* .NET Core 7.0



**NuGet packages:**
* Certes (Version 3.0.4)
* Newtonsoft.Json (Version 13.0.3)
* System.ServiceProcess.ServiceController (Version 7.0.1)
* MailKit (Version 4.2.0)



**Configuration file notes:**

```
{
	"Domains": [
		{
			"APIKey": "", - Request CloudFlare API key and set it in this field. Example: z2fd956c1a7a44606e8c6d654f7d6d3eb420
			"Email": "",  - Set your CloudFlare email address in this field. Example: myemail@gmail.com
			"Domain_Name": "", - Set the domain name which would hold the _acme-challenge TXT record. Example: mydomain.com
			"DNS_Record": "",  - Set the TXT record name which would hold the LetsEncrypt verification value. Example: _acme-challenge.mail
			"Type": "", - Set the DNS record type to be update in CloudFlare. This field always needs to be set to: TXT
			"TTL": "", - Set Time-to-Live of the DNS TXT record. Example: 120, consider low time value like 1 minute
			"SSLPath": "", - Set physical path where the SSL certificate (*.crt) and key file (*.key) would be saved. Example: C:\\HmailSSL\\ Please note here that the \ in path needs to be json escaped \\
			"CountryName": "", - This is SSL certificate information. Set two letters country name. Example: US or UK etc.
			"State": "", - This is SSL certificate information. Set state name. Example: California
			"Locality": "", - This is SSL certificate information. Set city name. Example: Bakersfield
			"Organization": "", - This is SSL certificate information. Set your organization name. Example: My Company Name
			"OU": "", - This is SSL certificate information. Set your organizational unit. Example: Headquaters
			"CommonName": "", - This is SSL certificate domain and at the same time your email server PTR DNS record and notification email Subject line. Choose wisely!
			"LetsEncryptPemAccount": "", - This field value must not be edited manually, it holds LetsEncrypt account information after initial registration.
			"SMTPSendNotification": "true" - Set true if you want to receive email notification regarding the status of the SSL updates or false if you do not want any email notifications.
			"SMTPServer": "", - This is IP address or domain name of your outgoing notification SMTP server. Example: smtp.gmail.com Please, DO NOT put your HmailServer instance here in case something fails :)
			"SMTPPort": "", - Set your SMTP notification server port number. Example: 465
			"SMTPUser": "", - Set your SMTP notification server user name. Example: myemail@gmail.com
			"SMTPPassword": "", - Set your SMTP notification server password.
			"SMTPTo": "" - Set an email address where you would like to receive the SSL status notifications. Kind reminder DO NOT put an email addres hold by your HmailServer instance here in case something fails :)
			"hMailUser": "" - Holds the HmailServer administraror's user name. Required for COM communication with HmailServer.
			"hMailPassword": "" - Holds the HmailServer administrator's password.
			"EncryptConfig": "true" - Set true if you need this configuration file values encrypted with default encryption method (AES) or false to leave all values in plain text.
		}
	]
}
```

**Deployment:**
* Open the solution with Visual Studio and compile both projects (Configure the GUI configuration manager and the HmailServerLetsEncrypt the mail executable).
* Run the Configure.exe file in order to create initial empty configuration file (config.dat). Use the GUI to fill out all required settings and save the changes.
* Create a Windows Task in Task Scheduler to run the tool at your desired frequency (e.g., every hour).
* By following these steps, you'll ensure that your HMailServer SSL certificates are automatically updated and maintained hassle-free.

**Note**
If you prefer to avoid the hassle of compiling, you can download the official executables from the Release section.