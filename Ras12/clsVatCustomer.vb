
Public Class clsVatCustomer
    Dim mstrCustomerName As String
    Dim mstrVatNbr As String
    Dim mstrAddr As String

    Public Property CustomerName() As String
        Get
            CustomerName = mstrCustomerName
        End Get
        Set(ByVal value As String)
            mstrCustomerName = value
        End Set
    End Property
    Public Property VatNbr() As String
        Get
            VatNbr = mstrVatNbr
        End Get
        Set(ByVal value As String)
            mstrVatNbr = value
        End Set
    End Property
    Public Property Addr() As String
        Get
            Addr = mstrAddr
        End Get
        Set(ByVal value As String)
            mstrAddr = value
        End Set
    End Property
End Class
