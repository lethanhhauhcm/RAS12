Imports System.IO

Public Class frmSelectFile
    Private mintVendorId As Integer
    Public Sub New(intDutoanId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadComboDisplay(cboVendor, "Select distinct Vendor as Display,VendorId as Value from Dutoan_Item" _
        & " where Status='OK' and VendorId<>2 and DutoanId=" & intDutoanId & " order by Vendor ", Conn)
        If cboVendor.Items.Count > 0 Then
            cboVendor.SelectedIndex = 0
        End If
    End Sub

    Private Sub frmSelectFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelectFile()
    End Sub
    Public Function SelectFile() As Boolean

        Try
            Using op As New OpenFileDialog()
                op.Filter = "Format files (*.pdf)|*.pdf|Image Files (*.jpg, *.png)|*.jpg;*.png"
                op.InitialDirectory = "C:"
                If op.ShowDialog() = DialogResult.OK Then
                    txtFilePath.Text = op.FileName
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return True
    End Function


    Private Sub lbkOK_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkOK.LinkClicked
        If cboVendor.SelectedIndex = -1 Then
            MsgBox("You must select Vendor!")
            Exit Sub
        End If
        If txtFilePath.Text <> "" Then
            Me.DialogResult = DialogResult.OK
        End If


    End Sub
    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Public Property VendorId As Integer
        Get
            Return mintVendorId
        End Get
        Set(value As Integer)
            mintVendorId = value
        End Set
    End Property
End Class