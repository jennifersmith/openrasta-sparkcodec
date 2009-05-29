using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenRasta.Codecs.Spark.Configuration;
using OpenRasta.Codecs.Spark.Tests.Stubs;
using OpenRasta.DI;
using OpenRasta.Web;
using PanelSystem.WorkingDays.Tests;
using Spark;

namespace OpenRasta.Codecs.Spark.Tests
{
	public class BaseSparkExtensionsContext : BaseContext
	{
		private SparkConfiguration sparkConfiguration;

		public override void CreateContext()
		{
			base.CreateContext();
			sparkConfiguration = new SparkConfiguration();
			var resolver = new InternalDependencyResolver();
			resolver.AddDependency(typeof (IUriResolver), typeof (TestUriResolver), DependencyLifetime.Singleton);
			resolver.AddDependency(typeof (ICommunicationContext), typeof (TestCommunicationContext),
			                       DependencyLifetime.Singleton);
			DependencyManager.SetResolver(resolver);
		}

		private void SetTemplateSource(string templateSource)
		{
			var settings = new SparkSettings
			               	{
			               		PageBaseType = typeof (SparkResourceView).Name
			               	};
			settings.AddViewFolder(typeof (TestingViewFolder), new Dictionary<string, string>
			                                                   	{
			                                                   		{"templateSource", templateSource}
			                                                   	});
			settings.AddNamespace("OpenRasta.Codecs.Spark");
			settings.AddNamespace("OpenRasta.Web.Markup");
			settings.AddNamespace("OpenRasta.Web");
			settings.AddNamespace("OpenRasta.Codecs.Spark.Tests.TestObjects");
			settings.AddNamespace("System.Collections.Generic");
			settings.AddNamespace("OpenRasta.Codecs.Spark.Extensions");
			sparkConfiguration
				.Container
				.SetService<ISparkSettings>(settings);
		}

		public string RenderTemplate(string templateSource, object data)
		{
			SetTemplateSource(templateSource);
			var descriptor = new SparkViewDescriptor();
			descriptor.AddTemplate(TestingViewFolder.SingleTemplateName);
			var engine = sparkConfiguration.Container.GetService<ISparkViewEngine>();
			var view = (SparkResourceView) engine.CreateInstance(descriptor);

			view.ViewData = new ViewData(data);
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