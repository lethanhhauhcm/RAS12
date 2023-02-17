<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDebitDueDate
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
        Me.lblstrVal1 = New System.Windows.Forms.Label()
        Me.llbAdd = New System.Windows.Forms.LinkLabel()
        Me.dgvMain = New System.Windows.Forms.DataGridView()
        Me.llbDelete = New System.Windows.Forms.LinkLabel()
        Me.cbostrVal1 = New System.Windows.Forms.ComboBox()
        Me.lblinVal = New System.Windows.Forms.Label()
        Me.txtintVal = New System.Windows.Forms.TextBox()
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblstrVal1
        '
        Me.lblstrVal1.AutoSize = True
        Me.lblstrVal1.Location = New System.Drawing.Point(12, 16)
        Me.lblstrVal1.Name = "lblstrVal1"
        Me.lblstrVal1.Size = New System.Drawing.Size(51, 13)
        Me.lblstrVal1.TabIndex = 0
        Me.lblstrVal1.Text = "Customer"
        '
        'llbAdd
        '
        Me.llbAdd.AutoSize = True
        Me.llbAdd.Location = New System.Drawing.Point(443, 16)
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
        Me.llbDelete.Location = New System.Drawing.Point(556, 16)
        Me.llbDelete.Name = "llbDelete"
        Me.llbDelete.Size = New System.Drawing.Size(38, 13)
        Me.llbDelete.TabIndex = 2
        Me.llbDelete.TabStop = True
        Me.llbDelete.Text = "Delete"
        '
        'cbostrVal1
        '
        Me.cbostrVal1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbostrVal1.FormattingEnabled = True
        Me.cbostrVal1.Location = New System.Drawing.Point(69, 12)
        Me.cbostrVal1.Name = "cbostrVal1"
        Me.cbostrVal1.Size = New System.Drawing.Size(200, 21)
        Me.cbostrVal1.TabIndex = 4
        '
        'lblinVal
        '
        Me.lblinVal.AutoSize = True
        Me.lblinVal.Location = New System.Drawing.Point(311, 16)
        Me.lblinVal.Name = "lblinVal"
        Me.lblinVal.Size = New System.Drawing.Size(80, 13)
        Me.lblinVal.TabIndex = 5
        Me.lblinVal.Text = "Number Of Day"
        '
        'txtintVal
        '
        Me.txtintVal.Location = New System.Drawing.Point(397, 13)
        Me.txtintVal.Name = "txtintVal"
        Me.txtintVal.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtintVal.Size = New System.Drawing.Size(40, 20)
        Me.txtintVal.TabIndex = 6
        '
        'frmDebitDueDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.txtintVal)
        Me.Controls.Add(Me.lblinVal)
        Me.Controls.Add(Me.cbostrVal1)
        Me.Controls.Add(Me.llbDelete)
        Me.Controls.Add(Me.dgvMain)
        Me.Controls.Add(Me.llbAdd)
        Me.Controls.Add(Me.lblstrVal1)
        Me.Name = "frmDebitDueDate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DebitDueDate"
        CType(Me.dgvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblstrVal1 As Label
    Friend WithEvents llbAdd As LinkLabel
    Friend WithEvents dgvMain As DataGridView
    Friend WithEvents llbDelete As LinkLabel
    Friend WithEvents cbostrVal1 As ComboBox
    Friend WithEvents lblinVal As Label
    Friend WithEvents txtintVal As TextBox
End Class
