Public Class frmAddNewTkt
    Private mlstNewTicket As New List(Of String)
    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        mlstNewTicket.Clear()

        For Each objRow As DataGridViewRow In dgrNewTicket.Rows
            If objRow.Cells(0).Value <> "" Then
                If CheckFormatTkno(objRow.Cells(0).Value) _
                    AndAlso Mid(objRow.Cells(0).Value, 1, 3) = "205" Then
                    mlstNewTicket.Add(objRow.Cells(0).Value)
                Else
                    MsgBox("Invalid ticket number")
                    Exit Sub
                End If
            End If
        Next
        Me.DialogResult = DialogResult.OK
        Me.Dispose()
    End Sub
    Public Property NewTicket As List(Of String)
        Get
            Return mlstNewTicket
        End Get
        Set(value As List(Of String))
            mlstNewTicket = value
        End Set
    End Property
End Class