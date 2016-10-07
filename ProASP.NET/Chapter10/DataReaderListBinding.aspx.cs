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
	/// Summary description for DataReaderListBinding.
	/// </summary>
	public class DataReaderListBinding : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ListBox Listbox1;
		protected System.Web.UI.WebControls.Button cmdGetSelection;
		protected System.Web.UI.WebControls.Literal Result;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				// Create the command and the connection.
				string connectionString = "Data Source=localhost;" +
					"Initial Catalog=Northwind;Integrated Security=SSPI";
		
				string sql = "SELECT EmployeeID, TitleOfCourtesy + ' ' + FirstName + ' ' + LastName As FullName FROM Employees";
				SqlConnection con = new SqlConnection(connectionString);
				SqlCommand cmd = new SqlCommand(sql, con);
      
				// Open the connection and get the DataReader.
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
           
				// bind the reader to the Listbox
				Listbox1.DataSource = reader;
				Listbox1.DataBind();
      
				// Close the reader and the connection.
				reader.Close();
				con.Close();			
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
			this.cmdGetSelection.Click += new System.EventHandler(this.cmdGetSelection_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdGetSelection_Click(object sender, System.EventArgs e)
		{
			Result.Text += "<b>Selected employees:</b>";
			foreach (ListItem li in Listbox1.Items)
			{
				if (li.Selected)
					Result.Text += String.Format("<li>({0}) {1}</li>", li.Value, li.Text);
			}
		}

	}
}
