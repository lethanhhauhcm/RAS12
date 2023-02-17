<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorList
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
        Me.txtShortName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.dgrVendors = New System.Windows.Forms.DataGridView()
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboFOP1 = New System.Windows.Forms.ComboBox()
        Me.cboCAT = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboFOP = New System.Windows.Forms.ComboBox()
        Me.chkLast5CreatedByMyself = New System.Windows.Forms.CheckBox()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkExpire = New System.Windows.Forms.LinkLabel()
        Me.dgrSuppliers = New System.Windows.Forms.DataGridView()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        Me.lbkAddSupplier = New System.Windows.Forms.LinkLabel()
        Me.lbkReActivate = New System.Windows.Forms.LinkLabel()
        Me.lbkMapBank = New System.Windows.Forms.LinkLabel()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgrBankBranch = New System.Windows.Forms.DataGridView()
        Me.lbkUnMap = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrVendors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrSuppliers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrBankBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtShortName
        '
        Me.txtShortName.Location = New System.Drawing.Point(51, 12)
        Me.txtShortName.Name = "txtShortName"
        Me.txtShortName.Size = New System.Drawing.Size(184, 20)
        Me.txtShortName.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(48, -2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "ShortName"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "XX"})
        Me.cboStatus.Location = New System.Drawing.Point(2, 11)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(43, 21)
        Me.cboStatus.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(-1, -2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Status"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(893, 15)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 16
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'dgrVendors
        '
        Me.dgrVendors.AllowUserToAddRows = False
        Me.dgrVendors.AllowUserToDeleteRows = False
        Me.dgrVendors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrVendors.Location = New System.Drawing.Point(2, 62)
        Me.dgrVendors.Name = "dgrVendors"
        Me.dgrVendors.ReadOnly = True
        Me.dgrVendors.Size = New System.Drawing.Size(978, 330)
        Me.dgrVendors.TabIndex = 15
        '
        'txtAccountName
        '
        Me.txtAccountName.Location = New System.Drawing.Point(238, 12)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.Size = New System.Drawing.Size(184, 20)
        Me.txtAccountName.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(235, -2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "AccountName"
        '
        'cboFOP1
        '
        Me.cboFOP1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFOP1.FormattingEnabled = True
        Me.cboFOP1.Items.AddRange(New Object() {"BTF", "CSH"})
        Me.cboFOP1.Location = New System.Drawing.Point(614, 11)
        Me.cboFOP1.Name = "cboFOP1"
        Me.cboFOP1.Size = New System.Drawing.Size(56, 21)
        Me.cboFOP1.TabIndex = 28
        '
        'cboCAT
        '
        Me.cboCAT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCAT.FormattingEnabled = True
        Me.cboCAT.Items.AddRange(New Object() {"AR - Air", "NH - Restaurant", "KS - Hotel", "XE - Coach", "BO - Boat", "LD - Land", "OS - OtherTour Service", "TO - TO/TA", "OT - Other", "US - Utility Supplier", "AL - Airlines", "AP - Airport", "YT - So YT", "GD - Guide", "TV - TransViet", "KB - KickBack", "CG - Cargo"})
        Me.cboCAT.Location = New System.Drawing.Point(427, 12)
        Me.cboCAT.Name = "cboCAT"
        Me.cboCAT.Size = New System.Drawing.Size(120, 21)
        Me.cboCAT.TabIndex = 23
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(553, -2)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(28, 13)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "FOP"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(424, -2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Cat"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(612, -2)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 13)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "FOP1"
        '
        'cboFOP
        '
        Me.cboFOP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFOP.FormattingEnabled = True
        Me.cboFOP.Items.AddRange(New Object() {"PSP", "PPD"})
        Me.cboFOP.Location = New System.Drawing.Point(553, 11)
        Me.cboFOP.Name = "cboFOP"
        Me.cboFOP.Size = New System.Drawing.Size(55, 21)
        Me.cboFOP.TabIndex = 27
        '
        'chkLast5CreatedByMyself
        '
        Me.chkLast5CreatedByMyself.AutoSize = True
        Me.chkLast5CreatedByMyself.Location = New System.Drawing.Point(3, 39)
        Me.chkLast5CreatedByMyself.Name = "chkLast5CreatedByMyself"
        Me.chkLast5CreatedByMyself.Size = New System.Drawing.Size(131, 17)
        Me.chkLast5CreatedByMyself.TabIndex = 30
        Me.chkLast5CreatedByMyself.Text = "Last5CreatedByMyself"
        Me.chkLast5CreatedByMyself.UseVisualStyleBackColor = True
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(940, 15)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 31
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(4, 589)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 32
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(51, 589)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 33
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkExpire
        '
        Me.lbkExpire.AutoSize = True
        Me.lbkExpire.Location = New System.Drawing.Point(98, 589)
        Me.lbkExpire.Name = "lbkExpire"
        Me.lbkExpire.Size = New System.Drawing.Size(36, 13)
        Me.lbkExpire.TabIndex = 34
        Me.lbkExpire.TabStop = True
        Me.lbkExpire.Text = "Expire"
        '
        'dgrSuppliers
        '
        Me.dgrSuppliers.AllowUserToAddRows = False
        Me.dgrSuppliers.AllowUserToDeleteRows = False
        Me.dgrSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrSuppliers.Location = New System.Drawing.Point(3, 398)
        Me.dgrSuppliers.Name = "dgrSuppliers"
        Me.dgrSuppliers.ReadOnly = True
        Me.dgrSuppliers.Size = New System.Drawing.Size(978, 105)
        Me.dgrSuppliers.TabIndex = 35
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(424, 589)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 36
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        Me.lbkSelect.Visible = False
        '
        'lbkAddSupplier
        '
        Me.lbkAddSupplier.AutoSize = True
        Me.lbkAddSupplier.Location = New System.Drawing.Point(260, 589)
        Me.lbkAddSupplier.Name = "lbkAddSupplier"
        Me.lbkAddSupplier.Size = New System.Drawing.Size(64, 13)
        Me.lbkAddSupplier.TabIndex = 37
        Me.lbkAddSupplier.TabStop = True
        Me.lbkAddSupplier.Text = "AddSupplier"
        '
        'lbkReActivate
        '
        Me.lbkReActivate.AutoSize = True
        Me.lbkReActivate.Location = New System.Drawing.Point(140, 589)
        Me.lbkReActivate.Name = "lbkReActivate"
        Me.lbkReActivate.Size = New System.Drawing.Size(60, 13)
        Me.lbkReActivate.TabIndex = 38
        Me.lbkReActivate.TabStop = True
        Me.lbkReActivate.Text = "ReActivate"
        '
        'lbkMapBank
        '
        Me.lbkMapBank.AutoSize = True
        Me.lbkMapBank.Location = New System.Drawing.Point(633, 589)
        Me.lbkMapBank.Name = "lbkMapBank"
        Me.lbkMapBank.Size = New System.Drawing.Size(49, 13)
        Me.lbkMapBank.TabIndex = 39
        Me.lbkMapBank.TabStop = True
        Me.lbkMapBank.Text = "MapTCB"
        '
        'txtBankName
        '
        Me.txtBankName.Location = New System.Drawing.Point(679, 12)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(184, 20)
        Me.txtBankName.TabIndex = 41
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(676, -2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 13)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "BankName"
        '
        'dgrBankBranch
        '
        Me.dgrBankBranch.AllowUserToAddRows = False
        Me.dgrBankBranch.AllowUserToDeleteRows = False
        Me.dgrBankBranch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrBankBranch.Location = New System.Drawing.Point(2, 509)
        Me.dgrBankBranch.Name = "dgrBankBranch"
        Me.dgrBankBranch.ReadOnly = True
        Me.dgrBankBranch.Size = New System.Drawing.Size(978, 77)
        Me.dgrBankBranch.TabIndex = 42
        '
        'lbkUnMap
        '
        Me.lbkUnMap.AutoSize = True
        Me.lbkUnMap.Location = New System.Drawing.Point(740, 589)
        Me.lbkUnMap.Name = "lbkUnMap"
        Me.lbkUnMap.Size = New System.Drawing.Size(67, 13)
        Me.lbkUnMap.TabIndex = 43
        Me.lbkUnMap.TabStop = True
        Me.lbkUnMap.Text = "UnMapBank"
        '
        'frmVendorList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.lbkUnMap)
        Me.Controls.Add(Me.dgrBankBranch)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lbkMapBank)
        Me.Controls.Add(Me.lbkReActivate)
        Me.Controls.Add(Me.lbkAddSupplier)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.dgrSuppliers)
        Me.Controls.Add(Me.lbkExpire)
        Me.Controls.Add(Me.lbkEdit)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.chkLast5CreatedByMyself)
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
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrVendors)
        Me.Name = "frmVendorList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VendorList"
        CType(Me.dgrVendors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrSuppliers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrBankBranch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtShortName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents dgrVendors As DataGridView
    Friend WithEvents txtAccountName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cboFOP1 As ComboBox
    Friend WithEvents cboCAT As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents cboFOP As ComboBox
    Friend WithEvents chkLast5CreatedByMyself As CheckBox
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkExpire As LinkLabel
    Friend WithEvents dgrSuppliers As DataGridView
    Friend WithEvents lbkSelect As LinkLabel
    Friend WithEvents lbkAddSupplier As LinkLabel
    Friend WithEvents lbkReActivate As LinkLabel
    Friend WithEvents lbkMapBank As LinkLabel
    Friend WithEvents txtBankName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dgrBankBranch As DataGridView
    Friend WithEvents lbkUnMap As LinkLabel
End Class
