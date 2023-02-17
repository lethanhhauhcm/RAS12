Imports RAS12.MySharedFunctions
Imports System.Diagnostics
Imports RAS12.MySharedFunctionsWzConn
Imports System.IO
Imports System.Data.SqlClient
Imports System.Object

Public Class Costing
    Private MyCust As New objCustomer
    Private CharEntered As Boolean = False
    Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
    Private D1Visible As String = "Accommodations_Transfer_Insurance"
    Private SVC_WzDetail As String = D1Visible & "_Meal_"
    Private QrySupplier As String = "select RecID as VAL, Shortname as DIS from Vendor where status='OK' and cat not in ('AL','AP','TV')"
    Private FirstClickQuote As Boolean = False, OverPay As Date = "01-Jan-2000"
    Private mblnFirstLoadCompleted As Boolean
    Private mintSelectedCrdRow As Integer
    Private mstrSelectedService As String
    Private mdteSelectedSdate As Date
    Private mintNbrOfPax As Integer
    Private mobjSvcRow As DataGridViewRow
    Private mblnNonVndCost As Boolean
    Private mdecMaxVendorAmt As Decimal
    Private mblnLoadDNTTcompleted As Boolean

    Private Structure BillBy
        Public Shared BillByBundle As String = "Bundle"
        Public Shared BillByCOD As String = "COD"
        Public Shared BillByEvent As String = "Event"
        Public Shared BillByPeriod As String = "Period"
        Public Shared BillByCRD As String = "CRD"
    End Structure
    Private Structure SVCType
        Public Shared Accommodations As String = "Accommodations"
        Public Shared Transfer As String = "Transfer"
        Public Shared Insurance As String = "Insurance"
        Public Shared Meal As String = "Meal"
    End Structure
    Private Sub LoadgridTour()
        'custid
        Dim strSQL As String = "select t.RecID, TCode, Pax, SDate, EDate, t.FstUser, Brief, Contact" _
                                & ", Email, BillingBy, QuoteValid, DLCfm, Traveller, t.CustID, t.Status" _
                                & ", t.FstUpdate, t.LstUser, t.LstUpdate, KeyMap, Owner, PRNO, IONo, RefNo" _
                                & ", BUAdmin, Location, t.RMK, Dept, RequiredData" _
                                & ", FileId, WindowId, QuotationFile, WzAir, RcpId" _
                                & ", c.VAL as Channel, CustShortName, VatPct, ActStatus,RejectReason" _
                                & ",WebId" _
                                & " from DuToan_Tour t" _
                                & " left join [Cust_Detail] c on t.CustId=c.CustId" _
                                & " where c.Status='OK' and c.Cat='Channel' "

        txtSearchValue.Text = txtSearchValue.Text.Trim

        Select Case cboSearchBy.Text
            Case "TCODE"
                strSQL = strSQL & "and TCode like '%" & Me.txtSearchValue.Text.Trim & "%'"
            Case "EventCode"
                strSQL = strSQL & "and RefNo like '%" & Me.txtSearchValue.Text.Trim & "%'"
            Case "PR Number"
                strSQL = strSQL & "and PRNO like '%" & Me.txtSearchValue.Text.Trim & "%'"
        End Select


        Select Case cboChannel.Text
            Case "CWT"
                strSQL = strSQL & " and c.VAL='CS'"
            Case "LCL"
                strSQL = strSQL & " and c.VAL='LC'"
        End Select


        If cboCustShortName.Text <> "" Then
            strSQL = strSQL & " and CustShortName= '" & cboCustShortName.Text & "'"
        End If
        If Me.cmbStatus.Text = "CXLD" Then
            strSQL = strSQL & " and t.Status='XX'"
        ElseIf Me.cmbStatus.Text = "Finalized" Then
            strSQL = strSQL & " and t.Status='RR'"
        ElseIf Me.cmbStatus.Text = "Pending" Then
            strSQL = strSQL & " and t.Status like 'O%'"
        Else
            strSQL = strSQL & " and t.Status='O" & Me.cmbStatus.Text.Substring(0, 1) & "'"
        End If

        AddEqualConditionCombo(strSQL, cboActStatus, "t.ActStatus")

        'For j As Int16 = 0 To Me.LstCCenter.Items.Count - 1
        '    Me.LstCCenter.SetItemChecked(j, False)
        'Next
        If Me.ChkSelectedCustOnly.Checked Then strSQL = strSQL & " and t.custID=" & Me.CmbCust.SelectedValue
        If Me.ChkPastOnly.Checked Then strSQL = strSQL & " and EDate <getdate()"
        Me.GridTour.DataSource = GetDataTable(strSQL)
        Me.GridTour.Columns("RecID").Visible = False
        Me.GridTour.Columns("CustID").Visible = False
        Me.GridTour.Columns("Pax").Width = 32
        Me.LblDeleteTour.Visible = False
        Me.LckLblFinalize.Visible = False
        LblOrderBV.Visible = False
        Me.LckLblUndoFinalize.Visible = False
        Me.LblDocSent.Visible = False
        'Me.LblPreview.Visible = False
        Me.LblQuote.Visible = False
        Me.LblSettlement.Visible = False
        Me.LblSvcCfm.Visible = False
        Me.GridTour.Columns("Tcode").Width = 180
        Me.GridTour.Columns("SDate").Width = 64
        Me.GridTour.Columns("EDate").Width = 64
        Me.GridTour.Columns("FstUser").Width = 30
        Me.LblSaveTour.Visible = False
        If InStr(Me.cmbStatus.Text, "-") > 0 Or Me.cmbStatus.Text = "Pending" Then
            Dim Back3 As Date = Now.Date.AddDays(-3)
            Dim Back7 As Date = Now.Date.AddDays(-7)
            Dim Back10 As Date = Now.Date.AddDays(-10)
            For r As Int16 = 0 To Me.GridTour.RowCount - 1
                If Me.GridTour.Item("Status", r).Value = "OC" Then Me.GridTour.Rows(r).DefaultCellStyle.ForeColor = Color.DarkGray
                If Me.GridTour.Item("Status", r).Value = "OD" Then Me.GridTour.Rows(r).DefaultCellStyle.ForeColor = Color.Blue
                If Me.GridTour.Item("BillingBy", r).Value.ToString.Substring(0, 1) = "P" AndAlso Me.GridTour.Item("Edate", r).Value < Back3 Then
                    Me.GridTour.Rows(r).DefaultCellStyle.ForeColor = Color.Red
                ElseIf Me.GridTour.Item("BillingBy", r).Value.ToString.Substring(0, 1) = "E" AndAlso Me.GridTour.Item("Edate", r).Value < Back10 Then
                    Me.GridTour.Rows(r).DefaultCellStyle.ForeColor = Color.Red
                ElseIf Me.GridTour.Item("BillingBy", r).Value.ToString.Substring(0, 1) = "E" AndAlso Me.GridTour.Item("Edate", r).Value < Back7 Then
                    Me.GridTour.Rows(r).DefaultCellStyle.ForeColor = Color.DarkRed
                End If
            Next
        End If
    End Sub

    Private Sub Costing_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Private Sub Costing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If HasNewerVersion_R12(Application.ProductVersion) Or SysDateIsWrong(Conn) Then
            Me.Close()
            Me.Dispose()
            End
        End If
        Me.cmbStatus.Text = Me.cmbStatus.Items(0).ToString
        Me.CmbCrd.Text = "??"
        'CheckRightForALLForm(Me)
        MyCust.GenCustList()
        LoadCmb_VAL(Me.CmbCountry, "select Country as VAL, CountryName as DIS from Country order by DIS")
        LoadCmb_VAL(Me.CmbCust, MyCust.List_LC)
        LoadCmb_MSC(Me.CmbCurrItem, "CURR")


        LoadCmb_MSC(Me.CmbService, "DT_DVU")
        LoadCmb_MSC(Me.CmbPmtRQSVC, "DT_DVU")
        LoadgridTour()
        LoadCmb_VAL(Me.CmbVendor, "select RecID as VAL, Shortname as DIS from Vendor where status='OK'")


        Me.CmbCurrItem.Text = "VND"
        If InStr("HDI_TKL", myStaff.SICode) > 0 Then
            DisableAllLinkLabel(Me)
        End If
        Reset()
        cboSearchBy.SelectedIndex = 0
        cboActStatus.SelectedIndex = -1

        mblnFirstLoadCompleted = True
    End Sub
    Private Sub Reset()
        cboChannel.SelectedIndex = 0
        cboCustShortName.SelectedIndex = -1
        cmbStatus.SelectedIndex = 3
        txtSearchValue.Text = ""
    End Sub
    Private Sub DisableAllLinkLabel(ByVal pRoot As Control)
        For Each Ctrl As Control In pRoot.Controls
            If Ctrl.Controls.Count > 0 Then
                DisableAllLinkLabel(Ctrl)
            ElseIf TypeOf Ctrl Is LinkLabel Then
                Ctrl.Enabled = False
                Ctrl.Visible = False
            End If
        Next
    End Sub
    Private Sub TxtPax_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPax.KeyDown, TxtHTLSF.KeyDown,
        TxtQty.KeyDown, TxtUnitCost.KeyDown, TxtVAT.KeyDown, TxtTvSfPct.KeyDown, txtMU.KeyDown, TxtTVSfAmount.KeyDown, TxtAmtAdj.KeyDown
        CharEntered = checkCharEntered(e.KeyValue)
    End Sub
    Private Sub TxtPax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtPax.KeyPress, TxtHTLSF.KeyPress,
        TxtQty.KeyPress, TxtUnitCost.KeyPress, TxtVAT.KeyPress, TxtTvSfPct.KeyPress, txtMU.KeyPress, TxtTVSfAmount.KeyPress, TxtAmtAdj.KeyPress
        If CharEntered Then
            e.Handled = True
        End If
    End Sub
    Private Function DuplicateEventCode(pEventCode As String _
                                        , Optional strService As String = "") As Boolean
        If Not MyCust.ShortName.Contains("SANOFI") Then Return False
        If pEventCode = "TBA" Then Return False
        Dim tblTour As New System.Data.DataTable

        If pEventCode = "" Or pEventCode.ToUpper = "_EVENT CODE" Then
            Return True
        ElseIf strService = "" Then
            tblTour = GetDataTable("Select Tcode from Dutoan_Tour where RefNo='" & pEventCode.ToUpper.Trim _
                                         & "' and status<>'XX' and tcode <>'" &
                                         txtTcode.Text & "' and CustShortName='" & MyCust.ShortName & "'")
        Else
            tblTour = GetDataTable(" Select Tcode from Dutoan_Tour t where RefNo='" & pEventCode.ToUpper.Trim _
                                         & "' and t.status<>'XX' and CustShortName='" & MyCust.ShortName _
                                         & "' and (select count (*) from dutoan_item i where i.dutoanid=t.recid" _
                                         & " and i.status<>'xx' and Service='" & strService & "')>0")
        End If
        If tblTour.Rows.Count > 1 Then
            Dim frmShow As New frmShowTableContent(tblTour, "Tcode")
            frmShow.ShowDialog()
            Return True
        Else
            Return False
        End If

    End Function
    'Private Function defineKeymapFromCCenter() As String
    '    Dim KQ As String = ""
    '    For i As Int16 = 0 To Me.LstCCenter.Items.Count - 1
    '        If Me.LstCCenter.GetItemChecked(i) Then
    '            KQ = KQ & "|" & Me.LstCCenter.Items(i).ToString
    '        End If
    '    Next
    '    If KQ.Length > 2 Then KQ = KQ.Substring(1)
    '    Return KQ
    'End Function

    Private Function CheckVat(decVatPct As Decimal) As Boolean
        Select Case decVatPct
            Case 10, 5
                If CreateFromDate(TxtStartDate.Value) <= "01 Jan 23" Then
                    If MsgBox("VAT 10 % is Correct?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
                        Return False
                    End If
                End If

            Case 8
                If CreateFromDate(TxtStartDate.Value) <= "01 Feb 22" Or CreateFromDate(TxtStartDate.Value) >= "31 Dec 22 23:59" Then
                    If MsgBox("VAT 8 % is Correct?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
                        Return False
                    End If
                End If

            Case 7
                    If Now.Date >= "30 Nov 21 23:59" Then
                    If MsgBox("VAT 7 % is Correct?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
                        Return False
                    End If
                End If
            Case 0
                Return True
            Case Else
                MsgBox("You must specify VAT Pct!")
                Return False
        End Select
        Return True
    End Function
    Private Sub LblCreate_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblCreate.LinkClicked
        Dim txtBodyAck As String = ScalarToString("MISC", "Details", "cat='EMLACK' and val='NonAir'")
        'Dim strKeyMap As String = defineKeymapFromCCenter()


        If Not CheckFormatTextBox(txtVatPct, True, 1, 2) Then
            Exit Sub
        End If
        If ScalarToInt("Cust_Detail", "RecId", "CAT='TVC' and Status='OK' and CustId=" & MyCust.CustID) = 0 Then
            MsgBox("Please ask Accounting to set TVC for this customer first. Action Aborted", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        If CmbCust.Text = "NVS_ MICE" Or CmbCust.Text = "SDZ_MICE" Then
            If txtRefCode.Text <> "" AndAlso txtRefCode.Text <> "NA" Then
                Dim strDupTCode As String = ScalarToString("Dutoan_Tour", "Tcode", "Status<>'XX' and RefNo='" & txtRefCode.Text & "'")
                If strDupTCode <> "" Then
                    MsgBox("Duplicate Event Code for " & strDupTCode)
                    Exit Sub
                End If
            End If
        End If

        If MsgBox("Travel request includes AIR?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            chkWzAir.Checked = True
        Else
            chkWzAir.Checked = False
        End If

        GenTourCode(Me.TxtStartDate.Value, Me.CmbCust.Text, "")

        Dim txtSubj As String = "Acknowledgement-" & Me.TxtBrief.Text, txtCC As String = "cwtbos.sgn@transviet.com"
        txtBodyAck = String.Format("Dear {0}, %0d{1} %0dFor further correspondence on this request, please quote", txtBooker.Text, txtBodyAck.Trim)
        If CInt(Me.TxtPax.Text) = 0 Or Me.TxtBrief.Text = "" Or Me.CmbBilling.Text = "" Or
                Me.TxtStartDate.Value.Date > Me.txtEndDate.Value.Date Or
                (Me.OptCWT.Checked And Me.cmbLocation.Text = "") Or
                (Me.CmbCust.Text.Contains("SANOFI") And DuplicateEventCode(Me.txtRefCode.Text)) Then
            MsgBox("Invalid NoOfPax or Brief or Billing or StartDate or PRNo or Empty Location for CWT client", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        If txtBooker.Text.ToUpper.Contains("ZPERS") And InStr("COD_CRD", Me.CmbBilling.Text) = 0 Then
            MsgBox("Illogic Booker and Billing Method", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        If txtBooker.Text = "" Then
            MsgBox("Invalid Booker!")
            Exit Sub
        End If
        If Strings.Left(Me.TxtEmail.Text, 1) = "_" Then Me.TxtEmail.Text = ""
        If Strings.Left(Me.TxtPRNo.Text, 1) = "_" Then Me.TxtPRNo.Text = ""
        If Strings.Left(Me.txtRefCode.Text, 1) = "_" Then Me.txtRefCode.Text = ""
        If Strings.Left(Me.TxtOwner.Text, 1) = "_" Then Me.TxtOwner.Text = ""
        If Strings.Left(Me.TxtIONo.Text, 1) = "_" Then Me.TxtIONo.Text = ""

        If Me.TxtStartDate.Value < Now.Date And myStaff.SupOf = "" Then
            MsgBox("Invalid Start Date Input", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        If Me.GridGO.Visible And InvalidSIR() Then
            MsgBox("Invalid G/O Data", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If


        If OptCWT.Checked _
            AndAlso (cmbTraveler.Text.StartsWith("MR.") Or cmbTraveler.Text.StartsWith("MR ")) Then
            MsgBox("Invalid Traveller Name", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        If CmbCust.Text.Contains("SANOFI") AndAlso (txtWindowId.Text = "") Then
            MsgBox("Must input W Id for Sanofi!")
            Exit Sub
        ElseIf txtBooker.Text <> "ZPERSONAL" Then
            If IsInCustGrp("UNIQUE PLAN CODE", CmbCust.Text) Then
                If Not CheckPlanCode4Abbott(txtRefCode.Text, 0) Then
                    Exit Sub
                End If
            ElseIf IsInCustGrp("UNIQUE EP3 CODE", CmbCust.Text) Then
                If Not CheckEp3Code4Novartis(txtRefCode.Text, 0) Then
                    Exit Sub
                End If
            End If
        End If

        GenTourCode(Me.TxtStartDate.Value, Me.CmbCust.Text, Me.CmbBilling.Text.Substring(0, 1))
        txtBodyAck = String.Format("{0} {1} as reference number", txtBodyAck, Me.txtTcode.Text) & "%0d" & "Best Regards"
        Dim t As SqlClient.SqlTransaction = Conn.BeginTransaction, tmpDuToanID As Integer
        cmd.Transaction = t
        Dim location As String = Me.cmbLocation.Text
        If Me.OptCWT.Checked Then location = "N/A"
        Try
            'custid
            cmd.CommandText = "insert DuToan_Tour (Tcode, SDate, CustShortName, CustID, Email, Contact, Brief, Pax, BillingBy, FstUser, " &
                "EDate, Traveller, KeyMap, Owner, PRNO, IONo, RefNo" _
                & ", BUAdmin, Dept, Location, EventDate,WindowId,WzAir,VatPct)" _
                & " values (@Tcode, @SDate, @CustShortName, " &
                "@CustID, @Email, @Contact, @Brief, @Pax, @BillingBy, @FstUser,@EDate, @Traveller, @KeyMap, @Owner, @PRNO,@IONo, @RefNo,@BUAdmin," &
                "@Dept, @Location, @EventDate, @WindowId,@WzAir,@VatPct);SELECT SCOPE_IDENTITY() AS [RecID]"
            cmd.Parameters.Clear()
            cmd.Parameters.Add("@TCode", SqlDbType.VarChar).Value = Me.txtTcode.Text
            cmd.Parameters.Add("@SDate", SqlDbType.DateTime).Value = Me.TxtStartDate.Value.Date
            cmd.Parameters.Add("@CustShortName", SqlDbType.VarChar).Value = Me.CmbCust.Text
            cmd.Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.CmbCust.SelectedValue
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Me.TxtEmail.Text
            cmd.Parameters.Add("@Contact", SqlDbType.VarChar).Value = txtBooker.Text
            cmd.Parameters.Add("@Brief", SqlDbType.NVarChar).Value = Me.TxtBrief.Text
            cmd.Parameters.Add("@Pax", SqlDbType.Int).Value = CInt(Me.TxtPax.Text)
            cmd.Parameters.Add("@BillingBy", SqlDbType.VarChar).Value = Me.CmbBilling.Text
            cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
            cmd.Parameters.Add("@EDate", SqlDbType.DateTime).Value = Me.txtEndDate.Value.Date
            cmd.Parameters.Add("@Traveller", SqlDbType.VarChar).Value = Me.cmbTraveler.Text
            cmd.Parameters.Add("@Keymap", SqlDbType.VarChar).Value = txtCostCenter.Text
            cmd.Parameters.Add("@Owner", SqlDbType.VarChar).Value = Me.TxtOwner.Text
            cmd.Parameters.Add("@PRNO", SqlDbType.VarChar).Value = Me.TxtPRNo.Text
            cmd.Parameters.Add("@IONO", SqlDbType.VarChar).Value = Me.TxtIONo.Text
            cmd.Parameters.Add("@RefNO", SqlDbType.VarChar).Value = Me.txtRefCode.Text
            cmd.Parameters.Add("@BUAdmin", SqlDbType.VarChar).Value = Me.CmbBUAdmin.Text
            cmd.Parameters.Add("@Dept", SqlDbType.VarChar).Value = Me.CmbDept.Text
            cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = location
            cmd.Parameters.Add("@EventDate", SqlDbType.DateTime).Value = Me.txtEventDate.Value.Date
            cmd.Parameters.Add("@WindowId", SqlDbType.VarChar).Value = txtWindowId.Text.Trim
            cmd.Parameters.Add("@WzAir", SqlDbType.Bit).Value = chkWzAir.Checked
            cmd.Parameters.Add("@VatPct", SqlDbType.Int).Value = txtVatPct.Text

            tmpDuToanID = cmd.ExecuteScalar
            t.Commit()

            If Not ExecuteNonQuerry(InsertTcode4AOP(txtTcode.Text, myStaff.City), Conn) Then
                MsgBox("Unable to update Tour code  to AOP. Please report NMK!")
            End If
            Process.Start(String.Format("mailto:{0}?subject={1}&cc={2}&body={3}", Me.TxtEmail.Text, txtSubj, txtCC, txtBodyAck))
            'SendKeys.SendWait("%S")
            If Me.GridGO.Visible Then
                InsertSIR(tmpDuToanID, False)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            t.Rollback()
        End Try
        LoadgridTour()
    End Sub

    Private Sub LoadGridSVC(ByVal pTourID As Integer)
        Dim strDK As String = IIf(Me.ChkXXOnly.Checked, "status ='XX'", "status <>'XX'")
        Me.GridSVC.DataSource = Nothing
        FirstClickQuote = True
        Me.GridSVC.DataSource = GetDataTable("select * from DuToan_Item where  DuToanID=" & pTourID & " and " & strDK)
        'Me.GridSVC.Columns("RecID").Visible = False
        Me.GridSVC.Columns("RecID").Width = 45
        Me.GridSVC.Columns("DuToanID").Visible = False
        Me.GridSVC.Columns("CCurr").Width = 32
        Me.GridSVC.Columns("Unit").Width = 32
        Me.GridSVC.Columns("Q").Width = 25
        Me.GridSVC.Columns("VAT").Width = 56
        Me.GridSVC.Columns("Cost").Width = 70
        Me.GridSVC.Columns("Status").Width = 32
        Me.GridSVC.Columns("Qty").Width = 32
        Me.GridSVC.Columns("VAT").Width = 32
        Me.GridSVC.Columns("PmtMethod").Width = 56
        Me.GridSVC.Columns("isVATincl").Width = 56
        Me.GridSVC.Columns("Svc_status").Width = 64
        Me.GridSVC.Columns("Cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridSVC.Columns("Cost").DefaultCellStyle.Format = "#,##0.0"
        'If Me.ChkXXOnly.Checked Then
        '    Me.GridSVC.Height = 477
        'Else
        '    Me.GridSVC.Height = 226
        'End If
        Me.LblDeleteSVC.Visible = False
        Me.LblQCSF.Visible = False
        Me.LblSaveSvc.Visible = False
        Me.LblAddSF.Visible = False
        Me.GrpCost.Enabled = True
    End Sub

    Private Sub GridSVC_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridSVC.CellContentClick
        RefreshSvc()
    End Sub
    Private Function RefreshSvc() As Boolean
        Dim sBrief As String, LineI As String
        LoadCmb_VAL(Me.CmbVendor, "select RecID as VAL, Shortname as DIS from Vendor where status='OK'")
        Me.CmbService.Text = Me.GridSVC.CurrentRow.Cells("Service").Value
        'CmbService.SelectedIndex = CmbService.FindStringExact(GridSVC.CurrentRow.Cells("Service").Value)
        Me.TxtUnit.Text = Me.GridSVC.CurrentRow.Cells("Unit").Value
        txtPaxName.Text = GridSVC.CurrentRow.Cells("PaxName").Value

        Me.TxtQty.Text = Me.GridSVC.CurrentRow.Cells("Qty").Value
        Me.TxtD1.Value = Me.GridSVC.CurrentRow.Cells("SVCDate").Value
        Me.CmbVendor.Text = Me.GridSVC.CurrentRow.Cells("Vendor").Value
        Me.CmbSupplier.SelectedValue = Me.GridSVC.CurrentRow.Cells("SupplierID").Value
        Me.CmbCurrItem.Text = Me.GridSVC.CurrentRow.Cells("CCurr").Value
        Me.TxtUnitCost.Text = Format(Me.GridSVC.CurrentRow.Cells("Cost").Value, "#,##0.0")
        Me.CmbPmtMethod.Text = Me.GridSVC.CurrentRow.Cells("PmtMethod").Value
        Me.TxtVAT.Text = Me.GridSVC.CurrentRow.Cells("VAT").Value
        If IsDBNull(Me.GridSVC.CurrentRow.Cells("VAT4Vendor").Value) Then
            Me.txtVat4Vendor.Text = 0
        Else
            Me.txtVat4Vendor.Text = Me.GridSVC.CurrentRow.Cells("VAT4Vendor").Value
        End If

        Me.ChkDeposit.Checked = Me.GridSVC.CurrentRow.Cells("NeedDeposit").Value
        Me.TxtMotaCost.Text = Me.GridSVC.CurrentRow.Cells("SupplierRMK").Value
        Me.CmbVendor.Text = Me.GridSVC.CurrentRow.Cells("Vendor").Value
        Me.CmbVendor.SelectedValue = Me.GridSVC.CurrentRow.Cells("VendorID").Value

        If IsDBNull(GridSVC.CurrentRow.Cells("Cost4CWT").Value) Then

            txtCost4CWT.Text = 0
        Else
            txtCost4CWT.Text = GridSVC.CurrentRow.Cells("Cost4CWT").Value
        End If


        If GridSVC.CurrentRow.Cells("ZeroFeeReason").Value = "" Then
            cboZeroFeeReason.SelectedIndex = -1
        Else
            cboZeroFeeReason.SelectedIndex = cboZeroFeeReason.FindStringExact(GridSVC.CurrentRow.Cells("ZeroFeeReason").Value)
        End If
        chkNonCompliance.Checked = GridSVC.CurrentRow.Cells("NonCompliance").Value

        If Me.GridSVC.CurrentRow.Cells("Cost").Value <> 0 Then
            Me.TxtHTLSF.Text = Me.GridSVC.CurrentRow.Cells("MU").Value / Me.GridSVC.CurrentRow.Cells("Cost").Value * 100
        End If

        Me.txtMU.Text = Me.GridSVC.CurrentRow.Cells("MU").Value
        Me.chkBookOnly.Checked = Me.GridSVC.CurrentRow.Cells("BookOnly").Value
        Me.ChkCostOnly.Checked = Me.GridSVC.CurrentRow.Cells("CostOnly").Value
        If Me.GridSVC.CurrentRow.Cells("isVATincl").Value = 0 Then
            Me.OptVATNotIncl.Checked = True
        Else
            Me.OptVATIncl.Checked = True
        End If
        sBrief = Me.GridSVC.CurrentRow.Cells("Brief").Value
        If InStr(SVC_WzDetail, Me.GridSVC.CurrentRow.Cells("Service").Value) > 0 Then
            LineI = sBrief.Split("|")(0)
            Me.cmbStype.Text = LineI.Split("_")(0)
            Me.CmbSCat.Text = LineI.Split("_")(1)
            Me.TxtD1.Text = LineI.Split("_")(2)
            Me.TxtD2.Text = LineI.Split("_")(3)
            '^_^20220725 mark by 7643 -b-
            'Me.TxtT1.Text = LineI.Split("_")(2)
            'Me.TxtT2.Text = LineI.Split("_")(3)
            '^_^20220725 mark by 7643 -e-
            '^_^20220725 modi by 7643 -b-
            If LineI.Split("_")(2).Contains(" ") Then Me.TxtT1.Text = LineI.Split("_")(2)
            If LineI.Split("_")(3).Contains(" ") Then Me.TxtT2.Text = LineI.Split("_")(3)
            '^_^20220725 modi by 7643 -e-
            Me.TxtQty.Text = LineI.Split("_")(4)
            Me.TxtMoTaSVC.Text = sBrief.Split("|")(1)
        Else

            Me.TxtMoTaSVC.Text = sBrief
        End If
        Me.LblDeleteSVC.Visible = True
        Me.LblSaveSvc.Visible = True
        If GridSVC.CurrentRow.Cells("Service").Value = "TransViet SVC Fee" Then
            Me.LblAddSF.Visible = False
            lbkAddFund.Enabled = False
            lbkUseFund.Enabled = False
        Else
            Me.LblAddSF.Visible = True
            lbkAddFund.Enabled = True
            lbkUseFund.Enabled = True
        End If

        If Me.GridSVC.CurrentRow.Cells("VendorID").Value <> 2 And
            Not Me.GridSVC.CurrentRow.Cells("Service").Value.ToString.Contains("SVC") Then
            If Me.GridSVC.CurrentRow.Cells("RelatedItem").Value <> Me.GridSVC.CurrentRow.Cells("RecID").Value Or
                Me.GridSVC.CurrentRow.Cells("RelatedItem").Value = 0 Then
                Me.LblAddSF.Visible = Me.LblAddSVC.Visible
            End If
        End If
        If Me.GridSVC.CurrentRow.Cells("VendorID").Value = 2 Then
            Me.GrpCost.Enabled = False
            If CmbService.Text = "HotLine" Then
                TxtUnitCost.Enabled = True
            End If

        Else
            Me.GrpCost.Enabled = True
        End If
        If GridSVC.CurrentRow.Cells("RelatedItem").Value = 0 Then
            lbkLinkItems.Visible = True
        Else
            lbkLinkItems.Visible = False
        End If

        mstrSelectedService = GridSVC.CurrentRow.Cells("Service").Value
        cboSubService.SelectedIndex = cboSubService.FindStringExact(GridSVC.CurrentRow.Cells("SubService").Value)

        txtPnr1A.Text = GridSVC.CurrentRow.Cells("PNR1A").Value
        txtPlace.Text = GridSVC.CurrentRow.Cells("Place").Value
        txtPaxCount.Text = GridSVC.CurrentRow.Cells("PaxCount").Value
        GrpCost.Tag = ""
        Return True
    End Function
    Private Sub LblSave_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblSaveSvc.LinkClicked
        If GridTour.CurrentRow.Cells("Status").Value = "RR" Then
            MsgBox("Không sửa được item cho Finalized Tcode!")
            Exit Sub
        End If
        'Me.LblSave.Visible = False
        If LblSaveSvc.Text = "Edit" Then
            RefreshSvc()
            LblAddSVC.Visible = False
            LblSaveSvc.Text = "SaveChange"
            lbkAbortChange.Visible = True
            Exit Sub
        End If
        Dim strQuerry As String
        'ngoai le dich vu Hot Line
        If CmbService.Text = "HotLine" Then
            If TxtPax.Text = "" Then
                MsgBox("You must input Caller Name")
                Exit Sub
            End If
            If TxtMoTaSVC.Text = "" Then
                MsgBox("You must input Call description")
                Exit Sub
            End If
            strQuerry = "Update Dutoan_item set Brief='" & TxtMoTaSVC.Text _
                    & "' where Recid=" & GridSVC.CurrentRow.Cells("RecId").Value
            If ExecuteNonQuerry(strQuerry, Conn) Then
                LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)
            Else
                MsgBox("Unable to Update HotLine Service")
            End If

            Exit Sub
        Else
            If Not CheckFormatAddService() Then
                Exit Sub
            End If
            DeleteService()
            AddService(True)
        End If

        If LblSaveSvc.Text = "SaveChange" Then
            LblAddSVC.Visible = True
            LblSaveSvc.Text = "Edit"
            lbkAbortChange.Visible = False
        End If

    End Sub

    Private Sub LblAddSVC_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblAddSVC.LinkClicked

        'Kiem tra tranh viec add them service sau khi da finalize
        Dim strTcodeStatus As String = ScalarToString("Dutoan_Tour", "Status", "RecId=" _
                                                      & GridTour.CurrentRow.Cells("RecId").Value)
        Select Case strTcodeStatus
            Case "RR"
                MsgBox("Tcode had been finalized!")
                Exit Sub
            Case "XX"
                MsgBox("Tcode had been deleted!")
                Exit Sub
        End Select

        ' Tranh  sua 2 ban ghi cung luc
        If Me.LblAddSVC.Tag = "Wait" Then Exit Sub
        Me.LblAddSVC.Tag = "Wait"

        'Bo sung phan loai dich vu chi tiet hon
        Select Case CmbService.Text
            Case "Visa", "Miscellaneous"
                If cboSubService.Text = "" Then
                    MsgBox("You must select SubService")
                    Exit Sub
                End If
        End Select

        If Not IsNumeric(txtVat4Vendor.Text) Then
            MsgBox("Invalid Vat4Vendor")
            Exit Sub
        ElseIf txtVat4Vendor.Text > 100 Then
            MsgBox("Invalid Vat4Vendor")
            Exit Sub
        End If
        If Not IsNumeric(txtPaxCount.Text) Then
            MsgBox("Invalid PaxCount")
            Exit Sub
        End If
        ' Unit cost VND khong duoc co so thap phan
        If CmbCurrItem.Text = "VND" _
            AndAlso Math.Floor(CDec(TxtUnitCost.Text)) <> Math.Ceiling(CDec(TxtUnitCost.Text)) Then
            MsgBox("Decimal is NOT allowed for VND!")
            Exit Sub
        End If
        'ngoai le dich vu Hot Line
        If CmbService.Text = "HotLine" And GrpCost.Tag <> "SVC" Then
            If TxtPax.Text = "" Then
                MsgBox("You must input Caller Name")
                Exit Sub
            End If
            If TxtMoTaSVC.Text = "" Then
                MsgBox("You must input Call description")
                Exit Sub
            End If

            If TxtVAT.Text <> txtVatPct.Text Then
                CheckVat(txtVatPct.Text)
            End If
            If Not CheckVat(TxtVAT.Text) Then Exit Sub
            AddFee("HotLine", "VND", 1, 0, True, TxtVAT.Text, 0, 1, TxtMoTaSVC.Text)
            LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)
            Exit Sub
        End If

        If GridTour.CurrentRow.Cells("Channel").Value = "CS" _
            AndAlso GridTour.CurrentRow.Cells("Traveller").Value <> "ZPERSONAL" Then
            If CmbService.Text = "TransViet SVC Fee" Then
                Me.LblAddSVC.Tag = "OK"
                MsgBox("Must use function AddSF for CWT Client on Business trip")
                Exit Sub
            ElseIf TxtQty.Text <> 1 Then
                Me.LblAddSVC.Tag = "OK"
                MsgBox("Must use Qty=1 for CWT Client on Business trip")
                Exit Sub
            End If
            If CmbService.Text = "Accommodations" AndAlso TxtUnitCost.Text = 0 _
                AndAlso txtCost4CWT.Text = 0 Then
                MsgBox("Must update Cost4 for CWT in this case!")
                Exit Sub
            End If
        End If

        If Me.GrpCost.Enabled Then
            Dim NoOfSvcInRQ As Int16 = ScalarToInt("Dutoan_Item", "count(*)", "DutoanID =" & Me.GridTour.CurrentRow.Cells("RecID").Value &
                                                   " and Status='OK' and service <>'TransViet  SVC Fee'")
            If NoOfSvcInRQ = 0 And Me.CmbService.Text = "TransViet  SVC Fee" Then
                MsgBox("Cannot Input TV SVC Fee Without Service", MsgBoxStyle.Critical, msgTitle)
                Me.LblAddSVC.Tag = "OK"
                Exit Sub
            End If
            If ScalarToInt("NonAirFund", "top 1 RecId", "Status='OK' and ExpiryDate>=getdate()" _
                    & " and VendorId=" & CmbVendor.SelectedValue & " order by RecId desc") > 0 Then
                MsgBox("VendorPendingBalance must be Used!")
            End If

            If Not CheckFormatAddService() Then
                Exit Sub
            Else
                If Not CheckVat(TxtVAT.Text) Then Exit Sub
                AddService(False)
            End If

        Else
            If Not CheckFormatTextBox(txtTvSfQty, True, 1, 1) Or txtTvSfQty.Text < 1 Then
                Exit Sub
            ElseIf GridTour.CurrentRow.Cells("Channel").Value = "CS" _
                AndAlso GridTour.CurrentRow.Cells("Owner").Value <> "ZPERSONAL" _
                AndAlso txtTvSfQty.Text <> 1 Then
                MsgBox("Must use Qty=1 for CWT Client on Business trip")
                Me.LblAddSVC.Tag = "OK"
                Exit Sub
            ElseIf TxtTVSfAmount.Text = 0 AndAlso cboZeroFeeReason.Text = "" Then
                MsgBox("You must select Reason for Zero Service Fee")
                Me.LblAddSVC.Tag = "OK"
                Exit Sub
            End If

            cmd.CommandText = "update Dutoan_Item set RelatedItem=reciD where recid=" & Me.GridSVC.CurrentRow.Cells("RecID").Value
            cmd.ExecuteNonQuery()

            If Not CheckVat(TxtVAT.Text) Then Exit Sub
            AddFee("TransViet " & Me.GrpCost.Tag, "VND", txtTvSfQty.Text, Me.TxtTVSfAmount.Text, IIf(Me.OptVATIncl.Checked, 1, 0),
                   Me.TxtVAT.Text, IIf(Me.GrpCost.Tag = "SVC", Me.GridSVC.CurrentRow.Cells("RecID").Value, 0), 1)
                MsgBox("Service Fee Added", MsgBoxStyle.Information, msgTitle)
                LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)
            End If

            Me.TxtUnitCost.Text = "0"
        Me.TxtQty.Text = "0"
        Me.LblAddSVC.Tag = "OK"
        txtCost4CWT.Text = 0
    End Sub
    Private Function InvalidInput(pSdate As Date, pQty As Integer) As Boolean
        Dim MyAns As Int16
        If pQty = 0 Then
            MsgBox("Qty must NOT be 0")
            Return True
        End If

        If txtPaxName.Text.Trim = "" AndAlso GridTour.CurrentRow.Cells("Channel").Value = "CS" _
            AndAlso TxtOwner.Text <> "ZPERSONAL" Then
            MsgBox("Invalid PaxName")
            Return True
        End If

        If CmbService.Text = "TransViet SVC Fee" AndAlso chkBookOnly.CheckState = CheckState.Checked Then
            MsgBox("TransViet SVC Fee CAN NOT be BookOnly!")
            Return True
        End If

        If Not Me.OptVATIncl.Checked And Not Me.OptVATNotIncl.Checked Then
            MsgBox("VAT option MUST be selected!")
            Return True
        End If

        If CInt(Me.TxtUnitCost.Text) = 0 Then
            MyAns = MsgBox("Oops! Zero Cost. Wanna Correct Your Input?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, msgTitle)
            If MyAns = vbYes Then Return True
        End If
        If pSdate.Date < Me.GridTour.CurrentRow.Cells("SDate").Value Then
            MsgBox("StartDate of service must be after StartDate Of Tour!")
            Return True
        End If

        If Me.CmbService.Text = "" Or Me.CmbVendor.Text = "" Or Me.CmbCurrItem.Text = "" Then Return True
        If InStr(D1Visible, Me.CmbService.Text) > 0 Then
            If Me.TxtD1.Value.Date > Me.TxtD2.Value.Date Then Return True
        End If
        Return False
    End Function
    Private Function GetSBrief() As String
        Dim KQ As String = "", dt1 As String, dt2 As String
        dt1 = Format(Me.TxtD1.Value, "dd-MMM-yy")
        dt2 = Format(Me.TxtD2.Value, "dd-MMM-yy")
        If Me.TxtT1.Checked Then dt1 = dt1 & " " & Format(Me.TxtT1.Value, "HH:mm")
        If Me.TxtT2.Checked Then dt2 = dt2 & " " & Format(Me.TxtT2.Value, "HH:mm")
        If InStr(SVC_WzDetail, Me.CmbService.Text) = 0 Then Return Me.TxtMoTaSVC.Text
        KQ = Me.cmbStype.Text & "_" & Me.CmbSCat.Text & "_" & dt1 & "_" & dt2 & "_" & Me.TxtQty.Text & "|" & Me.TxtMoTaSVC.Text
        KQ = Replace(KQ, "'", "")
        Return KQ
    End Function
    Private Function DefineSVCDate_time(pDate As Date, pTime As String) As Date
        Dim strKQ As String
        strKQ = Format(pDate, "dd-MMM-yy") & " " & pTime
        Return CDate(strKQ)
    End Function
    Private Function CheckFormatAddService() As Boolean
        Dim SDate As Date = DefineSVCDate_time(Me.TxtD2.Value.Date, Format(Me.TxtT2.Value, "HH:mm")), Qty As Int16 = Me.TxtQty.Text
        'Dim pmtMethod As String, SupplierID As Integer, Supplier As String
        If Me.CmbService.Text = SVCType.Accommodations Then Qty = Qty * DateDiff(DateInterval.Day, Me.TxtD1.Value.Date, Me.TxtD2.Value.Date)
        If InStr(D1Visible, Me.CmbService.Text) > 0 Then SDate = DefineSVCDate_time(Me.TxtD1.Value.Date, Format(Me.TxtT1.Value, "HH:mm"))
        If InvalidInput(SDate, Qty) Then
            MsgBox("invalid Input", MsgBoxStyle.Critical, msgTitle)
            Return False
        End If
        'custid
        If Me.CmbService.Text = "Registration" And Me.CmbCust.Text.Contains("SANOFI") Then
            Dim TourIDofSameRefNo As Integer = ScalarToInt("Dutoan_Tour", "recID", "CustID =" & Me.GridTour.CurrentRow.Cells("CustID").Value &
                                                           " and RefNo='" & Me.txtRefCode.Text.ToUpper.Trim &
                                                           "' and status<>'XX' and tcode <>'" & Me.GridTour.CurrentRow.Cells("Tcode").Value & "'")
            If TourIDofSameRefNo > 0 Then ' da tung co 1 EventCode, likely la Reg, can tim DV cua TourID nay 
                Dim ItemIDofReg As Integer = ScalarToInt("Dutoan_Item", "RecID", "DutoanID=" & TourIDofSameRefNo &
                                                         " and status<>'XX' and Service='Registration' ")
                If ItemIDofReg > 0 And Me.CmbService.Text = "Registration" Then
                    MsgBox("Duplicate Registration/EventCode", MsgBoxStyle.Critical, msgTitle)
                    Return False
                End If
            End If
        End If
        Return True
    End Function
    Private Sub AddService(ByVal isEdit As Boolean)
        Dim SDate As Date = DefineSVCDate_time(Me.TxtD2.Value.Date, Format(Me.TxtT2.Value, "HH:mm")), Qty As Int16 = Me.TxtQty.Text
        Dim pmtMethod As String, SupplierID As Integer, Supplier As String
        If Me.CmbService.Text = SVCType.Accommodations Then Qty = Qty * DateDiff(DateInterval.Day, Me.TxtD1.Value.Date, Me.TxtD2.Value.Date)
        If InStr(D1Visible, Me.CmbService.Text) > 0 Then SDate = DefineSVCDate_time(Me.TxtD1.Value.Date, Format(Me.TxtT1.Value, "HH:mm"))
        'If InvalidInput(SDate, Qty) Then
        '    MsgBox("invalid Input", MsgBoxStyle.Critical, msgTitle)
        '    Exit Sub
        'End If
        ''custid
        'If Me.CmbService.Text = "Registration" And Me.CmbCust.Text.Contains("SANOFI") Then
        '    Dim TourIDofSameRefNo As Integer = ScalarToInt("Dutoan_Tour", "recID", "CustID =" & Me.GridTour.CurrentRow.Cells("CustID").Value &
        '                                                   " and RefNo='" & Me.txtRefCode.Text.ToUpper.Trim &
        '                                                   "' and status<>'XX' and tcode <>'" & Me.GridTour.CurrentRow.Cells("Tcode").Value & "'")
        '    If TourIDofSameRefNo > 0 Then ' da tung co 1 EventCode, likely la Reg, can tim DV cua TourID nay 
        '        Dim ItemIDofReg As Integer = ScalarToInt("Dutoan_Item", "RecID", "DutoanID=" & TourIDofSameRefNo &
        '                                                 " and status<>'XX' and Service='Registration' ")
        '        If ItemIDofReg > 0 And Me.CmbService.Text = "Registration" Then
        '            MsgBox("Duplicate Registration/EventCode", MsgBoxStyle.Critical, msgTitle)
        '            Exit Sub
        '        End If
        '    End If
        'End If

        Dim NewRecNo As Integer, isVATincl As Boolean = Me.OptVATIncl.Checked, SBrief As String, VendorID As Integer = 0
        SBrief = GetSBrief()
        If Not Me.CmbVendor.SelectedValue Is Nothing Then VendorID = Me.CmbVendor.SelectedValue
        If Me.CmbSupplier.SelectedValue Is Nothing Then
            If VendorID = 2 Then
                SupplierID = 2
                Supplier = ""
            Else
                MsgBox("You Have to Specify Supplier", MsgBoxStyle.Critical, msgTitle)
                Exit Sub
            End If
        Else
            SupplierID = Me.CmbSupplier.SelectedValue
            Supplier = Me.CmbSupplier.Text
        End If
        If VendorID = 2 Then
            pmtMethod = "PSP"
            Supplier = ""
        Else
            pmtMethod = ScalarToString("Vendor", "FOP", "RecID=" & VendorID)
            If String.IsNullOrEmpty(pmtMethod) Then pmtMethod = "PPD"
        End If

        cmd.CommandText = "insert DuToan_Item (Service, CCurr, Unit, Qty, Cost, Supplier, VendorID" _
            & ", Vendor, FstUser, PmtMethod, isVATIncl " _
            & ", VAT, DuToanID, Brief, SupplierRMK, MU, SVCDate, NeedDeposit, SupplierID" _
            & ", BookOnly, CostOnly,TrxCount,PaxName,NonCompliance,SubService,Vat4Vendor,Place,PaxCount,DomInt)" _
            & " Values (@Service, @CCurr, @Unit, @Qty, @Cost" _
            & ", @Supplier, @VendorID, @Vendor, @FstUser, @PmtMethod, @isVATIncl, @VAT, @DuToanID" _
            & ", @Brief, @SupplierRMK, @MU, @SVCDate " _
            & ",@NeedDeposit,@SupplierID, @BookOnly, @CostOnly,@TrxCount,@PaxName" _
            & ",@NonCompliance,@SubService,@Vat4Vendor,@Place,@PaxCount,@DomInt)" _
            & "; SELECT SCOPE_IDENTITY() AS [RecID]"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@Service", SqlDbType.VarChar).Value = Me.CmbService.Text
        cmd.Parameters.Add("@CCurr", SqlDbType.VarChar).Value = Me.CmbCurrItem.Text
        cmd.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.TxtUnit.Text
        cmd.Parameters.Add("@Qty", SqlDbType.Decimal).Value = Qty
        cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = CDec(Me.TxtUnitCost.Text)
        cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = Supplier
        cmd.Parameters.Add("@Vendor", SqlDbType.VarChar).Value = Me.CmbVendor.Text
        cmd.Parameters.Add("@VendorID", SqlDbType.Int).Value = VendorID
        cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        cmd.Parameters.Add("@PmtMethod", SqlDbType.VarChar).Value = pmtMethod
        cmd.Parameters.Add("@isVATIncl", SqlDbType.Bit).Value = IIf(isVATincl, 1, 0)
        cmd.Parameters.Add("@VAT", SqlDbType.Decimal).Value = CDec(Me.TxtVAT.Text)
        cmd.Parameters.Add("@DuToanID", SqlDbType.Int).Value = Me.GridTour.CurrentRow.Cells("RecID").Value
        cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value = SupplierID
        cmd.Parameters.Add("@NeedDeposit", SqlDbType.Int).Value = Me.ChkDeposit.Checked
        cmd.Parameters.Add("@Brief", SqlDbType.NVarChar).Value = SBrief
        cmd.Parameters.Add("@SupplierRMK", SqlDbType.NVarChar).Value = Me.TxtMotaCost.Text
        cmd.Parameters.Add("@MU", SqlDbType.Decimal).Value = CDec(Me.txtMU.Text)
        cmd.Parameters.Add("@SVCDate", SqlDbType.DateTime).Value = SDate
        cmd.Parameters.Add("@BookOnly", SqlDbType.Bit).Value = IIf(Me.chkBookOnly.Checked, 1, 0)
        cmd.Parameters.Add("@CostOnly", SqlDbType.Bit).Value = IIf(Me.ChkCostOnly.Checked, 1, 0)
        cmd.Parameters.Add("@TrxCount", SqlDbType.Int).Value = TxtQty.Text
        cmd.Parameters.Add("@PaxName", SqlDbType.VarChar).Value = txtPaxName.Text.Trim
        cmd.Parameters.Add("@NonCompliance", SqlDbType.Bit).Value = IIf(Me.chkNonCompliance.Checked, 1, 0)
        cmd.Parameters.Add("@SubService", SqlDbType.VarChar).Value = cboSubService.Text
        cmd.Parameters.Add("@VAT4Vendor", SqlDbType.Decimal).Value = CDec(Me.txtVat4Vendor.Text)
        cmd.Parameters.Add("@Place", SqlDbType.VarChar).Value = txtPlace.Text
        cmd.Parameters.Add("@PaxCount", SqlDbType.Int).Value = txtPaxCount.Text
        cmd.Parameters.Add("@DomInt", SqlDbType.VarChar).Value = txtDomInt.Text
        NewRecNo = cmd.ExecuteScalar

        'insert vao AOP cho Trungpq
        cmd.Parameters.Clear()

        If VendorID <> 0 Then
            cmd.CommandText = InsertVendorId4AOP(VendorID, myStaff.City)
            cmd.ExecuteNonQuery()
        End If


        If isEdit Then
            Dim BeingEditTedRecNo As Integer = Me.GridSVC.CurrentRow.Cells("RecID").Value
            Dim OldROE As Decimal = Me.GridSVC.CurrentRow.Cells("ROE").Value
            Dim OldCurr As String = Me.GridSVC.CurrentRow.Cells("CCurr").Value
            Dim DaTra As Decimal = ScalarToDec("dutoan_pmt", "isnull(sum(VND),0)", "ItemID=" & BeingEditTedRecNo & " And status<>'XX'")
            Try
                If DaTra <> 0 Then ' gan lai ban ghi datra cho item cu thanh cho item moi
                    cmd.CommandText = " Update Dutoan_pmt set itemID=" & NewRecNo & " where ItemID=" & BeingEditTedRecNo &
                        "; update Dutoan_Item set VNDPaid= " & DaTra & " where recid=" & NewRecNo
                    cmd.ExecuteNonQuery()
                End If
                If OldCurr = Me.CmbCurrItem.Text And Me.CmbCurrItem.Text <> "VND" And OldROE <> 0 Then
                    cmd.CommandText = String.Format("update dutoan_item set ROE={0} where RecID={1}", OldROE, NewRecNo)
                    cmd.ExecuteNonQuery()
                End If
                If Me.GridSVC.CurrentRow.Cells("recID").Value = Me.GridSVC.CurrentRow.Cells("RelatedItem").Value Then ' da co sf
                    cmd.CommandText = String.Format("update dutoan_item set relatedItem={0} where relatedItem={1} and vendorID=2; " &
                        "update dutoan_item set relatedItem={0} where RecID={0}", NewRecNo, BeingEditTedRecNo)
                    cmd.ExecuteNonQuery()
                End If
                If Me.GridSVC.CurrentRow.Cells("VatInvId").Value > 0 Then ' da co VAT INV ID duoc xuat
                    cmd.CommandText = String.Format("update dutoan_item set VatInvId={0} where RecId={1}", Me.GridSVC.CurrentRow.Cells("VatInvId").Value, NewRecNo)
                    cmd.ExecuteNonQuery()
                End If
                If Me.GridSVC.CurrentRow.Cells("MainItem").Value > 0 Then ' da link voi MainItem
                    cmd.CommandText = String.Format("update dutoan_item set MainItem={0} where RecId={1}", Me.GridSVC.CurrentRow.Cells("MainItem").Value, NewRecNo)
                    cmd.ExecuteNonQuery()
                End If
            Catch ex As Exception
                Append2TextFile(ex.Message & vbNewLine & cmd.CommandText)
                GoTo ErrHandler
            End Try
        End If
        Me.CmbVendor.Visible = True

        Me.CmbCurrItem.Text = "VND"
        MsgBox("Service Item Added", MsgBoxStyle.Information, msgTitle)

        If Me.CmbService.Text = "Visa" Or Me.CmbService.Text = "Registration" Then
            If MsgBox("Wanna Order a Messgenger to Collect Travel Docs?", MsgBoxStyle.Question Or vbYesNo, msgTitle) = vbYes Then
                Book_a_MSGR(myStaff.SICode, myStaff.PSW, "N/A", Me.GridTour.CurrentRow.Cells("Tcode").Value, 0)
            End If
        End If

        If txtBooker.Text <> "ZPERSONAL" Then
            'Tinh service fee dang % tu dong
            Dim strAutoServiceType As String = GetAutoSfType(GridTour.CurrentRow.Cells("RecId").Value)
            If strAutoServiceType <> "N/A" Then
                Dim lstQuerries As New List(Of String)
                lstQuerries.AddRange(AutoSf(isEdit, strAutoServiceType))
                If Not UpdateListOfQuerries(lstQuerries, Conn) Then
                    MsgBox("Unable to AutoSF for " & Me.GridTour.CurrentRow.Cells("RecId").Value _
                           & ". Please report Khanhnm")
                End If
            End If
        End If
        LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)
        Exit Sub
ErrHandler:
        MsgBox(NewRecNo.ToString & ": Error Occurs During Saving Changes. " _
               & cmd.CommandText & ". Plz take a screenshot and email Khanhnm", MsgBoxStyle.Information, msgTitle)
    End Sub


    Private Sub LblDeleteSVC_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDeleteSVC.LinkClicked
        Dim myAns As Integer = MsgBox("Clicking Delete by Mistake?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle)
        If myAns = vbYes Then Exit Sub
        If DeleteService() Then
            If GridTour.CurrentRow.Cells("Contact").Value <> "ZPERSONAL" Then
                Dim strAutoServiceType As String = GetAutoSfType(GridTour.CurrentRow.Cells("RecId").Value)
                If strAutoServiceType <> "N/A" Then
                    If Not UpdateListOfQuerries(AutoSf(True, strAutoServiceType), Conn) Then
                        MsgBox("Unable to AutSF " & GridTour.CurrentRow.Cells("RecId").Value)
                    End If
                End If
            End If

            LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)
        End If

    End Sub
    Private Function DeleteService() As Boolean
        If Me.GridTour.CurrentRow.Cells("Status").Value = "RR" Then
            Return False
        End If
        Dim ExistingPmtID As Integer = ScalarToInt("Dutoan_pmt", "RecID", "ItemID=" & Me.GridSVC.CurrentRow.Cells("recID").Value & " and status='OK'")
        If Me.GridSVC.CurrentRow.Cells("VNDPaid").Value > 0 Or ExistingPmtID > 0 Then

            If myStaff.HasExtraRight("EditPaidNonAirItem") Then
                MsgBox("This Item Has Been Paid. Be Careful.", MsgBoxStyle.Critical, msgTitle)
            Else
                MsgBox("This Item Has Been Paid. Ask Supervisor to Delete.", MsgBoxStyle.Critical, msgTitle)
                Return False
            End If

        End If

        If GridSVC.CurrentRow.Cells("Service").Value = "VendorPendingBalance" _
            AndAlso Not ChangeNonAirFund(GridSVC.CurrentRow.Cells("RecId").Value, Math.Abs(GridSVC.CurrentRow.Cells("Cost").Value)) Then
            MsgBox("Unable to adjust VendorPendingBalance for Item " _
                    & GridSVC.CurrentRow.Cells("RecId").Value & ". Please report NMK")
            Return False
        End If

        cmd.CommandText = ChangeStatus_ByID("DuToan_Item", "XX", Me.GridSVC.CurrentRow.Cells("recID").Value)
        cmd.ExecuteNonQuery()

        If Me.GridSVC.CurrentRow.Cells("RelatedItem").Value <> 0 Then ' da co SF
            If Me.GridSVC.CurrentRow.Cells("RelatedItem").Value = Me.GridSVC.CurrentRow.Cells("RecID").Value Then ' ban ghi bi huy la chinh, se huy ban ghi sf
                cmd.CommandText = ChangeStatus_ByDK("DuToan_Item", "XX", String.Format("relatedItem={0} and status='OK'", Me.GridSVC.CurrentRow.Cells("recID").Value))
            Else ' ban ghi bi huy la sf, se danh dau ban ghi chinh thanh chua co sf
                cmd.CommandText = "update DuToan_Item set relatedItem=0 where status='OK' and recid=" & Me.GridSVC.CurrentRow.Cells("RelatedItem").Value
            End If
            cmd.ExecuteNonQuery()
        End If

        Return True
    End Function
    Private Sub LblDeleteTour_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDeleteTour.LinkClicked
        If myStaff.SupOf = "" Then Exit Sub
        Dim myAns As Integer = MsgBox("Clicking Delete by Mistake?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle)
        Dim AirID As Integer
        Dim strLastFinalizedDate As String = ""
        Dim lstQuerries As New List(Of String)

        If myAns = vbYes Then Exit Sub
        'custid
        If Me.GridTour.CurrentRow.Cells("custID").Value < 0 Then
            cmd.CommandText = String.Format("Delete from dutoan_tour where recid={0}; delete from dutoan_item where dutoanID={0}", Me.GridTour.CurrentRow.Cells("recID").Value)
            cmd.ExecuteNonQuery()
            GoTo ResumeHere
        End If
        If Me.GridTour.CurrentRow.Cells("billingBy").Value = BillBy.BillByBundle Or
                Me.GridTour.CurrentRow.Cells("billingBy").Value = BillBy.BillByEvent Then
            AirID = ScalarToInt("FOP", "RecID", "status<>'XX' and Document='" & Me.GridTour.CurrentRow.Cells("TCode").Value & "'")
            If AirID > 0 Then
                MsgBox("This TourCode Has Air Part. Plz Ask Air Team To Change FOP Before Continue", MsgBoxStyle.Critical, msgTitle)
                Exit Sub
            End If
        End If
        Dim LyDo As String = InputBox("Plz Enter Valid Reason for Deleting", msgTitle, "By Customer RQST")
        If LyDo = "" Then Exit Sub

        lstQuerries.Add("Update DuToan_Tour set status='XX', RMK=RMK+'|" & LyDo & "' where recid=" &
                        Me.GridTour.CurrentRow.Cells("recID").Value)
        lstQuerries.Add(ChangeStatus_ByDK("DuToan_Item", "XX", String.Format("DuToanID={0}", Me.GridTour.CurrentRow.Cells("recID").Value)))
        strLastFinalizedDate = ScalarToString("ActionLog", "top 1 F2", "DoWhat='Unfinalize' and F1='" _
                                      & GridTour.CurrentRow.Cells("Tcode").Value _
                                      & "' order by RecId desc")
        If strLastFinalizedDate <> "" Then
            lstQuerries.Add("insert into ActionLog (TableName,DoWhat,ActionBy,F1,F2) values" _
                            & "('Dutoan_Tour','DeleteFinalized','" & myStaff.SICode & "','" _
                            & GridTour.CurrentRow.Cells("Tcode").Value & "','" & strLastFinalizedDate & "')")
        End If
        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Unable to delete")
        End If

ResumeHere:
        LoadgridTour()
    End Sub
    Private Sub GridSVC_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridSVC.CellContentDoubleClick
        Me.GridSVC.Width = 773
        Me.GridSVC.BringToFront()
    End Sub
    Private Function UpdateROE_ItemPricing(QuoteOrSvcCfm As String) As Boolean
        Dim strDKWhatRow As String = String.Format("DuToanID={0} and status='OK'", Me.GridTour.CurrentRow.Cells("RecID").Value)
        Dim strDKZeroROE As String = String.Format("{0} and ROE=0", strDKWhatRow)
        Dim MyAns As Integer, tmpROE As Decimal
        cmd.CommandText = String.Format("update dutoan_item set roe=1 where {0} and cCurr='VND'", strDKWhatRow)
        cmd.ExecuteNonQuery()
        Dim tmpCurr As String = ScalarToString("DuToan_Item", "top 1 CCurr", strDKZeroROE)
        If String.IsNullOrEmpty(tmpCurr) Then
            'If QuoteOrSvcCfm = "Q" Or myStaff.SupOf = "" Then Exit Sub

            MyAns = MsgBox("ROE Has Been Updated. Wanna Edit?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, msgTitle)
            If MyAns = vbNo Then
                Return True
            Else
                cmd.CommandText = String.Format("update dutoan_item set roe=0 where {0} and cCurr<>'VND'", strDKWhatRow)
                cmd.ExecuteNonQuery()
            End If
        End If
        Do
            tmpCurr = ScalarToString("DuToan_Item", "top 1 CCurr", strDKZeroROE)
            If String.IsNullOrEmpty(tmpCurr) Then Exit Do
            Select Case tmpCurr
                Case "VND"
                    'BO QUA
                Case "USD"
                    tmpROE = ForEX_12(myStaff.City, Now, tmpCurr, "BSR", "TS").Amount
                Case Else
                    tmpROE = GetTsRoeMostUpdated(Now, tmpCurr)
            End Select

            If tmpROE = 0 Then
                MsgBox("ROE for " & tmpCurr & " not Found. Ask Accounting Dept to Update", MsgBoxStyle.Critical, msgTitle)
                Return False
            End If
            cmd.CommandText = String.Format("update dutoan_item set roe={0} where {1} and cCurr='{2}'", tmpROE, strDKWhatRow, tmpCurr)
            cmd.ExecuteNonQuery()
        Loop
        Return True
    End Function
    Private Sub CmbService_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbService.SelectedIndexChanged
        Me.TxtHTLSF.Text = 0
        Me.LblD2.Text = "Svc Date"
        Me.TxtUnit.Text = ""
        Me.TxtT1.Checked = False
        Me.TxtT2.Checked = False
        Me.TxtT1.Text = "00:00"
        Me.TxtT2.Text = "00:00"

        Select Case CmbService.Text
            Case "Visa", "Miscellaneous"
                LoadCombo(cboSubService, "Select Val1 as value from Misc where cat='NonAirSubSvc'" _
                            & " and Val='" & CmbService.Text _
                            & "' order by intVal", Conn)
            Case Else
                cboSubService.DataSource = Nothing
                '  cboSubService.Items.Clear()
        End Select

        If Me.CmbService.Text = "Miscellaneous" Then
            Me.CmbVendor.DropDownStyle = ComboBoxStyle.DropDown
        Else
            Me.CmbVendor.DropDownStyle = ComboBoxStyle.DropDownList
        End If
        If InStr(SVC_WzDetail, Me.CmbService.Text) = 0 Then
            Me.LblSCat.Visible = False
            'LoadCmb_VAL(Me.CmbVendor, QrySupplier)
            If Me.CmbService.Text = "Air Tickets" Then
                LoadCmb_VAL(Me.CmbVendor, String.Format("{0} and cat='{1}'", QrySupplier, "AR"))
                Me.LblD1.Visible = False
                Me.TxtD1.Visible = False
                Me.LblD2.Visible = True
                Me.TxtD2.Visible = True
                Me.TxtUnit.Text = "Pax"
            End If
        Else
            Me.LblSCat.Visible = True
            Me.LblD1.Visible = True
            Me.TxtD1.Visible = True
            Me.LblD2.Visible = True
            Me.TxtD2.Visible = True
            If Me.CmbService.Text = SVCType.Accommodations Then

                Me.LblSType.Text = "RoomType"
                Me.LblSCat.Text = "RoomCat"
                LblD1.Text = "ChkIn"
                LblD2.Text = "ChkOut"
                Me.TxtHTLSF.Text = 5
                Me.TxtUnit.Text = "R/N"
                'LoadCmb_VAL(Me.CmbVendor, String.Format("{0} and cat='{1}'", QrySupplier, "KS"))
            ElseIf Me.CmbService.Text = SVCType.Transfer Then
                Me.LblSType.Text = "CarType"
                Me.LblSCat.Text = "CarCat"
                LblD1.Text = "PickUp"
                LblD2.Text = "Drop"
                'LoadCmb_VAL(Me.CmbVendor, String.Format("{0} and cat='{1}'", QrySupplier, "XE"))
                Me.TxtUnit.Text = "Car"
            ElseIf Me.CmbService.Text = SVCType.Meal Then
                Me.LblSType.Text = "Cusine"
                Me.LblSCat.Text = "MenuType"
                LblD1.Visible = False
                LblD2.Text = "Time"
                Me.TxtD1.Visible = False
                Me.TxtUnit.Text = "Pax"
                Me.TxtUnit.Visible = True
                'LoadCmb_VAL(Me.CmbVendor, String.Format("{0} and cat='{1}'", QrySupplier, "NH"))
            ElseIf Me.CmbService.Text = SVCType.Insurance Then
                Me.LblSType.Text = "Type"
                Me.LblSCat.Text = "Cat"
                LblD1.Visible = True
                LblD1.Text = "From"
                LblD2.Text = "Thru"
                Me.TxtD1.Visible = True
                Me.TxtUnit.Text = "Person"
            End If
            Me.cmbStype.DataSource = GetDataTable("Select VAL from Misc where cat='STYPE' and val1='" & Me.CmbService.Text & "'")
            Me.cmbStype.DisplayMember = "VAL"

            Me.CmbSCat.DataSource = GetDataTable("Select VAL from Misc where cat='SCAT' and val1='" & Me.CmbService.Text & "'")
            Me.CmbSCat.DisplayMember = "VAL"
        End If
        Me.CmbVendor.SelectedValue = 2

        If CmbService.Text <> mstrSelectedService Then
            LblSaveSvc.Visible = False
        Else
            LblSaveSvc.Visible = True
        End If



    End Sub

    Private Sub LblFilter_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblFilter.LinkClicked
        LoadCmb_VAL(Me.CmbVendor, String.Format("{0} and shortname like '%{1}%'", QrySupplier, Me.TxtFilter.Text))
    End Sub
    Private Sub GenTourCode(ByVal pStart As Date, ByVal pCustName As String, ByVal pBillType As String)
        Dim LastTC As String, strThang As String = pStart.Month.ToString.Trim
        Dim LastChar As String
        If strThang.Length = 2 Then strThang = Chr(CInt(strThang) + 55)
        Dim tmpTC As String = pCustName & (pStart.Year - 2010).ToString.Trim & strThang & pStart.Day.ToString.Trim & pBillType
        cmd.CommandText = "select top 1 TCode from DuToan_Tour where TCode like '" & tmpTC & "%' order by RecID desc"
        LastTC = cmd.ExecuteScalar
        If String.IsNullOrEmpty(LastTC) Then
            LastChar = "A"
        ElseIf Strings.Right(LastTC, 3).Contains(".") Then
            LastChar = Strings.Right(LastTC, 2)
            LastChar = "." & Format(CInt(LastChar) + 1, "00")
        ElseIf Strings.Right(LastTC, 1) = "Z" Then
            LastChar = "0"
        ElseIf Strings.Right(LastTC, 1) = "9" Then
            LastChar = ".10"
        Else
            LastChar = Strings.Right(LastTC, 1)
            LastChar = Chr(Asc(LastChar) + 1)
        End If
        tmpTC = tmpTC & LastChar
        Me.txtTcode.Text = tmpTC
    End Sub
    'Private Sub GenListCCenter(pCustID As Integer)
    '    'custid
    '    Dim pSQL As String = "select distinct Traveler as VAL from employeeid where custid=" & pCustID & " and status='OK'"
    '    Dim tbl As DataTable = GetDataTable(pSQL)
    '    For i As Int16 = 0 To tbl.Rows.Count - 1
    '        Me.LstCCenter.Items.Add(tbl.Rows(i)("VAL"))
    '    Next
    'End Sub

    Private Sub CmbCust_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCust.SelectedIndexChanged
        'Private Sub CmbCust_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCust.LostFocus _
        ', CmbCust.SelectedIndexChanged
        If Not mblnFirstLoadCompleted Then
            Exit Sub
        End If
        MyCust.CustID = Me.CmbCust.SelectedValue
        ClearTcodeLabel()

        Dim NeedCheckTravelerName As Int16
        LoadBuAdmin(MyCust.ShortName)
        If MyCust.ShortName.Contains("SANOFI") Then
            'LoadCmb_MSC(Me.CmbCCenter, "select distinct Traveler as VAL from employeeid where custid=" & MyCust.CustID & " and status='OK'")
            'custid
            'GenListCCenter(MyCust.CustID)


            Me.TxtPRNo.Text = "_PR No."
            Me.txtRefCode.Text = "_EVENT Code"
            Me.TxtOwner.Text = "_EVENT OWNER"
            'ElseIf MyCust.ShortName = "LOR VN" Then
            '    'LoadCmb_MSC(Me.CmbCCenter, "select '' as VAL ")
            '    'Me.LstCCenter.Items.Clear()
            '    'LoadCmb_MSC(Me.CmbDept, "SELECT '' as VAL ")

            '    Me.TxtPRNo.Text = "_GL"
            '    Me.txtRefCode.Text = "_TER"
            '    Me.TxtOwner.Text = "_BUDGET"
        Else
            LoadTcodeLabel(CmbCust.Text)

            'txtBooker.DropDownStyle = ComboBoxStyle.DropDownList
            'Me.LstCCenter.Items.Clear()
        End If
        'custid
        NeedCheckTravelerName = ScalarToInt("cwt.dbo.go_companyinfo1", "Empl4NonAir", "Status='OK' and custid=" & MyCust.CustID)
        Me.cmbTraveler.DataSource = Nothing
        If NeedCheckTravelerName = 0 Then
            Me.cmbTraveler.DropDownStyle = ComboBoxStyle.Simple
        Else
            Me.cmbTraveler.DropDownStyle = ComboBoxStyle.DropDownList
            LoadCmb_MSC(Me.cmbTraveler, "SELECT Traveler as VAL FROM cwt.dbo.GO_EmployeeID where status<>'XX' and CustID=" & MyCust.CustID)
        End If
        'Try
        '    LoadCmb_MSC(txtBooker, "Select BookerName as VAL from cwt.dbo.Cwt_Bookers where Status='OK' and CustId=" & Me.CmbCust.SelectedValue)
        'Catch ex As Exception
        '    LoadCmb_MSC(txtBooker, "select distinct fValue as VAL from SIR where fName+status='BOOKEROK' and custID=" & Me.CmbCust.SelectedValue)
        'End Try
        LoadGridGO(True)
        'LoadgridTour()
    End Sub
    Private Function GetQryMCE2PSP(ppTcode As String) As String
        'Return "; update FOP set fop='" & MyCust.DelayType & "', RMK='TT.INV.BDL' where status='OK' and fop='MCE' and document='" & ppTcode & "'"  '^_^20220929 mark by 7643
        Return "update FOP set fop='" & MyCust.DelayType & "', RMK='TT.INV.BDL' where status='OK' and fop='MCE' and document='" & ppTcode & "'"  '^_^20220929 modi by 7643
    End Function
    Private Sub LblFinalize_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LckLblFinalize.LinkClicked
        If HasNewerVersion_R12(Application.ProductVersion) Or SysDateIsWrong(Conn) Then
            Me.Close()
            Me.Dispose()
            End
        End If

        Dim MyAns As Integer = MsgBox("Did You Click This By Accident", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle)
        If MyAns = vbYes Then Exit Sub
        Dim DuToanID As String = ScalarToString("FOP", "RMK", " Document='" & Me.GridTour.CurrentRow.Cells("TCode").Value & "' and status='OK' and fop <>'MCE' and rmk <>'MCE'")
        Dim blnFinalizeOK As Boolean

        If Not String.IsNullOrEmpty(DuToanID) Then
            MsgBox("This Tour Code Has Been Finalised", MsgBoxStyle.Information, msgTitle)
            Exit Sub
        End If

        'Kiem tra co Bill moi cho Finalize (StoreFile)
        If GridTour.CurrentRow.Cells("Contact").Value <> "ZPERSONAL" _
            AndAlso ScalarToInt("MISC", "RecId", "Val='STOREDFILE GRP' and IntVal = " & GridTour.CurrentRow.Cells("CustId").Value) > 0 Then
            'Voi Sanofi thi dung file Registration thay cho Bill
            If ScalarToInt("MISC", "RecId", "Val='SANOFI LOCAL' and IntVal = " & GridTour.CurrentRow.Cells("CustId").Value) > 0 _
                AndAlso ScalarToInt("Dutoan_Item", "count (*)", "status='OK' and Service ='Registration' and DutoanId = " & GridTour.CurrentRow.Cells("RecId").Value) > 0 _
                AndAlso GridTour.CurrentRow.Cells("FileId").Value = 0 Then
                MsgBox("You must Store Registration files to finalize this Tcode!")
                Exit Sub

            ElseIf ScalarToInt("MISC", "RecId", "Val='STOREDFILE GRP - NON SYT' and IntVal = " & GridTour.CurrentRow.Cells("CustId").Value) > 0 _
                AndAlso ScalarToInt("Dutoan_Item", "top 1 RecId", "Status='OK' and Service='Registration' and DutoanId=" & GridTour.CurrentRow.Cells("RecId").Value) > 0 Then
                'bo qua
            ElseIf ScalarToInt("Images", "RecId", "Dutoan_TourID = " & GridTour.CurrentRow.Cells("RecId").Value) = 0 Then
                MsgBox("You must Store Bill files to finalize this Tcode!")
                Exit Sub
            End If
        End If

        'Kiem tra da pass du chung tu cho ke toan
        With GridTour.CurrentRow
            If .Cells("EDate").Value >= "30 Jun 22" Then
                If Not (IsInCustGrp("ABBOTT LOCAL", .Cells("CustShortName").Value) _
                    Or IsInCustGrp("SANOFI LOCAL", .Cells("CustShortName").Value) _
                    Or IsInCustGrp("NVS LOCAL ", .Cells("CustShortName").Value)) _
                    AndAlso .Cells("ActStatus").Value <> "QC" Then
                    MsgBox("Chưa đủ điều kiện Finalize theo yêu cầu của kế toán!")
                    Exit Sub
                End If
            End If
        End With

        Dim tmpROE As Decimal, tmpPrice As Decimal, strSQL As String
        Dim i As Integer, strSql4Roe As String
        Dim tblItems As DataTable = GetDataTable("Select * from dutoan_Item where Status='OK'" _
                                                 & " and DutoanId=" & Me.GridTour.CurrentRow.Cells("recID").Value)
        Dim lstQuerries As New List(Of String)  '^_^20220929 add by 7643

        'strSQL = ChangeStatus_ByID("Dutoan_Tour", "RR", Me.GridTour.CurrentRow.Cells("recID").Value)  '^_^20220929 mark by 7643
        lstQuerries.Add(ChangeStatus_ByID("Dutoan_Tour", "RR", Me.GridTour.CurrentRow.Cells("recID").Value))  '^_^20220929 modi by 7643
        If tblItems.Rows.Count = 0 Then
            MyAns = MsgBox("This Tourcode is Empty. Wanna Recheck?", MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, msgTitle)
            If MyAns = vbYes Then Exit Sub
            'cmd.CommandText = strSQL  '^_^20220929 mark by 7643
            If Me.GridTour.CurrentRow.Cells("BillingBy").Value = BillBy.BillByBundle Then
                'cmd.CommandText = cmd.CommandText & GetQryMCE2PSP(Me.GridTour.CurrentRow.Cells("TCode").Value)  '^_^20220929 mark by 7643
                lstQuerries.Add(GetQryMCE2PSP(Me.GridTour.CurrentRow.Cells("TCode").Value))  '^_^20220929 modi by 7643
            End If
            GoTo UpdateDbHere
        ElseIf (GridTour.CurrentRow.Cells("Channel").Value = "CS" Or MyCust.LinkNonAirServiceWzFee) _
                AndAlso GridTour.CurrentRow.Cells("Contact").Value <> "ZPERSONAL" Then
            For i = 0 To tblItems.Rows.Count - 1
                If tblItems.Rows(i)("RelatedItem") = 0 _
                    AndAlso Not ("Merchant Fee,Bank Fee").Contains(tblItems.Rows(i)("Service")) _
                    AndAlso tblItems.Rows(i)("ZeroFeeReason") = "" Then
                    MsgBox("Must link SVC with FEE for item" & tblItems.Rows(i)("RecId"))
                    Exit Sub
                End If
                If tblItems.Rows(i)("ROE") = 0 Then
                    MsgBox("Must update ROE for item " & tblItems.Rows(i)("RecId"))
                    Exit Sub
                End If
                If tblItems.Rows(i)("Service") = "Accommodations" Then
                    If tblItems.Rows(i)("Cost") = 0 AndAlso IsDBNull(tblItems.Rows(i)("Cost4CWT")) Then
                        MsgBox("Must update Cost4CWT for item" & tblItems.Rows(i)("RecId"))
                        Exit Sub
                    ElseIf GridTour.CurrentRow.Cells("CustShortName").Value = "" _
                        AndAlso tblItems.Rows(i)("Cost") <> 0 AndAlso tblItems.Rows(i)("PNR1A") = "" Then
                        MsgBox("You must create GK PNR1A for hotel item" & tblItems.Rows(i)("RecId"))
                        Exit Sub
                    End If
                End If
            Next

            'xu ly dac thu cho P & G, Novartis VN
            With GridTour.CurrentRow
                Select Case .Cells("CustShortName").Value
                    Case "PG VIETNAM", "PG INDOCHINA"
                        Dim blnServiceExist As Boolean = False
                        Dim blnTofRequired As Boolean = False
                        Dim blnLocalPO As Boolean = False

                        'Local PO
                        If ScalarToInt("SIR", "RecId", " where RcpId=" & .Cells("RecId").Value _
                                           & " and Fname='LOCAL PO' and FValue='Y'") > 0 Then
                            blnLocalPO = True
                        Else
                            blnLocalPO = False
                        End If

                        If blnLocalPO AndAlso .Cells("BillingBy").Value <> "Period" Then
                            MsgBox("Billing by must be Period for Local PO")
                            Exit Sub
                        End If

                        'TOF
                        If ScalarToInt("SIR", "RecId", " where RcpId=" & .Cells("RecId").Value _
                                           & " and Fname='TOF REQUIRED' and FValue='Y'") > 0 Then
                            blnTofRequired = True
                        Else
                            blnTofRequired = False
                        End If
                        For i = 0 To tblItems.Rows.Count - 1
                            If tblItems.Rows(i)("Service") <> "TransViet SVC Fee" _
                                AndAlso tblItems.Rows(i)("BookOnly") = 0 Then
                                blnServiceExist = True
                                Exit For
                            End If
                        Next
                        If blnTofRequired AndAlso .Cells("BillingBy").Value <> "Period" Then
                            MsgBox("Billing by must be Period for PG Travel Order Form")
                            Exit Sub
                        ElseIf Not blnTofRequired AndAlso blnServiceExist AndAlso .Cells("BillingBy").Value <> "CRD" _
                            AndAlso Not blnLocalPO Then
                            MsgBox("Billing by must be CRD for PG Serices without Travel Order Form")
                            Exit Sub
                        End If


                    Case "NOVARTIS VN"
                        If GridTour.CurrentRow.Cells("Contact").Value <> "ZPERSONAL" Then
                            If Not LinkTkt() Then
                                Exit Sub
                            End If
                        End If

                End Select

            End With

        End If

        strSql4Roe = "DuToanID=" & Me.GridTour.CurrentRow.Cells("recID").Value & " And status='OK' "
        tmpROE = ScalarToDec("Dutoan_Item", "top 1 ROE", strSql4Roe & " order by ROE")
        If tmpROE = 0 Then
            MsgBox("You Have Finished Pricing First", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        Me.LckLblFinalize.Visible = False

        tmpPrice = ItemPriceToTotalPrice(Me.GridTour.CurrentRow.Cells("recID").Value, "TTL")

        If Me.GridTour.CurrentRow.Cells("BillingBy").Value <> BillBy.BillByEvent Then
            '^_^20220929 mark by 7643 -b-
            'strSQL = TaoBanGhiRasEvent(strSQL, Me.GridTour.CurrentRow.Cells("recID").Value, Me.GridTour.CurrentRow.Cells("TCode").Value,
            '    "VND", tmpPrice, Me.GridTour.CurrentRow.Cells("BillingBy").Value _
            '    , Me.GridTour.CurrentRow.Cells("Contact").Value)
            '^_^20220929 mark by 7643 -e-
            '^_^20220929 modi by 7643 -b-
            If Not TaoBanGhiRasEvent(lstQuerries, Me.GridTour.CurrentRow.Cells("recID").Value, Me.GridTour.CurrentRow.Cells("TCode").Value,
                "VND", tmpPrice, Me.GridTour.CurrentRow.Cells("BillingBy").Value _
                    , Me.GridTour.CurrentRow.Cells("Contact").Value) Then
                MsgBox("Err. Unable to create RCP")
                Exit Sub
            End If
            '^_^20220929 modi by 7643 -e-
        End If
        'cmd.CommandText = strSQL  '^_^20220929 mark by 7643
UpdateDbHere:
        '^_^20220929 mark by 7643 -b-
        'Dim t As SqlClient.SqlTransaction = Conn.BeginTransaction
        'cmd.Transaction = t  
        'Try
        '    cmd.ExecuteNonQuery()
        '    t.Commit()
        '    blnFinalizeOK = True

        'Catch ex As Exception
        '    t.Rollback()
        '    MsgBox("Error Finalizing This Tour", MsgBoxStyle.Critical, msgTitle)
        '    Append2TextFile(ex.Message & vbNewLine & strSQL)
        'End Try
        '^_^20220929 mark by 7643 -e-
        '^_^20220929 modi by 7643 -b-
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            blnFinalizeOK = True
        Else
            MsgBox("Error Finalizing This Tour", MsgBoxStyle.Critical, msgTitle)
        End If
        '^_^20220929 modi by 7643 -e-
        If blnFinalizeOK AndAlso GridTour.CurrentRow.Cells("BillingBy").Value <> "Event" Then
            If Not CreateAopQueueNonAir(GridTour.CurrentRow.Cells("Tcode").Value) Then
                MsgBox("Unable to update AOP for " & GridTour.CurrentRow.Cells("Tcode").Value)
            ElseIf Not ExecuteNonQuerry("Update AopQueue set Status='OK' where Status='--' and TrxCode='" & GridTour.CurrentRow.Cells("Tcode").Value _
                                        & "'", Conn) Then
                MsgBox("Unable to update AOP for " & GridTour.CurrentRow.Cells("Tcode").Value)
            End If

            If IsInCustGrp("ABBOTT - SVC EMAIL", GridTour.CurrentRow.Cells("CustShortName").Value) Then
                'If GridTour.CurrentRow.Cells("CustShortName").Value = "ABBOTT ALSA_MICE" Then
                'Dim strDivision As String = ScalarToString("SIR", "Fvalue", "Status='OK' and FName='DIVISION' and RcpId=" & GridTour.CurrentRow.Cells("RecId").Value)
                Dim strEmail As String = TxtEmail.Text
                If strEmail = "" Then
                    strEmail = GetBookerEmail(GridTour.CurrentRow.Cells("CustId").Value, GridTour.CurrentRow.Cells("Contact").Value)
                End If
                strEmail = strEmail & ";" & ScalarToString("SIR", "top 1 Fvalue", "RCPID=" _
                                & GridTour.CurrentRow.Cells("RecId").Value _
                                & " And Status='OK' and Fname='EVENT OWNER EMAIL'")
                Dim ofd As New OpenFileDialog
                ofd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                ofd.Filter = "pdf files (*.pdf)|"
                ofd.ShowDialog()

                If ofd.FileName <> "" Then
                    Dim lstAttachments As New List(Of String)
                    Dim strDir As String = "\\dataserver\Lip\NonAir\" & GridTour.CurrentRow.Cells("RecId").Value

                    Dim strFilePath As String = strDir
                    Dim strBody As String = "Dear Anh/Chị" & vbNewLine _
                    & "GDS gửi Anh/Chị Service Confirmation & Bill nhà hàng/khách sạn. Anh/Chị vui lòng kiểm tra và phản hồi trong vòng 3 ngày làm việc kể từ hôm nay. Nếu sau 3 ngày GDS không nhận bất cứ phản hồi nào thì GDS sẽ tiến hành lên công nợ và xuất hóa đơn theo đúng số tiền trên Service Confirmation." _
                    & vbNewLine & "Thanks & best regards," & vbNewLine & myStaff.ShortName
                    Dim tblStoreFiles As DataTable = GetDataTable("Select i.RecId,a.ShortName,FileType from Images i left join Vendor a on i.intVal=a.RecId" _
                                                              & " where i.Status='OK' and i.StorePurpose=1" _
                                                              & " And Dutoan_TourId=" & GridTour.CurrentRow.Cells("RecId").Value & " order by a.ShortName, i.RecId")
                    For Each objRow As DataRow In tblStoreFiles.Rows
                        lstAttachments.Add(strDir & "\" & objRow("RecId") & "." & objRow("FileType"))
                    Next
                    lstAttachments.Add(ofd.FileName)
                    'CreateOutLookEmail(GridTour.CurrentRow.Cells("Tcode").Value, strBody, strEmail, lstAttachments, False)  '^_^20220922 mark by 7643
                    '^_^20220922 modi by 7643 -b-
                    Dim mCC As String

                    mCC = ScalarToString("MISC", "VAL2", "CAT='CustGroupName' and VAL='ABBOTT - SVC EMAIL' and status<>'XX' and city='" & myStaff.City & "'")
                    CreateOutLookEmail(GridTour.CurrentRow.Cells("Tcode").Value, strBody, strEmail, lstAttachments, False, mCC)
                    '^_^20220922 modi by 7643 -e-
                End If
                '    End If
            End If

        End If
        LoadgridTour()

    End Sub
    Private Function ItemPriceToTotalPrice(ByVal pTourID As Integer, ByVal pWhat As String) As Decimal
        Dim strSQL As String = String.Format("select * from DuToan_Item where DutoanID={0} And status='OK' and TrvlPay=0 and BookOnly=0 and CostOnly=0", pTourID)
        Dim dTable As DataTable = GetDataTable(strSQL)
        Dim Cost As Decimal = 0, Gia As Decimal, TVCharge As Decimal
        For i As Int16 = 0 To dTable.Rows.Count - 1
            Gia = dTable.Rows(i)("Cost") + dTable.Rows(i)("MU")
            If Not dTable.Rows(i)("isVATIncl") Then
                Gia = Gia + Gia * dTable.Rows(i)("VAT") / 100
            End If
            If InStr(dTable.Rows(i)("Service"), "SVC Fee") > 0 Then
                TVCharge = TVCharge + dTable.Rows(i)("qty") * dTable.Rows(i)("ROE") * Gia
            Else
                Cost = Cost + dTable.Rows(i)("qty") * dTable.Rows(i)("ROE") * Gia
            End If
        Next
        If pWhat = "TTL" Then
            Return TVCharge + Cost
        ElseIf pWhat = "TVC" Then
            Return TVCharge
        ElseIf pWhat = "COST" Then
            Return Cost
        End If
    End Function
    '^_^20220929 mark by 7643 -b-
    'Private Function TaoBanGhiRasEvent(ByVal pSQL As String, ByVal pTourID As Integer, ByVal pTCode As String _
    '                                   , ByVal pCurr As String, ByVal pPrice As Decimal _
    '                                   , ByVal pBilling As String, pBooker As String) As String
    '    Dim KQ As String = pSQL, RCPNo As String, LocalRCPID As Integer, tmpROE As Decimal = 1, ROEID As Integer
    '^_^20220929 mark by 7643 -e-
    '^_^20220929 modi by 7643 -b-
    Private Function TaoBanGhiRasEvent(ByRef pSQL As List(Of String), ByVal pTourID As Integer, ByVal pTCode As String _
                                       , ByVal pCurr As String, ByVal pPrice As Decimal _
                                       , ByVal pBilling As String, pBooker As String) As Boolean
        '^_^20220929 modi by 7643 -e-
        Dim RCPNo As String, LocalRCPID As Integer, tmpROE As Decimal = 1, ROEID As Integer
        Dim TKNO As String = GenPseudoTKT("EVT", "TV")

        Dim Fare As Decimal = ItemPriceToTotalPrice(pTourID, "COST")
        Dim TVCharge As Decimal = ItemPriceToTotalPrice(pTourID, "TVC")
        Dim RasFOP As String = ""


        If pBilling = BillBy.BillByCOD Then
            RasFOP = "DEB"
        ElseIf pBilling = BillBy.BillByCRD Then
            RasFOP = "CRD"
        Else
            RasFOP = MyCust.DelayType
        End If
        RCPNo = GenRCPNo(MySession.TRXCode, "0")
        ROEID = ForEX_12(myStaff.City, Now.Date, "USD", "RECID", "YY").Id
        If pCurr <> "VND" Then
            tmpROE = ScalarToDec("forEx", "BSR", "Recid=" & ROEID)
        End If
        If RCPNo <> "" Then
            LocalRCPID = ScalarToInt("RCP", "RecID", "RCPNO='" & RCPNo & "'")
            '^_^20220929 mark by 7643 -b-
            'KQ = KQ & "; Update Dutoan_Tour set RcpID=" & LocalRCPID & " where Recid=" & pTourID
            'KQ = KQ & "; Update RCP set CustID=" & MyCust.CustID & ", DeliveryStatus='" & pTCode &
            '    "', FstUser='AUT', CustType='" & MyCust.CustType & "', Counter='N-A', status='NA', SRV='S'," &
            '    " DOS='" & Format(Now, "dd-MMM-yy") & "', Stock='01', CustshortName='" & MyCust.ShortName &
            '     "', PrintedCustName='" & MyCust.FullName & "', PrintedCustAddrr='" & MyCust.Addr & "', PrintedTaxCode='" &
            '     MyCust.taxCode & "', Currency='" & pCurr & "', ROE=" & tmpROE & ", City='" & MySession.City &
            '     "', Location='TVH', TTLDue=" & pPrice & ", ROEID=" & ROEID & ", CA='" & pBooker.Replace("--", "") & "' where recid=" & LocalRCPID

            'Dim Amt As Decimal = pPrice

            'If MyCust.AdhType.Trim <> "" AndAlso pBooker <> "ZPERSONAL" Then
            '    Amt = Fare
            '    KQ = KQ & "; insert fop (fop, currency, roe, amount, RCPID, RCPNO, document, RMK, customerID, FstUser) values ('" &
            '            MyCust.AdhType & "','" & pCurr & "'," & tmpROE & "," & TVCharge & "," & LocalRCPID &
            '            ",'" & RCPNo & "','" & pTCode & "','" & pTourID.ToString & "'," & MyCust.CustID & ",'" & myStaff.SICode & "')"
            'End If

            'If Amt <> 0 Then
            '    KQ = KQ & "; insert fop (fop, currency, roe, amount, RCPID, RCPNO, document, RMK, customerID, Status, FstUser) values ('" &
            '            RasFOP & "','" & pCurr & "'," & tmpROE & "," & Amt & "," & LocalRCPID &
            '            ",'" & RCPNo & "','" & pTCode & "','" & pTourID.ToString & "'," & MyCust.CustID & ",'" &
            '            IIf(RasFOP = "CRD", "QQ", "OK") & "','" & myStaff.SICode & "')"
            'End If

            'If pBilling = BillBy.BillByBundle Then
            '    KQ = KQ & GetQryMCE2PSP(pTCode)
            'End If

            'Return KQ
            '^_^20220929 mark by 7643 -e-
            '^_^20220929 modi by 7643 -b-
            If LocalRCPID = 0 Then Return False
            pSQL.Add("Update Dutoan_Tour set RcpID=" & LocalRCPID & " where Recid=" & pTourID)
            pSQL.Add("Update RCP set CustID=" & MyCust.CustID & ", DeliveryStatus='" & pTCode &
                "', FstUser='AUT', CustType='" & MyCust.CustType & "', Counter='N-A', status='NA', SRV='S'," &
                " DOS='" & Format(Now, "dd-MMM-yy") & "', Stock='01', CustshortName='" & MyCust.ShortName &
                 "', PrintedCustName='" & MyCust.FullName & "', PrintedCustAddrr='" & MyCust.Addr & "', PrintedTaxCode='" &
                 MyCust.taxCode & "', Currency='" & pCurr & "', ROE=" & tmpROE & ", City='" & MySession.City &
                 "', Location='TVH', TTLDue=" & pPrice & ", ROEID=" & ROEID & ", CA='" & pBooker.Replace("--", "") & "' where recid=" & LocalRCPID)

            Dim Amt As Decimal = pPrice

            If MyCust.AdhType.Trim <> "" AndAlso pBooker <> "ZPERSONAL" Then
                Amt = Fare
                pSQL.Add("insert fop (fop, currency, roe, amount, RCPID, RCPNO, document, RMK, customerID, FstUser) values ('" &
                        MyCust.AdhType & "','" & pCurr & "'," & tmpROE & "," & TVCharge & "," & LocalRCPID &
                        ",'" & RCPNo & "','" & pTCode & "','" & pTourID.ToString & "'," & MyCust.CustID & ",'" & myStaff.SICode & "')")
            End If

            If Amt <> 0 Then
                pSQL.Add("insert fop (fop, currency, roe, amount, RCPID, RCPNO, document, RMK, customerID, Status, FstUser) values ('" &
                        RasFOP & "','" & pCurr & "'," & tmpROE & "," & Amt & "," & LocalRCPID &
                        ",'" & RCPNo & "','" & pTCode & "','" & pTourID.ToString & "'," & MyCust.CustID & ",'" &
                        IIf(RasFOP = "CRD", "QQ", "OK") & "','" & myStaff.SICode & "')")
            End If

            If pBilling = BillBy.BillByBundle Then
                pSQL.Add(GetQryMCE2PSP(pTCode))
            End If

            Return True
            '^_^20220929 modi by 7643 -e-
        Else
            'Return "Err. Unable to create RCP"  '^_^20220929 mark by 7643
            Return False  '^_^20220929 modi by 7643
        End If
    End Function
    Private Sub TxtUnitCost_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtUnitCost.LostFocus, txtMU.LostFocus, TxtTVSfAmount.LostFocus
        Dim aa As Decimal = CDec(Me.TxtUnitCost.Text)
        Me.TxtUnitCost.Text = Format(aa, "#,##0.0")
    End Sub
    Private Sub TxtUnitCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUnitCost.TextChanged, txtMU.TextChanged, TxtTVSfAmount.TextChanged
        Me.TxtTvSfPct.Text = 0
    End Sub
    Private Sub TxtSFPCT_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtTvSfPct.LostFocus
        Dim tmpSF As Decimal
        With GridSVC.CurrentRow
            tmpSF = (.Cells("Cost").Value + .Cells("MU").Value) * .Cells("Qty").Value / txtTvSfQty.Text
        End With

        'phai tim related item roi tinh % theo do
        If Not Me.GridSVC.CurrentRow.Cells("isVATIncl").Value Then
            tmpSF = tmpSF + tmpSF * Me.GridSVC.CurrentRow.Cells("VAT").Value / 100
        End If
        Me.TxtTVSfAmount.Text = tmpSF * CDec(Me.TxtTvSfPct.Text) / 100
    End Sub
    Private Function MakeQuotation(blnAutoSF As Boolean) As Boolean
        If updateDL_CDM_Quote(Me.GridTour.CurrentRow.Cells("recID").Value, "QuoteValid", "Quotation Validity") Then
            If UpdateROE_ItemPricing("Q") Then
                Dim blnSfExist As Boolean
                If ScalarToInt("Dutoan_Item", "top 1 RecId", "Status='ok'" _
                                            & " And Service ='TransViet SVC Fee'" _
                                            & " And DutoanId = " & GridTour.CurrentRow.Cells("RecId").Value) Then
                    blnSfExist = True
                End If

                If blnAutoSF Then
                    Dim strAutoServiceType As String = GetAutoSfType(GridTour.CurrentRow.Cells("RecId").Value)
                    If strAutoServiceType <> "N/A" Then
                        Dim lstAutoSf As List(Of String) = AutoSf(blnSfExist, strAutoServiceType)
                        If lstAutoSf.Count > 0 AndAlso UpdateListOfQuerries(lstAutoSf, Conn) Then
                            LoadGridSVC(GridTour.CurrentRow.Cells("RecId").Value)
                            MsgBox("AutoSF calculated! Check it before Quotation")
                            Return False
                        End If
                    Else
                        Return False
                    End If
                End If
            End If
            Dim myPath As String = Application.StartupPath
            Dim ItemList As String = ""
            If FirstClickQuote Then
                For i As Int16 = 0 To Me.GridSVC.RowCount - 1
                    Me.GridSVC.Item("Q", i).Value = Not Me.GridSVC.Item("Q", i).Value
                Next
                FirstClickQuote = False
                Dim MyAns As Integer = MsgBox("Are You OK To Make Quotation for Selected Items?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle)
                If MyAns = vbNo Then Return False
            End If
            For i As Int16 = 0 To Me.GridSVC.RowCount - 1
                If Me.GridSVC.Item("Q", i).Value Then
                    ItemList = ItemList & "," & Me.GridSVC.Item("RecID", i).Value
                End If
            Next
            If ItemList.Length > 2 Then
                ItemList = "(" & ItemList.Substring(1) & ")"
                cmd.CommandText = "update dutoan_Item set q=-1 where recid in " & ItemList
                cmd.ExecuteNonQuery()
            End If
            If myStaff.Counter = "ALL" Then
                If Me.GridTour.CurrentRow.Cells("CustShortName").Value = "ROCHE" Then
                    InHoaDon(myPath, "Quotation_Roche.xlt", "V", "Q", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, ItemList, "", "")
                ElseIf Me.GridTour.CurrentRow.Cells("CustShortName").Value = "MAST" Then
                    InHoaDon(myPath, "Quotation_MAST.xlt", "V", "Q", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, ItemList, "", "")
                Else
                    InHoaDon(myPath, "Quotation.xlt", "V", "Q", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, ItemList, "", "")
                End If

            Else
                If Me.GridTour.CurrentRow.Cells("CustShortName").Value = "ROCHE" Then
                    InHoaDon(myPath, "Quotation_Roche.xlt", "V", "Q", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, ItemList, "", "E")
                ElseIf Me.GridTour.CurrentRow.Cells("CustShortName").Value = "MAST" Then
                    InHoaDon(myPath, "Quotation_MAST.xlt", "V", "Q", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, ItemList, "", "E")
                Else
                    InHoaDon(myPath, "Quotation.xlt", "V", "Q", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, ItemList, "", "E")
                End If
            End If
        End If
    End Function
    Private Sub LblQuote_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblQuote.LinkClicked
        MakeQuotation(False)


    End Sub

    Private Sub TxtStart_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtStartDate.LostFocus, txtEventDate.LostFocus

    End Sub

    Private Sub GridTour_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridTour.CellContentDoubleClick
        'Me.TabControl1.SelectTab("TabPage2")
    End Sub


    Private Sub LblSettlement_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblSettlement.LinkClicked
        Dim myPath As String = Application.StartupPath
        InHoaDon(myPath, "QuyetToanTour.xlt", "V", "", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, "", "", "")
    End Sub
    Private Function updateDL_CDM_Quote(ByVal pTourID As Integer, ByVal pWhat As String, ByVal pMsg As String) As Boolean
        Dim fstUpdate As Date = ScalarToDate("DuToan_Tour", "FstUpdate", "RecID=" & pTourID)
        Dim tmpDeadLine As Date = ScalarToDate("DuToan_Tour", pWhat, "RecID=" & pTourID)
        If fstUpdate <> tmpDeadLine Then Return True
        fstUpdate = Now.Date.AddDays(4)
        tmpDeadLine = InputBox("Please Input " & pMsg, msgTitle, Format(fstUpdate, "dd-MMM-yy"))
        If tmpDeadLine < Now.Date Then Return False
        cmd.CommandText = "update DuToan_Tour set " & pWhat & "='" & tmpDeadLine & "' where recid=" & pTourID & "; " _
            & UpdateLogFile("dutoan_Tour", pWhat, pTourID, tmpDeadLine, "", "", "")
        cmd.ExecuteNonQuery()
        If pWhat = "DLCFM" And Me.GridTour.CurrentRow.Cells("Status").Value = "OK" Then
            cmd.CommandText = "update DuToan_Tour set status='OC' where RecID=" & pTourID
            cmd.ExecuteNonQuery()
        End If
        Return True
    End Function
    Private Sub LblSvcCfm_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblSvcCfm.LinkClicked
        Dim myAns As Int16 = MsgBox("Be Carefull. Click OK ONLY if you REALLY want to send confirmation to client" & vbCrLf & "Otherwise, click Cancel and hit Preview", MsgBoxStyle.Critical Or MsgBoxStyle.OkCancel, msgTitle)
        If myAns = vbCancel Then Exit Sub
        If Me.GridTour.CurrentRow.Cells("Traveller").Value = "" Then
            MsgBox("You must input traveler name!")
            Exit Sub
        End If
        If updateDL_CDM_Quote(Me.GridTour.CurrentRow.Cells("recID").Value, "DLCFM", "Dead Line to Confirm") Then
            UpdateROE_ItemPricing("S")
            Dim myPath As String = Application.StartupPath
            If myStaff.Counter = "ALL" Then
                If Me.GridTour.CurrentRow.Cells("CustShortName").Value = "ROCHE" Then
                    InHoaDon(myPath, "Quotation_Roche.xlt", "V", "S", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, "", "", "F")
                ElseIf Me.GridTour.CurrentRow.Cells("CustShortName").Value = "MAST" Then
                    InHoaDon(myPath, "Quotation_MAST.xlt", "V", "S", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, "", "", "F")
                Else
                    InHoaDon(myPath, "Quotation.xlt", "V", "S", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, "", "F")
                End If

            Else
                If Me.GridTour.CurrentRow.Cells("CustShortName").Value = "ROCHE" Then
                    InHoaDon(myPath, "Quotation_Roche.xlt", "V", "S", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, "", "", "E")
                ElseIf Me.GridTour.CurrentRow.Cells("CustShortName").Value = "MAST" Then
                    InHoaDon(myPath, "Quotation_MAST.xlt", "V", "S", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, "", "", "E")
                Else
                    InHoaDon(myPath, "Quotation.xlt", "V", "S", Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, "", "", "E")
                End If

            End If
        End If

    End Sub
    Private Sub LblSCat_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblSCat.VisibleChanged
        Me.LblSType.Visible = Me.LblSCat.Visible
        Me.CmbSCat.Visible = Me.LblSCat.Visible
        Me.cmbStype.Visible = Me.LblSCat.Visible
        Me.LblD1.Visible = Me.LblSCat.Visible
        Me.TxtD1.Visible = Me.LblSCat.Visible
        Me.LblUnit.Visible = Not Me.LblSCat.Visible
        Me.TxtUnit.Visible = Not Me.LblSCat.Visible
    End Sub
    Private Sub CmbVendor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbVendor.SelectedIndexChanged
        Dim wholeSaler As Integer

        If CmbVendor.SelectedValue IsNot Nothing Then
            wholeSaler = ScalarToInt("MISC", "RecID", "cat='NonAirWS' and val='" & Me.CmbVendor.SelectedValue.ToString.Trim & "'")
        End If

        Dim strDK As String = " where status='OK' and left(fullName,5)<>'(SAI)' "
        Try
            If wholeSaler = 0 Then strDK = strDK & " and vendorID=" & Me.CmbVendor.SelectedValue
            LoadCmb_VAL(Me.CmbSupplier, "select RecID as VAL, FullName as DIS from Supplier " & strDK)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TxtD2_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtD2.VisibleChanged
        Me.TxtT2.Visible = Me.TxtD2.Visible
    End Sub

    Private Sub TxtD1_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtD1.VisibleChanged
        Me.TxtT1.Visible = Me.TxtD1.Visible
    End Sub

    Private Sub TxtHTLSF_LostFocus(sender As Object, e As EventArgs) Handles TxtHTLSF.LostFocus
        Dim aa As Decimal = CDec(Me.TxtHTLSF.Text) * CDec(Me.TxtUnitCost.Text) / 100
        Me.txtMU.Text = Format(aa, "#,##0.0")
    End Sub

    Private Sub LblSaveTour_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblSaveTour.LinkClicked
        Dim strCmd As String ', strKeyMap = defineKeymapFromCCenter()
        Dim strRegService4Sanofi As String = String.Empty
        Dim strAutoSf As String = String.Empty
        Dim lstQuerries As New List(Of String)

        If Me.GridGO.Visible And InvalidSIR() Then Exit Sub

        If Not CheckVat(TxtVAT.Text) Then
            Exit Sub
        End If

        If CmbCust.Text.Contains("SANOFI") Then
            strRegService4Sanofi = ScalarToString("dutoan_item", "top 1 Service" _
                                  , "Status<>'xx' and Service='Registration'" _
                                  & " and DutoanId=" & GridTour.CurrentRow.Cells("RecId").Value)
            If DuplicateEventCode(Me.txtRefCode.Text, strRegService4Sanofi) Then
                MsgBox("Duplicate Event Code for Sanofi")
                Exit Sub
            End If
        ElseIf CmbCust.Text = "NVS_ MICE" Or CmbCust.Text = "SDZ_MICE" Then
            If txtRefCode.Text <> "" AndAlso txtRefCode.Text <> "NA" Then
                Dim strDupTCode As String = ScalarToString("Dutoan_Tour", "Tcode", "RecId<>" & GridTour.CurrentRow.Cells("RecId").Value _
                                                           & " and Status<>'XX' and RefNo='" & txtRefCode.Text & "'")
                If strDupTCode <> "" Then
                    MsgBox("Duplicate Event Code for " & strDupTCode)
                    Exit Sub
                End If
            End If
        End If

        If txtBooker.Text.ToUpper.Contains("ZPERS") And InStr("COD_CRD", Me.CmbBilling.Text) = 0 Then Exit Sub
        If CInt(Me.TxtPax.Text) = 0 Or Me.TxtBrief.Text = "" Or Me.CmbBilling.Text = "" Or
            Me.TxtStartDate.Value.Date > Me.txtEndDate.Value.Date _
            Or (Me.OptCWT.Checked And Me.cmbLocation.Text = "") Then
            MsgBox("Invalid NoOfPax or Brief or Billing or StartDate or PRNo or Empty Location for CWT client", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        If myStaff.SupOf = "" And Me.TxtStartDate.Value < Now.Date Then
            MsgBox("Invalid StartDate", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        If myStaff.SupOf = "" AndAlso TxtPax.Text <> mintNbrOfPax AndAlso Me.TxtStartDate.Value.Date < Now.Date Then
            MsgBox("NbrOfPax Change is NOT allowed after StartDate", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        If IsInCustGrp("ABBOTT FF – PLAN CODE", CmbCust.Text) AndAlso txtBooker.Text <> "ZPERSONAL" Then
            If Not CheckPlanCode4Abbott(txtRefCode.Text, GridTour.CurrentRow.Cells("RecId").Value) Then
                Exit Sub
            End If
        End If
        lstQuerries.Add(UpdateLogFile("Dutoan_tour", "Edit", Me.GridTour.CurrentRow.Cells("RecID").Value, txtCostCenter.Text,
                Me.TxtEmail.Text, Me.TxtBrief.Text.Replace("'", ""), Me.cmbLocation.Text, txtBooker.Text, Me.cmbTraveler.Text, Me.TxtIONo.Text,
                Me.txtRefCode.Text, Me.TxtOwner.Text, Me.TxtPRNo.Text, Me.CmbBUAdmin.Text, txtWindowId.Text))
        '^_^20220729 add by 7643 -b-
        lstQuerries.Add("insert DuToan_TourHistory select *,'" & myStaff.SICode & "',getdate() from DuToan_Tour " &
                        "where RecId=" & GridTour.CurrentRow.Cells("RecID").Value)
        '^_^20220729 add by 7643 -e-
        strCmd = String.Format("update Dutoan_Tour set contact='{0}', Traveller='{1}', IONO='{2}', RefNo='{3}',Owner='{4}'," &
            "PRNo='{5}', BUAdmin='{6}', Dept='{7}', Location='{8}', KeyMap='{9}'" _
            & ", Email='{10}', Brief='{11}' , WindowId='{12}', FileId='{13}', WzAir='{14}', Pax='{15}', VatPct='{16}' ",
            txtBooker.Text, Me.cmbTraveler.Text.Replace("--", ""),
            Me.TxtIONo.Text.Replace("--", ""), Me.txtRefCode.Text.Replace("--", ""), Me.TxtOwner.Text.Replace("--", ""),
            Me.TxtPRNo.Text.Replace("--", ""), Me.CmbBUAdmin.Text.Replace("--", ""), Me.CmbDept.Text.Replace("--", ""),
            IIf(Me.OptCWT.Checked, "N/A", Me.cmbLocation.Text.Replace("--", "")), txtCostCenter.Text, Me.TxtEmail.Text.Replace("--", ""),
            Me.TxtBrief.Text.Replace("--", "").Replace("'", ""), txtWindowId.Text.Trim, txtFileId.Text.Trim, chkWzAir.Checked, TxtPax.Text, txtVatPct.Text)

        If myStaff.SupOf <> "" Then
            strCmd = String.Format(" {0}, Sdate='{1}', EDate='{2}', EventDate='{3}' ", strCmd, Me.TxtStartDate.Value.Date, Me.txtEndDate.Value.Date, Me.txtEventDate.Value.Date)
            If Me.GridTour.CurrentRow.Cells("Status").Value <> "RR" Then
                strCmd = String.Format("{0}, BillingBy='{1}'", strCmd, Me.CmbBilling.Text.Replace("--", ""))
            End If

            If txtBooker.Text <> "ZPERSONAL" AndAlso TxtStartDate.Value.Date <> mdteSelectedSdate Then
                Dim strAutoServiceType As String = GetAutoSfType(GridTour.CurrentRow.Cells("RecID").Value)
                If strAutoServiceType <> "N/A" Then
                    lstQuerries.AddRange(AutoSf(True, strAutoServiceType))
                End If
            End If
        End If

        strCmd = strCmd & " where RecID=" & Me.GridTour.CurrentRow.Cells("RecID").Value
        lstQuerries.Add(strCmd)


        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Unable to update TCode")
            Exit Sub
        End If

        If Me.GridGO.Visible Then
            InsertSIR(Me.GridTour.CurrentRow.Cells("RecID").Value, True)
        End If

        LoadgridTour()
    End Sub
    Private Function GetAutoSfType(intDutoanid As Integer) As String
        Dim strResult As String = ScalarToString("Sir", "Fvalue", "Status='OK' and Prod='NonAir'" _
                                                & " and FName='LeadTime' And Rcpid=" & GridTour.CurrentRow.Cells("RecId").Value)
        If strResult Is Nothing Then
            Return ""
        ElseIf strResult.Contains("URGENT") Then

            strResult = "URGENT"
        End If
        Return strResult
    End Function
    Private Function AutoSf(blnEdit As Boolean, strServiceType As String) As List(Of String)
        Dim lstAutoSfs As New List(Of String)
        Dim decVatPct4SF = GetVatPct(TxtStartDate.Value)
        Dim decAutoSf As Decimal = ScalarToDec("MISC", "top 1 intDec", "Cat='AutoSf' and Status='ok' and intVal=" _
                                                        & GridTour.CurrentRow.Cells("CustId").Value _
                                                        & " and City='" & myStaff.City _
                                                        & "' and Details='" & strServiceType & "'")

        If decAutoSf > 0 Then
            Dim decTtl4Services As Decimal = ScalarToDec("Dutoan_Item", "isnull(Sum(TtlToPax),0)", "Status='ok'" _
                                            & " And Service not in ('TransViet SVC Fee','Bank Fee','Merchant Fee')" _
                                            & " And DutoanId = " & GridTour.CurrentRow.Cells("RecId").Value)
            Dim decNewSF As Decimal = Math.Round(decTtl4Services * decAutoSf / 100, 0)
            Dim tblOldSf As DataTable = GetDataTable("select * from dutoan_item where Status='ok'" _
                                                    & " And Service='TransViet SVC Fee' and DutoanId=" _
                                                    & GridTour.CurrentRow.Cells("RecId").Value)

            If blnEdit Then


                If tblOldSf.Rows.Count > 0 Then
                    If decNewSF = 0 Or decNewSF <> tblOldSf.Rows(0)("TtlToPax") Then
                        lstAutoSfs.Add(ChangeStatus_ByID("Dutoan_item", "XX", tblOldSf.Rows(0)("RecId")))
                        If decNewSF > 0 Then
                            lstAutoSfs.Add("insert into DuToan_Item (Service, CCurr, Unit, Qty, Cost, Supplier, VendorID, Vendor" _
                                        & ", FstUser, PmtMethod, isVATIncl, " _
                                        & " VAT, DuToanID, SVCDate,  RelatedItem" _
                                        & ", ROE, SupplierID,BookOnly,PaxName,ZeroFeeReason,Vat4Vendor)" _
                                        & " SELECT Service, CCurr, Unit, Qty," & decNewSF & ", Supplier, VendorID, Vendor" _
                                        & ",'" & myStaff.SICode & "', PmtMethod, isVATIncl " _
                                        & ", VAT, DuToanID, SVCDate,  RelatedItem" _
                                        & ", ROE, SupplierID,BookOnly,PaxName,ZeroFeeReason ,Vat4Vendor " _
                                        & " from Dutoan_item where RecId=" _
                                        & tblOldSf.Rows(0)("RecId"))

                        End If

                    End If
                End If
            ElseIf decTtl4Services > 0 Then
                lstAutoSfs.Add("insert into DuToan_Item (Service, CCurr, Unit, Qty, Cost, Supplier, VendorID, Vendor" _
                                        & ", FstUser, PmtMethod, isVATIncl, " _
                                        & " VAT, DuToanID, SVCDate,  RelatedItem" _
                                        & ", ROE, SupplierID,BookOnly,PaxName,ZeroFeeReason,Vat4Vendor)" _
                                        & " values ('TransViet SVC Fee', 'VND', 'Service', 1," & decNewSF _
                                        & ", '',2, 'TransViet','" & myStaff.SICode & "', 'PSP','True'  " _
                                        & "," & decVatPct4SF & "," & GridTour.CurrentRow.Cells("RecId").Value _
                                        & ",'" & CreateFromDate(GridTour.CurrentRow.Cells("Sdate").Value) & "',0" _
                                        & ", 1, 2,'False','',''," & decVatPct4SF & ")")

            End If

        End If
        Return lstAutoSfs
    End Function
    Private Sub ChkPastOnly_Click(sender As Object, e As EventArgs) Handles ChkPastOnly.Click _
        , ChkSelectedCustOnly.Click
        LoadgridTour()
    End Sub

    Private Sub LblPreview_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblPreview.LinkClicked
        Dim myPath As String = Application.StartupPath
        Dim strViewType As String

        If GridTour.CurrentRow.Cells("Status").Value = "RR" Then
            strViewType = "S"
        Else
            strViewType = "Q"
        End If
        If Me.GridTour.CurrentRow.Cells("CustShortName").Value = "ROCHE" Then
            InHoaDon(myPath, "Quotation_Roche.xlt", "V", strViewType, Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, "", "", "V")
        ElseIf Me.GridTour.CurrentRow.Cells("CustShortName").Value = "MAST" Then
            InHoaDon(myPath, "Quotation_MAST.xlt", "V", strViewType, Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, "", "", "V")
        Else
            InHoaDon(myPath, "Quotation.xlt", "V", strViewType, Now.Date, Now.Date, Me.GridTour.CurrentRow.Cells("recID").Value, "", "", "V")
        End If
    End Sub

    Private Sub ChkXXOnly_CheckedChanged(sender As Object, e As EventArgs) Handles ChkXXOnly.CheckedChanged
        If Me.ChkXXOnly.Checked Then
            Me.ChkXXOnly.Left = 496
            Me.ChkXXOnly.Top = 480
            Me.GridSVC.BringToFront()
        Else
            Me.ChkXXOnly.Left = 43
            Me.ChkXXOnly.Top = 278
        End If
        'LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus.SelectedIndexChanged
        If Not mblnFirstLoadCompleted Then
            Exit Sub
        End If
        If Me.cmbStatus.Text = "CXLD" Then
            Me.TabControl1.Enabled = False
            'Me.GridTour.Height = 470
            Me.ChkXXOnly.Checked = True
            Me.TabControl1.SelectTab("TabCosting")
        Else
            Me.TabControl1.Enabled = True
            'Me.GridTour.Height = 454
            Me.ChkXXOnly.Checked = False
        End If
        LoadgridTour()
    End Sub
    Private Sub LblDocSent_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblDocSent.LinkClicked
        If ScalarToInt("Dutoan_Tour", "Status", "RecId=" & GridTour.CurrentRow.Cells("Status").Value) Then
            MsgBox("Unable to change status for Finalized Tcode!")
            Exit Sub
        End If
        cmd.CommandText = ChangeStatus_ByID("Dutoan_Tour", "OD", Me.GridTour.CurrentRow.Cells("recID").Value) &
            "; " & UpdateLogFile("dutoan_Tour", "DocSent", Me.GridTour.CurrentRow.Cells("recID").Value, "", "", "", "")
        cmd.ExecuteNonQuery()
        LoadgridTour()
    End Sub
    Private Sub LckLblUndoFinalize_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LckLblUndoFinalize.LinkClicked
        If MsgBox("Are You Sure To Undo Finalize", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle) = vbNo Then Exit Sub

        Dim lstQuerries As New List(Of String)
        Dim DKDocument As String = " And (Document = '" & Me.GridTour.CurrentRow.Cells("TCode").Value & "' or RMK like '%" &
                    Me.GridTour.CurrentRow.Cells("TCode").Value & "%')"
        Dim dTbl As DataTable
        dTbl = GetDataTable("Select * from FOP where RcpId<>0 and status in ('OK','QQ') and RcpId=" _
                            & GridTour.CurrentRow.Cells("RCPID").Value)
        'dTbl = GetDataTable("Select * from FOP where status in ('OK','QQ') and RMK like '%" &
        '                                     Me.GridTour.CurrentRow.Cells("RecID").Value & "%'" & DKDocument _
        '                                     & " order by FOP ")
        lstQuerries.Add(ChangeStatus_ByID("Dutoan_Tour", "OK", Me.GridTour.CurrentRow.Cells("recID").Value))

        If Me.GridTour.CurrentRow.Cells("BillingBy").Value <> BillBy.BillByEvent Then
            If dTbl.Rows.Count > 0 Then
                If VeCongNoDaInv(dTbl.Rows(0)("RCPID")) Then
                    MsgBox("This TRX has Been Invoiced And Cant Be Changed", MsgBoxStyle.Critical, msgTitle)
                    Exit Sub
                End If
                If dTbl.Rows(0)("FOP") = "CRD" And dTbl.Rows(0)("Status") = "OK" Then
                    MsgBox("This TRX Has Been Paid By CC And Cant Be Changed", MsgBoxStyle.Critical, msgTitle)
                    Exit Sub
                End If


                lstQuerries.Add("update RCP set status='XX' where counter='N-A' and recid=" & dTbl.Rows(0)("RCPID"))
                lstQuerries.Add(ChangeStatus_ByDK("FOP", "XX", "RcpId<>0 and RcpID=" & dTbl.Rows(0)("RCPID")))
                lstQuerries.Add("Update Dutoan_Tour set RcpID=0 where Recid=" & GridTour.CurrentRow.Cells("recID").Value)  '^_^20221221 add by 7643

                With GridTour.CurrentRow
                    lstQuerries.Add("insert ActionLog (TableName, doWhat, ActionBy, F1, F2)" _
                                & " values ('DuToan_Tour','Unfinalize','" & myStaff.SICode _
                                & "','" & .Cells("TCode").Value & "','" _
                                & Format(.Cells("LstUpdate").Value, "dd MMM yy HH:mm:ss") & "')")
                End With


                If Me.GridTour.CurrentRow.Cells("BillingBy").Value = BillBy.BillByBundle Then
                    lstQuerries.Add("Update FOP set FOP='MCE' where status='OK' and RMK='TT.INV.BDL' " & DKDocument)
                End If
                '^_^20221221 add by 7643 -b-
            ElseIf Not String.IsNullOrEmpty(ScalarToString("FOP", "RMK", " Document='" & Me.GridTour.CurrentRow.Cells("TCode").Value & "' and status='OK' and fop <>'MCE' and rmk <>'MCE'")) Then
                lstQuerries.Add(ChangeStatus_ByDK("FOP", "XX", "Document='" & GridTour.CurrentRow.Cells("TCode").Value & "' and status='OK' and fop <>'MCE' and rmk <>'MCE'"))
                lstQuerries.Add("Update Dutoan_Tour set RcpID=0 where Recid=" & GridTour.CurrentRow.Cells("recID").Value)

                With GridTour.CurrentRow
                    lstQuerries.Add("insert ActionLog (TableName, doWhat, ActionBy, F1, F2)" _
                                & " values ('DuToan_Tour','Unfinalize','" & myStaff.SICode _
                                & "','" & .Cells("TCode").Value & "','" _
                                & Format(.Cells("LstUpdate").Value, "dd MMM yy HH:mm:ss") & "')")
                End With


                If Me.GridTour.CurrentRow.Cells("BillingBy").Value = BillBy.BillByBundle Then
                    lstQuerries.Add("Update FOP set FOP='MCE' where status='OK' and RMK='TT.INV.BDL' " & DKDocument)
                End If
                '^_^20221221 add by 7643 -e-
            End If

        End If
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            LoadgridTour()
        End If

    End Sub

    Private Sub TxtEmail_Enter(sender As Object, e As EventArgs) Handles TxtEmail.Enter
        If Strings.Left(Me.TxtEmail.Text, 1) = "_" Then
            Me.TxtEmail.Text = ""
            Me.TxtEmail.ForeColor = Color.Black
        End If
    End Sub


    Private Sub LblAddSF_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblAddSF.LinkClicked
        If GridSVC.CurrentRow.Cells("Recid").Value > 0 Then
            Dim intSfCount As Integer = ScalarToInt("Dutoan_item ", "count(*)", "Status='OK'" _
                                                & " and Service='TransViet SVC Fee' and RelatedItem=" _
                                                & GridSVC.CurrentRow.Cells("Recid").Value)
            If intSfCount > 0 Then
                MsgBox("Service Fee exist! Unable to add more service fee!")
                Exit Sub
            End If
        End If

        Me.GrpCost.Tag = "SVC"
        Me.GrpCost.Enabled = False
        Me.chkBookOnly.Checked = False
    End Sub

    Private Sub GrpCost_EnabledChanged(sender As Object, e As EventArgs) Handles GrpCost.EnabledChanged
        Me.Label7.Visible = Not Me.GrpCost.Enabled
        Me.TxtTvSfPct.Visible = Not Me.GrpCost.Enabled
        Me.TxtTVSfAmount.Visible = Not Me.GrpCost.Enabled
        txtTvSfQty.Visible = Not Me.GrpCost.Enabled
        lblTvSfQty.Visible = Not Me.GrpCost.Enabled
    End Sub

    Private Sub TxtPRNo_Enter(sender As Object, e As EventArgs) Handles TxtPRNo.Enter
        If Strings.Left(Me.TxtPRNo.Text, 1) = "_" Then
            Me.TxtPRNo.Text = ""
            Me.TxtPRNo.ForeColor = Color.Black
        End If
    End Sub
    Private Sub TxtOwner_Enter(sender As Object, e As EventArgs) Handles TxtOwner.Enter
        If Me.TxtOwner.Text.Contains("_") Then
            Me.TxtOwner.Text = ""
            Me.TxtOwner.ForeColor = Color.Black
        End If
    End Sub
    Private Function defineTCList(wzAmt As Boolean) As String
        Dim KQ As String = ""
        For i As Int16 = 0 To Me.GridPmtRQTC.RowCount - 1
            If Me.GridPmtRQTC.Item("Q", i).Value Then
                'If Not KQ.Contains(GridPmtRQTC.Item("TCode", i).Value) Then
                KQ = KQ & "_" & Me.GridPmtRQTC.Item("TCode", i).Value
                'End If

                If wzAmt Then
                    KQ = KQ & ": " & Format(Me.GridPmtRQTC.Item("BeingPaidThisTime", i).Value, "#,##0")
                End If
            End If
        Next
        If KQ.Length > 2 Then KQ = KQ.Substring(1)
        Return KQ
    End Function
    Private Function AddTcode2List(ByRef lstTcodes As List(Of String)) As Boolean
        lstTcodes.Clear()

        For i As Int16 = 0 To Me.GridPmtRQTC.RowCount - 1
            If Me.GridPmtRQTC.Item("Q", i).Value Then
                lstTcodes.Add(GridPmtRQTC.Item("Tcode", i).Value)
            End If
        Next
        Return True
    End Function
    Private Function DefineTCList_Combine(pPmtID As Integer, wzAmt As Boolean) As String
        Dim dtbl As DataTable = GetDataTable("select TCode, sum(vnd) as Amt from dutoan_pmt where status='OK' and pmtID=" & pPmtID & " group by TCode")
        Dim KQ As String = ""
        For i As Int16 = 0 To dtbl.Rows.Count - 1
            KQ = KQ & "_" & dtbl.Rows(i)("Tcode")
            If wzAmt Then
                KQ = KQ & ": " & Format(dtbl.Rows(i)("Amt"), "#,##0")
            End If
        Next
        Return KQ.Substring(1)
    End Function
    Private Sub LblPrintPmtRQ_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblPrintPmtRQ.LinkClicked
        If HasNewerVersion_R12(Application.ProductVersion) Or SysDateIsWrong(Conn) Then Exit Sub
        Dim vendorID As Integer, ShortName As String = "", MoTa As String, TCode As String, PmtID As Integer, RMKNoiBo As String
        Dim CAT As String = "", myPath As String = Application.StartupPath
        Dim AccountName As String = "", AccountNumber As String = "", BankName As String = ""
        Dim BankAddress As String = "", swift As String = "", PayeeAccID As Integer = 0
        Dim strTvc As String = String.Empty
        Dim lstTcodes As New List(Of String)

        If CDec(Me.txtTraLanNay.Text) = 0 Then Exit Sub
        If Me.txtTraLanNay.Text <> Format(XacDinhAmtTraLanNay(), "#,##0") Then
            MsgBox("Item selection is not matched with Requested Payment amount. Please refresh it!")
            Exit Sub
        End If
        ShortName = Me.CmbPmtRQVendor.Text
        vendorID = Me.CmbPmtRQVendor.SelectedValue
        If Not isLogicAmt() Then Exit Sub
        TCode = defineTCList(False)
        AddTcode2List(lstTcodes)

        RMKNoiBo = defineTCList(True)

        If TCode.Length > 256 Or RMKNoiBo.Length > 512 Then
            MsgBox("Too many items seletected! Please split into 2 payments")
            Exit Sub
        End If

        Me.txtTraLanNay.Text = Format(XacDinhAmtTraLanNay(), "#,##0")
        If CDec(Me.txtTraLanNay.Text) < 0 Then
            MoTa = "Hoan Ung " & TCode & ". So phieu thu: " & InputBox("Please Enter [PhieuThu] No.", msgTitle)
            Me.CmbPmtRQFOP.Text = "CSH"

        ElseIf ScalarToInt("Misc", "RecId", "Status='OK' and CAT='CustNameInGroup'" _
                            & " And VAL='GDS' and intVal=" _
                            & GridTour.CurrentRow.Cells("CustId").Value) > 0 _
                            AndAlso CmbPmtRQFOP.Text <> "CSH" Then
            MoTa = "Phan Phoi Toan Cau Ttoan " & TCode
            strTvc = "GDS"
        Else
            MoTa = "TransViet TToan " & TCode
            strTvc = "TVTR"
        End If

        'If Me.GridAcct.Rows.Count = 0 Then
        '    MsgBox("No Bank Details. Please Update ", MsgBoxStyle.Critical, msgTitle)
        '    If Me.CmbPmtRQFOP.Text = "BTF" Then
        '        Exit Sub
        '    End If
        'Else
        AccountName = Me.GridAcct.CurrentRow.Cells("AccountName").Value
        AccountNumber = Me.GridAcct.CurrentRow.Cells("AccountNumber").Value
        BankName = Me.GridAcct.CurrentRow.Cells("BankName").Value
        BankAddress = Me.GridAcct.CurrentRow.Cells("BankAddress").Value
        swift = Me.GridAcct.CurrentRow.Cells("swift").Value
        PayeeAccID = Me.GridAcct.CurrentRow.Cells("RecID").Value
        CAT = ScalarToString("Vendor", "CAT", "RecID=" & vendorID)
        CAT = "[" & CAT & "] "

        If String.IsNullOrEmpty(AccountNumber) AndAlso CmbPmtRQFOP.Text = "BTF" Then
            MsgBox("You Must Specify Payee Account", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If


        Dim strVendorShortName As String = Me.CmbPmtRQVendor.Text
        If strVendorShortName = "SO Y TE" Then
            strVendorShortName = strVendorShortName & "-" & ScalarToString("dutoan_tour", "Location" _
                                                                           , "where status<>'xx' and tcode='" & lstTcodes(0) & "'")
            If CmbPmtRQFOP.Text <> "CSH" Then
                MsgBox("You must use CSH for SO Y TE!")
                Exit Sub
            End If
        ElseIf strVendorShortName = "CPN TRANSVIET" Then
            Dim strLocation As String = ScalarToString("dutoan_tour", "Location", "where status<>'xx' and tcode='" & lstTcodes(0) _
                                                       & "' and RecId in (Select DutoanId from Dutoan_Item where Status='OK' and Supplier='SO Y TE')")
            If strLocation <> "" Then
                strVendorShortName = strVendorShortName & "-" & strLocation
            End If

        End If

        InHoaDon(myPath, "DeNghiThanhToan.xlt", "V", strVendorShortName, OverPay, Now, vendorID, myStaff.SICode, MoTa,
                 "VND " & Me.txtTraLanNay.Text,, "VND " & txtBankFee.Text)
        Dim myAns As Int16 = MsgBox("Are You OK With the PrintOut?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle)
        If myAns = vbNo Then Exit Sub

        cmd.CommandText = "Insert into UNC_Payments (PayerAccountID, AccountName, AccountNumber, BankName, BankAddress" _
            & ", Curr, Amount, Description, swift, Charge, shortname, InvNo, TRX_TC" _
            & ", FstUser, Status, FOP, PayeeAccountID,Counter,Tvc) values (" _
            & " 0, @AccountName, @AccountNumber, @BankName, @BankAddress, 'VND', @Amount, @Description, @swift, 'IPOB', @shortname," &
            "'', @TRX_TC, @FstUser,'QQ',@FOP, @PayeeAccountID,@Counter,@Tvc);SELECT SCOPE_IDENTITY() AS [RecID]"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar).Value = AccountName
        cmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar).Value = AccountNumber
        cmd.Parameters.Add("@BankName", SqlDbType.NVarChar).Value = BankName
        cmd.Parameters.Add("@BankAddress", SqlDbType.NVarChar).Value = BankAddress
        cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = CDec(Me.txtTraLanNay.Text)
        cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = MoTa
        cmd.Parameters.Add("@swift", SqlDbType.VarChar).Value = swift
        cmd.Parameters.Add("@shortname", SqlDbType.VarChar).Value = ShortName
        cmd.Parameters.Add("@FOP", SqlDbType.VarChar).Value = Me.CmbPmtRQFOP.Text
        cmd.Parameters.Add("@TRX_TC", SqlDbType.VarChar).Value = "PPD: NA"
        cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        cmd.Parameters.Add("@PayeeAccountID", SqlDbType.Int).Value = PayeeAccID
        cmd.Parameters.Add("@Counter", SqlDbType.VarChar).Value = myStaff.Counter
        cmd.Parameters.Add("@Tvc", SqlDbType.VarChar).Value = strTvc
        PmtID = cmd.ExecuteScalar
        cmd.CommandText = ""
        cmd.Parameters.Clear()
        For i As Int16 = 0 To Me.GridPmtRQTC.RowCount - 1
            If Me.GridPmtRQTC.Item("Q", i).Value Then
                cmd.CommandText = cmd.CommandText & "; insert Dutoan_pmt (DutoanID, TCode, ItemID, VendorID, Vendor, PmtID, VND, FstUser)" &
                    " values (" & Me.GridPmtRQTC.Item("DuToanID", i).Value & ",'" & Me.GridPmtRQTC.Item("TCode", i).Value & "'," &
                    Me.GridPmtRQTC.Item("RecID", i).Value & "," & Me.CmbPmtRQVendor.SelectedValue & ",'" &
                    Me.CmbPmtRQVendor.Text & "'," & PmtID & "," & Me.GridPmtRQTC.Item("BeingPaidThisTime", i).Value &
                    ",'" & myStaff.SICode & "')"
            End If
        Next
        cmd.CommandText = cmd.CommandText.Substring(1)
        cmd.ExecuteNonQuery()

        For i As Int16 = 0 To Me.GridPmtRQTC.RowCount - 1
            If Me.GridPmtRQTC.Item("Q", i).Value Then
                RefreshSoDaTra(Me.GridPmtRQTC.Item("RecID", i).Value)
            End If
        Next
        TCode = DefineTCList_Combine(PmtID, False)
        RMKNoiBo = DefineTCList_Combine(PmtID, True)
        If CDec(Me.txtTraLanNay.Text) < 0 Then
            RMKNoiBo = "Hoan Ung " & RMKNoiBo & ". So phieu thu: " & InputBox("Please Enter [PhieuThu] No.", msgTitle)
            Me.CmbPmtRQFOP.Text = "CSH"
        Else
            MoTa = "TransViet TToan " & TCode
        End If
        cmd.CommandText = "update UNC_Payments set Description='" & MoTa.Replace("--", "") & "', RMKNoibo='" &
            RMKNoiBo.Replace("--", "") & "' where recid=" & PmtID
        cmd.ExecuteNonQuery()
        InHoaDon(myPath, "DeNghiThanhToan.xlt", "O", CAT & strVendorShortName, OverPay, Now, vendorID, myStaff.SICode, RMKNoiBo, "VND " & Me.txtTraLanNay.Text, PmtID)
        Me.LblPreview.Visible = True
        If Me.CmbPmtRQFOP.Text = "CSH" Then
            If MsgBox("Wanna Order a Messenger To Fulfill This Payment?", MsgBoxStyle.Question Or vbYesNo, msgTitle) = vbYes Then
                Book_a_MSGR(myStaff.SICode, myStaff.PSW, "N/A", IIf(TCode.Length > 16, "PmtByVendor", TCode), PmtID)
            End If
        End If
    End Sub
    Private Sub RefreshSoDaTra(pItemID As Integer)
        Dim DaTra As Decimal = ScalarToDec("dutoan_pmt", "isnull(sum(VND),0)", "ItemID=" & pItemID & " and status<>'XX'")
        cmd.CommandText = "Update Dutoan_item set VNDPaid=" & DaTra & " where RecID=" & pItemID
        cmd.ExecuteNonQuery()
    End Sub
    Private Sub LblQCSF_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblQCSF.LinkClicked
        Me.GrpCost.Tag = "QC"
        Me.GrpCost.Enabled = False
        Dim CT As String, MealTime As Integer = CInt(Format(Me.GridSVC.CurrentRow.Cells("SVCDate").Value, "HH"))
        CT = Me.GridSVC.CurrentRow.Cells("City").Value
        Dim QCFee As Decimal = ScalarToDec("MISC", "VAL1", "CAT='QCFEE' and VAL='" & CT & "'")
        Dim OverTime As Decimal = ScalarToDec("MISC", "VAL2", "CAT='QCFEE' and VAL='" & CT & "'")
        If MealTime > 17 Then QCFee = QCFee + OverTime
        Me.TxtTVSfAmount.Text = Format(QCFee, "#,##0")
    End Sub
    'Private Sub LblAddSF_VisibleChanged(sender As Object, e As EventArgs) Handles LblAddSF.VisibleChanged
    '    Try
    '        If MyCust.ShortName.Contains("SANOFI") OrElse MyCust.ShortName.Contains("TEST") Then
    '            Me.LblQCSF.Visible = Me.LblAddSF.Visible
    '        Else
    '            Me.LblQCSF.Visible = False
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Function isLogicAmt() As Boolean
        Dim TotalToVendor As Decimal, DaTra As Decimal
        Dim LanNay As Decimal, MyAns As Int16
        OverPay = "01-jan-2000"
        For i As Int16 = 0 To Me.GridPmtRQTC.RowCount - 1
            If Me.GridPmtRQTC.Item("Q", i).Value Then
                LanNay = Me.GridPmtRQTC.Item("BeingPaidThisTime", i).Value
                'TotalToVendor = Me.GridPmtRQTC.Item("toVendorWoTax", i).Value
                TotalToVendor = Me.GridPmtRQTC.Item("ttltovendor", i).Value
                DaTra = Me.GridPmtRQTC.Item("VNDPaid", i).Value
                If LanNay > TotalToVendor - DaTra Then
                    MyAns = MsgBox("Invalid Amount. Wanna Correct Your Input?", MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, msgTitle)
                    If MyAns = vbYes Then Return False
                    OverPay = "31-Dec-2000"
                End If
            End If
        Next
        Return True
    End Function
    Private Sub LblAddAdj_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblAddAdj.LinkClicked
        If CDec(Me.TxtAmtAdj.Text) = 0 Or Me.TxtRMKAdj.Text = "" Then Exit Sub
        cmd.CommandText = "insert DuToan_Item (Service, CCurr, Status, Qty, Cost, Supplier, VendorID, Vendor, FstUser, PmtMethod, isVATIncl, " &
            " VAT, DuToanID, Brief, SupplierRMK, MU, SVCDate, City, BookOnly) Values (@Service, @CCurr, 'TV', @Qty, @Cost, @Supplier, @VendorID, " &
            "@Vendor, @FstUser, @PmtMethod, @isVATIncl, @VAT, @DuToanID, @Brief, @SupplierRMK, @MU, @SVCDate, @City, @BookOnly) "
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@Service", SqlDbType.VarChar).Value = "ADJ"
        cmd.Parameters.Add("@CCurr", SqlDbType.VarChar).Value = Me.CmbCurrAdj.Text
        cmd.Parameters.Add("@Qty", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = CDec(Me.TxtAmtAdj.Text)
        cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = Me.CmbVendorAdj.Text
        cmd.Parameters.Add("@Vendor", SqlDbType.VarChar).Value = Me.CmbVendorAdj.Text
        cmd.Parameters.Add("@VendorID", SqlDbType.Int).Value = Me.CmbVendorAdj.SelectedValue
        cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        cmd.Parameters.Add("@PmtMethod", SqlDbType.VarChar).Value = "PSP"
        cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = "ALL"
        cmd.Parameters.Add("@isVATIncl", SqlDbType.Bit).Value = 1
        cmd.Parameters.Add("@VAT", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@DuToanID", SqlDbType.Int).Value = Me.GridTour.CurrentRow.Cells("RecID").Value
        cmd.Parameters.Add("@Brief", SqlDbType.VarChar).Value = Me.TxtRMKAdj.Text
        cmd.Parameters.Add("@SupplierRMK", SqlDbType.NVarChar).Value = ""
        cmd.Parameters.Add("@MU", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@SVCDate", SqlDbType.DateTime).Value = Now.Date
        cmd.Parameters.Add("@BookOnly", SqlDbType.Bit).Value = 0
        cmd.ExecuteNonQuery()
        LoadGridAdj()
    End Sub
    Private Sub LoadGridAdj()
        Me.GridAdj.DataSource = GetDataTable("select RecID, VendorID, cCurr as Curr, Cost as Amount, Vendor, Brief from dutoan_item where status='TV' and dutoanID=" & Me.GridTour.CurrentRow.Cells("RecID").Value)
        Me.GridAdj.Columns(0).Visible = False
        Me.GridAdj.Columns(1).Visible = False
        Me.GridAdj.Columns("Curr").Width = 32
        Me.GridAdj.Columns("Amount").Width = 75
        Me.GridAdj.Columns("Vendor").Width = 128
        Me.GridAdj.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridAdj.Columns("Amount").DefaultCellStyle.Format = "#,###.0"
        Me.LblDeleteAdj.Visible = False
    End Sub

    Private Sub GridAdj_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridAdj.CellContentClick
        Me.LblDeleteAdj.Visible = True
    End Sub
    Private Sub LblDeleteAdj_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblDeleteAdj.LinkClicked
        cmd.CommandText = ChangeStatus_ByID("Dutoan_Item", "XX", Me.GridAdj.CurrentRow.Cells("RecID").Value)
        cmd.ExecuteNonQuery()
        LoadGridAdj()
    End Sub

    Private Sub OptCWT_CheckedChanged(sender As Object, e As EventArgs) Handles OptCWT.CheckedChanged
        LoadCmb_VAL(Me.CmbCust, MyCust.List_CS)
    End Sub
    Private Sub OptLCL_Click(sender As Object, e As EventArgs) Handles OptLCL.CheckedChanged
        If mblnFirstLoadCompleted Then
            LoadCmb_VAL(Me.CmbCust, MyCust.List_LC)
        End If

    End Sub
    Private Sub txtRefCode_Enter(sender As Object, e As EventArgs) Handles txtRefCode.Enter
        If Me.txtRefCode.Text.Substring(0, 1) = "_" Then
            Me.txtRefCode.Text = ""
            Me.txtRefCode.ForeColor = Color.Black
        End If
    End Sub

    Private Sub OptPmtByTCode_Click(sender As Object, e As EventArgs) Handles OptPmtByTCode.Click, OptPmtByVendor.Click
        Me.CmbPmtRQSVC.Visible = Me.OptPmtByVendor.Checked
    End Sub
    Private Sub CmbPmtRQVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPmtRQVendor.SelectedIndexChanged
        Try
            If Me.OptPmtByVendor.Checked Then
                LoadGridPmtRQ(0, Me.CmbPmtRQVendor.SelectedValue)
            Else
                LoadGridPmtRQ(Me.GridTour.CurrentRow.Cells("RecID").Value, Me.CmbPmtRQVendor.SelectedValue)
            End If
            Me.GridAcct.DataSource = GetDataTable("select AccountNumber, BankName, AccountName, BankAddress,swift, RecID " &
                                      " from Vendor where RecID=" & Me.CmbPmtRQVendor.SelectedValue _
                                      & " and status='OK'")

        Catch ex As Exception
        End Try
    End Sub

    Private Sub CmbPmtRQSVC_LostFocus(sender As Object, e As EventArgs) Handles CmbPmtRQSVC.LostFocus
        LoadCmb_VAL(Me.CmbPmtRQVendor, "select RecId as VAL, ShortName as DIS from Vendor" _
                    & " where status='OK' and RecId<>2 and RecId in " _
                    & "(Select distinct VendorId from Dutoan_item where Status='OK' and Service='" & Me.CmbPmtRQSVC.Text & "')")
    End Sub
    Private Sub OptPmtByVendor_CheckedChanged(sender As Object, e As EventArgs) Handles OptPmtByVendor.CheckedChanged
        Me.TabControl1.Left = 0
        Me.TabControl1.Width = 975

        EnableSearchControl(Not OptPmtByVendor.Checked)

    End Sub

    Private Sub EnableSearchControl(blnEnable As Boolean)
        cboChannel.Visible = blnEnable
        cboCustShortName.Visible = blnEnable
        txtSearchValue.Visible = blnEnable
        LblSearch.Visible = blnEnable
        lbkReset.Visible = blnEnable
        cboActStatus.Visible = blnEnable
        cboSearchBy.Visible = blnEnable
    End Sub

    Private Sub OptPmtByTCode_CheckedChanged(sender As Object, e As EventArgs) Handles OptPmtByTCode.CheckedChanged
        Me.TabControl1.Left = 431
        Me.TabControl1.Width = 546
    End Sub


    Private Sub TabPage5_Leave(sender As Object, e As EventArgs) Handles TabPage5.Leave
        Me.OptPmtByTCode.PerformClick()
    End Sub
    Private Function GetOriAmt() As Boolean
        Dim strOriCur As String = ""
        Dim decOriAmt As Decimal
        For i As Int16 = 0 To Me.GridPmtRQTC.RowCount - 1
            With GridPmtRQTC
                If .Item("Q", i).Value Then
                    If strOriCur = "" Then
                        strOriCur = .Item("BeingPaidThisTime", i).Value
                    End If
                    decOriAmt = decOriAmt + .Item("BeingPaidThisTime", i).Value
                End If
            End With

        Next
        Return True
    End Function
    Private Function XacDinhAmtTraLanNay() As Decimal
        Dim AmtThisTime As Decimal = 0

        For i As Int16 = 0 To Me.GridPmtRQTC.RowCount - 1
            If Me.GridPmtRQTC.Item("BeingPaidThisTime", i).Value = 0 Then
                Me.GridPmtRQTC.Item("Q", i).Value = False
            End If
        Next
        For i As Int16 = 0 To Me.GridPmtRQTC.RowCount - 1
            If Me.GridPmtRQTC.Item("Q", i).Value Then
                AmtThisTime = AmtThisTime + Me.GridPmtRQTC.Item("BeingPaidThisTime", i).Value
            End If
        Next
        Return AmtThisTime
    End Function
    Private Function XacDinhBankFee() As Decimal
        Dim AmtThisTime As Decimal
        For i As Int16 = 0 To Me.GridPmtRQTC.RowCount - 1
            If Me.GridPmtRQTC.Item("Q", i).Value Then
                AmtThisTime = AmtThisTime + ScalarToDec("Dutoan_Item", "TTLtoVendor", "Service='Bank Fee' and Status='OK' and RelatedItem=" _
                                                        & Me.GridPmtRQTC.Item("RecId", i).Value)
            End If
        Next
        Return AmtThisTime
    End Function
    Private Sub txtTraLanNay_Enter(sender As Object, e As EventArgs) Handles txtTraLanNay.Enter
        Me.txtTraLanNay.Text = Format(XacDinhAmtTraLanNay(), "#,##0")
        txtBankFee.Text = Format(XacDinhBankFee(), "#,##0")
        Me.CmbPmtRQFOP.Text = "BTF"
    End Sub
    Private Sub LoadGridPmtRQ(ByVal pTourID As Integer, pVendorID As Integer)
        Me.GridPmtRQTC.DataSource = Nothing
        Dim StrQry As String = "select i.RecID, DuToanID, Q, TCode, CCurr,Cost, Qty, VAT, isVATIncl,TTLtoVendor" _
            & ", toVendorWoTax, VNDPaid, TTLtoVendor - VNDPaid as BeingPaidThisTime,t.Location" &
            " from Dutoan_item i inner join dutoan_tour t on t.recid=i.dutoanid And i.status<>'XX' "
        StrQry = StrQry & " where vendorID<>2 and bookonly=0 and toVendorWoTax - VNDPaid <>0"
        If pTourID > 0 Then
            StrQry = StrQry & " and DutoanID=" & pTourID
        End If
        StrQry = StrQry & " and VendorID=" & pVendorID & " and (pmtMethod='PPD' or NeedDeposit <>0) and svcdate >'1-Jan-16'"
        If pVendorID = 5042 Then ' so YTe thi ko lay HN
            StrQry = StrQry & " and dutoanID not in (select RecID from dutoan_tour where location='Ha Noi')"
        End If
        Select Case chkShowPlusOnly.CheckState
            Case CheckState.Checked
                StrQry = StrQry & " and TTLtoVendor>VndPaid "
            Case CheckState.Unchecked
                StrQry = StrQry & " and TTLtoVendor<VndPaid "
        End Select

        If Not myStaff.HasExtraRight("PrintPmtRQ") Then StrQry = StrQry & " and t.EDate>'" & Now.AddMonths(-6) & "' "  '^_^20220914 add by 7643

        Me.GridPmtRQTC.DataSource = GetDataTable(StrQry)
        If pTourID > 0 Then Me.GridPmtRQTC.Columns("TCode").Visible = False
        Me.GridPmtRQTC.Columns("RecID").Visible = False
        Me.GridPmtRQTC.Columns("DuToanID").Visible = False
        Me.GridPmtRQTC.Columns("CCurr").Width = 32
        Me.GridPmtRQTC.Columns("Q").Width = 25
        Me.GridPmtRQTC.Columns("VAT").Width = 56
        Me.GridPmtRQTC.Columns("Cost").Width = 70
        Me.GridPmtRQTC.Columns("toVendorWoTax").Width = 75
        Me.GridPmtRQTC.Columns("VNDPaid").Width = 75
        Me.GridPmtRQTC.Columns("Qty").Width = 32
        Me.GridPmtRQTC.Columns("VAT").Width = 32
        Me.GridPmtRQTC.Columns("isVATincl").Width = 56
        Me.GridPmtRQTC.Columns("Cost").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridPmtRQTC.Columns("Cost").DefaultCellStyle.Format = "#,##0.0"
        Me.GridPmtRQTC.Columns("toVendorWoTax").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridPmtRQTC.Columns("toVendorWoTax").DefaultCellStyle.Format = "#,##0.0"
        Me.GridPmtRQTC.Columns("VNDPaid").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridPmtRQTC.Columns("VNDPaid").DefaultCellStyle.Format = "#,##0.0"
        Me.GridPmtRQTC.Columns("BeingPaidThisTime").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridPmtRQTC.Columns("BeingPaidThisTime").DefaultCellStyle.Format = "#,##0.0"
        For i As Int16 = 0 To Me.GridPmtRQTC.RowCount - 1
            Me.GridPmtRQTC.Item("Q", i).Value = False
        Next
        For i As Int16 = 3 To Me.GridPmtRQTC.Columns.Count - 2
            Me.GridPmtRQTC.Columns(i).ReadOnly = True
        Next
        GridPmtRQTC.Columns("BeingPaidThisTime").ReadOnly = False
    End Sub

    Private Sub TxtIONo_Enter(sender As Object, e As EventArgs) Handles TxtIONo.Enter
        If Me.TxtIONo.Text <> "" AndAlso TxtIONo.Text.ToUpper.Substring(0, 1) = "_" Then
            Me.TxtIONo.Text = ""
            Me.TxtIONo.ForeColor = Color.Black
        End If
    End Sub

    Private Sub LblRefreshPaid_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblRefreshPaid.LinkClicked
        If Me.OptPmtByVendor.Checked Then Exit Sub
        Dim StrQry As String = "select RecID from Dutoan_item where status <>'XX' and vendorID= " & Me.CmbPmtRQVendor.SelectedValue &
                " and DutoanID=" & Me.GridTour.CurrentRow.Cells("RecID").Value
        Dim dTbl As DataTable = GetDataTable(StrQry)
        For i As Int16 = 0 To dTbl.Rows.Count - 1
            RefreshSoDaTra(dTbl.Rows(i)("RecID"))
        Next
        LoadGridPmtRQ(Me.GridTour.CurrentRow.Cells("RecID").Value, Me.CmbPmtRQVendor.SelectedValue)
    End Sub
    Private Sub LoadGridGO(blnCreate As Boolean)
        Dim chkLocalCorpID As Integer
        Dim strQry As String
        Me.GridGO.Visible = False
        GridGO.DataSource = vbNull
        If Me.OptLCL.Checked Then
            chkLocalCorpID = ScalarToInt("CWT.dbo.go_companyinfo1", "RecID", "CustID=" & Me.CmbCust.SelectedValue _
                                         & " and NoData4NonAir=0 and status='OK'")
            If chkLocalCorpID = 0 Then Exit Sub
            chkLocalCorpID = ScalarToInt("CWT.dbo.GO_RequiredData", "RecID", "CustID=" & Me.CmbCust.SelectedValue _
                                         & " and status='OK'")
            If chkLocalCorpID = 0 Then Exit Sub
            Me.GridGO.Visible = True
            If blnCreate Then
                strQry = "Select NameByCustomer, '' as FieldValues,DataCode, MinLength" _
                    & ", MaxLength, CharType,CheckValues,'RequiredData' as Source" _
                    & ",collectionmethod" _
                    & " from [CWT].[dbo].[GO_RequiredData]" _
                & " where status='OK' " _
                & " and ApplyTo in ('ALL','N-A','HTL','CAR')" _
                & " and custID=" & Me.CmbCust.SelectedValue
            Else
                strQry = " Select NameByCustomer, FValue as FieldValues,DataCode, MinLength" _
                    & ", MaxLength, CharType,CheckValues,'RequiredData'  as Source" _
                    & ",collectionmethod" _
                    & " from [CWT].[dbo].[GO_RequiredData] r left join cwt.dbo.SIR s" _
                    & " on r.NameByCustomer =s.Fname and s.Prod='nonair' and s.Status='OK' and RCPID=" _
                    & GridTour.CurrentRow.Cells("Recid").Value _
                    & " where r.status='OK'" _
                    & " and ApplyTo in ('ALL','N-A','HTL','CAR')" _
                    & " and r.custID=" & Me.CmbCust.SelectedValue
            End If

        ElseIf blnCreate Then
            strQry = "select CdrName AS NameByCustomer, '' as FieldValues, 'VEN'+ cast(CdrNbr as varchar) as DataCode" _
                & ",CollectionMethod, MinLength, MaxLength" _
                & ", CharType,CheckValues,'CDR' as Source" _
                & " from cwt.dbo.go_cdrs c " _
                & " where c.status='OK' " _
                & " and CMC in (select CMC from cwt.dbo.GO_CompanyInfo1 where custid=" _
                & Me.CmbCust.SelectedValue & " and status='OK' " &
                " and go_Client<>0 and NoData4NonAir=0)" _
                & " union all" _
                & " Select NameByCustomer, '' as FieldValues,DataCode" _
                & ",CollectionMethod, MinLength" _
                & ", MaxLength, CharType,CheckValues,'RequiredData'  as Source" _
                & " from [CWT].[dbo].[GO_RequiredData]" _
                & " where status='OK' " _
                & " and ApplyTo in ('ALL','N-A','HTL','CAR')" _
                & " and custID=" & Me.CmbCust.SelectedValue
        Else
            strQry = "select CdrName as NameByCustomer, FValue as FieldValues, 'VEN'+cast(CdrNbr as varchar) as DataCode" _
                & ",CollectionMethod, MinLength, MaxLength" _
                & ", CharType,CheckValues,'CDR' as Source" _
                & " from cwt.dbo.go_cdrs c left join cwt.dbo.SIR s" _
                & " on c.CdrName =s.Fname and s.Prod='nonair' and s.Status='OK' and RCPID=" _
                & GridTour.CurrentRow.Cells("Recid").Value _
                & " where c.status='OK' " _
                & " and CMC in (select CMC from cwt.dbo.GO_CompanyInfo1 where custid=" & Me.CmbCust.SelectedValue _
                & " and status='OK' and go_Client<>0 and NoData4NonAir=0)" _
                & " union all " _
                & " Select NameByCustomer, FValue as FieldValues,DataCode" _
                & ",CollectionMethod, MinLength" _
                & ", MaxLength, CharType,CheckValues ,'RequiredData'  as Source" _
                & " from [CWT].[dbo].[GO_RequiredData] r left join cwt.dbo.SIR s" _
                & " on r.NameByCustomer =s.Fname and s.Prod='nonair' and s.Status='OK' and RCPID=" & GridTour.CurrentRow.Cells("Recid").Value _
                & " where r.status='OK' " _
                & " and ApplyTo in ('ALL','N-A','HTL','CAR')" _
                & " and r.custID=" & Me.CmbCust.SelectedValue

        End If
        Me.GridGO.DataSource = GetDataTable(strQry)
        If Me.GridGO.RowCount = 0 Then
            Me.GridGO.Visible = False
        Else
            Me.GridGO.Visible = True
            For i As Int16 = 4 To Me.GridGO.ColumnCount - 2
                Me.GridGO.Columns(i).Visible = False
            Next

            Me.GridGO.Columns(0).ReadOnly = True
            Me.GridGO.Columns("DataCode").ReadOnly = True
            Me.GridGO.Columns("CollectionMethod").ReadOnly = True
            Me.GridGO.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            If Me.GridGO.Columns(0).Width > 180 Then
                Me.GridGO.Columns(0).Width = 180
            End If
            Me.GridGO.Columns(1).Width = 180
            Me.GridGO.Columns("MinLength").Width = 60
            Me.GridGO.Columns("DataCode").Width = 70
            Me.GridGO.Columns("CollectionMethod").Width = 120
        End If
    End Sub
    Private Function InvalidSIR() As Boolean
        Dim KQ As Boolean = False, FVal As String
        For i As Int16 = 0 To Me.GridGO.RowCount - 1
            FVal = Me.GridGO.Item(1, i).Value.ToString.Trim
            If FVal <> "" And FVal <> "NIL" Then
                If ScalarToInt("cwt.dbo.GO_RequiredDataValues", "top 1 RecId", "Status='OK' and CustId=" & MyCust.CustID _
                               & " and DataCode='" & GridGO.Item("DataCode", i).Value _
                               & "' and Value='" & GridGO.Item("FieldValues", i).Value & "'") > 0 Then
                    Continue For
                End If
                If FVal.Length < Me.GridGO.Item("MinLength", i).Value Or
                    FVal.Length > Me.GridGO.Item("MaxLength", i).Value Then

                    MsgBox("Invalid length for " & GridGO.Item("NameByCustomer", i).Value)
                    Return True
                End If

                If Me.GridGO.Item("CharType", i).Value = "ALPHA" Then
                    For j As Int16 = 0 To FVal.Length - 1
                        If InStr("0123456789", FVal.Substring(j, 1)) > 0 Then
                            MsgBox(FVal & " does NOT match with ALPHA")
                            Return True
                        End If
                    Next
                End If
                If Me.GridGO.Item("CharType", i).Value = "NUMERIC" Then
                    For j As Int16 = 0 To FVal.Length - 1
                        If InStr("0123456789", FVal.Substring(j, 1)) = 0 Then
                            MsgBox(FVal & " does NOT match with NUMERIC")
                            Return True
                        End If
                    Next
                End If
            End If
        Next
        Return KQ
    End Function
    Private Sub InsertSIR(pDutoanID As Integer, isDeleteB4 As Boolean)
        Dim lstQuerries As New List(Of String)
        If isDeleteB4 Then
            lstQuerries.Add("update cwt.dbo.SIR set status='XX', LstUpdate=getdate(), LstUser='" _
                             & myStaff.SICode & "' where PROD='NonAir' and RCPID=" & pDutoanID)
        End If
        For i As Int16 = 0 To Me.GridGO.RowCount - 1
            If Me.GridGO.Item(1, i).Value.ToString.ToUpper.Trim.Length > 0 Then
                lstQuerries.Add("Insert cwt.dbo.SIR (RCPID, PROD, FName, FValue, CustID, fstUser) values (" & pDutoanID _
                    & ",'NonAir','" & Me.GridGO.Item(0, i).Value _
                    & "','" & Me.GridGO.Item(1, i).Value.ToString.ToUpper _
                    & "'," & Me.CmbCust.SelectedValue & ",'" & myStaff.SICode & "')")
            End If
        Next
        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Unable to Save Required Data/CDRs")
            Exit Sub
        End If
    End Sub

    Private Sub GridVendorInforUpdate_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridVendorInforUpdate.CellContentClick
        Me.LckLblUpdateVendorAddr.Visible = False
        If e.RowIndex < 0 Then Exit Sub
        Me.CmbLoaiChungTu.Text = Me.GridVendorInforUpdate.CurrentRow.Cells("HoaDon").Value
        If GridVendorInforUpdate.CurrentRow.Cells(2).Value.ToString.Length > 16 Then Exit Sub
        Me.LckLblUpdateVendorAddr.Visible = True
    End Sub

    Private Sub LblUpdateVendorAddr_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LckLblUpdateVendorAddr.LinkClicked
        If myStaff.SupOf = "" Then Exit Sub
        Me.LckLblUpdateVendorAddr.Visible = False
        cmd.CommandText = "update Vendor set HoaDon=@HD where recID=" & Me.GridVendorInforUpdate.CurrentRow.Cells(0).Value
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@HD", SqlDbType.NVarChar).Value = Me.CmbLoaiChungTu.Text
        cmd.ExecuteNonQuery()
    End Sub
    Private Function DefineBankFee(pSupplierID As Integer, pItemID As Integer) As Decimal
        Dim KQ As Decimal, Country As String, AmtToVendor As Decimal
        Dim decUsdRoe As Decimal
        Country = ScalarToString("supplier", "Address_CountryCode", "RecID=" & pSupplierID)
        decUsdRoe = ForEX_12(myStaff.City, Now, "USD", "BSR", "TS").Amount
        AmtToVendor = ScalarToDec("Dutoan_Item", "TTLToVendor", "RecID=" & pItemID) / decUsdRoe
        KQ = 0.0015 * AmtToVendor
        If KQ < 10 Then KQ = 10
        If KQ > 350 Then KQ = 350
        KQ = KQ + 5
        KQ = KQ + IIf(Country = "JP", 55, 25)
        Return KQ
    End Function

    Private Sub LblAddMerchantFee_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblAddMerchantFee.LinkClicked
        Dim decKhachPhaiTra As Decimal = ScalarToDec("Dutoan_item", "Sum(TTLToPax)", "DutoanID=" & Me.GridTour.CurrentRow.Cells("RecID").Value & " and status<>'XX'")
        Dim decMF As Decimal
        Dim decPct As Decimal

        Select Case Me.CmbCrd.Text

            Case "VI", "MC", "CA"
                decPct = 0.0155
            Case "AX"
                decPct = 0.03
            Case "DC"
                decPct = 0.0385
            Case ""
                MsgBox("You must select Credit Card Type")
                Exit Sub
            Case Else
                MsgBox("Unable to add MerchantFee. Fee level is NOT specified")
                Exit Sub
        End Select

        decMF = Math.Round(decKhachPhaiTra / (1 - decPct) - decKhachPhaiTra, 0)

        AddFee("Merchant", "VND", 1, decMF, True, TxtVAT.Text, 0, 1)
        LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)
    End Sub
    Private Sub LblAddBancFee_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblAddBancFee.LinkClicked
        Dim BF As Decimal
        Dim decUsdRoe As Decimal
        With GridSVC.CurrentRow
            If .Cells("VendorId").Value = 2 Or .Cells("CCurr").Value = "VND" Then
                MsgBox("You must select related service!")
                Exit Sub
            End If
            If .Cells("VendorId").Value <> 2 And
                ScalarToString("supplier", "Address_CountryCode", "Status='OK' and VendorID=" & .Cells("VendorID").Value) <> "VN" Then
                BF = DefineBankFee(.Cells("SupplierID").Value, .Cells("RecID").Value)
                decUsdRoe = ForEX_12(myStaff.City, Now, "USD", "BSR", "TS").Amount
                AddFee("Bank", "USD", 1, BF, True, 10, .Cells("RecID").Value, decUsdRoe)
                LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)
            End If
        End With



        'For i As Int16 = 0 To Me.GridSVC.RowCount - 1
        '    If Me.GridSVC.Item("VendorID", i).Value <> 2 And _
        '        ScalarToString("supplier", "Address_CountryCode", "VendorID=" & Me.GridSVC.Item("VendorID", i).Value) <> "VN" Then
        '        BF = BF + DefineBankFee(Me.GridSVC.Item("SupplierID", i).Value, Me.GridSVC.Item("RecID", i).Value)
        '    End If
        'Next

    End Sub
    Private Sub AddFee(pFeeName As String, pCurr As String, pQty As Decimal, pAmt As Decimal, pVATInclude As Boolean _
                       , pVATAmt As Decimal, pRelatedItem As Integer, pROE As Decimal _
                       , Optional strSvcDesc As String = "")
        cmd.CommandText = "insert DuToan_Item (Service, CCurr, Unit, Qty, Cost, Supplier" _
            & ", VendorID, Vendor, FstUser, PmtMethod, isVATIncl, " _
            & " VAT, DuToanID, SVCDate,  RelatedItem, ROE" _
            & ", SupplierID,BookOnly,PaxName,ZeroFeeReason,Brief,Vat4Vendor,DomInt)" _
            & " Values (@Service, @CCurr,@Unit, @Qty, @Cost, @Supplier, @VendorID, " _
            & "@Vendor, @FstUser, @PmtMethod, @isVATIncl, @VAT, @DuToanID, @SVCDate" _
            & ", @RelatedItem, @ROE, @SupplierID, @BookOnly,@PaxName,@ZeroFeeReason,@Brief,@VAT4Vendor,@DomInt)"
        cmd.Parameters.Clear()
        If pFeeName = "HotLine" Then
            cmd.Parameters.Add("@Service", SqlDbType.VarChar).Value = pFeeName
        Else
            cmd.Parameters.Add("@Service", SqlDbType.VarChar).Value = pFeeName & " Fee"
        End If

        cmd.Parameters.Add("@CCurr", SqlDbType.VarChar).Value = pCurr
        cmd.Parameters.Add("@Unit", SqlDbType.VarChar).Value = "Service"
        cmd.Parameters.Add("@Qty", SqlDbType.Decimal).Value = pQty
        cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = pAmt
        cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@Vendor", SqlDbType.VarChar).Value = "TransViet"
        cmd.Parameters.Add("@VendorID", SqlDbType.Int).Value = 2
        cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        cmd.Parameters.Add("@PmtMethod", SqlDbType.VarChar).Value = "PSP"
        cmd.Parameters.Add("@isVATIncl", SqlDbType.Bit).Value = pVATInclude
        cmd.Parameters.Add("@BookOnly", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@VAT", SqlDbType.Decimal).Value = pVATAmt
        cmd.Parameters.Add("@ROE", SqlDbType.Decimal).Value = pROE
        cmd.Parameters.Add("@DuToanID", SqlDbType.Int).Value = Me.GridTour.CurrentRow.Cells("RecID").Value
        cmd.Parameters.Add("@SVCDate", SqlDbType.DateTime).Value = Me.GridTour.CurrentRow.Cells("SDate").Value
        cmd.Parameters.Add("@RelatedItem", SqlDbType.Int).Value = pRelatedItem
        cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value = 2
        cmd.Parameters.Add("@PaxName", SqlDbType.VarChar).Value = txtPaxName.Text
        If pAmt <> 0 Then
            cmd.Parameters.Add("@ZeroFeeReason", SqlDbType.VarChar).Value = ""
        Else
            cmd.Parameters.Add("@ZeroFeeReason", SqlDbType.VarChar).Value = cboZeroFeeReason.Text
        End If
        cmd.Parameters.Add("@Brief", SqlDbType.VarChar).Value = strSvcDesc
        cmd.Parameters.Add("@VAT4Vendor", SqlDbType.Decimal).Value = pVATAmt
        cmd.Parameters.Add("@DomInt", SqlDbType.VarChar).Value = txtDomInt.Text
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub CmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSupplier.SelectedIndexChanged
        Try
            Dim tblSupplier As DataTable = GetDataTable("select * from Supplier where RecId=" & CmbSupplier.SelectedValue)

            If CmbService.Text <> "Visa" Then
                Me.TxtMoTaSVC.Text = tblSupplier.Rows(0)("Address")
                If tblSupplier.Rows(0)("Address_CountryCode") = "VN" Then
                    txtDomInt.Text = "DOM"
                Else
                    txtDomInt.Text = "INT"
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtBooker_SelectedIndexChanged(sender As Object, e As EventArgs)
        If txtBooker.Text.Contains("ZPERSONAL") Then Me.GridGO.Visible = False
    End Sub

    Private Sub LblSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblSearch.LinkClicked
        LoadgridTour()

        'Me.GridTour.DataSource = GetDataTable("select t.*,c.VAL as Channel from Dutoan_Tour t" _
        '                                      & " left join [Cust_Detail] c on t.CustId=c.CustId" _
        '                                      & " where " & strDK & " And c.Status='OK' and c.Cat='Channel'")
        'With GridTour
        '    If .RowCount > 0 Then
        '        .Columns("RecId").Width = 50
        '        .Columns("Pax").Width = 30
        '        .Columns("SDate").Width = 60
        '        .Columns("EDate").Width = 60
        '    End If
        'End With
        'LoadGridGO()
    End Sub
    Private Sub LblOrderBV_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblOrderBV.LinkClicked
        Book_a_MSGR(myStaff.SICode, myStaff.PSW, "N/A", Me.GridTour.CurrentRow.Cells("Tcode").Value, 0)
    End Sub

    Private Sub UploadedFiles_Enter(sender As Object, e As EventArgs) Handles UploadedFiles.Enter

    End Sub

    Private Sub ShowCDRs()
        Dim colCdrs As New Collection
        Dim strCmc As String = ScalarToString("cwt.dbo.go_CompanyInfo1", "Cmc", " Status='OK' and CustId=" _
                                              & GridTour.CurrentRow.Cells("CustId").Value)
        flpCdr.Controls.Clear()
        If strCmc <> "" Then
            colCdrs = GetCDRs(Conn, strCmc, True, True)
            For Each objCdr As clsCwtCdr In colCdrs
                Dim ucCdr As New ucCdr(objCdr)
                flpCdr.Controls.Add(ucCdr)
            Next
        End If
    End Sub

    Private Sub FillRequiredData(tblRequiredData As DataTable)

        If GridTour.CurrentRow.Cells("RequiredData").Value = "" Then
            Exit Sub
        End If
        Dim arrRequiredData As String() = GridTour.CurrentRow.Cells("RequiredData").Value.ToString.Split("|")
        Dim colAvailData As New Collection



        For Each ucCdr As ucCdr In flpCdr.Controls
            If colAvailData.Contains("CDR" & ucCdr.lblName.Tag) Then
                ucCdr.txtValue.Text = colAvailData("CDR" & ucCdr.lblName.Tag)
            End If
        Next

    End Sub
    Private Sub lbkSaveCDRs_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        'Dim strRqData As String = String.Empty
        'For Each ucCdr As ucCdr In flpCdr.Controls
        '    If ucCdr.txtValue.Text <> "" Then
        '        strRqData = strRqData & "CDR" & ucCdr.lblName.Tag & "/" & ucCdr.txtValue.Text & "|"
        '    End If
        'Next
        'If strRqData.Length > 0 Then
        '    strRqData = Mid(strRqData, 1, strRqData.Length - 1)
        'End If
        'ExecuteNonQuerry("Update DuToan_Tour set RequiredData='" & strRqData & "' where RecId=" _
        '                 & GridTour.CurrentRow.Cells("RecId").Value, Conn)
    End Sub



    Private Sub lbkUploadFile_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUploadFile.LinkClicked
        If GridTour.CurrentRow IsNot Nothing Then
            Dim frmUpload As New frmUploadFile4NonAir(GridTour.CurrentRow, True, False)
            If frmUpload.ShowDialog = Windows.Forms.DialogResult.OK Then
                Select Case frmUpload.FileType
                    Case "Registration"
                        txtFileId.Text = frmUpload.FileId
                        GridTour.CurrentRow.Cells("FileId").Value = frmUpload.FileId
                    Case "Quotation"
                        txtQuotationId.Text = frmUpload.FileId
                        GridTour.CurrentRow.Cells("QuotationFile").Value = frmUpload.FileId
                End Select

            End If
        End If

    End Sub


    Private Sub lbkViewUploadedFile_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewUploadedFile.LinkClicked
        If txtFileId.Text <> 0 Or txtQuotationId.Text <> 0 Then
            Dim frmView As New frmUploadFile4NonAir(GridTour.CurrentRow, False, False)
            frmView.ShowDialog()

        End If
    End Sub




    Private Sub lbkSaveRequiredData_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Dim lstRequiredData As New List(Of String)

        If Not CheckInputValues4RequiredData() Then
            Exit Sub
        End If
        For Each objCtrl As Control In flpRequiredData.Controls
            If objCtrl.Name = "ucRqDataCombo" Then
                Dim objUc As ucRqDataCombo = objCtrl
                If objUc.cboValue.Text <> "" Then
                    lstRequiredData.Add(objUc.Tag & "|" & objUc.cboValue.Text)
                End If

            ElseIf objCtrl.Name = "ucRqDataText" Then
                Dim objUc As ucRqDataText = objCtrl
                If objUc.txtValue.Text <> "" Then
                    lstRequiredData.Add(objUc.Tag & "|" & objUc.txtValue.Text)
                End If
            End If
        Next
        If lstRequiredData.Count > 0 Then
            If Not ExecuteNonQuerry("Update DuToan_Tour set RequiredData='" _
                             & Join(lstRequiredData.ToArray, "|") & "' where RecId=" _
                         & GridTour.CurrentRow.Cells("RecId").Value, Conn) Then
                MsgBox("Unable to update RequiredData")
            End If
        End If

    End Sub
    Private Function CheckInputValues4RequiredData() As Boolean
        Dim lstError As New List(Of String)
        Dim strError As String = ""
        Dim strErrMsg As String

        Dim i As New List(Of String)

        For Each objCtrl As Control In flpRequiredData.Controls
            With objCtrl
                If .Name = "ucRqDataCombo" Then
                    Dim ucRd As ucRqDataCombo = objCtrl
                    strError = ucRd.CheckInput

                ElseIf .Name = "ucRqDataText" Then
                    Dim ucRd As ucRqDataText = objCtrl
                    strError = ucRd.CheckInput
                End If
            End With
            If strError <> "" Then
                lstError.Add(strError)
            End If
        Next
        strErrMsg = Join(lstError.ToArray, vbNewLine)
        If strErrMsg <> "" Then
            MsgBox(strErrMsg)
            Return False
        End If

        Return True
    End Function
    Private Sub AddData(tblRequiredData As DataTable, colAvailData As Collection)
        For Each objRow As DataRow In tblRequiredData.Rows

            If objRow("CheckValues") Then
                Dim ucRqData As New ucRqDataCombo
                ucRqData.Tag = objRow("DataCode")
                ucRqData.Row = objRow
                If objRow("Mandatory") = "M" Then
                    ucRqData.lblName.Text = objRow("NameByCustomer") & "(*)"
                ElseIf objRow("Mandatory") = "C" Then
                    ucRqData.lblName.Text = objRow("NameByCustomer") & "(" _
                                            & objRow("ConditionOfUse") & ")"
                End If
                LoadComboDisplay(ucRqData.cboValue, "Select Value" _
                        & ",Description as Display from cwt.dbo.GO_RequiredDataValues" _
                        & " where Status='OK' and CustId=" & objRow("CustId") _
                        & " and DataCode='" & objRow("DataCode") & "'", Conn)
                ucRqData.cboValue.SelectedIndex = -1
                If colAvailData.Contains(objRow("DataCode")) Then
                    ucRqData.cboValue.SelectedIndex = ucRqData.cboValue.FindStringExact(colAvailData(objRow("DataCode")))
                End If
                flpRequiredData.Controls.Add(ucRqData)
            Else
                Dim ucRqData As New ucRqDataText
                ucRqData.Tag = objRow("DataCode")
                ucRqData.Row = objRow
                If objRow("Mandatory") = "M" Then
                    ucRqData.lblName.Text = objRow("NameByCustomer") & "(*)"
                ElseIf objRow("Mandatory") = "C" Then
                    ucRqData.lblName.Text = objRow("NameByCustomer") & "(" _
                                            & objRow("ConditionOfUse") & ")"
                End If
                If colAvailData.Contains(objRow("DataCode")) Then
                    ucRqData.txtValue.Text = colAvailData(objRow("DataCode"))
                End If

                flpRequiredData.Controls.Add(ucRqData)
            End If
        Next
    End Sub

    Private Sub cboChannel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboChannel.SelectedIndexChanged
        Select Case cboChannel.Text
            Case "CWT"
                LoadCmb_VAL(Me.cboCustShortName, MyCust.List_CS)
            Case "LCL"
                LoadCmb_VAL(Me.cboCustShortName, MyCust.List_LC)
        End Select
        cboCustShortName.SelectedIndex = -1
    End Sub

    Private Sub blkReset_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkReset.LinkClicked
        Reset()
    End Sub


    Private Sub cboCustShortName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustShortName.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadgridTour()
        End If
    End Sub

    Private Sub GridGO_SelectionChanged(sender As Object, e As EventArgs) Handles GridGO.SelectionChanged
        With GridGO
            If .RowCount = 0 Then
                mintSelectedCrdRow = -1
            Else
                mintSelectedCrdRow = .CurrentRow.Index
                cboCdrValues.Enabled = .CurrentRow.Cells("CheckValues").Value
                lbkSelectCdrValues.Enabled = .CurrentRow.Cells("CheckValues").Value

                If .CurrentRow.Cells("CheckValues").Value Then
                    Dim strQuerry As String = ""
                    Select Case .CurrentRow.Cells("Source").Value
                        Case "CDR"
                            strQuerry = "Select Value from Cwt.dbo.GO_MiscWzDate" _
                                & " where Status='OK' and Catergory='CDR" & Mid(.CurrentRow.Cells("DataCode").Value, 4) _
                                & "' and Value1= (Select top 1 CMC from CompanyInfo where Status='ok'" _
                                & " and CustId=" & CmbCust.SelectedValue & ") order by Value"
                            LoadCombo(cboCdrValues, strQuerry, Conn)

                        Case "RequiredData"
                            strQuerry = "Select Value from Cwt.dbo.GO_RequiredDataValues" _
                                & " where Status='OK' and DataCode='" & .CurrentRow.Cells("DataCode").Value _
                                & "' and CustId =" & CmbCust.SelectedValue & " order by Value"
                            LoadCombo(cboCdrValues, strQuerry, Conn)
                    End Select
                End If
            End If
        End With

    End Sub

    Private Sub lbkSelectCdrValues_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectCdrValues.LinkClicked
        If mintSelectedCrdRow > -1 AndAlso cboCdrValues.Text <> "" Then
            GridGO.Rows(mintSelectedCrdRow).Cells("FieldValues").Value = cboCdrValues.Text
        End If
    End Sub

    Private Sub lbkQuickRef_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkQuickRef.LinkClicked
        Dim frmQuickRef As New frmQuickRef(CmbCust.SelectedValue)
        frmQuickRef.ShowDialog()
    End Sub

    Private Sub lbkLinkItems_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkLinkItems.LinkClicked
        If GridSVC.CurrentRow Is Nothing Then Exit Sub
        Dim frmLink As New frmLinkTourItems(GridSVC.CurrentRow)
        If frmLink.ShowDialog() = DialogResult.OK Then
            LoadGridSVC(GridTour.CurrentRow.Cells("RecId").Value)
        End If
    End Sub


    Private Sub lbkVoucher_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkVoucher.LinkClicked
        InHoaDon(Application.StartupPath, "HotelVoucher.xlt", "F", "Q", Now.Date, Now.Date _
                 , Me.GridSVC.CurrentRow.Cells("recID").Value, "", "", "F")
        Process.Start("D:\HotelVoucher" & GridSVC.CurrentRow.Cells("recID").Value & ".xls")
    End Sub

    Private Sub lbkUploadFile2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUploadFile2.LinkClicked
        If GridTour.CurrentRow IsNot Nothing Then
            Dim frmUpload As New frmUploadFile4NonAir(GridTour.CurrentRow, True, True)
            If frmUpload.ShowDialog = Windows.Forms.DialogResult.OK Then
                Select Case frmUpload.FileType
                    Case "Registration"
                        txtFileId.Text = frmUpload.FileId
                        GridTour.CurrentRow.Cells("FileId").Value = frmUpload.FileId
                    Case "Quotation"
                        txtQuotationId.Text = frmUpload.FileId
                        GridTour.CurrentRow.Cells("QuotationFile").Value = frmUpload.FileId
                    Case "RegistrationChange"
                        LoadFileList()
                End Select

            End If
        End If

    End Sub
    Private Function LoadFileList() As Boolean
        Dim strQuerry As String = "Select Val1 as FileType, Val2 as FileId, RecId from MISC" _
            & " where Cat='UploadedFile' and Val='" & GridTour.CurrentRow.Cells("RecId").Value & "'"
        LoadDataGridView(dgrUploadedFiles, strQuerry, Conn)
        dgrUploadedFiles.Columns("RecId").Visible = False
        TabControl1.TabPages("UploadedFiles").Text = "UploadedFile" & dgrUploadedFiles.RowCount
        Return True
    End Function
    Private Function LoadStoredFile2() As Boolean
        Dim strQuerry As String = "Select Val1 as FileType, Val2 as FileId, RecId from MISC" _
            & " where Cat='UploadedFile' and Val='" & GridTour.CurrentRow.Cells("RecId").Value & "'"
        LoadDataGridView(dgrUploadedFiles, strQuerry, Conn)
        dgrUploadedFiles.Columns("RecId").Visible = False

        Return True
    End Function
    Private Sub lbkViewUploadedFile2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewUploadedFile2.LinkClicked
        If dgrUploadedFiles.RowCount > 0 Then
            Dim frmView As New frmUploadFile4NonAir(dgrUploadedFiles.CurrentRow, False, True)
            frmView.ShowDialog()
        End If
    End Sub

    Private Sub lbkAddFund_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddFund.LinkClicked
        If GridSVC.CurrentRow IsNot Nothing Then
            Dim frmAddFund As New frmCreateNonAirFund(GridSVC.CurrentRow)
            frmAddFund.ShowDialog()
        End If

    End Sub

    Private Sub lbkUseFund_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUseFund.LinkClicked
        If GridSVC.CurrentRow IsNot Nothing Then
            Dim frmDeductFund As New frmDeductNonAirFund(GridSVC.CurrentRow)
            Try
                frmDeductFund.ShowDialog()
                LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)
            Catch ex As Exception

            End Try


        End If
    End Sub

    Private Sub GridSVC_SelectionChanged(sender As Object, e As EventArgs) Handles GridSVC.SelectionChanged
        If Not GridSVC.CurrentRow Is Nothing _
            AndAlso GridSVC.CurrentRow.Cells("Service").Value = "Accommodations" Then
            lbkVoucher.Visible = True
        Else
            lbkVoucher.Visible = False
        End If
    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        Dim lstQuerries As New List(Of String)
        lstQuerries.Add("Delete MISC where RecId=" & dgrUploadedFiles.CurrentRow.Cells("RecId").Value)
        lstQuerries.Add("Delete Images where RecId=" & dgrUploadedFiles.CurrentRow.Cells("FileId").Value)

        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Unable to deelte file")
        Else
            LoadFileList()
        End If
    End Sub

    Private Sub lbkGetDataFromAir_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkGetDataFromAir.LinkClicked
        Dim strRequiredData As String = ""
        Dim lstRequiredData As New List(Of String)
        Dim i As Integer

        Dim strTkno As String = InputBox("Input Ticket Number").Replace(" ", "")
        If strTkno.Length <> 13 Then
            MsgBox("Invalid Ticket Number")
            Exit Sub
        End If
        strRequiredData = ScalarToString("cwt.dbo.GO_Travel", "top 1 RequiredData" _
                            , " where Status ='OK' and BkgAction='AB' and RecID in " _
                            & " (Select TravelId FROM cwt.dbo.GO_Air where TKNO='" & Mid(strTkno, 4) _
                            & "' and (Carrier='" & Mid(strTkno, 2, 2) & "' or Carrier=" _
                            & " (select top 1 AlCode from AirlineList where DocCode='" _
                            & Mid(strTkno, 1, 3) & "')))")

        If strRequiredData = "" Then
            MsgBox("No Data From Air")
            Exit Sub
        End If
        lstRequiredData.AddRange(strRequiredData.Split("|"))

        For Each objRow As DataGridViewRow In GridGO.Rows
            For i = 0 To lstRequiredData.Count - 1
                If lstRequiredData(i).StartsWith(objRow.Cells("DataCode").Value & "/") Then
                    objRow.Cells("FieldValues").Value = Mid(lstRequiredData(i), objRow.Cells("DataCode").Value.ToString.Length + 2)
                    lstRequiredData.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub CmbPmtRQSVC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPmtRQSVC.SelectedIndexChanged

    End Sub

    Private Sub lbkSaveCost4CWT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSaveCost4CWT.LinkClicked
        Dim strQuerry As String = "Update Dutoan_Item set Cost4Cwt=" & txtCost4CWT.Text _
                                & " where Recid=" & GridSVC.CurrentRow.Cells("RecId").Value

        If Not IsNumeric(txtCost4CWT.Text) Then
            MsgBox("Invalid Cost4CWT")
            Exit Sub
        End If
        Select Case GridSVC.CurrentRow.Cells("CCurr").Value
            Case "USD", "SGD"
                If txtCost4CWT.Text > 10000 Then
                    MsgBox("Amount it too big!")
                    Exit Sub
                End If
            Case Else
        End Select

        If Not ExecuteNonQuerry(strQuerry, Conn) Then
            MsgBox("Unable to Update Cost4CWT")
        Else
            LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)
        End If
    End Sub

    Private Sub lbkSaveRequiredData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSaveRequiredData.LinkClicked
        If Me.GridGO.Visible Then
            InsertSIR(Me.GridTour.CurrentRow.Cells("RecID").Value, True)
        End If
    End Sub

    Private Function ChangeNonAirFund(intItemId As Integer, decAmount As Decimal) As Boolean
        Dim tblOldFund As System.Data.DataTable
        Dim lstQuerries As New List(Of String)

        tblOldFund = GetDataTable("select top 1 * from NonAirFund where Status='OK'" _
                                  & " and FundId=" _
                                  & "(select FundId from NonAirFund where CreatedByItem=" _
                                  & intItemId & ") order by RecId desc")
        If tblOldFund.Rows.Count = 0 Then
            MsgBox("Unable to find Old Fund")
            Return False
        End If
        lstQuerries.Add("Update NonAirFund set Status='RR',LstUpdate=getdate(),LstUser='" _
                         & myStaff.SICode & "' where RecId=" & tblOldFund.Rows(0)("RecId"))

        With tblOldFund
            lstQuerries.Add("insert into NonAirFund (FundId,Cur,Amount,VendorId,ExpiryDate" _
                        & ",CreatedByItem,FstUser,Description) values (" _
                        & .Rows(0)("FundId") & ",'" & .Rows(0)("Cur") _
                        & "'," & .Rows(0)("Amount") + decAmount _
                        & "," & .Rows(0)("VendorId") & ",'" & CreateToDate(.Rows(0)("ExpiryDate")) _
                        & "'," & intItemId & ",'" & myStaff.SICode _
                        & "','" & .Rows(0)("Description") & "')")
        End With

        If UpdateListOfQuerries(lstQuerries, Conn) Then
            Return True
        Else
            Return False
        End If

    End Function


    Private Sub tabAirTkt_Enter(sender As Object, e As EventArgs) Handles tabAirTkt.Enter
        If GridTour.CurrentRow Is Nothing Then Exit Sub
        LoadTkt()
    End Sub
    Private Function LoadTkt() As Boolean

        Dim strQuerry As String = "Select m.RecId,Tkno, DOI from MISC m left join Tkt t on m.intVal1=t.RecId" _
                                & " where t.Status<>'XX' and m.Cat='WzAir' and m.Status='OK' and m.intVal=" _
                                & GridTour.CurrentRow.Cells("RecId").Value
        LoadDataGridView(dgTkt, strQuerry, Conn)

        tabAirTkt.Text = tabAirTkt.Text & "(" & dgTkt.RowCount & ")"
        If dgTkt.RowCount > 0 Then
            lbkSelectTkt.Visible = False
        End If
        Return True
    End Function
    Private Function LinkTkt() As Boolean
        Dim tblCurrentTkt As DataTable = GetDataTable("Select VAL as Tkno, dteVal as DOI from MISC" _
                                & " where Cat='WzAir' and Status='OK' and intVal=" &
                                GridTour.CurrentRow.Cells("RecId").Value)
        Dim blnSelectTkt As Boolean = False

        With GridTour.CurrentRow
            If .Cells("WzAir").Value AndAlso tblCurrentTkt.Rows.Count > 0 Then
                'bo qua
            ElseIf .Cells("WzAir").Value AndAlso tblCurrentTkt.Rows.Count = 0 Then
                MsgBox("You must select Associated Ticket")
                blnSelectTkt = True
            ElseIf Not .Cells("WzAir").Value AndAlso tblCurrentTkt.Rows.Count > 0 Then
                MsgBox("You must Remove Associated Ticket")
            ElseIf MsgBox("Travel with Air", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                blnSelectTkt = True
            End If
        End With

        If blnSelectTkt Then
            If SelectTkt() Then
                LoadTkt()
            Else
                MsgBox("Unable to complete this action")
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectTkt.LinkClicked

        If dgTkt.RowCount > 0 Then
            MsgBox("You can select 1 ticket only!")
        End If

        If SelectTkt() Then
            LoadTkt()
        End If
    End Sub

    Private Sub lbkRemove_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRemove.LinkClicked
        If ExecuteNonQuerry("Update MISC set Status='XX',lstUpdate=getdate(),LstUser='" _
                             & myStaff.SICode & "' where RecId=" & dgTkt.CurrentRow.Cells("Recid").Value, Conn) Then
            LoadTkt()
        End If
    End Sub

    Private Sub GridPmtRQTC_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridPmtRQTC.CellContentClick

    End Sub

    Private Sub lbkAddHotLineFee_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub TxtStartDate_ValueChanged(sender As Object, e As EventArgs) Handles TxtStartDate.ValueChanged
        txtEndDate.Value = TxtStartDate.Value
    End Sub

    Private Function SelectTkt() As Boolean
        With GridTour.CurrentRow
            Dim tblNewTkt As DataTable
            Dim arrNameBreaks As String() = Split(.Cells("Traveller").Value, " ")
            Dim dteFromDate As Date = .Cells("FstUpdate").Value

            dteFromDate = dteFromDate.AddDays(-1)
            Dim strGetTkt As String = "Select t.RecId,PaxName,TKNO, DOI, DOF,Itinerary" _
                                        & " from Tkt t left join Rcp r on t.RcpId=r.RecId" _
                                        & " where t.Status<>'xx' and r.CustShortName='NOVARTIS VN'" _
                                        & " and t.RecId not in (Select IntVal1 from MISC where Cat='WzAir' and Status='OK')" _
                                        & " and DOI>='" & CreateFromDate(dteFromDate) _
                                        & "' and DOI<='" & CreateToDate(.Cells("Sdate").Value) & "'"

            For Each strNamePart As String In arrNameBreaks
                strGetTkt = strGetTkt & " and t.PaxName like '%" & strNamePart & "%'"
            Next
            strGetTkt = strGetTkt & " order by T.RecId desc"
            tblNewTkt = GetDataTable(strGetTkt)
            If tblNewTkt.Rows.Count > 0 Then
                Dim frmSelectTkt As New frmShowTableContent(tblNewTkt, "Please select ticket", "RecId")
                If frmSelectTkt.ShowDialog = DialogResult.OK Then
                    If ExecuteNonQuerry("insert into MISC (CAT, IntVal, IntVal1, FstUser, Status, City) values ('WzAir'," _
                                       & .Cells("RecId").Value & "," & frmSelectTkt.SelectedValue _
                                       & ",'" & myStaff.SICode & "','OK','" & myStaff.City & "')", Conn) Then
                        LoadTkt()
                    End If
                End If
            End If
        End With
        Return True
    End Function

    Private Sub TxtVAT_Validated(sender As Object, e As EventArgs) Handles TxtVAT.Validated
        If mobjSvcRow IsNot Nothing Then
            If TxtVAT.Text <> mobjSvcRow.Cells("VAT").Value _
                 AndAlso TxtVAT.Text <> txtVat4Vendor.Text _
                 AndAlso LblSaveSvc.Text = "SaveChange" Then
                MsgBox("VAT4Vendor will also be changed!")
                txtVat4Vendor.Text = TxtVAT.Text
            End If
        End If

    End Sub

    Private Sub lbkAbortChange_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAbortChange.LinkClicked
        RefreshSvc()
        LblSaveSvc.Text = "Edit"
        LblAddSVC.Visible = True
    End Sub



    Private Sub TxtVAT_Enter(sender As Object, e As EventArgs) Handles TxtVAT.Enter
        If GridSVC.CurrentRow Is Nothing Then
            mobjSvcRow = Nothing
        Else
            mobjSvcRow = GridSVC.CurrentRow
        End If
    End Sub

    Private Sub lbkViewStoredFile_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewStoredFile.LinkClicked
        If dgrStoredFile.CurrentRow Is Nothing Then Exit Sub
        Dim strDir As String = "\\dataserver\Lip\NonAir\" & GridTour.CurrentRow.Cells("RecId").Value

        Dim strFilePath As String = strDir & "\" & dgrStoredFile.CurrentRow.Cells("RecId").Value _
                          & "." & dgrStoredFile.CurrentRow.Cells("FileType").Value
        Dim strDestPath As String = "D:\temp_IT\" & dgrStoredFile.CurrentRow.Cells("RecId").Value _
                          & "." & dgrStoredFile.CurrentRow.Cells("FileType").Value

        Try
            If System.IO.File.Exists(strFilePath) Then
                System.IO.File.Copy(strFilePath, strDestPath, True)
                Process.Start(strDestPath)
            Else
                MsgBox("File does NOT exit!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lbkStoreFile_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkStoreFile.LinkClicked
        If GridTour.CurrentRow Is Nothing Then Exit Sub

        Dim frmStore As New frmSelectFile(GridTour.CurrentRow.Cells("RecId").Value)

        If frmStore.ShowDialog = DialogResult.OK Then
            If StoreFile(GridTour.CurrentRow.Cells("RecId").Value, frmStore.txtFilePath.Text, frmStore.txtBrief.Text _
                         , frmStore.cboVendor.SelectedValue) Then
                LoadStoredFile()
            End If
        End If
    End Sub
    Private Function LoadStoredFile() As Boolean
        If GridTour.CurrentRow Is Nothing Then Return False

        If (Not System.IO.Directory.Exists("D:\temp_IT\")) Then
            System.IO.Directory.CreateDirectory("D:\temp_IT\")
        End If

        Dim strQuerry As String = "Select i.RecId,a.ShortName,FileType,i.Brief,i.FstUpdate,intVal as VendorId" _
            & " from Images i" _
            & " left join Vendor a on i.intVal=a.RecId" _
            & " where i.Status='OK' and i.StorePurpose=1 and Dutoan_TourId=" & GridTour.CurrentRow.Cells("RecId").Value _
            & " order by a.ShortName, i.RecId"
        LoadDataGridView(dgrStoredFile, strQuerry, Conn)
        dgrStoredFile.Columns("RecId").Width = 40
        dgrStoredFile.Columns("FileType").Width = 50
        dgrStoredFile.Columns("VendorId").Visible = False
        tabStoreFiles.Text = "StoredFiles(" & dgrUploadedFiles.RowCount & ")"
        Return True
    End Function
    Private Sub tabStoreFiles_Enter(sender As Object, e As EventArgs) Handles tabStoreFiles.Enter
        LoadStoredFile()
    End Sub


    Private Sub lbkDeleteStoredFile_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDeleteStoredFile.LinkClicked
        If DeleteGridViewRow(dgrStoredFile, ChangeStatus_ByID("Images", "XX" _
                                                           , dgrStoredFile.CurrentRow.Cells("RecId").Value), Conn) Then
            LoadStoredFile()
        End If
    End Sub

    Private Sub lbkFillVendorRmk_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFillSvcDesc.LinkClicked
        Dim strService As String
        Select Case CmbService.Text
            Case "Visa", "Miscellaneous"
                If CmbService.Text = "Miscellaneous" AndAlso cboSubService.Text <> "Urgent" Then
                    strService = "Urgent"
                    Exit Sub
                End If
                Dim frmFillVisa As New frmFillServiceDesc(CmbService.Text, cboSubService.Text)
                If frmFillVisa.ShowDialog = DialogResult.OK Then
                    Select Case cboSubService.Text
                        Case "ResidenceCard", "Urgent"
                            strService = cboSubService.Text
                        Case Else
                            strService = "Visa"
                    End Select

                    TxtMoTaSVC.Text = (frmFillVisa.cboF1.SelectedValue & " " _
                        & frmFillVisa.cboF2.SelectedValue & " " & strService _
                        & " " & frmFillVisa.cboF3.SelectedValue _
                        & " " & frmFillVisa.cboF4.SelectedValue).ToString.Trim

                End If
        End Select
    End Sub

    Private Sub lbkUpdateSvcDesc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUpdateSvcDesc.LinkClicked
        If GridSVC.CurrentRow Is Nothing Then Exit Sub

        Dim lstQuerries As New List(Of String)
        lstQuerries.Add("Update Dutoan_Item set Brief ='" & TxtMoTaSVC.Text & "' where RecId=" _
                        & GridSVC.CurrentRow.Cells("Recid").Value)
        lstQuerries.Add("insert into ActionLog (TableName, ActionBy, doWhat,LinkId, F1, F2) values ('Dutoan_Item','" _
            & myStaff.SICode & "','Edit ServiceDesc'," & GridSVC.CurrentRow.Cells("RecId").Value _
            & ",'" & GridSVC.CurrentRow.Cells("Brief").Value & "','" & TxtMoTaSVC.Text & "')")

        If UpdateListOfQuerries(lstQuerries, Conn) Then
            LoadGridSVC(GridTour.CurrentRow.Cells("RecId").Value)
            RefreshSvc()
        End If


    End Sub

    Private Function StoreFile(intDutoanid As Integer, strFilePath As String, strBrief As String _
                               , intVendorId As Integer) As Integer
        Dim lstQuerries As New List(Of String)
        Dim strFiletype As String = ""
        Dim strFileName As String = ""
        Dim intFileId As Integer = -1
        Dim ArrBreaks As String() = Split(strFilePath, "\")
        Dim strDestPath As String

        strFileName = ArrBreaks(ArrBreaks.Length - 1)
        ArrBreaks = Split(strFileName, ".")
        If ArrBreaks.Length <> 2 Then
            MsgBox("File name must contain 1 DOT Character only!", MsgBoxStyle.Critical)
            Return -1
        End If
        strFiletype = ArrBreaks(1).ToLower


        Try
            lstQuerries.Add("insert into Images(FileType, Dutoan_TourID, Brief, StorePurpose" _
                            & ", IntVal, Status, FstUser) values ('" & strFiletype & "'," & intDutoanid _
                            & ",N'" & strBrief & "',1," & intVendorId & ",'OK','" & myStaff.SICode & "')")
            If Not UpdateListOfQuerries(lstQuerries, Conn, True, intFileId) Then
                Return -1
            Else
                strDestPath = "\\dataserver\Lip\NonAir\" & intDutoanid & "\" & intFileId & "." & strFiletype
                If Not System.IO.Directory.Exists("\\dataserver\Lip\NonAir\" & intDutoanid) Then
                    System.IO.Directory.CreateDirectory("\\dataserver\Lip\NonAir\" & intDutoanid)
                End If
                System.IO.File.Copy(strFilePath, strDestPath, True)
            End If


        Catch ex As Exception
            MsgBox("Unable to Store file" & vbNewLine & ex.Message)
        End Try


    End Function

    Private Sub lbkUpdatePNR1A_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUpdatePNR1A.LinkClicked
        If GridSVC.CurrentRow Is Nothing Then Exit Sub

        'Chi dung cho Hotel
        If GridSVC.CurrentRow.Cells("Service").Value <> "Accommodations" Then Exit Sub

        If txtPnr1A.TextLength <> 6 Then
            MsgBox("Invalid RLOC 1A!")
            txtPnr1A.Focus()
            Exit Sub
        End If
        Dim strQuerry As String = "Update Dutoan_Item set PNR1A='" & txtPnr1A.Text _
            & "' where RecId=" & GridSVC.CurrentRow.Cells("RecId").Value
        If ExecuteNonQuerry(strQuerry, Conn) Then
            LoadGridSVC(GridTour.CurrentRow.Cells("RecId").Value)
        End If
    End Sub

    Private Sub lbkCmd1A_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCmd1A.LinkClicked
        Dim lstCmd As New List(Of String)
        Dim strCityCode As String = String.Empty
        Dim strCheckInDate As String = String.Empty
        Dim strCheckOutDate As String = String.Empty
        Dim arrHotelDetails As String()
        Dim arrCmds(0 To 3) As String
        If GridSVC.CurrentRow Is Nothing Then Exit Sub
        If GridSVC.CurrentRow.Cells("Service").Value <> "Accommodations" Then Exit Sub

        With GridSVC.CurrentRow
            If Not .Cells("PaxName").Value.ToString.Contains("/") _
               AndAlso MsgBox("Pax name must have slash. Do you want to add it into PaxName", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim strNewPaxName As String = Replace(.Cells("PaxName").Value, " ", "/",, 1)
                If ExecuteNonQuerry("update dutoan_item set PaxName='" & strNewPaxName _
                                 & "' where RecId=" & .Cells("Recid").Value, Conn) Then
                    .Cells("PaxName").Value = strNewPaxName
                Else
                    MsgBox("Unable to change name!")
                    Exit Sub
                End If
            End If

            strCityCode = ScalarToString("Supplier", "Address_CityCode", "RecId=" _
                                 & .Cells("SupplierId").Value)
            If strCityCode.Trim.Length <> 3 Then
                MsgBox("You must update City code for this hotel in RAS first!")
                Exit Sub
            ElseIf ScalarToString("[CityCode]", "City", "City='" & strCityCode & "'") = "" Then
                MsgBox("You must update City code for this hotel in RAS first!")
                Exit Sub
            End If
            arrHotelDetails = Split(.Cells("BRIEF").Value, "_")
            strCheckInDate = Format(CDate(arrHotelDetails(2)), "ddMMM")
            strCheckOutDate = Format(CDate(arrHotelDetails(3)), "ddMMM")
            If strCheckInDate = "" Or strCheckOutDate = "" Then
                MsgBox("Unable to find check in/check out date!")
                Exit Sub
            End If
            lstCmd.Add("HU 1A HK1 " & strCityCode & " " & strCheckInDate & "-" & strCheckOutDate _
                       & "/" & .Cells("Supplier").Value & "/CF-" _
                       & .Cells("RecId").Value & "/RQ-" & .Cells("CCurr").Value & .Cells("Cost").Value)
            lstCmd.Add("NM1" & .Cells("PaxName").Value & ";AP;TKOK;RF" & myStaff.SICode & ";ER")

            Dim frmShow As New frmShowTextBoxes(lstCmd)
            frmShow.ShowDialog()
            frmShow.Dispose()
        End With
    End Sub

    Private Sub tabDNTT_Click(sender As Object, e As EventArgs) Handles tabDNTT.Click

    End Sub

    Private Sub GridTour_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridTour.CellClick
        If e.RowIndex < 0 Then Exit Sub
        Me.LblPreview.Visible = True
        Me.LblSaveTour.Visible = True
        Me.LblOrderBV.Visible = True
        Me.LblAddBancFee.Visible = False
        Me.LblAddMerchantFee.Visible = False

        MyCust.CustID = Me.GridTour.CurrentRow.Cells("CustID").Value
        mintNbrOfPax = TxtQty.Text

        With GridTour
            If .CurrentRow.Cells("Channel").Value = "CS" Then
                OptCWT.Checked = True
            Else
                OptLCL.Checked = True
            End If
            CmbCust.SelectedIndex = CmbCust.FindStringExact(.CurrentRow.Cells("CustShortName").Value)
        End With

        If Me.GridTour.CurrentRow.Cells("BillingBy").Value = "CRD" Then Me.LblAddMerchantFee.Visible = True

        LoadGridAdj()
        Me.CmbVendorAdj.DataSource = GetDataTable("select distinct vendorID as VAL, Vendor as DIS from dutoan_item where status<>'XX' and dutoanID=" &
                Me.GridTour.CurrentRow.Cells("RecID").Value)
        Me.CmbVendorAdj.DisplayMember = "DIS"
        Me.CmbVendorAdj.ValueMember = "VAL"
        Me.CmbCurrAdj.DataSource = GetDataTable("select distinct cCurr from dutoan_item where status<>'XX' and dutoanID=" &
                Me.GridTour.CurrentRow.Cells("RecID").Value)
        Me.CmbCurrAdj.DisplayMember = "cCurr"

        Me.LblSettlement.Visible = True
        LoadGridSVC(Me.GridTour.CurrentRow.Cells("RecID").Value)

        If GridSVC.RowCount = 0 Then
            txtPaxName.Text = GridTour.CurrentRow.Cells("Traveller").Value
        End If

        For i As Int16 = 0 To Me.GridSVC.RowCount - 1
            If ScalarToString("Supplier", "Address_CountryCode", "RecID=" & Me.GridSVC.Item("SupplierID", i).Value) <> "VN" Then
                Me.LblAddBancFee.Visible = True
                Exit For
            End If
        Next

        LoadCmb_VAL(Me.CmbPmtRQVendor, "Select distinct VendorID as VAL, Vendor as DIS from dutoan_item where dutoanID=" & Me.GridTour.CurrentRow.Cells("recID").Value & " and status <>'XX' and vendorID <>2")
        Me.GridPmtRQTC.DataSource = Nothing

        Select Case Me.GridTour.CurrentRow.Cells("Status").Value
            Case "RR"
                LckLblUndoFinalize.Visible = True
                lbkCreateAopQueue.Visible = True
                Me.LblAddSVC.Visible = False
                Me.LblQuote.Visible = False
                Me.LblSvcCfm.Visible = False
                Me.LblDeleteTour.Visible = False
                lbkSaveRequiredData.Visible = False
                LblDocSent.Visible = False
            Case Else
                lbkCreateAopQueue.Visible = False
                Me.LblAddSVC.Visible = True
                Me.LblQuote.Visible = True
                Me.LblSvcCfm.Visible = True
                If GridTour.CurrentRow.Cells("Status").Value <> "XX" Then
                    Me.LblDeleteTour.Visible = True
                    lbkSaveRequiredData.Visible = True
                End If
                LblDocSent.Visible = True
        End Select

        Me.LblSaveSvc.Visible = Me.LblAddSVC.Visible
        Me.LblDeleteSVC.Visible = Me.LblSaveSvc.Visible

        'If InStr("OK_OC", Me.GridTour.CurrentRow.Cells("Status").Value) > 0 Then
        '    Me.LblDocSent.Visible = True
        'End If

        If InStr("OK_OC", Me.GridTour.CurrentRow.Cells("Status").Value) > 0 Then
            Me.LckLblFinalize.Visible = True
        ElseIf InStr("RR", Me.GridTour.CurrentRow.Cells("Status").Value) > 0 Then
            Me.LckLblFinalize.Visible = False
        End If

        txtBooker.Text = Me.GridTour.CurrentRow.Cells("Contact").Value
        Me.cmbTraveler.Text = Me.GridTour.CurrentRow.Cells("Traveller").Value
        Me.TxtBrief.Text = Me.GridTour.CurrentRow.Cells("Brief").Value
        Me.TxtEmail.Text = Me.GridTour.CurrentRow.Cells("Email").Value
        Me.CmbBilling.Text = Me.GridTour.CurrentRow.Cells("BillingBy").Value
        Me.TxtPRNo.Text = Me.GridTour.CurrentRow.Cells("PRNO").Value
        Me.TxtOwner.Text = Me.GridTour.CurrentRow.Cells("Owner").Value
        Me.TxtIONo.Text = Me.GridTour.CurrentRow.Cells("IONO").Value
        Me.txtRefCode.Text = Me.GridTour.CurrentRow.Cells("RefNo").Value

        LoadBuAdmin(GridTour.CurrentRow.Cells("CustShortName").Value)
        Me.CmbBUAdmin.SelectedIndex = CmbBUAdmin.FindStringExact(GridTour.CurrentRow.Cells("BUAdmin").Value)

        Me.cmbLocation.Text = Me.GridTour.CurrentRow.Cells("Location").Value
        Me.CmbDept.Text = Me.GridTour.CurrentRow.Cells("Dept").Value
        Me.TxtStartDate.Value = Me.GridTour.CurrentRow.Cells("Sdate").Value
        Me.txtEndDate.Value = Me.GridTour.CurrentRow.Cells("EDate").Value
        '   Me.txtEventDate.Value = Me.GridTour.CurrentRow.Cells("EventDate").Value
        Me.txtTcode.Text = Me.GridTour.CurrentRow.Cells("TCode").Value
        txtFileId.Text = GridTour.CurrentRow.Cells("FileId").Value
        txtQuotationId.Text = GridTour.CurrentRow.Cells("QuotationFile").Value
        txtWindowId.Text = GridTour.CurrentRow.Cells("WindowId").Value
        TxtPax.Text = GridTour.CurrentRow.Cells("Pax").Value
        chkWzAir.Checked = GridTour.CurrentRow.Cells("WzAir").Value

        mdteSelectedSdate = GridTour.CurrentRow.Cells("Sdate").Value
        txtCostCenter.Text = GridTour.CurrentRow.Cells("KeyMap").Value
        txtVatPct.Text = GridTour.CurrentRow.Cells("VatPct").Value

        'If Me.GridTour.CurrentRow.Cells("CustShortname").Value.ToString.Contains("SANOFI") Then
        '    If Me.LstCCenter.Items.Count = 0 Then
        '        GenListCCenter(Me.GridTour.CurrentRow.Cells("CustID").Value)
        '    End If
        '    For j As Int16 = 0 To Me.LstCCenter.Items.Count - 1
        '        Me.LstCCenter.SetItemChecked(j, False)
        '    Next
        '    For i As Int16 = 0 To Me.GridTour.CurrentRow.Cells("KeyMap").Value.ToString.Split("|").Length - 1
        '        For j As Int16 = 0 To Me.LstCCenter.Items.Count - 1
        '            If Me.LstCCenter.Items(j).ToString = Me.GridTour.CurrentRow.Cells("KeyMap").Value.ToString.Split("|")(i) Then
        '                Me.LstCCenter.SetItemChecked(j, True)
        '            End If
        '        Next
        '    Next
        'Else
        '    Me.LstCCenter.Items.Clear()
        'End If
        'If Me.GridTour.CurrentRow.Cells("Channel").Value = "CS" Then
        '    GridGO.Visible = True
        '    'If Me.GridGO.Visible Or Me.GridTour.CurrentRow.Cells("Channel").Value = "CS" Then
        '    Dim dTbl As DataTable = GetDataTable("select FName, FValue from cwt.dbo.sir where status='OK' and rcpid=" _
        '                                & Me.GridTour.CurrentRow.Cells("RecID").Value _
        '                                & " and Prod='NonAir' and CustID=" & MyCust.CustID)
        '    For i As Int16 = 0 To dTbl.Rows.Count - 1
        '        For j As Int16 = 0 To Me.GridGO.RowCount - 1
        '            If Me.GridGO.Item(0, j).Value = dTbl.Rows(i)("FName") Then
        '                Me.GridGO.Item(1, j).Value = dTbl.Rows(i)("FValue")
        '                Exit For
        '            End If
        '        Next
        '    Next
        'End If
        Me.GridVendorInforUpdate.DataSource = GetDataTable("select RecID, ShortName, HoaDon from Vendor where cat in ('NH','KS') and status='OK' " &
            "and recID in (select VendorID from Dutoan_Item where DutoanID=" & Me.GridTour.CurrentRow.Cells("RecID").Value & ")")
        Me.GridVendorInforUpdate.Columns(0).Visible = False
        Me.GridVendorInforUpdate.Columns(1).Width = 200
        Me.GridVendorInforUpdate.Columns(2).Width = 56
        Me.LckLblUpdateVendorAddr.Visible = False

        'If TabControl1.SelectedTab.Name = "Data4GO" Then
        '    ShowCDRs()
        'End If
        If txtBooker.Text = "ZPERSONAL" Then
            GridGO.Visible = False
        Else
            GridGO.Visible = True
            LoadGridGO(False)
            LoadFileList()
            LoadStoredFile()
        End If
    End Sub

    Private Sub tabDNTT_Enter(sender As Object, e As EventArgs) Handles tabDNTT.Enter
        If GridTour.CurrentRow Is Nothing Then Exit Sub
        Dim strLoadVendor As String
        'Dim strLoadCur As String = "Select distinct CCurr As value from Dutoan_item " _
        '                        & " where Vendor<>'TransViet' and Status='OK' and DutoanId=" _
        '                        & GridTour.CurrentRow.Cells("RecId").Value

        strLoadVendor = "Select Distinct i.Vendor as Display,s.VendorId as Value" _
                        & " from Dutoan_Item i " _
                        & " left join Supplier s on i.SupplierId=s.RecId" _
                        & " where i.Status='OK' and i.DutoanId=" & GridTour.CurrentRow.Cells("RecId").Value _
                        & " and i.Vendor<>'TransViet'"

        LoadComboDisplay(cboVendor, strLoadVendor, Conn)
        'LoadCombo(cboRqCur, strLoadCur, Conn)
        RefreshPayment()
        mblnLoadDNTTcompleted = True
        If cboVendor.Items.Count > 0 Then
            cboVendor.SelectedIndex = 0
        End If
    End Sub
    Private Function RefreshPayment() As Boolean
        Dim strQuerryRequested As String = "Select d.*,v.CAT from DNTT d" _
                                        & " left join Vendor v On d.VendorId=v.RecId" _
                                        & " where d.Status <> 'XX' and RefId=" _
                                        & GridTour.CurrentRow.Cells("RecId").Value
        Dim strSumTtl2Vendor As String = "select sum(i.Qty*i.Cost) as TTLtoVendor from Dutoan_item i" _
                                        & " where i.Status='OK' and i.Vendor<>'TransViet' and i.DutoanId=" _
                                        & GridTour.CurrentRow.Cells("RecId").Value

        Dim strSumPaymentDue As String = " Select VendorId,Vendor,CCurr" _
                                        & " ,sum(Qty*Cost) as TTLtoVendor " _
                                        & " from Dutoan_Item where VendorId<>2 and Status='OK' and DutoanId=" & GridTour.CurrentRow.Cells("RecId").Value _
                                        & " group by VendorId,Vendor,CCurr"
        dgrRequested.DataSource = Nothing
        dgrSumPaymentDue.DataSource = Nothing
        LoadDataGridView(dgrRequested, strQuerryRequested, Conn)
        LoadDataGridView(dgrSumPaymentDue, strSumPaymentDue, Conn)

        dgrRequested.Columns("Amount").DefaultCellStyle.Format = "#,##0.00"

        dgrSumPaymentDue.Columns("TTLtoVendor").DefaultCellStyle.Format = "#,##0.00"
        'dgrSumPaymentDue.Columns("Requested").DefaultCellStyle.Format = "#,##0.00"
    End Function

    Private Sub txtRqAmt_Enter(sender As Object, e As EventArgs) Handles txtRqAmt.Enter
        Dim decRequested As Decimal
        Dim decTotal As Decimal

        decRequested = ScalarToDec("DNTT", "isnull(sum(Amount),0)", "RefId=" & GridTour.CurrentRow.Cells("RecId").Value _
                                    & " And VendorId=" & cboVendor.SelectedValue _
                                    & " And Curr='" & cboRqCur.Text & "'")
        decTotal = ScalarToDec("Dutoan_item", "isnull(sum(Qty*Cost),0)", "DutoanId=" & GridTour.CurrentRow.Cells("RecId").Value _
                                    & " and Vendor='" & cboVendor.Text _
                                    & "' and CCurr='" & cboRqCur.Text & "' and Status='OK'")
        mdecMaxVendorAmt = decTotal - decRequested
        If decRequested = decTotal Then
            txtRqAmt.Text = 0
        Else
            txtRqAmt.Text = decTotal - decRequested
        End If

    End Sub

    Private Sub lbkCreateDNTT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateDNTT.LinkClicked
        Dim strQuerry As String
        Dim strTvc As String

        If cboRqFOP.Text = "" Then
            MsgBox("You must select FOP")
            Exit Sub
        End If
        strTvc = MyCust.Tvc
        If strTvc = "" Then
            MsgBox("Unable to find TVC for selected customer")
            Exit Sub
        End If
        'If ScalarToInt("Misc", "RecId", "Status='OK' and CAT='CustNameInGroup'" _
        '                    & " And VAL='GDS' and intVal=" _
        '                    & GridTour.CurrentRow.Cells("CustId").Value) > 0 Then
        '    strTvc = "GDS"
        'Else
        '    strTvc = "TVTR"
        'End If

        With GridTour.CurrentRow
            strQuerry = "insert into DNTT (FOP, Curr, Amount, Ref, RefId, VendorID" _
                & ", Vendor, FstUser,Status, App, Counter,TVC,City) values ('" & cboRqFOP.Text _
                & "','" & cboRqCur.Text & "'," & CDec(txtRqAmt.Text) & ",'" & .Cells("Tcode").Value _
                & "'," & .Cells("RecId").Value & "," & cboVendor.SelectedValue _
                & ",'" & cboVendor.Text & "','" & myStaff.SICode & "','OK','RAS','" & myStaff.Counter _
                & "','" & strTvc & "','" & myStaff.City & "')"
        End With
        If ExecuteNonQuerry(strQuerry, Conn) Then
            RefreshPayment()
        End If
    End Sub

    Private Sub lbkRefreshPayment_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRefreshPayment.LinkClicked
        RefreshPayment()
    End Sub

    Private Sub cboVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboVendor.SelectedIndexChanged
        If Not mblnLoadDNTTcompleted Then Exit Sub
        Dim strLoadCur As String = "Select distinct CCurr As value from Dutoan_item " _
                                & " where Vendor<>'TransViet' and Status='OK' and DutoanId=" _
                                & GridTour.CurrentRow.Cells("RecId").Value _
                                & " and Vendor='" & cboVendor.Text & "'"

        LoadCombo(cboRqCur, strLoadCur, Conn)
        txtRqAmt.Text = 0
    End Sub
    Private Function PrintDNTT() As Boolean
        Dim objXls As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim objWsh As New Microsoft.Office.Interop.Excel.Worksheet

        objWbk = objXls.Workbooks.Add(My.Application.Info.DirectoryPath & "\R12_DeNghiThanhToan.xlt")
        objWsh = objWbk.Sheets("RPT")
        objWsh.Activate()

        With dgrRequested.CurrentRow
            objWsh.Range("A1").Value = "YÊU CẦU THANH TOÁN" & "(" & .Cells("FOP").Value & ")"
            If .Cells("Counter").Value = "N-A" Then
                objWsh.Range("C4").Value = ""
            End If
            objWsh.Range("C5").Value = "[" & .Cells("CAT").Value & "]" & .Cells("Vendor").Value
            objWsh.Range("C6").Value = .Cells("Ref").Value
            objWsh.Range("C8").Value = .Cells("Curr").Value & " " & Format(.Cells("Amount").Value, "#,##0")

            objWsh.Range("C16").Value = myStaff.ShortName
            objWsh.Range("C16").Value = ""
            For Each objGraphic As Object In objWsh.Shapes
                Select Case objGraphic.Name
                    Case "Rectangle 1", "Rectangle 2"
                        objGraphic.visible = False
                    Case Else
                        'bo qua
                End Select
            Next
            objWsh.Range("I4").Value = ""
        End With
        objWsh.PrintOutEx()
        objWbk.Close(False)
        objXls.Quit()
        Return True
    End Function
    Private Sub lbkPrint_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPrint.LinkClicked
        If dgrRequested.CurrentRow Is Nothing Then Exit Sub

        If ScalarToString("DNTT", "FOP", "RecId=" & dgrRequested.CurrentRow.Cells("RecId").Value) = "BTF" Then
            MsgBox("Bank Transfer Request must be printed by Manager")
            Exit Sub
        End If
        If PrintDNTT() Then

        End If

    End Sub

    Private Sub lbkPush2AOP_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        If Not ExecuteNonQuerry(InsertTcode4AOP(txtTcode.Text, myStaff.City), Conn) Then
            MsgBox("Unable to update to AOP. Please report NMK!")
        End If
    End Sub

    Private Sub lbkSelectCostCenter_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectCostCenter.LinkClicked
        Dim tblCostCenters As DataTable = GetDataTable("Select Value as CostCenter from Cwt.Dbo.Go_CustSettings where Status='OK' and Category='CostCenter' and CustId=" _
                                                       & CmbCust.SelectedValue & " order by Value")
        Dim frmShow As New frmSelectMultiRows(tblCostCenters, "Select Cost Center", "CostCenter")
        If frmShow.ShowDialog = DialogResult.OK Then
            txtCostCenter.Text = frmShow.SelectedValue
        End If

    End Sub

    Private Sub txtRqAmt_Leave(sender As Object, e As EventArgs) Handles txtRqAmt.Leave

        If IsNumeric(txtRqAmt.Text) AndAlso mdecMaxVendorAmt >= txtRqAmt.Text Then
            txtRqAmt.Text = FormatNumber(txtRqAmt.Text, 0,,, True)
            lbkCreateDNTT.Visible = True
        Else
            MsgBox("Invalid Requested Amount!")
        End If

    End Sub

    Private Function ClearTcodeLabel() As Boolean
        TxtPRNo.Text = ""
        TxtIONo.Text = ""
        TxtOwner.Text = ""
        txtRefCode.Text = ""
        Return True
    End Function
    Private Function LoadTcodeLabel(strCustShortName As String) As Boolean
        Dim tblLabels As DataTable = GetDataTable("Select VAL1 As control, VAL2 as Text from ras12.dbo.MISC where cat='tcodelabel'")

            For Each objRow As DataRow In tblLabels.Rows
            Me.Controls.Find(objRow("control"), True)(0).Text = objRow("Text")
        Next
        Return True
    End Function

    Private Sub lbkSelectPlace_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectPlace.LinkClicked
        If CmbService.Text = "Meal" Then
            If MsgBox("Place=" & CmbVendor.Text, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                txtPlace.Text = CmbVendor.Text
                Exit Sub
            End If
        End If
        Dim frmSelect As New frmSelectPlace
        If frmSelect.ShowDialog = DialogResult.OK Then
            txtPlace.Text = frmSelect.dgrPlace.CurrentRow.Cells("Value").Value
        End If
    End Sub

    Private Sub lbkCreateAopQueue_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateAopQueue.LinkClicked
        If GridTour.CurrentRow Is Nothing Then Exit Sub

        If AopQueueExist(GridTour.CurrentRow.Cells("Tcode").Value) Then
            Exit Sub
        End If

        If Not CreateAopQueueNonAir(GridTour.CurrentRow.Cells("Tcode").Value) Then
            MsgBox("Unable to update AOP for " & GridTour.CurrentRow.Cells("Tcode").Value)
        ElseIf Not ExecuteNonQuerry("Update AopQueue set Status='OK' where Status='--' and TrxCode='" & GridTour.CurrentRow.Cells("Tcode").Value _
                                        & "'", Conn) Then
            MsgBox("Unable to update AOP for " & GridTour.CurrentRow.Cells("Tcode").Value)
        End If

    End Sub

    Private Sub lbkViewAccount_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewAccount.LinkClicked
        If cboVendor.Text <> "" Then
            Dim tblVendor As DataTable = GetDataTable("select top 1 * from Vendor where Status='OK' and ShortName='" _
                                                      & cboVendor.Text & "'")
            If tblVendor.Rows.Count = 0 Then
                MsgBox("Vendor not Found")
            Else

                MsgBox("AccountName: " & tblVendor.Rows(0)("AccountName") _
                       & vbNewLine & "AccountNumber: " & tblVendor.Rows(0)("AccountNumber") _
                       & vbNewLine & "BankName: " & tblVendor.Rows(0)("BankName"))
            End If

        End If
    End Sub

    Private Sub lbkLinkMainSvc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkLinkMainSvc.LinkClicked
        If GridSVC.CurrentRow Is Nothing Then Exit Sub
        Dim frmLink As New frmLinkMainSvcNonAir(GridSVC.CurrentRow)
        If frmLink.ShowDialog() = DialogResult.OK Then
            LoadGridSVC(GridTour.CurrentRow.Cells("RecId").Value)
        End If
    End Sub

    Private Sub lbkBookerSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkBookerSelect.LinkClicked

        Dim strQuerry As String = "Select BookerName,Email from cwt.dbo.CWT_Bookers where CustId=" & MyCust.CustID _
            & " order by BookerName,email"
        Dim tblBooker As DataTable = GetDataTable(strQuerry)
        Dim frmSelectBooker As New frmShowTableContent(tblBooker, "Select Booker", "BookerName", "BookerName")
        If frmSelectBooker.ShowDialog = DialogResult.OK Then
            TxtEmail.Text = frmSelectBooker.SelectedRow.Cells("Email").Value
            txtBooker.Text = frmSelectBooker.SelectedRow.Cells("BookerName").Value
        End If
    End Sub

    Private Sub lbkClone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone.LinkClicked
        Dim tblCustomer As DataTable = GetDataTable("Select RecId as CustId,CustShortName" _
                    & " from CustomerList where RecId in" _
                    & "(Select CustId from Cust_Detail where Cat='Channel'" _
                    & " And Val in ('CS','LC') and Status='OK') order by CustShortName")
        Dim frmSelect As New frmShowTableContent(tblCustomer, "Select Customer", "CustShortName")

        Dim strNewTcode As String

        If frmSelect.ShowDialog = DialogResult.OK Then
            Dim lstQuerries As New List(Of String)
            Dim intNewRec As Integer
            Dim intNewCustId As Integer = ScalarToInt("CustomerList", "TOP 1 RecId", "Status='OK' and CustShortName='" _
                                                      & frmSelect.SelectedValue & "'")
            GenTourCode(Me.TxtStartDate.Value, frmSelect.SelectedValue, "")
            CmbCust.Text = frmSelect.SelectedValue
            strNewTcode = txtTcode.Text
            lstQuerries.Add("insert Dutoan_Tour (TCode, Pax, CustShortName, SDate, EDate" _
                            & ", Brief, Contact, Email, BillingBy, QuoteValid, DLCfm, Traveller" _
                            & ", CustID, Status, FstUser, KeyMap, Owner, PRNO, IONo, RefNo" _
                            & ", BUAdmin, Location, RMK, Dept, EventDate, RequiredData" _
                            & ", FileId, WindowId, QuotationFile, WzAir, RcpId, VatInvId, VatPct) " _
                            & " select '" & strNewTcode & "', Pax,'" & frmSelect.SelectedValue & "', SDate, EDate" _
                            & ", Brief, Contact, Email, BillingBy, QuoteValid, DLCfm, Traveller" _
                            & ", " & intNewCustId & ", 'OK','" & myStaff.SICode & "', KeyMap, Owner, PRNO, IONo, RefNo" _
                            & ", BUAdmin, Location, RMK, Dept, EventDate, RequiredData" _
                            & ", FileId, WindowId, QuotationFile, WzAir, RcpId, VatInvId, VatPct" _
                            & " from DuToan_Tour where RecId=" & GridTour.CurrentRow.Cells("RecId").Value)
            If UpdateListOfQuerries(lstQuerries, Conn, True, intNewRec) Then
                lstQuerries.Clear()
                lstQuerries.Add("insert Dutoan_Item (DuToanID, Q, Service, CCurr, Unit, Qty, Cost" _
                                & ", VAT, isVATIncl, Supplier, SVC_Status, PmtMethod, Status" _
                                & ", VendorID, Price, FstUser, Brief, AmtInCostCurr, ChargePer, SVCDate" _
                                & ", ROE,TRVLPay, SupplierRMK, PmtStatus, NeedDeposit, Vendor, MU" _
                                & ", SupplierID, BookOnly, CostOnly, TrxCount, PaxName, ZeroFeeReason" _
                                & ", NonCompliance, Cost4CWT, SubService, VAT4Vendor" _
                                & ", VatInvId, Pnr1A, Place, PaxCount, MainItem)" _
                                & " select " & intNewRec _
                                & ",Q, Service, CCurr, Unit, Qty, Cost" _
                                & ", VAT, isVATIncl, Supplier, SVC_Status, PmtMethod, Status" _
                                & ", VendorID, Price, FstUser, Brief, AmtInCostCurr, ChargePer, SVCDate" _
                                & ", ROE,TRVLPay, SupplierRMK, PmtStatus, NeedDeposit, Vendor, MU" _
                                & ", SupplierID, BookOnly, CostOnly, TrxCount, PaxName, ZeroFeeReason" _
                                & ", NonCompliance, Cost4CWT, SubService, VAT4Vendor" _
                                & ", VatInvId, Pnr1A, Place, PaxCount, MainItem" _
                                & " from Dutoan_Item where DutoanId=" & GridTour.CurrentRow.Cells("RecId").Value _
                                & " and Status='OK'")
                lstQuerries.Add("insert SIR (RCPID, CustID, Prod, Fname, FValue, Status, FstUser)" _
                                & "select " & intNewRec & "," & intNewCustId & ",Prod, Fname, FValue, Status" _
                                & ",'" & myStaff.SICode & "'" _
                                & " from SIR where RcpId=" & GridTour.CurrentRow.Cells("RecId").Value _
                                & " and Status='OK' and Prod='N-A'")
                If UpdateListOfQuerries(lstQuerries, Conn) Then
                    MsgBox("New Tcode:" & strNewTcode)
                    Reset()
                    txtSearchValue.Text = strNewTcode
                    cboSearchBy.SelectedIndex = 0
                    If Not ExecuteNonQuerry(InsertTcode4AOP(strNewTcode, myStaff.City), Conn) Then
                        MsgBox("Unable to update Tour code  to AOP. Please report NMK!")
                    End If
                    LoadgridTour()
                End If
            End If

        End If
    End Sub

    Private Sub GridTour_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridTour.CellContentClick

    End Sub

    Private Sub lbkPush2Act_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPush2Act.LinkClicked
        If GridTour.CurrentRow Is Nothing Then Exit Sub
        If ExecuteNonQuerry("Update DuToan_Tour set ActStatus='QC' where RecId=" _
                            & GridTour.CurrentRow.Cells("RecId").Value, Conn) Then
            LoadgridTour()
        End If
    End Sub

    Private Sub lbkRejectByAct_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRejectByAct.LinkClicked
        If GridTour.CurrentRow Is Nothing Then Exit Sub
        Dim tblReasons As DataTable = GetDataTable("Select Val as Reason from Misc" _
                                    & " where Cat='TcodeRejectReason' and Status='OK' order by Val")
        Dim frmReason As New frmShowTableContent(tblReasons, "Chọn lý do Reject", "Reason")
        If frmReason.ShowDialog = DialogResult.OK Then
            If ExecuteNonQuerry("Update DuToan_Tour set ActStatus='RJ'" _
                                & ",RejectReason='" & frmReason.SelectedValue _
                                & "' where RecId=" _
                                & GridTour.CurrentRow.Cells("RecId").Value, Conn) Then
                LoadgridTour()
            Else
                MsgBox("Không Reject được " & GridTour.CurrentRow.Cells("Tcode").Value)
            End If
        End If

    End Sub

    Private Sub lbkAddSlash_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddSlash.LinkClicked
        If txtPaxName.Text.Contains("/") Then
            Dim arrBreaks As String() = txtPaxName.Text.Split(" ")
            Dim i As Integer
            If arrBreaks.Length > 1 Then
                txtPaxName.Text = arrBreaks(0) & "/"
                For i = 1 To arrBreaks.Length - 1
                    txtPaxName.Text = txtPaxName.Text & " " & arrBreaks(i)
                    ExecuteNonQuerry("update Dutoan_item set PaxName='" & txtPaxName.Text _
                                     & " where RecId" = GridSVC.CurrentRow.Cells("RecId").Value & "'", Conn)
                    RefreshSvc()
                Next
            End If
        End If
    End Sub

    Private Sub lbkQuotation_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkQuotation.LinkClicked
        MakeQuotation(True)
    End Sub

    Private Function LoadBuAdmin(strCustShortName As String) As Boolean
        CmbDept.Items.Clear()
        CmbBUAdmin.DataSource = Nothing
        CmbBUAdmin.Items.Clear()

        If strCustShortName.Contains("SANOFI") Then
            LoadCmb_MSC(Me.CmbBUAdmin, "Select Value as VAL from cwt.dbo.GO_MiscWzDate where catergory='SANOFI_BU_ADMIN' and status='OK'")
            LoadCmb_MSC(Me.CmbDept, "SELECT [value] as VAL FROM [CWT].[dbo].[GO_MiscWzDate] where catergory='SANOFI_DEPT' and status='OK'")
        Else
            LoadCombo(CmbBUAdmin, "select Value from cwt.dbo.Go_CustSettings where Status='OK' and Category='BuAdmin' and CustId=" _
                      & MyCust.CustID & " order by Value", Conn)
            LoadCombo(CmbDept, "select Value from cwt.dbo.Go_CustSettings where Status='OK' and Category='Department' and CustId=" _
                      & MyCust.CustID & " order by Value", Conn)
        End If


        Return True
    End Function
End Class

