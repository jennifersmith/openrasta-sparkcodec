using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests.SparkInterface
{
	[TestFixture]
	public class SparkElementWrapperTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new SparkElementWrapperTestContext();
		}

		[Test]
		public void ShouldAddAnAttributeToOriginalNodeOnAddAttribute()
		{
			GivenAnOriginalElement(SparkTestNodes.BasicElementNode());
			WhenAnAttributeIsAdded("theAttribute");
			ThenTheWrappedElementShouldHaveABlankAttributeNamed("theAttribute");
		}
		[Test]
		public void ShouldReturnWrappedAttributeNodeWhenAttributeIsAdded()
		{
			GivenAnOriginalElement(SparkTestNodes.BasicElementNode());
			WhenAnAttributeIsAdded("theAttribute");
			ThenTheReturnedAttributeShouldBeWrappedSparkAttributeNamed("theAttribute");
		}
		[Test]
		public void ShouldReturnTrueForHasAttributeIfUnderlyingNodeHasAttribute()
		{
			GivenAnOriginalElement(SparkTestNodes.BasicElementNode());
			WhenAnAttributeIsAdded("theAttribute");
			ThenHasAttributeShouldBeTrueFor("theAttribute");
		}
		[Test]
		public void ShouldReturnFalseForHasAttributeIfUnderlyingNodeDoesntHaveAttribute()
		{
			GivenAnOriginalElement(SparkTestNodes.BasicElementNode());
			ThenHasAttributeShouldBeFalseFor("theAttribute");
		}

		[Test]
		public void ShouldReturnTrueForHasAttributeIfUnderlyingNodeHasAttributeWithDifferentCase()
		{
			GivenAnOriginalElement(SparkTestNodes.BasicElementNode());
			WhenAnAttributeIsAdded("theATTriBUTE");
			ThenHasAttributeShouldBeTrueFor("theAttribute");
		}

		[Test]
		public void GetAttributeShouldReturnEmptyAttributreNodeIfTheAttributeDoesntExist()
		{
			GivenAnOriginalElement(SparkTestNodes.BasicElementNode());
			WhenAttributeIsRetreived("thisAttribute");
			ThenGetAttributeResultShouldBeEmpty();
		}

		[Test]
		public void ShouldReturnWrappedAttributeNodeIfItExists()
		{
			AttributeNode attributeNode = SparkTestNodes.BasicAttributeNode("thisAttribute");
			ElementNode node = SparkTestNodes.BasicElementNode().WithAttribute(attributeNode);
			GivenAnOriginalElement(node);
			WhenAttributeIsRetreived("thisAttribute");
			ThenRetrievedAttributeShouldWrap(attributeNode);
		}


		[Test]
		public void NameShouldReturnTheNameOfTheWrappedNode()
		{
			GivenAnOriginalElement(SparkTestNodes.ElementNode("foo"));
			ThenNodeNameShouldBe("foo");
		}

		[Test]
		public void RemoveAttributeShouldRemoveFromTheWrappedNode()
		{
			AttributeNode attributeNode = SparkTestNodes.BasicAttributeNode("fred");
			GivenAnOriginalElement(SparkTestNodes.ElementNode("foo").WithAttribute(attributeNode));
			WhenAttributeIsRemoved(new SparkAttributeWrapper(attributeNode));
			ThenTheElementShouldNotContainAttribute(attributeNode);
		}

		[Test]
		public void AddCodeExpressionShouldAddNewCodeExpressionNodeToElement()
		{
			GivenAnOriginalElement(SparkTestNodes.ElementNode("foo"));
			WhenCodeExpressionIsAdded();
			ThenTheElementShouldHaveOneCodeElementInItsChildren();
		}
		[Test]
		public void AddCodeExpressionShouldReturnTheWrappedNode()
		{
			ElementNode node = SparkTestNodes.ElementNode("foo");
			GivenAnOriginalElement(node);
			WhenCodeExpressionIsAdded();
			ThenAddCodeExpressionAttributeShouldWrapBodyNode();
		}

		[Test]
		public void AddCodeConditionalExpressionShouldAddNewCodeConditionalExpressionNodeToElement()
		{
			GivenAnOriginalElement(SparkTestNodes.ElementNode("foo"));
			WhenConditionalExpressionAreAdded();
			ThenTheElementShouldHaveOneConditionalElementInItsChildren();
		}

		private void ThenTheElementShouldHaveOneConditionalElementInItsChildren()
		{
			Context.Target.Body.Single().ShouldBe<ConditionNode>();
		}

		[Test]
		public void AddCodeConditionalExpressionShouldReturnTheWrappedNode()
		{
			ElementNode node = SparkTestNodes.ElementNode("foo");
			GivenAnOriginalElement(node);
			WhenConditionalExpressionAreAdded();
			ThenAddCodeConditionalExpressionAttributeShouldWrapBodyNode();
		}

		private void ThenAddCodeConditionalExpressionAttributeShouldWrapBodyNode()
		{
			ConditionNode bodyNode = Context.Target.Body.Single().As<ConditionNode>();
			Context.AddConditionalExpressionResult.Unwrap().ShouldEqual(bodyNode);
		}

		[Test]
		public void ClearInnerTextShouldRemoveAllTextNodesFromBody()
		{
			ElementNode node = SparkTestNodes.ElementNode("bar");
			GivenAnOriginalElement(node, new TextNode("A text"), new TextNode("abc"));
			WhenInnerTextCleared();
			ThenNodeHasNoBody();
		}

		private void ThenNodeHasNoBody()
		{
			Context.Target.Body.Any().ShouldBeFalse();
		}

		private void WhenConditionalExpressionAreAdded()
		{
			Context.AddConditionalExpressionResult = Context.Target.AddConditionalExpressionNode();
		}

		private void WhenInnerTextCleared()
		{
			Context.Target.ClearInnerText();
		}


		private void ThenAddCodeExpressionAttributeShouldWrapBodyNode()
		{
			ExpressionNode bodyNode = Context.Target.Body.Single().As<ExpressionNode>();
			Context.AddCodeExpressionResult.Unwrap().ShouldEqual(bodyNode);
		}

		private void ThenTheElementShouldHaveOneCodeElementInItsChildren()
		{
			Context.Target.Body.Single().ShouldBe<ExpressionNode>();
		}

		private void WhenCodeExpressionIsAdded()
		{
			Context.AddCodeExpressionResult = Context.Target.AddCodeExpressionNode();
		}

		private void ThenTheElementShouldNotContainAttribute(AttributeNode node)
		{
			Context.Target.Unwrap().As<ElementNode>().Attributes.ShouldNotContain(x=>x.Name == node.Name);
		}

		private void WhenAttributeIsRemoved(SparkAttributeWrapper wrapper)
		{
			Context.Target.RemoveAttribute(wrapper);
		}

		private void ThenNodeNameShouldBe(string nodeName)
		{
			Context.Target.Name.ShouldEqual(nodeName);
		}

		private void ThenRetrievedAttributeShouldWrap(Node attributeNode)
		{
			Context.GetAttributeResult.ShouldNotBeNull();
			Context.GetAttributeResult.Unwrap().ShouldEqual(attributeNode);
		}

		private void ThenGetAttributeResultShouldBeEmpty()
		{
			Context.GetAttributeResult.Unwrap().ShouldBeNull();
		}

		private void WhenAttributeIsRetreived(string attributeName)
		{
			Context.GetAttributeResult = Context.Target.GetAttribute(attributeName);
		}

		private void ThenHasAttributeShouldBeFalseFor(string attributeName)
		{
			Context.Target.HasAttribute(attributeName).ShouldBeFalse();
		}

		private void ThenHasAttributeShouldBeTrueFor(string attributeName)
		{
			Context.Target.HasAttribute(attributeName).ShouldBeTrue();
		}


		private void ThenTheReturnedAttributeShouldBeWrappedSparkAttributeNamed(string attributeName)
		{
			Context.AddAttributeResult.Unwrap().As<AttributeNode>().Name.ShouldEqual(attributeName);
		}

		private void ThenTheWrappedElementShouldHaveABlankAttributeNamed(string attribute)
		{
			Context.Target.CurrentNode.Attributes.ShouldContain(x => x.Name == attribute);
			AttributeNode attributeNode = Context.Target.CurrentNode.Attributes.Where(x => x.Name == attribute).First();
			attributeNode.Value.ShouldBeEmpty();
		}

		private void WhenAnAttributeIsAdded(string attributeName)
		{
			Context.AddAttributeResult =  Context.Target.AddAttribute(attributeName);
		}

		private void GivenAnOriginalElement(ElementNode elementNode)
		{
			Context.Target = new SparkElementWrapper(elementNode, new Node[0]);
			Context.OriginalElement = elementNode;
		}

		private void GivenAnOriginalElement(ElementNode node, params TextNode[] textNodes)
		{
			Context.Target = new SparkElementWrapper(node, textNodes);
			Context.OriginalElement = node;
		}

		public SparkElementWrapperTestContext Context { get; set; }

		public class SparkElementWrapperTestContext
		{
			public ICodeExpressionNode AddCodeExpressionResult;
			public SparkElementWrapper Target { get; set; }

			public IAttribute AddAttributeResult { get; set; }

			public IAttribute GetAttributeResult { get; set; }

			public ElementNode OriginalElement { get; set; }

			public IConditionalExpressionNodeWrapper AddConditionalExpressionResult { get; set; }
		}
	}
}
