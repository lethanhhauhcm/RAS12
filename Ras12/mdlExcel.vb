Imports Microsoft.Office.Interop.Excel.XlBordersIndex
Imports Microsoft.Office.Interop.Excel.XlLineStyle
Imports Microsoft.Office.Interop.Excel.XlColorIndex
Imports Microsoft.Office.Interop.Excel.XlBorderWeight
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop

Public Enum XlsBorderType As Integer
    BorderAll = 1
    BorderInside = 2
    BorderOutside = 3
End Enum

Module mdlExcel
    Public Function XlsBorder(ByRef objExcel As Excel.Application _
                                , ByRef objRange As Excel.Range, ByVal intBorderType As XlsBorderType) As Boolean
        With objRange
            .Select()
            If intBorderType = XlsBorderType.BorderAll Or intBorderType = XlsBorderType.BorderOutside Then
                With .Borders(xlEdgeLeft)
                    .LineStyle = XlLineStyle.xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlColorIndexAutomatic
                End With
                With .Borders(xlEdgeTop)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlColorIndexAutomatic
                End With
                With .Borders(xlEdgeBottom)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlColorIndexAutomatic
                End With
                With .Borders(xlEdgeRight)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlColorIndexAutomatic
                End With
            End If
            If intBorderType = XlsBorderType.BorderAll Or intBorderType = XlsBorderType.BorderInside Then
                With .Borders(xlInsideVertical)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlColorIndexAutomatic
                End With
                If .Rows.Count > 1 Then
                    With .Borders(xlInsideHorizontal)
                        .LineStyle = xlContinuous
                        .Weight = xlThin
                        .ColorIndex = xlColorIndexAutomatic
                    End With
                End If

            End If
        End With
        Return True
    End Function
    Public Function Table2ExcelCts(tblInput As System.Data.DataTable _
                                , Optional strFileName As String = "" _
                                , Optional strVndColumns As String = "" _
                                , Optional lstDateColumns As List(Of String) = Nothing) As Boolean
        If tblInput.Rows.Count > 0 Then
            Dim i As Integer
            Dim objExcel As New Microsoft.Office.Interop.Excel.Application
            Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
            Dim objWbk As Microsoft.Office.Interop.Excel._Workbook
            Dim lstQuerries As New List(Of String)
            Dim strLastColumn As String = ConvertExcelColumnNbr2Letter(tblInput.Columns.Count)

            objExcel.Visible = False
            objWbk = objExcel.Workbooks.Add
            objWsh = objWbk.ActiveSheet
            With objWsh
                For i = 0 To tblInput.Columns.Count - 1
                    .Cells(1, i + 1) = tblInput.Columns(i).ColumnName
                Next

                For i = 0 To tblInput.Rows.Count - 1
                    objWsh.Range("A" & i + 2 & ":" & strLastColumn & i + 2).Value = tblInput.Rows(i).ItemArray
                Next


                For i = 1 To strVndColumns.Length
                    .Columns(Mid(strVndColumns, i, 1)).numberformat = "#,##0"
                Next

                If lstDateColumns IsNot Nothing Then
                    For Each strColumn As String In lstDateColumns
                        .Columns(strColumn).numberformat = "dd mmm yy"
                    Next
                End If
                .Columns("A:" & strLastColumn).AUTOFIT()
                .Rows("2:" & tblInput.Rows.Count + 1).EntireRow.AutoFit()

            End With

            If strFileName <> "" Then
                objWbk.SaveAs(strFileName)
            End If
            objExcel.Visible = True
        End If

        Return True
    End Function
    Public Function Table2Excel(tblInput As System.Data.DataTable _
                                , Optional strFileName As String = "" _
                                , Optional blnCopy2Clipboard As Boolean = False _
                                , Optional blnCloseXls As Boolean = False) As Boolean

        Dim I As Integer
        If tblInput.Rows.Count > 0 Then
            Dim objExcel As New Microsoft.Office.Interop.Excel.Application
            Dim objWsh As Microsoft.Office.Interop.Excel.Worksheet
            Dim objWbk As Microsoft.Office.Interop.Excel._Workbook
            Dim lstQuerries As New List(Of String)
            Dim strLastColumn As String = ConvertExcelColumnNbr2Letter(tblInput.Columns.Count)

            objExcel.Visible = False
            objWbk = objExcel.Workbooks.Add
            objWsh = objWbk.ActiveSheet
            With objWsh
                For I = 0 To tblInput.Columns.Count - 1
                    .Cells(1, I + 1) = tblInput.Columns(I).ColumnName
                Next

                For I = 0 To tblInput.Rows.Count - 1
                    objWsh.Range("A" & I + 2 & ":" & strLastColumn & I + 2).Value = tblInput.Rows(I).ItemArray
                Next
                .Columns("A:" & strLastColumn).AUTOFIT()
                If blnCopy2Clipboard Then
                    .Range("A1:" & strLastColumn & tblInput.Rows.Count).Copy()
                    objExcel.Quit()
                    Return True
                End If
            End With

            If strFileName <> "" Then
                objWbk.SaveAs(strFileName)
            End If
            If blnCloseXls Then
                objExcel.Quit()
            Else
                objExcel.Visible = True
            End If

        End If

        Return True
    End Function
    Public Function Table2ExcelSheet(tblInput As System.Data.DataTable _
                                , ByRef objWsh As Worksheet, blnGetTableHeader As Boolean) As Boolean
        Dim I As Integer
        If tblInput.Rows.Count > 0 Then
            Dim strLastColumn As String = ConvertExcelColumnNbr2Letter(tblInput.Columns.Count)

            With objWsh
                If blnGetTableHeader Then
                    For I = 0 To tblInput.Columns.Count - 1
                        .Cells(1, I + 1) = tblInput.Columns(I).ColumnName
                    Next

                End If

                For I = 0 To tblInput.Rows.Count - 1
                    objWsh.Range("A" & I + 2 & ":" & strLastColumn & I + 2).Value = tblInput.Rows(I).ItemArray
                Next
                .Columns("A:" & strLastColumn).AUTOFIT()
            End With
        End If

        Return True
    End Function
    Public Function ConvertExcelColumnNbr2Letter(iCol As Integer) As String
        Dim iAlpha As Integer
        Dim iRemainder As Integer
        Dim strColumn As String = String.Empty
        iAlpha = Int(iCol / 27)
        iRemainder = iCol - (iAlpha * 26)
        If iAlpha > 0 Then
            strColumn = Chr(iAlpha + 64)
        End If
        If iRemainder > 0 Then
            strColumn = strColumn & Chr(iRemainder + 64)
        End If
        Return strColumn
    End Function
End Module
