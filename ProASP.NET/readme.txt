
-------------------
Virtual Directories
-------------------

The code for each chapter is contained in a separate project. To open the project in Visual Studio .NET, you must create a virtual directory that has the same name as the project directory. For example, Chapter01 should become the virtual directory http://localhost/Chapter01. You can do this using the Virtual Directory Creation Wizard in IIS Manager (as described in Chapter 2).

There's also an alternate approach that provides a shortcut for making virtual directories. Right-click the directory in Windows Explorer, choose the Web Sharing tab, click "Share this folder", and then click OK twice. The only disadvantage with this aproach is that the new virtual directory won't have anonymous access enabled. To correct this, you must run IIS Manager, right-click the virtual directory, choose Properties, choose the Directory Security tab, and click Edit. Enable the check box next to "Anonymous access" and click OK twice.

Finally, you can always move the project to a new virtual directory. However, in this case you need to modify the .csproj.webinfo file (using Notepad) to refer to the new virtual directory. Otherwise, you won't be able to open the project in Visual Studio .NET.


---------------------------------------
Running the Pages in Visual Studio .NET
---------------------------------------

Note that every web project is made up of several example pages, which do not always relate directly. To run a page from Visual Studio .NET, right click on it and set it to be the startup page. Then start the application. You can also start the application, and then type different .aspx file names into the browser.

These examples use the Visual Studio .NET 2003 project format. You will not be able to open them with an earlier version of Visual Studio .NET. However, you can add the existing pages, web services, and code files to an existing Visual Studio .NET 2002 project.

NOTE: There are no project files for the simple examples in Chapter01 and Chapter02. Similarly, Chapter03 provides Visual Studio .NET macros but no web files.


---------------
Custom Controls
---------------

If you want the custom controls developed in Part IV to be shown on your Toolbox, you will need to add them as described in the book. Visual Studio .NET stores global preference information for the Toolbox--it is not stored in individual projects or solutions.


----------------
Database Scripts
----------------

Some of the database examples use custom SQL Server databases (others use the standard Northwind and pub databases that are always installed by default). To use an example that requires a custom database, you must install the database first use the included database script. Following is a list of all the scripts and what chapters require them.

EmployeesProcedures.sql
 Adds tables and stored procedures to the Northwind database.
 Used in:
   * Chapter 8 - Stored procedures for inserting/deleting to Employees table.
   * Chapter 9 - The Employees2 table and stored procedures for updating Employees2.
   * Chapter 10 - The GetEmployeesByPage stored procedure for paging.

UserCredentials.sql
 To use this script, you must first create a new database named UserCredentials.
 Run this script in the UserCredentials database to add the tables and data.
 Used in:
   * Chapter 15 - The Users table for database-based forms authentication.
   * Chapter 17 - The Roles and UsersRoles tables for role-based security.
   * Chapter 18 - The CreditCardData field in the Users table (for encrypting data).




SUPPORT INFORMATION
===================

Troubleshooting
---------------

Modifying database connection strings

All samples use a default database connection string to connect to 
(localhost) using Windows authentication (SSPI). If your SQL Server 
installation is not configured to use Windows authentication, you will need 
to modify the connection string in the source code for your login information.


Samples do not have permission to access file system

Some samples require access to the file system. Note that by default, ASP.NET 
does not run applications within the context of the local system account. This 
means that samples that access the file system, for example, will explicitly 
need to be granted read/write permissions. To enable access:

1) Using Windows Explorer, right-click on the application directory and 
   choose the Security tab.

2) Click Add and select the ASP.NET account (for example, ASPNET).

3) Give the ASPNET user account Write and Modify permissions.

4) Click OK.


Samples do not have permission to event log

Samples that modify the event log will only work if they execute under an 
account that has permission to modify the event log. For example, the ASPNET 
user account does not have permission to modify the event log by default, and 
a SOAP extension attempting to write to the event log will throw a security 
exception.

To grant this permission to an account that lacks it, do the following:

1) Run regedt32.exe.

2) Open HKEY_Local_Machine\SYSTEM\CurrentControlSet\Services\EventLog.

3) Select the EventLog folder and from the mentu click the Security and 
   choose Permissions.
    
4) Add the account (for example, ASPNET) to the list then give it Full 
   Control.

