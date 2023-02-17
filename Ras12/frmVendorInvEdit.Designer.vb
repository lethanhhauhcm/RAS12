<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVendorInvEdit
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
        Me.cboTvCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpInputDate = New System.Windows.Forms.DateTimePicker()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.RecId = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtInvID = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbkFind = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgrInvTcodes = New System.Windows.Forms.DataGridView()
        Me.txtVendorID = New System.Windows.Forms.TextBox()
        Me.txtVendor = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        Me.txtTvCompany = New System.Windows.Forms.TextBox()
        Me.txtTcodeAmount = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.MiscID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Action = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DutoanId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Matched = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgrInvTcodes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboTvCompany
        '
        Me.cboTvCompany.FormattingEnabled = True
        Me.cboTvCompany.Items.AddRange(New Object() {"GDS", "TVTR"})
        Me.cboTvCompany.Location = New System.Drawing.Point(-48, 127)
        Me.cboTvCompany.Name = "cboTvCompany"
        Me.cboTvCompany.Size = New System.Drawing.Size(41, 21)
        Me.cboTvCompany.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(205, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "InputDate"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "TvCompany"
        '
        'dtpInputDate
        '
        Me.dtpInputDate.CustomFormat = "dd MMM yy"
        Me.dtpInputDate.Enabled = False
        Me.dtpInputDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInputDate.Location = New System.Drawing.Point(265, 37)
        Me.dtpInputDate.Name = "dtpInputDate"
        Me.dtpInputDate.Size = New System.Drawing.Size(99, 20)
        Me.dtpInputDate.TabIndex = 3
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(12, 289)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 24
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'txtRecId
        '
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(91, 12)
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(100, 20)
        Me.txtRecId.TabIndex = 27
        Me.txtRecId.Text = "0"
        '
        'RecId
        '
        Me.RecId.AutoSize = True
        Me.RecId.Location = New System.Drawing.Point(42, 15)
        Me.RecId.Name = "RecId"
        Me.RecId.Size = New System.Drawing.Size(38, 13)
        Me.RecId.TabIndex = 26
        Me.RecId.Text = "RecID"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(91, 116)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(84, 20)
        Me.txtInvoiceNo.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 123)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "InvoiceNo"
        '
        'txtInvID
        '
        Me.txtInvID.Enabled = False
        Me.txtInvID.Location = New System.Drawing.Point(265, 15)
        Me.txtInvID.Name = "txtInvID"
        Me.txtInvID.Size = New System.Drawing.Size(100, 20)
        Me.txtInvID.TabIndex = 32
        Me.txtInvID.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(216, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "InvID"
        '
        'txtAccountName
        '
        Me.txtAccountName.Enabled = False
        Me.txtAccountName.Location = New System.Drawing.Point(91, 90)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.Size = New System.Drawing.Size(274, 20)
        Me.txtAccountName.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "AccountName"
        '
        'lbkFind
        '
        Me.lbkFind.AutoSize = True
        Me.lbkFind.Location = New System.Drawing.Point(511, 22)
        Me.lbkFind.Name = "lbkFind"
        Me.lbkFind.Size = New System.Drawing.Size(61, 13)
        Me.lbkFind.TabIndex = 33
        Me.lbkFind.TabStop = True
        Me.lbkFind.Text = "Find Tcode"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "VendorName"
        '
        'dgrInvTcodes
        '
        Me.dgrInvTcodes.AllowUserToAddRows = False
        Me.dgrInvTcodes.AllowUserToDeleteRows = False
        Me.dgrInvTcodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInvTcodes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MiscID, Me.Action, Me.Tcode, Me.Amount, Me.DutoanId, Me.Matched})
        Me.dgrInvTcodes.Location = New System.Drawing.Point(15, 142)
        Me.dgrInvTcodes.Name = "dgrInvTcodes"
        Me.dgrInvTcodes.Size = New System.Drawing.Size(514, 144)
        Me.dgrInvTcodes.TabIndex = 37
        '
        'txtVendorID
        '
        Me.txtVendorID.Enabled = False
        Me.txtVendorID.Location = New System.Drawing.Point(396, 15)
        Me.txtVendorID.Name = "txtVendorID"
        Me.txtVendorID.Size = New System.Drawing.Size(100, 20)
        Me.txtVendorID.TabIndex = 38
        Me.txtVendorID.Text = "0"
        Me.txtVendorID.Visible = False
        '
        'txtVendor
        '
        Me.txtVendor.Enabled = False
        Me.txtVendor.Location = New System.Drawing.Point(91, 64)
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(274, 20)
        Me.txtVendor.TabIndex = 39
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(274, 116)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(90, 20)
        Me.txtAmount.TabIndex = 40
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(181, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "InvoiceAmount"
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(344, 289)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(84, 13)
        Me.lbkSelect.TabIndex = 42
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select/Unselect"
        '
        'txtTvCompany
        '
        Me.txtTvCompany.Enabled = False
        Me.txtTvCompany.Location = New System.Drawing.Point(91, 38)
        Me.txtTvCompany.Name = "txtTvCompany"
        Me.txtTvCompany.Size = New System.Drawing.Size(100, 20)
        Me.txtTvCompany.TabIndex = 43
        Me.txtTvCompany.Text = "0"
        '
        'txtTcodeAmount
        '
        Me.txtTcodeAmount.Enabled = False
        Me.txtTcodeAmount.Location = New System.Drawing.Point(452, 116)
        Me.txtTcodeAmount.Name = "txtTcodeAmount"
        Me.txtTcodeAmount.Size = New System.Drawing.Size(77, 20)
        Me.txtTcodeAmount.TabIndex = 44
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(370, 123)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "TcodeAmount"
        '
        'MiscID
        '
        Me.MiscID.HeaderText = "MiscID"
        Me.MiscID.Name = "MiscID"
        '
        'Action
        '
        Me.Action.HeaderText = "Action"
        Me.Action.Name = "Action"
        '
        'Tcode
        '
        Me.Tcode.HeaderText = "Tcode"
        Me.Tcode.Name = "Tcode"
        '
        'Amount
        '
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        '
        'DutoanId
        '
        Me.DutoanId.HeaderText = "DutoanId"
        Me.DutoanId.Name = "DutoanId"
        '
        'Matched
        '
        Me.Matched.HeaderText = "Matched"
        Me.Matched.Name = "Matched"
        '
        'frmVendorInvEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 311)
        Me.Controls.Add(Me.txtTcodeAmount)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtTvCompany)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtVendor)
        Me.Controls.Add(Me.txtVendorID)
        Me.Controls.Add(Me.dgrInvTcodes)
        Me.Controls.Add(Me.txtAccountName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbkFind)
        Me.Controls.Add(Me.txtInvID)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.RecId)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.dtpInputDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboTvCompany)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmVendorInvEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SupplierInvEdit"
        CType(Me.dgrInvTcodes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboTvCompany As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpInputDate As DateTimePicker
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents RecId As Label
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtInvID As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtAccountName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lbkFind As LinkLabel
    Friend WithEvents Label4 As Label
    Friend WithEvents dgrInvTcodes As DataGridView
    Friend WithEvents txtVendorID As TextBox
    Friend WithEvents txtVendor As TextBox
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents lbkSelect As LinkLabel
    Friend WithEvents txtTvCompany As TextBox
    Friend WithEvents txtTcodeAmount As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents MiscID As DataGridViewTextBoxColumn
    Friend WithEvents Action As DataGridViewTextBoxColumn
    Friend WithEvents Tcode As DataGridViewTextBoxColumn
    Friend WithEvents Amount As DataGridViewTextBoxColumn
    Friend WithEvents DutoanId As DataGridViewTextBoxColumn
    Friend WithEvents Matched As DataGridViewTextBoxColumn
End Class
