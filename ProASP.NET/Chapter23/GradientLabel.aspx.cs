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
using System.Drawing.Imaging;
using System.IO;

namespace Chapter23
{
	/// <summary>
	/// Summary description for GradientLabel.
	/// </summary>
	public class GradientLabel : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			string text = Server.UrlDecode(Request.QueryString["Text"]);
			int textSize = Int32.Parse(Request.QueryString["TextSize"]);
			Color textColor = Color.FromArgb(Int32.Parse(Request.QueryString["TextColor"]));
			Color gradientColorA = Color.FromArgb(Int32.Parse(Request.QueryString["GradientColorA"]));
			Color gradientColorB = Color.FromArgb(Int32.Parse(Request.QueryString["GradientColorB"]));

			// Define the font.
			Font font = new Font("Tahoma", textSize, FontStyle.Bold);

			// Use a test image to measure the text.
			Bitmap image = new Bitmap(1, 1);
			Graphics g = Graphics.FromImage(image);
			SizeF size = g.MeasureString(text, font);
			g.Dispose();
			image.Dispose();
            
			// Using these measurements, try to choose a reasonable bitmap size.
			// Even if the text is large, cap the size at some maximum to
			// prevent causing a serious server slowdown!
			// (Allow for a 10 pixel buffer on all sides).
			int width = (int)Math.Min(size.Width + 20, 800);
			int height = (int)Math.Min(size.Height + 20, 800);
			image = new Bitmap(width, height);
			g = Graphics.FromImage(image);

			LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(new Point(0,0), image.Size),
				gradientColorA, gradientColorB, LinearGradientMode.ForwardDiagonal);

			// Draw the gradient background.
			g.FillRectangle(brush, 0, 0, 300, 300);
			
			// Draw the label text.
			g.DrawString(text, font, new SolidBrush(textColor), 10, 10);

			
			// Render the image to the output stream.
			Response.ContentType = "image/png";
			MemoryStream mem = new MemoryStream();	
			image.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
			mem.WriteTo(Response.OutputStream);
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
