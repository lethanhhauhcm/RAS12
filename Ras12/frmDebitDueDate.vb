Imports System.Data.SqlClient
Public Class frmDebitDueDate
    Private FCustRecID As String
    Private Sub dgvMain_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMain.SelectionChanged
        If dgvMain.CurrentRow Is Nothing Then
            llbDelete.Enabled = False
        Else
            llbDelete.Enabled = True
        End If
    End Sub

    Private Sub llbAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbAdd.LinkClicked
        Dim mDate As DateTime
        Dim mSQL As String
        Dim i, mintVal As Integer

        For i = 0 To dgvMain.Rows.Count - 1
            If dgvMain.Rows(i).Cells("CustShortName").Value = cbostrVal1.Text Then
                MsgBox("Customer duplicate!")
                cbostrVal1.Select()
                Exit Sub
            End If
        Next

        mDate = Now
        mintVal = Integer.Parse(txtintVal.Text)

        mSQL = "insert into LIB..MISC(strVal1,intVal1,intVal,FstUpdate,FstUser,Status,CAT) " &
               "values('" & cbostrVal1.Text & "','" & FCustRecID & "'," & mintVal & ",'" & mDate & "','" & myStaff.SICode & "','OK','DebitDueDate')"
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

        mSQL = "select RecID,strVal1 CustShortName,intVal NumberOfDay,intVal1 CustomerID,FstUpdate,FstUser,LstUpdate,LstUser,Status,CAT " &
               "from LIB..MISC MI " &
               "where Status='OK' and CAT='DebitDueDate' " &
               "order by strVal1"

        pobjTvcs.LoadDataGridView(dgvMain, mSQL)

        FormatDgvMain()
    End Sub

    Private Sub LoadComboBox()
        Dim mSQL As String

        mSQL = "select CustShortName value from LIB..Customer where status='OK' and CustShortName<>'' and City='" & myStaff.City & "' and App='RAS' order by CustShortName"
        pobjTvcs.LoadCombo(cbostrVal1, mSQL)
    End Sub

    Private Sub frmDebitDueDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pobjTvcs.Connect()
        LoadComboBox()
        If cbostrVal1.Items.Count > 0 Then
            cbostrVal1.SelectedIndex = 0
        End If
        txtintVal.Text = "0"
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

    Private Sub cbostrVal1_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbostrVal1.SelectedValueChanged
        Dim mSQL As String

        mSQL = "select RecID from LIB..Customer where CustShortName='" & cbostrVal1.Text & "'"
        FCustRecID = pobjTvcs.GetScalarAsString(mSQL)
    End Sub

    Private Sub txtintVal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtintVal.KeyPress
        PressInteger(e)
    End Sub

    Private Sub txtintVal_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtintVal.Validating
        If txtintVal.Text = "" Then
            txtintVal.Text = "0"
        Else
            txtintVal.Text = Integer.Parse(txtintVal.Text).ToString
        End If
    End Sub
End Class