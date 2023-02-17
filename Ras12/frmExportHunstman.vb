Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop

Public Class frmExportHunstman

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub
    Private Function Export(strCur As String, ByRef objWs As Worksheet, intStarLine As Integer _
                            , strCustIds As String, decRoe As Decimal) As Boolean
        Dim drTravel As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        Dim strEndDate As String
        Dim strStartDate As String
        Dim strTravellerName As String = ""
        'Dim intTktCount As Integer
        Dim strQuerry As String

        Dim i As Integer = intStarLine

        'ExportHotelOnly()

        strStartDate = CreateFromDate(dtpFromDate.Value.Date)
        strEndDate = CreateToDate(dtpToDate.Value.Date)

        pobjTvcs.Connect2()
        cmdSql.Connection = pobjTvcs.Connection2
        strQuerry = "select a.*,t.TravellerName,t.TripPurpose" _
                    & ",t.BkgMethod,t.BkgTool,t.BkgAction" _
                    & ",t.RLOC,t.BkgDate, convert(varchar,t.DepDate,6) as DepDate, t.DOI as TvlDOI" _
                    & ",t.OriDossierNbr,t.OriInvNbr" _
                    & ",t.EmplID,t.Dossier,t.InvNo" _
                    & ",t.AgencyCode,t.FOP,t.CCNo,t.CCCode" _
                    & ",t.Location,t.Hierachy1,t.Hierachy2,t.Hierachy3" _
                    & ",t.Hierachy4,t.Hierachy5,t.HtlExist,t.CorpType,DocCode" _
                    & ",RequiredData,InvDate,h.*" _
                    & " from Go_Air a left join Go_Travel t" _
                    & " on a.TravelId=t.Recid" _
                    & " left join airline c on a.carrier=c.alcode" _
                    & " left join go_hotel h on t.Recid=h.travelid" _
                    & " where (t.DOI between '" & strStartDate & "' and '" & strEndDate _
                    & "')  and Voided=0 and a.Currency='" & strCur _
                    & "' and t.Status='OK' and CustId in (" & strCustIds & ")" _
                    & " order by a.TravelId"
        '& " and BkgAction ='AB' order by a.TravelId"

        cmdSql.CommandText = strQuerry
        drTravel = cmdSql.ExecuteReader

        If Not drTravel.HasRows Then
            MsgBox("No data for selected dates!")
            drTravel.Close()
            Return False
        End If

        If Not ValidateData(drTravel) Then
            MsgBox("Unable to export Data")
            Me.Dispose()
            Return False
        End If

        drTravel.Close()
        drTravel = cmdSql.ExecuteReader

        Do While drTravel.Read
            Dim arrCarriers() As String
            Dim arrDepApts() As String
            Dim arrArrApts() As String
            Dim arrDepDates() As String
            Dim arrArrDates() As String
            Dim arrFBs() As String
            Dim arrFltNbrs() As String
            'Dim arrUIDS() As String
            'Dim decPaidFare As Decimal
            'Dim decRefFare As Decimal
            'Dim decLowFare As Decimal
            Dim colRqData As Collection
            Dim strClsFareDesc As String = ""
            Dim strCompanyName As String = ""
            Dim strCostCenter As String = ""
            Dim strDivision As String = ""
            Dim strEmpId As String = ""
            Dim strMsCode As String = ""
            Dim strRsCode As String = ""
            Dim strArrangerName As String
            Dim strTripType As String
            Dim decLowFare As Decimal
            Dim decHighFare As Decimal

            arrCarriers = Split(drTravel("ALs"), "_")
            arrDepApts = Split(drTravel("DepApts"), "_")
            arrArrApts = Split(drTravel("ArrApts"), "_")
            arrDepDates = Split(drTravel("DepDates"), "_")
            arrArrDates = Split(drTravel("DepDates"), "_") 'se sua sau
            arrFBs = Split(drTravel("FBs"), "+")
            arrFltNbrs = Split(drTravel("FltNbrs"), "_")

            colRqData = ConvertRequiredDataString2Collection(drTravel("RequiredData").ToString)

            strClsFareDesc = CType(colRqData("UDID32"), clsAvailableData).DataValue
            strCompanyName = CType(colRqData("UDID18"), clsAvailableData).DataValue
            strCostCenter = CType(colRqData("CC"), clsAvailableData).DataValue
            strDivision = CType(colRqData("UDID30"), clsAvailableData).DataValue

            strDivision = pobjTvcs.GetScalarAsString("select top 1 VAL from GO_MISC" _
                                                     & " where CAT = 'HunsmantDivision'" _
                                                     & " and Details='" & strDivision & "'")
            strEmpId = CType(colRqData("UDID27"), clsAvailableData).DataValue
            strMsCode = CType(colRqData("MS"), clsAvailableData).DataValue
            strRsCode = CType(colRqData("SC"), clsAvailableData).DataValue
            strArrangerName = CType(colRqData("UDID34"), clsAvailableData).DataValue
            decLowFare = CType(colRqData("LF"), clsAvailableData).DataValue
            decHighFare = CType(colRqData("RF"), clsAvailableData).DataValue
            Dim decPaidFare = drTravel("Fare")


            If colRqData.Contains("CO") Then
                strEmpId = CType(colRqData("CO"), clsAvailableData).DataValue
            End If

            strTripType = GetTripType4Hunstman(drTravel("DepApts"), drTravel("ArrApts"))
            With objWs

                .Range("A" & i).Value = Format(dtpToDate.Value, "MMMM")
                .Range("B" & i).Value = "Vietnam"
                .Range("C" & i).Value = "Ho Chi Minh City"
                .Range("D" & i).Value = drTravel("TravellerName")
                .Range("E" & i).Value = strEmpId
                .Range("F" & i).Value = strCompanyName
                .Range("G" & i).Value = strDivision
                .Range("H" & i).Value = strCostCenter
                .Range("I" & i).Value = strTripType
                .Range("J" & i).Value = GetRouting4Huntsman(arrDepApts, arrArrApts, arrCarriers)
                .Range("K" & i).Value = GetRoutingType4Hunstman(arrDepApts, arrArrApts, arrCarriers)
                .Range("L" & i).Value = arrDepApts(0)
                .Range("M" & i).Value = pobjTvcs.GetCityName(arrDepApts(0))
                .Range("N" & i).Value = pobjTvcs.GetCountryNameByAptCode(arrDepApts(0))
                If arrDepApts.Length = 1 Then
                    .Range("O" & i).Value = arrArrApts(0)
                    .Range("P" & i).Value = pobjTvcs.GetCityName(arrArrApts(0))
                    .Range("Q" & i).Value = pobjTvcs.GetCountryNameByAptCode(arrArrApts(0))
                Else
                    .Range("O" & i).Value = arrDepApts(arrDepApts.Length - 1)
                    .Range("P" & i).Value = pobjTvcs.GetCityName(arrDepApts(arrDepApts.Length - 1))
                    .Range("Q" & i).Value = pobjTvcs.GetCountryNameByAptCode(arrDepApts(arrDepApts.Length - 1))
                End If
                Select Case drTravel("BkgTool")
                    Case "AMA"
                        .Range("R" & i).Value = "Amadeus"
                    Case "SAB"
                        .Range("R" & i).Value = "Sabre"
                    Case Else
                        .Range("R" & i).Value = "Manual"
                End Select
                .Range("S" & i).Value = "Offline"
                .Range("T" & i).Value = drTravel("Fare") - drTravel("TotalTax")
                .Range("U" & i).Value = drTravel("TotalTax")
                If drTravel("SRV") = "R" Then
                    .Range("T" & i).Value = .Range("T" & i).Value * -1
                    .Range("U" & i).Value = .Range("U" & i).Value * -1
                End If

                .Range("V" & i).Value = drTravel("TrxFee")
                .Range("X" & i).FormulaR1C1 = "=(RC[-4]+RC[-3]+RC[-1])"
                .Range("Y" & i).FormulaR1C1 = "=(RC[-5]+RC[-4]+RC[-3]+RC[-2])"
                .Range("Z" & i).Value = decRoe
                .Range("AA" & i).FormulaR1C1 = "=(RC[-3]/RC[-1])"
                .Range("AB" & i).FormulaR1C1 = "=(RC[-3]/RC[-2])"

                If decPaidFare <= decLowFare Then
                    'If drTravel("OriALTKNO") = "" Then
                    '    .Range("AC" & i).Value = decHighFare
                    '    .Range("AC" & i).FormulaR1C1 = "=(RC[-1]-RC[-6])"
                    '    .Range("AE" & i).FormulaR1C1 = "=(RC[-1]/(RC[-2])*100)"
                    '    '.Range("Z" & i).Value = (.Range("Y" & i).Value / .Range("X" & i).Value) * 100
                    'End If
                    .Range("AF" & i).Value = strRsCode
                Else
                    .Range("AG" & i).Value = decLowFare
                    .Range("AH" & i).FormulaR1C1 = "=((RC[-10]+RC[-11])-RC[-1])"
                    .Range("AI" & i).FormulaR1C1 = "=ROUND((RC[-1]/(RC[-12]+RC[-11])*100),2)"
                    .Range("AJ" & i).Value = strMsCode
                End If

                .Range("AL" & i).Value = pobjTvcs.GetCarName(drTravel("Carrier"))
                .Range("AM" & i).Value = drTravel("Carrier")
                .Range("AN" & i).Value = pobjTvcs.GetAlliance(drTravel("Carrier"))
                .Range("AQ" & i).Value = pobjTvcs.GetCos4Rtg(arrCarriers, drTravel("RBD"))
                .Range("AR" & i).Value = ConverRbd4Hunstmant(drTravel("RBD"))
                .Range("AS" & i).Value = strClsFareDesc
                .Range("AT" & i).Value = .Range("AQ" & i).Value
                .Range("AU" & i).Value = drTravel("TKNO")
                .Range("AV" & i).Value = GetFopNameByCode(drTravel("FOP"))
                .Range("AW" & i).Value = drTravel("InvNo")
                .Range("AX" & i).Value = drTravel("BkgDate")
                .Range("AY" & i).Value = drTravel("DOI")
                .Range("AZ" & i).Value = CDate(drTravel("DepDate")).Date
                If arrDepApts.Length = 1 Then
                    .Range("BA" & i).Value = drTravel("DepDate")
                    .Range("BB" & i).Value = 1
                Else
                    .Range("BA" & i).Value = AddFutureYear(drTravel("DOI"), arrArrDates(arrArrDates.Length - 1))
                    .Range("BB" & i).Value = DateDiff(DateInterval.Day, .Range("AZ" & i).Value, .Range("BA" & i).Value) + 1

                End If

                .Range("BC" & i).Value = DateDiff(DateInterval.Day, .Range("AY" & i).Value, .Range("AZ" & i).Value)
                .Range("BD" & i).Value = DateDiff(DateInterval.Day, .Range("AX" & i).Value, .Range("AZ" & i).Value)
                .Range("BE" & i).Value = 1
                If drTravel("SRV") = "R" Then
                    .Range("BF" & i).Value = "Refund"
                ElseIf drTravel("OriALTKNO") = "" Then
                    .Range("BF" & i).Value = "Original"
                Else
                    .Range("BF" & i).Value = "Reissue"
                End If
                .Range("BG" & i).Value = strArrangerName
                'BO SUNG BH SAU
                .Range("BJ" & i).Value = strCur
            End With

            i = i + 1
        Loop
        drTravel.Close()

        objWs.Columns("A").HorizontalAlignment = HorizontalAlignment.Right
        objWs.Columns("AX:BA").NumberFormat = "dd/mm/yy"

        objWs.Range("A1:BI" & i - 1).Borders.LineStyle = XlsBorderType.BorderAll
        Return True
    End Function
    Private Function SumLine(objWs As Worksheet) As Boolean
        Dim i As Integer, j As Integer
        Dim intStartVnd As Integer
        Dim intEndVnd As Integer
        Dim strTotalColumns As String = "TUVWXY"

        With objWs
            intStartVnd = 2
            intEndVnd = objWs.Cells(objWs.Rows.Count, "A").End(XlDirection.xlUp).Row
            If intStartVnd > 0 Then
                .Range("S" & intEndVnd + 1).Value = "Grand Total in VND"
                For j = 0 To strTotalColumns.Length - 1

                    .Range(strTotalColumns.Chars(j) & intEndVnd + 1).Value = "=SUM(" & strTotalColumns.Chars(j).ToString _
                            & intStartVnd & ":" & strTotalColumns.Chars(j).ToString & intEndVnd & ")"
                    .Range(strTotalColumns.Chars(j).ToString & intEndVnd + 1).NumberFormat = "$#,##0.00"
                    .Range(strTotalColumns.Chars(j) & intEndVnd + 1).Borders.LineStyle = XlLineStyle.xlContinuous
                Next
            End If

            .Columns("AB").NumberFormat = "$#,##0.00"
            .Range("A:CB").EntireColumn.AutoFit()

        End With

        Return True
    End Function


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Dim arrCurrencies() As String = New String() {"VND"}
        Dim strCustIds As String = "18937,18939,19655,47950"
        Dim i As Integer, j As Integer
        Dim objXl As New Excel.Application
        Dim objWb As Workbook
        Dim objWs As New Worksheet
        Dim intStarLine As Integer
        Dim decRoe As Decimal

        objXl.Visible = True
        objWb = objXl.Workbooks.Add(My.Application.Info.DirectoryPath & "\2022 Huntsman Travel Report Template.xlsx")
        decRoe = objWb.Sheets("R.O.E.").Range("c16").value


        For i = 0 To arrCurrencies.Length - 1
            objWs = objWb.Sheets("VN")
            objWs.Activate()
            objWs.Range("A1").Select()
            intStarLine = objWs.Range("A" & 1024).End(XlDirection.xlUp).Row + 1
            Export(arrCurrencies(i), objWs, intStarLine, strCustIds, decRoe)
        Next
        SumLine(objWs)

        objXl.Quit()
        MsgBox("Done")
        Me.Dispose()
    End Sub
    Private Function ValidateDataHotelOnly(ByVal drExportData As SqlClient.SqlDataReader) As Boolean
        Dim objXl As New Excel.Application
        Dim objWb As Workbook
        Dim objWs As New Worksheet
        Dim i As Integer = 1
        Dim strCellPos As String
        Dim lstPnrElementTypes As New List(Of String)


        lstPnrElementTypes.Add("HTL")
        objXl.Visible = True
        objWb = objXl.Workbooks.Add
        objWs = objWb.ActiveSheet

        With objWs
            .Cells(i, 1) = "CAR"
            .Cells(i, 2) = "TicketNumber"
            .Cells(i, 3) = "PNR"
            .Cells(i, 4) = "TravellerName"
            .Cells(i, 5) = "Error"
            .Cells(i, 6) = "BkgTool"
            .Cells(i, 7) = "TravelId"

            Do While drExportData.Read()
                Dim colAvaiData As New Collection
                Dim colRequiredData As New Collection
                Dim colDataErr As New Collection
                Dim strErrMsg As String

                'Check RequiredData

                Dim intByPassId As Integer
                intByPassId = pobjTvcs.GetBypassId(drExportData("TravelId"), "REQUIRED_DATA")
                If intByPassId = 0 Then
                    colAvaiData = ConvertRequiredDataString2Collection(drExportData("RequiredData"))
                    colRequiredData = pobjTvcs.GetDataRequirement(8085, "M", lstPnrElementTypes, True)
                    colDataErr = pobjTvcs.CheckData(colRequiredData, colAvaiData, 8085, drExportData("TvlDOI"))
                    strErrMsg = pobjTvcs.DataErr2Msg(colDataErr)
                    If strErrMsg <> "" Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                                , drExportData("TravellerName") _
                                                , Replace(strErrMsg, vbCrLf, vbLf) _
                                                , drExportData("BkgTool"), drExportData("TravelId")}
                        strCellPos = "A" & i & ":" & "H" & i
                        objWs.Range(strCellPos).Value = arrLine
                        objWs.Range(strCellPos).RowHeight = strErrMsg.Split(vbLf).Length * 50
                    End If
                End If



                'Kiem tra thong tin Khach san

                If drExportData("HtlExist") AndAlso drExportData("HotelName") Is DBNull.Value Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "Missing Hotel Info" _
                                             , drExportData("BkgTool") _
                                             , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                             , drExportData("TravelId")}
                    strCellPos = "A" & i & ":" & "H" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If
                If Not drExportData("HotelName") Is DBNull.Value Then

                    If drExportData("ReferenceRate") < drExportData("RatePerRoomPerNight") Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), "Reference Rate is lower than Paid Rate" _
                                                , drExportData("BkgTool"), drExportData("TravelId")}
                        strCellPos = "A" & i & ":" & "H" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If

                End If

            Loop
            .Columns("A:H").EntireColumn.AutoFit()
            .Cells.Select()
            .Cells.EntireRow.AutoFit()
        End With

        If i > 1 Then
            Return False
        Else
            objWb.Close(False)
            objXl.Quit()
            Return True
        End If
    End Function
    Private Function ValidateData(ByVal drExportData As SqlClient.SqlDataReader) As Boolean
        Dim objXl As New Excel.Application
        Dim objWb As Workbook
        Dim objWs As New Worksheet
        Dim i As Integer = 1
        Dim j As Integer
        Dim strCellPos As String
        Dim arrAirFields() As String
        Dim arrAirErrors() As String
        Dim arrDepDates() As String
        Dim arrCars() As String
        Dim lstPnrDataType As New List(Of String)

        lstPnrDataType.Add("AIR")
        arrAirFields = New String() {"DepApts", "ArrApts", "ALs", "RBD", "DepDates" _
                                    , "ArrDates", "FltNbrs" _
                                    , "ETD", "ETA", "FBs", "Rloc", "InvNo"}
        arrAirErrors = New String() {"Missing Departure Airports", "Missing Arrival Airports" _
                                    , "Missing Airlines for segments", "Missing RBDs for segments" _
                                    , "Missing DepartureDates for segments", "Missing ArrivalDates for segments" _
                                    , "Missing Flight Numbers for segments" _
                                    , "Missing Departure times for segments", "Missing Arrival Times for segments" _
                                    , "Missing Fare Basis for segments", "Missing Record Locator", "Missing Invoice Nbr"}
        objXl.Visible = True
        objWb = objXl.Workbooks.Add
        objWs = objWb.ActiveSheet

        With objWs
            .Cells(i, 1) = "CAR"
            .Cells(i, 2) = "TicketNumber"
            .Cells(i, 3) = "PNR"
            .Cells(i, 4) = "TravellerName"
            .Cells(i, 5) = "Error"
            .Cells(i, 6) = "BkgTool"
            .Cells(i, 7) = "TravelId"

            Do While drExportData.Read()
                Dim blnMissingDepDate As Boolean
                Dim colAvaiData As New Collection
                Dim colRequiredData As New Collection
                Dim colDataErr As New Collection
                Dim strErrMsg As String

                For j = 0 To UBound(arrAirFields)
                    If Trim(drExportData(arrAirFields(j))) = "" Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("Carrier"), drExportData("TKNO") _
                                                , drExportData("RLOC"), drExportData("TravellerName") _
                                                , arrAirErrors(j), drExportData("BkgTool") _
                                                , drExportData("TravelId")}
                        strCellPos = "A" & i & ":" & "H" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If
                Next


                'arrSOs = Split(drExportData("SO"), "_")
                arrCars = Split(drExportData("ALS"), "_")
                arrDepDates = Split(drExportData("DepDates"), "_")
                If UBound(arrDepDates) < UBound(arrCars) Then
                    blnMissingDepDate = True
                Else
                    For j = 0 To UBound(arrCars)
                        If arrDepDates(j) = "" And arrCars(j) <> "//" Then
                            blnMissingDepDate = True
                        End If
                    Next
                End If
                If blnMissingDepDate Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName") _
                                            , "Missing Departure Date for a segment" _
                                            , drExportData("BkgTool") _
                                            , drExportData("TravelId")}
                    strCellPos = "A" & i & ":" & "H" & i
                    objWs.Range(strCellPos).Value = arrLine
                    blnMissingDepDate = False
                End If

                If Len(drExportData("RBD")) < arrCars.Length Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "Missing RBD for a segment" _
                                            , drExportData("BkgTool"), drExportData("TravelId")}
                    strCellPos = "A" & i & ":" & "H" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                If drExportData("SRV") = "R" _
                    AndAlso Split(drExportData("FBs"), "+").Length <> arrCars.Length Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "Missing Fare Basis for a segment for Refund Ticket" _
                                            , drExportData("BkgTool"), drExportData("TravelId")}
                    strCellPos = "A" & i & ":" & "H" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                'Check RequiredData

                Dim intByPassId As Integer
                intByPassId = pobjTvcs.GetBypassId(drExportData("TravelId"), "REQUIRED_DATA")
                If intByPassId = 0 Then
                    colAvaiData = ConvertRequiredDataString2Collection(drExportData("RequiredData"))
                    colRequiredData = pobjTvcs.GetDataRequirement(18937, "M", lstPnrDataType)
                    colDataErr = pobjTvcs.CheckData(colRequiredData, colAvaiData, 18937, drExportData("DOI"))
                    strErrMsg = pobjTvcs.DataErr2Msg(colDataErr)
                    If strErrMsg <> "" Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                                , drExportData("TravellerName") _
                                                , Replace(strErrMsg, vbCrLf, vbLf) _
                                                , drExportData("BkgTool"), drExportData("TravelId")}
                        strCellPos = "A" & i & ":" & "H" & i
                        objWs.Range(strCellPos).Value = arrLine
                        If strErrMsg.Split(vbLf).Length * 50 > 409 Then
                            objWs.Range(strCellPos).RowHeight = 409
                        Else
                            objWs.Range(strCellPos).RowHeight = strErrMsg.Split(vbLf).Length * 50
                        End If

                    End If

                    If drExportData("SRV") = "S" AndAlso drExportData("oriALTKNO") <> "" _
                        AndAlso CType(colAvaiData("SC"), clsAvailableData).DataValue.Trim <> "TICKET EXCHANGE" Then
                        Dim arrLine() As String
                        i = i + 1

                        strErrMsg = "INVALID REALISED SAVING CODE FOR EXCHANGED TICKET"
                        arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                                , drExportData("TravellerName") _
                                                , Replace(strErrMsg, vbCrLf, vbLf) _
                                                , drExportData("BkgTool"), drExportData("TravelId")}
                        strCellPos = "A" & i & ":" & "H" & i
                        objWs.Range(strCellPos).Value = arrLine
                        objWs.Range(strCellPos).RowHeight = strErrMsg.Split(vbLf).Length * 50
                    End If
                End If


                'Check fare
                Dim decPaidFare As Decimal = drExportData("Fare")
                Dim objRefFare As New clsAvailableData
                Dim objLowFare As New clsAvailableData

                If colAvaiData.Contains("RF") Then
                    objRefFare = colAvaiData("RF")
                    If objRefFare.DataValue < decPaidFare Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), "Reference Fare is lower than Paid Fare" _
                                                , drExportData("BkgTool"), drExportData("TravelId")}
                        strCellPos = "A" & i & ":" & "H" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If
                End If
                If colAvaiData.Contains("LF") Then
                    objLowFare = colAvaiData("LF")
                    If decPaidFare < objLowFare.DataValue Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                                , drExportData("TravellerName") _
                                                , "Paid Fare is lower than Lowest Fare" _
                                                , drExportData("BkgTool") _
                                                , drExportData("TravelId")}
                        strCellPos = "A" & i & ":" & "H" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If
                End If

                If objLowFare.DataValue < decPaidFare AndAlso decPaidFare < objRefFare.DataValue Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "Reference Fare/Paid Fare/Low Fare are NOT compatible" _
                                            , drExportData("BkgTool"), drExportData("TravelId")}
                    strCellPos = "A" & i & ":" & "H" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                'Kiem tra thong tin Khach san
                'arrHtlFields = New String() {"RsCode", "MsCode"}
                'arrHtlErrors = New String() {"Missing RealisedSaving Code", "Missing MissSaving Code"}

                If drExportData("HtlExist") AndAlso drExportData("HotelName") Is DBNull.Value Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "Missing Hotel Info" _
                                             , drExportData("BkgTool") _
                                             , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                             , drExportData("TravelId")}
                    strCellPos = "A" & i & ":" & "H" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If
                If Not drExportData("HotelName") Is DBNull.Value Then

                    If drExportData("ReferenceRate") < drExportData("RatePerRoomPerNight") Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), "Reference Rate is lower than Paid Rate" _
                                                , drExportData("BkgTool"), drExportData("TravelId")}
                        strCellPos = "A" & i & ":" & "H" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If


                    'For j = 0 To UBound(arrHtlFields)
                    '    If Trim(drExportData(arrHtlFields(j))) = "" Then
                    '        i = i + 1
                    '        Dim arrLine() As String
                    '        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate") _
                    '                                , drExportData("RLOC"), drExportData("TravellerName") _
                    '                                , arrHtlErrors(j), drExportData("BkgTool") _
                    '                                , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                    '                                , drExportData("TravelId")}
                    '        strCellPos = "A" & i & ":" & "H" & i
                    '        objWs.Range(strCellPos).Value = arrLine
                    '    End If
                    'Next
                End If

            Loop
            .Columns("A:H").EntireColumn.AutoFit()
            .Cells.Select()
            .Cells.EntireRow.AutoFit()
        End With

        If i > 1 Then
            Return False
        Else
            objWb.Close(False)
            objXl.Quit()
            Return True
        End If
    End Function
    Private Function ExportHotelOnly() As Boolean
        Dim drTravel As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        Dim strEndDate As String
        Dim strStartDate As String
        Dim strTravellerName As String = ""
        'Dim intTktCount As Integer
        Dim strQuerry As String


        strStartDate = CreateFromDate(dtpFromDate.Value.Date)
        strEndDate = CreateToDate(dtpToDate.Value.Date)


        pobjTvcs.Connect2()
        cmdSql.Connection = pobjTvcs.Connection2
        strQuerry = "select h.*,t.TravellerName,t.TripPurpose" _
                    & ",t.BkgMethod,t.BkgTool,t.BkgAction" _
                    & ",t.RLOC,t.BkgDate,t.DepDate,t.DOI as TvlDOI" _
                    & ",t.OriDossierNbr,t.OriInvNbr" _
                    & ",t.EmplID,t.Dossier,t.InvNo" _
                    & ",t.AgencyCode,t.FOP,t.CCNo,t.CCCode" _
                    & ",t.Location,t.Hierachy1,t.Hierachy2,t.Hierachy3" _
                    & ",t.Hierachy4,t.Hierachy5,t.HtlExist,t.CorpType" _
                    & ",RequiredData,InvDate,h.*" _
                    & " from Go_Hotel h left join Go_Travel t" _
                    & " on h.TravelId=t.Recid" _
                    & " where (t.DOI between '" & strStartDate & "' and '" & strEndDate _
                    & "')  and t.RecId not in (Select TravelId from go_air) " _
                    & " and Status='OK' and CustId=8085 order by h.TravelId"


        cmdSql.CommandText = strQuerry
        drTravel = cmdSql.ExecuteReader

        If Not ValidateDataHotelOnly(drTravel) Then
            MsgBox("Unable to export Data")
            Me.Dispose()
            Exit Function
        End If

        If Not drTravel.HasRows Then
            MsgBox("No data for selected dates!")
            drTravel.Close()
            Exit Function
        End If
        drTravel.Close()
        drTravel = cmdSql.ExecuteReader


        Dim strLogFile As String = "X:\CWT\iBank\"
        Dim strFileTCTRIPS As String = strLogFile & "TCTRIPS.csv"
        Dim strFileTCHOTEL As String = strLogFile & "TCHOTEL.csv"
        Dim strFileTCSERVICES As String = strLogFile & "TCSERVICES.csv"
        Dim strFileTCUDIDS As String = strLogFile & "TCUDIDS.csv"

        Dim objFileTCTRIPS As New System.IO.StreamWriter(strFileTCTRIPS, True)
        Dim objFileTCHOTEL As New System.IO.StreamWriter(strFileTCHOTEL, True)
        Dim objFileTCSERVICES As New System.IO.StreamWriter(strFileTCSERVICES, True)
        Dim objFileTCUDIDS As New System.IO.StreamWriter(strFileTCUDIDS, True)
        Dim strAcctNbr As String = "8085"

        Do While drTravel.Read
            Dim strTcTrips As String
            Dim strTcHotel As String
            Dim strTcServices As String
            Dim objName As New clsNameiBank
            Dim colRqData As Collection
            Dim strMsCode As String = ""
            Dim strDomIntl As String
            Dim strTransType As String = "S"
            Dim strPlusMinus As String = "1"
            Dim strCostCenter As String = ""
            Dim strCompanyCode As String = ""
            Dim strExchange As String = "F"
            Dim i As Int16
            Dim arrUIDS() As String

            objName.ParseFullName(drTravel("TravellerName"))
            colRqData = ConvertRequiredDataString2Collection(drTravel("RequiredData").ToString)

            If colRqData.Contains("MS") Then
                strMsCode = CType(colRqData("MS"), clsAvailableData).DataValue
            End If


            If colRqData.Contains("CC") Then
                strCostCenter = CType(colRqData("CC"), clsAvailableData).DataValue
            End If

            If colRqData.Contains("CO") Then
                strCompanyCode = CType(colRqData("CO"), clsAvailableData).DataValue
            End If

            strDomIntl = IdentifyDomInlt4iBank(drTravel("CityCode"), drTravel("CityCode"))

            Select Case drTravel("SRV")
                Case "S"
                    strTransType = "I"
                    strPlusMinus = "1"
                Case "R"
                    strTransType = "C"
                    strPlusMinus = "-1"
                Case "V"
                    strTransType = "V"
                    strPlusMinus = "1"
            End Select

            strExchange = "F"

            strTcTrips = drTravel("TravelId") & "," & drTravel("InvNo") _
                        & "," & Convert2iBankDate(drTravel("InvDate")) _
                        & "," & strAcctNbr & ",,,," & drTravel("BkgDate") _
                        & ",ZZ,," & objName.LastName & "," & objName.FirstName _
                        & ",0.00,,0.00," & strMsCode & ",0.00,,,,,," & drTravel("Rloc") & "," & strDomIntl _
                        & "," & strTransType & ",VN," & strCostCenter _
                        & "," & strCompanyCode & ",,," & strPlusMinus _
                        & ",,,,," & drTravel("CurCode") & "," & strExchange _
                        & ",,,,,,,,,A,,,"
            objFileTCTRIPS.WriteLine(strTcTrips)

            'TC HOTELS

            If Not drTravel("HotelName") Is DBNull.Value Then
                strTcHotel = drTravel("TravelId") & "," & drTravel("ChainCode") & "," & drTravel("HotelName") _
                    & "," & Mid(pobjTvcs.GetCityName(drTravel("CityCode")), 1, 24) _
                    & ",,," & Convert2iBankDate(drTravel("CheckInDate")) _
                    & "," & Convert2iBankDate(DateAdd(DateInterval.Day, drTravel("NbrOfNight"), drTravel("CheckInDate"))) _
                    & "," & drTravel("NbrOfNight") & ",1,,," & drTravel("RatePerRoomPerNight") & "," & drTravel("CurCode") _
                    & ",1,,,,,,,," & drTravel("ReferenceRate") & ",V,,,," & pobjTvcs.GetCountryCode(drTravel("CityCode")) _
                    & ",,,"
                objFileTCHOTEL.WriteLine(strTcHotel)
            End If

            'TC SERVICES
            strTcServices = drTravel("TravelId") & ",TSF,SERVICE FEE" _
                            & "," & FormatNumber(drTravel("TrxFee"), 2, , , TriState.False) & "," & drTravel("CurCode") _
                            & "," & Convert2iBankDate(drTravel("TvlDOI")) _
                            & ",,,,,," & strTransType & ",,,"
            objFileTCSERVICES.WriteLine(strTcServices)

            'TCUIDS
            arrUIDS = Split(drTravel("RequiredData"), "|")

            For i = 0 To arrUIDS.Length - 1

                If arrUIDS(i).StartsWith("UDID") Then
                    Dim strTcUids As String
                    Dim arrBreak() As String
                    arrBreak = Split(arrUIDS(i), "/", 2)
                    strTcUids = drTravel("TravelId") & "," _
                                & Mid(arrBreak(0), 5) & "," & arrBreak(1)
                    objFileTCUDIDS.WriteLine(strTcUids)
                End If
            Next
        Loop

        objFileTCTRIPS.Close()
        objFileTCHOTEL.Close()
        objFileTCSERVICES.Close()
        objFileTCUDIDS.Close()

        Return True
    End Function

    Private Sub frmExportHunstman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFromDate.Value = DateSerial(Now.Year, Now.Month, 1).AddMonths(-1)
        dtpToDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1)
    End Sub
End Class