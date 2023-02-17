<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFocEdit
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
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboBizUnit = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboCls = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboDiscount = New System.Windows.Forms.ComboBox()
        Me.txtAL = New System.Windows.Forms.TextBox()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboCurrency = New System.Windows.Forms.ComboBox()
        Me.txtMinValue = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkTaxIncluded = New System.Windows.Forms.CheckBox()
        Me.dtpExpiryDate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkAlFormRequired = New System.Windows.Forms.CheckBox()
        Me.txtPO = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCondition = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtFocID = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtItinerary = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(170, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "RecId"
        '
        'txtRecId
        '
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(215, 0)
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(70, 20)
        Me.txtRecId.TabIndex = 1
        Me.txtRecId.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(79, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "AL"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(-2, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "BizUnit"
        '
        'cboBizUnit
        '
        Me.cboBizUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBizUnit.FormattingEnabled = True
        Me.cboBizUnit.Items.AddRange(New Object() {"1A", "CWT", "CWT/TVS", "GSA", "HAN", "MKTG", "TOUR", "TVS"})
        Me.cboBizUnit.Location = New System.Drawing.Point(2, 39)
        Me.cboBizUnit.Name = "cboBizUnit"
        Me.cboBizUnit.Size = New System.Drawing.Size(75, 21)
        Me.cboBizUnit.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(132, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 13)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Cls"
        '
        'cboCls
        '
        Me.cboCls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCls.FormattingEnabled = True
        Me.cboCls.Items.AddRange(New Object() {"Y", "C", "Y2C"})
        Me.cboCls.Location = New System.Drawing.Point(136, 39)
        Me.cboCls.Name = "cboCls"
        Me.cboCls.Size = New System.Drawing.Size(43, 21)
        Me.cboCls.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(181, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Discount"
        '
        'cboDiscount
        '
        Me.cboDiscount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDiscount.FormattingEnabled = True
        Me.cboDiscount.Items.AddRange(New Object() {"100", "90", "75", "50", "25"})
        Me.cboDiscount.Location = New System.Drawing.Point(185, 39)
        Me.cboDiscount.Name = "cboDiscount"
        Me.cboDiscount.Size = New System.Drawing.Size(43, 21)
        Me.cboDiscount.TabIndex = 3
        '
        'txtAL
        '
        Me.txtAL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAL.Location = New System.Drawing.Point(83, 40)
        Me.txtAL.Name = "txtAL"
        Me.txtAL.Size = New System.Drawing.Size(47, 20)
        Me.txtAL.TabIndex = 1
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(-1, 248)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 12
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(-3, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 13)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "Currency"
        '
        'cboCurrency
        '
        Me.cboCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCurrency.FormattingEnabled = True
        Me.cboCurrency.Items.AddRange(New Object() {"VND", "USD"})
        Me.cboCurrency.Location = New System.Drawing.Point(1, 80)
        Me.cboCurrency.Name = "cboCurrency"
        Me.cboCurrency.Size = New System.Drawing.Size(43, 21)
        Me.cboCurrency.TabIndex = 4
        '
        'txtMinValue
        '
        Me.txtMinValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMinValue.Location = New System.Drawing.Point(52, 81)
        Me.txtMinValue.Name = "txtMinValue"
        Me.txtMinValue.Size = New System.Drawing.Size(78, 20)
        Me.txtMinValue.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(48, 64)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 13)
        Me.Label7.TabIndex = 39
        Me.Label7.Text = "MinValue"
        '
        'chkTaxIncluded
        '
        Me.chkTaxIncluded.AutoSize = True
        Me.chkTaxIncluded.Location = New System.Drawing.Point(135, 67)
        Me.chkTaxIncluded.Name = "chkTaxIncluded"
        Me.chkTaxIncluded.Size = New System.Drawing.Size(85, 17)
        Me.chkTaxIncluded.TabIndex = 6
        Me.chkTaxIncluded.Text = "TaxIncluded"
        Me.chkTaxIncluded.UseVisualStyleBackColor = True
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.CustomFormat = "dd MMM yy"
        Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpiryDate.Location = New System.Drawing.Point(0, 160)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.Size = New System.Drawing.Size(77, 20)
        Me.dtpExpiryDate.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(-1, 144)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 13)
        Me.Label8.TabIndex = 43
        Me.Label8.Text = "ExpiryDate"
        '
        'chkAlFormRequired
        '
        Me.chkAlFormRequired.AutoSize = True
        Me.chkAlFormRequired.Location = New System.Drawing.Point(135, 84)
        Me.chkAlFormRequired.Name = "chkAlFormRequired"
        Me.chkAlFormRequired.Size = New System.Drawing.Size(101, 17)
        Me.chkAlFormRequired.TabIndex = 7
        Me.chkAlFormRequired.Text = "AlFormRequired"
        Me.chkAlFormRequired.UseVisualStyleBackColor = True
        '
        'txtPO
        '
        Me.txtPO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPO.Location = New System.Drawing.Point(83, 160)
        Me.txtPO.MaxLength = 32
        Me.txtPO.Name = "txtPO"
        Me.txtPO.Size = New System.Drawing.Size(202, 20)
        Me.txtPO.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(79, 143)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(22, 13)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "PO"
        '
        'txtCondition
        '
        Me.txtCondition.Location = New System.Drawing.Point(0, 202)
        Me.txtCondition.MaxLength = 128
        Me.txtCondition.Multiline = True
        Me.txtCondition.Name = "txtCondition"
        Me.txtCondition.Size = New System.Drawing.Size(285, 47)
        Me.txtCondition.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(-1, 186)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(51, 13)
        Me.Label10.TabIndex = 47
        Me.Label10.Text = "Condition"
        '
        'txtFocID
        '
        Me.txtFocID.Enabled = False
        Me.txtFocID.Location = New System.Drawing.Point(94, 0)
        Me.txtFocID.Name = "txtFocID"
        Me.txtFocID.Size = New System.Drawing.Size(70, 20)
        Me.txtFocID.TabIndex = 49
        Me.txtFocID.Text = "0"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(49, 7)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 13)
        Me.Label11.TabIndex = 48
        Me.Label11.Text = "FocID"
        '
        'txtItinerary
        '
        Me.txtItinerary.Location = New System.Drawing.Point(3, 120)
        Me.txtItinerary.MaxLength = 32
        Me.txtItinerary.Name = "txtItinerary"
        Me.txtItinerary.Size = New System.Drawing.Size(282, 20)
        Me.txtItinerary.TabIndex = 8
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(0, 104)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(44, 13)
        Me.Label12.TabIndex = 51
        Me.Label12.Text = "Itinerary"
        '
        'frmFocEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.txtItinerary)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtFocID)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtCondition)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtPO)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.chkAlFormRequired)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.dtpExpiryDate)
        Me.Controls.Add(Me.chkTaxIncluded)
        Me.Controls.Add(Me.txtMinValue)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboCurrency)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.txtAL)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboDiscount)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboCls)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboBizUnit)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmFocEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FOC Edit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cboBizUnit As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cboCls As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cboDiscount As ComboBox
    Friend WithEvents txtAL As TextBox
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents Label6 As Label
    Friend WithEvents cboCurrency As ComboBox
    Friend WithEvents txtMinValue As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents chkTaxIncluded As CheckBox
    Friend WithEvents dtpExpiryDate As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents chkAlFormRequired As CheckBox
    Friend WithEvents txtPO As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtCondition As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtFocID As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtItinerary As TextBox
    Friend WithEvents Label12 As Label
End Class
