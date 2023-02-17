Imports Microsoft.Office.Interop
Public Class frmRunUTTR
    Private mblnFirstLoadCompleted As Boolean
    Private Sub frmRunUTTR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboCounter.Text = myStaff.Counter
        cboFilterByTktStatus.Text = "OK"
        Search()
        mblnFirstLoadCompleted = True
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String
        strQuerry = "SELECT u.*,CustShortName" _
            & " FROM UTTR u left join CustomerList c on u.CustId=c.RecId" _
            & " WHERE (u.Status ='OK') AND (c.Status = 'ok') " _
            & " and Counter='" & cboCounter.Text _
            & "' and TktStatus='" & cboFilterByTktStatus.Text & "'"

        LoadDataGridView(dgrUTTR, strQuerry, Conn)
        With dgrUTTR
            .Columns("CurFare").Visible = False
            .Columns("CurFare").Visible = False
        End With
        Return True

    End Function

    Private Sub lbkImport1A_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkImport1A.LinkClicked
        Dim ofdUttr As New OpenFileDialog
        Dim strFilePath As String
        Dim strRcp As String = ""
        Dim colLccSvcs As New Microsoft.VisualBasic.Collection

        With ofdUttr
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            .Filter = "Excel files|*.xls"
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                strFilePath = .FileName
                Dim i As Integer
                Dim lstQuerry As New List(Of String)
                Dim objExcel As New Excel.Application
                Dim objWsh As New Excel.Worksheet
                Dim blnPartialUsed As Boolean
                Dim strTkno As String = String.Empty
                objExcel.Visible = True
                objExcel.Workbooks.Open(strFilePath, , True)
                objWsh = objExcel.ActiveSheet
                With objWsh
                    If .Range("A1").Value <> "Office ID" Then
                        MsgBox("Invalid File Format")
                        Exit Sub
                    End If
                    If ScalarToInt("UTTR", "RecId", "Status='--' and TktOid='" _
                                   & .Range("A2").Value & "'") > 0 Then
                        MsgBox("Duplicate import")
                        Exit Sub
                    End If
                    For i = 2 To 1000


                        If .Range("A" & i).Value Is Nothing Then
                            Exit For
                        ElseIf CDate(.Range("F" & i).Value) > "31 Dec 2013 23:59" Then
                            strTkno = .Range("d" & i).Value & " " & Mid(.Range("E" & i).Value, 1, 4) & " " & Mid(.Range("E" & i).Value, 5)
                            blnPartialUsed = IIf(.Range("p" & i).Value = "Yes", True, False)
                            lstQuerry.Add("insert into [UTTR] (GDS, TktOID, PaxName" _
                                        & ",TKNO, DOI, LastDOF, RLOC, QuerryDate" _
                                        & ", CurTotal, TktValue, CurFare, Fare" _
                                        & ", PartiallyUnused, OpenSeg,FirstUser) values ('1A','" _
                                        & .Range("A" & i).Value & "','" & .Range("C" & i).Value _
                                        & "','" & strTkno & "','" & CreateFromDate(.Range("F" & i).Value) _
                                        & "','" & CreateFromDate(.Range("G" & i).Value) _
                                        & "','" & .Range("h" & i).Value & "','" & CreateFromDate(.Range("i" & i).Value) _
                                        & "','" & .Range("L" & i).Value & "'," & .Range("M" & i).Value _
                                        & ",'" & .Range("N" & i).Value & "'," & .Range("O" & i).Value _
                                        & ",'" & blnPartialUsed & "','" & .Range("Q" & i).Value _
                                        & "','" & myStaff.SICode & "')")
                        End If
                    Next
                    'If lstQuerry.Count > 0 Then
                    '    lstQuerry.Add("update UTTR set Status='EX' where Status='OK'")
                    'End If
                    If Not UpdateListOfQuerries(lstQuerry, Conn) Then
                        MsgBox("Unable to Update SQL")
                        Exit Sub
                    Else
                        MsgBox("Import Completed!")
                    End If
                    objExcel.Quit()
                End With

            End If
        End With
    End Sub



    Private Sub cboCounter_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboCounter.SelectedIndexChanged, cboFilterByTktStatus.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub

    Private Sub lbkChangeTktStatus_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkChangeTktStatus.LinkClicked
        If dgrUTTR.CurrentRow Is Nothing Or cboTktStatus.Text = "" Then
            Exit Sub
        End If
        ExecuteNonQuerry("Update UTTR set TktStatus='" & Mid(cboTktStatus.Text, 1, 2) _
                         & "' where RecId=" & dgrUTTR.CurrentRow.Cells("RecId").Value, Conn)
        Search()
    End Sub

    Private Sub lbkRefresh_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRefresh.LinkClicked
        Dim tblOffcId As DataTable
        tblOffcId = GetDataTable("Select distinct TktOID from UTTR where Status='--'", Conn)
        For Each objRow As DataRow In tblOffcId.Rows
            RefreshTktStatus(objRow("TktOID"))
        Next
        MsgBox("Refresh Completed")
        Search()
    End Sub
    Private Function RefreshTktStatus(strOid As String) As Boolean
        Dim tblNewTkt As DataTable
        Dim tblRcpInfo As DataTable
        Dim lstQuerries As New List(Of String)

        'ExecuteNonQuerry("update UTTR set Counter='TVS' where TktOID='SGNVM2103'", Conn)
        'ExecuteNonQuerry("update UTTR set Counter='CWT' where TktOID='SGNVM27C8'", Conn)

        tblNewTkt = GetDataTable("select * from UTTR where status='--' and TktOid='" _
                                 & strOid & "'", Conn)

        For Each objRow As DataRow In tblNewTkt.Rows

            tblRcpInfo = GetDataTable("Select TOP 1 r.CustId,r.Counter" _
                                      & ", isnull(u.TktStatus,'OK') as TktStatus" _
                                      & " from Tkt t left join RCP r on t.RcpId=r.RecId" _
                                      & " left join UTTR u on t.Tkno=u.Tkno And u.Status='OK'" _
                                      & " where t.Status<>'XX' and t.Srv='S'" _
                                      & " and t.TKNO='" & objRow("TKNO") _
                                      & "' and r.Status='OK'" _
                                      & " order by t.RecId desc", Conn)
            lstQuerries.Add("update UTTR set CustId=" & tblRcpInfo.Rows(0)("CustId") _
                            & ",Counter='" & tblRcpInfo.Rows(0)("Counter") _
                            & "',TktStatus='" & tblRcpInfo.Rows(0)("TktStatus") _
                            & "' where RecId=" & objRow("RecId"))
        Next
        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Unable to find Counter/Customer for UTTR of" & strOid)
            Return False
        End If
        lstQuerries.Clear()
        'lstQuerries.Add("update UTTR set TktStatus='EX' where TktOid=" & strOid _
        '                & "' and Tkno in " _
        '                & " (Select Tkno from UTTR where Status='OK' and TktStatus='EX')")
        'lstQuerries.Add("update UTTR set TktStatus='NR' where TktOid=" & strOid _
        '                & "' and Tkno in " _
        '                & " (Select Tkno from UTTR where Status='OK' and TktStatus='NR')")
        'lstQuerries.Add("update UTTR set TktStatus='RF' where TktOid=" & strOid _
        '                & "' and Tkno in " _
        '                & " (Select Tkno from UTTR where Status='OK' and TktStatus='RF')")

        lstQuerries.Add("Update UTTR set Status='XX',LstUpdt=getdate(),LastUser='" _
                         & myStaff.SICode & "' where Status='OK' and TktOID='" _
                         & strOid & "'")
        lstQuerries.Add("Update UTTR set Status='OK' where Status='--' and TktOID='" _
                         & strOid & "'")
        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Unable to change Status for UTTR of" & strOid)
            Return False
        End If

        Return True
    End Function
End Class