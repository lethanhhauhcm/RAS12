Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Public Class BulkPrint_New
    Private Const fName As String = ""
    Private MyCust As New objCustomer
    Private VN_CRS_Type As String
    Private mstrProd As String

    Private Function GetSelectedAL() As String
        Dim KQ As String = ""
        For i As Int16 = 0 To LstAL.Items.Count - 1
            If Me.LstAL.GetItemChecked(i) Then
                KQ = KQ & "," & Me.LstAL.Items(i).ToString
            End If
        Next
        If KQ.Length > 2 Then
            KQ = KQ.Substring(1)
            KQ = KQ.Replace(",", "','")
            KQ = "('" & KQ & "')"
        End If
        Return KQ
    End Function
    Private Sub LoadPendingTRX()
        Dim blnVatAdjusted As Boolean
        Dim decSumFareVat As Decimal


        If Me.CmbCounter.Text = "" Then Exit Sub

        Me.LblPreview.Visible = False
        Me.CmbPrintToWhom.Text = "Customer"
        Dim strDK As String

        If chkHavingInvNbr.Checked Then
            strDK = "not  i.InvNo is null" _
                                & " And t.status in ('OK','EX') and t.SRV in ('S','R')"
            LblSumAndCheck.Visible = False
        Else
            strDK = " i.InvNo is null" _
                                & " And t.status in ('OK','EX') and t.SRV in ('S','R')"
            LblSumAndCheck.Visible = True
        End If

        If Me.txtTCode.Text <> "" Then
            strDK = strDK & " and RCPID in (select RcpID from FOP where status='OK' and Document='" & Me.txtTCode.Text.Replace("--", "") & "')"
        End If
        Dim strSQL As String = "Select t.RecID, t.RCPID, t.Charge_D,i.InvNo, t.RCPNO" _
            & ", t.SRV, t.TKNO, t.DOI, t.Currency as Curr, t.Fare,0 as UE, t.NetToAL" _
            & ", t.Tax, t.Charge, t.ChargeTV, 0 as LCL_C,'' as TCode " _
            & ", r.Charge as CcCharge,t.Itinerary,r1.DomInt,t.Tax_D" _
            & ", (Select count (c.RecId) from tkt c where c.Status<>'XX' and c.RcpId=t.RcpId) as CountTktInRcp" _
            & ",t.PaxName,T.DOF,t.DocType" _
            & " from TKT t left join RCP r on t.RcpId=r.Recid And r.Status='OK'" _
            & " left join ReportData r1 on t.RecId=r1.Tkid" _
            & " left join INV_TVTR i on t.RecId=i.Tktid and i.status ='OK' and i.Prod='AIR'" _
            & " where " & strDK & " And RCPID in (select RecID from #" & myStaff.SICode & " where CustID=" &
            Me.cmbCust.SelectedValue & ")"
        '& " where " & strDK & " And RCPID in (select RecID from #" & myStaff.SICode & " where CustID=" &
        'Me.cmbCust.SelectedValue & ")"


        If cboFOP.Text <> "" Then
            strSQL = strSQL & " and RcpId in (Select RcpId from FOP where Status<>'XX' and FOP='" _
                & cboFOP.Text & "')"
        End If
        Me.GridTKT.DataSource = GetDataTable(strSQL)
        Me.GridTKT.Columns("recID").Visible = False
        Me.GridTKT.Columns("RCPID").Visible = False
        Me.GridTKT.Columns("Charge_D").Visible = False
        Me.GridTKT.Columns("InvNo").Visible = chkHavingInvNbr.Checked
        Me.GridTKT.Columns("InvNo").Width = 50
        Me.GridTKT.Columns("SRV").Width = 32
        Me.GridTKT.Columns("Curr").Width = 32
        Me.GridTKT.Columns("DOI").Width = 56
        Me.GridTKT.Columns("Fare").Width = 75
        Me.GridTKT.Columns("UE").Width = 60
        Me.GridTKT.Columns("NetToAL").Width = 75
        Me.GridTKT.Columns("Tax").Width = 56
        Me.GridTKT.Columns("Charge").Width = 56
        Me.GridTKT.Columns("ChargeTV").Width = 56
        Me.GridTKT.Columns("LCL_C").Width = 56
        Me.GridTKT.Columns("CcCharge").Width = 60
        Me.GridTKT.Columns("Fare").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridTKT.Columns("Fare").DefaultCellStyle.Format = "#,##0.00"
        Me.GridTKT.Columns("NetToAL").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridTKT.Columns("NetToAL").DefaultCellStyle.Format = "#,##0.00"
        Me.GridTKT.Columns("Tax").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridTKT.Columns("Tax").DefaultCellStyle.Format = "#,##0.00"
        Me.GridTKT.Columns("Charge").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridTKT.Columns("Charge").DefaultCellStyle.Format = "#,##0"
        Me.GridTKT.Columns("ChargeTV").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridTKT.Columns("ChargeTV").DefaultCellStyle.Format = "#,##0.00"
        Me.GridTKT.Columns("LCL_C").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridTKT.Columns("LCL_C").DefaultCellStyle.Format = "#,##0"
        Me.GridTKT.Columns("UE").DefaultCellStyle.Format = "#,##0.00"
        Me.GridTKT.Columns("CcCharge").DefaultCellStyle.Format = "#,##0.00"
        For i As Int16 = 0 To Me.GridTKT.Rows.Count - 1
            If Me.GridTKT.Item("Curr", i).Value = "USD" And Me.GridTKT.Item("Charge_D", i).Value.ToString.Contains("VND") Then ' dua SF ve nguyen te
                Me.GridTKT.Item("LCL_C", i).Value = Split_ChargeTV(Me.GridTKT.Item("Charge_D", i).Value, "VND")
                Me.GridTKT.Item("ChargeTV", i).Value = Split_ChargeTV(Me.GridTKT.Item("Charge_D", i).Value, "USD")
            End If
        Next

        blnVatAdjusted = IsInCustGrp("VAT ADJUSTED", cmbCust.Text)

        For i As Int16 = 0 To Me.GridTKT.Rows.Count - 1
            If GridTKT.Item("DocType", i).Value <> "AHC" AndAlso GridTKT.Item("DomInt", i).Value = "DOM" Then
                GridTKT.Item("UE", i).Value = GetTaxAmtFromTaxDetails("UE", GridTKT.Item("Tax_D", i).Value)
                GridTKT.Item("Tax", i).Value = GridTKT.Item("Tax", i).Value - GridTKT.Item("UE", i).Value

                If blnVatAdjusted AndAlso GridTKT.Item("UE", i).Value <> GridTKT.Item("Fare", i).Value * 0.1 Then
                    decSumFareVat = GridTKT.Item("Fare", i).Value + GridTKT.Item("UE", i).Value
                    GridTKT.Item("Fare", i).Value = Math.Round(decSumFareVat / 1.1, 0)
                    GridTKT.Item("UE", i).Value = decSumFareVat - GridTKT.Item("Fare", i).Value
                End If
            End If

            If Me.cmbCust.Text = "TVSGN" Then
                Dim RCPID As Integer, TCode As String
                If Me.GridTKT.Item("TCode", i).Value = "" Then
                    RCPID = Me.GridTKT.Item("RCPID", i).Value
                    TCode = ScalarToString("FOP", "Document", "RCPID=" & RCPID & " And FOP='PSP' and Document <>''")
                    If String.IsNullOrEmpty(TCode) Then TCode = "."
                    For r As Int16 = 0 To Me.GridTKT.Rows.Count - 1
                        If Me.GridTKT.Item("RCPID", r).Value = RCPID Then
                            Me.GridTKT.Item("TCode", r).Value = TCode
                        End If
                    Next
                End If
            End If
        Next
    End Sub
    Private Sub LoadPendingNonAir()
        If Me.CmbCounter.Text = "" Then Exit Sub

        Me.LblPreview.Visible = False
        Me.CmbPrintToWhom.Text = "Customer"
        Dim strDK As String

        If chkHavingInvNbr.Checked Then
            strDK = " i.RecID in (select TKTID from INV_TVTR where status ='OK' and Prod='N-A')"
            LblSumAndCheck.Visible = False
        Else
            strDK = " i.RecID not in (select TKTID from INV_TVTR where status ='OK' and Prod='N-A')"
            LblSumAndCheck.Visible = True
        End If

        If Me.txtTCode.Text <> "" Then
            strDK = strDK & " and Tcode='" & Me.txtTCode.Text.Replace("--", "") & "'"
        End If
        Dim strSQL As String = "Select i.RecID, t.Tcode,inv.InvNo,convert(varchar,inv.FstUpdate,6) as InvDate" _
            & ", i.Service,convert(varchar,t.LstUpdate,6) as FinalDate,i.TTLToPax" _
            & ", case when i.isVATIncl=0 then (i.Cost+i.MU)*i.qty*i.ROE when i.isVATIncl=1 then " _
            & " ((i.Cost+i.MU)*i.ROE/(1.00+i.Vat/100.00))*i.qty end as VND" _
            & ", case when i.isVATIncl=0 then ((i.Cost+i.MU)*i.ROE*i.VAT/100)*i.qty else ((i.Cost+i.MU)-(i.Cost+i.MU)/(1.00+i.Vat/100.00))*i.qty*i.ROE end as VAT" _
            & ",i.VAT as VatPct,i.RelatedItem,isnull(i2.Service,'') as RelatedService,i.PaxName" _
            & " from DuToan_Item i left join DuToan_Tour t On t.RecId=i.DutoanId" _
            & " left join DuToan_item i2 On i.RelatedItem=i2.RecId" _
            & " left join Rcp r On r.DeliveryStatus=t.Tcode" _
            & " left join INV_TVTR inv On i.RecId=inv.TktId  and inv.Status='OK'" _
            & " where i.Status='OK' and i.BookOnly=0 and i.CostOnly=0" _
            & " and i.Service <>'VendorPendingBalance'" _
            & " And t.Status='RR' and t.LstUpdate between '" & CreateFromDate(txtDOIFrm.Value) _
            & "' and '" & CreateToDate(TxtDOIto.Value) & "' and " _
            & strDK & " And t.CustID=" & Me.cmbCust.SelectedValue _
            & " and r.Status<>'xx'"

        If cboFOP.Text <> "" Then
            strSQL = strSQL & " and R.RecId in (Select RcpId from FOP where Status<>'XX' and FOP='" _
                & cboFOP.Text & "')"
        End If


        Me.GridTKT.DataSource = GetDataTable(strSQL)
        Me.GridTKT.AutoResizeColumns()
        Me.GridTKT.Columns("RecID").Visible = False
        Me.GridTKT.Columns("RelatedItem").Visible = False
        Me.GridTKT.Columns("InvNo").Visible = chkHavingInvNbr.Checked
        Me.GridTKT.Columns("InvDate").Visible = chkHavingInvNbr.Checked
        Me.GridTKT.Columns("VatPct").Width = 40
        Me.GridTKT.Columns("InvNo").Width = 40
        Me.GridTKT.Columns("InvDate").Width = 60
        Me.GridTKT.Columns("FinalDate").Width = 70
        'Me.GridTKT.Columns("Curr").Width = 32
        'Me.GridTKT.Columns("DOI").Width = 56
        'Me.GridTKT.Columns("Fare").Width = 64
        'Me.GridTKT.Columns("UE").Width = 60
        'Me.GridTKT.Columns("NetToAL").Width = 75
        'Me.GridTKT.Columns("Tax").Width = 56
        'Me.GridTKT.Columns("Charge").Width = 56
        'Me.GridTKT.Columns("ChargeTV").Width = 56
        'Me.GridTKT.Columns("LCL_C").Width = 56
        'Me.GridTKT.Columns("CcCharge").Width = 60
        Me.GridTKT.Columns("TTLToPax").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridTKT.Columns("VND").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridTKT.Columns("VAT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridTKT.Columns("TTLToPax").DefaultCellStyle.Format = "#,##0"
        Me.GridTKT.Columns("VND").DefaultCellStyle.Format = "#,##0"
        Me.GridTKT.Columns("VAT").DefaultCellStyle.Format = "#,##0"


        For i As Int16 = 0 To Me.GridTKT.Rows.Count - 1

        Next
    End Sub

    Private Sub BulkPrint_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Private Sub BulkPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = pubVarBackColor
        Dim tbl As System.Data.DataTable = GetDataTable("select AL from airline where status='OK' and City='" & myStaff.City _
                                                        & "' and AL not in ('XX') order by AL")
        'Me.LstAL.Items.Add("01", True)
        For i As Int16 = 0 To tbl.Rows.Count - 1
            'If tbl.Rows(i)("AL") = "VN" Then
            '    Me.LstAL.Items.Add(tbl.Rows(i)("AL"))
            'Else
            Me.LstAL.Items.Add(tbl.Rows(i)("AL"), True)
            'End If
        Next
        'If My.Computer.Name = "5-247" Then
        '    txtDOIFrm.Value = "01 jul 2017"
        '    TxtDOIto.Value = "31 jul 2017"
        'End If
    End Sub
    Private Sub LblPreview_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblPreview.LinkClicked
        Select Case mstrProd
            Case "AIR"
                PrintVatInvoiceAir(My.Application.Info.DirectoryPath & "\VAT Invoice Bulk.xls")
            Case "N-A"
                PrintVatInvoiceNonAir(My.Application.Info.DirectoryPath & "\VAT Invoice Bulk NonAir.xls")
        End Select


        LblPreview.Enabled = False

        '        Exit Sub
        '        Dim InvNoToPrint As String
        '        Dim NewAmt As Decimal, frm As Int16, Tto As Int16
        '        Dim RCPNO As String, INVID As Integer, tmpStr As String
        '        Dim RCPCharge As Decimal, TKTCharge As Decimal, ROE As Decimal
        '        Dim tblINV As DataTable
        '        Dim HasTKNO_INVNO_Record As Integer
        '        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand, myAnswer As Int16
        '        Dim t As SqlClient.SqlTransaction
        '        Dim CurrTaxCode As String, CurrINVAmt As Decimal
        '        For i As Int16 = frm To Tto
        '            HasTKNO_INVNO_Record = ScalarToInt("TKTNO_INVNO", "top 1 RecID", "status='OK' and RCPID=" & Me.GridTKT.Item("RecID", i).Value)
        '            If HasTKNO_INVNO_Record > 0 Then
        '                tblINV = GetDataTable("select RecID, INVNO, CustID, CustTaxCode, Amount from INV where RCPID=" &
        '                                      Me.GridTKT.Item("RecID", i).Value & " and status='OK'")
        '                INVID = tblINV.Rows(0)("RecID")
        '                InvNoToPrint = tblINV.Rows(0)("INVNO")
        '                CurrTaxCode = tblINV.Rows(0)("CustTaxCode")
        '                CurrINVAmt = tblINV.Rows(0)("Amount")
        '                RCPNO = Me.GridTKT.Item("RCPNO", i).Value
        '                cmd.CommandText = UpdateTblINVHistory(INVID, InvNoToPrint, 1)
        '                cmd.ExecuteNonQuery()
        '                PrintVatInvoiceAir(My.Application.Info.DirectoryPath & "\VAT Invoice Bulk.xls")
        '                'InHoaDon(Application.StartupPath, fName, "O", InvNoToPrint, Now.Date, Now.Date, 0, IIf(Me.OptGSA.Checked, RCPNO.Substring(0, 2), "TS"), IIf(Me.OptGSA.Checked, "GSA", "TVS"), "")
        '            End If
        '        Next
        '        LoadPendingTRX()
        '        Me.LblPreview.Visible = False
        '        On Error GoTo 0
        '        Exit Sub
        'RollBackT:
        '        t.Rollback()
        '        On Error GoTo 0
    End Sub
    Private Function PrintVatInvoiceAir(strFileName As String) As Boolean
        Dim objExcel As New Excel.Application
        Dim objWbk As Workbook
        Dim objWsh As Worksheet
        'Dim i As Integer, j As Integer

        Dim blnSingleTkt As Boolean = True
        Dim intTktCount As Integer

        Dim tblCustomer As System.Data.DataTable

        objWbk = objExcel.Workbooks.Open(strFileName, , True)
        objWsh = objWbk.Sheets("Data")
        'objExcel.Visible = True
        'objExcel.ActiveWindow.Activate()

        For i As Int16 = 0 To Me.GridTKT.Rows.Count - 1
            If Me.GridTKT.Item("S", i).Value Then
                intTktCount = intTktCount + 1
                If intTktCount > 1 Then
                    blnSingleTkt = False
                    'Exit For
                End If
            End If
        Next

        With objWsh
            .Range("A2").Value = TxtInvDate.Text
            If CmbPrintToWhom.Text = "Customer" Then
                .Range("B2").Value = cmbCust.Text
                .Range("c2").Value = cmbCust.SelectedValue
            Else
                .Range("B2").Value = "TVSGN"
                .Range("c2").Value = 5102
            End If
            tblCustomer = GetDataTable("Select c.*,m.Cat from CustomerList c" _
                            & " left join MISC m on c.CustShortName=m.VAL" _
                            & " and m.Cat='VatInvHasDOF'" _
                            & " where c.RecId=" & .Range("c2").Value)

            .Range("b2").Value = tblCustomer.Rows(0)("CustFullName")
            .Range("c2").Value = tblCustomer.Rows(0)("CustAddress")
            .Range("d2").Value = CStr(tblCustomer.Rows(0)("CustTaxCode"))
            .Range("E2").Value = "CK"
            .Range("F4:H9").ClearContents()

            If GridTKT.CurrentRow.Cells("SRV").Value = "S" Then
                Select Case GridTKT.CurrentRow.Cells("DocType").Value
                    Case "AHC"
                        .Range("B4").Value = "Phí dịch vụ ngoài giờ"
                    Case Else
                        .Range("B4").Value = objWbk.Sheets("Temp").Range("A1").value
                End Select

            ElseIf GridTKT.CurrentRow.Cells("SRV").Value = "R" Then
                .Range("B4").Value = objWbk.Sheets("Temp").Range("A2").value
            End If

            If blnSingleTkt Then
                objExcel.Visible = True
                .Range("B5").Value = GridTKT.CurrentRow.Cells("TKNO").Value
                Select Case GridTKT.CurrentRow.Cells("DocType").Value
                    Case "AHC"
                        .Range("B6").Value = GridTKT.CurrentRow.Cells("PaxName").Value
                        .Range("F6").Value = Math.Round((dgrSum.CurrentRow.Cells("ServiceFee").Value _
                                                         + dgrSum.CurrentRow.Cells("MerchantFee").Value) / 1.1)
                        '.Range("g11").Value = 10
                        .Range("H6").Value = (dgrSum.CurrentRow.Cells("ServiceFee").Value + dgrSum.CurrentRow.Cells("MerchantFee").Value) _
                                            - .Range("F6").Value
                        .Range("i6").Value = .Range("f6").Value + .Range("H6").Value

                        If .Range("H6").Value <> 0 Then
                            .Range("G6").Value = 10
                        Else
                            .Range("G6").Value = ""
                        End If
                        .Range("A7:I11").ClearContents()
                    Case Else
                        .Range("B6").Value = ConvertItinerary4FullName(GridTKT.CurrentRow.Cells("Itinerary").Value)
                        .Range("B7").Value = GridTKT.CurrentRow.Cells("PaxName").Value
                        If GridTKT.CurrentRow.Cells("DomInt").Value = "DOM" Then
                            .Range("F4").Value = dgrSum.CurrentRow.Cells("Fare").Value
                            .Range("G4").Value = 10
                            .Range("H4").Value = dgrSum.CurrentRow.Cells("UE").Value

                            If dgrSum.CurrentRow.Cells("SRV").Value = "S" Then
                                .Range("F8").Value = Math.Round(dgrSum.CurrentRow.Cells("Charge").Value / 1.1)
                                .Range("g8").Value = 10
                                .Range("H8").Value = dgrSum.CurrentRow.Cells("Charge").Value - .Range("F8").Value
                            Else
                                .Range("F8").Value = dgrSum.CurrentRow.Cells("Charge").Value
                                .Range("G8").Value = ""
                                .Range("H8").Value = ""
                            End If

                            .Range("F9").Value = Math.Round(dgrSum.CurrentRow.Cells("Tax").Value / 1.1)
                            .Range("G9").Value = 10
                            .Range("H9").Value = dgrSum.CurrentRow.Cells("Tax").Value - .Range("F9").Value
                        Else

                            .Range("F4").Value = Math.Round(dgrSum.CurrentRow.Cells("Fare").Value)
                            .Range("G4").Value = 0
                            .Range("H4").Value = .Range("F4").Value * .Range("G4").Value
                            .Range("F8").Value = Math.Round(dgrSum.CurrentRow.Cells("Charge").Value)
                            .Range("F9").Value = Math.Round(dgrSum.CurrentRow.Cells("Tax").Value)
                            .Range("G9").Value = 0
                            .Range("H9").Value = .Range("F9").Value * .Range("G9").Value
                        End If

                        If .Range("F8").Value = 0 Then
                            .Range("F8").Value = ""
                            .Range("G8").Value = ""
                            .Range("H8").Value = ""
                        End If

                        .Range("F11").Value = Math.Round((dgrSum.CurrentRow.Cells("ServiceFee").Value _
                                                         + dgrSum.CurrentRow.Cells("MerchantFee").Value) / 1.1)
                        '.Range("g11").Value = 10
                        .Range("H11").Value = (dgrSum.CurrentRow.Cells("ServiceFee").Value + dgrSum.CurrentRow.Cells("MerchantFee").Value) - .Range("F11").Value
                        If IsDBNull(tblCustomer.Rows(0)("CAT")) Then
                            'bo qua
                        ElseIf tblCustomer.Rows(0)("CAT") = "VatInvHasDOF" Then
                            .Rows("8:8").Insert
                            .Range("B8").Value = objWbk.Sheets("Temp").range("A3").value _
                                & Format(dgrSum.Rows(0).Cells("DOF").Value, "dd/MM/yyyy")
                        End If
                End Select


            End If



            'If cboSheetName.Text.StartsWith("INT") Then
            '    .Range("G4").Value = 0
            'ElseIf .Range("H4").Value <> 0 AndAlso cboSheetName.Text.StartsWith("DOM") Then
            '    .Range("G4").Value = 10
            'Else
            '    .Range("G4").Value = ""
            'End If
            'If cboSheetName.Text.StartsWith("INT") Then

            'ElseIf .Range("H8").Value <> 0 Then
            '    .Range("G8").Value = 10
            'Else
            '    .Range("G8").Value = ""
            'End If
            'If cboSheetName.Text.StartsWith("INT") Then
            '    .Range("G9").Value = 0
            'ElseIf .Range("H9").Value <> 0 Then
            '    .Range("G9").Value = 10
            'Else
            '    .Range("G9").Value = ""
            'End If

            If .Range("H11").Value <> 0 Then
                .Range("G11").Value = 10
            Else
                .Range("G11").Value = ""
            End If
            .Activate()

        End With
        objExcel.Visible = True
        'objExcel.ActiveWindow.Activate()
        Return True
    End Function
    Private Function PrintVatInvoiceNonAir(strFileName As String) As Boolean
        Dim objExcel As New Excel.Application
        Dim objWbk As Workbook
        Dim objWsh As Worksheet
        Dim int2ndLine As Integer
        Dim tblCustomer As System.Data.DataTable
        Dim decTtlProviderCost As Decimal
        Dim decTtlVat As Decimal

        objWbk = objExcel.Workbooks.Open(strFileName, , True)
        objWsh = objWbk.Sheets("PrintOut")
        objExcel.Visible = True
        'objExcel.ActiveWindow.Activate()


        With objWsh
            .Range("c2").Value = TxtInvDate.Value.Day
            .Range("f2").Value = TxtInvDate.Value.Month
            .Range("g2").Value = TxtInvDate.Value.Year

            tblCustomer = GetDataTable("Select c.*,m.Cat from CustomerList c" _
                            & " left join MISC m on c.CustShortName=m.VAL" _
                            & " and m.Cat='VatInvHasDOF'" _
                            & " where c.RecId=" & cmbCust.SelectedValue)

            .Range("c4").Value = tblCustomer.Rows(0)("CustFullName")
            .Range("c5").Value = "'" & CStr(tblCustomer.Rows(0)("CustTaxCode"))
            .Range("b6").Value = "       " & tblCustomer.Rows(0)("CustAddress")
            .Range("b7").Value = "CK"
            .Range("a10:j16").ClearContents()



            .Range("a10").Value = 1
            .Range("b10").Value = TranslateNonAirSvc(dgrSum.Rows(0).Cells("Desc").Value)
            .Range("g10").Value = ReformatVietnameseNumber(dgrSum.Rows(0).Cells("ProviderCost").Value)
            If dgrSum.Rows(0).Cells("VatPct").Value = 0 Then
                .Range("h10").Value = ""
                .Range("i10").Value = ""
            Else
                .Range("h10").Value = dgrSum.Rows(0).Cells("VatPct").Value
                .Range("i10").Value = ReformatVietnameseNumber(dgrSum.Rows(0).Cells("Vat").Value)
            End If

            .Range("j10").Value = ReformatVietnameseNumber(dgrSum.Rows(0).Cells("TtlToPax").Value)
            If dgrSum.Rows(0).Cells("Desc").Value.ToString.Contains("Fee") Then
                .Range("B11").Value = TranslateNonAirSvc(dgrSum.Rows(0).Cells("RelatedService").Value)
                .Range("B12").Value = dgrSum.Rows(0).Cells("PaxName").Value
                int2ndLine = 13
            Else
                .Range("B11").Value = dgrSum.Rows(0).Cells("PaxName").Value
                int2ndLine = 12
            End If


            If dgrSum.Rows.Count > 1 Then
                .Range("a" & int2ndLine).Value = 2
                .Range("b" & int2ndLine).Value = TranslateNonAirSvc(dgrSum.Rows(1).Cells("Desc").Value)
                .Range("g" & int2ndLine).Value = ReformatVietnameseNumber(dgrSum.Rows(1).Cells("ProviderCost").Value)

                .Range("h" & int2ndLine).Value = dgrSum.Rows(1).Cells("VatPct").Value
                .Range("i" & int2ndLine).Value = ReformatVietnameseNumber(dgrSum.Rows(1).Cells("Vat").Value)
                .Range("j" & int2ndLine).Value = ReformatVietnameseNumber(dgrSum.Rows(1).Cells("TtlToPax").Value)
            End If

            For Each objRow As DataGridViewRow In dgrSum.Rows
                decTtlProviderCost = decTtlProviderCost + objRow.Cells("ProviderCost").Value
                decTtlVat = decTtlVat + objRow.Cells("Vat").Value
            Next
            .Range("g23").Value = ReformatVietnameseNumber(decTtlProviderCost)
            .Range("i23").Value = ReformatVietnameseNumber(decTtlVat)
            .Range("j23").Value = ReformatVietnameseNumber(decTtlProviderCost + decTtlVat)

            .Range("b24").Value = "                             " _
                & TienBangChu(Math.Round(decTtlVat + decTtlProviderCost, 0)) & " đồng."
            .Activate()

        End With
        objExcel.Visible = True
        'objExcel.ActiveWindow.Activate()
        Return True
    End Function
    Private Sub GridTRX_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridTKT.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        If e.ColumnIndex = 0 Then
            Me.LblPreview.Visible = False
        End If

    End Sub
    Private Sub LblCheckAll_unCheckAll_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblCheckAll_unCheckAll_AL.LinkClicked
        If Me.LblCheckAll_unCheckAll_AL.Text = "CheckAll" Then
            Me.LblCheckAll_unCheckAll_AL.Text = "UnCheckAll"
            Check_UnCheckAL(True)
        Else
            Me.LblCheckAll_unCheckAll_AL.Text = "CheckAll"
            Check_UnCheckAL(False)
        End If
    End Sub
    Private Sub Check_UnCheckAL(pCheck As Boolean)
        For i As Int16 = 0 To Me.LstAL.Items.Count - 1
            If Me.LstAL.Items(i).ToString <> "VN" Or Not pCheck Then Me.LstAL.SetItemChecked(i, pCheck)
        Next
    End Sub

    Private Sub CmbCounter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCounter.SelectedIndexChanged
        If Me.CmbCounter.Text = "" Then
            LoadCmb_VAL(Me.cmbCust, "select 0 as VAL, '' as DIS")
            Exit Sub
        End If
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim strDK_TKT As String = GetSelectedAL()
        If strDK_TKT = "" Then Exit Sub
        If strDK_TKT = "('VN')" Then
            Me.CmbPrintToWhom.Enabled = True
        Else
            Me.CmbPrintToWhom.Enabled = False
        End If
        strDK_TKT = " status in ('OK','EX') and AL in " & strDK_TKT
        Dim strDK_Channel As String = "('CS','LC')"
        If Me.CmbCounter.Text = "TVS" Then strDK_Channel = "('TA','TO','CA','WK')"
        Try
            cmd.CommandText = "drop table #" & myStaff.SICode
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        cmd.CommandText = "select RecID, CustID into #" & myStaff.SICode &
            " from RCP where recID in (select RCPID from tkt where " & strDK_TKT & " and DOI between'" &
            Format(Me.txtDOIFrm.Value, "dd-MMM-yy") & "' and '" & Format(Me.TxtDOIto.Value, "dd-MMM-yy") & " 23:56')"
        cmd.ExecuteNonQuery()
        LoadCmb_VAL(Me.cmbCust, "select RecID as VAL, CustShortName as DIS from customerList" _
                    & " where RecID>0 And status='OK' and City='" & myStaff.City _
                    & "' and recID in (select CustID from Cust_detail where status='OK' and cat='Channel' and VAL in " & strDK_Channel & ")" &
                     " and recID in (select CustID from #" & myStaff.SICode & ")")
    End Sub
    Private Sub LblLoadTKT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblLoadTKT.LinkClicked
        mstrProd = "AIR"
        Do While dgrSum.Columns.Count > 0
            dgrSum.Columns.RemoveAt(0)
        Loop
        dgrSum.Columns.Add("Fare", "Fare")
        dgrSum.Columns.Add("Charge", "Charge")
        dgrSum.Columns.Add("Tax", "Tax")
        dgrSum.Columns.Add("UE", "UE")
        dgrSum.Columns.Add("ServiceFee", "ServiceFee")
        dgrSum.Columns.Add("MerchantFee", "MerchantFee")
        dgrSum.Columns.Add("DOF", "DOF")
        dgrSum.Columns.Add("Net2AL", "Net2AL")
        dgrSum.Columns.Add("SRV", "SRV")

        LoadPendingTRX()
        Me.TabControl1.SelectTab("TabPage2")
    End Sub

    Private Sub cmbCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCust.SelectedIndexChanged
        Me.LblLoadTKT.Visible = True
    End Sub

    Private Sub GridTRX_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles GridTKT.CellValueChanged
        If e.RowIndex = -1 Then Exit Sub
        Dim Tcode As String = Me.GridTKT.Item("TCode", e.RowIndex).Value
        Dim chkStatus As Boolean = Me.GridTKT.Item("S", e.RowIndex).Value

        If Me.GridTKT.Columns(e.ColumnIndex).Name = "S" Then
            LblPreview.Enabled = False
        End If

        If Me.cmbCust.Text = "TVSGN" AndAlso Tcode <> "." Then
            For i As Int16 = 0 To Me.GridTKT.Rows.Count - 1
                If Me.GridTKT.Item("TCode", i).Value = Tcode Then Me.GridTKT.Item("S", i).Value = chkStatus
            Next
        End If

    End Sub

    Private Sub GridTRX_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles GridTKT.CurrentCellDirtyStateChanged
        Me.GridTKT.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub
    Private Sub LBlChk_UnChkAll_TKT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LBlChk_UnChkAll_TKT.LinkClicked
        If Me.LBlChk_UnChkAll_TKT.Text = "SelectAll" Then
            Me.LBlChk_UnChkAll_TKT.Text = "UnSelectAll"
            Check_UnCheckTKT(True)
        Else
            Me.LBlChk_UnChkAll_TKT.Text = "SelectAll"
            Check_UnCheckTKT(False)
        End If
    End Sub
    Private Sub Check_UnCheckTKT(pStatus As Boolean)
        For i As Int16 = 0 To Me.GridTKT.Rows.Count - 1
            Me.GridTKT.Item("S", i).Value = pStatus
        Next
    End Sub
    Private Sub txtDOIFrm_ValueChanged(sender As Object, e As EventArgs) Handles txtDOIFrm.ValueChanged, TxtDOIto.ValueChanged
        Me.CmbCounter.Text = ""
    End Sub

    Private Sub LstAL_LostFocus(sender As Object, e As EventArgs) Handles LstAL.LostFocus
        Me.CmbCounter.Text = ""
    End Sub
    Private Function SumAndCheckAir() As Boolean
        Me.LblPreview.Enabled = False
        Dim decTotalVND As Decimal, ROE As Decimal, FTC As Decimal
        Dim decFare As Decimal  ', decNet2AL As Decimal
        Dim decUE As Decimal, decTax As Decimal, decCharge As Decimal
        Dim decSvcFee As Decimal
        Dim decMerchantFee As Decimal
        Dim intRefundMultiplier As Integer
        Dim strDomInt As String = String.Empty

        VN_CRS_Type = ""
        If dgrSum.RowCount > 0 Then
            dgrSum.Rows.RemoveAt(0)
        End If

        dgrSum.Rows.Add()

        For i As Int16 = 0 To Me.GridTKT.Rows.Count - 1
            If Me.GridTKT.Item("S", i).Value Then
                If VN_CRS_Type = "" Then
                    VN_CRS_Type = DefineVN_CRS_Type(Me.GridTKT.Item("TKNO", i).Value)
                Else
                    If VN_CRS_Type <> DefineVN_CRS_Type(Me.GridTKT.Item("TKNO", i).Value) Then
                        Me.txtSummary.Text = "Err. Selected TKTs Issued By Different CRS"
                        Return False
                    End If
                End If
                If GridTKT.Item("DocType", i).Value = "AHC" Then
                    'BO QUA CHECK DOMINT
                ElseIf strDomInt = "" Then
                    strDomInt = GridTKT.Item("DomInt", i).Value
                ElseIf strDomInt <> GridTKT.Item("DomInt", i).Value Then
                    Me.txtSummary.Text = "Unable to combine DOM & INT into 1 invoice"
                    Return False
                End If
                If GridTKT.Item("SRV", i).Value = "R" Then
                    intRefundMultiplier = -1
                Else
                    intRefundMultiplier = 1
                End If

                ROE = 1
                If Me.GridTKT.Item("Curr", i).Value <> "VND" Then
                    ROE = ScalarToDec("RCP", "ROE", "RecID=" & Me.GridTKT.Item("RCPID", i).Value)
                End If
                With GridTKT
                    decFare = decFare + .Item("Fare", i).Value * intRefundMultiplier
                    decUE = decUE + .Item("UE", i).Value * intRefundMultiplier
                    decTax = decTax + .Item("Tax", i).Value * intRefundMultiplier
                    decCharge = decCharge + .Item("Charge", i).Value
                    decMerchantFee = decMerchantFee + .Item("CcCharge", i).Value
                    decSvcFee = Me.GridTKT.Item("ChargeTV", i).Value
                End With

                FTC = GridTKT.Item("Tax", i).Value * intRefundMultiplier _
                        + Me.GridTKT.Item("Charge", i).Value + Me.GridTKT.Item("ChargeTV", i).Value

                'Cong phi ca the
                FTC = FTC + GridTKT.Item("CcCharge", i).Value * intRefundMultiplier

                If VN_CRS_Type = "BSP" Then
                    FTC = FTC + Me.GridTKT.Item("Fare", i).Value * intRefundMultiplier
                Else
                    FTC = FTC + Me.GridTKT.Item("NetToAL", i).Value * intRefundMultiplier
                End If
                decTotalVND = decTotalVND + FTC * ROE + Me.GridTKT.Item("LCL_C", i).Value

                If Not IsDate(dgrSum.Rows(0).Cells("DOF").Value) Then
                    dgrSum.Rows(0).Cells("DOF").Value = Me.GridTKT.Item("DOF", i).Value
                End If
                If dgrSum.Rows(0).Cells("SRV").Value = "" Then
                    dgrSum.Rows(0).Cells("SRV").Value = Me.GridTKT.Item("SRV", i).Value
                ElseIf dgrSum.Rows(0).Cells("SRV").Value <> Me.GridTKT.Item("SRV", i).Value Then
                    MsgBox("S & R can not be combined into 1 Invoice")
                    Return False
                End If
            End If

        Next
        With dgrSum.Rows(0)
            .Cells("Fare").Value = decFare
            .Cells("Charge").Value = decCharge
            .Cells("Tax").Value = decTax
            .Cells("UE").Value = decUE
            .Cells("ServiceFee").Value = decSvcFee
            .Cells("MerChantFee").Value = decMerchantFee

        End With
        Me.txtSummary.Text = IIf(VN_CRS_Type = "NonBSP", "Net2AL", "Fare") & "-Based Total: " & Format(decTotalVND, "#,##0")
        Return True
    End Function
    Private Function SumAndCheckNonAir() As Boolean
        Me.LblPreview.Enabled = False
        Dim objService As New clsVatInvLine
        Dim objFee As New clsVatInvLine

        For i As Int16 = 0 To Me.GridTKT.Rows.Count - 1
            If Me.GridTKT.Item("S", i).Value Then
                With GridTKT
                    Select Case .Item("Service", i).Value
                        Case "TransViet SVC Fee", "Merchant Fee", "Bank Fee"
                            objFee.ProviderCost = objFee.ProviderCost + GridTKT.Item("VND", i).Value
                            objFee.Vat = objFee.Vat + GridTKT.Item("Vat", i).Value
                            objFee.VatPct = GridTKT.Item("VatPct", i).Value
                            objFee.AddDesc(GridTKT.Item("Service", i).Value)
                            objFee.AddRelatedSvc(GridTKT.Item("RelatedService", i).Value)
                            If .Item("Service", i).Value = "TransViet SVC Fee" Then
                                objFee.SvcFeeFound = True
                            ElseIf .Item("Service", i).Value = "Merchant Fee" Then
                                objFee.MerchantFeeFound = True
                            ElseIf .Item("Service", i).Value = "Bank Fee" Then
                                objFee.BankFeeFound = True
                            End If
                            objFee.AddNonDupValues(.Item("PaxName", i).Value, objFee.PaxNames)

                        Case "Accommodations", "Transfer", "Miscellaneous"
                            objService.ProviderCost = objService.ProviderCost + GridTKT.Item("VND", i).Value
                            objService.Vat = objService.Vat + GridTKT.Item("Vat", i).Value
                            objService.VatPct = GridTKT.Item("VatPct", i).Value
                            objService.AddDesc(GridTKT.Item("Service", i).Value)
                            objService.AddNonDupValues(.Item("PaxName", i).Value, objService.PaxNames)

                            If .Item("Service", i).Value = "Accommodations" Then
                                objService.Hotel = True
                            ElseIf .Item("Service", i).Value = "Transfer" Then
                                objService.Car = True
                            ElseIf .Item("Service", i).Value = "Miscellaneous" Then
                                objService.MiscSvc = True
                            End If

                        Case Else
                            MsgBox("New service found. Please stop and inform NMK")
                    End Select

                    If objFee.DifferentVatPct Then
                        MsgBox("Fees with different VAT Percentage can not be combined in 1 Invoice")
                        Return False
                    End If
                    If objService.DifferentVatPct Then
                        MsgBox("Services with different VAT Percentage can not be combined in 1 Invoice")
                        Return False
                    End If

                End With
            End If
        Next
        Do While dgrSum.RowCount > 0
            dgrSum.Rows.RemoveAt(0)
        Loop

        If objFee.Desc <> "" Then
            dgrSum.Rows.Insert(0)
            With dgrSum.Rows(0)
                .Cells("Desc").Value = objFee.Desc
                .Cells("RelatedService").Value = objFee.RelatedSvc
                .Cells("Vat").Value = Math.Round(objFee.Vat, 0)
                .Cells("ProviderCost").Value = Math.Round(objFee.ProviderCost, 0)
                .Cells("TtlToPax").Value = Math.Round(objFee.ProviderCost + objFee.Vat)
                .Cells("VatPct").Value = objFee.VatPct
                .Cells("PaxName").Value = objFee.PaxNames
            End With
        End If

        If objService.Desc <> "" Then
            dgrSum.Rows.Insert(0)

            With dgrSum.Rows(0)
                .Cells("Desc").Value = objService.Desc
                .Cells("RelatedService").Value = objService.RelatedSvc
                .Cells("Vat").Value = Math.Round(objService.Vat, 0)
                .Cells("ProviderCost").Value = Math.Round(objService.ProviderCost, 0)
                .Cells("TtlToPax").Value = Math.Round(objService.ProviderCost + objService.Vat)
                .Cells("VatPct").Value = objService.VatPct
                .Cells("PaxName").Value = objService.PaxNames
            End With

        End If





        Return True
    End Function

    Private Sub LblCalcRASInvAmt_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblSumAndCheck.LinkClicked
        Select Case mstrProd
            Case "AIR"
                SumAndCheckAir()
            Case "N-A"
                SumAndCheckNonAir()
        End Select
        Me.LblPreview.Enabled = True
        Me.LblPreview.Visible = True
    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click
        If dgrSum.RowCount = 0 Then
            dgrSum.Rows.Add()
        End If
    End Sub

    Private Sub lbkAddInvNbr_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddInvNbr.LinkClicked
        Dim frmSelectInv As New frmSelectVatInvNbr


        If frmSelectInv.ShowDialog = DialogResult.OK Then
            Dim lstQuerries As New List(Of String)
            Dim intOldInv As Integer
            Dim tblInvStock As System.Data.DataTable
            For Each objRow As DataGridViewRow In GridTKT.Rows
                If objRow.Cells("S").Value Then
                    intOldInv = ScalarToInt("INV_TVTR", "InvNo", "Status='OK' and Prod='" & mstrProd _
                                & "' And Tktid=" & GridTKT.CurrentRow.Cells("Recid").Value)
                    If intOldInv > 0 Then
                        MsgBox(mstrProd & " " & GridTKT.CurrentRow.Cells("Tkno").Value _
                               & " has been associated with InvNbr " & intOldInv)
                        Exit Sub
                    Else
                        With objRow
                            lstQuerries.Add("insert into INV_TVTR (Prod,TKTID, INVNo, Status, FstUser)" _
                                        & " values ('" & mstrProd & "'," & .Cells("RecId").Value _
                                        & "," & frmSelectInv.SelectedInvNbr _
                                        & ",'OK','" & myStaff.SICode & "')")
                        End With
                    End If
                End If
            Next

            If lstQuerries.Count = 0 Then
                Exit Sub
            End If
            tblInvStock = GetDataTable("Select top 1 RecId, intVal as FromNbr,intVal1 as ToNbr from MISC" _
                                    & " where Cat='VatInvStock' and Status='OK' and City='" & myStaff.City & "'")
            If tblInvStock.Rows.Count = 0 Then
                MsgBox("Unable to use InvNbr " & frmSelectInv.SelectedInvNbr())
                Exit Sub
            ElseIf tblInvStock.Rows(0)("FromNbr") = tblInvStock.Rows(0)("ToNbr") Then
                lstQuerries.Add("update MISC set Status='EX',LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                                    & "' where RecId=" & tblInvStock.Rows(0)("RecId"))
            ElseIf tblInvStock.Rows(0)("FromNbr") < tblInvStock.Rows(0)("ToNbr") Then
                lstQuerries.Add("update MISC set IntVal=intVal+1 " _
                                    & " where RecId=" & tblInvStock.Rows(0)("RecId"))
            End If

            If UpdateListOfQuerries(lstQuerries, Conn) Then
                Select Case mstrProd
                    Case "AIR"
                        LoadPendingTRX()
                    Case "N-A"
                        LoadPendingNonAir()
                End Select

            Else
                MsgBox("Unable to update InvNbr!")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub lbkLoadNonAir_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkLoadNonAir.LinkClicked
        mstrProd = "N-A"
        Do While dgrSum.Columns.Count > 0
            dgrSum.Columns.RemoveAt(0)
        Loop
        dgrSum.Columns.Add("Desc", "Desc")
        dgrSum.Columns.Add("ProviderCost", "ProviderCost")
        dgrSum.Columns.Add("Vat", "Vat")
        dgrSum.Columns.Add("TtlToPax", "TtlToPax")
        dgrSum.Columns.Add("VatPct", "VatPct")
        dgrSum.Columns.Add("RelatedService", "RelatedService")
        dgrSum.Columns.Add("PaxName", "PaxName")
        dgrSum.Columns("ProviderCost").DefaultCellStyle.Format = "#,##0"
        dgrSum.Columns("Vat").DefaultCellStyle.Format = "#,##0"
        dgrSum.Columns("TtlToPax").DefaultCellStyle.Format = "#,##0"
        dgrSum.Columns("VatPct").Width = 30
        dgrSum.Columns("Vat").Width = 60
        LoadPendingNonAir()
        Me.TabControl1.SelectTab("TabPage2")
    End Sub



End Class