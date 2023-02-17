Public Class frmCustomerGroups
    Private mblnFirstLoadCompleted As Boolean
    Private Sub frmCustomerGroups_Load(sender As Object, e As EventArgs) Handles MyBase.Load
               LoadGroups()
        mblnFirstLoadCompleted = True
    End Sub

    Private Function LoadGroups() As Boolean
        Dim strQuerry As String = "select RecId,Val as GroupName,Status from Misc where Cat='CustGroupName'" _
                                & " order by Val"

        LoadDataGridView(dgCustGroups, strQuerry, Conn)
        dgCustGroups.Columns("RecId").Visible = False
        Return True
    End Function
    Private Function LoadCustomerInGroups(strGroupName As String) As Boolean
        Dim strQuerry As String = "select RecId, Val1 as CustShortName,Status,intVal as CustId" _
                                & " from Misc where Cat='CustNameInGroup'" _
                                & "  and Status='OK' and Val='" & strGroupName _
                                & "' order by Val1"
        dgrShortNames.DataSource = Nothing
        dgrShortNames.Rows.Clear()
        LoadDataGridView(dgrShortNames, strQuerry, Conn)
        dgrShortNames.Columns("RecId").Visible = False
        If dgrShortNames.RowCount > 0 Then
            dgrShortNames.Rows(0).Selected = True
        End If
        'dgrShortNames.DataSource = Nothing
    End Function


    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd.LinkClicked
        If txtGroupName.Text <> "" Then
            If ExecuteNonQuerry("insert Misc (Cat,Status,Val,FstUser) values ('CustGroupName','OK','" _
                                & txtGroupName.Text & "','" & myStaff.SICode & "')", Conn) Then
                LoadGroups()
            Else
                MsgBox("Unable to Add Customer Group Name")
            End If
        End If
    End Sub

    Private Sub lbkSaveNewName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSaveNewName.LinkClicked
        If txtGroupName.Text <> "" Then
            Dim lstQuerries As New List(Of String)
            lstQuerries.Add("insert Misc (Cat,Status,Val,FstUser) values ('CustGroupName','OK','" _
                                & txtGroupName.Text & "','" & myStaff.SICode & "')")
            For Each objRow As DataGridViewRow In dgrShortNames.Rows
                lstQuerries.Add("insert Misc (Cat,Status,Val,Val1,FstUser) values ('CustNameInGroup','OK','" _
                                & txtGroupName.Text & "','" & objRow.Cells("CustShortName").Value _
                                & "','" & myStaff.SICode & "')")
            Next

            If UpdateListOfQuerries(lstQuerries, Conn) Then
                LoadGroups()
            Else
                MsgBox("Unable to Edit Customer Group Name")
            End If
        End If
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        LoadCustomers()
    End Sub
    Private Function LoadCustomers() As Boolean
        Dim strQuerries As String = "Select RecId, CustShortName,CustFullName" _
            & " from CustomerList where Status='OK' and City='" & myStaff.City & "'"
        AddLikeConditionText(strQuerries, txtCustShortName)
        LoadDataGridView(dgrCustomerList, strQuerries, Conn)
    End Function

    Private Sub lblAddCust_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblAddCust.LinkClicked
        If dgrCustomerList.CurrentRow Is Nothing Then
            MsgBox("You must select Customer first!")
            Exit Sub
        End If
        Dim strQuerry As String = "insert Misc (Cat,Status,Val,Val1,intVal,FstUser) values ('CustNameInGroup','OK','" _
            & dgCustGroups.CurrentRow.Cells("GroupName").Value _
            & "','" & dgrCustomerList.CurrentRow.Cells("CustShortName").Value _
            & "'," & dgrCustomerList.CurrentRow.Cells("RecId").Value & ",'" & myStaff.SICode & "')"
        If ExecuteNonQuerry(strQuerry, Conn) Then
            LoadCustomerInGroups(dgCustGroups.CurrentRow.Cells("GroupName").Value)
        End If
    End Sub

    Private Sub lbkRemove_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRemove.LinkClicked
        Dim strQuerry As String = ChangeStatus_ByID("Misc", "XX", dgrShortNames.CurrentRow.Cells("RecId").Value)
        If ExecuteNonQuerry(strQuerry, Conn) Then
            dgrShortNames.Rows.Remove(dgrShortNames.CurrentRow)
        End If
    End Sub

    Private Sub dgCustGroups_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCustGroups.CellContentClick

    End Sub

    Private Sub lbkExpire_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkExpire.LinkClicked
        Dim strQuerry As String = ChangeStatus_ByID("Misc", "EX", dgCustGroups.CurrentRow.Cells("RecId").Value)
        If ExecuteNonQuerry(strQuerry, Conn) Then
            dgCustGroups.Rows.Remove(dgCustGroups.CurrentRow)
        End If
    End Sub

    Private Sub dgCustGroups_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCustGroups.CellClick
        If mblnFirstLoadCompleted Then
            LoadCustomerInGroups(dgCustGroups.CurrentRow.Cells("GroupName").Value)
        End If
    End Sub
End Class