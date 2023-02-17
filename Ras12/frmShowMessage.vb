Public Class frmShowMessageInTextBox
    Public Sub New(strTitle As String, strMsg As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = strTitle
        txtMsg.Text = strMsg
    End Sub
End Class