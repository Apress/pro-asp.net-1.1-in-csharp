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
using System.Xml;
using System.Data.SqlClient;
using System.Text;

namespace Chapter12
{
	/// <summary>
	/// Summary description for XmlQuery.
	/// </summary>
	public class XmlQuery : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Literal XmlText;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string connectionString = "Data Source=localhost;" +
				"Initial Catalog=Northwind;Integrated Security=SSPI";

			// Define the command.
			string customerQuery =
			"SELECT FirstName, LastName FROM Employees FOR XML AUTO, ELEMENTS";
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand com = new SqlCommand(customerQuery, con);

			// Execute the command.
			StringBuilder str = new StringBuilder();
			try 
			{
				con.Open();
				XmlReader reader = com.ExecuteXmlReader();
				
				while (reader.Read()) 
				{
					// Process each employee.
					if ((reader.Name == "Employees") && (reader.NodeType == XmlNodeType.Element))
					{
						reader.ReadStartElement("Employees");
						str.Append(reader.ReadElementString("FirstName"));
						str.Append(" ");
						str.Append(reader.ReadElementString("LastName"));
						str.Append("<br>");
						reader.ReadEndElement();
					}
				}
				reader.Close();
			}
			finally 
			{
				con.Close();
			}
			XmlText.Text = str.ToString();

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
