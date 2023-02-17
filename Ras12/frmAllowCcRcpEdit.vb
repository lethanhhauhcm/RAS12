Public Class frmAllowCcRcpEdit
    Private Sub frmAllowCcRcpEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select RecId, strVal1 as RcpNo,intVal as RcpId,Status,FstUpdate as UnlockDate" _
            & ",FstUser as UnlockUser, (case LstUpdate when FstUpdate then null else LstUpdate end) as EditDate" _
            & ",LstUser as EditUser from Lib.Dbo.Misc where Cat='UnlockCcRcp' and City='" & myStaff.City _
            & "'  order by RecId desc"
        LoadDataGridView(dgrRcp, strQuerry, Conn)
        Return True
    End Function

    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd.LinkClicked
        txtRcpNo.Text = txtRcpNo.Text.Trim
        Dim strQuerry As String
        Dim tblRcp As DataTable
        If txtRcpNo.TextLength <> 12 Then
            MsgBox("Số Receipt không đúng")
            Exit Sub
        End If
        tblRcp = GetDataTable("select top 1 * from FOP where FOP='CRD' and Status='OK' and RcpNo='" _
                              & txtRcpNo.Text & "'")
        If tblRcp.Rows.Count = 0 Then
            MsgBox("Không có số Receipt thỏa mãn điều kiện cần cho phép sửa")
            Exit Sub
        End If
        strQuerry = "insert into lib.dbo.MISC (CAT, strVal1,IntVal,Status,FstUser,City) values ('UnlockCcRcp','" _
            & txtRcpNo.Text & "'," & tblRcp.Rows(0)("RcpId") & ",'OK','" & myStaff.SICode _
            & "','" & myStaff.City & "')"
        If ExecuteNonQuerry(strQuerry, Conn) Then
            Search()
        Else
            MsgBox("Không Unlock được " & txtRcpNo.Text)
        End If

    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If dgrRcp.CurrentRow Is Nothing Then Exit Sub

        With dgrRcp.CurrentRow
            If .Cells("Status").Value = "OK" Then
                If ExecuteNonQuerry(ChangeStatus_ByID("lib.dbo.Misc", "XX", .Cells("RecId").Value), Conn) Then
                    Search()
                Else
                    MsgBox("Lỗi không xóa được bản ghi!")
                End If

            End If
        End With

    End Sub
End Class