using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Xml;
using System.Web.Services.Protocols;

namespace Chapter24
{
	[WebService (Name="EmployeesService",
		 Description="Retrieve the Northwind Employees",
		 Namespace="http://www.apress.com/ProASP.NET/")]
	public class EmployeesService : System.Web.Services.WebService
	{
		public EmployeesService()
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

		[WebMethod(Description="Returns the total number of employees.")]
		public int GetEmployeesCount()
		{
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
     		

		[WebMethod(Description="Returns the full list of employees.")]
		public DataSet GetEmployees()
		{
			// Simulate a delay of 3 seconds.
			System.Threading.Thread.Sleep(4000);

			// Create the command and the connection.
			string sql = "SELECT EmployeeID, LastName, FirstName, Title, " +
				"TitleOfCourtesy, HomePhone FROM Employees";
			SqlConnection con = new SqlConnection(connectionString);
			SqlDataAdapter da = new SqlDataAdapter(sql, con);
			DataSet ds = new DataSet();
        
			// Fill the DataSet.
			da.Fill(ds, "Employees");
			return ds;
		}

		[WebMethod(Description="Returns the full list of employees by city.",
			 MessageName="GetEmployeesByCity")]
		public DataSet GetEmployees(string city)
		{
			// Create the command and the connection.
			string sql = "SELECT EmployeeID, LastName, FirstName, Title, " +
				"TitleOfCourtesy, HomePhone FROM Employees " +
				"WHERE City LIKE '%'+ @City + '%'";
			SqlConnection con = new SqlConnection(connectionString);
			SqlDataAdapter da = new SqlDataAdapter(sql, con);
			da.SelectCommand.Parameters.Add("@City", city);
			DataSet ds = new DataSet();
			        
			// Fill the DataSet.
			da.Fill(ds, "Employees");
			return ds;
		}
	}
}
