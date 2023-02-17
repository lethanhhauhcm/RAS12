'^_^20230215 add by 7643
Public Class frmFrieslandNoMXP

    Private Sub txtTCode_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTCode.Validating
        If txtTCode.Text = "" Then Exit Sub

        If ScalarToInt("Dutoan_Tour", "RecID", String.Format("TCode='{0}' and Status<>'XX'", {txtTCode.Text})) = 0 Then
            MsgBox("TCode not available!")
            e.Cancel = True
        End If
    End Sub

    Private Sub txtTCode_Validated(sender As Object, e As EventArgs) Handles txtTCode.Validated
        If txtTCode.Text = "" Then
            txtDutoanID.Text = "0"
            Exit Sub
        End If

        txtDutoanID.Text = ScalarToString("Dutoan_Tour", "RecID", String.Format("TCode='{0}' and Status<>'XX'", {txtTCode.Text}))
    End Sub

    Private Sub frmFrieslandNoMXP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFrieslandNoMXP()
    End Sub

    Private Sub LoadFrieslandNoMXP()
        Dim mSQL As String

        mSQL = String.Format("select RecID,VAL TCode,IntVal DutoanID,FstUpdate,FstUser " &
                             "from MISC " &
                             "where CAT='NoMXP' and Status<>'XX' and City='{0}' order by VAL,IntVal",
                             {myStaff.City})
        LoadDataGridView(dgvFrieslandNoMXP, mSQL, Conn)
    End Sub

    Private Sub btnSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles btnAdd.LinkClicked
        If txtTCode.Text = "" Then
            MsgBox("Please input TCode first!")
            Exit Sub
        End If

        If ScalarToInt("MISC", "RecID", String.Format("VAL='{0}' and Status<>'XX'", {txtTCode.Text})) > 0 Then
            MsgBox("TCode is duplicate!")
            Exit Sub
        End If

        Try
            BeginTrans()

            FCmd.CommandText = String.Format("insert into MISC(CAT,VAL,IntVal,FstUser,City) values('NoMXP','{0}',{1},'{2}','{3}')",
                                                 {txtTCode.Text, CInt(txtDutoanID.Text), myStaff.SICode, myStaff.City})
            FCmd.ExecuteNonQuery()

            CommitTrans()
            LoadFrieslandNoMXP()
        Catch ex As Exception
            MsgBox(ex.Message)
            RollbackTrans()
        End Try

    End Sub

    Private Sub llbDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDelete.LinkClicked
        If MsgBox(String.Format("Are you sure delete TCode '{0}'?", {dgvFrieslandNoMXP.CurrentRow.Cells("TCode").Value}), vbYesNo) = vbNo Then Exit Sub

        Try
            BeginTrans()

            FCmd.CommandText = String.Format("update MISC set status='XX',LstUpdate='{0}',LstUser='{1}' where RecID={2}",
                                             {Format(Now, "yyyyMMdd hh:mm:ss"), myStaff.SICode, CStr(dgvFrieslandNoMXP.CurrentRow.Cells("RecID").Value)})
            FCmd.ExecuteNonQuery()

            CommitTrans()
            LoadFrieslandNoMXP()
        Catch ex As Exception
            MsgBox(ex.Message)
            RollbackTrans()
        End Try
    End Sub

    Private Sub dgvFrieslandNoMXP_DataSourceChanged(sender As Object, e As EventArgs) Handles dgvFrieslandNoMXP.DataSourceChanged
        llbDelete.Enabled = dgvFrieslandNoMXP.Rows.Count > 0
    End Sub
End Class