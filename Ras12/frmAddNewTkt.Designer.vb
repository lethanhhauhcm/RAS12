<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddNewTkt
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
        Me.dgrNewTicket = New System.Windows.Forms.DataGridView()
        Me.TKNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrNewTicket, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrNewTicket
        '
        Me.dgrNewTicket.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrNewTicket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrNewTicket.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TKNO})
        Me.dgrNewTicket.Location = New System.Drawing.Point(23, 29)
        Me.dgrNewTicket.Name = "dgrNewTicket"
        Me.dgrNewTicket.Size = New System.Drawing.Size(239, 203)
        Me.dgrNewTicket.TabIndex = 20
        '
        'TKNO
        '
        Me.TKNO.HeaderText = "TKNO"
        Me.TKNO.Name = "TKNO"
        Me.TKNO.Width = 62
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(157, 239)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 22
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(97, 239)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 21
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'frmAddNewTkt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.dgrNewTicket)
        Me.Name = "frmAddNewTkt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AddNewTkt"
        CType(Me.dgrNewTicket, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents TKNO As DataGridViewTextBoxColumn
    Friend WithEvents dgrNewTicket As DataGridView
End Class
