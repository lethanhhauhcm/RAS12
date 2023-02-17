Public Class frmSupplierList
    Private Sub frmSupplierList_Load(sender As Object, e As EventArgs)
        Clear()
    End Sub
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Sub Search()
        Dim StrSQL As String = "select RecId,FullName, Status, Address, Address_CityCode, Address_CountryCode" _
            & ",Province,District, Tel, Email,Contact,Bill,Payment,RatingType,Rmk" _
            & ", VendorID, FstUser, FstUpdate, LstUser, LstUpdate" _
            & " from Supplier" _
            & " where status<>'XX' and City='" & myStaff.City & "'"

        AddLikeConditionText(StrSQL, txtFullName,,, True)
        AddEqualConditionCombo(StrSQL, cboStatus)
        Select Case chkMappedWzVendor.CheckState
            Case CheckState.Checked
                StrSQL = StrSQL & " and VendorId<>0"
            Case CheckState.Unchecked
                StrSQL = StrSQL & " and VendorId=0"
            Case CheckState.Indeterminate
                'bo qua
        End Select
        If chkLast5CreatedByMyself.Checked Then
            StrSQL = StrSQL & " and RecId in (select top 5 RecId from Supplier where FstUser='" _
                & myStaff.SICode & "')"
        End If
        StrSQL = StrSQL & " order by FullName"
        dgrSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        LoadDataGridView(dgrSuppliers, StrSQL, Conn)
    End Sub
    Private Function Clear() As Boolean
        cboStatus.SelectedIndex = -1
        txtFullName.Text = ""
        chkMappedWzVendor.CheckState = CheckState.Indeterminate
        chkLast5CreatedByMyself.Checked = False
        Return True
    End Function
    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()

    End Sub

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmAdd As New frmSupplierEdit()
        If frmAdd.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        If dgrSuppliers.CurrentRow Is Nothing Then Exit Sub
        Dim frmAdd As New frmSupplierEdit(dgrSuppliers.CurrentRow)
        If frmAdd.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkExpire_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkExpire.LinkClicked
        If dgrSuppliers.CurrentRow Is Nothing Then Exit Sub
        If ExecuteNonQuerry(ChangeStatus_ByID("supplier", "EX", dgrSuppliers.CurrentRow.Cells("RecId").Value), Conn) Then
            Search()
        Else
            MsgBox("Unable to expire Supplier")
        End If
    End Sub
End Class