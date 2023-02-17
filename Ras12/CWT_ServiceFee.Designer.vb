<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CWT_ServiceFee
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CmbCust = New System.Windows.Forms.ComboBox()
        Me.CmbService = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GridSF = New System.Windows.Forms.DataGridView()
        Me.LblDelete = New System.Windows.Forms.LinkLabel()
        Me.TxtValidFrom = New System.Windows.Forms.DateTimePicker()
        Me.CmbAL = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CmbRtg = New System.Windows.Forms.ComboBox()
        Me.lblRtg = New System.Windows.Forms.Label()
        Me.lbkDefineRtgType = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboCustGroup = New System.Windows.Forms.ComboBox()
        Me.lbkClone = New System.Windows.Forms.LinkLabel()
        Me.chkValidToday = New System.Windows.Forms.CheckBox()
        Me.dtpNewValidThru = New System.Windows.Forms.DateTimePicker()
        Me.lbkNewValidThru = New System.Windows.Forms.LinkLabel()
        CType(Me.GridSF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmbCust
        '
        Me.CmbCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCust.FormattingEnabled = True
        Me.CmbCust.Location = New System.Drawing.Point(289, 12)
        Me.CmbCust.Name = "CmbCust"
        Me.CmbCust.Size = New System.Drawing.Size(175, 21)
        Me.CmbCust.TabIndex = 0
        '
        'CmbService
        '
        Me.CmbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbService.FormattingEnabled = True
        Me.CmbService.Items.AddRange(New Object() {"AIR", "HTL", "CAR", "INS", "VSA", "AHC"})
        Me.CmbService.Location = New System.Drawing.Point(57, 38)
        Me.CmbService.Name = "CmbService"
        Me.CmbService.Size = New System.Drawing.Size(70, 21)
        Me.CmbService.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(206, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Customer"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(709, 17)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 13
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Service"
        '
        'GridSF
        '
        Me.GridSF.AllowUserToAddRows = False
        Me.GridSF.AllowUserToDeleteRows = False
        Me.GridSF.BackgroundColor = System.Drawing.Color.Honeydew
        Me.GridSF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridSF.Location = New System.Drawing.Point(2, 65)
        Me.GridSF.Name = "GridSF"
        Me.GridSF.RowHeadersVisible = False
        Me.GridSF.Size = New System.Drawing.Size(994, 410)
        Me.GridSF.TabIndex = 7
        '
        'LblDelete
        '
        Me.LblDelete.AutoSize = True
        Me.LblDelete.Location = New System.Drawing.Point(128, 478)
        Me.LblDelete.Name = "LblDelete"
        Me.LblDelete.Size = New System.Drawing.Size(38, 13)
        Me.LblDelete.TabIndex = 8
        Me.LblDelete.TabStop = True
        Me.LblDelete.Text = "Delete"
        Me.LblDelete.Visible = False
        '
        'TxtValidFrom
        '
        Me.TxtValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.TxtValidFrom.Location = New System.Drawing.Point(592, 13)
        Me.TxtValidFrom.Name = "TxtValidFrom"
        Me.TxtValidFrom.Size = New System.Drawing.Size(115, 20)
        Me.TxtValidFrom.TabIndex = 10
        Me.TxtValidFrom.Visible = False
        '
        'CmbAL
        '
        Me.CmbAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbAL.FormattingEnabled = True
        Me.CmbAL.Items.AddRange(New Object() {"LCC", "FSC"})
        Me.CmbAL.Location = New System.Drawing.Point(289, 38)
        Me.CmbAL.Name = "CmbAL"
        Me.CmbAL.Size = New System.Drawing.Size(53, 21)
        Me.CmbAL.TabIndex = 14
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(263, 41)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(20, 13)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "AL"
        '
        'CmbRtg
        '
        Me.CmbRtg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbRtg.FormattingEnabled = True
        Me.CmbRtg.Items.AddRange(New Object() {"DOM", "REG", "REG2", "INTL", "INT2"})
        Me.CmbRtg.Location = New System.Drawing.Point(179, 38)
        Me.CmbRtg.Name = "CmbRtg"
        Me.CmbRtg.Size = New System.Drawing.Size(78, 21)
        Me.CmbRtg.TabIndex = 15
        '
        'lblRtg
        '
        Me.lblRtg.AutoSize = True
        Me.lblRtg.Location = New System.Drawing.Point(147, 41)
        Me.lblRtg.Name = "lblRtg"
        Me.lblRtg.Size = New System.Drawing.Size(30, 13)
        Me.lblRtg.TabIndex = 4
        Me.lblRtg.Text = "RTG"
        '
        'lbkDefineRtgType
        '
        Me.lbkDefineRtgType.AutoSize = True
        Me.lbkDefineRtgType.Location = New System.Drawing.Point(671, 478)
        Me.lbkDefineRtgType.Name = "lbkDefineRtgType"
        Me.lbkDefineRtgType.Size = New System.Drawing.Size(79, 13)
        Me.lbkDefineRtgType.TabIndex = 19
        Me.lbkDefineRtgType.TabStop = True
        Me.lbkDefineRtgType.Text = "DefineRtgType"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(3, 478)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 20
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(709, 41)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 21
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(0, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "CustGrp"
        '
        'cboCustGroup
        '
        Me.cboCustGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustGroup.FormattingEnabled = True
        Me.cboCustGroup.Location = New System.Drawing.Point(57, 11)
        Me.cboCustGroup.Name = "cboCustGroup"
        Me.cboCustGroup.Size = New System.Drawing.Size(143, 21)
        Me.cboCustGroup.TabIndex = 22
        '
        'lbkClone
        '
        Me.lbkClone.AutoSize = True
        Me.lbkClone.Location = New System.Drawing.Point(54, 478)
        Me.lbkClone.Name = "lbkClone"
        Me.lbkClone.Size = New System.Drawing.Size(34, 13)
        Me.lbkClone.TabIndex = 24
        Me.lbkClone.TabStop = True
        Me.lbkClone.Text = "Clone"
        '
        'chkValidToday
        '
        Me.chkValidToday.AutoSize = True
        Me.chkValidToday.Checked = True
        Me.chkValidToday.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkValidToday.Location = New System.Drawing.Point(356, 42)
        Me.chkValidToday.Name = "chkValidToday"
        Me.chkValidToday.Size = New System.Drawing.Size(79, 17)
        Me.chkValidToday.TabIndex = 25
        Me.chkValidToday.Text = "ValidToday"
        Me.chkValidToday.UseVisualStyleBackColor = True
        '
        'dtpNewValidThru
        '
        Me.dtpNewValidThru.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpNewValidThru.Location = New System.Drawing.Point(277, 481)
        Me.dtpNewValidThru.Name = "dtpNewValidThru"
        Me.dtpNewValidThru.Size = New System.Drawing.Size(115, 20)
        Me.dtpNewValidThru.TabIndex = 26
        Me.dtpNewValidThru.Visible = False
        '
        'lbkNewValidThru
        '
        Me.lbkNewValidThru.AutoSize = True
        Me.lbkNewValidThru.Location = New System.Drawing.Point(410, 487)
        Me.lbkNewValidThru.Name = "lbkNewValidThru"
        Me.lbkNewValidThru.Size = New System.Drawing.Size(90, 13)
        Me.lbkNewValidThru.TabIndex = 27
        Me.lbkNewValidThru.TabStop = True
        Me.lbkNewValidThru.Text = "SetNewValidThru"
        '
        'CWT_ServiceFee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.lbkNewValidThru)
        Me.Controls.Add(Me.dtpNewValidThru)
        Me.Controls.Add(Me.chkValidToday)
        Me.Controls.Add(Me.lbkClone)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboCustGroup)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.lbkDefineRtgType)
        Me.Controls.Add(Me.CmbRtg)
        Me.Controls.Add(Me.CmbAL)
        Me.Controls.Add(Me.TxtValidFrom)
        Me.Controls.Add(Me.LblDelete)
        Me.Controls.Add(Me.GridSF)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.lblRtg)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CmbService)
        Me.Controls.Add(Me.CmbCust)
        Me.Location = New System.Drawing.Point(0, 50)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CWT_ServiceFee"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "TransViet Corporate Travel Service :: RAS :. Service Fee"
        CType(Me.GridSF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CmbCust As System.Windows.Forms.ComboBox
    Friend WithEvents CmbService As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbkSearch As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GridSF As System.Windows.Forms.DataGridView
    Friend WithEvents LblDelete As System.Windows.Forms.LinkLabel
    Friend WithEvents TxtValidFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents CmbAL As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents CmbRtg As System.Windows.Forms.ComboBox
    Friend WithEvents lblRtg As System.Windows.Forms.Label
    Friend WithEvents lbkDefineRtgType As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents Label3 As Label
    Friend WithEvents cboCustGroup As ComboBox
    Friend WithEvents lbkClone As LinkLabel
    Friend WithEvents chkValidToday As CheckBox
    Friend WithEvents dtpNewValidThru As DateTimePicker
    Friend WithEvents lbkNewValidThru As LinkLabel
End Class
