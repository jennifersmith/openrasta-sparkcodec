using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using OpenRasta.Web;
using Rhino.Mocks;
using Spark;
using OpenRasta.Collections.Specialized;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class SparkCodecTests
	{

		[Test]
		public void WriteToUsesExistingTextWriterIfHttpEntitySupportsTextWriter()
		{
			var renderer = MockRepository.GenerateStub<ISparkRenderer>();
			var textWriter = new StringWriter();
			var supportsTextWriter = MockRepository.GenerateStub<IHttpEntitySupportingTextWriter>();
			supportsTextWriter.Stub(x => x.TextWriter).Return(textWriter);

			var sparkCodec = new SparkCodecBuilder().With(renderer).Build();
			sparkCodec.Configuration = new object();

			sparkCodec.WriteTo(new object(), supportsTextWriter, new[] {"A", "B"});

			renderer.AssertWasCalled(x => x.Render(
				Arg<object>.Is.Anything,
				Arg<TextWriter>.Is.Equal(textWriter),
				Arg<Dictionary<string,string>>.Is.Anything
				));
		}
		[Test]
		public void WriteToPassesViewDataToRender()
		{
			var renderer = MockRepository.GenerateStub<ISparkRenderer>();
			var sparkCodec = new SparkCodecBuilder().With(renderer).Build();

			var viewData = new object();

			sparkCodec.WriteTo(viewData, MockRepository.GenerateStub<IHttpEntitySupportingTextWriter>(), new[] { "A", "B" });

			renderer.AssertWasCalled(x => x.Render(
				Arg<object>.Is.Equal(viewData),
				Arg<TextWriter>.Is.Anything,
				Arg<Dictionary<string, string>>.Is.Anything
				));
		}
		[Test]
		public void WritePassesConfigObjectAsDictionaryToRender()
		{
			var renderer = MockRepository.GenerateStub<ISparkRenderer>();
			var sparkCodec = new SparkCodecBuilder().With(renderer).Build();

			var configData = new{foo="bar",bat=123};
			IDictionary<string, string> expectedConfiguration = configData.ToCaseInvariantDictionary();
			sparkCodec.Configuration = configData;

			sparkCodec.WriteTo(new object(), MockRepository.GenerateStub<IHttpEntitySupportingTextWriter>(), new[] { "A", "B" });

			renderer.AssertWasCalled(x => x.Render(
				Arg<object>.Is.Anything,
				Arg<TextWriter>.Is.Anything,
				Arg<Dictionary<string, string>>.Is.Equal(expectedConfiguration)
				));
		}

		[Test]
		public void WriteWritesToStreamIfHttpEntityDoesNotSupportTextWriter()
		{
			const string expectedContents = "abcdefg";
			var renderer = new StubSparkRendererThatWritesStuff(expectedContents);
			SparkCodec sparkCodec = new SparkCodecBuilder().With(renderer).Build();

			var httpEntity = MockRepository.GenerateStub<IHttpEntity>();
			var stream = new MemoryStream();
			httpEntity.Stub(x => x.Stream).Return(stream);

			sparkCodec.WriteTo(null, httpEntity, new[]{"a", "b"});

			stream.Seek(0, SeekOrigin.Begin);
			var reader = new StreamReader(stream);
			Assert.That(reader.ReadToEnd(), Is.EqualTo(expectedContents));
		}
		[Test]
		public void WriteFlushesToStreamButDoesntCloseItIfHadToCreateItsOwnWriter()
		{
			SparkCodec sparkCodec = new SparkCodecBuilder().Build();
			var httpEntity = MockRepository.GenerateStub<IHttpEntity>();
			var streamToWrite = MockRepository.GenerateStub<Stream>();
			streamToWrite.Stub(x => x.CanWrite).Return(true);
			httpEntity.Stub(x => x.Stream).Return(streamToWrite);

			sparkCodec.WriteTo(null, httpEntity, new[] { "a", "b" });

			streamToWrite.AssertWasCalled(x => x.Flush());
			streamToWrite.AssertWasNotCalled(x => x.Close());
		}
		[Test]
		public void WriteCreatesDeterministicWriterWithCorrectEncoding()
		{
			const string funnyCharacters = "úĀąü";
			ISparkRenderer renderer = new StubSparkRendererThatWritesStuff(funnyCharacters);
			SparkCodec sparkCodec = new SparkCodecBuilder().With(renderer).Build();
			var httpEntity = MockRepository.GenerateStub<IHttpEntity>();
			var stream = new MemoryStream();
			Assert.That(stream.CanRead, Is.True);
			httpEntity.Stub(x => x.Stream).Return(stream);

			sparkCodec.WriteTo(null, httpEntity, new[] { "a", "b" });

			stream.Seek(0, SeekOrigin.Begin);
			var reader = new StreamReader(stream, new UTF8Encoding());
			Assert.That(reader.ReadToEnd(), Is.EqualTo(funnyCharacters));
		}

	}

	public class StubSparkRendererThatWritesStuff : ISparkRenderer
	{
		private readonly string _stuffToWrite;

		public StubSparkRendererThatWritesStuff(string stuffToWrite)
		{
			_stuffToWrite = stuffToWrite;
		}

		public void Render(object viewData, TextWriter writer, IDictionary<string, string> configuration)
		{
			writer.Write(_stuffToWrite);
		}
	}

	public interface IHttpEntitySupportingTextWriter : IHttpEntity, ISupportsTextWriter
	{
		
	}
	public class StubSparkConfiguration : ISparkConfiguration
	{
		public ISparkSettings CreateSettings()
		{
			return MockRepository.GenerateStub<ISparkSettings>();
		}
	}
	public class StubSparkRenderer : ISparkRenderer
	{
		public void Render(object viewData, TextWriter writer, IDictionary<string, string> configuration)
		{
		}
	}

	public class SparkCodecBuilder
	{
		private ISparkRenderer _sparkRenderer = new StubSparkRenderer();
		public SparkCodec Build()
		{
			return new SparkCodec(_sparkRenderer)
			       	{
			       		Configuration = new object()
			       	};
		}
		public SparkCodecBuilder With(ISparkRenderer sparkRenderer)
		{
			_sparkRenderer = sparkRenderer;
			return this;
		}
	}
}
