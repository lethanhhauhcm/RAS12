Public Class frmShowTextBoxes
    Public Sub New(lstTexts As List(Of String))
        Dim i As Integer
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For Each strText As String In lstTexts
            Dim objTextbox As New TextBox
            'objTextbox.Top = Me.Top - 100 * (i + 1)
            objTextbox.Width = strText.Length * 8
            objTextbox.Text = strText
            flpText.Controls.Add(objTextbox)
            i = i + 1
        Next
    End Sub
End Class