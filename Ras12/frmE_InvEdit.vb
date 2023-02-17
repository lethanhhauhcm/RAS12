'^_^20230217 modi by 7643
Imports System.ComponentModel
Imports SignTokenCore.VNPTEInvoiceSignToken
Public Class frmE_InvEdit
    Private mblnFirstLoadCompleted As Boolean
    Private mblnCustLoadCompleted As Boolean
    Private mblnBizLoadCompleted As Boolean
    Private mblnAlLoadCompleted As Boolean
    Private mtblTkts As New System.Data.DataTable
    Private mstrProduct As String
    Private mstrRcp As String
    Private mstrTkIds As String
    Private mstrInvSettingTable As String = "E_invSettings"
    Private mstrInvoiceTable As String = "E_Inv"
    Private mstrInvWebTable As String = "E_InvWeb"
    Private mstrInvDetailTable As String = "E_InvDetails"
    Private mstrInvLinkTable As String = "E_InvLinks"
    Private mintAdjustType As Integer
    Private mblnNoOriInv As Boolean
    Private mstrOldInvPattern As String
    Private mstrOldInvPatternRaw As String
    Private mstrOldInvSerial As String
    Private mintOldInvNo As Integer
    Private mstrOldAl As String
    Private mdteOldInvDOI As Date
    Private mstrOriInvTable As String = "E_Inv"
    Private mstrOriInvDetailTable As String = "E_InvDetails"
    Private mstrTvc As String

    Public Sub New(Optional objCurrentInv As DataGridViewRow = Nothing _
                   , Optional intAdjustType As Integer = 0 _
                   , Optional objOldInv As DataGridViewRow = Nothing, Optional blnNoOriInv As Boolean = False _
                   , Optional strTvc As String = "", Optional blnSelectTvc As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()


        ' Add any initialization after the InitializeComponent() call.
        mstrTvc = strTvc
        txtTvc.Text = strTvc
        If pblnTT78 Then
            mstrInvoiceTable = "E_Inv78"
            mstrInvWebTable = "E_InvWeb78"
            mstrInvDetailTable = "E_InvDetails78"
            mstrInvLinkTable = "E_InvLinks78"
            mstrInvSettingTable = "E_invSettings78"
            mstrOriInvTable = "E_Inv78"
            lblGiamThue.Visible = False
            cboGiamThue.Visible = False
            cboSRV.SelectedIndex = 0
            cboSRV.Enabled = False
        End If
        mintAdjustType = intAdjustType
        mblnNoOriInv = blnNoOriInv

        If objOldInv IsNot Nothing Then
            mstrOldAl = objOldInv.Cells("AL").Value
            mstrOldInvPatternRaw = objOldInv.Cells("MauSo").Value
            mstrOldInvPattern = GetPatternShortForm(mstrOldInvPatternRaw, objOldInv.Cells("InvId").Value)
            mstrOldInvSerial = objOldInv.Cells("KyHieu").Value
            mintOldInvNo = objOldInv.Cells("InvoiceNo").Value
            If mblnNoOriInv Then
                mdteOldInvDOI = objOldInv.Cells("PublishDate").Value
                txtTaxCode.Text = objOldInv.Cells("TaxCode").Value
            Else
                mdteOldInvDOI = objOldInv.Cells("DOI").Value
                txtTaxCode.Text = objOldInv.Cells("TaxCode").Value
                txtAddress.Text = objOldInv.Cells("Address").Value
                If cboBU.Items.Count = 0 Then
                    cboBU.Items.Add(objOldInv.Cells("BU").Value)
                    cboBU.SelectedIndex = 0
                Else
                    cboBU.SelectedIndex = cboBU.FindStringExact(objOldInv.Cells("BU").Value)
                End If

                cboDomInt.SelectedIndex = cboDomInt.FindStringExact(objOldInv.Cells("DomInt").Value)
            End If

            txtOriInvNbr.Text = objOldInv.Cells("InvoiceNo").Value
            If objOldInv.Cells("InvId").Value = 0 Then
                txtOriFkey.Text = objOldInv.Cells("fkey").Value
            Else
                txtOriFkey.Text = objOldInv.Cells("InvId").Value
            End If

            txtCustId.Text = objOldInv.Cells("CustId").Value
            txtCustShortName.Text = objOldInv.Cells("CustShortName").Value
            txtCustomerFullName.Text = objOldInv.Cells("CustFullName").Value
            txtEmail.Text = objOldInv.Cells("Email").Value
            txtAddress.Text = objOldInv.Cells("Address").Value
            txtTvc.Text = objOldInv.Cells("TVC").Value
            cboFOP.SelectedIndex = cboFOP.FindStringExact(objOldInv.Cells("FOP").Value)
            pnlOriInv.Visible = True
        End If
        FormatGridSumAir()

        If objCurrentInv Is Nothing Then
            cboSRV.SelectedIndex = 0
            cboFOP.SelectedIndex = 0
            'cboDomInt.SelectedIndex = -1
            If blnSelectTvc Then SelectTvc()
        Else
            LoadInvDetails(objCurrentInv, intAdjustType, blnNoOriInv)
        End If

        RefreshGUI()
    End Sub
    Private Function RefreshGUI() As Boolean
        lbkCreateDraftE_Invoice.Visible = (mintAdjustType = 0)
        If Not mblnNoOriInv Then
            lbkPreview.Visible = True
        Else
            lbkPreview.Visible = (mintAdjustType <> 0) AndAlso MergeOldNewInvoice(mstrTvc)
        End If

        Select Case mintAdjustType
            Case 0
                Me.Text = "Tạo hóa đơn thông thường"
            Case 2
                Me.Text = "Điều chỉnh tăng tiền hóa đơn"
            Case 3
                Me.Text = "Điều chỉnh giảm tiền hóa đơn"
            Case 4
                Me.Text = "Điều chỉnh thông tin hóa đơn"
                txtBuyer.Enabled = True
                txtEmail.Enabled = True
            Case 9
                Me.Text = "Thay thế hóa đơn"
                txtBuyer.Enabled = True
                txtEmail.Enabled = True
        End Select
        If mblnNoOriInv Then
            Me.Text = Me.Text & " Không xác định hóa đơn gốc"
        End If
        If txtInvoiceNo.Text <> 0 Then
            pnlSettings.Enabled = False
            pnlInvHeader.Enabled = False
            dgrInvDetails.ReadOnly = True
        End If
        pnlOriInv.Visible = (txtOriFkey.Text <> "")

        If chkDraft.Checked Then
            lbkCreateDraftE_Invoice.Text = "ApproveDraft"
            lbkCreateDraftE_Invoice.Visible = True
            lbkCreateE_Invoice.Visible = False
        End If

        Return True
    End Function
    Private Function LoadInvDetails(objCurrentInv As DataGridViewRow, Optional intAdjustType As Integer = 0 _
                                    , Optional blnNoOriInv As Boolean = False) As Boolean
        Dim strDetailTable As String = mstrInvDetailTable
        Dim strSelect As String = "Select RecID, Tkno, Description, Unit, Qty, Price, Amount, VatPct, VAT, Total"
        Dim objOldInv As DataRow

        If pblnTT78 Then
            strSelect = strSelect & ", IsSum"
        End If
        If blnNoOriInv Then
            strDetailTable = mstrOriInvDetailTable
        End If

        LoadDataGridView(dgrInvDetails, strSelect _
            & " from " & strDetailTable & " d" _
            & " where d.InvId=" & objCurrentInv.Cells("InvId").Value, Conn)

        FormatGridSumAir()

        With objCurrentInv
            If objCurrentInv.Cells("InvoiceNo").Value <> 0 Then
                dgrInvDetails.ReadOnly = True
            End If
            txtInvId.Text = .Cells("InvId").Value
            txtRecId.Text = .Cells("RecId").Value
            txtInvoiceNo.Text = .Cells("InvoiceNo").Value
            txtTax.Text = .Cells("Tax").Value
            txtCharge.Text = .Cells("Charge").Value
            txtOriFkey.Text = .Cells("OriFkey").Value
            txtTvc.Text = .Cells("Tvc").Value
            txtBiz.Text = .Cells("Biz").Value
            txtAL.Text = .Cells("AL").Value
            txtMauSo.Text = .Cells("MauSo").Value
            txtKyHieu.Text = .Cells("KyHieu").Value
            txtCustId.Text = .Cells("CustId").Value
            txtCustShortName.Text = .Cells("CustShortName").Value
            txtTaxCode.Text = .Cells("TaxCode").Value
            txtCustomerFullName.Text = .Cells("CustFullName").Value
            txtAddress.Text = .Cells("Address").Value
            txtPeriod.Text = .Cells("Period").Value
            cboSRV.SelectedIndex = cboSRV.FindStringExact(.Cells("Srv").Value)
            cboFOP.SelectedIndex = cboFOP.FindStringExact(.Cells("Fop").Value)
            txtBuyer.Text = .Cells("Buyer").Value
            txtEmail.Text = .Cells("Email").Value
            txtBooker.Text = .Cells("Booker").Value
            txtCodeTour.Text = .Cells("CodeTour").Value
            txtNbrOfPax.Text = .Cells("NbrOfPax").Value
            cboBU.SelectedIndex = cboBU.FindStringExact(.Cells("BU").Value)
            cboDomInt.SelectedIndex = cboDomInt.FindStringExact(.Cells("DomInt").Value)
            chkDraft.Checked = .Cells("Draft").Value
            chkFakeTkno.Checked = .Cells("FakeTkno").Value
            If Not pblnTT78 Then
                cboGiamThue.SelectedIndex = cboGiamThue.FindStringExact(.Cells("GiamThue").Value)
            End If

            If .Cells("NoOriInv").Value Then
                mblnNoOriInv = True
            End If

            If txtOriFkey.Text <> "" Then
                Dim strDoiColumn As String
                pnlOriInv.Visible = True
                If pblnTT78 Then
                    txtOriInvNbr.Text = ScalarToInt("lib.dbo.E_inv78", "InvoiceNo", "InvId=" & .Cells("InvId").Value)
                Else
                    txtOriInvNbr.Text = ScalarToInt("lib.dbo.E_inv", "InvoiceNo", "InvId=" & .Cells("InvId").Value)
                End If
                If .Cells("NoOriInv").Value Then
                    mstrOriInvTable = "lib.dbo.E_InvWeb"
                    strDoiColumn = "PublishDate"
                    objOldInv = GetDataTable("select top 1 * from " & mstrOriInvTable _
                                                  & " where fkey='" & txtOriFkey.Text & "'", Conn).Rows(0)

                Else
                    mstrOriInvTable = "lib.dbo.E_Inv78"
                    strDoiColumn = "DOI"
                    objOldInv = GetDataTable("select top 1 * from " & mstrOriInvTable _
                                                  & " where InvId=" & txtOriFkey.Text, Conn).Rows(0)
                End If
                mdteOldInvDOI = objOldInv(strDoiColumn)
                mstrOldInvPatternRaw = objOldInv("MauSo")
                mstrOldInvPattern = GetPatternShortForm(mstrOldInvPatternRaw, txtOriFkey.Text)
                mstrOldInvSerial = objOldInv("KyHieu")
                mintOldInvNo = objOldInv("InvoiceNo")
                txtOriInvNbr.Text = objOldInv("InvoiceNo")
            End If


        End With
        SumGrandTotal()
        mtblTkts = GetDataTable("select TktId as RecId,RcpId from " & mstrInvLinkTable _
                                & " where InvId=" & objCurrentInv.Cells("InvId").Value, Conn)
        Return True
    End Function
    Private Sub lbkAddRow_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddRow.LinkClicked
        If Not pblnTT78 AndAlso Not IsNumeric(cboGiamThue.Text) Then
            MsgBox("You must select VAT discount level!")
            Exit Sub
        End If
        Dim tblOld As New DataTable
        If dgrInvDetails.DataSource IsNot Nothing Then
            tblOld = dgrInvDetails.DataSource
            Dim objRow As DataRow
            objRow = tblOld.NewRow
            tblOld.Rows.Add(objRow)
        Else
            dgrInvDetails.Rows.Add()
        End If

        If pnlSummary.Visible Then
            txtTax.Enabled = True
            txtCharge.Enabled = True
            txtInvTotal.Enabled = True
        End If
        dgrInvDetails.Columns("RecId").ReadOnly = True
        If mintAdjustType = 4 Then
            dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("IsSum").Value = 4
        Else
            dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("IsSum").Value = 0
        End If

    End Sub

    Private Sub lbkDeleteRow_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDeleteRow.LinkClicked

        dgrInvDetails.Rows.Remove(dgrInvDetails.CurrentRow)

    End Sub
    Private Function LoadOriInvValues(objOriInv As DataRow) As Boolean
        If objOriInv IsNot Nothing Then
            mstrOldAl = objOriInv("AL")
            mstrOldInvPatternRaw = objOriInv("MauSo")
            If objOriInv("InvId") > 40000 Then
                mstrOldInvPattern = Mid(objOriInv("MauSo"), 1, 1)
            Else
                mstrOldInvPattern = objOriInv("MauSo")
            End If

            mstrOldInvSerial = objOriInv("KyHieu")
            mdteOldInvDOI = objOriInv("DOI")
            mintOldInvNo = objOriInv("InvoiceNo")
            txtOriInvNbr.Text = objOriInv("InvoiceNo")
            txtOriFkey.Text = objOriInv("InvId")
        End If
    End Function
    Private Function FillTotal() As Boolean
        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            If objRow.Cells("Tkno").Value <> "" _
                AndAlso IsNumeric(objRow.Cells("Price").Value) Then
                If IsNumeric(objRow.Cells("Qty").Value) Then
                    objRow.Cells("Total").Value = objRow.Cells("Qty").Value * objRow.Cells("Price").Value
                Else
                    objRow.Cells("Total").Value = objRow.Cells("Price").Value
                End If
            End If
        Next
    End Function
    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerry As New List(Of String)
        Dim strQuerry As String
        Dim strFields As String
        Dim strValues As String
        Dim strInvId As String
        Dim intNewInvRecId As Integer
        Dim mintNewInvId As Integer
        Dim intVatDiscount As Integer
        Dim blnVat8 As Boolean
        Dim strOldFkey As String = ""

        txtTaxCode.Text = txtTaxCode.Text.Trim
        txtAddress.Text = Replace(txtAddress.Text, vbLf, " ")
        If Not CheckInputValues() Then Exit Sub

        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            If objRow.Cells("VatPct").Value = 8 Then
                blnVat8 = True
                Exit For
            End If
        Next
        If txtRecId.Text = 0 Or chkDraft.Checked Then
            If pblnTT78 Then
                strInvId = "(select isnull(Max(InvId),40000)+1 from lib.dbo." & mstrInvoiceTable & ")"
            Else
                strInvId = "(select isnull(Max(InvId),0)+1 from lib.dbo." & mstrInvoiceTable & ")"
            End If
            strOldFkey = txtInvId.Text
            'dtePmtDeadline = dtpDOI.Value.AddDays(ScalarToInt("KyBaocao", "top 1 DaysToPmt" _
            '                                , "Status='OK' and CustShortName='" & txtCustShortName.Text & "'"))
        Else
            strInvId = txtInvId.Text

            'dtePmtDeadline = mobjEditedRow.Cells("PmtDeadline").Value
        End If

        strFields = "insert into lib.dbo." & mstrInvoiceTable _
            & " (TVC,Biz,AL,Srv,CustId,CustShortName, InvID,  CustFullName, Address, TaxCode" _
            & ", InvoiceNo,Period, Status, FstUser, City,Tax,Charge,InvTotal,MauSo,KyHieu,Buyer,Email" _
            & ",Booker,BU,DomInt,CodeTour,NbrOfPax,FOP,OriFkey,AdjustType,Draft,FakeTkno"

        strValues = ") values ('" & txtTvc.Text & "','" & txtBiz.Text & "','" & txtAL.Text _
            & "','" & cboSRV.Text & "'," & txtCustId.Text & ",'" & txtCustShortName.Text _
            & "'," & strInvId & ",N'" & txtCustomerFullName.Text.Replace("'", "''") _
            & "',N'" & txtAddress.Text & "','" & txtTaxCode.Text & "'," & txtInvoiceNo.Text _
            & ",N'" & txtPeriod.Text & "','--','" & myStaff.SICode & "','" & myStaff.City _
            & "'," & CDec(txtTax.Text) & "," & CDec(txtCharge.Text) & "," & CDec(txtInvTotal.Text) _
            & ",'" & txtMauSo.Text & "','" & txtKyHieu.Text & "',N'" & txtBuyer.Text _
            & "','" & txtEmail.Text & "',N'" & txtBooker.Text _
            & "','" & cboBU.Text & "','" & cboDomInt.Text & "',N'" & txtCodeTour.Text & "','" & txtNbrOfPax.Text _
            & "',N'" & cboFOP.Text & "','" & txtOriFkey.Text & "'," & mintAdjustType _
            & ",'" & chkDraft.Checked & "','" & chkFakeTkno.Checked & "'"

        If pblnTT78 Then
            strFields = strFields & ",NoOriInv"
            strValues = strValues & ",'" & mblnNoOriInv.ToString & "'"
        Else
            strFields = strFields & ",GiamThue"
            strValues = strValues & "," & (100 - cboGiamThue.Text)
        End If

        strQuerry = strFields & strValues & ")"
        lstQuerry.Add(strQuerry)
        If Not UpdateListOfQuerries(lstQuerry, Conn_Web, True, intNewInvRecId) Then
            MsgBox("Unable to update VatInv record!")
            Exit Sub
        Else
            lstQuerry.Clear()
        End If

        If txtRecId.Text = 0 Or chkDraft.Checked Then
            mintNewInvId = ScalarToInt(mstrInvoiceTable, "InvId", "Recid=" & intNewInvRecId)
            txtInvId.Text = mintNewInvId
        Else
            lstQuerry.Add("delete lib.dbo." & mstrInvoiceTable & " where RecId=" & txtRecId.Text)
            lstQuerry.Add("delete lib.dbo." & mstrInvDetailTable & " where Status='OK' and InvId=" & txtInvId.Text)
            lstQuerry.Add("delete lib.dbo." & mstrInvLinkTable & " where InvId=" & txtInvId.Text)
            mintNewInvId = txtInvId.Text
        End If

        lstQuerry.Add(ChangeStatus_ByID("lib.dbo." & mstrInvoiceTable, "OK", intNewInvRecId))

        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            With objRow
                If (txtMauSo.Text.EndsWith("001") AndAlso .Cells("Description").Value <> "") _
                    Or (Not txtMauSo.Text.EndsWith("001") _
                        AndAlso (.Cells("Description").Value <> "" Or .Cells("Tkno").Value <> "")) Then
                    If pblnTT78 Then
                        lstQuerry.Add("insert into lib.dbo." & mstrInvDetailTable & " (InvID, Tkno, Description, Unit, Qty, Price, Amount, VatPct" _
                                    & ", VAT, Total, IsSum, Status, FstUser, City) values (" & mintNewInvId _
                                    & ",N'" & .Cells("Tkno").Value & "',N'" & .Cells("Description").Value & "',N'" & .Cells("Unit").Value _
                                    & "'," & CDec(.Cells("Qty").Value) & "," & CDec(.Cells("Price").Value) & "," & CDec(.Cells("Amount").Value) _
                                    & "," & CInt(.Cells("VatPct").Value) & "," & CDec(.Cells("VAT").Value) _
                                    & "," & CDec(.Cells("Total").Value) & "," & CDec(.Cells("IsSum").Value) & ",'OK','" _
                                    & myStaff.SICode & "','" & myStaff.City & "')")
                    Else
                        lstQuerry.Add("insert into lib.dbo." & mstrInvDetailTable & " (InvID, Tkno, Description, Unit, Qty, Price, Amount, VatPct" _
                                    & ", VAT, Total, Status, FstUser, City) values (" & mintNewInvId _
                                    & ",N'" & .Cells("Tkno").Value & "',N'" & .Cells("Description").Value & "',N'" & .Cells("Unit").Value _
                                    & "'," & CDec(.Cells("Qty").Value) & "," & CDec(.Cells("Price").Value) & "," & CDec(.Cells("Amount").Value) _
                                    & "," & CInt(.Cells("VatPct").Value) & "," & CDec(.Cells("VAT").Value) _
                                    & "," & CDec(.Cells("Total").Value) & ",'OK','" _
                                    & myStaff.SICode & "','" & myStaff.City & "')")
                    End If

                End If
            End With
        Next

        If Not pblnTT78 Then
            intVatDiscount = cboGiamThue.Text
        End If

        For Each objRow As DataRow In mtblTkts.Rows
            With objRow
                strFields = "Insert into lib.dbo." & mstrInvLinkTable _
                            & " (Prod, TKTID, InvId, Status, FstUser,City,RcpId,Vat8"
                strValues = " values ('" & mstrProduct & "'," & objRow("RecId") _
                              & "," & txtInvId.Text & ",'OK','" & myStaff.SICode _
                              & "','" & myStaff.City & "'," & objRow("RcpId") & ",'" & blnVat8 & "'"

                If Not pblnTT78 Then
                    strFields = strFields & ",VatDiscount"
                    strValues = strValues & "," & intVatDiscount
                End If
                strFields = strFields & ")"
                strValues = strValues & ")"

                lstQuerry.Add(strFields & strValues)
            End With
        Next

        If UpdateListOfQuerries(lstQuerry, Conn_Web) Then
            Dim dgrTemp As New DataGridView
            Dim strFieldNames As String = "Select RecId,Tkno, Description, Unit, Qty, Price, Amount, VatPct, VAT, Total"

            If pblnTT78 Then
                strFieldNames = strFieldNames & ",IsSum"
            End If

            LoadDataGridView(dgrTemp, "Select * from " & mstrInvoiceTable & " where RecId=" & intNewInvRecId, Conn)
            ' LoadInvDetails(dgrTemp.Rows(0))
            txtInvId.Text = mintNewInvId
            txtRecId.Text = intNewInvRecId
            dgrInvDetails.DataSource = Nothing
            LoadDataGridView(dgrInvDetails, strFieldNames _
                             & " from " & mstrInvDetailTable & " where InvId=" & mintNewInvId & " order by RecId", Conn)

            If chkDraft.Checked Then
                If Not DeleteDraftInv(pblnTT78, txtTvc.Text, strOldFkey) Then
                    MsgBox("Đã xóa bản ghi Draft cũ trong RAS nhưng chưa xóa được trên VNPT Web!")
                ElseIf Not CreateE_Invoice(True) Then
                    MsgBox("Đã xóa bản ghi cũ nhưng chưa tạo được bản ghi Draft mới trên VNPT Web!")
                End If
                Me.DialogResult = DialogResult.OK
            End If

        Else
            MsgBox("Unable to update E_InvDetails!")
        End If
    End Sub
    Private Function CheckInputValues() As Boolean
        'Dim blnVat8 As Boolean
        'Dim blnVatPctFound As Boolean
        'Dim i As Integer

        If txtInvoiceNo.Text > 0 Then
            MsgBox("You are NOT allowed to edit Issued E_Invoice!")
            Return False
        End If
        If Not pblnTT78 AndAlso Not IsNumeric(cboGiamThue.Text) Then
            MsgBox("You must select VAT discount level!")
            Return False
        End If
        If pblnTT78 AndAlso txtTaxCode.Text <> "" AndAlso Not CheckFormatTextBox(txtTaxCode,, , 14) Then
            MsgBox("Invalid TaxCode!")
            Return False
        End If
        If Not CheckFormatTextBox(txtTvc,, 3) Then Return False
        If Not CheckFormatTextBox(txtMauSo,, 3) Then Return False

        If Not CheckFormatTextBox(txtCustomerFullName,, 1) Then Return False
        'If Not CheckFormatTextBox(txtAddress,, 3) Then Return False
        If Not CheckFormatComboBox(cboSRV,, 1) Then Return False
        If Not CheckFormatComboBox(cboFOP,, 2) Then Return False
        If Not CheckFormatTextBox(txtInvoiceNo, True) Then
            Return False
        End If
        If Not pblnTT78 AndAlso txtInvTotal.Text < 0 Then
            MsgBox("Invoice total must be >= 0")
            Return False
        End If
        If mblnNoOriInv AndAlso txtAddress.Text = "" Then
            MsgBox("You must input Customer Address!")
            Return False
        End If
        If Not CheckFormatEmails(txtEmail.Text, True) Then
            MsgBox("Invalid email addresses!")
            Return False
        ElseIf txtEmail.TextLength > 50 Then
            MsgBox("E mail tối đa chỉ 50 ký tự!")
            txtEmail.Focus()
            Return False
        End If
        If dgrInvDetails.RowCount = 0 Then
            MsgBox("You must update Invoice Details")
            Return False
        Else
            For Each objRow As DataGridViewRow In dgrInvDetails.Rows
                If mintAdjustType = 0 Then
                    Select Case objRow.Cells("IsSum").Value
                        Case 0, 1, 2, 4
                        Case Else
                            MsgBox("Invalid value for IsSum!")
                            Return False
                    End Select
                ElseIf mintAdjustType = 4 AndAlso objRow.Cells("IsSum").Value <> 4 Then
                    MsgBox("Chỉ điều chỉnh thông tin hóa đơn cần chọn loại dòng Ghi chú!")
                    Return False
                ElseIf mintAdjustType = 2 AndAlso objRow.Cells("Total").Value < 0 Then
                    MsgBox("Điều chỉnh tăng tiền hóa đơn không dùng số tiền âm cho dòng chi tiết!")
                    Return False
                ElseIf mintAdjustType = 3 AndAlso objRow.Cells("Total").Value > 0 Then
                    MsgBox("Điều chỉnh giảm tiền hóa đơn không dùng số tiền dương cho dòng chi tiết!")
                    Return False
                End If

                If Not txtMauSo.Text.EndsWith("001") AndAlso Len(objRow.Cells("Tkno").Value) > 80 Then
                    MsgBox("Tkno must NOT be longer than 80 characters!")
                    Return False
                    'ElseIf Not blnVatPctFound AndAlso objRow.Cells("VatPct").Value >= 0 AndAlso objRow.Cells("IsSum").Value <> 4 Then
                    '    blnVatPctFound = True
                    'blnVat8 = (objRow.Cells("VatPct").Value = 8)
                ElseIf pblnTT78 AndAlso objRow.Cells("VatPct").Value <> 0 _
                    AndAlso objRow.Cells("IsSum").Value = 4 Then
                    MsgBox("Dòng Ghi chú chỉ không được nhập thuế suất khác 0%!")
                    Return False
                End If

                'If objRow.Cells("IsSum").Value <> 4 Then
                '    If Not blnVat8 AndAlso objRow.Cells("VatPct").Value = 8 Then
                '        MsgBox("VAT 8% must be issued in seperated invoice!")
                '        Return False

                '    ElseIf blnVat8 AndAlso objRow.Cells("VatPct").Value <> 8 Then
                '        MsgBox("VAT 8% must be issued in seperated invoice!")
                '        Return False
                '    End If
                'End If

            Next
        End If
        SumGrandTotal()

        If Not CheckFormatTextBox(txtInvTotal, True, 1) Then
            Return False
        End If
        If (txtTvc.Text.StartsWith("GDS") Or txtTvc.Text.StartsWith("TVTR")) _
            AndAlso txtMauSo.Text.EndsWith("1") Then
            If cboBU.Text = "" Then
                MsgBox("You must select BU for GDS/TVTR Companies!")
                Return False
            ElseIf cboDomInt.Text = "" Then
                MsgBox("You must select DomInt for GDS/TVTR Companies!")
                Return False
            ElseIf Not CheckFormatTextBox(txtNbrOfPax, True) Then
                Return False
            End If
        End If

        Select Case txtEmail.Text.Split(";").Length
            Case 2
                If txtEmail.Text.ToUpper.Contains("TRANSVIET") Then
                    MsgBox("Do NOT copy email to TransViet!")
                    Return False
                End If
            Case > 2
                MsgBox("Max 2 email addresses only!")
                Return False
        End Select

        Return True
    End Function
    Private Sub lbkCreateE_Invoice_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateE_Invoice.LinkClicked
        If txtInvoiceNo.Text <> "" AndAlso txtInvoiceNo.Text <> 0 Then
            MsgBox("E Invoice Number Exists!")
            Exit Sub
        End If
        Select Case txtEmail.Text.Split(";").Length
            Case 2
                If txtEmail.Text.ToUpper.Contains("TRANSVIET") Then
                    MsgBox("Do NOT copy email to TransViet!")
                    Exit Sub
                End If
            Case > 2
                MsgBox("Max 2 email addresses only!")
                Exit Sub
        End Select

        Select Case mintAdjustType
            Case 0
                If CreateE_Invoice(False) Then
                    Me.DialogResult = DialogResult.OK
                End If

            Case 9
                If ReplaceInvoice() Then
                    Me.DialogResult = DialogResult.OK
                End If
            Case Else
                If AdjustInvoice() Then
                    Me.DialogResult = DialogResult.OK
                End If
        End Select

    End Sub
    Private Function AdjustInvoice(Optional blnViewOnly As Boolean = False) As Boolean
        Dim objE_InvConnect As New clsE_InvConnect(pblnTT78, txtTvc.Text)
        If txtRecId.Text = 0 Then
            MsgBox("You must save Invoice details first!")
            Return False
        End If
        Dim objE_Invoice As New clsE_Invoice
        Dim lstProduct As New List(Of clsProduct)
        Dim strKindOfService As String = ""
        Dim intProdSeq As Integer
        Dim strSerialCert As String = String.Empty
        Dim decTax As Decimal = txtTax.Text
        Dim strInvToken As String = CreateInvToken(mstrOldInvPatternRaw, mstrOldInvSerial, mintOldInvNo)

        If txtTvc.Text = "123" Then
            strSerialCert = pstrSerialCert123
        End If

        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            Dim objProd As New clsProduct
            With objRow
                objProd.IsSum = .Cells("IsSum").Value
                If mintAdjustType <> 0 Then
                    If txtMauSo.Text.EndsWith("001") Then
                        objProd.ProdName = .Cells("Description").Value
                    Else
                        objProd.ProdName = .Cells("Tkno").Value
                        objProd.Extra1 = .Cells("Description").Value
                    End If
                Else
                    objProd.Extra1 = .Cells("Tkno").Value
                    objProd.ProdName = .Cells("Description").Value
                End If

                objProd.ProdUnit = .Cells("Unit").Value

                If IsNumeric(.Cells("Qty").Value) AndAlso .Cells("Qty").Value <> 0 Then
                    objProd.ProdQuantity = .Cells("Qty").Value
                End If

                If Not txtMauSo.Text.EndsWith("001") Then
                    objProd.TotalPrice = .Cells("Qty").Value * .Cells("Price").Value
                Else
                    objProd.TotalPrice = .Cells("Amount").Value
                End If

                objProd.ProdPrice = .Cells("Price").Value
                objProd.VatRate = .Cells("VatPct").Value
                objProd.VatAmount = .Cells("Vat").Value
                objProd.Amount = .Cells("Total").Value

                'Select Case .Cells("Total").Value
                '    Case > 0
                '        
                '    Case < 0
                '        objProd.DiscountAmount = Math.Abs(.Cells("Total").Value)
                '    Case 0
                '        'MsgBox("Amount must be <> 0")
                '        'Exit Sub
                'End Select

                If txtMauSo.Text.EndsWith("001") AndAlso objProd.VatRate <> -2 Then
                    'If txtMauSo.Text.EndsWith("001") AndAlso objProd.VatRate = .Cells("VatPct").Value <> -2 Then
                    intProdSeq = intProdSeq + 1
                    objProd.Seq = intProdSeq
                End If
            End With
            lstProduct.Add(objProd)
        Next

        strKindOfService = KindOfService.Hóa_đơn_GTGT

        If blnViewOnly Then
            Dim blnViewOK As Boolean = False
            If objE_Invoice.AdjustInvoiceNoPublish(objE_InvConnect.BusinessServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                        , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                        , txtCustId.Text, txtCustomerFullName.Text _
                                        , txtAddress.Text, "", txtTaxCode.Text, cboFOP.Text _
                                        , txtOriFkey.Text, strKindOfService, InvoiceType.Hóa_đơn_thông_thường, lstProduct _
                                        , txtInvId.Text, mintAdjustType, txtMauSo.Text, txtKyHieu.Text, decTax _
                                        , CDec(txtCharge.Text), txtBuyer.Text, txtEmail.Text, mstrOldInvPattern) Then
                blnViewOK = True
                'ElseIf mblnNoOriInv AndAlso objE_Invoice.AdjustInvoiceNoPublish(objE_InvConnect.BusinessServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                '                        , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                '                        , txtCustId.Text, txtCustomerFullName.Text _
                '                        , txtAddress.Text, "", txtTaxCode.Text, cboFOP.Text _
                '                        , txtOriFkey.Text, strKindOfService, InvoiceType.Hóa_đơn_thông_thường, lstProduct _
                '                        , txtInvId.Text, mintAdjustType, txtMauSo.Text, txtKyHieu.Text, decTax _
                '                        , CDec(txtCharge.Text), txtBuyer.Text, txtEmail.Text _
                '                        , mstrOldInvPattern, mstrOldInvSerial, mintOldInvNo, mdteOldInvDOI) Then
                '    blnViewOK = True

            Else
                MsgBox("Unable to view Invoice" & vbNewLine & objE_Invoice.ResponseDesc)
            End If
            If blnViewOK Then
                Dim frmShow As New frmShowHtml(objE_Invoice.ResponseDesc)
                frmShow.ShowDialog()
            End If
            Return True
        End If
        If strSerialCert = "" Then
            If mblnNoOriInv Then
                objE_Invoice.AdjustWithoutInv(objE_InvConnect.BusinessServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                            , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                            , txtCustId.Text, txtCustomerFullName.Text _
                                            , txtAddress.Text, "", txtTaxCode.Text, cboFOP.Text _
                                            , txtInvId.Text, strKindOfService, InvoiceType.Hóa_đơn_thông_thường, lstProduct _
                                            , txtOriFkey.Text, mintAdjustType _
                                            , mstrOldInvPattern, mstrOldInvSerial, mintOldInvNo, mdteOldInvDOI _
                                            , txtMauSo.Text, txtKyHieu.Text, decTax _
                                            , CDec(txtCharge.Text), txtBuyer.Text, txtEmail.Text)
            Else
                objE_Invoice.AdjustInvoiceAction(objE_InvConnect.BusinessServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                    , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                    , txtCustId.Text, txtCustomerFullName.Text _
                    , txtAddress.Text, "", txtTaxCode.Text, cboFOP.Text _
                    , txtInvId.Text, strKindOfService, InvoiceType.Hóa_đơn_thông_thường, lstProduct _
                    , txtOriFkey.Text, mintAdjustType, txtMauSo.Text, txtKyHieu.Text, decTax _
                    , CDec(txtCharge.Text), txtBuyer.Text, txtEmail.Text)
            End If
        Else
            Dim objInvToken As clsInvToken

            objInvToken = objE_Invoice.getHashInvWithToken(objE_InvConnect.WsUrl, objE_InvConnect.UserName _
                                    , objE_InvConnect.UserPass _
                                    , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                    , strSerialCert, mintAdjustType _
                                    , strInvToken, txtMauSo.Text, txtKyHieu.Text, txtCustId.Text _
                                    , txtCustomerFullName.Text, txtAddress.Text, "" _
                                    , txtTaxCode.Text, cboFOP.Text, txtInvId.Text _
                                    , strKindOfService, InvoiceType.Hóa_đơn_thông_thường _
                                    , lstProduct, txtBuyer.Text, txtEmail.Text)
            If objInvToken Is Nothing Then
                MsgBox("Không lấy được Invoice Token!")
                Return False
            End If
            If Not objE_Invoice.AdjustReplaceInvWithToken(objE_InvConnect.WsUrl _
                            , objE_InvConnect.UserName, objE_InvConnect.UserPass _
                            , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                            , strSerialCert, mstrOldInvPatternRaw, mstrOldInvSerial _
                            , mintOldInvNo, objInvToken) Then
                MsgBox(objE_Invoice.ReponseCode)
                Return False
            End If
        End If


        If objE_Invoice.ReponseCode.StartsWith("OK") Then
            'Dim arrBreaks As String() = objE_Invoice.ReponseCode.Split("-")
            Dim arrMauSoKyHieu As String() = objE_Invoice.ReponseCode.Split(";")
            Dim arrKeyNbr As String() = arrMauSoKyHieu(2).Split("_")
            Dim intInvNo As Integer = 0
            If arrKeyNbr.Length = 2 Then
                intInvNo = arrKeyNbr(1)
            End If
            Dim lstQuerries As New List(Of String)
            lstQuerries.Add("Update lib.dbo." & mstrInvoiceTable & " set MauSo='" & Mid(arrMauSoKyHieu(0), 4) _
                    & "',KyHieu='" & arrMauSoKyHieu(1) & "',InvoiceNo=" & intInvNo _
                    & ",DOI=getdate() where Recid=" & txtRecId.Text)

            'If txtOriFkey.Text <> 0 Then
            '    Dim intInvType As Integer = 1
            '    If mblnNoOriInv Then
            '        intInvType = 3
            '        lstQuerries.Add("insert into lib.dbo.E_InvNotice (InvRecId, InvFkey, TVC, MauSo, KyHieu, InvoiceNo,DOI, MaCoQuanThue" _
            '                    & ",InvType, Action,NoOriInv,FstUser,City,NewInvRecId)" _
            '                    & " select RecId, cast(invid as varchar), TVC, MauSo, KyHieu, InvoiceNo,DOI, MaCoQuanThue," _
            '                    & intInvType & ",'2-Adjust','true'," _
            '                    & myStaff.StaffId & ",'" & myStaff.City & "'," & txtRecId.Text _
            '                    & " from lib.dbo.E_inv" _
            '                    & " where InvId=" & txtOriFkey.Text)
            '    Else
            '        lstQuerries.Add("insert into lib.dbo.E_InvNotice (InvRecId, InvFkey, TVC, MauSo, KyHieu, InvoiceNo,DOI, MaCoQuanThue" _
            '                    & ",InvType, Action,FstUser,City,NewInvRecId)" _
            '                    & " select RecId, cast(invid as varchar), TVC, MauSo, KyHieu, InvoiceNo,DOI, MaCoQuanThue," & intInvType & ",'2-Adjust'," _
            '                    & myStaff.StaffId & ",'" & myStaff.City & "'," & txtRecId.Text _
            '                    & " from lib.dbo." & mstrInvoiceTable _
            '                    & " where InvId=" & txtOriFkey.Text)
            '    End If
            'End If
            MsgBox("Created E_Invoice Number:" & intInvNo)

            If UpdateListOfQuerries(lstQuerries, Conn_Web) Then
                Return True
            Else
                MsgBox("Unable to update E Invoice into RAS Database! " _
                           & vbNewLine & objE_Invoice.ResponseDesc _
                           & vbNewLine & ". Đề nghị báo người lập trình RAS!")
                Return False
            End If
        Else
            MsgBox("Unable to create E Invoice!" & vbNewLine _
                       & objE_Invoice.ConvertResponseCode2Desc("ImportAndPublishInv", objE_Invoice.ReponseCode))
            Return False
        End If


        Return True

    End Function
    Private Function CreateE_Invoice(blnDraft As Boolean) As Boolean
        'Dim strUserName As String = String.Empty
        'Dim strPass As String = String.Empty
        Dim intRefMultiplier As Integer = 1
        Dim objE_InvConnect As New clsE_InvConnect(pblnTT78, txtTvc.Text)

        Dim blnRefundHeaderExist As Boolean = False
        If txtRecId.Text = 0 Then
            MsgBox("You must save Invoice details first!")
            Return False
        End If
        Dim objE_Invoice As New clsE_Invoice
        Dim lstProduct As New List(Of clsProduct)
        Dim strKindOfService As String = ""
        Dim intProdSeq As Integer
        Dim strSerialCert As String = String.Empty
        Dim decTax As Decimal = txtTax.Text

        If cboSRV.Text = "R" Then
            intRefMultiplier = -1
        End If
        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            Dim objProd As New clsProduct
            With objRow
                If cboSRV.Text = "R" AndAlso .Cells("Tkno").Value <> "" AndAlso Not blnRefundHeaderExist Then
                    lstProduct.Add(CreateEmptyProduct("Hoàn vé ", ""))
                    blnRefundHeaderExist = True
                End If
                If pblnTT78 AndAlso Not txtMauSo.Text.EndsWith("1") Then
                    objProd.Extra1 = .Cells("Description").Value
                    objProd.ProdName = .Cells("Tkno").Value
                Else
                    objProd.Extra1 = .Cells("Tkno").Value
                    objProd.ProdName = .Cells("Description").Value
                End If

                objProd.ProdUnit = .Cells("Unit").Value

                If IsNumeric(.Cells("Qty").Value) AndAlso .Cells("Qty").Value <> 0 Then
                    objProd.ProdQuantity = .Cells("Qty").Value
                End If

                If Not txtMauSo.Text.EndsWith("001") Then
                    objProd.TotalPrice = .Cells("Qty").Value * .Cells("Price").Value * intRefMultiplier
                Else
                    objProd.TotalPrice = .Cells("Amount").Value * intRefMultiplier
                End If

                objProd.ProdPrice = .Cells("Price").Value * intRefMultiplier
                objProd.VatRate = .Cells("VatPct").Value
                objProd.VatAmount = .Cells("Vat").Value * intRefMultiplier
                objProd.Amount = .Cells("Total").Value * intRefMultiplier

                'Select Case .Cells("Total").Value '* intRefMultiplier)
                '    Case > 0
                '        objProd.Amount = .Cells("Total").Value * intRefMultiplier
                '    Case < 0
                '        objProd.DiscountAmount = Math.Abs(.Cells("Total").Value)
                '    Case 0
                '        'MsgBox("Amount must be <> 0")
                '        'Exit Sub
                'End Select

                If txtMauSo.Text.EndsWith("001") AndAlso objProd.VatRate <> -2 Then
                    'If txtMauSo.Text.EndsWith("001") AndAlso objProd.VatRate = .Cells("VatPct").Value <> -2 Then
                    intProdSeq = intProdSeq + 1
                    objProd.Seq = intProdSeq
                End If
            End With
            'Hard code vì không dùng trường này
            If objProd.DiscountAmount = 0 Then
                objProd.DiscountRate = 0
            End If

            lstProduct.Add(objProd)
        Next

        If cboSRV.Text = "S" Then
            strKindOfService = KindOfService.Hóa_đơn_GTGT
        ElseIf cboSRV.Text = "R" Then
            strKindOfService = KindOfService.Hoàn_trả_vé
        End If


        Dim intGiamThue As Integer
        If Not pblnTT78 Then
            intGiamThue = cboGiamThue.Text
        End If
        If txtTvc.Text = "123" Then
            strSerialCert = pstrSerialCert123
        End If

        If blnDraft Then
            objE_Invoice.ImportInvByPattern(objE_InvConnect.WsUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                         , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                        , txtCustId.Text, txtCustomerFullName.Text _
                                     , txtAddress.Text, "", txtTaxCode.Text, cboFOP.Text _
                                     , txtInvId.Text, strKindOfService, InvoiceType.Hóa_đơn_thông_thường, lstProduct _
                                     , txtMauSo.Text, txtKyHieu.Text, decTax _
                                     , CDec(txtCharge.Text), txtBuyer.Text, txtEmail.Text, strSerialCert, intGiamThue)


        ElseIf strSerialCert <> "" Then
            Dim objInvToken As clsInvToken

            objInvToken = objE_Invoice.getHashInvWithToken(objE_InvConnect.WsUrl, objE_InvConnect.UserName _
                                        , objE_InvConnect.UserPass _
                                        , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                        , strSerialCert, InvoiceType.Hóa_đơn_thông_thường _
                                        , "", txtMauSo.Text, txtKyHieu.Text, txtCustId.Text _
                                        , txtCustomerFullName.Text, txtAddress.Text, "" _
                                        , txtTaxCode.Text, cboFOP.Text, txtInvId.Text _
                                        , strKindOfService, InvoiceType.Hóa_đơn_thông_thường _
                                        , lstProduct, txtBuyer.Text, txtEmail.Text)
            If objInvToken Is Nothing Then
                MsgBox("Không lấy được Invoice Token!")
                Return False
            End If
            objE_Invoice.publishInvWithToken(objE_InvConnect.WsUrl, objE_InvConnect.UserName _
                            , objE_InvConnect.UserPass, objE_InvConnect.AccountName _
                            , objE_InvConnect.AccountPass, strSerialCert, objInvToken _
                            ,)
        Else

            objE_Invoice.ImportAndPublishInv(objE_InvConnect.WsUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                             , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                            , txtCustId.Text, txtCustomerFullName.Text _
                                         , txtAddress.Text, "", txtTaxCode.Text, cboFOP.Text _
                                         , txtInvId.Text, strKindOfService, InvoiceType.Hóa_đơn_thông_thường, lstProduct _
                                         , txtMauSo.Text, txtKyHieu.Text, decTax _
                                         , CDec(txtCharge.Text), txtBuyer.Text, txtEmail.Text, intGiamThue)
        End If

        If objE_Invoice.ReponseCode.StartsWith("OK") Then
            Dim arrBreaks As String() = objE_Invoice.ReponseCode.Split("-")
            Dim arrMauSoKyHieu As String() = arrBreaks(0).Split(";")
            Dim arrKeyNbr As String() = arrBreaks(1).Split("_")
            Dim intInvNo As Integer = 0
            If arrKeyNbr.Length = 2 Then
                intInvNo = arrKeyNbr(1)
            End If
            Dim strQuerry As String = "Update lib.dbo." & mstrInvoiceTable & " set MauSo='" & Mid(arrMauSoKyHieu(0), 4) _
                    & "',KyHieu='" & arrMauSoKyHieu(1) & "',InvoiceNo=" & intInvNo _
                    & ",DOI=getdate(),Draft='" & blnDraft & "' where Recid=" & txtRecId.Text

            MsgBox("Created E_Invoice Number:" & intInvNo)

            If ExecuteNonQuerry(strQuerry, Conn_Web) Then
                Return True
            Else
                MsgBox("Unable to update E Invoice into RAS Database! " _
                           & vbNewLine & objE_Invoice.ResponseDesc _
                           & vbNewLine & ". Please report NMK!")
                Return False
            End If
        Else
            MsgBox("Unable to create E Invoice!" & vbNewLine _
                       & objE_Invoice.ConvertResponseCode2Desc("ImportAndPublishInv", objE_Invoice.ReponseCode))
            Return False
        End If

        Return True
    End Function
    Private Function CreateE_InvoiceWzToken(strSerialCert As String) As Boolean
        Dim intRefMultiplier As Integer = 1
        Dim objE_InvConnect As New clsE_InvConnect(pblnTT78, txtTvc.Text)
        Dim blnRefundHeaderExist As Boolean = False
        Dim objInvToken As clsInvToken
        Dim strInvToken As String = "" ' CreateInvToken(txtMauSo.Text, txtKyHieu.Text, txtInvId.Text)
        If txtRecId.Text = 0 Then
            MsgBox("You must save Invoice details first!")
            Return False
        End If
        Dim objE_Invoice As New clsE_Invoice
        Dim lstProduct As New List(Of clsProduct)
        Dim strKindOfService As String = ""
        Dim intProdSeq As Integer

        If txtInvoiceNo.Text <> "" AndAlso txtInvoiceNo.Text <> 0 Then
            MsgBox("E Invoice Number Exists!")
        End If
        If cboSRV.Text = "R" Then
            intRefMultiplier = -1
        End If
        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            Dim objProd As New clsProduct
            With objRow
                If cboSRV.Text = "R" AndAlso .Cells("Tkno").Value <> "" AndAlso Not blnRefundHeaderExist Then
                    lstProduct.Add(CreateEmptyProduct("Hoàn vé ", " "))
                    blnRefundHeaderExist = True
                End If
                objProd.Extra1 = .Cells("Tkno").Value
                objProd.ProdName = .Cells("Description").Value
                If objProd.ProdName = "" Then
                    objProd.ProdName = " "
                End If
                objProd.ProdUnit = .Cells("Unit").Value

                If IsNumeric(.Cells("Qty").Value) AndAlso .Cells("Qty").Value <> 0 Then
                    objProd.ProdQuantity = .Cells("Qty").Value
                End If

                If txtMauSo.Text.EndsWith("001") Or strSerialCert <> "" Then
                    objProd.TotalPrice = .Cells("Amount").Value * intRefMultiplier
                Else
                    objProd.TotalPrice = .Cells("Qty").Value * .Cells("Price").Value * intRefMultiplier
                End If

                objProd.ProdPrice = .Cells("Price").Value * intRefMultiplier
                objProd.VatRate = .Cells("VatPct").Value
                objProd.VatAmount = .Cells("Vat").Value * intRefMultiplier

                Select Case .Cells("Total").Value '* intRefMultiplier)
                    Case > 0
                        objProd.Amount = .Cells("Total").Value * intRefMultiplier
                    Case < 0
                        objProd.DiscountAmount = Math.Abs(.Cells("Total").Value)
                    Case 0
                        objProd.Amount = .Cells("Amount").Value * intRefMultiplier
                End Select

                If txtMauSo.Text.EndsWith("001") AndAlso objProd.VatRate <> -2 Then
                    'If txtMauSo.Text.EndsWith("001") AndAlso objProd.VatRate = .Cells("VatPct").Value <> -2 Then
                    intProdSeq = intProdSeq + 1
                    objProd.Seq = intProdSeq
                End If
                If pblnTT78 Then
                    objProd.IsSum = .Cells("IsSum").Value
                End If
            End With
            lstProduct.Add(objProd)
        Next

        If cboSRV.Text = "S" Then
            strKindOfService = KindOfService.Hóa_đơn_GTGT
        ElseIf cboSRV.Text = "R" Then
            strKindOfService = KindOfService.Hoàn_trả_vé
        End If

        objInvToken = objE_Invoice.getHashInvWithToken(objE_InvConnect.WsUrl, objE_InvConnect.UserName _
                                    , objE_InvConnect.UserPass _
                                    , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                    , strSerialCert, InvoiceType.Hóa_đơn_thông_thường _
                                    , strInvToken, txtMauSo.Text, txtKyHieu.Text, txtCustId.Text _
                                    , txtCustomerFullName.Text, txtAddress.Text, "" _
                                    , txtTaxCode.Text, cboFOP.Text, txtInvId.Text _
                                    , strKindOfService, InvoiceType.Hóa_đơn_thông_thường _
                                    , lstProduct, txtBuyer.Text, txtEmail.Text)

        If objInvToken Is Nothing Then
            MsgBox("Không lấy được Invoice Token!")
            Return False
        End If

        If Not objE_Invoice.publishInvWithToken(objE_InvConnect.WsUrl _
                                    , objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                    , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                    , strSerialCert, objInvToken, txtMauSo.Text, txtKyHieu.Text) Then
            MsgBox(objE_Invoice.ReponseCode)
            Return False
        End If

        If objE_Invoice.ReponseCode.StartsWith("OK") Then
            Dim arrBreaks As String() = objE_Invoice.ReponseCode.Split("-")
            Dim arrMauSoKyHieu As String() = arrBreaks(0).Split(";")
            Dim arrKeyNbr As String() = arrBreaks(1).Split("_")
            Dim intInvNo As Integer = 0
            If arrKeyNbr.Length = 2 Then
                intInvNo = arrKeyNbr(1)
            End If
            Dim strQuerry As String = "Update lib.dbo." & mstrInvoiceTable & " set MauSo='" & Mid(arrMauSoKyHieu(0), 4) _
                    & "',KyHieu='" & arrMauSoKyHieu(1) & "',InvoiceNo=" & intInvNo _
                    & ",DOI=getdate(),Draft='False' where Recid=" & txtRecId.Text

            MsgBox("Created E_Invoice Number:" & intInvNo)

            If ExecuteNonQuerry(strQuerry, Conn_Web) Then
                Return True
            Else
                MsgBox("Unable to update E Invoice into RAS Database! " _
                           & vbNewLine & objE_Invoice.ResponseDesc _
                           & vbNewLine & ". Please report NMK!")
                Return False
            End If
        Else
            MsgBox("Unable to create E Invoice!" & vbNewLine _
                       & objE_Invoice.ConvertResponseCode2Desc("publishInvWithToken", objE_Invoice.ReponseCode))
            Return False
        End If

        Return True
    End Function

    Private Function CreateEmptyProduct(strExtra1 As String, strDesc As String) As clsProduct
        Dim objProd As New clsProduct
        objProd.Extra1 = strExtra1
        objProd.ProdName = strDesc
        If pblnTT78 Then
            objProd.IsSum = 4
        End If
        Return objProd

    End Function
    Private Function AutoFillTVTR(strCity As String) As Boolean
        Dim tblCust As DataTable
        Dim intCustid As Integer

        Select Case strCity
            Case "HAN"
                intCustid = 81327
            Case "SGN"
                intCustid = 5102
        End Select

        tblCust = GetDataTable("select * from CustomerList where RecId=" & intCustid)
        txtCustId.Text = tblCust.Rows(0)("RecId")
        txtCustShortName.Text = tblCust.Rows(0)("CustShortName")
        txtCustomerFullName.Text = tblCust.Rows(0)("CustFullName")
        txtAddress.Text = tblCust.Rows(0)("CustAddress")
        txtTaxCode.Text = tblCust.Rows(0)("CustTaxCode")
        'txtEmail.Text = tblCust.Rows(0)("InvoiceEmail")
        txtEmail.Text = GetInvoiceEmail4TV(strCity)
        Return True
    End Function
    Private Function LoadE_InvSettings(strRcpNo As String, strBiz As String _
                                       , Optional strTvc As String = "") As Boolean
        Dim tblE_InvSettings As New System.Data.DataTable
        Dim strQuerry As String = " select * from " & mstrInvSettingTable & " where Status='OK' and Biz='PAX' and AL='" _
                          & Mid(strRcpNo, 1, 2) & "'"
        If strTvc <> "" Then
            strQuerry = strQuerry & " and TVC='" & strTvc & "'"
        End If
        tblE_InvSettings = GetDataTable(strQuerry, Conn)

        Select Case tblE_InvSettings.Rows.Count
            Case 0
                MsgBox("Unable to find TransViet Company to issue Invoice!")
            Case 1
                txtTvc.Text = tblE_InvSettings.Rows(0)("TVC")
                txtBiz.Text = strBiz
                txtAL.Text = tblE_InvSettings.Rows(0)("AL")
                txtMauSo.Text = CreateMauSo(tblE_InvSettings.Rows(0)("MauSo"))
                txtKyHieu.Text = CreateKyHieu(tblE_InvSettings.Rows(0)("KyHieu"))
                pnlSettings.Enabled = False
                mblnFirstLoadCompleted = True
            Case Else
                Dim frmSelectTvc As New frmShowTableContent(tblE_InvSettings, "Select TVC", "TVC")
                If frmSelectTvc.ShowDialog = DialogResult.OK Then
                    With frmSelectTvc.SelectedRow
                        txtTvc.Text = .Cells("TVC").Value
                        txtBiz.Text = "PAX"
                        txtAL.Text = .Cells("AL").Value
                        txtMauSo.Text = CreateMauSo(.Cells("MauSo").Value)
                        txtKyHieu.Text = .Cells("KyHieu").Value
                    End With

                End If
                pnlSettings.Enabled = False
                mblnFirstLoadCompleted = True
        End Select
        Return True
    End Function
    Public Function LoadGridNonAir4Lloyd(dgrTktListing As DataGridView, strCustShortName As String _
                                         , intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable

        mstrProduct = "N-A"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        If myStaff.City = "HAN" Then
            LoadE_InvSettings("", "PAX", "TVTR HAN")
        ElseIf myStaff.City = "SGN" Then
            LoadE_InvSettings("", "PAX", "TVTR")
        End If
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))
        Dim strServiceDesc As String

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            With objRow
                If .Cells("S").Value Then
                    Select Case .Cells("Service").Value
                        Case "Accommodations"
                            strServiceDesc = "Tiền phòng khách sạn"
                        Case "Transfer"
                            strServiceDesc = "Thuê xe"
                        Case Else
                            strServiceDesc = .Cells("Service").Value
                    End Select

                    If .Cells("AmountNoVat").Value <> 0 Then
                        AddRow4E_Inv(strServiceDesc, "DOM", .Cells("AmountNoVat").Value, True _
                    , .Cells("Vat").Value, .Cells("VatPct").Value,,, intVatDiscount)
                        AddRow4E_Inv(.Cells("Supplier").Value, "DOM", 0, True, 0, -2,,, intVatDiscount)
                    End If

                    AddRow4E_Inv("Phí dịch vụ", "DOM", .Cells("SfNoVat_VND").Value, True _
                            , Math.Round(.Cells("VATSF").Value), .Cells("VatPct4SF").Value,,, intVatDiscount)

                    If Not IsDBNull(.Cells("BankFee").Value) AndAlso .Cells("BankFee").Value <> 0 Then
                        Dim decBankFeeNoVat As Decimal = Math.Round(.Cells("BankFee").Value * 100 / (100 + .Cells("VatPct4BankFee").Value))
                        Dim decVat4BankFee As Decimal = .Cells("BankFee").Value - decBankFeeNoVat

                        AddRow4E_Inv("Phí chuyển khoản", "DOM", decBankFeeNoVat _
                                           , True, decVat4BankFee,,,, intVatDiscount)
                    End If
                    CreateExtraInvLine4Lloyd(objRow)
                    mtblTkts.Rows.Add(.Cells("ItemId").Value, .Cells("RcpId").Value)

                    Return True
                End If
            End With

        Next

    End Function
    Public Function LoadGridTktsAir4Lloyd(dgrTktListing As DataGridView, strCustShortName As String _
                                          , intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim blnGetThisTkt As Boolean
        Dim blnGetThisTax As Boolean
        Dim blnGetThisSf As Boolean

        mstrProduct = "AIR"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        If myStaff.City = "HAN" Then
            LoadE_InvSettings("", "PAX", "TVTR HAN")
        ElseIf myStaff.City = "SGN" Then
            LoadE_InvSettings("", "PAX", "TVTR")
        End If
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False

        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Cells("S").Value Then
                Dim decFTC As Decimal
                Dim intVatPct As Integer
                Dim intVatPct4Sf As Integer
                Dim decVat As Decimal
                Dim tblEachTkt As DataTable
                Dim decSfNoVat As Decimal
                Dim decVat4Sf As Decimal
                Dim strExtraInfo As String
                Dim strDomInt As String

                With objRow
                    txtBuyer.Text = .Cells("PaxName").Value
                    Select Case .Cells("Service").Value
                        Case "AHC"
                            AddRow4E_Inv("Phí dịch vụ ngoài giờ ||" & objRow.Cells("TKNO").Value, "DOM", .Cells("SfNoVat_VND").Value, True, .Cells("Vat4SF").Value,,,, intVatDiscount)

                        Case Else
                            If .Cells("Dekho").Value.StartsWith("DOM") Then
                                strDomInt = "DOM"
                            Else
                                strDomInt = "INT"
                            End If

                            intVatPct = CalcVatPctNearest(.Cells("Fare_VND").Value, .Cells("VAT4Fare_VND").Value)
                            decVat = objRow.Cells("VAT4Fare_VND").Value
                            decFTC = .Cells("Fare_VND").Value

                            If .Cells("SRV").Value = "S" Then
                                blnGetThisTkt = GetThisTransaction(intVatDiscount, intVatPct)
                                If blnGetThisTkt Then
                                    Select Case decFTC
                                        Case 0
                                            AddRow4E_Inv("Vé máy bay" & "||" & objRow.Cells("TKNO").Value _
                                                    & "||" & objRow.Cells("Itinerary").Value, strDomInt, .Cells("Fare_VND").Value, True, -2,, "Vé", 1, intVatDiscount)
                                        Case > 0
                                            AddRow4E_Inv("Tiền vé máy bay" & "||" & objRow.Cells("TKNO").Value _
                                                    & "||" & objRow.Cells("Itinerary").Value, "DOM", .Cells("Fare_VND").Value, True, decVat,, "Vé", 1, intVatDiscount)
                                            AddRow4E_Inv("Các khoản thu hộ khác", strDomInt, .Cells("ThuHo").Value, True, .Cells("Vat4ThuHo").Value,, "Lần", 1, intVatDiscount)
                                    End Select
                                End If

                                blnGetThisTax = GetThisTransaction(intVatDiscount, intVatPct)
                                If blnGetThisTax AndAlso objRow.Cells("PhiKhac").Value > 0 Then
                                    AddRow4E_Inv("Phí khác", strDomInt, .Cells("PhiKhac").Value, True, .Cells("Vat4PhiKhac").Value, "Lần", 1, intVatDiscount)
                                End If

                                intVatPct4Sf = CalcVatPctNearest(.Cells("SfNoVat_VND").Value, .Cells("Vat4SF").Value)
                                blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                                If blnGetThisSf Then
                                    AddRow4E_Inv("Phần thu dịch vụ bán vé", "DOM", .Cells("SfNoVat_VND").Value, True, .Cells("Vat4SF").Value,, "Lần", 1, intVatDiscount)
                                End If

                            ElseIf .Cells("SRV").Value = "R" Then
                                Dim objOriInv As DataRow
                                Dim strSelection As String
                                Dim strLoadTicket As String

                                strLoadTicket = "Select t.itinerary As Rtg, (Fare - AgtDisctVAL) * ROE As Fare" _
                                & ", Tax * ROE As Tax " _
                                & ", t.Charge * ROE As Charge, t.ChargeTV * ROE As ChargeTV,InvEmail2TV, t.* " _
                                & " from tkt t left join rcp r on r.recid=t.rcpid" _
                                & " where Qty<>0 And t.Status<>'xx' and t.RecId =" & .Cells("Tkid").Value

                                tblEachTkt = GetDataTable(strLoadTicket, Conn)

                                objOriInv = GetOriginalInv(True, tblEachTkt.Rows(0)("Tkno"))

                                If objOriInv Is Nothing Then
                                    mblnNoOriInv = True
                                    objOriInv = GetOriginalInv(False, tblEachTkt.Rows(0)("Tkno"))
                                End If

                                If objOriInv Is Nothing Then
                                    MsgBox("Không tìm thấy hóa đơn bán cho vé ban đầu" & vbNewLine _
                                           & "Cần yêu cầu kế toán kiểm tra và xuất tay")
                                    Return False
                                End If
                                mintOldInvNo = objOriInv("InvoiceNo")
                                mdteOldInvDOI = objOriInv("DOI")
                                mstrOldInvPatternRaw = objOriInv("MauSo")
                                mstrOldInvPattern = objOriInv("MauSo")
                                mstrOldInvSerial = objOriInv("KyHieu")
                                txtOriFkey.Text = objOriInv("InvId")
                                cboFOP.SelectedIndex = cboFOP.FindStringExact(objOriInv("FOP"))
                                txtOriInvNbr.Text = objOriInv("InvoiceNo")

                                blnGetThisTkt = GetThisTransaction(intVatDiscount, intVatPct)

                                strSelection = UCase(InputBox("Refunded amount Or Charges? R/C", "R"))
                                Select Case strSelection
                                    Case "R"
                                        mintAdjustType = 3
                                        If blnGetThisTkt Then
                                            Select Case decFTC
                                                Case 0
                                                    AddRow4E_Inv("Tiền hoàn vé máy bay" _
                                                                 & "||" & objRow.Cells("TKNO").Value _
                                                            & "||" & objRow.Cells("Itinerary").Value _
                                                            , strDomInt, .Cells("Fare_VND").Value, True _
                                                            , -2,, "Vé", 1, intVatDiscount)
                                                Case < 0
                                                    AddRow4E_Inv("Tiền hoàn vé máy bay" _
                                                                 & "||" & objRow.Cells("TKNO").Value _
                                                            & "||" & objRow.Cells("Itinerary").Value _
                                                            , "DOM", .Cells("Fare_VND").Value, True _
                                                            , decVat,, "Vé", 1, intVatDiscount)
                                            End Select
                                        End If
                                        If .Cells("ThuHo").Value < 0 Then
                                            AddRow4E_Inv("Tiền hoàn các khoản thu hộ khác", strDomInt _
                                                         , .Cells("ThuHo").Value, True _
                                                         , .Cells("Vat4ThuHo").Value,, "Lần", 1, intVatDiscount)
                                        End If


                                    Case "C"
                                        mintAdjustType = 2
                                        If .Cells("PhiKhac").Value > 0 Then
                                            AddRow4E_Inv("Phí hoàn vé máy bay" _
                                                     & "||" & objRow.Cells("TKNO").Value _
                                                & "||" & objRow.Cells("Itinerary").Value _
                                                , "DOM", .Cells("PhiKhac").Value, True _
                                                , 0, -1, "Lần", 1, intVatDiscount)
                                        End If
                                        If .Cells("SfNoVat_VND").Value > 0 Then
                                            AddRow4E_Inv("Phần thu dịch vụ hoàn vé" _
                                                , "DOM", .Cells("SfNoVat_VND").Value, True _
                                                , .Cells("Vat4SF").Value,, "Lần", 1, intVatDiscount)
                                        End If

                                    Case Else
                                        Return False
                                End Select
                            End If

                            cboSRV.SelectedIndex = 0
                            cboSRV.Enabled = False

                    End Select


            CreateExtraInvLine4Lloyd(objRow)

                End With


                Dim strGetTktRecord As String
                'If .c.ToUpper = "HOTLINE" Then
                '    strGetTktRecord = "Select top 1 RecId,RcpId,Tkno from tkt where Rcpno='" _
                '                          & objRow.Cells("Transaction Code").Value _
                '                          & "' and SRV='S' order by RecId desc"
                'Else

                strGetTktRecord = "Select top 1 RecId,RcpId,Tkno from tkt where Tkno='" _
                                          & objRow.Cells("TKNO").Value _
                                          & "' and SRV='" & objRow.Cells("SRV").Value _
                                          & "' and CONVERT(date,DOI) ='" & CreateFromDate(objRow.Cells("DOI").Value) _
                                          & "' order by RecId desc"
                'End If
                tblEachTkt = GetDataTable(strGetTktRecord, Conn)
                If tblEachTkt.Rows.Count = 0 Then
                    MsgBox("unable to find " & objRow.Cells("TKNO").Value & " in RAS")
                    Me.Dispose()
                    Return False
                End If
                mtblTkts.Rows.Add(tblEachTkt.Rows(0)("RecId"), tblEachTkt.Rows(0)("RcpId"))
            End If
        Next

        'xac dinh Inv total => SRV
        Dim decInvTotal As Decimal
        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            decInvTotal = decInvTotal + objRow.Cells("Total").Value
        Next
        'If decInvTotal < 0 Then
        '    cboSRV.SelectedIndex = 1
        '    cboSRV.Enabled = False
        '    For Each objRow As DataGridViewRow In dgrInvDetails.Rows
        '        objRow.Cells("Amount").Value = 0 - objRow.Cells("Amount").Value
        '        objRow.Cells("VAT").Value = 0 - objRow.Cells("VAT").Value
        '        objRow.Cells("Total").Value = 0 - objRow.Cells("Total").Value
        '    Next
        'End If
        SumGrandTotal()
        RefreshGUI()
    End Function
    Private Function CreateExtraInvLine4Lloyd(objRow As DataGridViewRow)
        With objRow
            AddRow4E_Inv("One World Number:" & .Cells("OneWorldNumber").Value, "DOM", 0, True, 0, 0,,,, 4)
            If .Cells("ProjectTaskId").Value <> "" AndAlso .Cells("ProjectTaskId").Value <> "N/A" Then
                AddRow4E_Inv("Cost Center:N/A", "DOM", 0, True, 0, 0,,,, 4)
            Else
                AddRow4E_Inv("Cost Center:" & .Cells("CostCenter").Value, "DOM", 0, True, 0, 0,,,, 4)
            End If

            AddRow4E_Inv("Approver:" & .Cells("Approver").Value, "DOM", 0, True, 0, 0,,,, 4)

            If .Cells("ProjectTaskId").Value <> "" AndAlso .Cells("ProjectTaskId").Value <> "N/A" Then
                AddRow4E_Inv("ProjectTaskId/ServiceOrderID:" & .Cells("ProjectTaskId").Value, "DOM", 0, True, 0, 0,,,, 4)
            End If

            If .Cells("MastNumber").Value <> "" AndAlso .Cells("MastNumber").Value <> "N/A" Then
                AddRow4E_Inv("Mast Number:" & .Cells("MastNumber").Value, "DOM", 0, True, 0, 0,,,, 4)
            End If

            If .Cells("VesselName").Value <> "" AndAlso .Cells("VesselName").Value <> "N/A" Then
                AddRow4E_Inv("Vessel Name:" & .Cells("VesselName").Value, "DOM", 0, True, 0, 0,,,, 4)
            End If
        End With

        Return True
    End Function
    Public Function LoadRcp(strRcpNo As String, blnIssued2TV As Boolean) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim tblRcp As System.Data.DataTable

        Dim decTax As Decimal
        Dim decCharge As Decimal
        Dim decInvTotal As Decimal
        Dim intRefundMultiplier As Integer = 1

        Dim strLoadTkt As String

        pnlSelectCustomer.Visible = False

        tblRcp = GetDataTable("Select top 1 * from Rcp where Status ='OK' And RcpNo='" & strRcpNo & "'")

        If tblRcp.Rows.Count = 0 Then
            MsgBox("You must SAVE transaction before Create E_Invoice")
            Me.Dispose()
        End If

        tblCust = GetDataTable("Select top 1 * from CustomerList where RecId=" & tblRcp.Rows(0)("CustId"))
        LoadCustomer(tblCust.Rows(0))

        cboSRV.SelectedIndex = cboSRV.FindStringExact(tblRcp.Rows(0)("SRV"))

        If strRcpNo.StartsWith("TS") Then
            If tblCust.Rows(0)("CustShortName") = "WKI" Then
                LoadCustomerFromRcp(tblRcp.Rows(0))
            End If
            If myStaff.City = "HAN" Then
                LoadE_InvSettings(strRcpNo, "PAX", "TVTR HAN")
            ElseIf myStaff.City = "SGN" Then
                LoadE_InvSettings(strRcpNo, "PAX", "TVTR")
            End If
        Else
            LoadE_InvSettings(strRcpNo, "PAX")
        End If

        cboFOP.SelectedIndex = 0

        If blnIssued2TV Then
            AutoFillTVTR(myStaff.City)
        End If

        mstrProduct = "AIR"
        pnlSummary.Visible = True

        If tblRcp.Rows(0)("SRV") = "R" Then
            intRefundMultiplier = -1
        End If

        strLoadTkt = "Select itinerary As Rtg, (Fare - AgtDisctVAL) * ROE As Fare, Tax * ROE As Tax " _
            & ", t.Charge * ROE As Charge, t.ChargeTV * ROE As ChargeTV, t.* " _
            & " from tkt t left join rcp r on r.recid=t.rcpid" _
            & " where Qty<>0 and t.Status<>'xx' and t.Rcpno='" & strRcpNo & "'"

        mtblTkts = GetDataTable(strLoadTkt, Conn)
        For Each objRow As DataRow In mtblTkts.Rows
            With objRow
                Dim strDesc As String = ""

                AddRow4E_InvBSP(Replace(objRow("Tkno"), " ", ""), Replace(objRow("Rtg"), " ", ""), objRow("Fare"))
                decTax = decTax + objRow("Tax")
                decCharge = decCharge + objRow("Charge")

                'If blnIssued2TV Then
                decCharge = decCharge + objRow("ChargeTV")
                'End If

                decInvTotal = decInvTotal + objRow("Fare")

                FormatGridSumAir()

                dgrInvDetails.ReadOnly = True
            End With

        Next
        txtTax.Text = Format(decTax, "#,##0")
        txtCharge.Text = Format(decCharge, "#,##0")
        txtInvTotal.Text = Format(decInvTotal + decTax + (decCharge * intRefundMultiplier), "#,##0")

    End Function
    Public Function LoadRcp78(strRcpNo As String, blnIssued2TV As Boolean) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim tblRcp As System.Data.DataTable

        Dim decTax As Decimal
        Dim decCharge As Decimal
        Dim decInvTotal As Decimal
        Dim intRefundMultiplier As Integer = 1
        Dim strLoadTkt As String
        Dim objOriInv As DataRow
        Dim tblOldInv As  DataTable
        pnlSelectCustomer.Visible = False

        tblRcp = GetDataTable("Select top 1 * from Rcp where Status ='OK' And RcpNo='" & strRcpNo & "'")

        If tblRcp.Rows.Count = 0 Then
            MsgBox("You must SAVE transaction before Create E_Invoice")
            Me.Dispose()
        End If

        tblCust = GetDataTable("Select top 1 * from CustomerList where RecId=" & tblRcp.Rows(0)("CustId"))
        LoadCustomer(tblCust.Rows(0))

        cboSRV.SelectedIndex = cboSRV.FindStringExact(tblRcp.Rows(0)("SRV"))

        If strRcpNo.StartsWith("TS") Then
            If tblRcp.Rows(0)("CustType") = "WK" Then
                LoadCustomerFromRcp(tblRcp.Rows(0))
            End If
            If myStaff.City = "HAN" Then
                LoadE_InvSettings(strRcpNo, "PAX", "TVTR HAN")
            ElseIf myStaff.City = "SGN" Then
                LoadE_InvSettings(strRcpNo, "PAX", "TVTR")
            End If
        Else
            LoadE_InvSettings(strRcpNo, "PAX")
        End If

        cboFOP.SelectedIndex = 0

        If blnIssued2TV Then
            AutoFillTVTR(myStaff.City)
            chkIssue2TV.Enabled = False
        End If

        mstrProduct = "AIR"
        pnlSummary.Visible = True
        If tblRcp.Rows(0)("SRV") = "R" Then
            intRefundMultiplier = -1
        End If

        strLoadTkt = "Select itinerary As Rtg, (Fare - AgtDisctVAL) * ROE As Fare, Tax * ROE As Tax " _
            & ", t.Charge * ROE As Charge, t.ChargeTV * ROE As ChargeTV,InvEmail2TV, t.* " _
            & " from tkt t left join rcp r on r.recid=t.rcpid" _
            & " where Qty<>0 and t.Status<>'xx' and t.Rcpno='" & strRcpNo & "'"

        mtblTkts = GetDataTable(strLoadTkt, Conn)
        For Each objRow As DataRow In mtblTkts.Rows
            With objRow
                Dim decTktTotal As Decimal = objRow("Fare") * intRefundMultiplier + objRow("Tax") * intRefundMultiplier + objRow("Charge") + objRow("ChargeTV")
                AddRow4E_InvBSP(Replace(objRow("Tkno"), " ", ""), Replace(objRow("Rtg"), " ", ""), decTktTotal)

                decInvTotal = decInvTotal + decTktTotal

                FormatGridSumAir()

                dgrInvDetails.ReadOnly = True
            End With
        Next
        FillCustInfoInvEmail2TV()

        txtTax.Text = Format(decTax, "#,##0")
        txtCharge.Text = Format(decCharge, "#,##0")
        txtInvTotal.Text = Format(decInvTotal + decTax + (decCharge * intRefundMultiplier), "#,##0")

        If tblRcp.Rows(0)("SRV") = "R" Then

            mintAdjustType = 3
            objOriInv = GetOriginalInv(True, mtblTkts.Rows(0)("Tkno"))

            If objOriInv Is Nothing Then
                mblnNoOriInv = True
                objOriInv = GetOriginalInv(False, mtblTkts.Rows(0)("Tkno"))
            End If

            If objOriInv Is Nothing Then
                MsgBox("Không tìm thấy hóa đơn bán cho vé ban đầu. Cần yêu cầu kế toán kiểm tra và xuất tay")
                Return False
            Else
                Dim blnGetOldCustInfo As Boolean
                mintAdjustType = 3
                mintOldInvNo = objOriInv("InvoiceNo")
                mdteOldInvDOI = objOriInv("DOI")
                mstrOldInvPattern = objOriInv("MauSo")
                mstrOldInvSerial = objOriInv("KyHieu")
                txtOriFkey.Text = objOriInv("InvId")
                cboFOP.SelectedIndex = cboFOP.FindStringExact(objOriInv("FOP"))
                txtOriInvNbr.Text = objOriInv("InvoiceNo")

                If tblRcp.Rows(0)("Counter") = "GSA" Then
                    blnGetOldCustInfo = True
                ElseIf tblRcp.Rows(0)("Counter") = "TVS" AndAlso DefineBSPStock Then
                    blnGetOldCustInfo = True
                End If
                If blnGetOldCustInfo Then
                    txtCustId.Text = objOriInv("CustId")
                    txtCustomerFullName.Text = objOriInv("CustFullName")
                    txtTaxCode.Text = objOriInv("TaxCode")
                    txtBuyer.Text = objOriInv("Buyer")
                    txtAddress.Text = objOriInv("Address")
                    txtEmail.Text = objOriInv("Email")
                    'txtCustId.Enabled = False
                    'txtCustomerFullName.Enabled = False
                    'txtTaxCode.Enabled = False
                    'txtBuyer.Enabled = False
                    'txtAddress.Enabled = False
                    'txtEmail.Enabled=False
                End If
            End If
        ElseIf mtblTkts.Rows(0)("StockCtrl") <> "" Then
            mintAdjustType = 2
            Dim tblOldTkts As DataTable
            tblOldTkts = GetDataTable("select top 1 RcpId,RecId,Tkno,StockCtrl from Tkt where SRV='S' and replace(Tkno,' ','')='" _
                                                     & Replace(mtblTkts.Rows(0)("StockCtrl"), " ", "") _
                                                     & "' order by RecId desc")

            If tblOldTkts.Rows.Count = 0 Then
                MsgBox("Không tìm thấy bản ghi vé đổi trong RAS. Đề nghị quầy kiểm tra việc nhập số vé đổi!")
                Return False
            End If
            tblOldInv = GetDataTable("select top 1 * from lib.dbo.E_invDetails78 d" _
                                     & " left join lib.dbo.E_inv78 i on d.InvId=i.InvId" _
                                     & " where i.InvoiceNo<>0 and i.FakeTkno='False' and i.OriFkey='' And Tkno='" _
                                     & Replace(tblOldTkts.Rows(0)("Tkno"), " ", "") & "'", Conn)
            If tblOldInv.Rows.Count = 0 Then
                tblOldInv = GetDataTable("select top 1 i.* from lib.dbo.E_invDetails d" _
                                     & " left join lib.dbo.E_inv i on d.InvId=i.InvId" _
                                     & " where i.Srv='S' And replace(Tkno,' ','')='" _
                                     & Replace(mtblTkts.Rows(0)("StockCtrl"), " ", "") & "'", Conn)
                mblnNoOriInv = True
            End If
            If tblOldInv.Rows.Count = 0 Then
                MsgBox("Không tìm thấy hóa đơn vé bán bị đổi. Cần yêu cầu kế toán kiểm tra và xuất tay")
                Return False
            Else
                mintAdjustType = 2
                mintOldInvNo = tblOldInv.Rows(0)("InvoiceNo")
                mdteOldInvDOI = tblOldInv.Rows(0)("DOI")
                mstrOldInvPattern = tblOldInv.Rows(0)("MauSo")
                mstrOldInvSerial = tblOldInv.Rows(0)("KyHieu")
                txtOriFkey.Text = tblOldInv.Rows(0)("InvId")
                cboFOP.SelectedIndex = cboFOP.FindStringExact(tblOldInv.Rows(0)("FOP"))
                txtOriInvNbr.Text = tblOldInv.Rows(0)("InvoiceNo")
            End If
        End If

        RefreshGUI()
        Return True
    End Function
    Private Function DefineBSPStock() As Boolean
        Dim VeI As String
        Dim blnResult As Boolean

        For i As Int16 = 0 To mtblTkts.Rows.Count - 1
            VeI = Mid(mtblTkts.Rows(i)("TKNO"), 5, 4)
            If ScalarToInt("lib.dbo.MISC", "count(*)", "Status='OK' and cat='BSPSTOCK' and val='" & VeI & "'") Then
                Return True
            End If
        Next
        Return blnResult
    End Function
    Public Function LoadDetailsBsp(strRcpNo As String, strTkIds As String _
                                   , blnIssued2TV As Boolean) As Boolean
        Dim tblRcp As System.Data.DataTable
        Dim tblCust As System.Data.DataTable

        Dim decTax As Decimal
        Dim decTotalCharge As Decimal
        Dim decCharge As Decimal
        Dim decChargeTV As Decimal
        Dim decInvTotal As Decimal
        Dim intRefundMultiplier As Integer = 1
        Dim strLoadTkt As String
        Dim tblE_InvSettings As New System.Data.DataTable

        dgrInvDetails.Rows.Clear()
        mstrRcp = strRcpNo
        mstrTkIds = strTkIds
        pnlSelectCustomer.Visible = False
        tblRcp = GetDataTable("Select top 1 * from Rcp where Status='OK'" _
                                                            & " And RcpNo='" & strRcpNo & "'")
        tblCust = GetDataTable("Select top 1 * from CustomerList where RecId=" & tblRcp.Rows(0)("CustId"))
        LoadCustomer(tblCust.Rows(0))

        cboSRV.SelectedIndex = cboSRV.FindStringExact(tblRcp.Rows(0)("SRV"))

        'cboMauSo.SelectedIndex = cboMauSo.FindStringExact("01GTKT0/002")
        'cboKyHieu.SelectedIndex = cboKyHieu.FindStringExact("AB/" & Format(Now, "yy") & "E")

        cboSRV.Enabled = False
        cboFOP.SelectedIndex = 0

        If blnIssued2TV AndAlso myStaff.City = "SGN" Then
            AutoFillTVTR(myStaff.City)
        End If
        LoadE_InvSettings(strRcpNo, "PAX", mstrTvc)

        mstrProduct = "AIR"
        pnlSummary.Visible = True

        If tblRcp.Rows(0)("SRV") = "R" Then
            intRefundMultiplier = -1
        End If


        strLoadTkt = DefineLoadTkt(strRcpNo, blnIssued2TV)

        'strLoadTkt = "Select itinerary as Rtg, * from tkt where Qty<>0 and Status<>'xx' and Rcpno='" _
        '                           & strRcpNo & "'"
        If strTkIds <> "" Then
            strLoadTkt = strLoadTkt & " and t.RecId in (" & strTkIds & ")"
        End If
        mtblTkts = GetDataTable(strLoadTkt, Conn)

        For Each objRow As DataRow In mtblTkts.Rows
            Dim strDesc As String = ""
            AddRow4E_InvBSP(Replace(objRow("Tkno"), " ", ""), Replace(objRow("Rtg"), " ", ""), objRow("Fare"))
            decTax = decTax + objRow("Tax")
            decCharge = decCharge + objRow("Charge")
            decChargeTV = decChargeTV + objRow("ChargeTV")
            decInvTotal = decInvTotal + objRow("Fare")

            FormatGridSumAir()
            HideInvDetailsColumns(False)
            dgrInvDetails.ReadOnly = True
        Next
        decTotalCharge = decCharge
        If Not blnIssued2TV Then
            decTotalCharge = decTotalCharge + decChargeTV
        End If

        txtTax.Text = Format(decTax, "#,##0")
        txtCharge.Text = Format(decTotalCharge, "#,##0")
        txtChargeTV.Text = Format(decChargeTV, "#,##0")
        txtInvTotal.Text = Format(decInvTotal + decTax + (decTotalCharge * intRefundMultiplier), "#,##0")

    End Function
    Public Function LoadDetailsBsp78(strRcpNo As String, strTkIds As String _
                                   , blnIssued2TV As Boolean, Optional blnExchange As Boolean = False) As Boolean
        Dim tblRcp As System.Data.DataTable
        Dim tblCust As System.Data.DataTable

        'Dim decTax As Decimal        
        Dim decInvTotal As Decimal
        Dim intRefundMultiplier As Integer = 1
        Dim strLoadTkt As String
        Dim tblE_InvSettings As New System.Data.DataTable
        Dim decTktTotal As Decimal
        'Dim strFindOldInv As String=""
        Dim tblOldInv As DataTable
        Dim objOriInv As DataRow

        dgrInvDetails.Rows.Clear()
        mstrRcp = strRcpNo
        mstrTkIds = strTkIds
        pnlSelectCustomer.Visible = False
        tblRcp = GetDataTable("Select top 1 * from Rcp where Status='OK'" _
                                                            & " And RcpNo='" & strRcpNo & "'")
        tblCust = GetDataTable("Select top 1 * from CustomerList where RecId=" & tblRcp.Rows(0)("CustId"))
        LoadCustomer(tblCust.Rows(0))

        'cboMauSo.SelectedIndex = cboMauSo.FindStringExact("01GTKT0/002")
        'cboKyHieu.SelectedIndex = cboKyHieu.FindStringExact("AB/" & Format(Now, "yy") & "E")

        cboSRV.Enabled = False
        cboFOP.SelectedIndex = 0

        If blnIssued2TV Then
            AutoFillTVTR(myStaff.City)
        End If
        LoadE_InvSettings(strRcpNo, "PAX", mstrTvc)

        mstrProduct = "AIR"
        pnlSummary.Visible = True

        strLoadTkt = DefineLoadTkt(strRcpNo, blnIssued2TV)
        If strTkIds <> "" Then
            strLoadTkt = strLoadTkt & " and t.RecId in (" & strTkIds & ") order by t.RecId"
        End If
        mtblTkts = GetDataTable(strLoadTkt, Conn)

        FillCustInfoInvEmail2TV()

        If tblRcp.Rows(0)("SRV") = "R" Then
            intRefundMultiplier = -1
            objOriInv = GetOriginalInv(True, mtblTkts.Rows(0)("Tkno"))

            If objOriInv Is Nothing Then
                mblnNoOriInv = True
                objOriInv = GetOriginalInv(False, mtblTkts.Rows(0)("Tkno"))
            End If

            If objOriInv Is Nothing Then
                MsgBox("Không tìm thấy hóa đơn bán để điều chỉnh")
                Return False
            Else
                mintAdjustType = 3
                mintOldInvNo = objOriInv("InvoiceNo")
                mdteOldInvDOI = objOriInv("DOI")
                mstrOldInvPattern = objOriInv("MauSo")
                mstrOldInvSerial = objOriInv("KyHieu")
                txtOriFkey.Text = objOriInv("InvId")
                cboFOP.SelectedIndex = cboFOP.FindStringExact(objOriInv("FOP"))
                txtOriInvNbr.Text = objOriInv("InvoiceNo")

            End If
        ElseIf blnExchange Then
            Dim tblOldTkts As DataTable
            tblOldTkts = GetDataTable("select top 1 RcpId,RecId,Tkno,StockCtrl from Tkt where SRV='S' and replace(Tkno,' ','')='" _
                                                     & Replace(mtblTkts.Rows(0)("StockCtrl"), " ", "") _
                                                     & "' order by RecId desc")

            If tblOldTkts.Rows.Count = 0 Then
                MsgBox("Không tìm thấy bản ghi vé đổi trong RAS. Đề nghị quầy kiểm tra việc nhập số vé đổi!")
                Return False
            End If
            tblOldInv = GetDataTable("select top 1 * from lib.dbo.E_invDetails78 d" _
                                     & " left join lib.dbo.E_inv78 i on d.InvId=i.InvId" _
                                     & " where i.InvoiceNo<>0 and i.FakeTkno='False' and i.OriFkey='' And Tkno='" _
                                     & Replace(tblOldTkts.Rows(0)("Tkno"), " ", "") & "'", Conn)
            If tblOldInv.Rows.Count = 0 Then
                tblOldInv = GetDataTable("select top 1 i.* from lib.dbo.E_invDetails d" _
                                     & " left join lib.dbo.E_inv i on d.InvId=i.InvId" _
                                     & " where i.Srv='S' And replace(Tkno,' ','')='" _
                                     & Replace(mtblTkts.Rows(0)("StockCtrl"), " ", "") & "'", Conn)
                mblnNoOriInv = True
            End If
            If tblOldInv.Rows.Count = 0 Then
                MsgBox("Không tìm thấy hóa đơn vé bán bị đổi. Cần yêu cầu kế toán kiểm tra và xuất tay")
                Return False
            Else
                mintAdjustType = 2
                mintOldInvNo = tblOldInv.Rows(0)("InvoiceNo")
                mdteOldInvDOI = tblOldInv.Rows(0)("DOI")
                mstrOldInvPattern = tblOldInv.Rows(0)("MauSo")
                mstrOldInvSerial = tblOldInv.Rows(0)("KyHieu")
                txtOriFkey.Text = tblOldInv.Rows(0)("InvId")
                cboFOP.SelectedIndex = cboFOP.FindStringExact(tblOldInv.Rows(0)("FOP"))
                txtOriInvNbr.Text = tblOldInv.Rows(0)("InvoiceNo")
            End If
        End If


        For Each objRow As DataRow In mtblTkts.Rows
            Dim strDesc As String = Replace(objRow("Tkno"), " ", "")
            If tblRcp.Rows(0)("SRV") = "R" Then
                strDesc = "Hoàn vé " & strDesc
            End If

            If blnIssued2TV Then
                decTktTotal = (objRow("Fare") + objRow("Tax")) * intRefundMultiplier + objRow("Charge")
            Else
                decTktTotal = (objRow("Fare") + objRow("Tax")) * intRefundMultiplier + objRow("Charge") + objRow("ChargeTV")
            End If
            AddRow4E_InvBSP(strDesc, Replace(objRow("Rtg"), " ", ""), decTktTotal)
            decInvTotal = decInvTotal + decTktTotal

        Next

        FormatGridSumAir()
        HideInvDetailsColumns(False)
        dgrInvDetails.ReadOnly = True

        'txtTax.Text = Format(decTax, "#,##0")
        'txtCharge.Text = Format(decTotalCharge, "#,##0")
        'txtChargeTV.Text = Format(decChargeTV, "#,##0")
        txtInvTotal.Text = Format(decInvTotal, "#,##0")
        RefreshGUI()
    End Function

    Public Function LoadDetailsCombinedInv(intCustId As Integer,
                                           strTkIds As String) As Boolean
        Dim tblRcp As System.Data.DataTable
        Dim tblCust As System.Data.DataTable

        'Dim decTax As Decimal        
        Dim decInvTotal As Decimal
        Dim intRefundMultiplier As Integer = 1
        Dim strLoadTkt As String
        Dim tblE_InvSettings As New System.Data.DataTable
        Dim decTktTotal As Decimal
        'Dim strFindOldInv As String=""
        Dim tblOldInv As DataTable
        Dim objOriInv As DataRow

        dgrInvDetails.Rows.Clear()
        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where RecId=" & intCustId)
        LoadCustomer(tblCust.Rows(0))

        LoadE_InvSettings("TS", "PAX", mstrTvc)

        mstrProduct = "AIR"
        pnlSummary.Visible = True

        strLoadTkt = "Select itinerary as Rtg,(Fare-AgtDisctVAL)*ROE as Fare,Tax*ROE as Tax " _
            & ",t.Charge*ROE as Charge,t.ChargeTV*ROE as ChargeTV , r.InvEmail2TV,t.*" _
            & " from tkt t left join rcp r on r.recid=t.rcpid" _
            & " where Qty<>0 and t.Status<>'xx'" _
            & " And t.RecId in (" & strTkIds & ")"

        mtblTkts = GetDataTable(strLoadTkt, Conn)

        FillCustInfoInvEmail2TV()

        For Each objRow As DataRow In mtblTkts.Rows
            'Dim strDesc As String = Replace(objRow("Tkno"), " ", "")

            decTktTotal = (objRow("Fare") + objRow("Tax")) * intRefundMultiplier + objRow("Charge") + objRow("ChargeTV")
            AddRow4E_InvBSP(objRow("Tkno"), Replace(objRow("Rtg"), " ", ""), decTktTotal)
            decInvTotal = decInvTotal + decTktTotal
        Next

        FormatGridSumAir()
        HideInvDetailsColumns(False)
        dgrInvDetails.ReadOnly = True

        'txtTax.Text = Format(decTax, "#,##0")
        'txtCharge.Text = Format(decTotalCharge, "#,##0")
        'txtChargeTV.Text = Format(decChargeTV, "#,##0")
        txtInvTotal.Text = Format(decInvTotal, "#,##0")
        RefreshGUI()
    End Function

    Private Function DefineLoadTkt(strRcpNo As String, blnIssue2TV As Boolean) As String
        Dim strLoadTkt As String
        If blnIssue2TV Then
            strLoadTkt = "Select itinerary as Rtg,(NetToAL)*ROE as Fare,Tax*ROE as Tax " _
            & ",t.Charge*Roe as Charge,t.ChargeTV*ROE as ChargeTV, r.InvEmail2TV,t.* " _
            & " from tkt t left join rcp r on r.recid=t.rcpid" _
            & " where Qty<>0 and t.Status<>'xx' and t.Rcpno='" & strRcpNo & "'"
        Else
            strLoadTkt = "Select itinerary as Rtg,(Fare-AgtDisctVAL)*ROE as Fare,Tax*ROE as Tax " _
            & ",t.Charge*ROE as Charge,t.ChargeTV*ROE as ChargeTV , r.InvEmail2TV,t.*" _
            & " from tkt t left join rcp r on r.recid=t.rcpid" _
            & " where Qty<>0 and t.Status<>'xx' and t.Rcpno='" & strRcpNo & "'"
        End If
        Return strLoadTkt
    End Function

    Public Function LoadGridNonAir(dgrTktListing As DataGridView, strCustShortName As String _
                                   , strTemplate As String _
                                   , intVatDiscount As Integer) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim decSfNoVat As Decimal
        Dim decVat4Sf As Decimal
        'Dim tblEachTkt As DataTable
        Dim intRcpId As Integer
        Dim mDutoanID, i As Integer  '^_^20220808 add by 7643

        mstrProduct = "N-A"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        If myStaff.City = "HAN" Then
            LoadE_InvSettings("", "PAX", "TVTR HAN")
        ElseIf myStaff.City = "SGN" Then
            LoadE_InvSettings("", "PAX", "TVTR")
        End If
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False
        Select Case strTemplate
            Case ""
                DraftNonAirGeneric(dgrTktListing, intVatDiscount)
            Case "INV_NONAIR1"
                DraftNonAirDetail1(dgrTktListing)
            Case "INV_NONAIR2"
                DraftNonAirDetail12(dgrTktListing, intVatDiscount)
        End Select

        '^_^20220808 add by 7643 -b-
        If strCustShortName = "ALFA LAVAL" Then
            For i = dgrTktListing.Rows.Count - 1 To 0 Step -1
                If CInt(dgrTktListing.Rows(i).Cells("DutoanID").Value) > 0 Then
                    mDutoanID = CInt(dgrTktListing.Rows(i).Cells("DutoanID").Value)
                    Exit For
                End If
            Next

            If mDutoanID > 0 Then AddRow4E_Inv("TRIP PURPOSE:" & GetNonAirExtRowDetail(mDutoanID, "TRIP PURPOSE"), "DOM", 0, True, 0, 0,,,, 4)
            '^_^20220831 add by 7643 -b-
            '^_^20221006 mark by 7643 -b-
            'ElseIf strCustShortName = "COCA COLA" Then
            '    For i = dgrTktListing.Rows.Count - 1 To 0 Step -1
            '        If CInt(dgrTktListing.Rows(i).Cells("DutoanID").Value) > 0 Then
            '            mDutoanID = CInt(dgrTktListing.Rows(i).Cells("DutoanID").Value)
            '            Exit For
            '        End If
            '    Next

            '    If mDutoanID > 0 Then
            '        AddRow4E_Inv("Cost Center:" & GetNonAirExtRow(mDutoanID, "Cost Center"), "DOM", 0, True, 0, 0,,,, 4)
            '        AddRow4E_Inv("Trip Purpose:" & GetNonAirExtRow(mDutoanID, "Trip Purpose"), "DOM", 0, True, 0, 0,,,, 4)
            '        AddRow4E_Inv("Employee ID:" & GetNonAirExtRow(mDutoanID, "Employee ID"), "DOM", 0, True, 0, 0,,,, 4)
            '        AddRow4E_Inv("Why No Hotel Booked:" & GetNonAirExtRow(mDutoanID, "Why No Hotel Booked"), "DOM", 0, True, 0, 0,,,, 4)
            '        AddRow4E_Inv("Why Not Booked Online:" & GetNonAirExtRow(mDutoanID, "Why Not Booked Online"), "DOM", 0, True, 0, 0,,,, 4)
            '    End If
            '^_^20221006 mark by 7643 -e-
            '^_^20220831 add by 7643 -e-
        End If
        '^_^20220808 add by 7643 -e-

        SumGrandTotal()
        Return True

    End Function
    Private Function DraftNonAirDetail1(dgrTktListing As DataGridView) As Boolean
        Dim lstMainLines As New List(Of clsVatInvLine)
        Dim lstPaxName As New List(Of String)

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            Dim objVatLine As New clsVatInvLine

            If objRow.Cells("S").Value Then
                objVatLine.PaxName = objRow.Cells("PaxName").Value
                objVatLine.Unit = TranslateUnitName2Vietnamese(objRow.Cells("Service").Value, objRow.Cells("Unit").Value)
                objVatLine.Quantity = objRow.Cells("Qty").Value

                '^_^20220804 add by 7643 -b-
                If objVatLine.Unit = "Đêm" Then
                    objVatLine.Unit = "Lần"
                    objVatLine.Quantity = 1
                End If
                '^_^20220804 add by 7643 -e-

                If objRow.Cells("Service").Value = "Accommodations" Then
                    'objVatLine.Desc = TranslateServiceName2Vietnamese(objRow.Cells("Service").Value) & "||" & objRow.Cells("Supplier").Value  '^_^20220804 mark by 7643
                    objVatLine.Desc = TranslateServiceName2Vietnamese(objRow.Cells("Service").Value) & "||" & objRow.Cells("Supplier").Value & "||" & objRow.Cells("PaxName").Value  '^_^20220804 modi by 7643
                Else
                    objVatLine.Desc = TranslateServiceName2Vietnamese(objRow.Cells("Service").Value)
                End If

                objVatLine.ProviderCost = objRow.Cells("VND").Value
                objVatLine.VatPct = objRow.Cells("VatPct").Value
                objVatLine.Vat = objRow.Cells("VAT").Value

                lstMainLines.Add(objVatLine)
                mtblTkts.Rows.Add(objRow.Cells("RecId").Value, objRow.Cells("RcpId").Value)
            End If
        Next
        For Each objVatLine As clsVatInvLine In lstMainLines
            AddRow4E_Inv(objVatLine.Desc _
                         , DefineDomIntByVatPct(objVatLine.VatPct), objVatLine.ProviderCost _
                         , True, objVatLine.Vat, objVatLine.VatPct, objVatLine.Unit, objVatLine.Quantity,)
            If objVatLine.PaxName <> "" AndAlso Not lstPaxName.Contains(objVatLine.PaxName) Then
                lstPaxName.Add(objVatLine.PaxName)
            End If
        Next
        Dim arrPaxName(0 To lstPaxName.Count - 1) As String
        lstPaxName.CopyTo(arrPaxName)
        'txtBuyer.Text = Strings.Join(arrPaxName, ",")  '^_^20220804 mark by 7643
    End Function

    '^_^20220808 add by 7643 -b-
    Private Function GetNonAirExtRowDetail(xDutoanID As Integer, xFname As String) As String
        Dim mStr As String

        mStr = ScalarToString("SIR s" & vbLf &
                              "  left join CWT..GO_CompanyInfo1 gcy on s.CustID=gcy.CustID And gcy.Status='OK'" & vbLf &
                              "  left join cwt.dbo.go_CDRs gc on gcy.CMC=gc.CMC And gc.Status='OK' and gc.CdrName=s.Fname" & vbLf &
                              "  left join CWT..GO_MiscWzDate gm on s.FValue=gm.Value And gm.Status='OK' and gm.Catergory='CDR'+gc.CdrNbr and Value1=gcy.CMC",
                              "isnull(gm.Details,'') Details",
                              "s.Status ='OK' and s.RcpId=" & xDutoanID & " and s.Prod='NonAir' and s.Fname='" & xFname & "'")
        Return mStr
    End Function

    Private Function GetAirExtRowDetail(xTktID As Integer, xFname As String) As String
        Dim mStr As String

        mStr = ScalarToString("CWT..GO_Air a " & vbLf &
                              "left join CWT..GO_Travel tr on a.travelid=tr.RecId And tr.Status='OK' " & vbLf &
                              "left join CWT.dbo.go_CDRs gc on tr.CMC=gc.CMC And gc.Status='OK' " & vbLf &
                              "left join CWT..GO_MiscWzDate gm on case gc.CdrNbr when '1' then tr.ref1 when '2' then tr.ref2 when '3' then tr.ref3 when '4' then tr.ref4 " & vbLf &
                              "                                                  when '5' then tr.ref5 when '6' then tr.ref6 when '7' then tr.ref7 when '8' then tr.ref8 " & vbLf &
                              "                                                  when '9' then tr.ref9 when '10' then tr.ref10 end=gm.Value " & vbLf &
                              "  And gm.Status='OK' and gm.Catergory='CDR'+gc.CdrNbr and Value1=tr.CMC",
                              "isnull(gm.Details,'') Details",
                              "a.Tkid=" & xTktID & " and gc.CdrName='" & xFname & "'")
        Return mStr
    End Function
    '^_^20220808 add by 7643 -e-

    '^_^20220831 add by 7643 -b-
    '^_^20221006 mark by 7643 -b-
    'Public Function GetNonAirExtRow(xDutoanID As Integer, xFname As String) As String
    '    Dim mStr As String

    '    mStr = ScalarToString("SIR s", "isnull(s.FValue,'') FValue", "s.Status ='OK' and s.RcpId=" & xDutoanID & " and s.Prod='NonAir' and s.Fname='" & xFname & "'")
    '    Return mStr
    'End Function

    'Public Function GetAirExtRow(xTktID As Integer, xCdrName As String) As String
    '    Dim mStr As String

    '    mStr = ScalarToString("CWT..GO_Air a " & vbLf &
    '                          "left join CWT..GO_Travel tr on a.travelid=tr.RecId And tr.Status='OK' " & vbLf &
    '                          "left join CWT..go_CDRs gc on tr.CMC=gc.CMC And gc.Status='OK'",
    '                          "isnull(case gc.CdrNbr when '1' then tr.ref1 when '2' then tr.ref2 when '3' then tr.ref3 when '4' then tr.ref4 when '5' then tr.ref5 " & vbLf &
    '                          "                      when '6' then tr.ref6 when '7' then tr.ref7 when '8' then tr.ref8 when '9' then tr.ref9 when '10' then tr.ref10 end,'') ref",
    '                          "a.Tkid=" & xTktID & " and gc.CdrName='" & xCdrName & "'")
    '    Return mStr
    'End Function
    '^_^20221006 mark by 7643 -e-
    '^_^20220831 add by 7643 -e-

    Private Function DraftNonAirDetail12(dgrTktListing As DataGridView, intVatDiscount As Integer) As Boolean
        Dim lstMainLines As New List(Of clsVatInvLine)
        Dim lstPaxName As New List(Of String)
        Dim blnGetThisNonAir As Boolean
        Dim strDesc4SfOnly As String = String.Empty
        Dim blnServiceInclulded As Boolean
        Dim intDutoanId As Integer

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            Dim objVatLine As New clsVatInvLine
            If objRow.Cells("S").Value Then
                objVatLine.PaxName = objRow.Cells("PaxName").Value
                objVatLine.Unit = TranslateUnitName2Vietnamese(objRow.Cells("Service").Value, objRow.Cells("Unit").Value)
                objVatLine.Quantity = objRow.Cells("Qty").Value
                Select Case objRow.Cells("Service").Value
                    Case "Accommodations"
                        Dim arrHotelDetails As String() = Split(objRow.Cells("Brief").Value, "_")
                        objVatLine.Desc = TranslateServiceName2Vietnamese(objRow.Cells("Service").Value) & "||" & objRow.Cells("Supplier").Value _
                            & "||Check in:" & arrHotelDetails(2).Split(" ")(0) & " Check out:" & arrHotelDetails(3).Split(" ")(0)
                        strDesc4SfOnly = objVatLine.Desc
                    Case "Conf.Room"
                        objVatLine.Desc = TranslateServiceName2Vietnamese(objRow.Cells("Service").Value) & "||" & objRow.Cells("Brief").Value
                    Case "TransViet SVC Fee"
                        objVatLine.Desc = TranslateServiceName2Vietnamese(objRow.Cells("Service").Value)
                        If strDesc4SfOnly = "" Then
                            strDesc4SfOnly = objVatLine.Desc
                        End If
                    Case Else
                        If objRow.Cells("Service").Value = "Merchant Fee" Then
                            intDutoanId = objRow.Cells("DuToanId").Value
                        End If
                        objVatLine.Desc = TranslateServiceName2Vietnamese(objRow.Cells("Service").Value)
                End Select

                blnGetThisNonAir = GetThisTransaction(intVatDiscount, objRow.Cells("VatPct").Value)
                If blnGetThisNonAir Then
                    objVatLine.ProviderCost = objRow.Cells("VND").Value
                    objVatLine.VatPct = objRow.Cells("VatPct").Value
                    objVatLine.Vat = objRow.Cells("VAT").Value
                    lstMainLines.Add(objVatLine)
                    mtblTkts.Rows.Add(objRow.Cells("RecId").Value, objRow.Cells("RcpId").Value)
                End If
            End If
        Next
        For Each objVatLine As clsVatInvLine In lstMainLines
            If lstMainLines.Count = 1 AndAlso strDesc4SfOnly <> "" AndAlso strDesc4SfOnly <> objVatLine.Desc Then
                objVatLine.Desc = objVatLine.Desc & " " & strDesc4SfOnly
            End If
            If lstMainLines.Count = 1 AndAlso objVatLine.Desc = "Phí cà thẻ" Then
                Dim objMainItem As DataRow = GetDataTable("select top 1 * from dutoan_item where DutoanId=" & intDutoanId _
                                                          & " and Status='OK' and Service not in ('TransViet SVC Fee','Merchant Fee','Bank Fee')", Conn).Rows(0)
                Dim strMainItemDesc As String = ""
                Select Case objMainItem("Service")
                    Case "Accommodations"
                        Dim arrHotelDetails As String() = Split(objMainItem("Brief"), "_")
                        strMainItemDesc = TranslateServiceName2Vietnamese(objMainItem("Service")) & "||" & objMainItem("Supplier") _
                            & "||Check in:" & arrHotelDetails(2).Split(" ")(0) & " Check out:" & arrHotelDetails(3).Split(" ")(0)
                    Case "Conf.Room"
                        strMainItemDesc = TranslateServiceName2Vietnamese(objMainItem("Service")) & "||" & objMainItem("Brief")
                End Select
                objVatLine.Desc = objVatLine.Desc & " " & strMainItemDesc
            End If
            AddRow4E_Inv(objVatLine.Desc _
                         , DefineDomIntByVatPct(objVatLine.VatPct), objVatLine.ProviderCost _
                         , True, objVatLine.Vat, objVatLine.VatPct, objVatLine.Unit, objVatLine.Quantity, intVatDiscount)
            If objVatLine.PaxName <> "" AndAlso Not lstPaxName.Contains(objVatLine.PaxName) Then
                lstPaxName.Add(objVatLine.PaxName)
            End If
        Next

        Dim arrPaxName(0 To lstPaxName.Count - 1) As String
        lstPaxName.CopyTo(arrPaxName)
        txtBuyer.Text = Strings.Join(arrPaxName, ",")
    End Function
    Private Function DraftNonAirGeneric(dgrTktListing As DataGridView, intVatDiscount As Integer) As Boolean
        Dim lstMainServices As New List(Of String)
        Dim lstOtherServices As New List(Of String)
        Dim lstMainLines As New List(Of clsVatInvLine)
        Dim lstOtherLines As New List(Of clsVatInvLine)
        Dim objSfLine As New clsVatInvLine
        Dim objMfLine As New clsVatInvLine
        Dim blnBunldeMainWzOther As Boolean = False
        Dim blnGetThisNonAir As Boolean
        Dim strDesc4SfOnly As String = ""
        'Xac dinh co bao nhieu dong MainServices và OtherServices
        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            blnGetThisNonAir = False
            If objRow.Cells("S").Value Then
                blnGetThisNonAir = GetThisTransaction(intVatDiscount, objRow.Cells("VatPct").Value)
                If blnGetThisNonAir Then
                    Select Case objRow.Cells("Service").Value
                        Case "Accommodations", "Transfer", "Meal", "Visa"
                            AddService(objRow.Cells("Service").Value, objRow.Cells("VatPct").Value, lstMainServices)
                            If lstMainServices.Count > lstMainLines.Count Then
                                Dim objVatLine As New clsVatInvLine
                                objVatLine.ProviderCost = 0
                                objVatLine.VatPct = 0
                                objVatLine.Vat = 0
                            End If
                            If lstMainServices.Count > lstMainLines.Count Then
                                Dim objVatLine As New clsVatInvLine
                                objVatLine.Desc = objRow.Cells("Service").Value
                                objVatLine.ProviderCost = objRow.Cells("VND").Value
                                objVatLine.VatPct = objRow.Cells("VatPct").Value
                                objVatLine.Vat = objRow.Cells("VAT").Value
                                lstMainLines.Add(objVatLine)
                            Else
                                For Each objVatLine As clsVatInvLine In lstMainLines
                                    If objVatLine.Desc = objRow.Cells("Service").Value _
                                        AndAlso objVatLine.VatPct = objRow.Cells("VatPct").Value Then
                                        objVatLine.ProviderCost = objVatLine.ProviderCost + objRow.Cells("VND").Value
                                        objVatLine.Vat = objVatLine.Vat + objRow.Cells("VAT").Value
                                    End If
                                Next
                            End If
                    'Case 
                    '    strDesc = "Tiền xe"
                    '    AddService(strDesc, objRow.Cells("VatPct").Value, lstMainServices, lstMainVatPct)
                    'Case "Meal"
                    '    strDesc = "Tiền ăn"
                    '    AddService(strDesc, objRow.Cells("VatPct").Value, lstMainServices, lstMainVatPct)
                    'Case "Visa"
                    '    strDesc = "Phí Visa"
                    '    AddService(strDesc, objRow.Cells("VatPct").Value, lstMainServices, lstMainVatPct)
                        Case "Miscellaneous", "Bank Fee", "Merchant Fee"
                            AddService(objRow.Cells("Service").Value, objRow.Cells("VatPct").Value, lstOtherServices)
                            If lstOtherServices.Count > lstOtherLines.Count Then
                                Dim objVatLine As New clsVatInvLine
                                objVatLine.Desc = objRow.Cells("Service").Value
                                objVatLine.ProviderCost = objRow.Cells("VND").Value
                                objVatLine.VatPct = objRow.Cells("VatPct").Value
                                objVatLine.Vat = objRow.Cells("VAT").Value
                                lstOtherLines.Add(objVatLine)
                            Else
                                For Each objVatLine As clsVatInvLine In lstOtherLines
                                    If objVatLine.Desc = objRow.Cells("Service").Value _
                                        AndAlso objVatLine.VatPct = objRow.Cells("VatPct").Value Then
                                        objVatLine.ProviderCost = objVatLine.ProviderCost + objRow.Cells("VND").Value
                                        objVatLine.Vat = objVatLine.Vat + objRow.Cells("VAT").Value
                                    End If
                                Next
                            End If
                            objMfLine.Desc = objRow.Cells("Service").Value
                            objMfLine.ProviderCost = objMfLine.ProviderCost + objRow.Cells("VND").Value
                            objMfLine.VatPct = objRow.Cells("VatPct").Value
                            objMfLine.Vat = objMfLine.Vat + objRow.Cells("VAT").Value

                        Case "TransViet SVC Fee"
                            objSfLine.Desc = objRow.Cells("Service").Value
                            objSfLine.ProviderCost = objSfLine.ProviderCost + objRow.Cells("VND").Value
                            objSfLine.VatPct = objRow.Cells("VatPct").Value
                            objSfLine.Vat = objSfLine.Vat + objRow.Cells("VAT").Value
                    End Select
                    mtblTkts.Rows.Add(objRow.Cells("RecId").Value, objRow.Cells("RcpId").Value)
                Else
                    Select Case objRow.Cells("Service").Value
                        Case "Accommodations", "Transfer", "Meal", "Visa"
                            strDesc4SfOnly = TranslateServiceName2Vietnamese(objRow.Cells("Service").Value)
                    End Select


                End If
            End If
        Next

        'ghep Other lines vao main lines 
        If lstMainLines.Count = 1 AndAlso lstOtherLines.Count <= 1 Then
            AddMiscAmountToMainService(lstMainLines(0), lstOtherLines)
            blnBunldeMainWzOther = True
        End If

        'Tao thanh dong hoa don cho main line

        For Each objVatLine As clsVatInvLine In lstMainLines
            AddRow4E_Inv(TranslateServiceName2Vietnamese(objVatLine.Desc) _
                         , DefineDomIntByVatPct(objVatLine.VatPct), objVatLine.ProviderCost _
                         , True, objVatLine.Vat, objVatLine.VatPct, "Lần", 1, intVatDiscount)
        Next
        'tao thanh dong hoa don cho other line
        If Not blnBunldeMainWzOther Then
            For Each objVatLine As clsVatInvLine In lstOtherLines
                Dim strDesc As String = TranslateServiceName2Vietnamese(objVatLine.Desc)
                If lstMainLines.Count = 0 Then
                    strDesc = strDesc & " " & strDesc4SfOnly
                End If
                AddRow4E_Inv(strDesc _
                         , DefineDomIntByVatPct(objVatLine.VatPct), objVatLine.ProviderCost _
                         , True, objVatLine.Vat, objVatLine.VatPct,,, intVatDiscount)
            Next
        End If

        'tao dong hoa don cho service fee
        If objSfLine.ProviderCost <> 0 Then
            AddRow4E_Inv((TranslateServiceName2Vietnamese(objSfLine.Desc) & "" & strDesc4SfOnly).Trim _
                         , DefineDomIntByVatPct(objSfLine.VatPct), objSfLine.ProviderCost _
                         , True, objSfLine.Vat, objSfLine.VatPct, "Lần", 1, intVatDiscount)
        End If
        If objMfLine.ProviderCost <> 0 Then
            AddRow4E_Inv((TranslateServiceName2Vietnamese(objMfLine.Desc) & "" & strDesc4SfOnly).Trim _
                         , DefineDomIntByVatPct(objMfLine.VatPct), objMfLine.ProviderCost _
                         , True, objMfLine.Vat, objMfLine.VatPct, "Lần", 1, intVatDiscount)
        End If
    End Function
    Private Function AddMiscAmountToMainService(objMainLine As clsVatInvLine, lstMiscLines As List(Of clsVatInvLine))
        For Each objVatLine As clsVatInvLine In lstMiscLines
            If objVatLine.VatPct = objMainLine.VatPct Then
                objMainLine.ProviderCost = objMainLine.ProviderCost + objVatLine.ProviderCost
                objMainLine.Vat = objMainLine.Vat + objVatLine.Vat
            End If
        Next
        Return True
    End Function
    Private Function AddService(strService As String, intVatPct As Integer _
                                , ByRef lstServices As List(Of String)) As Boolean
        If Not lstServices.Contains(strService & "_" & intVatPct) Then
            lstServices.Add(strService & "_" & intVatPct)
        End If
        Return True
    End Function
    Public Function LoadGridNonAirHempel(dgrTktListing As DataGridView, strCustShortName As String _
                                , strDomInt As String, intVatDiscount As Integer) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim decSfNoVat As Decimal
        Dim decVat4Sf As Decimal
        Dim intRcpId As Integer
        Dim blnGetThisNonAir As Boolean
        Dim blnGetThisSf As Boolean
        Dim intVatPctNonAir As Integer
        Dim intVatPctSf As Integer

        mstrProduct = "N-A"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        If myStaff.City = "HAN" Then
            LoadE_InvSettings("", "PAX", "TVTR HAN")
        ElseIf myStaff.City = "SGN" Then
            LoadE_InvSettings("", "PAX", "TVTR")
        End If
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Cells("S").Value Then
                Dim strDesc As String = String.Empty
                Dim decTruocThue As Decimal = 0
                Dim decVat As Decimal = 0
                Select Case objRow.Cells("Service Type").Value
                    Case "Accommodations"
                        strDesc = "Tiền phòng " & objRow.Cells("Service Provider ").Value _
                                & " - " & objRow.Cells("Pax Name").Value
                    Case "Transfer"
                        strDesc = "Tiền xe " & objRow.Cells("Service Provider ").Value _
                                & " - " & objRow.Cells("Pax Name").Value
                    Case Else
                        MsgBox("Dich vu moi. Can yeu cau Khanhnm bổ sung.")
                End Select

                intVatPctNonAir = Math.Round(objRow.Cells("VAT for Expense").Value * 100 / objRow.Cells("Expense").Value)
                If objRow.Cells("VAT for SVF").Value IsNot Nothing Then
                    If objRow.Cells("VAT for SVF").Value = 0 Then
                        intVatPctSf = 0
                    Else
                        intVatPctSf = Math.Round(objRow.Cells("VAT for SVF").Value * 100 / objRow.Cells("Service fee").Value)
                    End If
                End If


                blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatPctNonAir)
                blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPctSf)

                Select Case strDomInt
                    Case "DOM"
                        If blnGetThisNonAir Then
                            decTruocThue = Math.Round(objRow.Cells("Expense").Value)
                            decVat = Math.Round(objRow.Cells("VAT for Expense").Value)
                        End If
                        If blnGetThisSf Then
                            decTruocThue = decTruocThue + Math.Round(objRow.Cells("Service fee").Value)
                            decVat = decVat + Math.Round(objRow.Cells("VAT for SVF").Value)
                        End If

                        If blnGetThisNonAir Then
                            AddRow4E_Inv(strDesc, "DOM", decTruocThue, True, decVat,, "Lần", 1, intVatDiscount)
                        ElseIf blnGetThisSf AndAlso decTruocThue <> 0 Then
                            AddRow4E_Inv("Phí dịch vụ " & strDesc, "DOM", decTruocThue, True, decVat,, "Lần", 1, intVatDiscount)
                        End If
                    Case "INT"
                        decTruocThue = Math.Round(objRow.Cells("Expense").Value)
                        decVat = 0
                        decSfNoVat = decSfNoVat + Math.Round(objRow.Cells("Service fee").Value)
                        decVat4Sf = decVat4Sf + Math.Round(objRow.Cells("VAT for SVF").Value)
                        AddRow4E_Inv(strDesc, "INT", decTruocThue, True, decVat,, "Lần", 1, intVatDiscount)
                        AddRow4E_Inv("Phí dịch vụ ", "DOM", decSfNoVat, True, decVat4Sf,, "Lần", 1, intVatDiscount)
                End Select

                'Tim RecId va RcpId cua Non Air
                intRcpId = ScalarToInt("Dutoan_Tour", "RcpId" _
                                       , "where RecID=(select DuToanID from DuToan_Item" _
                                        & " where recid=" & objRow.Cells("Related" & vbLf & "Item").Value & ")")

                If intRcpId = 0 Then
                    MsgBox("unable to find Non Air item " & objRow.Cells("Related Item").Value & " in RAS")
                    Me.Dispose()
                    Return False
                End If
                mtblTkts.Rows.Add(objRow.Cells("Related" & vbLf & "Item").Value, intRcpId)
            End If
        Next
        SumGrandTotal()
        AdjustSrv4E_Inv()
        Return True
    End Function
    '^_^20220802 add by 7643 -b-
    Public Function LoadGridNonAir4HASBRO(dgrTktListing As DataGridView, strCustShortName As String _
                                       , blnGetPrice As Boolean _
                                       , intVatDiscount As Integer) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim strDomInt As String
        Dim strDesc4Sf As String
        Dim blnGetThisNonAir As Boolean
        Dim blnGetThisSf As Boolean

        mstrProduct = "N-A"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))
        SelectVatDiscountLevel(intVatDiscount)

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")

        pnlSelectCustomer.Visible = False

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            blnGetThisNonAir = False
            blnGetThisSf = False
            Dim intVatRate As Integer = CalcVatPctNearest(objRow.Cells("Phí dịch vụ").Value, objRow.Cells("Thuế GTGT  (Phí dịch vụ)").Value)
            Dim strDesc As String = String.Empty
            Dim decTruocThue As Decimal
            Dim decVat As Decimal
            Dim mArrStr(), mHotelName, mStr, mCheckin, mCheckout As String
            strDesc4Sf = ""

            Select Case intVatRate
                Case 10, 7, 8
                    strDomInt = "DOM"
                Case Else
                    strDomInt = "INT"
            End Select

            Select Case objRow.Cells("Dịch vụ").Value
                Case "Accommodations"
                    mArrStr = Split(objRow.Cells("Mô tả dịch vụ").Value, "(CI:")
                    mHotelName = mArrStr(0).Trim

                    mStr = mArrStr(1).Trim
                    mArrStr = Split(mStr)
                    mCheckin = mArrStr(0)
                    mCheckin = Format(Date.Parse(mCheckin), "dd/MM/yyyy")

                    mCheckout = mArrStr(4)
                    mCheckout = Format(Date.Parse(mCheckout), "dd/MM/yyyy")

                    strDesc = "Phí dịch vụ tiền phòng khách sạn " & mHotelName _
                                & "||" & objRow.Cells("Tên hành khách").Value _
                                & "||" & mCheckin & "-" & mCheckout

                    If objRow.Cells("Phí dịch Vụ").Value <> 0 Then
                        Dim intVatPct4Sf As Integer = Math.Round(objRow.Cells("Thuế GTGT  (Phí dịch vụ)").Value / objRow.Cells("Phí dịch Vụ").Value * 100)
                        blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                        If blnGetThisSf Then
                            'strDesc = ("Phí dịch vụ " & strDesc4Sf).Trim
                            AddRow4E_InvHASBRO(strDesc, "DOM", objRow.Cells("Phí dịch Vụ").Value, True, objRow.Cells("Thuế GTGT  (Phí dịch vụ)").Value _
                                         ,, "Lần", 1, intVatDiscount)
                        End If
                    End If
                Case "Transfer"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)

                    strDesc = ("Tiền xe " & objRow.Cells("Mô tả dịch vụ").Value).ToString.Trim

                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Chi phí" & vbLf & "Giao dịch").Value)
                        decVat = Math.Round(objRow.Cells("Thuế" & vbLf & "GTGT").Value)
                        AddRow4E_InvHASBRO(strDesc, strDomInt, decTruocThue, True, decVat,, "Lần", 1, intVatDiscount)
                    Else
                        strDesc4Sf = strDesc
                    End If

                    If objRow.Cells("Phí" & vbLf & "Dịch Vụ").Value <> 0 Then
                        Dim intVatPct4Sf As Integer = Math.Round(objRow.Cells("Thuế GTGT  (Phí dịch vụ)").Value / objRow.Cells("Phí" & vbLf & "Dịch Vụ").Value * 100)
                        blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                        If blnGetThisSf Then
                            strDesc = ("Phí dịch vụ " & strDesc4Sf).Trim
                            AddRow4E_InvHASBRO(strDesc, "DOM", objRow.Cells("Phí" & vbLf & "Dịch Vụ").Value, True, objRow.Cells("Thuế GTGT  (Phí dịch vụ)").Value _
                                         ,, "Lần", 1, intVatDiscount)
                        End If
                    End If
                Case "Visa"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)

                    strDesc = ("Phí Visa " & objRow.Cells("Mô tả dịch vụ").Value).ToString.Trim

                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Chi phí" & vbLf & "Giao dịch").Value)
                        decVat = Math.Round(objRow.Cells("Thuế" & vbLf & "GTGT").Value)
                        AddRow4E_InvHASBRO(strDesc, strDomInt, decTruocThue, True, decVat,, "Lần", 1, intVatDiscount)
                    Else
                        strDesc4Sf = strDesc
                    End If

                    If objRow.Cells("Phí" & vbLf & "Dịch Vụ").Value <> 0 Then
                        Dim intVatPct4Sf As Integer = Math.Round(objRow.Cells("Thuế GTGT  (Phí dịch vụ)").Value / objRow.Cells("Phí" & vbLf & "Dịch Vụ").Value * 100)
                        blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                        If blnGetThisSf Then
                            strDesc = ("Phí dịch vụ " & strDesc4Sf).Trim
                            AddRow4E_InvHASBRO(strDesc, "DOM", objRow.Cells("Phí" & vbLf & "Dịch Vụ").Value, True, objRow.Cells("Thuế GTGT  (Phí dịch vụ)").Value _
                                         ,, "Lần", 1, intVatDiscount)
                        End If
                    End If
            End Select

        Next

        SumGrandTotal()
        AdjustSrv4E_Inv()
        Return True
    End Function
    '^_^20220802 add by 7643 -e-
    Public Function LoadGridNonAirAtotech(dgrTktListing As DataGridView, strCustShortName As String _
                                       , blnGetPrice As Boolean _
                                       , intVatDiscount As Integer) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim strDomInt As String
        Dim arrHotelDetails As String()
        Dim strDesc4Sf As String
        Dim blnGetThisNonAir As Boolean
        Dim blnGetThisSf As Boolean

        mstrProduct = "N-A"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))
        SelectVatDiscountLevel(intVatDiscount)

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")

        pnlSelectCustomer.Visible = False

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            blnGetThisNonAir = False
            blnGetThisSf = False
            Dim intVatRate As Integer = CalcVatPctNearest(objRow.Cells("Expense").Value, objRow.Cells("VAT for Expense").Value)
            Dim strDesc As String = String.Empty
            Dim decTruocThue As Decimal
            Dim decVat As Decimal
            strDesc4Sf = ""

            Select Case intVatRate
                Case 10, 7, 8
                    strDomInt = "DOM"
                Case Else
                    strDomInt = "INT"
            End Select

            Select Case objRow.Cells("Service Type").Value
                Case "Accommodations"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)

                    arrHotelDetails = Split(objRow.Cells("Service Description").Value, "_")
                    strDesc = "Tiền phòng " & objRow.Cells("Service Provider ").Value _
                                & "||" & objRow.Cells("Pax Name").Value _
                                & "||ngày in " & Mid(arrHotelDetails(2), 1, 10) & " ngày out " & Mid(arrHotelDetails(3), 1, 10)
                    If blnGetPrice Then
                        strDesc = strDesc & "||Đơn giá:" & Format(Math.Round(objRow.Cells("Đơn giá").Value), "#,##0")
                    End If

                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Expense").Value)
                        decVat = Math.Round(objRow.Cells("VAT for Expense").Value)
                        AddRow4E_Inv(strDesc, strDomInt, decTruocThue, True, decVat,, "Phòng", 1, intVatDiscount)
                    Else
                        strDesc4Sf = strDesc
                    End If

                    If objRow.Cells("Service fee").Value <> 0 Then
                        Dim intVatPct4Sf As Integer = Math.Round(objRow.Cells("VAT for SVF").Value / objRow.Cells("Service fee").Value * 100)
                        blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                        If blnGetThisSf Then
                            strDesc = ("Phí dịch vụ " & strDesc4Sf).Trim
                            AddRow4E_Inv(strDesc, "DOM", objRow.Cells("Service fee").Value, True, objRow.Cells("VAT for SVF").Value _
                                         ,, "Lần", 1, intVatDiscount)
                        End If
                    End If
                Case "Transfer"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)

                    strDesc = ("Tiền xe " & objRow.Cells("Service Description").Value).ToString.Trim

                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Expense").Value)
                        decVat = Math.Round(objRow.Cells("VAT for Expense").Value)
                        AddRow4E_Inv(strDesc, strDomInt, decTruocThue, True, decVat,, "Lần", 1, intVatDiscount)
                    Else
                        strDesc4Sf = strDesc
                    End If

                    If objRow.Cells("Phí" & vbLf & "Dịch Vụ").Value <> 0 Then
                        Dim intVatPct4Sf As Integer = Math.Round(objRow.Cells("Thuế GTGT  (Phí dịch vụ)").Value / objRow.Cells("Phí" & vbLf & "Dịch Vụ").Value * 100)
                        blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                        If blnGetThisSf Then
                            strDesc = ("Phí dịch vụ " & strDesc4Sf).Trim
                            AddRow4E_Inv(strDesc, "DOM", objRow.Cells("Service fee").Value, True, objRow.Cells("VAT for SVF").Value _
                                         ,, "Lần", 1, intVatDiscount)
                        End If
                    End If
                Case "Visa"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)

                    strDesc = ("Phí Visa " & objRow.Cells("Service Description").Value).ToString.Trim

                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Expense").Value)
                        decVat = Math.Round(objRow.Cells("VAT for Expense").Value)
                        AddRow4E_Inv(strDesc, strDomInt, decTruocThue, True, decVat,, "Lần", 1, intVatDiscount)
                    Else
                        strDesc4Sf = strDesc
                    End If

                    If objRow.Cells("Service fee").Value <> 0 Then
                        Dim intVatPct4Sf As Integer = Math.Round(objRow.Cells("VAT for SVF").Value / objRow.Cells("Service fee").Value * 100)
                        blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                        If blnGetThisSf Then
                            strDesc = ("Phí dịch vụ " & strDesc4Sf).Trim
                            AddRow4E_Inv(strDesc, "DOM", objRow.Cells("Service fee").Value, True, objRow.Cells("VAT for SVF").Value _
                                         ,, "Lần", 1, intVatDiscount)
                        End If
                    End If
            End Select

        Next

        SumGrandTotal()
        AdjustSrv4E_Inv()
        Return True
    End Function
    Public Function LoadGridNonAirHogan(dgrTktListing As DataGridView, strCustShortName As String _
                                       , blnGetPrice As Boolean _
                                       , intVatDiscount As Integer) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim strDomInt As String
        Dim strDesc4Sf As String
        Dim blnGetThisNonAir As Boolean
        Dim blnGetThisSf As Boolean
        Dim strDesc As String = ""

        mstrProduct = "N-A"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))
        SelectVatDiscountLevel(intVatDiscount)

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "GDS")

        pnlSelectCustomer.Visible = False

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            blnGetThisNonAir = False
            blnGetThisSf = False
            Dim intVatRate As Integer = CalcVatPctNearest(objRow.Cells("Giá phòng " & vbLf & "(Room Rate)").Value, objRow.Cells("Thuế GTGT" & vbLf & "(VAT)").Value)

            strDesc4Sf = ""

            Select Case intVatRate
                Case 10, 7, 8
                    strDomInt = "DOM"
                Case Else
                    strDomInt = "INT"
            End Select

            Select Case objRow.Cells("Tên dịch vụ (Service)").Value
                Case "Accommodations"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)

                    strDesc = "Tiền phòng khách sạn" _
                                & "||" & objRow.Cells("Tên khách" & vbLf & "(Traveller Name)").Value _
                                & "||" & objRow.Cells("Provider's name").Value _
                                & "||Ngày nhận phòng " & Format(objRow.Cells("Check in Date").Value, "dd/MM/yyyy") _
                                & "||Ngày trả phòng " & Format(objRow.Cells("Check out Date").Value, "dd/MM/yyyy")

                Case "Transfer"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)

                    strDesc = "Tiền xe " & "||" & objRow.Cells("Tên khách" & vbLf & "(Traveller Name)").Value _
                                & "||" & objRow.Cells("Provider's name").Value _
                                & "||Ngày đón " & Format(objRow.Cells("Check in Date").Value, "dd/MM/yyyy") _
                                & "||Ngày tiễn " & Format(objRow.Cells("Check out Date").Value, "dd/MM/yyyy")


                Case "Visa"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)
                    strDesc = ("Phí Visa " & objRow.Cells("Service Description").Value).ToString.Trim
            End Select

            If blnGetThisNonAir Then
                AddRow4E_Inv(strDesc, strDomInt, objRow.Cells("Giá phòng " & vbLf & "(Room Rate)").Value _
                             , True, objRow.Cells("Thuế GTGT" & vbLf & "(VAT)").Value,, "Lần", 1, intVatDiscount)
            Else
                strDesc4Sf = strDesc
            End If

            If objRow.Cells("Phí dịch vụ" & vbLf & "(Sv.Fee)").Value <> 0 Then
                Dim intVatPct4Sf As Integer = Math.Round(objRow.Cells("Thuế GTGT" & vbLf & "(VAT) của Phí dịch vụ").Value / objRow.Cells("Phí dịch vụ" & vbLf & "(Sv.Fee)").Value * 100)
                blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                If blnGetThisSf Then
                    strDesc = ("Phí dịch vụ " & strDesc4Sf).Trim
                    AddRow4E_Inv(strDesc, "DOM", objRow.Cells("Phí dịch vụ" & vbLf & "(Sv.Fee)").Value, True, objRow.Cells("Thuế GTGT" & vbLf & "(VAT) của Phí dịch vụ").Value _
                                 ,, "Lần", 1, intVatDiscount)
                End If
            End If
        Next

        SumGrandTotal()
        AdjustSrv4E_Inv()
        Return True
    End Function
    Public Function LoadGridNonAir4JCI(dgrTktListing As DataGridView, strCustShortName As String _
                                       , blnGetPrice As Boolean _
                                       , intVatDiscount As Integer) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim strDomInt As String
        Dim arrHotelDetails As String()
        Dim strDesc4Sf As String
        Dim blnGetThisNonAir As Boolean
        Dim blnGetThisSf As Boolean

        mstrProduct = "N-A"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))
        SelectVatDiscountLevel(intVatDiscount)

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")

        pnlSelectCustomer.Visible = False

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            blnGetThisNonAir = False
            blnGetThisSf = False
            Dim intVatRate As Integer = CalcVatPctNearest(objRow.Cells("Expense").Value, objRow.Cells("VAT for Expense").Value)
            Dim strDesc As String = String.Empty
            Dim decTruocThue As Decimal
            Dim decVat As Decimal
            strDesc4Sf = ""

            Select Case intVatRate
                Case 10, 7, 8
                    strDomInt = "DOM"
                Case Else
                    strDomInt = "INT"
            End Select

            Select Case objRow.Cells("Service Type").Value
                Case "Accommodations"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)

                    arrHotelDetails = Split(objRow.Cells("Service Description").Value, "_")
                    strDesc = "Tiền phòng " & objRow.Cells("Service Provider ").Value _
                                & "||" & objRow.Cells("Pax Name").Value _
                                & "||ngày in " & Mid(arrHotelDetails(2), 1, 10) & " ngày out " & Mid(arrHotelDetails(3), 1, 10)
                    If blnGetPrice Then
                        strDesc = strDesc & "||Đơn giá:" & Format(Math.Round(objRow.Cells("Đơn giá").Value), "#,##0")
                    End If

                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Expense").Value)
                        decVat = Math.Round(objRow.Cells("VAT for Expense").Value)
                        AddRow4E_Inv(strDesc, strDomInt, decTruocThue, True, decVat,, "Phòng", 1, intVatDiscount)
                    Else
                        strDesc4Sf = strDesc
                    End If

                    If objRow.Cells("Service fee").Value <> 0 Then
                        Dim intVatPct4Sf As Integer = Math.Round(objRow.Cells("VAT for SVF").Value / objRow.Cells("Service fee").Value * 100)
                        blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                        If blnGetThisSf Then
                            strDesc = ("Phí dịch vụ " & strDesc4Sf).Trim
                            AddRow4E_Inv(strDesc, "DOM", objRow.Cells("Service fee").Value, True _
                                         , objRow.Cells("VAT for SVF").Value _
                                         ,, "Lần", 1, intVatDiscount)
                        End If
                    End If
                Case "Transfer"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)

                    strDesc = ("Tiền xe " & objRow.Cells("Service Description").Value).ToString.Trim

                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Expense").Value)
                        decVat = Math.Round(objRow.Cells("VAT for Expense").Value)
                        AddRow4E_Inv(strDesc, strDomInt, decTruocThue, True, decVat,, "Lần", 1, intVatDiscount)
                    Else
                        strDesc4Sf = strDesc
                    End If

                    If objRow.Cells("Service fee").Value <> 0 Then
                        Dim intVatPct4Sf As Integer = Math.Round(objRow.Cells("VAT for SVF").Value / objRow.Cells("Service fee").Value * 100)
                        blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                        If blnGetThisSf Then
                            strDesc = ("Phí dịch vụ " & strDesc4Sf).Trim
                            AddRow4E_Inv(strDesc, "DOM", objRow.Cells("Service fee").Value, True _
                                         , objRow.Cells("VAT for SVF").Value _
                                         ,, "Lần", 1, intVatDiscount)
                        End If
                    End If
                Case "Visa"
                    blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatRate)

                    strDesc = ("Phí Visa " & objRow.Cells("Service Description").Value).ToString.Trim

                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Expense").Value)
                        decVat = Math.Round(objRow.Cells("VAT for Expense").Value)
                        AddRow4E_Inv(strDesc, strDomInt, decTruocThue, True, decVat,, "Lần", 1, intVatDiscount)
                    Else
                        strDesc4Sf = strDesc
                    End If

                    If objRow.Cells("Service fee").Value <> 0 Then
                        Dim intVatPct4Sf As Integer = Math.Round(objRow.Cells("VAT for SVF").Value / objRow.Cells("Service fee").Value * 100)
                        blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                        If blnGetThisSf Then
                            strDesc = ("Phí dịch vụ " & strDesc4Sf).Trim
                            AddRow4E_Inv(strDesc, "DOM", objRow.Cells("Service fee").Value, True, objRow.Cells("VAT for SVF").Value _
                                         ,, "Lần", 1, intVatDiscount)
                        End If
                    End If
            End Select

        Next

        SumGrandTotal()
        AdjustSrv4E_Inv()
        Return True
    End Function
    Public Function LoadGridNonAirPathVN(dgrTktListing As DataGridView, strCustShortName As String _
                                , strDomInt As String, strProjectCode As String _
                                , intVatDiscount As Integer) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim decSfNoVat As Decimal
        Dim decVat4Sf As Decimal
        Dim intRcpId As Integer
        Dim blnGetThisNonAir As Boolean
        Dim blnGetThisSf As Boolean
        Dim intVatPctNonAir As Integer
        Dim intVatPctSf As Integer

        mstrProduct = "N-A"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        If myStaff.City = "HAN" Then
            LoadE_InvSettings("", "PAX", "TVTR HAN")
        ElseIf myStaff.City = "SGN" Then
            LoadE_InvSettings("", "PAX", "TVTR")
        End If
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Cells("Project Code").Value = strProjectCode _
                AndAlso objRow.Cells("Dom/Int").Value = strDomInt Then
                Dim strDesc As String = String.Empty
                Dim decTruocThue As Decimal = 0
                Dim decVat As Decimal = 0
                Dim arrDetails As String()

                Select Case objRow.Cells("Type of service").Value
                    Case "Accommodations"
                        strDesc = "Tiền phòng||" & objRow.Cells("Provider").Value _
                                & " " & objRow.Cells("Room Night").Value & " đêm||" _
                                & objRow.Cells("Traveller").Value
                    Case "Transfer"
                        arrDetails = Split(objRow.Cells("Service Descrpt").Value, "|")
                        strDesc = "Tiền xe " & objRow.Cells("Provider").Value _
                                & Replace(arrDetails(1), vbLf, "||") _
                                & "||" & objRow.Cells("Traveller").Value
                    Case "Miscellaneous"
                        strDesc = objRow.Cells("Service Descrpt").Value
                    Case "Visa"
                        strDesc = "Tiền Visa " & objRow.Cells("Provider").Value _
                            & "||" & objRow.Cells("Traveller").Value
                    Case Else
                        MsgBox("Dich vu moi. Can yeu cau Khanhnm bổ sung.")
                End Select

                If objRow.Cells("Travel Cost").Value <> 0 Then
                    AddRow4E_Inv(strDesc, strDomInt, objRow.Cells("Travel Cost").Value, True _
                                 , objRow.Cells("VAT" & vbLf & "(Travel Cost)").Value,, "Lần", 1, intVatDiscount)
                    strDesc = ""
                End If

                If objRow.Cells("Service Fee").Value <> 0 Then
                    AddRow4E_Inv(Trim("Phí dịch vụ " & strDesc), "DOM", objRow.Cells("Service Fee").Value _
                                 , True, objRow.Cells("VAT for SVF").Value,, "Lần", 1, intVatDiscount)
                End If

                ''Tim RecId va RcpId cua Non Air
                'intRcpId = ScalarToInt("Dutoan_Tour", "RcpId" _
                '                       , "where RecID=(select DuToanID from DuToan_Item" _
                '                        & " where recid=" & objRow.Cells("Related" & vbLf & "Item").Value & ")")

                'If intRcpId = 0 Then
                '    MsgBox("unable to find Non Air item " & objRow.Cells("Related Item").Value & " in RAS")
                '    Me.Dispose()
                '    Return False
                'End If
                'mtblTkts.Rows.Add(objRow.Cells("Related" & vbLf & "Item").Value, intRcpId)
            End If
        Next
        SumGrandTotal()
        AdjustSrv4E_Inv()
        Return True
    End Function
    Public Function LoadGridCar4NghiSon(dgrTktListing As DataGridView, strCustShortName As String _
                                , strDomInt As String, intVatDiscount As Integer) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim decSfNoVat As Decimal
        Dim decVat4Sf As Decimal
        Dim intRcpId As Integer
        Dim intVatPct4NonAir As Integer
        Dim blnGetThisNonAir As Boolean
        Dim intVatPct4Sf As Integer
        Dim blnGetThisSf As Boolean

        mstrProduct = "N-A"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            Dim strDesc As String = String.Empty
            Dim decTruocThue As Decimal
            Dim decVat As Decimal
            Dim arrDetails As String() = Split(objRow.Cells("Details").Value, "|")
            arrDetails = Split(Replace(arrDetails(0), " 00:00", ""), "_")
            ReDim Preserve arrDetails(0 To arrDetails.Length - 2)

            strDesc = "Xe đón/tiễn sân bay||" & Join(arrDetails, "_") _
                            & "||" & objRow.Cells("Guest's Name").Value

            blnGetThisNonAir = False
            blnGetThisSf = False
            intVatPct4NonAir = CalcVatPctNearest(objRow.Cells("Amount").Value, objRow.Cells("VAT").Value)
            blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatPct4NonAir)

            If objRow.Cells("Service fee").Value <> 0 Then
                intVatPct4Sf = CalcVatPctNearest(objRow.Cells("Service fee").Value, objRow.Cells("VAT of service fee").Value)
                blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
            End If

            Select Case strDomInt
                Case "DOM"
                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Amount").Value)
                        decVat = Math.Round(objRow.Cells("VAT").Value)
                    End If
                    If blnGetThisSf Then
                        decTruocThue = decTruocThue + Math.Round(objRow.Cells("Service fee").Value)
                        decVat = decVat + Math.Round(objRow.Cells("VAT of service fee").Value)
                    End If
                    If blnGetThisNonAir Then
                        AddRow4E_Inv(strDesc, "DOM", decTruocThue, True, decVat,, "Lần", 1, intVatDiscount)
                    ElseIf blnGetThisSf Then
                        AddRow4E_Inv("Phí dịch vụ " & strDesc, "DOM", decTruocThue, True, decVat,, "Phòng", 1, intVatDiscount)
                    End If

                Case "INT"

                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Amount").Value)
                        decVat = Math.Round(objRow.Cells("VAT").Value)
                    End If
                    If blnGetThisSf Then
                        decTruocThue = decTruocThue + Math.Round(objRow.Cells("Service fee").Value)
                        decVat = decVat + Math.Round(objRow.Cells("VAT of service fee").Value)
                    End If

                    If blnGetThisNonAir Then
                        AddRow4E_Inv(strDesc, "INT", objRow.Cells("Amount").Value, True, 0,, "Lần", 1, intVatDiscount)
                    End If
                    If blnGetThisSf Then
                        Dim strDesc4Sf As String = String.Empty
                        If blnGetThisNonAir Then
                            strDesc4Sf = "Phí dịch vụ"
                        Else
                            strDesc4Sf = "Phí dịch vụ " & strDesc
                        End If
                        AddRow4E_Inv(strDesc4Sf, "DOM", objRow.Cells("Service fee").Value, True, objRow.Cells("VAT of service fee").Value,, "Lần", 1, intVatDiscount)
                    End If

            End Select

            ''Tim RecId va RcpId cua Non Air
            'intRcpId = ScalarToInt("Dutoan_Tour", "RcpId" _
            '                       , "where RecID=(select DuToanID from DuToan_Item" _
            '                        & " where recid=" & objRow.Cells("Related" & vbLf & "Item").Value & ")")

            'If intRcpId = 0 Then
            '    MsgBox("unable to find Non Air item " & objRow.Cells("Related Item").Value & " in RAS")
            '    Me.Dispose()
            '    Return False
            'End If
            'mtblTkts.Rows.Add(objRow.Cells("Related" & vbLf & "Item").Value, intRcpId)

        Next

        SumGrandTotal()
        Return True
    End Function
    Public Function LoadGridHotel4NghiSon(dgrTktListing As DataGridView, strCustShortName As String _
                                , strDomInt As String, intVatDiscount As Integer) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim decSfNoVat As Decimal
        Dim decVat4Sf As Decimal
        Dim intRcpId As Integer
        Dim intVatPct4NonAir As Integer
        Dim blnGetThisNonAir As Boolean
        Dim intVatPct4Sf As Integer
        Dim blnGetThisSf As Boolean

        mstrProduct = "N-A"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            Dim strDesc As String = String.Empty
            Dim decTruocThue As Decimal
            Dim decVat As Decimal
            Dim arrDetails As String() = Split(objRow.Cells("Details").Value, " ")
            strDesc = "Tiền phòng " & objRow.Cells("Hotel").Value _
                            & "||" & objRow.Cells("Guest's Name").Value _
                            & "||Check in:" & arrDetails(0) & " Check out:" & arrDetails(1)

            blnGetThisNonAir = False
            blnGetThisSf = False
            intVatPct4NonAir = CalcVatPctNearest(objRow.Cells("Amount").Value, objRow.Cells("VAT").Value)
            blnGetThisNonAir = GetThisTransaction(intVatDiscount, intVatPct4NonAir)
            intVatPct4Sf = CalcVatPctNearest(objRow.Cells("Service fee").Value, objRow.Cells("VAT of service fee").Value)
            blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)

            Select Case strDomInt
                Case "DOM"
                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Amount").Value)
                        decVat = Math.Round(objRow.Cells("VAT").Value)
                    End If
                    If blnGetThisSf Then
                        decTruocThue = decTruocThue + Math.Round(objRow.Cells("Service fee").Value)
                        decVat = decVat + Math.Round(objRow.Cells("VAT of service fee").Value)
                    End If
                    If blnGetThisNonAir Then
                        AddRow4E_Inv(strDesc, "DOM", decTruocThue, True, decVat,, "Phòng", 1, intVatDiscount)
                    ElseIf blnGetThisSf Then
                        AddRow4E_Inv("Phí dịch vụ " & strDesc, "DOM", decTruocThue, True, decVat,, "Phòng", 1, intVatDiscount)
                    End If

                Case "INT"

                    If blnGetThisNonAir Then
                        decTruocThue = Math.Round(objRow.Cells("Amount").Value)
                        decVat = Math.Round(objRow.Cells("VAT").Value)
                    End If
                    If blnGetThisSf Then
                        decTruocThue = decTruocThue + Math.Round(objRow.Cells("Service fee").Value)
                        decVat = decVat + Math.Round(objRow.Cells("VAT of service fee").Value)
                    End If

                    If blnGetThisNonAir Then
                        AddRow4E_Inv(strDesc, "INT", objRow.Cells("Amount").Value, True, 0,, "Phòng", 1, intVatDiscount)
                    End If
                    If blnGetThisSf Then
                        Dim strDesc4Sf As String = String.Empty
                        If blnGetThisNonAir Then
                            strDesc4Sf = "Phí dịch vụ"
                        Else
                            strDesc4Sf = "Phí dịch vụ " & strDesc
                        End If
                        AddRow4E_Inv(strDesc4Sf, "DOM", objRow.Cells("Service fee").Value, True, objRow.Cells("VAT of service fee").Value,, "Lần", 1, intVatDiscount)
                    End If

            End Select

            ''Tim RecId va RcpId cua Non Air
            'intRcpId = ScalarToInt("Dutoan_Tour", "RcpId" _
            '                       , "where RecID=(select DuToanID from DuToan_Item" _
            '                        & " where recid=" & objRow.Cells("Related" & vbLf & "Item").Value & ")")

            'If intRcpId = 0 Then
            '    MsgBox("unable to find Non Air item " & objRow.Cells("Related Item").Value & " in RAS")
            '    Me.Dispose()
            '    Return False
            'End If
            'mtblTkts.Rows.Add(objRow.Cells("Related" & vbLf & "Item").Value, intRcpId)

        Next

        SumGrandTotal()
        Return True
    End Function
    Public Function LoadGridAhc4NghiSon(dgrTktListing As DataGridView, strCustShortName As String) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim decSfNoVat As Decimal
        Dim decVat4Sf As Decimal
        'Dim tblEachTkt As DataTable
        Dim intRcpId As Integer

        mstrProduct = "N-A"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")

        pnlSelectCustomer.Visible = False

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            Dim strDesc As String = String.Empty
            Dim decTruocThue As Decimal
            Dim decVat As Decimal

            strDesc = "Phí dịch vụ ngoài giờ" & "||" & objRow.Cells("Passenger" & vbLf & "name").Value

            decTruocThue = Math.Round(objRow.Cells("ServiceFee").Value)
            decVat = Math.Round(objRow.Cells("VAT").Value)

            AddRow4E_Inv(strDesc, "DOM", decTruocThue, True, decVat,, "Lần", 1,)


        Next


        Return True
    End Function

    Public Function LoadGridTktsAirPATHVN(dgrTktListing As DataGridView, strCustShortName As String _
                                       , strDomInt As String, strProjectId As String) As Boolean
        Dim tblCustomer As System.Data.DataTable
        Dim strDesc As String = String.Empty
        Dim tblEachTkt As DataTable
        Dim decVatable As Decimal
        Dim decVat As Decimal

        mstrProduct = "Air"
        tblCustomer = GetDataTable("Select TOP 1 * from CustomerList" _
                                   & " where Status='OK' and City='" & myStaff.City _
                                   & "' and CustShortName='" & strCustShortName & "'")
        LoadCustomer(tblCustomer.Rows(0))

        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")

        pnlSelectCustomer.Visible = False

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            With objRow
                If .Cells("Project Code").Value = strProjectId Then
                    strDesc = "Vé máy bay " & .Cells("Ticket number").Value _
                        & "||" & .Cells("Itinerary").Value & "||" & .Cells("Pax Name").Value
                    If strDomInt = "DOM" Then
                        decVatable = .Cells("Total Fare + Tax").Value + .Cells("AirlineCharge").Value
                        decVat = .Cells("VAT for Fare + Tax").Value + .Cells("VAT for Airline Charge").Value
                    Else
                        decVatable = .Cells("Total Fare + Tax").Value + .Cells("AL Charge").Value
                        decVat = 0
                    End If
                    If decVatable <> 0 Then
                        AddRow4E_Inv(strDesc, strDomInt, decVatable, True, decVat,, "Vé", 1)
                        strDesc = ""
                    End If

                    AddRow4E_Inv(Trim("Phí dịch vụ " & strDesc), "DOM", .Cells("Service Fee (No VAT)").Value, True _
                                 , .Cells("VAT for SVF").Value,, "Lần", 1)
                    Dim strGetTktRecord As String
                    strGetTktRecord = "Select top 1 RecId,RcpId,Tkno from tkt where Tkno='" _
                                      & objRow.Cells("Ticket number").Value _
                                      & "' and SRV='" & objRow.Cells("Sale/Refund").Value _
                                      & "' and DOI ='" & CreateFromDate(objRow.Cells("Issued Date").Value) _
                                      & "' order by RecId desc"

                    tblEachTkt = GetDataTable(strGetTktRecord, Conn)
                    If tblEachTkt.Rows.Count = 0 Then
                        MsgBox("unable to find " & objRow.Cells("Ticket number").Value & " in RAS")
                        Me.Dispose()
                        Return False
                    End If
                    mtblTkts.Rows.Add(tblEachTkt.Rows(0)("RecId"), tblEachTkt.Rows(0)("RcpId"))
                End If

            End With
        Next
        SumGrandTotal()
        AdjustSrv4E_Inv()

        Return True
    End Function

    Private Sub AddRow4E_InvNoPct(xDesc As String, xNet As Decimal, xVat As Decimal, xUnit As String)
        Dim mVatPct As String

        If xNet = 0 Then
            mVatPct = 0
        Else
            mVatPct = Math.Round(xVat / xNet * 100)
        End If
        AddRow4E_Inv(xDesc, "DOM", xNet, True, xVat, mVatPct, xUnit, 1)
    End Sub
    '^_^20220929 add by 7643 -e-

    Public Function LoadGridTktsAirHempel(dgrTktListing As DataGridView, strSheetName As String _
                                    , strCustShortName As String, intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim blnGetThisTkt As Boolean
        Dim intVatPct As Integer
        Dim blnGetThisSf As Boolean
        Dim intVatPct4Sf As Integer

        mstrProduct = "AIR"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        If myStaff.City = "HAN" Then
            LoadE_InvSettings("", "PAX", "TVTR HAN")
        ElseIf myStaff.City = "SGN" Then
            LoadE_InvSettings("", "PAX", "TVTR")
        End If

        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))
        SelectVatDiscountLevel(intVatDiscount)
        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Cells("S").Value Then
                Dim decFTC As Decimal = 0
                Dim decVat As Decimal = 0
                Dim tblEachTkt As DataTable
                Dim decSfNoVat As Decimal = 0
                Dim decVat4Sf As Decimal = 0

                If strSheetName = "Hotline" Then
                    'intVatPct = Math.Round()
                    'blnGetThisTkt = GetThisTransaction(intVatDiscount, intVatPct)

                    AddRow4E_Inv("Phí dịch vụ ngoài giờ " & "-" & objRow.Cells("Passenger" & vbLf & "name").Value _
                                 , "DOM", objRow.Cells("ServiceFee").Value, True, objRow.Cells("VAT").Value,, "Lần", 1, intVatDiscount)

                ElseIf strSheetName.StartsWith("DOM") Or strSheetName.StartsWith("MISC") Then
                    If objRow.Cells("VAT4Fare_VND").Value <> 0 Then
                        intVatPct = Math.Round(objRow.Cells("VAT4Fare_VND").Value * 100 / objRow.Cells("Fare_VND").Value)
                    ElseIf objRow.Cells("SRV").Value = "S" Then
                        intVatPct = GetVatPct(objRow.Cells("DOI").Value)
                    ElseIf objRow.Cells("SRV").Value = "R" Then
                        Dim dteOriDOI As Date = ScalarToDate("Tkt", "top 1 DOI", "Srv='S' and Status='OK' and Tkno='" _
                                                             & objRow.Cells("TKNO").Value & "' order by RecId desc")
                        intVatPct = GetVatPct(dteOriDOI)
                    End If

                    If pblnTT78 AndAlso objRow.Cells("SRV").Value = "R" Then
                        blnGetThisTkt = False
                    Else
                        blnGetThisTkt = GetThisTransaction(intVatDiscount, intVatPct)
                    End If

                    intVatPct4Sf = Math.Round(objRow.Cells("VAT (SF)").Value * 100 / objRow.Cells("Service Fee (No VAT)").Value)
                    blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)

                    If blnGetThisTkt Then
                        decVat = Math.Round(objRow.Cells("VAT4Fare_VND").Value) + Math.Round(objRow.Cells("VAT AL Charge").Value) _
                        + Math.Round(objRow.Cells("VAT Tax").Value)

                        decFTC = objRow.Cells("Fare_VND").Value + Math.Round(objRow.Cells("Tax").Value)
                        If objRow.Cells("srv").Value = "S" Then
                            decFTC = decFTC + Math.Round(objRow.Cells("AL Charge").Value)
                        End If
                    End If
                    If blnGetThisSf Then
                        decVat = decVat + Math.Round(objRow.Cells("VAT (SF)").Value)

                        decFTC = decFTC + Math.Round(objRow.Cells("Service Fee (No VAT)").Value)
                    End If

                    If objRow.Cells("SRV").Value = "S" Then
                        If blnGetThisTkt Then
                            AddRow4E_Inv("Vé máy bay " & objRow.Cells("TKNO").Value _
                                & "-" & objRow.Cells("Itinerary").Value, "DOM", decFTC, True, decVat,, "Vé", 1, intVatDiscount)
                        ElseIf blnGetThisSf Then
                            AddRow4E_Inv("Phí dịch vụ vé máy bay " & objRow.Cells("TKNO").Value _
                                & "-" & objRow.Cells("Itinerary").Value, "DOM", decFTC, True, decVat,, "Vé", 1, intVatDiscount)
                        End If

                    Else    've hoan
                        If blnGetThisTkt Then
                            AddRow4E_Inv("Tiền hoàn vé máy bay " & objRow.Cells("TKNO").Value _
                                & "-" & objRow.Cells("Pax Name").Value, "DOM", decFTC, True, decVat,, "Vé", 1, intVatDiscount)
                        ElseIf blnGetThisSf Then
                            AddRow4E_Inv("Phí dịch vụ hoàn vé máy bay " & objRow.Cells("TKNO").Value _
                                & "-" & objRow.Cells("Itinerary").Value, "DOM", decFTC, True, decVat,,,, intVatDiscount)
                        End If

                        If Not (intVatDiscount = 30) Then
                            AddRow4E_Inv("Phí hoàn vé máy bay " & objRow.Cells("TKNO").Value _
                            & "-" & objRow.Cells("Pax Name").Value, "DOM", objRow.Cells("AL Charge").Value _
                            , True, 0, -1, "Lần", 1, intVatDiscount)
                        End If
                    End If
                Else    ' Sheet INT
                    Dim decRoe As Decimal
                    intVatPct4Sf = Math.Round(Math.Round(objRow.Cells("VAT (SF)").Value / objRow.Cells("Service Fee (No VAT)").Value))
                    blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                    If blnGetThisSf Then
                        decSfNoVat = decSfNoVat + Math.Round(objRow.Cells("Service Fee (No VAT)").Value)
                        decVat4Sf = decVat4Sf + Math.Round(objRow.Cells("VAT (SF)").Value)
                    End If


                    If objRow.Cells("Currency").Value = "VND" Then
                        decRoe = 1
                    Else
                        decRoe = objRow.Cells("ROE (USD/VND)").Value
                    End If
                    decFTC = Math.Round(objRow.Cells("Net Fare").Value _
                        * decRoe) + Math.Round(objRow.Cells("AL Charge").Value _
                        * decRoe) + Math.Round(objRow.Cells("Tax").Value _
                        * decRoe)

                    intVatPct = 0
                    If Not intVatDiscount Then
                        AddRow4E_Inv("Vé máy bay " & objRow.Cells("TKNO").Value _
                        & "-" & objRow.Cells("Pax Name").Value, "INT", decFTC, True, decVat,, "Vé", 1, intVatDiscount)
                    End If
                    If blnGetThisSf Then
                        AddRow4E_Inv("Phí dịch vụ ", "DOM", decSfNoVat, True, decVat4Sf,, "Lần", 1, intVatDiscount)
                    End If
                End If

                Dim strGetTktRecord As String
                If strSheetName.ToUpper = "HOTLINE" Then
                    strGetTktRecord = "Select top 1 RecId,RcpId,Tkno from tkt where Rcpno='" _
                                          & objRow.Cells("Transaction Code").Value _
                                          & "' and SRV='S' order by RecId desc"
                Else
                    strGetTktRecord = "Select top 1 RecId,RcpId,Tkno from tkt where Tkno='" _
                                          & objRow.Cells("TKNO").Value _
                                          & "' and SRV='" & objRow.Cells("SRV").Value _
                                          & "' and DOI ='" & CreateFromDate(objRow.Cells("DOI").Value) _
                                          & "' order by RecId desc"
                End If
                tblEachTkt = GetDataTable(strGetTktRecord, Conn)
                If tblEachTkt.Rows.Count = 0 Then
                    MsgBox("unable to find " & objRow.Cells("TKNO").Value & " in RAS")
                    Me.Dispose()
                    Return False
                End If
                mtblTkts.Rows.Add(tblEachTkt.Rows(0)("RecId"), tblEachTkt.Rows(0)("RcpId"))
            End If
        Next
        SumGrandTotal()
        AdjustSrv4E_Inv()

    End Function
    Public Function LoadGridNonAir4AbbottLocal(dgrTktListing As DataGridView, strCustShortName As String _
                                            , intInvNo As Integer, blnVat8 As Boolean) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim intVatPct As Integer
        Dim decVatable4Service As Decimal
        Dim decVat As Decimal
        Dim intVatPct4Sf As Integer
        Dim decVatable4Sf As Decimal
        Dim decVat4Sf As Decimal
        Dim strGetTktRecord As String
        Dim lstService As New List(Of String)
        Dim tblEachTkt As DataTable
        Dim strDesc As String = ""
        Dim intMonth As Integer

        mstrProduct = "N-A"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "GDS")

        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Cells("NOTE").Value = intInvNo Then
                If intMonth = 0 Then
                    intMonth = objRow.Cells("Month" & vbLf & "of StartDate").Value
                End If
                If txtBooker.Text = "" Then
                    txtBooker.Text = objRow.Cells("Người đặt").Value
                End If
                If Not lstService.Contains(objRow.Cells("Dịch vụ").Value) Then
                    lstService.Add(objRow.Cells("Dịch vụ").Value)
                End If
                decVat = decVat + objRow.Cells("Thuế GTGT").Value
                decVatable4Service = decVatable4Service + objRow.Cells("Chi phí giao dịch").Value
                decVat4Sf = decVat4Sf + objRow.Cells("Thuế GTGT  (Phí dịch vụ)").Value
                decVatable4Sf = decVatable4Sf + objRow.Cells("Phí dịch vụ").Value

                strGetTktRecord = "Select top 1 RcpId from DuToan_Tour where Status='RR' and Tcode='" _
                      & objRow.Cells("Mã giao dịch").Value & "'"
                tblEachTkt = GetDataTable(strGetTktRecord, Conn)
                If tblEachTkt.Rows.Count = 0 Then
                    MsgBox("unable to find RcpId for " & objRow.Cells("Mã giao dịch").Value & " in RAS")
                    Me.Dispose()
                    Return False
                End If
                mtblTkts.Rows.Add(0, tblEachTkt.Rows(0)("RcpId"))
            End If
        Next

        decVatable4Service = Math.Round(decVatable4Service)
        decVat = Math.Round(decVat)
        intVatPct = Math.Round(decVat * 100 / decVatable4Service)
        decVatable4Sf = Math.Round(decVatable4Sf)
        decVat4Sf = Math.Round(decVat4Sf)
        intVatPct = Math.Round(decVat * 100 / decVatable4Service)

        Select Case intVatPct
            Case 0, 5, 10
            Case Else
                MsgBox("VAT Rate must be in 0%/5%/10%")
                Return False
        End Select

        If decVatable4Service > 0 Then
            cboSRV.Text = "S"
        Else
            cboSRV.Text = "R"
        End If

        If lstService.Contains("Conf.Equipments") _
            Or lstService.Contains("Accommodations") _
            Or lstService.Contains("Conf.Room") Then
            strDesc = "Chi phí hội nghị + ăn uống "
        Else
            strDesc = "Dịch vụ ăn uống "
        End If

        If intMonth = 12 Then
            strDesc = strDesc & "T" & intMonth & "/" & Now.Year - 1
        Else
            strDesc = strDesc & "T" & intMonth & "/" & Now.Year
        End If

        If decVatable4Service > 0 Then
            AddRow4E_Inv(strDesc, "DOM", decVatable4Service, True, decVat, intVatPct,,,, blnVat8)

        End If

        If decVatable4Sf > 0 Then
            AddRow4E_Inv("Phí dịch vụ", "DOM", decVatable4Sf, True, decVat4Sf, intVatPct4Sf,,,, blnVat8)
        End If
        SumGrandTotal()
        AdjustSrv4E_Inv()

    End Function
    Public Function LoadGridTktsAir4EVN_SPC(dgrTktListing As DataGridView, strCustShortName As String _
                                            , intInvNo As Integer, intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim intVatPct As Integer
        mstrProduct = "AIR"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        If myStaff.City = "HAN" Then
            LoadE_InvSettings("", "PAX", "TVTR HAN")
        ElseIf myStaff.City = "SGN" Then
            LoadE_InvSettings("", "PAX", "TVTR")
        End If
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Cells("INV No").Value = intInvNo Then
                Dim decFTC As Decimal
                Dim decVat As Decimal
                Dim tblEachTkt As DataTable
                Dim strDomInt As String
                Dim strSrv As String

                intVatPct = Math.Round(objRow.Cells("TotaLVAT").Value * 100 / objRow.Cells("Total before VAT").Value)
                Select Case intVatPct
                    Case 0, 5, 10, 7, 3.5, 8
                    Case Else
                        MsgBox("VAT Rate must be in 0%/5%/10%/7%/3.5%/8%")
                        Return False
                End Select

                If intVatPct <> 0 Then
                    strDomInt = "DOM"
                Else
                    strDomInt = "INT"
                End If

                decVat = Math.Round(objRow.Cells("TotaLVAT").Value)

                If objRow.Cells("GIÁ VÉ").Value > 0 Then
                    strSrv = "S"
                Else
                    strSrv = "R"
                End If
                decFTC = objRow.Cells("Total before VAT").Value

                If objRow.Cells("GIÁ VÉ").Value > 0 Then
                    AddRow4E_Inv("Vé máy bay " & objRow.Cells("HÀNH TRÌNH").Value _
                                     & " " & objRow.Cells("SỐ VÉ").Value _
                               , strDomInt, decFTC, True, decVat, intVatPct, "Vé", 1, intVatDiscount)

                Else
                    AddRow4E_Inv("Hoàn Vé máy bay " & objRow.Cells("HÀNH TRÌNH").Value _
                                     & " " & objRow.Cells("SỐ VÉ").Value _
                               , strDomInt, decFTC, True, decVat, intVatPct, "Vé", 1, intVatDiscount)
                End If

                Dim strGetTktRecord As String
                strGetTktRecord = "Select top 1 RecId,RcpId,Tkno from tkt where Tkno='" _
                      & objRow.Cells("SỐ VÉ").Value _
                      & "' and SRV='" & strSrv _
                      & "' and DOI ='" & CreateFromDate(objRow.Cells("NGÀY XUẤT").Value) _
                      & "' order by RecId desc"

                tblEachTkt = GetDataTable(strGetTktRecord, Conn)
                If tblEachTkt.Rows.Count = 0 Then
                    MsgBox("unable to find " & objRow.Cells("SỐ VÉ").Value & " in RAS")
                    Me.Dispose()
                    Return False
                End If
                mtblTkts.Rows.Add(tblEachTkt.Rows(0)("RecId"), tblEachTkt.Rows(0)("RcpId"))
            End If
        Next
        SumGrandTotal()
        AdjustSrv4E_Inv()

    End Function
    Public Function LoadGridTktsAir4TVS(dgrTktListing As DataGridView, strCustShortName As String _
                                            , intVatDiscount As Integer, strSheetName As String) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim intVatPct As Integer
        mstrProduct = "AIR"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        If myStaff.City = "HAN" Then
            LoadE_InvSettings("", "PAX", "TVTR HAN")
        ElseIf myStaff.City = "SGN" Then
            LoadE_InvSettings("", "PAX", "TVTR")
        End If
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))
        cboBU.SelectedIndex = cboBU.FindStringExact("TVS")
        cboDomInt.SelectedIndex = cboDomInt.FindStringExact(strSheetName)
        For Each objRow As DataGridViewRow In dgrTktListing.Rows

            Dim decFTC As Decimal
            Dim decVat As Decimal
            Dim tblEachTkt As DataTable
            Dim strDomInt As String
            Dim strSrv As String

            intVatPct = Math.Round(objRow.Cells("VAT").Value * 100 / objRow.Cells("Total " & vbLf & "Before VAT").Value)
            Select Case intVatPct
                Case 0, 5, 10, 7, 3.5, 8
                Case Else
                    MsgBox("VAT Rate must be in 0%/5%/10%/7%/3.5%/8%")
                    Return False
            End Select

            If intVatPct <> 0 Then
                strDomInt = "DOM"
            Else
                strDomInt = "INT"
            End If

            decVat = Math.Round(objRow.Cells("VAT").Value)

            If objRow.Cells("Total " & vbLf & "Before VAT").Value > 0 Then
                strSrv = "S"
            Else
                strSrv = "R"
            End If
            decFTC = objRow.Cells("Total " & vbLf & "Before VAT").Value

            If decFTC > 0 Then
                AddRow4E_Inv("Vé máy bay " & objRow.Cells("Tkno").Value _
                               , strDomInt, decFTC, True, decVat, intVatPct, "Vé", 1, intVatDiscount)

            End If

            Dim strGetTktRecord As String
            strGetTktRecord = "Select top 1 RecId,RcpId,Tkno from tkt where Tkno='" _
                      & objRow.Cells("Tkno").Value _
                      & "' and SRV='" & strSrv _
                      & "' and DOI ='" & CreateFromDate(objRow.Cells("DOI").Value) _
                      & "' order by RecId desc"

            tblEachTkt = GetDataTable(strGetTktRecord, Conn)
            If tblEachTkt.Rows.Count = 0 Then
                MsgBox("unable to find " & objRow.Cells("Tkno").Value & " in RAS")
                Me.Dispose()
                Return False
            End If
            mtblTkts.Rows.Add(tblEachTkt.Rows(0)("RecId"), tblEachTkt.Rows(0)("RcpId"))

        Next
        SumGrandTotal()
        AdjustSrv4E_Inv()

    End Function
    Public Function LoadGridTktsAir4JCI(dgrTktListing As DataGridView, strCustShortName As String, strDomInt As String _
                                        , intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable

        Dim tblEachTkt As DataTable
        Dim strSrv As String

        Select Case UCase(InputBox("Sale or Refund (S or R)",, "S"))
            Case "S"
                strSrv = "S"
            Case "R"
                strSrv = "R"
            Case Else
                Me.Dispose()
        End Select

        mstrProduct = "AIR"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False

        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))


        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            Dim decFare As Decimal
            Dim decVat4Fare As Decimal
            Dim intVatPct4Fare As Integer
            Dim blnGetThisTkt As Boolean = False
            Dim intVatPct4Sf As Integer
            Dim blnGetThisSf As Boolean = False
            Dim strDescTkt As String

            If objRow.Cells("SRV").Value = strSrv Then
                If objRow.Cells("SRV").Value = "S" Then
                    'If strDomInt = "DOM" Then
                    decFare = objRow.Cells("Net Fare").Value
                    'Else
                    '    decFare = objRow.Cells("Net Fare").Value + objRow.Cells("Airline Charge").Value
                    'End If

                    'If strDomInt = "DOM" Then
                    decVat4Fare = objRow.Cells("VAT4FARE").Value
                    'Else
                    '    decVat4Fare = objRow.Cells("VAT").Value
                    'End If
                    If decFare = 0 Then
                        MsgBox("Không xuất hóa đơn được cho vé " & objRow.Cells("VAT4FARE").Value _
                               & " vì không xác định được mức thuế VAT")
                        Continue For
                    Else
                        intVatPct4Fare = Math.Round(decVat4Fare * 100 / decFare)
                    End If

                    blnGetThisTkt = GetThisTransaction(intVatDiscount, intVatPct4Fare)
                    strDescTkt = "Vé máy bay " & objRow.Cells("TKNO").Value & "||" & ConvertItinerary4FullName(objRow.Cells("Itinerary").Value) _
                                         & "||" & objRow.Cells("Pax Name").Value
                    If blnGetThisTkt Then
                        AddRow4E_Inv(strDescTkt, strDomInt, decFare, True, decVat4Fare,, "Vé", 1, intVatDiscount)
                        strDescTkt = ""
                        If objRow.Cells("AL Charge").Value <> 0 Then
                            AddRow4E_Inv("Phí khác", strDomInt, objRow.Cells("AL Charge").Value, True, objRow.Cells("VAT AL Charge").Value,, "Vé", 1, intVatDiscount)
                        End If

                        If objRow.Cells("Tax").Value <> 0 Then
                            'If strDomInt = "DOM" Then
                            AddRow4E_Inv("Các khoản thu hộ khác", strDomInt, objRow.Cells("Tax").Value, True, objRow.Cells("VAT Tax").Value,, "Vé", 1, intVatDiscount)
                            'Else
                            '    AddRow4E_Inv("Các khoản thu hộ khác", strDomInt, objRow.Cells("Other charges").Value, True, 0,, "Vé", 1)
                            'End If
                        End If
                    End If
                Else 've hoan
                    If strDomInt = "DOM" Then
                        decFare = objRow.Cells("Net Fare").Value
                        decVat4Fare = objRow.Cells("VAT4FARE").Value
                    Else
                        decFare = objRow.Cells("Net Fare").Value
                        decVat4Fare = objRow.Cells("VAT4FARE").Value
                    End If
                    strDescTkt = "Hoàn vé máy bay " & objRow.Cells("TKNO").Value & "||" & ConvertItinerary4FullName(objRow.Cells("Itinerary").Value) _
                                         & "||" & objRow.Cells("Pax Name").Value
                    If Not pblnTT78 Then
                        AddRow4E_Inv(strDescTkt, strDomInt, decFare, True, decVat4Fare,, "Vé", 1,)
                        strDescTkt = ""
                    End If

                    If strDomInt = "DOM" Then
                        If intVatDiscount = 0 Then
                            AddRow4E_Inv("Phí hoàn vé", strDomInt, objRow.Cells("AL Charge").Value, True, objRow.Cells("VAT AL Charge").Value, -1, "Vé", 1,)
                        End If

                    Else
                        If intVatDiscount = 0 Then
                            AddRow4E_Inv("Hoàn các khoản thu hộ khác", strDomInt, objRow.Cells("Tax").Value, True, 0,, "Vé", 1,)
                        End If
                    End If
                End If

                If objRow.Cells("Service Fee (No VAT)").Value <> 0 Then
                    intVatPct4Sf = CalcVatPctNearest(objRow.Cells("Service Fee (No VAT)").Value, objRow.Cells("VAT (SF)").Value)
                    blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                    If blnGetThisSf Then
                        'AddRow4E_Inv("Phí dịch vụ ", "DOM", objRow.Cells("Service Fee (No VAT)").Value, True, objRow.Cells("VAT (SF)").Value, intVatPct4Sf, "Lần", 1, intVatDiscount, blnVat8)
                        AddRow4E_Inv("Phí dịch vụ " & strDescTkt, "DOM", objRow.Cells("Service Fee (No VAT)").Value, True, objRow.Cells("VAT (SF)").Value, intVatPct4Sf, "Lần", 1, intVatDiscount)
                    End If
                End If

                Dim strGetTktRecord As String
                strGetTktRecord = "Select top 1 RecId,RcpId,Tkno from tkt where Tkno='" _
                          & objRow.Cells("TKNO").Value _
                          & "' and SRV='" & objRow.Cells("SRV").Value _
                          & "' and DOI ='" & CreateFromDate(objRow.Cells("DOI").Value) _
                          & "' order by RecId desc"

                tblEachTkt = GetDataTable(strGetTktRecord, Conn)
                If tblEachTkt.Rows.Count = 0 Then
                    MsgBox("unable to find " & objRow.Cells("TKNO").Value & " in RAS")
                    Me.Dispose()
                    Return False
                End If
                mtblTkts.Rows.Add(tblEachTkt.Rows(0)("RecId"), tblEachTkt.Rows(0)("RcpId"))
            End If
        Next
        SumGrandTotal()

        AdjustSrv4E_Inv()

    End Function
    Public Function LoadGridTktsAirHogan(dgrTktListing As DataGridView, strCustShortName As String, strDomInt As String _
                                           , intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable

        mstrProduct = "AIR"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "GDS")
        SelectVatDiscountLevel(intVatDiscount)

        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))

        ' , "Thuế GTGT" & vbLf & "(VAT) của Phí đổi/Phí mua hàng lý" _
        ' , "Phí hoàn" & vbLf & "(penalty)", "Tổng cộng" & vbLf & "(Total amount)" _
        ',
        ',
        ', "Tổng tiền" & vbLf & "thanh toán" & vbLf & "(Payable Amount)" _
        ', "Giá phòng " & vbLf & "(Room Rate)" _
        ', "Tổng tiền thanh toán" & vbLf & "(Payable Amount)"}

        AddRow4E_InvAirHogan(dgrTktListing, strDomInt, "Số vé" & vbLf & "(Tkt No)" _
                            , "Hành trình" & vbLf & "(Route)" _
                            , "Giá vé " & vbLf & "(Fare)", "Thuế GTGT" & vbLf & "(VAT)" _
                            , "Phí đổi/Phí mua hành lí" & vbLf & "(Charge)" _
                            , "Thuế GTGT" & vbLf & "(VAT) của Phí đổi/Phí mua hàng lý" _
                            , "Thuế khác" & vbLf & "(Tax)", "Thuế GTGT" & vbLf & "(VAT) của Thuế khác" _
                            , "Phí dịch vụ" & vbLf & "(Sv.Fee)" _
                            , "Thuế GTGT" & vbLf & "(VAT) của Phí dịch vụ" _
                            , "Tên khách" & vbLf & "(Traveller Name)" _
                            , "Ngày bay" & vbLf & "(Dep. date )" _
                            , "Ngày đến" & vbLf & "(Arv. date)", intVatDiscount)


        SumGrandTotal()

        AdjustSrv4E_Inv()

    End Function
    Public Function LoadGridTktsAirGeneric(dgrTktListing As DataGridView, strCustShortName As String, strDomInt As String _
                                           , intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim strFareColumnName As String,
            mVATALCharge As String  '^_^20220803 add by 7643

        mstrProduct = "AIR"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")
        SelectVatDiscountLevel(intVatDiscount)

        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))
        Select Case strCustShortName
            Case "ATOTECH"
                If strDomInt = "DOM" Then
                    strFareColumnName = "FareInVND"
                Else
                    strFareColumnName = "Net Fare"
                End If
                AddRow4E_InvAirGeneric(dgrTktListing, strDomInt, strFareColumnName, "VAT4FARE", "AL Charge", "VAT AL Charge" _
                            , "Tax", "VAT Tax", "Service Fee (No VAT)", "VAT (SF)", "Pax Name", "Dep Date", intVatDiscount)
            Case "INGER"
                If strDomInt = "DOM" Then
                    strFareColumnName = "FareInVND"
                Else
                    strFareColumnName = "Net Fare"
                End If
                AddRow4E_InvAirGeneric(dgrTktListing, strDomInt, strFareColumnName, "VAT4FARE", "AL Charge", "VAT AL Charge" _
                            , "Tax", "VAT Tax", "Service Fee (No VAT)", "VAT (SF)", "", "", intVatDiscount)
                '^_^20220802 add by 7643 -b-
            Case "HASBRO"
                mVATALCharge = IIf(strDomInt = "DOM", "VAT AL Charge", "")
                AddRow4E_InvAirGeneric(dgrTktListing, strDomInt, "Net Fare", "VAT4FARE", "AL Charge", mVATALCharge _
                            , "Tax", "VAT Tax", "Service Fee (No VAT)", "VAT (SF)", "Pax Name", "", intVatDiscount, "MerchantFeeNoVAT" _
                            , "VAT4MerchantFee")
                '^_^20220802 add by 7643 -e-
        End Select


        SumGrandTotal()

        AdjustSrv4E_Inv()

    End Function
    '^_^20220802 mark by 7643 -b-
    'Private Function AddRow4E_InvAirGeneric(dgrTktListing As DataGridView, strDomInt As String _
    '                                     , strFareColumn As String, strVat4FareColumn As String _
    '                                     , strAlChargeColumn As String, strVatAlChargeColumn As String _
    '                                     , strTaxColumn As String, strVatTaxColumn As String _
    '                                     , strSfColumn As String, strVatSfColumn As String _
    '                                     , strtPaxNameColum As String, strDepDateColumn As String _
    '                                     , intVatDiscount As Integer) As Boolean
    '^_^20220802 mark by 7643 -e-
    '^_^20220802 modi by 7643 -b-
    Private Function AddRow4E_InvAirHogan(dgrTktListing As DataGridView, strDomInt As String _
                                         , strTknoColumn As String, strRtgColumn As String _
                                         , strFareColumn As String, strVat4FareColumn As String _
                                         , strAlChargeColumn As String, strVatAlChargeColumn As String _
                                         , strTaxColumn As String, strVatTaxColumn As String _
                                         , strSfColumn As String, strVatSfColumn As String _
                                         , strtPaxNameColumn As String, strDepDateColumn As String _
                                         , strArrDateColumn As String, intVatDiscount As Integer) As Boolean
        '^_^20220802 modi by 7643 -e-
        Dim decFare As Decimal
        Dim decVat4Fare As Decimal
        Dim strDesc As String
        Dim intVat4Fare As Integer
        Dim intVatRate4Sf As Integer

        Dim strTkno4SF As String = String.Empty
        Dim blnGetThisTkt As Boolean
        Dim blnGetThisSf As Boolean

        Dim strSrv As String = "S"  'Tam gán là S vì chưa có ví dụ phân biệt với R

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            blnGetThisTkt = False
            blnGetThisSf = False
            strDesc = ""
            If objRow.Cells(strSfColumn).Value <> 0 Then
                intVatRate4Sf = Math.Round(objRow.Cells(strVatSfColumn).Value / objRow.Cells(strSfColumn).Value * 100)
                blnGetThisSf = GetThisTransaction(intVatDiscount, intVatRate4Sf)
            End If
            If strSrv = "S" Then

                'If strDomInt = "DOM" Then
                '    decFare = objRow.Cells(strFareColumn).Value
                'Else
                decFare = objRow.Cells(strFareColumn).Value + objRow.Cells(strAlChargeColumn).Value _
                    + objRow.Cells(strTaxColumn).Value
                'End If
                If strDomInt = "DOM" Then
                    decVat4Fare = objRow.Cells(strVat4FareColumn).Value + objRow.Cells(strVatAlChargeColumn).Value _
                        + objRow.Cells(strVatTaxColumn).Value
                End If

                intVat4Fare = Math.Round(decVat4Fare * 100 / decFare)

                blnGetThisTkt = GetThisTransaction(intVatDiscount, intVat4Fare)
                If Not blnGetThisTkt Then
                    strTkno4SF = objRow.Cells(strTknoColumn).Value
                Else
                    strTkno4SF = ""
                    strDesc = "Vé máy bay" _
                        & "||" & objRow.Cells(strTknoColumn).Value _
                        & "||" & objRow.Cells(strtPaxNameColumn).Value _
                        & "||" & objRow.Cells(strRtgColumn).Value

                    strDesc = strDesc & "||Ngày bay " & Format(objRow.Cells(strDepDateColumn).Value, "dd/MM/yyyy")

                    If IsDate(objRow.Cells(strArrDateColumn).Value) Then
                        strDesc = strDesc & "||Ngày đến " & Format(objRow.Cells(strArrDateColumn).Value, "dd/MM/yyyy")
                    End If

                    AddRow4E_Inv(strDesc, strDomInt, decFare, True, decVat4Fare,, "Vé", 1, intVatDiscount)
                End If


            Else    've refund
                If strDomInt = "DOM" Then
                    decFare = objRow.Cells(strFareColumn).Value
                    decVat4Fare = objRow.Cells(strVat4FareColumn).Value
                Else
                    decFare = objRow.Cells(strFareColumn).Value
                    decVat4Fare = 0
                End If

                strDesc = "Hoàn vé máy bay " & objRow.Cells(strTknoColumn).Value _
                    & "||" & objRow.Cells(strRtgColumn).Value
                If strtPaxNameColumn <> "" Then
                    strDesc = strDesc & "||" & objRow.Cells(strtPaxNameColumn).Value
                End If

                If blnGetThisSf Or (objRow.Cells(strAlChargeColumn).Value <> 0 AndAlso intVatDiscount = 0) Then
                    AddRow4E_Inv(strDesc, strDomInt, decFare, True, decVat4Fare,, "Vé", 1, intVatDiscount, 4)
                End If

                If strDomInt = "DOM" Then
                    If objRow.Cells(strTaxColumn).Value <> 0 Then
                        AddRow4E_Inv("Hoàn các khoản thu hộ khác", strDomInt, objRow.Cells(strTaxColumn).Value, True, objRow.Cells(strVatTaxColumn).Value,, "Vé", 1, intVatDiscount)
                    End If
                    If objRow.Cells(strAlChargeColumn).Value <> 0 AndAlso intVatDiscount = 0 Then
                        AddRow4E_Inv("Phí hoàn vé", strDomInt, objRow.Cells(strAlChargeColumn).Value, True, objRow.Cells(strVatAlChargeColumn).Value, -1, "Vé", 1, intVatDiscount)
                    End If

                Else
                    If objRow.Cells(strTaxColumn).Value <> 0 Then
                        AddRow4E_Inv("Hoàn các khoản thu hộ khác", strDomInt, objRow.Cells(strTaxColumn).Value, True, 0,, "Vé", 1, intVatDiscount)
                    End If
                    If objRow.Cells(strAlChargeColumn).Value <> 0 AndAlso intVatDiscount = 0 Then
                        AddRow4E_Inv("Phí hoàn vé", strDomInt, objRow.Cells(strAlChargeColumn).Value, True, 0, -1, "Vé", 1, intVatDiscount)
                    End If

                End If
            End If

            If blnGetThisSf Then
                strDesc = ("Phí dịch vụ " & strTkno4SF).Trim
                AddRow4E_Inv(strDesc, "DOM", objRow.Cells(strSfColumn).Value, True, objRow.Cells(strVatSfColumn).Value,, "Lần", 1, intVatDiscount)
            End If
        Next

        Return True
    End Function
    Private Function AddRow4E_InvAirGeneric(dgrTktListing As DataGridView, strDomInt As String _
                                         , strFareColumn As String, strVat4FareColumn As String _
                                         , strAlChargeColumn As String, strVatAlChargeColumn As String _
                                         , strTaxColumn As String, strVatTaxColumn As String _
                                         , strSfColumn As String, strVatSfColumn As String _
                                         , strtPaxNameColum As String, strDepDateColumn As String _
                                         , intVatDiscount As Integer, Optional strMerchanColumn As String = "" _
                                         , Optional strVatMerchanColumn As String = "") As Boolean
        '^_^20220802 modi by 7643 -e-
        Dim decFare As Decimal
        Dim decVat4Fare As Decimal
        Dim strDesc As String
        Dim intVat4Fare As Integer
        Dim intVatRate4Sf As Integer

        Dim strTkno4SF As String = String.Empty
        Dim blnGetThisTkt As Boolean
        Dim blnGetThisSf As Boolean
        Dim blnGetThisFee As Boolean

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            blnGetThisTkt = False
            blnGetThisSf = False
            If objRow.Cells(strSfColumn).Value <> 0 Then
                intVatRate4Sf = Math.Round(objRow.Cells(strVatSfColumn).Value / objRow.Cells(strSfColumn).Value * 100)
                blnGetThisSf = GetThisTransaction(intVatDiscount, intVatRate4Sf)
            End If
            If objRow.Cells("SRV").Value = "S" Then
                'If strDomInt = "DOM" Then
                '    decFare = objRow.Cells(strFareColumn).Value
                'Else
                decFare = objRow.Cells(strFareColumn).Value
                'End If
                If strDomInt = "DOM" Then
                    decVat4Fare = objRow.Cells(strVat4FareColumn).Value
                End If

                intVat4Fare = Math.Round(decVat4Fare * 100 / decFare)

                blnGetThisTkt = GetThisTransaction(intVatDiscount, intVat4Fare)
                If Not blnGetThisTkt Then
                    strTkno4SF = objRow.Cells("TKNO").Value
                Else
                    strTkno4SF = ""
                    strDesc = "Vé máy bay " & objRow.Cells("TKNO").Value & "||" & objRow.Cells("Itinerary").Value

                    If strtPaxNameColum <> "" Then
                        strDesc = strDesc & "||" & objRow.Cells(strtPaxNameColum).Value
                    End If

                    If strDepDateColumn <> "" Then
                        strDesc = strDesc & "||" & objRow.Cells(strDepDateColumn).Value
                    End If
                    '^_^20220803 mark by 7643 -b-
                    'AddRow4E_Inv(strDesc, strDomInt, decFare, True, decVat4Fare,, "Vé", 1, intVatDiscount)

                    'If objRow.Cells(strAlChargeColumn).Value <> 0 Then
                    '    AddRow4E_Inv("Phí khác", strDomInt, objRow.Cells(strAlChargeColumn).Value, True, objRow.Cells(strVatAlChargeColumn).Value,, "Vé", 1, intVatDiscount)
                    'End If
                    '^_^20220803 mark by 7643 -e-
                    '^_^20220803 modi by 7643 -b-
                    If txtCustShortName.Text = "HASBRO" Then
                        If objRow.Cells(strAlChargeColumn).Value <> 0 Then
                            decFare += objRow.Cells(strAlChargeColumn).Value
                            If strVatAlChargeColumn <> "" Then decVat4Fare += objRow.Cells(strVatAlChargeColumn).Value
                        End If
                        AddRow4E_Inv(strDesc, strDomInt, decFare, True, decVat4Fare,, "Vé", 1, intVatDiscount)
                    Else
                        AddRow4E_Inv(strDesc, strDomInt, decFare, True, decVat4Fare,, "Vé", 1, intVatDiscount)

                        If objRow.Cells(strAlChargeColumn).Value <> 0 Then
                            AddRow4E_Inv("Phí khác", strDomInt, objRow.Cells(strAlChargeColumn).Value, True, objRow.Cells(strVatAlChargeColumn).Value,, "Vé", 1, intVatDiscount)
                        End If
                    End If
                    '^_^20220803 modi by 7643 -e-

                    If objRow.Cells(strTaxColumn).Value <> 0 Then
                        If strDomInt = "DOM" Then
                            AddRow4E_Inv("Các khoản thu hộ khác", strDomInt, objRow.Cells(strTaxColumn).Value, True, objRow.Cells(strVatTaxColumn).Value,, "Lần", 1, intVatDiscount)
                        Else
                            AddRow4E_Inv("Các khoản thu hộ khác", strDomInt, objRow.Cells("Tax").Value, True, 0,, "Lần", 1, intVatDiscount)
                        End If
                    End If

                    '^_^20220802 add by 7643 -b-
                    If txtCustShortName.Text = "HASBRO" And strMerchanColumn <> "" AndAlso objRow.Cells(strMerchanColumn).Value <> 0 Then
                        AddRow4E_InvHASBRO("Phí cà thẻ", strDomInt, objRow.Cells(strMerchanColumn).Value, True, objRow.Cells(strVatMerchanColumn).Value,, "Lần", 1, intVatDiscount)
                    End If
                    '^_^20220802 add by 7643 -e-
                End If


            Else    've refund
                If strDomInt = "DOM" Then
                    decFare = objRow.Cells(strFareColumn).Value
                    decVat4Fare = objRow.Cells(strVat4FareColumn).Value
                Else
                    decFare = objRow.Cells(strFareColumn).Value
                    decVat4Fare = 0
                End If

                strDesc = "Hoàn vé máy bay " & objRow.Cells("TKNO").Value & "||" & objRow.Cells("Itinerary").Value
                If strtPaxNameColum <> "" Then
                    strDesc = strDesc & "||" & objRow.Cells(strtPaxNameColum).Value
                End If

                If blnGetThisSf Or (objRow.Cells(strAlChargeColumn).Value <> 0 AndAlso intVatDiscount = 0) Then
                    AddRow4E_Inv(strDesc, strDomInt, decFare, True, decVat4Fare,, "Vé", 1, intVatDiscount, 4)
                End If

                If strDomInt = "DOM" Then
                    If objRow.Cells(strTaxColumn).Value <> 0 Then
                        AddRow4E_Inv("Hoàn các khoản thu hộ khác", strDomInt, objRow.Cells(strTaxColumn).Value, True, objRow.Cells(strVatTaxColumn).Value,, "Vé", 1, intVatDiscount)
                    End If
                    If objRow.Cells(strAlChargeColumn).Value <> 0 AndAlso intVatDiscount = 0 Then
                        AddRow4E_Inv("Phí hoàn vé", strDomInt, objRow.Cells(strAlChargeColumn).Value, True, objRow.Cells(strVatAlChargeColumn).Value, -1, "Vé", 1, intVatDiscount)
                    End If

                Else
                    If objRow.Cells(strTaxColumn).Value <> 0 Then
                        AddRow4E_Inv("Hoàn các khoản thu hộ khác", strDomInt, objRow.Cells(strTaxColumn).Value, True, 0,, "Vé", 1, intVatDiscount)
                    End If
                    If objRow.Cells(strAlChargeColumn).Value <> 0 AndAlso intVatDiscount = 0 Then
                        AddRow4E_Inv("Phí hoàn vé", strDomInt, objRow.Cells(strAlChargeColumn).Value, True, 0, -1, "Vé", 1, intVatDiscount)
                    End If

                End If
            End If

            If blnGetThisSf Then
                strDesc = ("Phí dịch vụ " & strTkno4SF).Trim
                AddRow4E_Inv(strDesc, "DOM", objRow.Cells(strSfColumn).Value, True, objRow.Cells(strVatSfColumn).Value,, "Lần", 1, intVatDiscount)
            End If
        Next

        Return True
    End Function
    Public Function LoadGridTktsAir4NGHISON(dgrTktListing As DataGridView, strCustShortName As String _
                                            , strDomInt As String _
            , intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim intVatPct As Integer
        Dim blnGetThisTkt As Boolean
        Dim intVatPct4Sf As Integer
        Dim blnGetThisSf As Boolean
        Dim blnGetThuHo As Boolean
        Dim blnGetAlCharge As Boolean
        Dim tblEachTkt As DataTable
        Dim strDescTkt As String

        mstrProduct = "AIR"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")

        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))
        SelectVatDiscountLevel(intVatDiscount)

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            Dim decFare As Decimal = 0
            Dim decVat4Fare As Decimal = 0
            Dim decPhiHoan As Decimal = 0

            If objRow.Cells("Service Type").Value = "S" Then
                If strDomInt = "DOM" Then
                    decFare = objRow.Cells("Airfare").Value + objRow.Cells("Airline Charge").Value
                Else
                    decFare = objRow.Cells("Amount").Value + objRow.Cells("Airline Charge").Value
                End If

                If strDomInt = "DOM" Then
                    decVat4Fare = objRow.Cells("VAT of Air fare").Value + objRow.Cells("VAT of Airline Charge").Value
                Else
                    decVat4Fare = objRow.Cells("VAT").Value
                End If

                intVatPct = CalcVatPctNearest(decFare, decVat4Fare)
                blnGetThisTkt = GetThisTransaction(intVatDiscount, intVatPct)


                strDescTkt = "Vé máy bay " & objRow.Cells("Ticket No").Value & "||" & ConvertItinerary4FullName(objRow.Cells("Itinerary").Value) _
                                     & "||" & objRow.Cells("Guest's Name").Value
                If blnGetThisTkt Then

                    AddRow4E_Inv(strDescTkt, strDomInt, decFare, True, decVat4Fare,, "Vé", 1, intVatDiscount)

                    If objRow.Cells("Other charges").Value <> 0 Then
                        If strDomInt = "DOM" Then
                            AddRow4E_Inv("Các khoản thu hộ khác", strDomInt, objRow.Cells("Other charges").Value, True, objRow.Cells("VAT of other charges").Value,, "Lần", 1, intVatDiscount)
                        Else
                            AddRow4E_Inv("Các khoản thu hộ khác", strDomInt, objRow.Cells("Other charges").Value, True, 0,, "Lần", 1, intVatDiscount)
                        End If
                    End If
                End If
            Else    've hoan
                If strDomInt = "DOM" Then
                    decFare = objRow.Cells("Airfare").Value
                    decVat4Fare = objRow.Cells("VAT of Air fare").Value
                Else
                    decFare = objRow.Cells("Amount").Value
                    decVat4Fare = objRow.Cells("VAT").Value
                End If
                strDescTkt = "Hoàn vé máy bay " & objRow.Cells("Ticket No").Value & "||" & ConvertItinerary4FullName(objRow.Cells("Itinerary").Value) _
                                     & "||" & objRow.Cells("Guest's Name").Value

                If intVatDiscount = 0 Then
                    If strDomInt = "DOM" Then
                        AddRow4E_Inv("Phí hoàn vé" & " " & strDescTkt, strDomInt, objRow.Cells("Airline Charge").Value, True, objRow.Cells("VAT of Airline Charge").Value, -1, "Vé", 1,)
                    Else
                        AddRow4E_Inv("Phí hoàn vé" & " " & strDescTkt, strDomInt, objRow.Cells("Airline Charge").Value, True, 0, -1, "Vé", 1,)
                    End If
                End If
            End If

            If objRow.Cells("Service fee").Value <> 0 Then
                intVatPct4Sf = CalcVatPctNearest(objRow.Cells("Service fee").Value, objRow.Cells("VAT of service fee").Value)
                blnGetThisSf = GetThisTransaction(intVatDiscount, intVatPct4Sf)
                If blnGetThisSf Then
                    Dim strDesc As String = String.Empty
                    If pblnTT78 Then
                        If objRow.Cells("Service Type").Value = "S" Then
                            strDesc = "Phần thu dịch vụ bán vé"
                        Else
                            strDesc = "Phần thu dịch vụ hoàn vé" & " " & strDescTkt
                        End If
                    Else
                        If blnGetThisTkt Then
                            strDesc = "Phần thu dịch vụ bán vé"
                        Else
                            strDesc = "Phần thu dịch vụ bán vé " & objRow.Cells("Ticket No").Value
                        End If
                    End If
                    AddRow4E_Inv(strDesc, "DOM", objRow.Cells("Service fee").Value, True, objRow.Cells("VAT Of service fee").Value, intVatPct4Sf, "Lần", 1, intVatDiscount)
                End If
            End If

            Dim strGetTktRecord As String
            strGetTktRecord = "Select top 1 RecId, RcpId, Tkno from tkt where Tkno='" _
                      & objRow.Cells("Ticket No").Value _
                      & "' and SRV='" & objRow.Cells("Service Type").Value _
                      & "' and DOI ='" & CreateFromDate(objRow.Cells("Date").Value) _
                      & "' order by RecId desc"

            tblEachTkt = GetDataTable(strGetTktRecord, Conn)
            If tblEachTkt.Rows.Count = 0 Then
                MsgBox("unable to find " & objRow.Cells("Ticket No").Value & " in RAS")

                Return False
            End If
            mtblTkts.Rows.Add(tblEachTkt.Rows(0)("RecId"), tblEachTkt.Rows(0)("RcpId"))

        Next
        SumGrandTotal()

        AdjustSrv4E_Inv()

    End Function
    Public Function LoadGridTktsAir4THANHHOANG_VJ(dgrTktListing As DataGridView _
                                                  , strCustShortName As String _
                                                  , intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim intVatPct As Integer
        Dim strDomInt As String
        mstrProduct = "AIR"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        LoadE_InvSettings("", "PAX", "TVTR")
        SelectVatDiscountLevel(intVatDiscount)
        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))

        For Each objRow As DataGridViewRow In dgrTktListing.Rows


            Dim strSrv As String
            intVatPct = Math.Round(objRow.Cells("Thuế GTGT").Value * 100 / objRow.Cells("Giá chưa thuế").Value)
            Select Case intVatPct
                Case 0, 5, 10, 8
                    If intVatDiscount <> 0 Then
                        MsgBox("% VAT bị sai!")
                        Return False
                    End If

                Case 7, 3.5
                    If intVatDiscount <> 30 Then
                        MsgBox("% VAT bị sai!")
                        Return False
                    End If
                Case Else

                    Return False
            End Select

            If intVatPct <> 0 Then
                strDomInt = "DOM"
            Else
                strDomInt = "INT"
            End If


            If objRow.Cells("Giá chưa thuế").Value > 0 Then
                strSrv = "S"
            Else
                strSrv = "R"
            End If

            If strSrv = "S" Then
                AddRow4E_Inv("Vé máy bay " & objRow.Cells("Số PNR").Value _
                                     & "||" & objRow.Cells("Hành Trình").Value _
                               , strDomInt, objRow.Cells("Giá chưa thuế").Value, True, objRow.Cells("Thuế GTGT").Value, intVatPct, "Vé", 1, intVatDiscount)
            Else
                AddRow4E_Inv("Hoàn vé máy bay " & objRow.Cells("Số PNR").Value _
                                     & "||" & objRow.Cells("Hành Trình").Value _
                               , strDomInt, objRow.Cells("Giá chưa thuế").Value, True, objRow.Cells("Thuế GTGT").Value, intVatPct,,, intVatDiscount)
            End If

        Next
        SumGrandTotal()

        AdjustSrv4E_Inv()

    End Function
    Public Function LoadGridInvFromExcel(dgrTktListing As DataGridView _
                                         , strCustShortName As String, intSequence As Integer _
                                         , intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim strTvc As String = String.Empty

        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))
        SelectVatDiscountLevel(intVatDiscount)
        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Cells("STT").Value = intSequence Then
                If strTvc = "" Then
                    strTvc = objRow.Cells("TVC").Value
                    LoadE_InvSettings("", "PAX", strTvc)
                    cboSRV.SelectedIndex = cboSRV.FindStringExact(objRow.Cells("SRV").Value)
                    cboFOP.SelectedIndex = cboFOP.FindStringExact(objRow.Cells("FOP").Value)
                    cboBU.SelectedIndex = cboBU.FindStringExact(objRow.Cells("BU").Value)
                    cboDomInt.SelectedIndex = cboDomInt.FindStringExact(objRow.Cells("DomInt").Value)
                    txtBuyer.Text = objRow.Cells("Buyer").Value
                    txtEmail.Text = IIf(objRow.Cells("Email").Value <> "", objRow.Cells("Email").Value, txtEmail.Text)  '^_^20220919 add by 7643
                End If


                If cboSRV.Text = "S" Then
                    AddRow4E_Inv(objRow.Cells("Description").Value _
                                   , objRow.Cells("DomInt").Value, objRow.Cells("Amount").Value, True, objRow.Cells("VAT").Value, objRow.Cells("VATPct").Value _
                                   , objRow.Cells("Unit").Value, objRow.Cells("Qty").Value, intVatDiscount)
                Else
                    AddRow4E_Inv(objRow.Cells("Description").Value _
                                   , objRow.Cells("DomInt").Value, objRow.Cells("Amount").Value, True, objRow.Cells("VAT").Value, objRow.Cells("VATPct").Value _
                                   , objRow.Cells("Unit").Value, objRow.Cells("Qty").Value, intVatDiscount)
                End If

                '^_^20221001 add by 7643 -b-
                If ScalarToString("MISC", "RecID", "CAT='CustNameInGroup' and VAL='MULTICUST' and IntVal=" & tblCust.Rows(0)("RecID") & " and status<>'XX'") <> "" Then
                    txtCustomerFullName.Text = objRow.Cells("Customer").Value
                End If
                '^_^20221001 add by 7643 -e-
            End If


        Next
        SumGrandTotal()

        AdjustSrv4E_Inv()

    End Function

    Private Function AdjustSrv4E_Inv()
        Dim blnHasPriceColumn As Boolean
        'xac dinh Inv total => SRV

        If dgrInvDetails.Columns.Contains("Price") Then
            blnHasPriceColumn = True
        End If
        If txtInvTotal.Text < 0 Then
            cboSRV.SelectedIndex = 1
            cboSRV.Enabled = False
            For Each objRow As DataGridViewRow In dgrInvDetails.Rows
                If blnHasPriceColumn AndAlso objRow.Cells("Price").Value IsNot Nothing Then
                    objRow.Cells("Price").Value = 0 - objRow.Cells("Price").Value
                End If

                objRow.Cells("Amount").Value = 0 - objRow.Cells("Amount").Value
                objRow.Cells("VAT").Value = 0 - objRow.Cells("VAT").Value
                objRow.Cells("Total").Value = 0 - objRow.Cells("Total").Value
            Next
            txtInvTotal.Text = Format((0 - CDec(txtInvTotal.Text)), "#,##0")

        End If
        Return True
    End Function
    Public Function LoadGridHotLineAir(dgrTktListing As DataGridView, strSheetName As String _
                                    , strCustShortName As String) As Boolean
        Dim tblCust As System.Data.DataTable

        mstrProduct = "AIR"
        ' Fill tkt datatable
        If mtblTkts.Columns.Count = 0 Then
            mtblTkts.Columns.Add("RecId")
            mtblTkts.Columns.Add("RcpId")
        End If

        If myStaff.City = "HAN" Then
            LoadE_InvSettings("", "PAX", "TVTR HAN")
        ElseIf myStaff.City = "SGN" Then
            LoadE_InvSettings("", "PAX", "TVTR")
        End If

        pnlSelectCustomer.Visible = False
        tblCust = GetDataTable("Select top 1 * from CustomerList where CustShortName='" _
                               & strCustShortName & "'")
        LoadCustomer(tblCust.Rows(0))

        For Each objRow As DataGridViewRow In dgrTktListing.Rows
            If objRow.Cells("S").Value Then
                Dim decFTC As Decimal
                Dim decVatPct As Integer
                Dim decVat As Decimal
                Dim tblEachTkt As DataTable
                Dim decSfNoVat As Decimal
                Dim decVat4Sf As Decimal

                If strSheetName.StartsWith("DOM") Or strSheetName.StartsWith("MISC") Then
                    decFTC = objRow.Cells("Net Fare").Value + Math.Round(objRow.Cells("Tax").Value) _
                        + Math.Round(objRow.Cells("AL Charge").Value) + Math.Round(objRow.Cells("Service Fee (No VAT)").Value)

                    decVatPct = 10
                    decVat = objRow.Cells("VAT4FARE").Value + Math.Round(objRow.Cells("VAT Tax").Value) _
                        + Math.Round(objRow.Cells("VAT AL Charge").Value + Math.Round(objRow.Cells("VAT (SF)").Value))
                    AddRow4E_Inv("Vé máy bay " & objRow.Cells("TKNO").Value _
                        & "-" & objRow.Cells("Pax Name").Value, "DOM", decFTC, True, decVat)

                Else
                    Dim decRoe As Decimal
                    decSfNoVat = decSfNoVat + Math.Round(objRow.Cells("Service Fee (No VAT)").Value)
                    decVat4Sf = decVat4Sf + Math.Round(objRow.Cells("VAT (SF)").Value)

                    If objRow.Cells("Currency").Value = "VND" Then
                        decRoe = 1
                    Else
                        decRoe = objRow.Cells("ROE (USD/VND)").Value
                    End If
                    decFTC = Math.Round(objRow.Cells("Net Fare").Value _
                        * decRoe) + Math.Round(objRow.Cells("AL Charge").Value _
                        * decRoe) + Math.Round(objRow.Cells("Tax").Value _
                        * decRoe)

                    decVatPct = 0
                    AddRow4E_Inv("Vé máy bay " & objRow.Cells("TKNO").Value _
                        & "-" & objRow.Cells("Pax Name").Value, "INT", decFTC, True, decVat)
                    AddRow4E_Inv("Phí dịch vụ ", "DOM", decSfNoVat, True, decVat4Sf)
                End If


                tblEachTkt = GetDataTable("Select top 1 RecId,RcpId,Tkno from tkt where Tkno='" _
                                          & objRow.Cells("TKNO").Value _
                                          & "' and SRV='" & objRow.Cells("SRV").Value _
                                          & "' and DOI ='" & CreateFromDate(objRow.Cells("DOI").Value) _
                                          & "' order by RecId desc", Conn)
                If tblEachTkt.Rows.Count = 0 Then
                    MsgBox("unable to find " & objRow.Cells("TKNO").Value & " in RAS")
                    Me.Dispose()
                    Return False
                End If
                mtblTkts.Rows.Add(tblEachTkt.Rows(0)("RecId"), tblEachTkt.Rows(0)("RcpId"))
            End If
        Next
    End Function
    Private Function HideInvDetailsColumns(blnGtgtTemplate As Boolean) As Boolean
        'dgrInvDetails.Columns("Unit").Visible = False
        dgrInvDetails.Columns("Tkno").Visible = Not (blnGtgtTemplate)
        'dgrInvDetails.Columns("Price").Visible = Not (blnGtgtTemplate)        
        dgrInvDetails.Columns("VatPct").Visible = blnGtgtTemplate
        dgrInvDetails.Columns("Vat").Visible = blnGtgtTemplate
        'dgrInvDetails.Columns("Amount").Visible = blnGtgtTemplate
        dgrInvDetails.Columns("Total").Visible = blnGtgtTemplate
        cboGiamThue.Enabled = blnGtgtTemplate
        If Not blnGtgtTemplate Then
            cboGiamThue.SelectedIndex = cboGiamThue.FindStringExact("0")
        End If
    End Function
    Public Function LoadDetailsFromRas(objSumRow As DataGridViewRow, strTkIds As String _
                                       , intCustid As Integer _
                                       , strProduct As String, strDomInt As String _
                                       , blnIssued2TV As Boolean, strTemplate As String _
                                       , intVatDiscount As Integer) As Boolean
        Dim tblCust As System.Data.DataTable
        Dim strLoadTkt As String = ""
        Dim i As Integer  '^_^20220808 add by 7643
        strLoadTkt = "Select itinerary as Rtg, * from tkt where Qty<>0 and Status<>'xx'" _
                    & " And RecId in (" & strTkIds & ")"

        mtblTkts = GetDataTable(strLoadTkt, Conn)

        cboGiamThue.SelectedIndex = cboGiamThue.FindStringExact(intVatDiscount)
        mstrProduct = strProduct

        tblCust = GetDataTable("Select * from CustomerList where RecId=" & intCustid)
        LoadCustomer(tblCust.Rows(0))

        If blnIssued2TV Then
            AutoFillTVTR(myStaff.City)
        End If
        If myStaff.City = "HAN" Then
            LoadE_InvSettings("", "PAX", "TVTR HAN")
        ElseIf myStaff.City = "SGN" Then
            LoadE_InvSettings("", "PAX", "TVTR")
        End If

        '^_^20220808 mark by 7643 -b-
        'Select Case strTemplate
        '    Case "INV_AIR1"
        '        DraftAirTemplate1(objSumRow, strProduct, strDomInt, intVatDiscount)
        '    Case "INV_AIR2"
        '        DraftAirTemplate2(objSumRow, strProduct, strDomInt, intVatDiscount)
        '    Case Else
        '        DraftAirGeneric(objSumRow, strProduct, strDomInt, intVatDiscount)
        'End Select
        '^_^20220808 mark by 7643 -e-
        '^_^20220808 modi by 7643 -b-
        If txtCustShortName.Text = "ALFA LAVAL" Then
            Select Case strTemplate
                Case "INV_AIR1"
                    DraftAirTemplate1(objSumRow, strProduct, strDomInt, intVatDiscount, 1)
                Case "INV_AIR2"
                    DraftAirTemplate2(objSumRow, strProduct, strDomInt, intVatDiscount, 1)
                Case Else
                    DraftAirGeneric(objSumRow, strProduct, strDomInt, intVatDiscount, 1)
            End Select

            For i = 0 To dgrInvDetails.Rows.Count - 1
                If dgrInvDetails.Rows(i).Cells("Description").Value.contains("ExtraRow1:") Then
                    dgrInvDetails.Rows(i).Cells("Description").Value = "TRIP PURPOSE:" &
                                                                       GetAirExtRowDetail(CInt(Split(dgrInvDetails.Rows(i).Cells("Description").Value, "ExtraRow1:")(1)), "TRIP PURPOSE")
                End If
            Next
        Else
            Select Case strTemplate
                Case "INV_AIR1"
                    DraftAirTemplate1(objSumRow, strProduct, strDomInt, intVatDiscount)
                Case "INV_AIR2"
                    DraftAirTemplate2(objSumRow, strProduct, strDomInt, intVatDiscount)
                Case "INV_AIR3"
                    DraftAirTemplate3(objSumRow, strProduct, strDomInt, intVatDiscount)
                Case Else
                    'DraftAirGeneric(objSumRow, strProduct, strDomInt, intVatDiscount)  '^_^20221003 mark by 7643
                    '^_^20221003 modi by 7643 -b-
                    If ScalarToString("MISC", "RecID", "Cat='CustNameInGroup' and Val='COMBINEAMOUNT' and IntVal=" & intCustid & " and Status<>'XX'") <> "" Then
                        DraftAirCombineAmount(objSumRow, strProduct, strDomInt, intVatDiscount)
                    Else
                        DraftAirGeneric(objSumRow, strProduct, strDomInt, intVatDiscount)
                    End If
                    '^_^20221003 modi by 7643 -e-
            End Select
        End If
        '^_^20220808 modi by 7643 -e-
        SumGrandTotal()
        RefreshGUI()
    End Function
    '^_^20220822 mark by 7643 -b-
    'Private Function DraftAirTemplate1(objSumRow As DataGridViewRow, strProduct As String _
    '                                   , strDomInt As String, intVatDiscount As Integer) As Boolean
    '^_^20220822 mark by 7643 -e-
    '^_^20220822 modi by 7643 -b-
    Private Function DraftAirTemplate1(objSumRow As DataGridViewRow, strProduct As String _
                                       , strDomInt As String, intVatDiscount As Integer, Optional xNumExtRow As Integer = 0) As Boolean
        Dim i As Integer
        '^_^20220822 modi by 7643 -e-
        With objSumRow
            If strProduct = "AIR" Then
                Dim strDesc As String = ""
                Dim strTkno4Sf As String = ""
                Dim intVatPct4Fare As Integer
                Dim blnGetFtc As Boolean
                Dim blnGetMerchantFee As Boolean = GetThisTransaction(intVatDiscount, 10)

                If Not pblnTT78 Then
                    cboSRV.SelectedIndex = cboSRV.FindStringExact(.Cells("SRV").Value)
                End If
                If .Cells("Fare").Value = 0 And .Cells("UE").Value = 0 Then
                    intVatPct4Fare = ScalarToInt("tkt", "Top 1 VatPctRounded", "RcpId in (" & objSumRow.Cells("RcpId").Value & ") and Status<>'XX'")
                Else
                    intVatPct4Fare = CalcVatPctNearest(.Cells("Fare").Value, .Cells("UE").Value)
                End If

                blnGetFtc = GetThisTransaction(intVatDiscount, intVatPct4Fare)

                If .Cells("SRV").Value = "S" Then
                    If .Cells("DocType").Value.ToString.Contains("ETK") _
                        Or .Cells("DocType").Value.ToString.Contains("MCO") _
                        Or .Cells("DocType").Value.ToString.Contains("ATK") Then
                        strDesc = "Tiền vé máy bay"
                        If mtblTkts.Rows.Count = 1 Then
                            strDesc = strDesc & "||" & mtblTkts.Rows(0)("Tkno") _
                        & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary"))
                            txtBuyer.Text = mtblTkts.Rows(0)("PaxName")

                            If IsInCustGrp("VatInvHasDOF", txtCustShortName.Text) Then
                                strDesc = strDesc & "||Ngày bay:" & Format(mtblTkts.Rows(0)("DOF"), "dd-MM-yy")
                            End If
                            If .Cells("Fare").Value = 0 Then
                                strTkno4Sf = mtblTkts.Rows(0)("Tkno")
                            ElseIf blnGetFtc Then
                                AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                     , .Cells("UE").Value,, "Vé", 1, intVatDiscount)
                            End If

                        Else
                            strDesc = strDesc & " " & txtPeriod.Text
                            If blnGetFtc Then
                                AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                             , .Cells("UE").Value,,,, intVatDiscount)
                            End If
                        End If
                    End If

                    If blnGetFtc Then
                        If .Cells("Charge").Value <> 0 Then
                            strDesc = "Phí khác"
                            AddRow4E_Inv(strDesc, strDomInt, .Cells("Charge").Value, False, 0,, "Lần", 1, intVatDiscount)
                        End If
                        If .Cells("Tax").Value <> 0 Then
                            If .Cells("Tax").Value < 0 Then
                                strDesc = "Tiền hoàn các khoản thu hộ khác"
                            Else
                                strDesc = "Các khoản thu hộ khác"
                            End If
                            AddRow4E_Inv(strDesc, strDomInt, .Cells("Tax").Value, False, 0,, "Lần", 1, intVatDiscount)
                        End If
                        If .Cells("ServiceFee").Value <> 0 Then
                            Select Case .Cells("DocType").Value
                                Case "AHC"
                                    strDesc = "Phí dịch vụ ngoài giờ"
                                Case "INS"
                                    strDesc = "Phí bảo hiểm"
                                Case Else
                                    strDesc = ("Phí dịch vụ " & strTkno4Sf).Trim
                            End Select
                        End If
                        AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value, False, 0,, "Lần", 1, intVatDiscount)
                    End If

                    If blnGetMerchantFee AndAlso .Cells("MerchantFee").Value <> 0 Then
                        Dim decMfNoVat As Decimal = RoundNearest(.Cells("MerchantFee").Value * 100 / 110)
                        Dim decVatMf As Decimal = .Cells("MerchantFee").Value - decMfNoVat
                        strDesc = ("Phí cà thẻ " & strTkno4Sf).Trim
                        AddRow4E_Inv(strDesc, "DOM", decMfNoVat, True, decVatMf, 10, "Lần", 1, intVatDiscount)
                    End If
                    If mtblTkts.Rows(0)("StockCtrl") <> "" Then
                        Dim objOriInv As DataRow
                        objOriInv = GetOriginalInvBoth(mtblTkts.Rows(0)("StockCtrl"))
                        If objOriInv IsNot Nothing Then
                            mintAdjustType = 2
                            Me.Text = "Điều chỉnh tăng hóa đơn "
                            mstrOldInvPattern = objOriInv("MauSo")
                            mstrOldInvSerial = objOriInv("KyHieu")
                            mdteOldInvDOI = objOriInv("DOI")
                            mintOldInvNo = objOriInv("InvoiceNo")
                            txtOriInvNbr.Text = objOriInv("InvoiceNo")
                            txtOriFkey.Text = objOriInv("InvId")
                            pnlOriInv.Visible = True
                        End If
                    End If

                    '^_^20220822 add by 7643 -b-
                    For i = 0 To xNumExtRow - 1
                        AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & mtblTkts.Rows(0)("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                    Next
                    '^_^20220822 add by 7643 -e-
                ElseIf .Cells("SRV").Value = "R" Then
                    If pblnTT78 Then
                        Dim strAction As String = InputBox("Nhập R/C cho điều chỉnh Giảm(Hoàn giá vé và thuế)/Tăng(Thu phí)").ToUpper
                        txtBuyer.Text = mtblTkts.Rows(0)("PaxName")

                        If strAction = "R" Then
                            strDesc = "Tiền hoàn vé máy bay||" & strDesc
                            If mtblTkts.Rows.Count = 1 Then
                                strDesc = "Tiền hoàn vé máy bay||" & mtblTkts.Rows(0)("Tkno") _
                                & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary"))

                                AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                                 , .Cells("UE").Value,, "Vé", 1,)
                            Else
                                strDesc = "Tiền hoàn vé máy bay " & txtPeriod.Text
                                If blnGetFtc AndAlso .Cells("Fare").Value <> 0 Then
                                    AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                                         , .Cells("UE").Value,,,,)
                                End If
                            End If
                            If blnGetFtc Then
                                If .Cells("Tax").Value <> 0 Then
                                    strDesc = "Tiền hoàn các khoản thu hộ khác"
                                    AddRow4E_Inv(strDesc, strDomInt, .Cells("Tax").Value, False, 0,, "Lần", 1,)
                                End If
                            End If

                            '^_^20221007 add by 7643 -b-
                            If .Cells("MerchantFee").Value <> 0 Then
                                Dim decMfNoVat As Decimal = RoundNearest(.Cells("MerchantFee").Value * 100 / 110)
                                Dim decVatMf As Decimal = .Cells("MerchantFee").Value - decMfNoVat
                                strDesc = ("Tiền hoàn phí cà thẻ").Trim
                                AddRow4E_Inv(strDesc, "DOM", decMfNoVat, True, decVatMf, 10, "Lần", 1, intVatDiscount)
                            End If
                            '^_^20221007 add by 7643 -e-
                        ElseIf strAction = "C" Then
                            If blnGetFtc Then
                                If .Cells("Charge").Value <> 0 Then
                                    strDesc = "Phí khác"
                                    AddRow4E_Inv(strDesc, "INT", .Cells("Charge").Value, False, 0, -1, "Lần", 1,)
                                End If
                                If .Cells("ServiceFee").Value <> 0 Then
                                    strDesc = "Phần thu dịch vụ hoàn vé||" & mtblTkts.Rows(0)("Tkno") _
                                & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary"))
                                    AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value + .Cells("MerchantFee").Value, False, 0,, "Lần", 1,)
                                End If
                                '^_^20221007 mark by 7643 -b-
                                'If blnGetMerchantFee AndAlso .Cells("MerchantFee").Value <> 0 Then
                                '    strDesc = "Phí cà thẻ"
                                '    AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value + .Cells("MerchantFee").Value, False, 0,, "Lần", 1,)
                                'End If
                                '^_^20221007 mark by 7643 -e-
                            End If
                        End If
                        '^_^20221007 mark by 7643 -b-
                        'If mtblTkts.Rows(0)("StockCtrl") <> "" Then
                        '    Dim objOriInv As DataRow
                        '    objOriInv = GetOriginalInvBoth(mtblTkts.Rows(0)("StockCtrl"))
                        '    If objOriInv IsNot Nothing Then
                        '        If strAction = "R" Then
                        '            mintAdjustType = 3
                        '        ElseIf strAction = "C" Then
                        '            mintAdjustType = 2
                        '        End If
                        '    End If
                        'End If
                        '^_^20221007 mark by 7643 -e-
                        '^_^20221007 modi by 7643 -b-
                        Dim objOriInv As DataRow
                        objOriInv = GetOriginalInvBoth(mtblTkts.Rows(0)("Tkno"))
                        If objOriInv IsNot Nothing Then
                            If strAction = "R" Then
                                mintAdjustType = 3
                            ElseIf strAction = "C" Then
                                mintAdjustType = 2
                            End If
                            LoadOriInvValues(objOriInv)
                            mblnNoOriInv = NoOriInv(objOriInv("InvId"), objOriInv("TVC"))
                        End If
                        '^_^20221007 modi by 7643 -e-
                    Else
                        strDesc = "Tiền hoàn vé máy bay"
                        If mtblTkts.Rows.Count = 1 Then
                            strDesc = strDesc & "||" & mtblTkts.Rows(0)("Tkno") _
                            & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary"))
                            txtBuyer.Text = mtblTkts.Rows(0)("PaxName")
                            If blnGetFtc AndAlso .Cells("Fare").Value <> 0 Then
                                AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                                 , .Cells("UE").Value,, "Vé", 1,)
                            End If
                        Else
                            strDesc = strDesc & " " & txtPeriod.Text
                            If blnGetFtc AndAlso .Cells("Fare").Value <> 0 Then
                                AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                                     , .Cells("UE").Value,,,,)
                            End If
                        End If

                        If blnGetFtc Then
                            If .Cells("Charge").Value <> 0 Then
                                strDesc = "Phí khác"
                                AddRow4E_Inv(strDesc, "INT", .Cells("Charge").Value, False, 0, -1, "Lần", 1,)
                            End If
                            If .Cells("Tax").Value <> 0 Then
                                strDesc = "Tiền hoàn các khoản thu hộ khác"
                                AddRow4E_Inv(strDesc, strDomInt, .Cells("Tax").Value, False, 0,, "Lần", 1,)
                            End If
                            If .Cells("ServiceFee").Value <> 0 Then
                                strDesc = "Phần thu dịch vụ hoàn vé"
                                AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value + .Cells("MerchantFee").Value, False, 0,, "Lần", 1,)
                            End If
                            If blnGetMerchantFee AndAlso .Cells("MerchantFee").Value <> 0 Then
                                strDesc = "Phí cà thẻ"
                                AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value + .Cells("MerchantFee").Value, False, 0,, "Lần", 1,)
                            End If
                        End If
                    End If

                    '^_^20220822 add by 7643 -b-
                    For i = 0 To xNumExtRow - 1
                        AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & mtblTkts.Rows(0)("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                    Next
                    '^_^20220822 add by 7643 -e-

                    'ElseIf blnGetMerchantFee AndAlso .Cells("MerchantFee").Value <> 0 Then
                    '    strDesc = ("Phí cà thẻ " & strTkno4Sf).Trim
                    '    AddRow4E_Inv(strDesc, "DOM", .Cells("MerchantFee").Value, False, 0,, "Lần", 1, intVatDiscount, blnVat8)
                End If

                FormatGridSumAir()

            ElseIf strProduct = "N-A" Then
                cboSRV.SelectedIndex = cboSRV.FindStringExact("S")
            End If
        End With
        RefreshGUI()
        Return True
    End Function
    '^_^20220822 mark by 7643 -b-
    'Private Function DraftAirTemplate2(objSumRow As DataGridViewRow, strProduct As String _
    '                                , strDomInt As String, intVatDiscount As Integer) As Boolean
    '^_^20220822 mark by 7643 -e-
    '^_^20220822 modi by 7643 -b-
    Private Function DraftAirTemplate2(objSumRow As DataGridViewRow, strProduct As String _
                                    , strDomInt As String, intVatDiscount As Integer, Optional xNumExtRow As Integer = 0) As Boolean
        Dim i As Integer
        '^_^20220822 modi by 7643 -e-
        With objSumRow
            If strProduct = "AIR" Then
                Dim strDesc As String = ""
                Dim strTkno4Sf As String = ""
                Dim intVatPct4Fare As Integer
                Dim blnGetFtc As Boolean
                Dim blnGetMerchantFee As Boolean = GetThisTransaction(intVatDiscount, 10)
                '^_^20220822 add by 7643 -b-
                Dim intVatPctSf As Integer = .Cells("VatPctSf").Value
                Dim blnGetSf As Boolean = GetThisTransaction(intVatDiscount, intVatPctSf)
                Dim blnGetMf As Boolean = GetThisTransaction(intVatDiscount, 10)
                Dim blnGetCharge As Boolean
                '^_^20220822 add by 7643 -e-


                If .Cells("Fare").Value = 0 And .Cells("UE").Value = 0 Then
                    intVatPct4Fare = ScalarToInt("tkt", "Top 1 VatPctRounded", "RcpId in (" & objSumRow.Cells("RcpId").Value & ") and Status<>'XX'")
                Else
                    intVatPct4Fare = CalcVatPctNearest(.Cells("Fare").Value, .Cells("UE").Value)
                End If

                blnGetFtc = GetThisTransaction(intVatDiscount, intVatPct4Fare)

                If .Cells("SRV").Value = "S" Then
                    If .Cells("DocType").Value.ToString.Contains("ETK") _
                        Or .Cells("DocType").Value.ToString.Contains("MCO") _
                        Or .Cells("DocType").Value.ToString.Contains("ATK") Then
                        'strDesc = "Tiền vé máy bay"  '^_^20220809 mark by 7643
                        For Each objTktRow As DataRow In mtblTkts.Rows
                            '^_^20220809 mark by 7643 -b-
                            'strDesc = strDesc & "||" & objTktRow("Tkno") _
                            '& "||" & ConvertItinerary4FullName(objTktRow("Itinerary")) _
                            '& objTktRow("PaxName")
                            '^_^20220809 mark by 7643 -e-
                            '^_^20220809 modi by 7643 -b-
                            strDesc = "Tiền vé máy bay"
                            strDesc = strDesc & "||" & objTktRow("Tkno") _
                            & "||" & ConvertItinerary4FullName(objTktRow("Itinerary")) _
                            & "||" & objTktRow("PaxName")
                            '^_^20220809 modi by 7643 -e-

                            '^_^20230217 add by 7643 -b-
                            If txtCustId.Text = "91881" Then
                                strDesc &= "||Ngày đi: " & Format(objTktRow("DOF"), "dd-MMM-yy") & "||Ngày về: " & Format(objTktRow("ReturnDate"), "dd-MMM-yy")
                            End If
                            '^_^20230217 add by 7643 -e-

                            If objTktRow("Fare") + objTktRow("Tax") + objTktRow("Charge") = 0 Then
                                strTkno4Sf = objTktRow("Tkno")
                            ElseIf blnGetFtc Then
                                Dim decVatable As Decimal
                                Dim decVatFtc As Decimal
                                'decVatable = (objTktRow("Fare") + objTktRow("Tax") + objTktRow("Charge")) * 100 / (100 + intVatPct4Fare)  '^_^20220809 mark by 7643
                                '^_^20220809 modi by 7643 -b-
                                '^_^20221119 mark by 7643 -b-
                                'decVatable = Math.Round(objTktRow("Fare") * 100 / (100 + intVatPct4Fare)) +
                                '             Math.Round(objTktRow("Tax") * 100 / (100 + intVatPct4Fare)) +
                                '             Math.Round(objTktRow("Charge") * 100 / (100 + intVatPct4Fare))
                                '^_^20221119 mark by 7643 -e-
                                '^_^20221119 modi by 7643 -b-
                                '^_^20221121 mark by 7643 -b-
                                'decVatable = Math.Round((objTktRow("Fare") + objTktRow("VatAmt")) * 100 / (100 + intVatPct4Fare)) +
                                '             (objTktRow("TaxNoUE") - objTktRow("VatTaxNoUE")) +
                                'Math.Round(objTktRow("Charge") * 100 / (100 + intVatPct4Fare))
                                '^_^20221121 mark by 7643 -e-
                                '^_^20221121 modi by 7643 -b-
                                decVatable = Math.Round((objTktRow("Fare") + objTktRow("VatAmt")) * 100 / (100 + intVatPct4Fare)) +
                                             Math.Round((objTktRow("Tax") - objTktRow("VatAmt")) * 100 / (100 + intVatPct4Fare)) +
                                Math.Round(objTktRow("Charge") * 100 / (100 + intVatPct4Fare))
                                '^_^20221121 modi by 7643 -e-
                                '^_^20221119 modi by 7643 -e-
                                '^_^20220809 modi by 7643 -e-
                                decVatFtc = (objTktRow("Fare") + objTktRow("Tax") + objTktRow("Charge")) - decVatable
                                AddRow4E_Inv(strDesc, strDomInt, decVatable, True _
                                     , decVatFtc,, "Vé", 1, intVatDiscount)
                            End If
                            If blnGetFtc Then
                                If objTktRow("ChargeTV") <> 0 Then
                                    Select Case objTktRow("DocType")
                                        Case "AHC"
                                            strDesc = "Phí dịch vụ ngoài giờ"
                                        Case "INS"
                                            strDesc = "Phí bảo hiểm"
                                        Case Else
                                            strDesc = ("Phí dịch vụ " & strTkno4Sf).Trim
                                    End Select
                                End If
                                AddRow4E_Inv(strDesc, "DOM", objTktRow("ChargeTV"), False, 0,, "Lần", 1, intVatDiscount)
                            End If

                            If blnGetMerchantFee AndAlso .Cells("MerchantFee").Value <> 0 Then
                                Dim decMfNoVat As Decimal = RoundNearest(.Cells("MerchantFee").Value * 100 / 110)
                                Dim decVatMf As Decimal = .Cells("MerchantFee").Value - decMfNoVat
                                strDesc = ("Phí cà thẻ " & strTkno4Sf).Trim
                                AddRow4E_Inv(strDesc, "DOM", decMfNoVat, True, decVatMf, 10, "Lần", 1, intVatDiscount)
                            End If
                            If objTktRow("StockCtrl") <> "" Then
                                Dim objOriInv As DataRow
                                objOriInv = GetOriginalInvBoth(objTktRow("StockCtrl"))
                                If objOriInv IsNot Nothing Then
                                    mintAdjustType = 2
                                    Me.Text = "Điều chỉnh tăng hóa đơn "
                                    mstrOldInvPattern = objOriInv("MauSo")
                                    mstrOldInvSerial = objOriInv("KyHieu")
                                    mdteOldInvDOI = objOriInv("DOI")
                                    mintOldInvNo = objOriInv("InvoiceNo")
                                    txtOriInvNbr.Text = objOriInv("InvoiceNo")
                                    txtOriFkey.Text = objOriInv("InvId")
                                    pnlOriInv.Visible = True
                                End If
                            End If

                            '^_^20220822 add by 7643 -b-
                            For i = 0 To xNumExtRow - 1
                                'AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & mtblTkts.Rows(0)("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                                AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & objTktRow("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                            Next
                            '^_^20220822 add by 7643 -e-
                        Next

                    Else
                        '^_^20220822 mark by 7643 -b-
                        'strDesc = strDesc & " " & txtPeriod.Text
                        'If blnGetFtc Then
                        '    AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                        '                 , .Cells("UE").Value,,,, intVatDiscount)
                        'End If
                        '^_^20220822 mark by 7643 -e-
                        '^_^20220822 modi by 7643 -b-
                        For Each objTktRow As DataRow In mtblTkts.Rows
                            If blnGetFtc Then
                                If objTktRow("ChargeTV") <> 0 Then
                                    Select Case objTktRow("DocType")
                                        Case "AHC"
                                            strDesc = "Phí dịch vụ ngoài giờ"
                                        Case "INS"
                                            strDesc = "Phí bảo hiểm"
                                        Case Else
                                            strDesc = ("Phí dịch vụ " & strTkno4Sf).Trim
                                    End Select
                                End If
                                AddRow4E_Inv(strDesc & "||" & objTktRow("Tkno") & "||" & objTktRow("PaxName"), "DOM", objTktRow("ChargeTV"), False, 0,, "Lần", 1, intVatDiscount)
                            End If

                            If blnGetMerchantFee AndAlso .Cells("MerchantFee").Value <> 0 Then
                                Dim decMfNoVat As Decimal = RoundNearest(.Cells("MerchantFee").Value * 100 / 110)
                                Dim decVatMf As Decimal = .Cells("MerchantFee").Value - decMfNoVat
                                strDesc = ("Phí cà thẻ " & strTkno4Sf).Trim
                                AddRow4E_Inv(strDesc, "DOM", decMfNoVat, True, decVatMf, 10, "Lần", 1, intVatDiscount)
                            End If

                            For i = 0 To xNumExtRow - 1
                                'AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & mtblTkts.Rows(0)("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                                AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & objTktRow("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                            Next
                        Next
                        '^_^20220822 modi by 7643 -e-
                    End If

                    '^_^20220822 add by 7643 -b-
                ElseIf .Cells("SRV").Value = "R" Then
                    Dim intMultiplier As Integer = -1
                    If pblnTT78 Then
                        Dim strAction As String = InputBox("Nhập R/C cho điều chỉnh Giảm(Hoàn giá vé và thuế)/Tăng(Thu phí)").ToUpper
                        strDesc = "Hoàn vé máy bay"

                        Dim objOriInv As DataRow
                        objOriInv = GetOriginalInvBoth(mtblTkts.Rows(0)("Tkno"))
                        If objOriInv IsNot Nothing Then
                            If strAction = "R" Then
                                mintAdjustType = 3
                            ElseIf strAction = "C" Then
                                mintAdjustType = 2
                            End If
                            LoadOriInvValues(objOriInv)
                            mblnNoOriInv = NoOriInv(objOriInv("InvId"), objOriInv("TVC"))
                        End If

                        If strAction = "R" Then
                            If .Cells("Fare").Value <> 0 Then
                                strDesc = "Tiền hoàn vé máy bay"
                                If mtblTkts.Rows.Count = 1 Then
                                    Dim decVatable As Decimal
                                    Dim decVatFtc As Decimal
                                    strDesc = strDesc & "||" & mtblTkts.Rows(0)("Tkno") _
                                        & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                                        & "||" & mtblTkts.Rows(0)("PaxName")

                                    '^_^20230217 add by 7643 -b-
                                    If txtCustId.Text = "91881" Then
                                        strDesc &= "||Ngày đi: " & Format(mtblTkts.Rows(0)("DOF"), "dd-MMM-yy") &
                                                   "||Ngày về: " & Format(mtblTkts.Rows(0)("ReturnDate"), "dd-MMM-yy")
                                    End If
                                    '^_^20230217 add by 7643 -e-

                                    '^_^20221119 mark by 7643 -b-
                                    'decVatable = Math.Round(.Cells("Fare").Value * 100 / (100 + intVatPct4Fare)) +
                                    '             Math.Round(.Cells("Tax").Value * 100 / (100 + intVatPct4Fare)) +
                                    '             Math.Round(.Cells("UE").Value * 100 / (100 + intVatPct4Fare))
                                    '^_^20221119 mark by 7643 -e-
                                    '^_^20221119 modi by 7643 -b-
                                    decVatable = Math.Round((.Cells("Fare").Value + .Cells("UE").Value) * 100 / (100 + intVatPct4Fare)) +
                                                 Math.Round(.Cells("Tax").Value * 100 / (100 + intVatPct4Fare))
                                    '^_^20221119 modi by 7643 -e-
                                    decVatFtc = (.Cells("Fare").Value + .Cells("Tax").Value + .Cells("UE").Value) - decVatable
                                    AddRow4E_Inv(strDesc, strDomInt, decVatable, True _
                                                         , decVatFtc,, "Vé", 1,)
                                Else
                                    strDesc = strDesc & " " & txtPeriod.Text
                                    AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                                         , .Cells("UE").Value,, "Vé", 1,)
                                End If
                                If .Cells("MerchantFee").Value <> 0 Then
                                    Dim decMfNoVat As Decimal = RoundNearest(.Cells("MerchantFee").Value * 100 / 110)
                                    Dim decVatMf As Decimal = .Cells("MerchantFee").Value - decMfNoVat
                                    strDesc = "Tiền hoàn phí cà thẻ"
                                    AddRow4E_Inv(strDesc, "DOM", decMfNoVat, True, decVatMf, 10, "Lần", 1, intVatDiscount)
                                End If
                            End If
                        ElseIf strAction = "C" Then
                            Dim strTktInfo As String = "||" & mtblTkts.Rows(0)("Tkno") _
                                        & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                                        & "||" & mtblTkts.Rows(0)("PaxName")
                            blnGetCharge = GetThisTransaction(0, -1)
                            If blnGetCharge AndAlso .Cells("Charge").Value <> 0 Then
                                strDesc = "Phí hoàn vé" & strTktInfo

                                '^_^20230217 add by 7643 -b-
                                If txtCustId.Text = "91881" Then
                                    strDesc &= "||Ngày đi: " & Format(mtblTkts.Rows(0)("DOF"), "dd-MMM-yy") &
                                               "||Ngày về: " & Format(mtblTkts.Rows(0)("ReturnDate"), "dd-MMM-yy")
                                End If
                                '^_^20230217 add by 7643 -e-

                                AddRow4E_Inv(strDesc, "INT", .Cells("Charge").Value, False, 0, -1, "Lần", 1,)
                            End If

                            If blnGetSf AndAlso .Cells("ServiceFee").Value <> 0 Then
                                strDesc = "Phần thu dịch vụ hoàn vé" & strTktInfo
                                AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value, False, 0,, "Lần", 1, intVatDiscount)
                                'ElseIf blnGetMf AndAlso .Cells("MerchantFee").Value <> 0 Then
                                '    strDesc = "Phần thu dịch vụ hoàn vé"
                                '    AddRow4E_Inv(strDesc, "DOM", Math.Abs(.Cells("MerchantFee").Value), False, 0,,,,)
                            End If
                        End If
                    Else
                        If .Cells("Charge").Value <> 0 Then
                            strDesc = "Phí hoàn vé"
                            AddRow4E_Inv(strDesc, "INT", .Cells("Charge").Value * intMultiplier, False, 0, -1,,,)
                        End If

                        If blnGetSf AndAlso .Cells("ServiceFee").Value <> 0 Then
                            strDesc = "Phần thu dịch vụ hoàn vé"
                            AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value, False, 0,, "Lần",, intVatDiscount)
                            'AddRow4E_Inv(strDesc, "DOM", (.Cells("ServiceFee").Value) * intMultiplier, False, 0,,,,)
                            'ElseIf blnGetMf AndAlso .Cells("MerchantFee").Value <> 0 Then
                            '    strDesc = "Phần thu dịch vụ hoàn vé"
                            '    AddRow4E_Inv(strDesc, "DOM", (.Cells("MerchantFee").Value) * intMultiplier, False, 0,, "Lần",,)
                        End If

                    End If

                    For i = 0 To xNumExtRow - 1
                        AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & mtblTkts.Rows(0)("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                    Next
                    '^_^20220822 add by 7643 -e-
                End If


                'ElseIf .Cells("SRV").Value = "R" Then
                '    Dim strDesc As String
                '    Dim strAction As String = InputBox("Nhập R/C cho điều chỉnh Giảm(Hoàn giá vé và thuế)/Tăng(Thu phí)").ToUpper

                '    If strAction = "R" Then
                '        strDesc = "Tiền hoàn vé máy bay||" & strDesc

                '        For Each objTktRow As DataRow In mtblTkts.Rows
                '            strDesc = "Tiền hoàn vé máy bay||" & objTktRow("Tkno") & "||" & ConvertItinerary4FullName(objTktRow("Itinerary"))
                '            Dim decVatable As Decimal
                '            Dim decVatFtc As Decimal
                '            decVatable = (objTktRow("Fare") + objTktRow("Tax") + objTktRow("UE")) * 100 / (100 + intVatPct4Fare)
                '            decVatFtc = (objTktRow("Fare") + objTktRow("Tax") + objTktRow("UE")) - decVatable
                '            AddRow4E_Inv(strDesc, strDomInt, decVatable, True _
                '                         , decVatFtc,, "Vé", 1, intVatDiscount)
                '            If objTktRow("StockCtrl") <> "" Then
                '                Dim objOriInv As DataRow
                '                objOriInv = GetOriginalInvBoth(objTktRow("StockCtrl"))
                '                If objOriInv IsNot Nothing Then
                '                    If strAction = "R" Then
                '                        mintAdjustType = 3
                '                    ElseIf strAction = "C" Then
                '                        mintAdjustType = 2
                '                    End If
                '                End If
                '            End If
                '        Next


                '    ElseIf strAction = "C" Then
                '        If blnGetFtc Then
                '            If .Cells("Charge").Value <> 0 Then
                '                strDesc = "Phí khác"
                '                AddRow4E_Inv(strDesc, "INT", .Cells("Charge").Value, False, 0, -1, "Lần", 1,)
                '            End If
                '            If .Cells("ServiceFee").Value <> 0 Then
                '                strDesc = "Phần thu dịch vụ hoàn vé||" & objTktRow("Tkno") _
                '                & "||" & ConvertItinerary4FullName(objTktRow("Itinerary"))
                '                AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value + .Cells("MerchantFee").Value, False, 0,, "Lần", 1,)
                '            End If
                '            If blnGetMerchantFee AndAlso .Cells("MerchantFee").Value <> 0 Then
                '                strDesc = "Phí cà thẻ"
                '                AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value + .Cells("MerchantFee").Value, False, 0,, "Lần", 1,)
                '            End If
                '        End If
                'End If

                FormatGridSumAir()

            ElseIf strProduct = "N-A" Then
                cboSRV.SelectedIndex = cboSRV.FindStringExact("S")
            End If
        End With
        RefreshGUI()
        Return True
    End Function
    Private Function DraftAirTemplate3(objSumRow As DataGridViewRow, strProduct As String _
                                    , strDomInt As String, intVatDiscount As Integer, Optional xNumExtRow As Integer = 0) As Boolean
        Dim i As Integer
        '^_^20220822 modi by 7643 -e-
        With objSumRow
            If strProduct = "AIR" Then
                Dim strDesc As String = ""
                Dim strTkno4Sf As String = ""
                Dim intVatPct4Fare As Integer
                Dim blnGetFtc As Boolean
                Dim blnGetMerchantFee As Boolean = GetThisTransaction(intVatDiscount, 10)

                Dim intVatPctSf As Integer = .Cells("VatPctSf").Value
                Dim blnGetSf As Boolean = GetThisTransaction(intVatDiscount, intVatPctSf)
                Dim blnGetMf As Boolean = GetThisTransaction(intVatDiscount, 10)
                Dim blnGetCharge As Boolean


                If .Cells("Fare").Value = 0 And .Cells("UE").Value = 0 Then
                    intVatPct4Fare = ScalarToInt("tkt", "Top 1 VatPctRounded", "RcpId in (" & objSumRow.Cells("RcpId").Value & ") and Status<>'XX'")
                Else
                    intVatPct4Fare = CalcVatPctNearest(.Cells("Fare").Value, .Cells("UE").Value)
                End If

                blnGetFtc = GetThisTransaction(intVatDiscount, intVatPct4Fare)

                If .Cells("SRV").Value = "S" Then
                    If .Cells("DocType").Value.ToString.Contains("ETK") _
                        Or .Cells("DocType").Value.ToString.Contains("MCO") _
                        Or .Cells("DocType").Value.ToString.Contains("ATK") Then
                        For Each objTktRow As DataRow In mtblTkts.Rows
                            strDesc = "Tiền vé máy bay"
                            strDesc = strDesc & "||" & objTktRow("Tkno") _
                            & "||" & ConvertItinerary4FullName(objTktRow("Itinerary")) _
                            & "||" & objTktRow("PaxName")

                            If objTktRow("Fare") + objTktRow("Tax") + objTktRow("Charge") = 0 Then
                                strTkno4Sf = objTktRow("Tkno")
                            ElseIf blnGetFtc Then
                                Dim decVatable As Decimal
                                Dim decVatFtc As Decimal
                                decVatable = objTktRow("Fare") +
                                             Math.Round((objTktRow("Tax") - objTktRow("VatAmt")) * 100 / (100 + intVatPct4Fare)) +
                                             Math.Round(objTktRow("Charge") * 100 / (100 + intVatPct4Fare))

                                decVatFtc = (objTktRow("Fare") + objTktRow("Tax") + objTktRow("Charge")) - decVatable
                                AddRow4E_Inv(strDesc, strDomInt, decVatable, True _
                                     , decVatFtc,, "Vé", 1, intVatDiscount)
                            End If
                            If blnGetFtc Then
                                If objTktRow("ChargeTV") <> 0 Then
                                    Select Case objTktRow("DocType")
                                        Case "AHC"
                                            strDesc = "Phí dịch vụ ngoài giờ"
                                        Case "INS"
                                            strDesc = "Phí bảo hiểm"
                                        Case Else
                                            strDesc = ("Phí dịch vụ " & strTkno4Sf).Trim
                                    End Select
                                End If
                                AddRow4E_Inv(strDesc, "DOM", objTktRow("ChargeTV"), False, 0,, "Lần", 1, intVatDiscount)
                            End If

                            If blnGetMerchantFee AndAlso .Cells("MerchantFee").Value <> 0 Then
                                Dim decMfNoVat As Decimal = RoundNearest(.Cells("MerchantFee").Value * 100 / 110)
                                Dim decVatMf As Decimal = .Cells("MerchantFee").Value - decMfNoVat
                                strDesc = ("Phí cà thẻ " & strTkno4Sf).Trim
                                AddRow4E_Inv(strDesc, "DOM", decMfNoVat, True, decVatMf, 10, "Lần", 1, intVatDiscount)
                            End If
                            If objTktRow("StockCtrl") <> "" Then
                                Dim objOriInv As DataRow
                                objOriInv = GetOriginalInvBoth(objTktRow("StockCtrl"))
                                If objOriInv IsNot Nothing Then
                                    mintAdjustType = 2
                                    Me.Text = "Điều chỉnh tăng hóa đơn "
                                    mstrOldInvPattern = objOriInv("MauSo")
                                    mstrOldInvSerial = objOriInv("KyHieu")
                                    mdteOldInvDOI = objOriInv("DOI")
                                    mintOldInvNo = objOriInv("InvoiceNo")
                                    txtOriInvNbr.Text = objOriInv("InvoiceNo")
                                    txtOriFkey.Text = objOriInv("InvId")
                                    pnlOriInv.Visible = True
                                End If
                            End If

                            For i = 0 To xNumExtRow - 1
                                AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & objTktRow("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                            Next
                        Next

                    Else
                        For Each objTktRow As DataRow In mtblTkts.Rows
                            If blnGetFtc Then
                                If objTktRow("ChargeTV") <> 0 Then
                                    Select Case objTktRow("DocType")
                                        Case "AHC"
                                            strDesc = "Phí dịch vụ ngoài giờ"
                                        Case "INS"
                                            strDesc = "Phí bảo hiểm"
                                        Case Else
                                            strDesc = ("Phí dịch vụ " & strTkno4Sf).Trim
                                    End Select
                                End If
                                AddRow4E_Inv(strDesc & "||" & objTktRow("Tkno") & "||" & objTktRow("PaxName"), "DOM", objTktRow("ChargeTV"), False, 0,, "Lần", 1, intVatDiscount)
                            End If

                            If blnGetMerchantFee AndAlso .Cells("MerchantFee").Value <> 0 Then
                                Dim decMfNoVat As Decimal = RoundNearest(.Cells("MerchantFee").Value * 100 / 110)
                                Dim decVatMf As Decimal = .Cells("MerchantFee").Value - decMfNoVat
                                strDesc = ("Phí cà thẻ " & strTkno4Sf).Trim
                                AddRow4E_Inv(strDesc, "DOM", decMfNoVat, True, decVatMf, 10, "Lần", 1, intVatDiscount)
                            End If

                            For i = 0 To xNumExtRow - 1
                                AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & objTktRow("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                            Next
                        Next

                    End If

                ElseIf .Cells("SRV").Value = "R" Then
                    Dim intMultiplier As Integer = -1
                    If pblnTT78 Then
                        Dim strAction As String = InputBox("Nhập R/C cho điều chỉnh Giảm(Hoàn giá vé và thuế)/Tăng(Thu phí)").ToUpper
                        strDesc = "Hoàn vé máy bay"

                        Dim objOriInv As DataRow
                        objOriInv = GetOriginalInvBoth(mtblTkts.Rows(0)("Tkno"))
                        If objOriInv IsNot Nothing Then
                            If strAction = "R" Then
                                mintAdjustType = 3
                            ElseIf strAction = "C" Then
                                mintAdjustType = 2
                            End If
                            LoadOriInvValues(objOriInv)
                            mblnNoOriInv = NoOriInv(objOriInv("InvId"), objOriInv("TVC"))
                        End If

                        If strAction = "R" Then
                            If .Cells("Fare").Value <> 0 Then
                                strDesc = "Tiền hoàn vé máy bay"
                                If mtblTkts.Rows.Count = 1 Then
                                    Dim decVatable As Decimal
                                    Dim decVatFtc As Decimal
                                    strDesc = strDesc & "||" & mtblTkts.Rows(0)("Tkno") _
                                        & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                                        & "||" & mtblTkts.Rows(0)("PaxName")
                                    decVatable = Math.Round(.Cells("Fare").Value * 100 / (100 + intVatPct4Fare)) +
                                                 Math.Round(.Cells("Tax").Value * 100 / (100 + intVatPct4Fare)) +
                                                 Math.Round(.Cells("UE").Value * 100 / (100 + intVatPct4Fare))
                                    decVatFtc = (.Cells("Fare").Value + .Cells("Tax").Value + .Cells("UE").Value) - decVatable
                                    AddRow4E_Inv(strDesc, strDomInt, decVatable, True _
                                                         , decVatFtc,, "Vé", 1,)
                                Else
                                    strDesc = strDesc & " " & txtPeriod.Text
                                    AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                                         , .Cells("UE").Value,, "Vé", 1,)
                                End If
                                If .Cells("MerchantFee").Value <> 0 Then
                                    Dim decMfNoVat As Decimal = RoundNearest(.Cells("MerchantFee").Value * 100 / 110)
                                    Dim decVatMf As Decimal = .Cells("MerchantFee").Value - decMfNoVat
                                    strDesc = "Tiền hoàn phí cà thẻ"
                                    AddRow4E_Inv(strDesc, "DOM", decMfNoVat, True, decVatMf, 10, "Lần", 1, intVatDiscount)
                                End If
                            End If
                        ElseIf strAction = "C" Then
                            Dim strTktInfo As String = "||" & mtblTkts.Rows(0)("Tkno") _
                                        & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                                        & "||" & mtblTkts.Rows(0)("PaxName")
                            blnGetCharge = GetThisTransaction(0, -1)
                            If blnGetCharge AndAlso .Cells("Charge").Value <> 0 Then
                                strDesc = "Phí hoàn vé" & strTktInfo
                                AddRow4E_Inv(strDesc, "INT", .Cells("Charge").Value, False, 0, -1, "Lần", 1,)
                            End If

                            If blnGetSf AndAlso .Cells("ServiceFee").Value <> 0 Then
                                strDesc = "Phần thu dịch vụ hoàn vé" & strTktInfo
                                AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value, False, 0,, "Lần", 1, intVatDiscount)
                            End If
                        End If
                    Else
                        If .Cells("Charge").Value <> 0 Then
                            strDesc = "Phí hoàn vé"
                            AddRow4E_Inv(strDesc, "INT", .Cells("Charge").Value * intMultiplier, False, 0, -1,,,)
                        End If

                        If blnGetSf AndAlso .Cells("ServiceFee").Value <> 0 Then
                            strDesc = "Phần thu dịch vụ hoàn vé"
                            AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value, False, 0,, "Lần",, intVatDiscount)
                        End If

                    End If

                    For i = 0 To xNumExtRow - 1
                        AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & mtblTkts.Rows(0)("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                    Next
                End If

                FormatGridSumAir()

            ElseIf strProduct = "N-A" Then
                cboSRV.SelectedIndex = cboSRV.FindStringExact("S")
            End If
        End With
        RefreshGUI()
        Return True
    End Function

    '^_^20220822 mark by 7643 -b-
    'Private Function DraftAirGeneric(objSumRow As DataGridViewRow, strProduct As String _
    '                                 , strDomInt As String, intVatDiscount As Integer) As Boolean
    '^_^20220822 mark by 7643 -e-
    '^_^20220822 modi by 7643 -b-
    Private Function DraftAirGeneric(objSumRow As DataGridViewRow, strProduct As String _
                                     , strDomInt As String, intVatDiscount As Integer, Optional xNumExtRow As Integer = 0) As Boolean
        Dim i As Integer
        '^_^20220822 modi by 7643 -e-
        With objSumRow
            If strProduct = "AIR" Then
                Dim strDesc As String = ""
                Dim strTkno4Sf As String = String.Empty
                If Not pblnTT78 Then
                    cboSRV.SelectedIndex = cboSRV.FindStringExact(.Cells("SRV").Value)
                End If
                Dim intVatPct4Fare As Integer
                If .Cells("Fare").Value <> 0 Then
                    intVatPct4Fare = CalcVatPctNearest(.Cells("Fare").Value, .Cells("UE").Value)
                End If

                Dim blnGetFare As Boolean = GetThisTransaction(intVatDiscount, intVatPct4Fare)
                Dim intVatPctSf As Integer = .Cells("VatPctSf").Value
                Dim blnGetSf As Boolean = GetThisTransaction(intVatDiscount, intVatPctSf)
                Dim blnGetMf As Boolean = GetThisTransaction(intVatDiscount, 10)
                Dim blnGetCharge As Boolean

                If .Cells("SRV").Value = "S" Then
                    If pblnTT78 AndAlso mtblTkts.Rows(0)("StockCtrl") <> "" Then
                        Dim objOriInv As DataRow
                        objOriInv = GetOriginalInvBoth(mtblTkts.Rows(0)("StockCtrl"))
                        If objOriInv IsNot Nothing Then
                            LoadOriInvValues(objOriInv)
                            mintAdjustType = 2
                            mblnNoOriInv = NoOriInv(objOriInv("InvId"), objOriInv("TVC"))
                        End If
                    End If
                    If blnGetFare Then
                        If .Cells("DocType").Value.ToString.Contains("ETK") _
                        Or .Cells("DocType").Value.ToString.Contains("MCO") _
                        Or .Cells("DocType").Value.ToString.Contains("ATK") Then
                            strDesc = "Tiền vé máy bay"
                            If mtblTkts.Rows.Count = 1 Then
                                strDesc = strDesc & "||" & mtblTkts.Rows(0)("Tkno") _
                            & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                            & "||" & mtblTkts.Rows(0)("PaxName")

                                If IsInCustGrp("VatInvHasDOF", txtCustShortName.Text) Then
                                    strDesc = strDesc & "||Ngày bay:" & Format(mtblTkts.Rows(0)("DOF"), "dd-MM-yy")
                                End If
                                If .Cells("Fare").Value = 0 Then
                                    strTkno4Sf = mtblTkts.Rows(0)("Tkno")
                                Else
                                    AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                             , .Cells("UE").Value,, "Vé", 1, intVatDiscount)
                                End If


                            Else
                                strDesc = strDesc & " " & txtPeriod.Text
                                AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                             , .Cells("UE").Value,,,, intVatDiscount)
                            End If
                        End If
                        If .Cells("Charge").Value <> 0 Then
                            strDesc = "Phí khác"
                            AddRow4E_Inv(strDesc, strDomInt, .Cells("Charge").Value, False, 0,, "Lần", 1, intVatDiscount)
                        End If
                        If .Cells("Tax").Value <> 0 Then
                            If .Cells("Tax").Value < 0 Then
                                strDesc = "Tiền hoàn các khoản thu hộ khác"
                            Else
                                strDesc = "Các khoản thu hộ khác"
                            End If
                            AddRow4E_Inv(strDesc, strDomInt, .Cells("Tax").Value, False, 0,, "Lần", 1, intVatDiscount)
                        End If
                    Else
                        If mtblTkts.Rows.Count = 1 Then
                            strTkno4Sf = mtblTkts.Rows(0)("Tkno")
                        Else
                            strTkno4Sf = txtPeriod.Text
                        End If
                    End If

                    If blnGetSf AndAlso .Cells("ServiceFee").Value <> 0 Then
                        Select Case .Cells("DocType").Value
                            Case "AHC"
                                strDesc = "Phí dịch vụ ngoài giờ"
                            Case "INS"
                                strDesc = "Phí bảo hiểm"
                            Case Else
                                strDesc = ("Phần thu dịch vụ bán vé " & strTkno4Sf).Trim
                        End Select

                        AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value, False, 0,, "Lần", 1, intVatDiscount)
                    End If
                    If blnGetMf AndAlso .Cells("MerchantFee").Value <> 0 Then
                        Dim decMfNoVat As Decimal = RoundNearest(.Cells("MerchantFee").Value * 100 / 110)
                        Dim decVatMf As Decimal = .Cells("MerchantFee").Value - decMfNoVat
                        Select Case .Cells("DocType").Value
                            Case "AHC"
                                strDesc = "Phí dịch vụ ngoài giờ"
                            Case "INS"
                                strDesc = "Phí bảo hiểm"
                            Case Else
                                strDesc = ("Phí cà thẻ " & strTkno4Sf).Trim
                        End Select
                        AddRow4E_Inv(strDesc, "DOM", decMfNoVat, True, decVatMf,, "Lần", 1, intVatDiscount)
                    End If
                    'If blnGetSf AndAlso .Cells("MerchantFee").Value <> 0 Then
                    '    If .Cells("DocType").Value.ToString.Contains("ETK") _
                    '                Or .Cells("DocType").Value.ToString.Contains("MCO") _
                    '                Or .Cells("DocType").Value.ToString.Contains("ATK") Then
                    '        If mtblTkts.Rows.Count = 1 Then
                    '            strTkno4Sf = mtblTkts.Rows(0)("Tkno")
                    '        Else
                    '            strTkno4Sf = txtPeriod.Text
                    '        End If
                    '    End If

                    '    strDesc = ("Phí cà thẻ " & strTkno4Sf).Trim
                    '    AddRow4E_Inv(strDesc, "DOM", .Cells("MerchantFee").Value, False, 0,,,, intVatDiscount, blnVat8)
                    'End If

                    '^_^20220822 add by 7643 -b-
                    For i = 0 To xNumExtRow - 1
                        AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & mtblTkts.Rows(0)("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                    Next
                    '^_^20220822 add by 7643 -e-
                ElseIf .Cells("SRV").Value = "R" Then
                    Dim intMultiplier As Integer = -1
                    If pblnTT78 Then
                        Dim strAction As String = InputBox("Nhập R/C cho điều chỉnh Giảm(Hoàn giá vé và thuế)/Tăng(Thu phí)").ToUpper
                        'txtBuyer.Text = mtblTkts.Rows(0)("PaxName")
                        strDesc = "Hoàn vé máy bay"

                        Dim objOriInv As DataRow
                        objOriInv = GetOriginalInvBoth(mtblTkts.Rows(0)("Tkno"))
                        If objOriInv IsNot Nothing Then
                            If strAction = "R" Then
                                mintAdjustType = 3
                            ElseIf strAction = "C" Then
                                mintAdjustType = 2
                            End If
                            LoadOriInvValues(objOriInv)
                            mblnNoOriInv = NoOriInv(objOriInv("InvId"), objOriInv("TVC"))
                        End If

                        'If mtblTkts.Rows.Count = 1 Then
                        '    strDesc = strDesc & "||" & mtblTkts.Rows(0)("Tkno") _
                        '                & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                        '                & "||" & mtblTkts.Rows(0)("PaxName")
                        'End If
                        'AddRow4E_Inv(strDesc, "INT", 0, False, 0, -1,,,, blnVat8, 4)

                        If strAction = "R" Then
                            If .Cells("Fare").Value <> 0 Then
                                strDesc = "Tiền hoàn vé máy bay"
                                If mtblTkts.Rows.Count = 1 Then
                                    strDesc = strDesc & "||" & mtblTkts.Rows(0)("Tkno") _
                                        & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                                        & "||" & mtblTkts.Rows(0)("PaxName")

                                    AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                                         , .Cells("UE").Value,, "Vé", 1,)
                                Else
                                    strDesc = strDesc & " " & txtPeriod.Text
                                    AddRow4E_Inv(strDesc, strDomInt, .Cells("Fare").Value, True _
                                                         , .Cells("UE").Value,, "Vé", 1,)
                                End If
                                If .Cells("Tax").Value <> 0 Then
                                    strDesc = "Tiền hoàn các khoản thu hộ khác"
                                    AddRow4E_Inv(strDesc, strDomInt, .Cells("Tax").Value, False, 0,, "Lần", 1,)
                                End If

                                '^_^20220826 add by 7643 -b-
                                If .Cells("MerchantFee").Value <> 0 Then
                                    Dim decMfNoVat As Decimal = RoundNearest(.Cells("MerchantFee").Value * 100 / 110)
                                    Dim decVatMf As Decimal = .Cells("MerchantFee").Value - decMfNoVat
                                    strDesc = ("Tiền hoàn phí cà thẻ").Trim
                                    AddRow4E_Inv(strDesc, "DOM", decMfNoVat, True, decVatMf, 10, "Lần", 1, intVatDiscount)
                                End If
                                '^_^20220826 add by 7643 -e-
                            End If
                        ElseIf strAction = "C" Then
                            Dim strTktInfo As String = "||" & mtblTkts.Rows(0)("Tkno") _
                                        & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                                        & "||" & mtblTkts.Rows(0)("PaxName")
                            blnGetCharge = GetThisTransaction(0, -1)
                            If blnGetCharge AndAlso .Cells("Charge").Value <> 0 Then
                                'strDesc = "Phí khác" & strTktInfo  '^_^20220826 mark by 7643
                                strDesc = "Phí hoàn vé" & strTktInfo  '^_^20220826 modi by 7643
                                AddRow4E_Inv(strDesc, "INT", .Cells("Charge").Value, False, 0, -1, "Lần", 1,)
                            End If

                            '^_^20220826 mark by 7643 -b-
                            'If blnGetSf AndAlso .Cells("ServiceFee").Value <> 0 Then
                            '    strDesc = "Phần thu dịch vụ hoàn vé" & strTktInfo
                            '    If blnGetMf AndAlso .Cells("MerchantFee").Value <> 0 Then
                            '        AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value + Math.Abs(.Cells("MerchantFee").Value), False, 0,, "Lần", 1, intVatDiscount)
                            '    Else
                            '        AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value, False, 0,, "Lần", 1, intVatDiscount)
                            '    End If
                            'ElseIf blnGetMf AndAlso .Cells("MerchantFee").Value <> 0 Then
                            '    strDesc = "Phần thu dịch vụ hoàn vé"
                            '    AddRow4E_Inv(strDesc, "DOM", Math.Abs(.Cells("MerchantFee").Value), False, 0,,,,)
                            'End If
                            '^_^20220826 mark by 7643 -e-
                            '^_^20220826 modi by 7643 -b-
                            If blnGetSf AndAlso .Cells("ServiceFee").Value <> 0 Then
                                strDesc = "Phần thu dịch vụ hoàn vé" & strTktInfo
                                AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value, False, 0,, "Lần", 1, intVatDiscount)
                            End If
                            '^_^20220826 modi by 7643 -e-
                        End If

                    Else

                        If .Cells("Charge").Value <> 0 Then
                            strDesc = "Phí khác"
                            AddRow4E_Inv(strDesc, "INT", .Cells("Charge").Value * intMultiplier, False, 0, -1,,,)
                        End If

                        If blnGetSf AndAlso .Cells("ServiceFee").Value <> 0 Then
                            strDesc = "Phần thu dịch vụ hoàn vé"
                            If blnGetMf AndAlso .Cells("MerchantFee").Value <> 0 Then
                                AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value + .Cells("MerchantFee").Value, False, 0,,,, intVatDiscount)
                            Else
                                AddRow4E_Inv(strDesc, "DOM", .Cells("ServiceFee").Value, False, 0,, "Lần",, intVatDiscount)
                            End If
                            AddRow4E_Inv(strDesc, "DOM", (.Cells("ServiceFee").Value) * intMultiplier, False, 0,,,,)
                        ElseIf blnGetMf AndAlso .Cells("MerchantFee").Value <> 0 Then
                            strDesc = "Phần thu dịch vụ hoàn vé"
                            AddRow4E_Inv(strDesc, "DOM", (.Cells("MerchantFee").Value) * intMultiplier, False, 0,, "Lần",,)
                        End If
                    End If

                    '^_^20220822 add by 7643 -b-
                    For i = 0 To xNumExtRow - 1
                        AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & mtblTkts.Rows(0)("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                    Next
                    '^_^20220822 add by 7643 -e-
                End If

                FormatGridSumAir()
            ElseIf strProduct = "N-A" Then
                cboSRV.SelectedIndex = cboSRV.FindStringExact("S")
            End If
        End With
        RefreshGUI()
        Return True
    End Function

    '^_^20221003 add by 7643 -b-
    Private Function DraftAirCombineAmount(objSumRow As DataGridViewRow, strProduct As String _
                                     , strDomInt As String, intVatDiscount As Integer, Optional xNumExtRow As Integer = 0) As Boolean
        Dim i, mVATPct As Integer
        Dim mAmount, mCalcVatAmount, mAmountNoVAT, mVAT As Decimal
        With objSumRow
            If strProduct = "AIR" Then
                Dim strDesc As String = ""
                Dim strTkno4Sf As String = String.Empty
                If Not pblnTT78 Then
                    cboSRV.SelectedIndex = cboSRV.FindStringExact(.Cells("SRV").Value)
                End If
                Dim intVatPct4Fare As Integer
                If .Cells("Fare").Value <> 0 Then
                    intVatPct4Fare = CalcVatPctNearest(.Cells("Fare").Value, .Cells("UE").Value)
                End If

                Dim blnGetFare As Boolean = GetThisTransaction(intVatDiscount, intVatPct4Fare)
                Dim intVatPctSf As Integer = .Cells("VatPctSf").Value
                Dim blnGetSf As Boolean = GetThisTransaction(intVatDiscount, intVatPctSf)
                Dim blnGetMf As Boolean = GetThisTransaction(intVatDiscount, 10)
                Dim blnGetCharge As Boolean

                mAmount = .Cells("Fare").Value + .Cells("UE").Value + .Cells("Charge").Value + .Cells("Tax").Value + .Cells("ServiceFee").Value +
                                      .Cells("MerchantFee").Value
                mVATPct = ScalarToInt("MISC", "IntVal1", "CAT='CombineAmount' and IntVal=" & txtCustId.Text & "")
                mCalcVatAmount = IIf(strDomInt = "DOM", mAmount, .Cells("ServiceFee").Value + .Cells("MerchantFee").Value)
                mVAT = mCalcVatAmount - (mCalcVatAmount * 100 / (100 + mVATPct))
                mAmountNoVAT = mAmount - mVAT

                If .Cells("SRV").Value = "S" Then
                    If pblnTT78 AndAlso mtblTkts.Rows(0)("StockCtrl") <> "" Then
                        Dim objOriInv As DataRow
                        objOriInv = GetOriginalInvBoth(mtblTkts.Rows(0)("StockCtrl"))
                        If objOriInv IsNot Nothing Then
                            LoadOriInvValues(objOriInv)
                            mintAdjustType = 2
                            mblnNoOriInv = NoOriInv(objOriInv("InvId"), objOriInv("TVC"))
                        End If
                    End If
                    If blnGetFare Then
                        If .Cells("DocType").Value.ToString.Contains("ETK") _
                        Or .Cells("DocType").Value.ToString.Contains("MCO") _
                        Or .Cells("DocType").Value.ToString.Contains("ATK") Then
                            strDesc = "Tiền vé máy bay"
                            If mtblTkts.Rows.Count = 1 Then
                                strDesc = strDesc & "||" & mtblTkts.Rows(0)("Tkno") _
                                            & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                                            & "||"
                                txtBuyer.Text = mtblTkts.Rows(0)("PaxName")
                                If IsInCustGrp("VatInvHasDOF", txtCustShortName.Text) Then
                                    strDesc = strDesc & "||Ngày bay:" & Format(mtblTkts.Rows(0)("DOF"), "dd-MM-yy")
                                End If
                                If .Cells("Fare").Value = 0 Then
                                    strTkno4Sf = mtblTkts.Rows(0)("Tkno")
                                Else
                                    AddRow4E_Inv(strDesc, "DOM", mAmountNoVAT, True, mVAT, mVATPct, "Vé", 1, intVatDiscount)
                                End If


                            Else
                                strDesc = strDesc & " " & txtPeriod.Text
                                AddRow4E_Inv(strDesc, "DOM", mAmountNoVAT, True, mVAT, mVATPct,,, intVatDiscount)
                            End If
                        End If
                    Else
                        If mtblTkts.Rows.Count = 1 Then
                            strTkno4Sf = mtblTkts.Rows(0)("Tkno")
                        Else
                            strTkno4Sf = txtPeriod.Text
                        End If
                    End If

                    For i = 0 To xNumExtRow - 1
                        AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & mtblTkts.Rows(0)("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                    Next
                ElseIf .Cells("SRV").Value = "R" Then
                    Dim intMultiplier As Integer = -1
                    If pblnTT78 Then
                        Dim strAction As String = InputBox("Nhập R/C cho điều chỉnh Giảm(Hoàn giá vé và thuế)/Tăng(Thu phí)").ToUpper
                        strDesc = "Hoàn vé máy bay"

                        Dim objOriInv As DataRow
                        objOriInv = GetOriginalInvBoth(mtblTkts.Rows(0)("Tkno"))
                        If objOriInv IsNot Nothing Then
                            If strAction = "R" Then
                                mintAdjustType = 3
                            ElseIf strAction = "C" Then
                                mintAdjustType = 2
                            End If
                            LoadOriInvValues(objOriInv)
                            mblnNoOriInv = NoOriInv(objOriInv("InvId"), objOriInv("TVC"))
                        End If

                        If strAction = "R" Then
                            If .Cells("Fare").Value <> 0 Then
                                strDesc = "Tiền hoàn vé máy bay"
                                If mtblTkts.Rows.Count = 1 Then
                                    strDesc = strDesc & "||" & mtblTkts.Rows(0)("Tkno") _
                                        & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                                        & "||"
                                    txtBuyer.Text = mtblTkts.Rows(0)("PaxName")

                                    AddRow4E_Inv(strDesc, "DOM", mAmountNoVAT, True, mVAT, mVATPct, "Vé", 1,)
                                Else
                                    strDesc = strDesc & " " & txtPeriod.Text

                                    AddRow4E_Inv(strDesc, "DOM", mAmountNoVAT, True, mVAT, mVATPct, "Vé", 1,)
                                End If
                            End If
                        ElseIf strAction = "C" Then
                            Dim strTktInfo As String = "||" & mtblTkts.Rows(0)("Tkno") _
                                        & "||" & ConvertItinerary4FullName(mtblTkts.Rows(0)("Itinerary")) _
                                        & "||"
                            txtBuyer = mtblTkts.Rows(0)("PaxName")
                            blnGetCharge = GetThisTransaction(0, -1)
                            If blnGetCharge AndAlso .Cells("Charge").Value <> 0 Then
                                strDesc = "Phí hoàn vé" & strTktInfo

                                AddRow4E_Inv(strDesc, "DOM", mAmountNoVAT, True, mVAT, mVATPct, "Lần", 1,)
                            End If
                        End If
                    Else
                        If .Cells("Charge").Value <> 0 Then
                            strDesc = "Phí khác"

                            AddRow4E_Inv(strDesc, "DOM", mAmountNoVAT, True, mVAT, mVATPct,,,)
                        End If
                    End If

                    For i = 0 To xNumExtRow - 1
                        AddRow4E_Inv("ExtraRow" & CStr(i + 1) & ":" & mtblTkts.Rows(0)("RecID"), "DOM", 0, True, 0, 0,,,, 4)
                    Next
                End If

                FormatGridSumAir()
            ElseIf strProduct = "N-A" Then
                cboSRV.SelectedIndex = cboSRV.FindStringExact("S")
            End If
        End With
        RefreshGUI()
        Return True
    End Function
    '^_^20221003 add by 7643 -e-

    Private Function FormatGridSumAir() As Boolean
        dgrInvDetails.Columns("VAT").DefaultCellStyle.Format = "#,##0"
        dgrInvDetails.Columns("Price").DefaultCellStyle.Format = "#,##0"
        dgrInvDetails.Columns("Amount").DefaultCellStyle.Format = "#,##0"
        dgrInvDetails.Columns("Total").DefaultCellStyle.Format = "#,##0"
        dgrInvDetails.Columns("VAT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrInvDetails.Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrInvDetails.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrInvDetails.Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgrInvDetails.Columns("IsSum").Visible = pblnTT78
        Return True
    End Function
    Private Function AddRow4E_InvBSP(strTkno As String, strItinerary As String, decAmount As Decimal) As Boolean
        Dim objRow As DataGridViewRow
        If dgrInvDetails.RowCount = 0 Then
            dgrInvDetails.Rows.Add()
        Else
            dgrInvDetails.Rows.Insert(dgrInvDetails.RowCount)
        End If

        objRow = dgrInvDetails.Rows(dgrInvDetails.RowCount - 1)
        objRow.Cells("RecId").Value = dgrInvDetails.RowCount
        objRow.Cells("Qty").Value = 1
        objRow.Cells("Tkno").Value = strTkno
        objRow.Cells("Description").Value = strItinerary
        objRow.Cells("Price").Value = decAmount
        objRow.Cells("Amount").Value = decAmount
        objRow.Cells("VAT").Value = 0
        objRow.Cells("VatPct").Value = 0
        objRow.Cells("Total").Value = objRow.Cells("Amount").Value
        objRow.Cells("IsSum").Value = 0
        Return True
    End Function
    Private Function AddRow4E_Inv(strDesc As String, strDomInt As String, decAmount As Decimal _
                    , blnVatSpecified As Boolean, decVat As Decimal _
                    , Optional decVatPct As Decimal = 0, Optional strUnit As String = "" _
                    , Optional intQuatity As Integer = 0, Optional intVatDiscount As Integer = 0 _
                    , Optional intIsSum As Integer = 0) As Boolean
        Dim objRow As DataGridViewRow
        If dgrInvDetails.RowCount = 0 Then
            dgrInvDetails.Rows.Add()
        Else
            dgrInvDetails.Rows.Insert(dgrInvDetails.RowCount)
        End If

        objRow = dgrInvDetails.Rows(dgrInvDetails.RowCount - 1)
        objRow.Cells("RecId").Value = dgrInvDetails.RowCount
        objRow.Cells("Description").Value = strDesc
        objRow.Cells("Amount").Value = decAmount
        Select Case strDomInt
            Case "INT"
                objRow.Cells("Amount").Value = decAmount
                objRow.Cells("VAT").Value = 0
                'If strDesc = "Phí khác" Then
                '    objRow.Cells("VatPct").Value = -1
                'Else
                objRow.Cells("VatPct").Value = 0
                'End If

            Case "DOM"
                If Not blnVatSpecified Then
                    If intVatDiscount = 30 Then
                        objRow.Cells("Amount").Value = Math.Round(decAmount / 1.07, 0)
                        objRow.Cells("VAT").Value = decAmount - objRow.Cells("Amount").Value
                    ElseIf Now.Date < CDate("01 Jan 23") Then
                        objRow.Cells("Amount").Value = Math.Round(decAmount / 1.08, 0)
                        objRow.Cells("VAT").Value = decAmount - objRow.Cells("Amount").Value
                    Else
                        objRow.Cells("Amount").Value = Math.Round(decAmount / 1.1, 0)
                        objRow.Cells("VAT").Value = decAmount - objRow.Cells("Amount").Value
                    End If
                Else
                    objRow.Cells("Amount").Value = decAmount
                    objRow.Cells("VAT").Value = decVat
                End If

        End Select
        If decAmount <> 0 Then
            objRow.Cells("VatPct").Value = Math.Round(objRow.Cells("Vat").Value * 100 / objRow.Cells("Amount").Value, 0)
        End If

        If decVatPct <> 0 Then
            objRow.Cells("VatPct").Value = decVatPct
        End If

        If intVatDiscount = 30 AndAlso objRow.Cells("VatPct").Value <> 0 AndAlso Not pblnTT78 Then
            objRow.Cells("VatPct").Value = objRow.Cells("VatPct").Value / 7 * 10
        End If

        objRow.Cells("Total").Value = objRow.Cells("Amount").Value + objRow.Cells("VAT").Value

        If strUnit <> "" Then
            objRow.Cells("Unit").Value = strUnit
        End If

        If intQuatity <> 0 Then
            objRow.Cells("Qty").Value = intQuatity
            objRow.Cells("Price").Value = objRow.Cells("Amount").Value \ intQuatity
        End If

        objRow.Cells("IsSum").Value = intIsSum
        Return True
    End Function

    '^_^20220802 add by 7643 -b-
    Private Function AddRow4E_InvHASBRO(strDesc As String, strDomInt As String, decAmount As Decimal _
                    , blnVatSpecified As Boolean, decVat As Decimal _
                    , Optional decVatPct As Decimal = 0, Optional strUnit As String = "" _
                    , Optional intQuatity As Integer = 0, Optional intVatDiscount As Integer = 0 _
                    , Optional intIsSum As Integer = 0) As Boolean
        Dim objRow As DataGridViewRow
        If dgrInvDetails.RowCount = 0 Then
            dgrInvDetails.Rows.Add()
        Else
            dgrInvDetails.Rows.Insert(dgrInvDetails.RowCount)
        End If

        objRow = dgrInvDetails.Rows(dgrInvDetails.RowCount - 1)
        objRow.Cells("RecId").Value = dgrInvDetails.RowCount
        objRow.Cells("Description").Value = strDesc
        objRow.Cells("Amount").Value = decAmount
        Select Case strDomInt
            Case "INT"
                objRow.Cells("Amount").Value = decAmount
                If Not blnVatSpecified Then
                    objRow.Cells("VAT").Value = decAmount - objRow.Cells("Amount").Value
                Else
                    objRow.Cells("VAT").Value = decVat
                End If
                objRow.Cells("VatPct").Value = 0
            Case "DOM"
                If Not blnVatSpecified Then
                    If intVatDiscount = 30 Then
                        objRow.Cells("Amount").Value = Math.Round(decAmount / 1.07, 0)
                        objRow.Cells("VAT").Value = decAmount - objRow.Cells("Amount").Value
                    ElseIf Now.Date < CDate("01 Jan 23") Then
                        objRow.Cells("Amount").Value = Math.Round(decAmount / 1.08, 0)
                        objRow.Cells("VAT").Value = decAmount - objRow.Cells("Amount").Value
                    Else
                        objRow.Cells("Amount").Value = Math.Round(decAmount / 1.1, 0)
                        objRow.Cells("VAT").Value = decAmount - objRow.Cells("Amount").Value
                    End If
                Else
                    objRow.Cells("Amount").Value = decAmount
                    objRow.Cells("VAT").Value = decVat
                End If

        End Select
        If decAmount <> 0 Then
            objRow.Cells("VatPct").Value = Math.Round(objRow.Cells("Vat").Value * 100 / objRow.Cells("Amount").Value, 0)
        End If

        If decVatPct <> 0 Then
            objRow.Cells("VatPct").Value = decVatPct
        End If

        If intVatDiscount = 30 AndAlso objRow.Cells("VatPct").Value <> 0 AndAlso Not pblnTT78 Then
            objRow.Cells("VatPct").Value = objRow.Cells("VatPct").Value / 7 * 10
        End If

        objRow.Cells("Total").Value = objRow.Cells("Amount").Value + objRow.Cells("VAT").Value

        If strUnit <> "" Then
            objRow.Cells("Unit").Value = strUnit
        End If

        If intQuatity <> 0 Then
            objRow.Cells("Qty").Value = intQuatity
            objRow.Cells("Price").Value = objRow.Cells("Amount").Value \ intQuatity
        End If

        objRow.Cells("IsSum").Value = intIsSum
        Return True
    End Function
    '^_^20220802 add by 7643 -e-

    Private Sub cboCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomer.SelectedIndexChanged
        If mblnCustLoadCompleted Then
            Dim tblCustomer As System.Data.DataTable
            txtCustId.Text = cboCustomer.SelectedValue
            txtCustShortName.Text = cboCustomer.Text

            tblCustomer = GetDataTable("Select * From CustomerList where RecId=" & cboCustomer.SelectedValue, Conn)
            txtCustShortName.Text = tblCustomer.Rows(0)("CustShortName")
            txtTaxCode.Text = tblCustomer.Rows(0)("CustTaxCode")
            txtAddress.Text = tblCustomer.Rows(0)("CustAddress")
            txtCustomerFullName.Text = tblCustomer.Rows(0)("CustFullName")
            If pblnTestInv Then
                txtEmail.Text = "ketoan.sgn@transviet.com"
            Else
                txtEmail.Text = tblCustomer.Rows(0)("InvoiceEmail")
            End If

        End If

    End Sub

    Private Sub cboCustGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustGroup.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            If LoadComboDisplay(cboCustomer, "Select RecId as Value, CustShortName as Display" _
                & " from CustomerList " _
                & " where Status='OK' and RecId in (Select intVal from Misc where Status='OK'" _
                & " and CAT='CustNameInGroup' and VAL='" & cboCustGroup.Text & "')  order by CustShortName ", Conn) Then
                mblnCustLoadCompleted = True
            End If
        End If
    End Sub

    Private Sub cboCustType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustType.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            If LoadComboDisplay(cboCustomer, "Select RecId as Value, CustShortName as Display" _
                & " from CustomerList " _
                & " where Status='OK' and RecId in (Select CustId from Cust_Detail where Status='OK'" _
                & " and CAT='Channel' and VAL='" & cboCustType.Text & "') order by CustShortName", Conn) Then
                mblnCustLoadCompleted = True
            End If
        End If
    End Sub

    Private Sub frmE_InvEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mblnFirstLoadCompleted = True
    End Sub

    Private Sub chkOnceOff_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnceOff.CheckedChanged
        If chkOnceOff.Checked Then
            ClearCustDetails()
        End If

    End Sub
    Public Function ClearCustDetails() As Boolean
        txtCustomerFullName.Text = ""
        txtAddress.Text = ""
        txtTaxCode.Text = ""
        txtCustId.Text = 0
        If mintAdjustType <> 4 Then
            txtCustShortName.Text = ""
        End If
        txtCustomerFullName.Enabled = True
        txtAddress.Enabled = True
        txtTaxCode.Enabled = True
        Return True
    End Function
    Private Function ReplaceInvoice(Optional blnViewOnly As Boolean = False) As Boolean
        Dim objE_InvConnect As New clsE_InvConnect(pblnTT78, txtTvc.Text)
        If txtRecId.Text = 0 Then
            MsgBox("You must save Invoice details first!")
            Return False
        End If
        Dim objE_Invoice As New clsE_Invoice
        Dim lstProduct As New List(Of clsProduct)
        Dim strKindOfService As String = ""
        Dim intProdSeq As Integer
        Dim strSerialCert As String = String.Empty
        Dim decTax As Decimal = txtTax.Text

        For Each objRow As DataGridViewRow In dgrInvDetails.Rows
            Dim objProd As New clsProduct
            With objRow
                objProd.IsSum = .Cells("IsSum").Value
                If mintAdjustType <> 0 Then
                    If txtMauSo.Text.EndsWith("001") Then
                        objProd.ProdName = .Cells("Description").Value
                    Else
                        objProd.ProdName = .Cells("Tkno").Value
                        objProd.Extra1 = .Cells("Description").Value
                    End If
                Else
                    objProd.Extra1 = .Cells("Tkno").Value
                    objProd.ProdName = .Cells("Description").Value
                End If

                objProd.ProdUnit = .Cells("Unit").Value

                If IsNumeric(.Cells("Qty").Value) AndAlso .Cells("Qty").Value <> 0 Then
                    objProd.ProdQuantity = .Cells("Qty").Value
                End If

                If Not txtMauSo.Text.EndsWith("001") Then
                    objProd.TotalPrice = .Cells("Qty").Value * .Cells("Price").Value
                Else
                    objProd.TotalPrice = .Cells("Amount").Value
                End If

                objProd.ProdPrice = .Cells("Price").Value
                objProd.VatRate = .Cells("VatPct").Value
                objProd.VatAmount = .Cells("Vat").Value
                objProd.Amount = .Cells("Total").Value

                'Select Case .Cells("Total").Value
                '    Case > 0
                '        objProd.Amount = .Cells("Total").Value
                '    Case < 0
                '        objProd.DiscountAmount = Math.Abs(.Cells("Total").Value)
                '    Case 0
                '        'MsgBox("Amount must be <> 0")
                '        'Exit Sub
                'End Select

                If txtMauSo.Text.EndsWith("001") AndAlso objProd.VatRate <> -2 Then
                    'If txtMauSo.Text.EndsWith("001") AndAlso objProd.VatRate = .Cells("VatPct").Value <> -2 Then
                    intProdSeq = intProdSeq + 1
                    objProd.Seq = intProdSeq
                End If
            End With
            lstProduct.Add(objProd)
        Next

        strKindOfService = KindOfService.Hóa_đơn_GTGT

        If blnViewOnly Then
            Dim blnViewOK As Boolean = False
            If Not mblnNoOriInv AndAlso objE_Invoice.ReplaceInvoiceNoPublish(objE_InvConnect.BusinessServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                        , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                        , txtCustId.Text, txtCustomerFullName.Text _
                                        , txtAddress.Text, "", txtTaxCode.Text, cboFOP.Text _
                                        , txtInvId.Text, strKindOfService, InvoiceType.Hóa_đơn_thông_thường, lstProduct _
                                        , txtOriFkey.Text, mintAdjustType, txtMauSo.Text, txtKyHieu.Text, decTax _
                                        , CDec(txtCharge.Text), txtBuyer.Text, txtEmail.Text) Then
                blnViewOK = True
            ElseIf mblnNoOriInv AndAlso objE_Invoice.ReplaceInvoiceNoPublish(objE_InvConnect.BusinessServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                    , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                    , txtCustId.Text, txtCustomerFullName.Text _
                                    , txtAddress.Text, "", txtTaxCode.Text, cboFOP.Text _
                                    , txtInvId.Text, strKindOfService, InvoiceType.Hóa_đơn_thông_thường, lstProduct _
                                    , txtOriFkey.Text, mintAdjustType, txtMauSo.Text, txtKyHieu.Text, decTax _
                                    , CDec(txtCharge.Text), txtBuyer.Text, txtEmail.Text _
                                    , mstrOldInvPattern, mstrOldInvSerial, mintOldInvNo, mdteOldInvDOI) Then
                blnViewOK = True
            Else
                MsgBox("Unable to view Invoice" & vbNewLine & objE_Invoice.ResponseDesc)
                Return False
            End If
            If blnViewOK Then
                Dim frmShow As New frmShowHtml(objE_Invoice.ResponseDesc)
                frmShow.ShowDialog()
            End If
            Return True
        End If

        If mblnNoOriInv Then

            objE_Invoice.AdjustWithoutInv(objE_InvConnect.BusinessServiceUrl, objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                        , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                        , txtCustId.Text, txtCustomerFullName.Text _
                                        , txtAddress.Text, "", txtTaxCode.Text, cboFOP.Text _
                                        , txtInvId.Text, strKindOfService, InvoiceType.Hóa_đơn_thông_thường, lstProduct _
                                        , txtOriFkey.Text, mintAdjustType _
                                        , mstrOldInvPattern, mstrOldInvSerial, mintOldInvNo, mdteOldInvDOI _
                                        , txtMauSo.Text, txtKyHieu.Text, decTax _
                                        , CDec(txtCharge.Text), txtBuyer.Text, txtEmail.Text)
        Else
            objE_Invoice.ReplaceInvoiceAction(objE_InvConnect.BusinessServiceUrl _
                                , objE_InvConnect.UserName, objE_InvConnect.UserPass _
                                , objE_InvConnect.AccountName, objE_InvConnect.AccountPass _
                                , txtCustId.Text, txtCustomerFullName.Text _
                                , txtAddress.Text, "", txtTaxCode.Text, cboFOP.Text _
                                , txtInvId.Text, strKindOfService, InvoiceType.Hóa_đơn_thông_thường, lstProduct _
                                , txtOriFkey.Text, mintAdjustType, txtMauSo.Text, txtKyHieu.Text, decTax _
                                , CDec(txtCharge.Text), txtBuyer.Text, txtEmail.Text)
        End If



        If objE_Invoice.ReponseCode.StartsWith("OK") Then
            'Dim arrBreaks As String() = objE_Invoice.ReponseCode.Split("-")
            Dim arrMauSoKyHieu As String() = objE_Invoice.ReponseCode.Split(";")
            Dim arrKeyNbr As String() = arrMauSoKyHieu(2).Split("_")
            Dim intInvNo As Integer = 0
            If arrKeyNbr.Length = 2 Then
                intInvNo = arrKeyNbr(1)
            End If
            Dim lstQuerries As New List(Of String)
            lstQuerries.Add("Update lib.dbo." & mstrInvoiceTable & " set MauSo='" & Mid(arrMauSoKyHieu(0), 4) _
                    & "',KyHieu='" & arrMauSoKyHieu(1) & "',InvoiceNo=" & intInvNo _
                    & ",DOI=getdate() where Recid=" & txtRecId.Text)

            If txtOriFkey.Text <> 0 Then
                Dim intInvType As Integer = 1
                lstQuerries.Add(ChangeStatus_ByDK("lib.dbo." & mstrInvoiceTable, "XX", "InvId=" & txtOriFkey.Text))
                lstQuerries.Add(ChangeStatus_ByDK("lib.dbo." & mstrInvDetailTable, "XX", "InvId=" & txtOriFkey.Text))
                lstQuerries.Add(ChangeStatus_ByDK("lib.dbo." & mstrInvLinkTable, "XX", "InvId=" & txtOriFkey.Text))
                lstQuerries.Add("update lib.dbo." & mstrInvWebTable & " set invStatus='XX' where InvId=" _
                                & txtOriFkey.Text)

                If mblnNoOriInv Then
                    intInvType = 3
                    lstQuerries.Add("insert into lib.dbo.E_InvNotice (InvRecId, InvFkey, TVC, MauSo, KyHieu, InvoiceNo,DOI, MaCoQuanThue" _
                                & ",InvType, Action,NoOriInv,FstUser,City,NewInvRecId)" _
                                & " select RecId, cast(invid as varchar), TVC, MauSo, KyHieu, InvoiceNo,DOI, MaCoQuanThue," _
                                & intInvType & ",'3-Replace','true'," _
                                & myStaff.StaffId & ",'" & myStaff.City & "'," & txtRecId.Text _
                                & " from lib.dbo.E_inv" _
                                & " where InvId=" & txtOriFkey.Text)
                Else
                    lstQuerries.Add("insert into lib.dbo.E_InvNotice (InvRecId, InvFkey, TVC, MauSo, KyHieu, InvoiceNo,DOI, MaCoQuanThue" _
                                & ",InvType, Action,FstUser,City,NewInvRecId)" _
                                & " select RecId, cast(invid as varchar), TVC, substring(MauSo,1,1)" _
                                & ", KyHieu, InvoiceNo,DOI, MaCoQuanThue," & intInvType & ",'3-Replace'," _
                                & myStaff.StaffId & ",'" & myStaff.City & "'," & txtRecId.Text _
                                & " from lib.dbo." & mstrInvoiceTable _
                                & " where InvId=" & txtOriFkey.Text)
                End If
            End If
            MsgBox("Created E_Invoice Number:" & intInvNo)

            If UpdateListOfQuerries(lstQuerries, Conn_Web) Then
                Return True
            Else
                MsgBox("Unable to update E Invoice into RAS Database! " _
                           & vbNewLine & objE_Invoice.ResponseDesc _
                           & vbNewLine & ". Đề nghị báo người lập trình RAS!")
                Return False
            End If
        Else
            MsgBox("Unable to create E Invoice!" & vbNewLine _
                       & objE_Invoice.ResponseDesc)
            Return False
        End If


        Return True

    End Function
    Private Sub chkIssue2TV_CheckedChanged(sender As Object, e As EventArgs) Handles chkIssue2TV.CheckedChanged
        'If chkIssue2TV.Checked Then
        If pblnTT78 Then
            LoadDetailsBsp78(mstrRcp, mstrTkIds, chkIssue2TV.Checked)
        Else
            LoadDetailsBsp(mstrRcp, mstrTkIds, chkIssue2TV.Checked)
        End If

        '    'AutoFillTVTR(myStaff.City)
        'Else
        '    LoadDetailsBsp(mstrRcp, mstrTkIds, False)
        'End If
    End Sub

    Private Sub txtMauSo_TextChanged(sender As Object, e As EventArgs) Handles txtMauSo.TextChanged
        HideInvDetailsColumns(txtMauSo.Text.EndsWith("001"))

    End Sub

    Private Sub lbkSelectTVC_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelectTVC.LinkClicked
        SelectTvc()
    End Sub
    Private Function SelectTvc() As Boolean
        Dim frmSelectTvc As New frmSelectTVC(mstrTvc, mstrOldInvPatternRaw, mstrOldAl)
        If frmSelectTvc.ShowDialog = DialogResult.OK Then
            With frmSelectTvc.SelectedRow
                txtTvc.Text = .Cells("Tvc").Value
                txtBiz.Text = .Cells("Biz").Value
                txtAL.Text = .Cells("AL").Value
                txtMauSo.Text = CreateMauSo(.Cells("MauSo").Value)
                txtKyHieu.Text = CreateKyHieu(.Cells("KyHieu").Value)
                If Not txtMauSo.Text.EndsWith("001") Then
                    pnlSummary.Visible = True
                End If
            End With
            Return True
        End If
        Return False
    End Function
    Public Function LoadCustomerFromRcp(objRow As DataRow) As Boolean
        txtCustId.Text = objRow("CustId")
        txtCustShortName.Text = objRow("CustShortName")
        txtCustomerFullName.Text = objRow("PrintedCustName")
        txtAddress.Text = objRow("PrintedCustAddrr")
        txtTaxCode.Text = objRow("PrintedTaxCode")

    End Function
    Public Function LoadCustomer(objRow As DataRow) As Boolean
        txtCustId.Text = objRow("RecId")
        txtCustShortName.Text = objRow("CustShortName")
        txtCustomerFullName.Text = objRow("CustFullName")
        txtAddress.Text = objRow("CustAddress")
        txtTaxCode.Text = objRow("CustTaxCode")
        If pblnTestInv Then
            txtEmail.Text = "ketoan.sgn@transviet.com"
        Else
            txtEmail.Text = objRow("InvoiceEmail")
        End If

    End Function
    Private Sub lbkCreateDraftE_Invoice_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCreateDraftE_Invoice.LinkClicked
        Select Case lbkCreateDraftE_Invoice.Text
            Case "ApproveDraft"
                If ApproveDraftInv(pblnTT78, txtTvc.Text, txtMauSo.Text _
                            , txtKyHieu.Text, txtInvId.Text) Then
                    Me.DialogResult = DialogResult.OK
                End If
            Case "CreateDraft"
                If CreateE_Invoice(True) Then
                    Me.DialogResult = DialogResult.OK
                End If
        End Select

    End Sub

    Public Function SumGrandTotal() As Boolean
        Dim decTotal As Decimal
        Dim intMultiplier As Integer = 1
        If cboSRV.Text = "R" Then
            intMultiplier = -1
        End If
        If txtMauSo.Text.EndsWith("001") Then
            For Each objRow As DataGridViewRow In dgrInvDetails.Rows
                If IsNumeric(objRow.Cells("Total").Value) Then
                    decTotal = decTotal + objRow.Cells("Total").Value
                End If
            Next
        Else
            For Each objRow As DataGridViewRow In dgrInvDetails.Rows

                If IsNumeric(objRow.Cells("Price").Value) Then
                    If IsNumeric(objRow.Cells("Qty").Value) Then
                        decTotal = decTotal + (objRow.Cells("Qty").Value * objRow.Cells("Price").Value)
                    Else
                        decTotal = decTotal + objRow.Cells("Price").Value
                    End If
                ElseIf txtTvc.Text = "APG" AndAlso IsNumeric(objRow.Cells("Amount").Value) Then
                    decTotal = decTotal + objRow.Cells("Amount").Value
                End If
            Next
        End If

        If IsNumeric(txtTax.Text) Then
            decTotal = decTotal + txtTax.Text
        End If
        If IsNumeric(txtCharge.Text) Then
            decTotal = decTotal + (txtCharge.Text * intMultiplier)
        End If
        txtInvTotal.Text = Format(decTotal, "#,##0")
    End Function

    Private Sub dgrInvDetails_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles dgrInvDetails.CellValidated
        Dim strColumnName As String = dgrInvDetails.Columns(e.ColumnIndex).Name

        Select Case strColumnName
            Case "Amount", "Price"
                If IsNumeric(dgrInvDetails.Rows(e.RowIndex).Cells(strColumnName).Value) Then
                    dgrInvDetails.Rows(e.RowIndex).Cells(strColumnName).Value = dgrInvDetails.Rows(e.RowIndex).Cells(strColumnName).Value * 1.0

                    If strColumnName = "Price" And IsNumeric(dgrInvDetails.Rows(e.RowIndex).Cells("Qty").Value) Then
                        dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value = dgrInvDetails.Rows(e.RowIndex).Cells("Price").Value * dgrInvDetails.Rows(e.RowIndex).Cells("Qty").Value
                        dgrInvDetails.Rows(e.RowIndex).Cells("Total").Value = dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value + dgrInvDetails.Rows(e.RowIndex).Cells("VAT").Value
                    End If
                End If
                SumGrandTotal()
            Case "VAT"
                If IsNumeric(dgrInvDetails.Rows(e.RowIndex).Cells("Vat").Value) Then
                    If IsNumeric(dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value) Then
                        dgrInvDetails.Rows(e.RowIndex).Cells("Total").Value = Math.Round(dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value + dgrInvDetails.Rows(e.RowIndex).Cells("Vat").Value)
                    ElseIf dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value = "" Then
                        dgrInvDetails.Rows(e.RowIndex).Cells("Total").Value = Math.Round(0 + dgrInvDetails.Rows(e.RowIndex).Cells("Vat").Value)
                    End If

                End If
                SumGrandTotal()
            Case "VatPct"
                Select Case dgrInvDetails.Rows(e.RowIndex).Cells("VatPct").Value
                    Case -2, -1
                        dgrInvDetails.Rows(e.RowIndex).Cells("Vat").Value = 0
                        dgrInvDetails.Rows(e.RowIndex).Cells("Total").Value = RoundNearest(dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value)
                    Case 0, 5, 10, 8
                        If pblnTT78 Then
                            dgrInvDetails.Rows(e.RowIndex).Cells("Vat").Value = RoundNearest((dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value * dgrInvDetails.Rows(e.RowIndex).Cells("VatPct").Value / 100))
                        Else
                            dgrInvDetails.Rows(e.RowIndex).Cells("Vat").Value = RoundNearest((dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value * dgrInvDetails.Rows(e.RowIndex).Cells("VatPct").Value / 100) _
                            * (100 - cboGiamThue.Text) / 100)
                        End If
                        dgrInvDetails.Rows(e.RowIndex).Cells("Total").Value = Math.Round(dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value + dgrInvDetails.Rows(e.RowIndex).Cells("Vat").Value)

                    Case 7, 3.5
                        If Not pblnTT78 Then
                            If cboGiamThue.Text = 0 Then
                                MsgBox("Mức giảm thuế và % VAT không khớp nhau! Cần chỉnh lại cho phù hợp")
                            Else
                                dgrInvDetails.Rows(e.RowIndex).Cells("VatPct").Value = dgrInvDetails.Rows(e.RowIndex).Cells("VatPct").Value * 10 / 7
                            End If
                        End If
                        dgrInvDetails.Rows(e.RowIndex).Cells("Vat").Value = RoundNearest(dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value * dgrInvDetails.Rows(e.RowIndex).Cells("VatPct").Value / 100)
                        dgrInvDetails.Rows(e.RowIndex).Cells("Total").Value = Math.Round(dgrInvDetails.Rows(e.RowIndex).Cells("Amount").Value + dgrInvDetails.Rows(e.RowIndex).Cells("Vat").Value)
                    Case Else
                        MsgBox("VatPct must be 10, 8, 7, 5, 0, -1, -2")
                End Select
                SumGrandTotal()
            Case "Total"
                SumGrandTotal()
            Case "IsSum"
                Select Case dgrInvDetails.Rows(e.RowIndex).Cells("IsSum").Value
                    Case "0", "1", "2", "4"
                    Case Else
                        MsgBox("Must input one of the followings:" _
                               & vbNewLine & "0:Hàng hóa" _
                               & vbNewLine & "1:Khuyến mại" _
                               & vbNewLine & "2:Chiết khấu" _
                               & vbNewLine & "4:Ghi chú")
                        Return
                End Select
        End Select
    End Sub

    Private Sub txtTax_Validated(sender As Object, e As EventArgs) Handles txtTax.Validated
        If IsNumeric(txtTax.Text) Then
            SumGrandTotal()
        End If
    End Sub

    Private Sub txtCharge_Validated(sender As Object, e As EventArgs) Handles txtCharge.Validated
        If IsNumeric(txtCharge.Text) Then
            SumGrandTotal()
        End If
    End Sub


    Private Sub txtTvc_TextChanged(sender As Object, e As EventArgs) Handles txtTvc.TextChanged


        If txtTvc.Text.StartsWith("GDS") Or txtTvc.Text.StartsWith("TVTR") Then
            Dim strOldBU As String = cboBU.Text
            LoadCombo(cboBU, "SELECT strVal2 as value from lib.dbo.Misc where strVal1='" _
                  & txtTvc.Text & "' order by strVal2", Conn_Web)
            cboBU.SelectedIndex = cboBU.FindStringExact(strOldBU)
            cboDomInt.Enabled = True
            txtCodeTour.Enabled = True
            txtNbrOfPax.Enabled = True
        Else
            txtCodeTour.Enabled = False
            txtNbrOfPax.Enabled = False
            cboDomInt.Enabled = False
        End If
    End Sub

    Private Sub txtTvc_Validated(sender As Object, e As EventArgs) Handles txtTvc.Validated

        If txtTvc.Text.StartsWith("GDS") Or txtTvc.Text.StartsWith("TVTR") Then
            Dim strOldBU As String = cboBU.Text
            Dim strOldDomIn As String = cboDomInt.Text
            cboDomInt.Enabled = True
            txtCodeTour.Enabled = True
            txtNbrOfPax.Enabled = True

            LoadCombo(cboBU, "SELECT strVal2 as value from lib.dbo.Misc where strVal1='" _
                  & txtTvc.Text & "' order by strVal2", Conn_Web)

            cboBU.SelectedIndex = cboBU.FindStringExact(strOldBU)
            cboDomInt.SelectedIndex = cboDomInt.FindStringExact(strOldDomIn)

        Else
            cboDomInt.Enabled = False
            txtCodeTour.Enabled = False
            txtNbrOfPax.Enabled = False
        End If

    End Sub

    Private Sub lbkMoveDown_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkMoveDown.LinkClicked
        If txtInvId.Text = 0 Then
            Dim objSelectRow As DataGridViewRow = dgrInvDetails.CurrentRow
            Dim intNewIndex As Integer = dgrInvDetails.CurrentRow.Index + 1
            dgrInvDetails.Rows.Remove(objSelectRow)
            dgrInvDetails.Rows.Insert(intNewIndex, objSelectRow)
            For Each objRow As DataGridViewRow In dgrInvDetails.Rows
                objRow.Cells("RecId").Value = objRow.Index + 1
            Next
        End If

    End Sub

    Private Sub lbkMoveUp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkMoveUp.LinkClicked

    End Sub
    Private Function SelectVatDiscountLevel(intVatDiscount As Integer) As Boolean
        cboGiamThue.SelectedIndex = cboGiamThue.FindStringExact(intVatDiscount)
        Return True
    End Function

    Private Sub lbkEmailKtSGN_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkEmailKeToan.LinkClicked
        txtEmail.Text = GetInvoiceEmail4TV(myStaff.City)
    End Sub

    Private Sub lbkPreview_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkPreview.LinkClicked
        Select Case mintAdjustType
            Case 9
                ReplaceInvoice(True)
            Case > 0
                AdjustInvoice(True)
        End Select
    End Sub

    Private Sub lbkAddRowIsSum4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddRowIsSum4.LinkClicked
        If pblnTT78 Then
            Dim tblOld As New DataTable
            If dgrInvDetails.DataSource IsNot Nothing Then
                tblOld = dgrInvDetails.DataSource
                Dim objRow As DataRow
                objRow = tblOld.NewRow
                objRow("IsSum") = 4
                objRow("Amount") = 0
                objRow("Total") = 0
                objRow("VAT") = 0
                objRow("VatPct") = 0
                objRow("Price") = 0
                objRow("Qty") = 0
                objRow("Tkno") = ""
                objRow("Description") = ""
                tblOld.Rows.Add(objRow)
            Else
                dgrInvDetails.Rows.Add()
                dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("IsSum").Value = 4
                dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("Amount").Value = 0
                dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("Total").Value = 0
                dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("VAT").Value = 0
                dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("VatPct").Value = 0
                dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("Price").Value = 0
                dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("Qty").Value = 0
                dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("Tkno").Value = ""
                dgrInvDetails.Rows(dgrInvDetails.Rows.Count - 1).Cells("Description").Value = ""
            End If

        End If
    End Sub

    Private Sub lbkViewNoPay_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkViewOkdInv.LinkClicked
        ViewInv(txtTvc.Text, pblnTT78, mblnNoOriInv, mstrOldInvPatternRaw, mstrOldInvSerial, mintOldInvNo, "")
    End Sub

    Private Sub lbkClearData_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClearData.LinkClicked
        dgrInvDetails.DataBindings.Clear()
        dgrInvDetails.DataSource = Nothing
    End Sub

    Private Sub lbkCTSDOM_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCTSDOM.LinkClicked
        If cboBU.Enabled And cboDomInt.Enabled Then
            cboBU.SelectedIndex = cboBU.FindStringExact("CTS")
            cboDomInt.SelectedIndex = cboDomInt.FindStringExact("DOM")
        End If
    End Sub

    Private Sub lbkResetCustInfo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkResetCustInfo.LinkClicked
        ResetCustInfo()
    End Sub
    Private Function ResetCustInfo() As Boolean
        txtCustomerFullName.Text = ""
        txtAddress.Text = ""
        txtTaxCode.Text = ""
        txtEmail.Text = ""
        txtCustomerFullName.Enabled = True
        txtAddress.Enabled = True
        txtTaxCode.Enabled = True
        txtEmail.Enabled = True
    End Function
    Public Function FillCustInfoInvEmail2TV() As Boolean
        If mtblTkts.Rows.Count > 0 AndAlso mtblTkts.Rows(0)("InvEmail2TV") Then
            txtCustomerFullName.Text = mtblTkts.Rows(0)("PaxName")
            txtAddress.Text = ("").PadRight(15)
            txtTaxCode.Text = ""
        End If
    End Function
End Class