<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddCorpSF
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
        Me.CmbTKT = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbTRX = New System.Windows.Forms.ComboBox()
        Me.CmbService = New System.Windows.Forms.ComboBox()
        Me.CmbCust = New System.Windows.Forms.ComboBox()
        Me.CmbRtgType = New System.Windows.Forms.ComboBox()
        Me.lblRtg = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CmbCurr = New System.Windows.Forms.ComboBox()
        Me.TxtPersonal = New System.Windows.Forms.TextBox()
        Me.TxtAmount = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpValidThru = New System.Windows.Forms.DateTimePicker()
        Me.dtpValidFrom = New System.Windows.Forms.DateTimePicker()
        Me.label15 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbSegTKT = New System.Windows.Forms.ComboBox()
        Me.CmbCabin = New System.Windows.Forms.ComboBox()
        Me.CmbBase = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CmbOWRT = New System.Windows.Forms.ComboBox()
        Me.CmbAlType = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtFltTime = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.chkWzAIR = New System.Windows.Forms.CheckBox()
        Me.ChkVAT10 = New System.Windows.Forms.CheckBox()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.lbkSetPersonnal = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboCustGroup = New System.Windows.Forms.ComboBox()
        Me.chkVat8 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'CmbTKT
        '
        Me.CmbTKT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTKT.FormattingEnabled = True
        Me.CmbTKT.Items.AddRange(New Object() {"A", "E", "I"})
        Me.CmbTKT.Location = New System.Drawing.Point(392, 38)
        Me.CmbTKT.Name = "CmbTKT"
        Me.CmbTKT.Size = New System.Drawing.Size(34, 21)
        Me.CmbTKT.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(331, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "TRX"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(217, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Service"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Customer"
        '
        'CmbTRX
        '
        Me.CmbTRX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTRX.FormattingEnabled = True
        Me.CmbTRX.Items.AddRange(New Object() {"S", "R", "V"})
        Me.CmbTRX.Location = New System.Drawing.Point(360, 38)
        Me.CmbTRX.Name = "CmbTRX"
        Me.CmbTRX.Size = New System.Drawing.Size(32, 21)
        Me.CmbTRX.TabIndex = 7
        '
        'CmbService
        '
        Me.CmbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbService.FormattingEnabled = True
        Me.CmbService.Items.AddRange(New Object() {"AIR", "HTL", "CAR", "INS", "VSA", "AHC"})
        Me.CmbService.Location = New System.Drawing.Point(260, 38)
        Me.CmbService.Name = "CmbService"
        Me.CmbService.Size = New System.Drawing.Size(70, 21)
        Me.CmbService.TabIndex = 6
        '
        'CmbCust
        '
        Me.CmbCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCust.FormattingEnabled = True
        Me.CmbCust.Location = New System.Drawing.Point(57, 38)
        Me.CmbCust.Name = "CmbCust"
        Me.CmbCust.Size = New System.Drawing.Size(145, 21)
        Me.CmbCust.TabIndex = 5
        '
        'CmbRtgType
        '
        Me.CmbRtgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbRtgType.FormattingEnabled = True
        Me.CmbRtgType.Items.AddRange(New Object() {"DOM", "REG", "REG2", "INTL", "INT2"})
        Me.CmbRtgType.Location = New System.Drawing.Point(57, 92)
        Me.CmbRtgType.Name = "CmbRtgType"
        Me.CmbRtgType.Size = New System.Drawing.Size(78, 21)
        Me.CmbRtgType.TabIndex = 19
        '
        'lblRtg
        '
        Me.lblRtg.AutoSize = True
        Me.lblRtg.Location = New System.Drawing.Point(5, 95)
        Me.lblRtg.Name = "lblRtg"
        Me.lblRtg.Size = New System.Drawing.Size(30, 13)
        Me.lblRtg.TabIndex = 16
        Me.lblRtg.Text = "RTG"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(26, 13)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Curr"
        '
        'CmbCurr
        '
        Me.CmbCurr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCurr.FormattingEnabled = True
        Me.CmbCurr.Items.AddRange(New Object() {"USD", "VND", "PCT"})
        Me.CmbCurr.Location = New System.Drawing.Point(57, 65)
        Me.CmbCurr.Name = "CmbCurr"
        Me.CmbCurr.Size = New System.Drawing.Size(51, 21)
        Me.CmbCurr.TabIndex = 18
        '
        'TxtPersonal
        '
        Me.TxtPersonal.Location = New System.Drawing.Point(360, 65)
        Me.TxtPersonal.Name = "TxtPersonal"
        Me.TxtPersonal.Size = New System.Drawing.Size(66, 20)
        Me.TxtPersonal.TabIndex = 22
        Me.TxtPersonal.Text = "0"
        Me.TxtPersonal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtAmount
        '
        Me.TxtAmount.Location = New System.Drawing.Point(184, 66)
        Me.TxtAmount.Name = "TxtAmount"
        Me.TxtAmount.Size = New System.Drawing.Size(53, 20)
        Me.TxtAmount.TabIndex = 23
        Me.TxtAmount.Text = "0"
        Me.TxtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(312, 72)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(48, 13)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "Personal"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(129, 69)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(43, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Amount"
        '
        'dtpValidThru
        '
        Me.dtpValidThru.CustomFormat = "dd MMM yy"
        Me.dtpValidThru.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpValidThru.Location = New System.Drawing.Point(224, 145)
        Me.dtpValidThru.Name = "dtpValidThru"
        Me.dtpValidThru.Size = New System.Drawing.Size(127, 20)
        Me.dtpValidThru.TabIndex = 27
        '
        'dtpValidFrom
        '
        Me.dtpValidFrom.CustomFormat = "dd MMM yy"
        Me.dtpValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpValidFrom.Location = New System.Drawing.Point(57, 146)
        Me.dtpValidFrom.Name = "dtpValidFrom"
        Me.dtpValidFrom.Size = New System.Drawing.Size(115, 20)
        Me.dtpValidFrom.TabIndex = 26
        '
        'label15
        '
        Me.label15.AutoSize = True
        Me.label15.Location = New System.Drawing.Point(179, 150)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(29, 13)
        Me.label15.TabIndex = 24
        Me.label15.Text = "Thru"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 150)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "Valid From"
        '
        'cmbSegTKT
        '
        Me.cmbSegTKT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSegTKT.FormattingEnabled = True
        Me.cmbSegTKT.Items.AddRange(New Object() {"TKT", "SEG"})
        Me.cmbSegTKT.Location = New System.Drawing.Point(110, 119)
        Me.cmbSegTKT.Name = "cmbSegTKT"
        Me.cmbSegTKT.Size = New System.Drawing.Size(81, 21)
        Me.cmbSegTKT.TabIndex = 32
        '
        'CmbCabin
        '
        Me.CmbCabin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCabin.FormattingEnabled = True
        Me.CmbCabin.Items.AddRange(New Object() {"*", "F", "C", "Y"})
        Me.CmbCabin.Location = New System.Drawing.Point(203, 92)
        Me.CmbCabin.Name = "CmbCabin"
        Me.CmbCabin.Size = New System.Drawing.Size(34, 21)
        Me.CmbCabin.TabIndex = 31
        '
        'CmbBase
        '
        Me.CmbBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbBase.FormattingEnabled = True
        Me.CmbBase.Items.AddRange(New Object() {"Fare", "F+T"})
        Me.CmbBase.Location = New System.Drawing.Point(57, 119)
        Me.CmbBase.Name = "CmbBase"
        Me.CmbBase.Size = New System.Drawing.Size(51, 21)
        Me.CmbBase.TabIndex = 30
        Me.CmbBase.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(5, 122)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(31, 13)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "Base"
        Me.Label12.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(142, 96)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(34, 13)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Cabin"
        '
        'CmbOWRT
        '
        Me.CmbOWRT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbOWRT.FormattingEnabled = True
        Me.CmbOWRT.Items.AddRange(New Object() {"--", "OW", "RT"})
        Me.CmbOWRT.Location = New System.Drawing.Point(303, 122)
        Me.CmbOWRT.Name = "CmbOWRT"
        Me.CmbOWRT.Size = New System.Drawing.Size(48, 21)
        Me.CmbOWRT.TabIndex = 37
        '
        'CmbAlType
        '
        Me.CmbAlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbAlType.FormattingEnabled = True
        Me.CmbAlType.Items.AddRange(New Object() {"LCC", "FSC"})
        Me.CmbAlType.Location = New System.Drawing.Point(319, 96)
        Me.CmbAlType.Name = "CmbAlType"
        Me.CmbAlType.Size = New System.Drawing.Size(53, 21)
        Me.CmbAlType.TabIndex = 36
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(243, 99)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(20, 13)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "AL"
        '
        'TxtFltTime
        '
        Me.TxtFltTime.Location = New System.Drawing.Point(260, 122)
        Me.TxtFltTime.Name = "TxtFltTime"
        Me.TxtFltTime.Size = New System.Drawing.Size(37, 20)
        Me.TxtFltTime.TabIndex = 35
        Me.TxtFltTime.Text = "0"
        Me.TxtFltTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(219, 125)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 13)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "FltTime"
        '
        'chkWzAIR
        '
        Me.chkWzAIR.AutoSize = True
        Me.chkWzAIR.Location = New System.Drawing.Point(57, 172)
        Me.chkWzAIR.Name = "chkWzAIR"
        Me.chkWzAIR.Size = New System.Drawing.Size(60, 17)
        Me.chkWzAIR.TabIndex = 39
        Me.chkWzAIR.Text = "WzAIR"
        Me.chkWzAIR.UseVisualStyleBackColor = True
        '
        'ChkVAT10
        '
        Me.ChkVAT10.AutoSize = True
        Me.ChkVAT10.Location = New System.Drawing.Point(132, 172)
        Me.ChkVAT10.Name = "ChkVAT10"
        Me.ChkVAT10.Size = New System.Drawing.Size(85, 17)
        Me.ChkVAT10.TabIndex = 38
        Me.ChkVAT10.Text = "VAT 10 Incl."
        Me.ChkVAT10.UseVisualStyleBackColor = True
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(15, 221)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 40
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'lbkSetPersonnal
        '
        Me.lbkSetPersonnal.AutoSize = True
        Me.lbkSetPersonnal.Location = New System.Drawing.Point(221, 221)
        Me.lbkSetPersonnal.Name = "lbkSetPersonnal"
        Me.lbkSetPersonnal.Size = New System.Drawing.Size(88, 13)
        Me.lbkSetPersonnal.TabIndex = 41
        Me.lbkSetPersonnal.TabStop = True
        Me.lbkSetPersonnal.Text = "Personal=50%Biz"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(221, 208)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(71, 13)
        Me.LinkLabel1.TabIndex = 42
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Validity(years)"
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(314, 201)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(37, 20)
        Me.txtYear.TabIndex = 43
        Me.txtYear.Text = "1"
        Me.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(5, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "CustGrp"
        '
        'cboCustGroup
        '
        Me.cboCustGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustGroup.FormattingEnabled = True
        Me.cboCustGroup.Location = New System.Drawing.Point(57, 11)
        Me.cboCustGroup.Name = "cboCustGroup"
        Me.cboCustGroup.Size = New System.Drawing.Size(235, 21)
        Me.cboCustGroup.TabIndex = 44
        '
        'chkVat8
        '
        Me.chkVat8.AutoSize = True
        Me.chkVat8.Location = New System.Drawing.Point(224, 171)
        Me.chkVat8.Name = "chkVat8"
        Me.chkVat8.Size = New System.Drawing.Size(79, 17)
        Me.chkVat8.TabIndex = 46
        Me.chkVat8.Text = "VAT 8 Incl."
        Me.chkVat8.UseVisualStyleBackColor = True
        '
        'frmAddCorpSF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(428, 261)
        Me.Controls.Add(Me.chkVat8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboCustGroup)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.lbkSetPersonnal)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.chkWzAIR)
        Me.Controls.Add(Me.ChkVAT10)
        Me.Controls.Add(Me.CmbOWRT)
        Me.Controls.Add(Me.CmbAlType)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtFltTime)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cmbSegTKT)
        Me.Controls.Add(Me.CmbCabin)
        Me.Controls.Add(Me.CmbBase)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.dtpValidThru)
        Me.Controls.Add(Me.dtpValidFrom)
        Me.Controls.Add(Me.label15)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TxtPersonal)
        Me.Controls.Add(Me.TxtAmount)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CmbRtgType)
        Me.Controls.Add(Me.lblRtg)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CmbCurr)
        Me.Controls.Add(Me.CmbTKT)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CmbTRX)
        Me.Controls.Add(Me.CmbService)
        Me.Controls.Add(Me.CmbCust)
        Me.Name = "frmAddCorpSF"
        Me.Text = "AddCorporate Service Fees"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CmbTKT As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents CmbTRX As ComboBox
    Friend WithEvents CmbService As ComboBox
    Friend WithEvents CmbCust As ComboBox
    Friend WithEvents CmbRtgType As ComboBox
    Friend WithEvents lblRtg As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents CmbCurr As ComboBox
    Friend WithEvents TxtPersonal As TextBox
    Friend WithEvents TxtAmount As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents dtpValidThru As DateTimePicker
    Friend WithEvents dtpValidFrom As DateTimePicker
    Friend WithEvents label15 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents cmbSegTKT As ComboBox
    Friend WithEvents CmbCabin As ComboBox
    Friend WithEvents CmbBase As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents CmbOWRT As ComboBox
    Friend WithEvents CmbAlType As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtFltTime As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents chkWzAIR As CheckBox
    Friend WithEvents ChkVAT10 As CheckBox
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents lbkSetPersonnal As LinkLabel
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents txtYear As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cboCustGroup As ComboBox
    Friend WithEvents chkVat8 As CheckBox
End Class
