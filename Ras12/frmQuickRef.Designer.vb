<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuickRef
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
        Me.WebDisplay = New System.Windows.Forms.WebBrowser()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.lbkViewXls = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'WebDisplay
        '
        Me.WebDisplay.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WebDisplay.Location = New System.Drawing.Point(12, 42)
        Me.WebDisplay.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebDisplay.Name = "WebDisplay"
        Me.WebDisplay.Size = New System.Drawing.Size(757, 434)
        Me.WebDisplay.TabIndex = 14
        '
        'cboCategory
        '
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(12, 12)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(188, 21)
        Me.cboCategory.TabIndex = 15
        '
        'lbkViewXls
        '
        Me.lbkViewXls.AutoSize = True
        Me.lbkViewXls.Location = New System.Drawing.Point(224, 15)
        Me.lbkViewXls.Name = "lbkViewXls"
        Me.lbkViewXls.Size = New System.Drawing.Size(44, 13)
        Me.lbkViewXls.TabIndex = 16
        Me.lbkViewXls.TabStop = True
        Me.lbkViewXls.Text = "ViewXls"
        '
        'frmQuickRef
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 496)
        Me.Controls.Add(Me.lbkViewXls)
        Me.Controls.Add(Me.cboCategory)
        Me.Controls.Add(Me.WebDisplay)
        Me.Name = "frmQuickRef"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QuickRef"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents WebDisplay As System.Windows.Forms.WebBrowser
    Friend WithEvents cboCategory As System.Windows.Forms.ComboBox
    Friend WithEvents lbkViewXls As System.Windows.Forms.LinkLabel
End Class
