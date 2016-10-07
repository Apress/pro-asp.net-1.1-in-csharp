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

namespace Chapter18
{
	/// <summary>
	/// Summary description for GetSetCreditCardNumber.
	/// </summary>
	public class GetSetCreditCardNumber : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtCreditCard;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button cmdSet;
		protected System.Web.UI.WebControls.Button cmdGet;
	
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
			this.cmdGet.Click += new System.EventHandler(this.cmdGet_Click);
			this.cmdSet.Click += new System.EventHandler(this.cmdSet_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdSet_Click(object sender, System.EventArgs e)
		{
			// Rather than recreate this credentials object, you could store it in application state.
			UsersDB db = new UsersDB("CredentialConnectionString");
			db.SetCreditCard(User.Identity.Name, txtCreditCard.Text);
		}

		private void cmdGet_Click(object sender, System.EventArgs e)
		{
			// Rather than recreate this credentials object, you could store it in application state.
			UsersDB db = new UsersDB("CredentialConnectionString");
			txtCreditCard.Text = db.GetCreditCard(User.Identity.Name);
		}
	}
}
