<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmE_InvReport
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
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.cboMauSo = New System.Windows.Forms.ComboBox()
        Me.lbkRun = New System.Windows.Forms.LinkLabel()
        Me.cboTVC = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboThongTu = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "FromDate"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "ToDate"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 127)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "MauSo"
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd MMM yy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(82, 33)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(94, 20)
        Me.dtpFrom.TabIndex = 3
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd MMM yy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(82, 63)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(94, 20)
        Me.dtpTo.TabIndex = 4
        '
        'cboMauSo
        '
        Me.cboMauSo.FormattingEnabled = True
        Me.cboMauSo.Location = New System.Drawing.Point(82, 119)
        Me.cboMauSo.Name = "cboMauSo"
        Me.cboMauSo.Size = New System.Drawing.Size(94, 21)
        Me.cboMauSo.TabIndex = 5
        '
        'lbkRun
        '
        Me.lbkRun.AutoSize = True
        Me.lbkRun.Location = New System.Drawing.Point(239, 106)
        Me.lbkRun.Name = "lbkRun"
        Me.lbkRun.Size = New System.Drawing.Size(27, 13)
        Me.lbkRun.TabIndex = 6
        Me.lbkRun.TabStop = True
        Me.lbkRun.Text = "Run"
        '
        'cboTVC
        '
        Me.cboTVC.FormattingEnabled = True
        Me.cboTVC.Location = New System.Drawing.Point(82, 89)
        Me.cboTVC.Name = "cboTVC"
        Me.cboTVC.Size = New System.Drawing.Size(94, 21)
        Me.cboTVC.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "TVC"
        '
        'cboThongTu
        '
        Me.cboThongTu.FormattingEnabled = True
        Me.cboThongTu.Items.AddRange(New Object() {"32", "78"})
        Me.cboThongTu.Location = New System.Drawing.Point(82, 6)
        Me.cboThongTu.Name = "cboThongTu"
        Me.cboThongTu.Size = New System.Drawing.Size(94, 21)
        Me.cboThongTu.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Thông tư"
        '
        'frmE_InvReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 155)
        Me.Controls.Add(Me.cboThongTu)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboTVC)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbkRun)
        Me.Controls.Add(Me.cboMauSo)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmE_InvReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "E_Invoice Report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents cboMauSo As ComboBox
    Friend WithEvents lbkRun As LinkLabel
    Friend WithEvents cboTVC As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cboThongTu As ComboBox
    Friend WithEvents Label5 As Label
End Class
