<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowTableContent
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
        Me.dgrInput = New System.Windows.Forms.DataGridView()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        Me.lblSearchField = New System.Windows.Forms.Label()
        Me.cboSearchField = New System.Windows.Forms.ComboBox()
        CType(Me.dgrInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrInput
        '
        Me.dgrInput.AllowUserToAddRows = False
        Me.dgrInput.AllowUserToDeleteRows = False
        Me.dgrInput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInput.Location = New System.Drawing.Point(0, 37)
        Me.dgrInput.Name = "dgrInput"
        Me.dgrInput.ReadOnly = True
        Me.dgrInput.Size = New System.Drawing.Size(694, 549)
        Me.dgrInput.TabIndex = 0
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(12, 600)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 1
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        '
        'lblSearchField
        '
        Me.lblSearchField.AutoSize = True
        Me.lblSearchField.Location = New System.Drawing.Point(-3, 14)
        Me.lblSearchField.Name = "lblSearchField"
        Me.lblSearchField.Size = New System.Drawing.Size(63, 13)
        Me.lblSearchField.TabIndex = 2
        Me.lblSearchField.Text = "SearchField"
        '
        'cboSearchField
        '
        Me.cboSearchField.FormattingEnabled = True
        Me.cboSearchField.Location = New System.Drawing.Point(89, 6)
        Me.cboSearchField.Name = "cboSearchField"
        Me.cboSearchField.Size = New System.Drawing.Size(256, 21)
        Me.cboSearchField.TabIndex = 4
        '
        'frmShowTableContent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 623)
        Me.Controls.Add(Me.cboSearchField)
        Me.Controls.Add(Me.lblSearchField)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.dgrInput)
        Me.Name = "frmShowTableContent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ShowTableContent"
        CType(Me.dgrInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgrInput As System.Windows.Forms.DataGridView
    Friend WithEvents lbkSelect As LinkLabel
    Friend WithEvents lblSearchField As Label
    Friend WithEvents cboSearchField As ComboBox
End Class
