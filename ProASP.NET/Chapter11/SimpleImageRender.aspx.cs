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
using System.Configuration;
using System.IO;

namespace Chapter11
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class SimpleRender : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			string connectionString = 
				"Data Source=localhost;Initial Catalog=pubs;Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);
			string SQL = "SELECT logo FROM pub_info WHERE pub_id='1389'";

			SqlCommand cmd = new SqlCommand(SQL, con);
			
			try
			{
				con.Open();
				SqlDataReader r = cmd.ExecuteReader();
			
				if (r.Read())
				{
					byte[] bytes = (byte[])r["logo"];
					Response.BinaryWrite(bytes);
				}
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
