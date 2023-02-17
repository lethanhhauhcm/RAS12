<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeletedNonAirHtl
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
        Me.dgrDeleted = New System.Windows.Forms.DataGridView()
        Me.lbkDeleteOldTakeNew = New System.Windows.Forms.LinkLabel()
        Me.lbkKeepOldExludeNew = New System.Windows.Forms.LinkLabel()
        Me.dgrNew = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkSameHotelName = New System.Windows.Forms.CheckBox()
        CType(Me.dgrDeleted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrDeleted
        '
        Me.dgrDeleted.AllowUserToAddRows = False
        Me.dgrDeleted.AllowUserToDeleteRows = False
        Me.dgrDeleted.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrDeleted.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrDeleted.Location = New System.Drawing.Point(12, 12)
        Me.dgrDeleted.Name = "dgrDeleted"
        Me.dgrDeleted.ReadOnly = True
        Me.dgrDeleted.Size = New System.Drawing.Size(560, 154)
        Me.dgrDeleted.TabIndex = 1
        '
        'lbkDeleteOldTakeNew
        '
        Me.lbkDeleteOldTakeNew.AutoSize = True
        Me.lbkDeleteOldTakeNew.Location = New System.Drawing.Point(12, 350)
        Me.lbkDeleteOldTakeNew.Name = "lbkDeleteOldTakeNew"
        Me.lbkDeleteOldTakeNew.Size = New System.Drawing.Size(101, 13)
        Me.lbkDeleteOldTakeNew.TabIndex = 2
        Me.lbkDeleteOldTakeNew.TabStop = True
        Me.lbkDeleteOldTakeNew.Text = "DeleteOldTakeNew"
        '
        'lbkKeepOldExludeNew
        '
        Me.lbkKeepOldExludeNew.AutoSize = True
        Me.lbkKeepOldExludeNew.Location = New System.Drawing.Point(117, 350)
        Me.lbkKeepOldExludeNew.Name = "lbkKeepOldExludeNew"
        Me.lbkKeepOldExludeNew.Size = New System.Drawing.Size(102, 13)
        Me.lbkKeepOldExludeNew.TabIndex = 3
        Me.lbkKeepOldExludeNew.TabStop = True
        Me.lbkKeepOldExludeNew.Text = "KeepOldExludeNew"
        '
        'dgrNew
        '
        Me.dgrNew.AllowUserToAddRows = False
        Me.dgrNew.AllowUserToDeleteRows = False
        Me.dgrNew.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrNew.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrNew.Location = New System.Drawing.Point(12, 193)
        Me.dgrNew.Name = "dgrNew"
        Me.dgrNew.ReadOnly = True
        Me.dgrNew.Size = New System.Drawing.Size(560, 154)
        Me.dgrNew.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 177)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "NewRecord"
        '
        'chkSameHotelName
        '
        Me.chkSameHotelName.AutoSize = True
        Me.chkSameHotelName.Location = New System.Drawing.Point(85, 173)
        Me.chkSameHotelName.Name = "chkSameHotelName"
        Me.chkSameHotelName.Size = New System.Drawing.Size(106, 17)
        Me.chkSameHotelName.TabIndex = 6
        Me.chkSameHotelName.Text = "SameHotelName"
        Me.chkSameHotelName.UseVisualStyleBackColor = True
        '
        'frmDeletedNonAirHtl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 372)
        Me.Controls.Add(Me.chkSameHotelName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgrNew)
        Me.Controls.Add(Me.lbkKeepOldExludeNew)
        Me.Controls.Add(Me.lbkDeleteOldTakeNew)
        Me.Controls.Add(Me.dgrDeleted)
        Me.Name = "frmDeletedNonAirHtl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DeletedNonAirHtl"
        CType(Me.dgrDeleted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrDeleted As DataGridView
    Friend WithEvents lbkDeleteOldTakeNew As LinkLabel
    Friend WithEvents lbkKeepOldExludeNew As LinkLabel
    Friend WithEvents dgrNew As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents chkSameHotelName As CheckBox
End Class
