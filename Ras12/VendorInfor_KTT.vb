Public Class VendorInfor_KTT
    Private WhoIs As String
    Private Cmd As SqlClient.SqlCommand = Conn.CreateCommand
    Private mblnFirstLoadCompleted As Boolean
    Private mblnSupplierLoaded As Boolean
    Sub New(pWho As String)
        WhoIs = pWho
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub VendorInfor_KTT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CmbCAT.Text = Me.CmbCAT.Items(0).ToString
        If WhoIs = "KTT" Then
            Me.LblChangeFOP.Enabled = False
            Me.LblChangeCat.Enabled = False
            Me.LblChangeName.Enabled = False
            Me.LblDeleteCompany.Enabled = False
            Me.Panel1.Enabled = False
            Me.CmbCAT.Text = "NH"
            Me.TabControl1.SelectTab("TabPage2")
        Else
            LoadCmb_VAL(CmbCountry, "select Country as VAL, CountryName as DIS from lib.dbo.Country order by DIS", Conn_Web)

            Me.CmbNewCat.Text = Me.CmbNewCat.Items(0).ToString
            Me.CmbFOPedit.Text = "PPD"
            Me.CmbFOP1New.Text = "BTF"
        End If
        mblnFirstLoadCompleted = True
        Me.CmbCountry.SelectedIndex = CmbCountry.FindStringExact("VIETNAM")
    End Sub
    Private Sub LblFilter_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblFilter.LinkClicked
        LoadGridVendor()
    End Sub

    Private Sub GridVendor_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridVendor.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        If Me.ChckCompanyOKonly.Checked Then
            Me.LblDeleteCompany.Visible = True
            Me.LblChangeCat.Visible = True
            Me.LblChangeName.Visible = True
            Me.LblUpdateHD.Visible = True
            Me.LblChangeFOP.Visible = True
        End If
        Me.TxtNewShortName.Text = Me.GridVendor.CurrentRow.Cells("ShortName").Value
    End Sub

    Private Sub LblUpdate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblUpdateHD.LinkClicked
        ChangeInfor("HoaDon", Me.CmbINV.Text)
    End Sub
    Private Sub ChangeInfor(pField As String, pVal As String)
        Cmd.CommandText = "update Vendor set " & pField & "='" & pVal & "' where recID=" & Me.GridVendor.CurrentRow.Cells("RecID").Value
        Cmd.ExecuteNonQuery()
        LoadGridVendor()
    End Sub
    Private Sub LoadGridVendor()
        Dim StrSQL As String = "select RecID, HoaDon, FOP, ShortName from Vendor where cat='" & Me.CmbCAT.Text.Substring(0, 2) & "' and status<>'XX'"
        If Me.TxtNameFilter.Text <> "" Then StrSQL = StrSQL & " and shortname like '%" & Me.TxtNameFilter.Text & "%'"
        Me.GridVendor.DataSource = GetDataTable(StrSQL & " order by ShortName")
        Me.LblUpdateHD.Visible = False
        Me.LblChangeFOP.Visible = False

        Me.GridVendor.Columns(0).Visible = False
        Me.GridVendor.Columns(1).Width = 32
        Me.GridVendor.Columns(2).Width = 32
        Me.GridVendor.Columns("ShortName").Width = 256
    End Sub

    Private Sub LblChangeFOP_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblChangeFOP.LinkClicked
        ChangeInfor("FOP", Me.CmbFOPedit.Text)
        ChangeInfor("FOP1", Me.CmbFOP1edit.Text)
    End Sub

    Private Sub LblChangeCat_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblChangeCat.LinkClicked
        Cmd.CommandText = "update Vendor set cat='" & Me.CmbNewCat.Text.Substring(0, 2) &
            "' where recid=" & Me.GridVendor.CurrentRow.Cells("RecID").Value
        Cmd.ExecuteNonQuery()
        LoadGridVendor()
    End Sub

    Private Sub LblChangeName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblChangeName.LinkClicked
        Cmd.CommandText = "update Vendor set ShortName='" & Me.TxtNewShortName.Text &
            "' where recid=" & Me.GridVendor.CurrentRow.Cells("RecID").Value
        Cmd.ExecuteNonQuery()
        Me.LblChangeName.Visible = False
    End Sub

    Private Sub LblDeleteCompany_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblDeleteCompany.LinkClicked
        Dim tmpChkRecID As Integer
        Cmd.Parameters.Clear()
        Cmd.CommandText = "select City from Vendor where RecID=@VendorID"
        Cmd.Parameters.AddWithValue("@VendorID", Me.GridVendor.CurrentRow.Cells("recid").Value)
        If cmd.ExecuteScalar <> myStaff.City Then Exit Sub

        cmd.CommandText = "select top 1 RecID from dutoan_Item where vendorID=@VendorID" &
                " and DutoanID in (select recid from dutoan_Tour where status not in ('XX','RR'))"
        tmpChkRecID = cmd.ExecuteScalar
        If tmpChkRecID > 0 Then
            MsgBox("There is Pending NonAir Booking on This Vendor", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        Cmd.CommandText = ChangeStatus_ByID("Vendor", "XX", Me.GridVendor.CurrentRow.Cells("recid").Value) &
            ";" & ChangeStatus_ByDK("UNC_Accounts", "XX", "status ='OK' and CompanyID=" & Me.GridVendor.CurrentRow.Cells("recid").Value)
        Cmd.CommandText = Cmd.CommandText & "; Update Supplier set VendorID=0 where VendorID=@VendorID"
        cmd.ExecuteNonQuery()
        LoadGridVendor()
    End Sub
    Private Function AddSupplier(intVendorId As Integer) As Boolean
        If ScalarToInt("Supplier", "RecId", "VendorId=" & intVendorId _
                       & " and Status='ok' and FullName='" & TxtSupplierName.Text & "'") > 0 Then
            MsgBox("Duplicate Supplier Name!")
            Return False
        ElseIf cboCity.Text = "" Then
            MsgBox("Missing City!")
            Return False
        ElseIf Not CheckFormatComboBox(cboCity,, 3) Then
            Return False
        End If

        If ContainSpecialChars(TxtSupplierName.Text) Then
            MsgBox("Supplier name must NOT contain special character!")
            Return False
        End If

        Cmd.CommandText = "insert Supplier (FullName, Address, Address_CityCode, Address_CountryCode" _
            & " , Tel, Email, VendorID, FstUser, City,Province)" _
            & " values (@FullName, @Address, @Address_CityCode, @Address_CountryCode" _
            & " , @Tel, @Email, @VendorID, @FstUser, @City,@Province)"
        Cmd.Parameters.Clear()
        Cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = Me.TxtSupplierName.Text
        Cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = Me.TxtAddress.Text
        Cmd.Parameters.Add("@Address_CityCode", SqlDbType.VarChar).Value = Me.cboCity.SelectedValue
        Cmd.Parameters.Add("@Address_CountryCode", SqlDbType.VarChar).Value = Me.CmbCountry.SelectedValue
        Cmd.Parameters.Add("@Province", SqlDbType.VarChar).Value = Me.CmbProvince.Text
        Cmd.Parameters.Add("@Tel", SqlDbType.VarChar).Value = Me.txtPhone.Text
        Cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Me.txtEmail.Text
        Cmd.Parameters.Add("@FstUser", SqlDbType.VarChar).Value = myStaff.SICode
        Cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = myStaff.City
        Cmd.Parameters.Add("@VendorID", SqlDbType.Int).Value = intVendorId
        Cmd.ExecuteNonQuery()
        Return True
    End Function
    Private Function AddVendor() As Integer
        Dim KQ As Integer = 0
        TxtShortName.Text = TxtShortName.Text.Trim
        If Me.TxtShortName.Text = "" Then Exit Function
        If ScalarToInt("Vendor", "RecId", "Status='ok' and ShortName='" & TxtShortName.Text & "'") > 0 Then
            MsgBox("Duplicate Vendor Name!")
            Return KQ
        End If
        Cmd.CommandText = "insert into Vendor (cat, shortname, FOP, fstuser, city, FOP1)  values ('" &
            Me.CmbCAT.Text.Substring(0, 2) & "','" & Me.TxtShortName.Text.Replace("--", "") & "','" &
            Me.CmbFOPNew.Text & "','" & myStaff.SICode & "','" & myStaff.City & "','" & Me.CmbFOP1New.Text & "') "
        Cmd.ExecuteNonQuery()
        Cmd.CommandText = "select top 1 RecID from Vendor order by recID Desc"
        KQ = cmd.ExecuteScalar
        LoadGridVendor()

        Me.TxtShortName.Text = ""
        Return KQ

    End Function
    Private Sub LblAddNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblAddNewVendor.LinkClicked
        If AddVendor() = 0 Then
            MsgBox("Add Vendor: Invalid Input:" & Err.Description, MsgBoxStyle.Critical, msgTitle)
        End If
    End Sub
    Private Sub LblCreateVendorAndSupplier_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblCreateVendorAndSupplier.LinkClicked
        Dim NewVendorID As Integer = AddVendor()
        If NewVendorID = 0 Then Exit Sub
        If AddSupplier(NewVendorID) Then
            ClearSupplierInfo()
        Else
            MsgBox("Unable to Add Supplier!")
        End If

    End Sub
    Private Sub ClearSupplierInfo()
        Me.TxtSupplierName.Text = ""
        Me.TxtAddress.Text = ""
        Me.txtPhone.Text = ""
        Me.txtEmail.Text = ""
        Me.CmbCountry.SelectedValue = "VN"
    End Sub
    Private Sub CmbCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCountry.SelectedIndexChanged
        If Not mblnFirstLoadCompleted Then
            Exit Sub
        End If
        Try
            LoadComboDisplay(cboCity, "Select CityName as Display, City as Value from CityCode" _
                                 & " where Country='" & CmbCountry.SelectedValue & "'", Conn)

            If Me.CmbCountry.SelectedValue = "VN" Then
                Me.CmbProvince.DropDownStyle = ComboBoxStyle.DropDownList
            Else
                Me.CmbProvince.DropDownStyle = ComboBoxStyle.DropDown
                Me.CmbProvince.Text = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter
        Me.TxtSupplierName.Text = Me.TxtShortName.Text
    End Sub
    Private Sub LblChangeHD_Tong_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblChangeHD_Tong.LinkClicked
        ChangeInfor("HD_TachGop", Me.CmbHD_tong.Text)
    End Sub

    Private Sub CmbCAT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCAT.SelectedIndexChanged
        LoadGridVendor()
    End Sub

    Private Sub lbkFindSimilarVendor_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFindSimilarVendor.LinkClicked
        Dim frmFind As New frmFindVendor(TxtShortName.Text)
        frmFind.ShowDialog()
    End Sub

    Private Sub lbkCreateSupplierOnly_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateSupplierOnly.LinkClicked
        If AddSupplier(0) Then
            ClearSupplierInfo()
        End If
    End Sub

    Private Sub tabEditSupplier_Enter(sender As Object, e As EventArgs) Handles tabEditSupplier.Enter
        LoadCmb_VAL(cboNewCountry, "select Country as VAL, CountryName as DIS from lib.dbo.Country order by DIS", Conn_Web)

    End Sub



    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        LoadGridSupplier_Edit()
    End Sub

    Private Sub LblSave_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblSave.LinkClicked
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        If Me.txtNewName.Text = "" Or Me.txtNewAddress.Text = "" Then Exit Sub
        If Me.txtNewMail.Text <> "" And Not Me.txtNewMail.Text.Contains("@") Then Exit Sub
        If Me.TxtNewPhone.Text <> "" And Me.TxtNewPhone.Text.Length < 8 Then Exit Sub

        cmd.CommandText = "Update Supplier set FullName=@FullName, Address=@Address, Tel=@Tel, Email=@Email" _
                & ",Address_CountryCode=@CountryCode,Address_CityCode=@CityCode where RecID=@RecID"
        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = Me.txtNewName.Text
        cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = Me.txtNewAddress.Text
        cmd.Parameters.Add("@Tel", SqlDbType.VarChar).Value = Me.TxtNewPhone.Text
        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Me.txtNewMail.Text
        cmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = Me.cboNewCountry.SelectedValue
        cmd.Parameters.Add("@CityCode", SqlDbType.VarChar).Value = cboNewCity.SelectedValue
        cmd.Parameters.Add("@RecID", SqlDbType.Int).Value = Me.GridSupplier_Edit.CurrentRow.Cells("RecID").Value
        cmd.ExecuteNonQuery()
        LoadGridSupplier_Edit()
    End Sub
    Private Sub LoadGridSupplier_Edit()
        'Me.LblSave.Visible = False
        Dim strTopRecords As String = ""
        Dim strQuerry As String
        Dim strFilter As String = " where Status<>'XX'"

        If chkLast5CreatedByMyself.Checked Then
            strTopRecords = " top 5 "
            strFilter = strFilter & "and FstUser='" & myStaff.SICode & "'"
        End If
        strQuerry = "Select" & strTopRecords & " RecID, FullName, Tel, Email, Address" _
                & ", Address_CountryCode as Country,Address_CityCode from Supplier" & strFilter
        AddLikeConditionText(strQuerry, txtFullName)
        strQuerry = strQuerry & " order by FullName"

        LoadDataGridView(GridSupplier_Edit, strQuerry, Conn)
        Me.GridSupplier_Edit.Columns("RecId").Width = 40
        Me.GridSupplier_Edit.Columns("FullName").Width = 160
        Me.GridSupplier_Edit.Columns(3).Width = 256
        Me.GridSupplier_Edit.Columns(4).Width = 256
        mblnSupplierLoaded = True
    End Sub

    Private Sub tabUpdateSupplier_Click(sender As Object, e As EventArgs) Handles tabEditSupplier.Click

    End Sub

    Private Sub GridSupplier_Edit_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridSupplier_Edit.CellContentClick

        With GridSupplier_Edit.CurrentRow
            Dim strCountry As String = ScalarToString("[LIB].[dbo].[Country]", "CountryName", "Country='" _
                                                      & .Cells("Country").Value & "'")
            Dim strCity As String = ScalarToString("CityCode", "top 1 CityName", "Airport='" _
                                                      & .Cells("Address_CityCode").Value & "'")

            txtNewName.Text = .Cells("FullName").Value
            txtNewAddress.Text = .Cells("Address").Value
            txtNewMail.Text = .Cells("Email").Value
            TxtNewPhone.Text = .Cells("Tel").Value
            cboNewCountry.SelectedIndex = cboNewCountry.FindStringExact(strCountry)
            cboNewCity.SelectedIndex = cboNewCity.FindStringExact(strCity)
        End With
    End Sub

    Private Sub cboNewCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNewCountry.SelectedIndexChanged
        If Not mblnFirstLoadCompleted Then
            Exit Sub
        End If
        Try
            LoadComboDisplay(cboNewCity, "Select CityName as Display, City as Value from CityCode" _
                                 & " where Country='" & cboNewCountry.SelectedValue & "'", Conn)

        Catch ex As Exception
        End Try
    End Sub
End Class