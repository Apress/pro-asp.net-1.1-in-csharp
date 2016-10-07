using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;

namespace Chapter06 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
           
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
		
		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{
		
		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
		
		}

//		DateTime createdAt = DateTime.Now;
//		protected void Application_Error(Object sender, EventArgs e)
//		{
//			Response.Write("<font face=\"Tahoma\" size=\"2\" color=\"red\">");
//			Response.Write("Oops! Looks like an error occurred!!<hr></font>");
//			Response.Write("<font face=\"Arial\" size=\"2\">");
//			Response.Write(Server.GetLastError().Message.ToString());
//			Response.Write("<hr>"+Server.GetLastError().ToString());
//			Response.Write("<br>This application object created: " + createdAt.ToString());
//			Server.ClearError();
//		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{
			Response.Write("Application ended.<br>");
		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			// 
			// Global
			// 
			

		}
		#endregion



	}
}

