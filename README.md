# HMail Server, Let's Encrypt and CloudFlare Auto-Updater

Automate the renewal of SSL certificates for your HMailServer with Let's Encrypt and CloudFlare. This tool is designed to effortlessly monitor SSL certificate expiration and, if necessary, request, install, and apply new SSL certificates to your HMailServer instances.

**Key Features:**
* Easily configure multiple HMailServers on the same network with distinct domain names and settings.
* Currently supports **MSSQL** and **MySQL** servers only.

**Prerequisites:**

This tool was developed and tested with the following components:

* .NET Core 7.0



**NuGet packages:**
* Certes (Version 3.0.4)
* Newtonsoft.Json (Version 13.0.3)
* System.Data.SqlClient (Version 4.8.5)
* System.ServiceProcess.ServiceController (Version 7.0.1)
* MySql.Data (Version 8.1.0)
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
			"DBType": "", - Set database type where your HmailServer stores its settings. Values can be mysql or mssql. Example: mysql
			"DBIpAddress": "", - Set IP address of the database server or hostname. Example: 192.168.1.100 or DBServer
			"DBPort": "", - Set database port number. For MSSQL default port number is 1433 and for MySQL the default port number is 3306
			"DBName": "", - Set database name which holds your HmailServer installation. Example: emails
			"DBUser": "", - Set database user name which has sufficient priviledges on the database. Conside that using sa or root is always bad idea.
			"DBPassword": "", - Set the password the the database user.
			"SkipPorts": "", - Here you can enumerate list of ports which the updater will ignore and not bind SSL certificate to them. Use the following format: 2525;2500;2050 You can set as many skip/ignore ports as you wish as far as you keep the ; separator between them.
			"SSLPath": "", - Set physical path where the SSL certificate (*.crt) and key file (*.key) would be saved. Example: C:\\HmailSSL\\ Please note here that the \ in path needs to be json escaped \\
			"CountryName": "", - This is SSL certificate information. Set two letters country name. Example: US or UK etc.
			"State": "", - This is SSL certificate information. Set state name. Example: California
			"Locality": "", - This is SSL certificate information. Set city name. Example: Bakersfield
			"Organization": "", - This is SSL certificate information. Set your organization name. Example: My Company Name
			"OU": "", - This is SSL certificate information. Set your organizational unit. Example: Headquaters
			"CommonName": "", - This is SSL certificate domain and at the same time your email server PTR DNS record and notification email Subject line. Choose wisely!
			"SMTPServer": "", - This is IP address or domain name of your outgoing notification SMTP server. Example: smtp.gmail.com Please, DO NOT put your HmailServer instance here in case something fails :)
			"SMTPPort": "", - Set your SMTP notification server port number. Example: 465
			"SMTPUser": "", - Set your SMTP notification server user name. Example: myemail@gmail.com
			"SMTPPassword": "", - Set your SMTP notification server password.
			"SMTPTo": "" - Set an email address where you would like to receive the SSL status notifications. Kind reminder DO NOT put an email addres hold by your HmailServer instance here in case something fails :)
		}
	]
}
```

**Deployment:**
* Compile **dotnet publish -c Release -p:PublishSingleFile=true --self-contained false -r win-x64**
* Configure the JSON file as described above.
* Create a Windows Task in Task Scheduler to run the tool at your desired frequency (e.g., every hour).
* By following these steps, you'll ensure that your HMailServer SSL certificates are automatically updated and maintained hassle-free.
