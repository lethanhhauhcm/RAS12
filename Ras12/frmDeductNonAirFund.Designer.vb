<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeductNonAirFund
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
        Me.txtDeductedAmount = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCreatedByItem = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtVendorId = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCur = New System.Windows.Forms.TextBox()
        Me.txtFundId = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpExpiryDate = New System.Windows.Forms.DateTimePicker()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.txtVendor = New System.Windows.Forms.TextBox()
        Me.lbkCancel = New System.Windows.Forms.LinkLabel()
        Me.lbkOK = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkCostOnly = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'txtDeductedAmount
        '
        Me.txtDeductedAmount.Location = New System.Drawing.Point(98, 190)
        Me.txtDeductedAmount.Name = "txtDeductedAmount"
        Me.txtDeductedAmount.Size = New System.Drawing.Size(87, 20)
        Me.txtDeductedAmount.TabIndex = 36
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 193)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(90, 13)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "DeductedAmount"
        '
        'txtCreatedByItem
        '
        Me.txtCreatedByItem.Enabled = False
        Me.txtCreatedByItem.Location = New System.Drawing.Point(98, 164)
        Me.txtCreatedByItem.Name = "txtCreatedByItem"
        Me.txtCreatedByItem.Size = New System.Drawing.Size(87, 20)
        Me.txtCreatedByItem.TabIndex = 34
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 167)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "CreatedByItem"
        '
        'txtVendorId
        '
        Me.txtVendorId.Enabled = False
        Me.txtVendorId.Location = New System.Drawing.Point(98, 40)
        Me.txtVendorId.Name = "txtVendorId"
        Me.txtVendorId.Size = New System.Drawing.Size(87, 20)
        Me.txtVendorId.TabIndex = 32
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "VendorID"
        '
        'txtCur
        '
        Me.txtCur.Enabled = False
        Me.txtCur.Location = New System.Drawing.Point(98, 91)
        Me.txtCur.Name = "txtCur"
        Me.txtCur.Size = New System.Drawing.Size(87, 20)
        Me.txtCur.TabIndex = 30
        '
        'txtFundId
        '
        Me.txtFundId.Enabled = False
        Me.txtFundId.Location = New System.Drawing.Point(98, 16)
        Me.txtFundId.Name = "txtFundId"
        Me.txtFundId.Size = New System.Drawing.Size(87, 20)
        Me.txtFundId.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "FundId"
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.CustomFormat = "dd MMM yy"
        Me.dtpExpiryDate.Enabled = False
        Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpiryDate.Location = New System.Drawing.Point(98, 138)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.Size = New System.Drawing.Size(87, 20)
        Me.dtpExpiryDate.TabIndex = 23
        '
        'txtAmount
        '
        Me.txtAmount.Enabled = False
        Me.txtAmount.Location = New System.Drawing.Point(98, 115)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(87, 20)
        Me.txtAmount.TabIndex = 20
        '
        'txtVendor
        '
        Me.txtVendor.Enabled = False
        Me.txtVendor.Location = New System.Drawing.Point(98, 66)
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(184, 20)
        Me.txtVendor.TabIndex = 28
        '
        'lbkCancel
        '
        Me.lbkCancel.AutoSize = True
        Me.lbkCancel.Location = New System.Drawing.Point(171, 239)
        Me.lbkCancel.Name = "lbkCancel"
        Me.lbkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lbkCancel.TabIndex = 27
        Me.lbkCancel.TabStop = True
        Me.lbkCancel.Text = "Cancel"
        '
        'lbkOK
        '
        Me.lbkOK.AutoSize = True
        Me.lbkOK.Location = New System.Drawing.Point(46, 239)
        Me.lbkOK.Name = "lbkOK"
        Me.lbkOK.Size = New System.Drawing.Size(22, 13)
        Me.lbkOK.TabIndex = 25
        Me.lbkOK.TabStop = True
        Me.lbkOK.Text = "OK"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "ExpiryDate"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Amount"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Cur"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Vendor"
        '
        'txtDescription
        '
        Me.txtDescription.Enabled = False
        Me.txtDescription.Location = New System.Drawing.Point(98, 216)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(190, 20)
        Me.txtDescription.TabIndex = 38
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 219)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "Description"
        '
        'chkCostOnly
        '
        Me.chkCostOnly.AutoSize = True
        Me.chkCostOnly.Location = New System.Drawing.Point(191, 192)
        Me.chkCostOnly.Name = "chkCostOnly"
        Me.chkCostOnly.Size = New System.Drawing.Size(68, 17)
        Me.chkCostOnly.TabIndex = 39
        Me.chkCostOnly.Text = "CostOnly"
        Me.chkCostOnly.UseVisualStyleBackColor = True
        '
        'frmDeductNonAirFund
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.chkCostOnly)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtDeductedAmount)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtCreatedByItem)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtVendorId)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtCur)
        Me.Controls.Add(Me.txtFundId)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtpExpiryDate)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.txtVendor)
        Me.Controls.Add(Me.lbkCancel)
        Me.Controls.Add(Me.lbkOK)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmDeductNonAirFund"
        Me.Text = "DeductNonAirFund"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtDeductedAmount As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtCreatedByItem As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtVendorId As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtCur As TextBox
    Friend WithEvents txtFundId As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpExpiryDate As DateTimePicker
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents txtVendor As TextBox
    Friend WithEvents lbkCancel As LinkLabel
    Friend WithEvents lbkOK As LinkLabel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents chkCostOnly As CheckBox
End Class
