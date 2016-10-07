<%@ Page CodeBehind="Repeater1.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Chapter10.RepeaterTest" %>
<%@ Import Namespace="System.Data.Common" %>
<HTML>
	<body>
		<form method="post" runat="server">
			<h2>Employees</h2>
			<asp:Repeater runat="server" ID="Repeater1">
				<ItemTemplate>
					<li>
						<%# ((DbDataRecord)Container.DataItem)["TitleOfCourtesy"] %>
						<b>
							<%# ((DbDataRecord)Container.DataItem)["LastName"] %>
						</b>,
						<%# ((DbDataRecord)Container.DataItem)["FirstName"] %>
					</li>
				</ItemTemplate>
			</asp:Repeater>
		</form>
	</body>
</HTML>
