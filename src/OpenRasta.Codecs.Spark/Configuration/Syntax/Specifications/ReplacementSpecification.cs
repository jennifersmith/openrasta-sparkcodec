using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions.Specifications
{
	internal class ReplacementSpecification
	{
		private readonly string elementName;
		private readonly string newAttributeName;
		private readonly string originalAttributeName;

		public ReplacementSpecification(string elementName, string originalAttributeName)
			: this(elementName, originalAttributeName, "")
		{
		}

		public ReplacementSpecification(string elementName, string originalAttributeName, string newAttributeName)
		{
			this.elementName = elementName;
			this.originalAttributeName = originalAttributeName;
			this.newAttributeName = newAttributeName;
		}

		public string NewAttributeName
		{
			get { return newAttributeName; }
		}

		public string OriginalAttributeName
		{
			get { return originalAttributeName; }
		}

		public bool IsMatch(ElementNode node)
		{
			return node.IsTag(elementName) && node.HasAttribute(originalAttributeName);
		}
	}
}