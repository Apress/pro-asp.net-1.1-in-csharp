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
	public class ViewStateObjects : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox Name;
		protected System.Web.UI.WebControls.TextBox EmpID;
		protected System.Web.UI.WebControls.TextBox Age;
		protected System.Web.UI.WebControls.TextBox Email;
		protected System.Web.UI.WebControls.TextBox Password;
		protected System.Web.UI.WebControls.Button cmdSave;
		protected System.Web.UI.WebControls.Button cmdDisplay;
		protected System.Web.UI.WebControls.Label lblResults;
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
			this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// This will be created at the beginning of each request.
		Hashtable textToSave = new Hashtable();

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			// Put the text in the Hashtable.
			SaveAllText(Table1.Controls, true);
			
			// Store the entire collection in view state.
			ViewState["TextData"] = textToSave;
		}

		private void SaveAllText(ControlCollection controls, bool saveNested)
		{
			foreach (Control control in controls)
			{
				if (control is TextBox)
				{
					// Add the text to a collection.
					textToSave.Add(control.ID, ((TextBox)control).Text);
				}
				if ((control.Controls != null) && saveNested)
				{
					SaveAllText(control.Controls, true);
				}
			}
		}

		private void cmdDisplay_Click(object sender, System.EventArgs e)
		{
			if (ViewState["TextData"] != null)
			{
				// Retrieve the hashtable.
				Hashtable savedText = (Hashtable)ViewState["TextData"];

				// Display all the text by looping through the hashtable.
				lblResults.Text = "";
				foreach (DictionaryEntry item in savedText)
				{
					lblResults.Text += (string)item.Key + " = " + (string)item.Value + "<br>";
				}
			}
		}
	}
}
