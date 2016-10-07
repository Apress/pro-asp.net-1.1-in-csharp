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

namespace Chapter09
{
	/// <summary>
	/// Summary description for DataViewSort.
	/// </summary>
	public class DataViewSort : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid Datagrid1;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.DataGrid Datagrid3;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create the Connection, DataAdapter, and DataSet.
			string connectionString = "Data Source=localhost;Initial Catalog=Northwind;" + 
				"Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);
			string sql = "SELECT TOP 5 EmployeeID, TitleOfCourtesy, LastName, FirstName FROM Employees";
			
			SqlDataAdapter da = new SqlDataAdapter(sql, con);
			DataSet ds = new DataSet();
			
			// Fill the DataSet
			da.Fill(ds, "Employees");
				
			// Bind the original data to #1.
			Datagrid1.DataSource = ds.Tables["Employees"];	
    
			// Sort by last name and bind it to #2.
			DataView view2 = new DataView(ds.Tables["Employees"]);
    		view2.Sort = "LastName";
			Datagrid2.DataSource = view2;
			
    		// Sort by first name and bind it to #3.
			DataView view3 = new DataView(ds.Tables["Employees"]);
			view3.Sort = "FirstName";
			Datagrid3.DataSource = view3;

			// Bind all the data-bound controls on the page.
			// Alternatively, you could call the DataBind() method
			// of each grid separately
			this.DataBind();
			
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
