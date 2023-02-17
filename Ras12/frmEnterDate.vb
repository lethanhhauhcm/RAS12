Public Class frmEnterDate
    Public FFmt, Flbl01 As String
    Public FDate As Date

    Private Sub frmEnterDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = FFmt
        lbl01.Text = Flbl01
    End Sub

    Private Sub frmEnterDate_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        FDate = dtp01.Value
    End Sub

End Class