<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportProfiles
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
        Me.dgrHrFiles = New System.Windows.Forms.DataGridView()
        Me.lbkImport = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrHrFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrHrFiles
        '
        Me.dgrHrFiles.AllowUserToAddRows = False
        Me.dgrHrFiles.AllowUserToDeleteRows = False
        Me.dgrHrFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrHrFiles.Location = New System.Drawing.Point(0, 46)
        Me.dgrHrFiles.Name = "dgrHrFiles"
        Me.dgrHrFiles.ReadOnly = True
        Me.dgrHrFiles.Size = New System.Drawing.Size(781, 381)
        Me.dgrHrFiles.TabIndex = 0
        '
        'lbkImport
        '
        Me.lbkImport.AutoSize = True
        Me.lbkImport.Location = New System.Drawing.Point(12, 9)
        Me.lbkImport.Name = "lbkImport"
        Me.lbkImport.Size = New System.Drawing.Size(36, 13)
        Me.lbkImport.TabIndex = 1
        Me.lbkImport.TabStop = True
        Me.lbkImport.Text = "Import"
        '
        'frmImportProfiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 461)
        Me.Controls.Add(Me.lbkImport)
        Me.Controls.Add(Me.dgrHrFiles)
        Me.Name = "frmImportProfiles"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import RM Traveler Profiles"
        CType(Me.dgrHrFiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrHrFiles As DataGridView
    Friend WithEvents lbkImport As LinkLabel
End Class
