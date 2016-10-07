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
using DatabaseComponent;

namespace Chapter11
{
	/// <summary>
	/// Summary description for Master.
	/// </summary>
	public class Master : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid gridMaster;
	
		private NorthwindDB db = new NorthwindDB();

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				Session["SelectedCategory"] = null;
			}
			// Refresh the DataSet every time, even for postbacks.
			// This is not a performance drag, because the DataSet
			// is usuaslly cached.
			gridMaster.DataSource = db.GetCategoriesProductsDataSet();
			gridMaster.DataMember = "Categories";
			gridMaster.DataBind();
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
			this.gridMaster.SelectedIndexChanged += new System.EventHandler(this.gridMaster_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void gridMaster_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Session["SelectedCategory"] = gridMaster.DataKeys[gridMaster.SelectedIndex];

			// Use JavaScript to trigger the redirect in the other window.
			string frameScript = "<script language='javascript'>" +
				"window.parent.frames(1).location='Details.aspx';" + "</script>";
			Page.RegisterStartupScript("FrameScript", frameScript);

		}
	}
}
