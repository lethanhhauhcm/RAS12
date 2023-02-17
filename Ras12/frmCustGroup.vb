Public Class frmCustGroup
    Public mstrSelectedCustGroup As String
    Public Sub New(Optional blnSelectOnly As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If blnSelectOnly Then
            lbkClone.Visible = False
            lbkSelect.Visible = True
            dgrCustomers.Height = 520
            txtNewGroupName.Visible = False
            lbkNew.Visible = False
            lbkRemove.Visible = False
            dgrCustomerList.Visible = False
            txtCustShortName.Visible = False
            lbkSearchCust.Visible = False
            lbkAddCust.Visible = False
        End If
    End Sub

    Private Sub frmCustGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strSelectCust As String = "select distinct Val1 as Display, intVal1 as Value " _
            & " from Misc where cat ='CustGrp4HotelRate'" _
            & " AND status='OK' order by Val1"
        LoadComboDisplay(cboCustomer, strSelectCust, Conn)
        cboCustomer.SelectedIndex = -1
        Search()
    End Sub


    Private Sub lbkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClear.LinkClicked
        cboCustomer.SelectedIndex = -1
    End Sub

    Private Sub lbkSearch_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearch.LinkClicked
        Search()
    End Sub
    Private Function Search() As Boolean
        Dim strSelectCustGrp As String = "select distinct Val as CustGroup from Misc where cat ='CustGrp4HotelRate'" _
            & " AND status='OK' order by Val"

        AddEqualConditionCombo(strSelectCustGrp, cboCustomer, "Val1")
        LoadDataGridView(dgrCustGroup, strSelectCustGrp, Conn)
        Return True
    End Function
    Private Function Find(strCustGroup As String)
        For Each objRow As DataGridViewRow In dgrCustGroup.Rows
            If objRow.Cells("CustGroup").Value = strCustGroup Then
                dgrCustGroup.CurrentCell = objRow.Cells("CustGroup")
                Exit For
            End If
        Next
        Return True
    End Function
    Private Function LoadCustomer() As Boolean
        Dim strSelectCust As String = "select RecId, Val1 as CustShortName, intVal1 as CustId" _
                & " from Misc where cat ='CustGrp4HotelRate' and status='OK'" _
                & " And Val='" & dgrCustGroup.CurrentRow.Cells("CustGroup").Value _
                & "' order by Val1"
        dgrCustomers.DataSource = Nothing
        dgrCustomers.Columns.Clear()
        LoadDataGridView(dgrCustomers, strSelectCust, Conn)
    End Function
    Private Sub lbkNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub lbkEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        If dgrCustGroup.CurrentRow Is Nothing Then Exit Sub
        If ScalarToInt("HotelRates", "RecId", "CustGroup='" & dgrCustGroup.CurrentRow.Cells("CustGroup").Value & "'") Then
            MsgBox("Không được phép sửa CustGroup đang sử dụng")
        End If
    End Sub

    Private Sub lbkDelete_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub dgrCustGroup_SelectionChanged(sender As Object, e As EventArgs) Handles dgrCustGroup.SelectionChanged
        LoadCustomer()
        txtNewGroupName.Enabled = False
        txtNewGroupName.Text = ""
        lbkNew.Text = "NewGroup"
    End Sub

    Private Sub lbkAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkNew.LinkClicked
        If lbkNew.Text = "NewGroup" Then
            txtNewGroupName.Enabled = True
            txtNewGroupName.Focus()
            dgrCustomers.DataSource = Nothing
            dgrCustomers.Columns.Add("RecId", "RecId")
            dgrCustomers.Columns.Add("CustShortName", "CustShortName")
            dgrCustomers.Columns.Add("CustId", "CustId")
            lbkNew.Text = "Save"
            Exit Sub
        End If
        If Not CheckInputValues() Then Exit Sub
        Dim lstQuerries As New List(Of String)
        For Each objRow As DataGridViewRow In dgrCustomers.Rows
            lstQuerries.Add("insert Misc (Cat,Status,Val,Val1,intVal1,FstUser)" _
                                & " values ('CustGrp4HotelRate','OK','" _
                                & txtNewGroupName.Text & "','" & objRow.Cells("CustShortName").Value _
                                & "'," & objRow.Cells("CustId").Value & ",'" & myStaff.SICode & "')")
        Next
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            Search()
        Else
            MsgBox("Unable to Add Customer Group Name")
        End If
    End Sub
    Private Function CheckInputValues() As Boolean
        txtNewGroupName.Text = txtNewGroupName.Text.Trim
        If txtNewGroupName.Text = "" Then
            MsgBox("Cần nhập CustGroup Name")
            Return False
        ElseIf CustGroupExist(txtNewGroupName.Text) Then
            MsgBox("Cần dùng CustGroup Name mới")
            Return False
        End If
        If dgrCustomers.RowCount = 0 Then
            MsgBox("Cần thêm Customer vào Group")
            Return False
        End If
        Return True
    End Function
    Private Function CustGroupExist(strGroupName As String) As Boolean
        If ScalarToInt("Misc", "RecId", "Status='OK' and Val='" & strGroupName & "'") > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CustGroupInUse(strGroupName As String) As Boolean
        If ScalarToInt("HotelRates", "RecId", "Status='OK' and CustGroup='" & strGroupName & "'") > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub lbkClone_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkClone.LinkClicked
        For Each objRow As DataGridViewRow In dgrCustomers.Rows
            objRow.Cells("RecId").Value = 0
        Next
        txtNewGroupName.Enabled = True
        lbkNew.Text = "Save"
    End Sub

    Private Sub lbkRemove_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRemove.LinkClicked
        If dgrCustomers.Rows.Count = 0 Then Exit Sub
        If dgrCustomers.Rows(0).Cells("RecId").Value <> 0 Then
            If CustGroupInUse(dgrCustGroup.CurrentRow.Cells("CustGroup").Value) Then
                MsgBox("CustGroup đang sử dụng, không được thêm/bớt Customer")
                Exit Sub
            Else
                If ExecuteNonQuerry(ChangeStatus_ByID("Misc", "XX" _
                                , dgrCustomers.CurrentRow.Cells("RecId").Value), Conn) Then
                    dgrCustomers.Rows.Remove(dgrCustomers.CurrentRow)
                Else
                    MsgBox("Lỗi không bỏ được Customer")
                End If
            End If
        Else
            dgrCustomers.Rows.Remove(dgrCustomers.CurrentRow)
        End If
    End Sub

    Private Sub lbkSearchCust_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearchCust.LinkClicked
        Dim strQuerries As String = "Select RecId as CustId, CustShortName,CustFullName" _
            & " from CustomerList where Status='OK' and City='" & myStaff.City & "'"
        AddLikeConditionText(strQuerries, txtCustShortName)
        LoadDataGridView(dgrCustomerList, strQuerries, Conn)
    End Sub

    Private Sub lblAddCust_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAddCust.LinkClicked

        If dgrCustomerList.CurrentRow Is Nothing Then
            MsgBox("You must select Customer first!")
            Exit Sub
        End If
        If txtNewGroupName.Enabled Then
            dgrCustomers.Rows.Add({0, dgrCustomerList.CurrentRow.Cells("CustShortName").Value, dgrCustomerList.CurrentRow.Cells("CustId").Value})
        ElseIf CustGroupInUse(dgrCustGroup.CurrentRow.Cells("CustGroup").Value) Then
            MsgBox("Không được thêm Customer cho Group đang được sử dụng")
            Exit Sub
        ElseIf ExecuteNonQuerry("insert Misc (Cat,Status,Val,Val1,intVal1,FstUser) values ('CustGrp4HotelRate','OK','" _
                                & dgrCustGroup.CurrentRow.Cells("CustGroup").Value _
                                & "','" & dgrCustomerList.CurrentRow.Cells("CustShortName").Value _
                                & "'," & dgrCustomerList.CurrentRow.Cells("CustId").Value & ",'" _
                                & myStaff.SICode & "')", Conn) Then

            LoadCustomer()
        End If

        dgrCustomerList.Rows.Remove(dgrCustomerList.CurrentRow)
    End Sub

    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        mstrSelectedCustGroup = dgrCustGroup.CurrentRow.Cells("CustGroup").Value
        Me.DialogResult = DialogResult.OK
    End Sub
End Class