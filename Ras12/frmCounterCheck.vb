'^_^20220712 add by 7643
Public Class frmCounterCheck
#Region "Custom Method"
    Private Function CheckTKNO() As Boolean
        Dim mSQL, mCustID As String

        mSQL = "select r.CustID from RAS12..TKT t left join RAS12..RCP r on t.RcpId=r.RecId where t.Status<>'XX' and t." & cboTKNO.Text & "='" & txtTKNO.Text & "'"
        mCustID = pobjTvcs.GetScalarAsString(mSQL)
        If mCustID = "" Then
            MsgBox(cboTKNO.Text & " not exist!")
            Return False
        ElseIf Not IsInCustGrp("PWC", "", mCustID) Then
            MsgBox(cboTKNO.Text & " not in PWC group!")
            Return False
        End If

        Return True
    End Function

    Private Sub CounterChecked(xIsCheck As Boolean)
        Dim mSQL As String, mTKNO As New DataTable, i As Integer

        If xIsCheck AndAlso Not CheckTKNO() Then Exit Sub

        If xIsCheck Then
            If cboTKNO.SelectedIndex = 0 Then
                CheckedByCounter(txtTKNO.Text, xIsCheck)
            Else
                mSQL = "select TKNO from RAS12..TKT t where t.Status<>'XX' and t.RCPNo='" & txtTKNO.Text & "'"
                mTKNO = pobjTvcs.GetDataTable(mSQL)
                For i = 0 To mTKNO.Rows.Count - 1
                    CheckedByCounter(mTKNO.Rows(i)("TKNO"), xIsCheck)
                Next
            End If
        Else
            CheckedByCounter(dgvMain.CurrentRow.Cells("VAL1").Value, xIsCheck)
        End If

        If xIsCheck Then
            MsgBox("Counter had checked")
        Else
            MsgBox("Counter had uncheck")
        End If

        LoadMain()
    End Sub

    Private Sub LoadMain()
        Dim mSQL As String

        mSQL = "select RecID,VAL1,VAL,FstUpdate,FstUser,LstUpdate,LstUser from RAS12..MISC where Status<>'XX' and City='" & myStaff.City & "' and CAT='CheckedByCounter' and VAL='True'"
        pobjTvcs.LoadDataGridView(dgvMain, mSQL)

        llbUnCheckByCounter.Enabled = dgvMain.Rows.Count > 0
    End Sub
#End Region

    Private Sub llbCheckByCounter_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbCheckByCounter.LinkClicked
        CounterChecked(True)
    End Sub

    Private Sub lbkUnCheckByCounter_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbUnCheckByCounter.LinkClicked
        CounterChecked(False)
    End Sub

    Private Sub frmCounterCheck_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboTKNO.SelectedIndex = 0
        pobjTvcs.Connect()
        LoadMain()
    End Sub

    Private Sub txtTKNO_TextChanged(sender As Object, e As EventArgs) Handles txtTKNO.TextChanged
        llbCheckByCounter.Enabled = txtTKNO.Text <> ""
    End Sub
End Class