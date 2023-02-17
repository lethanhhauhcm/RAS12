'20220606 add by 7643
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Public Class frmAopBsp
    Private FExcel As New Excel.Application
    Private FTotalBalance As Double
    Private FTmpBsp, FTmpAop, FFileName As String
    Private FDateCol, FMemoCol, FAmountCol As Integer
    Private Enum FType
        Aop
        Bsp
    End Enum
    Private FAopTable As New DataTable

    Private Function CheckExcel() As Boolean
        Dim i As Integer
        Dim mFound As Boolean

        mFound = False
        For i = 1 To FExcel.ActiveSheet.usedrange.rows.count
            If (FExcel.ActiveSheet.cells(i, 1).value Is Nothing) Then
                Continue For
            End If

            If FExcel.ActiveSheet.cells(i, 1).value.ToString.ToUpper.Contains("FCAGBILLDET") Then

                mFound = True
                Exit For
            End If
        Next

        If Not mFound Then
            MsgBox(FFileName & " not a FCAGBILLDET file!")
            Return False
        End If

        Return True
    End Function

    Private Function addValue(xType As FType) As String
        Dim mResult, mTKNO, mTRNC, mArrStr(), mMemo, mTRNC2, mAir As String
        Dim i, j, mColBalance As Integer
        Dim mBalance As Double

        mResult = ""
        If xType = FType.Bsp Then
            FTotalBalance = 0
            mAir = ""
            For i = 3 To FExcel.ActiveSheet.UsedRange.rows.count
                If (FExcel.ActiveSheet.cells(i, 4).value IsNot Nothing) AndAlso IsDate(FExcel.ActiveSheet.cells(i, 4).value) Then
                    If FExcel.ActiveSheet.cells(i, 1).value IsNot Nothing Then
                        mAir = FExcel.ActiveSheet.cells(i, 1).value
                    End If

                    mTKNO = ""
                    If FExcel.ActiveSheet.cells(i, 3).value IsNot Nothing Then
                        mTKNO = mAir & FExcel.ActiveSheet.cells(i, 3).value
                    End If

                    mTRNC = ""
                    If (FExcel.ActiveSheet.cells(i, 2).value IsNot Nothing) Then
                        mTRNC = FExcel.ActiveSheet.cells(i, 2).value
                        mTRNC = Replace(mTRNC, "+", "")
                        mTRNC = Replace(mTRNC, ":", ":")
                    End If

                    mBalance = 0
                    If (FExcel.ActiveSheet.cells(i, 2).value IsNot Nothing) AndAlso (Not FExcel.ActiveSheet.cells(i, 2).value.ToString.Contains("+")) Then
                        For j = FExcel.ActiveSheet.UsedRange.columns.count To 6 Step -1
                            If (FExcel.ActiveSheet.cells(i, j).value IsNot Nothing) AndAlso IsNumeric(FExcel.ActiveSheet.cells(i, j).value) Then
                                mColBalance = j
                                Exit For
                            End If
                        Next

                        If FExcel.ActiveSheet.cells(i, mColBalance).value IsNot Nothing Then
                            mBalance = Double.Parse(FExcel.ActiveSheet.cells(i, mColBalance).value)

                            FTotalBalance += mBalance
                        End If
                        mBalance = IIf(mTRNC <> "RFND", mBalance, mBalance * (-1))
                    End If


                    mTRNC2 = IIf(mTRNC <> "RFND", "Bill", mTRNC)

                    If (mTKNO <> "") And (mTRNC <> "CANX") And (mTRNC <> "CANN") Then
                        mResult &= IIf(mResult <> "", "union all ", "") & "select '" & mTKNO & "'," & mBalance.ToString & ",'" & mTRNC & "','" & mTRNC2 & "' "
                    End If
                End If

                RunProgress()
            Next
        ElseIf xType = FType.Aop Then
            For i = 0 To FAopTable.Rows.Count - 1
                If (Not IsDBNull(FAopTable.Rows(i)("AmountDue"))) Then
                    mBalance = FAopTable.Rows(i)("AmountDue")
                Else
                    mBalance = 0
                End If

                mTRNC = FAopTable.Rows(i)("TRNC")

                If (Not IsDBNull(FAopTable.Rows(i)("Memo"))) Then
                    mMemo = FAopTable.Rows(i)("Memo")

                    mArrStr = Split(FAopTable.Rows(i)("Memo"), "/")
                    For j = 0 To mArrStr.Length - 1
                        mTKNO = Strings.Left(mArrStr(j), 15).Trim
                        If mTKNO.Length <> 15 Then Continue For
                        mTKNO = Replace(mTKNO, " ", "")

                        mResult &= IIf(mResult <> "", "union all ", "") & "select '" & mTKNO & "'," & mBalance.ToString & ",'" & mTRNC & "','" & mMemo & "' "
                    Next
                End If

                RunProgress()
            Next
        End If

        Return mResult
    End Function

    Private Function CheckValue() As Boolean
        Dim mFound As Boolean
        Dim i, j As Integer

        mFound = False
        For i = FExcel.ActiveSheet.UsedRange.rows.count To 1 Step -1
            For j = 1 To FExcel.ActiveSheet.UsedRange.columns.count
                If (FExcel.ActiveSheet.cells(i, j).value IsNot Nothing) AndAlso (FExcel.ActiveSheet.cells(i, j).value.ToString.ToUpper = "GRAND TOTAL") Then
                    mFound = True
                    Exit For
                End If
            Next

            If mFound Then
                Exit For
            End If
        Next

        For j = FExcel.ActiveSheet.UsedRange.columns.count To 1 Step -1
            If (FExcel.ActiveSheet.cells(i, j).value IsNot Nothing) AndAlso
                   IsNumeric(FExcel.ActiveSheet.cells(i, j).value) Then

                If FTotalBalance <> Double.Parse(FExcel.ActiveSheet.cells(i, j).value) Then
                    MsgBox("Import Total Balance<>Grand Total Balance" & vbLf &
                               "Import Total Balance:" & FTotalBalance.ToString & vbLf &
                               "Grand Total Balance:" & FExcel.ActiveSheet.cells(i, j).value.ToString)
                    Return False
                End If

                Exit For
            End If
        Next

        Return True
    End Function

    Private Function CreateTmpTable() As Boolean
        Dim mSQL As String

        mSQL = "create table " & FTmpBsp & "(TKNO varchar(15),Balance numeric(21,2),TRNC varchar(6),TRNC2 varchar(6))"
        pobjTvcs.ExecuteNonQuerry(mSQL)

        mSQL = "create table " & FTmpAop & "(TKNO varchar(15),Balance numeric(21,2),TRNC varchar(6),Memo varchar(max))"
        pobjTvcs.ExecuteNonQuerry(mSQL)

        Return True
    End Function

    Private Function InsertTmpTable() As Boolean
        Dim mSQL, mBspValue, mAopValue As String

        mSQL = "select 'Bill' TRNC,Memo,AmountDue from LIB..AOPBill where City='" & myStaff.City & "'  " &
               "union all " &
               "select 'RFND' TRNC,Memo,CreditAmount from LIB..AOPVendorCredit where City='" & myStaff.City & "' " &
               "order by memo"
        FAopTable = pobjTvcs.GetDataTable(mSQL)
        IniProgress("Comparing!", FExcel.ActiveSheet.UsedRange.rows.count - 2 + FAopTable.Rows.Count)

        mBspValue = addValue(FType.Bsp)
        mAopValue = addValue(FType.Aop)
        If ((mBspValue = "") Or (mAopValue = "")) OrElse (Not CheckValue()) Then
            Return False
        Else
            mSQL = "insert into " & FTmpBsp & "(TKNO,Balance,TRNC,TRNC2) "
            mSQL &= mBspValue
            pobjTvcs.ExecuteNonQuerry(mSQL)

            mSQL = "insert into " & FTmpAop & "(TKNO,Balance,TRNC,Memo) "
            mSQL &= mAopValue
            pobjTvcs.ExecuteNonQuerry(mSQL)
        End If

        Return True
    End Function

    Private Sub frmAopBsp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pobjTvcs.Connect()
    End Sub

    Private Sub InserGrid()
        Dim mSQL, mMemo, mStr, mArrStr(), mTKNO, mTRNC As String
        Dim mTable As DataTable
        Dim i, j As Integer
        Dim mBspAmount As Double

        mSQL = "select TKNO,TRNC2,TRNC,Balance from " & FTmpBsp & " where Balance<>0 " &
               "except " &
               "select TmpAop.TKNO,TmpAop.TRNC,TmpBsp.TRNC,TmpBsp.Balance " &
               "from " & FTmpAop & " TmpAop left join " & FTmpBsp & " TmpBsp on TmpAop.TKNO=TmpBsp.TKNO and TmpAop.TRNC=TmpBsp.TRNC2 " &
               "order by TKNO,TRNC"
        mTable = pobjTvcs.GetDataTable(mSQL)
        For i = 0 To mTable.Rows.Count - 1
            dgvMain.Rows.Add(mTable.Rows(i)("TKNO"), mTable.Rows(i)("TRNC"), 0, mTable.Rows(i)("Balance"), "AOP don't have TKNO")
        Next

        mSQL = "select distinct Memo,TRNC,Balance " &
               "from (select TKNO,TRNC,Balance,Memo from " & FTmpAop & " " &
                     "intersect " &
                     "select TmpBsp.TKNO,TmpBsp.TRNC2,TmpAop.Balance,TmpAop.Memo " &
                     "from " & FTmpBsp & " TmpBsp left join " & FTmpAop & " TmpAop on TmpBsp.TKNO=TmpAop.TKNO and TmpBsp.TRNC2=TmpAop.TRNC)a " &
               "order by Memo,TRNC"
        mTable = pobjTvcs.GetDataTable(mSQL)
        For i = 0 To mTable.Rows.Count - 1
            mMemo = mTable.Rows(i)("Memo")
            mTRNC = mTable.Rows(i)("TRNC")
            mTKNO = ""

            mArrStr = Split(mMemo, "/")
            For j = 0 To mArrStr.Length - 1
                mStr = Strings.Left(mArrStr(j), 15)
                mStr = Replace(mStr, " ", "")
                mTKNO &= IIf(mTKNO <> "", ",", "") & "'" & mStr & "'"
            Next

            mSQL = "select sum(Balance) SBalance from " & FTmpBsp & " where TKNO in (" & mTKNO & ") and TRNC2='" & mTRNC & "'"
            mStr = pobjTvcs.GetScalarAsString(mSQL)
            If mStr <> "" Then
                mBspAmount = Double.Parse(mStr)
            Else
                mBspAmount = 0
            End If

            If mTable.Rows(i)("Balance") <> mBspAmount Then
                dgvMain.Rows.Add(mMemo, mTRNC, mTable.Rows(i)("Balance"), mBspAmount, "AopAmount <> BspAmount")
            End If
        Next

        mSQL = "select TKNO,TRNC,Balance from " & FTmpAop & " where Balance<>0 " &
               "except " &
               "select TmpBsp.TKNO,TmpBsp.TRNC2,TmpAop.Balance " &
               "from " & FTmpBsp & " TmpBsp left join " & FTmpAop & " TmpAop on TmpBsp.TKNO=TmpAop.TKNO and TmpBsp.TRNC2=TmpAop.TRNC " &
               "order by TKNO,TRNC"
        mTable = pobjTvcs.GetDataTable(mSQL)
        For i = 0 To mTable.Rows.Count - 1
            dgvMain.Rows.Add(mTable.Rows(i)("TKNO"), mTable.Rows(i)("TRNC"), mTable.Rows(i)("Balance"), 0, "BSP don't have TKNO")
        Next
    End Sub

    Private Sub Compare()
        InserGrid()
        dgvMain.Sort(dgvMain.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
    End Sub

    Private Sub WriteHeader()
        Dim i As Integer

        For i = 0 To dgvMain.Columns.GetColumnCount(0) - 1
            FExcel.ActiveSheet.cells(1, i + 1).value = dgvMain.Columns(i).Name
        Next
    End Sub

    Private Sub WriteDetail()
        Dim i, j As Integer

        IniProgress("Exporting!", dgvMain.Rows.Count)
        For i = 0 To dgvMain.Rows.Count - 1
            For j = 0 To dgvMain.Columns.GetColumnCount(0) - 1
                FExcel.ActiveSheet.cells(i + 2, j + 1).value = dgvMain.Rows(i).Cells(j).Value.ToString
            Next

            RunProgress()
        Next
    End Sub

    Private Sub lbkDownload_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDownload.LinkClicked
        If Not CreateAopQueueDownload4BSP(dtpDueDate.Value, myStaff.City) Then
            MsgBox("Không tạo được yêu cầu Download dữ liệu BSP trong AOP!")
        Else
            LoadBspDownloadQueue()
        End If
    End Sub
    Private Function LoadBspDownloadQueue() As Boolean
        Dim strQuerry As String = "Select top 10 Recid" _
            & ", case B_I when 'B' then 'Bill' when 'VC' then 'VendorCredit' else B_I end as Data " _
            & ",Prod, BU,TrxCode as DueDate" _
            & ", case Status when 'OK' then 'Waiting' when 'RR' then 'Completed' when 'XX' then 'Deleted' else Status end as Status,FstUpdate,Querry" _
            & " from AopQueue" _
            & " where Prod='AOP' order by RecId desc"
        LoadDataGridView(dgvDownload, strQuerry, Conn)
        Return True
    End Function

    Private Sub lbkRefreshDownlloadQueue_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRefreshDownlloadQueue.LinkClicked
        LoadBspDownloadQueue()
    End Sub

    Private Sub ExportExcel()
        Dim mFileName As String
        Dim i As Integer

        FExcel.ActiveSheet.columns(1).numberformat = "@"
        FExcel.ActiveSheet.columns(2).numberformat = "@"
        FExcel.ActiveSheet.columns(3).numberformat = "#,##0.00"
        FExcel.ActiveSheet.columns(4).numberformat = "#,##0.00"
        FExcel.ActiveSheet.columns(5).numberformat = "@"

        WriteHeader()
        WriteDetail()

        For i = 1 To FExcel.ActiveSheet.usedrange.columns.count
            If i = 3 Then
                FExcel.ActiveSheet.Columns(i).ColumnWidth = 50
            Else
                FExcel.ActiveSheet.Columns(i).autofit
            End If
        Next

        mFileName = "D:\AopBsp_" & Now.ToString("yyyyMMddhhmmss") & ".xlsx"
        FExcel.ActiveSheet.saveas(mFileName)
        MsgBox("Export " & mFileName & " complete!")
    End Sub

    Private Sub llbExportExcel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbExportExcel.LinkClicked
        Try
            FExcel.Workbooks.Add()

            ExportExcel()
        Finally
            FExcel.Workbooks.Close()
            FExcel.Quit()
        End Try
    End Sub

    Private Sub llbCompare_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbCompare.LinkClicked
        Dim i As Integer

        With ofdExcel
            .Filter = "Excel file|*.xlsx*"
            If (.ShowDialog <> System.Windows.Forms.DialogResult.OK) Then
                Exit Sub
            End If
        End With

        Try
            For i = 0 To ofdExcel.FileNames.Length - 1
                Try
                    FFileName = ofdExcel.FileNames(i)
                    FExcel.Workbooks.Open(FFileName)

                    If Not CheckExcel() Then
                        Exit Sub
                    End If
                Finally
                    FExcel.Workbooks.Close()
                End Try
            Next
            dgvMain.Rows.Clear()
            FTmpBsp = "##TmpBsp_" & Now.ToString("yyyyMMddhhmmss")
            FTmpAop = "##TmpAop_" & Now.ToString("yyyyMMddhhmmss")
            CreateTmpTable()
            For i = 0 To ofdExcel.FileNames.Length - 1
                Try
                    FFileName = ofdExcel.FileNames(i)
                    FExcel.Workbooks.Open(FFileName)

                    InsertTmpTable()
                Finally
                    FExcel.Workbooks.Close()
                End Try
            Next
            Compare()
            MsgBox("Compare complete!")
            If dgvMain.Rows.Count > 0 Then
                llbExportExcel.Enabled = True
            Else
                llbExportExcel.Enabled = False
            End If
        Finally
            FExcel.Quit()
        End Try
    End Sub
End Class