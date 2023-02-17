<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCustGroup
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
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.dgrCustGroup = New System.Windows.Forms.DataGridView()
        Me.dgrCustomers = New System.Windows.Forms.DataGridView()
        Me.RecId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustShortName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbkClone = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkRemove = New System.Windows.Forms.LinkLabel()
        Me.lbkSearchCust = New System.Windows.Forms.LinkLabel()
        Me.txtCustShortName = New System.Windows.Forms.TextBox()
        Me.dgrCustomerList = New System.Windows.Forms.DataGridView()
        Me.lbkAddCust = New System.Windows.Forms.LinkLabel()
        Me.txtNewGroupName = New System.Windows.Forms.TextBox()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrCustGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrCustomers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrCustomerList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(9, 9)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(51, 13)
        Me.lblCustomer.TabIndex = 1
        Me.lblCustomer.Text = "Customer"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(383, 28)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 10
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(336, 28)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 9
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(12, 25)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(318, 21)
        Me.cboCustomer.TabIndex = 12
        '
        'dgrCustGroup
        '
        Me.dgrCustGroup.AllowUserToAddRows = False
        Me.dgrCustGroup.AllowUserToDeleteRows = False
        Me.dgrCustGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrCustGroup.Location = New System.Drawing.Point(12, 52)
        Me.dgrCustGroup.Name = "dgrCustGroup"
        Me.dgrCustGroup.ReadOnly = True
        Me.dgrCustGroup.Size = New System.Drawing.Size(318, 520)
        Me.dgrCustGroup.TabIndex = 13
        '
        'dgrCustomers
        '
        Me.dgrCustomers.AllowUserToAddRows = False
        Me.dgrCustomers.AllowUserToDeleteRows = False
        Me.dgrCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrCustomers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RecId, Me.CustShortName, Me.CustId})
        Me.dgrCustomers.Location = New System.Drawing.Point(336, 52)
        Me.dgrCustomers.Name = "dgrCustomers"
        Me.dgrCustomers.ReadOnly = True
        Me.dgrCustomers.Size = New System.Drawing.Size(452, 278)
        Me.dgrCustomers.TabIndex = 18
        '
        'RecId
        '
        Me.RecId.DataPropertyName = "RecId"
        Me.RecId.HeaderText = "RecId"
        Me.RecId.Name = "RecId"
        Me.RecId.ReadOnly = True
        Me.RecId.Width = 61
        '
        'CustShortName
        '
        Me.CustShortName.DataPropertyName = "CustShortName"
        Me.CustShortName.HeaderText = "CustShortName"
        Me.CustShortName.Name = "CustShortName"
        Me.CustShortName.ReadOnly = True
        Me.CustShortName.Width = 106
        '
        'CustId
        '
        Me.CustId.DataPropertyName = "CustId"
        Me.CustId.HeaderText = "CustId"
        Me.CustId.Name = "CustId"
        Me.CustId.ReadOnly = True
        Me.CustId.Width = 62
        '
        'lbkClone
        '
        Me.lbkClone.AutoSize = True
        Me.lbkClone.Location = New System.Drawing.Point(12, 584)
        Me.lbkClone.Name = "lbkClone"
        Me.lbkClone.Size = New System.Drawing.Size(63, 13)
        Me.lbkClone.TabIndex = 70
        Me.lbkClone.TabStop = True
        Me.lbkClone.Text = "CloneGroup"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(660, 337)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(58, 13)
        Me.lbkNew.TabIndex = 69
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "NewGroup"
        '
        'lbkRemove
        '
        Me.lbkRemove.AutoSize = True
        Me.lbkRemove.Location = New System.Drawing.Point(721, 337)
        Me.lbkRemove.Name = "lbkRemove"
        Me.lbkRemove.Size = New System.Drawing.Size(68, 13)
        Me.lbkRemove.TabIndex = 73
        Me.lbkRemove.TabStop = True
        Me.lbkRemove.Text = "RemoveCust"
        '
        'lbkSearchCust
        '
        Me.lbkSearchCust.AutoSize = True
        Me.lbkSearchCust.Location = New System.Drawing.Point(569, 580)
        Me.lbkSearchCust.Name = "lbkSearchCust"
        Me.lbkSearchCust.Size = New System.Drawing.Size(85, 13)
        Me.lbkSearchCust.TabIndex = 77
        Me.lbkSearchCust.TabStop = True
        Me.lbkSearchCust.Text = "SearchCustomer"
        '
        'txtCustShortName
        '
        Me.txtCustShortName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustShortName.Location = New System.Drawing.Point(336, 577)
        Me.txtCustShortName.Name = "txtCustShortName"
        Me.txtCustShortName.Size = New System.Drawing.Size(227, 20)
        Me.txtCustShortName.TabIndex = 76
        '
        'dgrCustomerList
        '
        Me.dgrCustomerList.AllowUserToAddRows = False
        Me.dgrCustomerList.AllowUserToDeleteRows = False
        Me.dgrCustomerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrCustomerList.Location = New System.Drawing.Point(336, 360)
        Me.dgrCustomerList.Name = "dgrCustomerList"
        Me.dgrCustomerList.ReadOnly = True
        Me.dgrCustomerList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrCustomerList.Size = New System.Drawing.Size(451, 211)
        Me.dgrCustomerList.TabIndex = 75
        '
        'lbkAddCust
        '
        Me.lbkAddCust.AutoSize = True
        Me.lbkAddCust.Location = New System.Drawing.Point(717, 580)
        Me.lbkAddCust.Name = "lbkAddCust"
        Me.lbkAddCust.Size = New System.Drawing.Size(70, 13)
        Me.lbkAddCust.TabIndex = 74
        Me.lbkAddCust.TabStop = True
        Me.lbkAddCust.Text = "AddCustomer"
        '
        'txtNewGroupName
        '
        Me.txtNewGroupName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNewGroupName.Location = New System.Drawing.Point(336, 334)
        Me.txtNewGroupName.MaxLength = 32
        Me.txtNewGroupName.Name = "txtNewGroupName"
        Me.txtNewGroupName.Size = New System.Drawing.Size(318, 20)
        Me.txtNewGroupName.TabIndex = 78
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(159, 584)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 79
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        Me.lbkSelect.Visible = False
        '
        'frmCustGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(799, 621)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.txtNewGroupName)
        Me.Controls.Add(Me.lbkSearchCust)
        Me.Controls.Add(Me.txtCustShortName)
        Me.Controls.Add(Me.dgrCustomerList)
        Me.Controls.Add(Me.lbkAddCust)
        Me.Controls.Add(Me.lbkRemove)
        Me.Controls.Add(Me.lbkClone)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.dgrCustomers)
        Me.Controls.Add(Me.dgrCustGroup)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.lblCustomer)
        Me.Name = "frmCustGroup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer Group"
        CType(Me.dgrCustGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrCustomers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrCustomerList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblCustomer As Label
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents dgrCustGroup As DataGridView
    Friend WithEvents dgrCustomers As DataGridView
    Friend WithEvents lbkClone As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkRemove As LinkLabel
    Friend WithEvents lbkSearchCust As LinkLabel
    Friend WithEvents txtCustShortName As TextBox
    Friend WithEvents dgrCustomerList As DataGridView
    Friend WithEvents lbkAddCust As LinkLabel
    Friend WithEvents txtNewGroupName As TextBox
    Friend WithEvents RecId As DataGridViewTextBoxColumn
    Friend WithEvents CustShortName As DataGridViewTextBoxColumn
    Friend WithEvents CustId As DataGridViewTextBoxColumn
    Friend WithEvents lbkSelect As LinkLabel
End Class
