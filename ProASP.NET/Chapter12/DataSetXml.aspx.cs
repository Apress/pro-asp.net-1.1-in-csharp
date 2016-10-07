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
using System.Xml;

namespace Chapter12
{
	/// <summary>
	/// Summary description for DataSetXml.
	/// </summary>
	public class DataSetXml : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.DataGrid Datagrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// create the connection, DataAdapter and DataSet
			string connString = "Data Source=localhost;database=Northwind;Integrated Security=SSPI";
			string sql = "SELECT TOP 5 EmployeeID, TitleOfCourtesy, LastName, FirstName FROM Employees";
			SqlConnection conn = new SqlConnection(connString);
			SqlDataAdapter da = new SqlDataAdapter(sql, conn);
			DataSet ds = new DataSet();
      
			// Fill the DataSet and fill the first grid.
			da.Fill(ds, "Employees");
			Datagrid1.DataSource = ds.Tables["Employees"];
			Datagrid1.DataBind();
    
			// Generate the XML file.
			string xmlFile = Server.MapPath("Employees.xml");
			ds.WriteXml(xmlFile, XmlWriteMode.WriteSchema);
    
			// Load the XML file.
			DataSet dsXml = new DataSet();
			dsXml.ReadXml(xmlFile);
			// Fill the second grid.
			Datagrid2.DataSource = dsXml.Tables["Employees"];
			Datagrid2.DataBind();
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
