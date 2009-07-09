using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using OpenRasta.Demo.Handlers;
using OpenRasta.IO;
using OpenRasta.Web;

namespace OpenRasta.Demo.Resources
{
	public interface IShoppingList : IEnumerable<ShoppingListItem>
	{
		void Add(ShoppingListItem item);
	}

	public class ShoppingList : IShoppingList
	{
		private readonly List<ShoppingListItem> _list = new List<ShoppingListItem>();
		public IEnumerator<ShoppingListItem> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(ShoppingListItem item)
		{
			_list.Add(item);
		}

		public ShoppingListItem GetItem(string description)
		{
			return this.Where(x => x.Description.ToUpper() == description.ToUpper()).FirstOrDefault();
		}

	}
	public class ShoppingListItem
	{
		public string Description { get; set; }
		public string Notes { get; set; }
		public string Quantity { get; set; }
		public bool Optional { get; set; }
		public ShoppingListItemImage Image { get; set; }
	}

	public static class ShoppingListItemExtensions
	{
		public static ShoppingListItem WithImage(this ShoppingListItem item, string fileName)
		{
			item.Image = new ShoppingListItemImage(fileName, item);
			return item;
		}
	}

	public class ShoppingListItemImage
	{
		public ShoppingListItemImage(string filename, ShoppingListItem parent)
		{
			Parent = parent;
			Filename = filename;
		}
		public string Filename
		{
			get; private set;
		}
		public ShoppingListItem Parent
		{
			get; private set;
		}
	}
}
