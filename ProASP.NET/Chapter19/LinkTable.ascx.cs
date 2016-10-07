namespace UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for LinkTable.
	/// </summary>
	public class LinkTable : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataList listContent;

		private void Page_Load(object sender, System.EventArgs e)
		{
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listContent.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.listContent_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected System.Web.UI.WebControls.Label lblTitle;
				
		public string Title
		{
			get { return lblTitle.Text; }
			set { lblTitle.Text = value; }
		}

		private LinkTableItem[] items;
		public LinkTableItem[] Items
		{
			get { return items; }
			set
			{
				items = value;

				// Refresh the grid.
				listContent.DataSource = items;
				listContent.DataBind();
			}
		}

		public event LinkClickedEventHandler LinkClicked ;
		private void listContent_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			if (LinkClicked != null)
			{
				// Get the HyperLink object that was clicked.
				LinkButton link = (LinkButton)e.Item.Controls[1];

				// Construct the event arguments.
				LinkTableItem item = new LinkTableItem(link.Text, link.CommandArgument);
				LinkTableEventArgs args = new LinkTableEventArgs(item);

				// Fire the event.
				LinkClicked(this, args);

				// Navigate to the link if the event recipient didn't
				// cancel the operation.
				if (!args.Cancel)
				{
					Response.Redirect(item.Url);
				}
			}
		}
	}

	public delegate void LinkClickedEventHandler(object sender,
	LinkTableEventArgs e);

	public class LinkTableItem
	{
		private string text;
		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		private string url;
		public string Url
		{
			get { return url; }
			set { url = value; }
		}

		// Default constructor.
		public LinkTableItem()
		{}
		
		public LinkTableItem(string text, string url)
		{
			this.text = text;
			this.url = url;
		}
	}

	public class LinkTableEventArgs : EventArgs
	{
		private LinkTableItem selectedItem;
		public LinkTableItem SelectedItem
		{
			get
			{ return selectedItem; }
		}

		private bool cancel = false;
		public bool Cancel
		{
			get { return cancel; }
			set { cancel=value; }
		}

		public LinkTableEventArgs(LinkTableItem item)
		{
			selectedItem = item;
		}
	}
}
