Imports System.Data.SqlClient
Imports System.IO

Module mdlCTS
    Dim DKaddRAS As String
    Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
    Public NotInheritable Class NonAirServices
        Public Const Hotel As String = "Accommodations"
        Public Const Car As String = "Transfer"
        Public Const Visa As String = "Visa"
        Public Const Misc As String = "Miscellaneous"
        Public Const Ahc As String = "HotLine"
        Public Const TvSvcFee As String = "TransViet  SVC Fee"
        Public Const All As String = ""
    End Class

    Public Function SumRev4Tkt(strCmc As String, strRtgType As String _
                                  , dteFromDate As Date, dteToDate As Date _
                                  , strSRV As String, intROE As Integer, strAlType As String _
                                  , Optional lstDocType As List(Of String) = Nothing) As clsSum

        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable
        Dim strQuerry As String = "Select count (t.RecId) as Nbr,Sum(TVCharge_VND+F_VND+T_VND+C_VND-VAT_VND)/" & intROE _
                                & " from ras12.dbo.TKT t LEFT JOIN ras12.dbo.RCP r ON t.RCPID=r.RecID" _
                                & " left join ras12.dbo.ReportData r1 on t.RecID=r1.TKID" _
                                & " where t.Status<>'xx' and t.Qty<>0 and t.AL<>'01'" _
                                & " and substring(T.RMK,1,4)='BIZT'" _
                                & " and DOI between '" & CreateFromDate(dteFromDate.Date) & "' and '" & CreateToDate(dteToDate) _
                                & "' and r.Status<>'xx' and r.CustID in " _
                                & "(Select CustId from Go_CompanyInfo1 where Status='OK' and CMC='" _
                                & strCmc & "')"

        Select Case strRtgType.ToUpper
            Case "DOM", "INTL", "REG", "REG2"
                strQuerry = strQuerry & " and r1.Dekho='" & strRtgType & "'"
            Case "NOTDOM"
                strQuerry = strQuerry & " and r1.Dekho<>'" & strRtgType & "'"
            Case ""
                'bo qua
            Case Else
                MsgBox("Invalid RtgType:" & strRtgType)
                Return objSum
        End Select

        Select Case strSRV.ToUpper
            Case "S", "R"
                strQuerry = strQuerry & "  and t.Srv='" & strSRV & "'"
            Case ""
                'Bo qua
        End Select
        Select Case strAlType.ToUpper
            Case "LCC"
                strQuerry = strQuerry & "and ISNUMERIC(SUBSTRING(tkno,1,3))=0"
            Case "FSC"
                strQuerry = strQuerry & "and ISNUMERIC(SUBSTRING(tkno,1,3))=1"
            Case Else
        End Select

        If lstDocType IsNot Nothing Then
            Dim arrDocTypes(0 To lstDocType.Count - 1) As String
            lstDocType.CopyTo(arrDocTypes)
            strQuerry = strQuerry & "and t.DocType in ('" & Join(arrDocTypes, "','") & "')"

        End If
        tblResult = pobjTvcs.GetDataTable(strQuerry)
        objSum.Nbr = tblResult.Rows(0)(0)
        If Not IsDBNull(tblResult.Rows(0)(1)) Then
            objSum.Amt = tblResult.Rows(0)(1)
        End If

        Return objSum
    End Function
    Public Function SumRev4TktHpi(strRtgType As String _
                                  , dteFromDate As Date, dteToDate As Date _
                                  , strAlType As String _
                                  , Optional lstDocType As List(Of String) = Nothing) As clsSum
        'làm cho (HPI SGN + HPI CW)
        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable
        Dim strQuerry As String = "Select count (t.RecId) as Nbr,Sum(TVCharge_VND+F_VND+T_VND+C_VND-VAT_VND)" _
                                & " from ras12.dbo.TKT t LEFT JOIN ras12.dbo.RCP r ON t.RCPID=r.RecID" _
                                & " left join ras12.dbo.ReportData r1 on t.RecID=r1.TKID" _
                                & " where t.Status<>'xx' and t.Qty=1 and t.AL<>'01'" _
                                & " and Biz='True'" _
                                & " and DOI between '" & CreateFromDate(dteFromDate.Date) & "' and '" & CreateToDate(dteToDate) _
                                & "' and r.Status<>'xx' and r.CustID in (21428,21430)"

        Select Case strRtgType.ToUpper
            Case "DOM", "INTL", "REG", "REG2"
                strQuerry = strQuerry & " and r1.Dekho='" & strRtgType & "'"
            Case "NOTDOM"
                strQuerry = strQuerry & " and r1.Dekho<>'" & strRtgType & "'"
            Case ""
                'bo qua
            Case Else
                MsgBox("Invalid RtgType:" & strRtgType)
                Return objSum
        End Select

        Select Case strAlType.ToUpper
            Case "LCC"
                strQuerry = strQuerry & "and ISNUMERIC(SUBSTRING(tkno,1,3))=0"
            Case "FSC"
                strQuerry = strQuerry & "and ISNUMERIC(SUBSTRING(tkno,1,3))=1"
            Case Else
        End Select

        If lstDocType IsNot Nothing Then
            Dim arrDocTypes(0 To lstDocType.Count - 1) As String
            lstDocType.CopyTo(arrDocTypes)
            strQuerry = strQuerry & "and t.DocType in ('" & Join(arrDocTypes, "','") & "')"

        End If
        tblResult = pobjTvcs.GetDataTable(strQuerry)
        objSum.Nbr = tblResult.Rows(0)(0)
        If Not IsDBNull(tblResult.Rows(0)(1)) Then
            objSum.Amt = tblResult.Rows(0)(1)
        End If

        Return objSum
    End Function
    Public Function SumSvcFeeNoVat4Tkt(strCmc As String, strRtgType As String _
                                  , dteFromDate As Date, dteToDate As Date _
                                  , strSRV As String, intROE As Integer, strAlType As String) As clsSum


        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable
        Dim strQuerry As String = "Select count (t.RecId) as Nbr,Sum(SfNoVat_VND)/" & intROE _
                                & " from ras12.dbo.TKT t LEFT JOIN ras12.dbo.RCP r ON t.RCPID=r.RecID" _
                                & " left join ras12.dbo.ReportData r1 on t.RecID=r1.TKID" _
                                & " where t.Status<>'xx' and t.Qty<>0 and t.AL<>'01'" _
                                & " and substring(T.RMK,1,4)='BIZT'" _
                                & " and DOI between '" & CreateFromDate(dteFromDate.Date) _
                                & "' and '" & CreateToDate(dteToDate) _
                                & "' and r.Status<>'xx' and r.CustID in " _
                                & "(Select CustId from cwt.dbo.Go_CompanyInfo1 where Status='OK' and CMC='" _
                                & strCmc & "')"

        Select Case strRtgType.ToUpper
            Case "DOM", "INTL", "REG", "REG2"
                strQuerry = strQuerry & " and r1.Dekho='" & strRtgType & "'"
            Case "NOTDOM"
                strQuerry = strQuerry & " and r1.Dekho<>'" & strRtgType & "'"
            Case ""
                'bo qua
            Case Else
                MsgBox("Invalid RtgType:" & strRtgType)
                Return objSum
        End Select

        Select Case strSRV.ToUpper
            Case "S", "R"
                strQuerry = strQuerry & "  and t.Srv='" & strSRV & "'"
            Case ""
                'Bo qua
        End Select
        Select Case strAlType.ToUpper
            Case "LCC"
                strQuerry = strQuerry & "and ISNUMERIC(SUBSTRING(tkno,1,3))=0"
            Case "FSC"
                strQuerry = strQuerry & "and ISNUMERIC(SUBSTRING(tkno,1,3))=1"
            Case Else
        End Select

        tblResult = GetDataTable(strQuerry)
        objSum.Nbr = tblResult.Rows(0)(0)
        If Not IsDBNull(tblResult.Rows(0)(1)) Then
            objSum.Amt = tblResult.Rows(0)(1)
        End If

        Return objSum
    End Function
    Public Function SumSvcFee4Tkt(strCmc As String, strRtgType As String _
                                  , dteFromDate As Date, dteToDate As Date _
                                  , strSRV As String, intROE As Integer, strAlType As String) As clsSum


        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable
        Dim strQuerry As String = "Select count (t.RecId) as Nbr,Sum(TVCharge_VND)/" & intROE _
                                & " from ras12.dbo.TKT t LEFT JOIN ras12.dbo.RCP r ON t.RCPID=r.RecID" _
                                & " left join ras12.dbo.ReportData r1 on t.RecID=r1.TKID" _
                                & " where t.Status<>'xx' and t.Qty<>0 and t.AL<>'01'" _
                                & " and substring(T.RMK,1,4)='BIZT'" _
                                & " and DOI between '" & CreateFromDate(dteFromDate.Date) _
                                & "' and '" & CreateToDate(dteToDate) _
                                & "' and r.Status<>'xx' and r.CustID in " _
                                & "(Select CustId from Go_CompanyInfo1 where Status='OK' and CMC='" _
                                & strCmc & "')"

        Select Case strRtgType.ToUpper
            Case "DOM", "INTL", "REG", "REG2"
                strQuerry = strQuerry & " and r1.Dekho='" & strRtgType & "'"
            Case "NOTDOM"
                strQuerry = strQuerry & " and r1.Dekho<>'" & strRtgType & "'"
            Case ""
                'bo qua
            Case Else
                MsgBox("Invalid RtgType:" & strRtgType)
                Return objSum
        End Select

        Select Case strSRV.ToUpper
            Case "S", "R"
                strQuerry = strQuerry & "  and t.Srv='" & strSRV & "'"
            Case ""
                'Bo qua
        End Select
        Select Case strAlType.ToUpper
            Case "LCC"
                strQuerry = strQuerry & "and ISNUMERIC(SUBSTRING(tkno,1,3))=0"
            Case "FSC"
                strQuerry = strQuerry & "and ISNUMERIC(SUBSTRING(tkno,1,3))=1"
            Case Else
        End Select

        tblResult = pobjTvcs.GetDataTable(strQuerry)
        objSum.Nbr = tblResult.Rows(0)(0)
        If Not IsDBNull(tblResult.Rows(0)(1)) Then
            objSum.Amt = tblResult.Rows(0)(1)
        End If

        Return objSum
    End Function
    Public Function SumSvcFee4Ahc(strCmc As String, dteFromDate As Date, dteToDate As Date _
                                  , intROE As Integer) As clsSum
        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable

        Dim strQuerry As String = "Select t.*" _
                                & " from ras12.dbo.TKT t LEFT JOIN ras12.dbo.RCP r ON t.RCPID=r.RecID" _
                                & " where t.Status<>'xx' and t.Qty<>0 and t.DocType='AHC'" _
                                & " and substring(T.RMK,1,4)='BIZT'" _
                                & " and DOI between '" & CreateFromDate(dteFromDate.Date) _
                                & "' and '" & CreateToDate(dteToDate) _
                                & "' and r.Status<>'xx' and r.CustID in " _
                                & "(Select CustId from Go_CompanyInfo1 where Status='OK' and CMC='" _
                                & strCmc & "')"

        tblResult = pobjTvcs.GetDataTable(strQuerry)

        For Each objRow As DataRow In tblResult.Rows
            Dim decSvcFee As Decimal
            objSum.Nbr = objSum.Nbr + 1
            Decimal.TryParse(Mid(GetChargeFromChargeDetails("TVN-OTHER:", objRow("Charge_D")), 4), decSvcFee)
            objSum.Amt = objSum.Amt + decSvcFee
        Next
        objSum.Amt = objSum.Amt / intROE
        Return objSum
    End Function
    Public Function SumSvcFee4AhcAirHpi(dteFromDate As Date, dteToDate As Date) As clsSum
        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable

        Dim strQuerry As String = "Select t.*" _
                                & " from ras12.dbo.TKT t LEFT JOIN ras12.dbo.RCP r ON t.RCPID=r.RecID" _
                                & " where t.Status<>'xx' and t.Qty=1 and t.DocType='AHC'" _
                                & " and Biz='True'" _
                                & " and DOI between '" & CreateFromDate(dteFromDate.Date) _
                                & "' and '" & CreateToDate(dteToDate) _
                                & "' and r.Status<>'xx' and r.CustID in (21428,21430)"

        tblResult = pobjTvcs.GetDataTable(strQuerry)

        For Each objRow As DataRow In tblResult.Rows
            Dim decSvcFee As Decimal
            objSum.Nbr = objSum.Nbr + 1
            Decimal.TryParse(Mid(GetChargeFromChargeDetails("TVN-OTHER:", objRow("Charge_D")), 4), decSvcFee)
            objSum.Amt = objSum.Amt + decSvcFee
        Next
        Return objSum
    End Function
    Public Function SumSvcFee4NonAir(strCmc As String, dteFromDate As Date, dteToDate As Date _
                                  , intROE As Integer) As clsSum
        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable
        Dim strFilterTour As String
        Dim strQuerry As String

        strFilterTour = " and DuToanID in" _
                        & "(select RecID from RAS12.dbo.DuToan_Tour t" _
                        & " where t.Status='rr' and t.Contact<>'ZPERSONAL'" _
                        & " and t.LstUpdate between '" & CreateFromDate(dteFromDate.Date) _
                        & "' and '" & CreateToDate(dteToDate) _
                        & "' and t.CustID in" _
                        & "(Select CustId from cwt.dbo.Go_CompanyInfo1 where Status='OK' and CMC='" _
                        & strCmc & "'))"

        strQuerry = "select Count(i.RecId), SUM(i.TTLToPax)/" & intROE _
                    & " from RAS12.dbo.DuToan_Item i" _
                    & " where i.Status='ok' and i.Service= 'TransViet SVC Fee'" _
                    & strFilterTour

        tblResult = GetDataTable(strQuerry)
        objSum.Nbr = tblResult.Rows(0)(0)
        If objSum.Nbr <> 0 Then
            objSum.Amt = tblResult.Rows(0)(1)
        End If

        Return objSum
    End Function
    Public Function SumRevNoSfNonAir(strCmc As String, dteFromDate As Date, dteToDate As Date _
                                  , intROE As Integer) As clsSum
        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable
        Dim strFilterTour As String
        Dim strQuerry As String

        strFilterTour = " and DuToanID in" _
                        & "(select RecID from RAS12.dbo.DuToan_Tour t" _
                        & " where t.Status='rr' and t.Contact<>'ZPERSONAL'" _
                        & " and t.LstUpdate between '" & CreateFromDate(dteFromDate.Date) _
                        & "' and '" & CreateToDate(dteToDate) _
                        & "' and t.CustID in" _
                        & "(Select CustId from cwt.dbo.Go_CompanyInfo1 where Status='OK' and CMC='" _
                        & strCmc & "'))"

        strQuerry = "select Count(i.RecId), SUM(i.TTLToPax)/" & intROE _
                    & " from RAS12.dbo.DuToan_Item i" _
                    & " where i.Status='ok' and i.Service not in" _
                    & "('TransViet SVC Fee','Miscellaneous')" _
                    & strFilterTour

        tblResult = GetDataTable(strQuerry)
        objSum.Nbr = tblResult.Rows(0)(0)
        If objSum.Nbr <> 0 Then
            objSum.Amt = tblResult.Rows(0)(1)
        End If

        Return objSum
    End Function
    Public Function SumRevNoSfNonAirHpi(dteFromDate As Date, dteToDate As Date _
                                        , Optional strServiceName As String = "") As clsSum
        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable
        Dim strFilterTour As String
        Dim strQuerry As String

        strFilterTour = " and DuToanID in" _
                        & "(select RecID from RAS12.dbo.DuToan_Tour t" _
                        & " where t.Status='rr' and t.Contact<>'ZPERSONAL'" _
                        & " and t.LstUpdate between '" & CreateFromDate(dteFromDate.Date) _
                        & "' and '" & CreateToDate(dteToDate) _
                        & "' and t.CustID in (21428,21430)"

        strQuerry = "select Count(i.RecId), SUM(i.TTLToPax)" _
                    & " from RAS12.dbo.DuToan_Item i" _
                    & " where i.Status='ok'" _
                    & strFilterTour
        If strServiceName <> "" Then
            strQuerry = strQuerry & " and i.Service='" & strServiceName & "')"
        End If
        tblResult = GetDataTable(strQuerry)
        objSum.Nbr = tblResult.Rows(0)(0)
        If objSum.Nbr <> 0 Then
            objSum.Amt = tblResult.Rows(0)(1)
        End If

        Return objSum
    End Function
    Public Function SumRevNoSfNonAirService(strCmc As String, dteFromDate As Date, dteToDate As Date _
                                  , intROE As Integer, strService As String) As clsSum
        'output: Tong thu Non Air da tru di VAT
        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable
        Dim strFilterTour As String
        Dim strQuerry As String

        strFilterTour = " and DuToanID in" _
                        & "(select RecID from RAS12.dbo.DuToan_Tour t" _
                        & " where t.Status='rr' and t.Contact<>'ZPERSONAL'" _
                        & " and t.LstUpdate between '" & CreateFromDate(dteFromDate.Date) _
                        & "' and '" & CreateToDate(dteToDate) _
                        & "' and t.CustID in" _
                        & "(Select CustId from Go_CompanyInfo1 where Status='OK' and CMC='" _
                        & strCmc & "'))"

        strQuerry = "select Count(i.RecId), SUM(i.TtlToPax /(1 + (CAST(i.VAT AS NUMERIC(4,2))/100)))/" & intROE _
                    & " from RAS12.dbo.DuToan_Item i" _
                    & " where i.Status='ok' and i.Service ='" & strService & "'" _
                    & strFilterTour

        tblResult = pobjTvcs.GetDataTable(strQuerry)
        objSum.Nbr = tblResult.Rows(0)(0)
        If Not IsDBNull(tblResult.Rows(0)(1)) Then
            objSum.Amt = tblResult.Rows(0)(1)
        End If

        Return objSum
    End Function
    Public Function SumSvcFee4NonAirSvc(strCmc As String, dteFromDate As Date, dteToDate As Date _
                                  , intROE As Integer, strNonAirServices As String) As clsSum
        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable
        Dim strFilterTour As String
        Dim strQuerry As String
        Dim strSumSfRelated As String
        Dim strSumSfNonRelated As String

        strFilterTour = " and DuToanID in" _
                        & "(select RecID from RAS12.dbo.DuToan_Tour t" _
                        & " where t.Status='rr' and t.Contact<>'ZPERSONAL'" _
                        & " and t.LstUpdate between '" & CreateFromDate(dteFromDate.Date) _
                        & "' and '" & CreateToDate(dteToDate) _
                        & "' and t.CustID in" _
                        & "(Select CustId from Go_CompanyInfo1 where Status='OK' and CMC='" _
                        & strCmc & "'))"
        strSumSfRelated = "(Select sum (i1.TTlToPax)" _
                        & " from dutoan_item i1 where i1.status='OK' and i1.RelatedItem=i.RelatedItem)"
        strSumSfNonRelated = "((Select sum (i2.TTlToPax)" _
                    & " from dutoan_item i2 where i2.status='OK' and i2.RelatedItem=0) " _
                    & "/(select count (i3.Recid) from dutoan_item i3 where i3.status='OK' and i3.RelatedItem=0))"

        strQuerry = "select SUM(i.Qty)" _
                    & ", (case RelatedItem when 0 then " & strSumSfNonRelated _
                    & " else " & strSumSfRelated & " end) /" & intROE _
                    & " from RAS12.dbo.DuToan_Item i" _
                    & " where i.Status='ok'" _
                    & strFilterTour

        Select Case strNonAirServices
            Case NonAirServices.Hotel, NonAirServices.Visa, NonAirServices.Misc
                strQuerry = strQuerry & " and i..Service='" & strNonAirServices & "')"

            Case NonAirServices.All
                'BO QUA
            Case Else
                MsgBox("CHUA XU LY NON AIR SERVICE " & strNonAirServices)
        End Select

        tblResult = GetDataTable(strQuerry)
        objSum.Nbr = tblResult.Rows(0)(0)
        objSum.Amt = tblResult.Rows(0)(1)
        Return objSum
    End Function
    Public Function SumComm4Tkt(strCmc As String, strRtgType As String _
                                  , dteFromDate As Date, dteToDate As Date _
                                  , strSRV As String, intROE As Integer, strAlType As String) As clsSum

        Dim objSum As New clsSum
        Dim tblResult As Data.DataTable
        Dim strQuerry As String = "Select count (t1.RecId) as Nbr,Sum(t1.Qty*t1.CommVal)/" & intROE _
                                & " from cwt.dbo.TKT_1a t1" _
                                & " left join ras12.dbo.TKT t on t1.Tkno=t.Tkno and t1.Qty=t.Qty" _
                                & " left join ras12.dbo.ReportData r1 on t.RecID=r1.TKID" _
                                & " where t.Status<>'xx' and t.Qty<>0 and t.AL<>'01'" _
                                & " and substring(T.RMK,1,4)='BIZT'" _
                                & " and t1.DOI between '" & CreateFromDate(dteFromDate.Date) & "' and '" & CreateToDate(dteToDate) _
                                & "' and t1.Status<>'xx' and t1.CustID in " _
                                & "(Select CustId from Go_CompanyInfo1 where Status='OK' and CMC='" _
                                & strCmc & "')"

        Select Case strRtgType.ToUpper
            Case "DOM", "INTL", "REG", "REG2"
                strQuerry = strQuerry & " and r1.Dekho='" & strRtgType & "'"
            Case "NOTDOM"
                strQuerry = strQuerry & " and r1.Dekho<>'" & strRtgType & "'"
            Case ""
                'bo qua
            Case Else
                MsgBox("Invalid RtgType:" & strRtgType)
                Return objSum
        End Select

        Select Case strSRV.ToUpper
            Case "S", "R"
                strQuerry = strQuerry & "  and t.Srv='" & strSRV & "'"
            Case ""
                'Bo qua
        End Select
        Select Case strAlType.ToUpper
            Case "LCC"
                strQuerry = strQuerry & "and ISNUMERIC(SUBSTRING(t1.tkno,1,3))=0"
            Case "FSC"
                strQuerry = strQuerry & "and ISNUMERIC(SUBSTRING(t1.tkno,1,3))=1"
            Case Else
        End Select

        tblResult = GetDataTable(strQuerry)
        objSum.Nbr = tblResult.Rows(0)(0)
        If Not IsDBNull(tblResult.Rows(0)(1)) Then
            objSum.Amt = tblResult.Rows(0)(1)
        End If

        Return objSum
    End Function


    Public Function Txt2SqlBulkCopy(strFileName As String, strDelimeter As String _
                                     , intColumnCount As Integer, strTable As String, objSql As SqlConnection) As Boolean
        Dim i As Long = 0
        Dim sr As StreamReader = New StreamReader(strFileName)
        Dim strLine As String = sr.ReadLine()
        Dim blnResult As Boolean

        Dim strArray As String() = strLine.Split(strDelimeter)
        Dim tblTemp As System.Data.DataTable = New System.Data.DataTable()
        Dim row As DataRow

        If strArray.Length <> intColumnCount Then
            MsgBox("Unmatched Number Of column")
            Return False
        End If

        For Each s As String In strArray
            tblTemp.Columns.Add(New DataColumn())
        Next

        Do While Not sr.EndOfStream
            strLine = sr.ReadLine()
            If strLine <> "" Then
                i = i + 1
                If i > 0 Then
                    row = tblTemp.NewRow()
                    Dim arrT As String() = Split(strLine, vbTab)
                    row.ItemArray = Split(strLine, vbTab)

                    tblTemp.Rows.Add(row)
                    'If i = 10 Then
                    '    Exit Do
                    'End If
                End If
            End If
        Loop

        ExecuteNonQuerry("delete " & strTable, objSql)
        Dim objSqlBulk As SqlBulkCopy = New SqlBulkCopy(objSql, SqlBulkCopyOptions.TableLock, Nothing)
        Try
            objSqlBulk.DestinationTableName = strTable
            objSqlBulk.BatchSize = tblTemp.Rows.Count
            objSqlBulk.BulkCopyTimeout = 512
            objSqlBulk.WriteToServer(tblTemp)
            blnResult = True
        Catch ex As Exception
            MsgBox("Unable to CONVERT Text to SQL" & vbNewLine & ex.Message)
            blnResult = False
        Finally
            objSqlBulk.Close()
        End Try

        Return blnResult
    End Function

    Public Function XacDinhSoVeNoi(ByVal strItinerary As String) As Int16

        Select Case strItinerary.Trim.Length
            Case Is <= 31
                Return 0
            Case Is <= 59
                Return 1
            Case Is <= 87
                Return 2
            Case Else
                Return 3
        End Select
    End Function
    Public Function MapCdrHier(dteFrom As Date, dteTo As Date) As Boolean
        'KHONG AP DUNG CHO REED MACKAY
        Dim strDateRange As String = " Doi between '" & CreateFromDate(dteFrom) & "' and '" & CreateToDate(dteTo) & "'"
        Dim tblMapping As DataTable
        tblMapping = pobjTvcs.GetDataTable("Select * from GO_CdrHierMap where Status='OK' ")
        '                                                    And CMC in" _
        '                                                    & "(select distinct Cmc from Go_Travel where Status='OK' and" _
        '                                                     & strDateRange & ")")
        For Each objRow As DataRow In tblMapping.Rows
            pobjTvcs.ExecuteNonQuerry("Update Go_travel set Hierachy" & objRow("Hier") & "=Ref" & objRow("Cdr") _
                                      & " where Status='OK' and Cmc='" & objRow("Cmc") & "' and " & strDateRange)
        Next

        Return True
    End Function
    Public Function RefreshedRequiredData() As Boolean

        If pobjTvcs.GetScalarAsDecimal("select DATEDIFF(n,tktdatefrom,getdate())" _
                                    & " from GO_MiscWzDate" _
                                    & " where Catergory='RefreshedRequiredData'") < 5 Then
            Return True
        End If
        If Not CleanDeletedRequiredData() Then
            MsgBox("Unable to clean required data")
        End If
        If Not CleanChangedRequiredData() Then
            MsgBox("Unable to clean required data")
        End If
        If Not PushNewRequiredData() Then
            MsgBox("Unable to push required data")
        End If
        If Not ExtractRequiredData() Then
            MsgBox("Unable to clean required data")
        End If

        pobjTvcs.ExecuteNonQuerry("Update GO_MiscWzDate set TktDateFrom=getdate() where catergory='RefreshedRequiredData'")
        Return True
    End Function
    Public Function CleanDeletedRequiredData() As Boolean
        Dim strQuerry As String = "Delete GO_TravelDetails where TravelId not in" _
                                  & "(Select RecId from Go_travel where Status='OK')"
        Return pobjTvcs.ExecuteNonQuerry(strQuerry)

    End Function
    Public Function CleanChangedRequiredData() As Boolean
        Dim strQuerry As String = " (Select d.TravelId from GO_TravelDetails d" _
                                  & " left join  Go_travel t on t.RecId=d.TravelId " _
                                  & " where d.DataCode='ALL' and t.RequiredData<>d.DataValue)"

        Return pobjTvcs.ExecuteNonQuerry("Delete GO_TravelDetails where TravelId  in" & strQuerry)

    End Function
    Public Function ExtractRequiredData() As Boolean
        Dim tblGoTravel As DataTable
        Dim strQuerry As String = "Select TravelId,DataValue from GO_TravelDetails where" _
                                & " DataCode='ALL' and Travelid not in " _
                                & "(select Distinct TravelId from Go_TravelDetails where DataCode<>'ALL')"

        tblGoTravel = pobjTvcs.GetDataTable(strQuerry)
        For Each objRow As DataRow In tblGoTravel.Rows
            Dim lstQuerries As New List(Of String)
            Dim arrValues As String() = Split(objRow("DataValue"), "|")
            For Each strValue As String In arrValues
                Dim arrSplit As String() = Split(strValue, "/", 2)
                lstQuerries.Add("insert GO_TravelDetails (TravelId,DataCode,DataValue) values (" _
                                & objRow("TravelId") & ",'" & arrSplit(0) & "','" & arrSplit(1) & "')")

            Next
            pobjTvcs.UpdateListQuerries(lstQuerries, False)
        Next
        Return True
    End Function
    Public Function PushNewRequiredData() As Boolean
        Dim strQuerry As String = "insert into GO_TravelDetails (TravelId,DataCode,DataValue)" _
                                    & " Select RecId,'ALL',RequiredData from Go_travel where Status='OK' and RequiredData<>'' " _
                                    & " and RecId not in (Select TravelId from GO_TravelDetails where DataCode='ALL')"

        Return pobjTvcs.ExecuteNonQuerry(strQuerry)
        Return True
    End Function
    Public Function CreateSCC11(strLocation As String, strHier1 As String, strHier2 As String, strHier3 As String _
                        , strHier4 As String, strHier5 As String) As String
        Dim strScc11 As String

        strScc11 = Mid(strLocation.PadRight(5), 1, 5) _
                & Mid((strHier1 & strHier2 & strHier3 & strHier4 & strHier5).PadRight(20), 1, 20)

        Return strScc11
    End Function
    Public Function FindSalesRcpId4Refund(ByVal pTKNO As String) As String
        Return pobjTvcs.GetScalarAsString("Select top 1 RCPID from ras12.dbo.TKT" _
                                    & " where status='OK' and SRV='S' " _
                                    & " and TKNO='" & pTKNO & "'")
    End Function
    Public Function GetColumnIndexByCdrHierName(strCdrName As String) As Integer
        If strCdrName.StartsWith("CDR") Then
            Return Mid(strCdrName, 4)
        ElseIf strCdrName.StartsWith("Hierachy") Then
            Return Mid(strCdrName, 9)
        End If

    End Function
    Public Function CrossCheckMissSaving(strMsCode As String, decPaidFare As Decimal, decLowFare As Decimal _
                                          , dteDOI As Date, strRtgType As String, strFareCur As String) As Boolean
        Dim strQuerry As String = "Select * from GO_LfThreshold where CustShortName=(Select top 1 CustShortName" _
                                  & " from Go_CompanyInfo1 where Status='OK' and CustId=" & pobjCustomer.CustId _
                                  & ") and (RtgType='' or RtgType='" & strRtgType & "')"
        Dim tblLfThreshold As System.Data.DataTable = pobjTvcs.GetDataTable(strQuerry)

        If tblLfThreshold.Rows.Count = 0 Then
            Return False
        Else
            Dim decThreshold As Decimal
            If strFareCur = tblLfThreshold.Rows(0)("Cur") Then
                decThreshold = tblLfThreshold.Rows(0)("Amount")
            ElseIf strFareCur = "USD" Then
                decThreshold = tblLfThreshold.Rows(0)("Amount") / pobjTvcs.GetUsdRoeInRas(dteDOI, True)
            ElseIf strFareCur = "VND" Then
                decThreshold = tblLfThreshold.Rows(0)("Amount") * pobjTvcs.GetUsdRoeInRas(dteDOI, True)
            End If
            If decPaidFare <= (decLowFare + decThreshold) Then
                Return False
            End If


        End If

        Return True
    End Function
    Public Function GenerateTravelRecord(intTkId As Integer) As Boolean
        Dim StrSQL As String
        Dim tblRAS As System.Data.DataTable
        Dim intTravelId As Integer
        Dim objRasTkt As DataRow
        Dim objGoAir As New clsGoAir
        Dim CMC As String, SoVeNoi As Int16, Tax As Decimal
        Dim VAT As Decimal
        Dim decAX As Decimal
        Dim strOriInvNo As String = String.Empty

        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        Dim t As SqlClient.SqlTransaction

        intTravelId = pobjTvcs.GetScalarAsDecimal("Select Travelid from Go_Air where TkId=" & intTkId)
        If intTravelId > 0 Then
            Return True
        End If



        StrSQL = "select t.*, r.CustId,c.Cmc" _
            & " from Ras12.dbo.Tkt t" _
            & " left join Ras12.dbo.Rcp r on t.RcpId=r.RecId " _
            & " left join Go_CompanyInfo1 c on r.Custid=c.CustId" _
            & " where t.RecId=" & intTkId _
            & " and t.Status<>'XX' and t.Qty<>0 and r.Status='OK' AND c.Status='OK'"

        tblRAS = GetDataTable(StrSQL)
        objRasTkt = tblRAS.Rows(0)

        Dim strFBs As String
        Dim strRBDs As String

        CMC = objRasTkt("CMC")
        If objRasTkt("SRV") = "R" Then
            strOriInvNo = pobjTvcs.GetScalarAsString("Select top 1 RcpId from tkt where Srv='S' and Tkno=" _
                                                   & "(select Tkno from tkt where RecId=" & intTkId)

        End If

        objGoAir.ParseItinerary(objRasTkt("Itinerary"))

        StrSQL = "insert cwt.dbo.GO_Travel (CustID, CMC, RLOC, TravellerName, BkgMethod, BkgTool, DepDate, "
        StrSQL = StrSQL & "BkgAction, DOI, invDate, Dossier, InvNo, OriInvNbr, bkgDate, OriDossierNbr,Location) values ('"
        StrSQL = StrSQL & objRasTkt("CustID") & "','"
        StrSQL = StrSQL & CMC & "','"
        StrSQL = StrSQL & IIf(objRasTkt("TKNO").ToString.Substring(0, 1) = "Z", objRasTkt("TKNO").ToString.Substring(3, 6), "") & "','"
        StrSQL = StrSQL & objRasTkt("PaxName") & "','"
        If objRasTkt("SRV") = "R" Then
            StrSQL = StrSQL & "M','"
        Else
            StrSQL = StrSQL & IIf(objRasTkt("TKNO").ToString.Substring(0, 1) = "Z", "M", "G") & "','"
        End If

        StrSQL = StrSQL & "MAN" & "','"
        StrSQL = StrSQL & DateTime2Text(objRasTkt("DOF")) & "','"
        StrSQL = StrSQL & IIf(objRasTkt("SRV") = "S", "AB", "AR") & "','"
        StrSQL = StrSQL & DateTime2Text(objRasTkt("DOI")) & "','"
        StrSQL = StrSQL & DateTime2Text(objRasTkt("DOI")) & "','"
        StrSQL = StrSQL & objRasTkt("RCPID") & "','"
        StrSQL = StrSQL & objRasTkt("RCPID") & "','"
        StrSQL = StrSQL & strOriInvNo & "','"
        StrSQL = StrSQL & DateTime2Text(objRasTkt("DOI")) & "','"
        StrSQL = StrSQL & strOriInvNo & "','SGN')"

        t = pobjTvcs.Connection.BeginTransaction
        cmd.Transaction = t


        cmd.CommandText = StrSQL
        cmd.ExecuteNonQuery()
        cmd.CommandText = "SELECT SCOPE_IDENTITY() AS [RecID]"
        intTravelId = cmd.ExecuteScalar

        SoVeNoi = XacDinhSoVeNoi(objRasTkt("Itinerary"))
        strFBs = objRasTkt("FareBasis")
        strRBDs = objRasTkt("BkgClass")
        If SoVeNoi > 0 Then
            Dim j As Integer = 0
            Dim tblConjTkt As DataTable
            For j = 1 To SoVeNoi
                Dim strTkno As String

                strTkno = Format(CLng(Replace(objRasTkt("TKNO").ToString, " ", "") + 1) _
                                    , "000 0000 000000")
                tblConjTkt = GetDataTable("Select top 1 * from ras12.dbo.tkt" _
                                & " where Status<>'XX' and Tkno='" & strTkno _
                                & "' order by Recid desc")

                strFBs = strFBs & "+" & tblConjTkt.Rows(0)("FareBasis")
                strRBDs = strRBDs & tblConjTkt.Rows(0)("BkgClass")
            Next
        End If
        decAX = 0
        VAT = 0
        If objRasTkt("Tax_D").ToString <> "" Then
            decAX = GetTaxAmtFromTaxDetails("AX", objRasTkt("Tax_D").ToString)
            VAT = GetTaxAmtFromTaxDetails("UE", objRasTkt("Tax_D").ToString)
        End If
        Tax = objRasTkt("Tax")

        StrSQL = "insert cwt.dbo.GO_Air (TravelID, Carrier, TKNO, OriALTKNO, AddNbrOfTKTs, SRV, DepApts, ArrApts, "
        StrSQL = StrSQL & "ALs, RBD, FBs, DOI, DepDate, Currency, Fare, trxFee, TotalTax,DocType,TkId) values ('"
        StrSQL = StrSQL & intTravelId & "','"

        StrSQL = StrSQL & objRasTkt("AL") & "','"
        StrSQL = StrSQL & Replace(objRasTkt("tkno").ToString, " ", "").Substring(3, 10) & "','"
        StrSQL = StrSQL & IIf(objRasTkt("SRV") = "R", Replace(objRasTkt("tkno").ToString, " ", "").Substring(3, 10) _
                                  , Replace(objRasTkt("StockCtrl"), " ", "")) & "','"
        StrSQL = StrSQL & SoVeNoi & "','"
        StrSQL = StrSQL & objRasTkt("SRV") & "','"
        StrSQL = StrSQL & objGoAir.DepApts & "','"
        StrSQL = StrSQL & objGoAir.ArrApts & "','"
        StrSQL = StrSQL & objGoAir.ALs & "','"
        StrSQL = StrSQL & strRBDs & "','"
        StrSQL = StrSQL & strFBs & "','"
        StrSQL = StrSQL & DateTime2Text(objRasTkt("DOI")) & "','"
        StrSQL = StrSQL & DateTime2Text(objRasTkt("DOF")) & "','"
        StrSQL = StrSQL & objRasTkt("Currency") & "',"

        If objRasTkt("SRV") = "S" Then
            StrSQL = StrSQL & objRasTkt("Fare") + objRasTkt("Charge") + -CDec(VAT) + Tax & ","
        Else
            StrSQL = StrSQL & objRasTkt("Fare") - objRasTkt("Charge") + -CDec(VAT) + Tax & ","
        End If

        StrSQL = StrSQL & objRasTkt("ChargeTV") & ","
        StrSQL = StrSQL & objRasTkt("Tax") & ",'" & objRasTkt("DocType") _
                    & "'," & objRasTkt("RecId") & ")"
        cmd.CommandText = StrSQL
        cmd.ExecuteNonQuery()

        Try
            t.Commit()
            Return True
        Catch ex As Exception
            t.Rollback()
            Return False
        End Try
    End Function

    Public Function RefreshNonAirCwt() As Boolean
        'KHONG DUNG CHO REED MACKAY
        Dim strQuerry As String = "select i.DuToanID,t.TCode,t.LstUpdate, h.* " _
            & " from Go_Hotel h" _
            & " left join ras12.dbo.Dutoan_Item i on h.ItemId=i.RecId" _
            & " left join ras12.dbo.Dutoan_Tour t on i.DutoanId=t.RecId" _
            & " left join Go_CompanyInfo1 c on c.CustId=t.CustId  and c.Status='OK'" _
            & " And (c.Go_Client=1 Or c.DataCapture=1) and c.TMC<>'RM'" _
            & " where h.NoSync='False' and i.Status='XX' and i.Service='Accommodations'" _
            & " and i.RecId not in (Select intVal from GO_MISC where Cat='ExcludedNA')" _
            & " And t.Status='RR' and t.LstUpdate>='31 Dec 21 23:59'" _
            & " order by t.LstUpdate"
        Dim tblResult As DataTable
        tblResult = pobjTvcs.GetDataTable(strQuerry)
        If tblResult.Rows.Count > 0 Then
            Dim frmShow As New frmDeletedNonAirHtl(tblResult)
            frmShow.ShowDialog()
        End If
        Return True
    End Function
    Public Sub AddManAir(dteFrom As Date, dteTo As Date, intCustId As Integer, blnReedMackay As Boolean)
        XDDieuKienAddRAS(dteFrom, dteTo, blnReedMackay)  'KHONG LAY DU LIEU REEDMACKAY
        Dim tblRAS As DataTable
        Dim strSQL As String
        Dim lstQuerries As New List(Of String)
        Dim TravelID As Integer, SoVeNoi As Int16, Tax As Decimal
        Dim VAT As Decimal
        Dim decAX As Decimal
        Dim tblNonAirItems As DataTable

        strSQL = "select case b.location when '' then 'SGN' else b.location end as Location" _
                    & ",T.RecId,r.CustID, t.AL, t.TKNO, FTKT, r.srv, r.currency, t.fare, Tax,t.Charge,t.RMK" _
                    & ", bkgClass, FareBasis, t.DOI, itinerary, "
        '^_^20220709 mark by 7643 -b-
        'strSQL = strSQL & "DOF, ChargeTV, paxName, t.DocType, tax_D, rcpid,StockCtrl, r.rcpno, Rloc " _
        '      & ",(select top 1 Cmc from go_companyinfo1 where Status='OK' and CustId=r.CustId) as CMC " & DKaddRAS _
        '      & " and T.RecId  not in (select tkid from cwt.dbo.go_AIR)" _
        '      & " And b.status<>'xx'"
        '^_^20220709 mark by 7643 -e-
        '^_^20220709 modi by 7643 -b-
        strSQL = strSQL & "DOF, ChargeTV, paxName, t.DocType, tax_D, rcpid,StockCtrl, r.rcpno, Rloc " _
              & ",(select top 1 Cmc from go_companyinfo1 where Status='OK' and CustId=r.CustId) as CMC " & DKaddRAS _
              & " And b.status<>'xx'" _
              & " and ga.RecID is null"
        '^_^20220709 modi by 7643 -e-

        If intCustId > 0 Then
            strSQL = strSQL & " and r.Custid=" & intCustId
        End If
        ' 1 trx nhieu ve thi travelID la gi, Dossier la gi
        tblRAS = pobjTvcs.GetDataTable(strSQL)
        For i As Int16 = 0 To tblRAS.Rows.Count - 1
            Dim strFBs As String
            Dim strRBDs As String
            Dim strRloc As String
            lstQuerries.Clear()

            If Mid(tblRAS.Rows(i)("TKNO"), 1, 1) = "Z" Then
                strRloc = Mid(tblRAS.Rows(i)("TKNO"), 5, 6).Trim
            Else
                strRloc = ""
            End If

            strSQL = "insert cwt.dbo.GO_Travel (CustID, CMC, RLOC, TravellerName, BkgMethod, BkgTool, DepDate, "
            strSQL = strSQL & "BkgAction, DOI, invDate, Dossier, InvNo, bkgDate, Location,Status) values ('"
            strSQL = strSQL & tblRAS.Rows(i)("CustID") & "','"
            strSQL = strSQL & tblRAS.Rows(i)("Cmc") & "','" & strRloc & "','" & tblRAS.Rows(i)("PaxName") & "','"
            If tblRAS.Rows(i)("SRV") = "R" Then
                strSQL = strSQL & "M','"
            Else
                strSQL = strSQL & IIf(tblRAS.Rows(i)("TKNO").ToString.Substring(0, 1) = "Z", "M", "G") & "','"
            End If

            strSQL = strSQL & "MAN" & "','"
            strSQL = strSQL & DateTime2Text(tblRAS.Rows(i)("DOF")) & "','"
            strSQL = strSQL & IIf(tblRAS.Rows(i)("SRV") = "S", "AB", "AR") & "','"
            strSQL = strSQL & DateTime2Text(tblRAS.Rows(i)("DOI")) & "','"
            strSQL = strSQL & DateTime2Text(tblRAS.Rows(i)("DOI")) & "','"
            strSQL = strSQL & tblRAS.Rows(i)("RCPID") & "','"
            strSQL = strSQL & tblRAS.Rows(i)("RCPNO").ToString.Substring(2, 4) & tblRAS.Rows(i)("RCPNO").ToString.Substring(7) & "','"
            strSQL = strSQL & DateTime2Text(tblRAS.Rows(i)("DOI")) & "','"
            strSQL = strSQL & tblRAS.Rows(i)("Location") & "','--')"
            lstQuerries.Add(strSQL)
            pobjTvcs.UpdateListOfQuerries(lstQuerries, True)
            TravelID = pobjTvcs.LastInsertedId
            lstQuerries.Clear()

            SoVeNoi = XacDinhSoVeNoi(tblRAS.Rows(i)("Itinerary"))
            strFBs = tblRAS.Rows(i)("FareBasis")
            strRBDs = tblRAS.Rows(i)("BkgClass")
            If SoVeNoi > 0 Then
                Dim j As Integer = 0
                Dim tblConjTkt As DataTable
                For j = 1 To SoVeNoi
                    Dim strTkno As String

                    strTkno = Format(CLng(Replace(tblRAS.Rows(i)("TKNO").ToString, " ", "") + 1) _
                                    , "000 0000 000000")
                    tblConjTkt = GetDataTable("Select top 1 * from ras12.dbo.tkt" _
                                    & " where Status<>'XX' and Tkno='" & strTkno _
                                    & "' order by Recid desc")

                    strFBs = strFBs & "+" & tblConjTkt.Rows(0)("FareBasis")
                    strRBDs = strRBDs & tblConjTkt.Rows(0)("BkgClass")
                Next
            End If
            decAX = 0
            VAT = 0
            If tblRAS.Rows(i)("Tax_D").ToString <> "" Then
                decAX = GetTaxAmtFromTaxDetails("AX", tblRAS.Rows(i)("Tax_D").ToString)
                VAT = GetTaxAmtFromTaxDetails("UE", tblRAS.Rows(i)("Tax_D").ToString)
            End If
            Tax = tblRAS.Rows(i)("Tax")

            Dim objGoAir As New clsGoAir
            objGoAir.ParseItinerary(tblRAS.Rows(i)("Itinerary"))

            strSQL = "insert cwt.dbo.GO_Air (TravelID, Carrier, TKNO, OriALTKNO, AddNbrOfTKTs, SRV, DepApts, ArrApts, "
            strSQL = strSQL & "ALs, RBD, FBs, DOI, DepDate, Currency, Fare, trxFee, TotalTax,DocType,TkId) values ('"
            strSQL = strSQL & TravelID & "','"
            strSQL = strSQL & tblRAS.Rows(i)("AL") & "','"
            strSQL = strSQL & Replace(tblRAS.Rows(i)("tkno").ToString, " ", "").Substring(3, 10) & "','"
            strSQL = strSQL & IIf(tblRAS.Rows(i)("SRV") = "R", Replace(tblRAS.Rows(i)("tkno").ToString, " ", "").Substring(3, 10) _
                                  , Replace(tblRAS.Rows(i)("StockCtrl"), " ", "")) & "','"
            strSQL = strSQL & SoVeNoi & "','"
            strSQL = strSQL & tblRAS.Rows(i)("SRV") & "','"
            strSQL = strSQL & objGoAir.DepApts & "','"
            strSQL = strSQL & objGoAir.ArrApts & "','"
            strSQL = strSQL & objGoAir.ALs & "','"
            strSQL = strSQL & strRBDs & "','"
            strSQL = strSQL & strFBs & "','"
            strSQL = strSQL & DateTime2Text(tblRAS.Rows(i)("DOI")) & "','"
            strSQL = strSQL & DateTime2Text(tblRAS.Rows(i)("DOF")) & "','"
            strSQL = strSQL & tblRAS.Rows(i)("Currency") & "',"


            If tblRAS.Rows(i)("SRV") = "S" Then
                strSQL = strSQL & tblRAS.Rows(i)("Fare") + tblRAS.Rows(i)("Charge") + Tax & ","
            Else
                strSQL = strSQL & tblRAS.Rows(i)("Fare") - tblRAS.Rows(i)("Charge") + Tax & ","
            End If

            strSQL = strSQL & tblRAS.Rows(i)("ChargeTV") & ","
            strSQL = strSQL & tblRAS.Rows(i)("Tax") & ",'" & tblRAS.Rows(i)("DocType") _
                    & "'," & tblRAS.Rows(i)("RecId") & ")"
            lstQuerries.Add(strSQL)

            If blnReedMackay Then
                tblNonAirItems = GetDataTable("select * from ras12.dbo.Dutoan_Item where Status='OK'" _
                                        & " and DutoanId in " _
                                        & " (select RecId from Dutoan_Tour where Status='RR' and RcpId=" _
                                        & tblRAS.Rows(i)("RcpId") & ")")
                If tblNonAirItems.Rows.Count > 1 Then
                    MsgBox("Cần báo Khanh.NguyenMinh trường hợp AIR kèm Non Air cho REED MACKAY" _
                           & "RCPID: " & tblRAS.Rows(i)("RcpId"))
                    Exit Sub
                End If
            End If

            lstQuerries.Add("Update Go_Travel set Status='OK' where Recid=" & TravelID)
            If Not pobjTvcs.UpdateListOfQuerries(lstQuerries) Then
                MsgBox("Unable to create Travel Record for " & tblRAS.Rows(i)("SRV") _
                       & "" & tblRAS.Rows(i)("TKNO") & " " & tblRAS.Rows(i)("DOI"))
            End If
        Next

    End Sub
    Private Sub XDDieuKienAddRAS(dteFrom As Date, dteTo As Date, blnReedMackay As Boolean)
        Dim strSelectCust As String
        Dim strReedMackayFilter As String = ""
        Dim DKDate As String = " between '" & CreateFromDate(dteFrom) _
                            & "' and '" & CreateToDate(dteTo) & "'"

        If blnReedMackay Then
            strReedMackayFilter = " and Tmc='RM'"
        Else
            strReedMackayFilter = " and Tmc<>'RM'"
        End If
        strSelectCust = "(select CustId from GO_CompanyInfo1 where Status='OK'" _
                        & " and (GO_client<>0  or DataCapture=1)" & strReedMackayFilter & ")"

        DKaddRAS = " from Ras12.dbo.tkt t inner join Ras12.dbo.rcp r "
        DKaddRAS = DKaddRAS & " on r.recid=t.rcpid " _
            & " left join cwt_bookers b on b.bookername=t.Booker and b.custid=r.CustID" _
            & " left join cwt.dbo.go_AIR ga on t.RecID=ga.Tkid" _  '^_^20220709 add by 7643
            & " where t.status<>'XX' and r.status <>'XX' and  "
        DKaddRAS = DKaddRAS & " t.rmk like '%BIZT%' and qty <>0 and r.srv not in ('V','A') and t.doctype in ('TKT','ETK','MCO') and "
        DKaddRAS = DKaddRAS & " r.CustID in " & strSelectCust
        DKaddRAS = DKaddRAS & " and ((t.fstupdate " & DKDate & " ) or (t.DOI " & DKDate & "))"
    End Sub
    Public Function FillOriginalInv(dteFrom As Date, dteTo As Date) As Boolean
        'KHONG DUNG CHO REED MACKAY
        Dim tblTravel As System.Data.DataTable
        Dim strQuerry As String = "Select t.*" _
                                  & ",(select top 1 s.RCPID from ras12.dbo.tkt s WHERE s.tkno=tkt.tkno and s.srv='S' order by RecId desc) as SalesRcpID " _
                                  & " from Go_Travel t" _
                                  & " left join GO_AIR A on a.TravelId=t.RecId" _
                                  & " left join ras12.dbo.tkt tkt on tkt.recId=a.tkId" _
                                  & " where t.Status='OK' and OriInvNbr='' and BkgAction='AR'" _
                                  & " and t.Doi between '" & CreateFromDate(dteFrom) _
                                  & "' and '" & CreateToDate(dteTo) & "' and t.CustId not in " _
                                  & " (select CustId from CWT.DBO.GO_CompanyInfo1 where Status='OK' and TMC<>'RM')"
        Dim lstQuerries As New List(Of String)

        tblTravel = pobjTvcs.GetDataTable(strQuerry)
        For Each objRow As DataRow In tblTravel.Rows
            If Not IsDBNull(objRow("SalesRcpID")) Then
                lstQuerries.Add("Update go_travel set OriDossierNbr=" & objRow("SalesRcpID") _
                            & ",OriInvNbr=" & objRow("SalesRcpID") & " where RecId=" & objRow("RecId"))
            End If

        Next
        If Not pobjTvcs.UpdateListOfQuerries(lstQuerries) Then
            MsgBox("Unable to find Original Invoice for refunded ticket")
        End If
        Return True
    End Function
    Public Function FillGDS_Record(dteFromDate As Date, dteToDate As Date _
                                   , Optional intCustId As Integer = 0 _
                                   , Optional blnReedMackay As Boolean = False) As Boolean
        Dim r As Int16, tmpFOP As String, CCNO As String = "", CrdCode As String, emplID As String
        Dim TVCCCode As String = "_ADJMVT"
        Dim CWTCCCode As String = "??-AX-DC-JC-CA-VI-TP"
        Dim dTable As DataTable, dTable1 As DataTable
        Dim intCcId As Integer
        Dim strSQL As String
        Dim tblTravel As DataTable

        strSQL = "Select Dossier, * from cwt.dbo.GO_Travel where status='OK' and DOI between '" _
                & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) _
                & "' and custID in (select CustID from GO_CompanyInfo1" _
                & " where (GO_client<>0  Or DataCapture=1) And Status='OK'"
        If blnReedMackay Then
            strSQL = strSQL & " and Tmc='RM'"
        Else
            strSQL = strSQL & " and Tmc<>'RM'"
        End If
        strSQL = strSQL & ")"

        If intCustId > 0 Then
            strSQL = strSQL & " and custID =" & intCustId
        End If

        tblTravel = pobjTvcs.GetDataTable(strSQL)

        For r = 0 To tblTravel.Rows.Count - 1
            'If GridTravel.Item("Dossier", r).Value = "" Then

            strSQL = "select RCPID, RCPNO, TKNO, FTKT, fstUpdate, chargeTV, PaxName, SRV, fare+tax-commVAL as TTLFare , doi"
            strSQL = strSQL & " from Ras12.dbo.TKT t where status='OK' "
            If tblTravel.Rows(r)("Dossier") = "" Then
                strSQL = strSQL & " and srv in('S','V') and TKNO in (select TKNO from tvcs.dbo.tkt_1A "
                strSQL = strSQL & " where GO_travelID=" & tblTravel.Rows(r)("RecID") & ")"
            Else
                strSQL = strSQL & " and RCPID=" & tblTravel.Rows(r)("Dossier")
            End If
            dTable = GetDataTable(strSQL)

            For i As Int16 = 0 To dTable.Rows.Count - 1
                If dTable.Rows(i)("SRV") = "V" Then
                    strSQL = "Update cwt.dbo.GO_Air set Voided=1 where"
                Else
                    If tblTravel.Rows(r)("EmplID") <> "" Then
                        emplID = tblTravel.Rows(r)("EmplID")
                    Else
                        emplID = xacDinhEmplID(tblTravel.Rows(r)("TravellerName"), tblTravel.Rows(r)("CustID"))
                    End If

                    If emplID Is Nothing Or emplID = "" Then emplID = ""

                    strSQL = "select top 1 FOP, document,CcId from Ras12.dbo.FOP where fop <>'RND' and status <>'XX' and rcpID= "
                    strSQL = strSQL & dTable.Rows(i)("RCPID")
                    dTable1 = GetDataTable(strSQL)

                    tmpFOP = ""
                    intCcId = 0

                    For j As Int16 = 0 To dTable1.Rows.Count - 1
                        tmpFOP = tmpFOP & dTable1.Rows(j)("FOP")
                        CCNO = dTable1.Rows(j)("Document")
                        intCcId = dTable1.Rows(j)("CcId")
                    Next

                    CrdCode = ""
                    If tmpFOP.Length > 3 Then
                        tmpFOP = "MP"
                    ElseIf tmpFOP = "CRD" Then
                        tmpFOP = "CC"
                        If CCNO <> "" Then
                            'CrdCode =  CWTCCCode.Split("-")(InStr(TVCCCode, CCNO.Substring(0, 1)))
                            CCNO = CCNO.Substring(1)
                            If CCNO.Length = 17 Then
                                CCNO = CCNO.Substring(1)
                            End If
                        End If
                        If intCcId = 0 Then
                            MsgBox("Must clear payment for " & dTable.Rows(i)("RCPNO"))
                            If dTable1.Rows(0)("Document").Length = 2 Then
                                CrdCode = dTable1.Rows(0)("Document")
                            End If

                            'Return False
                        Else
                            CrdCode = pobjTvcs.GetScalarAsString("Select top 1 VAL2 FROM ras12.dbo.MISC where RecId=" & intCcId)
                        End If


                    ElseIf tmpFOP = "PSP" Then
                        tmpFOP = "IN"
                    Else
                        tmpFOP = "CA"
                    End If

                    If tmpFOP <> "CC" Then CCNO = ""
                    'Bo sung them so the cua truong hop Refund neu thieu
                    If dTable.Rows(i)("SRV") = "R" AndAlso tmpFOP = "CC" AndAlso CCNO = "" Then
                        CCNO = pobjTvcs.GetCcNbrByTktInRas(dTable.Rows(i)("TKNO"))
                    End If
                    strSQL = "update cwt.dbo.GO_travel set "
                    strSQL = strSQL & "Dossier='" & dTable.Rows(i)("RCPID") & "'"
                    strSQL = strSQL & ", INVNo='" & dTable.Rows(i)("RCPNO").ToString.Substring(2, 4) & dTable.Rows(i)("RCPNO").ToString.Substring(7) & "'"
                    strSQL = strSQL & ", FOP='" & tmpFOP & "'"
                    strSQL = strSQL & ", CCNO='" & CCNO & "'"
                    strSQL = strSQL & ", CCCode='" & CrdCode & "'"

                    If dTable.Rows(i)("SRV") = "R" Then
                        strSQL = strSQL & ", bkgDate='" & DateTime2Text(dTable.Rows(i)("DOI")) & "'"
                    End If
                    strSQL = strSQL & " where RecID=" & tblTravel.Rows(r)("RecID")
                    pobjTvcs.ExecuteNonQuerry(strSQL)


                    'StrSQL = "Update cwt.dbo.GO_Air set TrxFee=" & dTable.Rows(i)("ChargeTV") & " where TKNO='"
                    'StrSQL = StrSQL & Replace(dTable.Rows(i)("TKNO").ToString.Substring(4), " ", "") & "' and "

                    '    If Me.GridTravel.Item("RLOC", r).Value <> "" Then
                    '        StrSQL = StrSQL & " ltrim(rtrim(ETA)) <>'' and "
                    '    Else
                    '        StrSQL = StrSQL & " ltrim(rtrim(ETA)) = '' and "
                    '    End If
                    'StrSQL = StrSQL & " travelID=" & Me.GridTravel.Item("RecID", r).Value
                    'cmd.CommandText = StrSQL
                    'cmd.ExecuteNonQuery()
                End If

                'cmd.CommandTimeout = 1024
                pobjTvcs.ExecuteNonQuerry("update cwt.dbo.go_AIR set fare=" & dTable.Rows(i)("TTLFare") & " where TKNO='" &
                    Replace(dTable.Rows(i)("TKNO").ToString.Substring(4), " ", "") _
                    & "' and travelid=" & tblTravel.Rows(r)("RecID") &
                    " and fare=0 and oriALTKNO=''")
                'cmd.ExecuteNonQuery()
            Next
            'End If
        Next

    End Function
    Public Function TcodeLinkMultiTkt(dteFromDate As Date, dteToDate As Date) As Boolean
        Dim strQuerry As String = "select Tcode,count(m.intval)" _
                                    & " From RAS12.dbo.MISC m" _
                                    & " left join RAS12.dbo.DuToan_Tour t on m.IntVal=t.RecID" _
                                    & " where m.cat='WzAir' and m.Status='OK'" _
                                    & " and t.LstUpdate between '" & CreateFromDate(dteFromDate) _
                                    & "' and '" & CreateToDate(dteToDate) _
                                    & "' group by TCode having count(m.intval)>1"
        Dim tblTcodes As DataTable = pobjTvcs.GetDataTable(strQuerry)
        If tblTcodes.Rows.Count = 0 Then
            Return False
        Else
            Dim frmShow As New frmShowTableContent(tblTcodes, "Tcode linked with Multiple Tickets")
            frmShow.ShowDialog()
            Return True
        End If
    End Function
    Public Function FillLocalNonAir(dteFromDate As Date, dteToDate As Date, Optional intCustId As Integer = 0) As Boolean
        'KHONG AP DUNG CHO REED MACKAY
        Dim tblNonAir As System.Data.DataTable
        Dim arrInsertGoTravel(0 To 0) As String
        Dim strInsertGoHotel As String
        Dim i As Integer
        Dim intTravelId As Integer
        Dim strSQL As String
        Dim strAvoidDup As String = " and i.recid not in (Select distinct ItemId from cwt.dbo.go_hotel)" _
                             & " and i.RecId not in (Select intVal from go_Misc where Cat='ExcludedNA')"
        ' bo dieu kien nay vi 1 DutoanId co the co nhieu non air service cung là Hotel
        '    & " and Dutoanid not in (Select DutoanId from go_travel where Status='ok')"

        strSQL = "Select isnull(a.Travelid,0) as TravelId, t.Status,t.Status,t.Custid" _
                & ",t.Brief as Rmk1,t.Location,t.Traveller,Sdate" _
                & ",Tcode,t.LstUpdate as DOI,c.Tmc,i.*" _
                & " from RAS12.dbo.DuToan_Tour t" _
                & " left join cwt.dbo.GO_CompanyInfo1 c on t.CustID=c.CustID  and c.Status='OK'" _
                & " left join  RAS12.dbo.DuToan_Item i on (t.RecId=i.DuToanID) " _
                & " left join ras12.dbo.MISC m On t.RecID=m.IntVal And m.CAT ='WzAir' and m.Status='OK'" _
                & " Left Join cwt.dbo.GO_Air a On m.IntVal1=a.Tkid" _
                & " where t.Status='RR' and t.Contact<>'ZPERSONAL' and i.Status='OK' " _
                & " and i.Service ='Accommodations' " _
                & " and (c.GO_client=1 or c.DataCapture=1) and c.TMC<>'RM'" _
                & " and t.LstUpdate between '" _
                & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) _
                & "'" & strAvoidDup

        If intCustId > 0 Then
            strSQL = strSQL & " and t.CustId=" & intCustId
        End If
        strSQL = strSQL & " order by DutoanId,SupplierId,i.Recid"

        'If Me.ChkPendingOnly.Checked Then
        '    StrSQL = StrSQL & " and recid in (select TravelID from cwt.dbo.go_Hotel where referenceRate=0 or LowestRate=0))"
        'End If

        tblNonAir = pobjTvcs.GetDataTable(strSQL)

        With tblNonAir
            For i = 0 To tblNonAir.Rows.Count - 1
                Dim objRcp As New clsRasRcp
                Dim objHtl As New clsLocalHotelInfo
                Dim intNbrOfRoom As Integer = 1
                Dim strCityCode As String = pobjTvcs.GetScalarAsString("Select CityCode from RAS12.DBO.supplier" _
                                                                            & " where RecId=" & .Rows(i)("SupplierId"))
                Dim strCountryCode As String = pobjTvcs.GetCountryCode(strCityCode)
                Dim strCityName As String = pobjTvcs.GetCityName(strCityCode)
                Dim strHotelName As String = Replace(.Rows(i)("Supplier"), "'", "*")
                Dim strTelNbr As String = pobjTvcs.GetTel4LocalHtl(strHotelName, strCityCode)
                Dim decRatePerRoomPerNight As Decimal

                Dim tblCdrs As System.Data.DataTable
                Dim strBkgAction As String = "AB"
                Dim decTrxFee As Decimal
                Dim decTrxFeeVnd As Decimal = pobjTvcs.GetTrxFee4NonAirHotel(.Rows(i)("DuToanID"), .Rows(i)("RelatedItem"))
                Dim strError As String = ""
                Dim blnWzAir As Boolean

                Dim strCdrQuerry As String = "Select s.Fvalue as CdrValue,c.CdrNbr from SIR s left join cwt.dbo.go_cdrs c on" _
                                            & "(fname=cdrname and " _
                                            & " (select top 1 CMC from cwt.dbo.go_companyinfo1 where status='ok' AND custid= s.custid)=c.CMC)" _
                                            & " where c.Status='OK' and s.status='OK' and RcpId=" & .Rows(i)("DuToanID") & " and s.Prod='nonair'"
                'If .Rows(i)("ROE") = 0 Then
                '    MsgBox("ROE must not be 0 for TourCode " & .Rows(i)("Tcode"))
                '    Return False
                'End If
                If .Rows(i)("BookOnly") Then
                    decRatePerRoomPerNight = .Rows(i)("Cost")
                ElseIf .Rows(i)("Ccurr") = "VND" Then
                    decRatePerRoomPerNight = .Rows(i)("TTLToPax") / .Rows(i)("Qty")
                ElseIf .Rows(i)("ROE") = 0 Then
                    strError = "ROE must not be 0 for TourCode " & .Rows(i)("Tcode")
                    MsgBox(strError)
                    Return False
                Else
                    decRatePerRoomPerNight = .Rows(i)("TTLToPax") / (.Rows(i)("ROE") * .Rows(i)("Qty"))
                End If

                If decRatePerRoomPerNight = 0 Then
                    Dim blnZeroRate As Boolean = True
                    If IsDBNull(.Rows(i)("Cost4CWT")) Then
                        blnZeroRate = True
                    ElseIf .Rows(i)("Cost4CWT") = 0 Then
                        blnZeroRate = True
                    Else
                        decRatePerRoomPerNight = .Rows(i)("Cost4CWT")
                        blnZeroRate = False
                    End If
                    If blnZeroRate AndAlso .Rows(i)("TMC") = "CWT" Then
                        strError = "Non Air must input Cost4CWT for Tcode/Item " & .Rows(i)("Tcode") & "*" & .Rows(i)("RecId")
                        Dim frmMsg As New frmShowMessageInTextBox("", strError)
                        frmMsg.ShowDialog()
                        Append2TextFile(strError)
                        Return False
                    End If
                End If

                If .Rows(i)("Ccurr") = "VND" Then
                    decTrxFee = decTrxFeeVnd
                Else
                    decTrxFee = decTrxFeeVnd / .Rows(i)("ROE")
                End If

                If .Rows(i)("TTLToPax") < 0 Then
                    strBkgAction = "AR"
                End If


                objRcp = pobjTvcs.GetRcp4LocalNonAir(.Rows(i)("DutoanID"))

                If .Rows(i)("TravelId") > 0 AndAlso .Rows(i)("TravelId") <> intTravelId Then
                    intTravelId = .Rows(i)("TravelId")
                    blnWzAir = True
                Else
                    blnWzAir = False
                    tblCdrs = GetDataTable(strCdrQuerry)

                    arrInsertGoTravel(0) = "Insert into GO_Travel (DutoanId,CustId, CMC,TravellerName,EmplID" _
                        & ",TripPurpose,BkgMethod,BkgTool,BkgAction" _
                        & ",CorpType,BkgDate,DOI,InvNo,InvDate) values (" & .Rows(i)("DutoanId") & "," _
                        & .Rows(i)("CustId") & ",'" & pobjTvcs.GetCmcByCustId(.Rows(i)("CustId")) _
                        & "','" & Mid(AdjustPaxName4GO(.Rows(i)("Traveller")), 1, 50) _
                        & "','" & pobjTvcs.GetEmpId(.Rows(i)("CustId"), .Rows(i)("Traveller")) _
                        & "','B','M','MAN','" & strBkgAction & "','" & .Rows(i)("TMC") & "','" & DateTime2Text(.Rows(i)("SDate")) _
                        & "','" & DateTime2Text(.Rows(i)("DOI")) & "',"

                    With objRcp
                        If .RcpNo = "" Then
                            arrInsertGoTravel(0) = arrInsertGoTravel(0) & "'',Getdate())"
                        Else
                            arrInsertGoTravel(0) = arrInsertGoTravel(0) & "'" & Mid(.RcpNo, 3, 4) & Mid(.RcpNo, 8) _
                                            & "','" & DateTime2Text(.DOI) & "')"
                        End If
                    End With

                    If Not pobjTvcs.Update(arrInsertGoTravel) Then
                        MsgBox(pobjTvcs.UpdtErr)
                        Return False
                    Else
                        intTravelId = pobjTvcs.LastInsertedId
                    End If

                End If

                If Not blnWzAir AndAlso tblCdrs.Rows.Count > 0 Then
                    If Not pobjTvcs.UpdateCDRs(tblCdrs, intTravelId) Then
                        pobjTvcs.ExecuteNonQuerry("DELETE GO_TRAVEL WHERE RECID=" & intTravelId)
                        Return False
                    End If
                End If
                strInsertGoHotel = "Insert into GO_Hotel (BookDate,CityCode,HotelName" _
                       & ",CheckinDate,RoomType,NbrOfRm,NbrOfNight,CurCode" _
                       & ",RatePerRoomPerNight,ResType,CityName" _
                       & ",CountryCode,ReferenceRate,LowestRate" _
                       & ",RsCode,MsCode,TravelID,VoucherNbr" _
                       & ",LocalIntlCode,LocalIntlCodeType,CustPrefered,Rmk1,Rmk2,TrxFee,ItemId,TrxFeeVnd) values ('"


                strInsertGoHotel = strInsertGoHotel & DateTime2Text(.Rows(i)("SDate")) _
                                    & "','" & strCityCode _
                                    & "','" & strHotelName & "','"
                If objHtl.Parse(.Rows(i)("Brief")) Then
                    strInsertGoHotel = strInsertGoHotel & DateTime2Text(objHtl.CheckInDate) _
                        & "','" & objHtl.RoomType & "',"
                Else
                    strInsertGoHotel = strInsertGoHotel & "GetDate()" & "','A1S',"
                    objHtl.NbrOfRoom = 1
                End If
                strInsertGoHotel = strInsertGoHotel & objHtl.NbrOfRoom & "," & .Rows(i)("Qty") / objHtl.NbrOfRoom _
                                    & ",'" & .Rows(i)("CCurr") _
                                    & "'," & decRatePerRoomPerNight & "," & IIf(.Rows(i)("BookOnly") = 1, 0, 1) _
                                    & ",'" & strCityName & "','" & strCountryCode & "'," & decRatePerRoomPerNight _
                                    & "," & decRatePerRoomPerNight & ",'" & pobjTvcs.GetDefaultRS4LocalHtl(.Rows(i)("CustID")) _
                                    & "','" & pobjTvcs.GetDefaultMS4LocalHtl(.Rows(i)("CustID")) & "'," & intTravelId _
                                    & ",'" & "DUTOANID" & .Rows(i)("DuToanID") _
                                    & "'," & pobjTvcs.GetHarpKeyFromAllSources(strHotelName, strCityCode) _
                                    & ",'HRP',0,'" & Replace(.Rows(i)("Rmk1"), "'", "*") & "','" _
                                    & Replace(Mid(.Rows(i)("Brief"), 1, 256), "'", "*") _
                                    & "'," & decTrxFee & "," & .Rows(i)("RecId") & "," & decTrxFeeVnd & ")"
                If Not pobjTvcs.ExecuteNonQuerry(strInsertGoHotel) Then
                    MsgBox(pobjTvcs.UpdtErr)
                    Return False
                End If
            Next
        End With

        Return True

    End Function
    Public Function FillVisa(dteFromDate As Date, dteToDate As Date, Optional intCustId As Integer = 0) As Boolean
        'KHONG AP DUNG CHO REED MACKAY
        Dim tblNonAir As System.Data.DataTable
        Dim arrInsertGoTravel(0 To 0) As String
        Dim strInsertGoVisa As String
        Dim i As Integer
        Dim strSQL As String
        '^_^20220708 mark by 7643 -b-
        'Dim strAvoidDup As String = " and 'VISA'+ Cast(i.Recid as varchar) not in " _
        '                            & "(Select distinct RLOC from cwt.dbo.go_travel where Status='OK')"

        'strSQL = "Select t.Status,Custid,t.Brief as Rmk1,t.Location,t.Traveller,Sdate,Tcode" _
        '        & ",t.LstUpdate as DOI,t.FstUpdate as BkgDate,i.*" _
        '        & " from RAS12.dbo.DuToan_Tour t" _
        '        & " left join  RAS12.dbo.DuToan_Item i on (t.RecId=i.DuToanID) " _
        '        & " where t.Status='RR' and i.Status='OK' " _
        '        & " and Service ='Visa' and Contact<>'ZPERSONAL'" _
        '        & " and t.LstUpdate between '" _
        '        & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) _
        '        & "' and t.custID in (select CustID from cwt.dbo.GO_CompanyInfo1 where GO_client<>0 and Status='OK')" _
        '        & strAvoidDup
        '^_^20220708 mark by 7643 -e-
        '^_^20220708 modi by 7643 -b-
        strSQL = "Select t.Status,t.Custid,t.Brief as Rmk1,t.Location,t.Traveller,Sdate,Tcode" _
                & ",t.LstUpdate as DOI,t.FstUpdate as BkgDate,c.TMC,i.*" _
                & " from RAS12.dbo.DuToan_Tour t" _
                & " left join cwt.dbo.GO_CompanyInfo1 c on t.CustID=c.CustID  and c.Status='OK'" _
                & " left join  RAS12.dbo.DuToan_Item i on (t.RecId=i.DuToanID) " _
                & " left join cwt.dbo.go_travel gt on 'VISA'+ Cast(i.Recid as varchar)=gt.RLOC and gt.status='OK' " _
                & " where t.Status='RR' and i.Status='OK' " _
                & " and Service ='Visa' and Contact<>'ZPERSONAL'" _
                & " and t.LstUpdate between '" _
                & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) _
                & "' and (c.GO_client=1 or DataCapture=1) and c.Tmc<>'RM' And c.Status='OK'" _  'KHÔNG LẤY CHO REED MACKAY
                & " and gt.recid is null"
        '^_^20220708 modi by 7643 -e-

        If intCustId > 0 Then
            strSQL = strSQL & " and t.CustId=" & intCustId
        End If
        strSQL = strSQL & " order by t.Recid,i.Recid"

        tblNonAir = GetDataTable(strSQL)
        With tblNonAir
            For i = 0 To tblNonAir.Rows.Count - 1
                Dim objRcp As New clsRasRcp
                Dim intTravelId As Integer
                Dim tblCdrs As System.Data.DataTable
                Dim strPaxName As String = Mid(AdjustPaxName4GO(.Rows(i)("Traveller")), 1, 50)
                Dim strCdrQuerry As String = "Select s.Fvalue as CdrValue,c.CdrNbr from SIR s left join cwt.dbo.go_cdrs c on" _
                                            & "(fname=cdrname and " _
                                            & " (select top 1 CMC from cwt.dbo.go_companyinfo1 where status='ok' AND custid= s.custid)=c.CMC)" _
                                            & " where c.Status='OK' and s.status='OK' and RcpId=" & .Rows(i)("DuToanID") & " and s.Prod='nonair'"

                Dim strSupplierCode As String = GetMiscSupplierCode(.Rows(i)("Vendor"))

                If strSupplierCode = "" AndAlso .Rows(i)("TMC") = "TMC" Then
                    MsgBox("Please request new supplier code from CWT for " & .Rows(i)("Vendor"))
                    CreateMiscSupplierCode(.Rows(i)("Vendor"))
                End If

                tblCdrs = GetDataTable(strCdrQuerry)

                objRcp = pobjTvcs.GetRcp4LocalNonAir(.Rows(i)("DutoanID"))

                arrInsertGoTravel(0) = "Insert into GO_Travel (Status,CustId, CMC,TravellerName,EmplID" _
                        & ",TripPurpose,BkgMethod,BkgTool,BillingSupplier" _
                        & ",CorpType,BkgDate,DOI,InvNo,InvDate,RLOC) values ('OK'," & .Rows(i)("CustId") & ",'" _
                        & pobjTvcs.GetCmcByCustId(.Rows(i)("CustId")) _
                        & "','" & strPaxName _
                        & "','" & pobjTvcs.GetEmpId(.Rows(i)("CustId"), strPaxName) _
                        & "','B','M','MAN','DSU','" & .Rows(i)("TMC") & "','" & DateTime2Text(.Rows(i)("BkgDate")) _
                        & "','" & DateTime2Text(.Rows(i)("DOI")) & "',"

                With objRcp
                    If .RcpNo = "" Then
                        arrInsertGoTravel(0) = arrInsertGoTravel(0) & "'',Getdate(),'VISA" & tblNonAir.Rows(i)("RECID") & "')"
                    Else
                        arrInsertGoTravel(0) = arrInsertGoTravel(0) & "'" & Mid(.RcpNo, 3, 4) & Mid(.RcpNo, 8) _
                                            & "','" & DateTime2Text(.DOI) & "','VISA" & tblNonAir.Rows(i)("RECID") & "')"
                    End If
                End With

                If Not pobjTvcs.Update(arrInsertGoTravel) Then
                    MsgBox(pobjTvcs.UpdtErr)
                    Return False
                Else
                    intTravelId = pobjTvcs.LastInsertedId
                End If

                If tblCdrs.Rows.Count > 0 Then
                    pobjTvcs.UpdateCDRs(tblCdrs, intTravelId)
                End If
                strInsertGoVisa = "Insert into GO_MiscSvc (TravelID, SvcType, DocNbr, SRV" _
                        & ", Currency, Fare, RefFare, LowestFare " _
                        & ", CommIndicator, TrxFeeApplied, Comm, TotalTax, TrxFee" _
                        & ", ProductCode, OriDocNbr, SupplierCode, SupplierLocalName" _
                        & ", Brief,Tcode,ItemId,TcodeId) values (" _
                        & intTravelId & ",'VISA','" & "DUTOANID" & .Rows(i)("DuToanID") & "','S','VND'," _
                        & .Rows(i)("TTLToPax") & "," & .Rows(i)("TTLToPax") & "," & .Rows(i)("TTLToPax") _
                        & ",'N','Y',0," & .Rows(i)("TTLToPax") * 0.1 & "," & GetSvcFee4Visa(.Rows(i)("DutoanID")) _
                        & ",'M','','" & strSupplierCode & "','" _
                        & Mid(ConvertMiscSupplierCode(.Rows(i)("Vendor")), 1, 25) _
                        & "','" & .Rows(i)("Brief") & "','" & .Rows(i)("TCode") _
                        & "'," & tblNonAir.Rows(i)("RecId") & "," & tblNonAir.Rows(i)("DutoanId") & ")"

                If Not pobjTvcs.ExecuteNonQuerry(strInsertGoVisa) Then
                    MsgBox(pobjTvcs.UpdtErr)
                    Return False
                End If
            Next
        End With

        Return True

    End Function
    Public Function FillMiscSvc4RM(dteFromDate As Date, dteToDate As Date) As Boolean
        'TAT CA DICH VU NON AIR, TRU HOTEL VA KHONG GAN LIEN TKT
        Dim tblNonAir As System.Data.DataTable

        Dim strSQL As String
        Dim strTcodeStatus = "RR"

        'strTcodeStatus = "OC"   'CHỈ DÙNG ĐỂ TEST KHI CAN
        strSQL = "Select t.Status,t.Custid,t.Brief as Rmk1,t.Location,t.Traveller,Sdate,Tcode" _
                & ",t.LstUpdate as DOI,t.FstUpdate as BkgDate,c.TMC,i.*" _
                & " from RAS12.dbo.DuToan_Tour t" _
                & " left join cwt.dbo.GO_CompanyInfo1 c on t.CustID=c.CustID  and c.Status='OK'" _
                & " left join  RAS12.dbo.DuToan_Item i on (t.RecId=i.DuToanID) " _
                & " left join cwt.dbo.go_travel gt on 'MIS'+ Cast(i.Recid as varchar)=gt.RLOC and gt.status='OK' " _
                & " left join Cwt.dbo.Go_air a on a.TravelID=gt.RecID" _
                & " where t.Status='" & strTcodeStatus _
                & "' and t.RecId not in (select TcodeId from [CWT].[dbo].[GO_MiscSvc])" _
                & " and t.RecId not in (select TravelId from Cwt.dbo.Go_air)" _
                & " and i.Status='OK' " _
                & " and Contact<>'ZPERSONAL'" _
                & " and t.LstUpdate between '" _
                & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) _
                & "' and (c.GO_client=1 or DataCapture=1) And c.Status='OK' and c.Tmc='RM'" _
                & " and gt.recid is null  and a.recid is null"

        strSQL = strSQL & " order by t.Recid,i.RelatedItem,i.RecId"

        tblNonAir = GetDataTable(strSQL)
        AddNonAirTable2CwtDatabase(tblNonAir)

        Return True

    End Function
    Private Function AddNonAirTable2CwtDatabase(tblNonAir As Data.DataTable) As Boolean
        Dim arrInsertGoTravel(0 To 0) As String
        Dim strInsertService As String
        Dim i As Integer
        With tblNonAir
            For i = 0 To tblNonAir.Rows.Count - 1
                Dim objRcp As New clsRasRcp
                Dim intTravelId As Integer
                Dim tblCdrs As System.Data.DataTable
                Dim strPaxName As String = AdjustPaxName4GO(.Rows(i)("Traveller"))
                Dim strCdrQuerry As String = "Select s.Fvalue as CdrValue,c.CdrNbr from SIR s" _
                                            & " left join cwt.dbo.go_cdrs c on" _
                                            & "(fname=cdrname And " _
                                            & " (select top 1 CMC from cwt.dbo.go_companyinfo1" _
                                            & " where status='ok' AND custid= s.custid)=c.CMC)" _
                                            & " where c.Status='OK' and s.status='OK' and RcpId=" _
                                            & .Rows(i)("DuToanID") & " and s.Prod='nonair'"

                Dim strSupplierCode As String = ""
                Dim strSvcCode As String = GetSvcCodeBySvcName(.Rows(i)("Service"))
                Dim intDutoanID As Integer

                tblCdrs = GetDataTable(strCdrQuerry)

                objRcp = pobjTvcs.GetRcp4LocalNonAir(.Rows(i)("DutoanID"))
                If intDutoanID <> .Rows(i)("DutoanID") Then
                    arrInsertGoTravel(0) = "Insert into GO_Travel " _
                        & "(Status,CustId, CMC,TravellerName,EmplID" _
                        & ",TripPurpose,BkgMethod,BkgTool,BillingSupplier" _
                        & ",CorpType,BkgDate,DOI,InvNo,InvDate,RLOC) values ('OK'," & .Rows(i)("CustId") & ",'" _
                        & pobjTvcs.GetCmcByCustId(.Rows(i)("CustId")) _
                        & "','" & strPaxName _
                        & "','" & pobjTvcs.GetEmpId(.Rows(i)("CustId"), strPaxName) _
                        & "','B','M','MAN','DSU','" & .Rows(i)("TMC") & "','" & DateTime2Text(.Rows(i)("BkgDate")) _
                        & "','" & DateTime2Text(.Rows(i)("DOI")) & "',"

                    With objRcp
                        If .RcpNo = "" Then
                            arrInsertGoTravel(0) = arrInsertGoTravel(0) & "'',Getdate(),'MIS" & tblNonAir.Rows(i)("RECID") & "')"
                        Else
                            arrInsertGoTravel(0) = arrInsertGoTravel(0) & "'" & Mid(.RcpNo, 3, 4) & Mid(.RcpNo, 8) _
                                                & "','" & DateTime2Text(.DOI) & "','MIS" & tblNonAir.Rows(i)("RECID") & "')"
                        End If
                    End With

                    If Not pobjTvcs.Update(arrInsertGoTravel) Then
                        MsgBox(pobjTvcs.UpdtErr)
                        Return False
                    Else
                        intTravelId = pobjTvcs.LastInsertedId
                        intDutoanID = .Rows(i)("DutoanID")
                    End If

                    If tblCdrs.Rows.Count > 0 Then
                        pobjTvcs.UpdateCDRs(tblCdrs, intTravelId)
                    End If
                End If

                If strSvcCode = "HTL" Then
                    'chua xong phan nay
                    Dim objHtl As New clsLocalHotelInfo
                    Dim strCityCode As String = pobjTvcs.GetScalarAsString("Select CityCode " _
                                                            & " from RAS12.DBO.supplier" _
                                                            & " where RecId=" & .Rows(i)("SupplierId"))
                    Dim strCountryCode As String = pobjTvcs.GetCountryCode(strCityCode)
                    Dim strCityName As String = pobjTvcs.GetCityName(strCityCode)
                    Dim strHotelName As String = Replace(.Rows(i)("Supplier"), "'", "*")
                    Dim strTelNbr As String = pobjTvcs.GetTel4LocalHtl(strHotelName, strCityCode)
                    Dim decRatePerRoomPerNight As Decimal
                    strInsertService = "Insert into GO_Hotel (BookDate,CityCode,HotelName" _
                       & ",CheckinDate,RoomType,NbrOfRm,NbrOfNight,CurCode" _
                       & ",RatePerRoomPerNight,ResType,CityName" _
                       & ",CountryCode,ReferenceRate,LowestRate" _
                       & ",RsCode,MsCode,TravelID,VoucherNbr" _
                       & ",LocalIntlCode,LocalIntlCodeType,CustPrefered,Rmk1,Rmk2,TrxFee,ItemId,TrxFeeVnd) values ('"


                    strInsertService = strInsertService & DateTime2Text(.Rows(i)("SDate")) _
                                    & "','" & strCityCode _
                                    & "','" & strHotelName & "','"
                    If objHtl.Parse(.Rows(i)("Brief")) Then
                        strInsertService = strInsertService & DateTime2Text(objHtl.CheckInDate) _
                        & "','" & objHtl.RoomType & "',"
                    Else
                        strInsertService = strInsertService & "GetDate()" & "','A1S',"
                        objHtl.NbrOfRoom = 1
                    End If
                    strInsertService = strInsertService & objHtl.NbrOfRoom & "," & .Rows(i)("Qty") / objHtl.NbrOfRoom _
                                    & ",'" & .Rows(i)("CCurr") _
                                    & "'," & decRatePerRoomPerNight & "," & IIf(.Rows(i)("BookOnly") = 1, 0, 1) _
                                    & ",'" & strCityName & "','" & strCountryCode & "'," & decRatePerRoomPerNight _
                                    & "," & decRatePerRoomPerNight & ",'" & pobjTvcs.GetDefaultRS4LocalHtl(.Rows(i)("CustID")) _
                                    & "','" & pobjTvcs.GetDefaultMS4LocalHtl(.Rows(i)("CustID")) & "'," & intTravelId _
                                    & ",'" & "DUTOANID" & .Rows(i)("DuToanID") _
                                    & "'," & pobjTvcs.GetHarpKeyFromAllSources(strHotelName, strCityCode) _
                                    & ",'HRP',0,'" & Replace(.Rows(i)("Rmk1"), "'", "*") & "','" _
                                    & Replace(Mid(.Rows(i)("Brief"), 1, 256), "'", "*") _
                                    & "'," & 0 & "," & .Rows(i)("RecId") & "," & 0 & ")"

                Else
                    strInsertService = "Insert into GO_MiscSvc (TravelID, SvcType, DocNbr, SRV, Currency" _
                        & ", Fare, RefFare, LowestFare " _
                        & ", CommIndicator, TrxFeeApplied, Comm, TotalTax, TrxFee" _
                        & ", ProductCode, OriDocNbr, SupplierCode, SupplierLocalName" _
                        & ", Brief,Tcode,ItemId,TcodeId) values (" _
                        & intTravelId & ",'" & strSvcCode & "','" & "DUTOANID" & .Rows(i)("DuToanID") & "','S','VND'," _
                        & .Rows(i)("TTLToPax") & "," & .Rows(i)("TTLToPax") & "," & .Rows(i)("TTLToPax") _
                        & ",'N','Y',0,0,0,'M','','" & strSupplierCode _
                        & "','" & Mid(ConvertMiscSupplierCode(.Rows(i)("Vendor")), 1, 25) _
                        & "','" & .Rows(i)("Brief") & "','" & .Rows(i)("TCode") & "'," & tblNonAir.Rows(i)("RecId") & "," & intDutoanID & ")"
                End If

                If Not pobjTvcs.ExecuteNonQuerry(strInsertService) Then
                    MsgBox(pobjTvcs.UpdtErr)
                    Return False
                End If
            Next
        End With
    End Function
    Public Function GetSvcCodeBySvcName(strSvcName As String) As String
        Select Case strSvcName
            Case "Accommodations"
                Return "HTL"
            Case "Transfer"
                Return "CAR"
            Case "Visa"
                Return "VISA"
            Case "TransViet SVC Fee"
                Return "SF"
            Case Else
                Return "MIS"
        End Select
    End Function
    Private Function GetMiscSupplierCode(strSupplierName As String) As String

        Select Case strSupplierName
            Case "CQLXNC", "DAI SU QUAN", "LANH SU QUAN", "PQLXNC"
                strSupplierName = "TRANSVIET"
        End Select

        Return pobjTvcs.GetScalarAsString("Select CwtCode from [GO_MiscSupplier] where status='OK' and Producttype='Visa'" _
                                          & " and SupplierName='" & strSupplierName & "'")
    End Function
    Private Function ConvertMiscSupplierCode(strSupplierName As String) As String

        Select Case strSupplierName
            Case "CQLXNC", "DAI SU QUAN", "LANH SU QUAN", "PQLXNC"
                Return "TRANSVIET"
            Case Else
                Return strSupplierName
        End Select
    End Function
    Public Function GetSvcFee4Visa(intDutoanId As Integer) As Decimal
        Dim decSvcFee As Decimal
        decSvcFee = pobjTvcs.GetScalarAsDecimal("Select Top 1 [TTLToPax] from RAS12.dbo.DuToan_Item where status='OK'" _
                                                & " and Service='TransViet SVC Fee' and Brief like '%visa%'" _
                                                & " and DutoanId=" & intDutoanId)
        If decSvcFee = 0 Then
            decSvcFee = pobjTvcs.GetScalarAsDecimal("Select Top 1 [TTLToPax] from RAS12.dbo.DuToan_Item where status='OK'" _
                                                & " and Service='TransViet SVC Fee'" _
                                                & " and DutoanId=" & intDutoanId)
        End If
        Return decSvcFee
    End Function
    Public Function FillAhc(dteFromDate As Date, dteToDate As Date, Optional intCustId As Integer = 0) As Boolean
        'KHONG AP DUNG CHO REED MACKAY
        Dim tblNonAir As System.Data.DataTable
        Dim arrInsertGoTravel(0 To 0) As String
        Dim strInsertGoMiscSvc As String
        Dim i As Integer
        Dim strSQL As String
        Dim strDeleteAdh As String = "Delete GO_MiscSvc where SvcType='AHC' and Travelid not in" _
                                     & "(Select RecId from go_travel where Status='ok')"
        Dim strCustFilter As String = "" ' "(select IntVal from RAS12.dbo.MISC where cat='CustNameInGroup'" _
        '& " and VAL='SEND AHC TO CWT' and Status='OK')"
        Dim strAvoidDup As String = " and t.Recid not in " _
                                    & "(Select ItemId from GO_MiscSvc where SvcType='AHC')"

        strSQL = "Select t.RecId, t.Status,r.Custid,t.FareBasis as Rmk1,'SGN' as Location ,t.PaxName" _
                & ",DOI,t.RcpNo, DOI,substring(t.Rmk,9,50) as Booker,t.Charge_D,t.Dependent" _
                & ", 'AHC'+ Cast(t.Recid as varchar) as RLOC,Tkno,c.TMC" _
                & " from RAS12.dbo.tKT t" _
                & " left join RAS12.dbo.Rcp r on t.RcpId=r.Recid" _
                & " left join cwt.dbo.GO_CompanyInfo1 c on r.CustID=c.CustID  and c.Status='OK'" _
                & " where t.Status<>'XX' and DocType ='AHC' and Substring(t.RMK,1,4)='BIZT'" _
                & " and t.doi between '" _
                & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) _
                & "' and r.Status='OK'" & strAvoidDup & " and c.TMC<>'RM'"
        If intCustId > 0 Then
            strSQL = strSQL & " and r.CustId=" & intCustId
        End If
        strSQL = strSQL & " order by t.Recid"

        pobjTvcs.ExecuteNonQuerry(strDeleteAdh)


        tblNonAir = pobjTvcs.GetDataTable(strSQL)
        With tblNonAir
            For i = 0 To tblNonAir.Rows.Count - 1
                Dim objRcp As New clsRasRcp
                Dim intTravelId As Integer
                Dim strSupplierCode As String = "SIAVN"
                Dim strSvcFee As String = GetChargeFromChargeDetails("TVN-OTHER:", .Rows(i)("Charge_D"))

                If strSvcFee = "" Then
                    MsgBox("Không lấy được " & tblNonAir.Rows(i)("Tkno") & " do nhập thiếu Service fee")
                    Continue For
                End If
                arrInsertGoTravel(0) = "Insert into GO_Travel (Status,CustId, CMC,TravellerName,EmplID" _
                        & ",TripPurpose,BkgMethod,BkgTool,BillingSupplier" _
                        & ",CorpType,BkgDate,DOI,InvNo,InvDate,RLOC,Exportable) values ('OK'," _
                        & .Rows(i)("CustId") & ",'" _
                        & pobjTvcs.GetCmcByCustId(.Rows(i)("CustId")) _
                        & "','" & .Rows(i)("PaxName") _
                        & "','" & pobjTvcs.GetEmpId(.Rows(i)("CustId"), .Rows(i)("PaxName")) _
                        & "','B','M','MAN','DSU','" & .Rows(i)("TMC") & "','" & DateTime2Text(.Rows(i)("doi")) _
                        & "','" & DateTime2Text(.Rows(i)("doi")) _
                        & "','" & .Rows(i)("RCPNO").ToString.Substring(2, 4) _
                        & .Rows(i)("RCPNO").ToString.Substring(7) _
                        & "','" & DateTime2Text(.Rows(i)("DOI")) & "','" & .Rows(i)("RLOC") _
                        & "','False')"

                If Not pobjTvcs.Update(arrInsertGoTravel) Then
                    MsgBox(pobjTvcs.UpdtErr)
                    Return False
                Else
                    intTravelId = pobjTvcs.LastInsertedId
                End If


                strInsertGoMiscSvc = "Insert into GO_MiscSvc (TravelID, SvcType, DocNbr, SRV" _
                        & ", Currency, Fare, RefFare, LowestFare " _
                        & ", CommIndicator, TrxFeeApplied, Comm, TotalTax, TrxFee,TrxCur" _
                        & ", ProductCode, OriDocNbr, SupplierCode, SupplierLocalName, Brief,Tcode,ItemId) values (" _
                        & intTravelId & ",'AHC','" & .Rows(i)("Rloc") & "','S','VND" _
                        & "',0,0,0,'N','Y',0,0," & CDec(Mid(strSvcFee, 4)) & ",'" & Mid(strSvcFee, 1, 3) _
                        & "','M','','" & strSupplierCode & "','" _
                        & "','" & .Rows(i)("RMK1") & "','" & .Rows(i)("Dependent") & "'," _
                        & .Rows(i)("RecId") & ")"

                If Not pobjTvcs.ExecuteNonQuerry(strInsertGoMiscSvc) Then
                    MsgBox(pobjTvcs.UpdtErr)
                    Return False
                End If
            Next
        End With

        Return True

    End Function
    Public Function FillAtk(dteFromDate As Date, dteToDate As Date, Optional intCustId As Integer = 0) As Boolean
        Dim tblNonAir As System.Data.DataTable
        Dim arrInsertGoTravel(0 To 0) As String
        Dim strInsertGoMiscSvc As String
        Dim i As Integer
        Dim strSQL As String
        Dim strCustFilter As String = "(59913)" 'Novartis & Abbott RO only
        Dim strAvoidDup As String = " and 'ATK'+ Cast(t.Recid as varchar) not in " _
                                    & "(Select distinct RLOC from cwt.dbo.go_travel" _
                                    & " where Status='OK' and CustId in " & strCustFilter & ")"

        strSQL = "Select t.RecId, t.Status,Custid,t.FareBasis as Rmk1,'SGN' as Location ,t.PaxName" _
                & ",DOI,t.RcpNo, DOI,substring(t.Rmk,9,50) as Booker,t.Charge_D,t.Dependent" _
                & ", 'ATK'+ Cast(t.Recid as varchar) as RLOC" _
                & " from RAS12.dbo.tKT t" _
                & " left join RAS12.dbo.Rcp r on t.RcpId=r.Recid" _
                & " where t.Status<>'XX' and DocType ='ATK' and Substring(t.RMK,1,4)='BIZT'" _
                & " and t.doi between '" _
                & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) _
                & "' and r.Status='OK' and r.custID in " & strCustFilter _
                & strAvoidDup
        If intCustId > 0 Then
            strSQL = strSQL & " and r.CustId=" & intCustId
        End If
        strSQL = strSQL & " order by t.Recid"


        tblNonAir = pobjTvcs.GetDataTable(strSQL)
        With tblNonAir
            For i = 0 To tblNonAir.Rows.Count - 1
                Dim objRcp As New clsRasRcp
                Dim intTravelId As Integer
                Dim strSupplierCode As String = "SIAVN"
                Dim strSvcFee As String = GetChargeFromChargeDetails("TVN-OTHER:", .Rows(i)("Charge_D"))

                arrInsertGoTravel(0) = "Insert into GO_Travel (Status,CustId, CMC,TravellerName,EmplID" _
                        & ",TripPurpose,BkgMethod,BkgTool,BillingSupplier" _
                        & ",CorpType,BkgDate,DOI,InvNo,InvDate,RLOC,Exportable) values ('OK'," _
                        & .Rows(i)("CustId") & ",'" _
                        & pobjTvcs.GetCmcByCustId(.Rows(i)("CustId")) _
                        & "','" & .Rows(i)("PaxName") _
                        & "','" & pobjTvcs.GetEmpId(.Rows(i)("CustId"), .Rows(i)("PaxName")) _
                        & "','B','M','MAN','DSU','CWT','" & DateTime2Text(.Rows(i)("doi")) _
                        & "','" & DateTime2Text(.Rows(i)("doi")) _
                        & "','" & .Rows(i)("RCPNO").ToString.Substring(2, 4) _
                        & .Rows(i)("RCPNO").ToString.Substring(7) _
                        & "','" & DateTime2Text(.Rows(i)("DOI")) & "','" & .Rows(i)("RLOC") _
                        & "','False')"

                If Not pobjTvcs.Update(arrInsertGoTravel) Then
                    MsgBox(pobjTvcs.UpdtErr)
                    Return False
                Else
                    intTravelId = pobjTvcs.LastInsertedId
                End If

                If strSvcFee = "" Then
                    strSvcFee = "VND0"
                End If
                strInsertGoMiscSvc = "Insert into GO_MiscSvc (TravelID, SvcType, DocNbr, SRV" _
                        & ", Currency, Fare, RefFare, LowestFare " _
                        & ", CommIndicator, TrxFeeApplied, Comm, TotalTax, TrxFee,TrxCur" _
                        & ", ProductCode, OriDocNbr, SupplierCode, SupplierLocalName, Brief,Tcode,ItemId) values (" _
                        & intTravelId & ",'ATK','" & .Rows(i)("Rloc") & "','S','VND" _
                        & "',0,0,0,'N','Y',0,0," & CDec(Mid(strSvcFee, 4)) & ",'" & Mid(strSvcFee, 1, 3) _
                        & "','M','','" & strSupplierCode & "','" _
                        & "','" & .Rows(i)("RMK1") & "','" & .Rows(i)("Dependent") & "'," _
                        & .Rows(i)("RecId") & ")"

                If Not pobjTvcs.ExecuteNonQuerry(strInsertGoMiscSvc) Then
                    MsgBox(pobjTvcs.UpdtErr)
                    Return False
                End If
            Next
        End With

        Return True

    End Function

    Public Function FillIns(dteFromDate As Date, dteToDate As Date, Optional intCustId As Integer = 0) As Boolean
        'KHONG AP DUNG CHO REED MACKAY
        Dim tblNonAir As System.Data.DataTable
        Dim arrInsertGoTravel(0 To 0) As String
        Dim strInsertGoMiscSvc As String
        Dim i As Integer
        Dim strSQL As String

        Dim strDeleteXx As String = "Delete GO_MiscSvc where SvcType='INS' and Travelid not in" _
                                     & "(Select RecId from go_travel where Status='ok')"
        'Dim strCustFilter As String = "(select IntVal from RAS12.dbo.MISC where cat='CustNameInGroup'" _
        '                            & " and VAL='SEND INS TO CWT' and Status='OK')"
        Dim strAvoidDup As String = " and t.Recid not in " _
                                    & "(Select ItemId from GO_MiscSvc where SvcType='INS')"

        strSQL = "Select t.TourCode, t.RecId, t.Status,r.Custid" _
                & ",t.FareBasis as Rmk1,'SGN' as Location ,t.PaxName" _
                & ",DOI,t.RcpNo, DOI,substring(t.Rmk,9,50) as Booker,t.Charge_D,t.Dependent" _
                & ",RLOC,c.TMC" _
                & " from RAS12.dbo.tKT t" _
                & " left join RAS12.dbo.Rcp r on t.RcpId=r.Recid" _
                & " left join cwt.dbo.GO_CompanyInfo1 c on r.CustID=c.CustID  and c.Status='OK'" _
                & " where t.Status<>'XX' and DocType ='INS' and BIZ='True'" _
                & " and t.doi between '" _
                & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) _
                & "' and r.Status='OK' and r.Counter='CWT'" _ 'and r.custID in " & strCustFilter _
                & strAvoidDup & " and c.TMC<>'RM'"
        If intCustId > 0 Then
            strSQL = strSQL & " and r.CustId=" & intCustId
        End If
        strSQL = strSQL & " order by t.Recid"

        pobjTvcs.ExecuteNonQuerry(strDeleteXx)

        tblNonAir = pobjTvcs.GetDataTable(strSQL)
        With tblNonAir
            For i = 0 To tblNonAir.Rows.Count - 1
                Dim objRcp As New clsRasRcp
                Dim intTravelId As Integer
                Dim strSupplierCode As String = "MIS"
                Dim strCdrFields As String

                Dim strSvcFee As String = GetChargeFromChargeDetails("TVN-OTHER:", .Rows(i)("Charge_D"))

                arrInsertGoTravel(0) = "Insert into GO_Travel (Status,CustId, CMC,TravellerName,EmplID" _
                        & ",TripPurpose,BkgMethod,BkgTool,BillingSupplier" _
                        & ",CorpType,BkgDate,DOI,InvNo,InvDate,RLOC,Exportable) " _
                        & " values ('OK'," & .Rows(i)("CustId") & ",'" _
                        & pobjTvcs.GetCmcByCustId(.Rows(i)("CustId")) _
                        & "','" & .Rows(i)("PaxName") _
                        & "','" & pobjTvcs.GetEmpId(.Rows(i)("CustId"), .Rows(i)("PaxName")) _
                        & "','B','M','MAN','DSU','" & .Rows(i)("TMC") _
                        & "','" & DateTime2Text(.Rows(i)("doi")) _
                        & "','" & DateTime2Text(.Rows(i)("doi")) _
                        & "','" & .Rows(i)("RCPNO").ToString.Substring(2, 4) _
                        & .Rows(i)("RCPNO").ToString.Substring(7) _
                        & "','" & DateTime2Text(.Rows(i)("DOI")) & "','" & .Rows(i)("RLOC") _
                        & "','False')"

                If Not pobjTvcs.Update(arrInsertGoTravel) Then
                    MsgBox(pobjTvcs.UpdtErr)
                    Return False
                Else
                    intTravelId = pobjTvcs.LastInsertedId
                    If .Rows(i)("TourCode") <> "" Then
                        Dim strSelectCDRs As String
                        Dim tblCDR As DataTable
                        strSelectCDRs = "select top 1 Ref1,Ref2,Ref3,Ref4,Ref5" _
                                & ",Ref6,Ref7,Ref8,Ref9,Ref10" _
                                & ",Hierachy1,Hierachy2,Hierachy3,Hierachy4,Hierachy5" _
                                & ",RequiredData" _
                                & " from cwt.dbo.go_travel" _
                                & " where status='ok' and recid=" _
                                & " (select top 1 TravelID from cwt.dbo.GO_Air " _
                                & " where Srv='S' and TKNO='" _
                                & Mid(Replace(.Rows(i)("TourCode"), " ", ""), 4) & "'" _
                                & " And Carrier=" _
                                & "(select top 1 al from RAS12.dbo.Airline where DocCode='" _
                                & Mid(Trim(.Rows(i)("TourCode")), 1, 3) & "'))" _
                                & " order by RecId desc"
                        tblCDR = pobjTvcs.GetDataTable(strSelectCDRs)

                        If tblCDR.Rows.Count > 0 Then
                            UpdateCDRsFromDatarow(intTravelId, tblCDR.Rows(0))
                        End If

                    End If
                End If

                strInsertGoMiscSvc = "Insert into GO_MiscSvc (TravelID, SvcType, DocNbr, SRV" _
                        & ", Currency, Fare, RefFare, LowestFare " _
                        & ", CommIndicator, TrxFeeApplied, Comm, TotalTax, TrxFee,TrxCur" _
                        & ", ProductCode, OriDocNbr, SupplierCode, SupplierLocalName, Brief,Tcode,ItemId) values (" _
                        & intTravelId & ",'INS','" & .Rows(i)("Rloc") & "','S','VND" _
                        & "',0,0,0,'N','Y',0,0," & CDec(Mid(strSvcFee, 4)) & ",'" & Mid(strSvcFee, 1, 3) _
                        & "','M','','" & strSupplierCode & "','" _
                        & "','" & .Rows(i)("RMK1") & "','" & .Rows(i)("Dependent") & "'," _
                        & .Rows(i)("RecId") & ")"

                If Not pobjTvcs.ExecuteNonQuerry(strInsertGoMiscSvc) Then
                    MsgBox(pobjTvcs.UpdtErr)
                    Return False
                End If
            Next
        End With

        Return True

    End Function
    Private Function xacDinhEmplID(ByVal PPaxName As String, ByVal pCustID As Integer) As String
        Dim strSQL As String
        strSQL = "select EmplID from cwt.dbo.go_employeeID where custid=" & pCustID
        strSQL = strSQL & " and Traveler='" & PPaxName & "'"
        Return pobjTvcs.GetScalarAsString(strSQL)
    End Function
    Private Function CreateMiscSupplierCode(strSupplierName As String) As Boolean
        If Not DuplicateMiscSupplierName(strSupplierName) Then
            Dim strQuerry As String = "insert into GO_MiscSupplier ([ProductType],[SupplierName],[Address]" _
            & ",[City],[Country],[CwtCode],[Status]) values ('VISA','" & strSupplierName & "','','','','','--')"
            If pobjTvcs.ExecuteNonQuerry(strQuerry) Then
                Return True
            End If
        End If
        Return True
    End Function
    Private Function DuplicateMiscSupplierName(strSupplierName As String) As Boolean
        Dim strQuerry As String = "select Recid from GO_MiscSupplier where status <>'--' and SupplierName='" & strSupplierName & "'"
        If pobjTvcs.GetScalarAsDecimal(strQuerry) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function FormatFare4GlobalOneExport(decFare As Decimal) As String
        If decFare.ToString.Length > 11 AndAlso decFare.ToString.EndsWith(".00") Then
            decFare = Mid(decFare, 1, decFare.ToString.Length - 3)
        End If
        Return decFare.ToString.PadLeft(11, "0")
    End Function
    Public Function GetRefByDataCode4RM(strDataCode As String) As Integer
        Select Case strDataCode
            Case "YR"
                Return 1
            Case "CO"
                Return 2
            Case "RE"
                Return 3
            Case "OR"
                Return 4
            Case "FV"
                Return 5
            Case "R6"
                Return 6
            Case "R7"
                Return 7
            Case "R8"
                Return 8
            Case "R9"
                Return 9
            Case "R10"
                Return 10
            Case "U99"
                Return 500
            Case Else
                Return 0
        End Select
    End Function
    Public Function UpdateCDRsFromDatarow(intTravelId As Integer, objDataRow As DataRow) As Boolean
        Dim i As Integer
        Dim strQuerry As String = "Update cwt.dbo.go_travel set "
        If objDataRow Is Nothing Then Return True
        For i = 1 To 5
            strQuerry = strQuerry & " Ref" & i & "='" & objDataRow("Ref" & i) _
                & "',Hierachy" & i & "='" & objDataRow("Hierachy" & i) & "',"
        Next
        For i = 6 To 10
            strQuerry = strQuerry & " Ref" & i & "='" & objDataRow("Ref" & i) & "',"
        Next
        strQuerry = Mid(strQuerry, 1, strQuerry.Length - 1) & " where RecId=" & intTravelId
        Return pobjTvcs.ExecuteNonQuerry(strQuerry)
    End Function
End Module
