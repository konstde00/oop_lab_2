<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/list">
		<html>
			<head>
				<style>
					table {
					font-family: arial, sans-serif;
					border-collapse: collapse;
					width:
					100%;
					}
					td, th {
					border: 1px solid #dddddd;
					text-align: left;
					padding: 8px;
					}
					tr:nth-child(even) {
					background-color: #dddddd;
					}
				</style>
			</head>
			<body>
				<h2>Services</h2>
				<table>
					<tr>
						<th>Title</th>
						<th>Description</th>
						<th>Type</th>
						<th>Version</th>
						<th>Author-Name</th>
						<th>Author-Surname</th>
						<th>Author-Age</th>
						<th>Rules</th>
						<th>Info</th>
					</tr>
					<xsl:for-each select="service">
						<tr>
							<td>
								<xsl:value-of select="title" />
							</td>
							<td>
								<xsl:value-of select="description" />
							</td>
							<td>
								<xsl:value-of select="type" />
							</td>
							<td>
								<xsl:value-of select="version" />
							</td>
							<xsl:for-each select="author" >
								<td>
									<xsl:value-of select="name" />
								</td>
								<td>
									<xsl:value-of select="surname" />
								</td>
								<td>
									<xsl:value-of select="age" />
								</td>
							</xsl:for-each>
							<td>
								<xsl:value-of select="rules" />
							</td>
							<td>
								<xsl:value-of select="info" />
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>