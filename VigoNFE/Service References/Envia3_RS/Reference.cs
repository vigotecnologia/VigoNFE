﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VigoNFE.Envia3_RS {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao", ConfigurationName="Envia3_RS.NfeAutorizacaoSoap12")]
    public interface NfeAutorizacaoSoap12 {
        
        // CODEGEN: Generating message contract since the operation nfeAutorizacaoLote is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao/nfeAutorizacaoLote", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        VigoNFE.Envia3_RS.nfeAutorizacaoLoteResponse nfeAutorizacaoLote(VigoNFE.Envia3_RS.nfeAutorizacaoLoteRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao/nfeAutorizacaoLote", ReplyAction="*")]
        System.Threading.Tasks.Task<VigoNFE.Envia3_RS.nfeAutorizacaoLoteResponse> nfeAutorizacaoLoteAsync(VigoNFE.Envia3_RS.nfeAutorizacaoLoteRequest request);
        
        // CODEGEN: Generating message contract since the operation nfeAutorizacaoLoteZip is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao/nfeAutorizacaoLoteZip", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipResponse nfeAutorizacaoLoteZip(VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao/nfeAutorizacaoLoteZip", ReplyAction="*")]
        System.Threading.Tasks.Task<VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipResponse> nfeAutorizacaoLoteZipAsync(VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")]
    public partial class nfeCabecMsg : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string cUFField;
        
        private string versaoDadosField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string cUF {
            get {
                return this.cUFField;
            }
            set {
                this.cUFField = value;
                this.RaisePropertyChanged("cUF");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string versaoDados {
            get {
                return this.versaoDadosField;
            }
            set {
                this.versaoDadosField = value;
                this.RaisePropertyChanged("versaoDados");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeAutorizacaoLoteRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")]
        public VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao", Order=0)]
        public System.Xml.XmlNode nfeDadosMsg;
        
        public nfeAutorizacaoLoteRequest() {
        }
        
        public nfeAutorizacaoLoteRequest(VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg) {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeAutorizacaoLoteResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")]
        public VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao", Order=0)]
        public System.Xml.XmlNode nfeAutorizacaoLoteResult;
        
        public nfeAutorizacaoLoteResponse() {
        }
        
        public nfeAutorizacaoLoteResponse(VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeAutorizacaoLoteResult) {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeAutorizacaoLoteResult = nfeAutorizacaoLoteResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeAutorizacaoLoteZipRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")]
        public VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao", Order=0)]
        public string nfeDadosMsgZip;
        
        public nfeAutorizacaoLoteZipRequest() {
        }
        
        public nfeAutorizacaoLoteZipRequest(VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg, string nfeDadosMsgZip) {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeDadosMsgZip = nfeDadosMsgZip;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeAutorizacaoLoteZipResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao")]
        public VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeAutorizacao", Order=0)]
        public System.Xml.XmlNode nfeAutorizacaoLoteZipResult;
        
        public nfeAutorizacaoLoteZipResponse() {
        }
        
        public nfeAutorizacaoLoteZipResponse(VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeAutorizacaoLoteZipResult) {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeAutorizacaoLoteZipResult = nfeAutorizacaoLoteZipResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface NfeAutorizacaoSoap12Channel : VigoNFE.Envia3_RS.NfeAutorizacaoSoap12, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NfeAutorizacaoSoap12Client : System.ServiceModel.ClientBase<VigoNFE.Envia3_RS.NfeAutorizacaoSoap12>, VigoNFE.Envia3_RS.NfeAutorizacaoSoap12 {
        
        public NfeAutorizacaoSoap12Client() {
        }
        
        public NfeAutorizacaoSoap12Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NfeAutorizacaoSoap12Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NfeAutorizacaoSoap12Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NfeAutorizacaoSoap12Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        VigoNFE.Envia3_RS.nfeAutorizacaoLoteResponse VigoNFE.Envia3_RS.NfeAutorizacaoSoap12.nfeAutorizacaoLote(VigoNFE.Envia3_RS.nfeAutorizacaoLoteRequest request) {
            return base.Channel.nfeAutorizacaoLote(request);
        }
        
        public System.Xml.XmlNode nfeAutorizacaoLote(ref VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg) {
            VigoNFE.Envia3_RS.nfeAutorizacaoLoteRequest inValue = new VigoNFE.Envia3_RS.nfeAutorizacaoLoteRequest();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsg = nfeDadosMsg;
            VigoNFE.Envia3_RS.nfeAutorizacaoLoteResponse retVal = ((VigoNFE.Envia3_RS.NfeAutorizacaoSoap12)(this)).nfeAutorizacaoLote(inValue);
            nfeCabecMsg = retVal.nfeCabecMsg;
            return retVal.nfeAutorizacaoLoteResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<VigoNFE.Envia3_RS.nfeAutorizacaoLoteResponse> VigoNFE.Envia3_RS.NfeAutorizacaoSoap12.nfeAutorizacaoLoteAsync(VigoNFE.Envia3_RS.nfeAutorizacaoLoteRequest request) {
            return base.Channel.nfeAutorizacaoLoteAsync(request);
        }
        
        public System.Threading.Tasks.Task<VigoNFE.Envia3_RS.nfeAutorizacaoLoteResponse> nfeAutorizacaoLoteAsync(VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg) {
            VigoNFE.Envia3_RS.nfeAutorizacaoLoteRequest inValue = new VigoNFE.Envia3_RS.nfeAutorizacaoLoteRequest();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsg = nfeDadosMsg;
            return ((VigoNFE.Envia3_RS.NfeAutorizacaoSoap12)(this)).nfeAutorizacaoLoteAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipResponse VigoNFE.Envia3_RS.NfeAutorizacaoSoap12.nfeAutorizacaoLoteZip(VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipRequest request) {
            return base.Channel.nfeAutorizacaoLoteZip(request);
        }
        
        public System.Xml.XmlNode nfeAutorizacaoLoteZip(ref VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg, string nfeDadosMsgZip) {
            VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipRequest inValue = new VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipRequest();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsgZip = nfeDadosMsgZip;
            VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipResponse retVal = ((VigoNFE.Envia3_RS.NfeAutorizacaoSoap12)(this)).nfeAutorizacaoLoteZip(inValue);
            nfeCabecMsg = retVal.nfeCabecMsg;
            return retVal.nfeAutorizacaoLoteZipResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipResponse> VigoNFE.Envia3_RS.NfeAutorizacaoSoap12.nfeAutorizacaoLoteZipAsync(VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipRequest request) {
            return base.Channel.nfeAutorizacaoLoteZipAsync(request);
        }
        
        public System.Threading.Tasks.Task<VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipResponse> nfeAutorizacaoLoteZipAsync(VigoNFE.Envia3_RS.nfeCabecMsg nfeCabecMsg, string nfeDadosMsgZip) {
            VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipRequest inValue = new VigoNFE.Envia3_RS.nfeAutorizacaoLoteZipRequest();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsgZip = nfeDadosMsgZip;
            return ((VigoNFE.Envia3_RS.NfeAutorizacaoSoap12)(this)).nfeAutorizacaoLoteZipAsync(inValue);
        }
    }
}
