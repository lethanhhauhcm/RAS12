Public Class frmSearchProvince
    Private mstrAddress As String
    Private mstrUsedBy As String
    Private mstrSelectedProvince As String

    Public Sub New(strAddress As String, strUsedBy As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mstrAddress = strAddress
        mstrUsedBy = strUsedBy
    End Sub

    Private Sub frmSearchProvince_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
    End Sub
    Private Function Search() As Boolean
        Return True
    End Function
End Class