Imports System.Data.SqlClient
Public Class frmBspStock
    Private Sub txtVal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVal.KeyPress
        If (Not Char.IsControl(e.KeyChar) _
            AndAlso (Not Char.IsDigit(e.KeyChar))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub dgvMain_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMain.SelectionChanged
        If dgvMain.CurrentRow Is Nothing Then
            llbDelete.Enabled = False
        Else
            llbDelete.Enabled = True
        End If
    End Sub

    Private Sub txtVal_TextChanged(sender As Object, e As EventArgs) Handles txtVal.TextChanged
        If txtVal.Text = "" Then
            llbAdd.Enabled = False
        Else
            llbAdd.Enabled = True
        End If
    End Sub

    Private Sub llbAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbAdd.LinkClicked
        Dim mDate As DateTime
        Dim mSQL As String
        Dim i As Integer

        If txtVal.Text.Length <> 4 Then
            MsgBox("Only apply 4 numeric character!")
            txtVal.Select()
            Exit Sub
        End If

        For i = 0 To dgvMain.Rows.Count - 1
            If dgvMain.Rows(i).Cells("VAL").Value = txtVal.Text Then
                MsgBox("VAL duplicate!")
                txtVal.Select()
                Exit Sub
            End If
        Next

        mDate = Now

        mSQL = "insert into LIB..MISC(VAL,FstUpdate,FstUser,Status,CAT) " &
               "values('" & txtVal.Text & "','" & mDate & "','" & myStaff.SICode & "','OK','BSPSTOCK')"
        pobjTvcs.ExecuteNonQuerry(mSQL)
        LoadMain()
    End Sub

    Private Sub FormatDgvMain()
        dgvMain.Columns("FstUpdate").DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss"
        dgvMain.Columns("LstUpdate").DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss"
        dgvMain.Columns("CAT").Visible = False
        dgvMain.Columns("LstUpdate").Visible = False
        dgvMain.Columns("LstUser").Visible = False
    End Sub

    Private Sub LoadMain()
        Dim mSQL As String

        mSQL = "select RecID,VAL,FstUpdate,FstUser,LstUpdate,LstUser,Status,CAT " &
               "from LIB..MISC MI " &
               "where Status='OK' and CAT='BSPSTOCK' " &
               "order by VAL"

        pobjTvcs.LoadDataGridView(dgvMain, mSQL)

        FormatDgvMain()
    End Sub

    Private Sub frmBspStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pobjTvcs.Connect()
        LoadMain()
    End Sub

    Private Sub llbDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDelete.LinkClicked
        Dim mSQL As String

        If MsgBox("Delete this row?", vbYesNo) = vbNo Then
            Exit Sub
        End If

        mSQL = "update LIB..MISC " &
               "set Status='XX',LstUpdate='" & Now & "',LstUser='" & myStaff.SICode & "' " &
               "where RecID=" & dgvMain.CurrentRow.Cells("RecID").Value & ""
        pobjTvcs.ExecuteNonQuerry(mSQL)
        LoadMain()
    End Sub
End Class