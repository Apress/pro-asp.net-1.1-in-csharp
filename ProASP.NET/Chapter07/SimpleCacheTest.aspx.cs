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

namespace DataCaching
{
	/// <summary>
	/// Summary description for SimpleCacheTest.
	/// </summary>
	public class SimpleCacheTest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
				
			if (this.IsPostBack)
			{
				lblInfo.Text += "Page posted back.<br>";
			}
			else
			{
				lblInfo.Text += "Page created.<br>";
			}

			if (Cache["TestItem"] == null)
			{
				lblInfo.Text += "Creating TestItem...<br>";
				DateTime testItem = DateTime.Now;

				lblInfo.Text += "Storing TestItem in cache";
				lblInfo.Text += " for 30 seconds.<br>";
				Cache.Insert("TestItem", testItem, null, 
					DateTime.Now.AddSeconds(30), TimeSpan.Zero);
			}
			else
			{
				lblInfo.Text += "Retrieving TestItem...<br>";
				DateTime testItem = (DateTime)Cache["TestItem"];
				lblInfo.Text += "TestItem is '" + testItem.ToString();
				lblInfo.Text += "'<br>";
			}

			lblInfo.Text += "<br>";

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
