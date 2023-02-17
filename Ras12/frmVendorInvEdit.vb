Public Class frmVendorInvEdit
    Private mobjInvRow As DataGridViewRow
    Public Sub New(Optional dgrTcodes As DataGridView = Nothing _
                   , Optional objRow As DataGridViewRow = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim decInvAmt As Decimal
        Dim decTcodeAmt As Decimal

        If dgrTcodes IsNot Nothing Then
            Dim objTcodeRow As DataGridViewRow = dgrTcodes.Rows(0)
            If ScalarToInt("Misc", "TOP 1 RecId", "Status='OK' and Cat='CustNameInGroup' and Val='GDS' and intVal=" _
                           & objTcodeRow.Cells("CustId").Value) > 0 Then
                txtTvCompany.Text = "GDS"
            Else
                txtTvCompany.Text = "TVTR"
            End If
            txtVendorID.Text = objTcodeRow.Cells("VendorId").Value
            txtVendor.Text = objTcodeRow.Cells("Vendor").Value
            'dtpDOI.Enabled = False
            For Each objTcode As DataGridViewRow In dgrTcodes.Rows
                With objTcode
                    dgrInvTcodes.Rows.Add({"0", "Add", .Cells("Tcode").Value, .Cells("Amount").Value, .Cells("DutoanId").Value, .Cells("Matched").Value})
                End With
            Next
            'txtAmount.Text = Format(decInvAmt, "#,##0")
        End If
        'dtpDOI.MaxDate = Now.Date
        If objRow Is Nothing Then
            Me.Text = "Add Vendor Invoice"

        Else
            Me.Text = "Edit Vendor Invoice"
            mobjInvRow = objRow
            With objRow
                txtRecId.Text = .Cells("RecId").Value
                txtInvID.Text = .Cells("InvId").Value
                txtInvoiceNo.Text = .Cells("InvoiceNo").Value
                txtTvCompany.Text = .Cells("TvCompany").Value
                txtVendorID.Text = .Cells("VendorId").Value
                txtVendor.Text = .Cells("ShortName").Value
                dtpInputDate.Value = .Cells("InputDate").Value
                decInvAmt = .Cells("Amount").Value

                'Load cac Tcode hien co
                Dim strQuerryTcode As String = "select m.RecId as MiscID,'Keep' as Action,Tcode,intDec as Amount, intVal1 as DutoanId,Details as Matched" _
                & " from MISC m" _
                & " left join DuToan_Tour t on t.RecID=m.intVal1" _
                & " where cat='InvTcodeMap' and m.Status='OK'" _
                & " and m.intval=" & txtInvID.Text

                Dim tblExistingTcodes As DataTable
                tblExistingTcodes = GetDataTable(strQuerryTcode, Conn)
                For Each objTcodeRow As DataRow In tblExistingTcodes.Rows
                    dgrInvTcodes.Rows.Add(objTcodeRow.ItemArray)
                Next

            End With

        End If
        'Tinh lai Amount cua Inv


        For Each objTcodeRow As DataGridViewRow In dgrInvTcodes.Rows
            decTcodeAmt = decTcodeAmt + objTcodeRow.Cells("Amount").Value
        Next
        txtTcodeAmount.Text = Format(decTcodeAmt, "#,###")

        If objRow Is Nothing Then
            decInvAmt = decTcodeAmt
        End If

        txtAmount.Text = Format(decInvAmt, "#,###")

        With dgrInvTcodes
            .Columns("Amount").DefaultCellStyle.Format = "#,###"
            .Columns("DutoanId").Visible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        End With
        For Each objCol As DataGridViewColumn In dgrInvTcodes.Columns
            If objCol.Name = "Matched" Then
                objCol.ReadOnly = False

            End If
        Next
    End Sub

    Private Sub frmSupplierInvEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerries As New List(Of String)
        Dim intInvId As String
        Dim intNewRecId As Integer

        If Not CheckInputs() Then Exit Sub

        If txtRecId.Text > 0 Then

            intInvId = txtInvID.Text
        Else
            intInvId = "(select isnull(max(InvId),0)+1 From VendorInvoice)"


        End If
        lstQuerries.Add("insert VendorInvoice (VendorId,InvID, TvCompany, InvoiceNo, Amount" _
                        & ",InputDate, Status, FstUser, City) values (" & txtVendorID.Text _
                        & "," & intInvId & ",'" & txtTvCompany.Text _
                        & "','" & txtInvoiceNo.Text _
                        & "'," & CDec(txtAmount.Text) & ",'" & CreateFromDate(dtpInputDate.Value) & "','QQ','" & myStaff.SICode _
                        & "','" & myStaff.City & "')")

        If UpdateListOfQuerries(lstQuerries, Conn, True, intNewRecId) Then
            Dim intNewInvId As Integer = ScalarToInt("VendorInvoice", "InvId", "RecId=" & intNewRecId)

            lstQuerries.Clear()

            For Each objRow As DataGridViewRow In dgrInvTcodes.Rows
                Select Case objRow.Cells("Action").Value
                    Case "Add"
                        lstQuerries.Add(" insert Misc (Cat,intVal,intVal1,intDec,Details) values ('InvTcodeMap'," & intNewInvId _
                                & "," & objRow.Cells("DutoanId").Value & "," & objRow.Cells("Amount").Value & ",'" & objRow.Cells("Matched").Value & "')")
                    Case "Delete"
                        lstQuerries.Add(" Update Misc Set Status='xx',LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                                & "' where RecId=" & objRow.Cells("MiscId").Value)
                    Case "Keep"
                        If objRow.Cells("Matched").Value <> ScalarToString("Misc", "Details", "RecId=" & objRow.Cells("MiscId").Value) Then
                            lstQuerries.Add(" insert Misc (Cat,intVal,intVal1,intDec,Details) values ('InvTcodeMap'," & intNewInvId _
                                & "," & objRow.Cells("DutoanId").Value & "," & objRow.Cells("Amount").Value & ",'" & objRow.Cells("Matched").Value & "')")
                            lstQuerries.Add(" Update Misc Set Status='xx',LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                                & "' where RecId=" & objRow.Cells("MiscId").Value)
                        End If

                End Select

            Next
            lstQuerries.Add("Update VendorInvoice set Status='OK' where RecId=" & intNewRecId)
            lstQuerries.Add("Update VendorInvoice Set Status='XX',LstUpdate=getdate(),LstUser='" _
                            & myStaff.SICode & "' where RecId=" & txtRecId.Text)

            If UpdateListOfQuerries(lstQuerries, Conn) Then
                DialogResult = DialogResult.OK
                Me.Dispose()
                Exit Sub
            End If

        End If

    End Sub
    Private Function CheckInputs() As Boolean

        'If Not CheckFormatComboBox(cboVendor,, 3) _
        '    Or Not CheckFormatComboBox(cboInvType,, 3) _
        '    Or Not CheckFormatComboBox(cboTvCompany,, 3) _
        '    Or Not CheckFormatComboBox(cboCurrency,, 3) Then
        '    Return False
        If Not CheckFormatTextBox(txtAmount, True, 1) _
            Or Not CheckFormatTextBox(txtInvoiceNo, True, 1) Then
            Return False
        End If

        Return True
    End Function

    'Private Sub lbkFind_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFind.LinkClicked
    '    Dim strFilter As String = ""
    '    For Each objRow As DataGridViewRow In dgrInvTcodes.Rows
    '        strFilter = strFilter & objRow.Cells("DutoanId").Value & ","
    '    Next
    '    If strFilter <> "" Then
    '        strFilter = " and DutoanId not in (" & Mid(strFilter, 1, strFilter.Length - 1) & ")"
    '    End If

    '    Dim frmSearch As New frmSelectTcode(dtpDOI.Value, strFilter)

    '    If frmSearch.ShowDialog = DialogResult.OK Then
    '        txtVendor.Text = frmSearch.SelectedRows(0).Cells("Vendor").Value
    '        txtVendorID.Text = frmSearch.SelectedRows(0).Cells("VendorID").Value
    '        txtAccountName.Text = frmSearch.SelectedRows(0).Cells("AccountName").Value
    '        Dim decAmount As Decimal = 0
    '        For Each objRow As DataGridViewRow In frmSearch.SelectedRows
    '            dgrInvTcodes.Rows.Add(0, "Add", objRow.Cells("Tcode").Value, objRow.Cells("Amount").Value, objRow.Cells("DutoanId").Value)
    '            decAmount = decAmount + objRow.Cells("Amount").Value
    '        Next
    '        txtAmount.Text = Format(decAmount, "#,###")
    '    End If
    '    dgrInvTcodes.Columns("Amount").DefaultCellStyle.Format = "#,###"

    'End Sub

    Private Sub txtAmount_DockChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked

        Dim decInvAmt As Decimal
        Decimal.TryParse(txtAmount.Text, decInvAmt)
        With dgrInvTcodes.CurrentRow
            If .Cells("Action").Value = "Keep" Then
                .Cells("Action").Value = "Delete"
                decInvAmt = decInvAmt - .Cells("Amount").Value
            ElseIf .Cells("Action").Value = "Delete" Then
                .Cells("Action").Value = "Keep"
                decInvAmt = decInvAmt + .Cells("Amount").Value
            ElseIf .Cells("Action").Value = "Add" Then
                dgrInvTcodes.Rows.Remove(dgrInvTcodes.CurrentRow)
                decInvAmt = decInvAmt - .Cells("Amount").Value
            End If
        End With
        txtAmount.Text = Format(decInvAmt, "#,###")
    End Sub

    Private Sub lbkFind_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFind.LinkClicked
        Dim frmEdit As New frmSelectTcode(mobjInvRow)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Dim decInvAmt As Decimal
            For Each objRow As DataGridViewRow In frmEdit.dgrTcodes.Rows
                dgrInvTcodes.Rows.Add({0, "Add", objRow.Cells("Tcode").Value, objRow.Cells("Amount").Value, objRow.Cells("DutoanId").Value})

            Next
            For Each objTcodeRow As DataGridViewRow In dgrInvTcodes.Rows
                decInvAmt = decInvAmt + objTcodeRow.Cells("Amount").Value
            Next
            txtAmount.Text = Format(decInvAmt, "#,##0")
        End If
    End Sub
End Class