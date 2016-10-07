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

namespace Chapter05
{
	/// <summary>
	/// Summary description for Validators.
	/// </summary>
	public class ViewStateTest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox Name;
		protected System.Web.UI.WebControls.TextBox EmpID;
		protected System.Web.UI.WebControls.TextBox DayOff;
		protected System.Web.UI.WebControls.TextBox Age;
		protected System.Web.UI.WebControls.TextBox Email;
		protected System.Web.UI.WebControls.TextBox Password;
		protected System.Web.UI.WebControls.Button cmdSave;
		protected System.Web.UI.WebControls.Button cmdRestore;
		protected System.Web.UI.WebControls.Table Table1;
	
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
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			this.cmdRestore.Click += new System.EventHandler(this.cmdRestore_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			// Save the current text.
			SaveAllText(Table1.Controls, true);
		}

		private void SaveAllText(ControlCollection controls, bool saveNested)
		{
			foreach (Control control in controls)
			{
				if (control is TextBox)
				{
					// Store the text using the unique control ID.
					ViewState[control.ID] = ((TextBox)control).Text;
				}

				if ((control.Controls != null) && saveNested)
				{
					SaveAllText(control.Controls, true);
				}
			}
		}

		private void cmdRestore_Click(object sender, System.EventArgs e)
		{
			// Retrieve the last saved text.
			RestoreAllText(Table1.Controls, true);
		}

		private void RestoreAllText(ControlCollection controls, bool saveNested)
		{
			foreach (Control control in controls)
			{
				if (control is TextBox)
				{
					if (ViewState[control.ID] != null)
					  ((TextBox)control).Text = (string)ViewState[control.ID];
				}

				if ((control.Controls != null) && saveNested)
				{
					RestoreAllText(control.Controls, true);
				}
			}
		}
	}
}
