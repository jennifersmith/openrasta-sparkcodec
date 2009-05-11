using OpenRasta.Demo.Resources;
using OpenRasta.Web;

namespace OpenRasta.Demo.Handlers
{
	public class ShoppingListHandler
	{
		public static readonly ShoppingList ShoppingList = CreateDefault();

		private static ShoppingList CreateDefault()
		{
			var result = new ShoppingList();
			result.Add(new ShoppingListItem() {Description = "Large Eggs", Quantity = "6", Notes="Free range please!"});
			result.Add(new ShoppingListItem() {Description = "Pints of Milk", Quantity = "4"});
			result.Add(new ShoppingListItem() {Description = "Bag of Apples", Quantity = "1"});
			result.Add(new ShoppingListItem() {Description = "Boxes of Wine", Quantity = "4"});
			return result;
		}

		public ShoppingList Get()
		{
			return ShoppingList;
		}

		public OperationResult Post(ShoppingListItem item)
		{
			ShoppingList.Add(item);
			return new OperationResult.SeeOther() { RedirectLocation = typeof(ShoppingList).CreateUri() };
		}
	}
}