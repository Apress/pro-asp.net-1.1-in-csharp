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
using Chapter26_WebClient.localhost;
using System.Threading;

namespace WebClient
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button cmdSynchronous;
		protected System.Web.UI.WebControls.Button cmdAsynchronous;
		protected System.Web.UI.WebControls.Label lblInfo;
		protected System.Web.UI.WebControls.Button cmdMultiple;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
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
			this.cmdSynchronous.Click += new System.EventHandler(this.cmdSynchronous_Click);
			this.cmdAsynchronous.Click += new System.EventHandler(this.cmdAsynchronous_Click);
			this.cmdMultiple.Click += new System.EventHandler(this.cmdMultiple_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdSynchronous_Click(object sender, System.EventArgs e)
		{
			// Record the start time.
			DateTime startTime = DateTime.Now;

			// Get the web service data.
			EmployeesService proxy = new EmployeesService();
			DataGrid1.DataSource = proxy.GetEmployees();
			DataGrid1.DataBind();

			// Perform some other time-consuming tasks.
			DoSomethingSlow();

			// Determine the total time taken.
			TimeSpan timeTaken = DateTime.Now.Subtract(startTime);
			lblInfo.Text = "Synchronous operations took " + timeTaken.TotalSeconds + " seconds.";
		}

		private void cmdAsynchronous_Click(object sender, System.EventArgs e)
		{
			// Record the start time.
			DateTime startTime = DateTime.Now;

			// Start the web service.
			EmployeesService proxy = new EmployeesService();
			IAsyncResult handle = proxy.BeginGetEmployees(null, null);

			// Perform some other time-consuming tasks.
			DoSomethingSlow();

			// Retrieve the result. If it isn't ready, wait.
			DataGrid1.DataSource = proxy.EndGetEmployees(handle);
			DataGrid1.DataBind();

			// Determine the total time taken.
			TimeSpan timeTaken = DateTime.Now.Subtract(startTime);
			lblInfo.Text = "Asynchronous operations took " + timeTaken.TotalSeconds + " seconds.";
		}

		private void DoSomethingSlow()
		{
			// Simulate a slow operation by pausing for three seconds.
			System.Threading.Thread.Sleep(3000);
		}

		private void cmdMultiple_Click(object sender, System.EventArgs e)
		{
			// Record the start time.
			DateTime startTime = DateTime.Now;

			EmployeesService proxy = new EmployeesService();

			// Call three methods asynchronously.
			IAsyncResult async1 = proxy.BeginGetEmployees(null, null);
			IAsyncResult async2 = proxy.BeginGetEmployees(null, null);
			IAsyncResult async3 = proxy.BeginGetEmployees(null, null);

			// Create an array of WaitHandle objects.
			WaitHandle[] waitHandles = {async1.AsyncWaitHandle, 
			  async2.AsyncWaitHandle, async3.AsyncWaitHandle};

			// Wait for all the calls to finish.
			WaitHandle.WaitAll(waitHandles);

			// You can now retrieve the results.
			DataSet ds1 = proxy.EndGetEmployees(async1);
			DataSet ds2 = proxy.EndGetEmployees(async2);
			DataSet ds3 = proxy.EndGetEmployees(async3);

			DataSet dsMerge = new DataSet();
			dsMerge.Merge(ds1);
			dsMerge.Merge(ds2);
			dsMerge.Merge(ds3);
			DataGrid1.DataSource = dsMerge;
			DataGrid1.DataBind();

			// Determine the total time taken.
			TimeSpan timeTaken = DateTime.Now.Subtract(startTime);
			lblInfo.Text = "Calling three methods took " + timeTaken.TotalSeconds + " seconds.";
		}

		
	}
}
