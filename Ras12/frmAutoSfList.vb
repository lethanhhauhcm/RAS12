Public Class frmAutoSfList
    Private mblnFistLoadCompleted As Boolean
    Private Sub frmAutoSfList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strCustShortName As String = "Select intVal as Value,c.CustShortName as Display from misc m" _
            & " left join Customerlist c on m.intVal=c.Recid" _
            & " where Cat='AutoSf' and c.Status='OK' and c.Status='OK' and c.City='" & myStaff.City _
            & "' and m.City='" & myStaff.City _
            & "' order by c.CustShortName"
        LoadComboDisplay(cboCustShortName, strCustShortName, Conn)
        cboCustShortName.SelectedIndex = -1
        mblnFistLoadCompleted = True
        cboStatus.SelectedIndex = 0
        cboServiceType.SelectedIndex = -1
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String

        strQuerry = "Select m.RecId, c.CustShortName as CustShortName,m.intDec as PCT" _
                    & ",convert(varchar(9), dteVal,06) as ValidFrom,convert(varchar(9), dteVal1,06) as ValidTo" _
                    & ",m.Details as ServiceType,m.Status,m.FstUpdate" _
                    & " ,m.LstUpdate,m.FstUser,m.LstUser" _
                    & " from  Misc m" _
                    & " LEFT JOIN  Customerlist c on c.Recid=m.intVal" _
                    & " where m.Cat='AutoSf' and c.Status='OK' and c.City='" & myStaff.City & "'"

        If cboStatus.SelectedIndex > -1 Then
            strQuerry = strQuerry & " and m.Status='" & cboStatus.Text & "'"
        End If

        If cboServiceType.SelectedIndex > -1 Then
            strQuerry = strQuerry & " and m.Details='" & cboServiceType.Text & "'"
        End If

        If cboCustShortName.SelectedIndex > -1 AndAlso cboCustShortName.Items.Count > 0 Then
            strQuerry = strQuerry & " and m.intVal='" & cboCustShortName.SelectedValue & "'"
        End If

        strQuerry = strQuerry & " order by c.CustShortName"
        LoadDataGridView(dgrAutoSf, strQuerry, Conn)
        Return True
    End Function

    Private Sub cboCustShortName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustShortName.SelectedIndexChanged, cboStatus.SelectedIndexChanged
        If mblnFistLoadCompleted Then
            Search()
        End If
    End Sub

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmNew As New frmAutoSfEdit
        If frmNew.ShowDialog = Windows.Forms.DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        Dim frmNew As New frmAutoSfEdit(dgrAutoSf.CurrentRow)
        If frmNew.ShowDialog = Windows.Forms.DialogResult.OK Then
            Search()
        End If
    End Sub

End Class