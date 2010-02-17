using System;
using System.Collections.Generic;
using System.IO;
using Spark;

namespace OpenRasta.Codecs.Spark
{
	public interface ISparkRenderer
	{
		void Render(object viewData, TextWriter writer, IDictionary<string, string> configuration);
	}

	public class SparkRenderer : ISparkRenderer
	{
		private readonly ISparkViewResolver _sparkViewResolver;

		public SparkRenderer(ISparkViewResolver sparkViewResolver)
		{
			_sparkViewResolver = sparkViewResolver;
		}

		public void Render(object viewData, TextWriter writer, IDictionary<string, string> configuration)
		{
			ISparkView sparkView = _sparkViewResolver.Create(configuration[SparkConfigKeys.TemplateName], viewData);
			sparkView.RenderView(writer);
		}
	}
	public static class SparkConfigKeys
	{
		public const string TemplateName = "TemplateName";
	}
}