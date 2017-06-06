﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VigoNFE.Evento3_SP {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento", ConfigurationName="Evento3_SP.RecepcaoEventoSoap12")]
    public interface RecepcaoEventoSoap12 {
        
        // CODEGEN: Generating message contract since the operation nfeRecepcaoEvento is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento/nfeRecepcaoEvento", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        VigoNFE.Evento3_SP.nfeRecepcaoEventoResponse nfeRecepcaoEvento(VigoNFE.Evento3_SP.nfeRecepcaoEventoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento/nfeRecepcaoEvento", ReplyAction="*")]
        System.Threading.Tasks.Task<VigoNFE.Evento3_SP.nfeRecepcaoEventoResponse> nfeRecepcaoEventoAsync(VigoNFE.Evento3_SP.nfeRecepcaoEventoRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento")]
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
    public partial class nfeRecepcaoEventoRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento")]
        public VigoNFE.Evento3_SP.nfeCabecMsg nfeCabecMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento", Order=0)]
        public System.Xml.XmlNode nfeDadosMsg;
        
        public nfeRecepcaoEventoRequest() {
        }
        
        public nfeRecepcaoEventoRequest(VigoNFE.Evento3_SP.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg) {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeRecepcaoEventoResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento")]
        public VigoNFE.Evento3_SP.nfeCabecMsg nfeCabecMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/RecepcaoEvento", Order=0)]
        public System.Xml.XmlNode nfeRecepcaoEventoResult;
        
        public nfeRecepcaoEventoResponse() {
        }
        
        public nfeRecepcaoEventoResponse(VigoNFE.Evento3_SP.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeRecepcaoEventoResult) {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeRecepcaoEventoResult = nfeRecepcaoEventoResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface RecepcaoEventoSoap12Channel : VigoNFE.Evento3_SP.RecepcaoEventoSoap12, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RecepcaoEventoSoap12Client : System.ServiceModel.ClientBase<VigoNFE.Evento3_SP.RecepcaoEventoSoap12>, VigoNFE.Evento3_SP.RecepcaoEventoSoap12 {
        
        public RecepcaoEventoSoap12Client() {
        }
        
        public RecepcaoEventoSoap12Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RecepcaoEventoSoap12Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RecepcaoEventoSoap12Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RecepcaoEventoSoap12Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        VigoNFE.Evento3_SP.nfeRecepcaoEventoResponse VigoNFE.Evento3_SP.RecepcaoEventoSoap12.nfeRecepcaoEvento(VigoNFE.Evento3_SP.nfeRecepcaoEventoRequest request) {
            return base.Channel.nfeRecepcaoEvento(request);
        }
        
        public System.Xml.XmlNode nfeRecepcaoEvento(ref VigoNFE.Evento3_SP.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg) {
            VigoNFE.Evento3_SP.nfeRecepcaoEventoRequest inValue = new VigoNFE.Evento3_SP.nfeRecepcaoEventoRequest();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsg = nfeDadosMsg;
            VigoNFE.Evento3_SP.nfeRecepcaoEventoResponse retVal = ((VigoNFE.Evento3_SP.RecepcaoEventoSoap12)(this)).nfeRecepcaoEvento(inValue);
            nfeCabecMsg = retVal.nfeCabecMsg;
            return retVal.nfeRecepcaoEventoResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<VigoNFE.Evento3_SP.nfeRecepcaoEventoResponse> VigoNFE.Evento3_SP.RecepcaoEventoSoap12.nfeRecepcaoEventoAsync(VigoNFE.Evento3_SP.nfeRecepcaoEventoRequest request) {
            return base.Channel.nfeRecepcaoEventoAsync(request);
        }
        
        public System.Threading.Tasks.Task<VigoNFE.Evento3_SP.nfeRecepcaoEventoResponse> nfeRecepcaoEventoAsync(VigoNFE.Evento3_SP.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg) {
            VigoNFE.Evento3_SP.nfeRecepcaoEventoRequest inValue = new VigoNFE.Evento3_SP.nfeRecepcaoEventoRequest();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsg = nfeDadosMsg;
            return ((VigoNFE.Evento3_SP.RecepcaoEventoSoap12)(this)).nfeRecepcaoEventoAsync(inValue);
        }
    }
}
