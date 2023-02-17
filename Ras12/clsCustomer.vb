Public Class clsCustomer

    Dim mstrProfileName1A As String
    Dim mstrShortName As String
    Dim mstrFullName As String
    Dim mintCustId As Integer
    Dim mstrCmc As String
    Dim mintGO_Client As Integer
    Dim mintCaptureData As Integer
    Dim mblnPrepaid As Boolean
    Dim mstrOffc1 As String
    Dim mstrQ1 As String
    Dim mstrOffc2 As String
    Dim mstrQ2 As String
    Dim mblnOptQ As Boolean
    Private mblnPnr1aMustHaveEmail As Boolean
    Private mblnPnr1aMustHaveMobile As Boolean
    Private mintVnCorpId As Integer
    Private mblnSOS As Boolean
    Private mstrTMC As String

    Public Property ProfileName1A() As String
        Get
            ProfileName1A = mstrProfileName1A
        End Get
        Set(ByVal value As String)
            mstrProfileName1A = value
        End Set

    End Property
    Public Property ShortName() As String
        Get
            ShortName = mstrShortName
        End Get
        Set(ByVal value As String)
            mstrShortName = value
        End Set
    End Property
    Public Property FullName() As String
        Get
            FullName = mstrFullName
        End Get
        Set(ByVal value As String)
            mstrFullName = value
        End Set
    End Property
    Public Property CustId() As Integer
        Get
            CustId = mintCustId
        End Get
        Set(ByVal value As Integer)
            mintCustId = value
        End Set
    End Property
    Public Property Cmc() As String
        Get
            Cmc = mstrCmc
        End Get
        Set(ByVal value As String)
            mstrCmc = value
        End Set
    End Property
    Public Property CaptureData() As Integer
        Get
            CaptureData = mintCaptureData
        End Get
        Set(ByVal value As Integer)
            mintCaptureData = value
        End Set
    End Property
    Public Property GO_Client() As Integer
        Get
            GO_Client = mintGO_Client
        End Get
        Set(ByVal value As Integer)
            mintGO_Client = value
        End Set
    End Property
    Public Property Prepaid() As Boolean
        Get
            Prepaid = mblnPrepaid
        End Get
        Set(ByVal value As Boolean)
            mblnPrepaid = value
        End Set
    End Property
    Public Property Offc1() As String
        Get
            Offc1 = mstrOffc1
        End Get
        Set(ByVal value As String)
            mstrOffc1 = value
        End Set

    End Property
    Public Property Q1() As String
        Get
            Q1 = mstrQ1
        End Get
        Set(ByVal value As String)
            mstrQ1 = value
        End Set

    End Property
    Public Property Offc2() As String
        Get
            Offc2 = mstrOffc2
        End Get
        Set(ByVal value As String)
            mstrOffc2 = value
        End Set

    End Property
    Public Property Q2() As String
        Get
            Q2 = mstrQ2
        End Get
        Set(ByVal value As String)
            mstrQ2 = value
        End Set

    End Property
    Public Property OptQ() As Boolean
        Get
            OptQ = mblnOptQ
        End Get
        Set(ByVal value As Boolean)
            mblnOptQ = value
        End Set
    End Property
    Public Property Pnr1aMustHaveEmail() As Boolean
        Get
            Pnr1aMustHaveEmail = mblnPnr1aMustHaveEmail
        End Get
        Set(ByVal value As Boolean)
            mblnPnr1aMustHaveEmail = value
        End Set
    End Property
    Public Property Pnr1aMustHaveMobile() As Boolean
        Get
            Pnr1aMustHaveMobile = mblnPnr1aMustHaveMobile
        End Get
        Set(ByVal value As Boolean)
            mblnPnr1aMustHaveMobile = value
        End Set
    End Property
    Public Property SOS() As Boolean
        Get
            Return mblnSOS
        End Get
        Set(ByVal value As Boolean)
            mblnSOS = value
        End Set
    End Property

    Public Property VnCorpId As Integer
        Get
            Return mintVnCorpId
        End Get
        Set(value As Integer)
            mintVnCorpId = value
        End Set
    End Property
    Public Property TMC() As String
        Get
            TMC = mstrTMC
        End Get
        Set(ByVal value As String)
            mstrTMC = value
        End Set

    End Property
End Class


