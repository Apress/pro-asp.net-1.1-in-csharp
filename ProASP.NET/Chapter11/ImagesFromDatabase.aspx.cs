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

namespace Chapter11
{
	/// <summary>
	/// Summary description for ImagesFromDatabase.
	/// </summary>
	public class ImagesFromDatabase : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.DataList DataList1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string connectionString = 
				"Data Source=localhost;Initial Catalog=pubs;Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);
			string SQL = "SELECT * FROM publishers";

			SqlCommand cmd = new SqlCommand(SQL, con);
						
			try
			{
				con.Open();
				SqlDataReader r = cmd.ExecuteReader();
			
				DataList1.DataSource = r;
				DataList1.DataBind();
				r.Close();
			}
			finally
			{
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
