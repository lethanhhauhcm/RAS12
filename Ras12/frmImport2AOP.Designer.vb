<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport2AOP
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboReportType = New System.Windows.Forms.ComboBox()
        Me.dtpSelectedDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbkGetInvoiceData = New System.Windows.Forms.LinkLabel()
        Me.lbkExport2AOP = New System.Windows.Forms.LinkLabel()
        Me.dgrRasData = New System.Windows.Forms.DataGridView()
        Me.lbkGetBillData = New System.Windows.Forms.LinkLabel()
        Me.cboAccount = New System.Windows.Forms.ComboBox()
        Me.lbkAddAccount = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrRasData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ReportType"
        '
        'cboReportType
        '
        Me.cboReportType.FormattingEnabled = True
        Me.cboReportType.Items.AddRange(New Object() {"NonAirCWT", "NonAirLocal", "Air CTS", "Air TVS"})
        Me.cboReportType.Location = New System.Drawing.Point(12, 25)
        Me.cboReportType.Name = "cboReportType"
        Me.cboReportType.Size = New System.Drawing.Size(121, 21)
        Me.cboReportType.TabIndex = 2
        '
        'dtpSelectedDate
        '
        Me.dtpSelectedDate.CustomFormat = "dd MMM yy"
        Me.dtpSelectedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSelectedDate.Location = New System.Drawing.Point(139, 26)
        Me.dtpSelectedDate.Name = "dtpSelectedDate"
        Me.dtpSelectedDate.Size = New System.Drawing.Size(121, 20)
        Me.dtpSelectedDate.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(136, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "SelectedDate"
        '
        'lbkGetInvoiceData
        '
        Me.lbkGetInvoiceData.AutoSize = True
        Me.lbkGetInvoiceData.Location = New System.Drawing.Point(266, 28)
        Me.lbkGetInvoiceData.Name = "lbkGetInvoiceData"
        Me.lbkGetInvoiceData.Size = New System.Drawing.Size(82, 13)
        Me.lbkGetInvoiceData.TabIndex = 5
        Me.lbkGetInvoiceData.TabStop = True
        Me.lbkGetInvoiceData.Text = "GetInvoiceData"
        '
        'lbkExport2AOP
        '
        Me.lbkExport2AOP.AutoSize = True
        Me.lbkExport2AOP.Location = New System.Drawing.Point(522, 28)
        Me.lbkExport2AOP.Name = "lbkExport2AOP"
        Me.lbkExport2AOP.Size = New System.Drawing.Size(65, 13)
        Me.lbkExport2AOP.TabIndex = 6
        Me.lbkExport2AOP.TabStop = True
        Me.lbkExport2AOP.Text = "Export2AOP"
        '
        'dgrRasData
        '
        Me.dgrRasData.AllowUserToAddRows = False
        Me.dgrRasData.AllowUserToDeleteRows = False
        Me.dgrRasData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrRasData.Location = New System.Drawing.Point(12, 52)
        Me.dgrRasData.Name = "dgrRasData"
        Me.dgrRasData.ReadOnly = True
        Me.dgrRasData.Size = New System.Drawing.Size(960, 497)
        Me.dgrRasData.TabIndex = 7
        '
        'lbkGetBillData
        '
        Me.lbkGetBillData.AutoSize = True
        Me.lbkGetBillData.Location = New System.Drawing.Point(371, 28)
        Me.lbkGetBillData.Name = "lbkGetBillData"
        Me.lbkGetBillData.Size = New System.Drawing.Size(60, 13)
        Me.lbkGetBillData.TabIndex = 8
        Me.lbkGetBillData.TabStop = True
        Me.lbkGetBillData.Text = "GetBillData"
        '
        'cboAccount
        '
        Me.cboAccount.FormattingEnabled = True
        Me.cboAccount.Location = New System.Drawing.Point(15, 556)
        Me.cboAccount.Name = "cboAccount"
        Me.cboAccount.Size = New System.Drawing.Size(303, 21)
        Me.cboAccount.TabIndex = 9
        '
        'lbkAddAccount
        '
        Me.lbkAddAccount.AutoSize = True
        Me.lbkAddAccount.Location = New System.Drawing.Point(324, 564)
        Me.lbkAddAccount.Name = "lbkAddAccount"
        Me.lbkAddAccount.Size = New System.Drawing.Size(66, 13)
        Me.lbkAddAccount.TabIndex = 10
        Me.lbkAddAccount.TabStop = True
        Me.lbkAddAccount.Text = "AddAccount"
        '
        'frmImportDailyReport2AOP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 589)
        Me.Controls.Add(Me.lbkAddAccount)
        Me.Controls.Add(Me.cboAccount)
        Me.Controls.Add(Me.lbkGetBillData)
        Me.Controls.Add(Me.dgrRasData)
        Me.Controls.Add(Me.lbkExport2AOP)
        Me.Controls.Add(Me.lbkGetInvoiceData)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpSelectedDate)
        Me.Controls.Add(Me.cboReportType)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmImportDailyReport2AOP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ImportDailyReport2AOP"
        CType(Me.dgrRasData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cboReportType As ComboBox
    Friend WithEvents dtpSelectedDate As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents lbkGetInvoiceData As LinkLabel
    Friend WithEvents lbkExport2AOP As LinkLabel
    Friend WithEvents dgrRasData As DataGridView
    Friend WithEvents lbkGetBillData As LinkLabel
    Friend WithEvents cboAccount As ComboBox
    Friend WithEvents lbkAddAccount As LinkLabel
End Class
