<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateCustInRcp
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
        Me.cboCustGrp = New System.Windows.Forms.ComboBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dgrRcp = New System.Windows.Forms.DataGridView()
        Me.cboCust = New System.Windows.Forms.ComboBox()
        Me.blkUpdate = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrRcp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboCustGrp
        '
        Me.cboCustGrp.FormattingEnabled = True
        Me.cboCustGrp.Items.AddRange(New Object() {"AIKYA GROUP"})
        Me.cboCustGrp.Location = New System.Drawing.Point(4, 33)
        Me.cboCustGrp.Name = "cboCustGrp"
        Me.cboCustGrp.Size = New System.Drawing.Size(189, 21)
        Me.cboCustGrp.TabIndex = 0
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd MMM yy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(199, 34)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(93, 20)
        Me.dtpFrom.TabIndex = 1
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd MMM yy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(298, 34)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(93, 20)
        Me.dtpTo.TabIndex = 2
        '
        'dgrRcp
        '
        Me.dgrRcp.AllowUserToAddRows = False
        Me.dgrRcp.AllowUserToDeleteRows = False
        Me.dgrRcp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrRcp.Location = New System.Drawing.Point(4, 60)
        Me.dgrRcp.Name = "dgrRcp"
        Me.dgrRcp.ReadOnly = True
        Me.dgrRcp.Size = New System.Drawing.Size(794, 347)
        Me.dgrRcp.TabIndex = 3
        '
        'cboCust
        '
        Me.cboCust.FormattingEnabled = True
        Me.cboCust.Location = New System.Drawing.Point(4, 417)
        Me.cboCust.Name = "cboCust"
        Me.cboCust.Size = New System.Drawing.Size(288, 21)
        Me.cboCust.TabIndex = 4
        '
        'blkUpdate
        '
        Me.blkUpdate.AutoSize = True
        Me.blkUpdate.Location = New System.Drawing.Point(305, 425)
        Me.blkUpdate.Name = "blkUpdate"
        Me.blkUpdate.Size = New System.Drawing.Size(86, 13)
        Me.blkUpdate.TabIndex = 5
        Me.blkUpdate.TabStop = True
        Me.blkUpdate.Text = "UpdateCustomer"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(712, 40)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 6
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'frmUpdateCustInRcp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.blkUpdate)
        Me.Controls.Add(Me.cboCust)
        Me.Controls.Add(Me.dgrRcp)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.cboCustGrp)
        Me.Name = "frmUpdateCustInRcp"
        Me.Text = "UpdateCustInRcp"
        CType(Me.dgrRcp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboCustGrp As ComboBox
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents dgrRcp As DataGridView
    Friend WithEvents cboCust As ComboBox
    Friend WithEvents blkUpdate As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
End Class
