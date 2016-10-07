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
	/// Summary description for DataSetVersioning.
	/// </summary>
	public class DataSetVersioning : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid Datagrid4;
		protected System.Web.UI.WebControls.DataGrid Datagrid3;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.DataGrid Datagrid5;
		protected System.Web.UI.WebControls.DataGrid Datagrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string connectionString = "Data Source=localhost;Initial Catalog=Northwind;" + 
				"Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);
			string sql = "SELECT TOP 3 EmployeeID, TitleOfCourtesy, LastName, FirstName FROM Employees";
			SqlDataAdapter da = new SqlDataAdapter(sql, con);
			DataSet ds = new DataSet();
    
			// Fill the DataSet.
			da.Fill(ds, "Employees");
			DataTable table = ds.Tables["Employees"];
    			
			// Bind the original data to grid #1.
			Datagrid1.DataSource = table;    
			Datagrid1.DataBind();
    
			// Edit the third row.
			table.Rows[2].BeginEdit();
			table.Rows[2]["LastName"] = "Thompson";
			table.Rows[2]["FirstName"] = "Jenny";
			table.Rows[2].EndEdit();

			// Create a new row.
			DataRow newRow = table.NewRow();
			newRow["TitleOfCourtesy"] = "Mr.";
			newRow["LastName"] = "Bellinaso";
			newRow["FirstName"] = "Marco";
			table.Rows.Add(newRow);

			// Delete the first row.
			table.Rows[0].Delete();

			// Bind the edited data to grid #2.
			Datagrid2.DataSource = table;
			Datagrid2.DataBind();

			// Bind the added rows to grid #3.
			DataView view3 = new DataView(table);
			view3.RowStateFilter = DataViewRowState.Added;
			Datagrid3.DataSource = view3;
			Datagrid3.DataBind();

			// Bind the deleted rows to grid #4.
			DataView view4 = new DataView(table);
			view4.RowStateFilter = DataViewRowState.Deleted;
			Datagrid4.DataSource = view4;
			Datagrid4.DataBind();

			// Show the original data from the edited rows in grid #5.
			DataView view5 = new DataView(table);
			view5.RowStateFilter = DataViewRowState.ModifiedOriginal;
			Datagrid5.DataSource = view5;
			Datagrid5.DataBind();
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
