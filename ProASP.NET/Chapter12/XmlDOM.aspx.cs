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
	/// Summary description for XmlDOM.
	/// </summary>
	public class XmlDOM : System.Web.UI.Page
	{
		protected Literal XmlText;

		private void Page_Load(object sender, System.EventArgs e)
		{
			string xmlFile = Server.MapPath("DvdList.xml");
    
			// Load the XML file in an XmlDocument.
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlFile);
			
			// Write the description text.
			XmlText.Text = GetChildNodesDescr(doc.ChildNodes, 0);
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

		private string GetChildNodesDescr(XmlNodeList nodeList, int level)
		{
			string indent = "";
			for (int i=0; i<level; i++)
				indent += "&nbsp; &nbsp; &nbsp;";
      
			StringBuilder str = new StringBuilder("");
    
			foreach (XmlNode node in nodeList)
			{
				switch(node.NodeType)
				{
					case XmlNodeType.XmlDeclaration:
						str.Append("XML Declaration: <b>");
						str.Append(node.Name);
						str.Append(" ");
						str.Append(node.Value);
						str.Append("</b><br>");
						break;
        
					case XmlNodeType.Element:
						str.Append(indent);
						str.Append("Element: <b>");
						str.Append(node.Name);
						str.Append("</b><br>");
						break;
        
					case XmlNodeType.Text:
						str.Append(indent);
						str.Append(" - Value: <b>");
						str.Append(node.Value);
						str.Append("</b><br>");
						break;
        
					case XmlNodeType.Comment:
						str.Append(indent);
						str.Append("Comment: <b>");
						str.Append(node.Value);
						str.Append("</b><br>");
						break;
				}

				if (node.Attributes != null)
				{
					foreach (XmlAttribute attrib in node.Attributes)
					{
						str.Append(indent);
						str.Append(" - Attribute: <b>");
						str.Append(attrib.Name);
						str.Append("</b> Value: <b>");
						str.Append(attrib.Value);
						str.Append("</b><br>");
					}
				}
      
				if (node.HasChildNodes)
					str.Append(GetChildNodesDescr(node.ChildNodes, level+1));
			}
    
			return str.ToString();
		} 
	}
}
