<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFocList
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
        Me.lbkDelete = New System.Windows.Forms.LinkLabel()
        Me.lbkEdit = New System.Windows.Forms.LinkLabel()
        Me.lbkClone = New System.Windows.Forms.LinkLabel()
        Me.dgrFoc = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboBizUnit = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lbkClear = New System.Windows.Forms.LinkLabel()
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboAL = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboExpire = New System.Windows.Forms.ComboBox()
        Me.lbkNew = New System.Windows.Forms.LinkLabel()
        Me.lbkAccept = New System.Windows.Forms.LinkLabel()
        Me.lbkUndoAccept = New System.Windows.Forms.LinkLabel()
        Me.lbkRequest = New System.Windows.Forms.LinkLabel()
        Me.lbkPrint = New System.Windows.Forms.LinkLabel()
        Me.txtCondition = New System.Windows.Forms.TextBox()
        CType(Me.dgrFoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(161, 539)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 19
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        Me.lbkDelete.Visible = False
        '
        'lbkEdit
        '
        Me.lbkEdit.AutoSize = True
        Me.lbkEdit.Location = New System.Drawing.Point(107, 539)
        Me.lbkEdit.Name = "lbkEdit"
        Me.lbkEdit.Size = New System.Drawing.Size(25, 13)
        Me.lbkEdit.TabIndex = 18
        Me.lbkEdit.TabStop = True
        Me.lbkEdit.Text = "Edit"
        Me.lbkEdit.Visible = False
        '
        'lbkClone
        '
        Me.lbkClone.AutoSize = True
        Me.lbkClone.Location = New System.Drawing.Point(54, 539)
        Me.lbkClone.Name = "lbkClone"
        Me.lbkClone.Size = New System.Drawing.Size(34, 13)
        Me.lbkClone.TabIndex = 17
        Me.lbkClone.TabStop = True
        Me.lbkClone.Text = "Clone"
        '
        'dgrFoc
        '
        Me.dgrFoc.AllowUserToAddRows = False
        Me.dgrFoc.AllowUserToDeleteRows = False
        Me.dgrFoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrFoc.BackgroundColor = System.Drawing.Color.Honeydew
        Me.dgrFoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrFoc.Location = New System.Drawing.Point(12, 51)
        Me.dgrFoc.Name = "dgrFoc"
        Me.dgrFoc.RowHeadersVisible = False
        Me.dgrFoc.Size = New System.Drawing.Size(860, 374)
        Me.dgrFoc.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "BizUnit"
        '
        'cboBizUnit
        '
        Me.cboBizUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBizUnit.FormattingEnabled = True
        Me.cboBizUnit.Items.AddRange(New Object() {"1A", "CWT", "CWT/TVS", "GSA", "HAN", "MKTG", "TOUR", "TVS"})
        Me.cboBizUnit.Location = New System.Drawing.Point(13, 24)
        Me.cboBizUnit.Name = "cboBizUnit"
        Me.cboBizUnit.Size = New System.Drawing.Size(75, 21)
        Me.cboBizUnit.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(90, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Status"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"--", "QQ", "OK", "RR", "XX"})
        Me.cboStatus.Location = New System.Drawing.Point(94, 24)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(75, 21)
        Me.cboStatus.TabIndex = 20
        '
        'lbkClear
        '
        Me.lbkClear.AutoSize = True
        Me.lbkClear.Location = New System.Drawing.Point(538, 32)
        Me.lbkClear.Name = "lbkClear"
        Me.lbkClear.Size = New System.Drawing.Size(31, 13)
        Me.lbkClear.TabIndex = 22
        Me.lbkClear.TabStop = True
        Me.lbkClear.Text = "Clear"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(480, 32)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 23
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(171, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "AL"
        '
        'cboAL
        '
        Me.cboAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAL.FormattingEnabled = True
        Me.cboAL.Items.AddRange(New Object() {"--", "QQ", "OK", "XX"})
        Me.cboAL.Location = New System.Drawing.Point(175, 24)
        Me.cboAL.Name = "cboAL"
        Me.cboAL.Size = New System.Drawing.Size(43, 21)
        Me.cboAL.TabIndex = 25
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(234, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Expire"
        '
        'cboExpire
        '
        Me.cboExpire.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExpire.FormattingEnabled = True
        Me.cboExpire.Items.AddRange(New Object() {"In 15 days", "Before Today"})
        Me.cboExpire.Location = New System.Drawing.Point(224, 25)
        Me.cboExpire.Name = "cboExpire"
        Me.cboExpire.Size = New System.Drawing.Size(83, 21)
        Me.cboExpire.TabIndex = 28
        '
        'lbkNew
        '
        Me.lbkNew.AutoSize = True
        Me.lbkNew.Location = New System.Drawing.Point(9, 539)
        Me.lbkNew.Name = "lbkNew"
        Me.lbkNew.Size = New System.Drawing.Size(29, 13)
        Me.lbkNew.TabIndex = 29
        Me.lbkNew.TabStop = True
        Me.lbkNew.Text = "New"
        '
        'lbkAccept
        '
        Me.lbkAccept.AutoSize = True
        Me.lbkAccept.Location = New System.Drawing.Point(328, 539)
        Me.lbkAccept.Name = "lbkAccept"
        Me.lbkAccept.Size = New System.Drawing.Size(41, 13)
        Me.lbkAccept.TabIndex = 30
        Me.lbkAccept.TabStop = True
        Me.lbkAccept.Text = "Accept"
        Me.lbkAccept.Visible = False
        '
        'lbkUndoAccept
        '
        Me.lbkUndoAccept.AutoSize = True
        Me.lbkUndoAccept.Location = New System.Drawing.Point(366, 539)
        Me.lbkUndoAccept.Name = "lbkUndoAccept"
        Me.lbkUndoAccept.Size = New System.Drawing.Size(67, 13)
        Me.lbkUndoAccept.TabIndex = 31
        Me.lbkUndoAccept.TabStop = True
        Me.lbkUndoAccept.Text = "UndoAccept"
        Me.lbkUndoAccept.Visible = False
        '
        'lbkRequest
        '
        Me.lbkRequest.AutoSize = True
        Me.lbkRequest.Location = New System.Drawing.Point(221, 539)
        Me.lbkRequest.Name = "lbkRequest"
        Me.lbkRequest.Size = New System.Drawing.Size(47, 13)
        Me.lbkRequest.TabIndex = 32
        Me.lbkRequest.TabStop = True
        Me.lbkRequest.Text = "Request"
        Me.lbkRequest.Visible = False
        '
        'lbkPrint
        '
        Me.lbkPrint.AutoSize = True
        Me.lbkPrint.Location = New System.Drawing.Point(531, 539)
        Me.lbkPrint.Name = "lbkPrint"
        Me.lbkPrint.Size = New System.Drawing.Size(28, 13)
        Me.lbkPrint.TabIndex = 33
        Me.lbkPrint.TabStop = True
        Me.lbkPrint.Text = "Print"
        Me.lbkPrint.Visible = False
        '
        'txtCondition
        '
        Me.txtCondition.Location = New System.Drawing.Point(12, 431)
        Me.txtCondition.MaxLength = 128
        Me.txtCondition.Multiline = True
        Me.txtCondition.Name = "txtCondition"
        Me.txtCondition.Size = New System.Drawing.Size(860, 47)
        Me.txtCondition.TabIndex = 34
        '
        'frmFocList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.Controls.Add(Me.txtCondition)
        Me.Controls.Add(Me.lbkPrint)
        Me.Controls.Add(Me.lbkRequest)
        Me.Controls.Add(Me.lbkUndoAccept)
        Me.Controls.Add(Me.lbkAccept)
        Me.Controls.Add(Me.lbkNew)
        Me.Controls.Add(Me.cboExpire)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboAL)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.lbkClear)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.lbkEdit)
        Me.Controls.Add(Me.lbkClone)
        Me.Controls.Add(Me.dgrFoc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboBizUnit)
        Me.Name = "frmFocList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmFocList"
        CType(Me.dgrFoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents lbkEdit As LinkLabel
    Friend WithEvents lbkClone As LinkLabel
    Friend WithEvents dgrFoc As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cboBizUnit As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents lbkClear As LinkLabel
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents Label3 As Label
    Friend WithEvents cboAL As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cboExpire As ComboBox
    Friend WithEvents lbkNew As LinkLabel
    Friend WithEvents lbkAccept As LinkLabel
    Friend WithEvents lbkUndoAccept As LinkLabel
    Friend WithEvents lbkRequest As LinkLabel
    Friend WithEvents lbkPrint As LinkLabel
    Friend WithEvents txtCondition As TextBox
End Class
