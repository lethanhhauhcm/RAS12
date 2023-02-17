Public Class frmVendorList
    Private Sub frmVendorList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Clear()

    End Sub
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Sub Search()
        Dim StrSQL As String = "select * from Vendor" _
            & " where status<>'XX' and City='" & myStaff.City & "'"

        AddEqualConditionCombo(StrSQL, cboStatus)
        AddLikeConditionText(StrSQL, txtShortName)
        AddLikeConditionText(StrSQL, txtAccountName)
        AddLikeConditionText(StrSQL, txtBankName)
        AddEqualConditionCombo(StrSQL, cboFOP)
        AddEqualConditionCombo(StrSQL, cboFOP1)

        If cboCAT.Text <> "" Then
            StrSQL = StrSQL & " and Cat='" & Mid(cboCAT.Text, 1, 2) & "'"
        End If

        If chkLast5CreatedByMyself.Checked Then
            StrSQL = StrSQL & " and RecId in (select top 5 RecId from Vendor where FstUser='" _
                & myStaff.SICode & "')"
        End If
        StrSQL = StrSQL & " order by ShortName"
        dgrVendors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        LoadDataGridView(dgrVendors, StrSQL, Conn)
    End Sub
    Private Function Clear() As Boolean
        cboStatus.SelectedIndex = -1
        txtShortName.Text = ""
        txtAccountName.Text = ""
        cboFOP.SelectedIndex = -1
        cboFOP1.SelectedIndex = -1
        cboCAT.SelectedIndex = -1
        chkLast5CreatedByMyself.Checked = False
        Return True
    End Function
    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()

    End Sub

    Private Sub dgrVendors_SelectionChanged(sender As Object, e As EventArgs) Handles dgrVendors.SelectionChanged
        If dgrVendors.CurrentRow Is Nothing Then
            Exit Sub
        End If
        LoadDataGridView(dgrSuppliers, "Select * from Supplier where Status='ok' and VendorId=" _
                         & dgrVendors.CurrentRow.Cells("RecId").Value, Conn)
        Select Case dgrVendors.CurrentRow.Cells("Status").Value
            Case "OK"
                lbkExpire.Visible = True
                lbkReActivate.Visible = False
            Case "EX"
                lbkExpire.Visible = False
                lbkReActivate.Visible = True
        End Select
        LoadBankBranch()
    End Sub
    Private Function LoadBankBranch() As Boolean
        LoadDataGridView(dgrBankBranch, "Select m.RecId,i.UsedBy, b.BankID, b.Province, b.BankProvince, b.Branch " _
                            & " from lib.dbo.BankBranches b left join lib.dbo.BankIDs i on b.BankId=i.RecId " _
                            & " left join lib.dbo.BankMap m on m.BranchId=b.RecId" _
                            & " where b.Status='OK' and i.Status='OK' and m.Status='OK'  and m.VendorId=" & dgrVendors.CurrentRow.Cells("RecId").Value, Conn)
    End Function
    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmAdd As New frmVendorEdit()
        If frmAdd.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        If dgrVendors.CurrentRow Is Nothing Then Exit Sub
        Dim frmAdd As New frmVendorEdit(dgrVendors.CurrentRow)
        If frmAdd.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkExpire_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkExpire.LinkClicked
        Dim mLstSQL As New List(Of String)  '^_^20220915 add by 7643

        If dgrVendors.CurrentRow Is Nothing Then Exit Sub
        'If ExecuteNonQuerry(ChangeStatus_ByID("Vendor", "EX", dgrVendors.CurrentRow.Cells("RecId").Value), Conn) Then  '^_^20220915 mark by 7643
        '^_^20220915 modi by 7643 -b-
        mLstSQL.Add(ChangeStatus_ByDK("MISC", "EX", "IntVal=" & dgrVendors.CurrentRow.Cells("RecId").Value & " and Status='OK' and CAT='VendorUpdateNoBTF'"))
        mLstSQL.Add(ChangeStatus_ByID("Vendor", "EX", dgrVendors.CurrentRow.Cells("RecId").Value))
        If UpdateListOfQuerries(mLstSQL, Conn) Then
            '^_^20220915 modi by 7643 -e-
            Search()
        Else
            MsgBox("Unable to expire Vendor")
        End If
    End Sub

    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub lbkAddSupplier_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddSupplier.LinkClicked
        If dgrVendors.CurrentRow Is Nothing Then Exit Sub
        With dgrVendors.CurrentRow
            Dim frmAdd As New frmSupplierEdit()
            frmAdd.txtVendorId.Text = .Cells("RecId").Value
            frmAdd.txtVendor.Text = ScalarToString("Vendor", "ShortName", "RecId=" & .Cells("RecId").Value)
            frmAdd.txtFullName.Text = .Cells("ShortName").Value
            frmAdd.txtEmail.Text = .Cells("Email").Value
            frmAdd.txtPhone.Text = .Cells("Phone").Value
            frmAdd.txtAddress.Text = .Cells("Address").Value
            If frmAdd.ShowDialog = DialogResult.OK Then
                LoadDataGridView(dgrSuppliers, "Select * from Supplier where Status='ok' and VendorId=" _
                                         & dgrVendors.CurrentRow.Cells("RecId").Value, Conn)
            End If
        End With



    End Sub

    Private Sub lbkReActivate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkReActivate.LinkClicked
        Dim mLstSQL As New List(Of String)  '^_^20220915 add by 7643

        If dgrVendors.CurrentRow Is Nothing Then Exit Sub
        'If ExecuteNonQuerry(ChangeStatus_ByID("Vendor", "OK", dgrVendors.CurrentRow.Cells("RecId").Value), Conn) Then  '^_^20220915 mark by 7643
        '^_^20220915 modi by 7643 -b-
        mLstSQL.Add(ChangeStatus_ByDK("MISC", "OK", "IntVal=" & dgrVendors.CurrentRow.Cells("RecId").Value & " and Status='EX' and CAT='VendorUpdateNoBTF'"))
        mLstSQL.Add(ChangeStatus_ByID("Vendor", "OK", dgrVendors.CurrentRow.Cells("RecId").Value))
        If UpdateListOfQuerries(mLstSQL, Conn) Then
            '^_^20220915 modi by 7643 -e-
            Search()
        Else
            MsgBox("Unable to ReActivate Vendor")
        End If
    End Sub

    Private Sub lbkMapBank_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkMapBank.LinkClicked
        If dgrVendors.CurrentRow Is Nothing Then Exit Sub
        Dim frmSelect As New frmMapVendorBankBranch("TCB", dgrVendors.CurrentRow.Cells("RecId").Value)
        If frmSelect.ShowDialog = DialogResult.OK Then
            LoadBankBranch()
        End If
    End Sub

    Private Sub lbkUnMap_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUnMap.LinkClicked
        If dgrVendors.CurrentRow Is Nothing Then Exit Sub
        Dim strQuerry As String = "update lib.dbo.BankMap set Status='XX', LstUpdate=getdate(),LstUser='" & myStaff.SICode & "' where RecId=" _
            & dgrBankBranch.CurrentRow.Cells("RecId").Value
        If ExecuteNonQuerry(strQuerry, Conn) Then
            LoadBankBranch()
        End If
    End Sub
End Class