﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 1.1.4322.573.
// 
namespace WindowsClientCustomDataClass.localhost {
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="EmployeesServiceCustomDataClassSoap", Namespace="http://tempuri.org/")]
    public class EmployeesServiceCustomDataClass : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public EmployeesServiceCustomDataClass() {
            this.Url = "http://localhost/Chapter25/EmployeesServiceCustomDataClass.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetEmployees", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public EmployeeDetails[] GetEmployees() {
            object[] results = this.Invoke("GetEmployees", new object[0]);
            return ((EmployeeDetails[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetEmployees(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetEmployees", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public EmployeeDetails[] EndGetEmployees(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((EmployeeDetails[])(results[0]));
        }
    }
    

}