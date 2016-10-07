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
using System.Xml;
using System.Text;

namespace Chapter12
{
	/// <summary>
	/// Summary description for XPathSearch.
	/// </summary>
	public class XPathSearch : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Literal XmlText;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Load the XML file.
			string xmlFile = Server.MapPath("DvdList.xml");
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlFile);
			
			// Retrieve the title of every science fiction move.
			XmlNodeList nodes = doc.SelectNodes("/DvdList/DVD/Title[../@Category='Science Fiction']");

			// Display the titles.
			StringBuilder str = new StringBuilder();    
			foreach (XmlNode node in nodes)
			{
				str.Append("Found: <b>");

				// Show the text contained in this <Title> element.
				str.Append(node.ChildNodes[0].Value);
				str.Append("</b><br>");
			}
			XmlText.Text = str.ToString();
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
