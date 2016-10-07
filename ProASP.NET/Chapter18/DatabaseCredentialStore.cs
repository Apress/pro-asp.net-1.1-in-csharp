using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Collections;
using System.Security.Cryptography;
using System.Web;
using System.IO;

namespace Chapter18
{
	/// <summary>
	/// Summary description for DatabaseCredentialStore.
	/// </summary>
	public class DatabaseCredentialStore : ICredentialStore
	{
		string connectionSetting;
		string hashingAlgorithm;

		public DatabaseCredentialStore(string connectionStringSettingName, string hashingAlgorithm)
		{
			this.connectionSetting = connectionStringSettingName;
			this.hashingAlgorithm = hashingAlgorithm;
		}
    
		public bool Authenticate(string userName, string password)
		{
			// Pass the call on to the other version of the 
			// Authenticate() method.
			string ignoredUserData;
			return Authenticate(userName, password, out ignoredUserData);
		}


		public bool Authenticate(string userName, string password, out string userData)
		{
			userData = null;

			string passwordToCompare;
			if ((hashingAlgorithm != null) && (hashingAlgorithm != "Clear"))
			{
				// If a hashing algorith is specified, hash the password.
				passwordToCompare = 
					FormsAuthentication.HashPasswordForStoringInConfigFile(
					password, hashingAlgorithm);
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
						userData = (string)matchingUser["emailAddress"]; 
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


	
}
