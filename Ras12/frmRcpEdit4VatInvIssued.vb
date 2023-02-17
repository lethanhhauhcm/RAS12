Public Class frmRcpEdit4VatInvIssued
    Public Sub New(intFrontBackOffc As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If intFrontBackOffc = FrontBackOffc.FrontOffice Then
            lbkApprove.Visible = False
            lbkReject.Visible = False
        ElseIf intFrontBackOffc = FrontBackOffc.BackOffice Then
            lbkRequest.Visible = False
        End If
    End Sub

    Private Sub frmRcpEdit4VatInvIssued_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select RecId, strVal1 as RcpNo,intVal as RcpId" _
            & ",Val as Reason,Status,FstUser ,FstUpdate, LstUser,LstUpdate" _
            & " from Lib.Dbo.Misc where Cat='UnlockVatedRcp' and City='" & myStaff.City _
            & "' order by RecId desc"
        LoadDataGridView(dgrRcp, strQuerry, Conn)
        Return True
    End Function

    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRequest.LinkClicked
        txtRcpNo.Text = txtRcpNo.Text.Trim
        Dim strQuerry As String
        Dim tblRcp As DataTable
        Dim strCheckIssuedInv As String = " and (r.Counter in ('TVS','GSA')" _
                    & "or (r.Counter='CWT' and l.RcpId=t.RCPID and l.InvId in " _
                    & "(select invid from lib.dbo.E_Inv78 where status='ok' and MauSo<>'1/001')))"

        If txtRcpNo.TextLength <> 12 Then
            MsgBox("Số Receipt không đúng")
            Exit Sub
        ElseIf txtReason.TextLength < 3 Then
            MsgBox("Cần nhập lý do đầy đủ")
            Exit Sub
        End If
        tblRcp = GetDataTable("select l.*,t.Tkno from lib.dbo.E_InvLinks78 l" _
                        & " left join Rcp r on r.RecId=l.RcpId" _
                        & " left join tkt t on r.RecId=t.RcpId" _
                        & " where l.Status='OK' and r.Status='OK'" _
                        & " and t.Status<>'XX' and t.RcpNo='" & txtRcpNo.Text _
                        & "'" & strCheckIssuedInv _
                        & " And substring(t.tkno,5,4) in" _
                        & "(select Val from lib.dbo.Misc where status='OK' and cat='BspStock')")
        If tblRcp.Rows.Count = 0 Then
            MsgBox("Không có số Receipt thỏa mãn điều kiện cần xin phép sửa")
            Exit Sub
        End If
        strQuerry = "insert into lib.dbo.MISC (CAT, strVal1,IntVal,VAL,Status,FstUser,City) values ('UnlockVatedRcp','" _
            & txtRcpNo.Text & "'," & tblRcp.Rows(0)("RcpId") & ",N'" & txtReason.Text & "','QQ','" _
            & myStaff.SICode & "','" & myStaff.City & "')"
        If ExecuteNonQuerry(strQuerry, Conn) Then
            Search()
        Else
            MsgBox("Không tạo Yêu cầu được " & txtRcpNo.Text)
        End If

    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If dgrRcp.CurrentRow Is Nothing Then Exit Sub

        With dgrRcp.CurrentRow
            If .Cells("Status").Value = "QQ" Then
                If ExecuteNonQuerry(ChangeStatus_ByID("lib.dbo.Misc", "XX", .Cells("RecId").Value), Conn) Then
                    Search()
                Else
                    MsgBox("Lỗi không xóa được bản ghi!")
                End If

            End If
        End With

    End Sub

    Private Sub lbkApprove_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkApprove.LinkClicked
        If dgrRcp.CurrentRow Is Nothing Then Exit Sub

        With dgrRcp.CurrentRow
            If .Cells("Status").Value = "QQ" Then
                If ExecuteNonQuerry(ChangeStatus_ByID("lib.dbo.Misc", "OK", .Cells("RecId").Value), Conn) Then
                    Search()
                Else
                    MsgBox("Lỗi không phê duyệt được bản ghi!")
                End If

            End If
        End With
    End Sub

    Private Sub lbkReject_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkReject.LinkClicked
        If dgrRcp.CurrentRow Is Nothing Then Exit Sub

        With dgrRcp.CurrentRow
            If .Cells("Status").Value = "QQ" Then
                If ExecuteNonQuerry(ChangeStatus_ByID("lib.dbo.Misc", "RJ", .Cells("RecId").Value), Conn) Then
                    Search()
                Else
                    MsgBox("Lỗi không REJECT được bản ghi!")
                End If
            End If
        End With
    End Sub
End Class