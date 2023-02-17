Public Class frmVendorBalance
    Private Sub frmVendorBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim strQuerryLoadVendor As String = "Select distinct Vendor as Display,VendorId as Value from VendorBalance" _
                    & " where Status='OK' order by Vendor"
        Dim strQuerryLoadAllVendor As String = "Select distinct ShortName as Display,RecId as Value from Vendor" _
                    & " where Status='OK' order by ShortName"
        LoadComboDisplay(cboCutOffVendor, strQuerryLoadAllVendor, Conn)
        LoadComboDisplay(cboVendor, strQuerryLoadVendor, Conn)
        LoadCombo(cboCur, "Select Val as Value from Misc where cat='CURR' order by intVal desc,Val", Conn)
        Clear()
    End Sub
    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search("TVTR")
        Search("GDS")
    End Sub

    Private Function Clear() As Boolean
        cboView.SelectedIndex = 0
        cboVendor.SelectedIndex = -1
        cboAPP.SelectedIndex = -1
        cboFOP.SelectedIndex = 0
        txtTcode.Text = ""
        chkTrxBetween.Checked = False
        dtpFrom.Value = Now.AddDays(-90)
        dtpTo.Value = Now
        Return True
    End Function
    Private Function Search(strTVC As String) As Boolean
        Dim strQuerry As String = String.Empty
        Select Case cboView.Text
            Case "LastBalance"
                strQuerry = "select Vendor,Cur,Sum(Amount) as Amount" _
                    & " from VendorBalance" _
                    & " where status='OK' and TVC='" & strTVC & "'"

            Case "Transactions"
                strQuerry = "Select Vendor, Cur,Amount, FOP, PaymentId, TrxDate, Status, Tcode, FstUser, App" _
                             & " from VendorBalance where TVC='" & strTVC & "'"
        End Select

        AddEqualConditionCombo(strQuerry, cboVendor)
        AddEqualConditionCombo(strQuerry, cboAPP)
        AddLikeConditionText(strQuerry, txtTcode)

        If chkTrxBetween.Checked Then
            strQuerry = strQuerry & " And TrxDate between '" & CreateFromDate(dtpFrom.Value) _
                & "' And '" & CreateToDate(dtpTo.Value) & "'"
        End If

        If cboView.Text = "LastBalance" Then
            strQuerry = strQuerry & " group by Vendor,Cur"
        End If

        strQuerry = strQuerry & " order by Vendor"

        Select Case strTVC
            Case "TVTR"
                LoadDataGridView(dgrVendorBalanceTVTR, strQuerry, Conn)
                dgrVendorBalanceTVTR.Columns("Amount").DefaultCellStyle.Format = "#,##0"
            Case "GDS"
                LoadDataGridView(dgrVendorBalanceGDS, strQuerry, Conn)
                dgrVendorBalanceGDS.Columns("Amount").DefaultCellStyle.Format = "#,##0"
        End Select

        Return True
    End Function

    Private Sub lbkAddCutOff_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd.LinkClicked
        Dim strQuerry As String
        If Not IsNumeric(txtCutOffAmt.Text) Then
            MsgBox("Invalid CutOffAmount!")
            Exit Sub
        ElseIf cboTVC.Text = "" Then
            MsgBox("You must choose TVC!")
            Exit Sub
        End If
        strQuerry = "insert into VendorBalance (VendorID, Vendor, Cur,Amount, FOP, TrxDate" _
                    & ", Status, FstUser, App,City,TVC) values (" & cboCutOffVendor.SelectedValue _
                    & ",'" & cboCutOffVendor.Text & "','" & cboCur.Text _
                    & "'," & CDec(txtCutOffAmt.Text) & ",'" & cboFOP.Text & "','" _
                    & CreateFromDate(dtpCutOffDate.Value) & "','OK','" & myStaff.SICode _
                    & "','RAS','" & myStaff.City & "','" & cboTVC.Text & "')"

        If ExecuteNonQuerry(strQuerry, Conn) Then
            Search(cboTVC.Text)
            txtCutOffAmt.Text = ""
        Else
            MsgBox("Unable to AddCutOffAmount!")
        End If
    End Sub

    Private Sub tpGDS_Enter(sender As Object, e As EventArgs) Handles tpGDS.Enter
        dgrVendorBalanceGDS.Visible = True
        dgrVendorBalanceTVTR.Visible = False
    End Sub

    Private Sub tpTVTR_Enter(sender As Object, e As EventArgs) Handles tpTVTR.Enter
        dgrVendorBalanceGDS.Visible = False
        dgrVendorBalanceTVTR.Visible = True
    End Sub
End Class