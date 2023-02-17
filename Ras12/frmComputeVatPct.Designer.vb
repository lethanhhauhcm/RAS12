<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmComputeVatPct
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
        Me.dtpFromDOI = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDOI = New System.Windows.Forms.DateTimePicker()
        Me.cboVatPctRaw = New System.Windows.Forms.ComboBox()
        Me.cboVatPctRounded = New System.Windows.Forms.ComboBox()
        Me.cboDiff = New System.Windows.Forms.ComboBox()
        Me.cboCounter = New System.Windows.Forms.ComboBox()
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.dgrTkts = New System.Windows.Forms.DataGridView()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.cboSRV = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbkChecked = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.chkVatChecked = New System.Windows.Forms.CheckBox()
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "DOI From/To"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(274, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "VatPctRounded"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(207, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "VatPctRaw"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(354, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(23, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Diff"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(422, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Counter"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(488, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Customer"
        '
        'dtpFromDOI
        '
        Me.dtpFromDOI.CustomFormat = "dd MMM yy"
        Me.dtpFromDOI.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDOI.Location = New System.Drawing.Point(8, 17)
        Me.dtpFromDOI.Name = "dtpFromDOI"
        Me.dtpFromDOI.Size = New System.Drawing.Size(94, 20)
        Me.dtpFromDOI.TabIndex = 6
        '
        'dtpToDOI
        '
        Me.dtpToDOI.CustomFormat = "dd MMM yy"
        Me.dtpToDOI.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDOI.Location = New System.Drawing.Point(108, 17)
        Me.dtpToDOI.Name = "dtpToDOI"
        Me.dtpToDOI.Size = New System.Drawing.Size(94, 20)
        Me.dtpToDOI.TabIndex = 7
        '
        'cboVatPctRaw
        '
        Me.cboVatPctRaw.FormattingEnabled = True
        Me.cboVatPctRaw.Location = New System.Drawing.Point(210, 17)
        Me.cboVatPctRaw.Name = "cboVatPctRaw"
        Me.cboVatPctRaw.Size = New System.Drawing.Size(61, 21)
        Me.cboVatPctRaw.TabIndex = 8
        '
        'cboVatPctRounded
        '
        Me.cboVatPctRounded.FormattingEnabled = True
        Me.cboVatPctRounded.Location = New System.Drawing.Point(277, 17)
        Me.cboVatPctRounded.Name = "cboVatPctRounded"
        Me.cboVatPctRounded.Size = New System.Drawing.Size(57, 21)
        Me.cboVatPctRounded.TabIndex = 9
        '
        'cboDiff
        '
        Me.cboDiff.FormattingEnabled = True
        Me.cboDiff.Items.AddRange(New Object() {"0.00-0.50", "0.51-1.00", "1.01-100"})
        Me.cboDiff.Location = New System.Drawing.Point(357, 17)
        Me.cboDiff.Name = "cboDiff"
        Me.cboDiff.Size = New System.Drawing.Size(62, 21)
        Me.cboDiff.TabIndex = 10
        '
        'cboCounter
        '
        Me.cboCounter.FormattingEnabled = True
        Me.cboCounter.Items.AddRange(New Object() {"TVS", "CWT"})
        Me.cboCounter.Location = New System.Drawing.Point(425, 17)
        Me.cboCounter.Name = "cboCounter"
        Me.cboCounter.Size = New System.Drawing.Size(57, 21)
        Me.cboCounter.TabIndex = 11
        '
        'cboCustShortName
        '
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(491, 17)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(273, 21)
        Me.cboCustShortName.TabIndex = 12
        '
        'dgrTkts
        '
        Me.dgrTkts.AllowUserToAddRows = False
        Me.dgrTkts.AllowUserToDeleteRows = False
        Me.dgrTkts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTkts.Location = New System.Drawing.Point(8, 43)
        Me.dgrTkts.Name = "dgrTkts"
        Me.dgrTkts.ReadOnly = True
        Me.dgrTkts.Size = New System.Drawing.Size(864, 486)
        Me.dgrTkts.TabIndex = 13
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(944, 53)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 14
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(944, 20)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 15
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'cboSRV
        '
        Me.cboSRV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSRV.FormattingEnabled = True
        Me.cboSRV.Items.AddRange(New Object() {"S", "R"})
        Me.cboSRV.Location = New System.Drawing.Point(770, 16)
        Me.cboSRV.Name = "cboSRV"
        Me.cboSRV.Size = New System.Drawing.Size(47, 21)
        Me.cboSRV.TabIndex = 16
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(767, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "SRV"
        '
        'lbkChecked
        '
        Me.lbkChecked.AutoSize = True
        Me.lbkChecked.Location = New System.Drawing.Point(12, 539)
        Me.lbkChecked.Name = "lbkChecked"
        Me.lbkChecked.Size = New System.Drawing.Size(50, 13)
        Me.lbkChecked.TabIndex = 18
        Me.lbkChecked.TabStop = True
        Me.lbkChecked.Text = "Checked"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(0, 0)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(59, 13)
        Me.LinkLabel1.TabIndex = 19
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "LinkLabel1"
        '
        'chkVatChecked
        '
        Me.chkVatChecked.AutoSize = True
        Me.chkVatChecked.Location = New System.Drawing.Point(823, 18)
        Me.chkVatChecked.Name = "chkVatChecked"
        Me.chkVatChecked.Size = New System.Drawing.Size(85, 17)
        Me.chkVatChecked.TabIndex = 20
        Me.chkVatChecked.Text = "VatChecked"
        Me.chkVatChecked.UseVisualStyleBackColor = True
        '
        'frmComputeVatPct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 561)
        Me.Controls.Add(Me.chkVatChecked)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.lbkChecked)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboSRV)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrTkts)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Controls.Add(Me.cboCounter)
        Me.Controls.Add(Me.cboDiff)
        Me.Controls.Add(Me.cboVatPctRounded)
        Me.Controls.Add(Me.cboVatPctRaw)
        Me.Controls.Add(Me.dtpToDOI)
        Me.Controls.Add(Me.dtpFromDOI)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmComputeVatPct"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ComputeVatPct"
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents dtpFromDOI As DateTimePicker
    Friend WithEvents dtpToDOI As DateTimePicker
    Friend WithEvents cboVatPctRaw As ComboBox
    Friend WithEvents cboVatPctRounded As ComboBox
    Friend WithEvents cboDiff As ComboBox
    Friend WithEvents cboCounter As ComboBox
    Friend WithEvents cboCustShortName As ComboBox
    Friend WithEvents dgrTkts As DataGridView
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents cboSRV As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents lbkChecked As LinkLabel
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents chkVatChecked As CheckBox
End Class
