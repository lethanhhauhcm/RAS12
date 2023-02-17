<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCitiBankMapping
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
        Me.dgrTvBank = New System.Windows.Forms.DataGridView()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.txtBankCode = New System.Windows.Forms.TextBox()
        Me.lbkShowAllTv = New System.Windows.Forms.LinkLabel()
        Me.lbkSearchTV = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTvBankName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbkMapCitiBankCode = New System.Windows.Forms.LinkLabel()
        Me.lbkOutSideVietnam = New System.Windows.Forms.LinkLabel()
        Me.dgrCitiBankCode = New System.Windows.Forms.DataGridView()
        Me.lbkShowAllCiti = New System.Windows.Forms.LinkLabel()
        Me.lbkSearchCiti = New System.Windows.Forms.LinkLabel()
        Me.chkIncludeAgriBankl = New System.Windows.Forms.CheckBox()
        Me.lbkViewVendors = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrTvBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrCitiBankCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrTvBank
        '
        Me.dgrTvBank.AllowUserToAddRows = False
        Me.dgrTvBank.AllowUserToDeleteRows = False
        Me.dgrTvBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTvBank.Location = New System.Drawing.Point(12, 48)
        Me.dgrTvBank.Name = "dgrTvBank"
        Me.dgrTvBank.ReadOnly = True
        Me.dgrTvBank.Size = New System.Drawing.Size(398, 496)
        Me.dgrTvBank.TabIndex = 14
        '
        'txtBankName
        '
        Me.txtBankName.Location = New System.Drawing.Point(538, 25)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(129, 20)
        Me.txtBankName.TabIndex = 13
        '
        'txtBankCode
        '
        Me.txtBankCode.Location = New System.Drawing.Point(429, 25)
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(100, 20)
        Me.txtBankCode.TabIndex = 12
        '
        'lbkShowAllTv
        '
        Me.lbkShowAllTv.AutoSize = True
        Me.lbkShowAllTv.Location = New System.Drawing.Point(211, 28)
        Me.lbkShowAllTv.Name = "lbkShowAllTv"
        Me.lbkShowAllTv.Size = New System.Drawing.Size(45, 13)
        Me.lbkShowAllTv.TabIndex = 11
        Me.lbkShowAllTv.TabStop = True
        Me.lbkShowAllTv.Text = "ShowAll"
        '
        'lbkSearchTV
        '
        Me.lbkSearchTV.AutoSize = True
        Me.lbkSearchTV.Location = New System.Drawing.Point(164, 28)
        Me.lbkSearchTV.Name = "lbkSearchTV"
        Me.lbkSearchTV.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearchTV.TabIndex = 10
        Me.lbkSearchTV.TabStop = True
        Me.lbkSearchTV.Text = "Search"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(535, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "CitiBankName"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(426, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "CitiBankCode"
        '
        'txtTvBankName
        '
        Me.txtTvBankName.Location = New System.Drawing.Point(12, 25)
        Me.txtTvBankName.Name = "txtTvBankName"
        Me.txtTvBankName.Size = New System.Drawing.Size(129, 20)
        Me.txtTvBankName.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "TvBankName"
        '
        'lbkMapCitiBankCode
        '
        Me.lbkMapCitiBankCode.AutoSize = True
        Me.lbkMapCitiBankCode.Location = New System.Drawing.Point(426, 551)
        Me.lbkMapCitiBankCode.Name = "lbkMapCitiBankCode"
        Me.lbkMapCitiBankCode.Size = New System.Drawing.Size(92, 13)
        Me.lbkMapCitiBankCode.TabIndex = 19
        Me.lbkMapCitiBankCode.TabStop = True
        Me.lbkMapCitiBankCode.Text = "MapCitiBankCode"
        '
        'lbkOutSideVietnam
        '
        Me.lbkOutSideVietnam.AutoSize = True
        Me.lbkOutSideVietnam.Location = New System.Drawing.Point(12, 551)
        Me.lbkOutSideVietnam.Name = "lbkOutSideVietnam"
        Me.lbkOutSideVietnam.Size = New System.Drawing.Size(130, 13)
        Me.lbkOutSideVietnam.TabIndex = 20
        Me.lbkOutSideVietnam.TabStop = True
        Me.lbkOutSideVietnam.Text = "SetBank=OutSideVietnam"
        '
        'dgrCitiBankCode
        '
        Me.dgrCitiBankCode.AllowUserToAddRows = False
        Me.dgrCitiBankCode.AllowUserToDeleteRows = False
        Me.dgrCitiBankCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrCitiBankCode.Location = New System.Drawing.Point(429, 50)
        Me.dgrCitiBankCode.Name = "dgrCitiBankCode"
        Me.dgrCitiBankCode.ReadOnly = True
        Me.dgrCitiBankCode.Size = New System.Drawing.Size(575, 494)
        Me.dgrCitiBankCode.TabIndex = 21
        '
        'lbkShowAllCiti
        '
        Me.lbkShowAllCiti.AutoSize = True
        Me.lbkShowAllCiti.Location = New System.Drawing.Point(842, 28)
        Me.lbkShowAllCiti.Name = "lbkShowAllCiti"
        Me.lbkShowAllCiti.Size = New System.Drawing.Size(45, 13)
        Me.lbkShowAllCiti.TabIndex = 23
        Me.lbkShowAllCiti.TabStop = True
        Me.lbkShowAllCiti.Text = "ShowAll"
        '
        'lbkSearchCiti
        '
        Me.lbkSearchCiti.AutoSize = True
        Me.lbkSearchCiti.Location = New System.Drawing.Point(795, 28)
        Me.lbkSearchCiti.Name = "lbkSearchCiti"
        Me.lbkSearchCiti.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearchCiti.TabIndex = 22
        Me.lbkSearchCiti.TabStop = True
        Me.lbkSearchCiti.Text = "Search"
        '
        'chkIncludeAgriBankl
        '
        Me.chkIncludeAgriBankl.AutoSize = True
        Me.chkIncludeAgriBankl.Location = New System.Drawing.Point(673, 24)
        Me.chkIncludeAgriBankl.Name = "chkIncludeAgriBankl"
        Me.chkIncludeAgriBankl.Size = New System.Drawing.Size(104, 17)
        Me.chkIncludeAgriBankl.TabIndex = 24
        Me.chkIncludeAgriBankl.Text = "IncludeAgriBank"
        Me.chkIncludeAgriBankl.UseVisualStyleBackColor = True
        '
        'lbkViewVendors
        '
        Me.lbkViewVendors.AutoSize = True
        Me.lbkViewVendors.Location = New System.Drawing.Point(164, 551)
        Me.lbkViewVendors.Name = "lbkViewVendors"
        Me.lbkViewVendors.Size = New System.Drawing.Size(69, 13)
        Me.lbkViewVendors.TabIndex = 25
        Me.lbkViewVendors.TabStop = True
        Me.lbkViewVendors.Text = "ViewVendors"
        '
        'frmCitiBankMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 573)
        Me.Controls.Add(Me.lbkViewVendors)
        Me.Controls.Add(Me.chkIncludeAgriBankl)
        Me.Controls.Add(Me.lbkShowAllCiti)
        Me.Controls.Add(Me.lbkSearchCiti)
        Me.Controls.Add(Me.dgrCitiBankCode)
        Me.Controls.Add(Me.lbkOutSideVietnam)
        Me.Controls.Add(Me.lbkMapCitiBankCode)
        Me.Controls.Add(Me.txtTvBankName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgrTvBank)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.txtBankCode)
        Me.Controls.Add(Me.lbkShowAllTv)
        Me.Controls.Add(Me.lbkSearchTV)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmCitiBankMapping"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CitiBankMapping"
        CType(Me.dgrTvBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrCitiBankCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgrTvBank As System.Windows.Forms.DataGridView
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents txtBankCode As System.Windows.Forms.TextBox
    Friend WithEvents lbkShowAllTv As System.Windows.Forms.LinkLabel
    Friend WithEvents lbkSearchTV As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTvBankName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbkMapCitiBankCode As System.Windows.Forms.LinkLabel
    Friend WithEvents lbkOutSideVietnam As System.Windows.Forms.LinkLabel
    Friend WithEvents dgrCitiBankCode As System.Windows.Forms.DataGridView
    Friend WithEvents lbkShowAllCiti As System.Windows.Forms.LinkLabel
    Friend WithEvents lbkSearchCiti As System.Windows.Forms.LinkLabel
    Friend WithEvents chkIncludeAgriBankl As System.Windows.Forms.CheckBox
    Friend WithEvents lbkViewVendors As System.Windows.Forms.LinkLabel
End Class
