using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CustomServerControlsLibrary
{
	public class CustomImageButton : WebControl, IPostBackEventHandler
	{
		public CustomImageButton() : base(HtmlTextWriterTag.Img)
		{
			ImageUrl = "";
		}

		public string ImageUrl
		{
			get {return (string)ViewState["ImageUrl"];}
			set {ViewState["ImageUrl"] = value;}
		}

		protected override void AddAttributesToRender(HtmlTextWriter output)
		{
			output.AddAttribute("name", UniqueID);
			output.AddAttribute("src", ImageUrl);
			output.AddAttribute("onClick", Page.GetPostBackEventReference(this));
		}

		public event EventHandler ImageClicked;

		public void RaisePostBackEvent(string eventArgument)
		{
			OnImageClicked(new EventArgs());
		}

		protected virtual void OnImageClicked(EventArgs e)
		{
			// Check for at least one listener and then raise the event.
			if (ImageClicked != null)
				ImageClicked(this, e);
		}

	}
}
