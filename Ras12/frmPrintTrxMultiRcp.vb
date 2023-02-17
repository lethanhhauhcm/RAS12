Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Public Class frmPrintTrxMultiRcp
    Private Sub frmPrintTrxMultiRcp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCmb_MSC(cboCustomer, "TrxMultiRcp")

    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Function Search() As Boolean

        Dim strQuerry As String = "Select cast('False' as bit) as Selected, t.RcpNo,Tkno,PaxName,Itinerary" _
            & " ,Fare,Tax,t.Charge,ChargeTV,t.RecId,RcpId,PrintedCustName,PrintedCustAddrr,PrintedTaxCode" _
            & ",r.Charge as RcpFee" _
            & " from TKT t left join Rcp r on t.RcpId=r.RecId" _
            & " where t.Status<>'XX' and t.Qty<>0 and t.Doi between '" & CreateFromDate(dtpDOI.Value) _
            & "' and '" & CreateToDate(dtpDOI.Value) & "' and r.CustShortName='" & cboCustomer.Text _
            & "' order by PaxName,r.RecId"

        LoadDataGridView(dgrTkts, strQuerry, Conn)
        For Each objColumn As DataGridViewColumn In dgrTkts.Columns
            If objColumn.Name <> "Selected" Then
                objColumn.ReadOnly = True
            End If
        Next
    End Function
    Private Function CheckInputVallues() As Boolean
        Dim intTktCount As Integer
        Dim strPaxName As String = ""
        If cboCustomer.Text = "" Then
            MsgBox("You must select Customer")
            Return False
        End If
        For Each objRow As DataGridViewRow In dgrTkts.Rows
            With objRow
                If .Cells("Selected").Value Then
                    intTktCount = intTktCount + 1

                    If strPaxName = "" Then
                        strPaxName = .Cells("PaxName").Value
                    ElseIf strPaxName <> .Cells("PaxName").Value Then
                        MsgBox("Select 1 Pax Name only!")
                    End If
                End If

            End With
        Next
        If intTktCount > 3 Or intTktCount < 2 Then
            MsgBox("Select 2 or 3 tickets only!")
            Return False
        End If
        Return True
    End Function
    Private Function Print(strAction As String) As Boolean
        Dim decTotal As Decimal
        Dim decChargeTV As Decimal
        Dim decRcpFee As Decimal

        Dim intRow As Integer = 13
        Dim strFilterByRcpId As String = " in ("
        Dim strRcps As String = "No."
        Dim tblFOP As Data.DataTable
        Dim decFopAmt As Decimal
        Dim intRcpCount As Integer
        Dim intTktCount As Integer

        If Not CheckInputVallues() Then
            Return False
        End If
        Dim objExcel As New Excel.Application
        Dim objWbk As Workbook
        Dim objWsh As Worksheet

        objWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath & "\R12_TRX_CFM_MultiRCP.xlt",, True,, "aibiet")
        objWsh = objWbk.Sheets("RPT")
        objExcel.Visible = True

        For Each objRow As DataGridViewRow In dgrTkts.Rows
            With objRow
                If .Cells("Selected").Value Then
                    intRow = intRow + 1
                    objWsh.Range("A" & intRow).Value = "'" & .Cells("Tkno").Value
                    objWsh.Range("b" & intRow).Value = .Cells("Itinerary").Value
                    objWsh.Range("c" & intRow).Value = .Cells("PaxName").Value
                    objWsh.Range("d" & intRow).Value = .Cells("Fare").Value
                    objWsh.Range("e" & intRow).Value = .Cells("Tax").Value
                    objWsh.Range("f" & intRow).Value = .Cells("Charge").Value
                    objWsh.Range("h" & intRow).Value = .Cells("Fare").Value + .Cells("Tax").Value + .Cells("Charge").Value
                    decTotal = decTotal + objWsh.Range("h" & intRow).Value
                    decChargeTV = decChargeTV + .Cells("ChargeTV").Value
                    strFilterByRcpId = strFilterByRcpId & .Cells("RcpId").Value & ","
                    If InStr(strRcps, .Cells("Rcpno").Value) = 0 Then
                        strRcps = strRcps & .Cells("RcpNo").Value & vbLf
                        intRcpCount = intRcpCount + 1
                        decRcpFee = decRcpFee + .Cells("RcpFee").Value
                    End If
                    intTktCount = intTktCount + 1
                End If
            End With
        Next
        objWsh.Range("h20").Value = decRcpFee
        objWsh.Range("a26").Value = TienBangChu(Math.Round(decTotal + decRcpFee, 0)) & " đồng."

        strFilterByRcpId = Mid(strFilterByRcpId, 1, strFilterByRcpId.Length - 1) & ")"
        strRcps = Mid(strRcps, 1, strRcps.Length - 1)

        tblFOP = GetDataTable("Select FOP,Currency,sum(Amount) as Amount ,Document" _
                              & " from FOP where Status<>'XX'" _
                              & " and RcpId " & strFilterByRcpId _
                              & " group by FOP, Currency,Document order by FOP", Conn)
        intRow = 29
        With objWsh
            For Each objRow As DataRow In tblFOP.Rows
                decFopAmt = objRow("Amount")
                If objRow("FOP") = "PSP" Then
                    decFopAmt = decFopAmt - decChargeTV
                    decChargeTV = 0
                End If
                .Range("a" & intRow).Value = objRow("FOP") & "      " & objRow("Currency")
                .Range("b" & intRow).Value = decFopAmt

                intRow = intRow + 1
            Next

            .Range("G6").Value = strRcps
            .Rows("6").RowHeight = 18 * intRcpCount
            .Range("G35").Value = dtpDOI.Value.Date
            .Range("A42").Value = myStaff.ShortName

            If strAction = "V" Then
                objExcel.Visible = True
                objWsh.PrintPreview(False)
            ElseIf strAction = "P" Then
                objExcel.Visible = False
                objWsh.PrintOut()
            End If

        End With
        objWbk.Close(SaveChanges:=False)
        objExcel.Quit()
        Return True
    End Function
    Private Sub lbkPrint_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPrint.LinkClicked
        Print("P")
    End Sub

    Private Sub lbkPreview_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPreview.LinkClicked
        Print("V")
    End Sub
End Class