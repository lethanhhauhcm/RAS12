Public Class clsGoAir
    Private mstrALs As String
    Private mstrDepApts As String
    Private mstrArrApts As String

    Public Function ParseItinerary(strIineary As String) As Boolean
        Dim arrBreaks As String() = Split(strIineary, " ")
        For i As Integer = 0 To arrBreaks.Length - 2 Step 2
            mstrDepApts = mstrDepApts & arrBreaks(i) & "_"
            mstrALs = mstrALs & arrBreaks(i + 1) & "_"
            mstrArrApts = mstrArrApts & arrBreaks(i + 2) & "_"
        Next
        mstrDepApts = RemoveLastChr(mstrDepApts)
        mstrALs = RemoveLastChr(mstrALs)
        mstrArrApts = RemoveLastChr(mstrArrApts)
    End Function

    Public Property ALs As String
        Get
            Return mstrALs
        End Get
        Set(value As String)
            mstrALs = value
        End Set
    End Property
    Public Property DepApts As String
        Get
            Return mstrDepApts
        End Get
        Set(value As String)
            mstrDepApts = value
        End Set
    End Property
    Public Property ArrApts As String
        Get
            Return mstrArrApts
        End Get
        Set(value As String)
            mstrArrApts = value
        End Set
    End Property
End Class
