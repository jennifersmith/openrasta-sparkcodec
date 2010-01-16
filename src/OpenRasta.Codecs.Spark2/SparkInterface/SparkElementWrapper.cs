using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class	SparkElementWrapper : IElement, IEquatable<SparkElementWrapper>, ISparkNodeWrapper
	{
		private readonly List<Node> _body;
		private readonly ElementNode _wrappedNode;

		public SparkElementWrapper(ElementNode wrappedNode, IEnumerable<Node> body)
		{
			_wrappedNode = wrappedNode;
			_body = new List<Node>(body);
		}

		public ElementNode CurrentNode
		{
			get { return _wrappedNode; }
		}

		public IList<Node> Body
		{
			get { return _body; }
		}

		#region IElement Members

		public string Name
		{
			get { return CurrentNode.Name; }
		}

		public IAttribute AddAttribute(string attributeName)
		{
			var attributeNode = new AttributeNode(attributeName, "");
			CurrentNode.Attributes.Add(attributeNode);
			return new SparkAttributeWrapper(attributeNode);
		}

		public bool HasAttribute(string attribute)
		{
			return GetAttributeByName(attribute) != null;
		}

		public IAttribute GetAttribute(string attribute)
		{
			AttributeNode attributeNode = GetAttributeByName(attribute);
			return new SparkAttributeWrapper(attributeNode);
		}

		public void RemoveAttribute(IAttribute attribute)
		{
			AttributeNode existingAttribute = CurrentNode.Attributes.Where(x => x.Name == attribute.Name).FirstOrDefault();
			if (existingAttribute != null)
			{
				CurrentNode.Attributes.Remove(existingAttribute);
			}
		}

		public ICodeExpressionNode AddCodeExpressionNode()
		{
			ExpressionNode expressionNode = new ExpressionNode("");
			_body.Add(expressionNode);
			return new  SparkCodeExpressionNodeWrapper(expressionNode);
		}

		public IConditionalExpressionNodeWrapper AddConditionalExpressionNode()
		{
			ConditionNode expressionNode = new ConditionNode();
			_body.Add(expressionNode);
			return new SparkConditionNodeWrapper(expressionNode);
		}

		public void ClearInnerText()
		{
			Body.Where(x => x is TextNode).ToArray().ForEach(x => Body.Remove(x));
		}

		public IEnumerable<IElement> GetChildElements(string name)
		{
			return BodyElementNodes().Where(x=>x.HasName(name)).WrapAll();
		}


		private IEnumerable<ElementNode> BodyElementNodes()
		{
			return Body.Where(x => x is ElementNode).Cast<ElementNode>();
		}

		#endregion

		public Node GetWrappedNode()
		{
			return _wrappedNode;
		}

		private AttributeNode GetAttributeByName(string attribute)
		{
			return CurrentNode.Attributes.Where(x => AttributeNameEquals(x, attribute)).FirstOrDefault();
		}

		private static bool AttributeNameEquals(AttributeNode x, string attribute)
		{
			return x.Name.Equals(attribute, StringComparison.InvariantCultureIgnoreCase);
		}

		public override string ToString()
		{
			return "SparkElementWrapper for element : " + CurrentNode.Name;
		}

		public bool Equals(SparkElementWrapper other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other._body.SequenceEqual(_body) && Equals(other._wrappedNode, _wrappedNode);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (SparkElementWrapper)) return false;
			return Equals((SparkElementWrapper) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (_body.GetHashCode()*397) ^ _wrappedNode.GetHashCode();
			}
		}

		public static bool operator ==(SparkElementWrapper left, SparkElementWrapper right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(SparkElementWrapper left, SparkElementWrapper right)
		{
			return !Equals(left, right);
		}
	}
}