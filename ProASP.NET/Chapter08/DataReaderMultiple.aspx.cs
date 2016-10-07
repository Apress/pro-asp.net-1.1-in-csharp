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

namespace Chapter08
{
	/// <summary>
	/// Summary description for Command1.
	/// </summary>
	public class DataReaderMultiple : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Literal HtmlContent;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create the Command and the Connection.
			string connectionString = "Data Source=localhost;Initial Catalog=Northwind;" + 
				"Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);
			string sql = @"SELECT TOP 5 EmployeeID, FirstName, LastName FROM Employees;" +
				"SELECT TOP 5 ContactName, ContactTitle FROM Customers;" +
				"SELECT TOP 5 SupplierID, CompanyName, ContactName FROM Suppliers";

			SqlCommand cmd = new SqlCommand(sql, con);
      
			// Open the Connection and get the DataReader.
			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();
    
			// Cycle through the records and all the rowsets,
			// and build the HTML string.
			StringBuilder htmlStr = new StringBuilder("");
			int i = 0;
			do 
			{
				htmlStr.Append("<h2>Rowset: ");
				htmlStr.Append(i.ToString());
				htmlStr.Append("</h2>");

			    while (reader.Read())
				{
					htmlStr.Append("<li>");
					// Get all the fields in this row.
				    for (int field = 0; field < reader.FieldCount; field++)
					{
						htmlStr.Append(reader.GetName(field).ToString());
						htmlStr.Append(": ");
						htmlStr.Append(reader.GetValue(field).ToString());
						htmlStr.Append("&nbsp;&nbsp;&nbsp;");
					}
					htmlStr.Append("</li>");
				}
				htmlStr.Append("<br><br>");
				i++;
			} while (reader.NextResult());

      
			// Close the DataReader and the Connection.
			reader.Close();
			con.Close();			
    
			// Show the generated HTML code on the page.
			HtmlContent.Text = htmlStr.ToString();
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
