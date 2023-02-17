Public Class clsTktRas12
    Private mstrRcpNo As String
    Private mstrDependent As String
    Private mdecNetToAL As Decimal
    Private mdteDOI As Date

    Public Property RcpNo As String
        Get
            RcpNo = mstrRcpNo
        End Get
        Set(value As String)
            mstrRcpNo = value
        End Set
    End Property
    Public Property Dependent As String
        Get
            Dependent = mstrDependent
        End Get
        Set(value As String)
            mstrDependent = value
        End Set
    End Property
    Public Property NetToAL As Decimal
        Get
            NetToAL = mdecNetToAL
        End Get
        Set(value As Decimal)
            mdecNetToAL = value
        End Set
    End Property
    Public Property DOI As Date
        Get
            DOI = mdteDOI
        End Get
        Set(value As Date)
            mdteDOI = value
        End Set
    End Property
End Class
