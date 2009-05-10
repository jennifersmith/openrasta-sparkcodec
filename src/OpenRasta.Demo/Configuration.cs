using OpenRasta.Codecs.Spark;
using OpenRasta.Configuration;
using OpenRasta.Demo.Handlers;
using OpenRasta.Demo.Resources;
using OpenRasta.Web.UriDecorators;

namespace OpenRasta.Demo
{
	public class Configuration : IConfigurationSource
	{
		#region IConfigurationSource Members

		public void Configure()
		{
			// OR resources config has to be done in the scope below, afterwards it's made immutable/read-only
			using (OpenRastaConfiguration.Manual)
			{
				// custom view engines that comes with OR, see example usage on "~/Views/WidgetList.aspx"

				// OR extension that allows to override http method/verb by specifying it in the URI
				// e.g. http://local!options overrides it to options verb
				// see example usage on WidgetListHandler & "~/Views/WidgetList.aspx"
				ResourceSpace.Uses.UriDecorator<HttpMethodOverrideUriDecorator>();

				// OR extension that allows to override content-type negotiation by specifying explict type in the uri
				// e.g. http://local/home.xml overrides it to application/xml
				ResourceSpace.Uses.UriDecorator<ContentTypeExtensionUriDecorator>();

				ResourceSpace.Has.ResourcesOfType<Home>()
					.AtUri("/home")
					.AndAt("/")
					.HandledBy<HomeHandler>()
					.AndRenderedBySpark("home.spark");

				ResourceSpace.Has.ResourcesOfType<ShoppingList>()
					.AtUri("/shoppinglist")
					.HandledBy<ShoppingListHandler>()
					.AndRenderedBySpark("shoppingList.spark");

				ResourceSpace.Has.ResourcesOfType<ShoppingListItem>()
					.AtUri("/shoppinglistitem/{description}")
					.HandledBy<ShoppingListItemHandler>()
					.AndRenderedBySpark("shoppinglistitem.spark");
			}
		}

		#endregion
	}
}