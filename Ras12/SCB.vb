Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
'Imports System.ComponentModel

Public Class SCB
    Dim myPath As String = Application.StartupPath
    Private mstrBankName As String
    Private mstrExportType As String

    Public Sub New(strBankName As String, Optional strExportType As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mstrBankName = strBankName
        mstrExportType = strExportType
        Me.Text = "TransViet Travel :: RAS 12 :." & strBankName & " Straight2Bank"
    End Sub
    Private Sub SCB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strQuerryAccount As String = "select recID as VAL, shortname + ': ' + Cur  as DIS " _
                    & " from Vendor where substring(Bank,1,3) ='" & mstrBankName & "'"
        LoadGridBatch()
        LoadCmb_VAL(Me.CmbAcctToExp, strQuerryAccount)
        LoadGridUNC("OK")

        If mstrBankName = "CTB" Then
            Dim tblMatchPayment4Citi As DataTable
            tblMatchPayment4Citi = GetDataTable("Select '' as Error,p.RecId,p.AccountName ,p.AccountNumber" _
                                    & ",p.BankName,p.Amount,o.Amount as CitiAmount" _
                                    & " from [UNC_Payments] p" _
                                    & " left join [OBLog] o on p.RefNo=o.[Ref_TV]" _
                                    & " where p.status='E0' and ScbNo=''" _
                                    & " and [PayerAccountID] in (5430,5431,5432,5433)")

            If tblMatchPayment4Citi.Rows.Count > 0 Then
                For Each objRow As DataRow In tblMatchPayment4Citi.Rows
                    If IsDBNull(objRow("CitiAmount")) Then
                        objRow("Error") = "Unprocessed Transaction"
                    ElseIf objRow("Amount") <> objRow("CitiAmount") Then
                        objRow("Error") = "Unmatched Amount"
                    Else
                        'update status
                        ChangeStatus_ByID("[UNC_Payments]", "OK", objRow("RecId"))
                    End If
                Next
                Dim frmShow As New frmShowTableContent(tblMatchPayment4Citi, "Last Export/Process Result for CITI")
                frmShow.ShowDialog()
            End If

        End If


    End Sub
    Private Sub LoadGridBatch()
        Dim strQuerry As String = "select Distinct SCBNo from UNC_Payments" _
                                  & " where ((status='OK' and SCBNo <>'') " _
                                  & " or (status='E0' and SCBNo='" & mstrBankName & "'))"

        Select Case mstrBankName
            Case "SCB"
                strQuerry = strQuerry & " and substring(SCBNo,1,3)='C00'"
            Case "CTB"
                strQuerry = strQuerry & " and LEN(SCBNo)=9 AND ISNUMERIC(SCBNO)=1"
            Case Else
                strQuerry = strQuerry & " and substring(SCBNo,1,3)='" & mstrBankName & "'"
        End Select
        strQuerry = strQuerry & "order by SCBNo"

        Me.GridBatch.DataSource = GetDataTable(strQuerry)
        Me.LblDelete.Visible = False
        Me.GridUNCinBatch.DataSource = Nothing
    End Sub
    Private Sub LoadGridUNC(pStatus As String, Optional strExportType As String = "")
        Try
            Dim StrDKDate As String = "17-Nov-14"
            Dim strQuerry As String
            Dim strExportTypeFilter As String = ""
            If Me.CmbAcctToExp.SelectedValue <> 1143 Then StrDKDate = "29-Feb-16"

            If mstrBankName = "VCB" Then
                StrDKDate = "24 Jun 17"
            ElseIf mstrBankName = "CTB" Then
                StrDKDate = "10 Apr 17"
            ElseIf myStaff.City = "SGN" AndAlso mstrBankName = "TCB" Then
                strExportType = ""
            End If

            Select Case mstrBankName
                Case "CTB"
                    strQuerry = "select p.RecID,Curr, Amount, p.ShortName, p.AccountName, p.AccountNumber" _
                            & ", p.BankName, p.BankAddress, p.Description" _
                            & ",isnull(m.Val,'') as CitiBankCode,m.Val1 as CitiBankName,BankInVietnam,p.RefNo,p.Swift" _
                            & " from UNC_payments p" _
                            & " left join misc m on p.bankname=m.val2" _
                            & " and m.Status='OK' and m.cat='citibankmap'" _
                            & " left join Vendor a on p.PayeeAccountID=a.recid" _
                            & " where payerAccountID =" & Me.CmbAcctToExp.SelectedValue _
                            & " and (scbNo='' or SCBNo='" & mstrBankName _
                            & "') and hasPrinted=0 and p.fstupdate >'" & StrDKDate _
                            & "' and p.status='" & pStatus _
                            & "' and a.status='ok'"
                Case "TCB"
                    strQuerry = "select p.RecID, Amount, ShortName, AccountName, AccountNumber, p.BankName, BankAddress, Description,RefNo" _
                                & ",i.BankName as BankNameByTCB,  b.Province,b.Branch" _
                                & " from UNC_payments p " _
                                & " left join lib.dbo.BankMap m on p.PayeeAccountID = m.VendorId" _
                                & " left join lib.dbo.BankBranches b on m.BranchId=b.RecID" _
                                & " left join lib.dbo.BankIDs i on i.RecID=b.BankID" _
                                & " where payerAccountID =" & CmbAcctToExp.SelectedValue _
                                & " and (scbNo='' or SCBNo='" & mstrBankName & "') and hasPrinted=0 and p.fstupdate >'01 Nov 21' and p.status='" & pStatus & "'"

                Case Else
                    strQuerry = "select RecID, Amount, ShortName, AccountName, AccountNumber, BankName, BankAddress, Description,RefNo " _
                            & " from UNC_payments where payerAccountID =" & Me.CmbAcctToExp.SelectedValue _
                            & " and (scbNo='' or SCBNo='" & mstrBankName _
                            & "') and hasPrinted=0 and fstupdate >'" & StrDKDate _
                            & "' and status='" & pStatus & "'"
            End Select

            strQuerry = strQuerry & " and SingleBtf='False'"
            Me.GridUNC.DataSource = GetDataTable(strQuerry)
            Me.GridUNC.Columns("RecID").Visible = False
            Me.GridUNC.Columns("Amount").Width = 75
            Me.GridUNC.Columns("ShortName").Width = 128
            Me.GridUNC.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Me.GridUNC.Columns("Amount").DefaultCellStyle.Format = "#,##0.0"
        Catch ex As Exception

        End Try
    End Sub
    Private Sub lblUpdateBatchNo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblUpdateBatchNo.LinkClicked
        UpdateBatchNo(txtLotNo.Text.ToUpper.Trim)
        If Me.ChkPending.Checked Then
            LoadGridUNC("E0")
        Else
            LoadGridUNC("OK")
        End If
    End Sub
    Private Function UpdateBatchNo(strBatchNbr As String) As Boolean
        If mstrBankName <> "VPB" Then
            Dim chckLotIDExist As Integer = ScalarToInt("UNC_payments", "RecID" _
                                                    , "payerAccountID=" & Me.CmbAcctToExp.SelectedValue _
                                                    & " and SCBNo='" & strBatchNbr & "'")
            If chckLotIDExist > 0 Then
                MsgBox("Batch No. You Entered Already Exists.", MsgBoxStyle.Critical, msgTitle)
                Return False
            End If
        End If
        If mstrBankName = "CTB" AndAlso Not CheckFormatTextBox(txtLotNo, True, 9, 9) Then
            MsgBox("Invalid batch number for CITI")
            Return False
        End If

        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        cmd.CommandText = "update UNC_payments set status='OK', SCBNo='" & strBatchNbr _
            & "' where payerAccountID=" & Me.CmbAcctToExp.SelectedValue _
            & " and status='E0' and SCBNo='" & mstrBankName & "'"
        cmd.ExecuteNonQuery()

        'If mstrBankName.Contains("SCB") Then
        '    CreateSms4UNC(strBatchNbr, mstrBankName, True)
        'End If
        Return True
    End Function
    Private Function ExportTcbInside() As Boolean
        Dim strExportFileName As String = "R12_2TCB_Inside.xlt"
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWbk As Excel.Workbook
        Dim objWsh As Excel.Worksheet
        Dim intRowVp As Integer = 22
        Dim intRowNonVp As Integer = 26
        Dim intRow As Integer = 2

        objWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath _
                                                     & "\" & strExportFileName, True)
        objExcel.Visible = True
        objWsh = objWbk.ActiveSheet
        With objWsh
            For Each objRow As DataGridViewRow In GridUNC.Rows
                If objRow.Cells("S").Value Then
                    .Range("A" & intRow).Value = "'" & Replace(objRow.Cells("AccountNumber").Value, " ", "")
                    .Range("B" & intRow).Value = objRow.Cells("Amount").Value
                    .Range("C" & intRow).Value = ReplaceSpecialChrsTcb(Mid(objRow.Cells("Description").Value, 1, 210))
                    intRow = intRow + 1
                End If
            Next
            .Activate()
        End With

    End Function
    Private Function ExportTcbOutside() As Boolean
        Dim strExportFileName As String = "R12_2TCB_Outside.xlt"
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWbk As Excel.Workbook
        Dim objWsh As Excel.Worksheet
        Dim intRow As Integer = 2

        objWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath _
                                                     & "\" & strExportFileName, True)
        objExcel.Visible = True
        objWsh = objWbk.ActiveSheet
        With objWsh
            For Each objRow As DataGridViewRow In GridUNC.Rows
                If objRow.Cells("S").Value Then
                    .Range("A" & intRow).Value = objRow.Cells("RefNo").Value
                    .Range("B" & intRow).Value = objRow.Cells("Amount").Value
                    .Range("C" & intRow).Value = objRow.Cells("AccountName").Value
                    .Range("D" & intRow).Value = "'" & Replace(objRow.Cells("AccountNumber").Value, " ", "")
                    .Range("E" & intRow).Value = ReplaceSpecialChrsTcb(Mid(objRow.Cells("Description").Value, 1, 210))
                    .Range("F" & intRow).Value = objRow.Cells("BankNameByTCB").Value
                    .Range("G" & intRow).Value = objRow.Cells("Province").Value
                    .Range("H" & intRow).Value = objRow.Cells("Branch").Value
                    intRow = intRow + 1
                End If
            Next
            .Activate()
        End With
    End Function
    Private Sub LblExport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblExport.LinkClicked
        'Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim lstQuerries As New List(Of String)
        Dim strExportFileName As String = String.Empty

        Dim tmpRecID As Integer = ScalarToInt("UNC_Payments", "Top 1 RECID", "status='E0' and SCBNo='' and " & _
                                              "PayerAccountID=" & Me.CmbAcctToExp.SelectedValue)
        If tmpRecID > 0 Then
            MsgBox("Lot No Already Exists. Plz Check Your Input", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        For i As Int16 = 0 To Me.GridUNC.RowCount - 1
            If Me.GridUNC.Item("S", i).Value = True Then
                If mstrBankName = "CTB" Then
                    If GridUNC.Item("BankInVietnam", i).Value = "Y" _
                        AndAlso GridUNC.Item("CitiBankCode", i).Value = "" Then
                        MsgBox("Missing CitiBankCode for " & GridUNC.Item("BankName", i).Value)
                        Exit Sub
                    ElseIf ContainSpecialChar4Citi(GridUNC.Item("BankName", i).Value) Then
                        MsgBox(GridUNC.Item("BankName", i).Value & " contains prohibited characters")
                        Exit Sub
                    End If
                Else
                    lstQuerries.Add("update UNC_payments set status='E0', ScbNo='" _
                                & mstrBankName & "' where recid=" & Me.GridUNC.Item("RecID", i).Value)
                End If
            End If
        Next
        If mstrBankName <> "CTB" AndAlso Not (lstQuerries.Count > 0 _
                AndAlso UpdateListOfQuerries(lstQuerries, Conn)) Then
            Exit Sub
        End If
        Select Case mstrBankName
            Case "SCB"
                If MySession.City = "SGN" Then
                    strExportFileName = "R12_2SCB_SGN.xlt"
                Else
                    strExportFileName = "R12_2SCB.xlt"
                End If
                InHoaDon(myPath, strExportFileName, "V", "E0", Now.Date, Now.Date _
                         , Me.CmbAcctToExp.SelectedValue, 0, mstrBankName, "")
                InHoaDon(myPath, strExportFileName, "V", "E0", Now.Date, Now.Date _
                         , Me.CmbAcctToExp.SelectedValue, 0, mstrBankName, "")
            Case "TCB"
                If myStaff.City = "SGN" Then
                    If mstrExportType = "Inside" Then
                        ExportTcbInside()

                    ElseIf mstrExportType = "Outside" Then
                        ExportTcbOutside()
                    End If
                Else
                    strExportFileName = "R12_2TCB.xlt"
                    InHoaDon(myPath, strExportFileName, "V", "E0", Now.Date, Now.Date _
                         , Me.CmbAcctToExp.SelectedValue, 0, mstrBankName, "")
                End If
                UpdateBatchNo(GenerateBankPaymentBatchNbr(Conn, "TCB"))


            Case "VCB"
                If MySession.City = "SGN" Then
                    strExportFileName = "R12_2VCB_SGN.xlt"
                End If
                InHoaDon(myPath, strExportFileName, "V", "E0", Now.Date, Now.Date _
                         , Me.CmbAcctToExp.SelectedValue, 0, mstrBankName, "")
                UpdateBatchNo(GenerateBankPaymentBatchNbr(Conn, "VCB"))

            Case "VPB"
                If MySession.City = "SGN" Then
                    Dim objExcel As Excel.Application = CreateObject("Excel.Application")
                    Dim objWbk As Excel.Workbook
                    Dim objWsh As Excel.Worksheet
                    Dim tblPayerAccount As DataTable
                    Dim intCountRecords As Integer
                    Dim intRowVp As Integer = 21
                    Dim intRowNonVp As Integer = 24
                    Dim intRow As Integer
                    Dim decAmountVp As Decimal
                    Dim decAmountNonVp As Decimal
                    Dim blnVPB As Boolean

                    tblPayerAccount = GetDataTable("Select * from Vendor" _
                                                    & " where Status='OK' and Recid=" _
                                                    & CmbAcctToExp.SelectedValue, Conn)

                    'strExportFileName = "MB01 De nghi chuyen tien theo lo kiem Uy nhiem chi.xlsx"
                    strExportFileName = "R12_2VPB_SGN.xlt"

                    objWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath _
                                                     & "\" & strExportFileName, True)
                    objExcel.Visible = True
                    objWsh = objWbk.Sheets("Lệnh TT")
                    With objWsh
                        .Range("F4").Value = "Ngày lập*:" & Format(Now, "dd/MM/yyyy")
                        .Range("A9").Value = "Tên Khách hàng: " & tblPayerAccount.Rows(0)("AccountName")
                        .Range("A10").Value = "Mã KH (CIF):" & tblPayerAccount.Rows(0)("CIF")
                        .Range("A14").Value = "Số tài khoản trích tiền và phí:" & tblPayerAccount.Rows(0)("AccountNumber")
                        .Range("A15").Value = "Mở tại  VPBank: " & tblPayerAccount.Rows(0)("BankAddress")
                        .Range("A17").Value = "Ngày có giá trị thanh toán:" & Format(Now, "dd/MM/yyyy")
                        .Range("A11").Value = "Địa chỉ *:" & tblPayerAccount.Rows(0)("Address")

                        '.Range("E5").Value = "Ngày lập*:" & Format(Now, "dd/MM/yyyy")
                        '.Range("a10").Value = "Tên Khách hàng: " & tblPayerAccount.Rows(0)("AccountName")
                        '.Range("a11").Value = "Mã KH (CIF):" & tblPayerAccount.Rows(0)("CIF")
                        '.Range("a14").Value = "Số tài khoản trích tiền: " & Mid(tblPayerAccount.Rows(0)("AccountNumber"), 5).Trim
                        '.Range("a15").Value = "Mở tại  VPBank: " & tblPayerAccount.Rows(0)("Address")

                        For Each objRow As DataGridViewRow In GridUNC.Rows
                            If objRow.Cells("S").Value Then

                                intCountRecords = intCountRecords + 1
                                If intCountRecords > 50 Then
                                    MsgBox("Max 50 records are allowed!")
                                    Exit Sub
                                End If
                                Dim objBankName As New clsBank
                                objBankName.ParseName(objRow.Cells("BankName").Value)

                                If objBankName.BankName.Contains("VP BANK") _
                                    Or objBankName.BankName.Contains("VIET NAM THINH VUONG") Then
                                    blnVPB = True
                                    intRow = intRowVp
                                    decAmountVp = decAmountVp + objRow.Cells("Amount").Value
                                    objWsh.Rows(intRow).insert
                                    .Range("a" & intRow).Value = intRowVp - 20
                                Else
                                    blnVPB = False
                                    intRow = intRowNonVp
                                    decAmountNonVp = decAmountNonVp + objRow.Cells("Amount").Value
                                    objWsh.Rows(intRow).insert
                                    .Range("a" & intRow).Value = intRowNonVp - 24
                                End If


                                .Range("B" & intRow).Value = objRow.Cells("AccountName").Value
                                'If objRow.Cells("AccountName").Value.ToString.Length > 20 Then
                                '    .Rows(intRow).RowHeight = 32
                                'End If
                                .Range("C" & intRow).Value = "'" & objRow.Cells("AccountNumber").Value
                                .Range("D" & intRow).Value = objRow.Cells("Amount").Value
                                .Range("E" & intRow).Value = TienBangChu(objRow.Cells("Amount").Value) & " đồng."
                                .Range("F" & intRow).Value = objBankName.BankName

                                If Not blnVPB Then
                                    .Range("G" & intRow).Value = objBankName.Branch
                                    'Else
                                    '    .Range("G" & intRow).Value = objBankName.Branch
                                    'End If

                                    .Range("H" & intRow).Value = objRow.Cells("BankAddress").Value
                                End If
                                .Range("I" & intRow).Value = objRow.Cells("Description").Value
                                .Range("J" & intRow).Value = objRow.Cells("RefNo").Value
                                .Rows(intRow).Font.Bold = False

                                If blnVPB Then
                                        intRowVp = intRowVp + 1
                                    End If
                                    intRowNonVp = intRowNonVp + 1
                                End If
                        Next
                        If intCountRecords = 0 Then
                            MsgBox("No records selected!")
                        Else
                            'If decAmountVp <> 0 Then
                            '    .Range("D" & intRowVp).Value = Format(decAmountVp, "#,###")
                            'End If
                            'If decAmountNonVp <> 0 Then
                            '    .Range("D" & intRowNonVp).Value = Format(decAmountNonVp, "#,###")
                            'End If
                            .Range("a" & intRowNonVp + 1).Value = "Tổng số món chuyển (Bằng số): " & intCountRecords _
                                & " món (Bằng chữ: " & TienBangChu(intCountRecords) & " món)"
                                .Range("A" & intRowNonVp + 2).Value = "Tổng số tiền chuyển (Bằng số):" _
                                & Format(Math.Floor(decAmountVp + decAmountNonVp), "#,###") _
                                & " VND(Bằng chữ: " _
                                & TienBangChu(Math.Floor(decAmountVp + decAmountNonVp)) & " đồng)"

                        End If

                            .Columns("D").NumberFormat = "#,##0"
                    End With
                End If
                UpdateBatchNo(GenerateBankPaymentBatchNbr(Conn, "VPB"))

            Case "CTB"
                If MySession.City = "SGN" Then
                    Dim objExcel As Excel.Application = CreateObject("Excel.Application")
                    Dim objWbk As Excel.Workbook
                    Dim objWsh As Excel.Worksheet
                    Dim intXlsRow As Integer = 2
                    Dim tblPayerAccount As DataTable
                    Dim strPayerAccount As String
                    Dim strPayerName As String
                    Dim rgSpecialCharacters As New Regex("[-,*,?]")
                    tblPayerAccount = GetDataTable("Select AccountNumber,AccountName from Vendor" _
                                                    & " where Status='OK' and Recid=" _
                                                    & CmbAcctToExp.SelectedValue, Conn)
                    strPayerAccount = CleanAccountNbr(tblPayerAccount.Rows(0)("AccountNumber"))
                    strPayerName = tblPayerAccount.Rows(0)("AccountName")

                    strExportFileName = "CitiExcelConvertTool - With User Guide.xlsm"

                    objWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath _
                                                     & "\" & strExportFileName, True)
                    objExcel.Visible = True
                    objWsh = objWbk.Sheets("FT Temp")
                    For Each objRow As DataGridViewRow In GridUNC.Rows
                        If objRow.Cells("S").Value Then
                            
                        End If
                    Next
                    For Each objRow As DataGridViewRow In GridUNC.Rows
                        If objRow.Cells("S").Value Then
                            Dim strPayCode As String = String.Empty
                            Dim strBeneficiaryBankName As String = String.Empty
                            Dim strBankCode As String = String.Empty

                            intXlsRow = intXlsRow + 1
                            With objRow
                                If .Cells("BankInVietnam").Value = "N" Then
                                    strPayCode = "EFT"
                                    strBeneficiaryBankName = .Cells("BankName").Value & " " & .Cells("BankAddress").Value
                                    strBankCode = objRow.Cells("Swift").Value
                                ElseIf .Cells("BankInVietnam").Value = "Y" Then
                                    If .Cells("BankName").Value.ToString.Contains("CITI") Then
                                        strPayCode = "BKT"
                                    Else
                                        strPayCode = "DFT"
                                    End If
                                    strBeneficiaryBankName = .Cells("BankName").Value
                                    strBankCode = objRow.Cells("CitiBankCode").Value
                                End If
                            End With
                            With objWsh
                                .Range("A" & intXlsRow).Value = strPayCode
                                .Range("B" & intXlsRow).Value = Format(Now, "yyyyMMdd")
                                .Range("C" & intXlsRow).Value = objRow.Cells("RefNo").Value
                                .Range("D" & intXlsRow).Value = rgSpecialCharacters.Replace(objRow.Cells("AccountName").Value, " ")
                                .Range("F" & intXlsRow).Value = objRow.Cells("Amount").Value
                                .Range("G" & intXlsRow).Value = objRow.Cells("Curr").Value
                                .Range("H" & intXlsRow).Value = CleanAccountNbr(objRow.Cells("AccountNumber").Value)
                                .Range("I" & intXlsRow).Value = strBankCode
                                .Range("J" & intXlsRow).Value = strBeneficiaryBankName
                                .Range("L" & intXlsRow).Value = strPayerAccount
                                .Range("M" & intXlsRow).Value = objRow.Cells("Description").Value
                                .Range("O" & intXlsRow).Value = strPayerName

                            End With
                            lstQuerries.Add("update UNC_payments set status='E0', ScbNo='" _
                                & mstrBankName & "' where recid=" & objRow.Cells("RecID").Value)
                        End If
                    Next
                    UpdateListOfQuerries(lstQuerries, Conn)
                    objExcel.Visible = True
                End If

                'InHoaDon(myPath, strExportFileName, "V", "E0", Now.Date, Now.Date _
                '         , Me.CmbAcctToExp.SelectedValue, 0, mstrBankName, "")
                'UpdateBatchNo(GenerateBankPaymentBatchNbr(Conn, "CTB"))
        End Select

    End Sub
    Private Sub ChkPending_Click(sender As Object, e As EventArgs) Handles ChkPending.Click
        If Me.ChkPending.Checked Then
            LoadGridUNC("E0")
        Else
            LoadGridUNC("OK")
        End If
        'If mstrBankName = "CTB" Then
        '    LblUpdateBatchNo.Visible = False
        'Else
        Me.LblUpdateBatchNo.Enabled = Me.ChkPending.Checked
        'End If

        Me.txtLotNo.Enabled = Me.ChkPending.Checked
        Me.LblExport.Enabled = Not Me.ChkPending.Checked
    End Sub

    Private Sub GridBatch_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridBatch.CellClick
        If e.RowIndex < 0 Then Exit Sub
        Me.LblDelete.Visible = True
        Dim strDK As String = "status='E0'"
        If Me.GridBatch.CurrentRow.Cells("SCBNo").Value <> "" Then
            strDK = "status='OK' and SCBNo='" & Me.GridBatch.CurrentRow.Cells("SCBNo").Value & "'"
        End If
        Me.GridUNCinBatch.DataSource = GetDataTable("Select RecID, TRX_TC, Curr, Amount, InvNo, RMKNoibo, RefNo, ShortName as Beneficiary," & _
                                                     "AccountName, AccountNumber, BankName, BankAddress, PayerACcountID, Charge, Description," & _
                                                     "Swift, FstUpdate from UNC_payments where " & strDK)
        Me.GridUNCinBatch.Columns("Curr").Width = 32
        Me.GridUNCinBatch.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridUNCinBatch.Columns("Amount").DefaultCellStyle.Format = "#,##0.00"
    End Sub

    Private Sub LblDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblDelete.LinkClicked
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        If Me.GridBatch.CurrentRow.Cells("SCBNo").Value = "" Then
            cmd.CommandText = ChangeStatus_ByDK("UNC_Payments", "OK", "status='E0'")
        Else
            cmd.CommandText = "update Unc_payments set SCBNo='' where scbNo='" & Me.GridBatch.CurrentRow.Cells("SCBNo").Value & "'"
            If myStaff.City = "SGN" And InStr("HXT_NMH_SYS", myStaff.SICode) = 0 Then Exit Sub
            If myStaff.City = "HAN" And InStr("NTH_SYS", myStaff.SICode) = 0 Then Exit Sub
        End If
        cmd.ExecuteNonQuery()
        LoadGridBatch()
    End Sub

    Private Sub GridUNC_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridUNC.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        Dim KQ As Decimal = 0
        Dim intCountRecord As Integer

        Me.txtAmount.Focus()
        If e.ColumnIndex = 0 Then
            If mstrBankName = "TCB" Then
                If mstrExportType = "Inside" Then
                    If IsDBNull(GridUNC.Rows(e.RowIndex).Cells("BankNameByTCB").Value) Then
                        GridUNC.Rows(e.RowIndex).Cells("S").Value = False
                        MsgBox("You must Map Bank before export!")
                        Exit Sub
                    ElseIf GridUNC.Rows(e.RowIndex).Cells("BankNameByTCB").Value <> "TECHCOMBANK" Then
                        GridUNC.Rows(e.RowIndex).Cells("S").Value = False
                        MsgBox("You are NOT allowed to select payment Outside Techcom Bank with selected template")
                        Exit Sub
                    End If
                ElseIf mstrExportType = "Outside" Then

                    If IsDBNull(GridUNC.Rows(e.RowIndex).Cells("BankNameByTCB").Value) Then
                        GridUNC.Rows(e.RowIndex).Cells("S").Value = False
                        MsgBox("You must Map Bank before export!")
                        Exit Sub
                    ElseIf GridUNC.Rows(e.RowIndex).Cells("BankNameByTCB").Value = "TECHCOMBANK" Then
                        GridUNC.Rows(e.RowIndex).Cells("S").Value = False
                        MsgBox("You are NOT allowed to select payment Inside Techcom Bank with selected template")
                        Exit Sub
                    ElseIf LEN(GridUNC.Rows(e.RowIndex).Cells("AccountName").Value) > 65 Then
                        GridUNC.Rows(e.RowIndex).Cells("S").Value = False
                        MsgBox("Account Name must NOT exceed 65 characters!")
                        Exit Sub
                    End If

                End If
            End If
            For r As Int16 = 0 To Me.GridUNC.RowCount - 1
                If GridUNC.Item(0, r).Value Then
                    KQ = KQ + GridUNC.Item("Amount", r).Value
                    intCountRecord = intCountRecord + 1
                End If
            Next
        End If
        Me.txtAmount.Text = Format(KQ, "#,##0.00")
        txtCount.Text = intCountRecord
    End Sub

    Private Sub CmbAcctToExp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAcctToExp.SelectedIndexChanged
        If IsNumeric(CmbAcctToExp.SelectedValue) AndAlso ScalarToInt("UNC_payments", "RecId", "payerAccountID =" & CmbAcctToExp.SelectedValue _
                        & " and fstupdate >'29-Feb-16' and status='E0' and SingleBtf='False'") > 0 Then
            LblExport.Visible = False
        Else
            LblExport.Visible = True
        End If
        LoadGridUNC("OK")
    End Sub


End Class
