Public Class frmCombinedInv
    Private Sub frmCombinedInv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomerInGroups2Combo("COMBINED_INV", cboCust)
        cboCust.SelectedIndex = 0
        dtpDOS.Value = Now.AddDays(-1)
    End Sub

    Private Sub lbkCreateE_Inv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateE_Inv.LinkClicked
        Dim frmE_Inv As New frmE_InvEdit()
        Dim strTkIds As String = String.Empty

        For Each objRow As DataGridViewRow In dgrTkts.Rows
            strTkIds = strTkIds & objRow.Cells("TkId").Value & ","
        Next
        If strTkIds <> "" Then
            strTkIds = Mid(strTkIds, 1, strTkIds.Length - 1)
            frmE_Inv.LoadDetailsCombinedInv(cboCust.SelectedValue, strTkIds)
            frmE_Inv.ShowDialog()
        End If

    End Sub

    Private Sub lbkLoad_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkLoad.LinkClicked
        Dim strQuerry As String
        Dim strDateRange As String = "between '" & CreateFromDate(dtpDOS.Value) _
                                    & "' and '" & CreateToDate(dtpDOS.Value) & "'"
        strQuerry = "select t.Rcpno, t.TKNO, t.Itinerary,t.Fare+t.Tax+t.Charge+t.ChargeTV as Total,t.Recid as Tkid" _
                    & " from tkt t" _
                    & " left join RCP r on t.rcpid=r.recid " _
                    & " where t.Status='OK' and t.SRV='S' and Custid=" & cboCust.SelectedValue _
                    & " AND DOS " & strDateRange _
                    & " order by r.recid ,t.tkno"
        LoadDataGridView(dgrTkts, strQuerry, Conn)
        dgrTkts.Columns("Total").DefaultCellStyle.Format = "#,###"
        dgrTkts.Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        For Each objRow As DataGridViewRow In dgrTkts.Rows
            objRow.Cells("S").Value = True
        Next
    End Sub

    Private Sub lbkSelectRcp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectRcp.LinkClicked
        If dgrTkts.CurrentRow Is Nothing Then Exit Sub
        ToggleSelectionByRcpNo(dgrTkts.CurrentRow.Cells("RcpNo").Value, True)
    End Sub
    Private Function ToggleSelectionByRcpNo(strRcpNo As String, blnSelected As Boolean) As Boolean
        For Each objRow As DataGridViewRow In dgrTkts.Rows
            If objRow.Cells("RcpNo").Value = strRcpNo Then
                objRow.Cells("S").Value = blnSelected
            End If
        Next
    End Function
    Private Sub lbkUnSelectRcp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUnSelectRcp.LinkClicked
        If dgrTkts.CurrentRow Is Nothing Then Exit Sub
        ToggleSelectionByRcpNo(dgrTkts.CurrentRow.Cells("RcpNo").Value, False)
    End Sub
End Class