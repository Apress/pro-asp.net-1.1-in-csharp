using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography;

namespace Chapter18
{
	/// <summary>
	/// Summary description for EncryptedQueryString.
	/// </summary>
	public class EncryptedQueryString: System.Collections.Specialized.StringDictionary
	{
		SymmetricAlgorithm crypt;
		public EncryptedQueryString(SymmetricAlgorithm crypt)
		{
			this.crypt = crypt;
		}

		public EncryptedQueryString(SymmetricAlgorithm crypt, string encryptedString)
		{
			this.crypt = crypt;
			Deserialize(encryptedString);
		}

		public override string ToString()
		{
			// Build the query string.
			// The name and value are separated with the = character.
			// Each subsequent name/value pair is separated with the & character.
			// To ensure that the setting value doesn't already use
			// the & or = characters (which would then be mistaken for a delimiter)
			// this code encodes the name and value before putting
			// it into the string.
			HttpServerUtility server = HttpContext.Current.Server;
			StringBuilder sb = new StringBuilder();
			foreach (DictionaryEntry item in this) 
			{
				sb.Append(server.UrlEncode(item.Key.ToString()));
				sb.Append("=");
				sb.Append(server.UrlEncode(item.Value.ToString()));
				sb.Append("&");
			}

			// Remove the last &.
			sb.Remove(sb.Length-1, 1);

			// Perform the encryption.
			byte[] encryptedData = EncryptionUtil.EncryptString(sb.ToString(), crypt);
			
			// Convert the encrypted byte array to a URL-legal string.
			// This would also be a good place to check that the data isn't too large
			// to fit in a typical 4 KB query string.
			return HexEncoding.ToString(encryptedData);
		}

		private void Deserialize(string encryptedString)
		{
			HttpServerUtility server = HttpContext.Current.Server;
			
			// Decode the string back into a byte array.
			byte[] encryptedData = HexEncoding.GetBytes(encryptedString);

			// Decrypt the string.
			string decryptedString = EncryptionUtil.DecryptToString(encryptedData, crypt);

			// Split the string into values.
			string[] values = decryptedString.Split('&');
			foreach (string val in values)
			{
				string[] nameValuePair = val.Split('=');
				base.Add(server.UrlDecode(nameValuePair[0]), server.UrlDecode(nameValuePair[1]));
			}
		}
	}



	
}

