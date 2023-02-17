Public Class clsiSOS
    Dim mintRecId As Integer
    Dim mstrGds As String
    Dim mstrRloc As String
    Dim mstrPaxNames As String
    Dim mstrAirSegs As String
    Public Property Gds() As String
        Get
            Gds = mstrGds
        End Get
        Set(ByVal value As String)
            mstrGds = value
        End Set
    End Property
    Public Property Rloc() As String
        Get
            Rloc = mstrRloc
        End Get
        Set(ByVal value As String)
            mstrRloc = value
        End Set
    End Property
    Public Property PaxNames() As String
        Get
            PaxNames = mstrPaxNames
        End Get
        Set(ByVal value As String)
            mstrPaxNames = value
        End Set
    End Property
    Public Property AirSegs() As String
        Get
            AirSegs = mstrAirSegs
        End Get
        Set(ByVal value As String)
            mstrAirSegs = value
        End Set
    End Property
    Public Property RecId() As Integer
        Get
            RecId = mintRecId
        End Get
        Set(ByVal value As Integer)
            mintRecId = value
        End Set
    End Property
End Class
