Public Class frmVendorInv
    Private mblnFirstLoadCompleted As Boolean
    Private mobjTcodeSeach As clsTcodeSearch
    Private Sub frmSupplierInv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo(cboCustomer, "Select CustShortName as Value from CompanyInfo where Status='OK' order by CustShortName", Conn)
        Clear()
        Search()

        mblnFirstLoadCompleted = True

    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select i.RecID, i.InvID, i.TvCompany" _
            & ",convert(varchar(9),i.InputDate,6) as InputDate, i.InvoiceNo" _
            & ",v.ShortName, i.Currency, i.Amount" _
            & ", i.Status,v.HoaDon, i.VendorId, i.FstUser, i.FstUpdate, i.LstUser, i.LstUpdate, i.City" _
            & " from VendorInvoice i" _
            & " left join Vendor v on i.VendorId=v.RecId" _
            & " where InputDate between '" & CreateFromDate(dtpFrom.Value) & "' and '" & CreateToDate(dtpTo.Value) & "'"

        AddEqualConditionCombo(strQuerry, cboTvCompany, "i.TvCompany")
        AddEqualConditionCombo(strQuerry, cboStatus, "i.Status")
        AddLikeConditionText(strQuerry, txtVemdorName, "v.ShortName")

        Select Case chkWzUNC.CheckState
            Case CheckState.Checked
                strQuerry = strQuerry & " and i.InvId in (Select intVal from Misc where Status='ok' and Cat='InvTcodeMap')"
            Case CheckState.Unchecked
                strQuerry = strQuerry & " and i.InvId not in (Select intVal from Misc where Status='ok' and Cat='InvTcodeMap')"
        End Select

        Select Case chkMatched.CheckState
            Case CheckState.Checked
                strQuerry = strQuerry & " and i.InvId in (Select intVal from Misc where Status='ok' and Cat='InvTcodeMap' and Details='Y')"
            Case CheckState.Unchecked
                strQuerry = strQuerry & " and i.InvId not in (Select intVal from Misc where Status='ok' and Cat='InvTcodeMap' and Details='Y')"
        End Select

        If txtTcode.Text <> "" Then
            strQuerry = strQuerry & " and i.InvId in (Select intVal from Misc where Status='ok' and Cat='InvTcodeMap'" _
                    & " and intVal1 in (Select RecId from Dutoan_Tour where Status<>'XX' and Tcode like '%" _
                    & txtTcode.Text & "%') " & ")"
        End If

        If cboCustomer.Text <> "" Then
            strQuerry = strQuerry & " and i.InvId in (Select intVal from Misc where Status='ok' and Cat='InvTcodeMap'" _
                    & " and intVal1 in (Select RecId from Dutoan_Tour where Status<>'XX' and CustShortName='" _
                    & cboCustomer.Text & "') " & ")"
        End If

        strQuerry = strQuerry & " order by InputDate"

        LoadDataGridView(dgrInvoices, strQuerry, Conn)
        With dgrInvoices
            .Columns("RecId").Width = 40
            .Columns("InvId").Width = 40
            .Columns("Currency").Width = 40
            .Columns("TvCompany").Width = 70
            .Columns("InputDate").Width = 60
            .Columns("InvoiceNo").Width = 60
            .Columns("Status").Width = 40
            .Columns("FstUser").Width = 50
            .Columns("LstUser").Width = 50
            .Columns("Amount").DefaultCellStyle.Format = "#,##0"
            .Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        Return True
    End Function

    Private Sub cboTvCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTvCompany.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmEdit As New frmSelectTcode(Nothing)
        If frmEdit.ShowDialog = DialogResult.OK Then
            'Dim decInvAmt As Decimal
            Dim frmEditVendorInv As New frmVendorInvEdit(frmEdit.dgrTcodes)
            'For Each objTcodeRow As DataGridViewRow In dgrInvTcodes.Rows
            '    'decInvAmt = decInvAmt + objTcodeRow.Cells("Amount").Value
            'Next
            ''txtAmount.Text = Format(decInvAmt, "#,##0")

            If frmEditVendorInv.ShowDialog = DialogResult.OK Then
                mobjTcodeSeach = New clsTcodeSearch
                mobjTcodeSeach.Vendor = frmEdit.txtVendorName.Text
                mobjTcodeSeach.AccountName = frmEdit.txtAccountName.Text
                mobjTcodeSeach.MaxEDate = frmEdit.dtpDOI.Value
                mobjTcodeSeach.VendorId = frmEdit.txtVendorId.Text
                Search()
            End If

        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        Dim frmEdit As New frmVendorInvEdit(Nothing, dgrInvoices.CurrentRow)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked

        If DeleteGridViewRow(dgrInvoices, "Update VendorInvoice Set Status='XX', LstUpdate=getdate(),LstUser='" _
                             & myStaff.SICode & "' where RecId=" & dgrInvoices.CurrentRow.Cells("RecId").Value, Conn) Then
            ExecuteNonQuerry("Update Misc Set Status='XX', LstUpdate=getdate(),LstUser='" _
                             & myStaff.SICode & "' where Cat='InvTcodeMap' and intVal=" & dgrInvoices.CurrentRow.Cells("InvId").Value _
                             & " and Status='OK'", Conn)
            Search()
        End If
    End Sub

    Private Sub lbkMapTcode_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        'Dim frmSelect As New frmSelectTcode(dgrInvoices.CurrentRow)
        'If frmSelect.ShowDialog = DialogResult.OK Then
        '    Dim lstQuerries As New List(Of String)
        '    For Each objRow As DataGridViewRow In frmSelect.SelectedRows
        '        With dgrInvoices.CurrentRow
        '            lstQuerries.Add("Insert MISC (CAT,intVal,intVal1,Status,FstUser,City) values ('InvTcodeMap'," _
        '                        & .Cells("RecId").Value _
        '                        & "," & objRow.Cells("RecId").Value _
        '                        & ",'OK','" & myStaff.SICode & "','" & myStaff.City & "'" _
        '                        & " where NOT EXISTS(Select RecId from MISC where Cat='InvTcodeMap' and Status='OK' and intVal=" _
        '                        & .Cells("RecId").Value & " and invVal1=" & objRow.Cells("RecId").Value & ")")
        '        End With
        '    Next
        'End If
    End Sub

    Private Sub lbkMapUNC_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Dim frmSelect As New frmSelectUNC(dgrInvoices.CurrentRow)
        If frmSelect.ShowDialog = DialogResult.OK Then
            Dim lstQuerries As New List(Of String)
            For Each objRow As DataGridViewRow In frmSelect.SelectedRows
                With dgrInvoices.CurrentRow
                    lstQuerries.Add("Insert MISC (CAT,intVal,intVal1,intDec,Status,FstUser,City) values ('InvUncMap'," _
                                & .Cells("RecId").Value _
                                & "," & objRow.Cells("RecId").Value & "," & objRow.Cells("Amount").Value _
                                & ",'OK','" & myStaff.SICode & "','" & myStaff.City & "')")
                    '& " where NOT EXISTS(Select RecId from MISC where Cat='InvUncMap' and Status='OK' and intVal=" _
                    '& .Cells("RecId").Value & " and invVal1=" & objRow.Cells("RecId").Value & ")")
                End With
            Next
            If Not UpdateListOfQuerries(lstQuerries, Conn) Then
                MsgBox("Unable to map Invoice with UNC")
            End If
        End If
    End Sub


    Private Sub dgrInvoices_SelectionChanged(sender As Object, e As EventArgs) Handles dgrInvoices.SelectionChanged
        If mblnFirstLoadCompleted Then
            If dgrInvoices.CurrentRow Is Nothing Then
                dgrInvTcodes.Rows.Clear()
            Else
                LoadInvTcode()
            End If
        End If
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub dgrInvoices_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrInvoices.CellContentClick

    End Sub

    Private Sub lbkOverInvoiced_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkOverInvoiced.LinkClicked
        Dim strQuerry As String = "Update Misc set LstUpdate=Getdate(), LstUser='" & myStaff.SICode _
            & "',VAL='" & cboOverUnder.Text _
            & "' where RecId=" & dgrInvTcodes.CurrentRow.Cells("Miscid").Value
        If ExecuteNonQuerry(strQuerry, Conn) Then
            LoadInvTcode()
        End If
    End Sub


    Private Function LoadInvTcode() As Boolean
        Dim strQuerry As String = "select m.RecId as MiscId,TCode,intDec as Amount,Val as UnderOver, Details as Matched" _
               & " from MISC m" _
               & " left join DuToan_Tour t on t.RecID=m.intVal1" _
               & " where m.Status='OK' and cat='InvTcodeMap'" _
               & " and m.intval=" & dgrInvoices.CurrentRow.Cells("InvId").Value

        LoadDataGridView(dgrInvTcodes, strQuerry, Conn)
        With dgrInvTcodes
            .Columns("Amount").DefaultCellStyle.Format = "#,###"

        End With
        Return True
    End Function
    Private Function LoadUnc() As Boolean
        Dim strQuerry As String = "select RefNo,Amount,FstUpdate" _
               & " from Unc_Payments where Status='OK' and RecId in" _
               & " (Select PmtId from Dutoan_Pmt where Status='OK' and Tcode='" _
               & dgrInvTcodes.CurrentRow.Cells("Tcode").Value & "')"

        LoadDataGridView(dgrUnc, strQuerry, Conn)
        With dgrUnc
            .Columns("Amount").DefaultCellStyle.Format = "#,###"

        End With
        Return True
    End Function
    Private Sub lbkRepeatNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRepeatNew.LinkClicked
        Dim frmEdit As New frmSelectTcode(Nothing, mobjTcodeSeach)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Dim frmEditVendorInv As New frmVendorInvEdit(frmEdit.dgrTcodes)
            If frmEditVendorInv.ShowDialog = DialogResult.OK Then
                mobjTcodeSeach.Vendor = frmEdit.txtVendorName.Text
                mobjTcodeSeach.AccountName = frmEdit.txtAccountName.Text
                mobjTcodeSeach.MaxEDate = frmEdit.dtpDOI.Value
                Search()
            End If

        End If
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub
    Private Function Clear() As Boolean
        cboStatus.SelectedIndex = 0
        dtpFrom.Value = Now
        dtpTo.Value = Now
        txtTcode.Text = ""
        cboCustomer.SelectedIndex = -1
        cboOverUnder.SelectedIndex = 0
        Return True
    End Function

    Private Sub dgrInvTcodes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrInvTcodes.CellContentClick

    End Sub

    Private Sub dgrInvTcodes_SelectionChanged(sender As Object, e As EventArgs) Handles dgrInvTcodes.SelectionChanged
        If mblnFirstLoadCompleted Then
            If dgrInvTcodes.CurrentRow Is Nothing Then
                dgrUnc.Rows.Clear()
            Else
                LoadUnc()
            End If
        End If
    End Sub
End Class