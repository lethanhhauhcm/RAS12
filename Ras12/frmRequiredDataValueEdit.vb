Public Class frmRequiredDataValueEdit
    Dim mstrDataCode As String
    Dim mintCustId As Integer
    Dim mstrDataType As String

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim arrInserts(0 To 0) As String

        If txtValue.Text = "" Then
            MsgBox("Invalid Value!")
            Exit Sub
        End If
        If txtDescription.Text = "" Then
            MsgBox("Invalid Description!")
            Exit Sub
        End If
        arrInserts(0) = "Insert into GO_RequiredDataValues (DataType,CustId,DataCode,Value" _
                    & ",Description) values ('" & mstrDataType _
                    & "'," & mintCustId & ",'" & mstrDataCode _
                    & "','" & txtValue.Text & "','" & txtDescription.Text & "')"
        If txtRecID.Text <> "" Then
            Array.Resize(arrInserts, 2)
            arrInserts(1) = "Update GO_RequiredDataValues set Status='XX'" _
                            & ",LstUpdate=Getdate(), LstUser='" & myStaff.SICode _
                            & "' where RecId=" & txtRecID.Text

        End If
        If Not pobjTvcs.Update(arrInserts) Then
            MsgBox("unable to add Required Data Value")
            Exit Sub
        End If
        Me.Dispose()
    End Sub

    Public Sub New(ByVal intCustId As Integer, ByVal strDataCode As String _
                    , ByVal strDataType As String, Optional ByVal objRow As DataGridViewRow = Nothing)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Not objRow Is Nothing Then
            With objRow
                txtRecID.Text = .Cells("RecId").Value
                txtValue.Text = .Cells("Value").Value
                txtDescription.Text = .Cells("Description").Value
            End With

        End If
        mintCustId = intCustId
        mstrDataCode = strDataCode
        mstrDataType = strDataType
    End Sub
    Public Property CustId() As Integer
        Get
            CustId = mintCustId
        End Get
        Set(ByVal value As Integer)
            mintCustId = value
        End Set
    End Property
    Public Property DataCode() As String
        Get
            DataCode = mstrDataCode
        End Get
        Set(ByVal value As String)
            mstrDataCode = value
        End Set
    End Property
    Public Property DataType() As String
        Get
            DataType = mstrDataType
        End Get
        Set(ByVal value As String)
            mstrDataType = value
        End Set
    End Property

    Private Sub frmRequiredDataValueEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class