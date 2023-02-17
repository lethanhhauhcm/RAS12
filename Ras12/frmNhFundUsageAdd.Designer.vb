<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNhFundUsageAdd
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.txtFundId = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtResidual = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboWaiverType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dgrOldTicket = New System.Windows.Forms.DataGridView()
        Me.TKNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtRloc = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpRequestDate = New System.Windows.Forms.DateTimePicker()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.chkNhApproved = New System.Windows.Forms.CheckBox()
        CType(Me.dgrOldTicket, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Customer"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(156, 171)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(100, 20)
        Me.txtAmount.TabIndex = 3
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(153, 366)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 6
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Location = New System.Drawing.Point(18, 171)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(80, 13)
        Me.lblAmount.TabIndex = 35
        Me.lblAmount.Text = "Deducted USD"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(93, 366)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 5
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'txtFundId
        '
        Me.txtFundId.Location = New System.Drawing.Point(156, 36)
        Me.txtFundId.Name = "txtFundId"
        Me.txtFundId.ReadOnly = True
        Me.txtFundId.Size = New System.Drawing.Size(100, 20)
        Me.txtFundId.TabIndex = 43
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "FundId"
        '
        'txtResidual
        '
        Me.txtResidual.Location = New System.Drawing.Point(156, 60)
        Me.txtResidual.Name = "txtResidual"
        Me.txtResidual.ReadOnly = True
        Me.txtResidual.Size = New System.Drawing.Size(100, 20)
        Me.txtResidual.TabIndex = 45
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Residual USD"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "WaiverType"
        '
        'cboWaiverType
        '
        Me.cboWaiverType.FormattingEnabled = True
        Me.cboWaiverType.Items.AddRange(New Object() {"SF1", "SF2", "SF3", "SF4", "SF5", "SF6", "Add2Fund"})
        Me.cboWaiverType.Location = New System.Drawing.Point(156, 118)
        Me.cboWaiverType.Name = "cboWaiverType"
        Me.cboWaiverType.Size = New System.Drawing.Size(100, 21)
        Me.cboWaiverType.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 221)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 49
        Me.Label6.Text = "Old Ticket"
        '
        'dgrOldTicket
        '
        Me.dgrOldTicket.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrOldTicket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrOldTicket.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TKNO})
        Me.dgrOldTicket.Location = New System.Drawing.Point(21, 244)
        Me.dgrOldTicket.Name = "dgrOldTicket"
        Me.dgrOldTicket.Size = New System.Drawing.Size(239, 119)
        Me.dgrOldTicket.TabIndex = 48
        '
        'TKNO
        '
        Me.TKNO.HeaderText = "TKNO"
        Me.TKNO.Name = "TKNO"
        Me.TKNO.Width = 62
        '
        'txtRloc
        '
        Me.txtRloc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRloc.Location = New System.Drawing.Point(156, 197)
        Me.txtRloc.MaxLength = 6
        Me.txtRloc.Name = "txtRloc"
        Me.txtRloc.Size = New System.Drawing.Size(100, 20)
        Me.txtRloc.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 197)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "RLOC"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 96)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 13)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "RequestDate"
        '
        'dtpRequestDate
        '
        Me.dtpRequestDate.CustomFormat = "dd MMM yy"
        Me.dtpRequestDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRequestDate.Location = New System.Drawing.Point(156, 89)
        Me.dtpRequestDate.Name = "dtpRequestDate"
        Me.dtpRequestDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpRequestDate.TabIndex = 1
        '
        'cboCustomer
        '
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(96, 10)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(160, 21)
        Me.cboCustomer.TabIndex = 0
        '
        'chkNhApproved
        '
        Me.chkNhApproved.AutoSize = True
        Me.chkNhApproved.Location = New System.Drawing.Point(156, 145)
        Me.chkNhApproved.Name = "chkNhApproved"
        Me.chkNhApproved.Size = New System.Drawing.Size(86, 17)
        Me.chkNhApproved.TabIndex = 53
        Me.chkNhApproved.Text = "NhApproved"
        Me.chkNhApproved.UseVisualStyleBackColor = True
        '
        'frmNhFundUsageAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 391)
        Me.Controls.Add(Me.chkNhApproved)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.dtpRequestDate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtRloc)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.dgrOldTicket)
        Me.Controls.Add(Me.cboWaiverType)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtResidual)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtFundId)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.lblAmount)
        Me.Controls.Add(Me.lbkSave)
        Me.Name = "frmNhFundUsageAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NhFundUsageAdd"
        CType(Me.dgrOldTicket, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents lblAmount As Label
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents txtFundId As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtResidual As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cboWaiverType As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents dgrOldTicket As DataGridView
    Friend WithEvents txtRloc As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents dtpRequestDate As DateTimePicker
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents TKNO As DataGridViewTextBoxColumn
    Friend WithEvents chkNhApproved As CheckBox
End Class
