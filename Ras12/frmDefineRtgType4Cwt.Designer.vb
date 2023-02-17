<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefineRtgType4Cwt
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
        Me.dgrRtgType = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.lbkClone = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrRtgType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrRtgType
        '
        Me.dgrRtgType.AllowUserToAddRows = False
        Me.dgrRtgType.AllowUserToDeleteRows = False
        Me.dgrRtgType.BackgroundColor = System.Drawing.Color.Honeydew
        Me.dgrRtgType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrRtgType.Location = New System.Drawing.Point(4, 29)
        Me.dgrRtgType.Name = "dgrRtgType"
        Me.dgrRtgType.RowHeadersVisible = False
        Me.dgrRtgType.Size = New System.Drawing.Size(992, 507)
        Me.dgrRtgType.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Customer"
        '
        'cboCustShortName
        '
        Me.cboCustShortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(58, 2)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(162, 21)
        Me.cboCustShortName.TabIndex = 8
        '
        'lbkClone
        '
        Me.lbkClone.AutoSize = True
        Me.lbkClone.Location = New System.Drawing.Point(2, 539)
        Me.lbkClone.Name = "lbkClone"
        Me.lbkClone.Size = New System.Drawing.Size(34, 13)
        Me.lbkClone.TabIndex = 11
        Me.lbkClone.TabStop = True
        Me.lbkClone.Text = "Clone"
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(55, 539)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 12
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(109, 539)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 13
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'frmDefineRtgType4Cwt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 561)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.lbkEdit)
        Me.Controls.Add(Me.lbkClone)
        Me.Controls.Add(Me.dgrRtgType)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Name = "frmDefineRtgType4Cwt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DefineRtgType4Cwt"
        CType(Me.dgrRtgType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrRtgType As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cboCustShortName As ComboBox
    Friend WithEvents lbkClone As LinkLabel
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkDelete As LinkLabel
End Class
