using System;
using System.Data;
using System.Data.SqlClient;

public class GenerateSchema
{
	static string connectionString = "Data Source=localhost;" +
		"Initial Catalog=Northwind;Integrated Security=SSPI";

	static string categorySQL = "SELECT * FROM Categories";
	static string productSQL = "SELECT * FROM Products";

	public static void Main() 
	{

		// Create ADO.NET objects.
		SqlConnection con = new SqlConnection(connectionString);
		SqlCommand com = new SqlCommand(categorySQL, con);
		SqlDataAdapter adapter = new SqlDataAdapter(com);
		DataSet ds = new DataSet("NorthwindDataSet");

		// Execute the command.
		try
		{
			con.Open();
			adapter.FillSchema(ds, SchemaType.Mapped, "Categories");

			// Modify the command and re-execute it.
			adapter.SelectCommand.CommandText = productSQL;
			adapter.FillSchema(ds, SchemaType.Mapped, "Products");
		}
		catch (Exception err)
		{
			Console.WriteLine(err.ToString());
		}
		finally
		{
			con.Close();
		}
		ds.WriteXmlSchema("c:\\Northwind.xsd");
	}
}
