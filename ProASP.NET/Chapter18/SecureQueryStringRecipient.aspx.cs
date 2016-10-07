using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Security.Cryptography;

namespace Chapter18
{
	/// <summary>
	/// Summary description for SecureQueryStringRecipient.
	/// </summary>
	public class SecureQueryStringRecipient : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			EncryptedQueryString queryString = new EncryptedQueryString(
				(Rijndael)HttpContext.Current.Application["Key"], Request.QueryString["data"]);


			StringBuilder sb = new StringBuilder();
			foreach (DictionaryEntry item in queryString)
			{
				sb.Append("Found ");
				sb.Append(item.Key);
				sb.Append(" = ");
				sb.Append(item.Value);
				sb.Append("<br>");
			}
			lblInfo.Text = sb.ToString();

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
