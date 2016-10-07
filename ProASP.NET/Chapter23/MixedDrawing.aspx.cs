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
using System.Drawing.Drawing2D;
using System.IO;

namespace Chapter23
{
	/// <summary>
	/// Summary description for MixedDrawing.
	/// </summary>
	public class MixedDrawing : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create the in-memory bitmap where you will draw the image. 
			// This bitmap is 450 pixels wide and 100 pixels high.
			Bitmap image = new Bitmap(450, 100);
			Graphics g = Graphics.FromImage(image);

			// Ensure high-quality curves.
			g.SmoothingMode = SmoothingMode.AntiAlias;

			// Paint the background.
			g.FillRectangle(Brushes.White, 0, 0, 450, 100);

			// Add an ellipse.
			g.FillEllipse(Brushes.PaleGoldenrod, 120, 13, 300, 50);
			g.DrawEllipse(Pens.Green, 120, 13, 299, 49);

			// Draw some text using a fancy font.
			Font font = new Font("Harrington", 20, FontStyle.Bold);
			g.DrawString("Oranges are tasty!", font, Brushes.DarkOrange, 150, 20);

			// Add a graphic from a file.
			System.Drawing.Image orangeImage = System.Drawing.Image.FromFile(Server.MapPath("oranges.gif"));
			g.DrawImageUnscaled(orangeImage, 0, 0);

			// Render the image to the HTML output stream.
			Response.ContentType = "image/png";
			MemoryStream mem = new MemoryStream();	
			image.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
			mem.WriteTo(Response.OutputStream);
			
			// Clean up.
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
