Public Class frmMapBankProvince
    Private mblnFirstLoadCompleted As Boolean
    Private Sub frmMapBankProvince_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim tblBanks As DataTable = GetDataTable("select strVal1 from lib.dbo.Misc where cat='BankMap' and City='" & myStaff.City & "'")
        Dim lstQuerries As New List(Of String)

        For Each objRow As DataRow In tblBanks.Rows
            lstQuerries.Add("Insert lib.dbo.BankMapProvince (BankAddress, UsedBy, City) select distinct(BankAddress),'" _
                            & objRow("strVal1") & "','" & myStaff.City & "' from Vendor where Status='OK' and BankAddress<>'' and BankAddress not in" _
                            & "(select distinct BankAddress from lib.dbo.BankMapProvince where UsedBY='" & objRow("strVal1") & "')")
        Next
        UpdateListOfQuerries(lstQuerries, Conn)
        cboUsedBy.SelectedIndex = 0
        mblnFirstLoadCompleted = True
        LoadProvinceList()
        Search()
    End Sub
    Private Function LoadProvinceList() As Boolean
        If mblnFirstLoadCompleted Then
            LoadCombo(cboProvince, "Select 'N/A' as value UNION SELECT distinct Province as value from lib.dbo.BankBranches b left join lib.dbo.BankIDs i on b.BankId=i.RecId" _
                                    & " where i.Status='OK' and b.Status='OK' and i.UsedBy='" & cboUsedBy.Text & "'", Conn)
        End If
        Return True
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select * from lib.dbo.BankMapProvince where UsedBy='" & cboUsedBy.Text & "'"

        AddEqualConditionCombo(strQuerry, cboProvince)
        AddLikeConditionText(strQuerry, txtBankAddress)
        strQuerry = strQuerry & " order by Province,BankAddress"

        LoadDataGridView(dgrProvinceMap, strQuerry, Conn)
        Return True
    End Function
    Private Function Clear() As Boolean
        cboProvince.SelectedIndex = -1
        txtBankAddress.Text = ""
    End Function

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub dgrProvinceMap_SelectionChanged(sender As Object, e As EventArgs) Handles dgrProvinceMap.SelectionChanged
        If mblnFirstLoadCompleted Then
            txtCurrentProvince.Text = dgrProvinceMap.CurrentRow.Cells("Province").Value
            txtNewProvince.Text = ""
        End If
    End Sub

    Private Sub lbkMapProvince_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkMapProvince.LinkClicked
        If dgrProvinceMap.CurrentRow Is Nothing Then Exit Sub
        Dim frmMap As New frmGetBankProvince("TCB", dgrProvinceMap.CurrentRow.Cells("BankAddress").Value)
        If frmMap.ShowDialog = DialogResult.OK Then
            txtNewProvince.Text = frmMap.SelectedProvince
            If ExecuteNonQuerry("Update lib.dbo.BankMapProvince set Province='" & frmMap.SelectedProvince & "' where RecId=" & dgrProvinceMap.CurrentRow.Cells("RecId").Value, Conn) Then
                Search()
            Else
                MsgBox("Unable to Map Province!")
            End If
        End If
    End Sub
End Class