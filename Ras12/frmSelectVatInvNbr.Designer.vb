<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectVatInvNbr
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
        Me.dgInvStock = New System.Windows.Forms.DataGridView()
        Me.UseFirstNbr = New System.Windows.Forms.LinkLabel()
        Me.lbkAdd = New System.Windows.Forms.LinkLabel()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        Me.lbkRemove = New System.Windows.Forms.LinkLabel()
        Me.cboSerialType = New System.Windows.Forms.ComboBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtFromNbr = New System.Windows.Forms.TextBox()
        Me.txtToNbr = New System.Windows.Forms.TextBox()
        Me.cboKyHieu = New System.Windows.Forms.ComboBox()
        CType(Me.dgInvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgInvStock
        '
        Me.dgInvStock.AllowUserToAddRows = False
        Me.dgInvStock.AllowUserToDeleteRows = False
        Me.dgInvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgInvStock.Location = New System.Drawing.Point(0, 0)
        Me.dgInvStock.Name = "dgInvStock"
        Me.dgInvStock.ReadOnly = True
        Me.dgInvStock.Size = New System.Drawing.Size(281, 150)
        Me.dgInvStock.TabIndex = 0
        '
        'UseFirstNbr
        '
        Me.UseFirstNbr.AutoSize = True
        Me.UseFirstNbr.Location = New System.Drawing.Point(-3, 157)
        Me.UseFirstNbr.Name = "UseFirstNbr"
        Me.UseFirstNbr.Size = New System.Drawing.Size(62, 13)
        Me.UseFirstNbr.TabIndex = 1
        Me.UseFirstNbr.TabStop = True
        Me.UseFirstNbr.Text = "UseFirstNbr"
        '
        'lbkAdd
        '
        Me.lbkAdd.AutoSize = True
        Me.lbkAdd.Location = New System.Drawing.Point(208, 220)
        Me.lbkAdd.Name = "lbkAdd"
        Me.lbkAdd.Size = New System.Drawing.Size(26, 13)
        Me.lbkAdd.TabIndex = 2
        Me.lbkAdd.TabStop = True
        Me.lbkAdd.Text = "Add"
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(243, 220)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 3
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'lbkRemove
        '
        Me.lbkRemove.AutoSize = True
        Me.lbkRemove.Location = New System.Drawing.Point(68, 155)
        Me.lbkRemove.Name = "lbkRemove"
        Me.lbkRemove.Size = New System.Drawing.Size(47, 13)
        Me.lbkRemove.TabIndex = 4
        Me.lbkRemove.TabStop = True
        Me.lbkRemove.Text = "Remove"
        '
        'cboSerialType
        '
        Me.cboSerialType.FormattingEnabled = True
        Me.cboSerialType.Items.AddRange(New Object() {"First", "Last"})
        Me.cboSerialType.Location = New System.Drawing.Point(121, 152)
        Me.cboSerialType.Name = "cboSerialType"
        Me.cboSerialType.Size = New System.Drawing.Size(84, 21)
        Me.cboSerialType.TabIndex = 5
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(211, 154)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(39, 20)
        Me.txtQty.TabIndex = 6
        Me.txtQty.Text = "1"
        '
        'txtFromNbr
        '
        Me.txtFromNbr.Location = New System.Drawing.Point(121, 187)
        Me.txtFromNbr.Name = "txtFromNbr"
        Me.txtFromNbr.Size = New System.Drawing.Size(84, 20)
        Me.txtFromNbr.TabIndex = 7
        Me.txtFromNbr.Text = "1"
        '
        'txtToNbr
        '
        Me.txtToNbr.Location = New System.Drawing.Point(211, 187)
        Me.txtToNbr.Name = "txtToNbr"
        Me.txtToNbr.Size = New System.Drawing.Size(70, 20)
        Me.txtToNbr.TabIndex = 8
        Me.txtToNbr.Text = "1"
        '
        'cboKyHieu
        '
        Me.cboKyHieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKyHieu.FormattingEnabled = True
        Me.cboKyHieu.Items.AddRange(New Object() {"TV/18P", "TV/19P "})
        Me.cboKyHieu.Location = New System.Drawing.Point(58, 187)
        Me.cboKyHieu.Name = "cboKyHieu"
        Me.cboKyHieu.Size = New System.Drawing.Size(57, 21)
        Me.cboKyHieu.TabIndex = 9
        '
        'frmSelectVatInvNbr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.cboKyHieu)
        Me.Controls.Add(Me.txtToNbr)
        Me.Controls.Add(Me.txtFromNbr)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.cboSerialType)
        Me.Controls.Add(Me.lbkRemove)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.lbkAdd)
        Me.Controls.Add(Me.UseFirstNbr)
        Me.Controls.Add(Me.dgInvStock)
        Me.Name = "frmSelectVatInvNbr"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select VAT invoice number"
        CType(Me.dgInvStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgInvStock As DataGridView
    Friend WithEvents UseFirstNbr As LinkLabel
    Friend WithEvents lbkAdd As LinkLabel
    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents lbkRemove As LinkLabel
    Friend WithEvents cboSerialType As ComboBox
    Friend WithEvents txtQty As TextBox
    Friend WithEvents txtFromNbr As TextBox
    Friend WithEvents txtToNbr As TextBox
    Friend WithEvents cboKyHieu As ComboBox
End Class
