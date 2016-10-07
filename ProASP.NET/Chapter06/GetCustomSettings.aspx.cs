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
using System.Configuration;
using System.Collections.Specialized;
using ConfigurationExtension;

namespace Chapter06
{
	/// <summary>
	/// Summary description for GetCustomSettings.
	/// </summary>
	public class GetCustomSettings : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			DbConnectionConfigSection[] connections = (DbConnectionConfigSection[])Context.GetConfig("system.web/DatabaseConnections");
			
			foreach (DbConnectionConfigSection con in connections)
			{
				lblInfo.Text += "Retrieved a connection with...<br>" +
					"<b>Connection:</b> " + con.ConnectionString + 
					"<br><b>Table:</b> " + con.TableName + "<br><br>";
			}
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
