Public Class frmSelectTrxForE_Inv
    Private mintCustId As Integer
    Private mstrDomInt As String
    Private mlstSelectedRows As New List(Of DataGridViewRow)
    Private mblnFirstLoadCompleted As Boolean
    Private mblnCustLoadCompleted As Boolean
    Private mintVatDiscount As Integer
    Private mstrInvSettingTable As String = "E_invSettings"
    Private mstrInvoiceTable As String = "E_Inv"
    Private mstrInvDetailTable As String = "E_InvDetails"
    Private mstrInvLinkTable As String = "E_InvLinks"

    Public Sub New(intVatDiscount As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If pblnTT78 Then
            mstrInvoiceTable = mstrInvoiceTable & "78"
            mstrInvDetailTable = mstrInvDetailTable & "78"
            mstrInvLinkTable = mstrInvLinkTable & "78"
            mstrInvSettingTable = mstrInvSettingTable & "78"
        End If

        SetUpValues(intVatDiscount)
    End Sub
    Private Function SetUpValues(intVatDiscount As Integer) As Boolean
        mintVatDiscount = intVatDiscount
        If intVatDiscount = 30 Then
            Me.Text = "Select Transactions For E_Invoice VAT7"
        Else
            Me.Text = "Select Transactions For E_Invoice No VAT Discount"
        End If
        dgrSum.Rows.Clear()
        Return True
    End Function
    Private Sub frmSelectTrxForE_Inv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo(cboCustGroup, "Select Val As Value from MISC where cat='CustGroupName' and val1='E' order by Val", Conn)

        Clear()
        mblnFirstLoadCompleted = True
    End Sub
    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        If mintCustId = 0 Then
            MsgBox("You must select Customer first!")
            Exit Sub
        End If
        Search()
        If dgrTkts.Rows.Count = 1 Then
            dgrTkts.Rows(0).Cells("S").Value = True
        End If

    End Sub


    Private Function Clear() As Boolean
        dtpFrom.Value = Now.AddMonths(-1)
        dtpTo.Value = Now
        cboCustGroup.SelectedIndex = -1
        cboFOP.SelectedIndex = -1
        cboProduct.SelectedIndex = 0
        txtTcode.Text = ""
        If mintCustId = 5102 Then 'TVSGN
            cboPrintToWhom.SelectedIndex = 1
        Else
            cboPrintToWhom.SelectedIndex = 0
        End If
        cboTemplate.SelectedIndex = -1
        Return True
    End Function
    Private Function Search() As Boolean
        lbkCreate.Visible = False

        Do While dgrSum.Columns.Count > 0
            dgrSum.Columns.RemoveAt(0)
        Loop
        Select Case cboProduct.Text
            Case "AIR"
                dgrSum.Columns.Add("Fare", "Fare")
                dgrSum.Columns.Add("Charge", "Charge")
                dgrSum.Columns.Add("Tax", "Tax")
                dgrSum.Columns.Add("UE", "UE")
                dgrSum.Columns.Add("ServiceFee", "ServiceFee")
                dgrSum.Columns.Add("MerchantFee", "MerchantFee")
                dgrSum.Columns.Add("DOF", "DOF")
                dgrSum.Columns.Add("Net2AL", "Net2AL")
                dgrSum.Columns.Add("SRV", "SRV")
                dgrSum.Columns.Add("RcpId", "RcpId")
                dgrSum.Columns.Add("DocType", "DocType")
                dgrSum.Columns.Add("VatPctSf", "VatPctSf")
                dgrSum.Columns.Add("StockCtrl", "StockCtrl")

                LoadPendingTRX(mintVatDiscount)
            Case "N-A"
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
                dgrSum.Columns.Add("RcpId", "RcpId")
                LoadPendingNonAir(mintVatDiscount)
        End Select

        If dgrTkts.RowCount > 0 Then
            lbkSumAndCheck.Visible = True
            lbkChk_UnChkAll_TKT.Visible = True
        Else
            lbkSumAndCheck.Visible = False
            lbkChk_UnChkAll_TKT.Visible = False
        End If
        Return True
    End Function
    Private Sub LoadPendingTRX(intVatDiscount As Integer)
        Dim blnVatAdjusted As Boolean
        Dim decSumFareVat As Decimal
        Dim decVatRatio As Decimal

        Dim strSQL As String = "Select t.RecID, t.RCPID, t.Charge_D, t.RCPNO" _
            & ", t.SRV, t.TKNO, t.DOI, t.Currency as Curr, t.Fare,0 as UE, t.NetToAL" _
            & ", t.Tax, t.Charge, t.ChargeTV, 0 as LCL_C,'' as TCode " _
            & ", r.Charge as CcCharge,t.Itinerary,t.DomInt" _
            & ", (Select count (c.RecId) from tkt c where c.Status<>'XX' and c.RcpId=t.RcpId) as CountTktInRcp" _
            & ", t.PaxName,T.DOF,t.DocType,t.VatPctRounded as VatPct" _
            & ", t.VatPctSf " _
            & ", t.StockCtrl,t.RcpId,t.Tax_D" _
            & " from TKT t left join RCP r on t.RcpId=r.Recid" _
            & " left join ReportData r1 on t.RecId=r1.Tkid" _
            & " where t.Status<>'XX' and r.Status='OK' and t.RCPID in (select RecID from Rcp where CustID=" _
            & cboCustomer.SelectedValue & " and Status='OK') and t.DOI between '" & CreateFromDate(dtpFrom.Value) _
            & "' and '" & CreateToDate(dtpTo.Value) & "'"
        If My.Computer.Name = "5-247" Or My.Computer.Name = "7-111" AndAlso MsgBox("Get E_Inv created ticket?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'lay ca cac ve da xuat E-Inv
        Else
            strSQL = strSQL & " And t.RecId Not in (Select TktId from " & mstrInvLinkTable & " l left join E_inv e on l.InvId=e.InvId" _
                                & " where l.Status='OK' and l.Prod='AIR' and e.Status='OK' and substring(e.MauSo,9,3)='001' "

            If Not pblnTT78 Then
                strSQL = strSQL & " And l.VatDiscount = " & intVatDiscount
            End If
            strSQL = strSQL & ")"
        End If

        If cboFOP.Text <> "" Then
            strSQL = strSQL & " and RcpId in (Select RcpId from FOP where Status<>'XX' and FOP='" _
                & cboFOP.Text & "')"
        End If
        If Me.txtTcode.Text <> "" Then
            strSQL = strSQL & " and RCPID in (select RcpID from FOP where status='OK' and Document='" _
                & Me.txtTcode.Text.Replace("--", "") & "')"
        End If
        Me.dgrTkts.DataSource = GetDataTable(strSQL)
        Me.dgrTkts.Columns("recID").Visible = False
        Me.dgrTkts.Columns("RCPID").Visible = False
        Me.dgrTkts.Columns("Charge_D").Visible = False
        Me.dgrTkts.Columns("SRV").Width = 32
        Me.dgrTkts.Columns("Curr").Width = 32
        Me.dgrTkts.Columns("DOI").Width = 56
        Me.dgrTkts.Columns("Fare").Width = 75
        Me.dgrTkts.Columns("UE").Width = 60
        Me.dgrTkts.Columns("NetToAL").Width = 75
        Me.dgrTkts.Columns("Tax").Width = 56
        Me.dgrTkts.Columns("Charge").Width = 56
        Me.dgrTkts.Columns("ChargeTV").Width = 56
        Me.dgrTkts.Columns("DomInt").Width = 32
        Me.dgrTkts.Columns("LCL_C").Width = 56
        Me.dgrTkts.Columns("CcCharge").Width = 60
        Me.dgrTkts.Columns("DocType").Width = 48
        Me.dgrTkts.Columns("VatPct").Width = 64
        Me.dgrTkts.Columns("VatPctSf").Width = 64
        Me.dgrTkts.Columns("Fare").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgrTkts.Columns("Fare").DefaultCellStyle.Format = "#,##0"
        Me.dgrTkts.Columns("NetToAL").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgrTkts.Columns("NetToAL").DefaultCellStyle.Format = "#,##0"
        Me.dgrTkts.Columns("Tax").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgrTkts.Columns("Tax").DefaultCellStyle.Format = "#,##0"
        Me.dgrTkts.Columns("Charge").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgrTkts.Columns("Charge").DefaultCellStyle.Format = "#,##0"
        Me.dgrTkts.Columns("ChargeTV").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgrTkts.Columns("ChargeTV").DefaultCellStyle.Format = "#,##0"
        Me.dgrTkts.Columns("LCL_C").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgrTkts.Columns("LCL_C").DefaultCellStyle.Format = "#,##0"
        Me.dgrTkts.Columns("UE").DefaultCellStyle.Format = "#,##0"
        Me.dgrTkts.Columns("UE").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgrTkts.Columns("CcCharge").DefaultCellStyle.Format = "#,##0.00"
        Me.dgrTkts.Columns("VatPctSf").DefaultCellStyle.Format = "#,##0"

        For i As Int16 = 0 To Me.dgrTkts.Rows.Count - 1
            If Me.dgrTkts.Item("Curr", i).Value = "USD" And Me.dgrTkts.Item("Charge_D", i).Value.ToString.Contains("VND") Then ' dua SF ve nguyen te
                Me.dgrTkts.Item("LCL_C", i).Value = Split_ChargeTV(Me.dgrTkts.Item("Charge_D", i).Value, "VND")
                Me.dgrTkts.Item("ChargeTV", i).Value = Split_ChargeTV(Me.dgrTkts.Item("Charge_D", i).Value, "USD")
            End If
        Next

        blnVatAdjusted = IsInCustGrp("VAT ADJUSTED", "", mintCustId)

        For i As Int16 = 0 To Me.dgrTkts.Rows.Count - 1
            If IsDBNull(dgrTkts.Item("DomInt", i).Value) AndAlso dgrTkts.Item("DocType", i).Value <> "AHC" Then
                MsgBox("DOM/INT is not specified for " & dgrTkts.Item("Tkno", i).Value)

            ElseIf dgrTkts.Item("DocType", i).Value <> "AHC" AndAlso dgrTkts.Item("DomInt", i).Value = "DOM" Then
                decVatRatio = GetVatRatio(dgrTkts.Item("DOI", i).Value)
                dgrTkts.Item("UE", i).Value = GetTaxAmtFromTaxDetails("UE", dgrTkts.Item("Tax_D", i).Value)
                dgrTkts.Item("Tax", i).Value = dgrTkts.Item("Tax", i).Value - dgrTkts.Item("UE", i).Value

                If blnVatAdjusted AndAlso dgrTkts.Item("UE", i).Value <> dgrTkts.Item("Fare", i).Value * (decVatRatio - 1) Then
                    decSumFareVat = dgrTkts.Item("Fare", i).Value + dgrTkts.Item("UE", i).Value
                    dgrTkts.Item("Fare", i).Value = Math.Round(decSumFareVat / decVatRatio, 0)
                    dgrTkts.Item("UE", i).Value = decSumFareVat - dgrTkts.Item("Fare", i).Value
                End If
            End If

            If mintCustId = 5102 Then 'TV SGN
                Dim RCPID As Integer, TCode As String
                If Me.dgrTkts.Item("TCode", i).Value = "" Then
                    RCPID = Me.dgrTkts.Item("RCPID", i).Value
                    TCode = ScalarToString("FOP", "Document", "RCPID=" & RCPID _
                                           & " And FOP='PSP' and Document <>''")
                    If String.IsNullOrEmpty(TCode) Then TCode = "."
                    For r As Int16 = 0 To Me.dgrTkts.Rows.Count - 1
                        If Me.dgrTkts.Item("RCPID", r).Value = RCPID Then
                            Me.dgrTkts.Item("TCode", r).Value = TCode
                        End If
                    Next
                End If
            End If
        Next
        lbkSelectSameTcode.Visible = False
        lbkSelectSameRCP.Visible = True
    End Sub
    Private Sub LoadPendingNonAir(intVatDiscount As Integer)
        Dim strDK As String = ""

        If Me.txtTcode.Text <> "" Then
            strDK = strDK & " and Tcode='" & Me.txtTcode.Text.Replace("--", "") & "'"
        End If
        Dim strSQL As String = "Select i.RecID, t.Tcode" _
            & ", i.Service,i.Supplier,i.Unit,i.Qty,convert(varchar,t.LstUpdate,6) as FinalDate,i.TTLToPax" _
            & ", case when i.isVATIncl=0 then (i.Cost+i.MU)*i.qty*i.ROE when i.isVATIncl=1 then " _
            & " ((i.Cost+i.MU)*i.ROE/(1.00+i.Vat/100.00))*i.qty end as VND" _
            & ", case when i.isVATIncl=0 then ((i.Cost+i.MU)*i.ROE*i.VAT/100)*i.qty else ((i.Cost+i.MU)-(i.Cost+i.MU)/(1.00+i.Vat/100.00))*i.qty*i.ROE end as VAT" _
            & ",i.VAT as VatPct,i.RelatedItem,isnull(i2.Service,'') as RelatedService,i.PaxName" _
            & ",r.RecId as RcpId,i.Brief,i.DutoanId" _
            & " from DuToan_Item i left join DuToan_Tour t On t.RecId=i.DutoanId" _
            & " left join DuToan_item i2 On i.RelatedItem=i2.RecId" _
            & " left join Rcp r On r.DeliveryStatus=t.Tcode" _
            & " where i.Status='OK' and i.BookOnly=0 and i.CostOnly=0" _
            & " and i.Service <>'VendorPendingBalance'" _
            & " And t.Status='RR' and t.LstUpdate between '" & CreateFromDate(dtpFrom.Value) _
            & "' and '" & CreateToDate(dtpTo.Value) & "'" _
            & strDK _
            & " and r.Status<>'XX'"

        If mintCustId <> 0 Then
            strSQL = strSQL & " And t.CustID=" & mintCustId
        End If

        If My.Computer.Name = "5-247" Or My.Computer.Name = "7-111" AndAlso MsgBox("Get E_Inv created ticket?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'lay ca cac ve da xuat E-Inv
        Else
            strSQL = strSQL & " And i.RecId Not in (Select TktId from " & mstrInvLinkTable & " where Status='OK' and Prod='N-A'"
            If Not pblnTT78 Then
                strSQL = strSQL & " And VatDiscount = " & intVatDiscount
            End If
            strSQL = strSQL & ")"

        End If

        If cboFOP.Text <> "" Then
            strSQL = strSQL & " and R.RecId in (Select RcpId from FOP where Status<>'XX' and FOP='" _
                & cboFOP.Text & "')"
        End If

        strSQL = strSQL & " order by Tcode"

        Me.dgrTkts.DataSource = GetDataTable(strSQL)
        Me.dgrTkts.AutoResizeColumns()
        Me.dgrTkts.Columns("RecID").Visible = False
        Me.dgrTkts.Columns("RelatedItem").Visible = False
        Me.dgrTkts.Columns("VatPct").Width = 40
        'Me.dgrTkts.Columns("InvNo").Width = 40
        'Me.dgrTkts.Columns("InvDate").Width = 60
        Me.dgrTkts.Columns("FinalDate").Width = 70

        Me.dgrTkts.Columns("TTLToPax").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgrTkts.Columns("VND").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgrTkts.Columns("VAT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.dgrTkts.Columns("TTLToPax").DefaultCellStyle.Format = "#,##0"
        Me.dgrTkts.Columns("VND").DefaultCellStyle.Format = "#,##0"
        Me.dgrTkts.Columns("VAT").DefaultCellStyle.Format = "#,##0"

        lbkSelectSameTcode.Visible = True
        lbkSelectSameRCP.Visible = False
    End Sub

    Private Sub LblSumAndCheck_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSumAndCheck.LinkClicked
        mstrDomInt = ""

        Select Case cboProduct.Text
            Case "AIR"
                If SumAndCheckAir(mintVatDiscount) Then
                    lbkCreate.Visible = True
                End If
            Case "N-A"
                If SumAndCheckNonAir(mintVatDiscount) Then
                    lbkCreate.Visible = True
                End If
        End Select
    End Sub
    Private Function SumAndCheckAir(intVatDiscount As Integer) As Boolean
        Dim decTotalVND As Decimal, ROE As Decimal, FTC As Decimal
        Dim decFare As Decimal
        Dim decFareThisTkt As Decimal
        Dim decNet2AlThisTkt As Decimal
        Dim decUE As Decimal
        Dim decUeThisTkt As Decimal
        Dim decTax As Decimal
        Dim decTaxThisTkt As Decimal
        Dim decCharge As Decimal
        Dim decChargeThisTkt As Decimal
        Dim decSvcFee As Decimal
        Dim decSvcFeeThisTkt As Decimal
        Dim decVatAmtSf As Decimal
        Dim decVatAmtSfThisTkt As Decimal

        Dim decMerchantFee As Decimal
        Dim decMerchantFeeThisTkt As Decimal
        Dim intRefundMultiplier As Integer
        Dim lstDocTypes As New List(Of String)
        Dim intVatPct As Integer
        Dim blnGetFtc As Boolean
        Dim blnGetSf As Boolean
        Dim intSumVatPctSf As Integer
        Dim strRcpId As String = String.Empty

        lbkCreate.Visible = False
        mlstSelectedRows.Clear()

        Dim strVN_CRS_Type = ""
        If dgrSum.RowCount > 0 Then
            dgrSum.Rows.RemoveAt(0)
        End If

        dgrSum.Rows.Add()

        For i As Int16 = 0 To Me.dgrTkts.Rows.Count - 1
            If Me.dgrTkts.Item("S", i).Value Then
                mlstSelectedRows.Add(dgrTkts.Rows(i))
                If Not lstDocTypes.Contains(dgrTkts.Item("DocType", i).Value) Then
                    lstDocTypes.Add(dgrTkts.Item("DocType", i).Value)
                End If

                If strVN_CRS_Type = "" Then
                    strVN_CRS_Type = DefineVN_CRS_Type(dgrTkts.Item("TKNO", i).Value)
                Else
                    If strVN_CRS_Type <> DefineVN_CRS_Type(Me.dgrTkts.Item("TKNO", i).Value) Then
                        MsgBox("Err. Selected TKTs Issued By Different CRS")
                        Return False
                    End If
                End If

                If dgrTkts.Item("DocType", i).Value <> "AHC" Then
                    If mstrDomInt = "" Then
                        mstrDomInt = dgrTkts.Item("DomInt", i).Value
                    ElseIf mstrDomInt <> dgrTkts.Item("DomInt", i).Value Then
                        MsgBox("Unable to combine DOM & INT into 1 invoice")
                        Return False
                    End If
                End If

                If dgrTkts.Item("SRV", i).Value = "R" Then
                    intRefundMultiplier = -1
                Else
                    intRefundMultiplier = 1
                End If

                ROE = 1
                If Me.dgrTkts.Item("Curr", i).Value <> "VND" Then
                    ROE = ScalarToDec("RCP", "ROE", "RecID=" & Me.dgrTkts.Item("RCPID", i).Value)
                End If

                With dgrTkts
                    If intVatDiscount = 30 Then
                        Select Case dgrTkts.Item("VatPct", i).Value
                            Case 7, 3.5
                                blnGetFtc = True
                            Case Else
                                blnGetFtc = False
                        End Select
                    Else
                        Select Case dgrTkts.Item("VatPct", i).Value
                            Case <> 7, 3.5, 8
                                blnGetFtc = True
                            Case Else
                                blnGetFtc = False
                        End Select
                    End If

                    If blnGetFtc Then
                        decFareThisTkt = (.Item("Fare", i).Value * intRefundMultiplier)
                        decUeThisTkt = .Item("UE", i).Value * intRefundMultiplier
                        decTaxThisTkt = .Item("Tax", i).Value * intRefundMultiplier
                        decChargeThisTkt = .Item("Charge", i).Value
                        decNet2AlThisTkt = .Item("NetToAL", i).Value
                        intVatPct = dgrTkts.Item("VatPct", i).Value
                    End If

                    If intVatDiscount = 30 AndAlso dgrTkts.Item("VatPctSf", i).Value = 7 Then
                        decSvcFeeThisTkt = dgrTkts.Item("ChargeTV", i).Value
                    Else
                        decSvcFeeThisTkt = dgrTkts.Item("ChargeTV", i).Value
                    End If

                    If intVatDiscount = 0 Then
                        decMerchantFeeThisTkt = .Item("CcCharge", i).Value
                    End If

                    If decSvcFeeThisTkt > 0 Then
                        intSumVatPctSf = dgrTkts.Item("VatPctSf", i).Value
                    End If

                    strRcpId = strRcpId & dgrTkts.Item("RcpId", i).Value & ","
                End With

                decFare = decFare + decFareThisTkt
                decUE = decUE + decUeThisTkt
                decTax = decTax + decTaxThisTkt
                decCharge = decCharge + decChargeThisTkt
                decSvcFee = decSvcFee + decSvcFeeThisTkt
                decMerchantFee = decMerchantFee + decMerchantFeeThisTkt

                FTC = decTaxThisTkt + decCharge + decSvcFeeThisTkt

                'Cong phi ca the
                FTC = FTC + decMerchantFeeThisTkt * intRefundMultiplier

                If strVN_CRS_Type = "BSP" Then
                    FTC = FTC + decMerchantFeeThisTkt * intRefundMultiplier
                Else
                    FTC = FTC + decNet2AlThisTkt * intRefundMultiplier
                End If

                'CHUA RO DIEU CHINH LCC CHARGE THE NAO
                decTotalVND = decTotalVND + FTC * ROE + Me.dgrTkts.Item("LCL_C", i).Value

                If Not IsDate(dgrSum.Rows(0).Cells("DOF").Value) Then
                    dgrSum.Rows(0).Cells("DOF").Value = Me.dgrTkts.Item("DOF", i).Value
                End If
                If dgrSum.Rows(0).Cells("SRV").Value = "" Then
                    dgrSum.Rows(0).Cells("SRV").Value = Me.dgrTkts.Item("SRV", i).Value
                ElseIf dgrSum.Rows(0).Cells("SRV").Value <> Me.dgrTkts.Item("SRV", i).Value Then
                    MsgBox("S & R can not be combined into 1 Invoice")
                    Return False
                End If
            End If
        Next
        If strRcpId.Length > 0 Then
            strRcpId = RemoveLastChr(strRcpId, 1)
        End If
        With dgrSum.Rows(0)
            .Cells("Fare").Value = decFare
            .Cells("Charge").Value = decCharge
            .Cells("Tax").Value = decTax
            .Cells("UE").Value = decUE
            .Cells("ServiceFee").Value = decSvcFee
            .Cells("MerChantFee").Value = decMerchantFee
            .Cells("DocType").Value = Join(lstDocTypes.ToArray, ",")
            .Cells("VatPctSf").Value = intSumVatPctSf
            .Cells("RcpId").Value = strRcpId
        End With
        dgrSum.Columns("UE").DefaultCellStyle.Format = "#,##0.00"

        Return True
    End Function
    Private Function SumAndCheckNonAir(intVatDiscount As Integer) As Boolean
        lbkCreate.Visible = False
        Dim objService As New clsVatInvLine
        Dim objFee As New clsVatInvLine
        Dim objMf As New clsVatInvLine
        mlstSelectedRows.Clear()
        Dim blnGetThisNonAir As Boolean
        Dim blnGetThisSf As Boolean
        Dim blnGetThisMf As Boolean

        For i As Int16 = 0 To Me.dgrTkts.Rows.Count - 1
            If Me.dgrTkts.Item("S", i).Value Then

                mlstSelectedRows.Add(dgrTkts.Rows(i))
                With dgrTkts
                    Select Case .Item("Service", i).Value
                        Case "TransViet SVC Fee"
                            blnGetThisSf = False
                            blnGetThisSf = GetThisTransaction(intVatDiscount, dgrTkts.Item("VatPct", i).Value)
                            If blnGetThisSf Then
                                objFee.ProviderCost = objFee.ProviderCost + dgrTkts.Item("VND", i).Value
                                objFee.Vat = objFee.Vat + dgrTkts.Item("Vat", i).Value
                                objFee.VatPct = dgrTkts.Item("VatPct", i).Value
                                objFee.AddDesc(dgrTkts.Item("Service", i).Value)
                                objFee.AddRelatedSvc(dgrTkts.Item("RelatedService", i).Value)
                                If .Item("Service", i).Value = "TransViet SVC Fee" Then
                                    objFee.SvcFeeFound = True
                                ElseIf .Item("Service", i).Value = "Merchant Fee" Then
                                    objFee.MerchantFeeFound = True
                                ElseIf .Item("Service", i).Value = "Bank Fee" Then
                                    objFee.BankFeeFound = True
                                End If
                                objFee.AddNonDupValues(.Item("PaxName", i).Value, objFee.PaxNames)
                            End If
                        Case "Merchant Fee", "Bank Fee"
                            blnGetThisMf = False
                            blnGetThisMf = GetThisTransaction(intVatDiscount, dgrTkts.Item("VatPct", i).Value)
                            If blnGetThisMf Then
                                objMf.ProviderCost = objMf.ProviderCost + dgrTkts.Item("VND", i).Value
                                objMf.Vat = objMf.Vat + dgrTkts.Item("Vat", i).Value
                                objMf.VatPct = dgrTkts.Item("VatPct", i).Value
                                objMf.AddDesc(dgrTkts.Item("Service", i).Value)
                                objMf.AddRelatedSvc(dgrTkts.Item("RelatedService", i).Value)
                                If .Item("Service", i).Value = "TransViet SVC Fee" Then
                                    objMf.SvcFeeFound = True
                                ElseIf .Item("Service", i).Value = "Merchant Fee" Then
                                    objMf.MerchantFeeFound = True
                                ElseIf .Item("Service", i).Value = "Bank Fee" Then
                                    objMf.BankFeeFound = True
                                End If
                                objMf.AddNonDupValues(.Item("PaxName", i).Value, objMf.PaxNames)
                            End If
                        Case "Accommodations", "Transfer", "Miscellaneous", "Visa", "Meal", "Conf.Room"
                            blnGetThisNonAir = False
                            blnGetThisNonAir = GetThisTransaction(intVatDiscount, dgrTkts.Item("VatPct", i).Value)
                            If blnGetThisNonAir Then
                                objService.ProviderCost = objService.ProviderCost + dgrTkts.Item("VND", i).Value
                                objService.Vat = objService.Vat + dgrTkts.Item("Vat", i).Value
                                objService.VatPct = dgrTkts.Item("VatPct", i).Value
                                objService.AddDesc(dgrTkts.Item("Service", i).Value)
                                objService.AddNonDupValues(.Item("PaxName", i).Value, objService.PaxNames)

                                If .Item("Service", i).Value = "Accommodations" Then
                                    objService.Hotel = True
                                ElseIf .Item("Service", i).Value = "Transfer" Then
                                    objService.Car = True
                                ElseIf .Item("Service", i).Value = "Visa" Then
                                    objService.Visa = True
                                ElseIf .Item("Service", i).Value = "Miscellaneous" Then
                                    objService.MiscSvc = True
                                ElseIf .Item("Service", i).Value = "Conf.Room" Then
                                    objService.MiscSvc = True
                                End If
                            End If
                        Case Else
                            MsgBox("New service found. Please stop and inform Khanhnm")
                    End Select

                    'If objFee.DifferentVatPct Then
                    '    MsgBox("Fees with different VAT Percentage can not be combined in 1 Invoice")
                    '    Return False
                    'End If
                    'If objService.DifferentVatPct Then
                    '    MsgBox("Services with different VAT Percentage can not be combined in 1 Invoice")
                    '    Return False
                    'End If

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
        If objMf.Desc <> "" Then
            dgrSum.Rows.Insert(0)
            With dgrSum.Rows(0)
                .Cells("Desc").Value = objMf.Desc
                .Cells("RelatedService").Value = objMf.RelatedSvc
                .Cells("Vat").Value = Math.Round(objMf.Vat, 0)
                .Cells("ProviderCost").Value = Math.Round(objMf.ProviderCost, 0)
                .Cells("TtlToPax").Value = Math.Round(objMf.ProviderCost + objMf.Vat)
                .Cells("VatPct").Value = objMf.VatPct
                .Cells("PaxName").Value = objMf.PaxNames
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
    Public Function SumAndCheckNonAirVIE() As Boolean
        lbkCreate.Visible = False
        Dim objService As New clsVatInvLine
        Dim objFee As New clsVatInvLine
        mlstSelectedRows.Clear()
        Dim lstMainServices As New List(Of String)
        Dim lstVatPct As New List(Of String)
        For i As Int16 = 0 To Me.dgrTkts.Rows.Count - 1
            If Me.dgrTkts.Item("S", i).Value Then
                With dgrTkts
                    Select Case .Item("Service", i).Value
                        Case "TransViet SVC Fee"
                        Case "Merchant Fee"
                        Case "Bank Fee"
                        Case "Accommodations"
                        Case "Transfer"
                        Case "Meal"
                        Case "Miscellaneous"
                            'bo qua
                    End Select
                End With
            End If
        Next

        For i As Int16 = 0 To Me.dgrTkts.Rows.Count - 1
            If Me.dgrTkts.Item("S", i).Value Then
                mlstSelectedRows.Add(dgrTkts.Rows(i))
                With dgrTkts
                    Select Case .Item("Service", i).Value
                        Case "TransViet SVC Fee", "Merchant Fee", "Bank Fee"
                            objFee.ProviderCost = objFee.ProviderCost + dgrTkts.Item("VND", i).Value
                            objFee.Vat = objFee.Vat + dgrTkts.Item("Vat", i).Value
                            objFee.VatPct = dgrTkts.Item("VatPct", i).Value
                            objFee.AddDesc(dgrTkts.Item("Service", i).Value)
                            objFee.AddRelatedSvc(dgrTkts.Item("RelatedService", i).Value)
                            If .Item("Service", i).Value = "TransViet SVC Fee" Then
                                objFee.SvcFeeFound = True
                            ElseIf .Item("Service", i).Value = "Merchant Fee" Then
                                objFee.MerchantFeeFound = True
                            ElseIf .Item("Service", i).Value = "Bank Fee" Then
                                objFee.BankFeeFound = True
                            End If
                            objFee.AddNonDupValues(.Item("PaxName", i).Value, objFee.PaxNames)

                        Case "Accommodations", "Transfer", "Miscellaneous"
                            objService.ProviderCost = objService.ProviderCost + dgrTkts.Item("VND", i).Value
                            objService.Vat = objService.Vat + dgrTkts.Item("Vat", i).Value
                            objService.VatPct = dgrTkts.Item("VatPct", i).Value
                            objService.AddDesc(dgrTkts.Item("Service", i).Value)
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

    Private Sub lbkCreate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreate.LinkClicked
        Dim lstE_InvLinks As New List(Of String)
        Dim frmE_Inv As New frmE_InvEdit
        Dim blnIssue2TV As Boolean
        Dim strTkIds As String = Strings.Join(GetRecIDsFromDataGridView(dgrTkts, "S").ToArray, ",")
        '^_^20220831 add by 7643 -b-
        '^_^20221006 mark by 7643 -b-
        'Dim mSql As String
        'Dim mReturn As New DataTable
        'Dim i, mDutoanID As Integer
        '^_^20221006 mark by 7643 -e-
        '^_^20220831 add by 7643 -e-

        If cboPrintToWhom.Text = "TransViet" Then
            blnIssue2TV = True
        End If

        If cboProduct.Text = "AIR" Then
            If cboTemplate.Text <> "" AndAlso Not IsInCustGrp(cboTemplate.Text, cboCustomer.Text) Then
                MsgBox("Unmatched Customer and Template!")
                Exit Sub
            End If

            '^_^20220831 add by 7643 -b-
            '^_^20221006 mark by 7643 -b-
            'If cboCustomer.Text = "COCA COLA" Then
            '    mSql = "Select itinerary as Rtg, * from tkt where Qty<>0 and Status<>'xx'" _
            '        & " And RecId in (" & strTkIds & ")"
            '    mReturn = GetDataTable(mSql, Conn)
            '    For i = 0 To mReturn.Rows.Count - 1
            '        If frmE_Inv.GetAirExtRow(mReturn.Rows(i)("RecID"), "Cost Center") = "" Then
            '            MsgBox("RCPNo:" & mReturn.Rows(i)("RCPNo") & " don't have 'Cost Center'!")
            '            Exit Sub
            '        ElseIf frmE_Inv.GetAirExtRow(mReturn.Rows(i)("RecID"), "Trip Purpose") = "" Then
            '            MsgBox("RCPNo:" & mReturn.Rows(i)("RCPNo") & " don't have 'Trip Purpose'!")
            '            Exit Sub
            '        ElseIf frmE_Inv.GetAirExtRow(mReturn.Rows(i)("RecID"), "Employee ID") = "" Then
            '            MsgBox("RCPNo:" & mReturn.Rows(i)("RCPNo") & " don't have 'Employee ID'!")
            '            Exit Sub
            '        ElseIf frmE_Inv.GetAirExtRow(mReturn.Rows(i)("RecID"), "Why No Hotel Booked") = "" Then
            '            MsgBox("RCPNo:" & mReturn.Rows(i)("RCPNo") & " don't have 'Why No Hotel Booked'!")
            '            Exit Sub
            '        ElseIf frmE_Inv.GetAirExtRow(mReturn.Rows(i)("RecID"), "Why Not Booked Online") = "" Then
            '            MsgBox("RCPNo:" & mReturn.Rows(i)("RCPNo") & " don't have 'Why Not Booked Online'!")
            '            Exit Sub
            '        End If
            '    Next
            'End If
            '^_^20221006 mark by 7643 -e-
            '^_^20220831 add by 7643 -e-

            frmE_Inv.LoadDetailsFromRas(dgrSum.Rows(0), strTkIds, cboCustomer.SelectedValue _
                                        , cboProduct.Text, mstrDomInt, blnIssue2TV, cboTemplate.Text, mintVatDiscount)
        ElseIf cboProduct.Text = "N-A" Then
            If cboTemplate.Text <> "" AndAlso Not IsInCustGrp(cboTemplate.Text, cboCustomer.Text) Then
                MsgBox("Unmatched Customer and Template!")
                Exit Sub
            End If

            '^_^20220831 add by 7643 -b-
            '^_^20221006 mark by 7643 -b-
            'If cboCustomer.Text = "COCA COLA" Then
            '    For i = dgrTkts.Rows.Count - 1 To 0 Step -1
            '        If CInt(dgrTkts.Rows(i).Cells("DutoanID").Value) > 0 Then
            '            mDutoanID = CInt(dgrTkts.Rows(i).Cells("DutoanID").Value)
            '            Exit For
            '        End If
            '    Next

            '    If mDutoanID > 0 Then
            '        If frmE_Inv.GetNonAirExtRow(mDutoanID, "Cost Center") = "" Then
            '            MsgBox("RCPNo:" & dgrTkts.Rows(i).Cells("TCode").Value & " don't have 'Cost Center'!")
            '            Exit Sub
            '        ElseIf frmE_Inv.GetNonAirExtRow(mDutoanID, "Trip Purpose") = "" Then
            '            MsgBox("RCPNo:" & dgrTkts.Rows(i).Cells("TCode").Value & " don't have 'Trip Purpose'!")
            '            Exit Sub
            '        ElseIf frmE_Inv.GetNonAirExtRow(mDutoanID, "Employee ID") = "" Then
            '            MsgBox("RCPNo:" & dgrTkts.Rows(i).Cells("TCode").Value & " don't have 'Employee ID'!")
            '            Exit Sub
            '        ElseIf frmE_Inv.GetNonAirExtRow(mDutoanID, "Why No Hotel Booked") = "" Then
            '            MsgBox("RCPNo:" & dgrTkts.Rows(i).Cells("TCode").Value & " don't have 'Why No Hotel Booked'!")
            '            Exit Sub
            '        ElseIf frmE_Inv.GetNonAirExtRow(mDutoanID, "Why Not Booked Online") = "" Then
            '            MsgBox("RCPNo:" & dgrTkts.Rows(i).Cells("TCode").Value & " don't have 'Why Not Booked Online'!")
            '            Exit Sub
            '        End If
            '    End If
            'End If
            '^_^20221006 mark by 7643 -e-
            '^_^20220831 add by 7643 -e-

            frmE_Inv.LoadGridNonAir(dgrTkts, cboCustomer.Text, cboTemplate.Text, mintVatDiscount)
        End If
        If frmE_Inv.ShowDialog = DialogResult.OK Then
            Search()
        End If

    End Sub
    Private Function CreateInvoiceAir() As Boolean
        Dim intTktCount As Integer
        Dim blnSingleTkt As Boolean = True
        Dim strSrv As String = ""
        For i As Int16 = 0 To Me.dgrTkts.Rows.Count - 1
            If Me.dgrTkts.Item("S", i).Value Then
                intTktCount = intTktCount + 1
                If strSrv = "" Then
                    strSrv = dgrTkts.Item("SRV", i).Value
                End If
                If blnSingleTkt AndAlso intTktCount > 1 Then
                    blnSingleTkt = False
                End If
            End If
        Next

    End Function
    Private Sub cboCustType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustType.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            If LoadComboDisplay(cboCustomer, "Select RecId as Value, CustShortName as Display" _
                & " from CustomerList " _
                & " where Status='OK' and RecId in (Select CustId from Cust_Detail where Status='OK'" _
                & " and CAT='Channel' and VAL='" & cboCustType.Text & "') order by CustShortName", Conn) Then
                mblnCustLoadCompleted = True
            End If
        End If
    End Sub
    Public Property DomInt As String
        Get
            Return mstrDomInt
        End Get
        Set(value As String)
            mstrDomInt = value
        End Set
    End Property

    Public Property SelectedRows As List(Of DataGridViewRow)
        Get
            Return mlstSelectedRows
        End Get
        Set(value As List(Of DataGridViewRow))
            mlstSelectedRows = value
        End Set
    End Property
    Private Sub cboCustGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustGroup.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            If LoadComboDisplay(cboCustomer, "Select RecId as Value, CustShortName as Display" _
                & " from CustomerList " _
                & " where Status='OK' and RecId in (Select intVal from Misc where Status='OK'" _
                & " and CAT='CustNameInGroup' and VAL='" & cboCustGroup.Text & "')  order by CustShortName ", Conn) Then
                mblnCustLoadCompleted = True
            End If
        End If
    End Sub

    Private Sub cboCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomer.SelectedIndexChanged
        If mblnCustLoadCompleted Then
            mintCustId = cboCustomer.SelectedValue
        End If
    End Sub

    Private Sub lbkChk_UnChkAll_TKT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkChk_UnChkAll_TKT.LinkClicked
        For Each objRow As DataGridViewRow In dgrTkts.Rows
            objRow.Cells("S").Value = (lbkChk_UnChkAll_TKT.Text = "SelectAll")
        Next
        If lbkChk_UnChkAll_TKT.Text = "SelectAll" Then
            lbkChk_UnChkAll_TKT.Text = "UnSelectAll"
        Else
            lbkChk_UnChkAll_TKT.Text = "SelectAll"
        End If
    End Sub

    Private Sub lbkSelectSameTcode_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectSameTcode.LinkClicked
        Dim strTcode As String = dgrTkts.CurrentRow.Cells("Tcode").Value

        For Each objRow As DataGridViewRow In dgrTkts.Rows
            If objRow.Cells("Tcode").Value = strTcode Then
                objRow.Cells("S").Value = True
            End If
        Next
    End Sub

    Private Sub lbkFromEqualTo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFromEqualTo.LinkClicked
        dtpTo.Value = dtpFrom.Value
    End Sub

    Private Sub lbkSwitchVatPct_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSwitchVatPct.LinkClicked
        Select Case cboVatPct.Text
            Case "VAT_NoDiscount"
                SetUpValues(0)
            Case "VAT7"
                SetUpValues(30)
        End Select
    End Sub

    Private Sub cboProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProduct.SelectedIndexChanged
        'If mblnFirstLoadCompleted Then
        txtTcode.Enabled = (cboProduct.Text = "N-A")
        'End If
    End Sub

    Private Sub cboVatPct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboVatPct.SelectedIndexChanged

    End Sub

    Private Sub lbkSelectSameRCP_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectSameRCP.LinkClicked
        Dim strRcpNo As String = dgrTkts.CurrentRow.Cells("RcpNo").Value

        For Each objRow As DataGridViewRow In dgrTkts.Rows
            If objRow.Cells("RcpNo").Value = strRcpNo Then
                objRow.Cells("S").Value = True
            End If
        Next
    End Sub
End Class