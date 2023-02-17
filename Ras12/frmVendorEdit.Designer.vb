<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorEdit
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtShortName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboFOP1 = New System.Windows.Forms.ComboBox()
        Me.cboCAT = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboFOP = New System.Windows.Forms.ComboBox()
        Me.txtAccountNumber = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboCur = New System.Windows.Forms.ComboBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtSwift = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboBankInVietnam = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cboHoaDon = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboHD_TachGop = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTaxCode = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cboCountry = New System.Windows.Forms.ComboBox()
        Me.txtBankAddress = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtAccountName
        '
        Me.txtAccountName.Location = New System.Drawing.Point(88, 26)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.Size = New System.Drawing.Size(490, 20)
        Me.txtAccountName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "AccountName"
        '
        'txtShortName
        '
        Me.txtShortName.Location = New System.Drawing.Point(72, 2)
        Me.txtShortName.Name = "txtShortName"
        Me.txtShortName.Size = New System.Drawing.Size(193, 20)
        Me.txtShortName.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(0, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "ShortName"
        '
        'cboFOP1
        '
        Me.cboFOP1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFOP1.FormattingEnabled = True
        Me.cboFOP1.Items.AddRange(New Object() {"BTF", "CSH"})
        Me.cboFOP1.Location = New System.Drawing.Point(353, 50)
        Me.cboFOP1.Name = "cboFOP1"
        Me.cboFOP1.Size = New System.Drawing.Size(56, 21)
        Me.cboFOP1.TabIndex = 4
        '
        'cboCAT
        '
        Me.cboCAT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCAT.FormattingEnabled = True
        Me.cboCAT.Items.AddRange(New Object() {"AR - Air", "NH - Restaurant", "KS - Hotel", "XE - Coach", "BO - Boat", "LD - Land", "OS - OtherTour Service", "TO - TO/TA", "OT - Other", "US - Utility Supplier", "AL - Airlines", "AP - Airport", "YT - So YT", "GD - Guide", "TV - TransViet", "KB - KickBack", "CG - Cargo"})
        Me.cboCAT.Location = New System.Drawing.Point(72, 50)
        Me.cboCAT.Name = "cboCAT"
        Me.cboCAT.Size = New System.Drawing.Size(120, 21)
        Me.cboCAT.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(198, 58)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(28, 13)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "FOP"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(6, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 13)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Cat"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(318, 58)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 13)
        Me.Label10.TabIndex = 32
        Me.Label10.Text = "FOP1"
        '
        'cboFOP
        '
        Me.cboFOP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFOP.FormattingEnabled = True
        Me.cboFOP.Items.AddRange(New Object() {"PSP", "PPD"})
        Me.cboFOP.Location = New System.Drawing.Point(257, 50)
        Me.cboFOP.Name = "cboFOP"
        Me.cboFOP.Size = New System.Drawing.Size(55, 21)
        Me.cboFOP.TabIndex = 3
        '
        'txtAccountNumber
        '
        Me.txtAccountNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAccountNumber.Location = New System.Drawing.Point(231, 77)
        Me.txtAccountNumber.Name = "txtAccountNumber"
        Me.txtAccountNumber.Size = New System.Drawing.Size(130, 20)
        Me.txtAccountNumber.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(149, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 13)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "AccNo."
        '
        'cboCur
        '
        Me.cboCur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCur.FormattingEnabled = True
        Me.cboCur.Location = New System.Drawing.Point(72, 76)
        Me.cboCur.Name = "cboCur"
        Me.cboCur.Size = New System.Drawing.Size(55, 21)
        Me.cboCur.TabIndex = 6
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(72, 103)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(506, 44)
        Me.txtAddress.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "AccAddr"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 84)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 13)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "Cur"
        '
        'txtBankName
        '
        Me.txtBankName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankName.Location = New System.Drawing.Point(72, 150)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(506, 20)
        Me.txtBankName.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 153)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 13)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "BankName"
        '
        'TxtSwift
        '
        Me.TxtSwift.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSwift.Location = New System.Drawing.Point(447, 77)
        Me.TxtSwift.Name = "TxtSwift"
        Me.TxtSwift.Size = New System.Drawing.Size(56, 20)
        Me.TxtSwift.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(381, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "SWIFT"
        '
        'cboBankInVietnam
        '
        Me.cboBankInVietnam.FormattingEnabled = True
        Me.cboBankInVietnam.Items.AddRange(New Object() {"Y", "N"})
        Me.cboBankInVietnam.Location = New System.Drawing.Point(72, 201)
        Me.cboBankInVietnam.Name = "cboBankInVietnam"
        Me.cboBankInVietnam.Size = New System.Drawing.Size(55, 21)
        Me.cboBankInVietnam.TabIndex = 11
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 204)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(54, 26)
        Me.Label14.TabIndex = 47
        Me.Label14.Text = "Bank" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "InVietnam"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(318, 209)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(43, 13)
        Me.Label15.TabIndex = 52
        Me.Label15.Text = "Country"
        '
        'cboHoaDon
        '
        Me.cboHoaDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHoaDon.FormattingEnabled = True
        Me.cboHoaDon.Items.AddRange(New Object() {"VAT", "B.H", "N/A"})
        Me.cboHoaDon.Location = New System.Drawing.Point(72, 228)
        Me.cboHoaDon.Name = "cboHoaDon"
        Me.cboHoaDon.Size = New System.Drawing.Size(55, 21)
        Me.cboHoaDon.TabIndex = 14
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 236)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(47, 13)
        Me.Label12.TabIndex = 54
        Me.Label12.Text = "HoaDon"
        '
        'cboHD_TachGop
        '
        Me.cboHD_TachGop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHD_TachGop.FormattingEnabled = True
        Me.cboHD_TachGop.Items.AddRange(New Object() {"TONG", "CHITIET"})
        Me.cboHD_TachGop.Location = New System.Drawing.Point(321, 228)
        Me.cboHD_TachGop.Name = "cboHD_TachGop"
        Me.cboHD_TachGop.Size = New System.Drawing.Size(139, 21)
        Me.cboHD_TachGop.TabIndex = 15
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(240, 236)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 13)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "Hd_TachGop"
        '
        'txtTaxCode
        '
        Me.txtTaxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTaxCode.Location = New System.Drawing.Point(471, 50)
        Me.txtTaxCode.Name = "txtTaxCode"
        Me.txtTaxCode.Size = New System.Drawing.Size(107, 20)
        Me.txtTaxCode.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(415, 53)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(50, 13)
        Me.Label16.TabIndex = 58
        Me.Label16.Text = "TaxCode"
        '
        'txtPhone
        '
        Me.txtPhone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPhone.Location = New System.Drawing.Point(72, 252)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(176, 20)
        Me.txtPhone.TabIndex = 16
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 255)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(38, 13)
        Me.Label17.TabIndex = 60
        Me.Label17.Text = "Phone"
        '
        'txtEmail
        '
        Me.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEmail.Location = New System.Drawing.Point(320, 256)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(258, 20)
        Me.txtEmail.TabIndex = 17
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(254, 259)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(32, 13)
        Me.Label18.TabIndex = 62
        Me.Label18.Text = "Email"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(178, 291)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 18
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(317, 291)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 19
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'txtRecId
        '
        Me.txtRecId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(471, 2)
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(56, 20)
        Me.txtRecId.TabIndex = 65
        Me.txtRecId.Text = "0"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(405, 5)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(36, 13)
        Me.Label19.TabIndex = 66
        Me.Label19.Text = "RecId"
        '
        'cboCountry
        '
        Me.cboCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCountry.FormattingEnabled = True
        Me.cboCountry.Items.AddRange(New Object() {"TONG", "CHITIET"})
        Me.cboCountry.Location = New System.Drawing.Point(364, 201)
        Me.cboCountry.Name = "cboCountry"
        Me.cboCountry.Size = New System.Drawing.Size(214, 21)
        Me.cboCountry.TabIndex = 67
        '
        'txtBankAddress
        '
        Me.txtBankAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankAddress.Location = New System.Drawing.Point(72, 176)
        Me.txtBankAddress.Name = "txtBankAddress"
        Me.txtBankAddress.Size = New System.Drawing.Size(506, 20)
        Me.txtBankAddress.TabIndex = 68
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 179)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 13)
        Me.Label11.TabIndex = 69
        Me.Label11.Text = "BankAddress"
        '
        'frmVendorEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 361)
        Me.Controls.Add(Me.txtBankAddress)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cboCountry)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtTaxCode)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cboHD_TachGop)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.cboHoaDon)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cboBankInVietnam)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.TxtSwift)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtAccountNumber)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboCur)
        Me.Controls.Add(Me.cboFOP1)
        Me.Controls.Add(Me.cboCAT)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cboFOP)
        Me.Controls.Add(Me.txtAccountName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtShortName)
        Me.Controls.Add(Me.Label4)
        Me.Name = "frmVendorEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VendorEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtAccountName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtShortName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cboFOP1 As ComboBox
    Friend WithEvents cboCAT As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents cboFOP As ComboBox
    Friend WithEvents txtAccountNumber As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cboCur As ComboBox
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtBankName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtSwift As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cboBankInVietnam As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cboHoaDon As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cboHD_TachGop As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtTaxCode As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents cboCountry As ComboBox
    Friend WithEvents txtBankAddress As TextBox
    Friend WithEvents Label11 As Label
End Class
