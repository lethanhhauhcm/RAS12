Public Class frmEditComm
    Private Sub frmEditComm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strLoadCust As String = "select m.intVal as Value, c.CustShortname as Display" _
            & " from tvcs.dbo.misc m left join CustomerList c on m.IntVal=c.Recid" _
            & " where m.Cat='GetComm4CustId'"

        LoadCombo(cboCustomers, strLoadCust, Conn)
        dtpFromDate.MinDate = "17 Dec 18"
        dtpToDate.MinDate = "17 Dec 18"
        dtpToDate.Value = Now.AddDays(-1)
        dtpFromDate.Value = Now.AddDays(-1)


    End Sub

    Private Function LinkTktComm(dteFromDate As Date, dteToDate As Date) As Boolean
        Dim strQuerry As String = "select c.RecId,c.Val as Tkno,dteVal as DOI,intVal as Comm,t.RecId as TkId" _
            & ", t.PaxName,t.Fare,t.Itinerary" _
            & " from tvcs.dbo.Misc c left join tkt t on c.Val=t.Tkno and c.dteVal=t.DOI" _
            & " left join Rcp r on t.RcpId=r.RecId" _
            & " where c.dteVal between '" & CreateFromDate(dteFromDate) & "' and ' " & CreateToDate(dteToDate) & "'" _
            & " and c.intVal2 is null" _
            & " and t.Srv='S' and t.Qty<>0 and t.Status<>'XX'" _
            & " and r.CustId in (select intVal" _
            & " from tvcs.dbo.misc where Cat='GetComm4CustId')"
        Dim tblTkt As DataTable = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblTkt.Rows
            ExecuteNonQuerry("update tvcs.dbo.Misc set intVal2=" & objRow("Tkid") _
                             & " where recid=" & objRow("Recid"), Conn)
        Next
        Return True
    End Function
    Private Function RelinkTktComm(dteFromDate As Date, dteToDate As Date) As Boolean
        Dim strQuerry As String = "select c.RecId,c.Val as Tkno,dteVal as DOI,intVal as Comm,intVal2 as TkId" _
            & ", t.PaxName,t.Fare,t.Itinerary" _
            & " from tvcs.dbo.Misc c left join tkt t on c.intVal2=t.RecId" _
            & " left join Rcp r on t.RcpId=r.RecId" _
            & " where c.dteVal between '" & CreateFromDate(dteFromDate) & "' and ' " & CreateToDate(dteToDate) & "'" _
            & " and t.Srv='S' and t.Qty<>0 and t.Status='XX'" _
            & " and r.CustId in (select intVal" _
            & " from tvcs.dbo.misc where Cat='GetComm4CustId')"
        Dim tblTkt As DataTable = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblTkt.Rows
            Dim intNewTkId As Integer = ScalarToInt("tkt", "recid", "Status<>'XX' and RecId>" & objRow("Tkid"))
            If intNewTkId > 0 Then
                ExecuteNonQuerry("update tvcs.dbo.Misc set intVal2=" & intNewTkId _
                             & " where recid=" & objRow("Recid"), Conn)
            End If
        Next
        Return True
    End Function
    Private Function GetManTktFromRas(dteFromDate As Date, dteToDate As Date) As Boolean
        Dim strQuerry As String = "insert tvcs.dbo.misc (Cat,Val,intVal2,dteVal) " _
            & " select 'Comm4Tkt',Tkno,RecId,DOI from Tkt" _
            & " left join Rcp r on t.RcpId=r.RecId" _
            & " where t.Srv='S' and t.Qty<>0 and t.Doi between '" _
            & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) & "'" _
            & " and r.CustId in (select intVal" _
            & " from tvcs.dbo.misc where Cat='GetComm4CustId')" _
            & " and RecId not in (Select intVal2 from tvcs.dbo.Misc where cat='Comm4Tkt')"

        Return ExecuteNonQuerry(strQuerry, Conn)

    End Function
    Private Function LoadTktComm(dteFromDate As Date, dteToDate As Date) As Boolean
        Dim strLoadTkt As String = "select c.RecId,c.Val as Tkno,dteVal as DOI,intVal as Comm,intVal2 as TkId" _
            & ", t.PaxName,t.Fare,t.Itinerary" _
            & " from tvcs.dbo.Misc c left join tkt t on c.intVal2=t.RecId" _
            & " left join Rcp r on t.RcpId=r.RecId" _
            & " where c.dteVal between '" & CreateFromDate(dteFromDate) & "' and ' " & CreateToDate(dteToDate) & "'" _
            & " and t.Srv='S' and t.Qty<>0" _
            & " and r.CustId in (select intVal" _
            & " from tvcs.dbo.misc where Cat='GetComm4CustId')"
        LoadDataGridView(dgrTkt, strLoadTkt, Conn)
        Return True
    End Function

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        LinkTktComm(dtpFromDate.Value, dtpToDate.Value)
        RelinkTktComm(dtpFromDate.Value, dtpToDate.Value)
        LoadTktComm(dtpFromDate.Value, dtpToDate.Value)
    End Sub

    Private Sub dgrTkt_SelectionChanged(sender As Object, e As EventArgs) Handles dgrTkt.SelectionChanged
        If dgrTkt.CurrentRow IsNot Nothing Then
            txtComm.Text = dgrTkt.CurrentRow.Cells("Comm").Value
        End If
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        If Not IsNumeric(txtComm.Text) Then
            MsgBox("Invalid Comm!")
            Exit Sub
        End If
        If dgrTkt.CurrentRow IsNot Nothing Then
            If ExecuteNonQuerry("update tvcs.dbo.Misc set intVal=" & txtComm.Text _
                             & " where recid=" & dgrTkt.CurrentRow.Cells("RecId").Value, Conn) Then
                dgrTkt.CurrentRow.Cells("Comm").Value = txtComm.Text
            End If
        End If
    End Sub
End Class