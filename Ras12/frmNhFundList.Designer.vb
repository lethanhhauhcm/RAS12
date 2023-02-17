<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNhFundList
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
        Me.cboFilter = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkReset = New System.Windows.Forms.LinkLabel()
        Me.dgrFund = New System.Windows.Forms.DataGridView()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkExpire = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrFund, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboCustomer
        '
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(-1, 24)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(160, 21)
        Me.cboCustomer.TabIndex = 0
        '
        'cboFilter
        '
        Me.cboFilter.FormattingEnabled = True
        Me.cboFilter.Items.AddRange(New Object() {"Valid", "NotValid"})
        Me.cboFilter.Location = New System.Drawing.Point(165, 24)
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(70, 21)
        Me.cboFilter.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-4, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Customer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(162, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Filter"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(448, 27)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 4
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkReset
        '
        Me.lbkReset.AutoSize = True
        Me.lbkReset.Location = New System.Drawing.Point(513, 27)
        Me.lbkReset.Name = "lbkReset"
        Me.lbkReset.Size = New System.Drawing.Size(35, 13)
        Me.lbkReset.TabIndex = 5
        Me.lbkReset.TabStop = True
        Me.lbkReset.Text = "Reset"
        '
        'dgrFund
        '
        Me.dgrFund.AllowUserToAddRows = False
        Me.dgrFund.AllowUserToDeleteRows = False
        Me.dgrFund.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrFund.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrFund.Location = New System.Drawing.Point(-1, 51)
        Me.dgrFund.Name = "dgrFund"
        Me.dgrFund.ReadOnly = True
        Me.dgrFund.Size = New System.Drawing.Size(973, 467)
        Me.dgrFund.TabIndex = 6
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(6, 539)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 7
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkExpire
        '
        Me.lbkExpire.AutoSize = True
        Me.lbkExpire.Location = New System.Drawing.Point(53, 539)
        Me.lbkExpire.Name = "lbkExpire"
        Me.lbkExpire.Size = New System.Drawing.Size(36, 13)
        Me.lbkExpire.TabIndex = 8
        Me.lbkExpire.TabStop = True
        Me.lbkExpire.Text = "Expire"
        Me.lbkExpire.Visible = False
        '
        'frmNhFundList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 561)
        Me.Controls.Add(Me.lbkExpire)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.dgrFund)
        Me.Controls.Add(Me.lbkReset)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboFilter)
        Me.Controls.Add(Me.cboCustomer)
        Me.Name = "frmNhFundList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NhFundList"
        CType(Me.dgrFund, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents cboFilter As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkReset As LinkLabel
    Friend WithEvents dgrFund As DataGridView
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkExpire As LinkLabel
End Class
