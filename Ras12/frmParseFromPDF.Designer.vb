<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParseFromPDF
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtRloc = New System.Windows.Forms.TextBox()
        Me.txtTotalFare = New System.Windows.Forms.TextBox()
        Me.txtTotalTax = New System.Windows.Forms.TextBox()
        Me.txtTotalVat = New System.Windows.Forms.TextBox()
        Me.txtGrandTotal = New System.Windows.Forms.TextBox()
        Me.dtpDOI = New System.Windows.Forms.DateTimePicker()
        Me.dgrTkts = New System.Windows.Forms.DataGridView()
        Me.TKNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaxName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaxType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FareBasis = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BkgCls = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fare = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KickBackAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MiscFeeAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChargeTV = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BIZ = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.BOOKER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dtpDOF = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dgrSegs = New System.Windows.Forms.DataGridView()
        Me.FromCity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToCity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Car = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FltNbr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FltDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ETD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ETA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbkPush2RAS = New System.Windows.Forms.LinkLabel()
        Me.wb = New System.Windows.Forms.WebBrowser()
        Me.txtCar = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lbkShowContent = New System.Windows.Forms.LinkLabel()
        Me.grbMassUpdate = New System.Windows.Forms.GroupBox()
        Me.lbkUpdateBooker = New System.Windows.Forms.LinkLabel()
        Me.ChkBizTrip = New System.Windows.Forms.CheckBox()
        Me.lbkUpdtNumeric = New System.Windows.Forms.LinkLabel()
        Me.cboBooker = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtNewNumeric = New System.Windows.Forms.TextBox()
        Me.cboPaxType = New System.Windows.Forms.ComboBox()
        Me.cboFieldName = New System.Windows.Forms.ComboBox()
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrSegs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbMassUpdate.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(44, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Rloc"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(97, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "DOI"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(286, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "TotalFare"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(390, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "TotalTax"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(496, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "TotalVAT"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(602, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "GrandTotal"
        '
        'txtRloc
        '
        Me.txtRloc.Location = New System.Drawing.Point(47, 16)
        Me.txtRloc.Name = "txtRloc"
        Me.txtRloc.Size = New System.Drawing.Size(49, 20)
        Me.txtRloc.TabIndex = 6
        '
        'txtTotalFare
        '
        Me.txtTotalFare.Enabled = False
        Me.txtTotalFare.Location = New System.Drawing.Point(289, 16)
        Me.txtTotalFare.Name = "txtTotalFare"
        Me.txtTotalFare.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalFare.TabIndex = 7
        '
        'txtTotalTax
        '
        Me.txtTotalTax.Enabled = False
        Me.txtTotalTax.Location = New System.Drawing.Point(393, 16)
        Me.txtTotalTax.Name = "txtTotalTax"
        Me.txtTotalTax.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalTax.TabIndex = 8
        '
        'txtTotalVat
        '
        Me.txtTotalVat.Enabled = False
        Me.txtTotalVat.Location = New System.Drawing.Point(499, 16)
        Me.txtTotalVat.Name = "txtTotalVat"
        Me.txtTotalVat.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalVat.TabIndex = 9
        '
        'txtGrandTotal
        '
        Me.txtGrandTotal.Enabled = False
        Me.txtGrandTotal.Location = New System.Drawing.Point(605, 16)
        Me.txtGrandTotal.Name = "txtGrandTotal"
        Me.txtGrandTotal.Size = New System.Drawing.Size(100, 20)
        Me.txtGrandTotal.TabIndex = 10
        '
        'dtpDOI
        '
        Me.dtpDOI.CustomFormat = "dd MMM yy"
        Me.dtpDOI.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOI.Location = New System.Drawing.Point(100, 16)
        Me.dtpDOI.Name = "dtpDOI"
        Me.dtpDOI.Size = New System.Drawing.Size(86, 20)
        Me.dtpDOI.TabIndex = 11
        '
        'dgrTkts
        '
        Me.dgrTkts.AllowUserToAddRows = False
        Me.dgrTkts.AllowUserToDeleteRows = False
        Me.dgrTkts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrTkts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTkts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TKNO, Me.PaxName, Me.PaxType, Me.FareBasis, Me.BkgCls, Me.Fare, Me.Tax, Me.VAT, Me.KickBackAmt, Me.MiscFeeAmt, Me.ChargeTV, Me.BIZ, Me.BOOKER})
        Me.dgrTkts.Location = New System.Drawing.Point(3, 175)
        Me.dgrTkts.Name = "dgrTkts"
        Me.dgrTkts.Size = New System.Drawing.Size(1006, 391)
        Me.dgrTkts.TabIndex = 12
        '
        'TKNO
        '
        Me.TKNO.HeaderText = "TKNO"
        Me.TKNO.Name = "TKNO"
        Me.TKNO.Width = 62
        '
        'PaxName
        '
        Me.PaxName.HeaderText = "PaxName"
        Me.PaxName.Name = "PaxName"
        Me.PaxName.Width = 78
        '
        'PaxType
        '
        Me.PaxType.HeaderText = "PaxType"
        Me.PaxType.Name = "PaxType"
        Me.PaxType.Width = 74
        '
        'FareBasis
        '
        Me.FareBasis.HeaderText = "FareBasis"
        Me.FareBasis.Name = "FareBasis"
        Me.FareBasis.Width = 78
        '
        'BkgCls
        '
        Me.BkgCls.HeaderText = "BkgCls"
        Me.BkgCls.Name = "BkgCls"
        Me.BkgCls.Width = 65
        '
        'Fare
        '
        Me.Fare.HeaderText = "Fare"
        Me.Fare.Name = "Fare"
        Me.Fare.Width = 53
        '
        'Tax
        '
        Me.Tax.HeaderText = "Tax"
        Me.Tax.Name = "Tax"
        Me.Tax.Width = 50
        '
        'VAT
        '
        Me.VAT.HeaderText = "VAT"
        Me.VAT.Name = "VAT"
        Me.VAT.Width = 53
        '
        'KickBackAmt
        '
        Me.KickBackAmt.HeaderText = "KickBackAmt"
        Me.KickBackAmt.Name = "KickBackAmt"
        Me.KickBackAmt.Width = 96
        '
        'MiscFeeAmt
        '
        Me.MiscFeeAmt.HeaderText = "MiscFeeAmt"
        Me.MiscFeeAmt.Name = "MiscFeeAmt"
        Me.MiscFeeAmt.Width = 90
        '
        'ChargeTV
        '
        Me.ChargeTV.HeaderText = "ChargeTV"
        Me.ChargeTV.Name = "ChargeTV"
        Me.ChargeTV.Width = 80
        '
        'BIZ
        '
        Me.BIZ.HeaderText = "BIZ"
        Me.BIZ.Name = "BIZ"
        Me.BIZ.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BIZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.BIZ.Width = 49
        '
        'BOOKER
        '
        Me.BOOKER.HeaderText = "BOOKER"
        Me.BOOKER.Name = "BOOKER"
        Me.BOOKER.Width = 77
        '
        'dtpDOF
        '
        Me.dtpDOF.CustomFormat = "dd MMM yy"
        Me.dtpDOF.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOF.Location = New System.Drawing.Point(192, 16)
        Me.dtpDOF.Name = "dtpDOF"
        Me.dtpDOF.Size = New System.Drawing.Size(86, 20)
        Me.dtpDOF.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(189, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "DOF"
        '
        'dgrSegs
        '
        Me.dgrSegs.AllowUserToAddRows = False
        Me.dgrSegs.AllowUserToDeleteRows = False
        Me.dgrSegs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrSegs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrSegs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FromCity, Me.ToCity, Me.Car, Me.FltNbr, Me.FltDate, Me.ETD, Me.ETA})
        Me.dgrSegs.Location = New System.Drawing.Point(3, 42)
        Me.dgrSegs.Name = "dgrSegs"
        Me.dgrSegs.Size = New System.Drawing.Size(606, 127)
        Me.dgrSegs.TabIndex = 15
        '
        'FromCity
        '
        Me.FromCity.HeaderText = "FromCity"
        Me.FromCity.Name = "FromCity"
        Me.FromCity.Width = 72
        '
        'ToCity
        '
        Me.ToCity.HeaderText = "ToCity"
        Me.ToCity.Name = "ToCity"
        Me.ToCity.Width = 62
        '
        'Car
        '
        Me.Car.HeaderText = "Car"
        Me.Car.Name = "Car"
        Me.Car.Width = 48
        '
        'FltNbr
        '
        Me.FltNbr.HeaderText = "FltNbr"
        Me.FltNbr.Name = "FltNbr"
        Me.FltNbr.Width = 60
        '
        'FltDate
        '
        Me.FltDate.HeaderText = "FltDate"
        Me.FltDate.Name = "FltDate"
        Me.FltDate.Width = 66
        '
        'ETD
        '
        Me.ETD.HeaderText = "ETD"
        Me.ETD.Name = "ETD"
        Me.ETD.Width = 54
        '
        'ETA
        '
        Me.ETA.HeaderText = "ETA"
        Me.ETA.Name = "ETA"
        Me.ETA.Width = 53
        '
        'lbkPush2RAS
        '
        Me.lbkPush2RAS.AutoSize = True
        Me.lbkPush2RAS.Location = New System.Drawing.Point(711, 22)
        Me.lbkPush2RAS.Name = "lbkPush2RAS"
        Me.lbkPush2RAS.Size = New System.Drawing.Size(59, 13)
        Me.lbkPush2RAS.TabIndex = 16
        Me.lbkPush2RAS.TabStop = True
        Me.lbkPush2RAS.Text = "Push2RAS"
        '
        'wb
        '
        Me.wb.Location = New System.Drawing.Point(3, 218)
        Me.wb.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wb.Name = "wb"
        Me.wb.Size = New System.Drawing.Size(1006, 381)
        Me.wb.TabIndex = 50
        Me.wb.Visible = False
        '
        'txtCar
        '
        Me.txtCar.Enabled = False
        Me.txtCar.Location = New System.Drawing.Point(3, 16)
        Me.txtCar.Name = "txtCar"
        Me.txtCar.Size = New System.Drawing.Size(38, 20)
        Me.txtCar.TabIndex = 52
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 13)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Car"
        '
        'lbkShowContent
        '
        Me.lbkShowContent.AutoSize = True
        Me.lbkShowContent.Location = New System.Drawing.Point(938, 23)
        Me.lbkShowContent.Name = "lbkShowContent"
        Me.lbkShowContent.Size = New System.Drawing.Size(71, 13)
        Me.lbkShowContent.TabIndex = 53
        Me.lbkShowContent.TabStop = True
        Me.lbkShowContent.Text = "ShowContent"
        '
        'grbMassUpdate
        '
        Me.grbMassUpdate.Controls.Add(Me.lbkUpdateBooker)
        Me.grbMassUpdate.Controls.Add(Me.ChkBizTrip)
        Me.grbMassUpdate.Controls.Add(Me.lbkUpdtNumeric)
        Me.grbMassUpdate.Controls.Add(Me.cboBooker)
        Me.grbMassUpdate.Controls.Add(Me.Label9)
        Me.grbMassUpdate.Controls.Add(Me.txtNewNumeric)
        Me.grbMassUpdate.Controls.Add(Me.cboPaxType)
        Me.grbMassUpdate.Controls.Add(Me.cboFieldName)
        Me.grbMassUpdate.Location = New System.Drawing.Point(615, 42)
        Me.grbMassUpdate.Name = "grbMassUpdate"
        Me.grbMassUpdate.Size = New System.Drawing.Size(394, 127)
        Me.grbMassUpdate.TabIndex = 54
        Me.grbMassUpdate.TabStop = False
        Me.grbMassUpdate.Text = "MassUpdate"
        '
        'lbkUpdateBooker
        '
        Me.lbkUpdateBooker.AutoSize = True
        Me.lbkUpdateBooker.Location = New System.Drawing.Point(329, 46)
        Me.lbkUpdateBooker.Name = "lbkUpdateBooker"
        Me.lbkUpdateBooker.Size = New System.Drawing.Size(58, 13)
        Me.lbkUpdateBooker.TabIndex = 55
        Me.lbkUpdateBooker.TabStop = True
        Me.lbkUpdateBooker.Text = "UpdateBkr"
        '
        'ChkBizTrip
        '
        Me.ChkBizTrip.AutoSize = True
        Me.ChkBizTrip.Checked = True
        Me.ChkBizTrip.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkBizTrip.Location = New System.Drawing.Point(271, 43)
        Me.ChkBizTrip.Name = "ChkBizTrip"
        Me.ChkBizTrip.Size = New System.Drawing.Size(58, 17)
        Me.ChkBizTrip.TabIndex = 57
        Me.ChkBizTrip.Text = "BizTrip"
        Me.ChkBizTrip.UseVisualStyleBackColor = True
        '
        'lbkUpdtNumeric
        '
        Me.lbkUpdtNumeric.AutoSize = True
        Me.lbkUpdtNumeric.Location = New System.Drawing.Point(329, 19)
        Me.lbkUpdtNumeric.Name = "lbkUpdtNumeric"
        Me.lbkUpdtNumeric.Size = New System.Drawing.Size(42, 13)
        Me.lbkUpdtNumeric.TabIndex = 17
        Me.lbkUpdtNumeric.TabStop = True
        Me.lbkUpdtNumeric.Text = "Update"
        '
        'cboBooker
        '
        Me.cboBooker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBooker.FormattingEnabled = True
        Me.cboBooker.Location = New System.Drawing.Point(44, 43)
        Me.cboBooker.Name = "cboBooker"
        Me.cboBooker.Size = New System.Drawing.Size(211, 21)
        Me.cboBooker.TabIndex = 56
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(2, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 55
        Me.Label9.Text = "Booker"
        '
        'txtNewNumeric
        '
        Me.txtNewNumeric.Location = New System.Drawing.Point(187, 16)
        Me.txtNewNumeric.Name = "txtNewNumeric"
        Me.txtNewNumeric.Size = New System.Drawing.Size(126, 20)
        Me.txtNewNumeric.TabIndex = 9
        '
        'cboPaxType
        '
        Me.cboPaxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaxType.FormattingEnabled = True
        Me.cboPaxType.Location = New System.Drawing.Point(120, 16)
        Me.cboPaxType.Name = "cboPaxType"
        Me.cboPaxType.Size = New System.Drawing.Size(61, 21)
        Me.cboPaxType.TabIndex = 1
        '
        'cboFieldName
        '
        Me.cboFieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFieldName.FormattingEnabled = True
        Me.cboFieldName.Items.AddRange(New Object() {"VAT", "KickBackAmt", "MiscFeeAmt", "ChargeTV"})
        Me.cboFieldName.Location = New System.Drawing.Point(3, 16)
        Me.cboFieldName.Name = "cboFieldName"
        Me.cboFieldName.Size = New System.Drawing.Size(111, 21)
        Me.cboFieldName.TabIndex = 0
        '
        'ParseFromPDF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.grbMassUpdate)
        Me.Controls.Add(Me.lbkShowContent)
        Me.Controls.Add(Me.txtCar)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lbkPush2RAS)
        Me.Controls.Add(Me.dgrSegs)
        Me.Controls.Add(Me.dtpDOF)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dgrTkts)
        Me.Controls.Add(Me.dtpDOI)
        Me.Controls.Add(Me.txtGrandTotal)
        Me.Controls.Add(Me.txtTotalVat)
        Me.Controls.Add(Me.txtTotalTax)
        Me.Controls.Add(Me.txtTotalFare)
        Me.Controls.Add(Me.txtRloc)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.wb)
        Me.Name = "ParseFromPDF"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ParseFromPDF"
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrSegs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbMassUpdate.ResumeLayout(False)
        Me.grbMassUpdate.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtRloc As TextBox
    Friend WithEvents txtTotalFare As TextBox
    Friend WithEvents txtTotalTax As TextBox
    Friend WithEvents txtTotalVat As TextBox
    Friend WithEvents txtGrandTotal As TextBox
    Friend WithEvents dtpDOI As DateTimePicker
    Friend WithEvents dgrTkts As DataGridView
    Friend WithEvents dtpDOF As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents dgrSegs As DataGridView
    Friend WithEvents FromCity As DataGridViewTextBoxColumn
    Friend WithEvents ToCity As DataGridViewTextBoxColumn
    Friend WithEvents Car As DataGridViewTextBoxColumn
    Friend WithEvents FltNbr As DataGridViewTextBoxColumn
    Friend WithEvents FltDate As DataGridViewTextBoxColumn
    Friend WithEvents lbkPush2RAS As LinkLabel
    Friend WithEvents ETD As DataGridViewTextBoxColumn
    Friend WithEvents ETA As DataGridViewTextBoxColumn
    Friend WithEvents wb As WebBrowser
    Friend WithEvents txtCar As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents lbkShowContent As LinkLabel
    Friend WithEvents grbMassUpdate As GroupBox
    Friend WithEvents cboPaxType As ComboBox
    Friend WithEvents cboFieldName As ComboBox
    Friend WithEvents txtNewNumeric As TextBox
    Friend WithEvents lbkUpdtNumeric As LinkLabel
    Friend WithEvents TKNO As DataGridViewTextBoxColumn
    Friend WithEvents PaxName As DataGridViewTextBoxColumn
    Friend WithEvents PaxType As DataGridViewTextBoxColumn
    Friend WithEvents FareBasis As DataGridViewTextBoxColumn
    Friend WithEvents BkgCls As DataGridViewTextBoxColumn
    Friend WithEvents Fare As DataGridViewTextBoxColumn
    Friend WithEvents Tax As DataGridViewTextBoxColumn
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents KickBackAmt As DataGridViewTextBoxColumn
    Friend WithEvents MiscFeeAmt As DataGridViewTextBoxColumn
    Friend WithEvents ChargeTV As DataGridViewTextBoxColumn
    Friend WithEvents BIZ As DataGridViewCheckBoxColumn
    Friend WithEvents BOOKER As DataGridViewTextBoxColumn
    Friend WithEvents lbkUpdateBooker As LinkLabel
    Friend WithEvents ChkBizTrip As CheckBox
    Friend WithEvents cboBooker As ComboBox
    Friend WithEvents Label9 As Label
End Class
