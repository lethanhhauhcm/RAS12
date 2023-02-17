Public Class frmRequiredDataAdd

    Dim mstrCdrpQuerry As String

    Public Property CdrpQuerry() As String
        Get
            CdrpQuerry = mstrCdrpQuerry
        End Get
        Set(ByVal value As String)
            mstrCdrpQuerry = value
        End Set
    End Property
    Private Sub frmCdrAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strCorpQuerry As String

        strCorpQuerry = "Select distinct CMC as Value,CompanyName as Display from GO_CompanyInfo1" _
                        & " where GO_client=1 order by CompanyName"

        pobjTvcs.LoadComboDisplay(cboCorporate, strCorpQuerry)
        cboCorporate.Text = ""
    End Sub
    Public Function Search() As Boolean
        Dim daConditions As SqlClient.SqlDataAdapter
        Dim dsConditions As New DataSet

        mstrCdrpQuerry = "Select distinct GO_CDRs.Recid,GO_CDRs.CMC,GO_CDRs.CdrNbr,GO_CDRs.CdrName" _
                           & ",GO_CompanyInfo1.CompanyName as CorpName,MinLength,MaxLength" _
                           & ",CharType,AgtInput" _
                           & " from GO_CDRs,GO_CompanyInfo1" _
                           & " where GO_CDRs.Status='OK' and GO_CDRs.CMC=GO_CompanyInfo1.CMC"

        If cboCorporate.Text <> "" Then
            mstrCdrpQuerry = mstrCdrpQuerry & " and GO_CDRs.CMC='" & cboCorporate.SelectedValue & "'"
        Else
            mstrCdrpQuerry = mstrCdrpQuerry & " and GO_CDRs.CMC=''"
        End If

        mstrCdrpQuerry = mstrCdrpQuerry & " order by CompanyName"

        daConditions = New SqlClient.SqlDataAdapter(mstrCdrpQuerry, pobjTvcs.ConnectionString)
        daConditions.Fill(dsConditions, "GO_MISC")
        DataGridView1.DataSource = dsConditions.Tables("GO_MISC")
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dsConditions.Dispose()
        daConditions.Dispose()
        Search = True
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim ArrQuerry(0 To 0) As String

        If txtMeaning.Text = "" Then
            MsgBox("You must input meaning for CDR!")
            Exit Sub
        End If
        If cboCdrCode.Text = "" Then
            MsgBox("You must select CDR code!")
            Exit Sub
        End If
        If Not IsNumeric(txtMinLength.Text) Then
            MsgBox("You must input Minimum Length!")
            Exit Sub
        End If
        If Not IsNumeric(txtMaxLength.Text) Then
            MsgBox("You must input Maximum Length!")
            Exit Sub
        End If
        If txtMinLength.Text > 25 Then
            MsgBox("Minimum Length can not be over 25 characters!")
            Exit Sub
        End If
        If txtMaxLength.Text > 25 Then
            MsgBox("Maximum Length can not be over 25 characters!")
            Exit Sub
        End If
        ArrQuerry(0) = "Insert into GO_CDRs (CMC,CdrNbr,CdrName,Status,MinLength,MaxLength" _
                        & ",CharType,AgtInput) values ('" _
                        & cboCorporate.SelectedValue & "','" & cboCdrCode.Text & "','" _
                        & txtMeaning.Text & "','OK'," & txtMinLength.Text _
                        & "," & txtMaxLength.Text & ",'" & cboCharType.Text _
                        & "','" & chkAgtInput.CheckState & "')"

        pobjTvcs.Update(ArrQuerry)
        cboCdrCode.Text = ""
        txtMeaning.Text = ""
        Search()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub cboCorporate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCorporate.SelectionChangeCommitted
        Search()
    End Sub

End Class