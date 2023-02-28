<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSelectTrxForE_Inv
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
        Me.cboFOP = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTcode = New System.Windows.Forms.TextBox()
        Me.cboProduct = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbkCreate = New System.Windows.Forms.LinkLabel()
        Me.dgrSum = New System.Windows.Forms.DataGridView()
        Me.lbkSumAndCheck = New System.Windows.Forms.LinkLabel()
        Me.cboPrintToWhom = New System.Windows.Forms.ComboBox()
        Me.lbkChk_UnChkAll_TKT = New System.Windows.Forms.LinkLabel()
        Me.dgrTkts = New System.Windows.Forms.DataGridView()
        Me.S = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboCustType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboCustGroup = New System.Windows.Forms.ComboBox()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.cboTemplate = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbkSelectSameTcode = New System.Windows.Forms.LinkLabel()
        Me.lbkFromEqualTo = New System.Windows.Forms.LinkLabel()
        Me.cboVatPct = New System.Windows.Forms.ComboBox()
        Me.lbkSwitchVatPct = New System.Windows.Forms.LinkLabel()
        Me.lbkSelectSameRCP = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrSum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboFOP
        '
        Me.cboFOP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFOP.FormattingEnabled = True
        Me.cboFOP.Items.AddRange(New Object() {"CRD", "DEB", "PPD", "PSP", "BTF"})
        Me.cboFOP.Location = New System.Drawing.Point(651, 24)
        Me.cboFOP.Name = "cboFOP"
        Me.cboFOP.Size = New System.Drawing.Size(55, 21)
        Me.cboFOP.TabIndex = 24
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(651, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "FOP"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(709, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Product"
        '
        'txtTcode
        '
        Me.txtTcode.Location = New System.Drawing.Point(773, 25)
        Me.txtTcode.Name = "txtTcode"
        Me.txtTcode.Size = New System.Drawing.Size(130, 20)
        Me.txtTcode.TabIndex = 21
        '
        'cboProduct
        '
        Me.cboProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProduct.FormattingEnabled = True
        Me.cboProduct.Items.AddRange(New Object() {"AIR", "N-A"})
        Me.cboProduct.Location = New System.Drawing.Point(712, 24)
        Me.cboProduct.Name = "cboProduct"
        Me.cboProduct.Size = New System.Drawing.Size(55, 21)
        Me.cboProduct.TabIndex = 25
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(770, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Tcode"
        '
        'lbkCreate
        '
        Me.lbkCreate.AutoSize = True
        Me.lbkCreate.Location = New System.Drawing.Point(510, 467)
        Me.lbkCreate.Name = "lbkCreate"
        Me.lbkCreate.Size = New System.Drawing.Size(85, 13)
        Me.lbkCreate.TabIndex = 28
        Me.lbkCreate.TabStop = True
        Me.lbkCreate.Text = "CreateInvDetails"
        '
        'dgrSum
        '
        Me.dgrSum.AllowUserToAddRows = False
        Me.dgrSum.AllowUserToDeleteRows = False
        Me.dgrSum.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrSum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrSum.Location = New System.Drawing.Point(17, 420)
        Me.dgrSum.Name = "dgrSum"
        Me.dgrSum.ReadOnly = True
        Me.dgrSum.RowHeadersVisible = False
        Me.dgrSum.Size = New System.Drawing.Size(487, 129)
        Me.dgrSum.TabIndex = 33
        '
        'lbkSumAndCheck
        '
        Me.lbkSumAndCheck.AutoSize = True
        Me.lbkSumAndCheck.Location = New System.Drawing.Point(170, 397)
        Me.lbkSumAndCheck.Name = "lbkSumAndCheck"
        Me.lbkSumAndCheck.Size = New System.Drawing.Size(78, 13)
        Me.lbkSumAndCheck.TabIndex = 32
        Me.lbkSumAndCheck.TabStop = True
        Me.lbkSumAndCheck.Text = "SumAndCheck"
        '
        'cboPrintToWhom
        '
        Me.cboPrintToWhom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrintToWhom.FormattingEnabled = True
        Me.cboPrintToWhom.Items.AddRange(New Object() {"Customer", "TransViet"})
        Me.cboPrintToWhom.Location = New System.Drawing.Point(590, 416)
        Me.cboPrintToWhom.Name = "cboPrintToWhom"
        Me.cboPrintToWhom.Size = New System.Drawing.Size(89, 21)
        Me.cboPrintToWhom.TabIndex = 29
        '
        'lbkChk_UnChkAll_TKT
        '
        Me.lbkChk_UnChkAll_TKT.AutoSize = True
        Me.lbkChk_UnChkAll_TKT.Location = New System.Drawing.Point(14, 397)
        Me.lbkChk_UnChkAll_TKT.Name = "lbkChk_UnChkAll_TKT"
        Me.lbkChk_UnChkAll_TKT.Size = New System.Drawing.Size(48, 13)
        Me.lbkChk_UnChkAll_TKT.TabIndex = 30
        Me.lbkChk_UnChkAll_TKT.TabStop = True
        Me.lbkChk_UnChkAll_TKT.Text = "SelectAll"
        '
        'dgrTkts
        '
        Me.dgrTkts.AllowUserToAddRows = False
        Me.dgrTkts.AllowUserToDeleteRows = False
        Me.dgrTkts.BackgroundColor = System.Drawing.Color.Azure
        Me.dgrTkts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTkts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.S})
        Me.dgrTkts.Location = New System.Drawing.Point(12, 52)
        Me.dgrTkts.Name = "dgrTkts"
        Me.dgrTkts.RowHeadersVisible = False
        Me.dgrTkts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrTkts.Size = New System.Drawing.Size(891, 338)
        Me.dgrTkts.TabIndex = 27
        '
        'S
        '
        Me.S.HeaderText = "S"
        Me.S.Name = "S"
        Me.S.Width = 24
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(909, 9)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 34
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "ddMMMyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(579, 25)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(66, 20)
        Me.dtpTo.TabIndex = 37
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "ddMMMyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(507, 25)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(66, 20)
        Me.dtpFrom.TabIndex = 36
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(504, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "From - To"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(909, 32)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 38
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 104
        Me.Label5.Text = "CustType"
        '
        'cboCustType
        '
        Me.cboCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustType.FormattingEnabled = True
        Me.cboCustType.Items.AddRange(New Object() {"CA", "CS", "LC", "TA", "TC", "TO", "WK"})
        Me.cboCustType.Location = New System.Drawing.Point(11, 24)
        Me.cboCustType.Name = "cboCustType"
        Me.cboCustType.Size = New System.Drawing.Size(52, 21)
        Me.cboCustType.TabIndex = 103
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(69, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 13)
        Me.Label6.TabIndex = 102
        Me.Label6.Text = "CustomerGroups"
        '
        'cboCustGroup
        '
        Me.cboCustGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustGroup.FormattingEnabled = True
        Me.cboCustGroup.Location = New System.Drawing.Point(69, 24)
        Me.cboCustGroup.Name = "cboCustGroup"
        Me.cboCustGroup.Size = New System.Drawing.Size(184, 21)
        Me.cboCustGroup.TabIndex = 101
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(256, 7)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(51, 13)
        Me.lblCustomer.TabIndex = 100
        Me.lblCustomer.Text = "Customer"
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(259, 25)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(242, 21)
        Me.cboCustomer.TabIndex = 99
        '
        'cboTemplate
        '
        Me.cboTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTemplate.FormattingEnabled = True
        Me.cboTemplate.Items.AddRange(New Object() {"INV_AIR1", "INV_AIR2", "INV_AIR3", "INV_AIR4", "INV_NONAIR1", "INV_NONAIR2"})
        Me.cboTemplate.Location = New System.Drawing.Point(511, 443)
        Me.cboTemplate.Name = "cboTemplate"
        Me.cboTemplate.Size = New System.Drawing.Size(166, 21)
        Me.cboTemplate.TabIndex = 105
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(510, 420)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 13)
        Me.Label7.TabIndex = 106
        Me.Label7.Text = "Template"
        '
        'lbkSelectSameTcode
        '
        Me.lbkSelectSameTcode.AutoSize = True
        Me.lbkSelectSameTcode.Location = New System.Drawing.Point(69, 397)
        Me.lbkSelectSameTcode.Name = "lbkSelectSameTcode"
        Me.lbkSelectSameTcode.Size = New System.Drawing.Size(95, 13)
        Me.lbkSelectSameTcode.TabIndex = 107
        Me.lbkSelectSameTcode.TabStop = True
        Me.lbkSelectSameTcode.Text = "SelectSameTcode"
        '
        'lbkFromEqualTo
        '
        Me.lbkFromEqualTo.AutoSize = True
        Me.lbkFromEqualTo.Location = New System.Drawing.Point(576, 9)
        Me.lbkFromEqualTo.Name = "lbkFromEqualTo"
        Me.lbkFromEqualTo.Size = New System.Drawing.Size(49, 13)
        Me.lbkFromEqualTo.TabIndex = 108
        Me.lbkFromEqualTo.TabStop = True
        Me.lbkFromEqualTo.Text = "To=From"
        '
        'cboVatPct
        '
        Me.cboVatPct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVatPct.FormattingEnabled = True
        Me.cboVatPct.Items.AddRange(New Object() {"VAT_NoDiscount", "VAT7"})
        Me.cboVatPct.Location = New System.Drawing.Point(700, 416)
        Me.cboVatPct.Name = "cboVatPct"
        Me.cboVatPct.Size = New System.Drawing.Size(117, 21)
        Me.cboVatPct.TabIndex = 109
        '
        'lbkSwitchVatPct
        '
        Me.lbkSwitchVatPct.AutoSize = True
        Me.lbkSwitchVatPct.Location = New System.Drawing.Point(746, 451)
        Me.lbkSwitchVatPct.Name = "lbkSwitchVatPct"
        Me.lbkSwitchVatPct.Size = New System.Drawing.Size(71, 13)
        Me.lbkSwitchVatPct.TabIndex = 110
        Me.lbkSwitchVatPct.TabStop = True
        Me.lbkSwitchVatPct.Text = "SwitchVatPct"
        '
        'lbkSelectSameRCP
        '
        Me.lbkSelectSameRCP.AutoSize = True
        Me.lbkSelectSameRCP.Location = New System.Drawing.Point(256, 397)
        Me.lbkSelectSameRCP.Name = "lbkSelectSameRCP"
        Me.lbkSelectSameRCP.Size = New System.Drawing.Size(86, 13)
        Me.lbkSelectSameRCP.TabIndex = 111
        Me.lbkSelectSameRCP.TabStop = True
        Me.lbkSelectSameRCP.Text = "SelectSameRCP"
        '
        'frmSelectTrxForE_Inv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 561)
        Me.Controls.Add(Me.lbkSelectSameRCP)
        Me.Controls.Add(Me.lbkSwitchVatPct)
        Me.Controls.Add(Me.cboVatPct)
        Me.Controls.Add(Me.lbkFromEqualTo)
        Me.Controls.Add(Me.lbkSelectSameTcode)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboTemplate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboCustType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboCustGroup)
        Me.Controls.Add(Me.lblCustomer)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.lbkCreate)
        Me.Controls.Add(Me.dgrSum)
        Me.Controls.Add(Me.lbkSumAndCheck)
        Me.Controls.Add(Me.cboPrintToWhom)
        Me.Controls.Add(Me.lbkChk_UnChkAll_TKT)
        Me.Controls.Add(Me.dgrTkts)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboProduct)
        Me.Controls.Add(Me.cboFOP)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTcode)
        Me.Name = "frmSelectTrxForE_Inv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SelectTrxForE_Inv"
        CType(Me.dgrSum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboFOP As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtTcode As TextBox
    Friend WithEvents cboProduct As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lbkCreate As LinkLabel
    Friend WithEvents dgrSum As DataGridView
    Friend WithEvents lbkSumAndCheck As LinkLabel
    Friend WithEvents cboPrintToWhom As ComboBox
    Friend WithEvents lbkChk_UnChkAll_TKT As LinkLabel
    Friend WithEvents dgrTkts As DataGridView
    Friend WithEvents S As DataGridViewCheckBoxColumn
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents Label5 As Label
    Friend WithEvents cboCustType As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cboCustGroup As ComboBox
    Friend WithEvents lblCustomer As Label
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents cboTemplate As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents lbkSelectSameTcode As LinkLabel
    Friend WithEvents lbkFromEqualTo As LinkLabel
    Friend WithEvents cboVatPct As ComboBox
    Friend WithEvents lbkSwitchVatPct As LinkLabel
    Friend WithEvents lbkSelectSameRCP As LinkLabel
End Class
