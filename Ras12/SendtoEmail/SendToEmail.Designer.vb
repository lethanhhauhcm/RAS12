<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SendToEmail
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
        Me.GridEmail = New System.Windows.Forms.DataGridView()
        Me.CbbWhereEmail = New System.Windows.Forms.ComboBox()
        Me.tbVal = New System.Windows.Forms.TextBox()
        Me.TbDetails = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbCity = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TbNotify = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        CType(Me.GridEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridEmail
        '
        Me.GridEmail.AllowUserToAddRows = False
        Me.GridEmail.AllowUserToDeleteRows = False
        Me.GridEmail.BackgroundColor = System.Drawing.SystemColors.MenuBar
        Me.GridEmail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridEmail.Location = New System.Drawing.Point(12, 39)
        Me.GridEmail.MultiSelect = False
        Me.GridEmail.Name = "GridEmail"
        Me.GridEmail.ReadOnly = True
        Me.GridEmail.Size = New System.Drawing.Size(799, 373)
        Me.GridEmail.TabIndex = 0
        '
        'CbbWhereEmail
        '
        Me.CbbWhereEmail.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.CbbWhereEmail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbbWhereEmail.FormattingEnabled = True
        Me.CbbWhereEmail.Location = New System.Drawing.Point(12, 12)
        Me.CbbWhereEmail.Name = "CbbWhereEmail"
        Me.CbbWhereEmail.Size = New System.Drawing.Size(107, 21)
        Me.CbbWhereEmail.TabIndex = 1
        '
        'tbVal
        '
        Me.tbVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbVal.Location = New System.Drawing.Point(77, 42)
        Me.tbVal.Multiline = True
        Me.tbVal.Name = "tbVal"
        Me.tbVal.Size = New System.Drawing.Size(180, 26)
        Me.tbVal.TabIndex = 2
        '
        'TbDetails
        '
        Me.TbDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbDetails.Location = New System.Drawing.Point(77, 84)
        Me.TbDetails.Multiline = True
        Me.TbDetails.Name = "TbDetails"
        Me.TbDetails.Size = New System.Drawing.Size(524, 28)
        Me.TbDetails.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbCity)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TbNotify)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TbDetails)
        Me.GroupBox1.Controls.Add(Me.tbVal)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 422)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(619, 209)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thông tin"
        '
        'cbCity
        '
        Me.cbCity.AllowDrop = True
        Me.cbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCity.FormattingEnabled = True
        Me.cbCity.Items.AddRange(New Object() {"SGN", "HAN"})
        Me.cbCity.Location = New System.Drawing.Point(391, 38)
        Me.cbCity.Name = "cbCity"
        Me.cbCity.Size = New System.Drawing.Size(210, 28)
        Me.cbCity.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AllowDrop = True
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(320, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 25)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "City"
        '
        'Label3
        '
        Me.Label3.AllowDrop = True
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 25)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Notify"
        '
        'TbNotify
        '
        Me.TbNotify.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbNotify.Location = New System.Drawing.Point(77, 127)
        Me.TbNotify.Multiline = True
        Me.TbNotify.Name = "TbNotify"
        Me.TbNotify.Size = New System.Drawing.Size(524, 64)
        Me.TbNotify.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 25)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Email"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 25)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Val"
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(651, 422)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(160, 56)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Thêm"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(651, 495)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(160, 56)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = "Xóa"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(651, 575)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(160, 56)
        Me.btnUpdate.TabIndex = 10
        Me.btnUpdate.Text = "Sửa"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'SendToEmail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.BackgroundImage = Global.RAS12.My.Resources.Resources.Z_Wing1
        Me.ClientSize = New System.Drawing.Size(823, 672)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CbbWhereEmail)
        Me.Controls.Add(Me.GridEmail)
        Me.Name = "SendToEmail"
        Me.Text = "SendToEmail"
        CType(Me.GridEmail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridEmail As DataGridView
    Friend WithEvents CbbWhereEmail As ComboBox
    Friend WithEvents tbVal As TextBox
    Friend WithEvents TbDetails As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Public WithEvents Label3 As Label
    Friend WithEvents TbNotify As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents Label1 As Label
    Public WithEvents Label4 As Label
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents cbCity As ComboBox
End Class
