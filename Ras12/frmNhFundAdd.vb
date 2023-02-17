Public Class frmNhFundAdd
    Private Sub frmNhFundAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboDisplay(cboCustomer, "Select RecId as Value, CustShortName as Display" _
                         & " from CustomerList where Status='ok' and RecId in" _
                         & "(select CustId from Cust_Detail where Status='OK'" _
                         & " and Cat='Channel' and Val='TA') order by CustShortName", Conn)
        AddFromDatesMonthly(cboValidFrom, 1, 1)
        AddToDatesQuartely(cboValidTo, 4)
    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim strQuerry As String
        If Not CheckInputValues() Then
            Exit Sub
        End If

        strQuerry = "insert into NhFund (FundId, Seq, Cur, Amount, ValidFrom, ValidTo" _
                    & ", CustId, FstUser, Status) Values (" _
                    & "(Select isnull(max(FundId),0)+1 from NhFund),1,'USD'," _
                    & txtAmount.Text & ",'" & cboValidFrom.Text _
                    & "','" & cboValidTo.Text & "'," & cboCustomer.SelectedValue _
                    & ",'" & myStaff.SICode & "','OK')"

        If Not ExecuteNonQuerry(strQuerry, Conn) Then
            MsgBox("Unable to create!")
            Exit Sub
        End If
        Me.DialogResult = DialogResult.OK
        Me.Dispose()
    End Sub
    Private Function CheckInputValues() As Boolean
        Dim intExistingFundId As Integer

        If cboCustomer.Text = "" Then
            MsgBox("Invalid Customer")
            Return False
        End If

        If Not CheckFormatTextBox(txtAmount, True, 1, 5) Then
            Return False
        End If

        If DateDiff(DateInterval.Month, CDate(cboValidFrom.Text), CDate(cboValidTo.Text)) > 12 Then
            MsgBox("Invalid From/To Dates")
            Return False
        End If

        intExistingFundId = ScalarToInt("NhFund", "top 1 FundId", "Status<>'XX' and CustId=" _
                       & cboCustomer.SelectedValue & " and '" & cboValidFrom.Text _
                       & "' between ValidFrom and ValidTo")
        If intExistingFundId > 0 Then
            MsgBox("Duplicate FundId " & intExistingFundId)
            Return False
        End If

        Return True
    End Function
End Class