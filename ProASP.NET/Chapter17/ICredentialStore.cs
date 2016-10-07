using System;
using System.Web.Security;

namespace Chapter15
{
	public interface ICredentialStore
	{
		bool Authenticate(string userName, string password);
		bool Authenticate(string userName, string password, out string userData);
		string[] GetRoles(string userName);
	}

	public class DefaultCredentialStore : ICredentialStore
	{
		public bool Authenticate(string userName, string password)
		{
			return FormsAuthentication.Authenticate(userName, password);			
		}

		public bool Authenticate(string userName, string password, out string userData)
		{
			userData = null;

			// Pass the call on to the other version of the 
			// Authenticate() method.
			return Authenticate(userName, password);
		}

		public string[] GetRoles(string userName)
		{
			return new string[]{};
		}
	}

}
