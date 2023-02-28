<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptDataList
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lbkCloneCustomer = New System.Windows.Forms.LinkLabel()
        Me.btnClone = New System.Windows.Forms.Button()
        Me.cboApplyTo = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnUpdateSpecialValues = New System.Windows.Forms.Button()
        Me.btnUpdateNormalValues = New System.Windows.Forms.Button()
        Me.chkAllowSpecialValues = New System.Windows.Forms.CheckBox()
        Me.chkCheckValues = New System.Windows.Forms.CheckBox()
        Me.cboDataCode = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboCollectionMethod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboMandatory = New System.Windows.Forms.ComboBox()
        Me.Details = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.dgrRptData = New System.Windows.Forms.DataGridView()
        Me.RecId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustShortName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NameByCustomer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MinLength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaxLength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mandatory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ConditionOfUse = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CharType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CollectionMethod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DefaultValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheckValues = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AllowSpecialValues = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.CustId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSeacrh = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.dgrRptData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkCloneCustomer
        '
        Me.lbkCloneCustomer.AutoSize = True
        Me.lbkCloneCustomer.Location = New System.Drawing.Point(10, 67)
        Me.lbkCloneCustomer.Name = "lbkCloneCustomer"
        Me.lbkCloneCustomer.Size = New System.Drawing.Size(78, 13)
        Me.lbkCloneCustomer.TabIndex = 121
        Me.lbkCloneCustomer.TabStop = True
        Me.lbkCloneCustomer.Text = "CloneCustomer"
        '
        'btnClone
        '
        Me.btnClone.Location = New System.Drawing.Point(127, 593)
        Me.btnClone.Name = "btnClone"
        Me.btnClone.Size = New System.Drawing.Size(91, 20)
        Me.btnClone.TabIndex = 120
        Me.btnClone.Text = "Clone"
        Me.btnClone.UseVisualStyleBackColor = True
        '
        'cboApplyTo
        '
        Me.cboApplyTo.FormattingEnabled = True
        Me.cboApplyTo.Items.AddRange(New Object() {"ALL", "AIR", "HTL", "ALL", "CAR", "N-A"})
        Me.cboApplyTo.Location = New System.Drawing.Point(495, 34)
        Me.cboApplyTo.Name = "cboApplyTo"
        Me.cboApplyTo.Size = New System.Drawing.Size(85, 21)
        Me.cboApplyTo.TabIndex = 119
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(443, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 118
        Me.Label3.Text = "ApplyTo"
        '
        'btnUpdateSpecialValues
        '
        Me.btnUpdateSpecialValues.Location = New System.Drawing.Point(736, 593)
        Me.btnUpdateSpecialValues.Name = "btnUpdateSpecialValues"
        Me.btnUpdateSpecialValues.Size = New System.Drawing.Size(91, 20)
        Me.btnUpdateSpecialValues.TabIndex = 117
        Me.btnUpdateSpecialValues.Text = "SpeciallValues"
        Me.btnUpdateSpecialValues.UseVisualStyleBackColor = True
        '
        'btnUpdateNormalValues
        '
        Me.btnUpdateNormalValues.Location = New System.Drawing.Point(619, 593)
        Me.btnUpdateNormalValues.Name = "btnUpdateNormalValues"
        Me.btnUpdateNormalValues.Size = New System.Drawing.Size(91, 20)
        Me.btnUpdateNormalValues.TabIndex = 116
        Me.btnUpdateNormalValues.Text = "NormalValues"
        Me.btnUpdateNormalValues.UseVisualStyleBackColor = True
        '
        'chkAllowSpecialValues
        '
        Me.chkAllowSpecialValues.AutoSize = True
        Me.chkAllowSpecialValues.Checked = True
        Me.chkAllowSpecialValues.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkAllowSpecialValues.Location = New System.Drawing.Point(586, 37)
        Me.chkAllowSpecialValues.Name = "chkAllowSpecialValues"
        Me.chkAllowSpecialValues.Size = New System.Drawing.Size(118, 17)
        Me.chkAllowSpecialValues.TabIndex = 115
        Me.chkAllowSpecialValues.Text = "AllowSpecialValues"
        Me.chkAllowSpecialValues.ThreeState = True
        Me.chkAllowSpecialValues.UseVisualStyleBackColor = True
        '
        'chkCheckValues
        '
        Me.chkCheckValues.AutoSize = True
        Me.chkCheckValues.Checked = True
        Me.chkCheckValues.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkCheckValues.Location = New System.Drawing.Point(586, 10)
        Me.chkCheckValues.Name = "chkCheckValues"
        Me.chkCheckValues.Size = New System.Drawing.Size(89, 17)
        Me.chkCheckValues.TabIndex = 114
        Me.chkCheckValues.Text = "CheckValues"
        Me.chkCheckValues.ThreeState = True
        Me.chkCheckValues.UseVisualStyleBackColor = True
        '
        'cboDataCode
        '
        Me.cboDataCode.FormattingEnabled = True
        Me.cboDataCode.Items.AddRange(New Object() {"RptData1", "RptData2", "RptData3"})
        Me.cboDataCode.Location = New System.Drawing.Point(331, 8)
        Me.cboDataCode.Name = "cboDataCode"
        Me.cboDataCode.Size = New System.Drawing.Size(106, 21)
        Me.cboDataCode.TabIndex = 113
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(272, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 112
        Me.Label6.Text = "DataCode"
        '
        'cboCollectionMethod
        '
        Me.cboCollectionMethod.FormattingEnabled = True
        Me.cboCollectionMethod.Items.AddRange(New Object() {"AGTINPUT", "PROFILE", "RAS"})
        Me.cboCollectionMethod.Location = New System.Drawing.Point(97, 35)
        Me.cboCollectionMethod.Name = "cboCollectionMethod"
        Me.cboCollectionMethod.Size = New System.Drawing.Size(177, 21)
        Me.cboCollectionMethod.TabIndex = 111
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 13)
        Me.Label5.TabIndex = 110
        Me.Label5.Text = "CollectionMethod"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(272, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 109
        Me.Label1.Text = "Mandatory"
        '
        'cboMandatory
        '
        Me.cboMandatory.FormattingEnabled = True
        Me.cboMandatory.Items.AddRange(New Object() {"M", "C", "O"})
        Me.cboMandatory.Location = New System.Drawing.Point(331, 34)
        Me.cboMandatory.Name = "cboMandatory"
        Me.cboMandatory.Size = New System.Drawing.Size(106, 21)
        Me.cboMandatory.TabIndex = 108
        '
        'Details
        '
        Me.Details.DataPropertyName = "Details"
        Me.Details.HeaderText = "Details"
        Me.Details.Name = "Details"
        Me.Details.ReadOnly = True
        Me.Details.Visible = False
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(244, 593)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(91, 20)
        Me.btnEdit.TabIndex = 107
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'dgrRptData
        '
        Me.dgrRptData.AllowUserToAddRows = False
        Me.dgrRptData.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrRptData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgrRptData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrRptData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RecId, Me.CustShortName, Me.DataCode, Me.NameByCustomer, Me.MinLength, Me.MaxLength, Me.Mandatory, Me.ConditionOfUse, Me.CharType, Me.CollectionMethod, Me.DefaultValue, Me.CheckValues, Me.AllowSpecialValues, Me.CustId, Me.Details})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgrRptData.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgrRptData.Location = New System.Drawing.Point(10, 83)
        Me.dgrRptData.Name = "dgrRptData"
        Me.dgrRptData.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrRptData.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgrRptData.RowHeadersVisible = False
        Me.dgrRptData.Size = New System.Drawing.Size(867, 504)
        Me.dgrRptData.TabIndex = 99
        '
        'RecId
        '
        Me.RecId.DataPropertyName = "RecID"
        Me.RecId.HeaderText = "RecId"
        Me.RecId.Name = "RecId"
        Me.RecId.ReadOnly = True
        Me.RecId.Width = 30
        '
        'CustShortName
        '
        Me.CustShortName.DataPropertyName = "CustShortName"
        Me.CustShortName.HeaderText = "CustShortName"
        Me.CustShortName.Name = "CustShortName"
        Me.CustShortName.ReadOnly = True
        Me.CustShortName.Width = 90
        '
        'DataCode
        '
        Me.DataCode.DataPropertyName = "DataCode"
        Me.DataCode.HeaderText = "DataCode"
        Me.DataCode.Name = "DataCode"
        Me.DataCode.ReadOnly = True
        Me.DataCode.Width = 60
        '
        'NameByCustomer
        '
        Me.NameByCustomer.DataPropertyName = "NameByCustomer"
        Me.NameByCustomer.HeaderText = "NameByCustomer"
        Me.NameByCustomer.Name = "NameByCustomer"
        Me.NameByCustomer.ReadOnly = True
        Me.NameByCustomer.Width = 180
        '
        'MinLength
        '
        Me.MinLength.DataPropertyName = "MinLength"
        Me.MinLength.HeaderText = "MinLength"
        Me.MinLength.Name = "MinLength"
        Me.MinLength.ReadOnly = True
        Me.MinLength.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MinLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MinLength.Width = 60
        '
        'MaxLength
        '
        Me.MaxLength.DataPropertyName = "MaxLength"
        Me.MaxLength.HeaderText = "MaxLength"
        Me.MaxLength.Name = "MaxLength"
        Me.MaxLength.ReadOnly = True
        Me.MaxLength.Width = 70
        '
        'Mandatory
        '
        Me.Mandatory.DataPropertyName = "Mandatory"
        Me.Mandatory.HeaderText = "Mandatory"
        Me.Mandatory.Name = "Mandatory"
        Me.Mandatory.ReadOnly = True
        Me.Mandatory.Width = 60
        '
        'ConditionOfUse
        '
        Me.ConditionOfUse.DataPropertyName = "ConditionOfUse"
        Me.ConditionOfUse.HeaderText = "ConditionOfUse"
        Me.ConditionOfUse.Name = "ConditionOfUse"
        Me.ConditionOfUse.ReadOnly = True
        '
        'CharType
        '
        Me.CharType.DataPropertyName = "CharType"
        Me.CharType.HeaderText = "CharType"
        Me.CharType.Name = "CharType"
        Me.CharType.ReadOnly = True
        Me.CharType.Width = 60
        '
        'CollectionMethod
        '
        Me.CollectionMethod.DataPropertyName = "CollectionMethod"
        Me.CollectionMethod.HeaderText = "CollectionMethod"
        Me.CollectionMethod.Name = "CollectionMethod"
        Me.CollectionMethod.ReadOnly = True
        Me.CollectionMethod.Width = 60
        '
        'DefaultValue
        '
        Me.DefaultValue.DataPropertyName = "DefaultValue"
        Me.DefaultValue.HeaderText = "DefaultValue"
        Me.DefaultValue.Name = "DefaultValue"
        Me.DefaultValue.ReadOnly = True
        '
        'CheckValues
        '
        Me.CheckValues.DataPropertyName = "CheckValues"
        Me.CheckValues.HeaderText = "CheckValues"
        Me.CheckValues.Name = "CheckValues"
        Me.CheckValues.ReadOnly = True
        Me.CheckValues.Width = 70
        '
        'AllowSpecialValues
        '
        Me.AllowSpecialValues.DataPropertyName = "AllowSpecialValues"
        Me.AllowSpecialValues.HeaderText = "AllowSpecialValues"
        Me.AllowSpecialValues.Name = "AllowSpecialValues"
        Me.AllowSpecialValues.ReadOnly = True
        Me.AllowSpecialValues.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AllowSpecialValues.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AllowSpecialValues.Width = 90
        '
        'CustId
        '
        Me.CustId.DataPropertyName = "CustId"
        Me.CustId.HeaderText = "CustId"
        Me.CustId.Name = "CustId"
        Me.CustId.ReadOnly = True
        Me.CustId.Visible = False
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(10, 593)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(91, 20)
        Me.btnNew.TabIndex = 106
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(790, 45)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 105
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'cboCustShortName
        '
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(97, 8)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(177, 21)
        Me.cboCustShortName.TabIndex = 104
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 102
        Me.Label2.Text = "CustShortName"
        '
        'btnSeacrh
        '
        Me.btnSeacrh.Location = New System.Drawing.Point(790, 16)
        Me.btnSeacrh.Name = "btnSeacrh"
        Me.btnSeacrh.Size = New System.Drawing.Size(75, 23)
        Me.btnSeacrh.TabIndex = 101
        Me.btnSeacrh.Text = "Search"
        Me.btnSeacrh.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(361, 593)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(103, 20)
        Me.btnDelete.TabIndex = 103
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(490, 593)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(103, 20)
        Me.btnCancel.TabIndex = 100
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "XX"})
        Me.cboStatus.Location = New System.Drawing.Point(495, 8)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(85, 21)
        Me.cboStatus.TabIndex = 123
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(443, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 122
        Me.Label4.Text = "Status"
        '
        'frmRptDataList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 621)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbkCloneCustomer)
        Me.Controls.Add(Me.btnClone)
        Me.Controls.Add(Me.cboApplyTo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnUpdateSpecialValues)
        Me.Controls.Add(Me.btnUpdateNormalValues)
        Me.Controls.Add(Me.chkAllowSpecialValues)
        Me.Controls.Add(Me.chkCheckValues)
        Me.Controls.Add(Me.cboDataCode)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboCollectionMethod)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboMandatory)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.dgrRptData)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnSeacrh)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCancel)
        Me.Name = "frmRptDataList"
        Me.Text = "RptDataList"
        CType(Me.dgrRptData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkCloneCustomer As LinkLabel
    Friend WithEvents btnClone As Button
    Friend WithEvents cboApplyTo As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnUpdateSpecialValues As Button
    Friend WithEvents btnUpdateNormalValues As Button
    Friend WithEvents chkAllowSpecialValues As CheckBox
    Friend WithEvents chkCheckValues As CheckBox
    Friend WithEvents cboDataCode As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cboCollectionMethod As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cboMandatory As ComboBox
    Friend WithEvents Details As DataGridViewTextBoxColumn
    Friend WithEvents btnEdit As Button
    Friend WithEvents dgrRptData As DataGridView
    Friend WithEvents RecId As DataGridViewTextBoxColumn
    Friend WithEvents CustShortName As DataGridViewTextBoxColumn
    Friend WithEvents DataCode As DataGridViewTextBoxColumn
    Friend WithEvents NameByCustomer As DataGridViewTextBoxColumn
    Friend WithEvents MinLength As DataGridViewTextBoxColumn
    Friend WithEvents MaxLength As DataGridViewTextBoxColumn
    Friend WithEvents Mandatory As DataGridViewTextBoxColumn
    Friend WithEvents ConditionOfUse As DataGridViewTextBoxColumn
    Friend WithEvents CharType As DataGridViewTextBoxColumn
    Friend WithEvents CollectionMethod As DataGridViewTextBoxColumn
    Friend WithEvents DefaultValue As DataGridViewTextBoxColumn
    Friend WithEvents CheckValues As DataGridViewCheckBoxColumn
    Friend WithEvents AllowSpecialValues As DataGridViewCheckBoxColumn
    Friend WithEvents CustId As DataGridViewTextBoxColumn
    Friend WithEvents btnNew As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents cboCustShortName As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnSeacrh As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents Label4 As Label
End Class
