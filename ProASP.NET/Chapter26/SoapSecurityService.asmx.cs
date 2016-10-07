using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;

namespace Chapter26
{
	/// <summary>
	/// Summary description for SoapSecurityService.
	/// </summary>
	public class SoapSecurityService : System.Web.Services.WebService
	{
		private string connectionString = "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=SSPI";

		public TicketHeader Ticket;
		public SoapSecurityService()
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

		public enum HashAlgorithm
		{
			SHA1, MD5, Clear
		}

		[WebMethod()]
		[SoapHeader("Ticket", Direction = SoapHeaderDirection.Out)]
		public void Login(string userName, string password, HashAlgorithm hashAlgorithm)
		{
			if (Authenticate(userName, password, hashAlgorithm))
			{
				// Get the user roles.
				string[] roles = GetRoles(userName);
				
				// Create a new ticket.
				TicketIdentity ticket = new TicketIdentity(userName, roles);

				// Add this ticket to Application state.
				Application[ticket.Ticket] = ticket;

				// Create the SOAP header.
				Ticket = new TicketHeader(ticket.Ticket);
  			}
			else
			{
				throw new SecurityException("Invalid credentials.");
			}
		}

		[WebMethod(Description="Returns the full list of employees.")]
		[SoapHeader("Ticket", Direction = SoapHeaderDirection.In)]
		public DataSet GetEmployees()
		{
			AuthorizeUser(Ticket.Ticket, "Administrator");

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

		private TicketIdentity AuthorizeUser(string ticket)
		{
			TicketIdentity ticketIdentity = (TicketIdentity)Application[ticket];
			if (ticket != null)
			{
				return ticketIdentity;
			}
			else
			{
				throw new SecurityException("Invalid ticket.");
			}
		}

		private TicketIdentity AuthorizeUser(string ticket, string role)
		{
			TicketIdentity ticketIdentity = AuthorizeUser(ticket);
			if (Array.IndexOf(ticketIdentity.Roles, role) == -1)
			{
				throw new SecurityException("Insufficient permissions.");
			}
			else
			{
				return ticketIdentity;
			}
		}



		string connectionSetting = "CredentialConnectionString";
		public bool Authenticate(string userName, string password, HashAlgorithm hashAlgorithm)
		{
			string passwordToCompare;
			if (hashAlgorithm != HashAlgorithm.Clear)
			{
				string hash = "";
				if (hashAlgorithm == HashAlgorithm.MD5) hash = "MD5";
				if (hashAlgorithm == HashAlgorithm.SHA1) hash = "SHA1";

				// If a hashing algorith is specified, hash the password.
				passwordToCompare = 
					FormsAuthentication.HashPasswordForStoringInConfigFile(
					password, hash);
			}
			else
			{
				// Otherwise, use the plain text of the password.
				passwordToCompare = password;
			}
			
			// Retrieve the connection string from the configuration file.				
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings[connectionSetting]);

			// Create a parameterized command to prevent SQL injection attacks.
			string query = "SELECT UserName, Password, EmailAddress FROM Users WHERE UserName = @UserName";
			SqlCommand cmd = new SqlCommand(query, con);
			cmd.Parameters.Add("@UserName", userName);
			
			bool isAuthenticated = false;
			try
			{
				con.Open();
				
				// The assumption here is that user names must be unique, which should be
				// enforced by an index in the database.
				SqlDataReader matchingUser = cmd.ExecuteReader(CommandBehavior.SingleRow);

				// Default behavior of SELECT is not case sensitive,
				// so it's up to you to perform a case sensitive comparison.
				if (matchingUser.Read())
				{
					if (((string)matchingUser["Password"] == passwordToCompare) &&
						((string)matchingUser["UserName"] == userName))
					{
						isAuthenticated = true;
					}
				}
				matchingUser.Close();
			}
			catch (Exception error)
			{
				return false;
			}
			finally
			{
				con.Close();
			}

			return isAuthenticated;
		}

		public string[] GetRoles(string userName)
		{
			// Retrieve the connection string from the configuration file.				
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings[connectionSetting]);

			// Create a parameterized command to prevent SQL injection attacks.
			string query = "SELECT Role FROM UsersRoles INNER JOIN Roles ON UsersRoles.RoleID = Roles.ID WHERE UserName = @UserName";
			SqlCommand cmd = new SqlCommand(query, con);
			cmd.Parameters.Add("@UserName", userName);
			
			ArrayList roles = new ArrayList();
			try
			{
				con.Open();
				
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					roles.Add((string)reader["Role"]);
				}
				reader.Close();
			}
			catch (Exception error)
			{
				// Don't return any roles.
				return new string[]{};
			}
			finally
			{
				con.Close();
			}

			return (string[])roles.ToArray(typeof(string));
		}


	}

	public class TicketIdentity
	{
		private string userName;
		public string UserName
		{
			get { return userName; }
		}

		private string[] roles;
		public string[] Roles
		{
			get { return roles; }
		}

		private string ticket;
		public string Ticket
		{
			get { return ticket; }
		}

		public TicketIdentity(string userName, string[] roles)
		{
			this.userName = userName;
			this.roles = roles;

			// Create the ticket GUID.
			this.ticket = Guid.NewGuid().ToString();
		}
	}

	public class TicketHeader : SoapHeader
	{
		public string Ticket;

			public TicketHeader(string ticket)
			{
				Ticket = ticket;
			}

		public TicketHeader()
		{}
	}


}
