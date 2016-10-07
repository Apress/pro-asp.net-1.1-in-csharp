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

namespace Chapter10
{
	/// <summary>
	/// Summary description for RepeatedValueBinding.
	/// </summary>
	public class RepeatedValueBinding : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ListBox Listbox1;
		protected System.Web.UI.WebControls.DropDownList DropdownList1;
		protected System.Web.UI.WebControls.RadioButtonList OptionList1;
		protected System.Web.UI.WebControls.CheckBoxList CheckList1;
		protected System.Web.UI.WebControls.Button cmdGetSelection;
		protected System.Web.UI.WebControls.Literal Result;
		protected System.Web.UI.HtmlControls.HtmlSelect Select1;
		protected System.Web.UI.HtmlControls.HtmlSelect Select2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				// create the data source
				Hashtable ht = new Hashtable(3);
				ht.Add("Lasagna", "Key1");
				ht.Add("Spaghetti", "Key2");
				ht.Add("Pizza", "Key3");
      
				// set the controls' DataSource property
				Select1.DataSource = ht;
				Select2.DataSource = ht;
				Listbox1.DataSource = ht;
				DropdownList1.DataSource = ht;
				CheckList1.DataSource = ht;
				OptionList1.DataSource = ht;
      
				// bind the data to all the control
				Page.DataBind();
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
			this.cmdGetSelection.Click += new System.EventHandler(this.cmdGetSelection_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdGetSelection_Click(object sender, System.EventArgs e)
		{
			Result.Text += "- Item selected in Select1: " + Select1.Items[Select1.SelectedIndex].Text + " - " + Select1.Value + "<br>";
			Result.Text += "- Item selected in Select2: " + Select2.Items[Select2.SelectedIndex].Text + " - " + Select2.Value + "<br>";
			Result.Text += "- Item selected in Listbox1: " + Listbox1.SelectedItem.Text + " - " + Listbox1.SelectedItem.Value + "<br>";
			Result.Text += "- Item selected in DropdownList1: " + DropdownList1.SelectedItem.Text + " - " + DropdownList1.SelectedItem.Value + "<br>";
			Result.Text += "- Item selected in OptionList1: " + OptionList1.SelectedItem.Text + " - " + OptionList1.SelectedItem.Value + "<br>";
			Result.Text += "- Items selected in CheckList1: ";
			foreach (ListItem li in CheckList1.Items)
			{
				if (li.Selected)
					Result.Text += li.Text + " - " + li.Value + " ";
			}		
		}
	}
}
