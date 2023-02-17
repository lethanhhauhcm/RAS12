<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVatInvEdit
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
        Me.txtInvId = New System.Windows.Forms.TextBox()
        Me.dtpDOI = New System.Windows.Forms.DateTimePicker()
        Me.txtTaxCode = New System.Windows.Forms.TextBox()
        Me.txtCustomerFullName = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.txtF1 = New System.Windows.Forms.TextBox()
        Me.txtPeriod = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblF1 = New System.Windows.Forms.Label()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.lbkPrint = New System.Windows.Forms.LinkLabel()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.lblF2 = New System.Windows.Forms.Label()
        Me.txtF2 = New System.Windows.Forms.TextBox()
        Me.lblF3 = New System.Windows.Forms.Label()
        Me.txtF3 = New System.Windows.Forms.TextBox()
        Me.lblF6 = New System.Windows.Forms.Label()
        Me.txtF6 = New System.Windows.Forms.TextBox()
        Me.lblF5 = New System.Windows.Forms.Label()
        Me.txtF5 = New System.Windows.Forms.TextBox()
        Me.lblF4 = New System.Windows.Forms.Label()
        Me.txtF4 = New System.Windows.Forms.TextBox()
        Me.lblF9 = New System.Windows.Forms.Label()
        Me.txtF9 = New System.Windows.Forms.TextBox()
        Me.lblF8 = New System.Windows.Forms.Label()
        Me.txtF8 = New System.Windows.Forms.TextBox()
        Me.lblF7 = New System.Windows.Forms.Label()
        Me.txtF7 = New System.Windows.Forms.TextBox()
        Me.dgrInvDetails = New System.Windows.Forms.DataGridView()
        Me.Seq = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NonVatable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AmountNoVat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VatPct = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Vatable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShownInListing = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.txtCustShortName = New System.Windows.Forms.TextBox()
        Me.lbkAddRow = New System.Windows.Forms.LinkLabel()
        Me.lbkDeleteRow = New System.Windows.Forms.LinkLabel()
        Me.lbkMoveUp = New System.Windows.Forms.LinkLabel()
        Me.lbkMoveDown = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtGhiNoId = New System.Windows.Forms.TextBox()
        Me.lbkGetNextInv = New System.Windows.Forms.LinkLabel()
        Me.lbkVatAdd1 = New System.Windows.Forms.LinkLabel()
        Me.lbkVatMinus1 = New System.Windows.Forms.LinkLabel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBuyer = New System.Windows.Forms.TextBox()
        Me.lbkGetLastPeriod = New System.Windows.Forms.LinkLabel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtService = New System.Windows.Forms.TextBox()
        Me.lbkCreateE_Invoice = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrInvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtInvId
        '
        Me.txtInvId.Enabled = False
        Me.txtInvId.Location = New System.Drawing.Point(908, 51)
        Me.txtInvId.Name = "txtInvId"
        Me.txtInvId.Size = New System.Drawing.Size(40, 20)
        Me.txtInvId.TabIndex = 1
        Me.txtInvId.Text = "0"
        '
        'dtpDOI
        '
        Me.dtpDOI.CustomFormat = "dd MMM yy"
        Me.dtpDOI.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOI.Location = New System.Drawing.Point(12, 51)
        Me.dtpDOI.Name = "dtpDOI"
        Me.dtpDOI.Size = New System.Drawing.Size(78, 20)
        Me.dtpDOI.TabIndex = 2
        '
        'txtTaxCode
        '
        Me.txtTaxCode.Enabled = False
        Me.txtTaxCode.Location = New System.Drawing.Point(167, 12)
        Me.txtTaxCode.Name = "txtTaxCode"
        Me.txtTaxCode.Size = New System.Drawing.Size(84, 20)
        Me.txtTaxCode.TabIndex = 3
        '
        'txtCustomerFullName
        '
        Me.txtCustomerFullName.Enabled = False
        Me.txtCustomerFullName.Location = New System.Drawing.Point(257, 12)
        Me.txtCustomerFullName.Name = "txtCustomerFullName"
        Me.txtCustomerFullName.Size = New System.Drawing.Size(254, 20)
        Me.txtCustomerFullName.TabIndex = 4
        '
        'txtAddress
        '
        Me.txtAddress.Enabled = False
        Me.txtAddress.Location = New System.Drawing.Point(517, 12)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(479, 20)
        Me.txtAddress.TabIndex = 5
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(96, 51)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(40, 20)
        Me.txtInvoiceNo.TabIndex = 0
        '
        'txtF1
        '
        Me.txtF1.Location = New System.Drawing.Point(3, 130)
        Me.txtF1.Name = "txtF1"
        Me.txtF1.Size = New System.Drawing.Size(280, 20)
        Me.txtF1.TabIndex = 2
        '
        'txtPeriod
        '
        Me.txtPeriod.Location = New System.Drawing.Point(158, 51)
        Me.txtPeriod.Name = "txtPeriod"
        Me.txtPeriod.Size = New System.Drawing.Size(96, 20)
        Me.txtPeriod.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(905, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "InvId"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "DOI"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(96, 35)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "InvoiceNo"
        '
        'lblF1
        '
        Me.lblF1.AutoSize = True
        Me.lblF1.Location = New System.Drawing.Point(0, 114)
        Me.lblF1.Name = "lblF1"
        Me.lblF1.Size = New System.Drawing.Size(19, 13)
        Me.lblF1.TabIndex = 32
        Me.lblF1.Text = "F1"
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.Location = New System.Drawing.Point(158, 35)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(37, 13)
        Me.lblPeriod.TabIndex = 33
        Me.lblPeriod.Text = "Period"
        '
        'lbkPrint
        '
        Me.lbkPrint.AutoSize = True
        Me.lbkPrint.Location = New System.Drawing.Point(3, 465)
        Me.lbkPrint.Name = "lbkPrint"
        Me.lbkPrint.Size = New System.Drawing.Size(28, 13)
        Me.lbkPrint.TabIndex = 36
        Me.lbkPrint.TabStop = True
        Me.lbkPrint.Text = "Print"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(46, 465)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 37
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'lblF2
        '
        Me.lblF2.AutoSize = True
        Me.lblF2.Location = New System.Drawing.Point(0, 153)
        Me.lblF2.Name = "lblF2"
        Me.lblF2.Size = New System.Drawing.Size(19, 13)
        Me.lblF2.TabIndex = 39
        Me.lblF2.Text = "F2"
        '
        'txtF2
        '
        Me.txtF2.Location = New System.Drawing.Point(3, 169)
        Me.txtF2.Name = "txtF2"
        Me.txtF2.Size = New System.Drawing.Size(280, 20)
        Me.txtF2.TabIndex = 3
        '
        'lblF3
        '
        Me.lblF3.AutoSize = True
        Me.lblF3.Location = New System.Drawing.Point(0, 192)
        Me.lblF3.Name = "lblF3"
        Me.lblF3.Size = New System.Drawing.Size(19, 13)
        Me.lblF3.TabIndex = 41
        Me.lblF3.Text = "F3"
        '
        'txtF3
        '
        Me.txtF3.Location = New System.Drawing.Point(3, 208)
        Me.txtF3.Name = "txtF3"
        Me.txtF3.Size = New System.Drawing.Size(280, 20)
        Me.txtF3.TabIndex = 40
        '
        'lblF6
        '
        Me.lblF6.AutoSize = True
        Me.lblF6.Location = New System.Drawing.Point(0, 309)
        Me.lblF6.Name = "lblF6"
        Me.lblF6.Size = New System.Drawing.Size(19, 13)
        Me.lblF6.TabIndex = 47
        Me.lblF6.Text = "F6"
        '
        'txtF6
        '
        Me.txtF6.Location = New System.Drawing.Point(3, 325)
        Me.txtF6.Name = "txtF6"
        Me.txtF6.Size = New System.Drawing.Size(280, 20)
        Me.txtF6.TabIndex = 46
        '
        'lblF5
        '
        Me.lblF5.AutoSize = True
        Me.lblF5.Location = New System.Drawing.Point(0, 270)
        Me.lblF5.Name = "lblF5"
        Me.lblF5.Size = New System.Drawing.Size(19, 13)
        Me.lblF5.TabIndex = 45
        Me.lblF5.Text = "F5"
        '
        'txtF5
        '
        Me.txtF5.Location = New System.Drawing.Point(3, 286)
        Me.txtF5.Name = "txtF5"
        Me.txtF5.Size = New System.Drawing.Size(280, 20)
        Me.txtF5.TabIndex = 44
        '
        'lblF4
        '
        Me.lblF4.AutoSize = True
        Me.lblF4.Location = New System.Drawing.Point(0, 231)
        Me.lblF4.Name = "lblF4"
        Me.lblF4.Size = New System.Drawing.Size(19, 13)
        Me.lblF4.TabIndex = 43
        Me.lblF4.Text = "F4"
        '
        'txtF4
        '
        Me.txtF4.Location = New System.Drawing.Point(3, 247)
        Me.txtF4.Name = "txtF4"
        Me.txtF4.Size = New System.Drawing.Size(280, 20)
        Me.txtF4.TabIndex = 42
        '
        'lblF9
        '
        Me.lblF9.AutoSize = True
        Me.lblF9.Location = New System.Drawing.Point(0, 426)
        Me.lblF9.Name = "lblF9"
        Me.lblF9.Size = New System.Drawing.Size(19, 13)
        Me.lblF9.TabIndex = 53
        Me.lblF9.Text = "F9"
        '
        'txtF9
        '
        Me.txtF9.Location = New System.Drawing.Point(3, 442)
        Me.txtF9.Name = "txtF9"
        Me.txtF9.Size = New System.Drawing.Size(280, 20)
        Me.txtF9.TabIndex = 52
        '
        'lblF8
        '
        Me.lblF8.AutoSize = True
        Me.lblF8.Location = New System.Drawing.Point(0, 387)
        Me.lblF8.Name = "lblF8"
        Me.lblF8.Size = New System.Drawing.Size(19, 13)
        Me.lblF8.TabIndex = 51
        Me.lblF8.Text = "F8"
        '
        'txtF8
        '
        Me.txtF8.Location = New System.Drawing.Point(3, 403)
        Me.txtF8.Name = "txtF8"
        Me.txtF8.Size = New System.Drawing.Size(280, 20)
        Me.txtF8.TabIndex = 50
        '
        'lblF7
        '
        Me.lblF7.AutoSize = True
        Me.lblF7.Location = New System.Drawing.Point(0, 348)
        Me.lblF7.Name = "lblF7"
        Me.lblF7.Size = New System.Drawing.Size(19, 13)
        Me.lblF7.TabIndex = 49
        Me.lblF7.Text = "F7"
        '
        'txtF7
        '
        Me.txtF7.Location = New System.Drawing.Point(3, 364)
        Me.txtF7.Name = "txtF7"
        Me.txtF7.Size = New System.Drawing.Size(280, 20)
        Me.txtF7.TabIndex = 48
        '
        'dgrInvDetails
        '
        Me.dgrInvDetails.AllowUserToAddRows = False
        Me.dgrInvDetails.AllowUserToDeleteRows = False
        Me.dgrInvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrInvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInvDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Seq, Me.Description, Me.NonVatable, Me.AmountNoVat, Me.VatPct, Me.VAT, Me.Vatable, Me.Total, Me.ShownInListing})
        Me.dgrInvDetails.Location = New System.Drawing.Point(297, 130)
        Me.dgrInvDetails.Name = "dgrInvDetails"
        Me.dgrInvDetails.Size = New System.Drawing.Size(703, 332)
        Me.dgrInvDetails.TabIndex = 54
        '
        'Seq
        '
        Me.Seq.DataPropertyName = "Seq"
        Me.Seq.HeaderText = "Seq"
        Me.Seq.Name = "Seq"
        Me.Seq.Width = 51
        '
        'Description
        '
        Me.Description.DataPropertyName = "Description"
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        Me.Description.Width = 85
        '
        'NonVatable
        '
        Me.NonVatable.DataPropertyName = "NonVatable"
        Me.NonVatable.HeaderText = "NonVatable"
        Me.NonVatable.Name = "NonVatable"
        Me.NonVatable.Width = 88
        '
        'AmountNoVat
        '
        Me.AmountNoVat.DataPropertyName = "AmountNoVat"
        Me.AmountNoVat.HeaderText = "AmountNoVat"
        Me.AmountNoVat.Name = "AmountNoVat"
        Me.AmountNoVat.Width = 98
        '
        'VatPct
        '
        Me.VatPct.DataPropertyName = "VatPct"
        Me.VatPct.HeaderText = "VatPct"
        Me.VatPct.Name = "VatPct"
        Me.VatPct.Width = 64
        '
        'VAT
        '
        Me.VAT.DataPropertyName = "VAT"
        Me.VAT.HeaderText = "VAT"
        Me.VAT.Name = "VAT"
        Me.VAT.Width = 53
        '
        'Vatable
        '
        Me.Vatable.DataPropertyName = "Vatable"
        Me.Vatable.HeaderText = "Vatable"
        Me.Vatable.Name = "Vatable"
        Me.Vatable.Width = 68
        '
        'Total
        '
        Me.Total.DataPropertyName = "Vatable"
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        Me.Total.Width = 56
        '
        'ShownInListing
        '
        Me.ShownInListing.DataPropertyName = "ShownInListing"
        Me.ShownInListing.HeaderText = "ShownInListing"
        Me.ShownInListing.Name = "ShownInListing"
        Me.ShownInListing.Width = 85
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(954, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "RecId"
        '
        'txtRecId
        '
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(957, 51)
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(40, 20)
        Me.txtRecId.TabIndex = 55
        Me.txtRecId.Text = "0"
        '
        'txtCustShortName
        '
        Me.txtCustShortName.Enabled = False
        Me.txtCustShortName.Location = New System.Drawing.Point(12, 12)
        Me.txtCustShortName.Name = "txtCustShortName"
        Me.txtCustShortName.Size = New System.Drawing.Size(149, 20)
        Me.txtCustShortName.TabIndex = 57
        '
        'lbkAddRow
        '
        Me.lbkAddRow.AutoSize = True
        Me.lbkAddRow.Location = New System.Drawing.Point(710, 104)
        Me.lbkAddRow.Name = "lbkAddRow"
        Me.lbkAddRow.Size = New System.Drawing.Size(48, 13)
        Me.lbkAddRow.TabIndex = 58
        Me.lbkAddRow.TabStop = True
        Me.lbkAddRow.Text = "AddRow"
        '
        'lbkDeleteRow
        '
        Me.lbkDeleteRow.AutoSize = True
        Me.lbkDeleteRow.Location = New System.Drawing.Point(764, 104)
        Me.lbkDeleteRow.Name = "lbkDeleteRow"
        Me.lbkDeleteRow.Size = New System.Drawing.Size(60, 13)
        Me.lbkDeleteRow.TabIndex = 59
        Me.lbkDeleteRow.TabStop = True
        Me.lbkDeleteRow.Text = "DeleteRow"
        '
        'lbkMoveUp
        '
        Me.lbkMoveUp.AutoSize = True
        Me.lbkMoveUp.Location = New System.Drawing.Point(835, 104)
        Me.lbkMoveUp.Name = "lbkMoveUp"
        Me.lbkMoveUp.Size = New System.Drawing.Size(21, 13)
        Me.lbkMoveUp.TabIndex = 60
        Me.lbkMoveUp.TabStop = True
        Me.lbkMoveUp.Text = "Up"
        '
        'lbkMoveDown
        '
        Me.lbkMoveDown.AutoSize = True
        Me.lbkMoveDown.Location = New System.Drawing.Point(862, 104)
        Me.lbkMoveDown.Name = "lbkMoveDown"
        Me.lbkMoveDown.Size = New System.Drawing.Size(35, 13)
        Me.lbkMoveDown.TabIndex = 61
        Me.lbkMoveDown.TabStop = True
        Me.lbkMoveDown.Text = "Down"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(854, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "GhiNoId"
        '
        'txtGhiNoId
        '
        Me.txtGhiNoId.Enabled = False
        Me.txtGhiNoId.Location = New System.Drawing.Point(857, 51)
        Me.txtGhiNoId.Name = "txtGhiNoId"
        Me.txtGhiNoId.Size = New System.Drawing.Size(40, 20)
        Me.txtGhiNoId.TabIndex = 62
        Me.txtGhiNoId.Text = "0"
        '
        'lbkGetNextInv
        '
        Me.lbkGetNextInv.AutoSize = True
        Me.lbkGetNextInv.Location = New System.Drawing.Point(9, 74)
        Me.lbkGetNextInv.Name = "lbkGetNextInv"
        Me.lbkGetNextInv.Size = New System.Drawing.Size(61, 13)
        Me.lbkGetNextInv.TabIndex = 64
        Me.lbkGetNextInv.TabStop = True
        Me.lbkGetNextInv.Text = "GetNextInv"
        '
        'lbkVatAdd1
        '
        Me.lbkVatAdd1.AutoSize = True
        Me.lbkVatAdd1.Location = New System.Drawing.Point(901, 104)
        Me.lbkVatAdd1.Name = "lbkVatAdd1"
        Me.lbkVatAdd1.Size = New System.Drawing.Size(40, 13)
        Me.lbkVatAdd1.TabIndex = 65
        Me.lbkVatAdd1.TabStop = True
        Me.lbkVatAdd1.Text = "VAT+1"
        '
        'lbkVatMinus1
        '
        Me.lbkVatMinus1.AutoSize = True
        Me.lbkVatMinus1.Location = New System.Drawing.Point(947, 104)
        Me.lbkVatMinus1.Name = "lbkVatMinus1"
        Me.lbkVatMinus1.Size = New System.Drawing.Size(37, 13)
        Me.lbkVatMinus1.TabIndex = 66
        Me.lbkVatMinus1.TabStop = True
        Me.lbkVatMinus1.Text = "VAT-1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(517, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 68
        Me.Label5.Text = "Buyer"
        '
        'txtBuyer
        '
        Me.txtBuyer.Enabled = False
        Me.txtBuyer.Location = New System.Drawing.Point(517, 51)
        Me.txtBuyer.Name = "txtBuyer"
        Me.txtBuyer.Size = New System.Drawing.Size(186, 20)
        Me.txtBuyer.TabIndex = 67
        '
        'lbkGetLastPeriod
        '
        Me.lbkGetLastPeriod.AutoSize = True
        Me.lbkGetLastPeriod.Location = New System.Drawing.Point(158, 74)
        Me.lbkGetLastPeriod.Name = "lbkGetLastPeriod"
        Me.lbkGetLastPeriod.Size = New System.Drawing.Size(74, 13)
        Me.lbkGetLastPeriod.TabIndex = 69
        Me.lbkGetLastPeriod.TabStop = True
        Me.lbkGetLastPeriod.Text = "GetLastPeriod"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(710, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 13)
        Me.Label6.TabIndex = 71
        Me.Label6.Text = "Service"
        '
        'txtService
        '
        Me.txtService.Enabled = False
        Me.txtService.Location = New System.Drawing.Point(710, 51)
        Me.txtService.Name = "txtService"
        Me.txtService.Size = New System.Drawing.Size(98, 20)
        Me.txtService.TabIndex = 70
        '
        'lbkCreateE_Invoice
        '
        Me.lbkCreateE_Invoice.AutoSize = True
        Me.lbkCreateE_Invoice.Location = New System.Drawing.Point(96, 465)
        Me.lbkCreateE_Invoice.Name = "lbkCreateE_Invoice"
        Me.lbkCreateE_Invoice.Size = New System.Drawing.Size(86, 13)
        Me.lbkCreateE_Invoice.TabIndex = 72
        Me.lbkCreateE_Invoice.TabStop = True
        Me.lbkCreateE_Invoice.Text = "CreateE_Invoice"
        '
        'frmVatInvEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 561)
        Me.Controls.Add(Me.lbkCreateE_Invoice)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtService)
        Me.Controls.Add(Me.lbkGetLastPeriod)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtBuyer)
        Me.Controls.Add(Me.lbkVatMinus1)
        Me.Controls.Add(Me.lbkVatAdd1)
        Me.Controls.Add(Me.lbkGetNextInv)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtGhiNoId)
        Me.Controls.Add(Me.lbkMoveDown)
        Me.Controls.Add(Me.lbkMoveUp)
        Me.Controls.Add(Me.lbkDeleteRow)
        Me.Controls.Add(Me.lbkAddRow)
        Me.Controls.Add(Me.txtCustShortName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.dgrInvDetails)
        Me.Controls.Add(Me.lblF9)
        Me.Controls.Add(Me.txtF9)
        Me.Controls.Add(Me.lblF8)
        Me.Controls.Add(Me.txtF8)
        Me.Controls.Add(Me.lblF7)
        Me.Controls.Add(Me.txtF7)
        Me.Controls.Add(Me.lblF6)
        Me.Controls.Add(Me.txtF6)
        Me.Controls.Add(Me.lblF5)
        Me.Controls.Add(Me.txtF5)
        Me.Controls.Add(Me.lblF4)
        Me.Controls.Add(Me.txtF4)
        Me.Controls.Add(Me.lblF3)
        Me.Controls.Add(Me.txtF3)
        Me.Controls.Add(Me.lblF2)
        Me.Controls.Add(Me.txtF2)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.lbkPrint)
        Me.Controls.Add(Me.lblPeriod)
        Me.Controls.Add(Me.lblF1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPeriod)
        Me.Controls.Add(Me.txtF1)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.txtCustomerFullName)
        Me.Controls.Add(Me.txtTaxCode)
        Me.Controls.Add(Me.dtpDOI)
        Me.Controls.Add(Me.txtInvId)
        Me.Name = "frmVatInvEdit"
        Me.Text = "Vat Invoice Edit"
        CType(Me.dgrInvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtInvId As TextBox
    Friend WithEvents dtpDOI As DateTimePicker
    Friend WithEvents txtTaxCode As TextBox
    Friend WithEvents txtCustomerFullName As TextBox
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents txtF1 As TextBox
    Friend WithEvents txtPeriod As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblF1 As Label
    Friend WithEvents lblPeriod As Label
    Friend WithEvents lbkPrint As LinkLabel
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents lblF2 As Label
    Friend WithEvents txtF2 As TextBox
    Friend WithEvents lblF3 As Label
    Friend WithEvents txtF3 As TextBox
    Friend WithEvents lblF6 As Label
    Friend WithEvents txtF6 As TextBox
    Friend WithEvents lblF5 As Label
    Friend WithEvents txtF5 As TextBox
    Friend WithEvents lblF4 As Label
    Friend WithEvents txtF4 As TextBox
    Friend WithEvents lblF9 As Label
    Friend WithEvents txtF9 As TextBox
    Friend WithEvents lblF8 As Label
    Friend WithEvents txtF8 As TextBox
    Friend WithEvents lblF7 As Label
    Friend WithEvents txtF7 As TextBox
    Friend WithEvents dgrInvDetails As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents txtCustShortName As TextBox
    Friend WithEvents lbkAddRow As LinkLabel
    Friend WithEvents lbkDeleteRow As LinkLabel
    Friend WithEvents lbkMoveUp As LinkLabel
    Friend WithEvents lbkMoveDown As LinkLabel
    Friend WithEvents Seq As DataGridViewTextBoxColumn
    Friend WithEvents Description As DataGridViewTextBoxColumn
    Friend WithEvents NonVatable As DataGridViewTextBoxColumn
    Friend WithEvents AmountNoVat As DataGridViewTextBoxColumn
    Friend WithEvents VatPct As DataGridViewTextBoxColumn
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents Vatable As DataGridViewTextBoxColumn
    Friend WithEvents Total As DataGridViewTextBoxColumn
    Friend WithEvents ShownInListing As DataGridViewCheckBoxColumn
    Friend WithEvents Label4 As Label
    Friend WithEvents txtGhiNoId As TextBox
    Friend WithEvents lbkGetNextInv As LinkLabel
    Friend WithEvents lbkVatAdd1 As LinkLabel
    Friend WithEvents lbkVatMinus1 As LinkLabel
    Friend WithEvents Label5 As Label
    Friend WithEvents txtBuyer As TextBox
    Friend WithEvents lbkGetLastPeriod As LinkLabel
    Friend WithEvents Label6 As Label
    Friend WithEvents txtService As TextBox
    Friend WithEvents lbkCreateE_Invoice As LinkLabel
End Class
