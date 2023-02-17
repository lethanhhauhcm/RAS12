Public Class frmGrpDeposit
    Private mblnFirstLoadCompleted As Boolean
    Private Sub frmGrpDeposit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboStatus.SelectedIndex = 0
        LoadFromRAS()
        If Not RefreshData() Then
            MsgBox("Unable to update changes ")
        End If
        Search()
        mblnFirstLoadCompleted = True
    End Sub
    Private Function RefreshData() As Boolean
        Dim tblChangedRecords As System.Data.DataTable
        Dim lstQuerries As New List(Of String)
        Dim i As Integer
        tblChangedRecords = GetDataTable("Select G.RecId,t.PaxName,t.DOF,t.LstUpdate from GrpDeposit g" _
                                         & " left join Tkt t  on t.Tkno=g.Tkno" _
                                         & " and t.SRV='S' where g.Status='OK'" _
                                         & " and t.Status<>'XX'" _
                                         & " and (t.PaxName<>g.PaxName or t.DOF<>g.DOF)", Conn)
        For Each objRow As DataRow In tblChangedRecords.Rows
            lstQuerries.Add("Update [GrpDeposit] set PaxName='" & objRow("PaxName") _
                & "',DOF='" & CreateFromDate(objRow("DOF")) _
                & "' where RecId=" & objRow("RecId"))
        Next
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            Return True
        Else
            Return False
        End If

    End Function
    Private Function LoadFromRAS() As Boolean
        Dim lstQuerries As New List(Of String)
        Dim strExcludeRefund = " And TKNO Not in (select tkno from tkt" _
                                & " where Status<>'XX' and DocType In ('GRP','MCO')" _
                                & " and paxname LIKE '%[0-9]%' and srv='R')"
        Dim strExcludeExisted = " and TKNO NOT in (select tkno from GrpDeposit" _
                                & " where Status<>'XX')"

        lstQuerries.Add("insert GrpDeposit (tkno,doi,dof,PaxName,DocType,PIC,FstUser,LstUpdate4Tkt)" _
                    & " select tkno,doi,dof,PaxName,DocType,FstUser,'" & myStaff.SICode _
                    & "',LstUpdate from tkt where Status NOT IN ('XX','EX') and Srv='S' and DocType in ('GRP','MCO')" _
                    & " and paxname LIKE '%[0-9]%'" _
                    & " and RcpId in (Select Recid from rcp" _
                    & " where Status <>'XX'  and counter='TVS')" _
                    & strExcludeRefund & strExcludeExisted)
        lstQuerries.Add("Update GrpDeposit set Status='XX' where Status<>'XX' and Tkno not in " _
                        & " (select tkno from tkt" _
                        & " where Status<>'XX' and DocType In ('GRP','MCO')" _
                        & " and paxname LIKE '%[0-9]%' and srv='s'" _
                        & " And RcpId in (Select Recid from rcp" _
                        & " where Status<>'XX' and counter='TVS'))")
        lstQuerries.Add("Update GrpDeposit set Status='XX' where Status<>'XX' and Tkno in " _
                        & " (select tkno from tkt" _
                        & " where DocType In ('GRP','MCO')" _
                        & " And ((Status<>'XX' and Srv='R')" _
                        & " OR (Status='EX' and SRV='S')))")
        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Unable to refresh the list of Deposit document")
        End If
        Return True
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select RecID, TKNO, DOI, DOF, PaxName, DocType, Status" _
            & ", PIC, Remark from GrpDeposit where Status<>'XX'"

        If cboDocType.Text <> "" Then
            strQuerry = strQuerry & " and DocType='" & cboDocType.Text & "'"
        End If
        If cboStatus.Text <> "" Then
            strQuerry = strQuerry & " and Status='" & cboStatus.Text & "'"
        End If
        If chkPastDOF.Checked Then
            strQuerry = strQuerry & " and DOF <= Getdate()"
        End If

        AddLikeConditionText(strQuerry, txtPaxName)

        strQuerry = strQuerry & " order by DOF, DOI"

        LoadDataGridView(dgrGrpDeposit, strQuerry, Conn)
        dgrGrpDeposit.SelectionMode = DataGridViewSelectionMode.CellSelect
        Return True
    End Function

    Private Sub cboDocType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDocType.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub

    Private Sub chkPastDOF_CheckedChanged(sender As Object, e As EventArgs) Handles chkPastDOF.CheckedChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub


    Private Sub dgrGrpDeposit_SelectionChanged(sender As Object, e As EventArgs) Handles dgrGrpDeposit.SelectionChanged
        If dgrGrpDeposit.CurrentRow IsNot Nothing Then
            txtRemark.Text = dgrGrpDeposit.CurrentRow.Cells("Remark").Value
            If dgrGrpDeposit.CurrentRow.Cells("Status").Value = "OK" Then
                lbkExpire.Visible = True
            Else
                lbkExpire.Visible = False
            End If
        End If
    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub

    Private Sub lbkExpire_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkExpire.LinkClicked
        If myStaff.SupOf = "" Then
            MsgBox("You are NOT allow to use this function")
        End If
        If dgrGrpDeposit.CurrentRow Is Nothing Then
            Exit Sub
        End If
        If Not ExecuteNonQuerry("update GrpDeposit set Status='EX', lstUser='" & myStaff.SICode _
                         & "' where Status<>'EX' and RecId=" _
                         & dgrGrpDeposit.CurrentRow.Cells("RecId").Value, Conn) Then
            MsgBox("Unable to expire record")
        End If
        Search()
    End Sub

    Private Sub lbkSaveRemark_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSaveRemark.LinkClicked
        If ExecuteNonQuerry("update GrpDeposit set Remark='" & txtRemark.Text _
                         & "' where RecId=" _
                         & dgrGrpDeposit.CurrentRow.Cells("RecId").Value, Conn) Then
            Search()
        Else

            MsgBox("Unable to SaveRemark record")
        End If
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        cboDocType.SelectedIndex = -1
        cboStatus.SelectedIndex = 0
        txtPaxName.Text = ""
        chkPastDOF.Checked = False
    End Sub
End Class