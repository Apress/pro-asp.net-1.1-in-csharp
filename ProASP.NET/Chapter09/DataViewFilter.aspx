<%@ Page language="c#" Codebehind="DataViewFilter.aspx.cs" AutoEventWireup="false" Inherits="Chapter09.DataViewFilter" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<body>
		<form method="post" runat="server" ID="Form1">
			<b><u>Filter = "ProductName = 'Chocolade' "</u></b><br>
			<br>
			<asp:DataGrid runat="server" ID="Datagrid1" HeaderStyle-Font-Bold="true" />
			<br>
			<STRONG><U>Filter = "UnitsInStock = 0 AND UnitsOnOrder = 0" </U></STRONG>
			<br>
			<br>
			<asp:DataGrid runat="server" ID="Datagrid2" HeaderStyle-Font-Bold="true" />
			<br>
			<STRONG><U>Filter = <STRONG><U>"ProductName LIKE 'P%'"</U></STRONG></U></STRONG><br>
			<br>
			<asp:DataGrid runat="server" ID="Datagrid3" HeaderStyle-Font-Bold="true" />
		</form>
	</body>
</HTML>
