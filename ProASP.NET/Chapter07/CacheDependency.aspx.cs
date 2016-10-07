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
using System.IO;

namespace DataCaching
{
	/// <summary>
	/// Summary description for CacheDependency.
	/// </summary>
	public class CacheDependency : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button cmdModfiy;
		protected System.Web.UI.WebControls.Button cmdGetItem;
		protected System.Web.UI.WebControls.Label lblInfo;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				lblInfo.Text += "Creating dependent item...<br>";
				Cache.Remove("Dependent");
				System.Web.Caching.CacheDependency dependency =
					new System.Web.Caching.CacheDependency(Server.MapPath("dependency.txt"));
				string item = "Dependent cached item";
				lblInfo.Text += "Adding dependent item<br>";
				Cache.Insert("Dependent", item, dependency);
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
			this.cmdModfiy.PreRender += new System.EventHandler(this.cmdModfiy_PreRender);
			this.cmdModfiy.Click += new System.EventHandler(this.cmdModfiy_Click);
			this.cmdGetItem.Click += new System.EventHandler(this.cmdGetItem_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdGetItem_Click(object sender, System.EventArgs e)
		{
			if (Cache["Dependent"] == null)
			{
				lblInfo.Text += "Cache item no longer exits.<br>";
			}
			else
			{
				string cacheInfo = (string)Cache["Dependent"];
				lblInfo.Text += "Retrieved item with text: '" + cacheInfo + "'<br>";
			}
		}

		private void cmdModfiy_Click(object sender, System.EventArgs e)
		{
			lblInfo.Text += "Modifying dependency.txt file.<br>";
			StreamWriter w= File.CreateText(Server.MapPath("dependency.txt"));
			w.Write(DateTime.Now);
			w.Flush();
			w.Close();
		}

		private void cmdModfiy_PreRender(object sender, System.EventArgs e)
		{
			lblInfo.Text += "<br>";
		}
	}
}
