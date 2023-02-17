<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectFile
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
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.lbkOK = New System.Windows.Forms.LinkLabel()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.txtBrief = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboVendor = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(432, 65)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 3
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'lbkOK
        '
        Me.lbkOK.AutoSize = True
        Me.lbkOK.Location = New System.Drawing.Point(404, 65)
        Me.lbkOK.Name = "lbkOK"
        Me.lbkOK.Size = New System.Drawing.Size(22, 13)
        Me.lbkOK.TabIndex = 2
        Me.lbkOK.TabStop = True
        Me.lbkOK.Text = "OK"
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(12, 38)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(460, 20)
        Me.txtFilePath.TabIndex = 4
        '
        'txtBrief
        '
        Me.txtBrief.Location = New System.Drawing.Point(66, 62)
        Me.txtBrief.MaxLength = 32
        Me.txtBrief.Name = "txtBrief"
        Me.txtBrief.Size = New System.Drawing.Size(332, 20)
        Me.txtBrief.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Brief"
        '
        'cboVendor
        '
        Me.cboVendor.FormattingEnabled = True
        Me.cboVendor.Location = New System.Drawing.Point(66, 12)
        Me.cboVendor.Name = "cboVendor"
        Me.cboVendor.Size = New System.Drawing.Size(332, 21)
        Me.cboVendor.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Vendor"
        '
        'frmSelectFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 161)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboVendor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBrief)
        Me.Controls.Add(Me.txtFilePath)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.lbkOK)
        Me.Name = "frmSelectFile"
        Me.Text = "StoreNonAirFiles"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents lbkOK As LinkLabel
    Friend WithEvents txtFilePath As TextBox
    Friend WithEvents txtBrief As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cboVendor As ComboBox
    Friend WithEvents Label2 As Label
End Class
