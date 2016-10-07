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
using System.Configuration;
using System.IO;

namespace Chapter11
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class BlockwiseRender : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			string connectionString = 
				"Data Source=localhost;Initial Catalog=pubs;Integrated Security=SSPI";
			SqlConnection con = new SqlConnection(connectionString);
			string SQL = "SELECT logo FROM pub_info WHERE pub_id='1389'";
			SqlCommand cmd = new SqlCommand(SQL, con);
				
			try
			{
				con.Open();
				SqlDataReader r = 
					cmd.ExecuteReader(CommandBehavior.SequentialAccess);
			
				if (r.Read())
				{			
					int bufferSize = 100;                  // Size of the buffer.
					byte[] bytes = new byte[bufferSize];   // The buffer (one block of data).
					long bytesRead;                        // The number of bytes read.
					long readFrom = 0;                     // The starting index.

                    // Read the field 100 bytes at a time.
					do
					{
						bytesRead = r.GetBytes(0, readFrom, bytes, 0, bufferSize);
						Response.BinaryWrite(bytes);
						readFrom += bufferSize;
					} while (bytesRead == bufferSize);
				}
				r.Close();
			}
			finally
			{
				con.Close();
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
