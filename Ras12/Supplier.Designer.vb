<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Supplier
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.LblSearch = New System.Windows.Forms.LinkLabel()
        Me.TxtAccountNo = New System.Windows.Forms.TextBox()
        Me.GridSupplier = New System.Windows.Forms.DataGridView()
        Me.OptDontKnowAccountNo = New System.Windows.Forms.RadioButton()
        Me.OptGotAccountNo = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LblAdd = New System.Windows.Forms.LinkLabel()
        Me.CmbCity = New System.Windows.Forms.ComboBox()
        Me.CmbCountry = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.OptCreatedNewOfSelectedVendor = New System.Windows.Forms.RadioButton()
        Me.OptCreateNew = New System.Windows.Forms.RadioButton()
        Me.OptNewSearch = New System.Windows.Forms.RadioButton()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCityCode = New System.Windows.Forms.TextBox()
        Me.CmbNewCountry = New System.Windows.Forms.ComboBox()
        Me.LblSave = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GridSupplier_Edit = New System.Windows.Forms.DataGridView()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtNewName = New System.Windows.Forms.TextBox()
        Me.txtNewAddress = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtNewPhone = New System.Windows.Forms.TextBox()
        Me.txtNewMail = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.chkLast5CreatedByMyself = New System.Windows.Forms.CheckBox()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        CType(Me.GridSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.GridSupplier_Edit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblSearch
        '
        Me.LblSearch.AutoSize = True
        Me.LblSearch.Location = New System.Drawing.Point(465, 8)
        Me.LblSearch.Name = "LblSearch"
        Me.LblSearch.Size = New System.Drawing.Size(41, 13)
        Me.LblSearch.TabIndex = 0
        Me.LblSearch.TabStop = True
        Me.LblSearch.Text = "Search"
        '
        'TxtAccountNo
        '
        Me.TxtAccountNo.Location = New System.Drawing.Point(253, 5)
        Me.TxtAccountNo.Name = "TxtAccountNo"
        Me.TxtAccountNo.Size = New System.Drawing.Size(206, 20)
        Me.TxtAccountNo.TabIndex = 2
        '
        'GridSupplier
        '
        Me.GridSupplier.AllowUserToAddRows = False
        Me.GridSupplier.AllowUserToDeleteRows = False
        Me.GridSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridSupplier.Location = New System.Drawing.Point(4, 31)
        Me.GridSupplier.Name = "GridSupplier"
        Me.GridSupplier.RowHeadersVisible = False
        Me.GridSupplier.Size = New System.Drawing.Size(764, 299)
        Me.GridSupplier.TabIndex = 3
        '
        'OptDontKnowAccountNo
        '
        Me.OptDontKnowAccountNo.AutoSize = True
        Me.OptDontKnowAccountNo.Location = New System.Drawing.Point(7, 6)
        Me.OptDontKnowAccountNo.Name = "OptDontKnowAccountNo"
        Me.OptDontKnowAccountNo.Size = New System.Drawing.Size(135, 17)
        Me.OptDontKnowAccountNo.TabIndex = 4
        Me.OptDontKnowAccountNo.Text = "Dont Know AccountNo"
        Me.OptDontKnowAccountNo.UseVisualStyleBackColor = True
        '
        'OptGotAccountNo
        '
        Me.OptGotAccountNo.AutoSize = True
        Me.OptGotAccountNo.Checked = True
        Me.OptGotAccountNo.Location = New System.Drawing.Point(148, 6)
        Me.OptGotAccountNo.Name = "OptGotAccountNo"
        Me.OptGotAccountNo.Size = New System.Drawing.Size(99, 17)
        Me.OptGotAccountNo.TabIndex = 4
        Me.OptGotAccountNo.TabStop = True
        Me.OptGotAccountNo.Text = "Got AccountNo"
        Me.OptGotAccountNo.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LblAdd)
        Me.GroupBox1.Controls.Add(Me.CmbCity)
        Me.GroupBox1.Controls.Add(Me.CmbCountry)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.txtPhone)
        Me.GroupBox1.Controls.Add(Me.TxtAddress)
        Me.GroupBox1.Controls.Add(Me.TxtName)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(232, 336)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(535, 123)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Add New Supplier"
        Me.GroupBox1.Visible = False
        '
        'LblAdd
        '
        Me.LblAdd.AutoSize = True
        Me.LblAdd.Location = New System.Drawing.Point(504, 107)
        Me.LblAdd.Name = "LblAdd"
        Me.LblAdd.Size = New System.Drawing.Size(26, 13)
        Me.LblAdd.TabIndex = 3
        Me.LblAdd.TabStop = True
        Me.LblAdd.Text = "Add"
        '
        'CmbCity
        '
        Me.CmbCity.FormattingEnabled = True
        Me.CmbCity.Items.AddRange(New Object() {"An Giang", "Bac Giang", "Bac Kan", "Bac Lieu", "Bac Ninh", "Ba Ria Vung Tau", "Ben Tre", "Binh Dinh", "Binh Duong", "Binh Phuoc", "Binh Thuan", "Ca Mau", "Can Tho", "Cao Bang", "Da Nang", "Dak Lack", "Dak Nong", "Dien Bien", "Dong Nai", "Dong Thap", "Gia Lai", "Ha Giang", "Ha Nam", "Ha Noi", "Ha Tay", "Ha Tinh", "Hai Duong", "Hai Phong", "Hau Giang", "HCMC", "Hoa Binh", "Hung Yen", "Khanh Hoa", "Kien Giang", "Kon Tum", "Lai Châu", "Lam Dong", "Lang Son", "Lao Cai", "Long An", "Nam Dinh", "Nghe An", "Ninh Binh", "Ninh Thuan", "Phu Tho", "Phu Yen", "Quang Binh", "Quang Nam", "Quang Ngai", "Quang Ninh", "Quang Tri", "Soc Trang", "Son La", "Tay Ninh", "Thai Binh", "Thai Nguyen", "Thanh Hoa", "Thua Thien - Hue", "Tien Giang", "Tra Vinh", "Tuyen Quang", "Vinh Long", "Vinh Phuc", "Yen Bai"})
        Me.CmbCity.Location = New System.Drawing.Point(399, 16)
        Me.CmbCity.Name = "CmbCity"
        Me.CmbCity.Size = New System.Drawing.Size(133, 21)
        Me.CmbCity.TabIndex = 2
        '
        'CmbCountry
        '
        Me.CmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCountry.FormattingEnabled = True
        Me.CmbCountry.Location = New System.Drawing.Point(245, 16)
        Me.CmbCountry.Name = "CmbCountry"
        Me.CmbCountry.Size = New System.Drawing.Size(150, 21)
        Me.CmbCountry.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 66)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Phone"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Address"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Name"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(245, 63)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(284, 20)
        Me.txtEmail.TabIndex = 0
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(59, 63)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(153, 20)
        Me.txtPhone.TabIndex = 0
        '
        'TxtAddress
        '
        Me.TxtAddress.Location = New System.Drawing.Point(59, 41)
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(473, 20)
        Me.TxtAddress.TabIndex = 0
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(59, 16)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(184, 20)
        Me.TxtName.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(214, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "eMail"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.OptCreatedNewOfSelectedVendor)
        Me.GroupBox2.Controls.Add(Me.OptCreateNew)
        Me.GroupBox2.Controls.Add(Me.OptNewSearch)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 336)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(223, 90)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "What is Next"
        Me.GroupBox2.Visible = False
        '
        'OptCreatedNewOfSelectedVendor
        '
        Me.OptCreatedNewOfSelectedVendor.AutoSize = True
        Me.OptCreatedNewOfSelectedVendor.Location = New System.Drawing.Point(6, 62)
        Me.OptCreatedNewOfSelectedVendor.Name = "OptCreatedNewOfSelectedVendor"
        Me.OptCreatedNewOfSelectedVendor.Size = New System.Drawing.Size(216, 17)
        Me.OptCreatedNewOfSelectedVendor.TabIndex = 0
        Me.OptCreatedNewOfSelectedVendor.TabStop = True
        Me.OptCreatedNewOfSelectedVendor.Text = "Create New Supplier of Selected Vendor"
        Me.OptCreatedNewOfSelectedVendor.UseVisualStyleBackColor = True
        '
        'OptCreateNew
        '
        Me.OptCreateNew.AutoSize = True
        Me.OptCreateNew.Location = New System.Drawing.Point(6, 40)
        Me.OptCreateNew.Name = "OptCreateNew"
        Me.OptCreateNew.Size = New System.Drawing.Size(122, 17)
        Me.OptCreateNew.TabIndex = 0
        Me.OptCreateNew.TabStop = True
        Me.OptCreateNew.Text = "Create New Supplier"
        Me.OptCreateNew.UseVisualStyleBackColor = True
        '
        'OptNewSearch
        '
        Me.OptNewSearch.AutoSize = True
        Me.OptNewSearch.Location = New System.Drawing.Point(6, 19)
        Me.OptNewSearch.Name = "OptNewSearch"
        Me.OptNewSearch.Size = New System.Drawing.Size(102, 17)
        Me.OptNewSearch.TabIndex = 0
        Me.OptNewSearch.TabStop = True
        Me.OptNewSearch.Text = "Try New Search"
        Me.OptNewSearch.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(778, 491)
        Me.TabControl1.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.OptGotAccountNo)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.LblSearch)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.TxtAccountNo)
        Me.TabPage1.Controls.Add(Me.GridSupplier)
        Me.TabPage1.Controls.Add(Me.OptDontKnowAccountNo)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(770, 465)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "New Supplier"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lbkSearch)
        Me.TabPage2.Controls.Add(Me.chkLast5CreatedByMyself)
        Me.TabPage2.Controls.Add(Me.txtFullName)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.txtCityCode)
        Me.TabPage2.Controls.Add(Me.CmbNewCountry)
        Me.TabPage2.Controls.Add(Me.LblSave)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.GridSupplier_Edit)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.txtNewName)
        Me.TabPage2.Controls.Add(Me.txtNewAddress)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.TxtNewPhone)
        Me.TabPage2.Controls.Add(Me.txtNewMail)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(770, 465)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Edit"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(273, 442)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "CityCode"
        '
        'txtCityCode
        '
        Me.txtCityCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCityCode.Location = New System.Drawing.Point(328, 440)
        Me.txtCityCode.Name = "txtCityCode"
        Me.txtCityCode.Size = New System.Drawing.Size(44, 20)
        Me.txtCityCode.TabIndex = 5
        '
        'CmbNewCountry
        '
        Me.CmbNewCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbNewCountry.FormattingEnabled = True
        Me.CmbNewCountry.Location = New System.Drawing.Point(52, 439)
        Me.CmbNewCountry.Name = "CmbNewCountry"
        Me.CmbNewCountry.Size = New System.Drawing.Size(215, 21)
        Me.CmbNewCountry.TabIndex = 4
        '
        'LblSave
        '
        Me.LblSave.AutoSize = True
        Me.LblSave.Location = New System.Drawing.Point(698, 442)
        Me.LblSave.Name = "LblSave"
        Me.LblSave.Size = New System.Drawing.Size(69, 13)
        Me.LblSave.TabIndex = 3
        Me.LblSave.TabStop = True
        Me.LblSave.Text = "SaveChange"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 442)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Country"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(500, 391)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "eMail"
        '
        'GridSupplier_Edit
        '
        Me.GridSupplier_Edit.AllowUserToAddRows = False
        Me.GridSupplier_Edit.AllowUserToDeleteRows = False
        Me.GridSupplier_Edit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridSupplier_Edit.Location = New System.Drawing.Point(3, 38)
        Me.GridSupplier_Edit.Name = "GridSupplier_Edit"
        Me.GridSupplier_Edit.RowHeadersVisible = False
        Me.GridSupplier_Edit.Size = New System.Drawing.Size(764, 344)
        Me.GridSupplier_Edit.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(284, 391)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Phone"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(3, 391)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(35, 13)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Name"
        '
        'txtNewName
        '
        Me.txtNewName.Location = New System.Drawing.Point(52, 388)
        Me.txtNewName.Name = "txtNewName"
        Me.txtNewName.Size = New System.Drawing.Size(226, 20)
        Me.txtNewName.TabIndex = 0
        '
        'txtNewAddress
        '
        Me.txtNewAddress.Location = New System.Drawing.Point(52, 413)
        Me.txtNewAddress.Name = "txtNewAddress"
        Me.txtNewAddress.Size = New System.Drawing.Size(710, 20)
        Me.txtNewAddress.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 416)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Address"
        '
        'TxtNewPhone
        '
        Me.TxtNewPhone.Location = New System.Drawing.Point(328, 388)
        Me.TxtNewPhone.Name = "TxtNewPhone"
        Me.TxtNewPhone.Size = New System.Drawing.Size(170, 20)
        Me.TxtNewPhone.TabIndex = 0
        '
        'txtNewMail
        '
        Me.txtNewMail.Location = New System.Drawing.Point(542, 388)
        Me.txtNewMail.Name = "txtNewMail"
        Me.txtNewMail.Size = New System.Drawing.Size(220, 20)
        Me.txtNewMail.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "SupplierName"
        '
        'txtFullName
        '
        Me.txtFullName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFullName.Location = New System.Drawing.Point(86, 12)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(166, 20)
        Me.txtFullName.TabIndex = 8
        '
        'chkLast5CreatedByMyself
        '
        Me.chkLast5CreatedByMyself.AutoSize = True
        Me.chkLast5CreatedByMyself.Location = New System.Drawing.Point(258, 14)
        Me.chkLast5CreatedByMyself.Name = "chkLast5CreatedByMyself"
        Me.chkLast5CreatedByMyself.Size = New System.Drawing.Size(131, 17)
        Me.chkLast5CreatedByMyself.TabIndex = 9
        Me.chkLast5CreatedByMyself.Text = "Last5CreatedByMyself"
        Me.chkLast5CreatedByMyself.UseVisualStyleBackColor = True
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(441, 15)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 10
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'Supplier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 496)
        Me.Controls.Add(Me.TabControl1)
        Me.Location = New System.Drawing.Point(0, 56)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Supplier"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "TransViet Travel :: RAS12 :. Update Supplier"
        CType(Me.GridSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.GridSupplier_Edit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblSearch As System.Windows.Forms.LinkLabel
    Friend WithEvents TxtAccountNo As System.Windows.Forms.TextBox
    Friend WithEvents GridSupplier As System.Windows.Forms.DataGridView
    Friend WithEvents OptDontKnowAccountNo As System.Windows.Forms.RadioButton
    Friend WithEvents OptGotAccountNo As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents LblAdd As System.Windows.Forms.LinkLabel
    Friend WithEvents CmbCity As System.Windows.Forms.ComboBox
    Friend WithEvents CmbCountry As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents TxtAddress As System.Windows.Forms.TextBox
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents OptCreatedNewOfSelectedVendor As System.Windows.Forms.RadioButton
    Friend WithEvents OptCreateNew As System.Windows.Forms.RadioButton
    Friend WithEvents OptNewSearch As System.Windows.Forms.RadioButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents LblSave As System.Windows.Forms.LinkLabel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GridSupplier_Edit As System.Windows.Forms.DataGridView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtNewName As System.Windows.Forms.TextBox
    Friend WithEvents txtNewAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtNewPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtNewMail As System.Windows.Forms.TextBox
    Friend WithEvents CmbNewCountry As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtCityCode As TextBox
    Friend WithEvents chkLast5CreatedByMyself As CheckBox
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents lbkSearch As LinkLabel
End Class
