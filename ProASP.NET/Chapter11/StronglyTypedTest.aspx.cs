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

namespace Chapter11
{
	/// <summary>
	/// Summary description for StronglyTypedTest.
	/// </summary>
	public class StronglyTypedTest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblOutput;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create the Connection, DataAdapter, and DataSet.
			string connectionString = "Data Source=localhost;Initial Catalog=Northwind;" + 
				"Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);
			string sqlProducts = "SELECT * FROM Products";
			string sqlCategories = "SELECT * FROM Categories";
						
			// Create the strongly typed DataSet.
			NorthwindDataSet ds = new NorthwindDataSet();
      
			// Fill the DataSet.
			SqlDataAdapter da = new SqlDataAdapter(sqlCategories, con);
			da.Fill(ds.Categories);
			da.SelectCommand.CommandText = sqlProducts;
			da.Fill(ds.Products);
    
			// Build the HTML string.
			StringBuilder htmlStr = new StringBuilder("");
			foreach (NorthwindDataSet.CategoriesRow row in ds.Categories)
			{
				htmlStr.Append("<b>");
				htmlStr.Append(row.CategoryName);
				htmlStr.Append("</b><br><i>");
				htmlStr.Append(row.Description);
				htmlStr.Append("</i><br>");

				// Get the related product rows.
				// Not the this uses the helper method GetProductsRows()
				// instead of the generic GetChildRows().
				// The advantage is you don't need to specify the relationship name.
				NorthwindDataSet.ProductsRow[] products = row.GetProductsRows();
				foreach (NorthwindDataSet.ProductsRow child in products)
				{
					htmlStr.Append("<li>");
					htmlStr.Append(child.ProductName);
					htmlStr.Append("</li>");
				}
				htmlStr.Append("<br><br>");
			}      
   
			// Show the generated HTML.
			lblOutput.Text = htmlStr.ToString();
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
