Public Class CWT_ServiceFee
    Private RegList As String
    Private MyCust As New objCustomer
    Private mblnFirstLoadCompleted As Boolean
    Private Sub CWT_ServiceFee_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Private Sub CWT_ServiceFee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCombo(cboCustGroup, "Select Val as value from MISC where Cat='CustGroupName' and Status='ok' order by Val", Conn)

        MyCust.GenCustList()
        Me.BackColor = pubVarBackColor
        LoadCmb_VAL(Me.CmbCust, MyCust.List_CS & "  Union " & MyCust.List_LC)
        'Me.CmbService.Text = Me.CmbService.Items(0).ToString
        ClearSeachCriteria()

        mblnFirstLoadCompleted = True
    End Sub



    Private Sub GridSF_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridSF.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        Me.LblDelete.Visible = True
    End Sub

    Private Sub LblDelete_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LblDelete.LinkClicked
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        cmd.CommandText = ChangeStatus_ByID("CWT_SF", "XX", Me.GridSF.CurrentRow.Cells("RecID").Value)
        cmd.ExecuteNonQuery()
        loadGridSF()
    End Sub


    Private Sub loadGridSF()
        If Not mblnFirstLoadCompleted Then
            Exit Sub
        End If
        Dim strQuerry As String = "Select f.Recid,f.CustID,c.CustShortName, f.SVCType, f.WzAIR, f.TRX, f.Status" _
                & ", f.ValidFrom, f.ValidThru, f.TKT, f.ALType, f.RtgType" _
                & ", f.Curr,Amount,VatPct, f.Base, f.Cabin, f.FltTime, f.SegTKT, f.AmtPersonal, f.OWRT" _
                & ", f.Countries, f.FstUser, f.FstUpdate, f.LstUser, f.LstUpdate" _
                & " from cwt_SF f left join CustomerList c on f.CustId=c.RecId " _
                & " where f.status='OK' and c.City='" & myStaff.City & "'"

        If cboCustGroup.SelectedIndex <> -1 Then
            strQuerry = strQuerry & " and f.custid in (select intVal from MISC where Cat='CustNameInGroup'" _
                        & " and Status='OK' and Val='" & cboCustGroup.Text & "') "
        End If
        If CmbCust.SelectedIndex <> -1 Then
            strQuerry = strQuerry & " and f.custid = " & Me.CmbCust.SelectedValue
        End If
        AddEqualConditionCombo(strQuerry, CmbService, "SvcType")
        AddEqualConditionCombo(strQuerry, CmbRtg, "RtgType")
        AddEqualConditionCombo(strQuerry, CmbAL, "AlType")

        If chkValidToday.Checked Then
            strQuerry = strQuerry & " and '" & CreateFromDate(Now) & "' between f.ValidFrom and ValidThru"
        End If

        'Try
        Me.GridSF.DataSource = GetDataTable(strQuerry)
        'Me.GridSF.Columns(0).Visible = False
        GridSF.Columns("CustId").Visible = False
        Me.GridSF.Columns("RecId").Width = 40
        Me.GridSF.Columns("CustId").Width = 40
        Me.GridSF.Columns("SvcType").Width = 50

        Me.GridSF.Columns("TRX").Width = 30
        Me.GridSF.Columns("Curr").Width = 40
        Me.GridSF.Columns("Status").Width = 40
        Me.GridSF.Columns("FstUser").Width = 50
        Me.GridSF.Columns("LstUser").Width = 50
        Me.GridSF.Columns("ValidFrom").Width = 65
        Me.GridSF.Columns("ValidThru").Width = 70
        Me.GridSF.Columns("TKT").Width = 30
        Me.GridSF.Columns("ALType").Width = 40
        Me.GridSF.Columns("RtgType").Width = 50
        Me.GridSF.Columns("Cabin").Width = 45
        Me.GridSF.Columns("FltTime").Width = 45
        Me.GridSF.Columns("WzAir").Width = 45
        Me.GridSF.Columns("Base").Width = 45
        Me.GridSF.Columns("SegTkt").Width = 50
        Me.GridSF.Columns("OWRT").Width = 40
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub CmbTRX_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.lbkSearch.Visible = True
    End Sub

    Private Sub CmbService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbService.SelectedIndexChanged

        If mblnFirstLoadCompleted Then

            Select Case CmbService.Text

                Case "AIR"
                    lblRtg.Text = "RTG"
                    CmbRtg.DataSource = Nothing
                    LoadCombo(CmbRtg, "Select Val1 as Value from Misc where Cat='NonAirSubSvc' and Status='OK'" _
                              & " and Val='Air' order by intVal", Conn)
                Case "VSA"
                    lblRtg.Text = "Type"
                    CmbRtg.DataSource = Nothing
                    LoadCombo(CmbRtg, "Select Val1 as Value from Misc where Cat='NonAirSubSvc' and Status='OK'" _
                              & " and Val='Visa' order by intVal", Conn)
            End Select
            CmbRtg.SelectedIndex = -1
        End If

    End Sub




    Private Sub lbkDefineRtgType_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDefineRtgType.LinkClicked
        frmDefineRtgType4Cwt.ShowDialog()
    End Sub

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmAdd As New frmAddCorpSF

        If frmAdd.ShowDialog = DialogResult.OK Then
            loadGridSF()
        End If
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        loadGridSF()
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        ClearSeachCriteria()
    End Sub
    Private Function ClearSeachCriteria() As Boolean
        cboCustGroup.SelectedIndex = -1
        CmbCust.SelectedIndex = -1
        CmbService.SelectedIndex = -1
        CmbRtg.SelectedIndex = -1
        CmbAL.SelectedIndex = -1
        Return True
    End Function

    Private Sub cboCustGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustGroup.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            If cboCustGroup.SelectedIndex = -1 Then
                LoadCmb_VAL(Me.CmbCust, MyCust.List_CS & "  Union " & MyCust.List_LC)
            Else
                LoadCmb_VAL(CmbCust, "Select Val1 as Dis,intVal as Val from MISC where Cat='CustNameInGroup'" _
                        & " and Status='OK' and Val='" & cboCustGroup.SelectedValue _
                        & "' order by Val1", Conn)
            End If
            CmbCust.SelectedIndex = -1
        End If
    End Sub

    Private Sub lbkClone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone.LinkClicked
        If GridSF.CurrentRow Is Nothing Then Exit Sub
        Dim frmAdd As New frmAddCorpSF(GridSF.CurrentRow, cboCustGroup.Text)

        If frmAdd.ShowDialog = DialogResult.OK Then
            loadGridSF()
        End If
    End Sub

    Private Sub lbkNewValidThru_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNewValidThru.LinkClicked
        If GridSF.CurrentRow Is Nothing Then Exit Sub
        Dim strQuerry As String = "update CWT_SF set LstUpdate=getdate(),LstUser='" & myStaff.SICode _
            & "',ValidThru='" & CreateToDate(dtpNewValidThru.Value) & "' where RecId=" & GridSF.CurrentRow.Cells("RecId").Value
        If ExecuteNonQuerry(strQuerry, Conn) Then
            loadGridSF()
        Else
            MsgBox("Unable to change ValidThru")
        End If
    End Sub
End Class