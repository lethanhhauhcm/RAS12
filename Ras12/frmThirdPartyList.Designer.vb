<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmThirdPartyList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboCAT = New System.Windows.Forms.ComboBox()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.dgrThirdParties = New System.Windows.Forms.DataGridView()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkClone = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrThirdParties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "CAT"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(52, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "FullName"
        '
        'cboCAT
        '
        Me.cboCAT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCAT.FormattingEnabled = True
        Me.cboCAT.Items.AddRange(New Object() {"KB", "MF"})
        Me.cboCAT.Location = New System.Drawing.Point(3, 12)
        Me.cboCAT.Name = "cboCAT"
        Me.cboCAT.Size = New System.Drawing.Size(46, 21)
        Me.cboCAT.TabIndex = 3
        '
        'txtFullName
        '
        Me.txtFullName.Location = New System.Drawing.Point(55, 13)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(201, 20)
        Me.txtFullName.TabIndex = 5
        '
        'dgrThirdParties
        '
        Me.dgrThirdParties.AllowUserToAddRows = False
        Me.dgrThirdParties.AllowUserToDeleteRows = False
        Me.dgrThirdParties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrThirdParties.Location = New System.Drawing.Point(3, 39)
        Me.dgrThirdParties.Name = "dgrThirdParties"
        Me.dgrThirdParties.ReadOnly = True
        Me.dgrThirdParties.Size = New System.Drawing.Size(1006, 543)
        Me.dgrThirdParties.TabIndex = 6
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(532, 18)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 12
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(466, 18)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 11
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkClone
        '
        Me.lbkClone.AutoSize = True
        Me.lbkClone.Location = New System.Drawing.Point(68, 589)
        Me.lbkClone.Name = "lbkClone"
        Me.lbkClone.Size = New System.Drawing.Size(34, 13)
        Me.lbkClone.TabIndex = 16
        Me.lbkClone.TabStop = True
        Me.lbkClone.Text = "Clone"
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(37, 589)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 15
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(0, 589)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 14
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(108, 589)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 17
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'frmThirdPartyList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.lbkClone)
        Me.Controls.Add(Me.lbkEdit)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrThirdParties)
        Me.Controls.Add(Me.txtFullName)
        Me.Controls.Add(Me.cboCAT)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmThirdPartyList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ThirdPartyList"
        CType(Me.dgrThirdParties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cboCAT As ComboBox
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents dgrThirdParties As DataGridView
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkClone As LinkLabel
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkDelete As LinkLabel
End Class
