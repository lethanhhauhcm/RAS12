<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCitiBankCode
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbkAdd = New System.Windows.Forms.LinkLabel()
        Me.lbkUpdate = New System.Windows.Forms.LinkLabel()
        Me.txtBankCode = New System.Windows.Forms.TextBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.dgrBankCode = New System.Windows.Forms.DataGridView()
        Me.dgrMapping = New System.Windows.Forms.DataGridView()
        CType(Me.dgrBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "BankCode"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(109, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "BankName"
        '
        'lbkAdd
        '
        Me.lbkAdd.AutoSize = True
        Me.lbkAdd.Location = New System.Drawing.Point(419, 23)
        Me.lbkAdd.Name = "lbkAdd"
        Me.lbkAdd.Size = New System.Drawing.Size(26, 13)
        Me.lbkAdd.TabIndex = 2
        Me.lbkAdd.TabStop = True
        Me.lbkAdd.Text = "Add"
        '
        'lbkUpdate
        '
        Me.lbkUpdate.AutoSize = True
        Me.lbkUpdate.Location = New System.Drawing.Point(451, 23)
        Me.lbkUpdate.Name = "lbkUpdate"
        Me.lbkUpdate.Size = New System.Drawing.Size(42, 13)
        Me.lbkUpdate.TabIndex = 3
        Me.lbkUpdate.TabStop = True
        Me.lbkUpdate.Text = "Update"
        '
        'txtBankCode
        '
        Me.txtBankCode.Location = New System.Drawing.Point(3, 16)
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(100, 20)
        Me.txtBankCode.TabIndex = 4
        '
        'txtBankName
        '
        Me.txtBankName.Location = New System.Drawing.Point(112, 16)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(301, 20)
        Me.txtBankName.TabIndex = 5
        '
        'dgrBankCode
        '
        Me.dgrBankCode.AllowUserToAddRows = False
        Me.dgrBankCode.AllowUserToDeleteRows = False
        Me.dgrBankCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrBankCode.Location = New System.Drawing.Point(3, 42)
        Me.dgrBankCode.Name = "dgrBankCode"
        Me.dgrBankCode.ReadOnly = True
        Me.dgrBankCode.Size = New System.Drawing.Size(813, 177)
        Me.dgrBankCode.TabIndex = 6
        '
        'dgrMapping
        '
        Me.dgrMapping.AllowUserToAddRows = False
        Me.dgrMapping.AllowUserToDeleteRows = False
        Me.dgrMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrMapping.Location = New System.Drawing.Point(3, 225)
        Me.dgrMapping.Name = "dgrMapping"
        Me.dgrMapping.ReadOnly = True
        Me.dgrMapping.Size = New System.Drawing.Size(813, 236)
        Me.dgrMapping.TabIndex = 7
        '
        'frmCitiBankCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 473)
        Me.Controls.Add(Me.dgrMapping)
        Me.Controls.Add(Me.dgrBankCode)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.txtBankCode)
        Me.Controls.Add(Me.lbkUpdate)
        Me.Controls.Add(Me.lbkAdd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmCitiBankCode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CitiBankCode"
        CType(Me.dgrBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrMapping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbkAdd As System.Windows.Forms.LinkLabel
    Friend WithEvents lbkUpdate As System.Windows.Forms.LinkLabel
    Friend WithEvents txtBankCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents dgrBankCode As System.Windows.Forms.DataGridView
    Friend WithEvents dgrMapping As System.Windows.Forms.DataGridView
End Class
