Public Class frmSelectTcode
    Private mblnFirstLoadCompleted As Boolean
    Private mobjEditRow As DataGridViewRow
    Private mobjOldSearch As clsTcodeSearch
    'Private mlstSelectedRows As New List(Of DataGridViewRow)
    Public Sub New(objEditRow As DataGridViewRow, Optional objOldSearch As clsTcodeSearch = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If objEditRow IsNot Nothing Then
            mobjEditRow = objEditRow
            txtVendorName.Text = objEditRow.Cells("ShortName").Value
            txtVendorName.Enabled = False
        End If
        If objOldSearch IsNot Nothing Then
            txtVendorName.Text = objOldSearch.Vendor
            txtAccountName.Text = objOldSearch.AccountName
            txtVendorId.Text = objOldSearch.VendorId
            dtpDOI.Value = objOldSearch.MaxEDate
            mobjOldSearch = objOldSearch
        End If
    End Sub
    Private Sub frmFindVendor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If mobjEditRow IsNot Nothing Or mobjOldSearch IsNot Nothing Then
            Search(0, txtVendorId.Text)
        End If

        mblnFirstLoadCompleted = True
    End Sub
    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked

        DialogResult = DialogResult.OK

    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked

        Dim strQuerry As String = "select distinct Cat,Vendor,VendorId" _
            & " ,isnull(AccountName,'') as AccountName" _
                        & " from dutoan_item i " _
                        & " left join Vendor v on i.VendorID=V.RecId and v.status='ok'" _
                        & " where i.status='ok' and v.Status<>'xx'"

        If txtVendorName.Text.Trim <> "" Then
            strQuerry = strQuerry & " And v.ShortName Like '%" & txtVendorName.Text & "%'"
        End If

        If txtAccountName.Text <> "" Then
            strQuerry = strQuerry & " and v.AccountName like N'%" & txtAccountName.Text.Trim & "%'"
        End If

        Dim tblVendor As DataTable = GetDataTable(strQuerry, Conn)
        Dim frmSelectVendor As New frmShowTableContent(tblVendor, "Select Vendor", "Vendor")
        If frmSelectVendor.ShowDialog = DialogResult.OK Then
            txtVendorName.Text = Trim(frmSelectVendor.SelectedRow.Cells("Vendor").Value)

            txtAccountName.Text = frmSelectVendor.SelectedRow.Cells("AccountName").Value.ToString.Trim
            txtVendorId.Text = frmSelectVendor.SelectedRow.Cells("VendorId").Value

        Else
            Exit Sub
        End If
        Search(0, txtVendorId.Text)

    End Sub

    Private Function Search(intUncId As Integer, intVendorId As Integer) As Boolean
        Dim strQuerry As String

        Dim strFilterUnlinkedTcode As String = String.Empty

        'If intVendorId = 0 Then
        '    strFilterUnlinkedTcode = " And t.RecId Not in (select distinct DutoanId from Dutoan_Pmt Where Status='OK'" _
        '                & " and PmtId in (Select RecId from UNC_payments Where Status='ok' and InvNo<>''" _
        '                & " and AccountName = '" & txtAccountName.Text & "'" _
        '                & " and ShortName = '" & txtVendorName.Text & "'))"
        'Else
        '    strFilterUnlinkedTcode = strFilterUnlinkedTcode & " and t.RecId not in (select distinct intVal1 from Misc m" _
        '                    & " left join VendorInvoice v on m.IntVal=v.InvID" _
        '                    & " Where m.Status='OK' and m.Cat='InvTcodeMap' and m.Val<>'Under'" _
        '                    & " and v.Status='OK' and v.VendorID=" & intVendorId & ")" _
        '                    & " And t.RecId Not in (select distinct DutoanId from Dutoan_Pmt Where Status='OK'" _
        '                    & " and PmtId in (Select RecId from UNC_payments Where Status='ok' and InvNo<>''" _
        '                    & " and AccountName ='" & txtAccountName.Text & "'" _
        '                    & " and ShortName = '" & txtVendorName.Text & "'))"
        'End If
        If intVendorId <> 0 Then
            strFilterUnlinkedTcode = strFilterUnlinkedTcode & " and t.RecId not in (select distinct intVal1 from Misc m" _
                        & " left join VendorInvoice v on m.IntVal=v.InvID" _
                        & " Where m.Status='OK' and m.Cat='InvTcodeMap' and m.Val<>'Under'" _
                        & " and v.Status='OK' and v.VendorID=" & intVendorId & ")"
        End If
        If intUncId = 0 Then
            strQuerry = "Select TCode, Pax, SDate, EDate" _
            & " ,sum(i.TTLtoVendor) As Amount,t.Status,t.CustId" _
            & ",Vendor,AccountName,i.VendorId,i.DutoanId" _
            & " from DuToan_tour t" _
            & " left join DuToan_Item i On t.RecID=i.DuToanID" _
            & " left join Vendor v On v.RecID=i.VendorID" _
            & " where t.status<>'xx' and i.Status='ok' and i.Vendor<>'TransViet'" _
            & " and i.VendorId=" & intVendorId _
            & " and t.Edate>='31 Dec 19 23:59'" _
            & " and edate<='" & CreateFromDate(dtpDOI.Value) _
            & "' and Vendor like '%" & txtVendorName.Text & "%'"
            AddLikeConditionText(strQuerry, txtAccountName, "AccountName",, True)
            'If mobjEditRow IsNot Nothing Then
            strQuerry = strQuerry & strFilterUnlinkedTcode
            'End If
            strQuerry = strQuerry & " group by TCode, Pax, SDate, EDate,Vendor,t.Status,t.CustId,AccountName,i.VendorId,i.DutoanId"
        Else
            strQuerry = "select TCode, Pax, SDate, EDate" _
            & " ,sum(i.TTLtoVendor) as Amount,t.Status" _
            & ",Vendor,AccountName,i.VendorId,i.DutoanId,t.CustId" _
            & " from DuToan_tour t" _
            & " left join DuToan_Item i on t.RecID=i.DuToanID" _
            & " where t.status<>'xx' and i.Status='ok' and i.Vendor<>'TransViet'" _
            & " and i.VendorId=" & intVendorId _
            & " and t.Edate>='31 Dec 19 23:59'" _
            & " and Tcode in (Select Tcode from [DuToan_Pmt] where Status='OK' and PmtId=" & intUncId & ")"
            AddLikeConditionText(strQuerry, txtAccountName, "AccountName")
            'If mobjEditRow IsNot Nothing Then
            strQuerry = strQuerry & strFilterUnlinkedTcode
            'End If
            strQuerry = strQuerry & " group by TCode, Pax, SDate, EDate,Vendor,AccountName,i.VendorId,i.DutoanId,t.Status,t.CustId"
        End If

        LoadDataGridView(dgrTcodes, strQuerry, Conn)
        With dgrTcodes
            .Columns("Pax").Width = 32
            .Columns("Selected").Width = 40
            .Columns("Status").Width = 40
            .Columns("Amount").DefaultCellStyle.Format = "#,###"
            '.Columns("AccountName").Visible = False
            .Columns("VendorId").Visible = False
            .Columns("DutoanId").Visible = False
            .Columns("CustId").Visible = False

        End With
        For Each objColumn As DataGridViewColumn In dgrTcodes.Columns
            Select Case objColumn.Name
                Case "Selected"
                Case Else
                    objColumn.ReadOnly = True
            End Select
        Next
    End Function




    Private Sub dgrTcodes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrTcodes.CellContentClick
        Dim strFOP As String

        If mblnFirstLoadCompleted Then
            LoadUnc(dgrTcodes.CurrentRow.Cells("Tcode").Value, dgrTcodes.CurrentRow.Cells("Vendor").Value)
        End If
        Try
            txtVendorName.Text = dgrTcodes.CurrentRow.Cells("Vendor").Value
            txtAccountName.Text = dgrTcodes.CurrentRow.Cells("AccountName").Value
            txtUncFOP.Text = GetColumnValuesAsString("UNC_Payments", "FOP" _
                                        , " where ShortName='" & txtVendorName.Text _
                                        & "' and  RecId in (Select PmtId from Dutoan_pmt where Status='OK' and DutoanId=" _
                                        & dgrTcodes.CurrentRow.Cells("DutoanId").Value & ")", ",")
        Catch ex As Exception

        End Try

    End Sub
    Private Function LoadUnc(strTcode As String, strVendorName As String)
        Dim strQuerry As String = "select RefNo,* from unc_Payments where Status='OK' " _
            & " and ShortName='" & strVendorName & "' And RecId in" _
            & " (SElect PmtId from Dutoan_Pmt where Status='OK' and Tcode='" & strTcode & "')"
        LoadDataGridView(dgrUNCs, strQuerry, Conn)
        Return True
    End Function
    Private Sub dgrTcodes_SelectionChanged(sender As Object, e As EventArgs) Handles dgrTcodes.SelectionChanged

    End Sub

    Private Sub dgrUNCs_SelectionChanged(sender As Object, e As EventArgs) Handles dgrUNCs.SelectionChanged

    End Sub

    Private Sub lbkSaveInvNo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSaveInvNo.LinkClicked
        Dim blnSelected As Boolean
        Dim i As Integer
        For Each objRow As DataGridViewRow In dgrTcodes.Rows
            With objRow
                If .Cells("Selected").Value Then
                    blnSelected = True
                    Exit For
                End If
            End With
        Next
        If Not blnSelected Then
            MsgBox("No Tcode selected!")
            Exit Sub
        End If
        For i = dgrTcodes.RowCount - 1 To 0 Step -1
            If dgrTcodes.Rows(i).Cells("Selected").Value = False Then
                dgrTcodes.Rows.RemoveAt(i)
            End If
        Next
        dgrTcodes.Columns.Add("Matched", "Matched")
        Me.DialogResult = DialogResult.OK
        'Me.Dispose()
    End Sub

    Private Sub dgrUNCs_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgrUNCs.CellContentClick
        If mblnFirstLoadCompleted Then
            'lay theo UNC duoc chon
            Search(dgrUNCs.CurrentRow.Cells("RecId").Value, 0)
        End If
    End Sub

    'Public Property Tcodes() As List(Of DataGridViewRow)
    '    Get
    '        Return mlstSelectedRows
    '    End Get
    '    Set(value As List(Of DataGridViewRow))
    '        mlstSelectedRows = value
    '    End Set
    'End Property



End Class
