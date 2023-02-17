Public Class clsNameiBank
    Dim mstrLastName As String
    Dim mstrFirstName As String

    Public Function ParseFullName(ByVal strFullName As String) As Boolean
        Dim arrBreak() As String
        If strFullName.Contains("/") Then
            arrBreak = Split(strFullName, "/", 2)
        Else
            arrBreak = Split(strFullName, " ", 1)
        End If

        mstrLastName = arrBreak(0)
        If arrBreak.Length = 2 Then
            mstrFirstName = arrBreak(1)
        End If

        Return True
    End Function
    Public Property LastName() As String
        Get
            LastName = mstrLastName
        End Get
        Set(ByVal value As String)
            mstrLastName = value
        End Set
    End Property
    Public Property FirstName() As String
        Get
            FirstName = mstrFirstName
        End Get
        Set(ByVal value As String)
            mstrFirstName = value
        End Set
    End Property
End Class
