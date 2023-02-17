<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNhFundUsage
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
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.dgrFundUsage = New System.Windows.Forms.DataGridView()
        Me.lbkReset = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboWaiverType = New System.Windows.Forms.ComboBox()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.dgrOldTicket = New System.Windows.Forms.DataGridView()
        Me.dgrNewTicket = New System.Windows.Forms.DataGridView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lbkAddNewTkt = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrFundUsage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrOldTicket, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrNewTicket, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(12, 539)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 3
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'dgrFundUsage
        '
        Me.dgrFundUsage.AllowUserToAddRows = False
        Me.dgrFundUsage.AllowUserToDeleteRows = False
        Me.dgrFundUsage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrFundUsage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrFundUsage.Location = New System.Drawing.Point(12, 54)
        Me.dgrFundUsage.Name = "dgrFundUsage"
        Me.dgrFundUsage.ReadOnly = True
        Me.dgrFundUsage.Size = New System.Drawing.Size(798, 467)
        Me.dgrFundUsage.TabIndex = 15
        '
        'lbkReset
        '
        Me.lbkReset.AutoSize = True
        Me.lbkReset.Location = New System.Drawing.Point(727, 27)
        Me.lbkReset.Name = "lbkReset"
        Me.lbkReset.Size = New System.Drawing.Size(35, 13)
        Me.lbkReset.TabIndex = 14
        Me.lbkReset.TabStop = True
        Me.lbkReset.Text = "Reset"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(662, 27)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 13
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(175, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "WaiverType"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Customer"
        '
        'cboWaiverType
        '
        Me.cboWaiverType.FormattingEnabled = True
        Me.cboWaiverType.Items.AddRange(New Object() {"SF1", "SF2", "SF3", "SF4", "SF5", "SF6", "Add2Fund"})
        Me.cboWaiverType.Location = New System.Drawing.Point(178, 27)
        Me.cboWaiverType.Name = "cboWaiverType"
        Me.cboWaiverType.Size = New System.Drawing.Size(59, 21)
        Me.cboWaiverType.TabIndex = 1
        '
        'cboCustomer
        '
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(12, 27)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(160, 21)
        Me.cboCustomer.TabIndex = 0
        '
        'dgrOldTicket
        '
        Me.dgrOldTicket.AllowUserToAddRows = False
        Me.dgrOldTicket.AllowUserToDeleteRows = False
        Me.dgrOldTicket.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrOldTicket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrOldTicket.ColumnHeadersVisible = False
        Me.dgrOldTicket.Location = New System.Drawing.Point(819, 80)
        Me.dgrOldTicket.Name = "dgrOldTicket"
        Me.dgrOldTicket.ReadOnly = True
        Me.dgrOldTicket.RowHeadersVisible = False
        Me.dgrOldTicket.Size = New System.Drawing.Size(188, 207)
        Me.dgrOldTicket.TabIndex = 18
        '
        'dgrNewTicket
        '
        Me.dgrNewTicket.AllowUserToAddRows = False
        Me.dgrNewTicket.AllowUserToDeleteRows = False
        Me.dgrNewTicket.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrNewTicket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrNewTicket.ColumnHeadersVisible = False
        Me.dgrNewTicket.Location = New System.Drawing.Point(819, 315)
        Me.dgrNewTicket.Name = "dgrNewTicket"
        Me.dgrNewTicket.ReadOnly = True
        Me.dgrNewTicket.RowHeadersVisible = False
        Me.dgrNewTicket.Size = New System.Drawing.Size(188, 203)
        Me.dgrNewTicket.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(816, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Old Ticket"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(816, 299)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "New Ticket"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(246, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Status"
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "RR"})
        Me.cboStatus.Location = New System.Drawing.Point(249, 25)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(59, 21)
        Me.cboStatus.TabIndex = 2
        '
        'lbkAddNewTkt
        '
        Me.lbkAddNewTkt.AutoSize = True
        Me.lbkAddNewTkt.Location = New System.Drawing.Point(47, 539)
        Me.lbkAddNewTkt.Name = "lbkAddNewTkt"
        Me.lbkAddNewTkt.Size = New System.Drawing.Size(64, 13)
        Me.lbkAddNewTkt.TabIndex = 24
        Me.lbkAddNewTkt.TabStop = True
        Me.lbkAddNewTkt.Text = "AddNewTkt"
        '
        'frmNhFundUsage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 561)
        Me.Controls.Add(Me.lbkAddNewTkt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgrNewTicket)
        Me.Controls.Add(Me.dgrOldTicket)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.dgrFundUsage)
        Me.Controls.Add(Me.lbkReset)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboWaiverType)
        Me.Controls.Add(Me.cboCustomer)
        Me.Name = "frmNhFundUsage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NhFundUsageList"
        CType(Me.dgrFundUsage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrOldTicket, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrNewTicket, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents dgrFundUsage As DataGridView
    Friend WithEvents lbkReset As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cboWaiverType As ComboBox
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents dgrOldTicket As DataGridView
    Friend WithEvents dgrNewTicket As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents lbkAddNewTkt As LinkLabel
End Class
