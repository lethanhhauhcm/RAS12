Public Class frmCdrHierMap
    Private mobjSql As clsTvcs

    Private Sub frmCdrHierMap_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect to SQL CWT!")
            Exit Sub
        End If
        Search()
    End Sub

    Private Sub dgrMap_SelectionChanged(sender As Object, e As EventArgs) Handles dgrMap.SelectionChanged
        If dgrMap.CurrentRow Is Nothing Then Exit Sub
        With dgrMap.CurrentRow
            txtCMC.Text = .Cells("Cmc").Value
            txtCdr.Text = .Cells("Cdr").Value
            txtHier.Text = .Cells("Hier").Value
        End With
    End Sub

    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd.LinkClicked
        Dim intOldRecId As Integer
        If Not CheckInputValues() Then Exit Sub

        intOldRecId = pobjTvcs.GetScalarAsDecimal("Select top 1 RecId from Go_CdrHierMap where Status='OK' and Cmc='" & txtCMC.Text _
                                                 & "' and Cdr=" & txtCdr.Text & " and Hier=" & txtHier.Text)
        If intOldRecId > 0 Then
            MsgBox("Unable to Add. Duplicated record exists!")
        ElseIf pobjTvcs.ExecuteNonQuerry("insert into Go_CdrHierMap (CMC, CDR, Hier, Status,Fstuser) values (" _
                                         & txtCMC.Text & "," & txtCdr.Text & "," & txtHier.Text _
                                         & ",'OK','" & myStaff.SICode & "')") Then
            Search()
        Else
            MsgBox("Unable to Add new record!")
        End If
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select * from cwt.dbo.Go_CdrHierMap where Status='ok' order by CMC"
        pobjTvcs.LoadDataGridView(dgrMap, strQuerry)

        Return True
    End Function
    Private Function CheckInputValues() As Boolean
        If Not CheckFormatTextBox(txtCMC, True) Then
            MsgBox("Invalid CMC!")
            Return False
        ElseIf pobjTvcs.GetScalarAsDecimal("Select top 1 RecId from Go_CompanyInfo1 where Status='OK' and Cmc='" & txtCMC.Text & "'") = 0 Then
            MsgBox("Invalid CMC!")
            Return False
        ElseIf Not CheckFormatTextBox(txtCdr, True) Then
            MsgBox("Invalid CDR number!")
            Return False
        ElseIf Not CheckFormatTextBox(txtHier, True) Then
            MsgBox("Invalid Hierachy number!")
            Return False
        End If

        Return True
    End Function

    Private Sub lbkUpdate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUpdate.LinkClicked
        Dim lstQuerries As New List(Of String)
        If dgrMap.CurrentRow Is Nothing Then Exit Sub
        If Not CheckInputValues() Then Exit Sub

        lstQuerries.Add("insert into Go_CdrHierMap (CMC, CDR, Hier, Status,Fstuser) values (" _
                                         & txtCMC.Text & "," & txtCdr.Text & "," & txtHier.Text _
                                         & ",'OK','" & myStaff.SICode & "')")
        lstQuerries.Add("Update Go_CdrHierMap set Status='xx',LstUpdate=getdate(),lstUser='" _
                                      & myStaff.SICode & "' where RecId=" & dgrMap.CurrentRow.Cells("RecId").Value)

        If pobjTvcs.UpdateListOfQuerries(lstQuerries) Then
            Search()
        Else
            MsgBox("Unable to Add new record!")
        End If

    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If dgrMap.CurrentRow Is Nothing Then Exit Sub
        If pobjTvcs.DeleteGridViewRow(dgrMap, "Update Go_CdrHierMap set Status='xx',LstUpdate=getdate(),lstUser='" _
                                      & myStaff.SICode & "' where RecId=" & dgrMap.CurrentRow.Cells("RecId").Value) Then
            Search()
        End If
    End Sub
End Class