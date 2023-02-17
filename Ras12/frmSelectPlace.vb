Public Class frmSelectPlace
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "select ShortName as Value from Vendor where status='OK' and ShortName like '%" & txtPlaceName.Text.Trim & "%'" _
                  & " UNION select Value from cwt.dbo.GO_MiscWzDate where status='ok' and Catergory='NonAirPlace'" _
                  & " and Value Like '%" & txtPlaceName.Text.Trim & "%'"
        LoadDataGridView(dgrPlace, strQuerry, Conn)
    End Function

    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        Me.DialogResult = DialogResult.OK
    End Sub
End Class