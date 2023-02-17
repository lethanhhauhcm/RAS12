Public Class frmBookerList

    Private Sub frmBookerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Start()
    End Sub
    Public Sub Start()

        pobjTvcs.LoadCustShortNameListAsCombo(cboCustShortName)
        txtBookerName.Text = ""
        cboCustShortName.Text = ""
        Search()
    End Sub
    Public Function Search() As Boolean

        Dim dsConditions As New DataSet
        Dim strListQuerry As String

        strListQuerry = "select b.Recid,CustShortName,BookerName,Email,B.CustId,Location,Tel,Remark,ReportGroup" _
                        & " from Cwt_Bookers B left join Go_CompanyInfo1 C" _
                        & " on B.CustId=C.CustId where B.status='OK' and C.Status='ok'"


        If cboCustShortName.Text <> "" Then
            strListQuerry = strListQuerry & " and b.CustId='" & cboCustShortName.SelectedValue & "'"
        End If

        If txtBookerName.Text <> "" Then
            strListQuerry = strListQuerry & " and BookerName like'%" & txtBookerName.Text & "%'"
        End If

        AddLikeConditionText(strListQuerry, txtEmail)

        strListQuerry = strListQuerry & " order by custshortname,BookerName"

        pobjTvcs.LoadDataGridView(dgBookers, strListQuerry)
        With dgBookers
            .Columns("RecId").Width = 40
            .Columns("CustId").Width = 40
        End With
        Return True
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        pobjTvcs.DeleteGridViewRow(dgBookers, "Update Cwt_Bookers" _
                                & " set Status='XX', LstUpdate=getdate()" _
                                & ",LstUser='" & myStaff.SICode _
                                & "' where Recid=" & dgBookers.CurrentRow.Cells(0).Value)
        Search()

    End Sub

    Private Sub btnSeacrh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeacrh.Click
        Search()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        cboCustShortName.Text = ""
        txtBookerName.Text = ""
        txtEmail.Text = ""
    End Sub

    Private Sub btnAddClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim frmAdd As New frmBookerAdd(, cboCustShortName.Text)
        frmAdd.ShowDialog()
        Search()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim frmAdd As New frmBookerAdd(dgBookers.CurrentRow)
        frmAdd.ShowDialog()
        Search()
    End Sub

    Private Sub btnExpire_Click(sender As Object, e As EventArgs) Handles btnExpire.Click
        pobjTvcs.ExecuteNonQuerry("Update Cwt_Bookers" _
                                & " set Status='EX', LstUpdate=getdate()" _
                                & ",LstUser='" & myStaff.SICode _
                                & "' where Recid=" & dgBookers.CurrentRow.Cells(0).Value)
        Search()

    End Sub
End Class