<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIncentiveRtgEdit
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCar = New System.Windows.Forms.TextBox()
        Me.txtRtgType = New System.Windows.Forms.TextBox()
        Me.txtFromCountries = New System.Windows.Forms.TextBox()
        Me.txtToCountries = New System.Windows.Forms.TextBox()
        Me.txtFromCities = New System.Windows.Forms.TextBox()
        Me.txtToCities = New System.Windows.Forms.TextBox()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.blkCancel = New System.Windows.Forms.LinkLabel()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(82, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Car"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(136, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "RtgType"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "FromCountries"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "ToCountries"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(2, 249)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "ToCities"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 185)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "FromCities"
        '
        'txtCar
        '
        Me.txtCar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCar.Location = New System.Drawing.Point(84, 21)
        Me.txtCar.MaxLength = 2
        Me.txtCar.Name = "txtCar"
        Me.txtCar.Size = New System.Drawing.Size(43, 20)
        Me.txtCar.TabIndex = 6
        '
        'txtRtgType
        '
        Me.txtRtgType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRtgType.Location = New System.Drawing.Point(139, 21)
        Me.txtRtgType.MaxLength = 16
        Me.txtRtgType.Name = "txtRtgType"
        Me.txtRtgType.Size = New System.Drawing.Size(117, 20)
        Me.txtRtgType.TabIndex = 7
        '
        'txtFromCountries
        '
        Me.txtFromCountries.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFromCountries.Location = New System.Drawing.Point(83, 57)
        Me.txtFromCountries.Multiline = True
        Me.txtFromCountries.Name = "txtFromCountries"
        Me.txtFromCountries.Size = New System.Drawing.Size(789, 58)
        Me.txtFromCountries.TabIndex = 8
        Me.txtFromCountries.Text = "*"
        '
        'txtToCountries
        '
        Me.txtToCountries.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtToCountries.Location = New System.Drawing.Point(83, 121)
        Me.txtToCountries.Multiline = True
        Me.txtToCountries.Name = "txtToCountries"
        Me.txtToCountries.Size = New System.Drawing.Size(789, 58)
        Me.txtToCountries.TabIndex = 9
        Me.txtToCountries.Text = "*"
        '
        'txtFromCities
        '
        Me.txtFromCities.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFromCities.Location = New System.Drawing.Point(83, 185)
        Me.txtFromCities.Multiline = True
        Me.txtFromCities.Name = "txtFromCities"
        Me.txtFromCities.Size = New System.Drawing.Size(789, 58)
        Me.txtFromCities.TabIndex = 10
        Me.txtFromCities.Text = "*"
        '
        'txtToCities
        '
        Me.txtToCities.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtToCities.Location = New System.Drawing.Point(83, 249)
        Me.txtToCities.Multiline = True
        Me.txtToCities.Name = "txtToCities"
        Me.txtToCities.Size = New System.Drawing.Size(789, 58)
        Me.txtToCities.TabIndex = 11
        Me.txtToCities.Text = "*"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(3, 310)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 12
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'blkCancel
        '
        Me.blkCancel.AutoSize = True
        Me.blkCancel.Location = New System.Drawing.Point(80, 310)
        Me.blkCancel.Name = "blkCancel"
        Me.blkCancel.Size = New System.Drawing.Size(40, 13)
        Me.blkCancel.TabIndex = 13
        Me.blkCancel.TabStop = True
        Me.blkCancel.Text = "Cancel"
        '
        'txtRecId
        '
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(4, 21)
        Me.txtRecId.MaxLength = 20
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(73, 20)
        Me.txtRecId.TabIndex = 15
        Me.txtRecId.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(2, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "RecId"
        '
        'frmIncentiveRtgEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 328)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.blkCancel)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.txtToCities)
        Me.Controls.Add(Me.txtFromCities)
        Me.Controls.Add(Me.txtToCountries)
        Me.Controls.Add(Me.txtFromCountries)
        Me.Controls.Add(Me.txtRtgType)
        Me.Controls.Add(Me.txtCar)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmIncentiveRtgEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Incentive Routing Edit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtCar As TextBox
    Friend WithEvents txtRtgType As TextBox
    Friend WithEvents txtFromCountries As TextBox
    Friend WithEvents txtToCountries As TextBox
    Friend WithEvents txtFromCities As TextBox
    Friend WithEvents txtToCities As TextBox
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents blkCancel As LinkLabel
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents Label7 As Label
End Class
