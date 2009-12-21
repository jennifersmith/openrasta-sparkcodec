using System;
using System.Linq;

namespace OpenRasta.Codecs.Spark2.Model
{
	public class TagAttribute : IEquatable<TagAttribute>
	{
		private readonly string _name;
		private readonly string _value;

		public TagAttribute(string name, string value)
		{
			_name = name;
			_value = value;
		}

		public bool Equals(TagAttribute other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other._name, _name) && Equals(other._value, _value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (TagAttribute)) return false;
			return Equals((TagAttribute) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (_name.GetHashCode()*397) ^ _value.GetHashCode();
			}
		}

		public static bool operator ==(TagAttribute left, TagAttribute right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(TagAttribute left, TagAttribute right)
		{
			return !Equals(left, right);
		}
	}


	public class Tag
	{
		private readonly string _name;
		private readonly TagAttribute[] _tagAttributes;

		public Tag(string name, params TagAttribute[] tagAttributes)
		{
			_name = name;
			_tagAttributes = tagAttributes;
		}

		public string Name
		{
			get { return _name; }
		}

		public bool Matches(Tag tag)
		{
			return Name.Equals(tag.Name, StringComparison.InvariantCultureIgnoreCase) && TagHasAllAttributes(tag);
		}

		private bool TagHasAllAttributes(Tag tag)
		{
			return _tagAttributes.IsEmpty() || _tagAttributes.TrueForAll(x => tag._tagAttributes.Contains(x));
		}
	}
}