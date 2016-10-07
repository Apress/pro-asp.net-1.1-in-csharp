<%@ Page Language="C#" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
  <title>SimpleControls</title>
</head>

  <body>
    <form id="Form1" method="post" runat="server">
      <h3>Web Controls</h3>
      <asp:Label ID="lblArea" Runat="server" /><br>
      <asp:Button ID="cmdButton" Runat="server" /><br>
      <br><br>
      <h3>HTML Server-Side Controls</h3>
      <span id="spnArea" runat="server"/><br>
      <button id="btnCmd" runat="server" type="button"/>
    </form>
  </body>
</html>

<script runat="server">
  void Page_Load()
  {
        lblArea.Text = "this is an asp:label";
        cmdButton.Text = "this is an asp:button";
        spnArea.InnerText = "this is an html span";
        btnCmd.InnerText = "this is an html button";
  }
</script>
