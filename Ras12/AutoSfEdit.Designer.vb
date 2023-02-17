<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoSfEdit
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
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.txtPct = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpValidFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpValidTo = New System.Windows.Forms.DateTimePicker()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.cboServiceType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(12, 123)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 21
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'txtPct
        '
        Me.txtPct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPct.Location = New System.Drawing.Point(93, 33)
        Me.txtPct.Name = "txtPct"
        Me.txtPct.Size = New System.Drawing.Size(165, 20)
        Me.txtPct.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "ValidFrom/To"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Pct"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "CustShortName"
        '
        'dtpValidFrom
        '
        Me.dtpValidFrom.CustomFormat = "dd MMM yy"
        Me.dtpValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpValidFrom.Location = New System.Drawing.Point(93, 63)
        Me.dtpValidFrom.Name = "dtpValidFrom"
        Me.dtpValidFrom.Size = New System.Drawing.Size(117, 20)
        Me.dtpValidFrom.TabIndex = 24
        '
        'dtpValidTo
        '
        Me.dtpValidTo.CustomFormat = "dd MMM yy"
        Me.dtpValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpValidTo.Location = New System.Drawing.Point(260, 63)
        Me.dtpValidTo.Name = "dtpValidTo"
        Me.dtpValidTo.Size = New System.Drawing.Size(117, 20)
        Me.dtpValidTo.TabIndex = 25
        '
        'txtRecId
        '
        Me.txtRecId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(335, 6)
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(61, 20)
        Me.txtRecId.TabIndex = 26
        Me.txtRecId.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(297, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "RecId"
        '
        'cboCustShortName
        '
        Me.cboCustShortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(93, 5)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(165, 21)
        Me.cboCustShortName.TabIndex = 66
        '
        'cboServiceType
        '
        Me.cboServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboServiceType.FormattingEnabled = True
        Me.cboServiceType.Items.AddRange(New Object() {"AFTER WH", "INTL REG", "NORMAL", "STANDARD", "URGENT"})
        Me.cboServiceType.Location = New System.Drawing.Point(93, 89)
        Me.cboServiceType.Name = "cboServiceType"
        Me.cboServiceType.Size = New System.Drawing.Size(117, 21)
        Me.cboServiceType.TabIndex = 70
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 71
        Me.Label4.Text = "ServiceType"
        '
        'frmAutoSfEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(408, 261)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboServiceType)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.dtpValidTo)
        Me.Controls.Add(Me.dtpValidFrom)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.txtPct)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmAutoSfEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AutoServiceFeeEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents txtPct As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpValidFrom As DateTimePicker
    Friend WithEvents dtpValidTo As DateTimePicker
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cboCustShortName As ComboBox
    Friend WithEvents cboServiceType As ComboBox
    Friend WithEvents Label4 As Label
End Class
