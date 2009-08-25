using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenRasta.Demo.Resources
{
	public interface IShoppingListService
	{
		ShoppingList GetShoppingList();
		ShoppingListItem GetItem(int description);
	}

	public interface ISupermarketSectionService
	{
		IEnumerable<SupermarketSection> Select();
		SupermarketSection Select(string name);
	}

	public class SupermarketSectionService : ISupermarketSectionService
	{
		private static readonly List<SupermarketSection> superMarketSections = new List<SupermarketSection>( CreateDefaults());

		
		public IEnumerable<SupermarketSection> Select()
		{
			return superMarketSections;
		}

		private static IEnumerable<SupermarketSection> CreateDefaults()
		{
			yield return new SupermarketSection("Dairy");
			yield return new SupermarketSection("Fruit N Veg");
			yield return new SupermarketSection("Booze");
			yield return new SupermarketSection("Cleaning products");
			yield return new SupermarketSection("Bakery");
		}

		public SupermarketSection Select(string name)
		{
			bool exists = superMarketSections.Where(x => x.Name == name).Count()>0;
			if(!exists)
			{
				Insert(name);
			}
			return superMarketSections.Where(x => x.Name == name).First();
		}

		private static void Insert(string name)
		{
			var newSection = new SupermarketSection(name);
			superMarketSections.Add(newSection);
		}
	}
	public class ShoppingListService : IShoppingListService
	{
		private static readonly ShoppingList ShoppingList = CreateDefault();

		private static ShoppingList CreateDefault()
		{
			ISupermarketSectionService supermarketSectionService = new SupermarketSectionService();
			int id = 1;
			var result = new ShoppingList
			             	{
			             		new ShoppingListItem(id++)
			             			{
			             				Description = "Large Eggs",
			             				Quantity = "6",
			             				Notes = "Free range please!",
										Section = supermarketSectionService.Select("Dairy")
			             			}.WithImage("eggs.jpg"),
			             		new ShoppingListItem(id++)
			             			{
			             				Description = "Pints of Milk", 
										Quantity = "4",
										Section = supermarketSectionService.Select("Dairy")
			             			}.WithImage("milk.jpg"),
			             		new ShoppingListItem(id++)
			             			{
			             				Description = "Bag of Apples", 
										Section = supermarketSectionService.Select("Fruit N Veg"),
										Quantity = "1"
			             			},
			             		new ShoppingListItem(id)
			             			{
			             				Description = "Boxes of Wine", 
										Section = supermarketSectionService.Select("Booze"),
										Quantity = "4"
			             			}
			             	};
			return result;
		}
		public ShoppingList GetShoppingList()
		{
			return ShoppingList;
		}

		public ShoppingListItem GetItem(int id)
		{
			return ShoppingList.Where(x => x.Id == id).FirstOrDefault();
		}
	}
}
