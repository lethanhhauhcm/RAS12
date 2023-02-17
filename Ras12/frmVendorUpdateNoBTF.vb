Public Class frmVendorUpdateNoBTF
    Private Enum FAction
        Add
        Edit
    End Enum
#Region "CustomMethod"
    Private Sub LoadVendorUpdateNoBTF()
        Dim mSQL As String
        mSQL = "select mis.RecID,vd.ShortName Vendor,mis.VAL AccountName,mis.VAL1 AccountNo,mis.VAL2 Bank,hn.TenVietTat UserRequest,mis.FstUpdate,mis.FstUser,mis.LstUpdate," &
               "       mis.LstUser,mis.IntVal,mis.IntVal1 " &
               "from MISC mis " &
               "    left join Vendor vd on mis.IntVal=vd.RecID And vd.Status<>'XX' and vd.City=mis.City " &
               "    left join HR_NhanVien hn on mis.IntVal1=hn.RecID And hn.Status='OK' and hn.HR_City=mis.City " &
               "where mis.Status='OK' and mis.City='" & myStaff.City & "' and mis.CAT='VendorUpdateNoBTF' " &
               "order by vd.ShortName,mis.VAL,mis.VAL1,mis.VAL2,hn.TenVietTat "
        LoadDataGridView(dgvVendorUpdateNoBTF, mSQL, Conn)
    End Sub

    Private Sub LoadCombobox()
        Dim mSQL As String

        mSQL = "select RecID Value,ShortName Display " &
               "from Vendor " &
               "where Status<>'XX' and City='" & myStaff.City & "' " &
               "order by ShortName "
        LoadComboDisplay(cboVendor, mSQL, Conn)

        mSQL = "select RecID Value,TenVietTat Display " &
               "from HR_NhanVien " &
               "where Status='OK' and HR_City='" & myStaff.City & "' " &
               "order by TenVietTat "
        LoadComboDisplay(cboUserRequest, mSQL, Conn)
    End Sub

    Private Function CheckDuplicate() As Boolean
        If txtAccountName.Text = "" Or txtAccountNo.Text = "" Or txtBank.Text = "" Then Return False
        If ScalarToString("MISC", "RecID", "IntVal=" & cboVendor.SelectedValue.ToString & " and IntVal1='" & cboUserRequest.SelectedValue.ToString & "' and Status='OK'") <> "" Then
            MsgBox("VendorUpdateNoBTF already exists!")
            Return True
        End If

        Return False
    End Function

    Private Function CheckValue(xAction As FAction) As Boolean
        Dim mDt As New DataTable
        Dim mMsg, mSQL, mVendor As String
        Dim i As Integer

        mMsg = ""
        If txtAccountName.Text = "" Then mMsg = "AccountName"
        If txtAccountNo.Text = "" Then mMsg &= IIf(mMsg <> "", ",", "") & "AccountNo"
        If txtBank.Text = "" Then mMsg &= IIf(mMsg <> "", ",", "") & "Bank"
        If mMsg <> "" Then
            MsgBox(mMsg & " be empty!")
            Return False
        End If

        mVendor = ""
        mSQL = "select vd.ShortName " &
               "from MISC mis " &
               "    left join Vendor vd on mis.IntVal=vd.RecID And vd.Status<>'XX' and vd.City=mis.City " &
               "where VAL='" & txtAccountName.Text & "' and mis.Status='OK' and mis.City='" & myStaff.City & "' and mis.CAT='VendorUpdateNoBTF'"
        mDt = GetDataTable(mSQL, Conn)
        For i = 0 To mDt.Rows.Count - 1
            mVendor &= IIf(mVendor <> "", ",", "") & "'" & mDt.Rows(i)("ShortName") & "'"
        Next

        If mVendor <> "" Then
            If MsgBox("TÊN TÀI KHOẢN BỊ TRÙNG VỚI VENDOR " & mVendor & " " & vbLf & "CONTINUE ADD?", vbYesNo) = vbNo Then Return False
        End If

        Return True
    End Function
