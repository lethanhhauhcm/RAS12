Imports Microsoft
Module mdlOutlook
    '^_^20220922 mark by 7643 -b-
    'Public Function CreateOutLookEmail(strSubj As String, strMailBody As String, strAddresses As String _
    '                                   , Optional lstFilePaths As List(Of String) = Nothing _
    '                                   , Optional blnSend As Boolean = True) As Boolean
    '^_^20220922 mark by 7643 -e-
    '^_^20220922 modi by 7643 -b-
    Public Function CreateOutLookEmail(strSubj As String, strMailBody As String, strAddresses As String _
                                       , Optional lstFilePaths As List(Of String) = Nothing _
                                       , Optional blnSend As Boolean = True, Optional xCCAddress As String = "") As Boolean
        '^_^20220922 modi by 7643 -e-
        Dim myOL As New Microsoft.Office.Interop.Outlook.Application
        Dim oMsg As Microsoft.Office.Interop.Outlook.MailItem
        oMsg = myOL.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem)
        Dim i As Integer
        Dim arrAddresses As String() = strAddresses.Split(";")
        For Each strAddress As String In arrAddresses
            oMsg.Recipients.Add(strAddress)
        Next
        oMsg.Recipients.ResolveAll()
        If xCCAddress <> "" Then oMsg.CC = xCCAddress  '^_^20220922 add by 7643
        oMsg.Subject = strSubj
        oMsg.Body = strMailBody
        If lstFilePaths IsNot Nothing Then
            For Each strAttachmentFile As String In lstFilePaths
                oMsg.Attachments.Add(strAttachmentFile)
            Next
        End If
        Try
            If blnSend Then
                oMsg.Send()
            Else
                oMsg.Display()
            End If
        Catch ex As Exception
            MsgBox("Unable to send Mail To " & strAddresses, MsgBoxStyle.Critical, msgTitle)
        End Try

    End Function
    'Public Function sRTF_To_HTML(ByVal sRTF As String) As String
    '    'Declare a Word Application Object and a Word WdSaveOptions object
    '    Dim MyWord As New Microsoft.Office.Interop.Word.Application
    '    Dim oDoNotSaveChanges As Object =
    '     Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges
    '    'Declare two strings to handle the data
    '    Dim sReturnString As String = ""
    '    Dim sConvertedString As String = ""
    '    Try
    '        'Instantiate the Word application,
    '        '˜Set visible To False And create a document
    '        MyWord = CreateObject("Word.application")
    '        MyWord.Visible = False
    '        MyWord.Documents.Add()
    '        'Create a DataObject to hold the Rich Text
    '        'and copy it to the clipboard
    '        Dim doRTF As New System.Windows.Forms.DataObject
    '        doRTF.SetData("Rich Text Format", sRTF)
    '        Clipboard.SetDataObject(doRTF)
    '        'Paste the contents of the clipboard to the empty,
    '        'hidden Word Document
    '        MyWord.Windows(1).Selection.Paste()
    '        'â€¦then, select the entire contents of the document
    '        'and copy back to the clipboard
    '        MyWord.Windows(1).Selection.WholeStory()
    '        MyWord.Windows(1).Selection.Copy()
    '        'Now retrieve the HTML property of the DataObject
    '        'stored on the clipboard
    '        sConvertedString =
    '         Clipboard.GetData(System.Windows.Forms.DataFormats.Html)
    '        'Remove some leading text that shows up in some instances
    '        '(like when you insert it into an email in Outlook
    '        sConvertedString =
    '         sConvertedString.Substring(sConvertedString.IndexOf("<html"))
    '        'Also remove multiple Ã‚ characters that somehow end up in there
    '        sConvertedString = sConvertedString.Replace("Ã‚", "")
    '        'â€¦and you're done.
    '        sReturnString = sConvertedString
    '        If Not MyWord Is Nothing Then
    '            MyWord.Quit(oDoNotSaveChanges)
    '            MyWord = Nothing
    '        End If
    '    Catch ex As Exception
    '        If Not MyWord Is Nothing Then
    '            MyWord.Quit(oDoNotSaveChanges)
    '            MyWord = Nothing
    '        End If
    '        MsgBox("Error converting Rich Text to HTML")
    '    End Try
    '    Return sReturnString
    'End Function

End Module
