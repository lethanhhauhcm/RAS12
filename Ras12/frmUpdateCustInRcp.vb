Public Class frmUpdateCustInRcp
    Private Sub frmUpdateCustInRcp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboCustGrp.SelectedIndex = 0
        dtpTo.Value = Now.AddDays(-1)
        dtpFrom.Value = DateSerial(dtpTo.Value.Year, dtpTo.Value.Month, 1)
    End Sub

    Private Sub cboCustGrp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustGrp.SelectedIndexChanged

    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Dim strQuerry As String = "select RecID, RCPNo, SRV, CustID, CustShortName" _
                                    & ", Currency, ROE, TTLDue, Discount, Charge, Status" _
                                    & " from Rcp where status='OK'" _
                                    & " and CustId in (select IntVal from MISC where cat='CustNameInGroup' " _
                                    & " and VAL='" & cboCustGrp.Text & "')"

        LoadCmb_VAL(cboCust, "SELECT VAL1 as DIS,IntVal as VAL  from misc where cat='CustNameInGroup'" _
                            & " And val='" & cboCustGrp.Text & "'")

        strQuerry = strQuerry & " order by RecId"
        LoadDataGridView(dgrRcp, strQuerry, Conn)
        dgrRcp.Columns("TTLDue").DefaultCellStyle.Format = "#,#00"
        dgrRcp.Columns("TTLDue").DefaultCellStyle.Alignment = ContentAlignment.MiddleRight
    End Sub

    Private Sub blkUpdate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles blkUpdate.LinkClicked
        If dgrRcp.CurrentRow Is Nothing Then Exit Sub
        Dim lstQuerries As New List(Of String)
        lstQuerries.Add("Update Rcp set CustId=" & cboCust.SelectedValue _
                        & ",CustShortName='" & cboCust.Text & "' where RecId=" _
                        & dgrRcp.CurrentRow.Cells("RecId").Value)
        lstQuerries.Add(UpdateLogFile("Rcp", "ChangeCust" _
                                      , dgrRcp.CurrentRow.Cells("RecId").Value _
                                      , dgrRcp.CurrentRow.Cells("CustId").Value _
                                      , dgrRcp.CurrentRow.Cells("CustShortName").Value, "Old Values", ""))
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            dgrRcp.CurrentRow.Cells("CustId").Value = cboCust.SelectedValue
            dgrRcp.CurrentRow.Cells("CustShortName").Value = cboCust.Text
        Else
            MsgBox("Không sửa đổi được Customer")
        End If
    End Sub
End Class