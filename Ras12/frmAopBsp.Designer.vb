<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAopBsp
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnMain = New System.Windows.Forms.Panel()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.tpDownload = New System.Windows.Forms.TabPage()
        Me.dgvDownload = New System.Windows.Forms.DataGridView()
        Me.lblDueDate = New System.Windows.Forms.Label()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.lbkRefreshDownlloadQueue = New System.Windows.Forms.LinkLabel()
        Me.lbkDownload = New System.Windows.Forms.LinkLabel()
        Me.tpCompare = New System.Windows.Forms.TabPage()
        Me.dgvMain = New System.Windows.Forms.DataGridView()
        Me.TKNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TRNC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AOPAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BspAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.llbExportExcel = New System.Windows.Forms.LinkLabel()
        Me.llbCompare = New System.Windows.Forms.LinkLabel()
        Me.ofdExcel = New System.Windows.Forms.OpenFileDialog()
        Me.pnMain.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.tpDownload.SuspendLayout()
        CType(Me.dgvDownload, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCompare.SuspendLayout()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnMain
        '
        Me.pnMain.Controls.Add(Me.tcMain)
        Me.pnMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnMain.Location = New System.Drawing.Point(0, 0)
        Me.pnMain.Name = "pnMain"
        Me.pnMain.Size = New System.Drawing.Size(847, 450)
        Me.pnMain.TabIndex = 1
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.tpDownload)
        Me.tcMain.Controls.Add(Me.tpCompare)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(847, 450)
        Me.tcMain.TabIndex = 9
        '
        'tpDownload
        '
        Me.tpDownload.BackColor = System.Drawing.SystemColors.Control
        Me.tpDownload.Controls.Add(Me.dgvDownload)
        Me.tpDownload.Controls.Add(Me.lblDueDate)
        Me.tpDownload.Controls.Add(Me.dtpDueDate)
        Me.tpDownload.Controls.Add(Me.lbkRefreshDownlloadQueue)
        Me.tpDownload.Controls.Add(Me.lbkDownload)
        Me.tpDownload.Location = New System.Drawing.Point(4, 22)
        Me.tpDownload.Name = "tpDownload"
        Me.tpDownload.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDownload.Size = New System.Drawing.Size(839, 424)
        Me.tpDownload.TabIndex = 0
        Me.tpDownload.Text = "Download"
        '
        'dgvDownload
        '
        Me.dgvDownload.AllowUserToAddRows = False
        Me.dgvDownload.AllowUserToDeleteRows = False
        Me.dgvDownload.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDownload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDownload.Location = New System.Drawing.Point(8, 32)
        Me.dgvDownload.Name = "dgvDownload"
        Me.dgvDownload.ReadOnly = True
        Me.dgvDownload.Size = New System.Drawing.Size(825, 384)
        Me.dgvDownload.TabIndex = 14
        '
        'lblDueDate
        '
        Me.lblDueDate.AutoSize = True
        Me.lblDueDate.Location = New System.Drawing.Point(5, 10)
        Me.lblDueDate.Name = "lblDueDate"
        Me.lblDueDate.Size = New System.Drawing.Size(50, 13)
        Me.lblDueDate.TabIndex = 13
        Me.lblDueDate.Text = "DueDate"
        '
        'dtpDueDate
        '
        Me.dtpDueDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDueDate.Location = New System.Drawing.Point(61, 6)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(95, 20)
        Me.dtpDueDate.TabIndex = 12
        '
        'lbkRefreshDownlloadQueue
        '
        Me.lbkRefreshDownlloadQueue.AutoSize = True
        Me.lbkRefreshDownlloadQueue.Location = New System.Drawing.Point(261, 10)
        Me.lbkRefreshDownlloadQueue.Name = "lbkRefreshDownlloadQueue"
        Me.lbkRefreshDownlloadQueue.Size = New System.Drawing.Size(44, 13)
        Me.lbkRefreshDownlloadQueue.TabIndex = 11
        Me.lbkRefreshDownlloadQueue.TabStop = True
        Me.lbkRefreshDownlloadQueue.Text = "Refresh"
        '
        'lbkDownload
        '
        Me.lbkDownload.AutoSize = True
        Me.lbkDownload.Location = New System.Drawing.Point(162, 10)
        Me.lbkDownload.Name = "lbkDownload"
        Me.lbkDownload.Size = New System.Drawing.Size(55, 13)
        Me.lbkDownload.TabIndex = 10
        Me.lbkDownload.TabStop = True
        Me.lbkDownload.Text = "Download"
        '
        'tpCompare
        '
        Me.tpCompare.BackColor = System.Drawing.SystemColors.Control
        Me.tpCompare.Controls.Add(Me.dgvMain)
        Me.tpCompare.Controls.Add(Me.llbExportExcel)
        Me.tpCompare.Controls.Add(Me.llbCompare)
        Me.tpCompare.Location = New System.Drawing.Point(4, 22)
        Me.tpCompare.Name = "tpCompare"
        Me.tpCompare.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCompare.Size = New System.Drawing.Size(839, 424)
        Me.tpCompare.TabIndex = 1
        Me.tpCompare.Text = "Compare"
        '
        'dgvMain
        '
        Me.dgvMain.AllowUserToAddRows = False
        Me.dgvMain.AllowUserToDeleteRows = False
        Me.dgvMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TKNO, Me.TRNC, Me.AOPAmount, Me.BspAmount, Me.Description})
        Me.dgvMain.Location = New System.Drawing.Point(3, 19)
        Me.dgvMain.Name = "dgvMain"
        Me.dgvMain.ReadOnly = True
        Me.dgvMain.Size = New System.Drawing.Size(830, 397)
        Me.dgvMain.TabIndex = 10
        '
        'TKNO
        '
        Me.TKNO.HeaderText = "TKNO"
        Me.TKNO.Name = "TKNO"
        Me.TKNO.ReadOnly = True
        Me.TKNO.Width = 150
        '
        'TRNC
        '
        Me.TRNC.HeaderText = "TRNC"
        Me.TRNC.Name = "TRNC"
        Me.TRNC.ReadOnly = True
        '
        'AOPAmount
        '
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.AOPAmount.DefaultCellStyle = DataGridViewCellStyle1
        Me.AOPAmount.HeaderText = "AOPAmount"
        Me.AOPAmount.Name = "AOPAmount"
        Me.AOPAmount.ReadOnly = True
        Me.AOPAmount.Width = 150
        '
        'BspAmount
        '
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.BspAmount.DefaultCellStyle = DataGridViewCellStyle2
        Me.BspAmount.HeaderText = "BspAmount"
        Me.BspAmount.Name = "BspAmount"
        Me.BspAmount.ReadOnly = True
        Me.BspAmount.Width = 150
        '
        'Description
        '
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        Me.Description.Width = 200
        '
        'llbExportExcel
        '
        Me.llbExportExcel.AutoSize = True
        Me.llbExportExcel.Enabled = False
        Me.llbExportExcel.Location = New System.Drawing.Point(61, 3)
        Me.llbExportExcel.Name = "llbExportExcel"
        Me.llbExportExcel.Size = New System.Drawing.Size(63, 13)
        Me.llbExportExcel.TabIndex = 9
        Me.llbExportExcel.TabStop = True
        Me.llbExportExcel.Text = "ExportExcel"
        '
        'llbCompare
        '
        Me.llbCompare.AutoSize = True
        Me.llbCompare.Location = New System.Drawing.Point(6, 3)
        Me.llbCompare.Name = "llbCompare"
        Me.llbCompare.Size = New System.Drawing.Size(49, 13)
        Me.llbCompare.TabIndex = 8
        Me.llbCompare.TabStop = True
        Me.llbCompare.Text = "Compare"
        '
        'ofdExcel
        '
        Me.ofdExcel.FileName = "OpenFileDialog1"
        '
        'frmAopBsp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(847, 450)
        Me.Controls.Add(Me.pnMain)
        Me.Name = "frmAopBsp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AopBsp"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnMain.ResumeLayout(False)
        Me.tcMain.ResumeLayout(False)
        Me.tpDownload.ResumeLayout(False)
        Me.tpDownload.PerformLayout()
        CType(Me.dgvDownload, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCompare.ResumeLayout(False)
        Me.tpCompare.PerformLayout()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnMain As Panel
    Friend WithEvents ofdExcel As OpenFileDialog
    Friend WithEvents tcMain As TabControl
    Friend WithEvents tpDownload As TabPage
    Friend WithEvents lblDueDate As Label
    Friend WithEvents dtpDueDate As DateTimePicker
    Friend WithEvents lbkRefreshDownlloadQueue As LinkLabel
    Friend WithEvents lbkDownload As LinkLabel
    Friend WithEvents tpCompare As TabPage
    Friend WithEvents dgvDownload As DataGridView
    Friend WithEvents dgvMain As DataGridView
    Friend WithEvents llbExportExcel As LinkLabel
    Friend WithEvents llbCompare As LinkLabel
    Friend WithEvents TKNO As DataGridViewTextBoxColumn
    Friend WithEvents TRNC As DataGridViewTextBoxColumn
    Friend WithEvents AOPAmount As DataGridViewTextBoxColumn
    Friend WithEvents BspAmount As DataGridViewTextBoxColumn
    Friend WithEvents Description As DataGridViewTextBoxColumn
End Class
