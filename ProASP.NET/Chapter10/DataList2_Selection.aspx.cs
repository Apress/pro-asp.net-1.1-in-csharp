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
	public class DataList2 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label MoreInfo;
		protected DataList Datalist1;

		string connectionString = "Data Source=localhost;" +
			"Initial Catalog=Northwind;Integrated Security=SSPI";
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
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
			this.Datalist1.SelectedIndexChanged += new System.EventHandler(this.Datalist1_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Datalist1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int empID = (int)Datalist1.DataKeys[Datalist1.SelectedIndex];
			string sql = "SELECT * FROM Employees WHERE EmployeeID = " + empID.ToString();
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand(sql, con);
      
			// open the connection and get the Reader
			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();
           
			// show the results
			StringBuilder str = new StringBuilder("");
			reader.Read();
			str.Append("<b>");
			str.Append(reader["FirstName"].ToString());
			str.Append(" ");
			str.Append(reader["LastName"].ToString());
			str.Append("<br>");
			str.Append(reader["Title"].ToString());
			str.Append("<br>");
			str.Append(reader["Address"].ToString());
			str.Append("<br>");
			str.Append(reader["City"].ToString());
			str.Append(", ");
			str.Append(reader["Region"].ToString());
			str.Append("</b><br>");
			str.Append(reader["Notes"].ToString());
			MoreInfo.Text = str.ToString();
      
			// close the reader and the connection
			reader.Close();
			con.Close();
    
			// set the forecolor according to the ItemType
			ListItemType lit = Datalist1.Items[Datalist1.SelectedIndex].ItemType;
			MoreInfo.ForeColor = (lit == ListItemType.Item ? 
				System.Drawing.Color.DarkBlue : System.Drawing.Color.Maroon);
		}
	}
}
