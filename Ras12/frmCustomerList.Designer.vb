<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerList
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
        Me.dgrCustomer = New System.Windows.Forms.DataGridView()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCustShortName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCustFullName = New System.Windows.Forms.TextBox()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkExpire = New System.Windows.Forms.LinkLabel()
        Me.lbkReActivate = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrCustomer
        '
        Me.dgrCustomer.AllowUserToAddRows = False
        Me.dgrCustomer.AllowUserToDeleteRows = False
        Me.dgrCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrCustomer.Location = New System.Drawing.Point(17, 49)
        Me.dgrCustomer.Name = "dgrCustomer"
        Me.dgrCustomer.ReadOnly = True
        Me.dgrCustomer.Size = New System.Drawing.Size(978, 476)
        Me.dgrCustomer.TabIndex = 39
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(762, 22)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 40
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Status"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "EX"})
        Me.cboStatus.Location = New System.Drawing.Point(22, 22)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(43, 21)
        Me.cboStatus.TabIndex = 42
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(68, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "ShortName"
        '
        'txtCustShortName
        '
        Me.txtCustShortName.Location = New System.Drawing.Point(71, 23)
        Me.txtCustShortName.Name = "txtCustShortName"
        Me.txtCustShortName.Size = New System.Drawing.Size(184, 20)
        Me.txtCustShortName.TabIndex = 44
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(255, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "FullName"
        '
        'txtCustFullName
        '
        Me.txtCustFullName.Location = New System.Drawing.Point(258, 23)
        Me.txtCustFullName.Name = "txtCustFullName"
        Me.txtCustFullName.Size = New System.Drawing.Size(416, 20)
        Me.txtCustFullName.TabIndex = 46
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(809, 22)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 54
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(19, 539)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 55
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(66, 539)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 56
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkExpire
        '
        Me.lbkExpire.AutoSize = True
        Me.lbkExpire.Location = New System.Drawing.Point(113, 539)
        Me.lbkExpire.Name = "lbkExpire"
        Me.lbkExpire.Size = New System.Drawing.Size(36, 13)
        Me.lbkExpire.TabIndex = 57
        Me.lbkExpire.TabStop = True
        Me.lbkExpire.Text = "Expire"
        '
        'lbkReActivate
        '
        Me.lbkReActivate.AutoSize = True
        Me.lbkReActivate.Location = New System.Drawing.Point(155, 539)
        Me.lbkReActivate.Name = "lbkReActivate"
        Me.lbkReActivate.Size = New System.Drawing.Size(60, 13)
        Me.lbkReActivate.TabIndex = 61
        Me.lbkReActivate.TabStop = True
        Me.lbkReActivate.Text = "ReActivate"
        '
        'frmCustomerList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 561)
        Me.Controls.Add(Me.lbkReActivate)
        Me.Controls.Add(Me.lbkExpire)
        Me.Controls.Add(Me.lbkEdit)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.txtCustFullName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCustShortName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrCustomer)
        Me.Name = "frmCustomerList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CustomerList"
        CType(Me.dgrCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrCustomer As DataGridView
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtCustShortName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtCustFullName As TextBox
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkExpire As LinkLabel
    Friend WithEvents lbkReActivate As LinkLabel
End Class
