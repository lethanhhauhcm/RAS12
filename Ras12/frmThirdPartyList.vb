Public Class frmThirdPartyList
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "select * from lib.dbo.ThirdParty where Status='OK' and City='" _
            & myStaff.City & "' and Counter='" & myStaff.Counter & "'"
        AddEqualConditionCombo(strQuerry, cboCAT)
        AddLikeConditionText(strQuerry, txtFullName)
        strQuerry = strQuerry & " order by CAT,FullName"
        LoadDataGridView(dgrThirdParties, strQuerry, Conn)
        Return True
    End Function
    Private Function Clear() As Boolean
        cboCAT.SelectedIndex = -1
        txtFullName.Text = ""
    End Function

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmThirdParty As New frmThirdPartyEdit(0, False)
        If frmThirdParty.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        If dgrThirdParties.CurrentRow Is Nothing Then Exit Sub
        Dim frmThirdParty As New frmThirdPartyEdit(dgrThirdParties.CurrentRow.Cells("RecId").Value, False)
        If frmThirdParty.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkClone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone.LinkClicked
        If dgrThirdParties.CurrentRow Is Nothing Then Exit Sub
        Dim frmThirdParty As New frmThirdPartyEdit(dgrThirdParties.CurrentRow.Cells("RecId").Value, True)
        If frmThirdParty.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If dgrThirdParties.CurrentRow Is Nothing Then Exit Sub
        Dim intUsageCount As Integer = ScalarToInt("Rcp", "count (*)", "Status='OK' and KickbackPartyId=" _
                                                   & dgrThirdParties.CurrentRow.Cells("RecId").Value _
                                                   & " or MiscFeePartyId=" & dgrThirdParties.CurrentRow.Cells("RecId").Value)
        If intUsageCount > 0 Then
            MsgBox("You are NOT allowed to delete this ThirdParty! It is used by " & intUsageCount & " Receipt")
        ElseIf ExecuteNonQuerry(ChangeStatus_ByID("lib.dbo.ThirdParty", "XX", dgrThirdParties.CurrentRow.Cells("RecId").Value), Conn) Then
            Search()
        Else
            MsgBox("Unable to delete ThirdParty!")
        End If
    End Sub
End Class