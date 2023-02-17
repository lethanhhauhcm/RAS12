Public Class frmLinkMainSvcNonAir
    Private mobjSelectedRow As DataGridViewRow
    Private Sub frmLinkTourItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strQuerry As String
        With mobjSelectedRow
            strQuerry = "select * from DuToan_Item where Status<>'XX' and DuToanID=" _
                            & .Cells("DutoanId").Value & " and RecId <>" & .Cells("RecId").Value _
                            & " And Service NOT in ('TransViet SVC Fee','Bank Fee','Merchant Fee')"
        End With

        LoadDataGridView(grdItems, strQuerry, Conn)
    End Sub

    Public Sub New(objSelectedRow As DataGridViewRow)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mobjSelectedRow = objSelectedRow
    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub lbkOK_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkOK.LinkClicked
        With grdItems
            If .CurrentRow IsNot Nothing Then

                If ExecuteNonQuerry("Update Dutoan_Item set MainItem=" & .CurrentRow.Cells("RecId").Value _
                                 & " where RecId =" & mobjSelectedRow.Cells("RecId").Value, Conn) Then
                    DialogResult = DialogResult.OK
                    Me.Dispose()
                End If
            End If
        End With
    End Sub

    Private Sub lbkDeleteLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDeleteLink.LinkClicked
        If ExecuteNonQuerry("Update Dutoan_Item set MainItem=0" _
                                 & " where RecId =" & mobjSelectedRow.Cells("RecId").Value, Conn) Then
            DialogResult = DialogResult.OK
            Me.Dispose()
        End If
    End Sub
End Class