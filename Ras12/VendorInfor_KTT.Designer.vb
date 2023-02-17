<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VendorInfor_KTT
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
        Me.GridVendor = New System.Windows.Forms.DataGridView()
        Me.CmbCAT = New System.Windows.Forms.ComboBox()
        Me.CmbINV = New System.Windows.Forms.ComboBox()
        Me.LblUpdateHD = New System.Windows.Forms.LinkLabel()
        Me.TxtNameFilter = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblFilter = New System.Windows.Forms.LinkLabel()
        Me.CmbFOPedit = New System.Windows.Forms.ComboBox()
        Me.LblChangeFOP = New System.Windows.Forms.LinkLabel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbkFindSimilarVendor = New System.Windows.Forms.LinkLabel()
        Me.CmbFOP1New = New System.Windows.Forms.ComboBox()
        Me.TxtShortName = New System.Windows.Forms.TextBox()
        Me.LblCreateVendorAndSupplier = New System.Windows.Forms.LinkLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbkCreateSupplierOnly = New System.Windows.Forms.LinkLabel()
        Me.cboCity = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CmbProvince = New System.Windows.Forms.ComboBox()
        Me.CmbCountry = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.TxtSupplierName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbFOPNew = New System.Windows.Forms.ComboBox()
        Me.LblAddNewVendor = New System.Windows.Forms.LinkLabel()
        Me.LblAddCompany = New System.Windows.Forms.LinkLabel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.CmbFOP1edit = New System.Windows.Forms.ComboBox()
        Me.CmbHD_tong = New System.Windows.Forms.ComboBox()
        Me.TxtNewShortName = New System.Windows.Forms.TextBox()
        Me.LblChangeHD_Tong = New System.Windows.Forms.LinkLabel()
        Me.LblChangeName = New System.Windows.Forms.LinkLabel()
        Me.CmbNewCat = New System.Windows.Forms.ComboBox()
        Me.LblDeleteCompany = New System.Windows.Forms.LinkLabel()
        Me.LblChangeCat = New System.Windows.Forms.LinkLabel()
        Me.tabEditSupplier = New System.Windows.Forms.TabPage()
        Me.LblSave = New System.Windows.Forms.LinkLabel()
        Me.cboNewCity = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.cboNewCountry = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.chkLast5CreatedByMyself = New System.Windows.Forms.CheckBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TxtNewPhone = New System.Windows.Forms.TextBox()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.txtNewMail = New System.Windows.Forms.TextBox()
        Me.GridSupplier_Edit = New System.Windows.Forms.DataGridView()
        Me.txtNewName = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtNewAddress = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.ChckCompanyOKonly = New System.Windows.Forms.CheckBox()
        CType(Me.GridVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.tabEditSupplier.SuspendLayout()
        CType(Me.GridSupplier_Edit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridVendor
        '
        Me.GridVendor.AllowUserToAddRows = False
        Me.GridVendor.AllowUserToDeleteRows = False
        Me.GridVendor.BackgroundColor = System.Drawing.Color.Pink
        Me.GridVendor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridVendor.Location = New System.Drawing.Point(5, 4)
        Me.GridVendor.Name = "GridVendor"
        Me.GridVendor.RowHeadersVisible = False
        Me.GridVendor.Size = New System.Drawing.Size(346, 469)
        Me.GridVendor.TabIndex = 0
        '
        'CmbCAT
        '
        Me.CmbCAT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCAT.FormattingEnabled = True
        Me.CmbCAT.Items.AddRange(New Object() {"AR - Air", "NH - Restaurant", "KS - Hotel", "XE - Coach", "BO - Boat", "LD - Land", "OS - OtherTour Service", "TO - TO/TA", "OT - Other", "US - Utility Supplier", "AL - Airlines", "AP - Airport", "YT - So YT", "GD - Guide"})
        Me.CmbCAT.Location = New System.Drawing.Point(32, 22)
        Me.CmbCAT.Name = "CmbCAT"
        Me.CmbCAT.Size = New System.Drawing.Size(120, 21)
        Me.CmbCAT.TabIndex = 1
        '
        'CmbINV
        '
        Me.CmbINV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbINV.FormattingEnabled = True
        Me.CmbINV.Items.AddRange(New Object() {"VAT", "B.H", "N/A"})
        Me.CmbINV.Location = New System.Drawing.Point(84, 10)
        Me.CmbINV.Name = "CmbINV"
        Me.CmbINV.Size = New System.Drawing.Size(55, 21)
        Me.CmbINV.TabIndex = 2
        '
        'LblUpdateHD
        '
        Me.LblUpdateHD.AutoSize = True
        Me.LblUpdateHD.Location = New System.Drawing.Point(6, 13)
        Me.LblUpdateHD.Name = "LblUpdateHD"
        Me.LblUpdateHD.Size = New System.Drawing.Size(72, 13)
        Me.LblUpdateHD.TabIndex = 3
        Me.LblUpdateHD.TabStop = True
        Me.LblUpdateHD.Text = "ChangeHD to"
        Me.LblUpdateHD.Visible = False
        '
        'TxtNameFilter
        '
        Me.TxtNameFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtNameFilter.Location = New System.Drawing.Point(160, 475)
        Me.TxtNameFilter.Name = "TxtNameFilter"
        Me.TxtNameFilter.Size = New System.Drawing.Size(156, 20)
        Me.TxtNameFilter.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(75, 478)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Name Contains"
        '
        'LblFilter
        '
        Me.LblFilter.AutoSize = True
        Me.LblFilter.Location = New System.Drawing.Point(322, 478)
        Me.LblFilter.Name = "LblFilter"
        Me.LblFilter.Size = New System.Drawing.Size(29, 13)
        Me.LblFilter.TabIndex = 6
        Me.LblFilter.TabStop = True
        Me.LblFilter.Text = "Filter"
        '
        'CmbFOPedit
        '
        Me.CmbFOPedit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFOPedit.FormattingEnabled = True
        Me.CmbFOPedit.Items.AddRange(New Object() {"PSP", "PPD"})
        Me.CmbFOPedit.Location = New System.Drawing.Point(84, 37)
        Me.CmbFOPedit.Name = "CmbFOPedit"
        Me.CmbFOPedit.Size = New System.Drawing.Size(55, 21)
        Me.CmbFOPedit.TabIndex = 9
        '
        'LblChangeFOP
        '
        Me.LblChangeFOP.AutoSize = True
        Me.LblChangeFOP.Location = New System.Drawing.Point(6, 40)
        Me.LblChangeFOP.Name = "LblChangeFOP"
        Me.LblChangeFOP.Size = New System.Drawing.Size(77, 13)
        Me.LblChangeFOP.TabIndex = 11
        Me.LblChangeFOP.TabStop = True
        Me.LblChangeFOP.Text = "ChangeFOP to"
        Me.LblChangeFOP.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.tabEditSupplier)
        Me.TabControl1.Location = New System.Drawing.Point(354, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(527, 498)
        Me.TabControl1.TabIndex = 12
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.LblAddCompany)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(418, 472)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Add Vendor"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbkFindSimilarVendor)
        Me.Panel1.Controls.Add(Me.CmbFOP1New)
        Me.Panel1.Controls.Add(Me.TxtShortName)
        Me.Panel1.Controls.Add(Me.LblCreateVendorAndSupplier)
        Me.Panel1.Controls.Add(Me.CmbCAT)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.CmbFOPNew)
        Me.Panel1.Controls.Add(Me.LblAddNewVendor)
        Me.Panel1.Location = New System.Drawing.Point(5, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(408, 454)
        Me.Panel1.TabIndex = 15
        '
        'lbkFindSimilarVendor
        '
        Me.lbkFindSimilarVendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbkFindSimilarVendor.AutoSize = True
        Me.lbkFindSimilarVendor.Location = New System.Drawing.Point(221, 77)
        Me.lbkFindSimilarVendor.Name = "lbkFindSimilarVendor"
        Me.lbkFindSimilarVendor.Size = New System.Drawing.Size(91, 13)
        Me.lbkFindSimilarVendor.TabIndex = 16
        Me.lbkFindSimilarVendor.TabStop = True
        Me.lbkFindSimilarVendor.Text = "FindSimilarVendor"
        '
        'CmbFOP1New
        '
        Me.CmbFOP1New.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFOP1New.FormattingEnabled = True
        Me.CmbFOP1New.Items.AddRange(New Object() {"BTF", "CSH"})
        Me.CmbFOP1New.Location = New System.Drawing.Point(224, 44)
        Me.CmbFOP1New.Name = "CmbFOP1New"
        Me.CmbFOP1New.Size = New System.Drawing.Size(56, 21)
        Me.CmbFOP1New.TabIndex = 15
        '
        'TxtShortName
        '
        Me.TxtShortName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtShortName.ForeColor = System.Drawing.Color.Blue
        Me.TxtShortName.Location = New System.Drawing.Point(224, 22)
        Me.TxtShortName.Name = "TxtShortName"
        Me.TxtShortName.Size = New System.Drawing.Size(180, 20)
        Me.TxtShortName.TabIndex = 1
        '
        'LblCreateVendorAndSupplier
        '
        Me.LblCreateVendorAndSupplier.AutoSize = True
        Me.LblCreateVendorAndSupplier.Location = New System.Drawing.Point(3, 349)
        Me.LblCreateVendorAndSupplier.Name = "LblCreateVendorAndSupplier"
        Me.LblCreateVendorAndSupplier.Size = New System.Drawing.Size(137, 13)
        Me.LblCreateVendorAndSupplier.TabIndex = 3
        Me.LblCreateVendorAndSupplier.TabStop = True
        Me.LblCreateVendorAndSupplier.Text = "Create Vendor and Supplier"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lbkCreateSupplierOnly)
        Me.GroupBox1.Controls.Add(Me.cboCity)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.CmbProvince)
        Me.GroupBox1.Controls.Add(Me.CmbCountry)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtPhone)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.TxtAddress)
        Me.GroupBox1.Controls.Add(Me.TxtSupplierName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(4, 109)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(401, 218)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Supplier Infor"
        '
        'lbkCreateSupplierOnly
        '
        Me.lbkCreateSupplierOnly.AutoSize = True
        Me.lbkCreateSupplierOnly.Location = New System.Drawing.Point(250, 189)
        Me.lbkCreateSupplierOnly.Name = "lbkCreateSupplierOnly"
        Me.lbkCreateSupplierOnly.Size = New System.Drawing.Size(97, 13)
        Me.lbkCreateSupplierOnly.TabIndex = 18
        Me.lbkCreateSupplierOnly.TabStop = True
        Me.lbkCreateSupplierOnly.Text = "CreateSupplierOnly"
        '
        'cboCity
        '
        Me.cboCity.FormattingEnabled = True
        Me.cboCity.Location = New System.Drawing.Point(52, 95)
        Me.cboCity.Name = "cboCity"
        Me.cboCity.Size = New System.Drawing.Size(224, 21)
        Me.cboCity.TabIndex = 17
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(2, 78)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(49, 13)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Province"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(2, 103)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "City4CWT"
        '
        'CmbProvince
        '
        Me.CmbProvince.FormattingEnabled = True
        Me.CmbProvince.Items.AddRange(New Object() {"An Giang", "Bac Giang", "Bac Kan", "Bac Lieu", "Bac Ninh", "Ba Ria Vung Tau", "Ben Tre", "Binh Dinh", "Binh Duong", "Binh Phuoc", "Binh Thuan", "Ca Mau", "Can Tho", "Cao Bang", "Da Nang", "Dak Lack", "Dak Nong", "Dien Bien", "Dong Nai", "Dong Thap", "Gia Lai", "Ha Giang", "Ha Nam", "Ha Noi", "Ha Tay", "Ha Tinh", "Hai Duong", "Hai Phong", "Hau Giang", "HCMC", "Hoa Binh", "Hung Yen", "Khanh Hoa", "Kien Giang", "Kon Tum", "Lai Châu", "Lam Dong", "Lang Son", "Lao Cai", "Long An", "Nam Dinh", "Nghe An", "Ninh Binh", "Ninh Thuan", "Phu Tho", "Phu Yen", "Quang Binh", "Quang Nam", "Quang Ngai", "Quang Ninh", "Quang Tri", "Soc Trang", "Son La", "Tay Ninh", "Thai Binh", "Thai Nguyen", "Thanh Hoa", "Thua Thien - Hue", "Tien Giang", "Tra Vinh", "Tuyen Quang", "Vinh Long", "Vinh Phuc", "Yen Bai"})
        Me.CmbProvince.Location = New System.Drawing.Point(51, 70)
        Me.CmbProvince.Name = "CmbProvince"
        Me.CmbProvince.Size = New System.Drawing.Size(225, 21)
        Me.CmbProvince.TabIndex = 2
        '
        'CmbCountry
        '
        Me.CmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCountry.FormattingEnabled = True
        Me.CmbCountry.Location = New System.Drawing.Point(52, 43)
        Me.CmbCountry.Name = "CmbCountry"
        Me.CmbCountry.Size = New System.Drawing.Size(224, 21)
        Me.CmbCountry.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(2, 185)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Phone"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(2, 141)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Address"
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(51, 182)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(193, 20)
        Me.txtPhone.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(2, 43)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Country"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(2, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Name"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(51, 161)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(349, 20)
        Me.txtEmail.TabIndex = 0
        '
        'TxtAddress
        '
        Me.TxtAddress.Location = New System.Drawing.Point(51, 119)
        Me.TxtAddress.Multiline = True
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(349, 39)
        Me.TxtAddress.TabIndex = 0
        '
        'TxtSupplierName
        '
        Me.TxtSupplierName.Location = New System.Drawing.Point(52, 16)
        Me.TxtSupplierName.Name = "TxtSupplierName"
        Me.TxtSupplierName.Size = New System.Drawing.Size(348, 20)
        Me.TxtSupplierName.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(2, 164)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "eMail"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(3, 47)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(28, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "FOP"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(3, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Cat"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(160, 47)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(62, 13)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "DefaultFOP"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(160, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "ShortName"
        '
        'CmbFOPNew
        '
        Me.CmbFOPNew.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFOPNew.FormattingEnabled = True
        Me.CmbFOPNew.Items.AddRange(New Object() {"PSP", "PPD"})
        Me.CmbFOPNew.Location = New System.Drawing.Point(32, 44)
        Me.CmbFOPNew.Name = "CmbFOPNew"
        Me.CmbFOPNew.Size = New System.Drawing.Size(55, 21)
        Me.CmbFOPNew.TabIndex = 12
        '
        'LblAddNewVendor
        '
        Me.LblAddNewVendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblAddNewVendor.AutoSize = True
        Me.LblAddNewVendor.Location = New System.Drawing.Point(29, 77)
        Me.LblAddNewVendor.Name = "LblAddNewVendor"
        Me.LblAddNewVendor.Size = New System.Drawing.Size(99, 13)
        Me.LblAddNewVendor.TabIndex = 13
        Me.LblAddNewVendor.TabStop = True
        Me.LblAddNewVendor.Text = "Create Vendor Only"
        '
        'LblAddCompany
        '
        Me.LblAddCompany.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblAddCompany.AutoSize = True
        Me.LblAddCompany.Location = New System.Drawing.Point(-51, 6)
        Me.LblAddCompany.Name = "LblAddCompany"
        Me.LblAddCompany.Size = New System.Drawing.Size(26, 13)
        Me.LblAddCompany.TabIndex = 1
        Me.LblAddCompany.TabStop = True
        Me.LblAddCompany.Text = "Add"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.CmbFOP1edit)
        Me.TabPage2.Controls.Add(Me.CmbHD_tong)
        Me.TabPage2.Controls.Add(Me.TxtNewShortName)
        Me.TabPage2.Controls.Add(Me.CmbINV)
        Me.TabPage2.Controls.Add(Me.LblChangeHD_Tong)
        Me.TabPage2.Controls.Add(Me.LblChangeFOP)
        Me.TabPage2.Controls.Add(Me.LblUpdateHD)
        Me.TabPage2.Controls.Add(Me.LblChangeName)
        Me.TabPage2.Controls.Add(Me.CmbFOPedit)
        Me.TabPage2.Controls.Add(Me.CmbNewCat)
        Me.TabPage2.Controls.Add(Me.LblDeleteCompany)
        Me.TabPage2.Controls.Add(Me.LblChangeCat)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(418, 472)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Edit"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'CmbFOP1edit
        '
        Me.CmbFOP1edit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFOP1edit.FormattingEnabled = True
        Me.CmbFOP1edit.Items.AddRange(New Object() {"BTF", "CSH"})
        Me.CmbFOP1edit.Location = New System.Drawing.Point(84, 59)
        Me.CmbFOP1edit.Name = "CmbFOP1edit"
        Me.CmbFOP1edit.Size = New System.Drawing.Size(55, 21)
        Me.CmbFOP1edit.TabIndex = 13
        '
        'CmbHD_tong
        '
        Me.CmbHD_tong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbHD_tong.FormattingEnabled = True
        Me.CmbHD_tong.Items.AddRange(New Object() {"TONG", "CHITIET"})
        Me.CmbHD_tong.Location = New System.Drawing.Point(228, 58)
        Me.CmbHD_tong.Name = "CmbHD_tong"
        Me.CmbHD_tong.Size = New System.Drawing.Size(139, 21)
        Me.CmbHD_tong.TabIndex = 12
        '
        'TxtNewShortName
        '
        Me.TxtNewShortName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtNewShortName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtNewShortName.Location = New System.Drawing.Point(228, 37)
        Me.TxtNewShortName.Name = "TxtNewShortName"
        Me.TxtNewShortName.Size = New System.Drawing.Size(139, 20)
        Me.TxtNewShortName.TabIndex = 10
        '
        'LblChangeHD_Tong
        '
        Me.LblChangeHD_Tong.AutoSize = True
        Me.LblChangeHD_Tong.Location = New System.Drawing.Point(142, 61)
        Me.LblChangeHD_Tong.Name = "LblChangeHD_Tong"
        Me.LblChangeHD_Tong.Size = New System.Drawing.Size(85, 13)
        Me.LblChangeHD_Tong.TabIndex = 11
        Me.LblChangeHD_Tong.TabStop = True
        Me.LblChangeHD_Tong.Text = "HD Tong_chitiet"
        '
        'LblChangeName
        '
        Me.LblChangeName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblChangeName.AutoSize = True
        Me.LblChangeName.Location = New System.Drawing.Point(140, 40)
        Me.LblChangeName.Name = "LblChangeName"
        Me.LblChangeName.Size = New System.Drawing.Size(88, 13)
        Me.LblChangeName.TabIndex = 9
        Me.LblChangeName.TabStop = True
        Me.LblChangeName.Text = "ChangeName To"
        Me.LblChangeName.Visible = False
        '
        'CmbNewCat
        '
        Me.CmbNewCat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CmbNewCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbNewCat.FormattingEnabled = True
        Me.CmbNewCat.Items.AddRange(New Object() {"AR - Air", "NH - Restaurant", "KS - Hotel", "XE - Coach", "BO - Boat", "LD - Land", "OS - OtherTour Service", "TO - TO/TA", "OT - Other", "US - Utility Supplier", "AL - Airlines", "AP - Airport", "YT- So YT"})
        Me.CmbNewCat.Location = New System.Drawing.Point(228, 10)
        Me.CmbNewCat.Name = "CmbNewCat"
        Me.CmbNewCat.Size = New System.Drawing.Size(139, 21)
        Me.CmbNewCat.TabIndex = 8
        '
        'LblDeleteCompany
        '
        Me.LblDeleteCompany.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblDeleteCompany.AutoSize = True
        Me.LblDeleteCompany.Location = New System.Drawing.Point(373, 13)
        Me.LblDeleteCompany.Name = "LblDeleteCompany"
        Me.LblDeleteCompany.Size = New System.Drawing.Size(38, 13)
        Me.LblDeleteCompany.TabIndex = 2
        Me.LblDeleteCompany.TabStop = True
        Me.LblDeleteCompany.Text = "Delete"
        Me.LblDeleteCompany.Visible = False
        '
        'LblChangeCat
        '
        Me.LblChangeCat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblChangeCat.AutoSize = True
        Me.LblChangeCat.Location = New System.Drawing.Point(142, 13)
        Me.LblChangeCat.Name = "LblChangeCat"
        Me.LblChangeCat.Size = New System.Drawing.Size(80, 13)
        Me.LblChangeCat.TabIndex = 7
        Me.LblChangeCat.TabStop = True
        Me.LblChangeCat.Text = "Change CAT to"
        Me.LblChangeCat.Visible = False
        '
        'tabEditSupplier
        '
        Me.tabEditSupplier.Controls.Add(Me.LblSave)
        Me.tabEditSupplier.Controls.Add(Me.cboNewCity)
        Me.tabEditSupplier.Controls.Add(Me.Label14)
        Me.tabEditSupplier.Controls.Add(Me.Label13)
        Me.tabEditSupplier.Controls.Add(Me.txtFullName)
        Me.tabEditSupplier.Controls.Add(Me.cboNewCountry)
        Me.tabEditSupplier.Controls.Add(Me.Label16)
        Me.tabEditSupplier.Controls.Add(Me.chkLast5CreatedByMyself)
        Me.tabEditSupplier.Controls.Add(Me.Label17)
        Me.tabEditSupplier.Controls.Add(Me.Label15)
        Me.tabEditSupplier.Controls.Add(Me.TxtNewPhone)
        Me.tabEditSupplier.Controls.Add(Me.lbkSearch)
        Me.tabEditSupplier.Controls.Add(Me.txtNewMail)
        Me.tabEditSupplier.Controls.Add(Me.GridSupplier_Edit)
        Me.tabEditSupplier.Controls.Add(Me.txtNewName)
        Me.tabEditSupplier.Controls.Add(Me.Label18)
        Me.tabEditSupplier.Controls.Add(Me.txtNewAddress)
        Me.tabEditSupplier.Controls.Add(Me.Label19)
        Me.tabEditSupplier.Location = New System.Drawing.Point(4, 22)
        Me.tabEditSupplier.Name = "tabEditSupplier"
        Me.tabEditSupplier.Size = New System.Drawing.Size(519, 472)
        Me.tabEditSupplier.TabIndex = 2
        Me.tabEditSupplier.Text = "EditSupplier"
        Me.tabEditSupplier.UseVisualStyleBackColor = True
        '
        'LblSave
        '
        Me.LblSave.AutoSize = True
        Me.LblSave.Location = New System.Drawing.Point(430, 448)
        Me.LblSave.Name = "LblSave"
        Me.LblSave.Size = New System.Drawing.Size(69, 13)
        Me.LblSave.TabIndex = 23
        Me.LblSave.TabStop = True
        Me.LblSave.Text = "SaveChange"
        '
        'cboNewCity
        '
        Me.cboNewCity.FormattingEnabled = True
        Me.cboNewCity.Location = New System.Drawing.Point(59, 393)
        Me.cboNewCity.Name = "cboNewCity"
        Me.cboNewCity.Size = New System.Drawing.Size(224, 21)
        Me.cboNewCity.TabIndex = 25
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 401)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(55, 13)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "City4CWT"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(4, 5)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 13)
        Me.Label13.TabIndex = 27
        Me.Label13.Text = "SupplierName"
        '
        'txtFullName
        '
        Me.txtFullName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFullName.Location = New System.Drawing.Point(7, 24)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(166, 20)
        Me.txtFullName.TabIndex = 28
        '
        'cboNewCountry
        '
        Me.cboNewCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNewCountry.FormattingEnabled = True
        Me.cboNewCountry.Location = New System.Drawing.Point(59, 366)
        Me.cboNewCountry.Name = "cboNewCountry"
        Me.cboNewCountry.Size = New System.Drawing.Size(224, 21)
        Me.cboNewCountry.TabIndex = 24
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(11, 453)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(32, 13)
        Me.Label16.TabIndex = 21
        Me.Label16.Text = "eMail"
        '
        'chkLast5CreatedByMyself
        '
        Me.chkLast5CreatedByMyself.AutoSize = True
        Me.chkLast5CreatedByMyself.Location = New System.Drawing.Point(179, 26)
        Me.chkLast5CreatedByMyself.Name = "chkLast5CreatedByMyself"
        Me.chkLast5CreatedByMyself.Size = New System.Drawing.Size(131, 17)
        Me.chkLast5CreatedByMyself.TabIndex = 29
        Me.chkLast5CreatedByMyself.Text = "Last5CreatedByMyself"
        Me.chkLast5CreatedByMyself.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(11, 427)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(38, 13)
        Me.Label17.TabIndex = 20
        Me.Label17.Text = "Phone"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(10, 369)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(43, 13)
        Me.Label15.TabIndex = 22
        Me.Label15.Text = "Country"
        '
        'TxtNewPhone
        '
        Me.TxtNewPhone.Location = New System.Drawing.Point(59, 420)
        Me.TxtNewPhone.Name = "TxtNewPhone"
        Me.TxtNewPhone.Size = New System.Drawing.Size(224, 20)
        Me.TxtNewPhone.TabIndex = 17
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(316, 26)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 30
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'txtNewMail
        '
        Me.txtNewMail.Location = New System.Drawing.Point(59, 446)
        Me.txtNewMail.Name = "txtNewMail"
        Me.txtNewMail.Size = New System.Drawing.Size(352, 20)
        Me.txtNewMail.TabIndex = 13
        '
        'GridSupplier_Edit
        '
        Me.GridSupplier_Edit.AllowUserToAddRows = False
        Me.GridSupplier_Edit.AllowUserToDeleteRows = False
        Me.GridSupplier_Edit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridSupplier_Edit.Location = New System.Drawing.Point(7, 50)
        Me.GridSupplier_Edit.Name = "GridSupplier_Edit"
        Me.GridSupplier_Edit.RowHeadersVisible = False
        Me.GridSupplier_Edit.Size = New System.Drawing.Size(514, 230)
        Me.GridSupplier_Edit.TabIndex = 16
        '
        'txtNewName
        '
        Me.txtNewName.Location = New System.Drawing.Point(59, 286)
        Me.txtNewName.Name = "txtNewName"
        Me.txtNewName.Size = New System.Drawing.Size(352, 20)
        Me.txtNewName.TabIndex = 15
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(10, 289)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(35, 13)
        Me.Label18.TabIndex = 19
        Me.Label18.Text = "Name"
        '
        'txtNewAddress
        '
        Me.txtNewAddress.Location = New System.Drawing.Point(59, 312)
        Me.txtNewAddress.Multiline = True
        Me.txtNewAddress.Name = "txtNewAddress"
        Me.txtNewAddress.Size = New System.Drawing.Size(352, 48)
        Me.txtNewAddress.TabIndex = 14
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(8, 320)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(45, 13)
        Me.Label19.TabIndex = 18
        Me.Label19.Text = "Address"
        '
        'ChckCompanyOKonly
        '
        Me.ChckCompanyOKonly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChckCompanyOKonly.AutoSize = True
        Me.ChckCompanyOKonly.Checked = True
        Me.ChckCompanyOKonly.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChckCompanyOKonly.Location = New System.Drawing.Point(107, 477)
        Me.ChckCompanyOKonly.Name = "ChckCompanyOKonly"
        Me.ChckCompanyOKonly.Size = New System.Drawing.Size(65, 17)
        Me.ChckCompanyOKonly.TabIndex = 1
        Me.ChckCompanyOKonly.Text = "OK Only"
        Me.ChckCompanyOKonly.UseVisualStyleBackColor = True
        '
        'VendorInfor_KTT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 496)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GridVendor)
        Me.Controls.Add(Me.ChckCompanyOKonly)
        Me.Controls.Add(Me.TxtNameFilter)
        Me.Controls.Add(Me.LblFilter)
        Me.Controls.Add(Me.Label2)
        Me.Location = New System.Drawing.Point(0, 56)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "VendorInfor_KTT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TransViet Travel :: RAS 12 :. Vendor Infor Update"
        CType(Me.GridVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.tabEditSupplier.ResumeLayout(False)
        Me.tabEditSupplier.PerformLayout()
        CType(Me.GridSupplier_Edit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GridVendor As System.Windows.Forms.DataGridView
    Friend WithEvents CmbCAT As System.Windows.Forms.ComboBox
    Friend WithEvents CmbINV As System.Windows.Forms.ComboBox
    Friend WithEvents LblUpdateHD As System.Windows.Forms.LinkLabel
    Friend WithEvents TxtNameFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LblFilter As System.Windows.Forms.LinkLabel
    Friend WithEvents CmbFOPedit As System.Windows.Forms.ComboBox
    Friend WithEvents LblChangeFOP As System.Windows.Forms.LinkLabel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TxtNewShortName As System.Windows.Forms.TextBox
    Friend WithEvents CmbFOPNew As System.Windows.Forms.ComboBox
    Friend WithEvents LblChangeName As System.Windows.Forms.LinkLabel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CmbNewCat As System.Windows.Forms.ComboBox
    Friend WithEvents TxtShortName As System.Windows.Forms.TextBox
    Friend WithEvents LblChangeCat As System.Windows.Forms.LinkLabel
    Friend WithEvents ChckCompanyOKonly As System.Windows.Forms.CheckBox
    Friend WithEvents LblAddCompany As System.Windows.Forms.LinkLabel
    Friend WithEvents LblDeleteCompany As System.Windows.Forms.LinkLabel
    Friend WithEvents LblAddNewVendor As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents LblCreateVendorAndSupplier As System.Windows.Forms.LinkLabel
    Friend WithEvents CmbProvince As System.Windows.Forms.ComboBox
    Friend WithEvents CmbCountry As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents TxtAddress As System.Windows.Forms.TextBox
    Friend WithEvents TxtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbHD_tong As System.Windows.Forms.ComboBox
    Friend WithEvents LblChangeHD_Tong As System.Windows.Forms.LinkLabel
    Friend WithEvents CmbFOP1New As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CmbFOP1edit As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents cboCity As ComboBox
    Friend WithEvents lbkFindSimilarVendor As LinkLabel
    Friend WithEvents lbkCreateSupplierOnly As LinkLabel
    Friend WithEvents tabEditSupplier As TabPage
    Friend WithEvents LblSave As LinkLabel
    Friend WithEvents cboNewCity As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents cboNewCountry As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents chkLast5CreatedByMyself As CheckBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents TxtNewPhone As TextBox
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents txtNewMail As TextBox
    Friend WithEvents GridSupplier_Edit As DataGridView
    Friend WithEvents txtNewName As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txtNewAddress As TextBox
    Friend WithEvents Label19 As Label
End Class
