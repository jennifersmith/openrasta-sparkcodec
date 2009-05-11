using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace OpenRasta.Demo.Resources
{
	public class ShoppingList : IEnumerable<ShoppingListItem>
	{
		private readonly List<ShoppingListItem> list = new List<ShoppingListItem>();

		public IEnumerator<ShoppingListItem> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(ShoppingListItem item)
		{
			list.Add(item);
		}
	}
	public class ShoppingListItem
	{
		public string Description { get; set; }
		public string Notes { get; set; }
		public string Quantity { get; set; }
	}
}
