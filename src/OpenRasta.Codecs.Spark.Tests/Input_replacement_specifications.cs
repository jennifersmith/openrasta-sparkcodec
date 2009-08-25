using System;
using Input_replacement_specifications.With_scenario_of;
using NUnit.Framework;
using OpenRasta.Codecs.Spark.Tests;
using OpenRasta.Codecs.Spark.Tests.Assertions;
using OpenRasta.Codecs.Spark.Tests.Extensions;
using OpenRasta.Codecs.Spark.Tests.TestObjects;

namespace Input_replacement_specifications
{
	namespace With_scenario_of
	{
		public abstract class Using_spark_codec_to_render_an_input_element : BaseSparkExtensionsContext
		{
			public abstract string TemplateSource { get; }

			public string TemplateResult { get; private set; }

			public TestEntity Entity { get; private set; }

			public void WithTestEntity()
			{
				WithTestEntity(true);
			}

			public void WithTestEntity(bool enabled)
			{
				Entity = new TestEntity
				         	{
				         		Description = "This is it",
				         		Name = "THENAME",
				         		Enabled = enabled
				         	};
			}

			public override void When()
			{
				base.When();
				TemplateResult = RenderTemplate(TemplateSource, Entity);
			}
		}
	}

	namespace Given_that_testentity_is_null
	{
		[TestFixture]
		public class When_rendering_an_input_with_a_for_attribute : Using_spark_codec_to_render_an_input_element
		{
			public override string TemplateSource
			{
				get
				{
					return
						@"<viewdata resource=""TestEntity""/> <input type=""text"" for=""resource.Name"" anotherattribute=""leave this alone""/>";
				}
			}

			[Test]
			public void Name_is_set_to_the_property_path()
			{
				TemplateResult.HasElement("input").WithAttribute("name").ShouldHaveValue("TestEntity.Name");
			}

			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("input").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}

			[Test]
			public void value_is_empty()
			{
				TemplateResult.HasElement("input").WithoutAttribute("value");
			}
		}

		[TestFixture]
		public class When_rendering_a_textarea_with_a_for_attribute : Using_spark_codec_to_render_an_input_element
		{
			public override string TemplateSource
			{
				get
				{
					return
						@"<viewdata resource=""TestEntity""/> <textarea for=""resource.Name"" anotherattribute=""leave this alone"">This should be replaced</textarea>";
				}
			}

			[Test]
			public void innter_text_is_empty()
			{
				TemplateResult.HasElement("textarea").Value.ShouldBeEmpty();
			}

			[Test]
			public void Name_is_set_to_the_property_path()
			{
				TemplateResult.HasElement("textarea").WithAttribute("name").ShouldHaveValue("TestEntity.Name");
			}

			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("textarea").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
		}

		[TestFixture]
		public class When_rendering_a_select_with_a_for_attribute : Using_spark_codec_to_render_an_input_element
		{
			public override string TemplateSource
			{
				get
				{
					return
						@"<viewdata resource=""TestEntity""/> <select for=""resource.Name"" anotherattribute=""leave this alone""><option value=""nothing"" selected=""true"">Blah</option></select>";
				}
			}

			[Test]
			public void Name_is_set_to_the_property_path()
			{
				TemplateResult.HasElement("select").WithAttribute("name").ShouldHaveValue("TestEntity.Name");
			}

			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("select").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}

			[Test]
			public void Selected_item_is_maintained()
			{
				TemplateResult
					.HasElement("select")
					.HasElement(x => x.HasAttributeValue("value", "nothing"))
					.WithAttribute("selected").ShouldHaveValue("true");
			}
		}
	}

	namespace Given_that_testentity_is_set_to_value
	{
		[TestFixture]
		public class When_rendering_an_input_with_a_for_attribute : Using_spark_codec_to_render_an_input_element
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithTestEntity();
			}

			public override string TemplateSource
			{
				get
				{
					return
						@"<viewdata resource=""TestEntity""/> <input type=""text"" for=""resource.Name"" anotherattribute=""leave this alone""/>";
				}
			}

			[Test]
			public void Name_is_set_to_the_property_path()
			{
				TemplateResult.HasElement("input").WithAttribute("name").ShouldHaveValue("TestEntity.Name");
			}

			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("input").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}

			[Test]
			public void value_is_set_correcty_from_testentity()
			{
				TemplateResult.HasElement("input").WithAttribute("value").ShouldHaveValue(Entity.Name);
			}
		}

		[TestFixture]
		public class When_rendering_a_textarea_with_a_for_attribute : Using_spark_codec_to_render_an_input_element
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithTestEntity();
			}

			public override string TemplateSource
			{
				get
				{
					return
						@"<viewdata resource=""TestEntity""/> <textarea for=""resource.Name"" anotherattribute=""leave this alone"">This should be replaced</textarea>";
				}
			}

			[Test]
			public void inner_text_is_set_to_TestEntity_name()
			{
				TemplateResult.HasElement("textarea").Value.ShouldEqual(Entity.Name);
			}

			[Test]
			public void Name_is_set_to_the_property_path()
			{
				TemplateResult.HasElement("textarea").WithAttribute("name").ShouldHaveValue("TestEntity.Name");
			}

			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("textarea").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
		}

		[TestFixture]
		public class When_rendering_a_check_box_with_a_for_attribute_with_boolean_true : Using_spark_codec_to_render_an_input_element
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithTestEntity(true);
			}
			public override string TemplateSource
			{
				get
				{
					return
						@"<viewdata resource=""TestEntity""/> 
							<input for=""resource.Enabled"" type=""checkbox"" />";
				}
		
			}
			[Test]
			public void Checked_should_be_set()
			{
				TemplateResult.HasElement("input").WithAttribute("checked");
			}
		}

		[TestFixture]
		public class When_rendering_a_check_box_with_a_for_attribute_with_boolean_false : Using_spark_codec_to_render_an_input_element
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithTestEntity(false);
			}

			public override string TemplateSource
			{
				get
				{
					return
						@"<viewdata resource=""TestEntity""/> 
							<input for=""resource.Enabled"" type=""checkbox"" />";
				}

			}
			[Test]
			public void Checked_attribute_should_not_be_set()
			{
				TemplateResult.HasElement("input").WithoutAttribute("checked");
			}
		}
		[TestFixture]
		public class When_rendering_a_select_with_a_for_attribute : Using_spark_codec_to_render_an_input_element
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithTestEntity();
			}

			public override string TemplateSource
			{
				get
				{
					return
						@"<viewdata resource=""TestEntity""/> <select for=""resource.Name"" anotherattribute=""leave this alone"">
							<option value=""nothing"" selected=""true"">Blah</option>
							<option value=""" +
						Entity.Name + @""" >The right one</option>
						</select>";
				}
			}

			[Test]
			public void Name_is_set_to_the_property_path()
			{
				TemplateResult.HasElement("select").WithAttribute("name").ShouldHaveValue("TestEntity.Name");
			}

			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("select").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}

			[Test]
			public void Selected_item_is_updated()
			{
				TemplateResult
					.HasElement("select")
					.HasElement(x => x.HasAttributeValue("value", Entity.Name))
					.WithAttribute("selected").ShouldHaveValue("true");
			}
		}
	}
}