#End Region

    Private Sub frmVendorUpdateNoBTF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombobox()
        LoadVendorUpdateNoBTF()
    End Sub

    Private Sub txtAccountNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAccountNo.KeyPress
        PressInteger(e)
    End Sub

    Private Sub llbSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbRefresh.LinkClicked
        LoadVendorUpdateNoBTF()
    End Sub

    Private Sub dgvVendorUpdateNoBTF_DataSourceChanged(sender As Object, e As EventArgs) Handles dgvVendorUpdateNoBTF.DataSourceChanged
        If dgvVendorUpdateNoBTF.Rows.Count = 0 Or Not myStaff.HasExtraRight("EditVendorUpdateNoBTF") Then
            llbEdit.Enabled = False
            llbDelete.Enabled = False
        Else
            llbEdit.Enabled = True
            llbDelete.Enabled = True
        End If
    End Sub

    Private Sub llbAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbAdd.LinkClicked
        Dim mSQL As String

        If CheckDuplicate() OrElse Not CheckValue(FAction.Add) Then Exit Sub

        mSQL = "insert into MISC(IntVal,VAL,VAL1,VAL2,IntVal1," &
               "                 CAT,Status,City,FstUser) " &
               "values(" & cboVendor.SelectedValue.ToString & ",'" & txtAccountName.Text & "','" & txtAccountNo.Text & "','" & txtBank.Text & "'," &
               "       " & cboUserRequest.SelectedValue.ToString & "," &
               "       'VendorUpdateNoBTF','OK','" & myStaff.City & "','" & myStaff.SICode & "')"
        ExecuteNonQuerry(mSQL, Conn)
        LoadVendorUpdateNoBTF()
    End Sub

    Private Sub llbEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEdit.LinkClicked
        Dim mSQL As String

        If MsgBox("Are you sure edit?", vbYesNo) = vbNo Then Exit Sub

        If Not CheckValue(FAction.Edit) Then Exit Sub

        mSQL = "update MISC " &
               "set IntVal=" & cboVendor.SelectedValue.ToString & ",VAL='" & txtAccountName.Text & "',VAL1='" & txtAccountNo.Text & "',VAL2='" & txtBank.Text & "'," &
               "    IntVal1=" & cboUserRequest.SelectedValue.ToString & ",LstUpdate=getdate(),LstUser='" & myStaff.SICode & "' " &
               "where RecID=" & dgvVendorUpdateNoBTF.CurrentRow.Cells("RecID").Value.ToString & ""
        ExecuteNonQuerry(mSQL, Conn)
        LoadVendorUpdateNoBTF()
    End Sub

    Private Sub llbDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbDelete.LinkClicked
        Dim mSQL As String

        If MsgBox("Are you sure delete?", vbYesNo) = vbNo Then Exit Sub

        mSQL = "update MISC " &
               "set Status='XX',LstUpdate=getdate(),LstUser='" & myStaff.SICode & "' " &
               "where RecID=" & dgvVendorUpdateNoBTF.CurrentRow.Cells("RecID").Value.ToString & ""
        ExecuteNonQuerry(mSQL, Conn)
        LoadVendorUpdateNoBTF()
    End Sub

    Private Sub dgvVendorUpdateNoBTF_SelectionChanged(sender As Object, e As EventArgs) Handles dgvVendorUpdateNoBTF.SelectionChanged
        cboVendor.SelectedValue = dgvVendorUpdateNoBTF.CurrentRow.Cells("IntVal").Value
        txtAccountName.Text = dgvVendorUpdateNoBTF.CurrentRow.Cells("AccountName").Value
        txtAccountNo.Text = dgvVendorUpdateNoBTF.CurrentRow.Cells("AccountNo").Value
        txtBank.Text = dgvVendorUpdateNoBTF.CurrentRow.Cells("Bank").Value
        cboUserRequest.SelectedValue = dgvVendorUpdateNoBTF.CurrentRow.Cells("IntVal1").Value
    End Sub

    Private Sub cboVendor_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboVendor.Validating
        Dim mVendorID As Integer

        mVendorID = ScalarToInt("Vendor", "RecID", "Status<>'XX' and City='" & myStaff.City & "' and ShortName='" & cboVendor.Text & "'")
        If mVendorID = 0 Then
            MsgBox("Vendor don't exists!")
            e.Cancel = True
        Else
            cboVendor.SelectedValue = mVendorID
        End If
    End Sub

    Private Sub cboUserRequest_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboUserRequest.Validating
        Dim mUserID As Integer

        mUserID = ScalarToInt("HR_NhanVien", "RecID", "Status='OK' and HR_City='" & myStaff.City & "' and TenVietTat='" & cboUserRequest.Text & "'")
        If mUserID = 0 Then
            MsgBox("User don't exists!")
            e.Cancel = True
        Else
            cboUserRequest.SelectedValue = mUserID
        End If
    End Sub
End Class