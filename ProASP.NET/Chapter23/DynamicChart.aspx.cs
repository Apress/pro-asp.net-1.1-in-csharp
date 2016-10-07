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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Chapter23
{
	/// <summary>
	/// Summary description for DynamicChart.
	/// </summary>
	public class DynamicChart : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create the in-memory bitmap where you will draw the image. 
			Bitmap image = new Bitmap(300, 200);
			Graphics g = Graphics.FromImage(image);

			g.FillRectangle(Brushes.White, 0, 0, 300, 200);
			g.SmoothingMode = SmoothingMode.AntiAlias;
            
			if (Session["ChartData"] != null)
			{
				// Retrieve the chart data.
				ArrayList chartData = (ArrayList)Session["ChartData"];
						
				// Write some text to the image.
				g.DrawString("Sample Chart", new Font("Verdana", 18, FontStyle.Bold), Brushes.Black, new PointF(5, 5));
            
				// Calculate the total of all data values.
				float total = 0;
				foreach (PieSlice item in chartData)
				{
					total += item.DataValue;
				}

				// Draw the pie slices.
				float currentAngle = 0, totalAngle = 0;
				int i = 0;
				foreach (PieSlice item in chartData)
				{
					currentAngle = item.DataValue / total * 360;
					g.FillPie(new SolidBrush(GetColor(i)), 10, 40, 150, 150, (float)Math.Round(totalAngle), (float)Math.Round(currentAngle));
					totalAngle += currentAngle;
					i++;
				}
            
				// Create a legend for the chart.
				PointF colorBoxPoint = new PointF(200, 83);
				PointF textPoint = new PointF(222, 80);
			
				i = 0;
				foreach (PieSlice item in chartData)
				{
					g.FillRectangle(new SolidBrush(GetColor(i)), colorBoxPoint.X, colorBoxPoint.Y, 20, 10);
					g.DrawString(item.Caption, new Font("Tahoma", 10), Brushes.Black, textPoint);
					colorBoxPoint.Y += 15;
					textPoint.Y += 15;
					i++;
				}

				// Render the image to the HTML output stream.
				image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
			}
		}

		private Color GetColor(int index)
		{
			// Support six different colors. This could be enhanced.
			if (index > 5)
			{
				index = index % 5;
			}
								  
			switch (index)
			{
				case 0:
					return Color.Red;
				case 1:
					return Color.Blue;
				case 2:
					return Color.Yellow;
				case 3:
					return Color.Green;
				case 4:
					return Color.Orange;
				case 5:
					return Color.Purple;
				default:
					return Color.Black;
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

	public class PieSlice
	{
		private float dataValue;
		private string caption;

		public float DataValue
		{
			get {return dataValue;}
			set {dataValue = value;}
		}

		public string Caption
		{
			get {return caption;}
			set {caption = value;}
		}

		public PieSlice(string caption, float dataValue)
		{
			Caption = caption;
			DataValue = dataValue;
		}

		public override string ToString()
		{
			return Caption + " (" + DataValue.ToString() + ")";
		}

	}

}
