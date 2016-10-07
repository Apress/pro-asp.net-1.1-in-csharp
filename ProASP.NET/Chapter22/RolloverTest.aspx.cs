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

namespace Chapter22
{
	/// <summary>
	/// Summary description for RolloverTest.
	/// </summary>
	public class RolloverTest : System.Web.UI.Page
	{
		protected CustomServerControlsLibrary.RollOverButton RollOverButton2;
		protected CustomServerControlsLibrary.RollOverButton RollOverButton1;
	
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
			this.RollOverButton1.ImageClicked += new System.EventHandler(this.RollOverButton1_ImageClicked);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void RollOverButton1_ImageClicked(object sender, System.EventArgs e)
		{
			Response.Write("Message clicked.");
		}
	}
}
