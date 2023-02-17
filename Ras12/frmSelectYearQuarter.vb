Public Class frmSelectYearQuarter
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        cboYear.Items.Add(Now.Year - 1)
        cboYear.Items.Add(Now.Year)
        Select Case Now.Month
            Case 11, 12, 1
                cboQuarter.Text = 4
            Case 2, 3, 4
                cboQuarter.Text = 1
            Case 5, 6, 7
                cboQuarter.Text = 2
            Case 8, 9, 10
                cboQuarter.Text = 3
        End Select
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If cboYear.Text = "" Or cboQuarter.Text = "" Then
            MsgBox("You must select Year and Quarter!")
            Exit Sub
        End If
        Me.DialogResult = DialogResult.OK
    End Sub

End Class