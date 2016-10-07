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
	/// Summary description for Repeater1.
	/// </summary>
	public class Repeater2 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Repeater Repeater1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				string sql = 
					"SELECT FirstName, LastName, TitleOfCourtesy FROM Employees";

				// Create the Command and the Connection.
				string connectionString = "Data Source=localhost;" +
					"Initial Catalog=Northwind;Integrated Security=SSPI";
				SqlConnection con = new SqlConnection(connectionString);
				SqlCommand cmd = new SqlCommand(sql, con);
      
				// Open the connection and get the DataReader.
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();
           
				// Bind the Reader to the Repeater.
				Repeater1.DataSource = reader;
				Repeater1.DataBind();
      
				// Close the DataReader and the Connection.
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
