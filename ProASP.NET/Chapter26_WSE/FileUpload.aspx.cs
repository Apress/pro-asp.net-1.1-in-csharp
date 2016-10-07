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
using Chapter26_WSE.localhost;
using Microsoft.Web.Services2.Dime;
using System.IO;

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
					  EmployeesServiceWse proxy = new EmployeesServiceWse();
    
					// Create the attachment.
					// You can use either a file path or an open stream.
					string fileName = Path.GetFileName(Uploader.PostedFile.FileName);
					DimeAttachment attachment = new DimeAttachment(fileName,
						TypeFormat.MediaType, Uploader.PostedFile.InputStream);
					proxy.RequestSoapContext.Attachments.Add(attachment);

					// Upload the attachment.
					proxy.UploadFile(fileName);

					lblStatus.InnerText = "Thanks for submitting your file";

				}
				catch (Exception err) 
				{
					lblStatus.InnerText = err.Message;
				}
			}

		}
	}
}
