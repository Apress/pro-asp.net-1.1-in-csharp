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
	/// Summary description for Search.
	/// </summary>
	public class Search : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Literal XmlText;
	
		private void Page_Load(object sender, System.EventArgs e)
		{  
			// Load the XML file.
			string xmlFile = Server.MapPath("DvdList.xml");
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlFile);
			
			// Find all the <Title> elements anywhere in the document.
			StringBuilder str = new StringBuilder();    
			XmlNodeList nodes = doc.GetElementsByTagName("Title");
			foreach (XmlNode node in nodes)
			{
				str.Append("Found: <b>");

				// Show the text contained in this <Title> element.
				string name = node.ChildNodes[0].Value;
				str.Append(name);
				str.Append("</b><br>");

				if (name == "Forrest Gump")
				{
					// Find the stars for just this movie.
					// First you need to get the parent node
					// (which is the <DVD> element for the movie).
					XmlNode parent = node.ParentNode;

					// Then you need to search down the tree.
					XmlNodeList childNodes = ((XmlElement)parent).GetElementsByTagName("Star");
					foreach (XmlNode childNode in childNodes)
					{
						str.Append("&nbsp;&nbsp;  Found Star: ");
						str.Append(childNode.ChildNodes[0].Value);
						str.Append("<br>");
					}
				}
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
