
Public Class frmInvB4Tt78
    Private mblnFirstLoadCompleted As Boolean
    Private mstrInvSettingTable As String = "E_invSettings"
    Private mstrInvoiceTable As String = "E_Inv"
    Private mstrInvWebTable As String = "E_InvWeb"
    Private mstrInvDetailTable As String = "E_InvDetails"
    Private mstrInvDetailWebTable As String = "E_InvDetailsWeb"
    Private mstrInvLinkTable As String = "E_InvLinks"
    Private Sub frmInvB4Tt78_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Download()
        LoadCombo(cboTVC, "Select distinct TVC as Value from " & mstrInvSettingTable _
                    & " where Status<>'XX' order by TVC", Conn)
        LoadCombo(cboCustGroup, "select Val as Value from Misc where Cat='CustGroupName' and Val1='E'", Conn)
        LoadCombo(cboMauSo, "Select distinct MauSo as value from " & mstrInvoiceTable & " order by MauSo", Conn)
        LoadCombo(cboKyHieu, "Select distinct KyHieu as value from " & mstrInvoiceTable & " order by KyHieu", Conn)
        Clear()

        mblnFirstLoadCompleted = True
    End Sub

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Function Download() As Boolean
        Dim tblTvc As DataTable
        Dim strStartInvNo As String = "(select isnull(max(InvoiceNo),0)+1 from lib.dbo." & mstrInvWebTable & " w" _
                    & " where w.RecordStatus<>'xx' and w.Tvc=s.Tvc) as Start"
        Dim strSelect As String = "Select distinct Tvc,MauSo,KyHieu,City" & "," & strStartInvNo _
                            & " from lib.dbo." & mstrInvSettingTable & " s" & " where Status='ok'"
        Dim strOrder As String = " order by Tvc,MauSo,KyHieu"
        If pblnTT78 Then
            strSelect = strSelect & " and Tvc='TVTR' and MauSo='1/001'"
        End If
        tblTvc = GetDataTable(strSelect & strOrder, Conn_Web)

        For Each objRow As DataRow In tblTvc.Rows
            If Not SyncFromWeb(objRow("TVC"), CreateMauSo(objRow("MauSo")), CreateKyHieu(objRow("KyHieu")) _
                               , objRow("Start"), objRow("City")) Then
                MsgBox("Unable to download data from VNPT Web for " _
                       & objRow("TVC") & " " & CreateMauSo(objRow("MauSo")) & " " _
                       & objRow("KyHieu") _
                       & " " & objRow("Start") & " " & objRow("City"))
            End If
        Next

        If Not DownloadInvoiceDetail() Then
            MsgBox("Unable to download Invoice Details to Local Data. Please report Khanhnm!")
            Return False
        End If

        If Not MapWebLocal() Then
            MsgBox("Unable to map Web data with Local Data. Please report Khanhnm!")
            Return False
        End If

        Return True
    End Function
    Private Function Clear() As Boolean
        If pblnTestInv Then
            cboTVC.SelectedIndex = 3
        Else
            cboTVC.SelectedIndex = 0
        End If

        cboCustType.SelectedIndex = -1
        cboCustGroup.SelectedIndex = -1
        cboCustomer.SelectedIndex = -1
        cboAL.SelectedIndex = -1
        cboMauSo.SelectedIndex = -1
        cboKyHieu.SelectedIndex = -1
        txtInvoiceNo.Text = ""
        txtInvId.Text = ""
        cboBiz.SelectedIndex = -1
        'chkDraft.CheckState = CheckState.Indeterminate
        cboBackDate.SelectedIndex = 2
        Return True
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String
        strQuerry = "Select * from lib.dbo." & mstrInvWebTable & " where RecordStatus<>'XX' and City='" & myStaff.City & "'"

        AddEqualConditionCombo(strQuerry, cboTVC)
        AddEqualConditionCombo(strQuerry, cboBiz)
        AddEqualConditionCombo(strQuerry, cboAL)
        AddEqualConditionCombo(strQuerry, cboMauSo)
        AddEqualConditionCombo(strQuerry, cboKyHieu)
        AddEqualConditionText(strQuerry, txtInvoiceNo)
        AddEqualConditionText(strQuerry, txtInvId)

        If cboCustType.Text <> "" Then
            strQuerry = strQuerry & " and CustId in " _
                & " (Select CustId from Cust_Detail where Status='OK' and Cat='channel' and VAL='" _
                & cboCustType.Text & "')"
        End If
        If cboCustGroup.Text <> "" Then
            strQuerry = strQuerry & " and CustId in " _
                & " (Select intVal from Misc where Status='OK' and Cat='CustNameInGroup' and VAL='" _
                & cboCustGroup.Text & "')"
        End If

        If cboCustomer.Text <> "" Then
            strQuerry = strQuerry & " and CustId = " & cboCustomer.SelectedValue
        End If

        'If txtInvoiceContent.Text <> "" Then
        '    strQuerry = strQuerry & " and InvId in (select InvId from E_InvDetails where Status='OK' and Description like '%" & txtInvoiceContent.Text & "%')"
        'End If
        If IsNumeric(cboBackDate.Text) Then
            strQuerry = strQuerry & " and DateDiff(d,PublishDate,Getdate())<=" & cboBackDate.Text
        End If

        strQuerry = strQuerry & " order by RecId desc"
        LoadDataGridView(dgrE_Invoices, strQuerry, Conn_Web)
        'dgrE_Invoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        dgrE_Invoices.Columns("RecId").Width = 40
        dgrE_Invoices.Columns("InvId").Width = 40
        'dgrE_Invoices.Columns("InvType").Width = 40
        dgrE_Invoices.Columns("TVC").Width = 65
        'dgrE_Invoices.Columns("Biz").Width = 40
        'dgrE_Invoices.Columns("AL").Width = 30
        dgrE_Invoices.Columns("MauSo").Width = 80
        dgrE_Invoices.Columns("KyHieu").Width = 50
        dgrE_Invoices.Columns("InvoiceNo").Width = 50
        dgrE_Invoices.Columns("SRV").Width = 30
        dgrE_Invoices.Columns("PublishDate").Width = 70
        'dgrE_Invoices.Columns("FOP").Width = 50
        'dgrE_Invoices.Columns("Address").Width = 130
        dgrE_Invoices.Columns("InvStatus").Width = 48

        dgrE_Invoices.Columns("Vatable").DefaultCellStyle.Format = "#,##0"
        dgrE_Invoices.Columns("Amount").DefaultCellStyle.Format = "#,##0"
        dgrE_Invoices.Columns("Vatable").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrE_Invoices.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.Text = "E_Invoice List - " & dgrE_Invoices.RowCount & " records"
        Return True
    End Function

    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Dim frmEdit As New frmE_InvEdit
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub



    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim frmEdit As New frmE_InvEdit(dgrE_Invoices.CurrentRow)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub



    Private Sub lbkSyncFromWeb_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSyncFromWeb.LinkClicked
        pblnTT78 = False
        'pblnTestInv = False

        Dim frmListWeb As New frmE_InvListWeb
        'frmListWeb.DownloadMissingInbvoices()
        frmListWeb.Download(False)
        pblnTT78 = True
        'pblnTestInv = True
    End Sub
    Private Function SyncFromWeb(strTvc As String, strMauSo As String, strKyHieu As String _
                                 , intStartNo As Integer, strCity As String) As Boolean
        Dim objConnect As New clsE_InvConnect(pblnTT78, strTvc)
        Dim objE_Inv As New clsE_Invoice

        If Not objE_Inv.listInvFromNoToNo(objConnect.PortalServiceUrl, objConnect.UserName, objConnect.UserPass _
                                       , strMauSo, strKyHieu, intStartNo, intStartNo + 128) Then
            Return False
        Else
            'update into Local database
            Dim objXmlDoc As New Xml.XmlDocument
            Dim lstInvoices As Xml.XmlNodeList
            Dim lstQuerries As New List(Of String)


            objXmlDoc.LoadXml(objE_Inv.ReponseCode)
            lstInvoices = objXmlDoc.GetElementsByTagName("Item")
            For Each objNode As XmlNode In lstInvoices
                Dim intInvIndex As Integer
                Dim strInvToken As String = ""
                Dim strFkey As String = ""
                Dim intInvoiceNo As Integer
                Dim strDOI As String = ""
                Dim intSignStatus As Integer
                Dim decVatable As Decimal
                Dim decAmount As Decimal
                Dim intPayment As Integer
                Dim intStatus As Integer
                Dim strInvStatus As String = ""
                Dim strCustFullName As String = ""

                For Each objSubNode As XmlNode In objNode.ChildNodes
                    Select Case objSubNode.Name
                        Case "index"
                            intInvIndex = objSubNode.InnerText
                        Case "invToken"
                            strInvToken = objSubNode.InnerText
                        Case "fkey"
                            strFkey = objSubNode.InnerText
                        Case "publishDate"
                            strDOI = objSubNode.InnerText
                        Case "signStatus"
                            intSignStatus = objSubNode.InnerText
                        Case "total"
                            decVatable = objSubNode.InnerText
                        Case "amount"
                            decAmount = objSubNode.InnerText
                        Case "invNum"
                            intInvoiceNo = objSubNode.InnerText
                        Case "status"
                            intStatus = objSubNode.InnerText
                            strInvStatus = ConvertEInvStatus(intStatus)
                        Case "cusname"
                            strCustFullName = objSubNode.InnerText.Replace("<![CDATA[", "").Replace("]]>", "")
                            If strCustFullName.Contains("'") Then
                                strCustFullName = Replace(strCustFullName, "'", "''")
                            End If
                        Case "payment"
                            intPayment = objSubNode.InnerText
                    End Select
                Next

                lstQuerries.Add("Insert into LIB.dbo." & mstrInvWebTable & " (MauSo, KyHieu, InvoiceNo, PublishDate" _
                                & ", SignStatus, Vatable, Amount, Payment" _
                                & ", Status,InvStatus, RecordStatus, FstUser, City, TVC, CustFullName" _
                                & ", InvIndex, invToken, fkey) values ('" & strMauSo _
                                & "','" & strKyHieu & "'," & intInvoiceNo _
                                & ",'" & strDOI & "'," & intSignStatus & "," & decVatable _
                                & "," & decAmount & "," & intPayment & "," & intStatus _
                                & ",'" & strInvStatus & "','QQ','" & myStaff.SICode & "','" & strCity _
                                & "','" & strTvc & "',N'" & strCustFullName & "'," & intInvIndex _
                                & ",'" & strInvToken & "','" & strFkey & "')")
            Next

            Return UpdateListOfQuerries(lstQuerries, Conn_Web)

        End If

    End Function
    Private Function DownloadInvoiceDetail() As Boolean
        Dim tblInvoices As DataTable
        Dim strQuerry As String
        Dim strFields As String
        Dim strValues As String

        Dim lstQuerries As New List(Of String)
        Dim objConnect As clsE_InvConnect = Nothing
        strQuerry = "Select *" _
                    & " from LIB.dbo." & mstrInvWebTable _
                    & " where HasDetails=0 and StopDownload='false'" _
                    & " order by Tvc,MauSo,KyHieu,RecId"

        tblInvoices = GetDataTable(strQuerry, Conn_Web)
        For Each objRow As DataRow In tblInvoices.Rows
            Dim objE_Inv As New clsE_Invoice
            Dim intGiamThue As Integer = 0
            If objConnect Is Nothing Then
                objConnect = New clsE_InvConnect(pblnTT78, objRow("TVC"))
            ElseIf objConnect.Tvc <> objRow("TVC") Then
                objConnect = New clsE_InvConnect(pblnTT78, objRow("TVC"))
            End If

            If objRow("Payment") = 0 AndAlso Not objE_Inv.downloadInvNoPay(objConnect.PortalServiceUrl, objConnect.UserName _
                                        , objConnect.UserPass, objRow("InvToken")) Then
                If objE_Inv.ReponseCode = "ERR:12" Then
                    ExecuteNonQuerry("update LIB.dbo." & mstrInvWebTable & " Set StopDownLoad='True' where RecId=" & objRow("RecId"), Conn)
                Else
                    MsgBox("Unable to get Invoice Details from Web for Invoice Recid " & objRow("RecId") _
                           & vbNewLine & objE_Inv.ResponseDesc)
                End If

                Continue For

            ElseIf objRow("Payment") = 1 AndAlso Not objE_Inv.downloadInv(objConnect.PortalServiceUrl, objConnect.UserName _
                                        , objConnect.UserPass, objRow("InvToken")) Then
                MsgBox("Unable to get Invoice Details from Web for Invoice Recid " & objRow("RecId"))
                Continue For
            End If

            Dim objContent As New clsE_InvContent

            If pblnTT78 AndAlso Not objContent.ParseXmlTT78(objE_Inv.ReponseCode) Then
                MsgBox("Unable to parse Invoice Content for Invoice Recid " & objRow("RecId"))
            ElseIf Not pblnTT78 AndAlso Not objContent.ParseXml(objE_Inv.ReponseCode) Then
                MsgBox("Unable to parse Invoice Content for Invoice Recid " & objRow("RecId"))
            Else
                With objContent

                    If pblnTT78 Then
                        strQuerry = "Update LIB.dbo." & mstrInvWebTable & " set HasDetails=1,Buyer=N'" _
                        & .Buyer & "',FOP='" & .FOP & "',CustId=" & .CustId & ",CusCode='" & .CusCode _
                        & "',Extra1='" & .Extra1 & "',Extra2='" & .Extra2 _
                        & "',EmailDeliver='" & .EmailDeliver & "',Discount_Rate=" & .Discount_Rate _
                        & ",Discount_Amount=" & .Discount_Amount & ",PBan='" & .PBan & "',THDon='" & .THDon _
                        & "',ROE=" & .Roe & ",Vat=" & .TotalVat & ",MaCoQuanThue='" & .MaCoQuanThue & "'"
                        If .LinkedTCHDon <> "" Then
                            strQuerry = strQuerry & ",LinkedTCHDon='" & .LinkedTCHDon & "',LinkedLHDCLQuan='" & .LinkedLHDCLQuan _
                                & "',LinkedMauSo='" & .LinkedMauSo & "',LinkedKyHieu='" & .LinkedKyHieu _
                                & "',LinkedInvNbr=" & .LinkedInvNbr & ",LinkedInvDOI='" & CreateFromDate(.LinkedInvDOI) _
                                & "',LinkedGhiChu=N'" & .LinkedGhiChu & "'"
                        End If
                        strQuerry = strQuerry & " where RecId=" & objRow("RecId")
                    Else
                        If objRow("MauSo").ToString.EndsWith("01") AndAlso .Extra <> "" Then
                            intGiamThue = .Extra
                        End If
                        strQuerry = "Update LIB.dbo." & mstrInvWebTable & " set HasDetails=1,Buyer=N'" _
                        & .Buyer & "',FOP='" & .FOP & "',CustId=" & .CustId & ",CusCode='" & .CusCode _
                        & "',Extra=N'" & .Extra & "',Extra1='" & .Extra1 _
                        & "',Extra2='" & .Extra2 & "',Extras='" & .Extras _
                        & "',EmailDeliver='" & .EmailDeliver & "',Discount_Rate=" & .Discount_Rate _
                        & ",Discount_Amount=" & .Discount_Amount & ",GrossValue=" & .GrossValue _
                        & ",GrossValue0=" & .GrossValue0 & ",GrossValue5=" & .GrossValue5 _
                        & ",GrossValue10=" & .GrossValue10 & ",GrossValueNonTax=" & .GrossValueNonTax _
                        & ",VAT_Rate=" & .VAT_Rate & ",IsReplace=N'" & .IsReplace _
                        & "',IsAdjust=N'" & .IsAdjust & "',GiamThue=" & intGiamThue _
                        & " where RecId=" & objRow("RecId")
                    End If
                    lstQuerries.Add(strQuerry)


                    For Each objPro As clsProduct In .Products
                        With objPro
                            If pblnTT78 Then
                                strQuerry = "insert into lib.dbo." & mstrInvDetailWebTable & " (InvIdWeb" _
                            & ",InvToken, ProdName, Unit, Qty" _
                            & ", Price, DiscountRate, DiscountAmt, VatPct, VAT, TotalPrice" _
                            & ", TChat, Remark, ProdNo) values (" & objRow("Recid") _
                            & ",'" & objRow("InvToken") & "',N'" & .ProdName & "',N'" & .ProdUnit _
                            & "'," & .ProdQuantity & "," & .ProdPrice & "," & .DiscountRate _
                            & "," & .DiscountAmount & "," & .VatRate & "," & .VatAmount _
                            & "," & .TotalPrice & ",'" & .TChat & "','" & .Remark & "','" & .ProdNo & "')"
                            Else
                                strQuerry = "insert into lib.dbo." & mstrInvDetailWebTable & " (InvIdWeb" _
                            & ",InvToken, ProdName, Unit, Qty" _
                            & ", Price, DiscountRate, DiscountAmt, VatPct, VAT, Total" _
                            & ", Extra1, Extra2, Remark, ProdNo) values (" & objRow("Recid") _
                            & ",'" & objRow("InvToken") & "',N'" & .ProdName & "','" & .ProdUnit _
                            & "'," & .ProdQuantity & "," & .ProdPrice & "," & .DiscountRate _
                            & "," & .DiscountAmount & "," & .VatRate & "," & .VatAmount _
                            & "," & .TotalPrice & ",N'" & .Extra1 & "',N'" & .Extra2 _
                            & "','" & .Remark & "','" & .ProdNo & "')"
                            End If

                        End With
                        lstQuerries.Add(strQuerry)
                    Next

                End With

                If Not UpdateListOfQuerries(lstQuerries, Conn_Web) Then
                    MsgBox("Unable to update Invoice Details from Web to Local for Invoice Recid " & objRow("RecId"))
                End If
                lstQuerries.Clear()

            End If

        Next
        Return True

    End Function
    Private Function MapWebLocal() As Boolean
        Dim tblInvoices As DataTable
        Dim strQuerry As String
        Dim lstQuerries As New List(Of String)
        strQuerry = "Select w.*, l.InvId as LocalInvId,l.Srv as LocalSRV" _
                    & ",l.Biz as LocalBiz,l.AL as LocalAL,l.RecId as LocalId" _
                    & " from LIB.dbo." & mstrInvWebTable & " w" _
                    & " left join LIB.dbo." & mstrInvoiceTable & " l on w.Tvc=l.Tvc And w.InvToken=l.InvToken" _
                    & " where w.RecordStatus='QQ' and l.InvId<>0 " _
                    & " order by w.RecId"

        tblInvoices = GetDataTable(strQuerry, Conn_Web)
        For Each objRow As DataRow In tblInvoices.Rows

            'update web, local records
            lstQuerries.Add("Update LIB.dbo." & mstrInvWebTable & " set RecordStatus='OK', InvId=" & objRow("LocalInvId") _
                            & ",SRV='" & objRow("LocalSRV") & "',Biz='" & objRow("LocalBiz") _
                            & "',AL='" & objRow("LocalAL") _
                            & "' where RecId=" & objRow("RecId"))
            If objRow("MaCoQuanThue") <> "" Then
                lstQuerries.Add("Update LIB.dbo." & mstrInvoiceTable & " set MaCoQuanThue='" & objRow("MaCoQuanThue") _
                            & "' where RecId=" & objRow("LocalId"))
            End If
        Next
        Return UpdateListOfQuerries(lstQuerries, Conn_Web)

    End Function

    Private Sub lbkAdjustInfo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdjustInfo.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim frmEdit As New frmE_InvEdit(, InvAdjustType.ChangeInfo, dgrE_Invoices.CurrentRow _
                                        , True, cboTVC.Text, True)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkAdjustIncrease_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdjustIncrease.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim frmEdit As New frmE_InvEdit(, InvAdjustType.IncreaseAmt, dgrE_Invoices.CurrentRow _
                                        , True, cboTVC.Text, True)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub

    Private Sub lbkAdjustDecrease_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdjustDecrease.LinkClicked
        If dgrE_Invoices.CurrentRow Is Nothing Then Exit Sub
        Dim frmEdit As New frmE_InvEdit(, InvAdjustType.DecreaseAmt, dgrE_Invoices.CurrentRow, True, cboTVC.Text, True)
        If frmEdit.ShowDialog = DialogResult.OK Then
            Search()
        End If
    End Sub
    Private Sub lbkFindMissing_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFindMissing.LinkClicked
        pblnTT78 = False
        pblnTestInv = False
        Dim frmListWeb As New frmE_InvListWeb
        frmListWeb.DownloadMissingInbvoices()
        pblnTT78 = True
        pblnTestInv = True
    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Dim lstQuerries As New List(Of String)
        'lstQuerries.Add(ChangeStatus_ByID(mstrInvWebTable, "XX", dgrE_Invoices.CurrentRow.Cells("RecId").Value))
        'lstQuerries.Add(ChangeStatus_ByDK(mstrInvDetailWebTable, "XX", "InvId=" & dgrE_Invoices.CurrentRow.Cells("InvId").Value))
        ''lstQuerries.Add(ChangeStatus_ByDK(mstrInvLinkTable, "XX", "InvId=" & dgrE_Invoices.CurrentRow.Cells("InvId").Value))
        If pblnTT78 Then
            If Not pblnTestInv Then
                lstQuerries.Add("update lib.dbo." & mstrInvWebTable & " set invStatus='XX' where InvId=" & dgrE_Invoices.CurrentRow.Cells("InvId").Value)
                lstQuerries.Add("update lib.dbo." & mstrInvDetailWebTable & " set Status='XX' where InvIdWeb=" & dgrE_Invoices.CurrentRow.Cells("RecId").Value)
            End If

            lstQuerries.Add("insert into lib.dbo.E_InvNotice (InvRecId, InvFkey, TVC, MauSo, KyHieu, InvoiceNo,DOI, MaCoQuanThue" _
                            & ",InvType, Action,NoOriInv,FstUser,City)" _
                            & " select RecId, Fkey, TVC, MauSo, KyHieu, InvoiceNo,PublishDate,MaCoQuanThue,3,'1-Cancel','True'," _
                            & myStaff.StaffId & ",'" & myStaff.City _
                            & "' from lib.dbo." & mstrInvWebTable & " where RecId=" & dgrE_Invoices.CurrentRow.Cells("RecId").Value)
        End If
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            If pblnTestInv Then
                MsgBox("Coi như hoàn thành. Trong môi trường TEST hóa đơn không được chuyển trạng thái sang XX vì ảnh hưởng dữ liệu thực!")
            Else
                Search()
            End If
        Else
            MsgBox("Không hủy được hóa đơn. Đề nghị thông báo người lập trình RAS")
        End If
    End Sub

    Private Sub lbkViewInvoiceFromWeb_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewInvoiceFromWeb.LinkClicked
        Dim objE_Inv As New clsE_Invoice

        With dgrE_Invoices.CurrentRow
            Dim objConnect As New clsE_InvConnect(False, .Cells("TVC").Value)
            Dim strInvoiceToken As String = CreateInvToken(.Cells("MauSo").Value, .Cells("KyHieu").Value, .Cells("InvoiceNo").Value)
            If Not objE_Inv.getInvViewNoPay(objConnect.PortalServiceUrl, objConnect.UserName, objConnect.UserPass _
                                       , strInvoiceToken) Then
                MsgBox("Unable to download E Invoice!" & vbNewLine & objE_Inv.ResponseDesc)
            Else
                Dim strPath As String = "D:\Invoice " & .Cells("InvoiceNo").Value & ".html"
                Dim objLogFile As New System.IO.StreamWriter(strPath)
                objLogFile.WriteLine(objE_Inv.ResponseDesc)
                objLogFile.Close()
                objLogFile = Nothing
                Process.Start(strPath)

            End If
        End With
    End Sub

    Private Sub dgrE_Invoices_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrE_Invoices.CellClick
        Dim strQuerry As String = "select * from lib.dbo.E_InvDetailsWeb where InvIdWeb=" _
            & dgrE_Invoices.CurrentRow.Cells("RecId").Value
        LoadDataGridView(dgrE_InvDetails, strQuerry, Conn)
    End Sub
End Class

