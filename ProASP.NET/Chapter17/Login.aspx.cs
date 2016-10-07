using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace Chapter15
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Login : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button cmdLogin;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator2;
		protected System.Web.UI.WebControls.Label Label3;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.cmdLogin.Click += new System.EventHandler(this.cmdLogin_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdLogin_Click(object sender, System.EventArgs e)
		{
			// Check if the control values are valid.
			if (Page.IsValid)
			{
				ICredentialStore store = new DatabaseCredentialStore("CredentialConnectionString", "SHA1");
				if (store.Authenticate(txtName.Text, txtPassword.Text))
				{
					lblStatus.Text = "Logged in.";

					// Get the role information.
					string[] roles = store.GetRoles(txtName.Text);

					// Convert the roles to a single string,
					// so it can be attached to the cookie.
					string roleList = string.Join("%", roles);
				
					// Create a new authentication ticket.
					FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
						1,                           // Version
						txtName.Text,                // User name
						DateTime.Now,                // Date issued
						DateTime.Now.AddMinutes(30), // Date to expire
						false,                       // Persistent
						roleList);                   // User data string

					// Encrypt the ticket.
					string encryptedTicket = FormsAuthentication.Encrypt(ticket);

					// Create the authentication cookie.
					HttpCookie authenticationCookie  = new
						HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

					// Attach the cookie to the response.
					Response.Cookies.Add(authenticationCookie);

					// Redirect the user back to their original URL.
					Response.Redirect(FormsAuthentication.GetRedirectUrl(
						txtName.Text, false));
				}
				else
				{
					// Show an error message.
					lblStatus.Text = "Try again.";
				}
			}
		}
	}
}
