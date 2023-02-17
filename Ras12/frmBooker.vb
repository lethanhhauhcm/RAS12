Public Class frmBooker
    Private mblnFirstLoadCompleted As Boolean
    Private Sub frmBooker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mblnFirstLoadCompleted = True

    End Sub

    Private Sub cboCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomer.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadBooker()
        End If
    End Sub

    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd.LinkClicked
        Dim strQuerry As String = "insert into SIR (CustId,Fname,Fvalue,Status,FstUser)" _
            & " values(" & cboCustomer.SelectedValue & ",'BOOKER','" & txtBooker.Text _
            & "','OK','" & myStaff.SICode & "')"
        ExecuteNonQuerry(strQuerry, Conn)
    End Sub
    Private Function LoadBooker() As Boolean
        LoadDataGridView(dgrBooker, "Select RecId,Fvalue as Booker,Status from SIR where Status<>'XX'" _
                             & "and Fname='Booker' And CustiD=" _
                             & cboCustomer.SelectedValue & " order by Fvalue", Conn)
        Return True
    End Function

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        Dim strQuerry As String = "update SIR set Status='XX', LstUpdate=getdate()" _
            & ",LstUser='" & myStaff.SICode & "' where RecId=" _
            & dgrBooker.CurrentRow.Cells("RecId").Value
        ExecuteNonQuerry(strQuerry, Conn)
    End Sub

    Private Sub lbkExpire_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkExpire.LinkClicked
        Dim strQuerry As String = "update SIR set Status='EX', LstUpdate=getdate()" _
            & ",LstUser='" & myStaff.SICode & "' where RecId=" _
            & dgrBooker.CurrentRow.Cells("RecId").Value
        ExecuteNonQuerry(strQuerry, Conn)
    End Sub

    Private Sub cboCustType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustType.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadCmb_VAL(cboCustomer, "select RecId as VAL, CustShortName as DIS" _
                    & " from [CustomerList] where Status='OK' and City='" & myStaff.City _
                    & "' and RecId in (" _
                    & " select Custid from [Cust_Detail] where Status='OK'" _
                    & " and Cat='Channel' and Val='" & cboCustType.Text & "')")
            cboCustomer.SelectedIndex = 0
        End If

    End Sub
End Class