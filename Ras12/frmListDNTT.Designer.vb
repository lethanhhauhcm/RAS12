<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListDNTT
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
        Me.Label43 = New System.Windows.Forms.Label()
        Me.cboVendor = New System.Windows.Forms.ComboBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtRef = New System.Windows.Forms.TextBox()
        Me.dgrRequested = New System.Windows.Forms.DataGridView()
        Me.S = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboFstUser = New System.Windows.Forms.ComboBox()
        Me.lbkPrint = New System.Windows.Forms.LinkLabel()
        Me.lbkResetAsUnprinted = New System.Windows.Forms.LinkLabel()
        Me.lbkFilter = New System.Windows.Forms.LinkLabel()
        Me.chkPrinted = New System.Windows.Forms.CheckBox()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.chkSamePrintID = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboTVC = New System.Windows.Forms.ComboBox()
        CType(Me.dgrRequested, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(12, 10)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(41, 13)
        Me.Label43.TabIndex = 65
        Me.Label43.Text = "Vendor"
        '
        'cboVendor
        '
        Me.cboVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVendor.FormattingEnabled = True
        Me.cboVendor.Location = New System.Drawing.Point(12, 25)
        Me.cboVendor.Name = "cboVendor"
        Me.cboVendor.Size = New System.Drawing.Size(214, 21)
        Me.cboVendor.TabIndex = 64
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(232, 10)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(24, 13)
        Me.Label39.TabIndex = 62
        Me.Label39.Text = "Ref"
        '
        'txtRef
        '
        Me.txtRef.Location = New System.Drawing.Point(232, 26)
        Me.txtRef.Name = "txtRef"
        Me.txtRef.Size = New System.Drawing.Size(167, 20)
        Me.txtRef.TabIndex = 57
        Me.txtRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dgrRequested
        '
        Me.dgrRequested.AllowUserToAddRows = False
        Me.dgrRequested.AllowUserToDeleteRows = False
        Me.dgrRequested.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgrRequested.BackgroundColor = System.Drawing.Color.MintCream
        Me.dgrRequested.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrRequested.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.S})
        Me.dgrRequested.Location = New System.Drawing.Point(12, 52)
        Me.dgrRequested.Name = "dgrRequested"
        Me.dgrRequested.RowHeadersVisible = False
        Me.dgrRequested.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgrRequested.Size = New System.Drawing.Size(702, 323)
        Me.dgrRequested.TabIndex = 58
        '
        'S
        '
        Me.S.HeaderText = "S"
        Me.S.Name = "S"
        Me.S.Width = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(401, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 67
        Me.Label1.Text = "User"
        '
        'cboFstUser
        '
        Me.cboFstUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFstUser.FormattingEnabled = True
        Me.cboFstUser.Location = New System.Drawing.Point(404, 25)
        Me.cboFstUser.Name = "cboFstUser"
        Me.cboFstUser.Size = New System.Drawing.Size(47, 21)
        Me.cboFstUser.TabIndex = 66
        '
        'lbkPrint
        '
        Me.lbkPrint.AutoSize = True
        Me.lbkPrint.Location = New System.Drawing.Point(15, 395)
        Me.lbkPrint.Name = "lbkPrint"
        Me.lbkPrint.Size = New System.Drawing.Size(28, 13)
        Me.lbkPrint.TabIndex = 68
        Me.lbkPrint.TabStop = True
        Me.lbkPrint.Text = "Print"
        '
        'lbkResetAsUnprinted
        '
        Me.lbkResetAsUnprinted.AutoSize = True
        Me.lbkResetAsUnprinted.Location = New System.Drawing.Point(83, 395)
        Me.lbkResetAsUnprinted.Name = "lbkResetAsUnprinted"
        Me.lbkResetAsUnprinted.Size = New System.Drawing.Size(93, 13)
        Me.lbkResetAsUnprinted.TabIndex = 69
        Me.lbkResetAsUnprinted.TabStop = True
        Me.lbkResetAsUnprinted.Text = "ResetAsUnprinted"
        '
        'lbkFilter
        '
        Me.lbkFilter.AutoSize = True
        Me.lbkFilter.Location = New System.Drawing.Point(642, 28)
        Me.lbkFilter.Name = "lbkFilter"
        Me.lbkFilter.Size = New System.Drawing.Size(29, 13)
        Me.lbkFilter.TabIndex = 70
        Me.lbkFilter.TabStop = True
        Me.lbkFilter.Text = "Filter"
        '
        'chkPrinted
        '
        Me.chkPrinted.AutoSize = True
        Me.chkPrinted.Location = New System.Drawing.Point(522, 9)
        Me.chkPrinted.Name = "chkPrinted"
        Me.chkPrinted.Size = New System.Drawing.Size(59, 17)
        Me.chkPrinted.TabIndex = 71
        Me.chkPrinted.Text = "Printed"
        Me.chkPrinted.UseVisualStyleBackColor = True
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(685, 29)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 72
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'chkSamePrintID
        '
        Me.chkSamePrintID.AutoSize = True
        Me.chkSamePrintID.Location = New System.Drawing.Point(522, 27)
        Me.chkSamePrintID.Name = "chkSamePrintID"
        Me.chkSamePrintID.Size = New System.Drawing.Size(85, 17)
        Me.chkSamePrintID.TabIndex = 73
        Me.chkSamePrintID.Text = "SamePrintID"
        Me.chkSamePrintID.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(454, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 75
        Me.Label2.Text = "TVC"
        '
        'cboTVC
        '
        Me.cboTVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTVC.FormattingEnabled = True
        Me.cboTVC.Items.AddRange(New Object() {"GDS", "TVTR"})
        Me.cboTVC.Location = New System.Drawing.Point(457, 25)
        Me.cboTVC.Name = "cboTVC"
        Me.cboTVC.Size = New System.Drawing.Size(59, 21)
        Me.cboTVC.TabIndex = 74
        '
        'frmListDNTT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(726, 561)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboTVC)
        Me.Controls.Add(Me.chkSamePrintID)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.chkPrinted)
        Me.Controls.Add(Me.lbkFilter)
        Me.Controls.Add(Me.lbkResetAsUnprinted)
        Me.Controls.Add(Me.lbkPrint)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboFstUser)
        Me.Controls.Add(Me.Label43)
        Me.Controls.Add(Me.cboVendor)
        Me.Controls.Add(Me.Label39)
        Me.Controls.Add(Me.txtRef)
        Me.Controls.Add(Me.dgrRequested)
        Me.Name = "frmListDNTT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ApproveDNTT"
        CType(Me.dgrRequested, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label43 As Label
    Friend WithEvents cboVendor As ComboBox
    Friend WithEvents Label39 As Label
    Friend WithEvents txtRef As TextBox
    Friend WithEvents dgrRequested As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cboFstUser As ComboBox
    Friend WithEvents lbkPrint As LinkLabel
    Friend WithEvents lbkResetAsUnprinted As LinkLabel
    Friend WithEvents lbkFilter As LinkLabel
    Friend WithEvents chkPrinted As CheckBox
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents S As DataGridViewCheckBoxColumn
    Friend WithEvents chkSamePrintID As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboTVC As ComboBox
End Class
