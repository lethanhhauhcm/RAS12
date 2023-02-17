<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHotelRates
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
        Me.dgrHotelRates = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboProvince = New System.Windows.Forms.ComboBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.cboDateType = New System.Windows.Forms.ComboBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.txtHotelName = New System.Windows.Forms.TextBox()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkClone = New System.Windows.Forms.LinkLabel()
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        Me.cboDistrict = New System.Windows.Forms.ComboBox()
        Me.lbkCustGrp = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrHotelRates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrHotelRates
        '
        Me.dgrHotelRates.AllowUserToAddRows = False
        Me.dgrHotelRates.AllowUserToDeleteRows = False
        Me.dgrHotelRates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrHotelRates.Location = New System.Drawing.Point(15, 56)
        Me.dgrHotelRates.Name = "dgrHotelRates"
        Me.dgrHotelRates.ReadOnly = True
        Me.dgrHotelRates.Size = New System.Drawing.Size(981, 524)
        Me.dgrHotelRates.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Province"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(196, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "District"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(370, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "HotelName"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(543, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Status"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(602, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Valid"
        '
        'cboProvince
        '
        Me.cboProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProvince.FormattingEnabled = True
        Me.cboProvince.Location = New System.Drawing.Point(15, 30)
        Me.cboProvince.Name = "cboProvince"
        Me.cboProvince.Size = New System.Drawing.Size(178, 21)
        Me.cboProvince.TabIndex = 0
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "XX"})
        Me.cboStatus.Location = New System.Drawing.Point(546, 29)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(52, 21)
        Me.cboStatus.TabIndex = 3
        '
        'cboDateType
        '
        Me.cboDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDateType.FormattingEnabled = True
        Me.cboDateType.Items.AddRange(New Object() {"OnDate", "FromDate", "ToDate", "Between"})
        Me.cboDateType.Location = New System.Drawing.Point(604, 29)
        Me.cboDateType.Name = "cboDateType"
        Me.cboDateType.Size = New System.Drawing.Size(99, 21)
        Me.cboDateType.TabIndex = 4
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "ddMMMyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(709, 30)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(83, 20)
        Me.dtpFrom.TabIndex = 5
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "ddMMMyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(798, 30)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(83, 20)
        Me.dtpTo.TabIndex = 6
        '
        'txtHotelName
        '
        Me.txtHotelName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtHotelName.Location = New System.Drawing.Point(373, 30)
        Me.txtHotelName.Name = "txtHotelName"
        Me.txtHotelName.Size = New System.Drawing.Size(165, 20)
        Me.txtHotelName.TabIndex = 2
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(887, 33)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 7
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(934, 33)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 8
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(59, 589)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 11
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(12, 589)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 10
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkClone
        '
        Me.lbkClone.AutoSize = True
        Me.lbkClone.Location = New System.Drawing.Point(106, 589)
        Me.lbkClone.Name = "lbkClone"
        Me.lbkClone.Size = New System.Drawing.Size(34, 13)
        Me.lbkClone.TabIndex = 12
        Me.lbkClone.TabStop = True
        Me.lbkClone.Text = "Clone"
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(292, 589)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 13
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'cboDistrict
        '
        Me.cboDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistrict.FormattingEnabled = True
        Me.cboDistrict.Location = New System.Drawing.Point(199, 30)
        Me.cboDistrict.Name = "cboDistrict"
        Me.cboDistrict.Size = New System.Drawing.Size(168, 21)
        Me.cboDistrict.TabIndex = 14
        '
        'lbkCustGrp
        '
        Me.lbkCustGrp.AutoSize = True
        Me.lbkCustGrp.Location = New System.Drawing.Point(887, 9)
        Me.lbkCustGrp.Name = "lbkCustGrp"
        Me.lbkCustGrp.Size = New System.Drawing.Size(57, 13)
        Me.lbkCustGrp.TabIndex = 15
        Me.lbkCustGrp.TabStop = True
        Me.lbkCustGrp.Text = "CustGroup"
        '
        'frmHotelRates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.lbkCustGrp)
        Me.Controls.Add(Me.cboDistrict)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.lbkClone)
        Me.Controls.Add(Me.lbkEdit)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.txtHotelName)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.cboDateType)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.cboProvince)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgrHotelRates)
        Me.Name = "frmHotelRates"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HotelRates"
        CType(Me.dgrHotelRates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrHotelRates As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cboProvince As ComboBox
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents cboDateType As ComboBox
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents txtHotelName As TextBox
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkClone As LinkLabel
    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents cboDistrict As ComboBox
    Friend WithEvents lbkCustGrp As LinkLabel
End Class
