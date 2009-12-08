using System;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class SparkNodeWrapper<NodeType> : ISparkNodeWrapper, INode
		where NodeType : Node
	{
		private readonly NodeType _wrappedNode;

		public SparkNodeWrapper(NodeType node)
		{
			_wrappedNode = node;
		}

		public virtual Node GetWrappedNode()
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

		public bool Equals(SparkNodeWrapper<NodeType> other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other._wrappedNode, _wrappedNode);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if ((obj is SparkNodeWrapper<NodeType>)==false) return false;
			return Equals((SparkNodeWrapper<NodeType>) obj);
		}

		public override int GetHashCode()
		{
			return _wrappedNode.GetHashCode();
		}

		public static bool operator ==(SparkNodeWrapper<NodeType> left, SparkNodeWrapper<NodeType> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(SparkNodeWrapper<NodeType> left, SparkNodeWrapper<NodeType> right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return string.Format("WrappedNode: {0}", _wrappedNode);
		}
	}
}