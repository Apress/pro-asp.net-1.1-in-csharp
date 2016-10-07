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
using System.Xml.Xsl;
using System.Xml.XPath;

namespace Chapter12
{
	/// <summary>
	/// Summary description for XmlToHtml.
	/// </summary>
	public class XmlToHtml : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			string xslFile = Server.MapPath("DvdList.xsl");
			string xmlFile = Server.MapPath("DvdList.xml");
			string htmlFile = Server.MapPath("DvdList.htm");
    
			XslTransform transf = new XslTransform();
			transf.Load(xslFile);
			transf.Transform(xmlFile, htmlFile);
    
			// Create an XPathDocument.
			XPathDocument xdoc = new XPathDocument(new XmlTextReader(xmlFile));
			
			// Create an XPathNavigator.

			XPathNavigator xnav = xdoc.CreateNavigator();
			
			// Transform the XML
			XmlReader reader = transf.Transform(xnav, null);

			// Go the the content and write it.
			reader.MoveToContent();
			Response.Write(reader.ReadOuterXml());
			reader.Close();
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
