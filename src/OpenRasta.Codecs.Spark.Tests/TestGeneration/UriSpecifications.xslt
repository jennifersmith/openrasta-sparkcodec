<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" 
				xmlns:xs="http://www.w3.org/2001/XMLSchema"
				exclude-result-prefixes="msxsl"
>
    <xsl:output method="text" indent="no"/>
	<xsl:template match="//xs:schema">
		// all attribute/elements needing uri replacement
	<xsl:apply-templates select="//xs:attribute" />

	</xsl:template>
    <xsl:template match="//xs:attribute[@type='URI']" priority="1" >
		<xsl:variable name="elementName" select="ancestor::xs:element/@name"/>
		new ReplacementSpecification("<xsl:value-of select="$elementName"/>", "<xsl:value-of select="@name"/>"),
    </xsl:template>
	<xsl:template match="//xs:attribute" priority="-1"></xsl:template>
</xsl:stylesheet>
