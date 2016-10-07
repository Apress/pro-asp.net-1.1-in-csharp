<%@ Page CodeBehind="DataReader.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Chapter08.Command1" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<HTML>
	<body>
		<form method="post" runat="server">
			<h2>Employees</h2>
			<asp:Literal runat="server" ID="HtmlContent" />
		</form>
	</body>
</HTML>
