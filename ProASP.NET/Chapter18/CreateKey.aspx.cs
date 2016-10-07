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
using System.Security.Cryptography;
using System.IO;

namespace Chapter18
{
	/// <summary>
	/// Summary description for CreateKey.
	/// </summary>
	public class CreateKey : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Don't overwrite an existing key.
			if (File.Exists(Server.MapPath("key.config")))
			{
				Response.Write("Key already exists in key.config.");
			}
			else
			{
				// Create the secret key.
				Rijndael crypt = Rijndael.Create();

				// Save the key.
				FileStream fs = File.OpenWrite(Server.MapPath("key.config"));
				fs.Write(crypt.Key, 0, crypt.Key.Length);

				// Save the initialization vector.
				fs.Write(crypt.IV, 0, crypt.IV.Length);
				fs.Close();

				Response.Write("Created key in file key.config");
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
