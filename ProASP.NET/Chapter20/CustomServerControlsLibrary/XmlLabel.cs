using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace CustomServerControlsLibrary
{
	public class XmlLabel : Label
	{
		protected override void RenderContents(HtmlTextWriter output)
		{
			// Don't call the base implementation of RenderContents().
			// You need to replace the rendering logic for the label text
			// with your own routine.
			string xmlText = XmlLabel.ConvertXmlTextToHtmlText(Text);
			output.Write(xmlText);
		}

		// This is a static method, just in case you want to use it
		// idependent of any control object.
		public static string ConvertXmlTextToHtmlText(string inputText)
		{
            // Replace all start and end tags.
			string startPattern = @"<([^>]+)>";
			Regex regEx = new Regex(startPattern);
			string outputText = regEx.Replace(inputText, "&lt;<b>$1&gt;</b>");

			outputText = outputText.Replace(" ", "&nbsp;");
			outputText = outputText.Replace("\r\n", "<br>");

			return outputText;
		}
	}
}
