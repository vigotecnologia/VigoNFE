using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace VigoNFE
{
    public class NFE
    {
        // Classes para o webservice client (compartilhado por todos)

        X509Certificate2 cert = new X509Certificate2();
        Certificado certificado = new Certificado();
        HttpsTransportBindingElement objHttpsTransportBindingElement = new HttpsTransportBindingElement();
        TextMessageEncodingBindingElement objTextMessageEncodingBindingElement = new TextMessageEncodingBindingElement();
        CustomBinding objCustomBinding;
        AssinaturaDigital AS = new AssinaturaDigital();

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Endereços dos webservices
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // São Paulo

        const string PROD_STATUS_SP = "https://nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx";
        const string PROD_PROTOCOLO_SP = "https://nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx";
        const string PROD_CANCELA_SP = "https://nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx";
        const string PROD_ENVIA_SP = "https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx";
        const string PROD_RETORNO_SP = "https://nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx";
        const string HOMOLOG_STATUS_SP = "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico2.asmx";
        const string HOMOLOG_PROTOCOLO_SP = "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeconsulta2.asmx";
        const string HOMOLOG_CANCELA_SP = "https://homologacao.nfe.fazenda.sp.gov.br/ws/recepcaoevento.asmx";
        const string HOMOLOG_ENVIA_SP = "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao.asmx";
        const string HOMOLOG_RETORNO_SP = "https://homologacao.nfe.fazenda.sp.gov.br/ws/nferetautorizacao.asmx";

        // Rio Grande do Sul

        const string PROD_STATUS_RS = "https://nfe.sefaz.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx";
        const string PROD_PROTOCOLO_RS = "https://nfe.sefaz.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx";
        const string PROD_CANCELA_RS = "https://nfe.sefaz.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
        const string PROD_ENVIA_RS = "https://nfe.sefaz.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx";
        const string PROD_RETORNO_RS = "https://nfe.sefaz.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx";
        const string HOMOLOG_STATUS_RS = "https://homologacao.nfe.sefaz.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx";
        const string HOMOLOG_PROTOCOLO_RS = "https://homologacao.nfe.sefaz.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx";
        const string HOMOLOG_CANCELA_RS = "https://homologacao.nfe.sefaz.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
        const string HOMOLOG_ENVIA_RS = "https://homologacao.nfe.sefaz.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx";
        const string HOMOLOG_RETORNO_RS = "https://homologacao.nfe.sefaz.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx";

        // Sefaz Virtual do Ambiente Nacional - SVAN

        const string PROD_STATUS_SVAN = "https://www.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx";
        const string PROD_PROTOCOLO_SVAN = "https://www.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx";
        const string PROD_CANCELA_SVAN = "https://www.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx";
        const string PROD_ENVIA_SVAN = "https://www.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx";
        const string PROD_RETORNO_SVAN = "https://www.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx";
        const string HOMOLOG_STATUS_SVAN = "https://hom.sefazvirtual.fazenda.gov.br/NfeStatusServico2/NfeStatusServico2.asmx";
        const string HOMOLOG_PROTOCOLO_SVAN = "https://hom.sefazvirtual.fazenda.gov.br/NfeConsulta2/NfeConsulta2.asmx";
        const string HOMOLOG_CANCELA_SVAN = "https://hom.sefazvirtual.fazenda.gov.br/RecepcaoEvento/RecepcaoEvento.asmx";
        const string HOMOLOG_ENVIA_SVAN = "https://hom.sefazvirtual.fazenda.gov.br/NfeAutorizacao/NfeAutorizacao.asmx";
        const string HOMOLOG_RETORNO_SVAN = "https://hom.sefazvirtual.fazenda.gov.br/NfeRetAutorizacao/NfeRetAutorizacao.asmx";

        // Sefaz Virtual do Rio Grande do Sul - SVRS

        const string PROD_STATUS_SVRS = "https://nfe.sefazvirtual.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx";
        const string PROD_PROTOCOLO_SVRS = "https://nfe.sefazvirtual.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx";
        const string PROD_CANCELA_SVRS = "https://nfe.sefazvirtual.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
        const string PROD_ENVIA_SVRS = "https://nfe.sefazvirtual.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx";
        const string PROD_RETORNO_SVRS = "https://nfe.sefazvirtual.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx";
        const string HOMOLOG_STATUS_SVRS = "https://homologacao.nfe.sefazvirtual.rs.gov.br/ws/NfeStatusServico/NfeStatusServico2.asmx";
        const string HOMOLOG_PROTOCOLO_SVRS = "https://homologacao.nfe.sefazvirtual.rs.gov.br/ws/NfeConsulta/NfeConsulta2.asmx";
        const string HOMOLOG_CANCELA_SVRS = "https://homologacao.nfe.sefazvirtual.rs.gov.br/ws/recepcaoevento/recepcaoevento.asmx";
        const string HOMOLOG_ENVIA_SVRS = "https://homologacao.nfe.sefazvirtual.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao.asmx";
        const string HOMOLOG_RETORNO_SVRS = "https://homologacao.nfe.sefazvirtual.rs.gov.br/ws/NfeRetAutorizacao/NFeRetAutorizacao.asmx";

        // Amazonas

        const string PROD_STATUS_AM = "https://nfe.sefaz.am.gov.br/services2/services/NfeStatusServico2";
        const string PROD_PROTOCOLO_AM = "https://nfe.sefaz.am.gov.br/services2/services/NfeConsulta2";
        const string PROD_CANCELA_AM = "https://nfe.sefaz.am.gov.br/services2/services/RecepcaoEvento";
        const string PROD_ENVIA_AM = "https://nfe.sefaz.am.gov.br/services2/services/NfeAutorizacao";
        const string PROD_RETORNO_AM = "https://nfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao";
        const string HOMOLOG_STATUS_AM = "https://homnfe.sefaz.am.gov.br/services2/services/NfeStatusServico2";
        const string HOMOLOG_PROTOCOLO_AM = "https://homnfe.sefaz.am.gov.br/services2/services/NfeConsulta2";
        const string HOMOLOG_CANCELA_AM = "https://homnfe.sefaz.am.gov.br/services2/services/RecepcaoEvento";
        const string HOMOLOG_ENVIA_AM = "https://homnfe.sefaz.am.gov.br/services2/services/NfeAutorizacao";
        const string HOMOLOG_RETORNO_AM = "https://homnfe.sefaz.am.gov.br/services2/services/NfeRetAutorizacao";

        // Bahia

        const string PROD_STATUS_BA = "https://nfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx";
        const string PROD_PROTOCOLO_BA = "https://nfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx";
        const string PROD_CANCELA_BA = "https://nfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx";
        const string PROD_ENVIA_BA = "https://nfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx";
        const string PROD_RETORNO_BA = "https://nfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx";
        const string HOMOLOG_STATUS_BA = "https://hnfe.sefaz.ba.gov.br/webservices/NfeStatusServico/NfeStatusServico.asmx";
        const string HOMOLOG_PROTOCOLO_BA = "https://hnfe.sefaz.ba.gov.br/webservices/nfenw/nfeconsulta2.asmx";
        const string HOMOLOG_CANCELA_BA = "https://hnfe.sefaz.ba.gov.br/webservices/sre/recepcaoevento.asmx";
        const string HOMOLOG_ENVIA_BA = "https://hnfe.sefaz.ba.gov.br/webservices/NfeAutorizacao/NfeAutorizacao.asmx";
        const string HOMOLOG_RETORNO_BA = "https://hnfe.sefaz.ba.gov.br/webservices/NfeRetAutorizacao/NfeRetAutorizacao.asmx";

        // Goiás

        const string PROD_STATUS_GO = "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl";
        const string PROD_PROTOCOLO_GO = "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl";
        const string PROD_CANCELA_GO = "https://nfe.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl";
        const string PROD_ENVIA_GO = "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl";
        const string PROD_RETORNO_GO = "https://nfe.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl";
        const string HOMOLOG_STATUS_GO = "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeStatusServico2?wsdl";
        const string HOMOLOG_PROTOCOLO_GO = "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeConsulta2?wsdl";
        const string HOMOLOG_CANCELA_GO = "https://homolog.sefaz.go.gov.br/nfe/services/v2/RecepcaoEvento?wsdl";
        const string HOMOLOG_ENVIA_GO = "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeAutorizacao?wsdl";
        const string HOMOLOG_RETORNO_GO = "https://homolog.sefaz.go.gov.br/nfe/services/v2/NfeRetAutorizacao?wsdl";

        // Minas Gerais

        const string PROD_STATUS_MG = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRecepcao2";
        const string PROD_PROTOCOLO_MG = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2";
        const string PROD_CANCELA_MG = "https://nfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento";
        const string PROD_ENVIA_MG = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao";
        const string PROD_RETORNO_MG = "https://nfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao";
        const string HOMOLOG_STATUS_MG = "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeStatusServico2";
        const string HOMOLOG_PROTOCOLO_MG = "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeConsulta2";
        const string HOMOLOG_CANCELA_MG = "https://hnfe.fazenda.mg.gov.br/nfe2/services/RecepcaoEvento";
        const string HOMOLOG_ENVIA_MG = "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeAutorizacao";
        const string HOMOLOG_RETORNO_MG = "https://hnfe.fazenda.mg.gov.br/nfe2/services/NfeRetAutorizacao";

        // Pernambuco

        const string PROD_STATUS_PE = "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeStatusServico2";
        const string PROD_PROTOCOLO_PE = "https://nfe.sefaz.pe.gov.br/nfe-service/services/NfeConsulta2";
        const string PROD_CANCELA_PE = "https://nfe.sefaz.pe.gov.br/nfe-service/services/RecepcaoEvento";
        const string PROD_ENVIA_PE = "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeAutorizacao";
        const string PROD_RETORNO_PE = "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRetAutorizacao";
        const string HOMOLOG_STATUS_PE = "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeStatusServico2";
        const string HOMOLOG_PROTOCOLO_PE = "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeConsulta2";
        const string HOMOLOG_CANCELA_PE = "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/RecepcaoEvento";
        const string HOMOLOG_ENVIA_PE = "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeAutorizacao";
        const string HOMOLOG_RETORNO_PE = "https://nfehomolog.sefaz.pe.gov.br/nfe-service/services/NfeRetAutorizacao";

        // Mato Grosso do Sul

        const string PROD_STATUS_MS = "https://nfe.fazenda.ms.gov.br/producao/services2/NfeStatusServico2";
        const string PROD_PROTOCOLO_MS = "https://nfe.fazenda.ms.gov.br/producao/services2/NfeConsulta2";
        const string PROD_CANCELA_MS = "https://nfe.fazenda.ms.gov.br/producao/services2/RecepcaoEvento";
        const string PROD_ENVIA_MS = "https://nfe.fazenda.ms.gov.br/producao/services2/NfeAutorizacao";
        const string PROD_RETORNO_MS = "https://nfe.fazenda.ms.gov.br/producao/services2/NfeRetAutorizacao";
        const string HOMOLOG_STATUS_MS = "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeStatusServico2";
        const string HOMOLOG_PROTOCOLO_MS = "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeConsulta2";
        const string HOMOLOG_CANCELA_MS = "https://homologacao.nfe.ms.gov.br/homologacao/services2/RecepcaoEvento";
        const string HOMOLOG_ENVIA_MS = "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeAutorizacao";
        const string HOMOLOG_RETORNO_MS = "https://homologacao.nfe.ms.gov.br/homologacao/services2/NfeRetAutorizacao";

        // Paraná

        const string PROD_STATUS_PR = "";
        const string PROD_PROTOCOLO_PR = "";
        const string PROD_CANCELA_PR = "";
        const string PROD_ENVIA_PR = "";
        const string PROD_RETORNO_PR = "";
        const string HOMOLOG_STATUS_PR = "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeStatusServico3?wsdl";
        const string HOMOLOG_PROTOCOLO_PR = "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeConsulta3?wsdl";
        const string HOMOLOG_CANCELA_PR = "https://homologacao.nfe2.fazenda.pr.gov.br/nfe-evento/NFeRecepcaoEvento?wsdl";
        const string HOMOLOG_ENVIA_PR = "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeAutorizacao3?wsdl";
        const string HOMOLOG_RETORNO_PR = "https://homologacao.nfe.fazenda.pr.gov.br/nfe/NFeRetAutorizacao3?wsdl";

        // Mato Grosso

        const string PROD_STATUS_MT = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl";
        const string PROD_PROTOCOLO_MT = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl";
        const string PROD_CANCELA_MT = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl";
        const string PROD_ENVIA_MT = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl";
        const string PROD_RETORNO_MT = "https://nfe.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl";
        const string HOMOLOG_STATUS_MT = "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeStatusServico2?wsdl";
        const string HOMOLOG_PROTOCOLO_MT = "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeConsulta2?wsdl";
        const string HOMOLOG_CANCELA_MT = "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/RecepcaoEvento?wsdl";
        const string HOMOLOG_ENVIA_MT = "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeAutorizacao?wsdl";
        const string HOMOLOG_RETORNO_MT = "https://homologacao.sefaz.mt.gov.br/nfews/v2/services/NfeRetAutorizacao?wsdl";

        // Ceará

        const string PROD_STATUS_CE = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl";
        const string PROD_PROTOCOLO_CE = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl";
        const string PROD_CANCELA_CE = "https://nfe.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl";
        const string PROD_ENVIA_CE = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl";
        const string PROD_RETORNO_CE = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl";
        const string HOMOLOG_STATUS_CE = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl";
        const string HOMOLOG_PROTOCOLO_CE = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl";
        const string HOMOLOG_CANCELA_CE = "https://nfeh.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl";
        const string HOMOLOG_ENVIA_CE = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl";
        const string HOMOLOG_RETORNO_CE = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl";

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Classes públicas
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public NFE()
        {
            // Configuração do binding manual (para não ser necessário a adição no app.config)

            objHttpsTransportBindingElement.RequireClientCertificate = true;
            objHttpsTransportBindingElement.AuthenticationScheme = System.Net.AuthenticationSchemes.Digest;

            objTextMessageEncodingBindingElement.MessageVersion = MessageVersion.Soap12;
            objTextMessageEncodingBindingElement.WriteEncoding = Encoding.UTF8;

            objCustomBinding = new CustomBinding(objTextMessageEncodingBindingElement, objHttpsTransportBindingElement);
        }

        public bool ValidarXML(string XML, string XSD)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(null, XSD);
                settings.ValidationType = ValidationType.Schema;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(XML);

                XmlReader xmlReader = XmlReader.Create(new StringReader(xmlDocument.InnerXml), settings);
                while (xmlReader.Read()) { }

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        public XmlNode status(string AMBIENTE, string UF)
        {
            XmlNode resultado = null;

            if (UF == "SP")
                resultado = status_SP(AMBIENTE);

            else if (UF == "RS")
                resultado = status_RS(AMBIENTE);

            else if (UF == "CE")
                resultado = status_CE(AMBIENTE);

            else if ((UF == "MA") || (UF == "PA") || (UF == "PI"))
                resultado = status_SVAN(AMBIENTE, UF);

            else if ((UF == "AC") || (UF == "AL") || (UF == "AP") || (UF == "DF") || (UF == "ES") || (UF == "PB") || (UF == "RJ") || (UF == "RN") || (UF == "RO") || (UF == "RR") || (UF == "SC") || (UF == "SE") || (UF == "TO"))
                resultado = status_SVRS(AMBIENTE, UF);

            else if (UF == "AM")
                resultado = status_AM(AMBIENTE);

            else if (UF == "BA")
                resultado = status_BA(AMBIENTE);

            else if (UF == "GO")
                resultado = status_GO(AMBIENTE);

            else if (UF == "MG")
                resultado = status_MG(AMBIENTE);

            else if (UF == "PE")
                resultado = status_PE(AMBIENTE);

            else if (UF == "MS")
                resultado = status_MS(AMBIENTE);

            else if (UF == "PR")
                resultado = status_PR(AMBIENTE);

            else if (UF == "MT")
                resultado = status_MT(AMBIENTE);

            else
                throw new Exception("UF (estado) informada não existe");

            return resultado;
        }

        public XmlNode protocolo(string AMBIENTE, string UF, string CHAVE)
        {
            XmlNode resultado = null;

            if (UF == "SP")
                resultado = protocolo_SP(AMBIENTE, CHAVE);

            else if (UF == "RS")
                resultado = protocolo_RS(AMBIENTE, CHAVE);

            else if (UF == "CE")
                resultado = protocolo_CE(AMBIENTE, CHAVE);

            else if ((UF == "MA") || (UF == "PA") || (UF == "PI"))
                resultado = protocolo_SVAN(AMBIENTE, UF, CHAVE);

            else if ((UF == "AC") || (UF == "AL") || (UF == "AP") || (UF == "DF") || (UF == "ES") || (UF == "PB") || (UF == "RJ") || (UF == "RN") || (UF == "RO") || (UF == "RR") || (UF == "SC") || (UF == "SE") || (UF == "TO"))
                resultado = protocolo_SVRS(AMBIENTE, UF, CHAVE);

            else if (UF == "AM")
                resultado = protocolo_AM(AMBIENTE, CHAVE);

            else if (UF == "BA")
                resultado = protocolo_BA(AMBIENTE, CHAVE);

            else if (UF == "GO")
                resultado = protocolo_GO(AMBIENTE, CHAVE);

            else if (UF == "MG")
                resultado = protocolo_MG(AMBIENTE, CHAVE);

            else if (UF == "PE")
                resultado = protocolo_PE(AMBIENTE, CHAVE);

            else if (UF == "MS")
                resultado = protocolo_MS(AMBIENTE, CHAVE);

            else if (UF == "PR")
                resultado = protocolo_PR(AMBIENTE, CHAVE);

            else if (UF == "MT")
                resultado = protocolo_MT(AMBIENTE, CHAVE);

            else
                throw new Exception("UF (estado) informada não existe");

            return resultado;
        }

        public XmlNode cancela(string AMBIENTE, string UF, string CHAVE, string CNPJ, string PROTOCOLO)
        {
            XmlNode resultado = null;

            if (UF == "SP")
                resultado = cancela_SP(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else if (UF == "RS")
                resultado = cancela_RS(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else if (UF == "CE")
                resultado = cancela_CE(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else if ((UF == "MA") || (UF == "PA") || (UF == "PI"))
                resultado = cancela_SVAN(AMBIENTE, UF, CHAVE, CNPJ, PROTOCOLO);

            else if ((UF == "AC") || (UF == "AL") || (UF == "AP") || (UF == "DF") || (UF == "ES") || (UF == "PB") || (UF == "RJ") || (UF == "RN") || (UF == "RO") || (UF == "RR") || (UF == "SC") || (UF == "SE") || (UF == "TO"))
                resultado = cancela_SVRS(AMBIENTE, UF, CHAVE, CNPJ, PROTOCOLO);

            else if (UF == "AM")
                resultado = cancela_AM(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else if (UF == "BA")
                resultado = cancela_BA(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else if (UF == "GO")
                resultado = cancela_GO(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else if (UF == "MG")
                resultado = cancela_MG(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else if (UF == "PE")
                resultado = cancela_PE(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else if (UF == "MS")
                resultado = cancela_MS(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else if (UF == "PR")
                resultado = cancela_PR(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else if (UF == "MT")
                resultado = cancela_MT(AMBIENTE, CHAVE, CNPJ, PROTOCOLO);

            else
                throw new Exception("UF (estado) informada não existe");

            return resultado;
        }

        public XmlNode envia(string AMBIENTE, string UF, string XML)
        {
            XmlNode resultado = null;

            if (UF == "SP")
                resultado = envia_SP(AMBIENTE, XML);

            else if (UF == "RS")
                resultado = envia_RS(AMBIENTE, XML);

            else if (UF == "CE")
                resultado = envia_CE(AMBIENTE, XML);

            else if ((UF == "MA") || (UF == "PA") || (UF == "PI"))
                resultado = envia_SVAN(AMBIENTE, UF, XML);

            else if ((UF == "AC") || (UF == "AL") || (UF == "AP") || (UF == "DF") || (UF == "ES") || (UF == "PB") || (UF == "RJ") || (UF == "RN") || (UF == "RO") || (UF == "RR") || (UF == "SC") || (UF == "SE") || (UF == "TO"))
                resultado = envia_SVRS(AMBIENTE, UF, XML);

            else if (UF == "AM")
                resultado = envia_AM(AMBIENTE, XML);

            else if (UF == "BA")
                resultado = envia_BA(AMBIENTE, XML);

            else if (UF == "GO")
                resultado = envia_GO(AMBIENTE, XML);

            else if (UF == "MG")
                resultado = envia_MG(AMBIENTE, XML);

            else if (UF == "PE")
                resultado = envia_PE(AMBIENTE, XML);

            else if (UF == "MS")
                resultado = envia_MS(AMBIENTE, XML);

            else if (UF == "PR")
                resultado = envia_PR(AMBIENTE, XML);

            else if (UF == "MT")
                resultado = envia_MT(AMBIENTE, XML);

            else
                throw new Exception("UF (estado) informada não existe");

            return resultado;
        }

        public XmlNode retorno(string AMBIENTE, string UF, string RECIBO)
        {
            XmlNode resultado = null;

            if (UF == "SP")
                resultado = retorno_SP(AMBIENTE, RECIBO);

            else if (UF == "RS")
                resultado = retorno_RS(AMBIENTE, RECIBO);

            else if (UF == "CE")
                resultado = retorno_CE(AMBIENTE, RECIBO);

            else if ((UF == "MA") || (UF == "PA") || (UF == "PI"))
                resultado = retorno_SVAN(AMBIENTE, UF, RECIBO);

            else if ((UF == "AC") || (UF == "AL") || (UF == "AP") || (UF == "DF") || (UF == "ES") || (UF == "PB") || (UF == "RJ") || (UF == "RN") || (UF == "RO") || (UF == "RR") || (UF == "SC") || (UF == "SE") || (UF == "TO"))
                resultado = retorno_SVRS(AMBIENTE, UF, RECIBO);

            else if (UF == "AM")
                resultado = retorno_AM(AMBIENTE, RECIBO);

            else if (UF == "BA")
                resultado = retorno_BA(AMBIENTE, RECIBO);

            else if (UF == "GO")
                resultado = retorno_GO(AMBIENTE, RECIBO);

            else if (UF == "MG")
                resultado = retorno_MG(AMBIENTE, RECIBO);

            else if (UF == "PE")
                resultado = retorno_PE(AMBIENTE, RECIBO);

            else if (UF == "MS")
                resultado = retorno_MS(AMBIENTE, RECIBO);

            else if (UF == "PR")
                resultado = retorno_PR(AMBIENTE, RECIBO);

            else if (UF == "MT")
                resultado = retorno_MT(AMBIENTE, RECIBO);

            else
                throw new Exception("UF (estado) informada não existe");

            return resultado;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Classes privadas
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private class AssinaturaDigital
        {

            /*
             *     Entradas:
             *         XMLString: string XML a ser assinada
             *         RefUri   : Referência da URI a ser assinada (Ex. infNFe)
             *         X509Cert : certificado digital a ser utilizado na assinatura digital
             * 
             *     Retornos:
             *         Assinar : 0 - Assinatura realizada com sucesso
             *                   1 - Erro: Problema ao acessar o certificado digital - %exceção%
             *                   2 - Problemas no certificado digital
             *                   3 - XML mal formado + exceção
             *                   4 - A tag de assinatura %RefUri% inexiste
             *                   5 - A tag de assinatura %RefUri% não é unica
             *                   6 - Erro Ao assinar o documento - ID deve ser string %RefUri(Atributo)%
             *                   7 - Erro: Ao assinar o documento - %exceção%
             * 
             *         XMLStringAssinado : string XML assinada
             * 
             *         XMLDocAssinado    : XMLDocument do XML assinado
             */

            public int Assinar(string XMLString, string RefUri, X509Certificate2 X509Cert)
            {
                int resultado = 0;
                msgResultado = "Assinatura realizada com sucesso";

                try
                {
                    string _xnome = "";
                    if (X509Cert != null)
                    {
                        _xnome = X509Cert.Subject.ToString();
                    }

                    X509Certificate2 _X509Cert = new X509Certificate2();
                    X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                    X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, _xnome, false);

                    if (collection1.Count == 0)
                    {
                        resultado = 2;
                        msgResultado = "Problemas no certificado digital";
                    }
                    else
                    {
                        _X509Cert = collection1[0];
                        string x;
                        x = _X509Cert.GetKeyAlgorithm().ToString();
                        XmlDocument doc = new XmlDocument();
                        doc.PreserveWhitespace = false;

                        try
                        {
                            doc.LoadXml(XMLString);

                            int qtdeRefUri = doc.GetElementsByTagName(RefUri).Count;

                            if (qtdeRefUri == 0)
                            {
                                resultado = 4;
                                msgResultado = "A tag de assinatura " + RefUri.Trim() + " inexiste";
                            }
                            else
                            {
                                if (qtdeRefUri > 1)
                                {
                                    resultado = 5;
                                    msgResultado = "A tag de assinatura " + RefUri.Trim() + " não é unica";

                                }
                                else
                                {
                                    try
                                    {
                                        SignedXml signedXml = new SignedXml(doc);
                                        signedXml.SigningKey = _X509Cert.PrivateKey;

                                        Reference reference = new Reference();

                                        XmlAttributeCollection _Uri = doc.GetElementsByTagName(RefUri).Item(0).Attributes;
                                        foreach (XmlAttribute _atributo in _Uri)
                                        {
                                            if (_atributo.Name == "Id")
                                            {
                                                reference.Uri = "#" + _atributo.InnerText;
                                            }
                                        }

                                        XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                                        reference.AddTransform(env);

                                        XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                                        reference.AddTransform(c14);

                                        signedXml.AddReference(reference);

                                        KeyInfo keyInfo = new KeyInfo();
                                        keyInfo.AddClause(new KeyInfoX509Data(_X509Cert));

                                        signedXml.KeyInfo = keyInfo;
                                        signedXml.ComputeSignature();

                                        XmlElement xmlDigitalSignature = signedXml.GetXml();
                                        doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                                        XMLDoc = new XmlDocument();
                                        XMLDoc.PreserveWhitespace = false;
                                        XMLDoc = doc;
                                    }
                                    catch (Exception caught)
                                    {
                                        resultado = 7;
                                        msgResultado = "Erro: Ao assinar o documento - " + caught.Message;
                                    }
                                }
                            }
                        }
                        catch (Exception caught)
                        {
                            resultado = 3;
                            msgResultado = "Erro: XML mal formado - " + caught.Message;
                        }
                    }
                }
                catch (Exception caught)
                {
                    resultado = 1;
                    msgResultado = "Erro: Problema ao acessar o certificado digital" + caught.Message;
                }

                return resultado;
            }

            private string msgResultado;
            private XmlDocument XMLDoc;
            public XmlDocument XMLDocAssinado
            {
                get { return XMLDoc; }
            }

            public string XMLStringAssinado
            {
                get { return XMLDoc.OuterXml; }
            }

            public string mensagemResultado
            {
                get { return msgResultado; }
            }
        }
        
        private class Certificado
        {
            public X509Certificate2 BuscaNome(string Nome)
            {
                X509Certificate2 _X509Cert = new X509Certificate2();
                try
                {
                    X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                    X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                    X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);

                    if (Nome == "")
                    {
                        X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado Digital", "Selecione o Certificado Digital para uso no aplicativo", X509SelectionFlag.SingleSelection);
                        if (scollection.Count == 0)
                        {
                            _X509Cert.Reset();
                            throw new Exception("Nenhum certificado válido foi encontrado com o nome informado: " + Nome);
                        }
                        else
                        {
                            _X509Cert = scollection[0];
                        }
                    }
                    else
                    {
                        X509Certificate2Collection scollection = (X509Certificate2Collection)collection2.Find(X509FindType.FindBySubjectDistinguishedName, Nome, false);
                        if (scollection.Count == 0)
                        {
                            _X509Cert.Reset();
                            throw new Exception("Nenhum certificado válido foi encontrado com o nome informado: " + Nome);
                        }
                        else
                        {
                            _X509Cert = scollection[0];
                        }
                    }

                    store.Close();
                    return _X509Cert;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return _X509Cert;
                }
            }
        }

        private string UFtoIBGE(string UF)
        {
            string resultado = "";

            if (UF == "RO")
                resultado = "11";

            else if (UF == "AC")
                resultado = "12";

            else if (UF == "AM")
                resultado = "13";

            else if (UF == "RR")
                resultado = "14";

            else if (UF == "PA")
                resultado = "15";

            else if (UF == "AP")
                resultado = "16";

            else if (UF == "TO")
                resultado = "17";

            else if (UF == "MA")
                resultado = "21";

            else if (UF == "PI")
                resultado = "22";

            else if (UF == "CE")
                resultado = "23";

            else if (UF == "RN")
                resultado = "24";

            else if (UF == "PB")
                resultado = "25";

            else if (UF == "PE")
                resultado = "26";

            else if (UF == "AL")
                resultado = "27";

            else if (UF == "SE")
                resultado = "28";

            else if (UF == "BA")
                resultado = "29";

            else if (UF == "MG")
                resultado = "31";

            else if (UF == "ES")
                resultado = "32";

            else if (UF == "RJ")
                resultado = "33";

            else if (UF == "SP")
                resultado = "35";

            else if (UF == "PR")
                resultado = "41";

            else if (UF == "SC")
                resultado = "42";

            else if (UF == "RS")
                resultado = "43";

            else if (UF == "MS")
                resultado = "50";

            else if (UF == "MT")
                resultado = "51";

            else if (UF == "GO")
                resultado = "52";

            else if (UF == "DF")
                resultado = "53";

            else
                throw new Exception("UF (estado) informada não existe");

            return resultado;
        }

        //===============================================================================================================
        // SÃO PAULO - SP                                                                  |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_SP(string AMBIENTE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_SP : HOMOLOG_STATUS_SP;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_SP.nfeCabecMsg CABEC = new Status3_SP.nfeCabecMsg();
            CABEC.cUF = "35";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_SP.NfeStatusServico2Soap12Client client = new Status3_SP.NfeStatusServico2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeStatusServicoNF2(ref CABEC, XML);
        }

        private XmlNode protocolo_SP(string AMBIENTE, string CHAVE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_SP : HOMOLOG_PROTOCOLO_SP;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_SP.nfeCabecMsg CABEC = new Protocolo3_SP.nfeCabecMsg();
            CABEC.cUF = "35";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_SP.NfeConsulta2Soap12Client client = new Protocolo3_SP.NfeConsulta2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeConsultaNF2(ref CABEC, XML);
        }

        private XmlNode cancela_SP(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_SP : HOMOLOG_CANCELA_SP;
            
            // Cabeçalho do SOAP e consumação do webservice

            Evento3_SP.nfeCabecMsg CABEC = new Evento3_SP.nfeCabecMsg();
            CABEC.cUF = "35";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>35</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_SP.RecepcaoEventoSoap12Client client = new Evento3_SP.RecepcaoEventoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeRecepcaoEvento(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_SP(string AMBIENTE, string XML) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_SP : HOMOLOG_ENVIA_SP;
            
            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_SP.nfeCabecMsg CABEC = new Envia3_SP.nfeCabecMsg();
                CABEC.cUF = "35";
                CABEC.versaoDados = "3.10";

                Envia3_SP.NfeAutorizacaoSoap12Client client = new Envia3_SP.NfeAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeAutorizacaoLote(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_SP(string AMBIENTE, string RECIBO) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_SP : HOMOLOG_RETORNO_SP;
            
            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_SP.nfeCabecMsg CABEC = new Retorno3_SP.nfeCabecMsg();
            CABEC.cUF = "35";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_SP.NfeRetAutorizacaoSoap12Client client = new Retorno3_SP.NfeRetAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeRetAutorizacaoLote(ref CABEC, XML);
        }

        //===============================================================================================================
        // RIO GRANDE DO SUL - RS                                                          |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_RS(string AMBIENTE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_RS : HOMOLOG_STATUS_RS;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_RS.nfeCabecMsg CABEC = new Status3_RS.nfeCabecMsg();
            CABEC.cUF = "43";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_RS.NfeStatusServico2Soap12Client client = new Status3_RS.NfeStatusServico2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeStatusServicoNF2(ref CABEC, XML);
        }

        private XmlNode protocolo_RS(string AMBIENTE, string CHAVE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_RS : HOMOLOG_PROTOCOLO_RS;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_RS.nfeCabecMsg CABEC = new Protocolo3_RS.nfeCabecMsg();
            CABEC.cUF = "43";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_RS.NfeConsulta2Soap12Client client = new Protocolo3_RS.NfeConsulta2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeConsultaNF2(ref CABEC, XML);
        }

        private XmlNode cancela_RS(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_RS : HOMOLOG_CANCELA_RS;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_RS.nfeCabecMsg CABEC = new Evento3_RS.nfeCabecMsg();
            CABEC.cUF = "43";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>35</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_RS.RecepcaoEventoSoap12Client client = new Evento3_RS.RecepcaoEventoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeRecepcaoEvento(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_RS(string AMBIENTE, string XML) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_RS : HOMOLOG_ENVIA_RS;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_RS.nfeCabecMsg CABEC = new Envia3_RS.nfeCabecMsg();
                CABEC.cUF = "43";
                CABEC.versaoDados = "3.10";

                Envia3_RS.NfeAutorizacaoSoap12Client client = new Envia3_RS.NfeAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeAutorizacaoLote(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_RS(string AMBIENTE, string RECIBO) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_RS : HOMOLOG_RETORNO_RS;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_RS.nfeCabecMsg CABEC = new Retorno3_RS.nfeCabecMsg();
            CABEC.cUF = "43";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_RS.NfeRetAutorizacaoSoap12Client client = new Retorno3_RS.NfeRetAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeRetAutorizacaoLote(ref CABEC, XML);
        }

        //===============================================================================================================
        // SEFAZ VIRTUAL AMBIENTE NACIONAL - SVAN (MA, PA, PI)                             |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_SVAN(string AMBIENTE, string ESTADO) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_SVAN : HOMOLOG_STATUS_SVAN;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_SVAN.nfeCabecMsg CABEC = new Status3_SVAN.nfeCabecMsg();
            CABEC.cUF = UFtoIBGE(ESTADO);
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_SVAN.NfeStatusServico2SoapClient client = new Status3_SVAN.NfeStatusServico2SoapClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeStatusServicoNF2(CABEC, XML);
        }

        private XmlNode protocolo_SVAN(string AMBIENTE, string ESTADO, string CHAVE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_SVAN : HOMOLOG_PROTOCOLO_SVAN;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_SVAN.nfeCabecMsg CABEC = new Protocolo3_SVAN.nfeCabecMsg();
            CABEC.cUF = UFtoIBGE(ESTADO);
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_SVAN.NfeConsulta2SoapClient client = new Protocolo3_SVAN.NfeConsulta2SoapClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeConsultaNF2(CABEC, XML);
        }

        private XmlNode cancela_SVAN(string AMBIENTE, string ESTADO, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_SVAN : HOMOLOG_CANCELA_SVAN;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_SVAN.nfeCabecMsg CABEC = new Evento3_SVAN.nfeCabecMsg();
            CABEC.cUF = UFtoIBGE(ESTADO);
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>35</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_SVAN.RecepcaoEventoSoapClient client = new Evento3_SVAN.RecepcaoEventoSoapClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeRecepcaoEvento(CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_SVAN(string AMBIENTE, string ESTADO, string XML) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_SVAN : HOMOLOG_ENVIA_SVAN;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_SVAN.nfeCabecMsg CABEC = new Envia3_SVAN.nfeCabecMsg();
                CABEC.cUF = UFtoIBGE(ESTADO);
                CABEC.versaoDados = "3.10";

                Envia3_SVAN.NfeAutorizacaoSoapClient client = new Envia3_SVAN.NfeAutorizacaoSoapClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeAutorizacaoLote(CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_SVAN(string AMBIENTE, string ESTADO, string RECIBO) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_SVAN : HOMOLOG_RETORNO_SVAN;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_SVAN.nfeCabecMsg CABEC = new Retorno3_SVAN.nfeCabecMsg();
            CABEC.cUF = UFtoIBGE(ESTADO);
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_SVAN.NfeRetAutorizacaoSoapClient client = new Retorno3_SVAN.NfeRetAutorizacaoSoapClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeRetAutorizacaoLote(CABEC, XML);
        }

        //===============================================================================================================
        // SEFAZ VIRTUAL RS - SVRS (AC, AL, AP, DF, ES, PB, RJ, RN, RO, RR, SC, SE, TO)    |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_SVRS(string AMBIENTE, string ESTADO) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_SVRS : HOMOLOG_STATUS_SVRS;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_SVRS.nfeCabecMsg CABEC = new Status3_SVRS.nfeCabecMsg();
            CABEC.cUF = UFtoIBGE(ESTADO);
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_SVRS.NfeStatusServico2Soap12Client client = new Status3_SVRS.NfeStatusServico2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeStatusServicoNF2(ref CABEC, XML);
        }

        private XmlNode protocolo_SVRS(string AMBIENTE, string ESTADO, string CHAVE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_SVRS : HOMOLOG_PROTOCOLO_SVRS;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_SVRS.nfeCabecMsg CABEC = new Protocolo3_SVRS.nfeCabecMsg();
            CABEC.cUF = UFtoIBGE(ESTADO);
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_SVRS.NfeConsulta2Soap12Client client = new Protocolo3_SVRS.NfeConsulta2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeConsultaNF2(ref CABEC, XML);
        }

        private XmlNode cancela_SVRS(string AMBIENTE, string ESTADO, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_SVRS : HOMOLOG_CANCELA_SVRS;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_SVRS.nfeCabecMsg CABEC = new Evento3_SVRS.nfeCabecMsg();
            CABEC.cUF = UFtoIBGE(ESTADO);
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>35</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_SVRS.RecepcaoEventoSoap12Client client = new Evento3_SVRS.RecepcaoEventoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeRecepcaoEvento(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_SVRS(string AMBIENTE, string ESTADO, string XML) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_SVRS : HOMOLOG_ENVIA_SVRS;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_SVRS.nfeCabecMsg CABEC = new Envia3_SVRS.nfeCabecMsg();
                CABEC.cUF = UFtoIBGE(ESTADO);
                CABEC.versaoDados = "3.10";

                Envia3_SVRS.NfeAutorizacaoSoap12Client client = new Envia3_SVRS.NfeAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeAutorizacaoLote(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_SVRS(string AMBIENTE, string ESTADO, string RECIBO) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_SVRS : HOMOLOG_RETORNO_SVRS;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_SVRS.nfeCabecMsg CABEC = new Retorno3_SVRS.nfeCabecMsg();
            CABEC.cUF = UFtoIBGE(ESTADO);
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_SVRS.NfeRetAutorizacaoSoap12Client client = new Retorno3_SVRS.NfeRetAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeRetAutorizacaoLote(ref CABEC, XML);
        }

        //===============================================================================================================
        // AMAZONAS - AM (Necessita instalar certificados adicionais)                      |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_AM(string AMBIENTE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_AM : HOMOLOG_STATUS_AM;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_AM.nfeCabecMsg CABEC = new Status3_AM.nfeCabecMsg();
            CABEC.cUF = "13";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_AM.NfeStatusServico2Soap12Client client = new Status3_AM.NfeStatusServico2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeStatusServicoNF2(ref CABEC, XML);
        }

        private XmlNode protocolo_AM(string AMBIENTE, string CHAVE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_AM : HOMOLOG_PROTOCOLO_AM;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_AM.nfeCabecMsg CABEC = new Protocolo3_AM.nfeCabecMsg();
            CABEC.cUF = "13";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_AM.NfeConsulta2Soap12Client client = new Protocolo3_AM.NfeConsulta2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeConsultaNF2(ref CABEC, XML);
        }

        private XmlNode cancela_AM(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_AM : HOMOLOG_CANCELA_AM;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_AM.nfeCabecMsg CABEC = new Evento3_AM.nfeCabecMsg();
            CABEC.cUF = "13";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>35</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_AM.RecepcaoEventoSoap12Client client = new Evento3_AM.RecepcaoEventoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeRecepcaoEvento(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_AM(string AMBIENTE, string XML) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_AM : HOMOLOG_ENVIA_AM;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_AM.nfeCabecMsg CABEC = new Envia3_AM.nfeCabecMsg();
                CABEC.cUF = "13";
                CABEC.versaoDados = "3.10";

                Envia3_AM.NfeAutorizacaoSoap12Client client = new Envia3_AM.NfeAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeAutorizacaoLote(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_AM(string AMBIENTE, string RECIBO) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_AM : HOMOLOG_RETORNO_AM;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_AM.nfeCabecMsg CABEC = new Retorno3_AM.nfeCabecMsg();
            CABEC.cUF = "13";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_AM.NfeRetAutorizacaoSoap12Client client = new Retorno3_AM.NfeRetAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeRetAutorizacaoLote(ref CABEC, XML);
        }

        //===============================================================================================================
        // BAHIA - BA                                                                      |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_BA(string AMBIENTE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_BA : HOMOLOG_STATUS_BA;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_BA.nfeCabecMsg CABEC = new Status3_BA.nfeCabecMsg();
            CABEC.cUF = "29";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_BA.NfeStatusServicoSoapClient client = new Status3_BA.NfeStatusServicoSoapClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.NfeStatusServicoNF(ref CABEC, XML);
        }

        private XmlNode protocolo_BA(string AMBIENTE, string CHAVE) // XML 2.01 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_BA : HOMOLOG_PROTOCOLO_BA;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_BA.nfeCabecMsg CABEC = new Protocolo3_BA.nfeCabecMsg();
            CABEC.cUF = "29";
            CABEC.versaoDados = "2.01";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_BA.NfeConsulta2SoapClient client = new Protocolo3_BA.NfeConsulta2SoapClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeConsultaNF2(ref CABEC, XML);
        }

        private XmlNode cancela_BA(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_BA : HOMOLOG_CANCELA_BA;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_BA.nfeCabecMsg CABEC = new Evento3_BA.nfeCabecMsg();
            CABEC.cUF = "29";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>35</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_BA.RecepcaoEventoSoapClient client = new Evento3_BA.RecepcaoEventoSoapClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeRecepcaoEvento(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_BA(string AMBIENTE, string XML) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_BA : HOMOLOG_ENVIA_BA;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_BA.nfeCabecMsg CABEC = new Envia3_BA.nfeCabecMsg();
                CABEC.cUF = "29";
                CABEC.versaoDados = "3.10";

                Envia3_BA.NfeAutorizacaoSoapClient client = new Envia3_BA.NfeAutorizacaoSoapClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.NfeAutorizacaoLote(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_BA(string AMBIENTE, string RECIBO) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_BA : HOMOLOG_RETORNO_BA;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_BA.nfeCabecMsg CABEC = new Retorno3_BA.nfeCabecMsg();
            CABEC.cUF = "29";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_BA.NfeRetAutorizacaoSoapClient client = new Retorno3_BA.NfeRetAutorizacaoSoapClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.NfeRetAutorizacaoLote(ref CABEC, XML);
        }

        //===============================================================================================================
        // GOIÁS - GO                                                                      |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_GO(string AMBIENTE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_GO : HOMOLOG_STATUS_GO;
            
            // Cabeçalho do SOAP e consumação do webservice

            Status3_GO.nfeCabecMsg CABEC = new Status3_GO.nfeCabecMsg();
            CABEC.cUF = "52";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_GO.NfeStatusServicoServiceClient client = new Status3_GO.NfeStatusServicoServiceClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeStatusServicoNF2(CABEC, XML);
        }

        private XmlNode protocolo_GO(string AMBIENTE, string CHAVE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_GO : HOMOLOG_PROTOCOLO_GO;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_GO.nfeCabecMsg CABEC = new Protocolo3_GO.nfeCabecMsg();
            CABEC.cUF = "52";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_GO.NfeConsultaServiceClient client = new Protocolo3_GO.NfeConsultaServiceClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeConsultaNF2(CABEC, XML);
        }

        private XmlNode cancela_GO(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_GO : HOMOLOG_CANCELA_GO;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_GO.nfeCabecMsg CABEC = new Evento3_GO.nfeCabecMsg();
            CABEC.cUF = "52";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>35</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_GO.RecepcaoEventoServiceClient client = new Evento3_GO.RecepcaoEventoServiceClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeRecepcaoEvento(CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_GO(string AMBIENTE, string XML) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_GO : HOMOLOG_ENVIA_GO;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_GO.nfeCabecMsg CABEC = new Envia3_GO.nfeCabecMsg();
                CABEC.cUF = "52";
                CABEC.versaoDados = "3.10";

                Envia3_GO.NfeAutorizacaoServiceClient client = new Envia3_GO.NfeAutorizacaoServiceClient(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeAutorizacaoLote(CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_GO(string AMBIENTE, string RECIBO) // XML 3.10 usando classe Retorno3_RS 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_GO : HOMOLOG_RETORNO_GO;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_RS.nfeCabecMsg CABEC = new Retorno3_RS.nfeCabecMsg();
            CABEC.cUF = "52";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_RS.NfeRetAutorizacaoSoap12Client client = new Retorno3_RS.NfeRetAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeRetAutorizacaoLote(ref CABEC, XML);
        }

        //===============================================================================================================
        // MINAS GERAIS - MG (Usando classes RS)                                           |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_MG(string AMBIENTE) // XML 2.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_MG : HOMOLOG_STATUS_MG;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_RS.nfeCabecMsg CABEC = new Status3_RS.nfeCabecMsg();
            CABEC.cUF = "31";
            CABEC.versaoDados = "2.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_RS.NfeStatusServico2Soap12Client client = new Status3_RS.NfeStatusServico2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeStatusServicoNF2(ref CABEC, XML);
        }

        private XmlNode protocolo_MG(string AMBIENTE, string CHAVE) // XML 2.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_MG : HOMOLOG_PROTOCOLO_MG;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_RS.nfeCabecMsg CABEC = new Protocolo3_RS.nfeCabecMsg();
            CABEC.cUF = "31";
            CABEC.versaoDados = "2.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_RS.NfeConsulta2Soap12Client client = new Protocolo3_RS.NfeConsulta2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeConsultaNF2(ref CABEC, XML);
        }

        private XmlNode cancela_MG(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_MG : HOMOLOG_CANCELA_MG;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_RS.nfeCabecMsg CABEC = new Evento3_RS.nfeCabecMsg();
            CABEC.cUF = "31";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>35</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_RS.RecepcaoEventoSoap12Client client = new Evento3_RS.RecepcaoEventoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeRecepcaoEvento(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_MG(string AMBIENTE, string XML) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_MG : HOMOLOG_ENVIA_MG;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_RS.nfeCabecMsg CABEC = new Envia3_RS.nfeCabecMsg();
                CABEC.cUF = "31";
                CABEC.versaoDados = "3.10";

                Envia3_RS.NfeAutorizacaoSoap12Client client = new Envia3_RS.NfeAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeAutorizacaoLote(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_MG(string AMBIENTE, string RECIBO) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_MG : HOMOLOG_RETORNO_MG;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_RS.nfeCabecMsg CABEC = new Retorno3_RS.nfeCabecMsg();
            CABEC.cUF = "31";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_RS.NfeRetAutorizacaoSoap12Client client = new Retorno3_RS.NfeRetAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeRetAutorizacaoLote(ref CABEC, XML);
        }

        //===============================================================================================================
        // PERNAMBUCO - PE                                                                 |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_PE(string AMBIENTE) // XML 2.00 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_PE : HOMOLOG_STATUS_PE;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_PE.nfeCabecMsg CABEC = new Status3_PE.nfeCabecMsg();
            CABEC.cUF = "26";
            CABEC.versaoDados = "2.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_PE.NfeStatusServico2 client = new Status3_PE.NfeStatusServico2();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeStatusServicoNF2(XML);
        }

        private XmlNode protocolo_PE(string AMBIENTE, string CHAVE) // XML 2.00 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_PE : HOMOLOG_PROTOCOLO_PE;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_PE.nfeCabecMsg CABEC = new Protocolo3_PE.nfeCabecMsg();
            CABEC.cUF = "26";
            CABEC.versaoDados = "2.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_PE.NfeConsulta2 client = new Protocolo3_PE.NfeConsulta2();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeConsultaNF2(XML);
        }

        private XmlNode cancela_PE(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_PE : HOMOLOG_CANCELA_PE;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_PE.nfeCabecMsg CABEC = new Evento3_PE.nfeCabecMsg();
            CABEC.cUF = "26";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>51</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_PE.RecepcaoEvento client = new Evento3_PE.RecepcaoEvento();

                client.Url = LINK;
                client.nfeCabecMsgValue = CABEC;
                client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                client.ClientCertificates.Add(cert);

                return client.nfeRecepcaoEvento(XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_PE(string AMBIENTE, string XML) // XML 3.10  (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_PE : HOMOLOG_ENVIA_PE;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_PE.nfeCabecMsg CABEC = new Envia3_PE.nfeCabecMsg();
                CABEC.cUF = "26";
                CABEC.versaoDados = "3.10";

                Envia3_PE.NfeAutorizacao client = new Envia3_PE.NfeAutorizacao();

                client.Url = LINK;
                client.nfeCabecMsgValue = CABEC;
                client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                client.ClientCertificates.Add(cert);

                return client.nfeAutorizacaoLote(XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_PE(string AMBIENTE, string RECIBO) // XML 3.10  (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_PE : HOMOLOG_RETORNO_PE;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_PE.nfeCabecMsg CABEC = new Retorno3_PE.nfeCabecMsg();
            CABEC.cUF = "26";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            Retorno3_PE.NfeRetAutorizacao client = new Retorno3_PE.NfeRetAutorizacao();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeRetAutorizacaoLote(XML);
        }

        //===============================================================================================================
        // MATO GROSSO DO SUL - MS (Usando classes SVRS)                                   |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_MS(string AMBIENTE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_MS : HOMOLOG_STATUS_MS;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_SVRS.nfeCabecMsg CABEC = new Status3_SVRS.nfeCabecMsg();
            CABEC.cUF = "50";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_SVRS.NfeStatusServico2Soap12Client client = new Status3_SVRS.NfeStatusServico2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeStatusServicoNF2(ref CABEC, XML);
        }

        private XmlNode protocolo_MS(string AMBIENTE, string CHAVE) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_MS : HOMOLOG_PROTOCOLO_MS;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_SVRS.nfeCabecMsg CABEC = new Protocolo3_SVRS.nfeCabecMsg();
            CABEC.cUF = "50";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_SVRS.NfeConsulta2Soap12Client client = new Protocolo3_SVRS.NfeConsulta2Soap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeConsultaNF2(ref CABEC, XML);
        }

        private XmlNode cancela_MS(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_MS : HOMOLOG_CANCELA_MS;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_SVRS.nfeCabecMsg CABEC = new Evento3_SVRS.nfeCabecMsg();
            CABEC.cUF = "50";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>35</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_SVRS.RecepcaoEventoSoap12Client client = new Evento3_SVRS.RecepcaoEventoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeRecepcaoEvento(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_MS(string AMBIENTE, string XML) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_MS : HOMOLOG_ENVIA_MS;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_SVRS.nfeCabecMsg CABEC = new Envia3_SVRS.nfeCabecMsg();
                CABEC.cUF = "50";
                CABEC.versaoDados = "3.10";

                Envia3_SVRS.NfeAutorizacaoSoap12Client client = new Envia3_SVRS.NfeAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                client.ClientCredentials.ClientCertificate.Certificate = cert;

                return client.nfeAutorizacaoLote(ref CABEC, XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_MS(string AMBIENTE, string RECIBO) // XML 3.10 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_MS : HOMOLOG_RETORNO_MS;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_SVRS.nfeCabecMsg CABEC = new Retorno3_SVRS.nfeCabecMsg();
            CABEC.cUF = "50";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_SVRS.NfeRetAutorizacaoSoap12Client client = new Retorno3_SVRS.NfeRetAutorizacaoSoap12Client(objCustomBinding, new System.ServiceModel.EndpointAddress(LINK));
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.ClientCertificate.Certificate = cert;

            return client.nfeRetAutorizacaoLote(ref CABEC, XML);
        }

        //===============================================================================================================
        // PARANÁ - PR                                                                     |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_PR(string AMBIENTE) // XML 3.10 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_PR : HOMOLOG_STATUS_PR;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_PR.nfeCabecMsg CABEC = new Status3_PR.nfeCabecMsg();
            CABEC.cUF = "41";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_PR.NfeStatusServico3 client = new Status3_PR.NfeStatusServico3();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeStatusServicoNF(XML);
        }

        private XmlNode protocolo_PR(string AMBIENTE, string CHAVE) // XML 3.10 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_PR : HOMOLOG_PROTOCOLO_PR;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_PR.nfeCabecMsg CABEC = new Protocolo3_PR.nfeCabecMsg();
            CABEC.cUF = "41";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_PR.NfeConsulta3 client = new Protocolo3_PR.NfeConsulta3();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeConsultaNF(XML);
        }

        private XmlNode cancela_PR(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_PR : HOMOLOG_CANCELA_PR;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_PR.nfeCabecMsg CABEC = new Evento3_PR.nfeCabecMsg();
            CABEC.cUF = "41";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>35</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_PR.RecepcaoEvento client = new Evento3_PR.RecepcaoEvento();

                client.Url = LINK;
                client.nfeCabecMsgValue = CABEC;
                client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                client.ClientCertificates.Add(cert);

                return client.nfeRecepcaoEvento(XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_PR(string AMBIENTE, string XML) // XML 3.10 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_PR : HOMOLOG_ENVIA_PR;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_PR.nfeCabecMsg CABEC = new Envia3_PR.nfeCabecMsg();
                CABEC.cUF = "41";
                CABEC.versaoDados = "3.10";

                Envia3_PR.NfeAutorizacao3 client = new Envia3_PR.NfeAutorizacao3();

                client.Url = LINK;
                client.nfeCabecMsgValue = CABEC;
                client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                client.ClientCertificates.Add(cert);

                return client.nfeAutorizacaoLote(XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_PR(string AMBIENTE, string RECIBO) // XML 3.10 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_PR : HOMOLOG_RETORNO_PR;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_PR.nfeCabecMsg CABEC = new Retorno3_PR.nfeCabecMsg();
            CABEC.cUF = "41";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_PR.NfeRetAutorizacao3 client = new Retorno3_PR.NfeRetAutorizacao3();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeRetAutorizacao(XML);
        }

        //===============================================================================================================
        // MATO GROSSO - MT                                                                |  SOMENTE WEBSERVICES 3.10  |
        //===============================================================================================================

        private XmlNode status_MT(string AMBIENTE) // XML 3.10 - Usando Web Reference (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_MT : HOMOLOG_STATUS_MT;

            // Cabeçalho do SOAP e consumação do webservice

            Status3_MT.nfeCabecMsg CABEC = new Status3_MT.nfeCabecMsg();
            CABEC.cUF = "51";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status3_MT.NfeStatusServico2 client = new Status3_MT.NfeStatusServico2();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeStatusServicoNF2(XML);
        }

        private XmlNode protocolo_MT(string AMBIENTE, string CHAVE) // XML 3.10 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_MT : HOMOLOG_PROTOCOLO_MT;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo3_MT.nfeCabecMsg CABEC = new Protocolo3_MT.nfeCabecMsg();
            CABEC.cUF = "51";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo3_MT.NfeConsulta2 client = new Protocolo3_MT.NfeConsulta2();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeConsultaNF2(XML);
        }

        private XmlNode cancela_MT(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_MT : HOMOLOG_CANCELA_MT;

            // Cabeçalho do SOAP e consumação do webservice

            Evento3_MT.nfeCabecMsg CABEC = new Evento3_MT.nfeCabecMsg();
            CABEC.cUF = "51";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>51</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento3_MT.RecepcaoEvento client = new Evento3_MT.RecepcaoEvento();

                client.Url = LINK;
                client.nfeCabecMsgValue = CABEC;
                client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                client.ClientCertificates.Add(cert);

                return client.nfeRecepcaoEvento(XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_MT(string AMBIENTE, string XML) // XML 3.10 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_MT : HOMOLOG_ENVIA_MT;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia3_MT.nfeCabecMsg CABEC = new Envia3_MT.nfeCabecMsg();
                CABEC.cUF = "51";
                CABEC.versaoDados = "3.10";

                Envia3_MT.NfeAutorizacao client = new Envia3_MT.NfeAutorizacao();

                client.Url = LINK;
                client.nfeCabecMsgValue = CABEC;
                client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                client.ClientCertificates.Add(cert);

                return client.nfeAutorizacaoLote(XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_MT(string AMBIENTE, string RECIBO) // XML 3.10 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_MT : HOMOLOG_RETORNO_MT;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno3_MT.nfeCabecMsg CABEC = new Retorno3_MT.nfeCabecMsg();
            CABEC.cUF = "51";
            CABEC.versaoDados = "3.10";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno3_MT.NfeRetAutorizacao client = new Retorno3_MT.NfeRetAutorizacao();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeRetAutorizacaoLote(XML);
        }
        
        //===============================================================================================================
        // CEARÁ - CE                                                                      |  SOMENTE WEBSERVICES 2.00  |
        //===============================================================================================================

        private XmlNode status_CE(string AMBIENTE) // XML 2.00 - Usando Web Reference (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_STATUS_CE : HOMOLOG_STATUS_CE;

            // Cabeçalho do SOAP e consumação do webservice

            Status2_CE.nfeCabecMsg CABEC = new Status2_CE.nfeCabecMsg();
            CABEC.cUF = "23";
            CABEC.versaoDados = "2.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"" + CABEC.versaoDados + "\"><tpAmb>" + AMBIENTE + "</tpAmb><cUF>" + CABEC.cUF + "</cUF><xServ>STATUS</xServ></consStatServ>");

            cert = certificado.BuscaNome("");
            Status2_CE.NfeStatusServico2 client = new Status2_CE.NfeStatusServico2();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeStatusServicoNF2(XML);
        }

        private XmlNode protocolo_CE(string AMBIENTE, string CHAVE) // XML 2.00 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_PROTOCOLO_CE : HOMOLOG_PROTOCOLO_CE;

            // Cabeçalho do SOAP e consumação do webservice

            Protocolo2_CE.nfeCabecMsg CABEC = new Protocolo2_CE.nfeCabecMsg();
            CABEC.cUF = "23";
            CABEC.versaoDados = "2.01";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consSitNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><xServ>CONSULTAR</xServ><chNFe>" + CHAVE + "</chNFe></consSitNFe>");

            cert = certificado.BuscaNome("");
            Protocolo2_CE.NfeConsulta2 client = new Protocolo2_CE.NfeConsulta2();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeConsultaNF2(XML);
        }

        private XmlNode cancela_CE(string AMBIENTE, string CHAVE, string CNPJ, string PROTOCOLO) // XML 1.00 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_CANCELA_CE : HOMOLOG_CANCELA_CE;

            // Cabeçalho do SOAP e consumação do webservice

            Evento2_CE.nfeCabecMsg CABEC = new Evento2_CE.nfeCabecMsg();
            CABEC.cUF = "23";
            CABEC.versaoDados = "1.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><envEvento versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><idLote>1</idLote><evento versao=\"" + CABEC.versaoDados + "\"><infEvento Id=\"ID110111" + CHAVE + "01\"><cOrgao>51</cOrgao><tpAmb>" + AMBIENTE + "</tpAmb><CNPJ>" + CNPJ + "</CNPJ><chNFe>" + CHAVE + "</chNFe><dhEvento>" + DateTime.Now.ToString("s") + "-03:00</dhEvento><tpEvento>110111</tpEvento><nSeqEvento>1</nSeqEvento><verEvento>" + CABEC.versaoDados + "</verEvento><detEvento versao=\"" + CABEC.versaoDados + "\"><descEvento>Cancelamento</descEvento><nProt>" + PROTOCOLO + "</nProt><xJust>Cancelamento da venda ao cliente</xJust></detEvento></infEvento></evento></envEvento>");

            // Assinar infEvento

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML.OuterXml, "infEvento", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</evento>", "").Replace("</envEvento>", "</evento></envEvento>")); // Consertar um pequeno bug da assinatura

                Evento2_CE.RecepcaoEvento client = new Evento2_CE.RecepcaoEvento();

                client.Url = LINK;
                client.nfeCabecMsgValue = CABEC;
                client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                client.ClientCertificates.Add(cert);

                return client.nfeRecepcaoEvento(XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode envia_CE(string AMBIENTE, string XML) // XML 2.00 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_ENVIA_CE : HOMOLOG_ENVIA_CE;

            // Assinar infNFe

            XmlDocument XMLASSINADO = new XmlDocument();

            cert = certificado.BuscaNome("");
            if (AS.Assinar(XML, "infNFe", cert) == 0)
            {
                XMLASSINADO.LoadXml(AS.XMLStringAssinado.Replace("</NFe>", "").Replace("</enviNFe>", "</NFe></enviNFe>")); // Consertar um pequeno bug da assinatura

                // Cabeçalho do SOAP e consumação do webservice

                Envia2_CE.nfeCabecMsg CABEC = new Envia2_CE.nfeCabecMsg();
                CABEC.cUF = "23";
                CABEC.versaoDados = "2.00";

                Envia2_CE.NfeRecepcao2 client = new Envia2_CE.NfeRecepcao2();

                client.Url = LINK;
                client.nfeCabecMsgValue = CABEC;
                client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                client.ClientCertificates.Add(cert);

                return client.nfeRecepcaoLote2(XMLASSINADO);
            }
            else
                throw new Exception("Erro ao tentar assinar o XML");
        }

        private XmlNode retorno_CE(string AMBIENTE, string RECIBO) // XML 2.00 (Framework 2 - Compatibilidade) 
        {
            string LINK = (AMBIENTE == "1") ? PROD_RETORNO_CE : HOMOLOG_RETORNO_CE;

            // Cabeçalho do SOAP e consumação do webservice

            Retorno2_CE.nfeCabecMsg CABEC = new Retorno2_CE.nfeCabecMsg();
            CABEC.cUF = "23";
            CABEC.versaoDados = "2.00";

            // XML da requisição

            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><consReciNFe versao=\"" + CABEC.versaoDados + "\" xmlns=\"http://www.portalfiscal.inf.br/nfe\"><tpAmb>" + AMBIENTE + "</tpAmb><nRec>" + RECIBO + "</nRec></consReciNFe>");

            cert = certificado.BuscaNome("");
            Retorno2_CE.NfeRetRecepcao2 client = new Retorno2_CE.NfeRetRecepcao2();

            client.Url = LINK;
            client.nfeCabecMsgValue = CABEC;
            client.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            client.ClientCertificates.Add(cert);

            return client.nfeRetRecepcao2(XML);
        }
    }
}
