<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRequiredDataEdit
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
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboCharType = New System.Windows.Forms.ComboBox()
        Me.txtMaxLength = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtMinLength = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkCheckValues = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboMandatory = New System.Windows.Forms.ComboBox()
        Me.cboCollectionMethod = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkAllowSpecialValues = New System.Windows.Forms.CheckBox()
        Me.txtDefaultValue = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtConditionOfUse = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboApplyTo = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtRecID
        '
        Me.txtRecID.Enabled = False
        Me.txtRecID.Location = New System.Drawing.Point(102, 11)
        Me.txtRecID.Name = "txtRecID"
        Me.txtRecID.Size = New System.Drawing.Size(67, 20)
        Me.txtRecID.TabIndex = 70
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "REC ID"
        '
        'txtNameByCustomer
        '
        Me.txtNameByCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNameByCustomer.Location = New System.Drawing.Point(102, 63)
        Me.txtNameByCustomer.Name = "txtNameByCustomer"
        Me.txtNameByCustomer.Size = New System.Drawing.Size(376, 20)
        Me.txtNameByCustomer.TabIndex = 67
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 66
        Me.Label3.Text = "NameByCustomer"
        '
        'cboDataCode
        '
        Me.cboDataCode.FormattingEnabled = True
        Me.cboDataCode.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cboDataCode.Location = New System.Drawing.Point(351, 37)
        Me.cboDataCode.Name = "cboDataCode"
        Me.cboDataCode.Size = New System.Drawing.Size(255, 21)
        Me.cboDataCode.TabIndex = 64
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(290, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 65
        Me.Label2.Text = "DataCode"
        '
        'cboCustShortName
        '
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(102, 37)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(188, 21)
        Me.cboCustShortName.TabIndex = 62
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(515, 149)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(91, 20)
        Me.btnSave.TabIndex = 61
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 63
        Me.Label1.Text = "CustShortName"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(515, 175)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(91, 20)
        Me.btnCancel.TabIndex = 60
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 175)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 79
        Me.Label5.Text = "CharType"
        '
        'cboCharType
        '
        Me.cboCharType.FormattingEnabled = True
        Me.cboCharType.Items.AddRange(New Object() {"ALPHA", "NUMERIC", "EITHER"})
        Me.cboCharType.Location = New System.Drawing.Point(102, 172)
        Me.cboCharType.Name = "cboCharType"
        Me.cboCharType.Size = New System.Drawing.Size(131, 21)
        Me.cboCharType.TabIndex = 78
        '
        'txtMaxLength
        '
        Me.txtMaxLength.Location = New System.Drawing.Point(351, 199)
        Me.txtMaxLength.MaxLength = 32
        Me.txtMaxLength.Name = "txtMaxLength"
        Me.txtMaxLength.Size = New System.Drawing.Size(129, 20)
        Me.txtMaxLength.TabIndex = 83
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(261, 201)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 82
        Me.Label6.Text = "MaxLength"
        '
        'txtMinLength
        '
        Me.txtMinLength.Location = New System.Drawing.Point(102, 199)
        Me.txtMinLength.MaxLength = 32
        Me.txtMinLength.Name = "txtMinLength"
        Me.txtMinLength.Size = New System.Drawing.Size(129, 20)
        Me.txtMinLength.TabIndex = 81
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 201)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 80
        Me.Label7.Text = "MinLength"
        '
        'chkCheckValues
        '
        Me.chkCheckValues.AutoSize = True
        Me.chkCheckValues.Location = New System.Drawing.Point(517, 71)
        Me.chkCheckValues.Name = "chkCheckValues"
        Me.chkCheckValues.Size = New System.Drawing.Size(89, 17)
        Me.chkCheckValues.TabIndex = 84
        Me.chkCheckValues.Text = "CheckValues"
        Me.chkCheckValues.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 92)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 86
        Me.Label8.Text = "Mandatory"
        '
        'cboMandatory
        '
        Me.cboMandatory.FormattingEnabled = True
        Me.cboMandatory.Items.AddRange(New Object() {"M", "C", "O"})
        Me.cboMandatory.Location = New System.Drawing.Point(102, 89)
        Me.cboMandatory.Name = "cboMandatory"
        Me.cboMandatory.Size = New System.Drawing.Size(131, 21)
        Me.cboMandatory.TabIndex = 85
        '
        'cboCollectionMethod
        '
        Me.cboCollectionMethod.FormattingEnabled = True
        Me.cboCollectionMethod.Items.AddRange(New Object() {"AGTINPUT", "PROFILE", "RAS"})
        Me.cboCollectionMethod.Location = New System.Drawing.Point(351, 89)
        Me.cboCollectionMethod.Name = "cboCollectionMethod"
        Me.cboCollectionMethod.Size = New System.Drawing.Size(129, 21)
        Me.cboCollectionMethod.TabIndex = 88
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(256, 92)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 13)
        Me.Label9.TabIndex = 87
        Me.Label9.Text = "CollectionMethod"
        '
        'chkAllowSpecialValues
        '
        Me.chkAllowSpecialValues.AutoSize = True
        Me.chkAllowSpecialValues.Location = New System.Drawing.Point(517, 92)
        Me.chkAllowSpecialValues.Name = "chkAllowSpecialValues"
        Me.chkAllowSpecialValues.Size = New System.Drawing.Size(118, 17)
        Me.chkAllowSpecialValues.TabIndex = 89
        Me.chkAllowSpecialValues.Text = "AllowSpecialValues"
        Me.chkAllowSpecialValues.UseVisualStyleBackColor = True
        '
        'txtDefaultValue
        '
        Me.txtDefaultValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDefaultValue.Location = New System.Drawing.Point(102, 142)
        Me.txtDefaultValue.Name = "txtDefaultValue"
        Me.txtDefaultValue.Size = New System.Drawing.Size(376, 20)
        Me.txtDefaultValue.TabIndex = 91
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(7, 144)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 90
        Me.Label10.Text = "DefaultValue"
        '
        'txtConditionOfUse
        '
        Me.txtConditionOfUse.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtConditionOfUse.Location = New System.Drawing.Point(102, 116)
        Me.txtConditionOfUse.Name = "txtConditionOfUse"
        Me.txtConditionOfUse.Size = New System.Drawing.Size(376, 20)
        Me.txtConditionOfUse.TabIndex = 93
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(5, 118)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(81, 13)
        Me.Label11.TabIndex = 92
        Me.Label11.Text = "ConditionOfUse"
        '
        'cboApplyTo
        '
        Me.cboApplyTo.FormattingEnabled = True
        Me.cboApplyTo.Items.AddRange(New Object() {"ALL", "AIR", "HTL", "ALL", "CAR", "N-A"})
        Me.cboApplyTo.Location = New System.Drawing.Point(351, 169)
        Me.cboApplyTo.Name = "cboApplyTo"
        Me.cboApplyTo.Size = New System.Drawing.Size(129, 21)
        Me.cboApplyTo.TabIndex = 95
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(261, 175)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 13)
        Me.Label12.TabIndex = 94
        Me.Label12.Text = "ApplyTo"
        '
        'frmRequiredDataEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(646, 223)
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
        Me.Name = "frmRequiredDataEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RequiredDataEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtRecID As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNameByCustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboDataCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboCustShortName As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboCharType As System.Windows.Forms.ComboBox
    Friend WithEvents txtMaxLength As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtMinLength As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkCheckValues As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboMandatory As System.Windows.Forms.ComboBox
    Friend WithEvents cboCollectionMethod As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkAllowSpecialValues As System.Windows.Forms.CheckBox
    Friend WithEvents txtDefaultValue As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtConditionOfUse As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboApplyTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
End Class
