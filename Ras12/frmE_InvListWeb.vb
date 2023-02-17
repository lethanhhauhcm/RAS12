Imports System.Xml
Public Class frmE_InvListWeb
    Private mblnFirstLoadCompleted As Boolean
    Private mstrInvSettingTable As String = "lib.dbo.E_invSettings"
    Private mstrInvoiceTable As String = "lib.dbo.E_Inv"
    Private mstrInvWebTable As String = "lib.dbo.E_InvWeb"
    Private mstrInvDetailTable As String = "lib.dbo.E_InvDetails"
    Private mstrInvDetailWebTable As String = "lib.dbo.E_InvDetailsWeb"
    Private mstrInvLinkTable As String = "lib.dbo.E_InvLinks"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If pblnTT78 AndAlso Not mblnFirstLoadCompleted Then
            mstrInvSettingTable = "lib.dbo.E_invSettings78"
            mstrInvoiceTable = "lib.dbo.E_Inv78"
            mstrInvWebTable = "lib.dbo.E_InvWeb78"
            mstrInvDetailTable = "lib.dbo.E_InvDetails78"
            mstrInvDetailWebTable = "lib.dbo.E_InvDetailsWeb78"
            mstrInvLinkTable = "lib.dbo.E_InvLinks78"

        End If
    End Sub

    Private Sub frmE_InvList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'DownloadMissingInbvoices()
        'MapShortName()
        Download(pblnTT78)
        LoadCombo(cboTVC, "Select distinct TVC as Value from " & mstrInvSettingTable _
                    & " where Status<>'XX' and City='" & myStaff.City & "' order by TVC", Conn)

        LoadCombo(cboCustGroup, "select Val as Value from Misc where Cat='CustGroupName' and Val1='E'", Conn)
        LoadCombo(cboMauSo, "Select distinct MauSo as value from " & mstrInvoiceTable & " order by MauSo", Conn)
        LoadCombo(cboKyHieu, "Select distinct KyHieu as value from " & mstrInvoiceTable & " order by KyHieu", Conn)
        Clear()

        mblnFirstLoadCompleted = True
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Public Function DownloadMissingInbvoices() As Boolean
        Dim tblInvDup As DataTable = GetDataTable(" select * from " & mstrInvWebTable _
                                                  & " where status='OK'  order by TVC, MauSo,KyHieu,InvoiceNo")
        Dim strTvc As String = ""
        Dim intInvNo As Integer
        For Each objRow As DataRow In tblInvDup.Rows
            If strTvc <> objRow("Tvc") & objRow("MauSo") & objRow("KyHieu") Then
                strTvc = objRow("Tvc") & objRow("MauSo") & objRow("KyHieu")
                intInvNo = 0
            End If
            intInvNo = intInvNo + 1
            If intInvNo < objRow("InvoiceNo") Then
                Dim i As Integer
                Dim lstStart As New List(Of Integer)
                Dim lstEnd As New List(Of Integer)
                For i = intInvNo To objRow("InvoiceNo") - 1 Step 100
                    lstStart.Add(i)
                    If i + 99 <= objRow("InvoiceNo") - 1 Then
                        lstEnd.Add(i + 99)
                    Else
                        lstEnd.Add(objRow("InvoiceNo") - 1)
                    End If
                Next
                For i = 0 To lstStart.Count - 1
                    If Not SyncFromWeb(objRow("TVC"), objRow("MauSo"), objRow("KyHieu") _
                               , lstStart(i), lstEnd(i), objRow("City")) Then
                        MsgBox("Unable to download data from VNPT Web for " _
                               & objRow("TVC") & " " & CreateMauSo(objRow("MauSo")) & " " _
                               & objRow("KyHieu") _
                               & " " & intInvNo & " " & objRow("City"))
                    End If
                Next

                intInvNo = objRow("InvoiceNo")
            End If
        Next
    End Function
    Public Function Download(blnTt78 As Boolean, Optional strTvc As String = "" _
                             , Optional strMauSo As String = "") As Boolean
        Dim tblTvc As DataTable
        Dim strStartInvNo As String

        If blnTt78 Then
            'strStartInvNo = "(select isnull(max(InvoiceNo),0)+1 from " & mstrInvWebTable & " w" _
            '        & " where w.RecordStatus<>'xx' and w.Tvc=s.Tvc and w.MauSo=s.MauSo" _
            '        & ") as Start"
            strStartInvNo = "(select isnull(max(InvoiceNo),0)+1 from " & mstrInvWebTable & " w" _
                    & " where w.RecordStatus<>'xx' and w.Tvc=s.Tvc and w.MauSo=s.MauSo" _
                    & " and year(w.PublishDate)=" & Now.Year & ") as Start"
        Else
            strStartInvNo = "(select isnull(max(InvoiceNo),0)+1 from " & mstrInvWebTable & " w" _
                    & " where w.RecordStatus<>'xx' and w.Tvc=s.Tvc and right(w.MauSo,3)=s.MauSo) as Start"
        End If
        Dim strSelect As String = "Select distinct Tvc,MauSo,KyHieu,City" & "," & strStartInvNo _
                            & " from " & mstrInvSettingTable & " s" & " where Status='ok'"
        If strTvc <> "" Then
            strSelect = strSelect & " and Tvc='" & strTvc & "'"
        End If
        If strMauSo <> "" Then
            strSelect = strSelect & " and MauSo='" & strMauSo & "'"
        End If

        Dim strOrder As String = " order by Tvc,MauSo,KyHieu"
        tblTvc = GetDataTable(strSelect & strOrder, Conn_Web)

        For Each objRow As DataRow In tblTvc.Rows
            If Not SyncFromWeb(objRow("TVC"), CreateMauSo(objRow("MauSo")), CreateKyHieu(objRow("KyHieu")) _
                               , objRow("Start"), objRow("Start") + 99, objRow("City")) Then
                MsgBox("Unable to download data from VNPT Web for " _
                       & objRow("TVC") & " " & CreateMauSo(objRow("MauSo")) & " " _
                       & objRow("KyHieu") _
                       & " " & objRow("Start") & " " & objRow("City"))
            End If
        Next

        If Not DownloadInvoiceDetail() Then
            MsgBox("Unable to download Invoice Details to Local Data. Please report Khanhnm!")
            Return False
        End If

        If Not MapWebLocal() Then
            MsgBox("Unable to map Web data with Local Data. Please report Khanhnm!")
            Return False
        End If

        Return True
    End Function
    Private Function Clear() As Boolean
        cboTVC.SelectedIndex = -1
        cboCustType.SelectedIndex = -1
        cboCustGroup.SelectedIndex = -1
        cboCustomer.SelectedIndex = -1
        cboAL.SelectedIndex = -1
        cboMauSo.SelectedIndex = -1
        cboKyHieu.SelectedIndex = -1
        txtInvoiceNo.Text = ""
        txtInvId.Text = ""
        cboBiz.SelectedIndex = -1
        'chkDraft.CheckState = CheckState.Indeterminate
        cboBackDate.SelectedIndex = 1
        Return True
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String
        strQuerry = "Select RecId, MauSo, KyHieu, InvoiceNo, InvId, PublishDate" _
                    & ", Vatable, Amount, SRV, CustShortName, Payment, InvStatus, RecordStatus" _
                    & ", FstUser, FstUpdate, LstUser, LstUpdate, City, TVC, BIZ, AL, CustFullName" _
                    & ", invToken, fkey,Extra1,Extra2,Extra,Email, SignStatus, invIndex"

        If pblnTT78 Then
            strQuerry = strQuerry & ",MaCoQuanThue,LinkedTCHDon, LinkedLHDCLQuan, LinkedMauSo, LinkedKyHieu, 
                         LinkedInvNbr, LinkedInvDOI, LinkedGhiChu"
        End If
        strQuerry = strQuerry & " from " & mstrInvWebTable _
            & " where RecordStatus<>'XX' and City='" & myStaff.City & "'"

        AddEqualConditionCombo(strQuerry, cboTVC)
        AddEqualConditionCombo(strQuerry, cboBiz)
        AddEqualConditionCombo(strQuerry, cboAL)
        AddEqualConditionCombo(strQuerry, cboMauSo)
        AddEqualConditionCombo(strQuerry, cboKyHieu)
        AddEqualConditionText(strQuerry, txtInvoiceNo)
        AddEqualConditionText(strQuerry, txtInvId)

        If cboCustType.Text <> "" Then
            strQuerry = strQuerry & " and CustId in " _
                & " (Select CustId from Cust_Detail where Status='OK' and Cat='channel' and VAL='" _
                & cboCustType.Text & "')"
        End If
        If cboCustGroup.Text <> "" Then
            strQuerry = strQuerry & " and CustId in " _
                & " (Select intVal from Misc where Status='OK' and Cat='CustNameInGroup' and VAL='" _
                & cboCustGroup.Text & "')"
        End If

        If cboCustomer.Text <> "" Then
            strQuerry = strQuerry & " and CustId = " & cboCustomer.SelectedValue
        End If

        'If txtInvoiceContent.Text <> "" Then
        '    strQuerry = strQuerry & " and InvId in (select InvId from E_InvDetails where Status='OK' and Description like '%" & txtInvoiceContent.Text & "%')"
        'End If
        If IsNumeric(cboBackDate.Text) Then
            strQuerry = strQuerry & " and DateDiff(d,PublishDate,Getdate())<=" & cboBackDate.Text
        End If

        strQuerry = strQuerry & " order by TVC,MAUSO,KYHIEU,INVOICENO desc"
        LoadDataGridView(dgrE_Invoices, strQuerry, Conn_Web)
        'dgrE_Invoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        dgrE_Invoices.Columns("RecId").Width = 40
        dgrE_Invoices.Columns("InvId").Width = 40
        'dgrE_Invoices.Columns("InvType").Width = 40
        dgrE_Invoices.Columns("TVC").Width = 65
        'dgrE_Invoices.Columns("Biz").Width = 40
        'dgrE_Invoices.Columns("AL").Width = 30
        dgrE_Invoices.Columns("MauSo").Width = 80
        dgrE_Invoices.Columns("KyHieu").Width = 50
        dgrE_Invoices.Columns("InvoiceNo").Width = 50
        dgrE_Invoices.Columns("SRV").Width = 30
        dgrE_Invoices.Columns("PublishDate").Width = 70
        'dgrE_Invoices.Columns("FOP").Width = 50
        'dgrE_Invoices.Columns("Address").Width = 130
        dgrE_Invoices.Columns("InvStatus").Width = 48

        dgrE_Invoices.Columns("Vatable").DefaultCellStyle.Format = "#,##0"
        dgrE_Invoices.Columns("Amount").DefaultCellStyle.Format = "#,##0"
        dgrE_Invoices.Columns("Vatable").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrE_Invoices.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.Text = "E_Invoice List - " & dgrE_Invoices.RowCount & " records"
        Return True
    End Function

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Dim frmEdit As New frmE_InvEdit
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub cboCustType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustType.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadComboDisplay(cboCustomer, "Select RecId as Value, CustShortName as Display" _
                & " from CustomerList " _
                & " where Status='OK' and RecId in (Select CustId from Cust_Detail where Status='OK'" _
                & " and CAT='Channel' and VAL='" & cboCustType.Text & "') order by CustShortName ", Conn)
        End If
    End Sub

    Private Sub cboCustGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustGroup.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadComboDisplay(cboCustomer, "Select RecId as Value, CustShortName as Display" _
                & " from CustomerList " _
                & " where Status='OK' and RecId in (Select intVal from Misc where Status='OK'" _
                & " and CAT='CustNameInGroup' and VAL='" & cboCustGroup.Text & "')  order by CustShortName ", Conn)
        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim frmEdit As New frmE_InvEdit(dgrE_Invoices.CurrentRow)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub
    Private Sub cboTvCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTVC.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadCombo(cboBiz, "Select distinct Biz as Value from " & mstrInvSettingTable _
                    & " where Status<>'XX' and TVC='" & cboTVC.Text & "' order by Biz", Conn)
            cboBiz.SelectedIndex = -1
        End If
    End Sub

    Private Sub cboBiz_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBiz.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadCombo(cboAL, "Select distinct AL as Value from " & mstrInvSettingTable _
                    & " where Status<>'XX' and TVC='" & cboTVC.Text _
                    & "' and Biz='" & cboBiz.Text & "' order by AL", Conn)
            cboAL.SelectedIndex = -1
        End If
    End Sub

    Private Sub dgrE_Invoices_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrE_Invoices.CellContentClick

    End Sub

    Private Sub lbkDownloadInv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDownloadInv.LinkClicked
        Dim objE_Inv As New clsE_Invoice

        With dgrE_Invoices.CurrentRow
            Dim objE_InvConnect As New clsE_InvConnect(pblnTT78, .Cells("TVC").Value)
            'Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, 8)
            Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value)

            If pblnTestInv Then
                objE_InvConnect.WsUrl = "https://tranviethcm-tt78admindemo.vnpt-invoice.com.vn/PublishService.asmx"
                objE_InvConnect.BusinessServiceUrl = "https://tranviethcm-tt78admindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                objE_InvConnect.PortalServiceUrl = "https://tranviethcm-tt78admindemo.vnpt-invoice.com.vn/PortalService.asmx"
                objE_InvConnect.AccountName = "tranviethcmadmin"
                objE_InvConnect.AccountPass = "Einv@oi@vn#pt20"
                objE_InvConnect.UserPass = "Einv@oi@vn#pt20"
                objE_InvConnect.UserName = "tranviethcmservice"
            End If

            If Not objE_Inv.downloadInvNoPay(objE_InvConnect.PortalServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                       , strInvoiceToken) Then
                MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
            Else
                Dim objContent As New clsE_InvContent
                If pblnTT78 Then
                    objContent.ParseXmlTT78(objE_Inv.ResponseDesc)
                Else
                    objContent.ParseXml(objE_Inv.ResponseDesc)
                End If

                MsgBox(objE_Inv.ResponseDesc)
            End If
        End With
    End Sub

    Private Sub lbkDownloadInvPDF_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDownloadInvPDF.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim objE_Inv As New clsE_Invoice

        With dgrE_Invoices.CurrentRow
            Dim objConnect As New clsE_InvConnect(pblnTT78, .Cells("TVC").Value)
            Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value)
            Dim strPath As String = "D:\Invoice " & .Cells("InvoiceNo").Value & ".pdf"
            If Not objE_Inv.downloadInvPDF(objConnect.PortalServiceUrl, objConnect.UserName, objConnect.UserPass _
                                       , strInvoiceToken) Then
                MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
            ElseIf Base64ToFile(objE_Inv.ReponseCode, strPath) Then
                MsgBox(strPath)
            Else
                MsgBox("Unable to save PDF file")
            End If
        End With
    End Sub

    Private Sub lbkDownloadInvPDFNoPay_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDownloadInvPDFNoPay.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim objE_Inv As New clsE_Invoice

        With dgrE_Invoices.CurrentRow
            Dim objE_InvConnect As New clsE_InvConnect(pblnTT78, .Cells("TVC").Value)
            Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value)
            Dim strPath As String = "D:\Invoice " & .Cells("InvoiceNo").Value & ".pdf"

            If pblnTestInv Then
                objE_InvConnect.WsUrl = "https://tranviethcm-tt78admindemo.vnpt-invoice.com.vn/PublishService.asmx"
                objE_InvConnect.BusinessServiceUrl = "https://tranviethcm-tt78admindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                objE_InvConnect.PortalServiceUrl = "https://tranviethcm-tt78admindemo.vnpt-invoice.com.vn/PortalService.asmx"
                objE_InvConnect.AccountName = "tranviethcmadmin"
                objE_InvConnect.AccountPass = "Einv@oi@vn#pt20"
                objE_InvConnect.UserPass = "Einv@oi@vn#pt20"
                objE_InvConnect.UserName = "tranviethcmservice"
            End If


            If Not objE_Inv.downloadInvPDFNoPay(objE_InvConnect.PortalServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                       , strInvoiceToken) Then
                MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
            ElseIf Base64ToFile(objE_Inv.ReponseCode, strPath) Then
                MsgBox(strPath)
            Else
                MsgBox("Unable to save PDF file")
            End If
        End With
    End Sub

    Private Sub lbkSyncFromWeb_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSyncFromWeb.LinkClicked
        Dim blnDownloadCompleted As Boolean
        '        XacDinhThongTuHoaDon()

        If Download(pblnTT78) Then
            blnDownloadCompleted = True
        End If
        If blnDownloadCompleted Then
            Search()
        End If
    End Sub
    Private Function SyncFromWeb(strTvc As String, strMauSo As String, strKyHieu As String _
                                 , intStartNo As Integer, intEndNo As Integer, strCity As String) As Boolean
        Dim objConnect As New clsE_InvConnect(pblnTT78, strTvc)
        Dim objE_Inv As New clsE_Invoice


        If Not objE_Inv.listInvFromNoToNo(objConnect.PortalServiceUrl, objConnect.UserName, objConnect.UserPass _
                                       , strMauSo, strKyHieu, intStartNo, intEndNo) Then
            Return False
        Else
            'update into Local database
            Dim objXmlDoc As New Xml.XmlDocument
            Dim lstInvoices As Xml.XmlNodeList
            Dim lstQuerries As New List(Of String)


            objXmlDoc.LoadXml(objE_Inv.ReponseCode)
            lstInvoices = objXmlDoc.GetElementsByTagName("Item")
            For Each objNode As XmlNode In lstInvoices
                Dim intInvIndex As Integer
                Dim strInvToken As String = ""
                Dim strFkey As String = ""
                Dim intInvoiceNo As Integer
                Dim strDOI As String = ""
                Dim intSignStatus As Integer
                Dim decVatable As Decimal
                Dim decAmount As Decimal
                Dim intPayment As Integer
                Dim intStatus As Integer
                Dim strInvStatus As String = ""
                Dim strCustFullName As String = ""

                For Each objSubNode As XmlNode In objNode.ChildNodes
                    Select Case objSubNode.Name
                        Case "index"
                            intInvIndex = objSubNode.InnerText
                        Case "invToken"
                            strInvToken = objSubNode.InnerText
                        Case "fkey"
                            strFkey = objSubNode.InnerText
                        Case "publishDate"
                            strDOI = objSubNode.InnerText
                        Case "signStatus"
                            intSignStatus = objSubNode.InnerText
                        Case "total"
                            decVatable = objSubNode.InnerText
                        Case "amount"
                            decAmount = objSubNode.InnerText
                        Case "invNum"
                            intInvoiceNo = objSubNode.InnerText
                        Case "status"
                            intStatus = objSubNode.InnerText
                            strInvStatus = ConvertEInvStatus(intStatus)
                        Case "cusname"
                            strCustFullName = objSubNode.InnerText.Replace("<![CDATA[", "").Replace("]]>", "")
                            If strCustFullName.Contains("'") Then
                                strCustFullName = Replace(strCustFullName, "'", "''")
                            End If
                        Case "payment"
                            intPayment = objSubNode.InnerText
                    End Select
                Next

                lstQuerries.Add("Insert into " & mstrInvWebTable & " (MauSo, KyHieu, InvoiceNo, PublishDate" _
                                & ", SignStatus, Vatable, Amount, Payment" _
                                & ", Status,InvStatus, RecordStatus, FstUser, City, TVC, CustFullName" _
                                & ", InvIndex, invToken, fkey) values ('" & strMauSo _
                                & "','" & strKyHieu & "'," & intInvoiceNo _
                                & ",'" & strDOI & "'," & intSignStatus & "," & decVatable _
                                & "," & decAmount & "," & intPayment & "," & intStatus _
                                & ",'" & strInvStatus & "','QQ','" & myStaff.SICode & "','" & strCity _
                                & "','" & strTvc & "',N'" & strCustFullName & "'," & intInvIndex _
                                & ",'" & strInvToken & "','" & strFkey & "')")
            Next

            Return UpdateListOfQuerries(lstQuerries, Conn_Web)

        End If

    End Function
    Private Function MapTaxCodeAddress() As Boolean
        Dim tblInvoices As DataTable = GetDataTable("Select w.*,l.TaxCode as LocalTaxCode,l.Address as LocalAddress" _
                                                    & " from e_invweb w" _
                                                    & " left join e_inv l on w.invid=l.invid" _
                                                    & " where w.invid<>0 and w.taxcode='' and w.City='" & myStaff.City & "'")
        For Each objRow As DataRow In tblInvoices.Rows
            ExecuteNonQuerry("Update e_invweb set Taxcode='" & objRow("LocalTaxCode") _
                             & "',Address=N'" & objRow("LocalAddress") & "' where Recid=" & objRow("RecId"), Conn)
        Next
        Return True
    End Function
    Private Function MapShortName() As Boolean
        Dim tblInvoices As DataTable = GetDataTable("Select w.*,l.CustShortName as LocalCustShortName" _
                                                    & " from e_invweb w" _
                                                    & " left join e_inv l on w.invid=l.invid" _
                                                    & " where w.invid<>0 and w.CustShortName=''" _
                                                    & " and w.City='" & myStaff.City & "'")
        For Each objRow As DataRow In tblInvoices.Rows
            ExecuteNonQuerry("Update e_invweb set CustShortName='" & objRow("LocalCustShortName") _
                             & "' where Recid=" & objRow("RecId"), Conn)
        Next
        Return True
    End Function
    Public Function DownloadMaCoQuanThue() As Boolean
        Dim tblInvoices As DataTable
        Dim strQuerry As String

        Dim lstQuerries As New List(Of String)
        Dim objConnect As clsE_InvConnect = Nothing
        strQuerry = "Select *" _
                    & " from lib.dbo.E_InvWeb78" _
                    & " where InvoiceNo<>0 and MaCoQuanThue=''" _ ' and Stopdownload='False'" _
                    & " and datediff(d,PublishDate,GetDate())<7" _
                    & " order by Tvc,MauSo,KyHieu,RecId"

        tblInvoices = GetDataTable(strQuerry, Conn_Web)
        For Each objRow As DataRow In tblInvoices.Rows
            Dim objE_Inv As New clsE_Invoice
            Dim intGiamThue As Integer = 0
            If objConnect Is Nothing Then
                objConnect = New clsE_InvConnect(pblnTT78, objRow("TVC"))
            ElseIf objConnect.Tvc <> objRow("TVC") Then
                objConnect = New clsE_InvConnect(pblnTT78, objRow("TVC"))
            End If

            If Not objE_Inv.downloadInvNoPay(objConnect.PortalServiceUrl, objConnect.UserName _
                                        , objConnect.UserPass, objRow("InvToken")) Then
                If objE_Inv.ReponseCode = "ERR:12" Then
                    ExecuteNonQuerry("update " & mstrInvWebTable & " Set StopDownLoad='True' where RecId=" & objRow("RecId"), Conn)
                Else
                    MsgBox("Unable to get MaCoQuanThue from Web for Invoice Recid " & objRow("RecId") _
                           & vbNewLine & objE_Inv.ResponseDesc)
                End If

                Continue For

            End If

            Dim objContent As New clsE_InvContent

            If pblnTT78 AndAlso Not objContent.ParseXmlTT78(objE_Inv.ReponseCode) Then
                MsgBox("Unable to parse Invoice Content for Web Invoice Recid " & objRow("RecId"))
            Else
                With objContent
                    If pblnTT78 Then
                        strQuerry = "Update lib.dbo.E_invWeb78 set MaCoQuanThue='" & .MaCoQuanThue & "'"
                        strQuerry = strQuerry & " where RecId=" & objRow("RecId")
                        lstQuerries.Add(strQuerry)
                    End If
                    lstQuerries.Add(strQuerry)
                    If objRow("InvId") <> 0 Then
                        strQuerry = "Update lib.dbo.E_inv78 set MaCoQuanThue='" & .MaCoQuanThue & "'"
                        strQuerry = strQuerry & " where MaCoQuanThue='' and InvId=" & objRow("InvId")
                        lstQuerries.Add(strQuerry)
                    End If

                End With

                If Not UpdateListOfQuerries(lstQuerries, Conn_Web) Then
                    MsgBox("Unable to update MaCoQuanThue from Web to Local for Web Invoice Recid " & objRow("RecId"))
                End If
                lstQuerries.Clear()

            End If

        Next
        Return True

    End Function
    Private Function DownloadInvoiceDetail() As Boolean
        Dim tblInvoices As DataTable
        Dim strQuerry As String

        Dim lstQuerries As New List(Of String)
        Dim objConnect As clsE_InvConnect = Nothing
        strQuerry = "Select *" _
                    & " from " & mstrInvWebTable _
                    & " where HasDetails=0 and StopDownload='false'" _
                    & " order by Tvc,MauSo,KyHieu,RecId"

        tblInvoices = GetDataTable(strQuerry, Conn_Web)
        For Each objRow As DataRow In tblInvoices.Rows
            Dim objE_Inv As New clsE_Invoice
            Dim intGiamThue As Integer = 0
            If objConnect Is Nothing Then
                objConnect = New clsE_InvConnect(pblnTT78, objRow("TVC"))
            ElseIf objConnect.Tvc <> objRow("TVC") Then
                objConnect = New clsE_InvConnect(pblnTT78, objRow("TVC"))
            End If

            If objRow("Payment") = 0 AndAlso Not objE_Inv.downloadInvNoPay(objConnect.PortalServiceUrl, objConnect.UserName _
                                        , objConnect.UserPass, objRow("InvToken")) Then
                If objE_Inv.ReponseCode = "ERR:12" Then
                    ExecuteNonQuerry("update " & mstrInvWebTable & " Set StopDownLoad='True' where RecId=" & objRow("RecId"), Conn)
                Else
                    MsgBox("Unable to get Invoice Details from Web for Invoice Recid " & objRow("RecId") _
                           & vbNewLine & objE_Inv.ResponseDesc)
                End If

                Continue For

            ElseIf objRow("Payment") = 1 AndAlso Not objE_Inv.downloadInv(objConnect.PortalServiceUrl, objConnect.UserName _
                                        , objConnect.UserPass, objRow("InvToken")) Then
                    MsgBox("Unable to get Invoice Details from Web for Invoice Recid " & objRow("RecId"))
                Continue For
            End If

            Dim objContent As New clsE_InvContent

            If pblnTT78 AndAlso Not objContent.ParseXmlTT78(objE_Inv.ReponseCode) Then
                MsgBox("Unable to parse Invoice Content for Invoice Recid " & objRow("RecId"))
            ElseIf Not pblnTT78 AndAlso Not objContent.ParseXml(objE_Inv.ReponseCode) Then
                MsgBox("Unable to parse Invoice Content for Invoice Recid " & objRow("RecId"))
            Else
                With objContent

                    If pblnTT78 Then
                        strQuerry = "Update " & mstrInvWebTable & " set HasDetails=1,Buyer=N'" _
                        & .Buyer & "',FOP='" & .FOP & "',CustId=" & .CustId & ",CusCode='" & .CusCode _
                        & "',TaxCode='" & .CustTaxCode & "',Address=N'" & .CustAddress _
                        & "',Extra1=N'" & .Extra1 & "',Extra2='" & .Extra2 _
                        & "',Email='" & .EmailDeliver & "',Discount_Rate=" & .Discount_Rate _
                        & ",Discount_Amount=" & .Discount_Amount & ",PBan='" & .PBan & "',THDon='" & .THDon _
                        & "',ROE=" & .Roe & ",Vat=" & .TotalVat & ",MaCoQuanThue='" & .MaCoQuanThue & "'"
                        If .LinkedTCHDon <> "" Then
                            strQuerry = strQuerry & ",LinkedTCHDon='" & .LinkedTCHDon & "',LinkedLHDCLQuan='" & .LinkedLHDCLQuan _
                                & "',LinkedMauSo='" & .LinkedMauSo & "',LinkedKyHieu='" & .LinkedKyHieu _
                                & "',LinkedInvNbr=" & .LinkedInvNbr & ",LinkedInvDOI='" & CreateFromDate(.LinkedInvDOI) _
                                & "',LinkedGhiChu=N'" & .LinkedGhiChu & "'"
                        End If
                        strQuerry = strQuerry & " where RecId=" & objRow("RecId")
                    Else
                        If objRow("MauSo").ToString.EndsWith("01") AndAlso .Extra <> "" Then
                            intGiamThue = .Extra
                        End If
                        strQuerry = "Update " & mstrInvWebTable & " set HasDetails=1,Buyer=N'" _
                        & .Buyer & "',FOP='" & .FOP & "',CustId=" & .CustId & ",CusCode='" & .CusCode _
                        & "',TaxCode='" & .CustTaxCode & "',Address=N'" & .CustAddress _
                        & "',Extra=N'" & .Extra & "',Extra1='" & .Extra1 _
                        & "',Extra2='" & .Extra2 & "',Extras='" & .Extras _
                        & "',Email='" & .EmailDeliver & "',Discount_Rate=" & .Discount_Rate _
                        & ",Discount_Amount=" & .Discount_Amount & ",GrossValue=" & .GrossValue _
                        & ",GrossValue0=" & .GrossValue0 & ",GrossValue5=" & .GrossValue5 _
                        & ",GrossValue10=" & .GrossValue10 & ",GrossValueNonTax=" & .GrossValueNonTax _
                        & ",VAT_Rate=" & .VAT_Rate & ",IsReplace=N'" & .IsReplace _
                        & "',IsAdjust=N'" & .IsAdjust & "',GiamThue=" & intGiamThue _
                        & " where RecId=" & objRow("RecId")
                    End If
                    lstQuerries.Add(strQuerry)

                    For Each objPro As clsProduct In .Products
                        With objPro
                            If pblnTT78 Then
                                strQuerry = "insert into " & mstrInvDetailWebTable & " (InvIdWeb" _
                            & ",InvToken, ProdName, Unit, Qty" _
                            & ", Price, DiscountRate, DiscountAmt, VatPct, VAT, TotalPrice" _
                            & ", TChat, Remark, ProdNo, Extra1, Extra2) values (" & objRow("Recid") _
                            & ",'" & objRow("InvToken") & "',N'" & .ProdName & "',N'" & .ProdUnit _
                            & "'," & .ProdQuantity & "," & .ProdPrice & "," & .DiscountRate _
                            & "," & .DiscountAmount & "," & .VatRate & "," & .VatAmount _
                            & "," & .TotalPrice & ",'" & .TChat & "','" & .Remark & "','" & .ProdNo _
                            & "',N'" & .Extra1 & "',N'" & .Extra2 & "')"
                            Else
                                strQuerry = "insert into " & mstrInvDetailWebTable & " (InvIdWeb" _
                            & ",InvToken, ProdName, Unit, Qty" _
                            & ", Price, DiscountRate, DiscountAmt, VatPct, VAT, Total" _
                            & ", Extra1, Extra2, Remark, ProdNo) values (" & objRow("Recid") _
                            & ",'" & objRow("InvToken") & "',N'" & .ProdName & "','" & .ProdUnit _
                            & "'," & .ProdQuantity & "," & .ProdPrice & "," & .DiscountRate _
                            & "," & .DiscountAmount & "," & .VatRate & "," & .VatAmount _
                            & "," & .TotalPrice & ",N'" & .Extra1 & "',N'" & .Extra2 _
                            & "','" & .Remark & "','" & .ProdNo & "')"
                            End If

                        End With
                        lstQuerries.Add(strQuerry)
                    Next

                End With

                If Not UpdateListOfQuerries(lstQuerries, Conn_Web) Then
                        MsgBox("Unable to update Invoice Details from Web to Local for Invoice Recid " & objRow("RecId"))
                    End If
                lstQuerries.Clear()

            End If

        Next
        Return True

    End Function
    Private Function MapWebLocal() As Boolean
        Dim tblInvoices As DataTable
        Dim strQuerry As String
        Dim lstQuerries As New List(Of String)
        strQuerry = "Select w.*, l.InvId as LocalInvId,l.Srv as LocalSRV" _
                    & ",l.Biz as LocalBiz,l.AL as LocalAL,l.RecId as LocalId" _
                    & " from " & mstrInvWebTable & " w" _
                    & " left join " & mstrInvoiceTable & " l on w.Tvc=l.Tvc And w.InvToken=l.InvToken" _
                    & " where w.RecordStatus='QQ' and l.InvId<>0 " _
                    & " order by w.RecId"

        tblInvoices = GetDataTable(strQuerry, Conn_Web)
        For Each objRow As DataRow In tblInvoices.Rows

            'update web, local records
            lstQuerries.Add("Update " & mstrInvWebTable & " set RecordStatus='OK', InvId=" & objRow("LocalInvId") _
                            & ",SRV='" & objRow("LocalSRV") & "',Biz='" & objRow("LocalBiz") _
                            & "',AL='" & objRow("LocalAL") _
                            & "' where RecId=" & objRow("RecId"))
            If objRow("MaCoQuanThue") <> "" Then
                lstQuerries.Add("Update " & mstrInvoiceTable & " set MaCoQuanThue='" & objRow("MaCoQuanThue") _
                            & "' where RecId=" & objRow("LocalId"))
            End If
        Next
        Return UpdateListOfQuerries(lstQuerries, Conn_Web)

    End Function
    Private Sub lbkResendEmail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkResendEmail.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim objE_Inv As New clsE_Invoice
        Dim strEmails As String

        With dgrE_Invoices.CurrentRow
            strEmails = InputBox("Input Emails, seperated by semi-column",, .Cells("Email").Value)
            If Not CheckFormatEmails(strEmails, False) Then
                MsgBox("Invalid Emails!")
                Exit Sub
            End If

            Dim objConnect As New clsE_InvConnect(pblnTT78, .Cells("TVC").Value)
            Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value)
            Dim strPath As String = "D:\Invoice " & .Cells("InvoiceNo").Value & ".pdf"
            If Not objE_Inv.SendAgainEmailServ(objConnect.WsUrl, objConnect.UserName, objConnect.UserPass _
                                        , objConnect.AccountName, objConnect.AccountPass _
                                       , .Cells("MauSo").Value, .Cells("KyHieu").Value _
                                       , .Cells("InvId").Value, strEmails) Then
                MsgBox("Unable to Resend Email!" & vbNewLine & objE_Inv.ResponseDesc)
            Else
                MsgBox("Done!")
            End If
        End With
    End Sub

    Private Sub lbkViewInvoiceFromWeb_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewInvoiceFromWeb.LinkClicked
        Dim objE_Inv As New clsE_Invoice

        With dgrE_Invoices.CurrentRow
            Dim objConnect As New clsE_InvConnect(pblnTT78, .Cells("TVC").Value)
            Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value)
            If Not objE_Inv.getInvViewNoPay(objConnect.PortalServiceUrl, objConnect.UserName, objConnect.UserPass _
                                       , strInvoiceToken) Then
                MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
            Else
                Dim strPath As String = "D:\Invoice " & .Cells("InvoiceNo").Value & ".html"
                Dim objLogFile As New System.IO.StreamWriter(strPath)
                objLogFile.WriteLine(objE_Inv.ResponseDesc)
                objLogFile.Close()
                objLogFile = Nothing
                Process.Start(strPath)

            End If
        End With
    End Sub

    Private Sub lbkFindMissing_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFindMissing.LinkClicked
        DownloadMissingInbvoices()
    End Sub

    Private Sub dgrE_Invoices_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrE_Invoices.CellClick
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        LoadDataGridView(dgrE_InvDetails, "Select * from " & mstrInvDetailWebTable & " where Status='ok' and InvIdWeb=" _
                         & dgrE_Invoices.CurrentRow.Cells("RecId").Value, Conn)

    End Sub

    Private Sub lbkSync2RAS_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSync2RAS.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim strInvId As String = ""

        With dgrE_Invoices.CurrentRow
            If IsNumeric(.Cells("Fkey").Value) Then
                Dim tblRasInv As DataTable = GetDataTable("Select top 1 * from lib.dbo.E_Inv78" _
                                            & " where Status<>'XX' and InvId=" & strInvId)
                Dim lstUpdates As New List(Of String)
                If tblRasInv.Rows.Count = 0 Then
                    Dim strQuerry As String
                    Dim lstQuerry As New List(Of String)
                    Dim strFields As String
                    Dim strValues As String
                    Dim intNewInvRecId As Integer
                    Dim strBiz As String
                    Dim strAl As String
                    Dim tblCust As DataTable
                    Dim tblSettings As DataTable
                    Dim intCustId As Integer
                    Dim strCustShortName As String
                    Dim strBU As String
                    Dim strDomInt As String

                    tblSettings = GetDataTable("select * from lib.dbo.E_InvSettings78" _
                                & " WHERE Status='OK' and MauSo='" & .Cells("MauSo").Value _
                                & "' AND KYHIEU='" & .Cells("KyHieu").Value _
                                & "' AND TVC='" & .Cells("TVC").Value & "'")
                    strBiz = tblSettings.Rows(0)("BIZ")
                    strAl = tblSettings.Rows(0)("AL")

                    tblCust = GetDataTable("select * from CustomerList where CustFullName=N'" _
                                          & .Cells("CustFullName").Value & "'")
                    Select Case tblCust.Rows.Count
                        Case 0
                            MsgBox("Không tìm thấy Customer trong RAS: " & .Cells("CustFullName").Value)
                            Exit Sub
                        Case 1
                            intCustId = .Cells("RecId").Value
                            strCustShortName = .Cells("CustShortName").Value
                        Case Else
                            Dim frmCust As New frmShowTableContent(tblCust, "select Customer", "RecId")
                            If frmCust.ShowDialog = DialogResult.OK Then
                                intCustId = frmCust.SelectedRow.Cells("RecId").Value
                                strCustShortName = frmCust.SelectedRow.Cells("CustShortName").Value
                            Else
                                Exit Sub
                            End If
                    End Select

                    If .Cells("TVC").Value.ToString.StartsWith("GDS") _
                        Or .Cells("TVC").Value.ToString.StartsWith("TVTR") Then
                        Dim tblBU As DataTable
                        tblBU = GetDataTable("SELECT strVal2 as BU from lib.dbo.Misc where strVal1='" _
                                & .Cells("TVC").Value & "' order by strVal2")
                        Dim frmBu As New frmShowTableContent(tblBU, "Select BU", "BU")
                        If frmBu.DialogResult = DialogResult.OK Then
                            strBU = frmBu.SelectedValue
                            If MsgBox("DOM ?", MsgBoxStyle.YesNo) = vbYes Then
                                strDomInt = "DOM"
                            Else
                                strDomInt = "INT"
                            End If
                        Else
                            Exit Sub
                        End If
                    End If
                    strFields = "insert into lib.dbo.E_Inv78" _
                                & " (TVC,Biz,AL,Srv,CustId,CustShortName, InvID,  CustFullName, Address, TaxCode" _
                                & ", InvoiceNo,Period, Status, FstUser, City,InvTotal,MauSo,KyHieu" _
                                & ",Email,BU,DomInt,FOP"

                        strValues = ") values ('" & .Cells("TVC").Value & "','" & .Cells("Fkey").Value

                        strQuerry = strFields & strValues & ")"
                        lstQuerry.Add(strQuerry)
                        If Not UpdateListOfQuerries(lstQuerry, Conn_Web, True, intNewInvRecId) Then
                            MsgBox("Unable to update VatInv record!")
                            Exit Sub
                        Else
                            lstQuerry.Clear()
                        End If

                    Else
                        lstUpdates.Add("update lib.dbo.E_Inv78" _
                                & " Set InvoiceNo=" & tblRasInv.Rows(0)("InvoiceNo") _
                                & ",DOI=" & tblRasInv.Rows(0)("PublishDate") _
                                & ",InvToken=" & tblRasInv.Rows(0)("InvToken") _
                                & ",MaCoQuanThue=" & tblRasInv.Rows(0)("MaCoQuanThue") _
                                & ",Draft='False'" _
                                & " where Status<>'XX' and InvId=" & strInvId)
                    lstUpdates.Add("update lib.dbo.E_InvWeb78" _
                                & " Set RecordStatus='OK'" _
                                & " where RecId=" & tblRasInv.Rows(0)("RecId"))
                    If UpdateListOfQuerries(lstUpdates, Conn) Then
                        Search()
                    Else
                        MsgBox("Không đồng bộ được dữ liệu Web xuống RAS cho InvoiceId" & strInvId _
                                & vbNewLine & "Đề nghị báo người lập trình RAS!")
                    End If
                End If
            End If
        End With
    End Sub
End Class

