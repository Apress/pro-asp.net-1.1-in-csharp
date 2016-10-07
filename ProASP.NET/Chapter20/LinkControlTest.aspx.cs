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
using System.IO;

namespace CustomServerControls
{
	/// <summary>
	/// Summary description for LinkControlTest.
	/// </summary>
	public class LinkControlTest : System.Web.UI.Page
	{
		protected CustomServerControlsLibrary.LinkWebControl LinkWebControl1;
		protected System.Web.UI.WebControls.Label lblHtml;
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			// Create the in-memory objects that will catch the rendered output.
			StringWriter writer = new StringWriter();
			HtmlTextWriter output = new HtmlTextWriter(writer);

			// Render the control.
			LinkWebControl1.RenderControl(output);

			// Display the HTML (and encode it properly so that
			// it appears as text in the browser).
			lblHtml.Text = "The HTML for LinkWebControl1 is<br><blockquote>" +
			Server.HtmlEncode(writer.ToString()) + "</blockquote>";
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
