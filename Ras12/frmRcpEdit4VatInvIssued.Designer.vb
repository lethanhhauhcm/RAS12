<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRcpEdit4VatInvIssued
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
        Me.dgrRcp = New System.Windows.Forms.DataGridView()
        Me.lbkRequest = New System.Windows.Forms.LinkLabel()
        Me.txtRcpNo = New System.Windows.Forms.TextBox()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.RCP = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbkApprove = New System.Windows.Forms.LinkLabel()
        Me.lbkReject = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrRcp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbkDelete
        '
        Me.lbkDelete.AutoSize = True
        Me.lbkDelete.Location = New System.Drawing.Point(12, 537)
        Me.lbkDelete.Name = "lbkDelete"
        Me.lbkDelete.Size = New System.Drawing.Size(38, 13)
        Me.lbkDelete.TabIndex = 7
        Me.lbkDelete.TabStop = True
        Me.lbkDelete.Text = "Delete"
        '
        'dgrRcp
        '
        Me.dgrRcp.AllowUserToAddRows = False
        Me.dgrRcp.AllowUserToDeleteRows = False
        Me.dgrRcp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrRcp.Location = New System.Drawing.Point(12, 59)
        Me.dgrRcp.Name = "dgrRcp"
        Me.dgrRcp.ReadOnly = True
        Me.dgrRcp.Size = New System.Drawing.Size(760, 465)
        Me.dgrRcp.TabIndex = 6
        '
        'lbkRequest
        '
        Me.lbkRequest.AutoSize = True
        Me.lbkRequest.Location = New System.Drawing.Point(695, 36)
        Me.lbkRequest.Name = "lbkRequest"
        Me.lbkRequest.Size = New System.Drawing.Size(47, 13)
        Me.lbkRequest.TabIndex = 5
        Me.lbkRequest.TabStop = True
        Me.lbkRequest.Text = "Request"
        '
        'txtRcpNo
        '
        Me.txtRcpNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRcpNo.Location = New System.Drawing.Point(15, 33)
        Me.txtRcpNo.Name = "txtRcpNo"
        Me.txtRcpNo.Size = New System.Drawing.Size(167, 20)
        Me.txtRcpNo.TabIndex = 4
        '
        'txtReason
        '
        Me.txtReason.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReason.Location = New System.Drawing.Point(188, 33)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(483, 20)
        Me.txtReason.TabIndex = 8
        '
        'RCP
        '
        Me.RCP.AutoSize = True
        Me.RCP.Location = New System.Drawing.Point(12, 17)
        Me.RCP.Name = "RCP"
        Me.RCP.Size = New System.Drawing.Size(29, 13)
        Me.RCP.TabIndex = 9
        Me.RCP.Text = "RCP"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(185, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Reason"
        '
        'lbkApprove
        '
        Me.lbkApprove.AutoSize = True
        Me.lbkApprove.Location = New System.Drawing.Point(80, 537)
        Me.lbkApprove.Name = "lbkApprove"
        Me.lbkApprove.Size = New System.Drawing.Size(47, 13)
        Me.lbkApprove.TabIndex = 11
        Me.lbkApprove.TabStop = True
        Me.lbkApprove.Text = "Approve"
        '
        'lbkReject
        '
        Me.lbkReject.AutoSize = True
        Me.lbkReject.Location = New System.Drawing.Point(165, 537)
        Me.lbkReject.Name = "lbkReject"
        Me.lbkReject.Size = New System.Drawing.Size(38, 13)
        Me.lbkReject.TabIndex = 12
        Me.lbkReject.TabStop = True
        Me.lbkReject.Text = "Reject"
        '
        'frmRcpEdit4VatInvIssued
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.lbkReject)
        Me.Controls.Add(Me.lbkApprove)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.RCP)
        Me.Controls.Add(Me.txtReason)
        Me.Controls.Add(Me.lbkDelete)
        Me.Controls.Add(Me.dgrRcp)
        Me.Controls.Add(Me.lbkRequest)
        Me.Controls.Add(Me.txtRcpNo)
        Me.Name = "frmRcpEdit4VatInvIssued"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmRcpEdit4VatInvIssued"
        CType(Me.dgrRcp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbkDelete As LinkLabel
    Friend WithEvents dgrRcp As DataGridView
    Friend WithEvents lbkRequest As LinkLabel
    Friend WithEvents txtRcpNo As TextBox
    Friend WithEvents txtReason As TextBox
    Friend WithEvents RCP As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lbkApprove As LinkLabel
    Friend WithEvents lbkReject As LinkLabel
End Class
