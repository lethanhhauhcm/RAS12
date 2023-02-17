<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerReports
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
        Me.cboReport = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.blkGetReport = New System.Windows.Forms.LinkLabel()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lbkGetData = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'cboReport
        '
        Me.cboReport.FormattingEnabled = True
        Me.cboReport.Items.AddRange(New Object() {"Abbott Weekly Control", "Bayer Financial HY", "BP Monthly Settlement", "Citi Monthly Settlement", "Chevron Quarterly Settlement", "Ericsson Quarterly Settlement", "EY Quarterly Settlement", "GE Monthly Air Breakdown", "GE Monthly ScoreCard", "Halliburton Monthly CPT", "Henkel POS Invoicing HY", "HPI Monthly", "IFAD Fortnightly", "JPMC Quarterly Settlement", "L Oreal Yearly Air", "Oracle Monthly Settlement", "Visa Monthly CTA", "Walmart Monthly Settlement"})
        Me.cboReport.Location = New System.Drawing.Point(68, 15)
        Me.cboReport.Name = "cboReport"
        Me.cboReport.Size = New System.Drawing.Size(200, 21)
        Me.cboReport.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "FromDate"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "ToDate"
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(205, 113)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 10
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'blkGetReport
        '
        Me.blkGetReport.AutoSize = True
        Me.blkGetReport.Location = New System.Drawing.Point(132, 113)
        Me.blkGetReport.Name = "blkGetReport"
        Me.blkGetReport.Size = New System.Drawing.Size(56, 13)
        Me.blkGetReport.TabIndex = 9
        Me.blkGetReport.TabStop = True
        Me.blkGetReport.Text = "GetReport"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd MMM yy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(68, 42)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpFromDate.TabIndex = 14
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd MMM yy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(68, 68)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpToDate.TabIndex = 15
        '
        'lbkGetData
        '
        Me.lbkGetData.AutoSize = True
        Me.lbkGetData.Location = New System.Drawing.Point(69, 113)
        Me.lbkGetData.Name = "lbkGetData"
        Me.lbkGetData.Size = New System.Drawing.Size(47, 13)
        Me.lbkGetData.TabIndex = 16
        Me.lbkGetData.TabStop = True
        Me.lbkGetData.Text = "GetData"
        '
        'frmCustomerReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 173)
        Me.Controls.Add(Me.lbkGetData)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.cboReport)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.blkGetReport)
        Me.Name = "frmCustomerReports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmNonMonthlyReports"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboReport As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbkCancel As System.Windows.Forms.LinkLabel
    Friend WithEvents blkGetReport As System.Windows.Forms.LinkLabel
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbkGetData As System.Windows.Forms.LinkLabel
End Class
