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
using DatabaseComponent;

namespace Chapter11
{
	/// <summary>
	/// Summary description for Details.
	/// </summary>
	public class Details : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid gridDetails;
		protected System.Web.UI.WebControls.DataList listDetail;
		private NorthwindDB db = new NorthwindDB();

		private decimal valueInStock = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Session["SelectedCategory"] != null)
			{
				DataSet ds = db.GetCategoriesProductsDataSet();
				DataView view = ds.Tables["Products"].DefaultView;
				view.RowFilter = "CategoryID =" + Session["SelectedCategory"].ToString();

				// The easiest way to calculate the total value for
				// the displayed records is to work through the DataView,
				// not the DataTable. That's because the DataView will
				// include only the rows in the apporopriate category.
				foreach (DataRowView rowView in view)
				{
					DataRow row = rowView.Row;
					valueInStock += (short)row["UnitsInStock"] * (decimal)row["UnitPrice"];
				}

				// Bind the grid.
				gridDetails.DataSource = view;
				gridDetails.DataBind();
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
			this.gridDetails.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.gridDetails_ItemCreated);
			this.gridDetails.SelectedIndexChanged += new System.EventHandler(this.gridDetails_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void gridDetails_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataSet ds = db.GetCategoriesProductsDataSet();
			DataView view = new DataView(ds.Tables["Products"]);
			view.RowFilter = "ProductID =" + gridDetails.DataKeys[gridDetails.SelectedIndex].ToString();
			listDetail.DataSource = view;
			listDetail.DataBind();
		}


		private void gridDetails_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// Check if this item is a footer.
			ListItemType itemType = e.Item.ItemType;
			if ((itemType == ListItemType.Footer)) 
			{
				// Set the first cell to span over the entire row.
				e.Item.Cells[0].ColumnSpan = 3;
				e.Item.Cells[0].HorizontalAlign = HorizontalAlign.Center;

				// Remove the unneeded cells.
				e.Item.Cells.RemoveAt(2);
				e.Item.Cells.RemoveAt(1);

				// Add the text.
				e.Item.Cells[0].Text = "Total value in stock: " + 
				  valueInStock.ToString("C");
			}
		
		}

	}
}
