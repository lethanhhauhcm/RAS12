Public Class UNC_support
    Private varAction As String, WhoIs As String
    Private fName As String, strDK_AddInvNo As String = ""
    Private PayerAccouID_frm As Integer, PayerAccouID_To As Integer
    Private mstrTeam As String
    Private mstrFirstLoadCompleted As Boolean
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal parAction As String, pWho As String, Optional strTeam As String = "")
        InitializeComponent()
        varAction = parAction
        WhoIs = pWho
        mstrTeam = strTeam
    End Sub

    Private Sub UNC_support_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LblDeleteUNC.Enabled = IIf(InStr("HXT_SYS_NMH", myStaff.SICode) > 0, True, False)
        Me.CmbCAT.Text = Me.CmbCAT.Items(0).ToString

        LoadCombo(Me.CmbTVC, "select distinct VAL as Value from Misc where CAT='TVPayer'", Conn)
        CmbTVC.SelectedIndex = -1
        If varAction = "ACCOUNT" Then
            Me.GrpAccount.Visible = True
            Me.GrpAccount.Top = 35
            Me.GrpAccount.Left = 5
            Me.GrpAccount.Height = 484
            GenCmbValue()
            Me.CmbCurr.Text = "VND"
            cboBankInVietnam.SelectedIndex = 0
        ElseIf varAction = "CANTEXT" Then
            Me.GrpCanText.Visible = True
            Me.GrpCanText.Top = 35
            Me.GrpCanText.Left = 5
            Me.GrpCanText.Width = 512
            Me.GrpCanText.Height = 484
        ElseIf varAction = "EDIT" Then
            CheckRightForALLForm(Me)
            Me.Text = "TransViet Travel :: Accounting :. Editing or Reprinting Payment Order"
            Me.GrpEdit.Visible = True
            Me.GrpEdit.Top = 5
            Me.GrpEdit.Left = 5
            Me.txtFrm.Value = Now.Date.AddDays(-7)
            Me.GrpEdit.Height = 512
            'Me.GrpEdit.Width = 1000
            GrpEdit.Refresh()
            GridUNC.Height = 420

            LoadGridUNC("")
            GridUNC.Columns("TRX_TC").DisplayIndex = 5
            If WhoIs = "KTT" Then
                Me.LblReprint.Enabled = False
                Me.LblEdit.Enabled = False
                Me.LblSave.Enabled = False
                Me.LblDeleteUNC.Enabled = False
                Me.LblUpdateInvNo.Enabled = False
                GridUNC.Height = 420
                Me.GrpEdit.Height = 600
                Me.GridUNC.Columns("PayerAccountID").Visible = True
                Me.GridUNC.Columns("PayerAccountID").DisplayIndex = 4
                dgrInvTcode.Visible = True
            End If
        ElseIf varAction.Contains("ADDINVNO") Then
            Me.Text = "TransViet Travel :: Accounting :. Invoice Follow Up"
            Me.GrpEdit.Visible = True
            Me.GrpEdit.Top = 5
            Me.GrpEdit.Left = 5
            Me.GrpEdit.Height = 600
            Me.GrpEdit.Width = 1000
            If mstrTeam = "N-A" Then
                GridUNC.Height = 420
                dgrInvTcode.Visible = True
            Else
                GridUNC.Height = 500
            End If


            Me.txtFrm.Value = Now.Date.AddDays(-7)
            Me.LblReprint.Enabled = False
            Me.LblReprint.Visible = False
            Me.LblShowAll.Enabled = False

            
            If varAction.Contains("TVS") Then
                strDK_AddInvNo = " and trx_TC like '%4%TVS%'"

            Else
                strDK_AddInvNo = "  and App='RAS' and fstUser in (select Fstuser from dutoan_Tour " _
                    & " UNION SELECT [SICode] from[tblUser]  where status<>'xx' and counter='N-A')"
            End If
            strDK_AddInvNo = strDK_AddInvNo & " and ShortName in (Select ShortName from [Vendor]" _
                & " where Status='OK' and HoaDon<>'N/A')"
            LoadGridUNC(" and invNo=''")

            With GridUNC

                If varAction = "ADDINVNO_TVS" Then
                    .Columns("RecID").Visible = False
                    .Columns("FOP").Visible = False
                    .Columns("RMKNoibo").Visible = False
                    .Columns("AccountNumber").Visible = False
                    .Columns("BankName").Visible = False
                    .Columns("BankAddress").Visible = False
                    '.Columns("Swift").Visible = False
                    '.Columns("App").Visible = False

                ElseIf varAction = "ADDINVNO_N_A" Then
                    .Columns("Curr").DisplayIndex = 15
                    .Columns("OnBehalf").Visible = False
                End If

            End With
        End If
            If myStaff.City = "SGN" Then
            PayerAccouID_frm = 1140
            PayerAccouID_To = 1145
        Else
            PayerAccouID_frm = 4182
            PayerAccouID_To = 4187
        End If
        mstrFirstLoadCompleted = True
    End Sub
    Private Sub LoadGridUNC(strDKFilter As String)
        Dim strSQL As String
        Me.LblUpdateInvNo.Visible = False
        Me.LblDeleteUNC.Visible = False
        strSQL = "select RecID, Curr, Amount,FOP, InvNo, RMKNoibo, RefNo, ShortName as Beneficiary, AccountName, SCBNo,App"
        '        strSQL = strSQL & " AccountNumber, BankName, BankAddress, Charge, Description,Swift, FstUpdate, App, TRX_TC"
        strSQL = strSQL & ",AccountNumber, BankName, BankAddress, PayerACcountID, Description,FstUpdate, TRX_TC,OnBehalf,PayerAccountId" _
                & " from UNC_payments "
        strSQL = strSQL & " where amount>0 And status Not in ('XX','QQ') and Lstupdate between '" & Me.txtFrm.Value.Date & "' and '" & Me.TxtThru.Value.Date & " 23:59'"
        If Me.CmbTVC.Text <> "" Then
            strSQL = strSQL & " and payerAccountID in (select intVal from Misc where Cat='TvPayer' and Val='" & Me.CmbTVC.Text & "')"
            If Me.CmbAcc.Text <> "" Then
                Dim arrAccBreak As String() = Split(CmbAcc.Text, "-")
                strSQL = strSQL & " and payerAccountID in (select RecId from Vendor where Bank='" _
                    & arrAccBreak(0) & "' and Cur='" & arrAccBreak(1) & "')"
            End If
        End If
        strSQL = strSQL & strDKFilter & strDK_AddInvNo
        Me.GridUNC.DataSource = GetDataTable(strSQL)
        Me.GridUNC.Columns("RecId").Visible = False
        Me.GridUNC.Columns("App").Visible = False
        Me.GridUNC.Columns("PayerAccountID").Visible = False
        Me.GridUNC.Columns("Curr").Width = 35
        Me.GridUNC.Columns("FOP").Width = 35
        Me.GridUNC.Columns("SCBNo").Width = 35
        Me.GridUNC.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridUNC.Columns("Amount").DefaultCellStyle.Format = "#,##0.00"
        Me.GridUNC.Columns("RefNo").Width = 75
        'Me.GridUNC.Columns("Charge").Width = 40
        Me.LblReprint.Visible = False
        Me.LblEdit.Visible = False
        Me.GridUNC.EditMode = DataGridViewEditMode.EditProgrammatically
    End Sub


    Private Sub CmbCAT_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCAT.SelectedIndexChanged
        If varAction = "ACCOUNT" Then
            LoadCmbCompany()
        ElseIf varAction = "CANTEXT" Then
            LoadGridCanText()
        End If
    End Sub
    Private Sub LoadGridCanText()
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim strSQL As String
        On Error GoTo errHandler
        strSQL = "select RecID, CannedText from UNC_CannedText where cat='" & Me.CmbCAT.Text.Substring(0, 2) & "'"
        Me.GridCanText.DataSource = GetDataTable(strSQL)
        Me.GridCanText.Columns(0).Visible = False
        Me.GridCanText.Columns(1).Width = 383
        Me.LblDeleteCanText.Visible = False
