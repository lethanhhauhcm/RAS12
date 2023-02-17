<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFrieslandNoMXP
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
        Me.lblTCode = New System.Windows.Forms.Label()
        Me.txtTCode = New System.Windows.Forms.TextBox()
        Me.txtDutoanID = New System.Windows.Forms.TextBox()
        Me.lblDutoanID = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.LinkLabel()
        Me.dgvFrieslandNoMXP = New System.Windows.Forms.DataGridView()
        Me.llbDelete = New System.Windows.Forms.LinkLabel()
        CType(Me.dgvFrieslandNoMXP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTCode
        '
        Me.lblTCode.AutoSize = True
        Me.lblTCode.Location = New System.Drawing.Point(30, 15)
        Me.lblTCode.Name = "lblTCode"
        Me.lblTCode.Size = New System.Drawing.Size(39, 13)
        Me.lblTCode.TabIndex = 0
        Me.lblTCode.Text = "TCode"
        '
        'txtTCode
        '
        Me.txtTCode.Location = New System.Drawing.Point(75, 12)
        Me.txtTCode.Name = "txtTCode"
        Me.txtTCode.Size = New System.Drawing.Size(290, 20)
        Me.txtTCode.TabIndex = 1
        '
        'txtDutoanID
        '
        Me.txtDutoanID.Location = New System.Drawing.Point(75, 38)
        Me.txtDutoanID.Name = "txtDutoanID"
        Me.txtDutoanID.ReadOnly = True
        Me.txtDutoanID.Size = New System.Drawing.Size(60, 20)
        Me.txtDutoanID.TabIndex = 2
        Me.txtDutoanID.TabStop = False
        Me.txtDutoanID.Text = "0"
        Me.txtDutoanID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDutoanID
        '
        Me.lblDutoanID.AutoSize = True
        Me.lblDutoanID.Location = New System.Drawing.Point(16, 41)
        Me.lblDutoanID.Name = "lblDutoanID"
        Me.lblDutoanID.Size = New System.Drawing.Size(53, 13)
        Me.lblDutoanID.TabIndex = 3
        Me.lblDutoanID.Text = "DutoanID"
        '
        'btnAdd
        '
        Me.btnAdd.AutoSize = True
        Me.btnAdd.Location = New System.Drawing.Point(12, 70)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(26, 13)
        Me.btnAdd.TabIndex = 4
        Me.btnAdd.TabStop = True
        Me.btnAdd.Text = "Add"
        '
        'dgvFrieslandNoMXP
        '
        Me.dgvFrieslandNoMXP.AllowUserToAddRows = False
        Me.dgvFrieslandNoMXP.AllowUserToDeleteRows = False
        Me.dgvFrieslandNoMXP.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvFrieslandNoMXP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvFrieslandNoMXP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFrieslandNoMXP.Location = New System.Drawing.Point(12, 86)
        Me.dgvFrieslandNoMXP.Name = "dgvFrieslandNoMXP"
        Me.dgvFrieslandNoMXP.ReadOnly = True
        Me.dgvFrieslandNoMXP.Size = New System.Drawing.Size(505, 150)
        Me.dgvFrieslandNoMXP.TabIndex = 5
        '
        'llbDelete
        '
        Me.llbDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.llbDelete.AutoSize = True
        Me.llbDelete.Location = New System.Drawing.Point(12, 239)
        Me.llbDelete.Name = "llbDelete"
        Me.llbDelete.Size = New System.Drawing.Size(38, 13)
        Me.llbDelete.TabIndex = 6
        Me.llbDelete.TabStop = True
        Me.llbDelete.Text = "Delete"
        '
        'frmFrieslandNoMXP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 260)
        Me.Controls.Add(Me.llbDelete)
        Me.Controls.Add(Me.dgvFrieslandNoMXP)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lblDutoanID)
        Me.Controls.Add(Me.txtDutoanID)
        Me.Controls.Add(Me.txtTCode)
        Me.Controls.Add(Me.lblTCode)
        Me.Name = "frmFrieslandNoMXP"
        Me.Text = "FrieslandNoMXP"
        CType(Me.dgvFrieslandNoMXP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTCode As Label
    Friend WithEvents txtTCode As TextBox
    Friend WithEvents txtDutoanID As TextBox
    Friend WithEvents lblDutoanID As Label
    Friend WithEvents btnAdd As LinkLabel
    Friend WithEvents dgvFrieslandNoMXP As DataGridView
    Friend WithEvents llbDelete As LinkLabel
End Class
