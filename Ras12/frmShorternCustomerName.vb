Public Class frmShorternCustomerName
    Private mblnFirstLoadCompleted As Boolean
    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd.LinkClicked
        If Not CheckInputValues() Then Exit Sub
        Dim strQuerry As String = "insert into lib.dbo.Misc (CAT, strVal1, strVal2, strVal3, strVal4, strVal5, intVal1, City)" _
            & " values ('ShorternCustName','" & cboCustShortName.Text & "','" & txtOriginalText.Text & "','" & txtReplacement4Bill.Text _
            & "','" & txtReplacement4Invoice.Text & "','" & myStaff.SICode & "'," & cboCustShortName.SelectedValue _
            & ",'" & myStaff.City & "')"
        If ExecuteNonQuerry(strQuerry, Conn_Web) Then
            Search()
            LoadCustomer()
        Else
            MsgBox("Unable to update into Database!")
        End If
    End Sub
    Private Function CheckInputValues() As Boolean
        If Not cboCustShortName.Text.Contains(txtOriginalText.Text) Then
            MsgBox("CustShortName must contain OriginalText")
            Return False
        End If
        If Not CheckFormatTextBox(txtOriginalText,, 1) Then
            Return False
        End If
        'If Not txtOriginalText.Text.Contains(txtReplacement4Bill.Text) Then
        '    MsgBox("OriginalText must contain Replacement4Bill")
        '    Return False
        'End If
        'If Not txtOriginalText.Text.Contains(txtReplacement4Invoice.Text) Then
        '    MsgBox("OriginalText must contain Replacement4Invoice")
        '    Return False
        'End If
        Return True
    End Function

    Private Sub frmShorternCustomerName_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
        LoadCustomer()
        mblnFirstLoadCompleted = True
    End Sub
    Private Function LoadCustomer() As Boolean
        Dim strListCust As String = "Select RecId as value, CustShortName as Display from lib.dbo.customer where status='OK' and APP like '%RAS%'" _
            & " and AOPListID<>''" _
            & " and City='" & myStaff.City & "'" _
            & " and RecId not in (select intVal1 from lib.dbo.Misc where Cat='ShorternCustName')" _
            & " order by CustShortName"
        LoadComboDisplay(cboCustShortName, strListCust, Conn_Web)
        Return True
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String = "select strVal1 as CustShortName, strVal2 as OriginalText, strVal3 as Replacement4Bill, strVal4 as Replacement4Invoice" _
            & ", strVal5 as FstUser, LstRun as FstUpdate , intVal1 as CustId" _
            & " from lib.dbo.Misc where Cat='ShorternCustName'" _
            & " and City='" & myStaff.City & "'" _
            & " order by strVal1"
        LoadDataGridView(dgrCustomers, strQuerry, Conn_Web)
        Return True
    End Function

    Private Sub cboCustShortName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustShortName.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            txtOriginalText.Text = cboCustShortName.Text
            txtReplacement4Bill.Text = ""
            txtReplacement4Invoice.Text = ""
        End If
    End Sub
End Class