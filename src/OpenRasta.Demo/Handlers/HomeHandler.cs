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
using OpenRasta.Demo.Resources;
using OpenRasta.Web;

namespace OpenRasta.Demo.Handlers
{
	public class HomeHandler
	{
		public Home Get()
		{
			typeof(ShoppingList).CreateUri();
			return new Home() {Title = "Shopping List tool", Strapline = "Because a pen and paper would be too simple..."};
		}
	}
}
