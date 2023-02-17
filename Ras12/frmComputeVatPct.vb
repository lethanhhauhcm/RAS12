Public Class frmComputeVatPct
    Private mblnFirstLoadCompleted
    Private Sub frmComputeVatPct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComputeVat()
        LoadCombo(cboVatPctRaw, "select distinct VatPctRaw as value from tkt order by VatPctRaw", Conn)
        LoadCombo(cboVatPctRounded, "select distinct VatPctRounded as value from tkt where status<>'XX' order by VatPctRounded", Conn)
        'LoadCombo(cboDiff, "select distinct abs(VatPctRaw-VatPctRounded) as value from tkt where status<>'XX' order by abs(VatPctRaw-VatPctRounded)", Conn)
        LoadComboDisplay(cboCustShortName, "select CustShortName as Display, RecId as Value from CustomerList where Status='OK' order by CustShortName", Conn)
        dtpFromDOI.MinDate = "01 NOV 21"
        Clear()
        mblnFirstLoadCompleted = True
        'Search()
    End Sub
    Private Function Clear() As Boolean
        cboVatPctRaw.SelectedIndex = -1
        cboVatPctRounded.SelectedIndex = -1
        cboDiff.SelectedIndex = -1
        cboCounter.SelectedIndex = -1
        cboCustShortName.SelectedIndex = -1
        dtpFromDOI.Value = "01 NOV 21"
        dtpToDOI.Value = Now.Date
        cboSRV.SelectedIndex = -1
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String = "select t.RecID, convert(varchar, t.DOI,6) as DOI, t.tkno,t.SRV, t.Itinerary" _
                                    & ", t.Fare,t.Charge,VatAmt,VatPctRaw,VatPctRounded, DomInt, t.Tax_D,r.Counter,r.AL,t.DocType,VatChecked" _
                                    & " from tkt t left join rcp r On t.RCPID=r.RecID" _
                                    & " where t.VatPctComputed='true' and t.qty<>0  and t.Status<>'XX' and Counter<>'GSA' " _
                                    & " and substring(tkno,1,3) in ('738','ZVJ','ZQH','978','926','INS')" _
                                    & " and DOI between '" & CreateFromDate(dtpFromDOI.Value) & "' and '" & CreateToDate(dtpToDOI.Value) & "'"

        AddEqualConditionCombo(strQuerry, cboVatPctRaw)
        AddEqualConditionCombo(strQuerry, cboVatPctRounded)
        AddEqualConditionCombo(strQuerry, cboCounter)
        AddEqualConditionCombo(strQuerry, cboCustShortName)
        AddEqualConditionCombo(strQuerry, cboSRV, "t.Srv")
        AddEqualConditionCheck(strQuerry, chkVatChecked)

        If cboDiff.SelectedIndex > -1 Then
            Dim arrBreaks As String() = cboDiff.Text.Split("-")
            strQuerry = strQuerry & " and abs(VatPctRaw-VatPctRounded)>=" & arrBreaks(0) & " and abs(VatPctRaw-VatPctRounded)<=" & arrBreaks(1)
        End If
        strQuerry = strQuerry & " ORDER BY t.DOI, t.RecID"
        LoadDataGridView(dgrTkts, strQuerry, Conn)

    End Function

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub lbkChecked_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkChecked.LinkClicked
        If dgrTkts.CurrentRow Is Nothing Then Exit Sub
        Dim strQuerry As String = "update Tkt set VatChecked='True' where RecId=" & dgrTkts.CurrentRow.Cells("RecId").Value
        If ExecuteNonQuerry(strQuerry, Conn) Then
            Search()
        End If
    End Sub
End Class