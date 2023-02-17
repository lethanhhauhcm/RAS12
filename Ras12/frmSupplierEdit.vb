Public Class frmSupplierEdit
    Private mblnCountryLoadCompleted As Boolean
    Public Sub New(Optional objRow As DataGridViewRow = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadCmb_VAL(cboCountry, "select Country as VAL, CountryName as DIS from lib.dbo.Country order by DIS", Conn_Web)
        lbkGetProvince.Visible = False
        lbkGetDistrict.Visible = False
        mblnCountryLoadCompleted = True
        cboCountry.SelectedIndex = cboCountry.FindStringExact("VIETNAM")
        If objRow IsNot Nothing Then
            With objRow
                Dim strCountry As String = ScalarToString("Country", "top 1 CountryName", "Country='" _
                                                          & .Cells("Address_CountryCode").Value & "'")
                Dim strCity As String = ScalarToString("CityCode", "top 1 CityName", "Airport='" _
                                                          & .Cells("Address_CityCode").Value & "'")


                txtRecId.Text = .Cells("RecId").Value
                txtVendorId.Text = .Cells("VendorId").Value
                txtCat.Text = ScalarToString("Vendor", "Cat", "RecId=" & .Cells("VendorId").Value)
                If txtVendorId.Text = 0 Then
                    lbkFindVendor.Enabled = True
                Else
                    lbkFindVendor.Enabled = False
                End If
                If txtVendorId.Text <> 0 Then
                    txtVendor.Text = ScalarToString("Vendor", "top 1 ShortName", "Status<>'XX' and RecId=" & txtVendorId.Text)
                End If
                txtFullName.Text = .Cells("FullName").Value
                txtAddress.Text = .Cells("Address").Value
                cboCountry.SelectedIndex = cboCountry.FindStringExact(strCountry)
                cboCity.SelectedIndex = cboCity.FindStringExact(strCity)
                txtProvince.Text = .Cells("Province").Value
                txtDistrict.Text = .Cells("District").Value
                txtEmail.Text = .Cells("Email").Value
                txtPhone.Text = .Cells("Tel").Value
                txtContact.Text = .Cells("Contact").Value
                txtBill.Text = .Cells("Bill").Value
                txtPayment.Text = .Cells("Payment").Value
                txtRMK.Text = .Cells("RMK").Value
            End With

        End If
    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim strSql As String
        If Not CheckInputValues() Then Exit Sub

        'cmd.CommandText = "insert Supplier (FullName, Address, Address_CityCode, Address_CountryCode" _
        '    & " , Tel, Email, VendorID, FstUser, City,Province)"
        If txtRecId.Text = 0 Then
            strSql = "insert into Supplier (FullName, Address, Address_CityCode" _
                            & " , Address_CountryCode,Province,District,Tel, Email" _
                            & " , Contact,Bill,Payment,Rmk,RatingType" _
                            & ", VendorID, FstUser, City)" _
                            & " values (N'" & txtFullName.Text & "',N'" & txtAddress.Text _
                            & "','" & cboCity.SelectedValue & "','" & cboCountry.SelectedValue _
                            & "',N'" & txtProvince.Text & "',N'" & txtDistrict.Text _
                            & "','" & txtPhone.Text & "','" & txtEmail.Text _
                            & "',N'" & txtContact.Text & "',N'" & txtBill.Text _
                            & "',N'" & txtPayment.Text & "',N'" & txtRMK.Text _
                            & "',N'" & txtRatingType.Text _
                            & "'," & txtVendorId.Text & ",'" & myStaff.SICode _
                            & "','" & myStaff.City & "')"
        Else
            strSql = "update Supplier Set FullName=N'" & txtFullName.Text _
                            & "',Address=N'" & txtAddress.Text _
                            & "', Address_CityCode='" & cboCity.SelectedValue _
                            & "', Address_CountryCode='" & cboCountry.SelectedValue _
                            & "', Province=N'" & txtProvince.Text & "',District=N'" & txtDistrict.Text _
                            & "', Tel='" & txtPhone.Text _
                            & "', Email='" & txtEmail.Text _
                            & "', Contact=N'" & txtContact.Text _
                            & "', Bill=N'" & txtBill.Text _
                            & "', Payment=N'" & txtPayment.Text _
                            & "', RMK=N'" & txtRMK.Text _
                            & "',RatingType=N'" & txtRatingType.Text _
                            & "', VendorId=" & txtVendorId.Text _
                            & ",LstUpdate=getdate(), LstUser='" & myStaff.SICode _
                            & "' where RecId=" & txtRecId.Text
        End If

        If ExecuteNonQuerry(strSql, Conn) Then
            Me.DialogResult = DialogResult.OK
        Else
            MsgBox("Unable to Add/Edit Supplier!")
            Exit Sub
        End If
    End Sub

    Private Sub lbkFindVendor_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFindVendor.LinkClicked
        Dim frmSearch As New frmVendorList
        With frmSearch
            .lbkNew.Visible = False
            .lbkEdit.Visible = False
            .lbkExpire.Visible = False
            .lbkAddSupplier.Visible = False
            .lbkSelect.Visible = True
            If .ShowDialog = DialogResult.OK Then
                txtVendorId.Text = frmSearch.dgrVendors.CurrentRow.Cells("RecId").Value
                txtVendor.Text = frmSearch.dgrVendors.CurrentRow.Cells("ShortName").Value
                txtCat.Text = frmSearch.dgrVendors.CurrentRow.Cells("Cat").Value
            End If
        End With
    End Sub
    Private Function CheckInputValues() As Boolean
        If txtCat.Text = "GD" Then
            If txtPhone.Text = "" Then
                MsgBox("Phone Required for Guide!")
                Return False
            ElseIf ScalarToInt("Supplier", "RecId", "RecId<>" & txtRecId.Text _
                   & " and Status='ok' and Tel='" & txtPhone.Text & "'") > 0 Then
                MsgBox("Duplicate Supplier with the same Tel!")
                Return False
            End If
        End If

        If ScalarToInt("Supplier", "RecId", "RecId<>" & txtRecId.Text & " and VendorId=" & txtVendorId.Text _
                       & " and Status='ok' and FullName='" & txtFullName.Text & "'") > 0 Then
            MsgBox("Duplicate Supplier Name!")
            Return False

        ElseIf cboCity.Text = "" Then
            MsgBox("Missing City!")
            Return False
        ElseIf Not CheckFormatComboBox(cboCity,, 3) Then
            Return False
        End If

        If ContainSpecialChars(txtFullName.Text) Then
            MsgBox("Supplier name must NOT contain special character!")
            Return False
        End If
        If cboCountry.Text = "VIETNAM" Then
            If txtProvince.Text = "" Then
                MsgBox("You must select Province!")
                Return False
            End If
            If txtDistrict.Text = "" Then
                MsgBox("You must select District!")
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub cboCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCountry.SelectedIndexChanged
        If Not mblnCountryLoadCompleted Then
            Exit Sub
        End If
        Try
            LoadComboDisplay(cboCity, "Select distinct CityName as Display, City as Value from CityCode" _
                                 & " where Country='" & cboCountry.SelectedValue & "'", Conn)

            If cboCountry.SelectedValue = "VN" Then
                lbkGetProvince.Visible = True
                lbkGetDistrict.Visible = True
            Else
                lbkGetProvince.Visible = False
                lbkGetDistrict.Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lbkGetProvince_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkGetProvince.LinkClicked
        Dim tblProvinces As DataTable = GetDataTable("select strVal1 as Province from lib.dbo.Misc" _
                                                     & " where Status='ok' and CAT='VnProvince'" _
                                                     & " order by strVal1")
        Dim frmShow As New frmShowTableContent(tblProvinces, "Select Province", "Province")
        If frmShow.ShowDialog = DialogResult.OK Then
            txtProvince.Text = frmShow.SelectedValue
        End If
    End Sub

    Private Sub lbkGetDistrict_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkGetDistrict.LinkClicked
        Dim tblDistricts As DataTable = GetDataTable("select VAL as District from lib.dbo.Misc" _
                                                     & " where Status='ok' and CAT='VnDistrict'" _
                                                     & " and strVal1=N'" & txtProvince.Text _
                                                     & "' order by strVal1")
        Dim frmShow As New frmShowTableContent(tblDistricts, "Select district", "District")
        If frmShow.ShowDialog = DialogResult.OK Then
            txtDistrict.Text = frmShow.SelectedValue
        End If
    End Sub

    Private Sub lbkRatingType_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRatingType.LinkClicked
        Dim tblRatings As DataTable = GetDataTable("Select strVal1 as RatingType from lib.dbo.Misc" _
                                                   & " where CAT='RatingType' and Status='OK' order by strVal1")
        Dim frmShow As New frmShowTableContent(tblRatings, "Chọn phân loại", "RatingType")
        If frmShow.ShowDialog = DialogResult.OK Then
            txtRatingType.Text = frmShow.SelectedValue
        End If
    End Sub
End Class