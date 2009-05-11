using System;
using System.IO;
using Spark.FileSystem;

namespace OpenRasta.Codecs.Spark.Tests
{
	internal class TestViewFile : IViewFile
	{
		private string templateSource;
		private DateTime modified;

		public TestViewFile(string templateSource)
		{
			this.templateSource = templateSource;
			this.modified = DateTime.Now;
		}

		public Stream OpenViewStream()
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter writer = new StreamWriter(memoryStream);
			writer.Write(templateSource);
			writer.Flush();
			memoryStream.Seek(0, SeekOrigin.Begin);
			return memoryStream;
		}

		public long LastModified
		{
			get
			{
				return DateTime.Now.Ticks;
			}
		}
	}
}