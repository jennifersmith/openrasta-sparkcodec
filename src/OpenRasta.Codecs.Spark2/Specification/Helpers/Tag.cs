using System;
using System.Collections.Generic;

namespace OpenRasta.Codecs.Spark2.Specification.Syntax
{
	public static class Tags
	{
		public readonly static Tag AnchorTag = new Tag("A");
		public readonly static Tag IframeTag = new Tag("iframe");
		public readonly static Tag ImageTag = new Tag("img");
		public readonly static Tag TextArea = new Tag("textarea");
		public readonly static Tag FormTag = new Tag("form");
		public readonly static Tag CheckTag = new Tag("check");
		public static IEnumerable<Tag> InputTags = new []{new Tag("Input"), new Tag("select"),TextArea};

	}
	public class Tag
	{
		private readonly string _name;

		public Tag(string name)
		{
			_name = name;
		}

		public string Name
		{
			get { return _name; }
		}

		public bool Equals(Tag other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(other._name, _name, StringComparison.InvariantCultureIgnoreCase);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Tag)) return false;
			return Equals((Tag) obj);
		}

		public override int GetHashCode()
		{
			return (_name != null ? _name.GetHashCode() : 0);
		}

		public static bool operator ==(Tag left, Tag right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Tag left, Tag right)
		{
			return !Equals(left, right);
		}
	}
}