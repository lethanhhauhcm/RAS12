<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class unReportedTickets
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GridUnRptTix = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbLstNdays = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboSystem = New System.Windows.Forms.ComboBox()
        Me.lbkRun = New System.Windows.Forms.LinkLabel()
        Me.lbkImportBL = New System.Windows.Forms.LinkLabel()
        Me.lbkEmail = New System.Windows.Forms.LinkLabel()
        Me.lbkImportTravelGuard = New System.Windows.Forms.LinkLabel()
        Me.lbkExclude = New System.Windows.Forms.LinkLabel()
        Me.lbkTempReport1S = New System.Windows.Forms.LinkLabel()
        CType(Me.GridUnRptTix, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridUnRptTix
        '
        Me.GridUnRptTix.AllowUserToAddRows = False
        Me.GridUnRptTix.AllowUserToDeleteRows = False
        Me.GridUnRptTix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.GridUnRptTix.BackgroundColor = System.Drawing.Color.LightCyan
        Me.GridUnRptTix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridUnRptTix.Location = New System.Drawing.Point(4, 26)
        Me.GridUnRptTix.Name = "GridUnRptTix"
        Me.GridUnRptTix.RowHeadersVisible = False
        Me.GridUnRptTix.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridUnRptTix.Size = New System.Drawing.Size(773, 470)
        Me.GridUnRptTix.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(97, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "days"
        '
        'CmbLstNdays
        '
        Me.CmbLstNdays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLstNdays.FormattingEnabled = True
        Me.CmbLstNdays.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "32"})
        Me.CmbLstNdays.Location = New System.Drawing.Point(45, 2)
        Me.CmbLstNdays.Name = "CmbLstNdays"
        Me.CmbLstNdays.Size = New System.Drawing.Size(46, 21)
        Me.CmbLstNdays.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Last"
        '
        'cboSystem
        '
        Me.cboSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSystem.FormattingEnabled = True
        Me.cboSystem.Items.AddRange(New Object() {"1A", "1S", "AK", "BL", "VJ", "TravelGuard"})
        Me.cboSystem.Location = New System.Drawing.Point(132, 2)
        Me.cboSystem.Name = "cboSystem"
        Me.cboSystem.Size = New System.Drawing.Size(86, 21)
        Me.cboSystem.TabIndex = 4
        '
        'lbkRun
        '
        Me.lbkRun.AutoSize = True
        Me.lbkRun.Location = New System.Drawing.Point(224, 9)
        Me.lbkRun.Name = "lbkRun"
        Me.lbkRun.Size = New System.Drawing.Size(27, 13)
        Me.lbkRun.TabIndex = 5
        Me.lbkRun.TabStop = True
        Me.lbkRun.Text = "Run"
        '
        'lbkImportBL
        '
        Me.lbkImportBL.AutoSize = True
        Me.lbkImportBL.Location = New System.Drawing.Point(619, 9)
        Me.lbkImportBL.Name = "lbkImportBL"
        Me.lbkImportBL.Size = New System.Drawing.Size(49, 13)
        Me.lbkImportBL.TabIndex = 6
        Me.lbkImportBL.TabStop = True
        Me.lbkImportBL.Text = "ImportBL"
        '
        'lbkEmail
        '
        Me.lbkEmail.AutoSize = True
        Me.lbkEmail.Location = New System.Drawing.Point(257, 9)
        Me.lbkEmail.Name = "lbkEmail"
        Me.lbkEmail.Size = New System.Drawing.Size(32, 13)
        Me.lbkEmail.TabIndex = 7
        Me.lbkEmail.TabStop = True
        Me.lbkEmail.Text = "Email"
        '
        'lbkImportTravelGuard
        '
        Me.lbkImportTravelGuard.AutoSize = True
        Me.lbkImportTravelGuard.Location = New System.Drawing.Point(674, 9)
        Me.lbkImportTravelGuard.Name = "lbkImportTravelGuard"
        Me.lbkImportTravelGuard.Size = New System.Drawing.Size(95, 13)
        Me.lbkImportTravelGuard.TabIndex = 8
        Me.lbkImportTravelGuard.TabStop = True
        Me.lbkImportTravelGuard.Text = "ImportTravelGuard"
        '
        'lbkExclude
        '
        Me.lbkExclude.AutoSize = True
        Me.lbkExclude.Location = New System.Drawing.Point(304, 9)
        Me.lbkExclude.Name = "lbkExclude"
        Me.lbkExclude.Size = New System.Drawing.Size(45, 13)
        Me.lbkExclude.TabIndex = 9
        Me.lbkExclude.TabStop = True
        Me.lbkExclude.Text = "Exclude"
        '
        'lbkTempReport1S
        '
        Me.lbkTempReport1S.AutoSize = True
        Me.lbkTempReport1S.Location = New System.Drawing.Point(416, 9)
        Me.lbkTempReport1S.Name = "lbkTempReport1S"
        Me.lbkTempReport1S.Size = New System.Drawing.Size(79, 13)
        Me.lbkTempReport1S.TabIndex = 10
        Me.lbkTempReport1S.TabStop = True
        Me.lbkTempReport1S.Text = "TempReport1S"
        '
        'unReportedTickets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 496)
        Me.Controls.Add(Me.lbkTempReport1S)
        Me.Controls.Add(Me.lbkExclude)
        Me.Controls.Add(Me.lbkImportTravelGuard)
        Me.Controls.Add(Me.lbkEmail)
        Me.Controls.Add(Me.lbkImportBL)
        Me.Controls.Add(Me.lbkRun)
        Me.Controls.Add(Me.cboSystem)
        Me.Controls.Add(Me.CmbLstNdays)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GridUnRptTix)
        Me.Controls.Add(Me.Label1)
        Me.Location = New System.Drawing.Point(0, 50)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "unReportedTickets"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "TransViet Travel :: RAS 12 :. unReported Tickets"
        CType(Me.GridUnRptTix, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GridUnRptTix As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbLstNdays As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboSystem As ComboBox
    Friend WithEvents lbkRun As LinkLabel
    Friend WithEvents lbkImportBL As LinkLabel
    Friend WithEvents lbkEmail As LinkLabel
    Friend WithEvents lbkImportTravelGuard As LinkLabel
    Friend WithEvents lbkExclude As LinkLabel
    Friend WithEvents lbkTempReport1S As LinkLabel
End Class
