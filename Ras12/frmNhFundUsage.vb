Public Class frmNhFundUsage
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Sub Reset()
        cboWaiverType.SelectedIndex = -1
        cboStatus.SelectedIndex = -1
        cboCustomer.SelectedIndex = -1
    End Sub
    Private Sub lbkReset_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkReset.LinkClicked
        Reset()
    End Sub

    Private Sub frmNhFundUsageList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboDisplay(cboCustomer, "Select distinct c.CustShortName as Display" _
                         & ",f.CustId as Value " _
                         & " from NhFund f" _
                         & " left join CustomerList c on f.CustId=c.RecId" _
                         & " order by c.CustShortName", Conn)
        Reset()
        Search()
    End Sub

    Private Function Search() As Boolean
        Dim strQuerry As String = "Select c.CustShortName, Amount , RLOC, TourCode,RequestDate,  WaiverType" _
                                & ",FundId,  Seq, Cur, CustId, u.FstUpdate, u.FstUser, u.LstUpdate, u.LstUser" _
                                & ", u.Status,u.RecId" _
                                & " from NhFundUsage u" _
                                & " left join CustomerList c on u.CustId=c.RecId" _
                                & " where u.Status<>'XX' and c.Status='OK'"

        If cboCustomer.Text <> "" Then
            strQuerry = strQuerry & " and u.CustId=" & cboCustomer.SelectedValue
        End If

        If cboStatus.Text <> "" Then
            strQuerry = strQuerry & " and u.Status='" & cboStatus.Text & "'"
        End If

        If cboWaiverType.Text <> "" Then
            strQuerry = strQuerry & " and u.WaiverType='" & cboWaiverType.Text & "'"
        End If

        strQuerry = strQuerry & " order by CustId"
        LoadDataGridView(dgrFundUsage, strQuerry, Conn)

        dgrFundUsage.Columns(3).DefaultCellStyle.ForeColor = Color.Red


    End Function

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked

        If frmNhFundUsageAdd.ShowDialog = DialogResult.OK Then
            Search()
        End If

    End Sub

    Private Sub lbkAddNewTkt_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddNewTkt.LinkClicked
        Dim frmAdd As New frmAddNewTkt
        If frmAdd.ShowDialog() = DialogResult.OK Then
            Dim lstQuerries As New List(Of String)

            For Each strTkno As String In frmAdd.NewTicket
                lstQuerries.Add("insert into Misc (Cat,Val,Val1,City) values('NhNewTkt'," _
                                & dgrFundUsage.CurrentRow.Cells("RecId").Value _
                                & ",'" & strTkno & "','" & myStaff.City & "')")

            Next
            If lstQuerries.Count > 0 Then
                lstQuerries.Add("Update NhFundUsage set Status='RR' where RecId=" _
                                & dgrFundUsage.CurrentRow.Cells("RecId").Value)
            End If
            If Not UpdateListOfQuerries(lstQuerries, Conn) Then
                MsgBox("Unable to create!")
                Exit Sub
            Else
                LoadDataGridView(dgrNewTicket, "Select Val1 from MISC where cat='NhNewTkt' and Val='" _
                         & dgrFundUsage.CurrentRow.Cells("recid").Value & "'", Conn)
            End If
        End If


    End Sub

    Private Sub dgrFundUsage_SelectionChanged(sender As Object, e As EventArgs) Handles dgrFundUsage.SelectionChanged

        If dgrFundUsage.CurrentRow Is Nothing Then
            Exit Sub
        End If

        LoadDataGridView(dgrOldTicket, "Select Val1 from MISC where cat='NhOldTkt' and Val='" _
                         & dgrFundUsage.CurrentRow.Cells("recid").Value & "'", Conn)
        LoadDataGridView(dgrNewTicket, "Select Val1 from MISC where cat='NhNewTkt' and Val='" _
                         & dgrFundUsage.CurrentRow.Cells("recid").Value & "'", Conn)
        If dgrNewTicket.Rows.Count > 0 Then
            lbkAddNewTkt.Visible = False
        Else
            lbkAddNewTkt.Visible = True
        End If
    End Sub

    Private Sub dgrFundUsage_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrFundUsage.CellContentClick

    End Sub
End Class