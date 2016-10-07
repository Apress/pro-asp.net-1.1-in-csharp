using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Xml;
using System.Web.Services.Protocols;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using System.IO;

namespace Chapter26_WSE
{
	[WebService (Name="EmployeesService",
		 Description="Retrieve the Northwind Employees",
		 Namespace="http://www.apress.com/ProASP.NET/")]
	public class EmployeesService : System.Web.Services.WebService
	{
		public EmployeesService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion


		private string connectionString = "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=SSPI";
  		

		[WebMethod(Description="Returns the full list of employees.")]
		public DataSet GetEmployees()
		{
			// Create the command and the connection.
			string sql = "SELECT EmployeeID, LastName, FirstName, Title, " +
				"TitleOfCourtesy, HomePhone FROM Employees";
			SqlConnection con = new SqlConnection(connectionString);
			SqlDataAdapter da = new SqlDataAdapter(sql, con);
			DataSet ds = new DataSet();
        
			// Fill the DataSet.
			da.Fill(ds, "Employees");
			return ds;
		}

		private string GetUsernameToken()
		{
			// Although there may be many tokens, only one of these
			// will be a UsernameToken
			foreach (UsernameToken token in RequestSoapContext.Current.Security.Tokens)
			{
				return token.Username;
			}
			throw new System.Security.SecurityException("Missing security token");
		}



		[WebMethod()]
		public void UploadFile(string fileName)
		{
 
			if (RequestSoapContext.Current.Attachments.Count != 1)
			{
				throw new ArgumentException("Only upload one file at a time.");
			}
			else
			{
				// Ensure that the supplied file name doesn't include a path
				// (so the application can't be tricked into using anything
				// other than the designated directory.) 
				fileName = Path.GetFileName(fileName);

				// Note that there's still no guarantee this file is unique,
				// so you might want to use randomly generated data in the name
				// (like a GUID) or a user-specific directory.

				// Save the file using the name provided.
				string fullPath = HttpContext.Current.Server.MapPath(fileName);
				FileStream fs = File.Create(fullPath);
				
				Stream stream = RequestSoapContext.Current.Attachments[0].Stream;
				while (stream.Position < (stream.Length-1))
				{
					// Write one byte at a time.
					// You could also grab larger chunks to be more efficient.
					fs.WriteByte((byte)stream.ReadByte());
				}
				fs.Close();
			}
		}
	}



	public class CustomAuthenticator : UsernameTokenManager
	{
		// This method returns the password for the provided username
		// WSE will determine if they match
		protected override string AuthenticateToken(UsernameToken token)
		{
			string username = token.Username;

			// In real site, would query database or check XML file...
			if (username == "dan")
			{
				return "secret";
			}
			else if (username == "jenny")
			{
				return "opensesame";
			}
			else
			{
				return "";
			}
		}
	}
}
