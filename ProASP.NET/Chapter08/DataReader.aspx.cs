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
	public class Command1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Literal HtmlContent;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create the Command and the Connection.
			string connectionString = "Data Source=localhost;Initial Catalog=Northwind;" + 
				"Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);
			string sql = "SELECT * FROM Employees";
			SqlCommand cmd = new SqlCommand(sql, con);
      
			// Open the Connection and get the DataReader.
			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();
    
			// Cycle through the records, and build the HTML string.
			StringBuilder htmlStr = new StringBuilder("");
			while (reader.Read())           
			{
				htmlStr.Append("<li>");
				htmlStr.Append(reader["TitleOfCourtesy"]);
				htmlStr.Append(" <b>");
				htmlStr.Append(reader.GetString(1));
				htmlStr.Append("</b>, ");
				htmlStr.Append(reader.GetString(2));
				htmlStr.Append(" - employee from ");
				htmlStr.Append(reader.GetDateTime(6).ToString("d"));
				htmlStr.Append("</li>");
			}
      
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
