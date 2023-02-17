Public Class frmAlIncentives
#Region "Formula"
    Private Sub tpIncentiveFormula_Enter(sender As Object, e As EventArgs) Handles tpIncentiveFormula.Enter
        ClearFormula()
        SearchFormula()
    End Sub
    Private Function ClearFormula() As Boolean
        cboCar.SelectedIndex = -1
        cboStatus.SelectedIndex = 0
        cboDateType.SelectedIndex = -1
        Return True
    End Function
    Private Function SearchFormula() As Boolean
        Dim strQuerry As String = "select * from lib.dbo.IncentiveFormula where City='" & myStaff.City & "'"
        AddEqualConditionCombo(strQuerry, cboCar)
        AddEqualConditionCombo(strQuerry, cboStatus)
        If cboDateType.SelectedIndex <> -1 Then
            AddEqualConditionCombo(strQuerry, cboDateType)
            strQuerry = strQuerry & " and '" & CreateFromDate(dtpValidOn.Value) & "' between FromDate and ToDate"
        End If
        strQuerry = strQuerry & " order by Car,FromDate,ToDate,RtgType"
        LoadDataGridView(dgrIncentiveFormula, strQuerry, Conn)
        dgrIncentiveFormula.Columns("VND").DefaultCellStyle.Format = "#.##0"
        Return True
    End Function

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        ClearFormula()
    End Sub

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmFormula As New frmIncentiveFormulaEdit(0, False)
        If frmFormula.ShowDialog = DialogResult.OK Then
            SearchFormula()
        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        If dgrIncentiveFormula.CurrentRow Is Nothing Then Exit Sub
        Dim frmFormula As New frmIncentiveFormulaEdit(dgrIncentiveFormula.CurrentRow.Cells("RecId").Value, False)
        If frmFormula.ShowDialog = DialogResult.OK Then
            SearchFormula()
        End If
    End Sub

    Private Sub lbkClone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone.LinkClicked
        If dgrIncentiveFormula.CurrentRow Is Nothing Then Exit Sub
        Dim frmFormula As New frmIncentiveFormulaEdit(dgrIncentiveFormula.CurrentRow.Cells("RecId").Value, True)
        If frmFormula.ShowDialog = DialogResult.OK Then
            SearchFormula()
        End If
    End Sub
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        SearchFormula()
    End Sub
#End Region

#Region "Routing"
    Private Sub tpIncentiveRoutings_Enter(sender As Object, e As EventArgs) Handles tpIncentiveRoutings.Enter
        ClearRtg()
        SearchRtg()
    End Sub

    Private Sub blkClearRtg_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles blkClearRtg.LinkClicked
        ClearRtg()
    End Sub
    Private Function ClearRtg() As Boolean
        cboCarRtg.SelectedIndex = -1
        cboStatusRtg.SelectedIndex = 0
        Return True
    End Function
    Private Function SearchRtg() As Boolean
        Dim strQuerry As String = "select * from lib.dbo.IncentiveRtg where Recid<>0"
        AddEqualConditionCombo(strQuerry, cboCarRtg, "Car")
        AddEqualConditionCombo(strQuerry, cboStatusRtg, "Status")

        strQuerry = strQuerry & " order by Car,RtgType"
        LoadDataGridView(dgrIncentiveRtg, strQuerry, Conn)
        Return True
    End Function

    Private Sub lbkNewRtg_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNewRtg.LinkClicked
        Dim frmRtg As New frmIncentiveRtgEdit(0)
        If frmRtg.ShowDialog = DialogResult.OK Then
            SearchRtg()
        End If
    End Sub

    Private Sub lbkEditRtg_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEditRtg.LinkClicked
        If dgrIncentiveRtg.CurrentRow Is Nothing Then Exit Sub
        Dim frmRtg As New frmIncentiveRtgEdit(dgrIncentiveRtg.CurrentRow.Cells("RecId").Value)
        If frmRtg.ShowDialog = DialogResult.OK Then
            SearchRtg()
        End If
    End Sub

    Private Sub lbkSearchRtg_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearchRtg.LinkClicked
        SearchRtg()
    End Sub

    Private Sub lbkCloneCities_vv_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCloneCities_vv.LinkClicked
        If dgrIncentiveRtg.CurrentRow Is Nothing Then Exit Sub
        Dim frmRtg As New frmIncentiveRtgEdit(dgrIncentiveRtg.CurrentRow.Cells("RecId").Value, True)
        If frmRtg.ShowDialog = DialogResult.OK Then
            SearchRtg()
        End If
    End Sub

