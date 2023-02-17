<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAcrAdd
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
        Me.lbkSave = New System.Windows.Forms.LinkLabel()
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.txtPaxNames = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.txtRcpNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDocuments = New System.Windows.Forms.TextBox()
        Me.lbkSelectRcp = New System.Windows.Forms.LinkLabel()
        Me.txtAL = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "AccountName"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "PaxNames"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Amount"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 146)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Documents"
        '
        'lbkSave
        '
        Me.lbkSave.AutoSize = True
        Me.lbkSave.Location = New System.Drawing.Point(9, 173)
        Me.lbkSave.Name = "lbkSave"
        Me.lbkSave.Size = New System.Drawing.Size(32, 13)
        Me.lbkSave.TabIndex = 7
        Me.lbkSave.TabStop = True
        Me.lbkSave.Text = "Save"
        '
        'txtAccountName
        '
        Me.txtAccountName.Enabled = False
        Me.txtAccountName.Location = New System.Drawing.Point(90, 34)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.Size = New System.Drawing.Size(233, 20)
        Me.txtAccountName.TabIndex = 1
        '
        'txtPaxNames
        '
        Me.txtPaxNames.Enabled = False
        Me.txtPaxNames.Location = New System.Drawing.Point(90, 61)
        Me.txtPaxNames.Name = "txtPaxNames"
        Me.txtPaxNames.Size = New System.Drawing.Size(233, 20)
        Me.txtPaxNames.TabIndex = 2
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(90, 84)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(125, 20)
        Me.txtAmount.TabIndex = 3
        '
        'txtRcpNo
        '
        Me.txtRcpNo.Location = New System.Drawing.Point(90, 112)
        Me.txtRcpNo.Name = "txtRcpNo"
        Me.txtRcpNo.Size = New System.Drawing.Size(125, 20)
        Me.txtRcpNo.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "RcpNo"
        '
        'txtDocuments
        '
        Me.txtDocuments.Location = New System.Drawing.Point(90, 138)
        Me.txtDocuments.Name = "txtDocuments"
        Me.txtDocuments.Size = New System.Drawing.Size(125, 20)
        Me.txtDocuments.TabIndex = 6
        '
        'lbkSelectRcp
        '
        Me.lbkSelectRcp.AutoSize = True
        Me.lbkSelectRcp.Location = New System.Drawing.Point(291, 119)
        Me.lbkSelectRcp.Name = "lbkSelectRcp"
        Me.lbkSelectRcp.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelectRcp.TabIndex = 5
        Me.lbkSelectRcp.TabStop = True
        Me.lbkSelectRcp.Text = "Select"
        '
        'txtAL
        '
        Me.txtAL.Enabled = False
        Me.txtAL.Location = New System.Drawing.Point(90, 8)
        Me.txtAL.Name = "txtAL"
        Me.txtAL.Size = New System.Drawing.Size(51, 20)
        Me.txtAL.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "AL"
        '
        'frmAcrAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 261)
        Me.Controls.Add(Me.txtAL)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lbkSelectRcp)
        Me.Controls.Add(Me.txtDocuments)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtRcpNo)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.txtPaxNames)
        Me.Controls.Add(Me.txtAccountName)
        Me.Controls.Add(Me.lbkSave)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmAcrAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AcrAdd"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lbkSave As LinkLabel
    Friend WithEvents txtAccountName As TextBox
    Friend WithEvents txtPaxNames As TextBox
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents txtRcpNo As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtDocuments As TextBox
    Friend WithEvents lbkSelectRcp As LinkLabel
    Friend WithEvents txtAL As TextBox
    Friend WithEvents Label6 As Label
End Class
