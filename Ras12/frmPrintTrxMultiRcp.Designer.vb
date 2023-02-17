<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintTrxMultiRcp
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
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.dtpDOI = New System.Windows.Forms.DateTimePicker()
        Me.lbkPrint = New System.Windows.Forms.LinkLabel()
        Me.dgrTkts = New System.Windows.Forms.DataGridView()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkPreview = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(143, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "DOI"
        '
        'cboCustomer
        '
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(16, 16)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(121, 21)
        Me.cboCustomer.TabIndex = 2
        '
        'dtpDOI
        '
        Me.dtpDOI.CustomFormat = "dd MMM yy"
        Me.dtpDOI.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOI.Location = New System.Drawing.Point(143, 16)
        Me.dtpDOI.Name = "dtpDOI"
        Me.dtpDOI.Size = New System.Drawing.Size(113, 20)
        Me.dtpDOI.TabIndex = 3
        '
        'lbkPrint
        '
        Me.lbkPrint.AutoSize = True
        Me.lbkPrint.Location = New System.Drawing.Point(13, 239)
        Me.lbkPrint.Name = "lbkPrint"
        Me.lbkPrint.Size = New System.Drawing.Size(28, 13)
        Me.lbkPrint.TabIndex = 4
        Me.lbkPrint.TabStop = True
        Me.lbkPrint.Text = "Print"
        '
        'dgrTkts
        '
        Me.dgrTkts.AllowUserToAddRows = False
        Me.dgrTkts.AllowUserToDeleteRows = False
        Me.dgrTkts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTkts.Location = New System.Drawing.Point(12, 50)
        Me.dgrTkts.Name = "dgrTkts"
        Me.dgrTkts.Size = New System.Drawing.Size(560, 186)
        Me.dgrTkts.TabIndex = 5
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(532, 22)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 6
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkPreview
        '
        Me.lbkPreview.AutoSize = True
        Me.lbkPreview.Location = New System.Drawing.Point(47, 239)
        Me.lbkPreview.Name = "lbkPreview"
        Me.lbkPreview.Size = New System.Drawing.Size(45, 13)
        Me.lbkPreview.TabIndex = 7
        Me.lbkPreview.TabStop = True
        Me.lbkPreview.Text = "Preview"
        '
        'frmPrintTrxMultiRcp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 261)
        Me.Controls.Add(Me.lbkPreview)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrTkts)
        Me.Controls.Add(Me.lbkPrint)
        Me.Controls.Add(Me.dtpDOI)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmPrintTrxMultiRcp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PrintTrxMultiRcp"
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents dtpDOI As DateTimePicker
    Friend WithEvents lbkPrint As LinkLabel
    Friend WithEvents dgrTkts As DataGridView
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkPreview As LinkLabel
End Class
