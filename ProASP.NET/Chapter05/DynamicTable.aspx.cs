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

namespace Chapter05
{
	/// <summary>
	/// Summary description for DynamicTable.
	/// </summary>
	public class DynamicTable : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Create a new HtmlTable object.
			HtmlTable table1 = new HtmlTable();  
			HtmlTableRow row;
			HtmlTableCell cell;
    
			// Set the table's formatting-related properties.
			table1.Border = 1;
			table1.CellPadding = 3;
			table1.CellSpacing = 3;
			table1.BorderColor = "red";

			// Start adding content to the table.
			for (int i=1; i<=5; i++)
			{
				// Create a new row and set its background color.
				row = new HtmlTableRow();
				row.BgColor = (i%2==0 ? "lightyellow" : "lightcyan");
      
				for (int j=1; j<=4; j++)
				{
					// Create a cell and set its text.
					cell = new HtmlTableCell();
					cell.InnerHtml = "Row: " + i.ToString() + 
						"<br>Cell: " + j.ToString();

					// Add the cell to the current row.
					row.Cells.Add(cell);
				}
    
				// Add the row to the table.
				table1.Rows.Add(row);
			}
    
			// Add the table to the page.
			this.Controls.Add(table1);
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
	}
}
