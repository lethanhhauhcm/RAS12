Imports Microsoft.SqlServer

Public Class clsTvcs
    Dim mcnxConnection As New SqlClient.SqlConnection
    Dim mcnxConnection2 As SqlClient.SqlConnection
    Dim mcmdSql As New SqlClient.SqlCommand

    Dim mstrCnxErr As String
    Dim mstrUpdtErr As String
    Dim mstrConnectionString As String = "server=118.69.81.103;uid=user_cwt;pwd=VietHealthy@170172#;database=CWT;Connect Timeout=300"
    Dim msglLastInsertedId As Single


    Public Function Start(ByVal strLogFileName As String _
                    , Optional ByVal blnGet2Conx As Boolean = True) As Boolean
        If Not GetConnecttionString(strLogFileName) Then
            Return False
        End If
        If Not Connect() Then
            Return False
        End If
        If blnGet2Conx AndAlso Not Connect2() Then
            Return False
        End If
        Return True

    End Function

    Public Function GetConnecttionString(ByVal strFileName As String) As Boolean
        Dim objFile As System.IO.StreamReader
        Dim strFullPath As String

        strFullPath = System.AppDomain.CurrentDomain.BaseDirectory() & "\" & strFileName
        Try
            objFile = New System.IO.StreamReader(strFullPath)
            mstrConnectionString = objFile.ReadLine() & ";uid=user_cwt;pwd=VietHealthy@170172#;database=CWT;Connect Timeout=300"
            objFile.Close()
            objFile.Dispose()
            GetConnecttionString = True
        Catch ex As Exception
            mstrCnxErr = ex.Message
            GetConnecttionString = False
        End Try

    End Function
    'Public Function ConnectSqlWeb(strDatabase As String) As Boolean

    '    Dim strConx As String = "server=42.117.5.70;uid=user_" & strDatabase _
    '                            & ";pwd=VietHealthy@170172#;database=" & strDatabase & ";Connect Timeout=300;"
    '    mstrConnectionString = strConx
    '    Try
    '        mcnxConnection = New SqlClient.SqlConnection(strConx)
    '        mcnxConnection.Open()
    '        mcmdSql = mcnxConnection.CreateCommand

    '    Catch ex As Exception
    '        mstrCnxErr = ex.Message
    '        Return False
    '    End Try
    '    Return True
    'End Function
    Public Function Connect() As Boolean

        Dim strConx As String = mstrConnectionString

        Try
            If mcnxConnection IsNot Nothing AndAlso mcnxConnection.State = ConnectionState.Open Then
                Return True
            End If
            mcnxConnection = New SqlClient.SqlConnection(strConx)
            mcnxConnection.Open()
            mcmdSql = mcnxConnection.CreateCommand

        Catch ex As Exception
            mstrCnxErr = ex.Message
            Return False
        End Try
        Return True
    End Function
    Public Function Connect2() As Boolean

        Dim strConx As String = mstrConnectionString

        Try
            mcnxConnection2 = New SqlClient.SqlConnection(strConx)
            mcnxConnection2.Open()

        Catch ex As Exception
            Connect2 = False
            mstrCnxErr = ex.Message
            Exit Function
        End Try
        Connect2 = True
    End Function
    Public Function Disconnect() As Boolean
        If mcnxConnection.State = ConnectionState.Open Then
            mcnxConnection.Dispose()
        End If
    End Function
    Public Function Disconnect2() As Boolean
        If mcnxConnection2.State = ConnectionState.Open Then
            mcnxConnection2.Dispose()
        End If
    End Function
    Public Function UpdateListQuerries(ByVal lstQuerries As List(Of String), blnGetLastInsertedRecId As Boolean) As Boolean
        Dim strQuerry As String = ""
        Dim i As Integer
        Dim trcSql As SqlClient.SqlTransaction
        trcSql = mcnxConnection.BeginTransaction
        mcmdSql.Transaction = trcSql
        mcmdSql.CommandTimeout = 512
        Try
            For i = 0 To lstQuerries.Count - 1
                strQuerry = lstQuerries(i)
                If Not String.IsNullOrEmpty(strQuerry) Then
                    mcmdSql.CommandText = strQuerry
                    mcmdSql.ExecuteNonQuery()
                    If blnGetLastInsertedRecId AndAlso UCase(Mid(strQuerry, 1, 6)) = "INSERT" Then
                        mcmdSql.CommandText = "select SCOPE_IDENTITY()"
                        msglLastInsertedId = mcmdSql.ExecuteScalar
                    End If
                End If
            Next
            trcSql.Commit()
            Return True
        Catch ex As Exception
            mstrUpdtErr = ex.Message & vbCrLf & strQuerry
            Append2TextFile("SQL ERR:" & mstrUpdtErr & vbCrLf)
            trcSql.Rollback()
            Return False
        End Try
    End Function
    Public Function Update(ByVal arrQuerries() As String) As Boolean

        Dim trcSql As SqlClient.SqlTransaction
        Dim i As Integer
        If mcnxConnection.State = ConnectionState.Closed Then
            mcnxConnection.Open()
        End If

        trcSql = mcnxConnection.BeginTransaction
        Dim cmdSql As SqlClient.SqlCommand = mcnxConnection.CreateCommand
        cmdSql.Transaction = trcSql
        Try
            For i = LBound(arrQuerries) To UBound(arrQuerries)
                If Not arrQuerries(i) Is Nothing And arrQuerries(i) <> "" Then
                    cmdSql.CommandText = arrQuerries(i)
                    cmdSql.ExecuteNonQuery()
                    If UCase(Mid(arrQuerries(i), 1, 6)) = "INSERT" Then
                        cmdSql.CommandText = "select SCOPE_IDENTITY()"
                        msglLastInsertedId = cmdSql.ExecuteScalar
                    End If
                End If
            Next
            trcSql.Commit()
            Update = True
        Catch ex As Exception
            mstrUpdtErr = ex.Message & vbCrLf & arrQuerries(i)
            Append2TextFile("SQL ERR:" & mstrUpdtErr & vbCrLf)
            Update = False
            trcSql.Rollback()
        End Try
    End Function
    Public Function ExecuteNonQuerry(ByVal strQuerry As String) As Boolean
        Try
            mcmdSql.CommandText = strQuerry
            mcmdSql.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            mstrUpdtErr = ex.Message & vbCrLf & strQuerry
            Append2TextFile("SQL ERR:" & mstrUpdtErr)
            MsgBox(mstrUpdtErr)
            Return False
        End Try
        
    End Function
    Public Function GetScalarAsString(ByVal strQuerry As String) As String
        'purpose: get result of SQL scalar querry in STRING format
        If mcnxConnection.State <> ConnectionState.Open Then
            mcnxConnection.Open()
        End If
        mcmdSql.CommandText = strQuerry
        Return mcmdSql.ExecuteScalar
    End Function
    Public Function GetScalarAsDecimal(ByVal strQuerry As String) As Decimal
        'purpose: get result of SQL scalar querry in decimal format
        Dim strResult As String
        strResult = GetScalarAsString(strQuerry)
        If IsNumeric(strResult) Then
            Return CDec(strResult)
        Else
            Return 0
        End If
    End Function
    Public Function GetDataTable(ByVal strQuerry As String) As System.Data.DataTable
        Dim tblResults As New System.Data.DataTable

        Dim adapter As New SqlClient.SqlDataAdapter(strQuerry, mcnxConnection)

        adapter.SelectCommand.CommandTimeout = 256
        adapter.Fill(tblResults)
        Return tblResults

    End Function
    Public Function GetRoe(ByVal strCur As String) As Decimal
        'Purpose: Get ROE in RAS
        Dim cmdSql As New SqlClient.SqlCommand
        Dim strQuerry As String

        cmdSql.Connection = mcnxConnection
        strQuerry = "Select top 1 BSR from ForEX where City='" & myStaff.City & "' and IsActive='Y'"
        strQuerry = strQuerry & " and ApplyROETo like '%TS%'"
        strQuerry = strQuerry & " and Currency='" & strCur & "' order by EffectDate desc"

        cmdSql.CommandText = strQuerry
        GetRoe = cmdSql.ExecuteScalar

    End Function
    Public Function CheckDupTkts(ByVal strTkno As String, ByVal strSRV As String) As Boolean
        'Purpose: Check if insert querry will create duplicate ticket nbrs
        'Input: Ticket number & SRV
        'Output: Y/N
        Dim strQuerry As String
        Dim intResult As Integer
        If strTkno.Length < 15 Then
            strTkno = FormatRasTkno(strTkno)
        End If

        strQuerry = "select TKNO from tkt_1a where Status<>'XX' and TKNO ='" & strTkno & "' and SRV='" _
                    & strSRV & "'"

        intResult = GetScalarAsDecimal(strQuerry)

        If intResult > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsDuplicatedTicket(ByVal strTkno As String, ByVal strSRV As String) As Boolean
        'Purpose: Check if insert querry will create duplicate ticket nbrs
        'Input: Ticket number & SRV
        'Output: Y/N
        Dim strQuerry As String
        Dim intResult As Integer
        If strTkno.Length < 15 Then
            strTkno = FormatRasTkno(strTkno)
        End If

        strQuerry = "select TKNO from tkt_1a where Status<>'XX' and TKNO ='" & strTkno & "' and SRV='" _
                    & strSRV & "'"

        intResult = GetScalarAsDecimal(strQuerry)

        If intResult > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function CheckTknoExist(ByVal strTableName As String, ByVal strTkno As String _
                               , ByVal strSrv As String) As Boolean

        'purpose: check if tkno exit in RAS2K7 for Travel shop, GSA or tkt_1a
        'input: Table name
        'Output: Y/N

        Dim cmdSql As New SqlClient.SqlCommand
        Dim strQuerry As String
        strTkno = FormatRasTkno(strTkno)
        cmdSql.Connection = mcnxConnection
        strQuerry = "Select TKNO from " & strTableName
        strQuerry = strQuerry & " where TKNO='" & strTkno & "'"
        strQuerry = strQuerry & " and SRV='" & strSrv & "'"

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar <> "" Then
            CheckTknoExist = True
        End If
    End Function
    Public Function CheckBooker(ByVal intCustId As String _
                                , ByVal strBooker As String _
                                , Optional ByVal strEmail As String = "") As Boolean

        Dim cmdSql As New SqlClient.SqlCommand
        Dim strQuerry As String
        cmdSql.Connection = mcnxConnection
        strQuerry = "select Recid from CWT_Bookers where Status='OK' and custid=" _
                    & intCustId & " and BookerName='" & strBooker _
                    & "'"

        If strEmail <> "" Then
            strQuerry = strQuerry & " and Email='" & strEmail & "'"
        End If


        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function AddBookerRas(ByVal intCustId As Integer, ByVal strBooker As String _
                                , ByVal strUser As String) As Boolean
        Dim arrUpdates(0 To 1) As String

        If CheckBooker(intCustId, strBooker) Then
            MsgBox("Booker already exists:" & strBooker)
            Return False
        End If

        arrUpdates(0) = "Insert into SIR (CustId,FName,Fvalue,Status,FstUser)" _
                        & " values(" & intCustId & ",'BOOKER','" & strBooker & "','OK','" & strUser & "')"

        If ExecuteNonQuerry(arrUpdates(0)) Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function AddBooker(ByVal intCustId As Integer, ByVal strBooker As String _
                            , ByVal strEmail As String, ByVal strUser As String) As Boolean
        Dim arrUpdates(0 To 1) As String

        If CheckBooker(intCustId, strBooker, strEmail) Then
            'MsgBox("Booker already exists:" & strBooker)
            Return False
        End If

        arrUpdates(0) = "Insert into CWT_Bookers" _
                        & "(CustId,BookerName,Email,Status,FstUser)" _
                        & " values(" & intCustId _
                        & ",'" & strBooker & "','" & strEmail & "','OK','" & strUser & "')"
        'arrUpdates(1) = "Insert into SIR (CustId,FName,Fvalue,Status,FstUser)" _
        '                & " values(" & intCustId & ",'BOOKER','" & strBooker & "','OK','" & strUser & "')"

        'ExecuteNonQuerry(arrUpdates(1))
        If ExecuteNonQuerry(arrUpdates(0)) Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function InCdrBypassList(ByVal lngTravelId As Long) As Boolean
        Dim cmdSql As New SqlClient.SqlCommand
        Dim strQuerry As String

        cmdSql.Connection = mcnxConnection
        strQuerry = "Select Recid  from GO_ByPass where Status='OK'" _
                    & " and Approved='True' and TrvlId=" & lngTravelId

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function Apt2City(ByVal strAirport As String) As String

        'purpose: Get the City code
        'input: Airport code
        'Output: City code

        Dim cmdSql As New SqlClient.SqlCommand
        Dim strQuerry As String
        cmdSql.Connection = mcnxConnection
        strQuerry = "Select City from City where Airport='" & strAirport & "'"

        cmdSql.CommandText = strQuerry
        Apt2City = cmdSql.ExecuteScalar
    End Function
    Public Function Apt2Country(ByVal strAirport As String) As String
        'purpose: Get the Country code
        'input: Airport code
        'Output: Country code

        Dim cmdSql As New SqlClient.SqlCommand
        Dim strQuerry As String
        cmdSql.Connection = mcnxConnection
        strQuerry = "Select Country from City where Airport='" & strAirport & "'"

        cmdSql.CommandText = strQuerry
        Apt2Country = cmdSql.ExecuteScalar
    End Function
    Public Function ConvertAirports2Cities(strAirports As String) As String
        Dim strCities As String = String.Empty
        Dim i As Integer
        For i = 1 To strAirports.Length Step 5
            strCities = strCities & GetCity(Mid(strAirports, i, 3))
            If strAirports.Length - i > 2 Then
                strCities = strCities & Mid(strAirports, i + 3, 2)
            End If
        Next
        Return strCities
    End Function
    Public Function GetCity(ByVal strAirportCode) As String
        'Tim ma thanh pho cho ma san bay
        'Input: Ma san bay
        'Output: Ma nuoc
        'Pre-requisite: Can ket noi TVCS

        Select Case strAirportCode
            Case "SGN", "HAN", "DAD", "HUI", "HPH"
                Return strAirportCode
            Case "CXR"
                Return "NHA"
            Case Else
                Dim strQry As String = ""
                Dim cmdSql As New SqlClient.SqlCommand
                cmdSql.Connection = mcnxConnection

                strQry = "select City from City where Airport ='" & strAirportCode & "'"
                cmdSql.CommandText = strQry
                Return cmdSql.ExecuteScalar
        End Select
    End Function
    Public Function GetCityCodeByCityName(ByVal strCityName) As String
        'Tim ma thanh pho cho ma san bay
        'Input: Ma san bay
        'Output: Ma nuoc
        'Pre-requisite: Can ket noi TVCS

        Return GetScalarAsString("select top 1 City from City where CityName ='" & strCityName & "'")
    End Function
    Public Function GetCityCodeByCityNameFromAllSources(strCityName As String) As String
        Dim strResult As String = GetCityCodeByCityName(strCityName)
        If strResult = "" Then
            strResult = GetCityCodeByBlCityName(strCityName)
        End If
        Return strResult
    End Function
    Public Function GetCityCodeByBlCityName(ByVal strCityName) As String
        'Tim ma thanh pho cho ma san bay
        'Input: Ma san bay
        'Output: Ma nuoc

        Dim strQry As String = "select top 1 City from City where CityName ='" & strCityName _
            & "' union (select top 1 Details from Cwt.dbo.GO_MISC where cat='blcity' and val =N'" _
            & strCityName & "')"
        Return GetScalarAsString(strQry)

    End Function
    Public Function GetTel4LocalHtl(ByVal strHotelName As String, strCityCode As String) As String
        'Tim so dien thoai cho local hotel
        Dim strQry As String = "select Tel from [GO_HotelListTV] where Status='OK' and CityCode ='" _
                               & strCityCode & "' and HtlName='" & strHotelName & "'"

        Return GetScalarAsString(strQry)
    End Function
    Public Function GetCityAirportName(ByVal strAirportCode As String, Optional ByVal blnWzNewCnx As Boolean = False) As String
        ''Tim ma thanh pho cho ma san bay
        ''Input: Ma san bay
        ''Output: Ma nuoc
        ''Pre-requisite: Can ket noi TVCS
        'Dim strQry As String = "select CityName from [citycode] where Airport ='" & strAirportCode & "'"

        'GetCityName = UCase(GetScalarAsString(strQry))
    End Function
    Public Function GetCityName(ByVal strAirportCode As String, Optional ByVal blnWzNewCnx As Boolean = False) As String
        'Tim ma thanh pho cho ma san bay
        'Input: Ma san bay
        'Output: Ma nuoc
        'Pre-requisite: Can ket noi TVCS
        Dim strQry As String = "select CityName from City where Airport ='" & strAirportCode & "'"

        GetCityName = UCase(GetScalarAsString(strQry))
    End Function
    Public Function GetCityCodeByProvinceName(ByVal strProvinceName As String) As String
        'Tim ma thanh pho cho ten tinh cua Vietnam
        'Input: Ma san bay
        'Output: Ma nuoc
        'Pre-requisite: Can ket noi TVCS
        Dim strQry As String = "select VAL1 from CWT.dbo.Misc where CAT='HTLLOCATION' and VAL ='" & strProvinceName & "'"

        Return (GetScalarAsString(strQry))
    End Function
    Public Function IsCwtRegionalCity(strAptCode As String) As Boolean
        If GetScalarAsDecimal("Select RecId from GO_MISC where Cat='Regional' and Val like '%'+" _
                             & "(select Country From CityCode where Airport='" & strAptCode & "')" & "+'%'") > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetCountryCode(ByVal strAirportCode As String) As String
        'Tim ma nuoc cho ma thanh pho
        'Input: Ma san bay
        'Output: Ma nuoc
        'Pre-requisite: Can ket noi TVCS
        Select Case strAirportCode
            Case "SGN", "HAN", "DAD", "HUI", "NHA", "HPH"
                Return "VN"
            Case Else
                Dim strQuerry As String = "select Country from City where Airport ='" & strAirportCode & "'"
                Return GetScalarAsString(strQuerry)
        End Select

    End Function
    Public Function GetCountryNameByAptCode(ByVal strAirportCode As String) As String
        'Tim ma nuoc cho ma thanh pho
        'Input: Ma san bay
        'Output: Ma nuoc
        'Pre-requisite: Can ket noi TVCS
        Dim strQuerry As String = "select top 1 CountryName from Country where Country=" _
                                  & "(select top 1 Country from City where Airport ='" & strAirportCode & "')"
        Return GetScalarAsString(strQuerry)
    End Function

    Public Function GetIataTcCode(ByVal strAirportCode As String) As String
        'Tim ma nuoc cho ma thanh pho
        'Input: Ma san bay
        'Output: Ma TC 
        'Pre-requisite: Can ket noi TVCS
        Dim strQuerry As String = "Select top 1 TC from [Country] where Country= " _
                                  & "(select top 1 Country from City where Airport ='" & strAirportCode & "')"
        Return GetScalarAsString(strQuerry)
    End Function
    Public Function GetRtgType(Optional ByVal strRtg As String = "") As String
        'Purpose: Tim loai hanh trinh tu PNR
        'Input: Hanh trinh chi co City va Carrier
        'Output: Loai hanh trinh INTL hoac XXDOM
        Dim ArrCity() As String
        Dim strRtgType As String = ""
        Dim strExCountry As String = ""
        Dim bytCityCount As Integer
        Dim i As Integer

        bytCityCount = (Len(strRtg) + 2) / 5
        ReDim ArrCity(0 To bytCityCount - 1)
        For i = 0 To bytCityCount - 1
            ArrCity(i) = Mid(strRtg, i * 5 + 1, 3)
            If i = 0 Then
                strExCountry = GetCountryCode(ArrCity(i))
                strRtgType = strExCountry & "DOM"
            ElseIf strExCountry <> GetCountryCode(ArrCity(i)) Then
                strRtgType = "INTL"
                Exit For
            End If
        Next
        GetRtgType = strRtgType
    End Function
    Public Function GetCwtRtgType(strRtg As String, strRtgType As String) As String
        'Purpose: Tim loai hanh trinh tu PNR
        'Input: Hanh trinh chi co City va Carrier
        'Output: Loai hanh trinh INTL hoac XXDOM
        Dim bytCityCount As Integer
        Dim i As Integer

        If strRtgType = "VNDOM" Then
            Return "DOM"
        End If

        bytCityCount = (Len(strRtg) + 2) / 5

        For i = 0 To bytCityCount - 1
            If Not IsCwtRegionalCity(Mid(strRtg, i * 5 + 1, 3)) Then
                Return "INTL"
            End If
        Next

        Return "REG"
    End Function
    Public Function GetCwtRtgTypeByTkno(strCar As String, strShortTkno As String) As String

        Dim strQuerry As String = "select DeKho from ras12.dbo.reportdata " _
            & " where tkid=(select TOP 1 RecID from RAS12.dbo.TKT where Status<>'XX' and StatusAL<>'XX'" _
            & " and AL='" & strCar _
            & "' and SUBSTRING(replace(tkno,' ',''),4,10)='" & strShortTkno _
            & "')"

        Return GetScalarAsString(strQuerry)
    End Function
    Public Function GetRtgType4CwtByApts(intCustId As String, dteDOI As Date _
    , strAirports As String) As String
        Dim arrApts() As String
        Dim i As Integer

        Dim strRtgType As String = ""
        arrApts = Split(strAirports, ",")

        For i = 0 To UBound(arrApts)
            strRtgType = strRtgType & GetRtgType4Country(intCustId, dteDOI, GetCountryCode(arrApts(i)))
        Next

        If InStr(strRtgType, "INTL") > 0 Then
            strRtgType = "INTL"
        ElseIf InStr(strRtgType, "REG1") > 0 Then
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
    Public Function GetRtgType4Itinerary(intCustId As String, dteDOI As Date _
    , strCountries As String) As String
        Dim arrCountries() As String
        Dim i As Integer

        Dim strRtgType As String = ""
        arrCountries = Split(strCountries, ",")

        For i = 0 To UBound(arrCountries)
            strRtgType = strRtgType & GetRtgType4Country(intCustId, dteDOI, arrCountries(i))
        Next

        If InStr(strRtgType, "INTL") > 0 Then
            strRtgType = "INTL"
        ElseIf InStr(strRtgType, "REG1") > 0 Then
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
    Public Function GetRtgType4OD(intCustId As String, dteDOI As Date _
    , strOriCountry As String, strDesCountry As String) As String

        Dim strOriRegion As String
        Dim strDesRegion As String

        Dim strRtgType As String

        strOriRegion = GetRtgType4Country(intCustId, dteDOI, strOriCountry)
        strDesRegion = GetRtgType4Country(intCustId, dteDOI, strDesCountry)

        If InStr(strOriRegion & strDesRegion, "INTL") > 0 Then
            strRtgType = "INTL"
        ElseIf InStr(strOriRegion & strDesRegion, "REG1") > 0 Then
            strRtgType = "REG1"
        ElseIf InStr(strOriRegion & strDesRegion, "REG2") > 0 Then
            strRtgType = "REG2"
        ElseIf InStr(strOriRegion & strDesRegion, "REG") > 0 Then
            strRtgType = "REG"
        ElseIf InStr(strOriRegion & strDesRegion, "DOM") > 0 Then
            strRtgType = "DOM"
        End If
        Return strRtgType

    End Function

    Private Function GetRtgType4Country(intCustId As String, dteDOI As Date _
    , strCountry As String) As String


        Dim strQuerry As String
        Dim strRtgType As String = ""

        If strCountry = "VN" Then
            strRtgType = "DOM"
        Else
            strQuerry = "select top 1 VAL from ras12.dbo.MISC where Status='ok'" _
                        & " and CAT='RtgTypeCwt' and IntVal=" & intCustId _
                        & " and VAL1 like '%" & strCountry & "%' and '" _
                        & Format(dteDOI, "dd MMM yy") & "' between dteVal and dteVal1"


            strRtgType = GetScalarAsString(strQuerry)
            If strRtgType = "" Then
                strRtgType = "INTL"
            End If
        End If

        Return strRtgType
    End Function

    Public Function GetCar2C(ByVal strCar3D As String) As String
        'purpose: Get the 2-character code of airlines
        'input: 3 digit code of airline        
        Dim strCarCode As String
        Dim strQuerry As String
        strQuerry = "select ALCode from Airline where DocCode='" & strCar3D & "'"
        strCarCode = GetScalarAsString(strQuerry)
        Return strCarCode
    End Function
    Public Function GetCar3D(ByVal strCar2C As String) As String
        'purpose: Get the 3-digit code of airlines
        'input: 2-character code of airline        
        Dim strCarCode As String
        Dim strQuerry As String
        strQuerry = "select DocCode from Airline where ALCode='" & strCar2C & "'"
        strCarCode = GetScalarAsString(strQuerry)
        Return strCarCode
    End Function
    Public Function GetCarName(ByVal strCarCode As String, Optional ByVal blnWzNewCnx As Boolean = False) As String

        'purpose: Get the NAME of airlines
        'input: 2-character code of airlines
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        If blnWzNewCnx Then
            cmdSql.Connection = New SqlClient.SqlConnection(mstrConnectionString)
            cmdSql.Connection.Open()
        Else
            cmdSql.Connection = mcnxConnection
        End If
        strQuerry = "select ALName from Airline where ALCode='" & strCarCode & "'"
        cmdSql.CommandText = strQuerry
        GetCarName = UCase(Trim(cmdSql.ExecuteScalar))
    End Function
    Public Function GetIsi(ByVal strAptCode As String) As String
        If GetCountryCode(strAptCode) = "VN" Then
            GetIsi = "SITI"
        Else
            GetIsi = "SOTO"
        End If
    End Function

    Public Function CreateEmail(ByVal intCus As Integer, ByVal strSubj As String, ByVal strMsg As String, _
                                ByVal strFrom As String, ByVal strEmailGroup As String) As Boolean
        Dim strColumns As String
        Dim strValues As String = ""
        Dim strQuerry As String
        Dim intResult As Integer
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        'If intCus = 0 Then Exit Function
        strColumns = "CustID,Subj,Msg,Frm,Dept"
        strValues = strValues & "'" & intCus & "'"
        strValues = strValues & ",'" & strSubj & "'"
        strValues = strValues & ",'" & strMsg & "'"
        strValues = strValues & ",'" & strFrom & "'"
        strValues = strValues & ",'" & strEmailGroup & "'"
        strQuerry = "insert into EmailLog ("
        strQuerry = strQuerry & strColumns & ") values ("
        strQuerry = strQuerry & strValues & ")"

        cmdSql.CommandText = strQuerry
        intResult = cmdSql.ExecuteNonQuery()
        If intResult > 0 Then
            CreateEmail = True
        Else
            CreateEmail = False
        End If

    End Function
    Public Function GetPnrCreationInfoF1S(ByVal strRLOC As String, ByRef strPcc As String _
                                    , ByRef strTVSI As String) As Boolean
        Dim strResult As String

        strResult = GetScalarAsString("Select PCC+'*'+convert(varchar,TVSI)" _
                    & " from F1S.DBO.F1S_PNR_TKT_Agent where DocNo='" & strRLOC & "'")
        If strResult = "" Then
            Return False
        Else
            Dim arrBreaks() As String
            arrBreaks = Split(strResult, "*")
            strPcc = arrBreaks(0)
            strTVSI = arrBreaks(1)
            Return True
        End If

    End Function
    Public Function GetUserInfoF1S(ByVal strRasUser As String, ByRef strPcc As String _
                                    , ByRef strTVSI As String) As Boolean
        Dim strResult As String

        strResult = GetScalarAsString("Select PCC+'*'+TVSI from CWT_USERS" _
                                        & " where RasUser='") & strRasUser & "'"
        If strResult = "" Then
            Return False
        Else
            Dim arrBreaks() As String
            arrBreaks = Split(strResult, "*")
            strPcc = arrBreaks(0)
            strTVSI = arrBreaks(1)
            Return True
        End If

    End Function
    Public Function GetPreviousVersion(ByVal strRLOC As String, ByRef lngCurrentId As Long _
                                    , ByRef lngPreviousRecId As Long) As Boolean
        Dim strResult As String

        strResult = GetScalarAsString("Select top 1 RecId from CWT_PNR" _
                                    & " where RLOC='" & strRLOC _
                                    & "' and Status<>'--' and Purged='false'" _
                                    & " and RecId <" & lngCurrentId & " order by RecId desc")
        If strResult = "" Then
            Return False
        Else
            lngPreviousRecId = strResult
            Return True
        End If
    End Function
    'Public Function GetTktEntry(ByVal strValCar As String, ByVal mstrTktBox As String _
    '                            , ByVal strLocaion As String) As String
    '    'Purpose: Find additional Ticket entry to be inserted into TST
    '    'Input: Validatint carrier, ISI
    '    'Output: Ticket entry

    '    Dim strQry As String
    '    Dim cmdSql As New SqlClient.SqlCommand
    '    cmdSql.Connection = mcnxConnection

    '    strQry = "select Value from TktEntries"
    '    strQry = strQry & " where Status ='OK' and ValCar ='" & strValCar & "'"
    '    strQry = strQry & " and '" & Format(Now, "dd-mmm-yyyy hh:nn:ss") & "' between TktDateFrom and TktDateTo"
    '    strQry = strQry & " and Catergory ='" & mstrTktBox & "'"
    '    strQry = strQry & " and Location='" & strLocaion & "'"
    '    cmdSql.CommandText = strQry
    '    GetTktEntry = cmdSql.ExecuteScalar
    'End Function

    Public Function InsertPnrText(ByVal strCounter As String _
                                , ByVal strGDS As String, ByVal strRloc As String _
                                , ByVal strPnrText As String _
                                , Optional ByVal strCreationOffc As String = "" _
                                , Optional ByVal strCreationSI As String = "") As Boolean
        Dim arrQry(0 To 0) As String
        arrQry(0) = "Insert into FT.dbo.CWT_PNR ([GDS],[Rloc],[PnrContent]" _
                & ",CreationOffc,CreationSI,Counter)" _
                & " Values ('" & strGDS & "','" & strRloc & "','" _
                & Replace(strPnrText, "'", "*") & "','" & strCreationOffc _
                & "','" & strCreationSI & "','" & strCounter & "')"
        If Update(arrQry) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function InsertPnrErrList(ByVal strGDS As String, ByVal strRloc As String _
                                , ByVal strPnrText As String _
                                , ByVal strErrType As String _
                                , ByVal strStatus As String) As Boolean
        Dim arrQry(0 To 0) As String

        If GetScalarAsDecimal("Select RecId from FT.dbo.CWT_ErrList" _
                            & " where Status<>'XX' and ErrType='" & strErrType _
                            & "' and GDS='" & strGDS & "' and Rloc='" & strRloc _
                            & "'") > 0 Then
            Return True
        End If
        arrQry(0) = "Insert into FT.dbo.CWT_ErrList ([GDS],[Rloc],[PnrContent]" _
                & ",ErrType,Status)" _
                & " Values ('" & strGDS & "','" & strRloc & "','" _
                & Replace(strPnrText, "'", "*") & "','" & strErrType _
                & "','" & strStatus & "')"
        If Update(arrQry) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetCDRs(ByVal strCmc As String, Optional ByVal blnBeforeRas As Boolean = False _
                            , Optional blnIncludeConditionalCDR As Boolean = False _
                            , Optional blnPosOnly As Boolean = False) As Collection

        Dim colCDRs As New Collection
        Dim strQry As String
        Dim tblCDRs As DataTable

        strQry = "select * from GO_CDRs"
        strQry = strQry & " where CMC='" & strCmc & "' and status='OK'"
        If blnBeforeRas Then
            strQry = strQry & " and CollectionMethod<>'RAS'"
        End If
        If blnPosOnly Then
            strQry = strQry & " and CollectionMethod='AGTINPUT'"
        End If
        If Not blnIncludeConditionalCDR Then
            strQry = strQry & " and Mandatory='M'"
        End If
        strQry = strQry & " order by cdrnbr"
        tblCDRs = pobjTvcs.GetDataTable(strQry)

        If tblCDRs.Rows.Count > 0 Then
            For Each objRow As DataRow In tblCDRs.Rows
                Dim objCdr As New clsCwtCdr
                objCdr.Nbr = objRow("CdrNbr")
                objCdr.CdrName = objRow("CdrName")
                objCdr.CharType = objRow("CharType")
                objCdr.MinLength = objRow("MinLength")
                objCdr.MaxLength = objRow("MaxLength")
                objCdr.Mandatory = objRow("Mandatory")
                objCdr.CheckValues = objRow("CheckValues")
                If objCdr.CheckValues Then
                    objCdr.Values = pobjTvcs.GetDataTable("select * from [GO_MiscWzDate] where Status='OK' and Value1='" & strCmc _
                                                          & "' And Catergory='CDR" _
                                                         & objCdr.Nbr & "' and getdate() between TktDateFrom and TktDateTo" _
                                                         & " order by Details")
                End If
                colCDRs.Add(objCdr, objCdr.Nbr)
            Next
        End If

        Return colCDRs
    End Function
    Public Function GetRequiredData(ByVal strCmc As String, Optional ByVal blnBeforeRas As Boolean = False _
                            , Optional blnIncludeConditionalCDR As Boolean = False) As Collection

        Dim colRequiredData As New Collection
        Dim strQry As String
        Dim drResult As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = pobjTvcs.Connection

        strQry = "select * from GO_RequiredData"
        strQry = strQry & " where CMC='" & strCmc & "' and status='OK'"
        If blnBeforeRas Then
            strQry = strQry & " and CollectionMethod<>'RAS'"
        End If
        If Not blnIncludeConditionalCDR Then
            strQry = strQry & " and Mandatory='M'"
        End If
        strQry = strQry & " order by cdrnbr"
        cmdSql.CommandText = strQry
        drResult = cmdSql.ExecuteReader
        If Not drResult Is Nothing Then
            Do While drResult.Read
                Dim objCdr As New clsCwtCdr
                objCdr.Nbr = drResult("CdrNbr")
                objCdr.CdrName = drResult("CdrName")
                objCdr.CharType = drResult("CharType")
                objCdr.MinLength = drResult("MinLength")
                objCdr.MaxLength = drResult("MaxLength")
                objCdr.Mandatory = drResult("Mandatory")
                colRequiredData.Add(objCdr, objCdr.Nbr)
            Loop
        End If
        drResult.Close()
        Return colRequiredData
    End Function
    Public Function GetHierachies(ByVal strCmc As String, Optional blnPos As Boolean = False) As Collection
        Dim colHierachies As New Collection
        Dim strQry As String
        Dim drResult As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = pobjTvcs.Connection

        strQry = "select *, 'M' as Mandatory from GO_Hierachies" _
                 & " where CMC='" & strCmc & "' and status='OK'"

        If blnPos Then
            strQry = strQry & " and "
        End If
        cmdSql.CommandText = strQry
        drResult = cmdSql.ExecuteReader
        If Not drResult Is Nothing Then
            Do While drResult.Read
                Dim objHierachy As New clsHierachy
                objHierachy.Nbr = drResult("Nbr")
                objHierachy.CheckValues = drResult("CheckValues")
                objHierachy.Description = drResult("Description")
                colHierachies.Add(objHierachy)
            Loop
        End If
        drResult.Close()
        GetHierachies = colHierachies
    End Function
    Public Function GetDefaultHierachy(ByVal strCmc As String, ByVal intHierNbr As Int16) As String
        Dim colHierachies As New Collection
        Dim strQry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = pobjTvcs.Connection

        strQry = "select top 1 DefaultValue from GO_Hierachies" _
                 & " where status='OK' and CMC='" & strCmc _
                 & "' and Nbr=" & intHierNbr

        cmdSql.CommandText = strQry
        Return cmdSql.ExecuteScalar

    End Function
    Public Function CheckHierachyValueValid(ByVal strCmc As String, ByVal intHierNbr As Int16 _
                                        , ByVal strHierValue As String) As Boolean
        Dim strQry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = pobjTvcs.Connection

        strQry = "select RecId from GO_HierachyValues" _
                 & " where status='OK' and Nbr=" & intHierNbr & " and CMC='" & strCmc _
                 & "' and HierValue='" & strHierValue & "'"

        cmdSql.CommandText = strQry
        If cmdSql.ExecuteScalar() = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function GetCorpName(ByVal strCmc As String) As String
        Dim strQry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = pobjTvcs.Connection

        strQry = "select CompanyName from GO_CompanyInfo1"
        strQry = strQry & " where CMC='" & strCmc & "'"

        cmdSql.CommandText = strQry
        GetCorpName = cmdSql.ExecuteScalar
    End Function
    Public Function GetCorpNameByCmc(ByVal strCmc As String) As String
        Dim strQry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = pobjTvcs.Connection

        strQry = "select CompanyName from GO_CompanyInfo1 where Status='OK' and CMC='" _
                & strCmc & "'"

        cmdSql.CommandText = strQry
        Return cmdSql.ExecuteScalar
    End Function
    Public Function GetCmcByCustId(ByVal intCustId As Integer) As String
        Dim strQry As String
        strQry = "select CMC from GO_CompanyInfo1 where Status='OK' and CustId=" & intCustId

        Return GetScalarAsString(strQry)
    End Function
    Public Function GetCmcName(ByVal strCmc As String) As String
        Dim strQry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = pobjTvcs.Connection

        strQry = "select CompanyName from GO_CompanyInfo1 where Status='OK' and CMC='" & strCmc & "'"

        cmdSql.CommandText = strQry
        GetCmcName = cmdSql.ExecuteScalar
    End Function
    Public Function GetCmcByName(ByVal strCorpName As String) As String
        Dim strQry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = pobjTvcs.Connection

        strQry = "select cmc from GO_CompanyInfo1 where Status='OK' and CompanyName='" & strCorpName & "'"

        cmdSql.CommandText = strQry
        Return cmdSql.ExecuteScalar
    End Function
    Public Function GetCustShortNames(ByVal strCmc As String, Optional ByVal blnGetAll As Boolean = False) As Collection
        Dim colShortNames As New Collection
        Dim strQry As String
        Dim drResult As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQry = "select * from GO_CompanyInfo1"
        If Not blnGetAll Then
            strQry = strQry & " where GO_client=1 and CMC='" & strCmc & "' order by CustShortName"
        End If

        cmdSql.CommandText = strQry
        drResult = cmdSql.ExecuteReader
        If Not drResult Is Nothing Then
            Do While drResult.Read
                Dim objShortName As New clsCwtShortName
                objShortName.ShortName = drResult("CustShortName")
                objShortName.CompanyName = drResult("CompanyName")
                objShortName.Cmc = drResult("Cmc")
                colShortNames.Add(objShortName)
            Loop
        End If
        drResult.Close()
        GetCustShortNames = colShortNames
    End Function
    Public Function GetRasCustId(ByVal strCustShortName As String) As String
        'Tim Customer Id
        'Input: Customer short name
        'Output: Customer Id
        'Pre-requisite: Can ket noi TVCS
        Dim strQry As String = ""
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQry = "select RecId from CustomerList where Status='OK' " _
                & " and CustShortName ='" & strCustShortName & "'"
        'strQry = "select CustId from CustomerList where CustShortName ='" _
        '        & strCustShortName & "'"
        cmdSql.CommandText = strQry
        GetRasCustId = cmdSql.ExecuteScalar
    End Function
    Public Function GetEmpId(ByVal intCustId As Integer, ByVal strTravellerName As String) As String
        'Tim Customer Id
        'Input: Customer short name
        'Output: Customer Id
        'Pre-requisite: Can ket noi TVCS
        Dim strQry As String = ""
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQry = "select EmplId from GO_EmployeeId where Status='OK'" _
                & " and CustiD =" & intCustId & " and Traveler='" & strTravellerName & "'"
        cmdSql.CommandText = strQry
        GetEmpId = cmdSql.ExecuteScalar
    End Function

    Public Function GetUsdRoeInRas(ByVal dteDateTime As Date, BlnIata As Boolean) As Decimal
        Dim strQuerry As String
        If BlnIata Then
            strQuerry = "Select top 1 BSR from ForEX where IsActive='Y'" _
                    & " and ApplyROETo like '%IATA%' and Currency='USD' and EffectDate <'" & DateTime2Text(dteDateTime) _
                    & "' and City='" & myStaff.City & "' order by Recid desc"
        Else
            strQuerry = "Select top 1 BSR from ForEX where IsActive='Y'" _
                    & " and ApplyROETo like '%TS%' and Currency='USD' and EffectDate <'" & DateTime2Text(dteDateTime) _
                    & "' and City='" & myStaff.City & "' order by Recid desc"
        End If

        Return FormatNumber(GetScalarAsDecimal(strQuerry), 2)

    End Function
    'Public Function GetRoeByTimeCustId(ByVal strCur As String, ByVal dteDateTime As Date _
    '                            , ByVal intCustId As Integer, ByVal blnBusiness As Boolean) As Decimal
    '    'Purpose: Get ROE in RAS
    '    Dim cmdSql As New SqlClient.SqlCommand
    '    Dim strQuerry As String
    '    Dim strApplyTo As String

    '    Select Case pobjCustomer.CustId
    '        Case 6536, 6537, 6538, 6539
    '            If blnBusiness Then
    '                strApplyTo = "B*"
    '            Else
    '                strApplyTo = "TS*"
    '            End If
    '        Case Else
    '            strApplyTo = "TS"
    '    End Select

    '    cmdSql.Connection = mcnxConnection
    '    strQuerry = "Select top 1 BSR from ForEX where IsActive='Y'" _
    '                & " and ApplyROETo like '%" & strApplyTo & "%' and Currency='" & strCur _
    '                & "' and EffectDate <'" & DateTime2Text(dteDateTime) _
    '                & "' order by Recid desc"

    '    cmdSql.CommandText = strQuerry
    '    GetRoeByTimeCustId = cmdSql.ExecuteScalar

    'End Function
    Public Function GetLastLccTktRow(ByVal strCar As String, ByVal strRloc As String _
                                     , intCountTtlPax As Integer) As DataRow
        Dim strQuerry As String
        Dim tblTkt As DataTable

        If intCountTtlPax > 99 And strRloc.Length > 7 Then
            strRloc = Mid(strRloc, strRloc.Length - 6)
        End If
        strQuerry = "select top 1 Tkno,PaxName from Ras12.dbo.tkt where Status <> 'XX' and SRV ='S' and substring(TKNO,1,13)='" _
            & "Z" & strCar & " " & Mid(strRloc.PadRight(8, "0"), 1, 6) & " " & Mid(strRloc.PadRight(8, "0"), 7) _
            & "'  order by RecId desc"
        ' can bo sung them lay luon tu tkt_1a de so sanh, lay so cuoi cung
        tblTkt = GetDataTable(strQuerry)

        If tblTkt.Rows.Count > 0 Then
            Return tblTkt.Rows(0)
        Else
            Return Nothing
        End If

    End Function
    Public Function GetLastLccTktNbr(ByVal strCar As String, ByVal strRloc As String _
                                     , intCountTtlPax As Integer) As String
        Dim strQuerry As String
        If intCountTtlPax > 99 And strRloc.Length > 7 Then
            strRloc = Mid(strRloc, strRloc.Length - 6)
        End If
        strQuerry = "select top 1 Tkno from Ras12.dbo.tkt where Status <> 'XX' and SRV ='S' and substring(TKNO,1,13)='" _
            & "Z" & strCar & " " & Mid(strRloc.PadRight(8, "0"), 1, 6) & " " & Mid(strRloc.PadRight(8, "0"), 7) _
            & "'  order by RecId desc"
        ' can bo sung them lay luon tu tkt_1a de so sanh, lay so cuoi cung
        Return GetScalarAsString(strQuerry)
    End Function
    Public Function GetOriLccTktNbr(ByVal strCar As String, ByVal strRloc As String, strPaxName As String) As String
        Dim strQuerry As String
        strQuerry = "select top 1 Tkno from Ras12.dbo.tkt where Status <> 'XX' and SRV ='S' and substring(TKNO,1,13)='" _
            & "Z" & strCar & " " & Mid(strRloc.PadRight(8, "0"), 1, 6) & " " & Mid(strRloc.PadRight(8, "0"), 7) _
            & "' and PaxName='" & strPaxName & "'  order by RecId desc"
        Return GetScalarAsString(strQuerry)
    End Function

    Public Function DuplicateTkt_1A(ByVal strTKNO As String, ByVal strSRV As String) As Boolean
        'Purpose: Check if insert querry will create duplicate ticket nbrs in TKT_1A
        'Input: Ticket number & SRV
        'Output: Y/N
        Dim strQuerry As String

        If Len(strTKNO) < 15 Then
            strTKNO = FormatRasTkno(strTKNO)
        End If

        strQuerry = "select tkno from tkt_1a where Status in ('OK','RE') and TKNO ='" & strTKNO _
                    & "' and SRV='" & strSRV & "' and DateDiff(d,doi,getdate())<300" _
                    & " Union select tkno from ras12.dbo.tkt where Status in ('OK','RE') and TKNO ='" & strTKNO _
                    & "' and SRV='" & strSRV & "' and DateDiff(d,doi,getdate())<365"
        If GetScalarAsString(strQuerry) = "" Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function GetRcp4LocalNonAir(intDuToanId As Integer) As clsRasRcp
        Dim objRcp As New clsRasRcp
        Dim strResult As String
        Dim strQuerry As String = "Select RcpNo+'*'+convert(varchar,FstUpdate,106) from ras12.dbo.fop" _
                                  & " where status='ok' and rmk='" & intDuToanId & "'"
        strResult = GetScalarAsString(strQuerry)
        If strResult <> "" Then
            Dim arrBreak() As String = strResult.Split("*")
            With objRcp
                .DOI = arrBreak(1)
                .RcpNo = arrBreak(0)
                .Rmk = intDuToanId
            End With
        End If
        Return objRcp
    End Function

    Public Function DuplicateGO_Air(ByVal strValCar As String, ByVal strTKNO As String, ByVal strSRV As String) As Boolean
        'Purpose: Check if insert querry will create duplicate ticket nbrs in TKT_1A
        'Input: Ticket number & SRV
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection
        Dim sglResult As Single

        strTKNO = Replace(strTKNO, "-", "")
        strTKNO = Replace(strTKNO, " ", "")
        strTKNO = Mid(strTKNO, 4)

        strQuerry = "select * from GO_Air where Carrier='" & strValCar _
                    & "' and TKNO='" & strTKNO & "' and SRV='" & strSRV & "'"
        cmdSql.CommandText = strQuerry
        sglResult = cmdSql.ExecuteScalar
        If sglResult = 0 Then
            DuplicateGO_Air = False
        Else
            DuplicateGO_Air = True
        End If
    End Function
    Public Function FindGO_TravelId(ByVal strRloc As String _
                                    , ByVal strPaxName As String) As Long

        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "select top 1 RecId from GO_Travel where BkgTool='AMA'" _
                    & " and RLOC='" & strRloc _
                    & "' and PaxName='" & strPaxName & "'"

        cmdSql.CommandText = strQuerry
        Return cmdSql.ExecuteScalar
    End Function
    Public Function DuplicateGO_Hotel(ByVal strRloc As String, ByVal strCity As String _
                                    , ByVal strHotelName As String _
                                    , ByVal dteInDate As Date) As Boolean

        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection
        Dim sglResult As Single

        strQuerry = "select Recid from GO_Hotel where Rloc='" & strRloc _
                    & "' and CityCode='" & strCity & "' and HotelName='" & strHotelName _
                    & "' and convert(varchar,checkindate,12)='" & Format(dteInDate, "yyMMdd") & "'"
        cmdSql.CommandText = strQuerry
        sglResult = cmdSql.ExecuteScalar
        If sglResult = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function DuplicateGO_Ins(ByVal strRloc As String, ByVal strInsuredName As String _
                                    , ByVal dteStartDate As Date _
                                    , ByVal dteEndDate As Date) As Boolean

        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection
        Dim sglResult As Single

        strQuerry = "select Recid from GO_Insurance where Rloc='" & strRloc _
                    & "' and InsuredName='" & strInsuredName _
                    & "' and StartDate='" & DateTime2Text(dteStartDate) _
                    & "' and EndDate='" & DateTime2Text(dteEndDate) & "'"
        cmdSql.CommandText = strQuerry
        sglResult = cmdSql.ExecuteScalar
        If sglResult = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function InsuranceExists4CHR(ByVal strCfrmNbr As String) As Boolean
        Dim sglResult As Single
        sglResult = GetScalarAsDecimal("select Recid from Tkt_1a where Status<>'XX' and Tkno='" _
                    & strCfrmNbr & "'")

        If sglResult = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function InsuranceExists4AAA(ByVal strCfrmNbr As String) As Boolean
        Dim sglResult As Single
        sglResult = GetScalarAsDecimal("select Recid from ras12.dbo.Tkt where Status<>'XX' and Dependent='" _
                    & strCfrmNbr & "'")

        If sglResult = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    'Public Function DeleteGO_Travel(ByVal sglRecId As Single) As Boolean
    '    'Purpose: Delete duplicate record in GO_Travel
    '    'Input: Record ID
    '    'Output: Y/N
    '    Dim strQuerry As String
    '    Dim cmdSql As New SqlClient.SqlCommand
    '    cmdSql.Connection = mcnxConnection

    '    strQuerry = "DELETE from GO_Travel where RecId=" & sglRecId
    '    cmdSql.CommandText = strQuerry
    '    cmdSql.ExecuteNonQuery()
    '    DeleteGO_Travel = True
    'End Function
    Public Function DeleteGO_Travel(ByVal lstTravelIds As List(Of Integer)) As Boolean
        'Purpose: Delete duplicate record in GO_Travel
        'Input: Record ID
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        For Each intTravelId As Integer In lstTravelIds
            strQuerry = "DELETE from GO_Travel where RecId=" & intTravelId
            cmdSql.CommandText = strQuerry
            cmdSql.ExecuteNonQuery()
        Next

        Return True
    End Function
    Public Function DeleteGO_Travel1S(ByVal htbTravelIds As Hashtable) As Boolean
        'Purpose: Delete duplicate record in GO_Travel
        'Input: Record ID
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        For Each intTravelId As Integer In htbTravelIds.Values
            strQuerry = "DELETE from GO_Travel where RecId=" & intTravelId
            cmdSql.CommandText = strQuerry
            cmdSql.ExecuteNonQuery()
        Next

        Return True
    End Function
    Public Function DeleteGO_Bypass(ByVal lstTravelIDs As List(Of Integer)) As Boolean
        'Purpose: Delete GO_Bypass
        'Input: Record ID
        'Output: Y/N
        For Each intTravelId As Integer In lstTravelIDs

            Dim strQuerry As String
            Dim cmdSql As New SqlClient.SqlCommand
            'Dim strFileName As String
            cmdSql.Connection = mcnxConnection

            strQuerry = "update GO_Bypass set Status='XX' where TrvlId=" & intTravelId
            cmdSql.CommandText = strQuerry
            cmdSql.ExecuteNonQuery()

            'Try
            '    strFileName = sglRecId & ".doc"
            '    strFileName = System.AppDomain.CurrentDomain.BaseDirectory() & "Doc\" _
            '                & strFileName
            '    My.Computer.FileSystem.DeleteFile(strFileName)
            'Catch ex As Exception
            '    strFileName = sglRecId & ".docx"
            '    strFileName = System.AppDomain.CurrentDomain.BaseDirectory() & "Doc\" _
            '                            & strFileName
            '    My.Computer.FileSystem.DeleteFile(strFileName)

            'End Try

        Next
        Return True
    End Function
    Public Function DeleteGO_Bypass1S(ByVal htbTravelIDs As Hashtable) As Boolean
        'Purpose: Delete GO_Bypass
        'Input: Record ID
        'Output: Y/N
        For Each intTravelId As Integer In htbTravelIDs.Values

            Dim strQuerry As String
            Dim cmdSql As New SqlClient.SqlCommand
            'Dim strFileName As String
            cmdSql.Connection = mcnxConnection

            strQuerry = "update GO_Bypass set Status='XX' where TrvlId=" & intTravelId
            cmdSql.CommandText = strQuerry
            cmdSql.ExecuteNonQuery()

            'Try
            '    strFileName = sglRecId & ".doc"
            '    strFileName = System.AppDomain.CurrentDomain.BaseDirectory() & "Doc\" _
            '                & strFileName
            '    My.Computer.FileSystem.DeleteFile(strFileName)
            'Catch ex As Exception
            '    strFileName = sglRecId & ".docx"
            '    strFileName = System.AppDomain.CurrentDomain.BaseDirectory() & "Doc\" _
            '                            & strFileName
            '    My.Computer.FileSystem.DeleteFile(strFileName)

            'End Try

        Next
        Return True
    End Function
    Public Function GetGO_COS(ByVal strBkgCls) As String
        'Purpose: Convert Bkg class into Global One's class of service
        'Input: Booking class
        'Output: GO's class of service

        Dim strQuerry As String = "select Details from GO_MISC where CAT='COS' and VAL='" & strBkgCls & "'"
        Dim strCOS As String
        Select Case strBkgCls
            Case "Y"
                Return "Y"
            Case Else
                strCOS = GetScalarAsString(strQuerry)

                Return IIf(strCOS = "", "Y", strCOS)

        End Select

    End Function
    Public Function GetCabinByCar(ByVal strBkgCls As String, strCar As String) As String
        'Purpose: Convert Bkg class into Global One's class of service
        'Input: Booking class
        'Output: GO's class of service
        Dim strQuerry As String
        strQuerry = "Select C from cwt.dbo.rbd_Cabin where status='OK' and AL='" & strCar _
            & "' and C like '%" & strBkgCls & "%'"
        If GetScalarAsString(strQuerry) = "" Then
            Return "Y"
        Else
            Return "C"
        End If
    End Function
    Public Function GetGO_ClassOfService(ByVal strBkgCls As String) As String
        'Purpose: Convert Bkg class into Global One's class of service
        'Input: Booking class
        'Output: GO's class of service

        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        Dim cnxSql As New SqlClient.SqlConnection(mstrConnectionString)

        cnxSql.Open()
        cmdSql.Connection = cnxSql

        strQuerry = "select RMK from GO_MISC where CAT='COS' and VAL='" & strBkgCls & "'"
        cmdSql.CommandText = strQuerry
        GetGO_ClassOfService = cmdSql.ExecuteScalar()
        If GetGO_ClassOfService = "" Then
            GetGO_ClassOfService = "ECONOMY"
        End If
    End Function
    Public Function GetFareType(ByVal strBkg As String, ByVal strValCar As String, _
                                ByVal dteDOI As Date, ByVal dteTvl As Date) As String
        'Purpose: Find Fare Type
        'Input: BkgCls, travel date, ticketing date
        'Result: Fare Type
        'Dim i As Integer
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "select FareType from Faretype where Status ='OK'"
        strQuerry = strQuerry & " and Valcar='" & strValCar & "'"
        strQuerry = strQuerry & " and '" & DateTime2Text(dteDOI) & "' between TktDateFrom and TktDateTo"
        strQuerry = strQuerry & " and '" & DateTime2Text(dteTvl) & "' between TvlDateFrom and TvlDateTo"
        strQuerry = strQuerry & " and Bkgs like'%" & strBkg & "%'"
        cmdSql.CommandText = strQuerry

        GetFareType = cmdSql.ExecuteScalar

    End Function
    Public Function GetVnDomCommSector(ByVal strFromCity As String, ByVal strToCity As String, _
                                    ByVal dteTvlDate As Date, ByVal dteTktDate As Date, _
                                    ByVal strFareType As String, ByVal strPaxType As String) _
                                    As Decimal
        'Purpose: Get comm for VN domestic sector
        'Input: Operating carrier, flight date
        'Output: commission amount

        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "select Comm from DomComm"
        strQuerry = strQuerry & " where Valcar='VN'"
        strQuerry = strQuerry & " and '" & DateTime2Text(dteTktDate) & "' between TktDateFrom and TktDateTo"
        strQuerry = strQuerry & " and '" & DateTime2Text(dteTvlDate) & "' between TvlDateFrom and TvlDateTo"
        strQuerry = strQuerry & " and (FromCity='' or FromCity='" & strFromCity & "')"
        strQuerry = strQuerry & " and (ToCity='' or ToCity='" & strToCity & "')"
        strQuerry = strQuerry & " and FareType='" & strFareType & "'"
        strQuerry = strQuerry & " and (PaxType ='' or PaxType like '%" & strPaxType & "%')"

        cmdSql.CommandText = strQuerry

        GetVnDomCommSector = cmdSql.ExecuteScalar
    End Function

    Public Function GetStoredConditions() As String()
        'Purpose: Get stored condition
        'Input: 
        'Output: array

        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        Dim drResult As SqlClient.SqlDataReader
        Dim arrResult(0 To 0) As String
        Dim i As Integer
        cmdSql.Connection = mcnxConnection

        strQuerry = "select Details from GO_MISC where CAT='Conditions'"

        cmdSql.CommandText = strQuerry

        drResult = cmdSql.ExecuteReader
        If Not drResult Is Nothing Then
            Do While drResult.Read
                ReDim Preserve arrResult(0 To i)
                arrResult(i) = Replace(drResult("Details"), vbCrLf, vbLf)
                i = i + 1
            Loop
        End If
        drResult.Close()
        GetStoredConditions = arrResult
    End Function

    Public Sub LoadCombo(ByRef cboInput As ComboBox, ByVal strQuerry As String)
        Dim daConditions As SqlClient.SqlDataAdapter
        Dim dsConditions As New DataSet

        daConditions = New SqlClient.SqlDataAdapter(strQuerry, mcnxConnection)
        If daConditions.Fill(dsConditions, "RESULT") > 0 Then
            cboInput.DataSource = dsConditions.Tables("RESULT")
            cboInput.DisplayMember = "Value"
            cboInput.ValueMember = "Value"
            'LoadCombo = cboInput
            dsConditions.Dispose()
            daConditions.Dispose()
        End If
    End Sub
    Public Function CreateCombo(ByRef cboInput As ComboBox, ByVal strQuerry As String) As ComboBox
        Dim daConditions As SqlClient.SqlDataAdapter
        Dim dsConditions As New DataSet

        daConditions = New SqlClient.SqlDataAdapter(strQuerry, mcnxConnection)
        If daConditions.Fill(dsConditions, "RESULT") > 0 Then
            cboInput.DataSource = dsConditions.Tables("RESULT")
            cboInput.DisplayMember = "Value"
            cboInput.ValueMember = "Value"
            dsConditions.Dispose()
            daConditions.Dispose()
        End If
        Return cboInput
    End Function
    Public Sub LoadComboDisplay(ByVal cboInput As ComboBox, ByVal strQuerry As String)
        Dim daConditions As SqlClient.SqlDataAdapter
        Dim dsConditions As New DataSet



        daConditions = New SqlClient.SqlDataAdapter(strQuerry, mcnxConnection)
        If daConditions.Fill(dsConditions, "RESULT") > 0 Then
            cboInput.DataSource = dsConditions.Tables("RESULT")
            cboInput.DisplayMember = "Display"
            cboInput.ValueMember = "Value"
            'LoadComboDisplay = cboInput
            dsConditions.Dispose()
            daConditions.Dispose()
        End If
    End Sub
    Public Sub LoadCmcNameListAsCombo(ByRef cboCmc As ComboBox)
        Dim strCmcList As String
        strCmcList = "Select distinct CompanyName as Display,Cmc as Value" _
                    & " from go_companyinfo1 where Status='OK'" _
                    & " and (GO_Client=1 or DataCapture=1) "
        LoadComboDisplay(cboCmc, strCmcList)
    End Sub
    Public Sub LoadFullCmcNameListAsCombo(ByRef cboCmc As ComboBox)
        Dim strCmcList As String
        strCmcList = "Select distinct CompanyName as Display,Cmc as Value" _
                    & " from go_companyinfo1 where Status='OK'"
        LoadComboDisplay(cboCmc, strCmcList)
    End Sub
    Public Sub LoadCmcCodeListAsCombo(ByRef cboCmc As ComboBox)
        Dim strCmcList As String
        strCmcList = "Select disctinct Cmc as Value" _
                    & " from GO_CompanyInfo1 where status='OK' order by Cmc"
        LoadCombo(cboCmc, strCmcList)
    End Sub
    Public Sub LoadCustShortNameListAsCombo(ByRef cboCustomers As ComboBox)
        Dim strCustList As String
        strCustList = "Select distinct CustShortName as Display,CustId as Value" _
                    & " from cwt.dbo.go_companyinfo1 where Status='OK' order by CustShortName"
        LoadComboDisplay(cboCustomers, strCustList)
    End Sub
    Public Function RasShortNameExists(ByRef strRasShortName As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "Select RecId from CustomerList" _
                    & " where CustShortName='" & strRasShortName _
                    & "' and APP like '%RAS%'"
        cmdSql.CommandText = strQuerry

        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If


    End Function

    Public Sub LoadRasShortNameListAsCombo(ByRef cboCustomer As ComboBox)
        Dim strShortNameQuerry As String
        strShortNameQuerry = "Select CustShortName as Value from CustomerList" _
                             & " where Status='OK' and RecId>0" _
                             & " and App like '%RAS%' order by CustShortName"
        LoadCombo(cboCustomer, strShortNameQuerry)

    End Sub
    Public Sub LoadRasShortNameListAsComboDisplay(ByRef cboCustomer As ComboBox)
        Dim strShortNameQuerry As String
        strShortNameQuerry = "Select Recid as Value,CustShortName as Display" _
                            & " from CustomerList" _
                             & " where Status='OK' and RecId>0" _
                             & " and App like '%RAS%' order by CustShortName"
        LoadComboDisplay(cboCustomer, strShortNameQuerry)
    End Sub
    Public Function DeleteGridViewRow(ByRef dbInput As DataGridView, ByVal strQuerry As String) As Boolean
        Dim strMessage As String
        Dim i As Integer

        strMessage = "Do you want to delete the following record?" & vbCrLf
        With dbInput
            For i = 0 To dbInput.Columns.Count - 1
                If .Columns.Item(i).Visible Then
                    strMessage = strMessage & .Columns.Item(i).HeaderText & ": " _
                                    & .CurrentRow.Cells.Item(i).Value & vbCrLf
                End If
            Next
        End With

        If MsgBox(strMessage, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok _
            AndAlso ExecuteNonQuerry(strQuerry) Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function DisplayGridViewRowAsMsg(ByRef dbInput As DataGridView) As Boolean
        Dim strMessage As String
        Dim i As Integer

        With dbInput
            For i = 0 To dbInput.Columns.Count - 1
                If .Columns.Item(i).Visible Then
                    strMessage = strMessage & .Columns.Item(i).HeaderText & ": " _
                                    & .CurrentRow.Cells.Item(i).Value & vbCrLf
                End If
            Next
        End With

        Return True

    End Function

    Public Function DeleteBooker(ByVal intCustId As Integer, ByVal strBooker As String _
                                , ByVal strUser As String) As Boolean
        Dim strMessage As String

        strMessage = "Do you want to delete the following record?" & vbCrLf

        If MsgBox(strMessage, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Dim arrQuerry(0 To 0) As String
            arrQuerry(0) = "Update SIR set Status='XX', LstUpdate=getdate()" _
                            & " where Fname='BOOKER' and Custid=" & intCustId _
                            & " and Fvalue=N'" & strBooker & "'"

            If Update(arrQuerry) Then
                Return True
            Else
                Return False
            End If
        End If

    End Function
    Public Function GetListSign1S() As String
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        Dim drResult As SqlClient.SqlDataReader
        Dim strResult As String = ""

        cmdSql.Connection = mcnxConnection

        strQuerry = "select VAL from GO_MISC where CAT='Sign1S'"

        cmdSql.CommandText = strQuerry

        drResult = cmdSql.ExecuteReader
        If Not drResult Is Nothing Then
            Do While drResult.Read
                strResult = strResult & drResult("VAL") & ","
            Loop
        End If
        If strResult <> "" Then
            strResult = RemoveLastChr(strResult)
        End If
        drResult.Close()
        GetListSign1S = strResult
    End Function

    Public Function GetStaffNameByTVSI(ByVal intTvsi As Integer) As String
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select RecId from F1S_TVSI where RecId=" & intTvsi

        cmdSql.CommandText = strQuerry
        Return cmdSql.ExecuteScalar

    End Function
    Public Function SignInAvailable() As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        Dim strResult As String
        cmdSql.Connection = mcnxConnection

        strQuerry = "select recid from sign1s where prg='CWT'" _
                    & " and datediff(s,lastupdt,getdate())>10"

        cmdSql.CommandText = strQuerry
        strResult = cmdSql.ExecuteScalar

        If strResult = "" Then
            SignInAvailable = False
        Else
            SignInAvailable = True
        End If
    End Function


    Public Function UpdateDefaultHierachy(ByVal strCmc As String) As Boolean
        'Purpose: update hierachy in GO_DefaultHierachy for record with blank hierachy 2 - 5
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection
        Dim intHierNbr As Integer

        For intHierNbr = 1 To 5
            strQuerry = "update GO_Travel set Hierachy" & intHierNbr & "=(select top 1 DefaultValue" _
                                & " from GO_Hierachies where Status='OK' and DefaultValue<>''" _
                                & " and CMC='" & strCmc & "' and Nbr=" & intHierNbr _
                                & ") where Hierachy" & intHierNbr & "='' and CMC='" & strCmc _
                                & "' and (select top 1 DefaultValue from GO_Hierachies" _
                                & " where Status='OK' and CMC='" & strCmc _
                                & "' and Nbr=" & intHierNbr & ") <>''"

            cmdSql.CommandText = strQuerry
            cmdSql.ExecuteNonQuery()
        Next

        UpdateDefaultHierachy = True
    End Function

    Public Function UpdateGoTravelDOI(ByVal intRecId As Integer) As Boolean
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "update GO_Travel set DOI=(select top 1 DOI from GO_Air where TravelId=" & intRecId _
                    & " order by DOI) where RecId=" & intRecId

        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()

        UpdateGoTravelDOI = True
    End Function
    Public Function UpdateGoTravelBkgTool(ByVal intRecId As Integer, ByVal strBkgTool As String) As Boolean
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "update GO_Travel set BkgTool='" & strBkgTool _
                    & "' where BkgMethod='G' and RecId=" & intRecId

        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()
        UpdateGoTravelBkgTool = True

    End Function
    Public Function UpdateGoTravelDefaultValues(ByVal intRecId As Integer, ByVal strBkgTool As String) As Boolean
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "update GO_Travel set BkgDate=DOI, BkgTool='" & strBkgTool _
                    & "' where BkgMethod='G' and RecId=" & intRecId

        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()
        UpdateGoTravelDefaultValues = True

    End Function
    Public Function UpdateGoAirDefaultValues(ByVal intRecId As Integer, ByVal strTkno As String _
                                            , ByVal strDepDates As String, ByVal strArrDates As String _
                                            , ByVal strFltNbrs As String, ByVal strETD As String _
                                            , ByVal strETA As String, ByVal strSOs As String) As Boolean
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "update GO_Air set RefFare=Fare,LowestFare=Fare,DepDates='" & strDepDates _
                    & "', ArrDateIndicators='" & strArrDates _
                    & "', FltNbrs='" & strFltNbrs _
                    & "', etd='" & strETD _
                    & "', eta='" & strETA _
                    & "', SO='" & strSOs _
                    & "' where Recid=" & intRecId _
                    & "  and TKNO='" & strTkno & "'"

        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()
        UpdateGoAirDefaultValues = True

    End Function
    Public Sub LoadDataGridView(ByRef dgInput As DataGridView, ByVal strQuerry As String)
        Dim daConditions As SqlClient.SqlDataAdapter
        Dim dsConditions As New DataSet

        daConditions = New SqlClient.SqlDataAdapter(strQuerry, mstrConnectionString)
        daConditions.Fill(dsConditions, "Result")
        dgInput.DataSource = dsConditions.Tables("Result")
        dgInput.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgInput.AutoResizeColumns()
        dsConditions.Dispose()
        daConditions.Dispose()

    End Sub
    Public Function GetUnselectedTktListingDefaultColumns(intCustId As Integer, strDOMINT As String) As List(Of String)
        Dim tblDataCode As System.Data.DataTable
        Dim i As Integer
        Dim lstDataCodes As New List(Of String)
        tblDataCode = GetDataTable("Select DataCode from GO_TktListing" _
                                    & " where CustId=0 and Status='OK' and DefaultUse='True'" _
                                    & " and DataCode not in (Select DataCode from GO_TktListing" _
                                    & " where Status='OK' and CustId=" & intCustId _
                                    & " and DOMINT='" & strDOMINT & "')")
        For i = 0 To tblDataCode.Rows.Count - 1
            lstDataCodes.Add(tblDataCode.Rows(i)("DataCode"))
        Next
        Return lstDataCodes
    End Function
    Public Function CreateCityPair(ByVal strCityPair As String) As Boolean
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        If CityPairExist(strCityPair) Then
            Exit Function
        End If
        cmdSql.Connection = mcnxConnection

        strQuerry = "Insert into GO_CITYPAIR (Citypair) values('" & strCityPair & "')"

        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()

        CreateCityPair = True
    End Function
    Public Function CityPairExist(ByVal strCityPair As String) As Boolean
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "select CityPair from GO_CITYPAIR where CityPair='" & strCityPair & "'"

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar = "" Then
            CityPairExist = False
        Else
            CityPairExist = True
        End If
    End Function
    Public Function UpdateETAs(ByVal intRecId As Integer, ByVal strETAs As String) As Boolean
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "update go_air set ETA='" & strETAs & "' where Recid=" & intRecId

        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()

        UpdateETAs = True
    End Function
    Public Function GetETA(ByVal strCityPair As String, ByVal strCar As String _
                            , ByVal strFltNbr As String) As String
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "select ETA from GO_AirSC where CityPair='" & strCityPair _
                    & "' and Car='" & strCar & "' and FltNbr='" & strFltNbr & "'"

        cmdSql.CommandText = strQuerry
        GetETA = cmdSql.ExecuteScalar()

    End Function
    Public Function GetElapsedTime(ByVal strCityPair As String, ByVal strCar As String _
                            ) As Integer
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "select ElapsedTime from GO_AirSC where CityPair='" & strCityPair _
                    & "' and Car='" & strCar & "'"

        cmdSql.CommandText = strQuerry
        GetElapsedTime = cmdSql.ExecuteScalar()

    End Function


    Public Function GetRequiredHiearchies(ByVal strCmc As String) As String
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        Dim drResult As SqlClient.SqlDataReader
        Dim strRequiredHierachies As String = ""

        cmdSql.Connection = mcnxConnection

        strQuerry = "select VAL from GO_Misc where CAT='RequiredHierachy' and RMK='" _
                    & strCmc & "'"

        cmdSql.CommandText = strQuerry
        drResult = cmdSql.ExecuteReader
        If drResult.HasRows Then
            Do While drResult.Read
                strRequiredHierachies = strRequiredHierachies & drResult("VAL") & ","
            Loop
            strRequiredHierachies = RemoveLastChr(strRequiredHierachies)
        End If
        drResult.Close()
        GetRequiredHiearchies = strRequiredHierachies
    End Function
    Public Function GetTravelIdByRloc1S(ByVal strRloc As String, ByVal strPaxName As String) As Long
        'Chi lay khi SRV='S'
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select top 1 T.RECID from go_travel T, GO_AIR A" _
                    & " where RLOC='" & strRloc & "' and A.SRV='S' and T.BkgTool='SAB'" _
                    & " and T.RECID=A.TRAVELID and TravellerName='" & strPaxName _
                    & "' order by T.RECID desc"

        cmdSql.CommandText = strQuerry
        GetTravelIdByRloc1S = cmdSql.ExecuteScalar
    End Function
    Public Function UpdateMatchNameByRecId(ByVal strRloc As String, ByVal strEmail As String _
                                            , ByVal intRecId As Integer) As Boolean
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "update GO_MatchName set RLOC='" & strRloc _
                    & "',Email='" & strEmail & "' where RecId=" & intRecId

        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()

        Return True
    End Function
    Public Function InsertGoMatchName(ByVal strPaxName As String, ByVal strNameInProfile As String _
                                    , ByVal strCompany As String, ByVal strRloc As String _
                                    , ByVal strEmail As String) As Boolean
        'Purpose:  
        'Output: Y/N
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "insert into GO_MatchName (PaxName,NameInProfile,Company" _
                    & ",RLOC,Email) values('" & strPaxName _
                    & "','" & strNameInProfile & "','" & strCompany _
                    & "','" & strRloc & "','" & strEmail & "')"

        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()

        Return True
    End Function
    Public Function CheckDupGoMatchName(ByVal strPaxName As String, ByVal strRloc As String) As Boolean
        'Purpose:  
        'Output: Y/N  Dup/no dup
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "select Recid from GO_MatchName where PaxName='" & strPaxName _
                    & "' and Rloc='" & strRloc & "'"

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetAquaQs(ByVal strCmc As String) As Collection
        'Purpose:  
        'Output: Y/N  Dup/no dup
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        Dim drResult As SqlClient.SqlDataReader
        Dim colResult As New Collection
        cmdSql.Connection = mcnxConnection

        strQuerry = "select top 1 * from GO_CompanyInfo1 where Status='OK' and Cmc='" & strCmc & "'"

        cmdSql.CommandText = strQuerry
        drResult = cmdSql.ExecuteReader

        Do While drResult.Read
            If drResult("AquaOffc1") <> "" Then
                colResult.Add(drResult("AquaOffc1") & "/" & drResult("AquaQueue1"))
            End If
            If drResult("AquaOffc2") <> "" Then
                colResult.Add(drResult("AquaOffc2") & "/" & drResult("AquaQueue2"))
            End If
            Exit Do
        Loop
        drResult.Close()
        GetAquaQs = colResult
    End Function
    Public Function OptionQueueRequired(ByVal strCmc As String) As Boolean
        'Purpose:  
        'Output: Y/N  
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        Dim intResult As Integer

        cmdSql.Connection = mcnxConnection

        strQuerry = "select top 1 OptQ from GO_CompanyInfo1 where Status='OK' and Cmc='" & strCmc & "'"

        cmdSql.CommandText = strQuerry
        intResult = cmdSql.ExecuteScalar

        If intResult = 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function EmplIdRequired(ByVal strCmc As String) As Boolean
        'Purpose:  
        'Output: Y/N  
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        Dim intResult As Integer

        cmdSql.Connection = mcnxConnection

        strQuerry = "select top 1 EmployeeId from GO_CompanyInfo1 where Status='OK' and Cmc='" & strCmc & "'"

        cmdSql.CommandText = strQuerry
        intResult = cmdSql.ExecuteScalar

        If intResult = 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub DeleteDeactivatedTravellerProfiles(ByVal strCompany As String)

        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "Update go_travellerprofile set Status='XX' where CompanyName='" _
                    & strCompany & "' and RLOC not in" _
                    & "(Select RLOC from go_travellerprofile where CompanyName='" _
                    & strCompany & "' and Status='--')"

        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()

        strQuerry = "Delete go_travellerprofile where CompanyName='" _
                    & strCompany & "' and Status='--'"
        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()

    End Sub
    Public Function GetHotelChainCodeCwtByName(ByVal strChainName As String) As String
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select CwtCode from GO_HotelChainsCwt where ChainsName='" _
                    & strChainName & "'"

        cmdSql.CommandText = strQuerry
        Return cmdSql.ExecuteScalar
    End Function
    Public Function GetHotelAddr4NonAir(ByVal strHotelName As String) As String
        Dim strQuerry As String = "select top 1 Address from Supplier where status='OK' and FullName='" _
                                & strHotelName & "'"
        Return GetScalarAsString(strQuerry)
    End Function
    Public Function GetHotelChainCodeCwtByTvCode(ByVal strTvCode As String) As String
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select CwtCode from GO_HotelChainsTv where TvCode='" _
                    & strTvCode & "'"

        cmdSql.CommandText = strQuerry
        Return cmdSql.ExecuteScalar
    End Function
    Public Function GetRoomTypeCwtByTvCode(ByVal strTvCode As String) As String
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select CwtCode from GO_RoomTypeTV where Status='OK' and TvCode='" _
                    & strTvCode & "'"

        cmdSql.CommandText = strQuerry
        Return cmdSql.ExecuteScalar
    End Function
    Public Function CheckRoomTypeCwt(ByVal strRoomType As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select Recid from GO_RoomTypeCwt where CwtCode='" & strRoomType & "'"

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function CheckHarpKey(ByVal intHtlKey As Integer, ByVal strCity As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select Recid from GO_HotelListCwt where HarpKey=" & intHtlKey

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetHarpKeyByTvNameCity(ByVal strTvHtlName As String, ByVal strCityCode As String) As String
        Dim strQuerry As String = "select HarpKey from GO_HotelListTv where (Status='OK' or Status='--')" _
                    & " and HtlName='" & strTvHtlName & "' and CityCode='" & strCityCode & "'"

        Return GetScalarAsString(strQuerry)
    End Function
    Public Function GetHarpKeyFromAllSources(ByVal strTvHtlName As String, ByVal strCityCode As String) As Integer

        strTvHtlName = Replace(strTvHtlName, "'", "*")
        Dim strQuerry As String = "select HarpKey from GO_HotelListTv where Status='OK'" _
                    & " and HtlName='" & strTvHtlName & "' and CityCode='" & strCityCode & "'"
        Dim strHarpKey As String
        strHarpKey = GetScalarAsString(strQuerry)
        If strHarpKey = "" Then
            strHarpKey = GetScalarAsString("select top 1 HarpKey from GO_HotelListCwt where Status='V' and " _
                                        & " HtlName='" & strTvHtlName & "' and CityCode='" & strCityCode _
                                        & "' order by HarpKey desc")
        End If
        If strHarpKey = "" Then
            Return 0
        Else
            Return CInt(strHarpKey)
        End If

    End Function
    Public Function HarpKeyExist(ByVal lngHarpKey As Long) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select Recid from GO_HotelListCwt where Status='V' and HarpKey=" & lngHarpKey

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function InsertCwtHotel(ByVal lngHarpKey As Long, ByVal strHotelName As String _
                                , ByVal strCityCode As String, ByVal strTel As String _
                                , ByVal strAddress As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "insert into GO_HotelListCwt (HarpKey,Htlname,Adrs1,Tel,CityCode)" _
                    & " values (" & lngHarpKey & ",'" & strHotelName _
                    & "','" & strAddress & "','" & strTel & "','" & strCityCode & "')"

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function UpdateGo_HotelWzHarpKey(ByVal lngHarpKey As Long, ByVal strHotelName As String _
                                            , ByVal strCityCode As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "Update GO_Hotel set LocalIntlCode=" & lngHarpKey _
                    & " where LocalIntlCode=0 and substring(HotelName,1,25) ='" _
                    & Mid(strHotelName, 1, 25).Trim _
                    & "' AND CityCode='" & strCityCode & "'"

        cmdSql.CommandText = strQuerry
        cmdSql.ExecuteNonQuery()

        Return True
    End Function
    Public Function HotelListTvExist(ByVal strTvHtlName As String, ByVal strCityCode As String _
                                    ) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "Select Recid from GO_HotelListTv  where Status<>'XX' and HtlName='" _
                    & strTvHtlName & "' and CityCode='" & strCityCode & "'"

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function Insert2GO_HotelListTv(ByVal strTvHtlName As String, ByVal strCityCode As String _
                                    , ByVal strTel As String, ByVal strAddress As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection
        If Not HotelListTvExist(strTvHtlName, strCityCode) Then
            strQuerry = "insert into GO_HotelListTv (HtlName,Tel,Address,CityCode) values ('" _
                    & strTvHtlName & "','" & strTel & "','" & strAddress.Replace("'", "*") _
                    & "','" & strCityCode & "')"

            cmdSql.CommandText = strQuerry
            cmdSql.ExecuteNonQuery()
        End If
        Return True
    End Function
    Public Function GetUserRole(ByVal strUserName As String) As String

        Return GetScalarAsString("Select Details from GO_MISC where CAT='UserRole'" _
                    & " and VAL='" & strUserName & "'")
    End Function
    Public Function GetDataCompletionDate1ASGN() As String
        Return GetScalarAsString("Select Details from TVCS.dbo.MISC where CAT='raschecker'" _
                    & " and RMK='SGN'")
    End Function
    Public Function GetDataCompletionDate1SSGN() As String
        Return GetScalarAsString("Select Details from FT.dbo.MISC where CAT='M1S'" _
                    & " and VAL='CompletionDate' and RMK='SGN'")
    End Function
    Public Function IsInvCodeRequired(ByVal strCustShortName As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select Recid from GO_Misc where Details='OK'" _
                    & " and CAT='InvCodeRequired' and Val='" & strCustShortName & "'"

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsSosClient(ByVal strCmc As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select Recid from GO_Misc where Details='OK'" _
                    & " and CAT='SOS' and Val='" & strCmc & "'"

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function FindCorpFareExclusion(ByVal strRasShortName As String _
                                    , ByVal strCarCode As String _
                                    , ByVal strTourCode As String _
                                    , ByVal dteDOI As Date) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select Recid from GO_CorpFareExclusion where Status='OK'" _
                    & " and Carcode='" & strCarCode & "' and RasShortName='" _
                    & strRasShortName & "' and CharIndex(TourcodeContains,'" _
                    & strTourCode & " ')>0 and '" & DateTime2Text(dteDOI) _
                    & "' between TktDateFrom and TktDateTo"

        cmdSql.CommandText = strQuerry
        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetTaxCode4ReissueFee(ByVal strCar3D_Code As String) As String
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select TaxCode from GO_TaxCode4ReissueFee where Status='OK'" _
                    & " and Car3D_Code='" & strCar3D_Code & "'"

        cmdSql.CommandText = strQuerry
        Return cmdSql.ExecuteScalar
    End Function
    Public Function ManualRasRequired(ByVal strCar3D_Code As String, ByVal strTktMethod As String) _
                                As Boolean
        Dim strQuerry As String

        strQuerry = "select RecId from GO_NoCommFareMatch where ManualRas=1 and Status='OK'" _
                    & " and getdate() between TktDateFrom and TktDateTo" _
                    & " and Car3D_Code='" & strCar3D_Code & _
                    "' and TktMethod='" & strTktMethod & "'"

        If GetScalarAsDecimal(strQuerry) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function GetFareField(ByVal strCar3D_Code As String, ByVal strTktMethod As String) _
                                As String
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select FareField from GO_NoCommFareMatch where Status='OK'" _
                    & " and getdate() between TktDateFrom and TktDateTo" _
                    & " and Car3D_Code='" & strCar3D_Code _
                    & "' and TktMethod='" & strTktMethod & "'"

        cmdSql.CommandText = strQuerry
        Return cmdSql.ExecuteScalar
    End Function
    Public Function IsNoCommAppllied(ByVal strCmc As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select Recid from GO_CompanyInfo1 where Status='OK'" _
                    & " and NoComm=1" _
                    & " and getdate() between TktDateFrom and TktDateTo" _
                    & " and Cmc='" & strCmc & "'"

        cmdSql.CommandText = strQuerry

        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function VmpdUsed(ByVal strCar3D_Code As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select Recid from GO_MiscWzDate where Status='OK'" _
                    & " and getdate() between TktDateFrom and TktDateTo" _
                    & " and Value='" & strCar3D_Code & "'"

        cmdSql.CommandText = strQuerry

        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function InvalidTourCode(ByVal strCounter As String, ByVal strTC As String _
                                    , ByVal intCustID As Integer) As Boolean
        Dim RecNo As Integer
        If strCounter <> "CWT" Then Return True
        RecNo = ScalarToInt("Ras12.dbo.DuToan_Tour", "RecID", " custid=" & intCustID & " and TCode='" & strTC & _
            "' and status not in ('RR','XX') and BillingBy in ('Event','Bundle') and sdate >getdate()")
        If RecNo = 0 Then Return True
        Return False
    End Function
    Public Function ScalarToInt(ByVal pTbl As String, ByVal pField As String, ByVal pDK_Order As String) As Integer
        Dim KQ As Integer
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection
        cmdSql.CommandText = "SELECT " & pField & " from " & pTbl & " where " & Finetune_pDK(pDK_Order)
        KQ = cmdSql.ExecuteScalar
        Return KQ
    End Function

    Private Function Finetune_pDK(ByVal pDK As String) As String
        If pDK.Trim.Substring(0, 5).ToUpper = "WHERE" Then
            Return pDK.Trim.Substring(5)
        End If
        Return pDK
    End Function

    Public Function GetCustomerByTsNbr(ByVal strTsNbr As String) As clsVatCustomer
        Dim strQuerry As String
        Dim drCustomer As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        Dim objCustomer As New clsVatCustomer
        cmdSql.Connection = mcnxConnection

        strQuerry = "select top 1 * from customerlist where recid=" _
                    & "(select CUSTID from ras12.dbo.RCP where status='ok'" _
                    & " AND rcpno='TS" & strTsNbr & "')"

        cmdSql.CommandText = strQuerry
        drCustomer = cmdSql.ExecuteReader
        Do While drCustomer.Read
            With objCustomer
                .CustomerName = drCustomer("CustFullName")
                .Addr = drCustomer("CustAddress")
                .VatNbr = drCustomer("CustTaxCode")
            End With
        Loop
        drCustomer.Close()
        Return objCustomer
    End Function
    Public Function CheckTsCounter(ByVal strTs As String, ByVal strCounter As String) As Boolean
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select count (*) from RAS12.DbO.RCP where Status='OK'" _
                    & " and RCPNo='TS" & strTs _
                    & "' and Counter='" & strCounter & "'"

        cmdSql.CommandText = strQuerry

        If cmdSql.ExecuteScalar > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetLocation() As String
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select TOP 1 Substring(VAL,1,1) from TVCS.dbo.POS where CAT='POS'"

        cmdSql.CommandText = strQuerry
        Select Case cmdSql.ExecuteScalar
            Case 1
                Return "HAN"
            Case 0
                Return "SGN"
            Case Else
                Return ""
        End Select
    End Function
    Public Function GetRtgTypeRas(ByVal strRtgType As String, ByVal strValCar As String) As String
        'Purpose: Tim loai hanh trinh noi dia hay quoc te theo hang xuat ve
        'Input: Loai Hanh trinh chung va hang xuat ve
        'Output: Loai hanh trinh INT hoac DOM duoc dung trong RAS
        Dim strCarCountry As String

        Select Case strRtgType
            Case "INTL"
                GetRtgTypeRas = "INT"
            Case Else
                strCarCountry = GetCarCountry(strValCar)
                If strCarCountry <> Mid(strRtgType, 1, 2) Then
                    GetRtgTypeRas = "INT"
                Else
                    GetRtgTypeRas = "DOM"
                End If
        End Select

    End Function
    Public Function GetCarCountry(ByVal strCar2A As String) As String
        'Tim ma nuoc noi dia cho mot carrier
        'Input: Ma 2 chu cua carrier
        'Output: Ma nuoc
        Dim strQuerry As String
        Dim cmdSql As New SqlClient.SqlCommand

        cmdSql.Connection = mcnxConnection

        strQuerry = "select top 1 Country from tvcs.dbo.Airline where ALCode ='" & strCar2A & "'"

        cmdSql.CommandText = strQuerry
        Return cmdSql.ExecuteScalar
    End Function

    Public Function GetFtExcOids() As String
        'Purpose: Get office IDs that are not counted when prg finds the TA queueing office.
        'Input: NA
        'Output: List of office ids, seperated by semi comma
        Return GetScalarAsString("select top 1 VAL from MISC where CAT='ExcOids'")

    End Function
    Public Function GetCusIdByOid(ByVal strOffcId As String) As Integer
        Return GetScalarAsString("select top 1 CustId from officeid where Status='OK' and OfficeID ='" _
                                & strOffcId & "'")

    End Function
    Public Function GetCustShortNameByCustId(ByVal intCustId As Integer) As String
        Return GetScalarAsString("select top 1 CustShortName from CustomerList where Status='OK'" _
                                & " and RecID =" & intCustId)

    End Function
    'Public Function GetRoe4TVS(ByVal strCur As String) As Decimal
    '    Return GetScalarAsDecimal("Select top 1 BSR from ForEX where IsActive='Y'" _
    '                            & " and ApplyROETo like '%TS%'" _
    '                            & " and Currency='" & strCur & "' order by RecId desc")

    'End Function
    Public Function IssuedByTV1A(ByVal strTkno As String) As Boolean
        Dim lngResult As Long
        lngResult = GetScalarAsDecimal("Select Recid from TVCS.dbo.DailyReport where TKNO='" _
                    & strTkno & "'")
        If lngResult = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IssuedByTV1S(ByVal strTkno As String) As Boolean
        Dim lngResult As Long
        lngResult = GetScalarAsDecimal("Select Recid from M1S_SrpTkts where TKNO='" _
                    & strTkno & "'")
        If lngResult = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function GetTktEntry(ByVal strValCar As String, ByVal strTktBox As String _
                                , ByVal strLocation As String) As String
        'Purpose: Find additional Ticket entry to be inserted into TST
        'Input: Validatint carrier, ISI
        'Output: Tour Code
        Dim strQuerry As String
        strQuerry = "select top 1 Value from TktEntries where Status ='OK' and ValCar ='" _
                    & strValCar & "' and getdate() between TktDateFrom and TktDateTo" _
                    & " and Catergory ='" & strTktBox _
                    & "'and Location='" & strLocation & "'"

        Return GetScalarAsString(strQuerry)
    End Function
    Public Function IsFxpTqnRequired(ByVal strValCar As String, ByVal strLocation As String) As Boolean
        Select Case GetTktEntry(strValCar, "FxpTqnRequired", strLocation)
            Case "YES"
                Return True
            Case Else
                Return False
        End Select
    End Function
    Public Function IsPaxTitleRequired(ByVal strLocation As String, ByVal strGds As String _
                                    , ByVal strValCar As String) As Boolean
        Dim strQry As String
        strQry = "Select top 1 Recid from TVCS.dbo.AutoIssue1 where Status='OK'" _
                & " and Getdate() between TktDateFrom and TktDateTo" _
                & " and Catergory='PaxTitleRequired'" _
                & " and GDS='" & strGds & "' and Location='" & strLocation _
                & "' and Value='" & strValCar & "'"

        If GetScalarAsDecimal(strQry) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsNoSqCorpFareApplied(ByVal strLocation As String, ByVal strPaxName As String) As Boolean
        Dim strQry As String
        strQry = "Select top 1 Recid from TVCS.dbo.TktEntries where Status='OK'" _
                & " and Getdate() between TktDateFrom and TktDateTo" _
                & " and Catergory='NoSqCorpFares'" _
                & " and ValCar='SQ' and Location='" & strLocation _
                & "' and Value='" & strPaxName & "'"

        If GetScalarAsDecimal(strQry) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsSFPD4SegRequired(ByVal strOpeCar As String, ByVal dteFltDate As Date _
                                    , ByVal strCountry As String) As Boolean
        'Purpose: Check if SFPD is required for a segment
        'Input: Operating carrier, flight date
        'Output: Y/N
        Dim intResult As Integer

        If strOpeCar = "//" Then
            Return False
        ElseIf strCountry = "US" Then
            Return True
        End If

        intResult = GetScalarAsDecimal("select top 1 RecId from SFPD where Status ='OK'" _
                & " and OpeCar ='" & strOpeCar _
                & "' and getdate() between tvldatefrom and tvldateto")

        If intResult = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsApisRequired4Apt(ByVal strValCar As String, ByVal strAirport As String _
                                    , ByVal strRtgType As String) As Boolean
        'Purpose: Check if APIS is required for a Airport
        'Input: Validating carrier, Airport code, Rtg Type
        'Output: Y/N
        Dim strQry As String

        strQry = "select top 1 Recid from APIS where Status ='OK'" _
                & " and getdate() between TktDateFrom and TktDateTo" _
                & " and ValCar='" & strValCar _
                & "' and (RtgType='' or RtgType ='" & strRtgType & "')" _
                & " and (CountriesEnRoute='' or CountriesEnRoute like '%" & GetCountryCode(strAirport) & "%')"

        If GetScalarAsDecimal(strQry) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function


    Public Function IsWlProhibitted(ByVal strValCar As String _
                                    , ByVal strLocation As String, ByVal strGds As String) As Boolean
        'Purpose": Check if airline prohibite ticketing for Waitlisted ticket
        'Input: validating carrier
        'Output: Yes/No

        Dim strQry As String

        strQry = "select top 1 Recid from AutoIssue1 where Status ='OK'" _
                & " and Catergory ='NotWL' and Value ='" & strValCar _
                & "' and getdate() between tktdatefrom and tktdateto" _
                & " and Location='" & strLocation _
                & "' and GDS='" & strGds & "'"

        If GetScalarAsDecimal(strQry) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsPrepaidCust(ByVal intCustId As Integer) As Boolean
        Dim strQry As String

        strQry = "Select top 1 PPCoef from cc_blc where custid =" & intCustId & " order by Recid Desc"

        If GetScalarAsDecimal(strQry) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function CheckCredit(ByVal decHoldAmt As Decimal, ByVal intCustId As Integer) As Boolean
        Dim strQry As String

        strQry = "Select top 1 case crcoef when 0 then VND_PPD_Avail else VND_PSP_Avail end" _
                & " from ras12.dbo.cc_blc where custid =" & intCustId & " and City='" & myStaff.City & "'"

        strQry = strQry & " order by recid desc"
        If GetScalarAsDecimal(strQry) >= decHoldAmt Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsOverDued(ByVal intCustId As Integer) As Boolean
        Dim strQry As String


        strQry = "Select count (*) from tvcs.dbo.overduecust where custid =" & intCustId

        If GetScalarAsDecimal(strQry) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ChangeCreditBalance(ByVal intCustId As Integer, ByVal strLocation As String _
                                        , ByVal strRloc As String) As Boolean

        Dim arrQuerry(0 To 0) As String
        Dim curFtSale As Decimal
        curFtSale = SumFtSales(intCustId, strLocation)
        arrQuerry(0) = "insert CC_BLC (CustId,PPCoef,CRCoef,PPD_Depo,PSP_UnUsed" _
                & ",PPD_Used,PSP_UnPaid,PSP_UnInv,BG,CR,CustShortName" _
                & ",SabreSale,FT_Sale,FstUser,RMK)" _
                & " select top 1 CustId,PPCoef,CRCoef,PPD_Depo,PSP_UnUsed" _
                & ",PPD_Used,PSP_UnPaid,PSP_UnInv,BG,CR,CustShortName" _
                & ",SabreSale," & curFtSale & ",'" & pstrPrg _
                & "','" & strRloc & "/HOLD'" _
                & " from cc_blc where custid=" & intCustId & " order by recid desc"


        If Update(arrQuerry) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SumFtSales(ByVal intCustId As Integer, ByVal strLocation As String) As Decimal
        Dim strFtSales As String
        strFtSales = "Select isnull(Sum(CreditAmt*ROE*Qty),0) from TKT_1A" _
                    & " where Qty>0 and PRG in ('TTQ','TTP') and SRV in ('S','R') and status in ('OK','QQ')" _
                    & " and Custid=" & intCustId

        Return GetScalarAsDecimal(strFtSales)
    End Function

    Public Function HoldCredit(ByVal intCustId As Integer, ByVal curHoldAmt As Decimal _
                               , ByVal blnPrePaid As Boolean, ByVal strRloc As String) As Boolean
        Dim strQuerry As String
        'Dim strColumns As String, strValues As String
        Dim arrQuerries(0 To 0) As String

        If blnPrePaid Then
            strQuerry = "insert CC_BLC (CustId,PPCoef,CRCoef,VND_PSP_Avail,PPD_Depo,PSP_UnUsed" _
                    & ",PPD_Used,PSP_UnPaid,PSP_UnInv,BG,CR,CustShortName" _
                    & ",FT_Sale,SabreSale,FstUser,RMK,VND_PPD_Avail)" _
                    & " select top 1 CustId,PPCoef,CRCoef,VND_PSP_Avail,PPD_Depo,PSP_UnUsed" _
                    & ",PPD_Used,PSP_UnPaid,PSP_UnInv,BG,CR,CustShortName" _
                    & ",FT_Sale,SabreSale,'" & pstrPrg _
                    & "','" & strRloc & "/HOLD',VND_PPD_Avail-" & curHoldAmt _
                    & " from cc_blc where custid=" & intCustId & " order by recid desc"

        Else
            strQuerry = "insert CC_BLC (CustId,PPCoef,CRCoef,VND_PSP_Avail,PPD_Depo,PSP_UnUsed" _
                    & ",PPD_Used,PSP_UnPaid,PSP_UnInv,BG,CR,CustShortName" _
                    & ",FT_Sale,SabreSale,FstUser,RMK,VND_PPD_Avail)" _
                    & " select top 1 CustId,PPCoef,CRCoef,VND_PPD_Avail,PPD_Depo,PSP_UnUsed" _
                    & ",PPD_Used,PSP_UnPaid,PSP_UnInv,BG,CR,CustShortName" _
                    & ",FT_Sale,SabreSale,'" & pstrPrg _
                    & "'," & strRloc & "/HOLD',VND_PSP_Avail-" & curHoldAmt _
                    & " from cc_blc where custid=" & intCustId & "' order by recid desc"
        End If
        arrQuerries(0) = strQuerry

        'strColumns = "RLOC,CustId, ActionType,HoldAmt,RMK,GDS,BalanceB4Hold"
        'strValues = "'" & strRloc & "'," & intCustId & ",'ISSUE'," _
        '            & curHoldAmt & ",'" & pstrPrg & "','" & pstrGds & "'" _
        '            & "," & mcurBalanceB4Hold
        'strQuerry = "insert into HoldCredit ("
        'strQuerry = strQuerry & strColumns & ") values ("
        'strQuerry = strQuerry & strValues & ")"
        'arrQuerries(2) = strQuerry

        If Update(arrQuerries) Then
            'sglHoldCreditRecId = lngInsertRecId
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetEmailsByCustId(ByVal intCustId As Integer _
                    , ByVal strDept As String) As String()
        Dim strEmails As String

        strEmails = GetScalarAsString("select Emails from Cust_email" _
                                    & " where status='OK' and dept='" _
                                    & strDept & "' and CustId=" & intCustId)
        Return Split(strEmails.Trim, ";")
    End Function
    Public Function AcceptCheckReason(ByVal intRecId As Integer) As Boolean
        Dim arrQuerries(0 To 0) As String
        arrQuerries(0) = "Update GO_Checks set Accepted='True' where RecId=" & intRecId
        If Update(arrQuerries) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ChangeCheckReason(ByVal intRecId As Integer _
                                    , ByVal strNewReason As String) As Boolean
        Dim arrQuerries(0 To 0) As String
        arrQuerries(0) = "Update GO_Checks set Reason='" & strNewReason _
                    & "' where RecId=" & intRecId
        If Update(arrQuerries) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function InsertCheckRecord(ByVal strCheckType As String, ByVal strTkno As String _
                                    , ByVal dteDOI As Date, ByVal dteDOF As Date _
                                    , ByVal dte1stDateInRAs As Date, ByVal strCMC As String _
                                    , Optional ByVal IntTravelId As Integer = 0 _
                                    , Optional ByVal strReason As String = "") As Boolean
        Dim arrQuerries(0 To 0) As String
        arrQuerries(0) = "insert into GO_Checks (CheckType,TKNO,DOI,DOF,FirstDateInRAS" _
                        & ",Cmc,TravelId,Reason) values ('" & strCheckType _
                        & "','" & strTkno & "','" & DateTime2Text(dteDOI) _
                        & "','" & DateTime2Text(dteDOF) & "','" & DateTime2Text(dte1stDateInRAs) _
                        & "','" & strCMC & "'," & IntTravelId & ",'" & strReason & "')"
        If Update(arrQuerries) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function Get1stDateInRas(ByVal strTkno As String) As Date
        Return GetScalarAsString("Select FstUpdate from ras12.dbo.tkt where" _
                                & " tkno='" & strTkno & "' order by recid")
    End Function
    Public Function GetPaidFare4Nike2GoAir(ByVal strTkno As String, strSrv As String) As Decimal
        Dim decPaidFare As Decimal
        Dim drResult As SqlClient.SqlDataReader

        mcmdSql.CommandText = "Select top 1 Fare,Tax, Charge,Charge_D from ras12.dbo.tkt where status<>'XX' and Tkno='" _
                                & strTkno & "' and Srv='" & strSrv & "'"
        drResult = mcmdSql.ExecuteReader

        If drResult.HasRows Then
            Do While drResult.Read
                Dim arrBreak() As String = Split(drResult("Charge_D"), "|TVR-VAT:")
                Dim decTvrVat As Decimal
                If arrBreak.Length > 1 Then
                    decTvrVat = Mid(arrBreak(1).Split("|")(0), 4)
                End If

                Select Case strSrv
                    Case "S"
                        decPaidFare = drResult("Fare") + drResult("Charge") + drResult("Tax") + decTvrVat
                    Case "R"
                        decPaidFare = drResult("Fare") - drResult("Charge") + drResult("Tax") + decTvrVat
                End Select

                Exit Do
            Loop
        End If

        drResult.Close()
        Return decPaidFare
    End Function
    Public Function GetPaidFare2GoAir(ByVal strTkno As String, strSrv As String) As Decimal
        Dim decPaidFare As Decimal
        Dim drResult As SqlClient.SqlDataReader

        mcmdSql.CommandText = "Select top 1 Fare,Tax,Tax_D, Charge,Charge_D from ras12.dbo.tkt where status<>'XX' and Tkno='" _
                                & strTkno & "' and Srv='" & strSrv & "'"
        drResult = mcmdSql.ExecuteReader

        If drResult.HasRows Then
            Do While drResult.Read
                Dim arrBreak() As String = Split(drResult("Charge_D"), "|TVR-VAT:")
                Dim decTvrVat As Decimal
                If arrBreak.Length > 1 Then
                    decTvrVat = Mid(arrBreak(1).Split("|")(0), 4)
                End If
                Dim decVAT As Decimal = GetTaxAmtFromTaxDetails("UE", drResult("Tax_D"))

                Select Case strSrv
                    Case "S"
                        decPaidFare = drResult("Fare") + drResult("Charge") _
                            + drResult("Tax") - decTvrVat - decVAT
                    Case "R"
                        decPaidFare = drResult("Fare") - drResult("Charge") + drResult("Tax") - decTvrVat
                End Select

                Exit Do
            Loop
        End If

        drResult.Close()
        Return decPaidFare
    End Function

    Public Function IsFltStatsRequired(ByVal intCustId As Integer) As Boolean
        Dim intResult As Integer
        intResult = GetScalarAsDecimal("Select FltStats from Go_CompanyInfo1" _
                    & " where Status='OK' and CustId=" & intCustId)
        If intResult = 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetComm4Ins(strProvider As String) As Decimal
        Return GetScalarAsDecimal("Select TOP 1 Value from Go_Miscwzdate where status='OK'" _
                                & " and CATERGORY='COMM4INS' and Getdate() between TktDateFrom" _
                                & " and TktDateTo and Details='" & strProvider & "'")
    End Function
    Public Function GetDiscount4Ins(strProvider As String, strCustShortName As String) As Decimal
        Return GetScalarAsDecimal("Select TOP 1 Value from Go_Miscwzdate where status='OK'" _
                                & " and CATERGORY='InsDiscount' and Getdate() between TktDateFrom" _
                                & " and TktDateTo and Details='" & strProvider _
                                & "' and Value1='" & strCustShortName & "'")
    End Function
    Public Function IsSegChanged(ByVal lngRlocId As Long _
                                , ByVal strOri As String, ByVal strDes As String _
                                , ByVal strDepDate As String, ByVal strDepTime As String _
                                , ByVal strCar As String, ByVal strFltNbr As String _
                                , ByVal strStatus As String) As Boolean
        Dim strQuerry As String = "Select RecId from CWT_Segment where RLID=" & lngRlocId _
                                & " and Ori='" & strOri & "' and Dest='" & strDes _
                                & "' and FltDate='" & strDepDate & "' and FltTime='" & strDepTime _
                                & "' and AL='" & strCar & "' and FltNo='" & strFltNbr _
                                & "' and FltStatus='" & strStatus & "'"

        If GetScalarAsDecimal(strQuerry) = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function CountSegments(ByVal lngRlocId As Long) As Boolean
        Dim strQuerry As String = "Select count (RecId) from CWT_Segment where RLID=" & lngRlocId

        If GetScalarAsDecimal(strQuerry) = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function InsertCWT_RlocList(ByVal lngRlocId As Long, ByVal strSegNbr As String _
                    , ByVal strOri As String, ByVal strDes As String _
                    , ByVal strCar As String, ByVal strFltNbr As String _
                    , ByVal strFltDate As String, ByVal strFltTime As String _
                    , ByVal strStatus As String) As Boolean
        Dim arrUpdate(0 To 0) As String
        arrUpdate(0) = "Insert into CWT_RlocList (RLID,SegNo,Ori,Dest,AL,FltNo,FltDate" _
                    & ",FltTime,FltStatus) values (" & lngRlocId & "','" & strSegNbr _
                    & "','" & strOri & "','" & strDes & "','" & strCar & "','" & strFltNbr _
                    & "','" & strFltDate & "','" & strFltTime & "','" & strStatus & "') "
        If Update(arrUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetDataRequirement(ByVal intCustId As Integer _
                                        , ByVal strMandatoryType As String _
                                        , lstApplyTo As List(Of String) _
                                        , Optional ByVal blnPosOnly As Boolean = False) As Collection
        Dim colResult As New Collection
        Dim strQuerry As String
        Dim drResult As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        Dim strApplyTo As String = " and ApplyTo in ('ALL'"

        If lstApplyTo.Contains("HTL") Then
            lstApplyTo.Add("N-A")
        End If

        For Each strTemp As String In lstApplyTo
            strApplyTo = strApplyTo & ",'" & strTemp & "'"
        Next

        strApplyTo = strApplyTo & ")"

        cmdSql.Connection = mcnxConnection

        strQuerry = "Select * from GO_RequiredData" _
                        & " where Status='OK' and CustId =" & intCustId & strApplyTo

        If strMandatoryType <> "" Then
            strQuerry = strQuerry & " and Mandatory='" & strMandatoryType & "'"
        End If

        If blnPosOnly Then
            strQuerry = strQuerry & " and CollectionMethod='AGTINPUT'"
        End If

        cmdSql.CommandText = strQuerry
        drResult = cmdSql.ExecuteReader
        Do While drResult.Read
            Dim objRqData As New clsRequiredData
            With objRqData
                .DataCode = drResult("DataCode")
                .NameByCustomer = drResult("NameByCustomer")
                .MinLength = drResult("MinLength")
                .MaxLength = drResult("MaxLength")
                .Mandatory = drResult("Mandatory")
                .ConditionOfUse = drResult("ConditionOfUse")
                .CollectionMethod = drResult("CollectionMethod")
                .DefaultValue = drResult("DefaultValue")
                .CheckValues = drResult("CheckValues")
                .AllowSpecialValues = drResult("AllowSpecialValues")
                .CharType = drResult("CharType")
                .ApplyTo = drResult("ApplyTo")
                colResult.Add(objRqData, objRqData.DataCode)
            End With
        Loop
        drResult.Close()
        Return colResult

    End Function
    Public Function GetTravelerDataRequirement(ByVal intCustId As Integer _
                                        , ByVal strMandatoryType As String) As Collection
        Dim colResult As New Collection
        Dim strQuerry As String
        Dim drResult As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "Select * from GO_TravelerDataDefinitions" _
                        & " where Status='OK' and CustId =" & intCustId
        If strMandatoryType <> "" Then
            strQuerry = strQuerry & " and Mandatory='" & strMandatoryType & "'"
        End If

        cmdSql.CommandText = strQuerry
        drResult = cmdSql.ExecuteReader
        Do While drResult.Read
            Dim objRqData As New clsRequiredData
            With objRqData
                .DataCode = drResult("DataCode")
                .NameByCustomer = drResult("NameByCustomer")
                .MinLength = drResult("MinLength")
                .MaxLength = drResult("MaxLength")
                .Mandatory = drResult("Mandatory")
                .ConditionOfUse = drResult("ConditionOfUse")
                .DefaultValue = drResult("DefaultValue")
                .CheckValues = drResult("CheckValues")
                .AllowSpecialValues = drResult("AllowSpecialValues")
                .CharType = drResult("CharType")
                colResult.Add(objRqData, objRqData.DataCode)
            End With
        Loop
        drResult.Close()
        Return colResult

    End Function
    Public Function IsSpecialValue(ByVal intCustId As Integer, ByVal strDataCode As String _
                                    , ByVal strValue As String) As Boolean
        Dim intResult As Integer
        intResult = GetScalarAsDecimal("Select RecId from GO_RequiredDataValues where DataType='SPECIAL'" _
                    & " and Value='" & strValue & "' and CustId=" & intCustId)
        If intResult > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsNormalValue(ByVal intCustId As Integer, ByVal strDataCode As String _
                                    , ByVal strValue As String) As Boolean
        Dim intResult As Integer
        intResult = GetScalarAsDecimal("Select RecId from GO_RequiredDataValues where DataType='NORMAL'" _
                    & " and Value='" & strValue & "' and CustId=" & intCustId)
        If intResult > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsTravelerSpecialValue(ByVal intCustId As Integer, ByVal strDataCode As String _
                                    , ByVal strValue As String) As Boolean
        Dim intResult As Integer
        intResult = GetScalarAsDecimal("Select RecId from GO_TravelerDataValues where DataType='SPECIAL'" _
                    & " and Value='" & strValue & "'")
        If intResult > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsTravelerNormalValue(ByVal intCustId As Integer, ByVal strDataCode As String _
                                    , ByVal strValue As String) As Boolean
        Dim intResult As Integer
        intResult = GetScalarAsDecimal("Select RecId from GO_TravelerDataValues where DataType='NORMAL'" _
                    & " and Value='" & strValue & "'")
        If intResult > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function CheckTravelerData(ByVal colRequiredData As Collection _
                                , ByVal colAvailableData As Collection _
                                , ByVal intCustId As Integer, dteDOI As Date) As Collection
        Dim colResult As New Collection
        Dim i As Integer

        For i = 1 To colRequiredData.Count
            Dim objRequiredData As clsRequiredData = colRequiredData(i)

            With objRequiredData
                .ClearOldCheck()
                Dim objAvaiData As New clsAvailableData

                If Not colAvailableData.Contains(.DataCode) Then
                    If objRequiredData.DefaultValue = "" Then
                        objRequiredData.ErrMsg = "MISSING"
                    Else
                        AddRequiredData(colAvailableData, .DataCode, .DefaultValue)
                    End If
                Else
                    objAvaiData = colAvailableData(.DataCode)
                    If pobjTvcs.IsTravelerSpecialValue(intCustId, .DataCode, objAvaiData.DataValue) Then
                        'pass object nay
                    ElseIf pobjTvcs.IsTravelerNormalValue(intCustId, .DataCode, objAvaiData.DataValue) Then
                        'pass object nay
                    ElseIf .CheckValues Then
                        objRequiredData.ErrMsg = "INVALID VALUE"

                    ElseIf objAvaiData.DataValue.Length < .MinLength Then
                        objRequiredData.ErrMsg = "INVALID MIN LENGTH"
                    ElseIf objAvaiData.DataValue.Length > .MaxLength Then
                        objRequiredData.ErrMsg = "INVALID MAX LENGTH"
                    ElseIf .CharType = "NUMERIC" AndAlso Not IsNumeric(objAvaiData.DataValue) Then
                        objRequiredData.ErrMsg = "VALUE IS NOT NUMERIC"
                    ElseIf .CharType = "ALPHA" AndAlso Not AlphaOnly(objAvaiData.DataValue) Then
                        objRequiredData.ErrMsg = "VALUE IS NOT ALPHA ONLY"
                    End If
                End If
                If .ErrMsg <> "" Then
                    .AvailableValue = objAvaiData.DataValue
                    colResult.Add(objRequiredData)
                End If

            End With
        Next
        Return colResult
    End Function
    Public Function CheckData(ByVal colRequiredData As Collection _
                                , ByVal colAvailableData As Collection _
                                , ByVal intCustId As Integer, dteDOI As Date) As Collection
        Dim colResult As New Collection
        Dim i As Integer

        For i = 1 To colRequiredData.Count
            Dim objRequiredData As clsRequiredData = colRequiredData(i)

            With objRequiredData
                .ClearOldCheck()
                Dim objAvaiData As New clsAvailableData

                If Not colAvailableData.Contains(.DataCode) Then
                    If objRequiredData.DefaultValue = "" Then
                        objRequiredData.ErrMsg = "MISSING"
                    Else
                        AddRequiredData(colAvailableData, .DataCode, .DefaultValue)
                    End If
                Else
                    objAvaiData = colAvailableData(.DataCode)
                    If pobjTvcs.IsSpecialValue(intCustId, .DataCode, objAvaiData.DataValue) Then
                        'pass object nay
                    ElseIf pobjTvcs.IsNormalValue(intCustId, .DataCode, objAvaiData.DataValue) Then
                        'pass object nay
                    ElseIf .CheckValues Then
                        objRequiredData.ErrMsg = "INVALID VALUE"

                    ElseIf objAvaiData.DataValue.Length < .MinLength Then
                        objRequiredData.ErrMsg = "INVALID MIN LENGTH"
                    ElseIf objAvaiData.DataValue.Length > .MaxLength Then
                        objRequiredData.ErrMsg = "INVALID MAX LENGTH"
                    ElseIf .CharType = "NUMERIC" AndAlso Not IsNumeric(objAvaiData.DataValue) Then
                        objRequiredData.ErrMsg = "VALUE IS NOT NUMERIC"
                    ElseIf .CharType = "ALPHA" AndAlso Not AlphaOnly(objAvaiData.DataValue) Then
                        objRequiredData.ErrMsg = "VALUE IS NOT ALPHA ONLY"
                    End If
                End If
                If .ErrMsg <> "" Then
                    .AvailableValue = objAvaiData.DataValue
                    colResult.Add(objRequiredData)
                End If

            End With

        Next
        Return colResult
    End Function
    Public Function GetLastSosPnr(ByVal strGds As String _
                                , ByVal strRloc As String) As clsiSOS
        Dim objSos As New clsiSOS
        Dim strQuerry As String
        Dim drResult As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = mcnxConnection

        strQuerry = "Select top 1 * from GO_PNR2iSOS where Status='OK'" _
                    & " and GDS='" & strGds & "' and Rloc='" & strRloc _
                    & "' order by recid desc"

        cmdSql.CommandText = strQuerry
        drResult = cmdSql.ExecuteReader
        Do While drResult.Read
            With objSos
                .RecId = drResult("RecId")
                .Gds = drResult("Gds")
                .Rloc = drResult("Rloc")
                .PaxNames = drResult("PaxNames")
            End With
        Loop
        drResult.Close()
        Return objSos

    End Function
    Public Function GetTkt1AByTknoSRV(ByVal strTkno As String, ByVal strSrv As String) _
                    As clsTKT_1A
        Dim objTkt1a As New clsTKT_1A
        Dim strQuerry As String
        Dim drResult As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        Connect2()
        cmdSql.Connection = mcnxConnection2

        strQuerry = "Select top 1 * from Ras12.dbo.tkt where Status='OK'" _
                    & " and SRV='" & strSrv & "' and Tkno='" & strTkno _
                    & "' order by recid desc"

        cmdSql.CommandText = strQuerry
        drResult = cmdSql.ExecuteReader
        Do While drResult.Read
            With objTkt1a
                .FareBasis = drResult("FareBasis")
                .BkgClass = drResult("BkgClass")
            End With
        Loop
        drResult.Close()
        Disconnect2()
        Return objTkt1a
    End Function
    Public Function GetTkt1AByRemarkPaxName(ByVal strRloc As String, ByVal strPaxName As String) As clsTKT_1A
        Dim objTkt1a As New clsTKT_1A
        Dim strQuerry As String
        Dim drResult As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        Connect2()
        cmdSql.Connection = mcnxConnection2

        strQuerry = "Select top 1 * from tkt_1a where Status='OK'" _
                    & " and SRV='S' and Remark='" & strRloc & "' and PaxName='" & strPaxName & "'"

        cmdSql.CommandText = strQuerry
        drResult = cmdSql.ExecuteReader
        Do While drResult.Read
            With objTkt1a
                .CustId = drResult("CustId")
                .RecId = drResult("RecId")
                .PaxType = drResult("PaxType")
            End With
        Loop
        drResult.Close()
        Disconnect2()
        Return objTkt1a
    End Function
    Public Function GetCustIdInTkt1AByRemarkPaxName(ByVal strRloc As String, ByVal strPaxName As String) As Integer
        Dim strQuerry As String
        Dim intResult As Integer

        strQuerry = "Select top 1 CustId from tkt_1a where Status='OK'" _
                    & " and SRV='S' and Remark='" & strRloc & "' and PaxName='" & strPaxName & "'"
        intResult = GetScalarAsDecimal(strQuerry)
        Return intResult
    End Function
    Public Function DataErr2Msg(ByVal colDataErr As Collection) As String
        Dim strMsg As String = ""
        For Each objRqData As clsRequiredData In colDataErr
            With objRqData
                strMsg = strMsg & .DataCode & "/" & .NameByCustomer _
                        & ". ERROR:" & .ErrMsg & vbNewLine
            End With
        Next
        Return strMsg
    End Function
    Public Function GetRequiredDataValueDescription(ByVal intCustId As Integer, ByVal strDataCode As String _
                                                    , ByVal strValue As String) As String
        Return GetScalarAsString("Select top 1 Description from go_RequiredDataValues" _
                                 & " where status='OK' and CustId=" & intCustId & " and DataCode='" & strDataCode _
                                 & "' and Value='" & strValue & "'")

    End Function
    Public Function GetDefaultRS4LocalHtl(ByVal intRcpId As Integer, ByVal dteDoi As Date) As String
        Return GetScalarAsString("SELECT TOP 1 RS FROM GO_DEFAULT4LOCALHTL" _
                                & " WHERE CUSTID=(select CUSTID from ras12.dbo.rcp" _
                                & " WHERE RECID=" & intRcpId & ")" _
                                & " and '" & DateTime2Text(dteDoi) & "' between TktDateFrom and TktDateTo")
    End Function
    Public Function GetDefaultMS4LocalHtl(ByVal intRcpId As Integer, ByVal dteDoi As Date) As String
        Return GetScalarAsString("SELECT TOP 1 MS FROM GO_DEFAULT4LOCALHTL" _
                                & " WHERE CUSTID=(select CUSTID from ras12.dbo.rcp" _
                                & " WHERE RECID=" & intRcpId & ")" _
                                & " and '" & DateTime2Text(dteDoi) & "' between TktDateFrom and TktDateTo")
    End Function
    Public Function GetCcNbrByTktInRas(strTktNbr As String) As String
        Return GetScalarAsString("SELECT top 1 Document FROM Ras12.dbo.FOP" _
                                & " WHERE FOP='CRD'and RCPID=" _
                                & " (SELECT top 1 rcpid FROM RAS12.DBO.TKT WHERE TKNO='" & strTktNbr _
                                & "' AND STATUS='OK' AND SRV='S')")
    End Function
    Public Function GetDefaultRS4LocalHtl(intCustId As Integer) As String
        Return GetScalarAsString("SELECT top 1 RS FROM [GO_Default4LocalHtl]" _
                                & " WHERE Status='OK' and CustId=" & intCustId)

    End Function
    Public Function GetDefaultMS4LocalHtl(intCustId As Integer) As String
        Return GetScalarAsString("SELECT top 1 MS FROM [GO_Default4LocalHtl]" _
                                & " WHERE Status='OK' and CustId=" & intCustId)

    End Function
    Public Function GetTrxFee4NonAirHotel(intDutoanId As Integer, intRelatedItem As Integer) As Decimal
        Return GetScalarAsDecimal("Select top 1 TtlToPax/Qty from Ras12.dbo.Dutoan_item where Status<>'XX'" _
                                  & "and Service='TransViet SVC Fee' and DutoanId=" & intDutoanId _
                                  & "and RelatedItem=" & intRelatedItem)
    End Function
    Public Function GetRas12ByInsVoucher(strVoucherNbr As String) As clsTktRas12
        Dim dtbResult As System.Data.DataTable
        Dim objTktRas As New clsTktRas12
        strVoucherNbr = Replace(strVoucherNbr, " ", "")

        Dim strQuerry As String = "Select top 1 * from Ras12.dbo.Tkt where status='OK' and Dependent='" & strVoucherNbr _
                                & "' and substring(tkno,1,3)='INS'"

        dtbResult = GetDataTable(strQuerry)
        With dtbResult
            If dtbResult.Rows.Count > 0 Then
                objTktRas.Dependent = .Rows(0)("Dependent")
                objTktRas.DOI = .Rows(0)("Doi")
                objTktRas.NetToAL = .Rows(0)("NetToAL")
                objTktRas.RcpNo = .Rows(0)("RcpNo")
            End If
        End With
        Return objTktRas
    End Function

    Public Function GetRcpNbrByInsVoucher(strVoucherNbr As String) As String
        Dim strQuerry As String

        strVoucherNbr = Replace(strVoucherNbr, " ", "")
        strQuerry = "Select RCPNO from Ras12.dbo.Tkt where status='OK' and Dependent='" & strVoucherNbr _
                    & "' and substring(tkno,1,3)='INS'"
        Return pobjTvcs.GetScalarAsString(strQuerry)

    End Function
    Public Function GetDoiByRcNbr(strRcpNbr As String) As String
        Dim strQuerry As String

        strQuerry = "Select convert(varchar,DOI,106) from Ras12.dbo.Tkt where status='OK' and RcpNo='" & strRcpNbr & "'"

        Return pobjTvcs.GetScalarAsString(strQuerry)
    End Function
    Public Function GetDoiByTknoSrv(strTkno As String, strSrv As String) As String
        Dim strQuerry As String
        'Select Case strSrv
        '    Case "S"
        '        strQuerry = "Select convert(varchar,DOI,106) from Ras12.dbo.Tkt where status='OK' and Tkno='" & strTkno _
        '        & "' and Srv in ('S','A')"
        '    Case Else
        strQuerry = "Select convert(varchar,DOI,106) from Ras12.dbo.Tkt where status='OK' and Tkno='" & strTkno _
            & "' and Srv='" & strSrv & "'"
        'End Select

        Return pobjTvcs.GetScalarAsString(strQuerry)
    End Function
    Public Function GetBypassId(intTravelId As Integer, strBypassType As String) As String
        Dim strQuerry As String = "Select RecId from [GO_ByPass] where status='OK' and TrvlId=" & intTravelId _
                                  & " and BypassType='" & strBypassType & "'"
        Dim intRecId As Integer

        intRecId = pobjTvcs.GetScalarAsDecimal(strQuerry)
        Return intRecId
    End Function
    Public Function UpdateCDRs(tblCDRs As System.Data.DataTable, intTrvlId As Integer) As Boolean
        Dim arrUpdates(0 To tblCDRs.Rows.Count - 1) As String
        Dim i As Integer
        Dim strQuerry As String

        For i = 0 To tblCDRs.Rows.Count - 1
            arrUpdates(i) = "Ref" & tblCDRs.Rows(i)("CdrNbr") & "='" _
                & Mid(tblCDRs.Rows(i)("CdrValue"), 1, 25) & "'"
        Next

        strQuerry = "Update cwt.dbo.Go_Travel SET " & Join(arrUpdates, ",") & " where Recid=" & intTrvlId
        If ExecuteNonQuerry(strQuerry) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsSqCorpFareApplied(strTourCodeSuffix As String) As Boolean
        Dim strQuerry As String = "select recid from tvcs.dbo.[SqCorp] where Status='OK' and [TourCodeSuffix]='" & strTourCodeSuffix & "'"

        If GetScalarAsDecimal(strQuerry) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetRequiredDataNameByCust(intCustId As Integer, strDatacode As String) As String
        Dim strQuerry As String = "select NameByCustomer from cwt.dbo.[GO_RequiredData] where Status='OK' and CustId=" _
                                  & intCustId & " and DataCode='" & strDatacode & "'"

        Return GetScalarAsString(strQuerry)
    End Function
    Public Function GetCabinByRbd(strCar As String, strRbd As String) As String
        Dim strQuerry As String = "select F+'/'+C from cwt.dbo.rbd_Cabin where Status='OK' and AL='" _
                                  & strCar & "'"
        Dim strCabins As String = GetScalarAsString(strQuerry)
        If strCabins = "" Then
            Return "ECONOMY"
        Else
            Dim arrCabins() As String = Split(strCabins, "/")
            Dim i As Integer
            For i = 0 To arrCabins.Length - 1
                If arrCabins(i).Contains(strRbd) Then
                    If i = 0 Then
                        Return "FIRST"
                    Else
                        Return "BUSINESS"
                    End If
                End If
            Next
        End If
        Return "ECONOMY"
    End Function
    Public Function GetCos4Rtg(arrCar() As String, strRbds As String) As String
        Dim i As Integer
        Dim strCOS As String
        Dim strResult As String = ""
        Dim lstCars As New List(Of String)
        Dim lstRBDs As New List(Of String)

        For i = 0 To arrCar.Length - 1
            If strRbds.Chars(i) = " " Then
                Continue For
            ElseIf lstCars.Contains(arrCar(i)) AndAlso lstRBDs.Contains(strRbds.Chars(i)) Then
                Continue For
            Else
                lstCars.Add(arrCar(i))
                lstRBDs.Add(strRbds.Chars(i))
            End If
        Next

        For i = 0 To lstCars.Count - 1
            strCOS = GetCabinByRbd(lstCars(i), lstRBDs(i))
            If strResult = "" Then
                strResult = strCOS
            ElseIf strResult <> strCOS Then
                strResult = "MIXED"
                Return strResult
            End If
        Next
        Return strResult
    End Function
    Public Function GetAlliance(strCar As String) As String
        Return GetScalarAsString("Select top 1 Details from GO_MISC where CAT='Alliance' and VAL='" & strCar & "'")
    End Function
    Public Function GetBookerLocation(strBooker As String, intCustId As Integer) As String
        Return GetScalarAsString("Select Location from Cwt_Bookers where Status='OK' and CustId=" _
                                & intCustId & " and BookerName='" & strBooker & "'")
    End Function
    Public Function RealisedSavingCodeAirExists(strRsCode As String, strCmc As String) As Boolean
        If GetScalarAsString("Select RecId from GO_MISC where CAT='RSavingAir' and RMK='" & strCmc _
                             & "' and VAL='" & strRsCode & "'") <> "" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function MissedSavingCodeAirExists(strMsCode As String, strCmc As String) As Boolean
        If GetScalarAsString("Select RecId from GO_MISC where CAT='MSavingAir' and RMK='" & strCmc _
                             & "' and VAL='" & strMsCode & "'") <> "" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function NoCheckTktNbr(strTkno As String) As Boolean
        If GetScalarAsString("Select RecId from GO_MISC where CAT='NoCheckTktNbr' and VAL='" & strTkno & "'") <> "" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetColumnValuesAsString(strTblName As String, strColumn As String, strCondition As String _
                                       , strSeperator As String) As String
        Dim strResult As String
        Dim strQuerry As String
        strQuerry = "SELECT cast (" & strColumn & " as varchar) +'" & strSeperator & "' from " _
            & strTblName & " " & strCondition & " for xml path('')"
        strResult = GetScalarAsString(strQuerry)
        If strResult <> "" Then
            strResult = Mid(strResult, 1, strResult.Length - strSeperator.Length)
        End If
        Return strResult
    End Function
    Public Function AllowCaptureMultiPax(strRasSignIn As String, strCustShortName As String)
        Dim strQuerry As String = "Select top 1 RecId from Misc where Cat='AllowCaptureMultiPax'" _
                                  & " and Status='OK' and Val='" & strCustShortName _
                                  & "' and Val1='" & strRasSignIn & "'"
        If GetScalarAsDecimal(strQuerry) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function DefineOWRT(ByVal pRTG As String, ByVal pDomINT As String) As String

        Dim OWRT As String = "??"
        If pRTG.Length < 10 Then Return "??"
        If pRTG.Length = 10 Then Return "OW"
        If pDomINT = "DOM" Then
            If pRTG.Substring(0, 3) <> Strings.Right(pRTG, 3) Then
                Return "OW"
            Else
                Return "RT"
            End If
        Else
            GetCountryCode(Strings.Right(pRTG, 3))
            If GetCountryCode(Strings.Left(pRTG, 3)) <> GetCountryCode(Strings.Right(pRTG, 3)) Then
                Return "OW"
            Else
                Return "RT"
            End If
        End If
        Return OWRT
    End Function
    Public Function GetCwtRtgType4Country(intCustId As String, dteDOI As Date _
            , strCountry As String) As String

        Dim strQuerry As String
        Dim strRtgType As String = "INTL"

        If strCountry = "VN" Then
            strRtgType = "DOM"
        Else
            strQuerry = "Select top 1 RtgType from CWT_SF where Status='OK' and SvcType='AIR' and CustId =" _
            & intCustId & " and Countries like '%" & strCountry & "%' and '" _
            & Format(dteDOI, "dd MMM yy") & "' between ValidFrom and ValidThru" _
            & " order by RtgType Desc"

            strRtgType = GetScalarAsString(strQuerry)
            If strRtgType = "" Then
                strRtgType = "INTL"
            End If
        End If

        Return strRtgType
    End Function
    Public Function TvSessionActive(intRecId As Integer)
        Dim strResult As String = GetScalarAsString("Select Active from DCP_Sessions where RecId=" & intRecId)
        If strResult = "True" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function CreateTvSession(intTvsi As Integer, strPcc As String, strSecurityKey As String) As Integer
        Dim lstQuerries As New List(Of String)
        lstQuerries.Add("insert into  DCP_Sessions (Tvsi, PCC, PcName,SecurityKey)" _
                & " values(" & intTvsi & ",'" & strPcc & "',N'" & My.Computer.Name _
                & "',N'" & strSecurityKey & "')")
        If UpdateListOfQuerries(lstQuerries, True) Then
            Return msglLastInsertedId
        Else
            Return 0
        End If
    End Function

    Public Function UpdateListOfQuerries(ByVal lstQuerries As List(Of String), Optional blnGetLastInsertedId As Boolean = False) As Boolean
        Dim trcSql As SqlClient.SqlTransaction
        Dim cmdSql As SqlClient.SqlCommand = mcnxConnection.CreateCommand
        Dim i As Integer
        If mcnxConnection.State <> ConnectionState.Open Then
            mcnxConnection.Open()
        End If

        trcSql = mcnxConnection.BeginTransaction
        cmdSql.Transaction = trcSql
        Try
            For i = 0 To lstQuerries.Count - 1
                cmdSql.CommandText = lstQuerries(i)
                cmdSql.ExecuteNonQuery()
                If blnGetLastInsertedId AndAlso UCase(Mid(lstQuerries(i), 1, 6)) = "INSERT" Then
                    cmdSql.CommandText = "select SCOPE_IDENTITY()"
                    msglLastInsertedId = cmdSql.ExecuteScalar
                End If
            Next
            trcSql.Commit()
            Return True
        Catch ex As Exception
            mstrUpdtErr = ex.Message
            LogSqlError(ex.Message & vbNewLine & lstQuerries(i), "Update list of Querries")
            trcSql.Rollback()
            Return False
        End Try

    End Function
    Public Function LogCommandResponse(strCommand As String, strResponse As String _
                                       , strPcc As String, intTvsi As Integer) As Boolean
        Dim strQuerry As String = String.Empty
        strQuerry = "insert into Dcp_InOut (Office, TVSI, Cmd, Output) values ('" _
                             & strPcc & "'," & intTvsi & ",'" & strCommand _
                             & "','" & strResponse & "')"
        Try
            ExecuteNonQuerry(strQuerry)
            Return True
        Catch ex As Exception
            LogSqlError(ex.Message & vbNewLine & strQuerry, "LogCommandResponse")
            Return False
        End Try

    End Function
    Public Property CnxErr() As String
        Get
            CnxErr = mstrCnxErr
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property UpdtErr() As String
        Get
            UpdtErr = mstrUpdtErr
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property Connection() As SqlClient.SqlConnection
        Get
            Connection = mcnxConnection
        End Get
        Set(ByVal value As SqlClient.SqlConnection)

        End Set
    End Property
    Public Property Connection2() As SqlClient.SqlConnection
        Get
            Connection2 = mcnxConnection2
        End Get
        Set(ByVal value As SqlClient.SqlConnection)

        End Set
    End Property
    Public Property ConnectionString() As String
        Get
            ConnectionString = mstrConnectionString
        End Get
        Set(ByVal value As String)
            mstrConnectionString = value
        End Set
    End Property
    Public Property LastInsertedId() As Single
        Get
            LastInsertedId = msglLastInsertedId
        End Get
        Set(ByVal value As Single)
            msglLastInsertedId = value
        End Set
    End Property
    Public Property Cmd As SqlClient.SqlCommand
        Get
            Return mcmdSql
        End Get
        Set(value As SqlClient.SqlCommand)
            mcmdSql = value
        End Set
    End Property
End Class
