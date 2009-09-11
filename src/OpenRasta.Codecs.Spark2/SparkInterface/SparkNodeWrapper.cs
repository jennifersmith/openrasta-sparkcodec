using System;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class SparkNodeWrapper<NodeType> : SparkNodeWrapper, INode
		where NodeType : Node
	{
		private readonly NodeType _wrappedNode;

		public SparkNodeWrapper(NodeType node)
		{
			_wrappedNode = node;
		}

		public override Node GetWrappedNode()
		{
			return _wrappedNode;
		}

		public NodeType CurrentNode
		{
			get
			{
				return _wrappedNode;
			}
		}
		public string Name
		{
			get { throw new NotImplementedException(); }
		}
	}
}