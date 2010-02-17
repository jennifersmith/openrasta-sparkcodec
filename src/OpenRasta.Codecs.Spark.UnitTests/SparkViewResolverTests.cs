using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Spark;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class SparkViewResolverTests
	{
		[Test]
		public void UsesTheSparkEngineToCreateAView()
		{
			var expectedSparkView = MockRepository.GenerateStub<ISparkView>();
			var sparkViewEngine = MockRepository.GenerateStub<ISparkViewEngine>()
				.StubCreateInstance(expectedSparkView);
			sparkViewEngine.StubCreateInstance(expectedSparkView);
			var resolver = new SparkViewResolverBuilder().With(sparkViewEngine).Build();

			ISparkView resolvedView = resolver.Create(null, null);

			Assert.That(resolvedView, Is.EqualTo(expectedSparkView));
		}
		[Test]	
		public void PassesTheSparkDescriptorIncludingTheTemplateNameToTheView()
		{
			const string viewName = "myView";
			var sparkViewEngine = MockRepository.GenerateStub<ISparkViewEngine>().StubCreateInstance();
			var resolver = new SparkViewResolverBuilder().With(sparkViewEngine).Build();

			resolver.Create(viewName,null);

			var sparkViewDescriptor = (SparkViewDescriptor)sparkViewEngine.GetArgumentsForSingleCall(x => x.CreateInstance(null));
			Assert.That(sparkViewDescriptor.Templates.FirstOrDefault(), Is.EqualTo(viewName));
		}
	}
}