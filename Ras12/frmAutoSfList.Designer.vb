<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoSfList
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
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.dgrAutoSf = New System.Windows.Forms.DataGridView()
        Me.cboServiceType = New System.Windows.Forms.ComboBox()
        CType(Me.dgrAutoSf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "XX"})
        Me.cboStatus.Location = New System.Drawing.Point(282, 10)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(88, 21)
        Me.cboStatus.TabIndex = 68
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(36, 549)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 67
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(1, 549)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 66
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'cboCustShortName
        '
        Me.cboCustShortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(4, 10)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(145, 21)
        Me.cboCustShortName.TabIndex = 65
        '
        'dgrAutoSf
        '
        Me.dgrAutoSf.AllowUserToAddRows = False
        Me.dgrAutoSf.AllowUserToDeleteRows = False
        Me.dgrAutoSf.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrAutoSf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrAutoSf.Location = New System.Drawing.Point(4, 40)
        Me.dgrAutoSf.Name = "dgrAutoSf"
        Me.dgrAutoSf.ReadOnly = True
        Me.dgrAutoSf.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrAutoSf.Size = New System.Drawing.Size(1012, 499)
        Me.dgrAutoSf.TabIndex = 64
        '
        'cboServiceType
        '
        Me.cboServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboServiceType.FormattingEnabled = True
        Me.cboServiceType.Items.AddRange(New Object() {"STANDARD", "URGENT"})
        Me.cboServiceType.Location = New System.Drawing.Point(155, 10)
        Me.cboServiceType.Name = "cboServiceType"
        Me.cboServiceType.Size = New System.Drawing.Size(121, 21)
        Me.cboServiceType.TabIndex = 69
        '
        'frmAutoSfList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 573)
        Me.Controls.Add(Me.cboServiceType)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.lbkEdit)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Controls.Add(Me.dgrAutoSf)
        Me.Name = "frmAutoSfList"
        Me.Text = "Auto Servire Fee List"
        CType(Me.dgrAutoSf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents cboCustShortName As ComboBox
    Friend WithEvents dgrAutoSf As DataGridView
    Friend WithEvents cboServiceType As ComboBox
End Class
