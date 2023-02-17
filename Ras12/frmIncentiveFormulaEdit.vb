Public Class frmIncentiveFormulaEdit
    Public Sub New(intFormulaId As Integer, blnClone As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadCombo(cboRtgType, "select distinct RtgType as value from lib.dbo.IncentiveRtg where status='OK' order by RtgType", Conn)

        If intFormulaId = 0 Then
            cboDateType.SelectedIndex = 0
            cboSegTkt.SelectedIndex = 0
            cboDomInt.SelectedIndex = 0
        Else
            Dim tblRtgRow As DataTable = GetDataTable("select * from lib.dbo.IncentiveFormula where Recid=" & intFormulaId, Conn)
            txtCar.Text = tblRtgRow.Rows(0)("Car")
            cboDateType.SelectedIndex = cboDateType.FindStringExact(tblRtgRow.Rows(0)("DateType"))
            cboSegTkt.SelectedIndex = cboSegTkt.FindStringExact(tblRtgRow.Rows(0)("SegTkt"))
            cboDomInt.SelectedIndex = cboDomInt.FindStringExact(tblRtgRow.Rows(0)("DomInt"))
            cboRtgType.SelectedIndex = cboRtgType.FindStringExact(tblRtgRow.Rows(0)("RtgType"))
            txtFareBasis.Text = tblRtgRow.Rows(0)("FareBasis")
            txtBkgCls.Text = tblRtgRow.Rows(0)("BkgCls")
            txtVND.Text = Format(tblRtgRow.Rows(0)("VND"), "#,###")
            If blnClone Then
                txtRecId.Text = 0
            Else
                txtRecId.Text = intFormulaId
            End If

        End If
    End Sub

    Private Sub frmIncentiveFormulaEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstQuerries As New List(Of String)
        Dim decVND As Decimal

        If Not CheckInputValues() Then Exit Sub
        decVND = CDec(txtVND.Text)
        lstQuerries.Add("insert into lib.dbo.IncentiveFormula (Car,DateType,FromDate,ToDate,DomInt,SegTkt, RtgType" _
                        & ",BkgCls,FareBasis,VND, Status, FstUser, City) values ('" _
                        & txtCar.Text & "','" & cboDateType.Text & "','" & CreateFromDate(dtpFromDate.Value) _
                        & "','" & CreateToDate(dtpToDate.Value) & "','" & cboDomInt.Text & "','" & cboSegTkt.Text _
                        & "','" & cboRtgType.Text & "','" & txtBkgCls.Text & "','" & txtFareBasis.Text & "'," & decVND _
                        & ",'OK','" & myStaff.SICode & "','" & myStaff.City & "')")
        If txtRecId.Text <> "0" Then
            lstQuerries.Add("UPDATE lib.dbo.IncentiveFormula set Status='XX',LstUpDate=getdate(),LstUser='" & myStaff.SICode & "' where RecId=" & txtRecId.Text)
        End If
        If UpdateListOfQuerries(lstQuerries, Conn) Then
            Me.DialogResult = DialogResult.OK
        Else
            MsgBox("Unable to update IncentiveFormula")
        End If
    End Sub

    Private Sub blkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles blkCancel.LinkClicked
        Me.Dispose()
    End Sub
    Private Function CheckInputValues() As Boolean
        txtBkgCls.Text = txtBkgCls.Text.Replace(" ", "")
        txtFareBasis.Text = txtFareBasis.Text.Replace(" ", "")
        If Not CheckFormatTextBox(txtCar,, 2, 2) Then
            Return False
        ElseIf Not CheckFormatComboBox(cboSegTkt,, 3, 3) Then
            Return False
        ElseIf Not CheckFormatComboBox(cboDomInt,, 3, 3) Then
            Return False
        ElseIf Not CheckFormatComboBox(cboRtgType,, 2) Then
            Return False
        ElseIf Not CheckBkgClass(txtBkgCls.Text) Then
            Return False
        ElseIf Not CheckFareBasis(txtFareBasis.Text) Then
            Return False
        End If

        Return True
    End Function
    Private Function CheckBkgClass(strBkgClss As String) As Boolean
        If strBkgClss <> "*" Then
            Dim arrBkgClss As String() = Split(strBkgClss, ",")
            For Each strBkgCls As String In arrBkgClss
                If strBkgCls.Length <> 1 Then
                    MsgBox("Invalid BkgCls:" & strBkgCls)
                    Return False
                End If
            Next
        End If
        Return True
    End Function
    Private Function CheckFareBasis(strFareBasiss As String) As Boolean
        If strFareBasiss <> "*" Then
            Dim arrFareBasiss As String() = Split(strFareBasiss, ",")
            For Each strFareBasis As String In arrFareBasiss
                If strFareBasis.Length = 0 Then
                    MsgBox("Invalid Empty FareBasis")
                    Return False
                End If
            Next
        End If
        Return True
    End Function
End Class