Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
'Imports RAS12.VNPT_Business
'Imports RAS12.VNPT_Portal
'Imports RAS12.VNPT_Publish

Public Class frmVatInvEdit
    Private mblnFirstLoadCompleted As Boolean
    Private mobjExcel As New Excel.Application
    Private mobjWbk As Workbook
    Private mobjWsh As Worksheet
    Private mblnStartEdit As Boolean
    Private mobjEditedRow As DataGridViewRow
    Private mintNewInvId As Integer
    Private mlstVatInvLinks As New List(Of clsVatInvLink)
    Public Sub New(strCustShortName As String, strService As String, objRow As DataGridViewRow, blnEdit As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim strQuerryExtraInfo
        Dim tblExtraInfo As System.Data.DataTable

        strQuerryExtraInfo = "select M.RecID, M.VAL1 as DataCode,M1.VAL2 as DataName" _
                & " from misc m " _
                & " left join misc m1 on m.CAT=m1.CAT and m.VAL1=m1.VAL1" _
                & " and m1.Status='OK' and M1.VAL='" & strCustShortName & "'" _
                & " where M.CAT='VatInvDisplay' and M.Status='OK' and m.VAL='ALL'" _
                & " order by M.VAL1"
        tblExtraInfo = GetDataTable(strQuerryExtraInfo, Conn)

        For Each objRowExtra As DataRow In tblExtraInfo.Rows
            If IsDBNull(objRowExtra("DataName")) Then
                CType(Me.Controls("txt" & objRowExtra("DataCode")), System.Windows.Forms.TextBox).Visible = False
                CType(Me.Controls("lbl" & objRowExtra("DataCode")), System.Windows.Forms.Label).Visible = False
            Else
                CType(Me.Controls("txt" & objRowExtra("DataCode")), System.Windows.Forms.TextBox).Visible = True
                CType(Me.Controls("lbl" & objRowExtra("DataCode")), System.Windows.Forms.Label).Text = objRowExtra("DataName")
            End If
        Next

        'dgrInvDetails.Columns("NonVatable").DefaultCellStyle.in = "#,##0"
        'dgrInvDetails.Columns("VAT").DefaultCellStyle.Format = "#,##0"
        'dgrInvDetails.Columns("Vatable").DefaultCellStyle.Format = "#,##0"
        'dgrInvDetails.Columns("Total").DefaultCellStyle.Format = "#,##0"

        txtCustShortName.Text = strCustShortName
        txtService.Text = strService
        Dim tblCust As System.Data.DataTable = GetDataTable("Select * from CustomerList where Status<>'XX' and CustShortName='" _
                                                   & strCustShortName & "'")
        With tblCust
            txtCustomerFullName.Text = .Rows(0)("CustFullName")
            txtAddress.Text = .Rows(0)("CustAddress")
            txtTaxCode.Text = .Rows(0)("CustTaxCode")
        End With

        If objRow IsNot Nothing Then
            mobjEditedRow = objRow
            With objRow
                If blnEdit Then
                    txtInvId.Text = .Cells("InvId").Value
                    txtRecId.Text = .Cells("RecId").Value
                    txtInvoiceNo.Text = .Cells("InvoiceNo").Value
                    txtGhiNoId.Text = .Cells("GhiNoId").Value
                End If
                dtpDOI.Value = .Cells("DOI").Value
                txtPeriod.Text = .Cells("Period").Value

                For Each objRowExtra As DataRow In tblExtraInfo.Rows
                    If Not IsDBNull(objRowExtra("DataName")) Then
                        CType(Me.Controls("txt" & objRowExtra("DataCode")), System.Windows.Forms.TextBox).Text = objRow.Cells(objRowExtra("DataName")).value
                    End If
                Next

                Dim tblDetails As System.Data.DataTable
                Dim strQuerry As String = "select Seq, Description, TotalNonVatable as NonVatable, AmountNoVat" _
                                        & ", VatPct, VAT, TotalVatable as Vatable,Total,ShownInListing,RecId" _
                                        & " from VatInvAmts where Status='OK' and InvId=" & .Cells("InvId").Value _
                                        & "  order by Seq"

                tblDetails = GetDataTable(strQuerry, Conn)

                For Each objRow2 As DataRow In tblDetails.Rows
                    dgrInvDetails.Rows.Add(objRow2.ItemArray)
                Next
            End With
        End If
        dgrInvDetails.Columns("Seq").ReadOnly = True
        dgrInvDetails.Columns("VAT").ReadOnly = True
        dgrInvDetails.Columns("Vatable").ReadOnly = True
        dgrInvDetails.Columns("Total").ReadOnly = True

        dgrInvDetails.Columns("NonVatable").DefaultCellStyle.Format = "#,##0"
        dgrInvDetails.Columns("AmountNoVat").DefaultCellStyle.Format = "#,##0"

        dgrInvDetails.Columns("VAT").DefaultCellStyle.Format = "#,##0"
        dgrInvDetails.Columns("Vatable").DefaultCellStyle.Format = "#,##0"
        dgrInvDetails.Columns("Total").DefaultCellStyle.Format = "#,##0"


        mblnFirstLoadCompleted = True
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerry As New List(Of String)
        Dim strQuerry As String
        Dim strFields As String
        Dim strValues As String

        Dim strInvId As String
        Dim intNewInvRecId As Integer
        Dim mintNewInvId As Integer
        Dim dtePmtDeadline As Date

        If Not CheckInputValues() Then Exit Sub
        If Not CheckFormatTextBox(txtInvoiceNo, True) Then
            Exit Sub
        End If

        If txtRecId.Text = 0 Then
            strInvId = "(select isnull(Max(InvId),0)+1 from VatInv)"
            dtePmtDeadline = dtpDOI.Value.AddDays(ScalarToInt("KyBaocao", "top 1 DaysToPmt" _
                                            , "Status='OK' and CustShortName='" & txtCustShortName.Text & "'"))
        Else
            strInvId = txtInvId.Text
            dtePmtDeadline = mobjEditedRow.Cells("PmtDeadline").Value
        End If

        strFields = "insert into VatInv (CustShortName, InvID, DOI, CustFullName, Address, TaxCode" _
            & ", InvoiceNo, Period,PmtDeadLine,GhiNoId" _
            & ", Status, FstUser, City,Service"

        strValues = ") values ('" & txtCustShortName.Text _
            & "'," & strInvId & ",'" & CreateFromDate(dtpDOI.Value) & "',N'" & txtCustomerFullName.Text _
            & "',N'" & txtAddress.Text & "','" & txtTaxCode.Text & "'," & txtInvoiceNo.Text & ",N'" & txtPeriod.Text _
            & "','" & CreateFromDate(dtePmtDeadline) & "'," & txtGhiNoId.Text _
            & ",'--','" & myStaff.SICode & "','" & myStaff.City & "','" & txtService.Text & "'"
        For Each objCtrl As Control In Me.Controls
            If objCtrl.Name.StartsWith("txtF") AndAlso objCtrl.Visible Then
                strFields = strFields & "," & Mid(objCtrl.Name, 4)
                strValues = strValues & ",'" & objCtrl.Text & "'"
            End If
        Next
        strQuerry = strFields & strValues & ")"
        lstQuerry.Add(strQuerry)
        If Not UpdateListOfQuerries(lstQuerry, Conn, True, intNewInvRecId) Then
            MsgBox("Unable to update VatInv record!")
            Exit Sub
        Else
            lstQuerry.Clear()
        End If

        If txtRecId.Text = 0 Then
            mintNewInvId = ScalarToInt("VatInv", "InvId", "Recid=" & intNewInvRecId)
            txtInvId.Text = mintNewInvId
        Else
            lstQuerry.Add(ChangeStatus_ByDK("VatInvAmts", "XX", "Status='OK' and InvId=" & txtInvId.Text))
            lstQuerry.Add(ChangeStatus_ByID("VatInv", "XX", txtRecId.Text))
            mintNewInvId = txtInvId.Text
        End If

        lstQuerry.Add(ChangeStatus_ByID("VatInv", "OK", intNewInvRecId))
        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            With objRow
                If .Cells("Description").Value <> "" Then
                    lstQuerry.Add("insert into VatInvAmts (InvID, Seq, Description, TotalNonVatable, AmountNoVat, VatPct" _
                                  & ", VAT,ShownInListing, Status, FstUser) values (" & mintNewInvId _
                                  & "," & .Cells("Seq").Value & ",N'" & .Cells("Description").Value _
                                  & "'," & CDec(.Cells("NonVatable").Value) & "," & CDec(.Cells("AmountNoVat").Value) _
                                   & "," & .Cells("VatPct").Value & "," & CDec(.Cells("VAT").Value) _
                                   & ",'" & .Cells("ShownInListing").Value _
                                   & "','OK','" & myStaff.SICode & "')")
                End If
            End With
        Next

        For Each objLink As clsVatInvLink In mlstVatInvLinks
            lstQuerry.Add("insert into VatInvLinks (VatInvID, LinkId,LinkRef,LinkType" _
                          & ", Status, FstUser) values (" & mintNewInvId _
                          & "," & objLink.LinkId & ",'" & objLink.LinkRef & "','" & objLink.LinkType _
                          & "','OK','" & myStaff.SICode & "')")
        Next

        If UpdateListOfQuerries(lstQuerry, Conn) Then
            Me.DialogResult = DialogResult.OK
            'Me.Dispose()
        Else
            MsgBox("Unable to update VatInv Details!")
        End If

    End Sub

    Private Sub lbkPrint_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPrint.LinkClicked
        Dim i As Integer = 10
        Dim decTotalAmountNoVat As Decimal
        Dim decTotalVat As Decimal
        Dim decTotal As Decimal

        If Not CheckInputValues() Then Exit Sub
        mobjWbk = mobjExcel.Workbooks.Open(My.Application.Info.DirectoryPath & "\VAT Invoice from RAS.xltx")
        mobjWsh = mobjWbk.Sheets("PrintOut")
        mobjExcel.Visible = True
        With mobjWsh
            .Range("E2").Value = dtpDOI.Value.Day
            .Range("G2").Value = dtpDOI.Value.Month
            .Range("H2").Value = dtpDOI.Value.Year
            .Range("C4").Value = txtCustomerFullName.Text
            .Range("C5").Value = txtTaxCode.Text
            .Range("C6").Value = txtAddress.Text
            .Range("D7").Value = "CK"
            .Range("D3").Value = txtBuyer.Text

            For Each objRow As DataGridViewRow In dgrInvDetails.Rows

                If objRow.Cells("Description").Value.ToString.Length > 32 Then
                    .Range("B" & i & ": f" & i).Merge()
                End If
                .Range("B" & i).Value = objRow.Cells("Description").Value

                If objRow.Cells("NonVatable").Value > 0 Then
                    .Range("H" & i).Value = FormatNumberWithDot(Math.Abs(objRow.Cells("NonVatable").Value))
                    decTotalAmountNoVat = decTotalAmountNoVat + CDec(objRow.Cells("NonVatable").Value)
                ElseIf objRow.Cells("AmountNoVat").Value <> 0 Then
                    .Range("H" & i).Value = FormatNumberWithDot(Math.Abs(objRow.Cells("AmountNoVat").Value))
                    decTotalAmountNoVat = decTotalAmountNoVat + CDec(objRow.Cells("AmountNoVat").Value)
                End If

                If objRow.Cells("AmountNoVat").Value <> 0 Then
                    If objRow.Cells("Description").Value <> "Phí hoàn vé" Then
                        .Range("i" & i).Value = objRow.Cells("VatPct").Value
                    End If
                End If
                If objRow.Cells("AmountNoVat").Value <> 0 Then
                    If objRow.Cells("Description").Value <> "Phí hoàn vé" Then
                        .Range("j" & i).Value = FormatNumberWithDot(objRow.Cells("Vat").Value)
                    End If
                    decTotalVat = decTotalVat + objRow.Cells("Vat").Value
                End If
                If objRow.Cells("Total").Value <> 0 Then
                    .Range("k" & i).Value = FormatNumberWithDot(Math.Abs(objRow.Cells("Total").Value))
                End If
                i = i + 1
            Next
            decTotal = decTotalAmountNoVat + decTotalVat

            .Range("H23").Value = FormatNumberWithDot(Math.Abs(decTotalAmountNoVat))
            .Range("J23").Value = FormatNumberWithDot(decTotalVat)
            .Range("K23").Value = FormatNumberWithDot(Math.Abs(decTotal))
            .Range("d24").Value = TienBangChu(Format(Math.Abs(decTotal), "##0")) & " đồng chẵn."
            .Activate()
        End With
        mobjExcel.Visible = True
        mobjExcel.WindowState = XlWindowState.xlMaximized

    End Sub
    Private Function CheckInputValues() As Boolean
        Dim blnError As Boolean

        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            For Each objCell As DataGridViewCell In objRow.Cells
                blnError = False
                Select Case dgrInvDetails.Columns(objCell.ColumnIndex).Name
                    Case "Description"
                        If objCell.Value = "" Then
                            blnError = True
                        End If
                    Case "VatPct"
                        If objCell.Value <> 0 AndAlso objCell.Value <> 5 AndAlso objCell.Value <> 10 Then
                            blnError = True
                        End If

                    Case Else
                        If Not IsNumeric(objCell.Value) Then
                            blnError = True
                        End If
                End Select

                If blnError Then
                    MsgBox("Invalid & dgrInvDetails.Columns(objCell.ColumnIndex).Name" _
                                   & " in row " & objCell.RowIndex + 1)

                End If

            Next
        Next
        If Not CheckFormatTextBox(txtInvId, True) Then
            Return False
        End If

        Return True
    End Function


    Private Sub txtTotalNonVatable_LostFocus(sender As Object, e As EventArgs)

        Dim txtCheck As System.Windows.Forms.TextBox = sender
        If IsNumeric(txtCheck.Text) Then
            Dim decTemp As Decimal = txtCheck.Text
            txtCheck.Text = Format(decTemp, "#,##0")
        End If
    End Sub
    Private Sub txtTotalNonVatable_Enter(sender As Object, e As EventArgs)

        Dim txtCheck As System.Windows.Forms.TextBox = sender
        If IsNumeric(txtCheck.Text) Then
            Dim decTemp As Decimal = txtCheck.Text
            txtCheck.Text = decTemp
        End If
    End Sub




    Private Sub dgrInvDetails_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgrInvDetails.CellEnter
        mblnStartEdit = True
    End Sub

    Private Sub dgrInvDetails_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles dgrInvDetails.CellValidated
        If mblnStartEdit AndAlso dgrInvDetails.CurrentRow IsNot Nothing Then
            Dim blnError As Boolean
            For Each objCell As DataGridViewCell In dgrInvDetails.CurrentRow.Cells
                Select Case dgrInvDetails.Columns(objCell.ColumnIndex).Name
                    Case "Description", "ShownInListing"

                    Case "VatPct"
                        Select Case objCell.Value
                            Case -2, -1, 0, 5, 10
                            Case Else
                                blnError = True
                        End Select

                    Case Else
                        If Not IsNumeric(objCell.Value) Then
                            blnError = True
                        End If
                End Select

                If blnError Then
                    MsgBox("Invalid " & dgrInvDetails.Columns(objCell.ColumnIndex).Name _
                                   & " in row " & objCell.RowIndex + 1)
                    Exit Sub
                End If
            Next
            With dgrInvDetails.CurrentRow
                If IsNumeric(.Cells("VatPct").Value) AndAlso .Cells("VatPct").Value > 0 _
                    AndAlso IsNumeric(.Cells("AmountNoVat").Value) Then
                    .Cells("VAT").Value = Math.Round(.Cells("AmountNoVat").Value * .Cells("VatPct").Value / 100)
                    .Cells("Vatable").Value = .Cells("AmountNoVat").Value + .Cells("VAT").Value
                    .Cells("Total").Value = .Cells("Vatable").Value + .Cells("NonVatable").Value
                End If
            End With
        End If

    End Sub

    Private Sub lbkAddRow_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddRow.LinkClicked
        If dgrInvDetails.CurrentRow Is Nothing Then
            dgrInvDetails.Rows.Add()
        Else
            dgrInvDetails.Rows.Add(dgrInvDetails.CurrentRow.Index + 1)
        End If
        FillGridDetails()
    End Sub

    Private Sub lbkDeleteRow_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDeleteRow.LinkClicked
        dgrInvDetails.Rows.Remove(dgrInvDetails.CurrentRow)
        FillGridDetails()
    End Sub
    Public Function FillGridDetails() As Boolean
        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            For Each objCell As DataGridViewCell In objRow.Cells

                Select Case dgrInvDetails.Columns(objCell.ColumnIndex).Name
                    Case "Description"
                    Case "Seq"
                        objCell.Value = objRow.Index + 1
                    Case "ShownInListing"
                        If objCell.Value = False AndAlso objRow.Cells("Seq").Value = 1 Then
                            objCell.Value = True
                        End If
                    Case Else
                        If CStr(objCell.Value) = "" Then
                            objCell.Value = 0
                        End If
                End Select
            Next
        Next
    End Function

    Private Sub lbkMoveUp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkMoveUp.LinkClicked
        If dgrInvDetails.CurrentRow IsNot Nothing AndAlso dgrInvDetails.CurrentRow.Index > 0 Then
            With dgrInvDetails
                Dim objNew As DataGridViewRow = dgrInvDetails.CurrentRow.Clone
                Dim i As Integer
                For i = 0 To .CurrentRow.Cells.Count - 1
                    objNew.Cells(i).Value = .CurrentRow.Cells(i).Value
                Next
                dgrInvDetails.Rows.Insert(.CurrentRow.Index - 1, objNew)
                .Rows.Remove(.CurrentRow)
            End With
            FillGridDetails()
        End If
    End Sub

    Private Sub lbkMoveDown_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkMoveDown.LinkClicked
        If dgrInvDetails.CurrentRow IsNot Nothing AndAlso dgrInvDetails.CurrentRow.Index < dgrInvDetails.RowCount - 1 Then
            With dgrInvDetails
                Dim objNew As DataGridViewRow = dgrInvDetails.CurrentRow.Clone
                Dim i As Integer
                For i = 0 To .CurrentRow.Cells.Count - 1
                    objNew.Cells(i).Value = .CurrentRow.Cells(i).Value
                Next
                dgrInvDetails.Rows.Insert(.CurrentRow.Index + 2, objNew)
                .Rows.Remove(.CurrentRow)
            End With
            FillGridDetails()
        End If
    End Sub

    Private Sub lbkGetNextInv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkGetNextInv.LinkClicked
        txtInvoiceNo.Text = ScalarToInt("VatInv", "isnull(max(InvoiceNo),0)+1", "CustShortName='" & txtCustShortName.Text & "'")
    End Sub

    Private Sub lbkVatAdd1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkVatAdd1.LinkClicked
        If dgrInvDetails.CurrentRow Is Nothing Then Exit Sub
        With dgrInvDetails.CurrentRow
            .Cells("Vat").Value = .Cells("Vat").Value + 1
            .Cells("Vatable").Value = .Cells("Vatable").Value + 1
            .Cells("Total").Value = .Cells("Total").Value + 1
        End With
    End Sub

    Private Sub lbkVatMinus1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkVatMinus1.LinkClicked
        If dgrInvDetails.CurrentRow Is Nothing Then Exit Sub
        With dgrInvDetails.CurrentRow
            .Cells("Vat").Value = .Cells("Vat").Value - 1
            .Cells("Vatable").Value = .Cells("Vatable").Value - 1
            .Cells("Total").Value = .Cells("Total").Value - 1
        End With
    End Sub
    Public Property NewInvId As Integer
        Get
            Return mintNewInvId
        End Get
        Set(value As Integer)
            mintNewInvId = value
        End Set
    End Property
    Public Property VatInvLinks As List(Of clsVatInvLink)
        Get
            Return mlstVatInvLinks
        End Get
        Set(value As List(Of clsVatInvLink))
            mlstVatInvLinks = value
        End Set
    End Property
    Private Sub lbkGetLastPeriod_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkGetLastPeriod.LinkClicked
        txtPeriod.Text = ScalarToString("VatInv", "top 1 Period", "Status='ok' order by RecId desc")
    End Sub

    Private Sub lbkCreateE_Invoice_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateE_Invoice.LinkClicked

    End Sub

    Private Sub dgrInvDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrInvDetails.CellContentClick

    End Sub
End Class