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

namespace Chapter09
{
	/// <summary>
	/// Summary description for DataSetUpdate.
	/// </summary>
	public class DataSetUpdate : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid Datagrid1;
		protected System.Web.UI.WebControls.Literal CommandsText;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string connectionString = "Data Source=localhost;Initial Catalog=Northwind;" + 
				"Integrated Security=SSPI";
		    string sql = "SELECT EmployeeID, TitleOfCourtesy, LastName, FirstName FROM Employees2";
			SqlConnection con = new SqlConnection(connectionString);
			SqlDataAdapter da = new SqlDataAdapter(sql, con);
			DataSet ds = new DataSet();
       
			// Create the CommandBuilder, and assign the commands to the DataAdapter.
			SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
			da = cmdBuilder.DataAdapter;
    
			// Fill the DataSet.
			da.Fill(ds, "Employees");
			DataTable table1 = ds.Tables["Employees"];
    
			// Bind the original data to the first grid.
			Datagrid1.DataSource = table1;
			Datagrid1.DataBind();
    
			// Do some editing on the DataTable.
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
    
			// Update the data in the database.
			da.Update(ds, "Employees");

			// Create the HTML string with the command text.
			StringBuilder str = new StringBuilder("");
			str.Append("<hr><b>InsertCommand:</b><br>");
			str.Append(cmdBuilder.GetInsertCommand().CommandText);
			str.Append("<br><br>");
			str.Append("<b>UpdateCommand:</b><br>");
			str.Append(cmdBuilder.GetUpdateCommand().CommandText);
			str.Append("<br><br>");
			str.Append("<b>DeleteCommand:</b><br>");
			str.Append(cmdBuilder.GetDeleteCommand().CommandText);
			str.Append("<hr>"); 

			CommandsText.Text = str.ToString();

			// Get fresh data from the database, and bind to the second grid.
			ds.Clear();
			da.Fill(ds, "Employees");
			Datagrid2.DataSource = ds.Tables["Employees"];
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
