using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace Chapter24
{
	/// <summary>
	/// Summary description for LogServiceTest.
	/// </summary>
	public class LogServiceTest : System.Web.Services.WebService
	{
		public LogServiceTest()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		private string connectionString = "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=SSPI";

		[SoapLog(Name="EmployeesService.GetEmployeesCount", Level=3)]
		[WebMethod(Description="Returns the total number of employees.")]
		public int GetEmployeesCount()
		{
			// uncomment the following line if you want to test the
			// asynchronous calls with the EmployeesWinAsync project
			// System.Threading.Thread.Sleep(3000);
			// Create the command and the connection.	
			SqlConnection con = new SqlConnection(connectionString);
			string sql = "SELECT COUNT(*) FROM Employees";
			SqlCommand cmd = new SqlCommand(sql, con);
			
			// Open the connection and get the value.
			cmd.Connection.Open();
			int numEmployees = -1;
			try
			{
				numEmployees = (int)cmd.ExecuteScalar();
			}
			finally 
			{
				cmd.Connection.Close();
			}
			return numEmployees;
		}
	}
}
