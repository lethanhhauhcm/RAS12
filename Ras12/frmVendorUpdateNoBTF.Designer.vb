<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorUpdateNoBTF
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
        Me.lblVendor = New System.Windows.Forms.Label()
        Me.cboVendor = New System.Windows.Forms.ComboBox()
        Me.lblUserRequest = New System.Windows.Forms.Label()
        Me.cboUserRequest = New System.Windows.Forms.ComboBox()
        Me.lblAccountName = New System.Windows.Forms.Label()
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.lblAccountNo = New System.Windows.Forms.Label()
        Me.txtAccountNo = New System.Windows.Forms.TextBox()
        Me.lblBank = New System.Windows.Forms.Label()
        Me.txtBank = New System.Windows.Forms.TextBox()
        Me.dgvVendorUpdateNoBTF = New System.Windows.Forms.DataGridView()
        Me.llbRefresh = New System.Windows.Forms.LinkLabel()
        Me.llbAdd = New System.Windows.Forms.LinkLabel()
        Me.llbEdit = New System.Windows.Forms.LinkLabel()
        Me.llbDelete = New System.Windows.Forms.LinkLabel()
        CType(Me.dgvVendorUpdateNoBTF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblVendor
        '
        Me.lblVendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblVendor.AutoSize = True
        Me.lblVendor.Location = New System.Drawing.Point(38, 422)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(44, 13)
        Me.lblVendor.TabIndex = 0
        Me.lblVendor.Text = "Vendor:"
        '
        'cboVendor
        '
        Me.cboVendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cboVendor.FormattingEnabled = True
        Me.cboVendor.Location = New System.Drawing.Point(88, 419)
        Me.cboVendor.Name = "cboVendor"
        Me.cboVendor.Size = New System.Drawing.Size(340, 21)
        Me.cboVendor.TabIndex = 1
        '
        'lblUserRequest
        '
        Me.lblUserRequest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserRequest.AutoSize = True
        Me.lblUserRequest.Location = New System.Drawing.Point(10, 448)
        Me.lblUserRequest.Name = "lblUserRequest"
        Me.lblUserRequest.Size = New System.Drawing.Size(72, 13)
        Me.lblUserRequest.TabIndex = 2
        Me.lblUserRequest.Text = "UserRequest:"
        '
        'cboUserRequest
        '
        Me.cboUserRequest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cboUserRequest.FormattingEnabled = True
        Me.cboUserRequest.Location = New System.Drawing.Point(88, 446)
        Me.cboUserRequest.Name = "cboUserRequest"
        Me.cboUserRequest.Size = New System.Drawing.Size(230, 21)
        Me.cboUserRequest.TabIndex = 3
        '
        'lblAccountName
        '
        Me.lblAccountName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblAccountName.AutoSize = True
        Me.lblAccountName.Location = New System.Drawing.Point(468, 422)
        Me.lblAccountName.Name = "lblAccountName"
        Me.lblAccountName.Size = New System.Drawing.Size(78, 13)
        Me.lblAccountName.TabIndex = 4
        Me.lblAccountName.Text = "AccountName:"
        '
        'txtAccountName
        '
        Me.txtAccountName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAccountName.Location = New System.Drawing.Point(552, 419)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.Size = New System.Drawing.Size(200, 20)
        Me.txtAccountName.TabIndex = 5
        '
        'lblAccountNo
        '
        Me.lblAccountNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblAccountNo.AutoSize = True
        Me.lblAccountNo.Location = New System.Drawing.Point(482, 448)
        Me.lblAccountNo.Name = "lblAccountNo"
        Me.lblAccountNo.Size = New System.Drawing.Size(64, 13)
        Me.lblAccountNo.TabIndex = 6
        Me.lblAccountNo.Text = "AccountNo:"
        '
        'txtAccountNo
        '
        Me.txtAccountNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAccountNo.Location = New System.Drawing.Point(552, 445)
        Me.txtAccountNo.Name = "txtAccountNo"
        Me.txtAccountNo.Size = New System.Drawing.Size(120, 20)
        Me.txtAccountNo.TabIndex = 7
        '
        'lblBank
        '
        Me.lblBank.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBank.AutoSize = True
        Me.lblBank.Location = New System.Drawing.Point(511, 474)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(35, 13)
        Me.lblBank.TabIndex = 8
        Me.lblBank.Text = "Bank:"
        '
        'txtBank
        '
        Me.txtBank.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtBank.Location = New System.Drawing.Point(552, 471)
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(200, 20)
        Me.txtBank.TabIndex = 9
        '
        'dgvVendorUpdateNoBTF
        '
        Me.dgvVendorUpdateNoBTF.AllowUserToAddRows = False
        Me.dgvVendorUpdateNoBTF.AllowUserToDeleteRows = False
        Me.dgvVendorUpdateNoBTF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvVendorUpdateNoBTF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvVendorUpdateNoBTF.Location = New System.Drawing.Point(12, 25)
        Me.dgvVendorUpdateNoBTF.Name = "dgvVendorUpdateNoBTF"
        Me.dgvVendorUpdateNoBTF.ReadOnly = True
        Me.dgvVendorUpdateNoBTF.Size = New System.Drawing.Size(738, 388)
        Me.dgvVendorUpdateNoBTF.TabIndex = 10
        '
        'llbRefresh
        '
        Me.llbRefresh.AutoSize = True
        Me.llbRefresh.Location = New System.Drawing.Point(12, 9)
        Me.llbRefresh.Name = "llbRefresh"
        Me.llbRefresh.Size = New System.Drawing.Size(44, 13)
        Me.llbRefresh.TabIndex = 11
        Me.llbRefresh.TabStop = True
        Me.llbRefresh.Text = "Refresh"
        '
        'llbAdd
        '
        Me.llbAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbAdd.AutoSize = True
        Me.llbAdd.Location = New System.Drawing.Point(651, 508)
        Me.llbAdd.Name = "llbAdd"
        Me.llbAdd.Size = New System.Drawing.Size(26, 13)
        Me.llbAdd.TabIndex = 12
        Me.llbAdd.TabStop = True
        Me.llbAdd.Text = "Add"
        '
        'llbEdit
        '
        Me.llbEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbEdit.AutoSize = True
        Me.llbEdit.Location = New System.Drawing.Point(683, 508)
        Me.llbEdit.Name = "llbEdit"
        Me.llbEdit.Size = New System.Drawing.Size(25, 13)
        Me.llbEdit.TabIndex = 13
        Me.llbEdit.TabStop = True
        Me.llbEdit.Text = "Edit"
        '
        'llbDelete
        '
        Me.llbDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbDelete.AutoSize = True
        Me.llbDelete.Location = New System.Drawing.Point(714, 508)
        Me.llbDelete.Name = "llbDelete"
        Me.llbDelete.Size = New System.Drawing.Size(38, 13)
        Me.llbDelete.TabIndex = 14
        Me.llbDelete.TabStop = True
        Me.llbDelete.Text = "Delete"
        '
        'frmVendorUpdateNoBTF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(769, 530)
        Me.Controls.Add(Me.llbDelete)
        Me.Controls.Add(Me.llbEdit)
        Me.Controls.Add(Me.llbAdd)
        Me.Controls.Add(Me.llbRefresh)
        Me.Controls.Add(Me.dgvVendorUpdateNoBTF)
        Me.Controls.Add(Me.txtBank)
        Me.Controls.Add(Me.lblBank)
        Me.Controls.Add(Me.txtAccountNo)
        Me.Controls.Add(Me.lblAccountNo)
        Me.Controls.Add(Me.txtAccountName)
        Me.Controls.Add(Me.lblAccountName)
        Me.Controls.Add(Me.cboUserRequest)
        Me.Controls.Add(Me.lblUserRequest)
        Me.Controls.Add(Me.cboVendor)
        Me.Controls.Add(Me.lblVendor)
        Me.Name = "frmVendorUpdateNoBTF"
        Me.Text = "VendorUpdateNoBTF"
        CType(Me.dgvVendorUpdateNoBTF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblVendor As Label
    Friend WithEvents cboVendor As ComboBox
    Friend WithEvents lblUserRequest As Label
    Friend WithEvents cboUserRequest As ComboBox
    Friend WithEvents lblAccountName As Label
    Friend WithEvents txtAccountName As TextBox
    Friend WithEvents lblAccountNo As Label
    Friend WithEvents txtAccountNo As TextBox
    Friend WithEvents lblBank As Label
    Friend WithEvents txtBank As TextBox
    Friend WithEvents dgvVendorUpdateNoBTF As DataGridView
    Friend WithEvents llbRefresh As LinkLabel
    Friend WithEvents llbAdd As LinkLabel
    Friend WithEvents llbEdit As LinkLabel
    Friend WithEvents llbDelete As LinkLabel
End Class
