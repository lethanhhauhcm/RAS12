Public Class frmLinkTourItems
    Private mobjSelectedRow As DataGridViewRow
    Private Sub frmLinkTourItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strQuerry As String
        With mobjSelectedRow
            strQuerry = "select * from DuToan_Item where Status<>'XX' and DuToanID=" _
                        & .Cells("DutoanId").Value & " and RecId <>" & .Cells("RecId").Value
            Select Case .Cells("Service").Value
                Case "TransViet SVC Fee", "Bank Fee"
                    strQuerry = strQuerry & " and Service  not in ('TransViet SVC Fee','Bank Fee')" _
                        & " and RecId not in (select RelatedItem from DuToan_Item where Status='OK'" _
                        & " and DutoanId=" & .Cells("DutoanId").Value & " and Service='" _
                        & .Cells("Service").Value & "')"
                Case Else
                    strQuerry = strQuerry & " And RelatedItem=0 And Service in ('TransViet SVC Fee','Bank Fee')"
            End Select
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
                Dim intMainId As Integer
                Dim strRelatedItems As String = "(" & .CurrentRow.Cells("RecId").Value _
                                                & "," & mobjSelectedRow.Cells("RecId").Value & ")"

                Select Case mobjSelectedRow.Cells("Service").Value
                    Case "TransViet SVC Fee", "Bank Fee"
                        intMainId = .CurrentRow.Cells("RecId").Value
                    Case Else
                        intMainId = mobjSelectedRow.Cells("RecId").Value
                End Select

                If ExecuteNonQuerry("Update Dutoan_Item set RelatedItem=" & intMainId _
                                 & " where RecId in " & strRelatedItems, Conn) Then
                    DialogResult = DialogResult.OK
                    Me.Dispose()
                End If
            End If
        End With
    End Sub
End Class