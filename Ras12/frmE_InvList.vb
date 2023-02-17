Imports System.ComponentModel

Public Class frmE_InvList
    Private mblnFirstLoadCompleted As Boolean
    Private mstrInvSettingTable As String = "E_invSettings"
    Private mstrInvoiceTable As String = "E_Inv"
    Private mstrInvDetailTable As String = "E_InvDetails"
    Private mstrInvLinkTable As String = "E_InvLinks"
    Private mstrInvWebTable As String = "E_InvWeb"
    Private mstrInvDetailWebTable As String = "E_InvDetailsWeb"
    Private Sub frmE_InvList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If pblnTT78 Then
            mstrInvSettingTable = "lib.dbo.E_invSettings78"
            mstrInvoiceTable = "lib.dbo.E_Inv78"
            mstrInvDetailTable = "lib.dbo.E_InvDetails78"
            mstrInvLinkTable = "lib.dbo.E_InvLinks78"
            mstrInvWebTable = "lib.dbo.E_InvWeb78"
            mstrInvDetailWebTable = "lib.dbo.E_InvDetailsWeb78"
            cboInvType.Enabled = True
            pnlAdjust.Visible = True
            Me.Text = "Danh sách hóa đơn Thông tư 78"
        Else
            mstrInvSettingTable = "lib.dbo.E_invSettings"
            mstrInvoiceTable = "lib.dbo.E_Inv"
            mstrInvDetailTable = "lib.dbo.E_InvDetails"
            mstrInvLinkTable = "lib.dbo.E_InvLinks"
            mstrInvWebTable = "lib.dbo.E_InvWeb"
            mstrInvDetailWebTable = "lib.dbo.E_InvDetailsWeb"
            Me.Text = "Danh sách hóa đơn Thông tư 32"
        End If
        LoadCombo(cboTVC, "Select distinct TVC as Value from " & mstrInvSettingTable _
                    & " where Status<>'XX' and City='" & myStaff.City & "' order by TVC", Conn)

        LoadCombo(cboCustGroup, "select Val as Value from Misc where Cat='CustGroupName' and Val1='E'", Conn)
        LoadCombo(cboMauSo, "Select distinct MauSo as value from " & mstrInvoiceTable & " order by MauSo", Conn)
        LoadCombo(cboKyHieu, "Select distinct KyHieu as value from " & mstrInvoiceTable & " order by KyHieu", Conn)
        Clear()
        'ExecuteNonQuerry("Delete " & mstrInvoiceTable & " where Status='XX' and InvoiceNo=0", Conn)
        mblnFirstLoadCompleted = True
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Function RefreshGUI()
        'Return True
        If dgrE_Invoices.CurrentRow Is Nothing Then Return True
        LoadDataGridView(dgrE_InvLinks, "Select * from " & mstrInvLinkTable & " where Status='ok' and InvId=" _
                         & dgrE_Invoices.CurrentRow.Cells("InvId").Value, Conn)
        LoadDataGridView(dgrE_InvDetails, "Select * from " & mstrInvDetailTable & " where Status='ok' and InvId=" _
                         & dgrE_Invoices.CurrentRow.Cells("InvId").Value, Conn)
        If pblnTT78 Then
            With dgrE_Invoices.CurrentRow
                If .Cells("AdjustType").Value <> 0 Then
                    lbkCancel.Visible = False
                Else
                    lbkCancel.Visible = True
                End If
            End With
        Else
        End If
        pnlDraft.Visible = dgrE_Invoices.CurrentRow.Cells("Draft").Value
        pnlDownload.Visible = (dgrE_Invoices.CurrentRow.Cells("InvoiceNo").Value <> 0 Or dgrE_Invoices.CurrentRow.Cells("Draft").Value)
        pnlEdit.Visible = (dgrE_Invoices.CurrentRow.Cells("InvoiceNo").Value = 0)
        lbkViewRelated.Visible = Not dgrE_Invoices.CurrentRow.Cells("Draft").Value
        lbkDownloadXml.Visible = Not dgrE_Invoices.CurrentRow.Cells("Draft").Value
        Return True
    End Function
    Private Function Clear() As Boolean
        If pblnTestInv Then
            cboTVC.SelectedIndex = 0
        ElseIf pblnTT78 Then
            cboTVC.SelectedIndex = 4
        Else
            cboTVC.SelectedIndex = 0
        End If

        cboCustType.SelectedIndex = -1
        cboCustGroup.SelectedIndex = -1
        cboCustomer.SelectedIndex = -1
        cboAL.SelectedIndex = -1
        cboMauSo.SelectedIndex = -1
        cboKyHieu.SelectedIndex = -1
        txtInvoiceNo.Text = ""
        txtInvId.Text = ""
        txtDesc.Text = ""
        txtTkno.Text = ""
        chkDraft.CheckState = CheckState.Indeterminate
        cboBackDate.SelectedIndex = 1
        cboInvType.SelectedIndex = -1
        Return True
    End Function

    Private Function Search() As Boolean
        Dim strQuerry As String
        Dim strFields As String = "Select RecId, InvID,TVC,BIZ,AL,MauSo,KyHieu, InvoiceNo, InvType" _
                                & " , SRV, CONVERT(date,DOI) as DOI, CustShortName" _
                                & ", FOP, Status, Period, PmtDeadline" _
                                & ", CustFullName , Address, TaxCode" _
                                & ",Tax,Charge , F1, F2, F3, F4, F5, CodeTour, NbrOfPax,BU, DomInt, Booker, Buyer,Email, CustId" _
                                & ", FstUser, FstUpdate, LstUser, LstUpdate, City" _
                                & ", GhiNoId, KhachTraId, Service, ClearDate, MauSo, KyHieu,Draft,AdjustType" _
                                & ",MaCoQuanThue,OriFkey,NoOriInv,FakeTkno"


        If Not pblnTT78 Then
            strFields = strFields & ",(100-giamthue) as GiamThue"
        End If

        strQuerry = strFields & " from " & mstrInvoiceTable & " where City='" & myStaff.City & "'"

        AddEqualConditionCombo(strQuerry, cboStatus)
        AddEqualConditionCombo(strQuerry, cboTVC)
        AddEqualConditionCombo(strQuerry, cboBiz)
        AddEqualConditionCombo(strQuerry, cboAL)
        AddEqualConditionCombo(strQuerry, cboMauSo)
        AddEqualConditionCombo(strQuerry, cboKyHieu)
        AddEqualConditionText(strQuerry, txtInvoiceNo)
        AddEqualConditionText(strQuerry, txtInvId)
        AddEqualConditionCheck(strQuerry, chkDraft)
        AddEqualConditionCheck(strQuerry, chkFakeTkno)

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

        If txtDesc.Text <> "" Then
            strQuerry = strQuerry & " and InvId in (select InvId from " & mstrInvDetailTable _
                        & " where Status='OK' and Description like N'%" & txtDesc.Text & "%')"
        End If
        If txtTkno.Text <> "" Then
            strQuerry = strQuerry & " and InvId in (select InvId from " & mstrInvDetailTable _
                        & " where Status='OK' and (Tkno like N'%" & Replace(txtTkno.Text, " ", "") & "%')" _
                        & " or replace(Description,' ','') like N'%" & Replace(txtTkno.Text, " ", "") & "%')"
        End If

        If txtRcpNo.Text <> "" Then
            strQuerry = strQuerry & " and InvId in (select InvId from " & mstrInvLinkTable _
                        & " where Status='OK' and RcpId in " _
                        & " (Select RecId from Rcp where Status='ok' and Rcpno='" & txtRcpNo.Text & "'))"
        End If
        If IsNumeric(cboBackDate.Text) Then
            strQuerry = strQuerry & " and DateDiff(d,FstUpdate,Getdate())<=" & cboBackDate.Text
        End If

        Select Case cboInvType.Text
            Case "Original"
                strQuerry = strQuerry & " and (AdjustType=0 or RecId NOT in (select InvRecId from lib.dbo.E_InvNotice))"
            Case "IsAdjusted"
                strQuerry = strQuerry & " and RecId in (select InvRecId from lib.dbo.E_InvNotice)"
            Case "Adjust"
                strQuerry = strQuerry & " and (AdjustType<>0 or RecId in (select InvRecId from lib.dbo.E_InvNotice where Note<>''))"
            Case "AdjustInfo"
                strQuerry = strQuerry & " and AdjustType=4"
            Case "Adjust+"
                strQuerry = strQuerry & " and AdjustType=2"
            Case "Adjust-"
                strQuerry = strQuerry & " and AdjustType=3"
            Case "AdjustNote"
                strQuerry = strQuerry & " and RecId in in (select InvRecId from lib.dbo.E_InvNotice where Note<>'')"
            Case "ThieuMaCoQuanThue"
                strQuerry = strQuerry & " and MaCoQuanThue=''"
        End Select
        strQuerry = strQuerry & " order by InvId desc"
        LoadDataGridView(dgrE_Invoices, strQuerry, Conn)
        dgrE_Invoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgrE_Invoices.Columns("RecId").Width = 40
        dgrE_Invoices.Columns("InvId").Width = 40
        dgrE_Invoices.Columns("InvType").Width = 40
        dgrE_Invoices.Columns("TVC").Width = 65
        dgrE_Invoices.Columns("Biz").Width = 40
        dgrE_Invoices.Columns("AL").Width = 30
        dgrE_Invoices.Columns("MauSo").Width = 80
        dgrE_Invoices.Columns("KyHieu").Width = 50
        dgrE_Invoices.Columns("InvoiceNo").Width = 50
        dgrE_Invoices.Columns("SRV").Width = 30
        dgrE_Invoices.Columns("DOI").Width = 70
        dgrE_Invoices.Columns("FOP").Width = 50
        dgrE_Invoices.Columns("Address").Width = 130
        dgrE_Invoices.Columns("Status").Width = 40
        If pblnTT78 Then
            dgrE_Invoices.Columns("MaCoQuanThue").Width = 40
            dgrE_Invoices.Columns("MaCoQuanThue").DisplayIndex = 7
        Else
            dgrE_Invoices.Columns("MaCoQuanThue").Visible = False
            dgrE_Invoices.Columns("OriFkey").Visible = False
            dgrE_Invoices.Columns("AdjustType").Visible = False
        End If
        dgrE_Invoices.Columns("InvType").Visible = False
        Me.Text = "E_Invoice List - " & dgrE_Invoices.RowCount & " records"
        Return True
    End Function

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmEdit As New frmE_InvEdit(,,,, cboTVC.Text, True)
Repeat:
        If frmEdit.ShowDialog = DialogResult.OK Then
            If frmEdit.chkKeepNewScreen.Checked Then
                Dim frmNew2 As New frmE_InvEdit
                frmNew2.txtTvc.Text = frmEdit.txtTvc.Text
                frmNew2.txtAL.Text = frmEdit.txtAL.Text
                frmNew2.txtBiz.Text = frmEdit.txtBiz.Text
                frmNew2.txtMauSo.Text = frmEdit.txtMauSo.Text
                frmNew2.txtKyHieu.Text = frmEdit.txtKyHieu.Text
                frmNew2.chkKeepNewScreen.Checked = True
                frmEdit = frmNew2
                GoTo Repeat
            Else
                Search()
            End If

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

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        With dgrE_Invoices.CurrentRow
            Dim frmEdit As New frmE_InvEdit(dgrE_Invoices.CurrentRow, .Cells("AdjustType").Value)
            If frmEdit.ShowDialog = DialogResult.OK Then
                Search()
            End If
        End With

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



    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        If dgrE_Invoices.CurrentRow.Cells("InvoiceNo").Value <> 0 Then
            MsgBox("Delete Record is NOT allowed for Issued Invoice")
            Exit Sub
        ElseIf dgrE_Invoices.CurrentRow.Cells("Draft").Value Then
            MsgBox("You should use DeleteDraft!")
            Exit Sub
        End If

        With dgrE_Invoices.CurrentRow
            Dim lstQuerries As New List(Of String)
            lstQuerries.Add("Delete " & mstrInvoiceTable & " where RecId=" & .Cells("RecId").Value)
            lstQuerries.Add("Delete " & mstrInvDetailTable & " where InvId=" & .Cells("InvId").Value)
            lstQuerries.Add("Delete " & mstrInvLinkTable & " where InvId=" & .Cells("InvId").Value)
            If UpdateListOfQuerries(lstQuerries, Conn) Then
                Search()
            Else
                MsgBox("Unable to delete Invoice Record!")
            End If
        End With

    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Dim blnSuccess As Boolean
        Dim lstQuerries As New List(Of String)
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub

        If dgrE_Invoices.CurrentRow.Cells("InvoiceNo").Value = 0 Then
            MsgBox("Cancel Invoice is NOT allowed for UnIssued Invoice")
            Exit Sub
        End If
        If pblnTT78 AndAlso dgrE_Invoices.CurrentRow.Cells("MaCoQuanThue").Value = "" Then
            MsgBox("Cần lấy MaCoQuanThue trước khi hủy hóa đơn")
            Exit Sub
        End If

        If pblnTT78 AndAlso ScalarToString(mstrInvoiceTable, "top 1 InvId", "OriFkey=" _
                          & dgrE_Invoices.CurrentRow.Cells("InvId").Value) <> "" Then
            MsgBox("Hóa đơn đã bị điều chỉnh, không được hủy!")
            Exit Sub
        End If
        If MsgBox("Có chắc chắn Hủy hóa đơn không?", MsgBoxStyle.YesNo) <> vbYes Then
            Exit Sub
        End If
        With dgrE_Invoices.CurrentRow
            Dim objE_invConnect As New clsE_InvConnect(pblnTT78, .Cells("TVC").Value)
            Dim objE_inv As New clsE_Invoice

            If .Cells("TVC").Value = "123" Then
                If objE_inv.CancelInvoiceWithToken(objE_invConnect.WsUrl, objE_invConnect.UserName _
                            , objE_invConnect.UserPass _
                            , objE_invConnect.AccountName, objE_invConnect.AccountPass _
                            , .Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value) Then
                    MsgBox("E_Inv in Web cancelled!")
                    blnSuccess = True
                Else
                    MsgBox("Unable to Cancel E_Inv in Web!" & vbNewLine & objE_inv.ResponseDesc)
                    Exit Sub
                End If
            Else
                If objE_inv.cancelInv(objE_invConnect.BusinessServiceUrl, objE_invConnect.UserName, objE_invConnect.UserPass, objE_invConnect.AccountName _
                                          , objE_invConnect.AccountPass, .Cells("InvId").Value) Then
                    MsgBox("E_Inv in Web cancelled!")
                    blnSuccess = True
                Else
                    MsgBox("Unable to Cancel E_Inv in Web!")
                    Exit Sub
                End If
            End If

        End With
        If blnSuccess Then
            lstQuerries.Add(ChangeStatus_ByID(mstrInvoiceTable, "XX", dgrE_Invoices.CurrentRow.Cells("RecId").Value))
            lstQuerries.Add(ChangeStatus_ByDK(mstrInvDetailTable, "XX", "InvId=" & dgrE_Invoices.CurrentRow.Cells("InvId").Value))
            lstQuerries.Add(ChangeStatus_ByDK(mstrInvLinkTable, "XX", "InvId=" & dgrE_Invoices.CurrentRow.Cells("InvId").Value))
            If pblnTT78 Then
                lstQuerries.Add("update " & mstrInvWebTable & " set invStatus='XX' where InvId=" & dgrE_Invoices.CurrentRow.Cells("InvId").Value)
                lstQuerries.Add("insert into lib.dbo.E_InvNotice (InvRecId, InvFkey, TVC, MauSo, KyHieu, InvoiceNo,DOI, MaCoQuanThue" _
                                & ",InvType, Action,FstUser,City)" _
                                & " select RecId, InvID, TVC, substring(MauSo,1,1), KyHieu" _
                                & ", InvoiceNo,DOI,MaCoQuanThue,1,'1-Cancel'," _
                                & myStaff.StaffId & ",'" & myStaff.City _
                                & "' from " & mstrInvoiceTable & " where RecId=" & dgrE_Invoices.CurrentRow.Cells("RecId").Value)
            End If
            If UpdateListOfQuerries(lstQuerries, Conn) Then
                Search()
            Else
                MsgBox("Hóa đơn đã xóa trên web VNPT nhưng chưa xóa được trong dữ liệu RAS. Đề nghị thông báo người lập trình RAS")
            End If
        End If

    End Sub

    Private Sub lbkApproveDraft_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkApproveDraft.LinkClicked
        With dgrE_Invoices.CurrentRow
            If ApproveDraftInv(pblnTT78, .Cells("TVC").Value, .Cells("MauSo").Value _
                            , .Cells("KyHieu").Value, .Cells("InvId").Value) Then
                Search()
            End If
        End With

    End Sub

    Private Sub lbkDownloadInv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDownloadXml.LinkClicked
        Dim objE_Inv As New clsE_Invoice

        With dgrE_Invoices.CurrentRow
            Dim objE_InvConnect As New clsE_InvConnect(pblnTT78, .Cells("TVC").Value)
            'If .Cells("Draft").Value Then
            '    If Not objE_Inv.downloadInvFkeyNoPay(objE_InvConnect.PortalServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
            '                               , .Cells("InvId").Value) Then
            '        MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
            '        Exit Sub
            '    End If

            'Else
            Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value)
            If Not objE_Inv.downloadInvNoPay(objE_InvConnect.PortalServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                       , strInvoiceToken) Then
                MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
                Exit Sub
            End If
            'End If

            Dim objContent As New clsE_InvContent
                If pblnTT78 AndAlso objContent.ParseXmlTT78(objE_Inv.ResponseDesc) Then
                    'tiep tuc
                ElseIf Not pblnTT78 AndAlso objContent.ParseXml(objE_Inv.ResponseDesc) Then
                    'tiep tuc
                Else
                    MsgBox("unable to parse E_invoice" & vbNewLine & objE_Inv.ResponseDesc)
                    Exit Sub
                End If
                Dim strPath As String = "D:\Invoice " & .Cells("InvoiceNo").Value & ".xml"
                Append2TextFile(objE_Inv.ResponseDesc, strPath)
            MsgBox(strPath)
        End With
    End Sub

    Private Sub lbkDownloadInvPDFNoPay_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDownloadInvPDFNoPay.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim objE_Inv As New clsE_Invoice

        With dgrE_Invoices.CurrentRow
            Dim objConnect As New clsE_InvConnect(pblnTT78, .Cells("TVC").Value)
            Dim strPath As String = "D:\Invoice " & .Cells("InvoiceNo").Value & ".pdf"

            If .Cells("Draft").Value Then
                If Not objE_Inv.downloadNewInvPDFFkey(objConnect.PortalServiceUrl, objConnect.UserName, objConnect.UserPass _
                                           , .Cells("InvId").Value) Then
                    MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
                    Exit Sub
                End If
            Else
                Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value)
                If Not objE_Inv.downloadInvPDFNoPay(objConnect.PortalServiceUrl, objConnect.UserName, objConnect.UserPass _
                                           , strInvoiceToken) Then
                    MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
                    Exit Sub
                End If
            End If

            If Base64ToFile(objE_Inv.ReponseCode, strPath) Then
                MsgBox(strPath)
            Else
                MsgBox("Unable to save PDF file")
            End If
        End With
    End Sub

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

    Private Sub lbkDownloadHtml_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDownloadHtml.LinkClicked
        Dim objE_Inv As New clsE_Invoice

        With dgrE_Invoices.CurrentRow
            Dim objConnect As New clsE_InvConnect(pblnTT78, .Cells("TVC").Value)


            If .Cells("Draft").Value Then
                If Not objE_Inv.getNewInvViewFkey(objConnect.PortalServiceUrl, objConnect.UserName, objConnect.UserPass _
                                       , .Cells("InvId").Value) Then
                    MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
                    Exit Sub
                End If
            Else
                Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value)
                If Not objE_Inv.getInvViewNoPay(objConnect.PortalServiceUrl, objConnect.UserName, objConnect.UserPass _
                                       , strInvoiceToken) Then
                    MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
                    Exit Sub
                End If
            End If

            Dim strPath As String = "D:\Invoice " & .Cells("InvoiceNo").Value & ".html"
                Dim objLogFile As New System.IO.StreamWriter(strPath)
                objLogFile.WriteLine(objE_Inv.ResponseDesc)
                objLogFile.Close()
                objLogFile = Nothing
                Process.Start(strPath)


        End With
    End Sub

    Private Sub lbkAdjustInfo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdjustInfo.LinkClicked, LinkLabel4.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        If pblnTT78 AndAlso dgrE_Invoices.CurrentRow.Cells("MaCoQuanThue").Value = "" Then
            MsgBox("Cần lấy MaCoQuanThue trước khi hủy hóa đơn")
            Exit Sub
        End If
        Dim frmEdit As New frmE_InvEdit(, InvAdjustType.ChangeInfo, dgrE_Invoices.CurrentRow, False, cboTVC.Text, True)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If

        'Dim objE_InvConnect As New clsE_InvConnect(dgrE_Invoices.CurrentRow.Cells("TVC").Value)
        'Dim objInv As New clsE_Invoice
        'If Not objInv.GetCertInfo(objE_InvConnect.PortalServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass) Then
        '    MsgBox("Không lấy được thông tin Chứng thư số hiện tại")
        '    Exit Sub
        'End If
        'If Not objInv.AdjustReplaceInvWithToken(objE_InvConnect.PortalServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
        '                                        , strStaffAccount As String, strStaffPass As String, intAdjustType) Then

        'End If
    End Sub

    Private Sub lbkAdjustIncrease_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdjustIncrease.LinkClicked, LinkLabel3.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        If pblnTT78 AndAlso dgrE_Invoices.CurrentRow.Cells("MaCoQuanThue").Value = "" Then
            MsgBox("Cần lấy MaCoQuanThue trước khi điều chỉnh hóa đơn")
            Exit Sub
        End If
        Dim frmEdit As New frmE_InvEdit(, InvAdjustType.IncreaseAmt, dgrE_Invoices.CurrentRow _
                                        , False, cboTVC.Text, True)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkAdjustDecrease_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdjustDecrease.LinkClicked, LinkLabel2.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        If pblnTT78 AndAlso dgrE_Invoices.CurrentRow.Cells("MaCoQuanThue").Value = "" Then
            MsgBox("Cần lấy MaCoQuanThue trước khi hủy hóa đơn")
            Exit Sub
        End If
        Dim frmEdit As New frmE_InvEdit(, InvAdjustType.DecreaseAmt, dgrE_Invoices.CurrentRow _
                                        , False, cboTVC.Text, True)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkAdjustInvoiceNote_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdjustInvoiceNote.LinkClicked, LinkLabel1.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        If pblnTT78 AndAlso dgrE_Invoices.CurrentRow.Cells("MaCoQuanThue").Value = "" Then
            MsgBox("Cần lấy MaCoQuanThue trước khi điều chỉnh hóa đơn")
            Exit Sub
        End If

        Dim strNote As String = InputBox("Ghi nội dung điều chỉnh không thay thế hóa đơn")
        Dim objE_Inv As New clsE_Invoice
        Dim intInvType As Integer = 1

        If strNote <> "" Then
            With dgrE_Invoices.CurrentRow
                Dim objConnect As New clsE_InvConnect(pblnTT78, .Cells("TVC").Value)
                Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value)
                If Not objE_Inv.AdjustInvoiceNote(objConnect.BusinessServiceUrl, objConnect.UserName, objConnect.UserPass _
                                           , objConnect.AccountName, objConnect.AccountPass, strNote _
                                           , .Cells("InvId").Value, .Cells("MauSo").Value) Then
                    MsgBox("Unable to Adjust E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
                Else
                    If ExecuteNonQuerry("insert into lib.dbo.E_InvNotice " _
                                & "(InvRecId, InvFkey, TVC, MauSo, KyHieu, InvoiceNo, MaCoQuanThue,DOI" _
                                & ",InvType, Action,FstUser,City,Note)" _
                                & " select RecId, cast(InvID as varchar), TVC, substring(MauSo,1,1)" _
                                & ", KyHieu, InvoiceNo, MaCoQuanThue,DOI," & intInvType & ",'4-Explain'," _
                                & myStaff.StaffId & ",'" & myStaff.City & "',N'" & strNote _
                                & "' from " & mstrInvoiceTable _
                                & " where RecId=" & dgrE_Invoices.CurrentRow.Cells("RecId").Value, Conn) Then
                        MsgBox("Đã cập nhật được điều chỉnh hóa đơn")
                        Search()
                    Else
                        MsgBox("Không cập nhật được điều chỉnh hóa đơn")
                    End If
                End If
            End With
        End If
    End Sub

    Private Sub lbkViewNoPay_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewNoPay.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        With dgrE_Invoices.CurrentRow
            ViewInv(.Cells("TVC").Value, pblnTT78, False, .Cells("MauSo").Value _
                    , .Cells("KyHieu").Value, .Cells("InvoiceNo").Value, .Cells("InvId").Value)
        End With

    End Sub

    Private Sub lbkSyncFromVNPT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSyncFromVNPT.LinkClicked
        Dim frmListWeb As New frmE_InvListWeb
        If frmListWeb.Download(pblnTT78, cboTVC.Text) Then
            frmListWeb.DownloadMaCoQuanThue()
            Search()
        End If

    End Sub

    Private Sub lbkDeleteDraft_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDeleteDraft.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim objE_Inv As New clsE_Invoice

        With dgrE_Invoices.CurrentRow
            If DeleteDraftInv(pblnTT78, .Cells("TVC").Value, .Cells("InvId").Value) Then
                Search()
            End If
        End With

    End Sub

    Private Sub dgrE_Invoices_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrE_Invoices.CellClick
        RefreshGUI()
    End Sub

    Private Sub lbkViewRelated_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewRelated.LinkClicked
        Dim tblRelatedInv As DataTable
        Dim objRelatedInv As DataRow
        Dim blnTt78 As Boolean
        Dim blnNoOriInv As Boolean
        With dgrE_Invoices.CurrentRow
            If .Cells("OriFkey").Value = "" Then
                blnTt78 = pblnTT78
                blnNoOriInv = False
                tblRelatedInv = GetDataTable("Select * from " & mstrInvoiceTable _
                                             & " where OriFkey=" & .Cells("InvId").Value, Conn)
                Select Case tblRelatedInv.Rows.Count
                    Case 0
                        MsgBox("Unable to find Related Invoice!")
                        Exit Sub
                    Case 1
                        objRelatedInv = tblRelatedInv.Rows(0)
                    Case Else
                        Dim frmShow As New frmShowTableContent(tblRelatedInv, "Hãy chọn hóa đơn tương ứng", "RecId")
                        If frmShow.ShowDialog = DialogResult.OK Then
                            objRelatedInv = GetDataTable("Select * from " & mstrInvoiceTable _
                                             & " where RecId=" & frmShow.SelectedValue, Conn).Rows(0)
                        End If
                End Select

            Else
                Dim strInvTable As String
                blnNoOriInv = True
                If .Cells("NoOriInv").Value Then
                    strInvTable = "E_Inv"
                    blnTt78 = False
                Else
                    strInvTable = "E_Inv78"
                    blnTt78 = True
                End If
                tblRelatedInv = GetDataTable("Select * from " & strInvTable _
                                             & " where InvId=" & .Cells("OriFkey").Value, Conn)
                If tblRelatedInv.Rows.Count = 0 Then
                    MsgBox("Unable to find Related Invoice!")
                    Exit Sub
                Else
                    objRelatedInv = tblRelatedInv.Rows(0)
                End If
            End If
            ViewInv(objRelatedInv("TVC"), blnTt78, blnNoOriInv _
                        , objRelatedInv("MauSo"), objRelatedInv("KyHieu") _
                        , objRelatedInv("InvoiceNo"), objRelatedInv("InvId"))
        End With
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Dim objE_Inv As New clsE_Invoice

        With dgrE_Invoices.CurrentRow
            Dim objConnect As New clsE_InvConnect((pblnTT78), .Cells("TVC").Value)

            If objE_Inv.GetInvbyFkey(objConnect.PortalServiceUrl, objConnect.UserName, objConnect.UserPass _
                                           , .Cells("InvId").Value) Then
                MsgBox(objE_Inv.ResponseDesc)
            Else
                MsgBox("Unable to get E Invoice html!" & vbNewLine & objE_Inv.ResponseDesc)

            End If
        End With

    End Sub

    Private Sub lbkReplaceInv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkReplaceInv.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        If pblnTT78 AndAlso dgrE_Invoices.CurrentRow.Cells("MaCoQuanThue").Value = "" Then
            MsgBox("Cần lấy MaCoQuanThue trước khi thay thế hóa đơn")
            Exit Sub
        End If
        Dim frmEdit As New frmE_InvEdit(, InvAdjustType.Replace, dgrE_Invoices.CurrentRow, False, cboTVC.Text, True)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub
End Class