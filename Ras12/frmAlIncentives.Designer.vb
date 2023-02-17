<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlIncentives
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
        Me.tcIncentives = New System.Windows.Forms.TabControl()
        Me.tpIncentiveFormula = New System.Windows.Forms.TabPage()
        Me.lbkClone = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.dtpValidOn = New System.Windows.Forms.DateTimePicker()
        Me.cboDateType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCar = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgrIncentiveFormula = New System.Windows.Forms.DataGridView()
        Me.tpIncentiveRoutings = New System.Windows.Forms.TabPage()
        Me.lbkCloneCities_vv = New System.Windows.Forms.LinkLabel()
        Me.lbkEditRtg = New System.Windows.Forms.LinkLabel()
        Me.lbkNewRtg = New System.Windows.Forms.LinkLabel()
        Me.blkClearRtg = New System.Windows.Forms.LinkLabel()
        Me.lbkSearchRtg = New System.Windows.Forms.LinkLabel()
        Me.cboStatusRtg = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboCarRtg = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dgrIncentiveRtg = New System.Windows.Forms.DataGridView()
        Me.tpReport = New System.Windows.Forms.TabPage()
        Me.cboCounter = New System.Windows.Forms.ComboBox()
        Me.Counter = New System.Windows.Forms.Label()
        Me.lbkReport2Excel = New System.Windows.Forms.LinkLabel()
        Me.cboDateTypeRpt = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboCarRpt = New System.Windows.Forms.ComboBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tcIncentives.SuspendLayout()
        Me.tpIncentiveFormula.SuspendLayout()
        CType(Me.dgrIncentiveFormula, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpIncentiveRoutings.SuspendLayout()
        CType(Me.dgrIncentiveRtg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpReport.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcIncentives
        '
        Me.tcIncentives.Controls.Add(Me.tpIncentiveFormula)
        Me.tcIncentives.Controls.Add(Me.tpIncentiveRoutings)
        Me.tcIncentives.Controls.Add(Me.tpReport)
        Me.tcIncentives.Location = New System.Drawing.Point(2, 1)
        Me.tcIncentives.Name = "tcIncentives"
        Me.tcIncentives.SelectedIndex = 0
        Me.tcIncentives.Size = New System.Drawing.Size(1007, 610)
        Me.tcIncentives.TabIndex = 0
        '
        'tpIncentiveFormula
        '
        Me.tpIncentiveFormula.Controls.Add(Me.lbkClone)
        Me.tpIncentiveFormula.Controls.Add(Me.lbkEdit)
        Me.tpIncentiveFormula.Controls.Add(Me.lbkNew)
        Me.tpIncentiveFormula.Controls.Add(Me.lbkClear)
        Me.tpIncentiveFormula.Controls.Add(Me.lbkSearch)
        Me.tpIncentiveFormula.Controls.Add(Me.dtpValidOn)
        Me.tpIncentiveFormula.Controls.Add(Me.cboDateType)
        Me.tpIncentiveFormula.Controls.Add(Me.Label3)
        Me.tpIncentiveFormula.Controls.Add(Me.cboStatus)
        Me.tpIncentiveFormula.Controls.Add(Me.Label2)
        Me.tpIncentiveFormula.Controls.Add(Me.cboCar)
        Me.tpIncentiveFormula.Controls.Add(Me.Label1)
        Me.tpIncentiveFormula.Controls.Add(Me.dgrIncentiveFormula)
        Me.tpIncentiveFormula.Location = New System.Drawing.Point(4, 22)
        Me.tpIncentiveFormula.Name = "tpIncentiveFormula"
        Me.tpIncentiveFormula.Padding = New System.Windows.Forms.Padding(3)
        Me.tpIncentiveFormula.Size = New System.Drawing.Size(999, 584)
        Me.tpIncentiveFormula.TabIndex = 0
        Me.tpIncentiveFormula.Text = "IncentiveFormula"
        Me.tpIncentiveFormula.UseVisualStyleBackColor = True
        '
        'lbkClone
        '
        Me.lbkClone.AutoSize = True
        Me.lbkClone.Location = New System.Drawing.Point(78, 554)
        Me.lbkClone.Name = "lbkClone"
        Me.lbkClone.Size = New System.Drawing.Size(34, 13)
        Me.lbkClone.TabIndex = 13
        Me.lbkClone.TabStop = True
        Me.lbkClone.Text = "Clone"
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(47, 554)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 12
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(10, 554)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 11
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(399, 33)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 10
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(331, 31)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 9
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'dtpValidOn
        '
        Me.dtpValidOn.CustomFormat = "dd MMM yy"
        Me.dtpValidOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpValidOn.Location = New System.Drawing.Point(190, 25)
        Me.dtpValidOn.Name = "dtpValidOn"
        Me.dtpValidOn.Size = New System.Drawing.Size(89, 20)
        Me.dtpValidOn.TabIndex = 8
        '
        'cboDateType
        '
        Me.cboDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDateType.FormattingEnabled = True
        Me.cboDateType.Items.AddRange(New Object() {"DOF", "DOI"})
        Me.cboDateType.Location = New System.Drawing.Point(131, 25)
        Me.cboDateType.Name = "cboDateType"
        Me.cboDateType.Size = New System.Drawing.Size(53, 21)
        Me.cboDateType.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(128, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "ValidOn"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "XX"})
        Me.cboStatus.Location = New System.Drawing.Point(69, 25)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(53, 21)
        Me.cboStatus.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(66, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Status"
        '
        'cboCar
        '
        Me.cboCar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCar.FormattingEnabled = True
        Me.cboCar.Location = New System.Drawing.Point(13, 25)
        Me.cboCar.Name = "cboCar"
        Me.cboCar.Size = New System.Drawing.Size(53, 21)
        Me.cboCar.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Car"
        '
        'dgrIncentiveFormula
        '
        Me.dgrIncentiveFormula.AllowUserToAddRows = False
        Me.dgrIncentiveFormula.AllowUserToDeleteRows = False
        Me.dgrIncentiveFormula.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrIncentiveFormula.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrIncentiveFormula.Location = New System.Drawing.Point(0, 61)
        Me.dgrIncentiveFormula.Name = "dgrIncentiveFormula"
        Me.dgrIncentiveFormula.ReadOnly = True
        Me.dgrIncentiveFormula.Size = New System.Drawing.Size(999, 481)
        Me.dgrIncentiveFormula.TabIndex = 1
        '
        'tpIncentiveRoutings
        '
        Me.tpIncentiveRoutings.Controls.Add(Me.lbkCloneCities_vv)
        Me.tpIncentiveRoutings.Controls.Add(Me.lbkEditRtg)
        Me.tpIncentiveRoutings.Controls.Add(Me.lbkNewRtg)
        Me.tpIncentiveRoutings.Controls.Add(Me.blkClearRtg)
        Me.tpIncentiveRoutings.Controls.Add(Me.lbkSearchRtg)
        Me.tpIncentiveRoutings.Controls.Add(Me.cboStatusRtg)
        Me.tpIncentiveRoutings.Controls.Add(Me.Label5)
        Me.tpIncentiveRoutings.Controls.Add(Me.cboCarRtg)
        Me.tpIncentiveRoutings.Controls.Add(Me.Label6)
        Me.tpIncentiveRoutings.Controls.Add(Me.dgrIncentiveRtg)
        Me.tpIncentiveRoutings.Location = New System.Drawing.Point(4, 22)
        Me.tpIncentiveRoutings.Name = "tpIncentiveRoutings"
        Me.tpIncentiveRoutings.Padding = New System.Windows.Forms.Padding(3)
        Me.tpIncentiveRoutings.Size = New System.Drawing.Size(999, 584)
        Me.tpIncentiveRoutings.TabIndex = 1
        Me.tpIncentiveRoutings.Text = "IncentiveRoutings"
        Me.tpIncentiveRoutings.UseVisualStyleBackColor = True
        '
        'lbkCloneCities_vv
        '
        Me.lbkCloneCities_vv.AutoSize = True
        Me.lbkCloneCities_vv.Location = New System.Drawing.Point(74, 566)
        Me.lbkCloneCities_vv.Name = "lbkCloneCities_vv"
        Me.lbkCloneCities_vv.Size = New System.Drawing.Size(77, 13)
        Me.lbkCloneCities_vv.TabIndex = 22
        Me.lbkCloneCities_vv.TabStop = True
        Me.lbkCloneCities_vv.Text = "CloneCities_vv"
        '
        'lbkEditRtg
        '
        Me.lbkEditRtg.AutoSize = True
        Me.lbkEditRtg.Location = New System.Drawing.Point(43, 566)
        Me.lbkEditRtg.Name = "lbkEditRtg"
        Me.lbkEditRtg.Size = New System.Drawing.Size(25, 13)
        Me.lbkEditRtg.TabIndex = 21
        Me.lbkEditRtg.TabStop = True
        Me.lbkEditRtg.Text = "Edit"
        '
        'lbkNewRtg
        '
        Me.lbkNewRtg.AutoSize = True
        Me.lbkNewRtg.Location = New System.Drawing.Point(6, 566)
        Me.lbkNewRtg.Name = "lbkNewRtg"
        Me.lbkNewRtg.Size = New System.Drawing.Size(29, 13)
        Me.lbkNewRtg.TabIndex = 20
        Me.lbkNewRtg.TabStop = True
        Me.lbkNewRtg.Text = "New"
        '
        'blkClearRtg
        '
        Me.blkClearRtg.AutoSize = True
        Me.blkClearRtg.Location = New System.Drawing.Point(390, 24)
        Me.blkClearRtg.Name = "blkClearRtg"
        Me.blkClearRtg.Size = New System.Drawing.Size(31, 13)
        Me.blkClearRtg.TabIndex = 19
        Me.blkClearRtg.TabStop = True
        Me.blkClearRtg.Text = "Clear"
        '
        'lbkSearchRtg
        '
        Me.lbkSearchRtg.AutoSize = True
        Me.lbkSearchRtg.Location = New System.Drawing.Point(322, 22)
        Me.lbkSearchRtg.Name = "lbkSearchRtg"
        Me.lbkSearchRtg.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearchRtg.TabIndex = 18
        Me.lbkSearchRtg.TabStop = True
        Me.lbkSearchRtg.Text = "Search"
        '
        'cboStatusRtg
        '
        Me.cboStatusRtg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatusRtg.FormattingEnabled = True
        Me.cboStatusRtg.Items.AddRange(New Object() {"OK", "XX"})
        Me.cboStatusRtg.Location = New System.Drawing.Point(60, 16)
        Me.cboStatusRtg.Name = "cboStatusRtg"
        Me.cboStatusRtg.Size = New System.Drawing.Size(53, 21)
        Me.cboStatusRtg.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(57, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Status"
        '
        'cboCarRtg
        '
        Me.cboCarRtg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCarRtg.FormattingEnabled = True
        Me.cboCarRtg.Location = New System.Drawing.Point(4, 16)
        Me.cboCarRtg.Name = "cboCarRtg"
        Me.cboCarRtg.Size = New System.Drawing.Size(53, 21)
        Me.cboCarRtg.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Car"
        '
        'dgrIncentiveRtg
        '
        Me.dgrIncentiveRtg.AllowUserToAddRows = False
        Me.dgrIncentiveRtg.AllowUserToDeleteRows = False
        Me.dgrIncentiveRtg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrIncentiveRtg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrIncentiveRtg.Location = New System.Drawing.Point(0, 49)
        Me.dgrIncentiveRtg.Name = "dgrIncentiveRtg"
        Me.dgrIncentiveRtg.ReadOnly = True
        Me.dgrIncentiveRtg.Size = New System.Drawing.Size(999, 500)
        Me.dgrIncentiveRtg.TabIndex = 0
        '
        'tpReport
        '
        Me.tpReport.Controls.Add(Me.cboCounter)
        Me.tpReport.Controls.Add(Me.Counter)
        Me.tpReport.Controls.Add(Me.lbkReport2Excel)
        Me.tpReport.Controls.Add(Me.cboDateTypeRpt)
        Me.tpReport.Controls.Add(Me.Label8)
        Me.tpReport.Controls.Add(Me.cboCarRpt)
        Me.tpReport.Controls.Add(Me.dtpToDate)
        Me.tpReport.Controls.Add(Me.dtpFromDate)
        Me.tpReport.Controls.Add(Me.Label10)
        Me.tpReport.Controls.Add(Me.Label9)
        Me.tpReport.Controls.Add(Me.Label4)
        Me.tpReport.Location = New System.Drawing.Point(4, 22)
        Me.tpReport.Name = "tpReport"
        Me.tpReport.Padding = New System.Windows.Forms.Padding(3)
        Me.tpReport.Size = New System.Drawing.Size(999, 584)
        Me.tpReport.TabIndex = 2
        Me.tpReport.Text = "Report"
        Me.tpReport.UseVisualStyleBackColor = True
        '
        'cboCounter
        '
        Me.cboCounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCounter.FormattingEnabled = True
        Me.cboCounter.Items.AddRange(New Object() {"CWT", "TVS"})
        Me.cboCounter.Location = New System.Drawing.Point(9, 29)
        Me.cboCounter.Name = "cboCounter"
        Me.cboCounter.Size = New System.Drawing.Size(60, 21)
        Me.cboCounter.TabIndex = 0
        '
        'Counter
        '
        Me.Counter.AutoSize = True
        Me.Counter.Location = New System.Drawing.Point(6, 13)
        Me.Counter.Name = "Counter"
        Me.Counter.Size = New System.Drawing.Size(44, 13)
        Me.Counter.TabIndex = 48
        Me.Counter.Text = "Counter"
        '
        'lbkReport2Excel
        '
        Me.lbkReport2Excel.AutoSize = True
        Me.lbkReport2Excel.Location = New System.Drawing.Point(431, 33)
        Me.lbkReport2Excel.Name = "lbkReport2Excel"
        Me.lbkReport2Excel.Size = New System.Drawing.Size(71, 13)
        Me.lbkReport2Excel.TabIndex = 5
        Me.lbkReport2Excel.TabStop = True
        Me.lbkReport2Excel.Text = "Report2Excel"
        '
        'cboDateTypeRpt
        '
        Me.cboDateTypeRpt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDateTypeRpt.FormattingEnabled = True
        Me.cboDateTypeRpt.Location = New System.Drawing.Point(126, 27)
        Me.cboDateTypeRpt.Name = "cboDateTypeRpt"
        Me.cboDateTypeRpt.Size = New System.Drawing.Size(60, 21)
        Me.cboDateTypeRpt.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(123, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 13)
        Me.Label8.TabIndex = 45
        Me.Label8.Text = "DateType"
        '
        'cboCarRpt
        '
        Me.cboCarRpt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCarRpt.FormattingEnabled = True
        Me.cboCarRpt.Location = New System.Drawing.Point(74, 28)
        Me.cboCarRpt.Name = "cboCarRpt"
        Me.cboCarRpt.Size = New System.Drawing.Size(48, 21)
        Me.cboCarRpt.TabIndex = 1
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd MMM yy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(296, 28)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(98, 20)
        Me.dtpToDate.TabIndex = 4
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd MMM yy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(192, 27)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(98, 20)
        Me.dtpFromDate.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(294, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(43, 13)
        Me.Label10.TabIndex = 41
        Me.Label10.Text = "ToDate"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(196, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 13)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "FromDate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(77, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(23, 13)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Car"
        '
        'frmAlIncentives
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1009, 611)
        Me.Controls.Add(Me.tcIncentives)
        Me.Name = "frmAlIncentives"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Airline Incentives"
        Me.tcIncentives.ResumeLayout(False)
        Me.tpIncentiveFormula.ResumeLayout(False)
        Me.tpIncentiveFormula.PerformLayout()
        CType(Me.dgrIncentiveFormula, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpIncentiveRoutings.ResumeLayout(False)
        Me.tpIncentiveRoutings.PerformLayout()
        CType(Me.dgrIncentiveRtg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpReport.ResumeLayout(False)
        Me.tpReport.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tcIncentives As TabControl
    Friend WithEvents tpIncentiveFormula As TabPage
    Friend WithEvents tpIncentiveRoutings As TabPage
    Friend WithEvents dgrIncentiveRtg As DataGridView
    Friend WithEvents dgrIncentiveFormula As DataGridView
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboCar As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpValidOn As DateTimePicker
    Friend WithEvents cboDateType As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents tpReport As TabPage
    Friend WithEvents lbkEditRtg As LinkLabel
    Friend WithEvents lbkNewRtg As LinkLabel
    Friend WithEvents blkClearRtg As LinkLabel
    Friend WithEvents lbkSearchRtg As LinkLabel
    Friend WithEvents cboStatusRtg As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cboCarRtg As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents lbkCloneCities_vv As LinkLabel
    Friend WithEvents lbkClone As LinkLabel
    Friend WithEvents cboCarRpt As ComboBox
    Friend WithEvents dtpToDate As DateTimePicker
    Friend WithEvents dtpFromDate As DateTimePicker
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents cboDateTypeRpt As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents lbkReport2Excel As LinkLabel
    Friend WithEvents cboCounter As ComboBox
    Friend WithEvents Counter As Label
End Class
