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
	/// Summary description for WriteAndReadXml.
	/// </summary>
	public class ReadXmlEfficient : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Literal XmlText;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ReadXML();
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

		     
		private void ReadXML()
		{
			string xmlFile = Server.MapPath("DvdList.xml");
    
			// Create the reader.
			XmlTextReader reader = new XmlTextReader(xmlFile);
    
			StringBuilder str = new StringBuilder();    
			reader.ReadStartElement("DvdList");
			
			// Read all the <DVD> elements.
			while (reader.Read())
			{
				if ((reader.Name == "DVD") && (reader.NodeType == XmlNodeType.Element))
				{
					reader.ReadStartElement("DVD");
					str.Append("<ul><b>");
					str.Append(reader.ReadElementString("Title"));
					str.Append("</b><li>");
					str.Append(reader.ReadElementString("Director"));
					str.Append("</li><li>");
					str.Append(String.Format("{0:C}", 
						Decimal.Parse(reader.ReadElementString("Price"))));
					str.Append("</li></ul>");
				}
			}
			// Close the reader and show the text.
			reader.Close();
			XmlText.Text = str.ToString();
		}
	}
}
