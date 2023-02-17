Public Class frmAcrList
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkReset_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkReset.LinkClicked

    End Sub

    Private Sub frmACRList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo(cboAccountName, "Select Val as Value from MISC where CAT='AcrAccount' and Status='ok'", Conn)
        cboAccountName.SelectedIndex = -1
        cboAL.SelectedIndex = 0
        Search()
    End Sub

    Private Function Search() As Boolean
        Dim strQuerry As String = "Select *" _
                                & " from ACR " _
                                & " where City='" & myStaff.City & "' and Status='OK'"


        AddEqualConditionCombo(strQuerry, cboAL)
        AddEqualConditionCombo(strQuerry, cboAccountName)

        strQuerry = strQuerry & " order by Recid"
        LoadDataGridView(dgrFund, strQuerry, Conn)

    End Function

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        frmAcrAdd.ShowDialog()
    End Sub



End Class