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
using System.IO;
using System.Diagnostics;

namespace Chapter13
{
	/// <summary>
	/// Summary description for FileBrowser.
	/// </summary>
	public class FileBrowser : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid gridFileList;
		protected System.Web.UI.WebControls.DataList listFile;
		protected System.Web.UI.WebControls.Label lblCurrentDir;
		protected System.Web.UI.WebControls.Button cmdUp;
		protected System.Web.UI.WebControls.DataGrid gridDirList;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				ShowDirectoryContents(Server.MapPath("\\"));
			}
		}

		private void ShowDirectoryContents(string path)
		{
			// Define the current directory.
			DirectoryInfo dir = new DirectoryInfo(path);

			// Get the DirectoryInfo and FileInfo objects.
			FileInfo[] files = dir.GetFiles();
			DirectoryInfo[] dirs = dir.GetDirectories();
					
			// Show the directory listing.
			lblCurrentDir.Text = "Currently showing " + path;
			gridFileList.DataSource = files;
			gridDirList.DataSource = dirs;
			Page.DataBind();

			// Clear any selection.
			gridFileList.SelectedIndex = -1;

			// Keep track of the current path.
			ViewState["CurrentPath"] = path;
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
			this.gridDirList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.gridDirList_ItemCommand);
			this.gridFileList.SelectedIndexChanged += new System.EventHandler(this.gridFileList_SelectedIndexChanged);
			this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void gridDirList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			// Get the selected directory.
			string dir = (string)gridDirList.DataKeys[e.Item.ItemIndex];

			// Now refresh the directory list to
			// show the selected directory.
            ShowDirectoryContents(dir);
		}

		private void gridFileList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Get the selected file.
			string file = (string)gridFileList.DataKeys[gridFileList.SelectedIndex];

			// The DataList shows a collection (or list) of items.
			// To accomodate this design, you must add the file
			// to a collection of some sort.
			ArrayList files = new ArrayList();
			files.Add(new FileInfo(file));

			// Now show the selected file.
			listFile.DataSource = files;
			listFile.DataBind();
		}

		protected string GetVersionInfoString(object path)
		{
			FileVersionInfo	info = FileVersionInfo.GetVersionInfo((string)path);
			return info.FileName + " " + info.FileVersion + "<br>" +
				info.ProductName + " " + info.ProductVersion;
		}

		private void cmdUp_Click(object sender, System.EventArgs e)
		{
			string path = (string)ViewState["CurrentPath"];
			path = Path.Combine(path, "..");
			path = Path.GetFullPath(path);
			ShowDirectoryContents(path);
		}
	}
}
