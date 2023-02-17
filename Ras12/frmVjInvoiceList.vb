Public Class frmVjInvoiceList
    Private Sub frmVjInvoiceList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboDisplay(cboCustShortName, "Select Val1 as Display,Val as value from [MISC] where CAT='VjAccount' and Status='ok' ", Conn)
        Clear()

    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Function Clear() As Boolean
        cboStatus.SelectedIndex = 0
        cboCustShortName.SelectedIndex = -1
        txtRloc.Text = ""
        txtPaxName.Text = ""
        txtInvoiceNo.Text = ""
        chkHasInvoice.CheckState = CheckState.Indeterminate
        dtpFrom.Value = Now.AddDays(-8)
        dtpTo.Value = Now.AddDays(-1)
        Return True
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String = "select i.RecId,m.Val1 as CustShortName,i.InvoiceNo, Amount, Rloc, PaxName, PaidDate, i.Details" _
                & ", AgentDetails, SignIn, AgentCode, i.Status, i.FstUser, i.FstUpdate, i.LstUser, i.LstUpdate" _
                & " from VjInvoice i left join Misc m on m.Cat='VjAccount' and m.Status='ok' and m.Val=i.AgentCode" _
                & " where PaidDate between '" & CreateFromDate(dtpFrom.Value) & "' and '" & dtpTo.Value & "'"

        AddEqualConditionCombo(strQuerry, cboStatus, "i.Status")
        AddEqualConditionCombo(strQuerry, cboCustShortName, "m.Val1")
        AddLikeConditionText(strQuerry, txtRloc)
        AddLikeConditionText(strQuerry, txtPaxName)

        Select Case chkHasInvoice.CheckState
            Case CheckState.Checked
                strQuerry = strQuerry & " and InvoiceNo<>''"
            Case CheckState.Unchecked
                strQuerry = strQuerry & " and InvoiceNo=''"
            Case CheckState.Indeterminate
                'bo qua
        End Select
        strQuerry = strQuerry & " order by PaidDate,m.Val1"
        LoadDataGridView(dgrInvoices, strQuerry, Conn)
        With dgrInvoices
            .Columns("Amount").DefaultCellStyle.Format = "#,##0"
        End With
        Return True
    End Function

    Private Sub lblImportExcel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblImportExcel.LinkClicked
        Dim objOfd As New OpenFileDialog
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
        Dim i As Integer
        Dim lstQuerries As New List(Of String)
        Dim strPaidDate As String

        With objOfd
            .Filter = "excel files (*.xls)|"
            .ShowDialog()
            If .FileName = "" Then
                Exit Sub
            End If
            If ScalarToInt("Misc", "RecId", "Status='OK' and CAT='VjSaleFile' and Val='" & .FileName & "'") > 0 Then
                MsgBox("This file name had been imported in the past!")
                objExcel.Quit()
                Exit Sub
            Else
                lstQuerries.Add("insert into Misc (CAT,Val,Status,FstUser) values ('VjSaleFile',N'" & .FileName & "','OK','" & myStaff.SICode & "')")
            End If
            objWbk = objExcel.Workbooks.Open(.FileName, , True)
            objWsh = objWbk.ActiveSheet
            With objWsh
                For i = 2 To 64000 Step 1
                    If .Range("B" & i).Value = "" Then
                        Exit For
                    Else
                        strPaidDate = CreateFromDate(CDate(.Range("D" & i).Value))
                        lstQuerries.Add("insert into VjInvoice (Rloc, PaxName, PaidDate, Details, AgentDetails, SignIn, AgentCode" _
                                        & ", Amount, Status, FstUser) values ('" & .Range("b" & i).Value & "','" & .Range("c" & i).Value _
                                        & "','" & strPaidDate & "','" & .Range("e" & i).Value & "','" & .Range("f" & i).Value _
                                        & "','" & .Range("g" & i).Value & "','" & .Range("h" & i).Value & "'," & .Range("j" & i).Value _
                                        & ",'OK','" & myStaff.SICode & "')")
                    End If
                Next
            End With
            If UpdateListOfQuerries(lstQuerries, Conn) Then
                MsgBox("Upload completed for " & lstQuerries.Count & " records!")
            Else
                MsgBox("Unable to upload !")
            End If
        End With
        objExcel.Quit()
    End Sub

    Private Sub lbkUpdate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUpdate.LinkClicked
        If dgrInvoices.CurrentRow Is Nothing Then Exit Sub
        If Not IsNumeric(txtNewInvNo.Text) Then
            MsgBox("Invalid Invoice No")
            Exit Sub
        End If
        With dgrInvoices.CurrentRow
            If .Cells("InvoiceNo").Value = "" Then
                If ExecuteNonQuerry("Update VjInvoice set InvoiceNo='" & txtNewInvNo.Text.Trim & "',LstUser='" & myStaff.SICode _
                                        & "' where RecId=" & .Cells("RecId").Value, Conn) Then
                    txtNewInvNo.Text = ""
                Else
                    MsgBox("Unable to update InvoiceNo!")
                End If
            Else
                Dim lstQuerries As New List(Of String)
                lstQuerries.Add(ChangeStatus_ByID("VjInvoice", "XX", .Cells("RecId").Value))
                lstQuerries.Add("insert VjInvoice (Rloc, PaxName, PaidDate, Details, AgentDetails, SignIn, AgentCode, Amount, Status,InvoiceNo,FstUser)" _
                                & " Select Rloc, PaxName, PaidDate, Details, AgentDetails, SignIn, AgentCode, Amount, 'OK'," _
                                & txtNewInvNo.Text & ",'" & myStaff.SICode & "'" _
                                & " from VjInvoice where RecId=" & .Cells("RecId").Value)
                If UpdateListOfQuerries(lstQuerries, Conn) Then
                    txtNewInvNo.Text = ""
                Else
                    MsgBox("Unable to update InvoiceNo!")
                End If
            End If
        End With
        Search()
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged
        If cboStatus.Text = "XX" Then
            lbkUpdate.Visible = False
        Else
            lbkUpdate.Visible = True
        End If
    End Sub
End Class