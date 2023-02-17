Public Class clsDataError
    Private mintRecordId As Integer
    Private mstrRecordNumber As String
    Private mstrDataError As String
    Private mstrProductDesc As String
    Private mstrDataName As String
    Public Sub New(intRecId As Integer, strRecordNumber As String, strProducDesc As String, strDataName As String, strDataError As String)
        mintRecordId = intRecId
        mstrRecordNumber = strRecordNumber
        mstrProductDesc = strProducDesc
        mstrDataName = strDataName
        mstrDataError = strDataError
    End Sub

    Public Property RecordId As Integer
        Get
            Return mintRecordId
        End Get
        Set(value As Integer)
            mintRecordId = value
        End Set
    End Property
    Public Property RecordNumber As String
        Get
            Return mstrRecordNumber
        End Get
        Set(value As String)
            mstrRecordNumber = value
        End Set
    End Property
    Public Property DataName As String
        Get
            Return mstrDataName
        End Get
        Set(value As String)
            mstrDataName = value
        End Set
    End Property
    Public Property ProductDesc As String
        Get
            Return mstrProductDesc
        End Get
        Set(value As String)
            mstrProductDesc = value
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
End Class
