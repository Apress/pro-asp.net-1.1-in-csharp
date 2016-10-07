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
using System.Xml.Schema;
using System.IO;
using System.Xml;

namespace SimpleXML
{
	/// <summary>
	/// Summary description for XmlValidation.
	/// </summary>
	public class XmlValidation : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button cmdValidate;
		protected System.Web.UI.WebControls.RadioButton optValid;
		protected System.Web.UI.WebControls.RadioButton optInvalidData;
		protected System.Web.UI.WebControls.Label lblStatus;
	
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
			this.cmdValidate.Click += new System.EventHandler(this.cmdValidate_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdValidate_Click(object sender, System.EventArgs e)
		{
			string filePath = "";
			if (optValid.Checked)
			{
				filePath = Server.MapPath("DvdList.xml");
			}
			else if (optInvalidData.Checked)
			{
				filePath += Server.MapPath("DvdListInvalid.xml");
			}
			
			lblStatus.Text="";

			// Open the XML file.
			FileStream fs = new FileStream(filePath, FileMode.Open);
			XmlTextReader r = new XmlTextReader(fs);

			// Create the validating reader.
			XmlValidatingReader vr = new XmlValidatingReader(r);
			vr.ValidationType = ValidationType.Schema;

			// Add the XSD file to the validator.
			XmlSchemaCollection schemas = new XmlSchemaCollection();
			schemas.Add("", Server.MapPath("DvdList.xsd"));
			vr.Schemas.Add(schemas);

			// Connect the event handler.
			vr.ValidationEventHandler += new ValidationEventHandler(MyValidateHandler);

			// Read through the document.
			while (vr.Read())
			{
				// Process document here.
				// If an error is found, an exception will be thrown.
			}

			vr.Close();

			lblStatus.Text+="<br>Complete.";
		}

		public void MyValidateHandler(Object sender, ValidationEventArgs e)
		{
			lblStatus.Text += "Error: " + e.Message + "<br>";
		}

	}
}
