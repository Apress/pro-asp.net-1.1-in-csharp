using System;
using System.Security.Principal;

namespace Chapter18
{
	/// <summary>
	/// Summary description for CustomIdentity.
	/// </summary>
	public class CustomIdentity : IIdentity
	{
		public string AuthenticationType
		{
			get
			{
				return "CustomAuthenticationType";
			}
		}

		string name;
		public string Name
		{
			get
			{
				return name;
			}
		}

		string email;
		public string EmailAddress
		{
			get
			{
				return email;
			}
		}

		public bool IsAuthenticated
		{
			get
			{
				return true;
			}
		}

		public CustomIdentity(string name, string emailAddress)
		{
			this.name = name;
			this.email = emailAddress;
		}

	}
}
