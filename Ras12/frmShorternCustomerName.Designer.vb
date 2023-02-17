<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShorternCustomerName
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
        Me.dgrCustomers = New System.Windows.Forms.DataGridView()
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.txtOriginalText = New System.Windows.Forms.TextBox()
        Me.txtReplacement4Bill = New System.Windows.Forms.TextBox()
        Me.lbkAdd = New System.Windows.Forms.LinkLabel()
        Me.txtReplacement4Invoice = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.dgrCustomers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrCustomers
        '
        Me.dgrCustomers.AllowUserToAddRows = False
        Me.dgrCustomers.AllowUserToDeleteRows = False
        Me.dgrCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrCustomers.Location = New System.Drawing.Point(0, 12)
        Me.dgrCustomers.Name = "dgrCustomers"
        Me.dgrCustomers.ReadOnly = True
        Me.dgrCustomers.Size = New System.Drawing.Size(582, 454)
        Me.dgrCustomers.TabIndex = 0
        '
        'cboCustShortName
        '
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(0, 495)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(218, 21)
        Me.cboCustShortName.TabIndex = 1
        '
        'txtOriginalText
        '
        Me.txtOriginalText.Location = New System.Drawing.Point(224, 495)
        Me.txtOriginalText.Name = "txtOriginalText"
        Me.txtOriginalText.Size = New System.Drawing.Size(213, 20)
        Me.txtOriginalText.TabIndex = 2
        '
        'txtReplacement4Bill
        '
        Me.txtReplacement4Bill.Location = New System.Drawing.Point(0, 536)
        Me.txtReplacement4Bill.Name = "txtReplacement4Bill"
        Me.txtReplacement4Bill.Size = New System.Drawing.Size(218, 20)
        Me.txtReplacement4Bill.TabIndex = 3
        '
        'lbkAdd
        '
        Me.lbkAdd.AutoSize = True
        Me.lbkAdd.Location = New System.Drawing.Point(513, 539)
        Me.lbkAdd.Name = "lbkAdd"
        Me.lbkAdd.Size = New System.Drawing.Size(26, 13)
        Me.lbkAdd.TabIndex = 4
        Me.lbkAdd.TabStop = True
        Me.lbkAdd.Text = "Add"
        '
        'txtReplacement4Invoice
        '
        Me.txtReplacement4Invoice.Location = New System.Drawing.Point(224, 539)
        Me.txtReplacement4Invoice.Name = "txtReplacement4Invoice"
        Me.txtReplacement4Invoice.Size = New System.Drawing.Size(213, 20)
        Me.txtReplacement4Invoice.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 479)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "CustShortName"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(224, 479)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "OriginalText"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(0, 519)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Replacement4Bill"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(221, 518)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Replacement4Invoice"
        '
        'frmShorternCustomerName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 561)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtReplacement4Invoice)
        Me.Controls.Add(Me.lbkAdd)
        Me.Controls.Add(Me.txtReplacement4Bill)
        Me.Controls.Add(Me.txtOriginalText)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Controls.Add(Me.dgrCustomers)
        Me.Name = "frmShorternCustomerName"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ShorternCustomerName"
        CType(Me.dgrCustomers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrCustomers As DataGridView
    Friend WithEvents cboCustShortName As ComboBox
    Friend WithEvents txtOriginalText As TextBox
    Friend WithEvents txtReplacement4Bill As TextBox
    Friend WithEvents lbkAdd As LinkLabel
    Friend WithEvents txtReplacement4Invoice As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
End Class
