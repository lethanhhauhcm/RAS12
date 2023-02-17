Public Class frmInvSendNotice
    Private mblnFirstLoadCompleted As Boolean
    Private mstrFilter As String = " from lib.dbo.E_InvNotice where city='" & myStaff.City & "'"
    Private Sub frmInvSendNotice_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadCombo(cboTVC, "Select distinct TVC as Value " & mstrFilter & " order by TVC", Conn)
        LoadCombo(cboMauSo, "Select distinct MauSo as value " & mstrFilter & " order by MauSo", Conn)
        LoadCombo(cboKyHieu, "Select distinct KyHieu as value" & mstrFilter & " order by KyHieu", Conn)
        MapMcct()
        Clear()
        mblnFirstLoadCompleted = True
    End Sub
    Private Function MapMcct() As Boolean
        Dim tblInv As DataTable = GetDataTable("select n.*,i.MaCoQuanThue as OriMccq" _
                                               & " from lib.dbo.E_InvNotice n" _
                                               & " left join lib.dbo.E_inv78 i on n.InvRecId=i.RecId" _
                                               & " where n.MaCoQuanThue='' and i.MaCoQuanThue<>''")
        For Each objRow As DataRow In tblInv.Rows
            ExecuteNonQuerry("update lib.dbo.E_InvNotice set MaCoQuanThue='" & objRow("OriMccq") _
                             & "' where RecId=" & objRow("RecId"), Conn)
        Next
        Return True
    End Function
    Private Function Clear() As Boolean
        cboTVC.SelectedIndex = 0
        cboMauSo.SelectedIndex = -1
        cboKyHieu.SelectedIndex = -1
        txtInvoiceNo.Text = ""
        txtInvId.Text = ""
        txtReason.Text = ""
        cboAction.SelectedIndex = -1
        cboStatus.SelectedIndex = -1
        Return True
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select *" & mstrFilter

        AddEqualConditionCombo(strQuerry, cboTVC)
        AddEqualConditionCombo(strQuerry, cboMauSo)
        AddEqualConditionCombo(strQuerry, cboKyHieu)
        AddEqualConditionCombo(strQuerry, cboAction)
        AddEqualConditionText(strQuerry, txtInvoiceNo)
        AddEqualConditionText(strQuerry, txtInvId)
        AddLikeConditionText(strQuerry, txtReason,,, True)

        Select Case cboStatus.Text
            Case "Sent"
                strQuerry = strQuerry & " And SentUser<>''"
            Case "NotSent"
                strQuerry = strQuerry & " and SentUser=''"
        End Select
        strQuerry = strQuerry & " order by RecId"
        LoadDataGridView(dgrE_Invoices, strQuerry, Conn)
        dgrE_Invoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgrE_Invoices.Columns("RecId").Width = 40
        dgrE_Invoices.Columns("InvFkey").Width = 40
        dgrE_Invoices.Columns("InvRecId").Width = 40
        dgrE_Invoices.Columns("InvType").Width = 40
        dgrE_Invoices.Columns("TVC").Width = 65
        dgrE_Invoices.Columns("MauSo").Width = 80
        dgrE_Invoices.Columns("KyHieu").Width = 50
        dgrE_Invoices.Columns("InvoiceNo").Width = 55
        dgrE_Invoices.Columns("FstUser").Width = 48
        dgrE_Invoices.Columns("SentUser").Width = 48
        Return True
    End Function
    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkSendNotice2TaxOffc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSendNotice2TaxOffc.LinkClicked
        Dim lstInvoices As New List(Of DataGridViewRow)
        Dim frmSend As Form
        Dim strPattern As String = String.Empty
        For Each objRow As DataGridViewRow In dgrE_Invoices.Rows
            If objRow.Cells("S").Value Then
                If strPattern = "" Then
                    strPattern = objRow.Cells("MauSo").Value
                ElseIf strPattern <> objRow.Cells("MauSo").Value Then
                    MsgBox("Mỗi thông báo chỉ cho 1 loại Mẫu số Hóa đơn!")
                    Exit Sub
                End If
                If objRow.Cells("MaCoQuanThue").Value = "" AndAlso Not objRow.Cells("NoOriInv").Value Then
                    MsgBox("Thiếu Mã Cơ Quan Thuế cho RecId " & objRow.Cells("RecId").Value)
                    Exit Sub
                End If
                If objRow.Cells("Response").Value <> "" Then
                    MsgBox("Đã từng gửi Cơ Quan Thuế cho RecId " & objRow.Cells("RecId").Value)
                    Exit Sub
                End If
                lstInvoices.Add(objRow)
            End If

        Next
        frmSend = New frmSendNotice2TaxOffc(cboTVC.Text, lstInvoices, Nothing)
        If frmSend.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkRefreshProcessStatus_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRefreshProcessStatus.LinkClicked
        Dim tblResponses As DataTable = GetDataTable("select distinct Response from lib.dbo.E_InvNotice where " _
                                                     & " Response<>'' and ProcessStatus='' and City='" & myStaff.City & "'")
        Dim tblInvoices As DataTable
        For Each objRow As DataRow In tblResponses.Rows
            tblInvoices = GetDataTable("select * from lib.dbo.E_InvNotice where Response='" & objRow("Response") & "'")
            Dim frmSend As New frmSendNotice2TaxOffc(cboTVC.Text, Nothing, tblInvoices)
            If frmSend.ShowDialog = DialogResult.OK Then
                Search()
            End If
        Next
    End Sub

    Private Sub lbkRestoreInvoices_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRestoreInvoices.LinkClicked
        Dim strResponse As String = dgrE_Invoices.CurrentRow.Cells("Response").Value

        If strResponse = "" Then Exit Sub

        Dim objConnect As New clsE_InvConnect(pblnTT78, cboTVC.Text)
        Dim objE_Invoice As New clsE_Invoice

        If objE_Invoice.HandleInvoiceErrors(objConnect.WsUrl, objConnect.UserName, objConnect.UserPass _
                                            , objConnect.AccountName, objConnect.AccountPass, strResponse) Then
            Dim lstQuerries As New List(Of String)
            Dim strStatus As String = objE_Invoice.ReponseCode.Split(":")(1)
            Dim tblInvoices As DataTable = GetDataTable("select * from lib.dbo.E_InvNotice where Response='" & strResponse & "'")
            For Each objRow As DataRow In tblInvoices.Rows
                With objRow
                    lstQuerries.Add("Update lib.dbo.E_InvNotice set ProcessStatus='Deleted'" _
                                    & " where RecId=" & objRow("RecId"))
                End With
            Next
            If UpdateListOfQuerries(lstQuerries, Conn) Then
                Me.DialogResult = DialogResult.OK
            Else
                MsgBox("Đề nghị thông báo cho người lập trình RAS:" & vbNewLine _
                       & "Đã hủy được bản ghi hóa đơn ở Cơ quan thuế nhưng không cập nhật được vào RAS!")
            End If
        Else
            MsgBox(objE_Invoice.ResponseDesc)
        End If
    End Sub

    Private Sub lbkViewOldInv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewOldInv.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        With dgrE_Invoices.CurrentRow
            ViewInv(.Cells("TVC").Value, pblnTT78, False, .Cells("MauSo").Value _
                    , .Cells("KyHieu").Value, .Cells("InvoiceNo").Value, .Cells("InvFkey").Value)
        End With
    End Sub

    Private Sub lbkViewNewInv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewNewInv.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim objNewInv As DataRow
        With dgrE_Invoices.CurrentRow
            objNewInv = GetDataTable("select * from E_inv78 where RecId=" & .Cells("NewInvRecId").Value).Rows(0)
            ViewInv(objNewInv("TVC"), pblnTT78, False, objNewInv("MauSo") _
                    , objNewInv("KyHieu"), objNewInv("InvoiceNo"), objNewInv("InvId"))
        End With
    End Sub
End Class