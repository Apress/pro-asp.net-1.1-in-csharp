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
using System.Net;
using Chapter26_WebClient.SecureWebService;

namespace SecureServiceTest
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
		protected System.Web.UI.WebControls.Button cmdAuthenticated;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.TextBox txtUserName;
		protected System.Web.UI.WebControls.Button cmdUnauthenticated;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.cmdAuthenticated.Click += new System.EventHandler(this.cmdAuthenticated_Click);
			this.cmdUnauthenticated.Click += new System.EventHandler(this.cmdUnauthenticated_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdUnauthenticated_Click(object sender, System.EventArgs e)
		{
			SecureService securedService = new SecureService();
			lblInfo.Text = securedService.TestAuthenticated();

		}

		private void cmdAuthenticated_Click(object sender, System.EventArgs e)
		{
			SecureService securedService = new SecureService();

			// Specity some user credentials for the web service.
			NetworkCredential credentials = new NetworkCredential(
				txtUserName.Text, txtPassword.Text);
			securedService.Credentials = credentials;

			lblInfo.Text = securedService.TestAuthenticated();

		}
	}
}
