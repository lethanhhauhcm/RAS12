Public Class frmImport2AOP
    Private mstrAction As String
    Private mstrTransactionDate As String
    Private mstrFilterLocalCust As String = " in (select intVal from misc where cat='custnameingroup' and val='CUS NON AIR LOCAL-AOP'" _
                                            & " and status='ok')"
    Private Sub frmImportDailyReport2AOP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboReportType.SelectedIndex = 0
        mstrAction = ""
        LoadComboDisplay(cboAccount, "select Val as Display, VAl1 as value from Misc where Cat='AopDepositAccount' and status='OK'", Conn)
    End Sub

    Private Sub lbkGetInvoiceData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkGetInvoiceData.LinkClicked
        Select Case cboReportType.Text
            Case "NonAirCWT"
                LoadNonAirInvoice(dtpSelectedDate.Value, False)
            Case "NonAirLocal"
                LoadNonAirInvoice(dtpSelectedDate.Value, True)
            Case "Air CTS"
                LoadAirCTSInvoice(dtpSelectedDate.Value)
            Case "Air TVS"
                LoadAirTVSInvoice(dtpSelectedDate.Value)
        End Select

    End Sub
    Private Function LoadAirTVSInvoice(dteSelectedDate As Date) As Boolean
        Dim strFilter2Test As String '= " And r.rcpno='TS0620003873'"
        Dim decAopUsdRoe As Decimal = ScalarToDec("Forex", "TOP 1 BSR", "Status='OK' and Currency='USD' and ApplyROETo='AOP'" _
                                                  & " and EffectDate <='" & CreateFromDate(dteSelectedDate) _
                                                  & "' order by EffectDate")

        Dim strQuerry As String = "select r.CustId, r.RcpNo,R.Srv" _
                & " ,(case (select count (*) from tkt t where t.Status<>'XX' and t.RCPID=r.RecID and T.DocType in ('GRP','MCO')) when 0 then 'FIT' else 'GRP' end) AS GRP" _
                & ",'' as AopRecord" _
                & ",r.Roe*r.TtlDue as InvAmt, 0 as SvcFee" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as InvDate" _
                & ",l.CustShortName,'' as TourCode" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID  ORDER BY TKNO For Xml path('') ) AS Tkno,'' as DepDate" _
                & ",'' as Account,'' as AccountName" _
                & ",'' as OriDeposits" _
                & " ,(select distinct t.DocType+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS DocTypes,AOPListID" _
                & ",'' as TspVendor,'' as TspVendorAopId,'' as TspCust,'' as TspCustAopId,'' as TspClass" _
                & ", (case r.CustShortName when 'VYM' then " & decAopUsdRoe & " else 1 end)  as ROE" _
                & " from Rcp r" _
                & " left join CustomerList l on l.Recid=r.CustId" _
                & " where r.status='OK' and r.Counter='TVS' and r.FstUpdate between '" & CreateFromDate(dteSelectedDate) & "' and '" & CreateToDate(dteSelectedDate) & "'" _
                & strFilter2Test _
                & " order by r.FstUpdate"
        Dim lstMissingCustShortNames As New List(Of String)

        mstrTransactionDate = CreateFromDate(dtpSelectedDate.Value)

        LoadDataGridView(dgrRasData, strQuerry, Conn)


        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                If .Cells("AOPListID").Value = "" Then
                    If Not lstMissingCustShortNames.Contains(.Cells("CustShortName").Value) Then
                        lstMissingCustShortNames.Add(.Cells("CustShortName").Value)
                    End If
                End If
                .Cells("Tkno").Value = Mid(.Cells("tkno").Value, 1, Len(.Cells("Tkno").Value) - 1)
                If .Cells("GRP").Value = "GRP" Then
                    Dim tblTourCode As DataTable
                    Dim tblTourInfo As DataTable

                    tblTourCode = GetDataTable("SELECT tourcode,Sdate FROM FLX.DBO.TOS_TOURCODE WHERE status ='ok' and TourCode in " _
                                                       & "(select document from FOP where status='OK' and Rcpno='" & .Cells("RcpNo").Value & "')", Conn_Web)
                    If tblTourCode.Rows.Count > 0 Then

                        .Cells("TourCode").Value = tblTourCode.Rows(0)("TourCode")
                        .Cells("DepDate").Value = tblTourCode.Rows(0)("Sdate")
                        tblTourInfo = GetTourCodeTable(.Cells("TourCode").Value)
                    End If


                    Select Case .Cells("CustShortName").Value

                        Case "TVSGN"
                            .Cells("CustShortName").Value = "TV_SGN"
                            .Cells("CustiD").Value = -7
                            .Cells("AopListId").Value = "8000017B-1579073162"
                            .Cells("TspVendor").Value = "AL TVSGN GROUP"
                            .Cells("TspVendorAopId").Value = "8000013C-1578889907"
                            .Cells("TspCust").Value = tblTourInfo.Rows(0)("TspCust")
                            .Cells("TspCustAopId").Value = GetCustomerAopId(.Cells("TspCust").Value)
                            .Cells("TspClass").Value = tblTourInfo.Rows(0)("TspClass")
                        Case "GDSSGN"
                            .Cells("CustShortName").Value = "GDS_SGN"
                            .Cells("CustiD").Value = -8
                            .Cells("AopListId").Value = "8000017A-1579073000"
                            .Cells("TspVendor").Value = "AL GDSSGN GROUP"
                            .Cells("TspVendorAopId").Value = "8000013F-1578895822"
                            .Cells("TspCust").Value = tblTourInfo.Rows(0)("TspCust")
                            .Cells("TspCustAopId").Value = GetCustomerAopId(.Cells("TspCust").Value)
                            .Cells("TspClass").Value = tblTourInfo.Rows(0)("TspClass")
                        Case Else
                            '.Cells("AopRecord").Value = ""
                    End Select
                    .Cells("AopRecord").Value = GetAopRecordNameTVS(.Cells("SRV").Value, .Cells("GRP").Value, .Cells("CustShortName").Value)
                    If .Cells("AopRecord").Value = "Deposit" Then
                        .Cells("DepDate").Value = Format(ScalarToDate("tkt", "Top 1 DOF", "Status<>'XX' and Tkno='" & Split(.Cells("Tkno").Value, "/")(0) & "' order by DOI desc"), "dd-MMM-yy")

                    End If


                Else    'FIT
                    Dim tblOriDeposit As DataTable
                    tblOriDeposit = GetDataTable("select F.Document,r.ROE*r.TTLDue AS VND" _
                        & " from FOP f left join tkt t on f.Document=t.TKNO" _
                        & " left join rcp r on r.RecID=t.RCPID" _
                        & " where f.FOP='EXC' and f.Status='OK' and f.RCPNo='" & .Cells("RcpNo").Value _
                        & "' and t.Status<>'XX' and t.Srv='S' and t.DocType in ('GRP','MCO') and r.Status='OK'")
                    If tblOriDeposit.Rows.Count > 0 Then
                        Dim strOriDep As String = String.Empty
                        For Each objRowDep As DataRow In tblOriDeposit.Rows
                            strOriDep = strOriDep & objRowDep("Document") & "/" & Format(objRowDep("VND"), "#,##0") & "|"
                        Next
                        .Cells("OriDeposits").Value = Mid(strOriDep, 1, strOriDep.Length - 1)
                    End If
                    If .Cells("SRV").Value = "S" Then
                        .Cells("AopRecord").Value = "Invoice"
                    Else
                        .Cells("AopRecord").Value = "CreditMemo"
                    End If

                    Dim tblTourCode As DataTable
                    Dim tblTourInfo As DataTable

                    tblTourCode = GetDataTable("SELECT tourcode,Sdate FROM FLX.DBO.TOS_TOURCODE WHERE status ='ok' and TourCode in " _
                                                       & "(select document from FOP where status='OK' and Rcpno='" & .Cells("RcpNo").Value & "')", Conn_Web)
                    If tblTourCode.Rows.Count > 0 Then
                        .Cells("TourCode").Value = tblTourCode.Rows(0)("TourCode")
                        .Cells("DepDate").Value = tblTourCode.Rows(0)("Sdate")
                        tblTourInfo = GetTourCodeTable(.Cells("TourCode").Value)
                    End If

                    Select Case .Cells("CustShortName").Value
                        Case "TVSGN"
                            .Cells("TspVendor").Value = "AL TVSGN"
                            .Cells("TspVendorAopId").Value = "8000013B-1578889879"
                            .Cells("TspCust").Value = tblTourInfo.Rows(0)("TspCust")
                            .Cells("TspCustAopId").Value = GetCustomerAopId(.Cells("TspCust").Value)
                            .Cells("TspClass").Value = tblTourInfo.Rows(0)("TspClass")

                        Case "GDSSGN"
                            .Cells("TspVendor").Value = "AL GDSSGN"
                            .Cells("TspVendorAopId").Value = "8000013E-1578895760"
                            .Cells("TspCust").Value = tblTourInfo.Rows(0)("TspCust")
                            .Cells("TspCustAopId").Value = GetCustomerAopId(.Cells("TspCust").Value)
                            .Cells("TspClass").Value = tblTourInfo.Rows(0)("TspClass")
                        Case Else
                            '.Cells("AopRecord").Value = ""
                    End Select
                End If
            End With
        Next
        If lstMissingCustShortNames.Count > 0 Then
            MsgBox("You must ask PQT to update AOPListID for the following Customers: " & Join(lstMissingCustShortNames.ToArray(), ","))
        End If

        dgrRasData.Columns("InvAmt").DefaultCellStyle.Format = "#,##0"
        dgrRasData.Columns("SvcFee").DefaultCellStyle.Format = "#,##0"
        dgrRasData.Columns("CustId").Visible = False
        dgrRasData.Columns("Account").Visible = False
        dgrRasData.Columns("InvAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrRasData.Columns("SvcFee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgrRasData.Columns("MerchantFee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        mstrAction = "AirTvsInvoice"
        Return True
    End Function


    Private Function LoadAirTVSBill(dteSelectedDate As Date) As Boolean
        Dim strFilter2Test As String '= " And r.rcpno='TS0620003873'"
        Dim strQuerry As String = "select r.Vendor,c.CustShortName, r.RcpNo,R.Srv" _
                & " ,(case (select count (*) from tkt t where t.Status<>'XX' and t.RCPID=r.RecID and T.DocType in ('GRP','MCO')) when 0 then 'FIT' else 'GRP' end) AS GRP" _
                & " ,(select top 1 DOI from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0) AS DOI" _
                & " ,((select sum((NetToAL+Tax)+Charge*t.Qty) from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0)" _
                & " - (select isnull(sum(Amount),0) from FOP f where f.Status='OK' and f.RCPID=r.RecID and f.FOP='EXC'))*r.Roe  AS BillAmt" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & " ,(select distinct t.DocType+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS DocType" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS Tkno" _
                & ",'' AS TourCode,'' as DepDate,'' as TspCust,'' as TspCustAopId,'' as TspClass" _
                & ",v.AOPListID as VendorAopId,c.AOPListID as CustAopId,r.CustId " _
                & " from Rcp r" _
                & " left join CustomerList c on c.Recid=r.CustId" _
                & " left join Vendor v on v.Recid=r.VendorId" _
                & " where r.status='OK' and r.Srv<>'V' and r.Counter='TVS' and r.FstUpdate between '" & CreateFromDate(dteSelectedDate) & "' and '" & CreateToDate(dteSelectedDate) & "'" _
                & " and r.RecId not in (Select RcpId from tkt where DocType='AHC')" _
                & strFilter2Test _
                & " order by r.FstUpdate"
        Dim lstMissingVendorShortNames As New List(Of String)
        Dim lstMissingCustShortNames As New List(Of String)

        mstrTransactionDate = CreateFromDate(dtpSelectedDate.Value)

        LoadDataGridView(dgrRasData, strQuerry, Conn)


        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                If .Cells("Vendor").Value = "" Then
                    MsgBox("You must ask TVS to update Vendor for " & .Cells("Rcpno").Value)
                    Return False
                End If
                If IsDBNull(.Cells("VendorAopId").Value) Then
                    If Not lstMissingVendorShortNames.Contains(.Cells("Vendor").Value) Then
                        lstMissingVendorShortNames.Add(.Cells("Vendor").Value)
                    End If
                End If
                If .Cells("CustAopId").Value = "" Then
                    If Not lstMissingCustShortNames.Contains(.Cells("CustShortName").Value) Then
                        lstMissingCustShortNames.Add(.Cells("CustShortName").Value)
                    End If
                End If

                .Cells("Tkno").Value = Mid(.Cells("tkno").Value, 1, Len(.Cells("Tkno").Value) - 1)

                If .Cells("GRP").Value = "GRP" Then
                    Dim tblTourCode As DataTable
                    Dim tblTourInfo As DataTable

                    tblTourCode = GetDataTable("SELECT tourcode,Sdate FROM FLX.DBO.TOS_TOURCODE WHERE status ='ok' and TourCode in " _
                                                       & "(select document from FOP where status='OK' and Rcpno='" & .Cells("RcpNo").Value & "')", Conn_Web)
                    If tblTourCode.Rows.Count > 0 Then

                        .Cells("TourCode").Value = tblTourCode.Rows(0)("TourCode")
                        .Cells("DepDate").Value = tblTourCode.Rows(0)("Sdate")
                        tblTourInfo = GetTourCodeTable(.Cells("TourCode").Value)
                    End If

                    Select Case .Cells("CustShortName").Value
                        Case "TVSGN"
                            .Cells("CustShortName").Value = "TV_SGN"
                            .Cells("CustiD").Value = -7
                            .Cells("CustAopId").Value = "8000017B-1579073162"
                            '.Cells("TspVendor").Value = "AL TVSGN GROUP"
                            '.Cells("TspVendorAopId").Value = "8000013C-1578889907"
                            '.Cells("TspCust").Value = tblTourInfo.Rows(0)("TspCust")
                            '.Cells("TspCustAopId").Value = GetCustomerAopId(.Cells("TspCust").Value)
                            '.Cells("TspClass").Value = tblTourInfo.Rows(0)("TspClass")
                        Case "GDSSGN"
                            .Cells("CustShortName").Value = "GDS_SGN"
                            .Cells("CustiD").Value = -8
                            .Cells("CustAopId").Value = "8000017A-1579073000"
                            '.Cells("TspVendor").Value = "AL GDSSGN GROUP"
                            '.Cells("TspVendorAopId").Value = "8000013F-1578895822"
                            '.Cells("TspCust").Value = tblTourInfo.Rows(0)("TspCust")
                            '.Cells("TspCustAopId").Value = GetCustomerAopId(.Cells("TspCust").Value)
                            '.Cells("TspClass").Value = tblTourInfo.Rows(0)("TspClass")
                        Case Else
                            '.Cells("AopRecord").Value = ""
                    End Select

                End If
            End With
        Next
        If lstMissingVendorShortNames.Count > 0 Then
            MsgBox("You must ask PQT to update AOPListID for the following Vendor: " & Join(lstMissingCustShortNames.ToArray(), ","))
        End If
        If lstMissingCustShortNames.Count > 0 Then
            MsgBox("You must ask PQT to update AOPListID for the following Customer: " & Join(lstMissingCustShortNames.ToArray(), ","))
        End If

        dgrRasData.Columns("BillAmt").DefaultCellStyle.Format = "#,##0"
        'dgrRasData.Columns("SvcFee").DefaultCellStyle.Format = "#,##0"
        'dgrRasData.Columns("MerchantFee").DefaultCellStyle.Format = "#,##0"
        dgrRasData.Columns("BillAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgrRasData.Columns("SvcFee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgrRasData.Columns("MerchantFee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        mstrAction = "AirTvsBill"
        Return True
    End Function
    Private Function LoadAirCTSBill(dteSelectedDate As Date) As Boolean
        Dim strFilter2Test As String '= " And r.rcpno='TS0720000334'"
        Dim strQuerry As String = "select r.Vendor,c.CustShortName, r.RcpNo,R.Srv" _
                & " ,(select top 1 DOI from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0) AS DOI" _
                & " ,(select sum((NetToAL+Tax)+Charge*t.Qty) from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0) AS BillAmt" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & " ,(select distinct t.DocType+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS DocType" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS Tkno" _
                & ",v.AOPListID as VendorAopId,c.AOPListID as CustAopId " _
                & " from Rcp r" _
                & " left join CustomerList c on c.Recid=r.CustId" _
                & " left join Vendor v on v.Recid=r.VendorId" _
                & " where r.status='OK' and r.Srv<>'V' and r.Counter='CWT' and r.FstUpdate between '" & CreateFromDate(dteSelectedDate) & "' and '" & CreateToDate(dteSelectedDate) & "'" _
                & " and r.RecId not in (Select RcpId from tkt where DocType='AHC')" _
                & strFilter2Test _
                & " order by r.FstUpdate"
        Dim lstMissingVendorShortNames As New List(Of String)
        Dim lstMissingCustShortNames As New List(Of String)

        mstrTransactionDate = CreateFromDate(dtpSelectedDate.Value)

        LoadDataGridView(dgrRasData, strQuerry, Conn)


        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                If .Cells("Vendor").Value = "" Then
                    MsgBox("You must ask CTS to update Vendor for " & .Cells("Rcpno").Value)
                    Return False
                End If
                If IsDBNull(.Cells("VendorAopId").Value) Then
                    If Not lstMissingVendorShortNames.Contains(.Cells("Vendor").Value) Then
                        lstMissingVendorShortNames.Add(.Cells("Vendor").Value)
                    End If
                End If
                If .Cells("CustAopId").Value = "" Then
                    If Not lstMissingCustShortNames.Contains(.Cells("CustShortName").Value) Then
                        lstMissingCustShortNames.Add(.Cells("CustShortName").Value)
                    End If
                End If

                .Cells("Tkno").Value = Mid(.Cells("tkno").Value, 1, Len(.Cells("Tkno").Value) - 1)

            End With
        Next
        If lstMissingVendorShortNames.Count > 0 Then
            MsgBox("You must ask PQT to update AOPListID for the following Vendor: " & Join(lstMissingCustShortNames.ToArray(), ","))
        End If
        If lstMissingCustShortNames.Count > 0 Then
            MsgBox("You must ask PQT to update AOPListID for the following Customer: " & Join(lstMissingCustShortNames.ToArray(), ","))
        End If

        dgrRasData.Columns("BillAmt").DefaultCellStyle.Format = "#,##0"
        'dgrRasData.Columns("SvcFee").DefaultCellStyle.Format = "#,##0"
        'dgrRasData.Columns("MerchantFee").DefaultCellStyle.Format = "#,##0"
        dgrRasData.Columns("BillAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgrRasData.Columns("SvcFee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgrRasData.Columns("MerchantFee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        mstrAction = "AirCtsBill"
        Return True
    End Function
    Private Function LoadAirCtsInvoice(dteSelectedDate As Date) As Boolean
        Dim strFilter2Test As String '= " And r.RcpNo='TS0720000334'"
        Dim strQuerry As String = "select (case when m.RecID is null then 1 else 2 end) as InvCount, r.CustId, r.RcpNo,R.Srv" _
                & ",r.TtlDue as InvAmt, 0 as SvcFee" _
                & ",r.Charge as MerchantFee" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as InvDate" _
                & ",l.CustShortName,AOPListID" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS Tkno" _
                & " from Rcp r" _
                & " left join CustomerList l on l.Recid=r.CustId" _
                & " left join Misc m on r.CustId=m.intVal and m.Cat='CustNameInGroup' and m.VAL='2 INVOICES CUS' and m.Status='OK'" _
                & " where r.status='OK' and r.Counter='CWT' and r.FstUpdate between '" & CreateFromDate(dteSelectedDate) & "' and '" & CreateToDate(dteSelectedDate) & "'" _
                & strFilter2Test _
                & " order by r.FstUpdate"
        Dim lstMissingCustShortNames As New List(Of String)

        mstrTransactionDate = CreateFromDate(dtpSelectedDate.Value)

        LoadDataGridView(dgrRasData, strQuerry, Conn)


        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                If .Cells("AOPListID").Value = "" Then
                    If Not lstMissingCustShortNames.Contains(.Cells("CustShortName").Value) Then
                        lstMissingCustShortNames.Add(.Cells("CustShortName").Value)
                    End If
                End If
                .Cells("Tkno").Value = Mid(.Cells("tkno").Value, 1, Len(.Cells("Tkno").Value) - 1)
                If .Cells("InvCount").Value = 2 Then
                    .Cells("SvcFee").Value = ScalarToDec("tkt", "sum(ChargeTV)", "Status<>'xx' and Rcpno='" & .Cells("RcpNo").Value & "'")
                    .Cells("InvAmt").Value = .Cells("InvAmt").Value - .Cells("SvcFee").Value
                End If
            End With
        Next
        If lstMissingCustShortNames.Count > 0 Then
            MsgBox("You must ask PQT to update AOPListID for the following Customers: " & Join(lstMissingCustShortNames.ToArray(), ","))
        End If

        dgrRasData.Columns("InvAmt").DefaultCellStyle.Format = "#,##0"
        dgrRasData.Columns("SvcFee").DefaultCellStyle.Format = "#,##0"
        dgrRasData.Columns("MerchantFee").DefaultCellStyle.Format = "#,##0"
        dgrRasData.Columns("InvAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrRasData.Columns("SvcFee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrRasData.Columns("MerchantFee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        mstrAction = "AirCtsInvoice"
        Return True
    End Function
    Private Function LoadNonAirInvoice(dteSelectedDate As Date, blnLocal As Boolean) As Boolean
        Dim strFilter2Test As String '= " and Tcode='ABBOTT ALSA_MICE10512PY'"
        Dim strFilterLocalCust As String
        Dim strQuerry As String

        mstrTransactionDate = CreateFromDate(dtpSelectedDate.Value)
        If blnLocal Then
            mstrAction = "NonAirInvoiceLocal"
            strFilterLocalCust = mstrFilterLocalCust
        Else
            mstrAction = "NonAirInvoiceCWT"
            strFilterLocalCust = " not " & mstrFilterLocalCust
        End If
        strQuerry = "select t.CustId, t.TCode,t.BillingBy" _
                & ",(select sum(ttltopax) from DuToan_Item where status='ok' and DuToanID=t.RecID ) as InvAmt" _
                & ",(select isnull(sum(ttltopax),0) from DuToan_Item where status='ok' and DuToanID=t.RecID and service='merchant fee') as MerchantFee" _
                & ", s.strval2 As OriginalText,s.strval4 As Replacement4Invoice" _
                & ",REPLACE(tcode,strval2,strval4) as RefNumber" _
                & ",REPLACE(tcode,strval1,'') as InvDate" _
                & ",t.CustShortName,AOPListID" _
                & " from DuToan_Tour t" _
                & " left join ShortenCustomerName s on t.CustId=s.intVal1" _
                & " left join CustomerList l on l.Recid=t.CustId" _
                & " where t.status='RR' and t.LstUpdate between '" & CreateFromDate(dteSelectedDate) & "' and '" & CreateToDate(dteSelectedDate) & "'" _
                & " and t.CustId " & strFilterLocalCust _
                & strFilter2Test _
                & " order by t.tcode"
        Dim lstMissingCustShortNames As New List(Of String)
        LoadDataGridView(dgrRasData, strQuerry, Conn)

        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                If IsDBNull(.Cells("InvDate").Value) Then
                    If Not lstMissingCustShortNames.Contains(.Cells("CustShortName").Value) Then
                        lstMissingCustShortNames.Add(.Cells("CustShortName").Value)
                    End If
                Else
                    .Cells("InvDate").Value = ExtractDateInTcode(.Cells("InvDate").Value)
                End If
            End With
        Next
        If lstMissingCustShortNames.Count > 0 Then

            MsgBox("You must update ShortenNameList for the following Customers:" & Join(lstMissingCustShortNames.ToArray(), ","))
        End If
        For Each objRow As DataGridViewRow In dgrRasData.Rows
            If Len(objRow.Cells("RefNumber").Value) > 11 Then
                MsgBox("Invalid length for RefNumber " & objRow.Cells("RefNumber").Value & vbNewLine & ".You must change CustNameShortern rule")
                Exit For
            End If
        Next
        dgrRasData.Columns("OriginalText").Visible = False
        dgrRasData.Columns("Replacement4Invoice").Visible = False
        dgrRasData.Columns("CustShortName").Visible = False
        dgrRasData.Columns("InvAmt").DefaultCellStyle.Format = "#,##0"
        dgrRasData.Columns("MerchantFee").DefaultCellStyle.Format = "#,##0"
        dgrRasData.Columns("InvAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrRasData.Columns("MerchantFee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        Return True
    End Function
    Private Function LoadNonAirBill(dteSelectedDate As Date, blnLocal As Boolean) As Boolean
        Dim strFilter2Test As String ' = " and Tcode='ABBOTT GLOMED BD_MCE1055P.11'"
        Dim strFilterLocalCust As String
        Dim strQuerry As String

        mstrTransactionDate = CreateFromDate(dtpSelectedDate.Value)
        If blnLocal Then
            mstrAction = "NonAirBillLocal"
            strFilterLocalCust = mstrFilterLocalCust
        Else
            mstrAction = "NonAirBillCWT"
            strFilterLocalCust = " not " & mstrFilterLocalCust
        End If
        strQuerry = "select t.CustId, t.TCode,i.Vendor,i.VendorId" _
                & ",CCurr,Sum(TtlToVendor_NguyenTe) as BillAmt" _
                & ", s.strval2 As OriginalText,s.strval3 As Replacement4Bill" _
                & ",REPLACE(tcode,strval2,strval3) as RefNumber" _
                & ",REPLACE(tcode,strval1,'') as BillDate" _
                & ",t.CustShortName,l.AOPListID,c.AOPListID as CustAOPListID" _
                & " from DuToan_Tour t" _
                & " left join ShortenCustomerName s on t.CustId=s.intVal1" _
                & " left join Dutoan_item i on i.DutoanId=t.RecId" _
                & " left join Vendor l on l.Recid=i.VendorId" _
                & " left join CustomerList c on t.CustId=c.RecId" _
                & " where t.status='RR' and t.LstUpdate between '" & CreateFromDate(dteSelectedDate) & "' and '" & CreateToDate(dteSelectedDate) & "'" _
                & " and i.Status='OK' and i.VendorId<>2" _
                & " and t.CustId" & strFilterLocalCust _
                & strFilter2Test _
                & " group by t.CustId,t.CustShortName, t.TCode,i.Vendor,i.VendorId,strVal1,strVal2,strVal3,l.AOPListID,CCurr,c.AOPListID" _
                & " order by t.tcode"
        Dim lstMissingCustShortNames As New List(Of String)
        LoadDataGridView(dgrRasData, strQuerry, Conn)

        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                If IsDBNull(.Cells("BillDate").Value) Then
                    If Not lstMissingCustShortNames.Contains(.Cells("CustShortName").Value) Then
                        lstMissingCustShortNames.Add(.Cells("CustShortName").Value)
                    End If
                Else
                    .Cells("BillDate").Value = ExtractDateInTcode(.Cells("BillDate").Value)
                End If
            End With
        Next
        If lstMissingCustShortNames.Count > 0 Then

            MsgBox("You must update ShortenNameList for the following Customers:" & Join(lstMissingCustShortNames.ToArray(), ","))
        End If
        For Each objRow As DataGridViewRow In dgrRasData.Rows
            If Len(objRow.Cells("RefNumber").Value) > 20 Then
                MsgBox("Invalid length for RefNumber " & objRow.Cells("RefNumber").Value & vbNewLine & ".You must change CustNameShortern rule")
                Exit For
            End If
        Next
        dgrRasData.Columns("OriginalText").Visible = False
        dgrRasData.Columns("Replacement4Bill").Visible = False
        dgrRasData.Columns("CustShortName").Visible = False
        dgrRasData.Columns("BillAmt").DefaultCellStyle.Format = "#,##0"
        'dgrRasData.Columns("MerchantFee").DefaultCellStyle.Format = "#,##0"
        dgrRasData.Columns("BillAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'dgrRasData.Columns("MerchantFee").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        Return True
    End Function
    Private Sub lbkExport2AOP_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkExport2AOP.LinkClicked
        Dim dteStart As Date = Now
        Dim tblImportedRecords As DataTable
        Dim blnSuccess As Boolean = False

        If ConnAOP.State <> ConnectionState.Open Then
            ConnAOP.ConnectionString = CnStr_AOP
            ConnAOP.Open()
        End If
        tblImportedRecords = GetDataTable("Select FstUser,FstUpdate from Misc where CAT='Export2AOP' and VAL='" & mstrAction _
                                          & "' and dteVal='" & mstrTransactionDate & "'", Conn)
        If tblImportedRecords.Rows.Count > 0 Then
            Dim frmShow As New frmShowTableContent(tblImportedRecords, "Data has been imported in the past by below User and date")
            frmShow.ShowDialog()

            If MsgBox("Continue import?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        Select Case mstrAction
            Case "NonAirBillCWT", "NonAirBillLocal"
                blnSuccess = ImportNonAirBill()
            Case "NonAirInvoiceCWT", "NonAirInvoiceLocal"
                blnSuccess = ImportNonAirInvoice()
            Case "AirCtsInvoice"
                blnSuccess = ImportAirInvoiceCTS()
            Case "AirTvsInvoice"
                blnSuccess = ImportAirInvoiceTVS()
            Case "AirCtsBill"
                blnSuccess = ImportAirBill("CTS")
            Case "AirTvsBill"
                blnSuccess = ImportAirBill("FTK")
        End Select

        If blnSuccess Then
            ExecuteNonQuerry("insert into MISC (CAT, VAL,dteVal,FstUser) values ('Export2AOP','" & mstrAction & "','" & mstrTransactionDate _
                             & "','" & myStaff.SICode & "')", Conn)
        End If
        MsgBox(DateDiff(DateInterval.Second, dteStart, Now) & "/" & dgrRasData.RowCount)

    End Sub
    Private Function ImportNonAirBill() As Boolean
        Dim i As Integer

        For Each objRow As DataGridViewRow In dgrRasData.Rows
            i = i + 1
            If i Mod 4 = 0 Then
                ConnAOP.Close()
                ConnAOP.Open()
            End If
            With objRow
                If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...BillItemLine (ItemLineBillableStatus, ItemLineCustomerRefListID" _
                & ",VendorRefListID,ItemLineClassRefFullName,APAccountRefFullName" _
                & ",TxnDate,RefNumber,ItemLineItemRefFullName, ItemLineDesc" _
                & ",ItemLineAmount,memo,FQSaveToCache) VALUES ('Billable','" & .Cells("CustAOPListid").Value & "','" & .Cells("AOPListid").Value _
                & "','CTS','VENDOR PAYABLE (" & .Cells("CCurr").Value & ")','" _
                & .Cells("BillDate").Value & "','" & .Cells("RefNumber").Value & "','" & .Cells("Tcode").Value & "','" & .Cells("Tcode").Value _
                & "'," & .Cells("BillAmt").Value & ",'" & .Cells("RefNumber").Value & "',1)", ConnAOP) Then
                    MsgBox("Unable to insert BillItemLine " & .Cells("Tcode").Value & " - " & .Cells("Vendor").Value)

                ElseIf Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...Bill (VendorRefListID, APAccountRefFullName, TxnDate, RefNumber" _
                & ", Memo) VALUES ('" & .Cells("AOPListid").Value & "','VENDOR PAYABLE (" & .Cells("CCurr").Value & ")','" _
                & .Cells("BillDate").Value & "','" & .Cells("RefNumber").Value & "','" & .Cells("Tcode").Value & "')", ConnAOP) Then
                    MsgBox("Unable to insert Bill " & .Cells("Tcode").Value & " - " & .Cells("Vendor").Value)
                End If
                'lstQuerries.Clear()
            End With
        Next
        Return True
    End Function
    Private Function ImportAirBill(strAopClass As String) As Boolean
        'dang lam do dang
        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                If .Cells("VendorAopId").Value = "" Then
                    MsgBox("Missing Vendor for " & .Cells("RefNumber").Value)
                    Return False
                End If

            End With
        Next
        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow

                If .Cells("SRV").Value = "R" Then
                    If Not ImportVendorCredit(.Cells("VendorAopId").Value, .Cells("CustAopId").Value, strAopClass, "VENDOR PAYABLE (VND)" _
                                  , .Cells("TrxDate").Value, .Cells("RefNumber").Value, "COST", .Cells("BillAmt").Value, 0, .Cells("TKNO").Value) Then
                        Continue For
                    End If

                    If strAopClass = "FTK" AndAlso .Cells("GRP").Value = "GRP" Then
                        If Not ImportCreditMemo(.Cells("CustAOPid").Value, "FTK", "CUSTOMER RECEIVABLE (VND)", .Cells("TrxDate").Value _
                                                      , .Cells("RefNumber").Value, .Cells("Tkno").Value, .Cells("BillAmt").Value, 0, .Cells("Tkno").Value _
                                                      & " " & .Cells("TourCode").Value) Then
                            Continue For
                        End If
                    End If
                Else
                    Dim strDueDate As String = String.Empty
                    Select Case .Cells("Vendor").Value
                        Case "VN", "BSP", "VN DEB"
                            If .Cells("DocType").Value.ToString.Contains("ETK") Or .Cells("DocType").Value.ToString.Contains("EMD") _
                                Or .Cells("DocType").Value.ToString.Contains("MCO") Then
                                If .Cells("Vendor").Value = "BSP" Then
                                    strDueDate = GetDueDate4AopBsp(.Cells("DOI").Value)
                                Else
                                    strDueDate = GetDueDate4AopNonBsp(.Cells("DOI").Value)
                                End If
                            End If
                    End Select

                    If Not ImportBill(.Cells("CustAopId").Value, .Cells("VendorAopId").Value, strAopClass, .Cells("BillAmt").Value _
                                  , .Cells("TrxDate").Value, .Cells("RefNumber").Value, "COST", .Cells("TKNO").Value, strDueDate) Then
                        Continue For
                    End If
                End If

            End With
        Next
        Return True
    End Function
    Private Function ImportBill(strCustAopId As String, strVendorAopId As String, strClass As String, decAmount As Decimal _
                                , strTrxDate As String, StrRefNumber As String, strItemLineReferenceName As String _
                                , strMemo As String, Optional strDueDate As String = "") As Boolean


        If strDueDate = "" Then
            If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...BillItemLine (ItemLineBillableStatus, ItemLineCustomerRefListID" _
                & ",VendorRefListID,ItemLineClassRefFullName,APAccountRefFullName" _
                & ",TxnDate,RefNumber,ItemLineItemRefFullName, ItemLineDesc" _
                & ",ItemLineAmount,memo,FQSaveToCache) VALUES ('Billable','" & strCustAopId & "','" & strVendorAopId _
                & "','" & strClass & "','VENDOR PAYABLE (VND)','" _
                & strTrxDate & "','" & StrRefNumber & "','" & strItemLineReferenceName & "','" & strMemo _
                & "'," & decAmount & ",'" & strMemo & "',1)", ConnAOP) Then
                MsgBox("Unable to insert BillItemLine " & StrRefNumber & " - " & strItemLineReferenceName)
                Return False
            End If

        ElseIf Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...BillItemLine (ItemLineBillableStatus, ItemLineCustomerRefListID" _
                & ",VendorRefListID,ItemLineClassRefFullName,APAccountRefFullName" _
                & ",TxnDate,RefNumber,ItemLineItemRefFullName, ItemLineDesc" _
                & ",ItemLineAmount,memo,DueDate,FQSaveToCache) VALUES ('Billable','" & strCustAopId & "','" & strVendorAopId _
                & "','" & strClass & "','VENDOR PAYABLE (VND)','" _
                & strTrxDate & "','" & StrRefNumber & "','" & strItemLineReferenceName & "','" & strMemo _
                & "'," & decAmount & ",'" & strMemo & "','" & strDueDate & "',1)", ConnAOP) Then
            MsgBox("Unable to insert BillItemLine " & StrRefNumber & " - " & strItemLineReferenceName)
            Return False
        End If


        If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...Bill (VendorRefListID, APAccountRefFullName, TxnDate, RefNumber" _
                & ", Memo) VALUES ('" & strVendorAopId & "','VENDOR PAYABLE (VND)','" _
                & strTrxDate & "','" & StrRefNumber & "','" & strMemo & "')", ConnAOP) Then
                    MsgBox("Unable to insert Bill " & StrRefNumber & " - " & strItemLineReferenceName)
                    Return False
                End If
        'lstQuerries.Clear()

        Return True
    End Function
    Private Function ImportNonAirInvoice() As Boolean
        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & .Cells("AOPListid").Value & "','CTS','CUSTOMER RECEIVABLE (VND)','" _
                & .Cells("InvDate").Value & "','" & .Cells("RefNumber").Value & "','" & .Cells("Tcode").Value & "','" & .Cells("Tcode").Value _
                & "'," & .Cells("InvAmt").Value & ",'" & .Cells("RefNumber").Value & "',1)", ConnAOP) Then
                    MsgBox("Unable to insert Invoice Line for " & .Cells("RefNumber").Value)
                End If

                If .Cells("MerchantFee").Value <> 0 AndAlso Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & .Cells("AOPListid").Value & "','CTS','CUSTOMER RECEIVABLE (VND)','" _
                & .Cells("InvDate").Value & "','" & .Cells("RefNumber").Value & "','" & .Cells("Tcode").Value & "','" & .Cells("Tcode").Value _
                & "'," & 0 - .Cells("MerchantFee").Value & ",'" & .Cells("RefNumber").Value & "',1)", ConnAOP) Then
                    MsgBox("Unable to insert Invoice Line for Merchant Fee for " & .Cells("RefNumber").Value)
                End If

                If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...Invoice (CustomerRefListID,ClassRefFullName, ARAccountRefFullName, TxnDate, RefNumber" _
                & ", Memo) VALUES ('" & .Cells("AOPListid").Value & "','CTS','CUSTOMER RECEIVABLE (VND)','" _
                & .Cells("InvDate").Value & "','" & .Cells("RefNumber").Value & "','" & .Cells("Tcode").Value & "')", ConnAOP) Then
                    MsgBox("Unable to insert Invoice  for " & .Cells("RefNumber").Value)
                End If
                'lstQuerries.Clear()
            End With
        Next
        Return True
    End Function
    Private Function ImportDeposit(strAOPListid As String, strClass As String, strAccountName As String, strInvDate As String _
                                   , strRefNumber As String, decInvAmt As Decimal _
                                   , strMemo As String) As Boolean
        If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...DepositLine (DepositLineEntityRefListID,DepositLineClassRefFullName,DepositToAccountRefFullName" _
                        & ",DepositLineAccountRefFullName,DepositLinePaymentMethodRefFullName,TxnDate,DepositLineCheckNumber" _
                        & ",DepositLineAmount,DepositLineMemo,Memo,FQSaveToCache) VALUES ('" & strAOPListid & "','" & strClass & "','" & strAccountName _
                        & "','CUSTOMER RECEIVABLE (VND)','Cash','" _
                        & strInvDate & "','" & strRefNumber & "'," & decInvAmt & ",'" & strMemo & "','" & strMemo & "',1)", ConnAOP) Then
            MsgBox("Unable to insert Deposit Line for " & strRefNumber)
            Return False
        End If

        If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...Deposit ( DepositToAccountRefFullName, TxnDate" _
                            & ", Memo) VALUES ('" & strAccountName & "','" _
                            & strInvDate & "','" & strMemo & "')", ConnAOP) Then
            MsgBox("Unable to insert Deposit for " & strRefNumber)
            Return False
        End If
        Return True
    End Function
    Private Function ImportInvoice(strAOPListid As String, strClass As String, strAccountName As String, strInvDate As String _
                                   , strRefNumber As String, strDesc As String, decInvAmt As Decimal, decMerchantFee As Decimal _
                                   , strMemo As String) As Boolean
        If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                        & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                        & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & strAOPListid & "','" & strClass & "','" & strAccountName & "','" _
                        & strInvDate & "','" & strRefNumber & "','REVENUE','" & strDesc _
                        & "'," & decInvAmt & ",'" & strMemo & "',1)", ConnAOP) Then
            MsgBox("Unable to insert Invoice Line for " & strRefNumber)
            Return False
        End If

        If decMerchantFee <> 0 AndAlso Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                        & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                        & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & strAOPListid & "','" & strClass & "','" & strAccountName & "','" _
                        & strInvDate & "','" & strRefNumber & "','REVENUE','" & strDesc _
                        & "'," & 0 - decMerchantFee & ",'" & strMemo & "',1)", ConnAOP) Then
            MsgBox("Unable to insert Invoice Line for Merchant Fee for " & strRefNumber)
            Return False
        End If

        If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...Invoice (CustomerRefListID,ClassRefFullName, ARAccountRefFullName, TxnDate, RefNumber" _
                            & ", Memo) VALUES ('" & strAOPListid & "','" & strClass & "','" & strAccountName & "','" _
                            & strInvDate & "','" & strRefNumber & "','" & strMemo & "')", ConnAOP) Then
            MsgBox("Unable to insert Invoice for " & strRefNumber)
            Return False
        End If
        Return True
    End Function
    Private Function ImportVendorCredit(strVendorAOPListid As String, strCustAopId As String, strClass As String _
                                    , strAccountName As String, strInvDate As String _
                                   , strRefNumber As String, strItemName As String, decInvAmt As Decimal, decMerchantFee As Decimal _
                                   , strMemo As String) As Boolean
        If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...VendorCreditItemLine (VendorRefListID,APAccountRefFullName" _
                        & ",TxnDate,RefNumber,Memo,ItemLineItemRefFullName,ItemLineDesc" _
                        & ",ItemLineAmount,ItemLineCustomerRefListID,ItemLineClassRefFullName,ItemLineBillableStatus,FQSaveToCache) VALUES ('" _
                        & strVendorAOPListid & "','" & strAccountName & "','" & strInvDate & "','" & strRefNumber & "','" & strMemo _
                        & "','" & strItemName & "','" & strMemo _
                        & "'," & decInvAmt & ",'" & strCustAopId & "','" & strClass & "','Billable',1)", ConnAOP) Then
            MsgBox("Unable to insert VendorCredit Line for " & strRefNumber)
            Return False
        End If


        If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...VendorCredit (VendorRefListID,APAccountRefFullName, TxnDate, RefNumber" _
                            & ", Memo) VALUES ('" & strVendorAOPListid & "','" & strAccountName & "','" _
                            & strInvDate & "','" & strRefNumber & "','" & strMemo & "')", ConnAOP) Then
            MsgBox("Unable to insert VendorCredit for " & strRefNumber)
            Return False
        End If
        Return True
    End Function
    Private Function ImportCreditMemo(strAOPListid As String, strClass As String, strAccountName As String, strInvDate As String _
                                   , strRefNumber As String, strDesc As String, decInvAmt As Decimal, decMerchantFee As Decimal _
                                   , strMemo As String) As Boolean
        If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...CreditMemoLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                        & ",TxnDate,RefNumber,CreditMemoLineItemRefFullName, CreditMemoLineDesc" _
                        & ",CreditMemoLineAmount,memo,FQSaveToCache) VALUES ('" & strAOPListid & "','" & strClass & "','" & strAccountName & "','" _
                        & strInvDate & "','" & strRefNumber & "','REVENUE','" & strDesc _
                        & "'," & decInvAmt & ",'" & strMemo & "',1)", ConnAOP) Then
            MsgBox("Unable to insert CreditMemo Line for " & strRefNumber)
            Return False
        End If

        If decMerchantFee <> 0 AndAlso Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                            & ",TxnDate,RefNumber,CreditMemoLineItemRefFullName, CreditMemoLineDesc" _
                            & ",CreditMemoLineAmount,memo,FQSaveToCache) VALUES ('" & strAOPListid & "','" & strClass & "','" & strAccountName & "','" _
                        & strInvDate & "','" & strRefNumber & "','REVENUE','" & strDesc _
                        & "'," & 0 - decMerchantFee & ",'" & strMemo & "',1)", ConnAOP) Then
            MsgBox("Unable to insert CreditMemo Line for Merchant Fee for " & strRefNumber)
            Return False
        End If

        If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...CreditMemo (CustomerRefListID,ClassRefFullName, ARAccountRefFullName, TxnDate, RefNumber" _
                            & ", Memo) VALUES ('" & strAOPListid & "','" & strClass & "','" & strAccountName & "','" _
                            & strInvDate & "','" & strRefNumber & "','" & strMemo & "')", ConnAOP) Then
            MsgBox("Unable to insert CreditMemo for " & strRefNumber)
            Return False
        End If
        Return True
    End Function
    Private Function CheckImportAirTVS() As Boolean
        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                Select Case .Cells("AopRecord").Value
                    Case "Deposit"
                        If .Cells("Account").Value = "" Then
                            MsgBox("You must update account for " & .Cells("RcpNo").Value)
                            Return False
                        End If
                End Select
            End With
        Next
        Return True
    End Function
    Private Function ImportAirInvoiceTVS() As Boolean
        If Not CheckImportAirTVS() Then Return False
        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                Select Case .Cells("GRP").Value
                    Case "GRP"
                        Select Case .Cells("CustShortName").Value
                            Case "TV_SGN", "GDS_SGN"
                                If .Cells("SRV").Value = "S" Then
                                    If Not ImportInvoice(.Cells("AOPListid").Value, "FTK", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                                      , .Cells("RefNumber").Value, Trim(.Cells("Tkno").Value & " " & .Cells("TourCode").Value) _
                                                      , .Cells("InvAmt").Value, 0, .Cells("Tkno").Value _
                                                      & " " & .Cells("TourCode").Value) Then
                                        Continue For
                                    End If
                                    If .Cells("TspVendor").Value <> "" Then
                                        If Not ImportBill(.Cells("TspCustAopId").Value, .Cells("TspVendorAopId").Value, .Cells("TspClass").Value, .Cells("InvAmt").Value _
                                              , .Cells("InvDate").Value, .Cells("RefNumber").Value, .Cells("TourCode").Value _
                                              , .Cells("Tkno").Value) Then
                                            Continue For
                                        End If
                                    End If
                                Else
                                    If Not ImportCreditMemo(.Cells("AOPListid").Value, "FTK", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                                      , .Cells("RefNumber").Value, .Cells("Tkno").Value, .Cells("InvAmt").Value, 0, .Cells("Tkno").Value _
                                                      & " " & .Cells("TourCode").Value) Then
                                        Continue For
                                    End If
                                    If .Cells("TspVendor").Value <> "" Then
                                        If Not ImportVendorCredit(.Cells("TspVendorAopId").Value, .Cells("TspCustAopId").Value, .Cells("TspClass").Value _
                                                                  , "VENDOR PAYABLE (VND)", .Cells("InvDate").Value, .Cells("RefNumber").Value _
                                                                , .Cells("TourCode").Value, .Cells("InvAmt").Value, 0, .Cells("Tkno").Value) Then
                                            Continue For
                                        End If
                                    End If
                                End If
                            Case Else
                                Dim strMemo As String = .Cells("Tkno").Value & " " & .Cells("DepDate").Value
                                If .Cells("CustShortName").Value = "TVHAN" Then
                                    strMemo = strMemo & " " & .Cells("TourCode").Value
                                End If
                                If .Cells("SRV").Value = "S" Then
                                    If Not ImportDeposit(.Cells("AOPListid").Value, "FTK", .Cells("Account").Value, .Cells("InvDate").Value _
                                                      , .Cells("RefNumber").Value, .Cells("InvAmt").Value, strMemo) Then
                                        Continue For
                                    End If
                                Else
                                    If Not ImportCreditMemo(.Cells("AOPListid").Value, "FTK", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                                      , .Cells("RefNumber").Value, strMemo, .Cells("InvAmt").Value, 0, strMemo) Then
                                        Continue For
                                    End If
                                End If
                        End Select
                    Case "FIT"
                        If .Cells("CustShortName").Value = "VYM" Then
                            Dim strMemo As String = .Cells("Tkno").Value & " (" & Format(.Cells("InvAmt").Value, "#,##0") & "/" & Format(.Cells("ROE").Value, "#,##0") & ")"
                            Dim decInvAmt As Decimal = Math.Round(.Cells("InvAmt").Value / .Cells("ROE").Value, 2)
                            If .Cells("SRV").Value = "S" Then
                                If Not ImportInvoice(.Cells("AOPListid").Value, "FTK", "CUSTOMER RECEIVABLE (USD)", .Cells("InvDate").Value _
                                          , .Cells("RefNumber").Value, strMemo, decInvAmt, 0, strMemo) Then
                                    Continue For
                                End If
                            Else
                                If Not ImportCreditMemo(.Cells("AOPListid").Value, "FTK", "CUSTOMER RECEIVABLE (USD)", .Cells("InvDate").Value _
                                          , .Cells("RefNumber").Value, .Cells("Tkno").Value, decInvAmt, 0, .Cells("Tkno").Value) Then
                                    Continue For
                                End If
                            End If
                        Else
                            If .Cells("SRV").Value = "S" Then
                                If Not ImportInvoice(.Cells("AOPListid").Value, "FTK", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                          , .Cells("RefNumber").Value, .Cells("Tkno").Value, .Cells("InvAmt").Value, 0, .Cells("Tkno").Value) Then
                                    Continue For
                                End If

                                If .Cells("OriDeposits").Value <> "" Then
                                    Dim strCreditCustId As String = .Cells("AOPListid").Value
                                    Select Case .Cells("CustShortName").Value
                                        Case "TVSGN"
                                            strCreditCustId = "8000017B-1579073162"

                                        Case "GDSSGN"
                                            strCreditCustId = "8000017A-1579073000"
                                    End Select
                                    Dim arrOriDeposits As String() = Split(.Cells("OriDeposits").Value, "|")
                                    For Each strOriDeposit As String In arrOriDeposits
                                        Dim arrOriInfo As String() = strOriDeposit.Split("/")
                                        If Not ImportCreditMemo(strCreditCustId, "FTK", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                          , .Cells("RefNumber").Value, .Cells("Tkno").Value & " " & .Cells("RcpNo").Value, arrOriInfo(1) _
                                          , 0, .Cells("Tkno").Value & " " & .Cells("RcpNo").Value) Then
                                            Continue For
                                        End If
                                    Next
                                End If

                                'Tao Bill cho TSP khi cust short name TVSGN, GDSSGN
                                Select Case .Cells("CustShortName").Value
                                    Case "TVSGN", "GDSSGN"
                                        If Not ImportBill(.Cells("TspCustAopid").Value, .Cells("TspVendorAopid").Value, .Cells("TspClass").Value _
                                                  , .Cells("InvAmt").Value, .Cells("InvDate").Value, .Cells("RefNumber").Value, .Cells("TourCode").Value, .Cells("Tkno").Value) Then

                                        End If
                                End Select
                            Else
                                If Not ImportCreditMemo(.Cells("AOPListid").Value, "FTK", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                      , .Cells("RefNumber").Value, .Cells("Tkno").Value, .Cells("InvAmt").Value, 0, .Cells("Tkno").Value) Then
                                    Continue For
                                End If
                                'Tao Bill Credit cho TSP khi cust short name TVSGN, GDSSGN
                                Select Case .Cells("CustShortName").Value
                                    Case "TVSGN", "GDSSGN"
                                        If Not ImportVendorCredit(.Cells("TspVendorAopId").Value, .Cells("TspCustAopId").Value, .Cells("TspClass").Value _
                                                                  , "VENDOR PAYABLE (VND)", .Cells("InvDate").Value, .Cells("RefNumber").Value _
                                                                , .Cells("TourCode").Value, .Cells("InvAmt").Value, 0, .Cells("Tkno").Value) Then
                                            Continue For
                                        End If
                                End Select
                            End If
                        End If


                End Select


            End With
        Next
        Return True
    End Function
    Private Function ImportAirInvoiceCTS() As Boolean

        For Each objRow As DataGridViewRow In dgrRasData.Rows
            With objRow
                If .Cells("InvCount").Value = 1 Then
                    If .Cells("SRV").Value = "S" Then
                        If Not ImportInvoice(.Cells("AOPListid").Value, "CTS", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                      , .Cells("RefNumber").Value, .Cells("Tkno").Value, .Cells("InvAmt").Value, .Cells("MerchantFee").Value, .Cells("Tkno").Value) Then
                            Continue For
                        End If
                    Else
                        If Not ImportCreditMemo(.Cells("AOPListid").Value, "CTS", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                      , .Cells("RefNumber").Value, .Cells("Tkno").Value, .Cells("InvAmt").Value, .Cells("MerchantFee").Value, .Cells("Tkno").Value) Then
                            Continue For
                        End If
                    End If
                ElseIf .Cells("InvCount").Value = 2 Then
                    If .Cells("SRV").Value = "S" Then
                        Dim decMerchantFee As Decimal
                        If Not ImportInvoice(.Cells("AOPListid").Value, "CTS", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                      , .Cells("RefNumber").Value, .Cells("Tkno").Value, .Cells("InvAmt").Value, .Cells("MerchantFee").Value, .Cells("Tkno").Value) Then
                            Continue For
                        End If
                        Select Case .Cells("CustShortName").Value
                            Case "PG VIETNAM", "PG INDOCHINA"
                                decMerchantFee = 0
                            Case Else
                                decMerchantFee = .Cells("MerchantFee").Value
                        End Select
                        If Not ImportInvoice(.Cells("AOPListid").Value, "CTS", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                      , .Cells("RefNumber").Value, .Cells("Tkno").Value, .Cells("SvcFee").Value, decMerchantFee, .Cells("Tkno").Value) Then
                            Continue For
                        End If
                    Else
                        If Not ImportCreditMemo(.Cells("AOPListid").Value, "CTS", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                      , .Cells("RefNumber").Value, .Cells("Tkno").Value, .Cells("InvAmt").Value, .Cells("MerchantFee").Value, .Cells("Tkno").Value) Then
                            Continue For
                        End If
                        If Not ImportCreditMemo(.Cells("AOPListid").Value, "CTS", "CUSTOMER RECEIVABLE (VND)", .Cells("InvDate").Value _
                                      , .Cells("RefNumber").Value, .Cells("Tkno").Value, .Cells("SvcFee").Value, 0, .Cells("Tkno").Value) Then
                            Continue For
                        End If
                    End If

                End If
                'If .Cells("SRV").Value = "S" Then
                '    If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                '        & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                '        & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & .Cells("AOPListid").Value & "','CTS','CUSTOMER RECEIVABLE (VND)','" _
                '        & .Cells("InvDate").Value & "','" & .Cells("RefNumber").Value & "','REVENUE','" & .Cells("Tkno").Value _
                '        & "'," & .Cells("InvAmt").Value & ",'" & .Cells("Tkno").Value & "',1)", ConnAOP) Then
                '        MsgBox("Unable to insert Invoice Line")
                '    End If

                '    If .Cells("MerchantFee").Value <> 0 AndAlso Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                '            & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                '            & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & .Cells("AOPListid").Value & "','CTS','CUSTOMER RECEIVABLE (VND)','" _
                '            & .Cells("InvDate").Value & "','" & .Cells("RefNumber").Value & "','REVENUE','" & .Cells("Tkno").Value _
                '            & "'," & 0 - .Cells("MerchantFee").Value & ",'" & .Cells("Tkno").Value & "',1)", ConnAOP) Then
                '        MsgBox("Unable to insert Invoice Line for Merchant Fee")
                '    End If

                '    If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...Invoice (CustomerRefListID,ClassRefFullName, ARAccountRefFullName, TxnDate, RefNumber" _
                '            & ", Memo) VALUES ('" & .Cells("AOPListid").Value & "','CTS','CUSTOMER RECEIVABLE (VND)','" _
                '            & .Cells("InvDate").Value & "','" & .Cells("RefNumber").Value & "','" & .Cells("Tkno").Value & "')", ConnAOP) Then
                '        MsgBox("Unable to insert Invoice ")
                '    End If
                'Else
                '    If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...CreditMemoLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                '        & ",TxnDate,RefNumber,CreditMemoLineItemRefFullName, CreditMemoLineDesc" _
                '        & ",CreditMemoLineAmount,memo,FQSaveToCache) VALUES ('" & .Cells("AOPListid").Value & "','CTS','CUSTOMER RECEIVABLE (VND)','" _
                '        & .Cells("InvDate").Value & "','" & .Cells("RefNumber").Value & "','REVENUE','" & .Cells("Tkno").Value _
                '        & "'," & .Cells("InvAmt").Value & ",'" & .Cells("Tkno").Value & "',1)", ConnAOP) Then
                '        MsgBox("Unable to insert CreditMemo Line")
                '    End If

                '    If .Cells("MerchantFee").Value <> 0 AndAlso Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                '            & ",TxnDate,RefNumber,CreditMemoLineItemRefFullName, CreditMemoLineDesc" _
                '            & ",CreditMemoLineAmount,memo,FQSaveToCache) VALUES ('" & .Cells("AOPListid").Value & "','CTS','CUSTOMER RECEIVABLE (VND)','" _
                '            & .Cells("InvDate").Value & "','" & .Cells("RefNumber").Value & "','REVENUE','" & .Cells("Tkno").Value _
                '            & "'," & 0 - .Cells("MerchantFee").Value & ",'" & .Cells("Tkno").Value & "',1)", ConnAOP) Then
                '        MsgBox("Unable to insert CreditMemo Line for Merchant Fee")
                '    End If

                '    If Not ExecuteNonQuerry("INSERT INTO AOP_SGN_TVTR...CreditMemo (CustomerRefListID,ClassRefFullName, ARAccountRefFullName, TxnDate, RefNumber" _
                '            & ", Memo) VALUES ('" & .Cells("AOPListid").Value & "','CTS','CUSTOMER RECEIVABLE (VND)','" _
                '            & .Cells("InvDate").Value & "','" & .Cells("RefNumber").Value & "','" & .Cells("Tkno").Value & "')", ConnAOP) Then
                '        MsgBox("Unable to insert CreditMemo ")
                '    End If
                'End If

                'lstQuerries.Clear()
            End With
        Next
        Return True
    End Function

    Private Sub lbkGetBillData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkGetBillData.LinkClicked
        Select Case cboReportType.Text
            Case "NonAirCWT"
                LoadNonAirBill(dtpSelectedDate.Value, False)
            Case "NonAirLocal"
                LoadNonAirBill(dtpSelectedDate.Value, True)
            Case "Air CTS"
                LoadAirCTSBill(dtpSelectedDate.Value)
            Case "Air TVS"
                LoadAirTVSBill(dtpSelectedDate.Value)
        End Select
    End Sub

    Private Sub lbkAddAccount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddAccount.LinkClicked
        dgrRasData.CurrentRow.Cells("Account").Value = cboAccount.SelectedValue
        dgrRasData.CurrentRow.Cells("AccountName").Value = cboAccount.Text
    End Sub

    Private Sub dgrRasData_SelectionChanged(sender As Object, e As EventArgs) Handles dgrRasData.SelectionChanged
        If dgrRasData.RowCount = 0 Then Exit Sub
        With dgrRasData.CurrentRow
            If dgrRasData.Columns.Contains("AopRecord") Then
                cboAccount.Visible = (.Cells("AopRecord").Value = "Deposit")
                lbkAddAccount.Visible = (.Cells("AopRecord").Value = "Deposit")
            Else
                cboAccount.Visible = False
                lbkAddAccount.Visible = False
            End If


        End With
    End Sub
End Class