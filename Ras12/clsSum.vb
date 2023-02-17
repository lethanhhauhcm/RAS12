Public Class clsSum
    Private mintNbr As Integer
    Private mdecAmt As Decimal

    Public Property Nbr As Integer
        Get
            Return mintNbr
        End Get
        Set(value As Integer)
            mintNbr = value
        End Set
    End Property
    Public Property Amt As Decimal
        Get
            Return mdecAmt
        End Get
        Set(value As Decimal)
            mdecAmt = value
        End Set
    End Property
End Class
