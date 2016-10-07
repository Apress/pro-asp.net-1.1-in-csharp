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
using System.Data.SqlClient;

namespace Chapter20
{
	/// <summary>
	/// Summary description for DbDropDownHost.
	/// </summary>
	public class DbDropDownHost : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button submit;
		protected CustomServerControlsLibrary.DbDropDown dropdownSample;
		protected System.Web.UI.WebControls.Label Message;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			if(!IsPostBack)
			{SqlConnection
				 cnn = new SqlConnection("server=(local);database=northwind;uid=sa;pwd=;");
				SqlCommand cmd = new SqlCommand("Select customerid, contactname from customers",cnn);
				SqlDataReader rdr;
     
				cnn.Open();
				rdr=cmd.ExecuteReader(CommandBehavior.CloseConnection);
     
				dropdownSample.DataSource=rdr;
				dropdownSample.DataTextField = "contactname";
				dropdownSample.DataValueField="customerid";
     
				DataBind();
				rdr.Close();
				cnn.Dispose();
				cmd.Dispose();
			}
    
			dropdownSample.SelectedIndexChanged+= new EventHandler(dropdownSample_SelectedIndexChanged);
		}
  
		public void dropdownSample_SelectedIndexChanged(object sender, EventArgs e)
		{
			Message.Text = "Index Changed";
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
