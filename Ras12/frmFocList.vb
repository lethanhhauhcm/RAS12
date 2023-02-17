Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class frmFocList
    Private mblnFirstLoadCompleted As Boolean
    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub
    Private Function Clear() As Boolean
        cboAL.SelectedIndex = -1
        cboBizUnit.SelectedIndex = -1
        cboExpire.SelectedIndex = -1
        cboStatus.SelectedIndex = -1

        Return True
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select Status, FocID, BizUnit, AL, Discount, Cls, Itinerary" _
            & ", Currency, MinValue, TaxIncluded, ExpiryDate, PO, AlFormRequired" _
            & ",RefDoc,FstUser, FstUpdate, LstUser, LstUpdate, RecID, City, Condition" _
            & " from FOC where RecId>0"

        AddEqualConditionCombo(strQuerry, cboBizUnit)
        AddEqualConditionCombo(strQuerry, cboAL)

        If cboStatus.Text = "" Then
            strQuerry = strQuerry & " and Status<>'XX'"
        End If
        AddEqualConditionCombo(strQuerry, cboStatus)
        Select Case cboExpire.Text
            Case "In 15 days"
                strQuerry = strQuerry & " and Status not in ('OK','XX','RR') and DateAdd(d,-15,ExpiryDate)<='" _
                    & CreateFromDate(Now) & "'"
            Case "Before Today"
                strQuerry = strQuerry & " and Status not in ('OK','XX','RR') and ExpiryDate>='" _
                    & CreateFromDate(Now) & "'"
        End Select
        LoadDataGridView(dgrFoc, strQuerry, Conn)
        dgrFoc.Columns("MinValue").DefaultCellStyle.Format = "#,###"

        If dgrFoc.RowCount > 0 Then
            dgrFoc.CurrentCell = dgrFoc.Rows(0).Cells(0)

        End If
        Return True
    End Function

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        Dim frmFoc As New frmFocEdit(False)
        If frmFoc.ShowDialog Then
            Search()
        End If
    End Sub

    Private Sub lbkClone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone.LinkClicked
        If dgrFoc.CurrentRow Is Nothing Then Exit Sub
        Dim frmFoc As New frmFocEdit(False, dgrFoc.CurrentRow)
        If frmFoc.ShowDialog Then
            Search()
        End If
    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEdit.LinkClicked
        If Not myStaff.HasExtraRight("EditFocVoucher") Then
            MsgBox("You do NOT have right to take this action!")
            Exit Sub
        End If
        Dim frmFoc As New frmFocEdit(True, dgrFoc.CurrentRow)
        If frmFoc.ShowDialog Then
            Search()
        End If
    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDelete.LinkClicked
        If Not myStaff.HasExtraRight("EditFocVoucher") Then
            MsgBox("You do NOT have right to take this action!")
            Exit Sub
        End If
        If DeleteGridViewRow(dgrFoc,
            ChangeStatus_ByID("FOC", "XX", dgrFoc.CurrentRow.Cells("RecId").Value), Conn) Then
            Search()
        Else
            MsgBox("Unable to delete FOC record " & dgrFoc.CurrentRow.Cells("RecId").Value)
        End If
    End Sub

    Private Sub lbkAccept_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAccept.LinkClicked
        Dim lstQuerries As New List(Of String)
        Dim strRefDoc As String

        strRefDoc = InputBox("Please input Receipt or Payment Order number")
        If strRefDoc = "" Then
            MsgBox("Invalid RefDoc!")
            Exit Sub
        End If
        If Not myStaff.HasExtraRight("AcceptFocVoucher") Then
            MsgBox("You do NOT have right to take this action!")
            Exit Sub
        End If
        lstQuerries.Add("Update FOC set Status='OK',LstUpdate=Getdate(),RefDoc='" & strRefDoc _
                        & "',LstUser='" & myStaff.SICode & "' where RecId=" & dgrFoc.CurrentRow.Cells("RecId").Value)
        'lstQuerries.Add(ChangeStatus_ByID("FOC", "OK", dgrFoc.CurrentRow.Cells("RecId").Value))
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Acceptance Completed !")
            Search()
        Else
            MsgBox("Unable to Accept Request!")
        End If
    End Sub

    Private Sub lbkUndoAccept_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkUndoAccept.LinkClicked
        Dim lstQuerries As New List(Of String)
        If Not myStaff.HasExtraRight("AcceptFocVoucher") Then
            MsgBox("You do NOT have right to take this action!")
            Exit Sub
        End If
        lstQuerries.Add(ChangeStatus_ByID("FOC", "QQ", dgrFoc.CurrentRow.Cells("RecId").Value))
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Undo completed!")
            Search()
        Else
            MsgBox("Unable to Undo!")
        End If
    End Sub

    Private Sub lbkRequest_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRequest.LinkClicked
        Dim lstQuerries As New List(Of String)
        'lstQuerries.Add(CloneFocRecord("QQ"))
        lstQuerries.Add(ChangeStatus_ByID("FOC", "QQ", dgrFoc.CurrentRow.Cells("RecId").Value))
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Request sent!")
            Search()
        Else
            MsgBox("Unable to send Request!")
        End If
    End Sub

    Private Sub lbkPrint_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPrint.LinkClicked
        Dim objExcel As New Excel.Application
        Dim objWbk As Workbook
        Dim objWsh As Worksheet

        objExcel = CreateObject("Excel.Application")
        objExcel.Visible = False
        objWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath & "\FOC Voucher.xlt",, True)
        objWsh = objWbk.ActiveSheet
        With dgrFoc.CurrentRow
            objWsh.Range("E1").Value = "FOC" & .Cells("FocId").Value
            objWsh.Range("b2").Value = ScalarToString("AirlineList", "AlName", "AlCode='" & .Cells("AL").Value & "'")
            objWsh.Range("b3").Value = .Cells("Itinerary").Value
            Select Case .Cells("Cls").Value
                Case "Y"
                    objWsh.Range("b4").Value = "Phổ thông"
                Case "Y2C"
                    objWsh.Range("b4").Value = "Nâng hạng Phổ thông lên Thương gia "
                Case "C"
                    objWsh.Range("b4").Value = "Thương gia"
            End Select
            objWsh.Range("b5").Value = .Cells("Discount").Value & "%"
            If .Cells("TaxIncluded").Value Then
                objWsh.Range("c5").Value = "Giá vé và thuế."
            Else
                objWsh.Range("c5").Value = "Giá vé. Không giảm thuế."
            End If
            objWsh.Range("b6").Value = .Cells("Condition").Value
            objWsh.Range("b7").Value = Format(.Cells("ExpiryDate").Value, "dd MMM yy")
            objWsh.Range("c9").Value = Now
            objWsh.Range("e9").Value = myStaff.ShortName
            objWsh.Range("B8").Value = "P R E V I E W. N O T  V A L I D  F O R  U S E!"
        End With
        objExcel.Visible = True
        objWsh.PrintPreview(vbNo)

        objExcel.Visible = False
        If MsgBox("Print it?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            objWsh.Range("B8").Value = ""
            objWsh.PrintOut()
            ExecuteNonQuerry(ChangeStatus_ByID("FOC", "RR", dgrFoc.CurrentRow.Cells("RecId").Value), Conn)
        End If
        objWbk.Close(False)
        objExcel.Quit()
    End Sub
    Private Function CloneFocRecord(strNewStatus As String) As String
        Dim strQuerry As String
        strQuerry = "insert FOC (FocID, BizUnit, AL, Discount, Cls, Itinerary, Currency" _
                    & ", MinValue, TaxIncluded, ExpiryDate, Condition, PO, AlFormRequired" _
                    & ", Status,City)" _
                    & " select FocID, BizUnit, AL, Discount, Cls, Itinerary, Currency" _
                    & ", MinValue, TaxIncluded, ExpiryDate, Condition, PO, AlFormRequired,'" _
                    & strNewStatus & "','" & myStaff.SICode & "'," _
                    & " City from FOC where RecId=" & dgrFoc.CurrentRow.Cells("RecId").Value

        Return strQuerry
    End Function

    Private Sub frmFocList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo(cboAL, "Select distinct AL as value from FOC order by AL", Conn)
        Clear()
        Search()
        mblnFirstLoadCompleted = True
    End Sub

    Private Sub dgrFoc_SelectionChanged(sender As Object, e As EventArgs) Handles dgrFoc.SelectionChanged
        If Not mblnFirstLoadCompleted Then Exit Sub

        Select Case dgrFoc.CurrentRow.Cells("Status").Value
            Case "--"
                lbkDelete.Visible = True
                lbkEdit.Visible = True
                lbkRequest.Visible = True
                lbkAccept.Visible = False
                lbkPrint.Visible = False
            Case "QQ"
                lbkDelete.Visible = True
                lbkEdit.Visible = True
                lbkRequest.Visible = False
                lbkAccept.Visible = True
                lbkPrint.Visible = False
            Case "OK"
                lbkAccept.Visible = False
                lbkUndoAccept.Visible = True
                lbkPrint.Visible = True
            Case "RR", "XX"
                lbkEdit.Visible = False
                lbkAccept.Visible = False
                lbkUndoAccept.Visible = False
                lbkPrint.Visible = False
        End Select
        txtCondition.Text = dgrFoc.CurrentRow.Cells("Condition").Value
    End Sub

End Class