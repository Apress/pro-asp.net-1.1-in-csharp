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
	/// Summary description for SimpleDrawing.
	/// </summary>
	public class SimpleDrawing : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create the in-memory bitmap where you will draw the image. 
			// This bitmap is 300 pixels wide and 50 pixels high.
			Bitmap image = new Bitmap(300, 50);
			Graphics g = Graphics.FromImage(image);

			// Draw a solid white rectangle..
			// Start from point (1, 1).
			// Make it 298 pixels wide and 48 pixels high.
			g.FillRectangle(Brushes.White, 0, 0, 300, 50);
			g.DrawRectangle(Pens.Green, 0, 0, 299, 49);

			// Draw some text using a fancy font.
			Font font = new Font("Impact", 20, FontStyle.Regular);
			g.DrawString("This is a test.", font, Brushes.Blue, 10, 5);

			// Render the image to the HTML output stream.
			image.Save(Response.OutputStream, 
				System.Drawing.Imaging.ImageFormat.Gif);

			g.Dispose();
			image.Dispose();

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
