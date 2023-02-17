Public Class frmDefineRtgType4Cwt
    Private MyCust As New objCustomer
    Private mblnFirstLoadCompleted As Boolean
    Private Sub frmDefineRtgType4Cwt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyCust.GenCustList()
        Me.BackColor = pubVarBackColor
        LoadCmb_VAL(Me.cboCustShortName, MyCust.List_CS & "  Union " & MyCust.List_LC)
        cboCustShortName.SelectedIndex = -1
        mblnFirstLoadCompleted = True
        Search()
    End Sub
    Private Sub Search()
        If Not mblnFirstLoadCompleted Then
            Exit Sub
        End If
        Dim strQuerry As String = "Select m.RecId,intVal as CustId,c.CustShortname,m.Val as RtgType,Val1 as Countries" _
                                & ", convert( varchar,[dteVal],106) as TktFromDate" _
                                & ", convert( varchar, [dteVal1],106) as TktToDate" _
                                & " from Misc m" _
                                & " left join CustomerList c on m.intVal=c.RecId" _
                                & " where m.intVal>0 and m.Cat='RtgTypeCwt' and m.Status='OK'"

        AddEqualConditionCombo(strQuerry, cboCustShortName)
        strQuerry = strQuerry & " order by CustShortName"

        LoadDataGridView(dgrRtgType, strQuerry, Conn)

        Me.dgrRtgType.Columns("RecId").Width = 40
        Me.dgrRtgType.Columns("CustId").Width = 40



    End Sub

    Private Sub lbkClone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone.LinkClicked
        Dim frmEdit As New frmRtgTypeEdit(dgrRtgType.CurrentRow, False)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        Dim frmEdit As New frmRtgTypeEdit(dgrRtgType.CurrentRow, True)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        Dim strQuerry As String
        strQuerry = "update Misc set Status='XX',LstUpdate=getdate()" _
                    & ",LstUser='" & myStaff.SICode & "' where Recid=" & dgrRtgType.CurrentRow.Cells("RecId").Value
        If DeleteGridViewRow(dgrRtgType, strQuerry, Conn) Then
            Search()
        End If

    End Sub

    Private Sub CmbCust_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustShortName.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub
End Class