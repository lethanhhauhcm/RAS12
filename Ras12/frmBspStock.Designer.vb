<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBspStock
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
        Me.lblVal = New System.Windows.Forms.Label()
        Me.txtVal = New System.Windows.Forms.TextBox()
        Me.llbAdd = New System.Windows.Forms.LinkLabel()
        Me.dgvMain = New System.Windows.Forms.DataGridView()
        Me.llbDelete = New System.Windows.Forms.LinkLabel()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblVal
        '
        Me.lblVal.AutoSize = True
        Me.lblVal.Location = New System.Drawing.Point(12, 16)
        Me.lblVal.Name = "lblVal"
        Me.lblVal.Size = New System.Drawing.Size(27, 13)
        Me.lblVal.TabIndex = 0
        Me.lblVal.Text = "VAL"
        '
        'txtVal
        '
        Me.txtVal.Location = New System.Drawing.Point(45, 12)
        Me.txtVal.MaxLength = 4
        Me.txtVal.Name = "txtVal"
        Me.txtVal.Size = New System.Drawing.Size(40, 20)
        Me.txtVal.TabIndex = 1
        '
        'llbAdd
        '
        Me.llbAdd.AutoSize = True
        Me.llbAdd.Enabled = False
        Me.llbAdd.Location = New System.Drawing.Point(91, 16)
        Me.llbAdd.Name = "llbAdd"
        Me.llbAdd.Size = New System.Drawing.Size(26, 13)
        Me.llbAdd.TabIndex = 1
        Me.llbAdd.TabStop = True
        Me.llbAdd.Text = "Add"
        '
        'dgvMain
        '
        Me.dgvMain.AllowUserToAddRows = False
        Me.dgvMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMain.Location = New System.Drawing.Point(12, 38)
        Me.dgvMain.Name = "dgvMain"
        Me.dgvMain.ReadOnly = True
        Me.dgvMain.Size = New System.Drawing.Size(776, 400)
        Me.dgvMain.TabIndex = 3
        '
        'llbDelete
        '
        Me.llbDelete.AutoSize = True
        Me.llbDelete.Enabled = False
        Me.llbDelete.Location = New System.Drawing.Point(168, 16)
        Me.llbDelete.Name = "llbDelete"
        Me.llbDelete.Size = New System.Drawing.Size(38, 13)
        Me.llbDelete.TabIndex = 2
        Me.llbDelete.TabStop = True
        Me.llbDelete.Text = "Delete"
        '
        'frmBspStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.llbDelete)
        Me.Controls.Add(Me.dgvMain)
        Me.Controls.Add(Me.llbAdd)
        Me.Controls.Add(Me.txtVal)
        Me.Controls.Add(Me.lblVal)
        Me.Name = "frmBspStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BspStock"
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblVal As Label
    Friend WithEvents txtVal As TextBox
    Friend WithEvents llbAdd As LinkLabel
    Friend WithEvents dgvMain As DataGridView
    Friend WithEvents llbDelete As LinkLabel
End Class
