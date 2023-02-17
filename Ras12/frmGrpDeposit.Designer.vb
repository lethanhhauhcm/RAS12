<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrpDeposit
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
        Me.cboDocType = New System.Windows.Forms.ComboBox()
        Me.chkPastDOF = New System.Windows.Forms.CheckBox()
        Me.dgrGrpDeposit = New System.Windows.Forms.DataGridView()
        Me.lbkExpire = New System.Windows.Forms.LinkLabel()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.lbkSaveRemark = New System.Windows.Forms.LinkLabel()
        Me.txtPaxName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrGrpDeposit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboDocType
        '
        Me.cboDocType.FormattingEnabled = True
        Me.cboDocType.Items.AddRange(New Object() {"GRP", "MCO"})
        Me.cboDocType.Location = New System.Drawing.Point(93, 23)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(68, 21)
        Me.cboDocType.TabIndex = 0
        '
        'chkPastDOF
        '
        Me.chkPastDOF.AutoSize = True
        Me.chkPastDOF.Location = New System.Drawing.Point(182, 25)
        Me.chkPastDOF.Name = "chkPastDOF"
        Me.chkPastDOF.Size = New System.Drawing.Size(69, 17)
        Me.chkPastDOF.TabIndex = 1
        Me.chkPastDOF.Text = "PastDOF"
        Me.chkPastDOF.UseVisualStyleBackColor = True
        '
        'dgrGrpDeposit
        '
        Me.dgrGrpDeposit.AllowUserToAddRows = False
        Me.dgrGrpDeposit.AllowUserToDeleteRows = False
        Me.dgrGrpDeposit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrGrpDeposit.Location = New System.Drawing.Point(1, 50)
        Me.dgrGrpDeposit.Name = "dgrGrpDeposit"
        Me.dgrGrpDeposit.ReadOnly = True
        Me.dgrGrpDeposit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgrGrpDeposit.Size = New System.Drawing.Size(585, 474)
        Me.dgrGrpDeposit.TabIndex = 2
        '
        'lbkExpire
        '
        Me.lbkExpire.AutoSize = True
        Me.lbkExpire.Location = New System.Drawing.Point(9, 539)
        Me.lbkExpire.Name = "lbkExpire"
        Me.lbkExpire.Size = New System.Drawing.Size(36, 13)
        Me.lbkExpire.TabIndex = 3
        Me.lbkExpire.TabStop = True
        Me.lbkExpire.Text = "Expire"
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(51, 539)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 4
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "EX"})
        Me.cboStatus.Location = New System.Drawing.Point(9, 25)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(68, 21)
        Me.cboStatus.TabIndex = 5
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(131, 532)
        Me.txtRemark.MaxLength = 50
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(305, 20)
        Me.txtRemark.TabIndex = 6
        '
        'lbkSaveRemark
        '
        Me.lbkSaveRemark.AutoSize = True
        Me.lbkSaveRemark.Location = New System.Drawing.Point(442, 539)
        Me.lbkSaveRemark.Name = "lbkSaveRemark"
        Me.lbkSaveRemark.Size = New System.Drawing.Size(69, 13)
        Me.lbkSaveRemark.TabIndex = 7
        Me.lbkSaveRemark.TabStop = True
        Me.lbkSaveRemark.Text = "SaveRemark"
        '
        'txtPaxName
        '
        Me.txtPaxName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPaxName.Location = New System.Drawing.Point(257, 24)
        Me.txtPaxName.Name = "txtPaxName"
        Me.txtPaxName.Size = New System.Drawing.Size(161, 20)
        Me.txtPaxName.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Status"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(90, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "DocType"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(254, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "PaxName"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(453, 28)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 12
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(509, 29)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 13
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'frmGrpDeposit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 561)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPaxName)
        Me.Controls.Add(Me.lbkSaveRemark)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.lbkExpire)
        Me.Controls.Add(Me.dgrGrpDeposit)
        Me.Controls.Add(Me.chkPastDOF)
        Me.Controls.Add(Me.cboDocType)
        Me.Name = "frmGrpDeposit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GrpDeposit"
        CType(Me.dgrGrpDeposit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboDocType As ComboBox
    Friend WithEvents chkPastDOF As CheckBox
    Friend WithEvents dgrGrpDeposit As DataGridView
    Friend WithEvents lbkExpire As LinkLabel
    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents txtRemark As TextBox
    Friend WithEvents lbkSaveRemark As LinkLabel
    Friend WithEvents txtPaxName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkClear As LinkLabel
End Class
