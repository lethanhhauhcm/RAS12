Imports RAS12.MySharedFunctions
Imports System.IO
Public Class frmCTS_MISC
    Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
    Private FullList As Int16, ShortList As Int16
    Dim ShowFullCustList As String, ShowShortCustList As String
    Dim msgTitle As String = "GO Settings MISC"
    Private Sub CTS_MISC_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
        pobjTvcs.Disconnect()
    End Sub
    Private Sub CTS_MISC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        pobjTvcs.Connect()
        'ShowShortCustList = "select RecID as VAL, CustShortName as DIS  from CustomerList" _
        '                    & " where status='OK' and RecID in " _
        '                    & "(select CustID from Ras12.dbo.cust_Detail" _
        '                    & " where status+CAT ='OKChannel' and val in ('LC','CS')) "

        'ShowFullCustList = ShowShortCustList & " and RecID not in (select custId from GO_CompanyInfo1)"

        ShowFullCustList = "select RecID as VAL, CustShortName as DIS  from CustomerList" _
                                    & " where status='OK' and RecID in " _
                                    & "(select CustID from Ras12.dbo.cust_Detail" _
                                    & " where status+CAT ='OKChannel' and val in ('LC','CS')) "

        ShowShortCustList = ShowFullCustList & " and RecID not in (select custId from GO_CompanyInfo1)"

        LoadCmb_VAL(Me.CmbCust, ShowFullCustList)

        ShortList = Me.CmbCust.Items.Count
        LoadGridHTL()
        LoadCmb_MSC(Me.cmbCity, "select distinct City as VAL from cityCode where country='" & Me.TxtCountry.Text & "'")
        LoadGridRBD()
        LoadGridCompany()
        LoadGridCorpFare()
        LoadGridALSF()
    End Sub
    Private Sub LoadGridALSF()
        On Error Resume Next
        Me.GridALSF.DataSource = pobjTvcs.GetDataTable("Select * from cwt.dbo.AL_SF where status='OK'")
        Me.GridALSF.Columns("RecID").Visible = False
        For i As Int16 = 1 To 10
            Me.GridALSF.Columns(i).Width = 36
        Next
        Me.LblDeleteALSF.Visible = False
        On Error GoTo 0
    End Sub
    Private Sub CmbCust_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCust.SelectedIndexChanged
        Try
            LoadGridEmployee(Me.CmbCust.SelectedValue)
            If TabControl1.SelectedTab.Name = "TabRptField" Then
                LoadDefaultValues4Rpt()
                OptShowFieldALL.PerformClick()
            ElseIf TabControl1.SelectedTab.Name = "TabRptFieldNew" Then
                LoadTktListingNew()

            End If

            Me.CmbTMC.Text = ""
            Me.TxtEmployeeID.Text = "0"
            If Me.CmbCust.Text = "SANOFI" Then
                Me.Label5.Text = "CC: ID_Name"
                Me.TxtTravelerName.Enabled = False
                Me.Label6.Text = "Product"
                Me.Label13.Text = "Entity"
            Else
                Me.TxtTravelerName.Enabled = True
                Me.Label5.Text = "Traveler"
                Me.Label6.Text = "EmployeeName"
                Me.Label13.Text = "Location"
            End If
            Me.TxtEmployeeID.Enabled = Me.TxtTravelerName.Enabled
        Catch ex As Exception
            '  MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub LoadGridRBD()
        On Error Resume Next
        Me.GridRBD.DataSource = pobjTvcs.GetDataTable("Select * from cwt.dbo.rbd_Cabin where status='OK'")
        Me.GridRBD.Columns("RecID").Visible = False
        Me.GridRBD.Columns("AL").Width = 32
        Me.GridRBD.Columns("F").Width = 32
        Me.GridRBD.Columns("C").Width = 48
        Me.GridRBD.Columns("Status").Width = 36
        Me.GridRBD.Columns("FstUser").Width = 36
        Me.GridRBD.Columns("LstUser").Width = 36
        Me.GridRBD.Columns("Comm").Width = 40
        Me.LblDeleteRBD.Visible = False
        On Error GoTo 0
    End Sub
    Private Sub LoadGridEmployee(ByVal pCustId As Integer)
        On Error Resume Next
        Me.GridEmployee.DataSource = pobjTvcs.GetDataTable("Select * from go_EmployeeID where status='OK' and custID=" & pCustId)
        Me.GridEmployee.Columns("RecID").Visible = False
        Me.GridEmployee.Columns("CustID").Visible = False
        Me.GridEmployee.Columns("LstUser").Visible = False
        Me.GridEmployee.Columns("LstUpdate").Visible = False
        'Me.GridEmployee.Columns("ClientID").Width = 56
        Me.GridEmployee.Columns("EmplID").Width = 56
        Me.GridEmployee.Columns("CostCenter").Width = 64
        Me.LblDeleteEmployeeInf.Visible = False
        Me.LblAddTraveler.Visible = False
        Me.LblUpdateEmployeeInf.Visible = False
        On Error GoTo 0
    End Sub
    Private Sub LoadGridCompany()
        Dim c As Int16
        On Error Resume Next
        Me.GridCompany.DataSource = pobjTvcs.GetDataTable("Select * from GO_CompanyInfo1 where status='OK' order by CompanyName")
        Me.GridCompany.Columns("RecID").Visible = False
        Me.GridCompany.Columns("CustID").Visible = False
        Me.GridCompany.Columns("CMC").Width = 56
        Me.GridCompany.Columns("CustShortName").Width = 128

        For c = 7 To Me.GridCompany.ColumnCount - 1
            Me.GridCompany.Columns(c).Width = 64
        Next
        Me.LblDeleteCompany.Visible = False
        Me.LblAddMail.Visible = False
        On Error GoTo 0
    End Sub

    Private Sub GridEmployee_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridEmployee.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        Me.LblDeleteEmployeeInf.Visible = True
        Me.LblUpdateEmployeeInf.Visible = True
        Me.TxtEmployeeName.Text = Me.GridEmployee.CurrentRow.Cells("Employee").Value
        Me.TxtCostCenter.Text = Me.GridEmployee.CurrentRow.Cells("CostCenter").Value
        Me.TxtTravelerName.Text = Me.GridEmployee.CurrentRow.Cells("Traveler").Value
        TxtEmployeeID.Text = GridEmployee.CurrentRow.Cells("EmplID").Value
        txtDept.Text = GridEmployee.CurrentRow.Cells("Dept").Value
        txtLocation.Text = GridEmployee.CurrentRow.Cells("Location").Value
    End Sub
    Private Sub LblAddEmployeeID_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblAddTraveler.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        If Me.TxtTravelerName.Text = "" Then
            MsgBox("Invalid Empty Fields. Plz Check", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        cmd.CommandText = "insert go_EmployeeID (CustID, EmplID, CostCenter, Employee, Traveler, Dept, Location, FstUser) values (" &
            "@CustID, @EmplID, @CostCenter, @Employee, @Traveler, @Dept, @Location, @FstUser)"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@CustID", SqlDbType.Int).Value = Me.CmbCust.SelectedValue
        cmd.Parameters.Add("@EmplID", SqlDbType.VarChar).Value = Me.TxtEmployeeID.Text
        cmd.Parameters.Add("@CostCenter", SqlDbType.VarChar).Value = Me.TxtCostCenter.Text
        cmd.Parameters.Add("@Employee", SqlDbType.VarChar).Value = Me.TxtEmployeeName.Text
        cmd.Parameters.Add("@Traveler", SqlDbType.VarChar).Value = Me.TxtTravelerName.Text
        cmd.Parameters.Add("@Dept", SqlDbType.VarChar).Value = Me.txtDept.Text
        cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = Me.txtLocation.Text
        cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode

        cmd.ExecuteNonQuery()
        LoadGridEmployee(Me.CmbCust.SelectedValue)
    End Sub


    Private Sub LblDeleteEmployeeInf_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDeleteEmployeeInf.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        Dim myAns As Int16
        myAns = MsgBox("Are You Sure?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgTitle)
        If myAns = vbNo Then Exit Sub
        cmd.CommandText = ChangeStatus_ByID("Go_employeeID", "XX", Me.GridEmployee.CurrentRow.Cells("RecID").Value)
        cmd.ExecuteNonQuery()
        LoadGridEmployee(Me.CmbCust.SelectedValue)
    End Sub

    Private Sub LblUpdateEmployeeInf_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateEmployeeInf.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        If Me.TxtTravelerName.Text = "" Then
            MsgBox("Invalid Empty Fields. Plz Check", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        cmd.CommandText = "Update GO_EmployeeID set CostCenter=@CostCenter, Employee=@Employee, EmplID=@EmplID, LstUser=@LstUser," &
            "Dept=@Dept, Location=@Location where recid=@recid"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@CostCenter", SqlDbType.VarChar).Value = Me.TxtCostCenter.Text
        cmd.Parameters.Add("@Employee", SqlDbType.VarChar).Value = Me.TxtEmployeeName.Text
        cmd.Parameters.Add("@EmplID", SqlDbType.VarChar).Value = Me.TxtEmployeeID.Text
        cmd.Parameters.Add("@LstUser", SqlDbType.VarChar).Value = myStaff.SICode
        cmd.Parameters.Add("@Dept", SqlDbType.VarChar).Value = Me.txtDept.Text
        cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = Me.txtLocation.Text
        cmd.Parameters.Add("@recid", SqlDbType.Int).Value = Me.GridEmployee.CurrentRow.Cells("RecID").Value
        cmd.ExecuteNonQuery()
        LoadGridEmployee(Me.CmbCust.SelectedValue)
    End Sub

    Private Sub cmbService_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbService.SelectedIndexChanged
        Me.LstSIR.Items.Clear()
        Dim dReader As SqlClient.SqlDataReader
        Dim Cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        Cmd.CommandText = String.Format("select VAL from MISC where CAT='SIR{0}' order by VAL", Me.cmbService.Text)
        dReader = Cmd.ExecuteReader
        Do While dReader.Read
            Me.LstSIR.Items.Add(dReader.Item("VAL"))
        Loop
        dReader.Close()
    End Sub

    Private Sub LblUpdateSIR_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateSIR.LinkClicked
        Dim Cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand, strSQL As String
        strSQL = "update Cust_Detail set Status='XX' where custid=" & Me.CmbCust.SelectedValue _
                & " and status+CAT+VAL1='OKSIR" & Me.cmbService.Text & "'"
        For i As Int16 = 0 To Me.LstSIR.Items.Count - 1
            If Me.LstSIR.GetItemChecked(i) Then
                strSQL = strSQL & "; insert cust_Detail (custID, cat, val, val1, FstUser) values (" & Me.CmbCust.SelectedValue & ",'SIR','" &
                    Me.LstSIR.Items(i).ToString & "','" & Me.cmbService.Text & "','" & myStaff.SICode & "')"
            End If
        Next
        Cmd.CommandText = strSQL
        Cmd.ExecuteNonQuery()
    End Sub
    Private Sub GridHTL_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridHTL.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        Me.LblDeleteHTL.Visible = True
        Me.LblUpdateHTL.Text = "Save"
        Me.TxtHTLName.Text = Me.GridHTL.CurrentRow.Cells("HTLName").Value
        Me.TxtHtlPhone.Text = Me.GridHTL.CurrentRow.Cells("Tel").Value
        Me.TxtHtlAddress.Text = Me.GridHTL.CurrentRow.Cells("Address").Value
        If Me.GridHTL.CurrentRow.Cells("Status").Value = "--" Then
            Me.TxtHKey.Enabled = True
            Me.LblUpdateHKey.Enabled = True
        Else
            Me.TxtHKey.Enabled = False
            Me.LblUpdateHKey.Enabled = False
        End If
    End Sub
    Private Sub LoadGridHTL()
        Me.LblDeleteHTL.Visible = False
        Me.LblUpdateHTL.Text = "Update"
        On Error Resume Next
        Me.GridHTL.DataSource = pobjTvcs.GetDataTable("Select RecID, HTLName, Tel, Address, CityCode, HarpKey, status from cwt.dbo.GO_HotelListTv where status<>'XX'")
        Me.GridHTL.Columns("RecID").Visible = False
        Me.GridHTL.Columns("CityCode").Width = 32
        Me.GridHTL.Columns("tel").Width = 56
        Me.GridHTL.Columns("Address").Width = 200
        Me.GridHTL.Columns("HTLName").Width = 200
        Me.GridHTL.Columns("harpkey").Width = 75
        Me.TxtHKey.Enabled = False
        Me.LblUpdateHKey.Enabled = False
    End Sub
    Private Sub LblDeleteHTL_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDeleteHTL.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        cmd.CommandText = String.Format("update cwt.dbo.GO_HotelListTv set Status='XX', lstupdt=getdate() where recid ={0}",
            Me.GridHTL.CurrentRow.Cells("recID").Value)
        cmd.ExecuteNonQuery()
        LoadGridHTL()
    End Sub
    Private Function InvalidAddress(pAddr As String) As Boolean
        If pAddr = "" Then Return True
        Dim English As String = "[A-Z]|[a-z]|[0-9]"
        For i As Int16 = 0 To pAddr.Length - 1
            If InStr(", #-.", pAddr.Substring(i, 1)) = 0 Then
                If Not System.Text.RegularExpressions.Regex.IsMatch(pAddr.Substring(i, 1), English) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function
    Private Function InvalidHTLName(pHTLName As String) As Boolean
        If pHTLName = "" Then Return True
        For i As Int16 = 0 To pHTLName.Length - 1
            If Asc(pHTLName.Substring(i, 1)) > 127 Then Return True
        Next
        Return False
    End Function

    Private Sub LblUpdateHTL_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateHTL.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        Me.LblUpdateHTL.Visible = False
        If InvalidAddress(Me.TxtHtlAddress.Text) Then Exit Sub
        If InvalidHTLName(Me.TxtHTLName.Text) Then Exit Sub
        cmd.Parameters.Clear()
        If Me.LblUpdateHTL.Text = "Update" Then
            cmd.CommandText = "insert cwt.dbo.GO_HotelListTv (HtlName, Tel, Address, Status, CityCode) values (" &
                "@HtlName, @Tel, @Address, @Status, @CityCode)"
            cmd.Parameters.Add("@Service", SqlDbType.VarChar).Value = "Accommodations_Conf.Room_Conf.Equipments"
            cmd.Parameters.Add("@Contact", SqlDbType.VarChar).Value = "||" & Me.TxtHtlPhone.Text & "|"
            cmd.Parameters.Add("@HtlName", SqlDbType.VarChar).Value = Me.TxtHTLName.Text
            cmd.Parameters.Add("@Tel", SqlDbType.VarChar).Value = Me.TxtHtlPhone.Text
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = Me.TxtHtlAddress.Text
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "--"
            cmd.Parameters.Add("@CityCode", SqlDbType.VarChar).Value = Me.cmbCity.Text
            cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        Else
            cmd.CommandText = "Update cwt.dbo.GO_HotelListTv set HtlName=@HtlName, Tel=@Tel, Address=@Address where RecID=@RecID"
            cmd.Parameters.Add("@HtlName", SqlDbType.VarChar).Value = Me.TxtHTLName.Text
            cmd.Parameters.Add("@Tel", SqlDbType.VarChar).Value = Me.TxtHtlPhone.Text
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = Me.TxtHtlAddress.Text
            cmd.Parameters.Add("@RecID", SqlDbType.Int).Value = Me.GridHTL.CurrentRow.Cells("recID").Value
        End If
        cmd.ExecuteNonQuery()
        Me.TxtHTLName.Text = ""
        Me.TxtHtlAddress.Text = ""
        Me.TxtHtlPhone.Text = ""
        LoadGridHTL()
    End Sub

    Private Sub TxtHtlAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtHtlAddress.TextChanged
        Me.LblUpdateHTL.Visible = True
    End Sub

    Private Sub TxtCountry_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCountry.LostFocus
        LoadCmb_MSC(Me.cmbCity, "select distinct City as VAL from cityCode where country='" & Me.TxtCountry.Text & "'")
    End Sub
    Private Sub OptShowFieldALL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        OptShowFieldALL.Click, OptShowFieldINT.Click, OptShowFieldDOM.Click
        Me.LblUpdateFieldShown.Enabled = True
        Dim i As Int16, Dom_int As String = "ALL"
        Dim Cmd As SqlClient.SqlCommand = pobjTvcs.Connection2.CreateCommand
        Dim dReader As SqlClient.SqlDataReader

        If Me.OptShowFieldDOM.Checked Then Dom_int = "DOM"
        If Me.OptShowFieldINT.Checked Then Dom_int = "INT"
        Cmd.CommandText = "Select Val,Val1 from cust_detail where status+cat='OKTKTLSTG" & Dom_int & "' and custid=" & Me.CmbCust.SelectedValue
        dReader = Cmd.ExecuteReader

        InvisibleCustomedReportFields()

        For i = 0 To Me.LstShowField.Items.Count - 1
            Me.LstShowField.SetItemChecked(i, False)
        Next
        Do While dReader.Read
            For i = 0 To Me.LstShowField.Items.Count - 1
                '             MsgBox(LstShowField.Items(i).ToString)
                If Me.LstShowField.Items(i).ToString = dReader.Item("VAL") Then
                    Me.LstShowField.SetItemChecked(i, True)
                    Select Case dReader.Item("VAL")
                        Case "J : Employee ID"
                            Label38.Visible = True
                            CmbField0.Visible = True
                            CmbField0.SelectedIndex = CmbField0.FindStringExact(dReader.Item("VAL1"))
                        Case "K : Cost Center"
                            Label39.Visible = True
                            CmbField1.Visible = True
                            CmbField1.SelectedIndex = CmbField1.FindStringExact(dReader.Item("VAL1"))
                        Case "M : WBS"
                            LblWBSField.Visible = True
                            CmbField3.Visible = True
                            CmbField3.SelectedIndex = CmbField3.FindStringExact(dReader.Item("VAL1"))
                        Case "AH: EngagementCode"
                            Label43.Visible = True
                            CmbField24.Visible = True
                            CmbField24.SelectedIndex = CmbField24.FindStringExact(dReader.Item("VAL1"))
                        Case "AM: WBS 2"
                            LblWBSField2.Visible = True
                            CmbField25.Visible = True
                            CmbField25.SelectedIndex = CmbField25.FindStringExact(dReader.Item("VAL1"))
                        Case "AN: RequiredData1"
                            ActivateRequiredDataField(lblRequiredData1, cboRequiredData1, dReader.Item("VAL1"))

                        Case "AR: RequiredData2"
                            ActivateRequiredDataField(lblRequiredData2, cboRequiredData2, dReader.Item("VAL1"))

                        Case "AS: RequiredData3"
                            ActivateRequiredDataField(lblRequiredData3, cboRequiredData3, dReader.Item("VAL1"))

                        Case "AT: RequiredData4"
                            ActivateRequiredDataField(lblRequiredData4, cboRequiredData4, dReader.Item("VAL1"))

                        Case "AU: RequiredData5"
                            ActivateRequiredDataField(lblRequiredData5, cboRequiredData5, dReader.Item("VAL1"))

                        Case "AV: RequiredData6"
                            ActivateRequiredDataField(lblRequiredData6, cboRequiredData6, dReader.Item("VAL1"))
                    End Select
                End If
            Next
        Loop
        dReader.Close()
    End Sub
    Private Function ActivateRequiredDataField(ByRef lblRequiredData As System.Windows.Forms.Label _
                                               , ByRef cboRequiredData As ComboBox, strDataCode As String) As Boolean
        lblRequiredData.Visible = True
        cboRequiredData.Visible = True

        If strDataCode.StartsWith("Ref") Then
            cboRequiredData.SelectedIndex = cboRequiredData1.FindStringExact(strDataCode)
        Else
            cboRequiredData.SelectedIndex = cboRequiredData1.FindStringExact(
            pobjTvcs.GetRequiredDataNameByCust(CmbCust.SelectedValue, strDataCode))
        End If
        Return True

    End Function
    Private Sub InvisibleCustomedReportFields()
        Me.LblWBSField.Visible = False
        Me.CmbField3.Visible = False

        Me.Label38.Visible = False
        Me.CmbField0.Visible = False

        Me.Label39.Visible = False
        Me.CmbField1.Visible = False

        Me.Label43.Visible = False
        Me.CmbField24.Visible = False

        LblWBSField2.Visible = False
        CmbField25.Visible = False

        lblRequiredData1.Visible = False
        cboRequiredData1.Visible = False
    End Sub
    Private Sub LstShowField_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
                Handles LstShowField.MouseUp
        Me.LblWBSField.Visible = Me.LstShowField.GetItemChecked(3)
        Me.CmbField3.Visible = Me.LstShowField.GetItemChecked(3)

        Me.Label38.Visible = Me.LstShowField.GetItemChecked(0)
        Me.CmbField0.Visible = Me.LstShowField.GetItemChecked(0)

        Me.Label39.Visible = Me.LstShowField.GetItemChecked(1)
        Me.CmbField1.Visible = Me.LstShowField.GetItemChecked(1)

        Me.Label43.Visible = Me.LstShowField.GetItemChecked(24)
        Me.CmbField24.Visible = Me.LstShowField.GetItemChecked(24)

        LblWBSField2.Visible = Me.LstShowField.GetItemChecked(25)
        CmbField25.Visible = Me.LstShowField.GetItemChecked(25)

        lblRequiredData1.Visible = Me.LstShowField.GetItemChecked(26)
        cboRequiredData1.Visible = Me.LstShowField.GetItemChecked(26)
        lblRequiredData2.Visible = Me.LstShowField.GetItemChecked(27)
        cboRequiredData2.Visible = Me.LstShowField.GetItemChecked(27)
        lblRequiredData3.Visible = Me.LstShowField.GetItemChecked(28)
        cboRequiredData3.Visible = Me.LstShowField.GetItemChecked(28)
        lblRequiredData4.Visible = Me.LstShowField.GetItemChecked(29)
        cboRequiredData4.Visible = Me.LstShowField.GetItemChecked(29)
        lblRequiredData5.Visible = Me.LstShowField.GetItemChecked(30)
        cboRequiredData5.Visible = Me.LstShowField.GetItemChecked(30)
        lblRequiredData6.Visible = Me.LstShowField.GetItemChecked(31)
        cboRequiredData6.Visible = Me.LstShowField.GetItemChecked(31)
    End Sub

    Private Sub LblUpdateFieldShown_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateFieldShown.LinkClicked
        Dim tmpStrSQL As String = "", Dom_int As String = "ALL"
        Dim RefField As String
        Dim Cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        If Me.OptShowFieldDOM.Checked Then Dom_int = "DOM"
        If Me.OptShowFieldINT.Checked Then Dom_int = "INT"
        For i As Int16 = 0 To Me.LstShowField.Items.Count - 1
            If Me.LstShowField.GetItemChecked(i) Then

                Select Case i
                    Case 0, 1, 3, 24, 25
                        RefField = Me.TabRptField.Controls("CmbField" & i.ToString.Trim).Text
                    Case 26
                        RefField = cboRequiredData1.SelectedValue
                    Case 27
                        RefField = cboRequiredData2.SelectedValue
                    Case 28
                        RefField = cboRequiredData3.SelectedValue
                    Case 29
                        RefField = cboRequiredData4.SelectedValue
                    Case 30
                        RefField = cboRequiredData5.SelectedValue
                    Case 31
                        RefField = cboRequiredData6.SelectedValue
                    Case Else
                        RefField = ""
                End Select
                tmpStrSQL = tmpStrSQL & "; insert cust_Detail (custID, Cat, VAL, VAL1, FstUser) values ("
                tmpStrSQL = tmpStrSQL & Me.CmbCust.SelectedValue & ",'TKTLSTG" & Dom_int & "','" & Me.LstShowField.Items(i).ToString
                tmpStrSQL = tmpStrSQL & "','" & RefField & "','" & myStaff.SICode & "')"
            End If
        Next
        If tmpStrSQL.Length > 2 Then
            'Cmd.CommandText = ChangeStatus_ByDK("cust_detail", "XX", String.Format(" status+cat='OKTKTLSTG{0}' and custid={1}", _
            '     Dom_int, Me.CmbCust.SelectedValue)) & tmpStrSQL
            Cmd.CommandText = ("Update cust_detail set Status='XX' where " _
                            & String.Format(" status+cat='OKTKTLSTG{0}' and custid={1}",
                            Dom_int, Me.CmbCust.SelectedValue)) & tmpStrSQL
            Cmd.ExecuteNonQuery()
        End If
        Me.LblUpdateFieldShown.Enabled = False
    End Sub

    Private Sub LblUpdateCabin_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateCabin.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        cmd.CommandText = "Update cwt.dbo.rbd_Cabin set Status='XX' where AL='" & Me.txtAL.Text & "'"
        cmd.ExecuteNonQuery()
        cmd.CommandText = "Insert cwt.dbo.rbd_Cabin  (AL, F, C, Comm, FstUser) values (@AL, @F, @C, @Comm, @FstUser)"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@AL", SqlDbType.VarChar).Value = Me.txtAL.Text
        cmd.Parameters.Add("@F", SqlDbType.VarChar).Value = Me.txtFirst.Text
        cmd.Parameters.Add("@C", SqlDbType.VarChar).Value = Me.txtBusiness.Text
        cmd.Parameters.Add("@Comm", SqlDbType.VarChar).Value = Me.TxtALComm.Text
        cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        cmd.ExecuteNonQuery()
        LoadGridRBD()
    End Sub

    Private Sub LblDeleteRBD_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDeleteRBD.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        cmd.CommandText = ChangeStatus_ByID("cwt.dbo.rbd_Cabin", "XX", Me.GridRBD.CurrentRow.Cells("RecID").Value)
        cmd.ExecuteNonQuery()
        LoadGridRBD()
    End Sub

    Private Sub GridRBD_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridRBD.CellContentClick
        Me.LblDeleteRBD.Visible = True
    End Sub

    Private Sub LblUpdateCompany_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateCompany.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        Select Case txtAOffice1.Text.Length
            Case 0, 9
            Case Else
                MsgBox("Invalid Office ID 1")
        End Select
        Select Case txtAOffice2.Text.Length
            Case 0, 9
            Case Else
                MsgBox("Invalid Office ID 2")
        End Select

        If Me.CmbTMC.Text = "" Or Me.TxtCMC.Text = "" Then
            MsgBox("Empty TMC or CMC. Action aborted", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If
        If Not CheckFormatComboBox(cboCity,, 3, 3) Then
            Exit Sub
        End If
        Dim intDifferentCompanyName As Integer = pobjTvcs.GetScalarAsDecimal("select count (*) from go_CompanyInfo1 where status='OK' and CMC='" _
                                            & Me.TxtCMC.Text & "' and CompanyName<>'" & TxtClientName.Text & "'")
        If intDifferentCompanyName > 0 And LblUpdateCompany.Text = "Update" Then
            MsgBox("1 CMC must have only 1 CompanyName!")
            Exit Sub
        End If

        Dim tmpCMC As Integer
        tmpCMC = pobjTvcs.GetScalarAsDecimal("select RecID from go_CompanyInfo1 where status='OK' and CMC='" _
                                            & Me.TxtCMC.Text & "'")
        If tmpCMC > 0 Then
            If Me.LblUpdateCompany.Text = "Save" AndAlso
                 tmpCMC <> Me.GridCompany.CurrentRow.Cells("RecID").Value Then
                If MsgBox("Same CMC Exists for another Record. Click OK to Save or Cancel to Ignore") _
                        <> MsgBoxResult.Ok Then
                    Exit Sub
                ElseIf Me.LblUpdateCompany.Text = "Update" Then
                    If MsgBox("Same CMC Exists for another Record. Click OK to Save or Cancel to Ignore") _
                        <> MsgBoxResult.Ok Then
                        Exit Sub
                    End If
                End If
            End If
        End If
        cmd.Parameters.Clear()
        cmd.CommandText = "Insert GO_CompanyInfo1 (CustID, CustShortName, CMC, CompanyName" _
            & ", ProfileName1A, GO_Client, DataCapture" _
            & ", NoComm, SOS, TMC, OpsMail, AquaOffc1, AquaOffc2" _
            & ", AquaQueue1, AquaQueue2, FstUser" _
            & ", TKTDateFrom, TKTDateto, OptQ, EmployeeID" _
            & ",FltStats, HtlOffc, HTLQueue,Empl4NonAir,NoData4NonAir" _
            & ",Pnr1aMustHaveEmail,Pnr1aMustHaveMobile,GetReturnDate,VnCorpId,SubUnitName,City)" _
            & " values (@CustID, @CustShortName, @CMC, @CompanyName, @ProfileName1A" _
            & ", @GO_Client, @DataCapture, @NoComm, @SOS, @TMC" _
            & ",@OpsMail, @AquaOffc1, @AquaOffc2, @AquaQueue1, @AquaQueue2, @FstUser" _
            & ", @TKTDateFrom, @TKTDateto, @OptQ, @EmployeeID" _
            & ",@FltStats, @HtlOffc, @HTLQueue,@Empl4NonAir,@NoData4NonAir" _
            & ",@Pnr1aMustHaveEmail,@Pnr1aMustHaveMobile,@GetReturnDate,@VnCorpId,@SubUnitName,@City)"
        cmd.Parameters.Add("@CMC", SqlDbType.VarChar).Value = Me.TxtCMC.Text
        cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Me.TxtClientName.Text
        cmd.Parameters.Add("@ProfileName1A", SqlDbType.VarChar).Value = Me.TxtProfileName.Text
        cmd.Parameters.Add("@GO_Client", SqlDbType.Bit).Value = IIf(Me.ChkGO.Checked, 1, 0)
        cmd.Parameters.Add("@DataCapture", SqlDbType.Bit).Value = IIf(Me.ChkDataCapture.Checked, 1, 0)
        cmd.Parameters.Add("@NoComm", SqlDbType.Bit).Value = IIf(Me.ChkNoComm.Checked, 1, 0)
        cmd.Parameters.Add("@SOS", SqlDbType.Bit).Value = IIf(Me.ChkSOS.Checked, 1, 0)
        cmd.Parameters.Add("@TMC", SqlDbType.VarChar).Value = Me.CmbTMC.Text
        cmd.Parameters.Add("@OpsMail", SqlDbType.VarChar).Value = Me.CmbEmail.Text
        cmd.Parameters.Add("@AquaOffc1", SqlDbType.VarChar).Value = Me.txtAOffice1.Text
        cmd.Parameters.Add("@AquaOffc2", SqlDbType.VarChar).Value = Me.txtAOffice2.Text
        cmd.Parameters.Add("@AquaQueue1", SqlDbType.VarChar).Value = Me.txtAQ1.Text
        cmd.Parameters.Add("@AquaQueue2", SqlDbType.VarChar).Value = Me.txtAQ2.Text
        cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        cmd.Parameters.Add("@TKTDateFrom", SqlDbType.DateTime).Value = CreateFromDate(Me.txtTKGFrom.Value.Date)
        cmd.Parameters.Add("@TKTDateto", SqlDbType.DateTime).Value = CreateToDate(DateAdd(DateInterval.Day, 1, Me.TxtTKTGThru.Value.Date))
        cmd.Parameters.Add("@OptQ", SqlDbType.Bit).Value = Me.ChkOptQ.Checked
        cmd.Parameters.Add("@EmployeeID", SqlDbType.Bit).Value = Me.ChkEmplID.Checked
        cmd.Parameters.Add("@FltStats", SqlDbType.Bit).Value = Me.chkFltStat.Checked
        cmd.Parameters.Add("@HtlOffc", SqlDbType.VarChar).Value = Me.TxHTLOffc.Text
        cmd.Parameters.Add("@HTLQueue", SqlDbType.VarChar).Value = Me.txtHTLQ.Text
        cmd.Parameters.Add("@Empl4NonAir", SqlDbType.Bit).Value = Me.chkEmpl4NonAir.Checked
        cmd.Parameters.Add("@NoData4NonAir", SqlDbType.Bit).Value = Me.chkNoData4NonAir.Checked
        cmd.Parameters.Add("@Pnr1aMustHaveEmail", SqlDbType.Bit).Value = Me.chkPnr1aMustHaveEmail.Checked
        cmd.Parameters.Add("@Pnr1aMustHaveMobile", SqlDbType.Bit).Value = Me.chkPnr1aMustHaveMobiile.Checked
        cmd.Parameters.Add("@GetReturnDate", SqlDbType.Bit).Value = Me.chkGetReturnDate.Checked
        cmd.Parameters.Add("@VnCorpId", SqlDbType.VarChar).Value = Me.txtVnCorpID.Text
        cmd.Parameters.Add("@SubUnitName", SqlDbType.VarChar).Value = Me.txtSubUnitName.Text
        cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = Me.cboCity.Text
        If Me.LblUpdateCompany.Text = "Update" Then
            cmd.Parameters.Add("@CustID", SqlDbType.Int).Value = Me.CmbCust.SelectedValue
            cmd.Parameters.Add("@CustShortName", SqlDbType.VarChar).Value = Me.CmbCust.Text
            cmd.ExecuteNonQuery()
        ElseIf Me.LblUpdateCompany.Text = "Save" Then
            cmd.Parameters.Add("@CustID", SqlDbType.Int).Value = Me.GridCompany.CurrentRow.Cells("CustID").Value
            cmd.Parameters.Add("@CustShortName", SqlDbType.VarChar).Value = Me.GridCompany.CurrentRow.Cells("CustshortName").Value
            cmd.ExecuteNonQuery()

            cmd.CommandText = ChangeStatus_ByID("go_CompanyInfo1", "XX", Me.GridCompany.CurrentRow.Cells("RecID").Value)
            cmd.ExecuteNonQuery()
        End If

        LoadGridCompany()
    End Sub

    Private Sub CmbTMC_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbTMC.SelectedIndexChanged
        Try
            If Me.CmbTMC.Text = "CWT" Then
                Me.ChkGO.Enabled = True
                Me.TxtCMC.Enabled = True
                Me.TxtCMC.Text = ""
            Else
                Me.ChkGO.Enabled = False
                Me.ChkGO.Checked = False
                Me.TxtCMC.Enabled = False
                Me.TxtCMC.Text = "L" & Me.CmbCust.SelectedValue.ToString.Trim
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridCompany_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridCompany.CellClick
        Me.LblUpdateCompany.Text = "Save"
        Me.CmbTMC.Text = Me.GridCompany.CurrentRow.Cells("TMC").Value
        Me.TxtCMC.Text = Me.GridCompany.CurrentRow.Cells("CMC").Value
        Me.TxtClientName.Text = Me.GridCompany.CurrentRow.Cells("CompanyName").Value
        Me.TxtProfileName.Text = Me.GridCompany.CurrentRow.Cells("ProfileName1A").Value
        Me.txtAOffice1.Text = Me.GridCompany.CurrentRow.Cells("AquaOffc1").Value
        Me.txtAOffice2.Text = Me.GridCompany.CurrentRow.Cells("AquaOffc2").Value
        Me.txtAQ1.Text = Me.GridCompany.CurrentRow.Cells("AquaQueue1").Value
        Me.txtAQ2.Text = Me.GridCompany.CurrentRow.Cells("AquaQueue2").Value
        Me.txtTKGFrom.Text = Me.GridCompany.CurrentRow.Cells("TktDateFrom").Value
        Me.TxtTKTGThru.Text = Me.GridCompany.CurrentRow.Cells("TKTDateTo").Value
        Me.TxHTLOffc.Text = Me.GridCompany.CurrentRow.Cells("htloffc").Value
        Me.txtHTLQ.Text = Me.GridCompany.CurrentRow.Cells("htlQueue").Value

        Me.CmbEmail.Text = Me.GridCompany.CurrentRow.Cells("OpsMail").Value
        Me.ChkGO.Checked = IIf(Me.GridCompany.CurrentRow.Cells("GO_client").Value = 1, True, False)
        Me.ChkSOS.Checked = IIf(Me.GridCompany.CurrentRow.Cells("SOS").Value = 1, True, False)
        Me.ChkNoComm.Checked = IIf(GridCompany.CurrentRow.Cells("NoComm").Value = 1, True, False)
        Me.ChkDataCapture.Checked = GridCompany.CurrentRow.Cells("DataCapture").Value
        Me.chkEmpl4NonAir.Checked = GridCompany.CurrentRow.Cells("Empl4NonAir").Value
        Me.chkNoData4NonAir.Checked = GridCompany.CurrentRow.Cells("NoData4NonAir").Value
        chkPnr1aMustHaveEmail.Checked = GridCompany.CurrentRow.Cells("Pnr1aMustHaveEmail").Value
        chkPnr1aMustHaveMobiile.Checked = GridCompany.CurrentRow.Cells("Pnr1aMustHaveMobile").Value
        chkGetReturnDate.Checked = GridCompany.CurrentRow.Cells("GetReturnDate").Value
        txtSubUnitName.Text = GridCompany.CurrentRow.Cells("SubUnitName").Value
        txtVnCorpID.Text = GridCompany.CurrentRow.Cells("VnCorpID").Value
        cboCity.SelectedIndex = cboCity.FindStringExact(GridCompany.CurrentRow.Cells("City").Value)
        Me.LblDeleteCompany.Visible = True
        Me.CmbCust.Enabled = False
        Me.LblAddMail.Visible = True
    End Sub
    Private Sub LblDeleteCompany_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDeleteCompany.LinkClicked
        cmd.CommandText = ChangeStatus_ByID("CWT..GO_CompanyInfo1", "XX", Me.GridCompany.CurrentRow.Cells("RecID").Value)
        cmd.ExecuteNonQuery()
        LoadGridCompany()
    End Sub
    Private Sub TabEmployee_Enter(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles TabCorpFareExcl.Enter
        Me.CmbCust.Enabled = True
        If Me.CmbCust.Items.Count = ShortList Then
            LoadCmb_VAL(Me.CmbCust, ShowFullCustList)
        End If

        FullList = Me.CmbCust.Items.Count
    End Sub
    Private Sub TabCompany_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabCompany.Enter
        If Me.CmbCust.Items.Count <> ShortList Then
            LoadCmb_VAL(Me.CmbCust, ShowShortCustList)
        End If

    End Sub
    Private Sub LblUpdateHKey_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateHKey.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        cmd.CommandText = "update cwt.dbo.GO_HotelListTv set status='OK', Harpkey=@Harpkey where Recid=@Recid"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@Harpkey", SqlDbType.Int).Value = Me.TxtHKey.Text
        cmd.Parameters.Add("@Recid", SqlDbType.Int).Value = Me.GridHTL.CurrentRow.Cells("RecID").Value
        cmd.ExecuteNonQuery()
        LoadGridHTL()
    End Sub

    Private Sub LblAddMail_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblAddMail.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        Dim tmpMail As String = Me.GridCompany.CurrentRow.Cells("OpsMail").Value
        If InStr(tmpMail, ";") > 0 Or tmpMail = Me.CmbEmail.Text Then Exit Sub
        cmd.CommandText = "update CompanyInfo set opsmail=opsmail + ';" & Me.CmbEmail.Text & "' where recID=" &
            Me.GridCompany.CurrentRow.Cells("RecID").Value
        cmd.ExecuteNonQuery()
        LoadGridCompany()
    End Sub


    Private Sub GridCorpFare_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridCorpFare.CellContentClick
        Me.LblDeleteCorpFare.Visible = True
    End Sub
    Private Sub LoadGridCorpFare()
        On Error Resume Next
        Me.GridCorpFare.DataSource = pobjTvcs.GetDataTable("Select * from cwt.dbo.go_corpFareExclusion where status='OK'")
        Me.LblDeleteCorpFare.Visible = False
        Me.GridCorpFare.Columns(0).Visible = False
        Me.GridCorpFare.Columns(1).Width = 50
        Me.GridCorpFare.Columns(3).Width = 50
    End Sub

    Private Sub LblDeleteCorpFare_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDeleteCorpFare.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        cmd.CommandText = ChangeStatus_ByID("cwt.dbo.go_CorpFareExclusion", "XX", Me.GridCorpFare.CurrentRow.Cells("RecID").Value)
        cmd.ExecuteNonQuery()
        LoadGridCorpFare()
    End Sub

    Private Sub LblUpdateCorpFare_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateCorpFare.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        cmd.CommandText = "Insert cwt.dbo.go_CorpFareExclusion (RasShortName, CustID, TourCodeContains, TKTDateFrom, TKTDateTo, FstUser, CarCode) " &
            "values (@RasShortName, @CustID, @TourCodeContains, @TKTDateFrom, @TKTDateTo, @FstUser, @CarCode)"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@RasShortName", SqlDbType.VarChar).Value = Me.CmbCust.Text
        cmd.Parameters.Add("@TourCodeContains", SqlDbType.VarChar).Value = Me.TxtTC.Text
        cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        cmd.Parameters.Add("@CarCode", SqlDbType.VarChar).Value = Me.TxtDocCode.Text
        cmd.Parameters.Add("@CustID", SqlDbType.Int).Value = Me.CmbCust.SelectedValue
        cmd.Parameters.Add("@TKTDateFrom", SqlDbType.DateTime).Value = Me.txtTKGFrom.Value.Date
        cmd.Parameters.Add("@TKTDateTo", SqlDbType.DateTime).Value = Me.TxtTKTGThru.Value.Date
        cmd.ExecuteNonQuery()
        LoadGridCorpFare()
    End Sub
    Private Sub GridALSF_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridALSF.CellContentClick
        Me.LblDeleteALSF.Visible = True
    End Sub

    Private Sub LblDeleteALSF_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDeleteALSF.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        cmd.CommandText = ChangeStatus_ByID("cwt.dbo.al_sf", "XX", Me.GridALSF.CurrentRow.Cells("recID").Value)
        cmd.ExecuteNonQuery()
        LoadGridALSF()
    End Sub

    Private Sub LblUpdateALSF_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblUpdateALSF.LinkClicked
        Dim cmd As SqlClient.SqlCommand = pobjTvcs.Connection.CreateCommand
        cmd.CommandText = "Insert cwt.dbo.AL_SF  (AL, DomC, DomY, RegF, RegC, RegY, IntF, IntC, IntY, FstUser) values (" &
            "@AL, @DomC, @DomY, @RegF, @RegC, @RegY, @IntF, @IntC, @IntY, @FstUser)"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@AL", SqlDbType.VarChar).Value = Me.txtAL.Text
        cmd.Parameters.Add("@DomC", SqlDbType.Decimal).Value = CDec(Me.TxtDomC.Text)
        cmd.Parameters.Add("@DomY", SqlDbType.Decimal).Value = CDec(Me.TxtDomY.Text)
        cmd.Parameters.Add("@RegF", SqlDbType.Decimal).Value = CDec(Me.TxtRegF.Text)
        cmd.Parameters.Add("@RegC", SqlDbType.Decimal).Value = CDec(Me.TxtRegC.Text)
        cmd.Parameters.Add("@RegY", SqlDbType.Decimal).Value = CDec(Me.TxtRegY.Text)
        cmd.Parameters.Add("@IntF", SqlDbType.Decimal).Value = CDec(Me.TxtIntF.Text)
        cmd.Parameters.Add("@IntC", SqlDbType.Decimal).Value = CDec(Me.TxtIntC.Text)
        cmd.Parameters.Add("@IntY", SqlDbType.Decimal).Value = CDec(Me.TxtIntY.Text)
        cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        cmd.ExecuteNonQuery()
        LoadGridALSF()
    End Sub

    Private Sub TxtTravelerName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtTravelerName.TextChanged
        Me.LblAddTraveler.Visible = True
    End Sub

    Private Sub TxtCostCenter_LostFocus(sender As Object, e As EventArgs) Handles TxtCostCenter.LostFocus
        If Me.CmbCust.Text = "SANOFI" Then
            Me.TxtTravelerName.Text = Me.TxtCostCenter.Text & "-" & Me.TxtEmployeeName.Text
        End If
    End Sub

    Private Sub LoadDefaultValues4Rpt()
        pobjTvcs.LoadComboDisplay(cboRequiredData1, "Select DataCode as Value, NameByCustomer As Display from Go_RequiredData" _
                                               & " where status='OK' and CustId=" & CmbCust.SelectedValue)
        pobjTvcs.LoadComboDisplay(cboRequiredData2, "Select DataCode as Value, NameByCustomer As Display from Go_RequiredData" _
                                               & " where status='OK' and CustId=" & CmbCust.SelectedValue)
        pobjTvcs.LoadComboDisplay(cboRequiredData3, "Select DataCode as Value, NameByCustomer As Display from Go_RequiredData" _
                                               & " where status='OK' and CustId=" & CmbCust.SelectedValue)
        pobjTvcs.LoadComboDisplay(cboRequiredData4, "Select DataCode as Value, NameByCustomer As Display from Go_RequiredData" _
                                               & " where status='OK' and CustId=" & CmbCust.SelectedValue)
        pobjTvcs.LoadComboDisplay(cboRequiredData5, "Select DataCode as Value, NameByCustomer As Display from Go_RequiredData" _
                                               & " where status='OK' and CustId=" & CmbCust.SelectedValue)
        pobjTvcs.LoadComboDisplay(cboRequiredData6, "Select DataCode as Value, NameByCustomer As Display from Go_RequiredData" _
                                               & " where status='OK' and CustId=" & CmbCust.SelectedValue)
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab.Name = "TabRptField" Then
            LoadDefaultValues4Rpt()
        End If
    End Sub


    Private Sub TabRptFieldNew_Enter(sender As Object, e As EventArgs) Handles TabRptFieldNew.Enter
        cboDOMINT.SelectedIndex = 0
        cboReportType.SelectedIndex = 0
    End Sub
    Private Function LoadTktListingNew() As Boolean
        Dim strGetCMC As String = "(Select top 1 CMC from GO_CompanyInfo1 where Status='OK' and CustId=" _
                                  & CmbCust.SelectedValue & ")"
        Dim strGetSelectedDataCodeList = "(Select DataCode from GO_TktListing" _
                                    & " where Status='OK' and CustId=" & CmbCust.SelectedValue _
                                    & " and DOMINT='" & cboDOMINT.Text _
                                    & "' and ReportType='" & cboReportType.Text & "')"

        Dim strQuerry As String = "select Recid,Seq,DataCode,SqlQuerry,Description,NameInReport,DataGroup" _
                                    & ",PreCalculated,InternalUse,cast('True' as bit) as Selected " _
                                    & " from GO_TktListing where Status='OK' and CustId=" & CmbCust.SelectedValue _
                                    & " and DOMINT='" & cboDOMINT.Text & "' and ReportType='" & cboReportType.Text _
                                    & "' union" _
                                    & " select Recid,Seq,DataCode,SqlQuerry,Description,NameInReport,DataGroup" _
                                    & ",PreCalculated,InternalUse, cast('False' as bit) as Selected " _
                                    & " from GO_TktListing where Status='OK' and CustId=0" _
                                    & " and DataCode not in " & strGetSelectedDataCodeList _
                                    & " union" _
                                    & " select Recid,90,'CDR'+CdrNbr as DataCode,'g.Ref'+CdrNbr as SqlQuerry" _
                                    & ",CdrName,CdrName,'GO',cast('False' as bit) as PreCalculated" _
                                    & ",cast('False' as bit) as InternalUse,'False' as Selected " _
                                    & " from [GO_CDRs] where Status='OK' and  CMC=" & strGetCMC _
                                    & " and 'CDR'+CdrNbr not in " & strGetSelectedDataCodeList _
                                    & " union" _
                                    & " select Recid,91,'Hier'+cast(Nbr as varchar) as DataCode" _
                                    & ",'g.Hierachy'+cast(Nbr as varchar) as SqlQuerry,Description,Description,'GO'" _
                                    & ",cast('False' as bit) as Precalculated " _
                                    & ",'False' as InternalUse,cast('False' as bit) as Selected " _
                                    & " from [GO_Hierachies] where Status='OK' and  CMC=" & strGetCMC _
                                    & " and 'Hier'+cast(Nbr as varchar) not in " & strGetSelectedDataCodeList _
                                    & " union" _
                                    & " select Recid,92,DataCode" _
                                    & ",'' as SqlQuerry,NameByCustomer,NameByCustomer,'RequiredData'" _
                                    & ",cast('False' as bit) as PreCalculated " _
                                    & ",cast('False' as bit) as InternalUse,cast('False' as bit) as Selected " _
                                    & " from [GO_RequiredData] where Status='OK' and CustId=" & CmbCust.SelectedValue _
                                    & " and datacode not in " & strGetSelectedDataCodeList _
                                    & " order by Selected desc, Seq,DataCode"

        If CmbCust.SelectedValue = 8085 AndAlso cboDOMINT.Items.Count = 4 Then
            cboDOMINT.Items.Add("GHOSTCARD")
        ElseIf CmbCust.SelectedValue <> 8085 AndAlso cboDOMINT.Items.Count > 4 Then
            cboDOMINT.Items.RemoveAt(4)
        End If

        pobjTvcs.LoadDataGridView(dgTktListing, strQuerry)
        dgTktListing.Columns("RecId").Visible = False
        dgTktListing.Columns("Description").Visible = False
        dgTktListing.Columns("SqlQuerry").Visible = False
        dgTktListing.Columns("PreCalculated").Visible = False

        Return True
    End Function

    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        Dim lstQuerries As New List(Of String)
        Dim strGetSeq As String = "(select ISNULL(MAX(seq),0)+1 from GO_TktListing where Status='OK' and CustID=" _
                        & CmbCust.SelectedValue & " and DOMINT='" & cboDOMINT.Text _
                        & "' and ReportType='" & cboReportType.Text & "')"
        With dgTktListing.CurrentRow
            '^_^20221031 mark by 7643 -b-
            'lstQuerries.Add("insert into GO_TktListing (ReportType,CustId,DOMINT,Seq,DataCode,Description,NameInReport" _
            '                & ",DataGroup,SqlQuerry,PreCalculated,InternalUse,FstUser) values ('" _
            '                & cboReportType.Text _
            '                & "'," & CmbCust.SelectedValue & ",'" & cboDOMINT.Text & "'," & strGetSeq _
            '                & ",'" & .Cells("DataCode").Value & "',N'" & .Cells("Description").Value _
            '                & "','" & .Cells("NameInReport").Value & "','" & .Cells("DataGroup").Value _
            '                & "','" & Replace(.Cells("SqlQuerry").Value, "'", "''") _
            '                & "','" & .Cells("PreCalculated").Value _
            '                & "','" & .Cells("InternalUse").Value _
            '                & "','" & myStaff.SICode & "')")
            '^_^20221031 mark by 7643 -e-
            '^_^20221031 modi by 7643 -b-
            lstQuerries.Add("insert into GO_TktListing (ReportType,CustId,DOMINT,Seq,DataCode,Description,NameInReport" _
                            & ",DataGroup,SqlQuerry,PreCalculated,InternalUse,FstUser) values ('" _
                            & cboReportType.Text _
                            & "'," & CmbCust.SelectedValue & ",'" & cboDOMINT.Text & "'," & strGetSeq _
                            & ",'" & .Cells("DataCode").Value & "',N'" & .Cells("Description").Value _
                            & "',N'" & .Cells("NameInReport").Value & "','" & .Cells("DataGroup").Value _
                            & "','" & Replace(.Cells("SqlQuerry").Value, "'", "''") _
                            & "','" & .Cells("PreCalculated").Value _
                            & "','" & .Cells("InternalUse").Value _
                            & "','" & myStaff.SICode & "')")
            '^_^20221031 modi by 7643 -e-
        End With
        If pobjTvcs.UpdateListQuerries(lstQuerries, False) Then
            LoadTktListingNew()
        Else
            MsgBox("Unable to update SQL")
        End If
    End Sub

    Private Sub lbkUnselect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUnselect.LinkClicked
        Dim lstQuerries As New List(Of String)
        lstQuerries.Add("Update GO_TktListing Set Status='XX',LstUpdate=getdate(),LstUser='" _
                        & myStaff.SICode & "' where RecId=" & dgTktListing.CurrentRow.Cells("RecId").Value)
        lstQuerries.Add("Update GO_TktListing set Seq=Seq-1,LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                        & "' where Seq>" & dgTktListing.CurrentRow.Cells("Seq").Value _
                        & " and CustId=" & CmbCust.SelectedValue & " and DOMINT='" & cboDOMINT.Text & "'")
        If pobjTvcs.UpdateListQuerries(lstQuerries, False) Then
            LoadTktListingNew()
        Else
            MsgBox("Unable to update SQL")
        End If

    End Sub

    Private Sub lbkChangeSeq_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkChangeSeq.LinkClicked
        Dim lstQuerries As New List(Of String)
        Dim strNewSeq As String = InputBox("Please input new sequence number!")
        Dim intNewSeq As Integer
        Dim intMaxNbr As Integer
        intMaxNbr = pobjTvcs.GetScalarAsDecimal("Select Max(Seq) from GO_TktListing" _
                                                & " where Status='OK' " _
                                                & " and CustId=" & CmbCust.SelectedValue _
                                                & " and DOMINT='" & cboDOMINT.Text & "'")
        If Not IsNumeric(strNewSeq) Then
            MsgBox("Invalid Sequence Number")
        Else
            intNewSeq = CInt(strNewSeq)
        End If

        If intNewSeq < 1 Then
            MsgBox("Invalid Sequence Number")
            Exit Sub
        ElseIf strNewSeq > intMaxNbr Then
            MsgBox("Invalid Sequence Number")
            Exit Sub
        ElseIf strNewSeq = dgTktListing.CurrentRow.Cells("Seq").Value Then
            MsgBox("Invalid Sequence Number")
            Exit Sub
        ElseIf intNewSeq > dgTktListing.CurrentRow.Cells("Seq").Value Then
            lstQuerries.Add("Update GO_TktListing set Seq=Seq-1,LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                        & "' where Seq<=" & intNewSeq & " and Seq>" & dgTktListing.CurrentRow.Cells("Seq").Value _
                        & " and CustId=" & CmbCust.SelectedValue)
        Else
            lstQuerries.Add("Update GO_TktListing set Seq=Seq+1,LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                        & "' where Seq>=" & intNewSeq & " and Seq<>" & dgTktListing.CurrentRow.Cells("Seq").Value _
                        & " and CustId=" & CmbCust.SelectedValue)
        End If

        lstQuerries.Add("Update GO_TktListing set Seq=" & intNewSeq & ",LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                        & "' where Recid=" & dgTktListing.CurrentRow.Cells("RecId").Value)

        If pobjTvcs.UpdateListQuerries(lstQuerries, False) Then
            LoadTktListingNew()
        Else
            MsgBox("Unable to update SQL")
        End If
    End Sub


    Private Sub cboDOMINT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDOMINT.SelectedIndexChanged
        LoadTktListingNew()
    End Sub


    Private Sub lbkChangeNameInReport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkChangeNameInReport.LinkClicked
        Dim strNewNameInReport As String

        strNewNameInReport = txtNameInReport.Text

        If strNewNameInReport = "" Then
            Exit Sub
        ElseIf pobjTvcs.ExecuteNonQuerry("Update GO_TktListing set NameInReport=N'" & strNewNameInReport _
                                      & "',LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                                    & "' where Recid=" & dgTktListing.CurrentRow.Cells("RecId").Value) Then
            LoadTktListingNew()
        Else
            MsgBox("Unable to update SQL")
        End If

    End Sub

    Private Sub dgTktListing_SelectionChanged(sender As Object, e As EventArgs) Handles dgTktListing.SelectionChanged

        If dgTktListing.CurrentRow Is Nothing Then
            Exit Sub


        End If
        txtDescription.Text = dgTktListing.CurrentRow.Cells("Description").Value
        Dim blnEnable As Boolean = dgTktListing.CurrentRow.Cells("Selected").Value

        lbkChangeSeq.Enabled = blnEnable
        lbkSelect.Enabled = Not blnEnable
        lbkUnselect.Enabled = blnEnable
        lbkChangeNameInReport.Enabled = blnEnable
        lbkInternalUse.Enabled = blnEnable

        If dgTktListing.CurrentRow.Cells("InternalUse").Value Then
            lbkInternalUse.Text = "UnsetInternalUse"
        Else
            lbkInternalUse.Text = "SetInternalUse"
        End If
        txtNameInReport.Text = dgTktListing.CurrentRow.Cells("NameInReport").Value
    End Sub

    Private Sub lbkInternalUse_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkInternalUse.LinkClicked
        Dim blnSetInternalUse As Boolean = (lbkInternalUse.Text = "SetInternalUse")

        If pobjTvcs.ExecuteNonQuerry("Update GO_TktListing set InternalUse='" & blnSetInternalUse _
                                  & "',LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                                & "' where Recid=" & dgTktListing.CurrentRow.Cells("RecId").Value) Then
            LoadTktListingNew()
        Else
            MsgBox("Unable to update SQL")
        End If

    End Sub

    Private Sub lbkSelectDefaultColumns_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectDefaultColumns.LinkClicked
        Dim strGetSeq As String = "(select ISNULL(MAX(seq),0)+1 from GO_TktListing where Status='OK' and CustID=" _
                                & CmbCust.SelectedValue & " and DOMINT='" & cboDOMINT.Text & "')"
        Dim lstQuerries As New List(Of String)
        Dim lstDefaultColumns As List(Of String) = pobjTvcs.GetUnselectedTktListingDefaultColumns(CmbCust.SelectedValue, cboDOMINT.Text)
        Dim i As Integer

        For i = 0 To dgTktListing.RowCount - 1
            With dgTktListing.Rows(i)
                If Not .Cells("Selected").Value AndAlso lstDefaultColumns.Contains(.Cells("DataCode").Value) Then
                    lstQuerries.Add("insert into GO_TktListing (ReportType,CustId,DOMINT,Seq,DataCode,Description,NameInReport" _
                            & ",DataGroup,SqlQuerry,PreCalculated,InternalUse,FstUser) values ('" _
                            & cboReportType.Text _
                            & "'," & CmbCust.SelectedValue & ",'" & cboDOMINT.Text & "'," & strGetSeq _
                            & ",'" & .Cells("DataCode").Value & "',N'" & .Cells("Description").Value _
                            & "','" & .Cells("NameInReport").Value & "','" & .Cells("DataGroup").Value _
                            & "','" & Replace(.Cells("SqlQuerry").Value, "'", "''") _
                            & "','" & .Cells("PreCalculated").Value _
                            & "','" & .Cells("InternalUse").Value & "','" & myStaff.SICode & "')")
                End If
            End With
        Next

        If pobjTvcs.UpdateListQuerries(lstQuerries, False) Then
            LoadTktListingNew()
        Else
            MsgBox("Unable to update SQL")
        End If
    End Sub

    Private Sub cboReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboReportType.SelectedIndexChanged
        LoadTktListingNew()
    End Sub

    Private Sub lbkClone2OtherCust_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone2OtherCust.LinkClicked
        Dim tblCust As DataTable = pobjTvcs.GetDataTable("Select distinct CustId, CustShortName" _
                        & " from GO_CompanyInfo1 where Status='OK'" _
                        & " And RecId Not in (Select distinct CustId from GO_TktListing where Status='ok')" _
                        & " order by CustShortName")
        Dim frmNewCust As New frmShowTableContent(tblCust, "Select new Customer", "CustShortName")
        If frmNewCust.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Dim strQuerry As String = "insert GO_TktListing (ReportType, CustID, DomInt, Seq, DataCode, Description, NameInReport, DataGroup, SqlQuerry, InternalUse, DefaultUse, PreCalculated, FstUser, Status)" _
                & "select ReportType," & frmNewCust.SelectedRow.Cells("CustId").Value _
                & ", DomInt, Seq, DataCode, Description, NameInReport, DataGroup, SqlQuerry, InternalUse" _
                & ", DefaultUse, PreCalculated, FstUser, Status" _
                & " from GO_TktListing where status='ok' and CustId=" & CmbCust.SelectedValue

            If pobjTvcs.ExecuteNonQuerry(strQuerry) Then
                MsgBox("Ticket listing for " & frmNewCust.SelectedRow.Cells("CustShortName").Value & " is created!")
            Else
                MsgBox("Ticket listing for " & frmNewCust.SelectedRow.Cells("DIS").Value & " can NOT be created!")
            End If
        End If
    End Sub


End Class