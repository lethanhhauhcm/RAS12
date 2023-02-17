<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserManagement
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Check to Prohibit")
        Me.CmbUsername = New System.Windows.Forms.ComboBox()
        Me.CmdResetPSW = New System.Windows.Forms.Button()
        Me.LstChannelAccess = New System.Windows.Forms.CheckedListBox()
        Me.GrpAccess = New System.Windows.Forms.GroupBox()
        Me.chkAccessTVS = New System.Windows.Forms.CheckBox()
        Me.chkAccessGSA = New System.Windows.Forms.CheckBox()
        Me.LstAL = New System.Windows.Forms.CheckedListBox()
        Me.ChckAccessYY = New System.Windows.Forms.CheckBox()
        Me.CmdChange = New System.Windows.Forms.Button()
        Me.TreeRight = New System.Windows.Forms.TreeView()
        Me.CmbTemplate = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblUserManagementUname = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtUserManagementSIcode = New System.Windows.Forms.TextBox()
        Me.CmdUserManagementCreate = New System.Windows.Forms.Button()
        Me.CmdUserManagementDelete = New System.Windows.Forms.Button()
        Me.CmbLocation = New System.Windows.Forms.ComboBox()
        Me.CmbCounter = New System.Windows.Forms.ComboBox()
        Me.LblChangeCounter = New System.Windows.Forms.LinkLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ChkHAN = New System.Windows.Forms.CheckBox()
        Me.lbkCcAccess = New System.Windows.Forms.LinkLabel()
        Me.lbkExtraRights = New System.Windows.Forms.LinkLabel()
        Me.grpCounter = New System.Windows.Forms.GroupBox()
        Me.chkGSA = New System.Windows.Forms.CheckBox()
        Me.chkCWT = New System.Windows.Forms.CheckBox()
        Me.chkNonAir = New System.Windows.Forms.CheckBox()
        Me.chkTVS = New System.Windows.Forms.CheckBox()
        Me.chkALL = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbkUpdateLocation = New System.Windows.Forms.LinkLabel()
        Me.txtStaffId = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GrpAccess.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpCounter.SuspendLayout()
        Me.SuspendLayout()
        '
        'CmbUsername
        '
        Me.CmbUsername.FormattingEnabled = True
        Me.CmbUsername.Location = New System.Drawing.Point(47, 32)
        Me.CmbUsername.Name = "CmbUsername"
        Me.CmbUsername.Size = New System.Drawing.Size(366, 21)
        Me.CmbUsername.TabIndex = 1
        '
        'CmdResetPSW
        '
        Me.CmdResetPSW.Enabled = False
        Me.CmdResetPSW.Location = New System.Drawing.Point(756, 0)
        Me.CmdResetPSW.Name = "CmdResetPSW"
        Me.CmdResetPSW.Size = New System.Drawing.Size(72, 26)
        Me.CmdResetPSW.TabIndex = 21
        Me.CmdResetPSW.Text = "Reset PSW"
        Me.CmdResetPSW.UseVisualStyleBackColor = True
        '
        'LstChannelAccess
        '
        Me.LstChannelAccess.CheckOnClick = True
        Me.LstChannelAccess.ColumnWidth = 50
        Me.LstChannelAccess.FormattingEnabled = True
        Me.LstChannelAccess.Location = New System.Drawing.Point(6, 26)
        Me.LstChannelAccess.MultiColumn = True
        Me.LstChannelAccess.Name = "LstChannelAccess"
        Me.LstChannelAccess.Size = New System.Drawing.Size(161, 49)
        Me.LstChannelAccess.TabIndex = 0
        '
        'GrpAccess
        '
        Me.GrpAccess.Controls.Add(Me.chkAccessTVS)
        Me.GrpAccess.Controls.Add(Me.chkAccessGSA)
        Me.GrpAccess.Controls.Add(Me.LstAL)
        Me.GrpAccess.Controls.Add(Me.ChckAccessYY)
        Me.GrpAccess.Location = New System.Drawing.Point(494, 73)
        Me.GrpAccess.Name = "GrpAccess"
        Me.GrpAccess.Size = New System.Drawing.Size(334, 81)
        Me.GrpAccess.TabIndex = 15
        Me.GrpAccess.TabStop = False
        Me.GrpAccess.Text = "AL Access"
        '
        'chkAccessTVS
        '
        Me.chkAccessTVS.AutoSize = True
        Me.chkAccessTVS.Location = New System.Drawing.Point(283, 43)
        Me.chkAccessTVS.Name = "chkAccessTVS"
        Me.chkAccessTVS.Size = New System.Drawing.Size(47, 17)
        Me.chkAccessTVS.TabIndex = 19
        Me.chkAccessTVS.Text = "TVS"
        Me.chkAccessTVS.UseVisualStyleBackColor = True
        '
        'chkAccessGSA
        '
        Me.chkAccessGSA.AutoSize = True
        Me.chkAccessGSA.Location = New System.Drawing.Point(283, 60)
        Me.chkAccessGSA.Name = "chkAccessGSA"
        Me.chkAccessGSA.Size = New System.Drawing.Size(48, 17)
        Me.chkAccessGSA.TabIndex = 19
        Me.chkAccessGSA.Text = "GSA"
        Me.chkAccessGSA.UseVisualStyleBackColor = True
        '
        'LstAL
        '
        Me.LstAL.CheckOnClick = True
        Me.LstAL.ColumnWidth = 50
        Me.LstAL.FormattingEnabled = True
        Me.LstAL.Location = New System.Drawing.Point(4, 26)
        Me.LstAL.MultiColumn = True
        Me.LstAL.Name = "LstAL"
        Me.LstAL.Size = New System.Drawing.Size(273, 49)
        Me.LstAL.TabIndex = 15
        '
        'ChckAccessYY
        '
        Me.ChckAccessYY.AutoSize = True
        Me.ChckAccessYY.Checked = True
        Me.ChckAccessYY.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChckAccessYY.Location = New System.Drawing.Point(283, 25)
        Me.ChckAccessYY.Name = "ChckAccessYY"
        Me.ChckAccessYY.Size = New System.Drawing.Size(40, 17)
        Me.ChckAccessYY.TabIndex = 14
        Me.ChckAccessYY.Text = "YY"
        Me.ChckAccessYY.UseVisualStyleBackColor = True
        '
        'CmdChange
        '
        Me.CmdChange.Enabled = False
        Me.CmdChange.Location = New System.Drawing.Point(674, 0)
        Me.CmdChange.Name = "CmdChange"
        Me.CmdChange.Size = New System.Drawing.Size(82, 26)
        Me.CmdChange.TabIndex = 13
        Me.CmdChange.Text = "ChangeRight"
        Me.CmdChange.UseVisualStyleBackColor = True
        '
        'TreeRight
        '
        Me.TreeRight.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TreeRight.CheckBoxes = True
        Me.TreeRight.Location = New System.Drawing.Point(2, 73)
        Me.TreeRight.Name = "TreeRight"
        TreeNode1.Name = "TreeRoot"
        TreeNode1.Text = "Check to Prohibit"
        Me.TreeRight.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.TreeRight.Size = New System.Drawing.Size(313, 433)
        Me.TreeRight.TabIndex = 3
        '
        'CmbTemplate
        '
        Me.CmbTemplate.FormattingEnabled = True
        Me.CmbTemplate.Items.AddRange(New Object() {"", "A***", "C***", "D***", "M***", "NH**", "NA**", "CS**", "S***", "T***", "UA**"})
        Me.CmbTemplate.Location = New System.Drawing.Point(313, 3)
        Me.CmbTemplate.Name = "CmbTemplate"
        Me.CmbTemplate.Size = New System.Drawing.Size(46, 21)
        Me.CmbTemplate.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(271, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Group"
        '
        'lblUserManagementUname
        '
        Me.lblUserManagementUname.AutoSize = True
        Me.lblUserManagementUname.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserManagementUname.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUserManagementUname.Location = New System.Drawing.Point(2, 32)
        Me.lblUserManagementUname.Name = "lblUserManagementUname"
        Me.lblUserManagementUname.Size = New System.Drawing.Size(39, 13)
        Me.lblUserManagementUname.TabIndex = 2
        Me.lblUserManagementUname.Text = "Name"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(98, 7)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 13)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "SI Code"
        '
        'txtUserManagementSIcode
        '
        Me.txtUserManagementSIcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUserManagementSIcode.Location = New System.Drawing.Point(143, 4)
        Me.txtUserManagementSIcode.MaxLength = 4
        Me.txtUserManagementSIcode.Name = "txtUserManagementSIcode"
        Me.txtUserManagementSIcode.Size = New System.Drawing.Size(41, 20)
        Me.txtUserManagementSIcode.TabIndex = 1
        '
        'CmdUserManagementCreate
        '
        Me.CmdUserManagementCreate.Enabled = False
        Me.CmdUserManagementCreate.Location = New System.Drawing.Point(629, 0)
        Me.CmdUserManagementCreate.Name = "CmdUserManagementCreate"
        Me.CmdUserManagementCreate.Size = New System.Drawing.Size(46, 26)
        Me.CmdUserManagementCreate.TabIndex = 5
        Me.CmdUserManagementCreate.Text = "Create"
        Me.CmdUserManagementCreate.UseVisualStyleBackColor = True
        '
        'CmdUserManagementDelete
        '
        Me.CmdUserManagementDelete.Enabled = False
        Me.CmdUserManagementDelete.Location = New System.Drawing.Point(582, 0)
        Me.CmdUserManagementDelete.Name = "CmdUserManagementDelete"
        Me.CmdUserManagementDelete.Size = New System.Drawing.Size(46, 26)
        Me.CmdUserManagementDelete.TabIndex = 3
        Me.CmdUserManagementDelete.Text = "Delete"
        Me.CmdUserManagementDelete.UseVisualStyleBackColor = True
        '
        'CmbLocation
        '
        Me.CmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLocation.FormattingEnabled = True
        Me.CmbLocation.Location = New System.Drawing.Point(419, 6)
        Me.CmbLocation.Name = "CmbLocation"
        Me.CmbLocation.Size = New System.Drawing.Size(51, 21)
        Me.CmbLocation.TabIndex = 22
        '
        'CmbCounter
        '
        Me.CmbCounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCounter.FormattingEnabled = True
        Me.CmbCounter.Location = New System.Drawing.Point(774, 203)
        Me.CmbCounter.Name = "CmbCounter"
        Me.CmbCounter.Size = New System.Drawing.Size(51, 21)
        Me.CmbCounter.TabIndex = 22
        '
        'LblChangeCounter
        '
        Me.LblChangeCounter.AutoSize = True
        Me.LblChangeCounter.Enabled = False
        Me.LblChangeCounter.Location = New System.Drawing.Point(750, 47)
        Me.LblChangeCounter.Name = "LblChangeCounter"
        Me.LblChangeCounter.Size = New System.Drawing.Size(81, 13)
        Me.LblChangeCounter.TabIndex = 23
        Me.LblChangeCounter.TabStop = True
        Me.LblChangeCounter.Text = "ChangeCounter"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LstChannelAccess)
        Me.GroupBox1.Location = New System.Drawing.Point(320, 73)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(171, 81)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Channel Access"
        '
        'ChkHAN
        '
        Me.ChkHAN.AutoSize = True
        Me.ChkHAN.Location = New System.Drawing.Point(2, 510)
        Me.ChkHAN.Name = "ChkHAN"
        Me.ChkHAN.Size = New System.Drawing.Size(49, 17)
        Me.ChkHAN.TabIndex = 31
        Me.ChkHAN.Text = "HAN"
        Me.ChkHAN.UseVisualStyleBackColor = True
        '
        'lbkCcAccess
        '
        Me.lbkCcAccess.AutoSize = True
        Me.lbkCcAccess.Location = New System.Drawing.Point(736, 511)
        Me.lbkCcAccess.Name = "lbkCcAccess"
        Me.lbkCcAccess.Size = New System.Drawing.Size(55, 13)
        Me.lbkCcAccess.TabIndex = 32
        Me.lbkCcAccess.TabStop = True
        Me.lbkCcAccess.Text = "CcAccess"
        '
        'lbkExtraRights
        '
        Me.lbkExtraRights.AutoSize = True
        Me.lbkExtraRights.Location = New System.Drawing.Point(652, 511)
        Me.lbkExtraRights.Name = "lbkExtraRights"
        Me.lbkExtraRights.Size = New System.Drawing.Size(61, 13)
        Me.lbkExtraRights.TabIndex = 33
        Me.lbkExtraRights.TabStop = True
        Me.lbkExtraRights.Text = "ExtraRights"
        '
        'grpCounter
        '
        Me.grpCounter.Controls.Add(Me.chkGSA)
        Me.grpCounter.Controls.Add(Me.chkCWT)
        Me.grpCounter.Controls.Add(Me.chkNonAir)
        Me.grpCounter.Controls.Add(Me.chkTVS)
        Me.grpCounter.Controls.Add(Me.chkALL)
        Me.grpCounter.Location = New System.Drawing.Point(494, 29)
        Me.grpCounter.Name = "grpCounter"
        Me.grpCounter.Size = New System.Drawing.Size(250, 49)
        Me.grpCounter.TabIndex = 34
        Me.grpCounter.TabStop = False
        Me.grpCounter.Text = "Counter"
        '
        'chkGSA
        '
        Me.chkGSA.AutoSize = True
        Me.chkGSA.Location = New System.Drawing.Point(205, 17)
        Me.chkGSA.Name = "chkGSA"
        Me.chkGSA.Size = New System.Drawing.Size(48, 17)
        Me.chkGSA.TabIndex = 19
        Me.chkGSA.Text = "GSA"
        Me.chkGSA.UseVisualStyleBackColor = True
        '
        'chkCWT
        '
        Me.chkCWT.AutoSize = True
        Me.chkCWT.Location = New System.Drawing.Point(154, 17)
        Me.chkCWT.Name = "chkCWT"
        Me.chkCWT.Size = New System.Drawing.Size(51, 17)
        Me.chkCWT.TabIndex = 18
        Me.chkCWT.Text = "CWT"
        Me.chkCWT.UseVisualStyleBackColor = True
        '
        'chkNonAir
        '
        Me.chkNonAir.AutoSize = True
        Me.chkNonAir.Location = New System.Drawing.Point(110, 17)
        Me.chkNonAir.Name = "chkNonAir"
        Me.chkNonAir.Size = New System.Drawing.Size(44, 17)
        Me.chkNonAir.TabIndex = 17
        Me.chkNonAir.Text = "N-A"
        Me.chkNonAir.UseVisualStyleBackColor = True
        '
        'chkTVS
        '
        Me.chkTVS.AutoSize = True
        Me.chkTVS.Location = New System.Drawing.Point(57, 17)
        Me.chkTVS.Name = "chkTVS"
        Me.chkTVS.Size = New System.Drawing.Size(47, 17)
        Me.chkTVS.TabIndex = 16
        Me.chkTVS.Text = "TVS"
        Me.chkTVS.UseVisualStyleBackColor = True
        '
        'chkALL
        '
        Me.chkALL.AutoSize = True
        Me.chkALL.Location = New System.Drawing.Point(6, 17)
        Me.chkALL.Name = "chkALL"
        Me.chkALL.Size = New System.Drawing.Size(45, 17)
        Me.chkALL.TabIndex = 15
        Me.chkALL.Text = "ALL"
        Me.chkALL.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(365, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Location"
        '
        'lbkUpdateLocation
        '
        Me.lbkUpdateLocation.AutoSize = True
        Me.lbkUpdateLocation.Enabled = False
        Me.lbkUpdateLocation.Location = New System.Drawing.Point(491, 6)
        Me.lbkUpdateLocation.Name = "lbkUpdateLocation"
        Me.lbkUpdateLocation.Size = New System.Drawing.Size(83, 13)
        Me.lbkUpdateLocation.TabIndex = 36
        Me.lbkUpdateLocation.TabStop = True
        Me.lbkUpdateLocation.Text = "UpdateLocation"
        '
        'txtStaffId
        '
        Me.txtStaffId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStaffId.Location = New System.Drawing.Point(51, 4)
        Me.txtStaffId.MaxLength = 4
        Me.txtStaffId.Name = "txtStaffId"
        Me.txtStaffId.Size = New System.Drawing.Size(41, 20)
        Me.txtStaffId.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "StaffId"
        '
        'UserManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(829, 534)
        Me.Controls.Add(Me.txtStaffId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbkUpdateLocation)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grpCounter)
        Me.Controls.Add(Me.lbkExtraRights)
        Me.Controls.Add(Me.lbkCcAccess)
        Me.Controls.Add(Me.LblChangeCounter)
        Me.Controls.Add(Me.ChkHAN)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CmbCounter)
        Me.Controls.Add(Me.CmbLocation)
        Me.Controls.Add(Me.CmbUsername)
        Me.Controls.Add(Me.GrpAccess)
        Me.Controls.Add(Me.CmdResetPSW)
        Me.Controls.Add(Me.TreeRight)
        Me.Controls.Add(Me.CmdChange)
        Me.Controls.Add(Me.CmdUserManagementDelete)
        Me.Controls.Add(Me.CmdUserManagementCreate)
        Me.Controls.Add(Me.txtUserManagementSIcode)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblUserManagementUname)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.CmbTemplate)
        Me.Location = New System.Drawing.Point(0, 56)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UserManagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "TransViet Airlines :: RAS :. UserManagement"
        Me.GrpAccess.ResumeLayout(False)
        Me.GrpAccess.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.grpCounter.ResumeLayout(False)
        Me.grpCounter.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TreeRight As System.Windows.Forms.TreeView
    Friend WithEvents CmbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblUserManagementUname As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtUserManagementSIcode As System.Windows.Forms.TextBox
    Friend WithEvents CmdUserManagementCreate As System.Windows.Forms.Button
    Friend WithEvents CmdUserManagementDelete As System.Windows.Forms.Button
    Friend WithEvents CmdChange As System.Windows.Forms.Button
    Friend WithEvents GrpAccess As System.Windows.Forms.GroupBox
    Friend WithEvents LstAL As System.Windows.Forms.CheckedListBox
    Friend WithEvents ChckAccessYY As System.Windows.Forms.CheckBox
    Friend WithEvents chkAccessTVS As System.Windows.Forms.CheckBox
    Friend WithEvents chkAccessGSA As System.Windows.Forms.CheckBox
    Friend WithEvents LstChannelAccess As System.Windows.Forms.CheckedListBox
    Friend WithEvents CmdResetPSW As System.Windows.Forms.Button
    Friend WithEvents CmbUsername As System.Windows.Forms.ComboBox
    Friend WithEvents CmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents CmbCounter As System.Windows.Forms.ComboBox
    Friend WithEvents LblChangeCounter As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ChkHAN As System.Windows.Forms.CheckBox
    Friend WithEvents lbkCcAccess As System.Windows.Forms.LinkLabel
    Friend WithEvents lbkExtraRights As LinkLabel
    Friend WithEvents grpCounter As GroupBox
    Friend WithEvents chkGSA As CheckBox
    Friend WithEvents chkCWT As CheckBox
    Friend WithEvents chkNonAir As CheckBox
    Friend WithEvents chkTVS As CheckBox
    Friend WithEvents chkALL As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lbkUpdateLocation As LinkLabel
    Friend WithEvents txtStaffId As TextBox
    Friend WithEvents Label2 As Label
End Class
