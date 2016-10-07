<%@ Page Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
  <title>SimpleControls</title>
</head>

  <body>
    <form id="Form1" method="post" runat="server">
      <asp:DataGrid id="MyDataGrid" runat="server"/>
    </form>
  </body>
</html>


<script runat="server">
  private string connectionString = "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=SSPI";
  void Page_Load()
  {
      SqlConnection con = new SqlConnection(connectionString);
      SqlCommand cmd = new SqlCommand("SELECT CompanyName, ContactName, ContactTitle, City FROM Customers", con);
      con.Open();
      SqlDataReader reader = cmd.ExecuteReader();
      MyDataGrid.DataSource = reader;
      MyDataGrid.DataBind();
      con.Close();
        
  }
</script>
