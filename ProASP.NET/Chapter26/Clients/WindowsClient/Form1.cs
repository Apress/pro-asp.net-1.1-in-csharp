using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using WindowsClient.localhost;
using System.Web.Services.Protocols;


namespace WindowsClient
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button cmdGetEmployees;
		private System.Windows.Forms.Button cmdTest;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.cmdGetEmployees = new System.Windows.Forms.Button();
			this.cmdTest = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(8, 8);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(496, 252);
			this.dataGrid1.TabIndex = 0;
			// 
			// cmdGetEmployees
			// 
			this.cmdGetEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGetEmployees.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdGetEmployees.Location = new System.Drawing.Point(372, 268);
			this.cmdGetEmployees.Name = "cmdGetEmployees";
			this.cmdGetEmployees.Size = new System.Drawing.Size(132, 28);
			this.cmdGetEmployees.TabIndex = 1;
			this.cmdGetEmployees.Text = "Get Employees (Async)";
			this.cmdGetEmployees.Click += new System.EventHandler(this.cmdGetEmployees_Click);
			// 
			// cmdTest
			// 
			this.cmdTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdTest.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdTest.Location = new System.Drawing.Point(8, 268);
			this.cmdTest.Name = "cmdTest";
			this.cmdTest.Size = new System.Drawing.Size(188, 28);
			this.cmdTest.TabIndex = 2;
			this.cmdTest.Text = "Click Me While the Call is Underway";
			this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(512, 306);
			this.Controls.Add(this.cmdTest);
			this.Controls.Add(this.cmdGetEmployees);
			this.Controls.Add(this.dataGrid1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "Form1";
			this.Text = "EmployeesService Client";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.Run(new Form1());
		}

	
		private void cmdGetEmployees_Click(object sender, System.EventArgs e)
		{
			// Disable the button so that only one asynchronous
			// call will be permitted at a time.
			cmdGetEmployees.Enabled = false;
			
			// Create the proxy.
			EmployeesService proxy = new EmployeesService();

			// Create the callback delegate.
			AsyncCallback callback = new AsyncCallback(Callback);

			// Call the web service asynchronously and
			// pass the callback and the proxy object.
			// There is no need to store the IAsyncResult object,
			// because this example doesn't check the state.
			proxy.BeginGetEmployees(callback, proxy);
		}

		private void Callback(IAsyncResult asyncResult)
		{
			// Retrieve the proxy from state.
			EmployeesService proxy = (EmployeesService)asyncResult.AsyncState;

			// Complete the call.
			DataSet ds = proxy.EndGetEmployees(asyncResult);
 
			// Update the user interface on the right thread.
			this.Invoke(new UpdateGridDelegate(UpdateGrid), new object[]{ds});
		}

		private delegate void UpdateGridDelegate(DataSet newDataSet);
		private void UpdateGrid(DataSet newDataSet)
		{
			// Bind the results.
			dataGrid1.DataSource = newDataSet.Tables[0];

			// Re-enabled the button for another refresh.
			cmdGetEmployees.Enabled = true;
		}

		private void cmdTest_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("Even when the web service call is underway, you can interact with this application.");
		}
		
	}
}
