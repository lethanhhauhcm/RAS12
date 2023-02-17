Public Class frmAlCreditAdd
    Private Sub frmAlCreditAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboDisplay(cboCustomer, "Select RecId as Value, CustShortName as Display" _
                         & " from CustomerList where Status='ok' and RecId in" _
                         & "(select CustId from Cust_Detail where Status='OK'" _
                         & " and Cat='Channel' and Val in ('CS','LC')) order by CustShortName", Conn)
        dtpValidFrom.MinDate = Now.AddDays(-14)
        dtpValidTo.Value = Now.AddDays(360)
        dtpValidTo.MaxDate = Now.AddYears(1)

    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim strQuerry As String
        If Not CheckInputValues() Then
            Exit Sub
        End If

        strQuerry = "insert into AlCredit (AL,VoucherNbr,PaxName,Rloc, Amount, ValidFrom, ValidTo, CustId, FstUser,Status,Remark) Values ('" _
                    & cboAL.Text & "','" & txtVoucherNbr.Text & "','" & txtPaxName.Text & "','" _
                    & txtRLOC.Text & "'," & CDec(txtAmount.Text) & ",'" & CreateFromDate(dtpValidFrom.Value) _
                    & "','" & CreateToDate(dtpValidTo.Value) & "'," & cboCustomer.SelectedValue _
                    & ",'" & myStaff.SICode & "','OK',N'" & txtRemark.Text & "')"

        If Not ExecuteNonQuerry(strQuerry, Conn) Then
            MsgBox("Unable to create!")
            Exit Sub
        End If
        Me.DialogResult = DialogResult.OK
        Me.Dispose()
    End Sub
    Private Function CheckInputValues() As Boolean
        Dim intExistingId As Integer
        If cboAL.Text = "" Then
            MsgBox("You must select Airline!")
            Return False
        End If

        If cboCustomer.Text = "" Then
            MsgBox("Invalid Customer")
            Return False
        End If

        If Not CheckFormatTextBox(txtAmount, True) Then
            Return False
        End If

        Select Case cboAL.Text
            Case "VJ"
                If Not CheckFormatTextBox(txtRLOC,, 6, 6) Then
                    Return False
                End If
                If DateDiff(DateInterval.Day, dtpValidFrom.Value, dtpValidTo.Value) <> 360 Then
                    MsgBox("Invalid Valid From/To Dates")
                    Return False
                End If

                intExistingId = ScalarToInt("AlCredit", "top 1 RecId", "Status<>'XX'" _
                        & " and Rloc='" & txtRLOC.Text & "' and '" & CreateFromDate(dtpValidFrom.Value) _
                        & "' between ValidFrom and ValidTo")
                If intExistingId > 0 Then
                    MsgBox("Duplicate RLOC!")
                    Return False
                End If

            Case "BL"
                If Not CheckFormatTextBox(txtVoucherNbr,, 16, 20) Then
                    Return False
                End If
                If Not CheckFormatTextBox(txtPaxName,, 2, 64) Then
                    Return False
                End If
                If DateDiff(DateInterval.Month, dtpValidFrom.Value, dtpValidTo.Value) <> 6 Then
                    MsgBox("Invalid Valid From/To Dates")
                    Return False
                End If

                intExistingId = ScalarToInt("AlCredit", "top 1 RecId", "Status<>'XX'" _
                        & " and VoucherNbr='" & txtVoucherNbr.Text & "' and '" & CreateFromDate(dtpValidFrom.Value) _
                        & "' between ValidFrom and ValidTo")
                If intExistingId > 0 Then
                    MsgBox("Duplicate VoucherNbr!")
                    Return False
                End If

        End Select


        'If ScalarToInt("Tkt", "RecId", "AL='VJ' and Status<>'XX' and qty=1 and Tkno like '%" & txtRLOC.Text & "%'") = 0 Then
        '    MsgBox("VJ RLOC not FOUND in RAS!")
        '    Return False
        'End If


        Return True
    End Function

    Private Sub dtpValidFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpValidFrom.ValueChanged
        dtpValidTo.Value = dtpValidFrom.Value.AddDays(360)
    End Sub



    Private Sub txtRLOC_Leave(sender As Object, e As EventArgs) Handles txtRLOC.Leave
        Dim decTktAmount As Decimal

        decTktAmount = ScalarToDec("Tkt", "isnull(SUM(NetToAL+Tax),0)", "AL='VJ' and Status<>'XX' and qty=1 and tkno LIKE '%" & txtRLOC.Text & "%'")
        txtAmount.Text = Format(decTktAmount, "#,##0")
    End Sub
End Class