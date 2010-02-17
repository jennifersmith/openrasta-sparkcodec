using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Rhino.Mocks;
using Spark;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class SparkRendererTests
	{
		[Test]
		public void RenderUsesCorrectlyNamedViewFromResolverToWriteToWriter()
		{
			string viewName = "fooView";
			var viewData = new {X = "b", Y = new {A = "C"}};
			var sparkViewResolver = MockRepository.GenerateStub<ISparkViewResolver>();
			ISparkView sparkview = MockRepository.GenerateStub<ISparkView>();
			sparkViewResolver.Stub(x => x.Create(viewName, viewData)).Return(sparkview);
			var configuration = new Dictionary<string, string> {{"TemplateName", viewName}};
			var sparkRenderer = new SparkRendererBuilder().With(sparkViewResolver).Build();
			TextWriter writer = MockRepository.GenerateStub<TextWriter>();

			sparkRenderer.Render(viewData, writer, configuration);

			sparkview.AssertWasCalled(x=>x.RenderView(writer));
		}
	}
}