<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSupplierList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.dgrSuppliers = New System.Windows.Forms.DataGridView()
        Me.lbkExpire = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.chkLast5CreatedByMyself = New System.Windows.Forms.CheckBox()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.chkMappedWzVendor = New System.Windows.Forms.CheckBox()
        CType(Me.dgrSuppliers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgrSuppliers
        '
        Me.dgrSuppliers.AllowUserToAddRows = False
        Me.dgrSuppliers.AllowUserToDeleteRows = False
        Me.dgrSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrSuppliers.Location = New System.Drawing.Point(5, 44)
        Me.dgrSuppliers.Name = "dgrSuppliers"
        Me.dgrSuppliers.ReadOnly = True
        Me.dgrSuppliers.Size = New System.Drawing.Size(978, 537)
        Me.dgrSuppliers.TabIndex = 55
        '
        'lbkExpire
        '
        Me.lbkExpire.AutoSize = True
        Me.lbkExpire.Location = New System.Drawing.Point(101, 594)
        Me.lbkExpire.Name = "lbkExpire"
        Me.lbkExpire.Size = New System.Drawing.Size(36, 13)
        Me.lbkExpire.TabIndex = 54
        Me.lbkExpire.TabStop = True
        Me.lbkExpire.Text = "Expire"
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(54, 594)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 53
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(7, 594)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 52
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(943, 20)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 51
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'chkLast5CreatedByMyself
        '
        Me.chkLast5CreatedByMyself.AutoSize = True
        Me.chkLast5CreatedByMyself.Location = New System.Drawing.Point(413, 18)
        Me.chkLast5CreatedByMyself.Name = "chkLast5CreatedByMyself"
        Me.chkLast5CreatedByMyself.Size = New System.Drawing.Size(131, 17)
        Me.chkLast5CreatedByMyself.TabIndex = 50
        Me.chkLast5CreatedByMyself.Text = "Last5CreatedByMyself"
        Me.chkLast5CreatedByMyself.UseVisualStyleBackColor = True
        '
        'txtFullName
        '
        Me.txtFullName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFullName.Location = New System.Drawing.Point(54, 17)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(232, 20)
        Me.txtFullName.TabIndex = 41
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(51, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "FullName"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"OK", "EX"})
        Me.cboStatus.Location = New System.Drawing.Point(5, 16)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(43, 21)
        Me.cboStatus.TabIndex = 39
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Status"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(896, 20)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 37
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'chkMappedWzVendor
        '
        Me.chkMappedWzVendor.AutoSize = True
        Me.chkMappedWzVendor.Checked = True
        Me.chkMappedWzVendor.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkMappedWzVendor.Location = New System.Drawing.Point(292, 19)
        Me.chkMappedWzVendor.Name = "chkMappedWzVendor"
        Me.chkMappedWzVendor.Size = New System.Drawing.Size(115, 17)
        Me.chkMappedWzVendor.TabIndex = 56
        Me.chkMappedWzVendor.Text = "MappedWzVendor"
        Me.chkMappedWzVendor.ThreeState = True
        Me.chkMappedWzVendor.UseVisualStyleBackColor = True
        '
        'frmSupplierList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 611)
        Me.Controls.Add(Me.chkMappedWzVendor)
        Me.Controls.Add(Me.dgrSuppliers)
        Me.Controls.Add(Me.lbkExpire)
        Me.Controls.Add(Me.lbkEdit)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.chkLast5CreatedByMyself)
        Me.Controls.Add(Me.txtFullName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbkSearch)
        Me.Name = "frmSupplierList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SupplierList"
        CType(Me.dgrSuppliers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgrSuppliers As DataGridView
    Friend WithEvents lbkExpire As LinkLabel
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents chkLast5CreatedByMyself As CheckBox
    Friend WithEvents txtFullName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents chkMappedWzVendor As CheckBox
End Class
