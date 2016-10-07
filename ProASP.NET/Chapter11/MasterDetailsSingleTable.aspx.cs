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
	/// Summary description for MasterDetailsSingleTable.
	/// </summary>
	public class MasterDetailsSingleTable : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid gridMaster;
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
	
		private NorthwindDB db = new NorthwindDB();

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				gridMaster.DataSource = db.GetCategoriesProductsDataSet();
				gridMaster.DataMember = "Categories";
				gridMaster.DataBind();
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
			this.gridMaster.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.gridProducts_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void gridProducts_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// Look for DataGrid items.
			if (e.Item.ItemType == ListItemType.Item || 
			  e.Item.ItemType == ListItemType.AlternatingItem)
			{
				// Retrieve the DataGrid control in the second column.
				DataGrid gridChild = (DataGrid)e.Item.Cells[1].Controls[1];

				// Retrieve the DataSet. Once again, this DataSet
				// is cached, so performance will not suffer. You could
				// also store it in a page-level variable when the Page.Load
				// event fires, so its available for the duration of the request.
				DataSet ds = db.GetCategoriesProductsDataSet();
				
				// Filter the view to only show products in the current category.
				DataView view = ds.Tables["Products"].DefaultView;
				view.RowFilter = "CategoryID='" + gridMaster.DataKeys[e.Item.ItemIndex] + "'";

				// Bind the grid.
				gridChild.DataSource = ds.Tables["Products"];
				gridChild.DataBind();
			}

		}
	}
}
