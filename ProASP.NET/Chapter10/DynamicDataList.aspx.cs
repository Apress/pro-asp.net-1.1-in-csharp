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

namespace Chapter10
{
	/// <summary>
	/// Summary description for DataList1.
	/// </summary>
	public class DynamicDataList : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButton SimpleMode;
		protected System.Web.UI.WebControls.RadioButton ExtendedMode;
		protected System.Web.UI.WebControls.DataList Datalist1;

		string connectionString = "Data Source=localhost;" +
			"Initial Catalog=Northwind;Integrated Security=SSPI";
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// decide which template must be used
			string templateFile = (SimpleMode.Checked ? 
				"DataList_SimpleTemplate.ascx" : "DataList_ExtTemplate.ascx");
			// load the item template
			Datalist1.ItemTemplate = Page.LoadTemplate(templateFile);
    
			// bind to the list 
			BindList();
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


		private void BindList()
		{
			string sql = 
				"SELECT * FROM Employees";

			// Create the Command and the Connection.
			
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand(sql, con);
      
			// Open the connection and get the DataReader.
			con.Open();
			SqlDataReader reader = cmd.ExecuteReader();
           
			// Bind the Reader to the Repeater.
			Datalist1.DataSource = reader;
			Datalist1.DataBind();
      
			// Close the DataReader and the Connection.
			reader.Close();
			con.Close();		
		}
		
	}
}
