<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCounterCheck
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
        Me.cboTKNO = New System.Windows.Forms.ComboBox()
        Me.txtTKNO = New System.Windows.Forms.TextBox()
        Me.llbCheckByCounter = New System.Windows.Forms.LinkLabel()
        Me.llbUnCheckByCounter = New System.Windows.Forms.LinkLabel()
        Me.dgvMain = New System.Windows.Forms.DataGridView()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboTKNO
        '
        Me.cboTKNO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTKNO.FormattingEnabled = True
        Me.cboTKNO.Items.AddRange(New Object() {"TKNO", "RCPNo"})
        Me.cboTKNO.Location = New System.Drawing.Point(12, 12)
        Me.cboTKNO.Name = "cboTKNO"
        Me.cboTKNO.Size = New System.Drawing.Size(80, 21)
        Me.cboTKNO.TabIndex = 0
        '
        'txtTKNO
        '
        Me.txtTKNO.Location = New System.Drawing.Point(98, 12)
        Me.txtTKNO.Name = "txtTKNO"
        Me.txtTKNO.Size = New System.Drawing.Size(130, 20)
        Me.txtTKNO.TabIndex = 1
        '
        'llbCheckByCounter
        '
        Me.llbCheckByCounter.AutoSize = True
        Me.llbCheckByCounter.Enabled = False
        Me.llbCheckByCounter.Location = New System.Drawing.Point(234, 15)
        Me.llbCheckByCounter.Name = "llbCheckByCounter"
        Me.llbCheckByCounter.Size = New System.Drawing.Size(87, 13)
        Me.llbCheckByCounter.TabIndex = 4
        Me.llbCheckByCounter.TabStop = True
        Me.llbCheckByCounter.Text = "CheckByCounter"
        '
        'llbUnCheckByCounter
        '
        Me.llbUnCheckByCounter.AutoSize = True
        Me.llbUnCheckByCounter.Enabled = False
        Me.llbUnCheckByCounter.Location = New System.Drawing.Point(327, 15)
        Me.llbUnCheckByCounter.Name = "llbUnCheckByCounter"
        Me.llbUnCheckByCounter.Size = New System.Drawing.Size(101, 13)
        Me.llbUnCheckByCounter.TabIndex = 5
        Me.llbUnCheckByCounter.TabStop = True
        Me.llbUnCheckByCounter.Text = "UnCheckByCounter"
        '
        'dgvMain
        '
        Me.dgvMain.AllowUserToAddRows = False
        Me.dgvMain.AllowUserToDeleteRows = False
        Me.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMain.Location = New System.Drawing.Point(12, 39)
        Me.dgvMain.Name = "dgvMain"
        Me.dgvMain.ReadOnly = True
        Me.dgvMain.Size = New System.Drawing.Size(570, 221)
        Me.dgvMain.TabIndex = 6
        '
        'frmCounterCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 271)
        Me.Controls.Add(Me.dgvMain)
        Me.Controls.Add(Me.llbUnCheckByCounter)
        Me.Controls.Add(Me.llbCheckByCounter)
        Me.Controls.Add(Me.txtTKNO)
        Me.Controls.Add(Me.cboTKNO)
        Me.Name = "frmCounterCheck"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CounterCheck"
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboTKNO As ComboBox
    Friend WithEvents txtTKNO As TextBox
    Friend WithEvents llbCheckByCounter As LinkLabel
    Friend WithEvents llbUnCheckByCounter As LinkLabel
    Friend WithEvents dgvMain As DataGridView
End Class
