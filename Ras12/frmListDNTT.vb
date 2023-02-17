Public Class frmListDNTT
    Private Sub frmApproveDNTT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo(cboVendor, "Select distinct Vendor as Value from DNTT where Status<>'XX'" _
            & " And Counter='N-A' order by Vendor", Conn)
        LoadCombo(cboFstUser, "Select distinct FstUser as Value from DNTT where Status<>'XX'" _
            & " And Counter='N-A' order by FstUser", Conn)
        Clear()
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String
        strQuerry = "Select D.*,v.BankName,v.AccountName,v.AccountNumber from DNTT d left join Vendor v on d.VendorId=v.RecId where d.Counter='N-A' and d.FOP='BTF' "

        AddEqualConditionCombo(strQuerry, cboVendor)
        AddEqualConditionCombo(strQuerry, cboTVC)
        AddEqualConditionCombo(strQuerry, cboFstUser)
        AddLikeConditionText(strQuerry, txtRef)

        If chkPrinted.Checked Then
            strQuerry = strQuerry & " and PrintedBy<>''"
        Else
            strQuerry = strQuerry & " and PrintedBy=''"
        End If

        If dgrRequested.RowCount > 0 AndAlso chkSamePrintID.Checked Then
            strQuerry = strQuerry & " and PrintID=" & dgrRequested.CurrentRow.Cells("PrintID").Value
        End If


        strQuerry = strQuerry & " order by Vendor,FstUpdate"

        LoadDataGridView(dgrRequested, strQuerry, Conn)
        dgrRequested.Columns("Amount").DefaultCellStyle.Format = "#,##0"
        Return True
    End Function
    Public Function Clear() As Boolean
        cboVendor.SelectedIndex = -1
        cboTVC.SelectedIndex = 0
        cboFstUser.SelectedIndex = -1
        txtRef.Text = ""
        chkPrinted.Checked = False
        Return True
    End Function

    Private Sub lbkFilter_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFilter.LinkClicked
        Search()
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub
    Private Function GetStatus4DNT(intRecId As Integer) As String
        Return ScalarToString("DNTT", "Status", "RecId=" & intRecId)
    End Function
    Private Sub lbkPrint_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPrint.LinkClicked
        Dim strStatus As String = ""
        Dim lstQuerries As New List(Of String)
        Dim lstDNTTs As New List(Of DataGridViewRow)
        Dim strPrintId As String = Format(Now, "yyMMdd_HHmmss") & "_" & myStaff.SICode

        For Each objRow As DataGridViewRow In dgrRequested.Rows
            With objRow
                If .Cells("S").Value Then
                    strStatus = GetStatus4DNT(.Cells("RecId").Value)
                    If strStatus <> "OK" Then
                        MsgBox("DNTT status had been changed! Please refresh first!")
                        Exit Sub
                    Else
                        lstQuerries.Add("Update Lib.dbo.DNTT set Status='OK', PrintedDate=getdate()" _
                                    & ", PrintedBy='" & myStaff.SICode _
                                    & "', PrintId='" & strPrintId & "' where RecId=" & .Cells("RecId").Value)
                        lstDNTTs.Add(objRow)
                    End If
                End If
            End With
        Next
        If lstQuerries.Count = 0 Then
            MsgBox("You must select DNTTs to print!")
            Exit Sub
        End If
        If Not PrintListDNTT(strPrintId, lstDNTTs) Then
            MsgBox("Unable to Print DNTT!")
            Exit Sub
        End If
        If UpdateListOfQuerries(lstQuerries, Conn_Web) Then
            Search()
        Else
            MsgBox(strPrintId & ".Unable to update DNTT as Printed! Please report NMK immediately")
        End If

    End Sub
    Private Function PrintListDNTT(strPrintId As String _
                                   , lstDNTTs As List(Of DataGridViewRow)) As Boolean

        Dim objXls As New Microsoft.Office.Interop.Excel.Application
        Dim objWbk As Microsoft.Office.Interop.Excel.Workbook
        Dim objWsh As New Microsoft.Office.Interop.Excel.Worksheet
        Dim i As Integer = 4

        objWbk = objXls.Workbooks.Add(My.Application.Info.DirectoryPath & "\R12_DeNghiThanhToan_Batch.xlt")
        objWsh = objWbk.Sheets("RPT")
        objWsh.Activate()

        objWsh.Range("A1").Value = "YÊU CẦU THANH TOÁN" & "(" & strPrintId & ")"

        For Each objRow As DataGridViewRow In lstDNTTs
            With objRow
                objWsh.Range("A" & i).Value = i - 3
                objWsh.Range("B" & i).Value = .Cells("Ref").Value
                objWsh.Range("C" & i).Value = .Cells("Vendor").Value
                objWsh.Range("D" & i).Value = .Cells("Curr").Value
                objWsh.Range("E" & i).Value = .Cells("Amount").Value
                objWsh.Range("F" & i).Value = .Cells("AccountName").Value
                objWsh.Range("G" & i).Value = .Cells("AccountNumber").Value
                objWsh.Range("H" & i).Value = "'" & .Cells("BankName").Value
                objWsh.Range("I" & i).Value = .Cells("FstUser").Value
            End With
            i = i + 1
        Next
        objWsh.Columns("E").NUMBERFORMAT = "#,##0.00"
        XlsBorder(objXls, objWsh.Range("A4:I" & i - 1), 1)
        objWsh.Range("A" & i + 1 & ":B" & i + 1).MergeCells = True
        objWsh.Range("A" & i + 1).Value = myStaff.ShortName
        objWsh.Columns("A:I").EntireColumn.AutoFit

        objXls.Visible = True
        objXls.PrintCommunication = False
        With objWsh.PageSetup
            .FitToPagesWide = 1
            .FitToPagesTall = False
        End With
        objXls.PrintCommunication = True

        objWsh.PrintOutEx()
        objWbk.Close(False)
        objXls.Quit()
        Return True

    End Function
    Private Sub lbkReset_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkResetAsUnprinted.LinkClicked
        If dgrRequested.CurrentRow Is Nothing Then Exit Sub
        With dgrRequested.CurrentRow
            Dim strStatus As String = GetStatus4DNT(.Cells("RecId").Value)
            If strStatus <> "OK" Then
                MsgBox("DNTT status had been changed! Please refresh first!")
            ElseIf ExecuteNonQuerry("update DNTT set PrintedBy='',PrintedDate=null,PrintId=0 where RecId=" _
                                    & .Cells("RecId").Value, Conn) Then
                Search()
            Else
                MsgBox("Unable to Reset DNTT!")
            End If
        End With
    End Sub
End Class