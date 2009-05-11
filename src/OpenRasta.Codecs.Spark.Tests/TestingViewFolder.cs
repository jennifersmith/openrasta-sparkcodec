using System.Collections.Generic;
using Spark.FileSystem;

namespace OpenRasta.Codecs.Spark.Tests
{
	internal class TestingViewFolder : IViewFolder
	{
		private readonly string templateSource;
		public const string SingleTemplateName = "MyTemplate";

		public TestingViewFolder(string templateSource)
		{
			this.templateSource = templateSource;
		}

		public IViewFile GetViewSource(string path)
		{
			if(path==SingleTemplateName)
			{
				return new TestViewFile(templateSource);
			}
			return null;
		}

		public IList<string> ListViews(string path)
		{
			return new List<string>(){path};
		}

		public bool HasView(string path)
		{
			return path==SingleTemplateName;
		}
	}
}