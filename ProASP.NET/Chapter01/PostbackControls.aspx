<%@ Page language="C#" %>

<html>
  <head>
    <title>TextBox</title>
  </head>
  <body>
    <form id="TextBox" method="post" runat="server">
      <asp:TextBox id="txtSample" runat="server" />
      <asp:Button id="Button1" runat="server" Text="Button" />
    </form>
  </body>
</html>
<script runat="server">
  void Page_Load()
  {
      if (!Page.IsPostBack)
      {
          Response.Write("This is the first time the page loaded.");
      }
      else 
      {
        Response.Write("You typed [" + txtSample.Text + "] into txtSample.");
      }
  }
</script>
