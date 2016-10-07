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
	/// Summary description for SingleValueBinding.
	/// </summary>
	public class SingleValueBinding : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.HyperLink Hyperlink1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden LogoPath;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.DataBind();
		}

		protected string GetFilePath() 
		{
			return "apress.gif";
		}
  
		protected string FilePath
		{ 
			get { return "apress.gif"; }
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
