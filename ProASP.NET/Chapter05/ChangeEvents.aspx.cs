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
	/// Summary description for ChangeEvents.
	/// </summary>
	public class ChangeEvents : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlSelect List1;
		protected System.Web.UI.HtmlControls.HtmlInputText Textbox1;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox Checkbox1;
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				List1.Items.Add("Option 3");
				List1.Items.Add("Option 4");
				List1.Items.Add("Option 5");
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
			this.List1.ServerChange += new System.EventHandler(this.List1_ServerChange);
			this.Textbox1.ServerChange += new System.EventHandler(this.Ctrl_ServerChange);
			this.Checkbox1.ServerChange += new System.EventHandler(this.Ctrl_ServerChange);
			this.Submit1.ServerClick += new System.EventHandler(this.Submit1_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Submit1_ServerClick(object sender, System.EventArgs e)
		{
			Response.Write("<li>ServerClick detected for Submit1.</li>");
		}

		private void Ctrl_ServerChange(object sender, System.EventArgs e)
		{
			Response.Write("<li>ServerChange detected for " + 
				((Control)sender).ID + "</li>");
		}

		private void List1_ServerChange(object sender, System.EventArgs e)
		{
			Response.Write("<li>ServerChange detected for List1. " + 
				"The selected items are:</li><br>");    
			foreach (ListItem li in List1.Items)
			{
				if (li.Selected)
					Response.Write("&nbsp;&nbsp;- " + li.Value + "<br>");    
			}

		}


	}
}