errHandler:
        On Error GoTo 0
    End Sub

    Private Sub LblDeleteCanText_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDeleteCanText.LinkClicked
        Dim strSQL As String
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        strSQL = " delete from unc_CannedText where recid=" & Me.GridCanText.CurrentRow.Cells("recid").Value
        cmd.CommandText = strSQL
        cmd.ExecuteNonQuery()
        LoadGridCanText()
    End Sub

    Private Sub LblAddCanText_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblAddCanText.LinkClicked
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        If Me.TxtCanText.Text = "" Then Exit Sub
        cmd.CommandText = "insert MISC (cat, VAL1, VAL) values ('CANTXT','" & Me.CmbCAT.Text.Substring(0, 2) & _
            "',N'" & Me.TxtCanText.Text & "')"
        On Error GoTo errHandler
        cmd.ExecuteNonQuery()
        LoadGridCanText()
        On Error GoTo 0
        Me.TxtCanText.Text = ""
        Exit Sub
errHandler:
        On Error GoTo 0
        MsgBox("Invalid Input.", MsgBoxStyle.Critical, msgTitle)
    End Sub

    Private Sub GridCanText_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridCanText.CellContentClick
        Me.LblDeleteCanText.Visible = True
    End Sub

    Private Sub LblDeleteAcct_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDeleteAcct.LinkClicked
        Dim strSQL As String
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        strSQL = "update Vendor set status='XX', lstuser='" & myStaff.SICode & "',"
        strSQL = strSQL & " lstupdate=getdate() where recid=" & Me.GridAcct.CurrentRow.Cells("recid").Value
        cmd.CommandText = strSQL
        cmd.ExecuteNonQuery()
        LoadGridAcc()
    End Sub
    Private Sub LoadGridAcc()
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand

        Dim strSQL As String
        On Error GoTo errHandler
        strSQL = "select RecID, AccountName, AccountNumber,Address, BankName, BankAddress, Swift, BankInVietnam,Country from Vendor "
        strSQL = strSQL & " where status ='OK' and CompanyID=" & Me.CmbCompany.SelectedValue
        
        Me.GridAcct.DataSource = GetDataTable(strSQL)
        Me.GridAcct.Columns(0).Visible = False
        Me.GridAcct.Columns("AccountName").Width = 256
        Me.GridAcct.Columns("AccountNumber").Width = 128
        Me.GridAcct.Columns("BankName").Width = 256
        Me.GridAcct.Columns("BankAddress").Width = 128
        Me.LblDeleteAcct.Visible = False
