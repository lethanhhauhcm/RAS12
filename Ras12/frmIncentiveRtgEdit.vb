Public Class frmIncentiveRtgEdit

    Public Sub New(intRtgId As Integer, Optional blnCloneCitiesVv As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If intRtgId = 0 Then
            txtFromCountries.Text = "VN"
        Else
            Dim tblRtgRow As DataTable = GetDataTable("select * from lib.dbo.IncentiveRtg where Recid=" & intRtgId, Conn)

            txtCar.Text = tblRtgRow.Rows(0)("Car")
            txtRtgType.Text = tblRtgRow.Rows(0)("RtgType")
            txtFromCountries.Text = tblRtgRow.Rows(0)("FromCountries")
            txtToCountries.Text = tblRtgRow.Rows(0)("ToCountries")
            If blnCloneCitiesVv Then
                txtRecId.Text = 0
                txtFromCities.Text = tblRtgRow.Rows(0)("ToCities")
                txtToCities.Text = tblRtgRow.Rows(0)("FromCities")
            Else
                txtRecId.Text = intRtgId
                txtFromCities.Text = tblRtgRow.Rows(0)("FromCities")
                txtToCities.Text = tblRtgRow.Rows(0)("ToCities")
            End If

        End If
    End Sub

    Private Sub frmIncentiveRtgEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerries As New List(Of String)
        If Not CheckInputValues() Then Exit Sub
        lstQuerries.Add("insert into lib.dbo.IncentiveRtg (Car, RtgType, FromCountries, ToCountries, FromCities, ToCities, Status, FstUser, City) values ('" _
                        & txtCar.Text & "','" & txtRtgType.Text & "','" & txtFromCountries.Text & "','" & txtToCountries.Text _
                        & "','" & txtFromCities.Text & "','" & txtToCities.Text & "','OK','" & myStaff.SICode & "','" & myStaff.City & "')")
        If txtRecId.Text <> "0" Then
            lstQuerries.Add("UPDATE lib.dbo.IncentiveRtg set Status='XX',LstUpDate=getdate(),LstUser='" & myStaff.SICode & "' where RecId=" & txtRecId.Text)
        End If
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            Me.DialogResult = DialogResult.OK
        Else
            MsgBox("Unable to update IncentiveRtg")
        End If
    End Sub

    Private Sub blkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles blkCancel.LinkClicked
        Me.Dispose()
    End Sub
    Private Function CheckInputValues() As Boolean
        txtFromCountries.Text = txtFromCountries.Text.Replace(" ", "")
        txtToCountries.Text = txtToCountries.Text.Replace(" ", "")
        txtFromCities.Text = txtFromCities.Text.Replace(" ", "")
        txtToCities.Text = txtToCities.Text.Replace(" ", "")
        If Not CheckFormatTextBox(txtCar,, 2, 2) Then
            Return False
        ElseIf Not CheckFormatTextBox(txtRtgType,, 2, 16) Then
            Return False
        ElseIf Not CheckCountries(txtFromCountries.Text) Then
            Return False
        ElseIf Not CheckCountries(txtToCountries.Text) Then
            Return False
        ElseIf Not CheckCities(txtFromCities.Text) Then
            Return False
        ElseIf Not CheckCities(txttoCities.Text) Then
            Return False
        End If

        Return True
    End Function
    Private Function CheckCountries(strCountries As String) As Boolean
        If strCountries <> "*" Then
            Dim arrCountries As String() = Split(strCountries, ",")
            For Each strCountry As String In arrCountries
                If strCountry.Length <> 2 Then
                    MsgBox("Invalid Country:" & strCountry)
                    Return False
                End If
            Next
        End If
        Return True
    End Function
    Private Function CheckCities(strCities As String) As Boolean
        If strCities <> "*" Then
            Dim arrCities As String() = Split(strCities, ",")
            For Each strCity As String In arrCities
                If strCity.Length <> 3 Then
                    MsgBox("Invalid City:" & strCity)
                    Return False
                End If
            Next
        End If
        Return True
    End Function
End Class