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
		public IEnumerable<ICodeExpressionNode> CodeNodes
		{
			get
			{
				return Nodes.Where(x => (x is ICodeExpressionNode)).Cast<ICodeExpressionNode>();
			}
		}

		public ICodeExpressionNode AddExpression()
		{
			var codeExpressionNode = new TestCodeExpressionNode();
			Nodes.Add(codeExpressionNode);
			return codeExpressionNode;
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