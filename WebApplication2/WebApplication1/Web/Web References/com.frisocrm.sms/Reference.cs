﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.225
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.225 版自动生成。
// 
#pragma warning disable 1591

namespace Maticsoft.Web.com.frisocrm.sms {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SendSMSService2Soap", Namespace="http://tempuri.org/")]
    public partial class SendSMSService2 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SendSMSSingleOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SendSMSService2() {
            this.Url = global::Maticsoft.Web.Properties.Settings.Default.Maticsoft_Web_com_frisocrm_sms_SendSMSService2;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SendSMSSingleCompletedEventHandler SendSMSSingleCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendSMSSingle", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ReturnStatus SendSMSSingle(int uid, string password, int templateId, string mobile, string contens) {
            object[] results = this.Invoke("SendSMSSingle", new object[] {
                        uid,
                        password,
                        templateId,
                        mobile,
                        contens});
            return ((ReturnStatus)(results[0]));
        }
        
        /// <remarks/>
        public void SendSMSSingleAsync(int uid, string password, int templateId, string mobile, string contens) {
            this.SendSMSSingleAsync(uid, password, templateId, mobile, contens, null);
        }
        
        /// <remarks/>
        public void SendSMSSingleAsync(int uid, string password, int templateId, string mobile, string contens, object userState) {
            if ((this.SendSMSSingleOperationCompleted == null)) {
                this.SendSMSSingleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendSMSSingleOperationCompleted);
            }
            this.InvokeAsync("SendSMSSingle", new object[] {
                        uid,
                        password,
                        templateId,
                        mobile,
                        contens}, this.SendSMSSingleOperationCompleted, userState);
        }
        
        private void OnSendSMSSingleOperationCompleted(object arg) {
            if ((this.SendSMSSingleCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendSMSSingleCompleted(this, new SendSMSSingleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ReturnStatus {
        
        private string statusField;
        
        private string statusMsgField;
        
        private long idField;
        
        /// <remarks/>
        public string Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public string StatusMsg {
            get {
                return this.statusMsgField;
            }
            set {
                this.statusMsgField = value;
            }
        }
        
        /// <remarks/>
        public long Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void SendSMSSingleCompletedEventHandler(object sender, SendSMSSingleCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendSMSSingleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendSMSSingleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ReturnStatus Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ReturnStatus)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591