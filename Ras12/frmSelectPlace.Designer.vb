<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectPlace
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
        Me.lbkSearch = New System.Windows.Forms.LinkLabel()
        Me.dgrPlace = New System.Windows.Forms.DataGridView()
        Me.txtPlaceName = New System.Windows.Forms.TextBox()
        Me.lbkSelect = New System.Windows.Forms.LinkLabel()
        CType(Me.dgrPlace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Place"
        '
        'lbkSearch
        '
        Me.lbkSearch.AutoSize = True
        Me.lbkSearch.Location = New System.Drawing.Point(313, 9)
        Me.lbkSearch.Name = "lbkSearch"
        Me.lbkSearch.Size = New System.Drawing.Size(41, 13)
        Me.lbkSearch.TabIndex = 1
        Me.lbkSearch.TabStop = True
        Me.lbkSearch.Text = "Search"
        '
        'dgrPlace
        '
        Me.dgrPlace.AllowUserToAddRows = False
        Me.dgrPlace.AllowUserToDeleteRows = False
        Me.dgrPlace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrPlace.Location = New System.Drawing.Point(3, 32)
        Me.dgrPlace.Name = "dgrPlace"
        Me.dgrPlace.ReadOnly = True
        Me.dgrPlace.Size = New System.Drawing.Size(381, 213)
        Me.dgrPlace.TabIndex = 2
        '
        'txtPlaceName
        '
        Me.txtPlaceName.Location = New System.Drawing.Point(45, 6)
        Me.txtPlaceName.Name = "txtPlaceName"
        Me.txtPlaceName.Size = New System.Drawing.Size(262, 20)
        Me.txtPlaceName.TabIndex = 3
        '
        'lbkSelect
        '
        Me.lbkSelect.AutoSize = True
        Me.lbkSelect.Location = New System.Drawing.Point(0, 248)
        Me.lbkSelect.Name = "lbkSelect"
        Me.lbkSelect.Size = New System.Drawing.Size(37, 13)
        Me.lbkSelect.TabIndex = 4
        Me.lbkSelect.TabStop = True
        Me.lbkSelect.Text = "Select"
        '
        'frmSelectPlace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 261)
        Me.Controls.Add(Me.lbkSelect)
        Me.Controls.Add(Me.txtPlaceName)
        Me.Controls.Add(Me.dgrPlace)
        Me.Controls.Add(Me.lbkSearch)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmSelectPlace"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SelectPlace"
        CType(Me.dgrPlace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents lbkSearch As LinkLabel
    Friend WithEvents dgrPlace As DataGridView
    Friend WithEvents txtPlaceName As TextBox
    Friend WithEvents lbkSelect As LinkLabel
End Class
