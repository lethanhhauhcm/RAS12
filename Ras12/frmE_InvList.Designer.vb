<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmE_InvList
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
        Me.dgrE_Invoices = New System.Windows.Forms.DataGridView()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboCustGroup = New System.Windows.Forms.ComboBox()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCustType = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cboBiz = New System.Windows.Forms.ComboBox()
        Me.lblAL = New System.Windows.Forms.Label()
        Me.cboAL = New System.Windows.Forms.ComboBox()
        Me.lblTVC = New System.Windows.Forms.Label()
        Me.cboTVC = New System.Windows.Forms.ComboBox()
        Me.dgrE_InvDetails = New System.Windows.Forms.DataGridView()
        Me.dgrE_InvLinks = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.txtInvId = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbkApproveDraft = New System.Windows.Forms.LinkLabel()
        Me.lbkDownloadInvPDFNoPay = New System.Windows.Forms.LinkLabel()
        Me.lbkResendEmail = New System.Windows.Forms.LinkLabel()
        Me.chkDraft = New System.Windows.Forms.CheckBox()
        Me.lbkDownloadHtml = New System.Windows.Forms.LinkLabel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboMauSo = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboKyHieu = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lbkAdjustIncrease = New System.Windows.Forms.LinkLabel()
        Me.lbkAdjustDecrease = New System.Windows.Forms.LinkLabel()
        Me.lbkAdjustInfo = New System.Windows.Forms.LinkLabel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboBackDate = New System.Windows.Forms.ComboBox()
        Me.lbkAdjustInvoiceNote = New System.Windows.Forms.LinkLabel()
        Me.lbkViewNoPay = New System.Windows.Forms.LinkLabel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboInvType = New System.Windows.Forms.ComboBox()
        Me.lbkSyncFromVNPT = New System.Windows.Forms.LinkLabel()
        Me.lbkDeleteDraft = New System.Windows.Forms.LinkLabel()
        Me.pnlAdjust = New System.Windows.Forms.Panel()
        Me.LinkLabel4 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.lbkDownloadXml = New System.Windows.Forms.LinkLabel()
        Me.lbkViewRelated = New System.Windows.Forms.LinkLabel()
        Me.txtTkno = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.pnlDraft = New System.Windows.Forms.Panel()
        Me.pnlDownload = New System.Windows.Forms.Panel()
        Me.pnlEdit = New System.Windows.Forms.Panel()
        Me.chkFakeTkno = New System.Windows.Forms.CheckBox()
        Me.lbkReplaceInv = New System.Windows.Forms.LinkLabel()
        Me.txtRcpNo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        CType(Me.dgrE_Invoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrE_InvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrE_InvLinks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAdjust.SuspendLayout()
        Me.pnlDraft.SuspendLayout()
        Me.pnlDownload.SuspendLayout()
        Me.pnlEdit.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgrE_Invoices
        '
        Me.dgrE_Invoices.AllowUserToAddRows = False
        Me.dgrE_Invoices.AllowUserToDeleteRows = False
        Me.dgrE_Invoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrE_Invoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrE_Invoices.Location = New System.Drawing.Point(3, 93)
        Me.dgrE_Invoices.Name = "dgrE_Invoices"
        Me.dgrE_Invoices.ReadOnly = True
        Me.dgrE_Invoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgrE_Invoices.Size = New System.Drawing.Size(979, 349)
        Me.dgrE_Invoices.TabIndex = 3
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(951, 48)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 23
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(899, 48)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 22
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(233, 66)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(192, 21)
        Me.cboCustomer.TabIndex = 21
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(229, 48)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(51, 13)
        Me.lblCustomer.TabIndex = 24
        Me.lblCustomer.Text = "Customer"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(55, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "CustomerGroups"
        '
        'cboCustGroup
        '
        Me.cboCustGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustGroup.FormattingEnabled = True
        Me.cboCustGroup.Location = New System.Drawing.Point(58, 66)
        Me.cboCustGroup.Name = "cboCustGroup"
        Me.cboCustGroup.Size = New System.Drawing.Size(168, 21)
        Me.cboCustGroup.TabIndex = 27
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(52, 3)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(73, 13)
        Me.lbkDelete.TabIndex = 32
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "DeleteRecord"
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(3, 3)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 31
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(12, 589)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 29
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(0, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "CustType"
        '
        'cboCustType
        '
        Me.cboCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustType.FormattingEnabled = True
        Me.cboCustType.Items.AddRange(New Object() {"CA", "CS", "LC", "TA", "TO", "WK"})
        Me.cboCustType.Location = New System.Drawing.Point(3, 66)
        Me.cboCustType.Name = "cboCustType"
        Me.cboCustType.Size = New System.Drawing.Size(49, 21)
        Me.cboCustType.TabIndex = 33
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(92, 9)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(24, 13)
        Me.Label15.TabIndex = 127
        Me.Label15.Text = "BIZ"
        '
        'cboBiz
        '
        Me.cboBiz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBiz.FormattingEnabled = True
        Me.cboBiz.Location = New System.Drawing.Point(95, 25)
        Me.cboBiz.Name = "cboBiz"
        Me.cboBiz.Size = New System.Drawing.Size(89, 21)
        Me.cboBiz.TabIndex = 126
        '
        'lblAL
        '
        Me.lblAL.AutoSize = True
        Me.lblAL.Location = New System.Drawing.Point(187, 9)
        Me.lblAL.Name = "lblAL"
        Me.lblAL.Size = New System.Drawing.Size(20, 13)
        Me.lblAL.TabIndex = 125
        Me.lblAL.Text = "AL"
        '
        'cboAL
        '
        Me.cboAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAL.FormattingEnabled = True
        Me.cboAL.Location = New System.Drawing.Point(190, 25)
        Me.cboAL.Name = "cboAL"
        Me.cboAL.Size = New System.Drawing.Size(37, 21)
        Me.cboAL.TabIndex = 124
        '
        'lblTVC
        '
        Me.lblTVC.AutoSize = True
        Me.lblTVC.Location = New System.Drawing.Point(0, 10)
        Me.lblTVC.Name = "lblTVC"
        Me.lblTVC.Size = New System.Drawing.Size(28, 13)
        Me.lblTVC.TabIndex = 123
        Me.lblTVC.Text = "TVC"
        '
        'cboTVC
        '
        Me.cboTVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTVC.FormattingEnabled = True
        Me.cboTVC.Location = New System.Drawing.Point(3, 26)
        Me.cboTVC.Name = "cboTVC"
        Me.cboTVC.Size = New System.Drawing.Size(86, 21)
        Me.cboTVC.TabIndex = 122
        '
        'dgrE_InvDetails
        '
        Me.dgrE_InvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrE_InvDetails.Location = New System.Drawing.Point(3, 448)
        Me.dgrE_InvDetails.Name = "dgrE_InvDetails"
        Me.dgrE_InvDetails.Size = New System.Drawing.Size(480, 105)
        Me.dgrE_InvDetails.TabIndex = 128
        '
        'dgrE_InvLinks
        '
        Me.dgrE_InvLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrE_InvLinks.Location = New System.Drawing.Point(502, 448)
        Me.dgrE_InvLinks.Name = "dgrE_InvLinks"
        Me.dgrE_InvLinks.Size = New System.Drawing.Size(480, 105)
        Me.dgrE_InvLinks.TabIndex = 129
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(427, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 130
        Me.Label3.Text = "InvoiceNo"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(430, 26)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(53, 20)
        Me.txtInvoiceNo.TabIndex = 131
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(492, 26)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(143, 20)
        Me.txtDesc.TabIndex = 133
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(489, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 132
        Me.Label4.Text = "ProductDesc"
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(80, 589)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(78, 13)
        Me.lbkCancel.TabIndex = 134
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel Invoice"
        '
        'txtInvId
        '
        Me.txtInvId.Location = New System.Drawing.Point(641, 26)
        Me.txtInvId.Name = "txtInvId"
        Me.txtInvId.Size = New System.Drawing.Size(53, 20)
        Me.txtInvId.TabIndex = 136
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(638, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 135
        Me.Label5.Text = "InvoiceId"
        '
        'lbkApproveDraft
        '
        Me.lbkApproveDraft.AutoSize = True
        Me.lbkApproveDraft.Location = New System.Drawing.Point(3, 0)
        Me.lbkApproveDraft.Name = "lbkApproveDraft"
        Me.lbkApproveDraft.Size = New System.Drawing.Size(70, 13)
        Me.lbkApproveDraft.TabIndex = 137
        Me.lbkApproveDraft.TabStop = True
        Me.lbkApproveDraft.Text = "ApproveDraft"
        '
        'lbkDownloadInvPDFNoPay
        '
        Me.lbkDownloadInvPDFNoPay.AutoSize = True
        Me.lbkDownloadInvPDFNoPay.Location = New System.Drawing.Point(2, 8)
        Me.lbkDownloadInvPDFNoPay.Name = "lbkDownloadInvPDFNoPay"
        Me.lbkDownloadInvPDFNoPay.Size = New System.Drawing.Size(76, 13)
        Me.lbkDownloadInvPDFNoPay.TabIndex = 140
        Me.lbkDownloadInvPDFNoPay.TabStop = True
        Me.lbkDownloadInvPDFNoPay.Text = "DownloadPDF"
        '
        'lbkResendEmail
        '
        Me.lbkResendEmail.AutoSize = True
        Me.lbkResendEmail.Location = New System.Drawing.Point(665, 556)
        Me.lbkResendEmail.Name = "lbkResendEmail"
        Me.lbkResendEmail.Size = New System.Drawing.Size(69, 13)
        Me.lbkResendEmail.TabIndex = 142
        Me.lbkResendEmail.TabStop = True
        Me.lbkResendEmail.Text = "ResendEmail"
        '
        'chkDraft
        '
        Me.chkDraft.AutoSize = True
        Me.chkDraft.Location = New System.Drawing.Point(700, 12)
        Me.chkDraft.Name = "chkDraft"
        Me.chkDraft.Size = New System.Drawing.Size(49, 17)
        Me.chkDraft.TabIndex = 143
        Me.chkDraft.Text = "Draft"
        Me.chkDraft.ThreeState = True
        Me.chkDraft.UseVisualStyleBackColor = True
        '
        'lbkDownloadHtml
        '
        Me.lbkDownloadHtml.AutoSize = True
        Me.lbkDownloadHtml.Location = New System.Drawing.Point(84, 8)
        Me.lbkDownloadHtml.Name = "lbkDownloadHtml"
        Me.lbkDownloadHtml.Size = New System.Drawing.Size(74, 13)
        Me.lbkDownloadHtml.TabIndex = 144
        Me.lbkDownloadHtml.TabStop = True
        Me.lbkDownloadHtml.Text = "DownloadHtm"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(230, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 146
        Me.Label6.Text = "MauSo"
        '
        'cboMauSo
        '
        Me.cboMauSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMauSo.FormattingEnabled = True
        Me.cboMauSo.Location = New System.Drawing.Point(233, 25)
        Me.cboMauSo.Name = "cboMauSo"
        Me.cboMauSo.Size = New System.Drawing.Size(121, 21)
        Me.cboMauSo.TabIndex = 145
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(357, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 148
        Me.Label7.Text = "KyHieu"
        '
        'cboKyHieu
        '
        Me.cboKyHieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKyHieu.FormattingEnabled = True
        Me.cboKyHieu.Location = New System.Drawing.Point(359, 26)
        Me.cboKyHieu.Name = "cboKyHieu"
        Me.cboKyHieu.Size = New System.Drawing.Size(65, 21)
        Me.cboKyHieu.TabIndex = 147
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(427, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 150
        Me.Label8.Text = "Status"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "EX", "XX"})
        Me.cboStatus.Location = New System.Drawing.Point(431, 66)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(52, 21)
        Me.cboStatus.TabIndex = 149
        '
        'lbkAdjustIncrease
        '
        Me.lbkAdjustIncrease.AutoSize = True
        Me.lbkAdjustIncrease.Location = New System.Drawing.Point(6, 0)
        Me.lbkAdjustIncrease.Name = "lbkAdjustIncrease"
        Me.lbkAdjustIncrease.Size = New System.Drawing.Size(42, 13)
        Me.lbkAdjustIncrease.TabIndex = 151
        Me.lbkAdjustIncrease.TabStop = True
        Me.lbkAdjustIncrease.Text = "Adjust+"
        '
        'lbkAdjustDecrease
        '
        Me.lbkAdjustDecrease.AutoSize = True
        Me.lbkAdjustDecrease.Location = New System.Drawing.Point(68, 0)
        Me.lbkAdjustDecrease.Name = "lbkAdjustDecrease"
        Me.lbkAdjustDecrease.Size = New System.Drawing.Size(39, 13)
        Me.lbkAdjustDecrease.TabIndex = 152
        Me.lbkAdjustDecrease.TabStop = True
        Me.lbkAdjustDecrease.Text = "Adjust-"
        '
        'lbkAdjustInfo
        '
        Me.lbkAdjustInfo.AutoSize = True
        Me.lbkAdjustInfo.Location = New System.Drawing.Point(139, 0)
        Me.lbkAdjustInfo.Name = "lbkAdjustInfo"
        Me.lbkAdjustInfo.Size = New System.Drawing.Size(54, 13)
        Me.lbkAdjustInfo.TabIndex = 153
        Me.lbkAdjustInfo.TabStop = True
        Me.lbkAdjustInfo.Text = "AdjustInfo"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(781, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 13)
        Me.Label9.TabIndex = 155
        Me.Label9.Text = "BackDate(Days)"
        '
        'cboBackDate
        '
        Me.cboBackDate.FormattingEnabled = True
        Me.cboBackDate.Items.AddRange(New Object() {"7", "30", "90", "366", "999"})
        Me.cboBackDate.Location = New System.Drawing.Point(785, 25)
        Me.cboBackDate.Name = "cboBackDate"
        Me.cboBackDate.Size = New System.Drawing.Size(45, 21)
        Me.cboBackDate.TabIndex = 154
        '
        'lbkAdjustInvoiceNote
        '
        Me.lbkAdjustInvoiceNote.AutoSize = True
        Me.lbkAdjustInvoiceNote.Location = New System.Drawing.Point(224, 0)
        Me.lbkAdjustInvoiceNote.Name = "lbkAdjustInvoiceNote"
        Me.lbkAdjustInvoiceNote.Size = New System.Drawing.Size(94, 13)
        Me.lbkAdjustInvoiceNote.TabIndex = 156
        Me.lbkAdjustInvoiceNote.TabStop = True
        Me.lbkAdjustInvoiceNote.Text = "AdjustInvoiceNote"
        '
        'lbkViewNoPay
        '
        Me.lbkViewNoPay.AutoSize = True
        Me.lbkViewNoPay.Location = New System.Drawing.Point(505, 589)
        Me.lbkViewNoPay.Name = "lbkViewNoPay"
        Me.lbkViewNoPay.Size = New System.Drawing.Size(30, 13)
        Me.lbkViewNoPay.TabIndex = 157
        Me.lbkViewNoPay.TabStop = True
        Me.lbkViewNoPay.Text = "View"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(781, 50)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 13)
        Me.Label10.TabIndex = 160
        Me.Label10.Text = "InvType"
        '
        'cboInvType
        '
        Me.cboInvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvType.Enabled = False
        Me.cboInvType.FormattingEnabled = True
        Me.cboInvType.Items.AddRange(New Object() {"Original", "Adjust", "AdjustInfo", "Adjust+", "Adjust-", "AdjustNote", "IsAdjusted", "ThieuMaCoQuanThue"})
        Me.cboInvType.Location = New System.Drawing.Point(785, 66)
        Me.cboInvType.Name = "cboInvType"
        Me.cboInvType.Size = New System.Drawing.Size(107, 21)
        Me.cboInvType.TabIndex = 159
        '
        'lbkSyncFromVNPT
        '
        Me.lbkSyncFromVNPT.AutoSize = True
        Me.lbkSyncFromVNPT.Location = New System.Drawing.Point(899, 9)
        Me.lbkSyncFromVNPT.Name = "lbkSyncFromVNPT"
        Me.lbkSyncFromVNPT.Size = New System.Drawing.Size(83, 13)
        Me.lbkSyncFromVNPT.TabIndex = 162
        Me.lbkSyncFromVNPT.TabStop = True
        Me.lbkSyncFromVNPT.Text = "SyncFromVNPT"
        '
        'lbkDeleteDraft
        '
        Me.lbkDeleteDraft.AutoSize = True
        Me.lbkDeleteDraft.Location = New System.Drawing.Point(79, 0)
        Me.lbkDeleteDraft.Name = "lbkDeleteDraft"
        Me.lbkDeleteDraft.Size = New System.Drawing.Size(61, 13)
        Me.lbkDeleteDraft.TabIndex = 163
        Me.lbkDeleteDraft.TabStop = True
        Me.lbkDeleteDraft.Text = "DeleteDraft"
        '
        'pnlAdjust
        '
        Me.pnlAdjust.Controls.Add(Me.LinkLabel4)
        Me.pnlAdjust.Controls.Add(Me.lbkAdjustInfo)
        Me.pnlAdjust.Controls.Add(Me.LinkLabel3)
        Me.pnlAdjust.Controls.Add(Me.LinkLabel2)
        Me.pnlAdjust.Controls.Add(Me.lbkAdjustIncrease)
        Me.pnlAdjust.Controls.Add(Me.LinkLabel1)
        Me.pnlAdjust.Controls.Add(Me.lbkAdjustDecrease)
        Me.pnlAdjust.Controls.Add(Me.lbkAdjustInvoiceNote)
        Me.pnlAdjust.Location = New System.Drawing.Point(12, 559)
        Me.pnlAdjust.Name = "pnlAdjust"
        Me.pnlAdjust.Size = New System.Drawing.Size(320, 24)
        Me.pnlAdjust.TabIndex = 164
        Me.pnlAdjust.Visible = False
        '
        'LinkLabel4
        '
        Me.LinkLabel4.AutoSize = True
        Me.LinkLabel4.Location = New System.Drawing.Point(139, 3)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Size = New System.Drawing.Size(54, 13)
        Me.LinkLabel4.TabIndex = 153
        Me.LinkLabel4.TabStop = True
        Me.LinkLabel4.Text = "AdjustInfo"
        '
        'LinkLabel3
        '
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.Location = New System.Drawing.Point(6, 3)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(42, 13)
        Me.LinkLabel3.TabIndex = 151
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "Adjust+"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(68, 3)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(39, 13)
        Me.LinkLabel2.TabIndex = 152
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Adjust-"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(224, 3)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(94, 13)
        Me.LinkLabel1.TabIndex = 156
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "AdjustInvoiceNote"
        '
        'lbkDownloadXml
        '
        Me.lbkDownloadXml.AutoSize = True
        Me.lbkDownloadXml.Location = New System.Drawing.Point(164, 8)
        Me.lbkDownloadXml.Name = "lbkDownloadXml"
        Me.lbkDownloadXml.Size = New System.Drawing.Size(72, 13)
        Me.lbkDownloadXml.TabIndex = 165
        Me.lbkDownloadXml.TabStop = True
        Me.lbkDownloadXml.Text = "DownloadXml"
        '
        'lbkViewRelated
        '
        Me.lbkViewRelated.AutoSize = True
        Me.lbkViewRelated.Location = New System.Drawing.Point(544, 589)
        Me.lbkViewRelated.Name = "lbkViewRelated"
        Me.lbkViewRelated.Size = New System.Drawing.Size(82, 13)
        Me.lbkViewRelated.TabIndex = 166
        Me.lbkViewRelated.TabStop = True
        Me.lbkViewRelated.Text = "ViewRelatedInv"
        '
        'txtTkno
        '
        Me.txtTkno.Location = New System.Drawing.Point(492, 65)
        Me.txtTkno.Name = "txtTkno"
        Me.txtTkno.Size = New System.Drawing.Size(143, 20)
        Me.txtTkno.TabIndex = 168
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(489, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(37, 13)
        Me.Label11.TabIndex = 167
        Me.Label11.Text = "TKNO"
        '
        'pnlDraft
        '
        Me.pnlDraft.Controls.Add(Me.lbkDeleteDraft)
        Me.pnlDraft.Controls.Add(Me.lbkApproveDraft)
        Me.pnlDraft.Location = New System.Drawing.Point(502, 559)
        Me.pnlDraft.Name = "pnlDraft"
        Me.pnlDraft.Size = New System.Drawing.Size(146, 28)
        Me.pnlDraft.TabIndex = 169
        '
        'pnlDownload
        '
        Me.pnlDownload.Controls.Add(Me.lbkDownloadInvPDFNoPay)
        Me.pnlDownload.Controls.Add(Me.lbkDownloadHtml)
        Me.pnlDownload.Controls.Add(Me.lbkDownloadXml)
        Me.pnlDownload.Location = New System.Drawing.Point(668, 578)
        Me.pnlDownload.Name = "pnlDownload"
        Me.pnlDownload.Size = New System.Drawing.Size(240, 24)
        Me.pnlDownload.TabIndex = 170
        '
        'pnlEdit
        '
        Me.pnlEdit.Controls.Add(Me.lbkDelete)
        Me.pnlEdit.Controls.Add(Me.lbkEdit)
        Me.pnlEdit.Location = New System.Drawing.Point(239, 586)
        Me.pnlEdit.Name = "pnlEdit"
        Me.pnlEdit.Size = New System.Drawing.Size(144, 24)
        Me.pnlEdit.TabIndex = 171
        '
        'chkFakeTkno
        '
        Me.chkFakeTkno.AutoSize = True
        Me.chkFakeTkno.Location = New System.Drawing.Point(700, 29)
        Me.chkFakeTkno.Name = "chkFakeTkno"
        Me.chkFakeTkno.Size = New System.Drawing.Size(75, 17)
        Me.chkFakeTkno.TabIndex = 172
        Me.chkFakeTkno.Text = "FakeTkno"
        Me.chkFakeTkno.ThreeState = True
        Me.chkFakeTkno.UseVisualStyleBackColor = True
        '
        'lbkReplaceInv
        '
        Me.lbkReplaceInv.AutoSize = True
        Me.lbkReplaceInv.Location = New System.Drawing.Point(370, 562)
        Me.lbkReplaceInv.Name = "lbkReplaceInv"
        Me.lbkReplaceInv.Size = New System.Drawing.Size(105, 13)
        Me.lbkReplaceInv.TabIndex = 173
        Me.lbkReplaceInv.TabStop = True
        Me.lbkReplaceInv.Text = "ReplaceInvoiceNote"
        '
        'txtRcpNo
        '
        Me.txtRcpNo.Location = New System.Drawing.Point(641, 66)
        Me.txtRcpNo.Name = "txtRcpNo"
        Me.txtRcpNo.Size = New System.Drawing.Size(124, 20)
        Me.txtRcpNo.TabIndex = 175
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(638, 50)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(41, 13)
        Me.Label12.TabIndex = 174
        Me.Label12.Text = "RcpNo"
        '
        'frmE_InvList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.txtRcpNo)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.lbkReplaceInv)
        Me.Controls.Add(Me.chkFakeTkno)
        Me.Controls.Add(Me.pnlEdit)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.pnlDownload)
        Me.Controls.Add(Me.pnlDraft)
        Me.Controls.Add(Me.txtTkno)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lbkViewRelated)
        Me.Controls.Add(Me.pnlAdjust)
        Me.Controls.Add(Me.lbkSyncFromVNPT)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cboInvType)
        Me.Controls.Add(Me.lbkViewNoPay)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboBackDate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboKyHieu)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboMauSo)
        Me.Controls.Add(Me.chkDraft)
        Me.Controls.Add(Me.lbkResendEmail)
        Me.Controls.Add(Me.txtInvId)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgrE_InvLinks)
        Me.Controls.Add(Me.dgrE_InvDetails)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cboBiz)
        Me.Controls.Add(Me.lblAL)
        Me.Controls.Add(Me.cboAL)
        Me.Controls.Add(Me.lblTVC)
        Me.Controls.Add(Me.cboTVC)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboCustType)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCustGroup)
        Me.Controls.Add(Me.lblCustomer)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.dgrE_Invoices)
        Me.Name = "frmE_InvList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "E_InvList"
        CType(Me.dgrE_Invoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrE_InvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrE_InvLinks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAdjust.ResumeLayout(False)
        Me.pnlAdjust.PerformLayout()
        Me.pnlDraft.ResumeLayout(False)
        Me.pnlDraft.PerformLayout()
        Me.pnlDownload.ResumeLayout(False)
        Me.pnlDownload.PerformLayout()
        Me.pnlEdit.ResumeLayout(False)
        Me.pnlEdit.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrE_Invoices As DataGridView
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents lblCustomer As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cboCustGroup As ComboBox
    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents cboCustType As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents cboBiz As ComboBox
    Friend WithEvents lblAL As Label
    Friend WithEvents cboAL As ComboBox
    Friend WithEvents lblTVC As Label
    Friend WithEvents cboTVC As ComboBox
    Friend WithEvents dgrE_InvDetails As DataGridView
    Friend WithEvents dgrE_InvLinks As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents txtDesc As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents txtInvId As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents lbkApproveDraft As LinkLabel
    Friend WithEvents lbkDownloadInvPDFNoPay As LinkLabel
    Friend WithEvents lbkResendEmail As LinkLabel
    Friend WithEvents chkDraft As CheckBox
    Friend WithEvents lbkDownloadHtml As LinkLabel
    Friend WithEvents Label6 As Label
    Friend WithEvents cboMauSo As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cboKyHieu As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents lbkAdjustIncrease As LinkLabel
    Friend WithEvents lbkAdjustDecrease As LinkLabel
    Friend WithEvents lbkAdjustInfo As LinkLabel
    Friend WithEvents Label9 As Label
    Friend WithEvents cboBackDate As ComboBox
    Friend WithEvents lbkAdjustInvoiceNote As LinkLabel
    Friend WithEvents lbkViewNoPay As LinkLabel
    Friend WithEvents Label10 As Label
    Friend WithEvents cboInvType As ComboBox
    Friend WithEvents lbkSyncFromVNPT As LinkLabel
    Friend WithEvents lbkDeleteDraft As LinkLabel
    Friend WithEvents pnlAdjust As Panel
    Friend WithEvents lbkDownloadXml As LinkLabel
    Friend WithEvents lbkViewRelated As LinkLabel
    Friend WithEvents txtTkno As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents pnlDraft As Panel
    Friend WithEvents pnlDownload As Panel
    Friend WithEvents pnlEdit As Panel
    Friend WithEvents LinkLabel4 As LinkLabel
    Friend WithEvents LinkLabel3 As LinkLabel
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents chkFakeTkno As CheckBox
    Friend WithEvents lbkReplaceInv As LinkLabel
    Friend WithEvents txtRcpNo As TextBox
    Friend WithEvents Label12 As Label
End Class
