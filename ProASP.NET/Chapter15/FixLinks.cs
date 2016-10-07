using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Chapter15
{
	/// <summary>
	/// Summary description for FixLinks.
	/// </summary>
	public class FixLinks
	{
		public static void UpdateHtmlAnchors(ControlCollection controls)
		{
			// Scan the page.
			foreach (Control control in controls)
			{
				// Update all HtmlAnchor objects
				if (control is HtmlAnchor)
				{
					HtmlAnchor anchor = (HtmlAnchor)control;
					anchor.HRef = UpdateUrl(anchor.HRef);
				}
				else if (control is HyperLink)
				{
					HyperLink link = (HyperLink)control;
					link.NavigateUrl = UpdateUrl(link.NavigateUrl);
				}


				// Search the control tree recursively.
				if (control.Controls != null)
				{
					UpdateHtmlAnchors(control.Controls);
				}
			}
		}

		public static string UpdateUrl(string targetUrl)
		{
			HttpContext context = HttpContext.Current;
			if (context.Request.QueryString[
				FormsAuthentication.FormsCookieName] != null)
			{
				// There is an authentication ticket. Add it.
				if (targetUrl.IndexOf("?") == -1)
				{
					targetUrl += "?" + FormsAuthentication.FormsCookieName +
						"=" + context.Request.QueryString[
						FormsAuthentication.FormsCookieName];
				}
				else
				{
					targetUrl += "&" + FormsAuthentication.FormsCookieName + 
						"=" + context.Request.QueryString[
						FormsAuthentication.FormsCookieName];
				}
			}
			return targetUrl;
		}
	}
}
