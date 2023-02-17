<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFindVendor
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
        Me.txtVemdorName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        Me.dgrVendors = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboCAT = New System.Windows.Forms.ComboBox()
        CType(Me.dgrVendors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtVemdorName
        '
        Me.txtVemdorName.Location = New System.Drawing.Point(4, 25)
        Me.txtVemdorName.Name = "txtVemdorName"
        Me.txtVemdorName.Size = New System.Drawing.Size(127, 20)
        Me.txtVemdorName.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "VendorName"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(242, 28)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 17
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(1, 248)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 18
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        '
        'dgrVendors
        '
        Me.dgrVendors.AllowUserToAddRows = False
        Me.dgrVendors.AllowUserToDeleteRows = False
        Me.dgrVendors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrVendors.Location = New System.Drawing.Point(4, 51)
        Me.dgrVendors.Name = "dgrVendors"
        Me.dgrVendors.ReadOnly = True
        Me.dgrVendors.Size = New System.Drawing.Size(468, 194)
        Me.dgrVendors.TabIndex = 19
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(137, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "CAT"
        '
        'cboCAT
        '
        Me.cboCAT.FormattingEnabled = True
        Me.cboCAT.Items.AddRange(New Object() {"NH", "KS", "OT"})
        Me.cboCAT.Location = New System.Drawing.Point(140, 24)
        Me.cboCAT.Name = "cboCAT"
        Me.cboCAT.Size = New System.Drawing.Size(55, 21)
        Me.cboCAT.TabIndex = 21
        '
        'frmFindVendor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 261)
        Me.Controls.Add(Me.cboCAT)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgrVendors)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.txtVemdorName)
        Me.Controls.Add(Me.Label4)
        Me.Name = "frmFindVendor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FindVendor"
        CType(Me.dgrVendors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtVemdorName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkSelect As LinkLabel
    Friend WithEvents dgrVendors As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cboCAT As ComboBox
End Class
