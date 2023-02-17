Imports HtmlAgilityPack
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class frmVnCorpTkt
    Dim mlbnPageReady As Boolean
    'Dim mstrCounter As String
    Dim mblnCheckContent As Boolean
    Dim mdteEndTime As Date
    Dim mblnCompleted As Boolean
    Dim mblnForceCompleted As Boolean
    Dim mstrCheckedField As String
    Dim mstrCheckedValue As String
    Dim mintElapsedTime As Integer
    Dim mblnFirstLoadCompleted

    Private mstrCurrentCorp As String

    Private Sub frmVnCorpTkt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        Select Case Now.Month
            Case > 9
                cboQuarter.Text = 3
                cboYear.Text = Now.Year
            Case > 6
                cboQuarter.Text = 2
                cboYear.Text = Now.Year
            Case > 3
                cboQuarter.Text = 1
                cboYear.Text = Now.Year
            Case Else
                cboQuarter.Text = 4
                cboYear.Text = Now.Year - 1
        End Select
        pobjTvcs.LoadCombo(cboVnCorpId, "Select '' as value UNION " _
                           & " select distinct cast(VnCorpId as varchar) as value from Go_CompanyInfo1" _
                           & " where Status='OK' and VnCorpId>0")

        mblnFirstLoadCompleted = True
    End Sub
    Private Function SignIn() As Boolean
        Dim strUserName As String = "CTV"
        Dim strPassword As String = "tvcs170"

        'If wb.Url.ToString = "http://agent.vietnamair.com.vn/" Then
        wb.Document.GetElementById("MainContent_LoginUser_UserName").InnerText = strUserName
        wb.Document.GetElementById("MainContent_LoginUser_Password").InnerText = strPassword
        wb.Document.GetElementById("MainContent_LoginUser_LoginButton").InvokeMember("click")

        'wb.Navigate("javascript:function%20x(){document.getElementById('btLogin').click()}x()")
        WaitForPageLoad()
        If wb.Url.ToString.EndsWith("CA_ticket.aspx") Then
            'pstrCounter = strCounter
            'btnSignIn.Enabled = False
            Return True
        Else
            MsgBox("Unable to Sign In")
            Return False
        End If
        'End If
    End Function
    'Private Sub lbkGetTkts_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkGetTkts.LinkClicked
    '    Dim objHtmlDoc As New HtmlAgilityPack.HtmlDocument
    '    objHtmlDoc.LoadHtml(wb.DocumentText)
    '    Dim lstQuerries As New List(Of String)
    '    Dim intYear As Integer
    '    Dim intQuarter As Integer
    '    Dim i As Integer

    '    intYear = wb.Document.GetElementById("MainContent_txtNam").GetAttribute("value")
    '    intQuarter = wb.Document.GetElementById("MainContent_ddlKy").GetAttribute("value")

    '    lstQuerries.Add("Update [GO_CorpTkts] set Status='XX',LstUser='" & pobjUser.UserName _
    '                    & "',LstUpdate=getdate() where Status='OK' and IssYear=" _
    '                    & intYear & " and Quarter=" & intQuarter)

    '    Dim arrCorps As String() = wb.Document.GetElementById("MainContent_ddlCA_Code").InnerText.Trim.Split(" ")
    '    Dim rgCorp As New System.Text.RegularExpressions.Regex("^[selected value=]")
    '    Dim intCorp As Integer = Split(wb.Document.GetElementById("MainContent_ddlCA_Code").InnerHtml, "selected value=")(1).Split(">")(0)
    '    lstQuerries.AddRange(GetCorpTkt)

    '    For i = 1 To arrCorps.Length - 1
    '        wb.Document.GetElementById("MainContent_ddlCA_Code").SetAttribute("value", arrCorps(i))
    '        wb.Document.GetElementById("MainContent_ddlCA_Code").RaiseEvent("onchange")
    '        'WaitForPageLoad()
    '        Threading.Thread.Sleep(5000)
    '        lstQuerries.AddRange(GetCorpTkt)
    '    Next

    '    If lstQuerries.Count > 1 AndAlso pobjTvcs.UpdateListQuerries(lstQuerries, False) Then
    '        MsgBox("Completed")
    '    Else
    '        MsgBox("UnCompleted" & lstQuerries.Count)
    '    End If


    'End Sub
    Private Function GetCorpTkt() As Boolean
        Dim intYear As Integer
        Dim intQuarter As Integer
        Dim lstCodes As New List(Of Integer)
        Dim objHtmlDoc As New HtmlAgilityPack.HtmlDocument
        objHtmlDoc.LoadHtml(wb.DocumentText)
        Dim colHtmlNodeTkts As HtmlNodeCollection
        Dim lstQuerries As New List(Of String)
        Dim myCulture As System.Globalization.CultureInfo = New Globalization.CultureInfo("en-US")
        Dim strDomInt As String

        If wb.Document.GetElementById("MainContent_ddlINTDOM").InnerHtml.Contains("selected value=I") Then
            strDomInt = "INT"
        Else
            strDomInt = "DOM"
        End If

        intYear = wb.Document.GetElementById("MainContent_txtNam").GetAttribute("value")
        intQuarter = wb.Document.GetElementById("MainContent_ddlKy").GetAttribute("value")
        Dim intCorp As Integer = Split(wb.Document.GetElementById("MainContent_ddlCA_Code").InnerHtml, "selected value=")(1).Split(">")(0)
        colHtmlNodeTkts = objHtmlDoc.DocumentNode.SelectNodes("//table//table")
        Dim strPaxName As String
        Dim strTkno As String
        Dim dteDOI As Date

        lstQuerries.Add("Update [GO_CorpTkts] set Status='XX',LstUser='" & myStaff.SICode _
                        & "',LstUpdate=getdate() where Status='OK' and IssYear=" _
                        & intYear & " and Quarter=" & intQuarter _
                        & " and DomInt='" & strDomInt & "' and CorpId=" & intCorp)

        For Each objNode As HtmlNode In colHtmlNodeTkts
            If objNode.InnerText.Contains("STT") Then
                Dim blnStart As Boolean
                For Each objSubNode As HtmlNode In objNode.ChildNodes
                    If objSubNode.InnerText.Contains("STT") Then
                        blnStart = True
                    ElseIf blnStart Then
                        strPaxName = objSubNode.ChildNodes(3).ChildNodes(1).InnerText
                        If strPaxName = "" Then
                            Exit For
                        End If
                        strTkno = FormatRasTkno(objSubNode.ChildNodes(5).ChildNodes(1).InnerText)
                        dteDOI = Date.ParseExact(objSubNode.ChildNodes(4).ChildNodes(1).InnerText, "dd/MM/yyyy", myCulture)
                        lstQuerries.Add("insert into GO_CorpTkts " _
                                        & "(Status,IssYear, Quarter,CorpId,DomInt" _
                                        & ",Tkno, PaxName, DOI, FstUser)" _
                                        & " values ('OK'," & intYear & "," & intQuarter _
                                        & "," & intCorp & ",'" & strDomInt _
                                        & "','" & strTkno & "','" & strPaxName _
                                        & "','" & CreateFromDate(dteDOI) & "','" & myStaff.SICode & "')")
                    End If

                Next

                Exit For
            End If
        Next
        If lstQuerries.Count > 1 _
            AndAlso pobjTvcs.UpdateListQuerries(lstQuerries, False) Then
            Return True
        Else
            Return False
        End If
        Return True
    End Function
    'Private Sub lbkCheck_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCheck.LinkClicked

    'End Sub

    Private Sub WaitForPageLoad()
        mlbnPageReady = False

        lblStatus.Text = "Please wait"
        AddHandler wb.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        While Not mlbnPageReady
            System.Windows.Forms.Application.DoEvents()
        End While

    End Sub
    Private Sub WaitForElement(strElementId As String)
        mlbnPageReady = False

        lblStatus.Text = "Please wait"
        AddHandler wb.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        While Not mlbnPageReady
            If wb.DocumentText.Contains(strElementId) Then
                Exit Sub
            End If
            System.Windows.Forms.Application.DoEvents()
        End While

    End Sub

    Private Sub PageWaiter(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
        If wb.ReadyState = WebBrowserReadyState.Complete Then
            Me.Text = wb.Url.ToString
            mlbnPageReady = True
            RemoveHandler wb.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
            Threading.Thread.Sleep(1000)
            lblStatus.Text = "OK"
        End If
    End Sub

    Private Sub lbkImport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkImport.LinkClicked
        Dim ofd As New OpenFileDialog
        Dim i As Integer
        Dim strDomInt As String
        Dim strFromDate As String
        Dim strToDate As String
        Dim dteFromDate As Date
        Dim dteTodate As Date

        With ofd
            .Filter = "PDF files|*.xls"

        End With
        If ofd.ShowDialog = DialogResult.OK Then
            Dim objExcel As New Excel.Application
            Dim objWbk As Workbook
            Dim objWsh As Worksheet
            Dim arrDateBreak As String()
            Dim myCulture As System.Globalization.CultureInfo = New Globalization.CultureInfo("en-US")
            Dim intYear As Integer
            Dim intQuarter As Integer
            Dim strSrv As String = "S"
            Dim lstQuerries As New List(Of String)
            Dim intCorpId As Integer
            Dim dteDOI As Date

            objExcel.Workbooks.Open(ofd.FileName,, True)
            objWbk = objExcel.ActiveWorkbook
            objWsh = objWbk.ActiveSheet
            With objWsh
                If CStr(.Range("A3").Value).Contains("QUỐC NỘI") Then
                    strDomInt = "DOM"
                ElseIf CStr(objWsh.Range("A3").Value).Contains("QUỐC TẾ") Then
                    strDomInt = "INT"
                Else
                    MsgBox("Invalid file content")
                    Exit Sub
                End If
                arrDateBreak = Split(.Range("A4").Value, ":")
                strFromDate = Split(arrDateBreak(1).Trim, " ")(0)
                strToDate = arrDateBreak(2).Trim
                dteFromDate = Date.ParseExact(strFromDate, "dd/MM/yyyy", myCulture)
                dteTodate = Date.ParseExact(strToDate, "dd/MM/yyyy", myCulture)

                intYear = dteFromDate.Year
                Select Case dteFromDate.Month
                    Case 1, 2, 3
                        intQuarter = 1
                    Case 4, 5, 6
                        intQuarter = 2
                    Case 7, 8, 9
                        intQuarter = 3
                    Case 10, 11, 12
                        intQuarter = 4
                End Select
                lstQuerries.Add("Update [GO_CorpTkts] set Status='XX',LstUser='" & myStaff.SICode _
                        & "',LstUpdate=getdate() where Status='OK' and IssYear=" _
                        & intYear & " and Quarter=" & intQuarter _
                        & " and DomInt='" & strDomInt & "'")

                For i = 7 To objExcel.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row
                    Select Case .Range("A" & i).Value
                        Case "BÁN", "ĐỔI"
                            strSrv = "S"
                        Case "HOÀN"
                            strSrv = "R"

                        Case Else
                            If .Range("A" & i).Value IsNot Nothing AndAlso CStr(.Range("A" & i).Value).StartsWith("Corprate Account:") Then
                                intCorpId = CStr(.Range("A" & i).Value).Replace(" ", "").Split(":")(1)
                                Continue For
                            ElseIf .Range("C" & i).Value = "" Then
                                Continue For
                            ElseIf .Range("C" & i).Value = "Tên khách" Then
                                Continue For
                            Else
                                'MsgBox(.Range("D" & i).Value)
                                dteDOI = .Range("D" & i).Value
                                lstQuerries.Add("insert into [GO_CorpTkts] (IssYear, Quarter, Status" _
                                                & ", DomInt, CorpId, Tkno, SRV, PaxName, DOI, FstUser) " _
                                                & " values (" & intYear & "," & intQuarter & ",'OK','" _
                                                & strDomInt & "'," & intCorpId _
                                                & ",'" & FormatRasTkno(.Range("E" & i).Value) _
                                                & "','" & strSrv & "','" & .Range("C" & i).Value _
                                                & "','" & CreateFromDate(dteDOI) _
                                                & "','" & myStaff.SICode & "')")

                            End If
                    End Select
                Next
            End With
            If lstQuerries.Count > 1 AndAlso pobjTvcs.UpdateListQuerries(lstQuerries, False) Then
                MsgBox("Completed")
            Else
                MsgBox("UnCompleted")
            End If
        End If

    End Sub

    Private Sub lbkCheck_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCheck.LinkClicked
        Dim tblMissingTkts As System.Data.DataTable
        Dim strQuerry As String
        Dim strWrongTktQuerry As String
        Dim strFromDate As String
        Dim strToDate As String
        Dim strDateFilter As String

        strFromDate = CreateFromDate(DateSerial(cboYear.Text, cboQuarter.Text * 3 - 2, 1))
        strToDate = CreateToDate(DateSerial(cboYear.Text, cboQuarter.Text * 3 + 1, 1).AddDays(-1))
        strDateFilter = " between '" & strFromDate & "' and '" & strToDate & "' "

        strQuerry = "select c.VnCorpId,r.CustShortName, t.Tkno,t.DOI,r1.DomInt,t.Itinerary" _
            & " from ras12.dbo.tkt t left join RAS12.dbo.RCP r on t.RCPID=r.RecID" _
            & " left join GO_CompanyInfo1 c on c.CustID=r.CustID" _
            & " left join ras12.dbo.reportdata r1 on t.RecId=r1.Tkid" _
            & " where t.status<>'xx' and t.srv='s' and Qty>0 and substring(tkno,1,3)='738'" _
            & " and SUBSTRING(t.rmk,1,4)='bizt' and doi" & strDateFilter _
            & " and r.status<>'xx' and c.status='ok' and c.VnCorpId > 0" _
            & " and t.TKNO not in (select tkno from GO_CorpTkts where status='ok'" _
            & " and doi " & strDateFilter & ")" _
            & " order  by c.VnCorpId,r.CustShortName, r1.domint desc, doi"
        tblMissingTkts = pobjTvcs.GetDataTable(strQuerry)

        If tblMissingTkts.Rows.Count = 0 Then
            MsgBox("No Missing Ticket")
        Else
            Dim objExcel As New Excel.Application
            Dim objWbk As Workbook
            Dim objWsh As Worksheet
            Dim i As Integer

            objExcel.Workbooks.Add()
            objExcel.Visible = True
            objWbk = objExcel.ActiveWorkbook
            objWsh = objWbk.ActiveSheet
            objWsh.Name = "MissingTkts"
            With objWsh
                For i = 0 To tblMissingTkts.Columns.Count - 1
                    .Cells(1, i + 1) = tblMissingTkts.Columns(i).ColumnName
                Next
                For i = 0 To tblMissingTkts.Rows.Count - 1
                    .Range("A" & i + 2 & ":E" & i + 2).Value = tblMissingTkts.Rows(i).ItemArray
                Next
                objWsh.Columns("A:E").EntireColumn.AutoFit
            End With

            'Lay cac ve co trong VN report nhung ko co trong RAS
            strWrongTktQuerry = "select tkno,SRV,DOI,CorpId,c.DomInt" _
            & " from GO_CorpTkts c" _
            & " left join ras12.dbo.tkt t on c.Tkno=t.tkno and c.Srv=t.Srv and c.Doi=t.Doi" _
            & " left join RAS12.dbo.RCP r on t.RCPID=r.RecID" _
            & " left join GO_CompanyInfo1 c1 on c1.CustID=r.CustID" _
            & " where c.status='ok' and t.doi " & strDateFilter _
            & " and t.status<>'xx' and Qty<>0 and substring(tkno,1,3)='738'" _
            & " and SUBSTRING(t.rmk,1,4)='bizt' and t.doi" & strDateFilter _
            & " and r.status<>'xx' and c1.status='ok' and c1.VnCorpId > 0" _
            & " order  by c.VnCorpId,r.CustShortName, c.domint desc, doi"

            objExcel.Workbooks.Add()
            objExcel.Visible = True
            objWbk = objExcel.ActiveWorkbook
            objWsh = objWbk.ActiveSheet
            objWsh.Name = "WrongTkts"
            With objWsh
                For i = 0 To tblMissingTkts.Columns.Count - 1
                    .Cells(1, i + 1) = tblMissingTkts.Columns(i).ColumnName
                Next
                For i = 0 To tblMissingTkts.Rows.Count - 1
                    .Range("A" & i + 2 & ":E" & i + 2).Value = tblMissingTkts.Rows(i).ItemArray
                Next
                objWsh.Columns("A:E").EntireColumn.AutoFit
            End With
            LoadGridTkts()
        End If
    End Sub

    Private Sub lbkLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkLogin.LinkClicked
        wb.Navigate("http://booker.sgn.vietnamair.com.vn/daily/CA_ticket.aspx")
        WaitForPageLoad()
        'If Not SignIn() Then
        '    Exit Sub
        'End If
    End Sub

    Private Sub lbkAddTkts_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddTkts.LinkClicked
        Dim strTkno As String = Replace(dgrTickets.CurrentRow.Cells("TKNO").Value, " ", "")
        wb.Document.GetElementById("MainContent_gvSale_Report_txtTicketID").InnerText = strTkno
        wb.Document.GetElementById("MainContent_gvSale_Report_lnkOK").InvokeMember("click")
        'WaitForElement("MainContent_gvSale_Report_bttAdd")
    End Sub

    Private Sub lbkSelectAddButton_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectAddButton.LinkClicked
        Try
            Dim intCorp As Integer = Split(wb.Document.GetElementById("MainContent_ddlCA_Code").InnerHtml, "selected value=")(1).Split(">")(0)
            Dim strDomInt As String

            If wb.Document.GetElementById("MainContent_ddlINTDOM").InnerHtml.Contains("selected value=I") Then
                strDomInt = "INT"
            Else
                strDomInt = "DOM"
            End If
            With dgrTickets.CurrentRow
                If .Cells("VnCorpId").Value <> intCorp Then
                    MsgBox("Wrong Corp ID selected")
                    Exit Sub
                ElseIf .Cells("DomInt").Value <> strDomInt Then
                    MsgBox("Wrong DOM/INT selected")
                    Exit Sub
                End If
            End With
            wb.Document.GetElementById("MainContent_gvSale_Report_bttAdd").InvokeMember("click")
            'WaitForElement("MainContent_gvSale_Report_txtTicketID")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cboDomInt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDomInt.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadGridTkts()
        End If
    End Sub
    Private Function LoadGridTkts() As Boolean
        Dim strQuerry As String
        Dim strFromDate As String
        Dim strToDate As String
        Dim strDateFilter As String

        strFromDate = CreateFromDate(DateSerial(cboYear.Text, cboQuarter.Text * 3 - 2, 1))
        strToDate = CreateToDate(DateSerial(cboYear.Text, cboQuarter.Text * 3 + 1, 1).AddDays(-1))
        strDateFilter = " between '" & strFromDate & "' and '" & strToDate & "' "

        strQuerry = "select c.VnCorpId,r.CustShortName, t.Tkno,t.DOI,r1.DomInt,t.Itinerary" _
            & " from ras12.dbo.tkt t left join RAS12.dbo.RCP r on t.RCPID=r.RecID" _
            & " left join GO_CompanyInfo1 c on c.CustID=r.CustID" _
            & " left join ras12.dbo.reportdata r1 on t.RecId=r1.Tkid" _
            & " where t.status<>'xx' and t.srv='s' and Qty>0 and substring(tkno,1,3)='738'" _
            & " and SUBSTRING(t.rmk,1,4)='bizt' and doi" & strDateFilter _
            & " and r.status<>'xx' and c.status='ok' and c.VnCorpId > 0" _
            & " and t.TKNO not in (select tkno from GO_CorpTkts where status='ok'" _
            & " and doi " & strDateFilter & ")"

        AddEqualConditionCombo(strQuerry, cboDomInt)
        AddEqualConditionCombo(strQuerry, cboVnCorpId)
        strQuerry = strQuerry & " order  by c.VnCorpId, domint desc,r.CustShortName, doi"

        pobjTvcs.LoadDataGridView(dgrTickets, strQuerry)
        lblRecords.Text = dgrTickets.RowCount & " Records"
        Return True
    End Function

    Private Sub cboVnCorpId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboVnCorpId.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            LoadGridTkts()
        End If
    End Sub

    Private Sub lbkRefreshData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRefreshData.LinkClicked
        GetCorpTkt()
        LoadGridTkts()
    End Sub

    Private Sub LbkChangeCorp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkChangeCorp.LinkClicked

        Timer1.Interval = 1000
        mstrCurrentCorp = wb.Document.GetElementById("MainContent_lblCA_Name").InnerText
        wb.Document.GetElementById("MainContent_ddlCA_Code").SetAttribute("value", 613)
        wb.Document.GetElementById("MainContent_ddlCA_Code").RaiseEvent("onchange")
        Timer1.Start()

    End Sub
    Private Function GetTktList() As List(Of String)
        Dim objHtmlDoc As New HtmlAgilityPack.HtmlDocument
        objHtmlDoc.LoadHtml(wb.DocumentText)
        Dim colHtmlNodeTkts As HtmlNodeCollection
        Dim lstQuerries As New List(Of String)
        Dim intCorp As Integer = Split(wb.Document.GetElementById("MainContent_ddlCA_Code").InnerHtml, "selected value=")(1).Split(">")(0)
        colHtmlNodeTkts = objHtmlDoc.DocumentNode.SelectNodes("//table//table")

        For Each objNode As HtmlNode In colHtmlNodeTkts
            If objNode.InnerText.Contains("STT") Then
                Dim blnStart As Boolean
                For Each objSubNode As HtmlNode In objNode.ChildNodes
                    If objSubNode.InnerText.Contains("STT") Then
                        blnStart = True

                    ElseIf blnStart Then
                        'If Not objSubNode.InnerText.Contains(intCorp) Then
                        '    Exit For
                        'End If
                        lstQuerries.Add(objSubNode.ChildNodes(5).ChildNodes(1).InnerText)
                    End If
                Next
                Exit For

            End If
        Next
        Return lstQuerries
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim strNewCorp As String
        Try
            strNewCorp = wb.Document.GetElementById("MainContent_lblCA_Name").InnerText
            If strNewCorp <> mstrCurrentCorp Then
                Timer1.Stop()
                MsgBox("Completed")
                Exit Sub
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub lbkChange2Dom_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkChange2Dom.LinkClicked
        wb.Document.GetElementById("MainContent_ddlINTDOM").SetAttribute("value", "D")
        wb.Document.GetElementById("MainContent_ddlINTDOM").RaiseEvent("onchange")
    End Sub

    Private Sub lbkChange2INT_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkChange2INT.LinkClicked
        wb.Document.GetElementById("MainContent_ddlINTDOM").SetAttribute("value", "I")
        wb.Document.GetElementById("MainContent_ddlINTDOM").RaiseEvent("onchange")
    End Sub

    Private Sub lbkImportDirectories_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkImportDirectory.LinkClicked

        Dim objExcel As New Excel.Application

        Dim objFolder As New FolderBrowserDialog
        objFolder.SelectedPath = "W:\CTS\Data-CWT\VN CA REPORT"
        If objFolder.ShowDialog = DialogResult.OK Then

            Dim arrFiles() As String = IO.Directory.GetFiles(objFolder.SelectedPath)

            For Each strFileName As String In arrFiles
                If strFileName.EndsWith(".xls") AndAlso
                    Not ImportVnCopTktFile(objExcel, strFileName) Then
                    MsgBox("unable to import file " & strFileName)
                End If
            Next

        End If

        objExcel.Quit()
        MsgBox("Import Completed")


    End Sub
    Private Function ImportVnCopTktFile(ByRef objExcel As Excel.Application, strFileName As String) As Boolean
        Dim strDomInt As String
        Dim strFromDate As String
        Dim strToDate As String
        Dim dteFromDate As Date
        Dim dteTodate As Date

        Dim objWbk As Workbook
        Dim objWsh As Worksheet
        Dim arrDateBreak As String()
        Dim myCulture As System.Globalization.CultureInfo = New Globalization.CultureInfo("en-US")
        Dim intYear As Integer
        Dim intQuarter As Integer
        Dim strSrv As String = "S"
        Dim lstQuerries As New List(Of String)
        Dim intCorpId As Integer
        Dim dteDOI As Date
        Dim i As Integer

        objExcel.Workbooks.Open(strFileName,, True)
        objWbk = objExcel.ActiveWorkbook
        objWsh = objWbk.ActiveSheet
        objExcel.Visible = True

        With objWsh
            If CStr(.Range("A3").Value).Contains("QUỐC NỘI") Then
                strDomInt = "DOM"
            ElseIf CStr(objWsh.Range("A3").Value).Contains("QUỐC TẾ") Then
                strDomInt = "INT"
            Else
                MsgBox("Invalid file content " & strFileName)
                Return False
            End If
            intCorpId = CStr(.Range("A7").Value).Replace(" ", "").Split(":")(1)

            arrDateBreak = Split(.Range("A4").Value, ":")
            strFromDate = Split(arrDateBreak(1).Trim, " ")(0)
            strToDate = arrDateBreak(2).Trim
            dteFromDate = Date.ParseExact(strFromDate, "dd/MM/yyyy", myCulture)
            dteTodate = Date.ParseExact(strToDate, "dd/MM/yyyy", myCulture)

            intYear = dteFromDate.Year
            Select Case dteFromDate.Month
                Case 1, 2, 3
                    intQuarter = 1
                Case 4, 5, 6
                    intQuarter = 2
                Case 7, 8, 9
                    intQuarter = 3
                Case 10, 11, 12
                    intQuarter = 4
            End Select
            lstQuerries.Add("Update [GO_CorpTkts] set Status='XX',LstUser='" & myStaff.SICode _
                        & "',LstUpdate=getdate() where Status='OK' and IssYear=" _
                        & intYear & " and Quarter=" & intQuarter _
                        & " and DomInt='" & strDomInt & "' and CorpId=" & intCorpId)

            For i = 7 To objExcel.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row
                Select Case .Range("A" & i).Value
                    Case "BÁN", "ĐỔI"
                        strSrv = "S"
                    Case "HOÀN"
                        strSrv = "R"

                    Case Else
                        If .Range("A" & i).Value IsNot Nothing AndAlso CStr(.Range("A" & i).Value).StartsWith("Corprate Account:") Then
                            intCorpId = CStr(.Range("A" & i).Value).Replace(" ", "").Split(":")(1)
                            Continue For
                        ElseIf .Range("C" & i).Value = "" Then
                            Continue For
                        ElseIf .Range("C" & i).Value = "Tên khách" Then
                            Continue For
                        Else

                            dteDOI = .Range("D" & i).Value
                            lstQuerries.Add("insert into [GO_CorpTkts] (IssYear, Quarter, Status" _
                                            & ", DomInt, CorpId, Tkno, SRV, PaxName, DOI, FstUser) " _
                                            & " values (" & intYear & "," & intQuarter & ",'OK','" _
                                            & strDomInt & "'," & intCorpId _
                                            & ",'" & FormatRasTkno(.Range("E" & i).Value) _
                                            & "','" & strSrv & "','" & .Range("C" & i).Value _
                                            & "','" & CreateFromDate(dteDOI) _
                                            & "','" & myStaff.SICode & "')")

                        End If
                End Select
            Next
            objWbk.Close(False)
        End With

        If lstQuerries.Count > 1 AndAlso pobjTvcs.UpdateListQuerries(lstQuerries, False) Then
            Return True
        Else
            Return False
        End If

    End Function
End Class