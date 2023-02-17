<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGetBankProvince
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
        Me.chkShowAll = New System.Windows.Forms.CheckBox()
        Me.dgrProvinceMap = New System.Windows.Forms.DataGridView()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrProvinceMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkShowAll
        '
        Me.chkShowAll.AutoSize = True
        Me.chkShowAll.Location = New System.Drawing.Point(1, 12)
        Me.chkShowAll.Name = "chkShowAll"
        Me.chkShowAll.Size = New System.Drawing.Size(64, 17)
        Me.chkShowAll.TabIndex = 0
        Me.chkShowAll.Text = "ShowAll"
        Me.chkShowAll.UseVisualStyleBackColor = True
        '
        'dgrProvinceMap
        '
        Me.dgrProvinceMap.AllowUserToAddRows = False
        Me.dgrProvinceMap.AllowUserToDeleteRows = False
        Me.dgrProvinceMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrProvinceMap.Location = New System.Drawing.Point(1, 35)
        Me.dgrProvinceMap.Name = "dgrProvinceMap"
        Me.dgrProvinceMap.ReadOnly = True
        Me.dgrProvinceMap.Size = New System.Drawing.Size(283, 401)
        Me.dgrProvinceMap.TabIndex = 7
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(39, 439)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 41
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(192, 439)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 42
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'frmGetBankProvince
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 461)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.dgrProvinceMap)
        Me.Controls.Add(Me.chkShowAll)
        Me.Name = "frmGetBankProvince"
        Me.Text = "GetBankProvince"
        CType(Me.dgrProvinceMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chkShowAll As CheckBox
    Friend WithEvents dgrProvinceMap As DataGridView
    Friend WithEvents lbkSelect As LinkLabel
    Friend WithEvents lbkCancel As LinkLabel
End Class
