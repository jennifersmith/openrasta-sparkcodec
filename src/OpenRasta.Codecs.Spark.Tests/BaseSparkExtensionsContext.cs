using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenRasta.Codecs.Spark.Configuration;
using OpenRasta.DI;
using OpenRasta.Web;
using Spark;

namespace OpenRasta.Codecs.Spark.Tests
{
	public class BaseSparkExtensionsContext : PanelSystem.WorkingDays.Tests.BaseContext
	{

		private SparkConfiguration sparkConfiguration;

		public override void CreateContext()
		{
			base.CreateContext();
			sparkConfiguration = new SparkConfiguration();
			InternalDependencyResolver resolver = new InternalDependencyResolver();
			resolver.AddDependency(typeof(IUriResolver), typeof(TestUriResolver), DependencyLifetime.Singleton);
			resolver.AddDependency(typeof(ICommunicationContext), typeof(TestCommunicationContext), DependencyLifetime.Singleton);
			DependencyManager.SetResolver(resolver);

		}
		private void SetTemplateSource(string templateSource)
		{
			SparkSettings settings = new SparkSettings()
			                         	{
			                         		PageBaseType = typeof(SparkResourceView).Name
				                         		
			                         	};
			settings.AddViewFolder(typeof(TestingViewFolder), new Dictionary<string, string>()
			                                                  	{
			                                                  		{"templateSource", templateSource}
			                                                  	});
			settings.AddNamespace("OpenRasta.Codecs.Spark");
			settings.AddNamespace("OpenRasta.Web.Markup");
			settings.AddNamespace("OpenRasta.Codecs.Spark.Tests");
			settings.AddNamespace("System.Collections.Generic");
			sparkConfiguration
				.Container
				.SetService<ISparkSettings>(settings);
		}
		public string RenderTemplate(string templateSource, object data)
		{
			SetTemplateSource(templateSource);
			SparkViewDescriptor descriptor = new SparkViewDescriptor();
			descriptor.AddTemplate(TestingViewFolder.SingleTemplateName);
			var engine = sparkConfiguration.Container.GetService<ISparkViewEngine>();
			SparkResourceView view = (SparkResourceView) engine.CreateInstance(descriptor);

			view.ViewData = new ViewData(data);
			StringBuilder builder = new StringBuilder();
			using(StringWriter writer = new StringWriter(builder))
			{
				view.RenderView(writer);
				writer.Flush();
			}
			return builder.ToString();
		}
	}
}