Imports System.IO
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class frmiBankExport
    Private mstrLogDir As String = "X:\RAS2K7\iBank\"
    Private mstrFileTCACCTS As String = mstrLogDir & "TCACCTS.csv"
    Private mstrFileTCTRIPS As String = mstrLogDir & "TCTRIPS.csv"
    Private mstrFileTCLEGS As String = mstrLogDir & "TCLEGS.csv"
    Private mstrFileTCHOTEL As String = mstrLogDir & "TCHOTEL.csv"
    Private mstrFileTCCARS As String = mstrLogDir & "TCCARS.csv"
    Private mstrFileTCSERVICES As String = mstrLogDir & "TCSERVICES.csv"
    Private mstrFileTCUDIDS As String = mstrLogDir & "TCUDIDS.csv"
    Private mobjFileTCACCTS As New System.IO.StreamWriter(mstrFileTCACCTS, False)
    Private mobjFileTCTRIPS As New System.IO.StreamWriter(mstrFileTCTRIPS, False)
    Private mobjFileTCLEGS As New System.IO.StreamWriter(mstrFileTCLEGS, False)
    Private mobjFileTCHOTEL As New System.IO.StreamWriter(mstrFileTCHOTEL, False)
    Private mobjFileTCCARS As New System.IO.StreamWriter(mstrFileTCCARS, False)
    Private mobjFileTCSERVICES As New System.IO.StreamWriter(mstrFileTCSERVICES, False)
    Private mobjFileTCUDIDS As New System.IO.StreamWriter(mstrFileTCUDIDS, False)

    Private mlstErrors As New List(Of clsExportError)
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub
    Private Function ExportAirWzNonAir() As Boolean
        Dim tblTravel As System.Data.DataTable
        Dim cmdSql As New SqlClient.SqlCommand
        Dim strEndDate As String
        Dim strStartDate As String
        Dim strTravellerName As String = ""
        Dim strQuerry As String


        strStartDate = CreateFromDate(dtpFromDate.Value.Date)
        strEndDate = CreateToDate(dtpToDate.Value.Date)

        strQuerry = "select t.CustId, a.*,t.TravellerName,t.TripPurpose" _
                    & ",t.BkgMethod,t.BkgTool,t.BkgAction" _
                    & ",t.RLOC,t.BkgDate,t.DepDate,t.DOI as TvlDOI" _
                    & ",t.OriDossierNbr,t.OriInvNbr" _
                    & ",t.EmplID,t.Dossier,t.InvNo" _
                    & ",t.AgencyCode,t.FOP,t.CCNo,t.CCCode" _
                    & ",t.Location,t.Hierachy1,t.Hierachy2,t.Hierachy3" _
                    & ",t.Hierachy4,t.Hierachy5,t.HtlExist,t.CorpType,tkt.TKNO as RasTkno" _
                    & ",RequiredData,InvDate,tkt.DOF" _
                    & " from Go_Air a left join Go_Travel t" _
                    & " on a.TravelId=t.Recid" _
                    & " left join RAS12.dbo.TKT tkt on tkt.RecID=a.Tkid" _
                    & " where (t.DOI between '" & strStartDate & "' and '" & strEndDate _
                    & "')  and Voided=0 " _
                    & " and t.Status='OK' "

        If Not chkAll.Checked Then
            strQuerry = strQuerry & " and Exportable='true' "
        End If

        strQuerry = strQuerry & " and CustId in (90359,90360) order by a.TravelId"

        tblTravel = pobjTvcs.GetDataTable(strQuerry)

        If tblTravel.Rows.Count = 0 Then
            MsgBox("No Ticket data for selected dates!")
            Return False
        End If

        If Not ValidateTravelData(tblTravel) Then
            MsgBox("Unable to export Data")
            Me.Dispose()
            Return False
        End If


        Dim intTravelId As Integer
        Dim strTkno As String = ""
        Dim strCcUsed As String

        mobjFileTCACCTS.WriteLine("90359,HOGAN HCM")
        mobjFileTCACCTS.WriteLine("90360,HOGAN HN")

        For Each objRow As DataRow In tblTravel.Rows
            Dim strTcTrips As String
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
            Dim arrCarriers() As String
            Dim arrDepApts() As String
            Dim arrArrApts() As String
            Dim arrDepDates() As String
            Dim arrArrDates() As String
            Dim arrFBs() As String
            Dim arrFltNbrs() As String
            Dim i As Int16
            Dim arrUIDS() As String
            Dim decPaidFare As Decimal
            Dim decRefFare As Decimal
            Dim decLowFare As Decimal
            Dim decTax As Decimal

            If objRow("TKNO") <> strTkno Then
                objName.ParseFullName(objRow("TravellerName"))

                colRqData = ConvertRequiredDataString2Collection(objRow("RequiredData").ToString)

                If colRqData.Contains("MS") Then
                    strMsCode = CType(colRqData("MS"), clsAvailableData).DataValue
                End If


                If colRqData.Contains("CC") Then
                    strCostCenter = CType(colRqData("CC"), clsAvailableData).DataValue
                End If

                If colRqData.Contains("CO") Then
                    strCompanyCode = CType(colRqData("CO"), clsAvailableData).DataValue
                End If

                strDomIntl = IdentifyDomInlt4iBank(objRow("DepApts"), objRow("ArrApts"))

                Select Case objRow("SRV")
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
                decPaidFare = objRow("Fare") * strPlusMinus
                decRefFare = objRow("RefFare") * strPlusMinus
                decLowFare = objRow("LowestFare") * strPlusMinus
                decTax = objRow("TotalTax") * strPlusMinus

                If objRow("OriALTKNO") <> "" Then
                    strExchange = "T"
                Else
                    strExchange = "F"
                End If

                strCcUsed = IIf(objRow("FOP") = "CC", "Y", "N")

                strTcTrips = objRow("TravelId") & "," & objRow("InvNo") _
                            & "," & Convert2iBankDate(objRow("InvDate")) _
                            & "," & objRow("CustId") & ",,,," & Convert2iBankDate(objRow("BkgDate")) _
                            & "," & objRow("Carrier") & "," & objRow("RasTKNO") _
                            & "," & objName.LastName & "," & objName.FirstName _
                            & "," & FormatNumber(decRefFare, 2, , , TriState.False) _
                            & ",0," & FormatNumber(decLowFare, 2, , , TriState.False) _
                            & "," & strMsCode & "," & FormatNumber(decPaidFare, 2, , , TriState.False) _
                            & "," & FormatNumber(decPaidFare - decTax, 2, , , TriState.False) _
                            & "," & FormatNumber(decTax, 2, , , TriState.False) _
                            & "," & FormatNumber(objRow("TrxFee"), 2, , , TriState.False) _
                            & "," & strCcUsed & ",," & objRow("Rloc") & "," & strDomIntl _
                            & "," & strTransType & ",,,," & Convert2iBankDate(objRow("DOF")) _
                            & ",," & strPlusMinus & ",,,," & objRow("Currency") & "," & strExchange _
                            & "," & objRow("OriALTKNO") & ",,,,,,,,A,,,Agent"
                mobjFileTCTRIPS.WriteLine(strTcTrips)


                'TCLEGS
                arrCarriers = Split(objRow("ALs"), "_")
                arrDepApts = Split(objRow("DepApts"), "_")
                arrArrApts = Split(objRow("ArrApts"), "_")
                arrDepDates = Split(objRow("DepDates"), "_")
                arrArrDates = Split(objRow("DepDates"), "_") 'se sua sau
                arrFBs = Split(objRow("FBs"), "+")
                arrFltNbrs = Split(objRow("FltNbrs"), "_")

                For i = 0 To arrCarriers.Length - 1
                    Dim strTcLegs As String
                    If arrCarriers(i) <> "//" And arrCarriers(i) <> "" Then
                        strTcLegs = objRow("TravelId") & "," & arrCarriers(i) _
                                & "," & arrDepApts(i) & "," & arrArrApts(i) _
                                & "," & ConvertGdsDate2iBankDate(arrDepDates(i), DateTime2Text(objRow("DOI"))) _
                                & ",," & ConvertGdsDate2iBankDate(arrArrDates(i), DateTime2Text(objRow("DOI"))) _
                                & ",," & objRow("RBD").ToString.Chars(i) & "," & arrFBs(i) _
                                & "," & arrFltNbrs(i) & ",," & i + 1 & ",,,,,A," & strPlusMinus & "," & strDomIntl & ",,,,"
                        mobjFileTCLEGS.WriteLine(strTcLegs)
                    End If
                Next
                'TC SERVICES
                strTcServices = objRow("TravelId") & ",TSF,SERVICE FEE" _
                            & "," & FormatNumber(objRow("TrxFee"), 2, , , TriState.False) & "," & objRow("Currency") _
                            & "," & Convert2iBankDate(objRow("DOI")) _
                            & ",,,,,," & strTransType & ",,,"
                mobjFileTCSERVICES.WriteLine(strTcServices)

                strTkno = objRow("TKNO")
            End If
            If intTravelId <> objRow("TravelId") Then
                intTravelId = objRow("TravelId")
                WriteHotelFile(intTravelId, strTransType, mobjFileTCHOTEL)
                WriteMiscFile(intTravelId, strTransType, mobjFileTCSERVICES)
            End If


            'TCUIDS
            arrUIDS = Split(objRow("RequiredData"), "|")

            For i = 0 To arrUIDS.Length - 1

                Dim strTcUids As String
                Dim arrBreak() As String
                Dim intRef As Integer

                arrBreak = Split(arrUIDS(i), "/", 2)
                intRef = GetRefByDataCode4RM(arrBreak(0))
                If intRef <> 0 Then
                    strTcUids = objRow("TravelId") & "," _
                                & intRef & "," & arrBreak(1)
                    mobjFileTCUDIDS.WriteLine(strTcUids)
                End If
            Next

        Next

    End Function
    Private Function WriteHotelFile(intTravelId As Integer, strTransType As String _
                                    , ByRef mobjFileTCHOTEL As StreamWriter) As Boolean
        'TC HOTELS
        Dim strTcHotel As String
        'Dim strTcServices As String
        Dim tblHotels As Data.DataTable = GetDataTable("select * from cwt.dbo.go_hotel where travelid=" & intTravelId)

        For Each objRow As DataRow In tblHotels.Rows
            strTcHotel = objRow("TravelId") & "," & objRow("ChainCode") & "," & objRow("HotelName") _
                & "," & Mid(pobjTvcs.GetCityName(objRow("CityCode")), 1, 24) _
                & ",,," & Convert2iBankDate(objRow("CheckInDate")) _
                & "," & Convert2iBankDate(DateAdd(DateInterval.Day, objRow("NbrOfNight"), objRow("CheckInDate"))) _
                & "," & objRow("NbrOfNight") & ",1,,," & objRow("RatePerRoomPerNight") & "," & objRow("CurCode") _
                & ",1,,,,,,,," & objRow("ReferenceRate") & ",V,,,," & pobjTvcs.GetCountryCode(objRow("CityCode")) _
                & ",,,"
            mobjFileTCHOTEL.WriteLine(strTcHotel)
            ''TC SERVICES
            'If Not IsDBNull(objRow("TrxFeeVnd")) AndAlso objRow("TrxFeeVnd") <> 0 Then
            '    strTcServices = objRow("TravelId") & ",TSF,SERVICE FEE" _
            '                & "," & FormatNumber(objRow("TrxFee"), 2, , , TriState.False) & "," & objRow("Currency") _
            '                & "," & Convert2iBankDate(objRow("DOI")) _
            '                & ",,,,,," & strTransType & ",,,"
            '    mobjFileTCSERVICES.WriteLine(strTcServices)
            'End If
        Next
        Return True

    End Function
    Private Function WriteCarFile(intTravelId As Integer, strTransType As String _
                                    , ByRef mobjFileTCHOTEL As StreamWriter _
                                    , ByRef mobjFileTCSERVICES As StreamWriter) As Boolean
        'TC HOTELS
        Dim strTcHotel As String
        Dim strTcServices As String
        Dim tblHotels As Data.DataTable = GetDataTable("select * from cwt.dbo.go_hotel where travelid=" & intTravelId)

        For Each objRow As DataRow In tblHotels.Rows
            strTcHotel = objRow("TravelId") & "," & objRow("ChainCode") & "," & objRow("HotelName") _
                & "," & Mid(pobjTvcs.GetCityName(objRow("CityCode")), 1, 24) _
                & ",,," & Convert2iBankDate(objRow("CheckInDate")) _
                & "," & Convert2iBankDate(DateAdd(DateInterval.Day, objRow("NbrOfNight"), objRow("CheckInDate"))) _
                & "," & objRow("NbrOfNight") & ",1,,," & objRow("RatePerRoomPerNight") & "," & objRow("CurCode") _
                & ",1,,,,,,,," & objRow("ReferenceRate") & ",V,,,," & pobjTvcs.GetCountryCode(objRow("CityCode")) _
                & ",,,"
            mobjFileTCHOTEL.WriteLine(strTcHotel)
            'TC SERVICES
            If Not IsDBNull(objRow("TrxFeeVnd")) AndAlso objRow("TrxFeeVnd") <> 0 Then
                strTcServices = objRow("TravelId") & ",TSF,SERVICE FEE" _
                            & "," & FormatNumber(objRow("TrxFee"), 2, , , TriState.False) & "," & objRow("Currency") _
                            & "," & Convert2iBankDate(objRow("DOI")) _
                            & ",,,,,," & strTransType & ",,,"
                mobjFileTCSERVICES.WriteLine(strTcServices)
            End If
        Next
        Return True

    End Function
    Private Function WriteMiscFile(intTravelId As Integer, strTransType As String _
                                    , ByRef objFileTCSERVICES As StreamWriter) As Boolean
        'TC HOTELS        
        Dim strTcServices As String = ""
        Dim tblMiscs As Data.DataTable = GetDataTable("select m.*,t.DOI from cwt.dbo.go_MiscSvc m" _
                                                       & " left join cwt.dbo.GO_Travel t on m.TravelId=t.RecId" _
                                                      & " where travelid=" & intTravelId)

        For Each objRow As DataRow In tblMiscs.Rows
            'TC SERVICES
            If objRow("SvcType") = "MIS" Then
                strTcServices = objRow("TravelId") & ",TSF,SERVICE FEE" _
                            & "," & FormatNumber(objRow("TrxFee"), 2, , , TriState.False) _
                            & "," & objRow("Currency") _
                            & "," & Convert2iBankDate(objRow("DOI")) _
                            & ",,,,,," & strTransType & ",,,"
            Else
                strTcServices = objRow("TravelId") & ",OSF," & objRow("Brief") _
                            & "," & FormatNumber(objRow("TrxFee"), 2, , , TriState.False) _
                            & "," & objRow("Currency") & "," & Convert2iBankDate(objRow("DOI")) _
                            & ",,,,,," & strTransType & ",,,"
            End If
            objFileTCSERVICES.WriteLine(strTcServices)
        Next
        Return True

    End Function
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        mlstErrors.Clear()
        ExportAirWzNonAir()

        ExportHotelOnly()

        ExportMiscSvc()

        If mlstErrors.Count > 0 Then
            MsgBox("D? li?u có " & mlstErrors.Count & " l?i")
        End If
        mobjFileTCACCTS.Close()
        mobjFileTCTRIPS.Close()
        mobjFileTCLEGS.Close()
        mobjFileTCHOTEL.Close()
        mobjFileTCCARS.Close()
        mobjFileTCSERVICES.Close()
        mobjFileTCUDIDS.Close()

        'If Not ExportMiscSvc() Then
        '    MsgBox("Unable to export Car-only records!")
        'End If

        'MsgBox("Done")
        'Me.Dispose()
    End Sub
    Private Function ValidateMiscSvc(ByVal tblTravel As System.Data.DataTable) As Boolean
        Dim i As Integer = 1
        Dim j As Integer
        Dim strSrv As String

        Dim arrAirFields() As String
        Dim arrDepDates() As String
        Dim arrCars() As String
        Dim blnMissingDepDate As Boolean = False
        Dim lstPnrElementTypes As New List(Of String)


        lstPnrElementTypes.Add("AIR")

        arrAirFields = New String() {"DepApts", "ArrApts", "ALs", "RBD", "DepDates" _
                                    , "ArrDates", "FltNbrs" _
                                    , "ETD", "ETA", "FBs", "Rloc", "InvNo"}
        For Each objRow As DataRow In tblTravel.Rows
            Dim colAvaiData As New Collection
            Dim colRequiredData As New Collection
            Dim colDataErr As New Collection
            Dim strErrMsg As String

            If objRow("BkgAction") = "AB" Then
                strSrv = "S"
            Else
                strSrv = "R"
            End If
            'Ten khach phai co gach cheo
            If Not objRow("TravellerName").ToString.Contains("/") Then
                Dim objError As New clsExportError(objRow("DOI"), strSrv, objRow("TravelId") _
                                                   , objRow("Supplier"), objRow("ItemId") _
                                                       , "PaxName must have slash", objRow("SvcType") _
                                                       , objRow("Tcode"), objRow("Rloc"))
                mlstErrors.Add(objError)
            End If

            'Check RequiredData
            Dim intByPassId As Integer
            intByPassId = pobjTvcs.GetBypassId(objRow("TravelId"), "REQUIRED_DATA")
            If intByPassId = 0 Then
                colAvaiData = ConvertRequiredDataString2Collection(objRow("RequiredData"))
                colRequiredData = pobjTvcs.GetDataRequirement(86756, "M", lstPnrElementTypes, True)
                colDataErr = pobjTvcs.CheckData(colRequiredData, colAvaiData, 8085, objRow("DOI"))
                strErrMsg = pobjTvcs.DataErr2Msg(colDataErr)
                If strErrMsg <> "" Then
                    Dim objError As New clsExportError(objRow("DOI"), strSrv, objRow("TravelId") _
                                                       , objRow("Supplier"), objRow("ItemId") _
                                                       , CreateDataErrorMsg(strErrMsg), objRow("SvcType") _
                                                       , objRow("Tcode"), objRow("Rloc"))
                    mlstErrors.Add(objError)
                End If
            End If
        Next

        If mlstErrors.Count = 0 Then
            Return True
        Else
            Dim objXl As New Excel.Application
            Dim objWb As Workbook
            Dim objWs As New Worksheet
            objXl.Visible = True
            objWb = objXl.Workbooks.Add
            objWs = objWb.ActiveSheet
            With objWs
                .Cells(i, 1) = "DOI"
                .Cells(i, 2) = "SRV"
                .Cells(i, 3) = "TravelId"
                .Cells(i, 4) = "Supplier"
                .Cells(i, 5) = "Document/Item"
                .Cells(i, 6) = "Error"
                .Cells(i, 7) = "BkgTool"
                .Cells(i, 8) = "RLOC"

                For i = 0 To mlstErrors.Count - 1
                    .Range("A" & i + 2).Value = mlstErrors(i).DOI
                    .Range("B" & i + 2).Value = mlstErrors(i).SRV
                    .Range("C" & i + 2).Value = mlstErrors(i).TravelId
                    .Range("D" & i + 2).Value = mlstErrors(i).Supplier
                    .Range("E" & i + 2).Value = IIf(mlstErrors(i).Document <> "", mlstErrors(i).Document, mlstErrors(i).ItemId)
                    .Range("F" & i + 2).Value = mlstErrors(i).DataError
                    .Range("G" & i + 2).Value = mlstErrors(i).BkgTool
                    .Range("H" & i + 2).Value = mlstErrors(i).Rloc
                Next
                .Columns("A:H").EntireColumn.AutoFit()
                .Cells.Select()
                .Cells.EntireRow.AutoFit()
            End With
            Return False
        End If
    End Function
    Private Function ValidateDataHotelOnly(ByVal tblExportData As System.Data.DataTable) As Boolean
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

        'With objWs
        '    .Cells(i, 1) = "CAR"
        '    .Cells(i, 2) = "TicketNumber"
        '    .Cells(i, 3) = "PNR"
        '    .Cells(i, 4) = "TravellerName"
        '    .Cells(i, 5) = "Error"
        '    .Cells(i, 6) = "BkgTool"
        '    .Cells(i, 7) = "TravelId"

        '    Do While drExportData.Read()
        '        Dim colAvaiData As New Collection
        '        Dim colRequiredData As New Collection
        '        Dim colDataErr As New Collection
        '        Dim strErrMsg As String

        '        'Check RequiredData

        '        Dim intByPassId As Integer
        '        intByPassId = pobjTvcs.GetBypassId(drExportData("TravelId"), "REQUIRED_DATA")
        '        If intByPassId = 0 Then
        '            colAvaiData = ConvertRequiredDataString2Collection(drExportData("RequiredData"))
        '            colRequiredData = pobjTvcs.GetDataRequirement(8085, "M", lstPnrElementTypes, True)
        '            colDataErr = pobjTvcs.CheckData(colRequiredData, colAvaiData, 8085, drExportData("TvlDOI"))
        '            strErrMsg = pobjTvcs.DataErr2Msg(colDataErr)
        '            If strErrMsg <> "" Then
        '                i = i + 1
        '                Dim arrLine() As String
        '                arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
        '                                        , drExportData("TravellerName") _
        '                                        , Replace(strErrMsg, vbCrLf, vbLf) _
        '                                        , drExportData("BkgTool"), drExportData("TravelId")}
        '                strCellPos = "A" & i & ":" & "H" & i
        '                objWs.Range(strCellPos).Value = arrLine
        '                objWs.Range(strCellPos).RowHeight = strErrMsg.Split(vbLf).Length * 50
        '            End If
        '        End If



        '        'Kiem tra thong tin Khach san

        '        If Not drExportData("HotelName") Is DBNull.Value Then

        '            If drExportData("ReferenceRate") < drExportData("RatePerRoomPerNight") Then
        '                i = i + 1
        '                Dim arrLine() As String
        '                arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
        '                                        , drExportData("TravellerName"), "Reference Rate is lower than Paid Rate" _
        '                                        , drExportData("BkgTool"), drExportData("TravelId")}
        '                strCellPos = "A" & i & ":" & "H" & i
        '                objWs.Range(strCellPos).Value = arrLine
        '            End If

        '        End If

        '    Loop
        '    .Columns("A:H").EntireColumn.AutoFit()
        '    .Cells.Select()
        '    .Cells.EntireRow.AutoFit()
        'End With

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

        Dim lstPnrElementTypes As New List(Of String)
        lstPnrElementTypes.Add("AIR")

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
                    colRequiredData = pobjTvcs.GetDataRequirement(86756, "M", lstPnrElementTypes, True)
                    colDataErr = pobjTvcs.CheckData(colRequiredData, colAvaiData, 8085, drExportData("DOI"))
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


                'Check fare
                If colAvaiData.Contains("RF") Then
                    Dim objRefFare As clsAvailableData
                    objRefFare = colAvaiData("RF")
                    If objRefFare.DataValue < drExportData("Fare") Then
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
                    Dim objRefFare As clsAvailableData
                    objRefFare = colAvaiData("LF")
                    If drExportData("Fare") < objRefFare.DataValue Then
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
    Private Function ValidateTravelData(tblTravel As System.Data.DataTable) As Boolean

        Dim i As Integer = 1
        Dim j As Integer

        Dim arrAirFields() As String
        Dim arrDepDates() As String
        Dim arrCars() As String
        Dim blnMissingDepDate As Boolean = False
        Dim lstPnrElementTypes As New List(Of String)


        lstPnrElementTypes.Add("AIR")

        arrAirFields = New String() {"DepApts", "ArrApts", "ALs", "RBD", "DepDates" _
                                    , "ArrDates", "FltNbrs" _
                                    , "ETD", "ETA", "FBs", "Rloc", "InvNo"}
        For Each objRow As DataRow In tblTravel.Rows
            blnMissingDepDate = False
            Dim colAvaiData As New Collection
            Dim colRequiredData As New Collection
            Dim colDataErr As New Collection
            Dim strErrMsg As String
            For j = 0 To UBound(arrAirFields)
                If Trim(objRow(arrAirFields(j))) = "" Then
                    Dim objError As New clsExportError(objRow("DOI"), objRow("SRV"), objRow("TravelId") _
                                                       , objRow("Carrier"), objRow("TkId") _
                                                       , CreateDataErrorMsg(arrAirFields(j)), objRow("BkgTool") _
                                                       , objRow("RasTkno"), objRow("Rloc"))
                    mlstErrors.Add(objError)
                End If
            Next
            arrCars = Split(objRow("ALS"), "_")
            arrDepDates = Split(objRow("DepDates"), "_")
            If arrDepDates.Length < arrCars.Length Then
                blnMissingDepDate = True
            Else
                For j = 0 To arrCars.Length - 1
                    If arrDepDates(j) = "" And arrCars(j) <> "//" Then
                        blnMissingDepDate = True
                        Exit For
                    End If
                Next
            End If
            If blnMissingDepDate Then
                Dim objError As New clsExportError(objRow("DOI"), objRow("SRV"), objRow("TravelId") _
                                                   , objRow("Carrier"), objRow("TkId") _
                                                    , CreateDataErrorMsg("DOF"), objRow("BkgTool") _
                                                    , objRow("RasTkno"), objRow("Rloc"))
                mlstErrors.Add(objError)
            End If
            If Len(objRow("RBD")) < arrCars.Length Then
                Dim objError As New clsExportError(objRow("DOI"), objRow("SRV"), objRow("TravelId") _
                                                   , objRow("Carrier"), objRow("TkId") _
                                                    , CreateDataErrorMsg("RBD"), objRow("BkgTool") _
                                                    , objRow("RasTkno"), objRow("Rloc"))
                mlstErrors.Add(objError)
            End If
            If objRow("SRV") = "R" _
                    AndAlso Split(objRow("FBs"), "+").Length <> arrCars.Length Then
                Dim objError As New clsExportError(objRow("DOI"), objRow("SRV"), objRow("TravelId") _
                                                   , objRow("Carrier"), objRow("TkId") _
                                                       , CreateDataErrorMsg("FBs"), objRow("BkgTool") _
                                                       , objRow("RasTkno"), objRow("Rloc"))
                mlstErrors.Add(objError)
            End If

            'Ten khach phai co gach cheo
            If Not objRow("TravellerName").ToString.Contains("/") Then
                Dim objError As New clsExportError(objRow("DOI"), objRow("SRV"), objRow("TravelId") _
                                                   , objRow("Carrier"), objRow("TkId") _
                                                       , "PaxName must have slash", objRow("BkgTool") _
                                                       , objRow("RasTkno"), objRow("Rloc"))
                mlstErrors.Add(objError)
            End If

            'Check RequiredData
            Dim intByPassId As Integer
            intByPassId = pobjTvcs.GetBypassId(objRow("TravelId"), "REQUIRED_DATA")
            If intByPassId = 0 Then
                colAvaiData = ConvertRequiredDataString2Collection(objRow("RequiredData"))
                colRequiredData = pobjTvcs.GetDataRequirement(86756, "M", lstPnrElementTypes, True)
                colDataErr = pobjTvcs.CheckData(colRequiredData, colAvaiData, 8085, objRow("DOI"))
                strErrMsg = pobjTvcs.DataErr2Msg(colDataErr)
                If strErrMsg <> "" Then
                    Dim objError As New clsExportError(objRow("DOI"), objRow("SRV"), objRow("TravelId") _
                                                       , objRow("Carrier"), objRow("TkId") _
                                                       , CreateDataErrorMsg(strErrMsg), objRow("BkgTool") _
                                                       , objRow("RasTkno"), objRow("Rloc"))
                    mlstErrors.Add(objError)
                End If
            End If

            'Check fare
            If colAvaiData.Contains("RF") Then
                Dim objRefFare As clsAvailableData
                objRefFare = colAvaiData("RF")
                If objRefFare.DataValue < objRow("Fare") Then
                    Dim objError As New clsExportError(objRow("DOI"), objRow("SRV"), objRow("TravelId") _
                                                       , objRow("Carrier"), objRow("TkId") _
                                                       , "Reference Fare is lower than Paid Fare", objRow("BkgTool") _
                                                       , objRow("RasTkno"), objRow("Rloc"))
                    mlstErrors.Add(objError)
                End If
            End If
            If colAvaiData.Contains("LF") Then
                Dim objRefFare As clsAvailableData
                objRefFare = colAvaiData("LF")
                If objRow("Fare") < objRefFare.DataValue Then
                    Dim objError As New clsExportError(objRow("DOI"), objRow("SRV"), objRow("TravelId"), objRow("Carrier"), objRow("TkId") _
                                                       , "Paid Fare is lower than Lowest Fare", objRow("BkgTool") _
                                                       , objRow("RasTkno"), objRow("Rloc"))
                    mlstErrors.Add(objError)
                End If
            End If

            'Kiem tra thong tin Khach san
            'If objRow("HtlExist") AndAlso objRow("HotelName") Is DBNull.Value Then
            '    Dim objError As New clsExportError(objRow("DOI"), objRow("SRV"), objRow("TravelId"), objRow("Carrier"), objRow("TkId") _
            '                                           , "Missing Hotel Info", objRow("BkgTool") _
            '                                           , objRow("RasTkno"), objRow("Rloc"))
            '    mlstErrors.Add(objError)
            'End If

            'If Not objRow("HotelName") Is DBNull.Value Then

            '    If objRow("ReferenceRate") < objRow("RatePerRoomPerNight") Then
            '        Dim objError As New clsExportError(objRow("DOI"), objRow("SRV"), objRow("TravelId"), objRow("Carrier"), objRow("TkId") _
            '                                           , "Reference Rate is lower than Paid Rate", objRow("BkgTool") _
            '                                           , objRow("RasTkno"), objRow("Rloc"))
            '        mlstErrors.Add(objError)
            '    End If
            'End If
        Next

        If mlstErrors.Count = 0 Then
            Return True
        Else
            Dim objXl As New Excel.Application
            Dim objWb As Workbook
            Dim objWs As New Worksheet
            objXl.Visible = True
            objWb = objXl.Workbooks.Add
            objWs = objWb.ActiveSheet
            With objWs
                .Cells(i, 1) = "DOI"
                .Cells(i, 2) = "SRV"
                .Cells(i, 3) = "TravelId"
                .Cells(i, 4) = "Supplier"
                .Cells(i, 5) = "Document/Item"
                .Cells(i, 6) = "Error"
                .Cells(i, 7) = "BkgTool"
                .Cells(i, 8) = "RLOC"

                For i = 0 To mlstErrors.Count - 1
                    .Range("A" & i + 2).Value = mlstErrors(i).DOI
                    .Range("B" & i + 2).Value = mlstErrors(i).SRV
                    .Range("C" & i + 2).Value = mlstErrors(i).TravelId
                    .Range("D" & i + 2).Value = mlstErrors(i).Supplier
                    .Range("E" & i + 2).Value = IIf(mlstErrors(i).Document <> "", mlstErrors(i).Document, mlstErrors(i).ItemId)
                    .Range("F" & i + 2).Value = mlstErrors(i).DataError
                    .Range("G" & i + 2).Value = mlstErrors(i).BkgTool
                    .Range("H" & i + 2).Value = mlstErrors(i).Rloc
                Next
                .Columns("A:H").EntireColumn.AutoFit()
                .Cells.Select()
                .Cells.EntireRow.AutoFit()
            End With
            Return False
        End If
    End Function
    Private Function ExportHotelOnly() As Boolean

        Dim strEndDate As String
        Dim strStartDate As String
        Dim strTravellerName As String = ""
        Dim strQuerry As String
        Dim tblTravel As System.Data.DataTable
        Dim intTravelId As Integer

        strStartDate = CreateFromDate(dtpFromDate.Value.Date)
        strEndDate = CreateToDate(dtpToDate.Value.Date)

        strQuerry = "select t.CustID,h.*,t.TravellerName,t.TripPurpose" _
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
                    & " and t.Status='OK' and CustId in (90359,90360) order by h.TravelId"

        tblTravel = pobjTvcs.GetDataTable(strQuerry)

        If tblTravel.Rows.Count = 0 Then
            MsgBox("No Hotel-only data for selected dates!")
            Return True
        End If

        If Not ValidateDataHotelOnly(tblTravel) Then
            MsgBox("Unable to export Data")
            Me.Dispose()
            Exit Function
        End If

        For Each objRow As DataRow In tblTravel.Rows
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

            objName.ParseFullName(objRow("TravellerName"))
            colRqData = ConvertRequiredDataString2Collection(objRow("RequiredData").ToString)

            If colRqData.Contains("MS") Then
                strMsCode = CType(colRqData("MS"), clsAvailableData).DataValue
            End If


            If colRqData.Contains("CC") Then
                strCostCenter = CType(colRqData("CC"), clsAvailableData).DataValue
            End If

            If colRqData.Contains("CO") Then
                strCompanyCode = CType(colRqData("CO"), clsAvailableData).DataValue
            End If

            strDomIntl = IdentifyDomInlt4iBank(objRow("CityCode"), objRow("CityCode"))

            Select Case objRow("SRV")
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

            strTcTrips = objRow("TravelId") & "," & objRow("InvNo") _
                        & "," & Convert2iBankDate(objRow("InvDate")) _
                        & "," & objRow("CustId") & ",,,," & objRow("BkgDate") _
                        & ",ZZ,," & objName.LastName & "," & objName.FirstName _
                        & ",0.00,,0.00," & strMsCode & ",0.00,,,,,," & objRow("Rloc") & "," & strDomIntl _
                        & "," & strTransType & ",VN," & strCostCenter _
                        & "," & strCompanyCode & ",,," & strPlusMinus _
                        & ",,,,," & objRow("CurCode") & "," & strExchange _
                        & ",,,,,,,,,A,,,"
            mobjFileTCTRIPS.WriteLine(strTcTrips)
            'TC HOTELS
            If Not objRow("HotelName") Is DBNull.Value Then
                strTcHotel = objRow("TravelId") & "," & objRow("ChainCode") & "," & objRow("HotelName") _
                    & "," & Mid(pobjTvcs.GetCityName(objRow("CityCode")), 1, 24) _
                    & ",,," & Convert2iBankDate(objRow("CheckInDate")) _
                    & "," & Convert2iBankDate(DateAdd(DateInterval.Day, objRow("NbrOfNight"), objRow("CheckInDate"))) _
                    & "," & objRow("NbrOfNight") & ",1,,," & objRow("RatePerRoomPerNight") & "," & objRow("CurCode") _
                    & ",1,,,,,,,," & objRow("ReferenceRate") & ",V,,,," & pobjTvcs.GetCountryCode(objRow("CityCode")) _
                    & ",,,"
                mobjFileTCHOTEL.WriteLine(strTcHotel)
            End If

            ''TC SERVICES
            'strTcServices = objRow("TravelId") & ",TSF,SERVICE FEE" _
            '                & "," & FormatNumber(objRow("TrxFee"), 2, , , TriState.False) & "," & objRow("CurCode") _
            '                & "," & Convert2iBankDate(objRow("TvlDOI")) _
            '                & ",,,,,," & strTransType & ",,,"
            'mobjFileTCSERVICES.WriteLine(strTcServices)

            'bo dung dich vu MISC
            If intTravelId <> objRow("TravelId") Then
                intTravelId = objRow("TravelId")
                WriteMiscFile(intTravelId, strTransType, mobjFileTCSERVICES)
            End If
            'TCUIDS
            arrUIDS = Split(objRow("RequiredData"), "|")

            For i = 0 To arrUIDS.Length - 1

                If arrUIDS(i).StartsWith("UDID") Then
                    Dim strTcUids As String
                    Dim arrBreak() As String
                    arrBreak = Split(arrUIDS(i), "/", 2)
                    strTcUids = objRow("TravelId") & "," _
                                & Mid(arrBreak(0), 5) & "," & arrBreak(1)
                    mobjFileTCUDIDS.WriteLine(strTcUids)
                End If
            Next
        Next


        Return True
    End Function
    Private Function ExportMiscSvc() As Boolean

        Dim strEndDate As String
        Dim strStartDate As String
        Dim strTravellerName As String = ""
        Dim strQuerry As String
        Dim tblTravel As System.Data.DataTable
        Dim intTravelId As Integer

        strStartDate = CreateFromDate(dtpFromDate.Value.Date)
        strEndDate = CreateToDate(dtpToDate.Value.Date)

        strQuerry = "select t.CustId,t.TravellerName,t.TripPurpose" _
                    & ",t.BkgMethod,t.BkgTool,t.BkgAction" _
                    & ",t.RLOC,t.BkgDate,t.DepDate,t.DOI" _
                    & ",t.OriDossierNbr,t.OriInvNbr" _
                    & ",t.EmplID,t.Dossier,t.InvNo" _
                    & ",t.AgencyCode,t.FOP,t.CCNo,t.CCCode" _
                    & ",t.Location,t.Hierachy1,t.Hierachy2,t.Hierachy3" _
                    & ",t.Hierachy4,t.Hierachy5,t.HtlExist,t.CorpType" _
                    & ",RequiredData,InvDate,h.*" _
                    & " from Go_MiscSvc h left join Go_Travel t" _
                    & " on h.TravelId=t.Recid" _
                    & " where (t.DOI between '" & strStartDate & "' and '" & strEndDate _
                    & "')  and t.RecId not in (Select TravelId from go_air) " _
                    & " and t.Status='OK' and CustId in (90359,90360) order by h.TravelId"

        tblTravel = pobjTvcs.GetDataTable(strQuerry)

        If tblTravel.Rows.Count = 0 Then
            MsgBox("No Misc service data for selected dates!")
            Return True
        End If

        If Not ValidateMiscSvc(tblTravel) Then
            MsgBox("Unable to export Misc Svc Data")
            Me.Dispose()
            Exit Function
        End If

        For Each objRow As DataRow In tblTravel.Rows
            Dim strTcTrips As String
            Dim objName As New clsNameiBank
            Dim colRqData As Collection
            Dim strMsCode As String = ""
            Dim strTransType As String = "S"
            Dim strPlusMinus As String = "1"
            Dim strCostCenter As String = ""
            Dim strCompanyCode As String = ""
            Dim strExchange As String = "F"
            Dim i As Int16
            Dim arrUIDS() As String

            objName.ParseFullName(objRow("TravellerName"))
            colRqData = ConvertRequiredDataString2Collection(objRow("RequiredData").ToString)

            If colRqData.Contains("MS") Then
                strMsCode = CType(colRqData("MS"), clsAvailableData).DataValue
            End If


            If colRqData.Contains("CC") Then
                strCostCenter = CType(colRqData("CC"), clsAvailableData).DataValue
            End If

            If colRqData.Contains("CO") Then
                strCompanyCode = CType(colRqData("CO"), clsAvailableData).DataValue
            End If


            Select Case objRow("SRV")
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

            strTcTrips = objRow("TravelId") & "," & objRow("InvNo") _
                        & "," & Convert2iBankDate(objRow("InvDate")) _
                        & "," & objRow("CustId") & ",,,," & objRow("BkgDate") _
                        & ",ZZ,," & objName.LastName & "," & objName.FirstName _
                        & ",0.00,,0.00," & strMsCode & ",0.00,,,,,," & objRow("Rloc") _
                        & ",," & strTransType & ",VN," & strCostCenter _
                        & "," & strCompanyCode & ",,," & strPlusMinus _
                        & ",,,,," & objRow("CurCode") & "," & strExchange _
                        & ",,,,,,,,,A,,,"
            mobjFileTCTRIPS.WriteLine(strTcTrips)
            'TCCARS

            If intTravelId <> objRow("TravelId") Then
                intTravelId = objRow("TravelId")
                WriteMiscFile(intTravelId, strTransType, mobjFileTCSERVICES)
            End If

            'TCUIDS
            arrUIDS = Split(objRow("RequiredData"), "|")

            For i = 0 To arrUIDS.Length - 1

                If arrUIDS(i).StartsWith("UDID") Then
                    Dim strTcUids As String
                    Dim arrBreak() As String
                    Dim intRef As Integer

                    arrBreak = Split(arrUIDS(i), "/", 2)
                    intRef = GetRefByDataCode4RM(arrBreak(0))
                    If intRef <> 0 Then
                        strTcUids = objRow("TravelId") & "," _
                                & intRef & "," & arrBreak(1)
                        mobjFileTCUDIDS.WriteLine(strTcUids)
                    End If
                End If
            Next
        Next

        mobjFileTCTRIPS.Close()
        mobjFileTCHOTEL.Close()
        mobjFileTCSERVICES.Close()
        mobjFileTCUDIDS.Close()

        Return True
    End Function
    Private Sub frmiBankExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Now.Day < 5 Then
            dtpFromDate.Value = DateSerial(Now.Year, Now.Month, 1).AddMonths(-1)
            dtpToDate.Value = DateSerial(Now.Year, Now.Month, 1).AddDays(-1)
        Else
            dtpFromDate.Value = DateSerial(Now.Year, Now.Month, 1)
            dtpToDate.Value = Now.AddDays(-1)
        End If

    End Sub
End Class