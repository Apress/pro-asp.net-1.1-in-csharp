using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Collections;
using System.Web;
using System.Security.Cryptography;

namespace Chapter18
{
	public class UsersDB
	{
		string connectionSetting;
		
		public UsersDB(string connectionStringSettingName)
		{
			this.connectionSetting = connectionStringSettingName;
		}
    
		public void SetCreditCard(string userName, string cardNumber)
		{
			byte[] encryptedData = EncryptionUtil.EncryptString(
				cardNumber,(Rijndael)HttpContext.Current.Application["Key"]);		
	
			// Retrieve the connection string from the configuration file.				
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings[connectionSetting]);
			
			// Create a parameterized command with placeholders.
			string SQL = "UPDATE Users SET CreditCardData = @CreditCardData " +
				"WHERE UserName = @UserName";
			SqlCommand cmd = new SqlCommand(SQL, con);

			// Add a @UserName parameter of "TestUser"
			cmd.Parameters.Add("@CreditCardData", encryptedData);
			cmd.Parameters.Add("@UserName", userName);

			try
			{
				// Update the record.
				con.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				con.Close();
			}
		}

		public string GetCreditCard(string userName)
		{
			// Retrieve the connection string from the configuration file.				
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings[connectionSetting]);
			
			// Create a parameterized command with placeholders.
			string SQL = "SELECT CreditCardData FROM Users " +
				"WHERE UserName = @UserName";
			SqlCommand cmd = new SqlCommand(SQL, con);
			cmd.Parameters.Add("@UserName", userName);

			byte[] encryptedData;
			try
			{
				// Update the record.
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
				reader.Read();
				encryptedData = (byte[])reader["CreditCardData"];
				reader.Close();
			}
			catch
			{
				return null;
			}
			finally
			{
				con.Close();
			}

			// Decrypt and return the credit card number.
			return EncryptionUtil.DecryptToString(
				encryptedData, (Rijndael)HttpContext.Current.Application["Key"]);
		}



	}
}
