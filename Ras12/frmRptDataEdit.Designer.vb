<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptDataEdit
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
        Me.cboApplyTo = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtConditionOfUse = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDefaultValue = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkAllowSpecialValues = New System.Windows.Forms.CheckBox()
        Me.cboCollectionMethod = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboMandatory = New System.Windows.Forms.ComboBox()
        Me.chkCheckValues = New System.Windows.Forms.CheckBox()
        Me.txtMaxLength = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtMinLength = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboCharType = New System.Windows.Forms.ComboBox()
        Me.txtRecID = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNameByCustomer = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboDataCode = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cboApplyTo
        '
        Me.cboApplyTo.FormattingEnabled = True
        Me.cboApplyTo.Items.AddRange(New Object() {"ALL", "AIR", "HTL", "ALL", "CAR", "N-A"})
        Me.cboApplyTo.Location = New System.Drawing.Point(356, 161)
        Me.cboApplyTo.Name = "cboApplyTo"
        Me.cboApplyTo.Size = New System.Drawing.Size(129, 21)
        Me.cboApplyTo.TabIndex = 123
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(266, 167)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 13)
        Me.Label12.TabIndex = 122
        Me.Label12.Text = "ApplyTo"
        '
        'txtConditionOfUse
        '
        Me.txtConditionOfUse.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtConditionOfUse.Location = New System.Drawing.Point(107, 108)
        Me.txtConditionOfUse.Name = "txtConditionOfUse"
        Me.txtConditionOfUse.Size = New System.Drawing.Size(376, 20)
        Me.txtConditionOfUse.TabIndex = 121
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 110)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(81, 13)
        Me.Label11.TabIndex = 120
        Me.Label11.Text = "ConditionOfUse"
        '
        'txtDefaultValue
        '
        Me.txtDefaultValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDefaultValue.Location = New System.Drawing.Point(107, 134)
        Me.txtDefaultValue.Name = "txtDefaultValue"
        Me.txtDefaultValue.Size = New System.Drawing.Size(376, 20)
        Me.txtDefaultValue.TabIndex = 119
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 136)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 118
        Me.Label10.Text = "DefaultValue"
        '
        'chkAllowSpecialValues
        '
        Me.chkAllowSpecialValues.AutoSize = True
        Me.chkAllowSpecialValues.Location = New System.Drawing.Point(522, 84)
        Me.chkAllowSpecialValues.Name = "chkAllowSpecialValues"
        Me.chkAllowSpecialValues.Size = New System.Drawing.Size(118, 17)
        Me.chkAllowSpecialValues.TabIndex = 117
        Me.chkAllowSpecialValues.Text = "AllowSpecialValues"
        Me.chkAllowSpecialValues.UseVisualStyleBackColor = True
        '
        'cboCollectionMethod
        '
        Me.cboCollectionMethod.FormattingEnabled = True
        Me.cboCollectionMethod.Items.AddRange(New Object() {"AGTINPUT", "PROFILE", "RAS"})
        Me.cboCollectionMethod.Location = New System.Drawing.Point(356, 81)
        Me.cboCollectionMethod.Name = "cboCollectionMethod"
        Me.cboCollectionMethod.Size = New System.Drawing.Size(129, 21)
        Me.cboCollectionMethod.TabIndex = 116
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(261, 84)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 13)
        Me.Label9.TabIndex = 115
        Me.Label9.Text = "CollectionMethod"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 84)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 114
        Me.Label8.Text = "Mandatory"
        '
        'cboMandatory
        '
        Me.cboMandatory.FormattingEnabled = True
        Me.cboMandatory.Items.AddRange(New Object() {"M", "C", "O"})
        Me.cboMandatory.Location = New System.Drawing.Point(107, 81)
        Me.cboMandatory.Name = "cboMandatory"
        Me.cboMandatory.Size = New System.Drawing.Size(131, 21)
        Me.cboMandatory.TabIndex = 113
        '
        'chkCheckValues
        '
        Me.chkCheckValues.AutoSize = True
        Me.chkCheckValues.Location = New System.Drawing.Point(522, 63)
        Me.chkCheckValues.Name = "chkCheckValues"
        Me.chkCheckValues.Size = New System.Drawing.Size(89, 17)
        Me.chkCheckValues.TabIndex = 112
        Me.chkCheckValues.Text = "CheckValues"
        Me.chkCheckValues.UseVisualStyleBackColor = True
        '
        'txtMaxLength
        '
        Me.txtMaxLength.Location = New System.Drawing.Point(356, 191)
        Me.txtMaxLength.MaxLength = 32
        Me.txtMaxLength.Name = "txtMaxLength"
        Me.txtMaxLength.Size = New System.Drawing.Size(129, 20)
        Me.txtMaxLength.TabIndex = 111
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(266, 193)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 110
        Me.Label6.Text = "MaxLength"
        '
        'txtMinLength
        '
        Me.txtMinLength.Location = New System.Drawing.Point(107, 191)
        Me.txtMinLength.MaxLength = 32
        Me.txtMinLength.Name = "txtMinLength"
        Me.txtMinLength.Size = New System.Drawing.Size(129, 20)
        Me.txtMinLength.TabIndex = 109
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 193)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 108
        Me.Label7.Text = "MinLength"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 167)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 107
        Me.Label5.Text = "CharType"
        '
        'cboCharType
        '
        Me.cboCharType.FormattingEnabled = True
        Me.cboCharType.Items.AddRange(New Object() {"ALPHA", "NUMERIC", "EITHER"})
        Me.cboCharType.Location = New System.Drawing.Point(107, 164)
        Me.cboCharType.Name = "cboCharType"
        Me.cboCharType.Size = New System.Drawing.Size(131, 21)
        Me.cboCharType.TabIndex = 106
        '
        'txtRecID
        '
        Me.txtRecID.Enabled = False
        Me.txtRecID.Location = New System.Drawing.Point(107, 3)
        Me.txtRecID.Name = "txtRecID"
        Me.txtRecID.Size = New System.Drawing.Size(67, 20)
        Me.txtRecID.TabIndex = 105
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 104
        Me.Label4.Text = "REC ID"
        '
        'txtNameByCustomer
        '
        Me.txtNameByCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNameByCustomer.Location = New System.Drawing.Point(107, 55)
        Me.txtNameByCustomer.Name = "txtNameByCustomer"
        Me.txtNameByCustomer.Size = New System.Drawing.Size(376, 20)
        Me.txtNameByCustomer.TabIndex = 103
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 102
        Me.Label3.Text = "NameByCustomer"
        '
        'cboDataCode
        '
        Me.cboDataCode.FormattingEnabled = True
        Me.cboDataCode.Items.AddRange(New Object() {"RptData1", "RptData2", "RptData3"})
        Me.cboDataCode.Location = New System.Drawing.Point(356, 29)
        Me.cboDataCode.Name = "cboDataCode"
        Me.cboDataCode.Size = New System.Drawing.Size(255, 21)
        Me.cboDataCode.TabIndex = 100
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(295, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 101
        Me.Label2.Text = "DataCode"
        '
        'cboCustShortName
        '
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(107, 29)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(188, 21)
        Me.cboCustShortName.TabIndex = 98
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(520, 141)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(91, 20)
        Me.btnSave.TabIndex = 97
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 99
        Me.Label1.Text = "CustShortName"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(520, 167)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(91, 20)
        Me.btnCancel.TabIndex = 96
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmRptDataEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 217)
        Me.Controls.Add(Me.cboApplyTo)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtConditionOfUse)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtDefaultValue)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.chkAllowSpecialValues)
        Me.Controls.Add(Me.cboCollectionMethod)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboMandatory)
        Me.Controls.Add(Me.chkCheckValues)
        Me.Controls.Add(Me.txtMaxLength)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtMinLength)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboCharType)
        Me.Controls.Add(Me.txtRecID)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtNameByCustomer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboDataCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancel)
        Me.Name = "frmRptDataEdit"
        Me.Text = "RptDataEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboApplyTo As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtConditionOfUse As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtDefaultValue As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents chkAllowSpecialValues As CheckBox
    Friend WithEvents cboCollectionMethod As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cboMandatory As ComboBox
    Friend WithEvents chkCheckValues As CheckBox
    Friend WithEvents txtMaxLength As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtMinLength As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cboCharType As ComboBox
    Friend WithEvents txtRecID As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtNameByCustomer As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cboDataCode As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboCustShortName As ComboBox
    Friend WithEvents btnSave As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents btnCancel As Button
End Class
