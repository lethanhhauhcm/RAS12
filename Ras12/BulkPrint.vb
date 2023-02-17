Public Class BulkPrint
    Private Const fName As String = "VATInvoice.xlt"
    Private pDomain As String
    Private MyCust As New objCustomer
    Private Sub OptGSA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptGSA.Click, OptTVS.Click
        LoadPendingTRX()
        If Me.OptTVS.Checked Then
            LoadCmb_VAL(Me.cmbCust, MyCust.List_TA)
            Me.ChkByCust.Enabled = True
        Else
            Me.ChkByCust.Checked = False
            Me.ChkByCust.Enabled = False
        End If
    End Sub
    Private Sub LoadPendingTRX()
        Dim strSQL As String, pDK As String = ""
        Dim strAvoidDup As String = "and RecID not in (select RcpID from E_InvLinks where status='OK')"
        If pblnTT78 Then
            strAvoidDup = "and RecID not in (select RcpID from E_InvLinks78 where status='OK')"
        End If
        pDomain = IIf(Me.OptGSA.Checked, "GSA", "TVS")
        Me.LblPreview.Visible = False
        strSQL = "Select RecID, RCPNO, CustShortName, SRV, TTLDue, Currency as Curr, ROE, Charge from RCP "
        strSQL = strSQL & " where TTLDUE<>0 and status='OK' " & strAvoidDup
        'strSQL = strSQL & " where status='OK' and RecID in (select RcpID from INV where status='OK' and printCopy=0) "
        strSQL = strSQL & " and substring(rcpno,3,4)='" & Me.CmbMonth.Text & "'"
        If Me.OptGSA.Checked Then
            strSQL = strSQL & " and left(rcpno,2) not in ('TS','XX')"
        Else
            pDK = " and stock in (select al from airline where vat like '%TY') and left(rcpno,2) ='TS' and City='" & myStaff.City & "'"
            If Me.OptCorp.Checked Then
                pDK = pDK & " and CustType in ('CS','LC')  AND VENDORID=2249 "
            ElseIf Me.OptTVS.Checked Then
                pDK = pDK & "  and CustType not in ('CS','LC') "
            End If
            strSQL = strSQL & pDK
        End If
        'custid
        If Me.ChkByCust.Checked Then
            strSQL = strSQL & " and custid=" & Me.cmbCust.SelectedValue
        End If

        Me.GridTRX.DataSource = GetDataTable(strSQL)
        Me.GridTRX.Columns("recID").Visible = False
        Me.GridTRX.Columns("SRV").Width = 32
        Me.GridTRX.Columns("Curr").Width = 32
        Me.GridTRX.Columns("TTLDue").Width = 64
        Me.GridTRX.Columns("TTLDue").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridTRX.Columns("TTLDue").DefaultCellStyle.Format = "#,##0.00"
    End Sub

    Private Sub OptCorp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptCorp.Click
        LoadPendingTRX()
        Me.ChkByCust.Enabled = True
        LoadCmb_VAL(Me.cmbCust, MyCust.List_CS & " UNION " & MyCust.List_LC)
    End Sub

    Private Sub BulkPrint_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub BulkPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strMonth As String
        Me.BackColor = pubVarBackColor
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        MyCust.GenCustList()
        For i As Int16 = -4 To 0
            strMonth = Format(Now.AddMonths(i), "MMyy")
            Me.CmbMonth.Items.Add(strMonth)
        Next
    End Sub
    Private Sub LblPreview_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblPreview.LinkClicked
        Dim InvNoToPrint As String, NewAmt As Decimal, frm As Int16, Tto As Int16
        Dim RCPNO As String, INVID As Integer, tmpStr As String
        Dim RCPCharge As Decimal, TKTCharge As Decimal, ROE As Decimal
        Dim tblINV As DataTable
        Dim HasTKNO_INVNO_Record As Integer
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand, myAnswer As Int16
        If Me.ChkByCust.Checked Then
            frm = 0
            Tto = Me.GridTRX.RowCount - 1
        Else
            frm = Me.GridTRX.CurrentRow.Index
            Tto = frm
        End If
        Dim t As SqlClient.SqlTransaction
        Dim CurrTaxCode As String, CurrINVAmt As Decimal
        For i As Int16 = frm To Tto
            HasTKNO_INVNO_Record = ScalarToInt("TKTNO_INVNO", "top 1 RecID", "status='OK' and RCPID=" & Me.GridTRX.Item("RecID", i).Value)
            If HasTKNO_INVNO_Record > 0 Then
                'custid
                tblINV = GetDataTable("select RecID, INVNO, CustID, CustTaxCode, Amount from INV where RCPID=" &
                                      Me.GridTRX.Item("RecID", i).Value & " and status='OK'")
                INVID = tblINV.Rows(0)("RecID")
                InvNoToPrint = tblINV.Rows(0)("INVNO")
                CurrTaxCode = tblINV.Rows(0)("CustTaxCode")
                CurrINVAmt = tblINV.Rows(0)("Amount")
                RCPNO = Me.GridTRX.Item("RCPNO", i).Value
                cmd.CommandText = UpdateTblINVHistory(INVID, InvNoToPrint, 1)
                cmd.ExecuteNonQuery()
                InHoaDon(Application.StartupPath, fName, "O", InvNoToPrint, Now.Date, Now.Date, 0, IIf(Me.OptGSA.Checked, RCPNO.Substring(0, 2), "TS"), IIf(Me.OptGSA.Checked, "GSA", "TVS"), "")
            End If
        Next
        LoadPendingTRX()
        Me.LblPreview.Visible = False
        On Error GoTo 0
        Exit Sub
RollBackT:
        t.Rollback()
        On Error GoTo 0
    End Sub
    Private Sub cmbCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCust.SelectedIndexChanged
        LoadPendingTRX()
    End Sub
    Private Sub CmbMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbMonth.SelectedIndexChanged
        LoadPendingTRX()
    End Sub
    Private Sub GridTRX_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridTRX.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        Me.LblPreview.Visible = True
    End Sub

    Private Sub lbkCreateE_Inv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateE_Inv.LinkClicked
        Dim intFrm As Integer
        Dim intTo As Integer
        Dim blnIssue2TV As Boolean
        If Me.ChkByCust.Checked Then
            intFrm = 0
            intTo = Me.GridTRX.RowCount - 1
        Else
            intFrm = Me.GridTRX.CurrentRow.Index
            intTo = intFrm
        End If
        For i As Int16 = intFrm To intTo
            Dim frmE_Inv As New frmE_InvEdit()

            If OptCorp.Checked Then
                blnIssue2TV = True
            ElseIf myStaff.City = "HAN" _
                AndAlso GridTRX.Rows(i).Cells("CustShortName").Value = "AIRIMEX" Then
                blnIssue2TV = True
            Else
                blnIssue2TV = False
            End If
            If pblnTT78 Then
                frmE_Inv.LoadDetailsBsp78(GridTRX.Rows(i).Cells("RcpNo").Value, "", False)
            Else
                frmE_Inv.LoadDetailsBsp(GridTRX.Rows(i).Cells("RcpNo").Value, "", False)
            End If

            frmE_Inv.ShowDialog()
        Next
        LoadPendingTRX()

    End Sub
End Class