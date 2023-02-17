Public Class frmPushData2Report

    Private Sub lbkPushData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPushData.LinkClicked
        Dim intCustId As Integer

        If cboCustomer.SelectedIndex <> -1 Then
            intCustId = cboCustomer.SelectedValue
        End If

        'Lam cho Reed Mackay
        AddManAir(dtpFrom.Value, dtpTo.Value, 0, True)
        FillMiscSvc4RM(dtpFrom.Value, dtpTo.Value)

        'Lam cho phan con lai
        RefreshNonAirCwt()
        LinkGoTravelDutoanId4Hotel()
        RelinkTktAir(dtpFrom.Value, dtpTo.Value)
        RelinkTktNonAir(dtpFrom.Value, dtpTo.Value)
        LinkTktAir(dtpFrom.Value, dtpTo.Value)
        AddManAir(dtpFrom.Value, dtpTo.Value, intCustId, False)
        FillOriginalInv(dtpFrom.Value, dtpTo.Value)

        FillGDS_Record(dtpFrom.Value, dtpTo.Value, intCustId, False)

        If TcodeLinkMultiTkt(dtpFrom.Value, dtpTo.Value) Then
            Exit Sub
        End If

        If Not FillLocalNonAir(dtpFrom.Value, dtpTo.Value, intCustId) Then
            MsgBox("Unable to get Non air hotel")
            Exit Sub
        End If

        If Not FillVisa(dtpFrom.Value, dtpTo.Value, intCustId) Then
            MsgBox("Unable to get Visa transactions")
            Exit Sub
        End If
        If Not FillAhc(dtpFrom.Value, dtpTo.Value, intCustId) Then
            MsgBox("Unable to get Adhoc Call transactions")
            Exit Sub
        End If
        If Not FillIns(dtpFrom.Value, dtpTo.Value, intCustId) Then
            MsgBox("Unable to get Insurance transactions")
            Exit Sub
        End If
        If Not FillAtk(dtpFrom.Value, dtpTo.Value, intCustId) Then
            MsgBox("Unable to get ATK transactions")
            Exit Sub
        End If
        LinkTktAir(dtpFrom.Value, dtpTo.Value)
        MapCdrHier(dtpFrom.Value, dtpTo.Value)

        Me.Dispose()
    End Sub

    Private Sub frmPushData2Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pobjTvcs.LoadComboDisplay(cboCustomer, "select distinct CustId as Value, CustShortName as Display" _
                    & " from GO_CompanyInfo1 Where Status<>'XX' order by CustShortName")

        cboCustomer.SelectedIndex = -1
    End Sub

    Private Sub lbkFromEqualTo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFromEqualTo.LinkClicked
        dtpTo.Value = dtpFrom.Value
    End Sub
End Class