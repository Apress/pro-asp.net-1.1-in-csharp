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
	/// Summary description for TestConnection.
	/// </summary>
	public class TestConnection : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create the connection object.
			string connectionString = "Data Source=localhost;Initial Catalog=Northwind;" + 
				"Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);

			try
			{
				// Try to open the connection.
				con.Open();
				lblInfo.Text = "<b>Server Version:</b> " + con.ServerVersion;
				lblInfo.Text += "<br><b>Connection Is:</b> " + con.State.ToString();
			}
			catch (Exception err)
			{
				// Handle an error by displaying the information.
				lblInfo.Text = "Error reading the database. ";
				lblInfo.Text += err.Message;
			}
			finally
			{
				// Either way, make sure the connection is properly closed.
				// Even if the connection wasn't opened successfully,
				// calling Close() won't cause an error.
				con.Close();
				lblInfo.Text += "<br><b>Now Connection Is:</b> ";
				lblInfo.Text +=  con.State.ToString();
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
