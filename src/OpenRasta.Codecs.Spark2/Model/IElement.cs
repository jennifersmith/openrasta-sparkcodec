using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenRasta.Codecs.Spark2.Model
{
	public interface IElement : INode
	{
		string Name { get; }
		IAttribute AddAttribute(string attributeName);
		bool HasAttribute(string attribute);
		IAttribute GetAttribute(string attribute);
	}
	public interface IConditionalExpressionNode : INode
	{
		void SetExpressionBody(ConditionalExpression conditionalExpression);
	}
	public interface IAttribute : INode
	{
		string Name { get; }
		IConditionalExpressionNode AddConditionalExpressionNode();
		string GetTextValue();
		bool Exists();
	}
}
	