<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditComm
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
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.txtComm = New System.Windows.Forms.TextBox()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgrTkt = New System.Windows.Forms.DataGridView()
        Me.cboCustomers = New System.Windows.Forms.ComboBox()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        CType(Me.dgrTkt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(133, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "DOI between"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(320, 22)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 2
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'txtComm
        '
        Me.txtComm.Location = New System.Drawing.Point(214, 238)
        Me.txtComm.Name = "txtComm"
        Me.txtComm.Size = New System.Drawing.Size(100, 20)
        Me.txtComm.TabIndex = 3
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(320, 245)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 5
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(133, 245)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Comm"
        '
        'dgrTkt
        '
        Me.dgrTkt.AllowUserToAddRows = False
        Me.dgrTkt.AllowUserToDeleteRows = False
        Me.dgrTkt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTkt.Location = New System.Drawing.Point(3, 43)
        Me.dgrTkt.Name = "dgrTkt"
        Me.dgrTkt.ReadOnly = True
        Me.dgrTkt.Size = New System.Drawing.Size(993, 170)
        Me.dgrTkt.TabIndex = 6
        '
        'cboCustomers
        '
        Me.cboCustomers.FormattingEnabled = True
        Me.cboCustomers.Location = New System.Drawing.Point(3, 16)
        Me.cboCustomers.Name = "cboCustomers"
        Me.cboCustomers.Size = New System.Drawing.Size(121, 21)
        Me.cboCustomers.TabIndex = 7
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd MMM yy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(136, 16)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(86, 20)
        Me.dtpFromDate.TabIndex = 8
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd MMM yy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(228, 17)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(86, 20)
        Me.dtpToDate.TabIndex = 9
        '
        'frmEditComm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 261)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.cboCustomers)
        Me.Controls.Add(Me.dgrTkt)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtComm)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmEditComm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmEditComm"
        CType(Me.dgrTkt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents txtComm As TextBox
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents Label3 As Label
    Friend WithEvents dgrTkt As DataGridView
    Friend WithEvents cboCustomers As ComboBox
    Friend WithEvents dtpFromDate As DateTimePicker
    Friend WithEvents dtpToDate As DateTimePicker
End Class
