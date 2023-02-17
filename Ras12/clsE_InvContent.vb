Imports System.Xml
Public Class clsE_InvContent
    Private mdteCreationDate As Date
    Private mdteIssueDate As Date
    Private mstrSrv As String
    Private mstrMauSo As String
    Private mstrKyHieu As String
    Private mintInvoiceNo As Integer
    Private mstrFOP As String
    Private mstrTvcFullName As String
    Private mstrTvcTaxCode As String
    Private mintCustId As Integer
    Private mstrCusCode As String
    Private mstrCustFullName As String
    Private mstrCustTaxCode As String
    Private mstrCustAddress As String
    Private mstrTotalInvInLetters As String
    Private mdecTotalInv As Decimal
    Private mdecTotalVat As Decimal
    Private mdecTotalVatable As Decimal
    Private mlstProducts As New List(Of clsProduct)
    Private mstrExtra As String
    Private mstrExtras As String
    Private mstrExtra1 As String
    Private mstrExtra2 As String
    Private mstrEmailDeliver As String
    Private mdecDiscount_Rate As Decimal
    Private mdecDiscount_Amount As Decimal
    Private mstrBuyer As String
    Private mdecGrossValue As Decimal
    Private mdecGrossValue0 As Decimal
    Private mdecGrossValue5 As Decimal
    Private mdecGrossValue10 As Decimal
    Private mdecGrossValueNonTax As Decimal
    Private mdecVatAmount0 As Decimal
    Private mdecVatAmount5 As Decimal
    Private mdecVatAmount10 As Decimal
    Private mdevGrossValueNonTax As Decimal
    Private mdecVAT_Rate As Decimal
    Private mstrIsReplace As String
    Private mstrIsAdjust As String
    Private mstrComId As String
    Private mstrPBan As String
    Private mstrTHDon As String
    Private mstrKHMSHDon As String
    Private mstrCur As String
    Private mintRoe As Integer
    Private mstrFkey As String
    Private mstrMaCoQuanThue As String
    Private mstrLinkedTCHDon As String
    Private mstrLinkedLHDCLQuan As String
    Private mstrLinkedMauSo As String
    Private mstrLinkedKyHieu As String
    Private mintLinkedInvNbr As Integer
    Private mdteLinkedInvDOI As Date
    Private mstrLinkedGhiChu As String

    Public Function ParseXmlTT78(strXml As String) As Boolean
        Dim objXmlDoc As New XmlDocument
        Dim lstProducts As XmlNodeList
        mintCustId = 0
        objXmlDoc.LoadXml(strXml)
        Dim objInvNodeDetails As XmlNodeList = objXmlDoc.GetElementsByTagName("HDon")
        For Each objSubNode As XmlNode In objInvNodeDetails(0).ChildNodes
            Select Case objSubNode.Name
                Case "DLHDon"
                    For Each objSub2 As XmlNode In objSubNode.ChildNodes
                        Select Case objSub2.Name
                            Case "TTChung"
                                For Each objSub3 As XmlNode In objSub2.ChildNodes
                                    Select Case objSub3.Name
                                        Case "PBan"
                                            mstrPBan = objSub3.InnerText
                                        Case "THDon"
                                            mstrTHDon = objSub3.InnerText
                                        Case "KHMSHDon"
                                            mstrMauSo = objSub3.InnerText
                                        Case "KHHDon"
                                            mstrKyHieu = objSub3.InnerText
                                        Case "SHDon"
                                            mintInvoiceNo = objSub3.InnerText
                                        Case "NLap"
                                            mdteCreationDate = objSub3.InnerText
                                        Case "DVTTe"
                                            mstrCur = objSub3.InnerText
                                        Case "TGia"
                                            mintRoe = objSub3.InnerText
                                        Case "HTTToan"
                                            mstrFOP = objSub3.InnerText
                                        Case "TTHDLQuan"
                                            For Each objSub4 As XmlNode In objSub3.ChildNodes
                                                Select Case objSub4.Name
                                                    Case "TCHDon"
                                                        mstrLinkedTCHDon = objSub4.InnerText
                                                    Case "LHDCLQuan"
                                                        mstrLinkedLHDCLQuan = objSub4.InnerText
                                                    Case "KHMSHDCLQuan"
                                                        mstrLinkedMauSo = objSub4.InnerText
                                                    Case "KHHDCLQuan"
                                                        mstrLinkedKyHieu = objSub4.InnerText
                                                    Case "SHDCLQuan"
                                                        mintLinkedInvNbr = objSub4.InnerText
                                                    Case "NLHDCLQuan"
                                                        mdteLinkedInvDOI = objSub4.InnerText
                                                    Case "GChu"
                                                        mstrLinkedGhiChu = objSub4.InnerText
                                                End Select
                                            Next
                                        Case "TTKhac"
                                            For Each objSub4 As XmlNode In objSub3.ChildNodes
                                                Select Case objSub4.ChildNodes(0).InnerText
                                                    Case "Extra1"
                                                        mstrExtra1 = objSub4.ChildNodes(2).InnerText
                                                    Case "Extra2"
                                                        mstrExtra2 = objSub4.ChildNodes(2).InnerText
                                                End Select
                                            Next
                                        Case Else
                                            MsgBox("New Tag in tag TTChung:" & objSub3.Name)
                                    End Select
                                Next
                            Case "NDHDon"
                                For Each objSub3 As XmlNode In objSub2.ChildNodes
                                    Select Case objSub3.Name
                                        Case "NBan"
                                            For Each objSub4 As XmlNode In objSub3.ChildNodes
                                                Select Case objSub4.Name
                                                    Case "Ten"
                                                        mstrTvcFullName = objSub3.InnerText
                                                    Case "MST"
                                                        mstrTvcTaxCode = objSub3.InnerText
                                                    Case "DChi", "SDThoai"
                                                        'BO QUA KO LAY
                                                End Select
                                            Next
                                        Case "NMua"
                                            For Each objSub4 As XmlNode In objSub3.ChildNodes
                                                Select Case objSub4.Name
                                                    Case "Ten"
                                                        mstrCustFullName = objSub4.InnerText
                                                    Case "MKHang"
                                                        If IsNumeric(objSub4.InnerText) Then
                                                            mintCustId = objSub4.InnerText
                                                        End If

                                                    Case "DCTDTu"
                                                        mstrEmailDeliver = objSub4.InnerText
                                                    Case "HVTNMHang"
                                                        mstrBuyer = objSub4.InnerText
                                                    Case "HVTNMHang"
                                                        mstrBuyer = objSub4.InnerText
                                                    Case "MST"
                                                        mstrCustTaxCode = objSub4.InnerText
                                                    Case "DChi"
                                                        mstrCustAddress = objSub4.InnerText
                                                End Select
                                            Next
                                        Case "DSHHDVu"
                                            For Each objSub4 As XmlNode In objSub3.ChildNodes
                                                mlstProducts.Add(ParseProductTT78(objSub4))
                                            Next

                                        Case "TToan"
                                            For Each objSub4 As XmlNode In objSub3.ChildNodes
                                                Select Case objSub4.Name
                                                    Case "THTTLTSuat"
                                                        For Each objSub5 As XmlNode In objSub4.ChildNodes
                                                            'dang lam do phan nay
                                                        Next
                                                    Case "TgTCThue"
                                                        mdecTotalVatable = objSub4.InnerText
                                                    Case "TgTThue"
                                                        mdecTotalVat = objSub4.InnerText
                                                    Case "TgTTTBSo"
                                                        mdecTotalInv = objSub4.InnerText
                                                    Case "TgTTTBChu"
                                                        mstrTotalInvInLetters = objSub4.InnerText
                                                End Select
                                            Next
                                    End Select
                                Next
                        End Select
                    Next
                Case "DSCKS"
                    'bo qua phan nay
                Case "MCCQT"
                    mstrMaCoQuanThue = objSubNode.InnerText
                Case "Fkey"
                    mstrFkey = objSubNode.InnerText
                Case Else
                    MsgBox("New Tag name in Invoice Content:" & objSubNode.Name)
            End Select
        Next
        'For Each objSubNode As XmlNode In objInvNodeDetails(0).ChildNodes
        '    Select Case objSubNode.Name
        '        Case "ArisingDate"
        '            mdteCreationDate = objSubNode.InnerText
        '        Case "SignDate"
        '            mdteIssueDate = objSubNode.InnerText
        '        Case "InvoiceName"
        '            Select Case objSubNode.InnerText
        '                Case "Hóa đơn giá trị gia tăng"
        '                    mstrSrv = "S"
        '                Case "Hoàn vé"
        '                    mstrSrv = "R"
        '            End Select
        '        Case "InvoicePattern"
        '            mstrMauSo = objSubNode.InnerText
        '        Case "SerialNo"
        '            mstrKyHieu = objSubNode.InnerText
        '        Case "InvoiceNo"
        '            mintInvoiceNo = objSubNode.InnerText
        '        Case "Kind_of_Payment"
        '            If objSubNode.InnerText.Contains(vbTab) Then
        '                mstrFOP = Replace(objSubNode.InnerText, vbTab, "").Replace(vbLf, "")
        '            End If
        '        Case "ComName"
        '            mstrTvcFullName = objSubNode.InnerText
        '        Case "ComTaxCode"
        '            mstrTvcTaxCode = objSubNode.InnerText
        '        Case "CusCode"
        '            mstrCusCode = objSubNode.InnerText
        '            If IsNumeric(mstrCusCode) AndAlso mstrCusCode.Length < 10 Then
        '                mintCustId = mstrCusCode
        '            End If
        '        Case "CusName"
        '            mstrCustFullName = objSubNode.InnerText
        '        Case "CusTaxCode"
        '            mstrCustTaxCode = objSubNode.InnerText
        '        Case "CusAddress"
        '            mstrCustAddress = objSubNode.InnerText
        '        Case "Total"
        '            mdecTotalInv = objSubNode.InnerText
        '        Case "VAT_Amount"
        '            mdecTotalVat = objSubNode.InnerText
        '        Case "Amount"
        '            mdecTotalVatable = objSubNode.InnerText
        '        Case "ComID"
        '            mstrComId = objSubNode.InnerText
        '        Case "Extra"
        '            mstrExtra = objSubNode.InnerText
        '        Case "Extras"
        '            mstrExtras = objSubNode.InnerText
        '        Case "Extra1"
        '            mstrExtra1 = objSubNode.InnerText
        '        Case "Extra2"
        '            mstrExtra2 = objSubNode.InnerText
        '        Case "Extra3", "Extra4", "Extra5", "Extra6", "Extra7", "Extra8", "Extra9", "Extra10", "Extra11", "Extra12"
        '            'bo qua
        '        Case "EmailDeliver"
        '            mstrEmailDeliver = objSubNode.InnerText
        '        Case "Buyer"
        '            mstrBuyer = objSubNode.InnerText
        '        Case "GrossValue"
        '            mdecGrossValue = objSubNode.InnerText
        '        Case "GrossValue0"
        '            mdecGrossValue0 = objSubNode.InnerText
        '        Case "GrossValue5"
        '            mdecGrossValue5 = objSubNode.InnerText
        '        Case "GrossValue10"
        '            mdecGrossValue10 = objSubNode.InnerText
        '        Case "GrossValueNonTax"
        '            mdecGrossValueNonTax = objSubNode.InnerText
        '        Case "VatAmount0"
        '            mdecVatAmount0 = objSubNode.InnerText
        '        Case "VatAmount5"
        '            mdecVatAmount0 = objSubNode.InnerText
        '        Case "VatAmount10"
        '            mdecVatAmount0 = objSubNode.InnerText
        '        Case "VAT_Rate"
        '            mdecVAT_Rate = objSubNode.InnerText
        '        Case "Discount_Rate"
        '            mdecDiscount_Rate = objSubNode.InnerText
        '        Case "Discount_Amount"
        '            mdecDiscount_Amount = objSubNode.InnerText
        '        Case "isReplace"
        '            mstrIsReplace = objSubNode.InnerText
        '        Case "isAdjust"
        '            mstrIsAdjust = objSubNode.InnerText

        '        Case "ResourceCode", "ComAddress", "ComPhone", "ComBankNo", "ComBankName", "CusPhone" _
        '            , "CusBankName", "CusBankNo", "Amount_words", "SMSDeliver", "KindOfService" _
        '            , "CurrencyUnit", "ExchangeRate", "ConvertedAmount", "Products"
        '            'bo qua

        '        Case Else
        '            MsgBox("Please report Khanhnm new tag name:" & objSubNode.Name)
        '    End Select

        'Next
        'mdteCreationDate = objXmlDoc.GetElementsByTagName("ArisingDate")(0).InnerText
        '    mdteIssueDate = objXmlDoc.GetElementsByTagName("SignDate")(0).InnerText
        'Select Case objXmlDoc.GetElementsByTagName("InvoiceName")(0).InnerText
        '    Case "Hóa đơn giá trị gia tăng"
        '        mstrSrv = "S"
        '    Case "Hoàn vé"
        '        mstrSrv = "R"
        'End Select
        'mstrMauSo = objXmlDoc.GetElementsByTagName("InvoicePattern")(0).InnerText
        'mstrKyHieu = objXmlDoc.GetElementsByTagName("SerialNo")(0).InnerText
        'mintInvoiceNo = objXmlDoc.GetElementsByTagName("InvoiceNo")(0).InnerText
        'mstrFOP = objXmlDoc.GetElementsByTagName("Kind_of_Payment")(0).InnerText
        'mstrTvcFullName = objXmlDoc.GetElementsByTagName("ComName")(0).InnerText
        'mstrTvcTaxCode = objXmlDoc.GetElementsByTagName("ComTaxCode")(0).InnerText
        'mintCustId = objXmlDoc.GetElementsByTagName("CusCode")(0).InnerText
        'mstrCustFullName = objXmlDoc.GetElementsByTagName("CusName")(0).InnerText
        'mstrCustTaxCode = objXmlDoc.GetElementsByTagName("CusTaxCode")(0).InnerText
        'mstrCustAddress = objXmlDoc.GetElementsByTagName("CusAddress")(0).InnerText
        'mdecTotalInv = objXmlDoc.GetElementsByTagName("Total")(0).InnerText
        'mdecTotalVat = objXmlDoc.GetElementsByTagName("VAT_Amount")(0).InnerText
        'mdecTotalVatable = objXmlDoc.GetElementsByTagName("Amount")(0).InnerText

        lstProducts = objXmlDoc.GetElementsByTagName("Product")
        For Each objNode As XmlNode In lstProducts
            mlstProducts.Add(ParseProduct(objNode))
        Next
        Return True
    End Function
    Public Function ParseXml(strXml As String) As Boolean
        Dim objXmlDoc As New XmlDocument
        Dim lstProducts As XmlNodeList
        objXmlDoc.LoadXml(strXml)
        Dim objInvNodeDetails As XmlNodeList = objXmlDoc.GetElementsByTagName("Content")

        For Each objSubNode As XmlNode In objInvNodeDetails(0).ChildNodes
            Select Case objSubNode.Name
                Case "ArisingDate"
                    mdteCreationDate = objSubNode.InnerText
                Case "SignDate"
                    mdteIssueDate = objSubNode.InnerText
                Case "InvoiceName"
                    Select Case objSubNode.InnerText
                        Case "Hóa đơn giá trị gia tăng"
                            mstrSrv = "S"
                        Case "Hoàn vé"
                            mstrSrv = "R"
                    End Select
                Case "InvoicePattern"
                    mstrMauSo = objSubNode.InnerText
                Case "SerialNo"
                    mstrKyHieu = objSubNode.InnerText
                Case "InvoiceNo"
                    mintInvoiceNo = objSubNode.InnerText
                Case "Kind_of_Payment"
                    If objSubNode.InnerText.Contains(vbTab) Then
                        mstrFOP = Replace(objSubNode.InnerText, vbTab, "").Replace(vbLf, "")
                    End If
                Case "ComName"
                    mstrTvcFullName = objSubNode.InnerText
                Case "ComTaxCode"
                    mstrTvcTaxCode = objSubNode.InnerText
                Case "CusCode"
                    mstrCusCode = objSubNode.InnerText
                    If IsNumeric(mstrCusCode) AndAlso mstrCusCode.Length < 10 Then
                        mintCustId = mstrCusCode
                    End If
                Case "CusName"
                    mstrCustFullName = objSubNode.InnerText
                Case "CusTaxCode"
                    mstrCustTaxCode = objSubNode.InnerText
                Case "CusAddress"
                    mstrCustAddress = objSubNode.InnerText
                Case "Total"
                    mdecTotalInv = objSubNode.InnerText
                Case "VAT_Amount"
                    mdecTotalVat = objSubNode.InnerText
                Case "Amount"
                    mdecTotalVatable = objSubNode.InnerText
                Case "ComID"
                    mstrComId = objSubNode.InnerText
                Case "Extra"
                    mstrExtra = objSubNode.InnerText
                Case "Extras"
                    mstrExtras = objSubNode.InnerText
                Case "Extra1"
                    mstrExtra1 = objSubNode.InnerText
                Case "Extra2"
                    mstrExtra2 = objSubNode.InnerText
                Case "Extra3", "Extra4", "Extra5", "Extra6", "Extra7", "Extra8", "Extra9", "Extra10", "Extra11", "Extra12"
                    'bo qua
                Case "EmailDeliver"
                    mstrEmailDeliver = objSubNode.InnerText
                Case "Buyer"
                    mstrBuyer = objSubNode.InnerText
                Case "GrossValue"
                    mdecGrossValue = objSubNode.InnerText
                Case "GrossValue0"
                    mdecGrossValue0 = objSubNode.InnerText
                Case "GrossValue5"
                    mdecGrossValue5 = objSubNode.InnerText
                Case "GrossValue10"
                    mdecGrossValue10 = objSubNode.InnerText
                Case "GrossValueNonTax"
                    mdecGrossValueNonTax = objSubNode.InnerText
                Case "VatAmount0"
                    mdecVatAmount0 = objSubNode.InnerText
                Case "VatAmount5"
                    mdecVatAmount0 = objSubNode.InnerText
                Case "VatAmount10"
                    mdecVatAmount0 = objSubNode.InnerText
                Case "VAT_Rate"
                    mdecVAT_Rate = objSubNode.InnerText
                Case "Discount_Rate"
                    mdecDiscount_Rate = objSubNode.InnerText
                Case "Discount_Amount"
                    mdecDiscount_Amount = objSubNode.InnerText
                Case "isReplace"
                    mstrIsReplace = objSubNode.InnerText
                Case "isAdjust"
                    mstrIsAdjust = objSubNode.InnerText

                Case "ResourceCode", "ComAddress", "ComPhone", "ComBankNo", "ComBankName", "CusPhone" _
                    , "CusBankName", "CusBankNo", "Amount_words", "SMSDeliver", "KindOfService" _
                    , "CurrencyUnit", "ExchangeRate", "ConvertedAmount", "Products"
                    'bo qua

                Case Else
                    MsgBox("Please report Khanhnm new tag name:" & objSubNode.Name)
            End Select

        Next
        'mdteCreationDate = objXmlDoc.GetElementsByTagName("ArisingDate")(0).InnerText
        '    mdteIssueDate = objXmlDoc.GetElementsByTagName("SignDate")(0).InnerText
        'Select Case objXmlDoc.GetElementsByTagName("InvoiceName")(0).InnerText
        '    Case "Hóa đơn giá trị gia tăng"
        '        mstrSrv = "S"
        '    Case "Hoàn vé"
        '        mstrSrv = "R"
        'End Select
        'mstrMauSo = objXmlDoc.GetElementsByTagName("InvoicePattern")(0).InnerText
        'mstrKyHieu = objXmlDoc.GetElementsByTagName("SerialNo")(0).InnerText
        'mintInvoiceNo = objXmlDoc.GetElementsByTagName("InvoiceNo")(0).InnerText
        'mstrFOP = objXmlDoc.GetElementsByTagName("Kind_of_Payment")(0).InnerText
        'mstrTvcFullName = objXmlDoc.GetElementsByTagName("ComName")(0).InnerText
        'mstrTvcTaxCode = objXmlDoc.GetElementsByTagName("ComTaxCode")(0).InnerText
        'mintCustId = objXmlDoc.GetElementsByTagName("CusCode")(0).InnerText
        'mstrCustFullName = objXmlDoc.GetElementsByTagName("CusName")(0).InnerText
        'mstrCustTaxCode = objXmlDoc.GetElementsByTagName("CusTaxCode")(0).InnerText
        'mstrCustAddress = objXmlDoc.GetElementsByTagName("CusAddress")(0).InnerText
        'mdecTotalInv = objXmlDoc.GetElementsByTagName("Total")(0).InnerText
        'mdecTotalVat = objXmlDoc.GetElementsByTagName("VAT_Amount")(0).InnerText
        'mdecTotalVatable = objXmlDoc.GetElementsByTagName("Amount")(0).InnerText

        lstProducts = objXmlDoc.GetElementsByTagName("Product")
        For Each objNode As XmlNode In lstProducts
            mlstProducts.Add(ParseProduct(objNode))
        Next
        Return True
    End Function
    Public Function ParseProduct(objNode As XmlNode) As clsProduct
        Dim objProduct As New clsProduct
        For Each objSubNode As XmlNode In objNode.ChildNodes
            Select Case objSubNode.Name
                Case "Code"
                    objProduct.ProductCode = objSubNode.InnerText
                Case "Remark"
                    objProduct.Seq = objSubNode.InnerText
                Case "Total"
                    objProduct.TotalPrice = objSubNode.InnerText
                Case "ProdName"
                    objProduct.ProdName = objSubNode.InnerText
                Case "ProdUnit"
                    objProduct.ProdUnit = objSubNode.InnerText
                Case "ProdQuantity"
                    objProduct.ProdQuantity = objSubNode.InnerText
                Case "ProdPrice"
                    objProduct.ProdPrice = objSubNode.InnerText
                Case "Discount"
                    objProduct.DiscountRate = objSubNode.InnerText
                Case "DiscountAmount"
                    objProduct.DiscountAmount = objSubNode.InnerText
                Case "VATRate"
                    objProduct.VatRate = objSubNode.InnerText
                Case "VATAmount"
                    objProduct.VatAmount = objSubNode.InnerText
                Case "Amount"
                    objProduct.Amount = objSubNode.InnerText
                Case "Extra1"
                    objProduct.Extra1 = objSubNode.InnerText
                Case "Extra2"
                    objProduct.Extra2 = objSubNode.InnerText
                Case "ProdNo"
                    objProduct.ProdQuantity = objSubNode.InnerText
                Case Else
                    MsgBox("Please report Khanhnm. New tag name:" & objSubNode.Name)
            End Select
        Next
        'objProduct.ProductCode = objNode.SelectSingleNode("Code").InnerText
        'objProduct.Seq = objNode.SelectSingleNode("Remark").InnerText
        'objProduct.TotalPrice = objNode.SelectSingleNode("Total").InnerText
        'objProduct.ProdName = objNode.SelectSingleNode("ProdName").InnerText
        'objProduct.ProdUnit = objNode.SelectSingleNode("ProdUnit").InnerText
        'objProduct.ProdQuantity = objNode.SelectSingleNode("ProdQuantity").InnerText
        'objProduct.ProdPrice = objNode.SelectSingleNode("ProdPrice").InnerText
        'objProduct.DiscountRate = objNode.SelectSingleNode("Discount").InnerText
        'objProduct.DiscountAmount = objNode.SelectSingleNode("DiscountAmount").InnerText
        'objProduct.VatRate = objNode.SelectSingleNode("VATRate").InnerText
        'objProduct.VatAmount = objNode.SelectSingleNode("VATAmount").InnerText
        'objProduct.Amount = objNode.SelectSingleNode("Amount").InnerText
        'mlstProducts.Add(objProduct)
        Return objProduct
    End Function
    Public Function ParseProductTT78(objNode As XmlNode) As clsProduct
        Dim objProduct As New clsProduct
        For Each objSubNode As XmlNode In objNode.ChildNodes
            Select Case objSubNode.Name
                Case "TChat"
                    objProduct.TChat = objSubNode.InnerText
                Case "STT"
                    objProduct.ProdNo = objSubNode.InnerText
                Case "THHDVu"
                    objProduct.ProdName = objSubNode.InnerText
                Case "MHHDVu"
                    objProduct.ProductCode = objSubNode.InnerText
                Case "SLuong"
                    objProduct.ProdQuantity = objSubNode.InnerText
                Case "DGia"
                    objProduct.ProdPrice = objSubNode.InnerText
                Case "TLCKhau"
                    objProduct.DiscountRate = objSubNode.InnerText
                Case "STCKhau"
                    objProduct.DiscountAmount = objSubNode.InnerText
                Case "TSuat"
                    objProduct.VatRate = ConvertVatPctFromVNPT(objSubNode.InnerText)
                Case "ThTien"
                    objProduct.TotalPrice = objSubNode.InnerText

                Case "TTKhac"
                    For Each objSub2 As XmlNode In objSubNode.ChildNodes
                        If objSub2.Name = "TTin" Then
                            Select Case objSub2.ChildNodes(0).InnerText
                                Case "Amount"
                                    objProduct.GrandTotal = objSub2.ChildNodes(2).InnerText
                                Case "VATAmount"
                                    objProduct.VatAmount = objSub2.ChildNodes(2).InnerText
                                Case "Remark"
                                    objProduct.Remark = objSub2.ChildNodes(2).InnerText
                                Case "Extra1"
                                    objProduct.Extra1 = objSub2.ChildNodes(2).InnerText
                                Case "Extra2"
                                    objProduct.Extra2 = objSub2.ChildNodes(2).InnerText
                            End Select
                        End If
                    Next
                Case "DVTinh"
                    objProduct.ProdUnit = objSubNode.InnerText
                    'Case "ProdQuantity"
                    '    objProduct.ProdQuantity = objSubNode.InnerText


                    'Case "Extra1"
                    '    objProduct.Extra1 = objSubNode.InnerText
                    'Case "Extra2"
                    '    objProduct.Extra2 = objSubNode.InnerText
                    'Case "ProdNo"
                    '    objProduct.ProdQuantity = objSubNode.InnerText
                Case Else
                    MsgBox("Please report Khanhnm. New tag name in tag HHDVu:" & objSubNode.Name)
            End Select
        Next

        Return objProduct
    End Function
    Public Property CreationDate As Date
        Get
            Return mdteCreationDate
        End Get
        Set(value As Date)
            mdteCreationDate = value
        End Set
    End Property
    Public Property IssueDate As Date
        Get
            Return mdteIssueDate
        End Get
        Set(value As Date)
            mdteIssueDate = value
        End Set
    End Property
    Public Property Srv As String
        Get
            Return mstrSrv
        End Get
        Set(value As String)
            mstrSrv = value
        End Set
    End Property
    Public Property MauSo As String
        Get
            Return mstrMauSo
        End Get
        Set(value As String)
            mstrMauSo = value
        End Set
    End Property
    Public Property KyHieu As String
        Get
            Return mstrKyHieu
        End Get
        Set(value As String)
            mstrKyHieu = value
        End Set
    End Property
    Public Property InvoiceNo As Integer
        Get
            Return mintInvoiceNo
        End Get
        Set(value As Integer)
            mintInvoiceNo = value
        End Set
    End Property
    Public Property FOP As String
        Get
            Return mstrFOP
        End Get
        Set(value As String)
            mstrFOP = value
        End Set
    End Property
    Public Property TvcFullName As String
        Get
            Return mstrTvcFullName
        End Get
        Set(value As String)
            mstrTvcFullName = value
        End Set
    End Property
    Public Property TvcTaxCode As String
        Get
            Return mstrTvcTaxCode
        End Get
        Set(value As String)
            mstrTvcTaxCode = value
        End Set
    End Property
    Public Property CustId As Integer
        Get
            Return mintCustId
        End Get
        Set(value As Integer)
            mintCustId = value
        End Set
    End Property
    Public Property CustFullName As String
        Get
            Return mstrCustFullName
        End Get
        Set(value As String)
            mstrCustFullName = value
        End Set
    End Property
    Public Property CustTaxCode As String
        Get
            Return mstrCustTaxCode
        End Get
        Set(value As String)
            mstrCustTaxCode = value
        End Set
    End Property
    Public Property CustAddress As String
        Get
            Return mstrCustAddress
        End Get
        Set(value As String)
            mstrCustAddress = value
        End Set
    End Property
    Public Property TotalInv As Decimal
        Get
            Return mdecTotalInv
        End Get
        Set(value As Decimal)
            mdecTotalInv = value
        End Set
    End Property
    Public Property TotalVat As Decimal
        Get
            Return mdecTotalVat
        End Get
        Set(value As Decimal)
            mdecTotalVat = value
        End Set
    End Property
    Public Property TotalVatable As Decimal
        Get
            Return mdecTotalVatable
        End Get
        Set(value As Decimal)
            mdecTotalVatable = value
        End Set
    End Property
    Public Property Products As List(Of clsProduct)
        Get
            Return mlstProducts
        End Get
        Set(value As List(Of clsProduct))
            mlstProducts = value
        End Set
    End Property

    Public Property Extras As String
        Get
            Return mstrExtras
        End Get
        Set(value As String)
            mstrExtras = value
        End Set
    End Property
    Public Property Extra1 As String
        Get
            Return mstrExtra1
        End Get
        Set(value As String)
            mstrExtra1 = value
        End Set
    End Property
    Public Property Extra2 As String
        Get
            Return mstrExtra2
        End Get
        Set(value As String)
            mstrExtra2 = value
        End Set
    End Property

    Public Property EmailDeliver As String
        Get
            Return mstrEmailDeliver
        End Get
        Set(value As String)
            mstrEmailDeliver = value
        End Set
    End Property

    Public Property Discount_Rate As Decimal
        Get
            Return mdecDiscount_Rate
        End Get
        Set(value As Decimal)
            mdecDiscount_Rate = value
        End Set
    End Property
    Public Property Discount_Amount As Decimal
        Get
            Return mdecDiscount_Amount
        End Get
        Set(value As Decimal)
            mdecDiscount_Amount = value
        End Set
    End Property

    Public Property Extra As String
        Get
            Return mstrExtra
        End Get
        Set(value As String)
            mstrExtra = value
        End Set
    End Property

    Public Property Buyer As String
        Get
            Return mstrBuyer
        End Get
        Set(value As String)
            mstrBuyer = value
        End Set
    End Property
    Public Property GrossValue As Decimal
        Get
            Return mdecGrossValue
        End Get
        Set(value As Decimal)
            mdecGrossValue = value
        End Set
    End Property

    Public Property GrossValue0 As Decimal
        Get
            Return mdecGrossValue0
        End Get
        Set(value As Decimal)
            mdecGrossValue0 = value
        End Set
    End Property
    Public Property GrossValue5 As Decimal
        Get
            Return mdecGrossValue5
        End Get
        Set(value As Decimal)
            mdecGrossValue5 = value
        End Set
    End Property
    Public Property GrossValue10 As Decimal
        Get
            Return mdecGrossValue10
        End Get
        Set(value As Decimal)
            mdecGrossValue10 = value
        End Set
    End Property

    Public Property VatAmount0 As Decimal
        Get
            Return mdecVatAmount0
        End Get
        Set(value As Decimal)
            mdecVatAmount0 = value
        End Set
    End Property
    Public Property VatAmount5 As Decimal
        Get
            Return mdecVatAmount5
        End Get
        Set(value As Decimal)
            mdecVatAmount5 = value
        End Set
    End Property
    Public Property VatAmount10 As Decimal
        Get
            Return mdecVatAmount10
        End Get
        Set(value As Decimal)
            mdecVatAmount10 = value
        End Set
    End Property
    Public Property GrossValueNonTax As Decimal
        Get
            Return mdevGrossValueNonTax
        End Get
        Set(value As Decimal)
            mdevGrossValueNonTax = value
        End Set
    End Property

    Public Property VAT_Rate As Decimal
        Get
            Return mdecVAT_Rate
        End Get
        Set(value As Decimal)
            mdecVAT_Rate = value
        End Set
    End Property
    Public Property CusCode As String
        Get
            Return mstrCusCode
        End Get
        Set(value As String)
            mstrCusCode = value
        End Set
    End Property

    Public Property IsReplace As String
        Get
            Return mstrIsReplace
        End Get
        Set(value As String)
            mstrIsReplace = value
        End Set
    End Property

    Public Property IsAdjust As String
        Get
            Return mstrIsAdjust
        End Get
        Set(value As String)
            mstrIsAdjust = value
        End Set
    End Property
    Public Property ComID As String
        Get
            Return mstrComId
        End Get
        Set(value As String)
            mstrComId = value
        End Set
    End Property
    Public Property PBan As String
        Get
            Return mstrPBan
        End Get
        Set(value As String)
            mstrPBan = value
        End Set
    End Property
    Public Property THDon As String
        Get
            Return mstrTHDon
        End Get
        Set(value As String)
            mstrTHDon = value
        End Set
    End Property
    Public Property KHMSHDon As String
        Get
            Return mstrKHMSHDon
        End Get
        Set(value As String)
            mstrKHMSHDon = value
        End Set
    End Property

    Public Property Cur As String
        Get
            Return mstrCur
        End Get
        Set(value As String)
            mstrCur = value
        End Set
    End Property

    Public Property Roe As Integer
        Get
            Return mintRoe
        End Get
        Set(value As Integer)
            mintRoe = value
        End Set
    End Property

    Public Property TotalInvInLetters As String
        Get
            Return mstrTotalInvInLetters
        End Get
        Set(value As String)
            mstrTotalInvInLetters = value
        End Set
    End Property

    Public Property Fkey As String
        Get
            Return mstrFkey
        End Get
        Set(value As String)
            mstrFkey = value
        End Set
    End Property

    Public Property MaCoQuanThue As String
        Get
            Return mstrMaCoQuanThue
        End Get
        Set(value As String)
            mstrMaCoQuanThue = value
        End Set
    End Property
    Public Property LinkedMauSo As String
        Get
            Return mstrLinkedMauSo
        End Get
        Set(value As String)
            mstrLinkedMauSo = value
        End Set
    End Property
    Public Property LinkedKyHieu As String
        Get
            Return mstrLinkedKyHieu
        End Get
        Set(value As String)
            mstrLinkedKyHieu = value
        End Set
    End Property
    Public Property LinkedInvNbr As Integer
        Get
            Return mintLinkedInvNbr
        End Get
        Set(value As Integer)
            mintLinkedInvNbr = value
        End Set
    End Property
    Public Property LinkedInvDOI As Date
        Get
            Return mdteLinkedInvDOI
        End Get
        Set(value As Date)
            mdteLinkedInvDOI = value
        End Set
    End Property
    Public Property LinkedTCHDon As String
        Get
            Return mstrLinkedTCHDon
        End Get
        Set(value As String)
            mstrLinkedTCHDon = value
        End Set
    End Property
    Public Property LinkedLHDCLQuan As String
        Get
            Return mstrLinkedLHDCLQuan
        End Get
        Set(value As String)
            mstrLinkedLHDCLQuan = value
        End Set
    End Property
    Public Property LinkedGhiChu As String
        Get
            Return mstrLinkedGhiChu
        End Get
        Set(value As String)
            mstrLinkedGhiChu = value
        End Set
    End Property
End Class
