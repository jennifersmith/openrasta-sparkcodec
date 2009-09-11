using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			Context.Target = new SparkElementWrapper(elementNode);
		}

		public SparkElementWrapperTestContext Context { get; set; }

		public class SparkElementWrapperTestContext
		{
			public SparkElementWrapper Target { get; set; }

			public IAttribute AddAttributeResult { get; set; }

			public IAttribute GetAttributeResult { get; set; }
		}
	}
}
