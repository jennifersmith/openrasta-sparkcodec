using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Reflection;
using OpenRasta.Web.Markup.Attributes;
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

		private void Transform(ElementNode node, IList<Node> body)
		{
			AttributeNode forAttribute = node.GetAttribute("for");
			AttributeNode forType = node.GetAttribute("fortype");
			AttributeNode forProperty = node.GetAttribute("forproperty");

			if(forAttribute!=null)
			{
				// put in as content property
				node.Attributes.Remove(forAttribute);
				node.RemoveAttributesByName("name");
				node.RemoveAttributesByName("value");
				var nameNode = new ExpressionNode(forAttribute.Value.GetPropertyNameSnippet());

				var valueNode = new ConditionNode("resource!=null")
				                     	{
				                     		Nodes = new List<Node>()
				                     		        	{
				                     		        		new ExpressionNode(forAttribute.Value)
				                     		        	}
				                     	};
				 SetNodeNameAndValue(node, valueNode, nameNode, body, forAttribute);
			}
			else if(forType!=null)
			{
				if(forProperty==null)
				{
					throw new Exception("Must have both a forProperty attribute if using the forType attribute.");
				}
				node.Attributes.Remove(forType);
				node.Attributes.Remove(forProperty);
				node.RemoveAttributesByName("name");
				 SetNodeNameAndValue(node, null, new TextNode(string.Concat(forType.Value, ".", forProperty.Value)), body, forAttribute);
			}

		}

		private List<Node> SetNodeNameAndValue(ElementNode elementNode, Node valueNode, Node nameNode, IList<Node> body, AttributeNode originalForAttrib)
		{
			List<Node> result = new List<Node>();
			if(elementNode.IsTag("textarea"))
			{
				if (valueNode != null)
				{
					elementNode.IsEmptyElement = false;
					body.Add(valueNode);
				}
			}
			else
			{
				string inputType = elementNode.GetAttributeValue("type");
				switch (inputType.ToUpper())
				{
					case "CHECKBOX":
						elementNode.Attributes.Add(new AttributeNode("value", "true"));
						// DODGY sorry will figure something decent out soon
						AddCheckedAttribute(elementNode, originalForAttrib);
						break;
					default:
						if (valueNode != null)
							elementNode.Attributes.Add(new AttributeNode("value", new List<Node>()
				                                               	{
				                                               		valueNode
				                                               	}));
						break;
				}
			}
			elementNode.Attributes.Add(new AttributeNode("name", new List<Node>()
			                                              	{
			                                              		nameNode
			                                              	}));
			return result;
		}

		private void AddCheckedAttribute(ElementNode elementNode, AttributeNode valueNode)
		{
			if (valueNode != null)
			{
				string entity = valueNode.Value.Split('.').First();
				string code = string.Format("({0}!= null)&&({1})", entity, valueNode.Value);
				ConditionNode conditionNode = new ConditionNode(code)
				                              	{
				                              		Nodes = new List<Node>()
				                              		        	{
				                              		        		new TextNode("true")
				                              		        	}
				                              	};
				elementNode.Attributes.Add(new AttributeNode("checked",
				                           		new List<Node>{conditionNode}
				                           	));
			}
		}

		public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
		{
			 Transform(node, body);

			 visitor.Accept(node);
			 visitor.Accept(body);
			if(!node.IsEmptyElement)
			{
				visitor.Accept(new EndElementNode(node.Name));
			}
		}

		public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
		{
			visitor.Accept(body);
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