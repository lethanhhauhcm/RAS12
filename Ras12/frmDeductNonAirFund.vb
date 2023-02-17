Public Class frmDeductNonAirFund
    Private mtblOldFund As System.Data.DataTable
    Private mobjOldSvc As DataGridViewRow
    Public Sub New(objRow As DataGridViewRow)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mobjOldSvc = objRow
        txtVendor.Text = objRow.Cells("Vendor").Value
        mtblOldFund = GetDataTable("Select top 1 f.*,ShortName from NonAirFund f" _
                                  & " left join Vendor c on f.VendorId=c.Recid" _
                                  & " where f.Status='OK' and c.Status<>'xx' and Amount>0" _
                                  & " and f.VendorId=" & objRow.Cells("VendorId").Value _
                                  & " and ExpiryDate>=getdate()" _
                                  & " order by f.RecId desc")
        If mtblOldFund.Rows.Count = 0 Then
            MsgBox("No Fund available")
            Me.DialogResult = DialogResult.Abort
            Me.Dispose()
        ElseIf objRow.Cells("CCurr").Value <> mtblOldFund.Rows(0)("Cur") Then
            MsgBox("Different Currency.Unable to use Fund")
            Me.Dispose()
        Else
            With mtblOldFund
                txtFundId.Text = .Rows(0)("FundId")
                txtVendorId.Text = .Rows(0)("VendorId")
                txtVendor.Text = .Rows(0)("ShortName")
                txtAmount.Text = .Rows(0)("Amount")
                txtCur.Text = .Rows(0)("Cur")
                dtpExpiryDate.Value = .Rows(0)("ExpiryDate")
                txtCreatedByItem.Text = .Rows(0)("CreatedByItem")
                txtDescription.Text = .Rows(0)("Description")
                If txtAmount.Text <= objRow.Cells("Qty").Value * objRow.Cells("Cost").Value Then
                    txtDeductedAmount.Text = txtAmount.Text
                Else
                    txtDeductedAmount.Text = objRow.Cells("Qty").Value * objRow.Cells("Cost").Value
                End If
            End With

        End If

    End Sub

    Private Sub frmCreateNonAirFund_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpExpiryDate.MinDate = Now
    End Sub

    Private Sub lbkOK_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkOK.LinkClicked
        If Not IsNumeric(txtDeductedAmount.Text) _
            Or txtDeductedAmount.Text > txtAmount.Text Then
            MsgBox("Invalid Deducted Amount")
            Exit Sub
        End If
        Dim lstQuerries As New List(Of String)
        Dim intNewServiceRecord As Integer

        lstQuerries.Add("insert DuToan_Item (Service, CCurr, Qty, Cost, Supplier" _
                        & ", VendorID, Vendor, FstUser, PmtMethod" _
                        & ", DuToanID, Brief, SVCDate" _
                        & ", SupplierID,CostOnly) Values ('VendorPendingBalance" _
                        & "','" & mobjOldSvc.Cells("CCurr").Value _
                        & "',1," & 0 - txtDeductedAmount.Text _
                        & ",'" & mobjOldSvc.Cells("Supplier").Value _
                        & "'," & mobjOldSvc.Cells("VendorId").Value _
                        & ",'" & mobjOldSvc.Cells("Vendor").Value _
                        & "','" & myStaff.SICode & "','" & mobjOldSvc.Cells("PmtMethod").Value _
                        & "'," & mobjOldSvc.Cells("DutoanId").Value _
                        & ",'" & mtblOldFund.Rows(0)("Description") _
                        & "','" & CreateFromDate(mobjOldSvc.Cells("SVCDate").Value) _
                        & "'," & mobjOldSvc.Cells("SupplierId").Value _
                        & ",'" & chkCostOnly.Checked & "')")

        If Not UpdateListOfQuerries(lstQuerries, Conn, True, intNewServiceRecord) Then
            GoTo Quit
        End If
        lstQuerries.Clear()

        lstQuerries.Add("Update NonAirFund set Status='RR',LstUpdate=getdate(),LstUser='" _
                            & myStaff.SICode & "' where RecId=" & mtblOldFund.Rows(0)("RecId"))
        lstQuerries.Add("Insert into NonAirFund (FundId,Cur,Amount,VendorId,ExpiryDate" _
                        & ",CreatedByItem,FstUser,Description) values ('" _
                        & txtFundId.Text & "','" & txtCur.Text & "'," & txtAmount.Text - txtDeductedAmount.Text _
                        & ",'" & txtVendorId.Text _
                        & "','" & CreateToDate(dtpExpiryDate.Value) _
                        & "'," & intNewServiceRecord & ",'" & myStaff.SICode _
                        & "','" & txtDescription.Text & "')")

        If UpdateListOfQuerries(lstQuerries, Conn) Then
            Me.Dispose()
            Exit Sub
        End If
Quit:
        MsgBox("Unable to Deduct Fund")

    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub
End Class