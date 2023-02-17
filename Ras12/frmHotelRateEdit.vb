Public Class frmHotelRateEdit
    Private mblnFirstLoadCompleted As Boolean

    Public Sub New(Optional objHotel As DataGridViewRow = Nothing, Optional blnClone As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        cboRoomCat.DataSource = GetDataTable("Select VAL from Misc where cat='SCAT' and val1='Accommodations'")
        cboRoomCat.DisplayMember = "VAL"
        cboRoomType.DataSource = GetDataTable("Select VAL from Misc where cat='STYPE' and val1='Accommodations'")
        cboRoomType.DisplayMember = "VAL"
        ' Add any initialization after the InitializeComponent() call.
        If objHotel Is Nothing Then
            txtHotelName.Enabled = True
        Else
            With objHotel
                txtRecId.Text = .Cells("RecId").Value
                If blnClone Then txtRecId.Text = "0"
                txtSupplierId.Text = .Cells("SupplierId").Value
                txtHotelName.Text = .Cells("FullName").Value
                txtContactPerson.Text = .Cells("ContactPerson").Value
                txtContactTel.Text = .Cells("ContactTel").Value
                txtContactEmail.Text = .Cells("ContactEmail").Value
                cboRoomCat.SelectedIndex = cboRoomCat.FindStringExact(.Cells("RoomCat").Value)
                cboRoomType.SelectedIndex = cboRoomType.FindStringExact(.Cells("RoomType").Value)
                txtRate.Text = Format(.Cells("Rate").Value, "#,#00")
                dtpFrom.Value = .Cells("ValidFrom").Value
                dtpTo.Value = .Cells("ValidTo").Value
                chkBreakfast.Checked = .Cells("Breakfast").Value
                chkServiceCharge.Checked = .Cells("ServiceCharge").Value
                txtCustGroup.Text = .Cells("CustGroup").Value
                txtRemark.Text = .Cells("Remark").Value

            End With
        End If
        mblnFirstLoadCompleted = True
    End Sub
    Private Sub cboValidFrom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboValidFrom.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Select Case cboValidFrom.Text
                Case "NextYear"
                    dtpFrom.Value = DateSerial(Now.Year + 1, 1, 1)
                Case "ContinuePrevious"
                    Dim tblPreviousRecord As DataTable = GetDataTable("Select top 1 * from HotelRate" _
                                                                      & " where RecId<>" & txtRecId.Text _
                                                                      & " and SupplierId=" & txtSupplierId.Text _
                                                                      & " order by ValidTo desc")
                    If tblPreviousRecord.Rows.Count > 0 Then
                        dtpFrom.Value = CDate(tblPreviousRecord.Rows(0)("ValidTo")).AddDays(1)
                    End If
            End Select
        End If
    End Sub
    Private Sub cboValidTo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboValidTo.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Select Case cboValidTo.Text
                Case "1Year"
                    dtpTo.Value = dtpFrom.Value.AddYears(1).AddDays(-1)
                Case "2Years"
                    dtpTo.Value = dtpFrom.Value.AddYears(2).AddDays(-1)
                Case "EndOfYear"
                    dtpTo.Value = DateSerial(dtpFrom.Value.Year, 12, 31)
                Case "EndOfNextYear"
                    dtpTo.Value = DateSerial(dtpFrom.Value.Year + 1, 12, 31)
            End Select
        End If
    End Sub

    Private Sub lbkSearchHotel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSearchHotel.LinkClicked
        Dim tblHotel As DataTable = GetDataTable("select RecId,FullName,Address,Tel,Email " _
                                                 & " from Supplier where Status='OK'" _
                                                 & " And FullName Like '%" _
                                                 & txtHotelName.Text & "%' order by FullName", Conn)
        Dim frmSearch As New frmShowTableContent(tblHotel, "Select Hotel", "RecId", "FullName")

        If frmSearch.ShowDialog = DialogResult.OK Then
            With frmSearch.SelectedRow
                txtSupplierId.Text = .Cells("RecId").Value
                txtHotelName.Text = .Cells("FullName").Value
                txtContactTel.Text = .Cells("Tel").Value
                txtContactEmail.Text = .Cells("Email").Value
            End With
        End If
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerries As New List(Of String)
        If Not CheckInputValues() Then Exit Sub
        lstQuerries.Add("insert into HotelRates (SupplierId, ContactPerson, ContactTel, ContactEmail" _
                        & ", RoomCat, RoomType, Rate, ValidFrom, ValidTo, Breakfast, ServiceCharge,CustGroup" _
                        & ", Remark, FstUser, Status, City) values (" _
                        & txtSupplierId.Text & ",N'" & txtContactPerson.Text _
                        & "','" & txtContactTel.Text & "','" & txtContactEmail.Text _
                        & "','" & cboRoomCat.Text & "','" & cboRoomType.Text _
                        & "'," & CDec(txtRate.Text) & ",'" & CreateFromDate(dtpFrom.Value) _
                        & "','" & CreateToDate(dtpTo.Value) & "','" & chkBreakfast.Checked _
                        & "','" & chkServiceCharge.Checked & "','" & txtCustGroup.Text _
                        & "','" & txtRemark.Text _
                        & "','" & myStaff.SICode & "','OK','" & myStaff.City & "')")
        If txtRecId.Text <> 0 Then
            lstQuerries.Add(ChangeStatus_ByID("HotelRates", "XX", txtRecId.Text))
        End If
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            Me.DialogResult = DialogResult.OK
        Else
            MsgBox("Unable to Save record!")
        End If
    End Sub

    Private Sub txtRate_Leave(sender As Object, e As EventArgs) Handles txtRate.Leave
        txtRate.Text = Format(CDec(txtRate.Text), "#,###")
    End Sub

    Private Function CheckInputValues() As Boolean
        Dim decRate As Decimal
        Dim tblDupHotels As DataTable

        If txtSupplierId.Text = 0 Then
            MsgBox("You must select Hotel")
            Return False
        End If
        If dtpFrom.Value.Date > dtpTo.Value.Date Then
            MsgBox("ValidFrom must on/before ValidTo!")
            Return False
        End If

        If txtContactEmail.Text <> "" AndAlso Not txtContactEmail.Text.Contains("@") Then
            MsgBox("Invalid ContactEmail!")
            Return False
        End If

        Try
            decRate = CDec(txtRate.Text)
        Catch ex As Exception

        End Try
        If decRate <= 0 Then
            MsgBox("Invalid Rate!")
            Return False
        End If

        tblDupHotels = GetDataTable("select top 1 * from HotelRates where Status='OK' and RecId<>" & txtRecId.Text _
                                 & " and SupplierId=" & txtSupplierId.Text & " and RoomCat='" & cboRoomCat.Text _
                                 & "' and RoomType='" & cboRoomType.Text _
                                 & "' and CustGroup='" & txtCustGroup.Text _
                                 & "' and ValidFrom <='" & CreateToDate(dtpTo.Value) _
                                 & "' and ValidTo >='" & CreateFromDate(dtpTo.Value) & "'")
        If tblDupHotels.Rows.Count > 0 Then
            Dim frmShow As New frmShowTableContent(tblDupHotels, "Duplicated Hotels Exist!")
            frmShow.ShowDialog()
            Return False
        End If
        Return True
    End Function

    Private Sub lbkCustGroup_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCustGroup.LinkClicked
        Dim frmGroup As New frmCustGroup(True)
        If frmGroup.ShowDialog = DialogResult.OK Then
            txtCustGroup.Text = frmGroup.mstrSelectedCustGroup
        End If
    End Sub
End Class