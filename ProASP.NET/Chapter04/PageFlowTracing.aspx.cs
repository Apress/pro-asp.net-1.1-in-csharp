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

namespace Chapter3
{
	/// <summary>
	/// Summary description for PageFlow.
	/// </summary>
	public class PageFlowTracing : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Trace.IsEnabled=true;
			lblInfo.Text += "Page.Load event handled.<br>";
			if (Page.IsPostBack)
			{
				lblInfo.Text += "<b>This is the second time you've seen this page.</b><br>";
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Unload += new System.EventHandler(this.PageFlow_Unload);
			this.Load += new System.EventHandler(this.Page_Load);
			this.Init += new System.EventHandler(this.PageFlow_Init);
			this.PreRender += new System.EventHandler(this.PageFlow_PreRender);

		}
		#endregion

		private void PageFlow_Init(object sender, System.EventArgs e)
		{
			lblInfo.Text += "Page.Init event handled.<br>";
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			// You can supply just a message, or include a category label,
			// as shown here.
			Trace.Write("Button1_Click", "About to update the label.");
			lblInfo.Text += "Button1.Click event handled.<br>";
			Trace.Write("Button1_Click", "Label updated.");
		}

		private void PageFlow_PreRender(object sender, System.EventArgs e)
		{
			lblInfo.Text += "Page.PreRender event handled.<br>";
		}

		private void PageFlow_Unload(object sender, System.EventArgs e)
		{
			// This text never appears because the HTML is already rendered for the page.
			lblInfo.Text += "Page.Unload event handled.<br>";
		}

	}
}
