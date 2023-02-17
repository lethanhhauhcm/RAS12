Public Class frmCreateNonAirFund
    Public Sub New(objRow As DataGridViewRow)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        txtVendor.Text = objRow.Cells("Vendor").Value
        Dim tblOldFund As New System.Data.DataTable
        tblOldFund = GetDataTable("Select top 1 f.* from NonAirFund f" _
                                  & " left join Vendor c on f.VendorId=c.Recid" _
                                  & " where f.Status<>'XX' and c.Status<>'xx'" _
                                  & " and f.CreatedByItem=" & objRow.Cells("Recid").Value _
                                  & " order by f.RecId desc")
        If tblOldFund.Rows.Count = 0 Then
            txtVendorId.Text = objRow.Cells("VendorId").Value
            txtVendor.Text = objRow.Cells("Vendor").Value
            txtCur.Text = objRow.Cells("CCurr").Value
            txtAmount.Text = objRow.Cells("Qty").Value * objRow.Cells("Cost").Value
            txtCreatedByItem.Text = objRow.Cells("Recid").Value
            txtDescription.Text = ScalarToString("Dutoan_Tour", "TCode" _
                                                 , " where Status<>'xx' and RecId=" _
                                                 & objRow.Cells("Dutoanid").Value)
        Else
            With tblOldFund
                txtFundId.Text = .Rows(0)("RecId")
                txtVendorId.Text = .Rows(0)("VendorId")
                txtVendor.Text = .Rows(0)("Vendor")
                txtAmount.Text = .Rows(0)("Amount")
                txtCur.Text = .Rows(0)("Cur")
                dtpExpiryDate.Value = .Rows(0)("ExpiryDate")
                txtCreatedByItem.Text = .Rows(0)("CreatedByItem")
                txtDescription.Text = .Rows(0)("Description")
                lbkOK.Enabled = False
                txtAmount.Enabled = False
            End With

        End If

    End Sub

    Private Sub frmCreateNonAirFund_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpExpiryDate.MinDate = Now
    End Sub

    Private Sub lbkOK_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkOK.LinkClicked
        If Not IsNumeric(txtAmount.Text) Then
            MsgBox("Invalid Amount")
            Exit Sub
        End If
        Dim lstQuerries As New List(Of String)
        Dim intNewFundRecord As Integer

        lstQuerries.Add("Insert into NonAirFund (FundId,Cur,Amount,VendorId,ExpiryDate" _
                        & ",CreatedByItem,FstUser,Description) values (" _
                        & "(select ISNULL(max( fundid),0)+ 1 from NonAirFund),'" _
                        & txtCur.Text & "'," & txtAmount.Text & ",'" & txtVendorId.Text _
                        & "','" & CreateToDate(dtpExpiryDate.Value) _
                        & "'," & txtCreatedByItem.Text _
                        & ",'" & myStaff.SICode & "','" & txtDescription.Text & "')")
        If UpdateListOfQuerries(lstQuerries, Conn, True, intNewFundRecord) Then
            Me.Dispose()

        Else
            MsgBox("Unable to add Fund")
        End If
    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub txtVendor_TextChanged(sender As Object, e As EventArgs) Handles txtVendor.TextChanged

    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged

    End Sub

    Private Sub dtpExpiryDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpExpiryDate.ValueChanged

    End Sub

    Private Sub txtFundId_TextChanged(sender As Object, e As EventArgs) Handles txtFundId.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub txtCur_TextChanged(sender As Object, e As EventArgs) Handles txtCur.TextChanged

    End Sub

    Private Sub txtVendorId_TextChanged(sender As Object, e As EventArgs) Handles txtVendorId.TextChanged

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub txtCreatedByItem_TextChanged(sender As Object, e As EventArgs) Handles txtCreatedByItem.TextChanged

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub txtDeductedByItem_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub
End Class