using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark.Configuration;
using OpenRasta.Codecs.Spark.Tests.Stubs;
using Spark;

namespace OpenRasta.Codecs.Spark.Tests.Contexts
{
	public class TestSparkConfiguration: ISparkConfiguration
	{
		private ISparkCodecNamespacesConfiguration _sparkCodecNamespacesConfiguration;
		private string _templateSource;

		public TestSparkConfiguration(ISparkCodecNamespacesConfiguration sparkCodecNamespacesConfiguration, string templateSource)
		{
			_sparkCodecNamespacesConfiguration = sparkCodecNamespacesConfiguration;
			_templateSource = templateSource;
		}

		public ISparkSettings CreateSettings()
		{
			var settings = new SparkSettings
			               	{
			               		PageBaseType = typeof (SparkResourceView).Name
			               	};
			settings.AddViewFolder(typeof (TestingViewFolder), new Dictionary<string, string>
			                                                   	{
			                                                   		{"templateSource", _templateSource}
			                                                   	});
			new SparkCodecNamespacesConfiguration().AddNamespaces(settings);
			settings.AddNamespace("OpenRasta.Codecs.Spark.Tests.TestObjects");
			return settings;
		}

		public void Configure(object configuration)
		{
			throw new NotImplementedException();
		}
	}
}