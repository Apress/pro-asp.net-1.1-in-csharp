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

namespace Chapter18
{
	/// <summary>
	/// Summary description for CurrentUserList.
	/// </summary>
	public class CurrentUserList : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button cmdLogOut;
		protected System.Web.UI.WebControls.Button cmdLogin;
		protected System.Web.UI.WebControls.DataList UsersDataList;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Hashtable users = (Hashtable)Application["CurrentUsers"];
			if (users.Count > 0)
			{
				// Bind the list to the values in the users hashtable.
				UsersDataList.DataSource = users.Values;
			}
			else
			{
				// Bind the list to a string array with a single piece of static text.
				string[] noUsers = {"No users logged in"};
				UsersDataList.DataSource = noUsers;
			}
     		UsersDataList.DataBind();
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
			this.cmdLogOut.Click += new System.EventHandler(this.cmdLogOut_Click);
			this.cmdLogin.Click += new System.EventHandler(this.cmdLogin_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.CurrentUserList_PreRender);

		}
		#endregion

		private void cmdLogin_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Login.aspx");
		}

		private void cmdLogOut_Click(object sender, System.EventArgs e)
		{
			if (Request.IsAuthenticated)
			{
				Session.Abandon();
				FormsAuthentication.SignOut();
				Response.Redirect("CurrentUserList.aspx");
			}
		}

		private void CurrentUserList_PreRender(object sender, System.EventArgs e)
		{
			
		}
	}
}
