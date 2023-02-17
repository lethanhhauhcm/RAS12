<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRunUTTR
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
        Me.dgrUTTR = New System.Windows.Forms.DataGridView()
        Me.lbkImport1A = New System.Windows.Forms.LinkLabel()
        Me.cboCounter = New System.Windows.Forms.ComboBox()
        Me.lbkRefresh = New System.Windows.Forms.LinkLabel()
        Me.lbkChangeTktStatus = New System.Windows.Forms.LinkLabel()
        Me.cboTktStatus = New System.Windows.Forms.ComboBox()
        Me.cboFilterByTktStatus = New System.Windows.Forms.ComboBox()
        CType(Me.dgrUTTR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrUTTR
        '
        Me.dgrUTTR.AllowUserToAddRows = False
        Me.dgrUTTR.AllowUserToDeleteRows = False
        Me.dgrUTTR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrUTTR.Location = New System.Drawing.Point(0, 27)
        Me.dgrUTTR.Name = "dgrUTTR"
        Me.dgrUTTR.Size = New System.Drawing.Size(996, 270)
        Me.dgrUTTR.TabIndex = 0
        '
        'lbkImport1A
        '
        Me.lbkImport1A.AutoSize = True
        Me.lbkImport1A.Location = New System.Drawing.Point(3, 300)
        Me.lbkImport1A.Name = "lbkImport1A"
        Me.lbkImport1A.Size = New System.Drawing.Size(49, 13)
        Me.lbkImport1A.TabIndex = 3
        Me.lbkImport1A.TabStop = True
        Me.lbkImport1A.Text = "Import1A"
        '
        'cboCounter
        '
        Me.cboCounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCounter.FormattingEnabled = True
        Me.cboCounter.Items.AddRange(New Object() {"TVS", "CWT"})
        Me.cboCounter.Location = New System.Drawing.Point(0, 0)
        Me.cboCounter.Name = "cboCounter"
        Me.cboCounter.Size = New System.Drawing.Size(74, 21)
        Me.cboCounter.TabIndex = 4
        '
        'lbkRefresh
        '
        Me.lbkRefresh.AutoSize = True
        Me.lbkRefresh.Location = New System.Drawing.Point(58, 300)
        Me.lbkRefresh.Name = "lbkRefresh"
        Me.lbkRefresh.Size = New System.Drawing.Size(44, 13)
        Me.lbkRefresh.TabIndex = 5
        Me.lbkRefresh.TabStop = True
        Me.lbkRefresh.Text = "Refresh"
        '
        'lbkChangeTktStatus
        '
        Me.lbkChangeTktStatus.AutoSize = True
        Me.lbkChangeTktStatus.Location = New System.Drawing.Point(108, 300)
        Me.lbkChangeTktStatus.Name = "lbkChangeTktStatus"
        Me.lbkChangeTktStatus.Size = New System.Drawing.Size(96, 13)
        Me.lbkChangeTktStatus.TabIndex = 6
        Me.lbkChangeTktStatus.TabStop = True
        Me.lbkChangeTktStatus.Text = "ChangeTktStatus2"
        '
        'cboTktStatus
        '
        Me.cboTktStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTktStatus.FormattingEnabled = True
        Me.cboTktStatus.Items.AddRange(New Object() {"NR - NonRefundable", "EX - Exprired", "RF - Refunded"})
        Me.cboTktStatus.Location = New System.Drawing.Point(210, 297)
        Me.cboTktStatus.Name = "cboTktStatus"
        Me.cboTktStatus.Size = New System.Drawing.Size(70, 21)
        Me.cboTktStatus.TabIndex = 7
        '
        'cboFilterByTktStatus
        '
        Me.cboFilterByTktStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFilterByTktStatus.FormattingEnabled = True
        Me.cboFilterByTktStatus.Items.AddRange(New Object() {"OK", "NR ", "EX", "RF"})
        Me.cboFilterByTktStatus.Location = New System.Drawing.Point(89, 0)
        Me.cboFilterByTktStatus.Name = "cboFilterByTktStatus"
        Me.cboFilterByTktStatus.Size = New System.Drawing.Size(70, 21)
        Me.cboFilterByTktStatus.TabIndex = 8
        '
        'frmRunUTTR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.cboFilterByTktStatus)
        Me.Controls.Add(Me.cboTktStatus)
        Me.Controls.Add(Me.lbkChangeTktStatus)
        Me.Controls.Add(Me.lbkRefresh)
        Me.Controls.Add(Me.cboCounter)
        Me.Controls.Add(Me.lbkImport1A)
        Me.Controls.Add(Me.dgrUTTR)
        Me.Name = "frmRunUTTR"
        Me.Text = "RunUTTR"
        CType(Me.dgrUTTR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgrUTTR As System.Windows.Forms.DataGridView
    Friend WithEvents lbkImport1A As System.Windows.Forms.LinkLabel
    Friend WithEvents cboCounter As ComboBox
    Friend WithEvents lbkRefresh As LinkLabel
    Friend WithEvents lbkChangeTktStatus As LinkLabel
    Friend WithEvents cboTktStatus As ComboBox
    Friend WithEvents cboFilterByTktStatus As ComboBox
End Class
