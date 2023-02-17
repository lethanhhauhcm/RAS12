<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIncentiveFormulaEdit
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
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.blkCancel = New System.Windows.Forms.LinkLabel()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.txtFareBasis = New System.Windows.Forms.TextBox()
        Me.txtBkgCls = New System.Windows.Forms.TextBox()
        Me.txtCar = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboDateType = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.txtVND = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboRtgType = New System.Windows.Forms.ComboBox()
        Me.cboSegTkt = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboDomInt = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtRecId
        '
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(9, 23)
        Me.txtRecId.MaxLength = 20
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(73, 20)
        Me.txtRecId.TabIndex = 31
        Me.txtRecId.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(36, 13)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "RecId"
        '
        'blkCancel
        '
        Me.blkCancel.AutoSize = True
        Me.blkCancel.Location = New System.Drawing.Point(168, 145)
        Me.blkCancel.Name = "blkCancel"
        Me.blkCancel.Size = New System.Drawing.Size(40, 13)
        Me.blkCancel.TabIndex = 29
        Me.blkCancel.TabStop = True
        Me.blkCancel.Text = "Cancel"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(91, 145)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 28
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'txtFareBasis
        '
        Me.txtFareBasis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFareBasis.Location = New System.Drawing.Point(88, 96)
        Me.txtFareBasis.Multiline = True
        Me.txtFareBasis.Name = "txtFareBasis"
        Me.txtFareBasis.Size = New System.Drawing.Size(789, 31)
        Me.txtFareBasis.TabIndex = 25
        Me.txtFareBasis.Text = "*"
        '
        'txtBkgCls
        '
        Me.txtBkgCls.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBkgCls.Location = New System.Drawing.Point(88, 59)
        Me.txtBkgCls.Multiline = True
        Me.txtBkgCls.Name = "txtBkgCls"
        Me.txtBkgCls.Size = New System.Drawing.Size(789, 31)
        Me.txtBkgCls.TabIndex = 24
        Me.txtBkgCls.Text = "*"
        '
        'txtCar
        '
        Me.txtCar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCar.Location = New System.Drawing.Point(89, 23)
        Me.txtCar.MaxLength = 2
        Me.txtCar.Name = "txtCar"
        Me.txtCar.Size = New System.Drawing.Size(43, 20)
        Me.txtCar.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "FareBasis"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "BkgCls"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(87, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Car"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(135, 6)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 13)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "DateType"
        '
        'cboDateType
        '
        Me.cboDateType.FormattingEnabled = True
        Me.cboDateType.Items.AddRange(New Object() {"DOF", "DOI"})
        Me.cboDateType.Location = New System.Drawing.Point(138, 22)
        Me.cboDateType.Name = "cboDateType"
        Me.cboDateType.Size = New System.Drawing.Size(60, 21)
        Me.cboDateType.TabIndex = 33
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(208, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 13)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = "FromDate"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(306, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(43, 13)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "ToDate"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd MMM yy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(204, 22)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(98, 20)
        Me.dtpFromDate.TabIndex = 36
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd MMM yy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(308, 23)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(98, 20)
        Me.dtpToDate.TabIndex = 37
        '
        'txtVND
        '
        Me.txtVND.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVND.Location = New System.Drawing.Point(674, 22)
        Me.txtVND.MaxLength = 16
        Me.txtVND.Name = "txtVND"
        Me.txtVND.Size = New System.Drawing.Size(117, 20)
        Me.txtVND.TabIndex = 39
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(671, 5)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 13)
        Me.Label11.TabIndex = 38
        Me.Label11.Text = "VND"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(546, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "RtgType"
        '
        'cboRtgType
        '
        Me.cboRtgType.FormattingEnabled = True
        Me.cboRtgType.Location = New System.Drawing.Point(547, 22)
        Me.cboRtgType.Name = "cboRtgType"
        Me.cboRtgType.Size = New System.Drawing.Size(121, 21)
        Me.cboRtgType.TabIndex = 40
        '
        'cboSegTkt
        '
        Me.cboSegTkt.FormattingEnabled = True
        Me.cboSegTkt.Items.AddRange(New Object() {"SEG", "TKT"})
        Me.cboSegTkt.Location = New System.Drawing.Point(413, 23)
        Me.cboSegTkt.Name = "cboSegTkt"
        Me.cboSegTkt.Size = New System.Drawing.Size(60, 21)
        Me.cboSegTkt.TabIndex = 42
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(412, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "SegTkt"
        '
        'cboDomInt
        '
        Me.cboDomInt.FormattingEnabled = True
        Me.cboDomInt.Items.AddRange(New Object() {"DOM", "INT"})
        Me.cboDomInt.Location = New System.Drawing.Point(481, 23)
        Me.cboDomInt.Name = "cboDomInt"
        Me.cboDomInt.Size = New System.Drawing.Size(60, 21)
        Me.cboDomInt.TabIndex = 44
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(480, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "DomInt"
        '
        'frmIncentiveFormulaEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 166)
        Me.Controls.Add(Me.cboDomInt)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboSegTkt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboRtgType)
        Me.Controls.Add(Me.txtVND)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboDateType)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.blkCancel)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.txtFareBasis)
        Me.Controls.Add(Me.txtBkgCls)
        Me.Controls.Add(Me.txtCar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmIncentiveFormulaEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Incentive Formula Edit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtRecId As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents blkCancel As LinkLabel
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents txtFareBasis As TextBox
    Friend WithEvents txtBkgCls As TextBox
    Friend WithEvents txtCar As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cboDateType As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents dtpFromDate As DateTimePicker
    Friend WithEvents dtpToDate As DateTimePicker
    Friend WithEvents txtVND As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cboRtgType As ComboBox
    Friend WithEvents cboSegTkt As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cboDomInt As ComboBox
    Friend WithEvents Label6 As Label
End Class
