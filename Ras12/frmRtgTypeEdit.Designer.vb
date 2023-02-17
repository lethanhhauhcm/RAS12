<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRtgTypeEdit
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
        Me.cboCustShortName = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboRtgType = New System.Windows.Forms.ComboBox()
        Me.txtCountries = New System.Windows.Forms.TextBox()
        Me.lbkGetDefaultCountries = New System.Windows.Forms.LinkLabel()
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.txtRecId = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lbkContinueFromPrevious = New System.Windows.Forms.LinkLabel()
        Me.lbkAdd1Year = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Customer"
        '
        'cboCustShortName
        '
        Me.cboCustShortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustShortName.FormattingEnabled = True
        Me.cboCustShortName.Location = New System.Drawing.Point(61, 12)
        Me.cboCustShortName.Name = "cboCustShortName"
        Me.cboCustShortName.Size = New System.Drawing.Size(162, 21)
        Me.cboCustShortName.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "RtgType"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Countries"
        '
        'cboRtgType
        '
        Me.cboRtgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRtgType.FormattingEnabled = True
        Me.cboRtgType.Items.AddRange(New Object() {"REG", "REG2"})
        Me.cboRtgType.Location = New System.Drawing.Point(61, 46)
        Me.cboRtgType.Name = "cboRtgType"
        Me.cboRtgType.Size = New System.Drawing.Size(162, 21)
        Me.cboRtgType.TabIndex = 14
        '
        'txtCountries
        '
        Me.txtCountries.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCountries.Location = New System.Drawing.Point(61, 77)
        Me.txtCountries.Name = "txtCountries"
        Me.txtCountries.Size = New System.Drawing.Size(791, 20)
        Me.txtCountries.TabIndex = 15
        '
        'lbkGetDefaultCountries
        '
        Me.lbkGetDefaultCountries.AutoSize = True
        Me.lbkGetDefaultCountries.Location = New System.Drawing.Point(370, 61)
        Me.lbkGetDefaultCountries.Name = "lbkGetDefaultCountries"
        Me.lbkGetDefaultCountries.Size = New System.Drawing.Size(102, 13)
        Me.lbkGetDefaultCountries.TabIndex = 16
        Me.lbkGetDefaultCountries.TabStop = True
        Me.lbkGetDefaultCountries.Text = "GetDefaultCountries"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(366, 111)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 17
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'txtRecId
        '
        Me.txtRecId.Enabled = False
        Me.txtRecId.Location = New System.Drawing.Point(408, 12)
        Me.txtRecId.Name = "txtRecId"
        Me.txtRecId.Size = New System.Drawing.Size(64, 20)
        Me.txtRecId.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(366, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "RecId"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "From/To"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd MMM yy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(61, 105)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(96, 20)
        Me.dtpFromDate.TabIndex = 21
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd MMM yy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(163, 105)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(96, 20)
        Me.dtpToDate.TabIndex = 22
        '
        'lbkContinueFromPrevious
        '
        Me.lbkContinueFromPrevious.AutoSize = True
        Me.lbkContinueFromPrevious.Location = New System.Drawing.Point(58, 128)
        Me.lbkContinueFromPrevious.Name = "lbkContinueFromPrevious"
        Me.lbkContinueFromPrevious.Size = New System.Drawing.Size(113, 13)
        Me.lbkContinueFromPrevious.TabIndex = 23
        Me.lbkContinueFromPrevious.TabStop = True
        Me.lbkContinueFromPrevious.Text = "ContinueFromPrevious"
        '
        'lbkAdd1Year
        '
        Me.lbkAdd1Year.AutoSize = True
        Me.lbkAdd1Year.Location = New System.Drawing.Point(177, 128)
        Me.lbkAdd1Year.Name = "lbkAdd1Year"
        Me.lbkAdd1Year.Size = New System.Drawing.Size(77, 13)
        Me.lbkAdd1Year.TabIndex = 24
        Me.lbkAdd1Year.TabStop = True
        Me.lbkAdd1Year.Text = "ToDate+1Year"
        '
        'frmRtgTypeEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(859, 165)
        Me.Controls.Add(Me.lbkAdd1Year)
        Me.Controls.Add(Me.lbkContinueFromPrevious)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtRecId)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.lbkGetDefaultCountries)
        Me.Controls.Add(Me.txtCountries)
        Me.Controls.Add(Me.cboRtgType)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboCustShortName)
        Me.Name = "frmRtgTypeEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RtgTypeEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cboCustShortName As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cboRtgType As ComboBox
    Friend WithEvents txtCountries As TextBox
    Friend WithEvents lbkGetDefaultCountries As LinkLabel
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents txtRecId As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpFromDate As DateTimePicker
    Friend WithEvents dtpToDate As DateTimePicker
    Friend WithEvents lbkContinueFromPrevious As LinkLabel
    Friend WithEvents lbkAdd1Year As LinkLabel
End Class
