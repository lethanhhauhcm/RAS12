Public Class frmSelectVatInvNbr
    Private mintSelectInvNbr As Integer

    Private Sub frmSelectVatInvNbr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mintSelectInvNbr = 0
        cboSerialType.SelectedIndex = 0
        txtQty.Text = 1
        cboKyHieu.SelectedIndex = 0
        Search()
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select top 1 RecId,Val as KyHieu, intVal as FromNbr,intVal1 as ToNbr from MISC" _
                                    & " where Cat='VatInvStock' and Status='OK' and City='" & myStaff.City & "'"
        Return LoadDataGridView(dgInvStock, strQuerry, Conn)

    End Function
    Public Property SelectedInvNbr As Integer
        Get
            Return mintSelectInvNbr
        End Get
        Set(value As Integer)
            mintSelectInvNbr = value
        End Set
    End Property

    Private Sub UseFirstNbr_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles UseFirstNbr.LinkClicked
        If dgInvStock.RowCount = 0 Then
            MsgBox("No Invoice Stock available to select")
            Exit Sub
        Else
            mintSelectInvNbr = dgInvStock.CurrentRow.Cells("FromNbr").Value
            Me.DialogResult = DialogResult.OK
        End If
    End Sub

    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd.LinkClicked
        Dim strDupCheck As String = "Select top 1 RecId, intVal as FromNbr,intVal1 as ToNbr from MISC" _
                                    & " where Cat='VatInvStock' and Status='OK' and City='" & myStaff.City _
                                    & "' and Val='" & cboKyHieu.Text & "'"
        Dim strQuerry As String = "Insert into MISC (CAT,Val,IntVal, IntVal1, FstUser, Status, City)" _
                                & " values ('VatInvStock','" & cboKyHieu.Text _
                                & "'," & txtFromNbr.Text & "," & txtToNbr.Text _
                                & ",'" & myStaff.SICode & "','OK','" & myStaff.City & "')"
        Dim tblUsedInvNbr As DataTable

        If Not CheckFormatTextBox(txtFromNbr, True, 1, 7) Or Not CheckFormatTextBox(txtToNbr, True, 1, 7) Then
            Exit Sub
        End If
        If txtFromNbr.Text > txtToNbr.Text Or CInt(txtFromNbr.Text) <> txtFromNbr.Text _
            Or CInt(txtToNbr.Text) <> txtToNbr.Text Then
            MsgBox("Invalid FromNbr/ToNbr")
            Exit Sub
        End If
        tblUsedInvNbr = GetDataTable("Select INVNo,FstUpdate from INV_TVTR where Status='OK'" _
                                     & " and InvNo between " & txtFromNbr.Text & " and " & txtToNbr.Text _
                                     & " and InvSeri='" & cboKyHieu.Text & "'")

        If tblUsedInvNbr.Rows.Count > 0 Then
            Dim frmShowOldInv As New frmShowTableContent(tblUsedInvNbr, "Duplicate with following Invoice Nbrs!")
            frmShowOldInv.ShowDialog()
            Exit Sub
        End If
        If ExecuteNonQuerry(strQuerry, Conn) Then
            Search()
        End If

    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If dgInvStock.RowCount > 0 Then
            Dim strQuerry As String = "update MISC set Status='XX',LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                                    & "' where RecId=" & dgInvStock.CurrentRow.Cells("RecId").Value
            If ExecuteNonQuerry(strQuerry, Conn) Then
                Search()
            End If
        End If
    End Sub

    Private Sub lbkRemove_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRemove.LinkClicked
        Dim intNewFromNbr As Integer
        Dim intNewToNbr As Integer
        If dgInvStock.RowCount = 0 Then
            Exit Sub
        End If
        With dgInvStock.CurrentRow
            Select Case cboSerialType.Text
                Case "First"
                    intNewFromNbr = .Cells("FromNbr").Value + txtQty.Text
                    intNewToNbr = .Cells("ToNbr").Value
                Case "Last"
                    intNewFromNbr = .Cells("FromNbr").Value
                    intNewToNbr = .Cells("ToNbr").Value - txtQty.Text
            End Select
        End With
        Select Case intNewFromNbr
            Case > intNewToNbr
                MsgBox("Invalid number of Invoices to be removed!")
                Exit Sub
            Case intNewToNbr
                Dim strQuerry As String = "update MISC set Status='XX',LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                                    & "' where RecId=" & dgInvStock.CurrentRow.Cells("RecId").Value
                If ExecuteNonQuerry(strQuerry, Conn) Then
                    Search()
                End If
            Case Else
                Dim lstQuerries As New List(Of String)
                lstQuerries.Add("update MISC set Status='XX',LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                                    & "' where RecId=" & dgInvStock.CurrentRow.Cells("RecId").Value)
                lstQuerries.Add("Insert into MISC (CAT, IntVal, IntVal1, FstUser, Status, City)" _
                                & " values ('VatInvStock'," & intNewFromNbr & "," & intNewToNbr _
                                & ",'" & myStaff.SICode & "','OK','" & myStaff.City & "')")
                If UpdateListOfQuerries(lstQuerries, Conn) Then
                    Search()
                Else
                    MsgBox("Unable to change Invoice stock")
                    Exit Sub
                End If
        End Select

    End Sub

End Class