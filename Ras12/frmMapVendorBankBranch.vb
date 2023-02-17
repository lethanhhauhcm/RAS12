Public Class frmMapVendorBankBranch
    Private mstrBankCode As String
    Private mblnFirstLoadCompleted As Boolean

    Public Sub New(strBankCode As String, intVendorId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadDataGridView(dgrVendor, "Select * from Vendor where recid=" & intVendorId, Conn)
        mstrBankCode = strBankCode
    End Sub

    Private Sub frmMapVendorBankBranch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombo(cboBankProvince, "select distinct BankProvince as value from lib.dbo.[BankBranches] b left join lib.dbo.Bankids i on b.BankID=i.RecID where i.Status='OK' and UsedBy='" & mstrBankCode _
                                    & "' order by BankProvince ", Conn)
        Clear()
        Search()
        mblnFirstLoadCompleted = True
    End Sub
    Private Function Clear() As Boolean
        'chkFilteredByBankAdress.Checked = False
        'chkFilteredByBankName.Checked = False
        cboBankProvince.SelectedIndex = -1
        txtBankName.Text = ""
        Return True
    End Function
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select i.BankName, b.* from lib.dbo.BankBranches b left join lib.dbo.BankIDs i on i.RecId=b.BankId "
        Dim strFilter As String = " where b.Status='OK' and i.Status='ok'"
        If chkFilteredByBankAdress.Checked Then
            strQuerry = strQuerry & " left join lib.dbo.BankMapProvince m on m.Province=b.Province"
            strFilter = strFilter & " and m.BankAddress ='" & dgrVendor.Rows(0).Cells("BankAddress").Value & "'"
        End If
        If chkFilteredByVendorBankName.Checked Then
            strQuerry = strQuerry & " left join lib.dbo.BankMapName m on m.BankNameByBank=i.BankName"
            strFilter = strFilter & " and m.BankNameByTv ='" & dgrVendor.Rows(0).Cells("BankName").Value & "'"
        End If
        AddLikeConditionText(strQuerry, txtBankName)
        AddEqualConditionCombo(strQuerry, cboBankProvince)

        strQuerry = strQuerry & strFilter & " order by i.BankName"

        LoadDataGridView(dgrBankBranches, strQuerry, Conn)
        Return True
    End Function

    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        Clear()
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub

    Private Sub lbkMap_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkMap.LinkClicked
        Dim strQuerry As String = "insert into lib.dbo.BankMap (BranchId, VendorId, Status, FstUser, City) values (" & dgrBankBranches.CurrentRow.Cells("RecId").Value _
            & "," & dgrVendor.CurrentRow.Cells("RecId").Value & ",'OK','" & myStaff.SICode & "','" & myStaff.City & "')"

        If ExecuteNonQuerry(strQuerry, Conn) Then
            Me.DialogResult = DialogResult.OK
            Dispose()
        Else
            MsgBox("Unable to map BankBranch!")
        End If

    End Sub

    Private Sub chkFilteredByBankName_CheckedChanged(sender As Object, e As EventArgs) Handles chkFilteredByBankAdress.CheckedChanged, chkFilteredByVendorBankName.CheckedChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub
End Class