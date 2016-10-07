using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Chapter07
{
	/// <summary>
	/// Summary description for CacheCallbackTest.
	/// </summary>
	public class CacheCallbackTest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblInfo;
		protected System.Web.UI.WebControls.Button cmdRemove;
		protected System.Web.UI.WebControls.Button cmdCheck;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				lblInfo.Text += "Creating items...<br>";
				string itemA = "item A";
				string itemB = "item B";
				Cache.Insert("itemA", itemA, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero, CacheItemPriority.Default, new CacheItemRemovedCallback(ItemRemovedCallback));
				Cache.Insert("itemB", itemB, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero, CacheItemPriority.Default, new CacheItemRemovedCallback(ItemRemovedCallback));
			}
		}

		private void cmdCheck_Click(object sender, System.EventArgs e)
		{
			string itemList = "";
			foreach(DictionaryEntry item in Cache)
			{
				itemList += item.Key.ToString() + " ";
			}
			lblInfo.Text += "<br>Found: " + itemList + "<br>";
		}

		private void cmdRemove_Click(object sender, System.EventArgs e)
		{
			lblInfo.Text += "<br>Removing itemA.<br>";
			Cache.Remove("itemA");
		}

		private void ItemRemovedCallback(string key, object value, 
			CacheItemRemovedReason reason)
		{
			// This fires after the request has ended, when the
			// item is removed.

			// If either item has been removed, make sure
			// the other item is also removed.
			if (key == "itemA" || key == "itemB")
			{
				Cache.Remove("itemA");
				Cache.Remove("itemB");
			}
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
			this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
			this.cmdCheck.Click += new System.EventHandler(this.cmdCheck_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


	}
}
