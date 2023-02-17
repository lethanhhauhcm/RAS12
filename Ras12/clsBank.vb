Imports System.Text.RegularExpressions
Public Class clsBank
    Private mstrBankName As String
    Private mstrBranch As String

    Public Function ParseName(strBankFullName As String) As Boolean

        If strBankFullName.Contains("-CN ") Then
            mstrBankName = Mid(strBankFullName, 1, InStr(strBankFullName, "-CN ") - 1).Trim
            mstrBranch = Mid(strBankFullName, InStr(strBankFullName, "-CN ") + 4)
        ElseIf strBankFullName.Contains(" CN ") Then
            mstrBankName = Mid(strBankFullName, 1, InStr(strBankFullName, " CN ") - 1)
            mstrBranch = Mid(strBankFullName, InStr(strBankFullName, " CN ") + 4)
        Else
            mstrBankName = strBankFullName
        End If
        Return True
    End Function
    Public Property BankName As String
        Get
            Return mstrBankName
        End Get
        Set(value As String)
            mstrBankName = value
        End Set
    End Property
    Public Property Branch As String
        Get
            Return mstrBranch
        End Get
        Set(value As String)
            mstrBranch = value
        End Set
    End Property
End Class
