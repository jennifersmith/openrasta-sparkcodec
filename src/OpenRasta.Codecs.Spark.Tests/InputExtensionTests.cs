using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Web.Markup;
using PanelSystem.WorkingDays.Tests;

namespace OpenRasta.Codecs.Spark.Tests
{
	[TestFixture]
	public class InputExtensionTests : BaseSparkExtensionsContext
	{
		[Test]
		public void Input_type_text_renders_correctly_when_entity_is_null()
		{
			const string template = @"<viewdata resource=""TestEntity""/><input type=""text"" for=""resource.Name"" />";
			const string expected = @"<input type=""text"" name=""TestEntity.Name""/>";
			string actual = RenderTemplate(template, null);
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Input_type_text_renders_correctly_when_entity_has_a_value()
		{
			const string template = @"<viewdata resource=""TestEntity""/><input type=""text"" for=""resource.Name"" />";
			const string expected = @"<input type=""text"" value=""Fred"" name=""TestEntity.Name""/>";
			string actual = RenderTemplate(template, new TestEntity{Name = "Fred"});
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Input_type_text_renders_correctly_when_not_using_entity()
		{
			const string template = @"<input type=""text"" fortype=""TestEntity"" forproperty=""Name"" />";
			const string expected = @"<input type=""text"" name=""TestEntity.Name""/>";
			string actual = RenderTemplate(template, "The view data");
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Input_type_password_renders_correctly_when_entity_is_null()
		{
			const string template = @"<viewdata resource=""TestEntity""/><input type=""password"" for=""resource.Name"" />";
			const string expected = @"<input type=""password"" name=""TestEntity.Name""/>";
			string actual = RenderTemplate(template, null);
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Input_type_password_renders_correctly_when_entity_has_a_value()
		{
			const string template = @"<viewdata resource=""TestEntity""/><input type=""password"" for=""resource.Name"" />";
			const string expected = @"<input type=""password"" value=""Fred"" name=""TestEntity.Name""/>";
			string actual = RenderTemplate(template, new TestEntity { Name = "Fred" }); 
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Input_type_password_renders_correctly_when_not_using_entity()
		{
			const string template = @"<input type=""password"" fortype=""TestEntity"" forproperty=""Name"" />";
			const string expected = @"<input type=""password"" name=""TestEntity.Name""/>";
			string actual = RenderTemplate(template, "The view data");
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Textarea_renders_correctly_when_entity_is_null()
		{
			const string template = @"<viewdata resource=""TestEntity""/><textarea for=""resource.Name"" />";
			const string expected = @"<textarea name=""TestEntity.Name""></textarea>";
			string actual = RenderTemplate(template, null);
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Textarea_renders_correctly_when_entity_has_a_value()
		{
			const string template = @"<viewdata resource=""TestEntity""/><textarea for=""resource.Name"" />";
			const string expected = @"<textarea name=""TestEntity.Name"">Fred</textarea>";
			string actual = RenderTemplate(template, new TestEntity { Name = "Fred" });
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Textarea_renders_correctly_when_not_using_entity()
		{
			const string template = @"<textarea fortype=""TestEntity"" forproperty=""Name""/>";
			const string expected = @"<textarea name=""TestEntity.Name""/>";
			string actual = RenderTemplate(template, "The view data");
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Checkbox_renders_correctly_when_entity_is_null()
		{
			const string template = @"<viewdata resource=""TestEntity""/><input type=""checkbox"" for=""resource.Enabled"" />";
			const string expected = @"<input type=""checkbox"" value=""true"" name=""TestEntity.Enabled""/>";
			string actual = RenderTemplate(template, null);
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Checkbox_renders_correctly_when_entity_has_a_value_and_bool_field_is_true()
		{
			const string template = @"<viewdata resource=""TestEntity""/><input type=""checkbox"" for=""resource.Enabled"" />";
			const string expected = @"<input type=""checkbox"" value=""true"" checked=""true"" name=""TestEntity.Enabled""/>";
			string actual = RenderTemplate(template, new TestEntity(){Enabled = true});
			actual.ShouldEqual(expected);
		}
		[Test]
		public void Checkbox_renders_correctly_when_entity_has_a_value_and_bool_field_is_false()
		{
			const string template = @"<viewdata resource=""TestEntity""/><input type=""checkbox"" for=""resource.Enabled"" />";
			const string expected = @"<input type=""checkbox"" value=""true"" name=""TestEntity.Enabled""/>";
			string actual = RenderTemplate(template, new TestEntity() { Enabled = false });
			actual.ShouldEqual(expected);
		}

		[Test]
		public void Checkbox_renders_correctly_when_not_using_entity()
		{
			const string template = @"<input type=""checkbox"" fortype=""TestEntity"" forproperty=""Enabled"" />";
			const string expected = @"<input type=""checkbox"" value=""true"" name=""TestEntity.Enabled""/>";
			string actual = RenderTemplate(template, "The view data");
			actual.ShouldEqual(expected);
		}
	}
}
