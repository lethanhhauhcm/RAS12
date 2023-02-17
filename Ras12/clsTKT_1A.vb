Public Class clsTKT_1A
    Dim mstrTKNO As String
    Dim mstrFTKT As String
    Dim mstrSRV As String = "S"
    Dim mintQty As Integer = 1
    Dim mdteDOI As Date = Now
    Dim mdteDOF As Date = Now
    Dim mstrBkgClass As String
    Dim mstrPaxName As String
    Dim mstrPaxType As String = "ADL"
    Dim mstrTourCode As String
    Dim mdecFare As Decimal
    Dim mdecShownFare As Decimal
    Dim mdecTax As Decimal
    Dim mdecCharge As Decimal
    Dim mdecCommPCT As Decimal
    Dim mdecCommVAL As Decimal
    Dim mdecNetToAL As Decimal
    Dim mstrItinerary As String
    Dim mstrFareBasis As String
    Dim mstrCurrency As String = "USD"
    Dim mstrTaxDetail As String
    Dim mstrChargeDetail As String
    Dim mdecCreditAmt As Decimal
    Dim mstrOffice As String
    Dim mstrStockCtrl As String
    Dim mstrRloc As String
    Dim mstrAL As String
    Dim mstrFltDate As String
    Dim mstrFltNo As String
    Dim mstrFltStatus As String
    Dim mstrPRG As String
    Dim mdecSvcFee As Decimal
    Dim mstrTktOffc As String
    Dim mdecROE As Decimal = 1
    Dim mintCustId As Integer
    Dim mstrGO_TravelId As String
    Dim mstrFullRtg As String
    Dim mstrStatus As String = "OK"
    Dim mstrLstUser As String
    Dim mbytAutoRAS As Byte
    Dim mstrRemark As String
    Dim mstrSvcFeeCur As String
    Dim mstrFOP As String
    Dim mstrBooker As String
    Dim mdecTax4Cust As Decimal
    Dim mstrCounter As String = "CWT"
    Private mintRecId As Integer

    Public Function CreateRasInsert() As String
        Dim strResult As String = "Insert Into Tkt_1A (" _
                & "TKNO,FTKT,SRV,Qty,DOI,DOF,BkgClass,PaxName,PaxType,TourCode" _
                & ",Fare,ShownFare,Tax,Charge,CommPCT,CommVAL,NetToAL" _
                & ",Itinerary,FareBasis,Currency,TaxDetail,ChargeDetail" _
                & ",CreditAmt,Office,StockCtrl,Rloc,AL,PRG,FltDate,FltNo" _
                & ",SvcFee,TktOffc,ROE,CustId,GO_TravelId,FullRtg" _
                & ",SvcFeeCur,Fop,Booker,AutoRas,Counter) Values ('" _
                & mstrTKNO & "','" & mstrFTKT & "','" & mstrSRV & "'," & mintQty _
                & ",'" & DateTime2Text(mdteDOI) & "','" & DateTime2Text(mdteDOF) _
                & "','" & mstrBkgClass & "','" & mstrPaxName & "','" & mstrPaxType _
                & "','" & mstrTourCode & "'," & mdecFare & "," & mdecShownFare _
                & "," & mdecTax & "," & mdecCharge & "," & mdecCommPCT & "," & mdecCommVAL _
                & "," & mdecNetToAL & ",'" & mstrItinerary & "','" & mstrFareBasis _
                & "','" & mstrCurrency & "','" & mstrTaxDetail & "','" & mstrChargeDetail _
                & "'," & mdecCreditAmt & ",'" & mstrOffice & "','" & mstrStockCtrl _
                & "','" & mstrRloc & "','" & mstrAL & "','" & mstrPRG _
                & "','" & mstrFltDate & "','" & mstrFltNo & "'," & mdecSvcFee _
                & ",'" & mstrTktOffc & "'," & mdecROE & "," & mintCustId _
                & ",'" & mstrGO_TravelId & "','" & mstrFullRtg & "','" & mstrSvcFeeCur _
                & "','" & mstrFOP & "','" & mstrBooker & "'," & mbytAutoRAS _
                & ",'" & mstrCounter & "')"

        Return strResult
    End Function
    Public Property TKNO() As String
        Get
            TKNO = mstrTKNO
        End Get
        Set(ByVal value As String)
            mstrTKNO = value
        End Set
    End Property
    Public Property FTKT() As String
        Get
            FTKT = mstrFTKT
        End Get
        Set(ByVal value As String)
            mstrFTKT = value
        End Set
    End Property
    Public Property SRV() As String
        Get
            SRV = mstrSRV
        End Get
        Set(ByVal value As String)
            mstrSRV = value
        End Set
    End Property
    Public Property Qty() As Integer
        Get
            Qty = mintQty
        End Get
        Set(ByVal value As Integer)
            mintQty = value
        End Set
    End Property
    Public Property DOI() As Date
        Get
            DOI = mdteDOI
        End Get
        Set(ByVal value As Date)
            mdteDOI = value
        End Set
    End Property
    Public Property DOF() As Date
        Get
            DOF = mdteDOF
        End Get
        Set(ByVal value As Date)
            mdteDOF = value
        End Set
    End Property
    Public Property BkgClass() As String
        Get
            BkgClass = mstrBkgClass
        End Get
        Set(ByVal value As String)
            mstrBkgClass = value
        End Set
    End Property

    Public Property PaxName() As String
        Get
            PaxName = mstrPaxName
        End Get
        Set(ByVal value As String)
            mstrPaxName = value
        End Set
    End Property

    Public Property PaxType() As String
        Get
            PaxType = mstrPaxType
        End Get
        Set(ByVal value As String)
            mstrPaxType = value
        End Set
    End Property

    Public Property TourCode() As String
        Get
            TourCode = mstrTourCode
        End Get
        Set(ByVal value As String)
            mstrTourCode = value
        End Set
    End Property
    Public Property Fare() As Decimal
        Get
            Fare = mdecFare
        End Get
        Set(ByVal value As Decimal)
            mdecFare = value
        End Set
    End Property
    
    Public Property ShownFare() As Decimal
        Get
            ShownFare = mdecShownFare
        End Get
        Set(ByVal value As Decimal)
            mdecShownFare = value
        End Set
    End Property
    Public Property Tax() As Decimal
        Get
            Tax = mdecTax
        End Get
        Set(ByVal value As Decimal)
            mdecTax = value
        End Set
    End Property
    Public Property Charge() As Decimal
        Get
            Charge = mdecCharge
        End Get
        Set(ByVal value As Decimal)
            mdecCharge = value
        End Set
    End Property
    Public Property CommPCT() As Decimal
        Get
            CommPCT = mdecCommPCT
        End Get
        Set(ByVal value As Decimal)
            mdecCommPCT = value
        End Set
    End Property
    Public Property CommVAL() As Decimal
        Get
            CommVAL = mdecCommVAL
        End Get
        Set(ByVal value As Decimal)
            mdecCommVAL = value
        End Set
    End Property
    Public Property NetToAL() As Decimal
        Get
            NetToAL = mdecNetToAL
        End Get
        Set(ByVal value As Decimal)
            mdecNetToAL = value
        End Set
    End Property
    Public Property Itinerary() As String
        Get
            Itinerary = mstrItinerary
        End Get
        Set(ByVal value As String)
            mstrItinerary = value
        End Set
    End Property

    Public Property FareBasis() As String
        Get
            FareBasis = mstrFareBasis
        End Get
        Set(ByVal value As String)
            mstrFareBasis = value
        End Set
    End Property

    Public Property Currency() As String
        Get
            Currency = mstrCurrency
        End Get
        Set(ByVal value As String)
            mstrCurrency = value
        End Set
    End Property

    Public Property TaxDetail() As String
        Get
            TaxDetail = mstrTaxDetail
        End Get
        Set(ByVal value As String)
            mstrTaxDetail = value
        End Set
    End Property
    Public Property ChargeDetail() As String
        Get
            ChargeDetail = mstrChargeDetail
        End Get
        Set(ByVal value As String)
            mstrChargeDetail = value
        End Set
    End Property
    Public Property CreditAmt() As Decimal
        Get
            CreditAmt = mdecCreditAmt
        End Get
        Set(ByVal value As Decimal)
            mdecCreditAmt = value
        End Set
    End Property
    Public Property Office() As String
        Get
            Office = mstrOffice
        End Get
        Set(ByVal value As String)
            mstrOffice = value
        End Set
    End Property
    Public Property StockCtrl() As String
        Get
            StockCtrl = mstrStockCtrl
        End Get
        Set(ByVal value As String)
            mstrStockCtrl = value
        End Set
    End Property
    Public Property Rloc() As String
        Get
            Rloc = mstrRloc
        End Get
        Set(ByVal value As String)
            mstrRloc = value
        End Set
    End Property
    Public Property AL() As String
        Get
            AL = mstrAL
        End Get
        Set(ByVal value As String)
            mstrAL = value
        End Set
    End Property
    Public Property FltDate() As String
        Get
            FltDate = mstrFltDate
        End Get
        Set(ByVal value As String)
            mstrFltDate = value
        End Set
    End Property
    Public Property FltNo() As String
        Get
            FltNo = mstrFltNo
        End Get
        Set(ByVal value As String)
            mstrFltNo = value
        End Set
    End Property
    Public Property FltStatus() As String
        Get
            FltStatus = mstrFltStatus
        End Get
        Set(ByVal value As String)
            mstrFltStatus = value
        End Set
    End Property
    Public Property PRG() As String
        Get
            PRG = mstrPRG
        End Get
        Set(ByVal value As String)
            mstrPRG = value
        End Set
    End Property
    Public Property SvcFee() As Decimal
        Get
            SvcFee = mdecSvcFee
        End Get
        Set(ByVal value As Decimal)
            mdecSvcFee = value
        End Set
    End Property

    Public Property TktOffc() As String
        Get
            TktOffc = mstrTktOffc
        End Get
        Set(ByVal value As String)
            mstrTktOffc = value
        End Set
    End Property
    Public Property ROE() As Decimal
        Get
            ROE = mdecROE
        End Get
        Set(ByVal value As Decimal)
            mdecROE = value
        End Set
    End Property
    Public Property CustId() As Integer
        Get
            CustId = mintCustId
        End Get
        Set(ByVal value As Integer)
            mintCustId = value
        End Set
    End Property
    Public Property GO_TravelId() As String
        Get
            GO_TravelId = mstrGO_TravelId
        End Get
        Set(ByVal value As String)
            mstrGO_TravelId = value
        End Set
    End Property
    Public Property FullRtg() As String
        Get
            FullRtg = mstrFullRtg
        End Get
        Set(ByVal value As String)
            mstrFullRtg = value
        End Set
    End Property
    Public Property Status() As String
        Get
            Status = mstrStatus
        End Get
        Set(ByVal value As String)
            mstrStatus = value
        End Set
    End Property
    Public Property LstUser() As String
        Get
            LstUser = mstrLstUser
        End Get
        Set(ByVal value As String)
            mstrLstUser = value
        End Set
    End Property
    Public Property Remark() As String
        Get
            Remark = mstrRemark
        End Get
        Set(ByVal value As String)
            mstrRemark = value
        End Set
    End Property
    Public Property SvcFeeCur() As String
        Get
            SvcFeeCur = mstrSvcFeeCur
        End Get
        Set(ByVal value As String)
            mstrSvcFeeCur = value
        End Set
    End Property
    Public Property FOP() As String
        Get
            FOP = mstrFOP
        End Get
        Set(ByVal value As String)
            mstrFOP = value
        End Set
    End Property
    Public Property Booker() As String
        Get
            Booker = mstrBooker
        End Get
        Set(ByVal value As String)
            mstrBooker = value
        End Set
    End Property
    Public Property Tax4Cust() As Decimal
        Get
            Tax4Cust = mdecTax4Cust
        End Get
        Set(ByVal value As Decimal)
            mdecTax4Cust = value
        End Set
    End Property
    Public Property Counter() As String
        Get
            Counter = mstrCounter
        End Get
        Set(ByVal value As String)
            mstrCounter = value
        End Set
    End Property
    Public Property AutoRAS() As Byte
        Get
            AutoRAS = mbytAutoRAS
        End Get
        Set(ByVal value As Byte)
            mbytAutoRAS = value
        End Set
    End Property
    Public Property RecId() As Integer
        Get
            RecId = mintRecId
        End Get
        Set(ByVal value As Integer)
            mintRecId = value
        End Set
    End Property
End Class
