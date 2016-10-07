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

namespace Chapter10
{
	/// <summary>
	/// Summary description for DataList1.
	/// </summary>
	public class DataList3 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label MoreInfo;
		protected DataList Datalist1;

		string connectionString = "Data Source=localhost;" +
			"Initial Catalog=Northwind;Integrated Security=SSPI";
		
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

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				BindList();
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
			this.Datalist1.CancelCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.Datalist1_Cancel);
			this.Datalist1.EditCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.Datalist1_Edit);
			this.Datalist1.UpdateCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.Datalist1_Update);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Datalist1_Edit(Object sender, DataListCommandEventArgs e)
		{
			Datalist1.EditItemIndex = (int)e.Item.ItemIndex;
			BindList();
		}
  
		private void Datalist1_Cancel(Object sender, DataListCommandEventArgs e)
		{
			Datalist1.EditItemIndex = -1;
			BindList();
		}

		private void Datalist1_Update(Object sender, DataListCommandEventArgs e)
		{
			// Get the ID of the record to update.
			int empID = (int)Datalist1.DataKeys[e.Item.ItemIndex];
    
			// Get the references to the edit controls.
			DropDownList title = (DropDownList)e.Item.FindControl("EditTitle");
			TextBox lastName = (TextBox)e.Item.FindControl("EditLastName");
			TextBox firstName = (TextBox)e.Item.FindControl("EditFirstName");
    
			// Create the Connection and the Command.
			string sql = @"UPDATE Employees SET TitleOfCourtesy = @TitleOfCourtesy, " + 
				"LastName = @LastName, FirstName = @FirstName WHERE EmployeeID = @EmployeeID";
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand(sql, con);
    	
			// Create the required parameters.
			cmd.Parameters.Add(new SqlParameter("@TitleOfCourtesy", SqlDbType.NVarChar, 25));
			cmd.Parameters["@TitleOfCourtesy"].Value = title.SelectedItem.Text.Trim();
			cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 20));
			cmd.Parameters["@LastName"].Value = lastName.Text.Trim();
			cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 10));
			cmd.Parameters["@FirstName"].Value = firstName.Text.Trim();
			cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int, 4));
			cmd.Parameters["@EmployeeID"].Value = empID;
	
			// Execute the Command.
			try 
			{
				con.Open();
				cmd.ExecuteNonQuery();	
			}
			finally 
			{ 
				cmd.Connection.Close();
			}
    
			// Stop the editing and rebind the list.
			Datalist1.EditItemIndex = -1;
			BindList();
		}


		private void BindList()
		{
			string sql = 
				"SELECT EmployeeID, FirstName, LastName, TitleOfCourtesy FROM Employees";

			// Create the Command and the Connection.
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand(sql, con);
      
			// Open the connection and get the DataReader.
			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();
           
			// Bind the Reader to the Repeater.
			Datalist1.DataSource = reader;
			Datalist1.DataBind();
      
			// Close the DataReader and the Connection.
			reader.Close();
			con.Close();		
		}
		
	}
}
