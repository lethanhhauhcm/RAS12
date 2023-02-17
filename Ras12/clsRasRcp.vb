Public Class clsRasRcp
    Private mdteDOI As Date
    Private mintRecId As Integer
    Private mstrRcpNo As String
    Private mstrRmk As String

    Public Property DOI As Date
        Get
            DOI = mdteDOI
        End Get
        Set(value As Date)
            mdteDOI = value
        End Set
    End Property
    Public Property RecId As Integer
        Get
            RecId = mintRecId
        End Get
        Set(value As Integer)
            mintRecId = value
        End Set
    End Property
    Public Property RcpNo As String
        Get
            RcpNo = mstrRcpNo
        End Get
        Set(value As String)
            mstrRcpNo = value
        End Set
    End Property
    Public Property Rmk As String
        Get
            Rmk = mstrRmk
        End Get
        Set(value As String)
            mstrRmk = value
        End Set
    End Property
End Class
