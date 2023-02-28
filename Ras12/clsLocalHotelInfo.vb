Public Class clsLocalHotelInfo
    Private mstrRoomType As String = String.Empty
    Private mintNbrOfRoom As Integer = 1
    Private mdteCheckInDate As Date
    Private mdteCheckOutDate As Date
    Public Function Parse(strDetails As String) As Boolean
        Dim arrBriefChild As String() = strDetails.Split("|")
        Dim arrSplit As String() = arrBriefChild(0).Split("_")

        If strDetails = "" Or arrBriefChild.Length = 1 Then
            Return False
        End If
        Select Case arrSplit(0)
            Case "SGL"
                If arrSplit(1).Contains("Deluxe") Or arrSplit(1).Contains("Luxury") _
                    Or arrSplit(1).Contains("VIP") Or arrSplit(1) = "Suite" Then
                    mstrRoomType = "A1S"
                ElseIf arrSplit(1).Contains("Superior") Then
                    mstrRoomType = "B1S"
                Else
                    mstrRoomType = "C1S"
                End If
            Case "DBL"
                If arrSplit(1).Contains("Deluxe") Or arrSplit(1).Contains("Luxury") _
                    Or arrSplit(1).Contains("VIP") Or arrSplit(1) = "Suite" Then
                    mstrRoomType = "A2S"
                ElseIf arrSplit(1).Contains("Superior") Then
                    mstrRoomType = "B2S"
                Else
                    mstrRoomType = "C2S"
                End If
            Case "TWN"
                If arrSplit(1).Contains("Deluxe") Or arrSplit(1).Contains("Luxury") _
                    Or arrSplit(1).Contains("VIP") Or arrSplit(1) = "Suite" Then
                    mstrRoomType = "A2T"
                ElseIf arrSplit(1).Contains("Superior") Then
                    mstrRoomType = "B2T"
                Else
                    mstrRoomType = "C2T"
                End If
            Case "TRPL"
                mstrRoomType = "CA3"

        End Select

        mdteCheckInDate = CDate(arrSplit(2))
        mdteCheckOutDate = CDate(arrSplit(3))
        mintNbrOfRoom = arrSplit(4)
        Return True
    End Function
    Public Property RoomType As String
        Get
            RoomType = mstrRoomType
        End Get
        Set(value As String)
            mstrRoomType = value
        End Set
    End Property
    Public Property NbrOfRoom As Integer
        Get
            NbrOfRoom = mintNbrOfRoom
        End Get
        Set(value As Integer)
            mintNbrOfRoom = value
        End Set
    End Property
    Public Property CheckInDate As Date
        Get
            CheckInDate = mdteCheckInDate
        End Get
        Set(value As Date)
            mdteCheckInDate = value
        End Set
    End Property
    Public Property CheckOutDate As Date
        Get
            CheckOutDate = mdteCheckOutDate
        End Get
        Set(value As Date)
            mdteCheckOutDate = value
        End Set
    End Property
End Class
