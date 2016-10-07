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
	public class Validators : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.CheckBox EnableValidators;
		protected System.Web.UI.WebControls.CheckBox EnableClientSide;
		protected System.Web.UI.WebControls.CheckBox ShowSummary;
		protected System.Web.UI.WebControls.CheckBox ShowMsgBox;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSum;
		protected System.Web.UI.WebControls.TextBox Name;
		protected System.Web.UI.WebControls.RequiredFieldValidator ValidateName;
		protected System.Web.UI.WebControls.RegularExpressionValidator ValidateName2;
		protected System.Web.UI.WebControls.TextBox EmpID;
		protected System.Web.UI.WebControls.RequiredFieldValidator ValidateEmpID;
		protected System.Web.UI.WebControls.CustomValidator ValidateEmpID2;
		protected System.Web.UI.WebControls.TextBox DayOff;
		protected System.Web.UI.WebControls.RequiredFieldValidator ValidateDayOff;
		protected System.Web.UI.WebControls.RangeValidator ValidateDayOff2;
		protected System.Web.UI.WebControls.TextBox Age;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.CompareValidator ValidateAge;
		protected System.Web.UI.WebControls.TextBox Email;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator2;
		protected System.Web.UI.WebControls.RegularExpressionValidator ValidateEmail;
		protected System.Web.UI.WebControls.TextBox Password;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator3;
		protected System.Web.UI.WebControls.TextBox Password2;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator4;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator1;
		protected System.Web.UI.WebControls.Table Table1;
		protected System.Web.UI.WebControls.Button Submit;
		protected System.Web.UI.WebControls.Label Result;
	
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
			this.Submit.Click += new System.EventHandler(this.Submit_Click);
			this.EnableValidators.CheckedChanged += new System.EventHandler(this.Options_Changed);
			this.EnableClientSide.CheckedChanged += new System.EventHandler(this.Options_Changed);
			this.ShowSummary.CheckedChanged += new System.EventHandler(this.Options_Changed);
			this.ShowMsgBox.CheckedChanged += new System.EventHandler(this.Options_Changed);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Options_Changed(object sender, System.EventArgs e)
		{
			foreach (BaseValidator valCtl in Page.Validators)
			{
				valCtl.Enabled=EnableValidators.Checked;
				valCtl.EnableClientScript = EnableClientSide.Checked;
			}
			ValidationSum.ShowMessageBox = ShowMsgBox.Checked;
			ValidationSum.ShowSummary = ShowSummary.Checked;
		}

		private void Submit_Click(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
				Result.Text = "Thanks for sending your data";
			else
				Result.Text = "There are some errors, please correct them and re-send the form.";
		}

		private void EmpIDServerValidate(object sender, ServerValidateEventArgs args)
		{
			try 
			{
				args.IsValid = (int.Parse(args.Value)%5 == 0);
			}
			catch
			{
				args.IsValid = false;
			}
		}
	}
}
