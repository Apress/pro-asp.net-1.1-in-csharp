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
using DatabaseComponent;

namespace Chapter11
{
	/// <summary>
	/// Summary description for MultipleSelection.
	/// </summary>
	public class MultipleSelection : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid gridProducts;
		protected System.Web.UI.WebControls.DataGrid gridCart;
	
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox TextBox1;

		private NorthwindDB db = new NorthwindDB();
		private DataSet ds;
		private ShoppingCart cart;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Update the product list.
			ds = db.GetCategoriesProductsDataSet();
			gridProducts.DataSource = ds.Tables["Products"];
			gridProducts.DataBind();
			
			// Check for the shopping cart. If it doesn't
			// exist, create a new cart and make it available.
			if (Session["Cart"] == null)
			{
				cart = new ShoppingCart();
			}
			else
			{
				cart = (ShoppingCart)Session["Cart"];
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
			this.gridCart.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.gridCart_ItemCommand);
			this.gridProducts.SelectedIndexChanged += new System.EventHandler(this.gridProducts_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.MultipleSelection_PreRender);

		}
		#endregion


		private void gridProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Get the full record for the one selected row.
			DataRow[] rows = ds.Tables["Products"].Select("ProductID=" + gridProducts.DataKeys[gridProducts.SelectedIndex].ToString());
			DataRow row = rows[0];

			// Search to see if an item of this type is already in the cart.
			Boolean inCart = false;
			foreach (ShoppingCartItem item in cart)
			{
				// Increment the number count.
				if (item.ProductID == (int)row["ProductID"])
				{
					item.Units += 1;
					inCart = true;
					break;
				}
			}
			
			// If the item isn't in the cart, add it.
			if (!inCart)
			{
				ShoppingCartItem item = new ShoppingCartItem(
					(int)row["ProductID"], (string)row["ProductName"], 
					(decimal)row["UnitPrice"], 1);
				cart.Add(item);
			}

			// Don't keep the item selected in the product list.
			gridProducts.SelectedIndex = -1;
		}

		private void gridCart_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			// The text box is the second control.
			// The first control in a template column
			// is always a blank LiteralControl.
			TextBox txt = (TextBox)gridCart.Items[e.Item.DataSetIndex].Cells[3].Controls[1];
			try
			{
				// Update the appropriate cart item.
				int newCount = int.Parse(txt.Text);
				if (newCount > 0)
				{
					cart[e.Item.DataSetIndex].Units = newCount;
				}
				else if (newCount == 0)
				{
					cart.RemoveAt(e.Item.DataSetIndex);
				}
			}
			catch
			{
				// Ignore invalid (non-numeric) entries.
			}
		}

		private void MultipleSelection_PreRender(object sender, System.EventArgs e)
		{
			// Store the shopping cart.
			Session["Cart"] = cart;

			// Show the shopping cart in the grid.
			gridCart.DataSource = cart;
			gridCart.DataBind();
		}
	}


    public class ShoppingCartItem
	{
		private int productID;
		private string productName;
		private decimal unitPrice;
		private int units;

		public int ProductID
		{
			get {return productID;}
		}
		public string ProductName
		{
			get {return productName;}
		}
		public decimal UnitPrice
		{
			get {return unitPrice;}
		}
		public int Units
		{
			get {return units;}
			set {units = value;}
		}
		public decimal Total
		{
			get {return Units * UnitPrice;}
		}
		public ShoppingCartItem(int productID, 
		  string productName, decimal unitPrice, int units)
		{
			this.productID = productID;
            this.productName = productName;
			this.unitPrice = unitPrice;
			this.units = units;
		}
	}


	public class ShoppingCart : CollectionBase  
	{
		public ShoppingCartItem this[int index]  
		{
			get {return((ShoppingCartItem)List[index]);}
			set {List[index] = value;}
		}
		public int Add(ShoppingCartItem value)  
		{
			return(List.Add(value));
		}
		public int IndexOf(ShoppingCartItem value)
		{
			return(List.IndexOf(value));
		}
		public void Insert(int index, ShoppingCartItem value)
		{
			List.Insert(index, value);
		}
		public void Remove(ShoppingCartItem value)  
		{
			List.Remove(value);
		}
		public bool Contains(ShoppingCartItem value)  
		{
			return(List.Contains(value));
		}
	}


}
