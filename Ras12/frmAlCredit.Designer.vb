<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlCredit
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
        Me.lbkExpire = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.dgrFund = New System.Windows.Forms.DataGridView()
        Me.lbkReset = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboFilter = New System.Windows.Forms.ComboBox()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboAL = New System.Windows.Forms.ComboBox()
        CType(Me.dgrFund, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkExpire
        '
        Me.lbkExpire.AutoSize = True
        Me.lbkExpire.Location = New System.Drawing.Point(61, 539)
        Me.lbkExpire.Name = "lbkExpire"
        Me.lbkExpire.Size = New System.Drawing.Size(36, 13)
        Me.lbkExpire.TabIndex = 17
        Me.lbkExpire.TabStop = True
        Me.lbkExpire.Text = "Expire"
        Me.lbkExpire.Visible = False
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(14, 539)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 16
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'dgrFund
        '
        Me.dgrFund.AllowUserToAddRows = False
        Me.dgrFund.AllowUserToDeleteRows = False
        Me.dgrFund.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrFund.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrFund.Location = New System.Drawing.Point(7, 51)
        Me.dgrFund.Name = "dgrFund"
        Me.dgrFund.ReadOnly = True
        Me.dgrFund.Size = New System.Drawing.Size(973, 467)
        Me.dgrFund.TabIndex = 15
        '
        'lbkReset
        '
        Me.lbkReset.AutoSize = True
        Me.lbkReset.Location = New System.Drawing.Point(521, 27)
        Me.lbkReset.Name = "lbkReset"
        Me.lbkReset.Size = New System.Drawing.Size(35, 13)
        Me.lbkReset.TabIndex = 14
        Me.lbkReset.TabStop = True
        Me.lbkReset.Text = "Reset"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(456, 27)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 13
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(227, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Filter"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(61, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Customer"
        '
        'cboFilter
        '
        Me.cboFilter.FormattingEnabled = True
        Me.cboFilter.Items.AddRange(New Object() {"Valid", "NotValid"})
        Me.cboFilter.Location = New System.Drawing.Point(230, 24)
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(70, 21)
        Me.cboFilter.TabIndex = 10
        '
        'cboCustomer
        '
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(64, 24)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(160, 21)
        Me.cboCustomer.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "AL"
        '
        'cboAL
        '
        Me.cboAL.FormattingEnabled = True
        Me.cboAL.Items.AddRange(New Object() {"BL", "VJ"})
        Me.cboAL.Location = New System.Drawing.Point(7, 24)
        Me.cboAL.Name = "cboAL"
        Me.cboAL.Size = New System.Drawing.Size(49, 21)
        Me.cboAL.TabIndex = 18
        '
        'frmAlCredit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 561)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboAL)
        Me.Controls.Add(Me.lbkExpire)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.dgrFund)
        Me.Controls.Add(Me.lbkReset)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboFilter)
        Me.Controls.Add(Me.cboCustomer)
        Me.Name = "frmAlCredit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AL Credit List"
        CType(Me.dgrFund, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkExpire As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents dgrFund As DataGridView
    Friend WithEvents lbkReset As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cboFilter As ComboBox
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cboAL As ComboBox
End Class
