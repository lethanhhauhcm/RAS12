<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPushData2Report
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
        Me.cboCustomer = New System.Windows.Forms.ComboBox()
        Me.lbkPushData = New System.Windows.Forms.LinkLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbkFromEqualTo = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'cboCustomer
        '
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(73, 38)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(164, 21)
        Me.cboCustomer.TabIndex = 60
        '
        'lbkPushData
        '
        Me.lbkPushData.AutoSize = True
        Me.lbkPushData.Location = New System.Drawing.Point(70, 72)
        Me.lbkPushData.Name = "lbkPushData"
        Me.lbkPushData.Size = New System.Drawing.Size(54, 13)
        Me.lbkPushData.TabIndex = 56
        Me.lbkPushData.TabStop = True
        Me.lbkPushData.Text = "PushData"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "DOI Btwn"
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd MMM yy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(73, 12)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(69, 20)
        Me.dtpFrom.TabIndex = 58
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd MMM yy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(168, 12)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(69, 20)
        Me.dtpTo.TabIndex = 59
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Customer"
        '
        'lbkFromEqualTo
        '
        Me.lbkFromEqualTo.AutoSize = True
        Me.lbkFromEqualTo.Location = New System.Drawing.Point(142, 72)
        Me.lbkFromEqualTo.Name = "lbkFromEqualTo"
        Me.lbkFromEqualTo.Size = New System.Drawing.Size(95, 13)
        Me.lbkFromEqualTo.TabIndex = 62
        Me.lbkFromEqualTo.TabStop = True
        Me.lbkFromEqualTo.Text = "FromDate=ToDate"
        '
        'frmPushData2Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(279, 107)
        Me.Controls.Add(Me.lbkFromEqualTo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboCustomer)
        Me.Controls.Add(Me.lbkPushData)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.dtpTo)
        Me.Name = "frmPushData2Report"
        Me.Text = "frmPushData2Report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboCustomer As ComboBox
    Friend WithEvents lbkPushData As LinkLabel
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents lbkFromEqualTo As LinkLabel
End Class
