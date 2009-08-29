using System;
using System.IO;
using System.Text;
using OpenRasta.Codecs.Spark.Configuration;
using OpenRasta.Codecs.Spark.Tests.Contexts;
using OpenRasta.Codecs.Spark.Tests.Stubs;
using OpenRasta.DI;
using OpenRasta.Web;
using PanelSystem.WorkingDays.Tests;
using Spark;

namespace OpenRasta.Codecs.Spark.Tests
{
	public class BaseSparkExtensionsContext : BaseContext
	{
		public override void CreateContext()
		{
			base.CreateContext();
			var resolver = new InternalDependencyResolver();
			resolver.AddDependency(typeof (IUriResolver), typeof (TestUriResolver), DependencyLifetime.Singleton);
			resolver.AddDependency(typeof (ICommunicationContext), typeof (TestCommunicationContext),
			                       DependencyLifetime.Singleton);
			DependencyManager.SetResolver(resolver);
		}
		public string RenderTemplate(string templateSource, object data)
		{
			var testSparkConfiguration =new TestSparkConfiguration(new SparkCodecNamespacesConfiguration(), templateSource);
			var sparkServiceContainerFactory = new SparkServiceContainerFactory(testSparkConfiguration);
			var descriptor = new SparkViewDescriptor();
			descriptor.AddTemplate(TestingViewFolder.SingleTemplateName);
			var engine = sparkServiceContainerFactory.CreateServiceContainer().GetService<ISparkViewEngine>();
			var view = (SparkResourceView) engine.CreateInstance(descriptor);
			view.ViewData = new ViewData(data);
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