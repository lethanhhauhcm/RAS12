<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowTextBoxes
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
        Me.flpText = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'flpText
        '
        Me.flpText.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpText.Location = New System.Drawing.Point(0, 0)
        Me.flpText.Name = "flpText"
        Me.flpText.Size = New System.Drawing.Size(900, 249)
        Me.flpText.TabIndex = 0
        '
        'frmShowTextBoxes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 261)
        Me.Controls.Add(Me.flpText)
        Me.Name = "frmShowTextBoxes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmShowTextBoxes"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents flpText As FlowLayoutPanel
End Class
