Public Class clsFareDisplayParameters
    Private mstrOriCity As String
    Private mstrDesCity As String
    Private mdteDepDate As Nullable(Of Date)
    Private mstrFillingCar As String
    Public Sub New(strOriCity As String, strDesCity As String)
        mstrOriCity = strOriCity
        mstrDesCity = strDesCity
    End Sub

    Public Property OriCity As String
        Get
            Return mstrOriCity
        End Get
        Set(value As String)
            mstrOriCity = value
        End Set
    End Property
    Public Property DesCity As String
        Get
            Return mstrDesCity
        End Get
        Set(value As String)
            mstrDesCity = value
        End Set
    End Property
    Public Property FillingCar As String
        Get
            Return mstrFillingCar
        End Get
        Set(value As String)
            mstrFillingCar = value
        End Set
    End Property
End Class
