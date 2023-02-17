Public Class frmExtraRights
    Private mblnFirstLoadCompleted As Boolean

    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd.LinkClicked
        Dim strQuerry As String = "insert tblRight (SiCode,Object,SubObject,FstUser) values ('" _
            & txtSignIn.Text & "','ExtraRights','" & cmbExtraRights.Text & "','" & myStaff.SICode & "')"

        If cmbExtraRights.Text = "" Then
            MsgBox("You must select Right")
            Exit Sub
        End If
        If Not (txtSignIn.TextLength >= 3 AndAlso ScalarToInt("tblUser", "RecId", "Status<>'xx' and SiCode='" & txtSignIn.Text & "'") > 0) Then
            MsgBox("Sign In Not Found")
            Exit Sub
        End If

        If ExecuteNonQuerry(strQuerry, Conn) Then
            txtSignIn.Text = ""
            Search()
        Else
            MsgBox("Unable to Add ExtraRight")
        End If
    End Sub

    Private Sub frmExtraRights_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbExtraRights.SelectedIndex = 0
        Search()
        mblnFirstLoadCompleted = True
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select r.RecId, r.SiCode,u.SiName,r.SubObject as ExtraRights" _
            & ", r.Fstuser, r.LstUser, r.FstUpdate, r.Lstupdate" _
            & " from tblRight r" _
            & " left join tblUser u on r.SiCode=u.SiCode" _
            & " where Object='ExtraRights' and r.Status<>'XX'" _
            & " and u.Status<>'XX'"

        AddEqualConditionCombo(strQuerry, cmbExtraRights, "SubObject")

        strQuerry = strQuerry & " ORDER BY r.SiCode,SubObject"

        Return LoadDataGridView(dgrExtraRights, strQuerry, Conn)
    End Function

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If DeleteGridViewRow(dgrExtraRights, "Update tblRight set Status='XX',LstUpdate=Getdate()" _
                             & ",LstUser='" & myStaff.SICode & "' where RecId=" _
                             & dgrExtraRights.CurrentRow.Cells("RecId").Value, Conn) Then
            Search()
        End If
    End Sub

    Private Sub cmbExtraRights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbExtraRights.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub
End Class