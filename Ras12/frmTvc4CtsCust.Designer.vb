<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTvc4CtsCust
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
        Me.cboTvc = New System.Windows.Forms.ComboBox()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.dgrCustomers = New System.Windows.Forms.DataGridView()
        Me.lbkSetNew = New System.Windows.Forms.LinkLabel()
        Me.cboNewTvc = New System.Windows.Forms.ComboBox()
        CType(Me.dgrCustomers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboTvc
        '
        Me.cboTvc.FormattingEnabled = True
        Me.cboTvc.Items.AddRange(New Object() {"TVTR", "GDS"})
        Me.cboTvc.Location = New System.Drawing.Point(2, 12)
        Me.cboTvc.Name = "cboTvc"
        Me.cboTvc.Size = New System.Drawing.Size(85, 21)
        Me.cboTvc.TabIndex = 0
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(108, 20)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 1
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(155, 20)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 2
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'dgrCustomers
        '
        Me.dgrCustomers.AllowUserToAddRows = False
        Me.dgrCustomers.AllowUserToDeleteRows = False
        Me.dgrCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrCustomers.Location = New System.Drawing.Point(2, 39)
        Me.dgrCustomers.Name = "dgrCustomers"
        Me.dgrCustomers.ReadOnly = True
        Me.dgrCustomers.Size = New System.Drawing.Size(470, 383)
        Me.dgrCustomers.TabIndex = 3
        '
        'lbkSetNew
        '
        Me.lbkSetNew.AutoSize = True
        Me.lbkSetNew.Location = New System.Drawing.Point(108, 436)
        Me.lbkSetNew.Name = "lbkSetNew"
        Me.lbkSetNew.Size = New System.Drawing.Size(45, 13)
        Me.lbkSetNew.TabIndex = 5
        Me.lbkSetNew.TabStop = True
        Me.lbkSetNew.Text = "SetNew"
        '
        'cboNewTvc
        '
        Me.cboNewTvc.FormattingEnabled = True
        Me.cboNewTvc.Items.AddRange(New Object() {"TVTR", "GDS"})
        Me.cboNewTvc.Location = New System.Drawing.Point(2, 428)
        Me.cboNewTvc.Name = "cboNewTvc"
        Me.cboNewTvc.Size = New System.Drawing.Size(85, 21)
        Me.cboNewTvc.TabIndex = 4
        '
        'frmTvc4CtsCust
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 461)
        Me.Controls.Add(Me.lbkSetNew)
        Me.Controls.Add(Me.cboNewTvc)
        Me.Controls.Add(Me.dgrCustomers)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.cboTvc)
        Me.Name = "frmTvc4CtsCust"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmTvc4CtsCust"
        CType(Me.dgrCustomers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboTvc As ComboBox
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents dgrCustomers As DataGridView
    Friend WithEvents lbkSetNew As LinkLabel
    Friend WithEvents cboNewTvc As ComboBox
End Class
