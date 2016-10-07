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
	/// Summary description for FileUpload.
	/// </summary>
	public class FileUpload : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputFile Uploader;
		protected System.Web.UI.HtmlControls.HtmlGenericControl lblStatus;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdUpload;
	
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
			this.cmdUpload.ServerClick += new System.EventHandler(this.cmdUpload_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdUpload_ServerClick(object sender, System.EventArgs e)
		{
			// Check if a file was submitted.
			if (Uploader.PostedFile.ContentLength != 0)
			{
				try 
				{
					if (Uploader.PostedFile.ContentLength > 1064)
					{
						// This exceeds the size limit we want to allow,.
						// You should check the size to prevent a denial of
						// service attack that attempts to fill up your
						// web server's hard drive.
						// You might also want to check the amount of 
						// remaining free space.
						lblStatus.InnerText = "Too large. This file is not allowed";
					}
					else
					{
						// Retrieve the physical directory path for the Upload subdirectory.
						string destDir = Server.MapPath("./Upload");

						// Extract the filename part from the full path of the original file.
						string fileName = System.IO.Path.GetFileName(
							Uploader.PostedFile.FileName);

						// Combine the destination directory with the filename.
						string destPath = System.IO.Path.Combine(destDir, fileName);

						// Save the file on the server.
						Uploader.PostedFile.SaveAs(destPath);
						lblStatus.InnerText = "Thanks for submitting your file";
					}
				}
				catch (Exception err) 
				{
					lblStatus.InnerText = err.Message;
				}
			}

		}
	}
}
