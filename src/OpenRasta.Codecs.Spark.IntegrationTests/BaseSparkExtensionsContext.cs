using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Castle.Windsor;
using OpenRasta.Codecs.Spark.Tests.Stubs;
using OpenRasta.Codecs.Spark2.SparkInterface;
using OpenRasta.Codecs.Spark2.Specification;
using OpenRasta.Codecs.Spark2.Specification.Syntax;
using OpenRasta.Codecs.Spark2.Transformers;
using OpenRasta.Codecs.Spark2.ViewHelpers;
using OpenRasta.DI;
using OpenRasta.DI.Windsor;
using OpenRasta.Web;
using Spark;

namespace OpenRasta.Codecs.Spark.IntegrationTests
{
	public abstract class TestSparkView : AbstractSparkView
	{
		public TestViewData ViewData { set; get; }
		public IUriGenerator Uris { set; get; }
	}

	public class TestViewData
	{
		private readonly object resource;

		public TestViewData(object resource)
		{
			this.resource = resource;
		}


		public object Eval(string expression)
		{
			if (Matches(expression, "Resource"))
			{
				return resource;
			}
			return null;
		}

		private static bool Matches(string expression, string name)
		{
			return string.Equals(expression, name, StringComparison.CurrentCultureIgnoreCase);
		}

		public string Eval(string expression, string format)
		{
			// not sure if this will be used;
			return string.Format(format, Eval(expression));
		}
	}

	public class BaseSparkExtensionsContext : BaseContext
	{
		private static IWindsorContainer CreateTestDependencies()
		{
			var result = new WindsorContainer();
			result.AddComponent<ISparkExtensionFactory, CodecSparkExtensionFactory>();
			result.AddComponent<ISparkElementTransformerService, SparkElementTransformerService>();
			result.AddComponent<IElementTransformerService, ElementTransformerService>();
			result.AddComponent<ISpecificationProvider, DefaultSpecification>();
			
			// openrasta stuff
			var resolver = new WindsorDependencyResolver(result);
			resolver.AddDependency(typeof(IUriResolver), typeof(TestUriResolver), DependencyLifetime.Singleton);
			resolver.AddDependency(typeof(ICommunicationContext), typeof(TestCommunicationContext),DependencyLifetime.Singleton);
			DependencyManager.SetResolver(resolver);
			return result;
		}
		public string RenderTemplate(string templateSource, object data)
		{
			var settings = new SparkSettings
			               	{
			               		PageBaseType = typeof(TestSparkView).Name
			               	};
			settings.AddViewFolder(typeof (TestingViewFolder), new Dictionary<string, string>
			                                                   	{
			                                                   		{"templateSource", templateSource}
			                                                   	});

			settings.AddNamespace("OpenRasta.Codecs.Spark.Tests.TestObjects");
			settings.AddNamespace("OpenRasta.Codecs.Spark.IntegrationTests");

			IWindsorContainer dependencies = CreateTestDependencies();
			ISparkViewEngine sparkViewEngine = new SparkViewEngine(settings)
			                                   	{	
			                                   		ExtensionFactory =dependencies.Resolve<ISparkExtensionFactory>()
			                                   	};

			var descriptor = new SparkViewDescriptor();	
			descriptor.AddTemplate(TestingViewFolder.SingleTemplateName);
			var view = (TestSparkView)sparkViewEngine.CreateInstance(descriptor);
			view.Uris = new UriGenerator();
			view.ViewData = new TestViewData(data);
			return Render(view);
		}

		private static string Render(ISparkView view)
		{
			var builder = new StringBuilder();
			using (var writer = new StringWriter(builder))
			{
				view.RenderView(writer);
				writer.Flush();
			}
			return builder.ToString();
		}
	}
}