<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRequiredDataAdd
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.cboCorporate = New System.Windows.Forms.ComboBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.cboCdrCode = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtMeaning = New System.Windows.Forms.TextBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.txtMinLength = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtMaxLength = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cboCharType = New System.Windows.Forms.ComboBox
        Me.chkAgtInput = New System.Windows.Forms.CheckBox
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CorpName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CdrCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CDRMeaning = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CMC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MinLength = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MaxLength = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CharType = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboCorporate
        '
        Me.cboCorporate.FormattingEnabled = True
        Me.cboCorporate.Location = New System.Drawing.Point(93, 14)
        Me.cboCorporate.Name = "cboCorporate"
        Me.cboCorporate.Size = New System.Drawing.Size(129, 21)
        Me.cboCorporate.TabIndex = 48
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(240, 488)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(91, 20)
        Me.btnOK.TabIndex = 47
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "CorpName"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(445, 488)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(91, 20)
        Me.btnCancel.TabIndex = 46
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'cboCdrCode
        '
        Me.cboCdrCode.FormattingEnabled = True
        Me.cboCdrCode.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cboCdrCode.Location = New System.Drawing.Point(310, 14)
        Me.cboCdrCode.Name = "cboCdrCode"
        Me.cboCdrCode.Size = New System.Drawing.Size(129, 21)
        Me.cboCdrCode.TabIndex = 50
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(238, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "CDR Code"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Meaning"
        '
        'txtMeaning
        '
        Me.txtMeaning.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMeaning.Location = New System.Drawing.Point(93, 68)
        Me.txtMeaning.MaxLength = 32
        Me.txtMeaning.Name = "txtMeaning"
        Me.txtMeaning.Size = New System.Drawing.Size(629, 20)
        Me.txtMeaning.TabIndex = 53
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.CorpName, Me.CdrCode, Me.CDRMeaning, Me.CMC, Me.MinLength, Me.MaxLength, Me.CharType})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.Location = New System.Drawing.Point(19, 105)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(703, 367)
        Me.DataGridView1.TabIndex = 57
        '
        'txtMinLength
        '
        Me.txtMinLength.Location = New System.Drawing.Point(93, 42)
        Me.txtMinLength.MaxLength = 32
        Me.txtMinLength.Name = "txtMinLength"
        Me.txtMinLength.Size = New System.Drawing.Size(129, 20)
        Me.txtMinLength.TabIndex = 59
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "MinLength"
        '
        'txtMaxLength
        '
        Me.txtMaxLength.Location = New System.Drawing.Point(310, 42)
        Me.txtMaxLength.MaxLength = 32
        Me.txtMaxLength.Name = "txtMaxLength"
        Me.txtMaxLength.Size = New System.Drawing.Size(129, 20)
        Me.txtMaxLength.TabIndex = 61
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(238, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 13)
        Me.Label5.TabIndex = 60
        Me.Label5.Text = "MaxLength"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(458, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 13)
        Me.Label6.TabIndex = 79
        Me.Label6.Text = "CharType"
        '
        'cboCharType
        '
        Me.cboCharType.FormattingEnabled = True
        Me.cboCharType.Items.AddRange(New Object() {"ALPHA", "NUMERIC", "EITHER"})
        Me.cboCharType.Location = New System.Drawing.Point(532, 41)
        Me.cboCharType.Name = "cboCharType"
        Me.cboCharType.Size = New System.Drawing.Size(67, 21)
        Me.cboCharType.TabIndex = 78
        '
        'chkAgtInput
        '
        Me.chkAgtInput.AutoSize = True
        Me.chkAgtInput.Location = New System.Drawing.Point(461, 12)
        Me.chkAgtInput.Name = "chkAgtInput"
        Me.chkAgtInput.Size = New System.Drawing.Size(66, 17)
        Me.chkAgtInput.TabIndex = 85
        Me.chkAgtInput.Text = "AgtInput"
        Me.chkAgtInput.UseVisualStyleBackColor = True
        '
        'ID
        '
        Me.ID.DataPropertyName = "RecID"
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Width = 30
        '
        'CorpName
        '
        Me.CorpName.DataPropertyName = "CorpName"
        Me.CorpName.HeaderText = "CorpName"
        Me.CorpName.Name = "CorpName"
        Me.CorpName.ReadOnly = True
        Me.CorpName.Width = 150
        '
        'CdrCode
        '
        Me.CdrCode.DataPropertyName = "CdrNbr"
        Me.CdrCode.HeaderText = "CdrCode"
        Me.CdrCode.Name = "CdrCode"
        Me.CdrCode.ReadOnly = True
        Me.CdrCode.Width = 50
        '
        'CDRMeaning
        '
        Me.CDRMeaning.DataPropertyName = "CdrName"
        Me.CDRMeaning.HeaderText = "CDR Meaning"
        Me.CDRMeaning.Name = "CDRMeaning"
        Me.CDRMeaning.ReadOnly = True
        Me.CDRMeaning.Width = 250
        '
        'CMC
        '
        Me.CMC.DataPropertyName = "CMC"
        Me.CMC.HeaderText = "CMC"
        Me.CMC.Name = "CMC"
        Me.CMC.ReadOnly = True
        Me.CMC.Visible = False
        '
        'MinLength
        '
        Me.MinLength.DataPropertyName = "MinLength"
        Me.MinLength.HeaderText = "MinLength"
        Me.MinLength.Name = "MinLength"
        Me.MinLength.ReadOnly = True
        Me.MinLength.Width = 70
        '
        'MaxLength
        '
        Me.MaxLength.DataPropertyName = "MaxLength"
        Me.MaxLength.HeaderText = "MaxLength"
        Me.MaxLength.Name = "MaxLength"
        Me.MaxLength.ReadOnly = True
        Me.MaxLength.Width = 70
        '
        'CharType
        '
        Me.CharType.DataPropertyName = "CharType"
        Me.CharType.HeaderText = "CharType"
        Me.CharType.Name = "CharType"
        Me.CharType.ReadOnly = True
        Me.CharType.Width = 70
        '
        'frmCdrAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(742, 523)
        Me.Controls.Add(Me.chkAgtInput)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboCharType)
        Me.Controls.Add(Me.txtMaxLength)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtMinLength)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtMeaning)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboCdrCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboCorporate)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancel)
        Me.Name = "frmCdrAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CDR Add"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboCorporate As System.Windows.Forms.ComboBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cboCdrCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMeaning As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtMinLength As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMaxLength As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboCharType As System.Windows.Forms.ComboBox
    Friend WithEvents chkAgtInput As System.Windows.Forms.CheckBox
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CorpName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CdrCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CDRMeaning As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MinLength As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaxLength As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CharType As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
