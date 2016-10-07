using System.Web.Services;
public class Calculator : System.Web.Services.WebService
{
	[WebMethod]public int Add(int X, int Y) 
		{
			return (X + Y);
		}
}
