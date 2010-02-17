using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs;
using OpenRasta.Web;

namespace OpenRasta.Demo.Resources
{
	public class ShoppingList : IEnumerable<ShoppingListItem>
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
		private readonly int _id;

		public ShoppingListItem(int id)
		{
			_id = id;
		}

		public string Description { get; set; }
		public string Notes { get; set; }
		public string Quantity { get; set; }
		public bool Optional { get; set; }
		public ShoppingListItemImage Image { get; set; }
		public SupermarketSection Section { get; set; }
		public HttpEntityFile NewImage
		{
			get;set;
		}
		public bool HasImage 
		{ 
			get
			{
				return Image != null;
			}
		}
		public int Id
		{
			get
			{
				return _id;
			}
		}
	}

	public struct SupermarketSection
	{
		private readonly string _name;

		public SupermarketSection(string name)
		{
			_name = name;
		}

		public string Name
		{
			get
			{
				return _name;
			}
		}
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
