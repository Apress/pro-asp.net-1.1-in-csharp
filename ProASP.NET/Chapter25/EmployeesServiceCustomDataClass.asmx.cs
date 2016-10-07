using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using DatabaseComponent;

namespace Chapter24
{
	/// <summary>
	/// Summary description for EmployeesServiceCustomDataClass.
	/// </summary>
	public class EmployeesServiceCustomDataClass : System.Web.Services.WebService
	{
		public EmployeesServiceCustomDataClass()
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

		[WebMethod(Description="Returns the full list of employees.")]
		public EmployeeDetails[] GetEmployees()
		{
			EmployeeDB db = new EmployeeDB();
			return db.GetEmployees();
		}
	}
}
