<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExtraRights
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
        Me.dgrExtraRights = New System.Windows.Forms.DataGridView()
        Me.cmbExtraRights = New System.Windows.Forms.ComboBox()
        Me.lbkAdd = New System.Windows.Forms.LinkLabel()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        Me.txtSignIn = New System.Windows.Forms.TextBox()
        CType(Me.dgrExtraRights, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrExtraRights
        '
        Me.dgrExtraRights.AllowUserToAddRows = False
        Me.dgrExtraRights.AllowUserToDeleteRows = False
        Me.dgrExtraRights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrExtraRights.Location = New System.Drawing.Point(3, 74)
        Me.dgrExtraRights.Name = "dgrExtraRights"
        Me.dgrExtraRights.ReadOnly = True
        Me.dgrExtraRights.Size = New System.Drawing.Size(778, 362)
        Me.dgrExtraRights.TabIndex = 0
        '
        'cmbExtraRights
        '
        Me.cmbExtraRights.FormattingEnabled = True
        Me.cmbExtraRights.Items.AddRange(New Object() {"EditPaidNonAirItem", "EditFocVoucher", "AcceptFocVoucher", "ViewCcDetail", "EditVendorUpdateNoBTF", "PrintPmtRQ"})
        Me.cmbExtraRights.Location = New System.Drawing.Point(3, 27)
        Me.cmbExtraRights.Name = "cmbExtraRights"
        Me.cmbExtraRights.Size = New System.Drawing.Size(219, 21)
        Me.cmbExtraRights.TabIndex = 1
        '
        'lbkAdd
        '
        Me.lbkAdd.AutoSize = True
        Me.lbkAdd.Location = New System.Drawing.Point(305, 30)
        Me.lbkAdd.Name = "lbkAdd"
        Me.lbkAdd.Size = New System.Drawing.Size(26, 13)
        Me.lbkAdd.TabIndex = 2
        Me.lbkAdd.TabStop = True
        Me.lbkAdd.Text = "Add"
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(0, 439)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 3
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'txtSignIn
        '
        Me.txtSignIn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSignIn.Location = New System.Drawing.Point(228, 28)
        Me.txtSignIn.MaxLength = 4
        Me.txtSignIn.Name = "txtSignIn"
        Me.txtSignIn.Size = New System.Drawing.Size(61, 20)
        Me.txtSignIn.TabIndex = 4
        '
        'frmExtraRights
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 461)
        Me.Controls.Add(Me.txtSignIn)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.lbkAdd)
        Me.Controls.Add(Me.cmbExtraRights)
        Me.Controls.Add(Me.dgrExtraRights)
        Me.Name = "frmExtraRights"
        Me.Text = "ExtraRights"
        CType(Me.dgrExtraRights, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrExtraRights As DataGridView
    Friend WithEvents cmbExtraRights As ComboBox
    Friend WithEvents lbkAdd As LinkLabel
    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents txtSignIn As TextBox
End Class
