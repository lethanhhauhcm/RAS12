Public Class frmNhFundList
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkReset_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkReset.LinkClicked
        cboFilter.SelectedIndex = 0
        cboCustomer.SelectedIndex = -1
    End Sub

    Private Sub frmNhFundList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboDisplay(cboCustomer, "Select RecId as Value, CustShortName as Display" _
                         & " from CustomerList where Status='ok' and RecId in" _
                         & "(select CustId from Cust_Detail where Status='OK'" _
                         & " and Cat='Channel' and Val='TA') order by CustShortName", Conn)
        cboCustomer.SelectedIndex = -1
        cboFilter.SelectedIndex = 0
        Search()
    End Sub

    Private Function Search() As Boolean
        Dim strQuerry As String = "Select c.CustShortName, f.*" _
                                & " from NhFund f" _
                                & " left join CustomerList c on f.CustId=c.RecId" _
                                & " where f.Status='OK' and c.Status='OK'"

        If cboCustomer.Text <> "" Then
            strQuerry = strQuerry & " and f.CustId=" & cboCustomer.SelectedValue
        End If

        Select Case cboFilter.Text
            Case "Valid"
                strQuerry = strQuerry & " and ValidTo >='" & Format(Now, "dd MMM yy") & "'"
            Case "NotValid"
                strQuerry = strQuerry & " and ValidTo <'" & Format(Now, "dd MMM yy") & "'"
        End Select

        strQuerry = strQuerry & " order by CustId,FundId,Seq"
        LoadDataGridView(dgrFund, strQuerry, Conn)

    End Function

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        If frmNhFundAdd.ShowDialog = DialogResult.OK Then
            Search()
        End If

    End Sub
End Class