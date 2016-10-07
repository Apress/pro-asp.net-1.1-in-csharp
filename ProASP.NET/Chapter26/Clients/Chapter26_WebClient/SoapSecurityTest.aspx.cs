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
using Chapter26_WebClient.SoapSecurity;

namespace SecurityTest
{
	/// <summary>
	/// Summary description for SoapSecurityTest.
	/// </summary>
	public class SoapSecurityTest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.TextBox txtUserName;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label lblInfo;
		protected System.Web.UI.WebControls.Button cmdCall;
	
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
			this.cmdCall.Click += new System.EventHandler(this.cmdCall_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdCall_Click(object sender, System.EventArgs e)
		{
			SoapSecurityService proxy = new SoapSecurityService();
			
			try
			{
				proxy.Login(txtUserName.Text, txtPassword.Text, HashAlgorithm.SHA1);
				DataGrid1.DataSource = proxy.GetEmployees();
				DataGrid1.DataBind();
				lblInfo.Text = "";
			}
			catch (Exception err)
			{
				lblInfo.Text = err.Message;
			}
		}
	}
}
