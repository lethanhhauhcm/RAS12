<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMapBankProvince
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboUsedBy = New System.Windows.Forms.ComboBox()
        Me.cboProvince = New System.Windows.Forms.ComboBox()
        Me.txtBankAddress = New System.Windows.Forms.TextBox()
        Me.dgrProvinceMap = New System.Windows.Forms.DataGridView()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.txtCurrentProvince = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNewProvince = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbkMapProvince = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrProvinceMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "UsedBy"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(67, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Province"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(269, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "BankAddress"
        '
        'cboUsedBy
        '
        Me.cboUsedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsedBy.FormattingEnabled = True
        Me.cboUsedBy.Items.AddRange(New Object() {"TCB"})
        Me.cboUsedBy.Location = New System.Drawing.Point(4, 16)
        Me.cboUsedBy.Name = "cboUsedBy"
        Me.cboUsedBy.Size = New System.Drawing.Size(55, 21)
        Me.cboUsedBy.TabIndex = 3
        '
        'cboProvince
        '
        Me.cboProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProvince.FormattingEnabled = True
        Me.cboProvince.Items.AddRange(New Object() {"TCB"})
        Me.cboProvince.Location = New System.Drawing.Point(70, 16)
        Me.cboProvince.Name = "cboProvince"
        Me.cboProvince.Size = New System.Drawing.Size(190, 21)
        Me.cboProvince.TabIndex = 4
        '
        'txtBankAddress
        '
        Me.txtBankAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankAddress.Location = New System.Drawing.Point(272, 17)
        Me.txtBankAddress.Name = "txtBankAddress"
        Me.txtBankAddress.Size = New System.Drawing.Size(250, 20)
        Me.txtBankAddress.TabIndex = 5
        '
        'dgrProvinceMap
        '
        Me.dgrProvinceMap.AllowUserToAddRows = False
        Me.dgrProvinceMap.AllowUserToDeleteRows = False
        Me.dgrProvinceMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrProvinceMap.Location = New System.Drawing.Point(4, 43)
        Me.dgrProvinceMap.Name = "dgrProvinceMap"
        Me.dgrProvinceMap.ReadOnly = True
        Me.dgrProvinceMap.Size = New System.Drawing.Size(784, 354)
        Me.dgrProvinceMap.TabIndex = 6
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(757, 20)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 33
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(710, 20)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 32
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'txtCurrentProvince
        '
        Me.txtCurrentProvince.Location = New System.Drawing.Point(4, 417)
        Me.txtCurrentProvince.Name = "txtCurrentProvince"
        Me.txtCurrentProvince.ReadOnly = True
        Me.txtCurrentProvince.Size = New System.Drawing.Size(250, 20)
        Me.txtCurrentProvince.TabIndex = 35
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1, 400)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "CurrentProvince"
        '
        'txtNewProvince
        '
        Me.txtNewProvince.Location = New System.Drawing.Point(260, 417)
        Me.txtNewProvince.Name = "txtNewProvince"
        Me.txtNewProvince.ReadOnly = True
        Me.txtNewProvince.Size = New System.Drawing.Size(250, 20)
        Me.txtNewProvince.TabIndex = 37
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(257, 400)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 13)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Sugested Province"
        '
        'lbkMapProvince
        '
        Me.lbkMapProvince.AutoSize = True
        Me.lbkMapProvince.Location = New System.Drawing.Point(516, 420)
        Me.lbkMapProvince.Name = "lbkMapProvince"
        Me.lbkMapProvince.Size = New System.Drawing.Size(70, 13)
        Me.lbkMapProvince.TabIndex = 41
        Me.lbkMapProvince.TabStop = True
        Me.lbkMapProvince.Text = "MapProvince"
        '
        'frmMapBankProvince
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.lbkMapProvince)
        Me.Controls.Add(Me.txtNewProvince)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtCurrentProvince)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrProvinceMap)
        Me.Controls.Add(Me.txtBankAddress)
        Me.Controls.Add(Me.cboProvince)
        Me.Controls.Add(Me.cboUsedBy)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmMapBankProvince"
        Me.Text = "frmMapBankProvince"
        CType(Me.dgrProvinceMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cboUsedBy As ComboBox
    Friend WithEvents cboProvince As ComboBox
    Friend WithEvents txtBankAddress As TextBox
    Friend WithEvents dgrProvinceMap As DataGridView
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents txtCurrentProvince As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtNewProvince As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents lbkMapProvince As LinkLabel
End Class
