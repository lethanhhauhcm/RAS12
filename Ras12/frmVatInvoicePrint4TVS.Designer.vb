<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVatInvoicePrint4TVS
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
        Me.lbkCreateE_Invoice = New System.Windows.Forms.LinkLabel()
        Me.cboRasDoc = New System.Windows.Forms.ComboBox()
        Me.txtTktItem = New System.Windows.Forms.TextBox()
        Me.lbkLoadFromRAS = New System.Windows.Forms.LinkLabel()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.dgrTktListing = New System.Windows.Forms.DataGridView()
        Me.lbkLoadFile = New System.Windows.Forms.LinkLabel()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.cboSheetName = New System.Windows.Forms.ComboBox()
        Me.cboCustType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dgrTktListing, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkCreateE_Invoice
        '
        Me.lbkCreateE_Invoice.AutoSize = True
        Me.lbkCreateE_Invoice.Location = New System.Drawing.Point(12, 573)
        Me.lbkCreateE_Invoice.Name = "lbkCreateE_Invoice"
        Me.lbkCreateE_Invoice.Size = New System.Drawing.Size(86, 13)
        Me.lbkCreateE_Invoice.TabIndex = 134
        Me.lbkCreateE_Invoice.TabStop = True
        Me.lbkCreateE_Invoice.Text = "CreateE_Invoice"
        '
        'cboRasDoc
        '
        Me.cboRasDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRasDoc.FormattingEnabled = True
        Me.cboRasDoc.Items.AddRange(New Object() {"TRX", "TCODE", "TKT", "AHC"})
        Me.cboRasDoc.Location = New System.Drawing.Point(938, 0)
        Me.cboRasDoc.Name = "cboRasDoc"
        Me.cboRasDoc.Size = New System.Drawing.Size(62, 21)
        Me.cboRasDoc.TabIndex = 130
        '
        'txtTktItem
        '
        Me.txtTktItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTktItem.Location = New System.Drawing.Point(854, 27)
        Me.txtTktItem.Name = "txtTktItem"
        Me.txtTktItem.Size = New System.Drawing.Size(146, 20)
        Me.txtTktItem.TabIndex = 129
        '
        'lbkLoadFromRAS
        '
        Me.lbkLoadFromRAS.AutoSize = True
        Me.lbkLoadFromRAS.Location = New System.Drawing.Point(851, 4)
        Me.lbkLoadFromRAS.Name = "lbkLoadFromRAS"
        Me.lbkLoadFromRAS.Size = New System.Drawing.Size(76, 13)
        Me.lbkLoadFromRAS.TabIndex = 5
        Me.lbkLoadFromRAS.TabStop = True
        Me.lbkLoadFromRAS.Text = "LoadFromRAS"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(804, 30)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 125
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(804, 4)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 4
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'dgrTktListing
        '
        Me.dgrTktListing.AllowUserToAddRows = False
        Me.dgrTktListing.AllowUserToDeleteRows = False
        Me.dgrTktListing.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrTktListing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTktListing.Location = New System.Drawing.Point(8, 49)
        Me.dgrTktListing.Name = "dgrTktListing"
        Me.dgrTktListing.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgrTktListing.Size = New System.Drawing.Size(992, 521)
        Me.dgrTktListing.TabIndex = 115
        '
        'lbkLoadFile
        '
        Me.lbkLoadFile.AutoSize = True
        Me.lbkLoadFile.Location = New System.Drawing.Point(522, 27)
        Me.lbkLoadFile.Name = "lbkLoadFile"
        Me.lbkLoadFile.Size = New System.Drawing.Size(47, 13)
        Me.lbkLoadFile.TabIndex = 3
        Me.lbkLoadFile.TabStop = True
        Me.lbkLoadFile.Text = "LoadFile"
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(100, 22)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(180, 21)
        Me.cboCustomer.TabIndex = 1
        '
        'cboSheetName
        '
        Me.cboSheetName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSheetName.FormattingEnabled = True
        Me.cboSheetName.Location = New System.Drawing.Point(299, 22)
        Me.cboSheetName.Name = "cboSheetName"
        Me.cboSheetName.Size = New System.Drawing.Size(202, 21)
        Me.cboSheetName.TabIndex = 2
        '
        'cboCustType
        '
        Me.cboCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustType.FormattingEnabled = True
        Me.cboCustType.Items.AddRange(New Object() {"CA", "TA", "TO"})
        Me.cboCustType.Location = New System.Drawing.Point(8, 19)
        Me.cboCustType.Name = "cboCustType"
        Me.cboCustType.Size = New System.Drawing.Size(72, 21)
        Me.cboCustType.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 137
        Me.Label1.Text = "CustType"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(97, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 138
        Me.Label2.Text = "Customer"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(296, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 139
        Me.Label3.Text = "SheetName"
        '
        'frmVatInvoicePrint4TVS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCustType)
        Me.Controls.Add(Me.cboSheetName)
        Me.Controls.Add(Me.lbkCreateE_Invoice)
        Me.Controls.Add(Me.cboRasDoc)
        Me.Controls.Add(Me.txtTktItem)
        Me.Controls.Add(Me.lbkLoadFromRAS)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrTktListing)
        Me.Controls.Add(Me.lbkLoadFile)
        Me.Controls.Add(Me.cboCustomer)
        Me.Name = "frmVatInvoicePrint4TVS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VatInvoicePrint4TVS"
        CType(Me.dgrTktListing, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbkCreateE_Invoice As LinkLabel
    Friend WithEvents cboRasDoc As ComboBox
    Friend WithEvents txtTktItem As TextBox
    Friend WithEvents lbkLoadFromRAS As LinkLabel
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents dgrTktListing As DataGridView
    Friend WithEvents lbkLoadFile As LinkLabel
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents cboSheetName As ComboBox
    Friend WithEvents cboCustType As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
