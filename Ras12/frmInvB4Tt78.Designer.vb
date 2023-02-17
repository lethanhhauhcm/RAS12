<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvB4Tt78
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
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboBackDate = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboKyHieu = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboMauSo = New System.Windows.Forms.ComboBox()
        Me.lbkSyncFromWeb = New System.Windows.Forms.LinkLabel()
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
        Me.lbkAdjustInfo = New System.Windows.Forms.LinkLabel()
        Me.lbkAdjustDecrease = New System.Windows.Forms.LinkLabel()
        Me.lbkAdjustIncrease = New System.Windows.Forms.LinkLabel()
        Me.lbkFindMissing = New System.Windows.Forms.LinkLabel()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.lbkViewInvoiceFromWeb = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrE_InvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrE_Invoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(440, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 13)
        Me.Label9.TabIndex = 222
        Me.Label9.Text = "BackDate(Days)"
        '
        'cboBackDate
        '
        Me.cboBackDate.FormattingEnabled = True
        Me.cboBackDate.Items.AddRange(New Object() {"7", "30", "90", "366", "999"})
        Me.cboBackDate.Location = New System.Drawing.Point(444, 61)
        Me.cboBackDate.Name = "cboBackDate"
        Me.cboBackDate.Size = New System.Drawing.Size(45, 21)
        Me.cboBackDate.TabIndex = 221
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(370, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 220
        Me.Label7.Text = "KyHieu"
        '
        'cboKyHieu
        '
        Me.cboKyHieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKyHieu.FormattingEnabled = True
        Me.cboKyHieu.Location = New System.Drawing.Point(372, 21)
        Me.cboKyHieu.Name = "cboKyHieu"
        Me.cboKyHieu.Size = New System.Drawing.Size(65, 21)
        Me.cboKyHieu.TabIndex = 219
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(243, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 218
        Me.Label6.Text = "MauSo"
        '
        'cboMauSo
        '
        Me.cboMauSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMauSo.FormattingEnabled = True
        Me.cboMauSo.Location = New System.Drawing.Point(246, 20)
        Me.cboMauSo.Name = "cboMauSo"
        Me.cboMauSo.Size = New System.Drawing.Size(121, 21)
        Me.cboMauSo.TabIndex = 217
        '
        'lbkSyncFromWeb
        '
        Me.lbkSyncFromWeb.AutoSize = True
        Me.lbkSyncFromWeb.Location = New System.Drawing.Point(12, 589)
        Me.lbkSyncFromWeb.Name = "lbkSyncFromWeb"
        Me.lbkSyncFromWeb.Size = New System.Drawing.Size(77, 13)
        Me.lbkSyncFromWeb.TabIndex = 214
        Me.lbkSyncFromWeb.TabStop = True
        Me.lbkSyncFromWeb.Text = "SyncFromWeb"
        '
        'txtInvId
        '
        Me.txtInvId.Location = New System.Drawing.Point(502, 21)
        Me.txtInvId.Name = "txtInvId"
        Me.txtInvId.Size = New System.Drawing.Size(53, 20)
        Me.txtInvId.TabIndex = 210
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(499, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 209
        Me.Label5.Text = "InvoiceId"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(443, 21)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(53, 20)
        Me.txtInvoiceNo.TabIndex = 208
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(440, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 207
        Me.Label3.Text = "InvoiceNo"
        '
        'dgrE_InvDetails
        '
        Me.dgrE_InvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrE_InvDetails.Location = New System.Drawing.Point(16, 440)
        Me.dgrE_InvDetails.Name = "dgrE_InvDetails"
        Me.dgrE_InvDetails.Size = New System.Drawing.Size(979, 105)
        Me.dgrE_InvDetails.TabIndex = 206
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(105, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(24, 13)
        Me.Label15.TabIndex = 205
        Me.Label15.Text = "BIZ"
        '
        'cboBiz
        '
        Me.cboBiz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBiz.FormattingEnabled = True
        Me.cboBiz.Location = New System.Drawing.Point(108, 20)
        Me.cboBiz.Name = "cboBiz"
        Me.cboBiz.Size = New System.Drawing.Size(89, 21)
        Me.cboBiz.TabIndex = 204
        '
        'lblAL
        '
        Me.lblAL.AutoSize = True
        Me.lblAL.Location = New System.Drawing.Point(200, 4)
        Me.lblAL.Name = "lblAL"
        Me.lblAL.Size = New System.Drawing.Size(20, 13)
        Me.lblAL.TabIndex = 203
        Me.lblAL.Text = "AL"
        '
        'cboAL
        '
        Me.cboAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAL.FormattingEnabled = True
        Me.cboAL.Location = New System.Drawing.Point(203, 20)
        Me.cboAL.Name = "cboAL"
        Me.cboAL.Size = New System.Drawing.Size(37, 21)
        Me.cboAL.TabIndex = 202
        '
        'lblTVC
        '
        Me.lblTVC.AutoSize = True
        Me.lblTVC.Location = New System.Drawing.Point(13, 5)
        Me.lblTVC.Name = "lblTVC"
        Me.lblTVC.Size = New System.Drawing.Size(28, 13)
        Me.lblTVC.TabIndex = 201
        Me.lblTVC.Text = "TVC"
        '
        'cboTVC
        '
        Me.cboTVC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTVC.FormattingEnabled = True
        Me.cboTVC.Location = New System.Drawing.Point(16, 21)
        Me.cboTVC.Name = "cboTVC"
        Me.cboTVC.Size = New System.Drawing.Size(86, 21)
        Me.cboTVC.TabIndex = 200
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 199
        Me.Label2.Text = "CustType"
        '
        'cboCustType
        '
        Me.cboCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustType.FormattingEnabled = True
        Me.cboCustType.Items.AddRange(New Object() {"CA", "CS", "LC", "TA", "TO", "WK"})
        Me.cboCustType.Location = New System.Drawing.Point(16, 61)
        Me.cboCustType.Name = "cboCustType"
        Me.cboCustType.Size = New System.Drawing.Size(49, 21)
        Me.cboCustType.TabIndex = 198
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(68, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 197
        Me.Label1.Text = "CustomerGroups"
        '
        'cboCustGroup
        '
        Me.cboCustGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustGroup.FormattingEnabled = True
        Me.cboCustGroup.Location = New System.Drawing.Point(71, 61)
        Me.cboCustGroup.Name = "cboCustGroup"
        Me.cboCustGroup.Size = New System.Drawing.Size(168, 21)
        Me.cboCustGroup.TabIndex = 196
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(242, 43)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(51, 13)
        Me.lblCustomer.TabIndex = 195
        Me.lblCustomer.Text = "Customer"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(846, 43)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 194
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(846, 20)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 193
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(246, 61)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(192, 21)
        Me.cboCustomer.TabIndex = 192
        '
        'dgrE_Invoices
        '
        Me.dgrE_Invoices.AllowUserToAddRows = False
        Me.dgrE_Invoices.AllowUserToDeleteRows = False
        Me.dgrE_Invoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrE_Invoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrE_Invoices.Location = New System.Drawing.Point(16, 88)
        Me.dgrE_Invoices.Name = "dgrE_Invoices"
        Me.dgrE_Invoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgrE_Invoices.Size = New System.Drawing.Size(979, 346)
        Me.dgrE_Invoices.TabIndex = 191
        '
        'lbkAdjustInfo
        '
        Me.lbkAdjustInfo.AutoSize = True
        Me.lbkAdjustInfo.Location = New System.Drawing.Point(146, 557)
        Me.lbkAdjustInfo.Name = "lbkAdjustInfo"
        Me.lbkAdjustInfo.Size = New System.Drawing.Size(54, 13)
        Me.lbkAdjustInfo.TabIndex = 225
        Me.lbkAdjustInfo.TabStop = True
        Me.lbkAdjustInfo.Text = "AdjustInfo"
        '
        'lbkAdjustDecrease
        '
        Me.lbkAdjustDecrease.AutoSize = True
        Me.lbkAdjustDecrease.Location = New System.Drawing.Point(75, 557)
        Me.lbkAdjustDecrease.Name = "lbkAdjustDecrease"
        Me.lbkAdjustDecrease.Size = New System.Drawing.Size(39, 13)
        Me.lbkAdjustDecrease.TabIndex = 224
        Me.lbkAdjustDecrease.TabStop = True
        Me.lbkAdjustDecrease.Text = "Adjust-"
        '
        'lbkAdjustIncrease
        '
        Me.lbkAdjustIncrease.AutoSize = True
        Me.lbkAdjustIncrease.Location = New System.Drawing.Point(13, 557)
        Me.lbkAdjustIncrease.Name = "lbkAdjustIncrease"
        Me.lbkAdjustIncrease.Size = New System.Drawing.Size(42, 13)
        Me.lbkAdjustIncrease.TabIndex = 223
        Me.lbkAdjustIncrease.TabStop = True
        Me.lbkAdjustIncrease.Text = "Adjust+"
        '
        'lbkFindMissing
        '
        Me.lbkFindMissing.AutoSize = True
        Me.lbkFindMissing.Location = New System.Drawing.Point(95, 589)
        Me.lbkFindMissing.Name = "lbkFindMissing"
        Me.lbkFindMissing.Size = New System.Drawing.Size(102, 13)
        Me.lbkFindMissing.TabIndex = 226
        Me.lbkFindMissing.TabStop = True
        Me.lbkFindMissing.Text = "FindMissingInvoices"
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(220, 557)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 227
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'lbkViewInvoiceFromWeb
        '
        Me.lbkViewInvoiceFromWeb.AutoSize = True
        Me.lbkViewInvoiceFromWeb.Location = New System.Drawing.Point(268, 589)
        Me.lbkViewInvoiceFromWeb.Name = "lbkViewInvoiceFromWeb"
        Me.lbkViewInvoiceFromWeb.Size = New System.Drawing.Size(111, 13)
        Me.lbkViewInvoiceFromWeb.TabIndex = 228
        Me.lbkViewInvoiceFromWeb.TabStop = True
        Me.lbkViewInvoiceFromWeb.Text = "ViewInvoiceFromWeb"
        '
        'frmInvB4Tt78
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.lbkViewInvoiceFromWeb)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.lbkFindMissing)
        Me.Controls.Add(Me.lbkAdjustInfo)
        Me.Controls.Add(Me.lbkAdjustDecrease)
        Me.Controls.Add(Me.lbkAdjustIncrease)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboBackDate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboKyHieu)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboMauSo)
        Me.Controls.Add(Me.lbkSyncFromWeb)
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
        Me.Name = "frmInvB4Tt78"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Invoice Before TT 78"
        CType(Me.dgrE_InvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrE_Invoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label9 As Label
    Friend WithEvents cboBackDate As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cboKyHieu As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cboMauSo As ComboBox
    Friend WithEvents lbkSyncFromWeb As LinkLabel
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
    Friend WithEvents lbkAdjustInfo As LinkLabel
    Friend WithEvents lbkAdjustDecrease As LinkLabel
    Friend WithEvents lbkAdjustIncrease As LinkLabel
    Friend WithEvents lbkFindMissing As LinkLabel
    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents lbkViewInvoiceFromWeb As LinkLabel
End Class
