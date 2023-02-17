<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCustomerEdit
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCustShortName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCustFullName = New System.Windows.Forms.TextBox()
        Me.txtInvoiceEmail = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TxtPhone = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCustAddress = New System.Windows.Forms.TextBox()
        Me.txtCustTaxCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Short Name"
        '
        'txtCustShortName
        '
        Me.txtCustShortName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustShortName.Location = New System.Drawing.Point(81, 12)
        Me.txtCustShortName.MaxLength = 20
        Me.txtCustShortName.Name = "txtCustShortName"
        Me.txtCustShortName.Size = New System.Drawing.Size(124, 20)
        Me.txtCustShortName.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Full Name"
        '
        'txtCustFullName
        '
        Me.txtCustFullName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustFullName.Location = New System.Drawing.Point(81, 38)
        Me.txtCustFullName.Name = "txtCustFullName"
        Me.txtCustFullName.Size = New System.Drawing.Size(297, 20)
        Me.txtCustFullName.TabIndex = 1
        '
        'txtInvoiceEmail
        '
        Me.txtInvoiceEmail.ForeColor = System.Drawing.SystemColors.ScrollBar
        Me.txtInvoiceEmail.Location = New System.Drawing.Point(81, 116)
        Me.txtInvoiceEmail.MaxLength = 128
        Me.txtInvoiceEmail.Name = "txtInvoiceEmail"
        Me.txtInvoiceEmail.Size = New System.Drawing.Size(297, 20)
        Me.txtInvoiceEmail.TabIndex = 5
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 119)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(67, 13)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "InvoiceEmail"
        '
        'TxtPhone
        '
        Me.TxtPhone.Location = New System.Drawing.Point(238, 64)
        Me.TxtPhone.MaxLength = 11
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(140, 20)
        Me.TxtPhone.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(172, 67)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Phone Nbr."
        '
        'txtCustAddress
        '
        Me.txtCustAddress.Location = New System.Drawing.Point(81, 142)
        Me.txtCustAddress.Name = "txtCustAddress"
        Me.txtCustAddress.Size = New System.Drawing.Size(504, 20)
        Me.txtCustAddress.TabIndex = 6
        '
        'txtCustTaxCode
        '
        Me.txtCustTaxCode.Location = New System.Drawing.Point(81, 64)
        Me.txtCustTaxCode.Name = "txtCustTaxCode"
        Me.txtCustTaxCode.Size = New System.Drawing.Size(85, 20)
        Me.txtCustTaxCode.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "TaxCode"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(12, 190)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 7
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(115, 190)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 8
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Address"
        '
        'txtRecId
        '
        Me.txtRecId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(322, 12)
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(56, 20)
        Me.txtRecId.TabIndex = 67
        Me.txtRecId.Text = "0"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(256, 15)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(36, 13)
        Me.Label19.TabIndex = 68
        Me.Label19.Text = "RecId"
        '
        'txtEmail
        '
        Me.txtEmail.ForeColor = System.Drawing.SystemColors.ScrollBar
        Me.txtEmail.Location = New System.Drawing.Point(81, 90)
        Me.txtEmail.MaxLength = 128
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(297, 20)
        Me.txtEmail.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "Email"
        '
        'frmCustomerEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 261)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.txtInvoiceEmail)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TxtPhone)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtCustAddress)
        Me.Controls.Add(Me.txtCustTaxCode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCustFullName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCustShortName)
        Me.Name = "frmCustomerEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CustomerEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents txtCustShortName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtCustFullName As TextBox
    Friend WithEvents txtInvoiceEmail As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TxtPhone As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtCustAddress As TextBox
    Friend WithEvents txtCustTaxCode As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents Label4 As Label
End Class
