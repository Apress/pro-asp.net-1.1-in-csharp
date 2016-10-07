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

namespace RichControls
{
	/// <summary>
	/// Summary description for Calendar.
	/// </summary>
	public class Calendar : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Calendar Calendar1;
		protected System.Web.UI.WebControls.Label lblDates;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-CA");
			// Put user code to initialize the page here
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
			this.Calendar1.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.Calendar1_DayRender);
			this.Calendar1.SelectionChanged += new System.EventHandler(this.Calendar1_SelectionChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void MyCalendar_SelectionChanged(object sender, System.EventArgs e)
		{
			lblDates.Text = "You selected these dates:<br>";

			foreach (DateTime dt in Calendar1.SelectedDates)
			{
				lblDates.Text += dt.ToLongDateString() + "<br>";
			}

		}

		private void Calendar1_SelectionChanged(object sender, System.EventArgs e)
		{
			lblDates.Text = "You selected these dates:<br>";
			foreach (DateTime dt in Calendar1.SelectedDates)
			{
				lblDates.Text += dt.ToLongDateString() + "<br>";
			}

		}

		private void Calendar1_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
		{
			if (e.Day.IsWeekend)
			{
				e.Cell.BackColor = System.Drawing.Color.Green;
				e.Cell.ForeColor = System.Drawing.Color.Yellow;
				e.Day.IsSelectable = false;
			}

		}
	}
}
