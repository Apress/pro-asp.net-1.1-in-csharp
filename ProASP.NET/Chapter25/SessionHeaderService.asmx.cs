using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Chapter24
{
	/// <summary>
	/// Summary description for SessionHeaderService.
	/// </summary>
	public class SessionHeaderService : System.Web.Services.WebService
	{
		public SessionHeaderService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion


		public SessionHeader CurrentSessionHeader;

		[WebMethod()]
		[SoapHeader("CurrentSessionHeader", Direction=SoapHeaderDirection.Out)]
		public void CreateSession()
		{
			// Create the header.
			CurrentSessionHeader = new SessionHeader(Guid.NewGuid().ToString());

			// From now on, all session data will be indexed under that key.
			Application[CurrentSessionHeader.SessionID] = new Hashtable();
		}

		[WebMethod()]
		[SoapHeader("CurrentSessionHeader", Direction=SoapHeaderDirection.In)]
		public void SetSessionData(DataSet ds)
		{
			// Locking is not required, because no two clients
			// could share the same session ID.
			Hashtable session = (Hashtable)Application[CurrentSessionHeader.SessionID];
			session.Add("DataSet", ds);
		}

		[WebMethod()]
		[SoapHeader("CurrentSessionHeader", Direction=SoapHeaderDirection.In)]
		public DataSet GetSessionData()
		{
			Hashtable session = (Hashtable)Application[CurrentSessionHeader.SessionID];
			return (DataSet)session["DataSet"];
		}

	}

	public class SessionHeader: SoapHeader
	{
		public string SessionID;

		public SessionHeader(string sessionID)
		{
			SessionID = sessionID;
		}
		// Default constructor is required for serialization.
		public SessionHeader()
		{}
	}
}
