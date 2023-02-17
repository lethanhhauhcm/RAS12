Imports Microsoft.Office.Interop.Excel
Public Class frmImportProfiles
    Private Sub lbkImport_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkImport.LinkClicked
        Dim objOfd As New OpenFileDialog

        With objOfd
            .InitialDirectory = "W:\CTS\SALE CTS - NEW\6.TMC\REEDnMACKAY\HR feed"
            .DefaultExt = "csv"
            .Filter = "csv files (*.csv)|"
            .ShowDialog()
            If .FileName = "" Then
                Exit Sub
            ElseIf ScalarToInt("[CWT].[dbo].[misc]", "RecId", "Status='OK' and Val='" & .SafeFileName & "'") Then
                MsgBox("File had been imported in the past!")
                Exit Sub
            ElseIf Not ImportHrFile(.FileName) Then
                MsgBox("Unable to import file:" & .FileName)
                Exit Sub
            Else
                Search()
            End If

        End With
    End Sub

    Private Sub frmImportProfiles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
    End Sub
    Private Function Search() As Boolean
        LoadDataGridView(dgrHrFiles, "select top 32 Val as FileName,Status,FstUpdate,FstUser" _
                        & " from [CWT].[dbo].[misc] where cat='RmHrFiles' order by FstUpdate DESC", Conn)
        Return True
    End Function
    Private Function ImportHrFile(strFilePath As String) As Boolean
        Dim objExcel As New Application
        Dim objWbk As Workbook
        Dim objWsh As Worksheet
        Dim lstQuerries As New List(Of String)
        Dim i As Integer
        Dim intFileId As Integer
        Dim arrPathBreaks As String() = strFilePath.Split("\")
        lstQuerries.Add("insert into Cwt.dbo.Misc (CAT,VAL,Status,FstUser) values ('RmHrFiles','" _
                        & arrPathBreaks(arrPathBreaks.Length - 1) & "','QQ','" & myStaff.SICode & "')")
        If Not UpdateListOfQuerries(lstQuerries, Conn, True, intFileId) Then
            Return False
        End If
        lstQuerries.Clear()

        objExcel.Visible = True

        objWbk = objExcel.Workbooks.Open(strFilePath, , True)
        objWsh = objWbk.ActiveSheet
        With objWsh
            i = 2
            Do While .Range("A" & i).Value IsNot Nothing AndAlso .Range("A" & i).Value.ToString <> ""
                lstQuerries.Add("insert into [CWT].[dbo].[RM_TravelerProfiles] (TravellerID, CorporateID, SecurityCode" _
                                & ", Forename, MiddleName, Surname, Title, EmailAddress,JobTitle" _
                                & ",CreatedDateTime, LastUpdateTime, LastReviewDate,HrFileId" _
                                & ", Reference1, Reference2, Reference3, Reference4, Reference5" _
                                & ", Reference6, Reference7, Reference8, Reference9, Reference10" _
                                & ") values (" & .Range("A" & i).Value & "," & .Range("B" & i).Value _
                                & ",'" & .Range("C" & i).Value & "','" & .Range("D" & i).Value _
                                & "','" & .Range("E" & i).Value & "','" & .Range("F" & i).Value _
                                & "','" & .Range("G" & i).Value & "','" & .Range("H" & i).Value _
                                & "','" & .Range("S" & i).Value _
                                & "','" & .Range("DE" & i).Value & "','" & .Range("DF" & i).Value _
                                & "','" & .Range("DG" & i).Value & "'," & intFileId _
                                & ",'" & .Range("CT" & i).Value & "','" & .Range("CU" & i).Value _
                                & "','" & .Range("CV" & i).Value & "','" & .Range("CW" & i).Value _
                                & "','" & .Range("CX" & i).Value & "','" & .Range("CY" & i).Value _
                                & "','" & .Range("DB" & i).Value & "','" & .Range("DC" & i).Value _
                                & "','" & .Range("CZ" & i).Value & "','" & .Range("DA" & i).Value _
                                & "')")
                i = i + 1
            Loop
        End With
        lstQuerries.Add("update Cwt.dbo.Misc set status='OK' where RecId=" & intFileId)
        objExcel.Quit()
        Return UpdateListOfQuerries(lstQuerries, Conn)

    End Function
End Class