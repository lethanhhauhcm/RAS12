Public Class frmFillServiceDesc
    Private mblnFirstLoadCompleted As Boolean
    Private mstrService As String
    Private mstrSubService As String
    Public Sub New(strService As String, strSubService As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mstrService = strService
        mstrSubService = strSubService
    End Sub

    Private Sub frmFillVisaInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case mstrService
            Case "Visa"
                LoadCountry(cboF1, lblF1, "Destination")
                cboF1.SelectedIndex = -1
                LoadCountry(cboF2, lblF2, "Nationality")
                cboF2.SelectedIndex = -1

            Case "Miscellaneous"
                LoadServiceDetails(cboF3, lblF3, mstrSubService)
                lblF1.Visible = False
                lblF2.Visible = False
                lblF4.Visible = False
                cboF1.Visible = False
                cboF2.Visible = False
                cboF4.Visible = False
        End Select
        mblnFirstLoadCompleted = True

        If mstrService = "Visa" Then
            Select Case mstrSubService
                Case "Vietnam", "ResidenceCard"
                    cboF1.SelectedIndex = 0
            End Select
        End If
    End Sub

    Private Sub cboF1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboF1.SelectedIndexChanged
        If Not mblnFirstLoadCompleted Then Exit Sub
        If mstrService = "Visa" AndAlso cboF1.SelectedValue = "VN" Then
            Select Case mstrSubService
                Case "ResidenceCard"
                    LoadResidenceCardType(cboF3, lblF3)
                    LoadServiceDetails(cboF4, lblF4, mstrSubService)
                Case Else
                    LoadVisaType(cboF3, lblF3, cboF1.SelectedValue)
                    LoadServiceDetails(cboF4, lblF4, mstrService)
                    cboF4.SelectedIndex = -1
            End Select

        End If
    End Sub

    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        Select Case mstrService
            Case "Visa"
                If cboF1.Text = "" Then
                    MsgBox(" You must select " & lblF1.Text)
                    Exit Sub
                ElseIf cboF2.Text = "" Then
                    MsgBox(" You must select " & lblF2.Text)
                    Exit Sub
                ElseIf cboF3.Text = "" AndAlso cboF1.SelectedValue = "VN" Then
                    MsgBox(" You must select " & lblF3.Text)
                    Exit Sub
                End If

        End Select
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
    Private Function LoadMiscTypes(ByRef cboInput As ComboBox, ByRef lblInput As Label) As Boolean
        lblInput.Text = "MiscellaneousTypes"
        LoadComboDisplay(cboInput, "select VAL1 as display, VAL1 as Value" _
                    & " FROM MISC where cat='NonAirSubSvc' and Val='Miscellaneous' order by VAL1 ", Conn)

    End Function
    Private Function LoadServiceDetails(ByRef cboInput As ComboBox, ByRef lblInput As Label, strService As String) As Boolean
        lblInput.Text = "ServiceDetails"
        LoadComboDisplay(cboInput, "select VAL1 as display, VAL1 as Value" _
                    & " FROM MISC where cat='NonAirSvcDetails' and Val='" _
                    & strService & "' order by VAL2 ", Conn)

    End Function
    Private Function LoadResidenceCardType(ByRef cboInput As ComboBox, ByRef lblInput As Label) As Boolean
        lblInput.Text = "ResidenceCardType"
        LoadComboDisplay(cboInput, "select VAL2 as display, VAL1 as Value" _
                    & " FROM MISC where cat='NonAirDocType' and Val='ResidenceCard' order by VAL2 ", Conn)

    End Function
    Private Sub cboCatergory_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Not mblnFirstLoadCompleted Then Exit Sub
        Select Case cboF1.Text
            Case "ON ARRIVAL", "RENEWAL"

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
    Public Property Service As String
        Get
            Return mstrService
        End Get
        Set(value As String)
            mstrService = value
        End Set
    End Property
    Public Property SubService As String
        Get
            Return mstrSubService
        End Get
        Set(value As String)
            mstrSubService = value
        End Set
    End Property
End Class