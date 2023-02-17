Public Class frmBookerAdd

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim arrUpdates(0 To 1) As String

        txtBookerName.Text = Replace(txtBookerName.Text, "  ", " ").Trim
        txtEmail.Text = txtEmail.Text.Trim
        If txtBookerName.Text = "" Then
            MsgBox("You must input Booker Name!")
            Exit Sub
        ElseIf ContainSpecialChars(txtBookerName.Text) Then
            MsgBox("Can not use special character in Booker Name!")
            Exit Sub
        ElseIf txtBookerName.Text.Contains("/") Then
            MsgBox("Can not use / in Booker Name!")
            Exit Sub
        ElseIf txtBookerName.Text.Contains("-") Then
            MsgBox("Can not use - in Booker Name!")
            Exit Sub
        End If
        If txtEmail.Text <> "" AndAlso Not txtEmail.Text.Contains("@") Then
            MsgBox("Invalid Email address!")
            Exit Sub
        End If

        'Erission yeu cau phai co Location cho Booker
        If cboCustShortName.SelectedValue = 24018 AndAlso txtLocation.Text = "" Then
            MsgBox("Invalid Location!")
            Exit Sub
        End If

        arrUpdates(0) = "Insert into CWT_Bookers" _
                        & "(CustId,BookerName,Email,Location,Status,FstUser,Counter,ReportGroup,Tel,Remark)" _
                        & " values(" & cboCustShortName.SelectedValue _
                        & ",'" & txtBookerName.Text & "','" & txtEmail.Text & "','" & txtLocation.Text _
                        & "','OK','" & myStaff.SICode & "','" & myStaff.Counter _
                        & "','" & txtReportGroup.Text & "','" & txtTel.Text & "','" & txtRemark.Text & "')"

        If Not pobjTvcs.ExecuteNonQuerry(arrUpdates(0)) Then
            MsgBox("Unable to add Booker!")
            Exit Sub
        End If

        If txtRecId.Text <> "" Then
            arrUpdates(1) = "Update Cwt_Bookers" _
                                & " set Status='XX', LstUpdate=getdate()" _
                                & ",LstUser='" & myStaff.SICode _
                                & "' where Recid=" & txtRecId.Text
            pobjTvcs.ExecuteNonQuerry(arrUpdates(1))
        End If

        Me.Dispose()
    End Sub

    Public Sub New(Optional ByVal dgRow As DataGridViewRow = Nothing, Optional strCustShortName As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        pobjTvcs.LoadCustShortNameListAsCombo(cboCustShortName)
        If dgRow Is Nothing Then
            cboCustShortName.SelectedIndex = cboCustShortName.FindStringExact(strCustShortName)
        Else
            txtRecId.Text = dgRow.Cells("Recid").Value
            cboCustShortName.SelectedIndex = cboCustShortName.FindStringExact(dgRow.Cells("CustShortName").Value)
            txtBookerName.Text = dgRow.Cells("BookerName").Value
            txtEmail.Text = dgRow.Cells("Email").Value
            txtTel.Text = dgRow.Cells("Tel").Value
            txtReportGroup.Text = dgRow.Cells("ReportGroup").Value
            txtRemark.Text = dgRow.Cells("Remark").Value
            txtLocation.Text = dgRow.Cells("Location").Value
        End If

    End Sub

    
End Class