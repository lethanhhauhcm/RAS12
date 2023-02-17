Public Class frmCustomerEdit
    Private objCust As New objCustomer

    Public Sub New(Optional objRow As DataGridViewRow = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If objRow Is Nothing Then
            If myStaff.City = "SGN" Then
                txtEmail.Text = "ketoan.sgn@transviet.com"
            ElseIf myStaff.City = "HAN" Then
                txtEmail.Text = "ketoan.han@transviet.com"
            End If
        Else
            txtRecId.Text = objRow.Cells("RecId").Value
            txtCustShortName.Text = objRow.Cells("CustShortName").Value
            txtCustFullName.Text = objRow.Cells("CustFullName").Value
            txtCustTaxCode.Text = objRow.Cells("CustTaxCode").Value
            txtCustAddress.Text = objRow.Cells("CustAddress").Value
            txtInvoiceEmail.Text = objRow.Cells("InvoiceEmail").Value
            txtEmail.Text = objRow.Cells("Email").Value
        End If
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim tmpCustid As Integer
        Dim strEmail As String = ""
        If Not CheckInputFormat() Then Exit Sub

        If txtRecId.Text = 0 Then
            tmpCustid = objCust.AddCustomer(Me.txtCustShortName.Text, Me.txtCustFullName.Text,
            Me.txtCustTaxCode.Text, Me.txtCustAddress.Text, strEmail, "", "OK", txtInvoiceEmail.Text)
            If tmpCustid = 0 Then
                MsgBox("Unable to create Customer")
                Exit Sub
            End If
            objCust.InsertCustDetail(tmpCustid, "Channel", "AP", False)
        Else
            If objCust.SaveChange(Me.txtCustFullName.Text, Me.txtCustTaxCode.Text _
                          , txtEmail.Text, Me.TxtPhone.Text, txtCustAddress.Text, txtRecId.Text _
                          , myStaff.City, txtInvoiceEmail.Text) Then
            Else
                MsgBox("Unable to Edit Customer")
                Exit Sub
            End If
        End If
        SingleUpLoadCustomer2VNPT(tmpCustid)
        Me.DialogResult = DialogResult.OK
    End Sub
    Private Function CheckInputFormat() As Boolean
        Dim tblExistCust As DataTable
        If Not CheckFormatEmails(txtEmail.Text, False) Then Return False
        If Not CheckFormatEmails(txtInvoiceEmail.Text, False) Then Return False
        If Not CheckFormatTextBox(txtCustShortName,, 2, 31) Then Return False
        If Not CheckFormatTextBox(txtCustFullName,, 2, 128) Then Return False

        tblExistCust = GetDataTable("select * from customerlist where CustShortName='" _
                                   & txtCustShortName.Text & "' and RecId<>" & txtRecId.Text)
        If tblExistCust.Rows.Count > 0 Then
            Dim frmShow As New frmShowTableContent(tblExistCust, "Duplicated CustShortName!")
            frmShow.ShowDialog()
        End If

        Return True
    End Function

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub
End Class