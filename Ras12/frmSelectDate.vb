'20220505 modi by 7643
Public Class frmSelectDate
    Private mdteMinDate As Date
    Private mdteMaxDate As Date  '20220505 add by 7643
    'Public Sub New(dteSelectedDate As Date, Optional dteMinDate As Date = Nothing)  '20220505 mark by 7643
    Public Sub New(dteSelectedDate As Date, Optional dteMinDate As Date = Nothing, Optional dteMaxDate As Date = Nothing, Optional xLabel As String = Nothing, Optional xForm As String = Nothing)  '20220505 modi by 7643

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mdteMinDate = dteMinDate
        dtpNewDate.Value = dteSelectedDate

        '20220505 add by 7643 -B-
        mdteMaxDate = dteMaxDate
        If Not dteMinDate = Nothing Then
            dtpNewDate.MinDate = dteMinDate
        End If

        If Not dteMaxDate = Nothing Then
            dtpNewDate.MaxDate = dteMaxDate
        End If

        If Not xLabel = Nothing Then
            Label1.Text = xLabel
        End If

        If Not xForm = Nothing Then
            Text = xForm
        End If
        '20220505 add by 7643 -E-
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If dtpNewDate IsNot Nothing AndAlso dtpNewDate.Value.Date < mdteMinDate Then  '20220505 mark by 7643
            MsgBox("Invalid Selected Date!")
            Exit Sub
        End If
        Me.DialogResult = DialogResult.OK
    End Sub
End Class