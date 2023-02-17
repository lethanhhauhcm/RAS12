<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLinkMainSvcNonAir
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
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.lbkOK = New System.Windows.Forms.LinkLabel()
        Me.grdItems = New System.Windows.Forms.DataGridView()
        Me.lbkDeleteLink = New System.Windows.Forms.LinkLabel()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(379, 394)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 5
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'lbkOK
        '
        Me.lbkOK.AutoSize = True
        Me.lbkOK.Location = New System.Drawing.Point(298, 394)
        Me.lbkOK.Name = "lbkOK"
        Me.lbkOK.Size = New System.Drawing.Size(22, 13)
        Me.lbkOK.TabIndex = 4
        Me.lbkOK.TabStop = True
        Me.lbkOK.Text = "OK"
        '
        'grdItems
        '
        Me.grdItems.AllowUserToAddRows = False
        Me.grdItems.AllowUserToDeleteRows = False
        Me.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItems.Location = New System.Drawing.Point(12, 2)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.ReadOnly = True
        Me.grdItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdItems.Size = New System.Drawing.Size(776, 389)
        Me.grdItems.TabIndex = 3
        '
        'lbkDeleteLink
        '
        Me.lbkDeleteLink.AutoSize = True
        Me.lbkDeleteLink.Location = New System.Drawing.Point(469, 394)
        Me.lbkDeleteLink.Name = "lbkDeleteLink"
        Me.lbkDeleteLink.Size = New System.Drawing.Size(58, 13)
        Me.lbkDeleteLink.TabIndex = 6
        Me.lbkDeleteLink.TabStop = True
        Me.lbkDeleteLink.Text = "DeleteLink"
        '
        'frmLinkMainSvcNonAir
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.lbkDeleteLink)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.lbkOK)
        Me.Controls.Add(Me.grdItems)
        Me.Name = "frmLinkMainSvcNonAir"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmLinkMainSvcNonAir"
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents lbkOK As LinkLabel
    Friend WithEvents grdItems As DataGridView
    Friend WithEvents lbkDeleteLink As LinkLabel
End Class
