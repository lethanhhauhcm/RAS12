<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectVatInv
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
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        Me.dgrVatInv = New System.Windows.Forms.DataGridView()
        Me.lblSumSelected = New System.Windows.Forms.Label()
        Me.txtSumSelected = New System.Windows.Forms.TextBox()
        CType(Me.dgrVatInv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(3, 630)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 3
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        '
        'dgrVatInv
        '
        Me.dgrVatInv.AllowUserToAddRows = False
        Me.dgrVatInv.AllowUserToDeleteRows = False
        Me.dgrVatInv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrVatInv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrVatInv.Location = New System.Drawing.Point(-5, 26)
        Me.dgrVatInv.Name = "dgrVatInv"
        Me.dgrVatInv.ReadOnly = True
        Me.dgrVatInv.Size = New System.Drawing.Size(694, 592)
        Me.dgrVatInv.TabIndex = 2
        '
        'lblSumSelected
        '
        Me.lblSumSelected.AutoSize = True
        Me.lblSumSelected.Location = New System.Drawing.Point(515, 9)
        Me.lblSumSelected.Name = "lblSumSelected"
        Me.lblSumSelected.Size = New System.Drawing.Size(73, 13)
        Me.lblSumSelected.TabIndex = 5
        Me.lblSumSelected.Text = "Sum Selected"
        '
        'txtSumSelected
        '
        Me.txtSumSelected.Location = New System.Drawing.Point(589, 2)
        Me.txtSumSelected.Name = "txtSumSelected"
        Me.txtSumSelected.Size = New System.Drawing.Size(83, 20)
        Me.txtSumSelected.TabIndex = 6
        Me.txtSumSelected.Text = "0"
        '
        'frmSelectVatInv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 661)
        Me.Controls.Add(Me.txtSumSelected)
        Me.Controls.Add(Me.lblSumSelected)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.dgrVatInv)
        Me.Name = "frmSelectVatInv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SelectVatInv"
        CType(Me.dgrVatInv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkSelect As LinkLabel
    Friend WithEvents dgrVatInv As DataGridView
    Friend WithEvents lblSumSelected As Label
    Friend WithEvents txtSumSelected As TextBox
End Class
