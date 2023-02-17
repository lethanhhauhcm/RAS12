Imports RAS12.MySharedFunctionsWzConn
Imports RAS12.MySharedFunctions
Module DataConvertMktg
    Private Cmd As SqlClient.SqlCommand = Conn.CreateCommand
    Private Sub GanShortNameChoBSP()
        Dim dTable As DataTable, CustShortName As String, CustCity As String, testIATACodeExist As Integer
        dTable = GetDataTable("select ID, AGTN from ua_hot where id in (select tkid from reportdata_BSP where custshortName='')")
        For i As Integer = 0 To dTable.Rows.Count - 1
            CustShortName = ScalarToString("MISC", "VAL1", "cat='BSPAGT' and VAL='" & dTable.Rows(i)("AGTN") & "'")
            If Not String.IsNullOrEmpty(CustShortName) Then
                CustCity = ScalarToString("MISC", "VAL2", "cat='BSPAGT' and VAL='" & dTable.Rows(i)("AGTN") & "'")
                Cmd.CommandText = "update ReportData_BSP set CustShortName ='" & CustShortName & "', City='" & CustCity & "' where TKID=" & dTable.Rows(i)("ID")
                Cmd.ExecuteNonQuery()
            Else
                testIATACodeExist = ScalarToInt("MISC", "RecID", "VAL='" & dTable.Rows(i)("AGTN") & "'")
                If testIATACodeExist = 0 Then
                    Cmd.CommandText = "insert misc (cat, val) values ('BSPAGT','" & dTable.Rows(i)("AGTN") & "')"
                    Cmd.ExecuteNonQuery()
                End If
            End If
        Next
    End Sub
    Public Sub GoDataConvert()
        If Conn.State <> ConnectionState.Open Then Conn.Open()
        Dim myRptData As New RptData, RTG As String, RecID As Integer, Amt As Decimal, ROE As Decimal
        Dim dTable As DataTable, Qry(2) As String, TblName(2) As String
        Dim ChayBSP As Int16 = 1
        Cmd.CommandText = "set datefirst 5"
        Cmd.ExecuteNonQuery()
        Dim strVnDomCities As String = GetColumnValuesAsString("CityCode", "Airport", "where Country='VN'", "_")
        Dim intChayRas As Int16
        Dim j As Integer

        ' Chi do lieu RAS neu lan do du lieu cuoi cach day nho hon 5 phut
        ' de tranh trung lap viec do du lieu TKT tao Dup TKID
        If ScalarToInt("MISC", "RecId", "CAT='GoDataConvert' and datediff(n,fstupdate,getdate())>5") > 0 Then
            intChayRas = 0
            ExecuteNonQuerry("update MISC set fstupdate=getdate() Where CAT ='GoDataConvert'", Conn)
        Else
            intChayRas = 1
        End If

        TblName(0) = " reportData"
        TblName(1) = TblName(0) & "_BSP"
        'Lay du lieu TKT do sang ReportData

        Qry(0) = "Select t.recid, Itinerary, Farebasis, DOI, ROEID, ROE, (fare+tax)*Qty + t.Charge As Amt, CustID, Fare*qty As Fare, Tax*qty As Tax, " _
                & "t.Charge, ChargeTV, CommVAL*qty As CommVAL, NetToAL*qty As NetToAL, r.Currency, left(t.RCPNO,2) As Counter, BkgClass, t.AL, AgtDisctVAL" _
                & ",DocType,Tkno,r.Counter As BizUnit,t.RMK,r.Location " & DKDataConvertMktg_RAS
        '_
        '        & " and t.Recid=742259"


        'Lay du lieu hotData do sang ReportData_BSP
        Qry(1) = "Select ID As RecID, Routing As Itinerary, Farebasis, DAIS, [Class] As BkgClass, TDNR, TRNC, NTFA, COBL, FareCur, 0 As CustID, NetRev As Amt, AL" &
            DKDataConvertMktg_BSP
        If myStaff.City = "HAN" Then ChayBSP = 0

        For j = intChayRas To ChayBSP
            dTable = GetDataTable(Qry(j))
            For i As Int16 = 0 To dTable.Rows.Count - 1
                Dim strDomRtg As String = String.Empty     'Xac dinh hanh trinh DOM cho CWT
                Amt = dTable.Rows(i)("Amt")
                RTG = dTable.Rows(i)("Itinerary").ToString.Trim

                If RTG.Trim = "" Then
                    'If j = 1 Then
                    RTG = "XXX YY XXX"
                    '    'ElseIf dTable.Rows(i)("DocType") = "MCO" AndAlso dTable.Rows(i)("BizUnit") = "CWT" _
                    '    '    AndAlso dTable.Rows(i)("RMK").ToString.StartsWith("BIZT|") _
                    '    '    AndAlso myStaff.Counter = "CWT" Then
                    '    '    MsgBox("Missing Itinerary For ticket " & dTable.Rows(i)("TKNO") & ".Please report NMK!")
                    '    '    GoTo resumeHere
                    'Else
                    '    GoTo resumeHere
                    'End If
                End If
                If Not RTG.Contains(" ") Then
                    RTG = AddSpace2Rtg(RTG)
                End If
                If j = 0 Then
                    myRptData.AL = dTable.Rows(i)("AL")
                    myRptData.DOI = dTable.Rows(i)("DOI")
                    myRptData.Counter = dTable.Rows(i)("Counter")
                    myRptData.Location = dTable.Rows(i)("Location")

                    ROE = dTable.Rows(i)("ROE")
                    myRptData.VND = Amt * ROE
                    myRptData.F_VND = dTable.Rows(i)("Fare") * ROE
                    myRptData.T_VND = dTable.Rows(i)("Tax") * ROE
                    myRptData.C_VND = dTable.Rows(i)("Charge") * ROE
                    myRptData.TVCharge_VND = dTable.Rows(i)("ChargeTV") * ROE
                    myRptData.Comm_VND = dTable.Rows(i)("CommVAL") * ROE
                    myRptData.toAL_VND = dTable.Rows(i)("NetToAL") * ROE
                    myRptData.Disct_VND = dTable.Rows(i)("AgtDisctVAL") * ROE

                    ROE = ROEOfTixByVND(dTable.Rows(i)("Currency"), dTable.Rows(i)("ROEID"))
                    myRptData.USD = Amt / ROE
                    myRptData.F_USD = dTable.Rows(i)("Fare") / ROE
                    myRptData.T_USD = dTable.Rows(i)("Tax") / ROE
                    myRptData.C_USD = dTable.Rows(i)("Charge") / ROE
                    myRptData.TVCharge_USD = dTable.Rows(i)("ChargeTV") / ROE
                    myRptData.Comm_USD = dTable.Rows(i)("CommVAL") / ROE
                    myRptData.toAL_USD = dTable.Rows(i)("NetToAL") / ROE
                    myRptData.Disct_USD = dTable.Rows(i)("AgtDisctVAL") / ROE
                    strDomRtg = DefineDomRtg(pstrVnDomCities, RTG.Trim)
                Else
                    If dTable.Rows(i)("DAIS").ToString.Trim = "" Then GoTo resumeHere
                    'If (RTG.Length - 3) Mod 7 <> 0 Then GoTo resumeHere
                    If (RTG.Length - 3) Mod 7 <> 0 Then RTG = "XXX YY XXX"
                    myRptData.AL = dTable.Rows(i)("AL")
                    If myRptData.AL.Trim = "" Then myRptData.AL = ScalarToString("Airline", "AL", " docCode='" & dTable.Rows(i)("TDNR").ToString.Substring(0, 3) & "'")
                    myRptData.Counter = myRptData.AL
                    myRptData.DOI = DateSerial(dTable.Rows(i)("DAIS").ToString.Substring(0, 2), dTable.Rows(i)("DAIS").ToString.Substring(2, 2), dTable.Rows(i)("DAIS").ToString.Substring(4, 2))
                    'Amt = IIf(dTable.Rows(i)("NTFA") > 0, dTable.Rows(i)("NTFA"), dTable.Rows(i)("COBL"))

                    If myRptData.DOI > "30 jun 2017 23:59" Then
                        ROE = GetUsdGdsRoeByDoi(myRptData.DOI)
                    Else
                        ROE = ForEX_12(myStaff.City, myRptData.DOI, "USD", "BSR", myRptData.AL).Amount
                    End If

                    If dTable.Rows(i)("FareCur") = "VND" Then
                        myRptData.VND = Amt
                        myRptData.USD = Amt / ROE
                    Else
                        myRptData.VND = Amt * ROE
                        myRptData.USD = Amt
                    End If
                End If
                RecID = dTable.Rows(i)("RecID")
                myRptData.OrgCity = RTG.Substring(0, 3)
                myRptData.Tuan = DatePart(DateInterval.WeekOfYear, myRptData.DOI)
                myRptData.Thang = Month(myRptData.DOI)
                myRptData.Nam = Year(myRptData.DOI)
                myRptData.Ngay = myRptData.DOI.Day

                If RTG = "XXX YY XXX" Then ' Empty route
                    myRptData.OrgCountry = ".."
                    myRptData.OrgArea = "..."
                    myRptData.DomInt = "..."
                    myRptData.OWRT = ".."
                    myRptData.Dest = "..."
                    myRptData.DestCity = "..."
                    myRptData.Country = ".."
                    myRptData.Area = "..."
                Else
                    myRptData.OrgCountry = CityAPTToCountry_Area_City("Country", myRptData.OrgCity)
                    myRptData.OrgArea = CityAPTToCountry_Area_City("Area", myRptData.OrgCity)
                    myRptData.DomInt = DefindDomInt(myRptData.OrgCountry, RTG)
                    myRptData.OWRT = DefineOWRT_New(RTG, myRptData.DomInt, myRptData.OrgCountry)
                    myRptData.Dest = DefineDest_new(RTG, myRptData.OWRT, myRptData.OrgCountry, myRptData.DomInt)
                    myRptData.DestCity = CityAPTToCountry_Area_City("City", myRptData.Dest)
                    myRptData.Country = CityAPTToCountry_Area_City("Country", myRptData.Dest)
                    myRptData.Area = CityAPTToCountry_Area_City("Area", myRptData.Dest)
                End If
                myRptData.Cabin = DefineCabin(dTable.Rows(i)("BkgClass"), myRptData.AL)
                myRptData.stFB = DefineFB(dTable.Rows(i)("FareBasis"), myRptData.Counter)
                Cmd.CommandText = "insert " & TblName(j) & " (TKID, Dest, OWRT, DomInt, DeKho, FB, Cabin, USD, VND, Country, Area, tuan, " &
                    "Thang, Nam, Counter, OrgCity, OrgCountry, OrgArea, DestCity, Ngay"
                If j = 0 Then
                    Cmd.CommandText = Cmd.CommandText & ", F_USD, T_USD, C_USD, TVCharge_USD, Comm_USD, Net2AL_USD" &
                        ", F_VND, T_VND, C_VND, TVCharge_VND, Comm_VND, Net2AL_VND, Disct_USD, Disct_VND,Location"
                End If
                Cmd.CommandText = Cmd.CommandText & ") values (" & RecID & ",'" & myRptData.Dest & "','" & myRptData.OWRT & "','" &
                    myRptData.DomInt & "','" & strDomRtg & "','" & myRptData.stFB & "','" & myRptData.Cabin & "'," &
                    myRptData.USD & "," & myRptData.VND & ",'" & myRptData.Country & "','" & myRptData.Area & "'," & myRptData.Tuan &
                    "," & myRptData.Thang & "," & myRptData.Nam & ",'" & myRptData.Counter & "','" & myRptData.OrgCity &
                    "','" & myRptData.OrgCountry & "','" & myRptData.OrgArea & "','" & myRptData.DestCity & "'," & myRptData.Ngay
                If j = 0 Then
                    Cmd.CommandText = Cmd.CommandText & "," & myRptData.F_USD & "," & myRptData.T_USD & "," & myRptData.C_USD & "," &
                        myRptData.TVCharge_USD & "," & myRptData.Comm_USD & "," & myRptData.toAL_USD & "," &
                        myRptData.F_VND & "," & myRptData.T_VND & "," & myRptData.C_VND & "," & myRptData.TVCharge_VND & "," &
                        myRptData.Comm_VND & "," & myRptData.toAL_VND & "," & myRptData.Disct_USD & "," & myRptData.Disct_VND _
                        & ",'" & myRptData.Location & "'"
                End If
                Cmd.CommandText = Cmd.CommandText & ")"
                Cmd.ExecuteNonQuery()
