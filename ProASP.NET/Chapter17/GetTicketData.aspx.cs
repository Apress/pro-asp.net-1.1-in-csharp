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
using System.Text;

namespace Chapter15
{
	/// <summary>
	/// Summary description for GetTicketData.
	/// </summary>
	public class GetTicketData : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			StringBuilder htmlString = new StringBuilder();

			// Has the request been authenticated?
			if (Request.IsAuthenticated)
			{
				// Display generic identity information.
				htmlString.Append("<h3>Generic User Information</h3>");
				htmlString.Append("<b>Name: </b>");
				htmlString.Append(User.Identity.Name);
				htmlString.Append("<br><b>Authenticated With: </b>");
				htmlString.Append(User.Identity.AuthenticationType);
				htmlString.Append("<br><br>");
				
				// Test role membership.
				htmlString.Append("<br><h3>User Role Information</h3>");
				if (User.IsInRole("Administrator"))
				{
					htmlString.Append("This user is an administrator.<br>");
				}
				else
				{
					htmlString.Append("This user is not an administrator.<br>");
				}
				if (User.IsInRole("Supervisor"))
				{
					htmlString.Append("This user is a supervisor.<br>");
				}
				else
				{
					htmlString.Append("This user is not a supervisor.<br>");
				}
				if (User.IsInRole("Guest"))
				{
					htmlString.Append("This user is a guest.<br>");
				}
				else
				{
					htmlString.Append("This user is not a guest.<br>");
				}
				
				

				// Display the information.
				lblInfo.Text=htmlString.ToString();
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
