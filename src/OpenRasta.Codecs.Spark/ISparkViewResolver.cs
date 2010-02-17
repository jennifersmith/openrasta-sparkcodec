using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark.Configuration;
using Spark;

namespace OpenRasta.Codecs.Spark
{
	public interface ISparkViewResolver
	{
		ISparkView Create(string name, object viewData);
	}
	public class SparkViewResolverWithServiceContainerWrapper : ISparkViewResolver
	{
		private readonly ISparkViewResolver _wrapped;
		
		public SparkViewResolverWithServiceContainerWrapper(ISparkServiceContainerFactory sparkServiceContainerFactory)
		{
			ISparkServiceContainer container = sparkServiceContainerFactory.CreateServiceContainer();
			_wrapped = new SparkViewResolver(container.GetService<ISparkViewEngine>());
		}

		public ISparkView Create(string name, object viewData)
		{
			return _wrapped.Create(name, viewData);
		}
	}
	public class SparkViewResolver : ISparkViewResolver
	{
		private readonly ISparkViewEngine _sparkViewEngine;

		public SparkViewResolver(ISparkViewEngine sparkViewEngine)
		{
			_sparkViewEngine = sparkViewEngine;
		}

		public ISparkView Create(string name, object viewData)
		{
			return _sparkViewEngine.CreateInstance(new SparkViewDescriptor().AddTemplate(name));
		}
	}
}