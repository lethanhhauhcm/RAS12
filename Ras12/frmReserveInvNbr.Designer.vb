<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReserveInvNbr
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.chkShowLast3Month = New System.Windows.Forms.CheckBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lbkReserveInvNo = New System.Windows.Forms.LinkLabel()
        Me.CmbYear = New System.Windows.Forms.ComboBox()
        Me.TxtDOI = New System.Windows.Forms.DateTimePicker()
        Me.TxtNoiDung = New System.Windows.Forms.TextBox()
        Me.LblSave = New System.Windows.Forms.LinkLabel()
        Me.LblDelete = New System.Windows.Forms.LinkLabel()
        Me.LblRePrint = New System.Windows.Forms.LinkLabel()
        Me.GridInvHandlerInv = New System.Windows.Forms.DataGridView()
        Me.LblPreviewInv = New System.Windows.Forms.LinkLabel()
        Me.CmbAL = New System.Windows.Forms.ComboBox()
        Me.CmbCustomer = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtCustAddrr = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.TxtAmt = New System.Windows.Forms.TextBox()
        Me.txtTaxCode = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        CType(Me.GridInvHandlerInv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkShowLast3Month
        '
        Me.chkShowLast3Month.AutoSize = True
        Me.chkShowLast3Month.Checked = True
        Me.chkShowLast3Month.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowLast3Month.Location = New System.Drawing.Point(616, 485)
        Me.chkShowLast3Month.Name = "chkShowLast3Month"
        Me.chkShowLast3Month.Size = New System.Drawing.Size(130, 17)
        Me.chkShowLast3Month.TabIndex = 95
        Me.chkShowLast3Month.Text = "ShowLast3MonthOnly"
        Me.chkShowLast3Month.UseVisualStyleBackColor = True
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "XX", "QQ"})
        Me.cboStatus.Location = New System.Drawing.Point(752, 483)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(58, 21)
        Me.cboStatus.TabIndex = 94
        '
        'lbkReserveInvNo
        '
        Me.lbkReserveInvNo.AutoSize = True
        Me.lbkReserveInvNo.Location = New System.Drawing.Point(504, 486)
        Me.lbkReserveInvNo.Name = "lbkReserveInvNo"
        Me.lbkReserveInvNo.Size = New System.Drawing.Size(76, 13)
        Me.lbkReserveInvNo.TabIndex = 93
        Me.lbkReserveInvNo.TabStop = True
        Me.lbkReserveInvNo.Text = "ReserveInvNo"
        '
        'CmbYear
        '
        Me.CmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbYear.FormattingEnabled = True
        Me.CmbYear.Location = New System.Drawing.Point(55, 49)
        Me.CmbYear.Name = "CmbYear"
        Me.CmbYear.Size = New System.Drawing.Size(41, 21)
        Me.CmbYear.TabIndex = 92
        '
        'TxtDOI
        '
        Me.TxtDOI.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.TxtDOI.Location = New System.Drawing.Point(687, 50)
        Me.TxtDOI.Name = "TxtDOI"
        Me.TxtDOI.Size = New System.Drawing.Size(84, 20)
        Me.TxtDOI.TabIndex = 91
        '
        'TxtNoiDung
        '
        Me.TxtNoiDung.Location = New System.Drawing.Point(318, 50)
        Me.TxtNoiDung.MaxLength = 56
        Me.TxtNoiDung.Name = "TxtNoiDung"
        Me.TxtNoiDung.Size = New System.Drawing.Size(333, 20)
        Me.TxtNoiDung.TabIndex = 77
        '
        'LblSave
        '
        Me.LblSave.AutoSize = True
        Me.LblSave.Location = New System.Drawing.Point(777, 53)
        Me.LblSave.Name = "LblSave"
        Me.LblSave.Size = New System.Drawing.Size(32, 13)
        Me.LblSave.TabIndex = 78
        Me.LblSave.TabStop = True
        Me.LblSave.Text = "Save"
        '
        'LblDelete
        '
        Me.LblDelete.AutoSize = True
        Me.LblDelete.Location = New System.Drawing.Point(577, 486)
        Me.LblDelete.Name = "LblDelete"
        Me.LblDelete.Size = New System.Drawing.Size(38, 13)
        Me.LblDelete.TabIndex = 89
        Me.LblDelete.TabStop = True
        Me.LblDelete.Text = "Delete"
        Me.LblDelete.Visible = False
        '
        'LblRePrint
        '
        Me.LblRePrint.AutoSize = True
        Me.LblRePrint.Location = New System.Drawing.Point(87, 484)
        Me.LblRePrint.Name = "LblRePrint"
        Me.LblRePrint.Size = New System.Drawing.Size(42, 13)
        Me.LblRePrint.TabIndex = 90
        Me.LblRePrint.TabStop = True
        Me.LblRePrint.Text = "RePrint"
        Me.LblRePrint.Visible = False
        '
        'GridInvHandlerInv
        '
        Me.GridInvHandlerInv.AllowUserToAddRows = False
        Me.GridInvHandlerInv.AllowUserToDeleteRows = False
        Me.GridInvHandlerInv.BackgroundColor = System.Drawing.Color.Beige
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridInvHandlerInv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridInvHandlerInv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridInvHandlerInv.Location = New System.Drawing.Point(23, 73)
        Me.GridInvHandlerInv.Name = "GridInvHandlerInv"
        Me.GridInvHandlerInv.ReadOnly = True
        Me.GridInvHandlerInv.RowHeadersVisible = False
        Me.GridInvHandlerInv.Size = New System.Drawing.Size(788, 409)
        Me.GridInvHandlerInv.TabIndex = 75
        '
        'LblPreviewInv
        '
        Me.LblPreviewInv.AutoSize = True
        Me.LblPreviewInv.Location = New System.Drawing.Point(25, 484)
        Me.LblPreviewInv.Name = "LblPreviewInv"
        Me.LblPreviewInv.Size = New System.Drawing.Size(45, 13)
        Me.LblPreviewInv.TabIndex = 88
        Me.LblPreviewInv.TabStop = True
        Me.LblPreviewInv.Text = "Preview"
        Me.LblPreviewInv.Visible = False
        '
        'CmbAL
        '
        Me.CmbAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbAL.FormattingEnabled = True
        Me.CmbAL.Location = New System.Drawing.Point(55, 8)
        Me.CmbAL.Name = "CmbAL"
        Me.CmbAL.Size = New System.Drawing.Size(41, 21)
        Me.CmbAL.TabIndex = 70
        '
        'CmbCustomer
        '
        Me.CmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbCustomer.FormattingEnabled = True
        Me.CmbCustomer.Location = New System.Drawing.Point(156, 6)
        Me.CmbCustomer.Name = "CmbCustomer"
        Me.CmbCustomer.Size = New System.Drawing.Size(111, 23)
        Me.CmbCustomer.TabIndex = 71
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(658, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 15)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "DOI"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(25, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 15)
        Me.Label5.TabIndex = 87
        Me.Label5.Text = "Year"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(21, 15)
        Me.Label1.TabIndex = 84
        Me.Label1.Text = "AL"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(98, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 15)
        Me.Label6.TabIndex = 85
        Me.Label6.Text = "Customer"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(98, 32)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(53, 13)
        Me.Label29.TabIndex = 82
        Me.Label29.Text = "Tax Code"
        '
        'txtCustAddrr
        '
        Me.txtCustAddrr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustAddrr.Location = New System.Drawing.Point(318, 29)
        Me.txtCustAddrr.MaxLength = 0
        Me.txtCustAddrr.Name = "txtCustAddrr"
        Me.txtCustAddrr.ReadOnly = True
        Me.txtCustAddrr.Size = New System.Drawing.Size(492, 20)
        Me.txtCustAddrr.TabIndex = 74
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(98, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 80
        Me.Label2.Text = "VND"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(267, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 81
        Me.Label3.Text = "Descrt."
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(267, 32)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(45, 13)
        Me.Label28.TabIndex = 79
        Me.Label28.Text = "Address"
        '
        'TxtAmt
        '
        Me.TxtAmt.Location = New System.Drawing.Point(156, 50)
        Me.TxtAmt.MaxLength = 20
        Me.TxtAmt.Name = "TxtAmt"
        Me.TxtAmt.Size = New System.Drawing.Size(111, 20)
        Me.TxtAmt.TabIndex = 76
        Me.TxtAmt.Text = "0"
        Me.TxtAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTaxCode
        '
        Me.txtTaxCode.Location = New System.Drawing.Point(156, 29)
        Me.txtTaxCode.MaxLength = 20
        Me.txtTaxCode.Name = "txtTaxCode"
        Me.txtTaxCode.ReadOnly = True
        Me.txtTaxCode.Size = New System.Drawing.Size(111, 20)
        Me.txtTaxCode.TabIndex = 73
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(267, 11)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(51, 13)
        Me.Label30.TabIndex = 83
        Me.Label30.Text = "FullName"
        '
        'txtCustName
        '
        Me.txtCustName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustName.Location = New System.Drawing.Point(318, 8)
        Me.txtCustName.MaxLength = 256
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.ReadOnly = True
        Me.txtCustName.Size = New System.Drawing.Size(492, 20)
        Me.txtCustName.TabIndex = 72
        '
        'frmReserveInvNbr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 511)
        Me.Controls.Add(Me.chkShowLast3Month)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.lbkReserveInvNo)
        Me.Controls.Add(Me.CmbYear)
        Me.Controls.Add(Me.TxtDOI)
        Me.Controls.Add(Me.TxtNoiDung)
        Me.Controls.Add(Me.LblSave)
        Me.Controls.Add(Me.LblDelete)
        Me.Controls.Add(Me.LblRePrint)
        Me.Controls.Add(Me.GridInvHandlerInv)
        Me.Controls.Add(Me.LblPreviewInv)
        Me.Controls.Add(Me.CmbAL)
        Me.Controls.Add(Me.CmbCustomer)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.txtCustAddrr)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.TxtAmt)
        Me.Controls.Add(Me.txtTaxCode)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.txtCustName)
        Me.Name = "frmReserveInvNbr"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reserve Invoice Number"
        CType(Me.GridInvHandlerInv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chkShowLast3Month As CheckBox
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents lbkReserveInvNo As LinkLabel
    Friend WithEvents CmbYear As ComboBox
    Friend WithEvents TxtDOI As DateTimePicker
    Friend WithEvents TxtNoiDung As TextBox
    Friend WithEvents LblSave As LinkLabel
    Friend WithEvents LblDelete As LinkLabel
    Friend WithEvents LblRePrint As LinkLabel
    Friend WithEvents GridInvHandlerInv As DataGridView
    Friend WithEvents LblPreviewInv As LinkLabel
    Friend WithEvents CmbAL As ComboBox
    Friend WithEvents CmbCustomer As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents txtCustAddrr As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents TxtAmt As TextBox
    Friend WithEvents txtTaxCode As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents txtCustName As TextBox
End Class
