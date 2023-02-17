<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMapBankName
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
        Me.lbkMapBankName = New System.Windows.Forms.LinkLabel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCurrentBankName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.dgrBankNameMap = New System.Windows.Forms.DataGridView()
        Me.txtBankNameByTV = New System.Windows.Forms.TextBox()
        Me.cboBankNameByBank = New System.Windows.Forms.ComboBox()
        Me.cboUsedBy = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboNewBankNameByBank = New System.Windows.Forms.ComboBox()
        CType(Me.dgrBankNameMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkMapBankName
        '
        Me.lbkMapBankName.AutoSize = True
        Me.lbkMapBankName.Location = New System.Drawing.Point(508, 432)
        Me.lbkMapBankName.Name = "lbkMapBankName"
        Me.lbkMapBankName.Size = New System.Drawing.Size(81, 13)
        Me.lbkMapBankName.TabIndex = 55
        Me.lbkMapBankName.TabStop = True
        Me.lbkMapBankName.Text = "MapBankName"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(255, 412)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 13)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "Sugested BankName"
        '
        'txtCurrentBankName
        '
        Me.txtCurrentBankName.Location = New System.Drawing.Point(2, 429)
        Me.txtCurrentBankName.Name = "txtCurrentBankName"
        Me.txtCurrentBankName.ReadOnly = True
        Me.txtCurrentBankName.Size = New System.Drawing.Size(250, 20)
        Me.txtCurrentBankName.TabIndex = 52
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(-1, 412)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 51
        Me.Label4.Text = "CurrentBankName"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(727, 32)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 50
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(680, 32)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 49
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'dgrBankNameMap
        '
        Me.dgrBankNameMap.AllowUserToAddRows = False
        Me.dgrBankNameMap.AllowUserToDeleteRows = False
        Me.dgrBankNameMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrBankNameMap.Location = New System.Drawing.Point(2, 55)
        Me.dgrBankNameMap.Name = "dgrBankNameMap"
        Me.dgrBankNameMap.ReadOnly = True
        Me.dgrBankNameMap.Size = New System.Drawing.Size(784, 354)
        Me.dgrBankNameMap.TabIndex = 48
        '
        'txtBankNameByTV
        '
        Me.txtBankNameByTV.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankNameByTV.Location = New System.Drawing.Point(270, 29)
        Me.txtBankNameByTV.Name = "txtBankNameByTV"
        Me.txtBankNameByTV.Size = New System.Drawing.Size(250, 20)
        Me.txtBankNameByTV.TabIndex = 47
        '
        'cboBankNameByBank
        '
        Me.cboBankNameByBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBankNameByBank.FormattingEnabled = True
        Me.cboBankNameByBank.Items.AddRange(New Object() {"TCB"})
        Me.cboBankNameByBank.Location = New System.Drawing.Point(68, 28)
        Me.cboBankNameByBank.Name = "cboBankNameByBank"
        Me.cboBankNameByBank.Size = New System.Drawing.Size(190, 21)
        Me.cboBankNameByBank.TabIndex = 46
        '
        'cboUsedBy
        '
        Me.cboUsedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsedBy.FormattingEnabled = True
        Me.cboUsedBy.Items.AddRange(New Object() {"TCB"})
        Me.cboUsedBy.Location = New System.Drawing.Point(2, 28)
        Me.cboUsedBy.Name = "cboUsedBy"
        Me.cboUsedBy.Size = New System.Drawing.Size(55, 21)
        Me.cboUsedBy.TabIndex = 45
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(267, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "BankNameByTV"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(65, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "BankNameByBank"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-2, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "UsedBy"
        '
        'cboNewBankNameByBank
        '
        Me.cboNewBankNameByBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNewBankNameByBank.FormattingEnabled = True
        Me.cboNewBankNameByBank.Location = New System.Drawing.Point(258, 428)
        Me.cboNewBankNameByBank.Name = "cboNewBankNameByBank"
        Me.cboNewBankNameByBank.Size = New System.Drawing.Size(244, 21)
        Me.cboNewBankNameByBank.TabIndex = 56
        '
        'frmMapBankName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 461)
        Me.Controls.Add(Me.cboNewBankNameByBank)
        Me.Controls.Add(Me.lbkMapBankName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtCurrentBankName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrBankNameMap)
        Me.Controls.Add(Me.txtBankNameByTV)
        Me.Controls.Add(Me.cboBankNameByBank)
        Me.Controls.Add(Me.cboUsedBy)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmMapBankName"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MapBankName"
        CType(Me.dgrBankNameMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkMapBankName As LinkLabel
    Friend WithEvents Label5 As Label
    Friend WithEvents txtCurrentBankName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents dgrBankNameMap As DataGridView
    Friend WithEvents txtBankNameByTV As TextBox
    Friend WithEvents cboBankNameByBank As ComboBox
    Friend WithEvents cboUsedBy As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cboNewBankNameByBank As ComboBox
End Class
