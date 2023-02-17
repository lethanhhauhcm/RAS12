<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAmadeusCommand
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.dgrSegs = New System.Windows.Forms.DataGridView()
        Me.FromCity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToCity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Car = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FltNbr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FltDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ETD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ETA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumDay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgrTkts = New System.Windows.Forms.DataGridView()
        Me.TKNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaxName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaxType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BkgCls = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgrSegs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(344, 542)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.CausesValidation = False
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(99, 542)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 14
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'dgrSegs
        '
        Me.dgrSegs.AllowUserToAddRows = False
        Me.dgrSegs.AllowUserToDeleteRows = False
        Me.dgrSegs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrSegs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrSegs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FromCity, Me.ToCity, Me.Car, Me.FltNbr, Me.FltDate, Me.ETD, Me.ETA, Me.NumDay})
        Me.dgrSegs.Location = New System.Drawing.Point(12, 12)
        Me.dgrSegs.Name = "dgrSegs"
        Me.dgrSegs.Size = New System.Drawing.Size(536, 127)
        Me.dgrSegs.TabIndex = 17
        '
        'FromCity
        '
        Me.FromCity.HeaderText = "FromCity"
        Me.FromCity.Name = "FromCity"
        Me.FromCity.ReadOnly = True
        Me.FromCity.Width = 72
        '
        'ToCity
        '
        Me.ToCity.HeaderText = "ToCity"
        Me.ToCity.Name = "ToCity"
        Me.ToCity.ReadOnly = True
        Me.ToCity.Width = 62
        '
        'Car
        '
        Me.Car.HeaderText = "Car"
        Me.Car.Name = "Car"
        Me.Car.ReadOnly = True
        Me.Car.Width = 48
        '
        'FltNbr
        '
        Me.FltNbr.HeaderText = "FltNbr"
        Me.FltNbr.Name = "FltNbr"
        Me.FltNbr.ReadOnly = True
        Me.FltNbr.Width = 60
        '
        'FltDate
        '
        Me.FltDate.HeaderText = "FltDate"
        Me.FltDate.Name = "FltDate"
        Me.FltDate.ReadOnly = True
        Me.FltDate.Width = 66
        '
        'ETD
        '
        Me.ETD.HeaderText = "ETD"
        Me.ETD.Name = "ETD"
        Me.ETD.ReadOnly = True
        Me.ETD.Width = 54
        '
        'ETA
        '
        Me.ETA.HeaderText = "ETA"
        Me.ETA.Name = "ETA"
        Me.ETA.ReadOnly = True
        Me.ETA.Width = 53
        '
        'NumDay
        '
        Me.NumDay.HeaderText = "NumDay"
        Me.NumDay.Name = "NumDay"
        Me.NumDay.Width = 73
        '
        'dgrTkts
        '
        Me.dgrTkts.AllowUserToAddRows = False
        Me.dgrTkts.AllowUserToDeleteRows = False
        Me.dgrTkts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgrTkts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrTkts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TKNO, Me.PaxName, Me.PaxType, Me.BkgCls, Me.Tel})
        Me.dgrTkts.Location = New System.Drawing.Point(12, 145)
        Me.dgrTkts.Name = "dgrTkts"
        Me.dgrTkts.Size = New System.Drawing.Size(536, 391)
        Me.dgrTkts.TabIndex = 16
        '
        'TKNO
        '
        Me.TKNO.HeaderText = "TKNO"
        Me.TKNO.Name = "TKNO"
        Me.TKNO.ReadOnly = True
        Me.TKNO.Width = 62
        '
        'PaxName
        '
        Me.PaxName.HeaderText = "PaxName"
        Me.PaxName.Name = "PaxName"
        Me.PaxName.ReadOnly = True
        Me.PaxName.Width = 78
        '
        'PaxType
        '
        Me.PaxType.HeaderText = "PaxType"
        Me.PaxType.Name = "PaxType"
        Me.PaxType.ReadOnly = True
        Me.PaxType.Width = 74
        '
        'BkgCls
        '
        Me.BkgCls.HeaderText = "BkgCls"
        Me.BkgCls.Name = "BkgCls"
        Me.BkgCls.ReadOnly = True
        Me.BkgCls.Width = 65
        '
        'Tel
        '
        Me.Tel.HeaderText = "Tel"
        Me.Tel.Name = "Tel"
        Me.Tel.Width = 47
        '
        'frmAmadeusCommand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(565, 573)
        Me.Controls.Add(Me.dgrSegs)
        Me.Controls.Add(Me.dgrTkts)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Name = "frmAmadeusCommand"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmAmadeusCommand"
        CType(Me.dgrSegs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgrTkts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents dgrSegs As DataGridView
    Friend WithEvents dgrTkts As DataGridView
    Friend WithEvents FromCity As DataGridViewTextBoxColumn
    Friend WithEvents ToCity As DataGridViewTextBoxColumn
    Friend WithEvents Car As DataGridViewTextBoxColumn
    Friend WithEvents FltNbr As DataGridViewTextBoxColumn
    Friend WithEvents FltDate As DataGridViewTextBoxColumn
    Friend WithEvents ETD As DataGridViewTextBoxColumn
    Friend WithEvents ETA As DataGridViewTextBoxColumn
    Friend WithEvents NumDay As DataGridViewTextBoxColumn
    Friend WithEvents TKNO As DataGridViewTextBoxColumn
    Friend WithEvents PaxName As DataGridViewTextBoxColumn
    Friend WithEvents PaxType As DataGridViewTextBoxColumn
    Friend WithEvents BkgCls As DataGridViewTextBoxColumn
    Friend WithEvents Tel As DataGridViewTextBoxColumn
End Class
