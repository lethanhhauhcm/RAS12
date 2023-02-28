Public Class frmRptDataEdit
    Private mintOldCust As Integer
    Private mstrOldDataCode As String

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim lstQuerries As New List(Of String)
        Dim intCdrNbr As Integer
        If txtNameByCustomer.Text = "" Then
            MsgBox("You must input NameByCustomer!")
            Exit Sub
        End If

        intCdrNbr = pobjTvcs.GetScalarAsDecimal("select TOP 1 CdrNbr From [GO_CDRs]" _
                      & " where Status='OK' and CdrName='" & txtNameByCustomer.Text & "' and CMC=" _
                      & " (Select top 1 Cmc from go_companyinfo1 where status='OK' and CustShortName='" _
                      & cboCustShortName.Text & "')")
        If intCdrNbr > 0 Then
            MsgBox("Name by Customer must not duplicate with CDR" & intCdrNbr)
            Exit Sub
        End If
        If cboDataCode.Text = "" Then
            MsgBox("You must select DataCode!")
            Exit Sub
        End If
        If cboCharType.Text = "" Then
            MsgBox("You must select CharType option!")
            Exit Sub
        End If
        If Not CheckFormatTextBox(txtMinLength, True) _
        Or Not CheckFormatTextBox(txtMaxLength, True) Then
            Exit Sub
        End If

        If cboCollectionMethod.Text = "" Then
            MsgBox("You must select Collection Method!")
            Exit Sub
        End If

        If cboMandatory.Text = "C" AndAlso txtConditionOfUse.Text = "" Then
            MsgBox("You must specify ConditionOfUse!")
            Exit Sub
        ElseIf cboMandatory.Text <> "C" AndAlso txtConditionOfUse.Text <> "" Then
            MsgBox("You must Not specify ConditionOfUse!")
            Exit Sub
        End If

        If cboApplyTo.Text = "" Then
            MsgBox("You must specify ApplyTo!")
            Exit Sub
        End If

        lstQuerries.Add("Insert into RAS12.DBO.RptData (CustId, DataCode,NameByCustomer" _
                    & ",Status,MinLength,MaxLength,Mandatory,ConditionOfUse" _
                    & ",CharType,CollectionMethod,DefaultValue,CheckValues" _
                    & ",AllowSpecialValues,Fstuser,ApplyTo) values (" _
                    & cboCustShortName.SelectedValue _
                    & ",'" & cboDataCode.Text _
                    & "','" & txtNameByCustomer.Text & "','OK','" _
                    & txtMinLength.Text & "','" & txtMaxLength.Text _
                    & "','" & cboMandatory.Text _
                    & "','" & txtConditionOfUse.Text & "','" & cboCharType.Text _
                    & "','" & cboCollectionMethod.Text & "','" & txtDefaultValue.Text _
                    & "','" & chkCheckValues.Checked & "','" & chkAllowSpecialValues.Checked _
                    & "','" & myStaff.SICode & "','" & cboApplyTo.Text & "')")


        If txtRecID.Text <> "" Then

            lstQuerries.Add("update RAS12.DBO.RptData set Status='XX'" _
                            & ",LstUser='" & myStaff.SICode & "',LstUpdate=getdate()" _
                            & " where Recid=" & txtRecID.Text)
        End If

        If mintOldCust <> 0 Then
            lstQuerries.Add("insert RAS12.DBO.RptDataValues (CustId, DataCode, DataType, Description" _
                            & ", Value, FstUpdate, Fstuser)" _
                            & " select " & cboCustShortName.SelectedValue _
                            & ",'" & cboDataCode.Text _
                            & "', DataType, Description, Value, getdate(),'" _
                            & myStaff.SICode & "' from RAS12.DBO.RptDataValues where Status='ok' and Custid=" & mintOldCust & " and DataCode='" _
                            & mstrOldDataCode & "'")
        End If

        If Not pobjTvcs.UpdateListOfQuerries(lstQuerries) Then
            MsgBox("Unable to add RequiedData!")
        Else
            Me.Dispose()
            Me.DialogResult = DialogResult.OK
        End If


    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
        Me.DialogResult = DialogResult.Cancel
    End Sub


    Public Sub New(Optional ByVal objRow As DataGridViewRow = Nothing, Optional blnClone As Boolean = False)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        pobjTvcs.LoadCustShortNameListAsCombo(cboCustShortName)

        If Not objRow Is Nothing Then
            With objRow


                cboCustShortName.SelectedIndex = cboCustShortName.FindStringExact(.Cells("CustShortName").Value)
                cboApplyTo.SelectedIndex = cboApplyTo.FindStringExact(.Cells("ApplyTo").Value)
                cboDataCode.SelectedIndex = cboDataCode.FindStringExact(
                            .Cells("DataCode").Value)

                If blnClone Then
                    mintOldCust = cboCustShortName.SelectedValue
                    mstrOldDataCode = cboDataCode.SelectedValue.split("-")(0)
                Else
                    txtRecID.Text = .Cells("RecId").Value
                End If

                txtNameByCustomer.Text = .Cells("NameByCustomer").Value
                txtMinLength.Text = .Cells("MinLength").Value
                txtMaxLength.Text = .Cells("MaxLength").Value
                cboMandatory.Text = .Cells("Mandatory").Value
                txtConditionOfUse.Text = .Cells("ConditionOfUse").Value
                cboCharType.Text = .Cells("CharType").Value
                cboCollectionMethod.Text = .Cells("CollectionMethod").Value
                txtDefaultValue.Text = .Cells("DefaultValue").Value
                chkCheckValues.Checked = .Cells("CheckValues").Value
                chkAllowSpecialValues.Checked = .Cells("AllowSpecialValues").Value

            End With
        End If
    End Sub
End Class