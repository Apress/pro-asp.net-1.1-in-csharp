using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Security.Cryptography;
using System.IO;
using System.Security.Principal;
using System.Web.Security;

namespace Chapter18 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			
			// Retrieve the key.
			FileStream fs = File.OpenRead(Server.MapPath("key.config"));
			byte[] key = new byte[32];
			byte[] IV = new byte[16];
			fs.Read(key, 0, key.Length);
			fs.Read(IV, 0, IV.Length);
			fs.Close();

			// Create a new encryption class with this key.
			Rijndael crypt = Rijndael.Create();
			crypt.Padding = PaddingMode.Zeros;
			crypt.Key = key;
			crypt.IV = IV;

			// Store the key information in Application state.
			Application["Key"] = crypt;
		


			// Track logged in users.
			Application["CurrentUsers"] = new Hashtable();
			Application["AnonymousUsers"] = new Counter();
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{
            Counter anonymous = (Counter)Application["AnonymousUsers"];
			lock (anonymous)
			{
				anonymous.Count++;
			}
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			// Check that the request has been authenticated.
			if(Request.IsAuthenticated)
			{
				if (Context.User.Identity is FormsIdentity)
				{
					// Get the email address from the ticket.
					string email = ((FormsIdentity)Context.User.Identity).Ticket.UserData;

					// Create a new identity with this email information.
					CustomIdentity identity = new CustomIdentity(Context.User.Identity.Name, email);

					// Create and attach a new principal (with no roles).
					GenericPrincipal newPrincipal = 
						new GenericPrincipal(identity, new string[]{});
					Context.User = newPrincipal;
				}
			} 
		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{
			Hashtable users = (Hashtable)Application["CurrentUsers"];
			Counter anonymous = (Counter)Application["AnonymousUsers"];

			if (users.Contains(Session.SessionID))
			{
				lock (users)
				{
					users.Remove(Session.SessionID);    
				}
			}
			else
			{
				lock (anonymous)
				{
					anonymous.Count--;
				}
			}
		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

