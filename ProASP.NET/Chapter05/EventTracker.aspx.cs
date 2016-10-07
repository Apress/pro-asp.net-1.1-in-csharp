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

namespace WebControls
{
	/// <summary>
	/// Summary description for EventTracker.
	/// </summary>
	public class EventTracker : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txt;
		protected System.Web.UI.WebControls.CheckBox chk;
		protected System.Web.UI.WebControls.RadioButton opt1;
		protected System.Web.UI.WebControls.RadioButton opt2;
		protected System.Web.UI.WebControls.ListBox lstEvents;
	
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
			this.txt.TextChanged += new System.EventHandler(this.CtrlChanged);
			this.chk.CheckedChanged += new System.EventHandler(this.CtrlChanged);
			this.opt1.CheckedChanged += new System.EventHandler(this.CtrlChanged);
			this.opt2.CheckedChanged += new System.EventHandler(this.CtrlChanged);

		}
		#endregion

		private void CtrlChanged(Object sender, EventArgs e)
		{
			string ctrlName = ((Control)sender).ID;
			lstEvents.Items.Add(ctrlName + " Changed");

			// Select the last item to scroll the list so the most recent
			// entries are visible.
			lstEvents.SelectedIndex = lstEvents.Items.Count - 1;

		}

	}
}
