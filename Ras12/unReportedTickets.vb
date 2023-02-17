Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Globalization
Public Class unReportedTickets
    Dim strDKdate As String
    Dim strSQL As String
    Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand

    Private Sub DropTableUnReportedTKT()
        cmd.CommandText = "drop table zUnreportedTKT"
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub unReportedTickets_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        DropTableUnReportedTKT()
        Me.Dispose()
    End Sub

    Private Sub unReportedTickets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CmbLstNdays.Text = "16"
        cboSystem.SelectedIndex = 0
    End Sub

    Private Sub CmbLstNdays_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbLstNdays.SelectedIndexChanged
        Dim Frm As Date, Thru As Date
        Thru = Now.Date.AddDays(-1)
        Frm = Now.Date.AddDays(-1 * CInt(Me.CmbLstNdays.Text))
        strDKdate = " (DOI between '" & Format(Frm, "dd-MMM-yy") & "' and '" & Format(Thru, "dd-MMM-yy") & " 23:59')"
    End Sub

    Private Sub lbkRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRun.LinkClicked
        Select Case cboSystem.Text
            Case "1A"
                RunReport1A()
            Case "1S"
                RunReport1S()
            Case "AK"
            Case "BL"
                RunReportBL()
            Case "VJ"

            Case "TravelGuard"
                RunReportTravelGuard()
        End Select
    End Sub
    Private Sub RunReportBL()
        Dim tblBl As New System.Data.DataTable
        Dim strDateRange As String = " between '" & CreateFromDate(Now.AddDays(0 - CmbLstNdays.Text)) _
            & "' and '" & CreateToDate(Now) & "' "

        Dim lstQuerries As New List(Of String)
        ExecuteNonQuerry("update LCC_PNRs set Tkid=null where Tkid>0 and tkid not in" _
                         & "(select RecId from Tkt where Status<>'xx' and DOI" & strDateRange _
                         & ") and DOI " & strDateRange, Conn)
        tblBl = GetDataTable("Select * from LCC_PNRs where Tkid=0", Conn)
        For Each objRow As DataRow In tblBl.Rows
            lstQuerries.Add("Update LCC_PNRs set Tkid=(select top 1 RecId from Tkt" _
                            & " where Status<>'xx' and Srv='" & objRow("Srv") _
                            & "' and substring(Tkno,1,10)='ZBL " & objRow("rloc") _
                            & "' and " & FilterByPaxName(objRow("PaxName")) _
                            & " order by RecId desc)" _
                            & " where RecId=" & objRow("RecId"))
        Next
        UpdateListOfQuerries(lstQuerries, Conn)
        GridUnRptTix.DataSource = GetDataTable("Select * from LCC_PNRs where Tkid IS null and DOI " & strDateRange, Conn)

    End Sub
    Private Sub RunReportTravelGuard()
        Dim tblBl As New System.Data.DataTable
        Dim strDateRange As String = " between '" & CreateFromDate(Now.AddDays(0 - CmbLstNdays.Text)) _
            & "' and '" & CreateToDate(Now) & "' "

        Dim lstQuerries As New List(Of String)
        ExecuteNonQuerry("update InsuranceRaw set Tkid=0 where Tkid>0 and tkid not in" _
                         & "(select RecId from Tkt where DocType='INS' and Status<>'xx' and DOI" & strDateRange _
                         & ") and DOI " & strDateRange, Conn)
        tblBl = GetDataTable("Select * from InsuranceRaw where Tkid=0", Conn)

        UpdateListOfQuerries(lstQuerries, Conn)
        GridUnRptTix.DataSource = GetDataTable("Select * from InsuranceRaw where Provider='CHR' and Tkid=0 and DOI " _
                                               & strDateRange, Conn)

    End Sub
    Private Sub RunReport1A()
        Dim LstRun As Date = ScalarToDate("tvcs.dbo.MISC", "Details", "CAT ='RasChecker' and VAL ='CompletionDate' and RMK='" & MySession.City & "'")
        If LstRun < Now.Date.AddDays(-1) Then
            MsgBox("Not All Data Has Been Downloaded")
        End If
        Dim i As Integer
        Dim strSRV As String = "SRV"
        Dim arrQuerries(0 To 2) As String
        Dim strSrvFilter As String

        strSQL = String.Empty
        For i = 0 To strSRV.Length - 1
            strSrvFilter = " and Srv='" & strSRV.Chars(i) & "'"
            arrQuerries(i) = "Select * from Srp1a where " & strDKdate & strSrvFilter _
                & " and tkno NOT in " _
                & " (select tkno from Tkt where (status='OK' or statusal='OK') and " & strDKdate & strSrvFilter & ")"
        Next
        strSQL = Join(arrQuerries, " UNION ") & " order by DOI"
        Me.GridUnRptTix.DataSource = GetDataTable(strSQL)
    End Sub
    Private Sub RunReport1S()
        Dim LstRun As Date, tmpTVSI As Integer
        Dim conn_f1s As New SqlClient.SqlConnection
        conn_f1s.ConnectionString = CnStr_F1S
        Dim Cmd1s As SqlClient.SqlCommand = conn_f1s.CreateCommand
        Cmd1s.CommandTimeout = 64
        conn_f1s.Open()
        cmd.CommandText = "select Details from FT.dbo.MISC where RMK='" & MySession.City & "' and cat='M1S' and val='CompletionDate'"
        LstRun = cmd.ExecuteScalar
        If LstRun < Now.Date.AddDays(-1) Then
            MsgBox("Not All Data Has Been Downloaded")
        End If
        DropTableUnReportedTKT()

        cmd.CommandText = "select * into zUnreportedTKT from M1S_SrpTkts where location='" & myStaff.City & "' and rmk <>'REXC'"
        cmd.ExecuteNonQuery()
        cmd.CommandText = "delete from zUnreportedTKT where not " & strDKdate &
            "; delete from zUnreportedTKT where TKNO+SRV+convert(char(12), DOI) in (select TKNO+SRV+convert(char(12), DOI) from tkt where status <>'XX' or statusAL<>'XX')" &
            "; delete from zUnreportedTKT where RelatedDoc+'R'+convert(char(12), DOI) in (select TKNO+SRV+convert(char(12), DOI) from tkt where status <>'XX')" &
            "; delete from zUnreportedTKT where voided<>0 and srv='R'"
        cmd.ExecuteNonQuery()
        strSQL = "select u.DOI, OffcID, u.TKNO, u.SRV, u.RLOC, 0 as TVSI, RelatedDoc,CustId, '' as Counter,AgtCode" _
            & " from zUnreportedTKT u left join tkt_1a t on u.tkno=t.tkno"
        Me.GridUnRptTix.DataSource = GetDataTable(strSQL)
        For i As Int16 = 0 To Me.GridUnRptTix.RowCount - 1
            If Me.GridUnRptTix.Item("SRV", i).Value = "V" Then
                Cmd1s.CommandText = "Select TVSI from f1s.dbo.F1S_Inout where Cmd='WV" &
                    Replace(Me.GridUnRptTix.Item("TKNO", i).Value, " ", "") & "' and Output like '%VOIDED%'"
            Else
                Cmd1s.CommandText = "Select TVSI from f1s.dbo.F1S_PNR_TKT_Agent where SRV='" &
                    Me.GridUnRptTix.Item("SRV", i).Value & "' and DocNo='"
                If Me.GridUnRptTix.Item("SRV", i).Value = "R" Then
                    Cmd1s.CommandText = Cmd1s.CommandText & Replace(Me.GridUnRptTix.Item("RelatedDoc", i).Value, "", "") & "'"
                Else
                    Cmd1s.CommandText = Cmd1s.CommandText & Replace(Me.GridUnRptTix.Item("TKNO", i).Value, " ", "") & "'"
                End If
            End If
            tmpTVSI = Cmd1s.ExecuteScalar
            If tmpTVSI > 0 Then
                Me.GridUnRptTix.Item("TVSI", i).Value = tmpTVSI
                Cmd1s.CommandText = "select Office from f1s_TVSI where recid=" & tmpTVSI
                Me.GridUnRptTix.Item("Counter", i).Value = Cmd1s.ExecuteScalar
            End If
        Next
        conn_f1s.Close()
        conn_f1s.Dispose()
    End Sub

    Private Sub lbkImportBL_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkImportBL.LinkClicked
        Dim ofd As New OpenFileDialog()
        Dim tblBlTkt As New System.Data.DataTable
        Dim intTkId As Integer
        Dim lstQuerries As New List(Of String)

        With ofd
            .Filter = "Format files (*.csv)|*.csv"
            .InitialDirectory = My.Application.Info.DirectoryPath
            If .ShowDialog() = DialogResult.OK Then
                Dim objExcel As New Excel.Application
                Dim objWbk As Workbook
                Dim objWsh As Worksheet
                Dim i As Integer


                objExcel = CreateObject("Excel.Application")
                objWbk = objExcel.Workbooks.Open(.FileName,, True)
                objWsh = objWbk.ActiveSheet
                With objWsh
                    If .Range("A1").Value <> "AgencyCode" Then
                        MsgBox("Invalid file format")
                        Exit Sub
                    ElseIf IsDupLccPnr(.Range("B2").Value, Split(.Range("Q2").Value, ";")(0) _
                               , .Range("M2").Value) Then
                        MsgBox("Duplicated import")
                        Exit Sub
                    End If

                    For i = 2 To objExcel.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row
                        Dim arrPaxNmes As String() = Split(.Range("Q" & i).Value, "; ")
                        For Each strPaxName As String In arrPaxNmes
                            Dim strSrv As String
                            Dim decTotal2Al As Decimal
                            Dim strAgentCode As String
                            Dim dteDOF As Date
                            Dim strDOF As String
                            Dim strItin As String = String.Empty

                            If .Range("E" & i).Value.ToString.StartsWith("Funds Used") _
                                Or .Range("E" & i).Value.ToString.StartsWith("AgencyCreditPayment") Then
                                strSrv = "S"
                                decTotal2Al = (0 - .Range("K" & i).Value)
                                strAgentCode = .Range("F" & i).Value
                                dteDOF = DateTime.ParseExact(Split(Trim(.Range("P" & i).Value), " - ")(0), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                                strItin = Replace(.Range("O" & i).Value, "-", "BL")
                            ElseIf .Range("E" & i).Value.ToString.StartsWith("Funds Added") Then
                                strSrv = "R"
                                decTotal2Al = .Range("K" & i).Value
                                strAgentCode = .Range("G" & i).Value
                                dteDOF = .Range("M" & i).Value
                            ElseIf .Range("E" & i).Value.ToString.Contains("-") Then
                                'bo qua giao dich top up tai khoan
                                Continue For
                            Else
                                MsgBox("Unknown transaction For line " & i)
                                Exit Sub
                            End If
                            strDOF = CreateFromDate(dteDOF)
                            lstQuerries.Add("insert into LCC_PNRs (RLOC, System, AgentCode" _
                                            & ", SRV, DOI, DOF, PaxName, Cur, Total2AL" _
                                            & ", Itinerary, DepDates, FstUser) values ('" _
                                            & .Range("B" & i).Value & "','BL','" _
                                            & strAgentCode & "','" & strSrv & "','" _
                                            & .Range("M" & i).Value & "','" & strDOF _
                                            & "','" & strPaxName.Trim & "','" & .Range("L" & i).Value _
                                            & "'," & decTotal2Al & ",'" & strItin _
                                            & "','" & .Range("p" & i).Value _
                                            & "','" & myStaff.SICode & "')")

                        Next
                    Next
                    UpdateListOfQuerries(lstQuerries, Conn)
                End With
            End If
        End With

        'Xac dinh counter va TVSI
        lstQuerries.Clear()
        tblBlTkt = GetDataTable("select distinct p.AgentCode, isnull(VAL1,'') as counter" _
                    & ", p.SRV,isnull(l.SI,'') as SI" _
                    & " from LCC_PNRs p left join MISC m on AgentCode=m.val" _
                    & " left join LCC_PaidPNRs l on p.RLOC=l.RLOC" _
                    & " where p.TvsiUser='' and system='BL'", Conn)
        For Each objRow As DataRow In tblBlTkt.Rows
            Select Case objRow("SRV")
                Case "S"
                    lstQuerries.Add("update LCC_PNRs set Counter='" & objRow("Counter") _
                            & "',TvsiUser='" & objRow("si") _
                            & "' where TvsiUser='' and AgentCode='" & objRow("AgentCode") & "'")
                Case "R"
                    'Bo ko xu ly R vi nhieu truong hop ko du thong tin

            End Select

        Next
        UpdateListOfQuerries(lstQuerries, Conn)
    End Sub

    Private Sub lbkEmail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEmail.LinkClicked
        Dim strSubject As String = cboSystem.Text & " Unreported tickets in last " & CmbLstNdays.Text & " days"
        Dim strAddresses As String = "vi.dohoang@transviet.com;travelshop.sgn@transviet.com;tocs.sgn@transviet.com;tocsvn.sgn@transviet.com" _
                                    & ";groupticketing.sgn@transviet.com;trinh.huynhtuyet@transviet.com"

        Dim tblNew As System.Data.DataTable = GridUnRptTix.DataSource
        Dim strFilePath As String = "D:\" & Format(Now, "yyMMdd_HHmmss") & " unreported tickets " & cboSystem.Text & ".xlsx"

        Table2Excel(tblNew, strFilePath,, True)

        Dim lstAttachments As New List(Of String)
        lstAttachments.Add(strFilePath)
        CreateOutLookEmail(strSubject, strSubject, strAddresses, lstAttachments, False)
        My.Computer.FileSystem.DeleteFile(strFilePath)

    End Sub

    Private Sub lbkImportTravelGuard_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkImportTravelGuard.LinkClicked
        Dim ofd As New OpenFileDialog()

        With ofd
            .Filter = "Format files (*.xls)|*.xls"
            .InitialDirectory = My.Application.Info.DirectoryPath
            If .ShowDialog() = DialogResult.OK Then
                Dim objExcel As New Excel.Application
                Dim objWbk As Workbook
                Dim objWsh As Worksheet
                Dim i As Integer
                Dim lstQuerries As New List(Of String)
                Dim strSrv As String
                Dim strCounter As String = String.Empty
                Dim intTkId As Integer

                objExcel = CreateObject("Excel.Application")
                objWbk = objExcel.Workbooks.Open(.FileName,, True)
                objWsh = objWbk.ActiveSheet
                With objWsh
                    If .Range("A14").Value <> "COUNTRY" Then
                        MsgBox("Invalid file format")
                        Exit Sub
                    ElseIf InsExist("CHR", .Range("B11").Value, .Range("B12").Value) Then
                        MsgBox("Duplicated import")
                        Exit Sub
                    End If

                    For i = 15 To objExcel.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row
                        Select Case .Range("K" & i).Value
                            Case "Sold"
                                strSrv = "S"
                            Case "Refund"
                                strSrv = "R"
                            Case Else
                                MsgBox(objWsh.Range("K" & i).Value & " Unable to find SRV. Please report NMK")
                                Exit Sub
                        End Select

                        strCounter = ScalarToString("[ft.dbo.CWT_Users", "Counter" _
                                                    , "Status='OK' and (SI1A1='" & Mid(.Range("M" & i).Value, 2, 6) _
                                                    & "') OR (SI1A2='" & Mid(.Range("M" & i).Value, 2, 6) & "')")
                        intTkId = ScalarToInt("Tkt", "RecId", "DocType='INS' and Dependent='" _
                                              & .Range("E" & i).Value & "'")

                        lstQuerries.Add("insert into InsuranceRaw (InsuredName,Qty, SRV, DOI, StartDate" _
                                        & ", PolicyNbr, ProductName, InsPlan, CurCode, Rate, Provider, IssueAgent" _
                                        & ",Counter,TkId)" _
                                        & " values ('" & .Range("F" & i).Value & "'," & .Range("G" & i).Value _
                                        & ",'" & strSrv & "','" & CreateFromDate(.Range("D" & i).Value) _
                                        & "','" & CreateFromDate(.Range("J" & i).Value) _
                                        & "','" & .Range("E" & i).Value & "','" & .Range("H" & i).Value _
                                        & "','" & .Range("I" & i).Value & "','VND'," & .Range("L" & i).Value _
                                        & ",'CHR','" & .Range("M" & i).Value & "','" & strCounter _
                                        & "'," & intTkId & ")")
                    Next

                    If UpdateListOfQuerries(lstQuerries, Conn) Then
                        MsgBox("Import Completed")
                    Else
                        MsgBox("Unable to import Insurance for TravelGuard")
                    End If
                End With
            End If
        End With

    End Sub

    Private Sub lbkExclude_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkExclude.LinkClicked
        If ExecuteNonQuerry("Update LCC_PNRs set Tkid=-1,LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                            & "' where RecId=" & GridUnRptTix.CurrentRow.Cells("RecId").Value, Conn) Then
            GridUnRptTix.Rows.Remove(GridUnRptTix.CurrentRow)
        End If

    End Sub

    Private Sub GridUnRptTix_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridUnRptTix.CellContentClick
        With GridUnRptTix
            If .CurrentRow IsNot Nothing Then
                If cboSystem.Text = "BL" AndAlso .CurrentRow.Cells("TKID").Value = 0 Then
                    lbkExclude.Visible = True
                Else
                    lbkExclude.Visible = False
                End If
            End If
        End With
    End Sub

    Private Sub lbkTempReport1S_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkTempReport1S.LinkClicked
        Dim tblUnreportTkt As System.Data.DataTable
        Dim strQuerry As String = "select t.RecId,p.Counter,t.tkno,p.Rloc,p.CustId,t.TktImage,t.TaxImage,p.PnrContent" _
            & " from cwt_etk t left join cwt_pnr p on t.RlocId=p.Recid " _
            & " where tkid=0 order by RlocId"

        Dim lstQuerries As New List(Of String)
        Dim intTkid As Integer

        tblUnreportTkt = GetDataTable(strQuerry, Conn_Web)
        For Each objRow As DataRow In tblUnreportTkt.Rows
            intTkid = ScalarToInt("tkt", "recid", "Status<>'XX' and Srv in ('S','V') and Tkno='" _
                                  & FormatRasTkno(objRow("Tkno")) _
                                  & "' and DOI >=Dateadd(d,-30,getdate())")
            If intTkid > 0 Then
                lstQuerries.Add("update cwt_etk set Tkid=" & intTkid _
                                & " where RecId=" & objRow("RecId"))
            End If
        Next
        If lstQuerries.Count > 0 Then
            UpdateListOfQuerries(lstQuerries, Conn_Web)
        End If


        tblUnreportTkt = GetDataTable(strQuerry, Conn_Web)
        Table2Excel(tblUnreportTkt, "d:\UnreportTkts1S")
    End Sub
End Class