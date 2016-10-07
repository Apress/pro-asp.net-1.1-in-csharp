using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI.Design;

namespace CustomServerControlsLibrary
{

	[Designer(typeof(SuperSimpleRepeaterDesigner))]
	public class SuperSimpleRepeater2 : WebControl, INamingContainer
	{
		public SuperSimpleRepeater2() : base()
		{
			RepeatCount = 1;
		}

		public int RepeatCount
		{
			get {return (int)ViewState["RepeatCount"];}
			set {ViewState["RepeatCount"] = value;}
		}

		private ITemplate itemTemplate;

		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SimpleRepeaterItem))]
		public ITemplate ItemTemplate
		{
			get {return itemTemplate;}
			set {itemTemplate=value;}
		}

		private ITemplate alternatingItemTemplate;

		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SimpleRepeaterItem))]
		public ITemplate AlternatingItemTemplate
		{
			get {return alternatingItemTemplate;}
			set {alternatingItemTemplate=value;}
		}

		private ITemplate headerTemplate;

		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SimpleRepeaterItem))]
		public ITemplate HeaderTemplate
		{
			get {return headerTemplate;}
			set {headerTemplate=value;}
		}

		private ITemplate footerTemplate;

		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(SimpleRepeaterItem))]
		public ITemplate FooterTemplate
		{
			get {return footerTemplate;}
			set {footerTemplate=value;}
		}


		protected override void CreateChildControls() 
		{
			Controls.Clear();
				
			if ((RepeatCount > 0) && (itemTemplate!=null))
			{
				// Start by outputing the header template (if supplied).
				if(headerTemplate != null)
				{
					SimpleRepeaterItem headerContainer = new SimpleRepeaterItem(0, RepeatCount);
					headerTemplate.InstantiateIn(headerContainer);
					//headerContainer.DataBind();
					Controls.Add(headerContainer);
				}

				// Output the content the specified number of times.
				for (int i = 0; i<RepeatCount; i++)
				{
					SimpleRepeaterItem container = new SimpleRepeaterItem(i+1, RepeatCount);

					if ((i%2 == 0) && (alternatingItemTemplate != null))
					{
						// This is an alternating item and there is an alternating template.
						alternatingItemTemplate.InstantiateIn(container);
					}
					else
					{
						itemTemplate.InstantiateIn(container);
					}
					//container.DataBind();
					Controls.Add(container);
				}

				// Once all of the items have been rendered,
				// add the footer template if specified.
				if (footerTemplate != null)
				{
					SimpleRepeaterItem footerContainer = new SimpleRepeaterItem(RepeatCount, RepeatCount);
					footerTemplate.InstantiateIn(footerContainer);
					//footerContainer.DataBind();
					Controls.Add(footerContainer);
				}
			}
			else
			{
				// Show an error message.
				Controls.Add(new LiteralControl("Specify the record count and an item template"));
			}
		}

		public override void DataBind()
		{
			EnsureChildControls();
			base.DataBind();
		}



	}



	public class SimpleRepeaterItem : WebControl, INamingContainer
	{
		int index;
		public int Index
		{
			get {return index;}
		}

		int total;
		public int Total
		{
			get{return total;}
		}

		// A constructor that allows you to set the index and total count
		// when you create an item.
		public SimpleRepeaterItem(int itemIndex, int totalCount)
		{
			index = itemIndex;
			total = totalCount;
		}	
	}


	public class SuperSimpleRepeaterDesigner : ControlDesigner
	{
			
		public override string GetDesignTimeHtml()
		{
			try
			{
				SuperSimpleRepeater2 repeater = (SuperSimpleRepeater2)base.Component;
				if (repeater.ItemTemplate == null)
				{
					return GetEmptyDesignTimeHtml();
				}
				else
				{
					String designTimeHtml = String.Empty;

						((Control)this.Component).DataBind();
						designTimeHtml = base.GetDesignTimeHtml();

					
				}
				
				return base.GetDesignTimeHtml();
			}
			catch (Exception e)
			{
				return GetErrorDesignTimeHtml(e);
			}
		}
	
		protected override string GetEmptyDesignTimeHtml() 
		{
			string text = "Switch to design view to add a template to this control.";
			return CreatePlaceHolderDesignTimeHtml(text);
		}

		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			string text = string.Format("{0}{1}{2}{3}", 
				"There was an error and the control can't be displayed.",
				"<BR>", "Exception: ", e.Message);
			return CreatePlaceHolderDesignTimeHtml(text);
		}

	}
}
