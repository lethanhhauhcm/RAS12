Public Class frmVendorGroups
    Private mblnFirstLoadCompleted As Boolean
    Private Sub frmVendorGroups_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGroups()
        mblnFirstLoadCompleted = True
    End Sub

    Private Function LoadGroups() As Boolean
        Dim strQuerry As String = "select RecId,Val as GroupName,Status from Misc where Cat='VendorGroupName'" _
                                & " order by Val"

        LoadDataGridView(dgrVendorGroups, strQuerry, Conn)
        dgrVendorGroups.Columns("RecId").Visible = False
        Return True
    End Function
    Private Function LoadVendorInGroups(strGroupName As String) As Boolean
        Dim strQuerry As String = "select RecId, Val1 as VendorShortName,Status,intVal as VendorId" _
                                & " from Misc where Cat='VendorNameInGroup'" _
                                & "  and Status='OK' and Val='" & strGroupName _
                                & "' order by Val1"
        dgrShortNames.DataSource = Nothing
        LoadDataGridView(dgrShortNames, strQuerry, Conn)
        dgrShortNames.Columns("RecId").Visible = False
        If dgrShortNames.RowCount > 0 Then
            dgrShortNames.Rows(0).Selected = True
        End If
        'dgrShortNames.DataSource = Nothing
    End Function




    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd.LinkClicked
        If txtGroupName.Text <> "" Then
            If ExecuteNonQuerry("insert Misc (Cat,Status,Val,FstUser) values ('VendorGroupName','OK','" _
                                & txtGroupName.Text & "','" & myStaff.SICode & "')", Conn) Then
                LoadGroups()
            Else
                MsgBox("Unable to Add Vendor Group Name")
            End If
        End If
    End Sub

    Private Sub lbkSaveNewName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSaveNewName.LinkClicked
        If txtGroupName.Text <> "" Then
            Dim lstQuerries As New List(Of String)
            lstQuerries.Add("insert Misc (Cat,Status,Val,FstUser) values ('VendorGroupName','OK','" _
                                & txtGroupName.Text & "','" & myStaff.SICode & "')")
            For Each objRow As DataGridViewRow In dgrShortNames.Rows
                lstQuerries.Add("insert Misc (Cat,Status,Val,Val1,FstUser) values ('VendorNameInGroup','OK','" _
                                & txtGroupName.Text & "','" & objRow.Cells("VendorShortName").Value _
                                & "','" & myStaff.SICode & "')")
            Next

            If UpdateListOfQuerries(lstQuerries, Conn) Then
                LoadGroups()
            Else
                MsgBox("Unable to Edit Vendor Group Name")
            End If
        End If
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        LoadVendors()
    End Sub
    Private Function LoadVendors() As Boolean
        Dim strQuerries As String = "Select RecId, ShortName,AccountName" _
            & " from Vendor where Status='OK' and City='" & myStaff.City & "'"
        AddLikeConditionText(strQuerries, txtShortName)
        LoadDataGridView(dgrVendorList, strQuerries, Conn)
    End Function

    Private Sub lblAddVendor_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblAddVendor.LinkClicked
        If dgrVendorList.CurrentRow Is Nothing Then
            MsgBox("You must select Vendor first!")
            Exit Sub
        End If
        Dim strQuerry As String = "insert Misc (Cat,Status,Val,Val1,intVal,FstUser) values ('VendorNameInGroup','OK','" _
            & dgrVendorGroups.CurrentRow.Cells("GroupName").Value _
            & "','" & dgrVendorList.CurrentRow.Cells("ShortName").Value _
            & "'," & dgrVendorList.CurrentRow.Cells("RecId").Value & ",'" & myStaff.SICode & "')"
        If ExecuteNonQuerry(strQuerry, Conn) Then
            LoadVendorInGroups(dgrVendorGroups.CurrentRow.Cells("GroupName").Value)
        End If
    End Sub

    Private Sub lbkRemove_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRemove.LinkClicked
        Dim strQuerry As String = ChangeStatus_ByID("Misc", "XX", dgrShortNames.CurrentRow.Cells("RecId").Value)
        If ExecuteNonQuerry(strQuerry, Conn) Then
            dgrShortNames.Rows.Remove(dgrShortNames.CurrentRow)
        End If
    End Sub

    Private Sub dgVendorGroups_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrVendorGroups.CellContentClick

    End Sub

    Private Sub lbkExpire_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkExpire.LinkClicked
        Dim strQuerry As String = ChangeStatus_ByID("Misc", "EX", dgrVendorGroups.CurrentRow.Cells("RecId").Value)
        If ExecuteNonQuerry(strQuerry, Conn) Then
            dgrVendorGroups.Rows.Remove(dgrVendorGroups.CurrentRow)
        End If
    End Sub

    Private Sub dgrVendorGroups_SelectionChanged(sender As Object, e As EventArgs) Handles dgrVendorGroups.SelectionChanged
        If dgrVendorGroups.CurrentRow Is Nothing Then Exit Sub
        If mblnFirstLoadCompleted Then
            LoadVendorInGroups(dgrVendorGroups.CurrentRow.Cells("GroupName").Value)
        End If
    End Sub
End Class