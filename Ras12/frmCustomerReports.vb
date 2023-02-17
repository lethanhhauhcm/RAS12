Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class frmCustomerReports
    Dim mobjExcel As New Excel.Application
    Dim mobjWbk As Workbook
    Private mstrCompanyName As String

    Private Sub frmNonMonthlyReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboReport.SelectedIndex = 0
        
    End Sub

    Private Sub blkOK_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles blkGetReport.LinkClicked
        Dim ofdReport As New OpenFileDialog
        Dim arrTemplateParts As String()

        If cboReport.Text = "Visa Monthly CTA" Then
            If RunVisaMonthlyCta() Then
                MsgBox("Completed")
                Me.Dispose()
            End If
            Exit Sub
        ElseIf cboReport.Text = "L Oreal Yearly Air" Then
            If RunLOrealYearlyAir() Then
                MsgBox("Completed")
                Me.Dispose()
            End If
            Exit Sub
        ElseIf cboReport.Text = "Abbott Weekly Control" Then
            If RunAbbottWeeklyControl() Then
                MsgBox("Completed")
                Me.Dispose()
                Exit Sub
            End If
        End If

        With ofdReport
            .InitialDirectory = "W:\CTS\SALE CTS - NEW\6.TMC\CWT"
            arrTemplateParts = cboReport.Text.Split(" ")
            If System.IO.Directory.Exists(.InitialDirectory & "\" & arrTemplateParts(0)) Then
                .InitialDirectory = .InitialDirectory & "\" & arrTemplateParts(0)
            End If
            If System.IO.Directory.Exists(.InitialDirectory & "\4.REPORT") Then
                .InitialDirectory = .InitialDirectory & "\4.REPORT"
            End If

            If System.IO.Directory.Exists(.InitialDirectory & "\" & Year(dtpFromDate.Value)) Then
                .InitialDirectory = .InitialDirectory & "\" & Year(dtpFromDate.Value)
            End If

            .Filter = "XLSX files|*.xlsx|XLS Binary files |*.xlsb|XLS template |*.xlt"

            If .ShowDialog = DialogResult.OK Then
                If .FileName.Contains("EY Settlement Summary Q") Then
                    mobjExcel.Visible = True
                    mobjWbk = mobjExcel.Workbooks.Open(.FileName, , , , "VIE15")
                Else
                    mobjWbk = mobjExcel.Workbooks.Open(.FileName)
                End If

                Select Case cboReport.Text
                    

                    Case "BAT REP Monthly Departure"

                    Case "Bayer Financial HY"
                        If RunBayerFinancialHY() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "BP Monthly Settlement"
                        If RunBPMonthlySettlement() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "Citi Monthly Settlement"
                        If RunCitiMonthlySettlement() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "Chevron Quarterly Settlement"
                        If RunChevronQuarterlySettlement() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "Ericsson Quarterly Settlement"
                        If RunEricssonQuarterlySettlement() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "EY Quarterly Settlement"
                        If RunEyQuarterlySettlement() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "GE Monthly Air Breakdown"
                        If RunGeMontlyAirBreakdown() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "GE Monthly ScoreCard"
                        If RunGeMontlyScorecard() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "Halliburton Monthly CPT"
                        If RunHalliburtonMontlyCPT() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "HPI Monthly"
                        If RunHpiMonthly() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "Henkel POS Invoicing HY"
                        If HenkelPOSInvoicingHY() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "IFAD Fortnightly"
                        If IFADFortnightly() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "JPMC Quarterly Settlement"
                        If RunJpmcQuarterlySettlement() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "Walmart Monthly Settlement"
                        If RunWalmartMonthlySettlement() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "Oracle Monthly Settlement"
                        If RunOracleMonthlySettlement() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                    Case "Visa Monthly CTA"
                        If RunVisaMonthlyCta() Then
                            MsgBox("Completed")
                            Me.Dispose()
                        End If
                End Select
            End If
        End With
    End Sub
    Private Function RunAbbottWeeklyControl() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim strFromDate As String = CreateFromDate(dtpFromDate.Value)
        Dim strToDate As String = CreateToDate(dtpToDate.Value)

        Dim strTargetSheetName As String = "AIR"
        Dim strQuerryAir As String
        Dim strQuerryNonAir As String

        Dim tblResult As System.Data.DataTable

        mobjExcel.Visible = True
        mobjExcel.Workbooks.Add()
        mobjWbk = mobjExcel.ActiveWorkbook
        objWsh = mobjWbk.Sheets("Sheet1")
        'For Each objSheet As Worksheet In mobjWbk.Sheets
        '    If objSheet.Name = strTargetSheetName Then
        '        objWsh = objSheet
        '        blnSheetFound = True
        '        Exit For
        '    End If
        'Next
        'If Not blnSheetFound Then
        '    MsgBox("Unable to find Relevant sheet")
        '    Return False
        'End If
        strQuerryAir = "Select ref2 as UPI,PaxName,tr.Ref5 as Division,RequiredData as Department" _
            & ",Itinerary,RequiredData as Approver,DOF as StartDate , a.ArrDates as EndDate,'Air' as MeanOfTransport,'TransViet' as VendorName,RcpNo" _
            & " from ras12.dbo.tkt t" _
            & " left join cwt.dbo.GO_Air a on t.RecID=a.Tkid" _
            & " left join cwt.dbo.GO_Travel tr on a.TravelID=tr.RecID" _
            & " where t.Qty=1 and t.Status<>'xx' and tr.Status='ok' and t.DocType='ETK'" _
            & " and tr.CMC='1480' " _
            & " and t.Doi between '" & strFromDate & "' and '" & strToDate & "'"

        strQuerryNonAir = "Select s3.FValue as UPI,Traveller as PaxName,s.FValue as Division,s1.FValue as Department" _
            & ",i.Brief as Itinerary, s2.FValue as Approver,t.SDate as StartDate,cast(t.EDate as varchar) as EndDate" _
            & ",'Car' as MeanOfTransport,'TransViet' as VendorName,Tcode" _
            & " from RAS12.dbo.DuToan_Item i" _
            & " left join RAS12.dbo.DuToan_Tour t on i.DuToanID=t.RecID" _
            & " left join RAS12.dbo.SIR s on s.RCPID=t.RecID and s.Prod='NonAir' and s.Status='ok' and s.Fname='DIVISION'" _
            & " left join RAS12.dbo.SIR s1 on s1.RCPID=t.RecID and s1.Prod='NonAir' and s1.Status='ok' and s1.Fname='DEPARTMENT'" _
            & " left join RAS12.dbo.SIR s2 on s2.RCPID=t.RecID and s2.Prod='NonAir' and s2.Status='ok' and s2.Fname='APPROVER'" _
            & " left join RAS12.dbo.SIR s3 on s3.RCPID=t.RecID and s3.Prod='NonAir' and s3.Status='ok' and s3.Fname='UPI'" _
            & " where i.Status='ok' and i.Service ='Transfer'" _
            & " and t.Status='RR' and t.LstUpdate between '" & strFromDate & "' and '" & strToDate & "'" _
            & " and t.CustID in (select IntVal from ras12.dbo.MISC where cat='custnameingroup' and VAL ='ABBOTT CWT')"

        tblResult = pobjTvcs.GetDataTable(strQuerryAir & " union " & strQuerryNonAir)
        For Each objRow As DataRow In tblResult.Rows
            Select Case objRow("MeanOfTransport")
                Case "Air"
                    objRow("Department") = GetRequiredDataByDataCode(objRow("Department"), "UDID30")
                    objRow("Approver") = GetRequiredDataByDataCode(objRow("Approver"), "UDID32")
                    objRow("EndDate") = AddFutureYear(objRow("StartDate"), Strings.Right(objRow("EndDate"), 5))
                Case "Car"
                    objRow("Itinerary") = Split(objRow("Itinerary"), "|")(1)
                    objRow("EndDate") = CDate(objRow("EndDate"))
            End Select
            
        Next
        Dim lstDateColumns As New List(Of String)
        lstDateColumns.Add("G")
        lstDateColumns.Add("H")
        Table2ExcelCts(tblResult, "AbbottWeeklyControl" & Format(Now, "yyMMdd"), , lstDateColumns)

        'objWsh.Activate()

        Return True
    End Function
    Private Function RunBayerFinancialHY() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim dteFromDate As Date = dtpFromDate.Value
        Dim strTargetSheetName As String = "Vietnam"
        'Dim i As Integer
        Dim intRoe As Integer = 1

        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = strTargetSheetName Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        With objWsh

            .Range("B3").Value = Format(dteFromDate, "MMM").ToUpper _
                & " - " & Format(dtpToDate.Value, "MMM yyyy").ToUpper

            Dim objRevDomTkt As clsSum
            Dim objRevRegTkt As clsSum
            Dim objRevIntlTkt As clsSum
            Dim objSvcFeeAhc As clsSum
            Dim objSvcFeeLcc As clsSum

            objRevDomTkt = SumRev4Tkt("415", "DOM", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevRegTkt = SumRev4Tkt("415", "REG", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevIntlTkt = SumRev4Tkt("415", "INTL", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objSvcFeeAhc = SumSvcFee4Ahc("415", dteFromDate, dtpToDate.Value, intRoe)
            objSvcFeeLcc = SumSvcFee4Tkt("415", "", dteFromDate, dtpToDate.Value, "S", intRoe, "LCC")

            .Range("C19").Value = objRevDomTkt.Nbr
            .Range("C21").Value = objRevIntlTkt.Nbr
            .Range("C20").Value = objRevRegTkt.Nbr
            .Range("C10").Value = objRevDomTkt.Amt + objRevRegTkt.Amt + objRevIntlTkt.Amt
            .Range("C14").Value = objSvcFeeAhc.Amt
            .Range("C25").Value = objSvcFeeAhc.Nbr

            .Range("C74").Value = .Range("B74").Value * objSvcFeeAhc.Nbr
            .Range("C78").Value = .Range("B78").Value * objSvcFeeLcc.Nbr

        End With
        
        Return True
    End Function
    Private Function RunBPMonthlySettlement() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim dteFromDate As Date = dtpFromDate.Value
        Dim strTargetSheetName As String = "Input File"
        'Dim i As Integer
        Dim intRoe As Integer = 23000

        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = strTargetSheetName Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        With objWsh

            .Range("A7").Value = Format(dtpToDate.Value, "MMM,yyyy").ToUpper

            Dim objRevDomTkt As clsSum
            Dim objRevDomTktSales As clsSum
            Dim objRevRegTkt As clsSum
            Dim objRevIntlTkt As clsSum
            Dim objRevHotel As clsSum
            Dim objRevCar As clsSum
            Dim objRevVisa As clsSum

            Dim objSvcFeeAhc As clsSum

            'Dim objSvcFeeDomTkt As clsSum
            Dim objSvcFeeRegTkt As clsSum
            Dim objSvcFeeIntlTkt As clsSum

            Dim objRevNoSfCar As clsSum

            objRevDomTkt = SumRev4Tkt("39", "DOM", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevDomTktSales = SumRev4Tkt("39", "DOM", dteFromDate, dtpToDate.Value, "S", intRoe, "")
            objRevRegTkt = SumRev4Tkt("39", "REG", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevIntlTkt = SumRev4Tkt("39", "INTL", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevHotel = SumRevNoSfNonAirService("39", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Hotel)
            objRevCar = SumRevNoSfNonAirService("39", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Car)
            objRevVisa = SumRevNoSfNonAirService("39", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Visa)

            objSvcFeeAhc = SumSvcFee4Ahc("39", dteFromDate, dtpToDate.Value, intRoe)

            'objSvcFeeDomTkt = SumSvcFee4Tkt("39", "DOM", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objSvcFeeRegTkt = SumSvcFee4Tkt("39", "REG", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objSvcFeeIntlTkt = SumSvcFee4Tkt("39", "INTL", dteFromDate, dtpToDate.Value, "", intRoe, "")

            objRevNoSfCar = SumRevNoSfNonAirService("39", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Car)

            .Range("D15").Value = objRevDomTkt.Amt
            .Range("D16").Value = objRevRegTkt.Amt + objRevIntlTkt.Amt
            .Range("D18").Value = objRevCar.Amt
            .Range("D20").Value = objRevVisa.Amt

            .Range("D33").Value = objRevDomTktSales.Nbr
            .Range("D34").Value = objRevRegTkt.Nbr + objRevIntlTkt.Nbr
            .Range("D38").Value = objRevCar.Nbr
            .Range("D40").Value = objRevVisa.Nbr
            .Range("D76").Value = (objSvcFeeRegTkt.Amt + objSvcFeeIntlTkt.Amt) * 0.9
            .Range("D131").Value = objSvcFeeAhc.Nbr
            .Range("D135").Value = objRevHotel.Nbr
            .Range("D137").Value = objRevVisa.Nbr

        End With

        Return True
    End Function
    Private Function RunCitiMonthlySettlement() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim dteFromDate As Date = CreateFromDate(dtpFromDate.Value)
        Dim strTargetSheetName As String = "Vietnam"
        'Dim i As Integer
        Dim intRoe As Integer = 1

        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = strTargetSheetName Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        With objWsh

            .Range("A3").Value = Format(dtpToDate.Value, "MMM,yyyy").ToUpper

            Dim objSvcFeeDomTkt As clsSum
            Dim objSvcFeeRegTkt As clsSum
            Dim objSvcFeeIntlTkt As clsSum
            Dim objSvcFeeAhc As clsSum
            Dim objRevNoSfCar As clsSum

            objSvcFeeDomTkt = SumSvcFee4Tkt("67", "DOM", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objSvcFeeRegTkt = SumSvcFee4Tkt("67", "REG", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objSvcFeeIntlTkt = SumSvcFee4Tkt("67", "INTL", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objSvcFeeAhc = SumSvcFee4Ahc("67", dteFromDate, dtpToDate.Value, intRoe)
            objRevNoSfCar = SumRevNoSfNonAirService("67", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Car)

            .Range("C11").Value = objSvcFeeDomTkt.Nbr
            .Range("C12").Value = objSvcFeeIntlTkt.Nbr
            .Range("C18").Value = objSvcFeeRegTkt.Nbr

            .Range("C13").Value = objRevNoSfCar.Nbr
            .Range("C52").Value = objSvcFeeAhc.Nbr
        End With
        'Dim arrRtgTypes As String() = {"DOM", "INTL", "REG"}
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name <> strTargetSheetName Then
                objSheet.Range("A1:K1000").ClearContents()
                If objSheet.Name.EndsWith("DOM") Then
                    FillDataAir("67", "DOM", dteFromDate, dtpToDate.Value, objSheet)
                ElseIf objSheet.Name.EndsWith("INTL") Then
                    FillDataAir("67", "INTL", dteFromDate, dtpToDate.Value, objSheet)
                ElseIf objSheet.Name.EndsWith("REG") Then
                    FillDataAir("67", "REG", dteFromDate, dtpToDate.Value, objSheet)
                ElseIf objSheet.Name.EndsWith("Non-Air") Then
                    FillDataNonAir("67", "REG", dteFromDate, dtpToDate.Value, objSheet)
                End If
            End If
        Next
        
        Return True
    End Function
    Private Function RunOracleMonthlySettlement() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim dteFromDate As Date = CreateFromDate(dtpFromDate.Value)
        Dim dteToDate As Date = CreateToDate(dtpToDate.Value)

        Dim strTargetSheetName As String = "Input Template"
        'Dim i As Integer
        Dim intRoe As Integer = 23000

        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            Select Case objSheet.Name
                Case strTargetSheetName
                    objWsh = objSheet
                    blnSheetFound = True
                Case "DOM ticket back up", "INT ticket back up", "non-Air back up"
                    objSheet.Range("A1:Z200").Delete()

            End Select

        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        'dien thong tin chung
        With objWsh
            .Range("A7").Value = Format(dteToDate, "MMMM, yyyy")

            Dim objRevDomTkt As clsSum
            Dim objRevTktRefund As clsSum

            Dim objRevRegTkt As clsSum
            Dim objRevIntlTkt As clsSum
            Dim objRevHotel As clsSum
            Dim objRevCar As clsSum
            Dim objRevVisa As clsSum

            Dim objSvcFeeAhc As clsSum

            Dim objSvcFeeDomTkt As clsSum
            Dim objSvcFeeRegTkt As clsSum
            Dim objSvcFeeIntlTkt As clsSum

            Dim objRevNoSfCar As clsSum

            objRevDomTkt = SumRev4Tkt("174", "DOM", dteFromDate, dteToDate, "", intRoe, "")
            objRevRegTkt = SumRev4Tkt("174", "REG", dteFromDate, dteToDate, "", intRoe, "")
            objRevIntlTkt = SumRev4Tkt("174", "INTL", dteFromDate, dteToDate, "", intRoe, "")

            objRevTktRefund = SumRev4Tkt("174", "", dteFromDate, dteToDate, "R", intRoe, "")


            objRevHotel = SumRevNoSfNonAirService("174", dteFromDate, dteToDate, intRoe, NonAirServices.Hotel)
            objRevCar = SumRevNoSfNonAirService("174", dteFromDate, dteToDate, intRoe, NonAirServices.Car)
            objRevVisa = SumRevNoSfNonAirService("174", dteFromDate, dteToDate, intRoe, NonAirServices.Visa)

            objSvcFeeAhc = SumSvcFee4Ahc("174", dteFromDate, dteToDate, intRoe)

            objSvcFeeDomTkt = SumSvcFee4Tkt("2335", "DOM", dteFromDate, dteToDate, "", intRoe, "")
            objSvcFeeRegTkt = SumSvcFee4Tkt("174", "REG", dteFromDate, dteToDate, "", intRoe, "")
            objSvcFeeIntlTkt = SumSvcFee4Tkt("174", "INTL", dteFromDate, dteToDate, "", intRoe, "")

            'objRevNoSfCar = SumRevNoSfNonAirService("174", dteFromDate, dteToDate, intRoe, NonAirServices.Car)

            '.Range("D12").Value = objRevDomTkt.Amt + objRevRegTkt.Amt + objRevIntlTkt.Amt
            '.Range("D15").Value = objRevDomTkt.Amt
            '.Range("D17").Value = objRevRegTkt.Amt + objRevIntlTkt.Amt

            .Range("D39").Value = objRevDomTkt.Nbr
            .Range("D40").Value = objRevRegTkt.Nbr
            .Range("D41").Value = objRevIntlTkt.Nbr

            .Range("D73").Value = objRevHotel.Nbr
            .Range("D77").Value = objRevTktRefund.Nbr
            .Range("D81").Value = objSvcFeeAhc.Nbr
            .Range("D104").Value = objRevVisa.Amt
        End With

        'dien thong tin Air
        objWsh = mobjWbk.Sheets("Backup file_Air")
        With objWsh
            Dim strQuerry As String = "select t.PaxName,t.RLOC,'','',t.TKNO,t.DOI,a.ALName" _
                                        & ", case r1.DeKho when 'DOM' then 'D' when 'REG' then 'T' ELSE 'I' end" _
                                        & ",t.Itinerary,'Agent Booked'" _
                                        & " from ras12.dbo.tkt t " _
                                        & " left join ras12.dbo.rcp r on t.RCPID=r.RecID" _
                                        & " left join RAS12.dbo.ReportData r1 on t.RecID=r1.TKID" _
                                        & " left join Airline a on a.ALCode=t.AL" _
                                        & " where t.Status<>'xx' and t.Qty<>0" _
                                        & " and r.CustID in (select CustID from cwt.dbo.GO_CompanyInfo1 where CMC='174' and status='ok')" _
                                        & " and t.DOI between '" & CreateFromDate(dteFromDate) _
                                        & "' and '" & CreateToDate(dteToDate) & "' and DocType ='ETK'"
            Dim tblTkts As System.Data.DataTable = pobjTvcs.GetDataTable(strQuerry)
            Dim i As Integer
            For i = 0 To tblTkts.Rows.Count - 1
                objWsh.Range("A" & i + 3 & ":J" & i + 3).Value = tblTkts.Rows(i).ItemArray
            Next


        End With
        Return True
    End Function
    Private Function RunWalmartMonthlySettlement() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean

        Dim strTargetSheetName As String = "Input"
        Dim intRoe As Integer = 1
        Dim dteFromDate As Date = CreateFromDate(dtpFromDate.Value)
        Dim dteToDate As Date = CreateToDate(dtpToDate.Value)

        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = strTargetSheetName Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        With objWsh

            '.Range("E14").Value = Format(dteToDate, "MMM,yyyy").ToUpper

            Dim objRevDomTkt As clsSum
            Dim objRevDomTktSales As clsSum
            Dim objRevRegTkt As clsSum
            Dim objRevIntlTkt As clsSum
            Dim objRevHotel As clsSum
            Dim objRevCar As clsSum
            Dim objRevVisa As clsSum

            Dim objSvcFeeAhc As clsSum

            'Dim objSvcFeeDomTkt As clsSum
            Dim objSvcFeeRegTkt As clsSum
            Dim objSvcFeeIntlTkt As clsSum

            Dim objRevNoSfCar As clsSum

            objRevDomTkt = SumRev4Tkt("2335", "DOM", dteFromDate, dteToDate, "", intRoe, "")
            objRevDomTktSales = SumRev4Tkt("2335", "DOM", dteFromDate, dteToDate, "S", intRoe, "")
            objRevRegTkt = SumRev4Tkt("2335", "REG", dteFromDate, dteToDate, "", intRoe, "")
            objRevIntlTkt = SumRev4Tkt("2335", "INTL", dteFromDate, dteToDate, "", intRoe, "")
            objRevHotel = SumRevNoSfNonAirService("2335", dteFromDate, dteToDate, intRoe, NonAirServices.Hotel)
            objRevCar = SumRevNoSfNonAirService("2335", dteFromDate, dteToDate, intRoe, NonAirServices.Car)
            objRevVisa = SumRevNoSfNonAirService("2335", dteFromDate, dteToDate, intRoe, NonAirServices.Visa)

            objSvcFeeAhc = SumSvcFee4Ahc("2335", dteFromDate, dteToDate, intRoe)

            'objSvcFeeDomTkt = SumSvcFee4Tkt("2335", "DOM", dteFromDate, dteToDate, "", intRoe, "")
            objSvcFeeRegTkt = SumSvcFee4Tkt("2335", "REG", dteFromDate, dteToDate, "", intRoe, "")
            objSvcFeeIntlTkt = SumSvcFee4Tkt("2335", "INTL", dteFromDate, dteToDate, "", intRoe, "")

            objRevNoSfCar = SumRevNoSfNonAirService("2335", dteFromDate, dteToDate, intRoe, NonAirServices.Car)

            .Range("E24").Value = objRevDomTkt.Nbr
            .Range("E25").Value = objRevRegTkt.Nbr
            .Range("E26").Value = objRevIntlTkt.Nbr

            .Range("E18").Value = objRevDomTkt.Amt + objRevRegTkt.Amt + objRevIntlTkt.Amt

            .Range("E30").Value = objRevCar.Nbr
            .Range("E121").Value = objRevHotel.Nbr

            .Range("E115").Value = objSvcFeeAhc.Amt
            .Range("E120").Value = ""

            If objRevVisa.Amt <> 0 Then
                MsgBox("Tu dien tay SF cho Visa vao o E120")
            End If


        End With

        Return True
    End Function
    Private Function FillDataNonAir(strCmc As String, strRtgType As String _
                                 , dteFromDate As Date, dteToDate As Date, ByRef objWsh As Worksheet)
        Dim strQuerry As String
        Dim tblResult As System.Data.DataTable
        Dim i As Integer
        strQuerry = "select t.TCode,i.RecID, i.RelatedItem,i.Service,'VND',i.TTLToPax,i.Brief" _
            & " from RAS12.dbo.DuToan_Item i" _
            & " LEFT JOIN RAS12.DBO.DuToan_Tour t on i.DuToanID=t.RecID" _
            & " where i.Status='OK'" _
            & " and t.Status ='RR' and t.Contact<>'ZPERSONAL' and t.LstUpdate between '" _
            & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) _
            & "' and i.CustID in " _
            & " (select CustID from cwt.dbo.GO_CompanyInfo1 c where c.Status='ok' and c.CMC='" & strCmc & "')" _
            & " order by t.LstUpdate,i.recid"

        tblResult = pobjTvcs.GetDataTable(strQuerry)
        For i = 1 To tblResult.Columns.Count
            objWsh.Cells(1, i) = tblResult.Columns(i - 1).ColumnName
        Next
        For i = 1 To tblResult.Rows.Count
            objWsh.Range("A" & i + 1 & ":G" & i + 1).Value = tblResult.Rows(i - 1).ItemArray
        Next
        objWsh.Columns("A:G").AUTOFIT()
        Return True
    End Function
    Private Function FillDataAir(strCmc As String, strRtgType As String _
                                 , dteFromDate As Date, dteToDate As Date, ByRef objWsh As Worksheet)
        Dim strQuerry As String
        Dim tblResult As System.Data.DataTable
        Dim i As Integer
        strQuerry = "select t.TKNO,t.SRV,r1.DeKho as RtgType,t.DOI,t.Itinerary,t.Currency,t.Fare,t.Tax" _
            & ",t.Charge,t.ChargeTV,t.Charge_D " _
            & " from RAS12.dbo.TKT t" _
            & " LEFT JOIN RAS12.DBO.RCP r on t.RCPID=r.RecID" _
            & " LEFT JOIN RAS12.DBO.ReportData r1 on t.RecID=r1.TKID" _
            & " where t.Status<>'XX' AND t.AL<>'01' and t.Qty<>0" _
            & " and t.SRV<>'V' and t.DOI between '" & CreateFromDate(dteFromDate) _
            & "' and '" & CreateToDate(dteToDate) _
            & "' and substring(T.RMK,1,4)='BIZT'" _
            & " and r.Status='OK' and r.CustID in " _
            & " (select CustID from cwt.dbo.GO_CompanyInfo1 c where c.Status='ok' and c.CMC='" & strCmc & "')" _
            & " and r1.DeKho='" & strRtgType & "'" _
            & " order by r.RCPNo"

        tblResult = pobjTvcs.GetDataTable(strQuerry)
        For i = 1 To tblResult.Columns.Count
            objWsh.Cells(1, i) = tblResult.Columns(i - 1).ColumnName
        Next
        For i = 1 To tblResult.Rows.Count
            objWsh.Range("A" & i + 1 & ":K" & i + 1).Value = tblResult.Rows(i - 1).ItemArray
        Next
        objWsh.Columns("A:K").AUTOFIT()
        Return True
    End Function
    Private Function RunEyQuarterlySettlement() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim strTargetColumn As String = "H"
        Dim strTargetSheetName As String = mobjWbk.ActiveSheet.name
        Dim i As Integer, j As Integer
        Dim intRoe As Integer = 1
        'Dim strCheckedColumns As String = "EFGHIJKLMOQRSTUVWXYZ"

        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = strTargetSheetName Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If

        For i = dtpFromDate.Value.Month To dtpToDate.Value.Month
            Dim dteFromDate As Date = DateSerial(dtpFromDate.Value.Year, i, 1)
            Dim dteToDate As Date = CreateToDate(dteFromDate.AddMonths(1).AddDays(-1))

            With objWsh
                'For j = 1 To strCheckedColumns.Length
                '    If .Range(Mid(strCheckedColumns, j, 1) & 5).Value = Format(dteFromDate, "MMMM") Then
                '        strTargetColumn = Mid(strCheckedColumns, j, 1)
                '        Exit For
                '    End If
                'Next
                strTargetColumn = InputBox("Input column for Month " & dteFromDate.Month)
                Dim objRevDomTkt As clsSum
                Dim objRevIntlTkt As clsSum
                Dim objRevRegTkt As clsSum
                Dim objSfLccTkt As clsSum
                Dim objRevRefundedTkt As clsSum
                Dim objRevHotel As clsSum
                Dim objRevCar As clsSum
                Dim objSfAhc As clsSum
                Dim objCommDomm As New clsSum
                Dim objCommReg As New clsSum
                Dim objCommInt As New clsSum

                objRevDomTkt = SumRev4Tkt("396", "DOM", dteFromDate, dteToDate, "", intRoe, "")
                objRevRegTkt = SumRev4Tkt("396", "REG", dteFromDate, dteToDate, "", intRoe, "")
                objRevIntlTkt = SumRev4Tkt("396", "INTL", dteFromDate, dteToDate, "", intRoe, "")
                objSfLccTkt = SumSvcFee4Tkt("396", "", dteFromDate, dteToDate, "", intRoe, "LCC")
                objRevRefundedTkt = SumRev4Tkt("396", "", dteFromDate, dteToDate, "R", intRoe, "")

                objRevHotel = SumRevNoSfNonAirService("396", dteFromDate, dteToDate, 1, NonAirServices.Hotel)
                objRevCar = SumRevNoSfNonAirService("396", dteFromDate, dteToDate, 1, NonAirServices.Car)
                objSfAhc = SumSvcFee4Ahc("396", dteFromDate, dteToDate, 1)

                objCommReg = SumComm4Tkt("396", "REG", dteFromDate, dteToDate, "", intRoe, "")
                objCommInt = SumComm4Tkt("396", "INTL", dteFromDate, dteToDate, "", intRoe, "")

                .Range(strTargetColumn & 9).Value = objRevDomTkt.Nbr
                .Range(strTargetColumn & 10).Value = objRevRegTkt.Nbr
                .Range(strTargetColumn & 12).Value = objRevIntlTkt.Nbr
                .Range(strTargetColumn & 13).Value = objSfLccTkt.Nbr
                .Range(strTargetColumn & 16).Value = objRevHotel.Nbr
                .Range(strTargetColumn & 18).Value = objRevCar.Nbr
                .Range(strTargetColumn & 19).Value = objRevRefundedTkt.Nbr
                .Range(strTargetColumn & 83).Value = objSfAhc.Amt / 1.1
                .Range(strTargetColumn & 109).Value = objCommReg.Amt / 1.1
                .Range(strTargetColumn & 110).Value = objCommInt.Amt / 1.1
            End With

            
        Next
        
        Return True
    End Function

    Private Function RunJpmcQuarterlySettlement() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim strTargetColumn As String = "D"
        Dim strTargetSheetName As String = "Vietnam"
        'Dim i As Integer
        Dim intRoe As Integer = 1
        Dim dteFromDate As Date = CreateFromDate(dtpFromDate.Value)

        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = strTargetSheetName Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        With objWsh

            '.Range("B2").Value = "Q" & dtpToDate.Value.Month / 3 & " " & dtpToDate.Value.Year

            Dim objRevDomTkt As clsSum
            Dim objRevIntlTkt As clsSum
            Dim objRevRegTkt As clsSum
            Dim objSvcFeeDomTkt As clsSum
            Dim objSvcFeeRegTkt As clsSum
            Dim objSvcFeeIntlTkt As clsSum
            Dim objSfRefundedTkt As clsSum

            Dim objSvcFeeNonAir As clsSum
            Dim objSfAhc As clsSum

            'Dim objRevNoSfCar As clsSum
            'Dim objRevNoSfHtl As clsSum
            'Dim objRevNoSfVisa As clsSum

            objRevDomTkt = SumRev4Tkt("1904", "DOM", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevRegTkt = SumRev4Tkt("1904", "REG", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevIntlTkt = SumRev4Tkt("1904", "INTL", dteFromDate, dtpToDate.Value, "", intRoe, "")

            objSvcFeeDomTkt = SumSvcFee4Tkt("1904", "DOM", dteFromDate, dtpToDate.Value, "S", intRoe, "")
            objSvcFeeRegTkt = SumSvcFee4Tkt("1904", "REG", dteFromDate, dtpToDate.Value, "S", intRoe, "")
            objSvcFeeIntlTkt = SumSvcFee4Tkt("1904", "INTL", dteFromDate, dtpToDate.Value, "S", intRoe, "")
            objSfRefundedTkt = SumSvcFee4Tkt("1904", "", dteFromDate, dtpToDate.Value, "R", intRoe, "R")

            objSvcFeeNonAir = SumSvcFee4NonAir("1904", dteFromDate, dtpToDate.Value, intRoe)
            objSfAhc = SumSvcFee4Ahc("1904", dteFromDate, dtpToDate.Value, intRoe)

            If objSvcFeeNonAir.Amt <> 0 Then
                .Range(strTargetColumn & 39).Value = ""
                .Range(strTargetColumn & 41).Value = ""
                .Range(strTargetColumn & 44).Value = ""
                MsgBox("Can dien tay thong tin non air")
            End If
            'objRevNoSfCar = SumRevNoSfNonAirService("1904", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Car)
            'objRevNoSfHtl = SumRevNoSfNonAirService("1904", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Hotel)
            'objRevNoSfVisa = SumRevNoSfNonAirService("1904", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Visa)

            .Range(strTargetColumn & 10).Value = objRevDomTkt.Amt + objRevRegTkt.Amt + objRevIntlTkt.Amt
            .Range(strTargetColumn & 19).Value = objRevDomTkt.Nbr
            .Range(strTargetColumn & 20).Value = objRevRegTkt.Nbr
            .Range(strTargetColumn & 21).Value = objRevIntlTkt.Nbr
            .Range(strTargetColumn & 34).Value = objSvcFeeDomTkt.Amt / 1.1
            .Range(strTargetColumn & 35).Value = objSvcFeeRegTkt.Amt / 1.1
            .Range(strTargetColumn & 36).Value = objSvcFeeIntlTkt.Amt / 1.1
            .Range(strTargetColumn & 84).Value = objSfAhc.Amt / 1.1
            .Range(strTargetColumn & 88).Value = objSfRefundedTkt.Amt / 1.1

        End With

        Return True
    End Function
    Private Function RunEricssonQuarterlySettlement() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim strTargetColumn As String = "D"
        Dim strTargetSheetName As String
        'Dim i As Integer
        Dim intRoe As Integer = 1


        Dim dteFromDate As Date = dtpFromDate.Value
        Dim dteToDate As Date = dtpToDate.Value

        strTargetSheetName = "Input Template"
        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = strTargetSheetName Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        With objWsh

            .Range("A4").Value = "Q" & DatePart(DateInterval.Quarter, dteFromDate) & dteFromDate.Year
            .Range("D7").Value = .Range("A4").Value
            .Range("A5").Value = "Vietnam"

            Dim objRevDomTkt As clsSum
            Dim objRevIntlTkt As clsSum
            Dim objRevRegTkt As clsSum
            Dim objRevLccTkt As clsSum
            Dim objRevRefundedTkt As clsSum
            Dim objRevCar As clsSum
            Dim objRevHtl As clsSum
            Dim objSfAhc As clsSum

            objRevDomTkt = SumRev4Tkt("504", "DOM", dteFromDate, dteToDate, "S", intRoe, "")
            objRevRegTkt = SumRev4Tkt("504", "REG", dteFromDate, dteToDate, "S", intRoe, "")
            objRevIntlTkt = SumRev4Tkt("504", "INTL", dteFromDate, dteToDate, "S", intRoe, "")
            objRevLccTkt = SumRev4Tkt("504", "", dteFromDate, dteToDate, "S", intRoe, "LCC")
            objRevRefundedTkt = SumRev4Tkt("504", "", dteFromDate, dteToDate, "R", intRoe, "")
            objRevCar = SumRevNoSfNonAirService("504", dteFromDate, dteToDate, intRoe, NonAirServices.Car)
            objRevHtl = SumRevNoSfNonAirService("504", dteFromDate, dteToDate, intRoe, NonAirServices.Hotel)
            objSfAhc = SumSvcFee4Ahc("504", dteFromDate, dteToDate, intRoe)

            .Range(strTargetColumn & 12).Value = objRevIntlTkt.Nbr
            .Range(strTargetColumn & 13).Value = objRevRegTkt.Nbr
            .Range(strTargetColumn & 14).Value = objRevDomTkt.Nbr
            .Range(strTargetColumn & 15).Value = objRevLccTkt.Nbr

            .Range(strTargetColumn & 20).Value = objRevCar.Nbr
            .Range(strTargetColumn & 55).Value = objRevHtl.Nbr
            .Range(strTargetColumn & 22).Value = objRevRefundedTkt.Nbr
            .Range(strTargetColumn & 54).Value = objSfAhc.Nbr


        End With

        Return True

    End Function
    Private Function RunChevronQuarterlySettlement() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim strTargetColumn As String = "H"
        Dim strTargetSheetName As String
        'Dim i As Integer
        Dim intRoe As Integer = 1
        Dim dteFromDate As Date = CreateFromDate(dtpFromDate.Value)

        strTargetSheetName = "Q" & dtpToDate.Value.Month / 3 & " " & dtpToDate.Value.Year
        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = strTargetSheetName Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        With objWsh

            .Range("B2").Value = .Name

            Dim objRevDomTkt As clsSum
            Dim objRevIntlTkt As clsSum
            Dim objRevRegTkt As clsSum
            Dim objSvcFeeDomTkt As clsSum
            Dim objSvcFeeRegTkt As clsSum
            Dim objSvcFeeIntlTkt As clsSum
            Dim objSvcFeeNonAir As clsSum
            Dim objRevNoSfNonAir As clsSum

            objRevDomTkt = SumRev4Tkt("317", "DOM", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevRegTkt = SumRev4Tkt("317", "REG", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevIntlTkt = SumRev4Tkt("317", "INTL", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objSvcFeeDomTkt = SumSvcFee4Tkt("317", "DOM", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objSvcFeeRegTkt = SumSvcFee4Tkt("317", "REG", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objSvcFeeIntlTkt = SumSvcFee4Tkt("317", "INTL", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objSvcFeeNonAir = SumSvcFee4NonAir("317", dteFromDate, dtpToDate.Value, intRoe)
            objRevNoSfNonAir = SumRevNoSfNonAir("317", dteFromDate, dtpToDate.Value, intRoe)

            .Range(strTargetColumn & 7).Value = objRevDomTkt.Amt
            .Range(strTargetColumn & 8).Value = objRevRegTkt.Amt
            .Range(strTargetColumn & 9).Value = objRevIntlTkt.Amt
            .Range(strTargetColumn & 12).Value = objRevDomTkt.Nbr
            .Range(strTargetColumn & 13).Value = objRevRegTkt.Nbr
            .Range(strTargetColumn & 14).Value = objRevIntlTkt.Nbr
            .Range(strTargetColumn & 18).Value = objRevNoSfNonAir.Nbr
            .Range(strTargetColumn & 21).Value = objSvcFeeDomTkt.Amt
            .Range(strTargetColumn & 22).Value = objSvcFeeRegTkt.Amt
            .Range(strTargetColumn & 23).Value = objSvcFeeIntlTkt.Amt
            .Range(strTargetColumn & 26).Value = objSvcFeeNonAir.Amt
            If objSvcFeeNonAir.Amt <> 0 Then
                MsgBox("Can tach service fee cho Non Air")
            End If
            '.Range("D51").Value = objSvcFeeNonAir.Amt

        End With

        Return True
    End Function
    Private Function RunGeMontlyAirBreakdown() As Boolean
        Dim objWsh As New Worksheet
        Dim intRoe As Integer = 23000
        Dim strTargetSheetName As String = String.Empty
        Dim blnSheetFound As Boolean
        Dim dteFromDate As Date = CreateFromDate(dtpFromDate.Value)
        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = "Sheet1" Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If

        With objWsh

            .Range("A1").Value = Format(dteFromDate, "MMM yy")

            Dim objSvcFeeDomTktInUsd As clsSum
            Dim objSvcFeeRegTktInUsd As clsSum
            Dim objSvcFeeIntlTktInUsd As clsSum

            objSvcFeeDomTktInUsd = SumSvcFee4Tkt("43", "DOM", dteFromDate, dtpToDate.Value _
                                              , "", intRoe, "")
            objSvcFeeRegTktInUsd = SumSvcFee4Tkt("43", "REG", dteFromDate, dtpToDate.Value _
                                              , "", intRoe, "")
            objSvcFeeIntlTktInUsd = SumSvcFee4Tkt("43", "INTL", dteFromDate, dtpToDate.Value _
                                              , "", intRoe, "")

            .Range("B3").Value = objSvcFeeDomTktInUsd.Nbr
            .Range("C3").Value = objSvcFeeDomTktInUsd.Amt
            .Range("B4").Value = objSvcFeeRegTktInUsd.Nbr
            .Range("C4").Value = objSvcFeeRegTktInUsd.Amt
            .Range("B5").Value = objSvcFeeIntlTktInUsd.Nbr
            .Range("C5").Value = objSvcFeeIntlTktInUsd.Amt

        End With

        Return True
    End Function
    Private Function RunGeMontlyScorecard() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim strTargetColumn As String = String.Empty
        Dim strDateColumns As String = "E"
        Dim dteFromDate As Date = CreateFromDate(dtpFromDate.Value)
        Dim dte01JanLastYear As Date = DateSerial(dteFromDate.Year - 1, 1, 1)
        Dim dte01JanThisYear As Date = DateSerial(dteFromDate.Year, 1, 1)
        Dim dteToDateLastYear As Date = dtpToDate.Value.AddYears(-1)
        Dim i As Integer
        Dim intRoe As Integer = 21000

        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = "Vietnam" Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        With objWsh
        
            '    .Range("B9").Value = "Vietnam ROE: " & intRoe

            Dim objSvcFeeTktInUsd As clsSum
            Dim objSvcFeeAhcInUsd As clsSum
            
            objSvcFeeTktInUsd = SumSvcFeeNoVat4Tkt("43", "", dteFromDate, dtpToDate.Value _
                                              , "", intRoe, "")
            objSvcFeeAhcInUsd = SumSvcFee4Ahc("43", dteFromDate, dtpToDate.Value, intRoe)

            .Range("E12").Value = objSvcFeeTktInUsd.Nbr
            .Range("E13").Value = objSvcFeeAhcInUsd.Nbr


            .Range("E21").Value = objSvcFeeTktInUsd.Amt + objSvcFeeAhcInUsd.Amt
            '.Range("E50").Value = objSvcFeeAhcInUsd.Amt

            'YTD Last year
            objSvcFeeTktInUsd = SumSvcFeeNoVat4Tkt("43", "", dte01JanLastYear, dteToDateLastYear _
                                              , "", intRoe, "")
            objSvcFeeAhcInUsd = SumSvcFee4Ahc("43", dte01JanLastYear, dteToDateLastYear, intRoe)

            .Range("I12").Value = objSvcFeeTktInUsd.Nbr
            .Range("I13").Value = objSvcFeeAhcInUsd.Nbr

            .Range("I21").Value = objSvcFeeTktInUsd.Amt + objSvcFeeAhcInUsd.Amt
            '.Range("I50").Value = objSvcFeeAhcInUsd.Amt

            'YTD this year
            objSvcFeeTktInUsd = SumSvcFeeNoVat4Tkt("43", "", dte01JanThisYear, dtpToDate.Value _
                                              , "", intRoe, "")
            objSvcFeeAhcInUsd = SumSvcFee4Ahc("43", dte01JanThisYear, dtpToDate.Value, intRoe)

            '.Range("K12").Value = objSvcFeeTktInUsd.Nbr
            '.Range("K12").Value = objSvcFeeAhcInUsd.Nbr

            '.Range("K21").Value = objSvcFeeTktInUsd.Amt + objSvcFeeAhcInUsd.Amt
            '.Range("K50").Value = objSvcFeeAhcInUsd.Amt
        End With

        Return True
    End Function
    Private Function RunHalliburtonMontlyCPT() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim strTargetColumn As String = String.Empty
        Dim strDateColumns As String = "BCDEFGHIJKLM"
        Dim dteFromDate As Date = CreateFromDate(dtpFromDate.Value)
        Dim i As Integer
        Dim intRoe As Integer = 23000

        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = "VN" Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        With objWsh
            If Not IsDate(.Range("B1").Value) Then
                MsgBox("invalid file format")
                mobjExcel.Quit()
                Return False
            Else
                mobjExcel.Visible = True
            End If

            For i = 0 To strDateColumns.Length - 1
                If .Range(strDateColumns.Chars(i) & 1).Value = dteFromDate.Date Then
                    strTargetColumn = strDateColumns.Chars(i)
                    Exit For
                End If
            Next
            If strTargetColumn = "" Then
                MsgBox("Can not find Target Month")
                Return False
            End If
            .Range(strTargetColumn & "8").Value = intRoe
            Dim objSvcFeeTktInUsd As clsSum
            Dim objSvcFeeAhcInUsd As clsSum
            Dim objSvcFeeNonAirInUsd As clsSum
            Dim objSvcFeeFscDomTktInUsd As clsSum
            Dim objSvcFeeFscIntlTktInUsd As clsSum
            Dim objSvcFeeFscRegTktInUsd As clsSum
            Dim objSvcFeeRefundTktInUsd As clsSum
            Dim objSvcFeeLccDomTktInUsd As clsSum
            Dim objSvcFeeLccIntlTktInUsd As clsSum
            Dim objSvcFeeLccRegTktInUsd As clsSum
            Dim objRevNonAir As clsSum

            objSvcFeeTktInUsd = SumSvcFee4Tkt("1843", "", dteFromDate, dtpToDate.Value _
                                              , "", intRoe, "")
            objSvcFeeFscDomTktInUsd = SumSvcFee4Tkt("1843", "DOM", dteFromDate, dtpToDate.Value _
                                              , "S", intRoe, "FSC")
            objSvcFeeFscIntlTktInUsd = SumSvcFee4Tkt("1843", "INTL", dteFromDate, dtpToDate.Value _
                                              , "S", intRoe, "FSC")
            objSvcFeeFscRegTktInUsd = SumSvcFee4Tkt("1843", "REG", dteFromDate, dtpToDate.Value _
                                              , "S", intRoe, "FSC")
            objSvcFeeRefundTktInUsd = SumSvcFee4Tkt("1843", "", dteFromDate, dtpToDate.Value _
                                              , "R", intRoe, "")
            objSvcFeeLccDomTktInUsd = SumSvcFee4Tkt("1843", "DOM", dteFromDate, dtpToDate.Value _
                                              , "S", intRoe, "LCC")
            objSvcFeeLccIntlTktInUsd = SumSvcFee4Tkt("1843", "INTL", dteFromDate, dtpToDate.Value _
                                              , "S", intRoe, "LCC")
            objSvcFeeLccRegTktInUsd = SumSvcFee4Tkt("1843", "REG", dteFromDate, dtpToDate.Value _
                                              , "S", intRoe, "LCC")
            objSvcFeeAhcInUsd = SumSvcFee4Ahc("1843", dteFromDate, dtpToDate.Value, intRoe)
            objSvcFeeNonAirInUsd = SumSvcFee4NonAir("1843", dteFromDate, dtpToDate.Value, intRoe)
            objRevNonAir = SumRevNoSfNonAir("1843", dteFromDate, dtpToDate.Value, intRoe)
            '.Range(strTargetColumn & "3").Value = objSvcFeeTktInUsd.Amt + objSvcFeeAhcInUsd.Amt _
            '    + objSvcFeeNonAirInUsd.Amt
            '.Range(strTargetColumn & "5").Value = objSvcFeeTktInUsd.Nbr
            .Range(strTargetColumn & "6").Value = intRoe
            .Range(strTargetColumn & "14").Value = objSvcFeeFscDomTktInUsd.Nbr
            .Range(strTargetColumn & "15").Value = objSvcFeeFscDomTktInUsd.Amt / 1.1
            .Range(strTargetColumn & "16").Value = objSvcFeeFscIntlTktInUsd.Nbr
            .Range(strTargetColumn & "17").Value = objSvcFeeFscIntlTktInUsd.Amt / 1.1
            .Range(strTargetColumn & "20").Value = objSvcFeeFscRegTktInUsd.Nbr
            .Range(strTargetColumn & "21").Value = objSvcFeeFscRegTktInUsd.Amt / 1.1
            '.Range(strTargetColumn & "23").Value = objSvcFeeAhcInUsd.Nbr
            .Range(strTargetColumn & "26").Value = objSvcFeeLccDomTktInUsd.Nbr
            .Range(strTargetColumn & "27").Value = objSvcFeeLccDomTktInUsd.Amt / 1.1
            .Range(strTargetColumn & "28").Value = objSvcFeeLccIntlTktInUsd.Nbr
            .Range(strTargetColumn & "29").Value = objSvcFeeLccIntlTktInUsd.Amt / 1.1
            .Range(strTargetColumn & "30").Value = objSvcFeeLccRegTktInUsd.Nbr
            .Range(strTargetColumn & "31").Value = objSvcFeeLccRegTktInUsd.Amt / 1.1
            .Range(strTargetColumn & "32").Value = objRevNonAir.Nbr
            .Range(strTargetColumn & "33").Value = objSvcFeeNonAirInUsd.Amt / 1.1
            .Range(strTargetColumn & "34").Value = objSvcFeeRefundTktInUsd.Nbr
            .Range(strTargetColumn & "35").Value = objSvcFeeRefundTktInUsd.Amt
            .Range(strTargetColumn & "38").Value = objSvcFeeAhcInUsd.Amt / 1.1

            .Range(strTargetColumn & "40").Value = objSvcFeeFscDomTktInUsd.Nbr _
                + objSvcFeeFscIntlTktInUsd.Nbr + objSvcFeeFscRegTktInUsd.Nbr _
                + objSvcFeeLccDomTktInUsd.Nbr + objSvcFeeLccIntlTktInUsd.Nbr _
                + objSvcFeeLccRegTktInUsd.Nbr + objSvcFeeRefundTktInUsd.Nbr _
                + objRevNonAir.Nbr

            .Range(strTargetColumn & "41").Value = objSvcFeeFscDomTktInUsd.Amt _
                + objSvcFeeFscIntlTktInUsd.Amt + objSvcFeeFscRegTktInUsd.Amt _
                + objSvcFeeLccDomTktInUsd.Amt + objSvcFeeLccIntlTktInUsd.Amt _
                + objSvcFeeLccRegTktInUsd.Amt + objSvcFeeRefundTktInUsd.Amt _
                + objRevNonAir.Amt + objSvcFeeAhcInUsd.Amt


        End With

        Return True
    End Function
    Private Function RunHpiMonthly() As Boolean
        Dim objWsh As New Worksheet
        Dim blnSheetFound As Boolean
        Dim dteFromDate As Date = CreateFromDate(dtpFromDate.Value)
        Dim dteToDate As Date = CreateToDate(dtpToDate.Value)
        Dim lstEtkTypes As New List(Of String)
        Dim lstAhcTypes As New List(Of String)

        Dim strTargetSheetName As String = "HPI Template FY2023"
        'Dim i As Integer

        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            Select Case objSheet.Name
                Case strTargetSheetName
                    objWsh = objSheet
                    blnSheetFound = True
                Case "Transaction details"
                    objSheet.Range("A3:S512").Delete()
            End Select

        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If
        'dien thong tin chung
        With objWsh
            .Range("D6").Value = Format(dteToDate, "MMMM, yyyy")

            Dim objRevFscDomTkt As clsSum
            Dim objRevFscRegTkt As clsSum
            Dim objRevFscIntlTkt As clsSum
            Dim objRevLccTkt As clsSum
            Dim objSfAhcAir As clsSum
            Dim objSfAhcNonAir As clsSum
            Dim objSfVisa As clsSum
            Dim objSfHotel As clsSum

            lstEtkTypes.Add("ETK")
            lstAhcTypes.Add("AHC")

            objRevFscDomTkt = SumRev4TktHpi("DOM", dteFromDate, dteToDate, "FSC", lstEtkTypes)
            objRevFscRegTkt = SumRev4TktHpi("REG", dteFromDate, dteToDate, "FSC", lstEtkTypes)
            objRevFscIntlTkt = SumRev4TktHpi("INTL", dteFromDate, dteToDate, "FSC", lstEtkTypes)
            objRevLccTkt = SumRev4TktHpi("", dteFromDate, dteToDate, "LCC", lstEtkTypes)

            objSfAhcAir = SumSvcFee4AhcAirHpi(dteFromDate, dteToDate)
            objSfAhcNonAir = SumRevNoSfNonAirHpi(dteFromDate, dteToDate, NonAirServices.Ahc)
            objSfVisa = SumRevNoSfNonAirHpi(dteFromDate, dteToDate, NonAirServices.Visa)
            objSfHotel = SumRevNoSfNonAirHpi(dteFromDate, dteToDate, NonAirServices.Hotel)

            objWsh.Range("I10").Value = objRevFscDomTkt.Nbr
            objWsh.Range("I11").Value = objRevFscRegTkt.Nbr
            objWsh.Range("I12").Value = objRevFscIntlTkt.Nbr
            objWsh.Range("B47").Value = objSfAhcAir.Nbr + objSfAhcNonAir.Nbr
            'objWsh.Range("B50").Value = objSfVisa.Nbr
            objWsh.Range("B72").Value = objSfHotel.Nbr
            objWsh.Range("B76").Value = objRevLccTkt.Nbr
        End With

        'dien thong tin Air
        objWsh = mobjWbk.Sheets("Transaction details")
        With objWsh
            Dim strQuerry As String
            strQuerry = "select row_number() over(order by t.DOI),'HPI VN','Air','Assisted'" _
                        & ",t.ChargeTv-t.VatAmtSf,case t.StockCtrl when '' then 'Original' else 'Exchange' end" _
                        & ", t.PaxName,t.RcpNo,t.FstUpdate,(r1.F_VND+r1.T_VND+r1.C_VND-t.VatAmt) as TktAmt,t.VatAmt" _
                        & ",t.TKNO,t.DOI,a.ALName,t.DOF,right(air.DepDates,5) as LastDOF,t.Itinerary" _
                        & " from ras12.dbo.tkt t " _
                        & " left join ras12.dbo.rcp r on t.RCPID=r.RecID" _
                        & " left join RAS12.dbo.ReportData r1 on t.RecID=r1.TKID" _
                        & " left join Airline a on a.AL=t.AL" _
                        & " left join cwt.dbo.go_air air on t.RecId=air.tkid" _
                        & " where t.Status<>'xx' and t.Qty>0" _
                        & " and r.CustID in (21428,21430)" _
                        & " and t.DOI between '" & CreateFromDate(dteFromDate) _
                        & "' and '" & CreateToDate(dteToDate) & "' and t.DocType ='ETK'" _
                        & " order by t.Doi"
            Dim tblTkts As System.Data.DataTable = GetDataTable(strQuerry, Conn)
            Dim i As Integer
            For i = 0 To tblTkts.Rows.Count - 1
                objWsh.Range("A" & i + 3 & ":Q" & i + 3).Value = tblTkts.Rows(i).ItemArray
            Next


        End With
        Return True
    End Function
    Private Function HenkelPOSInvoicingHY() As Boolean
        Dim objWsh As New Worksheet
        Dim intRoe As Integer = 1
        Dim strTargetSheetName As String = "4. TOTAL fees invoiced in LC"
        Dim blnSheetFound As Boolean
        Dim dteFromDate As Date = CreateFromDate(dtpFromDate.Value)
        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = strTargetSheetName Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If

        With objWsh
            If .Range("A1").Value <> "Year: " Then
                MsgBox("invalid file format")
                mobjExcel.Quit()
                Return False
            Else
                mobjExcel.Visible = True
            End If
            .Range("B1").Value = "HY " & Format(dteFromDate, "MMM") & " - " & Format(dtpToDate.Value, "MMM yyyy")

            Dim objRevIntlTkt As clsSum
            Dim objRevRegTkt As clsSum
            Dim objRevDomTkt As clsSum
            Dim objRevHtl As clsSum
            Dim objRevCar As clsSum
            Dim objRevVisa As clsSum
            Dim objSfAhc As clsSum

            objRevIntlTkt = SumRev4Tkt("273", "INTL", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevRegTkt = SumRev4Tkt("273", "REG", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevDomTkt = SumRev4Tkt("273", "DOM", dteFromDate, dtpToDate.Value, "", intRoe, "")
            objRevHtl = SumRevNoSfNonAirService("273", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Hotel)
            objRevCar = SumRevNoSfNonAirService("273", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Car)
            objRevVisa = SumRevNoSfNonAirService("273", dteFromDate, dtpToDate.Value, intRoe, NonAirServices.Visa)
            objSfAhc = SumSvcFee4Ahc("273", dteFromDate, dtpToDate.Value, intRoe)
            .Range("B5").Value = objRevIntlTkt.Amt
            .Range("B6").Value = objRevRegTkt.Amt
            .Range("B7").Value = objRevDomTkt.Amt
            .Range("B12").Value = objRevCar.Amt
            .Range("B13").Value = objRevHtl.Amt
            .Range("B16").Value = objSfAhc.Amt
            .Range("B23").Value = objRevVisa.Amt

            MsgBox("Non Air Services are calculated WITHOUT SERVICE FEE! Please add it manually")

        End With


        Return True
    End Function
    Private Function IFADFortnightly() As Boolean
        Dim objWsh As New Worksheet
        Dim intRoe As Integer = 1
        Dim strTargetSheetName As String = "Sheet1"
        Dim blnSheetFound As Boolean
        Dim strFromDate As String = CreateFromDate(dtpFromDate.Value)
        Dim strToDate As String = CreateToDate(dtpToDate.Value)
        Dim tblTkts As System.Data.DataTable
        Dim i As Integer = 2

        tblTkts = pobjTvcs.GetDataTable("select m2.VAL1+m.Details,t.ShortTkno, t.PaxName,t.RCPNO" _
                    & ",t.Itinerary,ci.CityName,(t.Fare+t.Tax)*T.qty+t.charge,t.ChargeTV" _
                    & ",convert(varchar(9),t.DOF,6),g.Ref3,g.Ref4,g.Ref2" _
                    & " from ras12.dbo.tkt t" _
                    & " left join ras12.dbo.rcp r on t.RCPID=r.RecID" _
                    & " left join ras12.dbo.ReportData r1 on t.RecID=r1.TKID" _
                    & " left join ras12.dbo.CityCode ci on r1.Dest=ci.Airport" _
                    & " left join cwt.dbo.GO_Air a on t.RecID=a.Tkid" _
                    & " left join cwt.dbo.GO_Travel g on a.TravelID=g.RecID" _
                    & " left join ras12.dbo.FOP f on f.RCPID=r.RecID and f.Status='ok' and f.FOP='CRD'" _
                    & " Left join ras12.dbo.MISC m on m.CAT='CcNbr' and f.CcId=m.RecID" _
                    & " Left join ras12.dbo.MISC m2 on m2.CAT='PrefixC' and m.RecID = cast(m2.VAL as int)" _
                    & " where t.Status<>'xx' and t.Qty<>0" _
                    & " and substring(t.RMK,1,4)='BIZT'" _
                    & " and f.LstUpdate between '" & strFromDate & "' and '" & strToDate _
                    & "' and r.CustShortName='IFAD VN'" _
                    & " order by t.doi")
        If tblTkts.Rows.Count = 0 Then
            MsgBox("No data for selected period")
            Return True
        End If
        mobjExcel.Visible = True
        For Each objSheet As Worksheet In mobjWbk.Sheets
            If objSheet.Name = strTargetSheetName Then
                objWsh = objSheet
                blnSheetFound = True
                Exit For
            End If
        Next
        If Not blnSheetFound Then
            MsgBox("Unable to find Relevant sheet")
            Return False
        End If

        With objWsh
            If .Range("A1").Value <> "AIR PLUS AIDA Card No." Then
                MsgBox("invalid file format")
                mobjExcel.Quit()
                Return False
            Else
                mobjExcel.Visible = True
                For Each objRow As DataRow In tblTkts.Rows
                    .Range("A" & i & ":L" & i).Value = objRow.ItemArray
                    .Range("E" & i).Value = RemoveCarInItinerary(.Range("E" & i).Value)
                    i = i + 1
                Next
                .Columns("G:h").NumberFormat = "#,##0"
                .Columns("G:h").HorizontalAlignment = -4152
            End If
        End With


        Return True
    End Function
    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub cboReport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboReport.SelectedIndexChanged

        If cboReport.Text.Contains("Monthly") Then
            dtpFromDate.Value = Now.AddMonths(-1)
            dtpFromDate.Value = dtpFromDate.Value.AddDays(1 - dtpFromDate.Value.Day)
            'dtpToDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1)
        ElseIf cboReport.Text.Contains("Fortnightly") Then
            If Now.Day < 16 Then
                If Now.Month = 1 Then
                    dtpFromDate.Value = DateSerial(Now.Year - 1, 12, 16)
                Else
                    dtpFromDate.Value = DateSerial(Now.Year, Now.Month - 1, 16)
                End If
                dtpToDate.Value = dtpToDate.Value.AddDays(16)
                dtpToDate.Value = DateSerial(dtpToDate.Value.Year, dtpToDate.Value.Month, 1).AddDays(-1)
            Else
                dtpFromDate.Value = DateSerial(Now.Year, Now.Month, 1)
                dtpToDate.Value = DateSerial(Now.Year, Now.Month, 15)
            End If

            'dtpFromDate.Value = dtpFromDate.Value.AddDays(1 - dtpFromDate.Value.Day)
            'dtpToDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1)
        ElseIf cboReport.Text.Contains("Quarterly") Then
            If Now.Month < 4 Then
                dtpFromDate.Value = DateSerial(Now.Year - 1, 10, 1)
                'dtpToDate.Value = DateSerial(Now.Year - 1, 12, 31)
            ElseIf Now.Month < 7 Then
                dtpFromDate.Value = DateSerial(Now.Year, 1, 1)
                'dtpToDate.Value = DateSerial(Now.Year, 3, 31)
            ElseIf Now.Month < 10 Then
                dtpFromDate.Value = DateSerial(Now.Year, 4, 1)
                'dtpToDate.Value = DateSerial(Now.Year, 6, 30)
            Else
                dtpFromDate.Value = DateSerial(Now.Year, 7, 1)
                'dtpToDate.Value = DateSerial(Now.Year, 9, 30)
            End If
            

        ElseIf cboReport.Text.EndsWith("HY") Then
            If Now.Month > 6 Then
                dtpFromDate.Value = DateSerial(Now.Year, 1, 1)
                'dtpToDate.Value = DateSerial(Now.Year, 6, 30)
            Else
                dtpFromDate.Value = DateSerial(Now.Year - 1, 7, 1)
                'dtpToDate.Value = DateSerial(Now.Year - 1, 12, 31)
            End If
        End If

        If cboReport.Text.StartsWith("Bayer") Then
            mstrCompanyName = "BAYER"
        ElseIf cboReport.Text.StartsWith("Citi") Then
            mstrCompanyName = "CITI"
        ElseIf cboReport.Text.StartsWith("Chevron") Then
            mstrCompanyName = "CHEVRON"
        ElseIf cboReport.Text.StartsWith("Ericsson") Then
            mstrCompanyName = "ERICSSON"
        ElseIf cboReport.Text.StartsWith("EY") Then
            mstrCompanyName = "ERNST YOUNG"
        ElseIf cboReport.Text.StartsWith("GE") Then
            mstrCompanyName = "GE"
        ElseIf cboReport.Text.StartsWith("Halliburton") Then
            mstrCompanyName = "HALLIBURTON"
        ElseIf cboReport.Text.StartsWith("Henkel") Then
            mstrCompanyName = "HENKEL"
        ElseIf cboReport.Text.StartsWith("JPMC") Then
            mstrCompanyName = "JPMC"
        ElseIf cboReport.Text.StartsWith("Oracle") Then
            mstrCompanyName = "ORACLE"
        ElseIf cboReport.Text.StartsWith("Walmart") Then
            mstrCompanyName = "WALMART"
        End If
            

    End Sub

    Private Sub dtpFromDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpFromDate.ValueChanged
        If cboReport.Text.Contains("Monthly") Then
            dtpToDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1)

        ElseIf cboReport.Text.Contains("Quarterly") Then
            dtpToDate.Value = dtpFromDate.Value.AddMonths(3).AddDays(-1)
        ElseIf cboReport.Text.Contains("Fortnightly") Then
            Select dtpFromDate.Value.Day
                Case 1
                    dtpToDate.Value = dtpFromDate.Value.AddDays(15)
                Case 16
                    dtpToDate.Value = DateSerial(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End Select


        ElseIf cboReport.Text.EndsWith("HY") Then
            dtpToDate.Value = dtpFromDate.Value.AddMonths(6).AddDays(-1)
        End If
    End Sub

    Private Sub lbkGetData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkGetData.LinkClicked
        Dim objExcel As New Excel.Application
        Dim objWbk As Workbook
        Dim objWsh As Worksheet

        objWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath & "\CTS_DataExtract.xlt", , , , , , , , True)
        objWsh = objWbk.Sheets("Para")
        With objWsh
            .Range("B2").Value = dtpFromDate.Value
            .Range("B3").Value = dtpToDate.Value
            .ComboBox1.Text = mstrCompanyName
            .Range("B13").Value = "YES"
        End With
        objExcel.Visible = True
    End Sub
    Private Function RunVisaMonthlyCta()
        Dim strQuerry As String = "select f.RCPNo,t.PaxName, t.tkno" _
                                    & ", tr.Ref1 as CostCenter,tr.Ref3 as LegalEntitu,tr.ref9 as GL" _
                                    & ",f.Amount as CcAmount" _
                                    & " from ras12.dbo.fop f" _
                                    & " left join ras12.dbo.rcp r on f.rcpid=r.recid" _
                                    & " left join ras12.dbo.tkt t on r.RecID=t.RCPID" _
                                    & " left join cwt.dbo.GO_Air a on a.Tkid=t.RecID" _
                                    & " left join cwt.dbo.GO_Travel tr on a.TravelID=tr.RecID" _
                                    & " where f.status='ok' and f.fop='crd'" _
                                    & " and f.ccid=23610 and t.Status<>'xx'" _
                                    & " and f.FstUpdate between '" & CreateFromDate(dtpFromDate.Value) _
                                    & "' and '" & CreateToDate(dtpToDate.Value) _
                                    & "' order by f.FstUpdate"
        Dim tblResult As System.Data.DataTable

        tblResult = pobjTvcs.GetDataTable(strQuerry)

        Return Table2Excel(tblResult)
        

    End Function
    Private Function RunLOrealYearlyAir()
        Dim i As Integer
        Dim strQuerry As String = "select t.tkno,t.paxname" _
                                & ", substring(t.rmk, 9, 50) as Booker" _
                                & ",'' as Division,t.Itinerary" _
                                & ",t.al,t.DOI,(R1.F_vnd+R1.T_VND)*T.Qty +R1.C_VND as Revenue,t.RecId as Tkid" _
                                & " from ras12.dbo.tkt t" _
                                & " left join ras12.dbo.rcp r on t.RCPID=r.RecID" _
                                & " left join ras12.dbo.ReportData r1 on t.RecID=r1.TKID" _
                                & " where t.status<>'XX' and t.qty<>0 " _
                                & " and substring(t.rmk,1,4)='BIZT'" _
                                & " and r.CustShortName ='LOR VN' and r.Status='ok'" _
                                & " and t.DOI between '" & CreateFromDate(dtpFromDate.Value) _
                                & "' and '" & CreateToDate(dtpToDate.Value) _
                                & "' order by tkno"

        Dim tblResult As System.Data.DataTable

        tblResult = pobjTvcs.GetDataTable(strQuerry)

        If tblResult.Rows.Count > 0 Then
            Dim objExcel As New Microsoft.Office.Interop.Excel.Application
            Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
            Dim objWbk As Microsoft.Office.Interop.Excel._Workbook
            Dim lstQuerries As New List(Of String)
            Dim strLastColumn As String = ConvertExcelColumnNbr2Letter(tblResult.Columns.Count)

            objExcel.Visible = False
            objWbk = objExcel.Workbooks.Add
            objWsh = objWbk.ActiveSheet
            With objWsh
                For i = 0 To tblResult.Columns.Count - 1
                    .Cells(1, i + 1) = tblResult.Columns(i).ColumnName
                Next

                For i = 0 To tblResult.Rows.Count - 1
                    objWsh.Range("A" & i + 2 & ":" & strLastColumn & i + 2).Value = tblResult.Rows(i).ItemArray
                Next
                .Columns("A:" & strLastColumn).AUTOFIT()

                For i = 2 To tblResult.Rows.Count + 1
                    objWsh.Range("D" & i).Value = pobjTvcs.GetScalarAsString("select Hierachy2 from Go_travel where Recid=" _
                                                        & "(Select top 1 TravelId from go_air where tkid=" _
                                                        & objWsh.Range("i" & i).Value & ")")
                Next

            End With


            objExcel.Visible = True
        End If

        Return True

    End Function
End Class
