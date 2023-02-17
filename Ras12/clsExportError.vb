Public Class clsExportError
	Private mdteDOI As Date
	Private mintTravelId As Integer
	Private mstrSupplier As String
	Private mstrSRV As String
	Private mintItemId As Integer
    Private mstrDocument As String
    Private mstrDataError As String
    Private mstrBkgTool As String
    Private mstrRloc As String

	Public Sub New(dteDOI As Date, strSrv As String, intTravelId As Integer, strSupplier As String, intItemId As Integer _
				   , strDataError As String, strBkgTool As String _
				   , Optional strDocument As String = "", Optional strRloc As String = "")
		mdteDOI = dteDOI
		mstrSRV = strSrv
		mintTravelId = intTravelId
		mstrSupplier = strSupplier
		mintItemId = intItemId
		mstrDataError = strDataError
		mstrBkgTool = strBkgTool
		mstrDocument = strDocument
		mstrRloc = strRloc
	End Sub

	Public Property TravelId As Integer
		Get
			Return mintTravelId
		End Get
		Set(value As Integer)
			mintTravelId = value
		End Set
	End Property
	Public Property Supplier As String
		Get
			Return mstrSupplier
		End Get
		Set(value As String)
			mstrSupplier = value
		End Set
	End Property
	Public Property ItemId As Integer
		Get
			Return mintItemId
		End Get
		Set(value As Integer)
			mintItemId = value
		End Set
	End Property
	Public Property Document As String
		Get
			Return mstrDocument
		End Get
		Set(value As String)
			mstrDocument = value
		End Set
	End Property
	Public Property DataError As String
		Get
			Return mstrDataError
		End Get
		Set(value As String)
			mstrDataError = value
		End Set
	End Property
	Public Property BkgTool As String
		Get
			Return mstrBkgTool
		End Get
		Set(value As String)
			mstrBkgTool = value
		End Set
	End Property
	Public Property Rloc As String
		Get
			Return mstrRloc
		End Get
		Set(value As String)
			mstrRloc = value
		End Set
	End Property

	Public Property DOI As Date
		Get
			Return mdteDOI
		End Get
		Set(value As Date)
			mdteDOI = value
		End Set
	End Property

	Public Property SRV As String
		Get
			Return mstrSRV
		End Get
		Set(value As String)
			mstrSRV = value
		End Set
	End Property
End Class
