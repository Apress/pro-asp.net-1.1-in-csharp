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
	/// Summary description for DataGrid1.
	/// </summary>
	public class DataGridEditing : System.Web.UI.Page
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
			// Caching no longer makes sense because the data can change.
			// However, you could keep the caching code and
			// explicitly remove the cached object
			// every time an edit is completed.

			// Create the DataSet.
			DataSet ds = CreateDataSet();

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
				"SELECT * FROM Employees ";
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
			this.Datagrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_Cancel);
			this.Datagrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_Edit);
			this.Datagrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_Update);
			this.Datagrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid1_Delete);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Datagrid1_Edit(Object sender, DataGridCommandEventArgs e)
		{
			Datagrid1.EditItemIndex = (int)e.Item.ItemIndex;
			BindGrid();
		}
  
		private void Datagrid1_Cancel(Object sender, DataGridCommandEventArgs e)
		{
			Datagrid1.EditItemIndex = -1;
			BindGrid();
		}

		private void Datagrid1_Update(Object sender, DataGridCommandEventArgs e)
		{
			// Get the ID of the record to update.
			int empID = (int)Datagrid1.DataKeys[e.Item.ItemIndex];
    
			// Get the references to the edit controls.
			DropDownList title = (DropDownList)e.Item.FindControl("EditTitle");
			TextBox lastName = (TextBox)e.Item.FindControl("EditLastName");
			TextBox firstName = (TextBox)e.Item.FindControl("EditFirstName");
			TextBox city = (TextBox)e.Item.Cells[5].Controls[0];
    
			// Create the connection and the UPDATE command.
			string sql = @"UPDATE Employees SET TitleOfCourtesy = @TitleOfCourtesy, "+
                "LastName = @LastName, FirstName = @FirstName, City = @City WHERE EmployeeID = @EmployeeID";
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand(sql, con);
    	
			// Create the required parameters.
			cmd.Parameters.Add(new SqlParameter("@TitleOfCourtesy", SqlDbType.NVarChar, 25));
			cmd.Parameters["@TitleOfCourtesy"].Value = title.SelectedItem.Text.Trim();
			cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 20));
			cmd.Parameters["@LastName"].Value = lastName.Text.Trim();
			cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 10));
			cmd.Parameters["@FirstName"].Value = firstName.Text.Trim();
			cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 15));
			cmd.Parameters["@City"].Value = city.Text.Trim();
			cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int, 4));
			cmd.Parameters["@EmployeeID"].Value = empID;
	
			// Execute the command.			
			try 
			{
				con.Open();
				cmd.ExecuteNonQuery();
			}
			catch (SqlException) 
			{
				// handle exception... 
			}
			finally 
			{ 
				con.Close();
			}
    
			// Stop the editing and rebind the grid.
			Datagrid1.EditItemIndex = -1;
			BindGrid();
		}
  
		private void Datagrid1_Delete(Object sender, DataGridCommandEventArgs e)
		{
			// Get the ID of the record to update.
			int empID = (int)Datagrid1.DataKeys[e.Item.ItemIndex];
       
			// Create the connection and the DELETE command.
			string sql = @"DELETE FROM Employees WHERE EmployeeID = " + empID.ToString();
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand(sql, con);
    	
			// Execute the command.
			try 
			{
				con.Open();
				cmd.ExecuteNonQuery();	
			}
			finally 
			{ 
				con.Close();
			}
    
			// Rebind the grid.
			Datagrid1.EditItemIndex = -1;
			BindGrid();
		}

		protected string[] TitlesOfCourtesy 
		{
			get 
			{
				return new string[4]{"Mr.", "Dr.", "Ms.", "Mrs."};
			}
		}
  
		protected int GetSelectedTitle(object title)
		{
			return Array.IndexOf(TitlesOfCourtesy, title.ToString());
		}
	}
}
