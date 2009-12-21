using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Specification.Actions
{
	public class RemoveAttributeAction : IElementTransformerAction
	{
		private readonly string _attributeName;

		public RemoveAttributeAction(string attributeName)
		{
			_attributeName = attributeName;
		}

		public void Do(IElement element)
		{
			if(element.HasAttribute(_attributeName))
			{
				element.RemoveAttribute(element.GetAttribute(_attributeName));
			}
		}
	}
}