<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorInv
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
        Me.dgrInvoices = New System.Windows.Forms.DataGridView()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboTvCompany = New System.Windows.Forms.ComboBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.chkWzUNC = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtVemdorName = New System.Windows.Forms.TextBox()
        Me.dgrInvTcodes = New System.Windows.Forms.DataGridView()
        Me.lbkOverInvoiced = New System.Windows.Forms.LinkLabel()
        Me.cboOverUnder = New System.Windows.Forms.ComboBox()
        Me.lbkRepeatNew = New System.Windows.Forms.LinkLabel()
        Me.txtTcode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.dgrUnc = New System.Windows.Forms.DataGridView()
        Me.chkMatched = New System.Windows.Forms.CheckBox()
        CType(Me.dgrInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrInvTcodes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrUnc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrInvoices
        '
        Me.dgrInvoices.AllowUserToAddRows = False
        Me.dgrInvoices.AllowUserToDeleteRows = False
        Me.dgrInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInvoices.Location = New System.Drawing.Point(-1, 44)
        Me.dgrInvoices.Name = "dgrInvoices"
        Me.dgrInvoices.ReadOnly = True
        Me.dgrInvoices.Size = New System.Drawing.Size(1007, 378)
        Me.dgrInvoices.TabIndex = 0
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(955, 3)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 1
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(12, 619)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 2
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(149, 619)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 3
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(196, 619)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 4
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "TvCompany"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(65, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Status"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(318, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "DOI between"
        '
        'cboTvCompany
        '
        Me.cboTvCompany.FormattingEnabled = True
        Me.cboTvCompany.Items.AddRange(New Object() {"GDS", "TVTR"})
        Me.cboTvCompany.Location = New System.Drawing.Point(5, 21)
        Me.cboTvCompany.Name = "cboTvCompany"
        Me.cboTvCompany.Size = New System.Drawing.Size(41, 21)
        Me.cboTvCompany.TabIndex = 8
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "XX"})
        Me.cboStatus.Location = New System.Drawing.Point(68, 21)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(43, 21)
        Me.cboStatus.TabIndex = 9
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd MMM yy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(321, 20)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(71, 20)
        Me.dtpFrom.TabIndex = 10
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd MMM yy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(398, 19)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(72, 20)
        Me.dtpTo.TabIndex = 11
        '
        'chkWzUNC
        '
        Me.chkWzUNC.AutoSize = True
        Me.chkWzUNC.Checked = True
        Me.chkWzUNC.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkWzUNC.Location = New System.Drawing.Point(789, 22)
        Me.chkWzUNC.Name = "chkWzUNC"
        Me.chkWzUNC.Size = New System.Drawing.Size(65, 17)
        Me.chkWzUNC.TabIndex = 12
        Me.chkWzUNC.Text = "WzUNC"
        Me.chkWzUNC.ThreeState = True
        Me.chkWzUNC.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(128, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "VendorName"
        '
        'txtVemdorName
        '
        Me.txtVemdorName.Location = New System.Drawing.Point(131, 23)
        Me.txtVemdorName.Name = "txtVemdorName"
        Me.txtVemdorName.Size = New System.Drawing.Size(184, 20)
        Me.txtVemdorName.TabIndex = 14
        '
        'dgrInvTcodes
        '
        Me.dgrInvTcodes.AllowUserToAddRows = False
        Me.dgrInvTcodes.AllowUserToDeleteRows = False
        Me.dgrInvTcodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInvTcodes.Location = New System.Drawing.Point(-1, 428)
        Me.dgrInvTcodes.Name = "dgrInvTcodes"
        Me.dgrInvTcodes.ReadOnly = True
        Me.dgrInvTcodes.Size = New System.Drawing.Size(502, 188)
        Me.dgrInvTcodes.TabIndex = 17
        '
        'lbkOverInvoiced
        '
        Me.lbkOverInvoiced.AutoSize = True
        Me.lbkOverInvoiced.Location = New System.Drawing.Point(398, 622)
        Me.lbkOverInvoiced.Name = "lbkOverInvoiced"
        Me.lbkOverInvoiced.Size = New System.Drawing.Size(96, 13)
        Me.lbkOverInvoiced.TabIndex = 18
        Me.lbkOverInvoiced.TabStop = True
        Me.lbkOverInvoiced.Text = "ChangeUnderOver"
        '
        'cboOverUnder
        '
        Me.cboOverUnder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOverUnder.FormattingEnabled = True
        Me.cboOverUnder.Items.AddRange(New Object() {"", "Over", "Under"})
        Me.cboOverUnder.Location = New System.Drawing.Point(305, 619)
        Me.cboOverUnder.Name = "cboOverUnder"
        Me.cboOverUnder.Size = New System.Drawing.Size(87, 21)
        Me.cboOverUnder.TabIndex = 20
        '
        'lbkRepeatNew
        '
        Me.lbkRepeatNew.AutoSize = True
        Me.lbkRepeatNew.Location = New System.Drawing.Point(63, 619)
        Me.lbkRepeatNew.Name = "lbkRepeatNew"
        Me.lbkRepeatNew.Size = New System.Drawing.Size(64, 13)
        Me.lbkRepeatNew.TabIndex = 21
        Me.lbkRepeatNew.TabStop = True
        Me.lbkRepeatNew.Text = "RepeatNew"
        '
        'txtTcode
        '
        Me.txtTcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTcode.Location = New System.Drawing.Point(476, 20)
        Me.txtTcode.Name = "txtTcode"
        Me.txtTcode.Size = New System.Drawing.Size(99, 20)
        Me.txtTcode.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(473, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Tcode"
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Items.AddRange(New Object() {"OK", "XX"})
        Me.cboCustomer.Location = New System.Drawing.Point(581, 19)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(202, 21)
        Me.cboCustomer.TabIndex = 25
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(578, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 13)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Customer"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(955, 25)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 26
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'dgrUnc
        '
        Me.dgrUnc.AllowUserToAddRows = False
        Me.dgrUnc.AllowUserToDeleteRows = False
        Me.dgrUnc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrUnc.Location = New System.Drawing.Point(507, 428)
        Me.dgrUnc.Name = "dgrUnc"
        Me.dgrUnc.ReadOnly = True
        Me.dgrUnc.Size = New System.Drawing.Size(499, 188)
        Me.dgrUnc.TabIndex = 27
        '
        'chkMatched
        '
        Me.chkMatched.AutoSize = True
        Me.chkMatched.Checked = True
        Me.chkMatched.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkMatched.Location = New System.Drawing.Point(860, 21)
        Me.chkMatched.Name = "chkMatched"
        Me.chkMatched.Size = New System.Drawing.Size(68, 17)
        Me.chkMatched.TabIndex = 28
        Me.chkMatched.Text = "Matched"
        Me.chkMatched.ThreeState = True
        Me.chkMatched.UseVisualStyleBackColor = True
        '
        'frmVendorInv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 641)
        Me.Controls.Add(Me.chkMatched)
        Me.Controls.Add(Me.dgrUnc)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTcode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lbkRepeatNew)
        Me.Controls.Add(Me.cboOverUnder)
        Me.Controls.Add(Me.lbkOverInvoiced)
        Me.Controls.Add(Me.dgrInvTcodes)
        Me.Controls.Add(Me.txtVemdorName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chkWzUNC)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.cboTvCompany)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.lbkEdit)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrInvoices)
        Me.Name = "frmVendorInv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SupplierInv"
        CType(Me.dgrInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrInvTcodes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrUnc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrInvoices As DataGridView
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cboTvCompany As ComboBox
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents chkWzUNC As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtVemdorName As TextBox
    Friend WithEvents dgrInvTcodes As DataGridView
    Friend WithEvents lbkOverInvoiced As LinkLabel
    Friend WithEvents cboOverUnder As ComboBox
    Friend WithEvents lbkRepeatNew As LinkLabel
    Friend WithEvents txtTcode As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents dgrUnc As DataGridView
    Friend WithEvents chkMatched As CheckBox
End Class
