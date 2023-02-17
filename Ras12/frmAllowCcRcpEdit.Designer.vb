<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAllowCcRcpEdit
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
        Me.txtRcpNo = New System.Windows.Forms.TextBox()
        Me.lbkAdd = New System.Windows.Forms.LinkLabel()
        Me.dgrRcp = New System.Windows.Forms.DataGridView()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrRcp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtRcpNo
        '
        Me.txtRcpNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRcpNo.Location = New System.Drawing.Point(12, 12)
        Me.txtRcpNo.Name = "txtRcpNo"
        Me.txtRcpNo.Size = New System.Drawing.Size(167, 20)
        Me.txtRcpNo.TabIndex = 0
        '
        'lbkAdd
        '
        Me.lbkAdd.AutoSize = True
        Me.lbkAdd.Location = New System.Drawing.Point(185, 15)
        Me.lbkAdd.Name = "lbkAdd"
        Me.lbkAdd.Size = New System.Drawing.Size(26, 13)
        Me.lbkAdd.TabIndex = 1
        Me.lbkAdd.TabStop = True
        Me.lbkAdd.Text = "Add"
        '
        'dgrRcp
        '
        Me.dgrRcp.AllowUserToAddRows = False
        Me.dgrRcp.AllowUserToDeleteRows = False
        Me.dgrRcp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrRcp.Location = New System.Drawing.Point(12, 38)
        Me.dgrRcp.Name = "dgrRcp"
        Me.dgrRcp.ReadOnly = True
        Me.dgrRcp.Size = New System.Drawing.Size(760, 488)
        Me.dgrRcp.TabIndex = 2
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(12, 539)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 3
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'frmAllowCcRcpEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.dgrRcp)
        Me.Controls.Add(Me.lbkAdd)
        Me.Controls.Add(Me.txtRcpNo)
        Me.Name = "frmAllowCcRcpEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Allow Edit for Receipt paid by Credit Card"
        CType(Me.dgrRcp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtRcpNo As TextBox
    Friend WithEvents lbkAdd As LinkLabel
    Friend WithEvents dgrRcp As DataGridView
    Friend WithEvents lbkDelete As LinkLabel
End Class
