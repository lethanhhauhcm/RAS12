Public Class frmSelectDateRange

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()

    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If DateDiff(DateInterval.Day, dtpFromDate.Value, dtpTodate.Value) < 0 Then
            MsgBox("FromDate must on/before ToDate")
            Exit Sub
        End If

        dtpFromDate.Value = CreateFromDate(dtpFromDate.Value)
        dtpTodate.Value = CreateFromDate(dtpTodate.Value)
        Me.DialogResult = DialogResult.OK
        Me.Dispose()

    End Sub

    Private Sub frmSelectDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

End Class