﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VigoNFE.Status3_GO {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2", ConfigurationName="Status3_GO.NfeStatusServicoService")]
    public interface NfeStatusServicoService {
        
        // CODEGEN: Generating message contract since the operation nfeStatusServicoNF2 is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2/nfeStatusServicoNF2", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        VigoNFE.Status3_GO.nfeStatusServicoNF2Response nfeStatusServicoNF2(VigoNFE.Status3_GO.nfeStatusServicoNF2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2/nfeStatusServicoNF2", ReplyAction="*")]
        System.Threading.Tasks.Task<VigoNFE.Status3_GO.nfeStatusServicoNF2Response> nfeStatusServicoNF2Async(VigoNFE.Status3_GO.nfeStatusServicoNF2Request request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2")]
    public partial class nfeCabecMsg : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string cUFField;
        
        private string versaoDadosField;
        
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
    public partial class nfeStatusServicoNF2Request {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2")]
        public VigoNFE.Status3_GO.nfeCabecMsg nfeCabecMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2", Order=0)]
        public System.Xml.XmlNode nfeDadosMsg;
        
        public nfeStatusServicoNF2Request() {
        }
        
        public nfeStatusServicoNF2Request(VigoNFE.Status3_GO.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg) {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeStatusServicoNF2Response {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2", Order=0)]
        public System.Xml.XmlNode nfeStatusServicoNF2Result;
        
        public nfeStatusServicoNF2Response() {
        }
        
        public nfeStatusServicoNF2Response(System.Xml.XmlNode nfeStatusServicoNF2Result) {
            this.nfeStatusServicoNF2Result = nfeStatusServicoNF2Result;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface NfeStatusServicoServiceChannel : VigoNFE.Status3_GO.NfeStatusServicoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NfeStatusServicoServiceClient : System.ServiceModel.ClientBase<VigoNFE.Status3_GO.NfeStatusServicoService>, VigoNFE.Status3_GO.NfeStatusServicoService {
        
        public NfeStatusServicoServiceClient() {
        }
        
        public NfeStatusServicoServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NfeStatusServicoServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NfeStatusServicoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NfeStatusServicoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        VigoNFE.Status3_GO.nfeStatusServicoNF2Response VigoNFE.Status3_GO.NfeStatusServicoService.nfeStatusServicoNF2(VigoNFE.Status3_GO.nfeStatusServicoNF2Request request) {
            return base.Channel.nfeStatusServicoNF2(request);
        }
        
        public System.Xml.XmlNode nfeStatusServicoNF2(VigoNFE.Status3_GO.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg) {
            VigoNFE.Status3_GO.nfeStatusServicoNF2Request inValue = new VigoNFE.Status3_GO.nfeStatusServicoNF2Request();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsg = nfeDadosMsg;
            VigoNFE.Status3_GO.nfeStatusServicoNF2Response retVal = ((VigoNFE.Status3_GO.NfeStatusServicoService)(this)).nfeStatusServicoNF2(inValue);
            return retVal.nfeStatusServicoNF2Result;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<VigoNFE.Status3_GO.nfeStatusServicoNF2Response> VigoNFE.Status3_GO.NfeStatusServicoService.nfeStatusServicoNF2Async(VigoNFE.Status3_GO.nfeStatusServicoNF2Request request) {
            return base.Channel.nfeStatusServicoNF2Async(request);
        }
        
        public System.Threading.Tasks.Task<VigoNFE.Status3_GO.nfeStatusServicoNF2Response> nfeStatusServicoNF2Async(VigoNFE.Status3_GO.nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg) {
            VigoNFE.Status3_GO.nfeStatusServicoNF2Request inValue = new VigoNFE.Status3_GO.nfeStatusServicoNF2Request();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsg = nfeDadosMsg;
            return ((VigoNFE.Status3_GO.NfeStatusServicoService)(this)).nfeStatusServicoNF2Async(inValue);
        }
    }
}
