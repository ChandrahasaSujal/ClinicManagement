# Introduction 
Main agenda of this project is to maintain clinic those includes appointmnets creation, Medicines Stock Management, Invoices creation for purchased medicines.

# Getting Started

1.	Installation process:

    clone:  https://github.com/chanducodemonk/ClinicManagement.git
    
    Update Config file as per your machine.
    
    syntax:  ```<add name="keyName" connectionString="Data Source={{replace this with system name}};Initial Catalog={{replace this with                       Database name}};User Id = {{replace this with user ID}};Password={{replace this with your sql password }}"                                 providerName="System.Data.SqlClient" /> ```
    
    ex: >    ```<add name="DefaultConnection" connectionString="Data Source=CHANDU;Initial Catalog=CMTest3;User Id = sa;Password=test#123"                   providerName="System.Data.SqlClient" />```
   
2.	Software dependencies
                .NET Framework 4.8

4.	API references
               1. EPPlus for Excel Generation: https://www.nuget.org/packages/EPPlus/
               2. NLog for Logging Exceptions: https://nlog-project.org/
               3. Jquery DataTable: https://datatables.net/

# Build and Test
1. Since this Deployed in Local IIS you need to run Visual Studio as Adminstrator or change to change to IIS Express by right clicking    on CM.Web project and WEB > Server then change Local IIS to IIS Express click on Create Virtual Directory.

2. Open CM.Data Project and go to **Seed Class** and if you want to change Admin Credentials on first go or go with default.
   Right Click on Solution Explorer if you're opening in a visual studio and restore packages and build solution.

3. Enter the Admin credentials click on Login.


Feel Free to reach me here on https://www.linkedin.com/in/chandrahasa-sujal/
