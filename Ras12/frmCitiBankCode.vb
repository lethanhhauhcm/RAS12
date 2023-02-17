Public Class frmCitiBankCode

    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd.LinkClicked
        Dim strQuerry As String

        If Not IsNumeric(txtBankCode.Text) Or txtBankCode.TextLength < 7 Then
            MsgBox("Invalid BankCode")
            Exit Sub
        End If
        If ScalarToDec("MISC", "RecId", " Status='OK' and Cat='CitiBankCode' and Val='" _
                       & txtBankCode.Text & "'") > 0 Then
            MsgBox("BankCode Exists! Unable to add duplicates!")
            Exit Sub
        End If
        strQuerry = "insert into Misc (Cat,Val,Val1,Status,FstUser) values ('CitiBankCode','" _
                                  & txtBankCode.Text & "','" & txtBankName.Text _
                                  & "','OK','" & myStaff.SICode & "')"
        If ExecuteNonQuerry(strQuerry, Conn) Then
            Search()
        End If

    End Sub

    Private Sub lbkUpdate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUpdate.LinkClicked
        Dim lstQuerries As New List(Of String)
        lstQuerries.Add("insert into Misc (Cat,Val,Val1,Status,FstUser,City) values ('CitiBankCode','" _
                        & txtBankCode.Text & "','" & txtBankName.Text _
                        & "','OK','" & myStaff.SICode & "','" & myStaff.City & "')")

        lstQuerries.Add("update Misc set Status='XX',LstUpdate=getdate(),'LstUser='" _
                                  & myStaff.SICode & "' Where Recid=" _
                                  & dgrBankCode.CurrentRow.Cells("RecId").Value)
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            Search()
        End If
    End Sub

    Private Sub frmCitiBankCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
    End Sub

    Private Sub dgrBankCode_SelectionChanged(sender As Object, e As EventArgs) Handles dgrBankCode.SelectionChanged
        If dgrBankCode.CurrentRow IsNot Nothing Then
            With dgrBankCode.CurrentRow
                txtBankCode.Text = .Cells("BankCode").Value
                txtBankName.Text = .Cells("BankName").Value
            End With
            'load current mapping
        End If
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select RecId,Val as BankCode, Val1 as BankName from Misc" _
                                  & " where Status<>'XX' and Cat='CitiBankCode'" _
                                  & " order by Val1"
        LoadDataGridView(dgrBankCode, strQuerry, Conn)
        Return True
    End Function
End Class