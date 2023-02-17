Public Class clsHierachy
    Dim mintNbr As Int16
    Dim mstrDescription As String
    Dim mblnCheckValues As Boolean
    Dim mstrHierName As String
    Dim mstrCharType As String
    Private mintMinLength As Int16
    Private mintMaxLength As Int16
    Private mstrMandatory As String

    Public Property Nbr() As Int16
        Get
            Nbr = mintNbr
        End Get
        Set(ByVal value As Int16)
            mintNbr = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Description = mstrDescription
        End Get
        Set(ByVal value As String)
            mstrDescription = value
        End Set
    End Property
    Public Property CheckValues() As Boolean
        Get
            CheckValues = mblnCheckValues
        End Get
        Set(ByVal value As Boolean)
            mblnCheckValues = value
        End Set
    End Property
    Public Property HierName() As String
        Get
            HierName = mstrHierName
        End Get
        Set(ByVal value As String)
            mstrHierName = value
        End Set
    End Property
    Public Property CharType() As String
        Get
            CharType = mstrCharType
        End Get
        Set(ByVal value As String)
            mstrCharType = value
        End Set
    End Property
    Public Property MinLength As Integer
        Get
            Return mintMinLength
        End Get
        Set(value As Integer)
            mintMinLength = value
        End Set
    End Property
    Public Property MaxLength As Integer
        Get
            Return mintMaxLength
        End Get
        Set(value As Integer)
            mintMaxLength = value
        End Set
    End Property
    Public Property Mandatory() As String
        Get
            Mandatory = mstrMandatory
        End Get
        Set(ByVal value As String)
            mstrMandatory = value
        End Set
    End Property
End Class
