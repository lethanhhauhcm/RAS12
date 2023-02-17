<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHotelRateEdit
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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.chkBreakfast = New System.Windows.Forms.CheckBox()
        Me.chkServiceCharge = New System.Windows.Forms.CheckBox()
        Me.txtSupplierId = New System.Windows.Forms.TextBox()
        Me.txtContactPerson = New System.Windows.Forms.TextBox()
        Me.txtHotelName = New System.Windows.Forms.TextBox()
        Me.txtContactTel = New System.Windows.Forms.TextBox()
        Me.txtContactEmail = New System.Windows.Forms.TextBox()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.cboRoomCat = New System.Windows.Forms.ComboBox()
        Me.cboRoomType = New System.Windows.Forms.ComboBox()
        Me.lbkSearchHotel = New System.Windows.Forms.LinkLabel()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.cboValidTo = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cboValidFrom = New System.Windows.Forms.ComboBox()
        Me.txtCustGroup = New System.Windows.Forms.TextBox()
        Me.lbkCustGroup = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SupplierId"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(72, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "HotelName"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "ContactPerson"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(342, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "ContactTel"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 100)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "ContactEmail"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 145)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "RoomCat"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(192, 145)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "RoomType"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(296, 145)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(30, 13)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Rate"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(385, 145)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "ValidFrom"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(473, 145)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(43, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "ValidTo"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 239)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(44, 13)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Remark"
        '
        'chkBreakfast
        '
        Me.chkBreakfast.AutoSize = True
        Me.chkBreakfast.Location = New System.Drawing.Point(299, 193)
        Me.chkBreakfast.Name = "chkBreakfast"
        Me.chkBreakfast.Size = New System.Drawing.Size(71, 17)
        Me.chkBreakfast.TabIndex = 12
        Me.chkBreakfast.Text = "Breakfast"
        Me.chkBreakfast.UseVisualStyleBackColor = True
        '
        'chkServiceCharge
        '
        Me.chkServiceCharge.AutoSize = True
        Me.chkServiceCharge.Location = New System.Drawing.Point(195, 193)
        Me.chkServiceCharge.Name = "chkServiceCharge"
        Me.chkServiceCharge.Size = New System.Drawing.Size(96, 17)
        Me.chkServiceCharge.TabIndex = 13
        Me.chkServiceCharge.Text = "ServiceCharge"
        Me.chkServiceCharge.UseVisualStyleBackColor = True
        '
        'txtSupplierId
        '
        Me.txtSupplierId.Enabled = False
        Me.txtSupplierId.Location = New System.Drawing.Point(12, 25)
        Me.txtSupplierId.Name = "txtSupplierId"
        Me.txtSupplierId.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSupplierId.Size = New System.Drawing.Size(54, 20)
        Me.txtSupplierId.TabIndex = 14
        Me.txtSupplierId.Text = "0"
        '
        'txtContactPerson
        '
        Me.txtContactPerson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtContactPerson.Location = New System.Drawing.Point(12, 72)
        Me.txtContactPerson.Name = "txtContactPerson"
        Me.txtContactPerson.Size = New System.Drawing.Size(321, 20)
        Me.txtContactPerson.TabIndex = 2
        '
        'txtHotelName
        '
        Me.txtHotelName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtHotelName.Enabled = False
        Me.txtHotelName.Location = New System.Drawing.Point(75, 25)
        Me.txtHotelName.Name = "txtHotelName"
        Me.txtHotelName.Size = New System.Drawing.Size(258, 20)
        Me.txtHotelName.TabIndex = 16
        '
        'txtContactTel
        '
        Me.txtContactTel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtContactTel.Location = New System.Drawing.Point(339, 72)
        Me.txtContactTel.MaxLength = 16
        Me.txtContactTel.Name = "txtContactTel"
        Me.txtContactTel.Size = New System.Drawing.Size(222, 20)
        Me.txtContactTel.TabIndex = 3
        '
        'txtContactEmail
        '
        Me.txtContactEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtContactEmail.Location = New System.Drawing.Point(12, 116)
        Me.txtContactEmail.Name = "txtContactEmail"
        Me.txtContactEmail.Size = New System.Drawing.Size(549, 20)
        Me.txtContactEmail.TabIndex = 4
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(299, 161)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtRate.Size = New System.Drawing.Size(82, 20)
        Me.txtRate.TabIndex = 7
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRemark
        '
        Me.txtRemark.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemark.Location = New System.Drawing.Point(9, 253)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(549, 59)
        Me.txtRemark.TabIndex = 11
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "ddMMMyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(476, 161)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(83, 20)
        Me.dtpTo.TabIndex = 9
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "ddMMMyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(387, 161)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(83, 20)
        Me.dtpFrom.TabIndex = 8
        '
        'cboRoomCat
        '
        Me.cboRoomCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRoomCat.FormattingEnabled = True
        Me.cboRoomCat.Location = New System.Drawing.Point(15, 160)
        Me.cboRoomCat.Name = "cboRoomCat"
        Me.cboRoomCat.Size = New System.Drawing.Size(174, 21)
        Me.cboRoomCat.TabIndex = 5
        '
        'cboRoomType
        '
        Me.cboRoomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRoomType.FormattingEnabled = True
        Me.cboRoomType.Location = New System.Drawing.Point(195, 161)
        Me.cboRoomType.Name = "cboRoomType"
        Me.cboRoomType.Size = New System.Drawing.Size(94, 21)
        Me.cboRoomType.TabIndex = 6
        '
        'lbkSearchHotel
        '
        Me.lbkSearchHotel.AutoSize = True
        Me.lbkSearchHotel.Location = New System.Drawing.Point(267, 9)
        Me.lbkSearchHotel.Name = "lbkSearchHotel"
        Me.lbkSearchHotel.Size = New System.Drawing.Size(66, 13)
        Me.lbkSearchHotel.TabIndex = 0
        Me.lbkSearchHotel.TabStop = True
        Me.lbkSearchHotel.Text = "SearchHotel"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(257, 315)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 12
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'cboValidTo
        '
        Me.cboValidTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboValidTo.FormattingEnabled = True
        Me.cboValidTo.Items.AddRange(New Object() {"1Year", "2Years", "EndOfYear", "EndOfNextYear"})
        Me.cboValidTo.Location = New System.Drawing.Point(476, 216)
        Me.cboValidTo.Name = "cboValidTo"
        Me.cboValidTo.Size = New System.Drawing.Size(83, 21)
        Me.cboValidTo.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(385, 220)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 13)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "QuickValildTo"
        '
        'txtRecId
        '
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(507, 25)
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtRecId.Size = New System.Drawing.Size(54, 20)
        Me.txtRecId.TabIndex = 31
        Me.txtRecId.Text = "0"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(504, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(36, 13)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "RecId"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(384, 194)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(83, 13)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "QuickValildFrom"
        '
        'cboValidFrom
        '
        Me.cboValidFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboValidFrom.FormattingEnabled = True
        Me.cboValidFrom.Items.AddRange(New Object() {"NextYear", "ContinuePrevious"})
        Me.cboValidFrom.Location = New System.Drawing.Point(475, 190)
        Me.cboValidFrom.Name = "cboValidFrom"
        Me.cboValidFrom.Size = New System.Drawing.Size(83, 21)
        Me.cboValidFrom.TabIndex = 32
        '
        'txtCustGroup
        '
        Me.txtCustGroup.Enabled = False
        Me.txtCustGroup.Location = New System.Drawing.Point(12, 213)
        Me.txtCustGroup.Name = "txtCustGroup"
        Me.txtCustGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCustGroup.Size = New System.Drawing.Size(177, 20)
        Me.txtCustGroup.TabIndex = 34
        Me.txtCustGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbkCustGroup
        '
        Me.lbkCustGroup.AutoSize = True
        Me.lbkCustGroup.Location = New System.Drawing.Point(9, 193)
        Me.lbkCustGroup.Name = "lbkCustGroup"
        Me.lbkCustGroup.Size = New System.Drawing.Size(57, 13)
        Me.lbkCustGroup.TabIndex = 35
        Me.lbkCustGroup.TabStop = True
        Me.lbkCustGroup.Text = "CustGroup"
        '
        'frmHotelRateEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 347)
        Me.Controls.Add(Me.lbkCustGroup)
        Me.Controls.Add(Me.txtCustGroup)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cboValidFrom)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cboValidTo)
        Me.Controls.Add(Me.lbkSearchHotel)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.cboRoomType)
        Me.Controls.Add(Me.cboRoomCat)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.txtRate)
        Me.Controls.Add(Me.txtContactEmail)
        Me.Controls.Add(Me.txtContactTel)
        Me.Controls.Add(Me.txtHotelName)
        Me.Controls.Add(Me.txtContactPerson)
        Me.Controls.Add(Me.txtSupplierId)
        Me.Controls.Add(Me.chkServiceCharge)
        Me.Controls.Add(Me.chkBreakfast)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmHotelRateEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HotelRateEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents chkBreakfast As CheckBox
    Friend WithEvents chkServiceCharge As CheckBox
    Friend WithEvents txtSupplierId As TextBox
    Friend WithEvents txtContactPerson As TextBox
    Friend WithEvents txtHotelName As TextBox
    Friend WithEvents txtContactTel As TextBox
    Friend WithEvents txtContactEmail As TextBox
    Friend WithEvents txtRate As TextBox
    Friend WithEvents txtRemark As TextBox
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents cboRoomCat As ComboBox
    Friend WithEvents cboRoomType As ComboBox
    Friend WithEvents lbkSearchHotel As LinkLabel
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents cboValidTo As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents cboValidFrom As ComboBox
    Friend WithEvents txtCustGroup As TextBox
    Friend WithEvents lbkCustGroup As LinkLabel
End Class
