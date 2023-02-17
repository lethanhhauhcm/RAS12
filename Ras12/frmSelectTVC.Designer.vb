<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectTVC
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
        Me.cboTVC = New System.Windows.Forms.ComboBox()
        Me.lblSearchField = New System.Windows.Forms.Label()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        Me.dgrInput = New System.Windows.Forms.DataGridView()
        CType(Me.dgrInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboTVC
        '
        Me.cboTVC.FormattingEnabled = True
        Me.cboTVC.Location = New System.Drawing.Point(1, 24)
        Me.cboTVC.Name = "cboTVC"
        Me.cboTVC.Size = New System.Drawing.Size(121, 21)
        Me.cboTVC.TabIndex = 8
        '
        'lblSearchField
        '
        Me.lblSearchField.AutoSize = True
        Me.lblSearchField.Location = New System.Drawing.Point(-2, 8)
        Me.lblSearchField.Name = "lblSearchField"
        Me.lblSearchField.Size = New System.Drawing.Size(28, 13)
        Me.lblSearchField.TabIndex = 7
        Me.lblSearchField.Text = "TVC"
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(12, 515)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 6
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        '
        'dgrInput
        '
        Me.dgrInput.AllowUserToAddRows = False
        Me.dgrInput.AllowUserToDeleteRows = False
        Me.dgrInput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInput.Location = New System.Drawing.Point(1, 51)
        Me.dgrInput.Name = "dgrInput"
        Me.dgrInput.ReadOnly = True
        Me.dgrInput.Size = New System.Drawing.Size(694, 461)
        Me.dgrInput.TabIndex = 5
        '
        'frmSelectTVC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 561)
        Me.Controls.Add(Me.cboTVC)
        Me.Controls.Add(Me.lblSearchField)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.dgrInput)
        Me.Name = "frmSelectTVC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select TransViet Company, MauSo, Ky Hieu"
        CType(Me.dgrInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboTVC As ComboBox
    Friend WithEvents lblSearchField As Label
    Friend WithEvents lbkSelect As LinkLabel
    Friend WithEvents dgrInput As DataGridView
End Class
