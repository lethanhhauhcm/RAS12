Public Class frmAcrAdd
    Private Sub frmAcrAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtAL.Text = "VJ"
        txtAccountName.Text = "99990051 - VJHAN"
        txtPaxNames.Text = "HAN CREDIT"
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim strQuerry As String = "insert into ACR (AL, AccountName, RcpNo, Documents, PaxNames, Amount, FstUser, Status,City) values ('" _
                    & txtAL.Text & "','" & txtAccountName.Text & "','" & txtRcpNo.Text & "','" & txtDocuments.Text & "','" & txtPaxNames.Text _
                    & "'," & txtAmount.Text & ",'" & myStaff.SICode & "','OK','" & myStaff.City & "')"
        If ExecuteNonQuerry(strQuerry, Conn) Then
            Me.DialogResult = DialogResult.OK
        Else
            MsgBox("Unable to add ACR!")
        End If
    End Sub

    Private Sub lbkSelectRcp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectRcp.LinkClicked
        Dim tblRcp As DataTable = GetDataTable("select distinct top 128 RcpNo,Document from FOP" _
                                                    & " where CustomerId in (68612,5102) And Status='ok' and FOP='PSP' and Document<>''" _
                                                    & " order by RcpNo  desc,Document")
        Dim frmSelectRcp As New frmShowTableContent(tblRcp, "Select RCP", "RcpNo")
        If frmSelectRcp.ShowDialog = DialogResult.OK Then
            txtRcpNo.Text = frmSelectRcp.SelectedRow.Cells("RcpNo").Value
            txtDocuments.Text = frmSelectRcp.SelectedRow.Cells("Document").Value.ToString.Trim
        End If
    End Sub
    Private Function CheckInputValues() As Boolean
        If Not CheckFormatTextBox(txtAmount, True, 1, 12) Then
            Return False
        End If
        If Not CheckFormatTextBox(txtRcpNo, , 12, 12) Then
            Return False
        End If
        If Not CheckFormatTextBox(txtDocuments, , 8, 16) Then
            Return False
        End If
        If ScalarToInt("Rcp", "TOP 1 RecId", "Status='OK' and RcpNo='" & txtRcpNo.Text & "'") = 0 Then
            MsgBox("RcpNo does NOT exist!")
            Return False
        End If
        If ScalarToInt("TOS_TOURCODE", "RecId", "status ='ok' and TourCode='" & txtDocuments.Text & "'") = 0 Then
            MsgBox("TourCode does NOT exist!")
            Return False
        End If
        Return True
    End Function

End Class