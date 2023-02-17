<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMapVendorBankBranch
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
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.dgrBankBranches = New System.Windows.Forms.DataGridView()
        Me.dgrVendor = New System.Windows.Forms.DataGridView()
        Me.chkFilteredByBankAdress = New System.Windows.Forms.CheckBox()
        Me.cboBankProvince = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbkMap = New System.Windows.Forms.LinkLabel()
        Me.chkFilteredByVendorBankName = New System.Windows.Forms.CheckBox()
        CType(Me.dgrBankBranches, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtBankName
        '
        Me.txtBankName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankName.Location = New System.Drawing.Point(561, 86)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(184, 20)
        Me.txtBankName.TabIndex = 46
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(558, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "BankName"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(822, 89)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 44
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(775, 89)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 43
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'dgrBankBranches
        '
        Me.dgrBankBranches.AllowUserToAddRows = False
        Me.dgrBankBranches.AllowUserToDeleteRows = False
        Me.dgrBankBranches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrBankBranches.Location = New System.Drawing.Point(-2, 112)
        Me.dgrBankBranches.Name = "dgrBankBranches"
        Me.dgrBankBranches.ReadOnly = True
        Me.dgrBankBranches.Size = New System.Drawing.Size(885, 412)
        Me.dgrBankBranches.TabIndex = 42
        '
        'dgrVendor
        '
        Me.dgrVendor.AllowUserToAddRows = False
        Me.dgrVendor.AllowUserToDeleteRows = False
        Me.dgrVendor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrVendor.Location = New System.Drawing.Point(-2, 1)
        Me.dgrVendor.Name = "dgrVendor"
        Me.dgrVendor.ReadOnly = True
        Me.dgrVendor.Size = New System.Drawing.Size(885, 68)
        Me.dgrVendor.TabIndex = 47
        '
        'chkFilteredByBankAdress
        '
        Me.chkFilteredByBankAdress.AutoSize = True
        Me.chkFilteredByBankAdress.Location = New System.Drawing.Point(0, 95)
        Me.chkFilteredByBankAdress.Name = "chkFilteredByBankAdress"
        Me.chkFilteredByBankAdress.Size = New System.Drawing.Size(163, 17)
        Me.chkFilteredByBankAdress.TabIndex = 49
        Me.chkFilteredByBankAdress.Text = "FilteredByVendorBankAdress"
        Me.chkFilteredByBankAdress.UseVisualStyleBackColor = True
        '
        'cboBankProvince
        '
        Me.cboBankProvince.FormattingEnabled = True
        Me.cboBankProvince.Location = New System.Drawing.Point(310, 86)
        Me.cboBankProvince.Name = "cboBankProvince"
        Me.cboBankProvince.Size = New System.Drawing.Size(245, 21)
        Me.cboBankProvince.TabIndex = 50
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(307, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "BankProvince"
        '
        'lbkMap
        '
        Me.lbkMap.AutoSize = True
        Me.lbkMap.Location = New System.Drawing.Point(12, 539)
        Me.lbkMap.Name = "lbkMap"
        Me.lbkMap.Size = New System.Drawing.Size(28, 13)
        Me.lbkMap.TabIndex = 52
        Me.lbkMap.TabStop = True
        Me.lbkMap.Text = "Map"
        '
        'chkFilteredByVendorBankName
        '
        Me.chkFilteredByVendorBankName.AutoSize = True
        Me.chkFilteredByVendorBankName.Location = New System.Drawing.Point(0, 72)
        Me.chkFilteredByVendorBankName.Name = "chkFilteredByVendorBankName"
        Me.chkFilteredByVendorBankName.Size = New System.Drawing.Size(159, 17)
        Me.chkFilteredByVendorBankName.TabIndex = 53
        Me.chkFilteredByVendorBankName.Text = "FilteredByVendorBankName"
        Me.chkFilteredByVendorBankName.UseVisualStyleBackColor = True
        '
        'frmMapVendorBankBranch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.Controls.Add(Me.chkFilteredByVendorBankName)
        Me.Controls.Add(Me.lbkMap)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboBankProvince)
        Me.Controls.Add(Me.chkFilteredByBankAdress)
        Me.Controls.Add(Me.dgrVendor)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrBankBranches)
        Me.Name = "frmMapVendorBankBranch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MapVendorBankBranch"
        CType(Me.dgrBankBranches, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtBankName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents dgrBankBranches As DataGridView
    Friend WithEvents dgrVendor As DataGridView
    Friend WithEvents chkFilteredByBankAdress As CheckBox
    Friend WithEvents cboBankProvince As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lbkMap As LinkLabel
    Friend WithEvents chkFilteredByVendorBankName As CheckBox
End Class
