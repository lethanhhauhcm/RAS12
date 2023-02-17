Public Class frmVendorEdit
    Public Sub New(Optional objRow As DataGridViewRow = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadCmb_VAL(cboCountry, "select Country as VAL, CountryName as DIS from lib.dbo.Country order by DIS", Conn_Web)
        LoadCombo(cboCur, "Select Val as value from Misc where cat='CURR' order by intVal desc,Val", Conn)
        cboCountry.SelectedIndex = -1
        If objRow Is Nothing Then
            cboCur.SelectedIndex = cboCur.FindStringExact("VND")
        Else
            txtShortName.Enabled = False
            Dim i As Integer
            With objRow
                txtRecId.Text = .Cells("Recid").Value
                txtShortName.Text = .Cells("Shortname").Value
                txtAccountName.Text = .Cells("AccountName").Value
                txtShortName.Text = .Cells("Shortname").Value
                For i = 0 To cboCAT.Items.Count - 1
                    If Mid(cboCAT.Items(i).ToString, 1, 2) = .Cells("Cat").Value.trim Then
                        cboCAT.SelectedItem = cboCAT.Items(i)
                        Exit For
                    End If
                Next
                cboFOP.SelectedIndex = cboFOP.FindStringExact(.Cells("FOP").Value)
                cboFOP1.SelectedIndex = cboFOP1.FindStringExact(.Cells("FOP1").Value)
                txtTaxCode.Text = .Cells("TaxCode").Value
                txtTaxCode.Text = .Cells("TaxCode").Value
                cboCur.SelectedIndex = cboCur.FindStringExact(.Cells("Cur").Value)
                txtAccountNumber.Text = .Cells("AccountNumber").Value
                TxtSwift.Text = .Cells("Swift").Value
                txtAddress.Text = .Cells("Address").Value
                txtBankName.Text = .Cells("BankName").Value
                txtBankAddress.Text = .Cells("BankAddress").Value
                cboBankInVietnam.SelectedIndex = cboBankInVietnam.FindStringExact(.Cells("BankInVietnam").Value)
                cboCountry.SelectedIndex = cboCountry.FindStringExact(.Cells("Country").Value)
                cboHoaDon.SelectedIndex = cboHoaDon.FindStringExact(.Cells("HoaDon").Value)
                cboHD_TachGop.SelectedIndex = cboHD_TachGop.FindStringExact(.Cells("HD_TachGop").Value)
                txtPhone.Text = .Cells("Phone").Value
                txtEmail.Text = .Cells("Email").Value
            End With
        End If
    End Sub

    Private Sub frmVendorEdit_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerries As New List(Of String)

        txtShortName.Text = txtShortName.Text.Trim

        If Not CheckInputValues() Then
            Exit Sub
        End If

        If txtRecId.Text = 0 Then
            lstQuerries.Add("insert into lib.dbo.Vendor (CAT, ShortName, FOP, HoaDon, HD_TachGop" _
                        & ", City, FOP1, AccountName, Cur, AccountNumber, Address, BankName" _
                        & ", BankAddress, BankInVietnam, Swift, Country, TaxCode" _
                        & ", Phone, Email, Status, FstUser) values ('" & Mid(cboCAT.Text, 1, 2) _
                        & "','" & txtShortName.Text & "','" & cboFOP.Text & "','" & cboHoaDon.Text _
                        & "','" & cboHD_TachGop.Text & "','" & myStaff.City & "','" & cboFOP1.Text _
                        & "',N'" & txtAccountName.Text & "','" & cboCur.Text & "','" & txtAccountNumber.Text _
                        & "',N'" & txtAddress.Text & "',N'" & txtBankName.Text & "',N'" & txtBankAddress.Text _
                        & "','" & cboBankInVietnam.Text & "','" & TxtSwift.Text _
                        & "','" & cboCountry.Text & "','" & txtTaxCode.Text & "','" & txtPhone.Text _
                        & "','" & txtEmail.Text & "','OK','" & myStaff.SICode & "')")

        Else
            lstQuerries.Add("insert lib.dbo.VendorHistory select *,'" & myStaff.SICode _
                            & "',getdate() from lib.dbo.Vendor where RecId=" & txtRecId.Text)
            lstQuerries.Add("update lib.dbo.Vendor  Set CAT='" & Mid(cboCAT.Text, 1, 2) _
                            & "', ShortName='" & txtShortName.Text & "', FOP='" & cboFOP.Text _
                            & "', HoaDon='" & cboHoaDon.Text & "', HD_TachGop='" _
                            & cboHD_TachGop.Text & "', City='" & myStaff.City & "', FOP1='" & cboFOP1.Text _
                            & "', AccountName=N'" & txtAccountName.Text & "', Cur='" & cboCur.Text _
                            & "', AccountNumber='" & txtAccountNumber.Text & "', Address=N'" & txtAddress.Text _
                            & "', BankName=N'" & txtBankName.Text & "', BankAddress=N'" & txtBankAddress.Text _
                            & "', BankInVietnam='" & cboBankInVietnam.Text & "', Swift='" & TxtSwift.Text _
                            & "', Country='" & cboCountry.Text & "', TaxCode='" & txtTaxCode.Text _
                            & "', Phone='" & txtPhone.Text & "', Email='" & txtEmail.Text _
                            & "', Status='OK', FstUser='" & myStaff.SICode _
                            & "' where RecId=" & txtRecId.Text)
        End If

        Dim intVendorId As Integer

        If UpdateListOfQuerries(lstQuerries, Conn_Web, True, intVendorId) Then
            If txtRecId.Text = 0 Then
                AddZeroBalance(intVendorId, txtShortName.Text, cboCur.Text, myStaff.City)
                If intVendorId <> 0 Then
                    ExecuteNonQuerry(InsertVendorId4AOP(intVendorId, myStaff.City), Conn)
                End If

            End If
            Me.DialogResult = DialogResult.OK

            'ElseIf myStaff.City <> "SGN" AndAlso UpdateListOfQuerries(lstQuerries, Conn) Then
            '    If txtRecId.Text = 0 Then
            '        If intVendorId <> 0 Then
            '            ExecuteNonQuerry(InsertVendorId4AOP(intVendorId, myStaff.City), Conn)
            '        End If
            '        AddZeroBalance(intVendorId, txtShortName.Text, cboCur.Text, myStaff.City)

            '    End If
            '    Me.DialogResult = DialogResult.OK
        Else
            MsgBox("Unable to Add/Edit Vendor!")
            Exit Sub
        End If
    End Sub
    Private Function AddZeroBalance(intVendorId As Integer, strVendorName As String, strCur As String, strCity As String) As Boolean
        Dim lstQuerries As New List(Of String)

        lstQuerries.Add("insert into LIB.DBO.VendorBalance (VendorID, Vendor, Cur,Amount, FOP, TrxDate" _
                    & ", Status, FstUser, App,City,TVC) values (" & intVendorId & ",'" & strVendorName & "','" & strCur _
                    & "',0,'COF',GETDATE(),'OK','" & myStaff.SICode _
                    & "','RAS','" & strCity & "','TVTR')")
        lstQuerries.Add("insert into LIB.DBO.VendorBalance (VendorID, Vendor, Cur,Amount, FOP, TrxDate" _
                    & ", Status, FstUser, App,City,TVC) values (" & intVendorId & ",'" & strVendorName & "','" & strCur _
                    & "',0,'COF',GETDATE(),'OK','" & myStaff.SICode _
                    & "','RAS','" & strCity & "','GDS')")
        Return UpdateListOfQuerries(lstQuerries, Conn_Web )

    End Function
    Private Function CheckInputValues() As Boolean

        Dim strCheckDup As String
        Dim strTemp As String = ""
        Dim tblDupVendors As System.Data.DataTable
        Dim intMaxLength4ShortName As Integer

        If txtRecId.Text <> 0 Then
            intMaxLength4ShortName = 50
        Else
            intMaxLength4ShortName = 31
        End If
        If Not CheckFormatTextBox(txtShortName,, 2, intMaxLength4ShortName) Then
            Return False
        End If
        'If Not CheckFormatTextBox(txtAccountName,, 2, 100) Then
        '    Return False
        'End If
        If Not CheckFormatComboBox(cboCur,, 3, 3) Then
            Return False
        End If
        'If Not CheckFormatComboBox(cboFOP,, 3, 3) Then
        '    Return False
        'End If

        'If Not CheckFormatComboBox(cboHoaDon,, 3, 3) Then
        '    Return False
        'End If
        If cboCAT.Text = "" Then
            MsgBox("You must select Category")
            Return False
        End If

        '^_^20221027 add by 7643 -b-
        If ContainSpecialAndUnicodeChars(txtShortName.Text) Then
            MsgBox("Can not use special character or unicode in ShortName!")
            Return False
        End If
        '^_^20221027 add by 7643 -e-

        strCheckDup = "Select * from Vendor where Status<>'XX'"

        strCheckDup = strCheckDup & " and RecId<>" & txtRecId.Text

        AddEqualConditionText(strTemp, txtShortName, "or")

        If cboCAT.Text <> "TV" Then
            AddEqualConditionText(strTemp, txtTaxCode, "or")
        End If

        If strTemp.Trim <> "" Then
            strCheckDup = strCheckDup & Replace(strTemp, "or", " and (",, 1) & ")"
        End If
        tblDupVendors = GetDataTable(strCheckDup, Conn)
        If tblDupVendors.Rows.Count > 0 Then
            Dim frmShow As New frmShowTableContent(tblDupVendors, "Duplicated Vendors exist!")
            frmShow.ShowDialog()
            Return False
        End If
        Return True
    End Function
End Class