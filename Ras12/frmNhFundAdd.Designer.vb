<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNhFundAdd
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
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboValidTo = New System.Windows.Forms.ComboBox()
        Me.cboValidFrom = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(92, 173)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 2
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(130, 173)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 3
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Amount USD"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(150, 62)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(100, 20)
        Me.txtAmount.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Customer"
        '
        'cboCustomer
        '
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(90, 26)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(160, 21)
        Me.cboCustomer.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "ValidFrom/To"
        '
        'cboValidTo
        '
        Me.cboValidTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboValidTo.FormattingEnabled = True
        Me.cboValidTo.Location = New System.Drawing.Point(145, 120)
        Me.cboValidTo.Name = "cboValidTo"
        Me.cboValidTo.Size = New System.Drawing.Size(121, 21)
        Me.cboValidTo.TabIndex = 32
        '
        'cboValidFrom
        '
        Me.cboValidFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboValidFrom.FormattingEnabled = True
        Me.cboValidFrom.Location = New System.Drawing.Point(18, 120)
        Me.cboValidFrom.Name = "cboValidFrom"
        Me.cboValidFrom.Size = New System.Drawing.Size(121, 21)
        Me.cboValidFrom.TabIndex = 31
        '
        'frmNhFundAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.cboValidTo)
        Me.Controls.Add(Me.cboValidFrom)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbkSave)
        Me.Name = "frmNhFundAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NhFundAdd"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cboValidTo As ComboBox
    Friend WithEvents cboValidFrom As ComboBox
End Class
