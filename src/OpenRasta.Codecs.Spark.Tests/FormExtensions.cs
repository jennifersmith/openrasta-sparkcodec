using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace OpenRasta.Codecs.Spark.Tests
{
	[TestFixture]
	public class FormExtensionsTests : BaseSparkExtensionsContext
	{
		[Test]
		public void	Rendering_form_with_type_uri_works()
		{
			const string input = @"<form method=""post"" fortype=""IEnumerable<TestEntity>"" />";
			const string expected = @"<form method=""post"" action=""http://localhost" + TestUriResolver.TestEntitiesUriString + @"""/>";
			string actual = RenderTemplate(input, null);
			Assert.That(actual, Is.EqualTo(expected));
		}
		[Test]
		public void Rendering_form_with_inner_html_works()
		{
			const string input = @"<form method=""post"" fortype=""IEnumerable<TestEntity>"">Preserve<i>This</i> markup and stuff please</form>";
			const string expected = @"<form method=""post"" action=""http://localhost" + TestUriResolver.TestEntitiesUriString + @""">Preserve<i>This</i> markup and stuff please</form>";
			string actual = RenderTemplate(input, null);
			Assert.That(actual, Is.EqualTo(expected));
		}
		[Test]
		public void Rendering_form_with_entity_url_works()
		{
			const string input = @"<viewdata resource=""TestEntity""/><form method=""post"" for=""resource"">Preserve<i>This</i> markup and stuff please</form>";
			string expected = @"<form method=""post"" action=""http://localhost" + string.Format(TestUriResolver.TestEntityFormatString, "TheEntity") + @""">Preserve<i>This</i> markup and stuff please</form>";
			string actual = RenderTemplate(input, new TestEntity(){Name="TheEntity"});
			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
