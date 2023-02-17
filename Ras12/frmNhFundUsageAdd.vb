Imports System.Data
Public Class frmNhFundUsageAdd
    Private mblnFirstLoadCompleted As Boolean
    Private mtblFund As DataTable
    Private mintCustId As Integer


    Private Sub frmNhFundUsageAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboDisplay(cboCustomer, "Select distinct c.CustShortName as Display" _
                         & ",f.CustId as Value " _
                         & " from NhFund f" _
                         & " left join CustomerList c on f.CustId=c.RecId" _
                         & " order by c.CustShortName", Conn)
        mblnFirstLoadCompleted = True
        cboCustomer.SelectedIndex = -1
    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerries As New List(Of String)
        Dim intUsageId As Integer
        Dim decUsdAmt As Decimal

        If Not CheckInputValues() Then
            Exit Sub
        End If

        If cboWaiverType.Text = "Add2Fund" Then
            decUsdAmt = 0 - txtAmount.Text
        Else
            decUsdAmt = txtAmount.Text
        End If

        lstQuerries.Add("insert into NhFundUsage (RequestDate,FundId, Seq, Cur, Amount, CustId" _
                    & ", RLOC, WaiverType, FstUser, Status,NhApproved) Values ('" _
                    & dtpRequestDate.Text & "'," & txtFundId.Text & "," _
                    & "(Select isnull(max(Seq),0)+1 from NhFundUsage)" _
                    & ",'USD'," & decUsdAmt & "," & mintCustId _
                    & ",'" & txtRloc.Text & "','" & cboWaiverType.Text _
                    & "','" & myStaff.SICode & "','XX','" & chkNhApproved.Checked & "')")

        If Not UpdateListOfQuerries(lstQuerries, Conn, True, intUsageId) Then
            MsgBox("Unable to create!")
            Exit Sub
        Else
            lstQuerries.Clear()
        End If
        For Each objRow As DataGridViewRow In dgrOldTicket.Rows
            If objRow.Cells(0).Value <> "" Then
                lstQuerries.Add("insert into Misc (Cat,Val,Val1,City) values('NhOldTkt'," _
                                & intUsageId & ",'" & objRow.Cells(0).Value & "','" & myStaff.City & "')")
            End If
        Next
        '
        lstQuerries.Add("Update NhFundUsage set Status='OK' where RecId=" & intUsageId)
        lstQuerries.Add("insert NhFund (FundId, Seq, Cur, Amount, ValidFrom, ValidTo, CustId" _
                       & ", FstUser,Status, CreatedByUsageId)" _
                       & " select top 1 FundId, Seq+1, Cur, Amount" & "-(" & decUsdAmt & ")" _
                       & ", ValidFrom, ValidTo, CustId,'" & myStaff.SICode _
                       & "', Status," & intUsageId _
                       & " from NhFund where Status='OK' and CustId=" & mintCustId _
                       & " order by RecId desc")
        lstQuerries.Add("update NhFund set Status='RR' where RecId=" _
                       & " (select top 1 RecId" _
                       & " from NhFund where Status='OK' and CustId=" & mintCustId _
                       & " and FundId=" & txtFundId.Text _
                       & " and CreatedByUsageId<>" & intUsageId _
                       & " order by RecId desc)")

        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Unable to create!")
            Exit Sub
        End If
        Me.DialogResult = DialogResult.OK
        Me.Dispose()
    End Sub
    Private Function CheckInputValues() As Boolean

        If cboWaiverType.Text <> "Add2Fund" AndAlso Not CheckFormatTextBox(txtRloc, False, 6, 6) Then
            Return False
        End If

        If Not CheckFormatTextBox(txtAmount, True, 1, 5) Then
            Return False
        End If

        For Each objRow As DataGridViewRow In dgrOldTicket.Rows
            If objRow.Cells(0).Value <> "" Then
                If Not CheckFormatTkno(objRow.Cells(0).Value) _
                    Or Mid(objRow.Cells(0).Value, 1, 3) <> "205" Then
                    MsgBox("Invalid ticket number")
                    Return False
                End If
            End If
        Next

        mtblFund = GetDataTable("Select top 1 f.* ,c.RecId from NhFund f" _
                                & " left join CustomerList c on c.Recid=f.CustId " _
                                & " where f.Status='OK' and c.Status='ok'" _
                                & " and f.CustId=" & mintCustId _
                                & " order by f.RecId desc", Conn)
        If mtblFund.Rows.Count = 0 Then
            MsgBox("Fund is NOT available for use")
            Me.Dispose()
        ElseIf cboWaiverType.Text <> "Add2Fund" AndAlso txtAmount.Text > mtblFund.Rows(0)("Amount") Then
            MsgBox("Insufficient Fund")
            Return False
        End If

        Return True
    End Function

    Private Sub cboWaiverType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWaiverType.SelectedIndexChanged

        txtAmount.ReadOnly = True
        If chkNhApproved.Checked Then
            txtAmount.ReadOnly = False
        End If
        Select Case cboWaiverType.Text
            Case "SF1", "Add2Fund"
                txtAmount.Text = ""
                txtAmount.ReadOnly = False
            Case "SF2"
                txtAmount.Text = 50
            Case "SF3"
                txtAmount.Text = 200
            Case "SF4"
                txtAmount.Text = 100
            Case "SF5"
                txtAmount.Text = 350
            Case "SF6"
                txtAmount.Text = 300
        End Select

        If cboWaiverType.Text = "Add2Fund" Then
            lblAmount.Text = "Added USD"
        Else
            lblAmount.Text = "Deducted USD"
        End If
    End Sub

    Private Sub cboCustomer_LostFocus(sender As Object, e As EventArgs) Handles cboCustomer.LostFocus
        Dim tblUnfinalizedUsage As DataTable
        If cboCustomer.Text = "" Or cboCustomer.Text = "System.Data.DataRowView" Then
            Exit Sub
        End If

        tblUnfinalizedUsage = GetDataTable("Select * from NhFundUsage where Status='OK'" _
                                            & " and CustId=" & mintCustId _
                                            & " and not RecId in " _
                                            & "(Select cast(Val as int) from MISC where Cat='NhNewTkt')")
        If tblUnfinalizedUsage.Rows.Count > 0 Then
            MsgBox("Please add New Ticket for existing Usage before creation of New Usage")
        End If

        mtblFund = GetDataTable("Select top 1 f.* ,c.RecId from NhFund f" _
                                & " left join CustomerList c on c.Recid=f.CustId " _
                                & " where f.Status='OK' and c.Status='ok'" _
                                & " and f.CustId=" & cboCustomer.SelectedValue _
                                & " order by f.RecId desc", Conn)
        If mtblFund.Rows.Count = 0 Then
            MsgBox("Fund is NOT available for use")
            Me.Dispose()
        Else
            mintCustId = mtblFund.Rows(0)("CustId")
            txtFundId.Text = mtblFund.Rows(0)("FundId")
            txtResidual.Text = mtblFund.Rows(0)("Amount")
            cboWaiverType.SelectedIndex = 0
            dtpRequestDate.MinDate = mtblFund.Rows(0)("ValidFrom")
            dtpRequestDate.MaxDate = mtblFund.Rows(0)("Validto")

        End If
    End Sub

    Private Sub chkNhApproved_CheckedChanged(sender As Object, e As EventArgs) Handles chkNhApproved.CheckedChanged
        If chkNhApproved.Checked Then
            txtAmount.ReadOnly = False
        End If

    End Sub
End Class