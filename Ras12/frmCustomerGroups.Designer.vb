<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCustomerGroups
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
        Me.lbkAdd = New System.Windows.Forms.LinkLabel()
        Me.lbkRemove = New System.Windows.Forms.LinkLabel()
        Me.dgCustGroups = New System.Windows.Forms.DataGridView()
        Me.dgrShortNames = New System.Windows.Forms.DataGridView()
        Me.lblAddCust = New System.Windows.Forms.LinkLabel()
        Me.dgrCustomerList = New System.Windows.Forms.DataGridView()
        Me.lbkSaveNewName = New System.Windows.Forms.LinkLabel()
        Me.lbkExpire = New System.Windows.Forms.LinkLabel()
        Me.txtGroupName = New System.Windows.Forms.TextBox()
        Me.txtCustShortName = New System.Windows.Forms.TextBox()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        CType(Me.dgCustGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrShortNames, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrCustomerList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkAdd
        '
        Me.lbkAdd.AutoSize = True
        Me.lbkAdd.Location = New System.Drawing.Point(300, 601)
        Me.lbkAdd.Name = "lbkAdd"
        Me.lbkAdd.Size = New System.Drawing.Size(26, 13)
        Me.lbkAdd.TabIndex = 64
        Me.lbkAdd.TabStop = True
        Me.lbkAdd.Text = "Add"
        '
        'lbkRemove
        '
        Me.lbkRemove.AutoSize = True
        Me.lbkRemove.Location = New System.Drawing.Point(481, 349)
        Me.lbkRemove.Name = "lbkRemove"
        Me.lbkRemove.Size = New System.Drawing.Size(47, 13)
        Me.lbkRemove.TabIndex = 63
        Me.lbkRemove.TabStop = True
        Me.lbkRemove.Text = "Remove"
        '
        'dgCustGroups
        '
        Me.dgCustGroups.AllowUserToAddRows = False
        Me.dgCustGroups.AllowUserToDeleteRows = False
        Me.dgCustGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCustGroups.Location = New System.Drawing.Point(8, 9)
        Me.dgCustGroups.Name = "dgCustGroups"
        Me.dgCustGroups.ReadOnly = True
        Me.dgCustGroups.Size = New System.Drawing.Size(460, 587)
        Me.dgCustGroups.TabIndex = 62
        '
        'dgrShortNames
        '
        Me.dgrShortNames.AllowUserToAddRows = False
        Me.dgrShortNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrShortNames.Location = New System.Drawing.Point(484, 9)
        Me.dgrShortNames.Name = "dgrShortNames"
        Me.dgrShortNames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrShortNames.Size = New System.Drawing.Size(516, 337)
        Me.dgrShortNames.TabIndex = 61
        '
        'lblAddCust
        '
        Me.lblAddCust.AutoSize = True
        Me.lblAddCust.Location = New System.Drawing.Point(847, 604)
        Me.lblAddCust.Name = "lblAddCust"
        Me.lblAddCust.Size = New System.Drawing.Size(70, 13)
        Me.lblAddCust.TabIndex = 60
        Me.lblAddCust.TabStop = True
        Me.lblAddCust.Text = "AddCustomer"
        '
        'dgrCustomerList
        '
        Me.dgrCustomerList.AllowUserToAddRows = False
        Me.dgrCustomerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrCustomerList.Location = New System.Drawing.Point(484, 378)
        Me.dgrCustomerList.Name = "dgrCustomerList"
        Me.dgrCustomerList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrCustomerList.Size = New System.Drawing.Size(516, 218)
        Me.dgrCustomerList.TabIndex = 65
        '
        'lbkSaveNewName
        '
        Me.lbkSaveNewName.AutoSize = True
        Me.lbkSaveNewName.Location = New System.Drawing.Point(332, 601)
        Me.lbkSaveNewName.Name = "lbkSaveNewName"
        Me.lbkSaveNewName.Size = New System.Drawing.Size(82, 13)
        Me.lbkSaveNewName.TabIndex = 66
        Me.lbkSaveNewName.TabStop = True
        Me.lbkSaveNewName.Text = "SaveNewName"
        '
        'lbkExpire
        '
        Me.lbkExpire.AutoSize = True
        Me.lbkExpire.Location = New System.Drawing.Point(420, 601)
        Me.lbkExpire.Name = "lbkExpire"
        Me.lbkExpire.Size = New System.Drawing.Size(36, 13)
        Me.lbkExpire.TabIndex = 67
        Me.lbkExpire.TabStop = True
        Me.lbkExpire.Text = "Expire"
        '
        'txtGroupName
        '
        Me.txtGroupName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtGroupName.Location = New System.Drawing.Point(8, 601)
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(228, 20)
        Me.txtGroupName.TabIndex = 68
        '
        'txtCustShortName
        '
        Me.txtCustShortName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustShortName.Location = New System.Drawing.Point(484, 601)
        Me.txtCustShortName.Name = "txtCustShortName"
        Me.txtCustShortName.Size = New System.Drawing.Size(173, 20)
        Me.txtCustShortName.TabIndex = 69
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(683, 604)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(85, 13)
        Me.lbkSearch.TabIndex = 70
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "SearchCustomer"
        '
        'frmCustomerGroups
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 621)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.txtCustShortName)
        Me.Controls.Add(Me.txtGroupName)
        Me.Controls.Add(Me.lbkExpire)
        Me.Controls.Add(Me.lbkSaveNewName)
        Me.Controls.Add(Me.dgrCustomerList)
        Me.Controls.Add(Me.lbkAdd)
        Me.Controls.Add(Me.lbkRemove)
        Me.Controls.Add(Me.dgCustGroups)
        Me.Controls.Add(Me.dgrShortNames)
        Me.Controls.Add(Me.lblAddCust)
        Me.Name = "frmCustomerGroups"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CustomerGroups"
        CType(Me.dgCustGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrShortNames, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrCustomerList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkAdd As LinkLabel
    Friend WithEvents lbkRemove As LinkLabel
    Friend WithEvents dgCustGroups As DataGridView
    Friend WithEvents dgrShortNames As DataGridView
    Friend WithEvents lblAddCust As LinkLabel
    Friend WithEvents dgrCustomerList As DataGridView
    Friend WithEvents lbkSaveNewName As LinkLabel
    Friend WithEvents lbkExpire As LinkLabel
    Friend WithEvents txtGroupName As TextBox
    Friend WithEvents txtCustShortName As TextBox
    Friend WithEvents lbkSearch As LinkLabel
End Class
