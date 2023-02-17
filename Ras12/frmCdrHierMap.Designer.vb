<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCdrHierMap
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
        Me.lbkAdd = New System.Windows.Forms.LinkLabel()
        Me.lbkUpdate = New System.Windows.Forms.LinkLabel()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        Me.dgrMap = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCMC = New System.Windows.Forms.TextBox()
        Me.txtCdr = New System.Windows.Forms.TextBox()
        Me.txtHier = New System.Windows.Forms.TextBox()
        CType(Me.dgrMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkAdd
        '
        Me.lbkAdd.AutoSize = True
        Me.lbkAdd.Location = New System.Drawing.Point(8, 415)
        Me.lbkAdd.Name = "lbkAdd"
        Me.lbkAdd.Size = New System.Drawing.Size(26, 13)
        Me.lbkAdd.TabIndex = 0
        Me.lbkAdd.TabStop = True
        Me.lbkAdd.Text = "Add"
        '
        'lbkUpdate
        '
        Me.lbkUpdate.AutoSize = True
        Me.lbkUpdate.Location = New System.Drawing.Point(73, 415)
        Me.lbkUpdate.Name = "lbkUpdate"
        Me.lbkUpdate.Size = New System.Drawing.Size(42, 13)
        Me.lbkUpdate.TabIndex = 1
        Me.lbkUpdate.TabStop = True
        Me.lbkUpdate.Text = "Update"
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(137, 415)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 2
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'dgrMap
        '
        Me.dgrMap.AllowUserToAddRows = False
        Me.dgrMap.AllowUserToDeleteRows = False
        Me.dgrMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrMap.Location = New System.Drawing.Point(2, 12)
        Me.dgrMap.Name = "dgrMap"
        Me.dgrMap.ReadOnly = True
        Me.dgrMap.Size = New System.Drawing.Size(786, 353)
        Me.dgrMap.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 368)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "CMC"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(76, 368)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "CDR"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(138, 368)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Hierachy"
        '
        'txtCMC
        '
        Me.txtCMC.Location = New System.Drawing.Point(11, 384)
        Me.txtCMC.Name = "txtCMC"
        Me.txtCMC.Size = New System.Drawing.Size(47, 20)
        Me.txtCMC.TabIndex = 7
        '
        'txtCdr
        '
        Me.txtCdr.Location = New System.Drawing.Point(76, 384)
        Me.txtCdr.Name = "txtCdr"
        Me.txtCdr.Size = New System.Drawing.Size(47, 20)
        Me.txtCdr.TabIndex = 8
        '
        'txtHier
        '
        Me.txtHier.Location = New System.Drawing.Point(140, 384)
        Me.txtHier.Name = "txtHier"
        Me.txtHier.Size = New System.Drawing.Size(47, 20)
        Me.txtHier.TabIndex = 9
        '
        'frmCdrHierMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.txtHier)
        Me.Controls.Add(Me.txtCdr)
        Me.Controls.Add(Me.txtCMC)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgrMap)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.lbkUpdate)
        Me.Controls.Add(Me.lbkAdd)
        Me.Name = "frmCdrHierMap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CdrHierMap"
        CType(Me.dgrMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkAdd As LinkLabel
    Friend WithEvents lbkUpdate As LinkLabel
    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents dgrMap As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtCMC As TextBox
    Friend WithEvents txtCdr As TextBox
    Friend WithEvents txtHier As TextBox
End Class
