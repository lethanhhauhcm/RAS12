Public Class frmRptDataList
    Private Sub frmRptDataList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strCustList As String
        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect SQL")
            Me.Dispose()
            Exit Sub
        End If
        strCustList = "Select distinct CustShortName as Display,CustId as Value" _
                    & " from cwt.dbo.go_companyinfo1 where Status='OK' order by CustShortName"
        LoadComboDisplay(cboCustShortName, strCustList, Conn)
        cboStatus.SelectedIndex = 0
    End Sub
    Public Function Search() As Boolean
        Dim daConditions As SqlClient.SqlDataAdapter
        Dim dsConditions As New DataSet
        Dim strMsQuerry As String

        strMsQuerry = "Select r.RecId,i.CustShortName,ApplyTo,r.DataCode,NameByCustomer" _
                        & ",MinLength,MaxLength,Mandatory,ConditionOfUse" _
                        & ",CharType,CollectionMethod,DefaultValue,CheckValues" _
                        & ",AllowSpecialValues,r.CustId,r.Status" _
                        & " from RAS12.DBO.RptData r left join CustomerList i" _
                        & " on r.custId=i.Recid" _
                        & " where i.Status='OK'"

        If cboCustShortName.Text <> "" Then
            strMsQuerry = strMsQuerry & " and r.CustId='" & cboCustShortName.SelectedValue & "'"
        End If

        AddEqualConditionCombo(strMsQuerry, cboMandatory)
        AddEqualConditionCombo(strMsQuerry, cboCollectionMethod)
        AddEqualConditionCombo(strMsQuerry, cboApplyTo)
        AddEqualConditionCheck(strMsQuerry, chkCheckValues)
        AddEqualConditionCheck(strMsQuerry, chkAllowSpecialValues)
        AddEqualConditionCombo(strMsQuerry, cboStatus, "r.Status")
        If cboDataCode.Text <> "" Then
            strMsQuerry = strMsQuerry & " and DataCode='" & cboDataCode.Text.Split("-")(0) & "'"
        End If

        strMsQuerry = strMsQuerry & " order by CustShortName, DataCode"

        daConditions = New SqlClient.SqlDataAdapter(strMsQuerry, pobjTvcs.ConnectionString)
        daConditions.Fill(dsConditions, "RptData")
        dgrRptData.DataSource = dsConditions.Tables("RptData")
        dgrRptData.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dsConditions.Dispose()
        daConditions.Dispose()
        Return True
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim strQuerry As String
        strQuerry = "update ras12.dbo.RptData set Status='XX',LstUser='" & myStaff.SICode _
                    & "',LstUpdate=getdate() where Recid='" _
                    & dgrRptData.CurrentRow.Cells("RecId").Value & "'"
        pobjTvcs.DeleteGridViewRow(dgrRptData, strQuerry)
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
        cboStatus.SelectedIndex = 0
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim frmRqData As New frmRptDataEdit
        frmRqData.ShowDialog()
        Search()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim frmRqData As New frmRptDataEdit(dgrRptData.CurrentRow)
        frmRqData.ShowDialog()
        Search()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgrRptData.CellClick
        With dgrRptData
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
        Dim frmUpdateValues As New frmRptDataValuesList("NORMAL", dgrRptData.CurrentRow)
        frmUpdateValues.ShowDialog()
    End Sub

    Private Sub btnUpdateSpecialValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateSpecialValues.Click
        Dim frmUpdateValues As New frmRptDataValuesList("SPECIAL", dgrRptData.CurrentRow)
        frmUpdateValues.ShowDialog()
    End Sub

    Private Sub btnClone_Click(sender As Object, e As EventArgs) Handles btnClone.Click
        Dim frmRqData As New frmRequiredDataEdit(dgrRptData.CurrentRow, True)
        frmRqData.ShowDialog()
        Search()
    End Sub

    Private Sub lbkCloneCustomer_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCloneCustomer.LinkClicked
        If dgrRptData.CurrentRow Is Nothing Then Exit Sub

        Dim tblCust As DataTable = pobjTvcs.GetDataTable("select CustId, CustShortName from go_companyinfo1 " _
                                                         & " where status='ok' and (GO_Client+DataCapture)>0" _
                                                         & " and CustId<>" & dgrRptData.CurrentRow.Cells("CustId").Value _
                                                         & " order by CustShortName")
        Dim frmSelectCust As New frmShowTableContent(tblCust, "Select New Customer", "CustId", "CustShortName")
        If frmSelectCust.ShowDialog = DialogResult.OK Then
            Dim lstQuerry As New List(Of String)
            lstQuerry.Add("insert into RptData (CustId, DataCode, NameByCustomer, Status, MinLength, MaxLength, Mandatory, ConditionOfUse" _
                          & ", CharType, CollectionMethod, DefaultValue, CheckValues, AllowSpecialValues, ApplyTo, Fstuser)" _
                          & " select " & frmSelectCust.SelectedValue & ",DataCode, NameByCustomer, Status, MinLength, MaxLength, Mandatory, ConditionOfUse" _
                          & ", CharType, CollectionMethod, DefaultValue, CheckValues, AllowSpecialValues, ApplyTo,'" & myStaff.SICode _
                          & "' from RptData where Status='OK' and CustId =" & dgrRptData.CurrentRow.Cells("CustId").Value)
            lstQuerry.Add("insert into RptDataValues (CustId, DataCode, DataType, Description, Status, Value, Fstuser)" _
                           & " select " & frmSelectCust.SelectedValue & ",DataCode, DataType, Description, Status, Value,'" & myStaff.SICode _
                           & "' from RptDataValues where Status='OK' and CustId =" & dgrRptData.CurrentRow.Cells("CustId").Value)
            If pobjTvcs.UpdateListOfQuerries(lstQuerry) Then
                cboCustShortName.SelectedIndex = cboCustShortName.FindStringExact(frmSelectCust.SelectedRow.Cells("CustShortName").Value)
                Search()
            Else
                MsgBox("Unable to Clone Customer!")
            End If
        End If
    End Sub
End Class