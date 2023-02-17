Imports RAS12.MySharedFunctions
Imports RAS12.MySharedFunctionsWzConn
Imports System.IO
Public Class UNC
    Private CharEntered As Boolean = False
    Private Recno As Integer, SelectedCurr As String
    Private iCounter As Int16 = 0
    Private PayerAccouID_frm As Integer, PayerAccouID_To As Integer
    Private mblnFirstLoadCompleted As Boolean
    Private mstrFilterByTvc As String
    Private mdecVendorBalance As Decimal
    Private mdecRequestedAmt As Decimal
    Private mdecChangedAmt As Decimal

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub UNC_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Private Sub UNC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If HasNewerVersion_R12(Application.ProductVersion) Or SysDateIsWrong(Conn) Then
            Me.Close()
            Me.Dispose()
            End
        End If
        GenCmbValue()
        LoadGridNewSupplier()
        Dim dtable As DataTable = GetDataTable("select distinct TCode as val from dutoan_Tour where custid>0 and status<>'XX' and SDate >dateadd(d,-128,getdate()) " & _
                                                "Union select VAL from misc where cat='UNC_WHO'")
        'For i As Int16 = 0 To dtable.Rows.Count - 1
        '    Me.LstTRX_TC.Items.Add(dtable.Rows(i)("VAL"))
        'Next
        If myStaff.City = "SGN" Then
            PayerAccouID_frm = 1140
            PayerAccouID_To = 1145
        Else
            PayerAccouID_frm = 4182
            PayerAccouID_To = 4187
        End If
        'LoadGridRQ()
        mblnFirstLoadCompleted = True
    End Sub
    Private Sub GenCmbValue()
        'Dim objSqlFt As New SqlClient.SqlConnection
        'objSqlFt.ConnectionString = CnStr_F1S
        'objSqlFt.Open()
        'LoadCmb_VAL(Me.CmbTVC, "select RecID as VAL, shortName as DIS from Lib.dbo.Vendor where CAT='TV'", objSqlFt)

        LoadCombo(Me.CmbTVC, "select distinct VAL as Value from Misc where CAT='TVPayer' and City='" & myStaff.City & "'", Conn)

        LoadCmb_MSC(Me.CmbCurr, "Curr")
        Me.CmbCurr.Text = "VND"
        'Me.CmbTVC.SelectedValue = 20
    End Sub
    Private Sub LoadGridRQ()
        Dim strSQL As String = "select RecID, App,RefNo, Shortname, Curr, Amount, FOP as RQ4, TRX_TC, AccountName, AccountNumber, " &
            "BankName, BankAddress, Description, FstUpdate, FstUser, RMK, SupplierID,InvNo,PayeeAccountID" &
            ",OnBehalf , PmtLotID,Status,FOP,Counter,PayerAccountId from unc_payments where "
        If Me.ChkShowLstAccepted.Checked Then
            strSQL = strSQL & " Status='OK' and LstUpdate >'" & Format(Now.AddDays(-8), "dd-MMM-yy") & "'"
        Else
            strSQL = strSQL & " status='QQ'"
            strSQL = strSQL & " and (fop in ('CRD','CSH') or AccountNumber<>'')"
        End If
        AddEqualConditionCombo(strSQL, cboFOP)
        AddEqualConditionCombo(strSQL, cboApp)

        If CInt(Me.txtPmtLotID.Text) >= 32000 Then
            strSQL = strSQL & " and PmtLotID=" & CInt(Me.txtPmtLotID.Text)
        End If
        If txtDescription.Text <> "" Then
            strSQL = strSQL & " and Description like '%" & txtDescription.Text & "%'"
        End If

        If cboFOP.Text <> "CSH" Then
            strSQL = strSQL & mstrFilterByTvc
        End If
        'strSQL = "select RecID, App, Shortname, Curr, Amount, FOP as RQ4, TRX_TC, AccountName, AccountNumber, " &
        '    "BankName, BankAddress, Description, FstUpdate, FstUser, RMK, SupplierID,InvNo,PayeeAccountID" &
        '    ",OnBehalf , PmtLotID,Status,FOP,Counter from unc_payments where RefNo='TR1120-0569'"

        strSQL = strSQL & " order by recid desc"
        Me.GridRQ.DataSource = GetDataTable(strSQL)
        Me.GridRQ.Columns(0).Visible = False
        Me.GridRQ.Columns(1).Visible = False
        Me.GridRQ.Columns("RefNo").Visible = True
        Me.GridRQ.Columns("APP").Width = 30
        Me.GridRQ.Columns("TRX_TC").Width = 128
        Me.GridRQ.Columns("ShortName").Width = 128
        Me.GridRQ.Columns("RQ4").Width = 36
        Me.GridRQ.Columns("Curr").Width = 36
        Me.GridRQ.Columns("FstUser").Width = 36
        Me.GridRQ.Columns("pmtLotID").Width = 56
        Me.GridRQ.Columns("SupplierID").Width = 56
        Me.GridRQ.Columns("App").Width = 56
        Me.GridRQ.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridRQ.Columns("Amount").DefaultCellStyle.Format = "#,##0.00"
        'Me.GridRQ.Columns("BalanceAdj").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'Me.GridRQ.Columns("BalanceAdj").DefaultCellStyle.Format = "#,##0.00"
        'Me.GridRQ.Columns("BalanceAdj").Width = 64
        Me.LblAcceptSingleBTF.Visible = False
        Me.LblCash.Visible = False
        Me.LblCRD.Visible = False
        Me.LblRejectRQ.Visible = False
        Me.LblChangeToVND.Visible = False
        Me.LckLblUndoAccept.Visible = False
        Me.txtNewDescr.Enabled = False
        Me.txtNewAmt.Enabled = False

        'If mstrFilterByTvc = "" Then
        '    Me.LblCashAdv2Guide.Visible = False
        'Else
        If Me.GridRQ.RowCount > 0 And CInt(Me.txtPmtLotID.Text) > 32000 Then
                Me.LblCashAdv2Guide.Visible = True
            End If
    End Sub
    Private Sub LoadGridNewSupplier()
        Dim strSQL = "select RecID, Curr, Amount, FOP, TRX_TC, Description, RefNo, FstUpdate, FstUser, SupplierID " &
            " from unc_payments where status='QQ' and SupplierID >0 and accountNumber=''  " &
            " order by recid desc"
        Me.GridNewSupplier.DataSource = GetDataTable(strSQL)
        Me.GridNewSupplier.Columns(0).Visible = False
        Me.GridNewSupplier.Columns("TRX_TC").Width = 128
        Me.GridNewSupplier.Columns("FOP").Width = 36
        Me.GridNewSupplier.Columns("Curr").Width = 36
        Me.GridNewSupplier.Columns("FstUser").Width = 36
        Me.GridNewSupplier.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridNewSupplier.Columns("Amount").DefaultCellStyle.Format = "#,##0.00"
        Me.LblAssignAccount.Visible = False
        Me.LblGo2Mapping.Visible = False
    End Sub

    Private Function LoadVendor(strCat As String) As Boolean

        If strCat <> "ZZ" Then
            GenBeneficiary(strCat)
            Me.CmbBeneficiary.Enabled = True
        Else
            Me.CmbBeneficiary.Enabled = False
            ClearTextBox()
        End If
        Me.GridBeneficiary.Visible = Me.CmbBeneficiary.Enabled
        Me.TxtAccountName.Enabled = Not Me.CmbBeneficiary.Enabled
        Me.TxtAccNo.Enabled = Not Me.CmbBeneficiary.Enabled
        Me.TxtBank.Enabled = Not Me.CmbBeneficiary.Enabled
        Me.TxtCity.Enabled = Not Me.CmbBeneficiary.Enabled
        Me.TxtSwift.Enabled = Not Me.CmbBeneficiary.Enabled
        Return True
    End Function
    Private Sub GenBeneficiary(ByVal pCat As String)

        LoadCmb_VAL(Me.CmbBeneficiary, "select RecID as VAL, shortName as DIS from Vendor where status ='OK' and CAT='" & pCat & "'")

        Me.GridCanText.DataSource = GetDataTable("select VAL from MISC where cat='CANTXT' and VAL1='" & pCat & "'")
        Me.GridCanText.Columns(0).Width = 320
        Me.TxtDescript.Text = ""
    End Sub

    Private Sub CmbTVC_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbTVC.SelectedIndexChanged
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand
        On Error GoTo ErrHandler
        Dim strFilterGDS As String = " (SupplierId=11700 or TVC='GDS')"
        'Dim strFilterGDS As String = " ((RMKNoiBo like '%ABBOTT%' and (RMKNoiBo like '%MICE%' or RMKNoiBo like '%MCE%')) " _
        '                        & " or (SupplierId=11700) or TVC='GDS')"

        Dim strSQL As String = "select Bank, AccountNumber,Cur,RecID from Vendor" _
            & " where status='OK' and RecID in (select intVal from Misc where Cat='TvPayer' and Val='" _
            & Me.CmbTVC.Text & "') order by Bank, AccountNumber"
        Me.GridPayer.DataSource = GetDataTable(strSQL)
        'Me.GridPayer.Columns(0).Visible = False
        Me.GridPayer.Columns("Bank").Width = 90
        Me.GridPayer.Columns("AccountNumber").Width = 128
        Me.GridPayer.Columns("Cur").Width = 40
        Me.GridPayer.Columns("RecId").Width = 50
        Me.GrpBeneficiaryDetail.Enabled = False
        ClearTextBox()

        Select Case Me.CmbTVC.Text
            Case "TVTR", "TVTRDAD", "GDS"
                Me.CmbInvNo.Text = ""
                Me.CmbPmtType.Visible = True
                Me.CmbPmtType.Text = Me.CmbPmtType.Items(0).ToString
                Me.GridRQ.Visible = True

                'SGN chi duoc dung tai khoan VCB VND cua TVTR tra cho Guide
                'If myStaff.City = "SGN" Then
                '    OptGD.Visible = True
                'Else
                '    OptGD.Visible = False
                'End If


            Case Else
                Me.GridRQ.Visible = False
                Me.CmbPmtType.Visible = False
                Me.CmbPmtType.Text = ""
                Me.CmbInvNo.Text = "N/A"
                'OptGD.Visible = False
        End Select

        If mblnFirstLoadCompleted Then
            If CmbTVC.Text = "GDS" Then
                mstrFilterByTvc = " and" & strFilterGDS
                LoadGridRQ()
            Else
                mstrFilterByTvc = " and not " & strFilterGDS
                LoadGridRQ()
            End If

        End If

        'Me.LstTRX_TC.Visible = Me.CmbPmtType.Visible
        Me.Label10.Visible = Me.CmbPmtType.Visible
ErrHandler:
        On Error GoTo 0
    End Sub

    Private Sub TxtAmt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtAmt.KeyDown
        CharEntered = checkCharEntered(e.KeyValue)
    End Sub

    Private Sub TxtAmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtAmt.KeyPress
        If CharEntered Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridCanText_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridCanText.CellContentClick
        Me.TxtDescript.Text = Me.GridCanText.CurrentCell.Value & " "
        Me.TxtDescript.Focus()
        Me.TxtDescript.SelectionStart = Me.TxtDescript.TextLength
    End Sub

    Private Sub CmbBeneficiary_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbBeneficiary.SelectedIndexChanged
        On Error GoTo ErrHandler
        Dim strSQL As String = "select RecID, AccountName,AccountNumber, BankName, BankAddress,swift" _
            & ", CAT" _
            & "  from Vendor " &
            " where status ='OK' and RecID=" & Me.CmbBeneficiary.SelectedValue _
            & " order by BankName, AccountNumber"
        Me.GridBeneficiary.DataSource = GetDataTable(strSQL)
        Me.GridBeneficiary.Columns(0).Visible = False
        Me.GridBeneficiary.Columns(1).Width = 200
        Me.GridBeneficiary.Columns(2).Width = 128
        Me.GridBeneficiary.Columns(3).Visible = False
        Me.GridBeneficiary.Columns(4).Visible = False
        Me.GridBeneficiary.Columns(5).Width = 192
        Me.Label12.Text = ScalarToString("Vendor", "FOP", "RecID=" & Me.CmbBeneficiary.SelectedValue)
        ClearTextBox()
        Exit Sub
