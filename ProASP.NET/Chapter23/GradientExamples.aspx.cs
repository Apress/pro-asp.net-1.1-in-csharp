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
	/// Summary description for GradientExamples.
	/// </summary>
	public class GradientExamples : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create the in-memory bitmap where you will draw the image. 
			Bitmap image = new Bitmap(300, 300);
			Graphics g = Graphics.FromImage(image);

			// Paint the background.
			g.FillRectangle(Brushes.White, 0, 0, 300, 300);
			
			// Create a brush to use.
			LinearGradientBrush myBrush;

			// Create variable to track the coordinates in the image.
			int y = 20;
			int x = 20;
	
			// Show a rectangle with each type of gradient.
			foreach (LinearGradientMode gradientStyle in System.Enum.GetValues(typeof(LinearGradientMode)))
			{
				myBrush = new LinearGradientBrush(new Rectangle(x, y, 100, 60), Color.Violet, Color.White, gradientStyle);
				g.FillRectangle(myBrush, x, y, 100, 60);
				g.DrawString(gradientStyle.ToString(), new Font("Tahoma", 8), Brushes.Black, 110 + x, y + 20);
				y += 70;
			}
			
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
