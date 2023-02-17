<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVnCorpTkt
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
        Me.components = New System.ComponentModel.Container()
        Me.lbkLogin = New System.Windows.Forms.LinkLabel()
        Me.lbkCheck = New System.Windows.Forms.LinkLabel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lbkImport = New System.Windows.Forms.LinkLabel()
        Me.cboYear = New System.Windows.Forms.ComboBox()
        Me.cboQuarter = New System.Windows.Forms.ComboBox()
        Me.wb = New System.Windows.Forms.WebBrowser()
        Me.lbkAddTkts = New System.Windows.Forms.LinkLabel()
        Me.lbkSelectAddButton = New System.Windows.Forms.LinkLabel()
        Me.dgrTickets = New System.Windows.Forms.DataGridView()
        Me.cboDomInt = New System.Windows.Forms.ComboBox()
        Me.cboVnCorpId = New System.Windows.Forms.ComboBox()
        Me.lblRecords = New System.Windows.Forms.Label()
        Me.lbkRefreshData = New System.Windows.Forms.LinkLabel()
        Me.lbkChangeCorp = New System.Windows.Forms.LinkLabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lbkChange2Dom = New System.Windows.Forms.LinkLabel()
        Me.lbkChange2INT = New System.Windows.Forms.LinkLabel()
        Me.lbkImportDirectory = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrTickets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkLogin
        '
        Me.lbkLogin.AutoSize = True
        Me.lbkLogin.Location = New System.Drawing.Point(539, 9)
        Me.lbkLogin.Name = "lbkLogin"
        Me.lbkLogin.Size = New System.Drawing.Size(33, 13)
        Me.lbkLogin.TabIndex = 0
        Me.lbkLogin.TabStop = True
        Me.lbkLogin.Text = "Login"
        '
        'lbkCheck
        '
        Me.lbkCheck.AutoSize = True
        Me.lbkCheck.Location = New System.Drawing.Point(54, 4)
        Me.lbkCheck.Name = "lbkCheck"
        Me.lbkCheck.Size = New System.Drawing.Size(38, 13)
        Me.lbkCheck.TabIndex = 1
        Me.lbkCheck.TabStop = True
        Me.lbkCheck.Text = "Check"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(944, 9)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(22, 13)
        Me.lblStatus.TabIndex = 33
        Me.lblStatus.Text = "OK"
        Me.lblStatus.Visible = False
        '
        'lbkImport
        '
        Me.lbkImport.AutoSize = True
        Me.lbkImport.Location = New System.Drawing.Point(12, 4)
        Me.lbkImport.Name = "lbkImport"
        Me.lbkImport.Size = New System.Drawing.Size(36, 13)
        Me.lbkImport.TabIndex = 34
        Me.lbkImport.TabStop = True
        Me.lbkImport.Text = "Import"
        '
        'cboYear
        '
        Me.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboYear.FormattingEnabled = True
        Me.cboYear.Items.AddRange(New Object() {"2020", "2021"})
        Me.cboYear.Location = New System.Drawing.Point(98, 1)
        Me.cboYear.Name = "cboYear"
        Me.cboYear.Size = New System.Drawing.Size(121, 21)
        Me.cboYear.TabIndex = 36
        '
        'cboQuarter
        '
        Me.cboQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboQuarter.FormattingEnabled = True
        Me.cboQuarter.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.cboQuarter.Location = New System.Drawing.Point(225, 1)
        Me.cboQuarter.Name = "cboQuarter"
        Me.cboQuarter.Size = New System.Drawing.Size(75, 21)
        Me.cboQuarter.TabIndex = 37
        '
        'wb
        '
        Me.wb.Location = New System.Drawing.Point(9, 253)
        Me.wb.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wb.Name = "wb"
        Me.wb.Size = New System.Drawing.Size(990, 396)
        Me.wb.TabIndex = 50
        '
        'lbkAddTkts
        '
        Me.lbkAddTkts.AutoSize = True
        Me.lbkAddTkts.Location = New System.Drawing.Point(694, 9)
        Me.lbkAddTkts.Name = "lbkAddTkts"
        Me.lbkAddTkts.Size = New System.Drawing.Size(47, 13)
        Me.lbkAddTkts.TabIndex = 51
        Me.lbkAddTkts.TabStop = True
        Me.lbkAddTkts.Text = "AddTkts"
        '
        'lbkSelectAddButton
        '
        Me.lbkSelectAddButton.AutoSize = True
        Me.lbkSelectAddButton.Location = New System.Drawing.Point(601, 9)
        Me.lbkSelectAddButton.Name = "lbkSelectAddButton"
        Me.lbkSelectAddButton.Size = New System.Drawing.Size(87, 13)
        Me.lbkSelectAddButton.TabIndex = 52
        Me.lbkSelectAddButton.TabStop = True
        Me.lbkSelectAddButton.Text = "SelectAddButton"
        '
        'dgrTickets
        '
        Me.dgrTickets.AllowUserToAddRows = False
        Me.dgrTickets.AllowUserToDeleteRows = False
        Me.dgrTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTickets.Location = New System.Drawing.Point(9, 45)
        Me.dgrTickets.Name = "dgrTickets"
        Me.dgrTickets.ReadOnly = True
        Me.dgrTickets.Size = New System.Drawing.Size(987, 202)
        Me.dgrTickets.TabIndex = 53
        '
        'cboDomInt
        '
        Me.cboDomInt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDomInt.FormattingEnabled = True
        Me.cboDomInt.Items.AddRange(New Object() {"", "DOM", "INT"})
        Me.cboDomInt.Location = New System.Drawing.Point(306, 1)
        Me.cboDomInt.Name = "cboDomInt"
        Me.cboDomInt.Size = New System.Drawing.Size(75, 21)
        Me.cboDomInt.TabIndex = 54
        '
        'cboVnCorpId
        '
        Me.cboVnCorpId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVnCorpId.FormattingEnabled = True
        Me.cboVnCorpId.Location = New System.Drawing.Point(387, 1)
        Me.cboVnCorpId.Name = "cboVnCorpId"
        Me.cboVnCorpId.Size = New System.Drawing.Size(75, 21)
        Me.cboVnCorpId.TabIndex = 55
        '
        'lblRecords
        '
        Me.lblRecords.AutoSize = True
        Me.lblRecords.Location = New System.Drawing.Point(468, 9)
        Me.lblRecords.Name = "lblRecords"
        Me.lblRecords.Size = New System.Drawing.Size(60, 13)
        Me.lblRecords.TabIndex = 56
        Me.lblRecords.Text = "xx Records"
        '
        'lbkRefreshData
        '
        Me.lbkRefreshData.AutoSize = True
        Me.lbkRefreshData.Location = New System.Drawing.Point(694, 29)
        Me.lbkRefreshData.Name = "lbkRefreshData"
        Me.lbkRefreshData.Size = New System.Drawing.Size(67, 13)
        Me.lbkRefreshData.TabIndex = 57
        Me.lbkRefreshData.TabStop = True
        Me.lbkRefreshData.Text = "RefreshData"
        '
        'lbkChangeCorp
        '
        Me.lbkChangeCorp.AutoSize = True
        Me.lbkChangeCorp.Location = New System.Drawing.Point(820, 9)
        Me.lbkChangeCorp.Name = "lbkChangeCorp"
        Me.lbkChangeCorp.Size = New System.Drawing.Size(66, 13)
        Me.lbkChangeCorp.TabIndex = 58
        Me.lbkChangeCorp.TabStop = True
        Me.lbkChangeCorp.Text = "ChangeCorp"
        '
        'Timer1
        '
        '
        'lbkChange2Dom
        '
        Me.lbkChange2Dom.AutoSize = True
        Me.lbkChange2Dom.Location = New System.Drawing.Point(539, 29)
        Me.lbkChange2Dom.Name = "lbkChange2Dom"
        Me.lbkChange2Dom.Size = New System.Drawing.Size(38, 13)
        Me.lbkChange2Dom.TabIndex = 59
        Me.lbkChange2Dom.TabStop = True
        Me.lbkChange2Dom.Text = "2DOM"
        '
        'lbkChange2INT
        '
        Me.lbkChange2INT.AutoSize = True
        Me.lbkChange2INT.Location = New System.Drawing.Point(601, 29)
        Me.lbkChange2INT.Name = "lbkChange2INT"
        Me.lbkChange2INT.Size = New System.Drawing.Size(31, 13)
        Me.lbkChange2INT.TabIndex = 60
        Me.lbkChange2INT.TabStop = True
        Me.lbkChange2INT.Text = "2INT"
        '
        'lbkImportDirectory
        '
        Me.lbkImportDirectory.AutoSize = True
        Me.lbkImportDirectory.Location = New System.Drawing.Point(12, 29)
        Me.lbkImportDirectory.Name = "lbkImportDirectory"
        Me.lbkImportDirectory.Size = New System.Drawing.Size(78, 13)
        Me.lbkImportDirectory.TabIndex = 61
        Me.lbkImportDirectory.TabStop = True
        Me.lbkImportDirectory.Text = "ImportDirectory"
        '
        'frmVnCorpTkt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 661)
        Me.Controls.Add(Me.lbkImportDirectory)
        Me.Controls.Add(Me.lbkChange2INT)
        Me.Controls.Add(Me.lbkChange2Dom)
        Me.Controls.Add(Me.lbkChangeCorp)
        Me.Controls.Add(Me.lbkRefreshData)
        Me.Controls.Add(Me.lblRecords)
        Me.Controls.Add(Me.cboVnCorpId)
        Me.Controls.Add(Me.cboDomInt)
        Me.Controls.Add(Me.dgrTickets)
        Me.Controls.Add(Me.lbkSelectAddButton)
        Me.Controls.Add(Me.lbkAddTkts)
        Me.Controls.Add(Me.wb)
        Me.Controls.Add(Me.cboQuarter)
        Me.Controls.Add(Me.cboYear)
        Me.Controls.Add(Me.lbkImport)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lbkCheck)
        Me.Controls.Add(Me.lbkLogin)
        Me.Name = "frmVnCorpTkt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VnCorpTkt"
        CType(Me.dgrTickets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkLogin As LinkLabel
    Friend WithEvents lbkCheck As LinkLabel
    Friend WithEvents lblStatus As Label
    Friend WithEvents lbkImport As LinkLabel
    Friend WithEvents cboYear As ComboBox
    Friend WithEvents cboQuarter As ComboBox
    Friend WithEvents wb As WebBrowser
    Friend WithEvents lbkAddTkts As LinkLabel
    Friend WithEvents lbkSelectAddButton As LinkLabel
    Friend WithEvents dgrTickets As DataGridView
    Friend WithEvents cboDomInt As ComboBox
    Friend WithEvents cboVnCorpId As ComboBox
    Friend WithEvents lblRecords As Label
    Friend WithEvents lbkRefreshData As LinkLabel
    Friend WithEvents lbkChangeCorp As LinkLabel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lbkChange2Dom As LinkLabel
    Friend WithEvents lbkChange2INT As LinkLabel
    Friend WithEvents lbkImportDirectory As LinkLabel
End Class
