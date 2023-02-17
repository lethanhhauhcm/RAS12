Public Class frmMapBankName
    Private mblnFirstLoadCompleted As Boolean
    Private Sub frmMapBankProvince_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim tblBanks As DataTable = GetDataTable("select strVal1 from lib.dbo.Misc where cat='BankMap' and City='" & myStaff.City & "'")
        Dim lstQuerries As New List(Of String)

        For Each objRow As DataRow In tblBanks.Rows
            lstQuerries.Add("Insert lib.dbo.BankMapName (BankNameByTv, UsedBy, City) select distinct(BankName),'" _
                                & objRow("strVal1") & "','" & myStaff.City & "' from Vendor where Status='OK' and BankName<>'' and BankName not in" _
                                & "(select distinct BankNameByTV from lib.dbo.BankMapName where UsedBy='" & objRow("strVal1") & "')")
        Next
        UpdateListOfQuerries(lstQuerries, Conn)
        cboUsedBy.SelectedIndex = 0
        mblnFirstLoadCompleted = True
        LoadBankList()
        Clear()
        Search()
    End Sub
    Private Function LoadBankList() As Boolean
        If mblnFirstLoadCompleted Then
            Dim strQuerry As String = "SELECT distinct BankName as value from lib.dbo.BankBranches b left join lib.dbo.BankIDs i on b.BankId=i.RecId" _
                                        & " where i.Status='OK' and b.Status='OK' and i.UsedBy='" & cboUsedBy.Text & "'"
            LoadCombo(cboBankNameByBank, strQuerry, Conn)
            LoadCombo(cboNewBankNameByBank, strQuerry, Conn)
        End If
        Return True
    End Function

    Private Function Search() As Boolean
        Dim strQuerry As String = "Select * from lib.dbo.BankMapName where UsedBy='" & cboUsedBy.Text & "'"

        AddEqualConditionCombo(strQuerry, cboBankNameByBank)
        AddLikeConditionText(strQuerry, txtBankNameByTV)
        strQuerry = strQuerry & " order by BankNameByBank, BankNameByTV"

        LoadDataGridView(dgrBankNameMap, strQuerry, Conn)
        dgrBankNameMap.Columns("BankNameByTV").Width = 200
        Return True
    End Function

    Private Function Clear() As Boolean
        cboBankNameByBank.SelectedIndex = -1
        txtBankNameByTV.Text = ""
    End Function


    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub dgrProvinceMap_SelectionChanged(sender As Object, e As EventArgs) Handles dgrBankNameMap.SelectionChanged
        If mblnFirstLoadCompleted Then
            txtCurrentBankName.Text = dgrBankNameMap.CurrentRow.Cells("BankNameByBank").Value
            'cboNewBankNameByBank.SelectedIndex = -1
        End If
    End Sub


    Private Sub lbkMapBankName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkMapBankName.LinkClicked
        If dgrBankNameMap.CurrentRow Is Nothing OrElse cboNewBankNameByBank.Text = "" Then Exit Sub
        If ExecuteNonQuerry("Update lib.dbo.BankMapName set BankNameByBank='" & cboNewBankNameByBank.Text & "' where RecId=" & dgrBankNameMap.CurrentRow.Cells("RecId").Value, Conn) Then
            Search()
        Else
            MsgBox("Unable to Map Bank Name!")
        End If
    End Sub

End Class