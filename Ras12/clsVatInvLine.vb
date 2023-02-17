Public Class clsVatInvLine
    Private mdecVatPct As Decimal = -1
    Private mdecProviderCost As Decimal
    Private mdecVat As Decimal
    Private mstrDesc As String
    Private mstrRelatedSvc As String
    Private mblnSvcFeeFounnd As Boolean
    Private mblnMerchantFeeFound As Boolean
    Private mblnBankFeeFounnd As Boolean
    Private mblnHotelFound As Boolean
    Private mblnCarFound As Boolean
    Private mblnVisaFound As Boolean
    Private mblnMiscSvcFound As Boolean
    Private mblnDifferentVatPct As Boolean
    Private mstrPaxNames As String = String.Empty
    Private mblnMealFound As Boolean
    Private mstrUnit As String
    Private mintQuantity As Integer
    Private mstrPaxName As String
    Public Function AddDesc(strNewDesc As String) As Boolean
        If mstrDesc = "" Then
            mstrDesc = strNewDesc
        ElseIf mstrDesc.Contains(strNewDesc) Then
            'bo qua
        Else
            mstrDesc = mstrDesc & "+" & strNewDesc
        End If
        Return True
    End Function
    Public Function AddRelatedSvc(strNewRelatedSvc As String) As Boolean
        If mstrRelatedSvc = "" Then
            mstrRelatedSvc = strNewRelatedSvc
        ElseIf mstrRelatedSvc.Contains(strNewRelatedSvc) Then
            'bo qua
        Else
            mstrRelatedSvc = mstrRelatedSvc & "+" & strNewRelatedSvc
        End If
        Return True
    End Function
    Public Function AddNonDupValues(strNewValue As String, ByRef strExistingValue As String) As Boolean
        If strExistingValue = "" Then
            strExistingValue = strNewValue
        ElseIf strExistingValue.Contains(strNewValue) Then
            'bo qua
        Else
            strExistingValue = strExistingValue & "+" & strNewValue
        End If
        Return True
    End Function
    Public Property Vat As Decimal
        Get
            Return mdecVat
        End Get
        Set(value As Decimal)
            mdecVat = value
        End Set
    End Property
    Public Property VatPct As Decimal
        Get
            Return mdecVatPct
        End Get
        Set(value As Decimal)
            If mdecVatPct = -1 Then
                mblnDifferentVatPct = False
            ElseIf mdecVatPct = value Then
                mblnDifferentVatPct = False
            Else
                mblnDifferentVatPct = True
            End If
            mdecVatPct = value
        End Set
    End Property
    Public Property ProviderCost As Decimal
        Get
            Return mdecProviderCost
        End Get
        Set(value As Decimal)
            mdecProviderCost = value
        End Set
    End Property
    Public Property Desc As String
        Get
            Return mstrDesc
        End Get
        Set(value As String)
            mstrDesc = value
        End Set
    End Property
    Public Property SvcFeeFound As Boolean
        Get
            Return mblnSvcFeeFounnd
        End Get
        Set(value As Boolean)
            mblnSvcFeeFounnd = value
        End Set
    End Property
    Public Property MerchantFeeFound As Boolean
        Get
            Return mblnMerchantFeeFound
        End Get
        Set(value As Boolean)
            mblnMerchantFeeFound = value
        End Set
    End Property
    Public Property BankFeeFound As Boolean
        Get
            Return mblnBankFeeFounnd
        End Get
        Set(value As Boolean)
            mblnBankFeeFounnd = value
        End Set
    End Property
    Public Property Hotel As Boolean
        Get
            Return mblnHotelFound
        End Get
        Set(value As Boolean)
            mblnHotelFound = value
        End Set
    End Property
    Public Property Car As Boolean
        Get
            Return mblnCarFound
        End Get
        Set(value As Boolean)
            mblnCarFound = value
        End Set
    End Property
    Public Property Visa As Boolean
        Get
            Return mblnVisaFound
        End Get
        Set(value As Boolean)
            mblnVisaFound = value
        End Set
    End Property
    Public Property Meal As Boolean
        Get
            Return mblnMealFound
        End Get
        Set(value As Boolean)
            mblnMealFound = value
        End Set
    End Property
    Public Property MiscSvc As Boolean
        Get
            Return mblnMiscSvcFound
        End Get
        Set(value As Boolean)
            mblnMiscSvcFound = value
        End Set
    End Property
    Public Property DifferentVatPct As Boolean
        Get
            Return mblnDifferentVatPct
        End Get
        Set(value As Boolean)
            mblnDifferentVatPct = value
        End Set
    End Property
    Public Property RelatedSvc As String
        Get
            Return mstrRelatedSvc
        End Get
        Set(value As String)
            mstrRelatedSvc = value
        End Set
    End Property
    Public Property PaxNames As String
        Get
            Return mstrPaxNames
        End Get
        Set(value As String)
            mstrPaxNames = value
        End Set
    End Property
    Public Property Unit As String
        Get
            Return mstrUnit
        End Get
        Set(value As String)
            mstrUnit = value
        End Set
    End Property
    Public Property Quantity As Integer
        Get
            Return mintQuantity
        End Get
        Set(value As Integer)
            mintQuantity = value
        End Set
    End Property
    Public Property PaxName As String
        Get
            Return mstrPaxName
        End Get
        Set(value As String)
            mstrPaxName = value
        End Set
    End Property
End Class
