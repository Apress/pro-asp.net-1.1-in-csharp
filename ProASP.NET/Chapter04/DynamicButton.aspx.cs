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

namespace Chapter04
{
	/// <summary>
	/// Summary description for DynamicButton.
	/// </summary>
	public class DynamicButton : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolder1;
		protected System.Web.UI.WebControls.Button cmdReset;
		protected System.Web.UI.WebControls.Button cmdRemove;
		protected System.Web.UI.WebControls.Button cmdCreate;
		protected System.Web.UI.WebControls.Panel Panel1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create a new button object.
			Button newButton = new Button();

			// Assign some text and an ID so you can retrieve it later.
			newButton.Text = "* Dynamic Button *";
			newButton.ID = "newButton";

			// Attach an event handler to the Button.Click event.
			newButton.Click += new System.EventHandler(this.Button_Click);

			// Add the putton to a placeholder.
			PlaceHolder1.Controls.Add(newButton);
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
			this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
			this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
			this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		private void Button_Click(object sender, System.EventArgs e)
		{
			Label1.Text = "You clicked the dynamic button.";
		}

		private void cmdReset_Click(object sender, System.EventArgs e)
		{
			Label1.Text = "(No value.)";
		}

		private void cmdRemove_Click(object sender, System.EventArgs e)
		{
			// Search for the button, no matter what level it's at.
			Button foundButton = (Button)Page.FindControl("newButton");

			// Remove the button.
			if (foundButton != null)
			{
				foundButton.Parent.Controls.Remove(foundButton);
			}
		}

		private void cmdCreate_Click(object sender, System.EventArgs e)
		{
			// (Button is automatically created in postback.)
		}
	}
}
