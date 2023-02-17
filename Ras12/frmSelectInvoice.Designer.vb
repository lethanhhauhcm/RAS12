<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectInvoice
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
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        Me.dgrE_Invoices = New System.Windows.Forms.DataGridView()
        Me.lbkViewInv = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrE_Invoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(9, 339)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 3
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        '
        'dgrE_Invoices
        '
        Me.dgrE_Invoices.AllowUserToAddRows = False
        Me.dgrE_Invoices.AllowUserToDeleteRows = False
        Me.dgrE_Invoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrE_Invoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrE_Invoices.Location = New System.Drawing.Point(12, 11)
        Me.dgrE_Invoices.MultiSelect = False
        Me.dgrE_Invoices.Name = "dgrE_Invoices"
        Me.dgrE_Invoices.ReadOnly = True
        Me.dgrE_Invoices.Size = New System.Drawing.Size(760, 325)
        Me.dgrE_Invoices.TabIndex = 2
        '
        'lbkViewInv
        '
        Me.lbkViewInv.AutoSize = True
        Me.lbkViewInv.Location = New System.Drawing.Point(110, 339)
        Me.lbkViewInv.Name = "lbkViewInv"
        Me.lbkViewInv.Size = New System.Drawing.Size(68, 13)
        Me.lbkViewInv.TabIndex = 4
        Me.lbkViewInv.TabStop = True
        Me.lbkViewInv.Text = "View Invoice"
        '
        'frmSelectInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 361)
        Me.Controls.Add(Me.lbkViewInv)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.dgrE_Invoices)
        Me.Name = "frmSelectInvoice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Invoice"
        CType(Me.dgrE_Invoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkSelect As LinkLabel
    Friend WithEvents dgrE_Invoices As DataGridView
    Friend WithEvents lbkViewInv As LinkLabel
End Class
