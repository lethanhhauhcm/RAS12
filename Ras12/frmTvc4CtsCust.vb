Public Class frmTvc4CtsCust
    Private Sub frmTvc4CtsCust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
        cboNewTvc.SelectedIndex = 0
    End Sub
    Private Function Search() As Boolean
        Dim strNewCust As String = "Union Select 0,a.Custid,CustShortName,'' as TVC from Cust_Detail a left join CustomerList c on a.CustId=c.RecId" _
                                    & " where c.Status='OK' and a.Status='ok' and a.Cat='Channel' and a.Val in ('CS','LC')" _
                                    & " and a.CustID NOT in (select Custid from Cust_Detail where Status='ok' and cat='TVC')"
        Dim strQuerry As String = "select d.RecId, d.CustId,c.CustShortName,d.Val As TVC " _
            & " from Cust_Detail d" _
            & " left join CustomerList c on c.RecId=d.CustId " _
            & " where d.Status='OK' and d.Cat='TVC'"
        AddEqualConditionCombo(strQuerry, cboTvc, "d.Val")
        strQuerry = strQuerry & strNewCust & " order by CustShortName"

        LoadDataGridView(dgrCustomers, strQuerry, Conn)

        Return True
    End Function

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        cboNewTvc.SelectedIndex = -1
        'Search()
    End Sub

    Private Sub lbkSetNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSetNew.LinkClicked
        With dgrCustomers
            If .CurrentRow IsNot Nothing AndAlso .CurrentRow.Cells("TVC").Value <> cboNewTvc.Text Then
                Dim lstQuerry As New List(Of String)
                lstQuerry.Add("insert Cust_Detail (CustId,Cat,Val,FstUser,Status,City) values (" _
                              & .CurrentRow.Cells("CustId").Value & ",'TVC','" & cboNewTvc.Text & "','" & myStaff.SICode _
                              & "','OK','" & myStaff.City & "')")
                If .CurrentRow.Cells("RecId").Value > 0 Then
                    lstQuerry.Add(ChangeStatus_ByID("Cust_Detail", "XX", .CurrentRow.Cells("RecId").Value))
                End If

                If UpdateListOfQuerries(lstQuerry, Conn) Then
                    Search()
                Else
                    MsgBox("Unable to set TVC for selected customer")
                End If
            End If
        End With

    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
End Class