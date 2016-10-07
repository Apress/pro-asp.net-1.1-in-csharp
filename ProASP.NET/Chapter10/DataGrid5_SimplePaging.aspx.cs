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
using System.Data.SqlClient;

namespace Chapter10
{
	/// <summary>
	/// Summary description for DataGridSimplePaging.
	/// </summary>
	public class DataGridSimplePaging : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid Datagrid1;
	
		string connectionString = "Data Source=localhost;" +
			"Initial Catalog=Northwind;Integrated Security=SSPI";
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				BindGrid();
			}
		}

		private void BindGrid()
		{
			DataSet ds;

			// Check the cache.
			if (Cache["Employees"] != null)
			{
				ds = (DataSet)Cache["Employees"];
			}
			else
			{
				// Create the DataSet.
				ds = CreateDataSet();

				// Cache the DataSet for five minutes.
				Cache.Insert("Employees", ds, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
			}
			
			// Retrieve the default view for the DataSet.
			DataView dv = ds.Tables["Employees"].DefaultView;

			// Set the sort order.
			dv.Sort = (string)ViewState["SortExpression"];

			// Bind the grid.
			Datagrid1.DataSource = dv;
			Datagrid1.DataBind();
		}

		private DataSet CreateDataSet()
		{
			string sql = 
				"SELECT EmployeeID, FirstName, LastName, TitleOfCourtesy FROM Employees ";
			SqlConnection con = new SqlConnection(connectionString);
			SqlDataAdapter da = new SqlDataAdapter(sql, con);
			DataSet ds = new DataSet();
			da.Fill(ds, "Employees");
			return ds;
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
			this.Datagrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Datagrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			// deselect the currently selected row, if any
			Datagrid1.SelectedIndex = -1; 

			// change the current page and rebind
			Datagrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid();

		}
	}
}
