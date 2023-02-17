<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAcrList
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboAL = New System.Windows.Forms.ComboBox()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.dgrFund = New System.Windows.Forms.DataGridView()
        Me.lbkReset = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboAccountName = New System.Windows.Forms.ComboBox()
        CType(Me.dgrFund, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "AL"
        '
        'cboAL
        '
        Me.cboAL.FormattingEnabled = True
        Me.cboAL.Items.AddRange(New Object() {"VJ"})
        Me.cboAL.Location = New System.Drawing.Point(7, 24)
        Me.cboAL.Name = "cboAL"
        Me.cboAL.Size = New System.Drawing.Size(49, 21)
        Me.cboAL.TabIndex = 29
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(14, 539)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 27
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
        Me.dgrFund.TabIndex = 26
        '
        'lbkReset
        '
        Me.lbkReset.AutoSize = True
        Me.lbkReset.Location = New System.Drawing.Point(521, 27)
        Me.lbkReset.Name = "lbkReset"
        Me.lbkReset.Size = New System.Drawing.Size(35, 13)
        Me.lbkReset.TabIndex = 25
        Me.lbkReset.TabStop = True
        Me.lbkReset.Text = "Reset"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(456, 27)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 24
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(61, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "AccountName"
        '
        'cboAccountName
        '
        Me.cboAccountName.FormattingEnabled = True
        Me.cboAccountName.Location = New System.Drawing.Point(64, 24)
        Me.cboAccountName.Name = "cboAccountName"
        Me.cboAccountName.Size = New System.Drawing.Size(160, 21)
        Me.cboAccountName.TabIndex = 20
        '
        'frmAcrList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 561)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboAL)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.dgrFund)
        Me.Controls.Add(Me.lbkReset)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboAccountName)
        Me.Name = "frmAcrList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AcrList"
        CType(Me.dgrFund, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents cboAL As ComboBox
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents dgrFund As DataGridView
    Friend WithEvents lbkReset As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents cboAccountName As ComboBox
End Class
