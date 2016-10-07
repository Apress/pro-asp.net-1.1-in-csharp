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

namespace UserControls
{
	/// <summary>
	/// Summary description for LinkTableHost.
	/// </summary>
	public class LinkTableHost : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
		protected LinkTable LinkTable1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Set the title.
			LinkTable1.Title = "A List of Links";

			// Set the hyperlinked item list.
			LinkTableItem[] items = new LinkTableItem[3];
			items[0] = new LinkTableItem("Test Item 1", "http://www.apress.com");
			items[1] = new LinkTableItem("Test Item 2", "http://www.apress.com");
			items[2] = new LinkTableItem("Test Item 3", "http://www.apress.com");
			LinkTable1.Items = items;

			// Attach the event hander.
			LinkTable1.LinkClicked += new LinkClickedEventHandler(LinkClicked);
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

		private void LinkClicked(object sender, LinkTableEventArgs e)
		{
			lblInfo.Text = "You clicked '" + e.SelectedItem.Text +
				"' but this page chose not to direct you to '" +
				e.SelectedItem.Url + "'.";
			e.Cancel = true;
		}

	}
}
