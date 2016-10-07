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

namespace Chapter19
{
	/// <summary>
	/// Summary description for DynamicUserControls.
	/// </summary>
	public class DynamicUserControls : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList lstControls1;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolder1;
		protected System.Web.UI.WebControls.Label PanelLabel1;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolder2;
		protected System.Web.UI.WebControls.DropDownList DropDownList2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList DropDownList3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.HtmlControls.HtmlGenericControl DIV1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl DIV2;
		protected System.Web.UI.HtmlControls.HtmlGenericControl DIV3;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolder3;
	
	
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

		private void Page_Load(object sender, System.EventArgs e)
		{
			LoadControls(DIV1);
			LoadControls(DIV2);
			LoadControls(DIV3);
		}

		private void LoadControls(Control container)
		{
			DropDownList list = null;
			PlaceHolder ph = null;
			Label lbl = null;

			// Find the controls for this panel.
			foreach (Control ctrl in container.Controls)
			{
				if (ctrl is DropDownList)
				{
					list = (DropDownList)ctrl;
				}
				else if (ctrl is PlaceHolder)
				{
					ph = (PlaceHolder)ctrl;
				}
				else if (ctrl is Label)
				{
					lbl = (Label)ctrl;
				}
			}

			// Load the dynamic content into this panel.
			string ctrlName = list.SelectedItem.Value;
			if (ctrlName.EndsWith(".ascx"))
			{
				ph.Controls.Add(Page.LoadControl(ctrlName));
			}
			lbl.Text = "Loaded..." + ctrlName;
		}
	
	}
}
