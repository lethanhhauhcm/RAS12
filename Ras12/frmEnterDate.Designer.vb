<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEnterDate
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
        Me.dtp01 = New System.Windows.Forms.DateTimePicker()
        Me.lbl01 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'dtp01
        '
        Me.dtp01.Location = New System.Drawing.Point(12, 25)
        Me.dtp01.Name = "dtp01"
        Me.dtp01.Size = New System.Drawing.Size(200, 20)
        Me.dtp01.TabIndex = 0
        '
        'lbl01
        '
        Me.lbl01.AutoSize = True
        Me.lbl01.Location = New System.Drawing.Point(12, 9)
        Me.lbl01.Name = "lbl01"
        Me.lbl01.Size = New System.Drawing.Size(39, 13)
        Me.lbl01.TabIndex = 1
        Me.lbl01.Text = "Label1"
        '
        'frmEnterDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(221, 55)
        Me.Controls.Add(Me.lbl01)
        Me.Controls.Add(Me.dtp01)
        Me.Name = "frmEnterDate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmEnterDate"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dtp01 As DateTimePicker
    Friend WithEvents lbl01 As Label
End Class
