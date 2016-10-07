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
	/// Summary description for ImpersonationTest.
	/// </summary>
	public class ImpersonationTest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (User is WindowsPrincipal)
			{
				DisplayIdentity();

				// Impersonate the IIS identity.
				WindowsIdentity id;
				id = (WindowsIdentity)User.Identity;
				WindowsImpersonationContext impersonateContext;
				impersonateContext = id.Impersonate();
				DisplayIdentity();

				// Revert to the original ID as shown below.
				impersonateContext.Undo();
				DisplayIdentity();
			}
			else
			{
				// User isn't Windows authenticated.
				// Throw an error to or take other steps.
			}
		}

		private void DisplayIdentity()
		{
			// Get the identity under which the code is currently executing.
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			lblInfo.Text += "Executing as: " + identity.Name + "<br>";
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