errHandler:
        On Error GoTo 0
    End Sub
    Private Sub LoadCmbCompany()
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim strSQL As String
        On Error GoTo errHandler
        strSQL = "select RecID as VAL, ShortName as DIS from Vendor where cat='" & Me.CmbCAT.Text.Substring(0, 2) & "'"
        strSQL = strSQL & "and status ='OK'"
        LoadCmb_VAL(Me.CmbCompany, strSQL)
errHandler:
        On Error GoTo 0
    End Sub
    Private Sub GenCmbValue()
        LoadCmb_MSC(Me.CmbCurr, "Curr")
    End Sub
    Private Sub CmbCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCompany.SelectedIndexChanged
        LoadGridAcc()
    End Sub

    Private Sub LblAddAcct_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblAddAcct.LinkClicked
        Dim strSQL As String
        Dim tblExistingAccounts As DataTable
        If Me.TxtAccountName.Text = "" Or Me.TxtAccNo.Text = "" Or _
           Me.TxtBank.Text = "" Or Me.TxtCity.Text = "" Then
            MsgBox("Invalid Input!", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        If ContainSpecialChar4Citi(TxtBank.Text) Then
            MsgBox("Bank Name must NOT contain special characters!", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        tblExistingAccounts = GetDataTable("Select * from Vendor Where Status='OK'" _
                                           & " and AccountName='" & TxtAccountName.Text & "'", Conn)
        If tblExistingAccounts.Rows.Count > 0 _
            AndAlso MsgBox("Identical Account Name exist! Stop Adding?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Exit Sub
        End If
        strSQL = "insert into Vendor (CompanyID, AccountName, AccountNumber,Address, BankName" _
            & ", BankAddress, Swift,BankInVietnam,Country, FstUser) values ('"
        strSQL = strSQL & Me.CmbCompany.SelectedValue & "',N'"
        strSQL = strSQL & Me.TxtAccountName.Text & "','"
        strSQL = strSQL & Me.CmbCurr.Text & ": " & Me.TxtAccNo.Text & "',N'" & txtAddress.Text & "',N'"
        strSQL = strSQL & Me.TxtBank.Text & "',N'"
        strSQL = strSQL & Me.TxtCity.Text & "','"
        strSQL = strSQL & Me.TxtSwift.Text & "','" & cboBankInVietnam.Text _
            & "','" & txtCountry.Text & "','" & myStaff.SICode & "')"

        ExecuteNonQuerry(strSQL, Conn)

        Me.TxtAccountName.Text = ""
        Me.TxtBank.Text = ""
        Me.TxtCity.Text = ""
        Me.TxtSwift.Text = ""
        cboBankInVietnam.SelectedIndex = 0
        LoadGridAcc()
    End Sub
    Private Sub GridAcct_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridAcct.CellContentClick
        Me.LblDeleteAcct.Visible = True
        Me.TxtAccountName.Text = Me.GridAcct.CurrentRow.Cells("AccountName").Value
        Me.txtAddress.Text = Me.GridAcct.CurrentRow.Cells("Address").Value
        Me.CmbCurr.Text = Me.GridAcct.CurrentRow.Cells("AccountNumber").Value.ToString.Substring(0, 3)
        Me.TxtAccNo.Text = Me.GridAcct.CurrentRow.Cells("AccountNumber").Value.ToString.Substring(4).Trim
        Me.TxtBank.Text = Me.GridAcct.CurrentRow.Cells("BankName").Value
        Me.TxtCity.Text = Me.GridAcct.CurrentRow.Cells("BankAddress").Value
        Me.TxtSwift.Text = Me.GridAcct.CurrentRow.Cells("swift").Value
        txtCountry.Text = Me.GridAcct.CurrentRow.Cells("Country").Value
    End Sub

    Private Sub GridUNC_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles _
        GridUNC.CellContentClick, GridUNC.CellClick
        fName = Me.GridUNC.Columns(e.ColumnIndex).Name
        If varAction = "EDIT" Then
            Me.LblReprint.Visible = True
            Me.LblEdit.Visible = True
        End If
        Me.LblUpdateInvNo.Visible = True
        Me.GridUNC.EditMode = DataGridViewEditMode.EditProgrammatically
        Dim strSQL As String
        Me.TxtInvNo.Text = Me.GridUNC.CurrentRow.Cells("InvNo").Value
        strSQL = "select VAL from misc where cat='UNC' and left(val,3) in (select substring(Bank,1,3) from Vendor where recid="
        strSQL = strSQL & Me.GridUNC.CurrentRow.Cells("PayerAccountID").Value & ")"
        LoadCmb_MSC(Me.CmbTemplate, strSQL)
        Me.LblDeleteUNC.Visible = True
        If myStaff.City = "SGN" Then
            LoadGridInvTcode(GridUNC.CurrentRow.Cells("RecId").Value)
            lblLstUpdateInvNbr.Text = "Last Update InvNbr:" & ScalarToString("ActionLog", "ActionDate" _
                            , "TableName ='unc_pmt' and doWhat='addinvno' and f6=" & GridUNC.CurrentRow.Cells("RecId").Value)
        End If

    End Sub

    Private Sub GridUNC_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridUNC.CellEndEdit
        Me.LblSave.Visible = True
    End Sub

    Private Sub LblSave_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblSave.LinkClicked
        Me.LblSave.Visible = False
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim strSQL As String, MyAns As Int16, LyDo As String
        MyAns = MsgBox("Are You Sure To Save Changes To Current Record?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle)
        If MyAns = vbNo Then Exit Sub
        LyDo = InputBox("Enter the Reason for Editing", msgTitle)
        If LyDo = "" Then Exit Sub
        strSQL = "update UNC_Payments set "
        strSQL = strSQL & " InvNo= '" & Me.GridUNC.CurrentRow.Cells("InvNo").Value & "'"
        strSQL = strSQL & ", RMK=RMK+'|" & LyDo & "'"
        strSQL = strSQL & ", amount= " & Me.GridUNC.CurrentRow.Cells("Amount").Value
        strSQL = strSQL & ", Curr= '" & Me.GridUNC.CurrentRow.Cells("Curr").Value & "'"
        strSQL = strSQL & ", Description = N'" & Me.GridUNC.CurrentRow.Cells("Description").Value & "'"
        'strSQL = strSQL & ", Charge= '" & Me.GridUNC.CurrentRow.Cells("Charge").Value & "'"
        strSQL = strSQL & " where RecID=" & Me.GridUNC.CurrentRow.Cells("RecID").Value
        cmd.CommandText = strSQL
        cmd.ExecuteNonQuery()
        strSQL = UpdateLogFile("UNC_PMT", varAction, Me.GridUNC.CurrentRow.Cells("InvNo").Value,
            Me.GridUNC.CurrentRow.Cells("AMount").Value, Me.GridUNC.CurrentRow.Cells("Curr").Value,
            Me.GridUNC.CurrentRow.Cells("Description").Value, "", Me.GridUNC.CurrentRow.Cells("RecID").Value)
        'Me.GridUNC.CurrentRow.Cells("Description").Value, Me.GridUNC.CurrentRow.Cells("Charge").Value, Me.GridUNC.CurrentRow.Cells("RecID").Value)
        cmd.CommandText = strSQL
        cmd.ExecuteNonQuery()
        LoadGridUNC("")
    End Sub

    Private Sub LblEdit_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblEdit.LinkClicked
        Me.LblEdit.Visible = False
        Me.GridUNC.EditMode = DataGridViewEditMode.EditOnEnter
    End Sub

    Private Sub LblFind_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblFind.LinkClicked
        Dim StrDK As String = ""
        If fName <> "" And Me.txtSearch.Text <> "" Then
            If fName = "Beneficiary" Then fName = "shortName"
            StrDK = " and " & fName & " like '%" & Me.txtSearch.Text & "%'"
        End If
        LoadGridUNC(StrDK)
    End Sub

    Private Sub CmbTVC_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbTVC.SelectedIndexChanged
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim strSQL As String = "select distinct Bank + '-' + Cur as ACN from Vendor where RecId in " _
            & "(select intVal from Misc where Cat='TvPayer' and Val='" & Me.CmbTVC.Text & "')"
        Me.CmbAcc.DataSource = GetDataTable(strSQL)
        Me.CmbAcc.DisplayMember = "ACN"
    End Sub

    Private Sub LblReprint_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblReprint.LinkClicked
        Dim intPayerAccountId As Integer = ScalarToInt("UNC_payments", "PayerAccountID", "RecID=" & GridUNC.CurrentRow.Cells("recID").Value)

        If Me.GridUNC.CurrentRow.Cells("SCBNo").Value <> "" Or Me.GridUNC.CurrentRow.Cells("Amount").Value = 0 Then Exit Sub
        If Me.CmbTemplate.Text.Contains("SCB") And _
            ScalarToInt("UNC_payments", "PayerAccountID", "RecID=" & Me.GridUNC.CurrentRow.Cells("recID").Value) > PayerAccouID_frm And _
            ScalarToInt("UNC_payments", "PayerAccountID", "RecID=" & Me.GridUNC.CurrentRow.Cells("recID").Value) < PayerAccouID_To Then
            InHoaDon(Application.StartupPath, Me.CmbTemplate.Text, "V", Me.GridUNC.CurrentRow.Cells("recID").Value, Now.Date, Now.Date, 0, "REPRINT", MySession.Domain)
            If MsgBox("Wanna Print it Out?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle) = vbNo Then Exit Sub
            InHoaDon(Application.StartupPath, Me.CmbTemplate.Text, "O", Me.GridUNC.CurrentRow.Cells("recID").Value, Now.Date, Now.Date, 0, "REPRINT", MySession.Domain)
            Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
            cmd.CommandText = "update UNC_payments Set HasPrinted=1 where recid=" & Me.GridUNC.CurrentRow.Cells("recID").Value
            cmd.ExecuteNonQuery()
        Else
            InHoaDon(Application.StartupPath, Me.CmbTemplate.Text, "O", Me.GridUNC.CurrentRow.Cells("recID").Value, Now.Date, Now.Date, 0, "REPRINT", MySession.Domain)
            Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
            cmd.CommandText = "update UNC_payments Set HasPrinted=1 where recid=" & Me.GridUNC.CurrentRow.Cells("recID").Value
            cmd.ExecuteNonQuery()
        End If
        Dim strBankName As String = ScalarToString("Vendor", "BankName", "RecID=" & intPayerAccountId)
        If strBankName.Contains("CITIBANK") Or strBankName.Contains("Standard Chartered Bank") Then
            CreateSms4UNC(GridUNC.CurrentRow.Cells("recID").Value, strBankName, False)
        End If
    End Sub

    Private Sub LblShowAll_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblShowAll.LinkClicked
        LoadGridUNC("")
    End Sub
    Private Sub ChkNoInv_Click(sender As Object, e As EventArgs) Handles ChkNoInv.Click
        Dim strDK As String = ""
        If Me.ChkNoInv.Checked Then
            strDK = " And invNo=''"
        Else
            strDK = " and invNo<>'' and invno <>'N/A'"
        End If
        LoadGridUNC(strDK)
    End Sub

    Private Sub LblUpdateInvNo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblUpdateInvNo.LinkClicked
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim strSQL As String, MyAns As Int16
        MyAns = MsgBox("Are You Sure INV No is Correct?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle)
        If MyAns = vbNo Or Me.TxtInvNo.Text = "" Then Exit Sub
        strSQL = "update UNC_Payments set InvNo= '" & Me.TxtInvNo.Text & "'"
        strSQL = strSQL & " where RecID=" & Me.GridUNC.CurrentRow.Cells("RecID").Value
        strSQL = strSQL & "; " & UpdateLogFile("UNC_PMT", varAction.Substring(0, 3) & "INVNO", Me.TxtInvNo.Text, _
            "", "", "", "", Me.GridUNC.CurrentRow.Cells("RecID").Value)
        cmd.CommandText = strSQL
        cmd.ExecuteNonQuery()
        LoadGridUNC("")
    End Sub

    Private Sub lbkUpdateAddr_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUpdateAddr.LinkClicked
        Dim strSql As String
        Select Case CmbCAT.Text
            Case "TV - TransViet"
                strSql = "update Vendor set Address='" & txtAddress.Text _
                    & "', lstupdate=getdate() where recid=" & Me.GridAcct.CurrentRow.Cells("recid").Value
            Case Else
                strSql = "update Vendor set Address='" & txtAddress.Text _
                    & "', lstuser='" & myStaff.SICode & "'," _
                    & " lstupdate=getdate() where recid=" & Me.GridAcct.CurrentRow.Cells("recid").Value
        End Select

        If ExecuteNonQuerry(strSql, Conn) Then
            LoadGridAcc()
        End If
    End Sub

    Private Sub GridUNC_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridUNC.CellContentDoubleClick
        If Me.GridUNC.Columns(e.ColumnIndex).Name <> "TRX_TC" Then Exit Sub
        Dim TCode As String = Me.GridUNC.CurrentCell.Value.ToString.Substring(4)
        Dim Detail2Show As String = "", TC As String
        Dim VendorID As Integer = ScalarToInt("Vendor", "RecID", "accountnumber='" & Me.GridUNC.CurrentRow.Cells("accountnumber").Value & "'")
        Dim DuToanID As Integer
        Dim tbDetail As DataTable
        For i As Int16 = 0 To TCode.Split("_").Length - 1
            TC = TCode.Split("_")(i)
            DuToanID = ScalarToInt("dutoan_tour", "RecID", "Tcode='" & TC & "'")
            tbDetail = GetDataTable("select Brief from dutoan_item where status<>'XX' and VendorID=" & VendorID & " and DutoanID =" & DuToanID)
            For j As Int16 = 0 To tbDetail.Rows.Count - 1
                Detail2Show = Detail2Show & vbCrLf & TC & ": " & tbDetail.Rows(j)("Brief")
            Next
        Next
        MsgBox(Detail2Show, MsgBoxStyle.Information, msgTitle)
    End Sub

    Private Sub LblDeleteUNC_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblDeleteUNC.LinkClicked
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim DaTra As Decimal
        cmd.CommandText = ChangeStatus_ByID("UNC_payments", "XX", Me.GridUNC.CurrentRow.Cells("RecID").Value)
        If Me.GridUNC.CurrentRow.Cells("App").Value = "RAS" Then
            cmd.CommandText = cmd.CommandText & ";" & ChangeStatus_ByDK("dutoan_pmt", "XX", "PmtID=" & Me.GridUNC.CurrentRow.Cells("RecID").Value)
            cmd.ExecuteNonQuery()
        Else
            cmd.CommandText = cmd.CommandText & "; delete from dieuhanh.dbo.tbl_FOP where RecIDDNTT =" & Me.GridUNC.CurrentRow.Cells("RecID").Value
            cmd.ExecuteNonQuery()
            Exit Sub
        End If

        Dim tbPmt As DataTable = GetDataTable("select ItemID from dutoan_Pmt where pmtID=" & Me.GridUNC.CurrentRow.Cells("RecID").Value)
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
        LoadGridUNC("")
    End Sub
    Private Function LoadGridInvTcode(intUncId As Integer) As Boolean
        Dim strQuerry As String = "select tcode,sum(VND) as TcodeAmount, InvId,InvoiceNo" _
            & ", i.Amount as InvAmount" _
            & " from DuToan_Pmt p" _
            & " left join Misc m on p.DutoanId=m.IntVal1 and m.CAT='InvTcodeMap' and m.Status='ok'" _
            & " left join VendorInvoice i on i.InvId=m.IntVal and i.VendorID=p.VendorID" _
            & " where pmtid=" & intUncId _
            & " group by tcode,InvId,InvoiceNo, i.Amount"
        LoadDataGridView(dgrInvTcode, strQuerry, Conn)
        Try
            dgrInvTcode.Columns("TcodeAmount").DefaultCellStyle.Format = "#,###"
            dgrInvTcode.Columns("InvAmount").DefaultCellStyle.Format = "#,###"
        Catch ex As Exception

        End Try

        Return True
    End Function
End Class