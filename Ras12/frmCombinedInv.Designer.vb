<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCombinedInv
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
        Me.lbkCreateE_Inv = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboCust = New System.Windows.Forms.ComboBox()
        Me.dgrTkts = New System.Windows.Forms.DataGridView()
        Me.S = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dtpDOS = New System.Windows.Forms.DateTimePicker()
        Me.lbkLoad = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblInvoiceTotal = New System.Windows.Forms.Label()
        Me.lbkSelectRcp = New System.Windows.Forms.LinkLabel()
        Me.lbkUnSelectRcp = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkCreateE_Inv
        '
        Me.lbkCreateE_Inv.AutoSize = True
        Me.lbkCreateE_Inv.Location = New System.Drawing.Point(523, 39)
        Me.lbkCreateE_Inv.Name = "lbkCreateE_Inv"
        Me.lbkCreateE_Inv.Size = New System.Drawing.Size(66, 13)
        Me.lbkCreateE_Inv.TabIndex = 13
        Me.lbkCreateE_Inv.TabStop = True
        Me.lbkCreateE_Inv.Text = "CreateE_Inv"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Cust."
        '
        'cboCust
        '
        Me.cboCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCust.FormattingEnabled = True
        Me.cboCust.Location = New System.Drawing.Point(15, 31)
        Me.cboCust.Name = "cboCust"
        Me.cboCust.Size = New System.Drawing.Size(146, 21)
        Me.cboCust.TabIndex = 11
        '
        'dgrTkts
        '
        Me.dgrTkts.AllowUserToAddRows = False
        Me.dgrTkts.AllowUserToDeleteRows = False
        Me.dgrTkts.BackgroundColor = System.Drawing.Color.Azure
        Me.dgrTkts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTkts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.S})
        Me.dgrTkts.Location = New System.Drawing.Point(3, 61)
        Me.dgrTkts.Name = "dgrTkts"
        Me.dgrTkts.RowHeadersVisible = False
        Me.dgrTkts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrTkts.Size = New System.Drawing.Size(785, 377)
        Me.dgrTkts.TabIndex = 10
        '
        'S
        '
        Me.S.HeaderText = "S"
        Me.S.Name = "S"
        Me.S.Width = 24
        '
        'dtpDOS
        '
        Me.dtpDOS.CustomFormat = "dd MMM yy"
        Me.dtpDOS.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDOS.Location = New System.Drawing.Point(167, 31)
        Me.dtpDOS.Name = "dtpDOS"
        Me.dtpDOS.Size = New System.Drawing.Size(90, 20)
        Me.dtpDOS.TabIndex = 14
        '
        'lbkLoad
        '
        Me.lbkLoad.AutoSize = True
        Me.lbkLoad.Location = New System.Drawing.Point(263, 38)
        Me.lbkLoad.Name = "lbkLoad"
        Me.lbkLoad.Size = New System.Drawing.Size(49, 13)
        Me.lbkLoad.TabIndex = 15
        Me.lbkLoad.TabStop = True
        Me.lbkLoad.Text = "Load Inv"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(164, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Ngày nhập RAS"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(704, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Invoice Total"
        Me.Label3.Visible = False
        '
        'lblInvoiceTotal
        '
        Me.lblInvoiceTotal.AutoSize = True
        Me.lblInvoiceTotal.Location = New System.Drawing.Point(724, 34)
        Me.lblInvoiceTotal.Name = "lblInvoiceTotal"
        Me.lblInvoiceTotal.Size = New System.Drawing.Size(49, 13)
        Me.lblInvoiceTotal.TabIndex = 18
        Me.lblInvoiceTotal.Text = "TotalAmt"
        Me.lblInvoiceTotal.Visible = False
        '
        'lbkSelectRcp
        '
        Me.lbkSelectRcp.AutoSize = True
        Me.lbkSelectRcp.Location = New System.Drawing.Point(318, 39)
        Me.lbkSelectRcp.Name = "lbkSelectRcp"
        Me.lbkSelectRcp.Size = New System.Drawing.Size(60, 13)
        Me.lbkSelectRcp.TabIndex = 19
        Me.lbkSelectRcp.TabStop = True
        Me.lbkSelectRcp.Text = "Select Rcp"
        '
        'lbkUnSelectRcp
        '
        Me.lbkUnSelectRcp.AutoSize = True
        Me.lbkUnSelectRcp.Location = New System.Drawing.Point(384, 39)
        Me.lbkUnSelectRcp.Name = "lbkUnSelectRcp"
        Me.lbkUnSelectRcp.Size = New System.Drawing.Size(74, 13)
        Me.lbkUnSelectRcp.TabIndex = 20
        Me.lbkUnSelectRcp.TabStop = True
        Me.lbkUnSelectRcp.Text = "UnSelect Rcp"
        '
        'frmCombinedInv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.lbkUnSelectRcp)
        Me.Controls.Add(Me.lbkSelectRcp)
        Me.Controls.Add(Me.lblInvoiceTotal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbkLoad)
        Me.Controls.Add(Me.dtpDOS)
        Me.Controls.Add(Me.lbkCreateE_Inv)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCust)
        Me.Controls.Add(Me.dgrTkts)
        Me.Name = "frmCombinedInv"
        Me.Text = "Combined Invoice"
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkCreateE_Inv As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents cboCust As ComboBox
    Friend WithEvents dgrTkts As DataGridView
    Friend WithEvents S As DataGridViewCheckBoxColumn
    Friend WithEvents dtpDOS As DateTimePicker
    Friend WithEvents lbkLoad As LinkLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblInvoiceTotal As Label
    Friend WithEvents lbkSelectRcp As LinkLabel
    Friend WithEvents lbkUnSelectRcp As LinkLabel
End Class
