Public Class frmHotelRates
    Private mblnFirstLoadCompleted As Boolean
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub frmHotelRates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo(cboProvince, "select strVal1 as Value from lib.dbo.Misc" _
                                & " where Status='ok' and CAT='VnProvince'" _
                                & " order by strVal1", Conn)
        Clear()
        mblnFirstLoadCompleted = True
    End Sub
    Private Function Clear() As Boolean
        cboProvince.SelectedIndex = -1
        cboDistrict.SelectedIndex = -1
        txtHotelName.Text = ""
        cboStatus.SelectedIndex = 0
        cboDateType.SelectedIndex = -1
        dtpFrom.Visible = False
        dtpTo.Visible = False
        Return True
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String
        strQuerry = "select h.RecID,s.FullName, h.SupplierId,s.District, h.ContactPerson" _
                        & ", h.ContactTel, h.ContactEmail, h.RoomCat, h.RoomType, h.Rate" _
                        & ", h.ValidFrom, h.ValidTo, h.Breakfast, h.ServiceCharge,h.CustGroup" _
                        & ", h.Remark, h.FstUpdate, h.FstUser, h.LstUpdate, h.LstUser, h.Status, h.City" _
                        & ",s.Province" _
                        & " from HotelRates h" _
                        & " left join Supplier s on h.SupplierId=s.RecId" _
                        & " where s.Status='OK'"
        AddEqualConditionCombo(strQuerry, cboStatus, "h.Status")
        AddEqualConditionCombo(strQuerry, cboProvince, "s.Province", True)
        AddEqualConditionCombo(strQuerry, cboDistrict, "s.District", True)
        AddLikeConditionText(strQuerry, txtHotelName, "s.FullName")
        Select Case cboDateType.Text
            Case "OnDate"
                strQuerry = strQuerry & " and '" & CreateFromDate(dtpFrom.Value) & "' between ValidFrom and ValidTo"
            Case "FromDate"
                strQuerry = strQuerry & " and '" & CreateFromDate(dtpFrom.Value) & "' =ValidFrom"
            Case "ToDate"
                strQuerry = strQuerry & " and '" & CreateToDate(dtpTo.Value) & "' =ValidTo"
            Case "Between"
                strQuerry = strQuerry & " and '" & CreateFromDate(dtpFrom.Value) & "' <= ValidTo" _
                    & " and '" & CreateToDate(dtpTo.Value) & "' >= ValidFrom"
        End Select
        strQuerry = strQuerry & " order by s.FullName"
        LoadDataGridView(dgrHotelRates, strQuerry, Conn)
        dgrHotelRates.Columns("Rate").DefaultCellStyle.Format = "#,#00"
        dgrHotelRates.Columns("SupplierId").Visible = False
        Return True
    End Function

    Private Sub cboDateType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDateType.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Select Case cboDateType.Text
                Case "OnDate"
                    dtpFrom.Visible = True
                    dtpTo.Visible = False
                Case "FromDate"
                    dtpFrom.Visible = True
                    dtpTo.Visible = False
                Case "ToDate"
                    dtpFrom.Visible = False
                    dtpTo.Visible = True
                Case "Between"
                    dtpFrom.Visible = True
                    dtpTo.Visible = True
            End Select
        End If

    End Sub

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmNew As New frmHotelRateEdit
        If frmNew.ShowDialog = DialogResult.OK Then Search()
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        If dgrHotelRates.CurrentRow Is Nothing Then Exit Sub
        Dim frmNew As New frmHotelRateEdit(dgrHotelRates.CurrentRow)
        If frmNew.ShowDialog = DialogResult.OK Then Search()
    End Sub

    Private Sub lbkClone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone.LinkClicked
        If dgrHotelRates.CurrentRow Is Nothing Then Exit Sub
        Dim frmNew As New frmHotelRateEdit(dgrHotelRates.CurrentRow, True)
        If frmNew.ShowDialog = DialogResult.OK Then Search()
    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If dgrHotelRates.CurrentRow Is Nothing Then Exit Sub
        If ExecuteNonQuerry(ChangeStatus_ByID("HotelRates", "XX", dgrHotelRates.CurrentRow.Cells("RecId").Value), Conn) Then
            Search()
        Else
            MsgBox("Unable to delete HotelRates Record Id " & dgrHotelRates.CurrentRow.Cells("RecId").Value)
        End If
    End Sub

    Private Sub cboProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvince.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadCombo(cboDistrict, "Select VAL As Value from Lib.dbo.Misc" _
                        & " where Status ='ok' and CAT='VnDistrict'" _
                        & " and strVal1=N'" & cboProvince.Text & "'", Conn)
        End If
    End Sub

    Private Sub lbkCustGrp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCustGrp.LinkClicked
        Dim frmGroup As New frmCustGroup
        frmGroup.ShowDialog()
    End Sub
End Class