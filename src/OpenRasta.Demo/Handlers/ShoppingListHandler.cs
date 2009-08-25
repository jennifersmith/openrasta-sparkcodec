using System;
using OpenRasta.Demo.Resources;
using OpenRasta.Web;

namespace OpenRasta.Demo.Handlers
{
	public class ShoppingListHandler
	{
		private readonly IShoppingListService _shoppingListService;


		public ShoppingListHandler()
		{
			_shoppingListService = new ShoppingListService();
			// temp!
		}


		public ShoppingList Get()
		{
			return _shoppingListService.GetShoppingList();
		}

		public OperationResult Post(ShoppingListItem item)
		{
			ShoppingList shoppingList = _shoppingListService.GetShoppingList();
			shoppingList.Add(item);
			return new OperationResult.SeeOther() { RedirectLocation = shoppingList.CreateUri()};
		}
	}
}