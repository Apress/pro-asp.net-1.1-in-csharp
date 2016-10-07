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
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputText TextBox1;
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Only perform the initialization the first time the page is requested.
			// After that, this information is tracked in view state.
			if (!Page.IsPostBack)
			{
				// Set the style attributes to configure appearance.
				TextBox1.Style["font-size"] = "20px";
				TextBox1.Style["color"] = "red";
				// Use a slightly different but equivalent syntax
				// for setting a style attribute.
				TextBox1.Style.Add("background-color", "lightyellow");
				
				// Set the default text.
				TextBox1.Value = "<Enter e-mail address here>";

				// Set other non-standard attributes.
				TextBox1.Attributes["onfocus"] = "alert(TextBox1.value)";
  				
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
