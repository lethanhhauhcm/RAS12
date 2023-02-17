Imports Microsoft.Office.Interop

Public Class frmExportFriesland
    Private mstrFilePath As String = "d:\CWT_FrieslandVN" & Format(Now, "yyMMdd") & ".csv"
    Private mlstErrors As New List(Of clsDataError)
    Private mobjMxpFile As System.IO.StreamWriter
    Const strCwtAddressTaxCode As String = ";TransViet Travel Co. Ltd;Ho Chi Minh City;;170-172 Nam Ky Khoi Nghia Str. Ward Vo Thi Sau;;;0301069809"
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim objFileInfo As New IO.FileInfo(mstrFilePath)
        mlstErrors.Clear()

        If objFileInfo.Exists Then
            objFileInfo.Delete()
        End If

        mobjMxpFile = New IO.StreamWriter(mstrFilePath, True)
        ExportAir()

        ExportHotel()
        ExportCar()
        ExportVisa()
        'ExportMiscAir() VISA
        'If Not ExportMiscNonAir() Then Exit Sub

        mobjMxpFile.Close()
        If mlstErrors.Count = 0 Then
            MsgBox("Export Done!" & vbNewLine & mstrFilePath)
        Else
            Error2Excel()
            MsgBox("Error found!")
        End If

        Me.Dispose()
    End Sub
    Private Function Error2Excel() As Boolean
        Dim objExcel As New Excel.Application
        Dim objWbk As Excel.Workbook
        Dim objWsh As Excel.Worksheet
        Dim i As Integer = 2

        objWbk = objExcel.Workbooks.Add
        objWsh = objWbk.ActiveSheet
        With objWsh
            .Range("A1").Value = ""
            .Range("B1").Value = ""
            .Range("C1").Value = ""
            .Range("D1").Value = ""
            .Range("E1").Value = ""

            For Each objDataErr As clsDataError In mlstErrors
                .Range("A" & i).Value = objDataErr.RecordId
                .Range("B" & i).Value = objDataErr.RecordNumber
                .Range("C" & i).Value = objDataErr.ProductDesc
                .Range("D" & i).Value = objDataErr.DataName
                .Range("E" & i).Value = objDataErr.DataError
            Next
            objExcel.Visible = True

        End With
    End Function
    Private Function ExportAir() As Boolean
        Dim strEndDate As String
        Dim strStartDate As String
        Dim tblData As DataTable
        Dim strQuerry As String
        'mobjMxpFile = New System.IO.StreamWriter("d:\CWT_FrieslandVN" & Format(Now, "yyMMdd") & ".csv", True)

        strStartDate = Format(dtpFromDate.Value.Date, "dd-MMM-yy") & " 00:00:00"
        strEndDate = Format(dtpToDate.Value.Date, "dd-MMM-yy") & " 23:59:00"


        strQuerry = "select t.Srv,t.Tkno,t.PaxName, t.DOI,t.Itinerary,'' as LocalEmployeeId,t.Currency" _
                    & ",(t.Fare+t.Tax)*qty+t.Charge+t.ChargeTV as BillAmt" _
                    & ",t.DOF,Right(a.DepDates,5) as LastDOF" _
                    & ",'' as LocalCostCenter,'' as TripDescription" _
                    & ", g.RequiredData, g.RecId as TravelId,t.DocType,t.TourCode" _
                    & " from ras12.dbo.tkt t " _
                    & " left join Ras12.dbo.Rcp r on t.RcpId=r.RecId" _
                    & " left join Go_air a on t.RecId=a.TkId And t.Status<>'XX'" _
                    & " left join Go_Travel g on a.TravelId=g.Recid and g.Status='OK'" _
                    & " where (t.DOI between '" & strStartDate _
                    & "' and '" & strEndDate _
                    & "') and t.Qty<>0 and t.Status<>'XX'" _
                    & " and r.CustId in (89440,89439) and r.Status='OK'" _
                    & " and Biz='true'"
        '& " and Biz='true'"

        tblData = pobjTvcs.GetDataTable(strQuerry)

        If tblData.Rows.Count = 0 Then
            MsgBox("No ticket for selected period")
            Return False
        ElseIf Not ValidateAir(tblData) Then
            Return False
        End If
        Return WriteAir(tblData)

    End Function
    Private Function ExportCar() As Boolean
        Dim strEndDate As String
        Dim strStartDate As String
        Dim tblData As DataTable
        Dim strQuerry As String
        Dim strFilterBySvc As String = " and i.DutoanId in (select dutoanId from ras12.dbo.dutoan_item" _
                                        & " where Status='ok' and i.Service='Transfer') "
        Dim strExcludeSF As String = " and i.Service not in ('TransViet SVC Fee') and i.RecId=i.RelatedItem "
        Dim strNoMXP As String = " and t.RecId NOT in (Select intVal from ras12.dbo.Misc where Cat='NoMxp'" _
                                    & " And Status='OK') "

        'mobjMxpFile = New System.IO.StreamWriter("d:\Fcv.csv", True)

        strStartDate = Format(dtpFromDate.Value.Date, "dd-MMM-yy") & " 00:00:00"
        strEndDate = Format(dtpToDate.Value.Date, "dd-MMM-yy") & " 23:59:00"

        strQuerry = "select i.*,t.Contact,t.LstUpdate as DOI, s1.Fvalue as LocalEmployeeId, t.Tcode" _
                    & ", (case when i.TtlToPax <0 then 'R' else 'S' end) as SRV " _
                    & ", (Select sum(TtlToPax) from ras12.dbo.Dutoan_Item where Status='OK' and RelatedItem=i.RecId and RecId<>RelatedItem) as SF " _
                    & ",s2.Fvalue as LocalCostCenter, s3.Fvalue as TripDescription,i.Brief" _
                    & " from ras12.dbo.dutoan_item i " _
                    & " left join Ras12.dbo.Dutoan_Tour t on t.RecId=i.DutoanId" _
                    & " left join Sir s1 on s1.Rcpid=t.RecId and s1.Status='OK' and s1.FName='LOCAL EMPLOYEE ID'" _
                    & " left join Sir s2 on s2.Rcpid=t.RecId and s2.Status='OK' and s2.FName='LOCAL COST CENTER'" _
                    & " left join Sir s3 on s3.Rcpid=t.RecId and s3.Status='OK' and s3.FName='TRIP DESCRIPTION'" _
                    & " where (t.LstUpdate between '" & strStartDate _
                    & "' and '" & strEndDate _
                    & "') and  t.Status='RR'" _
                    & " and t.CustId  in (89440,89439)  and i.Status='OK'" _
                    & strFilterBySvc & strExcludeSF & strNoMXP _
                    & " and t.Contact<>'zpersonal' and i.BookOnly=0 and i.CostOnly=0 and i.TtlToPax<>0" _
                    & " order by t.RecId"


        tblData = pobjTvcs.GetDataTable(strQuerry)

        If tblData.Rows.Count = 0 Then
            MsgBox("No Car for selected period")
            Return False
        ElseIf Not ValidateHotel(tblData) Then
            Return False
        End If
        WriteCar(tblData)

    End Function
    Private Function ExportVisa() As Boolean
        Dim strEndDate As String
        Dim strStartDate As String
        Dim tblData As DataTable
        Dim strQuerry As String
        Dim strFilterBySvc As String = " and i.DutoanId in (select dutoanId from ras12.dbo.dutoan_item" _
                                        & " where Status='ok' and i.Service='Visa') "
        Dim strExcludeSF As String = " and i.Service not in ('TransViet SVC Fee') and i.RecId=i.RelatedItem "
        Dim strNoMXP As String = " and t.RecId NOT in (Select intVal from ras12.dbo.Misc where Cat='NoMxp'" _
                                    & " And Status='OK') "

        'mobjMxpFile = New System.IO.StreamWriter("d:\Fcv.csv", True)

        strStartDate = Format(dtpFromDate.Value.Date, "dd-MMM-yy") & " 00:00:00"
        strEndDate = Format(dtpToDate.Value.Date, "dd-MMM-yy") & " 23:59:00"


        strQuerry = "select i.*,t.Contact,t.LstUpdate as DOI, s1.Fvalue as LocalEmployeeId, t.Tcode" _
                    & ", (case when i.TtlToPax <0 then 'R' else 'S' end) as SRV " _
                    & ", (Select sum(TtlToPax) from ras12.dbo.Dutoan_Item where Status='OK' and RelatedItem=i.RecId and RecId<>RelatedItem) as SF " _
                    & ",s2.Fvalue as LocalCostCenter, s3.Fvalue as TripDescription,i.Brief" _
                    & " from ras12.dbo.dutoan_item i " _
                    & " left join Ras12.dbo.Dutoan_Tour t on t.RecId=i.DutoanId" _
                    & " left join Sir s1 on s1.Rcpid=t.RecId and s1.Status='OK' and s1.FName='LOCAL EMPLOYEE ID'" _
                    & " left join Sir s2 on s2.Rcpid=t.RecId and s2.Status='OK' and s2.FName='LOCAL COST CENTER'" _
                    & " left join Sir s3 on s3.Rcpid=t.RecId and s3.Status='OK' and s3.FName='TRIP DESCRIPTION'" _
                    & " where (t.LstUpdate between '" & strStartDate _
                    & "' and '" & strEndDate _
                    & "') and  t.Status='RR'" _
                    & " and t.CustId  in (89440,89439)  and i.Status='OK'" _
                    & strFilterBySvc & strExcludeSF & strNoMXP _
                    & " and t.Contact<>'zpersonal' and i.BookOnly=0 and i.CostOnly=0 and i.TtlToPax<>0" _
                    & " order by t.RecId"

        tblData = pobjTvcs.GetDataTable(strQuerry)

        If tblData.Rows.Count = 0 Then
            MsgBox("No Visa for selected period")
            Return False
        ElseIf Not ValidateHotel(tblData) Then
            Return False
        End If
        WriteHotel(tblData)

    End Function
    Private Function ExportHotel() As Boolean
        Dim strEndDate As String
        Dim strStartDate As String
        Dim tblData As DataTable
        Dim strQuerry As String
        Dim strFilterBySvc As String = " and i.DutoanId in (select dutoanId from ras12.dbo.dutoan_item" _
                                        & " where Status='ok' and i.Service='Accommodations') "
        Dim strExcludeSF As String = " and i.Service not in ('TransViet SVC Fee') and i.RecId=i.RelatedItem "
        Dim strNoMXP As String = " and t.RecId NOT in (Select intVal from ras12.dbo.Misc where Cat='NoMxp'" _
                                    & " And Status='OK') "
        'mobjMxpFile = New System.IO.StreamWriter("d:\Fcv.csv", True)

        strStartDate = Format(dtpFromDate.Value.Date, "dd-MMM-yy") & " 00:00:00"
        strEndDate = Format(dtpToDate.Value.Date, "dd-MMM-yy") & " 23:59:00"


        strQuerry = "select i.*,t.Contact,t.LstUpdate as DOI, s1.Fvalue as LocalEmployeeId, t.Tcode" _
                    & ", (case when i.TtlToPax <0 then 'R' else 'S' end) as SRV " _
                    & ", (Select sum(TtlToPax) from ras12.dbo.Dutoan_Item where Status='OK' and RelatedItem=i.RecId and RecId<>RelatedItem) as SF " _
                    & ",s2.Fvalue as LocalCostCenter, s3.Fvalue as TripDescription,i.Brief" _
                    & " from ras12.dbo.dutoan_item i " _
                    & " left join Ras12.dbo.Dutoan_Tour t on t.RecId=i.DutoanId" _
                    & " left join Sir s1 on s1.Rcpid=t.RecId and s1.Status='OK' and s1.FName='LOCAL EMPLOYEE ID'" _
                    & " left join Sir s2 on s2.Rcpid=t.RecId and s2.Status='OK' and s2.FName='LOCAL COST CENTER'" _
                    & " left join Sir s3 on s3.Rcpid=t.RecId and s3.Status='OK' and s3.FName='TRIP DESCRIPTION'" _
                    & " where (t.LstUpdate between '" & strStartDate _
                    & "' and '" & strEndDate _
                    & "') and  t.Status='RR'" _
                    & " and t.CustId  in (89440,89439)  and i.Status='OK'" _
                    & strFilterBySvc & strExcludeSF & strNoMXP _
                    & " and t.Contact<>'zpersonal' and i.BookOnly=0 and i.CostOnly=0 and i.TtlToPax<>0" _
                    & " order by t.RecId"

        tblData = pobjTvcs.GetDataTable(strQuerry)

        If tblData.Rows.Count = 0 Then
            MsgBox("No Hotel for selected period")
            Return False
        ElseIf Not ValidateHotel(tblData) Then
            Return False
        End If
        WriteHotel(tblData)

    End Function
    Private Function ExportMiscAir() As Boolean
        Dim strEndDate As String
        Dim strStartDate As String
        Dim tblData As DataTable
        Dim strQuerry As String
        'mobjMxpFile = New System.IO.StreamWriter("d:\Fcv.csv", True)

        strStartDate = Format(dtpFromDate.Value.Date, "dd-MMM-yy") & " 00:00:00"
        strEndDate = Format(dtpToDate.Value.Date, "dd-MMM-yy") & " 23:59:00"


        strQuerry = "select t.Srv,t.Tkno,t.PaxName, t.DOI,t.Itinerary,g.Ref6 as EmployeeId,t.Currency,t.FareBasis,t.DocType" _
                    & ",(t.Fare+t.Tax)*qty+t.Charge+t.ChargeTV as BillAmt" _
                    & ",g.ref5 as CostCenter,'' as TravelRequest,'' as RequestNumber" _
                    & " from ras12.dbo.tkt t " _
                    & " left join Ras12.dbo.Rcp r on t.RcpId=r.RecId" _
                    & " left join Go_air a on t.RecId=a.TkId And t.Status='OK'" _
                    & " left join Go_Travel g on a.TravelId=g.Recid and g.Status='OK'" _
                    & " where (t.DOI between '" & strStartDate _
                    & "' and '" & strEndDate _
                    & "') and t.Qty<>0 and t.Status<>'XX'" _
                    & " and r.CustId  in (89440,89439)  and r.Status='OK'" _
                    & " and t.DocType <>'ETK' and t.Booker<>'zpersonal'"

        tblData = pobjTvcs.GetDataTable(strQuerry)

        If tblData.Rows.Count = 0 Then
            MsgBox("No Air Misc Service for selected period")
            Return False
            'ElseIf Not ValidateAir(tblData) Then
            '    Return False
        End If
        WriteMiscAir(tblData)

    End Function
    Private Function ExportMiscNonAir() As Boolean
        Dim strEndDate As String
        Dim strStartDate As String
        Dim tblData As DataTable
        Dim strQuerry As String
        'mobjMxpFile = New System.IO.StreamWriter("d:\Fcv.csv", True)

        strStartDate = Format(dtpFromDate.Value.Date, "dd-MMM-yy") & " 00:00:00"
        strEndDate = Format(dtpToDate.Value.Date, "dd-MMM-yy") & " 23:59:00"


        strQuerry = "select i.*,t.Contact,t.LstUpdate as DOI, s3.Fvalue EmployeeId, t.Tcode" _
                    & ", (case when i.TtlToPax <0 then 'R' else 'S' end) as SRV " _
                    & ", (Select sum(TtlToPax) from ras12.dbo.Dutoan_Item where Status='OK' and RelatedItem=i.RecId and RecId<>RelatedItem) as SF " _
                    & ",s4.Fvalue as CostCenter,s1.Fvalue as TravelRequest,s2.Fvalue as RequestNumber" _
                    & " from ras12.dbo.dutoan_item i " _
                    & " left join Ras12.dbo.Dutoan_Tour t on t.RecId=i.DutoanId" _
                    & " left join Sir s3 on s3.Rcpid=t.RecId and s3.Status='OK' and s3.FName='GLOBAL ID NUMBER'" _
                    & " left join Sir s4 on s4.Rcpid=t.RecId and s4.Status='OK' and s4.FName='COST CENTRE'" _
                    & " left join Sir s1 on s1.Rcpid=t.RecId and s1.Status='OK' and s1.FName='TRAVEL REQUEST'" _
                    & " left join Sir s2 on s2.Rcpid=t.RecId and s2.Status='OK' and s2.FName='REQUEST NBR'" _
                    & " where (t.LstUpdate between '" & strStartDate _
                    & "' and '" & strEndDate _
                    & "') and  t.Status='RR'" _
                    & " and t.CustId  in (89440,89439) and i.Status='OK' and i.Service not in ('Transfer','Accommodations','TransViet SVC Fee','Bank Fee','Visa')" _
                    & " and t.Contact<>'zpersonal' and i.BookOnly=0 and i.CostOnly=0 and i.TtlToPax<>0"

        tblData = pobjTvcs.GetDataTable(strQuerry)

        If tblData.Rows.Count = 0 Then
            MsgBox("No Non Air Misc services for selected period")
            Return False
        ElseIf Not ValidateHotel(tblData) Then
            Return False
        End If
        WriteMiscNonAir(tblData)

    End Function
    Private Function BuildDesciption2(strSrv As String, strDocNbr As String, strPaxName As String _
                                      , strDetail As String, strCostCenter As String _
                                     , strEmployeeId As String, intServiceType As Int16 _
                                     , strTripDesc As String, strRelatedTkno As String) As String
        Dim strResult As String
        If strSrv = "S" Then
            strSrv = "Booking"
        Else
            strSrv = "Refund"
        End If

        strResult = strSrv & " " & strDocNbr & " " & strPaxName & " " & strDetail _
            & " " & strCostCenter & " " & strEmployeeId _
            & " " & intServiceType & " " & strTripDesc
        If strRelatedTkno <> "" Then
            strResult = strResult & " Related Tkno " & strRelatedTkno
        End If
        Return strResult
    End Function
    Private Function BuildDesciption(strSrv As String, strDocNbr As String, strPaxName As String, strDetail As String, strCostCenter As String _
                                     , strEmployeeId As String, intServiceType As Int16, strTrvlRequest As String, strRequestNumer As String) As String

        If strSrv = "S" Then
            strSrv = "Booking"
        Else
            strSrv = "Refund"
        End If

        Return strSrv & " " & strDocNbr & " " & strPaxName & " " & strDetail & " " & strCostCenter & " " & strEmployeeId _
            & " " & intServiceType & " " & strTrvlRequest & " (TR " & strRequestNumer & ")"
    End Function
    Private Function ValidateAir(tblData As DataTable) As Boolean
        Dim intTravelId As String
        Dim tblRelatedTkno As DataTable
        For Each objRow As DataRow In tblData.Rows
            If IsDBNull(objRow("TravelId")) Then
                MsgBox("Cần PushData2Report cho " & objRow("Tkno"))
                Return False
            End If
            Select Case objRow("DocType")
                Case "AHC", "INS"
                    intTravelId = 0

                    Dim strQuerry As String
                    strQuerry = "select top 1 t.Srv,t.Tkno,t.PaxName, t.DOI,t.Itinerary" _
                        & ",t.DOF,Right(a.DepDates,5) as LastDOF" _
                        & ",g.RequiredData, g.RecId as TravelId,t.DocType" _
                        & " from ras12.dbo.tkt t " _
                        & " left join Ras12.dbo.Rcp r on t.RcpId=r.RecId" _
                        & " left join cwt.dbo.Go_air a on t.RecId=a.TkId And t.Status='OK'" _
                        & " left join cwt.dbo.Go_Travel g on a.TravelId=g.Recid and g.Status='OK'" _
                        & " where t.qty=1 and t.DocType='ETK' and t.Tkno='" & objRow("TourCode") _
                        & "' and t.Qty<>0 and t.Status<>'XX'" _
                        & " and r.CustId in (89440,89439) and r.Status='OK'" _
                        & " and Biz='true'"

                    tblRelatedTkno = GetDataTable(strQuerry)
                    If tblRelatedTkno.Rows.Count = 0 Then
                        MsgBox("Không tìm được thông tin của số vé gắn liền với " & objRow("Tkno"))
                        Return False
                    Else
                        'Dim objEtkRow As DataRow = tblRelatedTkno.Rows(0)
                        objRow("RequiredData") = tblRelatedTkno.Rows(0)("RequiredData")
                    End If

                Case Else
                    intTravelId = objRow("TravelId")
            End Select
            If IsDBNull(objRow("RequiredData")) OrElse objRow("RequiredData") = "" Then
                Dim objErr As New clsDataError(intTravelId, objRow("SRV") & " " & objRow("Tkno"), objRow("PaxName"), "TravelRequest", "Missing all required data")
                Continue For
            Else
                objRow("LocalCostCenter") = GetRequiredDataValueByDataCode("CC", objRow("RequiredData"))
                objRow("LocalEmployeeId") = GetRequiredDataValueByDataCode("UDID30", objRow("RequiredData"))
                objRow("TripDescription") = GetRequiredDataValueByDataCode("UDID34", objRow("RequiredData"))
            End If

            If objRow("LocalCostCenter") = "" Then
                Dim objErr As New clsDataError(objRow("TravelId"), objRow("SRV") & " " & objRow("Tkno"), objRow("PaxName"), "LocalCostCenter", "Missing")
                mlstErrors.Add(objErr)
            End If
            If objRow("LocalEmployeeId") = "" Then
                Dim objErr As New clsDataError(objRow("TravelId"), objRow("SRV") & " " & objRow("Tkno"), objRow("PaxName"), "LocalEmployeeId", "Missing")
                mlstErrors.Add(objErr)
            End If
            If objRow("TripDescription") = "" Then
                Dim objErr As New clsDataError(objRow("TravelId"), objRow("SRV") & " " & objRow("Tkno"), objRow("PaxName"), "TripDescription", "Missing")
                mlstErrors.Add(objErr)
            End If
        Next
        Return True

    End Function
    Private Function ValidateHotel(tblData As DataTable) As Boolean
        For Each objRow As DataRow In tblData.Rows
            If objRow("LocalCostCenter") = "" Then
                Dim objErr As New clsDataError(objRow("TravelId"), objRow("SRV") & " " & objRow("Tcode"), objRow("PaxName"), "LocalCostCenter", "Missing")
                mlstErrors.Add(objErr)
            End If
            If objRow("LocalEmployeeId") = "" Then
                Dim objErr As New clsDataError(objRow("TravelId"), objRow("SRV") & " " & objRow("Tcode"), objRow("PaxName"), "LocalEmployeeId", "Missing")
                mlstErrors.Add(objErr)
            End If
            If objRow("TripDescription") = "" Then
                Dim objErr As New clsDataError(objRow("TravelId"), objRow("SRV") & " " & objRow("Tcode"), objRow("PaxName"), "TripDescription", "Missing")
                mlstErrors.Add(objErr)
            End If

        Next
        Return True
    End Function
    Private Function WriteAir(tblData As DataTable) As Boolean
        Dim strDesc As String = ""
        Dim strLastDOF As String = ""
        Dim strRelatedTkno As String = ""
        For Each objRow As DataRow In tblData.Rows

            If objRow("DocType") = "ETK" Then
                If objRow("SRV") = "S" Then
                    If objRow("LastDOF") <> "" Then
                        strLastDOF = AddFutureYear(objRow("DOF"), objRow("LastDOF"))
                    Else
                        strLastDOF = ""
                    End If
                Else
                    strLastDOF = objRow("DOF")
                End If

            Else
                strRelatedTkno = objRow("TourCode")
            End If
            strDesc = BuildDesciption2(objRow("SRV"), objRow("Tkno"), objRow("PaxName"), objRow("Itinerary") _
                              & " " & objRow("DOF") & " " & strLastDOF, objRow("LocalCostCenter"), objRow("LocalEmployeeId"), 36 _
                              , objRow("TripDescription"), strRelatedTkno)

            mobjMxpFile.WriteLine("CWT;676;" & objRow("LocalEmployeeId") & ";" & Format(objRow("DOI"), "dd/MM/yyyy") _
                              & ";36;VN;1;" & Format(objRow("BillAmt"), "##0") & ";" & objRow("Currency") _
                              & ";" & Format(objRow("BillAmt"), "##0") & ";" & objRow("Currency") _
                              & ";" & strDesc & strCwtAddressTaxCode)
        Next
        Return True
    End Function
    Private Function WriteCar(tblData As DataTable) As Boolean
        Dim strDetail As String
        For Each objRow As DataRow In tblData.Rows
            strDetail = Replace(objRow("Brief"), vbLf, " ")
            mobjMxpFile.WriteLine("CWT;676;" & objRow("LocalEmployeeId") & ";" & Format(objRow("DOI"), "dd/MM/yyyy") _
                                  & ";6;VN;1;" & Format(objRow("TtlToPax") + objRow("SF"), "##0") & ";VND" _
                                  & ";" & Format(objRow("TtlToPax") + objRow("SF"), "##0") & ";VND" _
                                  & ";" & BuildDesciption2(objRow("SRV"), objRow("Tcode"), objRow("PaxName"), objRow("Supplier") _
                                  & " " & strDetail, objRow("LocalCostCenter"), objRow("LocalEmployeeId"), 6 _
                                  , objRow("TripDescription"), "") & strCwtAddressTaxCode)

        Next
        Return True
    End Function
    Private Function WriteHotel(tblData As DataTable) As Boolean
        Dim arrHotelInfo As String()
        For Each objRow As DataRow In tblData.Rows
            arrHotelInfo = Split(objRow("Brief"), "_")

            mobjMxpFile.WriteLine("CWT;676;" & objRow("LocalEmployeeId") & ";" & Format(objRow("DOI"), "dd/MM/yyyy") _
                                  & ";16;VN;1;" & Format(objRow("TtlToPax") + objRow("SF"), "##0") & ";VND" _
                                  & ";" & Format(objRow("TtlToPax") + objRow("SF"), "##0") & ";VND" _
                                  & ";" & BuildDesciption2(objRow("SRV"), objRow("Tcode"), objRow("PaxName"), objRow("Supplier") _
                                  & " " & arrHotelInfo(2) & " " & arrHotelInfo(2), objRow("LocalCostCenter"), objRow("LocalEmployeeId"), 16 _
                                  , objRow("TripDescription"), "") & strCwtAddressTaxCode)
        Next
        Return True
    End Function
    Private Function WriteVisa(tblData As DataTable) As Boolean
        Dim arrHotelInfo As String()
        For Each objRow As DataRow In tblData.Rows
            arrHotelInfo = Split(objRow("Brief"), "_")

            mobjMxpFile.WriteLine("CWT;676;" & objRow("LocalEmployeeId") & ";" & Format(objRow("DOI"), "dd/MM/yyyy") _
                                  & ";11;VN;1;" & Format(objRow("TtlToPax") + objRow("SF"), "##0") & ";VND" _
                                  & ";" & Format(objRow("TtlToPax") + objRow("SF"), "##0") & ";VND" _
                                  & ";" & BuildDesciption2(objRow("SRV"), objRow("Tcode"), objRow("PaxName"), objRow("Supplier") _
                                  & " " & arrHotelInfo(2) & " " & arrHotelInfo(2), objRow("LocalCostCenter"), objRow("LocalEmployeeId"), 11 _
                                  , objRow("TripDescription"), "") & strCwtAddressTaxCode)
        Next
        Return True
    End Function
    Private Function WriteMiscAir(tblData As DataTable) As Boolean
        For Each objRow As DataRow In tblData.Rows

            mobjMxpFile.WriteLine("CWT;676;" & objRow("EmployeeId") & ";" & Format(objRow("DOI"), "dd/MM/yyyy") _
                                  & ";131;VN;1;" & Format(objRow("BillAmt"), "##0") & "," & objRow("Currency") _
                                  & ";" & Format(objRow("BillAmt"), "##0") & ";" & objRow("Currency") _
                                  & ";" & BuildDesciption(objRow("SRV"), objRow("Tkno"), objRow("PaxName"), objRow("Itinerary"), objRow("CostCenter") _
                                  , objRow("EmployeeId"), 131, objRow("TravelRequest"), objRow("RequestNumber")) & strCwtAddressTaxCode)
        Next
        Return True
    End Function
    Private Function WriteMiscNonAir(tblData As DataTable) As Boolean
        Dim strDetail As String
        For Each objRow As DataRow In tblData.Rows
            strDetail = Replace(objRow("Brief"), vbLf, " ")
            mobjMxpFile.WriteLine("CWT;676;" & objRow("EmployeeId") & ";" & Format(objRow("DOI"), "dd/MM/yyyy") _
                                  & ";131;VN;1;" & Format(objRow("TtlToPax") + objRow("SF"), "##0") & ";VND" _
                                  & ";" & Format(objRow("TtlToPax") + objRow("SF"), "##0") & ";VND" _
                                  & ";" & BuildDesciption(objRow("SRV"), objRow("Tcode"), objRow("PaxName"), objRow("Supplier") _
                                  & " " & strDetail, objRow("CostCenter"), objRow("EmployeeId"), 131 _
                                  , objRow("TravelRequest"), objRow("RequestNumber")) & strCwtAddressTaxCode)
        Next
        Return True
    End Function

    Private Sub frmExportFriesland_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dteStartOfLastWeek As DateTime = Today.AddDays(-7)
        dtpFromDate.Value = dteStartOfLastWeek.AddDays((dteStartOfLastWeek.DayOfWeek - DayOfWeek.Sunday - 1) * -1)
        dtpToDate.Value = dtpFromDate.Value.AddDays(6)

    End Sub
End Class