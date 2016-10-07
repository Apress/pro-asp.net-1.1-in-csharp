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

namespace StateManagement
{
	/// <summary>
	/// Summary description for SessionStateExample.
	/// </summary>
	public class SessionStateExample : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblSession;
		protected System.Web.UI.WebControls.ListBox lstItems;
		protected System.Web.UI.WebControls.Button cmdMoreInfo;
		protected System.Web.UI.WebControls.Label lblRecord;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				// Create Furniture objects.
				Furniture piece1 = new Furniture("Econo Sofa", 
					"Acme Inc.", 74.99M);
				Furniture piece2 = new Furniture("Pioneer Table", 
					"Heritage Unit", 866.75M);
				Furniture piece3 = new Furniture("Retro Cabinet", 
					"Sixties Ltd.", 300.11M);

				// Add objects to session state.
				Session["Furniture1"] = piece1;
				Session["Furniture2"] = piece2;
				Session["Furniture3"] = piece3;

				// Add rows to list control.
				lstItems.Items.Clear();
				lstItems.Items.Add(piece1.Name);
				lstItems.Items.Add(piece2.Name);
				lstItems.Items.Add(piece3.Name);
			}

			// Display some basic information about the session.
			// This is useful for testing configuration settings.
			lblSession.Text = "Session ID: " + Session.SessionID;
			lblSession.Text += "<br>Number of Objects: ";
			lblSession.Text += Session.Count.ToString();
			lblSession.Text += "<br>Mode: " + Session.Mode.ToString();
			lblSession.Text += "<br>Is Cookieless: ";
			lblSession.Text += Session.IsCookieless.ToString();
			lblSession.Text += "<br>Is New: ";
			lblSession.Text += Session.IsNewSession.ToString();
			lblSession.Text += "<br>Timeout (minutes): ";
			lblSession.Text += Session.Timeout.ToString();

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
			this.cmdMoreInfo.Click += new System.EventHandler(this.cmdMoreInfo_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdMoreInfo_Click(object sender, System.EventArgs e)
		{
			if (lstItems.SelectedIndex == -1)
			{
				lblRecord.Text = "No item selected.";
			}
			else
			{
				// Construct the right key name based on the index.
				string key = "Furniture" +
					(lstItems.SelectedIndex + 1).ToString();

				// Retrieve the Furniture object from session state.
				Furniture piece = (Furniture)Session[key];

				// Display the information for this object.
				lblRecord.Text = "Name: " + piece.Name;
				lblRecord.Text += "<br>Manufacturer: ";
				lblRecord.Text +=  piece.Description;
				lblRecord.Text += "<br>Cost: $" + piece.Cost.ToString();
			}

		}
	}


	public class Furniture
	{
		public string Name;
		public string Description;
		public decimal Cost;

		public Furniture(string name, string description, 
			decimal cost)
		{
			Name = name;
			Description = description;
			Cost = cost;
		}
	}

}

