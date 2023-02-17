Imports System.Text.RegularExpressions
Public Class frmRtgTypeEdit
    Public Sub New(objRow As DataGridViewRow, blnEdit As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        LoadCmb_VAL(cboCustShortName, "select RecID as VAL, CustShortName as DIS  from CustomerList l where status='OK' " _
                            & " and RecID in (select CustID from cust_Detail where status='OK'" _
                            & " and Cat='Channel' and Val in ('CS','LC')) order by CustShortName")
        ' Add any initialization after the InitializeComponent() call.
        If objRow IsNot Nothing Then
            If blnEdit Then txtRecId.Text = objRow.Cells("RecId").Value
            cboCustShortName.SelectedIndex = cboCustShortName.FindStringExact(objRow.Cells("CustShortName").Value)
            cboRtgType.SelectedIndex = cboRtgType.FindStringExact(objRow.Cells("RtgType").Value)
            dtpFromDate.Value = objRow.Cells("TktFromDate").Value
            dtpToDate.Value = objRow.Cells("TktToDate").Value
            txtCountries.Text = objRow.Cells("Countries").Value

        End If
    End Sub
    Private Function CheckInputValues() As Boolean
        Dim tblExistingRtgType As DataTable
        Dim strQuerry As String = "Select m.RecId,m.Val as RtgType,Val1 as Countries,intVal as CustId" _
                                & ",c.CustShortname, convert( varchar,[dteVal],106) as TktFromDate" _
                                & ",convert( varchar, [dteVal1],106) as TktToDate" _
                                & " from Misc m" _
                                & " left join CustomerList c on m.intVal=c.RecId" _
                                & " where m.intVal=" & cboCustShortName.SelectedValue _
                                & " and m.Val='" & cboRtgType.Text & "'" _
                                & " and m.Cat='RtgTypeCwt' and m.Status='OK'"
        Dim arrCountries As String()

        If IsNumeric(txtRecId.Text) Then
            strQuerry = strQuerry & " and m.RecId<>" & txtRecId.Text
        End If

        txtCountries.Text = txtCountries.Text.Replace(" ", "")
        arrCountries = txtCountries.Text.Split("_")
        For Each strCountry As String In arrCountries
            If strCountry.Length <> 2 Then
                MsgBox("Invalid Country Code " & strCountry)
                Return False
            End If
        Next
        If Not CheckMustNotEmptyText(cboCustShortName) Then Return False
        If Not CheckMustNotEmptyText(cboRtgType) Then Return False
        If Not CheckMustNotEmptyText(txtCountries) Then Return False

        tblExistingRtgType = GetDataTable(strQuerry & " and (('" & CreateFromDate(dtpFromDate.Value) _
                                          & "' between dteVal and dteVal1) or ('" _
                                          & CreateToDate(dtpToDate.Value) & "' between dteVal and dteVal1))")
        If tblExistingRtgType.Rows.Count > 0 Then
            Dim frmShow As New frmShowTableContent(tblExistingRtgType, "Overlap Validity with the following records!")
            frmShow.ShowDialog()
            Return False
        End If
        Return True
    End Function
    Private Sub frmRtgTypeEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub lbkGetDefaultCountries_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkGetDefaultCountries.LinkClicked
        txtCountries.Text = ScalarToString("Misc", "top 1 Val1", "Cat='RtgTypeCwt' and intVal=0")
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Dim lstInserts As New List(Of String)
        If Not CheckInputValues() Then
            Exit Sub
        End If
        lstInserts.Add("Insert Misc (CAT, VAL, VAL1, IntVal, dteVal, dteVal1, FstUser, Status, City)" _
            & " values ('RtgTypeCwt','" & cboRtgType.Text & "','" & txtCountries.Text _
            & "'," & cboCustShortName.SelectedValue _
            & ",'" & CreateFromDate(dtpFromDate.Value) & "','" & CreateToDate(dtpToDate.Value) _
            & "','" & myStaff.SICode & "','OK','" & myStaff.City & "')")

        If txtRecId.Text <> "" Then
            lstInserts.Add("update Misc set Status='XX',LstUpdate=getdate(),LstUser='" _
                           & myStaff.SICode & "' where RecId=" & txtRecId.Text)
        End If
        If UpdateListOfQuerries(lstInserts, Conn) Then
            Me.DialogResult = DialogResult.OK
            Me.Dispose()
        Else
            MsgBox("Unable to update RtgType!")
        End If
    End Sub

    Private Sub lbkContinuosFrom_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkContinueFromPrevious.LinkClicked
        If IsNumeric(txtRecId.Text) Then
            Dim dteNewFromDate As Date = ScalarToDate("MISC", "top 1 dteVAl1 " _
                                         , "Cat='RtgTypeCwt' and intVal=" & cboCustShortName.SelectedValue _
                                         & " and RecId<>" & txtRecId.Text _
                                         & " and Val='" & cboRtgType.Text & "' order by intVal1 desc").AddDays(1)
            dtpFromDate.Value = dteNewFromDate
        End If
    End Sub

    Private Sub lbkAdd1Year_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkAdd1Year.LinkClicked
        dtpToDate.Value = dtpToDate.Value.AddYears(1)
    End Sub
End Class