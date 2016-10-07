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

namespace OutputCaching
{
	/// <summary>
	/// Summary description for QueryStringSender.
	/// </summary>
	public class QueryStringSender : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button cmdLarge;
		protected System.Web.UI.WebControls.Button cmdNormal;
		protected System.Web.UI.WebControls.Button cmdSmall;
	
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
			this.cmdLarge.Click += new System.EventHandler(this.cmd_Click);
			this.cmdNormal.Click += new System.EventHandler(this.cmd_Click);
			this.cmdSmall.Click += new System.EventHandler(this.cmd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmd_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("QueryStringRecipient.aspx" + "?Version=" + 
				((Control)sender).ID);

		}
	}
}