#End Region
#Region "Report"
    Private Sub tpReport_Enter(sender As Object, e As EventArgs) Handles tpReport.Enter
        cboCounter.SelectedIndex = cboCounter.FindStringExact(myStaff.Counter)
        LoadCombo(cboCarRpt, "select distinct Car as value from lib.dbo.IncentiveFormula where City='" & myStaff.City _
                  & "' order by Car", Conn)
        cboCarRpt.SelectedIndex = -1
        dtpFromDate.Value = DateSerial(Now.Year, Now.Month, 1)
        dtpToDate.Value = Now

    End Sub

    Private Sub cboCarRpt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCarRpt.SelectedIndexChanged
        If cboCarRpt.Text <> "System.Data.DataRowView" AndAlso cboCarRpt.Text <> "" Then
            LoadCombo(cboDateTypeRpt, "select distinct DateType as value from Lib.dbo.IncentiveFormula where City='" & myStaff.City _
                      & "' and Car='" & cboCarRpt.Text & "'", Conn)
        End If
    End Sub
    Private Function ClearDeletedTkts() As Boolean
        Dim lstQuerries As New List(Of String)
        lstQuerries.Add("update lib.dbo.IncentiveTkt set status='xx' where tkid not in (select RecId from Tkt where Qty=1 and StatusAL='OK')")
        lstQuerries.Add("update lib.dbo.IncentiveSeg set Valid='false' where Valid='true' and tkid not in (select TKID from LIB.DBO.IncentiveTkt where Status='OK')")
        Return UpdateListOfQuerries(lstQuerries, Conn)
    End Function

    Private Function DefineDomRtg4Rpt() As Boolean
        Dim strQuerry As String = "select t.RecID,t.tkno,t.Itinerary" _
                                & " from tkt t" _
                                & " left join RCP r on t.RCPID=r.RecID" _
                                & " where t.qty<>0  and t.DomInt='' and t.Status<>'XX' and t.DocType='ETK'" _
                                & " and t.DOI>='01 Jan 22'" _
                                & " and r.Counter  in ('CWT','TVS') order by t.RecID "
        Dim tblTkts As DataTable = GetDataTable(strQuerry, Conn)
        Dim strDomInt As String
        For Each objRow As DataRow In tblTkts.Rows
            strDomInt = DefineDomIntRtg(pstrVnDomCities, objRow("Itinerary"))
            ExecuteNonQuerry("Update Tkt set DomInt='" & strDomInt & "' where RecId=" & objRow("RecId"), Conn)
        Next
    End Function

    Private Function PushTktFromRas(strCounter As String, strCar As String, strDateType As String, dteFrom As Date, dteTo As Date) As Boolean
        Dim strAvoidDuplicates As String = " and t.RecId not in (select TkId from lib.dbo.IncentiveTkt where Status='OK'" _
                                            & " and Car='" & strCar & "' and Counter='" & myStaff.Counter _
                                            & "' and City='" & myStaff.City & "') "
        Dim strExcludeCustomers As String = " and r.CustId not in (select intVal from Misc where Cat='CustNameInGrp' and strVal='NO INCENTIVE'"
        Dim strQuerry As String = "insert lib.dbo.IncentiveTkt (Counter,Car, CustId, CustShortName, Tkid, TKNO, DOI, DOF,DomInt, Itinerary" _
                                & ", RefundTkid, RefundItinerary, BkgClass, FareBasis,PaxType, FareDiff, ServiceFee,KickBackAmt,MiscFeeAmt" _
                                & ", FirstUser,City)" _
                                & "select r.Counter,t.al,r.CustID,r.CustShortName,t.RecID as Tkid,t.TKNO,t.DOI,t.DOF,t.DomInt,t.Itinerary" _
                                & ", isnull(ref.RecID,0) as RefundTkid,isnull(ref.Itinerary,'') as RefundItinerary" _
                                & ",t.BkgClass,t.FareBasis,t.PaxType,(t.Fare-t.NetToAL) as FareDiff,t.ChargeTV,t.KickBackAmt,t.MiscFeeAmt" _
                                & ",'" & myStaff.SICode & "','" & myStaff.City _
                                & "' from tkt t" _
                                & " left join RCP r on t.RCPID=r.RecID" _
                                & " left join tkt ref on t.TKNO=ref.TKNO and ref.Qty=-1 and ref.RecID>t.RecID and ref.Itinerary<>t.Itinerary and ref.StatusAL='ok' and not(ref.RecID is null) " _
                                & " where t.qty=1 and t.Fare<>0 and t.AL='" & strCar _
                                & "' and t.StatusAL='ok' and t.PaxType<>'INF' and t.DocType='ETK' and t.StockCtrl=''" _
                                & " and t." & strDateType & " between '" & CreateFromDate(dteFrom) & "' and '" & CreateToDate(dteTo) _
                                & "' and r.Counter='" & strCounter & "' and r.CustiD NOT " & SqlFilterCustByGroupName("NO INCENTIVE") _
                                & " and r.VendoriD " & SqlFilterVendorByGroupName(strCar & " INCENTIVE") _
                                & strAvoidDuplicates & " order by t.RecID "
        If ExecuteNonQuerry(strQuerry, Conn) Then
            Return True
        Else
            MsgBox("Không thể đổ dữ liệu từ RAS để tính Incentive!")
            Return False
        End If
    End Function
    Private Function BreakSegment() As Boolean
        Dim tblTkts As DataTable = GetDataTable("select * from lib.dbo.IncentiveTkt where BreakSeg='False' and City='" _
                                                & myStaff.City & "' order by RecId")
        Dim strRtg As String
        Dim intSegCount As Integer
        Dim strBkgCls As String
        Dim arrFareBasis As String()
        Dim i As Integer
        Dim intFbCount As Integer
        Dim lstQuerries As New List(Of String)
        Dim strSegCar As String

        For Each objRow As DataRow In tblTkts.Rows
            lstQuerries.Clear()

            If objRow("RefundTkid") = 0 Then
                strRtg = objRow("Itinerary")
            Else
                strRtg = Mid(objRow("Itinerary").ToString, 1, objRow("Itinerary").ToString.Length + 3 - objRow("RefundItinerary").ToString.Length)
            End If
            intSegCount = (strRtg.Length - 3) \ 5
            strBkgCls = objRow("BkgClass")
            For i = strBkgCls.Length + 1 To intSegCount
                strBkgCls = strBkgCls & Mid(strBkgCls, strBkgCls.Length - 1)
            Next

            arrFareBasis = Split(objRow("FareBasis"), "+")
            If objRow("PaxType") = "CHD" Then       'Bo phan Designator cho CHD 
                For i = 0 To arrFareBasis.Length - 1
                    arrFareBasis(i) = Split(arrFareBasis(i), "/")(0)
                Next
            End If
            intFbCount = arrFareBasis.Length
            ReDim Preserve arrFareBasis(intSegCount)
            For i = intFbCount + 1 To intSegCount
                arrFareBasis(i) = arrFareBasis(i - 1)
            Next

            For i = 1 To intSegCount
                strSegCar = Mid(strRtg, 5 + (i - 1) * 7, 2)
                If strSegCar <> objRow("Car") Then
                    Continue For
                ElseIf strSegCar = "//" Then
                    Continue For
                End If
                lstQuerries.Add("insert into lib.dbo.IncentiveSeg (Tkid, FromApt, ToApt, BkgClass, FareBasis) values (" _
                    & objRow("Tkid") & ",'" & Mid(strRtg, 1 + (i - 1) * 7, 3) & "','" & Mid(strRtg, 8 + (i - 1) * 7, 3) _
                    & "','" & Mid(strBkgCls, i, 1) & "','" & arrFareBasis(i - 1) & "')")
            Next
            lstQuerries.Add("update lib.dbo.IncentiveTkt Set BreakSeg='true' where RecId=" & objRow("RecId"))
            If Not UpdateListOfQuerries(lstQuerries, Conn) Then
                MsgBox("Đề nghị báo người lập trình: Không thể tách chặng bay cho vé " & objRow("TKNO"))
                Return False
            End If
        Next
        ExecuteNonQuerry("update lib.dbo.IncentiveSeg set Valid='false' where Valid='true' and FromCountry not in ('VN','')", Conn)
        Return True
    End Function
    Private Function GetCityCountryCodes() As Boolean
        Dim strQuerry As String = "select distinct f.FromApt as CheckedApt,c.City,c.Country from lib.dbo.IncentiveSeg f " _
                                & " left join lib.dbo.CityCode c on c.Airport= f.FromApt" _
                                & " where f.FromCity=''" _
                                & " union select t.ToApt as CheckedApt,c.City,c.Country from lib.dbo.IncentiveSeg t" _
                                & " left join lib.dbo.CityCode c on c.Airport= t.ToApt" _
                                & " where t.ToCity=''"

        Dim tblAirports As DataTable = GetDataTable(strQuerry)
        For Each objRow As DataRow In tblAirports.Rows
            If IsDBNull(objRow("City")) Then
                MsgBox("Đề nghị báo người lập trình: Không tìm thấy City Code cho sân bay " & objRow("CheckedApt"))
                Return False
            Else

                ExecuteNonQuerry("update lib.dbo.IncentiveSeg set FromCity='" & objRow("City") _
                                 & "',FromCountry='" & objRow("Country") _
                                 & "' where FromCity='' and FromApt='" & objRow("CheckedApt") & "'", Conn)
                ExecuteNonQuerry("update lib.dbo.IncentiveSeg set ToCity='" & objRow("City") _
                                 & "',ToCountry='" & objRow("Country") _
                                 & "' where ToCity='' and ToApt='" & objRow("CheckedApt") & "'", Conn)
            End If
        Next
        Return True
    End Function
    Private Function InvalidateSeg4IntTkt() As Boolean
        Dim lstQuerries As New List(Of String)
        lstQuerries.Add("update lib.dbo.IncentiveSeg set Valid='false' where valid='true' and ToCountry='VN'" _
                                & " and TkId in (select Tkid from lib.dbo.IncentiveTkt where DomInt='INT' and status='OK')")
        lstQuerries.Add("update lib.dbo.IncentiveSeg set Valid='false' where valid='true' and FromCountry<>'VN'" _
                                & " and TkId in (select Tkid from lib.dbo.IncentiveTkt where DomInt='INT' and status='OK')")
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            Return True
        Else
            MsgBox("Đề nghị báo người lập trình: Không đánh dấu được chặng không áp dụng cho vé quốc tế!")
            Return False
        End If
    End Function

    Private Function DefineRtgType(strCounter As String, strCar As String, strDateType As String, dteFrom As Date, dteTo As Date) As Boolean
        Dim strFilterByTkid As String = " and tkid in (select TkId from lib.dbo.IncentiveTkt where Status='OK'" _
                                            & " and Car='" & strCar _ '& "' and Counter='" & myStaff.Counter _
                                            & "' and City='" & myStaff.City & "' and " & strDateType _
                                            & " between '" & CreateFromDate(dteFrom) & "' and '" & CreateToDate(dteTo) & "')"

        Dim strQuerry As String = "Select distinct FromCity,ToCity,FromCountry,ToCountry" _
                                & " from Lib.dbo.IncentiveSeg where valid='true' and rtgtype=''" _
                                & strFilterByTkid
        Dim tblSegments As DataTable = GetDataTable(strQuerry, Conn)
        Dim strRtgType As String
        Dim strRtgTypeFilter As String
        For Each objRow As DataRow In tblSegments.Rows
            strRtgTypeFilter = "Status='OK' and Car='" & strCar _
                & "' and (FromCountries ='*' or FromCountries like '%" & objRow("FromCountry") _
                & "%') and (ToCountries='*' or ToCountries LIKE '%" & objRow("ToCountry") & "%')" _
                & " and (FromCities ='*' or FromCities like '%" & objRow("FromCity") _
                & "%') and (ToCities='*' or ToCities LIKE '%" & objRow("ToCity") & "%')"
            strRtgType = ScalarToString("lib.dbo.IncentiveRtg", "top 1 RtgType", strRtgTypeFilter)
            If strRtgType = "" Then
                MsgBox("Không xác định được RtgType cho Hãng/Từ/Đến:" & strCar _
                       & "/" & objRow("FromCity") & "/" & objRow("ToCity"))
                Return False
            Else
                ExecuteNonQuerry("Update Lib.dbo.IncentiveSeg set rtgtype='" & strRtgType _
                                 & "' where valid='true' and rtgtype='' and FromCity='" & objRow("FromCity") _
                                 & "' and ToCity='" & objRow("ToCity") _
                                 & "'" & strFilterByTkid, Conn)
            End If
        Next
        Return True
    End Function
    Private Function CalcIncentive4Seg(strCounter As String, strCar As String, strDateType As String, dteFrom As Date, dteTo As Date) As Boolean
        Dim strFilterFormula As String = " and Car='" & strCar _
                                          & "' and City='" & myStaff.City & "' and DateType='" & strDateType _
                                          & "' and FromDate <= '" & CreateToDate(dteTo) _
                                          & "' and ToDate >='" & CreateFromDate(dteFrom) & "'"

        Dim strQuerry As String = "Select * " _
                                & " from Lib.dbo.IncentiveFormula where Status='OK'" & strFilterFormula
        Dim strQuerrySegs As String
        Dim tblFormulas As DataTable = GetDataTable(strQuerry, Conn)
        Dim tblSegs As DataTable
        Dim strFilterSegByFromCountry As String = String.Empty
        Dim strFilterSegByToCountry As String = String.Empty
        Dim strFilterSegByFromCity As String = String.Empty
        Dim strFilterSegByBkgClass As String = String.Empty
        Dim strFilterSegByFareBasis As String = String.Empty

        For Each objRow As DataRow In tblFormulas.Rows
            'If objRow("FromCountries") <> "*" Then
            '    strFilterSegByFromCountry = " and s.FromCountry" & SqlConvertValues2InFilter(objRow("FromCountries"), ",")
            'End If
            'If objRow("ToCountries") <> "*" Then
            '    strFilterSegByToCountry = " and s.ToCountry" & SqlConvertValues2InFilter(objRow("ToCountries"), ",")
            'End If
            'If objRow("FromCities") <> "*" Then
            '    strFilterSegByFromCity = " and s.FromCity" & SqlConvertValues2InFilter(objRow("FromCities"), ",")
            'End If
            'If objRow("ToCiies") <> "*" Then
            '    strFilterSegByToCity = " and s.ToCity" & SqlConvertValues2InFilter(objRow("ToCities"), ",")
            'End If
            If objRow("FareBasis") <> "*" Then
                strFilterSegByFareBasis = " and s.FareBasis" & SqlConvertValues2InFilter(objRow("FareBasis"), ",")
            End If
            If objRow("BkgCls") <> "*" Then
                strFilterSegByBkgClass = " and s.BkgClass" & SqlConvertValues2InFilter(objRow("BkgCls"), ",")
            End If

            strQuerrySegs = "in (Select s.RecId from Lib.dbo.IncentiveTkt t" _
                            & " left join Lib.dbo.IncentiveSeg s On t.Tkid=s.Tkid" _
                            & " where t.Incentified=-1 And t.Status='OK' and s.Valid='true'" _
                            & " and t.Car='" & strCar & "' and t.Counter='" & strCounter _
                            & "' and t.City='" & myStaff.City & "' and t." & strDateType _
                            & " between '" & CreateFromDate(objRow("FromDate")) & "' and '" & CreateToDate(objRow("ToDate")) _
                            & "' and s.RtgType='" & objRow("RtgType") _
                            & "' " & strFilterSegByFareBasis & strFilterSegByBkgClass & ")"

            If objRow("RecId") = 33 Then
                MsgBox("")
            End If
            If Not ExecuteNonQuerry("update Lib.dbo.IncentiveSeg set IncentiveAmt=" & objRow("VND") _
                            & ", FormulaId=" & objRow("RecId") _
                             & " where Valid=1 and RecId " & strQuerrySegs, Conn) Then
                MsgBox("Đề nghị báo người lập trình: Không tính Incentive được cho FormulaId:" & objRow("RecId"))
            End If
        Next
        Return True
    End Function

    Private Function CalcIncentive4Tkt(strCounter As String, strCar As String, strDateType As String, dteFrom As Date, dteTo As Date) As Boolean
        Dim strFilterTkt As String = " and t.Counter='" & strCounter & "' and t.Car='" & strCar _
                                          & "' and t.City='" & myStaff.City & "' and t." & strDateType _
                                          & " between '" & CreateFromDate(dteFrom) & "' and '" & CreateToDate(dteTo) _
                                          & "' "
        Dim strQuerry As String = "update t set t.Incentified=1" _
                                    & ", t.IncentiveAmt=(select isnull(sum(s.incentiveamt),0)" _
                                    & " from lib.dbo.incentiveseg s where s.tkid=t.tkid And s.valid=1)" _
                                    & " from lib.dbo.incentivetkt t where T.Incentified=-1" & strFilterTkt
        If ExecuteNonQuerry(strQuerry, Conn) Then
            Return True
        Else
            MsgBox("Đề nghị báo người lập trình: Không tính Incentive được cho từng vé!")
            Return False
        End If

    End Function
    Private Function Report2Excel(strCounter As String, strCar As String, strDateType As String, dteFrom As Date, dteTo As Date) As Boolean
        Dim strFilterTkt As String = " and t.Counter='" & strCounter & "' and t.Car='" & strCar _
                                          & "' and t.City='" & myStaff.City & "' and t." & strDateType _
                                          & " between '" & CreateFromDate(dteFrom) & "' and '" & CreateToDate(dteTo) _
                                          & "' "
        Dim strQuerry As String = "select *" _
                                   & " from lib.dbo.incentivetkt t where T.Incentified=1" & strFilterTkt
        Dim tblTkt As DataTable = GetDataTable(strQuerry)
        Dim lstDateColumns As New List(Of String)

        lstDateColumns.Add("H")
        lstDateColumns.Add("I")

        Return Table2ExcelCts(tblTkt,, "QRS", lstDateColumns)

    End Function
    Private Sub lbkRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkReport2Excel.LinkClicked
        Dim dteStart As Date = Now
        If Not CheckInputValues4Report() Then Exit Sub

        ClearDeletedTkts()
        DefineDomRtg4Rpt()
        If Not PushTktFromRas(cboCounter.Text, cboCarRpt.Text, cboDateTypeRpt.Text, dtpFromDate.Value, dtpToDate.Value) Then
            Exit Sub
        ElseIf Not BreakSegment() Then
            Exit Sub
        ElseIf Not GetCityCountryCodes() Then
            Exit Sub
        ElseIf Not InvalidateSeg4IntTkt() Then
            Exit Sub
        ElseIf Not DefineRtgType(cboCounter.Text, cboCarRpt.Text, cboDateTypeRpt.Text, dtpFromDate.Value, dtpToDate.Value) Then
            Exit Sub
        ElseIf Not CalcIncentive4Seg(cboCounter.Text, cboCarRpt.Text, cboDateTypeRpt.Text, dtpFromDate.Value, dtpToDate.Value) Then
            Exit Sub
        ElseIf Not CalcIncentive4Tkt(cboCounter.Text, cboCarRpt.Text, cboDateTypeRpt.Text, dtpFromDate.Value, dtpToDate.Value) Then
            Exit Sub
        ElseIf Report2Excel(cboCounter.Text, cboCarRpt.Text, cboDateTypeRpt.Text, dtpFromDate.Value, dtpToDate.Value) Then
            MsgBox("Completed in " & DateDiff(DateInterval.Second, dteStart, Now) & " seconds")
        Else
            MsgBox("Không ra được báo cáo Incentive")
        End If
    End Sub
    Private Function CheckInputValues4Report() As Boolean
        If Not CheckFormatComboBox(cboCarRpt,, 2, 2) Then
            Return False
        ElseIf Not CheckFormatComboBox(cboDateTypeRpt,, 3, 3) Then
            Return False
        End If

        Return True
    End Function


#End Region 'Report
End Class