Public Class frmRptDataValuesList
    Dim mstrDataType As String
    Dim mstrCustShortName As String
    Dim mstrDataCode As String
    Dim mintCustId As Integer

    Public Sub New(ByVal strDataType As String, ByVal objRow As DataGridViewRow)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mstrDataType = strDataType

        With objRow
            mstrDataCode = .Cells("DataCode").Value
            mstrCustShortName = .Cells("CustShortName").Value
            mintCustId = .Cells("CustId").Value
            Me.Text = Me.Text & "-" & mstrCustShortName _
                        & "-" & .Cells("NameByCustomer").Value
        End With
    End Sub

    Public Property DataType() As String
        Get
            DataType = mstrDataType
        End Get
        Set(ByVal value As String)
            mstrDataType = value
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
    Public Property CustShortName() As String
        Get
            CustShortName = mstrCustShortName
        End Get
        Set(ByVal value As String)
            mstrCustShortName = value
        End Set
    End Property
    Public Property CustId() As Integer
        Get
            CustId = mintCustId
        End Get
        Set(ByVal value As Integer)
            mintCustId = value
        End Set
    End Property
    Private Sub frmRequireDataValuesList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Search()
    End Sub
    Public Function Search() As Boolean
        Dim strMsQuerry As String

        strMsQuerry = "Select Recid,Description,Value" _
                        & " from RAS12.DBO.RptDataValues" _
                        & " where Status='OK' and Custid=" & mintCustId _
                        & " and DataCode='" & mstrDataCode & "' and DataType='" & mstrDataType _
                        & "' order by Value"

        pobjTvcs.LoadDataGridView(DataGridView1, strMsQuerry)
        Return True
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim frmRqData As New frmRptDataValueEdit(mintCustId, mstrDataCode, mstrDataType)
        frmRqData.ShowDialog()
        Search()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim frmRqData As New frmRptDataValueEdit(mintCustId, mstrDataCode, mstrDataType _
                                                    , DataGridView1.CurrentRow)
        frmRqData.ShowDialog()
        Search()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim strQuerry As String = "Update RAS12.DBO.RptDataValues set Status='XX' where Recid=" _
                                  & DataGridView1.CurrentRow.Cells("RecId").Value
        If Not pobjTvcs.DeleteGridViewRow(DataGridView1, strQuerry) Then
            MsgBox("Unable to delete record")
        End If
        Search()
    End Sub


End Class