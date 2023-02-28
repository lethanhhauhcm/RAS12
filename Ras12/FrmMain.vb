Imports RAS12.MySharedFunctions
Imports System.IO
'Imports Microsoft.Office.Interop.Excel

Public Class frmMain
    'route change 0.0.0.0 mask 0.0.0.0 172.16.1.252
    Private cmd As SqlClient.SqlCommand = Conn.CreateCommand
    Private tblName As String
    Private MyCust As New objCustomer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd-MMM-yy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongTimePattern = "HH:mm"
        Conn_Web.ConnectionString = CnStr_FT
        'Conn_Web .ConnectionString = CnStr_RasHAN

        DDAN = System.Windows.Forms.Application.StartupPath

        ' CHO NAY DE TEST HOA DON VAT KHI CAN
        'pblnTestInv = True
        pblnTT78 = True

        'pblnLogXml = True

        ClearLogFile(30)
        'CreateWebConfigFile()

        myStaff.App = "RAS"
        SetAllMenuStatus(False)

        'check file chay
        'If My.Computer.Name <> "5-247" AndAlso Not Process.GetCurrentProcess().ProcessName.Contains(Environment.MachineName) Then
        '    MsgBox("Vui lòng chạy chương trình từ thư mục Shortcut hoặc bằng chương trình RunMe.")
        '    Application.Exit()
        '    Exit Sub
        'End If


        Dim frmSign As New frmSignIn
        If frmSign.ShowDialog = DialogResult.OK Then
            SignIn(myStaff.SICode)
            InitPanel()
            GenComboValueMain()
            Me.StatusVersion.Text = "Version: " & System.Windows.Forms.Application.ProductVersion
            If myStaff.SICode = "NMK" Then
                padNMK.Visible = True
            Else
                padNMK.Visible = False
            End If
        Else
            Exit Sub
        End If

        'myStaff.CnStr = CnStr
        'MyCust.CnStr = myStaff.CnStr
        'MyAL.CnStr = CnStr

    End Sub
    Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If String.IsNullOrEmpty(myStaff.SICode) Then GoTo ResumeHere


        If Conn.State <> ConnectionState.Open Then Conn.Open()
        If myStaff.City = "SGN" Then
            cmd.CommandTimeout = 512
            If myStaff.SICode = "SYS" Then
                cmd.CommandText = "Exec CleanReportData"
                cmd.ExecuteNonQuery()
            ElseIf myStaff.Counter = "CWT" Then
                cmd.CommandText = "Exec CleanReportData; Exec updateBooker"
                cmd.ExecuteNonQuery()
            ElseIf myStaff.UGroup.Contains("S") Then
                cmd.CommandText = "Exec CleanReportData; Exec UpdateQT_FB"
                cmd.ExecuteNonQuery()
            End If
        ElseIf myStaff.City = "HAN" Then
            'cmd.ExecuteNonQuery()
        End If
        If myStaff.SICode = "SYS" OrElse myStaff.UGroup.Contains("S") Or myStaff.Counter = "CWT" Then
            GoDataConvert()
        End If
        LogInOut("")
        Conn.Close()
