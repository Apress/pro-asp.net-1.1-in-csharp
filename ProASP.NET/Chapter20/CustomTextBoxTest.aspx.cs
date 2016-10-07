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

namespace Chapter20
{
	/// <summary>
	/// Summary description for CustomTextBoxTest.
	/// </summary>
	public class CustomTextBoxTest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected CustomServerControlsLibrary.CustomTextBox CustomTextBox1;
		protected System.Web.UI.WebControls.Label lblInfo;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
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
			this.CustomTextBox1.TextChanged += new System.EventHandler(this.CustomTextBox1_TextChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void CustomTextBox1_TextChanged(object sender, System.EventArgs e)
		{
			lblInfo.Text = "Change event raised for CustomTextBox." +
				"<br>The new text is <b>" + CustomTextBox1.Text + "</b>";
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
		
		}

		
		
	}
}
