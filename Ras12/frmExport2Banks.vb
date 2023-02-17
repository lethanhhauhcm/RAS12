Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Public Class frmExport2Banks
    Dim myPath As String = Application.StartupPath
    Private mstrBankName As String
    Private mblnFirstLoadCompleted As Boolean

    Private Sub frmExport2Banks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboBanks.SelectedIndex = -1
        mblnFirstLoadCompleted = True

    End Sub
    Private Sub LoadGridBatch()
        Dim strQuerry As String = "select Distinct BatchNo from VendorPayments" _
                                  & " where ((status='OK' and BatchNo <>'') " _
                                  & " or (status='E0' and BatchNo='" & mstrBankName & "'))"

        Select Case mstrBankName
            Case "SCB"
                strQuerry = strQuerry & " and substring(BatchNo,1,3)='C00'"
            Case "CTB"
                strQuerry = strQuerry & " and LEN(BatchNo)=9 AND ISNUMERIC(BatchNo)=1"
            Case Else
                strQuerry = strQuerry & " and substring(BatchNo,1,3)='" & mstrBankName & "'"
        End Select
        strQuerry = strQuerry & "order by BatchNo"

        Me.GridBatch.DataSource = GetDataTable(strQuerry)
        Me.LblDelete.Visible = False
        Me.GridUNCinBatch.DataSource = Nothing
    End Sub
    Private Sub LoadGridUNC(pStatus As String)
        Try
            Dim strQuerry As String

            If mstrBankName = "CTB" Then
                strQuerry = "select p.RecID,Curr, Amount, p.ShortName, p.AccountName, p.AccountNumber" _
                            & ", p.BankName, p.BankAddress, p.Description" _
                            & ",isnull(m.Val,'') as CitiBankCode,m.Val1 as CitiBankName,BankInVietnam,p.RefNo,p.Swift" _
                            & " from VendorPayments p" _
                            & " left join misc m on p.bankname=m.val2" _
                            & " and m.Status='OK' and m.cat='citibankmap'" _
                            & " left join Vendor a on p.PayeeAccountID=a.recid" _
                            & " where payerAccountID =" & Me.CmbAcctToExp.SelectedValue _
                            & " and (BatchNo='' or BatchNo='" & mstrBankName _
                            & "') and hasPrinted=0 and p.status='" & pStatus _
                            & "' and a.status='ok'"
            Else
                strQuerry = "select RecID, Amount, ShortName, AccountName, AccountNumber, BankName, BankAddress, Description,RefNo " _
                            & " from VendorPayments where payerAccountID =" & Me.CmbAcctToExp.SelectedValue _
                            & " and (BatchNo='' or BatchNo='" & mstrBankName _
                            & "') and hasPrinted=0 and status='" & pStatus & "'"
            End If
            strQuerry = strQuerry & " and SingleBtf='False'"
            Me.dgrVendorPayments.DataSource = GetDataTable(strQuerry)
            Me.dgrVendorPayments.Columns("RecID").Visible = False
            Me.dgrVendorPayments.Columns("Amount").Width = 75
            Me.dgrVendorPayments.Columns("ShortName").Width = 128
            Me.dgrVendorPayments.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Me.dgrVendorPayments.Columns("Amount").DefaultCellStyle.Format = "#,##0.0"
        Catch ex As Exception

        End Try
    End Sub
    Private Function UpdateBatchNo(strBatchNbr As String) As Boolean
        If mstrBankName <> "VPB" Then
            Dim chckLotIDExist As Integer = ScalarToInt("VendorPayments", "RecID" _
                                                    , "payerAccountID=" & Me.CmbAcctToExp.SelectedValue _
                                                    & " and BatchNo='" & strBatchNbr & "'")
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
        cmd.CommandText = "update VendorPayments set status='OK', BatchNo='" & strBatchNbr _
            & "' where payerAccountID=" & Me.CmbAcctToExp.SelectedValue _
            & " and status='E0' and BatchNo='" & mstrBankName & "'"
        cmd.ExecuteNonQuery()

        Return True
    End Function
    Private Function Export2CTB() As Boolean
        Dim lstQuerries As New List(Of String)
        Dim strExportFileName As String = String.Empty
        For i As Int16 = 0 To Me.dgrVendorPayments.RowCount - 1
            If Me.dgrVendorPayments.Item("S", i).Value = True Then
                If dgrVendorPayments.Item("BankInVietnam", i).Value = "Y" _
                    AndAlso dgrVendorPayments.Item("CitiBankCode", i).Value = "" Then
                    MsgBox("Missing CitiBankCode for " & dgrVendorPayments.Item("BankName", i).Value)
                    Return False
                ElseIf ContainSpecialChar4Citi(dgrVendorPayments.Item("BankName", i).Value) Then
                    MsgBox(dgrVendorPayments.Item("BankName", i).Value & " contains prohibited characters")
                    Return False
                End If
            End If
        Next
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

            For Each objRow As DataGridViewRow In dgrVendorPayments.Rows
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
                    lstQuerries.Add("update lib.dbo.VendorPayments set status='E0', BatchNo='" _
                        & mstrBankName & "' where recid=" & objRow.Cells("RecID").Value)
                End If
            Next
            UpdateListOfQuerries(lstQuerries, Conn_Web)
            objExcel.Visible = True
        End If
        Return True
    End Function
    Private Function Export2VPB() As Boolean
        Dim lstQuerries As New List(Of String)
        Dim strExportFileName As String = String.Empty

        For i As Int16 = 0 To Me.dgrVendorPayments.RowCount - 1
            If Me.dgrVendorPayments.Item("S", i).Value = True Then
                lstQuerries.Add("update lib.dbo.VendorPayments set status='E0', BatchNo='" _
                                & mstrBankName & "' where recid=" & Me.dgrVendorPayments.Item("RecID", i).Value)
            End If
        Next

        If Not (lstQuerries.Count > 0 _
                AndAlso UpdateListOfQuerries(lstQuerries, Conn_Web)) Then
            Return False
        End If
        If MySession.City = "SGN" Then
            Dim objExcel As Excel.Application = CreateObject("Excel.Application")
            Dim objWbk As Excel.Workbook
            Dim objWsh As Excel.Worksheet
            Dim tblPayerAccount As DataTable
            Dim intCountRecords As Integer
            Dim intRowVp As Integer = 22
            Dim intRowNonVp As Integer = 26
            Dim intRow As Integer
            Dim decAmountVp As Decimal
            Dim decAmountNonVp As Decimal
            Dim blnVPB As Boolean

            tblPayerAccount = GetDataTable("Select * from Vendor" _
                                            & " where Status='OK' and Recid=" _
                                            & CmbAcctToExp.SelectedValue, Conn)


            strExportFileName = "R12_2VPB_SGN.xlt"

            objWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath _
                                             & "\" & strExportFileName, True)
            objExcel.Visible = True
            objWsh = objWbk.Sheets("Lệnh TT")
            With objWsh
                .Range("f5").Value = "Ngày lập*:" & Format(Now, "dd/MM/yyyy")
                .Range("a10").Value = "Tên Khách hàng: " & tblPayerAccount.Rows(0)("AccountName")
                .Range("a11").Value = "Mã KH (CIF):" & tblPayerAccount.Rows(0)("CIF")
                .Range("a15").Value = "Số tài khoản trích tiền và phí:" & tblPayerAccount.Rows(0)("AccountNumber")
                .Range("a16").Value = "Mở tại  VPBank: " & tblPayerAccount.Rows(0)("BankAddress")
                .Range("a18").Value = "Ngày có giá trị thanh toán:" & Format(Now, "dd/MM/yyyy")
                .Range("a12").Value = "Địa chỉ *:" & tblPayerAccount.Rows(0)("Address")

                '.Range("E5").Value = "Ngày lập*:" & Format(Now, "dd/MM/yyyy")
                '.Range("a10").Value = "Tên Khách hàng: " & tblPayerAccount.Rows(0)("AccountName")
                '.Range("a11").Value = "Mã KH (CIF):" & tblPayerAccount.Rows(0)("CIF")
                '.Range("a14").Value = "Số tài khoản trích tiền: " & Mid(tblPayerAccount.Rows(0)("AccountNumber"), 5).Trim
                '.Range("a15").Value = "Mở tại  VPBank: " & tblPayerAccount.Rows(0)("Address")

                For Each objRow As DataGridViewRow In dgrVendorPayments.Rows
                    If objRow.Cells("S").Value Then

                        intCountRecords = intCountRecords + 1
                        If intCountRecords > 50 Then
                            MsgBox("Max 50 records are allowed!")
                            Return False
                        End If
                        Dim objBankName As New clsBank
                        objBankName.ParseName(objRow.Cells("BankName").Value)

                        If objBankName.BankName.Contains("VP BANK") _
                            Or objBankName.BankName.Contains("VIET NAM THINH VUONG") Then
                            blnVPB = True
                            intRow = intRowVp
                            decAmountVp = decAmountVp + objRow.Cells("Amount").Value
                            objWsh.Rows(intRow).insert
                            .Range("a" & intRow).Value = intRowVp - 21
                        Else
                            blnVPB = False
                            intRow = intRowNonVp
                            decAmountNonVp = decAmountNonVp + objRow.Cells("Amount").Value
                            objWsh.Rows(intRow).insert
                            .Range("a" & intRow).Value = intRowNonVp - 25
                        End If

                        .Range("B" & intRow).Value = objRow.Cells("AccountName").Value

                        .Range("C" & intRow).Value = "'" & objRow.Cells("AccountNumber").Value
                        .Range("D" & intRow).Value = objRow.Cells("Amount").Value
                        .Range("e" & intRow).Value = TienBangChu(objRow.Cells("Amount").Value)
                        .Range("F" & intRow).Value = objBankName.BankName

                        If Not blnVPB Then
                            .Range("G" & intRow).Value = objBankName.Branch
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
                    If decAmountVp <> 0 Then
                        .Range("D" & intRowVp).Value = Format(decAmountVp, "#,###")
                    End If
                    If decAmountNonVp <> 0 Then
                        .Range("D" & intRowNonVp).Value = Format(decAmountNonVp, "#,###")
                    End If
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
    End Function
    Private Function Export2VCB() As Boolean
        Dim lstQuerries As New List(Of String)
        Dim strExportFileName As String = String.Empty
        Dim i As Integer

        For i = 0 To Me.dgrVendorPayments.RowCount - 1
            If Me.dgrVendorPayments.Item("S", i).Value = True Then
                lstQuerries.Add("update lib.dbo.VendorPayments set status='E0', BatchNo='" _
                                & mstrBankName & "' where recid=" & Me.dgrVendorPayments.Item("RecID", i).Value)
            End If
        Next

        If Not (lstQuerries.Count > 0 _
                AndAlso UpdateListOfQuerries(lstQuerries, Conn_Web)) Then
            Return False
        End If

        If MySession.City = "SGN" Then
            strExportFileName = "R12_2VCB_SGN.xlt"
        End If

        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWbk As Excel.Workbook
        Dim objWsh As Excel.Worksheet
        Dim tblPayerAccount As DataTable

        i = 14
        Dim decTotal As Decimal

        tblPayerAccount = GetDataTable("Select * from Vendor" _
                                            & " where Status='OK' and Recid=" _
                                            & CmbAcctToExp.SelectedValue, Conn)

        objWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath _
                                             & "\" & strExportFileName, True)
        objWsh = objWbk.Sheets("BANG KE")
        objWsh.Activate()
        objExcel.Visible = True
        With objWsh
            For Each objRow As DataGridViewRow In dgrVendorPayments.Rows
                .Range("E5").Value = Now.Date
                If objRow.Cells("S").Value Then
                    .Range("A" & i).Value = i - 13
                    .Range("B" & i).Value = objRow.Cells("AccountName").Value
                    .Range("c" & i).Value = objRow.Cells("AccountNumber").Value
                    .Range("d" & i).Value = objRow.Cells("Amount").Value
                    .Range("e" & i).Value = objRow.Cells("BankName").Value
                    .Range("f" & i).Value = objRow.Cells("Description").Value
                    .Range("g" & i).Value = objRow.Cells("RefNo").Value
                    decTotal = decTotal + objRow.Cells("Amount").Value
                    i = i + 1
                End If
                .Range("C8").Value = decTotal
            Next
            .Range("A" & i).Value = "Tổng số tiền chuyển:" & Format(decTotal, "#,##0")
            .Range("A" & i + 1).Value = "Bằng chữ:" & TienBangChu(decTotal)
            .Range("A" & i + 2).Value = "Chúng tôi cam kết và chịu hoàn toàn trách nhiệm về tính chính xác, trung thực của bảng kê này."
            .Range("b" & i + 3).Value = "Kế toán trưởng (nếu có)"
            .Range("e" & i + 3).Value = "Chủ tài khoản"
        End With

        objWsh = objWbk.Sheets("UNC")
        objWsh.Activate()
        objWsh.Range("C12").Value = tblPayerAccount.Rows(0)("Address")
        objWsh.Range("C13").Value = tblPayerAccount.Rows(0)("BankName")
        objWsh.Range("G12").Value = TienBangChu(decTotal)
        objWsh.Range("G15").Value = "Thanh toan theo bang ke ngay " & Format(Now, "dd/MM/yy")


        UpdateBatchNo(GenerateBankPaymentBatchNbr(Conn, "VCB"))

    End Function
    Private Sub ChkPending_Click(sender As Object, e As EventArgs) Handles ChkPending.Click
        If Me.ChkPending.Checked Then
            LoadGridUNC("E0")
        Else
            LoadGridUNC("OK")
        End If

        Me.LblUpdateBatchNo.Enabled = Me.ChkPending.Checked

        Me.txtLotNo.Enabled = Me.ChkPending.Checked
        Me.LblExport.Enabled = Not Me.ChkPending.Checked
    End Sub

    Private Sub GridBatch_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Exit Sub
        Me.LblDelete.Visible = True
        Dim strDK As String = "status='E0'"
        If Me.GridBatch.CurrentRow.Cells("BatchNo").Value <> "" Then
            strDK = "status='OK' and BatchNo='" & Me.GridBatch.CurrentRow.Cells("BatchNo").Value & "'"
        End If
        Me.GridUNCinBatch.DataSource = GetDataTable("Select RecID, TRX_TC, Curr, Amount, InvNo, RMKNoibo, RefNo, ShortName as Beneficiary," &
                                                     "AccountName, AccountNumber, BankName, BankAddress, PayerACcountID, Charge, Description," &
                                                     "Swift, FstUpdate from VendorPayments where " & strDK)
        Me.GridUNCinBatch.Columns("Curr").Width = 32
        Me.GridUNCinBatch.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridUNCinBatch.Columns("Amount").DefaultCellStyle.Format = "#,##0.00"
    End Sub

    Private Sub LblDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblDelete.Click
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        If Me.GridBatch.CurrentRow.Cells("BatchNo").Value = "" Then
            cmd.CommandText = ChangeStatus_ByDK("VendorPayments", "OK", "status='E0'")
        Else
            cmd.CommandText = "update VendorPayments set BatchNo='' where BatchNo='" & Me.GridBatch.CurrentRow.Cells("BatchNo").Value & "'"
            If myStaff.City = "SGN" And InStr("HXT_NMH_SYS", myStaff.SICode) = 0 Then Exit Sub
            If myStaff.City = "HAN" And InStr("NTH_SYS", myStaff.SICode) = 0 Then Exit Sub
        End If
        cmd.ExecuteNonQuery()
        LoadGridBatch()
    End Sub

    Private Sub GridUNC_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Exit Sub
        Dim KQ As Decimal = 0
        Dim intCountRecord As Integer

        Me.txtAmount.Focus()
        If e.ColumnIndex = 0 Then
            For r As Int16 = 0 To Me.dgrVendorPayments.RowCount - 1
                If dgrVendorPayments.Item(0, r).Value Then
                    KQ = KQ + dgrVendorPayments.Item("Amount", r).Value
                    intCountRecord = intCountRecord + 1
                End If
            Next
        End If
        Me.txtAmount.Text = Format(KQ, "#,##0.00")
        txtCount.Text = intCountRecord
    End Sub

    Private Sub CmbAcctToExp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAcctToExp.SelectedIndexChanged
        If IsNumeric(CmbAcctToExp.SelectedValue) AndAlso ScalarToInt("VendorPayments", "RecId", "payerAccountID =" & CmbAcctToExp.SelectedValue _
                        & " and status='E0' and SingleBtf='False'") > 0 Then
            LblExport.Visible = False
        Else
            LblExport.Visible = True
        End If
        LoadGridUNC("OK")
    End Sub



    Private Sub LblExport_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblExport.LinkClicked

        Dim lstQuerries As New List(Of String)
        Dim strExportFileName As String = String.Empty

        Dim tmpRecID As Integer = ScalarToInt("VendorPayments", "Top 1 RECID", "status='E0' and BatchNo='' and " &
                                              "PayerAccountID=" & Me.CmbAcctToExp.SelectedValue)
        If tmpRecID > 0 Then
            MsgBox("Lot No Already Exists. Plz Check Your Input", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        Select Case mstrBankName
            Case "CTB"
                Export2CTB()
            Case "VCB"
                Export2VCB()
            Case "VPB"
                Export2VPB()
        End Select

    End Sub

    Private Sub LblUpdateBatchNo_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblUpdateBatchNo.LinkClicked
        UpdateBatchNo(txtLotNo.Text.ToUpper.Trim)
        If Me.ChkPending.Checked Then
            LoadGridUNC("E0")
        Else
            LoadGridUNC("OK")
        End If
    End Sub

    Private Sub cboBanks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBanks.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            mstrBankName = Mid(cboBanks.Text, 1, 3)
            Dim strQuerryAccount As String = "select recID as VAL, shortname as DIS " _
                    & " from Vendor where Bank ='" & mstrBankName & "'"
            LoadCmb_VAL(Me.CmbAcctToExp, strQuerryAccount)
            LoadGridUNC("OK")
            LoadGridBatch()
        End If
    End Sub

    Private Sub txtAmount_Enter(sender As Object, e As EventArgs) Handles txtAmount.Enter
        Dim decTotal As Decimal
        Dim intCount As Integer
        For Each objRow As DataGridViewRow In dgrVendorPayments.Rows
            If objRow.Cells("S").Value Then
                decTotal = decTotal + objRow.Cells("Amount").Value
                intCount = intCount + 1
            End If

        Next
        txtAmount.Text = Format(decTotal, "#,##0")
        txtCount.Text = intCount
    End Sub

    Private Sub dgrVendorPayments_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrVendorPayments.CellContentClick

        If e.ColumnIndex = 0 Then
            Me.txtAmount.Text = 0
            txtCount.Text = 0
        End If

    End Sub
End Class
