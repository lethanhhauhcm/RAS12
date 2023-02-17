'20220505 add by 7643
Public Class ParseFromPDF
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
    Private ofdPnr As New OpenFileDialog
    Public Sub New(strSystem As String, strFileExtension As String, intCustId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mintCustId = intCustId
        'Dim ofdPnr As New OpenFileDialog
        txtCar.Text = strSystem

        With ofdPnr
            .Filter = strFileExtension.ToUpper & "file|*." & strFileExtension & "*"
            If .ShowDialog <> System.Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            If strFileExtension.ToUpper = "PDF" Then
                Select Case strSystem
                    Case "VJ"
                        If ParseVjPnrPdf() Then
                            AdjustParsedData()
                            AdjustParsedDataVj()
                            LoadData2GUI()
                        Else
                            MsgBox("Không đọc được VJ PNR dạng " & strFileExtension)
                        End If
                End Select
            End If
        End With

    End Sub

    Private Sub lbkOK_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPush2RAS.LinkClicked
        Me.DialogResult = DialogResult.OK
    End Sub
    Private Function ParseVjVndAmtHtml(ByRef arrLines As String(), ByRef intStartLine As Integer) As Decimal
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

    Public Function GetLastLccTktNbr(ByVal strCar As String, ByVal strRloc As String _
                                     , intCountTtlPax As Integer) As String
        Dim strQuerry, mStr As String
        Dim DataTable As DataTable

        If intCountTtlPax > 99 And strRloc.Length > 7 Then
            strRloc = Mid(strRloc, strRloc.Length - 6)
        End If
        strQuerry = "select top 1 Tkno from Ras12.dbo.tkt where Status <> 'XX' and SRV ='S' and substring(TKNO,1,13)='" _
            & "Z" & strCar & " " & Mid(strRloc.PadRight(8, "0"), 1, 6) & " " & Mid(strRloc.PadRight(8, "0"), 7) _
            & "'  order by RecId desc"

        DataTable = pobjTvcs.GetDataTable(strQuerry)
        If DataTable.Rows.Count = 0 Then
            mStr = ""
        Else
            mStr = pobjTvcs.GetScalarAsString(strQuerry)
        End If
        ' can bo sung them lay luon tu tkt_1a de so sanh, lay so cuoi cung
        Return mStr
    End Function

    Public Function CreateLccTkno(strCar As String, strRloc As String, intSeqNbr As Integer _
                                  , intCountTtlPax As Integer) As String
        Dim strTkno As String

        If intCountTtlPax > 99 And strRloc.Length > 7 Then
            strRloc = Mid(strRloc, strRloc.Length - 6)
        End If
        strTkno = "Z" & strCar & strRloc
        If strTkno.Length < 13 Then
            Dim strZeros As New String("0", 13 - (strTkno.Length + intSeqNbr.ToString.Length))
            strTkno = strTkno & strZeros & intSeqNbr
        ElseIf strTkno.Length > 13 Then
            MsgBox("")

        End If

        Return Mid(strTkno, 1, 3) & " " & Mid(strTkno, 4, 6) & " " & Mid(strTkno, 10)
    End Function

    Private Function GetCityCode(xCityName As String) As String
        Dim DataTable As DataTable
        Dim mStr As String

        mStr = ""
        DataTable = pobjTvcs.GetDataTable("select top 1 City " +
                                                       "from LIB..CityCode " +
                                                       "where CityName='" + xCityName + "'")
        If DataTable.Rows.Count > 0 Then
            mStr = DataTable.Rows(0)("City")
        Else
            Try
                DataTable = pobjTvcs.GetDataTable("select top 1 City " +
                                             "from LIB..CityCode_Custom " +
                                             "where CityName='" + xCityName + "'")
                If DataTable.Rows.Count > 0 Then
                    mStr = DataTable.Rows(0)("City")
                End If
            Catch
                pobjTvcs.ExecuteNonQuerry("select RecID,City,CityName " +
                                          "into LIB..CityCode_Custom " +
                                          "from LIB..CityCode " +
                                          "where RecID=0")
            End Try

            If DataTable.Rows.Count = 0 Then
                mStr = InputBox("Please enter City code for '" + xCityName + "'", "Create new City code")

                If mStr.Length > 3 Then
                    MsgBox("City code max lengh is 3!")
                    mStr = ""
                Else
                    If mStr <> "" Then
                        pobjTvcs.ExecuteNonQuerry("insert into LIB..CityCode_Custom(City,CityName) " +
                                                  "values('" + mStr + "','" + xCityName + "')")
                    End If
                End If
            End If
        End If

        Return mStr
    End Function

    Private Function Gettkno(xLastTkno As String, xLengthSeq As Integer, ByRef xSeq As Integer) As String
        Dim mStr As String

        xSeq = xSeq + 1
        mStr = xSeq.ToString
        mStr = mStr.PadLeft(xLengthSeq, "0")

        Return Strings.Left(xLastTkno, xLastTkno.Length - xLengthSeq) + mStr
    End Function

    Private Function GetArrayStringPath(xString As String, xDelimiterBegin As String, xDelimiterFinal As String) As String()
        Dim mStr As String

        mStr = Split(xString, xDelimiterBegin)(1)
        mStr = Split(mStr, xDelimiterFinal)(0)

        Return Split(mStr, vbLf)
    End Function

    Private Function GetArrayStringPath2(xString As String, xDelimiterBegin As String, xDelimiterMidle1 As String, xDelimiterMidle2 As String, xDelimiterFinal As String) As String()
        Dim mStr, mStr2, mStr3 As String

        mStr = Split(xString, xDelimiterBegin)(1)
        mStr = Split(mStr, xDelimiterFinal)(0)

        If mStr.Contains(xDelimiterMidle1) And mStr.Contains(xDelimiterMidle1) Then
            mStr2 = Split(mStr, xDelimiterMidle1)(0)
            mStr3 = Split(mStr, xDelimiterMidle2)(1)

            mStr = mStr2 + mStr3
        End If

        Return Split(mStr, vbLf)
    End Function

    Private Function GetAmt(xString As String, xType As Integer) As Double
        Dim mStr, mArrStr() As String
        Dim mDou As Double

        mArrStr = Split(xString)
        If xType = 0 Then
            mStr = mArrStr(mArrStr.Length - 3)
        ElseIf xType = 1 Then
            mStr = mArrStr(mArrStr.Length - 2)
        Else
            mStr = mArrStr(mArrStr.Length - 1)
        End If
        mDou = CDec(mStr)

        Return mDou
    End Function

    Private Function GetStringPath(xString As String, xDelimiterBegin As String, xDelimiterFinal As String) As String
        Dim mStr As String

        mStr = Split(xString, xDelimiterBegin)(1)
        mStr = Split(mStr, xDelimiterFinal)(0)

        Return mStr
    End Function

    Private Function ParseVjPnrPdf() As Boolean
        Dim mSou, mDel, mDel2, mStr, mArrStr(), mPNR, mArrStr2(), mLastTkno, mStr2, mDel3, mDel4, mStr3, mArrFly(), mArrPay(), mArrPri(), mArrCus() As String
        Dim mNumCus, mNumFly, i, mLenSeq, mSeq, j As Integer
        Dim pdfReader As New iTextSharp.text.pdf.PdfReader(ofdPnr.FileName)
        Dim strategy As iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy
        Dim mDo As Boolean
        Dim mLstFlyNo As New List(Of String)

        mSou = ""
        For i = 1 To pdfReader.NumberOfPages
            strategy = New iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy
            mSou = mSou + iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(pdfReader, i, strategy)
        Next

        pdfReader.Close()
        pobjTvcs.Connect()

        'Danh sach thanh toan
        mDel = "Số tiền  "
        mDel2 = "Booking Offices"
        mArrPay = GetArrayStringPath(mSou, mDel, mDel2)

        'Ngay thanh toan
        mdteDOI = dtpDOI.MinDate
        For i = mArrPay.Length - 1 To 0 Step -1
            If (mArrPay(i).Trim <> "") And (Split(mArrPay(i)).Length > 3) Then
                If IsDate(Split(mArrPay(i))(2) + Split(mArrPay(i))(1) + Split(mArrPay(i))(0)) Then
                    mdteDOI = Convert.ToDateTime(Split(mArrPay(i))(2) + Split(mArrPay(i))(1) + Split(mArrPay(i))(0))
                    Exit For
                End If
            End If
        Next

        'Danh sach chuyen bay
        mDel = "Khởi hành Đến"
        mDel2 = "Hành trình"
        mArrFly = GetArrayStringPath(mSou, mDel, mDel2)

        'So chuyen bay
        mNumFly = 0
        For i = 0 To mArrFly.Length - 1
            If mArrFly(i) <> "" Then
                If Split(mArrFly(i)).Length > 4 Then
                    If IsDate(Split(mArrFly(i))(3) + Split(mArrFly(i))(1) + Strings.Left(Split(mArrFly(i))(2), 2)) Then
                        mNumFly = mNumFly + 1
                    End If
                End If
            End If
        Next

        'Thong tin chuyen bay
        mStr3 = ""
        For i = 0 To mArrFly.Length - 1
            If mArrFly(i).Trim <> "" Then
                mStr3 = mStr3 + IIf(mStr3 = "", "", " ") + mArrFly(i)
                mDo = False
                If i < mArrFly.Length - 1 Then
                    If Split(mArrFly(i + 1))(0) = "" Then
                        mDo = True
                    ElseIf (Split(mArrFly(i + 1)).Length > 3) Then
                        If IsDate(Split(mArrFly(i + 1))(3) + Split(mArrFly(i + 1))(1) + Strings.Left(Split(mArrFly(i + 1))(2), 2)) Then
                            mDo = True
                        End If
                    End If
                Else
                    mDo = True
                End If

                If mDo Then
                    'From City
                    mStr = Split(mStr3, " - ")(1)
                    If mStr.Contains(":") Then
                        mStr = Strings.Left(mStr, mStr.Length - 6)
                    End If

                    mlstFromCities.Add(GetCityCode(mStr))

                    'To city
                    mStr = Split(mStr3, ":")(2)
                    mStr = Split(mStr, " - ")(1).TrimEnd
                    mlstToCities.Add(GetCityCode(mStr))

                    'Car
                    mStr = Split(mStr3)(0)
                    mStr2 = Strings.Left(mStr, 2)
                    mlstCars.Add(mStr2)

                    'FltNbr
                    mStr2 = Strings.Right(mStr, 3)
                    mlstFltNbrs.Add(mStr2)

                    'Flight No
                    mLstFlyNo.Add(mStr)

                    'Ngay khoi hanh
                    mlstFltDates.Add(Convert.ToDateTime(Split(mStr3)(3) + Split(mStr3)(1) + Strings.Left(Split(mStr3)(2), 2)))

                    'ETD
                    mStr = Split(mStr3, ":")(0)
                    mStr = Strings.Right(mStr, 2) + ":"
                    mStr2 = Split(mStr3, ":")(1)
                    mStr = mStr + Strings.Left(mStr2, 2)
                    mlstETDs.Add(mStr)

                    'ETA
                    mStr = Split(mStr3, ":")(1)
                    mStr = Strings.Right(mStr, 2) + ":"
                    mStr2 = Split(mStr3, ":")(2)
                    mStr = mStr + Strings.Left(mStr2, 2)
                    mlstETAs.Add(mStr)

                    mStr3 = ""
                End If
            End If
        Next

        'Ngay bat dau
        mdteDOF = mlstFltDates(0)

        'Danh sach khach
        mDel = "Số ghế"
        mDel2 = "1. Thông tin đặt chỗ"
        mArrCus = GetArrayStringPath(mSou, mDel, mDel2)

        'PNR
        mDel = "Mã đặt chỗ (số vé)"
        mStr = Split(mSou, mDel)(1)
        mPNR = Split(mStr, vbLf)(1)

        'So khach
        mStr = ""
        mNumCus = 0
        For i = 0 To mArrCus.Length - 1
            If (mArrCus(i).Trim <> "") Then
                mStr = mStr + IIf(mStr = "", "", " ") + mArrCus(i)
                For j = 0 To mLstFlyNo.Count - 1
                    If mStr.Contains(mLstFlyNo(j)) Then
                        mNumCus = mNumCus + 1
                        Exit For
                    End If
                Next
            End If
        Next

        'tkno cuoi
        mLastTkno = GetLastLccTktNbr(txtCar.Text, mPNR, mNumCus).Trim
        If mLastTkno = "" Then
            'Tao tkno cuoi
            mLastTkno = CreateLccTkno(txtCar.Text, mPNR, mNumFly, mNumCus)
        End If

        'Thong tin khach
        mLenSeq = Split(mLastTkno)(2).Length
        mSeq = CInt(Split(mLastTkno)(2))
        mStr = ""
        For i = 0 To mArrCus.Length - 1
            If (mArrCus(i).Trim <> "") Then
                mStr = mStr + IIf(mStr = "", "", " ") + mArrCus(i)
                mDo = False
                For j = 0 To mLstFlyNo.Count - 1
                    If mStr.Contains(mLstFlyNo(j)) Then
                        mDo = True
                        Exit For
                    End If
                Next

                If mDo Then
                    mStr = Replace(mStr, "  ", " ")
                    For j = 0 To mLstFlyNo.Count - 1
                        mStr = Split(mStr, mLstFlyNo(j))(0)
                    Next

                    'tkno
                    mlstTknos.Add(Gettkno(mLastTkno, mLenSeq, mSeq))
                    If mStr.Contains("Infant:") Then
                        mlstTknos.Add(Gettkno(mLastTkno, mLenSeq, mSeq))
                    End If

                    'Ten khach
                    mStr = Replace(mStr, ",", "")
                    If mStr.Contains("Infant:") Then
                        mlstPaxNames.Add(Split(mStr, "Infant:")(0).Trim)

                        mlstPaxNames.Add(Split(mStr, "Infant:")(1).Trim)
                        mlstDOBs.Add(Date.MinValue)
                    Else
                        mlstPaxNames.Add(mStr.Trim)
                    End If
                    mlstDOBs.Add(Date.MinValue)

                    mStr = ""
                End If
            End If
        Next

        'Danh sach gia
        mDel = "Thuế Cộng"
        mDel2 = "Giá hiển thị theo tiền"
        mDel3 = "cho hành khách."
        mDel4 = "Tổng cộng"
        mArrPri = GetArrayStringPath2(mSou, mDel, mDel2, mDel3, mDel4)

        'Khoi tao gia
        For i = 0 To mlstTknos.Count - 1
            mlstFares.Add(0)
            mlstVatFares.Add(0)
            mlstFbs.Add("")
            mlstBkgClss.Add("")
            mlstPhiKhac.Add(0)
            mlstVatPhiKhac.Add(0)
        Next

        'Tinh gia
        mStr2 = ""
        For i = 0 To mArrPri.Length - 1
            If mArrPri(i).Trim <> "" Then
                mStr2 = mStr2 + IIf(mStr2 = "", "", " ") + mArrPri(i)
                mArrStr = Split(mArrPri(i))
                mDo = False
                If mArrStr.Length > 2 Then
                    If IsNumeric(mArrStr(mArrStr.Length - 1)) And IsNumeric(mArrStr(mArrStr.Length - 2)) And IsNumeric(mArrStr(mArrStr.Length - 3)) Then
                        mDo = True
                    End If
                End If

                If mDo Then
                    mStr2 = Replace(mStr2, "  ", " ")
                    mStr = Replace(mStr2, ",", " ", 1, 1)
                    For j = 0 To mlstPaxNames.Count - 1
                        If mStr.Contains(mlstPaxNames(j)) Then
                            Exit For
                        End If
                    Next

                    If mStr2.Contains("Eco") Or mStr2.Contains("SkyBoss") Or mStr2.Contains("Deluxe") Or mStr2.Contains("Add Ons") Or mStr2.Contains("Admin Fee") Or
                       mStr2.Contains("Management Fee") Then

                        'FBasic, BkgClass
                        If mStr2.Contains("Eco") Then
                            mlstFbs(j) = mlstFbs(j) + IIf(mlstFbs(j) = "", "", "+") + "Y"
                            mlstBkgClss(j) = mlstBkgClss(j) + "Y"
                        ElseIf mStr2.Contains("SkyBoss") Then
                            mlstFbs(j) = mlstFbs(j) + IIf(mlstFbs(j) = "", "", "+") + "C"
                            mlstBkgClss(j) = mlstBkgClss(j) + "C"
                        ElseIf mStr2.Contains("Deluxe") Then
                            mlstFbs(j) = mlstFbs(j) + IIf(mlstFbs(j) = "", "", "+") + "P"
                            mlstBkgClss(j) = mlstBkgClss(j) + "P"
                        End If

                        'Fare
                        mlstFares(j) = mlstFares(j) + GetAmt(mStr2, 0)

                        'VAT Fare
                        mlstVatFares(j) = mlstVatFares(j) + GetAmt(mStr2, 1)
                    Else
                        'Phi khac, VAT phi khac
                        mlstPhiKhac(j) = mlstPhiKhac(j) + GetAmt(mStr2, 0)
                        mlstVatPhiKhac(j) = mlstVatPhiKhac(j) + GetAmt(mStr2, 1)
                    End If

                    mStr2 = ""
                End If
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
            mlstTotalTaxes.Add(0)
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
    Private Function AdjustParsedDataVj() As Boolean
        Dim i As Integer
        Dim intVatPct As Decimal = GetVatPct(mdteDOI)
        Dim intAdlCount As Integer
        Dim intChdCount As Integer
        Dim decThuHoPerAdl As Decimal
        Dim decThuHoPerChd As Decimal
        Dim decThuHoPerInf As Decimal = 0

        For i = 0 To mlstPaxTypes.Count - 1
            If mlstPaxTypes(i) = "INF" Then
            ElseIf mlstPaxTypes(i) = "CHD" Then
                intChdCount = intChdCount + 1
            Else
                intAdlCount = intAdlCount + 1
            End If
        Next

        decThuHoPerAdl = mdecTotalThuHo / (intAdlCount + (intChdCount / 2))
        decThuHoPerChd = decThuHoPerAdl / 2

        For i = 0 To mlstPaxTypes.Count - 1
            If mlstPaxTypes(i) = "INF" Then
                mlstTotalTaxes(i) = mlstVatFares(i) + mlstVatPhiKhac(i)
            ElseIf mlstPaxTypes(i) = "CHD" Then
                mlstTotalTaxes(i) = mlstVatFares(i) + decThuHoPerChd + mlstVatPhiKhac(i)
            ElseIf mlstPaxTypes(i) = "ADL" Then
                mlstTotalTaxes(i) = mlstVatFares(i) + decThuHoPerAdl + mlstVatPhiKhac(i)
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
            dgrSegs.Rows.Add({mlstFromCities(i), mlstToCities(i), mlstCars(i), mlstFltNbrs(i), mlstFltDates(i), mlstETDs(i), mlstETAs(i)})
        Next
        For i = 0 To mlstTknos.Count - 1
            dgrTkts.Rows.Add({FormatRasTkno(mlstTknos(i)), mlstPaxNames(i), mlstPaxTypes(i), mlstFbs(i), mlstBkgClss(i) _
                             , mlstFares(i), mlstTotalTaxes(i) + mlstPhiKhac(i), mlstVatFares(i) + mlstVatPhiKhac(i), 0, 0, 0, False, ""})
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
End Class