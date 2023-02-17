<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlCreditAdd
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.txtRLOC = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpValidFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpValidTo = New System.Windows.Forms.DateTimePicker()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboAL = New System.Windows.Forms.ComboBox()
        Me.txtPaxName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtVoucherNbr = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 162)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "ValidFrom/To"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Customer"
        '
        'cboCustomer
        '
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(106, 37)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(242, 21)
        Me.cboCustomer.TabIndex = 1
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(180, 139)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(86, 20)
        Me.txtAmount.TabIndex = 5
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(141, 230)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 10
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 139)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Amount VND"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(103, 230)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 9
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'txtRLOC
        '
        Me.txtRLOC.Location = New System.Drawing.Point(106, 113)
        Me.txtRLOC.Name = "txtRLOC"
        Me.txtRLOC.Size = New System.Drawing.Size(160, 20)
        Me.txtRLOC.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "RLOC"
        '
        'dtpValidFrom
        '
        Me.dtpValidFrom.CustomFormat = "dd MMM yy"
        Me.dtpValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpValidFrom.Location = New System.Drawing.Point(89, 165)
        Me.dtpValidFrom.Name = "dtpValidFrom"
        Me.dtpValidFrom.Size = New System.Drawing.Size(86, 20)
        Me.dtpValidFrom.TabIndex = 6
        '
        'dtpValidTo
        '
        Me.dtpValidTo.CustomFormat = "dd MMM yy"
        Me.dtpValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpValidTo.Location = New System.Drawing.Point(180, 165)
        Me.dtpValidTo.Name = "dtpValidTo"
        Me.dtpValidTo.Size = New System.Drawing.Size(86, 20)
        Me.dtpValidTo.TabIndex = 7
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(59, 194)
        Me.txtRemark.MaxLength = 64
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(207, 20)
        Me.txtRemark.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 194)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "Remark"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 13)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "AL"
        '
        'cboAL
        '
        Me.cboAL.FormattingEnabled = True
        Me.cboAL.Items.AddRange(New Object() {"BL", "VJ"})
        Me.cboAL.Location = New System.Drawing.Point(106, 12)
        Me.cboAL.Name = "cboAL"
        Me.cboAL.Size = New System.Drawing.Size(49, 21)
        Me.cboAL.TabIndex = 0
        '
        'txtPaxName
        '
        Me.txtPaxName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPaxName.Location = New System.Drawing.Point(106, 87)
        Me.txtPaxName.Name = "txtPaxName"
        Me.txtPaxName.Size = New System.Drawing.Size(242, 20)
        Me.txtPaxName.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 87)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 53
        Me.Label7.Text = "PaxName"
        '
        'txtVoucherNbr
        '
        Me.txtVoucherNbr.Location = New System.Drawing.Point(106, 64)
        Me.txtVoucherNbr.Name = "txtVoucherNbr"
        Me.txtVoucherNbr.Size = New System.Drawing.Size(242, 20)
        Me.txtVoucherNbr.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 55
        Me.Label8.Text = "VoucherNbr"
        '
        'frmAlCreditAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 261)
        Me.Controls.Add(Me.txtVoucherNbr)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtPaxName)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboAL)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpValidTo)
        Me.Controls.Add(Me.dtpValidFrom)
        Me.Controls.Add(Me.txtRLOC)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbkSave)
        Me.Name = "frmAlCreditAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AL Credit Add"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents txtRLOC As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpValidFrom As DateTimePicker
    Friend WithEvents dtpValidTo As DateTimePicker
    Friend WithEvents txtRemark As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cboAL As ComboBox
    Friend WithEvents txtPaxName As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtVoucherNbr As TextBox
    Friend WithEvents Label8 As Label
End Class
