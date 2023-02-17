Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class frmVatInvoicePrint4TVS
    Private mblnDataLoadCompleted As Boolean
    Private mobjExcel As New Excel.Application
    Private mobjWbk As Workbook
    Private mobjWsh As Worksheet
    'Private mblnLoadExcel As Boolean
    Private mstrAction As String
    Private mstrCustGroup As String
    Private mlstVatInvLinks As New List(Of clsVatInvLink)
    Private mintVatDiscount As Integer

    Public Sub New(intVatDiscount As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetUpValues(intVatDiscount)

    End Sub
    Private Function SetUpValues(intVatDiscount As Integer) As Boolean
        mintVatDiscount = intVatDiscount

        If intVatDiscount = 30 Then
            Me.Text = "VatInvoicePrint4TVS VAT7"
        Else
            Me.Text = "VatInvoicePrint4TVS No VAT Discount"
        End If
        Return True
    End Function
    Public Structure Action
        Const LoadAhc = "LoadAhc"
        Const LoadListing = "LoadListing"
        Const LoadTrx = "LoadTrx"
        Const LoadTkt = "LoadTkt"
        Const LoadTcode = "LoadTcode"
        Const LoadVatInv = "LoadVatInv"
    End Structure
    Private Sub frmVatInvoicePrint4Cwt_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        mobjExcel.Quit()

    End Sub
    Private Sub frmVatInvoicePrint4TVS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mstrAction = Action.LoadVatInv
        Clear()

        mblnDataLoadCompleted = True
    End Sub
    Private Function Clear() As Boolean
        cboCustType.SelectedIndex = 0
        'cboCustomer.SelectedIndex = 0
        cboRasDoc.SelectedIndex = 0
        txtTktItem.Text = ""
        Return True
    End Function
    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub
    Private Sub lbkLoadFile_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkLoadFile.LinkClicked
        Dim objOfd As New OpenFileDialog
        With objOfd
            .Filter = "excel files (*.xls)|"
            .ShowDialog()
            If .FileName = "" Then
                Exit Sub
            End If

            mstrAction = Action.LoadListing

            Dim objWsh As Worksheet
            'Dim i As Integer, j As Integer

            mobjWbk = mobjExcel.Workbooks.Open(.FileName, , True)
            mblnDataLoadCompleted = False
            cboSheetName.Items.Clear()

            For Each objWsh In mobjWbk.Sheets

                If objWsh.Name.StartsWith("DOM") _
                    Or objWsh.Name.StartsWith("INT") Then
                    cboSheetName.Items.Add(objWsh.Name)
                End If
            Next
            mblnDataLoadCompleted = True

        End With
    End Sub
    Private Sub lbkLoadTktTrx_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkLoadFromRAS.LinkClicked
        Dim strQuerry As String = ""
        Dim tblTkt As System.Data.DataTable
        Dim strCondition As String = " where t.Status<>'XX' and r.CustShortName='" & cboCustomer.Text & "'"
        Dim decVatRatio As Decimal = 1.1

        dgrTktListing.DataSource = Nothing
        dgrTktListing.Columns.Clear()
        Dim colS As New DataGridViewCheckBoxColumn
        colS.Name = "S"
        colS.DataPropertyName = "S"
        dgrTktListing.Columns.Add(colS)

        Select Case cboRasDoc.Text
            'Case "AHC"
            '    decVatRatio = GetVatRatio(ScalarToDate("Tkt", "TOP 1 DOI", "Status<>'xx' and tkno='" & txtTktItem.Text & "'"))
            '    strQuerry = "Select 'True' as S, t.RecId as tkid, t.tkno,t.Itinerary,t.PaxName" _
            '        & " ,0 as Fare_VND,0 as VAT4Fare_VND" _
            '        & ", 0  as ThuHo,0 as Vat4ThuHo,t.ChargeTV/" & decVatRatio & "as SfNoVat_VND" _
            '        & ",t.ChargeTV- (t.ChargeTV/" & decVatRatio & ") as Vat4SF" _
            '        & " , 0  as PhiKhac, 0  as Vat4PhiKhac,tr.Ref1 as OneWorldNumber,tr.Ref2 as CostCenter" _
            '        & ",tr.Ref4 As Approver,'' as ProjectTaskId,'' as MastNumber,'' as VesselName ,tr.RequiredData" _
            '        & ", t.ChargeTV As TotalVND, '',t.DocType as Service,t.RcpNo" _
            '        & ",l.VatInvID ,t.Srv,t.DOI from tkt t " _
            '        & " left join Rcp r on t.rcpid=r.Recid " _
            '        & " left join cwt.dbo.GO_MiscSvc a on a.ItemId=t.RecId " _
            '        & " left join cwt.dbo.go_travel tr On a.travelid=tr.RecId And tr.Status='OK' " _
            '        & " left join vatinvlinks l on l.vatinvid=r.recid and l.linktype='ETK' and l.Status='ok'" _
            '        & strCondition & " and t.tkno='" & txtTktItem.Text & "'"
            '    mstrAction = Action.LoadAhc
            Case "TKT"
                decVatRatio = GetVatRatio(ScalarToDate("TOP 1 Tkt", "DOI", "Status<>'xx' and tkno='" & txtTktItem.Text & "'"))
                strQuerry = "Select 'True' as S, r1.tkid, t.tkno,t.Itinerary,t.PaxName,r1.F_VND as Fare_VND,r1.VAT4Fare_VND" _
                    & ", round((case Dekho when 'DOM' then r1.T_VND-r1.VAT4Fare_VND else r1.T_VND end) - VAT4TfcNoUE_VND,0)  as ThuHo" _
                    & ",round(r1.VAT4TfcNoUE_VND,0) as Vat4ThuHo,r1.SfNoVat_VND,r1.TVCharge_VND-r1.SfNoVat_VND as Vat4SF" _
                    & ", round((case Dekho when 'DOM' then r1.C_VND/" & decVatRatio & " else r1.C_VND end),0)  as PhiKhac" _
                    & ", round((case Dekho when 'DOM' then r1.C_VND - r1.C_VND/" & decVatRatio & " else 0 end),0)  as Vat4PhiKhac" _
                    & ",tr.Ref1 as OneWorldNumber,tr.Ref2 as CostCenter,tr.Ref4 as Approver" _
                    & ",'' as ProjectTaskId,'' as MastNumber,'' as VesselName ,tr.RequiredData" _
                    & ", F_VND+T_VND +C_VND+TVCharge_VND as TotalVND, R1.DEKHO,t.DocType as Service" _
                    & ",l.VatInvID,t.RcpNo,t.Srv,t.DOI" _
                    & " from tkt t" _
                    & " left join Rcp r on t.rcpid=r.Recid" _
                    & " left join ReportData r1 on t.recid=r1.tkid"
                strQuerry = strQuerry _
                    & " left join vatinvlinks l on l.vatinvid=r.recid and l.linktype='ETK' and l.Status='ok'" _
                    & strCondition & " and t.tkno='" & txtTktItem.Text & "'"
                mstrAction = Action.LoadTkt
            Case "TRX"
                decVatRatio = GetVatRatio(ScalarToDate("Tkt", "top 1 DOI", "Status<>'xx' and RcpId =(select RecId from Rcp where status='OK' and RcpNo='" & txtTktItem.Text & "')"))

                strQuerry = "Select 'True' as S, r1.tkid, t.tkno,t.Itinerary,t.PaxName,r1.F_VND as Fare_VND,r1.VAT4Fare_VND" _
                    & ", round((case Dekho when 'DOM' then r1.T_VND-r1.VAT4Fare_VND else r1.T_VND end) - VAT4TfcNoUE_VND,0)  as ThuHo" _
                    & ",round(r1.VAT4TfcNoUE_VND,0) as Vat4ThuHo,r1.SfNoVat_VND,r1.TVCharge_VND-r1.SfNoVat_VND as Vat4SF" _
                    & ", round((case Dekho when 'DOM' then r1.C_VND/" & decVatRatio & " else r1.C_VND end),0)  as PhiKhac" _
                    & ", round((case Dekho when 'DOM' then r1.C_VND - r1.C_VND/" & decVatRatio & " else 0 end),0)  as Vat4PhiKhac" _
                    & ", F_VND+T_VND +C_VND+TVCharge_VND as TotalVND, R1.DEKHO,t.DocType as Service" _
                    & ",l.VatInvID,t.RcpNo,t.Srv,t.DOI" _
                    & " from tkt t" _
                    & " left join Rcp r on t.rcpid=r.Recid" _
                    & " left join ReportData r1 on t.recid=r1.tkid"

                strQuerry = strQuerry _
                    & " left join vatinvlinks l on l.vatinvid=r.recid and l.linktype='TRX' and l.Status='ok'" _
                    & strCondition & " and t.rcpno='" & txtTktItem.Text & "'"
                mstrAction = Action.LoadTrx
                'Case "TCODE"
                '    strQuerry = "select 'True' as S , i.RecId as ItemId,i.PaxName, i.service,i.Supplier" _
                '        & ",round(i.TTLToPax*100/(100+i.VAT),0) as AmountNoVAT" _
                '        & ",i.VAT as VatPct" _
                '        & ",(case i.vat when 0 then 0 else i.TTLToPax-round(i.TTLToPax*100/(100+i.VAT),0) end) as Vat" _
                '        & ",round(f.TTLToPax*100/(100+f.VAT),0) as SfNoVat_VND" _
                '        & ",f.VAT as VatPct4SF" _
                '        & ",f.TTLToPax-round(f.TTLToPax*100/(100+f.VAT),0) as VatSf" _
                '        & " ,b.TTLToPax as BankFee,b.VAT as VatPct4BankFee" _
                '        & " ,(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='ONE WORLD NUMBER') as OneWorldNumber" _
                '        & " ,(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='COST CENTER') as CostCenter" _
                '        & " ,(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='APPROVER/CLIENT NAME') as Approver" _
                '        & " ,(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='PROJECT TASK ID') as ProjectTaskId" _
                '        & " ,(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='MAST NUMBER') as MastNumber" _
                '        & ",(select fvalue from SIR where Status='OK' and RcpId=i.DuToanID and Prod='NonAir' and Fname='VESSEL NAME') as VesselName" _
                '        & ",(i.TTLToPax+ isnull(f.TtlToPax,0)) as TotalVND,i.Dutoanid,t.Tcode,t.RcpId" _
                '        & " from dutoan_item i" _
                '        & " left join dutoan_tour t on i.DuToanID=t.RecID" _
                '        & " left join dutoan_item f on i.RecID=f.RelatedItem and f.Service='TransViet SVC Fee' and f.Status='ok'" _
                '        & " left join dutoan_item b on i.RecID=b.RelatedItem and b.Service='Bank Fee' and b.Status='ok'" _
                '        & " where t.custshortname ='" & cboCustomer.Text & "'and t.Status='rr'" _
                '        & " and i.Status='OK' and i.Service not in ('TransViet SVC Fee','Bank Fee')" _
                '        & " and i.CostOnly=0 and t.Tcode='" & txtTktItem.Text & "'"

                '    mstrAction = Action.LoadTcode
        End Select


        tblTkt = GetDataTable(strQuerry, Conn)

        If tblTkt.Rows.Count = 0 Then
            MsgBox("Unable to get data from RAS!")
            Exit Sub
        Else
            dgrTktListing.DataSource = tblTkt
            'If mstrAction <> Action.LoadTcode Then
            '    If Not IsDBNull(tblTkt.Rows(0)("VatInvId").Value) Then
            '        MsgBox("Vat Invoice had been issued for " & txtTktItem.Text)
            '    End If
            '    For Each objRow As DataGridViewRow In dgrTktListing.Rows
            '        If mstrAction <> Action.LoadAhc AndAlso objRow.Cells("Service").Value = "AHC" Then
            '            MsgBox("Cần load AHC với số AHC tương ứng!")
            '            Exit Sub
            '        End If
            '        If Not IsDBNull(objRow.Cells("RequiredData").Value) Then
            '            objRow.Cells("ProjectTaskId").Value = GetRequiredDataValueByDataCode("UDID32", objRow.Cells("RequiredData").Value)
            '            objRow.Cells("MastNumber").Value = GetRequiredDataValueByDataCode("UDID18", objRow.Cells("RequiredData").Value)
            '            objRow.Cells("VesselName").Value = GetRequiredDataValueByDataCode("UDID30", objRow.Cells("RequiredData").Value)
            '        End If
            '    Next
            'End If
        End If

        RefreshListing()
    End Sub
    Private Sub lbkCreateE_Invoice_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateE_Invoice.LinkClicked
        If dgrTktListing.CurrentRow Is Nothing Then
            Exit Sub
        End If

        Dim frmE_Inv As New frmE_InvEdit
        frmE_Inv.LoadGridTktsAir4TVS(dgrTktListing, cboCustomer.Text, mintVatDiscount, cboSheetName.Text)
        frmE_Inv.ShowDialog()

    End Sub
    Private Function RefreshListing() As Boolean
        For Each objCol As DataGridViewColumn In dgrTktListing.Columns
            Select Case objCol.Name
                Case "Total " & vbLf & "Before VAT", "VAT", "Airline" & vbLf & "Refund Charge", "Total" & vbLf & "VND"
                    objCol.DefaultCellStyle.Format = "#,##0"
            End Select
            If objCol.Name <> "S" Then
                objCol.ReadOnly = True
            End If
        Next
        dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    End Function

    Private Sub cboSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSheetName.SelectedIndexChanged
        If mblnDataLoadCompleted Then

            For Each objWsh As Worksheet In mobjWbk.Sheets
                If objWsh.Name = cboSheetName.Text Then
                    LoadWorksheet2Datagridview(objWsh)

                    dgrTktListing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                    mstrAction = Action.LoadListing
                End If
            Next

        End If
    End Sub
    Private Function LoadWorksheet2Datagridview(objWsh As Worksheet) As Boolean
        Dim intMaxColumn As Integer
        Dim i As Integer, j As Integer
        Dim intHeaderRow As Integer
        dgrTktListing.DataSource = Nothing
        dgrTktListing.Rows.Clear()
        dgrTktListing.Columns.Clear()
        dgrTktListing.Columns.Add("Sheet", "Sheet")


        intHeaderRow = 7

        For i = 1 To 54
            If objWsh.Cells(intHeaderRow, i) Is Nothing Or objWsh.Cells(intHeaderRow, i).value = "" Then
                intMaxColumn = i
                Exit For
            Else
                dgrTktListing.Columns.Add(objWsh.Cells(intHeaderRow, i).value, objWsh.Cells(intHeaderRow, i).value)
            End If
        Next

        For i = intHeaderRow + 1 To 1000
            If objWsh.Range("B" & i).Value Is Nothing Then
                Exit For
            End If
            dgrTktListing.Rows.Add()
            dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(0).Value = objWsh.Name
            For j = 1 To intMaxColumn - 1
                dgrTktListing.Rows(dgrTktListing.Rows.Count - 1).Cells(j).Value _
                    = objWsh.Cells(i, j).value
            Next
        Next
        RefreshListing()
        Return True
    End Function

    Private Sub cboCustType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustType.SelectedIndexChanged
        If cboCustType.Text <> "" Then
            LoadComboDisplay(cboCustomer, "Select l.RecID as value, CustShortName as display from CustomerList l" _
                         & " left join Cust_Detail d on l.RecID=d.CustID" _
                         & " where l.Status='ok' and d.Status='ok' and d.CAT='channel' and d.VAL='" _
                         & cboCustType.Text & "'" _
                         & " order by CustShortName", Conn)
        End If

    End Sub
End Class