'20220505 modi by 7643
'20220523 modi by 7643
Imports HtmlAgilityPack
Public Class frmParseNonGDS
    Private mintCustId As Integer
    Private mstrRloc As String
    Private mlstPaxNames As New List(Of String)
    Private mlstPaxTypes As New List(Of String)
    Private mlstDOBs As New List(Of Date)
    Private mlstTknos As New List(Of String)
    Private mlstFares As New List(Of Decimal)
    Private mlstPhiKhac As New List(Of Decimal)
    Private mlstVatFares As New List(Of Decimal)
    Private mlstVatPhiKhac As New List(Of Decimal)
    Private mlstTotalTaxes As New List(Of Decimal)
    Private mlstFbs As New List(Of String)
    Private mlstBkgClss As New List(Of String)
    Private mlstFromCities As New List(Of String)
    Private mlstToCities As New List(Of String)
    Private mlstCars As New List(Of String)
    Private mlstFltDates As New List(Of String)
    Private mlstFltNbrs As New List(Of String)
    Private mlstETDs As New List(Of String)
    Private mlstETAs As New List(Of String)
    Dim mblnPageReady As Boolean
    Dim mblnCompleted As Boolean
    Private mdecGrandTotal As Decimal
    Private mdecTotalFare As Decimal
    Private mdecTotalTax As Decimal
    Private mdecTotalVAT As Decimal
    Private mdecTotalThuHo As Decimal
    Private mdecTotalPhiKhac As Decimal
    Private mdteDOI As Date
    Private mdteDOF As Date
    Private ImportVjPdf As New clsImportVJPdf  '20220505 add by 7643
    Public Sub New(strSystem As String, strFileExtension As String, intCustId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mintCustId = intCustId
        Dim ofdPnr As New OpenFileDialog
        txtCar.Text = strSystem

        With ofdPnr
            .Filter = strFileExtension.ToUpper & "file|*." & strFileExtension & "*"
            If .ShowDialog <> System.Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            '20220505 mark by 7643 -B-
            'wb.Navigate(.FileName)
            'WaitForPageLoad()
            '20220505 mark by 7643 -E-

            If strFileExtension.ToUpper = "HTM" Then
                wb.Navigate(.FileName)
                WaitForPageLoad()
                Select Case strSystem
                    Case "QH"
                        If ParseQhPnrHtm() Then
                            AdjustParsedData()
                            AdjustParsedDatqQh()
                            LoadData2GUI()
                        Else
                            MsgBox("Không đọc được QH PNR dạng " & strFileExtension)
                        End If
                    Case "VJ"
                End Select
                '20220505 add by 7643 -B-

            ElseIf strFileExtension.ToUpper = "PDF" Then
                Select Case strSystem
                    Case "VJ"
                        ImportVjPdf.FCar = txtCar.Text
                        ImportVjPdf.ofdPnr = ofdPnr
                        If ImportVjPdf.ParseVjPnrPdf() Then
                            ImportVjPdf.AdjustParsedData()
                            ImportVjPdf.AdjustParsedDataVj()
                            LoadVJData2GUI()
                        Else
                            MsgBox("Không đọc được VJ PNR dạng " & strFileExtension)
                        End If
                        wb.Navigate(.FileName)
                End Select
                '20220505 add by 7643 -E-

            End If
        End With

    End Sub

    Private Sub lbkOK_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPush2RAS.LinkClicked
        If txtTktIssuedBy.Text = 0 AndAlso myStaff.Counter = "CWT" Then  '^_^20221107 add AndAlso myStaff.Counter = "CWT" by 7643
            MsgBox("You must select TktIssuedBy!")
            Exit Sub
        End If
        Me.DialogResult = DialogResult.OK
    End Sub
    Private Function ParseQhVndAmtHtml(ByRef arrLines As String(), ByRef intStartLine As Integer) As Decimal
        Dim i As Integer
        Dim decResult As Decimal
        For i = intStartLine To arrLines.Length - 1
            If IsNumeric(arrLines(i).Trim) Then
                decResult = CDec(arrLines(i).Trim)
                intStartLine = i
                Exit For
            ElseIf arrLines(i).Contains("VND") Then
                Dim arrBreaks As String() = Split(arrLines(i), "VND")(0).Trim.Split(" ")
                decResult = CDec(arrBreaks(arrBreaks.Length - 1))
                intStartLine = i
                Exit For
            End If
        Next
        Return decResult
    End Function

    Private Function ParseQhPnrHtm() As Boolean
        Dim objDoc As New HtmlDocument
        objDoc.LoadHtml(wb.DocumentText)
        Dim i As Integer, j As Integer
        Dim blnEng As Boolean
        Dim colDivNodes As HtmlNodeCollection = objDoc.DocumentNode.SelectNodes("//div/div/div")
        Dim colPaxNameNodes As HtmlNodeCollection = objDoc.DocumentNode.SelectNodes("//table")
        Dim colSegNodes As HtmlNodeCollection = objDoc.DocumentNode.SelectNodes("//table//table//table//table")
        Dim colFareNodes As HtmlNodeCollection = objDoc.DocumentNode.SelectNodes("//table//table//table//table//table")
        Dim colTotalAmtNodes As HtmlNodeCollection = objDoc.DocumentNode.SelectNodes("//table//table//table//table")
        Dim colTotalAmtNodes2 As HtmlNodeCollection = objDoc.DocumentNode.SelectNodes("//table//table//table//table/tr/td")
        Dim colDoiNodes As HtmlNodeCollection = objDoc.DocumentNode.SelectNodes("//table//tr")
        Dim strPaxNameKey As String = "Hành khách"
        Dim strDobKey As String = "Ngày sinh"
        If wb.DocumentText.Contains("Thank you for choosing Bamboo") Then
            blnEng = True
            strPaxNameKey = "Passengers"
            strDobKey = "Date of birth"
        End If

        'Lay ten khach, ngay sinh, so ve
        For Each objHtmlNode As HtmlNode In colPaxNameNodes
            'MsgBox(objHtmlNode.InnerText)
            If objHtmlNode.InnerText.Contains(strPaxNameKey) Then
                Dim strFirstPaxName As String
                strFirstPaxName = Replace(Replace(Replace(objHtmlNode.InnerText, vbCrLf, ""), "&nbsp;", ""), strPaxNameKey, "").Trim
                mlstPaxNames.Add(strFirstPaxName)
                mlstDOBs.Add(Date.MinValue)
                Exit For
            End If
        Next
        For Each objHtmlNode As HtmlNode In colPaxNameNodes
            If objHtmlNode.InnerText.Contains(strDobKey) Then
                Dim arrLines As String() = Split(Replace(objHtmlNode.InnerText, "&nbsp;", ""), vbCrLf)
                For i = 0 To arrLines.Length - 1
                    Dim strLine As String = arrLines(i).Trim
                    'strLine = strLine.Trim
                    If strLine <> "" Then
                        If strLine.Length = 13 AndAlso IsNumeric(strLine) AndAlso Not mlstTknos.Contains(strLine) Then
                            mlstTknos.Add(strLine)
                        ElseIf Not blnEng AndAlso strLine.Contains(".") Then
                            If strLine.Split(".").Length = 2 Then
                                If strLine.Length <= 5 Then
                                    strLine = strLine & " " & arrLines(i + 1).Trim
                                End If
                                If Not mlstPaxNames.Contains(strLine) Then
                                    mlstPaxNames.Add(strLine)
                                    mlstDOBs.Add(Date.MinValue)
                                End If
                            ElseIf strLine.Split(".").Length = 3 AndAlso Not mlstDOBs.Contains(ParseQhDobHtm(strLine)) Then
                                mlstDOBs(mlstPaxNames.Count - 1) = strLine
                            End If
                            '^_^20221019 add by 7643 -b-
                        ElseIf strLine.Contains(".") Then
                            If strLine.Split(".").Length = 2 Then
                                If strLine.Length <= 5 Then
                                    strLine = strLine & " " & arrLines(i + 1).Trim
                                End If
                                If Not mlstPaxNames.Contains(strLine) AndAlso Split(strLine, ".")(1) <> "" Then
                                    mlstPaxNames.Add(strLine)
                                    mlstDOBs.Add(Date.MinValue)
                                End If
                            ElseIf strLine.Split(".").Length = 3 AndAlso Not mlstDOBs.Contains(ParseQhDobHtm(strLine)) Then
                                mlstDOBs(mlstPaxNames.Count - 1) = strLine
                            End If
                            '^_^20221019 add by 7643 -e-
                        End If
                    End If
                Next
            End If
        Next

        If mlstTknos.Count = 0 Then
            MsgBox("Không tìm thấy số vé nào!")
            Return False
        End If
        'Lay chang bay
        For Each objNode As HtmlNode In colSegNodes
            If (objNode.InnerText.Contains("Khởi") AndAlso objNode.InnerText.Contains("hành")) _
                Or objNode.InnerText.Contains("Outbound Flight") Then
                Dim arrLines As String() = Split(objNode.InnerText, vbCrLf)
                If objNode.InnerText.Contains("Số hiệu chuyến bay") _
                    Or objNode.InnerText.Contains("Flight No") Then
                    For Each strLine As String In arrLines
                        strLine = strLine.Trim
                        If strLine.Contains(",") Then
                            mlstFltDates.Add(ParseQhFltDateHtm(strLine.Split(",")(1)))
                        ElseIf strLine.Contains("(") Then
                            If mlstFromCities.Count > mlstToCities.Count Then
                                mlstToCities.Add(Mid(strLine.Split("(")(1), 1, 3))
                            Else
                                mlstFromCities.Add(Mid(strLine.Split("(")(1), 1, 3))
                            End If
                        ElseIf strLine.Contains(":") AndAlso strLine.Length = 5 Then
                            If mlstETDs.Count > mlstETAs.Count Then
                                mlstETAs.Add(strLine)
                            Else
                                mlstETDs.Add(strLine)
                            End If
                        ElseIf (strLine.Trim.Contains("Số hiệu chuyến bay:") AndAlso strLine.Trim.Length > 20) _
                            Or (strLine.Trim.Contains("Flight No:") AndAlso strLine.Trim.Length > 15) Then
                            mlstFltNbrs.Add(Mid(Split(strLine, ": ")(1), 3))
                        ElseIf strLine.StartsWith(txtCar.Text) Then
                            mlstFltNbrs.Add(Mid(strLine, 3))
                        End If
                    Next
                End If


            End If
        Next
        mdteDOF = mlstFltDates(0)


        For i = 0 To mlstTknos.Count - 1
            mlstFares.Add(0)
            mlstFbs.Add("")
            mlstBkgClss.Add("")
        Next
        'lay gia
        For Each objNode As HtmlNode In colFareNodes
            If (objNode.InnerText.Contains("Giá vé") AndAlso objNode.InnerText.Contains("Khởi hành")) OrElse
                 (objNode.InnerText.Contains("Outbound Flight") AndAlso objNode.InnerText.Contains("Fare")) Then
                Dim intPaxIndex As Integer

                For Each objSubNode As HtmlNode In objNode.ChildNodes
                    If objSubNode.Name = "tr" Then
                        If objSubNode.InnerText.Contains(".") Then
                            Dim strPaxName As String = Remove2Spaces(Replace(objSubNode.InnerText, vbCrLf, "").Trim)
                            intPaxIndex = mlstPaxNames.FindIndex(Function(value As String)
                                                                     Return value = strPaxName
                                                                 End Function)
                        ElseIf objSubNode.InnerText.Contains("VND") _
                            AndAlso Not objSubNode.InnerText.Contains("Hành lý") _
                            AndAlso Not objSubNode.InnerText.Contains("Baggage") Then
                            If intPaxIndex <> -1 Then
                                mlstFares(intPaxIndex) = mlstFares(intPaxIndex) + ParseQhVndAmtHtml(Split(objSubNode.InnerText, vbCrLf), 0)
                                Dim arrBreaks As String() = Replace(objNode.InnerText, vbCrLf, "").Trim.Split(")")
                                Dim strFareType As String = arrBreaks(0).Split("(")(1)
                                If mlstFbs(intPaxIndex) = "" Then
                                    mlstFbs(intPaxIndex) = ConverQhFareType2Fb(strFareType)
                                Else
                                    mlstFbs(intPaxIndex) = mlstFbs(intPaxIndex) & "+" & ConverQhFareType2Fb(strFareType)
                                End If

                                mlstBkgClss(intPaxIndex) = mlstBkgClss(intPaxIndex) & ConverQhFareType2BkgCls(strFareType)
                            End If

                        End If
                    End If
                Next
                'MsgBox(objNode.XPath & vbNewLine & objNode.InnerText)

            End If

            'If objNode.InnerText.Contains("Giá vé") AndAlso objNode.InnerText.Contains("(") Then
            '    'MsgBox(objNode.XPath & vbNewLine & objNode.InnerText)
            '    Dim arrBreaks As String() = Replace(objNode.InnerText, vbCrLf, "").Trim.Split(")")
            '    Dim strFareType As String = arrBreaks(0).Split("(")(1)
            '    mlstFares.Add(CDec(arrBreaks(1).Trim.Split(" ")(0)))
            '    mlstFbs.Add(ConverQhFareType2Fb(strFareType))
            '    mlstBkgClss.Add(ConverQhFareType2BkgCls(strFareType))
            'End If
        Next

        'Lat total cac khoan
        For Each objNode As HtmlNode In colDoiNodes
            If objNode.InnerText.Contains("AGENCY PAY") Then
                Dim arrLines As String() = Split(objNode.InnerText.Trim, vbCrLf)
                For Each strLine As String In arrLines
                    If strLine.Contains(".") AndAlso strLine.Split(".").Length = 3 Then
                        mdteDOI = ParseQhDobHtm(strLine)
                    End If
                Next
                'MsgBox(objNode.XPath & vbNewLine & objNode.InnerText)
            End If
        Next
        If mdteDOI = Date.MinValue Then
            MsgBox("Không tìm thấy ngày trả tiền. Tạm lấy ngày hôm nay. Đề nghị kiểm tra và chọn DOI đúng")
            mdteDOI = Now
        End If
        For Each objNode As HtmlNode In colTotalAmtNodes2
            '^_^20220825 mark by 7643 -b-
            'If (objNode.InnerText.Contains("Giá vé") AndAlso objNode.InnerText.Contains("Tổng VAT")) _
            '    Or (objNode.InnerText.Contains("Fare") AndAlso objNode.InnerText.Contains("Total VAT")) Then
            '    Dim arrLines As String() = Split(objNode.InnerText.Trim, vbCrLf)
            '^_^20220825 mark by 7643 -e-
            '^_^20220825 modi by 7643 -b-
            If (objNode.InnerText.Contains("Giá vé") AndAlso
                objNode.InnerText.Contains("Tổng VAT") Or objNode.InnerText.Contains("phụ thu") Or objNode.InnerText.Contains("Thu hộ") Or objNode.InnerText.Contains("Phí khác")) Or
               (objNode.InnerText.Contains("Fare") AndAlso
                objNode.InnerText.Contains("Total VAT") Or objNode.InnerText.Contains("surcharge") Or objNode.InnerText.Contains("Authorized collectio") Or
                objNode.InnerText.Contains("Other charges")) Then
                Dim arrLines As String() = Split(objNode.InnerText.Trim, vbCrLf)
                '^_^20220825 modi by 7643 -e-

                For i = 0 To arrLines.Length - 1
                    If arrLines(i).Contains("Giá vé") Or arrLines(i).Contains("Fare") Then
                        mdecTotalFare = mdecTotalFare + ParseQhVndAmtHtml(arrLines, i)
                    ElseIf arrLines(i).Contains("phụ thu)") Or arrLines(i).Contains("surcharge)") Then
                        mdecTotalVAT = mdecTotalVAT + ParseQhVndAmtHtml(arrLines, i)
                    ElseIf arrLines(i).Contains("Thu hộ") Or arrLines(i).Contains("Authorized collection") Then
                        mdecTotalThuHo = mdecTotalThuHo + ParseQhVndAmtHtml(arrLines, i)
                    ElseIf arrLines(i).Contains("Phí khác") Or arrLines(i).Contains("Other charges") Then
                        mdecTotalPhiKhac = mdecTotalPhiKhac + ParseQhVndAmtHtml(arrLines, i)
                    End If
                Next
            End If

        Next
        For Each objNode As HtmlNode In colTotalAmtNodes
            If objNode.InnerText.Contains("Total") Then
                If objNode.InnerText.Contains("Tổng cộng") Then
                    mdecGrandTotal = ParseVndAmtHtmlQH(Split(objNode.InnerText.Trim, "Tổng cộng")(1).Trim)
                ElseIf objNode.InnerText.Contains("Amount") Then
                    mdecGrandTotal = ParseVndAmtHtmlQH(Split(objNode.InnerText.Trim, "Amount")(1).Trim)
                End If

                For Each objSubNode As HtmlNode In objNode.ChildNodes
                    'MsgBox(objSubNode.XPath & vbNewLine & objSubNode.InnerText)
                    If objSubNode.InnerText.Contains("Giá vé") Then
                        mdecTotalFare = mdecTotalFare + ParseVndAmtHtmlQH(Split(objSubNode.InnerText.Trim, "Giá vé")(1).Trim)
                    ElseIf objSubNode.InnerText.Contains("Total amount") Then
                        mdecTotalFare = mdecTotalFare + ParseVndAmtHtmlQH(Split(objSubNode.InnerText.Trim, "Total amount")(1).Trim)
                    End If
                Next
            End If
        Next




        Return True
    End Function

    Private Function AdjustParsedData() As Boolean
        Dim intPaxCount As Integer = mlstTknos.Count
        Dim i As Integer

        If intPaxCount > mlstPaxNames.Count Then
            intPaxCount = mlstPaxNames.Count
        End If

        For i = 0 To intPaxCount - 1
            mlstVatFares.Add(0)
            mlstVatPhiKhac.Add(0)
            mlstTotalTaxes.Add(0)
            mlstPhiKhac.Add(0)
        Next

        If mlstDOBs.Count > 0 Then
            For i = 0 To mlstDOBs.Count - 1
                mlstPaxTypes.Add(GetPaxTypeByDOB(mdteDOF, mlstDOBs(i)))
            Next
        End If
        'Neu ko co PaxType thi gan gia tri ADL
        For i = mlstDOBs.Count To intPaxCount - 1
            mlstPaxTypes.Add("ADL")
        Next
    End Function
    Private Function AdjustParsedDatqQh() As Boolean
        Dim i As Integer
        Dim intVatPct As Decimal = GetVatPct(mdteDOI)
        Dim intAdlCount As Integer
        Dim intChdCount As Integer
        Dim decThuHoPerAdl As Decimal
        Dim decThuHoPerChd As Decimal
        Dim decThuHoPerInf As Decimal = 0

        Dim decPhiKhacPerAdlChd As Decimal
        Dim decPhiKhacPerAdlChdPerSeg As Decimal
        Dim decVatPhiKhacPerAdlChd As Decimal

        For i = 0 To mlstPaxTypes.Count - 1
            If mdecTotalVAT > 0 Then  '^_^20220825 add by 7643
                If mlstPaxTypes(i) = "INF" Then
                    mlstVatFares(i) = RoundUp((mlstFares(i) * intVatPct / 100), 3)
                ElseIf mlstPaxTypes(i) = "CHD" Then
                    intChdCount = intChdCount + 1
                    mlstVatFares(i) = RoundUp(RoundNearest(mlstFares(i) * intVatPct / 100), 3)
                Else
                    mlstVatFares(i) = RoundUp(RoundNearest(mlstFares(i) * intVatPct / 100), 3)
                    intAdlCount = intAdlCount + 1
                End If
                '^_^20220825 add by 7643 -b-
            Else
                If mlstPaxTypes(i) <> "INF" Then intAdlCount = intAdlCount + 1
            End If
            '^_^20220825 add by 7643 -e-
        Next

        decThuHoPerAdl = mdecTotalThuHo / (intAdlCount + (intChdCount / 2))
        decThuHoPerChd = decThuHoPerAdl / 2

        decPhiKhacPerAdlChd = Math.Round((mdecTotalPhiKhac / (intAdlCount + intChdCount)), 0)
        decPhiKhacPerAdlChdPerSeg = decPhiKhacPerAdlChd / mlstFromCities.Count
        'decVatPhiKhacPerAdlChd = RoundUp(decPhiKhacPerAdlChdPerSeg * intVatPct / 100, 3) * mlstFromCities.Count  '^_^20220825 mark by 7643
        If mdecTotalVAT > 0 Then decVatPhiKhacPerAdlChd = RoundUp(decPhiKhacPerAdlChdPerSeg * intVatPct / 100, 3) * mlstFromCities.Count  '^_^20220825 modi by 7643

        For i = 0 To mlstPaxTypes.Count - 1
            If mlstPaxTypes(i) = "INF" Then
                mlstTotalTaxes(i) = mlstVatFares(i) + decThuHoPerInf
            ElseIf mlstPaxTypes(i) = "CHD" Then
                mlstPhiKhac(i) = decPhiKhacPerAdlChd
                mlstVatPhiKhac(i) = decVatPhiKhacPerAdlChd
                mlstTotalTaxes(i) = mlstVatFares(i) + decThuHoPerChd + decVatPhiKhacPerAdlChd
            ElseIf mlstPaxTypes(i) = "ADL" Then
                mlstPhiKhac(i) = decPhiKhacPerAdlChd
                mlstVatPhiKhac(i) = decVatPhiKhacPerAdlChd
                mlstTotalTaxes(i) = mlstVatFares(i) + decThuHoPerAdl + decVatPhiKhacPerAdlChd
            End If
        Next

        Return True
    End Function
    Private Function LoadData2GUI() As Boolean
        Dim i As Integer
        Dim lstPaxTypes As New List(Of String)
        txtRloc.Text = mstrRloc
        dtpDOI.Value = mdteDOI
        dtpDOF.Value = mdteDOF
        For i = 0 To mlstFromCities.Count - 1
            dgrSegs.Rows.Add({mlstFromCities(i), mlstToCities(i), txtCar.Text, mlstFltNbrs(i), mlstFltDates(i), mlstETDs(i), mlstETAs(i)})
        Next
        For i = 0 To mlstTknos.Count - 1
            dgrTkts.Rows.Add({FormatRasTkno(mlstTknos(i)), ConverQhPaxName2Ras(mlstPaxNames(i)), mlstPaxTypes(i), mlstFbs(i), mlstBkgClss(i) _
                             , mlstFares(i) + mlstPhiKhac(i), mlstTotalTaxes(i), mlstVatFares(i) + mlstVatPhiKhac(i), 0, 0, 0, False, ""})
            If Not lstPaxTypes.Contains(mlstPaxTypes(i)) Then
                lstPaxTypes.Add(mlstPaxTypes(i))
            End If
        Next
        ReformatGridTkts()
        SumGrandTotal()

        cboPaxType.DataSource = lstPaxTypes.ToArray
        cboFieldName.SelectedIndex = 0

        LoadComboBooker(mintCustId, cboBooker)

        Return True
    End Function

    '20220505 add by 7643 -B-
    Private Function LoadVJData2GUI() As Boolean
        Dim i As Integer
        Dim lstPaxTypes As New List(Of String)
        txtRloc.Text = ImportVjPdf.FstrRloc
        dtpDOI.Value = ImportVjPdf.FdteDOI
        dtpDOF.Value = ImportVjPdf.FdteDOF
        mlstTknos = ImportVjPdf.FlstTknos
        For i = 0 To ImportVjPdf.FlstFromCities.Count - 1
            dgrSegs.Rows.Add({ImportVjPdf.FlstFromCities(i), ImportVjPdf.FlstToCities(i), ImportVjPdf.FlstCars(i), ImportVjPdf.FlstFltNbrs(i), ImportVjPdf.FlstFltDates(i), ImportVjPdf.FlstETDs(i),
                              ImportVjPdf.FlstETAs(i)})
        Next
        For i = 0 To ImportVjPdf.FlstTknos.Count - 1
            dgrTkts.Rows.Add({FormatRasTkno(ImportVjPdf.FlstTknos(i)), ImportVjPdf.FlstPaxNames(i), ImportVjPdf.FlstPaxTypes(i), ImportVjPdf.FlstFbs(i), ImportVjPdf.FlstBkgClss(i) _
                             , ImportVjPdf.FlstFares(i), ImportVjPdf.FlstTotalTaxes(i) + ImportVjPdf.FlstPhiKhac(i), ImportVjPdf.FlstVatFares(i) + ImportVjPdf.FlstVatPhiKhac(i), 0, 0, 0, False, ""})
            If Not lstPaxTypes.Contains(ImportVjPdf.FlstPaxTypes(i)) Then
                lstPaxTypes.Add(ImportVjPdf.FlstPaxTypes(i))
            End If
        Next
        ReformatGridTkts()
        SumGrandTotal()

        cboPaxType.DataSource = lstPaxTypes.ToArray
        cboFieldName.SelectedIndex = 0

        LoadComboBooker(mintCustId, cboBooker)

        Return True
    End Function
    '20220505 add by 7643 -E-

    Private Function ReformatGridTkts() As Boolean
        dgrTkts.Columns("Fare").DefaultCellStyle.Format = "#,##0"
        dgrTkts.Columns("Tax").DefaultCellStyle.Format = "#,##0"
        dgrTkts.Columns("VAT").DefaultCellStyle.Format = "@"
        dgrTkts.Columns("VAT").DefaultCellStyle.Format = "#,##0"
        dgrTkts.Columns("KickBackAmt").DefaultCellStyle.Format = "#,##0"
        dgrTkts.Columns("MiscFeeAmt").DefaultCellStyle.Format = "#,##0"
        dgrTkts.Columns("ChargeTv").DefaultCellStyle.Format = "#,##0"
        dgrTkts.Refresh()
        Return True
    End Function
    Private Function SumGrandTotal() As Boolean
        Dim i As Integer
        Dim decTotalFare As Decimal
        Dim decTotalTax As Decimal
        Dim decTotalVat As Decimal
        For i = 0 To mlstTknos.Count - 1
            decTotalFare = decTotalFare + CDec(dgrTkts.Rows(i).Cells("Fare").Value)
            decTotalTax = decTotalTax + CDec(dgrTkts.Rows(i).Cells("Tax").Value)
            decTotalVat = decTotalVat + CDec(dgrTkts.Rows(i).Cells("VAT").Value)
        Next
        txtTotalFare.Text = Format(decTotalFare, "#,#00")
        txtTotalTax.Text = Format(decTotalTax, "#,#00")
        txtTotalVat.Text = Format(decTotalVat, "#,#00")
        txtGrandTotal.Text = Format(decTotalFare + decTotalTax, "#,#00")
        Return True
    End Function
    Private Sub WaitForPageLoad(Optional intExtraWaitTime As Integer = 0)
        mblnPageReady = False
        mblnCompleted = False
        'lblStatus.Text = "Please wait"
        AddHandler wb.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        While Not mblnPageReady
            Application.DoEvents()
            If wb.DocumentText.Contains("Timeout expired") Then
                Exit While
            End If
        End While
        Threading.Thread.Sleep(1000 * intExtraWaitTime)
    End Sub
    Private Sub PageWaiter(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
        If wb.ReadyState = WebBrowserReadyState.Complete Then
            Me.Text = wb.Url.ToString
            mblnPageReady = True
            RemoveHandler wb.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
            Threading.Thread.Sleep(1000)
            'lblStatus.Text = "OK"
        End If
    End Sub

    Private Sub dgrTkts_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles dgrTkts.CellValidated
        Dim strColumnName As String = dgrTkts.Columns(e.ColumnIndex).Name

        Select Case strColumnName
            Case "Fare", "Tax", "VAT"
                SumGrandTotal()
        End Select
    End Sub

    Private Sub lbkShowContent_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkShowContent.LinkClicked
        If lbkShowContent.Text = "ShowContent" Then
            wb.Visible = True
            wb.BringToFront()
            lbkShowContent.Text = "HideContent"
        Else
            wb.Visible = False
            wb.SendToBack()
            lbkShowContent.Text = "ShowContent"
        End If
    End Sub

    Private Sub lbkUpdtNumeric_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUpdtNumeric.LinkClicked
        If Not IsNumeric(txtNewNumeric.Text) Then
            MsgBox("Invalid numeric vallue for " & cboFieldName.Text)
        End If
        For Each objRow As DataGridViewRow In dgrTkts.Rows
            If objRow.Cells("PaxType").Value = cboPaxType.Text Then
                objRow.Cells(cboFieldName.Text).Value = txtNewNumeric.Text
            End If
        Next

        'ReformatGridTkts()
        SumGrandTotal()
    End Sub

    Private Sub lbkUpdateBooker_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUpdateBooker.LinkClicked
        If ChkBizTrip.Checked AndAlso cboBooker.Text = "" Then
            MsgBox("You must select Booker for Business trip!")
            Exit Sub
        End If
        For Each objRow As DataGridViewRow In dgrTkts.Rows
            objRow.Cells("Booker").Value = cboBooker.Text
            objRow.Cells("BIZ").Value = ChkBizTrip.Checked
        Next
    End Sub

    Private Sub cboBooker_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBooker.SelectedIndexChanged
        ChkBizTrip.Checked = Not (cboBooker.Text = "ZPERSONAL")
    End Sub

    '20220523 add by 7643 -b-
    Private Sub lbkCreateGkPnr_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateGkPnr.LinkClicked
        Dim i, j As Integer
        Dim mStr, mSS, mNM1, mMsg As String
        Dim mShowmessage As frmShowMessageInTextBox

        If (txtRloc.Text = "") Or (txtRloc.Text.Length <> 6) Then
            MsgBox("Please enter Rloc again!")
            Exit Sub
        End If

        mMsg = ""
        For i = 0 To dgrTkts.Rows.Count - 1
            If dgrTkts.Rows(i).Cells("PaxType").Value = "INF" Then
                Continue For
            End If

            mSS = ""
            For j = 0 To dgrSegs.Rows.Count - 1
                mStr = "SS " & dgrSegs.Rows(j).Cells("Car").Value & dgrSegs.Rows(j).Cells("FltNbr").Value & " " &
                       Strings.Mid(dgrTkts.Rows(i).Cells("BkgCls").Value, j + 1, 1) & " " &
                       Date.Parse(dgrSegs.Rows(j).Cells("FltDate").Value).ToString("ddMMMyy") & " " &
                       dgrSegs.Rows(j).Cells("FromCity").Value & dgrSegs.Rows(j).Cells("ToCity").Value & " " &
                       "GK1/" & Replace(dgrSegs.Rows(j).Cells("ETD").Value, ":", "") & " " & Replace(dgrSegs.Rows(j).Cells("ETA").Value, ":", "") &
                                IIf(dgrSegs.Rows(j).Cells("NumDay").Value.ToString <> 0, "+" & dgrSegs.Rows(j).Cells("NumDay").Value.ToString, "") & "/" & txtRloc.Text

                mSS = mSS & IIf(mSS <> "", vbCrLf, "") & mStr
            Next
            mNM1 = "NM1 " & dgrTkts.Rows(i).Cells("PaxName").Value & IIf(dgrTkts.Rows(i).Cells("PaxType").Value = "CHD", "(CHD)", "")

            mMsg = mMsg & IIf(mMsg <> "", vbCrLf & vbCrLf, "") & mSS & vbCrLf & mNM1 & vbCrLf & "AP" & vbCrLf & "TKOK" & vbCrLf & "RFP;ER"
        Next

        mShowmessage = New frmShowMessageInTextBox("Amadeus Command", mMsg)
        mShowmessage.txtMsg.ScrollBars = ScrollBars.Both
        mShowmessage.ShowDialog()
    End Sub

    Private Sub lbkInc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkInc.LinkClicked
        If dgrSegs.CurrentRow.Cells("NumDay").Value < 2 Then
            dgrSegs.CurrentRow.Cells("NumDay").Value = dgrSegs.CurrentRow.Cells("NumDay").Value + 1
        End If
    End Sub

    Private Sub lbkDec_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDec.LinkClicked
        If dgrSegs.CurrentRow.Cells("NumDay").Value > -1 Then
            dgrSegs.CurrentRow.Cells("NumDay").Value = dgrSegs.CurrentRow.Cells("NumDay").Value - 1
        End If
    End Sub

    Private Sub lbkTktIssuedBy_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkTktIssuedBy.LinkClicked
        Dim tblUser As DataTable = GetDataTable("select SiCode, SiName,StaffId from tblUser" _
                                                & " where status<>'XX' and staffId<>0 and Counter='CWT'" _
                                                & " order by SiName")

        Dim frmSelect As New frmShowTableContent(tblUser, "Select Staff", "StaffId",, myStaff.StaffId)
        If frmSelect.ShowDialog = DialogResult.OK Then
            txtTktIssuedBy.Text = frmSelect.SelectedValue
        End If
    End Sub
    '20220523 add by 7643 -e-
End Class