using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Specification.Actions
{
	public class ConvertAttributeAction : IElementTransformerAction
	{
		private readonly string _toAttribute;
		private readonly string _fromAttribute;
		private readonly IAttributeModifer _attributeModifer;

		public ConvertAttributeAction(string toAttribute, string fromAttribute, IAttributeModifer attributeModifer)
		{
			_toAttribute = toAttribute;
			_fromAttribute = fromAttribute;
			_attributeModifer = attributeModifer;
		}

		public void Do(IElement element)
		{
			if (element.HasAttribute(_fromAttribute) == false)
			{
				return;
			}
			IAttribute fromAttribute = element.GetAttribute(_fromAttribute);
			IAttribute attribute = element.AddAttribute(_toAttribute);
			_attributeModifer.Modify(fromAttribute, attribute);
		}
	}
}