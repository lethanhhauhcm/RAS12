<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorBalance
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboVendor = New System.Windows.Forms.ComboBox()
        Me.cboAPP = New System.Windows.Forms.ComboBox()
        Me.txtTcode = New System.Windows.Forms.TextBox()
        Me.chkTrxBetween = New System.Windows.Forms.CheckBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboView = New System.Windows.Forms.ComboBox()
        Me.lbkAdd = New System.Windows.Forms.LinkLabel()
        Me.dtpCutOffDate = New System.Windows.Forms.DateTimePicker()
        Me.txtCutOffAmt = New System.Windows.Forms.TextBox()
        Me.cboCutOffVendor = New System.Windows.Forms.ComboBox()
        Me.lbkViewDetails = New System.Windows.Forms.LinkLabel()
        Me.cboCur = New System.Windows.Forms.ComboBox()
        Me.cboFOP = New System.Windows.Forms.ComboBox()
        Me.cboTVC = New System.Windows.Forms.ComboBox()
        Me.dgrVendorBalanceGDS = New System.Windows.Forms.DataGridView()
        Me.tcTVC = New System.Windows.Forms.TabControl()
        Me.tpTVTR = New System.Windows.Forms.TabPage()
        Me.tpGDS = New System.Windows.Forms.TabPage()
        Me.dgrVendorBalanceTVTR = New System.Windows.Forms.DataGridView()
        CType(Me.dgrVendorBalanceGDS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcTVC.SuspendLayout()
        Me.tpTVTR.SuspendLayout()
        Me.tpGDS.SuspendLayout()
        CType(Me.dgrVendorBalanceTVTR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(111, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Vendor"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(389, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Tcode"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(550, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "APP"
        '
        'cboVendor
        '
        Me.cboVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVendor.FormattingEnabled = True
        Me.cboVendor.Location = New System.Drawing.Point(114, 29)
        Me.cboVendor.Name = "cboVendor"
        Me.cboVendor.Size = New System.Drawing.Size(273, 21)
        Me.cboVendor.TabIndex = 5
        '
        'cboAPP
        '
        Me.cboAPP.FormattingEnabled = True
        Me.cboAPP.Items.AddRange(New Object() {"RAS", "OPS"})
        Me.cboAPP.Location = New System.Drawing.Point(553, 29)
        Me.cboAPP.Name = "cboAPP"
        Me.cboAPP.Size = New System.Drawing.Size(45, 21)
        Me.cboAPP.TabIndex = 6
        '
        'txtTcode
        '
        Me.txtTcode.Location = New System.Drawing.Point(393, 30)
        Me.txtTcode.Name = "txtTcode"
        Me.txtTcode.Size = New System.Drawing.Size(154, 20)
        Me.txtTcode.TabIndex = 7
        '
        'chkTrxBetween
        '
        Me.chkTrxBetween.AutoSize = True
        Me.chkTrxBetween.Location = New System.Drawing.Point(613, 12)
        Me.chkTrxBetween.Name = "chkTrxBetween"
        Me.chkTrxBetween.Size = New System.Drawing.Size(86, 17)
        Me.chkTrxBetween.TabIndex = 8
        Me.chkTrxBetween.Text = "Trx Between"
        Me.chkTrxBetween.UseVisualStyleBackColor = True
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd MMM yy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(613, 31)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(86, 20)
        Me.dtpFrom.TabIndex = 9
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd MMM yy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(705, 31)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(90, 20)
        Me.dtpTo.TabIndex = 10
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(795, 37)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 11
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(842, 37)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 12
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(-2, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "View"
        '
        'cboView
        '
        Me.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboView.FormattingEnabled = True
        Me.cboView.Items.AddRange(New Object() {"LastBalance", "Transactions"})
        Me.cboView.Location = New System.Drawing.Point(1, 29)
        Me.cboView.Name = "cboView"
        Me.cboView.Size = New System.Drawing.Size(107, 21)
        Me.cboView.TabIndex = 19
        '
        'lbkAdd
        '
        Me.lbkAdd.AutoSize = True
        Me.lbkAdd.Location = New System.Drawing.Point(762, 540)
        Me.lbkAdd.Name = "lbkAdd"
        Me.lbkAdd.Size = New System.Drawing.Size(26, 13)
        Me.lbkAdd.TabIndex = 22
        Me.lbkAdd.TabStop = True
        Me.lbkAdd.Text = "Add"
        '
        'dtpCutOffDate
        '
        Me.dtpCutOffDate.CustomFormat = "dd MMM yy"
        Me.dtpCutOffDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCutOffDate.Location = New System.Drawing.Point(450, 537)
        Me.dtpCutOffDate.Name = "dtpCutOffDate"
        Me.dtpCutOffDate.Size = New System.Drawing.Size(71, 20)
        Me.dtpCutOffDate.TabIndex = 21
        '
        'txtCutOffAmt
        '
        Me.txtCutOffAmt.Location = New System.Drawing.Point(580, 537)
        Me.txtCutOffAmt.Name = "txtCutOffAmt"
        Me.txtCutOffAmt.Size = New System.Drawing.Size(100, 20)
        Me.txtCutOffAmt.TabIndex = 23
        '
        'cboCutOffVendor
        '
        Me.cboCutOffVendor.FormattingEnabled = True
        Me.cboCutOffVendor.Location = New System.Drawing.Point(99, 537)
        Me.cboCutOffVendor.Name = "cboCutOffVendor"
        Me.cboCutOffVendor.Size = New System.Drawing.Size(256, 21)
        Me.cboCutOffVendor.TabIndex = 24
        '
        'lbkViewDetails
        '
        Me.lbkViewDetails.AutoSize = True
        Me.lbkViewDetails.Location = New System.Drawing.Point(12, 542)
        Me.lbkViewDetails.Name = "lbkViewDetails"
        Me.lbkViewDetails.Size = New System.Drawing.Size(62, 13)
        Me.lbkViewDetails.TabIndex = 25
        Me.lbkViewDetails.TabStop = True
        Me.lbkViewDetails.Text = "ViewDetails"
        '
        'cboCur
        '
        Me.cboCur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCur.FormattingEnabled = True
        Me.cboCur.Location = New System.Drawing.Point(527, 536)
        Me.cboCur.Name = "cboCur"
        Me.cboCur.Size = New System.Drawing.Size(47, 21)
        Me.cboCur.TabIndex = 27
        '
        'cboFOP
        '
        Me.cboFOP.FormattingEnabled = True
        Me.cboFOP.Items.AddRange(New Object() {"BTF", "CSH", "COF"})
        Me.cboFOP.Location = New System.Drawing.Point(686, 536)
        Me.cboFOP.Name = "cboFOP"
        Me.cboFOP.Size = New System.Drawing.Size(47, 21)
        Me.cboFOP.TabIndex = 28
        '
        'cboTVC
        '
        Me.cboTVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTVC.FormattingEnabled = True
        Me.cboTVC.Items.AddRange(New Object() {"TVTR", "GDS"})
        Me.cboTVC.Location = New System.Drawing.Point(361, 537)
        Me.cboTVC.Name = "cboTVC"
        Me.cboTVC.Size = New System.Drawing.Size(83, 21)
        Me.cboTVC.TabIndex = 29
        '
        'dgrVendorBalanceGDS
        '
        Me.dgrVendorBalanceGDS.AllowUserToAddRows = False
        Me.dgrVendorBalanceGDS.AllowUserToDeleteRows = False
        Me.dgrVendorBalanceGDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrVendorBalanceGDS.Location = New System.Drawing.Point(10, 6)
        Me.dgrVendorBalanceGDS.Name = "dgrVendorBalanceGDS"
        Me.dgrVendorBalanceGDS.ReadOnly = True
        Me.dgrVendorBalanceGDS.Size = New System.Drawing.Size(1000, 431)
        Me.dgrVendorBalanceGDS.TabIndex = 30
        '
        'tcTVC
        '
        Me.tcTVC.Controls.Add(Me.tpGDS)
        Me.tcTVC.Controls.Add(Me.tpTVTR)
        Me.tcTVC.Location = New System.Drawing.Point(1, 56)
        Me.tcTVC.Name = "tcTVC"
        Me.tcTVC.SelectedIndex = 0
        Me.tcTVC.Size = New System.Drawing.Size(1008, 454)
        Me.tcTVC.TabIndex = 31
        '
        'tpTVTR
        '
        Me.tpTVTR.Controls.Add(Me.dgrVendorBalanceTVTR)
        Me.tpTVTR.Location = New System.Drawing.Point(4, 22)
        Me.tpTVTR.Name = "tpTVTR"
        Me.tpTVTR.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVTR.Size = New System.Drawing.Size(1000, 428)
        Me.tpTVTR.TabIndex = 0
        Me.tpTVTR.Text = "TVTR"
        Me.tpTVTR.UseVisualStyleBackColor = True
        '
        'tpGDS
        '
        Me.tpGDS.Controls.Add(Me.dgrVendorBalanceGDS)
        Me.tpGDS.Location = New System.Drawing.Point(4, 22)
        Me.tpGDS.Name = "tpGDS"
        Me.tpGDS.Padding = New System.Windows.Forms.Padding(3)
        Me.tpGDS.Size = New System.Drawing.Size(1000, 428)
        Me.tpGDS.TabIndex = 1
        Me.tpGDS.Text = "GDS"
        Me.tpGDS.UseVisualStyleBackColor = True
        '
        'dgrVendorBalanceTVTR
        '
        Me.dgrVendorBalanceTVTR.AllowUserToAddRows = False
        Me.dgrVendorBalanceTVTR.AllowUserToDeleteRows = False
        Me.dgrVendorBalanceTVTR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrVendorBalanceTVTR.Location = New System.Drawing.Point(0, 3)
        Me.dgrVendorBalanceTVTR.Name = "dgrVendorBalanceTVTR"
        Me.dgrVendorBalanceTVTR.ReadOnly = True
        Me.dgrVendorBalanceTVTR.Size = New System.Drawing.Size(988, 422)
        Me.dgrVendorBalanceTVTR.TabIndex = 0
        '
        'frmVendorBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 561)
        Me.Controls.Add(Me.tcTVC)
        Me.Controls.Add(Me.cboTVC)
        Me.Controls.Add(Me.cboFOP)
        Me.Controls.Add(Me.cboCur)
        Me.Controls.Add(Me.lbkViewDetails)
        Me.Controls.Add(Me.cboCutOffVendor)
        Me.Controls.Add(Me.txtCutOffAmt)
        Me.Controls.Add(Me.lbkAdd)
        Me.Controls.Add(Me.dtpCutOffDate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboView)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.chkTrxBetween)
        Me.Controls.Add(Me.txtTcode)
        Me.Controls.Add(Me.cboAPP)
        Me.Controls.Add(Me.cboVendor)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmVendorBalance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VendorBalance"
        CType(Me.dgrVendorBalanceGDS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcTVC.ResumeLayout(False)
        Me.tpTVTR.ResumeLayout(False)
        Me.tpGDS.ResumeLayout(False)
        CType(Me.dgrVendorBalanceTVTR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents cboVendor As ComboBox
    Friend WithEvents cboAPP As ComboBox
    Friend WithEvents txtTcode As TextBox
    Friend WithEvents chkTrxBetween As CheckBox
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents Label3 As Label
    Friend WithEvents cboView As ComboBox
    Friend WithEvents lbkAdd As LinkLabel
    Friend WithEvents dtpCutOffDate As DateTimePicker
    Friend WithEvents txtCutOffAmt As TextBox
    Friend WithEvents cboCutOffVendor As ComboBox
    Friend WithEvents lbkViewDetails As LinkLabel
    Friend WithEvents cboCur As ComboBox
    Friend WithEvents cboFOP As ComboBox
    Friend WithEvents cboTVC As ComboBox
    Friend WithEvents dgrVendorBalanceGDS As DataGridView
    Friend WithEvents tcTVC As TabControl
    Friend WithEvents tpTVTR As TabPage
    Friend WithEvents tpGDS As TabPage
    Friend WithEvents dgrVendorBalanceTVTR As DataGridView
End Class
