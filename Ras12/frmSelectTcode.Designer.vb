<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSelectTcode
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
        Me.dgrTcodes = New System.Windows.Forms.DataGridView()
        Me.Selected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.txtVendorName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.dtpDOI = New System.Windows.Forms.DateTimePicker()
        Me.MaxEDate = New System.Windows.Forms.Label()
        Me.dgrUNCs = New System.Windows.Forms.DataGridView()
        Me.lbkSaveInvNo = New System.Windows.Forms.LinkLabel()
        Me.lblUncFOP = New System.Windows.Forms.Label()
        Me.txtUncFOP = New System.Windows.Forms.TextBox()
        Me.txtVendorId = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgrTcodes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrUNCs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrTcodes
        '
        Me.dgrTcodes.AllowUserToAddRows = False
        Me.dgrTcodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTcodes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Selected})
        Me.dgrTcodes.Location = New System.Drawing.Point(4, 236)
        Me.dgrTcodes.MultiSelect = False
        Me.dgrTcodes.Name = "dgrTcodes"
        Me.dgrTcodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgrTcodes.Size = New System.Drawing.Size(876, 300)
        Me.dgrTcodes.TabIndex = 26
        '
        'Selected
        '
        Me.Selected.HeaderText = "Selected"
        Me.Selected.Name = "Selected"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(2, 835)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 25
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbkSearch.Location = New System.Drawing.Point(710, 210)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(55, 18)
        Me.lbkSearch.TabIndex = 24
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'txtVendorName
        '
        Me.txtVendorName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVendorName.Location = New System.Drawing.Point(3, 210)
        Me.txtVendorName.Name = "txtVendorName"
        Me.txtVendorName.Size = New System.Drawing.Size(175, 20)
        Me.txtVendorName.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(0, 194)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "VendorName"
        '
        'txtAccountName
        '
        Me.txtAccountName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAccountName.Location = New System.Drawing.Point(241, 210)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.Size = New System.Drawing.Size(303, 20)
        Me.txtAccountName.TabIndex = 30
        '
        'dtpDOI
        '
        Me.dtpDOI.CustomFormat = "dd MMM yy"
        Me.dtpDOI.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOI.Location = New System.Drawing.Point(605, 207)
        Me.dtpDOI.Name = "dtpDOI"
        Me.dtpDOI.Size = New System.Drawing.Size(99, 20)
        Me.dtpDOI.TabIndex = 34
        '
        'MaxEDate
        '
        Me.MaxEDate.AutoSize = True
        Me.MaxEDate.Location = New System.Drawing.Point(602, 182)
        Me.MaxEDate.Name = "MaxEDate"
        Me.MaxEDate.Size = New System.Drawing.Size(57, 13)
        Me.MaxEDate.TabIndex = 35
        Me.MaxEDate.Text = "MaxEDate"
        '
        'dgrUNCs
        '
        Me.dgrUNCs.AllowUserToAddRows = False
        Me.dgrUNCs.AllowUserToDeleteRows = False
        Me.dgrUNCs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrUNCs.Location = New System.Drawing.Point(4, 12)
        Me.dgrUNCs.Name = "dgrUNCs"
        Me.dgrUNCs.ReadOnly = True
        Me.dgrUNCs.Size = New System.Drawing.Size(876, 164)
        Me.dgrUNCs.TabIndex = 36
        '
        'lbkSaveInvNo
        '
        Me.lbkSaveInvNo.AutoSize = True
        Me.lbkSaveInvNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbkSaveInvNo.Location = New System.Drawing.Point(12, 539)
        Me.lbkSaveInvNo.Name = "lbkSaveInvNo"
        Me.lbkSaveInvNo.Size = New System.Drawing.Size(41, 18)
        Me.lbkSaveInvNo.TabIndex = 37
        Me.lbkSaveInvNo.TabStop = True
        Me.lbkSaveInvNo.Text = "Save"
        '
        'lblUncFOP
        '
        Me.lblUncFOP.AutoSize = True
        Me.lblUncFOP.Location = New System.Drawing.Point(826, 186)
        Me.lblUncFOP.Name = "lblUncFOP"
        Me.lblUncFOP.Size = New System.Drawing.Size(54, 13)
        Me.lblUncFOP.TabIndex = 38
        Me.lblUncFOP.Text = "UNC FOP"
        '
        'txtUncFOP
        '
        Me.txtUncFOP.Location = New System.Drawing.Point(829, 207)
        Me.txtUncFOP.Name = "txtUncFOP"
        Me.txtUncFOP.Size = New System.Drawing.Size(51, 20)
        Me.txtUncFOP.TabIndex = 39
        '
        'txtVendorId
        '
        Me.txtVendorId.Enabled = False
        Me.txtVendorId.Location = New System.Drawing.Point(184, 210)
        Me.txtVendorId.Name = "txtVendorId"
        Me.txtVendorId.Size = New System.Drawing.Size(51, 20)
        Me.txtVendorId.TabIndex = 41
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(181, 185)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "VendorId"
        '
        'frmSelectTcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.Controls.Add(Me.txtVendorId)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtUncFOP)
        Me.Controls.Add(Me.lblUncFOP)
        Me.Controls.Add(Me.lbkSaveInvNo)
        Me.Controls.Add(Me.dgrUNCs)
        Me.Controls.Add(Me.dtpDOI)
        Me.Controls.Add(Me.MaxEDate)
        Me.Controls.Add(Me.txtAccountName)
        Me.Controls.Add(Me.dgrTcodes)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.txtVendorName)
        Me.Controls.Add(Me.Label4)
        Me.Name = "frmSelectTcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SelectTcode"
        CType(Me.dgrTcodes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrUNCs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgrTcodes As DataGridView
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents txtVendorName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtAccountName As TextBox
    Friend WithEvents dtpDOI As DateTimePicker
    Friend WithEvents MaxEDate As Label
    Friend WithEvents dgrUNCs As DataGridView
    Friend WithEvents lbkSaveInvNo As LinkLabel
    Friend WithEvents Selected As DataGridViewCheckBoxColumn
    Friend WithEvents lblUncFOP As Label
    Friend WithEvents txtUncFOP As TextBox
    Friend WithEvents txtVendorId As TextBox
    Friend WithEvents Label1 As Label
End Class
