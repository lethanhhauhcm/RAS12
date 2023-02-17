Public Class AddCustomer
    Private cmd As SqlClient.SqlCommand = Conn.CreateCommand
    Private WhoIs As String
    Private MyCust As New objCustomer
    Private Sub AddCustomer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Sub New(ByVal pWho As String)
        InitializeComponent()
        WhoIs = pWho
    End Sub
    Private Sub AddCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.GridCustList.BackgroundColor = pubVarBackColor
        RefreshCustList()
        CheckRightForALLForm(Me)
        Me.txtShortName.Focus()
        Me.txtCustCode.Text = "-56"
        LoadCmb_MSC(Me.CmbChannel, "select VAL from MISC where CAT='Channel' and VAL <>'WK' and val in " & myStaff.CAccess & " and Val1 in " & WhoIs)
        If InStr(WhoIs, "CS") > 0 Then
            Me.CmbChannel.Text = "CS"
            Me.LckChkNotCustomer.Enabled = False
            Me.LckChkNotCustomer.Checked = False
        ElseIf InStr(WhoIs, "TC") > 0 Then
            CmbChannel.Items.Add("TC")
            Me.CmbChannel.SelectedIndex = 0
            Me.LckChkNotCustomer.Enabled = False
            Me.LckChkNotCustomer.Checked = False
        ElseIf myStaff.City = "SGN" Then
            Me.CmbChannel.Text = "TA"
            Dim i As Int16 = ScalarToInt("MISC", "count(*)", "cat='BSPAGT' and VAL1=''")
            If i > 0 Then
                MsgBox("Con IATA code chua dc gan ten Dai ly.")
            End If
        End If
    End Sub

    Private Sub RefreshCustList()
        Dim StrSQL As String
        Dim strFilterChannel As String

        If WhoIs.Contains("TC") Then
            strFilterChannel = WhoIs
        Else
            strFilterChannel = myStaff.CAccess
        End If
        If Me.ChkXXOnly.Checked Then
            StrSQL = "select d.Val as Channel, c.* from Customer c " _
                & " left join Cust_Detail d on c.RecId=d.CustId and d.Cat='channel' and d.Status='OK'" _
                & " where custShortName <> CustFullName And c.status in ('EX','QQ') and c.city='" & myStaff.City & "'"
        Else
            StrSQL = "select d.Val as Channel,c.* from CustomerList c" _
                    & " left join Cust_Detail d on c.RecId=d.CustId and d.Cat='channel' and d.Status='OK'" _
                    & " where custShortName <> CustFullName And c.status='OK'" _
                    & " and c.City='" & myStaff.City _
                    & "' and  c.RecID in (select CustID from cust_Detail where status+cat='OKChannel' and val in " & strFilterChannel & ")"
        End If
        Me.GridCustList.DataSource = GetDataTable(StrSQL)
        Me.GridCustList.Columns("Channel").Width = 45
        Me.GridCustList.Columns("RecID").Width = 45
        Me.GridCustList.Columns("CustTaxCode").Width = 75
        Me.GridCustList.Columns("CustShortName").Width = 75
        Me.GridCustList.Columns("Status").Width = 40
        Me.LckCmdSave.Visible = False
        Me.LckCmdAdd.Visible = False
    End Sub
    Private Sub txtCustCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustCode.GotFocus
        Me.txtShortName.Text = ""
        Me.txtFullName.Text = ""
        Me.txtTaxCode.Text = ""
        Me.txtAddress.Text = ""
    End Sub
    Private Sub txtCustCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
            txtShortName.LostFocus, txtCustCode.LostFocus
        Dim txt As TextBox = CType(sender, TextBox)
        Dim dtbl As DataTable, DaTonTai As Boolean = False, StrSQL As String

        StrSQL = String.Format("select * from CustomerList where City='" & myStaff.City _
                               & "' and RecID={0}", Me.txtCustCode.Text)
        If Me.txtShortName.Text.Length > 0 Then
            StrSQL = String.Format("{0} or CustShortName ='{1}'", StrSQL, Me.txtShortName.Text)
        End If
        If Me.txtFullName.Text.Length > 0 Then
            StrSQL = String.Format("{0} or CustFullName='{1}'", StrSQL, Me.txtFullName.Text)
        End If
        dtbl = GetDataTable(StrSQL)
        For i As Int16 = 0 To dtbl.Rows.Count - 1
            DaTonTai = True
            Me.txtCustCode.Text = dtbl.Rows(i)("RecID")
            If txt.Name <> "txtShortName" Then
                Me.txtShortName.Text = dtbl.Rows(i)("CustShortName")
            End If
            Me.txtFullName.Text = dtbl.Rows(i)("CustFullName")
            Me.txtTaxCode.Text = dtbl.Rows(i)("CustTaxCode")
            Me.txtAddress.Text = dtbl.Rows(i)("CustAddress")
            Exit For

        Next
        If DaTonTai Then MsgBox(" This Customer Exists. Please Check!", MsgBoxStyle.Critical, msgTitle)
        Me.LckCmdAdd.Visible = Not DaTonTai
        Me.LckCmdSave.Visible = DaTonTai
    End Sub
    Private Function SaiSoDT() As Boolean
        If WhoIs.Contains("TC") Then Return False
        Me.TxtPhone.Text = Me.TxtPhone.Text.Replace(" ", "")
        If Not CheckFormatTextBox(TxtPhone, True, 8, 11) Then
            MsgBox("Invalid Phone No. Please Check!", MsgBoxStyle.Critical, msgTitle)
            Return True
        Else
            Return False
        End If

    End Function
    Private Function SaiEmail(ByVal pSave As Int16) As Boolean
        Dim j As Integer, peMail As String = Me.TxtEmail.Text.Trim
        If peMail <> "" AndAlso peMail.Substring(0, 3) <> "Plz" Then
            peMail = peMail.Replace(" ", "")
            peMail = peMail.Replace(",", ";")
            For i As Int16 = 0 To UBound(peMail.Split(";"))
                j = InStr(peMail.Split(";")(i), "@")
                If j = 0 Then GoTo errMsg
                If InStr(j + 1, peMail.Split(";")(i), "@") > 0 Then GoTo errMsg
                If InStr(j + 1, peMail.Split(";")(i), ".") = 0 Then GoTo errMsg
                If i > 1 Then GoTo errMsg
            Next
        End If
        If pSave = 1 Then
            j = ScalarToInt("cc_Setting", "RecID", "Custid=" & Me.txtCustCode.Text)
            If j > 0 Then
                If peMail = "" Or InStr(peMail, "@") = 0 Then
                    MsgBox("Need At Least One Email Addresses", MsgBoxStyle.Information, msgTitle)
                    Return True
                End If
            End If
        End If
        Return False
