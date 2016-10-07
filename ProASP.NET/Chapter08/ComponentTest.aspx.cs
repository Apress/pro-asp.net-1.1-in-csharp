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
using DatabaseComponent;

namespace Chapter08
{
	/// <summary>
	/// Summary description for Command3.
	/// </summary>
	public class Command3 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Literal HtmlContent;

		// Create the database component.
		private EmployeeDB db = new EmployeeDB();

		private void Page_Load(object sender, System.EventArgs e)
		{
			WriteEmployeesList();
    
			int empID = db.InsertEmployee(
				new EmployeeDetails(0, "Mr.", "Bellinaso", "Marco"));
			HtmlContent.Text += "<br>Inserted 1 employee.<br>";

			WriteEmployeesList();
    
			db.DeleteEmployee(empID);
			HtmlContent.Text += "<br>Deleted 1 employee.<br>";

			WriteEmployeesList();
		}
  
  
		private void WriteEmployeesList()
		{	
			StringBuilder htmlStr = new StringBuilder("");

			int numEmployees = db.CountEmployees();
			htmlStr.Append("<br>Total employees: <b>");
			htmlStr.Append(numEmployees.ToString());
			htmlStr.Append("</b><br><br>");

			EmployeeDetails[] employees = db.GetEmployees();
			foreach (EmployeeDetails emp in employees)
			{
    			htmlStr.Append("<li>");
				htmlStr.Append(emp.EmployeeID);
				htmlStr.Append(" ");
				htmlStr.Append(emp.TitleOfCourtesy);
				htmlStr.Append(" <b>");
				htmlStr.Append(emp.FirstName);
				htmlStr.Append("</b>, ");
				htmlStr.Append(emp.LastName);
				htmlStr.Append("</li>");
			}
			htmlStr.Append("<br>");
			HtmlContent.Text += htmlStr.ToString();
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
