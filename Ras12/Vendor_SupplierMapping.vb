Public Class Vendor_SupplierMapping
    Sub New(pSupplierID As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LblCreateVendor_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblCreateVendor.LinkClicked
        Dim f As New UNC_support("COMPANY", "ACT")
        f.ShowDialog()
    End Sub

    Private Sub Vendor_SupplierMapping_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub Vendor_SupplierMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGridSupplier()
        If Me.txtSupplierID.Text <> 0 Then
            Me.LblRef.Text = ScalarToString("Supplier", "select FullName + ' / ' + Address", " where RecID=" & Me.txtSupplierID.Text)
            Me.TabControl1.SelectTab("TabPage2")
        End If
    End Sub
    Private Sub LoadGridSupplier()
        Dim strQuerry As String
        strQuerry = "select RecID, FullName, Address ,FstUpdate from Supplier " &
                                                  " where vendorID=0 and status='OK'"
        If txtFullName.Text <> "" Then
            strQuerry = strQuerry & " and FullName like '%" & txtFullName.Text & "%'"
        End If
        If IsNumeric(txtPendingSupplierId.Text) Then
            strQuerry = strQuerry & " and RecId=" & txtPendingSupplierId.Text
        End If
        Me.GridSupplier.DataSource = GetDataTable(strQuerry)
        Me.GridSupplier.Columns("RecId").Width = 48
        Me.GridSupplier.Columns("FullName").Width = 256
        Me.GridSupplier.Columns(2).Width = 512
        Me.LblCreateVendor.Visible = False
        Me.LblRef.Text = ""
        txtFullName.Visible = True
        lbkFilter.Visible = True
    End Sub
    Private Sub GridSupplier_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridSupplier.CellContentDoubleClick
        Me.TabControl1.SelectTab("TabPage2")
        Me.LblRef.Text = Me.GridSupplier.CurrentRow.Cells(1).Value & " / " & Me.GridSupplier.CurrentRow.Cells(2).Value
        Me.txtSupplierID.Text = Me.GridSupplier.CurrentRow.Cells("RecID").Value
        Me.LblCreateVendor.Visible = True
    End Sub
    Private Sub LblSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblSearch.LinkClicked
        Me.LblMap.Visible = False
        Dim StrSQL As String = "Select RecID, ShortName from Vendor where status <>'XX'"
        If Me.OptAccountNo.Checked Then
            StrSQL = StrSQL & " and RecID in (select CompanyID from UNC_Accounts where replace(accountNumber,' ','') like '%" & Me.TxtSearch.Text.Replace(" ", "") & "' and status <>'XX')"
        ElseIf Me.OptSupplierAddrress.Checked Then
            StrSQL = StrSQL & " and RecID in (select VendorID from Supplier where address like '%" & Me.TxtSearch.Text & "%' and status <>'XX' and vendorID <>0)"
        ElseIf Me.OptSupplierName.Checked Then
            StrSQL = StrSQL & " and RecID in (select VendorID from Supplier where FullName like '%" & Me.TxtSearch.Text & "%' and status <>'XX' and vendorID <>0)"
        ElseIf Me.OptVendorName.Checked Then
            StrSQL = StrSQL & " and ShortName like '%" & Me.TxtSearch.Text & "%' "
        Else
            Exit Sub
        End If
        Me.GridVendor.DataSource = GetDataTable(StrSQL)
        Me.GridVendor.Columns(1).Width = 256
    End Sub

    Private Sub GridVendor_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridVendor.CellContentClick
        Me.LblMap.Visible = True
    End Sub

    Private Sub LblMap_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblMap.LinkClicked
        If Me.txtSupplierID.Text = 0 Then Exit Sub
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        cmd.CommandText = "update Supplier set VendorID=" & Me.GridVendor.CurrentRow.Cells("RecID").Value & " where RecID=" &
                Me.txtSupplierID.Text
        cmd.ExecuteNonQuery()

        Me.TabControl1.SelectTab("TabPage1")
        LoadGridSupplier()
    End Sub

    Private Sub OptSupplierName_CheckedChanged(sender As Object, e As EventArgs) Handles _
        OptSupplierName.CheckedChanged, OptAccountNo.CheckedChanged, OptSupplierAddrress.CheckedChanged, OptVendorName.CheckedChanged
        If Me.OptAccountNo.Checked Then
            Me.LblContains.Text = "Equals"
        Else
            Me.LblContains.Text = "Contains"
        End If
    End Sub

    Private Sub lbkFilterByName_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        LoadGridSupplier()
    End Sub

    Private Sub TabPage2_Enter(sender As Object, e As EventArgs) Handles TabPage2.Enter
        txtFullName.Visible = False
        lbkFilter.Visible = False
    End Sub

    Private Sub TabPage1_Enter(sender As Object, e As EventArgs) Handles TabPage1.Enter
        txtFullName.Visible = True
        lbkFilter.Visible = True
    End Sub

    Private Sub lbkFilterByName_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkFilter.LinkClicked
        LoadGridSupplier()
    End Sub
End Class
