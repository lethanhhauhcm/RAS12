Public Class frmFillVisaInfo
    Private mblnFirstLoadCompleted As Boolean
    Private mstrService As String
    Public Sub New(strService As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mstrService = strService
    End Sub

    Private Sub frmFillVisaInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim strQuerry As String = "select CountryName as display, Country as Value" _
        '                            & " FROM [LIB].[dbo].[Country] c " _
        '                            & " left join MISC m On m.VAL=c.Country And m.CAT='VisaCountrySort'" _
        '                            & " order by m.IntVal desc, CountryName"
        'LoadComboDisplay(cboDestination, strQuerry, Conn)
        'LoadComboDisplay(cboNationality, strQuerry, Conn)
        mblnFirstLoadCompleted = True
    End Sub

    Private Sub cboDestination_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboF1.SelectedIndexChanged
        If Not mblnFirstLoadCompleted Then Exit Sub
        If cboF1.SelectedValue = "VN" Then
            LoadComboDisplay(cboVisaType, "select VAL2 as display, VAL1 as Value" _
            & " FROM MISC where cat='VisaTypeCWT' and Val='" & cboF1.SelectedValue _
            & "' order by VAL2 ", Conn)
        Else
            cboVisaType.DataSource = Nothing
        End If
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Function LoadCountry(ByRef cboInput As ComboBox, ByRef lblInput As Label, strLabelName As String) As Boolean
        Dim strQuerry As String = "select CountryName as display, Country as Value" _
                                    & " FROM [LIB].[dbo].[Country] c " _
                                    & " left join MISC m On m.VAL=c.Country And m.CAT='VisaCountrySort'" _
                                    & " order by m.IntVal desc, CountryName"
        lblInput.Text = strLabelName
        LoadComboDisplay(cboInput, strQuerry, Conn)
        Return True
    End Function
    Private Function LoadVisaType(ByRef cboInput As ComboBox, ByRef lblInput As Label, strCountry As String) As Boolean
        lblInput.Text = "VisaType"
        LoadComboDisplay(cboInput, "select VAL2 as display, VAL1 as Value" _
                    & " FROM MISC where cat='VisaTypeCWT' and Val='" & strCountry _
                    & "' order by VAL2 ", Conn)

    End Function
    Private Sub cboCatergory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCatergory.SelectedIndexChanged
        If Not mblnFirstLoadCompleted Then Exit Sub
        Select Case cboF1.Text
            Case "ON ARRIVAL", "RENEWAL"
                LoadCountry(cboF1, lblF1, "Destination")
                cboF1.SelectedIndex = 0
                LoadCountry(cboF2, lblF2, "Nationality")
                cboF2.SelectedIndex = 0
                LoadVisaType(cboF3, lblF3, cboF1.SelectedValue)
                cboF3.SelectedIndex = 0
                lblF4.Visible = False
                cboF4.Visible = False

            Case "OUTBOUND"
                LoadCountry(cboF1, lblF1, "Destination")
                cboF1.SelectedIndex = 0
                LoadCountry(cboF2, lblF2, "Nationality")
                cboF2.SelectedIndex = 0
        End Select
    End Sub
End Class