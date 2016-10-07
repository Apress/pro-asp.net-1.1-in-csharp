using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

namespace CustomServerControlsLibrary
{
	public class DbDropDown : System.Web.UI.WebControls.WebControl, IPostBackDataHandler
	{
		private IEnumerable dataSource;		
		public IEnumerable DataSource
		{
			get {return dataSource;}
			set {dataSource=value;}
		}

		private string dataText;
		public string DataTextField
		{
			get {return dataText;}
			set {dataText=value;}
		}

		private string dataValue;
		public string DataValueField
		{
			get {return dataValue;}
			set {dataValue=value;}
		}
	
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);

			// Clear child controls and view state.
			Controls.Clear();
			if(HasChildViewState)
			{
				ClearChildViewState();
			}

			// Create controls from data.
			CreateControlHierarchy(true);

			// Indicate that the controls have been created.
			ChildControlsCreated=true;

			// Turn on view state tracking.
			if(IsTrackingViewState)
			{
				TrackViewState();
			}
		}

		
		private int itemCount = -1;
		public int ItemCount
		{
			get {return itemCount;}
		}
		
		private int selectedIndex;
		public int SelectedIndex
		{
			get {return selectedIndex;}
		}

		// Examine the posted value to see if the index has changed.
		public bool LoadPostData(string postDataKey, NameValueCollection postData)
		{
			string selectedValue;
			int currentIndex=0;
			bool hasIndexChanged=false;

			// Get the posted value.
			selectedValue = postData[postDataKey];

			// Make sure the controls are created.
			EnsureChildControls();

			// Loop through the controls selecting the appropriate one
			// and deselecting all others.
			foreach(Control ctrl in Controls)
			{
				if(ctrl as DbListItem !=null)
				{
					if(String.Compare(((DbListItem)ctrl).Value, selectedValue,true)==0)
					{
						((DbListItem)ctrl).Selected = true;

						// If the index has changed, flip our flag to 
						// true so our event will be raised.
						if(selectedIndex!=currentIndex)
						{
							selectedIndex=currentIndex;
							hasIndexChanged=true;
						}
					}
					else
					{
						((DbListItem)ctrl).Selected=false;
					}
					currentIndex++;
				}
			}
			return hasIndexChanged;
		}

		// Called if LoadPostData returns true.
		public void RaisePostDataChangedEvent()
		{
			OnSelectedIndexChanged();
		}

		public event EventHandler SelectedIndexChanged;

		// Method to raise the index changed event.
		protected void OnSelectedIndexChanged()
		{
			if(SelectedIndexChanged!=null)
			{
				SelectedIndexChanged(this,EventArgs.Empty);
			}
		}

		// Create the child controls using view state.
		protected override void CreateChildControls()
		{
			Controls.Clear();
			// If items were created with data, 
			// then create the hierarchy.
			if(itemCount>0)
				CreateControlHierarchy(false);
		}

		protected void CreateControlHierarchy(bool useDataSource)
		{
			IEnumerator items;

			// Set the enumerator to the data source or the item array.
			if (useDataSource)
			{
				items = dataSource.GetEnumerator();
			}
			else
			{
				items = new DummyDataSource(itemCount).GetEnumerator(); 
			}

			int count = 0;
			
			while(items.MoveNext())
			{
				DbListItem item = null;
				if(useDataSource)
				{
					string itemText = (string)DataBinder.GetPropertyValue(items.Current,DataTextField);
					string itemValue =(string)DataBinder.GetPropertyValue(items.Current, DataValueField);

					// Create the new item with the supplied values.
					item = new DbListItem(itemValue, itemText, false);
					item.TrackViewState();
				}
				else
				{
					// Create a new empty item.
					// It's values will be retrieved from view state.
					item = new DbListItem();
				}

				// Add the item to the controls collection
				Controls.Add(item);
				count++;
			}

			if(useDataSource)
			{
				// Store the number of items in a private member variable.
				itemCount=count;
			}
		}


		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
		}

		public DbDropDown() : base(HtmlTextWriterTag.Select)
		{}


		
		// Restore the view state of the control.
		protected override void LoadViewState(object state)
		{
			// Cast the state object to a triplet.
			Triplet oldState = (Triplet)state;

			// Load the state for the base control.
			base.LoadViewState(oldState.First);

			// Get the selected index out of the state.
			selectedIndex=(int)oldState.Second;

			// Get the item count from state.
			itemCount=(int)oldState.Third;
		}

		// Create the view state for this control.
		protected override object SaveViewState()
		{
			// Create a new triplet with the base state and
			// the selected index of the list.
			Triplet state = new Triplet(base.SaveViewState(),selectedIndex, itemCount);
			return state;
		}
		
	}

	// Represents an item in the list.
	public class DbListItem : System.Web.UI.WebControls.WebControl
	{
		string text;
		public string Text
		{
			get {return text;}
			set {text=value;}
		}

		string itemValue;
		public string Value
		{
			get {return itemValue;}
			set {itemValue=value;}
		}

		bool selected;
		public bool Selected
		{
			get {return selected;}
			set {selected=value;}
		}

		public DbListItem(){}

		public DbListItem(string itemValue, string text, bool isSelected)
		{
			Text=text;
			Value=itemValue;
			Selected=isSelected;
		}
		
		// Render the option tag using the properties of the control.
		protected override void Render(HtmlTextWriter output)
		{
			// Add the value attribute.
			output.AddAttribute(HtmlTextWriterAttribute.Value,Value);
			
			// If this item is selected, then add that attribute.
			if (Selected)
			{
				output.AddAttribute(HtmlTextWriterAttribute.Selected,"true");
			}

			// Render the option tag with the text in it.
			output.RenderBeginTag(HtmlTextWriterTag.Option);
			output.Write(Text);
			output.RenderEndTag();
		}

		// Internal method to let the list indicate
		// that this object should track its view state
		internal new void TrackViewState()
		{
			base.TrackViewState();
		}

		// Load the view state into the object.
		protected override void LoadViewState(object state)
		{
			// Get the three properties out of
			// the triplet object that is the state.
			Triplet tri = (Triplet)state;
			Text=(string)tri.First;
			Value=(string)tri.Second;
			Selected=(bool)tri.Third;
		}
		
		// Save the state of the object.
		protected override object SaveViewState()
		{
			// Create a new triplet with the three properties.
			Triplet tri = new Triplet(Text,Value,Selected);
			return tri;
		}

	}

	// The DummyDataSource class provides the ability to
	// create the control hierarchy on postback. 
	internal sealed class DummyDataSource : IEnumerable 
	{
		private int dataItemCount;

		public DummyDataSource(int dataItemCount) 
		{
			this.dataItemCount = dataItemCount;
		}

		public IEnumerator GetEnumerator() 
		{
			return new DummyDataSourceEnumerator(dataItemCount);
		}

		// The enumerator for the dummy data source.
		private class DummyDataSourceEnumerator : IEnumerator 
		{
 			private int count;
			private int index;

			public DummyDataSourceEnumerator(int count) 
			{
				this.count = count;
				this.index = -1;
			}

			public object Current 
			{
				get{return null;}
			}

			public bool MoveNext() 
			{
				index++;
				return index < count;
			}

			public void Reset() 
			{
				this.index = -1;
			}
		}
	}

}
