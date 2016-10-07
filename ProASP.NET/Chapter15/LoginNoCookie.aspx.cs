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
	public class LoginNoCookie : System.Web.UI.Page
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
				if (FormsAuthentication.Authenticate(txtName.Text, txtPassword.Text))
				{
					lblStatus.Text = "Logged in.";
				
					// Create an authentication ticket.
					FormsAuthenticationTicket ticket = 
						new FormsAuthenticationTicket(txtName.Text, false, 30);

					// Encrypt it.
					string encryptedTicket = FormsAuthentication.Encrypt(ticket);
					
					// Create a string to hold the redirect URL.
					string destinationURL;

					// Get the original redirection URL.
					string originalURL = 
						FormsAuthentication.GetRedirectUrl(txtName.Text,false);

  
					// Check whether the original URL has query parameters/
					if (originalURL.IndexOf("?") == -1)
					{
						// Add the encrypted authentication ticket as the only parameter.
						destinationURL = originalURL + "?" +
							FormsAuthentication.FormsCookieName + "=" + encryptedTicket;
					}
					else
					{
						// Add the encrypted authentication ticket as an additional parameter.
						destinationURL = originalURL + "&" +
							FormsAuthentication.FormsCookieName + "=" + encryptedTicket;
					}
					Response.Redirect(destinationURL);
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