ResumeHere:
        Me.Dispose()
        End
    End Sub

    Public Function SignIn(strSignIn As String) As Boolean
        pstrVnDomCities = GetColumnValuesAsString("CityCode", "Airport", "where Country='VN'", "_")
        pubVarBackColor = GetColorForToday()
        cmd.CommandText = "Update tblUser set status='ON' where SICode='" & myStaff.SICode & "'"
        cmd.ExecuteNonQuery()
        LogInOut(myStaff.SICode)
        statusUser.Text = "   User=" & myStaff.SICode & ". Domain=" & MySession.Domain
        HideMenu4HAN()
        MyCust.GenCustList()
        Me.Text = "RAS-" & myStaff.City & " - " & Application.ProductVersion & " - " & myStaff.SICode _
              & " - " & myStaff.StaffId & " - " & MySession.Domain & " - " & myStaff.Counter
        'LogInOut(strSignIn)
    End Function
    Private Function CreateWebConfigFile() As Boolean
        If IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath) <> "RAS12.EXE" Then
            IO.File.Copy(My.Application.Info.DirectoryPath & "\RAS12.EXE.config", Application.ExecutablePath & ".config", True)
        End If
        Return True
    End Function
    Public Sub Conn_StateChange(ByVal sender As Object, ByVal e As System.Data.StateChangeEventArgs)
        If e.CurrentState = ConnectionState.Open Then
            Me.statusConnSate.Text = "Connected"
            Me.statusConnSate.ForeColor = Color.Blue
        Else
            Me.statusConnSate.Text = "Disconnected"
            Me.statusConnSate.ForeColor = Color.Red
        End If
    End Sub
    Private Sub BarLogIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarLogIn.Click
        HideFrame()
        If Me.BarLogIn.Text = "Log In" Then
            Dim frmSignIn As New frmSignIn
            If frmSignIn.ShowDialog = DialogResult.OK Then
                SignIn(myStaff.SICode)
            Else
                Exit Sub
            End If
        Else
            'Me.txtLogInPSW.Text = ""
            myStaff.LogOut()
            LogInOut("")
        End If
    End Sub
    Private Sub CmdLogInCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LogInOut("")
    End Sub

    Private Sub HideMenu4HAN()
        If MySession.POSCode = "3" Then
            Me.PadCorpSales.Visible = False
            Me.PadMICE.Visible = False
            'Me.BarSalesCall.Visible = False
            Me.BarAPGInv_KTT.Visible = False
            Me.BarInvChecker.Visible = False
            Me.BarIATAMaintain.Visible = False
            Me.BarGoToTSP.Visible = False
            Me.BarExportToTSP.Visible = False
        Else
            Me.BarUpdateForExQuay.Visible = False
        End If
    End Sub

    Private Sub CmdChangePSWCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
         CmdReportClose.Click
        HideFrame()
    End Sub

    Private Sub BarUserManager_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarUserManager.Click, BarUserMngerSales.Click, BarFOUserManager.Click
        Dim Mnu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim f As New UserManagement(Mnu.Tag)
        f.ShowDialog()
    End Sub
    Private Sub BarFOIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarFOIssue.Click
        Dim f As Form
        For Each f In Application.OpenForms
            If f.GetType.Name = "FOissueTKT" Then
                f.Activate()
                Exit Sub
            End If
        Next
        f = New FOissueTKT()
        f.ShowDialog()
    End Sub
    Private Sub BarFORefund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarFOEditMoney.Click, BarFOEditFOP.Click, BarReprintTRX.Click, BarFOPrintDeleteRCP.Click, BarFODataCorrection.Click
        Dim f As Form, RCP As String, b As ToolStripItem = CType(sender, ToolStripItem)
        For Each f In Application.OpenForms
            If f.GetType.Name = "FOissueTKT" Then
                f.Activate()
                Exit Sub
            End If
        Next
        RCP = InputBox("Enter Transaction Confirmation Number:", msgTitle).Trim
        If RCP = "" Then Exit Sub
        RCP = RCP.ToUpper
        If b.Tag <> "PRN" AndAlso Not CheckCcRcpEditable(RCP) Then
            MsgBox("Cần yêu cầu kế toán cho phép sửa vì đã trả bằng thẻ tín dụng:" & RCP)
            Exit Sub
        End If
        If b.Tag <> "PRN" AndAlso Not CheckVatedRcp(RCP) Then
            MsgBox("Cần yêu cầu kế toán thuế cho phép sửa vì đã xuất hóa đơn VAT:" & RCP)
            Exit Sub
        End If
        f = New FOissueTKT(b.Tag & "_" & RCP)
        f.ShowDialog()
    End Sub
    Private Sub DoReport()
        GenRPTName()
        HideFrame()
        Me.PnlReport.Visible = True
    End Sub
    Private Sub GenRPTName()
        Dim WhoIs As String = Me.CmdReportClose.Tag
        Dim strSQL As String = String.Format("select VAL, val as ReportName from MISC where Status='OK' and cat like '%{0}%-RPTName'", WhoIs)
        If WhoIs = "C" Then
            If InStr(myStaff.AAccess, "YY") = 0 Then
                strSQL = String.Format("{0} and (substring(val,4,2)='YY' or substring(val,4,2) in {1})", strSQL, myStaff.AAccess)
            End If
            Me.CmdReportPrint.Enabled = True
            Me.LckCmdRPTexport.Visible = False
        ElseIf WhoIs = "A" Then
            Me.LckCmdRPTexport.Visible = True
            Me.CmdReportPrint.Enabled = False
        ElseIf WhoIs = "S" Then
            Me.LckCmdRPTexport.Visible = False
            Me.CmdReportPrint.Enabled = False
        End If

        Me.GridRPTname.DataSource = GetDataTable(strSQL)
        Me.GridRPTname.Columns(0).Visible = False
        Me.GridRPTname.Columns(0).Width = 0
        Me.GridRPTname.Columns(1).Width = 200
        For i As Int16 = 0 To Me.GridRPTname.RowCount - 1
            Me.GridRPTname.Item(1, i).Value = Me.GridRPTname.Item(1, i).Value.Replace("CC_", "")
            Me.GridRPTname.Item(1, i).Value = Me.GridRPTname.Item(1, i).Value.Replace("CS_", "")
            Me.GridRPTname.Item(1, i).Value = Me.GridRPTname.Item(1, i).Value.Replace("CT_", "")
            Me.GridRPTname.Item(1, i).Value = Me.GridRPTname.Item(1, i).Value.Replace("SR_", "")
            Me.GridRPTname.Item(1, i).Value = Me.GridRPTname.Item(1, i).Value.Replace("RP_", "")
            Me.GridRPTname.Item(1, i).Value = Me.GridRPTname.Item(1, i).Value.Replace("YY_", "")
            Me.GridRPTname.Item(1, i).Value = Me.GridRPTname.Item(1, i).Value.Replace("TS_", "")
            Me.GridRPTname.Item(1, i).Value = Me.GridRPTname.Item(1, i).Value.Replace("AR_", "")
            Me.GridRPTname.Item(1, i).Value = Me.GridRPTname.Item(1, i).Value.Replace("CR_", "")
            Me.GridRPTname.Item(1, i).Value = Me.GridRPTname.Item(1, i).Value.Replace("PP_", "")
        Next
    End Sub
    Private Sub BarFOReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarFOReport.Click
        Me.CmdReportClose.Tag = "C"

        DoReport()
    End Sub
    Private Sub CmdReportRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdReportRun.Click, LckCmdRPTexport.Click
        Dim fName As String, varFrm As Date, varTo As Date
        Dim cmd As Button = CType(sender, Button)
        Dim RPTNO As String, varPreview As String = "V"
        If InStr(cmd.Name.ToUpper, "EXPORT") > 0 Then varPreview = "F"
        If Me.GridRPTname.CurrentCell.Value = "" Or Me.CmbReportCounter.Text = "" Then
            MsgBox("Please Select Report Name or Airline/Counter", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        fName = Me.GridRPTname.CurrentRow.Cells(0).Value

        If myStaff.City = "SGN" AndAlso fName.Contains("CC_CT_TKT Listing CWT") Then
            PrepareTktListing4Cwt()
            ComputeVat()
        End If

        varFrm = Me.txtReportFrom.Text
        varTo = Me.txtReportTo.Text
        If InStr("GSA_EDU", MySession.Domain) > 0 Then
            RPTNO = Me.CmbReportCounter.Text
        Else
            RPTNO = "TS"
        End If
        InHoaDon(DDAN, fName, varPreview, RPTNO, varFrm, varTo, Me.CmbRPTcust.SelectedValue _
                 , Me.CmbReportCounter.Text, MySession.Domain, Me.CmdReportClose.Tag)
        'Log report
        'LogReport("RAS", fName, myStaff.SICode, myStaff.City)
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarCorporateAccount.Click, BarCustList.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New AddCustomer(b.Tag)
        f.ShowDialog()
    End Sub
    Private Sub statusConnSate_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles statusConnSate.DoubleClick
        If Me.statusConnSate.Text = "Disconnected" Then
            Try
                Conn.Open()
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub BarViewAcc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarBOViewAcc.Click, BarViewTRXList.Click, BarFOView.Click, BarTRXListing_CS.Click, BarTRXListing_KTT.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New View(b.Tag)
        f.ShowDialog()
    End Sub

    Private Sub CloseBC_AL()
        Dim fName As String, MyAns As Integer, RPTNO As String
        Dim varFrm As Date, varTo As Date, LstRPT As String
        If Me.GridRPTname.CurrentCell.Value = "" Then
            MsgBox("Please Select Report Name", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        MyAns = MsgBox("Do You Want to Authorize Report and Print It?", MsgBoxStyle.YesNo Or MsgBoxStyle.Critical Or MsgBoxStyle.DefaultButton2, msgTitle)
        If MyAns = vbNo Then Exit Sub

        varTo = Me.txtReportTo.Text & " 23:59"
        varFrm = Me.txtReportFrom.Text & " 00:01"
        MyAns = 0
        MyAns = ScalarToInt("TKT", "count(RPTNO)", "DOI <'" & varTo & "' and RPTNo = '' and AL='" & Me.CmbReportCounter.Text & "'")
        fName = Me.GridRPTname.CurrentRow.Cells(0).Value
        RPTNO = Me.CmbReportCounter.Text
        RPTNO = RPTNO & Format(varTo.Month, "00") & Format(varTo, "yy")
        RPTNO = RPTNO & "_" & Format(varFrm, "dd") & "-" & Format(varTo, "dd")
        If MyAns = 0 Then
            MyAns = MsgBox("All Tickets Have Been Reported. Wanna Reprint It Only?", MsgBoxStyle.Critical Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, msgTitle)
            If MyAns = vbNo Then
                Me.CmdReportPrint.Visible = False
            Else
                InHoaDon(DDAN, fName, "P", RPTNO, Me.txtReportFrom.Text, Me.txtReportTo.Text, 0, Me.CmbReportCounter.Text, MySession.Domain)
            End If
            Exit Sub
        End If
        Try
            cmd.CommandText = "Drop table #RPTNO"
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        cmd.CommandText = "select * into #RPTNO from ActionLog where tableName='RPTLISTAL' "
        cmd.ExecuteNonQuery()
        LstRPT = ScalarToString("#RPTNO", " F1", " left(F1,7) ='" & RPTNO.Substring(0, 7) & "' " &
            "and F1 not in (select F1 from #RPTNO where DoWhat='U' " &
            "and left(F1,7) ='" & RPTNO.Substring(0, 7) & "') and F2='OK' order by ActionDate desc")
        If LstRPT <> "" Then
            MyAns = CInt(LstRPT.Substring(10, 2))
            If MyAns > varFrm.Day Then
                MsgBox("Last Report Was Upto " & MyAns & Format(varFrm, "MMM") & ". Plz Check Your Input", MsgBoxStyle.Critical, msgTitle)
                Exit Sub
            End If
        End If
        cmd.CommandText = String.Format("update TKT set RPTNO='{0}' where doi <'{1}' and RPTNO+AL='{2}'", RPTNO, varTo, Me.CmbReportCounter.Text)
        cmd.ExecuteNonQuery()
        '^_^20220801 mark by 7643 -b-
        'cmd.CommandText = UpdateLogFile("RPTLISTAL", "C", RPTNO, "OK", "", "", "", "", "", "")
        'cmd.ExecuteNonQuery()

        'InHoaDon(DDAN, fName, "P", RPTNO, Me.txtReportFrom.Text, Me.txtReportTo.Text, 0, Me.CmbReportCounter.Text, MySession.Domain, Me.CmbReportCounter.Text)
        '^_^20220801 mark by 7643 -e-
        '^_^20220801 modi by 7643 -b-
        If InHoaDon(DDAN, fName, "P", RPTNO, Me.txtReportFrom.Text, Me.txtReportTo.Text, 0, Me.CmbReportCounter.Text, MySession.Domain, Me.CmbReportCounter.Text) Then
            cmd.CommandText = UpdateLogFile("RPTLISTAL", "C", RPTNO, "OK", "", "", "", "", "", "")
            cmd.ExecuteNonQuery()
        End If
        '^_^20220801 modi by 7643 -e-
    End Sub
    Private Sub CloseBC_Daily()
        Dim fName As String, MyAns As Integer, RPTNO As String, NgayChuaDongBC As Date
        Dim varFrm As Date, varTo As Date
        If Me.txtReportTo.Value.Date <> Me.txtReportFrom.Value.Date Or Me.txtReportFrom.Value.Date > Now.Date Then
            MsgBox("Invalid Date Input", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        varTo = Me.txtReportTo.Text & " 23:59"
        varFrm = Me.txtReportFrom.Text & " 00:00"
        If Me.GridRPTname.CurrentCell.Value = "" Then
            MsgBox("Please Select Report Name", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        NgayChuaDongBC = ScalarToDate("RCP", " top 1 FstUpdate", "fstupdate >'" & CutOverDateCloseRPT & " 23:59' and " &
                                    " fstupdate <'" & varFrm & "' and RPTNO=''" &
                                    " and AL='" & Me.CmbReportCounter.Text & "' and status not in ('XX','NA')" &
                                    " and City+Location+Counter='" & MySession.City + MySession.Location + MySession.Counter & "'")
        If NgayChuaDongBC > DateSerial(2016, 5, 1) Then
            MsgBox("Sales Report For " & Format(NgayChuaDongBC, "dd-MMM-yy") & " Has not Been Closed. Plz Close It Before Running This Report", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        MyAns = MsgBox("Do You Want to Authorize Report and Print It?", MsgBoxStyle.YesNo Or MsgBoxStyle.Critical Or MsgBoxStyle.DefaultButton2, msgTitle)
        If MyAns = vbNo Then Exit Sub
        If InStr("GSA_EDU", MySession.Domain) > 0 Then
            RPTNO = "KT_" & Me.CmbReportCounter.Text & Format(varTo.Month, "00") & Format(varTo, "yy")
        Else
            RPTNO = "KT_TS" & Format(varTo.Month, "00") & Format(varTo, "yy")
        End If
        RPTNO = RPTNO & "_"
        RPTNO = RPTNO & Format(varTo, "dd")
        fName = ScalarToString("actionlog", " F1", "tablename='RPTLIST' and dowhat='C' and F1='" & RPTNO & "' and F2='OK' and f3='" & MySession.Counter & "'")

        cmd.CommandText = "update RCP set rptno=@RPTNO where RPTNO='' and FstUpdate between @varFrm and @varTo and status='OK' and al=@AL" &
            " and City+Counter+Location=@CityCounterLoc"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@RPTNO", SqlDbType.VarChar).Value = RPTNO
        cmd.Parameters.Add("@varFrm", SqlDbType.VarChar).Value = varFrm
        cmd.Parameters.Add("@VarTo", SqlDbType.VarChar).Value = varTo
        cmd.Parameters.Add("@AL", SqlDbType.VarChar).Value = Me.CmbReportCounter.Text
        cmd.Parameters.Add("@CityCounterLoc", SqlDbType.VarChar).Value = MySession.City + MySession.Counter + myStaff.Location
        cmd.ExecuteNonQuery()

        cmd.CommandText = UpdateLogFile("RPTLIST", "C", RPTNO, "OK", MySession.Counter, varFrm.ToShortDateString, varTo.ToShortDateString, "", "", "")
        cmd.ExecuteNonQuery()
        fName = Me.GridRPTname.CurrentRow.Cells(0).Value
        InHoaDon(DDAN, fName, "P", RPTNO.Substring(3, 2), Me.txtReportFrom.Text, Me.txtReportTo.Text, 0, Me.CmbReportCounter.Text, MySession.Domain, Me.CmdReportClose.Tag)
    End Sub
    Private Sub CmdReportPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdReportPrint.Click
        If Me.CmdReportPrint.Tag = "SR" Then
            CloseBC_AL()
        Else
            CloseBC_Daily()
        End If
    End Sub

    Private Sub GenCustList4RPT(ByVal pLoaiKhach As String)
        If pLoaiKhach = "CR" Then
            LoadCmb_VAL(Me.CmbRPTcust, MyCust.List_CR)
        ElseIf pLoaiKhach = "PP" Then
            LoadCmb_VAL(Me.CmbRPTcust, MyCust.List_PP)
        ElseIf pLoaiKhach = "CS" Then
            LoadCmb_VAL(Me.CmbRPTcust, MyCust.List_CS)
        ElseIf pLoaiKhach = "CT" Then
            LoadCmb_VAL(Me.CmbRPTcust, MyCust.List_CT)
        ElseIf pLoaiKhach = "LC" Then
            LoadCmb_VAL(Me.CmbRPTcust, MyCust.List_LC)
        End If
    End Sub
    Private Sub PrepareTktListing4Cwt()
        Try

            'custid
            Select Case CmbRPTcust.Text
                Case "NOVARTIS VN"
                    Dim strQuerry As String = "Select t.RecId,RequiredData,al.doccode, a.TKNO,SRV,Carrier,a.DOI" _
                        & " from Go_Travel t" _
                        & " left join go_air a on t.RecID=a.TravelID" _
                        & " left join airline al on al.alcode=a.carrier" _
                        & " where Status='OK'" _
                        & " and t.DOI between '" & CreateFromDate(txtReportFrom.Value) _
                        & "' and '" & CreateToDate(txtReportTo.Value) _
                        & "' and CustId=" & CmbRPTcust.SelectedValue _
                        & " and a.DOI between '" & CreateFromDate(txtReportFrom.Value) _
                        & "' and '" & CreateToDate(txtReportTo.Value) _
                        & "' order by t.DOI, a.RecId"

                    Dim strError As String = ""
                    Dim strConnection As String = "server=118.69.81.103;uid=user_cwt;pwd=VietHealthy@170172#;database=CWT;Connect Timeout=300"
                    Dim objCwt As New SqlClient.SqlConnection
                    objCwt.ConnectionString = strConnection
                    Dim tblGoTravel As New DataTable
                    Dim strSplitBy As String
                    Dim lstQuerries As New List(Of String)
                    objCwt.Open()
                    tblGoTravel = GetDataTable(strQuerry, objCwt)
                    For Each objRow As DataRow In tblGoTravel.Rows
                        Dim blnErrorFound As Boolean = False
                        strSplitBy = GetRequiredDataValueByDataCode("UDID30", objRow("RequiredData"))
                        If strSplitBy <> "" Then
                            lstQuerries.Add("Update Go_Travel set SplitBy='" & strSplitBy _
                                        & "' where RecId=" & objRow("RecId"))
                        ElseIf objRow("SRV") = "R" Then
                            strSplitBy = ScalarToString("cwt.dbo.Go_Travel", "SplitBy", "RecId=" _
                                                & "(Select top 1 TravelId from cwt.dbo.go_air where Srv='S' and Carrier='" _
                                                & objRow("Carrier") & "' and Tkno='" & objRow("Tkno") _
                                                & "' order by RecId desc) ")
                            If strSplitBy = "" Then
                                blnErrorFound = True
                            End If
                        Else
                            blnErrorFound = True
                        End If
                        If blnErrorFound Then
                            strError = strError & vbNewLine & objRow("DocCode") & objRow("tkno") _
                                & "-" & objRow("SRV") & "-" & objRow("DOI")
                        End If
                    Next
                    If strError <> "" Then
                        Dim frmNotify As New frmShowMessageInTextBox("Tickets with Missing BU ADMIN", strError)
                        frmNotify.ShowDialog()
                    End If

                    If lstQuerries.Count > 0 AndAlso Not UpdateListOfQuerries(lstQuerries, objCwt) Then
                        MsgBox("Unable to find BU Admin for Novartis vn Ticket listing")
                    End If

                    objCwt.Close()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub
    Private Sub BarBOReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarBOReport.Click
        Me.CmdReportClose.Tag = "A"
        If myStaff.City = "SGN" Then
            GoDataConvert()
            CalcLastDOF()
            LinkTktGoAir()
            RelinkTktAir(Now.AddDays(-31), Now)
        End If

        DoReport()
    End Sub
    Private Sub BarApplyPayments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarApplyPayments.Click, BarQuickInvoicing.Click, BarPaymentFollowUp.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New frmDC_Pax(b.Tag)
        f.ShowDialog()
    End Sub

    Private Sub statusConnSate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles statusConnSate.Click
        If Conn.State <> ConnectionState.Open Then Conn.Open()
    End Sub


    Private Sub BarClearPendingPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarClearPendingPayment.Click, BarClearDEB.Click, BarAddCC2FOP.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New ClearPendingPmt(b.Tag)
        f.ShowDialog()
    End Sub

    Private Sub BarVNPCT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarCreditExtensionCounter.Click, BarDEBApproval.Click, BarPPandCreditLimit.Click,
        BarPaymentOption.Click, BarCreditApprovalCS.Click, BarApproveITP.Click, BarApproveITP4Counter.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)

        Dim f As New frmMISC(b.Tag)
        f.ShowDialog()


        'If b.Name.Contains("ITP") Then
        '    Dim whichApp As String = InputBox("RAS Or FLX?", msgTitle, "RAS")
        '    If whichApp = "RAS" Then
        '        Dim f As New frmMISC(b.Tag)
        '        f.ShowDialog()
        '    ElseIf whichApp = "FLX" AndAlso myStaff.SICode <> "LHA" Then
        '        FLX_DEBControl.ShowDialog()
        '    Else
        '        Exit Sub
        '    End If
        'Else
        '    Dim f As New frmMISC(b.Tag)
        '    f.ShowDialog()
        'End If
    End Sub


    Private Sub BarFODepositHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarRQ4DepHandlingCounter.Click, BarRQForDepositHandlingSALES.Click, BarDepositHandler.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New RQXulyDatCoc(b.Tag)
        f.ShowDialog()
    End Sub

    Private Sub BarIssueInvoiceBO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New InvoicePrinting(b.Tag, "")
        f.ShowDialog()
    End Sub

    Private Sub BarInvoiceHandlerFO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarInvoiceHandlerFO.Click, BarInvHandler_KTT.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New InvHandler(b.Tag)
        f.ShowDialog()
    End Sub
    Private Sub BarReportSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarReportSales.Click
        Me.CmdReportClose.Tag = "S"
        DoReport()
    End Sub

    Private Sub BarUpdateBegBalance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        NhapSoDu.ShowDialog()
    End Sub
    Private Sub BarUNCPrinting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarUNCPrinting.Click
        UNC.ShowDialog()
    End Sub

    Private Sub BarUNCCompanyName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarUNCCannedText.Click, BarUNCReprinting.Click, BarALINVFollowUp.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New UNC_support(b.Tag, "ACT")
        f.ShowDialog()
    End Sub
    Private Sub BarINVFromSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
         BarINVFromSupplier.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New UNC_support(b.Tag, "ACT", "N-A")
        f.ShowDialog()
    End Sub
    Private Sub BarPivotUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarPivotUser.Click
        PivotUser.ShowDialog()
    End Sub
    Private Sub BarReportingPeriod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarReportingPeriod.Click
        KyBaoCao.ShowDialog()
    End Sub

    Private Sub BarADMACMupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarADMACMupdate.Click
        ACM_ADM.ShowDialog()
    End Sub

    Private Sub BarBalancePreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarBalancePreview.Click, BarViewBalance.Click, BarBalancePreviewKT.Click, BarBalancePreviewCS.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New BalancePreview(b.Tag)
        f.ShowDialog()
    End Sub

    Private Sub BarReportCS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarReportCS.Click

        Me.CmdReportClose.Tag = "S"
        DoReport()
    End Sub
    Private Sub BarCustInfor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarCustInfor.Click
        CustInfor.ShowDialog()
    End Sub
    Private Sub BarFoxSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        BarFoxSettingBLC.Click, BarFoxSettingSMS.Click, BarChangePSWAndStopSales.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New FoxSetting(b.Tag)
        f.ShowDialog()
    End Sub

    Private Sub BarFoxRefund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarFoxRefund.Click
        FoxRefund.ShowDialog()
    End Sub
    Private Sub CmbReportCounter_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbReportCounter.VisibleChanged
        Me.Label11.Visible = Me.CmbReportCounter.Visible
        If Me.CmbReportCounter.Visible Then
            If Me.CmdReportClose.Tag = "C" Then ' quay thi shortlist ten quay lai khoi lam nham bao cao
                Me.CmbReportCounter.Items.Clear()
                If MySession.Domain = "EDU" Then
                    Me.CmbReportCounter.Items.Add("XX")
                ElseIf MySession.Domain = "TVS" Then
                    Me.CmbReportCounter.Items.Add("TS")
                Else
                    LoadCmb_MSC(Me.CmbReportCounter, myStaff.TVA & " And al Not in ('TS','XX')")
                End If
            Else
                LoadCmb_MSC(Me.CmbReportCounter, myStaff.TVA)
            End If
        End If
    End Sub
    Private Sub BarCS_ServiceFee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarCS_ServiceFee.Click
        CWT_ServiceFee.ShowDialog()
    End Sub
    Private Sub BarLockTheUnlocked_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarLockTheUnlocked.Click
        LockTheUnLocked.ShowDialog()
    End Sub
    Private Sub BarForEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarForEx.Click
        UpdateForEx.ShowDialog()
    End Sub
    Private Sub BarChargesAndFees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarChargesAndFees.Click, BarTVComm.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New ChargeAndFee(b.Tag)
        f.ShowDialog()
    End Sub
    Private Sub BarCustChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarCustChannel.Click
        CustChannelLevel.ShowDialog()
    End Sub

    Private Sub BarServiceFee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarServiceFee.Click
        ServiceFee.ShowDialog()
    End Sub

    Private Sub BarBGKeeper_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarBGKeeper.Click
        BGUpdate.Show()
    End Sub
    Private Sub GridRPTname_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridRPTname.CellContentClick
        Try
            'Me.CmbReportCounter.Visible = False
            If Me.GridRPTname.CurrentRow.Cells(0).Value.ToUpper.Substring(0, 2) = "SR" Then
                Me.CmdReportPrint.Visible = True
                Me.CmdReportPrint.Tag = "SR"
                Me.CmbRPTcust.Visible = False
                If Me.GridRPTname.CurrentRow.Cells(0).Value.ToUpper.Substring(3, 2) = "YY" Then
                    Me.CmbReportCounter.Visible = True
                End If
            ElseIf Me.GridRPTname.CurrentRow.Cells(0).Value.ToUpper.Substring(0, 2) = "AR" Then
                Me.CmdReportPrint.Visible = True
                Me.CmbRPTcust.Visible = False
                Me.CmdReportPrint.Tag = "AR"
                Me.CmbReportCounter.Visible = True
                LoadCmb_MSC(Me.CmbRptNo, "Select distinct RPTNo as val from rcp where status<>'XX' and RPTNO<>'' and city+Counter+sbu='" &
                    MySession.City + MySession.Counter + MySession.Domain & "' order by rptno desc")

            ElseIf Me.GridRPTname.CurrentRow.Cells(0).Value.ToUpper.Substring(0, 2) = "CC" Then
                Me.CmbRPTcust.Visible = True
                GenCustList4RPT(Me.GridRPTname.CurrentRow.Cells(0).Value.ToUpper.Substring(3, 2))
            Else
                Me.CmdReportPrint.Visible = False
                Me.CmbRPTcust.Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BarPendingXX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarPendingXX.Click
        PendingXX.ShowDialog()
    End Sub

    Private Sub BarF1SUserManager_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarF1SUserManager.Click
        F1SUserManager.ShowDialog()
    End Sub
    Private Sub PromoCodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarPromoCode.Click
        PromoCode.ShowDialog()
    End Sub

    Private Sub BarFrontEndLoss_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarFrontEndLoss.Click
        Dim f As New GSA_MISC()
        f.ShowDialog()
    End Sub
    'Private Sub txtLogInPSW_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
    '    myStaff.SICode = Me.txtLogInSIcode.Text

    '    LoadComboBiz(myStaff.DAccess)
    '    If myStaff.SICode = "SYS" Then Me.CmbCounter.Visible = True
    '    'If myStaff.DAccess.Contains("GSA_TVS") Or myStaff.SICode = "SYS" Then
    '    '    'Me.CmbBiz.Visible = True
    '    'Else
    '    '    'Me.CmbBiz.Visible = False
    '    'End If
    '    'Me.Label12.Visible = Me.CmbBiz.Visible
    '    Me.Label14.Visible = Me.CmbCounter.Visible
    '    Me.StatusVersion.Text = myStaff.DAccess & "|" & myStaff.Counter
    'End Sub
    'Private Sub LoadComboBiz(strDomainAccess As String)
    '    Dim arrDomains As String() = strDomainAccess.Split("_")
    '    Dim i As Integer
    '    CmbBiz.Items.Clear()
    '    For i = 0 To arrDomains.Length - 1
    '        CmbBiz.Items.Add(arrDomains(i))
    '    Next
    '    CmbBiz.SelectedIndex = CmbBiz.Items.Count - 1
    'End Sub
    Private Sub BarCosting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarCosting.Click
        Dim frmCosting As New Costing
        frmCosting.ShowDialog()
        frmCosting.Dispose()
    End Sub
    Private Sub BarUnReportedTKTs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarUnReportedTKTs.Click
        unReportedTickets.ShowDialog()
    End Sub
    Private Sub BarDataFromSQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarDataFromSQL.Click
        Dim f As New Sales("RPT")
        f.Show()
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles BarQuickRef.Click
        MyDoc.ShowDialog()
    End Sub

    Private Sub BarReportNonAir_Click(sender As Object, e As EventArgs) Handles BarReportNonAir.Click
        Me.CmdReportClose.Tag = "N"
        DoReport()
    End Sub
    Private Sub BasrSalesCall_Click(sender As Object, e As EventArgs) Handles BarSalesCall.Click, BarMeetingLogs.Click
        SaleLog.ShowDialog()
    End Sub
    Private Sub OverCreditOverDueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OverCreditOverDueToolStripMenuItem.Click
        OverDue_CreditReport.ShowDialog()
    End Sub

    Private Sub BarNewsUpdate_Click(sender As Object, e As EventArgs) Handles BarNewsUpdate.Click
        NewsUpdater.ShowDialog()
    End Sub

    Private Sub BarUserMapping_Click(sender As Object, e As EventArgs) Handles BarUserMapping.Click
        B2BUserMap.ShowDialog()
    End Sub

    Private Sub BarUpdateForExQuay_Click(sender As Object, e As EventArgs) Handles BarUpdateForExQuay.Click
        UpdateForEx.ShowDialog()
    End Sub

    Private Sub BarIATAMaintain_Click(sender As Object, e As EventArgs) Handles BarIATAMaintain.Click
        Dim f As New BSP_Agent("SALES")
        f.ShowDialog()
    End Sub

    Private Sub BarReportKTT_Click(sender As Object, e As EventArgs) Handles BarReportKTT.Click
        Me.CmdReportClose.Tag = "A"
        If myStaff.City = "SGN" Then
            ReserveBspInv(0, "Reserved for Tax Accounting")
        End If
        DoReport()
    End Sub

    Private Sub BarBulkPrinting_Click(sender As Object, e As EventArgs) Handles BarBulkPrinting.Click
        BulkPrint.ShowDialog()
    End Sub

    Private Sub BarAPGInv_KTT_Click(sender As Object, e As EventArgs) Handles BarAPGInv_KTT.Click
        Dim frmInv As New APG_INV("APGAL", "APGINV")
        frmInv.ShowDialog()
    End Sub

    Private Sub BarInvChecker_Click(sender As Object, e As EventArgs) Handles BarInvChecker.Click
        InvChecker.ShowDialog()
    End Sub

    Private Sub BarVendorInfor_Click(sender As Object, e As EventArgs) Handles BarVendorInfor.Click
        Dim f As New VendorInfor_KTT("KTT")
        f.ShowDialog()

    End Sub

    Private Sub BarUpdateVoucher_Click(sender As Object, e As EventArgs) Handles BarUpdateVoucher.Click
        DiscountManager.ShowDialog()
    End Sub

    Private Sub BarPaymenForVoucher_Click(sender As Object, e As EventArgs) Handles BarPaymenForVoucher.Click
        Payment4VCR.ShowDialog()
    End Sub

    Private Sub BarMappingSupplierVendor_Click(sender As Object, e As EventArgs) Handles BarMappingSupplierVendor.Click
        Dim f As New Vendor_SupplierMapping(0)
        f.ShowDialog()
    End Sub

    Private Sub BarUpdateSupplier_Click(sender As Object, e As EventArgs)
        Supplier.ShowDialog()
    End Sub

    'Private Sub BarUNCCompanyName_Click_1(sender As Object, e As EventArgs) Handles BarUNCCompanyName.Click
    '    MsgBox("Please use Vendor/Supplier menu")
    '    Exit Sub
    '    Dim f As New VendorInfor_KTT("ACC")
    '    f.ShowDialog()

    'End Sub

    Private Sub BarGroupBooking_Click(sender As Object, e As EventArgs) Handles BarGroupBooking.Click
        GroupBooking.ShowDialog()
    End Sub
    Private Sub BarSpecimen_Click(sender As Object, e As EventArgs) Handles BarSpecimen.Click
        Dim AL As String = InputBox("Enter Airline Code ", msgTitle, "NH")
        InHoaDon(DDAN, "R12_VATInvoice_Specimen.xlt", "V", AL, Now, Now, 0, "", "", "")
    End Sub
    'Private Sub CheckToUploadNewVersion()
    '    Dim CurrSetting As String = ScalarToString("MISC", "VAL", "Cat='APPVERSION'")
    '    Dim AppList As String = "RAS-12|COS-14|TSP-15"
    '    Dim SourceFileName As String

    '    SourceFileName = Application.StartupPath & "\SharedFunctions12_1.dll"
    '    If System.IO.File.Exists(SourceFileName) Then
    '        For i As Int16 = 0 To AppList.Split("|").Length - 1
    'UploadFileToFtp(SourceFileName, "ftp://42.117.5.70/" & AppList.Split("|")(i).Replace("-", "") & "/", "transviet", "Abcd1234", "APP")
    '        Next
    '        Kill(SourceFileName)
    '    End If
    '    If Application.ProductVersion <> CurrSetting Then
    '        Shell("D:\D_disc\Exe\forFTP.bat")
    '        SourceFileName = Application.StartupPath & "\Ras12.exe"
    '        UploadFileToFtp(SourceFileName, "ftp://42.117.5.70/ras12/", "transviet", "Abcd1234", "APP")

    '        cmd.CommandText = "update MISC set VAL=@VAL where cat=@Cat" &
    '                            "; update [42.117.5.70].ras12.dbo.MISC set VAL=@VAL where cat=@Cat"
    '        cmd.Parameters.Clear()
    '        cmd.Parameters.AddWithValue("@VAL", Application.ProductVersion)
    '        cmd.Parameters.AddWithValue("@Cat", "APPVERSION")
    '        cmd.ExecuteNonQuery()
    '    End If
    'End Sub
    Private Sub BarNonTVSGNBSPAgents_Click(sender As Object, e As EventArgs) Handles BarNonTVSGNBSPAgents.Click
        Dim f As New BSP_Agent("ACC")
        f.ShowDialog()
    End Sub

    Private Sub BarVATNoForTourDesk_Click(sender As Object, e As EventArgs) Handles BarVATNoForTourDesk.Click
        VATForTD.ShowDialog()
    End Sub
    'Private Sub BarGoToTSP_Click(sender As Object, e As EventArgs) Handles BarGoToTSP.Click
    '    Dim fName As String = GetLastFileName_FullPath("X:\Ras2k7\TSP15\", "TSP15_*.exe")
    '    fName = fName & " " & myStaff.SICode & "|" & myStaff.PSW
    '    Try
    '        Shell(fName)
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Private Sub BarBulkPrintingPilot_Click(sender As Object, e As EventArgs) Handles BarBulkPrintingPilot.Click
        BulkPrint_New.ShowDialog()
    End Sub

    Private Sub BarUNCPreview_Click(sender As Object, e As EventArgs) Handles BarUNCPreview.Click
        Dim f As New UNC_support("EDIT", "KTT")
        f.ShowDialog()
    End Sub

    Private Sub BarExportToTSP_Click(sender As Object, e As EventArgs) Handles BarExportToTSP.Click
        ExportToTSP.ShowDialog()
    End Sub

    Private Sub BarUpdateLastBalance_Click(sender As Object, e As EventArgs) Handles BarUpdateStartBalance.Click
        frmStartBalanceList.ShowDialog()
    End Sub


    Private Sub BarCcUpdate_Click(sender As Object, e As EventArgs) Handles BarCcUpdate.Click, BarCcUpdateNonAir.Click
        frmCcList.ShowDialog()
    End Sub


    Private Sub BarExportToSCB_Click(sender As Object, e As EventArgs) Handles BarExportToSCB.Click
        Dim frmExport2Bank As New SCB("SCB")
        frmExport2Bank.ShowDialog()
    End Sub

    Private Sub BarExportToVCB_Click(sender As Object, e As EventArgs) Handles BarExportToVCB.Click
        Dim frmExport2Bank As New SCB("VCB")
        frmExport2Bank.ShowDialog()
    End Sub
    Private Sub barBankCode_Click(sender As Object, e As EventArgs) Handles barBankCode.Click
        frmCitiBankCode.ShowDialog()
    End Sub

    Private Sub barBankMapping_Click(sender As Object, e As EventArgs) Handles barBankMapping.Click
        frmCitiBankMapping.ShowDialog()
    End Sub

    Private Sub barCiti_Click(sender As Object, e As EventArgs) Handles barCiti.Click
        Dim frmExport2Bank As New SCB("CTB")
        frmExport2Bank.ShowDialog()
    End Sub

    Private Sub BarFOEditSettlement_Click(sender As Object, e As EventArgs) Handles barFOEditRcpInternal.Click
        Dim f As Form, RCP As String, b As ToolStripItem = CType(sender, ToolStripItem)
        For Each f In Application.OpenForms
            If f.GetType.Name = "FOissueTKT" Then
                f.Activate()
                Exit Sub
            End If
        Next
        RCP = InputBox("Enter Transaction Confirmation Number:", msgTitle)
        If RCP = "" Then Exit Sub
        RCP = RCP.ToUpper
        f = New FOissueTKT(b.Tag & "_" & RCP)
        f.ShowDialog()
    End Sub

    Private Sub PadKTT_Click(sender As Object, e As EventArgs) Handles PadKTT.Click

    End Sub

    Private Sub barBooker_Click(sender As Object, e As EventArgs) Handles barBooker.Click
        frmBooker.ShowDialog()
    End Sub

    Private Sub barBLDailyReport_Click(sender As Object, e As EventArgs)
        Dim ofd As New OpenFileDialog

        With ofd

        End With
    End Sub

    Private Sub barUTTR_Click(sender As Object, e As EventArgs) Handles BarUTTR.Click
        frmRunUTTR.ShowDialog()
    End Sub

    Private Sub BarGrpDeposit_Click(sender As Object, e As EventArgs) Handles BarGrpDeposit.Click
        frmGrpDeposit.ShowDialog()

    End Sub

    Private Sub barFundList_Click(sender As Object, e As EventArgs) Handles barFundList.Click
        frmNhFundList.ShowDialog()

    End Sub

    Private Sub barUsageList_Click(sender As Object, e As EventArgs) Handles barUsageList.Click
        frmNhFundUsage.ShowDialog()

    End Sub

    Private Sub barReserveInv_Click(sender As Object, e As EventArgs) Handles barReserveInv.Click
        Dim frmInv As New APG_INV("INV_AL", "INV_AL")
        frmInv.ShowDialog()
    End Sub

    Private Sub BarBOUnReportedTKTs_Click(sender As Object, e As EventArgs) Handles BarBOUnReportedTKTs.Click
        unReportedTickets.ShowDialog()
    End Sub

    Private Sub barPrintTrxMultiRcp_Click(sender As Object, e As EventArgs) Handles barPrintTrxMultiRcp.Click
        frmPrintTrxMultiRcp.ShowDialog()
    End Sub

    Private Sub barVPB_Click(sender As Object, e As EventArgs) Handles barVPB.Click
        Dim frmExport2Bank As New SCB("VPB")
        frmExport2Bank.ShowDialog()
    End Sub

    Private Sub barEditComm_Click(sender As Object, e As EventArgs) Handles barEditComm.Click
        frmEditComm.ShowDialog()

    End Sub



    Private Sub txtLogInPSW_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub barAutoSF_Click(sender As Object, e As EventArgs) Handles barAutoSF.Click
        frmAutoSfList.ShowDialog()
    End Sub

    Private Sub barVendorInv_Click(sender As Object, e As EventArgs) Handles barVendorInv.Click
        frmVendorInv.ShowDialog()
    End Sub

    Private Sub barCustomerGroups_Click(sender As Object, e As EventArgs) Handles barCustomerGroups.Click
        frmCustomerGroups.ShowDialog()
    End Sub

    Private Sub barFocManager_Click(sender As Object, e As EventArgs) Handles barFocManager.Click
        frmFocList.ShowDialog()
    End Sub

    Private Sub barVendorBalance_Click(sender As Object, e As EventArgs) Handles barVendorBalance.Click
        frmVendorBalance.ShowDialog()
    End Sub

    Private Sub barApproveDNTT_Click(sender As Object, e As EventArgs) Handles barListDNTT.Click
        frmListDNTT.ShowDialog()
    End Sub

    Private Sub barExport2Banks2020_Click(sender As Object, e As EventArgs) Handles barExport2Banks2020.Click
        frmExport2Banks.ShowDialog()
    End Sub

    Private Sub barChangeCustHAN_Click(sender As Object, e As EventArgs)

    End Sub
    'Private Function ChangeCustomer()
    '    Dim strQuerry As String
    '    strQuerry = "update BG set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update CC_BLC set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update CC_Setting set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update TT_ChotCongNo set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update Cust_Detail set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update FOP set CustomerID=CustomerID+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update GSALog set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update INV set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update RCP set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update RCP_GSA set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update RCP_TVS set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update TT_ChotCongNo set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update GhiNoKhach set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update TT_KhachTra set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)

    '    strQuerry = "update TT_KyBaoCao set CustId=CustId+80000"
    '    ExecuteNonQuerry(strQuerry, Conn)


    '    Return True

    'End Function

    Private Sub barUploadCustomer2VNPT_Click(sender As Object, e As EventArgs) Handles barUploadCustomer2VNPT.Click
        MassUpLoadCustomer2VNPT()
        MsgBox("Customer upload completed!")
    End Sub

    Private Sub barSupplier_Click(sender As Object, e As EventArgs) Handles barSupplier.Click
        frmSupplierList.ShowDialog()
    End Sub

    Private Sub barMergeVendorAccounts_Click(sender As Object, e As EventArgs)
        Dim tblVendor As System.Data.DataTable
        Dim strGetVendor As String = "Select * from Unc_Accounts where Status<>'xx'" _
            & " And RecId in (Select distinct PayerAccountID from UNC_payments where Status<>'xx')"

        'Conn_Web.Open()
        tblVendor = GetDataTable(strGetVendor, Conn)
        For Each objVendor As DataRow In tblVendor.Rows

            Dim strQuerry As String = "update UNC_payments set  PayerAccountID=" & objVendor("CompanyId") _
                    & " where PayerAccountID=" & objVendor("RecID")
            If Not ExecuteNonQuerry(strQuerry, Conn) Then
                MsgBox("Unable to update PayeeAccountID in UNC_PAYMENTS for " & objVendor("AccountName"))
            End If

        Next
    End Sub
    Private Function RefreshUncpayment4Payee() As Boolean
        Dim tblVendor As System.Data.DataTable
        Dim strGetVendor As String = "Select * from Unc_Accounts where Status<>'xx'" _
            & " And RecId in (Select distinct PayereAccountID from UNC_payments where Status<>'xx')"

        'Conn_Web.Open()
        tblVendor = GetDataTable(strGetVendor, Conn)
        For Each objVendor As DataRow In tblVendor.Rows

            Dim strQuerry As String = "update UNC_payments set  PayeeAccountID=" & objVendor("CompanyId") _
                    & " where PayeeAccountID=" & objVendor("RecID")
            If Not ExecuteNonQuerry(strQuerry, Conn) Then
                MsgBox("Unable to update PayeeAccountID in UNC_PAYMENTS for " & objVendor("AccountName"))
            End If

        Next
        Return True
    End Function
    Private Function MergeVendorAccount() As Boolean
        Dim tblVendor As System.Data.DataTable
        Dim strGetVendor As String = "Select * from Vendor where Status<>'xx'" _
            & " And RecId in (Select companyid from unc_accounts where Status<>'xx') and AccountName=''"

        Conn_Web.Open()
        tblVendor = GetDataTable(strGetVendor, Conn)
        For Each objVendor As DataRow In tblVendor.Rows
            Dim tblAccount As DataTable = GetDataTable("Select top 1 * from unc_accounts where Status<>'xx' and CompanyId=" _
                                                      & objVendor("Recid") & " order by Recid desc")
            If tblAccount.Rows.Count > 0 Then
                Dim objAccount As DataRow = tblAccount.Rows(0)
                Dim strQuerry As String = "update Vendor set  AccountName=N'" & objAccount("AccountName") _
                    & "', Cur='" & Mid(objAccount("AccountNumber"), 1, 3) _
                    & "', AccountNumber='" & Replace(Mid(objAccount("AccountNumber"), 5).Trim, "'", "") _
                    & "', Address=N'" & objAccount("Address") & "', Bank='" & Trim(objAccount("Bank")) _
                    & "', BankName=N'" & objAccount("BankName") _
                    & "', BankAddress=N'" & objAccount("BankAddress") _
                    & "', BankInVietnam='" & objAccount("BankInVietnam") & "', Swift='" & objAccount("Swift") _
                    & "', CIF='" & objAccount("CIF") & "', Country='" & objAccount("Country") _
                    & "' where RecId=" & objVendor("RecId")
                If Not ExecuteNonQuerry(strQuerry, Conn_Web) Then
                    MsgBox("Unable to update account for Vendor " & objVendor("ShortName"))
                End If
            End If

        Next
        Return True
    End Function

    Private Sub barInvoiceList_Click(sender As Object, e As EventArgs) Handles barInvoiceList.Click
        'XacDinhThongTuHoaDon()
        ComputeVat()
        Dim frmList As New frmE_InvList
        frmList.ShowDialog()
    End Sub

    Private Sub barBulkPrint_Click(sender As Object, e As EventArgs) Handles barBulkPrintNoVatDiscount.Click
        Dim frmCreate As New frmSelectTrxForE_Inv(False)
        frmCreate.ShowDialog()
    End Sub

    Private Sub UploadAPGcustToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim strQuerry As String = "Select VAL as IATANO,VAL1 as CustShortName, VAL2 as City, RecID, details as FullName, description as Address from MISC where cat='BSPAGT' and  Left(val1,3) in ('HAN','SGN') and len(val1)=7 and  VAL1 <>''" _
             & " And val1 not in (select custshortname from customerlist)" _
             & " And details<>''"
        Dim tblCust As DataTable
        Dim strEmail As String

        If myStaff.City = "SGN" Then
            strEmail = "ketoan.sgn@transviet.com"
        Else
            strEmail = "ketoan.han@transviet.com"
        End If
        tblCust = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblCust.Rows
            Dim objCust As New objCustomer
            Dim arrAddress As String() = Split(objRow("Address"), "|")

            Dim intCustId As Integer = objCust.AddCustomer(objRow("CustShortName"), objRow("FullName") _
                                                , arrAddress(1), arrAddress(0), strEmail, "", "OK", "")
            If intCustId > 0 Then
                objCust.InsertCustDetail(intCustId, "Channel", "AP", False)
            End If
        Next
    End Sub

    Private Sub barApgCustomers_Click(sender As Object, e As EventArgs) Handles barApgCustomers.Click
        Dim frmList As New frmCustomerList("AP")
        frmList.ShowDialog()
    End Sub

    Private Sub barGetTS4VN1S_Click(sender As Object, e As EventArgs) Handles barGetTS4VN1S.Click
        Dim objOfd As New OpenFileDialog
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim i As Integer

        Dim tblRcp As System.Data.DataTable
        Dim strGetRcpId As String


        With objOfd
            .Filter = "excel files (*.xls)|"
            .ShowDialog()
            If .FileName = "" Then
                Exit Sub
            End If

            Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
            'objExcel.Visible = True

            objWbk = objExcel.Workbooks.Open(.FileName, , True)
            objWsh = objWbk.Sheets("VN 1S")
            With objWsh
                For i = 2 To 10000
                    If .Range("A" & i).Value Is Nothing Then
                        Exit For
                    Else
                        strGetRcpId = "(select top 1 RcpId from tkt where Status<>'xx' and Tkno='" _
                            & FormatRasTkno(.Range("B" & i).Value & .Range("C" & i).Value) _
                            & "' and cast(DOI as date)='" & CreateFromDate(.Range("A" & i).Value) & "')"
                        tblRcp = GetDataTable("Select * from Rcp where RecId=" _
                                              & strGetRcpId, Conn)
                        If tblRcp.Rows.Count > 0 Then
                            .Range("N" & i).Value = tblRcp.Rows(0)("RCPNO")
                            .Range("O" & i).Value = tblRcp.Rows(0)("CustShortName")
                            .Range("P" & i).Value = ScalarToString("FOP", "top 1 Document" _
                                                                   , "Status<>'XX' and Rcpid=" & tblRcp.Rows(0)("RecId") _
                                                                   & " and Document<>''")
                        Else
                            ' MsgBox("")

                        End If
                    End If
                Next
            End With
            objExcel.Visible = True
            MsgBox("Completed")
        End With

    End Sub

    Private Sub barInvoiceListWeb_Click(sender As Object, e As EventArgs) Handles barInvoiceListWeb.Click
        frmE_InvListWeb.ShowDialog()
    End Sub

    Private Sub barE_InvReport_Click(sender As Object, e As EventArgs) Handles barE_InvReport.Click
        frmE_InvReport.ShowDialog()
    End Sub

    Private Sub barVjCredit_Click(sender As Object, e As EventArgs) Handles barAlCredit.Click
        frmAlCredit.ShowDialog()
    End Sub

    Private Sub barShorternCustomerName_Click(sender As Object, e As EventArgs) Handles barShorternCustomerName.Click
        frmShorternCustomerName.ShowDialog()
    End Sub

    Private Sub barPushData2AOP_Click(sender As Object, e As EventArgs)
        frmImport2AOP.ShowDialog()
    End Sub

    Private Sub barGetCustShortNameTourCode_Click(sender As Object, e As EventArgs) Handles barGetCustShortNameTourCode.Click
        Dim objOfd As New OpenFileDialog
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim i As Integer

        Dim tblRcp As System.Data.DataTable

        'Dim strTourCode As String

        With objOfd
            .Filter = "excel files (*.xls)|"
            .ShowDialog()
            If .FileName = "" Then
                Exit Sub
            End If

            Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
            'objExcel.Visible = True

            objWbk = objExcel.Workbooks.Open(.FileName, , True)
            objWsh = objWbk.Sheets("TS")
            With objWsh
                For i = 2 To 10000
                    If .Range("A" & i).Value Is Nothing Then
                        Exit For
                    Else
                        tblRcp = GetDataTable("Select * from Rcp where Status='OK' and Rcpno='" _
                                              & .Range("A" & i).Value & "'", Conn)
                        If tblRcp.Rows.Count > 0 Then
                            .Range("b" & i).Value = tblRcp.Rows(0)("CustShortName")
                            .Range("c" & i).Value = ScalarToString("FOP", "top 1 Document" _
                                                                   , "Status<>'XX' and Rcpid=" & tblRcp.Rows(0)("RecId") _
                                                                   & " and Document<>''")
                        Else
                            ' MsgBox("")

                        End If
                    End If
                Next
            End With
            objExcel.Visible = True
            MsgBox("Completed")
        End With
    End Sub

    Private Sub barVendorGroups_Click(sender As Object, e As EventArgs) Handles barVendorGroups.Click
        frmVendorGroups.ShowDialog()

    End Sub

    Private Sub barAddTcode2AOP_Click(sender As Object, e As EventArgs) Handles barAddTcode2AOP.Click
        Dim strTcode As String = UCase(InputBox("Input Tcode")).Trim

        If ScalarToInt("Dutoan_Tour", "RecId", "Status<>'xx' and Tcode='" & strTcode & "'") = 0 Then
            MsgBox("Invalide Tcode")
            Exit Sub
        End If
        If Not ExecuteNonQuerry(InsertTcode4AOP(strTcode, myStaff.City), Conn) Then
            MsgBox("Unable to update to AOP. Please report NMK!")
        End If
    End Sub

    Private Sub barACR_Click(sender As Object, e As EventArgs) Handles barACR.Click
        frmAcrList.ShowDialog()
    End Sub

    Private Sub CtsPaxCount_Click(sender As Object, e As EventArgs) Handles CtsPaxCount.Click
        Dim frmDateRange As New frmSelectDateRange
        If frmDateRange.ShowDialog = DialogResult.OK Then
            Dim objExcel As New Microsoft.Office.Interop.Excel.Application
            Dim objWbk As Microsoft.Office.Interop.Excel.Workbook

            Dim strDateFilter As String = " between '" & CreateFromDate(frmDateRange.dtpFromDate.Value) _
                                        & "' and '" & CreateToDate(frmDateRange.dtpTodate.Value) & "'"

            Dim strCountDom As String = "select COUNT (*) AS PaxCount from TKT t left join rcp r on t.RCPID=r.RecID" _
                                        & " left join ReportData r1 on t.RecID=r1.TKID" _
                                        & " where t.DocType='etk' and t.Status<>'xx' and t.Qty=1 and r.Counter='CWT'" _
                                        & " and Country='VN' and OrgCountry='VN'" _
                                        & " and t.DOI " & strDateFilter
            Dim strCountVN2YY As String = "select c.CountryName, COUNT (*) as PaxCount from TKT t left join rcp r on t.RCPID=r.RecID" _
                                        & " left join ReportData r1 on t.RecID=r1.TKID" _
                                        & " left join lib.dbo.Country c on c.Country=r1.Country" _
                                        & " where t.DocType='etk' and t.Status<>'xx' and t.Qty=1 and r.Counter='CWT'" _
                                        & " and r1.Country<>'VN' and OrgCountry='VN'" _
                                        & " and t.DOI " & strDateFilter _
                                        & " group by c.CountryName order by c.CountryName"
            Dim strCountYY2VN As String = "select c.CountryName, COUNT (*) as PaxCount from TKT t left join rcp r on t.RCPID=r.RecID" _
                                        & " left join ReportData r1 on t.RecID=r1.TKID" _
                                        & " left join lib.dbo.Country c on c.Country=r1.OrgCountry" _
                                        & " where t.DocType='etk' and t.Status<>'xx' and t.Qty=1 and r.Counter='CWT'" _
                                        & " and r1.Country='VN' and OrgCountry<>'VN'" _
                                        & " and t.DOI " & strDateFilter _
                                        & " group by c.CountryName order by c.CountryName"
            Dim strCountOTHER As String = "select c2.CountryName,c.CountryName, COUNT (*) as PaxCount from TKT t left join rcp r on t.RCPID=r.RecID" _
                                        & " left join ReportData r1 on t.RecID=r1.TKID" _
                                        & " left join lib.dbo.Country c on c.Country=r1.Country" _
                                        & " left join lib.dbo.Country c2 on c2.Country=r1.OrgCountry" _
                                        & " where t.DocType='etk' and t.Status<>'xx' and t.Qty=1 and r.Counter='CWT'" _
                                        & " and r1.Country<>'VN' and OrgCountry<>'VN'" _
                                        & " and t.DOI " & strDateFilter _
                                        & " group by c2.CountryName,c.CountryName order by c2.CountryName, c.CountryName"

            Dim tblTktCount As System.Data.DataTable

            Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
            objExcel.Visible = True

            objWbk = objExcel.Workbooks.Add(My.Application.Info.DirectoryPath & "\CTS Pax Count.xlt")

            objWsh = objWbk.Sheets("DOM")
            tblTktCount = GetDataTable(strCountDom)
            objWsh.Range("A2").Value = tblTktCount.Rows(0)("PaxCount")

            objWsh = objWbk.Sheets("VN2YY")
            tblTktCount = GetDataTable(strCountVN2YY)
            Table2ExcelSheet(tblTktCount, objWsh, False)

            objWsh = objWbk.Sheets("YY2VN")
            tblTktCount = GetDataTable(strCountYY2VN)
            Table2ExcelSheet(tblTktCount, objWsh, False)

            objWsh = objWbk.Sheets("OTHER")
            tblTktCount = GetDataTable(strCountOTHER)
            Table2ExcelSheet(tblTktCount, objWsh, False)

            With objWsh
                objExcel.Visible = True
                objWsh.Activate()
                MsgBox("Completed")
            End With
        End If
    End Sub

    Private Sub FixReportData_Click(sender As Object, e As EventArgs) Handles FixReportData.Click
        Dim tblReportData As DataTable

        tblReportData = GetDataTable("select * from tkt t where Booker='' and Rmk like '%BKR%' and DOI>='01 JUL 2020'")

    End Sub

    Private Sub barCtsCustomers_Click(sender As Object, e As EventArgs) Handles barCtsCustomers.Click

        Dim strQuerry As String = "Select d.VAL as CustType, c.CustFullName, c.CustShortName, ''''+c.CustTaxCode as TaxCode" _
                                & ", case  when m.recid >0  then 'Y' ELSE 'N' end as GDS" _
                                & " from CustomerList c" _
                                & " left join Cust_Detail d On c.RecID=d.CustID And d.Status='ok' and d.CAT='channel'" _
                                & " left join MISC m On m.CAT='CustNameInGroup' and m.Status='ok' and m.VAL='GDS' and m.IntVal=c.RecID" _
                                & " where c.Status<>'xx' and c.RecID>0 and d.VAL in ('CS','LC')" _
                                & " ORDER BY c.RecID"
        Dim tblCust As System.Data.DataTable = GetDataTable(strQuerry)

        Table2Excel(tblCust)

    End Sub

    Private Sub barAddVendor2AOP_Click(sender As Object, e As EventArgs) Handles barAddVendor2AOP.Click
        Dim strVendor As String = UCase(InputBox("Input VendorShortName")).Trim
        Dim intVendorId As Integer = ScalarToInt("Vendor", "RecId", "Status<>'xx' and ShortName='" & strVendor & "'")

        If intVendorId = 0 Then
            MsgBox("Invalide Vendor")
            Exit Sub
        End If
        If Not ExecuteNonQuerry(InsertVendorId4AOP(intVendorId, myStaff.City), Conn) Then
            MsgBox("Unable to update to AOP. Please report NMK!")
        End If
    End Sub

    Private Sub barAddRcp2AOP_Click(sender As Object, e As EventArgs) Handles barAddRcp2AOP.Click
        Dim strRcp As String = ""

        strRcp = InputBox("RCPNO").ToString.Trim
        If strRcp <> "" Then
            If AopQueueExist(strRcp) Then
                Exit Sub
            End If
            If CreateAopQueueAir(strRcp) Then
                MsgBox("AopQueue created!")
            End If
        End If
    End Sub

    Private Sub LongVendorNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LongVendorNameToolStripMenuItem.Click
        Dim objOfd As New OpenFileDialog
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim i As Integer

        Dim tblRcp As System.Data.DataTable

        Dim lstQuerries As New List(Of String)

        With objOfd
            .Filter = "excel files (*.xls)|"
            .ShowDialog()
            If .FileName = "" Then
                Exit Sub
            End If

            Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
            objExcel.Visible = True

            objWbk = objExcel.Workbooks.Open(.FileName, , True)
            objWsh = objWbk.Sheets("Sheet1")
            With objWsh
                For i = 2 To 10000
                    If .Range("A" & i).Value Is Nothing Then
                        Exit For
                    Else
                        lstQuerries.Add("update lib.dbo.Vendor set ShortName='" & Trim(.Range("F" & i).Value) & "',OldName='" & .Range("C" & i).Value _
                                         & "' where RecId=" & .Range("A" & i).Value)

                    End If
                Next
                If UpdateListOfQuerries(lstQuerries, Conn_Web) Then
                    lstQuerries.Clear()
                Else
                    MsgBox("Unable to change ShortName for Vendor")
                    Exit Sub
                End If

                For i = 2 To 10000
                    If .Range("A" & i).Value Is Nothing Then
                        Exit For
                    Else
                        lstQuerries.Clear()
                        lstQuerries.Add("update DuToan_Item set Vendor='" & Trim(.Range("F" & i).Value) & "' where VendorId=" & .Range("A" & i).Value)
                        lstQuerries.Add("update UNC_Payments set ShortName='" & Trim(.Range("F" & i).Value) & "' where ShortName='" & .Range("C" & i).Value & "'")
                        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
                            MsgBox("Unable to change ShortName for Vendor")
                        End If
                    End If
                Next
            End With
            objExcel.Visible = True
            MsgBox("Completed")
        End With
    End Sub

    Private Sub barAddCustomer2AOP_Click(sender As Object, e As EventArgs) Handles barAddCustomer2AOP.Click
        'For i As Integer = 1 To 100
        Dim strCustomer As String = UCase(InputBox("Input CustomerShortName")).Trim
        If strCustomer = "" Then
            Exit Sub
        End If
        Dim intCustomerId As Integer = ScalarToInt("CustomerList", "RecId", "Status<>'xx' and CustShortName='" & strCustomer & "'")

        If intCustomerId = 0 Then
            MsgBox("Invalide Customer")
            Exit Sub
        End If
        If Not ExecuteNonQuerry(InsertCustId4AOP(intCustomerId, myStaff.City), Conn) Then
            MsgBox("Unable to update to AOP. Please report NMK!")
        End If
        'Next

    End Sub

    Private Sub barSiemensQuarterly_Click(sender As Object, e As EventArgs) Handles barSiemensQuarterly.Click
        Dim frmSelect As New frmSelectYearQuarter
        Dim intQuarter As Integer

        Dim dteFrom As Date
        Dim dteTo As Date

        If Not frmSelect.ShowDialog = DialogResult.OK Then
            Exit Sub
        End If
        If Not GetDates4Quarter(frmSelect.cboYear.Text, frmSelect.cboQuarter.Text, dteFrom, dteTo) Then
            Exit Sub
        End If

        Dim objOfd As New OpenFileDialog
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook

        Dim htbTrxColumns As New Hashtable
        Dim htbAncColumns As New Hashtable
        Dim lstDocType4Tkt As New List(Of String)
        With objOfd
            .Filter = "excel files (*.xlsx)|"
            .ShowDialog()
            If .FileName = "" Then
                Exit Sub
            End If

            htbTrxColumns.Add(1, "F")
            htbTrxColumns.Add(2, "G")
            htbTrxColumns.Add(3, "H")
            htbTrxColumns.Add(4, "I")

            htbAncColumns.Add(1, "G")
            htbAncColumns.Add(2, "H")
            htbAncColumns.Add(3, "I")
            htbAncColumns.Add(4, "J")

            Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
            objExcel.Visible = True

            objWbk = objExcel.Workbooks.Open(.FileName, , True)
            objWsh = objWbk.Sheets("Transactions")
            With objWsh
                lstDocType4Tkt.Add("ETK")
                .Range(htbTrxColumns(intQuarter) & 6).Value = SumRev4Tkt("21", "INTL", dteFrom, dteTo, "S", 1, "ALL", lstDocType4Tkt)
                .Range(htbTrxColumns(intQuarter) & 7).Value = SumRev4Tkt("21", "REG", dteFrom, dteTo, "S", 1, "ALL", lstDocType4Tkt)
                .Range(htbTrxColumns(intQuarter) & 6).Value = SumRev4Tkt("21", "DOM", dteFrom, dteTo, "S", 1, "ALL", lstDocType4Tkt)
            End With
            objWsh = objWbk.Sheets("Ancillary")
            With objWsh

            End With
        End With
    End Sub

    Private Sub barTvc4CtsCust_Click(sender As Object, e As EventArgs) Handles barTvc4CtsCust.Click
        frmTvc4CtsCust.ShowDialog()
    End Sub

    Private Sub barManualDiscount_Click(sender As Object, e As EventArgs) Handles barManualDiscount.Click
        frmManualDiscountList.ShowDialog()
    End Sub
    Private Sub barCustomers_Click(sender As Object, e As EventArgs) Handles barCustomers.Click
        Dim b As ToolStripItem = CType(sender, ToolStripItem)
        Dim f As New AddCustomer(b.Tag)
        f.ShowDialog()
    End Sub

    Private Sub AopQueueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AopQueueToolStripMenuItem.Click
        Dim strQuerry As String = "Select top 1 * from aopqueue where status='ok' and querrytype='SQL' order by recid"
        Dim blnCompleted As Boolean
        Dim tblTrx As DataTable
        Dim blnRequeued As Boolean
        Do Until blnCompleted
            blnRequeued = False
            tblTrx = GetDataTable(strQuerry, Conn)
            If tblTrx.Rows.Count = 0 Then
                blnCompleted = True
                Exit Do
            ElseIf tblTrx.Rows(0)("Prod") = "Air" Then
                If CreateAopQueueAir(tblTrx.Rows(0)("TrxCode")) Then
                    blnRequeued = True
                End If
            ElseIf tblTrx.Rows(0)("Prod") = "NonAir" Then
                If CreateAopQueueNonAir(tblTrx.Rows(0)("TrxCode")) Then
                    blnRequeued = True
                End If
            End If
            If blnRequeued Then
                ExecuteNonQuerry("DELETE aopqueue where status='ok' and querrytype='SQL' and TrxCode='" & tblTrx.Rows(0)("TrxCode") & "'", Conn)
            Else
                MsgBox("Unable to requeue " & tblTrx.Rows(0)("TrxCode"))
                blnCompleted = True
            End If
        Loop
    End Sub

    Private Sub barPayBill_Click(sender As Object, e As EventArgs) Handles barPayBill.Click

        'Dim strQuerry As String = "Select b.TxnId as BillId,a.Recid,a.TrxCode,a.RefNumber,a.Counter,a.memo from AopQueue a" _
        '    & " left join AopTravel_Bill b on a.Amount=b.AmountDue and a.RefNumber=b.Refnumber and a.Memo=b.Memo" _
        '    & " where Prod='UNC' and RecId=LinkId and Status='RR'" _
        '    & " and DateDiff(d,LstUpdate,getdate())<14 and FstUpdate>='10 NOV 20' and b.TxnId is not null" _
        '    & " and isPaid='false' and a.refnumber='TR1120-0239'"
        Dim strQuerry As String = "Select b.TxnId as BillId,a.Recid,a.TrxCode,a.RefNumber,a.Counter,a.memo from AopQueue a" _
            & " left join AopTravel_Bill b on abs(a.Amount-b.AmountDue)<1 and a.RefNumber=b.Refnumber and a.Memo=b.Memo" _
            & " where Prod='UNC' and RecId=LinkId and Status='RR'" _
            & " And a.TxnId='' and DateDiff(d,LstUpdate,getdate())<14 and FstUpdate>='10 NOV 20' and b.TxnId is not null" _
            & " and isPaid='false' "
        Dim tblBill As DataTable = GetDataTable(strQuerry, Conn)
        Dim lstQuerries As New List(Of String)
        For Each objRow As DataRow In tblBill.Rows
            If Not CreateAopQueueBillPayment(objRow("TrxCode"), objRow("RefNumber"), objRow("Counter"), objRow("BillId") _
                                             , objRow("RecId"), objRow("Memo")) Then
                Dim strError As String = "Unable to Pay Bill for UNC " & objRow("TrxCode")
                MsgBox(strError)
                Append2TextFile(strError)
            Else

            End If

        Next

    End Sub

    Private Sub barVjInvoice_Click(sender As Object, e As EventArgs) Handles barVjInvoice.Click
        frmVjInvoiceList.ShowDialog()
    End Sub

    Private Sub barImportUnc2AOP_Click(sender As Object, e As EventArgs) Handles barImportUnc2AOP.Click
        Dim strRefNo As String
        Dim tblUnc As DataTable
        strRefNo = InputBox("Nhập số UNC")
        If Not CheckFormatUnc(strRefNo) Then
            MsgBox("Số UNC bị sai định dạng")
            Exit Sub
        End If

        tblUnc = GetDataTable("select top 1 * from Unc_Payments where Status='OK' and RefNo='" & strRefNo & "'")
        If tblUnc.Rows.Count = 0 Then
            MsgBox("Không tìm thấy số UNC tương ứng")
            Exit Sub
        End If

        If ("BTF_CSH").Contains(tblUnc.Rows(0)("FOP")) _
                AndAlso Not ImportAopUNC(tblUnc.Rows(0)("RecId"), tblUnc.Rows(0)("APP"), tblUnc.Rows(0)("Counter")) Then
            MsgBox("Unable to import UNC to AOP:" & tblUnc.Rows(0)("RefNo"))
            Append2TextFile("Unable to import UNC to AOP:" & tblUnc.Rows(0)("RecId"))
        End If
    End Sub

    Private Sub ExportGO_Click(sender As Object, e As EventArgs) Handles barExportGO.Click

        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect to SQL!")
            Exit Sub
        End If
        RefreshedRequiredData()
        frmExportGO.ShowDialog()
    End Sub

    'Private Sub padNMK_Click(sender As Object, e As EventArgs) Handles padNMK.Click
    '    'Dim strQuerry As String = "select t.* " _
    '    '    & " from Dutoan_Tour t" _
    '    '        & " left join Dutoan_Item i on t.RecId=i.DutoanId" _
    '    '        & " left join SIR s on t.RecId=s.RcpId and s.Status='OK' and s.Fname='NBR_OF_LETTER'" _
    '    '        & " where t.CustId=85546 and t.status <>'xx'" _
    '    '        & " and i.Status='OK' and i.Service='Registration' and i.CostOnly=0 and s.FValue is null"
    '    'Dim tblTcode As DataTable = GetDataTable(strQuerry)

    '    'For Each objRow As DataRow In tblTcode.Rows
    '    '    ExecuteNonQuerry("Insert cwt.dbo.SIR (RCPID, PROD, FName, FValue, CustID, fstUser) values (" & objRow("RecId") _
    '    '            & ",'NonAir','NBR_OF_LETTER','" & objRow("RefNo") _
    '    '            & "'," & objRow("CustId") & ",'" & objRow("FstUser") & "')", Conn)
    '    'Next


    'End Sub

    Private Sub barUpdateStaffId_Click(sender As Object, e As EventArgs) Handles barUpdateStaffId.Click
        Dim tblUser As DataTable = GetDataTable("select * from tblUser where Status<>'xx' and StaffId=0")
        Dim tblStaff As DataTable
        Dim objSql As New clsTvcs

        objSql.ConnectionString = CnStr_FT
        objSql.Connect()

        For Each objRow As DataRow In tblUser.Rows
            tblStaff = objSql.GetDataTable("select * from aop.dbo.tblHR_NhanVien where Status='OK' and HR_City='" _
                        & objRow("City") & "' and TenVietTat='" & objRow("SiName") & "'")
            If tblStaff.Rows.Count = 0 Then
                MsgBox("Staff ID not found for " & objRow("City") & " " & objRow("SiName"))
            Else
                ExecuteNonQuerry("Update tblUser set StaffId=" & tblStaff.Rows(0)("RecId") & " where RecId=" & objRow("RecId"), Conn)
            End If
        Next
    End Sub

    Private Sub BarChangePassword_Click(sender As Object, e As EventArgs) Handles BarChangePassword.Click
        If MsgBox("Đổi mật khẩu sẽ thực hiện trên http://transviet.net/ và ảnh hưởng mọi chương trình dùng chung mã nhân viên", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Process.Start("http://transviet.net/changepassword2.aspx?StaffID=" & myStaff.StaffId)
        End If

    End Sub

    Private Sub barCdrHierMap_Click(sender As Object, e As EventArgs) Handles barCdrHierMap.Click
        frmCdrHierMap.ShowDialog()
    End Sub


    Private Sub barRequiredData_Click(sender As Object, e As EventArgs) Handles barRequiredData.Click
        frmRequiedDataList.ShowDialog()
    End Sub

    Private Sub ExportHuntsman_Click(sender As Object, e As EventArgs) Handles barExportHuntsman.Click
        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect to SQL CWT!")
            Exit Sub
        End If

        frmExportHunstman.ShowDialog()
    End Sub

    Private Sub barVNCorpTicket_Click(sender As Object, e As EventArgs) Handles barVNCorpTicket.Click
        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect to SQL CWT!")
            Exit Sub
        End If
        frmVnCorpTkt.ShowDialog()
    End Sub

    Private Sub barClearTestUnc_Click(sender As Object, e As EventArgs) Handles barClearTestUnc.Click
        frmCancelUnc.ShowDialog()
    End Sub

    Private Sub barUpdateCDRs_Click(sender As Object, e As EventArgs) Handles barUpdateCDRs.Click
        Dim tblTkts As System.Data.DataTable
        Dim strQuerry As String
        Dim strTkno As String = FormatRasTkno(InputBox("Input Tkno"))

        If Not pobjTvcs.Connect() Then
            MsgBox("Unable to connect SQL Database CWT")
            Exit Sub
        End If
        strQuerry = "Select RecId,Srv,Tkno,Doi,PaxName, DocType from ras12.dbo.Tkt where Status<>'XX' and tkno='" & strTkno & "'"
        tblTkts = pobjTvcs.GetDataTable(strQuerry)
        If tblTkts.Rows.Count = 0 Then
            MsgBox("Ticket not found!")
        Else
            Dim frmShow As New frmShowTableContent(tblTkts, "Select ticket", "RecId")
            If frmShow.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                With frmShow.dgrInput.CurrentRow
                    RelinkTktAir(.Cells("DOI").Value, .Cells("DOI").Value)
                    If ("ETK,ATK,MCO").Contains(.Cells("DocType").Value) AndAlso Not GenerateTravelRecord(CInt(.Cells("RecId").Value)) Then
                        MsgBox("Unable to create Travel Record!")
                        Exit Sub
                    End If
                    Dim frmUpdate As New frmUpdateCDRs(.Cells("RecId").Value, .Cells("DocType").Value)
                    frmUpdate.ShowDialog()
                End With

            End If
        End If
    End Sub

    Private Sub barPushData2Report_Click(sender As Object, e As EventArgs) Handles barPushData2Report.Click
        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect to SQL!")
            Exit Sub
        End If
        frmPushData2Report.ShowDialog()
        pobjTvcs.Disconnect()
    End Sub

    Private Sub barClearDupCtsAir_Click(sender As Object, e As EventArgs) Handles barClearDupCtsAir.Click
        Dim strQuerry As String = ""
        Dim tblDup As DataTable
        Dim intTkId As Integer
        Dim i As Integer

        If Not pobjTvcs.Connect Then Exit Sub
        tblDup = pobjTvcs.GetDataTable("select * from GO_Air where Tkid in " _
                                       & " (select tkid from GO_Air where tkid>0 and FstUpdate>='01 jan 20' group by Tkid having COUNT(tkid)>1)" _
                                       & " order by Tkid,RecID ")
        For Each objRow As DataRow In tblDup.Rows
            If objRow("Tkid") <> intTkId Then
                intTkId = objRow("Tkid")
            Else
                pobjTvcs.ExecuteNonQuerry("delete go_air where recid=" & objRow("RecId"))
                pobjTvcs.ExecuteNonQuerry("delete go_travel where recid=" & objRow("TravelId"))
            End If
        Next
    End Sub



    Private Sub barTcbInside_Click(sender As Object, e As EventArgs) Handles barTcbInside.Click
        Dim frmExport2Bank As New SCB("TCB", "Inside")
        frmExport2Bank.ShowDialog()
    End Sub

    Private Sub barTcbOutside_Click(sender As Object, e As EventArgs) Handles barTcbOutside.Click
        Dim frmExport2Bank As New SCB("TCB", "Outside")
        frmExport2Bank.ShowDialog()
    End Sub

    Private Sub barComputeVatPct_Click(sender As Object, e As EventArgs) Handles barComputeVatPct.Click
        frmComputeVatPct.ShowDialog()
    End Sub

    Private Sub barGetTcbBankList_Click(sender As Object, e As EventArgs) Handles barGetTcbBankList.Click
        Dim objExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
        Dim objofd As New OpenFileDialog
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
        Dim i As Integer, j As Integer, k As Integer
        Dim lstQuerries As New List(Of String)
        Dim strBankName As String
        Dim intFromProvince As Integer
        Dim intToProvince As Integer
        Dim intFromBranch As Integer
        Dim intToBranch As Integer
        Dim intBankId As Integer
        Dim strProvince As String
        Dim strBankProvince As String
        Dim strBranch As String = ""


        With objofd
            .Filter = "excel files |*.xls"
            .ShowDialog()
            If .FileName = "" Then
                Exit Sub
            End If
            objExcel.Visible = True

            objWbk = objExcel.Workbooks.Open(.FileName, , True)
            objWsh = objWbk.ActiveSheet
            With objWsh
                For i = 2001 To 2100
                    lstQuerries.Clear()
                    strBankName = .Range("U" & i).Value

                    lstQuerries.Add("insert into LIB.dbo.BankIDs (UsedBy, BankName, Status, FstUser, City) values ('TCB','" & strBankName & "','--','" _
                                    & myStaff.SICode & "','" & myStaff.City & "')")
                    If UpdateListOfQuerries(lstQuerries, Conn, True, intBankId) Then
                        lstQuerries.Clear()
                        intFromProvince = Mid(.Range("V" & i).Value, 2, 4)
                        intToProvince = Mid(.Range("V" & i).Value, 8, 4)

                        For j = intFromProvince To intToProvince
                            strProvince = .Range("W" & j).Value
                            strBankProvince = .Range("X" & j).Value
                            intFromBranch = Mid(.Range("Y" & j).Value, 2, 4)
                            intToBranch = Mid(.Range("Y" & j).Value, 8, 4)
                            For k = intFromBranch To intToBranch
                                strBranch = .Range("Z" & k).Value
                                lstQuerries.Add("insert into LIB.dbo.BankBranches (BankID, Province, BankProvince, Branch, Status, FstUser, City) values (" & intBankId & ",'" _
                                            & strProvince & "','" & strBankProvince & "','" & strBranch & "','OK','" & myStaff.SICode & "','" & myStaff.City & "')")
                            Next
                        Next
                        lstQuerries.Add("update LIB.dbo.BankIDs set Status='OK' where Recid=" & intBankId)
                        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
                            MsgBox("Unable to update Branch at cell Z" & k)
                        End If
                    End If

                Next
            End With

        End With
        objExcel.Quit()

    End Sub

    Private Sub barVendorUpdate_Click(sender As Object, e As EventArgs) Handles barVendorUpdate.Click
        frmVendorList.ShowDialog()
    End Sub

    Private Sub barMapBankProvince_Click(sender As Object, e As EventArgs) Handles barMapBankProvince.Click
        frmMapBankProvince.ShowDialog()
    End Sub

    Private Sub barMapBankName_Click(sender As Object, e As EventArgs) Handles barMapBankName.Click
        frmMapBankName.ShowDialog()
    End Sub

    Private Sub barExportReedMackay_Click(sender As Object, e As EventArgs) Handles barExportReedMackay.Click
        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect to SQL!")
            Exit Sub
        End If
        frmiBankExport.ShowDialog()
    End Sub

    Private Sub barBulkPrintVat7_Click(sender As Object, e As EventArgs) Handles barBulkPrintVat7.Click
        Dim frmCreate As New frmSelectTrxForE_Inv(30)
        frmCreate.ShowDialog()
    End Sub

    Private Sub barVAT7Invoice4Cwt_Click(sender As Object, e As EventArgs) Handles barVAT7Invoice4Cwt.Click
        Dim frmNew As New frmVatInvoicePrint4Cwt(30)
        frmNew.ShowDialog()
    End Sub

    Private Sub barVATNoDiscountInvoice4Cwt_Click(sender As Object, e As EventArgs) Handles barVatNoDiscountInvoice4Cwt.Click
        Dim frmNew As New frmVatInvoicePrint4Cwt(0)
        frmNew.ShowDialog()
    End Sub

    Private Sub CTSMISCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles barCTS_MISC.Click
        Dim frmNew As New frmCTS_MISC()
        frmNew.ShowDialog()
    End Sub

    Private Sub TestToolStripMenuItem_Click(sender As Object, e As EventArgs) 'Handles TestToolStripMenuItem.Click
        ImportAopUNC4AdjustedAmt(212695, "RAS", "N-A", 1000000)
    End Sub

    Private Sub barEmailItem_Click(sender As Object, e As EventArgs) Handles barEmailItem.Click
        Dim frmNew As New SendToEmail()
        frmNew.ShowDialog()
    End Sub

    Private Sub barClearLog_Click(sender As Object, e As EventArgs) Handles barClearLog.Click
        ClearLogFile(0)
    End Sub



    Private Sub barAdjustVAT8PCT_Click(sender As Object, e As EventArgs) Handles barAdjustVAT8PCT.Click
        Dim tblOldSf As DataTable
        Dim strQuerry As String = " select * FROM [CWT_SF]" _
                                    & " where status='ok' and '01 feb 22' between ValidFrom and ValidThru order by CustId,SvcType"
        Dim lstQuerry As New List(Of String)
        tblOldSf = GetDataTable(strQuerry, Conn)

        For Each objRow As DataRow In tblOldSf.Rows
            lstQuerry.Clear()
            lstQuerry.Add("insert into CWT_SF (CustID, SVCType, TRX, Curr, Status, FstUser, Countries, WzAIR, Base" _
                          & ", ValidFrom, ValidThru, TKT, ALType, RtgType, Amount, Cabin, FltTime, SegTKT, AmtPersonal, OWRT, VatPct)" _
                          & " select CustID, SVCType, TRX, Curr, Status, 'NMK', Countries, WzAIR, Base" _
                          & ", '01 Feb 22', ValidThru, TKT, ALType, RtgType,ROUND((Amount/1.1)*1.08,0), Cabin, FltTime, SegTKT, ROUND((AmtPersonal*1.1)*1.08,0), OWRT, 8" _
                          & " from CWT_SF where RecId=" & objRow("RecId"))
            lstQuerry.Add("update CWT_SF set ValidThru='31 Jan 22 23:59',LstUser='NMK',lstUpdate=getdate() where RecId=" & objRow("RecId"))
            If Not UpdateListOfQuerries(lstQuerry, Conn) Then
                MsgBox("Unable to update SF with recid = " & objRow("RecId"))
            End If
        Next

    End Sub

    Private Sub barBulkPrintVat8_Click(sender As Object, e As EventArgs)
        Dim frmCreate As New frmSelectTrxForE_Inv(0)
        frmCreate.ShowDialog()
    End Sub

    Private Sub barCorrectCtsSF_Click(sender As Object, e As EventArgs) Handles barCorrectCtsSF.Click
        Dim tblOldSf As DataTable
        Dim strQuerry As String = " select ChargeTV, a.* FROM cwt.dbo.go_air a " _
                                    & " left join tkt t on a.Tkid=t.RecId" _
                                    & " where t.status<>'XX' and T.DOI >='01 Feb 22' and tkid<>0 and a.TrxFee<>ChargeTV"
        Dim lstQuerry As New List(Of String)
        tblOldSf = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblOldSf.Rows
            ExecuteNonQuerry("update cwt.dbo.go_air set TrxFee=" & objRow("ChargeTV") & "where recId=" & objRow("RecId"), Conn)
        Next
        MsgBox("Completed!")
    End Sub

    Private Sub barCtsReports_Click(sender As Object, e As EventArgs) Handles barCtsReports.Click
        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect to SQL CWT!")
            Exit Sub
        End If
        frmCustomerReports.ShowDialog()
    End Sub

    Private Sub barALIncentives_Click(sender As Object, e As EventArgs) Handles barALIncentives.Click
        frmAlIncentives.ShowDialog()
    End Sub

    Private Sub barFillRloc4Cts_Click(sender As Object, e As EventArgs) Handles barFillRloc4Cts.Click
        Dim tblTkt As DataTable
        Dim strQuerry As String = " select t.RecId, g.rloc FROM tkt t " _
                                    & " left join cwt.dbo.go_air a on a.Tkid=t.RecId" _
                                    & " left join cwt.dbo.go_travel g on g.Recid=a.TravelId" _
                                    & " where t.Qty<>0 and t.status<>'XX' and T.DOI >='01 Jan 22' and t.Rloc in ('RLOC','')" _
                                    & " and t.DocType='ETK' AND G.RLOC<>'' "

        tblTkt = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblTkt.Rows
            ExecuteNonQuerry("update tkt set Rloc='" & objRow("Rloc") & "' where recId=" & objRow("RecId"), Conn)
        Next
        MsgBox("Completed!")
    End Sub

    Private Sub barThirdParty_Click(sender As Object, e As EventArgs) Handles barThirdParty.Click
        frmThirdPartyList.ShowDialog()
    End Sub

    Private Sub QHToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QHToolStripMenuItem.Click
        'Try
        Dim frmQH As New frmParseNonGDS("QH", "Htm", 5225)
        frmQH.ShowDialog()
        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub barInvoiceListB4Tt78_Click(sender As Object, e As EventArgs) Handles barInvoiceListB4Tt78.Click

        Dim frmList As New frmInvB4Tt78
        frmList.ShowDialog()
    End Sub

    Private Sub barSendNotice_Click(sender As Object, e As EventArgs) Handles barSendNotice.Click
        frmInvSendNotice.ShowDialog()
    End Sub

    Private Sub barInvIssuerTT78_Click(sender As Object, e As EventArgs) Handles barInvIssuerTT78.Click
        Dim f As New frmInvIssuerTT78("BO", "")
        f.ShowDialog()
    End Sub

    Private Sub VJToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VJToolStripMenuItem.Click
        Dim frmVJ As New frmParseNonGDS("VJ", "pdf", 7643)
        frmVJ.ShowDialog()


    End Sub

    Private Sub barIssuerTT78_Click(sender As Object, e As EventArgs) Handles barIssuerTT78.Click
        Dim f As New frmInvIssuerTT78("FO", "")
        f.ShowDialog()
    End Sub

    Private Sub barTurnOnLog_Click(sender As Object, e As EventArgs) Handles barTurnOnLog.Click
        pblnLogXml = True
    End Sub

    Private Sub barAopBspCheck_Click(sender As Object, e As EventArgs) Handles barAopBspCheck.Click
        Dim mAopBsp As New frmAopBsp
        mAopBsp.ShowDialog()
    End Sub

    Private Sub barCorrectStockCtrl_Click(sender As Object, e As EventArgs) Handles barCorrectStockCtrl.Click
        Dim tblTkts As DataTable
        Dim strQuerry As String = " select *" _
                                    & " from tkt" _
                                    & " where status<>'XX' and DOI >='01 Jan 21' and len(StockCtrl)=10 "

        tblTkts = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblTkts.Rows
            ExecuteNonQuerry("update tkt set StockCtrl='" & Mid(objRow("Tkno"), 1, 3) & objRow("StockCtrl") _
                            & "' where recId=" & objRow("RecId"), Conn)
        Next
        MsgBox("Completed!")
    End Sub

    Private Sub barAllowCcRcpEdit_Click(sender As Object, e As EventArgs) Handles barAllowCcRcpEdit.Click
        frmAllowCcRcpEdit.ShowDialog()
    End Sub

    Private Sub barCorrectInvCustShortName_Click(sender As Object, e As EventArgs) Handles barCorrectInvCustShortName.Click
        Dim tblTkts As DataTable
        Dim strQuerry As String = "select w.*, l.CustShortName as LocalName from lib.dbo.E_InvWeb w" _
                                    & " Left join lib.dbo.E_Inv l on l.InvID=w.InvId and l.Status='ok'" _
                                    & " where w.InvId<>0 and w.CustShortName='' and l.CustShortName <>''"

        tblTkts = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblTkts.Rows
            ExecuteNonQuerry("update lib.dbo.E_InvWeb set CustShortName='" & objRow("LocalName") _
                            & "' where recId=" & objRow("RecId"), Conn)
        Next
        MsgBox("Completed!")
    End Sub

    Private Sub barBspStock_Click(sender As Object, e As EventArgs) Handles barBspStock.Click
        frmBspStock.ShowDialog()
    End Sub


    Private Sub barRcpEdit4VatInvIssuedFO_Click(sender As Object, e As EventArgs) Handles barRcpEdit4VatInvIssuedFO.Click
        Dim frmRq As New frmRcpEdit4VatInvIssued(FrontBackOffc.FrontOffice)
        frmRq.ShowDialog()
    End Sub

    Private Sub barRcpEdit4VatInvIssued_Click(sender As Object, e As EventArgs) Handles barRcpEdit4VatInvIssued.Click
        Dim frmRq As New frmRcpEdit4VatInvIssued(FrontBackOffc.BackOffice)
        frmRq.ShowDialog()
    End Sub
    Private Sub barDebitDueDate_Click(sender As Object, e As EventArgs) Handles barDebitDueDate.Click
        frmDebitDueDate.ShowDialog()

    End Sub

    Private Sub TestTokenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles barTest.Click
        Dim tblSupplier As DataTable = GetDataTable("Select t.RecID ,T1.TktIssuedBy" _
                                                     & " from TKT T " _
                                                     & " LEFT JOIN CWT.DBO.tkt_1a T1 ON T.SRV=T1.SRV AND T.TKNO=T1.TKNO" _
                                                     & " where T.TktIssuedBy=0 AND T1.TktIssuedBy<>0")
        Dim strDomInt As String
        For Each objRow As DataRow In tblSupplier.Rows
            ExecuteNonQuerry("update TKT set TKTISSUEDBY=" & objRow("TktIssuedBy") _
                         & " where RecId=" & objRow("RecId"), Conn)
        Next

        'Dim tblSupplier As DataTable = GetDataTable("select distinct SupplierId,Supplier,Address_CountryCode" _
        '                                            & " from Dutoan_Item i" _
        '                                            & " left join supplier s on i.SupplierId=s.RecId" _
        '                                            & " where DomInt='' and i.Status='ok'")
        'Dim strDomInt As String
        'For Each objRow As DataRow In tblSupplier.Rows
        '    If Not IsDBNull(objRow("Address_CountryCode")) Then
        '        If objRow("Address_CountryCode") = "VN" Then
        '            strDomInt = "DOM"
        '        Else
        '            strDomInt = "INT"
        '        End If
        '        ExecuteNonQuerry("update Dutoan_Item set DomInt='" & strDomInt _
        '                     & "' where SupplierId=" & objRow("SupplierId"), Conn)
        '    End If


        'Next
        'Dim objXls As New Microsoft.Office.Interop.Excel.Application
        'Dim ofd1 As New OpenFileDialog
        'Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        'Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
        'Dim i As Integer = 2
        'Dim lstDistrict As New List(Of String)

        'With ofd1
        '    .DefaultExt = "*.xlsx"
        '    .InitialDirectory = "D:\VB.net\PRG\RAS12\Ras12\bin\Debug\ChangeRequest\2210 Province district"
        '    .ShowDialog()

        '    If .FileName <> "" Then
        '        objWbk = objXls.Workbooks.Open(.FileName,, True)
        '        objWsh = objWbk.ActiveSheet
        '        Do While objWsh.Range("A" & i).Value <> ""
        '            If Not lstDistrict.Contains(objWsh.Range("A" & i).Value & objWsh.Range("C" & i).Value) Then
        '                ExecuteNonQuerry("insert into lib.dbo.Misc (CAT,strVal1,VAL,FstUser,City)" _
        '                                & " values ('VnDistrict',N'" & objWsh.Range("A" & i).Value _
        '                                & "',N'" & objWsh.Range("C" & i).Value & "','NMK','SGN')", Conn)
        '                lstDistrict.Add(objWsh.Range("A" & i).Value & objWsh.Range("C" & i).Value)

        '            End If
        '            i = i + 1
        '        Loop
        '    End If
        'End With
    End Sub

    Private Sub barBookerList_Click(sender As Object, e As EventArgs) Handles barBookerList.Click
        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect to SQL CWT!")
            Exit Sub
        End If
        frmBookerList.ShowDialog()
    End Sub

    '^_^20220712 add by 7643 -b-
    Private Sub BarCounterCheck_Click(sender As Object, e As EventArgs) Handles BarCounterCheck.Click
        Dim mfrm As New frmCounterCheck
        mfrm.ShowDialog()
    End Sub
    '^_^20220712 add by 7643 -e-

    Private Sub barVatInvoice4TVS_Click(sender As Object, e As EventArgs) Handles barVatInvoice4TVS.Click
        Dim frmInv As New frmVatInvoicePrint4TVS(0)
        frmInv.ShowDialog()
    End Sub

    Private Sub barImportRmTravelerProfiles_Click(sender As Object, e As EventArgs) Handles barImportRmTravelerProfiles.Click
        frmImportProfiles.ShowDialog()
    End Sub
    Private Sub barVendorUpdateNoBTF_Click(sender As Object, e As EventArgs) Handles barVendorUpdateNoBTF.Click
        '^_^20220913 add by 7643 -b-
        Dim VendorUpdateNoBTF As New frmVendorUpdateNoBTF
        VendorUpdateNoBTF.Show()
        '^_^20220913 add by 7643 -e-
    End Sub

    Private Sub barCorrectReturnDate_Click(sender As Object, e As EventArgs) Handles barCorrectReturnDate.Click
        Dim tblOldSf As DataTable
        Dim strQuerry As String = " select a.ReturnDate,a.AutoRAS,t.RecId " _
                                    & " from tkt t" _
                                    & " left join cwt.dbo.tkt_1a a on t.TKNO=a.TKNO And t.SRV=a.SRV" _
                                    & " where t.Status<>'xx' and a.CustId in " _
                                    & " (select custid from CompanyInfo where GetReturnDate='true')" _
                                    & " And t.ReturnDate Is null And t.DOI>='01 Aug 2022'"
        Dim lstQuerry As New List(Of String)
        tblOldSf = GetDataTable(strQuerry, Conn)
        For Each objRow As DataRow In tblOldSf.Rows
            ExecuteNonQuerry("update TKT set ReturnDate='" _
                             & CreateFromDate(objRow("ReturnDate")) & "' where recId=" & objRow("RecId"), Conn)
        Next
        MsgBox("Completed!")
    End Sub

    Private Sub barCombinedInv_Click(sender As Object, e As EventArgs) Handles barCombinedInv.Click
        frmCombinedInv.ShowDialog()
    End Sub

    Private Sub barUpdateCustInRcp_Click(sender As Object, e As EventArgs) Handles barUpdateCustInRcp.Click
        frmUpdateCustInRcp.ShowDialog()
    End Sub

    Private Sub barHotelRates_Click(sender As Object, e As EventArgs) Handles barHotelRates.Click
        frmHotelRates.ShowDialog()
    End Sub

    Private Sub barExport2MXP_Click(sender As Object, e As EventArgs) Handles barExport2MXP.Click
        If Not pobjTvcs.Connect Then
            MsgBox("Unable to connect to SQL!")
            Exit Sub
        End If
        frmExportFriesland.ShowDialog()
    End Sub

    Private Sub barNoMXP_Click(sender As Object, e As EventArgs) Handles barNoMXP.Click
        frmFrieslandNoMXP.ShowDialog()
    End Sub

    Private Sub BarEditRptData_Click(sender As Object, e As EventArgs) Handles BarEditRptData.Click
        Dim f As Form, RCP As String, b As ToolStripItem = CType(sender, ToolStripItem)
        For Each f In Application.OpenForms
            If f.GetType.Name = "FOissueTKT" Then
                f.Activate()
                Exit Sub
            End If
        Next
        RCP = InputBox("Enter Transaction Confirmation Number:", msgTitle).Trim.ToUpper
        If RCP = "" Then Exit Sub

        f = New FOissueTKT(b.Tag & "_" & RCP)
        f.ShowDialog()
    End Sub

    Private Sub barRptData_Click(sender As Object, e As EventArgs) Handles barRptData.Click
        frmRptDataList.ShowDialog()
    End Sub
End Class


