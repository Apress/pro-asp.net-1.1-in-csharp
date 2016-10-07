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
	/// Summary description for ListControls.
	/// </summary>
	public class ListControls : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ListBox Listbox1;
		protected System.Web.UI.WebControls.DropDownList DropdownList1;
		protected System.Web.UI.WebControls.CheckBoxList CheckboxList1;
		protected System.Web.UI.WebControls.RadioButtonList RadiobuttonList1;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				for (int i=3; i<=5; i++)
				{
					Listbox1.Items.Add("Option " + i.ToString());
					DropdownList1.Items.Add("Option " + i.ToString());
					CheckboxList1.Items.Add("Option " + i.ToString());
					RadiobuttonList1.Items.Add("Option " + i.ToString());
				} 
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Write("<b>Selected items for Listbox1:</b><br>");
			foreach (ListItem li in Listbox1.Items)
			{
				if (li.Selected) Response.Write("- " + li.Text + "<br>");
			}
    
			Response.Write("<b>Selected item for DropdownList1:</b><br>");
			Response.Write("- " + DropdownList1.SelectedItem.Text + "<br>");
    
			Response.Write("<b>Selected items for CheckboxList1:</b><br>");
			foreach (ListItem li in CheckboxList1.Items)
			{
				if (li.Selected) Response.Write("- " + li.Text + "<br>");
			}
    
			Response.Write("<b>Selected item for RadiobuttonList1:</b><br>");
			Response.Write("- " + RadiobuttonList1.SelectedItem.Text + "<br>");
		}
	}
}
