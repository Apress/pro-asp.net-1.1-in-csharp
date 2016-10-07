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
	[WebService (Name="Employees Service",
		 Description="Retrieve the Northwind Employees",
		 Namespace="http://www.apress.com/ProASP.NET/")]
	public class EmployeesServiceCached : System.Web.Services.WebService
	{
		public EmployeesServiceCached()
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
     
		[WebMethod(Description="Returns the full list of employees.")]
		public DataSet GetEmployees()
		{
			return GetEmployeesDataSet();
		}

		[WebMethod(Description="Returns the full list of employees by city.",
			 MessageName="GetEmployeesByCity")]
		public DataSet GetEmployees(string city)
		{
			// Copy the DataSet.
			DataSet dsFiltered = GetEmployeesDataSet().Copy();

			// Remove the rows manually.
			// This is a good approach (rather than using the
			// DataTable.Select() method) because it is impervious
			// to SQL injection attacks.
			foreach (DataRow row in dsFiltered.Tables[0].Rows)
			{
				// Perform a case-insensitive compare.
				if (String.Compare(row["City"].ToString(), city.ToUpper(), true) != 0)
				{
					row.Delete();
				}
			}

			// Remove these rows permanently.
			dsFiltered.AcceptChanges();

			return dsFiltered;
		}

		private DataSet GetEmployeesDataSet()
		{
			DataSet ds;

			if (Context.Cache["EmployeesDataSet"] != null)
			{
				// Retrieve it from the cache
				ds = (DataSet)Context.Cache["EmployeesDataSet"];
			}
			else
			{
				// Retrieve it from the database.
				string sql = "SELECT EmployeeID, LastName, FirstName, Title, " +
					"TitleOfCourtesy, HomePhone, City FROM Employees";
				SqlConnection con = new SqlConnection(connectionString);
				SqlDataAdapter da = new SqlDataAdapter(sql, con);
				ds = new DataSet();
				da.Fill(ds, "Employees");

				// Track when the DataSet was created. You can
				// retrieve this information in your client to test
				// that caching is working.
				ds.ExtendedProperties.Add("CreatedDate", DateTime.Now);

				// Store it in the cache for ten minutes.
				Context.Cache.Insert("EmployeesDataSet", ds, null, 
				 DateTime.Now.AddMinutes(10), TimeSpan.Zero);
			}
			return ds;
		}
	}
}
