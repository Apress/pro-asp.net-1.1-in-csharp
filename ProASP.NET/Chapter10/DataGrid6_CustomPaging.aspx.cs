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
	public class DataGridCustomPaging : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid Datagrid1;
	
		string connectionString = "Data Source=localhost;" +
			"Initial Catalog=Northwind;Integrated Security=SSPI";
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				SetGridItemCount();
				BindGrid();
			}
		}

		private void SetGridItemCount()
		{
			// Create the Command and the Connection.
			string sql = "SELECT COUNT(*) FROM Employees";
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand(sql, con);
			con.Open();

			// Execute the command and use the return value for the
			// VirtualItemCount property.
			Datagrid1.VirtualItemCount = (int)cmd.ExecuteScalar();
			con.Close();
		} 

		private void BindGrid()
		{
			// Define a Command that uses the stored procedure.
			SqlConnection con = new SqlConnection(connectionString);
    		SqlCommand cmd = new SqlCommand("GetEmployeesByPage", con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int, 4));
			cmd.Parameters["@PageNumber"].Value = Datagrid1.CurrentPageIndex + 1;
			cmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int, 4));
			cmd.Parameters["@PageSize"].Value = Datagrid1.PageSize;
    
			// Open the Connection and get the DataReader.
			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();
           
			// Bind the DataReader to the DataGrid.
			Datagrid1.DataSource = reader;
			Datagrid1.DataBind();
      
			// Clean up.
			reader.Close();
			con.Close();			
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
