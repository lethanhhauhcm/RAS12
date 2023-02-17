<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSelectMultiRows
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
        Me.cboSearchField = New System.Windows.Forms.ComboBox()
        Me.lblSearchField = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgrInput = New System.Windows.Forms.DataGridView()
        Me.Selected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboSearchField
        '
        Me.cboSearchField.FormattingEnabled = True
        Me.cboSearchField.Location = New System.Drawing.Point(36, -23)
        Me.cboSearchField.Name = "cboSearchField"
        Me.cboSearchField.Size = New System.Drawing.Size(121, 21)
        Me.cboSearchField.TabIndex = 8
        '
        'lblSearchField
        '
        Me.lblSearchField.AutoSize = True
        Me.lblSearchField.Location = New System.Drawing.Point(-56, -15)
        Me.lblSearchField.Name = "lblSearchField"
        Me.lblSearchField.Size = New System.Drawing.Size(63, 13)
        Me.lblSearchField.TabIndex = 7
        Me.lblSearchField.Text = "SearchField"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(107, 5)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "SearchField"
        '
        'dgrInput
        '
        Me.dgrInput.AllowUserToAddRows = False
        Me.dgrInput.AllowUserToDeleteRows = False
        Me.dgrInput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInput.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Selected})
        Me.dgrInput.Location = New System.Drawing.Point(12, 32)
        Me.dgrInput.Name = "dgrInput"
        Me.dgrInput.Size = New System.Drawing.Size(694, 560)
        Me.dgrInput.TabIndex = 9
        '
        'Selected
        '
        Me.Selected.HeaderText = "Selected"
        Me.Selected.Name = "Selected"
        Me.Selected.Width = 55
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(16, 615)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 13
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        '
        'frmSelectMultiRows
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 661)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboSearchField)
        Me.Controls.Add(Me.lblSearchField)
        Me.Controls.Add(Me.dgrInput)
        Me.Name = "frmSelectMultiRows"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SelectMultiRows"
        CType(Me.dgrInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboSearchField As ComboBox
    Friend WithEvents lblSearchField As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dgrInput As DataGridView
    Friend WithEvents Selected As DataGridViewCheckBoxColumn
    Friend WithEvents lbkSelect As LinkLabel
End Class
