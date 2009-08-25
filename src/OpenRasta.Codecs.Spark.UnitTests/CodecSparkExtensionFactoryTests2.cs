using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class CodecSparkExtensionFactoryTests 
	{
		[Test]
		public void ShouldReturnWrappedNodeExtensionForApplicableNodes()
		{
			CodecSparkExtensionFactory codecSparkExtensionFactory = new CodecSparkExtensionFactoryBuilder();

			ISparkExtension extension = codecSparkExtensionFactory.CreateExtension(new VisitorContext(),
			                                           new ElementNode(TestElementDescriptors.OverridableTagName, new List<AttributeNode>(), true));

			extension.ShouldNotBeNull();
		}
	}
	[TestFixture]
	public class SparkExtensionWrapperTests
	{
		[Test]
		public void VisitChunkShouldJustPassOnToBody()
		{
			//SparkExtensionWrapper wrapper = new SparkExtensionWrapper(new ElementNodeDescriptor());
			//IChunkVisitor chunkVisitor = MockRepository.GenerateStub<IChunkVisitor>();
			//List<Chunk> body = new List<Chunk>();

			//wrapper.VisitChunk(chunkVisitor, OutputLocation.ClassMembers, body, new StringBuilder());
			
			//chunkVisitor.AssertWasCalled(x=>x.Accept(body));
		}

		public void VisitNodeShouldCallWrappedClassToStartTheElement()
		{
			SparkExtensionWrapper wrapper = new SparkExtensionWrapper(new ElementNodeDescriptor());
			INodeVisitor visitor = new NodeVisitor(new VisitorContext());
				wrapper.VisitNode(null, null, null);

		}
	}

	public static class NodeWriterExtensions
	{
		public static void ShouldBeWrappingNodeVisitor(INodeWriter writer, INodeVisitor visitor)
		{
			writer.As<SparkNodeWriter>().ShouldEqual(new SparkNodeWriter(visitor));
		}
	}

	public static class SparkExtensionWrapperExtensions
	{
		public struct WriteNodeParams
		{
			public WriteNodeParams(INodeWriter writer, IList<NodeDescriptor> body)
			{
			}
		}
	}
	public class CodecSparkExtensionFactoryBuilder
	{
		private StubElementOverrideFactory _elementOverrideFactory;

		public static implicit operator CodecSparkExtensionFactory (CodecSparkExtensionFactoryBuilder builder)
		{
			return builder.Build();
		}

		public CodecSparkExtensionFactory Build()
		{
			_elementOverrideFactory = new StubElementOverrideFactory();
			return new CodecSparkExtensionFactory(_elementOverrideFactory);
		}
	}
	public static class TestElementDescriptors
	{
		public const string OverridableTagName = "overridableTag";
		public const string NonOverridableTagName = "notAnOverridable";
		public readonly static ElementNodeDescriptor OverrideableElement = new ElementNodeDescriptor(TestElementDescriptors.OverridableTagName);
	}

	public class StubElementOverrideFactory : IElementOverrideFactory
	{
		public bool Overrideable(ElementNodeDescriptor element)
		{
			return element.Equals(TestElementDescriptors.OverrideableElement);
		}

	}
}
