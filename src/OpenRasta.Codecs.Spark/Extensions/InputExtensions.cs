using System;
using System.Collections.Generic;
using System.Text;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	internal class InputExtensions : ISparkExtension
	{
		private readonly ElementNode node;

		public InputExtensions(ElementNode node)
		{
			this.node = node;
		}

		public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
		{
			visitor.Accept(body);
		}

		public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
		{
			if (location == OutputLocation.RenderMethod)
			{
				string forValue = node.GetAttributeValue("for");
				string forType = node.GetAttributeValue("forType");
				string forProperty = node.GetAttributeValue("forProperty");
				string inputMethod = GetInputMethod(node.Name, node.GetAttributeValue("type"));
				string forAccessor;
				if(!string.IsNullOrEmpty(forValue))
				{
					forAccessor = "()=>" + forValue;
				}
				else if (!string.IsNullOrEmpty(forProperty))
				{
					forAccessor = "x=>x." + forProperty;
				}
				else
				{
					throw new InvalidOperationException("input must have either for attribute or fortype and forproperty attribute");
				}
				string typeParameters = "";
				if(!string.IsNullOrEmpty(forType))
				{
					typeParameters = "<" + forType + ">";
				}
				string attributesFluent = node.GetAttributesAsFluentString("for", "fortype", "forproperty", "type");
				//todo:RF this beatch
				output.AppendFormat("Output.Write(Xhtml.{3}{0}({1}){2});\r\n", typeParameters, forAccessor, attributesFluent,inputMethod);
			}
		}

		private string GetInputMethod(string nodeName, string typeValue)
		{
			if(nodeName.ToUpper()=="TEXTAREA")
			{
				return "TextArea";
			}
			switch (typeValue.ToUpper())
			{
				case "TEXT":
					return "TextBox";
				case "PASSWORD":
					return "Password";
			}
			throw new Exception("Unrecognised input type attribute: " + typeValue);
		}
	}
}