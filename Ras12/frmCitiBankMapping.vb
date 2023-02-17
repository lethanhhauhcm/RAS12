Public Class frmCitiBankMapping
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearchTV.LinkClicked
        SearchTv()
    End Sub

    Private Sub lbkShowAllTv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkShowAllTv.LinkClicked
        txtTvBankName.Text = ""
        SearchTv()
    End Sub

    Private Sub frmCitiBankCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strQuerry As String = "Select Val as BankCode, Val1 as BankName" _
                                  & " from MISC where Status='OK' and Cat='CitiBankCode'" _
                                  & " order by Val1"
        LoadDataGridView(dgrCitiBankCode, strQuerry, Conn)

        strQuerry = "insert MISC (cat,Val,Val1,Val2,Status,FstUser) " _
            & " select 'CitiBankMap','','',BankName,'OK','" & myStaff.SICode _
            & "' from Vendor where Status='OK' and BankInVietnam='Y'" _
            & " and BankName not in" _
            & " (Select Val2 from MISC where Status='OK' and Cat='CitiBankMap')"
        ExecuteNonQuerry(strQuerry, Conn)

        SearchTv()
        SearchCiti()
    End Sub

    
    Private Function SearchTv() As Boolean
        Dim strQuerry As String = "Select RecId,Val2 as TvBankName,Val as CitiBankCode" _
                                  & ", Val1 as CitiBankName from Misc" _
                                  & " where Status<>'XX' and Cat='CitiBankMap'" _
                                  & " and Val=''"

        If txtTvBankName.Text <> "" Then
            strQuerry = strQuerry & " and Val2 like'%" & txtTvBankName.Text & "%'"
        End If

        strQuerry = strQuerry & "  order by Val2"
        LoadDataGridView(dgrTvBank, strQuerry, Conn)
        Return True
    End Function
    Private Function SearchCiti() As Boolean
        Dim strQuerry As String = "Select RecId,Val as CitiBankCode" _
                                  & ", Val1 as CitiBankName from Misc" _
                                  & " where Status<>'XX' and Cat='CitiBankCode'" 


        If txtBankCode.Text <> "" Then
            strQuerry = strQuerry & " and Val like'%" & txtBankCode.Text & "%'"
        End If
        If txtBankName.Text <> "" Then
            strQuerry = strQuerry & " and Val1 like'%" & txtBankName.Text & "%'"
        End If

        Select Case chkIncludeAgriBankl.CheckState
            Case CheckState.Checked
                'strQuerry = strQuerry & " and Val1 like'%NO%' and Val1 like'%PTNT%'"
            Case CheckState.Unchecked
                strQuerry = strQuerry & " and not (Val1 like'%NO%' and Val1 like'%PTNT%')"

        End Select

        strQuerry = strQuerry & "  order by Val1"
        LoadDataGridView(dgrCitiBankCode, strQuerry, Conn)
        Return True
    End Function
    Private Sub dgrBankCode_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrTvBank.CellContentClick

    End Sub

    Private Sub dgrBankCode_SelectionChanged(sender As Object, e As EventArgs) Handles dgrTvBank.SelectionChanged
        If dgrTvBank.CurrentRow IsNot Nothing Then
            With dgrTvBank.CurrentRow
                If .Cells("CitiBankCode").Value = "" Then
                    lbkMapCitiBankCode.Enabled = True
                    lbkOutSideVietnam.Enabled = True
                Else
                    lbkMapCitiBankCode.Enabled = False
                    lbkOutSideVietnam.Enabled = False
                End If
            End With
        End If
    End Sub

    Private Sub lbkSetCitiBankCode_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkMapCitiBankCode.LinkClicked

        Dim strQuerry As String

        If ContainSpecialChar4Citi(dgrTvBank.CurrentRow.Cells("RecId").Value) Then
            MsgBox("Bank name contains prohibited character")
            Exit Sub
        End If

        With dgrCitiBankCode.CurrentRow
            strQuerry = "Update MISC set Val='" & .Cells("CitiBankCode").Value _
                                  & "',Val1='" & .Cells("CitiBankName").Value _
                                  & "' where Recid=" _
                                  & dgrTvBank.CurrentRow.Cells("RecId").Value
        End With

        If ExecuteNonQuerry(strQuerry, Conn) Then
            SearchTv()
        End If
    End Sub

    Private Sub lbkOutSideVietnam_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkOutSideVietnam.LinkClicked
        Dim lstQuerries As New List(Of String)
        lstQuerries.Add("Delete Misc where Recid=" & dgrTvBank.CurrentRow.Cells("RecId").Value)
        'lstQuerries.Add("Update UNC_Accounts set BankInVietnam='N'" _
        '                & " where Status='OK' and BankInVietnam=''" _
        '                & " and BankName=N'" & dgrTvBank.CurrentRow.Cells("TvBankName").Value & "'")

        If UpdateListOfQuerries(lstQuerries, Conn) Then
            SearchTv()
        End If
    End Sub

    Private Sub lbkSearchCiti_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearchCiti.LinkClicked
        SearchCiti()
    End Sub

    Private Sub lbkShowAllCiti_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkShowAllCiti.LinkClicked
        txtBankCode.Text = ""
        txtBankName.Text = ""
        chkIncludeAgriBankl.Checked = True
        SearchCiti()
    End Sub

    Private Sub chkIncludeAgriBankl_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncludeAgriBankl.CheckedChanged
        SearchCiti()
    End Sub

    Private Sub lbkViewVendors_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewVendors.LinkClicked
        Dim tblVendor As DataTable
        tblVendor = GetDataTable("select RecID, AccountName, AccountNumber,Address, BankName" _
                                & ", BankAddress, Swift from Vendor " _
                                & " where status ='OK' and BankName='" _
                                & dgrTvBank.CurrentRow.Cells("TvBankName").Value & "'")
        Dim frmView As New frmShowTableContent(tblVendor, "List of Vendors")
        frmView.ShowDialog()
    End Sub
End Class