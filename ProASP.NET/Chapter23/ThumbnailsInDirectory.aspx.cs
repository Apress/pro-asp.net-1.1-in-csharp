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

namespace Chapter23
{
	/// <summary>
	/// Summary description for ThumbnailsInDirectory.
	/// </summary>
	public class ThumbnailsInDirectory : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataList listThumbs;
		protected System.Web.UI.WebControls.Button cmdShow;
		protected System.Web.UI.WebControls.TextBox txtDir;
		protected System.Web.UI.WebControls.Label Label1;
	


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
			this.cmdShow.Click += new System.EventHandler(this.cmdShow_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdShow_Click(object sender, System.EventArgs e)
		{
			// Get a string array with all the image files.
			DirectoryInfo dir = new DirectoryInfo(txtDir.Text);
			listThumbs.DataSource = dir.GetFiles("*.bmp");

			// Bind the string array.
			listThumbs.DataBind();		
		}

		protected string GetImageUrl(object path)
		{
			return "ThumbnailViewer.aspx?x=50&y=50&FilePath=" +
				Server.UrlEncode((string)path);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
		
		}


	}
}
