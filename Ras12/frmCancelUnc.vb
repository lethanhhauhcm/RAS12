Public Class frmCancelUnc
    Private Sub frmCancelUnc_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Dim lstQuerries As New List(Of String)

        txtUNC.Text = txtUNC.Text.Trim
        If txtUNC.TextLength < 8 Then
            MsgBox("Wrong UNC number")
            Exit Sub
        End If
        lstQuerries.Add("update UNC_Payments set status='xx' where RefNo='" & txtUNC.Text & "'")
        lstQuerries.Add("update AopQueue set status='xx' where TrxCode='" & txtUNC.Text & "'")
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Cancel Test UNC completed")
        Else
            MsgBox("Cancel Test UNC uncompleted")
        End If
    End Sub
End Class