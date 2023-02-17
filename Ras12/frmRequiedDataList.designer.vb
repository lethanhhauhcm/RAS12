<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRequiedDataList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgrRequiredData = New System.Windows.Forms.DataGridView()
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
        Me.Details = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnSeacrh = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.cboMandatory = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboCollectionMethod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboDataCode = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkCheckValues = New System.Windows.Forms.CheckBox()
        Me.chkAllowSpecialValues = New System.Windows.Forms.CheckBox()
        Me.btnUpdateNormalValues = New System.Windows.Forms.Button()
        Me.btnUpdateSpecialValues = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboApplyTo = New System.Windows.Forms.ComboBox()
        Me.btnClone = New System.Windows.Forms.Button()
        Me.lbkCloneCustomer = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrRequiredData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrRequiredData
        '
        Me.dgrRequiredData.AllowUserToAddRows = False
        Me.dgrRequiredData.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrRequiredData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgrRequiredData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrRequiredData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RecId, Me.CustShortName, Me.DataCode, Me.NameByCustomer, Me.MinLength, Me.MaxLength, Me.Mandatory, Me.ConditionOfUse, Me.CharType, Me.CollectionMethod, Me.DefaultValue, Me.CheckValues, Me.AllowSpecialValues, Me.CustId, Me.Details})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgrRequiredData.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgrRequiredData.Location = New System.Drawing.Point(12, 81)
        Me.dgrRequiredData.Name = "dgrRequiredData"
        Me.dgrRequiredData.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrRequiredData.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgrRequiredData.RowHeadersVisible = False
        Me.dgrRequiredData.Size = New System.Drawing.Size(867, 504)
        Me.dgrRequiredData.TabIndex = 56
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
        'Details
        '
        Me.Details.DataPropertyName = "Details"
        Me.Details.HeaderText = "Details"
        Me.Details.Name = "Details"
        Me.Details.ReadOnly = True
        Me.Details.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(492, 591)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(103, 20)
        Me.btnCancel.TabIndex = 57
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(363, 591)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(103, 20)
        Me.btnDelete.TabIndex = 63
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(246, 591)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(91, 20)
        Me.btnEdit.TabIndex = 73
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnSeacrh
        '
        Me.btnSeacrh.Location = New System.Drawing.Point(792, 14)
        Me.btnSeacrh.Name = "btnSeacrh"
        Me.btnSeacrh.Size = New System.Drawing.Size(75, 23)
        Me.btnSeacrh.TabIndex = 59
        Me.btnSeacrh.Text = "Search"
        Me.btnSeacrh.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "CustShortName"
        '
        'cboCustShortName
        '
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(99, 6)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(177, 21)
        Me.cboCustShortName.TabIndex = 66
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(792, 43)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 67
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(12, 591)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(91, 20)
        Me.btnNew.TabIndex = 72
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'cboMandatory
        '
        Me.cboMandatory.FormattingEnabled = True
        Me.cboMandatory.Items.AddRange(New Object() {"M", "C", "O"})
        Me.cboMandatory.Location = New System.Drawing.Point(333, 32)
        Me.cboMandatory.Name = "cboMandatory"
        Me.cboMandatory.Size = New System.Drawing.Size(106, 21)
        Me.cboMandatory.TabIndex = 76
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(274, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 77
        Me.Label1.Text = "Mandatory"
        '
        'cboCollectionMethod
        '
        Me.cboCollectionMethod.FormattingEnabled = True
        Me.cboCollectionMethod.Items.AddRange(New Object() {"AGTINPUT", "PROFILE", "RAS"})
        Me.cboCollectionMethod.Location = New System.Drawing.Point(99, 33)
        Me.cboCollectionMethod.Name = "cboCollectionMethod"
        Me.cboCollectionMethod.Size = New System.Drawing.Size(177, 21)
        Me.cboCollectionMethod.TabIndex = 79
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 13)
        Me.Label5.TabIndex = 78
        Me.Label5.Text = "CollectionMethod"
        '
        'cboDataCode
        '
        Me.cboDataCode.FormattingEnabled = True
        Me.cboDataCode.Location = New System.Drawing.Point(333, 6)
        Me.cboDataCode.Name = "cboDataCode"
        Me.cboDataCode.Size = New System.Drawing.Size(249, 21)
        Me.cboDataCode.TabIndex = 81
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(274, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 80
        Me.Label6.Text = "DataCode"
        '
        'chkCheckValues
        '
        Me.chkCheckValues.AutoSize = True
        Me.chkCheckValues.Checked = True
        Me.chkCheckValues.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkCheckValues.Location = New System.Drawing.Point(588, 8)
        Me.chkCheckValues.Name = "chkCheckValues"
        Me.chkCheckValues.Size = New System.Drawing.Size(89, 17)
        Me.chkCheckValues.TabIndex = 82
        Me.chkCheckValues.Text = "CheckValues"
        Me.chkCheckValues.ThreeState = True
        Me.chkCheckValues.UseVisualStyleBackColor = True
        '
        'chkAllowSpecialValues
        '
        Me.chkAllowSpecialValues.AutoSize = True
        Me.chkAllowSpecialValues.Checked = True
        Me.chkAllowSpecialValues.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkAllowSpecialValues.Location = New System.Drawing.Point(588, 35)
        Me.chkAllowSpecialValues.Name = "chkAllowSpecialValues"
        Me.chkAllowSpecialValues.Size = New System.Drawing.Size(118, 17)
        Me.chkAllowSpecialValues.TabIndex = 83
        Me.chkAllowSpecialValues.Text = "AllowSpecialValues"
        Me.chkAllowSpecialValues.ThreeState = True
        Me.chkAllowSpecialValues.UseVisualStyleBackColor = True
        '
        'btnUpdateNormalValues
        '
        Me.btnUpdateNormalValues.Location = New System.Drawing.Point(621, 591)
        Me.btnUpdateNormalValues.Name = "btnUpdateNormalValues"
        Me.btnUpdateNormalValues.Size = New System.Drawing.Size(91, 20)
        Me.btnUpdateNormalValues.TabIndex = 84
        Me.btnUpdateNormalValues.Text = "NormalValues"
        Me.btnUpdateNormalValues.UseVisualStyleBackColor = True
        '
        'btnUpdateSpecialValues
        '
        Me.btnUpdateSpecialValues.Location = New System.Drawing.Point(738, 591)
        Me.btnUpdateSpecialValues.Name = "btnUpdateSpecialValues"
        Me.btnUpdateSpecialValues.Size = New System.Drawing.Size(91, 20)
        Me.btnUpdateSpecialValues.TabIndex = 85
        Me.btnUpdateSpecialValues.Text = "SpeciallValues"
        Me.btnUpdateSpecialValues.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(445, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 87
        Me.Label3.Text = "ApplyTo"
        '
        'cboApplyTo
        '
        Me.cboApplyTo.FormattingEnabled = True
        Me.cboApplyTo.Items.AddRange(New Object() {"ALL", "AIR", "HTL", "ALL", "CAR", "N-A"})
        Me.cboApplyTo.Location = New System.Drawing.Point(497, 32)
        Me.cboApplyTo.Name = "cboApplyTo"
        Me.cboApplyTo.Size = New System.Drawing.Size(85, 21)
        Me.cboApplyTo.TabIndex = 96
        '
        'btnClone
        '
        Me.btnClone.Location = New System.Drawing.Point(129, 591)
        Me.btnClone.Name = "btnClone"
        Me.btnClone.Size = New System.Drawing.Size(91, 20)
        Me.btnClone.TabIndex = 97
        Me.btnClone.Text = "Clone"
        Me.btnClone.UseVisualStyleBackColor = True
        '
        'lbkCloneCustomer
        '
        Me.lbkCloneCustomer.AutoSize = True
        Me.lbkCloneCustomer.Location = New System.Drawing.Point(12, 65)
        Me.lbkCloneCustomer.Name = "lbkCloneCustomer"
        Me.lbkCloneCustomer.Size = New System.Drawing.Size(78, 13)
        Me.lbkCloneCustomer.TabIndex = 98
        Me.lbkCloneCustomer.TabStop = True
        Me.lbkCloneCustomer.Text = "CloneCustomer"
        '
        'frmRequiedDataList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 623)
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
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnSeacrh)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.dgrRequiredData)
        Me.Name = "frmRequiedDataList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Required Data List"
        CType(Me.dgrRequiredData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgrRequiredData As System.Windows.Forms.DataGridView
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnSeacrh As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboCustShortName As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents cboMandatory As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboCollectionMethod As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboDataCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkCheckValues As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllowSpecialValues As System.Windows.Forms.CheckBox
    Friend WithEvents btnUpdateNormalValues As System.Windows.Forms.Button
    Friend WithEvents btnUpdateSpecialValues As System.Windows.Forms.Button
    Friend WithEvents RecId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CustShortName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameByCustomer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MinLength As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaxLength As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Mandatory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConditionOfUse As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CharType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CollectionMethod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DefaultValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CheckValues As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents AllowSpecialValues As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents CustId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Details As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboApplyTo As System.Windows.Forms.ComboBox
    Friend WithEvents btnClone As System.Windows.Forms.Button
    Friend WithEvents lbkCloneCustomer As LinkLabel
End Class
