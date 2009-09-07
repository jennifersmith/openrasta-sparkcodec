using System.Collections.Generic;

namespace OpenRasta.Codecs.Spark2.Specification.Syntax
{
	public struct Tag
	{
		private readonly string _name;

		private Tag(string name)
		{
			_name = name;
		}

		public readonly static Tag AnchorTag = new Tag("A");
		public readonly static Tag FormTag = new Tag("form");
		public static IEnumerable<Tag> AllInputTags = new []{new Tag("Input"), new Tag("select"), new Tag("textarea")};

		public string Name
		{
			get { return _name; }
		}

	}
}