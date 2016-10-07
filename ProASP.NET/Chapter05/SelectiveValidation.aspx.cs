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

namespace Recipe07_10
{
	/// <summary>
	/// Summary description for SelectiveValidation.
	/// </summary>
	public class SelectiveValidation : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtNumber;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.Label lblCustomSummary;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator1;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator2;
		protected System.Web.UI.WebControls.Button cmdValidate;
	
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
			this.cmdValidate.Click += new System.EventHandler(this.cmdValidate_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdValidate_Click(object sender, System.EventArgs e)
		{
			// Validate the page.
			this.Validate();
			
			if (!Page.IsValid)
			{
				lblCustomSummary.Text = "";
				foreach (BaseValidator validator in this.Validators)
				{
					if (!validator.IsValid)
					{
						TextBox invalidControl = (TextBox)this.FindControl(validator.ControlToValidate);
						lblCustomSummary.Text +=
							"The page contains the following error: <b>" +
							validator.ErrorMessage + "</b>.<br>" +
							"The invalid input is: <b>" +
							invalidControl.Text + "</b>." + "<br><br>";
					}
				}
			}
			else
			{
				lblCustomSummary.Text = "Validation succeeded.";
			}
		}
	}
}
