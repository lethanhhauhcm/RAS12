<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchProvince
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
        Me.chkShowAlls = New System.Windows.Forms.CheckBox()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Address"
        '
        'chkShowAlls
        '
        Me.chkShowAlls.AutoSize = True
        Me.chkShowAlls.Location = New System.Drawing.Point(279, 8)
        Me.chkShowAlls.Name = "chkShowAlls"
        Me.chkShowAlls.Size = New System.Drawing.Size(69, 17)
        Me.chkShowAlls.TabIndex = 1
        Me.chkShowAlls.Text = "ShowAlls"
        Me.chkShowAlls.UseVisualStyleBackColor = True
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(151, 439)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 2
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(63, 5)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(210, 20)
        Me.txtAddress.TabIndex = 3
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 31)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(336, 399)
        Me.DataGridView1.TabIndex = 4
        '
        'frmSearchProvince
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 461)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.chkShowAlls)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmSearchProvince"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SearchProvince"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents chkShowAlls As CheckBox
    Friend WithEvents lbkSelect As LinkLabel
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents DataGridView1 As DataGridView
End Class
