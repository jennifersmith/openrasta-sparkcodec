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
	public interface ICodeExpressionNode : INode
	{
		void SetExpressionBody(CodeExpression expression);
	}
	public interface IConditionalExpressionNodeWrapper : INode
	{
		void SetExpressionBody(ConditionalExpression conditionalExpression);
		void SetExpressionBody(CodeExpression codeExpression);
	}
	public interface IAttribute : INode
	{
		string Name { get; }
		IConditionalExpressionNodeWrapper AddConditionalExpressionNode();
		ICodeExpressionNode AddCodeExpressionNode();
		string GetTextValue();
		bool Exists();
	}
}
	