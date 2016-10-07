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
using System.Web.Security;

namespace FormsSecurity_UserList
{
	/// <summary>
	/// Summary description for PasswordHasher.
	/// </summary>
	public class PasswordHasher : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button cmdHash;
		protected System.Web.UI.WebControls.RadioButton optSHA1;
		protected System.Web.UI.WebControls.RadioButton optMD5;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtHash;
	
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
			this.cmdHash.Click += new System.EventHandler(this.cmdHash_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdHash_Click(object sender, System.EventArgs e)
		{
			string algorithm = "";

			if (optSHA1.Checked)
			{
				algorithm = "SHA1";
			}
			else
			{
				algorithm = "MD5";
			}

			txtHash.Text = FormsAuthentication.HashPasswordForStoringInConfigFile(
				txtPassword.Text, algorithm);

		}
	}
}
