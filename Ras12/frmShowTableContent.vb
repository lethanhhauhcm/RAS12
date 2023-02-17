Public Class frmShowTableContent
    Private mstrSelectedValue As String
    Private mstrSelectedColumn As String
    Private mobjSelectedRow As DataGridViewRow
    Private mtblInput As DataTable
    Private mblnFirstLoadCompleted As Boolean
    Private objBind As New BindingSource
    Private Sub frmShowTableContent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mblnFirstLoadCompleted = True
    End Sub

    Public Sub New(tblInput As DataTable, strMsg As String, Optional strSelectColumn As String = "" _
                   , Optional strFilterColumn As String = "", Optional strSearchValue As String = "")
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        objBind.DataSource = tblInput
        dgrInput.DataSource = objBind
        mtblInput = tblInput
        Me.Text = strMsg
        mstrSelectedColumn = strSelectColumn
        If strSelectColumn <> "" AndAlso tblInput.Rows.Count > 0 Then
            lbkSelect.Visible = True
            If strSearchValue <> "" Then
                For Each objRow As DataGridViewRow In dgrInput.Rows
                    If objRow.Cells(strSelectColumn).Value = strSearchValue Then
                        dgrInput.CurrentCell = objRow.Cells(strSelectColumn)
                        Exit For
                    End If
                Next
            End If
        Else
            lbkSelect.Visible = False
        End If
        If strFilterColumn = "" Then
            lblSearchField.Visible = False
            cboSearchField.Visible = False
        Else
            lblSearchField.Text = strFilterColumn
            For Each objRow As DataRow In tblInput.Rows
                If Not cboSearchField.Items.Contains(objRow(strFilterColumn)) Then
                    cboSearchField.Items.Add(objRow(strFilterColumn))
                End If
            Next
        End If

    End Sub

    Public Property SelectedValue As String
        Get
            Return mstrSelectedValue
        End Get
        Set(value As String)
            mstrSelectedValue = value
        End Set
    End Property
    Public Property SelectedRow As DataGridViewRow
        Get
            Return mobjSelectedRow
        End Get
        Set(value As DataGridViewRow)
            mobjSelectedRow = value
        End Set
    End Property
    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        If dgrInput.RowCount > 0 Then
            mstrSelectedValue = dgrInput.CurrentRow.Cells(mstrSelectedColumn).Value
            mobjSelectedRow = dgrInput.CurrentRow
            Me.DialogResult = DialogResult.OK
        End If
    End Sub

    Private Sub cboSearchField_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearchField.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Dim tblData As New DataTable
            Dim dgr1 As New DataGridView
            For Each objRow As DataRow In mtblInput.Select(lblSearchField.Text & "='" & cboSearchField.Text & "'" _
                                                           , lblSearchField.Text, DataViewRowState.CurrentRows)
                tblData.ImportRow(objRow)
            Next
            dgrInput.ReadOnly = False
            objBind.DataSource = tblData
            dgrInput.Refresh()
        End If
    End Sub
End Class