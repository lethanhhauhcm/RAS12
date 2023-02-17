<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvSendNotice
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
        Me.lbkSendNotice2TaxOffc = New System.Windows.Forms.LinkLabel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboAction = New System.Windows.Forms.ComboBox()
        Me.lbkViewOldInv = New System.Windows.Forms.LinkLabel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboKyHieu = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboMauSo = New System.Windows.Forms.ComboBox()
        Me.txtInvId = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblTVC = New System.Windows.Forms.Label()
        Me.cboTVC = New System.Windows.Forms.ComboBox()
        Me.S = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.dgrE_Invoices = New System.Windows.Forms.DataGridView()
        Me.lbkRefreshProcessStatus = New System.Windows.Forms.LinkLabel()
        Me.lbkRestoreInvoices = New System.Windows.Forms.LinkLabel()
        Me.lbkViewNewInv = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrE_Invoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkSendNotice2TaxOffc
        '
        Me.lbkSendNotice2TaxOffc.AutoSize = True
        Me.lbkSendNotice2TaxOffc.Location = New System.Drawing.Point(263, 589)
        Me.lbkSendNotice2TaxOffc.Name = "lbkSendNotice2TaxOffc"
        Me.lbkSendNotice2TaxOffc.Size = New System.Drawing.Size(107, 13)
        Me.lbkSendNotice2TaxOffc.TabIndex = 215
        Me.lbkSendNotice2TaxOffc.TabStop = True
        Me.lbkSendNotice2TaxOffc.Text = "SendNotice2TaxOffc"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(650, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(37, 13)
        Me.Label10.TabIndex = 214
        Me.Label10.Text = "Action"
        '
        'cboAction
        '
        Me.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAction.FormattingEnabled = True
        Me.cboAction.Items.AddRange(New Object() {"1-Cancel", "2-Adjust", "3-Replace", "4-Explain"})
        Me.cboAction.Location = New System.Drawing.Point(654, 24)
        Me.cboAction.Name = "cboAction"
        Me.cboAction.Size = New System.Drawing.Size(107, 21)
        Me.cboAction.TabIndex = 213
        '
        'lbkViewOldInv
        '
        Me.lbkViewOldInv.AutoSize = True
        Me.lbkViewOldInv.Location = New System.Drawing.Point(520, 589)
        Me.lbkViewOldInv.Name = "lbkViewOldInv"
        Me.lbkViewOldInv.Size = New System.Drawing.Size(61, 13)
        Me.lbkViewOldInv.TabIndex = 211
        Me.lbkViewOldInv.TabStop = True
        Me.lbkViewOldInv.Text = "ViewOldInv"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(431, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 204
        Me.Label8.Text = "Status"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"Sent", "NotSent"})
        Me.cboStatus.Location = New System.Drawing.Point(435, 24)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(64, 21)
        Me.cboStatus.TabIndex = 203
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(232, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 202
        Me.Label7.Text = "KyHieu"
        '
        'cboKyHieu
        '
        Me.cboKyHieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKyHieu.FormattingEnabled = True
        Me.cboKyHieu.Location = New System.Drawing.Point(234, 26)
        Me.cboKyHieu.Name = "cboKyHieu"
        Me.cboKyHieu.Size = New System.Drawing.Size(65, 21)
        Me.cboKyHieu.TabIndex = 201
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(105, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 200
        Me.Label6.Text = "MauSo"
        '
        'cboMauSo
        '
        Me.cboMauSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMauSo.FormattingEnabled = True
        Me.cboMauSo.Location = New System.Drawing.Point(108, 25)
        Me.cboMauSo.Name = "cboMauSo"
        Me.cboMauSo.Size = New System.Drawing.Size(121, 21)
        Me.cboMauSo.TabIndex = 199
        '
        'txtInvId
        '
        Me.txtInvId.Location = New System.Drawing.Point(305, 26)
        Me.txtInvId.Name = "txtInvId"
        Me.txtInvId.Size = New System.Drawing.Size(65, 20)
        Me.txtInvId.TabIndex = 190
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(302, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 189
        Me.Label5.Text = "InvoiceId"
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(505, 26)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(143, 20)
        Me.txtReason.TabIndex = 187
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(502, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 186
        Me.Label4.Text = "Reason"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(376, 25)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(53, 20)
        Me.txtInvoiceNo.TabIndex = 185
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(373, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 184
        Me.Label3.Text = "InvoiceNo"
        '
        'lblTVC
        '
        Me.lblTVC.AutoSize = True
        Me.lblTVC.Location = New System.Drawing.Point(13, 10)
        Me.lblTVC.Name = "lblTVC"
        Me.lblTVC.Size = New System.Drawing.Size(28, 13)
        Me.lblTVC.TabIndex = 177
        Me.lblTVC.Text = "TVC"
        '
        'cboTVC
        '
        Me.cboTVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTVC.FormattingEnabled = True
        Me.cboTVC.Location = New System.Drawing.Point(16, 26)
        Me.cboTVC.Name = "cboTVC"
        Me.cboTVC.Size = New System.Drawing.Size(86, 21)
        Me.cboTVC.TabIndex = 176
        '
        'S
        '
        Me.S.HeaderText = "S"
        Me.S.Name = "S"
        Me.S.Width = 20
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(928, 24)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 166
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(881, 24)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 165
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'dgrE_Invoices
        '
        Me.dgrE_Invoices.AllowUserToAddRows = False
        Me.dgrE_Invoices.AllowUserToDeleteRows = False
        Me.dgrE_Invoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrE_Invoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrE_Invoices.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.S})
        Me.dgrE_Invoices.Location = New System.Drawing.Point(17, 53)
        Me.dgrE_Invoices.Name = "dgrE_Invoices"
        Me.dgrE_Invoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgrE_Invoices.Size = New System.Drawing.Size(979, 506)
        Me.dgrE_Invoices.TabIndex = 163
        '
        'lbkRefreshProcessStatus
        '
        Me.lbkRefreshProcessStatus.AutoSize = True
        Me.lbkRefreshProcessStatus.Location = New System.Drawing.Point(26, 589)
        Me.lbkRefreshProcessStatus.Name = "lbkRefreshProcessStatus"
        Me.lbkRefreshProcessStatus.Size = New System.Drawing.Size(106, 13)
        Me.lbkRefreshProcessStatus.TabIndex = 216
        Me.lbkRefreshProcessStatus.TabStop = True
        Me.lbkRefreshProcessStatus.Text = "CheckProcessStatus"
        '
        'lbkRestoreInvoices
        '
        Me.lbkRestoreInvoices.AutoSize = True
        Me.lbkRestoreInvoices.Location = New System.Drawing.Point(155, 589)
        Me.lbkRestoreInvoices.Name = "lbkRestoreInvoices"
        Me.lbkRestoreInvoices.Size = New System.Drawing.Size(84, 13)
        Me.lbkRestoreInvoices.TabIndex = 217
        Me.lbkRestoreInvoices.TabStop = True
        Me.lbkRestoreInvoices.Text = "RestoreInvoices"
        '
        'lbkViewNewInv
        '
        Me.lbkViewNewInv.AutoSize = True
        Me.lbkViewNewInv.Location = New System.Drawing.Point(587, 589)
        Me.lbkViewNewInv.Name = "lbkViewNewInv"
        Me.lbkViewNewInv.Size = New System.Drawing.Size(67, 13)
        Me.lbkViewNewInv.TabIndex = 218
        Me.lbkViewNewInv.TabStop = True
        Me.lbkViewNewInv.Text = "ViewNewInv"
        '
        'frmInvSendNotice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.lbkViewNewInv)
        Me.Controls.Add(Me.lbkRestoreInvoices)
        Me.Controls.Add(Me.lbkRefreshProcessStatus)
        Me.Controls.Add(Me.lbkSendNotice2TaxOffc)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cboAction)
        Me.Controls.Add(Me.lbkViewOldInv)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboKyHieu)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboMauSo)
        Me.Controls.Add(Me.txtInvId)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtReason)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblTVC)
        Me.Controls.Add(Me.cboTVC)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrE_Invoices)
        Me.Name = "frmInvSendNotice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Invoice Send Notice to Tax Office"
        CType(Me.dgrE_Invoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbkSendNotice2TaxOffc As LinkLabel
    Friend WithEvents Label10 As Label
    Friend WithEvents cboAction As ComboBox
    Friend WithEvents lbkViewOldInv As LinkLabel
    Friend WithEvents Label8 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cboKyHieu As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cboMauSo As ComboBox
    Friend WithEvents txtInvId As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtReason As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblTVC As Label
    Friend WithEvents cboTVC As ComboBox
    Friend WithEvents S As DataGridViewCheckBoxColumn
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents dgrE_Invoices As DataGridView
    Friend WithEvents lbkRefreshProcessStatus As LinkLabel
    Friend WithEvents lbkRestoreInvoices As LinkLabel
    Friend WithEvents lbkViewNewInv As LinkLabel
End Class
