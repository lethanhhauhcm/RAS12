Public Class frmFindVendor
    Private mobjSelectedRow As DataGridViewRow
    Private mblnFirstLoadCompleted As Boolean
    Public Sub New(strShortName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        txtVemdorName.Text = strShortName
    End Sub
    Private Sub frmFindVendor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
        mblnFirstLoadCompleted = True
    End Sub
    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        mobjSelectedRow = dgrVendors.CurrentRow
        DialogResult = DialogResult.OK

    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()

    End Sub

    Private Function Search() As Boolean
        Dim strQuerry As String = "Select Recid,ShortName,FOP,HD,CAT from Vendor" _
            & " where Status='OK' and shortname like '%" & txtVemdorName.Text & "%' order by ShortName"

        AddEqualConditionCombo(strQuerry, cboCAT)

        LoadDataGridView(dgrVendors, strQuerry, Conn)
        With dgrVendors
            .Columns("CAT").Width = 32
            .Columns("FOP").Width = 32
        End With
    End Function
    Public Property SelectedRow As DataGridViewRow
        Get
            Return mobjSelectedRow
        End Get
        Set(value As DataGridViewRow)
            mobjSelectedRow = value
        End Set
    End Property

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub
End Class