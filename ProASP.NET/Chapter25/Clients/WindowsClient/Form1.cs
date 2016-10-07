using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Web.Services.Protocols;
using WindowsClient.localhost;
using WindowsClient.SessionHeaderWebService;

namespace WindowsClient
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button cmdGetEmployees;
		private System.Windows.Forms.Button cmdTestError;
		private System.Windows.Forms.Button cmdSessionHeader;
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
			this.cmdTestError = new System.Windows.Forms.Button();
			this.cmdSessionHeader = new System.Windows.Forms.Button();
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
			this.cmdGetEmployees.Location = new System.Drawing.Point(404, 268);
			this.cmdGetEmployees.Name = "cmdGetEmployees";
			this.cmdGetEmployees.Size = new System.Drawing.Size(96, 28);
			this.cmdGetEmployees.TabIndex = 1;
			this.cmdGetEmployees.Text = "Get Employees";
			this.cmdGetEmployees.Click += new System.EventHandler(this.cmdGetEmployees_Click);
			// 
			// cmdTestError
			// 
			this.cmdTestError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdTestError.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdTestError.Location = new System.Drawing.Point(8, 268);
			this.cmdTestError.Name = "cmdTestError";
			this.cmdTestError.Size = new System.Drawing.Size(96, 28);
			this.cmdTestError.TabIndex = 3;
			this.cmdTestError.Text = "Test Error";
			this.cmdTestError.Click += new System.EventHandler(this.cmdTestError_Click);
			// 
			// cmdSessionHeader
			// 
			this.cmdSessionHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSessionHeader.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdSessionHeader.Location = new System.Drawing.Point(116, 268);
			this.cmdSessionHeader.Name = "cmdSessionHeader";
			this.cmdSessionHeader.Size = new System.Drawing.Size(96, 28);
			this.cmdSessionHeader.TabIndex = 4;
			this.cmdSessionHeader.Text = "Test Header";
			this.cmdSessionHeader.Click += new System.EventHandler(this.cmdSessionHeader_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(512, 306);
			this.Controls.Add(this.cmdSessionHeader);
			this.Controls.Add(this.cmdTestError);
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
			this.Cursor = Cursors.WaitCursor;

			// Create the proxy.
			EmployeesService proxy = new EmployeesService();

			// Call the web service and get the results.
			DataSet ds = proxy.GetEmployees();

			// Bind the results.
			dataGrid1.DataSource = ds.Tables[0];

			this.Cursor = Cursors.Default;
		}

		private void cmdTestError_Click(object sender, System.EventArgs e)
		{
			// Create the proxy.
			EmployeesService proxy = new EmployeesService();
			
			try
			{
				int count = proxy.GetEmployeesCountError();
			}
			catch (SoapException err)
			{
				MessageBox.Show("Original error was: " + err.Detail.InnerText);
			}
		}

		private void cmdSessionHeader_Click(object sender, System.EventArgs e)
		{
			SessionHeaderService proxy = new SessionHeaderService();

			//Uri newUrl = new Uri(proxy.Url);
			//proxy.Url = newUrl.Scheme + "://" + newUrl.Host + ":8080" + newUrl.AbsolutePath;
			
			proxy.CreateSession();
			proxy.SetSessionData(new DataSet("TestDataSet"));
			DataSet ds = proxy.GetSessionData();
			MessageBox.Show("Received: " + ds.DataSetName);
		}
	}
}
