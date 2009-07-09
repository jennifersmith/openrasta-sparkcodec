using System;
using OpenRasta.Data;
using OpenRasta.Demo.Resources;
using OpenRasta.Web;

namespace OpenRasta.Demo.Handlers
{
	public class ShoppingListItemHandler
	{
		private readonly IShoppingListService _shoppingListService;

		public ShoppingListItemHandler(IShoppingListService shoppingListService)
		{
			_shoppingListService = shoppingListService;
		}

		public ShoppingListItem Get(string description)
		{
			return new ShoppingListItem();
		}

		public OperationResult Post(string description, ChangeSet<ShoppingListItem> changes)
		{
			return null;
			//ShoppingListItem item = ShoppingListHandler.ShoppingList.GetItem(description);
			//changes.Apply(item);
			//return new OperationResult.SeeOther {RedirectLocation = item.CreateUri()};
		}
	}
}