using System;
using System.IO;
using System.Web;
using OpenRasta.Data;
using OpenRasta.Demo.Resources;
using OpenRasta.Web;

namespace OpenRasta.Demo.Handlers
{
	public class ShoppingListItemHandler
	{
		private readonly IShoppingListService _shoppingListService;

		public ShoppingListItemHandler()
		{
			_shoppingListService = new ShoppingListService();// temp!
		}

		public ShoppingListItem Get(int id)
		{
			return _shoppingListService.GetItem(id);
		}
		public OperationResult Post(int id)
		{

			ShoppingListItem item = _shoppingListService.GetItem(id);
			//diff.Apply(item);
			if(item.NewImage.Length>0)
			{
				using(Stream newImageStream =item.NewImage.OpenStream())
				{
					// I am basically crap at this...
					string path = HttpContext.Current.Server.MapPath("~/images/") + item.NewImage.FileName;
					var buffer = new byte[newImageStream.Length];
					newImageStream.Read(buffer, 0, buffer.Length);
					File.WriteAllBytes(path, buffer);
					
				}
				item.Image = new ShoppingListItemImage(item.NewImage.FileName, item);
			}
			return new OperationResult.SeeOther {RedirectLocation = item.CreateUri()};
		}
	}
}