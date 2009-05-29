using System.Collections.Generic;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	internal abstract class SpecifiedReplacement : IReplacement
	{
		private readonly ReplacementSpecification replacementSpecification;

		protected SpecifiedReplacement(ReplacementSpecification replacementSpecification)
		{
			this.replacementSpecification = replacementSpecification;
		}

		protected ReplacementSpecification ReplacementSpecification
		{
			get { return replacementSpecification; }
		}

		#region IReplacement Members

		public abstract void DoReplace(ElementNode node, IList<Node> body);

		#endregion

		protected static void AddAttribute(ElementNode elementNode, string attributeName, Node childNode)
		{
			elementNode.RemoveAttributesByName(attributeName);
			elementNode.Attributes.Add(new AttributeNode(attributeName, new List<Node>
			                                                            	{
			                                                            		childNode
			                                                            	}));
		}
	}
}