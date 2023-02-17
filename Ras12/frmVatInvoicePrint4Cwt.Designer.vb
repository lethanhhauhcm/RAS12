<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVatInvoicePrint4Cwt
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
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.lbkLoadFile = New System.Windows.Forms.LinkLabel()
        Me.dgrTktListing = New System.Windows.Forms.DataGridView()
        Me.cboSheetName = New System.Windows.Forms.ComboBox()
        Me.lbkSelectAll = New System.Windows.Forms.LinkLabel()
        Me.grbUpdate = New System.Windows.Forms.GroupBox()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkClone = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.dgrInvDetails = New System.Windows.Forms.DataGridView()
        Me.cboF1 = New System.Windows.Forms.ComboBox()
        Me.cboF2 = New System.Windows.Forms.ComboBox()
        Me.lblF1 = New System.Windows.Forms.Label()
        Me.lblF2 = New System.Windows.Forms.Label()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.dtpNewPmtDeadLine = New System.Windows.Forms.DateTimePicker()
        Me.lbkUpdatePmtDeadline = New System.Windows.Forms.LinkLabel()
        Me.lbkLoadFromRAS = New System.Windows.Forms.LinkLabel()
        Me.txtTktItem = New System.Windows.Forms.TextBox()
        Me.cboRasDoc = New System.Windows.Forms.ComboBox()
        Me.txtInvAmt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboSelectCount = New System.Windows.Forms.ComboBox()
        Me.lbkCreateE_Invoice = New System.Windows.Forms.LinkLabel()
        Me.lbkSwitchVatPct = New System.Windows.Forms.LinkLabel()
        Me.cboVatPct = New System.Windows.Forms.ComboBox()
        CType(Me.dgrTktListing, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbUpdate.SuspendLayout()
        CType(Me.dgrInvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Items.AddRange(New Object() {"ABBOTT 3A", "ABBOTT 3A ANI_M", "ABBOTT 3A_MICE", "ABBOTT ALSA ANI_MICE", "ABBOTT ALSA_MICE", "ABBOTT LA HCP", "ABBOTT RO", "ABBOTT RO_MICE", "ATOTECH", "EVN SPC", "HASBRO", "HEMPEL", "HOGAN HCM", "HOGAN HN", "INGER", "JCI VN", "KHLE", "LLOYDS REGISTER", "NGHISON VN", "NGOCMAI_VJ", "NOVARTIS VN", "NOVARTIS_MICE", "NVS_ MICE", "PATH VN", "PHUONGNAM_VJ", "SANDOZ NOVARTIS", "SAOMAI_VJ", "SDZ_MICE", "THANHHOANG_VJ"})
        Me.cboCustomer.Location = New System.Drawing.Point(17, 27)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(150, 21)
        Me.cboCustomer.TabIndex = 0
        '
        'lbkLoadFile
        '
        Me.lbkLoadFile.AutoSize = True
        Me.lbkLoadFile.Location = New System.Drawing.Point(173, 35)
        Me.lbkLoadFile.Name = "lbkLoadFile"
        Me.lbkLoadFile.Size = New System.Drawing.Size(47, 13)
        Me.lbkLoadFile.TabIndex = 1
        Me.lbkLoadFile.TabStop = True
        Me.lbkLoadFile.Text = "LoadFile"
        '
        'dgrTktListing
        '
        Me.dgrTktListing.AllowUserToAddRows = False
        Me.dgrTktListing.AllowUserToDeleteRows = False
        Me.dgrTktListing.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrTktListing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTktListing.Location = New System.Drawing.Point(12, 54)
        Me.dgrTktListing.Name = "dgrTktListing"
        Me.dgrTktListing.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgrTktListing.Size = New System.Drawing.Size(992, 346)
        Me.dgrTktListing.TabIndex = 2
        '
        'cboSheetName
        '
        Me.cboSheetName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSheetName.FormattingEnabled = True
        Me.cboSheetName.Location = New System.Drawing.Point(226, 27)
        Me.cboSheetName.Name = "cboSheetName"
        Me.cboSheetName.Size = New System.Drawing.Size(202, 21)
        Me.cboSheetName.TabIndex = 8
        '
        'lbkSelectAll
        '
        Me.lbkSelectAll.AutoSize = True
        Me.lbkSelectAll.Location = New System.Drawing.Point(12, 600)
        Me.lbkSelectAll.Name = "lbkSelectAll"
        Me.lbkSelectAll.Size = New System.Drawing.Size(86, 13)
        Me.lbkSelectAll.TabIndex = 9
        Me.lbkSelectAll.TabStop = True
        Me.lbkSelectAll.Text = "Select/UnSelect"
        Me.lbkSelectAll.Visible = False
        '
        'grbUpdate
        '
        Me.grbUpdate.Controls.Add(Me.lbkDelete)
        Me.grbUpdate.Controls.Add(Me.lbkEdit)
        Me.grbUpdate.Controls.Add(Me.lbkClone)
        Me.grbUpdate.Controls.Add(Me.lbkNew)
        Me.grbUpdate.Location = New System.Drawing.Point(674, 584)
        Me.grbUpdate.Name = "grbUpdate"
        Me.grbUpdate.Size = New System.Drawing.Size(200, 31)
        Me.grbUpdate.TabIndex = 13
        Me.grbUpdate.TabStop = False
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(146, 16)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 16
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(85, 16)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 15
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkClone
        '
        Me.lbkClone.AutoSize = True
        Me.lbkClone.Location = New System.Drawing.Point(40, 16)
        Me.lbkClone.Name = "lbkClone"
        Me.lbkClone.Size = New System.Drawing.Size(34, 13)
        Me.lbkClone.TabIndex = 14
        Me.lbkClone.TabStop = True
        Me.lbkClone.Text = "Clone"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(6, 16)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 13
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'dgrInvDetails
        '
        Me.dgrInvDetails.AllowUserToAddRows = False
        Me.dgrInvDetails.AllowUserToDeleteRows = False
        Me.dgrInvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrInvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInvDetails.Location = New System.Drawing.Point(12, 406)
        Me.dgrInvDetails.Name = "dgrInvDetails"
        Me.dgrInvDetails.Size = New System.Drawing.Size(992, 160)
        Me.dgrInvDetails.TabIndex = 14
        '
        'cboF1
        '
        Me.cboF1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboF1.FormattingEnabled = True
        Me.cboF1.Location = New System.Drawing.Point(434, 27)
        Me.cboF1.Name = "cboF1"
        Me.cboF1.Size = New System.Drawing.Size(181, 21)
        Me.cboF1.TabIndex = 15
        '
        'cboF2
        '
        Me.cboF2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboF2.FormattingEnabled = True
        Me.cboF2.Location = New System.Drawing.Point(621, 27)
        Me.cboF2.Name = "cboF2"
        Me.cboF2.Size = New System.Drawing.Size(181, 21)
        Me.cboF2.TabIndex = 16
        '
        'lblF1
        '
        Me.lblF1.AutoSize = True
        Me.lblF1.Location = New System.Drawing.Point(431, 9)
        Me.lblF1.Name = "lblF1"
        Me.lblF1.Size = New System.Drawing.Size(19, 13)
        Me.lblF1.TabIndex = 17
        Me.lblF1.Text = "F1"
        '
        'lblF2
        '
        Me.lblF2.AutoSize = True
        Me.lblF2.Location = New System.Drawing.Point(618, 9)
        Me.lblF2.Name = "lblF2"
        Me.lblF2.Size = New System.Drawing.Size(19, 13)
        Me.lblF2.TabIndex = 18
        Me.lblF2.Text = "F2"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(808, 9)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 19
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(808, 35)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 20
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'dtpNewPmtDeadLine
        '
        Me.dtpNewPmtDeadLine.CustomFormat = "dd MMM yy"
        Me.dtpNewPmtDeadLine.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNewPmtDeadLine.Location = New System.Drawing.Point(880, 583)
        Me.dtpNewPmtDeadLine.Name = "dtpNewPmtDeadLine"
        Me.dtpNewPmtDeadLine.Size = New System.Drawing.Size(81, 20)
        Me.dtpNewPmtDeadLine.TabIndex = 22
        '
        'lbkUpdatePmtDeadline
        '
        Me.lbkUpdatePmtDeadline.AutoSize = True
        Me.lbkUpdatePmtDeadline.Location = New System.Drawing.Point(877, 602)
        Me.lbkUpdatePmtDeadline.Name = "lbkUpdatePmtDeadline"
        Me.lbkUpdatePmtDeadline.Size = New System.Drawing.Size(127, 13)
        Me.lbkUpdatePmtDeadline.TabIndex = 21
        Me.lbkUpdatePmtDeadline.TabStop = True
        Me.lbkUpdatePmtDeadline.Text = "UpdatePaymemtDeadline"
        '
        'lbkLoadFromRAS
        '
        Me.lbkLoadFromRAS.AutoSize = True
        Me.lbkLoadFromRAS.Location = New System.Drawing.Point(855, 9)
        Me.lbkLoadFromRAS.Name = "lbkLoadFromRAS"
        Me.lbkLoadFromRAS.Size = New System.Drawing.Size(76, 13)
        Me.lbkLoadFromRAS.TabIndex = 25
        Me.lbkLoadFromRAS.TabStop = True
        Me.lbkLoadFromRAS.Text = "LoadFromRAS"
        '
        'txtTktItem
        '
        Me.txtTktItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTktItem.Location = New System.Drawing.Point(858, 32)
        Me.txtTktItem.Name = "txtTktItem"
        Me.txtTktItem.Size = New System.Drawing.Size(146, 20)
        Me.txtTktItem.TabIndex = 26
        '
        'cboRasDoc
        '
        Me.cboRasDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRasDoc.FormattingEnabled = True
        Me.cboRasDoc.Items.AddRange(New Object() {"TRX", "TCODE", "TKT", "AHC"})
        Me.cboRasDoc.Location = New System.Drawing.Point(942, 5)
        Me.cboRasDoc.Name = "cboRasDoc"
        Me.cboRasDoc.Size = New System.Drawing.Size(62, 21)
        Me.cboRasDoc.TabIndex = 27
        '
        'txtInvAmt
        '
        Me.txtInvAmt.Location = New System.Drawing.Point(568, 594)
        Me.txtInvAmt.Name = "txtInvAmt"
        Me.txtInvAmt.Size = New System.Drawing.Size(100, 20)
        Me.txtInvAmt.TabIndex = 28
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(568, 578)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "InvoiceAmount"
        '
        'cboSelectCount
        '
        Me.cboSelectCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSelectCount.FormattingEnabled = True
        Me.cboSelectCount.Items.AddRange(New Object() {"10", "999"})
        Me.cboSelectCount.Location = New System.Drawing.Point(101, 593)
        Me.cboSelectCount.Name = "cboSelectCount"
        Me.cboSelectCount.Size = New System.Drawing.Size(39, 21)
        Me.cboSelectCount.TabIndex = 30
        '
        'lbkCreateE_Invoice
        '
        Me.lbkCreateE_Invoice.AutoSize = True
        Me.lbkCreateE_Invoice.Location = New System.Drawing.Point(155, 600)
        Me.lbkCreateE_Invoice.Name = "lbkCreateE_Invoice"
        Me.lbkCreateE_Invoice.Size = New System.Drawing.Size(86, 13)
        Me.lbkCreateE_Invoice.TabIndex = 73
        Me.lbkCreateE_Invoice.TabStop = True
        Me.lbkCreateE_Invoice.Text = "CreateE_Invoice"
        '
        'lbkSwitchVatPct
        '
        Me.lbkSwitchVatPct.AutoSize = True
        Me.lbkSwitchVatPct.Location = New System.Drawing.Point(379, 601)
        Me.lbkSwitchVatPct.Name = "lbkSwitchVatPct"
        Me.lbkSwitchVatPct.Size = New System.Drawing.Size(71, 13)
        Me.lbkSwitchVatPct.TabIndex = 112
        Me.lbkSwitchVatPct.TabStop = True
        Me.lbkSwitchVatPct.Text = "SwitchVatPct"
        '
        'cboVatPct
        '
        Me.cboVatPct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVatPct.FormattingEnabled = True
        Me.cboVatPct.Items.AddRange(New Object() {"VAT_NoDiscount", "VAT7"})
        Me.cboVatPct.Location = New System.Drawing.Point(256, 594)
        Me.cboVatPct.Name = "cboVatPct"
        Me.cboVatPct.Size = New System.Drawing.Size(117, 21)
        Me.cboVatPct.TabIndex = 111
        '
        'frmVatInvoicePrint4Cwt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 623)
        Me.Controls.Add(Me.lbkSwitchVatPct)
        Me.Controls.Add(Me.cboVatPct)
        Me.Controls.Add(Me.lbkCreateE_Invoice)
        Me.Controls.Add(Me.cboSelectCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtInvAmt)
        Me.Controls.Add(Me.cboRasDoc)
        Me.Controls.Add(Me.txtTktItem)
        Me.Controls.Add(Me.lbkLoadFromRAS)
        Me.Controls.Add(Me.dtpNewPmtDeadLine)
        Me.Controls.Add(Me.lbkUpdatePmtDeadline)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.lblF2)
        Me.Controls.Add(Me.lblF1)
        Me.Controls.Add(Me.cboF2)
        Me.Controls.Add(Me.cboF1)
        Me.Controls.Add(Me.dgrInvDetails)
        Me.Controls.Add(Me.grbUpdate)
        Me.Controls.Add(Me.lbkSelectAll)
        Me.Controls.Add(Me.cboSheetName)
        Me.Controls.Add(Me.dgrTktListing)
        Me.Controls.Add(Me.lbkLoadFile)
        Me.Controls.Add(Me.cboCustomer)
        Me.Name = "frmVatInvoicePrint4Cwt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VatInvoicePrint4Cwt"
        CType(Me.dgrTktListing, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbUpdate.ResumeLayout(False)
        Me.grbUpdate.PerformLayout()
        CType(Me.dgrInvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents lbkLoadFile As System.Windows.Forms.LinkLabel
    Friend WithEvents dgrTktListing As System.Windows.Forms.DataGridView
    Friend WithEvents cboSheetName As System.Windows.Forms.ComboBox
    Friend WithEvents lbkSelectAll As LinkLabel
    Friend WithEvents grbUpdate As GroupBox
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkClone As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents dgrInvDetails As DataGridView
    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents cboF1 As ComboBox
    Friend WithEvents cboF2 As ComboBox
    Friend WithEvents lblF1 As Label
    Friend WithEvents lblF2 As Label
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents dtpNewPmtDeadLine As DateTimePicker
    Friend WithEvents lbkUpdatePmtDeadline As LinkLabel
    Friend WithEvents lbkLoadFromRAS As LinkLabel
    Friend WithEvents txtTktItem As TextBox
    Friend WithEvents cboRasDoc As ComboBox
    Friend WithEvents txtInvAmt As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboSelectCount As ComboBox
    Friend WithEvents lbkCreateE_Invoice As LinkLabel
    Friend WithEvents lbkSwitchVatPct As LinkLabel
    Friend WithEvents cboVatPct As ComboBox
End Class
