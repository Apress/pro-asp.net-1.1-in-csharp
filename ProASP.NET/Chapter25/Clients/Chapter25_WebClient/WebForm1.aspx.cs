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
using Chapter25_WebClient.localhost;

namespace WebClient
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button cmdGetData;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.cmdGetData.Click += new System.EventHandler(this.cmdGetData_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdGetData_Click(object sender, System.EventArgs e)
		{
			// Create the proxy.
			EmployeesService proxy = new EmployeesService();

			// Uncomment these lines to use the tracing utility
			// (described in Chapter 25).
			//Uri newUrl = new Uri(proxy.Url);
			//proxy.Url = newUrl.Scheme + "://" + newUrl.Host + ":8080" + newUrl.AbsolutePath;
			
			// Call the web service and get the results.
			DataSet ds = proxy.GetEmployees();

			// Bind the results.
			DataGrid1.DataSource = ds;
			DataGrid1.DataBind();
		}


	
	}
}
