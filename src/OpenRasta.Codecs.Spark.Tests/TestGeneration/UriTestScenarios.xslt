<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" 
				xmlns:xs="http://www.w3.org/2001/XMLSchema"
				exclude-result-prefixes="msxsl"
>
    <xsl:output method="text" indent="no"/>
	<xsl:template match="//xs:schema">
namespace Given_that_testentity_is_null
{
		<xsl:apply-templates select="//xs:element[@name='a']" mode="nullentity"/>
		<xsl:apply-templates select="//xs:element[@name='iframe']" mode="nullentity"/>
		<xsl:apply-templates select="//xs:element[@name='img']" mode="nullentity"/>
		<xsl:apply-templates select="//xs:element[@name='form']" mode="nullentity"/>
}
namespace Given_that_testentity_is_initialised
{
		<xsl:apply-templates select="//xs:element[@name='a']" mode="filledentity"/>
		<xsl:apply-templates select="//xs:element[@name='iframe']" mode="filledentity"/>
		<xsl:apply-templates select="//xs:element[@name='img']" mode="filledentity"/>
		<xsl:apply-templates select="//xs:element[@name='form']" mode="filledentity"/>
}
	</xsl:template>
    <xsl:template match="//xs:element[descendant::xs:attribute[@type='URI']]" priority="1" mode="nullentity">
		<xsl:variable name="theAttribute" select="descendant::xs:attribute[@type='URI']"/>
		<xs:whiteSpace value="preserve"  />
		[TestFixture]
		public	class When_rendering_<xsl:value-of select="@name"/>_tag_with_a_templated_<xsl:value-of select="$theAttribute/@name"/>_using_entity_reference : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override string TemplateSource
			{
				get { return @"&lt;viewdata resource=""TestEntity""/> &lt;<xsl:value-of select="@name"/>&#160;to=""resource"" anotherattribute=""leave this alone"">Inner text&lt;/<xsl:value-of select="@name"/>>"; }
			}
			[Test]
			public void <xsl:value-of select="$theAttribute/@name"/>_is_empty()
			{
				TemplateResult.HasElement("<xsl:value-of select="@name"/>").WithAttribute("<xsl:value-of select="$theAttribute/@name"/>").ShouldHaveValue("");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("<xsl:value-of select="@name"/>").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("<xsl:value-of select="@name"/>").Value.ShouldEqual("Inner text");
			}
		}
		
		[TestFixture]
		public	class When_rendering_<xsl:value-of select="@name"/>_tag_with_a_templated_<xsl:value-of select="$theAttribute/@name"/>_using_entity_type : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override string TemplateSource
			{
				get { return @"&lt;<xsl:value-of select="@name"/>&#160;totype=""IEnumerable&lt;TestEntity>"" anotherattribute=""leave this alone"">Inner text&lt;/<xsl:value-of select="@name"/>>"; }
			}
			[Test]
			public void <xsl:value-of select="$theAttribute/@name"/>_resolves_to_correct_uri_for_type()
			{
				TemplateResult.HasElement("<xsl:value-of select="@name"/>").WithAttribute("<xsl:value-of select="$theAttribute/@name"/>").ShouldHaveValue("http://localhost/TestEntities");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("<xsl:value-of select="@name"/>").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("<xsl:value-of select="@name"/>").Value.ShouldEqual("Inner text");
			}
		}
    </xsl:template>
    <xsl:template match="//xs:element[descendant::xs:attribute[@type='URI']]" priority="1" mode="filledentity">
		<xsl:variable name="theAttribute" select="descendant::xs:attribute[@type='URI']"/>
		<xs:whiteSpace value="preserve"  />
		[TestFixture]
		public	class When_rendering_<xsl:value-of select="@name"/>_tag_with_a_templated_<xsl:value-of select="$theAttribute/@name"/>_using_entity_reference : With_scenario_of.Using_spark_codec_to_render_an_element_with_a_uri_attributes
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithTestEntity();
			}
		
			public override string TemplateSource
			{
				get { return @"&lt;viewdata resource=""TestEntity""/> &lt;<xsl:value-of select="@name"/>&#160;to=""resource"" anotherattribute=""leave this alone"">Inner text&lt;/<xsl:value-of select="@name"/>>"; }
			}
			[Test]
			public void <xsl:value-of select="$theAttribute/@name"/>_is_set_to_correct_Url()
			{
				TemplateResult.HasElement("<xsl:value-of select="@name"/>").WithAttribute("<xsl:value-of select="$theAttribute/@name"/>").ShouldHaveValue("http://localhost/THENAME");
			}
			[Test]
			public void Non_relevant_attributes_are_ignored()
			{
				TemplateResult.HasElement("<xsl:value-of select="@name"/>").WithAttribute("anotherattribute").ShouldHaveValue("leave this alone");
			}
			[Test]
			public void Inner_text_is_maintained()
			{
				TemplateResult.HasElement("<xsl:value-of select="@name"/>").Value.ShouldEqual("Inner text");
			}
		}
		
	
    </xsl:template>
	<xsl:template match="//xs:element" priority="-1" mode="nullentity"></xsl:template>
	<xsl:template match="//xs:element" priority="-1" mode="filledentity"></xsl:template>
</xsl:stylesheet>
