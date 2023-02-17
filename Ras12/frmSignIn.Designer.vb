<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSignIn
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
        Me.txtStaffId = New System.Windows.Forms.TextBox()
        Me.txtLogInPSW = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmdLogInCancel = New System.Windows.Forms.Button()
        Me.CmdLogInOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Staff Id"
        '
        'txtStaffId
        '
        Me.txtStaffId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStaffId.Location = New System.Drawing.Point(54, 12)
        Me.txtStaffId.MaxLength = 4
        Me.txtStaffId.Name = "txtStaffId"
        Me.txtStaffId.Size = New System.Drawing.Size(56, 20)
        Me.txtStaffId.TabIndex = 10
        '
        'txtLogInPSW
        '
        Me.txtLogInPSW.Location = New System.Drawing.Point(195, 12)
        Me.txtLogInPSW.Name = "txtLogInPSW"
        Me.txtLogInPSW.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtLogInPSW.Size = New System.Drawing.Size(73, 20)
        Me.txtLogInPSW.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(140, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Password"
        '
        'CmdLogInCancel
        '
        Me.CmdLogInCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CmdLogInCancel.Location = New System.Drawing.Point(140, 82)
        Me.CmdLogInCancel.Name = "CmdLogInCancel"
        Me.CmdLogInCancel.Size = New System.Drawing.Size(73, 22)
        Me.CmdLogInCancel.TabIndex = 13
        Me.CmdLogInCancel.Text = "Cancel"
        Me.CmdLogInCancel.UseVisualStyleBackColor = True
        '
        'CmdLogInOK
        '
        Me.CmdLogInOK.Location = New System.Drawing.Point(64, 82)
        Me.CmdLogInOK.Name = "CmdLogInOK"
        Me.CmdLogInOK.Size = New System.Drawing.Size(72, 22)
        Me.CmdLogInOK.TabIndex = 14
        Me.CmdLogInOK.Text = "OK"
        Me.CmdLogInOK.UseVisualStyleBackColor = True
        '
        'frmSignIn
        '
        Me.AcceptButton = Me.CmdLogInOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 122)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtStaffId)
        Me.Controls.Add(Me.txtLogInPSW)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CmdLogInCancel)
        Me.Controls.Add(Me.CmdLogInOK)
        Me.Name = "frmSignIn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SignIn"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents txtStaffId As TextBox
    Friend WithEvents txtLogInPSW As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CmdLogInCancel As Button
    Friend WithEvents CmdLogInOK As Button
End Class
