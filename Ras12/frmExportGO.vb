Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class frmExportGO
    Dim mstrQuerry As String
    Private Function ExportAirHotel() As Boolean
        Dim drTravel As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        Dim strEndDate As String
        Dim strStartDate As String
        Dim strTravellerName As String = ""
        Dim intTktCount As Integer

        strStartDate = Format(dtpFromDate.Value.Date, "dd-MMM-yy") & " 00:00:00"
        strEndDate = Format(dtpToDate.Value.Date, "dd-MMM-yy") & " 23:59:00"

        pobjTvcs.Connect2()
        cmdSql.Connection = pobjTvcs.Connection2

        mstrQuerry = "select a.*,t.Status,t.CMC,t.CustID" _
                    & ",t.TravellerName,t.TripPurpose" _
                    & ",t.BkgMethod,t.BkgTool,t.BkgAction" _
                    & ",t.RLOC,t.Ref1,t.Ref2,t.Ref3" _
                    & ",t.Ref4,t.Ref5,t.Ref2,t.Ref6" _
                    & ",t.Ref7,t.Ref8,t.Ref9,t.Ref10" _
                    & ",t.BkgDate,t.DepDate,t.DOI as TvlDOI" _
                    & ",t.OriDossierNbr,t.OriInvNbr" _
                    & ",t.EmplID,t.Dossier,t.InvNo" _
                    & ",t.AgencyCode,t.FOP,t.CCNo" _
                    & ",t.Location,t.Hierachy1,t.Hierachy2,t.Hierachy3" _
                    & ",t.Hierachy4,t.Hierachy5,t.HtlExist,t.CorpType,t.CcCode" _
                    & ",h.CityCode,h.HotelName,h.CheckInDate,h.RoomType" _
                    & ",h.NbrOfRm,h.NbrOfNight,h.CurCode,h.RatePerRoomPerNight" _
                    & ",h.ResType,h.VoucherNbr,h.CityName,h.CountryCode" _
                    & ",h.ChainCode,h.LocalIntlCode,h.LocalIntlCodeType,h.TelNbr" _
                    & ",h.ReferenceRate,h.LowestRate,h.RsCode,h.MsCode" _
                    & ",h.CcNbr,h.ContractNbr,h.IataCode,h.CwtPrefered" _
                    & ",h.Commissionable,h.TripPurpose,h.SvcFeeApplied,h.BkgMethod" _
                    & ",h.Eticket,h.BkgTool,h.FOP" _
                    & ",h.CommAmt,h.TaxAmt,h.TrxFee as HtlTrxFee,h.BillingSupplier" _
                    & ",h.OriVoucherNbr,h.CustPrefered,h.BkgAgt" _
                    & ",h.BookDate,h.ReferenceRate,h.LowestRate" _
                    & " from Go_Air a " _
                    & " left join Go_Travel t on a.TravelID=t.RecID" _
                    & " left join go_hotel h on t.Recid=h.travelid and h.Status='OK'" _
                    & " where (t.DOI between '" & strStartDate _
                    & "' and '" & strEndDate _
                    & "') and a.TravelId=t.Recid and Voided=0" _
                    & " and t.CorpType='CWT'" _
                    & " and t.Status='OK'" _
                    & " and t.cmc IN (select CMC from GO_CompanyInfo1 where Status='ok' and Go_Client=1) " _
                    & " and a.DocType not in ('MCO','AHC')" '& " and t.Recid=358101"


        If cboCustomer.Text <> "" Then
            mstrQuerry = mstrQuerry & " and t.CustId=" & cboCustomer.SelectedValue
        End If
        If IsNumeric(txtTravelId.Text) AndAlso txtTravelId.Text > 0 Then
            mstrQuerry = mstrQuerry & " and t.RecId=" & txtTravelId.Text
        End If
        mstrQuerry = mstrQuerry & " order by t.DOI, t.CMC"


        cmdSql.CommandText = mstrQuerry
        drTravel = cmdSql.ExecuteReader
        If Not drTravel.HasRows Then
            MsgBox("No ticket data for selected dates!")
            drTravel.Close()
            Return False
        End If
        If Not ExportValidation(drTravel) Then
            MsgBox("Unable to export AIR data due to data errors!")
            drTravel.Close()
            Me.Dispose()
            Return False
        End If

        drTravel.Close()
        drTravel = cmdSql.ExecuteReader

        If Not drTravel Is Nothing Then
            Dim strLogFile As String
            Dim strHeader As String
            Dim intTravelId As Integer
            Dim intRecNbr As Integer = 0
            Dim strReference As String = ""
            Dim strCommonStructure As String = ""
            Dim strBasicRecord As String
            Dim strTravelBRecord As String

            Dim strFullLine As String = ""
            Dim strCountry As String = "VN "
            Dim strCreationDate As String = Format(Now, "ddMMyy")
            Dim strCreationTime As String = Format(Now, "hhmmss")

            Dim strCustomerStructure As New String(" "c, 25)
            Dim strOdTravel As New String(" "c, 60)
            Dim strOdTkt As New String(" "c, 10)
            Dim strSavingAmt As New String("0"c, 22)
            Dim strSavingCode As New String(" "c, 4)
            Dim strBranchId As String = "128005  "
            Dim strUnusedPart As New String(" "c, 125)

            Dim strTktNbr As String = ""
            Dim strCos As String = ""
            Dim strCommIndicator As String = ""
            Dim strFareType As String = ""
            Dim strMinusPlus As String = ""
            Dim strHierachy As String
            'Dim strHierachy1 As String
            'Dim strHierachy2 As String
            'Dim strHierachy3 As String
            'Dim strHierachy4 As String
            'Dim strHierachy5 As String

            strLogFile = System.AppDomain.CurrentDomain.BaseDirectory() _
                        & Format(dtpFromDate.Value, "yyMMdd") & "-" _
                        & Format(dtpToDate.Value, "yyMMdd") & ".txt"
            Dim objLogFile As New System.IO.StreamWriter(strLogFile, False)

            strHeader = "I" & "CWTVN   " & "CWT-CIB " & strCreationDate & strCreationTime _
                        & "CIS"

            Do While drTravel.Read()


                'Tao Travel Record
                If intTravelId <> drTravel("TravelId") Then

                    'Tinh cac gia tri chung cho tung ve (bao gom ca ve noi)
                    strTravellerName = RemoveSpecialChrsWebField(drTravel("TravellerName")).PadRight(30)

                    If Len(strTravellerName) > 30 Then
                        strTravellerName = Mid(strTravellerName, 1, 30)
                    End If
                    intTktCount = 1
                    If drTravel("Carrier") & drTravel("TKNO") <> strTktNbr Then
                        strCos = pobjTvcs.GetGO_COS(Mid(drTravel("RBD"), 1, 1))
                        System.Threading.Thread.Sleep(50)
                        If drTravel("Comm") = 0 Then
                            strCommIndicator = "N"
                        Else
                            strCommIndicator = "C"
                        End If
                    End If
                    Select Case drTravel("NegoFare")
                        Case "CWT"
                            strFareType = "W"
                        Case Else
                            strFareType = "N"
                    End Select
                    If drTravel("SRV") = "R" Then
                        strMinusPlus = "-"
                    Else
                        strMinusPlus = " "
                    End If

                    strHierachy = CreateSCC11(drTravel("Location"), drTravel("Hierachy1"), drTravel("Hierachy2") _
                                , drTravel("Hierachy3"), drTravel("Hierachy4"), drTravel("Hierachy5"))

                    intRecNbr = intRecNbr + 1
                    strReference = drTravel("CMC").ToString.PadLeft(5, "0") & strCountry & " " _
                            & strHierachy.PadRight(25) _
                            & strTravellerName _
                            & drTravel("EmplId").ToString.PadRight(25) _
                            & Date2YYMMDD(drTravel("DepDate"))

                    If strReference.Length <> "95" Then
                        MsgBox("Incorrect format Reference Length for travel id" _
                                & drTravel("TravelId"))
                        Return False
                    End If

                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference

                    strBasicRecord = "1" & drTravel("BkgTool").ToString.PadRight(3) _
                                    & drTravel("Dossier").ToString.PadLeft(9, "0") _
                                    & drTravel("InvNo").ToString.PadLeft(9, "0") _
                                    & Format(drTravel("DOI"), "yyMMdd") _
                                    & strOdTravel & strSavingAmt & strSavingCode _
                                    & drTravel("FOP") _
                                    & Mid(drTravel("CCNo").ToString.PadRight(16), 1, 16) _
                                    & strBranchId _
                                    & strUnusedPart & "*" & vbCrLf
                    strBasicRecord = strCommonStructure & strBasicRecord
                    If Len(strBasicRecord) <> 402 Then
                        MsgBox("Incorrect format Travel Basic Record for travel id" _
                                & drTravel("TravelId"))
                        Return False
                    End If
                    strBasicRecord = Replace(strBasicRecord, Chr(160), " ")
                    objLogFile.Write(strBasicRecord)

                    'Tao Additional TravelB record - Chi co voi ve Refund hoac ve doi

                    If drTravel("OriDossierNbr") <> "" Then
                        intRecNbr = intRecNbr + 1
                        Dim strDocNbrB As New String("0"c, 15)
                        Dim strSequence As New String("0"c, 4)
                        Dim strCountriesOfOD As New String(" "c, 6)
                        Dim strTrvlAutho As New String(" "c, 15)

                        Dim strTrxFeeFlag As String
                        Dim strSourceSystem As String = "RAS"
                        Dim strSpacesB As New String(" "c, 175)

                        If drTravel("CMC") = 843 Then
                            strTrxFeeFlag = "N"
                        Else
                            strTrxFeeFlag = "Y"
                        End If

                        strTravelBRecord = "910" & strDocNbrB & strSequence _
                                            & drTravel("OriDossierNbr").ToString.PadLeft(9, "0") _
                                            & drTravel("OriInvNbr").ToString.PadLeft(9, "0") _
                                            & strCountriesOfOD & strTrvlAutho & pstrIata _
                                            & strTrxFeeFlag & drTravel("CustId").ToString.PadLeft(10, "0") _
                                            & strSourceSystem.PadRight(10) & strSpacesB & "*" & vbCrLf

                        strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                        strTravelBRecord = strCommonStructure & strTravelBRecord
                        If Len(strTravelBRecord) <> 402 Then
                            MsgBox("Incorrect format Travel B Record")
                        End If
                        objLogFile.Write(strTravelBRecord)
                    End If

                    'Tao TravelCRecord
                    intRecNbr = intRecNbr + 1
                    Dim strTravelCRecord As String
                    Dim strSpacesC As New String(" "c, 12)
                    strTravelCRecord = "915" & drTravel("Ref1").ToString.PadRight(25) _
                                        & drTravel("Ref2").ToString.PadRight(25) _
                                        & drTravel("Ref3").ToString.PadRight(25) _
                                        & drTravel("Ref4").ToString.PadRight(25) _
                                        & drTravel("Ref5").ToString.PadRight(25) _
                                        & drTravel("Ref6").ToString.PadRight(25) _
                                        & drTravel("Ref7").ToString.PadRight(25) _
                                        & drTravel("Ref8").ToString.PadRight(25) _
                                        & drTravel("Ref9").ToString.PadRight(25) _
                                        & drTravel("Ref10").ToString.PadRight(25) _
                                        & strSpacesC & "*" & vbCrLf
                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                    strTravelCRecord = strCommonStructure & strTravelCRecord
                    strTravelCRecord = ReplaceSpecialChar(strTravelCRecord)
                    If Len(strTravelCRecord) <> 402 Then
                        MsgBox("Incorrect format Travel C Record")
                    End If
                    objLogFile.Write(strTravelCRecord)

                End If

                'Tao AIR Section of the trip
                Dim arrTkno(0 To 0) As String
                Dim arrAddNbrOfTkts(0 To 0) As String
                Dim arrTktSegmentInfo(0 To 0) As String
                Dim arrTktSOs(0 To 0) As String
                Dim arrTktAddSegmentInfo(0 To 0) As String
                Dim strSpaces1AddSegRecord As New String(" "c, 15)

                Dim i As Integer
                Dim arrDepDates() As String
                Dim arrDepApts() As String
                Dim arrArrApts() As String
                Dim arrALs() As String
                Dim arrFltNbrs() As String
                Dim arrDepTimes() As String
                Dim arrArrTimes() As String
                Dim arrFBs() As String
                Dim arrSOs() As String
                Dim arrClss(0 To Len(drTravel("RBD")) - 1) As String
                Dim strMileage As New String(" "c, 5)
                Dim strValorisation As New String(" "c, 11)

                arrDepDates = Split(drTravel("DepDates"), "_")
                arrDepApts = Split(drTravel("DepApts"), "_")
                arrArrApts = Split(drTravel("ArrApts"), "_")
                arrALs = Split(drTravel("ALs"), "_")
                arrFltNbrs = Split(drTravel("FltNbrs"), "_")
                arrDepTimes = Split(drTravel("ETD"), "_")
                arrArrTimes = Split(drTravel("ETA"), "_")
                arrFBs = Split(drTravel("FBs"), "+")
                arrSOs = Split(drTravel("SO"), "_")

                For i = 1 To Len(drTravel("RBD"))

                    Dim strCls As String
                    strCls = drTravel("RBD").ToString.Chars(i - 1)
                    If strCls = " " Then
                        'Continue For
                        arrClss(i - 1) = "**"
                    Else
                        arrClss(i - 1) = strCls.PadRight(2)
                    End If
                Next
                If drTravel("AddNbrOfTkts") = 0 Then

                    arrTkno(0) = drTravel("TKNO").ToString.PadLeft(15, "0")
                    arrAddNbrOfTkts(0) = drTravel("AddNbrOfTkts").ToString.PadLeft(2, "0")
                    For i = 0 To UBound(arrDepDates)
                        If arrDepDates(i) = "VOID" Then
                            Continue For
                        End If
                        arrTktSegmentInfo(0) = arrTktSegmentInfo(0) & arrDepDates(i).PadRight(5) _
                                & 0 _
                                & arrDepApts(i).PadRight(5) _
                                & arrArrApts(i).PadRight(5) _
                                & arrALs(i).PadRight(3) _
                                & arrFltNbrs(i).PadRight(5) & arrClss(i) _
                                & arrDepTimes(i).PadRight(4) _
                                & arrArrTimes(i).PadRight(4) _
                                & Mid(arrFBs(i), 1, 8).PadRight(8) _
                                & strMileage & strValorisation & " "
                        arrTktSOs(0) = arrTktSOs(0) & arrSOs(i)
                        arrTktAddSegmentInfo(0) = arrTktAddSegmentInfo(0) & "N" & "N" _
                                                & " " & " " & " " _
                                                & strSpaces1AddSegRecord
                    Next
                Else

                    ReDim Preserve arrTkno(0 To drTravel("AddNbrOfTkts"))
                    ReDim Preserve arrAddNbrOfTkts(0 To drTravel("AddNbrOfTkts"))
                    ReDim Preserve arrTktSegmentInfo(0 To drTravel("AddNbrOfTkts"))
                    ReDim Preserve arrTktSOs(0 To drTravel("AddNbrOfTkts"))
                    ReDim Preserve arrTktAddSegmentInfo(0 To drTravel("AddNbrOfTkts"))
                    For i = 0 To UBound(arrTkno)
                        Dim j As Integer
                        Dim intStartSeg As Integer
                        Dim intEndSeg As Integer
                        arrTkno(i) = (drTravel("TKNO") + i).ToString.PadLeft(15, "0")
                        If i = 0 Then
                            arrAddNbrOfTkts(i) = drTravel("AddNbrOfTkts").ToString.PadLeft(2, "0")
                        Else
                            arrAddNbrOfTkts(i) = "00"
                        End If

                        intStartSeg = i * 4
                        Select Case i
                            Case UBound(arrTkno)
                                intEndSeg = UBound(arrDepDates)

                            Case Else
                                intEndSeg = intStartSeg + 3
                        End Select

                        For j = intStartSeg To intEndSeg

                            If arrDepDates(j) = "VOID" Then
                                Continue For
                            End If

                            arrTktSegmentInfo(i) = arrTktSegmentInfo(i) _
                                                    & arrDepDates(j).PadRight(5) _
                                                    & 0 _
                                                    & arrDepApts(j).PadRight(5) _
                                                    & arrArrApts(j).PadRight(5) _
                                                    & arrALs(j).PadRight(3) _
                                                    & arrFltNbrs(j).PadRight(5) & arrClss(j) _
                                                    & arrDepTimes(j).PadRight(4) _
                                                    & arrArrTimes(j).PadRight(4) _
                                                    & Mid(arrFBs(j), 1, 8).PadRight(8) & strMileage _
                                                    & strValorisation & " "
                            arrTktSOs(i) = arrTktSOs(i) & arrSOs(j)
                            arrTktAddSegmentInfo(i) = arrTktAddSegmentInfo(i) & "N" & "N" _
                                                & " " & " " & " " _
                                                & strSpaces1AddSegRecord
                        Next
                    Next
                End If
                For i = 0 To UBound(arrTkno)
                    'Tao Tkt Record
                    Dim strTktHeader As String
                    Dim strTktInfo As String
                    Dim strTktRecord As String
                    Dim strTktOD As New String(" "c, 10)
                    Dim strFfp As New String(" "c, 12)
                    'Dim strTourCode As New String(" "c, 15)
                    Dim strSaveCodes As New String(" "c, 6)
                    Dim strSpacesAir As New String(" "c, 63)
                    Dim strPaidFare As String = FormatGoText(drTravel("Fare"), 11, True)
                    Dim strRefFare As String = FormatGoText(drTravel("RefFare"), 11, True)
                    Dim strLowFare As String = FormatGoText(drTravel("LowestFare"), 11, True)


                    intRecNbr = intRecNbr + 1
                    strTktHeader = "2" & arrTkno(i) _
                                   & arrAddNbrOfTkts(i) & 1

                    If strTktHeader.Length <> 19 Then
                        MsgBox("Invalid ticket header length for tkt " & arrTkno(i))
                        Return False
                    End If
                    strTktInfo = strTravellerName _
                                & drTravel("Carrier").ToString.PadRight(3) _
                                & strCos & strFareType & strOdTkt _
                                & strPaidFare & strMinusPlus _
                                & strRefFare _
                                & strLowFare _
                                & drTravel("Currency").ToString.PadRight(4) & " " _
                                & drTravel("RSaving").ToString.PadRight(2) _
                                & drTravel("MSaving").ToString.PadRight(2) _
                                & Mid(drTravel("CCNo").ToString.PadRight(16), 1, 16) _
                                & Format(drTravel("BkgDate"), "yyMMdd") & strFfp _
                                & drTravel("RLOC").ToString.PadRight(6) _
                                & Format(drTravel("DOI"), "yyMMdd") _
                                & FormatGoText(drTravel("ContractNbr"), 15) _
                                & pstrIata & "    " _
                                & drTravel("OriALTKNO").ToString.PadLeft(15, "0") & "I" _
                                & strSaveCodes & strSpacesAir & "*" & vbCrLf

                    If strTktInfo.Length <> 249 Then
                        MsgBox("Invalid ticket Info length for tkt " & arrTkno(i))
                        Return False
                    End If

                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                    strTktRecord = strCommonStructure & strTktHeader & strTktInfo
                    If strTktRecord.Length <> 402 Then
                        MsgBox("Incorrect format Ticket Record")
                        Return False
                    End If
                    objLogFile.Write(strTktRecord)

                    If i = 0 Then
                        'Tao Additional Air Record, chi co voi Main ticket
                        Dim strAddAirRecord As String
                        Dim strDocNbrAir As New String("0"c, 15)
                        'Dim strBkgTool As New String(" "c, 3)
                        'Dim strCCCode As New String(" "c, 2)
                        Dim strBkgAgt As New String(" "c, 7)
                        Dim strBkgProductCode As New String(" "c, 10)
                        Dim strBkgAction As New String(" "c, 10)
                        Dim strTrxCode As New String(" "c, 4)
                        Dim strSpacesAddAir As New String(" "c, 165)
                        Dim strTrxFeeFlag As String
                        intRecNbr = intRecNbr + 1

                        If drTravel("CMC") = 843 Then
                            strTrxFeeFlag = "N"
                        Else
                            strTrxFeeFlag = "Y"
                        End If

                        strAddAirRecord = "921" & strDocNbrAir & "0000" & "NN" & strCommIndicator _
                                        & drTravel("TripPurpose").ToString.PadRight(1) _
                                        & strTrxFeeFlag & drTravel("BkgMethod") & "E" _
                                        & drTravel("BkgTool") & drTravel("FOP") _
                                        & drTravel("CCCode").ToString.PadRight(2) & strBkgAgt _
                                        & drTravel("Comm").ToString.PadLeft(11, "0") _
                                        & drTravel("TotalTax").ToString.PadLeft(11, "0") _
                                        & drTravel("TrxFee").ToString.PadLeft(11, "0") _
                                        & drTravel("BillingSupplier") _
                                        & strBkgProductCode & drTravel("BkgAction") & " " & "I" _
                                        & drTravel("CommType") & strTrxCode & " " & strSpacesAddAir _
                                        & "*" & vbCrLf
                        strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                        strAddAirRecord = strCommonStructure & strAddAirRecord

                        If Len(strAddAirRecord) <> 402 Then
                            MsgBox("Incorrect format Additional Air Record" & vbCrLf & strAddAirRecord)
                        End If
                        objLogFile.Write(strAddAirRecord)
                    End If

                    'Segment sub section of the air ticket
                    Dim strSegRecord As String
                    Dim strSegHeader As String
                    Dim strSegInfo As String = ""
                    Dim str1Seg As String = ""
                    Dim str4Segs As String = ""
                    Dim strSOs As String = ""


                    Dim strSpacesSeg As New String(" "c, 6)

                    intRecNbr = intRecNbr + 1
                    strSegHeader = "2" & arrTkno(i) _
                                    & arrAddNbrOfTkts(i) & "2"


                    strSegInfo = arrTktSegmentInfo(i).PadRight(236) & arrTktSOs(i).PadRight(4) _
                                & strSpacesSeg & "*" & vbCrLf
                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                    strSegRecord = strCommonStructure & strSegHeader & strSegInfo
                    If Len(strSegRecord) <> 402 Then
                        Append2TextFile(strSegRecord)
                        MsgBox("Incorrect format Segment Record" & vbCrLf & strSegRecord)
                    End If
                    objLogFile.Write(strSegRecord)

                    'Tao Additional segment record
                    Dim strAddSegRecord As String
                    Dim strAddSegHeader As String
                    Dim strAddSegInfo As String = ""
                    Dim strDocNbrAddSeg As New String(" "c, 15)

                    Dim strSpaces2AddSegRecord As New String(" "c, 162)

                    intRecNbr = intRecNbr + 1

                    strAddSegHeader = "922" & strDocNbrAddSeg & "0000" & " "

                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                    strAddSegRecord = strCommonStructure & strAddSegHeader & arrTktAddSegmentInfo(i).PadRight(80) _
                                      & strSpaces2AddSegRecord & "*" & vbCrLf
                    If Len(strAddSegRecord) <> 402 Then
                        MsgBox("Incorrect format Additional Segment Record" & vbCrLf & strAddSegRecord)
                        Append2TextFile(strAddSegRecord)
                    End If
                    objLogFile.Write(strAddSegRecord)
                Next

                If Not drTravel.IsDBNull(drTravel.GetOrdinal("CityCode")) Then
                    'Tao hotel segment
                    Dim strHtlRecord As String
                    Dim strFiller1 As New String(" "c, 5)
                    Dim strFiller2 As New String(" "c, 20)
                    Dim strHarpHtlKey As New String(" "c, 20)
                    Dim strRateSign As String = ""
                    Dim strBkgAction As String = ""

                    If drTravel("SRV") = "R" Then
                        strRateSign = "-"
                        strBkgAction = "AR"
                    ElseIf drTravel("SRV") = "S" Then
                        strRateSign = " "
                        strBkgAction = "AB"
                    End If


                    strHtlRecord = "5" & drTravel("ResType") _
                            & drTravel("VoucherNbr").ToString.PadRight(15) _
                            & drTravel("CityCode").ToString.PadRight(5) _
                            & drTravel("CityName").ToString.PadRight(25) _
                            & drTravel("CountryCode").ToString.PadRight(3) _
                            & drTravel("ChainCode").ToString.PadRight(4) _
                            & drTravel("LocalIntlCode").ToString.PadRight(17) _
                            & drTravel("TelNbr").ToString.PadRight(20) & strFiller1 _
                            & FormatGoText(drTravel("HotelName"), 25) _
                            & Format(drTravel("CheckInDate"), "yyMMdd") _
                            & drTravel("RoomType").ToString.PadRight(4) _
                            & drTravel("NbrOfRm").ToString.PadLeft(2, "0") _
                            & drTravel("NbrOfNight").ToString.PadLeft(2, "0") _
                            & drTravel("RatePerRoomPerNight").ToString.PadLeft(11, "0") _
                            & strRateSign.PadRight(1) _
                            & drTravel("ReferenceRate").ToString.PadLeft(11, "0") _
                            & drTravel("LowestRate").ToString.PadLeft(11, "0") _
                            & drTravel("CurCode").ToString.PadRight(4) _
                            & drTravel("RsCode").ToString.PadRight(2) _
                            & drTravel("MsCode").ToString.PadRight(2) _
                            & drTravel("CcNbr").ToString.PadRight(16) _
                            & Format(drTravel("BkgDate"), "yyMMdd") _
                            & drTravel("ContractNbr").ToString.PadRight(15) _
                            & drTravel("IataCode").ToString.PadRight(8) _
                            & drTravel("LocalIntlCodeType").ToString.PadRight(3) _
                            & drTravel("LocalIntlCode").ToString.PadRight(20) _
                            & strFiller2 & "*" & vbCrLf
                    'Dim strHarpHtlKey As New String(" "c, 20)

                    intRecNbr = intRecNbr + 1
                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                    strHtlRecord = strCommonStructure & strHtlRecord
                    If Len(strHtlRecord) <> 402 Then
                        MsgBox("Incorrect format Hotel Record" & vbCrLf & strHtlRecord)
                        Append2TextFile(strHtlRecord)
                    End If
                    objLogFile.Write(strHtlRecord)


                    'Tao Additional Hotel record
                    Dim strAddHtlRecord As String
                    Dim strFiller3 As New String(" "c, 15)
                    Dim strFiller4 As New String(" "c, 149)
                    Dim strBkgProductCode As New String(" "c, 10)

                    strAddHtlRecord = "950" & strFiller3 & "0000"
                    Select Case drTravel("CustPrefered")
                        Case True
                            strAddHtlRecord = strAddHtlRecord & "P"
                        Case False
                            strAddHtlRecord = strAddHtlRecord & "N"
                    End Select

                    Select Case drTravel("CwtPrefered")
                        Case True
                            strAddHtlRecord = strAddHtlRecord & "P"
                        Case False
                            strAddHtlRecord = strAddHtlRecord & "N"
                    End Select

                    Select Case drTravel("Commissionable")
                        Case True
                            strAddHtlRecord = strAddHtlRecord & "C"
                        Case False
                            strAddHtlRecord = strAddHtlRecord & "N"
                    End Select
                    strAddHtlRecord = strAddHtlRecord & drTravel("TripPurpose")

                    Select Case drTravel("SvcFeeApplied")
                        Case True
                            strAddHtlRecord = strAddHtlRecord & "Y"
                        Case False
                            strAddHtlRecord = strAddHtlRecord & "N"
                    End Select
                    strAddHtlRecord = strAddHtlRecord & drTravel("BkgMethod") & "E" _
                                    & drTravel("BkgTool") & drTravel("FOP") _
                                    & drTravel("CcCode").ToString.PadRight(2) _
                                    & drTravel("BkgAgt").ToString.PadRight(7) _
                                    & drTravel("CommAmt").ToString.PadLeft(11, "0") _
                                    & drTravel("TaxAmt").ToString.PadLeft(11, "0") _
                                    & drTravel("HtlTrxFee").ToString.PadLeft(11, "0") _
                                    & drTravel("BillingSupplier").ToString.PadRight(3) _
                                    & strBkgProductCode & strBkgAction.PadRight(2) _
                                    & drTravel("RLOC").ToString.PadRight(10) _
                                    & drTravel("OriVoucherNbr").ToString.PadLeft(15, "0") _
                                    & strFiller4 & "*" & vbCrLf

                    intRecNbr = intRecNbr + 1
                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                    strAddHtlRecord = strCommonStructure & strAddHtlRecord
                    If strAddHtlRecord.Length <> 402 Then
                        MsgBox("Incorrect format Additional Hotel Record" & vbCrLf & strAddHtlRecord)
                        Append2TextFile(strAddHtlRecord)
                    End If
                    objLogFile.Write(strAddHtlRecord)
                End If
            Loop

            objLogFile.Close()
            objLogFile = Nothing
        End If
        drTravel.Close()
        pobjTvcs.Disconnect2()

        Return True
    End Function
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If ExportAirHotel() Then ' AndAlso ExportVisa(True) Then
            MsgBox("Export Air Completed!")
        End If
        If ExportHotel(True) Then
            MsgBox("Export Non Air Hotel Completed!")
        End If
        If ExportVisa(True) Then
            MsgBox("Export Visa Services Completed!")
        End If

        Me.Dispose()
    End Sub
    Private Function ExportHotel(blnAppend2OldFile As Boolean) As Boolean
        Dim drTravel As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        Dim strEndDate As String
        Dim strStartDate As String
        Dim strTravellerName As String = ""
        Dim intTktCount As Integer

        strStartDate = Format(dtpFromDate.Value.Date, "dd-MMM-yy") & " 00:00:00"
        strEndDate = Format(dtpToDate.Value.Date, "dd-MMM-yy") & " 23:59:00"

        pobjTvcs.Connect2()
        cmdSql.Connection = pobjTvcs.Connection2

        mstrQuerry = "select t.Status,t.CMC,t.CustID" _
                    & ",t.TravellerName,t.TripPurpose" _
                    & ",t.BkgMethod,t.BkgTool,t.BkgAction" _
                    & ",t.RLOC,t.Ref1,t.Ref2,t.Ref3" _
                    & ",t.Ref4,t.Ref5,t.Ref2,t.Ref6" _
                    & ",t.Ref7,t.Ref8,t.Ref9,t.Ref10" _
                    & ",t.BkgDate,t.DepDate,t.DOI as TvlDOI" _
                    & ",t.OriDossierNbr,t.OriInvNbr" _
                    & ",t.EmplID,t.Dossier,t.InvNo" _
                    & ",t.AgencyCode,t.FOP,t.CCNo,t.CCCode" _
                    & ",t.Location,t.Hierachy1,t.Hierachy2,t.Hierachy3" _
                    & ",t.Hierachy4,t.Hierachy5,t.HtlExist,t.CorpType" _
                    & ",h.CityCode,h.HotelName,h.CheckInDate,h.RoomType" _
                    & ",h.NbrOfRm,h.NbrOfNight,h.CurCode,h.RatePerRoomPerNight" _
                    & ",h.ResType,h.VoucherNbr,h.CityName,h.CountryCode" _
                    & ",h.ChainCode,h.LocalIntlCode,h.LocalIntlCodeType,h.TelNbr" _
                    & ",h.ReferenceRate,h.LowestRate,h.RsCode,h.MsCode" _
                    & ",h.CcNbr,h.ContractNbr,h.IataCode,h.CwtPrefered" _
                    & ",h.Commissionable,h.TripPurpose,h.SvcFeeApplied,h.BkgMethod" _
                    & ",h.Eticket,h.BkgTool,h.FOP,h.CcCode" _
                    & ",h.CommAmt,h.TaxAmt,h.TrxFee as HtlTrxFee,h.BillingSupplier" _
                    & ",h.OriVoucherNbr,h.CustPrefered,h.BkgAgt,h.TravelId" _
                    & ",h.BookDate,h.ReferenceRate,h.LowestRate" _
                    & " from go_hotel h" _
                    & " left join Go_Travel t on t.Recid=h.travelid" _
                    & " where (t.DOI between '" & strStartDate _
                    & "' and '" & strEndDate _
                    & "') and substring(cmc,1,1) <>'L'" _
                    & " and t.Status='OK' and h.Status='OK'" _
                    & " and t.RecId not in (Select TravelId from Go_air" _
                    & " where Voided=0) " _
                    & " and t.CustID in (select CustID from GO_CompanyInfo1 where status='ok' and TMC<>'RM'" _
                    & " and GO_Client=1)"

        If cboCustomer.Text <> "" Then
            mstrQuerry = mstrQuerry & " and t.CustId=" & cboCustomer.SelectedValue
        End If

        If IsNumeric(txtTravelId.Text) AndAlso txtTravelId.Text > 0 Then
            mstrQuerry = mstrQuerry & " and t.RecId=" & txtTravelId.Text
        End If

        cmdSql.CommandText = mstrQuerry
        drTravel = cmdSql.ExecuteReader
        If Not drTravel.HasRows Then
            MsgBox("No Hotel-Only data for selected dates!")
            drTravel.Close()
            Return True
        End If
        If Not ExportValidationHotel(drTravel) Then
            MsgBox("Unable to export Non-Air Hotel data due to data errors!")
            drTravel.Close()
            Me.Dispose()
            frmMain.Show()
            Return False
        End If
        drTravel.Close()
        drTravel = cmdSql.ExecuteReader

        If Not drTravel Is Nothing Then
            Dim strLogFile As String
            Dim strHeader As String
            Dim intTravelId As Integer
            Dim intRecNbr As Integer = 0
            Dim strReference As String = ""
            Dim strCommonStructure As String = ""
            Dim strBasicRecord As String

            Dim strFullLine As String = ""
            Dim strCountry As String = "VN "
            Dim strCreationDate As String = Format(Now, "ddMMyy")
            Dim strCreationTime As String = Format(Now, "hhmmss")

            Dim strCustomerStructure As New String(" "c, 25)
            Dim strOdTravel As New String(" "c, 60)
            Dim strOdTkt As New String(" "c, 10)
            Dim strSavingAmt As New String("0"c, 22)
            Dim strSavingCode As New String(" "c, 4)
            Dim strBranchId As String = "128005  "
            Dim strUnusedPart As New String(" "c, 125)

            Dim strTktNbr As String = ""
            Dim strCos As String = ""
            Dim strCommIndicator As String = ""
            Dim strMinusPlus As String = ""
            Dim strHierachy As String
            Dim strHierachy1 As String
            Dim strHierachy2 As String
            Dim strHierachy3 As String
            Dim strHierachy4 As String
            Dim strHierachy5 As String
            Dim strTripPurpose As String

            strLogFile = System.AppDomain.CurrentDomain.BaseDirectory() _
                        & Format(dtpFromDate.Value, "yyMMdd") & "-" _
                        & Format(dtpToDate.Value, "yyMMdd") & ".txt"
            Dim objLogFile As New System.IO.StreamWriter(strLogFile, blnAppend2OldFile)

            strHeader = "I" & "CWTVN   " & "CWT-CIB " & strCreationDate & strCreationTime _
                        & "CIS"

            Do While drTravel.Read()
                'Tao Travel Record

                If intTravelId <> drTravel("TravelId") Then
                    'Tinh cac gia tri chung cho tung ve (bao gom ca ve noi)
                    strTravellerName = drTravel("TravellerName").ToString.Trim.PadRight(30)
                    
                    If strTravellerName.Length > 30 Then
                        strTravellerName = Mid(strTravellerName, 1, 30)
                    End If

                    intTktCount = 1

                    strMinusPlus = " "

                    strHierachy = CreateSCC11(drTravel("Location"), drTravel("Hierachy1"), drTravel("Hierachy2") _
                            , drTravel("Hierachy3"), drTravel("Hierachy4"), drTravel("Hierachy5"))

                    intRecNbr = intRecNbr + 1
                    strReference = drTravel("CMC").ToString.PadLeft(5, "0") & strCountry & " " _
                            & strHierachy _
                            & strTravellerName _
                            & drTravel("EmplId").ToString.PadRight(25) _
                            & Date2YYMMDD(drTravel("DepDate"))

                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference

                    strBasicRecord = "1" & drTravel("BkgTool").ToString.PadRight(3) _
                                    & drTravel("Dossier").ToString.PadLeft(9, "0") _
                                    & drTravel("InvNo").ToString.PadLeft(9, "0") _
                                    & Format(drTravel("TvlDOI"), "yyMMdd") _
                                    & strOdTravel & strSavingAmt & strSavingCode _
                                    & drTravel("FOP") _
                                    & drTravel("CCNo").ToString.PadRight(16) _
                                    & strBranchId _
                                    & strUnusedPart & "*" & vbCrLf
                    strBasicRecord = strCommonStructure & strBasicRecord
                    If Len(strBasicRecord) <> 402 Then
                        MsgBox("Incorrect format Travel Basic Record for travel id" _
                                & drTravel("TravelId"))
                        Exit Function
                    End If
                    strBasicRecord = Replace(strBasicRecord, Chr(160), " ")
                    objLogFile.Write(strBasicRecord)


                    'Tao TravelCRecord
                    intRecNbr = intRecNbr + 1
                    Dim strTravelCRecord As String
                    Dim strSpacesC As New String(" "c, 12)
                    strTravelCRecord = "915" & drTravel("Ref1").ToString.PadRight(25) _
                                        & drTravel("Ref2").ToString.PadRight(25) _
                                        & drTravel("Ref3").ToString.PadRight(25) _
                                        & drTravel("Ref4").ToString.PadRight(25) _
                                        & drTravel("Ref5").ToString.PadRight(25) _
                                        & drTravel("Ref6").ToString.PadRight(25) _
                                        & drTravel("Ref7").ToString.PadRight(25) _
                                        & drTravel("Ref8").ToString.PadRight(25) _
                                        & drTravel("Ref9").ToString.PadRight(25) _
                                        & drTravel("Ref10").ToString.PadRight(25) _
                                        & strSpacesC & "*" & vbCrLf
                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                    strTravelCRecord = strCommonStructure & strTravelCRecord
                    strTravelCRecord = ReplaceSpecialChar(strTravelCRecord)
                    If Len(strTravelCRecord) <> 402 Then
                        MsgBox("Incorrect format Travel C Record")
                    End If
                    objLogFile.Write(strTravelCRecord)

                End If

                If Not drTravel.IsDBNull(drTravel.GetOrdinal("CityCode")) Then
                    'Tao hotel segment
                    Dim strHtlRecord As String
                    Dim strFiller1 As New String(" "c, 5)
                    Dim strFiller2 As New String(" "c, 20)
                    Dim strHarpHtlKey As New String(" "c, 20)
                    Dim strRateSign As String = ""

                    Dim strHotelName As String = ReplaceSpecialChar(drTravel("HotelName"))

                    strRateSign = IIf(drTravel("BkgAction") = "AB", " ", "-")

                    strHtlRecord = "5" & drTravel("ResType") _
                                    & drTravel("VoucherNbr").ToString.PadRight(15) _
                                    & drTravel("CityCode").ToString.PadRight(5) _
                                    & drTravel("CityName").ToString.PadRight(25) _
                                    & drTravel("CountryCode").ToString.PadRight(3) _
                                    & drTravel("ChainCode").ToString.PadRight(4) _
                                    & drTravel("LocalIntlCode").ToString.PadRight(17) _
                                    & drTravel("TelNbr").ToString.PadRight(20) & strFiller1 _
                                    & FormatGoText(strHotelName, 25) _
                                    & Format(drTravel("CheckInDate"), "yyMMdd") _
                                    & drTravel("RoomType").ToString.PadRight(4) _
                                    & drTravel("NbrOfRm").ToString.PadLeft(2, "0") _
                                    & drTravel("NbrOfNight").ToString.PadLeft(2, "0") _
                                    & Math.Abs(drTravel("RatePerRoomPerNight")).ToString.PadLeft(11, "0") _
                                    & strRateSign.PadRight(1) _
                                    & Math.Abs(drTravel("ReferenceRate")).ToString.PadLeft(11, "0") _
                                    & Math.Abs(drTravel("LowestRate")).ToString.PadLeft(11, "0") _
                                    & drTravel("CurCode").ToString.PadRight(4) _
                                    & drTravel("RsCode").ToString.PadRight(2) _
                                    & drTravel("MsCode").ToString.PadRight(2) _
                                    & drTravel("CcNbr").ToString.PadRight(16) _
                                    & Format(drTravel("BkgDate"), "yyMMdd") _
                                    & drTravel("ContractNbr").ToString.PadRight(15) _
                                    & drTravel("IataCode").ToString.PadRight(8) _
                                    & drTravel("LocalIntlCodeType").ToString.PadRight(3) _
                                    & drTravel("LocalIntlCode").ToString.PadRight(20) & strFiller2 & "*" & vbCrLf

                    intRecNbr = intRecNbr + 1
                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                    strHtlRecord = strCommonStructure & strHtlRecord
                    If strHtlRecord.Length <> 402 Then
                        MsgBox("Incorrect format Hotel Record" & vbCrLf & strHtlRecord)
                        Append2TextFile(strHtlRecord)
                    End If
                    objLogFile.Write(strHtlRecord)

                    'Tao Additional Hotel record
                    Dim strAddHtlRecord As String
                    Dim strFiller3 As New String(" "c, 15)
                    Dim strFiller4 As New String(" "c, 149)
                    Dim strBkgProductCode As New String(" "c, 10)

                    strAddHtlRecord = "950" & strFiller3 & "0000"
                    Select Case drTravel("CustPrefered")
                        Case True
                            strAddHtlRecord = strAddHtlRecord & "P"
                        Case False
                            strAddHtlRecord = strAddHtlRecord & "N"
                    End Select

                    Select Case drTravel("CwtPrefered")
                        Case True
                            strAddHtlRecord = strAddHtlRecord & "P"
                        Case False
                            strAddHtlRecord = strAddHtlRecord & "N"
                    End Select

                    Select Case drTravel("Commissionable")
                        Case True
                            strAddHtlRecord = strAddHtlRecord & "C"
                        Case False
                            strAddHtlRecord = strAddHtlRecord & "N"
                    End Select

                    strTripPurpose = IIf(drTravel("TripPurpose") = "", "B", drTravel("TripPurpose"))

                    strAddHtlRecord = strAddHtlRecord & strTripPurpose

                    Select Case drTravel("SvcFeeApplied")
                        Case True
                            strAddHtlRecord = strAddHtlRecord & "Y"
                        Case False
                            strAddHtlRecord = strAddHtlRecord & "N"
                    End Select
                    strAddHtlRecord = strAddHtlRecord & drTravel("BkgMethod") & "E" _
                                    & drTravel("BkgTool") & drTravel("FOP") _
                                    & drTravel("CcCode") _
                                    & drTravel("BkgAgt").ToString.PadRight(7) _
                                    & drTravel("CommAmt").ToString.PadLeft(11, "0") _
                                    & drTravel("TaxAmt").ToString.PadLeft(11, "0") _
                                    & drTravel("HtlTrxFee").ToString.PadLeft(11, "0") _
                                    & drTravel("BillingSupplier").ToString.Trim.PadRight(3) _
                                    & strBkgProductCode & drTravel("BkgAction").PadRight(2) _
                                    & drTravel("RLOC").ToString.PadRight(10) _
                                    & drTravel("OriVoucherNbr").ToString.PadLeft(15, "0") _
                                    & strFiller4 & "*" & vbCrLf

                    intRecNbr = intRecNbr + 1
                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                    strAddHtlRecord = strCommonStructure & strAddHtlRecord
                    If strAddHtlRecord.Length <> 402 Then
                        MsgBox("Incorrect format Additional Hotel Record" & vbCrLf & strAddHtlRecord)
                        Append2TextFile(strAddHtlRecord)
                        Return False
                    End If
                    objLogFile.Write(strAddHtlRecord)
                End If
            Loop

            objLogFile.Close()
            objLogFile = Nothing
        End If
        drTravel.Close()
        pobjTvcs.Disconnect2()
        Return True
    End Function
    Private Function ExportVisa(blnAppend2OldFile As Boolean) As Boolean
        Dim drTravel As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        Dim strEndDate As String
        Dim strStartDate As String
        Dim strTravellerName As String = ""
        Dim intTktCount As Integer

        strStartDate = Format(dtpFromDate.Value.Date, "dd-MMM-yy") & " 00:00:00"
        strEndDate = Format(dtpToDate.Value.Date, "dd-MMM-yy") & " 23:59:00"

        pobjTvcs.Connect2()
        cmdSql.Connection = pobjTvcs.Connection2
        cmdSql.CommandText = "update go_travel set status='OK' where Status='XX' and RecID in (select travelid from GO_MiscSvc)"
        cmdSql.ExecuteNonQuery()

        mstrQuerry = "select t.Status,t.CMC,t.CustID" _
                    & ",t.TravellerName,t.TripPurpose" _
                    & ",t.BkgMethod,t.BkgTool,t.BkgAction" _
                    & ",t.RLOC,t.Ref1,t.Ref2,t.Ref3" _
                    & ",t.Ref4,t.Ref5,t.Ref2,t.Ref6" _
                    & ",t.Ref7,t.Ref8,t.Ref9,t.Ref10" _
                    & ",t.BkgDate,t.DepDate,t.DOI as TvlDOI" _
                    & ",t.OriDossierNbr,t.OriInvNbr" _
                    & ",t.EmplID,t.Dossier,t.InvNo" _
                    & ",t.AgencyCode,t.FOP,t.CCNo,t.CCCode" _
                    & ",t.Location,t.Hierachy1,t.Hierachy2,t.Hierachy3" _
                    & ",t.Hierachy4,t.Hierachy5,t.CorpType,T.BillingSupplier" _
                    & ",m.*" _
                    & " from GO_MiscSvc m" _
                    & " left join Go_Travel t on t.Recid=m.travelid" _
                    & " where (t.DOI between '" & strStartDate _
                    & "' and '" & strEndDate _
                    & "') and t.CorpType='CWT'" _
                    & " and t.Bkgaction<>'AR' and Status='OK' and m.SvcType='VISA'" _
                    & " and t.CustId IN (select CustId from GO_CompanyInfo1 where Status='ok' and Go_Client=1) "

        If cboCustomer.Text <> "" Then
            mstrQuerry = mstrQuerry & " and t.CustId=" & cboCustomer.SelectedValue
        End If

        If IsNumeric(txtTravelId.Text) AndAlso txtTravelId.Text > 0 Then
            mstrQuerry = mstrQuerry & " and t.RecId=" & txtTravelId.Text
        End If
        mstrQuerry = mstrQuerry & " order by t.DOI, t.CMC"

        cmdSql.CommandText = mstrQuerry
        drTravel = cmdSql.ExecuteReader
        If Not drTravel.HasRows Then
            MsgBox("No Visa service data for selected dates!")
            drTravel.Close()
            Me.Dispose()
            Return True
        End If
        If Not ExportValidationVisa(drTravel) Then
            MsgBox("Unable to export Visa services data due to data errors!")
            drTravel.Close()
            Me.Dispose()
            frmMain.Show()
            Return False
        End If
        drTravel.Close()
        drTravel = cmdSql.ExecuteReader

        If Not drTravel Is Nothing Then
            Dim strLogFile As String
            Dim strHeader As String
            Dim intTravelId As Integer
            Dim intRecNbr As Integer = 0
            Dim strReference As String = ""
            Dim strCommonStructure As String = ""
            Dim strBasicRecord As String

            Dim strFullLine As String = ""
            Dim strCountry As String = "VN "
            Dim strCreationDate As String = Format(Now, "ddMMyy")
            Dim strCreationTime As String = Format(Now, "hhmmss")

            Dim strCustomerStructure As New String(" "c, 25)
            Dim strOdTravel As New String(" "c, 60)
            Dim strOdTkt As New String(" "c, 10)
            Dim strSavingAmt As New String("0"c, 22)
            Dim strSavingCode As New String(" "c, 4)
            Dim strBranchId As String = "128005  "
            Dim strUnusedPart As New String(" "c, 125)

            Dim strTktNbr As String = ""
            Dim strCos As String = ""
            Dim strCommIndicator As String = ""
            Dim strMinusPlus As String = ""
            Dim strHierachy As String
            Dim strHierachy1 As String
            Dim strHierachy2 As String
            Dim strHierachy3 As String
            Dim strHierachy4 As String
            Dim strHierachy5 As String

            strLogFile = System.AppDomain.CurrentDomain.BaseDirectory() _
                        & Format(dtpFromDate.Value, "yyMMdd") & "-" _
                        & Format(dtpToDate.Value, "yyMMdd") & ".txt"
            Dim objLogFile As New System.IO.StreamWriter(strLogFile, blnAppend2OldFile)

            strHeader = "I" & "CWTVN   " & "CWT-CIB " & strCreationDate & strCreationTime _
                        & "CIS"

            Do While drTravel.Read()
                'Tao Travel Record


                If intTravelId <> drTravel("TravelId") Then
                    'Tinh cac gia tri chung cho tung ve (bao gom ca ve noi)
                    strTravellerName = drTravel("TravellerName").ToString.PadRight(30)

                    If strTravellerName.Length > 30 Then
                        strTravellerName = Mid(strTravellerName, 1, 30)
                    End If

                    intTktCount = 1

                    strMinusPlus = " "

                    strHierachy = CreateSCC11(drTravel("Location"), drTravel("Hierachy1"), drTravel("Hierachy2") _
                            , drTravel("Hierachy3"), drTravel("Hierachy4"), drTravel("Hierachy5"))

                    intRecNbr = intRecNbr + 1
                    strReference = drTravel("CMC").ToString.PadLeft(5, "0") & strCountry & " " _
                            & strHierachy _
                            & strTravellerName _
                            & drTravel("EmplId").ToString.PadRight(25) _
                            & Date2YYMMDD(drTravel("DepDate"))

                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference

                    strBasicRecord = "1" & drTravel("BkgTool").ToString.PadRight(3) _
                                    & drTravel("Dossier").ToString.PadLeft(9, "0") _
                                    & drTravel("InvNo").ToString.PadLeft(9, "0") _
                                    & Format(drTravel("TvlDOI"), "yyMMdd") _
                                    & strOdTravel & strSavingAmt & strSavingCode _
                                    & drTravel("FOP") _
                                    & drTravel("CCNo").ToString.PadRight(16) _
                                    & strBranchId _
                                    & strUnusedPart & "*" & vbCrLf
                    strBasicRecord = strCommonStructure & strBasicRecord
                    If Len(strBasicRecord) <> 402 Then
                        MsgBox("Incorrect format Travel Basic Record for travel id" _
                                & drTravel("TravelId"))
                        Return False
                    End If
                    strBasicRecord = Replace(strBasicRecord, Chr(160), " ")
                    objLogFile.Write(strBasicRecord)


                    'Tao TravelCRecord
                    intRecNbr = intRecNbr + 1
                    Dim strTravelCRecord As String
                    Dim strSpacesC As New String(" "c, 12)
                    strTravelCRecord = "915" & drTravel("Ref1").ToString.PadRight(25) _
                                        & drTravel("Ref2").ToString.PadRight(25) _
                                        & drTravel("Ref3").ToString.PadRight(25) _
                                        & drTravel("Ref4").ToString.PadRight(25) _
                                        & drTravel("Ref5").ToString.PadRight(25) _
                                        & drTravel("Ref6").ToString.PadRight(25) _
                                        & drTravel("Ref7").ToString.PadRight(25) _
                                        & drTravel("Ref8").ToString.PadRight(25) _
                                        & drTravel("Ref9").ToString.PadRight(25) _
                                        & drTravel("Ref10").ToString.PadRight(25) _
                                        & strSpacesC & "*" & vbCrLf
                    strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                    strTravelCRecord = strCommonStructure & strTravelCRecord
                    If Len(strTravelCRecord) <> 402 Then
                        MsgBox("Incorrect format Travel C Record")
                    End If
                    objLogFile.Write(strTravelCRecord)

                End If

                'Tao MISC SVC segment
                Dim strMISCRecord As String
                Dim strCityName As String = "HO CHI MINH CITY".PadRight(25)
                Dim strFiller As New String(" "c, 136)
                Dim strContract As New String(" "c, 15)
                Dim strFare As String = FormatFare4GlobalOneExport(drTravel("Fare"))

                strMISCRecord = "710" & Format(drTravel("DepDate"), "yyMMdd") & strCityName & "VN " _
                                & strFare & " " _
                                & strFare & strFare _
                                & drTravel("Currency").ToString.PadRight(4) & "    " _
                                & drTravel("CCNo").ToString.PadRight(16) _
                                & Format(drTravel("BkgDate"), "yyMMdd") & strContract & "37301401" _
                                & "     " & strFiller & "*" & vbCrLf

                intRecNbr = intRecNbr + 1
                strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                strMISCRecord = strCommonStructure & strMISCRecord
                If strMISCRecord.Length <> 402 Then
                    MsgBox("Incorrect format MISC SVC Record" & vbCrLf & strMISCRecord)
                    Append2TextFile(strMISCRecord)
                End If
                objLogFile.Write(strMISCRecord)

                'Tao     ADDITIONAL MISC SVC segment
                Dim strVisaRecord As String
                Dim strFiller1 As New String(" "c, 113)
                Dim strOriDoc As New String(" "c, 15)

                strVisaRecord = "970" & drTravel("DocNbr").ToString.PadRight(15) & "0000PPNBYME" _
                                & drTravel("BkgTool").ToString.PadRight(3) _
                                & drTravel("FOP").ToString.PadRight(2) _
                                & drTravel("CCCode").ToString.PadRight(2) & "NON-AIR" _
                                & drTravel("Comm").ToString.PadLeft(11, "0") _
                                & drTravel("TotalTax").ToString.PadLeft(11, "0") _
                                & drTravel("TrxFee").ToString.PadLeft(11, "0") _
                                & drTravel("BillingSupplier").ToString.PadRight(3) & "VISA      " _
                                & drTravel("BkgAction").ToString.PadRight(2) _
                                & drTravel("RLOC").ToString.PadRight(10) & strOriDoc _
                                & drTravel("SupplierCode").ToString.PadRight(10) _
                                & drTravel("SupplierLocalName").ToString.PadRight(25) _
                                & "I" & strFiller1 & "*" & vbCrLf

                intRecNbr = intRecNbr + 1
                strCommonStructure = strHeader & intRecNbr.ToString.PadLeft(7, "0") & strReference
                strVisaRecord = strCommonStructure & strVisaRecord
                If strVisaRecord.Length <> 402 Then
                    MsgBox("Incorrect format ADDITIONAL MISC SVC Record" & vbCrLf & strVisaRecord)
                    Append2TextFile(strVisaRecord)
                End If
                objLogFile.Write(strVisaRecord)
            Loop

            objLogFile.Close()
            objLogFile = Nothing
        End If
        drTravel.Close()
        pobjTvcs.Disconnect2()
        Return True
    End Function
    Private Function DataCrossCheck(tblCrossCheckValues As System.Data.DataTable, arrInputs As String()) As String
        Dim i As Integer
        With tblCrossCheckValues
            For i = 0 To tblCrossCheckValues.Rows.Count - 1
                If .Rows(i)("Field1Value") = arrInputs(GetColumnIndexByCdrHierName(.Rows(i)("Field1Name"))) _
                            AndAlso .Rows(i)("Field2Value") <> arrInputs(GetColumnIndexByCdrHierName(.Rows(i)("Field2Name"))) Then
                    Return "Conflict data between" & .Rows(i)("Field1Name") & " and " & .Rows(i)("Field2Name")
                End If
            Next
        End With
        Return ""
    End Function

    Private Function ExportValidation(ByVal drExportData As SqlClient.SqlDataReader) As Boolean
        Dim objXl As New Excel.Application
        Dim objWb As Workbook
        Dim objWs As New Worksheet
        Dim i As Integer = 1
        Dim j As Integer
        Dim strCellPos As String
        Dim arrHtlFields() As String
        Dim arrHtlErrors() As String
        Dim arrAirFields() As String
        Dim arrAirErrors() As String
        Dim arrDepDates() As String
        Dim arrCars() As String
        Dim arrFBs() As String
        Dim strCmc As String = ""
        Dim tblCrossCheckData As New System.Data.DataTable

        arrAirFields = New String() {"DepApts", "ArrApts", "ALs", "RBD", "DepDates", "FltNbrs" _
                                    , "ETD", "ETA", "FBs", "BkgMethod"}
        arrAirErrors = New String() {"Missing Departure Airports", "Missing Arrival Airports" _
                                    , "Missing Airlines for segments", "Missing RBDs for segments" _
                                    , "Missing DepartureDates for segments", "Missing Flight Numbers for segments" _
                                    , "Missing Departure times for segments", "Missing Arrival Times for segments" _
                                    , "Missing Fare Basis for segments", "Missing Bkg Method"}
        arrHtlFields = New String() {"RsCode", "MsCode"}
        arrHtlErrors = New String() {"Missing RealisedSaving Code", "Missing MissSaving Code"}
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
            .Cells(i, 7) = "Corporates"
            .Cells(i, 8) = "TravelId"
            .Cells(i, 9) = "DOI"
            .Cells(i, 9) = "SRV"

            Do While drExportData.Read()
                Dim blnMissingDepDate As Boolean
                Dim blnMissingFBs As Boolean
                Dim blnMissCcCode As Boolean
                If strCmc <> drExportData("CMC") Then
                    strCmc = drExportData("CMC")
                    tblCrossCheckData = pobjTvcs.GetDataTable("Select * from GO_DataCrossCheck where Status='OK' order by Field1Name,Field2Name")
                End If

                For j = 0 To UBound(arrAirFields)
                    If Trim(drExportData(arrAirFields(j))) = "" Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO") _
                                                , drExportData("RLOC"), drExportData("TravellerName") _
                                                , arrAirErrors(j), drExportData("BkgTool") _
                                                , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If
                Next


                'Check credit card type
                If drExportData("FOP") = "CC" Then
                    If IsDBNull(drExportData("CcCode")) Then
                        blnMissCcCode = True
                    ElseIf drExportData("CcCode") = "??" Then
                        blnMissCcCode = True
                    End If

                    If blnMissCcCode Then

                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), "Missing CreditCard Type" _
                                                , drExportData("BkgTool"), pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If

                End If

                If drExportData("RefFare") < drExportData("Fare") Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "Reference Fare is lower than Paid Fare" _
                                            , drExportData("BkgTool"), pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                If drExportData("Fare") < drExportData("LowestFare") Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName") _
                                            , "Paid Fare is lower than Lowest Fare" _
                                            , drExportData("BkgTool") _
                                            , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                If drExportData("RefFare") = drExportData("Fare") _
                    AndAlso Not "XX,EX".Contains(drExportData("RSaving")) Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "RealizedSavingCode must be XX/EX" _
                                            , drExportData("BkgTool"), pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                'Cross check MissSaving code

                If drExportData("MSaving") = "L" AndAlso drExportData("Fare") <> drExportData("LowestFare") _
                    AndAlso Not CrossCheckMissSaving(drExportData("MSaving"), drExportData("Fare") _
                                , drExportData("LowestFare"), drExportData("DOI") _
                                , pobjTvcs.GetCwtRtgTypeByTkno(drExportData("Carrier"), drExportData("TKNO")), drExportData("Currency")) Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName") _
                                            , "Conflict values: MissSaving Code, LowFare, PaidFare" _
                                            , drExportData("BkgTool") _
                                            , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                If  drExportData("LowestFare") = drExportData("Fare") _
                    AndAlso Not "L,E".Contains(drExportData("MSaving")) Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName") _
                                            , "MissSaving Code must be L/E" _
                                            , drExportData("BkgTool") _
                                            , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If


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
                    arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName") _
                                            , "Missing Departure Date for a segment" _
                                            , drExportData("BkgTool") _
                                            , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                    blnMissingDepDate = False
                End If

                If Len(drExportData("RBD")) < arrCars.Length Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "Missing RBD for a segment" _
                                            , drExportData("BkgTool"), pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If
                arrFBs = Split(drExportData("FBs"), "+")
                If UBound(arrDepDates) <> UBound(arrFBs) Then
                    blnMissingFBs = True
                End If
                If blnMissingFBs Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName") _
                                            , "Missing Fare Basis for a segment" _
                                            , drExportData("BkgTool") _
                                            , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                    blnMissingFBs = False
                End If

                If drExportData("SRV") = "R" _
                    AndAlso Split(drExportData("FBs"), "+").Length <> arrCars.Length Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "Missing Fare Basis for a segment for Refund Ticket" _
                                            , drExportData("BkgTool"), pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                If Not pobjTvcs.InCdrBypassList(drExportData("TravelId")) Then
                    'Kiem tra Hierachies
                    Dim colRequiredHierachies As New Collection
                    colRequiredHierachies = pobjTvcs.GetHierachies(drExportData("CMC"))
                    For Each objHierachy As clsHierachy In colRequiredHierachies
                        If drExportData("Hierachy" & objHierachy.Nbr) = "" Then
                            i = i + 1
                            Dim arrLine() As String
                            arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                                    , drExportData("TravellerName") _
                                                    , "Missing Hierachy" & objHierachy.Nbr _
                                                    , drExportData("BkgTool") _
                                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                    , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                            strCellPos = "A" & i & ":" & "J" & i
                            objWs.Range(strCellPos).Value = arrLine
                        ElseIf objHierachy.CheckValues _
                            AndAlso Not pobjTvcs.CheckHierachyValueValid(drExportData("Cmc") _
                                        , objHierachy.Nbr, drExportData("Hierachy" & objHierachy.Nbr)) Then
                            i = i + 1
                            Dim arrLine() As String
                            arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                                    , drExportData("TravellerName") _
                                                    , "Invalid Hierachy" & objHierachy.Nbr _
                                                    , drExportData("BkgTool") _
                                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                    , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                            strCellPos = "A" & i & ":" & "J" & i
                            objWs.Range(strCellPos).Value = arrLine
                        End If
                    Next

                    'Kiem tra CDRs
                    Dim colRequiredCDRs As New Collection
                    colRequiredCDRs = pobjTvcs.GetCDRs(drExportData("CMC"))
                    For Each objCdr As clsCwtCdr In colRequiredCDRs
                        If drExportData("Ref" & objCdr.Nbr) = "" Then
                            i = i + 1
                            Dim arrLine() As String
                            arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                                    , drExportData("TravellerName"), "Missing CDR" & objCdr.Nbr _
                                                    , drExportData("BkgTool") _
                                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                    , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                            strCellPos = "A" & i & ":" & "J" & i
                            objWs.Range(strCellPos).Value = arrLine

                        ElseIf Not objCdr.CheckFormat(drExportData("Ref" & objCdr.Nbr)) Then
                            i = i + 1
                            Dim arrLine() As String
                            arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                                    , drExportData("TravellerName"), "CDR" & objCdr.Nbr & "-" _
                                                    & objCdr.FormatError, drExportData("BkgTool") _
                                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                    , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                            strCellPos = "A" & i & ":" & "J" & i
                            objWs.Range(strCellPos).Value = arrLine
                        End If
                    Next
                End If

                'Data cross check

                If tblCrossCheckData.Rows.Count > 0 Then
                    Dim arrInputs As String() = {drExportData("Ref1"), drExportData("Ref2"), drExportData("Ref3"), drExportData("Ref4") _
                        , drExportData("Ref5"), drExportData("Ref6"), drExportData("Ref7"), drExportData("Ref8"), drExportData("Ref9") _
                        , drExportData("Ref10"), drExportData("Hierachy1"), drExportData("Hierachy2"), drExportData("Hierachy3")}
                    Dim strErrMsg As String = DataCrossCheck(tblCrossCheckData, arrInputs)
                    If strErrMsg <> "" Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), strErrMsg _
                                                 , drExportData("BkgTool") _
                                                 , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                 , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If
                End If

                'Kiem tra thong tin Khach san
                If drExportData("HtlExist") AndAlso drExportData("HotelName") Is DBNull.Value Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {pobjTvcs.GetCar3D(drExportData("Carrier")), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "Missing Hotel Info" _
                                             , drExportData("BkgTool") _
                                             , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                             , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If
                If Not drExportData("HotelName") Is DBNull.Value Then
                    If Not pobjTvcs.CheckRoomTypeCwt(drExportData("RoomType")) Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), "Invalid room type" _
                                                 , drExportData("BkgTool") _
                                                 , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                 , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If

                    If drExportData("LocalIntlCodeType") <> "RVN" _
                        AndAlso Not pobjTvcs.CheckHarpKey(CInt(drExportData("LocalIntlCode")) _
                                        , drExportData("CityCode")) Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), "Invalid Harpkey " _
                                                , drExportData("BkgTool") _
                                                , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                , drExportData("TravelId"), drExportData("SRV")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If
                    If drExportData("ReferenceRate") < drExportData("RatePerRoomPerNight") Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), "Reference Rate is lower than Paid Rate" _
                                                , drExportData("BkgTool"), pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If

                    If drExportData("RatePerRoomPerNight") < drExportData("LowestRate") Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                , drExportData("TravellerName") _
                                                , "Paid Rate is lower than Lowest Rate" _
                                                , drExportData("BkgTool") _
                                                , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If

                    For j = 0 To UBound(arrHtlFields)
                        If Trim(drExportData(arrHtlFields(j))) = "" Then
                            i = i + 1
                            Dim arrLine() As String
                            arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate") _
                                                    , drExportData("RLOC"), drExportData("TravellerName") _
                                                    , arrHtlErrors(j), drExportData("BkgTool") _
                                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                    , drExportData("TravelId"), drExportData("TvlDOI"), drExportData("SRV")}
                            strCellPos = "A" & i & ":" & "J" & i
                            objWs.Range(strCellPos).Value = arrLine
                        End If
                    Next
                End If
            Loop
            .Columns("A:H").EntireColumn.AutoFit()
        End With
        If i > 1 Then
            ExportValidation = False
        Else
            ExportValidation = True
            objWb.Close(False)
            objXl.Quit()
        End If
    End Function
    Private Function ExportValidationHotel(ByVal drExportData As SqlClient.SqlDataReader) As Boolean
        Dim objXl As New Excel.Application
        Dim objWb As Workbook
        Dim objWs As New Worksheet
        Dim i As Integer = 1
        Dim j As Integer
        Dim strCellPos As String
        Dim arrHtlFields() As String
        Dim arrHtlErrors() As String
        Dim strTcode As String

        objXl.Visible = True
        objWb = objXl.Workbooks.Add
        objWs = objWb.ActiveSheet

        arrHtlFields = New String() {"RsCode", "MsCode", "CityCode"}
        arrHtlErrors = New String() {"Missing RealisedSaving Code", "Missing MissSaving Code", "Missing City Code"}

        With objWs
            .Cells(i, 1) = "HotelName"
            .Cells(i, 2) = "CheckInDate"
            .Cells(i, 3) = "PNR"
            .Cells(i, 4) = "TravellerName"
            .Cells(i, 5) = "Error"
            .Cells(i, 6) = "BkgTool"
            .Cells(i, 7) = "Corporates"
            .Cells(i, 8) = "TravelId"
            .Cells(i, 9) = "TCODE"
            .Cells(i, 10) = "FinalizedDate"


            Do While drExportData.Read()
                If ContainSpecialChars(drExportData("HotelName"), True) Then
                    i = i + 1
                    Dim arrLine() As String
                    strTcode = GetTcode(drExportData("BkgTool"), drExportData("Rloc"), drExportData("VoucherNbr"))

                    arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                            , drExportData("TravellerName") _
                                            , "Hotel Name has special character" _
                                            , drExportData("BkgTool") _
                                            , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                    strCellPos = "A" & i & ":" & "j" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If
                If UCase(drExportData("TravellerName").ToString).StartsWith("MS") _
                    Or UCase(drExportData("TravellerName").ToString).StartsWith("MR") _
                    Or ContainSpecialChars(drExportData("TravellerName")) Then
                    i = i + 1
                    Dim arrLine() As String
                    strTcode = GetTcode(drExportData("BkgTool"), drExportData("Rloc"), drExportData("VoucherNbr"))

                    arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                            , drExportData("TravellerName") _
                                            , "Invalid Traveler Name format" _
                                            , drExportData("BkgTool") _
                                            , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                    strCellPos = "A" & i & ":" & "j" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                If Not pobjTvcs.InCdrBypassList(drExportData("TravelId")) Then
                    'Kiem tra Hierachies
                    Dim colRequiredHierachies As New Collection
                    strTcode = GetTcode(drExportData("BkgTool"), drExportData("Rloc"), drExportData("VoucherNbr"))

                    colRequiredHierachies = pobjTvcs.GetHierachies(drExportData("CMC"))
                    For Each objHierachy As clsHierachy In colRequiredHierachies
                        If drExportData("Hierachy" & objHierachy.Nbr) = "" Then
                            i = i + 1
                            Dim arrLine() As String
                            arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                    , drExportData("TravellerName") _
                                                    , "Missing Hierachy" & objHierachy.Nbr _
                                                    , drExportData("BkgTool") _
                                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                    , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                            strCellPos = "A" & i & ":" & "J" & i
                            objWs.Range(strCellPos).Value = arrLine
                        ElseIf objHierachy.CheckValues _
                            AndAlso Not pobjTvcs.CheckHierachyValueValid(drExportData("Cmc") _
                                        , objHierachy.Nbr, drExportData("Hierachy" & objHierachy.Nbr)) Then
                            i = i + 1
                            Dim arrLine() As String
                            arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                    , drExportData("TravellerName") _
                                                    , "Invalid Hierachy" & objHierachy.Nbr _
                                                    , drExportData("BkgTool") _
                                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                    , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                            strCellPos = "A" & i & ":" & "J" & i
                            objWs.Range(strCellPos).Value = arrLine
                        End If
                    Next

                    'Kiem tra CDRs
                    Dim colRequiredCDRs As New Collection
                    colRequiredCDRs = pobjTvcs.GetCDRs(drExportData("CMC"))
                    strTcode = GetTcode(drExportData("BkgTool"), drExportData("Rloc"), drExportData("VoucherNbr"))

                    For Each objCdr As clsCwtCdr In colRequiredCDRs
                        If drExportData("Ref" & objCdr.Nbr) = "" Then
                            i = i + 1
                            Dim arrLine() As String
                            arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                    , drExportData("TravellerName"), "Missing CDR" & objCdr.Nbr _
                                                    , drExportData("BkgTool") _
                                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                    , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                            strCellPos = "A" & i & ":" & "J" & i
                            objWs.Range(strCellPos).Value = arrLine

                        ElseIf Not objCdr.CheckFormat(drExportData("Ref" & objCdr.Nbr)) Then
                            i = i + 1
                            Dim arrLine() As String
                            arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                    , drExportData("TravellerName"), "CDR" & objCdr.Nbr & "-" _
                                                    & objCdr.FormatError, drExportData("BkgTool") _
                                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                    , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                            strCellPos = "A" & i & ":" & "J" & i
                            objWs.Range(strCellPos).Value = arrLine
                        End If
                    Next
                End If

                'Kiem tra thong tin Khach san
                If drExportData("HtlExist") AndAlso drExportData("HotelName") Is DBNull.Value Then
                    i = i + 1
                    Dim arrLine() As String
                    strTcode = GetTcode(drExportData("BkgTool"), drExportData("Rloc"), drExportData("VoucherNbr"))
                    arrLine = New String() {drExportData("Carrier"), drExportData("TKNO"), drExportData("RLOC") _
                                            , drExportData("TravellerName"), "Missing Hotel Info" _
                                             , drExportData("BkgTool") _
                                             , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                             , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                    strCellPos = "A" & i & ":" & "J" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If
                If Not drExportData("HotelName") Is DBNull.Value Then
                    strTcode = GetTcode(drExportData("BkgTool"), drExportData("Rloc"), drExportData("VoucherNbr"))
                    If Not pobjTvcs.CheckRoomTypeCwt(drExportData("RoomType")) Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), "Invalid room type" _
                                                 , drExportData("BkgTool") _
                                                 , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                 , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If

                    If drExportData("LocalIntlCodeType") <> "RVN" _
                        AndAlso Not pobjTvcs.CheckHarpKey(CInt(drExportData("LocalIntlCode")) _
                                        , drExportData("CityCode")) Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), "Invalid Harpkey" & drExportData("LocalIntlCode") _
                                                 , drExportData("BkgTool") _
                                                 , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                 , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If
                    If drExportData("ReferenceRate") < drExportData("RatePerRoomPerNight") Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                , drExportData("TravellerName"), "Reference Rate is lower than Paid Rate" _
                                                , drExportData("BkgTool"), pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If

                    If drExportData("RatePerRoomPerNight") < drExportData("LowestRate") Then
                        i = i + 1
                        Dim arrLine() As String
                        arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate"), drExportData("RLOC") _
                                                , drExportData("TravellerName") _
                                                , "Paid rate is lower than Lowest Rate" _
                                                , drExportData("BkgTool") _
                                                , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                        strCellPos = "A" & i & ":" & "J" & i
                        objWs.Range(strCellPos).Value = arrLine
                    End If

                    For j = 0 To UBound(arrHtlFields)
                        If Trim(drExportData(arrHtlFields(j))) = "" Then
                            i = i + 1
                            Dim arrLine() As String
                            arrLine = New String() {drExportData("HotelName"), drExportData("CheckInDate") _
                                                    , drExportData("RLOC"), drExportData("TravellerName") _
                                                    , arrHtlErrors(j), drExportData("BkgTool") _
                                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                                    , drExportData("TravelId"), strTcode, drExportData("TvlDOI")}
                            strCellPos = "A" & i & ":" & "J" & i
                            objWs.Range(strCellPos).Value = arrLine
                        End If
                    Next

                End If
            Loop
            .Columns("A:J").EntireColumn.AutoFit()
        End With
        If i > 1 Then
            Return False
        Else
            objWb.Close(False)
            objXl.Quit()
            Return True
        End If
    End Function
    Private Function ExportValidationVisa(ByVal drExportData As SqlClient.SqlDataReader) As Boolean
        Dim objXl As New Excel.Application
        Dim objWb As Workbook
        Dim objWs As New Worksheet
        Dim i As Integer = 1
        'Dim j As Integer
        Dim strCellPos As String
        Dim arrVisaFields() As String
        Dim arrVisaErrors() As String

        objXl.Visible = True
        objWb = objXl.Workbooks.Add
        objWs = objWb.ActiveSheet

        arrVisaFields = New String() {"RsCode", "MsCode", "CityCode"}
        arrVisaErrors = New String() {"Missing RealisedSaving Code", "Missing MissSaving Code", "Missing City Code"}

        With objWs
            .Cells(i, 1) = "SupplierName"
            .Cells(i, 2) = "Brief"
            .Cells(i, 4) = "TravellerName"
            .Cells(i, 5) = "Error"
            .Cells(i, 7) = "Corporates"
            .Cells(i, 8) = "TravelId"


            Do While drExportData.Read()
                If UCase(drExportData("TravellerName").ToString).StartsWith("MS") _
                    Or UCase(drExportData("TravellerName").ToString).StartsWith("MR") _
                    Or ContainSpecialChars(drExportData("TravellerName")) Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {drExportData("SupplierLocalName"), drExportData("Brief") _
                                            , drExportData("TravellerName") _
                                            , "Invalid Name format" _
                                            , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                            , drExportData("TravelId")}
                    strCellPos = "A" & i & ":" & "F" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                'Kiem tra thong tin Visa
                If drExportData("SupplierCode") Is DBNull.Value Then
                    i = i + 1
                    Dim arrLine() As String
                    arrLine = New String() {drExportData("SupplierLocalName"), drExportData("Brief") _
                                            , drExportData("TravellerName"), "SupplierCode" _
                                            , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                                             , drExportData("TravelId")}
                    strCellPos = "A" & i & ":" & "H" & i
                    objWs.Range(strCellPos).Value = arrLine
                End If

                'If Not pobjTvcs.InCdrBypassList(drExportData("TravelId")) Then

                '    'Kiem tra Hierachies
                '    Dim colRequiredHierachies As New Collection
                '    colRequiredHierachies = pobjTvcs.GetHierachies(drExportData("CMC"))
                '    For Each objHierachy As clsHierachy In colRequiredHierachies
                '        If drExportData("Hierachy" & objHierachy.Nbr) = "" Then
                '            i = i + 1
                '            Dim arrLine() As String
                '            arrLine = New String() {drExportData("SupplierLocalName"), drExportData("Brief") _
                '                                    , drExportData("TravellerName") _
                '                                    , "Missing Hierachy" & objHierachy.Nbr _
                '                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                '                                    , drExportData("TravelId")}
                '            strCellPos = "A" & i & ":" & "F" & i
                '            objWs.Range(strCellPos).Value = arrLine
                '        ElseIf objHierachy.CheckValues _
                '            AndAlso Not pobjTvcs.CheckHierachyValueValid(drExportData("Cmc") _
                '                        , objHierachy.Nbr, drExportData("Hierachy" & objHierachy.Nbr)) Then
                '            i = i + 1
                '            Dim arrLine() As String
                '            arrLine = New String() {drExportData("SupplierLocalName"), drExportData("Brief") _
                '                                    , drExportData("TravellerName") _
                '                                    , "Invalid Hierachy" & objHierachy.Nbr _
                '                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                '                                    , drExportData("TravelId")}
                '            strCellPos = "A" & i & ":" & "H" & i
                '            objWs.Range(strCellPos).Value = arrLine
                '        End If
                '    Next

                '    'Kiem tra CDRs
                '    Dim colRequiredCDRs As New Collection
                '    colRequiredCDRs = pobjTvcs.GetCDRs(drExportData("CMC"))
                '    For Each objCdr As clsCwtCdr In colRequiredCDRs
                '        If drExportData("Ref" & objCdr.Nbr) = "" Then
                '            i = i + 1
                '            Dim arrLine() As String
                '            arrLine = New String() {drExportData("SupplierLocalName"), drExportData("Brief") _
                '                                    , drExportData("TravellerName") _
                '                                    , "Missing CDR" & objCdr.Nbr _
                '                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                '                                    , drExportData("TravelId")}
                '            strCellPos = "A" & i & ":" & "F" & i
                '            objWs.Range(strCellPos).Value = arrLine

                '        ElseIf Not objCdr.CheckFormat(drExportData("Ref" & objCdr.Nbr)) Then
                '            i = i + 1
                '            Dim arrLine() As String
                '            arrLine = New String() {drExportData("SupplierLocalName"), drExportData("Brief") _
                '                                    , drExportData("TravellerName") _
                '                                    , "CDR" & objCdr.Nbr & "-" & objCdr.FormatError _
                '                                    , pobjTvcs.GetCorpNameByCmc(drExportData("CMC")) _
                '                                    , drExportData("TravelId")}
                '            strCellPos = "A" & i & ":" & "F" & i
                '            objWs.Range(strCellPos).Value = arrLine
                '        End If
                '    Next
                'End If


            Loop
            .Columns("A:F").EntireColumn.AutoFit()
        End With
        If i > 1 Then
            Return False
        Else
            objWb.Close(False)
            objXl.Quit()
            Return True
        End If
    End Function
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
        frmMain.Show()
    End Sub

    Private Sub frmExportGO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pobjTvcs.LoadComboDisplay(cboCustomer, "select distinct CustId as Value, CustShortName as Display" _
                    & " from GO_CompanyInfo1 Where Status<>'XX' ")

        cboCustomer.SelectedIndex = -1
        dtpFromDate.Value = Now.AddDays(-4)
        dtpToDate.Value = Now.AddDays(-4)
    End Sub
    Private Function GetTcode(strBkgTool As String, strRloc As String, strVoucherNbr As String) As String
        If strBkgTool = "MAN" Then
            Return pobjTvcs.GetScalarAsString("Select Tcode from ras12.dbo.dutoan_tour where Recid =" _
                                           & Mid(strVoucherNbr, 9))
        Else
            Return strRloc
        End If
    End Function

    Private Sub lbkFromDate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFromDate.LinkClicked
        dtpToDate.Value = dtpFromDate.Value
    End Sub
End Class