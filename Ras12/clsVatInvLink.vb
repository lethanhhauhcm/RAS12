Public Class clsVatInvLink
    Private mstrLinkType As String
    Private mintLinkId As Integer = 0
    Private mstrLinkRef As String
    Private mstrRcpTcode As String
    Public Property LinkType As String
        Get
            Return mstrLinkType
        End Get
        Set(value As String)
            mstrLinkType = value
        End Set
    End Property
    Public Property LinkId As Integer
        Get
            Return mintLinkId
        End Get
        Set(value As Integer)
            mintLinkId = value
        End Set
    End Property
    Public Property LinkRef As String
        Get
            Return mstrLinkRef
        End Get
        Set(value As String)
            mstrLinkRef = value
        End Set
    End Property

    Public Property RcpTcode As String
        Get
            Return mstrRcpTcode
        End Get
        Set(value As String)
            mstrRcpTcode = value
        End Set
    End Property
End Class
