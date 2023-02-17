Public Class frmFocEdit
    Public Sub New(blnEdit As Boolean, Optional objFoc As DataGridViewRow = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If objFoc IsNot Nothing Then
            cboBizUnit.SelectedIndex = cboBizUnit.FindStringExact(objFoc.Cells("BizUnit").Value)
            txtAL.Text = objFoc.Cells("AL").Value
            cboCls.SelectedIndex = cboCls.FindStringExact(objFoc.Cells("Cls").Value)
            cboDiscount.SelectedIndex = cboDiscount.FindStringExact(objFoc.Cells("Discount").Value)
            cboCurrency.SelectedIndex = cboCurrency.FindStringExact(objFoc.Cells("Currency").Value)
            txtMinValue.Text = objFoc.Cells("MinValue").Value
            chkTaxIncluded.Checked = objFoc.Cells("TaxIncluded").Value
            chkAlFormRequired.Checked = objFoc.Cells("AlFormRequired").Value
            dtpExpiryDate.Value = objFoc.Cells("ExpiryDate").Value
            txtPO.Text = objFoc.Cells("PO").Value
            txtItinerary.Text = objFoc.Cells("Itinerary").Value
            txtCondition.Text = objFoc.Cells("Condition").Value
            If blnEdit Then
                txtRecId.Text = objFoc.Cells("RecId").Value
                txtFocID.Text = objFoc.Cells("FocId").Value
            End If
        End If
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerries As New List(Of String)
        Dim strFocID As String
        Dim decMinAmount As Decimal

        Decimal.TryParse(txtMinValue.Text, decMinAmount)
        If Not CheckInputFormat() Then Exit Sub

        If txtFocID.Text <> 0 Then
            strFocID = txtFocID.Text
        Else
            strFocID = "(Select Isnull(Max(FocId),0)+1 from FOC)"
        End If

        lstQuerries.Add("insert into FOC (FocID, BizUnit, AL, Discount, Cls, Itinerary, Currency" _
                        & ", MinValue, TaxIncluded, ExpiryDate, Condition, PO, AlFormRequired" _
                        & ", Status, FstUser,City) values (" & strFocID & ",'" & cboBizUnit.Text _
                        & "','" & txtAL.Text & "','" & cboDiscount.Text & "','" & cboCls.Text _
                        & "',N'" & txtItinerary.Text & "','" & cboCurrency.Text _
                        & "'," & decMinAmount & ",'" & chkAlFormRequired.Checked _
                        & "','" & CreateToDate(dtpExpiryDate.Value) & "',N'" & txtCondition.Text _
                        & "','" & txtPO.Text & "','" & chkAlFormRequired.Checked & "','--','" _
                        & myStaff.SICode & "','" & myStaff.City & "')")
        If txtRecId.Text <> 0 Then
            lstQuerries.Add(ChangeStatus_ByID("FOC", "XX", txtRecId.Text))
        End If

        If UpdateListOfQuerries(lstQuerries, Conn) Then
            Me.DialogResult = DialogResult.OK
        Else
            MsgBox("Unable to update FOC Record!")
        End If

    End Sub
    Private Function CheckInputFormat() As Boolean
        If Not CheckFormatTextBox(txtAL,, 2, 2) Then
            Return False
        ElseIf cboBizUnit.Text = "" Then
            MsgBox("Invalid BizUnit")
            Return False
        ElseIf cboCls.Text = "" Then
            MsgBox("Invalid Cls")
            Return False
        ElseIf cboDiscount.Text = "" Then
            MsgBox("Invalid Discount")
            Return False
        ElseIf cboCurrency.Text = "" Then
            MsgBox("Invalid Currency")
            Return False
        ElseIf Not CheckFormatTextBox(txtMinValue, True, 1, 12) Then
            Return False
        End If

        Return True
    End Function
End Class