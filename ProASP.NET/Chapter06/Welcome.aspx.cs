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
using System.Configuration;

namespace Chapter2
{
	/// <summary>
	/// Summary description for Welcome.
	/// </summary>
	public class Welcome : Page
	{
		protected Label lblSiteName;
		protected Label lblWelcome;
	
		private void Page_Load(object sender, EventArgs e)
		{
			// Put user code to initialize the page here
			lblSiteName.Text = ConfigurationSettings.AppSettings["websitename"];
			lblWelcome.Text = ConfigurationSettings.AppSettings["welcomemessage"];

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
