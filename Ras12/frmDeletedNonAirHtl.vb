Public Class frmDeletedNonAirHtl
    Private mblnFirstLoadCompleted As Boolean
    Public Sub New(tblDeleted As DataTable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        dgrDeleted.DataSource = tblDeleted
    End Sub

    Private Sub frmDeletedNonAirHtl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        chkSameHotelName.Checked = True

        mblnFirstLoadCompleted = True
        If dgrDeleted.Rows.Count > 0 Then
            dgrDeleted.Rows(0).Selected = True
        End If
    End Sub

    Private Sub lbkKeepOldExcludeNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkKeepOldExludeNew.LinkClicked
        Dim lstQuerry As New List(Of String)

        If dgrNew.CurrentRow Is Nothing Then
            MsgBox("Must Select New Record")
            Exit Sub
        End If
        MsgBox("Must update GO if needed")

        lstQuerry.Add("Update GO_hotel set NoSync='True' where RecId=" _
                      & dgrDeleted.CurrentRow.Cells("RecId").Value)
        lstQuerry.Add("Delete GO_hotel where ItemId=" _
                      & dgrNew.CurrentRow.Cells("RecId").Value)
        lstQuerry.Add("insert Go_Misc (Cat,IntVal) values ('ExcludedNA'," _
                      & dgrNew.CurrentRow.Cells("RecId").Value & ")")

        If pobjTvcs.UpdateListQuerries(lstQuerry, False) Then
            dgrDeleted.Rows.Remove(dgrDeleted.CurrentRow)
        End If


    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDeleteOldTakeNew.LinkClicked
        Dim strQuerry As String
        Dim intNewRecordSelected As Integer = -1
        Dim i As Integer

        For i = 0 To dgrNew.RowCount - 1
                If dgrNew.Rows(i).Cells("S").Value Then
                    intNewRecordSelected = i
                    Exit For
                End If
            Next

        If dgrNew.RowCount > 0 AndAlso intNewRecordSelected = -1 Then
            MsgBox("Must Select New Record")
            Exit Sub
        End If
        MsgBox("Please delete GO record before continue")

        strQuerry = "Delete Go_Hotel where Recid=" & dgrDeleted.CurrentRow.Cells("RecId").Value
        If pobjTvcs.ExecuteNonQuerry(strQuerry) Then
            dgrDeleted.Rows.Remove(dgrDeleted.CurrentRow)
        End If
    End Sub

    Private Function LoadNewHotel() As Boolean
        Dim strQuerry As String
        With dgrDeleted.CurrentRow
            strQuerry = "Select 'False' as S, isNull(h.RecId,0) as NewHtlId, i.* " _
            & " from RAS12.dbo.Dutoan_item i" _
            & " left join GO_Hotel h on i.RecId=h.ItemId" _
            & " where i.Status='OK'" _
            & " and i.Service='Accommodations' and i.DutoanId=" _
            & .Cells("DutoanId").Value

            If chkSameHotelName.Checked Then
                strQuerry = strQuerry & " and i.Supplier='" & .Cells("HotelName").Value & "'"
            End If
        End With

        pobjTvcs.LoadDataGridView(dgrNew, strQuerry)

        Return True
    End Function

    Private Sub dgrDeleted_SelectionChanged(sender As Object, e As EventArgs) Handles dgrDeleted.SelectionChanged
        If mblnFirstLoadCompleted Then
            LoadNewHotel()
        End If
    End Sub

    Private Sub chkSameHotelName_CheckedChanged(sender As Object, e As EventArgs) Handles chkSameHotelName.CheckedChanged
        If mblnFirstLoadCompleted Then
            LoadNewHotel()
        End If
    End Sub
End Class