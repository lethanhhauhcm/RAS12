Public Class frmUpdateCDRs

    Private mobjTravel As DataRow
    Private mintTravelId As Integer

    Public Sub New(intTkId As Integer, strDocType As String)
        Dim strQuerry As String
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Select Case strDocType
            Case "ATK", "ETK", "MCO"
                strQuerry = "Select TravelId from Go_Air where (Select Status from Go_travel t where t.RecId=TravelId)='OK' and Tkid=" & intTkId
            Case "AHC"
                strQuerry = "Select TravelId from GO_MiscSvc where (Select Status from Go_travel t where t.RecId=TravelId)='OK' and itemId=" & intTkId
            Case Else
                MsgBox("Chưa lập trình cho loại chứng từ này. Cần đặt hàng Khanhnm nếu cần")
        End Select
        mintTravelId = pobjTvcs.GetScalarAsDecimal(strQuerry)
        Dim tblGoTravel As DataTable = pobjTvcs.GetDataTable("Select * from Go_Travel where RecId=" & mintTravelId)
        If tblGoTravel.Rows.Count > 0 Then
            mobjTravel = tblGoTravel.Rows(0)
        End If
    End Sub

    Private Sub frmUpdateCDRs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim stri As String = ""
        Dim strSql As String = ""
        Dim dTable As DataTable
        Dim i As Integer
        For i = 1 To 10
            stri = i.ToString.Trim
            If i < 6 Then
                'Me.Controls("TxtHierachy" & stri).Enabled = False
                Me.Controls("TxtHierachy" & stri).Visible = False
                Me.Controls("lblHier" & stri).Visible = False
                Me.Controls("TxtHierachy" & stri).Text = mobjTravel("Hierachy" & stri)
            End If
            Me.Controls("Lblref" & stri).Visible = False
            'Me.Controls("Lblref" & stri).Enabled = False
            Me.Controls("Txtref" & stri).Visible = False
            'Me.Controls("Txtref" & stri).Enabled = False
            Me.Controls("Txtref" & stri).Text = mobjTravel("Ref" & stri)
        Next
        strSql = "select CDRNbr, CdrName from cwt.dbo.go_CDRs where cmc='" & mobjTravel("CMC")
        strSql = strSql & "' and status='OK' order by Len(cdrNbr), cdrNbr"
        dTable = GetDataTable(strSql)
        For k As Int16 = 0 To dTable.Rows.Count - 1
            stri = dTable.Rows(k)("CDRNbr").ToString.Trim
            Me.Controls("Lblref" & stri).Text = "(" & stri & ") " & dTable.Rows(k)("CDRName")
            Me.Controls("Lblref" & stri).Visible = True
            Me.Controls("Txtref" & stri).Visible = True
        Next

        strSql = "select Nbr from cwt.dbo.go_hierachies where cmc='" & mobjTravel("CMC")
        StrSQL = StrSQL & "' and status='OK' order by Nbr"

        dTable = pobjTvcs.GetDataTable(strSql)
        For k As Int16 = 0 To dTable.Rows.Count - 1
            stri = dTable.Rows(k)("Nbr").ToString.Trim
            Me.Controls("TxtHierachy" & stri).Visible = True
            Me.Controls("lblHier" & stri).Visible = True
        Next

    End Sub
    Private Sub LblSaveCDR_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblSaveCDR.LinkClicked
        SaveCDR()
    End Sub

    Private Sub LblGetProfile_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblGetProfile.LinkClicked

    End Sub
    Private Sub SaveCDR()
        Dim i As Int16
        Dim strSql As String = "update Go_Travel SET "

        For i = 1 To 10
            If Me.Controls("Lblref" & i.ToString.Trim).Visible Then
                strSql = strSql & "Ref" & i.ToString.Trim & "='" & Me.Controls("Txtref" & i.ToString.Trim).Text & "',"
            End If
            If i < 6 Then
                strSql = strSql & " Hierachy" & i.ToString.Trim & "='" & Me.Controls("txtHierachy" & i.ToString.Trim).Text & "',"

            End If
        Next
        strSql = RemoveLastChr(strSql)
        strSql = strSql & " where recid =" & mintTravelId
        If pobjTvcs.ExecuteNonQuerry(strSql) Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Else
            MsgBox("Unable to update CDRs")
        End If
    End Sub
End Class