Imports Microsoft.Office.Interop.Excel

Public Class frmE_InvReport
    Private mblnFirstLoadCompleted As Boolean
    Private mstrInvSettingTable As String = "E_InvSettings"
    Private Sub frmE_InvReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mblnFirstLoadCompleted = True
        cboThongTu.SelectedIndex = 1
    End Sub

    Private Sub lbkRun_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkRun.LinkClicked
        If cboMauSo.Text.EndsWith("001") Then
            If cboThongTu.Text = "78" Then
                ReportMauSo1Tt78()
            Else
                ReportMauSo1()
            End If

        Else
            If cboThongTu.Text = "78" Then
                ReportMauSo2Tt78()
            Else
                ReportMauSo2()
            End If

        End If
    End Sub
    Private Function CheckInputValues() As Boolean
        If dtpFrom.Value.Date < dtpTo.Value.Date Then
            MsgBox("Invalid From/To date!")
            Return False
        End If
        Return True
    End Function
    Private Function ReportMauSo1Tt78()
        Dim objWbk As Workbook
        Dim objWsh As Worksheet
        Dim i As Integer
        Dim strLastColumn As String
        Dim tblInvoice As System.Data.DataTable
        Dim strDateFilter As String = " and DOI between '" & CreateFromDate(dtpFrom.Value) _
                                    & "' and '" & CreateToDate(dtpTo.Value) & "'"
        Dim strFilterRcpId As String = ""

        tblInvoice = GetDataTable("Select Doi,MauSo,KyHieu,InvoiceNo,SRV,CustShortName,CustFullName,''''+TaxCode,'' as Description" _
                                    & ",(select sum(Amount) from lib.dbo.E_InvDetails78 where InvId=i.InvId and Status='ok') as TruocThue" _
                                    & ",'',(select sum(VAT) from lib.dbo.E_InvDetails78 where InvId=i.InvId and Status='ok') as VAT" _
                                    & ",(select sum(Total) from lib.dbo.E_InvDetails78 where InvId=i.InvId and Status='ok') as Total" _
                                    & ",BU,DomInt,Status,NbrOfPax,CodeTour,Booker,Period" _
                                    & " ,(select cast(RcpId as varchar) +',' from lib.dbo.E_InvLinks78 l where l.Status='ok' and l.InvId=i.InvID  For Xml path('')) as RcpId" _
                                    & " from lib.dbo.E_Inv78 i where i.InvoiceNo<>0 and TVC='" _
                                    & cboTVC.Text & "' and Right(mauso,3)='001'" & strDateFilter, Conn)
        If tblInvoice.Rows.Count = 0 Then
            MsgBox("No Invoice for selected Period/MauSo")
            Return False
        End If
        strLastColumn = ConvertExcelColumnNbr2Letter(tblInvoice.Columns.Count)
        Dim objExcel As New Application

        objExcel.Visible = True
        objWbk = objExcel.Workbooks.Add(My.Application.Info.DirectoryPath & "\E_InvMauSo1.xltx")
        objWsh = objWbk.Sheets("List")

        For i = 0 To tblInvoice.Rows.Count - 1
            objWsh.Range("A" & i + 3 & ":" & strLastColumn & i + 3).Value = tblInvoice.Rows(i).ItemArray
            strFilterRcpId = objWsh.Range("U" & i + 3).Value
            If strFilterRcpId IsNot Nothing AndAlso strFilterRcpId.EndsWith(",") Then
                strFilterRcpId = Mid(strFilterRcpId, 1, strFilterRcpId.Length - 1)
            End If
            If strFilterRcpId <> "" Then
                objWsh.Range("U" & i + 3).Value = ScalarToString("Rcp", "RcpNo+','", "Status='ok' and RecId in (" & strFilterRcpId & ") For Xml path('')")
            End If
        Next
        objWsh.Columns("A:A").NumberFormat = "d-mmm-yy"
        objWsh.Columns("J:M").NumberFormat = "#,##0"
        objWsh.Columns("A:" & strLastColumn).AUTOFIT()

        tblInvoice = GetDataTable("Select Doi,MauSo,KyHieu,InvoiceNo,SRV,CustShortName,CustFullName,''''+TaxCode, Description" _
                                    & ",d.Amount,VatPct,VAT,Total" _
                                    & ",BU,DomInt,i.Status,NbrOfPax,CodeTour,Booker,Period" _
                                    & " from lib.dbo.E_Inv78 i " _
                                    & " left join lib.dbo.E_InvDetails78 d on d.InvId=i.InvId " _
                                    & " where d.Status='OK' and i.InvoiceNo<>0 and right(mauso,3)='001'" & strDateFilter _
                                    & " And TVC ='" & cboTVC.Text _
                                    & "' order by i.RecId,i.MauSo,i.KyHieu", Conn)

        objWsh = objWbk.Sheets("Details")
        objExcel.Visible = True

        For i = 0 To tblInvoice.Rows.Count - 1
            objWsh.Range("A" & i + 3 & ":" & strLastColumn & i + 3).Value = tblInvoice.Rows(i).ItemArray
            If objWsh.Range("h" & i + 3).Text.ToString.Contains("E+") Then
                objWsh.Range("h" & i + 3).Value = "'" & objWsh.Range("h" & i + 3).Value
            End If
        Next
        objWsh.Columns("A:A").NumberFormat = "d-mmm-yy"
        objWsh.Columns("I:I").NumberFormat = "@"
        objWsh.Columns("J:M").NumberFormat = "#,##0"
        objWsh.Columns("A:" & strLastColumn).AUTOFIT()
        objExcel.Visible = True
        MsgBox("Completed")



        Return True
    End Function
    Private Function ReportMauSo1()
        Dim objWbk As Workbook
        Dim objWsh As Worksheet
        Dim i As Integer
        Dim strLastColumn As String
        Dim tblInvoice As System.Data.DataTable
        Dim strDateFilter As String = " and DOI between '" & CreateFromDate(dtpFrom.Value) & "' and '" & CreateToDate(dtpTo.Value) & "'"
        Dim strFilterRcpId As String = ""

        tblInvoice = GetDataTable("Select Doi,MauSo,KyHieu,InvoiceNo,SRV,CustShortName,CustFullName,''''+TaxCode,'' as Description" _
                                    & ",(select sum(Amount) from lib.dbo.E_InvDetails where InvId=i.InvId and Status='ok')*Multiplier as TruocThue" _
                                    & ",'',(select sum(VAT) from lib.dbo.E_InvDetails where InvId=i.InvId and Status='ok')*Multiplier as VAT" _
                                    & ",(select sum(Total) from lib.dbo.E_InvDetails where InvId=i.InvId and Status='ok')*Multiplier as Total" _
                                    & ",BU,DomInt,Status,NbrOfPax,CodeTour,Booker,Period" _
                                    & " ,(select cast(RcpId as varchar) +',' from lib.dbo.E_InvLinks l where l.Status='ok' and l.InvId=i.InvID  For Xml path('')) as RcpId" _
                                    & " from lib.dbo.E_Inv i where i.InvoiceNo<>0 and TVC='" _
                                    & cboTVC.Text & "' and SUBSTRING(mauso,9,3)='001'" & strDateFilter, Conn_Web)
        If tblInvoice.Rows.Count = 0 Then
            MsgBox("No Invoice for selected Period/MauSo")
            Return False
        End If
        strLastColumn = ConvertExcelColumnNbr2Letter(tblInvoice.Columns.Count)
        Dim objExcel As New Application

        objExcel.Visible = True
        objWbk = objExcel.Workbooks.Add(My.Application.Info.DirectoryPath & "\E_InvMauSo1.xltx")
        objWsh = objWbk.Sheets("List")

        For i = 0 To tblInvoice.Rows.Count - 1
            objWsh.Range("A" & i + 3 & ":" & strLastColumn & i + 3).Value = tblInvoice.Rows(i).ItemArray
            strFilterRcpId = objWsh.Range("U" & i + 3).Value
            If strFilterRcpId IsNot Nothing AndAlso strFilterRcpId.EndsWith(",") Then
                strFilterRcpId = Mid(strFilterRcpId, 1, strFilterRcpId.Length - 1)
            End If
            If strFilterRcpId <> "" Then
                objWsh.Range("U" & i + 3).Value = ScalarToString("Rcp", "RcpNo+','", "Status='ok' and RecId in (" & strFilterRcpId & ") For Xml path('')")
            End If
        Next
        objWsh.Columns("A:A").NumberFormat = "d-mmm-yy"
        objWsh.Columns("J:M").NumberFormat = "#,##0"
        objWsh.Columns("A:" & strLastColumn).AUTOFIT()

        tblInvoice = GetDataTable("Select Doi,MauSo,KyHieu,InvoiceNo,SRV,CustShortName,CustFullName,''''+TaxCode, Description" _
                                    & ",d.Amount*Multiplier,VatPct,VAT*Multiplier,Total*Multiplier" _
                                    & ",BU,DomInt,i.Status,NbrOfPax,CodeTour,Booker,Period" _
                                    & " from lib.dbo.E_Inv i " _
                                    & " left join lib.dbo.E_InvDetails d on d.InvId=i.InvId " _
                                    & " where d.Status='OK' and i.InvoiceNo<>0 and SUBSTRING(mauso,9,3)='001'" & strDateFilter _
                                    & " And TVC ='" & cboTVC.Text _
                                    & "' order by i.RecId,i.MauSo,i.KyHieu", Conn_Web)

        objWsh = objWbk.Sheets("Details")
        objExcel.Visible = True

        For i = 0 To tblInvoice.Rows.Count - 1
            objWsh.Range("A" & i + 3 & ":" & strLastColumn & i + 3).Value = tblInvoice.Rows(i).ItemArray
            If objWsh.Range("h" & i + 3).Text.ToString.Contains("E+") Then
                objWsh.Range("h" & i + 3).Value = "'" & objWsh.Range("h" & i + 3).Value
            End If
        Next
        objWsh.Columns("A:A").NumberFormat = "d-mmm-yy"
        objWsh.Columns("I:I").NumberFormat = "@"
        objWsh.Columns("J:M").NumberFormat = "#,##0"
        objWsh.Columns("A:" & strLastColumn).AUTOFIT()
        objExcel.Visible = True
        MsgBox("Completed")



        Return True
    End Function
    Private Function ReportMauSo2Tt78()
        Dim objWbk As Workbook
        Dim objWsh As Worksheet
        Dim i As Integer
        Dim strLastColumn As String
        Dim tblInvoice As System.Data.DataTable

        Dim strDateFilter As String = " and i.DOI between '" & CreateFromDate(dtpFrom.Value) & "' and '" & CreateToDate(dtpTo.Value) & "'"
        Dim strGetRcpNo = "(select top 1 RcpNo from Rcp where RecId=(select top 1 RcpId from E_InvLinks78" _
                            & " where InvId=i.InvId And Status='ok')) as RcpNo"
        Dim tblTkt As System.Data.DataTable
        Dim strQuerryTax As String

        tblInvoice = GetDataTable("Select i.AL," & strGetRcpNo _
                                    & ", I.Doi,MauSo,KyHieu,InvoiceNo,i.SRV,i.CustShortName,CustFullName,''''+TaxCode" _
                                    & ",(select Itinerary+',' from tkt where Status<>'XX' and RcpId =(select top 1 RcpId from E_InvLinks78 where InvId=i.InvId and Status='ok') For Xml path('') ) as Description" _
                                    & ",(select Tkno+',' from tkt where Status<>'XX' and RcpId =(select top 1 RcpId from E_InvLinks78 where InvId=i.InvId and Status='ok') For Xml path('') ) as TKNO" _
                                    & ",(select sum(Amount) from E_InvDetails78 where InvId=i.InvId and Status='ok') as TruocThue" _
                                    & ",'' as VatPct" _
                                    & ",(select sum(VAT) from E_InvDetails78 where InvId=i.InvId and Status='ok') as VAT" _
                                    & ",i.Tax+Charge as ThueKhac" _
                                    & ",(Select sum(Total) from E_invDetails78 where Status='OK' and InvId=i.InvId) +i.Tax+i.Charge as Total" _
                                    & ",i.Status" _
                                    & ",(select FOP+',' from FOP where Status='ok' and RcpId in (Select RcpId from E_InvLinks78 where Status='ok' and InvId=i.InvId) For Xml path('') ) as FOP" _
                                    & ",i.InvId" _
                                    & " from E_Inv78 i" _
                                    & " where i.InvoiceNo<>0 and Tvc='" & cboTVC.Text & "' and mauso='" & cboMauSo.Text _
                                    & "'" _
                                    & strDateFilter, Conn)
        If tblInvoice.Rows.Count = 0 Then
            MsgBox("No Invoice for selected Period/MauSo")
            Return False
        End If
        strLastColumn = ConvertExcelColumnNbr2Letter(tblInvoice.Columns.Count - 1)
        Dim objExcel As New Application

        objExcel.Visible = True
        objWbk = objExcel.Workbooks.Add(My.Application.Info.DirectoryPath & "\E_InvMauSo2.xltx")
        objWsh = objWbk.Sheets("List")

        For i = 0 To tblInvoice.Rows.Count - 1
            objWsh.Range("A" & i + 3 & ":" & strLastColumn & i + 3).Value = tblInvoice.Rows(i).ItemArray
            If objWsh.Range("B" & i + 3).Value <> "" Then
                tblTkt = GetDataTable("Select sum(Fare*Qty*r.Roe) as Fare from tkt t" _
                                  & " left join Rcp r on t.RcpId=r.RecId" _
                                  & " where t.status<>'XX'" _
                                  & " and t.Rcpno='" & objWsh.Range("B" & i + 3).Value _
                                  & "' and r.Status<>'XX'")
                If Not IsDBNull(tblTkt.Rows(0)("Fare")) Then
                    objWsh.Range("P" & i + 3).Value = objWsh.Range("Q" & i + 3).Value - tblTkt.Rows(0)("Fare")
                End If
            End If

            objWsh.Range("M" & i + 3).Value = objWsh.Range("Q" & i + 3).Value - objWsh.Range("P" & i + 3).Value

            If objWsh.Range("J" & i + 3).Text.ToString.Contains("E+") Then
                objWsh.Range("J" & i + 3).Value = "'" & objWsh.Range("J" & i + 3).Value
            End If
            If objWsh.Range("K" & i + 3).Text = "" Then
                objWsh.Range("K" & i + 3).Value = GetColumnValuesAsString("E_InvDetails78", "Description", "where Status='OK' and InvId=" & tblInvoice.Rows(i)("InvId"), ",")
                objWsh.Range("L" & i + 3).Value = "'" & GetColumnValuesAsString("E_InvDetails78", "Tkno", "where Status='OK' and InvId=" & tblInvoice.Rows(i)("InvId"), ",")
            End If
        Next
        objWsh.Columns("C:C").NumberFormat = "d-mmm-yy"
        objWsh.Columns("M:Q").NumberFormat = "#,##0"
        objWsh.Columns("A:" & strLastColumn).AUTOFIT()

        tblInvoice = GetDataTable("Select i.AL," & strGetRcpNo _
                                    & ", i.Doi,MauSo,KyHieu,InvoiceNo,i.SRV,i.CustShortName,CustFullName,''''+TaxCode, Description" _
                                    & ",''''+Replace(Tkno,'Hoàn vé ','')" _
                                    & ",d.Amount,VatPct,VAT,'' as ThueKhac,Total" _
                                    & ",i.Status" _
                                    & ",(select FOP+',' from FOP where Status='ok' and RcpId in (Select RcpId from E_InvLinks78 where Status='ok' and InvId=i.InvId) For Xml path('') ) as FOP,'' AS tktDOI" _
                                    & ",i.Tax as Tax" _
                                    & " from E_Inv78 i " _
                                    & " left join E_InvDetails78 d on d.InvId=i.InvId " _
                                    & " where d.Status='OK' and i.InvoiceNo<>0 and mauso='" _
                                    & cboMauSo.Text & "'" & strDateFilter _
                                    & "  and TVC ='" & cboTVC.Text _
                                    & "' order by i.RecId,i.MauSo,i.KyHieu", Conn)

        objWsh = objWbk.Sheets("Details")
        objExcel.Visible = True

        For i = 0 To tblInvoice.Rows.Count - 1
            objWsh.Range("A" & i + 3 & ":" & strLastColumn & i + 3).Value = tblInvoice.Rows(i).ItemArray
            If objWsh.Range("h" & i + 3).Text.ToString.Contains("E+") Then
                objWsh.Range("h" & i + 3).Value = "'" & objWsh.Range("h" & i + 3).Value
            End If
            If tblInvoice.Rows(i)("CustShortName") = "TVSGN" Then
                strQuerryTax = "(t.tax*t.Qty+t.Charge)*r.ROE"
            Else
                strQuerryTax = "(t.tax*t.Qty+t.Charge+t.ChargeTV)*r.ROE"
            End If

            If objWsh.Range("B" & i + 3).Value = "" Then
                objWsh.Range("P" & i + 3).Value = tblInvoice.Rows(i)("Tax")
            Else
                tblTkt = GetDataTable("select top 1 " & strQuerryTax & " as ThueKhac, doi" _
                            & " from tkt t" _
                            & " left join rcp r on t.RcpId=r.RecId" _
                            & " where t.RcpNo='" & objWsh.Range("B" & i + 3).Value _
                            & "' and t.Status<>'xx' and replace(tkno,' ','')='" _
                            & Replace(Replace(objWsh.Range("L" & i + 3).Value, "Hoàn vé", ""), " ", "") & "'")
                If tblTkt.Rows.Count > 0 Then
                    objWsh.Range("P" & i + 3).Value = tblTkt.Rows(0)("ThueKhac")
                    objWsh.Range("M" & i + 3).Value = objWsh.Range("M" & i + 3).Value - objWsh.Range("P" & i + 3).Value
                    objWsh.Range("T" & i + 3).Value = tblTkt.Rows(0)("DOI")
                End If
            End If
            objWsh.Range("Q" & i + 3).Value = objWsh.Range("M" & i + 3).Value + objWsh.Range("P" & i + 3).Value
        Next
        objWsh.Columns("C:C").NumberFormat = "d-mmm-yy"
        objWsh.Columns("T:T").NumberFormat = "d-mmm-yy"
        objWsh.Columns("I:I").NumberFormat = "@"
        objWsh.Columns("M:Q").NumberFormat = "#,##0"
        objWsh.Columns("A:" & strLastColumn).AUTOFIT()

        objExcel.Visible = True

        objExcel.Visible = True
        MsgBox("Completed")

        Return True
    End Function
    Private Function ReportMauSo2()
        Dim objWbk As Workbook
        Dim objWsh As Worksheet
        Dim i As Integer
        Dim strLastColumn As String
        Dim tblInvoice As System.Data.DataTable
        Dim strDateFilter As String = " and i.DOI between '" & CreateFromDate(dtpFrom.Value) & "' and '" & CreateToDate(dtpTo.Value) & "'"
        Dim strGetRcpNo = "(select top 1 RcpNo from Rcp where RecId=(select top 1 RcpId from E_InvLinks where InvId=i.InvId and Status='ok')) as RcpNo"
        Dim tblTkt As System.Data.DataTable
        Dim strQuerryTax As String

        tblInvoice = GetDataTable("Select i.AL," & strGetRcpNo _
                                    & ", I.Doi,MauSo,KyHieu,InvoiceNo,i.SRV,i.CustShortName,CustFullName,''''+TaxCode" _
                                    & ",(select Itinerary+',' from tkt where Status<>'XX' and RcpId =(select top 1 RcpId from E_InvLinks where InvId=i.InvId and Status='ok') For Xml path('') ) as Description" _
                                    & ",(select Tkno+',' from tkt where Status<>'XX' and RcpId =(select top 1 RcpId from E_InvLinks where InvId=i.InvId and Status='ok') For Xml path('') ) as TKNO" _
                                    & ",(select sum(Amount) from E_InvDetails where InvId=i.InvId and Status='ok')*Multiplier as TruocThue" _
                                    & ",'' as VatPct" _
                                    & ",(select sum(VAT) from E_InvDetails where InvId=i.InvId and Status='ok')*Multiplier as VAT" _
                                    & ",i.Tax*Multiplier+Charge as ThueKhac" _
                                    & ",(Select sum(Total) from E_invDetails where Status='OK' and InvId=i.InvId)*Multiplier +i.Tax *Multiplier+i.Charge as Total" _
                                    & ",i.Status" _
                                    & ",(select FOP+',' from FOP where Status='ok' and RcpId in (Select RcpId from E_InvLinks where Status='ok' and InvId=i.InvId) For Xml path('') ) as FOP" _
                                    & ",i.InvId" _
                                    & " from E_Inv i" _
                                    & " where i.InvoiceNo<>0 and Tvc='" & cboTVC.Text & "' and SUBSTRING(mauso,9,3)='" & cboMauSo.Text _
                                    & "'" _
                                    & strDateFilter, Conn)
        If tblInvoice.Rows.Count = 0 Then
            MsgBox("No Invoice for selected Period/MauSo")
            Return False
        End If
        strLastColumn = ConvertExcelColumnNbr2Letter(tblInvoice.Columns.Count - 1)
        Dim objExcel As New Application

        objExcel.Visible = True
        objWbk = objExcel.Workbooks.Add(My.Application.Info.DirectoryPath & "\E_InvMauSo2.xltx")
        objWsh = objWbk.Sheets("List")

        For i = 0 To tblInvoice.Rows.Count - 1
            objWsh.Range("A" & i + 3 & ":" & strLastColumn & i + 3).Value = tblInvoice.Rows(i).ItemArray
            If objWsh.Range("J" & i + 3).Text.ToString.Contains("E+") Then
                objWsh.Range("J" & i + 3).Value = "'" & objWsh.Range("J" & i + 3).Value
            End If
            If objWsh.Range("K" & i + 3).Text = "" Then
                objWsh.Range("K" & i + 3).Value = GetColumnValuesAsString("E_InvDetails", "Description", "where Status='OK' and InvId=" & tblInvoice.Rows(i)("InvId"), ",")
                objWsh.Range("L" & i + 3).Value = "'" & GetColumnValuesAsString("E_InvDetails", "Tkno", "where Status='OK' and InvId=" & tblInvoice.Rows(i)("InvId"), ",")
            End If
        Next
        objWsh.Columns("C:C").NumberFormat = "d-mmm-yy"
        objWsh.Columns("M:Q").NumberFormat = "#,##0"
        objWsh.Columns("A:" & strLastColumn).AUTOFIT()

        tblInvoice = GetDataTable("Select i.AL," & strGetRcpNo _
                                    & ", i.Doi,MauSo,KyHieu,InvoiceNo,i.SRV,i.CustShortName,CustFullName,''''+TaxCode, Description,''''+Tkno" _
                                    & ",d.Amount*Multiplier,VatPct,VAT*Multiplier,'' as ThueKhac,Total*Multiplier" _
                                    & ",i.Status" _
                                    & ",(select FOP+',' from FOP where Status='ok' and RcpId in (Select RcpId from E_InvLinks where Status='ok' and InvId=i.InvId) For Xml path('') ) as FOP,'' AS tktDOI" _
                                    & ",i.Tax*Multiplier as Tax" _
                                    & " from E_Inv i " _
                                    & " left join E_InvDetails d on d.InvId=i.InvId " _
                                    & " where d.Status='OK' and i.InvoiceNo<>0 and SUBSTRING(mauso,9,3)='" _
                                    & cboMauSo.Text & "'" & strDateFilter _
                                    & "  and TVC ='" & cboTVC.Text _
                                    & "' order by i.RecId,i.MauSo,i.KyHieu", Conn)

        objWsh = objWbk.Sheets("Details")
        objExcel.Visible = True

        For i = 0 To tblInvoice.Rows.Count - 1
            objWsh.Range("A" & i + 3 & ":" & strLastColumn & i + 3).Value = tblInvoice.Rows(i).ItemArray
            If objWsh.Range("h" & i + 3).Text.ToString.Contains("E+") Then
                objWsh.Range("h" & i + 3).Value = "'" & objWsh.Range("h" & i + 3).Value
            End If
            If tblInvoice.Rows(i)("CustShortName") = "TVSGN" Then
                strQuerryTax = "tax*Qty+Charge"
            Else
                strQuerryTax = "tax*Qty+Charge+ChargeTV"
            End If

            If objWsh.Range("B" & i + 3).Value = "" Then
                objWsh.Range("P" & i + 3).Value = tblInvoice.Rows(i)("Tax")
            Else
                tblTkt = GetDataTable("select top 1 " & strQuerryTax & " as ThueKhac, doi from tkt where RcpNo='" _
                                  & objWsh.Range("B" & i + 3).Value & "' and Status<>'xx' and replace(tkno,' ','')='" _
                                  & Replace(objWsh.Range("L" & i + 3).Value, " ", "") & "'")
                If tblTkt.Rows.Count > 0 Then
                    objWsh.Range("P" & i + 3).Value = tblTkt.Rows(0)("ThueKhac")
                    objWsh.Range("T" & i + 3).Value = tblTkt.Rows(0)("DOI")
                End If
            End If
            objWsh.Range("Q" & i + 3).Value = objWsh.Range("Q" & i + 3).Value + objWsh.Range("P" & i + 3).Value
        Next
        objWsh.Columns("C:C").NumberFormat = "d-mmm-yy"
        objWsh.Columns("T:T").NumberFormat = "d-mmm-yy"
        objWsh.Columns("I:I").NumberFormat = "@"
        objWsh.Columns("M:Q").NumberFormat = "#,##0"
        objWsh.Columns("A:" & strLastColumn).AUTOFIT()

        objExcel.Visible = True

        objExcel.Visible = True
        MsgBox("Completed")

        Return True
    End Function

    Private Sub cboTVC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTVC.SelectedIndexChanged
        If mblnFirstLoadCompleted Then

            LoadCombo(cboMauSo, "Select distinct MauSo as value from lib.dbo." & mstrInvSettingTable _
                      & " where TVC='" & cboTVC.Text _
                      & "' and City='" & myStaff.City & "' order by MauSo", Conn_Web)
        End If
    End Sub

    Private Sub cboThongTu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboThongTu.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Select Case cboThongTu.Text
                Case "32"
                    mstrInvSettingTable = "E_invSettings"

                Case "78"
                    mstrInvSettingTable = "E_invSettings78"
            End Select
            dtpFrom.Value = DateSerial(Now.AddMonths(-1).Year, Now.AddMonths(-1).Month, 1)
            dtpTo.Value = DateSerial(Now.Year, Now.Month, 1).AddDays(-1)
            LoadCombo(cboTVC, "Select distinct TVC as Value from lib.dbo." & mstrInvSettingTable _
                    & " where Status<>'XX' and City='" & myStaff.City & "' order by TVC", Conn)
        End If
    End Sub
End Class