<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVendorGroups
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
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.txtShortName = New System.Windows.Forms.TextBox()
        Me.txtGroupName = New System.Windows.Forms.TextBox()
        Me.lbkExpire = New System.Windows.Forms.LinkLabel()
        Me.lbkSaveNewName = New System.Windows.Forms.LinkLabel()
        Me.dgrVendorList = New System.Windows.Forms.DataGridView()
        Me.lbkAdd = New System.Windows.Forms.LinkLabel()
        Me.lbkRemove = New System.Windows.Forms.LinkLabel()
        Me.dgrVendorGroups = New System.Windows.Forms.DataGridView()
        Me.dgrShortNames = New System.Windows.Forms.DataGridView()
        Me.lblAddVendor = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgrVendorList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrVendorGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrShortNames, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(780, 599)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 81
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'txtShortName
        '
        Me.txtShortName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtShortName.Location = New System.Drawing.Point(564, 596)
        Me.txtShortName.Name = "txtShortName"
        Me.txtShortName.Size = New System.Drawing.Size(173, 20)
        Me.txtShortName.TabIndex = 80
        '
        'txtGroupName
        '
        Me.txtGroupName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtGroupName.Location = New System.Drawing.Point(8, 596)
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(228, 20)
        Me.txtGroupName.TabIndex = 79
        '
        'lbkExpire
        '
        Me.lbkExpire.AutoSize = True
        Me.lbkExpire.Location = New System.Drawing.Point(420, 596)
        Me.lbkExpire.Name = "lbkExpire"
        Me.lbkExpire.Size = New System.Drawing.Size(36, 13)
        Me.lbkExpire.TabIndex = 78
        Me.lbkExpire.TabStop = True
        Me.lbkExpire.Text = "Expire"
        '
        'lbkSaveNewName
        '
        Me.lbkSaveNewName.AutoSize = True
        Me.lbkSaveNewName.Location = New System.Drawing.Point(332, 596)
        Me.lbkSaveNewName.Name = "lbkSaveNewName"
        Me.lbkSaveNewName.Size = New System.Drawing.Size(82, 13)
        Me.lbkSaveNewName.TabIndex = 77
        Me.lbkSaveNewName.TabStop = True
        Me.lbkSaveNewName.Text = "SaveNewName"
        '
        'dgrVendorList
        '
        Me.dgrVendorList.AllowUserToAddRows = False
        Me.dgrVendorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrVendorList.Location = New System.Drawing.Point(484, 373)
        Me.dgrVendorList.Name = "dgrVendorList"
        Me.dgrVendorList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrVendorList.Size = New System.Drawing.Size(516, 218)
        Me.dgrVendorList.TabIndex = 76
        '
        'lbkAdd
        '
        Me.lbkAdd.AutoSize = True
        Me.lbkAdd.Location = New System.Drawing.Point(300, 596)
        Me.lbkAdd.Name = "lbkAdd"
        Me.lbkAdd.Size = New System.Drawing.Size(26, 13)
        Me.lbkAdd.TabIndex = 75
        Me.lbkAdd.TabStop = True
        Me.lbkAdd.Text = "Add"
        '
        'lbkRemove
        '
        Me.lbkRemove.AutoSize = True
        Me.lbkRemove.Location = New System.Drawing.Point(481, 344)
        Me.lbkRemove.Name = "lbkRemove"
        Me.lbkRemove.Size = New System.Drawing.Size(47, 13)
        Me.lbkRemove.TabIndex = 74
        Me.lbkRemove.TabStop = True
        Me.lbkRemove.Text = "Remove"
        '
        'dgrVendorGroups
        '
        Me.dgrVendorGroups.AllowUserToAddRows = False
        Me.dgrVendorGroups.AllowUserToDeleteRows = False
        Me.dgrVendorGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrVendorGroups.Location = New System.Drawing.Point(8, 4)
        Me.dgrVendorGroups.Name = "dgrVendorGroups"
        Me.dgrVendorGroups.ReadOnly = True
        Me.dgrVendorGroups.Size = New System.Drawing.Size(460, 587)
        Me.dgrVendorGroups.TabIndex = 73
        '
        'dgrShortNames
        '
        Me.dgrShortNames.AllowUserToAddRows = False
        Me.dgrShortNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrShortNames.Location = New System.Drawing.Point(484, 4)
        Me.dgrShortNames.Name = "dgrShortNames"
        Me.dgrShortNames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrShortNames.Size = New System.Drawing.Size(516, 337)
        Me.dgrShortNames.TabIndex = 72
        '
        'lblAddVendor
        '
        Me.lblAddVendor.AutoSize = True
        Me.lblAddVendor.Location = New System.Drawing.Point(847, 599)
        Me.lblAddVendor.Name = "lblAddVendor"
        Me.lblAddVendor.Size = New System.Drawing.Size(60, 13)
        Me.lblAddVendor.TabIndex = 71
        Me.lblAddVendor.TabStop = True
        Me.lblAddVendor.Text = "AddVendor"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(481, 599)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 82
        Me.Label1.Text = "ShortName"
        '
        'frmVendorGroups
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 621)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.txtShortName)
        Me.Controls.Add(Me.txtGroupName)
        Me.Controls.Add(Me.lbkExpire)
        Me.Controls.Add(Me.lbkSaveNewName)
        Me.Controls.Add(Me.dgrVendorList)
        Me.Controls.Add(Me.lbkAdd)
        Me.Controls.Add(Me.lbkRemove)
        Me.Controls.Add(Me.dgrVendorGroups)
        Me.Controls.Add(Me.dgrShortNames)
        Me.Controls.Add(Me.lblAddVendor)
        Me.Name = "frmVendorGroups"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VendorGroups"
        CType(Me.dgrVendorList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrVendorGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrShortNames, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents txtShortName As TextBox
    Friend WithEvents txtGroupName As TextBox
    Friend WithEvents lbkExpire As LinkLabel
    Friend WithEvents lbkSaveNewName As LinkLabel
    Friend WithEvents dgrVendorList As DataGridView
    Friend WithEvents lbkAdd As LinkLabel
    Friend WithEvents lbkRemove As LinkLabel
    Friend WithEvents dgrVendorGroups As DataGridView
    Friend WithEvents dgrShortNames As DataGridView
    Friend WithEvents lblAddVendor As LinkLabel
    Friend WithEvents Label1 As Label
End Class
