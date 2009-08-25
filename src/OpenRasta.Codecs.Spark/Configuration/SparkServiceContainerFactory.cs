using Spark;

namespace OpenRasta.Codecs.Spark.Configuration
{
	public class SparkServiceContainerFactory : ISparkServiceContainerFactory
	{
		private ISparkConfiguration _sparkConfiguration;

		public SparkServiceContainerFactory(ISparkConfiguration sparkConfiguration)
		{
			_sparkConfiguration = sparkConfiguration;
		}

		public ISparkServiceContainer CreateServiceContainer()
		{
			var sparkServiceContainer = new SparkServiceContainer();

			sparkServiceContainer.SetServiceBuilder<ISparkSettings>(c =>_sparkConfiguration.CreateSettings());
			sparkServiceContainer.SetServiceBuilder<ISparkViewEngine>(c => new SparkViewEngine(c.GetService<ISparkSettings>())
			                                                               	{
			                                                               		ExtensionFactory = new OpenRastaSparkExtensionsFactory()
			                                                               	});
			return sparkServiceContainer;
		}
	}
}