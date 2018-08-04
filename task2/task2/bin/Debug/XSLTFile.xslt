<?xml version='1.0'?>
<xsl:stylesheet version="1.0"
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>Viewing twice</title>
      </head>
      <body>
        <p>
          <xsl:value-of select="."/>
        </p>
        <p>
          <xsl:value-of select="."/>
        </p>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>