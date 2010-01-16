using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	public class TestAttributeNode :ContentContainingNode, IAttribute
	{
		private readonly string _value;

		public TestAttributeNode(string name, string value) : base(name)
		{
			_value = value;
		}
		public IEnumerable<TestConditionalExpressionNode> ConditionalExpressionNodes
		{
			get
			{
				return Nodes.Where(x => (x is TestConditionalExpressionNode)).Cast<TestConditionalExpressionNode>();
			}
		}
		public IEnumerable<ICodeExpressionNode> CodeNodes
		{
			get
			{
				return Nodes.Where(x => (x is ICodeExpressionNode)).Cast<ICodeExpressionNode>();
			}
		}

		public IConditionalExpressionNodeWrapper AddConditionalExpressionNode()
		{
			var codeExpressionNode = new TestConditionalExpressionNode();
			Nodes.Add(codeExpressionNode);
			return codeExpressionNode;
		}

		public ICodeExpressionNode AddCodeExpressionNode()
		{
			var node = new TestCodeExpressionNode();
			Nodes.Add(node);
			return node;
		}

		public string GetTextValue()
		{
			return _value;
		}

		public bool Exists()
		{
			return true;
		}
	}

}