ErrHandler:
        On Error GoTo 0
    End Sub

    Private Sub GridBeneficiary_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles _
        GridBeneficiary.CellClick
        Me.TxtBank.Text = Me.GridBeneficiary.CurrentRow.Cells("BankName").Value
        Me.TxtCity.Text = Me.GridBeneficiary.CurrentRow.Cells("BankAddress").Value
        Me.TxtAccNo.Text = Me.GridBeneficiary.CurrentRow.Cells("AccountNumber").Value
        Me.TxtAccountName.Text = Me.GridBeneficiary.CurrentRow.Cells("AccountName").Value
        Me.TxtSwift.Text = Me.GridBeneficiary.CurrentRow.Cells("Swift").Value
        Dim LstPmt As Date = ScalarToDate("UNC_payments", "top 1 FstUpdate", "AccountNumber='" & Me.TxtAccNo.Text &
                                            "' order by fstupdate desc")
        If LstPmt < Now.AddMonths(-3) Then
            MsgBox("This Account Seems to Be Asleep. Plz Check its Validity", MsgBoxStyle.Exclamation, msgTitle)
        End If
    End Sub
    Private Sub GenTemplate()
        Dim strSQL As String = "select VAL from misc where cat='UNC' and left(val,3)='" _
            & Strings.Left(Me.GridPayer.CurrentRow.Cells("Bank").Value, 3) & "' order by intVal"
        LoadCmb_MSC(Me.CmbTemplate, strSQL)
    End Sub

    Private Sub GridPayer_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles _
        GridPayer.CellContentClick, GridPayer.CellClick
        If e.RowIndex < 0 Then Exit Sub
        iCounter = 0
        GenTemplate()
        If Me.GridPayer.CurrentRow.Cells("Bank").Value = "TCB" Then
            If InStr(Me.GridPayer.CurrentRow.Cells("AccountNumber").Value, "VND") > 0 Then
                Me.CmbTemplate.Text = Me.CmbTemplate.Items(1).ToString
            End If
        End If
        Me.GrpBeneficiaryDetail.Enabled = True
        Me.GridRQ.Enabled = True
        For i As Int16 = 0 To Me.GridRQ.RowCount - 1
            If Me.GridRQ.Item("RQ4", i).Value <> "BTF" Then
                Me.GridRQ.Rows(i).DefaultCellStyle.ForeColor = Color.Red
            End If
        Next
    End Sub

    Private Sub TxtAmt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAmt.LostFocus
        Dim aa As Double = CDbl(Me.TxtAmt.Text)
        Me.TxtAmt.Text = Format(aa, "#,##0.00")
        iCounter = 0
    End Sub
    Private Function DefineWhoBearCharge() As String
        Dim KQ As String = ""
        If Me.OptInVNBeneficiary.Checked Then KQ = "IB"
        If Me.OptInVNPayer.Checked Then KQ = KQ & "IP"
        If Me.OptOutVNBeneficiary.Checked Then KQ = KQ & "OB"
        If Me.OptOutVNPayer.Checked Then KQ = KQ & "OP"
        Return KQ
    End Function
    Private Function SaveUnc(blnSingle As Boolean) As Boolean
        Dim blnCheckBankAccount4Guide As Boolean = True
        If Me.TxtAccountName.Text = "" Or Me.TxtDescript.Text = "" Or
           Me.TxtBank.Text = "" Or CDec(Me.TxtAmt.Text) = 0 Or
           Me.TxtAccountName.Text = "ACCOUNT NAME" Or
           Me.TxtBank.Text = "BANK" Or Me.TxtCity.Text = "LOCATION" Then
            MsgBox("Invalid Input!", MsgBoxStyle.Critical, msgTitle)
            Return False
        ElseIf Me.TxtAccNo.Text = "" Or Me.TxtAccNo.Text = "ACCOUNT NO" Then
            MsgBox("Missing Account Numbner for Vendor!", MsgBoxStyle.Critical, msgTitle)
        ElseIf Me.TxtCity.Text = "" Then
            MsgBox("Missing Bank Addrees for Vendor!", MsgBoxStyle.Critical, msgTitle)
        End If

        'Neu SGN tra cho Guide thi phai dung TVTR-VCB-VND
        If cboCAT.Text.StartsWith("GD") AndAlso myStaff.City = "SGN" Then
            If GridPayer.CurrentRow.Cells("Cur").Value <> "VND" Then
                blnCheckBankAccount4Guide = False
            Else
                Select Case CmbTVC.Text
                    Case "TVTR"
                        If GridPayer.CurrentRow.Cells("Bank").Value <> "VCB" Then
                            blnCheckBankAccount4Guide = False
                        End If
                    Case "TVTR DAD"
                        If GridPayer.CurrentRow.Cells("Bank").Value <> "TCB" Then
                            blnCheckBankAccount4Guide = False
                        End If
                    Case Else
                        blnCheckBankAccount4Guide = False
                End Select
            End If

            If Not blnCheckBankAccount4Guide Then
                MsgBox("Guide must be paid by TVTR-VCB-VND/TVTR DAD-TCB-VND!", MsgBoxStyle.Critical, msgTitle)
                Return False
            End If
        End If

        Me.LblSaveSingle.Visible = False
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim strSQL As String, tmpNoiDung As String
        Dim t As SqlClient.SqlTransaction
        Dim tmpCharge As String = "", RefNo As String, TRX_TC As String = ""
        tmpNoiDung = Me.TxtDescript.Text.Replace("'", "").Replace("--", "")
        tmpCharge = DefineWhoBearCharge()

        Select Case CmbTVC.Text
            Case "GDS"
                RefNo = DefineRefNo_UNC4GDS()
            Case Else
                RefNo = DefineRefNo_UNC(Me.CmbTVC.Text)
        End Select

        'For i As Int16 = 0 To Me.LstTRX_TC.Items.Count - 1
        '    If Me.LstTRX_TC.GetItemChecked(i) Then
        '        TRX_TC = TRX_TC & "_" & Me.LstTRX_TC.Items(i).ToString
        '    End If
        'Next
        If TRX_TC.Length > 2 Then
            TRX_TC = Me.CmbPmtType.Text & ":" & TRX_TC.Substring(1)
        End If
        t = Conn.BeginTransaction
        Cmd.Transaction = t
        strSQL = "Insert into UNC_Payments (PayerAccountID, AccountName, AccountNumber" _
            & ", BankName, BankAddress, Curr, Amount,RequestedAmt, Description, swift, Charge, shortname" _
            & ", InvNo, TRX_TC, PayeeAccountID, RMKNoiBo, FstUser,SingleBtf) values ('" &
            Me.GridPayer.CurrentRow.Cells("Recid").Value & "',N'" &
            Me.TxtAccountName.Text & "','" &
            Me.TxtAccNo.Text & "',N'" &
            Me.TxtBank.Text & "',N'" &
            Me.TxtCity.Text & "','" &
            Me.CmbCurr.Text & "','" &
            CDec(Me.TxtAmt.Text) & "','" & CDec(Me.TxtAmt.Text) & "',N'" &
            tmpNoiDung & "','" &
            Me.TxtSwift.Text & "','" &
            tmpCharge & "',N'" &
            Me.CmbBeneficiary.Text & "','" &
            Me.CmbInvNo.Text & "','" &
            TRX_TC & "',"
        If cboCAT.Text = "" Then
            strSQL = strSQL & "0,'"
        Else
            strSQL = strSQL & Me.GridBeneficiary.CurrentRow.Cells("RecID").Value & ",'"
        End If
        strSQL = strSQL & tmpNoiDung & "','" & myStaff.SICode & "','" & blnSingle & "')"
        Cmd.CommandText = strSQL & "; SELECT SCOPE_IDENTITY() AS [RecID]"
        Try
            Recno = Cmd.ExecuteScalar
            Cmd.CommandText = "update UNC_Payments set refno='" & Replace(RefNo, "--", "") & "' where recid =" & Recno
            Cmd.ExecuteNonQuery()
            t.Commit()

            Me.LblPreview.Visible = True
            Return True
        Catch ex As Exception
            t.Rollback()
            Append2TextFile(ex.Message & vbNewLine & Cmd.CommandText)
            MsgBox("Unable Writting into DataBase. Please Check Your Input or If You Ensure the Correct Input, Contact Supervisor", MsgBoxStyle.Critical, msgTitle)
            Return False
        End Try

    End Function
    Private Sub LblSave_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblSaveSingle.LinkClicked
        If SaveUnc(True) Then

        End If

    End Sub

    Private Sub LblNew_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblNew.LinkClicked
        Me.LblSaveSingle.Visible = True
        Me.LblPreview.Visible = False
        ClearTextBox()
    End Sub

    Private Sub LblPreview_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblPreview.LinkClicked
        Dim intPayerAccountId As Integer = ScalarToInt("UNC_payments", "PayerAccountID", "RecID=" & Recno)

        If Me.CmbTemplate.Text.Contains("SCB") And
             intPayerAccountId > PayerAccouID_frm And
            intPayerAccountId < PayerAccouID_To Then
            InHoaDon(Application.StartupPath, Me.CmbTemplate.Text, "V", Recno, Now.Date, Now.Date, 0, "NEW", MySession.Domain)

            If MsgBox("Wanna Print it Out?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle) = vbNo Then Exit Sub

            InHoaDon(Application.StartupPath, Me.CmbTemplate.Text, "O", Recno, Now.Date, Now.Date, 0, "NEW", MySession.Domain)

            Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
            cmd.CommandText = "update UNC_payments set HasPrinted=1 where recid=" & Recno
            cmd.ExecuteNonQuery()

        Else
            InHoaDon(Application.StartupPath, Me.CmbTemplate.Text, "O", Recno, Now.Date, Now.Date, 0, "NEW", MySession.Domain)
        End If

        Dim strBankName As String = ScalarToString("Vendor", "BankName", "RecID=" & intPayerAccountId)
        If strBankName.Contains("CITIBANK") Or strBankName.Contains("Standard Chartered Bank") Then
            CreateSms4UNC(Recno, strBankName, False)
        End If

    End Sub

    Private Sub ClearTextBox()
        Me.TxtAccountName.Text = "ACCOUNT NAME"
        Me.TxtAccNo.Text = "ACCOUNT NO"
        Me.TxtDescript.Text = ""
        Me.TxtBank.Text = "BANK"
        Me.TxtSwift.Text = "SWIFT"
        Me.TxtCity.Text = "LOCATION"
        Me.TxtAmt.Text = 0
        'For i As Int16 = 0 To Me.LstTRX_TC.Items.Count - 1
        '    Me.LstTRX_TC.SetItemChecked(i, False)
        'Next
    End Sub
    Private Function AcceptBTF(blnSingleBft As Boolean) As Boolean
        Dim blnClearInvNo As Boolean = False
        Dim blnCheckBankAccount4Guide As Boolean = True

        Dim strPayeeCat As String = ScalarToString("Vendor", "CAT" _
                                , "RecID =" & GridRQ.CurrentRow.Cells("PayeeAccountId").Value).Trim

        'SGN chi tra cho Guide bang TVTR VCB VND
        If myStaff.City = "SGN" AndAlso strPayeeCat = "GD" Then
            If GridPayer.CurrentRow.Cells("Cur").Value <> "VND" Then
                blnCheckBankAccount4Guide = False
            Else
                Select Case CmbTVC.Text
                    Case "GDS", "TVTR"
                        If GridPayer.CurrentRow.Cells("Bank").Value <> "TCB" Then
                            blnCheckBankAccount4Guide = False
                        End If
                    Case "TVTR DAD"
                        If GridPayer.CurrentRow.Cells("Bank").Value <> "TCB" Then
                            blnCheckBankAccount4Guide = False
                        End If
                End Select
            End If
            If Not blnCheckBankAccount4Guide Then
                MsgBox("Guide must be paid by TVTR-VCB-VND/TVTR DAD-VCB-VND!", MsgBoxStyle.Critical, msgTitle)
                Return False
            End If
        End If

        With GridRQ.CurrentRow
            If .Cells("InvNo").Value = "CTYNN" Then
                blnClearInvNo = True
                If GridPayer.CurrentRow.Cells("Bank").Value <> "VCB" _
                Or Not GridPayer.CurrentRow.Cells("Cur").Value.ToString.StartsWith("VND") Then
                    MsgBox("CTYNN Must use VCB-VND")
                    Return False
                End If
            End If
        End With
        Me.LblPreview.Visible = True
        If GridPayer.CurrentRow.Cells("Bank").Value = "***" Then
            MsgBox("Cần chọn tài khoản thanh toán chính xác")
            Return False
        End If
        Return AcceptRQ("BTF", Me.GridRQ.CurrentRow.Cells("recID").Value, True, blnClearInvNo, blnSingleBft _
                        , GridRQ.CurrentRow.Cells("APP").Value, GridRQ.CurrentRow.Cells("Counter").Value _
                        , GridRQ.CurrentRow.Cells("TRX_TC").Value)

    End Function
    Private Sub LblAcceptSingleBTF_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblAcceptSingleBTF.LinkClicked
        AcceptBTF(True)
    End Sub
    Private Function AcceptRQ(pFOP As String, RQID As Integer, NeedReload As Boolean _
                         , Optional blnClearInvNo As Boolean = False _
                         , Optional blnSingleBtf As Boolean = False, Optional strApp As String = "" _
                         , Optional strCounter As String = "" _
                         , Optional strTrx_Tc As String = "") As Boolean
        Dim RefNo As String = "", PayerID As Integer = 0
        Recno = 0
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand

        If pFOP = "BTF" Then
            If CmbTVC.Text = "GDS" Then
                RefNo = DefineRefNo_UNC4GDS()
            Else
                RefNo = DefineRefNo_UNC("TVTR")
            End If
            PayerID = Me.GridPayer.CurrentRow.Cells("RecID").Value
        ElseIf pFOP = "CSH" Then
            RefNo = "TV" & Format(Now, "MM") & RQID
            PayerID = Me.GridPayer.CurrentRow.Cells("RecID").Value
        End If
        cmd.CommandText = "update UNC_Payments set status='OK', Refno='" & RefNo & "', PayerAccountID= " & PayerID &
            ", FOP='" & pFOP & "', LstUpdate=getdate(), LstUser='" & myStaff.SICode & "', Description=@Description" &
            ", Amount=" & CDec(Me.txtNewAmt.Text) & ", RequestedAmt=" & mdecRequestedAmt

        If blnClearInvNo Then
            cmd.CommandText = cmd.CommandText & ",InvNo=''"
        End If
        If blnSingleBtf Then
            cmd.CommandText = cmd.CommandText & ",SingleBtf='True'"
        End If
        cmd.CommandText = cmd.CommandText & " where recid=" & RQID
        cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Me.txtNewDescr.Text
        cmd.ExecuteNonQuery()
        Recno = RQID
        If NeedReload Then LoadGridRQ()
        Me.GroupBox1.Enabled = False
        Me.txtNewDescr.Enabled = False
        Me.txtNewAmt.Enabled = False

        If Recno > 0 AndAlso myStaff.City = "SGN" AndAlso ("BTF_CSH").Contains(pFOP) _
            AndAlso Not strTrx_Tc.Contains("PPD: 4-TVS") Then
            If ScalarToInt("MISC", "RecId", "Status='OK' and CAT='VendorNameInGroup' and val='VENDOR NOT IMPORT AOP' and intVal=" _
                           & "(select PayeeAccountId from UNC_Payments where RecId=" & RQID & ")") = 0 _
                           AndAlso ScalarToInt("UNC_Payments", "RecId", "RMKNoiBo='1' and RecId=" & RQID) = 0 Then
                If Not ImportAopUNC(Recno, strApp, strCounter) Then
                    MsgBox("Unable to import UNC to AOP:" & RefNo)
                ElseIf mdecChangedAmt > 0 Then
                    If ImportAopUNC4AdjustedAmt(Recno, strApp, strCounter, mdecChangedAmt) Then
                        ResetAdjustedAmt()
                    Else
                        MsgBox("Unable to import UNC to AOP for Adjusted Amount:" & mdecChangedAmt & " in UNC " & RefNo)
                    End If
                End If
            End If
        End If


        MsgBox(RefNo)
        Return (Recno > 0)
    End Function
    Private Function ResetAdjustedAmt() As Boolean
        'xoa thong tin so tien dieu chinh tra thap hon de nghi
        mdecRequestedAmt = 0
        mdecChangedAmt = 0
        Return True
    End Function
    Private Function AcceptDNTT(strFOP As String, strVendor As String, intVendorId As Integer _
                           , strCur As String, decRqAmount As Decimal, strApp As String _
                           , strDesc As String, strTvc As String, intPayerId As Integer _
                           , strCounter As String, Optional blnSingleBtf As Boolean = False) As Boolean
        Dim lstUpdateDNTT As New List(Of String)
        'Dim intPayerID As Integer = 0

        Dim decAcceptAmt As Decimal
        Dim strCreateUNC As String
        Dim tblAccount As System.Data.DataTable
        Dim strCutOffDate As String = GetLastCutOffDate(intVendorId, strCur)
        Dim intPaymentId As Integer
        Dim blnDeductBalance As Boolean

        tblAccount = GetDataTable("select top 1 * from Vendor where Status='OK' and RecId=" _
                                  & intVendorId & " order by RecId desc", Conn)
        If tblAccount.Rows.Count = 0 Then
            MsgBox("Unable to find Account for Vendor " & strVendor)
            Return False
        End If
        mdecVendorBalance = GetVendorBalance(intVendorId, strCur, strCutOffDate, strTvc)
        If mdecVendorBalance >= 0 Then
            decAcceptAmt = decRqAmount
        Else
            If MsgBox("Vendor Balance is " & mdecVendorBalance & ".Deduct payment amount?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                blnDeductBalance = True
                If Math.Abs(mdecVendorBalance) <= decRqAmount Then
                    decAcceptAmt = decRqAmount + mdecVendorBalance
                Else
                    decAcceptAmt = 0
                End If
            Else
                decAcceptAmt = decRqAmount
            End If
        End If

        strCreateUNC = "insert into Lib.dbo.VendorPayments (PayerAccountID, AccountName, AccountNumber, BankName" _
                & ", BankAddress,Swift, Curr, Amount, Description, FstUser" _
                & ", Status,VendorId, ShortName" _
                & ", PayeeAccountID, FOP, App, RMKNoiBo, Counter, SingleBTF, TVC,City,DeductBalance)" _
                & " values (" & GridPayer.CurrentRow.Cells("RecID").Value & ",'" & tblAccount.Rows(0)("AccountName") _
                & "','" & tblAccount.Rows(0)("AccountNumber") _
                & "','" & tblAccount.Rows(0)("BankName") & "','" & tblAccount.Rows(0)("BankAddress") _
                & "','" & tblAccount.Rows(0)("Swift") _
                & "','" & strCur & "'," & decAcceptAmt & ",'" & strDesc _
                & "','" & myStaff.SICode & "','--'," & intVendorId & ",'" & strVendor _
                & "'," & tblAccount.Rows(0)("Recid") & ",'" & strFOP & "','" & strApp _
                & "','" & strDesc & "','','" & blnSingleBtf & "','" & strTvc & "','" & myStaff.City & "','" & blnDeductBalance & "')"

        lstUpdateDNTT.Add(strCreateUNC)
        If Not UpdateListOfQuerries(lstUpdateDNTT, Conn_Web, True, intPaymentId) Then
            MsgBox("Unable to create UNC!")
            Return False
        Else
            lstUpdateDNTT.Clear()
        End If

        For Each objRow As DataGridViewRow In dgrDNTT.Rows
            If objRow.Cells("S").Value Then
                lstUpdateDNTT.Add("Update Lib.dbo.DNTT set PaymentId=" & intPaymentId _
                    & " where RecId=" & objRow.Cells("RecId").Value)
                lstUpdateDNTT.Add("insert into Lib.dbo.VendorBalance (VendorID, Vendor, Cur,Amount, FOP, PaymentId" _
                          & ", Status, DutoanId, Tcode, FstUser, Description, App,City) values (" _
                          & intVendorId & ",'" & strVendor & "','" & strCur & "'," & objRow.Cells("Amount").Value _
                          & ",'" & strFOP & "'," & intPaymentId & ",'OK'," _
                          & objRow.Cells("RefId").Value & ",'" & objRow.Cells("Ref").Value & "','" _
                          & myStaff.SICode & "','" & strDesc & "','" & strApp _
                          & "','" & myStaff.City & "')")

            End If
        Next

        lstUpdateDNTT.Add("Update Lib.dbo.VendorPayments set Status='OK' where RecId=" & intPaymentId)
        lstUpdateDNTT.Add("insert into Lib.dbo.VendorBalance (VendorID, Vendor, Cur,Amount, FOP, PaymentId" _
                          & ", Status, DutoanId, Tcode, FstUser, Description, App,City,TVC) values (" _
                          & intVendorId & ",'" & strVendor & "','" & strCur & "'," & (0 - decAcceptAmt) _
                          & ",'" & strFOP & "'," & intPaymentId & ",'OK',0,'','" _
                          & myStaff.SICode & "','" & strDesc & "','" & strApp _
                          & "','" & myStaff.City & "','" & strTvc & "')")

        If UpdateListOfQuerries(lstUpdateDNTT, Conn_Web) Then
            LoadGridDNTT()
        Else
            MsgBox("Unable to create UNC And update DNTT!")
        End If


    End Function
    Private Sub GridRQ_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridRQ.CellClick
        If e.RowIndex < 0 Then Exit Sub
        iCounter = 0
        Me.LblAcceptSingleBTF.Visible = Not Me.ChkShowLstAccepted.Checked
        Me.LblRejectRQ.Visible = Not Me.ChkShowLstAccepted.Checked
        Me.LblCash.Visible = Not Me.ChkShowLstAccepted.Checked
        'Me.lbkCashVCB.Visible = Not Me.ChkShowLstAccepted.Checked
        Me.LblCRD.Visible = Not Me.ChkShowLstAccepted.Checked
        Me.LckLblUndoAccept.Visible = Me.ChkShowLstAccepted.Checked
        Me.txtNewDescr.Text = Me.GridRQ.CurrentRow.Cells("Description").Value
        Me.txtNewAmt.Text = Format(Me.GridRQ.CurrentRow.Cells("Amount").Value, "#,##0.00")
        mdecRequestedAmt = GridRQ.CurrentRow.Cells("Amount").Value
        If Me.GridRQ.CurrentRow.Cells("Curr").Value <> "VND" Then
            Me.LblChangeToVND.Visible = True
            Me.TxtROE.Text = ForEX_12(myStaff.City, Now, Me.GridRQ.CurrentRow.Cells("Curr").Value, "BSR", "YY").Amount
        End If
        Me.GroupBox1.Enabled = True
        Me.txtPmtLotID.Text = -1
        Me.LblCashAdv2Guide.Visible = False
        Me.txtNewDescr.Enabled = False
        If Me.GridRQ.CurrentRow.Cells("supplierID").Value <> 0 Then
            LblCreditType.Text = "CreditType: " & ScalarToString("Vendor", "FOP", " RecID in (select VendorID from supplier where RecID=" &
                                        Me.GridRQ.CurrentRow.Cells("supplierID").Value & ")")

        End If
        'ResetAdjustedAmt()
    End Sub
    Private Function DefineRefNo_UNC(pCongTy As String) As String
        Dim tmpCty As String = Strings.Right(pCongTy, 2) & Format(Now, "MMyy") & "-"
        Dim KQ As String = ScalarToString("UNC_payments", "top 1 RefNo", "FOP<>'CSH' and left(refno,7)='" & tmpCty _
                                          & "' order by cast (substring(refno,8,4) AS int) desc")
        If KQ <> "" Then
            KQ = tmpCty & Format(CInt(Mid(KQ, 8)) + 1, "0000")
        Else
            KQ = tmpCty & "0001"
        End If
        Return KQ
    End Function
    Private Function DefineRefNo_UNC4GDS() As String
        Dim tmpCty As String = "GDS" & Format(Now, "MMyy") & "-"
        Dim KQ As String = ScalarToString("UNC_payments", "top 1 RefNo", "FOP<>'CSH' and left(refno,8)='" & tmpCty _
                                          & "' order by cast (substring(refno,9,4) AS int) desc")
        If KQ <> "" Then
            KQ = tmpCty & Format(CInt(Mid(KQ, 9)) + 1, "0000")
        Else
            KQ = tmpCty & "0001"
        End If
        Return KQ
    End Function

    Private Sub RejectRQ()
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        'Dim ServerName As String = IIf(myStaff.City = "SGN", "[42.117.5.70].", "")
        Dim DaTra As Decimal, pmtLotID As Integer = Me.GridRQ.CurrentRow.Cells("pmtLotID").Value
        If pmtLotID = 0 Then
            cmd.CommandText = ChangeStatus_ByID("UNC_payments", "XX", Me.GridRQ.CurrentRow.Cells("recID").Value)
        Else
            cmd.CommandText = ChangeStatus_ByDK("UNC_payments", "XX", " PmtLotID=" & Me.GridRQ.CurrentRow.Cells("pmtLotID").Value)
        End If
        If Me.GridRQ.CurrentRow.Cells("App").Value = "RAS" Then
            cmd.CommandText = cmd.CommandText & ";" & ChangeStatus_ByDK("dutoan_pmt", "XX", "PmtID=" & Me.GridRQ.CurrentRow.Cells("recID").Value)
            cmd.ExecuteNonQuery()

            Dim tbPmt As DataTable = GetDataTable("select ItemID from dutoan_Pmt where pmtID=" & Me.GridRQ.CurrentRow.Cells("recID").Value)
            cmd.CommandText = ""
            For i As Int16 = 0 To tbPmt.Rows.Count - 1
                DaTra = ScalarToDec("dutoan_pmt", "isnull(sum(VND),0)", "ItemID=" & tbPmt.Rows(i)("ItemID") & " and status<>'XX'")
                cmd.CommandText = cmd.CommandText & "; Update Dutoan_item set VNDPaid=" & DaTra &
                    " where RecID=" & tbPmt.Rows(i)("ItemID")
            Next
            If cmd.CommandText.Length > 2 Then
                cmd.CommandText = cmd.CommandText.Substring(1)
                cmd.ExecuteNonQuery()
            End If
        Else
            If pmtLotID = 0 Then
                cmd.CommandText = cmd.CommandText & "; update flx.dbo.tos_Pmt set status='XX' where DNTTID =" &
                    Me.GridRQ.CurrentRow.Cells("recID").Value & " and city='" & myStaff.City & "'"
            Else
                For r As Int16 = 0 To Me.GridRQ.RowCount - 1
                    If Me.GridRQ.Item("PmtLotID", r).Value = pmtLotID Then
                        cmd.CommandText = cmd.CommandText & "; update flx.dbo.tos_Pmt set status='XX' where DNTTID =" &
                            Me.GridRQ.Item("recID", r).Value & " and city='" & myStaff.City & "'"
                    End If
                Next
            End If
            cmd.ExecuteNonQuery()
        End If
        LoadGridRQ()
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblRejectRQ.LinkClicked
        RejectRQ()
    End Sub

    Private Sub LblCash_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblCash.LinkClicked
        If GridPayer.CurrentRow.Cells("Bank").Value <> "***" Then
            MsgBox("Cần phải chọn tài khoản tiền mặt tương ứng!")
            Exit Sub
        End If
        AcceptRQ("CSH", Me.GridRQ.CurrentRow.Cells("recID").Value, True,, , GridRQ.CurrentRow.Cells("APP").Value _
                 , GridRQ.CurrentRow.Cells("Counter").Value, GridRQ.CurrentRow.Cells("TRX_TC").Value)
    End Sub

    Private Sub LblCRD_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblCRD.LinkClicked
        AcceptRQ("CRD", Me.GridRQ.CurrentRow.Cells("recID").Value, True)
    End Sub

    Private Sub LblGo2Mapping_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblGo2Mapping.LinkClicked
        Dim SupplierID As Integer = Me.GridNewSupplier.CurrentRow.Cells("SupplierID").Value
        Dim f As New Vendor_SupplierMapping(SupplierID)
        f.ShowDialog()
    End Sub

    Private Sub AssignAccountToPmtRQ(pPmtRQID As Integer, pSupplierID As Integer, pAccountID As Integer)
        Dim VendorID As Integer, dtblPayee As DataTable
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        VendorID = ScalarToInt("supplier", "VendorID", "RecID=" & pSupplierID)
        Dim VendorName As String = ScalarToString("Vendor", "shortName", "RecID=" & VendorID)
        cmd.CommandText = "update UNC_Payments set ShortName=@ShortName where recID=" & pPmtRQID
        cmd.Parameters.AddWithValue("@ShortName", VendorName)
        cmd.ExecuteNonQuery()
        If pAccountID = 0 Then
            dtblPayee = GetDataTable("select top 1 * from Vendor where RecID=" & VendorID & " and status='OK'")
        Else
            dtblPayee = GetDataTable("select * from Vendor where RecID=" & pAccountID)
        End If
        If dtblPayee.Rows.Count = 1 Then
            cmd.CommandText = "update UNC_Payments set AccountName =@AccountName, AccountNumber =@AccountNumber, " &
                "BankName =@BankName , BankAddress =@BankAddress , Swift=@Swift, PayeeAccountID=@PayeeAccID " &
                "where recID=" & pPmtRQID
            cmd.Parameters.Clear()
            cmd.Parameters.Add("@AccountName", SqlDbType.VarChar).Value = dtblPayee.Rows(0)("AccountName")
            cmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar).Value = dtblPayee.Rows(0)("AccountNumber")
            cmd.Parameters.Add("@BankName", SqlDbType.VarChar).Value = dtblPayee.Rows(0)("BankName")
            cmd.Parameters.Add("@BankAddress", SqlDbType.NVarChar).Value = dtblPayee.Rows(0)("BankAddress")
            cmd.Parameters.Add("@Swift", SqlDbType.VarChar).Value = dtblPayee.Rows(0)("Swift")
            cmd.Parameters.Add("@PayeeAccID", SqlDbType.Int).Value = dtblPayee.Rows(0)("RecID")
            cmd.ExecuteNonQuery()
        End If
    End Sub
    Private Sub LblAssignAccount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblAssignAccount.LinkClicked
        AssignAccountToPmtRQ(Me.GridNewSupplier.CurrentRow.Cells("RecID").Value, Me.GridNewSupplier.CurrentRow.Cells("SupplierID").Value, 0)
        LoadGridRQ()
        LoadGridNewSupplier()
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridNewSupplier.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        Me.LblAssignAccount.Visible = True
        Me.LblGo2Mapping.Visible = True
    End Sub

    Private Sub LblChangeToVND_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblChangeToVND.LinkClicked
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim VNDEquiv As Decimal = Me.GridRQ.CurrentRow.Cells("Amount").Value * CDec(Me.TxtROE.Text)
        VNDEquiv = Math.Round(VNDEquiv, 0)
        If myStaff.City = "SGN" AndAlso Me.GridRQ.CurrentRow.Cells("AccountName").Value <> "" Then
            Dim CustomeRounding As String = ScalarToString("MISC", "VAL1", "cat='CUSTRNDG' and charindex(VAL,'" &
                                                           Me.GridRQ.CurrentRow.Cells("AccountName").Value & "')<>0 ")
            If Not String.IsNullOrEmpty(CustomeRounding) Then
                Dim strVND As String = VNDEquiv.ToString
                Dim HangNgan As String = Strings.Left(strVND, strVND.Length - 3)
                Dim PhanTram As String = Strings.Right(strVND, 3)
                If CInt(PhanTram) > CInt(CustomeRounding) Then
                    VNDEquiv = (CDec(HangNgan) + 1) * 1000
                Else
                    VNDEquiv = CDec(HangNgan) * 1000
                End If
            End If
        End If
        cmd.CommandText = UpdateLogFile("UNC_payments", "Change2VND", Me.TxtROE.Text, Me.GridRQ.CurrentRow.Cells("RecID").Value, "", "", "") &
            "; update UNC_Payments set Curr='VND', Amount=" & VNDEquiv & " Where RecID=" &
            Me.GridRQ.CurrentRow.Cells("RecID").Value
        cmd.ExecuteNonQuery()
        LoadGridRQ()
    End Sub
    Private Sub ChkShowLstAccepted_CheckedChanged(sender As Object, e As EventArgs) Handles ChkShowLstAccepted.CheckedChanged
        LoadGridRQ()
    End Sub
    Private Sub LblUndoAccept_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LckLblUndoAccept.LinkClicked
        If (InStr("SYS_NMH_HXT", myStaff.SICode) > 0 And myStaff.City = "SGN") Or
            (InStr("SYS_NTH", myStaff.SICode) > 0 And myStaff.City = "HAN") Then
            RejectRQ()
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        iCounter = iCounter + 1
        If iCounter = 2 And myStaff.City = "SGN" Then Me.Close()
    End Sub
    Private Sub TabPage1_Enter(sender As Object, e As EventArgs) Handles NewUNC.Enter
        iCounter = -4
    End Sub


    Private Sub LblCashAdv2Guide_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblCashAdv2Guide.LinkClicked
        If Me.GridRQ.RowCount = 0 Then Exit Sub
        Dim tmpCurr As String = Me.GridRQ.Item("Curr", 0).Value
        For i As Int16 = 1 To Me.GridRQ.RowCount - 1
            If Me.GridRQ.Item("Curr", i).Value <> tmpCurr Then Exit Sub
        Next
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        cmd.CommandText = "update UNC_Payments set status='OK', FOP='CSH', FstUpdate=getdate(), LstUpdate=getdate(), LstUser='" &
            myStaff.SICode & "' where pmtLotID<>0 and PmtLotID=" & CInt(Me.txtPmtLotID.Text)
        cmd.ExecuteNonQuery()
        LoadGridRQ()
        Me.GroupBox1.Enabled = False
        Me.txtNewDescr.Enabled = False
        Me.txtNewAmt.Enabled = False
    End Sub
    Private Sub Label11_DoubleClick(sender As Object, e As EventArgs) Handles Label11.DoubleClick
        Me.txtNewDescr.Enabled = True
    End Sub
    Private Sub txtNewDescr_TextChanged(sender As Object, e As EventArgs) Handles txtNewDescr.TextChanged
        If Me.txtNewDescr.Text.Contains("can tru") Then
            Me.txtNewAmt.Enabled = True
        Else
            Me.txtNewAmt.Enabled = False
            Me.txtNewAmt.Text = Format(Me.GridRQ.CurrentRow.Cells("Amount").Value, "#,##0.00")
        End If

    End Sub

    Private Sub TxtAccountName_Enter(sender As Object, e As EventArgs) Handles TxtAccountName.Enter
        If Me.TxtAccountName.Text = "ACCOUNT NAME" Then Me.TxtAccountName.Text = ""
    End Sub

    Private Sub TxtBank_Enter(sender As Object, e As EventArgs) Handles TxtBank.Enter
        If Me.TxtBank.Text = "BANK" Then Me.TxtBank.Text = ""
    End Sub
    Private Sub TxtAccNo_Enter(sender As Object, e As EventArgs) Handles TxtAccNo.Enter
        If Me.TxtAccNo.Text = "ACCOUNT NO" Then Me.TxtAccNo.Text = ""
    End Sub
    Private Sub TxtSwift_Enter(sender As Object, e As EventArgs) Handles TxtSwift.Enter
        If Me.TxtSwift.Text = "SWIFT" Then Me.TxtSwift.Text = ""
    End Sub

    Private Sub lbkFilterByDesc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFilterByDesc.LinkClicked
        LoadGridRQ()
    End Sub

    Private Sub OptXE_CheckedChanged(sender As Object, e As EventArgs)

    End Sub



    Private Sub cboFOP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFOP.SelectedIndexChanged _
        , cboApp.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadGridRQ()
        End If
    End Sub

    Private Sub lblBatchBTF_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblBatchBTF.LinkClicked
        AcceptBTF(False)
    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If GridNewSupplier.CurrentRow Is Nothing Then Exit Sub
        ExecuteNonQuerry(ChangeStatus_ByID("UNC_Payments", "XX", GridNewSupplier.CurrentRow.Cells("Recid").Value), Conn)
        LoadGridNewSupplier()
    End Sub

    Private Sub lbkSaveBatch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSaveBatch.LinkClicked
        SaveUnc(False)
    End Sub

    Private Sub lbkRqFilter_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRqFilter.LinkClicked
        LoadGridDNTT()
    End Sub

    Private Sub TxtCity_Enter(sender As Object, e As EventArgs) Handles TxtCity.Enter
        If Me.TxtCity.Text = "LOCATION" Then Me.TxtCity.Text = ""
    End Sub



    Public Function LoadGridDNTT() As Boolean
        Dim strSQL = "select d.RecID, d.FOP, d.Curr, d.Amount, d.Ref, d.RefId, d.VendorID, d.Vendor" _
                    & ",v.AccountName,v.AccountNumber,v.BankName, d.PaymentId, d.Remark, d.RMKNoiBo" _
                    & ", d.FstUpdate, d.FstUser, d.LstUpdate, d.LstUser, d.Status, d.App, d.Counter" _
                    & ", d.TVC, d.City, d.PrintedBy, PrintedDate, PrintID" _
                    & " from DNTT d left join Vendor v on d.VendorId=v.RecId where d.Status='OK' and d.FstUpdate>='18 Aug 2020'"

        If chkPaid.Checked Then
            strSQL = strSQL & " and PaymentId<>0"
        Else
            strSQL = strSQL & " and PaymentId=0"
        End If

        AddEqualConditionCombo(strSQL, cboRqFOP, "d.FOP")
        AddEqualConditionCombo(strSQL, cboApp)
        AddLikeConditionText(strSQL, txtRef)
        AddEqualConditionCombo(strSQL, cboRqVendor, "Vendor")

        If cboFOP.Text <> "CSH" Then
            strSQL = strSQL & " and TVC='" & CmbTVC.Text & "'"
        End If
        If txtRqLotID.Text > 0 Then
            strSQL = strSQL & " and PmtLotId=" & txtPmtLotID.Text
        End If
        strSQL = strSQL & " order by Vendor"
        dgrDNTT.DataSource = GetDataTable(strSQL)
        dgrDNTT.Columns("remark").Visible = False
        Me.dgrDNTT.Columns(1).Visible = False
        dgrDNTT.Columns("APP").Width = 30
        dgrDNTT.Columns("Curr").Width = 36
        dgrDNTT.Columns("FstUser").Width = 36
        dgrDNTT.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrDNTT.Columns("Amount").DefaultCellStyle.Format = "#,##0.00"

    End Function
    Private Function CheckDataValues() As Boolean
        Dim strFOP As String = ""
        Dim strAPP As String = ""
        Dim strVendor As String = ""
        Dim strCur As String = ""
        Dim strLastCutOff As String

        Dim i As Integer
        For Each objRow As DataGridViewRow In dgrDNTT.Rows
            If objRow.Cells("S").Value Then
                If i = 0 Then
                    strFOP = objRow.Cells("FOP").Value
                    strAPP = objRow.Cells("APP").Value
                    strVendor = objRow.Cells("Vendor").Value
                    strCur = objRow.Cells("Curr").Value
                    strLastCutOff = GetLastCutOffDate(objRow.Cells("VendorId").Value, strCur)

                    If strLastCutOff = "" Then
                        MsgBox("You must set CUT-OFF Balance for " & strVendor & " first!")
                        Return False
                    End If

                ElseIf strFOP <> objRow.Cells("FOP").Value Then
                    MsgBox("Unable to take action on multiple FOPs")
                    Return False
                ElseIf strAPP <> objRow.Cells("APP").Value Then
                    MsgBox("Unable to take action on multiple APPs")
                    Return False
                ElseIf strVendor <> objRow.Cells("Vendor").Value Then
                    MsgBox("Unable to take action on multiple Vendors")
                    Return False
                ElseIf strCur <> objRow.Cells("Curr").Value Then
                    MsgBox("Unable to take action on multiple Currencies")
                    Return False
                End If
                i = i + 1
            End If
        Next

        Return True
    End Function
    Public Function LoadGridPayment() As Boolean
        Dim strSQL = "select v.ShortName as TvAccount, p.* from VendorPayments p" _
            & " left join Vendor v on p.PayerAccountID=v.RecID" _
            & " where p.Status='OK'"

        AddEqualConditionCombo(strSQL, cboAcceptedFOP, "FOP")
        AddEqualConditionCombo(strSQL, cboAcceptedAPP, "APP")
        AddLikeConditionText(strSQL, txtAcceptedRef, "Description")
        AddEqualConditionCombo(strSQL, cboAcceptedVendor, "ShortName")

        strSQL = strSQL & " and p.LstUpdate between '" & CreateFromDate(dtpAcceptDate.Value) & "' and '" & CreateToDate(dtpAcceptDate.Value) & "'"

        If cboAcceptedFOP.Text <> "CSH" Then
            strSQL = strSQL & " and TVC='" & CmbTVC.Text & "'"
        End If
        Select Case chkAcceptedByMyself.CheckState
            Case CheckState.Checked
                strSQL = strSQL & " and p.FstUser='" & myStaff.SICode & "'"
            Case CheckState.Unchecked
                strSQL = strSQL & " and p.FstUser<>'" & myStaff.SICode & "'"
        End Select
        If txtAcceptedLotId.Text > 0 Then
            strSQL = strSQL & " and PmtLotId=" & txtPmtLotID.Text
        End If
        strSQL = strSQL & " order by ShortName"
        dgrPayments.DataSource = GetDataTable(strSQL)
        dgrPayments.Columns("TvAccount").Visible = False
        'Me.dgrPayments.Columns(1).Visible = False
        dgrPayments.Columns("APP").Width = 30
        dgrPayments.Columns("Curr").Width = 36
        dgrPayments.Columns("FstUser").Width = 36
        dgrPayments.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrPayments.Columns("Amount").DefaultCellStyle.Format = "#,##0.00"

    End Function
    Private Sub lbkRqReject_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRqReject.LinkClicked
        Dim lstQuerries As New List(Of String)
        If Not CheckDataValues() Then Exit Sub
        With dgrDNTT.CurrentRow
            If .Cells("PaymentId").Value <> 0 Then
                MsgBox("Unable to reject Paid Request!")
                Exit Sub
            End If
            lstQuerries.Add(ChangeStatus_ByID("LIB.dbo.DNTT", "XX", .Cells("RecId").Value))
            If .Cells("APP").Value = "OPS" Then
                lstQuerries.Add("Update flx.dbo.TOS_Pmt set status='XX’ where DNTTID_New=" _
                                & .Cells("RecId").Value)
            End If

            If UpdateListOfQuerries(lstQuerries, Conn_Web) Then
                LoadGridDNTT()
            Else
                MsgBox("Unable to reject DNTT " & .Cells("RecId").Value)
            End If
        End With

    End Sub

    Private Sub lbkRqAcceptSingleBTF_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRqAcceptSingleBTF.LinkClicked
        If txtAcceptFOP.Text <> "BTF" Then
            MsgBox("You must change FOP first!")
            Exit Sub
        End If
        AcceptDNTT(txtAcceptFOP.Text, txtAcceptVendor.Text, txtAcceptVendor.Tag, txtAcceptCur.Text _
                   , CDec(txtAcceptAmt.Text), txtAcceptApp.Text, txtAcceptRef.Text, CmbTVC.Text _
                   , GridPayer.CurrentRow.Cells("RecId").Value, txtAcceptCounter.Text, True)
    End Sub

    Private Function CreateUncInfo() As Boolean
        Dim i As Integer
        Dim decAcceptAmt As Decimal
        Dim strDesc As String = ""

        For Each objRow As DataGridViewRow In dgrDNTT.Rows
            If objRow.Cells("S").Value Then
                If i = 0 Then
                    txtAcceptVendor.Text = objRow.Cells("Vendor").Value
                    txtAcceptVendor.Tag = objRow.Cells("VendorId").Value
                    txtAcceptFOP.Text = objRow.Cells("FOP").Value
                    txtAcceptApp.Text = objRow.Cells("APP").Value
                    txtAcceptCur.Text = objRow.Cells("Curr").Value
                    txtAcceptCounter.Text = objRow.Cells("Counter").Value
                End If
                decAcceptAmt = decAcceptAmt + objRow.Cells("Amount").Value
                If txtAcceptApp.Text = "OPS" Then
                    strDesc = strDesc & objRow.Cells("Remark").Value & ","
                Else
                    strDesc = strDesc & objRow.Cells("Ref").Value & ","
                End If

                i = i + 1
            End If
        Next
        txtAcceptAmt.Text = Format(decAcceptAmt, "#,##0.00")
        If strDesc.EndsWith(",") Then
            strDesc = Mid(strDesc, 1, strDesc.Length - 1)
        End If
        If txtAcceptApp.Text = "RAS" Then
            Select Case CmbTVC.Text
                Case "TVTR"
                    strDesc = "TransViet TToan " & strDesc
                Case "GDS"
                    strDesc = "Phan Phoi Toan Cau Ttoan " & strDesc
            End Select
        End If
        txtAcceptRef.Text = strDesc
        Return True
    End Function
    Private Function LoadGripPayer() As Boolean
        Dim strSQL As String = "select RecID,  Bank, AccountNumber from Vendor" _
            & " where status='OK' and RecID in (select intVal from Misc where Cat='TvPayer' and Val='" &
            Me.CmbTVC.Text & "' order by Bank, AccountNumber"
        Me.GridPayer.DataSource = GetDataTable(strSQL)
        Me.GridPayer.Columns(0).Visible = False
        Me.GridPayer.Columns(1).Width = 64
        Me.GridPayer.Columns(2).Width = 256
        'Me.GrpBeneficiaryDetail.Enabled = False
        Return True
    End Function

    Private Sub tabDNTT_Enter(sender As Object, e As EventArgs) Handles tabDNTT.Enter
        LoadCombo(cboRqVendor, "Select distinct Vendor as value from DNTT where Status='OK' and Paymentid=0", Conn)
        cboRqVendor.SelectedIndex = -1
        'lbkPreview2020.Visible = True
    End Sub

    Private Sub txtAcceptAmt_Enter(sender As Object, e As EventArgs) Handles txtAcceptAmt.Enter

        If Not CheckDataValues() Then Exit Sub
        CreateUncInfo()

        'EnableAcceptDNTT(False)
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        cboRqVendor.SelectedIndex = -1
        cboRqFOP.SelectedIndex = -1
        cboRqApp.SelectedIndex = -1
        txtRef.Text = ""

    End Sub

    Private Sub lbkRqAcceptCSH_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRqAcceptCSH.LinkClicked
        If txtAcceptFOP.Text <> "CSH" Then
            MsgBox("You must change FOP first!")
            Exit Sub
        End If
        AcceptDNTT("CSH", txtAcceptVendor.Text, txtAcceptVendor.Tag, txtAcceptCur.Text _
                  , CDec(txtAcceptAmt.Text), txtAcceptApp.Text, txtAcceptRef.Text, CmbTVC.Text _
                  , GridPayer.CurrentRow.Cells("RecId").Value, txtAcceptCounter.Text, False)
    End Sub

    Private Sub lbkRqAcceptCRD_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRqAcceptCRD.LinkClicked
        If txtAcceptFOP.Text <> "CRD" Then
            MsgBox("You must change FOP first!")
            Exit Sub
        End If
        AcceptDNTT("CRD", txtAcceptVendor.Text, txtAcceptVendor.Tag, txtAcceptCur.Text _
                  , CDec(txtAcceptAmt.Text), txtAcceptApp.Text, txtAcceptRef.Text, CmbTVC.Text _
                  , GridPayer.CurrentRow.Cells("RecId").Value, txtAcceptCounter.Text, False)
    End Sub

    Private Sub txtAcceptAmt_Leave(sender As Object, e As EventArgs) Handles txtAcceptAmt.Leave
        If IsNumeric(txtAcceptAmt.Text) AndAlso txtAcceptAmt.Text > 0 Then
            EnableAcceptDNTT(True)
        End If

    End Sub

    Private Sub lbkRqAcceptMultiBTF_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRqAcceptMultiBTF.LinkClicked
        If txtAcceptFOP.Text <> "BTF" Then
            MsgBox("You must change FOP first!")
            Exit Sub
        End If
        AcceptDNTT(txtAcceptFOP.Text, txtAcceptVendor.Text, txtAcceptVendor.Tag, txtAcceptCur.Text _
                  , CDec(txtAcceptAmt.Text), txtAcceptApp.Text, txtAcceptRef.Text, CmbTVC.Text _
                  , GridPayer.CurrentRow.Cells("RecId").Value, txtAcceptCounter.Text, False)
    End Sub



    Private Function EnableAcceptDNTT(blnVisible As Boolean) As Boolean
        txtAcceptRef.Visible = blnVisible
        txtAcceptFOP.Visible = blnVisible
        txtAcceptVendor.Visible = blnVisible
        txtAcceptCur.Visible = blnVisible
        txtAcceptCounter.Visible = blnVisible
        grbAcceptDNTT.Visible = blnVisible
        Return True
    End Function
    Public Function PrintTCB_TrongNuoc() As Boolean
        Dim objXls As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim objWsh As New Microsoft.Office.Interop.Excel.Worksheet
        Dim tblTvc As DataTable
        Dim objPayBank As New clsBank
        Dim objReceiveBank As New clsBank

        With dgrPayments.CurrentRow
            tblTvc = GetDataTable("Select top 1 * from Vendor where RecId=" & .Cells("PayerAccountId").Value)
            objWbk = objXls.Workbooks.Open(My.Application.Info.DirectoryPath _
                            & "\R12_" & CmbTemplate.Text,,,, "aibiet")
            objWsh = objWbk.Sheets("RPT")
            objWsh.Activate()

            objWsh.Range("I4").Value = .Cells("RefNo").Value
            objWsh.Range("B6").Value = tblTvc.Rows(0)("AccountName")
            objWsh.Range("E7").Value = .Cells("Amount").Value
            objWsh.Range("B8").Value = Mid(tblTvc.Rows(0)("AccountNumber"), 5).Trim
            objPayBank.ParseName(tblTvc.Rows(0)("BankName"))
            objWsh.Range("B9").Value = objPayBank.BankName
            objWsh.Range("D9").Value = objPayBank.Branch
            objWsh.Range("B10").Value = tblTvc.Rows(0)("BankAddress")
            objWsh.Range("e10").Value = TienBangChu(.Cells("Amount").Value)
            objWsh.Range("b11").Value = .Cells("AccountName").Value
            objWsh.Range("b12").Value = Mid(.Cells("AccountNumber").Value, 5).Trim
            objWsh.Range("b13").Value = .Cells("BankAddress").Value
            objReceiveBank.ParseName(.Cells("BankName").Value)
            objWsh.Range("b14").Value = objReceiveBank.BankName
            objWsh.Range("b15").Value = objReceiveBank.Branch
            objWsh.Range("d15").Value = .Cells("BankAddress").Value
            objWsh.Range("a17").Value = .Cells("Description").Value

        End With
        objWsh.Range("E7").NumberFormat = "#,##0.00"
        objWsh.PrintOutEx()
        objWbk.Close(False)
        objXls.Quit()
        Return True
    End Function
    Public Function PrintTCB_NgoaiNuoc() As Boolean
        Dim objXls As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim objWsh As New Microsoft.Office.Interop.Excel.Worksheet
        Dim tblTvc As DataTable
        Dim tblVendor As DataTable
        Dim objPayBank As New clsBank

        Dim strTvcVndAccount As String

        With dgrPayments.CurrentRow
            tblTvc = GetDataTable("Select top 1 * from Vendor where RecId=" & .Cells("PayerAccountId").Value)
            tblVendor = GetDataTable("Select top 1 * from Vendor where Status='OK' and AccountName='" _
                                     & .Cells("AccountName").Value & "'")
            strTvcVndAccount = ScalarToString("Vendor", "AccountNumber" _
                , "RecID=" & tblTvc.Rows(0)("CompanyId") _
                & " and BankName=" _
                & "(select BankName" _
                & " from Vendor where recid=" & dgrPayments.CurrentRow.Cells("PayerAccountId").Value _
                & ") and left(accountnumber,3)='VND'")

            If strTvcVndAccount = "" Then
                MsgBox("Không tìm thấy tài khoản VND để trích phí!")
                objXls.Quit()
                Return False
            End If
            objWbk = objXls.Workbooks.Open(My.Application.Info.DirectoryPath _
                                           & "\R12_" & CmbTemplate.Text,, True,, "aibiet")
            objWsh = objWbk.Sheets("RPT")
            objWsh.Activate()

            objWsh.Range("E2").Value = .Cells("RefNo").Value & ")"
            objWsh.Range("D5").Value = Mid(tblTvc.Rows(0)("AccountNumber"), 5).Trim
            objWsh.Range("c6").Value = Now.Date
            objWsh.Range("D6").Value = "Số tiền chuyển đi bằng số (Amt in figures): " & Format(.Cells("Amount").Value, "#,##0.00")
            objWsh.Range("D7").Value = "Loại tiền (currency): " & .Cells("Curr").Value
            objWsh.Range("b8").Value = "Số tiền chuyển đi bằng chữ (Amt in words): " & TienBangChu(.Cells("Amount").Value)

            objWsh.Range("B9").Value = "Tên (full Name): " & tblTvc.Rows(0)("AccountName")
            objWsh.Range("D10").Value = "Địa chỉ (add): " & tblTvc.Rows(0)("Address")

            objWsh.Range("b14").Value = "Tên (Name): " & .Cells("BankName").Value
            objWsh.Range("d14").Value = "Mã NH (Bank code: Swift code/ABA/Routing /UID/Sort code/A/c at other bank…): " _
                                        & .Cells("Swift").Value
            objWsh.Range("b15").Value = "Địa chỉ (Add):" & .Cells("BankAddress").Value
            objWsh.Range("b16").Value = "Tên (Name): " & .Cells("AccountName").Value
            objWsh.Range("d16").Value = "Số Tài khoản (A/c No): " & Mid(.Cells("AccountNumber").Value, 5).Trim
            objWsh.Range("b17").Value = "Địa chỉ (Add):" & tblVendor.Rows(0)("Address")
            objWsh.Range("b18").Value = .Cells("Description").Value
            objWsh.Range("d21").Value = .Cells("Description").Value

        End With
        objWsh.PrintOutEx()
        objWbk.Close(False)
        objXls.Quit()
        Return True
    End Function
    Public Function PrintVCB_LCT() As Boolean
        Dim objXls As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim objWsh As New Microsoft.Office.Interop.Excel.Worksheet
        Dim tblTvc As DataTable
        Dim tblVendor As DataTable
        Dim objPayBank As New clsBank

        'Dim strTvcVndAccount As String

        With dgrPayments.CurrentRow
            tblTvc = GetDataTable("Select top 1 * from Vendor where RecId=" & .Cells("PayerAccountId").Value)
            tblVendor = GetDataTable("Select top 1 * from Vendor where Status='OK' and AccountName='" _
                                     & .Cells("AccountName").Value & "'")
            'strTvcVndAccount = ScalarToString("unc_accounts", "AccountNumber" _
            '    , "companyID=" & tblTvc.Rows(0)("CompanyId") _
            '    & " and BankName=" _
            '    & "(select BankName" _
            '    & " from unc_accounts where recid=" & dgrPayments.CurrentRow.Cells("PayerAccountId").Value _
            '    & ") and left(accountnumber,3)='VND'")

            'If strTvcVndAccount = "" Then
            '    MsgBox("Không tìm thấy tài khoản VND để trích phí!")
            '    objXls.Quit()
            '    Return False
            'End If
            objWbk = objXls.Workbooks.Open(My.Application.Info.DirectoryPath _
                                           & "\R12_" & CmbTemplate.Text,, True,, "aibiet")
            objWsh = objWbk.Sheets("RPT")
            objWsh.Activate()

            objWsh.Range("g1").Value = .Cells("RefNo").Value & ")"
            objWsh.Range("D5").Value = Now.Date
            objWsh.Range("G5").Value = Format(.Cells("Amount").Value, "#,##0.00")
            objWsh.Range("b6").Value = "Số tiền chuyển đi bằng chữ (Amt in words): " _
                & TienBangChu(.Cells("Amount").Value) & .Cells("Curr").Value
            objWsh.Range("E7").Value = Mid(tblTvc.Rows(0)("AccountNumber"), 5).Trim
            objWsh.Range("D10").Value = tblTvc.Rows(0)("AccountName")
            objWsh.Range("B11").Value = tblTvc.Rows(0)("Address")
            objWsh.Range("b15").Value = "Tên (Name): " & .Cells("BankName").Value
            objWsh.Range("G15").Value = .Cells("Swift").Value
            objWsh.Range("b16").Value = "Địa chỉ (Add):" & .Cells("BankAddress").Value
            objWsh.Range("b17").Value = "Tên (Name): " & .Cells("AccountName").Value
            objWsh.Range("E18").Value = .Cells("AccountNumber").Value
            objWsh.Range("b19").Value = "Địa chỉ (Add):" & tblVendor.Rows(0)("Address")
            objWsh.Range("b20").Value = .Cells("Description").Value

        End With
        objWsh.PrintOutEx()
        objWbk.Close(False)
        objXls.Quit()
        Return True
    End Function
    Public Function PrintVCB_UNC() As Boolean
        Dim objXls As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim objWsh As New Microsoft.Office.Interop.Excel.Worksheet
        Dim tblTvc As DataTable
        Dim tblVendor As DataTable
        Dim objPayBank As New clsBank

        'Dim strTvcVndAccount As String

        With dgrPayments.CurrentRow
            tblTvc = GetDataTable("Select top 1 * from Vendor where RecId=" & .Cells("PayerAccountId").Value)
            tblVendor = GetDataTable("Select top 1 * from Vendor where Status='OK' and AccountName='" _
                                     & .Cells("AccountName").Value & "'")
            'strTvcVndAccount = ScalarToString("unc_accounts", "AccountNumber" _
            '    , "companyID=" & tblTvc.Rows(0)("CompanyId") _
            '    & " and BankName=" _
            '    & "(select BankName" _
            '    & " from unc_accounts where recid=" & dgrPayments.CurrentRow.Cells("PayerAccountId").Value _
            '    & ") and left(accountnumber,3)='VND'")

            'If strTvcVndAccount = "" Then
            '    MsgBox("Không tìm thấy tài khoản VND để trích phí!")
            '    objXls.Quit()
            '    Return False
            'End If
            objWbk = objXls.Workbooks.Open(My.Application.Info.DirectoryPath _
                                           & "\R12_" & CmbTemplate.Text,, True,, "aibiet")
            objWsh = objWbk.Sheets("RPT")
            objWsh.Activate()

            objWsh.Range("g2").Value = .Cells("RefNo").Value & ")"
            objWsh.Range("c6").Value = "'" & tblTvc.Rows(0)("AccountNumber")
            objWsh.Range("f6").Value = .Cells("Curr").Value & Format(.Cells("Amount").Value, "#,##0.00")
            objWsh.Range("c7").Value = tblTvc.Rows(0)("AccountName")
            objWsh.Range("c8").Value = tblTvc.Rows(0)("Address")
            objWsh.Range("e8").Value = TienBangChu(.Cells("Amount").Value) & .Cells("Curr").Value & " đồng chẵn."
            objWsh.Range("c10").Value = tblTvc.Rows(0)("BankName")
            objWsh.Range("e12").Value = .Cells("Description").Value
            objWsh.Range("c16").Value = "'" & .Cells("AccountNumber").Value
            objWsh.Range("c17").Value = .Cells("AccountName").Value
            objWsh.Range("c19").Value = .Cells("BankAddress").Value
            objWsh.Range("c20").Value = .Cells("BankName").Value

        End With
        objWsh.PrintOutEx()
        objWbk.Close(False)
        objXls.Quit()
        Return True
    End Function
    Public Function PrintVPB_UNC_TrongNuoc() As Boolean
        Dim objXls As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim objWsh As New Microsoft.Office.Interop.Excel.Worksheet
        Dim tblTvc As DataTable
        Dim tblVendor As DataTable
        Dim objPayBank As New clsBank

        'Dim strTvcVndAccount As String

        With dgrPayments.CurrentRow
            tblTvc = GetDataTable("Select top 1 * from Vendor where RecId=" & .Cells("PayerAccountId").Value)
            tblVendor = GetDataTable("Select top 1 * from Vendor where Status='OK' and AccountName='" _
                                     & .Cells("AccountName").Value & "'")
            'strTvcVndAccount = ScalarToString("unc_accounts", "AccountNumber" _
            '    , "companyID=" & tblTvc.Rows(0)("CompanyId") _
            '    & " and BankName=" _
            '    & "(select BankName" _
            '    & " from unc_accounts where recid=" & dgrPayments.CurrentRow.Cells("PayerAccountId").Value _
            '    & ") and left(accountnumber,3)='VND'")

            'If strTvcVndAccount = "" Then
            '    MsgBox("Không tìm thấy tài khoản VND để trích phí!")
            '    objXls.Quit()
            '    Return False
            'End If
            objWbk = objXls.Workbooks.Open(My.Application.Info.DirectoryPath _
                                           & "\R12_" & CmbTemplate.Text,, True,, "aibiet")
            objWsh = objWbk.Sheets("RPT")
            objWsh.Activate()
            objWsh.Range("F3").Value = "PAYMENT ORDER " & .Cells("RefNo").Value
            objWsh.Range("h8").Value = tblTvc.Rows(0)("AccountName")
            objWsh.Range("r8").Value = .Cells("RefNo").Value
            objWsh.Range("e9").Value = tblTvc.Rows(0)("Address")
            objWsh.Range("e10").Value = tblTvc.Rows(0)("AccountNumber")
            objPayBank.ParseName(tblTvc.Rows(0)("BankName"))
            objWsh.Range("E15").Value = objPayBank.Branch
            objWsh.Range("g11").Value = .Cells("AccountName").Value
            objWsh.Range("e12").Value = ScalarToString("Vendor", "Address", "RecId=" & .Cells("VendorId").Value)
            objWsh.Range("f13").Value = "'" & .Cells("AccountNumber").Value
            objWsh.Range("f14").Value = .Cells("BankName").Value
            objWsh.Range("o15").Value = .Cells("BankAddress").Value
            objWsh.Range("a17").Value = TienBangChu(.Cells("Amount").Value) & " đồng chẵn."
            objWsh.Range("l17").Value = Format(.Cells("Amount").Value, "#,##0")
            If .Cells("DeductBalance").Value = True Then
                objWsh.Range("a19").Value = .Cells("Description").Value & " Can tru thanh toan thua"
            Else
                objWsh.Range("a19").Value = .Cells("Description").Value
            End If


        End With
        objXls.Visible = True
        objWsh.PrintOutEx()
        objWbk.Close(False)
        objXls.Quit()
        Return True
    End Function
    Public Function PrintVPB_UNC_NgoaiNuoc() As Boolean
        Dim objXls As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim objWsh As New Microsoft.Office.Interop.Excel.Worksheet
        Dim tblTvc As DataTable
        Dim tblVendor As DataTable
        Dim objPayBank As New clsBank
        Dim strTvcVndAccount As String
        Dim strCurNameVie As String

        With dgrPayments.CurrentRow
            tblTvc = GetDataTable("Select top 1 * from Vendor where RecId=" & .Cells("PayerAccountId").Value)
            tblVendor = GetDataTable("Select top 1 * from Vendor where Status='OK' and AccountName='" _
                                     & .Cells("AccountName").Value & "'")
            strTvcVndAccount = ScalarToString("Vendor", "AccountNumber" _
                , "RecID=" & tblTvc.Rows(0)("CompanyId") _
                & " and BankName=" _
                & "(select BankName" _
                & " from Vendor where recid=" & dgrPayments.CurrentRow.Cells("PayerAccountId").Value _
                & ") and left(accountnumber,3)='VND'")

            If strTvcVndAccount = "" Then
                MsgBox("Không tìm thấy tài khoản VND để trích phí!")
                objXls.Quit()
                Return False
            End If
            strCurNameVie = ScalarToString("Lib.dbo.Currency", "Top 1 CurrencyNameVie", "Code='" _
                                           & .Cells("Curr").Value & "'")
            If strTvcVndAccount = "" Then
                MsgBox("Không tìm thấy tên tiếng Việt của " & .Cells("Curr").Value)
                objXls.Quit()
                Return False
            End If

            objWbk = objXls.Workbooks.Open(My.Application.Info.DirectoryPath _
                                           & "\R12_" & CmbTemplate.Text,, True,, "aibiet")
            objWsh = objWbk.Sheets("RPT")
            objWsh.Activate()

            objWsh.Range("c5").Value = tblTvc.Rows(0)("AccountName")
            objWsh.Range("h5").Value = "Mã KH:" & tblTvc.Rows(0)("CIF") & vbLf & "CIF No"
            objWsh.Range("C6").Value = tblTvc.Rows(0)("Address")
            objWsh.Range("r8").Value = .Cells("RefNo").Value
            objWsh.Range("G11").Value = Mid(tblTvc.Rows(0)("AccountNumber"), 5).Trim
            objWsh.Range("G12").Value = Mid(strTvcVndAccount, 5).Trim

            Dim chk As CheckBox
            Dim blnFoundCur As Boolean
            For Each chk In objWsh.CheckBoxes
                If chk.Name = "chk" & .Cells("Curr").Value Then
                    chk.Checked = True
                    blnFoundCur = True
                Else
                    chk.Checked = False
                End If
            Next
            If Not blnFoundCur Then
                objWsh.CheckBoxes("chkOTHER").Value = True
                objWsh.CheckBoxes("chkOTHER").Text = .Cells("Curr").Value
            End If
            objWsh.Range("g15").Value = Format(.Cells("Amount").Value, "#,##0.00")
            objWsh.Range("d17").Value = TienBangChu(.Cells("Amount").Value) & .Cells("Curr").Value _
                & " " & strCurNameVie
            objWsh.Range("d20").Value = Now.Date
            objWsh.Range("f21").Value = Mid(.Cells("AccountNumber").Value, 5).Trim
            objWsh.Range("f22").Value = .Cells("AccountName").Value
            objWsh.Range("e23").Value = .Cells("Address").Value
            objWsh.Range("f25").Value = .Cells("Swift").Value
            objWsh.Range("e26").Value = .Cells("BankName").Value
            objWsh.Range("e27").Value = .Cells("BankAddress").Value
            objWsh.Range("h28").Value = .Cells("Country").Value
            objWsh.Range("d29").Value = .Cells("Description").Value

            If MsgBox("Buy foreing currency?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                objWsh.Range("e35").Value = objWsh.Range("g15").Value
                objWsh.Range("c36").Value = objWsh.Range("d17").Value
            Else
                objWsh.Range("e35").Value = ""
                objWsh.Range("c36").Value = ""
            End If

            objWsh.Range("h38").Value = Now.Date


        End With
        objWsh.PrintOutEx()
        objWbk.Close(False)
        objXls.Quit()
        Return True
    End Function
    Private Function UpdatePrinted4DNTT(intPaymentId As Integer) As Boolean
        Dim strQuerry As String = "update [VendorPayments] set HasPrinted ='True' where RecId=" & intPaymentId
        Return ExecuteNonQuerry(strQuerry, Conn)
    End Function
    Private Sub lbkPreview2020_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPrint2020.LinkClicked
        If tabUNC.SelectedTab.Name <> tabAcceptedPayment.Name Then Exit Sub
        Select Case CmbTemplate.Text
            Case "AGR_NgoaiNuoc.xlt"

            Case "AGR_TrongNuoc.xlt"

            Case "TCB_NgoaiNuoc.xlt"
                If PrintTCB_NgoaiNuoc() Then
                    UpdatePrinted4DNTT(dgrPayments.CurrentRow.Cells("Recid").Value)
                End If
            Case "TCB_TrongNuoc.xlt"
                If PrintTCB_TrongNuoc() Then
                    UpdatePrinted4DNTT(dgrPayments.CurrentRow.Cells("Recid").Value)
                End If

            Case "VCB_LCT.xlt"
                If PrintVCB_LCT() Then
                    UpdatePrinted4DNTT(dgrPayments.CurrentRow.Cells("Recid").Value)
                End If

            Case "VCB_UNC.xlt"
                If PrintVCB_UNC() Then
                    UpdatePrinted4DNTT(dgrPayments.CurrentRow.Cells("Recid").Value)
                End If

            Case "VPB_UNC_TrongNuoc.xlt"
                If PrintVPB_UNC_TrongNuoc() Then
                    UpdatePrinted4DNTT(dgrPayments.CurrentRow.Cells("Recid").Value)
                End If
            Case "VPB_UNC_NgoaiNuoc.xlt"
                If PrintVPB_UNC_NgoaiNuoc() Then
                    UpdatePrinted4DNTT(dgrPayments.CurrentRow.Cells("Recid").Value)
                End If
        End Select

        'Dim intPayerAccountId As Integer = ScalarToInt("UNC_payments", "PayerAccountID", "RecID=" & Recno)

        'If Me.CmbTemplate.Text.Contains("SCB") And
        '     intPayerAccountId > PayerAccouID_frm And
        '    intPayerAccountId < PayerAccouID_To Then
        '    InHoaDon(Application.StartupPath, Me.CmbTemplate.Text, "V", Recno, Now.Date, Now.Date, 0, "NEW", MySession.Domain)

        '    If MsgBox("Wanna Print it Out?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle) = vbNo Then Exit Sub

        '    InHoaDon(Application.StartupPath, Me.CmbTemplate.Text, "O", Recno, Now.Date, Now.Date, 0, "NEW", MySession.Domain)

        '    Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        '    cmd.CommandText = "update UNC_payments set HasPrinted=1 where recid=" & Recno
        '    cmd.ExecuteNonQuery()

        'Else
        '    InHoaDon(Application.StartupPath, Me.CmbTemplate.Text, "O", Recno, Now.Date, Now.Date, 0, "NEW", MySession.Domain)
        'End If

        'Dim strBankName As String = ScalarToString("UNC_Accounts", "BankName", "RecID=" & intPayerAccountId)
        'If strBankName.Contains("CITIBANK") Or strBankName.Contains("Standard Chartered Bank") Then
        '    CreateSms4UNC(Recno, strBankName, False)
        'End If
    End Sub

    'Ends Edit Mode So CellValueChanged Event Can Fire
    Private Sub EndEditMode(sender As System.Object,
                        e As EventArgs) _
            Handles dgrDNTT.CurrentCellDirtyStateChanged
        'if current cell of grid is dirty, commits edit
        If dgrDNTT.IsCurrentCellDirty Then
            dgrDNTT.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub DataGridCellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgrDNTT.CellValueChanged

    End Sub

    'Executes when Cell Value on a DataGridView changes
    Private Sub DataGridCellValueChanged(sender As DataGridView,
                                     e As DataGridViewCellEventArgs) _
            Handles dgrDNTT.CellValueChanged
        'check that row isn't -1, i.e. creating datagrid header
        If e.RowIndex = -1 Then Exit Sub

        'mark as dirty
        If e.ColumnIndex = 0 Then
            txtAcceptAmt.Text = 0
            EnableAcceptDNTT(False)
        End If


    End Sub
    Private Function ResetAcceptedPaymentFilter() As Boolean
        cboAcceptedVendor.SelectedIndex = -1
        cboAcceptedFOP.SelectedIndex = -1
        cboAcceptedAPP.SelectedIndex = -1
        txtAcceptedRef.Text = ""
        chkAcceptedByMyself.Checked = True
    End Function
    Private Sub FollowUp_Enter(sender As Object, e As EventArgs) Handles FollowUp.Enter
        lbkPrint2020.Visible = False
    End Sub

    Private Sub lbkAcceptedClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAcceptedClear.LinkClicked
        ResetAcceptedPaymentFilter()
    End Sub

    Private Sub lbkAcceptedFilter_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAcceptedFilter.LinkClicked
        LoadGridPayment()
    End Sub

    Private Sub lbklbkRqAccept2Guide_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbklbkRqAccept2Guide.LinkClicked
        If Me.dgrDNTT.RowCount = 0 Then Exit Sub
        Dim tmpCurr As String = Me.dgrDNTT.Item("Curr", 0).Value
        For i As Int16 = 1 To Me.GridRQ.RowCount - 1
            If Me.GridRQ.Item("Curr", i).Value <> tmpCurr Then
                MsgBox("Multi Currencies are NOT allowed")
                Exit Sub
            End If
        Next
        ExecuteNonQuerry("update DNTT set status='OK', FOP='CSH', FstUpdate=getdate(), LstUpdate=getdate()" _
                         & ", LstUser='" & myStaff.SICode _
                         & "' where pmtLotID<>0 and PmtLotID=" & CInt(Me.txtPmtLotID.Text), Conn_Web)
        LoadGridDNTT()
    End Sub

    Private Sub OptZZ_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub chkOnceOff_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnceOff.CheckedChanged
        LoadVendor("ZZ")
        If chkOnceOff.Checked Then
            cboCAT.SelectedIndex = -1
        End If
    End Sub

    Private Sub cboCAT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCAT.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            If cboCAT.SelectedIndex <> -1 Then
                chkOnceOff.Checked = False
            End If
            LoadVendor(Mid(cboCAT.Text, 1, 2))
            If cboCAT.Text.StartsWith("GD") Then
                Select Case Me.CmbTVC.Text
                    Case "TVTR", "TVTRDAD"
                        'SGN chi duoc dung tai khoan VCB VND cua TVTR tra cho Guide
                        If myStaff.City <> "SGN" Then
                            MsgBox("Guide must be paid by TVTR-VCB-VND/TVTR DAD-TCB-VND!", MsgBoxStyle.Critical, msgTitle)
                            cboCAT.SelectedIndex = -1
                        End If

                    Case Else
                        MsgBox("Guide must be paid by TVTR-VCB-VND/TVTR DAD-TCB-VND!", MsgBoxStyle.Critical, msgTitle)
                        cboCAT.SelectedIndex = -1
                End Select
            End If
        End If
    End Sub



    Private Sub tabAcceptedPayment_Enter(sender As Object, e As EventArgs) Handles tabAcceptedPayment.Enter
        LoadCombo(cboAcceptedVendor, "Select distinct Vendor as value from DNTT where Status='OK' and Paymentid=0", Conn)
        ResetAcceptedPaymentFilter()
        lbkPrint2020.Visible = True
    End Sub

    Private Sub lbkImport2AOP_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkImport2AOP.LinkClicked
        With GridRQ.CurrentRow
            If .Cells("Status").Value = "OK" AndAlso ("BTF_CSH").Contains(.Cells("FOP").Value) _
                AndAlso Not ImportAopUNC(.Cells("RecId").Value, .Cells("APP").Value, .Cells("Counter").Value) Then
                MsgBox("Unable to import UNC to AOP:" & .Cells("RecId").Value)
                Append2TextFile("Unable to import UNC to AOP:" & .Cells("RecId").Value)
            End If
        End With

    End Sub

    Private Sub lbkCashVCB_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCashVCB.LinkClicked
        If GridPayer.CurrentRow.Cells("Bank").Value <> "***" Then
            MsgBox("Cần phải chọn tài khoản tiền mặt tương ứng!")
            Exit Sub
        End If
        If AcceptRQ("CSH", Me.GridRQ.CurrentRow.Cells("recID").Value, False,,, GridRQ.CurrentRow.Cells("APP").Value _
                    , GridRQ.CurrentRow.Cells("Counter").Value, GridRQ.CurrentRow.Cells("TRX_TC").Value) Then
            Select Case GridPayer.CurrentRow.Cells("RecId").Value
                Case 11090, 9947
                    If CmbTVC.Text = "TVTR" OrElse CmbTVC.Text = "GDS" Then
                        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
                        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
                        Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
                        Dim strAmountInLetters As String
                        Dim intBreakDown As Integer

                        objWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath & "\VCB Cash Deposit.xls",, True)
                        objWsh = objWbk.Sheets("FORM")
                        With GridRQ.CurrentRow
                            objWsh.Range("c9").Value = "'" & .Cells("AccountNumber").Value
                            objWsh.Range("c10").Value = "'" & .Cells("AccountName").Value
                            objWsh.Range("c11").Value = .Cells("BankAddress").Value
                            objWsh.Range("c12").Value = "'" & .Cells("BankName").Value
                            objWsh.Range("h9").Value = "'" & Format(.Cells("Amount").Value, "#,##0")
                            objWsh.Range("k9").Value = "'" & .Cells("Curr").Value
                            strAmountInLetters = TienBangChu(.Cells("Amount").Value) & "đồng"
                            If strAmountInLetters.Length <= 32 Then
                                objWsh.Range("h10").Value = strAmountInLetters
                            ElseIf strAmountInLetters.Length <= 64 Then
                                intBreakDown = InStrRev(strAmountInLetters, " ", 32)
                                objWsh.Range("h10").Value = Mid(strAmountInLetters, 1, intBreakDown).Trim
                                objWsh.Range("g11").Value = Mid(strAmountInLetters, intBreakDown + 1).Trim
                            Else
                                intBreakDown = InStrRev(strAmountInLetters, " ", 32)
                                objWsh.Range("h10").Value = Mid(strAmountInLetters, 1, intBreakDown).Trim
                                strAmountInLetters = Mid(strAmountInLetters, intBreakDown + 1).Trim
                                intBreakDown = InStrRev(strAmountInLetters, " ", 32)
                                objWsh.Range("g11").Value = Mid(strAmountInLetters, 1, intBreakDown).Trim
                                objWsh.Range("g12").Value = Mid(strAmountInLetters, intBreakDown + 1).Trim
                            End If
                            objWsh.Range("b19").Value = Mid(.Cells("Description").Value, 1, 32)
                        End With
                        objExcel.Visible = True
                        objWsh.PrintPreview(False)
                        LoadGridRQ()
                    End If
            End Select
        End If

    End Sub

    Private Function LoadUnc2Switch(strUnc As String) As Boolean
        LoadDataGridView(dgrSwitchUNC, "select RecId, RefNo,SingleBTF, PayerAccountID, AccountName, AccountNumber, BankName, BankAddress" _
                         & ", Curr, Amount, Description from UNC_Payments where Status<>'xx' and RefNo='" & strUnc & "'", Conn)
    End Function
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        LoadUnc2Switch(txtRefNo.Text)
    End Sub

    Private Sub lbkSwitch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSwitch.LinkClicked
        If dgrSwitchUNC.CurrentRow Is Nothing Then Exit Sub
        If ExecuteNonQuerry("Update UNC_Payments set SingleBTF = ~SingleBTF where RecId=" _
                            & dgrSwitchUNC.CurrentRow.Cells("RecId").Value, Conn) Then
            LoadUnc2Switch(txtRefNo.Text)
        Else
            MsgBox("Unable to Switch Single/Batch for UNC " & txtRefNo.Text)
        End If
    End Sub

    Private Sub tabAcceptedPayment_LostFocus(sender As Object, e As EventArgs) Handles tabAcceptedPayment.LostFocus
        lbkPrint2020.Visible = False
    End Sub

    Private Sub GridRQ_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridRQ.CellContentClick

    End Sub

    Private Sub dgrPayments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrPayments.CellClick
        If dgrPayments.CurrentRow IsNot Nothing Then
            txtTvVendor.Text = dgrPayments.CurrentRow.Cells("TvAccount").Value
        End If
    End Sub

    'Private Sub txtNewAmt_Enter(sender As Object, e As EventArgs) Handles txtNewAmt.Enter
    '    If txtNewAmt.Text <> "" AndAlso mdecRequestedAmt = 0 Then
    '        Decimal.TryParse(txtNewAmt.Text, mdecRequestedAmt)
    '    End If
    'End Sub

    Private Sub txtNewAmt_LostFocus(sender As Object, e As EventArgs) Handles txtNewAmt.LostFocus
        If txtNewAmt.Text <> "" Then
            Dim decAcceptedAmt As Decimal
            Decimal.TryParse(txtNewAmt.Text, decAcceptedAmt)
            If decAcceptedAmt <> 0 Then
                mdecChangedAmt = mdecRequestedAmt - decAcceptedAmt
            End If
        End If
    End Sub
End Class