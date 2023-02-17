<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVjInvoiceList
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
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.chkHasInvoice = New System.Windows.Forms.CheckBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.dgrInvoices = New System.Windows.Forms.DataGridView()
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPaxName = New System.Windows.Forms.TextBox()
        Me.txtRloc = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtNewInvNo = New System.Windows.Forms.TextBox()
        Me.lbkUpdate = New System.Windows.Forms.LinkLabel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblImportExcel = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(939, 21)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 37
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'chkHasInvoice
        '
        Me.chkHasInvoice.AutoSize = True
        Me.chkHasInvoice.Location = New System.Drawing.Point(738, 18)
        Me.chkHasInvoice.Name = "chkHasInvoice"
        Me.chkHasInvoice.Size = New System.Drawing.Size(80, 17)
        Me.chkHasInvoice.TabIndex = 36
        Me.chkHasInvoice.Text = "HasInvoice"
        Me.chkHasInvoice.ThreeState = True
        Me.chkHasInvoice.UseVisualStyleBackColor = True
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "XX"})
        Me.cboStatus.Location = New System.Drawing.Point(1, 17)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(43, 21)
        Me.cboStatus.TabIndex = 35
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(-2, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Status"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(892, 21)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 33
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'dgrInvoices
        '
        Me.dgrInvoices.AllowUserToAddRows = False
        Me.dgrInvoices.AllowUserToDeleteRows = False
        Me.dgrInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrInvoices.Location = New System.Drawing.Point(1, 39)
        Me.dgrInvoices.Name = "dgrInvoices"
        Me.dgrInvoices.ReadOnly = True
        Me.dgrInvoices.Size = New System.Drawing.Size(978, 478)
        Me.dgrInvoices.TabIndex = 32
        '
        'cboCustShortName
        '
        Me.cboCustShortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Items.AddRange(New Object() {"OK", "EX"})
        Me.cboCustShortName.Location = New System.Drawing.Point(55, 17)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(135, 21)
        Me.cboCustShortName.TabIndex = 39
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(52, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Customer"
        '
        'txtPaxName
        '
        Me.txtPaxName.Location = New System.Drawing.Point(280, 16)
        Me.txtPaxName.Name = "txtPaxName"
        Me.txtPaxName.Size = New System.Drawing.Size(179, 20)
        Me.txtPaxName.TabIndex = 40
        '
        'txtRloc
        '
        Me.txtRloc.Location = New System.Drawing.Point(196, 16)
        Me.txtRloc.Name = "txtRloc"
        Me.txtRloc.Size = New System.Drawing.Size(78, 20)
        Me.txtRloc.TabIndex = 41
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(193, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "RLOC"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(277, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "PaxName"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(-2, 520)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 47
        Me.Label5.Text = "New InvoiceNo"
        '
        'txtNewInvNo
        '
        Me.txtNewInvNo.Location = New System.Drawing.Point(1, 536)
        Me.txtNewInvNo.Name = "txtNewInvNo"
        Me.txtNewInvNo.Size = New System.Drawing.Size(78, 20)
        Me.txtNewInvNo.TabIndex = 46
        '
        'lbkUpdate
        '
        Me.lbkUpdate.AutoSize = True
        Me.lbkUpdate.Location = New System.Drawing.Point(104, 520)
        Me.lbkUpdate.Name = "lbkUpdate"
        Me.lbkUpdate.Size = New System.Drawing.Size(42, 13)
        Me.lbkUpdate.TabIndex = 48
        Me.lbkUpdate.TabStop = True
        Me.lbkUpdate.Text = "Update"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(462, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "InvoiceNo"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(465, 16)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(61, 20)
        Me.txtInvoiceNo.TabIndex = 49
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(533, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 13)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "PaidDate From/To"
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd MMM yy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(536, 15)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(95, 20)
        Me.dtpFrom.TabIndex = 52
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd MMM yy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(637, 15)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(95, 20)
        Me.dtpTo.TabIndex = 53
        '
        'lblImportExcel
        '
        Me.lblImportExcel.AutoSize = True
        Me.lblImportExcel.Location = New System.Drawing.Point(776, 520)
        Me.lblImportExcel.Name = "lblImportExcel"
        Me.lblImportExcel.Size = New System.Drawing.Size(62, 13)
        Me.lblImportExcel.TabIndex = 54
        Me.lblImportExcel.TabStop = True
        Me.lblImportExcel.Text = "ImportExcel"
        '
        'frmVjInvoiceList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 561)
        Me.Controls.Add(Me.lblImportExcel)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.lbkUpdate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtNewInvNo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtRloc)
        Me.Controls.Add(Me.txtPaxName)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.chkHasInvoice)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.dgrInvoices)
        Me.Name = "frmVjInvoiceList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmVjInvoiceList"
        CType(Me.dgrInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents chkHasInvoice As CheckBox
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents dgrInvoices As DataGridView
    Friend WithEvents cboCustShortName As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPaxName As TextBox
    Friend WithEvents txtRloc As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtNewInvNo As TextBox
    Friend WithEvents lbkUpdate As LinkLabel
    Friend WithEvents Label6 As Label
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents lblImportExcel As LinkLabel
End Class
