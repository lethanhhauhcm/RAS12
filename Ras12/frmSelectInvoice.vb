Public Class frmSelectInvoice
    Private mstrSelectedValue As String
    Private mstrSelectedColumn As String
    Private mobjSelectedGridRow As DataGridViewRow
    Private mobjSelectedDataRow As DataRow
    Private mtblInput As DataTable
    Private mblnFirstLoadCompleted As Boolean
    Private objBind As New BindingSource

    Private Sub frmSelectInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For Each objColumn As DataGridViewColumn In dgrE_Invoices.Columns
            Select Case objColumn.Name
                Case "RecId", "InvId", "TVC", "MauSo", "KyHieu", "InvoiceNo", "DOI", "OriFkey"
                    objColumn.Visible = True
                    objColumn.SortMode = DataGridViewColumnSortMode.NotSortable
                Case Else
                    objColumn.Visible = False
                    objColumn.SortMode = DataGridViewColumnSortMode.NotSortable
            End Select
        Next

        mblnFirstLoadCompleted = True
    End Sub

    Public Sub New(tblInput As DataTable, strMsg As String, Optional strSelectColumn As String = "")
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        objBind.DataSource = tblInput
        dgrE_Invoices.DataSource = objBind
        mtblInput = tblInput
        Me.Text = strMsg
        mstrSelectedColumn = strSelectColumn
        If strSelectColumn <> "" AndAlso tblInput.Rows.Count > 0 Then
            lbkSelect.Visible = True
        Else
            lbkSelect.Visible = False
        End If

    End Sub
    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        If dgrE_Invoices.RowCount > 0 Then
            mstrSelectedValue = dgrE_Invoices.CurrentRow.Cells(mstrSelectedColumn).Value
            mobjSelectedGridRow = dgrE_Invoices.CurrentRow
            mobjSelectedDataRow = mtblInput.Rows(dgrE_Invoices.CurrentRow.Index)
            Me.DialogResult = DialogResult.OK
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
            Return mobjSelectedGridRow
        End Get
        Set(value As DataGridViewRow)
            mobjSelectedGridRow = value
        End Set
    End Property
    Public Property SelectedDataRow As DataRow
        Get
            Return mobjSelectedDataRow
        End Get
        Set(value As DataRow)
            mobjSelectedDataRow = value
        End Set
    End Property

    Private Sub lbkViewInv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewInv.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim blnNoOriInv As Boolean

        With dgrE_Invoices.CurrentRow
            If IsNumeric(.Cells("InvId").Value) AndAlso .Cells("InvId").Value > 40000 Then
                blnNoOriInv = False
            Else
                blnNoOriInv = Not MergeOldNewInvoice(.Cells("TVC").Value)
            End If
            ViewInv(.Cells("TVC").Value, pblnTT78, blnNoOriInv,
                    .Cells("MauSo").Value _
                    , .Cells("KyHieu").Value, .Cells("InvoiceNo").Value, .Cells("InvId").Value)
        End With
    End Sub
End Class