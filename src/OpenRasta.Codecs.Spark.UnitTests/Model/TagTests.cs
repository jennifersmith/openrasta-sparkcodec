	using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
	using NUnit.Framework;
	using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark.UnitTests.Model
{
	[TestFixture]
	public class TagTests
	{
		[Test]
		public void TagShouldMatchIfTagnamesMatchAndHaveNoAttributes()
		{
			var tag = new Tag("hello");
			var tagTwo = new Tag("HELLO");
			tag.Matches(tagTwo).ShouldBeTrue();
		}
		[Test]
		public void TagsShouldntMatchIfDifferentName()
		{
			var tag = new Tag("hello");
			var tagTwo = new Tag("goodbye");
			tag.Matches(tagTwo).ShouldBeFalse();
		}
		[Test]
		public void TagsShouldMatchIfSameNameAndMatchingTagHasAllAttributesOfFirstTag()
		{
			var tag = new Tag("hello", new TagAttribute("anAttribute", "this"));
			var tagToMatch = new Tag("hello", new TagAttribute("anAttribute", "this"), new TagAttribute("attrib1", "that"), new TagAttribute("anotherAttrib", "x"));
			tag.Matches(tagToMatch).ShouldBeTrue();
		}
		[Test]
		public void TagsShouldNotMatchIfSameNameAndMatchingTagDoesntHaveAllAttributesOfFirstTag()
		{
			var tag = new Tag("hello", new TagAttribute("anAttribute", "this"), new TagAttribute("thisAttribute", "that"));
			var tagToMatch = new Tag("hello", new TagAttribute("anAttribute", "this"), new TagAttribute("attrib1", "that"), new TagAttribute("anotherAttrib", "x"));
			tag.Matches(tagToMatch).ShouldBeFalse();
		}
	}
}
