Imports RAS12.MySharedFunctionsWzConn
Public Class FoxSetting
    Dim WhatAction As String
    Public Sub New(Optional ByVal pWhatAction As String = "")
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        WhatAction = pWhatAction
    End Sub
    Private Sub LblSave_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblSet.LinkClicked
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand
        On Error GoTo ErrHandler
        Cmd.CommandText = "update CC_setting set FoxCoef=" & CDec(Me.TxtFQ.Text) & ", MinBLC=" & CDec(Me.TxtMinBLC.Text) & _
            " where RecID=" & Me.GridAgentSI.CurrentRow.Cells("cc_RecID").Value
        Cmd.ExecuteNonQuery()
        LoadGridAgentSI("")
        UpdateLogFile("CC_Cust", "FoxChange", "recID:" & Me.GridAgentSI.CurrentRow.Cells("cc_RecID").Value.ToString, _
            "MinBLC:" & Me.TxtMinBLC.Text, "FQ:" & Me.TxtFQ.Text, "", "", "", "", "")
ErrHandler:
        On Error GoTo 0
    End Sub
    Private Sub LoadGridAgentSI(ByVal pDK As String)
        Me.GridAgentSI.DataSource = GetDataTable("Select C.RecID as CC_RecID, f.RecID as fox_RecID, c.CustID, c.CustShortName, SI, MinBLC, " & _
                                        "FoxCoef as IntervalCoef, ToBeTakenBack, 0 as VND_AVail, CrCoef, PPCoef, f.Status, PSW, QNumber as QNbr " & _
                                        "from fox_iAmChick f inner join cc_setting c on f.custid=c.custid where f.status<>'XX' " & pDK)
        Me.GridAgentSI.Columns(0).Visible = False
        Me.GridAgentSI.Columns(1).Visible = False
        Me.GridAgentSI.Columns("MinBLC").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.GridAgentSI.Columns("MinBLC").DefaultCellStyle.Format = "#,###0"
        Me.LblSet.Enabled = False
        Me.LblUpdateIniPSW.Enabled = False
        Me.LblAddSI.Enabled = False
        Me.LblUpdateQNbr.Enabled = False
        Me.LblRQDelete.Visible = False
        Me.LblCFM_PSW_Changed_RemoveSI.Visible = False
        Me.LblCFM_PSW_Changed_QQ.Visible = False
        Me.GridAgentSI.Columns("CustID").Width = 48
        Me.GridAgentSI.Columns("Status").Width = 32
        Me.GridAgentSI.Columns("CustShortName").Width = 128
        Me.GridAgentSI.Columns("MinBLC").Width = 64
        Me.GridAgentSI.Columns("IntervalCoef").Width = 72
        Me.GridAgentSI.Columns("ToBeTakenBack").Visible = False
        Me.GridAgentSI.Columns("VND_AVAIL").Visible = False
        Me.GridAgentSI.Columns("CRCoef").Visible = False
        Me.GridAgentSI.Columns("PPCoef").Visible = False
        Me.GridAgentSI.Columns("PSW").Visible = False
        Dim fName As String
        For i As Int16 = 0 To Me.GridAgentSI.RowCount - 1
            If Me.GridAgentSI.Item("CRCoef", i).Value = 0 Then
                fName = "top 1 VND_PPD_Avail"
            Else
                fName = "top 1 VND_PSP_Avail"
            End If
            Me.GridAgentSI.Item("VND_Avail", i).Value = ScalarToDec("CC_BLC", fName, " where custid=" & Me.GridAgentSI.Item("CustID", i).Value _
                                                                    & " and City='" & myStaff.City & "' order by recid desc")
        Next
    End Sub
    Private Sub LoadGridSMS()
        Me.GridSMS.DataSource = GetDataTable("Select RecID, TimeFrom as Frm, TimeThru as Thru, StaffName, MobileNo from fox_SMS where status='OK'")
        Me.GridSMS.Columns(0).Visible = False
        Me.GridSMS.Columns(1).Width = 30
        Me.GridSMS.Columns(2).Width = 30
        Me.GridSMS.Columns(4).Width = 80
        Me.LblUpdateMobileNo.Enabled = False
    End Sub
    Private Sub FoxSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = pubVarBackColor
        LoadGridAgentSI("")
        If WhatAction = "BLC" Then
            GenCmbCust()
            Me.LblCFM_PSW_Changed_QQ.Enabled = False
        ElseIf WhatAction = "SMS" Then
            Me.TabControl1.SelectTab("Tab" & WhatAction)
            LoadGridSMS()
            Me.TxtSIList.Enabled = False
        ElseIf WhatAction = "STP" Then
            Me.TabControl1.Visible = False
        End If
    End Sub
    Private Sub GenCmbCust()
        Dim strSQL As String
        strSQL = "Select CustID as val , custShortName as dis from CC_setting where status='OK' " & _
            "and custID in (select CustID from Cust_channel_level where status='OK' and channel='TA'" & _
            " and al='VN' and getdate() between validFrom and ValidThru)" & _
            " and custID not in (select custID from fox_iAmChick where status<>'XX')"
        LoadCmb_VAL(Me.CmbCustomer, strSQL)
    End Sub
    Private Sub GridAgentSI_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridAgentSI.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        Me.CmbCustomer.SelectedValue = Me.GridAgentSI.CurrentRow.Cells("CustID").Value
        Me.CmbCustomer.Enabled = False
        Me.LblCustomer.Enabled = True
        If Me.GridAgentSI.CurrentRow.Cells("QNbr").Value = "" Then
            Me.LblUpdateQNbr.Enabled = True
        Else
            Me.LblUpdateQNbr.Enabled = False
        End If
        Me.LblSet.Enabled = True
        If Me.GridAgentSI.CurrentRow.Cells("IntervalCoef").Value > 0 Then
            Me.LblUpdateIniPSW.Enabled = True
        Else
            Me.LblUpdateIniPSW.Enabled = False
        End If
        If Me.GridAgentSI.CurrentRow.Cells("ToBeTakenBack").Value > "" Then
            Me.LblRQDelete.Visible = True
        Else
            Me.LblRQDelete.Visible = False
        End If
        If WhatAction = "BLC" Then
            Me.TxtSIList.Text = Me.GridAgentSI.CurrentRow.Cells("SI").Value
        Else
            Me.TxtSIList.Text = Me.GridAgentSI.CurrentRow.Cells("ToBeTakenBack").Value
        End If
        Me.LblCFM_PSW_Changed_RemoveSI.Enabled = True
        If Me.GridAgentSI.CurrentRow.Cells("PSW").Value.ToString.Length = 8 Then
            Me.LblCFM_PSW_Changed_QQ.Visible = True
            Me.LblViewSentMail.Visible = True
        Else
            Me.LblCFM_PSW_Changed_QQ.Visible = False
            Me.LblViewSentMail.Visible = False
        End If
    End Sub

    Private Sub TxtMinBLC_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtMinBLC.LostFocus, TxtSI.LostFocus
        Dim aa As Decimal = CDec(Me.TxtMinBLC.Text)
        Me.TxtMinBLC.Text = Format(aa, "#,##0")
    End Sub
    Private Function strOccrrence(ByVal pChar As String, ByVal pStr As String) As Int16
        Dim KQ As Int16
        For i As Int16 = 0 To pStr.Length - 1
            If pStr.Substring(i, 1) = pChar Then KQ = KQ + 1
        Next
        Return KQ
    End Function
    Private Sub LblSendEmail_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateIniPSW.LinkClicked
        If Me.TxtPSW.Text = "" Then Exit Sub
        Dim coSO As Boolean = False, coChu As Boolean = False
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        If Me.GridAgentSI.CurrentRow.Cells("VND_Avail").Value < 15000000 Then
            MsgBox("Dont Give PSW to Agent with CurrBalance Below 30 Mil", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        If Me.GridAgentSI.CurrentRow.Cells("PSW").Value <> "" Then
            MsgBox("This Is NOT New 1S Agent", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        For i As Int16 = 0 To Me.TxtPSW.Text.Length - 1
            If Asc(Me.TxtPSW.Text.Substring(i, 1)) > 47 And Asc(Me.TxtPSW.Text.Substring(i, 1)) < 58 Then
                coSO = True
            ElseIf Asc(Me.TxtPSW.Text.Substring(i, 1)) > 64 And Asc(Me.TxtPSW.Text.Substring(i, 1)) < 91 Then
                coChu = True
            ElseIf Asc(Me.TxtPSW.Text.Substring(i, 1)) > 96 And Asc(Me.TxtPSW.Text.Substring(i, 1)) < 123 Then
                coChu = True
            End If
            If strOccrrence(Me.TxtPSW.Text.Substring(i, 1), Me.TxtPSW.Text) > 3 Then
                coChu = False
                Exit For
            End If
        Next
        If coSO = False Or coChu = False Or InStr(Me.TxtPSW.Text, "Q") + InStr(Me.TxtPSW.Text, "Z") > 0 Then
            MsgBox("Invalid PSW. Plz Check", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        Insert_Update_WebTable(Conn_Web, "update ft.dbo.fox_iamChick set psw='" & _
            Replace(Me.TxtPSW.Text, "--", "") & "' where psw='' and Locid=" & Me.GridAgentSI.CurrentRow.Cells("Fox_RecID").Value)
        Me.LblUpdateIniPSW.Enabled = False
    End Sub
    Private Sub SMS_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TabSMS.Enter, TabBLC.Enter
        Dim tb As TabPage = CType(sender, TabPage)
        If InStr(tb.Name, WhatAction) = 0 Then
            Me.TabControl1.SelectTab("Tab" & WhatAction)
        End If
    End Sub

    Private Sub LblAddSI_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblAddSI.LinkClicked
        Dim OldSI As String, ExistSI_Status As String, PendingSales As Int16, OldQ As String, XXDate As Date
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Cmd.CommandText = "select top 1 Status from fox_iAmChick where SI like '%" & Me.TxtSI.Text & "%' order by Status"
        ExistSI_Status = Cmd.ExecuteScalar
        If Not String.IsNullOrEmpty(ExistSI_Status) Then
            If InStr(ExistSI_Status, "X") = 0 Then
                MsgBox("The SI You Input Already Exists.", MsgBoxStyle.Exclamation, msgTitle)
                Exit Sub
            Else
                XXDate = ScalarToDate("fox_Iamchick", "top 1 LstUpdate", "status='XX' and SI like '%" & Me.TxtSI.Text & "%' order by LstUpdate Desc")
                If XXDate < Now.Date.AddDays(-1) Then
                    Cmd.CommandText = "select count(*) from  funcTKT_1AnotUpdated2RAS_wzparam('" & Format(Now.Date.AddDays(-1), "dd-MMM-yy") & _
                        "') where left(tkno,3)='738' and PRG='M1S' and doi <'" & Format(Now.Date, "dd-MMM-yy") & "'"
                    PendingSales = Cmd.ExecuteScalar
                    If PendingSales > 0 Then
                        MsgBox("SI Cant Be Swapped Between Agts unless All Pending Sales Cleared.", MsgBoxStyle.Exclamation, msgTitle)
                        Exit Sub
                    End If
                Else
                    MsgBox("SI Must Be Inactive for More Than a Day Before Reuse", MsgBoxStyle.Exclamation, msgTitle)
                    Exit Sub
                End If
            End If
        End If
        If Not Me.CmbCustomer.Enabled Then
            OldSI = Me.GridAgentSI.CurrentRow.Cells("SI").Value
            OldQ = Me.GridAgentSI.CurrentRow.Cells("QNbr").Value
            Cmd.CommandText = ChangeStatus_ByID("fox_iAmChick", "XX", Me.GridAgentSI.CurrentRow.Cells("fox_recid").Value)
            Cmd.ExecuteNonQuery()
        Else
            OldSI = ""
            OldQ = ""
        End If
        OldSI = OldSI.Replace("__", "_")
        OldSI = OldSI & "_" & Me.TxtSI.Text.ToUpper
        Do While OldSI.Substring(0, 1) = "_"
            OldSI = OldSI.Substring(1)
        Loop

        Cmd.CommandText = "insert fox_iAmChick (CustID, SI, Qnumber, Status, FstUser) values (@CustID, @SI, @Qnumber, @Status, @FstUser)"
        Cmd.Parameters.Clear()
        If Me.CmbCustomer.Enabled Then
            Cmd.Parameters.Add("@CustID", SqlDbType.Int).Value = Me.CmbCustomer.SelectedValue
        Else
            Cmd.Parameters.Add("@CustID", SqlDbType.Int).Value = Me.GridAgentSI.CurrentRow.Cells("CustID").Value
        End If
        Cmd.Parameters.Add("@SI", SqlDbType.VarChar).Value = OldSI
        Cmd.Parameters.Add("@Qnumber", SqlDbType.VarChar).Value = OldQ
        Cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "OA"
        Cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        Cmd.ExecuteNonQuery()
        If Me.CmbCustomer.Enabled Then GenCmbCust()
        LoadGridAgentSI("")
    End Sub

    Private Sub TxtSI_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSI.TextChanged
        Me.LblAddSI.Enabled = True
    End Sub

    Private Sub LblUpdateMobileNo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateMobileNo.LinkClicked
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand
        If CheckFormatTextBox(TxtMobileNo, True, 10, 10) Then
            If Me.GridSMS.RowCount > 0 Then
                Cmd.CommandText = ChangeStatus_ByID("fox_SMS", "XX", Me.GridSMS.CurrentRow.Cells("RecID").Value)
                Cmd.ExecuteNonQuery()
            End If

            Cmd.CommandText = "Insert fox_SMS (timeFrom,TimeThru, StaffName, MobileNo, Skype, FstUser) values (" &
                "@timeFrom,@TimeThru, @StaffName, @MobileNo, @Skype, @FstUser)"
            Cmd.Parameters.Clear()
            Cmd.Parameters.Add("@timeFrom", SqlDbType.TinyInt).Value = Me.GridSMS.CurrentRow.Cells("Frm").Value
            Cmd.Parameters.Add("@TimeThru", SqlDbType.TinyInt).Value = Me.GridSMS.CurrentRow.Cells("Thru").Value
            Cmd.Parameters.Add("@Skype", SqlDbType.VarChar).Value = Me.TxtSkype.Text
            If Me.GridSMS.CurrentRow.Cells("Thru").Value > 18 Then
                Cmd.Parameters.Add("@StaffName", SqlDbType.VarChar).Value = Me.GridSMS.CurrentRow.Cells("StaffName").Value
            Else
                Cmd.Parameters.Add("@StaffName", SqlDbType.VarChar).Value = Me.TxtStaff.Text
            End If
            Cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = Me.TxtMobileNo.Text
            Cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
            Cmd.ExecuteNonQuery()
            LoadGridSMS()
        Else
            MsgBox("Invalid Phone No.", MsgBoxStyle.Critical, msgTitle)
        End If
    End Sub

    Private Sub GridSMS_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridSMS.CellContentClick
        If Me.GridSMS.RowCount > 0 AndAlso e.RowIndex < 0 Then Exit Sub
        Me.LblUpdateMobileNo.Enabled = True
    End Sub
    Private Sub LblDelete_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblRQDelete.LinkClicked
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand
        If InStr(Me.TxtSIList.Text, "_") = 0 And Me.TxtSIList.Text.Length <> 3 Then Exit Sub
        If Me.TxtSIList.Text.Substring(0, 1) = "_" Then Me.TxtSIList.Text = Me.TxtSIList.Text.Substring(1)
        For i As Int16 = 0 To UBound(Me.TxtSIList.Text.Split("_"))
            If Me.TxtSIList.Text.Split("_")(i).Length <> 3 OrElse _
                InStr(Me.GridAgentSI.CurrentRow.Cells("SI").Value, Me.TxtSIList.Text.Split("_")(i)) = 0 Then Exit Sub
        Next
        If Me.TxtSIList.Text = Me.GridAgentSI.CurrentRow.Cells("SI").Value Or _
            InStr(Me.GridAgentSI.CurrentRow.Cells("SI").Value, Me.TxtSIList.Text) > 0 Then
            UpdateLogFile("IamChick", "RQTakeBck", "Cust:" & Me.GridAgentSI.CurrentRow.Cells("CustID").Value, _
                "SI:" & Me.TxtSIList.Text, "", "", "", "", "", "")

            Cmd.CommandText = "update fox_iAmChick set ToBeTakenBack=@ToBeTakenBack where RecID=@RecID"
            Cmd.Parameters.Clear()
            Cmd.Parameters.Add("@ToBeTakenBack", SqlDbType.VarChar).Value = Me.TxtSIList.Text.ToUpper
            Cmd.Parameters.Add("@RecID", SqlDbType.Int).Value = Me.GridAgentSI.CurrentRow.Cells("fox_recid").Value
            Cmd.ExecuteNonQuery()
            LoadGridAgentSI("")
        End If
    End Sub

    Private Sub LblCustomer_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblCustomer.LinkClicked
        GenCmbCust()
        Me.CmbCustomer.Enabled = True
        Me.LblCustomer.Enabled = False
    End Sub
    Private Sub LblCFM_PSW_Changed_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblCFM_PSW_Changed_RemoveSI.LinkClicked
        Dim NewSI As String, CurrPSW As String, CurrStatus As String, CurrQ As String
        Dim Cmd As SqlClient.SqlCommand = Conn.CreateCommand

        'xoa ban ghi hien tai + update web
        Cmd.CommandText = ChangeStatus_ByID("fox_iAmChick", "XX", Me.GridAgentSI.CurrentRow.Cells("fox_recid").Value)
        Cmd.ExecuteNonQuery()
        Insert_Update_WebTable(Conn_Web, "update ft.dbo.fox_iamchick set status='XX' where locID=" & Me.GridAgentSI.CurrentRow.Cells("fox_recid").Value)

        If Me.TxtSIList.Text = Me.GridAgentSI.CurrentRow.Cells("SI").Value Then Exit Sub ' neu chi co 1 SI, take back la het

        ' neu chi lay lai 1 phan SI, tao 1 ban ghi cũ
        CurrStatus = Me.GridAgentSI.CurrentRow.Cells("Status").Value
        CurrQ = Me.GridAgentSI.CurrentRow.Cells("QNbr").Value
        CurrPSW = Me.GridAgentSI.CurrentRow.Cells("PSW").Value
        NewSI = Me.GridAgentSI.CurrentRow.Cells("SI").Value.Replace(Me.TxtSIList.Text, "")
        NewSI = NewSI.Replace("__", "_")
        If NewSI.Length > 0 AndAlso NewSI.Substring(0, 1) = "_" Then NewSI = NewSI.Substring(1)
        If NewSI.Length > 0 AndAlso Strings.Right(NewSI, 1) = "_" Then NewSI = Strings.Left(NewSI, NewSI.Length - 1)

        Cmd.CommandText = "insert fox_iAmChick (CustID, SI, Status, PSW, QNumber, FstUser) values (" & _
            " @CustID, @SI, @Status, @PSW, @QNumber, @FstUser)"
        Cmd.Parameters.Clear()
        Cmd.Parameters.Add("@CustID", SqlDbType.Int).Value = Me.GridAgentSI.CurrentRow.Cells("CustID").Value
        Cmd.Parameters.Add("@SI", SqlDbType.VarChar).Value = NewSI.ToUpper
        Cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = CurrStatus
        Cmd.Parameters.Add("@PSW", SqlDbType.VarChar).Value = CurrPSW
        Cmd.Parameters.Add("@QNumber", SqlDbType.VarChar).Value = CurrQ
        Cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        If NewSI.Length > 2 Then Cmd.ExecuteNonQuery() 'tao ban ghi voi SI moi, no tu sync sau

    End Sub
    Private Sub TxtSIList_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSIList.TextChanged
        If Me.TxtSIList.Text <> "" Then
            Me.LblRQDelete.Visible = True
            Me.LblCFM_PSW_Changed_RemoveSI.Visible = True
        Else
            Me.LblRQDelete.Visible = False
            Me.LblCFM_PSW_Changed_RemoveSI.Visible = False
        End If
    End Sub
    Private Sub LblCFM_PSW_Changed_QQ_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblCFM_PSW_Changed_QQ.LinkClicked
        If Me.GridAgentSI.CurrentRow.Cells("PSW").Value.ToString.Length > 0 And _
            Me.GridAgentSI.CurrentRow.Cells("Status").Value = "OK" Then
            Insert_Update_WebTable(Conn_Web, "update ft.dbo.fox_iamchick set status='QQ', lstUpdate=getdate() where locID=" & Me.GridAgentSI.CurrentRow.Cells("fox_recid").Value)
            Me.LblCFM_PSW_Changed_QQ.Visible = False
        End If
    End Sub

    Private Sub LblViewSentMail_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblViewSentMail.LinkClicked
        Dim strSQL As String
        Me.LblUpdateMobileNo.Visible = False
        On Error GoTo ErrHandler
        If Conn_Web.State = ConnectionState.Closed Then Conn_Web.Open()
        strSQL = "Select top 8 MSG, LstUpdate as Sent from emaillog where status='OK' and "
        strSQL = strSQL & " subj in ('Sabre Password','PSW Release') and custid=" & Me.GridAgentSI.CurrentRow.Cells("CustID").Value
        strSQL = strSQL & " order by recid desc"
        Me.GridSMS.DataSource = GetDataTable(strSQL, Conn_Web)
        Me.GridSMS.Columns(0).Width = 128
        Conn_Web.Close()
ErrHandler:
    End Sub

    Private Sub LblUpdateQNbr_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateQNbr.LinkClicked
        If Me.TxtQNbr.Text = "" Then Exit Sub
        Dim i As Int16
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Try
            i = CInt(Me.TxtQNbr.Text)
            If i < 200 Or i > 299 Then
                MsgBox("Invalid Q Number. Should Be Between 200-299", MsgBoxStyle.Critical, msgTitle)
                Exit Sub
            End If
            cmd.CommandText = "select RecID from fox_iamchick where status <>'XX' and Qnumber='" & Me.TxtQNbr.Text & "'"
            i = cmd.ExecuteScalar
            If i > 0 Then Exit Sub
        Catch ex As Exception
            Exit Sub
        End Try

        Insert_Update_WebTable(Conn_Web, "update Ft.dbo.Fox_Iamchick set Qnumber='" & Me.TxtQNbr.Text & "' where LocID=" & _
            Me.GridAgentSI.CurrentRow.Cells("Fox_RecID").Value)
    End Sub

    
End Class