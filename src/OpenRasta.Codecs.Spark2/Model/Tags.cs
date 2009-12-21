using System.Collections.Generic;

namespace OpenRasta.Codecs.Spark2.Model
{
	public static class Tags
	{
		public readonly static Tag AnchorTag = new Tag("A");
		public readonly static Tag IframeTag = new Tag("iframe");
		public readonly static Tag ImageTag = new Tag("img");
		public readonly static Tag TextArea = new Tag("textarea");
		public readonly static Tag FormTag = new Tag("form");
		public readonly static Tag CheckTag = new Tag("input", new TagAttribute("type", "checkbox"));
		public static IEnumerable<Tag> InputTags = new []{new Tag("Input", new TagAttribute("type", "text")), new Tag("select"),TextArea, CheckTag};

	}
}