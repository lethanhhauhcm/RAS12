Public Class frmRequiedDataList

    Private Sub frmCdrList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect to SQL CWT!")
            Exit Sub
        End If
        pobjTvcs.LoadCombo(cboDataCode, "Select VAL+'-'+Details as Value from GO_Misc where Cat='DataCode' order by Val ")
        pobjTvcs.LoadCustShortNameListAsCombo(cboCustShortName)

        btnClear.PerformClick()
        Search()

    End Sub
    
    Public Function Search() As Boolean
        Dim daConditions As SqlClient.SqlDataAdapter
        Dim dsConditions As New DataSet
        Dim strMsQuerry As String

        strMsQuerry = "Select r.RecId,i.CustShortName,ApplyTo,r.DataCode,NameByCustomer" _
                        & ",MinLength,MaxLength,Mandatory,ConditionOfUse" _
                        & ",CharType,CollectionMethod,DefaultValue,CheckValues" _
                        & ",AllowSpecialValues,r.CustId,m.Details" _
                        & " from GO_RequiredData r left join CustomerList i" _
                        & " on r.custId=i.Recid" _
                        & " left join go_misc m on m.val=r.datacode" _
                        & " where r.Status='OK' and i.Status='OK' and m.cat='DataCode'"

        If cboCustShortName.Text <> "" Then
            strMsQuerry = strMsQuerry & " and r.CustId='" & cboCustShortName.SelectedValue & "'"
        End If

        AddEqualConditionCombo(strMsQuerry, cboMandatory)
        AddEqualConditionCombo(strMsQuerry, cboCollectionMethod)
        AddEqualConditionCombo(strMsQuerry, cboApplyTo)
        AddEqualConditionCheck(strMsQuerry, chkCheckValues)
        AddEqualConditionCheck(strMsQuerry, chkAllowSpecialValues)

        If cboDataCode.Text <> "" Then
            strMsQuerry = strMsQuerry & " and DataCode='" & cboDataCode.Text.Split("-")(0) & "'"
        End If

        strMsQuerry = strMsQuerry & " order by CustShortName, DataCode"

        daConditions = New SqlClient.SqlDataAdapter(strMsQuerry, pobjTvcs.ConnectionString)
        daConditions.Fill(dsConditions, "GO_RequiredData")
        dgrRequiredData.DataSource = dsConditions.Tables("GO_RequiredData")
        dgrRequiredData.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dsConditions.Dispose()
        daConditions.Dispose()
        Return True
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim strQuerry As String
        strQuerry = "update GO_RequiredData set Status='XX',LstUpdate=getdate() where Recid='" _
                    & dgrRequiredData.CurrentRow.Cells.Item(0).Value & "'"
        pobjTvcs.DeleteGridViewRow(dgrRequiredData, strQuerry)
        Search()
    End Sub

    Private Sub btnSeacrh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeacrh.Click
        Search()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        cboCustShortName.Text = ""
        cboDataCode.Text = ""
        cboMandatory.Text = ""
        cboApplyTo.Text = ""
        cboCollectionMethod.Text = ""
        chkCheckValues.CheckState = CheckState.Indeterminate
        chkAllowSpecialValues.CheckState = CheckState.Indeterminate

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim frmRqData As New frmRequiredDataEdit
        frmRqData.ShowDialog()
        Search()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim frmRqData As New frmRequiredDataEdit(dgrRequiredData.CurrentRow)
        frmRqData.ShowDialog()
        Search()

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgrRequiredData.CellClick
        With dgrRequiredData
            If .CurrentRow.Cells("CheckValues").Value Then
                btnUpdateNormalValues.Visible = True
            Else
                btnUpdateNormalValues.Visible = False
            End If
            If .CurrentRow.Cells("AllowSpecialValues").Value Then
                btnUpdateSpecialValues.Visible = True
            Else
                btnUpdateSpecialValues.Visible = False
            End If
        End With
    End Sub

    Private Sub btnUpdateNormalValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateNormalValues.Click
        Dim frmUpdateValues As New frmRequireDataValuesList("NORMAL", dgrRequiredData.CurrentRow)
        frmUpdateValues.ShowDialog()
    End Sub

    Private Sub btnUpdateSpecialValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateSpecialValues.Click
        Dim frmUpdateValues As New frmRequireDataValuesList("SPECIAL", dgrRequiredData.CurrentRow)
        frmUpdateValues.ShowDialog()
    End Sub

    Private Sub btnClone_Click(sender As Object, e As EventArgs) Handles btnClone.Click
        Dim frmRqData As New frmRequiredDataEdit(dgrRequiredData.CurrentRow, True)
        frmRqData.ShowDialog()
        Search()
    End Sub

    Private Sub lbkCloneCustomer_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCloneCustomer.LinkClicked
        If dgrRequiredData.CurrentRow Is Nothing Then Exit Sub

        Dim tblCust As DataTable = pobjTvcs.GetDataTable("select CustId, CustShortName from go_companyinfo1 " _
                                                         & " where status='ok' and (GO_Client+DataCapture)>0" _
                                                         & " and CustId<>" & dgrRequiredData.CurrentRow.Cells("CustId").Value _
                                                         & " order by CustShortName")
        Dim frmSelectCust As New frmShowTableContent(tblCust, "Select New Customer", "CustId", "CustShortName")
        If frmSelectCust.ShowDialog = DialogResult.OK Then
            Dim lstQuerry As New List(Of String)
            lstQuerry.Add("insert into Go_RequiredData (CustId, DataCode, NameByCustomer, Status, MinLength, MaxLength, Mandatory, ConditionOfUse" _
                          & ", CharType, CollectionMethod, DefaultValue, CheckValues, AllowSpecialValues, ApplyTo, Fstuser)" _
                          & " select " & frmSelectCust.SelectedValue & ",DataCode, NameByCustomer, Status, MinLength, MaxLength, Mandatory, ConditionOfUse" _
                          & ", CharType, CollectionMethod, DefaultValue, CheckValues, AllowSpecialValues, ApplyTo,'" & myStaff.SICode _
                          & "' from Go_RequiredData where Status='OK' and CustId =" & dgrRequiredData.CurrentRow.Cells("CustId").Value)
            lstQuerry.Add("insert into GO_RequiredDataValues (CustId, DataCode, DataType, Description, Status, Value, Fstuser)" _
                           & " select " & frmSelectCust.SelectedValue & ",DataCode, DataType, Description, Status, Value,'" & myStaff.SICode _
                           & "' from GO_RequiredDataValues where Status='OK' and CustId =" & dgrRequiredData.CurrentRow.Cells("CustId").Value)
            If pobjTvcs.UpdateListOfQuerries(lstQuerry) Then
                cboCustShortName.SelectedIndex = cboCustShortName.FindStringExact(frmSelectCust.SelectedRow.Cells("CustShortName").Value)
                Search()
            Else
                MsgBox("Unable to Clone Customer!")
            End If
        End If
    End Sub
End Class