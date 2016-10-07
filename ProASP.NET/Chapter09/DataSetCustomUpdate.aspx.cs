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
using System.Text;
using DatabaseComponent;

namespace Chapter09
{
	/// <summary>
	/// Summary description for DataSetUpdate.
	/// </summary>
	public class DataSetCustomUpdate : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid Datagrid1;
		protected System.Web.UI.WebControls.Literal CommandsText;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Employee2DB db = new Employee2DB();

			DataTable table1 = db.GetAllEmployees();
    
			// Bind the original data to the first grid.
			Datagrid1.DataSource = table1;
			Datagrid1.DataBind();
    			
			// Do some editing.
			table1.Rows[2].BeginEdit();
			table1.Rows[2]["LastName"] = "Thompson";
			table1.Rows[2]["FirstName"] = "Jenny";
			table1.Rows[2].EndEdit();
			
			// Create two new rows.
			DataRow newRow = table1.NewRow();
			newRow["TitleOfCourtesy"] = "Mr.";
			newRow["LastName"] = "Bellinaso";
			newRow["FirstName"] = "Marco";		
			table1.Rows.Add(newRow);

			newRow = table1.NewRow();
			newRow["TitleOfCourtesy"] = "Mrs.";
			newRow["LastName"] = "Virginia";
			newRow["FirstName"] = "Woolf";
			table1.Rows.Add(newRow);

			// Delete a row.
			table1.Rows[0].Delete();
    
			// Update the data source.
			table1 = db.UpdateEmployeeBatch(table1);

			// Get fresh data from the database, and bind to the second grid.
			Datagrid2.DataSource = table1;
			Datagrid2.DataBind();
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
