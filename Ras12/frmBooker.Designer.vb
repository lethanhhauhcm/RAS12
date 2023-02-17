<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBooker
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
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.dgrBooker = New System.Windows.Forms.DataGridView()
        Me.txtBooker = New System.Windows.Forms.TextBox()
        Me.lbkAdd = New System.Windows.Forms.LinkLabel()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        Me.lbkExpire = New System.Windows.Forms.LinkLabel()
        Me.cboCustType = New System.Windows.Forms.ComboBox()
        CType(Me.dgrBooker, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(84, 3)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(198, 21)
        Me.cboCustomer.TabIndex = 0
        '
        'dgrBooker
        '
        Me.dgrBooker.AllowUserToAddRows = False
        Me.dgrBooker.AllowUserToDeleteRows = False
        Me.dgrBooker.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrBooker.Location = New System.Drawing.Point(3, 30)
        Me.dgrBooker.Name = "dgrBooker"
        Me.dgrBooker.ReadOnly = True
        Me.dgrBooker.Size = New System.Drawing.Size(279, 150)
        Me.dgrBooker.TabIndex = 1
        '
        'txtBooker
        '
        Me.txtBooker.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBooker.Location = New System.Drawing.Point(3, 186)
        Me.txtBooker.Name = "txtBooker"
        Me.txtBooker.Size = New System.Drawing.Size(269, 20)
        Me.txtBooker.TabIndex = 2
        '
        'lbkAdd
        '
        Me.lbkAdd.AutoSize = True
        Me.lbkAdd.Location = New System.Drawing.Point(0, 209)
        Me.lbkAdd.Name = "lbkAdd"
        Me.lbkAdd.Size = New System.Drawing.Size(26, 13)
        Me.lbkAdd.TabIndex = 3
        Me.lbkAdd.TabStop = True
        Me.lbkAdd.Text = "Add"
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(32, 209)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 4
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'lbkExpire
        '
        Me.lbkExpire.AutoSize = True
        Me.lbkExpire.Location = New System.Drawing.Point(76, 209)
        Me.lbkExpire.Name = "lbkExpire"
        Me.lbkExpire.Size = New System.Drawing.Size(36, 13)
        Me.lbkExpire.TabIndex = 5
        Me.lbkExpire.TabStop = True
        Me.lbkExpire.Text = "Expire"
        '
        'cboCustType
        '
        Me.cboCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustType.FormattingEnabled = True
        Me.cboCustType.Items.AddRange(New Object() {"CA", "TO"})
        Me.cboCustType.Location = New System.Drawing.Point(3, 3)
        Me.cboCustType.Name = "cboCustType"
        Me.cboCustType.Size = New System.Drawing.Size(75, 21)
        Me.cboCustType.TabIndex = 6
        '
        'frmBooker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.cboCustType)
        Me.Controls.Add(Me.lbkExpire)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.lbkAdd)
        Me.Controls.Add(Me.txtBooker)
        Me.Controls.Add(Me.dgrBooker)
        Me.Controls.Add(Me.cboCustomer)
        Me.Name = "frmBooker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Booker"
        CType(Me.dgrBooker, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents dgrBooker As DataGridView
    Friend WithEvents txtBooker As TextBox
    Friend WithEvents lbkAdd As LinkLabel
    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents lbkExpire As LinkLabel
    Friend WithEvents cboCustType As ComboBox
End Class
