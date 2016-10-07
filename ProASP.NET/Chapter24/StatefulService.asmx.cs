using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;

namespace Chapter24
{
	/// <summary>
	/// Summary description for StatefulService.
	/// </summary>
	public class StatefulService : System.Web.Services.WebService
	{
		public StatefulService()
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

		[WebMethod(EnableSession=true)]
		public void StoreName(string name)
		{
			Session["Name"] = name;
		}

		[WebMethod(EnableSession=true)]
		public string GetName()
		{
			if (Session["Name"] == null)
			{
				return "";
			}
			else
			{
			    return (string)Session["Name"];
			}
		}
	}
}
