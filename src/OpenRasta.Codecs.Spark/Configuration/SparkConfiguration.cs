using System;
using System.Collections.Generic;
using Spark;
using Spark.FileSystem;

namespace OpenRasta.Codecs.Spark.Configuration
{
	public class SparkConfiguration : ISparkConfiguration
	{
		private readonly ISparkCodecNamespacesConfiguration _sparkCodecNamespacesConfiguration;

		public SparkConfiguration(ISparkCodecNamespacesConfiguration sparkCodecNamespacesConfiguration)
		{
			_sparkCodecNamespacesConfiguration = sparkCodecNamespacesConfiguration;
		}

		#region ISparkConfiguration Members


		public ISparkSettings CreateSettings()
		{
			var result = new SparkSettings
			{
				PageBaseType = typeof(SparkResourceView).Name
			};
			AddViewFolder(result);
			_sparkCodecNamespacesConfiguration.AddNamespaces(result);
			return result;
		}

		#endregion

	

		private static void AddViewFolder(SparkSettings result)
		{
			result.AddViewFolder(ViewFolderType.VirtualPathProvider,
			                     new Dictionary<string, string> {{"virtualBaseDir", "~/views/"}});
		}
	}
}