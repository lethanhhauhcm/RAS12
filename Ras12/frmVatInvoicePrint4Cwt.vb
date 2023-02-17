Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions

Public Class frmVatInvoicePrint4Cwt
    Private mblnDataLoadCompleted As Boolean
    Private mobjExcel As New Excel.Application
    Private mobjWbk As Workbook
    Private mobjWsh As Worksheet
    'Private mblnLoadExcel As Boolean
    Private mstrAction As String
    Private mstrCustGroup As String
    Private mlstVatInvLinks As New List(Of clsVatInvLink)
    Private mintVatDiscount As Integer

    Public Sub New(intVatDiscount As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetUpValues(intVatDiscount)

    End Sub
    Private Function SetUpValues(intVatDiscount As Integer) As Boolean
        mintVatDiscount = intVatDiscount

        If intVatDiscount = 30 Then
            Me.Text = "VatInvoicePrint4Cwt VAT7"
        Else
            Me.Text = "VatInvoicePrint4Cwt No VAT Discount"
        End If
        Return True
    End Function
    Public Structure Action
        Const LoadAhc = "LoadAhc"
        Const LoadListing = "LoadListing"
        Const LoadTrx = "LoadTrx"
        Const LoadTkt = "LoadTkt"
        Const LoadTcode = "LoadTcode"
        Const LoadVatInv = "LoadVatInv"
    End Structure
    Private Sub frmVatInvoicePrint4Cwt_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        mobjExcel.Quit()

    End Sub

    Private Sub frmVatInvoicePrint4Cwt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'cboCustomer.SelectedIndex = 0
        mstrAction = Action.LoadVatInv
        Clear()
        cboSelectCount.SelectedIndex = 0
        mblnDataLoadCompleted = True
    End Sub

    Private Sub lbkLoadFile_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkLoadFile.LinkClicked
        Dim objOfd As New OpenFileDialog
        With objOfd
            .Filter = "excel files (*.xls)|"
            .ShowDialog()
            If .FileName = "" Then
                Exit Sub
            End If

            mstrAction = Action.LoadListing
            dgrInvDetails.DataSource = Nothing


            Dim objWsh As Worksheet
            'Dim i As Integer, j As Integer

            mobjWbk = mobjExcel.Workbooks.Open(.FileName, , True)
            mblnDataLoadCompleted = False
            cboSheetName.Items.Clear()

            If mstrCustGroup = "ABBOTT LOCAL" Then
                If cboSheetName.Items.Count = 0 Then
                    For Each objWsh In mobjWbk.Sheets
                        cboSheetName.Items.Add(objWsh.Name)
                        mblnDataLoadCompleted = True
                        cboSheetName.SelectedIndex = 0
                        Exit For
                    Next
                End If
            End If
            Select Case cboCustomer.Text
                'Case "ATOTECH", "EY HAN", "EY SGN", "HEMPEL", "VOITH HYDRO", "JCI VN", "INGER"  '^_^20220802 mark by 7643
                Case "ATOTECH", "EY HAN", "EY SGN", "HEMPEL", "VOITH HYDRO", "JCI VN", "INGER", "HASBRO"  '^_^20220802 modi by 7643
                    For Each objWsh In mobjWbk.Sheets

                        If objWsh.Name.StartsWith("DOM") _
                            Or objWsh.Name.StartsWith("INT") _
                            Or objWsh.Name.ToUpper.StartsWith("NON AIR") _
                            Or objWsh.Name.ToUpper.StartsWith("HOTLINE") Then
                            cboSheetName.Items.Add(objWsh.Name)
                        End If
                    Next
                    mblnDataLoadCompleted = True

                    cboSheetName.SelectedIndex = 0
                Case "ORACLE"
                Case "EVN SPC"
                    For Each objWsh In mobjWbk.Sheets
                        If objWsh.Name = "ALL" Then
                            cboSheetName.Items.Add(objWsh.Name)
                            Exit For
                        End If
                    Next
                Case "HOGAN HCM", "HOGAN HN"

                    cboSheetName.Items.Add("DOM")
                    cboSheetName.Items.Add("INT")
                    cboSheetName.Items.Add("NONAIR")

                Case "NGHISON VN"
                    For Each objWsh In mobjWbk.Sheets
                        Select Case objWsh.Name
                            Case "Vé nội địa ", "Vé quốc tế", "Phòng nội địa", "Phòng quốc tế", "Xe quốc tế", "Xe nội địa"
                                cboSheetName.Items.Add(objWsh.Name)
                        End Select
                    Next

                Case "ABBOTT 3A ANI_M", "ABBOTT ALSA ANI_MICE", "ABBOTT ALSA_MICE", "ABBOTT LA HCP", "ABBOTT RO", "ABBOTT RO_MICE", "NOVARTIS VN" _
                    , "NOVARTIS_MICE", "NVS_ MICE", "SANDOZ NOVARTIS", "SDZ_MICE", "ABBOTT 3A", "ABBOTT 3A_MICE",  '^_^20220919 add "ABBOTT 3A", "ABBOTT 3A_MICE" by 7643
                     "KHLE"  '^_^20220930 add by 7643

                    For Each objWsh In mobjWbk.Sheets
                        Select Case objWsh.Name.ToUpper
                            Case "IMPORT"
                                cboSheetName.Items.Add(objWsh.Name)

                                '^_^20221001 add by 7643 -b-
                                If cboCustomer.Text = "KHLE" Then
                                    mblnDataLoadCompleted = True
                                    cboSheetName.SelectedIndex = 0
                                End If
                                '^_^20221001 add by 7643 -e-
                        End Select
                    Next
                Case "NGOCMAI_VJ", "PHUONGNAM_VJ", "SAOMAI_VJ", "THANHHOANG_VJ"
                    For Each objWsh In mobjWbk.Sheets
                        cboSheetName.Items.Add(objWsh.Name)
                    Next

                Case "PATH VN"
                    For Each objWsh In mobjWbk.Sheets
                        If objWsh.Name.StartsWith("MISC") Or objWsh.Name.StartsWith("DOM-") Or objWsh.Name.StartsWith("INT-") Or objWsh.Name.ToUpper.StartsWith("NONAIR-") Then
                            cboSheetName.Items.Add(objWsh.Name)
                        End If
                    Next
                    mblnDataLoadCompleted = True
                    'cboSheetName.SelectedIndex = 0

            End Select
            mblnDataLoadCompleted = True

        End With
    End Sub
    Private Function LoadWorksheet2Datagridview4EVN_SPC(objWsh As Worksheet) As Boolean
        Dim intMaxColumn As Integer
        Dim i As Integer, j As Integer
        Dim intHeaderRow As Integer
        dgrTktListing.DataSource = Nothing
        dgrTktListing.Rows.Clear()
        dgrTktListing.Columns.Clear()
        dgrTktListing.Columns.Add("Sheet", "Sheet")


        intHeaderRow = 6

        For i = 1 To 54
            If objWsh.Cells(intHeaderRow, i) Is Nothing Or objWsh.Cells(intHeaderRow, i).value = "" Then
                intMaxColumn = i
                Exit For
            Else
                dgrTktListing.Columns.Add(objWsh.Cells(intHeaderRow, i).value, objWsh.Cells(intHeaderRow, i).value)
            End If
        Next

        For i = intHeaderRow + 1 To 1000
            If objWsh.Range("B" & i).Value Is Nothing Then
                Exit For
            ElseIf objWsh.Range("B" & i).Value.ToString = "TOTAL" Then
                Exit For
            End If
            dgrTktListing.Rows.Add()
            dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(0).Value = objWsh.Name
            For j = 1 To intMaxColumn - 1
                dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(j).Value _
                    = objWsh.Cells(i, j).value
            Next
        Next
        dgrTktListing.Columns("INV No").DisplayIndex = 1
        Return True
    End Function
    Private Function LoadWorksheet2Datagridview4NGHISON(objWsh As Worksheet) As Boolean
        Dim intMaxColumn As Integer
        Dim i As Integer, j As Integer
        Dim intHeaderRow As Integer
        dgrTktListing.DataSource = Nothing
        dgrTktListing.Rows.Clear()
        dgrTktListing.Columns.Clear()
        dgrTktListing.Columns.Add("Sheet", "Sheet")
        Dim strColumnName As String
        Dim arrVndColumns() As String = {"Airfare", "VAT of Air fare", "Other charges", "VAT of other charges", "Airline Charge" _
                                           , "VAT of Airline Charge", "Service fee", "VAT of service fee", "Total/VND" _
                                           , "Airfare", "Amount", "VAT"}
        intHeaderRow = 11

        For i = 1 To 54
            If objWsh.Cells(intHeaderRow, i) Is Nothing OrElse objWsh.Cells(intHeaderRow, i).value = "" Then
                intMaxColumn = i
                Exit For
            Else
                Select Case objWsh.Cells(intHeaderRow, i).value
                    Case "STT", "Employee Code", "Purpose", "TRX"
                        strColumnName = objWsh.Cells(intHeaderRow, i).value
                    Case Else
                        strColumnName = GetFirstValueInBracket(objWsh.Cells(intHeaderRow, i).value)
                        If strColumnName = "" Then
                            MsgBox("Invalid format for column name " & objWsh.Cells(intHeaderRow, intHeaderRow).value)
                            Return False
                        End If
                End Select
                dgrTktListing.Columns.Add(strColumnName, strColumnName)
            End If
        Next

        For i = intHeaderRow + 1 To 1000
            If objWsh.Range("A" & i).Value Is Nothing Then
                Exit For
            ElseIf objWsh.Range("A" & i).Value.ToString = "" Then
                Exit For
            End If
            dgrTktListing.Rows.Add()
            dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(0).Value = objWsh.Name
            For j = 1 To intMaxColumn - 1
                dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(j).Value _
                    = objWsh.Cells(i, j).value
            Next
        Next
        FormatDatagridView4VndColumns(dgrTktListing, arrVndColumns)

        Return True
    End Function
    Private Function LoadWorksheet2Datagridview4HOGAN(objWsh As Worksheet, strSvcType As String) As Boolean
        Dim intMaxColumn As Integer = 27
        Dim i As Integer, j As Integer
        Dim intHeaderRow As Integer = 11
        Dim intStartRow As Integer = 12
        Dim intEndRow As Integer
        dgrTktListing.DataSource = Nothing
        dgrTktListing.Rows.Clear()
        dgrTktListing.Columns.Clear()
        dgrTktListing.Columns.Add("Sheet1", "Sheet1")
        Dim strColumnName As String
        Dim arrVndColumns() As String = {"Giá vé " & vbLf & "(Fare)", "Thuế GTGT" & vbLf & "(VAT)" _
                                        , "Thuế khác" & vbLf & "(Tax)" _
                                        , "Thuế GTGT" & vbLf & "(VAT) của Thuế khác" _
                                        , "Phí đổi/Phí mua hành lí" & vbLf & "(Charge)" _
                                        , "Thuế GTGT" & vbLf & "(VAT) của Phí đổi/Phí mua hàng lý" _
                                        , "Phí hoàn" & vbLf & "(penalty)", "Tổng cộng" & vbLf & "(Total amount)" _
                                       , "Phí dịch vụ" & vbLf & "(Sv.Fee)" _
                                       , "Thuế GTGT" & vbLf & "(VAT) của Phí dịch vụ" _
                                       , "Tổng tiền" & vbLf & "thanh toán" & vbLf & "(Payable Amount)" _
                                       , "Giá phòng " & vbLf & "(Room Rate)" _
                                       , "Tổng tiền thanh toán" & vbLf & "(Payable Amount)"}
        intEndRow = objWsh.Cells(objWsh.Rows.Count, "A").End(XlDirection.xlUp).Row
        Select Case strSvcType
            Case "INT"
                For i = 12 To intEndRow
                    If CStr(objWsh.Range("A" & i).Value) = "VÉ QUỐC TẾ/INTERNATIONAL" Then
                        intEndRow = i - 1
                        Exit For
                    End If
                Next
            Case "DOM"
                For i = 12 To intEndRow
                    If CStr(objWsh.Range("A" & i).Value) = "VÉ QUỐC TẾ/INTERNATIONAL" Then
                        intStartRow = i + 1
                        i = i + 1
                    ElseIf CStr(objWsh.Range("A" & i).Value) = "VÉ QUỐC NỘI/DOMESTIC (VNA)" Then
                        intEndRow = i - 1
                        Exit For
                    End If
                Next
            Case "NONAIR"
                For i = 14 To intEndRow
                    If CStr(objWsh.Range("A" & i).Value) = "VÉ QUỐC NỘI/DOMESTIC (VNA)" Then
                        intHeaderRow = i + 2
                        intStartRow = i + 3
                        i = i + 1
                    ElseIf CStr(objWsh.Range("A" & i).Value) = "DỊCH VỤ KHÁC/NON AIR" Then
                        intEndRow = i - 1
                        Exit For
                    End If
                Next
        End Select

        For i = 1 To intMaxColumn
            If objWsh.Cells(intHeaderRow, i) Is Nothing OrElse objWsh.Cells(intHeaderRow, i).value = "" Then
                strColumnName = i
            Else
                strColumnName = objWsh.Cells(intHeaderRow, i).value
            End If
            dgrTktListing.Columns.Add(strColumnName, strColumnName)
        Next

        For i = intStartRow To intEndRow
            If objWsh.Range("A" & i).Value Is Nothing OrElse Not IsNumeric(objWsh.Range("A" & i).Value) Then
                Exit For
            ElseIf objWsh.Range("A" & i).Value.ToString = "" Then
                Exit For
            End If
            dgrTktListing.Rows.Add()
            dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(0).Value = objWsh.Name
            For j = 1 To intMaxColumn
                '    Select Case j
                '        Case 9, 10
                '            dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(j).Value = Math.Round(objWsh.Cells(i, j).value)
                '        Case Else
                dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(j).Value = objWsh.Cells(i, j).value
                '    End Select
            Next
        Next
        FormatDatagridView4VndColumns(dgrTktListing, arrVndColumns)

        Return True
    End Function
    Private Function LoadWorksheet2Datagridview4THANHHOANG_VJ(objWsh As Worksheet) As Boolean
        Dim intMaxColumn As Integer
        Dim i As Integer, j As Integer
        Dim intHeaderRow As Integer
        dgrTktListing.DataSource = Nothing
        dgrTktListing.Rows.Clear()
        dgrTktListing.Columns.Clear()
        dgrTktListing.Columns.Add("Sheet", "Sheet")
        Dim strColumnName As String

        intHeaderRow = 6
        intMaxColumn = 11
        For i = 1 To 11
            If objWsh.Cells(intHeaderRow, i) Is Nothing OrElse objWsh.Cells(intHeaderRow, i).value = "" Then
                strColumnName = i
            Else
                strColumnName = objWsh.Cells(intHeaderRow, i).value
            End If
            dgrTktListing.Columns.Add(strColumnName, strColumnName)
        Next

        For i = intHeaderRow + 1 To 1000
            If objWsh.Range("A" & i).Value Is Nothing OrElse Not IsNumeric(objWsh.Range("A" & i).Value) Then
                Exit For
            ElseIf objWsh.Range("A" & i).Value.ToString = "" Then
                Exit For
            End If
            dgrTktListing.Rows.Add()
            dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(0).Value = objWsh.Name
            For j = 1 To intMaxColumn
                Select Case j
                    Case 9, 10
                        dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(j).Value = Math.Round(objWsh.Cells(i, j).value)
                    Case Else
                        dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(j).Value = objWsh.Cells(i, j).value
                End Select


            Next
        Next
        dgrTktListing.Columns("Giá chưa thuế").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrTktListing.Columns("Giá chưa thuế").DefaultCellStyle.Format = "#,##0"
        dgrTktListing.Columns("Thuế GTGT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrTktListing.Columns("Thuế GTGT").DefaultCellStyle.Format = "#,##0"
        dgrTktListing.Columns("Tổng tiền VNĐ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrTktListing.Columns("Tổng tiền VNĐ").DefaultCellStyle.Format = "#,##0"
        Return True
    End Function
    Private Function LoadWorksheet2Datagridview(objWsh As Worksheet) As Boolean
        Dim intMaxColumn As Integer
        Dim i As Integer, j As Integer
        Dim intHeaderRow As Integer
        dgrTktListing.DataSource = Nothing
        dgrTktListing.Rows.Clear()
        dgrTktListing.Columns.Clear()
        dgrTktListing.Columns.Add("Sheet", "Sheet")
        Dim arrVndColumns() As String = {"Chi phí" & vbLf & "Giao dịch", "Thuế" & vbLf & "GTGT", "Phí" & vbLf & "Dịch Vụ" _
                                        , "Thuế GTGT  (Phí dịch vụ)", "Tổng tiền VNĐ", "Amount", "VAT", "Total"}  '^_^20220930 add "Amount", "VAT", "Total" by 7643

        If objWsh.Range("C11").Value = "Description of Service" Then
            intHeaderRow = 11
        ElseIf objWsh.Name.ToUpper.StartsWith("NON AIR") Then
            Select Case cboCustomer.Text
                Case "ATOTECH", "HEMPEL", "JCI VN"
                    intHeaderRow = 6
                Case Else
                    intHeaderRow = 10
            End Select
        ElseIf objWsh.Name.ToUpper.StartsWith("IMPORT") Then
            intHeaderRow = 1
        ElseIf objWsh.Name.ToUpper.StartsWith("SHEET1") Then
            intHeaderRow = 10
        Else
            intHeaderRow = 6
        End If

        For i = 1 To 54
            If objWsh.Cells(intHeaderRow, i) Is Nothing Or objWsh.Cells(intHeaderRow, i).value = "" Then
                intMaxColumn = i
                Exit For
            Else
                dgrTktListing.Columns.Add(objWsh.Cells(intHeaderRow, i).value, objWsh.Cells(intHeaderRow, i).value)
            End If
        Next

        For i = intHeaderRow + 1 To 1000
            If objWsh.Range("A" & i).Value Is Nothing Then
                Exit For
            ElseIf objWsh.Range("A" & i).Value.ToString = "Total" Then
                Exit For
            End If
            dgrTktListing.Rows.Add()
            dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(0).Value = objWsh.Name
            For j = 1 To intMaxColumn - 1
                dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(j).Value _
                    = objWsh.Cells(i, j).value
            Next
        Next
        If cboCustomer.Text = "HEMPEL" Then
            'If cboCustomer.Text = "HEMPEL" Or mstrCustGroup = "ABBOTT LOCAL" Then 
            Dim colSelected As New DataGridViewCheckBoxColumn
            colSelected.Name = "S"
            colSelected.HeaderText = "S"
            dgrTktListing.Columns.Insert(0, colSelected)

            lbkSelectAll.Visible = True
        End If

        FormatDatagridView4VndColumns(dgrTktListing, arrVndColumns)
        If objWsh.Name = "DOM" Then
            Dim arrDomVndColumns() As String = {"Net Fare", "VAT4FARE", "Tax", "VAT Tax", "Service Fee (No VAT)", "VAT (SF)", "TotalVND"}
            FormatDatagridView4VndColumns(dgrTktListing, arrDomVndColumns)
        End If
        Return True
    End Function


    Private Function LoadWorksheet2Datagridview4PATHVN(objWsh As Worksheet) As Boolean
        Dim intMaxColumn As Integer
        Dim i As Integer, j As Integer
        Dim intHeaderRow As Integer
        dgrTktListing.DataSource = Nothing
        dgrTktListing.Rows.Clear()
        dgrTktListing.Columns.Clear()
        dgrTktListing.Columns.Add("Sheet", cboSheetName.Text)
        Dim arrVndColumns() As String = {"Total", "VAT", "Charge", "Cost", "Fee", "Amount"}

        intHeaderRow = 6

        For i = 1 To 54
            If objWsh.Cells(intHeaderRow, i) Is Nothing _
                OrElse objWsh.Cells(intHeaderRow, i).Value = "" Then
                intMaxColumn = i
                Exit For
            Else
                dgrTktListing.Columns.Add(objWsh.Cells(intHeaderRow, i).value, objWsh.Cells(intHeaderRow, i).value)
            End If
        Next

        For i = intHeaderRow + 1 To 1000
            If objWsh.Range("A" & i).Value Is Nothing Then
                Exit For
            ElseIf objWsh.Range("A" & i).Value.ToString = "Total" Then
                Exit For
            End If
            dgrTktListing.Rows.Add()
            dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(0).Value = objWsh.Name
            For j = 1 To intMaxColumn - 1
                dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(j).Value _
                    = objWsh.Cells(i, j).value
            Next
        Next

        FormatDatagridView4VndColumns(dgrTktListing, arrVndColumns)
        dgrTktListing.Columns("Project Code").DisplayIndex = 1
        Return True
    End Function
    '^_^20220929 add by 7643 -e-

    Private Function CheckCorpName(ByRef objWsh As Worksheet, strCustomerName As String _
                                   , strKeyword As String) As Boolean
        Select Case strCustomerName
            Case "EY"
                If Not objWsh.Range("C2").Value.ToString.Contains("ERNST & YOUNG") Then
                    Return False
                End If
            Case "ORACLE"
        End Select

        Return True
    End Function







    Private Sub cboSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSheetName.SelectedIndexChanged
        If mblnDataLoadCompleted Then

            Select Case cboCustomer.Text
                Case "ABBOTT 3A ANI_M", "ABBOTT ALSA ANI_MICE", "ABBOTT ALSA_MICE", "ABBOTT LA HCP", "ABBOTT RO", "ABBOTT RO_MICE", "NOVARTIS VN" _
                    , "NOVARTIS_MICE", "NVS_ MICE", "SANDOZ NOVARTIS", "SDZ_MICE", "HEMPEL", "HASBRO", "ABBOTT 3A", "ABBOTT 3A_MICE", "KHLE"  '^_^20220930 add KHLE by 7643  '^_^20220919 add "ABBOTT 3A", "ABBOTT 3A_MICE" by 7643
                    '^_^20220802 modi by 7643 -e-
                    For Each objWsh As Worksheet In mobjWbk.Sheets
                        If objWsh.Name = cboSheetName.Text Then
                            LoadWorksheet2Datagridview(objWsh)
                            dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                            mstrAction = Action.LoadListing
                        End If
                    Next

                Case "EVN SPC"
                    For Each objWsh As Worksheet In mobjWbk.Sheets
                        If objWsh.Name = cboSheetName.Text Then
                            LoadWorksheet2Datagridview4EVN_SPC(objWsh)
                            dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                            mstrAction = Action.LoadListing
                        End If
                    Next
                Case "NGHISON VN"
                    For Each objWsh As Worksheet In mobjWbk.Sheets
                        If objWsh.Name = cboSheetName.Text Then
                            LoadWorksheet2Datagridview4NGHISON(objWsh)
                            dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                            mstrAction = Action.LoadListing
                        End If
                    Next


                Case "HOGAN HCM", "HOGAN HN"
                    For Each objWsh As Worksheet In mobjWbk.Sheets
                        If objWsh.Name = "Sheet1" Then
                            LoadWorksheet2Datagridview4HOGAN(objWsh, cboSheetName.Text)
                            dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                            mstrAction = Action.LoadListing
                        End If
                    Next
                Case "NGOCMAI_VJ", "PHUONGNAM_VJ", "SAOMAI_VJ", "THANHHOANG_VJ"
                    For Each objWsh As Worksheet In mobjWbk.Sheets
                        If objWsh.Name = cboSheetName.Text Then
                            LoadWorksheet2Datagridview4THANHHOANG_VJ(objWsh)
                            dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                            mstrAction = Action.LoadListing
                        End If
                    Next

                Case "PATH VN"
                    For Each objWsh As Worksheet In mobjWbk.Sheets
                        If objWsh.Name = cboSheetName.Text Then
                            LoadWorksheet2Datagridview4PATHVN(objWsh)
                            dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                            mstrAction = Action.LoadListing
                            Exit For
                        End If
                    Next

                Case Else
                    For Each objWsh As Worksheet In mobjWbk.Sheets
                        If objWsh.Name = cboSheetName.Text Then
                            dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                            LoadWorksheet2Datagridview(objWsh)
                            dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                            mstrAction = Action.LoadListing
                            If ScalarToInt("MISC", "RecId", "cat='CustNameInGroup' and Status='OK' and VAL1='" _
                                       & cboCustomer.Text & "'") > 0 Then
                                lblF1.Text = "Booker"
                                cboF1.DataSource = Nothing
                                cboF1.Items.Clear()
                                lblF2.Text = "MonthOfStartDate"
                                cboF2.DataSource = Nothing
                                cboF2.Items.Clear()
                                For Each objRow As DataGridViewRow In dgrTktListing.Rows
                                    If cboF1.FindStringExact(objRow.Cells("Người đặt").Value) = -1 Then
                                        cboF1.Items.Add(objRow.Cells("Người đặt").Value)
                                    End If
                                    If cboF2.FindStringExact(objRow.Cells("Month" & vbLf & "of StartDate").Value) = -1 Then
                                        cboF2.Items.Add(objRow.Cells("Month" & vbLf & "of StartDate").Value)
                                    End If
                                Next
                                cboF1.Sorted = True
                                cboF2.Sorted = True
                            End If
                        End If
                    Next
            End Select
        End If
        'End If
    End Sub

    Private Function LoadGridInvoice() As Boolean
        Dim strQuerry As String = ""
        Dim strQuerryExtraInfo As String
        Dim tblExtraInfo As System.Data.DataTable

        cboSheetName.Items.Clear()
        dgrTktListing.DataSource = Nothing
        dgrTktListing.Columns.Clear()
        strQuerryExtraInfo = "select M.RecID, M.VAL1 as DataCode,M1.VAL2 as DataName" _
                & " from misc m " _
                & " left join misc m1 on m.CAT=m1.CAT and m.VAL1=m1.VAL1" _
                & " and m1.Status='OK' and M1.VAL='" & cboCustomer.Text & "'" _
                & " where M.CAT='VatInvDisplay' and M.Status='OK' and m.VAL='ALL'" _
                & " order by M.VAL1"
        tblExtraInfo = GetDataTable(strQuerryExtraInfo, Conn)


        Select Case mstrCustGroup
            Case "ABBOTT LOCAL"

                strQuerry = "Select RecId, InvID, DOI, CustFullName, TaxCode" _
                            & " , InvoiceNo, Period"
                For Each objRow As DataRow In tblExtraInfo.Rows
                    If Not IsDBNull(objRow("DataName")) Then
                        strQuerry = strQuerry & "," & objRow("DataCode") & " as " & objRow("DataName")
                    End If

                    'If IsDBNull(objRow("DataName")) Then
                    '    CType(Me.Controls("txtF" & objRow("DataCode")), TextBox).Visible = False
                    'Else
                    '    CType(Me.Controls("txtF" & objRow("DataCode")), TextBox).Visible = True
                    'End If
                Next
                strQuerry = strQuerry & ",PmtDeadline, Status,GhiNoId,CustShortName, Address, FOP,Service" _
                            & ", FstUser, FstUpdate, LstUser, LstUpdate,ClearDate" _
                            & " FROM VatInv where Status='OK' and CustShortName='" & cboCustomer.Text & "'"
                AddEqualConditionCombo(strQuerry, cboF1)
                AddEqualConditionCombo(strQuerry, cboF2)
                strQuerry = strQuerry & " order by FstUpdate"

                LoadDataGridView(dgrTktListing, strQuerry, Conn)

                cboSheetName.Enabled = False
                grbUpdate.Visible = True
            Case Else
                cboSheetName.Enabled = True
                If cboCustomer.Text = "LLOYDS REGISTER" Then
                    strQuerry = "Select RecId, InvID, DOI, CustFullName, TaxCode" _
                            & " , InvoiceNo, Period"
                    strQuerry = strQuerry & ",PmtDeadline, Status,GhiNoId,CustShortName, Address, FOP,Service" _
                            & ", FstUser, FstUpdate, LstUser, LstUpdate,ClearDate" _
                            & " FROM VatInv where Status='OK' and CustShortName='" & cboCustomer.Text & "'"
                    AddEqualConditionCombo(strQuerry, cboF1)
                    AddEqualConditionCombo(strQuerry, cboF2)
                    strQuerry = strQuerry & " order by FstUpdate"

                    LoadDataGridView(dgrTktListing, strQuerry, Conn)
                    grbUpdate.Visible = True
                Else
                    grbUpdate.Visible = False
                End If

        End Select
        dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        If dgrTktListing.RowCount > 0 Then
            With dgrTktListing
                .Columns("FOP").Width = 40
                .Columns("InvId").Width = 40
                .Columns("RecId").Width = 40
                .Columns("InvoiceNo").Width = 50
                .Columns("Status").Width = 50
            End With
        End If
        Return True
    End Function
    Private Sub cboCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomer.SelectedIndexChanged
        If mblnDataLoadCompleted Then
            LoadCombo(cboF1, "Select distinct F1 as Value from VatInv where CustShortName='" & cboCustomer.Text _
                      & "' order by F1", Conn)
            cboF1.SelectedIndex = -1
            LoadCombo(cboF2, "Select distinct F2 as Value from VatInv where CustShortName='" & cboCustomer.Text _
                      & "' order by F2", Conn)
            cboF2.SelectedIndex = -1
            lblF1.Text = ScalarToString("MISC", "VAL2", "CAT='VatInvDisplay' and Val1='F1' AND VAL='" _
                                        & cboCustomer.Text & "'")
            lblF2.Text = ScalarToString("MISC", "VAL2", "CAT='VatInvDisplay' and Val1='F2' AND VAL='" _
                                        & cboCustomer.Text & "'")

            If ScalarToInt("Misc", "RecId", "Status='OK' and CAT='CustNameInGroup' and Val='ABBOTT LOCAL' and Val1='" _
                           & cboCustomer.Text & "'") > 0 Then
                mstrCustGroup = "ABBOTT LOCAL"
            Else
                mstrCustGroup = ""
                Select Case cboCustomer.Text
                    Case "LLOYDS REGISTER"
                        lbkLoadFromRAS.Visible = True
                        txtTktItem.Visible = True
                    Case Else
                        lbkLoadFromRAS.Visible = False
                        txtTktItem.Visible = False
                End Select
            End If
            LoadGridInvoice()
            mstrAction = Action.LoadVatInv
        End If

    End Sub

    Private Sub lbkSelectAll_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectAll.LinkClicked
        Try
            Dim intCount As Integer
            For Each objRow As DataGridViewRow In dgrTktListing.Rows
                If objRow.Visible Then
                    objRow.Cells("S").Value = Not (objRow.Cells("S").Value)
                    intCount = intCount + 1
                    If intCount = cboSelectCount.Text Then
                        Exit For
                    End If
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub
    Private Function CreateVatInv4AbbottLocal() As Boolean
        Dim decNonVatable As Decimal = 0
        Dim decAmountNoVat As Decimal = 0
        Dim decVAT As Decimal = 0
        Dim decVatPct As Decimal = 0
        Dim decVatable As Decimal = 0
        Dim decAmountNoVatSf As Decimal = 0
        Dim decVATSf As Decimal = 0
        Dim decVatPctSf As Decimal = 0
        Dim decVatableSf As Decimal = 0
        Dim strDivision As String = String.Empty
        Dim strService As String = String.Empty

        mlstVatInvLinks.Clear()

        Dim strF2 As String = String.Empty
        Dim blnDivisionFound As Boolean

        Select Case mstrAction
            Case Action.LoadListing
                'tiep tuc
            Case Action.LoadVatInv
                CreateVatInvBlank()
                Return True
            Case Action.LoadTcode
                Return False
            Case Action.LoadTkt, Action.LoadTrx
                Return False
        End Select


        If cboF1.Text = "" Then
            MsgBox("You must select " & lblF1.Text)
            Return False
        End If

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Visible AndAlso objRow.Cells("S").Value Then
                Dim objLink As New clsVatInvLink
                objLink.LinkType = "TCODE"
                objLink.LinkRef = objRow.Cells("Mã giao dịch").Value
                objLink.LinkId = ScalarToInt("Dutoan_Tour", "Top 1 RecId", "Tcode='" & objRow.Cells("Mã giao dịch").Value & "'")
                objLink.RcpTcode = objRow.Cells("Mã giao dịch").Value
                mlstVatInvLinks.Add(objLink)

                If strService = "" Then
                    strService = objRow.Cells("Dịch vụ").Value
                End If

                decAmountNoVat = decAmountNoVat + objRow.Cells("Chi phí giao dịch").Value
                decVAT = decVAT + objRow.Cells("Thuế GTGT").Value
                decAmountNoVatSf = decAmountNoVatSf + objRow.Cells("Phí dịch vụ").Value
                decVATSf = decVATSf + objRow.Cells("Thuế GTGT  (Phí dịch vụ)").Value

                If strF2 = "" Then
                    strF2 = objRow.Cells("Month" & vbLf & "Of StartDate").Value
                ElseIf strF2 <> objRow.Cells("Month" & vbLf & "Of StartDate").Value Then
                    MsgBox("You must Select " & lblF2.Text)
                    Return False
                End If

                If Not blnDivisionFound Then
                    strDivision = objRow.Cells("DIVISION").Value
                    blnDivisionFound = True
                End If
            End If
        Next
        decAmountNoVat = Math.Round(decAmountNoVat)
        decAmountNoVatSf = Math.Round(decAmountNoVatSf)

        decVatable = decAmountNoVat + decVAT
        If decAmountNoVat <> 0 Then
            decVatPct = CInt(decVAT * 100 / decAmountNoVat)
        End If

        decVatableSf = decAmountNoVatSf + decVATSf
        If decAmountNoVatSf <> 0 Then
            decVatPctSf = CInt(decVATSf * 100 / decAmountNoVatSf)
        End If


        Dim frmEdit As New frmVatInvEdit(cboCustomer.Text, strService, Nothing, False)
        If mstrAction = Action.LoadListing Then
            Dim strDesc4FstLine = "Service Line"
            Select Case strService
                Case "Meal"
                    strDesc4FstLine = "Dịch vụ ăn uống"
                Case "Transfer"
                    strDesc4FstLine = "Dịch vụ thuê xe"
            End Select

            frmEdit.dgrInvDetails.Rows.Add({1, strDesc4FstLine, 0, decAmountNoVat + decAmountNoVatSf, decVatPct, decVAT + decVATSf _
                , decVatable + decVatableSf, decVatable + decVatableSf, True})
            frmEdit.VatInvLinks = mlstVatInvLinks
            frmEdit.txtF1.Text = cboF1.Text
            frmEdit.txtF2.Text = strDivision
            frmEdit.VatInvLinks = mlstVatInvLinks

        End If

        If frmEdit.ShowDialog = DialogResult.OK Then

            MsgBox("VAT Invoice Saved!")
            Return True
        End If
    End Function
    Private Function CreateVatInv4LloydsRegister() As Boolean
        Dim decNonVatable As Decimal = 0
        Dim decAmountNoVat As Decimal = 0
        Dim decVAT As Decimal = 0
        Dim decVatPct As Decimal = 0
        Dim decVatable As Decimal = 0
        Dim decAmountNoVatSf As Decimal = 0
        Dim decVATSf As Decimal = 0
        Dim decVatPctSf As Decimal = 0
        Dim decVatableSf As Decimal = 0
        Dim strDivision As String = String.Empty
        Dim strService As String = String.Empty

        mlstVatInvLinks.Clear()

        Select Case mstrAction
            Case Action.LoadListing
                Return False
            Case Action.LoadVatInv
                CreateVatInvBlank()
                Return True
            Case Action.LoadTcode
                strService = dgrTktListing.CurrentRow.Cells("Service").Value
            Case Action.LoadTkt, Action.LoadTrx
                strService = "ETK"
        End Select

        Dim frmEdit As New frmVatInvEdit(cboCustomer.Text, strService, Nothing, False)
        If mstrAction <> Action.LoadVatInv Then
            Dim intSeq As Integer = 1
            With dgrTktListing.CurrentRow
                Dim objLink As New clsVatInvLink
                frmEdit.txtBuyer.Text = .Cells("PaxName").Value

                Select Case mstrAction
                    Case Action.LoadAhc
                        objLink.LinkType = "TKT"
                        objLink.LinkRef = .Cells("Tkno").Value
                        objLink.LinkId = .Cells("Tkid").Value
                        objLink.RcpTcode = .Cells("RcpNo").Value

                        frmEdit.dgrInvDetails.Rows.Add({intSeq, "Phí dịch vụ ngoài giờ", 0, .Cells("SfNoVat_VND").Value _
                                        , Math.Round(.Cells("VAT4SF").Value * 100 / .Cells("SfNoVat_VND").Value) _
                                        , Math.Round(.Cells("VAT4SF").Value) _
                                        , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value) _
                                        , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value), False})


                        intSeq = intSeq + 1
                    Case Action.LoadTkt, Action.LoadTrx
                        objLink.LinkType = "TKT"
                        objLink.LinkRef = .Cells("Tkno").Value
                        objLink.LinkId = .Cells("Tkid").Value
                        objLink.RcpTcode = .Cells("RcpNo").Value
                        If .Cells("Fare_VND").Value = 0 Then
                            frmEdit.dgrInvDetails.Rows.Add({1, "Phí đổi vé máy bay", 0, 0, 0, 0, 0, 0, False})
                        ElseIf txtInvAmt.Text < 0 Then
                            frmEdit.dgrInvDetails.Rows.Add({1, "Tiền hoàn vé máy bay", 0, .Cells("Fare_VND").Value _
                                    , Math.Round(.Cells("VAT4Fare_VND").Value * 100 / .Cells("Fare_VND").Value) _
                                    , Math.Round(.Cells("VAT4Fare_VND").Value) _
                                    , Math.Round(.Cells("Fare_VND").Value + .Cells("VAT4Fare_VND").Value) _
                                    , Math.Round(.Cells("Fare_VND").Value + .Cells("VAT4Fare_VND").Value), True})
                        Else
                            frmEdit.dgrInvDetails.Rows.Add({1, "Tiền vé máy bay", 0, .Cells("Fare_VND").Value _
                                    , Math.Round(.Cells("VAT4Fare_VND").Value * 100 / .Cells("Fare_VND").Value) _
                                    , Math.Round(.Cells("VAT4Fare_VND").Value) _
                                    , Math.Round(.Cells("Fare_VND").Value + .Cells("VAT4Fare_VND").Value) _
                                    , Math.Round(.Cells("Fare_VND").Value + .Cells("VAT4Fare_VND").Value), True})
                        End If
                        intSeq = intSeq + 1

                        frmEdit.dgrInvDetails.Rows.Add({intSeq, .Cells("Tkno").Value, 0, 0, 0, 0, 0, 0, False})
                        intSeq = intSeq + 1

                        frmEdit.dgrInvDetails.Rows.Add({intSeq, .Cells("Itinerary").Value, 0, 0, 0, 0, 0, 0, False})
                        intSeq = intSeq + 1

                        If .Cells("PhiKhac").Value > 0 Then
                            If txtInvAmt.Text >= 0 Then
                                frmEdit.dgrInvDetails.Rows.Add({1, "Phí khác", 0, .Cells("PhiKhac").Value _
                                                           , Math.Round(.Cells("Vat4PhiKhac").Value * 100 / .Cells("PhiKhac").Value) _
                                                           , .Cells("Vat4PhiKhac").Value _
                                                           , .Cells("PhiKhac").Value + .Cells("Vat4PhiKhac").Value _
                                                           , .Cells("PhiKhac").Value + .Cells("Vat4PhiKhac").Value, False})
                            Else
                                frmEdit.dgrInvDetails.Rows.Add({1, "Phí hoàn vé", 0, .Cells("PhiKhac").Value _
                                                           , Math.Round(.Cells("Vat4PhiKhac").Value * 100 / .Cells("PhiKhac").Value) _
                                                           , .Cells("Vat4PhiKhac").Value _
                                                           , .Cells("PhiKhac").Value + .Cells("Vat4PhiKhac").Value _
                                                           , .Cells("PhiKhac").Value + .Cells("Vat4PhiKhac").Value, False})
                            End If

                            intSeq = intSeq + 1
                        End If

                        If .Cells("ThuHo").Value <> 0 Then
                            If txtInvAmt.Text >= 0 Then
                                frmEdit.dgrInvDetails.Rows.Add({intSeq, "Các khoản thu hộ khác", 0, .Cells("ThuHo").Value _
                                            , Math.Round(.Cells("VAT4ThuHo").Value * 100 / .Cells("ThuHo").Value) _
                                            , Math.Round(.Cells("VAT4ThuHo").Value) _
                                            , Math.Round(.Cells("ThuHo").Value + .Cells("VAT4ThuHo").Value) _
                                            , Math.Round(.Cells("ThuHo").Value + .Cells("VAT4ThuHo").Value), False})
                            Else
                                frmEdit.dgrInvDetails.Rows.Add({intSeq, "Tiền hoàn các khoản thu hộ khác", 0, .Cells("ThuHo").Value _
                                            , Math.Round(.Cells("VAT4ThuHo").Value * 100 / .Cells("ThuHo").Value) _
                                            , Math.Round(.Cells("VAT4ThuHo").Value) _
                                            , Math.Round(.Cells("ThuHo").Value + .Cells("VAT4ThuHo").Value) _
                                            , Math.Round(.Cells("ThuHo").Value + .Cells("VAT4ThuHo").Value), False})
                            End If

                            intSeq = intSeq + 1
                        End If

                        If txtInvAmt.Text >= 0 Then
                            frmEdit.dgrInvDetails.Rows.Add({intSeq, "Phần thu dịch vụ bán vé", 0, .Cells("SfNoVat_VND").Value _
                                            , Math.Round(.Cells("VAT4SF").Value * 100 / .Cells("SfNoVat_VND").Value) _
                                            , Math.Round(.Cells("VAT4SF").Value) _
                                            , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value) _
                                            , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value), False})
                        Else
                            Dim decAmt As Decimal
                            If .Cells("SfNoVat_VND").Value <> 0 Then
                                decAmt = .Cells("VAT4SF").Value * 100 / .Cells("SfNoVat_VND").Value
                            End If

                            frmEdit.dgrInvDetails.Rows.Add({intSeq, "Phần thu dịch vụ hoàn vé", 0, .Cells("SfNoVat_VND").Value _
                                            , Math.Round(decAmt) _
                                            , Math.Round(.Cells("VAT4SF").Value) _
                                            , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value) _
                                            , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value), False})
                        End If

                        intSeq = intSeq + 1

                    Case Action.LoadTcode
                        objLink.LinkType = "TCODE"
                        objLink.LinkRef = .Cells("Tcode").Value
                        objLink.LinkId = .Cells("Dutoanid").Value
                        objLink.RcpTcode = .Cells("Tcode").Value

                        Dim strServiceDesc As String
                        Select Case .Cells("Service").Value
                            Case "Accommodations"
                                strServiceDesc = "Tiền phòng khách sạn"
                            Case "Transfer"
                                strServiceDesc = "Thuê xe"
                            Case Else
                                strServiceDesc = .Cells("Service").Value
                        End Select
                        If .Cells("AmountNoVat").Value <> 0 Then
                            frmEdit.dgrInvDetails.Rows.Add({1, strServiceDesc, 0, .Cells("AmountNoVat").Value _
                                    , .Cells("VatPct").Value _
                                    , .Cells("Vat").Value _
                                    , .Cells("AmountNoVat").Value + .Cells("Vat").Value _
                                    , .Cells("AmountNoVat").Value + .Cells("Vat").Value, True})
                            intSeq = intSeq + 1
                            frmEdit.dgrInvDetails.Rows.Add({intSeq, .Cells("Supplier").Value, 0, 0, 0, 0, 0, 0, False})
                            intSeq = intSeq + 1
                        End If

                        frmEdit.dgrInvDetails.Rows.Add({intSeq, "Phí dịch vụ", 0, .Cells("SfNoVat_VND").Value _
                                            , .Cells("VatPct4SF").Value _
                                            , Math.Round(.Cells("VATSF").Value) _
                                            , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VATSF").Value) _
                                            , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VATSF").Value) _
                                            , (.Cells("AmountNoVat").Value = 0)})
                        intSeq = intSeq + 1
                        If .Cells("AmountNoVat").Value = 0 Then
                            frmEdit.dgrInvDetails.Rows.Add({intSeq, .Cells("Supplier").Value, 0, 0, 0, 0, 0, 0, False})
                            intSeq = intSeq + 1
                        End If

                        If Not IsDBNull(.Cells("BankFee").Value) AndAlso .Cells("BankFee").Value <> 0 Then
                            Dim decBankFeeNoVat As Decimal = Math.Round(.Cells("BankFee").Value * 100 / (100 + .Cells("VatPct4BankFee").Value))
                            Dim decVat4BankFee As Decimal = .Cells("BankFee").Value - decBankFeeNoVat

                            frmEdit.dgrInvDetails.Rows.Add({intSeq, "Phí chuyển khoản", 0 _
                                                           , decBankFeeNoVat _
                                                           , .Cells("VatPct4BankFee").Value _
                                                           , decVat4BankFee, .Cells("BankFee").Value, .Cells("BankFee").Value, False})
                            intSeq = intSeq + 1
                        End If
                End Select

                frmEdit.dgrInvDetails.Rows.Add({intSeq, "One World Number:" & .Cells("OneWorldNumber").Value, 0, 0, 0, 0, 0, 0, False})
                intSeq = intSeq + 1
                If .Cells("ProjectTaskId").Value <> "" AndAlso .Cells("ProjectTaskId").Value <> "N/A" Then
                    frmEdit.dgrInvDetails.Rows.Add({intSeq, "Cost Center:N/A", 0, 0, 0, 0, 0, 0, False})
                Else
                    frmEdit.dgrInvDetails.Rows.Add({intSeq, "Cost Center:" & .Cells("CostCenter").Value, 0, 0, 0, 0, 0, 0, False})
                End If
                intSeq = intSeq + 1

                frmEdit.dgrInvDetails.Rows.Add({intSeq, "Approver:" & .Cells("Approver").Value, 0, 0, 0, 0, 0, 0, False})
                intSeq = intSeq + 1

                If .Cells("ProjectTaskId").Value <> "" AndAlso .Cells("ProjectTaskId").Value <> "N/A" Then
                    frmEdit.dgrInvDetails.Rows.Add({intSeq, "ProjectTaskId/ServiceOrderID:" & .Cells("ProjectTaskId").Value, 0, 0, 0, 0, 0, 0, False})
                    intSeq = intSeq + 1
                End If

                If .Cells("MastNumber").Value <> "" AndAlso .Cells("MastNumber").Value <> "N/A" Then
                    frmEdit.dgrInvDetails.Rows.Add({intSeq, "Mast Number:" & .Cells("MastNumber").Value, 0, 0, 0, 0, 0, 0, False})
                    intSeq = intSeq + 1
                End If

                If .Cells("VesselName").Value <> "" AndAlso .Cells("VesselName").Value <> "N/A" Then
                    frmEdit.dgrInvDetails.Rows.Add({intSeq, "Vessel Name:" & .Cells("VesselName").Value, 0, 0, 0, 0, 0, 0, False})
                    intSeq = intSeq + 1
                End If

                mlstVatInvLinks.Add(objLink)
                frmEdit.VatInvLinks = mlstVatInvLinks
            End With
        End If

        If frmEdit.ShowDialog = DialogResult.OK Then
            Return True
            MsgBox("VAT Invoice Saved!")
        End If


    End Function
    Private Function CreateVatInvBlank() As Boolean
        Dim strService As String
        Dim tblService As DataTable = GetDataTable("Select Val as Service from MISC order by Val")
        Dim frmSelectSvc As New frmShowTableContent(tblService, "Select Service", "Service")
        If frmSelectSvc.DialogResult = DialogResult.OK Then
            strService = frmSelectSvc.SelectedValue
            Dim frmEdit As New frmVatInvEdit(cboCustomer.Text, strService, Nothing, False)
            If frmEdit.ShowDialog = DialogResult.OK Then
                MsgBox("VAT Invoice Saved!")
            End If
        End If

        Return True
    End Function
    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim blnClearPreviousSelection As Boolean

        mlstVatInvLinks.Clear()

        If cboCustomer.Text = "LLOYDS REGISTER" Then
            If CreateVatInv4LloydsRegister() Then
                blnClearPreviousSelection = True
            End If

        ElseIf mstrCustGroup = "ABBOTT LOCAL" Then
            If CreateVatInv4AbbottLocal() Then
                blnClearPreviousSelection = True
            End If
        Else
            CreateVatInvBlank()
        End If

        If blnClearPreviousSelection Then
            Select Case mstrAction
                Case Action.LoadTkt, Action.LoadTrx, Action.LoadTcode, Action.LoadListing
                    Dim i As Integer
                    For i = dgrTktListing.Rows.Count - 1 To 0 Step -1
                        Dim objRow As DataGridViewRow = dgrTktListing.Rows(i)
                        If objRow.Cells("S").Value Then
                            dgrTktListing.Rows.RemoveAt(i)
                        End If
                    Next
            End Select

        End If

    End Sub

    Private Sub lbkClone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone.LinkClicked
        Dim frmEdit As New frmVatInvEdit(cboCustomer.Text, dgrTktListing.CurrentRow.Cells("Service").Value, dgrTktListing.CurrentRow, False)
        If frmEdit.ShowDialog = DialogResult.OK Then
            LoadGridInvoice()
        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        Dim frmEdit As New frmVatInvEdit(cboCustomer.Text, dgrTktListing.CurrentRow.Cells("Service").Value, dgrTktListing.CurrentRow, True)
        If frmEdit.ShowDialog = DialogResult.OK Then
            LoadGridInvoice()
        End If
    End Sub

    Private Sub dgrTktListing_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrTktListing.CellContentClick

        If mblnDataLoadCompleted AndAlso dgrTktListing.CurrentRow IsNot Nothing AndAlso mstrAction = Action.LoadVatInv Then
            dtpNewPmtDeadLine.Value = dgrTktListing.CurrentRow.Cells("PmtDeadline").Value
            LoadGridDetails(dgrTktListing.CurrentRow.Cells("InvId").Value)

        End If
        If dgrTktListing.Columns(e.ColumnIndex).Name = "S" Then
            grbUpdate.Visible = False
        End If
    End Sub
    Private Function LoadGridDetails(intInvId As Integer) As Boolean
        Dim strQuerry As String = "Select * from VatInvAmts where Status='OK' and InvId=" & intInvId
        LoadDataGridView(dgrInvDetails, strQuerry, Conn)
        dgrInvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        With dgrInvDetails
            .Columns("TotalNonVatable").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TotalNonVatable").DefaultCellStyle.Format = "#,##0"
            .Columns("TotalNonVatable").Width = 90
            .Columns("AmountNoVat").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("AmountNoVat").DefaultCellStyle.Format = "#,##0"
            .Columns("AmountNoVat").Width = 80
            .Columns("VAT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("VAT").DefaultCellStyle.Format = "#,##0"
            .Columns("VAT").Width = 70
            .Columns("TotalVatable").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TotalVatable").DefaultCellStyle.Format = "#,##0"
            .Columns("TotalVatable").Width = 80
            .Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("Total").DefaultCellStyle.Format = "#,##0"
            .Columns("Total").Width = 80
            .Columns("VatPct").Width = 40
            .Columns("InvId").Width = 40
            .Columns("Status").Width = 50
            .Columns("RecId").Width = 40
            .Columns("InvId").Width = 40
            .Columns("Seq").Width = 40
        End With
        Return True
    End Function

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If mstrAction = Action.LoadVatInv Then
            If DeleteGridViewRow(dgrTktListing, ChangeStatus_ByID("VatInv", "XX" _
                                                , dgrTktListing.CurrentRow.Cells("RecId").Value), Conn) Then
                Dim lstQuerries As New List(Of String)
                lstQuerries.Add(ChangeStatus_ByDK("VatInvAmts", "XX", "InvID=" & dgrTktListing.CurrentRow.Cells("RecId").Value))
                lstQuerries.Add(ChangeStatus_ByDK("VatInvLinks", "XX", "VatInvID=" & dgrTktListing.CurrentRow.Cells("RecId").Value))
                UpdateListOfQuerries(lstQuerries, Conn)
                LoadGridInvoice()
            End If
        End If

    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        LoadGridInvoice()
    End Sub
    Private Function Clear() As Boolean
        cboF1.SelectedIndex = -1
        cboF2.SelectedIndex = -1
        cboRasDoc.SelectedIndex = 0
        txtTktItem.Text = ""
        Return True
    End Function
    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub lbkUpdatePmtDeadline_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUpdatePmtDeadline.LinkClicked
        If dgrTktListing.CurrentRow Is Nothing Then Exit Sub
        Dim strQuerry As String = "Update VatInv Set PmtDeadline='" & CreateFromDate(dtpNewPmtDeadLine.Value) _
            & "', LstUpdate=getdate(),LstUser='" & myStaff.SICode & "' where RecId=" _
            & dgrTktListing.CurrentRow.Cells("RecId").Value

        If ExecuteNonQuerry(strQuerry, Conn) Then
            LoadGridInvoice()
        End If

    End Sub
    Private Function HideGridRow() As Boolean
        If mstrAction = Action.LoadListing AndAlso cboF1.Text <> "" Then
            dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            If cboF2.Text = "" Then

                For Each objRow As DataGridViewRow In dgrTktListing.Rows

                    If cboF1.Text <> objRow.Cells("Người đặt").Value Then
                        objRow.Visible = False
                        objRow.Cells("S").Value = False
                    Else
                        objRow.Visible = True
                        If cboF2.FindStringExact(objRow.Cells("Month" & vbLf & "of StartDate").Value) = -1 Then
                            cboF2.Items.Add(objRow.Cells("Month" & vbLf & "of StartDate").Value)
                        End If
                    End If
                Next

            Else
                For Each objRow As DataGridViewRow In dgrTktListing.Rows

                    If cboF1.Text <> objRow.Cells("Người đặt").Value _
                        Or cboF2.Text <> objRow.Cells("Month" & vbLf & "of StartDate").Value Then
                        objRow.Visible = False
                    Else
                        objRow.Visible = True
                    End If
                Next
            End If
            dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        End If
    End Function
    Private Sub cboF1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboF1.SelectedIndexChanged
        Try
            cboF2.Items.Clear()
        Catch ex As Exception

        End Try

        HideGridRow()

    End Sub
    Private Function LoadF2() As Boolean
        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Visible Then
                If cboF2.Text <> objRow.Cells("Month" & vbLf & "of StartDate").Value Then
                    objRow.Visible = False
                End If
            End If
        Next
    End Function
    Private Sub cboF2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboF2.SelectedIndexChanged
        If mstrAction = Action.LoadListing AndAlso cboF2.Text <> "" Then
            HideGridRow()
        End If
    End Sub

    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub lbkLoadTktTrx_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkLoadFromRAS.LinkClicked
        Dim strQuerry As String
        Dim tblTkt As System.Data.DataTable
        Dim strCondition As String = " where t.Status<>'XX' and r.CustShortName='" & cboCustomer.Text & "'"
        Dim decVatRatio As Decimal = 1.1

        dgrTktListing.DataSource = Nothing
        dgrTktListing.Columns.Clear()
        Dim colS As New DataGridViewCheckBoxColumn
        colS.Name = "S"
        colS.DataPropertyName = "S"
        dgrTktListing.Columns.Add(colS)

        Select Case cboRasDoc.Text
            Case "AHC"
                decVatRatio = GetVatRatio(ScalarToDate("Tkt", "TOP 1 DOI", "Status<>'xx' and tkno='" & txtTktItem.Text & "'"))
                strQuerry = "Select 'True' as S, t.RecId as tkid, t.tkno,t.Itinerary,t.PaxName" _
                    & " ,0 as Fare_VND,0 as VAT4Fare_VND" _
                    & ", 0  as ThuHo,0 as Vat4ThuHo,t.ChargeTV/" & decVatRatio & "as SfNoVat_VND" _
                    & ",t.ChargeTV- (t.ChargeTV/" & decVatRatio & ") as Vat4SF" _
                    & " , 0  as PhiKhac, 0  as Vat4PhiKhac,tr.Ref1 as OneWorldNumber,tr.Ref2 as CostCenter" _
                    & ",tr.Ref4 As Approver,'' as ProjectTaskId,'' as MastNumber,'' as VesselName ,tr.RequiredData" _
                    & ", t.ChargeTV As TotalVND, '',t.DocType as Service,t.RcpNo" _
                    & ",l.VatInvID ,t.Srv,t.DOI from tkt t " _
                    & " left join Rcp r on t.rcpid=r.Recid " _
                    & " left join cwt.dbo.GO_MiscSvc a on a.ItemId=t.RecId " _
                    & " left join cwt.dbo.go_travel tr On a.travelid=tr.RecId And tr.Status='OK' " _
                    & " left join vatinvlinks l on l.vatinvid=r.recid and l.linktype='ETK' and l.Status='ok'" _
                    & strCondition & " and t.tkno='" & txtTktItem.Text & "'"
                mstrAction = Action.LoadAhc
            Case "TKT"
                decVatRatio = GetVatRatio(ScalarToDate("TOP 1 Tkt", "DOI", "Status<>'xx' and tkno='" & txtTktItem.Text & "'"))
                strQuerry = "Select 'True' as S, r1.tkid, t.tkno,t.Itinerary,t.PaxName,r1.F_VND as Fare_VND,r1.VAT4Fare_VND" _
                    & ", round((case Dekho when 'DOM' then r1.T_VND-r1.VAT4Fare_VND else r1.T_VND end) - VAT4TfcNoUE_VND,0)  as ThuHo" _
                    & ",round(r1.VAT4TfcNoUE_VND,0) as Vat4ThuHo,r1.SfNoVat_VND,r1.TVCharge_VND-r1.SfNoVat_VND as Vat4SF" _
                    & ", round((case Dekho when 'DOM' then r1.C_VND/" & decVatRatio & " else r1.C_VND end),0)  as PhiKhac" _
                    & ", round((case Dekho when 'DOM' then r1.C_VND - r1.C_VND/" & decVatRatio & " else 0 end),0)  as Vat4PhiKhac" _
                    & ",tr.Ref1 as OneWorldNumber,tr.Ref2 as CostCenter,tr.Ref4 as Approver" _
                    & ",'' as ProjectTaskId,'' as MastNumber,'' as VesselName ,tr.RequiredData" _
                    & ", F_VND+T_VND +C_VND+TVCharge_VND as TotalVND, R1.DEKHO,t.DocType as Service" _
                    & ",l.VatInvID,t.RcpNo,t.Srv,t.DOI" _
                    & " from tkt t" _
                    & " left join Rcp r on t.rcpid=r.Recid" _
                    & " left join ReportData r1 on t.recid=r1.tkid" _
                    & " left join cwt.dbo.go_air a on a.Tkid=t.RecId" _
                    & " left join cwt.dbo.go_travel tr on a.travelid=tr.RecId and tr.Status='OK'"
                strQuerry = strQuerry _
                    & " left join vatinvlinks l on l.vatinvid=r.recid and l.linktype='ETK' and l.Status='ok'" _
                    & strCondition & " and t.tkno='" & txtTktItem.Text & "'"
                mstrAction = Action.LoadTkt
            Case "TRX"
                decVatRatio = GetVatRatio(ScalarToDate("Tkt", "top 1 DOI", "Status<>'xx' and RcpId =(select RecId from Rcp where status='OK' and RcpNo='" & txtTktItem.Text & "')"))

                strQuerry = "Select 'True' as S, r1.tkid, t.tkno,t.Itinerary,t.PaxName,r1.F_VND as Fare_VND,r1.VAT4Fare_VND" _
                    & ", round((case Dekho when 'DOM' then r1.T_VND-r1.VAT4Fare_VND else r1.T_VND end) - VAT4TfcNoUE_VND,0)  as ThuHo" _
                    & ",round(r1.VAT4TfcNoUE_VND,0) as Vat4ThuHo,r1.SfNoVat_VND,r1.TVCharge_VND-r1.SfNoVat_VND as Vat4SF" _
                    & ", round((case Dekho when 'DOM' then r1.C_VND/" & decVatRatio & " else r1.C_VND end),0)  as PhiKhac" _
                    & ", round((case Dekho when 'DOM' then r1.C_VND - r1.C_VND/" & decVatRatio & " else 0 end),0)  as Vat4PhiKhac" _
                    & ",tr.Ref1 as OneWorldNumber,tr.Ref2 as CostCenter,tr.Ref4 as Approver" _
                    & ",'' as ProjectTaskId,'' as MastNumber,'' as VesselName ,tr.RequiredData" _
                    & ", F_VND+T_VND +C_VND+TVCharge_VND as TotalVND, R1.DEKHO,t.DocType as Service" _
                    & ",l.VatInvID,t.RcpNo,t.Srv,t.DOI" _
                    & " from tkt t" _
                    & " left join Rcp r on t.rcpid=r.Recid" _
                    & " left join ReportData r1 on t.recid=r1.tkid" _
                    & " left join cwt.dbo.go_air a on a.Tkid=t.RecId" _
                    & " left join cwt.dbo.go_travel tr on a.travelid=tr.RecId and tr.Status='OK'"
                strQuerry = strQuerry _
                    & " left join vatinvlinks l on l.vatinvid=r.recid and l.linktype='TRX' and l.Status='ok'" _
                    & strCondition & " and t.rcpno='" & txtTktItem.Text & "'"
                mstrAction = Action.LoadTrx
            Case "TCODE"
                strQuerry = "select 'True' as S , i.RecId as ItemId,i.PaxName, i.service,i.Supplier" _
                    & ",round(i.TTLToPax*100/(100+i.VAT),0) as AmountNoVAT" _
                    & ",i.VAT as VatPct" _
                    & ",(case i.vat when 0 then 0 else i.TTLToPax-round(i.TTLToPax*100/(100+i.VAT),0) end) as Vat" _
                    & ",round(f.TTLToPax*100/(100+f.VAT),0) as SfNoVat_VND" _
                    & ",f.VAT as VatPct4SF" _
                    & ",f.TTLToPax-round(f.TTLToPax*100/(100+f.VAT),0) as VatSf" _
                    & " ,b.TTLToPax as BankFee,b.VAT as VatPct4BankFee" _
                    & " ,(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='ONE WORLD NUMBER') as OneWorldNumber" _
                    & " ,(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='COST CENTER') as CostCenter" _
                    & " ,(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='APPROVER/CLIENT NAME') as Approver" _
                    & " ,(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='PROJECT TASK ID') as ProjectTaskId" _
                    & " ,(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='MAST NUMBER') as MastNumber" _
                    & ",(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='VESSEL NAME') as VesselName" _
                    & ",(i.TTLToPax+ isnull(f.TtlToPax,0)) as TotalVND,i.Dutoanid,t.Tcode,t.RcpId" _
                    & " from dutoan_item i" _
                    & " left join dutoan_tour t on i.DuToanID=t.RecID" _
                    & " left join dutoan_item f on i.RecID=f.RelatedItem and f.Service='TransViet SVC Fee' and f.Status='ok'" _
                    & " left join dutoan_item b on i.RecID=b.RelatedItem and b.Service='Bank Fee' and b.Status='ok'" _
                    & " where t.custshortname ='" & cboCustomer.Text & "'and t.Status='rr'" _
                    & " and i.Status='OK' and i.Service not in ('TransViet SVC Fee','Bank Fee')" _
                    & " and i.CostOnly=0 and t.Tcode='" & txtTktItem.Text & "'"

                mstrAction = Action.LoadTcode
        End Select


        tblTkt = GetDataTable(strQuerry, Conn)

        If tblTkt.Rows.Count = 0 Then
            MsgBox("Unable to get data from RAS!")
            Exit Sub
        Else
            dgrTktListing.DataSource = tblTkt
            If mstrAction <> Action.LoadTcode Then
                If Not IsDBNull(tblTkt.Rows(0)("VatInvId").Value) Then
                    MsgBox("Vat Invoice had been issued for " & txtTktItem.Text)
                End If
                For Each objRow As DataGridViewRow In dgrTktListing.Rows
                    If mstrAction <> Action.LoadAhc AndAlso objRow.Cells("Service").Value = "AHC" Then
                        MsgBox("Cần load AHC với số AHC tương ứng!")
                        Exit Sub
                    End If
                    If Not IsDBNull(objRow.Cells("RequiredData").Value) Then
                        objRow.Cells("ProjectTaskId").Value = GetRequiredDataValueByDataCode("UDID32", objRow.Cells("RequiredData").Value)
                        objRow.Cells("MastNumber").Value = GetRequiredDataValueByDataCode("UDID18", objRow.Cells("RequiredData").Value)
                        objRow.Cells("VesselName").Value = GetRequiredDataValueByDataCode("UDID30", objRow.Cells("RequiredData").Value)
                    End If
                Next
            End If

        End If
        grbUpdate.Visible = False
        RefreshListing()
    End Sub

    Private Sub txtTkt_Leave(sender As Object, e As EventArgs) Handles txtTktItem.Leave
        txtTktItem.Text = txtTktItem.Text.Trim

        If txtTktItem.Text.StartsWith(cboCustomer.Text) Then
            cboRasDoc.Text = "TCODE"
        ElseIf txtTktItem.Text.StartsWith("TS") Then
            cboRasDoc.Text = "TRX"
        ElseIf txtTktItem.Text.StartsWith("AHC") Then
            cboRasDoc.Text = "AHC"
        Else
            cboRasDoc.Text = "TKT"
            txtTktItem.Text = FormatRasTkno(txtTktItem.Text)
        End If
    End Sub

    Private Sub txtInvAmt_Enter(sender As Object, e As EventArgs) Handles txtInvAmt.Enter

        RefreshInvAmt(mstrAction = Action.LoadTcode)

    End Sub

    Private Sub txtInvAmt_Leave(sender As Object, e As EventArgs) Handles txtInvAmt.Leave
        grbUpdate.Visible = True
    End Sub
    Private Function RefreshInvAmt(Optional blnIncludeBankFee As Boolean = False) As Boolean
        Dim decInvAmt As Decimal
        Dim strTotalVndColumn As String = String.Empty

        For Each objCol As DataGridViewColumn In dgrTktListing.Columns
            Select Case objCol.Name
                Case "TotalVND", "Tổng tiền VNĐ"
                    strTotalVndColumn = objCol.Name
                    Exit For
            End Select
        Next
        If strTotalVndColumn = "" Then
            MsgBox("Unable to find TotalVND column to calculate Total Invoice Amt!")
            Return False
        End If
        Try
            For Each objRow As DataGridViewRow In dgrTktListing.Rows
                If IsDBNull(objRow.Cells("S").Value) Or IsDBNull(objRow.Cells(strTotalVndColumn).Value) Then

                ElseIf objRow.Cells("S").Value Then
                    decInvAmt = decInvAmt + objRow.Cells(strTotalVndColumn).Value
                    If blnIncludeBankFee AndAlso Not IsDBNull(objRow.Cells("BankFee").Value) Then
                        decInvAmt = decInvAmt + objRow.Cells("BankFee").Value
                    End If
                End If
            Next
            txtInvAmt.Text = Format(decInvAmt, "#,##0")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return True
    End Function

    Private Function RefreshListing() As Boolean
        For Each objCol As DataGridViewColumn In dgrTktListing.Columns
            Select Case objCol.Name
                Case "PhiKhac", "Vat4PhiKhac"
                    objCol.DefaultCellStyle.Format = "#,##0"
            End Select
            If objCol.Name <> "S" Then
                objCol.ReadOnly = True
            ElseIf cboCustomer.Text = "LLOYDS REGISTER" Then
                objCol.ReadOnly = True
            End If
        Next
        dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    End Function

    Private Sub lbkCreateE_Invoice_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateE_Invoice.LinkClicked
        If dgrTktListing.CurrentRow Is Nothing Then
            Exit Sub
        End If

        Select Case cboCustomer.Text


            Case "HEMPEL"
                Dim intRowCount As Integer

                If cboSheetName.Text.ToUpper.StartsWith("NON AIR") Then
                    Dim strDomInt As String = String.Empty
                    For Each objRow As DataGridViewRow In dgrTktListing.Rows
                        If objRow.Cells("S").Value Then
                            intRowCount = intRowCount + 1
                            If Not objRow.Cells("Service Type").Value.ToString.Contains("Fee") Then
                                If strDomInt = "" Then
                                    If objRow.Cells("VAT for Expense").Value = 0 Then
                                        strDomInt = "INT"
                                    Else
                                        strDomInt = "DOM"
                                    End If
                                ElseIf strDomInt = "INT" AndAlso objRow.Cells("VAT for Expense").Value > 0 Then
                                    MsgBox("Can not combine DOM & INT in one Invoice")
                                    Exit Sub
                                ElseIf strDomInt = "DOM" AndAlso objRow.Cells("VAT for Expense").Value = 0 Then
                                    MsgBox("Can not combine DOM & INT in one Invoice")
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next
                    Dim frmE_Inv As New frmE_InvEdit
                    frmE_Inv.LoadGridNonAirHempel(dgrTktListing, cboCustomer.Text, strDomInt, mintVatDiscount)
                    frmE_Inv.ShowDialog()
                Else
                    Dim strSrv As String
                    If cboSheetName.Text.ToUpper.StartsWith("HOTLINE") Then
                        strSrv = "S"
                    Else
                        strSrv = dgrTktListing.Rows(0).Cells("SRV").Value
                        For Each objRow As DataGridViewRow In dgrTktListing.Rows
                            If objRow.Cells("S").Value Then
                                intRowCount = intRowCount + 1
                            End If
                            'If strSrv <> objRow.Cells("SRV").Value Then
                            '    MsgBox("Can not combine Sale & Refund in one Invoice")
                            '    Exit Sub
                            'End If
                        Next
                        Select Case intRowCount
                            Case > 12
                                MsgBox("Can not select more than 12 lines")
                                Exit Sub
                            Case 12
                                If dgrTktListing.Rows(0).Cells("Sheet").Value = "INT" Then
                                    MsgBox("Can not select more than 11 lines")
                                    Exit Sub
                                End If

                        End Select
                    End If

                    Dim frmE_Inv As New frmE_InvEdit
                    frmE_Inv.LoadGridTktsAirHempel(dgrTktListing, cboSheetName.Text, cboCustomer.Text, mintVatDiscount)
                    frmE_Inv.ShowDialog()

                End If
                'Case "ATOTECH", "INGER"  '^_^20220802 mark by 7643
            Case "ATOTECH", "INGER", "HASBRO"  '^_^20220802 modi by 7643
                Select Case cboSheetName.Text.ToUpper
                    Case "DOM"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridTktsAirGeneric(dgrTktListing, cboCustomer.Text, "DOM", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "INT"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridTktsAirGeneric(dgrTktListing, cboCustomer.Text, "INT", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "NON AIR"
                        Dim frmE_Inv As New frmE_InvEdit
                        'frmE_Inv.LoadGridNonAir4JCI(dgrTktListing, cboCustomer.Text, True, mintVatDiscount)  '^_^20220802 mark by 7643
                        '^_^20220802 modi by 7643 -b-
                        If cboCustomer.Text = "HASBRO" Then
                            frmE_Inv.LoadGridNonAir4HASBRO(dgrTktListing, cboCustomer.Text, True, mintVatDiscount)
                        Else
                            frmE_Inv.LoadGridNonAirAtotech(dgrTktListing, cboCustomer.Text, True, mintVatDiscount)
                        End If
                        '^_^20220802 modi by 7643 -e-
                        frmE_Inv.ShowDialog()
                    Case "HOTLINE"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridAhc4NghiSon(dgrTktListing, cboCustomer.Text)
                        frmE_Inv.ShowDialog()
                End Select

            Case "EVN SPC"
                Dim intInvNo As Integer
                intInvNo = dgrTktListing.CurrentRow.Cells("INV No").Value

                Dim frmE_Inv As New frmE_InvEdit
                frmE_Inv.LoadGridTktsAir4EVN_SPC(dgrTktListing, cboCustomer.Text, intInvNo, mintVatDiscount)
                frmE_Inv.ShowDialog()

            Case "HOGAN HCM", "HOGAN HN"
                Select Case cboSheetName.Text.ToUpper
                    Case "DOM"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridTktsAirHogan(dgrTktListing, cboCustomer.Text, "DOM", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "INT"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridTktsAirHogan(dgrTktListing, cboCustomer.Text, "INT", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "NONAIR"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridNonAirHogan(dgrTktListing, cboCustomer.Text, True, mintVatDiscount)
                        frmE_Inv.ShowDialog()

                End Select

            Case "JCI VN"
                Select Case cboSheetName.Text.ToUpper
                    Case "DOM"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridTktsAir4JCI(dgrTktListing, cboCustomer.Text, "DOM".ToUpper, mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "INT"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridTktsAir4JCI(dgrTktListing, cboCustomer.Text, "INT", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "NON AIR"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridNonAir4JCI(dgrTktListing, cboCustomer.Text, False, mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "HOTLINE"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridAhc4NghiSon(dgrTktListing, cboCustomer.Text)
                        frmE_Inv.ShowDialog()
                End Select

            Case "LLOYDS REGISTER"
                CreateE_Inv4LloydsRegister(mintVatDiscount)

            Case "NGHISON VN"
                Select Case cboSheetName.Text
                    Case "Vé nội địa "
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridTktsAir4NGHISON(dgrTktListing, cboCustomer.Text, "DOM", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "Vé quốc tế"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridTktsAir4NGHISON(dgrTktListing, cboCustomer.Text, "INT", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "Phòng nội địa"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridHotel4NghiSon(dgrTktListing, cboCustomer.Text, "DOM", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "Phòng quốc tế"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridHotel4NghiSon(dgrTktListing, cboCustomer.Text, "INT", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "Xe quốc tế"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridCar4NghiSon(dgrTktListing, cboCustomer.Text, "INT", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                    Case "Xe nội địa"
                        Dim frmE_Inv As New frmE_InvEdit
                        frmE_Inv.LoadGridCar4NghiSon(dgrTktListing, cboCustomer.Text, "DOM", mintVatDiscount)
                        frmE_Inv.ShowDialog()
                End Select

            Case "ABBOTT 3A ANI_M", "ABBOTT ALSA ANI_MICE", "ABBOTT ALSA_MICE", "ABBOTT LA HCP", "ABBOTT RO", "ABBOTT RO_MICE", "NOVARTIS VN" _
                , "NOVARTIS_MICE", "NVS_ MICE", "SANDOZ NOVARTIS", "SDZ_MICE", "ABBOTT 3A", "ABBOTT 3A_MICE", "KHLE"  '^_^20220930 add KHLE by 7643  '^_^20220919 add "ABBOTT 3A", "ABBOTT 3A_MICE" by 7643

                Dim frmE_Inv As New frmE_InvEdit
                If mintVatDiscount = 30 AndAlso dgrTktListing.CurrentRow.Cells("VatPct").Value <> 7 Then
                    MsgBox("Không chọn đúng bản ghi Có mức giảm thuế")
                    Exit Sub

                End If
                frmE_Inv.LoadGridInvFromExcel(dgrTktListing, cboCustomer.Text, dgrTktListing.CurrentRow.Cells("STT").Value, mintVatDiscount)
                frmE_Inv.ShowDialog()

            Case "NGOCMAI_VJ", "PHUONGNAM_VJ", "SAOMAI_VJ", "THANHHOANG_VJ"

                Dim frmE_Inv As New frmE_InvEdit
                frmE_Inv.LoadGridTktsAir4THANHHOANG_VJ(dgrTktListing, cboCustomer.Text, mintVatDiscount)
                frmE_Inv.ShowDialog()

                '^_^20220929 add by 7643 -b-
            Case "PATH VN"
                Dim frmE_Inv As New frmE_InvEdit
                Select Case Mid(cboSheetName.Text, 1, 4)
                    Case "DOM-"
                        MsgBox("Tạo hóa đơn cho Project Code " & dgrTktListing.CurrentRow.Cells("Project Code").Value)
                        frmE_Inv.LoadGridTktsAirPATHVN(dgrTktListing, cboCustomer.Text, "DOM" _
                                                       , dgrTktListing.CurrentRow.Cells("Project Code").Value)
                    Case "INT-"
                        MsgBox("Tạo hóa đơn cho Project Code " & dgrTktListing.CurrentRow.Cells("Project Code").Value)
                        frmE_Inv.LoadGridTktsAirPATHVN(dgrTktListing, cboCustomer.Text, "INT" _
                                                       , dgrTktListing.CurrentRow.Cells("Project Code").Value)
                    Case "MISC"
                    Case "NONA"
                        Dim strDomInt As String
                        MsgBox("Tạo hóa đơn cho Project Code " & dgrTktListing.CurrentRow.Cells("Project Code").Value)

                        strDomInt = InputBox("D/I for Domestic/International", "D").ToUpper
                        Select Case strDomInt
                            Case "D"
                                frmE_Inv.LoadGridNonAirPathVN(dgrTktListing, cboCustomer.Text, "DOM" _
                                                       , dgrTktListing.CurrentRow.Cells("Project Code").Value _
                                                       , mintVatDiscount)
                            Case "I"
                                frmE_Inv.LoadGridNonAirPathVN(dgrTktListing, cboCustomer.Text, "INT" _
                                                        , dgrTktListing.CurrentRow.Cells("Project Code").Value _
                                                        , mintVatDiscount)
                            Case Else
                                'BO QUA
                        End Select

                End Select
                frmE_Inv.ShowDialog()

        End Select

        'End If

    End Sub
    Private Function CreateE_Inv4LloydsRegister(intVatDiscount As Integer) As Boolean
        Dim decNonVatable As Decimal = 0
        Dim decAmountNoVat As Decimal = 0
        Dim decVAT As Decimal = 0
        Dim decVatPct As Decimal = 0
        Dim decVatable As Decimal = 0
        Dim decAmountNoVatSf As Decimal = 0
        Dim decVATSf As Decimal = 0
        Dim decVatPctSf As Decimal = 0
        Dim decVatableSf As Decimal = 0
        Dim strDivision As String = String.Empty
        Dim strService As String = String.Empty

        mlstVatInvLinks.Clear()

        Select Case mstrAction
            Case Action.LoadListing
                Return False
            Case Action.LoadVatInv
                CreateVatInvBlank()
                Return True
            Case Action.LoadTcode
                strService = dgrTktListing.CurrentRow.Cells("Service").Value
            Case Action.LoadTkt, Action.LoadTrx
                strService = "ETK"
        End Select

        Dim frmEdit As New frmE_InvEdit
        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Cells("S").Value Then
                If mstrAction <> Action.LoadVatInv Then
                    Select Case mstrAction
                        Case Action.LoadAhc, Action.LoadTkt, Action.LoadTrx
                            frmEdit.LoadGridTktsAir4Lloyd(dgrTktListing, cboCustomer.Text, intVatDiscount)
                        Case Action.LoadTcode
                            frmEdit.LoadGridNonAir4Lloyd(dgrTktListing, cboCustomer.Text, intVatDiscount)
                    End Select
                End If

                frmEdit.ShowDialog()
                Return True
            End If
        Next



        'Select Case mstrAction
        '            Case Action.LoadAhc

        '                frmEdit.dgrInvDetails.Rows.Add({intSeq, "Phí dịch vụ ngoài giờ", 0, .Cells("SfNoVat_VND").Value _
        '                                , Math.Round(.Cells("VAT4SF").Value * 100 / .Cells("SfNoVat_VND").Value) _
        '                                , Math.Round(.Cells("VAT4SF").Value) _
        '                                , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value) _
        '                                , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value), False})


        '    Case Action.LoadTkt, Action.LoadTrx
        '        If .Cells("Fare_VND").Value = 0 Then
        '                    frmEdit.dgrInvDetails.Rows.Add({1, "Phí đổi vé máy bay", 0, 0, 0, 0, 0, 0, False})
        '                ElseIf txtInvAmt.Text < 0 Then
        '                    frmEdit.dgrInvDetails.Rows.Add({1, "Tiền hoàn vé máy bay", 0, .Cells("Fare_VND").Value _
        '                            , Math.Round(.Cells("VAT4Fare_VND").Value * 100 / .Cells("Fare_VND").Value) _
        '                            , Math.Round(.Cells("VAT4Fare_VND").Value) _
        '                            , Math.Round(.Cells("Fare_VND").Value + .Cells("VAT4Fare_VND").Value) _
        '                            , Math.Round(.Cells("Fare_VND").Value + .Cells("VAT4Fare_VND").Value), True})
        '                Else
        '                    frmEdit.dgrInvDetails.Rows.Add({1, "Tiền vé máy bay", 0, .Cells("Fare_VND").Value _
        '                            , Math.Round(.Cells("VAT4Fare_VND").Value * 100 / .Cells("Fare_VND").Value) _
        '                            , Math.Round(.Cells("VAT4Fare_VND").Value) _
        '                            , Math.Round(.Cells("Fare_VND").Value + .Cells("VAT4Fare_VND").Value) _
        '                            , Math.Round(.Cells("Fare_VND").Value + .Cells("VAT4Fare_VND").Value), True})
        '                End If
        '                intSeq = intSeq + 1

        '                frmEdit.dgrInvDetails.Rows.Add({intSeq, .Cells("Tkno").Value, 0, 0, 0, 0, 0, 0, False})
        '                intSeq = intSeq + 1

        '                frmEdit.dgrInvDetails.Rows.Add({intSeq, .Cells("Itinerary").Value, 0, 0, 0, 0, 0, 0, False})
        '                intSeq = intSeq + 1

        '                If .Cells("PhiKhac").Value > 0 Then
        '                    If txtInvAmt.Text >= 0 Then
        '                        frmEdit.dgrInvDetails.Rows.Add({1, "Phí khác", 0, .Cells("PhiKhac").Value _
        '                                                   , Math.Round(.Cells("Vat4PhiKhac").Value * 100 / .Cells("PhiKhac").Value) _
        '                                                   , .Cells("Vat4PhiKhac").Value _
        '                                                   , .Cells("PhiKhac").Value + .Cells("Vat4PhiKhac").Value _
        '                                                   , .Cells("PhiKhac").Value + .Cells("Vat4PhiKhac").Value, False})
        '                    Else
        '                        frmEdit.dgrInvDetails.Rows.Add({1, "Phí hoàn vé", 0, .Cells("PhiKhac").Value _
        '                                                   , Math.Round(.Cells("Vat4PhiKhac").Value * 100 / .Cells("PhiKhac").Value) _
        '                                                   , .Cells("Vat4PhiKhac").Value _
        '                                                   , .Cells("PhiKhac").Value + .Cells("Vat4PhiKhac").Value _
        '                                                   , .Cells("PhiKhac").Value + .Cells("Vat4PhiKhac").Value, False})
        '                    End If

        '                    intSeq = intSeq + 1
        '                End If

        '                If .Cells("ThuHo").Value <> 0 Then
        '                    If txtInvAmt.Text >= 0 Then
        '                        frmEdit.dgrInvDetails.Rows.Add({intSeq, "Các khoản thu hộ khác", 0, .Cells("ThuHo").Value _
        '                                    , Math.Round(.Cells("VAT4ThuHo").Value * 100 / .Cells("ThuHo").Value) _
        '                                    , Math.Round(.Cells("VAT4ThuHo").Value) _
        '                                    , Math.Round(.Cells("ThuHo").Value + .Cells("VAT4ThuHo").Value) _
        '                                    , Math.Round(.Cells("ThuHo").Value + .Cells("VAT4ThuHo").Value), False})
        '                    Else
        '                        frmEdit.dgrInvDetails.Rows.Add({intSeq, "Tiền hoàn các khoản thu hộ khác", 0, .Cells("ThuHo").Value _
        '                                    , Math.Round(.Cells("VAT4ThuHo").Value * 100 / .Cells("ThuHo").Value) _
        '                                    , Math.Round(.Cells("VAT4ThuHo").Value) _
        '                                    , Math.Round(.Cells("ThuHo").Value + .Cells("VAT4ThuHo").Value) _
        '                                    , Math.Round(.Cells("ThuHo").Value + .Cells("VAT4ThuHo").Value), False})
        '                    End If

        '                    intSeq = intSeq + 1
        '                End If

        '                If txtInvAmt.Text >= 0 Then
        '                    frmEdit.dgrInvDetails.Rows.Add({intSeq, "Phần thu dịch vụ bán vé", 0, .Cells("SfNoVat_VND").Value _
        '                                    , Math.Round(.Cells("VAT4SF").Value * 100 / .Cells("SfNoVat_VND").Value) _
        '                                    , Math.Round(.Cells("VAT4SF").Value) _
        '                                    , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value) _
        '                                    , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value), False})
        '                Else
        '                    frmEdit.dgrInvDetails.Rows.Add({intSeq, "Phần thu dịch vụ hoàn vé", 0, .Cells("SfNoVat_VND").Value _
        '                                    , Math.Round(.Cells("VAT4SF").Value * 100 / .Cells("SfNoVat_VND").Value) _
        '                                    , Math.Round(.Cells("VAT4SF").Value) _
        '                                    , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value) _
        '                                    , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VAT4SF").Value), False})
        '                End If

        '                intSeq = intSeq + 1

        '            Case Action.LoadTcode

        '                Dim strServiceDesc As String
        '                Select Case .Cells("Service").Value
        '                    Case "Accommodations"
        '                        strServiceDesc = "Tiền phòng khách sạn"
        '                    Case "Transfer"
        '                        strServiceDesc = "Thuê xe"
        '                    Case Else
        '                        strServiceDesc = .Cells("Service").Value
        '                End Select
        '                If .Cells("AmountNoVat").Value <> 0 Then
        '                    frmEdit.dgrInvDetails.Rows.Add({1, strServiceDesc, 0, .Cells("AmountNoVat").Value _
        '                            , .Cells("VatPct").Value _
        '                            , .Cells("Vat").Value _
        '                            , .Cells("AmountNoVat").Value + .Cells("Vat").Value _
        '                            , .Cells("AmountNoVat").Value + .Cells("Vat").Value, True})
        '                    intSeq = intSeq + 1
        '                    frmEdit.dgrInvDetails.Rows.Add({intSeq, .Cells("Supplier").Value, 0, 0, 0, 0, 0, 0, False})
        '                    intSeq = intSeq + 1
        '                End If

        '                frmEdit.dgrInvDetails.Rows.Add({intSeq, "Phí dịch vụ", 0, .Cells("SfNoVat_VND").Value _
        '                                    , .Cells("VatPct4SF").Value _
        '                                    , Math.Round(.Cells("VATSF").Value) _
        '                                    , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VATSF").Value) _
        '                                    , Math.Round(.Cells("SfNoVat_VND").Value + .Cells("VATSF").Value) _
        '                                    , (.Cells("AmountNoVat").Value = 0)})
        '                intSeq = intSeq + 1
        '                If .Cells("AmountNoVat").Value = 0 Then
        '                    frmEdit.dgrInvDetails.Rows.Add({intSeq, .Cells("Supplier").Value, 0, 0, 0, 0, 0, 0, False})
        '                    intSeq = intSeq + 1
        '                End If

        '                If Not IsDBNull(.Cells("BankFee").Value) AndAlso .Cells("BankFee").Value <> 0 Then
        '                    Dim decBankFeeNoVat As Decimal = Math.Round(.Cells("BankFee").Value * 100 / (100 + .Cells("VatPct4BankFee").Value))
        '                    Dim decVat4BankFee As Decimal = .Cells("BankFee").Value - decBankFeeNoVat

        '                    frmEdit.dgrInvDetails.Rows.Add({intSeq, "Phí chuyển khoản", 0 _
        '                                                   , decBankFeeNoVat _
        '                                                   , .Cells("VatPct4BankFee").Value _
        '                                                   , decVat4BankFee, .Cells("BankFee").Value, .Cells("BankFee").Value, False})
        '                    intSeq = intSeq + 1
        '                End If
        '        End Select

        '        frmEdit.dgrInvDetails.Rows.Add({intSeq, "One World Number:" & .Cells("OneWorldNumber").Value, 0, 0, 0, 0, 0, 0, False})
        '        intSeq = intSeq + 1
        '        If .Cells("ProjectTaskId").Value <> "" AndAlso .Cells("ProjectTaskId").Value <> "N/A" Then
        '            frmEdit.dgrInvDetails.Rows.Add({intSeq, "Cost Center:N/A", 0, 0, 0, 0, 0, 0, False})
        '        Else
        '            frmEdit.dgrInvDetails.Rows.Add({intSeq, "Cost Center:" & .Cells("CostCenter").Value, 0, 0, 0, 0, 0, 0, False})
        '        End If
        '        intSeq = intSeq + 1

        '        frmEdit.dgrInvDetails.Rows.Add({intSeq, "Approver:" & .Cells("Approver").Value, 0, 0, 0, 0, 0, 0, False})
        '        intSeq = intSeq + 1

        '        If .Cells("ProjectTaskId").Value <> "" AndAlso .Cells("ProjectTaskId").Value <> "N/A" Then
        '            frmEdit.dgrInvDetails.Rows.Add({intSeq, "ProjectTaskId/ServiceOrderID:" & .Cells("ProjectTaskId").Value, 0, 0, 0, 0, 0, 0, False})
        '            intSeq = intSeq + 1
        '        End If

        '        If .Cells("MastNumber").Value <> "" AndAlso .Cells("MastNumber").Value <> "N/A" Then
        '            frmEdit.dgrInvDetails.Rows.Add({intSeq, "Mast Number:" & .Cells("MastNumber").Value, 0, 0, 0, 0, 0, 0, False})
        '            intSeq = intSeq + 1
        '        End If

        '        If .Cells("VesselName").Value <> "" AndAlso .Cells("VesselName").Value <> "N/A" Then
        '            frmEdit.dgrInvDetails.Rows.Add({intSeq, "Vessel Name:" & .Cells("VesselName").Value, 0, 0, 0, 0, 0, 0, False})
        '            intSeq = intSeq + 1
        '        End If

        '    End With
        'End If

        'If frmEdit.ShowDialog = DialogResult.OK Then
        '    Return True

        'End If


    End Function

    Private Sub lbkSwitchVatPct_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSwitchVatPct.LinkClicked
        Select Case cboVatPct.Text
            Case "VAT_NoDiscount"
                SetUpValues(0)
            Case "VAT7"
                SetUpValues(30)
        End Select
    End Sub
End Class