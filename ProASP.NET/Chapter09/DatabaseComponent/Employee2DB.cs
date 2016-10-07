using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace DatabaseComponent
{
	public class Employee2DB
	{
		private string connectionString;

		public Employee2DB()
		{
			// Get connection string from web.config.
			connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
		}

		public DataTable GetAllEmployees()
		{
			string sql = "SELECT EmployeeID, TitleOfCourtesy, LastName, FirstName FROM Employees2";
			SqlConnection con = new SqlConnection(connectionString);
			SqlDataAdapter da = new SqlDataAdapter(sql, con);
			DataSet ds = new DataSet();
        
			// Fill the DataSet.
			try
			{
				da.Fill(ds, "Employees");
			}
			catch
			{
				throw new ApplicationException("Data error.");
			}
			return ds.Tables["Employees"];
		}

		public DataTable UpdateEmployeeBatch(DataTable dt)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlDataAdapter da = new SqlDataAdapter();
			
			// Create the Insert command, and set its parameters.
			da.InsertCommand = new SqlCommand("InsertEmployee2", con);
			da.InsertCommand.CommandType = CommandType.StoredProcedure;
			
			SqlParameter insParam1 = new SqlParameter("@TitleOfCourtesy", SqlDbType.NVarChar, 25);
			insParam1.SourceVersion = DataRowVersion.Current;
			insParam1.SourceColumn = "TitleOfCourtesy";
			
			SqlParameter insParam2 = new SqlParameter("@LastName", SqlDbType.NVarChar, 20);
			insParam2.SourceVersion = DataRowVersion.Current;
			insParam2.SourceColumn = "LastName";
			
			SqlParameter insParam3 = new SqlParameter("@FirstName", SqlDbType.NVarChar, 10);
			insParam3.SourceVersion = DataRowVersion.Current;
			insParam3.SourceColumn = "FirstName";

			SqlParameter insParam4 = new SqlParameter("@EmployeeID", SqlDbType.Int, 4);
			insParam4.SourceColumn = "EmployeeID";
			insParam4.Direction = ParameterDirection.Output;
			
			// Add the parameters.
			da.InsertCommand.Parameters.Add(insParam1);    
			da.InsertCommand.Parameters.Add(insParam2);
			da.InsertCommand.Parameters.Add(insParam3);
			da.InsertCommand.Parameters.Add(insParam4);
    

			// Create the Update command, and set its parameters.
			da.UpdateCommand = new SqlCommand("UpdateEmployee2", con);
			da.UpdateCommand.CommandType = CommandType.StoredProcedure;
			
			SqlParameter updParam1 = new SqlParameter("@EmployeeID", SqlDbType.Int, 4);
			updParam1.SourceVersion = DataRowVersion.Original;
			updParam1.SourceColumn = "EmployeeID";
			
			SqlParameter updParam2 = new SqlParameter("@TitleOfCourtesy", SqlDbType.NVarChar, 25);
			updParam2.SourceVersion = DataRowVersion.Current;
			updParam2.SourceColumn = "TitleOfCourtesy";
			
			SqlParameter updParam3 = new SqlParameter("@LastName", SqlDbType.NVarChar, 20);
			updParam3.SourceVersion = DataRowVersion.Current;
			updParam3.SourceColumn = "LastName";
			
			SqlParameter updParam4 = new SqlParameter("@FirstName", SqlDbType.NVarChar, 10);
			updParam4.SourceVersion = DataRowVersion.Current;
			updParam4.SourceColumn = "FirstName";
			
			// Add the parameters.
			da.UpdateCommand.Parameters.Add(updParam1);    
			da.UpdateCommand.Parameters.Add(updParam2);
			da.UpdateCommand.Parameters.Add(updParam3);
			da.UpdateCommand.Parameters.Add(updParam4);


			// Create the Delete command, and set its parameters.
			da.DeleteCommand = new SqlCommand("DeleteEmployee2", con);
			da.DeleteCommand.CommandType = CommandType.StoredProcedure;
			
			SqlParameter delParam1 = new SqlParameter("@EmployeeID", SqlDbType.Int, 4);
			delParam1.SourceVersion = DataRowVersion.Original;
			delParam1.SourceColumn = "EmployeeID";
			
			// Add the parameter.
			da.DeleteCommand.Parameters.Add(delParam1);    
     
			// Update the data source.
			try
			{
				da.Update(dt);
			}
			catch
			{
				throw new ApplicationException("Data error.");
			}
			return dt;
		}
	}
}

