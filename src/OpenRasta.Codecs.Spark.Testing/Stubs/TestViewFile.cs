using System;
using System.IO;
using Spark.FileSystem;

namespace OpenRasta.Codecs.Spark.Tests.Stubs
{
	internal class TestViewFile : IViewFile
	{
		private readonly string templateSource;
		private DateTime modified;

		public TestViewFile(string templateSource)
		{
			this.templateSource = templateSource;
			modified = DateTime.Now;
		}

		#region IViewFile Members

		public Stream OpenViewStream()
		{
			var memoryStream = new MemoryStream();
			var writer = new StreamWriter(memoryStream);
			writer.Write(templateSource);
			writer.Flush();
			memoryStream.Seek(0, SeekOrigin.Begin);
			return memoryStream;
		}

		public long LastModified
		{
			get { return DateTime.Now.Ticks; }
		}

		#endregion
	}
}