Public Class frmAddCorpSF
    Private mblnFirstLoadCompleted As Boolean

    Public Sub New(Optional objRow As DataGridViewRow = Nothing, Optional strCustGroup As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadCombo(cboCustGroup, "Select Val as value from MISC where Cat='CustGroupName' and Status='ok' order by Val", Conn)
        mblnFirstLoadCompleted = True
        cboCustGroup.SelectedIndex = -1

        If objRow Is Nothing Then
            If Now.Date >= "01 NOV 21" AndAlso Now.Date <= "31 Dec 21" Then
                chkVat8.Checked = True
            Else
                ChkVAT10.Checked = True
            End If
        Else
            With objRow
                cboCustGroup.SelectedIndex = cboCustGroup.FindStringExact(strCustGroup)
                CmbCust.SelectedIndex = CmbCust.FindStringExact(.Cells("CustShortName").Value)
                CmbService.Text = .Cells("SVCType").Value
                CmbTRX.Text = .Cells("TRX").Value
                CmbTKT.Text = .Cells("TKT").Value
                Select Case .Cells("SegTKT").Value
                    Case "T"
                        cmbSegTKT.Text = "TKT"
                    Case "S"
                        cmbSegTKT.Text = "SEG"
                End Select

                CmbCurr.Text = .Cells("Curr").Value
                TxtAmount.Text = .Cells("Amount").Value
                TxtPersonal.Text = .Cells("AmtPersonal").Value
                CmbRtgType.Text = .Cells("RtgType").Value
                CmbCabin.Text = .Cells("Cabin").Value
                CmbAlType.Text = .Cells("ALType").Value
                CmbBase.Text = .Cells("Base").Value

                TxtFltTime.Text = .Cells("FltTime").Value
                CmbOWRT.Text = .Cells("OWRT").Value
                dtpValidFrom.Value = .Cells("ValidFrom").Value
                dtpValidThru.Value = .Cells("ValidThru").Value
                chkWzAIR.Checked = .Cells("WzAIR").Value

                If .Cells("VatPct").Value = 8 Then
                    chkVat8.Checked = True
                ElseIf .Cells("VatPct").Value = 10 Then
                    ChkVAT10.Checked = True
                End If
            End With

        End If
    End Sub

    Private Function CreateQuerry(intCustId As Integer) As String
        Dim aa As Decimal = 0
        Dim intOldRecId As Integer = 0
        Dim strSegTkt As String = ""
        Dim strQuerry As String = ""
        'Dim HS As Decimal = 1
        Dim intVatPct As Int32

        Dim DKValid As String = " and ((ValidFrom between '" & Format(Me.dtpValidFrom.Value, "dd-MMM-yy") & "' and '" & Format(Me.dtpValidThru.Value, "dd-MMM-yy") & " 23:59') "
        DKValid = DKValid & " or ((ValidThru between '" & Format(Me.dtpValidFrom.Value, "dd-MMM-yy") & "' and '" & Format(Me.dtpValidThru.Value, "dd-MMM-yy") & " 23:59')))"
        If CmbService.Text = "VSA" Then
            strSegTkt = ""
        Else
            strSegTkt = Me.cmbSegTKT.Text.Substring(0, 1)
        End If

        Select Case CmbService.Text
            Case "AIR"
                If Me.CmbAlType.Text = "" Or Me.CmbRtgType.Text = "" Or Me.cmbSegTKT.Text = "" Or Me.CmbCabin.Text = "" Then
                    MsgBox("Invalid input for AIR")
                    Return strQuerry
                End If
                intOldRecId = ScalarToInt("CWT_SF", "RecID", "Custid=" & intCustId & " and status='OK' and SVCType='" & Me.CmbService.Text &
                    "' and trx='" & Me.CmbTRX.Text & "' and RtgType='" & Me.CmbRtgType.Text & "' and ALType='" & Me.CmbAlType.Text &
                    "' and tkt ='" & Me.CmbTKT.Text & "' and SegTKT='" & Me.cmbSegTKT.Text.Substring(0, 1) & "' and OWRT='" & Me.CmbOWRT.Text &
                    "' and cabin='" & Me.CmbCabin.Text &
                    "' and FltTime='" & Me.TxtFltTime.Text & "' and WzAIR=" & IIf(Me.chkWzAIR.Checked, 1, 0) & DKValid) > 0
            Case "VSA"
                If CmbRtgType.Text = "" Then
                    MsgBox("Invalid input for VISA")
                    Return strQuerry
                End If

                intOldRecId = ScalarToInt("CWT_SF", "RecID", "Custid=" & intCustId & " and status='OK' and SVCType='" & Me.CmbService.Text &
                    "' and trx='" & Me.CmbTRX.Text & "' and RtgType='" & Me.CmbRtgType.Text & "' and ALType='" & Me.CmbAlType.Text &
                    "' and tkt ='' and SegTKT='' and OWRT='' and cabin='' and FltTime=0" & DKValid) > 0
            Case Else
                intOldRecId = ScalarToInt("CWT_SF", "RecID", "Custid=" & intCustId & " and status='OK' and SVCType='" & Me.CmbService.Text &
                    "' and trx='" & Me.CmbTRX.Text & "' and RtgType='" & Me.CmbRtgType.Text & "' and ALType='" & Me.CmbAlType.Text &
                    "' and tkt ='" & Me.CmbTKT.Text & "' and SegTKT='" & Me.cmbSegTKT.Text.Substring(0, 1) & "' and OWRT='" & Me.CmbOWRT.Text &
                    "' and cabin='" & Me.CmbCabin.Text &
                    "' and FltTime='" & Me.TxtFltTime.Text & "' and WzAIR=" & IIf(Me.chkWzAIR.Checked, 1, 0) & DKValid) > 0
        End Select

        If intOldRecId > 0 Then
            MsgBox("Duplicate Record found:" & intOldRecId & ". Plz check your input", MsgBoxStyle.Critical, msgTitle)
            Return strQuerry
        End If

        'If Not Me.ChkVAT10.Checked Then HS = 1.1

        If chkVat8.Checked Then
            intVatPct = 8
        ElseIf ChkVAT10.Checked Then
            intVatPct = 10
        End If

        aa = CDec(Me.TxtAmount.Text)
        aa = CDec(Me.TxtPersonal.Text)
        aa = CDec(Me.TxtFltTime.Text)
        If (Me.CmbCurr.Text = "VND" And CDec(Me.TxtAmount.Text) <> 0 And CDec(Me.TxtAmount.Text) < 16000) Or
            (Me.CmbCurr.Text = "USD" And CDec(Me.TxtAmount.Text) > 256) Then
            MsgBox("Illogic Currency/Amount. Plz check your input", MsgBoxStyle.Critical, msgTitle)
            Return strQuerry
        End If

        strQuerry = "insert Cwt_SF (CustID, TRX, SVCType, Curr, RtgType, ALType, Amount, wzAIR, Base" _
            & ", ValidFrom, ValidThru, FstUser, TKT, Cabin, FltTime" _
            & ", SegTKT, AmtPersonal, OWRT, VatPct) values (" _
            & intCustId & ",'" & CmbTRX.Text & "','" & CmbService.Text _
            & "','" & CmbCurr.Text & "','" & CmbRtgType.Text & "','" & CmbAlType.Text _
            & "'," & TxtAmount.Text & ",'" & chkWzAIR.Checked & "','" & CmbBase.Text _
            & "','" & CreateFromDate(dtpValidFrom.Value) & "','" & CreateToDate(dtpValidThru.Value) _
            & "','" & myStaff.SICode & "','" & CmbTKT.Text & "','" & CmbCabin.Text _
            & "','" & TxtFltTime.Text & "','" & strSegTkt _
            & "'," & TxtPersonal.Text & ",'" & CmbOWRT.Text & "'," & intVatPct & ")"

        Return strQuerry
    End Function
    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerries As New List(Of String)
        Dim strQuerry As String
        Dim DKValid As String = " and ((ValidFrom between '" & Format(Me.dtpValidFrom.Value, "dd-MMM-yy") & "' and '" & Format(Me.dtpValidThru.Value, "dd-MMM-yy") & " 23:59') "
        DKValid = DKValid & " or ((ValidThru between '" & Format(Me.dtpValidFrom.Value, "dd-MMM-yy") & "' and '" & Format(Me.dtpValidThru.Value, "dd-MMM-yy") & " 23:59')))"
        Dim HS As Decimal = 1

        If CmbService.Text = "AIR" Then
            If CmbTRX.Text = "" Then
                MsgBox("Invalid TRX value")
                Exit Sub
            ElseIf CmbTKT.Text = "" Then
                MsgBox("Invalid TKT value")
                Exit Sub
            End If
            If CmbOWRT.Text = "" Then
                MsgBox("Invalid OWRT value")
                Exit Sub
            End If
        End If

        If ChkVAT10.Checked AndAlso chkVat8.Checked Then
            MsgBox("VAT must be 10% or 8%")
            Exit Sub
        ElseIf Not ChkVAT10.Checked AndAlso Not chkVat8.Checked Then
            MsgBox("VAT must be 10% or 8%")
            Exit Sub
        End If

        If cboCustGroup.SelectedIndex = -1 AndAlso CmbCust.SelectedIndex = -1 Then
            MsgBox("You must select CustGroup and/or Customer")
            Exit Sub
        ElseIf cboCustGroup.SelectedIndex <> -1 AndAlso CmbCust.SelectedIndex = -1 Then
            For Each objR As DataRowView In CmbCust.Items
                strQuerry = CreateQuerry(objR.Row.ItemArray(1).ToString)
                If strQuerry = "" Then
                    Exit Sub
                Else
                    lstQuerries.Add(strQuerry)
                End If

            Next
        ElseIf CmbCust.SelectedIndex <> -1 Then
            strQuerry = CreateQuerry(CmbCust.SelectedValue)
            If strQuerry = "" Then
                Exit Sub
            Else
                lstQuerries.Add(strQuerry)
            End If

        End If


        If lstQuerries.Count > 0 AndAlso UpdateListOfQuerries(lstQuerries, Conn) Then
            Me.DialogResult = DialogResult.OK
        Else
            MsgBox("Unable to Add Service Fee")
        End If

    End Sub

    Private Sub frmAddCorpSF_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        CmbCurr.SelectedIndex = 1
        CmbService.SelectedIndex = 0
        mblnFirstLoadCompleted = True

    End Sub
    Private Sub cboCustGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustGroup.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            If cboCustGroup.SelectedIndex = -1 Then
                LoadCmb_VAL(Me.CmbCust, "Select c.CustShortName as Dis, c.Recid as VAL" _
                        & " from CustomerList c left join Cust_Detail d on c.RecId=d.CustId" _
                        & " where c.Status='OK' and d.Status='OK' and d.CAT='Channel'" _
                        & " and d.VAL in ('CS','LC') order by c.CustShortName", Conn)
            Else
                LoadCmb_VAL(CmbCust, "Select Val1 as Dis,intVal as Val from MISC where Cat='CustNameInGroup'" _
                        & " and Status='OK' and Val='" & cboCustGroup.SelectedValue _
                        & "' order by Val1", Conn)
            End If
            CmbCust.SelectedIndex = -1
        End If
    End Sub
    Private Sub CmbService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbService.SelectedIndexChanged

        Me.chkWzAIR.Checked = False
        If Me.CmbService.Text = "AIR" Then
            Me.chkWzAIR.Visible = False
        Else
            Me.chkWzAIR.Visible = True
        End If
        If mblnFirstLoadCompleted Then
            Select Case CmbService.Text
                Case "AIR"
                    lblRtg.Text = "RTG"
                    CmbRtgType.DataSource = Nothing
                    LoadCombo(CmbRtgType, "Select Val1 as Value from Misc where Cat='NonAirSubSvc' and Status='OK'" _
                              & " and Val='Air' order by intVal", Conn)
                Case "VSA"
                    lblRtg.Text = "Type"
                    CmbRtgType.DataSource = Nothing
                    LoadCombo(CmbRtgType, "Select Val1 as Value from Misc where Cat='NonAirSubSvc' and Status='OK'" _
                              & " and Val='Visa' order by intVal", Conn)
            End Select
        End If

    End Sub

    Private Sub lbkSetPersonnal_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSetPersonnal.LinkClicked
        If IsNumeric(TxtAmount.Text) Then
            TxtPersonal.Text = TxtAmount.Text / 2
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If IsNumeric(txtYear.Text) Then
            dtpValidThru.Value = dtpValidFrom.Value.AddYears(1).AddDays(-1)
        End If
    End Sub
    Private Sub CmbCurr_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Me.CmbCurr.Text = "PCT" Then
            Me.CmbBase.Visible = True
        Else
            Me.CmbBase.Visible = False
        End If
        Me.Label12.Visible = Me.CmbBase.Visible
    End Sub

    Private Sub ChkVAT10_CheckedChanged(sender As Object, e As EventArgs) Handles ChkVAT10.CheckedChanged
        chkVat8.Checked = Not ChkVAT10.Checked
    End Sub

    Private Sub chkVat8_CheckedChanged(sender As Object, e As EventArgs) Handles chkVat8.CheckedChanged
        ChkVAT10.Checked = Not chkVat8.Checked
    End Sub
End Class