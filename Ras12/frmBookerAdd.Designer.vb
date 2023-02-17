<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBookerAdd
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtBookerName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTel = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtReportGroup = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "CustShortName"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(89, 258)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 20)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(226, 258)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 20)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtBookerName
        '
        Me.txtBookerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBookerName.Location = New System.Drawing.Point(104, 67)
        Me.txtBookerName.Name = "txtBookerName"
        Me.txtBookerName.Size = New System.Drawing.Size(215, 20)
        Me.txtBookerName.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 71
        Me.Label3.Text = "Booker Name"
        '
        'txtEmail
        '
        Me.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEmail.Location = New System.Drawing.Point(104, 100)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(215, 20)
        Me.txtEmail.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 76
        Me.Label2.Text = "Email"
        '
        'cboCustShortName
        '
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(104, 33)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(215, 21)
        Me.cboCustShortName.TabIndex = 0
        '
        'txtRecId
        '
        Me.txtRecId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(104, 0)
        Me.txtRecId.MaxLength = 32
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(65, 20)
        Me.txtRecId.TabIndex = 105
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 13)
        Me.Label7.TabIndex = 106
        Me.Label7.Text = "RecID"
        '
        'txtLocation
        '
        Me.txtLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLocation.Location = New System.Drawing.Point(104, 133)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(215, 20)
        Me.txtLocation.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 108
        Me.Label4.Text = "Location"
        '
        'txtTel
        '
        Me.txtTel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTel.Location = New System.Drawing.Point(104, 166)
        Me.txtTel.Name = "txtTel"
        Me.txtTel.Size = New System.Drawing.Size(215, 20)
        Me.txtTel.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 173)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 13)
        Me.Label5.TabIndex = 110
        Me.Label5.Text = "Tel"
        '
        'txtReportGroup
        '
        Me.txtReportGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReportGroup.Location = New System.Drawing.Point(104, 199)
        Me.txtReportGroup.Name = "txtReportGroup"
        Me.txtReportGroup.Size = New System.Drawing.Size(215, 20)
        Me.txtReportGroup.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 206)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 13)
        Me.Label6.TabIndex = 112
        Me.Label6.Text = "ReportGroup"
        '
        'txtRemark
        '
        Me.txtRemark.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemark.Location = New System.Drawing.Point(104, 232)
        Me.txtRemark.MaxLength = 64
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(215, 20)
        Me.txtRemark.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 239)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 114
        Me.Label8.Text = "Remark"
        '
        'frmBookerAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 286)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtReportGroup)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTel)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtBookerName)
        Me.Controls.Add(Me.Label3)
        Me.Name = "frmBookerAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Booker Add"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtBookerName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboCustShortName As System.Windows.Forms.ComboBox
    Friend WithEvents txtRecId As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTel As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtReportGroup As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
