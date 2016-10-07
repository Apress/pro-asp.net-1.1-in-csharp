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
	public class TransactionTest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string connectionString = "Data Source=localhost;Initial Catalog=Northwind;" + 
				"Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);
			
			SqlCommand cmd1 = new SqlCommand(
              "INSERT INTO Employees2 (LastName, FirstName) VALUES ('Joe','Tester')");
			SqlCommand cmd2 = new SqlCommand(
			  "INSERT INTO Employees2 (LastName, FirstName) VALUES ('Harry','Sullivan')");

			SqlTransaction tran = null;
			try
			{
				// Open the connection and create the transaction.
				con.Open();
				tran = con.BeginTransaction();

				// Enlist two commands in the transaction.
				cmd1.Transaction = tran;
				cmd2.Transaction = tran;

				// Execute both commands and commit the transaction.
				cmd1.ExecuteNonQuery();
				cmd2.ExecuteNonQuery();

				// To test the transaction rollback, uncomment the line below.
				//throw new ApplicationException();

				tran.Commit();
				lblInfo.Text = "Transaction committed.";
			}
			catch
			{
				// In the case of error, roll back the transaction.
				tran.Rollback();
				lblInfo.Text = "Transaction aborted.";
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
