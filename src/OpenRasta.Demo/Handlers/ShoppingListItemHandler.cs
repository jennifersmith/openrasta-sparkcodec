using System;
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
using OpenRasta.Data;
using OpenRasta.Demo.Resources;
using OpenRasta.Reflection;
using OpenRasta.Web;

namespace OpenRasta.Demo.Handlers
{
	public class ShoppingListItemHandler
	{
		public ShoppingListItem Get(string description)
		{
			return GetItem(description);
		}

		private ShoppingListItem GetItem(string description)
		{
			return ShoppingListHandler.ShoppingList.Where(x => x.Description.ToUpper() == description.ToUpper()).FirstOrDefault();
		}

		public OperationResult Post(string description, ChangeSet<ShoppingListItem> changes)
		{
			var item = GetItem(description);
			changes.Apply(item);
			return new OperationResult.SeeOther {RedirectLocation = item.CreateUri()};
		}
	}
}
