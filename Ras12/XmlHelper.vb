Imports System.Linq
Imports System.Xml.Linq

Imports <xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
Imports <xmlns:sec="http://schemas.xmlsoap.org/ws/2002/12/secext">

Imports <xmlns:mes="http://www.ebxml.org/namespaces/messageHeader">
'Imports <xmlns:ns="http://www.opentravel.org/OTA/2002/11">
Imports <xmlns:ns="http://webservices.sabre.com/sabreXML/2003/07">
'Imports <xmlns:xs = "http://www.w3.org/2001/XMLSchema">
'Imports <xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
Public Class XmlHelper

    Public Shared Function AddE_InvoiceCustomer(strCustFullName As String, strCustShortName As String _
                                , strTaxCode As String, strAddress As String, strEmail As String _
                                , intCustType As Integer _
                                , Optional strBankAccountName As String = "", Optional strBankName As String = "" _
                                , Optional strAccountNumber As String = "", Optional strPhone As String = "" _
                                , Optional strContact As String = "", Optional strRepresentative As String = "") As XElement
        Dim objXElement As XElement =
            <Customers>
                <Customer>
                    <Name><%= strCustFullName %></Name>
                    <Code><%= strCustShortName %></Code>
                    <TaxCode><%= strTaxCode %></TaxCode>
                    <Address><%= strAddress %></Address>
                    <BankAccountName><%= strBankAccountName %></BankAccountName>
                    <BankName><%= strBankName %></BankName>
                    <BankNumber><%= strBankName %></BankNumber>
                    <Email><%= strEmail %></Email>
                    <Fax></Fax>
                    <Phone><%= strPhone %></Phone>
                    <ContactPerson><%= strContact %></ContactPerson>
                    <RepresentPerson><%= strRepresentative %></RepresentPerson>
                    <CusType><%= intCustType %></CusType>
                </Customer>
            </Customers>
        Return objXElement
    End Function

    Public Shared Function OTA_PingRQ(strEchoData As String) As XElement
        Dim objOTA_PingRQ As XElement =
                <OTA_PingRQ xmlns="http://www.opentravel.org/OTA/2003/05" TimeStamp="2011-02-18T10:15:00-06:00" Version="1.0.0">
                    <EchoData><%= strEchoData %></EchoData>
                </OTA_PingRQ>
        Return objOTA_PingRQ
    End Function
    Public Shared Function DetailRQ(strTicketingProvider As String _
                        , strTkno As String) As XElement
        Dim objSabreCommandLLSRQ As XElement =
                <DetailRQ version="1.0.0" xmlns="http://www.sabre.com/ns/Ticketing/AsrServices/1.0">
                    <Header/>
                    <SelectionCriteria>
                        <TicketingProvider><%= strTicketingProvider %></TicketingProvider>
                        <DocumentNumber><%= strTkno %></DocumentNumber>
                    </SelectionCriteria>
                </DetailRQ>
        Return objSabreCommandLLSRQ
    End Function
    Public Shared Function StationActivityRQ(dteReportDate As Date _
                        , strStationNbr As String) As XElement
        ' cau lenh lam duoc nhugn web service thi bi loi
        Dim strReportDate As String = Format(dteReportDate, "yyyy-MM-dd")
        Dim objSabreCommandLLSRQ As XElement =
                <StationActivityRQ version="1.0.0" xmlns="http://www.sabre.com/ns/Ticketing/AsrServices/1.0">
                    <Header/>
                    <SelectionCriteria>
                        <TicketingProvider>VN</TicketingProvider>
                        <StationNumber><%= strStationNbr %></StationNumber>
                        <ReportDate><%= strReportDate %></ReportDate>
                    </SelectionCriteria>
                </StationActivityRQ>
        Return objSabreCommandLLSRQ
    End Function
    Public Shared Function SalesSummaryRQ(dteReportDate As Date _
                        , strStationNbr As String _
                        , strEpr As String) As XElement
        Dim strReportDate As String = Format(dteReportDate, "yyyy-MM-dd")
        Dim objSabreCommandLLSRQ As XElement =
                <SalesSummaryRQ version="1.0.0" xmlns="http://www.sabre.com/ns/Ticketing/AsrServices/1.0">
                    <Header/>
                    <SelectionCriteria>
                        <TicketingProvider>VN</TicketingProvider>
                        <StationNumber><%= strStationNbr %></StationNumber>
                        <EmployeeNumber><%= strEpr %></EmployeeNumber>
                        <ReportDate><%= strReportDate %></ReportDate>
                    </SelectionCriteria>
                </SalesSummaryRQ>
        Return objSabreCommandLLSRQ
    End Function
    Public Shared Function TaxRQ(dteReportDate As Date _
                        , strStationNbr As String _
                        , strEpr As String) As XElement
        Dim strReportDate As String = Format(dteReportDate, "yyyy-MM-dd")
        Dim objSabreCommandLLSRQ As XElement =
                <TaxRQ version="1.0.0" xmlns="http://www.sabre.com/ns/Ticketing/AsrServices/1.0">
                    <Header/>
                    <SelectionCriteria>
                        <TicketingProvider>VN</TicketingProvider>
                        <StationNumber><%= strStationNbr %></StationNumber>
                        <EmployeeNumber><%= strEpr %></EmployeeNumber>
                        <ReportDate><%= strReportDate %></ReportDate>
                    </SelectionCriteria>
                </TaxRQ>
        Return objSabreCommandLLSRQ
    End Function
    Public Shared Function CreditCardRQ(dteReportDate As Date _
                        , strStationNbr As String _
                        , strEpr As String) As XElement
        Dim strReportDate As String = Format(dteReportDate, "yyyy-MM-dd")
        Dim objSabreCommandLLSRQ As XElement =
                <CreditCardRQ version="1.0.0" xmlns="http://www.sabre.com/ns/Ticketing/AsrServices/1.0">
                    <Header/>
                    <SelectionCriteria>
                        <TicketingProvider>VN</TicketingProvider>
                        <StationNumber><%= strStationNbr %></StationNumber>
                        <EmployeeNumber><%= strEpr %></EmployeeNumber>
                        <ReportDate><%= strReportDate %></ReportDate>
                    </SelectionCriteria>
                </CreditCardRQ>
        Return objSabreCommandLLSRQ
    End Function
    Public Shared Function AgentListRQ(dteReportDate As Date _
                        , strStationNbr As String) As XElement
        Dim strReportDate As String = Format(dteReportDate, "yyyy-MM-dd")
        Dim objSabreCommandLLSRQ As XElement =
                <AgentListRQ version="1.0.0" xmlns="http://www.sabre.com/ns/Ticketing/AsrServices/1.0">
                    <Header/>
                    <SelectionCriteria>
                        <TicketingProvider>VN</TicketingProvider>
                        <StationNumber><%= strStationNbr %></StationNumber>
                        <ReportDate><%= strReportDate %></ReportDate>
                    </SelectionCriteria>
                </AgentListRQ>
        Return objSabreCommandLLSRQ
    End Function
    Public Shared Function StationSummaryRQ(dteReportDate As Date _
                        , strStationNbr As String, Optional blnClose As Boolean = False) As XElement
        Dim strReportDate As String = Format(dteReportDate, "yyyy-MM-dd")
        Dim strAction As String
        If blnClose Then
            strAction = AccountingReportOperation.close
        Else
            strAction = AccountingReportOperation.display
        End If
        Dim objSabreCommandLLSRQ As XElement =
                <StationSummaryRQ version="1.0.0" xmlns="http://www.sabre.com/ns/Ticketing/AsrServices/1.0">
                    <Header/>
                    <SelectionCriteria>
                        <ReportOperation><%= strAction %></ReportOperation>
                        <TicketingProvider>VN</TicketingProvider>
                        <StationNumber><%= strStationNbr %></StationNumber>
                        <ReportDate><%= strReportDate %></ReportDate>
                    </SelectionCriteria>
                </StationSummaryRQ>
        Return objSabreCommandLLSRQ
    End Function
    Public Shared Function StationManagerRQ(dteReportDate As Date _
                        , strStationNbr As String) As XElement
        Dim strReportDate As String = Format(dteReportDate, "yyyy-MM-dd")
        Dim objSabreCommandLLSRQ As XElement =
                <StationManagerRQ version="1.0.0" xmlns="http://www.sabre.com/ns/Ticketing/AsrServices/1.0">
                    <Header/>
                    <SelectionCriteria>
                        <TicketingProvider>VN</TicketingProvider>
                        <StationNumber><%= strStationNbr %></StationNumber>
                        <ReportDate><%= strReportDate %></ReportDate>
                    </SelectionCriteria>
                </StationManagerRQ>
        Return objSabreCommandLLSRQ
    End Function
    Public Shared Function AccountingRQ(dteReportDate As Date _
                        , strStationNbr As String _
                        , strEpr As String, Optional blnClose As Boolean = False) As XElement
        Dim strReportDate As String = Format(dteReportDate, "yyyy-MM-dd")
        Dim strAction As String = AccountingReportOperation.display
        If blnClose Then
            strAction = AccountingReportOperation.close
        End If
        Dim objSabreCommandLLSRQ As XElement =
                <AccountingRQ version="1.0.0" xmlns="http://www.sabre.com/ns/Ticketing/AsrServices/1.0">
                    <Header/>
                    <SelectionCriteria>
                        <AccountingReportOperation><%= strAction %></AccountingReportOperation>
                        <TicketingProvider>VN</TicketingProvider>
                        <StationNumber><%= strStationNbr %></StationNumber>
                        <EmployeeNumber><%= strEpr %></EmployeeNumber>
                        <ReportDate><%= strReportDate %></ReportDate>
                    </SelectionCriteria>
                </AccountingRQ>
        Return objSabreCommandLLSRQ
    End Function
    Public Shared Function SabreCommandLLSRQ(strCommand As String) As XElement
        Dim objSabreCommandLLSRQ As XElement =
                <ns:SabreCommandLLSRQ xmlns="http://webservices.sabre.com/sabreXML/2003/07" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" TimeStamp="2011-03-09T15:00:47-06:00" Version="1.8.1">
                    <ns:Request Output="SCREEN" CDATA="true">
                        <ns:HostCommand><%= strCommand %></ns:HostCommand>
                    </ns:Request>
                </ns:SabreCommandLLSRQ>
        Return objSabreCommandLLSRQ
    End Function
    Private Shared Function XmlHeader(security As XElement _
                                      , strAction As String, Optional strConversationId As String = "") As XElement
        Dim partyIdsFrom As String() = {
            "WebServiceClient"
        }

        'Dim partyIdFrom As String() = {
        '    "WebServiceClient",
        '    "WebServiceClient2"
        '} ' vi du nay cho params la array

        Dim header As XElement =
                <soapenv:Header>
                    <%= security %>
                    <mes:MessageHeader mes:id="?" mes:version="?">
                        <mes:From>
                            <%=
                                From partyId In partyIdsFrom
                                Select
                                    <mes:PartyId>
                                        <%= partyId %>
                                    </mes:PartyId>
                            %>
                        </mes:From>
                        <mes:To>
                            <mes:PartyId>WebServiceSupplier</mes:PartyId>
                        </mes:To><mes:CPAId>VN</mes:CPAId>
                        <mes:ConversationId><%= strConversationId %>></mes:ConversationId>
                        <mes:Service>?</mes:Service>
                        <mes:Action><%= strAction %></mes:Action>
                        <mes:MessageData>
                            <mes:MessageId>mid:20001209-133003-2333@clientofsabre.com1</mes:MessageId>
                            <mes:Timestamp>2014-05-26</mes:Timestamp>
                        </mes:MessageData>
                    </mes:MessageHeader>
                </soapenv:Header>

        Return header
    End Function
    Private Shared Function XmlHeader2(security As XElement _
                                      , strAction As String, Optional strConversationId As String = "") As XElement
        Dim partyIdsFrom As String() = {
            "WebServiceClient"
        }

        'Dim partyIdFrom As String() = {
        '    "WebServiceClient",
        '    "WebServiceClient2"
        '} ' vi du nay cho params la array

        Dim header As XElement =
                <MessageHeader d2p1:version="2.12.0" xmlns:d2p1="http://www.ebxml.org/namespaces/messageHeader">
                    <%= security %>
                    <mes:MessageHeader mes:id="?" mes:version="?">
                        <mes:From>
                            <%=
                                From partyId In partyIdsFrom
                                Select
                                    <mes:PartyId>
                                        <%= partyId %>
                                    </mes:PartyId>
                            %>
                        </mes:From>
                        <mes:To>
                            <mes:PartyId>WebServiceSupplier</mes:PartyId>
                        </mes:To><mes:CPAId>VN</mes:CPAId>
                        <mes:ConversationId><%= strConversationId %>></mes:ConversationId>
                        <mes:Service>?</mes:Service>
                        <mes:Action><%= strAction %></mes:Action>
                        <mes:MessageData>
                            <mes:MessageId>mid:20001209-133003-2333@clientofsabre.com1</mes:MessageId>
                            <mes:Timestamp>2014-05-26</mes:Timestamp>
                        </mes:MessageData>
                    </mes:MessageHeader>
                </MessageHeader>

        Return header
    End Function
    Private Shared Function XmlHeaderWithAuth(strUserName As String, strPass As String) As XElement
        Dim security As XElement =
            <sec:Security>
                <sec:UsernameToken>
                    <sec:Username><%= strUserName %></sec:Username>
                    <sec:Password><%= strPass %></sec:Password>
                    <sec:NewPassword>?</sec:NewPassword>
                    <Organization>ABY</Organization>
                    <Domain>VN</Domain>
                </sec:UsernameToken>
            </sec:Security>

        Return XmlHeader(security, "SessionCreateRQ")
    End Function

    Private Shared Function XmlHeaderWithToken(mstrSecurityKey As String _
                                , strAction As String, Optional strConversationId As String = "") As XElement
        Dim tokenSec As XElement =
            <sec:Security>
                <sec:BinarySecurityToken>
                    <%= mstrSecurityKey %>
                </sec:BinarySecurityToken>
            </sec:Security>

        Return XmlHeader(tokenSec, strAction, strConversationId)
    End Function
    Private Shared Function XmlHeaderWithToken2(mstrSecurityKey As String _
                                , strAction As String, Optional strConversationId As String = "") As XElement
        Dim tokenSec As XElement =
            <sec:Security>
                <UsernameToken xmlns="http://schemas.xmlsoap.org/ws/2002/12/secext">
                    <Organization xmlns="">VN</Organization>
                    <Domain xmlns="">VN</Domain>
                </UsernameToken>
                <sec:BinarySecurityToken>
                    <%= mstrSecurityKey %>
                </sec:BinarySecurityToken>
            </sec:Security>

        Return XmlHeader2(tokenSec, strAction, strConversationId)
    End Function
    Public Shared Function Connect(strUserName As String, strPass As String) As String
        Dim header As XElement = XmlHeaderWithAuth(strUserName, strPass)

        Dim envelop As XElement =
         <soapenv:Envelope>
             <%= header %>
             <soapenv:Body>
                 <ns:SessionCreateRQ returnContextID="?"><ns:POS>
                     <ns:Source PseudoCityCode="?"/></ns:POS>
                 </ns:SessionCreateRQ>
             </soapenv:Body>
         </soapenv:Envelope>

        Return envelop.ToString()
    End Function

    Public Shared Function SessionCloseRQ(token As String) As String
        Dim header As XElement = XmlHeaderWithToken(token, "SessionCloseRQ")

        Dim envelop As XElement =
         <soapenv:Envelope>
             <%= header %>
             '<soapenv:Body>
             '    <ns:SessionCreateRQ returnContextID="?"><ns:POS>
             '        <ns:Source PseudoCityCode="?"/></ns:POS>
             '    </ns:SessionCreateRQ>
             '</soapenv:Body>
         </soapenv:Envelope>

        Return envelop.ToString()
    End Function
    Public Shared Function CreateSoap(strToken As String _
                    , xeBody As XElement, strAction As String, Optional strConversationId As String = "") As String
        Dim header As XElement = XmlHeaderWithToken(strToken, strAction, strConversationId)

        Dim envelop As XElement
        envelop =
         <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:sec="http://schemas.xmlsoap.org/ws/2002/12/secext" xmlns:mes="http://www.ebxml.org/namespaces/messageHeader" xmlns:ns="http://webservices.sabre.com/sabreXML/2011/10">
             <%= header %>
             <soapenv:Body>
                 <%= xeBody %>
             </soapenv:Body>
         </soapenv:Envelope>

        Return envelop.ToString()
    End Function
    Public Shared Function CreateSoap2(strToken As String _
                    , xeBody As XElement, strAction As String, Optional strConversationId As String = "") As String
        Dim header As XElement = XmlHeaderWithToken2(strToken, strAction, strConversationId)

        Dim envelop As XElement
        envelop =
         <soapenv:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
             <%= header %>
             <soapenv:Body>
                 <%= xeBody %>
             </soapenv:Body>
         </soapenv:Envelope>

        Return envelop.ToString()
    End Function
    Public Shared Function CreateSoapDesignatePrinter(strToken As String, xeBody As XElement) As String
        Dim header As XElement = XmlHeaderWithToken(strToken, "DesignatePrinterLLSRQ")

        Dim envelop As XElement =
                             <DesignatePrinterRQRequest xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                 <MessageHeader>
                                     <From xmlns="http://www.ebxml.org/namespaces/messageHeader">
                                         <PartyId d4p1:type="?" xmlns:d4p1="http://www.ebxml.org/namespaces/messageHeader">WebServiceClient</PartyId>
                                     </From>
                                     <To xmlns="http://www.ebxml.org/namespaces/messageHeader">
                                         <PartyId d4p1:type="?" xmlns:d4p1="http://www.ebxml.org/namespaces/messageHeader">WebServiceSupplier</PartyId>
                                     </To>
                                     <CPAId xmlns="http://www.ebxml.org/namespaces/messageHeader">VN</CPAId>
                                     <ConversationId xmlns="http://www.ebxml.org/namespaces/messageHeader"></ConversationId>
                                     <Action xmlns="http://www.ebxml.org/namespaces/messageHeader">DesignatePrinterLLSRQ</Action>
                                 </MessageHeader>
                                 <Security>
                                     <UsernameToken xmlns="http://schemas.xmlsoap.org/ws/2002/12/secext">
                                         <Organization xmlns="">AZB</Organization>
                                         <Domain xmlns="">VN</Domain>
                                     </UsernameToken>
                                     <BinarySecurityToken xmlns="http://schemas.xmlsoap.org/ws/2002/12/secext"><%= strToken %>></BinarySecurityToken>
                                 </Security>
                                 <DesignatePrinterRQ xmlns="http://webservices.sabre.com/sabreXML/2011/10" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" ReturnHostCommand="false" TimeStamp="2014-10-09T15:00:00-06:00" Version="2.0.1">
                                     <Printers xmlns="http://webservices.sabre.com/sabreXML/2011/10">
                                         <Ticket CountryCode="GF" LNIATA="AB31D2" Undesignate="false"/>
                                     </Printers>
                                 </DesignatePrinterRQ>
                             </DesignatePrinterRQRequest>

        Return envelop.ToString()
    End Function
    Public Shared Function GetMarketingTextRQ() As XElement

        Dim objXElement As XElement =
                <GetMarketingTextRQ xmlns="http://stl.sabre.com/Merchandising/v1" version="1.0.0">
                    <BrandsMarketingTextRequest>
                        <RequestSource clientID="SHP" requestingCarrierGDS="VN" geoLocation="PAR"/>
                        <MarketingTextCriteria language="EN" carrier="VN"/>
                    </BrandsMarketingTextRequest>
                </GetMarketingTextRQ>
        Return objXElement
    End Function
    Public Shared Function DesignatePrinterRQ(strPrinterAddr As String) As XElement

        Dim objXElement As XElement =
                <DesignatePrinterRQ xmlns="http://webservices.sabre.com/sabreXML/2011/10" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" Version="2.0.1">
                    <Printers>
                        <Ticket CountryCode="GF" LNIATA=<%= strPrinterAddr %>/>
                    </Printers>
                </DesignatePrinterRQ>

        Return objXElement
    End Function
    Public Shared Function VCR_DisplayLLSRQ(strTktNbr As String) As XElement

        Dim objXElement As XElement =
                <VCR_DisplayRQ Version="2.2.2">
                    <SearchOptions xmlns="http://webservices.sabre.com/sabreXML/2011/10">
                        <Ticketing eTicketNumber=<%= strTktNbr %>/>
                    </SearchOptions>
                </VCR_DisplayRQ>

        Return objXElement
    End Function
    Public Shared Function GetReservationRQ(Optional strRloc As String = "" _
                                            , Optional strSubjectArea As String = "ACTIVE" _
                                            , Optional strViewname As String = "Full") As XElement
        Dim strRequestType As String
        If strRloc = "" Then
            strRequestType = PnrRequestType.Stateful
        Else
            strRequestType = PnrRequestType.Stateless
        End If
        Dim objXElement As XElement =
        <GetReservationRQ Version="1.19.0">
            <Locator xmlns="http://webservices.sabre.com/pnrbuilder/v1_19"><%= strRloc %></Locator>
            <RequestType xmlns="http://webservices.sabre.com/pnrbuilder/v1_19"><%= strRequestType %></RequestType>
            <ReturnOptions PriceQuoteServiceVersion="3.2.0" xmlns="http://webservices.sabre.com/pnrbuilder/v1_19">
                <SubjectAreas>
                    <SubjectArea><%= strSubjectArea %></SubjectArea>
                </SubjectAreas>
                <ViewName><%= strViewname %></ViewName>
            </ReturnOptions>
            <POS xmlns="http://webservices.sabre.com/pnrbuilder/v1_19">
                <Source/>
            </POS>
        </GetReservationRQ>
        Return objXElement
    End Function

End Class
