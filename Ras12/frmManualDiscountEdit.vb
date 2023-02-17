Public Class frmManualDiscountEdit
    Private mblnNew As Boolean
    Public Sub New(Optional objDiscount As DataGridViewRow = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadCombo(cboAL, "select AL as Value from Airline where ManDiscount='true' and Status='ok' and City='" _
                  & myStaff.City & "' order by AL", Conn)
        If objDiscount Is Nothing Then
            mblnNew = True
        Else
            With objDiscount
                cboCounter.SelectedIndex = cboCounter.FindStringExact(.Cells("Counter").Value)
                cboAL.SelectedIndex = cboAL.FindStringExact(.Cells("AL").Value)
                cboCustType.SelectedIndex = cboCustType.FindStringExact(.Cells("CustType").Value)
                cboDocType.SelectedIndex = cboDocType.FindStringExact(.Cells("DocType").Value)
                cboPaxType.SelectedIndex = cboPaxType.FindStringExact(.Cells("PaxType").Value)
                cboCur.SelectedIndex = cboCur.FindStringExact(.Cells("Cur").Value)
                cboBase.SelectedIndex = cboBase.FindStringExact(.Cells("Base").Value)
                txtAmount.Text = .Cells("Amount").Value
                dtpValidFrom.Value = .Cells("ValidFrom").Value
                dtpValidTo.Value = .Cells("ValidTo").Value
            End With
        End If

    End Sub

    Private Sub frmManualDiscountEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If mblnNew Then
            cboCounter.SelectedIndex = 0
            cboCustType.SelectedIndex = 0
            cboPaxType.SelectedIndex = 0
            cboDocType.SelectedIndex = 0
            cboCustType.SelectedIndex = 0
            cboBase.SelectedIndex = 0
        End If

    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim strQuerry As String
        If Not CheckInputValues() Then Exit Sub

        strQuerry = " insert into ManualDiscount (Counter, AL, CustType, DocType, PaxType, Cur, Amount" _
            & ", ValidFrom, ValidTo, Base, FstUser, Status) values ('" & cboCounter.Text & "','" & cboAL.Text _
            & "','" & cboCustType.Text & "','" & cboDocType.Text & "','" & cboPaxType.Text & "','" & cboCur.Text _
            & "'," & txtAmount.Text & ",'" & CreateFromDate(dtpValidFrom.Value) & "','" & CreateToDate(dtpValidTo.Value) _
            & "','" & cboBase.Text & "','" & myStaff.SICode & "','OK')"

        If ExecuteNonQuerry(strQuerry, Conn) Then
            Me.DialogResult = DialogResult.OK
        Else
            MsgBox("Unable to update Manual Discount!")
        End If
    End Sub
    Private Function CheckInputValues() As Boolean
        If Not IsNumeric(txtAmount.Text) Then
            MsgBox("Invalid Amount")
            Return False
        End If
        If dtpValidFrom.Value.Date < Now.Date Then
            MsgBox("Invalid ValidFrom")
            Return False
        End If
        If dtpValidFrom.Value.Date > dtpValidTo.Value.Date Then
            MsgBox("Invalid ValidTo")
            Return False
        End If
        Return True
    End Function
End Class