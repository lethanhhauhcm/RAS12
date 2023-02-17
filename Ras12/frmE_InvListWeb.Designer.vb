<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmE_InvListWeb
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
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboKyHieu = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboMauSo = New System.Windows.Forms.ComboBox()
        Me.lbkViewInvoiceFromWeb = New System.Windows.Forms.LinkLabel()
        Me.lbkResendEmail = New System.Windows.Forms.LinkLabel()
        Me.lbkSyncFromWeb = New System.Windows.Forms.LinkLabel()
        Me.lbkDownloadInvPDFNoPay = New System.Windows.Forms.LinkLabel()
        Me.lbkDownloadInvPDF = New System.Windows.Forms.LinkLabel()
        Me.lbkDownloadInv = New System.Windows.Forms.LinkLabel()
        Me.txtInvId = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgrE_InvDetails = New System.Windows.Forms.DataGridView()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cboBiz = New System.Windows.Forms.ComboBox()
        Me.lblAL = New System.Windows.Forms.Label()
        Me.cboAL = New System.Windows.Forms.ComboBox()
        Me.lblTVC = New System.Windows.Forms.Label()
        Me.cboTVC = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCustType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboCustGroup = New System.Windows.Forms.ComboBox()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.dgrE_Invoices = New System.Windows.Forms.DataGridView()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboBackDate = New System.Windows.Forms.ComboBox()
        Me.lbkFindMissing = New System.Windows.Forms.LinkLabel()
        Me.lbkSync2RAS = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrE_InvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrE_Invoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(358, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 188
        Me.Label7.Text = "KyHieu"
        '
        'cboKyHieu
        '
        Me.cboKyHieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKyHieu.FormattingEnabled = True
        Me.cboKyHieu.Location = New System.Drawing.Point(360, 26)
        Me.cboKyHieu.Name = "cboKyHieu"
        Me.cboKyHieu.Size = New System.Drawing.Size(65, 21)
        Me.cboKyHieu.TabIndex = 187
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(231, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 186
        Me.Label6.Text = "MauSo"
        '
        'cboMauSo
        '
        Me.cboMauSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMauSo.FormattingEnabled = True
        Me.cboMauSo.Location = New System.Drawing.Point(234, 25)
        Me.cboMauSo.Name = "cboMauSo"
        Me.cboMauSo.Size = New System.Drawing.Size(121, 21)
        Me.cboMauSo.TabIndex = 185
        '
        'lbkViewInvoiceFromWeb
        '
        Me.lbkViewInvoiceFromWeb.AutoSize = True
        Me.lbkViewInvoiceFromWeb.Location = New System.Drawing.Point(483, 598)
        Me.lbkViewInvoiceFromWeb.Name = "lbkViewInvoiceFromWeb"
        Me.lbkViewInvoiceFromWeb.Size = New System.Drawing.Size(111, 13)
        Me.lbkViewInvoiceFromWeb.TabIndex = 184
        Me.lbkViewInvoiceFromWeb.TabStop = True
        Me.lbkViewInvoiceFromWeb.Text = "ViewInvoiceFromWeb"
        '
        'lbkResendEmail
        '
        Me.lbkResendEmail.AutoSize = True
        Me.lbkResendEmail.Location = New System.Drawing.Point(408, 598)
        Me.lbkResendEmail.Name = "lbkResendEmail"
        Me.lbkResendEmail.Size = New System.Drawing.Size(69, 13)
        Me.lbkResendEmail.TabIndex = 182
        Me.lbkResendEmail.TabStop = True
        Me.lbkResendEmail.Text = "ResendEmail"
        '
        'lbkSyncFromWeb
        '
        Me.lbkSyncFromWeb.AutoSize = True
        Me.lbkSyncFromWeb.Location = New System.Drawing.Point(1, 598)
        Me.lbkSyncFromWeb.Name = "lbkSyncFromWeb"
        Me.lbkSyncFromWeb.Size = New System.Drawing.Size(77, 13)
        Me.lbkSyncFromWeb.TabIndex = 181
        Me.lbkSyncFromWeb.TabStop = True
        Me.lbkSyncFromWeb.Text = "SyncFromWeb"
        '
        'lbkDownloadInvPDFNoPay
        '
        Me.lbkDownloadInvPDFNoPay.AutoSize = True
        Me.lbkDownloadInvPDFNoPay.Location = New System.Drawing.Point(279, 598)
        Me.lbkDownloadInvPDFNoPay.Name = "lbkDownloadInvPDFNoPay"
        Me.lbkDownloadInvPDFNoPay.Size = New System.Drawing.Size(123, 13)
        Me.lbkDownloadInvPDFNoPay.TabIndex = 180
        Me.lbkDownloadInvPDFNoPay.TabStop = True
        Me.lbkDownloadInvPDFNoPay.Text = "DownloadInvPDFNoPay"
        '
        'lbkDownloadInvPDF
        '
        Me.lbkDownloadInvPDF.AutoSize = True
        Me.lbkDownloadInvPDF.Location = New System.Drawing.Point(182, 598)
        Me.lbkDownloadInvPDF.Name = "lbkDownloadInvPDF"
        Me.lbkDownloadInvPDF.Size = New System.Drawing.Size(91, 13)
        Me.lbkDownloadInvPDF.TabIndex = 179
        Me.lbkDownloadInvPDF.TabStop = True
        Me.lbkDownloadInvPDF.Text = "DownloadInvPDF"
        '
        'lbkDownloadInv
        '
        Me.lbkDownloadInv.AutoSize = True
        Me.lbkDownloadInv.Location = New System.Drawing.Point(109, 598)
        Me.lbkDownloadInv.Name = "lbkDownloadInv"
        Me.lbkDownloadInv.Size = New System.Drawing.Size(70, 13)
        Me.lbkDownloadInv.TabIndex = 178
        Me.lbkDownloadInv.TabStop = True
        Me.lbkDownloadInv.Text = "DownloadInv"
        '
        'txtInvId
        '
        Me.txtInvId.Location = New System.Drawing.Point(490, 26)
        Me.txtInvId.Name = "txtInvId"
        Me.txtInvId.Size = New System.Drawing.Size(53, 20)
        Me.txtInvId.TabIndex = 176
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(487, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 175
        Me.Label5.Text = "InvoiceId"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(431, 26)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(53, 20)
        Me.txtInvoiceNo.TabIndex = 171
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(428, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 170
        Me.Label3.Text = "InvoiceNo"
        '
        'dgrE_InvDetails
        '
        Me.dgrE_InvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrE_InvDetails.Location = New System.Drawing.Point(4, 481)
        Me.dgrE_InvDetails.Name = "dgrE_InvDetails"
        Me.dgrE_InvDetails.Size = New System.Drawing.Size(979, 105)
        Me.dgrE_InvDetails.TabIndex = 168
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(93, 9)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(24, 13)
        Me.Label15.TabIndex = 167
        Me.Label15.Text = "BIZ"
        '
        'cboBiz
        '
        Me.cboBiz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBiz.FormattingEnabled = True
        Me.cboBiz.Location = New System.Drawing.Point(96, 25)
        Me.cboBiz.Name = "cboBiz"
        Me.cboBiz.Size = New System.Drawing.Size(89, 21)
        Me.cboBiz.TabIndex = 166
        '
        'lblAL
        '
        Me.lblAL.AutoSize = True
        Me.lblAL.Location = New System.Drawing.Point(188, 9)
        Me.lblAL.Name = "lblAL"
        Me.lblAL.Size = New System.Drawing.Size(20, 13)
        Me.lblAL.TabIndex = 165
        Me.lblAL.Text = "AL"
        '
        'cboAL
        '
        Me.cboAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAL.FormattingEnabled = True
        Me.cboAL.Location = New System.Drawing.Point(191, 25)
        Me.cboAL.Name = "cboAL"
        Me.cboAL.Size = New System.Drawing.Size(37, 21)
        Me.cboAL.TabIndex = 164
        '
        'lblTVC
        '
        Me.lblTVC.AutoSize = True
        Me.lblTVC.Location = New System.Drawing.Point(1, 10)
        Me.lblTVC.Name = "lblTVC"
        Me.lblTVC.Size = New System.Drawing.Size(28, 13)
        Me.lblTVC.TabIndex = 163
        Me.lblTVC.Text = "TVC"
        '
        'cboTVC
        '
        Me.cboTVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTVC.FormattingEnabled = True
        Me.cboTVC.Location = New System.Drawing.Point(4, 26)
        Me.cboTVC.Name = "cboTVC"
        Me.cboTVC.Size = New System.Drawing.Size(86, 21)
        Me.cboTVC.TabIndex = 162
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 161
        Me.Label2.Text = "CustType"
        '
        'cboCustType
        '
        Me.cboCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustType.FormattingEnabled = True
        Me.cboCustType.Items.AddRange(New Object() {"CA", "CS", "LC", "TA", "TO", "WK"})
        Me.cboCustType.Location = New System.Drawing.Point(4, 66)
        Me.cboCustType.Name = "cboCustType"
        Me.cboCustType.Size = New System.Drawing.Size(49, 21)
        Me.cboCustType.TabIndex = 160
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(56, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 155
        Me.Label1.Text = "CustomerGroups"
        '
        'cboCustGroup
        '
        Me.cboCustGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustGroup.FormattingEnabled = True
        Me.cboCustGroup.Location = New System.Drawing.Point(59, 66)
        Me.cboCustGroup.Name = "cboCustGroup"
        Me.cboCustGroup.Size = New System.Drawing.Size(168, 21)
        Me.cboCustGroup.TabIndex = 154
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(230, 48)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(51, 13)
        Me.lblCustomer.TabIndex = 153
        Me.lblCustomer.Text = "Customer"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(834, 48)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 152
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(834, 25)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 151
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(234, 66)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(192, 21)
        Me.cboCustomer.TabIndex = 150
        '
        'dgrE_Invoices
        '
        Me.dgrE_Invoices.AllowUserToAddRows = False
        Me.dgrE_Invoices.AllowUserToDeleteRows = False
        Me.dgrE_Invoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrE_Invoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrE_Invoices.Location = New System.Drawing.Point(4, 93)
        Me.dgrE_Invoices.Name = "dgrE_Invoices"
        Me.dgrE_Invoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgrE_Invoices.Size = New System.Drawing.Size(979, 382)
        Me.dgrE_Invoices.TabIndex = 149
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(428, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 13)
        Me.Label9.TabIndex = 190
        Me.Label9.Text = "BackDate(Days)"
        '
        'cboBackDate
        '
        Me.cboBackDate.FormattingEnabled = True
        Me.cboBackDate.Items.AddRange(New Object() {"7", "30", "90", "366", "999"})
        Me.cboBackDate.Location = New System.Drawing.Point(432, 66)
        Me.cboBackDate.Name = "cboBackDate"
        Me.cboBackDate.Size = New System.Drawing.Size(45, 21)
        Me.cboBackDate.TabIndex = 189
        '
        'lbkFindMissing
        '
        Me.lbkFindMissing.AutoSize = True
        Me.lbkFindMissing.Location = New System.Drawing.Point(881, 598)
        Me.lbkFindMissing.Name = "lbkFindMissing"
        Me.lbkFindMissing.Size = New System.Drawing.Size(102, 13)
        Me.lbkFindMissing.TabIndex = 191
        Me.lbkFindMissing.TabStop = True
        Me.lbkFindMissing.Text = "FindMissingInvoices"
        '
        'lbkSync2RAS
        '
        Me.lbkSync2RAS.AutoSize = True
        Me.lbkSync2RAS.Location = New System.Drawing.Point(630, 598)
        Me.lbkSync2RAS.Name = "lbkSync2RAS"
        Me.lbkSync2RAS.Size = New System.Drawing.Size(59, 13)
        Me.lbkSync2RAS.TabIndex = 192
        Me.lbkSync2RAS.TabStop = True
        Me.lbkSync2RAS.Text = "Sync2RAS"
        '
        'frmE_InvListWeb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.lbkSync2RAS)
        Me.Controls.Add(Me.lbkFindMissing)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboBackDate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboKyHieu)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboMauSo)
        Me.Controls.Add(Me.lbkViewInvoiceFromWeb)
        Me.Controls.Add(Me.lbkResendEmail)
        Me.Controls.Add(Me.lbkSyncFromWeb)
        Me.Controls.Add(Me.lbkDownloadInvPDFNoPay)
        Me.Controls.Add(Me.lbkDownloadInvPDF)
        Me.Controls.Add(Me.lbkDownloadInv)
        Me.Controls.Add(Me.txtInvId)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgrE_InvDetails)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cboBiz)
        Me.Controls.Add(Me.lblAL)
        Me.Controls.Add(Me.cboAL)
        Me.Controls.Add(Me.lblTVC)
        Me.Controls.Add(Me.cboTVC)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboCustType)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCustGroup)
        Me.Controls.Add(Me.lblCustomer)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.dgrE_Invoices)
        Me.Name = "frmE_InvListWeb"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "E_Invoice List in VNPT Web"
        CType(Me.dgrE_InvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrE_Invoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label7 As Label
    Friend WithEvents cboKyHieu As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cboMauSo As ComboBox
    Friend WithEvents lbkViewInvoiceFromWeb As LinkLabel
    Friend WithEvents lbkResendEmail As LinkLabel
    Friend WithEvents lbkSyncFromWeb As LinkLabel
    Friend WithEvents lbkDownloadInvPDFNoPay As LinkLabel
    Friend WithEvents lbkDownloadInvPDF As LinkLabel
    Friend WithEvents lbkDownloadInv As LinkLabel
    Friend WithEvents txtInvId As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dgrE_InvDetails As DataGridView
    Friend WithEvents Label15 As Label
    Friend WithEvents cboBiz As ComboBox
    Friend WithEvents lblAL As Label
    Friend WithEvents cboAL As ComboBox
    Friend WithEvents lblTVC As Label
    Friend WithEvents cboTVC As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboCustType As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cboCustGroup As ComboBox
    Friend WithEvents lblCustomer As Label
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents dgrE_Invoices As DataGridView
    Friend WithEvents Label9 As Label
    Friend WithEvents cboBackDate As ComboBox
    Friend WithEvents lbkFindMissing As LinkLabel
    Friend WithEvents lbkSync2RAS As LinkLabel
End Class
