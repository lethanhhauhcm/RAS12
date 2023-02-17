Public Class frmSelectUNC
    Private mobjSelectedRows As DataGridViewSelectedRowCollection
    Private mblnFirstLoadCompleted As Boolean
    Private mobjInvRow As DataGridViewRow
    Public Sub New(objRow As DataGridViewRow)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mobjInvRow = objRow
        txtVemdorName.Text = mobjInvRow.Cells("ShortName").Value
        Me.Text = Me.Text & "-VND:" & objRow.Cells("Amount").Value
    End Sub
    Private Sub frmSelectUNC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
        mblnFirstLoadCompleted = True
    End Sub
    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        mobjSelectedRows = dgrUNCs.SelectedRows
        DialogResult = DialogResult.OK

    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()

    End Sub

    Private Function Search() As Boolean
        Dim strFilterByItem As String = "select dutoanid from DuToan_Item where status='ok'" _
            & " and Vendor='" & txtVemdorName.Text & "'"
        Dim strQuerry As String

        AddEqualConditionCombo(strFilterByItem, cboService)

        strQuerry = "select RecID, Shortname, Amount, FOP ,RefNo, AccountName, AccountNumber, " _
                    & "BankName, BankAddress, Description, FstUpdate, FstUser" _
                    & ", RMK, SupplierID,InvNo, Curr" _
                    & " from unc_payments where Status='OK' " _
                    & " and FstUpdate>='" & CreateFromDate(mobjInvRow.Cells("DOI").Value) _
                    & "' and ShortName='" & txtVemdorName.Text & "'"

        AddLikeConditionText(strQuerry, txtAccountName)

        If chkCompareAmount.Checked Then
            strQuerry = strQuerry & " and Amount<=" & mobjInvRow.Cells("Amount").Value
        End If
        strQuerry = strQuerry & " order by recid desc"

        LoadDataGridView(dgrUNCs, strQuerry, Conn)
        With dgrUNCs
            .Columns("Amount").DefaultCellStyle.Format = "#,###"
            '.Columns("Pax").Width = 32
            '.Columns("Brief").Width = 256
        End With
    End Function
    Public Property SelectedRows As DataGridViewSelectedRowCollection
        Get
            Return mobjSelectedRows
        End Get
        Set(value As DataGridViewSelectedRowCollection)
            mobjSelectedRows = value
        End Set
    End Property

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub

    Private Sub dgrUNCs_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrUNCs.CellContentClick
        If mblnFirstLoadCompleted Then
            Dim decTotalSelected As Decimal = 0
            For Each objRow As DataGridViewRow In dgrUNCs.SelectedRows
                decTotalSelected = decTotalSelected + objRow.Cells("Amount").Value
            Next
            lblTotalSelected.Text = "Matched:" & Format(decTotalSelected, "#,###")
            lblResidual.Text = "Residual:" & Format(mobjInvRow.Cells("Amount").Value - decTotalSelected, "#,###")
        End If
    End Sub
End Class