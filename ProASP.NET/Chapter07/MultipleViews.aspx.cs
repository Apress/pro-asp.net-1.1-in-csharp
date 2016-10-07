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
using System.Data.OleDb;

namespace DataCaching
{
	/// <summary>
	/// Summary description for MultipleViews.
	/// </summary>
	public class MultipleViews : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblCacheStatus;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.CheckBoxList chkColumns;
		protected System.Web.UI.WebControls.DataGrid gridPubs;
		protected System.Web.UI.WebControls.Button cmdApply;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				Cache.Remove("Titles");
                
				DataSet dsPubs;
				if (Cache["Titles"] == null)
				{
					dsPubs = RetrieveData();
					Cache.Insert("Titles", dsPubs, null, DateTime.MaxValue, TimeSpan.FromMinutes(2));
					lblCacheStatus.Text = "Created and added to cache.";
				}
				else
				{
					dsPubs = (DataSet)Cache["Titles"];
					lblCacheStatus.Text = "Retrieved from cache.";
				}
				chkColumns.DataSource = dsPubs.Tables[0].Columns;
				chkColumns.DataMember = "Item";
				chkColumns.DataBind();
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
			this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private DataSet RetrieveData()
		{
			string connectionString;
			connectionString = "Provider=SQLOLEDB.1; " + 
				"Data Source=localhost;Initial Catalog=Pubs;Integrated Security=SSPI";

			string SQLSelect = "SELECT * FROM Titles";

			// Define ADO.NET objects.
			OleDbConnection con = new OleDbConnection(connectionString);
			OleDbCommand cmd = new OleDbCommand(SQLSelect, con);
			OleDbDataAdapter adapter = new OleDbDataAdapter(cmd) ;
			DataSet dsPubs = new DataSet();

			con.Open();
			adapter.Fill(dsPubs, "Titles");
			con.Close();

			return dsPubs;
		}

		private void cmdApply_Click(object sender, System.EventArgs e)
		{
			DataSet dsPubs;
			if (Cache["Titles"] == null)
			{
				dsPubs = RetrieveData();
				Cache.Insert("Titles", dsPubs, null, DateTime.MaxValue, 
					TimeSpan.FromMinutes(2));
				lblCacheStatus.Text = "Created and added to cache.";
			}
			else
			{
				dsPubs = (DataSet)Cache["Titles"];
				dsPubs = dsPubs.Copy();
				lblCacheStatus.Text = "Retrieved from cache.";
			}

			foreach (ListItem item in chkColumns.Items)
			{
				if (item.Selected)
				{
					dsPubs.Tables[0].Columns.Remove(item.Text);
				}
			}

			gridPubs.DataSource = dsPubs.Tables[0];
			gridPubs.DataBind();

		}

	}
}
