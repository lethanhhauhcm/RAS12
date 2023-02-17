<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectUNC
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
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboService = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgrUNCs = New System.Windows.Forms.DataGridView()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.txtVemdorName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTotalSelected = New System.Windows.Forms.Label()
        Me.lblResidual = New System.Windows.Forms.Label()
        Me.chkCompareAmount = New System.Windows.Forms.CheckBox()
        CType(Me.dgrUNCs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtAccountName
        '
        Me.txtAccountName.Enabled = False
        Me.txtAccountName.Location = New System.Drawing.Point(243, 20)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.Size = New System.Drawing.Size(127, 20)
        Me.txtAccountName.TabIndex = 39
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(240, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "AccountName"
        '
        'cboService
        '
        Me.cboService.FormattingEnabled = True
        Me.cboService.Items.AddRange(New Object() {"Accommodations", "Meal", "Miscellaneous", "Transfer", "Visa"})
        Me.cboService.Location = New System.Drawing.Point(141, 19)
        Me.cboService.Name = "cboService"
        Me.cboService.Size = New System.Drawing.Size(96, 21)
        Me.cboService.TabIndex = 37
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(138, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Service"
        '
        'dgrUNCs
        '
        Me.dgrUNCs.AllowUserToAddRows = False
        Me.dgrUNCs.AllowUserToDeleteRows = False
        Me.dgrUNCs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrUNCs.Location = New System.Drawing.Point(5, 46)
        Me.dgrUNCs.Name = "dgrUNCs"
        Me.dgrUNCs.ReadOnly = True
        Me.dgrUNCs.Size = New System.Drawing.Size(676, 194)
        Me.dgrUNCs.TabIndex = 35
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(2, 243)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 34
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(480, 9)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 33
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'txtVemdorName
        '
        Me.txtVemdorName.Enabled = False
        Me.txtVemdorName.Location = New System.Drawing.Point(5, 20)
        Me.txtVemdorName.Name = "txtVemdorName"
        Me.txtVemdorName.Size = New System.Drawing.Size(127, 20)
        Me.txtVemdorName.TabIndex = 32
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(2, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "VendorName"
        '
        'lblTotalSelected
        '
        Me.lblTotalSelected.AutoSize = True
        Me.lblTotalSelected.Location = New System.Drawing.Point(72, 243)
        Me.lblTotalSelected.Name = "lblTotalSelected"
        Me.lblTotalSelected.Size = New System.Drawing.Size(39, 13)
        Me.lblTotalSelected.TabIndex = 40
        Me.lblTotalSelected.Text = "Label3"
        '
        'lblResidual
        '
        Me.lblResidual.AutoSize = True
        Me.lblResidual.Location = New System.Drawing.Point(276, 243)
        Me.lblResidual.Name = "lblResidual"
        Me.lblResidual.Size = New System.Drawing.Size(48, 13)
        Me.lblResidual.TabIndex = 41
        Me.lblResidual.Text = "Residual"
        '
        'chkCompareAmount
        '
        Me.chkCompareAmount.AutoSize = True
        Me.chkCompareAmount.Location = New System.Drawing.Point(376, 23)
        Me.chkCompareAmount.Name = "chkCompareAmount"
        Me.chkCompareAmount.Size = New System.Drawing.Size(145, 17)
        Me.chkCompareAmount.TabIndex = 42
        Me.chkCompareAmount.Text = "UncAmount<=InvAmount"
        Me.chkCompareAmount.UseVisualStyleBackColor = True
        '
        'frmSelectUNC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 261)
        Me.Controls.Add(Me.chkCompareAmount)
        Me.Controls.Add(Me.lblResidual)
        Me.Controls.Add(Me.lblTotalSelected)
        Me.Controls.Add(Me.txtAccountName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboService)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgrUNCs)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.txtVemdorName)
        Me.Controls.Add(Me.Label4)
        Me.Name = "frmSelectUNC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SelectUNC"
        CType(Me.dgrUNCs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtAccountName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboService As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dgrUNCs As DataGridView
    Friend WithEvents lbkSelect As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents txtVemdorName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lblTotalSelected As Label
    Friend WithEvents lblResidual As Label
    Friend WithEvents chkCompareAmount As CheckBox
End Class
