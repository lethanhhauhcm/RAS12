<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManualDiscountEdit
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboCounter = New System.Windows.Forms.ComboBox()
        Me.cboAL = New System.Windows.Forms.ComboBox()
        Me.cboCustType = New System.Windows.Forms.ComboBox()
        Me.cboDocType = New System.Windows.Forms.ComboBox()
        Me.cboPaxType = New System.Windows.Forms.ComboBox()
        Me.cboCur = New System.Windows.Forms.ComboBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.dtpValidFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpValidTo = New System.Windows.Forms.DateTimePicker()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.cboBase = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Counter"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(56, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "AL"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(108, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "CustType"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(166, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "DocType"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "PaxType"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(65, 68)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Cur"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(129, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Amount"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1, 128)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "ValidFrom/ValidTo"
        '
        'cboCounter
        '
        Me.cboCounter.FormattingEnabled = True
        Me.cboCounter.Items.AddRange(New Object() {"TVS", "GSA", "CWT"})
        Me.cboCounter.Location = New System.Drawing.Point(4, 25)
        Me.cboCounter.Name = "cboCounter"
        Me.cboCounter.Size = New System.Drawing.Size(49, 21)
        Me.cboCounter.TabIndex = 8
        '
        'cboAL
        '
        Me.cboAL.FormattingEnabled = True
        Me.cboAL.Location = New System.Drawing.Point(59, 25)
        Me.cboAL.Name = "cboAL"
        Me.cboAL.Size = New System.Drawing.Size(43, 21)
        Me.cboAL.TabIndex = 9
        '
        'cboCustType
        '
        Me.cboCustType.FormattingEnabled = True
        Me.cboCustType.Items.AddRange(New Object() {"TA", "WK"})
        Me.cboCustType.Location = New System.Drawing.Point(111, 25)
        Me.cboCustType.Name = "cboCustType"
        Me.cboCustType.Size = New System.Drawing.Size(49, 21)
        Me.cboCustType.TabIndex = 10
        '
        'cboDocType
        '
        Me.cboDocType.FormattingEnabled = True
        Me.cboDocType.Items.AddRange(New Object() {"ETK"})
        Me.cboDocType.Location = New System.Drawing.Point(166, 25)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.Size = New System.Drawing.Size(58, 21)
        Me.cboDocType.TabIndex = 11
        '
        'cboPaxType
        '
        Me.cboPaxType.FormattingEnabled = True
        Me.cboPaxType.Items.AddRange(New Object() {"ADL", "CHD", "INF"})
        Me.cboPaxType.Location = New System.Drawing.Point(4, 84)
        Me.cboPaxType.Name = "cboPaxType"
        Me.cboPaxType.Size = New System.Drawing.Size(58, 21)
        Me.cboPaxType.TabIndex = 12
        '
        'cboCur
        '
        Me.cboCur.FormattingEnabled = True
        Me.cboCur.Items.AddRange(New Object() {"VND"})
        Me.cboCur.Location = New System.Drawing.Point(68, 84)
        Me.cboCur.Name = "cboCur"
        Me.cboCur.Size = New System.Drawing.Size(58, 21)
        Me.cboCur.TabIndex = 13
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(132, 85)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(92, 20)
        Me.txtAmount.TabIndex = 14
        '
        'dtpValidFrom
        '
        Me.dtpValidFrom.CustomFormat = "dd MMM yy"
        Me.dtpValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpValidFrom.Location = New System.Drawing.Point(4, 144)
        Me.dtpValidFrom.Name = "dtpValidFrom"
        Me.dtpValidFrom.Size = New System.Drawing.Size(91, 20)
        Me.dtpValidFrom.TabIndex = 15
        '
        'dtpValidTo
        '
        Me.dtpValidTo.CustomFormat = "dd MMM yy"
        Me.dtpValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpValidTo.Location = New System.Drawing.Point(132, 144)
        Me.dtpValidTo.Name = "dtpValidTo"
        Me.dtpValidTo.Size = New System.Drawing.Size(92, 20)
        Me.dtpValidTo.TabIndex = 16
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(1, 183)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 17
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'cboBase
        '
        Me.cboBase.FormattingEnabled = True
        Me.cboBase.Items.AddRange(New Object() {"SEG", "TKT"})
        Me.cboBase.Location = New System.Drawing.Point(230, 84)
        Me.cboBase.Name = "cboBase"
        Me.cboBase.Size = New System.Drawing.Size(58, 21)
        Me.cboBase.TabIndex = 19
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(227, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 13)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Base"
        '
        'frmManualDiscountEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(298, 261)
        Me.Controls.Add(Me.cboBase)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.dtpValidTo)
        Me.Controls.Add(Me.dtpValidFrom)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.cboCur)
        Me.Controls.Add(Me.cboPaxType)
        Me.Controls.Add(Me.cboDocType)
        Me.Controls.Add(Me.cboCustType)
        Me.Controls.Add(Me.cboAL)
        Me.Controls.Add(Me.cboCounter)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmManualDiscountEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ManualDiscountEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cboCounter As ComboBox
    Friend WithEvents cboAL As ComboBox
    Friend WithEvents cboCustType As ComboBox
    Friend WithEvents cboDocType As ComboBox
    Friend WithEvents cboPaxType As ComboBox
    Friend WithEvents cboCur As ComboBox
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents dtpValidFrom As DateTimePicker
    Friend WithEvents dtpValidTo As DateTimePicker
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents cboBase As ComboBox
    Friend WithEvents Label9 As Label
End Class
