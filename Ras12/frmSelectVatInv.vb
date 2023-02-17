Public Class frmSelectVatInv
    Private mstrSelectedValue As String
    Private mstrSelectedColumn As String
    Private Sub frmSelectVatInv_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New(strCustShortName As String, decMaxAmount As Decimal _
                   , Optional strSelectedColumn As String = "")
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim tblInput As DataTable = GetVatInvoices(strCustShortName _
                                        , "", "", " and KhachTraId=0")
        dgrVatInv.DataSource = tblInput
        dgrVatInv.Columns("Amount").DefaultCellStyle.Format = "#,##0"
        dgrVatInv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        If tblInput.Rows.Count > 0 Then
            lbkSelect.Visible = True
        Else
            lbkSelect.Visible = False
        End If
        mstrSelectedColumn = strSelectedColumn
    End Sub

    Public Property SelectedValue As String
        Get
            Return mstrSelectedValue
        End Get
        Set(value As String)
            mstrSelectedValue = value
        End Set
    End Property

    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        If dgrVatInv.RowCount > 0 Then

            mstrSelectedValue = dgrVatInv.CurrentRow.Cells(mstrSelectedColumn).Value
            Me.DialogResult = DialogResult.OK
        End If
    End Sub

    Private Sub dgrVatInv_RowStateChanged(sender As Object, e As DataGridViewRowStateChangedEventArgs) Handles dgrVatInv.RowStateChanged
        Dim decSumSelected As Decimal
        For Each objRow As DataGridViewRow In dgrVatInv.SelectedRows
            decSumSelected = decSumSelected + objRow.Cells("Amount").Value
        Next
        txtSumSelected.Text = Format(decSumSelected, "#,##0")
    End Sub


End Class