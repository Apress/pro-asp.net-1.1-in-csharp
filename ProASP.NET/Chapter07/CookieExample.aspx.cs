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
using System.Net;

namespace StateManagement
{
	/// <summary>
	/// Summary description for CookieExample.
	/// </summary>
	public class CookieExample : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblWelcome;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.Button cmdStore;
		protected System.Web.UI.WebControls.Label Label1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			HttpCookie cookie = Request.Cookies["Preferences"];
			if (cookie == null)
			{
				lblWelcome.Text = "<b>Unknown Customer</b>";
			}
			else
			{
				lblWelcome.Text = "<b>Cookie Found.</b><br><br>";
				lblWelcome.Text += "Welcome, " + cookie["Name"];
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
			this.cmdStore.Click += new System.EventHandler(this.cmdStore_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdStore_Click(object sender, System.EventArgs e)
		{
			HttpCookie cookie = Request.Cookies["Preferences"];
			if (cookie == null)
			{
				cookie = new HttpCookie("Preferences");
			}

			cookie["Name"] = txtName.Text;
			cookie.Expires = DateTime.Now.AddYears(1);
			Response.Cookies.Add(cookie);

			lblWelcome.Text = "<b>Cookie Created.</b><br><br>";
			lblWelcome.Text += "New Customer: " + cookie["Name"];

		}
	}
}
