Public Class clsCwtShortName
    Dim mstrCmc As String
    Dim mstrShortName As String
    Dim mstrCompanyName As String
    Public Property Cmc() As String
        Get
            Cmc = mstrCmc
        End Get
        Set(ByVal value As String)
            mstrCmc = value
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
    Public Property CompanyName() As String
        Get
            CompanyName = mstrCompanyName
        End Get
        Set(ByVal value As String)
            mstrCompanyName = value
        End Set
    End Property
End Class
