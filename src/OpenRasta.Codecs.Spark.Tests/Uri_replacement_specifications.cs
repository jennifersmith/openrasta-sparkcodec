using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Codecs.Spark.Tests;
using PanelSystem.WorkingDays.Tests;

namespace Uri_replacement_specifications
{
	namespace With_scenario_of
	{
		public abstract class Using_spark_codec_to_render_an_element_with_a_uri_attributes : BaseSparkExtensionsContext
		{
			public abstract string TemplateSource
			{
				get;
			}
			public  void WithTestEntity()
			{
				Entity = new TestEntity()
				         	{
				         		Description = "This is it",
				         		Name = "THENAME",
				         		Enabled = true
				         	};
			}
			public override void When()
			{
				base.When();
				this.TemplateResult = RenderTemplate(TemplateSource, Entity);
			}

			public string TemplateResult { get;private  set; }

			public TestEntity Entity { get; private set; }
		}
	}

	namespace Given_that_testentity_is_null
	{

		[TestFixture]
		public class When_rendering_a_tag_with_a_templated_href_using_entity_reference : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override string TemplateSource
			{
				get { return @"<viewdata resource=""TestEntity""/> <a to=""resource"" anotherattribute=""leave this alone"">Inner text</a>"; }
			}
			[Test]
			public void href_is_empty()
			{
				TemplateResult.HasElement("a").WithoutAttribute("href");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("a").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("a").Value.ShouldEqual("Inner text");
			}
		}

		[TestFixture]
		public class When_rendering_a_tag_with_a_templated_href_using_entity_type : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override string TemplateSource
			{
				get { return @"<a totype=""IEnumerable<TestEntity>"" anotherattribute=""leave this alone"">Inner text</a>"; }
			}
			[Test]
			public void href_resolves_to_correct_uri_for_type()
			{
				TemplateResult.HasElement("a").WithAttribute("href").ShouldHaveValue("http://localhost/TestEntities");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("a").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("a").Value.ShouldEqual("Inner text");
			}
		}

		[TestFixture]
		public class When_rendering_iframe_tag_with_a_templated_src_using_entity_reference : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override string TemplateSource
			{
				get { return @"<viewdata resource=""TestEntity""/> <iframe to=""resource"" anotherattribute=""leave this alone"">Inner text</iframe>"; }
			}
			[Test]
			public void src_is_empty()
			{
				TemplateResult.HasElement("iframe").WithoutAttribute("src");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("iframe").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("iframe").Value.ShouldEqual("Inner text");
			}
		}

		[TestFixture]
		public class When_rendering_iframe_tag_with_a_templated_src_using_entity_type : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override string TemplateSource
			{
				get { return @"<iframe totype=""IEnumerable<TestEntity>"" anotherattribute=""leave this alone"">Inner text</iframe>"; }
			}
			[Test]
			public void src_resolves_to_correct_uri_for_type()
			{
				TemplateResult.HasElement("iframe").WithAttribute("src").ShouldHaveValue("http://localhost/TestEntities");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("iframe").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("iframe").Value.ShouldEqual("Inner text");
			}
		}

		[TestFixture]
		public class When_rendering_img_tag_with_a_templated_src_using_entity_reference : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override string TemplateSource
			{
				get { return @"<viewdata resource=""TestEntity""/> <img to=""resource"" anotherattribute=""leave this alone"">Inner text</img>"; }
			}
			[Test]
			public void src_is_empty()
			{
				TemplateResult.HasElement("img").WithoutAttribute("src");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("img").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("img").Value.ShouldEqual("Inner text");
			}
		}

		[TestFixture]
		public class When_rendering_img_tag_with_a_templated_src_using_entity_type : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override string TemplateSource
			{
				get { return @"<img totype=""IEnumerable<TestEntity>"" anotherattribute=""leave this alone"">Inner text</img>"; }
			}
			[Test]
			public void src_resolves_to_correct_uri_for_type()
			{
				TemplateResult.HasElement("img").WithAttribute("src").ShouldHaveValue("http://localhost/TestEntities");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("img").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("img").Value.ShouldEqual("Inner text");
			}
		}

		[TestFixture]
		public class When_rendering_form_tag_with_a_templated_action_using_entity_reference : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override string TemplateSource
			{
				get { return @"<viewdata resource=""TestEntity""/> <form to=""resource"" anotherattribute=""leave this alone"">Inner text</form>"; }
			}
			[Test]
			public void action_is_empty()
			{
				TemplateResult.HasElement("form").WithoutAttribute("action");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("form").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("form").Value.ShouldEqual("Inner text");
			}
		}

		[TestFixture]
		public class When_rendering_form_tag_with_a_templated_action_using_entity_type : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override string TemplateSource
			{
				get { return @"<form totype=""IEnumerable<TestEntity>"" anotherattribute=""leave this alone"">Inner text</form>"; }
			}
			[Test]
			public void action_resolves_to_correct_uri_for_type()
			{
				TemplateResult.HasElement("form").WithAttribute("action").ShouldHaveValue("http://localhost/TestEntities");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("form").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("form").Value.ShouldEqual("Inner text");
			}
		}

	}
	namespace Given_that_testentity_is_initialised
	{

		[TestFixture]
		public class When_rendering_a_tag_with_a_templated_href_using_entity_reference : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithTestEntity();
			}

			public override string TemplateSource
			{
				get { return @"<viewdata resource=""TestEntity""/> <a to=""resource"" anotherattribute=""leave this alone"">Inner text</a>"; }
			}
			[Test]
			public void href_is_set_to_correct_Url()
			{
				TemplateResult.HasElement("a").WithAttribute("href").ShouldHaveValue("http://localhost/TestEntity/THENAME");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("a").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("a").Value.ShouldEqual("Inner text");
			}
		}



		[TestFixture]
		public class When_rendering_iframe_tag_with_a_templated_src_using_entity_reference : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithTestEntity();
			}

			public override string TemplateSource
			{
				get { return @"<viewdata resource=""TestEntity""/> <iframe to=""resource"" anotherattribute=""leave this alone"">Inner text</iframe>"; }
			}
			[Test]
			public void src_is_set_to_correct_Url()
			{
				TemplateResult.HasElement("iframe").WithAttribute("src").ShouldHaveValue("http://localhost/TestEntity/THENAME");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("iframe").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("iframe").Value.ShouldEqual("Inner text");
			}
		}



		[TestFixture]
		public class When_rendering_img_tag_with_a_templated_src_using_entity_reference : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithTestEntity();
			}

			public override string TemplateSource
			{
				get { return @"<viewdata resource=""TestEntity""/> <img to=""resource"" anotherattribute=""leave this alone"">Inner text</img>"; }
			}
			[Test]
			public void src_is_set_to_correct_Url()
			{
				TemplateResult.HasElement("img").WithAttribute("src").ShouldHaveValue("http://localhost/TestEntity/THENAME");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("img").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("img").Value.ShouldEqual("Inner text");
			}
		}



		[TestFixture]
		public class When_rendering_form_tag_with_a_templated_action_using_entity_reference : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithTestEntity();
			}

			public override string TemplateSource
			{
				get { return @"<viewdata resource=""TestEntity""/> <form to=""resource"" anotherattribute=""leave this alone"">Inner text</form>"; }
			}
			[Test]
			public void action_is_set_to_correct_Url()
			{
				TemplateResult.HasElement("form").WithAttribute("action").ShouldHaveValue("http://localhost/TestEntity/THENAME");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("form").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("form").Value.ShouldEqual("Inner text");
			}
		}

	}

}
