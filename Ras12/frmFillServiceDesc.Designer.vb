<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFillServiceDesc
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
        Me.lblF1 = New System.Windows.Forms.Label()
        Me.lblF2 = New System.Windows.Forms.Label()
        Me.lblF3 = New System.Windows.Forms.Label()
        Me.lblF4 = New System.Windows.Forms.Label()
        Me.cboF1 = New System.Windows.Forms.ComboBox()
        Me.cboF2 = New System.Windows.Forms.ComboBox()
        Me.cboF3 = New System.Windows.Forms.ComboBox()
        Me.cboF4 = New System.Windows.Forms.ComboBox()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'lblF1
        '
        Me.lblF1.AutoSize = True
        Me.lblF1.Location = New System.Drawing.Point(4, 39)
        Me.lblF1.Name = "lblF1"
        Me.lblF1.Size = New System.Drawing.Size(19, 13)
        Me.lblF1.TabIndex = 1
        Me.lblF1.Text = "F1"
        '
        'lblF2
        '
        Me.lblF2.AutoSize = True
        Me.lblF2.Location = New System.Drawing.Point(4, 66)
        Me.lblF2.Name = "lblF2"
        Me.lblF2.Size = New System.Drawing.Size(19, 13)
        Me.lblF2.TabIndex = 2
        Me.lblF2.Text = "F2"
        '
        'lblF3
        '
        Me.lblF3.AutoSize = True
        Me.lblF3.Location = New System.Drawing.Point(4, 93)
        Me.lblF3.Name = "lblF3"
        Me.lblF3.Size = New System.Drawing.Size(19, 13)
        Me.lblF3.TabIndex = 3
        Me.lblF3.Text = "F3"
        '
        'lblF4
        '
        Me.lblF4.AutoSize = True
        Me.lblF4.Location = New System.Drawing.Point(4, 120)
        Me.lblF4.Name = "lblF4"
        Me.lblF4.Size = New System.Drawing.Size(19, 13)
        Me.lblF4.TabIndex = 4
        Me.lblF4.Text = "F4"
        '
        'cboF1
        '
        Me.cboF1.FormattingEnabled = True
        Me.cboF1.Location = New System.Drawing.Point(76, 31)
        Me.cboF1.Name = "cboF1"
        Me.cboF1.Size = New System.Drawing.Size(206, 21)
        Me.cboF1.TabIndex = 6
        '
        'cboF2
        '
        Me.cboF2.FormattingEnabled = True
        Me.cboF2.Location = New System.Drawing.Point(76, 58)
        Me.cboF2.Name = "cboF2"
        Me.cboF2.Size = New System.Drawing.Size(206, 21)
        Me.cboF2.TabIndex = 7
        '
        'cboF3
        '
        Me.cboF3.FormattingEnabled = True
        Me.cboF3.Location = New System.Drawing.Point(76, 85)
        Me.cboF3.Name = "cboF3"
        Me.cboF3.Size = New System.Drawing.Size(206, 21)
        Me.cboF3.TabIndex = 8
        '
        'cboF4
        '
        Me.cboF4.FormattingEnabled = True
        Me.cboF4.Location = New System.Drawing.Point(76, 112)
        Me.cboF4.Name = "cboF4"
        Me.cboF4.Size = New System.Drawing.Size(206, 21)
        Me.cboF4.TabIndex = 9
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(4, 151)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 10
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'frmFillServiceDesc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.cboF4)
        Me.Controls.Add(Me.cboF3)
        Me.Controls.Add(Me.cboF2)
        Me.Controls.Add(Me.cboF1)
        Me.Controls.Add(Me.lblF4)
        Me.Controls.Add(Me.lblF3)
        Me.Controls.Add(Me.lblF2)
        Me.Controls.Add(Me.lblF1)
        Me.Name = "frmFillServiceDesc"
        Me.Text = "Fill Service Desc"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblF1 As Label
    Friend WithEvents lblF2 As Label
    Friend WithEvents lblF3 As Label
    Friend WithEvents lblF4 As Label
    Friend WithEvents cboF1 As ComboBox
    Friend WithEvents cboF2 As ComboBox
    Friend WithEvents cboF3 As ComboBox
    Friend WithEvents cboF4 As ComboBox
    Friend WithEvents lbkSave As LinkLabel
End Class
