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

namespace Chapter08
{
	/// <summary>
	/// Summary description for SqlInjection.
	/// </summary>
	public class SqlInjectionCorrected : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button cmdGetRecords;
		protected System.Web.UI.WebControls.TextBox txtID;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.cmdGetRecords.Click += new System.EventHandler(this.cmdGetRecords_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdGetRecords_Click(object sender, System.EventArgs e)
		{
			string connectionString = "Data Source=localhost;Initial Catalog=Northwind;" + 
				"Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);
			string sql =
				"SELECT Orders.CustomerID, Orders.OrderID, COUNT(UnitPrice) AS Items, " +
				"SUM(UnitPrice * Quantity) AS Total FROM Orders " +
				"INNER JOIN [Order Details] " +
				"ON Orders.OrderID = [Order Details].OrderID " +
                "WHERE Orders.CustomerID = @CustID " +
                "GROUP BY Orders.OrderID, Orders.CustomerID";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.Add("@CustID", txtID.Text);

			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			DataGrid1.DataSource = reader;
			DataGrid1.DataBind();
    		reader.Close();
			con.Close();
		}
	}
}
