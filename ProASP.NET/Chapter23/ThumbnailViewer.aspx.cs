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

namespace Chapter23
{
	/// <summary>
	/// Summary description for ThumbnailViewer.
	/// </summary>
	public class ThumbnailViewer : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if ((Request.QueryString["X"] == null) ||
				(Request.QueryString["Y"] == null) ||
				(Request.QueryString["FilePath"] == null))
			{
				// There is missing data, so don't display anything.
				// Other options including choosing reasonable defaults,
				// or returning an image with some static error text.
			}
			else
			{
				int x = Int32.Parse(Request.QueryString["X"]);
				int y = Int32.Parse(Request.QueryString["Y"]);
				string file = Server.UrlDecode(Request.QueryString["FilePath"]);
                
				// Create the in-memory bitmap where you will draw the image. 
				Bitmap image = new Bitmap(x, y);
				Graphics g = Graphics.FromImage(image);

				// Load the file data.
				System.Drawing.Image thumbnail = System.Drawing.Image.FromFile(file);

				// Draw the thumbnail.
				g.DrawImage(thumbnail, 0, 0, x, y);

				// Render the image.
				image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
				g.Dispose();
				image.Dispose();
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
