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
using System.Security.Cryptography;

namespace Chapter18
{
	/// <summary>
	/// Summary description for SecureQueryStringSender.
	/// </summary>
	public class SecureQueryStringSender : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtData;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button cmdOK;
	
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
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			EncryptedQueryString queryString = new EncryptedQueryString((Rijndael)HttpContext.Current.Application["Key"]);
			queryString["testValue1"] = "This is a sample string.";
			queryString["testValue2"] = "6171742";
			queryString["TextBox"] = txtData.Text;

			// Note that when redirecting, all the values become a single
			// encrypted query string argument.
			Response.Redirect("SecureQueryStringRecipient.aspx?data=" + queryString.ToString());
		}
	}
}