resumeHere:
            Next

        Next
        If myStaff.City = "SGN" Then
            If j > 1 Then
                GanShortNameChoBSP()
            End If
            DefineRtgType4LocalCorp()
            Call BreakBySegment()
            UpdateRtgType4Cwt()
            CalcVat4Cwt()

        End If
    End Sub
    Private Function DefineRtgType4LocalCorp() As Boolean
        'Bo sung them cac truowng hop khong xac dinh duoc Routing Type cua Local corp
        Dim tblTkt As DataTable = GetDataTable("SELECT * FROM ReportData r1 " _
                                                & " Left Join tkt t on r1.TKID=t.RecID" _
                                                & " Left Join rcp r on r.RecID=t.RCPID" _
                                                & " where t.DocType ='atk' and r1.DomInt<>'' and DeKho='' and r.CustType='lc'")
        Dim lstQuerries As New List(Of String)
        Dim strRtgType As String
        For Each objRow As DataRow In tblTkt.Rows
            If objRow("DomInt") = "DOM" AndAlso objRow("OrgCountry") = "VN" Then
                strRtgType = "DOM"
            Else
                strRtgType = "INTL"
            End If
            lstQuerries.Add("Update ReportData set DeKho='" & strRtgType & "' where RecId=" & objRow("RecId"))
        Next
        If lstQuerries.Count > 0 Then
            Return UpdateListOfQuerries(lstQuerries, Conn)
        End If
        Return True
    End Function
    Private Function GetRtgType4Country(intCustId As String, dteDOI As Date _
    , strCountry As String) As String
        Dim tblRtgType As DataTable
        Dim strQuerry As String
        Dim strRtgType As String

        If strCountry = "VN" Then
            strRtgType = "DOM"
        Else
            strQuerry = "Select top 1 Val from Misc where Status='OK' and Cat='RtgTypeCwt' and intVal =" _
            & intCustId & " and Val1 like '%" & strCountry & "%' and '" _
            & Format(dteDOI, "dd MMM yy") & "' between dteVal and dteVal1" _
            & " order by dteVal desc"

            tblRtgType = GetDataTable(strQuerry, Conn)
        'Pending
        If tblRtgType.Rows.Count = 0 Then
            strRtgType = "INTL"
        Else
            strRtgType = tblRtgType.Rows(0)("Val")
        End If

        End If

        Return strRtgType
    End Function
    Private Function GetRtgType4Itinerary(intCustId As String, dteDOI As Date _
    , strCountries As String) As String
        Dim arrCountries() As String
        Dim strRtgType As String = String.Empty
        Dim i As Integer

        arrCountries = Split(strCountries, ",")

        For i = 0 To UBound(arrCountries)
            strRtgType = strRtgType & GetRtgType4Country(intCustId, dteDOI, arrCountries(i))
            If strRtgType.Contains("INTL") Then
                Return "INTL"
            End If
        Next
        If InStr(strRtgType, "REG1") > 0 Then
            strRtgType = "REG1"
        ElseIf InStr(strRtgType, "REG2") > 0 Then
            strRtgType = "REG2"
        ElseIf InStr(strRtgType, "REG") > 0 Then
            strRtgType = "REG"
        ElseIf InStr(strRtgType, "DOM") > 0 Then
            strRtgType = "DOM"
        End If
        Return strRtgType

    End Function

    Public Function UpdateRtgType4Cwt()
        Dim tblTkt As DataTable
        Dim strQuerry As String

        'chinh DEKHO theo kieu Routing type cua CWT
        strQuerry = "Select RD.*,T.SRV,T.DOI,R.CUSTID" _
            & ", (Case Substring(TKNO,1,1) When 'Z' then 'LCC' else 'FSC' end) as AlType " _
            & ", Itinerary, Tkno" _
            & " FROM ReportData RD" _
            & " LEFT JOIN TKT T ON T.RECID=RD.TKID " _
            & " LEFT JOIN RCP R ON T.RCPNO=R.RCPNO " _
            & " where DEKHO ='' and T.Status<>'XX' and T.AL<>'01'" _
            & " and (QTY=1 or QTY=-1) and T.SRV<>'V' and R.STATUS='OK' and DOCTYPE in('ETK','MCO','ATK')" _
            & " and T.DOI >='01 JAN 19'" _
            & " and r.COUNTER='CWT' and Rd.Completed<1"
        '& " and T.DOI between DATEADD(d,-14,getdate()) and getdate()" _

        tblTkt = GetDataTable(strQuerry, Conn)

        For Each objRow As DataRow In tblTkt.Rows
            Dim strRtgType As String = String.Empty
            Dim strCountries As String = objRow("OrgCountry") & "," & objRow("Country")

            If Len(objRow("Itinerary")) > 10 Then
                For i As Integer = 8 To Len(objRow("Itinerary")) - 7 Step 7
                    Dim strCountry As String = ScalarToString("CityCode", "Country", "Airport='" & Mid(objRow("Itinerary"), i, 3) & "'")
                    If InStr(strCountries, strCountry) = 0 Then
                        strCountries = strCountries & "," & strCountry
                    End If
                Next
            End If
            strRtgType = GetRtgType4Itinerary(objRow("CustId"), objRow("DOI") _
                        , strCountries)
            ExecuteNonQuerry("Update ReportData set Completed=1,Dekho='" & strRtgType _
                             & "',Pc='" & My.Computer.Name & "' where RecId=" & objRow("RecId"), Conn)
        Next

        'Loop
        Return True
    End Function
    Public Function RelinkTktAir(dteFromDate As Date, dteToDate As Date) As Boolean
        Dim strQuerry As String
        Dim lstQuerries As New List(Of String)
        Dim tblAir As DataTable
        Dim intNewTkid As Integer

        '^_^20221019 mark by 7643 -b-
        'strQuerry = "select  t.RecID as TktId,air.RecID as AirId,t.Tkno,t.AL,t.Srv" _
        '            & " from cwt.dbo.GO_Air air" _
        '            & " left join ras12.dbo.tkt t on t.SRV=air.SRV and air.Carrier=t.AL " _
        '            & " and air.Tkno=t.ShortTkno" _
        '            & " where t.Qty<>0 And t.Status='xx'" _
        '            & " and t.RecId not in (select intVal from cwt.dbo.go_Misc where Cat='CheckedXxTkid')" _
        '            & " and  air.Tkid>0 and air.DOI between '" & CreateFromDate(dteFromDate) _
        '            & "' AND '" & CreateToDate(dteToDate) & "' and t.CustId in " _
        '            & " (Select CustId From CWT.DBO.GO_CompanyInfo1 where Status='OK' and TMC<>'RM')"
        '^_^20221019 mark by 7643 -e-
        '^_^20221019 modi by 7643 -b-
        strQuerry = "select  t.RecID as TktId,air.RecID as AirId,t.Tkno,t.AL,t.Srv" _
                    & " from cwt.dbo.GO_Air air" _
                    & " left join ras12.dbo.tkt t on t.SRV=air.SRV and air.Carrier=t.AL " _
                    & " and air.Tkno=t.ShortTkno left join ras12.dbo.RCP rcp on t.RCPID=rcp.RecID" _
                    & " where t.Qty<>0 And t.Status='xx'" _
                    & " and t.RecId not in (select intVal from cwt.dbo.go_Misc where Cat='CheckedXxTkid')" _
                    & " and  air.Tkid>0 and air.DOI between '" & CreateFromDate(dteFromDate) _
                    & "' AND '" & CreateToDate(dteToDate) & "' and rcp.CustId in " _
                    & " (Select CustId From CWT.DBO.GO_CompanyInfo1 where Status='OK' and TMC<>'RM')"
        '^_^20221019 modi by 7643 -e-

        tblAir = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblAir.Rows
            intNewTkid = ScalarToDec("Tkt", "top 1 RecId", "Status<>'XX' and Tkno='" _
                        & objRow("Tkno") & "' and Srv='" & objRow("Srv") & "' and Recid>" & objRow("Tktid") _
                        & " order by RecId")
            If intNewTkid > 0 Then
                lstQuerries.Clear()
                lstQuerries.Add("Update cwt.dbo.GO_Air set Tkid=" & intNewTkid & " where RecId=" & objRow("AirId"))
                lstQuerries.Add("insert cwt.dbo.GO_Misc (Cat,intVal) values ('CheckedXxTkid'," & objRow("Tktid") & ")")
                If Not UpdateListOfQuerries(lstQuerries, Conn) Then
                    MsgBox("Unable to Relink Tkt in RAS with GO_AIR. Tkno=" & objRow("Tkno"))
                End If
            End If
        Next

        Return True
    End Function
    Public Function LinkTktGoAir() As Boolean
        Dim tblTkt As Data.DataTable
        Dim strQuerry As String = "select t.RecId, t.DOI, a.DocType, a.RecID as GoAirId" _
                                    & " From tkt t" _
                                    & " left join rcp r on t.RCPID=r.RecID" _
                                    & " left join cwt.dbo.GO_Air a on t.ShortTkno=a.tkno and t.SRV=a.SRV and t.AL=a.Carrier and t.DOI=a.doi and t.DocType=a.DocType" _
                                    & " where qty<>0 And r.CustType In ('CS','LC')" _
                                    & " And a.Tkid=0 and t.DOI>='01 jan 19' and t.Status<>'XX'"

        tblTkt = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblTkt.Rows
            ExecuteNonQuerry("Update Cwt.dbo.Go_air set Tkid=" _
                             & objRow("RecId") & " where Recid=" & objRow("GoAirId"), Conn)
        Next
        Return True
    End Function
    Public Function CalcLastDOF() As Boolean
        Dim tblTkt As Data.DataTable
        Dim strQuerry As String = "Select t.DOI, Right(a.DepDates, 5) As FltDate, r1.RecId" _
            & " from tkt t left join rcp r On t.RcpId=r.RecId" _
            & " left join ReportData r1 On t.RecId=r1.Tkid" _
            & " left join cwt.dbo.go_air a On t.RecId=a.Tkid" _
            & " where t.Status<>'XX' and t.Srv='S'" _
            & " and t.DocType='ETK' and substring(t.rmk,1,4)='BIZT'" _
            & " and r.CustId in (select intVal from Misc where Status='OK' and cat='custnameingroup' and val='get_last_dof')" _
            & " and r1.LastDOF is null" _
            & " and not a.DepDates is null"

        tblTkt = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblTkt.Rows
            If objRow("FltDate") <> "" Then
                ExecuteNonQuerry("Update ReportData set LastDOF='" _
                             & CreateFromDate(AddFutureYear(objRow("DOI"), objRow("FltDate"))) _
                             & "' where Recid=" & objRow("RecId"), Conn)
            End If

        Next

    End Function
    Private Function CalcVat4Cwt() As Boolean
        Dim tblReportData As System.Data.DataTable
        Dim i As Integer
        Dim decVatRatio As Decimal

        Dim strQuerry As String = "SELECT T.Tax_D, t.Charge_D,T.SRV, R.*, C.CustShortName, M.intVal, t.DOI,t.Tkno " _
                & " FROM REPORTDATA R" _
                & " LEFT JOIN TKT T ON R.TKID=T.RecID" _
                & " LEFT JOIN RCP C ON C.RECID=T.RCPID" _
                & " LEFT JOIN MISC M ON M.IntVal=C.CustID AND M.CAT='ZEROVAT4SF' AND M.Status='OK'" _
                & " WHERE T.Status<>'XX' AND T.DOI>'31 DEC 2018 23:59'" _
                & " AND R.Vat4TfcNoUE_VND IS NULL AND R.Counter='TS' AND R.DEKHO<>''" _
                & " AND C.Status<>'XX'"
        'Dim strQuerry As String = "SELECT T.Tax_D, t.Charge_D,T.SRV, R.*, C.CustShortName " _
        '        & " FROM REPORTDATA R" _
        '        & " LEFT JOIN TKT T ON R.TKID=T.RecID" _
        '        & " LEFT JOIN RCP C ON C.RECID=T.RCPID" _
        '        & " WHERE T.Status<>'XX' AND T.DOI>'31 DEC 2017 23:59'" _
        '        & " AND R.SfNoVat_VND IS NULL AND R.Counter='TS' AND R.DEKHO<>''" _
        '        & " AND C.Status<>'XX'"
        tblReportData = GetDataTable(strQuerry, Conn)
        With tblReportData
            For i = 0 To .Rows.Count - 1
                Dim decVat4Fare As Decimal = 0
                Dim decDomAptSvc As Decimal = 0
                Dim decVat4DomAptSvc As Decimal = 0
                Dim decVat4AlCharge As Decimal = 0
                Dim decVat4TvCharge As Decimal = 0
                Dim decVatTotal As Decimal = 0
                Dim decSfNoVat As Decimal = 0
                Dim decVAT4TfcNoUE As Decimal = 0

                If .Rows(i)("srv") = "S" Then
                    decVatRatio = GetVatRatio(.Rows(i)("DOI"))
                Else
                    Dim dteDOI4Sale As Date = ScalarToDate("tkt", "TOP 1 DOI", " SRV='S' and status<>'XX' And Tkno='" _
                    & .Rows(i)("tkno") & "' order by Recid desc")
                    decVatRatio = GetVatRatio(dteDOI4Sale)
                End If


                If .Rows(i)("DOMINT") = "DOM" Then
                    decVat4Fare = GetTaxAmtFromTaxDetails("UE", .Rows(i)("Tax_D"))
                    decDomAptSvc = (.Rows(i)("T_VND") - decVat4Fare)
                    decVat4DomAptSvc = decDomAptSvc - (decDomAptSvc / decVatRatio)
                    If .Rows(i)("SRV") = "R" Then
                        decVat4Fare = -decVat4Fare
                        decVat4DomAptSvc = -decVat4DomAptSvc
                    Else
                        decVat4AlCharge = .Rows(i)("C_VND") - (.Rows(i)("C_VND") / decVatRatio)
                    End If
                    decVAT4TfcNoUE = (.Rows(i)("T_VND") - decVat4Fare) - ((.Rows(i)("T_VND") - decVat4Fare) / decVatRatio)
                    'decVAT4TfcNoUE = ((.Rows(i)("T_VND") - decVat4Fare) * 0.1) / decVatRatio
                End If

                'Khong thu thue VAT cho GEVNHPH , PG INDOCHINA
                If IsDBNull(.Rows(i)("intVal")) Then
                    decSfNoVat = .Rows(i)("TVCharge_VND") / decVatRatio
                Else
                    decSfNoVat = .Rows(i)("TVCharge_VND")
                End If
                decVat4TvCharge = .Rows(i)("TVCharge_VND") - decSfNoVat

                decVatTotal = decVat4Fare + decVat4DomAptSvc + decVat4AlCharge + decVat4TvCharge
                strQuerry = "Update ReportData set VAT_VND=" & decVatTotal _
                    & ",SfNoVat_VND=" & decSfNoVat & ",Vat4Fare_VND=" & decVat4Fare _
                    & ",Vat4TfcNoUE_VND=" & decVAT4TfcNoUE _
                    & " where Tkid=" & .Rows(i)("TKID")
                ExecuteNonQuerry(strQuerry, Conn)
            Next
        End With

    End Function
    Private Structure RptData
        Public OWRT As String
        Public Dest As String
        Public Country As String
        Public Area As String
        Public DomInt As String
        Public stFB As String
        Public Cabin As String
        Public Counter As String
        Public AL As String
        Public USD As Decimal
        Public VND As Decimal
        Public Ngay As Int16
        Public Tuan As Int16
        Public Thang As Int16
        Public Nam As Int16
        Public DOI As Date
        Public DestCity As String
        Public OrgCountry As String
        Public OrgArea As String
        Public OrgCity As String
        Public Qty As Int16
        Public F_USD As Decimal
        Public T_USD As Decimal
        Public C_USD As Decimal
        Public TVCharge_USD As Decimal
        Public Comm_USD As Decimal
        Public toAL_USD As Decimal
        Public F_VND As Decimal
        Public T_VND As Decimal
        Public C_VND As Decimal
        Public TVCharge_VND As Decimal
        Public Comm_VND As Decimal
        Public toAL_VND As Decimal
        Public Disct_VND As Decimal
        Public Disct_USD As Decimal
        Public Location As String

    End Structure
    Private Function DefineCabin(pRBD As String, pAL As String) As String
        Dim KQ As String = "Y", FC As String, f As String, C As String
        FC = ScalarToString("RBD_Cabin", " F + '_' + C ", " al='" & pAL & "' and status='OK' ")
        pRBD = pRBD.Replace("+", "")
        If FC <> "" AndAlso FC <> "_" Then
            f = FC.Split("_")(0)
            C = FC.Split("_")(1)
            For i As Int16 = 0 To Len(pRBD) - 1
                If InStr(f, pRBD.Substring(i, 1)) > 0 Then
                    KQ = "F"
                    Exit For
                ElseIf InStr(C, pRBD.Substring(i, 1)) > 0 Then
                    KQ = "C"
                    Exit For
                End If
            Next
        End If
        Return KQ
    End Function
    Private Function DefindDomInt(pOrgCountry As String, ByVal pRTG As String) As String
        Dim SoChang As Int16

        If pOrgCountry <> "VN" Then
            Return "INT"
        End If
        SoChang = (pRTG.Length - 3) / 7
        For i As Int16 = 1 To SoChang
            If CityAPTToCountry_Area_City("Country", pRTG.Substring(7 * i, 3)) <> pOrgCountry Then
                Return "INT"
            End If
        Next
        Return "DOM"
    End Function
    Public Function DefineOWRT_New(ByVal pRTG As String, ByVal pDomINT As String, pOrgCountry As String) As String
        Dim OWRT As String = "??", ViTriGiua As Integer, LstCountry As String
        Dim SoChang As Integer, TPi As String, TPDoiXung As String
        If pRTG.Length < 10 Then Return "??"
        If pRTG.Length = 10 Then Return "OW"
        If pDomINT = "DOM" Then
            If pRTG.Substring(0, 3) <> Strings.Right(pRTG, 3) Then
                Return "OW"
            Else
                Return "RT"
            End If
        Else
            LstCountry = CityAPTToCountry_Area_City("Country", Strings.Right(pRTG, 3))
            If pOrgCountry <> LstCountry Then
                Return "OW"
            Else
                SoChang = (pRTG.Length - 3) / 7
                If SoChang / 2 > Int(SoChang / 2) Then
                    OWRT = "CT"
                Else
                    OWRT = "RT"
                    ViTriGiua = (SoChang / 2) + 1
                    For i As Int16 = 2 To ViTriGiua - 1
                        TPi = pRTG.Substring(7 * i - 6 - 1, 3)
                        TPDoiXung = pRTG.Substring(7 * (SoChang + 2 - i) - 6 - 1, 3)
                        If TPi <> TPDoiXung Then
                            OWRT = "CT"
                            Exit For
                        End If
                    Next
                End If
            End If
        End If
        Return OWRT
    End Function
    Private Function CleanRTG(ppRTG As String) As String
        Dim KQ As String, SoChang As Int16, ChangI As String, CT As String
        SoChang = (ppRTG.Length - 3) / 7
        If Not ppRTG.Contains("/") Or SoChang < 3 Then Return ppRTG
        KQ = ppRTG
        For i As Int16 = 2 To SoChang - 1
            ChangI = ppRTG.Substring(7 * (i - 1), 10)
            If ChangI.Contains("/") Then
                CT = CityAPTToCountry_Area_City("City", ChangI.Substring(0, 3))
                If CT = CityAPTToCountry_Area_City("City", Strings.Right(ChangI, 3)) Then
                    KQ = KQ.Replace(ChangI, CT)
                End If
            End If
        Next
        Return KQ
    End Function
    Public Function DefineDest_new(ByVal pRTG As String, ByVal pOWRT As String, pOrgCountry As String _
                                    , pDomInt As String) As String
        Dim Dest As String = "???", ViTriGiua As Integer, SoChang As Integer, isChanChang As Boolean
        Dim tblCityCode As New System.Data.DataTable
        Dim strFirstArea As String = String.Empty
        Dim intTC As Integer


        If pRTG.Length > 9 Then
            pRTG = CleanRTG(pRTG)
            SoChang = (pRTG.Length - 3) / 7

            Select Case pOWRT
                Case "OW"
                    Return Strings.Right(pRTG, 3)
                Case Else
                    isChanChang = IIf(SoChang Mod 2 = 0, True, False)
                    ViTriGiua = (SoChang / 2) + 1
                    If pOWRT = "RT" Then
                        Return pRTG.Substring(7 * ViTriGiua - 7, 3)
                    Else
                        Dest = pRTG.Substring(7 * ViTriGiua - 7, 3)
                        intTC = 4
                    End If
            End Select

            If pDomInt = "DOM" Then
                Return pRTG.Substring(7 * ViTriGiua - 7, 3)
            Else
                For i As Int16 = 1 To SoChang
                    tblCityCode = GetDataTable("Select C.*, substring(c1.TC,3,1) as TC from CityCode C" _
                                                    & " Left join Country c1 on c.Country=c1.Country" _
                                                    & " where Airport='" & pRTG.Substring(7 * i, 3) & "'")
                    If tblCityCode.Rows.Count = 0 Then
                        MsgBox("Unable to find airport code " & pRTG.Substring(7 * i, 3))
                        Return ""
                    End If
                    If tblCityCode.Rows(0)("Country") <> "VN" Then
                        If intTC < CInt(tblCityCode.Rows(0)("TC")) Then
                            Return Dest
                        Else
                            Dest = tblCityCode.Rows(0)("City")
                            intTC = tblCityCode.Rows(0)("TC")
                        End If


                    End If
                Next
            End If
        End If
        Return Dest
    End Function
    Private Function ROEOfTixByVND(ByVal pCurr As String, ByVal pROEID As Integer) As Decimal
        Dim KQ As Decimal
        If pCurr <> "VND" Then
            Return 1
        Else
            Cmd.CommandText = "select BSR from forex where recid=" & pROEID
            KQ = Cmd.ExecuteScalar
            Return IIf(KQ = 0, 21000, KQ)
        End If
    End Function
    Private Function DefineFB(ByVal pFB As String, ByVal pAL As String) As String
        If pFB = "" Then pFB = "Y"
        Cmd.CommandText = "insert into FB_RBD_QT (CAT, AL, Raw) values ('FB','" & pAL & "','" & pFB.Split("+")(0) & "')"
        Cmd.ExecuteNonQuery()
        Return pFB.Split("+")(0)
    End Function
    Private Sub BreakBySegment()
        Cmd.CommandText = "Delete from Segment where tkID in (select recid from tkt where status ='XX')"
        Cmd.ExecuteNonQuery()
        Dim strQry As String = "select RecID, Itinerary, BkgClass, FareBasis from tkt " &
            " where al='VN' and qty <>0 and status <>'XX' and DocType='ETK'" _
            & " And RecID Not in (select TKID from Segment) And len(itinerary) >8 "
        Dim Rtg As String, Seg As Int16, RBD As String, FB As String
        Dim dTable As DataTable = GetDataTable(strQry)
        For i As Int16 = 0 To dTable.Rows.Count - 1
            Rtg = dTable.Rows(i)("Itinerary").trim
            If Strings.Right(Rtg, 3).Contains(" ") Then
                Rtg = Rtg.Replace(" ", "")
                Rtg = Rtg.Replace("NHACXR", "CXR")
                Rtg = Rtg.Replace("TMKVCL", "TMK")
                Rtg = AddSpace2Rtg(Rtg).Trim
            End If
            RBD = dTable.Rows(i)("BkgClass")
            FB = dTable.Rows(i)("FareBasis")
            Seg = (Rtg.Length - 3) / 7
            If FB.Split("+").Length < Seg Then FB = FillFB_byRTG(Rtg, FB)
            If Seg > RBD.Length Then RBD = FillRBD_byRTG(Rtg, RBD)
            strQry = ""
            For j As Int16 = 1 To Seg
                If Rtg.Substring(7 * j - 7, 10).Substring(4, 2) <> "//" Then
                    strQry = strQry & "; insert Segment (TKID, Segment, RBD, FB) values (" & dTable.Rows(i)("RecID") & _
                        ",'" & Rtg.Substring(7 * j - 7, 10) & "','" & RBD.ToString.Substring(j - 1, 1) & "','" & _
                        FB.Split("+")(j - 1) & "')"
                End If
            Next
            Cmd.CommandText = strQry.Substring(1)
            If strQry.Length > 2 Then Cmd.ExecuteNonQuery()
        Next
    End Sub
End Module
