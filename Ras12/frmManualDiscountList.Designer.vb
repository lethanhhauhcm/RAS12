<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManualDiscountList
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
        Me.dgrManualDiscount = New System.Windows.Forms.DataGridView()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkChangeExpiry = New System.Windows.Forms.LinkLabel()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        Me.lbkClone = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrManualDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrManualDiscount
        '
        Me.dgrManualDiscount.AllowUserToAddRows = False
        Me.dgrManualDiscount.AllowUserToDeleteRows = False
        Me.dgrManualDiscount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrManualDiscount.Location = New System.Drawing.Point(-1, 34)
        Me.dgrManualDiscount.Name = "dgrManualDiscount"
        Me.dgrManualDiscount.ReadOnly = True
        Me.dgrManualDiscount.Size = New System.Drawing.Size(881, 193)
        Me.dgrManualDiscount.TabIndex = 0
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(12, 239)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 1
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkChangeExpiry
        '
        Me.lbkChangeExpiry.AutoSize = True
        Me.lbkChangeExpiry.Location = New System.Drawing.Point(106, 239)
        Me.lbkChangeExpiry.Name = "lbkChangeExpiry"
        Me.lbkChangeExpiry.Size = New System.Drawing.Size(72, 13)
        Me.lbkChangeExpiry.TabIndex = 2
        Me.lbkChangeExpiry.TabStop = True
        Me.lbkChangeExpiry.Text = "ChangeExpiry"
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(184, 239)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 3
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'lbkClone
        '
        Me.lbkClone.AutoSize = True
        Me.lbkClone.Location = New System.Drawing.Point(57, 239)
        Me.lbkClone.Name = "lbkClone"
        Me.lbkClone.Size = New System.Drawing.Size(34, 13)
        Me.lbkClone.TabIndex = 4
        Me.lbkClone.TabStop = True
        Me.lbkClone.Text = "Clone"
        '
        'frmManualDiscountList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 261)
        Me.Controls.Add(Me.lbkClone)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.lbkChangeExpiry)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.dgrManualDiscount)
        Me.Name = "frmManualDiscountList"
        Me.Text = "ManualDiscountList"
        CType(Me.dgrManualDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrManualDiscount As DataGridView
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkChangeExpiry As LinkLabel
    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents lbkClone As LinkLabel
End Class
