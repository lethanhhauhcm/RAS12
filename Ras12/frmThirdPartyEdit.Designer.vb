<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThirdPartyEdit
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
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboCAT = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.blkCancel = New System.Windows.Forms.LinkLabel()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBankBranch = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBankAccount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboPayType = New System.Windows.Forms.ComboBox()
        Me.lblPayType = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCounter = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'cboCustShortName
        '
        Me.cboCustShortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Items.AddRange(New Object() {"SEG", "TKT"})
        Me.cboCustShortName.Location = New System.Drawing.Point(444, 25)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(300, 21)
        Me.cboCustShortName.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(441, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "CustShortName"
        '
        'txtBankName
        '
        Me.txtBankName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankName.Location = New System.Drawing.Point(7, 70)
        Me.txtBankName.MaxLength = 16
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(202, 20)
        Me.txtBankName.TabIndex = 4
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(4, 53)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 13)
        Me.Label11.TabIndex = 62
        Me.Label11.Text = "BankName"
        '
        'cboCAT
        '
        Me.cboCAT.FormattingEnabled = True
        Me.cboCAT.Items.AddRange(New Object() {"KB", "MF"})
        Me.cboCAT.Location = New System.Drawing.Point(87, 25)
        Me.cboCAT.Name = "cboCAT"
        Me.cboCAT.Size = New System.Drawing.Size(40, 21)
        Me.cboCAT.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(84, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(28, 13)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "CAT"
        '
        'txtRecId
        '
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(8, 26)
        Me.txtRecId.MaxLength = 20
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(73, 20)
        Me.txtRecId.TabIndex = 0
        Me.txtRecId.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "RecId"
        '
        'blkCancel
        '
        Me.blkCancel.AutoSize = True
        Me.blkCancel.Location = New System.Drawing.Point(320, 139)
        Me.blkCancel.Name = "blkCancel"
        Me.blkCancel.Size = New System.Drawing.Size(40, 13)
        Me.blkCancel.TabIndex = 10
        Me.blkCancel.TabStop = True
        Me.blkCancel.Text = "Cancel"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(243, 139)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 9
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'txtEmail
        '
        Me.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEmail.Location = New System.Drawing.Point(236, 109)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(508, 20)
        Me.txtEmail.TabIndex = 8
        '
        'txtPhone
        '
        Me.txtPhone.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPhone.Location = New System.Drawing.Point(7, 109)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(202, 20)
        Me.txtPhone.TabIndex = 7
        '
        'txtFullName
        '
        Me.txtFullName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFullName.Location = New System.Drawing.Point(138, 27)
        Me.txtFullName.MaxLength = 64
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(300, 20)
        Me.txtFullName.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(233, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Email"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Phone"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(135, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "FullName"
        '
        'txtBankBranch
        '
        Me.txtBankBranch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankBranch.Location = New System.Drawing.Point(236, 70)
        Me.txtBankBranch.MaxLength = 16
        Me.txtBankBranch.Name = "txtBankBranch"
        Me.txtBankBranch.Size = New System.Drawing.Size(202, 20)
        Me.txtBankBranch.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(233, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 67
        Me.Label2.Text = "BankBranch"
        '
        'txtBankAccount
        '
        Me.txtBankAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankAccount.Location = New System.Drawing.Point(444, 70)
        Me.txtBankAccount.MaxLength = 16
        Me.txtBankAccount.Name = "txtBankAccount"
        Me.txtBankAccount.Size = New System.Drawing.Size(202, 20)
        Me.txtBankAccount.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(441, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 13)
        Me.Label6.TabIndex = 69
        Me.Label6.Text = "BankAccount"
        '
        'cboPayType
        '
        Me.cboPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPayType.FormattingEnabled = True
        Me.cboPayType.Items.AddRange(New Object() {"BANK", "CASH"})
        Me.cboPayType.Location = New System.Drawing.Point(751, 69)
        Me.cboPayType.Name = "cboPayType"
        Me.cboPayType.Size = New System.Drawing.Size(70, 21)
        Me.cboPayType.TabIndex = 70
        '
        'lblPayType
        '
        Me.lblPayType.AutoSize = True
        Me.lblPayType.Location = New System.Drawing.Point(748, 53)
        Me.lblPayType.Name = "lblPayType"
        Me.lblPayType.Size = New System.Drawing.Size(49, 13)
        Me.lblPayType.TabIndex = 71
        Me.lblPayType.Text = "PayType"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(750, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 13)
        Me.Label9.TabIndex = 73
        Me.Label9.Text = "Counter"
        '
        'txtCounter
        '
        Me.txtCounter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCounter.Location = New System.Drawing.Point(753, 27)
        Me.txtCounter.MaxLength = 16
        Me.txtCounter.Name = "txtCounter"
        Me.txtCounter.ReadOnly = True
        Me.txtCounter.Size = New System.Drawing.Size(60, 20)
        Me.txtCounter.TabIndex = 74
        '
        'frmThirdPartyEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(906, 161)
        Me.Controls.Add(Me.txtCounter)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboPayType)
        Me.Controls.Add(Me.lblPayType)
        Me.Controls.Add(Me.txtBankAccount)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtBankBranch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cboCAT)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.blkCancel)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtFullName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmThirdPartyEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ThirdPartyEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboCustShortName As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtBankName As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents cboCAT As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents blkCancel As LinkLabel
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtBankBranch As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtBankAccount As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cboPayType As ComboBox
    Friend WithEvents lblPayType As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txtCounter As TextBox
End Class
