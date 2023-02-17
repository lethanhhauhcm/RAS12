<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmE_InvEdit
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
        Me.lbkVatMinus1 = New System.Windows.Forms.LinkLabel()
        Me.lbkVatAdd1 = New System.Windows.Forms.LinkLabel()
        Me.lbkMoveDown = New System.Windows.Forms.LinkLabel()
        Me.lbkMoveUp = New System.Windows.Forms.LinkLabel()
        Me.lbkDeleteRow = New System.Windows.Forms.LinkLabel()
        Me.lbkAddRow = New System.Windows.Forms.LinkLabel()
        Me.dgrInvDetails = New System.Windows.Forms.DataGridView()
        Me.RecId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tkno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VatPct = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IsSum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbkCreateE_Invoice = New System.Windows.Forms.LinkLabel()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.pnlSelectCustomer = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboCustType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboCustGroup = New System.Windows.Forms.ComboBox()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.pnlSummary = New System.Windows.Forms.Panel()
        Me.txtChargeTV = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtCharge = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTax = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtInvTotal = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.chkOnceOff = New System.Windows.Forms.CheckBox()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.lbkSelectTVC = New System.Windows.Forms.LinkLabel()
        Me.txtMauSo = New System.Windows.Forms.TextBox()
        Me.txtAL = New System.Windows.Forms.TextBox()
        Me.txtKyHieu = New System.Windows.Forms.TextBox()
        Me.txtBiz = New System.Windows.Forms.TextBox()
        Me.txtTvc = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblAL = New System.Windows.Forms.Label()
        Me.pnlInvHeader = New System.Windows.Forms.Panel()
        Me.lbkCTSDOM = New System.Windows.Forms.LinkLabel()
        Me.chkDraft = New System.Windows.Forms.CheckBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.cboDomInt = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cboBU = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtBuyer = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtCustId = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboFOP = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboSRV = New System.Windows.Forms.ComboBox()
        Me.txtDOI = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.txtCustShortName = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtCustomerFullName = New System.Windows.Forms.TextBox()
        Me.txtTaxCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtInvId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.lbkViewOkdInv = New System.Windows.Forms.LinkLabel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtOriFkey = New System.Windows.Forms.TextBox()
        Me.txtPeriod = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkIssue2TV = New System.Windows.Forms.CheckBox()
        Me.lbkCreateDraftE_Invoice = New System.Windows.Forms.LinkLabel()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtBooker = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtNbrOfPax = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtCodeTour = New System.Windows.Forms.TextBox()
        Me.chkKeepNewScreen = New System.Windows.Forms.CheckBox()
        Me.lblGiamThue = New System.Windows.Forms.Label()
        Me.cboGiamThue = New System.Windows.Forms.ComboBox()
        Me.lbkEmailKeToan = New System.Windows.Forms.LinkLabel()
        Me.lbkPreview = New System.Windows.Forms.LinkLabel()
        Me.lbkAddRowIsSum4 = New System.Windows.Forms.LinkLabel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtOriInvNbr = New System.Windows.Forms.TextBox()
        Me.pnlOriInv = New System.Windows.Forms.Panel()
        Me.lbkClearData = New System.Windows.Forms.LinkLabel()
        Me.chkFakeTkno = New System.Windows.Forms.CheckBox()
        Me.lbkResetCustInfo = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrInvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSelectCustomer.SuspendLayout()
        Me.pnlSummary.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.pnlInvHeader.SuspendLayout()
        Me.pnlOriInv.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbkVatMinus1
        '
        Me.lbkVatMinus1.AutoSize = True
        Me.lbkVatMinus1.Location = New System.Drawing.Point(247, 257)
        Me.lbkVatMinus1.Name = "lbkVatMinus1"
        Me.lbkVatMinus1.Size = New System.Drawing.Size(37, 13)
        Me.lbkVatMinus1.TabIndex = 73
        Me.lbkVatMinus1.TabStop = True
        Me.lbkVatMinus1.Text = "VAT-1"
        '
        'lbkVatAdd1
        '
        Me.lbkVatAdd1.AutoSize = True
        Me.lbkVatAdd1.Location = New System.Drawing.Point(201, 257)
        Me.lbkVatAdd1.Name = "lbkVatAdd1"
        Me.lbkVatAdd1.Size = New System.Drawing.Size(40, 13)
        Me.lbkVatAdd1.TabIndex = 72
        Me.lbkVatAdd1.TabStop = True
        Me.lbkVatAdd1.Text = "VAT+1"
        '
        'lbkMoveDown
        '
        Me.lbkMoveDown.AutoSize = True
        Me.lbkMoveDown.Location = New System.Drawing.Point(162, 257)
        Me.lbkMoveDown.Name = "lbkMoveDown"
        Me.lbkMoveDown.Size = New System.Drawing.Size(35, 13)
        Me.lbkMoveDown.TabIndex = 71
        Me.lbkMoveDown.TabStop = True
        Me.lbkMoveDown.Text = "Down"
        '
        'lbkMoveUp
        '
        Me.lbkMoveUp.AutoSize = True
        Me.lbkMoveUp.Location = New System.Drawing.Point(135, 257)
        Me.lbkMoveUp.Name = "lbkMoveUp"
        Me.lbkMoveUp.Size = New System.Drawing.Size(21, 13)
        Me.lbkMoveUp.TabIndex = 70
        Me.lbkMoveUp.TabStop = True
        Me.lbkMoveUp.Text = "Up"
        '
        'lbkDeleteRow
        '
        Me.lbkDeleteRow.AutoSize = True
        Me.lbkDeleteRow.Location = New System.Drawing.Point(64, 257)
        Me.lbkDeleteRow.Name = "lbkDeleteRow"
        Me.lbkDeleteRow.Size = New System.Drawing.Size(60, 13)
        Me.lbkDeleteRow.TabIndex = 69
        Me.lbkDeleteRow.TabStop = True
        Me.lbkDeleteRow.Text = "DeleteRow"
        '
        'lbkAddRow
        '
        Me.lbkAddRow.AutoSize = True
        Me.lbkAddRow.Location = New System.Drawing.Point(10, 257)
        Me.lbkAddRow.Name = "lbkAddRow"
        Me.lbkAddRow.Size = New System.Drawing.Size(48, 13)
        Me.lbkAddRow.TabIndex = 68
        Me.lbkAddRow.TabStop = True
        Me.lbkAddRow.Text = "AddRow"
        '
        'dgrInvDetails
        '
        Me.dgrInvDetails.AllowUserToAddRows = False
        Me.dgrInvDetails.AllowUserToDeleteRows = False
        Me.dgrInvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrInvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInvDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RecId, Me.Tkno, Me.Description, Me.Unit, Me.Qty, Me.Price, Me.Amount, Me.VatPct, Me.VAT, Me.Total, Me.IsSum})
        Me.dgrInvDetails.Location = New System.Drawing.Point(15, 273)
        Me.dgrInvDetails.Name = "dgrInvDetails"
        Me.dgrInvDetails.Size = New System.Drawing.Size(981, 279)
        Me.dgrInvDetails.TabIndex = 67
        '
        'RecId
        '
        Me.RecId.DataPropertyName = "RecId"
        Me.RecId.HeaderText = "RecId"
        Me.RecId.Name = "RecId"
        Me.RecId.Width = 61
        '
        'Tkno
        '
        Me.Tkno.DataPropertyName = "Tkno"
        Me.Tkno.HeaderText = "Tkno"
        Me.Tkno.Name = "Tkno"
        Me.Tkno.Width = 57
        '
        'Description
        '
        Me.Description.DataPropertyName = "Description"
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        Me.Description.Width = 85
        '
        'Unit
        '
        Me.Unit.DataPropertyName = "Unit"
        Me.Unit.HeaderText = "Unit"
        Me.Unit.Name = "Unit"
        Me.Unit.Width = 51
        '
        'Qty
        '
        Me.Qty.DataPropertyName = "Qty"
        Me.Qty.HeaderText = "Qty"
        Me.Qty.Name = "Qty"
        Me.Qty.Width = 48
        '
        'Price
        '
        Me.Price.DataPropertyName = "Price"
        Me.Price.HeaderText = "Price"
        Me.Price.Name = "Price"
        Me.Price.Width = 56
        '
        'Amount
        '
        Me.Amount.DataPropertyName = "Amount"
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        Me.Amount.Width = 68
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
        'Total
        '
        Me.Total.DataPropertyName = "Total"
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        Me.Total.Width = 56
        '
        'IsSum
        '
        Me.IsSum.DataPropertyName = "IsSum"
        Me.IsSum.HeaderText = "IsSum"
        Me.IsSum.Name = "IsSum"
        Me.IsSum.Width = 61
        '
        'lbkCreateE_Invoice
        '
        Me.lbkCreateE_Invoice.AutoSize = True
        Me.lbkCreateE_Invoice.Location = New System.Drawing.Point(833, 568)
        Me.lbkCreateE_Invoice.Name = "lbkCreateE_Invoice"
        Me.lbkCreateE_Invoice.Size = New System.Drawing.Size(86, 13)
        Me.lbkCreateE_Invoice.TabIndex = 75
        Me.lbkCreateE_Invoice.TabStop = True
        Me.lbkCreateE_Invoice.Text = "CreateE_Invoice"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(782, 568)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 74
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'pnlSelectCustomer
        '
        Me.pnlSelectCustomer.Controls.Add(Me.Label5)
        Me.pnlSelectCustomer.Controls.Add(Me.cboCustType)
        Me.pnlSelectCustomer.Controls.Add(Me.Label6)
        Me.pnlSelectCustomer.Controls.Add(Me.cboCustGroup)
        Me.pnlSelectCustomer.Controls.Add(Me.lblCustomer)
        Me.pnlSelectCustomer.Controls.Add(Me.cboCustomer)
        Me.pnlSelectCustomer.Location = New System.Drawing.Point(372, 4)
        Me.pnlSelectCustomer.Name = "pnlSelectCustomer"
        Me.pnlSelectCustomer.Size = New System.Drawing.Size(495, 49)
        Me.pnlSelectCustomer.TabIndex = 107
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 104
        Me.Label5.Text = "CustType"
        '
        'cboCustType
        '
        Me.cboCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustType.FormattingEnabled = True
        Me.cboCustType.Items.AddRange(New Object() {"AP", "CA", "CS", "LC", "TA", "TC", "TO", "WK"})
        Me.cboCustType.Location = New System.Drawing.Point(9, 26)
        Me.cboCustType.Name = "cboCustType"
        Me.cboCustType.Size = New System.Drawing.Size(52, 21)
        Me.cboCustType.TabIndex = 103
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(67, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 13)
        Me.Label6.TabIndex = 102
        Me.Label6.Text = "CustomerGroups"
        '
        'cboCustGroup
        '
        Me.cboCustGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustGroup.FormattingEnabled = True
        Me.cboCustGroup.Location = New System.Drawing.Point(67, 26)
        Me.cboCustGroup.Name = "cboCustGroup"
        Me.cboCustGroup.Size = New System.Drawing.Size(153, 21)
        Me.cboCustGroup.TabIndex = 101
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(224, 9)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(51, 13)
        Me.lblCustomer.TabIndex = 100
        Me.lblCustomer.Text = "Customer"
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(226, 25)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(241, 21)
        Me.cboCustomer.TabIndex = 99
        '
        'pnlSummary
        '
        Me.pnlSummary.Controls.Add(Me.txtChargeTV)
        Me.pnlSummary.Controls.Add(Me.Label26)
        Me.pnlSummary.Controls.Add(Me.txtCharge)
        Me.pnlSummary.Controls.Add(Me.Label11)
        Me.pnlSummary.Controls.Add(Me.txtTax)
        Me.pnlSummary.Controls.Add(Me.Label4)
        Me.pnlSummary.Location = New System.Drawing.Point(17, 558)
        Me.pnlSummary.Name = "pnlSummary"
        Me.pnlSummary.Size = New System.Drawing.Size(349, 46)
        Me.pnlSummary.TabIndex = 105
        Me.pnlSummary.Visible = False
        '
        'txtChargeTV
        '
        Me.txtChargeTV.Enabled = False
        Me.txtChargeTV.Location = New System.Drawing.Point(208, 26)
        Me.txtChargeTV.Name = "txtChargeTV"
        Me.txtChargeTV.Size = New System.Drawing.Size(93, 20)
        Me.txtChargeTV.TabIndex = 96
        Me.txtChargeTV.Text = "0"
        Me.txtChargeTV.Visible = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(205, 10)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(55, 13)
        Me.Label26.TabIndex = 95
        Me.Label26.Text = "ChargeTV"
        Me.Label26.Visible = False
        '
        'txtCharge
        '
        Me.txtCharge.Enabled = False
        Me.txtCharge.Location = New System.Drawing.Point(109, 26)
        Me.txtCharge.Name = "txtCharge"
        Me.txtCharge.Size = New System.Drawing.Size(93, 20)
        Me.txtCharge.TabIndex = 94
        Me.txtCharge.Text = "0"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(106, 10)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(41, 13)
        Me.Label11.TabIndex = 93
        Me.Label11.Text = "Charge"
        '
        'txtTax
        '
        Me.txtTax.Enabled = False
        Me.txtTax.Location = New System.Drawing.Point(10, 26)
        Me.txtTax.Name = "txtTax"
        Me.txtTax.Size = New System.Drawing.Size(93, 20)
        Me.txtTax.TabIndex = 92
        Me.txtTax.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 13)
        Me.Label4.TabIndex = 91
        Me.Label4.Text = "Tax"
        '
        'txtInvTotal
        '
        Me.txtInvTotal.Enabled = False
        Me.txtInvTotal.Location = New System.Drawing.Point(462, 584)
        Me.txtInvTotal.Name = "txtInvTotal"
        Me.txtInvTotal.Size = New System.Drawing.Size(118, 20)
        Me.txtInvTotal.TabIndex = 96
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(416, 591)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 13)
        Me.Label12.TabIndex = 95
        Me.Label12.Text = "InvTotal"
        '
        'chkOnceOff
        '
        Me.chkOnceOff.AutoSize = True
        Me.chkOnceOff.Location = New System.Drawing.Point(876, 12)
        Me.chkOnceOff.Name = "chkOnceOff"
        Me.chkOnceOff.Size = New System.Drawing.Size(66, 17)
        Me.chkOnceOff.TabIndex = 113
        Me.chkOnceOff.Text = "OnceOff"
        Me.chkOnceOff.UseVisualStyleBackColor = True
        '
        'pnlSettings
        '
        Me.pnlSettings.Controls.Add(Me.lbkSelectTVC)
        Me.pnlSettings.Controls.Add(Me.txtMauSo)
        Me.pnlSettings.Controls.Add(Me.txtAL)
        Me.pnlSettings.Controls.Add(Me.txtKyHieu)
        Me.pnlSettings.Controls.Add(Me.txtBiz)
        Me.pnlSettings.Controls.Add(Me.txtTvc)
        Me.pnlSettings.Controls.Add(Me.Label14)
        Me.pnlSettings.Controls.Add(Me.Label13)
        Me.pnlSettings.Controls.Add(Me.Label15)
        Me.pnlSettings.Controls.Add(Me.lblAL)
        Me.pnlSettings.Location = New System.Drawing.Point(9, 4)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(357, 49)
        Me.pnlSettings.TabIndex = 116
        '
        'lbkSelectTVC
        '
        Me.lbkSelectTVC.AutoSize = True
        Me.lbkSelectTVC.Location = New System.Drawing.Point(-3, 5)
        Me.lbkSelectTVC.Name = "lbkSelectTVC"
        Me.lbkSelectTVC.Size = New System.Drawing.Size(58, 13)
        Me.lbkSelectTVC.TabIndex = 145
        Me.lbkSelectTVC.TabStop = True
        Me.lbkSelectTVC.Text = "SelectTVC"
        '
        'txtMauSo
        '
        Me.txtMauSo.Enabled = False
        Me.txtMauSo.Location = New System.Drawing.Point(175, 22)
        Me.txtMauSo.Name = "txtMauSo"
        Me.txtMauSo.Size = New System.Drawing.Size(86, 20)
        Me.txtMauSo.TabIndex = 150
        '
        'txtAL
        '
        Me.txtAL.Enabled = False
        Me.txtAL.Location = New System.Drawing.Point(135, 22)
        Me.txtAL.Name = "txtAL"
        Me.txtAL.Size = New System.Drawing.Size(34, 20)
        Me.txtAL.TabIndex = 142
        '
        'txtKyHieu
        '
        Me.txtKyHieu.Enabled = False
        Me.txtKyHieu.Location = New System.Drawing.Point(267, 22)
        Me.txtKyHieu.Name = "txtKyHieu"
        Me.txtKyHieu.Size = New System.Drawing.Size(64, 20)
        Me.txtKyHieu.TabIndex = 148
        '
        'txtBiz
        '
        Me.txtBiz.Enabled = False
        Me.txtBiz.Location = New System.Drawing.Point(81, 21)
        Me.txtBiz.Name = "txtBiz"
        Me.txtBiz.Size = New System.Drawing.Size(48, 20)
        Me.txtBiz.TabIndex = 141
        '
        'txtTvc
        '
        Me.txtTvc.Enabled = False
        Me.txtTvc.Location = New System.Drawing.Point(2, 22)
        Me.txtTvc.Name = "txtTvc"
        Me.txtTvc.Size = New System.Drawing.Size(73, 20)
        Me.txtTvc.TabIndex = 140
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(251, 5)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(42, 13)
        Me.Label14.TabIndex = 123
        Me.Label14.Text = "Ký hiệu"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(172, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(42, 13)
        Me.Label13.TabIndex = 122
        Me.Label13.Text = "Mẫu số"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(83, 5)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(24, 13)
        Me.Label15.TabIndex = 121
        Me.Label15.Text = "BIZ"
        '
        'lblAL
        '
        Me.lblAL.AutoSize = True
        Me.lblAL.Location = New System.Drawing.Point(132, 5)
        Me.lblAL.Name = "lblAL"
        Me.lblAL.Size = New System.Drawing.Size(20, 13)
        Me.lblAL.TabIndex = 119
        Me.lblAL.Text = "AL"
        '
        'pnlInvHeader
        '
        Me.pnlInvHeader.Controls.Add(Me.lbkResetCustInfo)
        Me.pnlInvHeader.Controls.Add(Me.lbkCTSDOM)
        Me.pnlInvHeader.Controls.Add(Me.chkDraft)
        Me.pnlInvHeader.Controls.Add(Me.Label23)
        Me.pnlInvHeader.Controls.Add(Me.cboDomInt)
        Me.pnlInvHeader.Controls.Add(Me.Label22)
        Me.pnlInvHeader.Controls.Add(Me.cboBU)
        Me.pnlInvHeader.Controls.Add(Me.Label20)
        Me.pnlInvHeader.Controls.Add(Me.txtBuyer)
        Me.pnlInvHeader.Controls.Add(Me.Label19)
        Me.pnlInvHeader.Controls.Add(Me.Label16)
        Me.pnlInvHeader.Controls.Add(Me.Label17)
        Me.pnlInvHeader.Controls.Add(Me.Label18)
        Me.pnlInvHeader.Controls.Add(Me.txtCustId)
        Me.pnlInvHeader.Controls.Add(Me.Label8)
        Me.pnlInvHeader.Controls.Add(Me.cboFOP)
        Me.pnlInvHeader.Controls.Add(Me.Label7)
        Me.pnlInvHeader.Controls.Add(Me.cboSRV)
        Me.pnlInvHeader.Controls.Add(Me.txtDOI)
        Me.pnlInvHeader.Controls.Add(Me.Label10)
        Me.pnlInvHeader.Controls.Add(Me.Label2)
        Me.pnlInvHeader.Controls.Add(Me.txtInvoiceNo)
        Me.pnlInvHeader.Controls.Add(Me.txtCustShortName)
        Me.pnlInvHeader.Controls.Add(Me.txtAddress)
        Me.pnlInvHeader.Controls.Add(Me.txtCustomerFullName)
        Me.pnlInvHeader.Controls.Add(Me.txtTaxCode)
        Me.pnlInvHeader.Controls.Add(Me.Label1)
        Me.pnlInvHeader.Controls.Add(Me.txtInvId)
        Me.pnlInvHeader.Controls.Add(Me.Label3)
        Me.pnlInvHeader.Controls.Add(Me.txtRecId)
        Me.pnlInvHeader.Location = New System.Drawing.Point(5, 59)
        Me.pnlInvHeader.Name = "pnlInvHeader"
        Me.pnlInvHeader.Size = New System.Drawing.Size(991, 127)
        Me.pnlInvHeader.TabIndex = 123
        '
        'lbkCTSDOM
        '
        Me.lbkCTSDOM.AutoSize = True
        Me.lbkCTSDOM.Location = New System.Drawing.Point(485, 2)
        Me.lbkCTSDOM.Name = "lbkCTSDOM"
        Me.lbkCTSDOM.Size = New System.Drawing.Size(53, 13)
        Me.lbkCTSDOM.TabIndex = 161
        Me.lbkCTSDOM.TabStop = True
        Me.lbkCTSDOM.Text = "CTSDOM"
        '
        'chkDraft
        '
        Me.chkDraft.AutoSize = True
        Me.chkDraft.Enabled = False
        Me.chkDraft.Location = New System.Drawing.Point(874, 102)
        Me.chkDraft.Name = "chkDraft"
        Me.chkDraft.Size = New System.Drawing.Size(49, 17)
        Me.chkDraft.TabIndex = 160
        Me.chkDraft.Text = "Draft"
        Me.chkDraft.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(538, 5)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(41, 13)
        Me.Label23.TabIndex = 151
        Me.Label23.Text = "DomInt"
        '
        'cboDomInt
        '
        Me.cboDomInt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDomInt.FormattingEnabled = True
        Me.cboDomInt.Items.AddRange(New Object() {"DOM", "INT"})
        Me.cboDomInt.Location = New System.Drawing.Point(533, 22)
        Me.cboDomInt.Name = "cboDomInt"
        Me.cboDomInt.Size = New System.Drawing.Size(55, 21)
        Me.cboDomInt.TabIndex = 150
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(462, 3)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(22, 13)
        Me.Label22.TabIndex = 149
        Me.Label22.Text = "BU"
        '
        'cboBU
        '
        Me.cboBU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBU.FormattingEnabled = True
        Me.cboBU.Location = New System.Drawing.Point(465, 22)
        Me.cboBU.Name = "cboBU"
        Me.cboBU.Size = New System.Drawing.Size(62, 21)
        Me.cboBU.TabIndex = 148
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(3, 47)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(51, 13)
        Me.Label20.TabIndex = 147
        Me.Label20.Text = "FullName"
        '
        'txtBuyer
        '
        Me.txtBuyer.Location = New System.Drawing.Point(457, 63)
        Me.txtBuyer.Name = "txtBuyer"
        Me.txtBuyer.Size = New System.Drawing.Size(405, 20)
        Me.txtBuyer.TabIndex = 146
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(457, 47)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(34, 13)
        Me.Label19.TabIndex = 145
        Me.Label19.Text = "Buyer"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(-2, 5)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(39, 13)
        Me.Label16.TabIndex = 144
        Me.Label16.Text = "CustID"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(52, 5)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(81, 13)
        Me.Label17.TabIndex = 143
        Me.Label17.Text = "CustShortName"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(242, 5)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(50, 13)
        Me.Label18.TabIndex = 142
        Me.Label18.Text = "TaxCode"
        '
        'txtCustId
        '
        Me.txtCustId.Enabled = False
        Me.txtCustId.Location = New System.Drawing.Point(1, 21)
        Me.txtCustId.Name = "txtCustId"
        Me.txtCustId.Size = New System.Drawing.Size(48, 20)
        Me.txtCustId.TabIndex = 139
        Me.txtCustId.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(402, 5)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(28, 13)
        Me.Label8.TabIndex = 138
        Me.Label8.Text = "FOP"
        '
        'cboFOP
        '
        Me.cboFOP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFOP.FormattingEnabled = True
        Me.cboFOP.Items.AddRange(New Object() {"CK", "TM", "TM, CK"})
        Me.cboFOP.Location = New System.Drawing.Point(405, 21)
        Me.cboFOP.Name = "cboFOP"
        Me.cboFOP.Size = New System.Drawing.Size(54, 21)
        Me.cboFOP.TabIndex = 137
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(364, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 136
        Me.Label7.Text = "SRV"
        '
        'cboSRV
        '
        Me.cboSRV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSRV.FormattingEnabled = True
        Me.cboSRV.Items.AddRange(New Object() {"S", "R"})
        Me.cboSRV.Location = New System.Drawing.Point(367, 21)
        Me.cboSRV.Name = "cboSRV"
        Me.cboSRV.Size = New System.Drawing.Size(32, 21)
        Me.cboSRV.TabIndex = 135
        '
        'txtDOI
        '
        Me.txtDOI.Enabled = False
        Me.txtDOI.Location = New System.Drawing.Point(594, 22)
        Me.txtDOI.Name = "txtDOI"
        Me.txtDOI.Size = New System.Drawing.Size(78, 20)
        Me.txtDOI.TabIndex = 134
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(678, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 133
        Me.Label10.Text = "InvoiceNo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(591, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 132
        Me.Label2.Text = "DOI"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Enabled = False
        Me.txtInvoiceNo.Location = New System.Drawing.Point(678, 22)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(54, 20)
        Me.txtInvoiceNo.TabIndex = 131
        Me.txtInvoiceNo.Text = "0"
        '
        'txtCustShortName
        '
        Me.txtCustShortName.Enabled = False
        Me.txtCustShortName.Location = New System.Drawing.Point(55, 21)
        Me.txtCustShortName.Name = "txtCustShortName"
        Me.txtCustShortName.Size = New System.Drawing.Size(184, 20)
        Me.txtCustShortName.TabIndex = 130
        '
        'txtAddress
        '
        Me.txtAddress.Enabled = False
        Me.txtAddress.Location = New System.Drawing.Point(1, 90)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(861, 29)
        Me.txtAddress.TabIndex = 129
        '
        'txtCustomerFullName
        '
        Me.txtCustomerFullName.Enabled = False
        Me.txtCustomerFullName.Location = New System.Drawing.Point(1, 63)
        Me.txtCustomerFullName.Name = "txtCustomerFullName"
        Me.txtCustomerFullName.Size = New System.Drawing.Size(450, 20)
        Me.txtCustomerFullName.TabIndex = 128
        '
        'txtTaxCode
        '
        Me.txtTaxCode.Enabled = False
        Me.txtTaxCode.Location = New System.Drawing.Point(245, 21)
        Me.txtTaxCode.Name = "txtTaxCode"
        Me.txtTaxCode.Size = New System.Drawing.Size(116, 20)
        Me.txtTaxCode.TabIndex = 127
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(735, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 126
        Me.Label1.Text = "InvId"
        '
        'txtInvId
        '
        Me.txtInvId.Enabled = False
        Me.txtInvId.Location = New System.Drawing.Point(738, 20)
        Me.txtInvId.Name = "txtInvId"
        Me.txtInvId.Size = New System.Drawing.Size(54, 20)
        Me.txtInvId.TabIndex = 125
        Me.txtInvId.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(795, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 124
        Me.Label3.Text = "RecId"
        '
        'txtRecId
        '
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(798, 20)
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(64, 20)
        Me.txtRecId.TabIndex = 123
        Me.txtRecId.Text = "0"
        '
        'lbkViewOkdInv
        '
        Me.lbkViewOkdInv.AutoSize = True
        Me.lbkViewOkdInv.Location = New System.Drawing.Point(58, 40)
        Me.lbkViewOkdInv.Name = "lbkViewOkdInv"
        Me.lbkViewOkdInv.Size = New System.Drawing.Size(61, 13)
        Me.lbkViewOkdInv.TabIndex = 158
        Me.lbkViewOkdInv.TabStop = True
        Me.lbkViewOkdInv.Text = "ViewOldInv"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(0, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(43, 13)
        Me.Label27.TabIndex = 153
        Me.Label27.Text = "OriFkey"
        '
        'txtOriFkey
        '
        Me.txtOriFkey.Enabled = False
        Me.txtOriFkey.ForeColor = System.Drawing.Color.Red
        Me.txtOriFkey.Location = New System.Drawing.Point(3, 17)
        Me.txtOriFkey.Name = "txtOriFkey"
        Me.txtOriFkey.Size = New System.Drawing.Size(71, 20)
        Me.txtOriFkey.TabIndex = 152
        '
        'txtPeriod
        '
        Me.txtPeriod.Location = New System.Drawing.Point(5, 208)
        Me.txtPeriod.Name = "txtPeriod"
        Me.txtPeriod.Size = New System.Drawing.Size(307, 20)
        Me.txtPeriod.TabIndex = 141
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 195)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 140
        Me.Label9.Text = "Period"
        '
        'chkIssue2TV
        '
        Me.chkIssue2TV.AutoSize = True
        Me.chkIssue2TV.Location = New System.Drawing.Point(876, 36)
        Me.chkIssue2TV.Name = "chkIssue2TV"
        Me.chkIssue2TV.Size = New System.Drawing.Size(71, 17)
        Me.chkIssue2TV.TabIndex = 142
        Me.chkIssue2TV.Text = "Issue2TV"
        Me.chkIssue2TV.UseVisualStyleBackColor = True
        '
        'lbkCreateDraftE_Invoice
        '
        Me.lbkCreateDraftE_Invoice.AutoSize = True
        Me.lbkCreateDraftE_Invoice.Location = New System.Drawing.Point(925, 568)
        Me.lbkCreateDraftE_Invoice.Name = "lbkCreateDraftE_Invoice"
        Me.lbkCreateDraftE_Invoice.Size = New System.Drawing.Size(61, 13)
        Me.lbkCreateDraftE_Invoice.TabIndex = 143
        Me.lbkCreateDraftE_Invoice.TabStop = True
        Me.lbkCreateDraftE_Invoice.Text = "CreateDraft"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(44, 234)
        Me.txtEmail.MaxLength = 200
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(770, 20)
        Me.txtEmail.TabIndex = 145
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(6, 234)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(32, 13)
        Me.lblEmail.TabIndex = 144
        Me.lblEmail.Text = "Email"
        '
        'txtBooker
        '
        Me.txtBooker.Location = New System.Drawing.Point(318, 208)
        Me.txtBooker.Name = "txtBooker"
        Me.txtBooker.Size = New System.Drawing.Size(307, 20)
        Me.txtBooker.TabIndex = 147
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(315, 192)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(41, 13)
        Me.Label21.TabIndex = 146
        Me.Label21.Text = "Booker"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(628, 192)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(53, 13)
        Me.Label24.TabIndex = 149
        Me.Label24.Text = "NbrOfPax"
        '
        'txtNbrOfPax
        '
        Me.txtNbrOfPax.Enabled = False
        Me.txtNbrOfPax.Location = New System.Drawing.Point(631, 208)
        Me.txtNbrOfPax.Name = "txtNbrOfPax"
        Me.txtNbrOfPax.Size = New System.Drawing.Size(50, 20)
        Me.txtNbrOfPax.TabIndex = 148
        Me.txtNbrOfPax.Text = "0"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(684, 192)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(54, 13)
        Me.Label25.TabIndex = 151
        Me.Label25.Text = "CodeTour"
        '
        'txtCodeTour
        '
        Me.txtCodeTour.Enabled = False
        Me.txtCodeTour.Location = New System.Drawing.Point(687, 208)
        Me.txtCodeTour.Name = "txtCodeTour"
        Me.txtCodeTour.Size = New System.Drawing.Size(180, 20)
        Me.txtCodeTour.TabIndex = 150
        '
        'chkKeepNewScreen
        '
        Me.chkKeepNewScreen.AutoSize = True
        Me.chkKeepNewScreen.Location = New System.Drawing.Point(836, 591)
        Me.chkKeepNewScreen.Name = "chkKeepNewScreen"
        Me.chkKeepNewScreen.Size = New System.Drawing.Size(82, 17)
        Me.chkKeepNewScreen.TabIndex = 152
        Me.chkKeepNewScreen.Text = "NewScreen"
        Me.chkKeepNewScreen.UseVisualStyleBackColor = True
        '
        'lblGiamThue
        '
        Me.lblGiamThue.AutoSize = True
        Me.lblGiamThue.Location = New System.Drawing.Point(375, 257)
        Me.lblGiamThue.Name = "lblGiamThue"
        Me.lblGiamThue.Size = New System.Drawing.Size(75, 13)
        Me.lblGiamThue.TabIndex = 153
        Me.lblGiamThue.Text = "Giảm thuế (%):"
        '
        'cboGiamThue
        '
        Me.cboGiamThue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGiamThue.FormattingEnabled = True
        Me.cboGiamThue.Items.AddRange(New Object() {"0", "30"})
        Me.cboGiamThue.Location = New System.Drawing.Point(456, 253)
        Me.cboGiamThue.Name = "cboGiamThue"
        Me.cboGiamThue.Size = New System.Drawing.Size(50, 21)
        Me.cboGiamThue.TabIndex = 152
        '
        'lbkEmailKeToan
        '
        Me.lbkEmailKeToan.AutoSize = True
        Me.lbkEmailKeToan.Location = New System.Drawing.Point(816, 237)
        Me.lbkEmailKeToan.Name = "lbkEmailKeToan"
        Me.lbkEmailKeToan.Size = New System.Drawing.Size(51, 13)
        Me.lbkEmailKeToan.TabIndex = 154
        Me.lbkEmailKeToan.TabStop = True
        Me.lbkEmailKeToan.Text = "2KeToan"
        '
        'lbkPreview
        '
        Me.lbkPreview.AutoSize = True
        Me.lbkPreview.Location = New System.Drawing.Point(924, 592)
        Me.lbkPreview.Name = "lbkPreview"
        Me.lbkPreview.Size = New System.Drawing.Size(45, 13)
        Me.lbkPreview.TabIndex = 155
        Me.lbkPreview.TabStop = True
        Me.lbkPreview.Text = "Preview"
        '
        'lbkAddRowIsSum4
        '
        Me.lbkAddRowIsSum4.AutoSize = True
        Me.lbkAddRowIsSum4.Location = New System.Drawing.Point(290, 257)
        Me.lbkAddRowIsSum4.Name = "lbkAddRowIsSum4"
        Me.lbkAddRowIsSum4.Size = New System.Drawing.Size(83, 13)
        Me.lbkAddRowIsSum4.TabIndex = 157
        Me.lbkAddRowIsSum4.TabStop = True
        Me.lbkAddRowIsSum4.Text = "AddRowGhiChu"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(73, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(52, 13)
        Me.Label28.TabIndex = 160
        Me.Label28.Text = "OriInvNbr"
        '
        'txtOriInvNbr
        '
        Me.txtOriInvNbr.Enabled = False
        Me.txtOriInvNbr.ForeColor = System.Drawing.Color.Red
        Me.txtOriInvNbr.Location = New System.Drawing.Point(73, 17)
        Me.txtOriInvNbr.Name = "txtOriInvNbr"
        Me.txtOriInvNbr.Size = New System.Drawing.Size(55, 20)
        Me.txtOriInvNbr.TabIndex = 159
        Me.txtOriInvNbr.Text = "0"
        '
        'pnlOriInv
        '
        Me.pnlOriInv.Controls.Add(Me.txtOriFkey)
        Me.pnlOriInv.Controls.Add(Me.Label28)
        Me.pnlOriInv.Controls.Add(Me.lbkViewOkdInv)
        Me.pnlOriInv.Controls.Add(Me.Label27)
        Me.pnlOriInv.Controls.Add(Me.txtOriInvNbr)
        Me.pnlOriInv.Location = New System.Drawing.Point(873, 208)
        Me.pnlOriInv.Name = "pnlOriInv"
        Me.pnlOriInv.Size = New System.Drawing.Size(128, 56)
        Me.pnlOriInv.TabIndex = 159
        Me.pnlOriInv.Visible = False
        '
        'lbkClearData
        '
        Me.lbkClearData.AutoSize = True
        Me.lbkClearData.Location = New System.Drawing.Point(532, 257)
        Me.lbkClearData.Name = "lbkClearData"
        Me.lbkClearData.Size = New System.Drawing.Size(54, 13)
        Me.lbkClearData.TabIndex = 160
        Me.lbkClearData.TabStop = True
        Me.lbkClearData.Text = "ClearData"
        '
        'chkFakeTkno
        '
        Me.chkFakeTkno.AutoSize = True
        Me.chkFakeTkno.Location = New System.Drawing.Point(749, 590)
        Me.chkFakeTkno.Name = "chkFakeTkno"
        Me.chkFakeTkno.Size = New System.Drawing.Size(75, 17)
        Me.chkFakeTkno.TabIndex = 162
        Me.chkFakeTkno.Text = "FakeTkno"
        Me.chkFakeTkno.UseVisualStyleBackColor = True
        '
        'lbkResetCustInfo
        '
        Me.lbkResetCustInfo.AutoSize = True
        Me.lbkResetCustInfo.Location = New System.Drawing.Point(884, 20)
        Me.lbkResetCustInfo.Name = "lbkResetCustInfo"
        Me.lbkResetCustInfo.Size = New System.Drawing.Size(74, 13)
        Me.lbkResetCustInfo.TabIndex = 162
        Me.lbkResetCustInfo.TabStop = True
        Me.lbkResetCustInfo.Text = "ResetCustInfo"
        '
        'frmE_InvEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.chkFakeTkno)
        Me.Controls.Add(Me.lbkClearData)
        Me.Controls.Add(Me.pnlOriInv)
        Me.Controls.Add(Me.lbkAddRowIsSum4)
        Me.Controls.Add(Me.lbkPreview)
        Me.Controls.Add(Me.lbkEmailKeToan)
        Me.Controls.Add(Me.lblGiamThue)
        Me.Controls.Add(Me.chkKeepNewScreen)
        Me.Controls.Add(Me.cboGiamThue)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.txtCodeTour)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txtNbrOfPax)
        Me.Controls.Add(Me.txtBooker)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtInvTotal)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.lbkCreateDraftE_Invoice)
        Me.Controls.Add(Me.chkIssue2TV)
        Me.Controls.Add(Me.pnlInvHeader)
        Me.Controls.Add(Me.pnlSettings)
        Me.Controls.Add(Me.chkOnceOff)
        Me.Controls.Add(Me.pnlSelectCustomer)
        Me.Controls.Add(Me.lbkCreateE_Invoice)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.lbkVatMinus1)
        Me.Controls.Add(Me.lbkVatAdd1)
        Me.Controls.Add(Me.lbkMoveDown)
        Me.Controls.Add(Me.lbkMoveUp)
        Me.Controls.Add(Me.txtPeriod)
        Me.Controls.Add(Me.lbkDeleteRow)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lbkAddRow)
        Me.Controls.Add(Me.dgrInvDetails)
        Me.Controls.Add(Me.pnlSummary)
        Me.Name = "frmE_InvEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "E_Invoice Edit"
        CType(Me.dgrInvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSelectCustomer.ResumeLayout(False)
        Me.pnlSelectCustomer.PerformLayout()
        Me.pnlSummary.ResumeLayout(False)
        Me.pnlSummary.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.pnlInvHeader.ResumeLayout(False)
        Me.pnlInvHeader.PerformLayout()
        Me.pnlOriInv.ResumeLayout(False)
        Me.pnlOriInv.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkVatMinus1 As LinkLabel
    Friend WithEvents lbkVatAdd1 As LinkLabel
    Friend WithEvents lbkMoveDown As LinkLabel
    Friend WithEvents lbkMoveUp As LinkLabel
    Friend WithEvents lbkDeleteRow As LinkLabel
    Friend WithEvents lbkAddRow As LinkLabel
    Friend WithEvents dgrInvDetails As DataGridView
    Friend WithEvents lbkCreateE_Invoice As LinkLabel
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents pnlSelectCustomer As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents cboCustType As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cboCustGroup As ComboBox
    Friend WithEvents lblCustomer As Label
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents pnlSummary As Panel
    Friend WithEvents txtCharge As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtTax As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtInvTotal As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents chkOnceOff As CheckBox
    Friend WithEvents pnlSettings As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents lblAL As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents pnlInvHeader As Panel
    Friend WithEvents Label20 As Label
    Friend WithEvents txtBuyer As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents txtPeriod As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtCustId As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents cboFOP As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cboSRV As ComboBox
    Friend WithEvents txtDOI As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents txtCustShortName As TextBox
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents txtCustomerFullName As TextBox
    Friend WithEvents txtTaxCode As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtInvId As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents txtMauSo As TextBox
    Friend WithEvents txtKyHieu As TextBox
    Friend WithEvents chkIssue2TV As CheckBox
    Friend WithEvents lbkSelectTVC As LinkLabel
    Friend WithEvents txtAL As TextBox
    Friend WithEvents txtBiz As TextBox
    Friend WithEvents txtTvc As TextBox
    Friend WithEvents lbkCreateDraftE_Invoice As LinkLabel
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lblEmail As Label
    Friend WithEvents txtBooker As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents cboDomInt As ComboBox
    Friend WithEvents Label22 As Label
    Friend WithEvents cboBU As ComboBox
    Friend WithEvents Label24 As Label
    Friend WithEvents txtNbrOfPax As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents txtCodeTour As TextBox
    Friend WithEvents chkKeepNewScreen As CheckBox
    Friend WithEvents txtChargeTV As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents lblGiamThue As Label
    Friend WithEvents cboGiamThue As ComboBox
    Friend WithEvents RecId As DataGridViewTextBoxColumn
    Friend WithEvents Tkno As DataGridViewTextBoxColumn
    Friend WithEvents Description As DataGridViewTextBoxColumn
    Friend WithEvents Unit As DataGridViewTextBoxColumn
    Friend WithEvents Qty As DataGridViewTextBoxColumn
    Friend WithEvents Price As DataGridViewTextBoxColumn
    Friend WithEvents Amount As DataGridViewTextBoxColumn
    Friend WithEvents VatPct As DataGridViewTextBoxColumn
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents Total As DataGridViewTextBoxColumn
    Friend WithEvents IsSum As DataGridViewTextBoxColumn
    Friend WithEvents lbkEmailKeToan As LinkLabel
    Friend WithEvents Label27 As Label
    Friend WithEvents txtOriFkey As TextBox
    Friend WithEvents lbkPreview As LinkLabel
    Friend WithEvents lbkAddRowIsSum4 As LinkLabel
    Friend WithEvents lbkViewOkdInv As LinkLabel
    Friend WithEvents Label28 As Label
    Friend WithEvents txtOriInvNbr As TextBox
    Friend WithEvents pnlOriInv As Panel
    Friend WithEvents chkDraft As CheckBox
    Friend WithEvents lbkClearData As LinkLabel
    Friend WithEvents lbkCTSDOM As LinkLabel
    Friend WithEvents chkFakeTkno As CheckBox
    Friend WithEvents lbkResetCustInfo As LinkLabel
End Class
