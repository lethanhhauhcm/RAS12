Public Class frmSelectMultiRows
    Private mstrSelectedValue As String
    Private mstrSelectedColumn As String
    Private mlstSelectedRows As New List(Of DataGridViewRow)
    Private mtblInput As DataTable
    Private mblnFirstLoadCompleted As Boolean
    Private objBind As New BindingSource
    Private Sub frmShowTableContent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mblnFirstLoadCompleted = True
    End Sub

    Public Sub New(tblInput As DataTable, strMsg As String, Optional strSelectColumn As String = "" _
                   , Optional strFilterColumn As String = "")
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
    Public Property SelectedRows As List(Of DataGridViewRow)
        Get
            Return mlstSelectedRows
        End Get
        Set(value As List(Of DataGridViewRow))
            mlstSelectedRows = value
        End Set
    End Property
    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked

        If dgrInput.RowCount > 0 Then
            For Each objRow As DataGridViewRow In dgrInput.Rows
                If objRow.Cells("Selected").Value Then
                    mstrSelectedValue = mstrSelectedValue & objRow.Cells(mstrSelectedColumn).Value & "|"
                    mlstSelectedRows.Add(objRow)
                End If
            Next
            If mstrSelectedValue <> "" Then
                mstrSelectedValue = Mid(mstrSelectedValue, 1, mstrSelectedValue.Length - 1)
                Me.DialogResult = DialogResult.OK
            End If
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