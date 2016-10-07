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
	/// Summary description for CreateChart.
	/// </summary>
	public class CreateChart : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.ListBox lstPieSlices;
		protected System.Web.UI.WebControls.Button cmdAdd;
		protected System.Web.UI.WebControls.TextBox txtLabel;
		protected System.Web.UI.WebControls.TextBox txtValue;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
	
		// The data that will be used to create the pie chart.
		private ArrayList pieSlices = new ArrayList();

		private void Page_Load(object sender, System.EventArgs e)
		{
			 // Retrieve the pie slices that are defined so far.
			if (Session["ChartData"] != null)
			{
				pieSlices = (ArrayList)Session["ChartData"];
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
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.CreateChart_PreRender);

		}
		#endregion

		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			// Create a new pie slice.
			PieSlice pieSlice = new PieSlice(txtLabel.Text, Single.Parse(txtValue.Text));
			pieSlices.Add(pieSlice);

			// Bind the list box to the new data.
			lstPieSlices.DataSource = pieSlices;
			lstPieSlices.DataBind();
		}

		private void CreateChart_PreRender(object sender, System.EventArgs e)
		{
			// Before rendering the page, store the current collection
			// of pie slices.
			Session["ChartData"] = pieSlices;
		}
	}
}
