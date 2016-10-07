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
using System.Security.Principal;

namespace Chapter16
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.IsAuthenticated) 
			{
				// Display generic identity information.
				lblInfo.Text = "<b>Name: </b>" + User.Identity.Name;
				lblInfo.Text += "<br><b>Authenticated With: </b>";
				lblInfo.Text += User.Identity.AuthenticationType;

				WindowsPrincipal principal = (WindowsPrincipal)User;
				lblInfo.Text += "<br><b>Power user? </b>";
				lblInfo.Text += principal.IsInRole(
					WindowsBuiltInRole.PowerUser).ToString();

				WindowsIdentity identity = (WindowsIdentity)User.Identity;
				lblInfo.Text += "<br><b>Token: </b>";
				lblInfo.Text +=	identity.Token.ToString();
				lblInfo.Text += "<br><b>Guest? </b>";
				lblInfo.Text +=	identity.IsGuest.ToString();
				lblInfo.Text += "<br><b>System? </b>";
				lblInfo.Text +=	identity.IsSystem.ToString();


				
			}

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
