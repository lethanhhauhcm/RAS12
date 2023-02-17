<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSendNotice2TaxOffc
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
        Me.txtTaxOffice = New System.Windows.Forms.TextBox()
        Me.txtNoticeDate = New System.Windows.Forms.TextBox()
        Me.cboNoticeType = New System.Windows.Forms.ComboBox()
        Me.txtTaxCode = New System.Windows.Forms.TextBox()
        Me.txtTaxPayer = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.txtTaxOffcNoticeNbr = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpNoticeDateTaxOffc = New System.Windows.Forms.DateTimePicker()
        Me.lbkSend = New System.Windows.Forms.LinkLabel()
        Me.dgrInvoices = New System.Windows.Forms.DataGridView()
        Me.lbkDeleteRow = New System.Windows.Forms.LinkLabel()
        Me.MaCoQuanThue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MauSo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KyHieu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvoiceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TinhChat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Reason = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgrInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Tên cơ quan thuế"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(246, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Ngày thông báo"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(0, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Loại thông báo"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(380, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Địa danh"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(246, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Mã số thuế"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(0, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Người nộp thuế"
        '
        'txtTaxOffice
        '
        Me.txtTaxOffice.Enabled = False
        Me.txtTaxOffice.Location = New System.Drawing.Point(3, 16)
        Me.txtTaxOffice.Name = "txtTaxOffice"
        Me.txtTaxOffice.Size = New System.Drawing.Size(231, 20)
        Me.txtTaxOffice.TabIndex = 6
        '
        'txtNoticeDate
        '
        Me.txtNoticeDate.Enabled = False
        Me.txtNoticeDate.Location = New System.Drawing.Point(249, 16)
        Me.txtNoticeDate.Name = "txtNoticeDate"
        Me.txtNoticeDate.Size = New System.Drawing.Size(91, 20)
        Me.txtNoticeDate.TabIndex = 7
        '
        'cboNoticeType
        '
        Me.cboNoticeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNoticeType.FormattingEnabled = True
        Me.cboNoticeType.Items.AddRange(New Object() {"1-Thông báo hủy/giải trình của NNT", "2-Thông báo hủy/giải trình của NNT theo thông báo của CQT"})
        Me.cboNoticeType.Location = New System.Drawing.Point(3, 124)
        Me.cboNoticeType.Name = "cboNoticeType"
        Me.cboNoticeType.Size = New System.Drawing.Size(337, 21)
        Me.cboNoticeType.TabIndex = 8
        '
        'txtTaxCode
        '
        Me.txtTaxCode.Enabled = False
        Me.txtTaxCode.Location = New System.Drawing.Point(249, 68)
        Me.txtTaxCode.Name = "txtTaxCode"
        Me.txtTaxCode.Size = New System.Drawing.Size(91, 20)
        Me.txtTaxCode.TabIndex = 10
        '
        'txtTaxPayer
        '
        Me.txtTaxPayer.Enabled = False
        Me.txtTaxPayer.Location = New System.Drawing.Point(3, 68)
        Me.txtTaxPayer.Name = "txtTaxPayer"
        Me.txtTaxPayer.Size = New System.Drawing.Size(231, 20)
        Me.txtTaxPayer.TabIndex = 9
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(383, 68)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(355, 20)
        Me.txtLocation.TabIndex = 11
        '
        'txtTaxOffcNoticeNbr
        '
        Me.txtTaxOffcNoticeNbr.Location = New System.Drawing.Point(383, 124)
        Me.txtTaxOffcNoticeNbr.Name = "txtTaxOffcNoticeNbr"
        Me.txtTaxOffcNoticeNbr.Size = New System.Drawing.Size(231, 20)
        Me.txtTaxOffcNoticeNbr.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(626, 108)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(129, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Ngày thông báo của CQT"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(380, 108)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(117, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Số thông báo của CQT"
        '
        'dtpNoticeDateTaxOffc
        '
        Me.dtpNoticeDateTaxOffc.CustomFormat = "dd MMM yy"
        Me.dtpNoticeDateTaxOffc.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNoticeDateTaxOffc.Location = New System.Drawing.Point(629, 125)
        Me.dtpNoticeDateTaxOffc.Name = "dtpNoticeDateTaxOffc"
        Me.dtpNoticeDateTaxOffc.Size = New System.Drawing.Size(109, 20)
        Me.dtpNoticeDateTaxOffc.TabIndex = 15
        '
        'lbkSend
        '
        Me.lbkSend.AutoSize = True
        Me.lbkSend.Location = New System.Drawing.Point(795, 132)
        Me.lbkSend.Name = "lbkSend"
        Me.lbkSend.Size = New System.Drawing.Size(32, 13)
        Me.lbkSend.TabIndex = 16
        Me.lbkSend.TabStop = True
        Me.lbkSend.Text = "Send"
        '
        'dgrInvoices
        '
        Me.dgrInvoices.AllowUserToAddRows = False
        Me.dgrInvoices.AllowUserToDeleteRows = False
        Me.dgrInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInvoices.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MaCoQuanThue, Me.RecId, Me.MauSo, Me.KyHieu, Me.InvoiceNo, Me.DOI, Me.InvType, Me.InvId, Me.TinhChat, Me.Reason})
        Me.dgrInvoices.Location = New System.Drawing.Point(3, 179)
        Me.dgrInvoices.Name = "dgrInvoices"
        Me.dgrInvoices.Size = New System.Drawing.Size(878, 280)
        Me.dgrInvoices.TabIndex = 68
        '
        'lbkDeleteRow
        '
        Me.lbkDeleteRow.AutoSize = True
        Me.lbkDeleteRow.Location = New System.Drawing.Point(12, 163)
        Me.lbkDeleteRow.Name = "lbkDeleteRow"
        Me.lbkDeleteRow.Size = New System.Drawing.Size(60, 13)
        Me.lbkDeleteRow.TabIndex = 70
        Me.lbkDeleteRow.TabStop = True
        Me.lbkDeleteRow.Text = "DeleteRow"
        '
        'MaCoQuanThue
        '
        Me.MaCoQuanThue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.MaCoQuanThue.HeaderText = "MaCoQuanThue"
        Me.MaCoQuanThue.Name = "MaCoQuanThue"
        Me.MaCoQuanThue.ReadOnly = True
        Me.MaCoQuanThue.Width = 111
        '
        'RecId
        '
        Me.RecId.HeaderText = "RecId"
        Me.RecId.Name = "RecId"
        Me.RecId.ReadOnly = True
        Me.RecId.Width = 61
        '
        'MauSo
        '
        Me.MauSo.HeaderText = "MauSo"
        Me.MauSo.Name = "MauSo"
        Me.MauSo.ReadOnly = True
        Me.MauSo.Width = 66
        '
        'KyHieu
        '
        Me.KyHieu.HeaderText = "KyHieu"
        Me.KyHieu.Name = "KyHieu"
        Me.KyHieu.ReadOnly = True
        Me.KyHieu.Width = 66
        '
        'InvoiceNo
        '
        Me.InvoiceNo.HeaderText = "InvoiceNo"
        Me.InvoiceNo.Name = "InvoiceNo"
        Me.InvoiceNo.ReadOnly = True
        Me.InvoiceNo.Width = 81
        '
        'DOI
        '
        Me.DOI.HeaderText = "DOI"
        Me.DOI.Name = "DOI"
        Me.DOI.ReadOnly = True
        Me.DOI.Width = 51
        '
        'InvType
        '
        Me.InvType.HeaderText = "InvType"
        Me.InvType.Name = "InvType"
        Me.InvType.Width = 71
        '
        'InvId
        '
        Me.InvId.HeaderText = "InvId"
        Me.InvId.Name = "InvId"
        Me.InvId.ReadOnly = True
        Me.InvId.Width = 56
        '
        'TinhChat
        '
        Me.TinhChat.HeaderText = "TinhChat"
        Me.TinhChat.Name = "TinhChat"
        Me.TinhChat.ReadOnly = True
        Me.TinhChat.Width = 75
        '
        'Reason
        '
        Me.Reason.HeaderText = "Reason"
        Me.Reason.Name = "Reason"
        Me.Reason.Width = 69
        '
        'frmSendNotice2TaxOffc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 461)
        Me.Controls.Add(Me.lbkDeleteRow)
        Me.Controls.Add(Me.dgrInvoices)
        Me.Controls.Add(Me.lbkSend)
        Me.Controls.Add(Me.dtpNoticeDateTaxOffc)
        Me.Controls.Add(Me.txtTaxOffcNoticeNbr)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.txtTaxCode)
        Me.Controls.Add(Me.txtTaxPayer)
        Me.Controls.Add(Me.cboNoticeType)
        Me.Controls.Add(Me.txtNoticeDate)
        Me.Controls.Add(Me.txtTaxOffice)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmSendNotice2TaxOffc"
        Me.Text = "SendNotice2TaxOffc"
        CType(Me.dgrInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtTaxOffice As TextBox
    Friend WithEvents txtNoticeDate As TextBox
    Friend WithEvents cboNoticeType As ComboBox
    Friend WithEvents txtTaxCode As TextBox
    Friend WithEvents txtTaxPayer As TextBox
    Friend WithEvents txtLocation As TextBox
    Friend WithEvents txtTaxOffcNoticeNbr As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents dtpNoticeDateTaxOffc As DateTimePicker
    Friend WithEvents lbkSend As LinkLabel
    Friend WithEvents dgrInvoices As DataGridView
    Friend WithEvents lbkDeleteRow As LinkLabel
    Friend WithEvents MaCoQuanThue As DataGridViewTextBoxColumn
    Friend WithEvents RecId As DataGridViewTextBoxColumn
    Friend WithEvents MauSo As DataGridViewTextBoxColumn
    Friend WithEvents KyHieu As DataGridViewTextBoxColumn
    Friend WithEvents InvoiceNo As DataGridViewTextBoxColumn
    Friend WithEvents DOI As DataGridViewTextBoxColumn
    Friend WithEvents InvType As DataGridViewTextBoxColumn
    Friend WithEvents InvId As DataGridViewTextBoxColumn
    Friend WithEvents TinhChat As DataGridViewTextBoxColumn
    Friend WithEvents Reason As DataGridViewTextBoxColumn
End Class
