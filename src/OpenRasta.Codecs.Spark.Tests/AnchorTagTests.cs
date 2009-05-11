using NUnit.Framework;
using PanelSystem.WorkingDays.Tests;

namespace OpenRasta.Codecs.Spark.Tests
{
	[TestFixture]
	public class AnchorTagTests : BaseSparkExtensionsContext
	{
		[Test]
		public void Renders_anchor_for_list_of_testentitytype_with_forType_attribute()
		{
			// so.. much hardcoding
			const string template = @"<a forType=""IEnumerable<TestEntity>"">InnerText</a>";
			const string expected = @"<a href=""http://localhost/TestEntities"">InnerText</a>";
			string actual = RenderTemplate(template, null);
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Renders_anchor_for_list_of_testentity_with_for_attribute()
		{
			TestEntity entity = new TestEntity();
			entity.Name = "fred";
			const string template = @"<viewdata resource=""TestEntity""><a for=""resource"">InnerText</a>";
			const string expected = @"<a href=""http://localhost/TestEntity/fred"">InnerText</a>";
			string actual = RenderTemplate(template, entity);
			actual.ShouldEqual(expected);
		}
	}
}