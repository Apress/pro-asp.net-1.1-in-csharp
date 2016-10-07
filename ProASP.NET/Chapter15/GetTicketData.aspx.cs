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
				
				// Was forms authentication used?
				if (User.Identity is FormsIdentity)
				{
					// Get the ticket.
					FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;

					htmlString.Append("<h3>Ticket User Information</h3>");
					htmlString.Append("<b>Name: </b>");
					htmlString.Append(ticket.Name);
					htmlString.Append("<br><b>Issued at: </b>");
					htmlString.Append(ticket.IssueDate);
					htmlString.Append("<br><b>Expires at: </b>");
					htmlString.Append(ticket.Expiration);
					htmlString.Append("<br><b>Cookie version: </b>");
					htmlString.Append(ticket.Version);
					htmlString.Append("<br><b>User data: </b>");
					htmlString.Append(ticket.UserData);
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
