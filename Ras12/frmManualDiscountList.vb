Public Class frmManualDiscountList
    Private Sub frmManualDiscountList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select * from ManualDiscount where Status='OK' order by Counter, AL,DocType,PaxType"
        LoadDataGridView(dgrManualDiscount, strQuerry, Conn)
        Return True
    End Function
    Private Sub lbkExpire_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkChangeExpiry.LinkClicked
        Dim strQuerry As String
        Dim frmSelectDate As New frmSelectDate(dgrManualDiscount.CurrentRow.Cells("ValidTo").Value, Now.Date)

        If frmSelectDate.ShowDialog = DialogResult.OK Then
            strQuerry = "Update ManualDiscount set LstUpdate=getdate(),LstUser='" & myStaff.SICode & "',ValidTo='" & CreateToDate(frmSelectDate.dtpNewDate.Value) _
                & "' where Recid=" & dgrManualDiscount.CurrentRow.Cells("RecId").Value
            If ExecuteNonQuerry(strQuerry, Conn) Then
                Search()
            End If
        End If
    End Sub

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmDiscount As New frmManualDiscountEdit
        If frmDiscount.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If dgrManualDiscount.CurrentRow.Cells("ValidFrom").Value <= Now.Date Then
            MsgBox("You can not delete Record in use")
            Exit Sub
        End If
        If ExecuteNonQuerry(ChangeStatus_ByID("ManualDiscount", "XX", dgrManualDiscount.CurrentRow.Cells("RecId").Value), Conn) Then
            Search()
        End If

    End Sub

    Private Sub lbkClone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone.LinkClicked
        Dim frmDiscount As New frmManualDiscountEdit(dgrManualDiscount.CurrentRow)
        If frmDiscount.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub
End Class