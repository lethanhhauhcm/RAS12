Public Class frmAutoSfEdit
    Private mdgrEditedSf As DataGridViewRow
    Private mblnFirstLoadCompleted As Boolean
    Public Sub New(Optional dgrSf As DataGridViewRow = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim strLoadCust As String = "Select RecId as value, CustShortName as Display" _
            & " from CustomerList where Status='OK' and City='" & myStaff.City & "' and RecId in " _
            & " (select Custid from Cust_Detail" _
            & " where cat='channel' and val='lc' and status='ok')" _
            & " order by CustShortName"
        LoadComboDisplay(cboCustShortName, strLoadCust, Conn)

        If dgrSf IsNot Nothing Then
            With dgrSf
                txtRecId.Text = .Cells("RecId").Value
                cboCustShortName.SelectedIndex = cboCustShortName.FindStringExact(.Cells("CustShortName").Value)
                cboCustShortName.Enabled = False
                txtPct.Text = .Cells("Pct").Value
                dtpValidFrom.Value = .Cells("ValidFrom").Value
                dtpValidTo.Value = .Cells("ValidTo").Value
                'LoadServiceType(cboCustShortName.SelectedValue)
                cboServiceType.SelectedIndex = cboServiceType.FindStringExact(.Cells("ServiceType").Value)
                mdgrEditedSf = dgrSf
            End With
        End If

    End Sub
    'Private Function LoadServiceType(intCustId As Integer) As Boolean
    '    Dim strLoadSvcType As String
    '    strLoadSvcType = "select v.value from cwt.dbo.GO_RequiredData d" _
    '                                & " Left join cwt.dbo.GO_RequiredDataValues v on d.CustId=v.CustId and d.DataCode=v.DataCode" _
    '                                & " where d.Status='ok' and v.Status='ok' and d.NameByCustomer='LEADTIME'" _
    '                                & " and v.CustId=" & cboCustShortName.SelectedValue
    '    LoadCombo(cboServiceType, strLoadSvcType, Conn)
    '    Return True
    'End Function
    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerries As New List(Of String)
        If Not CheckInputValue() Then Exit Sub

        lstQuerries.Add("insert into MISC (CAT, IntVal, IntDec, dteVal, dteVal1,Details, FstUser, Status, City)" _
                        & " values ('AutoSf'," & cboCustShortName.SelectedValue & "," & txtPct.Text _
                        & ",'" & CreateFromDate(dtpValidFrom.Text) & "','" & CreateToDate(dtpValidTo.Value) _
                        & "','" & cboServiceType.Text & "','" & myStaff.SICode & "','OK','" & myStaff.City & "')")
        If txtRecId.Text <> 0 Then
            lstQuerries.Add("Update MISC set Status='XX', lstUpdate=getdate(),lstUser='" _
                            & myStaff.SICode & "',City='" & myStaff.City _
                            & "' where RecId=" & txtRecId.Text)
        End If
        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Unable to update AutoSF")
            Exit Sub
        Else
            Me.DialogResult = DialogResult.OK
            Me.Dispose()
        End If
    End Sub

    Private Function CheckInputValue() As Boolean
        Dim intOvelapedId As Integer
        Dim strDateCondition As String = ""

        If Not CheckFormatTextBox(txtPct, True, 1, 5) Then
            Return False
        End If
        If CInt(txtPct.Text) > 100 Then
            MsgBox("Too high Percentage!")
            Return False
        End If
        If dtpValidFrom.Value > dtpValidTo.Value Then
            MsgBox("Invalid ValidFrom/To!")
            Return False
        End If
        intOvelapedId = ScalarToInt("misc", "recid", "Cat='AutoSf' and Status='OK' and intVal=" _
                                    & cboCustShortName.SelectedValue & " and RecId<>" & txtRecId.Text _
                                    & " and Details='" & cboServiceType.Text _
                                    & "' and City='" & myStaff.City & "'" _
                                    & " and dteVal <='" & CreateFromDate(dtpValidTo.Value) _
                                    & "' and dteVal1>='" & CreateToDate(dtpValidFrom.Value) & "'")
        If intOvelapedId > 0 Then
            MsgBox("Overlap Validity with RecordId" & intOvelapedId)
            Return False
        End If

        Return True
    End Function
    Private Sub frmAutoSfEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'LoadServiceType(cboCustShortName.SelectedValue)
        mblnFirstLoadCompleted = True
    End Sub

    Private Sub cboCustShortName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustShortName.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            'LoadServiceType(cboCustShortName.SelectedValue)
        End If
    End Sub
End Class