Public Class frmCustomerList
    Public Sub New(strCustChannel As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmCustomerList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Function Search() As Boolean
        Dim StrSQL As String = "select * from CustomerList" _
            & " where status<>'XX' and City='" & myStaff.City _
            & "' and RecId in (Select CustId from Cust_Detail where Status='OK' and Cat='Channel' and Val='AP') "

        AddLikeConditionText(StrSQL, txtCustShortName)
        AddLikeConditionText(StrSQL, txtCustFullName)


        StrSQL = StrSQL & " order by CustShortName"
        dgrCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        LoadDataGridView(dgrCustomer, StrSQL, Conn)

    End Function

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmEdit As New frmCustomerEdit
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        If dgrCustomer.CurrentRow Is Nothing Then Exit Sub
        Dim frmEdit As New frmCustomerEdit(dgrCustomer.CurrentRow)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkExpire_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkExpire.LinkClicked

    End Sub

    Private Sub lbkReActivate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkReActivate.LinkClicked

    End Sub
End Class