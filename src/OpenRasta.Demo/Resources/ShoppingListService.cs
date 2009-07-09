using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenRasta.Demo.Resources
{
	public interface IShoppingListService
	{
		IShoppingList GetShoppingList();
	}

	public class ShoppingListService : IShoppingListService
	{
		private static readonly ShoppingList ShoppingList = CreateDefault();
		private static ShoppingList CreateDefault()
		{
			var result = new ShoppingList
			             	{
			             		new ShoppingListItem()
			             			{
			             				Description = "Large Eggs",
			             				Quantity = "6",
			             				Notes = "Free range please!"
			             			}.WithImage("eggs.jpg"),
			             		new ShoppingListItem
			             			{
			             				Description = "Pints of Milk", 
										Quantity = "4"
			             			}.WithImage("milk.jpg"),
			             		new ShoppingListItem
			             			{
			             				Description = "Bag of Apples", 
										Quantity = "1"
			             			},
			             		new ShoppingListItem()
			             			{
			             				Description = "Boxes of Wine", 
										Quantity = "4"
			             			}
			             	};
			return result;
		}
		public IShoppingList GetShoppingList()
		{
			return ShoppingList;
		}
	}
}
