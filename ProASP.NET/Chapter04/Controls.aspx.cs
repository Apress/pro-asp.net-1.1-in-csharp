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

namespace Chapter3
{
	/// <summary>
	/// Summary description for Controls.
	/// </summary>
	public class Controls : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button4;
		protected System.Web.UI.WebControls.Panel MainPanel;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Start examining all the controls.
			DisplayControl(Page.Controls, 0);

			// Add the closing horizontal line.
			Response.Write("<hr>");
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
		
		private void DisplayControl(ControlCollection controls, int depth)
		{
			foreach (Control control in controls)
			{
				// Use the depth parameter to indent the control tree.
				Response.Write(new String('-', depth * 4) + "> ");

				// Display this control.
				Response.Write(control.GetType().ToString() + " - <b>" +
					control.ID + "</b><br>");

				//				if (control is LiteralControl)
				//				{
				//					Response.Write("*** Text: "+((LiteralControl)control).Text + "<br>");
				//				}

				// Check if this control contains more controls.
				if (control.Controls != null)
				{
					DisplayControl(control.Controls, depth + 1);
				}
			}
		}

	}
}