errMsg:
        MsgBox("Invalid Email Addresses", MsgBoxStyle.Information, msgTitle)
        Return True
    End Function
    Private Function SaiName() As Boolean
        Dim tmpCustShortName As String

        If txtShortName.TextLength > 20 Then
            MsgBox("ShortName must NOT over 16 characters")
            Return True
        End If
        If Me.txtShortName.Text.Length = Me.txtFullName.Text.Length _
            And Me.txtShortName.Text = Me.txtFullName.Text Then
            MsgBox("Short Name and Full Name Cant Be the Same", MsgBoxStyle.Information, msgTitle)
            Return True
        End If
        For i As Int16 = 0 To Me.txtShortName.Text.Length - 1
            If InStr("?*#-/!'", Me.txtShortName.Text.Substring(i, 1)) > 0 Then
                MsgBox("Short Name Cant Contain Special Charactor", MsgBoxStyle.Information, msgTitle)
                Return True
            End If
        Next
        cmd.CommandText = String.Format("Select distinct custShortName from rcp where custShortname='{0}'" &
                " UNION select distinct CustShortName from cc_Setting where custShortname='{0}'", Me.txtShortName.Text)
        tmpCustShortName = cmd.ExecuteScalar
        If tmpCustShortName <> "" Then
            MsgBox("This Name Already Exists", MsgBoxStyle.Information, msgTitle)
            Return True
        End If
        Return False
    End Function
    Private Sub CmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LckCmdAdd.Click
        Dim tmpCustID As Integer, vStatus As String = "OK"
        'Dim tblExistingCust As DataTable

        If txtShortName.TextLength > 33 Then
            MsgBox("Max length for ShortName is 33 characters!")
            Exit Sub
        End If
        If MsgBox("Are You Sure That All Input Are Correct and Wanna Create This New Customer?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, msgTitle) = vbNo Then Exit Sub
        If SaiSoDT() Or SaiEmail(0) Or SaiName() Then
            Exit Sub
        End If
        If Mid(TxtEmail.Text, 1, 3) = "Plz" Then Me.TxtEmail.Text = ""
        tmpCustID = ScalarToInt("CustomerList", "recID", String.Format(" custshortname='{0}'", Me.txtShortName.Text))
        If tmpCustID > 0 Then
            MsgBox("Customer Already Exists.", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        'tam bo qua phan nay
        'tblExistingCust = GetDataTable("Select * from Customer where CustFullName=N'" & txtFullName.Text & "'", Conn)

        'If tblExistingCust.Rows.Count > 0 AndAlso tblExistingCust.Rows(0)("RecId") > 0 Then
        '    If tblExistingCust.Rows(0)("App").ToString.Contains("RAS") Then
        '        MsgBox("Customer Already Exists. Will pick up existing Customer:" _
        '               & tblExistingCust.Rows(0)("CustShortName"), MsgBoxStyle.Critical, msgTitle)
        '    Else
        '        MsgBox("Customer Already Exists. Will pick up existing Customer:" _
        '               & tblExistingCust.Rows(0)("CustShortName"), MsgBoxStyle.Critical, msgTitle)
        '        ExecuteNonQuerry("Update CustomerList set App=App+'RAS' where RecId=" & tmpCustID, Conn)
        '    End If
        '    Exit Sub
        'End If

        If Me.LckChkNotCustomer.Checked Then vStatus = "QQ"
        tmpCustID = MyCust.AddCustomer(Me.txtShortName.Text.Replace("--", ""), Me.txtFullName.Text.Replace("--", ""),
            Me.txtTaxCode.Text.Replace("--", ""), Me.txtAddress.Text.Replace("--", "") _
            , Me.TxtEmail.Text.Replace(",", ":").Replace("--", ""),
            Me.TxtPhone.Text, vStatus, txtInvoiceEmail.Text.Replace(" ", ""))
        If tmpCustID = 0 Then
            MsgBox("Unable to create Customer")
        End If
        MyCust.InsertCustDetail(tmpCustID, "Channel", Me.CmbChannel.Text, False)
        MyCust.InsertCustDetail(tmpCustID, "AL", "YY", False)
        RefreshCustList()
        If vStatus <> "QQ" Then
            ExecuteNonQuerry(InsertCustId4AOP(tmpCustID, myStaff.City), Conn)
            SingleUpLoadCustomer2VNPT(tmpCustID)
        End If
        If Not WhoIs.Contains("TC") Then
            MsgBox("Customer Has Been Created. Click to [More Detail] to set AL and Channel for it", MsgBoxStyle.Information, msgTitle)
        End If


    End Sub

    Private Sub CmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LckCmdSave.Click
        Dim i As Int16 = MsgBox("Are You Sure That All Input Are Correct and Wanna Save Changes?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, msgTitle)
        If i = vbNo Then Exit Sub
        If SaiSoDT() Or SaiEmail(1) Or Me.txtFullName.Text = "" Then
            Exit Sub
        End If
        If CmbChannel.Text = "" Then
            MsgBox("You must select Channel for this customer!")
            Exit Sub
        End If
        If MyCust.SaveChange(Me.txtFullName.Text.Replace("--", ""), Me.txtTaxCode.Text.Replace("--", "") _
                          , Replace(Me.TxtEmail.Text, ",", ";"), Me.TxtPhone.Text _
                          , Me.txtAddress.Text.Replace("--", ""), Me.txtCustCode.Text _
                          , Me.CmbLocation.Text, txtInvoiceEmail.Text) Then
            RefreshCustList()
            SingleUpLoadCustomer2VNPT(txtCustCode.Text)
        Else
            MsgBox("Unable to update Customer")
        End If

    End Sub

    Private Sub GridCustList_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridCustList.CellClick
        Dim custID As Integer, StrSQL As String
        On Error GoTo ErrHandler
        custID = Me.GridCustList.Item("RecID", e.RowIndex).Value
        On Error GoTo 0
        Me.LckCmdAdd.Visible = False
        Me.LckCmdSave.Visible = True
        Me.LckLblDeactivate.Visible = Not Me.ChkXXOnly.Checked
        Me.LckLblReinstate.Visible = Me.ChkXXOnly.Checked
        Me.txtCustCode.Text = Me.GridCustList.CurrentRow.Cells("RecID").Value
        Me.txtShortName.Text = Me.GridCustList.CurrentRow.Cells("CustShortName").Value
        Me.txtFullName.Text = Me.GridCustList.CurrentRow.Cells("CustFullName").Value
        Me.txtTaxCode.Text = Me.GridCustList.CurrentRow.Cells("CustTaxCode").Value
        Me.txtAddress.Text = Me.GridCustList.CurrentRow.Cells("CustAddress").Value
        Me.TxtEmail.Text = Me.GridCustList.CurrentRow.Cells("Email").Value
        Me.txtInvoiceEmail.Text = Me.GridCustList.CurrentRow.Cells("InvoiceEmail").Value
        Me.TxtPhone.Text = Me.GridCustList.CurrentRow.Cells("Phone").Value
        Me.CmbLocation.Text = Me.GridCustList.CurrentRow.Cells("Location").Value
        CmbChannel.SelectedIndex = CmbChannel.FindStringExact(GridCustList.CurrentRow.Cells("Channel").Value)
        StrSQL = String.Format("select SBU, AL, Channel, CustLevel, ValidFrom, ValidThru from Cust_Channel_level where " &
            " status ='OK' and custID={0}", custID)
        Me.GridCommDetail.DataSource = GetDataTable(StrSQL)
        Me.GridCommDetail.Columns("SBU").Width = 32
        Me.GridCommDetail.Columns("AL").Width = 32
        Me.GridCommDetail.Columns("Channel").Width = 32
        Me.GridCommDetail.Columns("CustLevel").Width = 32
        Me.GridCommDetail.Columns("ValidFrom").Width = 64
        Me.GridCommDetail.Columns("ValidThru").Width = 64
ErrHandler:
        Me.TxtEmail.ForeColor = Color.Black
        Exit Sub
    End Sub

    Private Sub LblDeactivate_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LckLblDeactivate.LinkClicked
        Dim i As Integer
        i = MsgBox("This Gonna DeActivate Selected Customer. Are You Sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, msgTitle)
        If i = vbNo Then Exit Sub
        i = ScalarToInt("cc_Setting", "RecID", String.Format(" custid={0}", Me.txtCustCode.Text))
        If i > 0 Then
            MsgBox("Cần Clear Balance và Delete Payment option trước khi Deactivate khách hàng.", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        If Me.GridCustList.CurrentRow.Cells("APP").Value = "RAS" Then
            cmd.CommandText = ChangeStatus_ByID("CustomerList", "EX", Me.txtCustCode.Text)
        Else
            cmd.CommandText = "update CustomerList set App=replace(app,'RAS','') where RecID=" & CInt(Me.txtCustCode.Text)
        End If
        cmd.CommandText = cmd.CommandText & "; " & ChangeStatus_ByDK("OfficeID", "XX", String.Format("CustID={0}", Me.txtCustCode.Text)) &
            "; " & ChangeStatus_ByDK("Cust_Detail", "XX", String.Format(" cat='Channel' and status='OK' and CustID={0}", Me.txtCustCode.Text)) &
            "; " & ChangeStatus_ByDK("cust_Channel_level", "XX", String.Format("CustID={0}", Me.txtCustCode.Text))
        cmd.ExecuteNonQuery()
        RefreshCustList()
    End Sub

    Private Sub ChkXXOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkXXOnly.CheckedChanged
        RefreshCustList()
    End Sub

    Private Sub LblReinstate_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LckLblReinstate.LinkClicked
        If MsgBox("This Gonna Reinstate Selected Customer. Are You Sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, msgTitle) = vbNo Then Exit Sub
        Dim CustID As Integer = ScalarToInt("Cust_detail", "top 1 RecID", "CAT='Channel' and custId=" & Me.txtCustCode.Text & " order by recID desc")
        cmd.CommandText = ChangeStatus_ByID("Customer", "OK", Me.txtCustCode.Text) &
            ";" & ChangeStatus_ByID("Cust_detail", "OK", CustID)
        cmd.ExecuteNonQuery()
        RefreshCustList()
    End Sub

    Private Sub LblMoreDetail_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LckLblMoreDetail.LinkClicked
        Dim f As Form = New CustDetail("|FT")
        f.ShowDialog()
    End Sub
    Private Sub TxtEmail_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtEmail.Enter
        If Me.TxtEmail.Text.Length > 4 AndAlso Me.TxtEmail.Text.Substring(0, 4) = "Plz " Then
            Me.TxtEmail.Text = ""
            Me.TxtEmail.ForeColor = Color.Black
        End If
    End Sub

    Private Sub CmbChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbChannel.SelectedIndexChanged
        If Me.CmbChannel.Text <> "TA" And myStaff.SICode <> "SYS" Then
            Me.LckLblMoreDetail.Enabled = False
        Else
            Me.LckLblMoreDetail.Enabled = True
        End If
    End Sub

    Private Sub GridCustList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridCustList.CellContentClick

    End Sub
End Class