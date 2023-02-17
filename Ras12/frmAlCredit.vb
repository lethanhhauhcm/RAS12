Public Class frmAlCredit
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkReset_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkReset.LinkClicked
        cboFilter.SelectedIndex = 0
        cboCustomer.SelectedIndex = -1
    End Sub

    Private Sub frmAlCreditList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboDisplay(cboCustomer, "Select RecId as Value, CustShortName as Display" _
                         & " from CustomerList where Status='ok' and RecId in" _
                         & "(select distinct CustId from AlCredit where Status='OK') order by CustShortName", Conn)

        cboCustomer.SelectedIndex = -1
        cboFilter.SelectedIndex = 0
        Search()
    End Sub

    Private Function Search() As Boolean
        Dim strQuerry As String = "Select c.CustShortName, f.*" _
                                & " from AlCredit f" _
                                & " left join CustomerList c on f.CustId=c.RecId" _
                                & " where f.Status='OK' and c.Status='OK'"

        If cboCustomer.Text <> "" Then
            strQuerry = strQuerry & " and f.CustId=" & cboCustomer.SelectedValue
        End If

        AddEqualConditionCombo(strQuerry, cboAL)
        Select Case cboFilter.Text
            Case "Valid"
                strQuerry = strQuerry & " and ValidTo >='" & Format(Now, "dd MMM yy") & "'"
            Case "NotValid"
                strQuerry = strQuerry & " and ValidTo <'" & Format(Now, "dd MMM yy") & "'"
        End Select

        strQuerry = strQuerry & " order by CustId"
        LoadDataGridView(dgrFund, strQuerry, Conn)

    End Function

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        If frmAlCreditAdd.ShowDialog = DialogResult.OK Then
            Search()
        End If

    End Sub

    Private Sub lbkExpire_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkExpire.LinkClicked

    End Sub
End Class