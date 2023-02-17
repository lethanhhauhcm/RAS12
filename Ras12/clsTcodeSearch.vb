Public Class clsTcodeSearch
    Private mstrVendor As String
    Private mstrAccountName As String
    Private mdteMaxEDate As Date
    Private mintVendorId As Integer
    'Private mintAccountId As Integer
    Public Property Vendor As String
        Get
            Return mstrVendor
        End Get
        Set(value As String)
            mstrVendor = value
        End Set
    End Property
    Public Property AccountName As String
        Get
            Return mstrAccountName
        End Get
        Set(value As String)
            mstrAccountName = value
        End Set
    End Property
    Public Property MaxEDate As Date
        Get
            Return mdteMaxEDate
        End Get
        Set(value As Date)
            mdteMaxEDate = value
        End Set
    End Property
    Public Property VendorId As Integer
        Get
            Return mintVendorId
        End Get
        Set(value As Integer)
            mintVendorId = value
        End Set
    End Property

End Class
