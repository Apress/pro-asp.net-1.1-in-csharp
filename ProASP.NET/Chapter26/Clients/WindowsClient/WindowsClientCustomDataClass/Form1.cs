using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using WindowsClientCustomDataClass.localhost;

namespace WindowsClientCustomDataClass
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button cmdGetEmployees;
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
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(512, 306);
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
			EmployeesServiceCustomDataClass proxy = new EmployeesServiceCustomDataClass();

			//Uri newUrl = new Uri(proxy.Url);
			//proxy.Url = newUrl.Scheme + "://" + newUrl.Host + ":8080" + newUrl.AbsolutePath;
			
			// Call the web service and get the results.
			EmployeeDetails[] employees = proxy.GetEmployees();

			// Bind the results.
			dataGrid1.DataSource = employees;

			this.Cursor = Cursors.Default;
		}

	
	}
}
