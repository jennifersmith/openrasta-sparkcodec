using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Web.Markup;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark
{

	internal class TextBoxExtension : ISparkExtension
	{
		private readonly ElementNode node;

		public TextBoxExtension(ElementNode node)
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
				// todo: this prob could handle most other input contros....
				string forValue = node.GetAttributeValue("for");
				string forType = node.GetAttributeValue("forType");
				string forProperty = node.GetAttributeValue("forProperty");
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
					throw new InvalidOperationException("textbox must have either for attribute or fortype and forproperty attribute");
				}
				string typeParameters = "";
				if(!string.IsNullOrEmpty(forType))
				{
					typeParameters = "<" + forType + ">";
				}
				string attributesFluent = node.GetAttributesAsFluentString("for", "fortype", "forproperty");
				output.AppendFormat("Output.Write(Xhtml.TextBox{0}({1}){2});\r\n", typeParameters, forAccessor, attributesFluent);
			}
		}

	}
	internal class FormExtension : ISparkExtension
	{
		private readonly ElementNode node;

		public FormExtension(ElementNode node)
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
				//todo: rf
				string forType = node.GetAttributeValue("forType");
				string forValue = node.GetAttributeValue("for");
				string fluentMethods = node.GetAttributesAsFluentString("forType", "for");
				if (!string.IsNullOrEmpty(forType))
				{
					output.AppendLine(string.Format("using (scope(Xhtml.Form<{0}>(){1})){{", forType, fluentMethods));
					visitor.Accept(body);
					output.AppendLine("}");
				}
				else
				{
					output.AppendLine(string.Format("using (scope(Xhtml.Form({0}){1})){{", forValue, fluentMethods));
					visitor.Accept(body);
					output.AppendLine("}");

				}
			}
		}
	}
}