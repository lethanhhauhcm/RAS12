'20220523 add by 7643
Public Class frmAmadeusCommand
    Public Sub New(xSegs As DataGridView, xTkts As DataGridView)
        Dim i As Integer

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For i = 0 To xSegs.Rows.Count - 1
            dgrSegs.Rows.Add({xSegs.Rows(i).Cells("FromCity").Value, xSegs.Rows(i).Cells("ToCity").Value, xSegs.Rows(i).Cells("Car").Value, xSegs.Rows(i).Cells("FltNbr").Value,
                              xSegs.Rows(i).Cells("FltDate").Value, xSegs.Rows(i).Cells("ETD").Value, xSegs.Rows(i).Cells("ETA").Value, 0})
        Next

        For i = 0 To xTkts.Rows.Count - 1
            dgrTkts.Rows.Add({xTkts.Rows(i).Cells("TKNO").Value, xTkts.Rows(i).Cells("PaxName").Value, xTkts.Rows(i).Cells("PaxType").Value, xTkts.Rows(i).Cells("BkgCls").Value, 0})
        Next
    End Sub

    Private Sub frmEnterNumber_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        dgrSegs.Select()
        dgrTkts.Select()

        If Not DialogResult = DialogResult.OK Then
            dgrSegs.Rows.Clear()
            Exit Sub
        End If
    End Sub

    Private Sub dgrSegs_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgrSegs.EditingControlShowing
        AddHandler e.Control.KeyPress, AddressOf cell_KeyPress
    End Sub

    Private Sub cell_KeyPress(sender As Object, e As KeyPressEventArgs)
        If dgrSegs.CurrentRow.Cells("NumDay").Selected Then
            If (Not Char.IsControl(e.KeyChar) _
             AndAlso (Not Char.IsDigit(e.KeyChar) _
             AndAlso (e.KeyChar <> Microsoft.VisualBasic.ChrW(46)))) Then
                e.Handled = True
            End If
        ElseIf dgrTkts.CurrentRow.Cells("Tel").Selected Then
            If (Not Char.IsControl(e.KeyChar) _
             AndAlso (Not Char.IsDigit(e.KeyChar) _
             AndAlso (e.KeyChar <> Microsoft.VisualBasic.ChrW(46)))) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgrSegs_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgrSegs.CellEndEdit
        If dgrSegs.CurrentRow.Cells("NumDay").Value = Nothing Then
            dgrSegs.CurrentRow.Cells("NumDay").Value = 0
        Else
            dgrSegs.CurrentRow.Cells("NumDay").Value = Integer.Parse(dgrSegs.CurrentRow.Cells("NumDay").Value)
        End If
    End Sub

    Private Sub dgrTkts_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgrTkts.EditingControlShowing
        AddHandler e.Control.KeyPress, AddressOf cell_KeyPress
    End Sub

    Private Sub dgrTkts_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgrTkts.CellEndEdit
        If dgrTkts.CurrentRow.Cells("Tel").Value = Nothing Then
            dgrTkts.CurrentRow.Cells("Tel").Value = 0
        End If
    End Sub
End Class