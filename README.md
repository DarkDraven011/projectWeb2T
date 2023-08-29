# projectWeb2T

Check on the internet:
	- Access the client page: https://103.17.140.160:7016/
(note, when the message Unsafe site appears, please select Advanced > Continue Visit)
	- Access admin page: https://103.17.140.160:7016/login.html
	- Access account: quantruongpham011@gmail.com
Password: blackswan011

Run the code locally:
	- Download Visual Studio
	- Download MSSQL 2022
	- Download the project's source code file
	- Go to Resource > Database > Backup Database, download the backup file of the database
	- Go to SQL Server Management Studio to create a new database to Restore Database, use the backup file to restore
	- Open the project with Visual Studio
	- In the file appsetting.json, change the Connection String as follows: "Server=YOUR SERVER NAME;Database=dbMarkets;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;" (Note: YOUR SERVER NAME is the name of the MSSQL server that you are using, if there is a "/" in the server name, change it to "//")
	- On the toolbar at Tools > NuGet Package Manager > Package Manager Console
	- Run command line: Scaffold-DbContext "Server=YOUR SERVER NAME;Database=dbMarkets;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force > Press Enter (Note: YOUR SERVER NAME is the server name of MSSQL and the server name that you entered earlier in the appsetting.json file, keep the "/" sign and DO NOT CHANGE it. to the sign "//" )
	- If there is no error, can run normally on local