<SCRIPT LANGUAGE="VBScript" RUNAT="Server">
Option Explicit

Dim SoapClient
Set SoapClient = CreateObject("MSSOAP.SoapClient")

' Generate a proxy.
Dim WSDLPath
'WSDLPath = "http://localhost/Chapter24/EmployeesService.asmx?WSDL"
WSDLPath = Server.MapPath("WSDL.xml")
SoapClient.MSSoapInit WSDLPath

' Read the number of employees.
Dim numEmp
numEmp = SoapClient.GetEmployeesCount()
Response.Write(numEmp & " employee(s) in London")

</SCRIPT>
