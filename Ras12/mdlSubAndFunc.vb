Imports RAS12.MySharedFunctions
Imports RAS12.MySharedFunctionsWzConn
Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop
Imports System.Drawing.Printing

Module mdlSubAndFunc
    Private cmd As SqlClient.SqlCommand = Conn.CreateCommand
    Public FCmd As SqlClient.SqlCommand = Conn.CreateCommand  '^_^20230215 add by 7643
    Private FTrans As SqlClient.SqlTransaction  '^_^20230215 add by 7643
    Public Function GetLastFileName_FullPath(pDDan As String, pPattern As String) As String
        Dim DDan As String = pDDan
        Dim FileNamePattern As String = DDan & pPattern
        Dim LstModif As Date = CDate("01-Jan-2010")
        Dim tmpFName As String = Dir(FileNamePattern)
        Dim LstFileName As String = ""
        Do While tmpFName <> ""
            If LstModif < File.GetCreationTime(DDan & tmpFName) Then
                LstModif = File.GetCreationTime(DDan & tmpFName)
                LstFileName = DDan & tmpFName
            End If
            tmpFName = Dir()
        Loop
        Return LstFileName
    End Function
    Public Sub Book_a_MSGR(pSI As String, pPSW As String, pApp As String, pTC As String, pID As Integer)
        Dim Fname As String = GetLastFileName_FullPath("X:\RAS2K7\", "MSGR*.exe")
        Fname = Fname & " " & pSI & "|" & pPSW & "|" & pApp & "|" & pTC & "|" & pID
        On Error Resume Next
        Shell(Fname)
        On Error GoTo 0
    End Sub
    Public Function getNextXX_Status(pRCPID As Integer) As String
        Dim KQ As String = ScalarToString("INV", "top 1 Status", "RCPID=" & pRCPID & " order by Status desc")
        Dim ChckChar As String = KQ.Substring(1, 1)
        If KQ = "OK" Then
            Return "X0"
        ElseIf InStr("012345678", ChckChar) > 0 Then
            Return "X" & (CInt(ChckChar) + 1).ToString.Trim
        ElseIf ChckChar = "9" Then
            Return "XA"
        End If
        Return "X" & Chr(Asc(ChckChar) + 1)
    End Function
    Public Sub AutoUploadPPD2TransVietVN()
        Dim DDanPPD As String = ScalarToString("MISC", "VAL", "cat='TRXListingPath'")
        Dim FName As String = Dir(DDanPPD & "TRXlisting*PPD_RPT.xls")
        On Error Resume Next
        Do While FName <> ""
            If UploadFileToFtp(DDanPPD & FName, "ftp://118.69.81.103/transviet.vn/wwwroot/Upload/", "TransViet", "ftp@123456789", "APP") = "True" Then
                Kill(DDanPPD & FName)

            End If
            'UploadFileToFtp(DDanPPD & FName, "ftp://transviet.vn/Upload/", "transviet.vn", "Abcd@123456789", "APP")
            FName = Dir()
        Loop
        On Error GoTo 0
    End Sub

    Public Function HasNewerVersion_R12(pProductVersion As String) As Boolean
        Return False
        Dim VerConn As New SqlClient.SqlConnection
        If My.Computer.Name = "5-247" Then Return False

        Dim AppName As String = Application.ExecutablePath
        If AppName.ToLower.Contains("_0.exe") Then
            MsgBox("Please Use RAS12-RunMe.exe file to Start Application", MsgBoxStyle.Critical, msgTitle)
            Return True
        End If

        VerConn.ConnectionString = "server=" & MySession.ServerIP & ";uid=reporter;pwd=reporter;database=RAS12"
        VerConn.Open()
        Dim cmd As SqlClient.SqlCommand = VerConn.CreateCommand
        cmd.CommandText = "Select VAL from MISC where cat='APPVERSION'"
        Dim vProductVersion As String = cmd.ExecuteScalar
        VerConn.Close()
        If vProductVersion <> pProductVersion Then
            MsgBox("Newer Version Available. Please Quit This Application and Run it Again.", MsgBoxStyle.Critical, msgTitle)
            Return True
        End If
        Return False
    End Function
    Public Function isExistExeNewerThanMe(pCurrentExeName As String) As Boolean
        If My.Computer.Name = "5-086" Then Return False
        If Not pCurrentExeName.Contains("_") Then Return True ' buoc ten file co dang AppYY_*.exe
        Dim CurrentExeDate As Date = File.GetLastWriteTime(DDAN & "\" & pCurrentExeName)
        Dim tmpFileName As String = Dir(DDAN & pCurrentExeName.Substring(0, 7) & "*.exe")
        Do While tmpFileName <> ""
            If tmpFileName.Contains("_") Then
                If File.GetLastWriteTime(DDAN & "\" & tmpFileName) > CurrentExeDate Then Return True
            End If
            tmpFileName = Dir()
        Loop
        Return False
    End Function

    Public Function GetFareTaxChargeInVND(pSRV As String, pINVID As Integer) As Decimal
        Dim KQ As Decimal
        Dim dTbl As DataTable = GetDataTable("Select F_VND, T_VND, C_VND from TKTNO_INVNO where status='OK' and INVID=" & pINVID)
        For i As Int16 = 0 To dTbl.Rows.Count - 1
            If pSRV = "S" Then
                KQ = KQ + dTbl.Rows(i)("F_VND") + dTbl.Rows(i)("T_VND") + dTbl.Rows(i)("C_VND")
            ElseIf pSRV = "R" Then
                KQ = KQ + Math.Abs(dTbl.Rows(i)("F_VND")) + Math.Abs(dTbl.Rows(i)("T_VND")) - dTbl.Rows(i)("C_VND")
            End If
        Next
        Return KQ
    End Function
    Public Function FillRBD_byRTG(RTG As String, pRBD As String) As String
        Dim KQ As String = pRBD
        RTG = RTG.Replace(" ", "")
        If RTG = "" Then Return ""
        Dim NoOfSeg As Int16, tmpRBD As String = ""
        NoOfSeg = (RTG.Length - 3) / 5
        If KQ = "" Then KQ = StrDup(NoOfSeg, "Y")
        If NoOfSeg > KQ.Length Then
            tmpRBD = StrDup(NoOfSeg - KQ.Length, KQ.Substring(0))
            KQ = KQ & tmpRBD
        ElseIf NoOfSeg < KQ.Length Then
            KQ = KQ.Substring(0, NoOfSeg)
        End If
        If NoOfSeg > 2 Then
            For i As Int16 = 1 To NoOfSeg - 2
                If RTG.Substring(5 * i + 3, 2) = "//" Then
                    KQ = KQ.Substring(0, i) & "-" & KQ.Substring(i + 1)
                End If
            Next
        End If
        Return KQ
    End Function
    Public Function FillFB_byRTG(RTG As String, pFB As String) As String
        Dim tmpFB As String = pFB, DefaultFB As String
        If tmpFB = "" Then tmpFB = "Y"
        'tmpFB = tmpFB.Replace("++", "+//+")
        RTG = RTG.Replace(" ", "")
        Dim NoOfSeg As Int16, NoOfPlus As Int16, FBi(16) As String
        NoOfSeg = (RTG.Length - 3) / 5 - 1
        NoOfPlus = UBound(tmpFB.Split("+"))
        If NoOfSeg > NoOfPlus Then
            DefaultFB = tmpFB.Split("+")(0)
            For i As Int16 = 1 To NoOfSeg - NoOfPlus
                tmpFB = tmpFB & "+" & DefaultFB
            Next
        ElseIf NoOfSeg < NoOfPlus Then
            For i As Int16 = 0 To NoOfSeg - 1
                tmpFB = tmpFB & "+" & tmpFB.Split("+")(i)
                tmpFB = tmpFB.Substring(1)
            Next
        End If
        FBi = Split(tmpFB, "+")
        For i As Int16 = 1 To NoOfSeg - 1
            If RTG.Substring(5 * i + 3, 2) = "//" Then
                FBi(i) = ""
            End If
        Next
        tmpFB = FBi(0)
        For i As Int16 = 1 To NoOfSeg
            tmpFB = tmpFB + "+" + FBi(i)
        Next
        Return tmpFB
    End Function
    Public Sub TaoBanGhiTKTNO_INVNO_Standard(pRCPID As Integer, pINVNO As String, pINVID As Integer, pWzOrWoCharge As String)
        Dim ROE As Decimal = ScalarToDec("RCP", "ROE", "RecID=" & pRCPID)
        cmd.CommandText = "insert TKTNO_INVNO (INVNO, INVID, FstUser, RCPID, TKNO, F_VND, T_VND, C_VND, " &
            " CTV_VND) select '" & pINVNO & "'," & pINVID & ", FstUser, RCPID, TKNO, " &
            " (Fare-AgtDisctVAL)*qty*" & ROE & ", Tax*Qty*" & ROE & ", Charge*" & ROE
        If pWzOrWoCharge = "WO" Then
            cmd.CommandText = cmd.CommandText & ", 0"
        Else
            cmd.CommandText = cmd.CommandText & ", ChargeTV*" & ROE
        End If
        cmd.CommandText = cmd.CommandText & " from TKT where status='OK' and RCPID=" & pRCPID
        cmd.ExecuteNonQuery()
    End Sub
    Public Function Invalid3Rd(pDocNo As String, pCustID As Integer) As Boolean
        If pCustID <> 8085 Or pDocNo <> "TS24" Then Return True
        Return False
    End Function
    Public Function InvalidTourCode(ByVal pTC As String, ByVal pCustID As Integer, pSRV As String, pTKNO As String, pIsNew As Boolean, pDOI As Date, Optional pExDoc As String = "") As Boolean
        Dim RecNo As Integer, RCPID As Integer, S_TCode As String, vWhat As String = IIf(pSRV = "R", pTKNO, pExDoc)
        Dim strDK As String
        If MySession.Counter <> "CWT" Then Return True
        strDK = " custid=" & pCustID & " and TCode='" & pTC & "' and BillingBy in ('Event','Bundle') and status not in ('XX','RR')"
        If pSRV = "R" Or pExDoc <> "" Then
            RecNo = ScalarToInt("DuToan_Tour", "RecID", strDK)
        ElseIf pSRV = "S" And pExDoc = "" Then
            If pIsNew Then strDK = strDK & " and edate >='" & pDOI & "'"
            RecNo = ScalarToInt("DuToan_Tour", "RecID", strDK)
        End If
        If RecNo = 0 Then
            RecNo = ScalarToInt("Ras12.dbo.TourInfo", "count (*)", "TourCode='" & pTC & "'")
        End If
        Return (RecNo = 0)
    End Function

    Public Function GenPseudoTKT(ByVal pDoc As String, ByVal pAL As String) As String
        Dim KQ As String, i As Integer
        KQ = ScalarToString("TKT", "top 1 TKNO", "left(tkno,6)='" & pDoc & " " & pAL & "' order by TKNO desc")
        If KQ = "" Then
            KQ = pDoc & " " & pAL & "00 000001"
        Else
            i = CInt(Strings.Right(KQ, 6)) + 1
            KQ = pDoc & " " & pAL & "00 " & Format(i, "000000")
        End If
        Return KQ
    End Function
    Public Sub LoadCombo(ByRef cboInput As ComboBox, ByVal strQuerry As String _
                         , objConn As SqlClient.SqlConnection)
        Dim daConditions As SqlClient.SqlDataAdapter
        Dim dsConditions As New DataSet

        daConditions = New SqlClient.SqlDataAdapter(strQuerry, objConn)
        If daConditions.Fill(dsConditions, "RESULT") > 0 Then
            cboInput.DataSource = dsConditions.Tables("RESULT")
            cboInput.DisplayMember = "Value"
            cboInput.ValueMember = "Value"
            'LoadCombo = cboInput
            dsConditions.Dispose()
            daConditions.Dispose()
        End If
    End Sub
    Public Function LoadComboDisplay(ByRef cboInput As ComboBox, ByVal strQuerry As String _
                         , objConn As SqlClient.SqlConnection) As Boolean
        Dim daConditions As SqlClient.SqlDataAdapter
        Dim dsConditions As New DataSet

        daConditions = New SqlClient.SqlDataAdapter(strQuerry, objConn)
        If daConditions.Fill(dsConditions, "RESULT") > 0 Then
            cboInput.DataSource = dsConditions.Tables("RESULT")
            cboInput.DisplayMember = "Display"
            cboInput.ValueMember = "Value"
            'LoadCombo = cboInput
            dsConditions.Dispose()
            daConditions.Dispose()
        End If
        Return True
    End Function
    Public Function LoadDataGridView(ByRef dgInput As DataGridView, strQuerry As String _
                                     , objConn As SqlClient.SqlConnection) As Boolean
        Dim daConditions As SqlClient.SqlDataAdapter
        Dim dsConditions As New DataSet


        daConditions = New SqlClient.SqlDataAdapter(strQuerry, objConn)
        daConditions.SelectCommand.CommandTimeout = 128
        daConditions.Fill(dsConditions, "Result")
        dgInput.DataSource = dsConditions.Tables("Result")
        dgInput.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        dgInput.AutoResizeColumns()
        dsConditions.Dispose()
        daConditions.Dispose()
        Return True
    End Function
    Public Function GetDataTable(ByVal pStrCmd As String, Optional ByVal pConn As SqlClient.SqlConnection = Nothing) As DataTable
        Dim tblResults As New DataTable
        'Try
        If pConn Is Nothing Then
            Dim adapter As New SqlClient.SqlDataAdapter(pStrCmd, Conn)

            adapter.SelectCommand.CommandTimeout = 128
            adapter.Fill(tblResults)

        Else
            Dim adapter As New SqlClient.SqlDataAdapter(pStrCmd, pConn)
            adapter.SelectCommand.CommandTimeout = 128
            adapter.Fill(tblResults)
        End If
        'Catch ex As Exception
        '    MsgBox("SQL ERROR:" & pStrCmd & vbNewLine & ex.Message)
        'End Try

        Return tblResults
    End Function
    Public Sub CheckRightForALLForm(ByVal frm As Form)
        Dim Ctrl As Control
        myStaff.CurrObj = frm.Name
        If myStaff.URights = "" Then Exit Sub
        For i As Int16 = 0 To myStaff.URights.Split("|").Length - 1
            Ctrl = findControl(myStaff.URights.Split("|")(i), frm)
            Ctrl.Enabled = False
        Next
    End Sub
    Private Function findControl(ByVal pName As String, ByVal pCtrl As Control) As Control
        Dim ReturnControl As Control = Nothing
        For Each Ctrl_i As Control In pCtrl.Controls
            If Ctrl_i.Name.ToUpper = pName.ToUpper Then
                ReturnControl = Ctrl_i
                Exit For
            ElseIf Ctrl_i.Controls.Count > 0 Then
                ReturnControl = findControl(pName, Ctrl_i)
                If ReturnControl IsNot Nothing Then Exit For
            End If
        Next
        Return ReturnControl
    End Function

    Public Function GetDomainNameFromTRXCode(ByVal pTRXCode As String) As String
        If pTRXCode = "XX" Then
            Return "EDU"
        ElseIf pTRXCode = "TS" Then
            Return "TVS"
        Else
            Return "GSA"
        End If
    End Function
    Public Function CityAPTToCountry_Area_City(ByVal pReturnWhat As String, ByVal pInput As String) As String
        Dim KQ As String = ScalarToString("cityCode", pReturnWhat, " airport='" & pInput & "' OR CITY='" & pInput & "'")
        If KQ Is Nothing Then
            KQ = IIf(pReturnWhat = "Country", "??", "???")
        End If
        Return KQ
    End Function
    Public Sub LoadCmb_VAL(ByVal cmb As ComboBox, ByVal pQry As String _
                           , Optional Conx As SqlClient.SqlConnection = Nothing)
        If InStr(pQry.ToUpper, "ORDER BY") = 0 Then
            pQry = pQry & " order by DIS"
        End If
        cmb.DataSource = GetDataTable(pQry, Conx)
        cmb.ValueMember = "VAL"
        cmb.DisplayMember = "DIS"
    End Sub
    Public Sub LoadCmbAL(ByVal cmb As ComboBox)
        If MySession.Domain = "GSA" Then
            LoadCmb_MSC(cmb, myStaff.GSA)
        ElseIf MySession.Domain = "TVS" Then
            LoadCmb_MSC(cmb, myStaff.ALList)
        Else
            LoadCmb_MSC(cmb, "select 'XX' as VAL")
        End If
        MyAL.Domain = MySession.Domain
        MyAL.ALCode = ""
    End Sub
    Public Sub LoadCmb_MSC(ByVal cmb As ComboBox, ByVal pCat As String)
        Dim dTable As DataTable
        If pCat.Length < 16 Then
            dTable = GetDataTable("select VAL from MISC where status='OK' and CAT='" & pCat & "' and City='" & myStaff.City & "' order by val ")
        Else
            If InStr(pCat.ToUpper, "ORDER BY") = 0 Then pCat = pCat & " order by VAL"
            dTable = GetDataTable(pCat)
        End If
        cmb.Items.Clear()
        For i As Int32 = 0 To dTable.Rows.Count - 1
            cmb.Items.Add(dTable.Rows(i)("VAL"))
        Next
        If cmb.Items.Count > 0 Then cmb.Text = cmb.Items(0).ToString
    End Sub
    Public Function CheckTKTformat(ByVal vTKNO As String) As String
        Dim KQ As String = ""
        If vTKNO.Length <> 13 And vTKNO.Length <> 15 Then
            KQ = "Error. Invalid Ticket Number!  "
            Return KQ
        End If
        If (InStr(vTKNO, " ") > 0 And vTKNO.Length = 13) Or
            (InStr(vTKNO, " ") = 0 And vTKNO.Length = 15) Then
            KQ = "Error. Invalid Ticket Number!  "
            Return KQ
        End If
        If InStr(vTKNO, " ") = 0 And vTKNO.Length = 13 Then
            KQ = AddSpace2TKNO(vTKNO)
        ElseIf InStr(vTKNO, " ") > 0 And vTKNO.Length = 15 Then
            KQ = vTKNO
        End If
        If Not MyAL.ValidDocCode.Contains(KQ.Substring(0, 3)) Then
            KQ = "Error. Invalid Airline Document Code!  "
        End If
        Return KQ
    End Function

    Public Function GenInvNo_QD153(ByVal pRCP As String, ByVal pKyHieu As String) As String
        Dim KQ As String = "", strDK As String
        Dim strPrefix As String = pRCP.Substring(0, 2) + pKyHieu + pRCP.Substring(4, 2) + MySession.POSCode 'AL yy POS
        strDK = " left(invno,7)='" & strPrefix & "' "

        If Now() > CDate("01-aug-16") AndAlso myStaff.City = "HAN" AndAlso InStr("UA_RJ_TS", MySession.TRXCode) > 0 Then
            strDK = strDK & " AND FSTUPDATE>'01-AUG-16'  " ' THEM DO HAN danh lai so hd tu so 1 cho 3 hang nay. co the bo sau 1Jan17
        End If

        strDK = strDK & "  order by invno desc"
        KQ = ScalarToString("INV", "top 1 INVNO", strDK)
        If KQ <> "" Then
            KQ = strPrefix & Format(CInt(Strings.Right(KQ, 5)) + 1, "00000")
        Else
            KQ = strPrefix & "00001"
        End If
        Return KQ
    End Function
    Public Function CheckEditROE(ByVal pAL As String) As Boolean
        If myStaff.SupOf <> "" Then Return True
        If pubVarSRV = "O" Then
            Return True
        Else
            If MySession.TRXCode <> "TS" Then
                Return False
            Else
                Return Not MyAL.isTVA
            End If
        End If
        Return False
    End Function
    Public Function DefineDefauSF(ByVal pAL As String) As Decimal
        Return ScalarToDec("MISC", "top 1 VAL ", "cat ='MINSF' or CAT='MINSF" & pAL & "' order by CAT DESC")
    End Function
    Public Function ForEX_12(strCity As String, ByVal DOS As Date, ByVal pCurr As String, ByVal pType As String, ByVal pAL As String _
                             , Optional ByVal parQuay As String = "**") As clsROE
        Dim objROE As New clsROE
        Dim surCharge As Decimal
        Dim dTable As DataTable
        dTable = GetDataTable("select * from ForEx where Currency='" & pCurr & "' and City='" & strCity & "' and Status='OK' order by EffectDate DESC, recid desc ")
        For i As Int16 = 0 To dTable.Rows.Count - 1
            If dTable.Rows(i)("EffectDate") <= DOS And
                (dTable.Rows(i)("ApplyROEto") = "YY" Or InStr(dTable.Rows(i)("ApplyROEto"), pAL) > 0 _
                 Or pAL = "YY" Or InStr(dTable.Rows(i)("ApplySCto"), pAL) > 0) Then
                objROE.Amount = dTable.Rows(i)(pType)
                objROE.Id = dTable.Rows(i)("RecId")

                If pType = "RECID" Then Exit For
                surCharge = dTable.Rows(i)("SurCharge")
                If pType = "BBR" Then surCharge = -surCharge
                If (parQuay <> "**" And InStr(dTable.Rows(i)("ApplySCto"), parQuay) > 0) Or
                    InStr(dTable.Rows(i)("ApplySCto"), pAL) > 0 Then
                    objROE.Amount = objROE.Amount + surCharge
                End If
                Exit For
            End If
        Next
        Return objROE
    End Function
    Public Function GetTsRoeByDoi(ByVal dteDOI As Date, strCur As String) As Integer

        Return ScalarToInt("Forex", "TOP 1 BSR", "where Currency='" & strCur _
                    & "' and Status='OK' and ApplyRoeTo like '%TS%'" _
                    & " and EffectDate between '" & CreateFromDate(dteDOI) _
                    & "' and '" & CreateToDate(dteDOI) & "' and City='" & myStaff.City _
                    & "' order by EffectDate DESC")

    End Function
    '^_^20221104 mark by 7643 -b-
    'Public Function GetTsRoeMostUpdated(ByVal dteDOI As Date, strCur As String) As Integer  
    'Return ScalarToInt("Forex", "TOP 1 BSR", "where Currency='" & strCur _
    '                & "' and Status='OK' and ApplyRoeTo like '%TS%'" _
    '                & " and EffectDate <='" & Format(dteDOI, "dd MMM yy HH:mm") _
    '                & "' and City='" & myStaff.City _
    '                & "' order by EffectDate DESC")
    'End Function
    '^_^20221104 mark by 7643 -e-
    '^_^20221104 modi by 7643 -b-
    Public Function GetTsRoeMostUpdated(ByVal dteDOI As Date, strCur As String) As Decimal

        Return ScalarToDec("Forex", "TOP 1 BSR", "where Currency='" & strCur _
                    & "' and Status='OK' and ApplyRoeTo like '%TS%'" _
                    & " and EffectDate <='" & Format(dteDOI, "dd MMM yy HH:mm") _
                    & "' and City='" & myStaff.City _
                    & "' order by EffectDate DESC")

    End Function
    '^_^20221104 modi by 7643 -e-
    Public Function GetUsdGdsRoeIdByDoi(ByVal dteDOI As Date) As Integer

        Return ScalarToInt("Forex", "TOP 1 Recid", "where Currency='USD'" _
                    & " and Status='OK' and ApplyRoeTo like '%GDS'" _
                    & " and EffectDate between '" & CreateFromDate(dteDOI) _
                    & "' and '" & CreateToDate(dteDOI) & "' and City='" & myStaff.City _
                    & "' order by EffectDate DESC")

    End Function
    Public Function GetUsdGdsRoeByDoi(ByVal dteDOI As Date) As Integer

        Return ScalarToInt("Forex", "TOP 1 BSR", "where Currency='USD'" _
                    & " and Status='OK' and ApplyRoeTo like '%GDS'" _
                    & " and EffectDate between '" & CreateFromDate(dteDOI) _
                    & "' and '" & CreateToDate(dteDOI) & "' and City='" & myStaff.City _
                    & "' order by EffectDate DESC")

    End Function
    Public Function GetRoeIdByRateAndDoi(ByVal dteDOI As Date, ByVal strCur As String _
                                         , decRoe As Decimal, strCounter As String) As Integer

        Return ScalarToInt("Forex", "TOP 1 Recid", "where Currency='" & strCur _
                    & "' and Status='OK' and ApplyRoeTo like '%" & strCounter _
                    & "%' and BSR=" & decRoe _
                    & " and EffectDate<='" & CreateToDate(dteDOI) & "' and City='" & myStaff.City _
                    & "' order by EffectDate DESC")

    End Function
    Public Function getHotStr(ByVal vAL As String, ByVal vHotKey As String, ByVal vtxtBox As String) As String
        Return ScalarToString("MISC", "details", "cat+VAL='HOTKEY" & vAL & "' and val1='" & vtxtBox & "' and VAL2='" & vHotKey & "'")
    End Function

    Public Function XDHoaHongGSA(ByVal varAL As String, ByVal varProduct As String, ByVal pCurr As String) As Decimal
        Dim KQ As Decimal = 0
        Dim dTable As DataTable
        dTable = GetDataTable("select * from Charge_Comm where cat='COMM' and status='OK' and  AL='" & varAL &
        "' and currency='" & pCurr & "' and Type='" & varProduct & "' and ('" & Now.Date & "' between ValidFrom and ValidThru)")
        For i As Int16 = 0 To dTable.Rows.Count - 1
            KQ = dTable.Rows(i)("Amount")
            If dTable.Rows(i)("AmtType") = "PCT" Then
                KQ = KQ / 100
            End If
        Next
        XDHoaHongGSA = KQ
    End Function
    Private Sub TaoDataChoALRPT(ByVal ppAL As String, ByVal ppFrm As Date, ByVal ppThru As Date)
        Dim strDKDate As String, QryForFOPAmt As String, QryForFOPdocs As String
        Dim tblFOP As String = "##ztmpal_fop"
        Dim tblRCP As String = "##ztmpal_RCP"
        Dim tblTKT As String = "##ztmpal_TKT"
        Dim tblTrans As String = "##ztmpal_tkt_trans"

        On Error Resume Next
        cmd.CommandText = "drop table " & tblFOP
        cmd.ExecuteNonQuery()
        cmd.CommandText = "drop table " & tblRCP
        cmd.ExecuteNonQuery()
        cmd.CommandText = "drop table " & tblTKT
        cmd.ExecuteNonQuery()
        cmd.CommandText = "drop table " & tblTrans
        cmd.ExecuteNonQuery()
        On Error GoTo 0

        strDKDate = " SRV <>'A' and  DOI between '" & ppFrm & "' and '" & ppThru & " 23:59'"
        QryForFOPAmt = "(select sum(amount) from FOP  where status='OK' and "
        QryForFOPAmt = QryForFOPAmt & " t1.rcpid=FOP.rcpid and "
        QryForFOPdocs = "(select top 1 Document from FOP where status='OK' and "
        QryForFOPdocs = QryForFOPdocs & " t1.rcpid=FOP.rcpid and "

        cmd.CommandTimeout = 64
        cmd.CommandText = "select RCPNo, TKNO, SRV, FTKT, fare, ShownFare, itinerary, Currency, charge, CommVal, CommPCT, " &
            " NetToAL, tax, tax_d , RCPID, stockCtrl, qty, doi, doctype, faretype into " &
            tblTKT & " from TKT where statusAL='OK' and " & strDKDate &
            " and (srv <>'A' or (srv='A' and Fare+tax+charge >0 )) and al='" & ppAL &
            "' and TKNO not like '%TV%' and TKNO not like '%GRP%' and rcpid in (select recid from rcp where sbu='GSA')"

        cmd.ExecuteNonQuery()

        cmd.CommandText = "select  FOP, Currency, sum(Amount) as Amount into " & tblFOP & " from FOP " &
                " where fop in ('MCO','PTA','EXC','UCF') and status ='OK' and RCPID in  (select RCPID from " & tblTKT & " t " &
                " where  RECID in (Select top 1 recid from " & tblTKT & "  b where b.tkno= t.tkno and b.srv=t.SRV" &
                " and document not like 'GRP%' order by fstupdate desc)) group by FOP, Currency "

        cmd.ExecuteNonQuery()

        cmd.CommandText = "Select RCPID, TKNO, FTKT, SRV, Qty, DOI, fare, tax, charge, CommVal, nettoAL, itinerary, Currency, " &
            " Tax_D, (select sum(qty) from " & tblTKT & " t2 where  t2.rcpid=t1.rcpid) as TTLPax," & QryForFOPAmt & "FOP = 'MCO') as MCO, " &
            QryForFOPAmt & "FOP = 'PTA') as PTA, " & QryForFOPAmt & "FOP = 'EXC' and document not like 'GRP%') as EXC, " & QryForFOPAmt & "FOP = 'UCF') as UCF, " &
            QryForFOPdocs & " FOP = 'MCO') as MCO_doc, " & QryForFOPdocs & " FOP = 'PTA') as PTA_doc, " & QryForFOPdocs &
            " FOP = 'EXC' and document not like 'GRP%') as EXC_doc, " & QryForFOPdocs & " FOP = 'UCF') as UCF_doc, DocType, RCPNO, Faretype, ShownFare,StockCtrl " &
            " into " & tblTrans & " from " & tblTKT & " t1"
        cmd.ExecuteNonQuery()

        cmd.CommandText = "select RecID, RCPNo, SRV, TTLDue, Status, CustID, Currency, ROE, VNDPCT into " & tblRCP & " from RCP " &
            " where al='" & ppAL & "' and status+city='OK" & MySession.City & "' and recID in (select RCPID from " & tblTKT & ")"

        On Error Resume Next
        cmd.ExecuteNonQuery()
        On Error GoTo 0
    End Sub
    Private Sub TaoDataChoDailyRPT(ByVal ppAL As String, ByVal ppFrm As Date, ByVal ppThru As Date, ByVal pBoPhan As String)
        Dim strDKDate As String, StrTKTFieldList As String, StrTKTdk As String, strSQL As String
        If MySession.Domain = "TVS" Then ppAL = "TS"
        Dim KTFOP = "##ztmpkt_fop_" & ppAL.ToLower & "_" & MySession.Counter, KTRCP = "##ztmpkt_rcp_" & ppAL.ToLower & "_" & MySession.Counter
        Dim KTTKT = "##ztmpkt_tkt_" & ppAL.ToLower & "_" & MySession.Counter
        KTFOP = KTFOP.Replace("-", "")
        KTRCP = KTRCP.Replace("-", "")
        KTTKT = KTTKT.Replace("-", "")
        On Error Resume Next
        cmd.CommandText = "drop table " & KTFOP
        cmd.ExecuteNonQuery()
        cmd.CommandText = "drop table " & KTRCP
        cmd.ExecuteNonQuery()
        cmd.CommandText = "drop table " & KTTKT
        cmd.ExecuteNonQuery()
        On Error GoTo 0
        strDKDate = " fstUpdate between '" & Format(ppFrm, "dd-MMM-yy") & "' and '" & Format(ppThru, "dd-MMM-yy") & " 23:59'"
        StrTKTFieldList = " Select RCPNo, SRV, TKNO, FTKT, Qty, RCPID, Fare, tax, Charge, ChargeTV, CommVAL, "
        StrTKTFieldList = StrTKTFieldList & " NetToAL, AgtDisctPCT, AgtDisctVAL, Itinerary, Promocode, DocType, DOF,Dependent,DOI "
        StrTKTdk = " RCPID in (Select RecID from " & KTRCP & ")"

        strSQL = "Select RecID, RCPNo, SRV, CustShortName, TTLDue, Discount, Charge, Status, CustID, Currency, " &
            " ROE, RPTNo, City, Counter, cast(Stock as varchar(4)) as AL , Location,VendorId,Vendor into " _
            & KTRCP _
            & " from RCP where " & strDKDate _
            & " and status <>'NA' and al='" & ppAL & "'" &
            " and City='" & MySession.City & "'"
        Select Case myStaff.Counter
            Case "CWT", "HAN", "ALL"
            Case "GSA"
                If ppAL <> "PG" Then
                    strSQL = strSQL & " and location ='" & myStaff.Location & "'"
                End If
            Case Else
                strSQL = strSQL & " and location ='" & myStaff.Location & "'"
        End Select

        If pBoPhan = "C" Then
            strSQL = strSQL & " and Counter='" & MySession.Counter & "'"
        End If

        strSQL = strSQL & "  order by RCPNO"
        cmd.CommandText = strSQL

        cmd.ExecuteNonQuery()

        cmd.CommandText = "Select  RCPID, RCPNo, FOP, Currency, Amount, ROE, 'NEWS' as PmtType, Document,Rmk  into " & KTFOP &
                " from FOP where FOP <>'RND' and left(rmk,8)<>'BO_CLEAR' and status in ('OK','QQ') and RCPID in " &
                "(select RecID from " & KTRCP & " where status <> 'XX')"
        'Append2TextFile(cmd.CommandText)
        cmd.ExecuteNonQuery()

        cmd.CommandText = " insert into " & KTFOP & " Select  RCPID, RCPNo, FOP, Currency, Amount, ROE, 'EDIT', Document,Rmk " &
                " from FOP where FOP <>'RND' and status in('OK','QQ') and " & strDKDate & " and RCPID NOT in " &
                " (select RecID from " & KTRCP & " where status <> 'XX') " &
                " and left(rcpno,2)='" & ppAL & "' and left(rmk,8)<>'BO_CLEAR' " &
                " and RCPID NOT in (select recid from rcp where status='NA') "
        If MySession.Domain = "TVS" And InStr("CN", pBoPhan) > 0 Then
            cmd.CommandText = cmd.CommandText & " and rcpid in (select recid from rcp where counter='" & MySession.Counter & "')"
        End If
        'Append2TextFile(cmd.CommandText)
        cmd.ExecuteNonQuery()

        'Lay cac ve SA-R bt co statusAL = OK
        strSQL = StrTKTFieldList & " into " & KTTKT & " from TKT"
        strSQL = strSQL & " Where SRV in ('S','A','R') and StatusAL='OK' and " & StrTKTdk
        If MySession.Domain = "GSA" Then
            strSQL = strSQL & " and al='" & ppAL & "'"
        End If

        cmd.CommandText = strSQL
        'Append2TextFile(cmd.CommandText)
        cmd.ExecuteNonQuery()

        'lay cac ve R cua void deu, tuc la SRV=R va statusNoibo=OK  va phai thay so RCP tuong ung co TKT.SRV=V statusAL =OK
        strSQL = "Insert into " & KTTKT & StrTKTFieldList & " from TKT Where SRV='R' and Status<>'XX'"
        If MySession.Domain = "GSA" Then
            strSQL = strSQL & " and AL='" & ppAL & "' "
        End If
        strSQL = strSQL & " and " & StrTKTdk & " and RCPID in (Select RCPID from TKT"
        strSQL = strSQL & " Where SRV='V' and statusAL='OK' and " & StrTKTdk
        If MySession.Domain = "GSA" Then
            strSQL = strSQL & " and al='" & ppAL & "'"
        End If
        strSQL = strSQL & " )"
        cmd.CommandText = strSQL
        'Append2TextFile(cmd.CommandText)
        cmd.ExecuteNonQuery()

        ' Lay cac ve ban ma sau do bi void deu, khi void deu statusal=XX nen no ko vao khi lay lan 1
        'DK la SRV=S + StatusNoiBo=OK va phai thay 1 so ve tuong ung bi SRV=V va statusAL=OK

        strSQL = "Insert into " & KTTKT & StrTKTFieldList & " from TKT Where SRV='S' and Status<>'XX'"
        If MySession.Domain = "GSA" Then
            strSQL = strSQL & " and al='" & ppAL & "'"
        End If
        strSQL = strSQL & " and " & StrTKTdk & " and TKNO in (Select TKNO from TKT Where SRV='V' and statusAL='OK'"
        strSQL = strSQL & " and " & strDKDate   'han che tranh lay ve bi dup trong qua khu
        If MySession.Domain = "GSA" Then
            strSQL = strSQL & " and al='" & ppAL & "'"
        End If
        strSQL = strSQL & " )"

        cmd.CommandText = strSQL
        'Append2TextFile(cmd.CommandText)
        cmd.ExecuteNonQuery()

        ' voi KT nen chi Lay cac ve Void Xin, tuc la ko nam trong loai co RCP tuong ung chua ve R 
        strSQL = "Insert into " & KTTKT & StrTKTFieldList & " from TKT Where SRV='V' and "
        strSQL = strSQL & " StatusAL='OK' and " & StrTKTdk
        If MySession.Domain = "GSA" Then
            strSQL = strSQL & " and al='" & ppAL & "'"
        End If
        strSQL = strSQL & " and RCPID not in (Select RCPID from TKT"
        strSQL = strSQL & " Where SRV='R' and status<>'XX' and " & StrTKTdk + ")"
        cmd.CommandText = strSQL
        'Append2TextFile(cmd.CommandText)
        cmd.ExecuteNonQuery()

        'thay cac ve void Deu trong ngay thanh SRV=C de khau tru tien trong bao cao ngay  
        strSQL = "update " & KTTKT & " set SRV='C' where SRV='R' and "
        strSQL = strSQL & " TKNO in (select tkno from TKT where srv='V' and statusal='OK' "
        If MySession.Domain = "GSA" Then
            strSQL = strSQL & " and al='" & ppAL & "'"
        End If
        strSQL = strSQL & " ) and "
        strSQL = strSQL & " TKNO in (select tkno from " & KTTKT & " where srv='S') "
        cmd.CommandText = strSQL
        'Append2TextFile(cmd.CommandText)
        cmd.ExecuteNonQuery()

        'thay cac RCP void Deu trong ngay thanh SRV=C de khau tru tien trong bao cao ngay  
        cmd.CommandText = "update " & KTRCP & " set SRV='C' where SRV='R' and RECID in (select RCPID from " & KTTKT & " where srv='C')"
        'Append2TextFile(cmd.CommandText)
        cmd.ExecuteNonQuery()

        cmd.CommandText = "update " & KTRCP & " set AL='1A' where RecID in (select RCPID from " & KTTKT &
            " where substring(tkno,5,4) in (select VAL from LIB.DBO.misc where status='OK' and cat='BSPSTOCK'))"
        'Append2TextFile(cmd.CommandText)
        cmd.ExecuteNonQuery()

        cmd.CommandText = "update " & KTRCP & " set AL=AL+'1A' where RecID in (select RCPID from " & KTTKT &
            " where left(tkno,3) in (select SecondCode from Airline where secondCode <>'' and City='" & myStaff.City & "'))"
        'Append2TextFile(cmd.CommandText)
        cmd.ExecuteNonQuery()

    End Sub
    '^_^20220801 mark by 7643 -b-
    'Public Sub InHoaDon(ByVal strPath As String, ByVal parFileName As String, ByVal parViewPrint As String _
    '                    , ByVal parRCPNO As String, ByVal parFrm As Date, ByVal parTo As Date _
    '                    , ByVal ParNewValue As Decimal, ByVal pAL As String, ByVal pDomain As String _
    '                    , Optional ByVal ParLoaiHD As String = "", Optional AnyInt1 As Integer = 0 _
    '                    , Optional ByVal RMK As String = "", Optional strCity As String = "")
    '^_^20220801 mark by 7643 -e-
    '^_^20220801 modi by 7643 -b-
    Public Function InHoaDon(ByVal strPath As String, ByVal parFileName As String, ByVal parViewPrint As String _
                        , ByVal parRCPNO As String, ByVal parFrm As Date, ByVal parTo As Date _
                        , ByVal ParNewValue As Decimal, ByVal pAL As String, ByVal pDomain As String _
                        , Optional ByVal ParLoaiHD As String = "", Optional AnyInt1 As Integer = 0 _
                        , Optional ByVal RMK As String = "", Optional strCity As String = "") As Boolean
        '^_^20220801 modi by 7643 -e-

        If PrinterSettings.InstalledPrinters.Count = 0 Then
            MsgBox("You must install printer to use this function!")
            'Exit Sub  '^_^20220801 mark by 7643
            Return False  '^_^20220801 modi by 7643
        End If

        Dim AppXls As Microsoft.Office.Interop.Excel.Application, WkBook As Microsoft.Office.Interop.Excel.Workbook, WkSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim varAL As String
        If parFileName = "" Then
            MsgBox("Please Select Invoice Type", MsgBoxStyle.Critical, msgTitle)
            'Exit Sub  '^_^20220801 mark by 7643
            Return False  '^_^20220801 modi by 7643
        End If

        If parFileName.Substring(0, 3) <> "R12" Then parFileName = "R12_" & parFileName
        varAL = Dir(strPath & "\" & parFileName)
        If varAL = "" Then
            MsgBox("Template File Not Found. Plz Check " & parFileName _
                   , MsgBoxStyle.Critical, msgTitle)
            'Exit Sub  '^_^20220801 mark by 7643
            Return False  '^_^20220801 modi by 7643
        End If
        On Error Resume Next
        AppXls = CreateObject("Excel.Application")
        If AppXls.Version.StartsWith("14") Or AppXls.Version.StartsWith("15") Then
            Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Office\14.0\Excel\Security\FileValidation", "EnableOnLoad", 0, Microsoft.Win32.RegistryValueKind.DWord)
        End If

        On Error GoTo 0
        If parRCPNO.Length > 2 Then
            varAL = parRCPNO.Substring(0, 2)
        Else
            varAL = pAL
        End If
        If InStr(parFileName.ToUpper, "SR_") > 0 Or InStr(parFileName.ToUpper, "AR_") > 0 Or
            InStr(parFileName.ToUpper, "DISCREPANCY") > 0 Then
            If InStr(parFileName.ToUpper, "DISCREPANCY") > 0 Then
                cmd.CommandText = "Exec DiscrepancyRPT '" & varAL & "','" & Format(parFrm, "dd MMM yyyy") & "','" &
                    Format(parTo, "dd MMM yyyy") & "','" & MySession.Domain & "'"
                cmd.ExecuteNonQuery()
            ElseIf InStr(parFileName.ToUpper, "AR_") > 0 Then
                TaoDataChoDailyRPT(varAL, parFrm, parTo, ParLoaiHD)
            ElseIf InStr(parFileName.ToUpper, "SR_") > 0 Then
                TaoDataChoALRPT(varAL, parFrm, parTo)
            End If
        End If
        On Error GoTo CloseXLS
        AppXls.Visible = True
        WkBook = AppXls.Workbooks.Open(strPath & "\" & parFileName, , , , "aibiet", , , , , True)
        WkSheet = WkBook.Worksheets("Para")
        WkSheet.Cells.Range("B1").Value = parRCPNO
        WkSheet.Cells.Range("B2").Value = "'" & varAL
        WkSheet.Cells.Range("B3").Value = myStaff.SICode
        WkSheet.Cells.Range("B4").Value = parFrm
        WkSheet.Cells.Range("B5").Value = parTo
        WkSheet.Cells.Range("B6").Value = ParNewValue
        WkSheet.Cells.Range("B7").Value = pDomain
        WkSheet.Cells.Range("B8").Value = ParLoaiHD
        WkSheet.Cells.Range("B9").Value = parViewPrint
        WkSheet.Cells.Range("B10").Value = AnyInt1
        WkSheet.Cells.Range("B11").Value = RMK
        WkSheet.Cells.Range("c3").Value = strCity

        Select Case parFileName
            Case "R12_CC_CT_TKT Listing CWT.xlt", "R12_CC_CT_TKT Listing CWT VAT7.xlt", "R12_CC_CT_TKT Listing CWT VAT8.xlt"
                WkSheet.ComboBox1.Text = ScalarToString("CustomerList", "CustShortName" _
                                    , "Status<>'XX' and RecId=" & ParNewValue _
                                    & " and City='" & myStaff.City & "'")
        End Select

        WkSheet.Cells.Range("B15").Value = "YES"

        If InStr("PFVO", parViewPrint) = 0 Then GoTo CloseXLS
        WkSheet = WkBook.Worksheets("RPT")
        Select Case parViewPrint
            Case "V"
                AppXls.Visible = True
                If InStr(parFileName.ToUpper, "RECEIPT") Or InStr(parFileName.ToUpper, "VAT") Then
                    WkSheet.Cells.Range("D6").Value = "P R E V I E W"
                ElseIf InStr(parFileName.ToUpper, "SR_") + InStr(parFileName.ToUpper, "AR_") > 0 Then
                    WkSheet.Cells.Range("G2").Value = "P R E V I E W.   N O T  F O R  R E P O R T I N G  P U R P O S E"
                End If

                WkSheet.PrintPreview(vbNo)

            Case "P"
                AppXls.Visible = True
                WkSheet.PrintPreview(vbNo)
            Case "O"
                AppXls.Visible = False
                WkSheet.PrintOut()
            Case "F"
                AppXls.Visible = True
                Dim strSavedFile As String = "d:\" & parFileName.Split(".")(0) & ".xlsx"
                WkBook.SaveAs(strSavedFile, Excel.XlFileFormat.xlWorkbookDefault,, "aibiet")
                MsgBox("Exported file " & strSavedFile)
        End Select
        'If InStr("V", parViewPrint) > 0 Then
        '    AppXls.Visible = True
        '    If InStr(parFileName.ToUpper, "RECEIPT") Or InStr(parFileName.ToUpper, "VAT") Then
        '        WkSheet.Cells.Range("D6").Value = "P R E V I E W"
        '    ElseIf InStr(parFileName.ToUpper, "SR_") + InStr(parFileName.ToUpper, "AR_") > 0 Then
        '        WkSheet.Cells.Range("G2").Value = "P R E V I E W.   N O T  F O R  R E P O R T I N G  P U R P O S E"
        '    End If
        '    WkSheet.PrintPreview(vbNo)
        'ElseIf parViewPrint = "P" Then
        '    AppXls.Visible = True
        '    WkSheet.PrintPreview(vbNo)
        'ElseIf parViewPrint = "O" Then
        '    AppXls.Visible = False
        '    WkSheet.PrintOut()

        'End If
CloseXLS:
        WkBook.Close(SaveChanges:=False)
        AppXls.Quit()
        AppXls = Nothing
        On Error GoTo 0

        Return True  '^_^20220801 add by 7643
    End Function
    Public Function CalcCharge(ByVal pCharge As String, ByVal varWho As String, ByVal pTRXCurr As String, ByVal pROE As Decimal, ByVal pDOS As Date, ByVal pAL As String) As Decimal
        Dim KQ As Decimal, c As Decimal, Curri As String, tmpROE As Decimal
        For i As Int16 = 0 To UBound(pCharge.Split("|"))
            If pCharge.Split("|")(i).Substring(0, 2) = varWho Then
                Curri = pCharge.Split("|")(i).Split(":")(1).Substring(0, 3)
                c = CDec(pCharge.Split("|")(i).Split(":")(1).Substring(3))
                If pTRXCurr <> "VND" And Curri = "VND" Then
                    c = c / pROE
                ElseIf pTRXCurr = "VND" And Curri <> "VND" Then
                    tmpROE = ForEX_12(myStaff.City, pDOS, Curri, "BSR", pAL).Amount
                    c = c * tmpROE
                End If
                KQ = KQ + c
            End If
        Next
        Return KQ
    End Function
    Public Function DefineNextTKNO(ByVal parThisTKT As String, ByVal IsTKTless As Boolean) As String
        Dim lstTKT As Double, KQ As String
        If Not parThisTKT.Contains("Z") Or Left(parThisTKT, 3) = "GRP" Then
            lstTKT = parThisTKT.Substring(9, 6)
            lstTKT = CDbl(lstTKT) + 1
            KQ = parThisTKT.Substring(0, 9) & Format(lstTKT, "000000")
        Else
            lstTKT = parThisTKT.Substring(parThisTKT.Length - 3, 2)
            lstTKT = CDbl(lstTKT) + 1
            KQ = parThisTKT.Substring(0, parThisTKT.Length - 2) & Format(lstTKT, "00")
        End If
        DefineNextTKNO = KQ
    End Function
    Public Function UpdateTblINVHistory(ByVal ParInvID As Integer, ByVal parInvNo As String, pInt As Int16) As String
        Return "update INV set printCopy=printCopy +" & pInt & " where RecID=" & ParInvID & " ; " &
            UpdateLogFile("INV", "INPPrint", parInvNo, ParInvID, "", "", "", "", "", "")
    End Function
    Public Function UpdateLogFile(ByVal pTbl As String, ByVal pAction As String, ByVal pF1 As String _
                                  , ByVal pF2 As String, ByVal pF3 As String, ByVal pF4 As String _
                                  , ByVal pF5 As String, Optional ByVal pF6 As String = "" _
                                  , Optional ByVal pF7 As String = "", Optional ByVal pF8 As String = "" _
                                  , Optional ByVal pF9 As String = "", Optional ByVal pF10 As String = "" _
                                  , Optional ByVal pF11 As String = "", Optional ByVal pF12 As String = "" _
                                  , Optional ByVal pF13 As String = "") As String
        Dim KQ As String = "insert ActionLog (TableName, doWhat, F1, F2, F3, F4, F5, F6, F7, F8, f9,f10, F11,F12,F13" _
        & ",city, ActionBy) Values ('"
        KQ = KQ & pTbl & "','" & pAction & "',N'" & pF1 & "',N'" & pF2 & "','" & pF3 & "','" & pF4 & "','" & pF5 & "','" & pF6 & "',N'" &
                pF7 & "',N'" & pF8 & "','" & pF9 & "','" & pF10 & "','" & pF11 & "','" & pF12 & "','" & pF13 _
                & "','" & MySession.City & "','" & myStaff.SICode & "')"
        Return KQ
    End Function
    Public Function ConvertDomainAccess2SqlList(strDomainAccess As String) As String
        Return "('" & Replace(strDomainAccess, "_", "','") & "')"
    End Function
    Public Function CreateFromDate(dteInput As Date) As String
        Return Format(dteInput, "dd MMM yy 00:00")
    End Function
    Public Function CreateToDate(dteInput As Date) As String
        Return Format(dteInput, "dd MMM yy 23:59")
    End Function
    Public Function ExecuteNonQuerry(strQuerry As String, objConn As SqlClient.SqlConnection) As Boolean
        Dim objCmd As SqlClient.SqlCommand = objConn.CreateCommand
        If strQuerry.Contains("AOP_SGN_TVTR") AndAlso objCmd.CommandTimeout < 256 Then
            objCmd.CommandTimeout = 512
        End If
        If strQuerry.Contains("AOP_SGN_TVTR") Then
            Threading.Thread.Sleep(1000)
        End If
        objCmd.CommandText = strQuerry
        If objConn.State = ConnectionState.Closed Then
            objConn.Open()
        End If
        Try
            objCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Dim strLog As String = vbNewLine & "ERROR|" & myStaff.UID & "|" & Now & vbNewLine & strQuerry & vbNewLine & ex.Message
            If ex.Message.Contains("time out") Then

            End If
            Append2TextFile(strLog)
            Return False
        End Try

    End Function
    Public Function UpdateListOfQuerries(lstQuerries As List(Of String), objConn As SqlClient.SqlConnection _
                                         , Optional blnGetLastInsertedRecId As Boolean = False _
                                         , Optional ByRef intLastInsertedRecId As Integer = 0) As Boolean

        Dim i As Integer
        Dim strQuerry As String = String.Empty
        If objConn.State = ConnectionState.Closed Then
            objConn.Open()
        End If
        Dim trcSql As SqlClient.SqlTransaction = objConn.BeginTransaction()
        Dim objCmd As SqlClient.SqlCommand = objConn.CreateCommand
        objCmd.Transaction = trcSql

        Try
            For i = 0 To lstQuerries.Count - 1
                strQuerry = lstQuerries(i)
                objCmd.CommandText = strQuerry
                If Not String.IsNullOrEmpty(strQuerry) Then
                    objCmd.ExecuteNonQuery()
                    If blnGetLastInsertedRecId AndAlso UCase(strQuerry).StartsWith("INSERT") Then
                        objCmd.CommandText = "select SCOPE_IDENTITY()"
                        intLastInsertedRecId = objCmd.ExecuteScalar
                    End If
                End If
            Next
            trcSql.Commit()
            Return True
        Catch ex As Exception

            trcSql.Rollback()
            Append2TextFile(vbNewLine & "ERROR|" & myStaff.SICode & "|" & Now & vbNewLine & strQuerry & vbNewLine & ex.Message)
            Return False
        End Try
    End Function
    Public Function PromtAndLog(ByVal strText As String, Optional strLogfile As String = "") As Boolean
        MsgBox(strText)
        Append2TextFile(strText, strLogfile)
        Return True
    End Function
    Public Function Append2TextFile(ByVal strText As String, Optional strLogfile As String = "") As Boolean
        If strLogfile = "" Then
            strLogfile = My.Application.Info.DirectoryPath & "\" _
                                            & Format(Today, "yyMMdd") & pstrPrg & ".txt"
        End If

        Dim objLogFile As New System.IO.StreamWriter(strLogfile, True)
        objLogFile.WriteLine(strText)
        objLogFile.Close()
        objLogFile = Nothing
        Return True
    End Function
    Public Function LogSqlError(strError As String, strFunction As String) As Boolean
        Append2TextFile("SQL:" & strFunction & vbNewLine & strError)
        Return True
    End Function
    Public Function ClearLogFile(ByVal intNbrOfDays As Int16) As Boolean
        ' make a reference to a directory
        Dim objDir As New IO.DirectoryInfo(Directory.GetCurrentDirectory)
        Dim objFileInfo As IO.FileInfo
        Dim intMinDay As Integer
        intMinDay = Format(DateAdd(DateInterval.Day, -intNbrOfDays, Now), "yyMMdd")

        For Each objFileInfo In objDir.GetFiles
            If IsNumeric(Mid(objFileInfo.Name, 1, 6)) _
                AndAlso Mid(objFileInfo.Name, 1, 6) < intMinDay _
                AndAlso Mid(objFileInfo.Name, 7) = pstrPrg & ".txt" Then
                objFileInfo.Delete()
            End If
        Next

        Return True
    End Function
    Public Function ChangeGridViewSelectedColumn(ByRef dgInput As DataGridView, blnSelected As Boolean) As Boolean
        For Each objdgrCust As DataGridViewRow In dgInput.Rows
            With objdgrCust
                .Cells("Selected").Value = blnSelected
            End With
        Next
    End Function
    Public Function CheckFormatTextBox(ByRef txtInput As System.Windows.Forms.TextBox _
                                        , Optional ByVal blnNumeric As Boolean = False _
                                        , Optional ByVal intMinLength As Int16 = 0 _
                                         , Optional ByVal intMaxLength As Int16 = 0) As Boolean
        Dim strName As String
        If txtInput.Tag = "" Then
            strName = Mid(txtInput.Name, 4)
        Else
            strName = txtInput.Tag
        End If
        If txtInput.Text = "" Then
            MsgBox("Invalid value for " & strName)
            txtInput.Focus()
            Return False
        End If
        If intMaxLength > 0 AndAlso txtInput.Text.Length > intMaxLength Then
            MsgBox("Invalid MaxLength for " & strName)
            txtInput.Focus()
            Return False
        End If
        If intMinLength > 0 AndAlso txtInput.Text.Length < intMinLength Then
            MsgBox("Invalid value for " & strName)
            txtInput.Focus()
            Return False
        End If
        If blnNumeric AndAlso Not IsNumeric(txtInput.Text) Then
            MsgBox("Invalid value for " & strName)
            txtInput.Focus()
            Return False
        End If
        Return True
    End Function
    Public Function CheckMustNotEmptyText(objCtrl As Control) As Boolean
        If objCtrl.Text = "" Then
            MsgBox("Invalid Value for " & Mid(objCtrl.Name, 1, 3))
            Return False
        Else
            Return True
        End If
    End Function
    Public Function CheckFormatComboBox(ByRef cboInput As System.Windows.Forms.ComboBox _
                                        , Optional ByVal blnNumeric As Boolean = False _
                                        , Optional ByVal intMinLength As Int16 = 0 _
                                        , Optional ByVal intMaxLength As Int16 = 0) As Boolean
        Dim strName As String
        If cboInput.Tag = "" Then
            strName = Mid(cboInput.Name, 4)
        Else
            strName = cboInput.Tag
        End If

        If (intMaxLength > 0 AndAlso cboInput.Text.Length > intMaxLength) _
            Or (intMinLength > 0 AndAlso cboInput.Text.Length < intMinLength) _
            Or (blnNumeric AndAlso Not IsNumeric(cboInput.Text)) Then
            GoTo Quit
        End If
        Return True
Quit:
        MsgBox("Invalid value for " & strName)
        cboInput.Focus()

    End Function
    Public Function GetCardTypeByCardNbr(strCardNbr As String) As String
        Select Case Mid(strCardNbr, 1, 2)
            Case "40", "41", "42", "43", "44", "45", "46", "47", "48", "49"
                Return "VI"
            Case "34", "37", "35"
                Return "AX"
            Case "36"
                Return "DC"
            Case "51", "52", "53", "54", "55"
                Return "CA"
            Case Else
                Return "XX"
                'Throw New SystemException("Unable to find CreditCard Type for ")
        End Select
    End Function

    Public Function GetCDRs(ByVal objSqlConx As SqlClient.SqlConnection, ByVal strCmc As String _
                            , Optional ByVal blnBeforeRas As Boolean = False _
                            , Optional blnIncludeConditionalCDR As Boolean = False) As Collection

        Dim colCDRs As New Collection
        Dim strQry As String
        Dim drResult As SqlClient.SqlDataReader
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = objSqlConx

        strQry = "select * from cwt.dbo.GO_CDRs"
        strQry = strQry & " where CMC='" & strCmc & "' and status='OK'"
        If blnBeforeRas Then
            strQry = strQry & " and CollectionMethod<>'RAS'"
        End If
        If Not blnIncludeConditionalCDR Then
            strQry = strQry & " and Mandatory='M'"
        End If
        strQry = strQry & " order by cdrnbr"
        cmdSql.CommandText = strQry
        drResult = cmdSql.ExecuteReader
        If Not drResult Is Nothing Then
            Do While drResult.Read
                Dim objCdr As New clsCwtCdr
                objCdr.Nbr = drResult("CdrNbr")
                objCdr.CdrName = drResult("CdrName")
                objCdr.CharType = drResult("CharType")
                objCdr.MinLength = drResult("MinLength")
                objCdr.MaxLength = drResult("MaxLength")
                objCdr.Mandatory = drResult("Mandatory")
                colCDRs.Add(objCdr, objCdr.Nbr)
            Loop
        End If
        drResult.Close()
        Return colCDRs
    End Function
    Public Function IsAlphaOnly(ByRef strText As String) As Boolean
        Dim rgAlpha As New Regex("\d")
        If rgAlpha.IsMatch(strText) Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function GenerateBankPaymentBatchNbr(ByVal objSqlConx As SqlClient.SqlConnection _
                                                , strBankName As String) As String
        Dim strBatchNbr As String
        Dim strQuerry As String = "Select top 1 SCBNo from UNC_Payments where substring(SCBNo,1,3)='" & strBankName _
                                  & "' and substring(SCBNo,4,6)=CONVERT(varchar,getdate(),12)" _
                                  & " order by substring(SCBNo,10,2) desc"
        Dim cmdSql As New SqlClient.SqlCommand
        cmdSql.Connection = objSqlConx
        cmdSql.CommandText = strQuerry
        strBatchNbr = cmdSql.ExecuteScalar

        If strBatchNbr = "" Then
            strBatchNbr = strBankName & Format(Now, "yyMMdd") & "01"
        Else
            strBatchNbr = Mid(strBatchNbr, 1, strBankName.Length) & (Mid(strBatchNbr, strBankName.Length + 1) + 1)
        End If
        Return strBatchNbr

    End Function
    Public Function ConvertToLetter(iCol As Integer) As String
        Dim iAlpha As Integer
        Dim iRemainder As Integer
        Dim strResult As String = String.Empty
        iAlpha = Int(iCol / 27)
        iRemainder = iCol - (iAlpha * 26)
        If iAlpha > 0 Then
            strResult = Chr(iAlpha + 64)
        End If
        If iRemainder > 0 Then
            strResult = strResult & Chr(iRemainder + 64)
        End If
        Return strResult
    End Function
    Public Function DefineDomRtg(strDomCities As String, strRtg As String)
        Dim strResult As String = "DOM"
        Dim arrCities As String() = strRtg.Split(" ")
        For Each strCity As String In arrCities
            If strCity.Length = 3 Then
                If Not strDomCities.Contains(strCity) Then
                    Return ""
                End If
            End If
        Next
        Return strResult
    End Function
    Public Function DefineDomIntRtg(strDomCities As String, strRtg As String)
        Dim strResult As String = "DOM"
        Dim arrCities As String() = strRtg.Split(" ")
        For Each strCity As String In arrCities
            If strCity.Length = 3 Then
                If Not strDomCities.Contains(strCity) Then
                    Return "INT"
                End If
            End If
        Next
        Return strResult
    End Function
    Public Function GetTaxAmtFromTaxDetails(strTaxCode As String, strTaxDetails As String) As Decimal
        Dim decResult As Decimal = 0

        If strTaxDetails <> "" Then
            Dim arrTaxes() As String = strTaxDetails.Split("|")
            Dim i As Integer
            For i = 0 To arrTaxes.Length - 1
                If Mid(arrTaxes(i), 1, 2) = strTaxCode Then
                    decResult = decResult + Mid(arrTaxes(i), 3)
                End If
            Next
        End If
        Return decResult
    End Function

    Public Function CheckData(ByVal colRequiredData As Collection _
                                , ByVal colAvailableData As Collection _
                                , ByVal intCustId As Integer, dteDOI As Date) As Collection
        Dim colResult As New Collection
        Dim i As Integer

        For i = 1 To colRequiredData.Count
            Dim objRequiredData As clsRequiredData = colRequiredData(i)

            With objRequiredData
                .ClearOldCheck()
                Dim objAvaiData As New clsAvailableData

                If Not colAvailableData.Contains(.DataCode) Then
                    If objRequiredData.DefaultValue = "" Then
                        objRequiredData.ErrMsg = "MISSING"
                    Else
                        AddRequiredData(colAvailableData, .DataCode, .DefaultValue)
                    End If
                Else
                    objAvaiData = colAvailableData(.DataCode)
                    If IsSpecialValue(intCustId, .DataCode, objAvaiData.DataValue) Then
                        'pass object nay
                    ElseIf IsNormalValue(intCustId, .DataCode, objAvaiData.DataValue) Then
                        'pass object nay
                    ElseIf .CheckValues Then
                        objRequiredData.ErrMsg = "INVALID VALUE"

                    ElseIf objAvaiData.DataValue.Length < .MinLength Then
                        objRequiredData.ErrMsg = "INVALID MIN LENGTH"
                    ElseIf objAvaiData.DataValue.Length > .MaxLength Then
                        objRequiredData.ErrMsg = "INVALID MAX LENGTH"
                    ElseIf .CharType = "NUMERIC" AndAlso Not IsNumeric(objAvaiData.DataValue) Then
                        objRequiredData.ErrMsg = "VALUE IS NOT NUMERIC"
                    ElseIf .CharType = "ALPHA" AndAlso Not AlphaOnly(objAvaiData.DataValue) Then
                        objRequiredData.ErrMsg = "VALUE IS NOT ALPHA ONLY"
                    End If
                End If
                If .ErrMsg <> "" Then
                    .AvailableValue = objAvaiData.DataValue
                    colResult.Add(objRequiredData)
                End If

            End With

        Next
        Return colResult
    End Function
    Public Function AddRequiredData(ByVal colResult As Collection _
                    , ByRef strDataCode As String, ByRef strValue As String) As Boolean
        Try
            Dim objAvaiData As New clsAvailableData
            objAvaiData.DataCode = strDataCode
            objAvaiData.DataValue = strValue
            colResult.Add(objAvaiData, objAvaiData.DataCode)
        Catch ex As Exception
            If ex.Message.Contains("Duplicate") Then
                MsgBox("Duplicate RM*" & strDataCode)
                Return False
            End If
        End Try

        Return True
    End Function
    Public Function IsSpecialValue(ByVal intCustId As Integer, ByVal strDataCode As String _
                                    , ByVal strValue As String) As Boolean
        Dim intResult As Integer
        intResult = ScalarToInt("cwt.dbo.GO_RequiredDataValues", "top 1 RecId", " DataType='SPECIAL'" _
                    & " and Value='" & strValue & "' and CustId=" & intCustId)
        If intResult > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsNormalValue(ByVal intCustId As Integer, ByVal strDataCode As String _
                                    , ByVal strValue As String) As Boolean
        Dim intResult As Integer
        intResult = ScalarToInt("cwt.dbo.GO_RequiredDataValues", "top 1 RecId", " DataType='NORMAL'" _
                    & " and Value='" & strValue & "' and CustId=" & intCustId)
        If intResult > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function AlphaOnly(ByVal strText As String) As Boolean
        Dim rgCheck As New Regex("\d")

        If rgCheck.IsMatch(strText) Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function GetDataRequirement(ByVal intCustId As Integer _
                                        , ByVal strMandatoryType As String _
                                        , Optional ByVal blnPosOnly As Boolean = False _
                                        , Optional strAppyTo As String = "") As Collection
        Dim colResult As New Collection
        Dim strQuerry As String
        Dim tblResult As New DataTable

        strQuerry = "Select * from GO_RequiredData" _
                        & " where Status='OK' and CustId =" & intCustId
        If strMandatoryType <> "" Then
            strQuerry = strQuerry & " and Mandatory='" & strMandatoryType & "'"
        End If

        If blnPosOnly Then
            strQuerry = strQuerry & " and CollectionMethod='AGTINPUT'"
        End If

        If strAppyTo <> "" Then
            strQuerry = strQuerry & " and ApplyTo in ('ALL','" & strAppyTo & "')"
        End If

        For Each objRow As DataRow In tblResult.Rows
            Dim objRqData As New clsRequiredData
            With objRqData
                .DataCode = objRow("DataCode")
                .NameByCustomer = objRow("NameByCustomer")
                .MinLength = objRow("MinLength")
                .MaxLength = objRow("MaxLength")
                .Mandatory = objRow("Mandatory")
                .ConditionOfUse = objRow("ConditionOfUse")
                .CollectionMethod = objRow("CollectionMethod")
                .DefaultValue = objRow("DefaultValue")
                .CheckValues = objRow("CheckValues")
                .AllowSpecialValues = objRow("AllowSpecialValues")
                .CharType = objRow("CharType")
                colResult.Add(objRqData, objRqData.DataCode)
            End With
        Next

        Return colResult

    End Function
    Public Function CreateSms4UNC(strRecId As String, strBankName As String, blnUseBatchNbr As Boolean) As Boolean
        Dim strSql As String
        If Conn_Web.State = ConnectionState.Closed Then Conn_Web.Open()
        Dim arrMobiles() As String = {"0909250946", "0908900131"}
        Dim tblUnc As DataTable
        If blnUseBatchNbr Then
            tblUnc = GetDataTable("Select Curr,Sum(Amount) as Amount from UNC_Payments where SCBNo='" _
                                  & strRecId & "' group by Curr", Conn)
        Else
            tblUnc = GetDataTable("Select * from UNC_Payments where RecId=" & strRecId, Conn)
        End If

        Dim strMsg As String = "TransViet msg: Account update:" & strBankName & " " & tblUnc.Rows(0)("Curr") _
                                & " " & Format(tblUnc.Rows(0)("Amount"), "#,###.00")

        For Each strMobile As String In arrMobiles
            strSql = "Insert SMSLog (CustID, SMSText, Location, MobileNbr) values (-1,'" _
                & strMsg & "','SGN','" & strMobile & "')"
            ExecuteNonQuerry(strSql, Conn_Web)
        Next
        Conn_Web.Close()
    End Function

    Public Function CleanAccountNbr(strAccountNbr As String) As String
        Dim rgClean As New Regex("\s|[:]|[-]")
        strAccountNbr = strAccountNbr
        Return rgClean.Replace(strAccountNbr, "")
    End Function

    Public Function ViewXlsFileOnly(strFileName) As Boolean
        Dim objExcel As New Excel.Application
        Dim objSourceWbk As Excel.Workbook
        Dim objTargeWbk As Excel.Workbook
        Dim objActiveSheet As Excel.Worksheet
        objTargeWbk = objExcel.Workbooks.Open(My.Application.Info.DirectoryPath _
                                             & "\ViewOnlyTemplate.xlsm", , False)
        objSourceWbk = objExcel.Workbooks.Open(strFileName)
        objActiveSheet = objSourceWbk.ActiveSheet

        For Each objWsh As Excel.Worksheet In objSourceWbk.Sheets
            objWsh.Copy(objTargeWbk.Sheets("TempXXX"))
        Next
        objTargeWbk.Sheets(objActiveSheet.Name).activate()
        objSourceWbk.Close(False)
        objExcel.Visible = True
        Return True
    End Function
    Public Function ReserveBspInv(decAmt As Decimal, strDescription As String) As Boolean
        Dim InvNo As String, INVID As Integer
        Dim fstUpdate As Date = Now()
        Dim intCountInv As Integer

        If Now.Month = 1 And Now.Day < 10 Then fstUpdate = DateSerial(Now.Year - 1, 12, 31)
        Dim objAl As New objAL
        objAl.Domain = "TVS"
        objAl.ALCode = "TS"

        intCountInv = ScalarToDec("INV", " count(RecId)", "Status='OK' and RcpId=-55 and FstUpdate between '" _
                       & CreateFromDate(fstUpdate) & "' and '" & CreateToDate(fstUpdate) _
                       & "' and substring(InvNo,1,2)='" & objAl.ALCode & "'") > 0
        If intCountInv > 1 Then
            Return True
        End If
        InvNo = GenInvNo_QD153(objAl.ALCode & fstUpdate.Year, objAl.VAT_KyHieu)
        Try
            Dim strSaveType As String = "Create"
            INVID = Insert_INV("E", InvNo, objAl.ALCode, -55, fstUpdate)

            cmd.CommandText = UpdateLogFile("INV", "Reserve INV for Tax Accounting", INVID, strDescription, strSaveType, "", "")
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MsgBox("Unable Creating Invoice", MsgBoxStyle.Critical, msgTitle)
            Return False
        End Try
    End Function

    Public Function ContainSpecialChar4Citi(strText As String) As Boolean
        Dim rgCheck As New Regex("^[a-zA-Z0-9?().,+/ -:]*$")

        If Not rgCheck.IsMatch(strText) Or strText.StartsWith("/") _
            Or strText.StartsWith(":") Or strText.StartsWith("-") Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function ConvertItinerary4FullName(strOldItinerary As String) As String
        Dim rgCar As New Regex("\s\w\w\s")
        Dim arrCities As String()
        Dim i As Integer

        strOldItinerary = rgCar.Replace(strOldItinerary, "-")
        strOldItinerary = Replace(strOldItinerary, " // ", "--")
        arrCities = strOldItinerary.Split("-")
        For i = 0 To arrCities.Length - 1
            Select Case arrCities(i)
                Case "SGN"
                    arrCities(i) = "HO CHI MINH"
                Case "HAN"
                    arrCities(i) = "HA NOI"
                Case "DAD"
                    arrCities(i) = "DA NANG"
                Case Else
                    arrCities(i) = ScalarToString("CityCode", "CityName" _
                                               , "Airport='" & arrCities(i) & "'")
            End Select
        Next
        Return Replace(Join(arrCities, "-"), "--", "//")
    End Function
    Public Function IsDupLccPnr(strRloc As String, strPaxName As String, dteDOI As Date) As Boolean
        Dim intRecId As Integer
        intRecId = ScalarToInt("LCC_PNRs", "RecId", "RLOC='" & strRloc _
                                & "' and PaxName='" & strPaxName.Trim _
                                & "' and DOI='" & Format(dteDOI, "dd MMM yy HH:mm") & "'")
        If intRecId = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function InsExist(strProvider As String, dteFrom As Date, dteTo As Date) As Boolean
        Dim intRecId As Integer
        intRecId = ScalarToInt("InsuranceRaw", "top 1 RecId", "DOI between'" & CreateFromDate(dteFrom) _
                                & "' and '" & CreateToDate(dteTo) _
                                & "' and Provider='" & strProvider & "'")
        If intRecId = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function FilterByPaxName(strPaxName As String) As String
        Dim arrNameParts As String() = strPaxName.Split(" ")
        Dim strResult As String = " len(PaxName)=" & strPaxName.Length
        For Each strNamePart As String In arrNameParts
            strResult = strResult & " and PaxName  like '%" & strNamePart & "%' "
        Next
        Return strResult
    End Function
    Public Function MatchRasRtgFormat(strRouting As String) As Boolean
        Dim arrRtgBreaks() As String = strRouting.Split(" ")
        Dim i As Integer
        If arrRtgBreaks.Length < 3 Then
            Return False
        Else
            For i = 0 To arrRtgBreaks.Length - 1
                Select Case i Mod 2
                    Case 0
                        If arrRtgBreaks(i).Length <> 3 Then
                            Return False
                        End If
                    Case 1
                        If arrRtgBreaks(i).Length <> 2 Then
                            Return False
                        End If
                End Select
            Next
        End If
        Return True
    End Function
    Public Function AddFromDatesMonthly(ByRef cboDate As ComboBox, intBackwardMonth As Integer, intForwardMonth As Integer) As Boolean
        Dim i As Integer
        Dim dteFrom As Date

        For i = intForwardMonth To 1 Step -1
            dteFrom = DateAdd(DateInterval.Month, i, Now)
            dteFrom = DateSerial(Year(dteFrom), Month(dteFrom), 1)
            cboDate.Items.Add(Format(dteFrom, "dd MMM yy"))
        Next

        For i = 0 To intBackwardMonth
            dteFrom = DateAdd(DateInterval.Month, -i, Now)
            dteFrom = DateSerial(Year(dteFrom), Month(dteFrom), 1)
            cboDate.Items.Add(Format(dteFrom, "dd MMM yy"))
        Next


        Return True
    End Function
    Public Function AddToDatesQuartely(ByRef cboDate As ComboBox _
                                       , intCountForward As Integer) As Boolean
        Dim i As Integer
        Dim strBaseDate As String = ""

        cboDate.Items.Clear()

        Select Case Now.Month
            Case 1, 2, 3
                strBaseDate = Format(DateSerial(Now.Year, 4, 1), "dd MMM yy")
            Case 4, 5, 6
                strBaseDate = Format(DateSerial(Now.Year, 7, 1), "dd MMM yy")
            Case 7, 8, 9
                strBaseDate = Format(DateSerial(Now.Year, 10, 1), "dd MMM yy")
            Case 10, 11, 12
                strBaseDate = Format(DateSerial(Now.Year + 1, 1, 1), "dd MMM yy")
        End Select
        cboDate.Items.Add(CDate(strBaseDate).AddDays(-1))
        For i = 1 To intCountForward
            cboDate.Items.Add(Format(CDate(strBaseDate).AddMonths(3 * i).AddDays(-1), "dd MMM yy"))
        Next

        Return True
    End Function
    Public Function CheckFormatTkno(strTkno As String) As Boolean
        Dim rgTkno As New Regex("\d{3}\s\d{4}\s\d{6}")
        If rgTkno.IsMatch(strTkno) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetRequiredDataValueByDataCode(strDataCode As String _
        , strRequiredData As String) As String
        Dim arrValues As String()
        Dim i As Integer

        If strRequiredData <> "" Then
            arrValues = Split(strRequiredData, "|")
            For i = 0 To arrValues.Length - 1
                If arrValues(i).StartsWith(strDataCode & "/") Then
                    Return Mid(arrValues(i), Len(strDataCode) + 2)
                End If
            Next
        End If
        Return ""
    End Function
    Public Function LogReport(strPrg As String, strReportName As String, strUser As String, strCity As String)
        Dim strQuerry As String = "insert into MISC (CAT,VAL,VAL1,FstUser,City) values ('DoReport','" _
                                & strPrg & "','" & strReportName & "','" & myStaff.SICode _
                                & "','" & myStaff.City & "')"

        Return ExecuteNonQuerry(strQuerry, Conn)
    End Function
    Public Function ContainSpecialChars(ByVal strCheckedText As String _
                                        , Optional blnAcceptampersand As Boolean = False) As Boolean
        'xem co chua cac ky tu dac biet khong duoc update vao PNR hay khong
        Dim i As Integer
        For i = 0 To strCheckedText.Length - 1
            Select Case Asc(strCheckedText.Chars(i))
                Case Is < 31, 63, Is > 125
                    'MsgBox("special character" & strCheckedText.Chars(i) & " asci" & Asc(strCheckedText.Chars(i)))
                    Return True
                Case 38
                    If Not blnAcceptampersand Then
                        Return True
                    End If
            End Select
        Next
        Return False
    End Function

    '^_^20221027 add by 7643 -b-
    Public Function ContainSpecialAndUnicodeChars(ByVal strCheckedText As String) As Boolean
        Dim i As Integer
        For i = 0 To strCheckedText.Length - 1
            '^_^20221115 mark by 7643 -b-
            'If (Asc(strCheckedText.Chars(i)) <> 32 And Asc(strCheckedText.Chars(i)) <> 38 And Asc(strCheckedText.Chars(i)) <> 40 And Asc(strCheckedText.Chars(i)) <> 41 And
            '    Asc(strCheckedText.Chars(i)) <> 46) AndAlso Not (Char.IsLetterOrDigit(strCheckedText.Chars(i)) And Asc(strCheckedText.Chars(i)) < 123) Then Return True
            '^_^20221115 mark by 7643 -e-
            '^_^20221115 modi by 7643 -b-
            If Not (Asc(strCheckedText.Chars(i)) = 32 Or Asc(strCheckedText.Chars(i)) = 33 Or
                    Asc(strCheckedText.Chars(i)) = 35 Or (Asc(strCheckedText.Chars(i)) >= 38 And Asc(strCheckedText.Chars(i)) <= 46) Or
                    (Asc(strCheckedText.Chars(i)) >= 48 And Asc(strCheckedText.Chars(i)) <= 57) Or Asc(strCheckedText.Chars(i)) = 59 Or
                    (Asc(strCheckedText.Chars(i)) >= 63 And Asc(strCheckedText.Chars(i)) <= 90) Or Asc(strCheckedText.Chars(i)) = 95 Or
                    (Asc(strCheckedText.Chars(i)) >= 97 And Asc(strCheckedText.Chars(i)) <= 122) Or Asc(strCheckedText.Chars(i)) = 126) Then Return True
            '^_^20221115 modi by 7643 -e-
        Next
        Return False
    End Function
    '^_^20221027 add by 7643 -e-

    Public Function TranslateNonAirSvc(strServices As String) As String

        strServices = Replace(strServices, "Accommodations", "Tiền phòng khách sạn")
        strServices = Replace(strServices, "Transfer", "Tiền thuê xe")
        strServices = Replace(strServices, "Miscellaneous", "Tiền dịch vụ bổ sung")
        strServices = Replace(strServices, "TransViet SVC Fee", "Phí dịch vụ")
        strServices = Replace(strServices, "Bank Fee", "Phí chuyển tiền")
        strServices = Replace(strServices, "Merchant Fee", "Phí cà thẻ")

        Return strServices

    End Function
    Public Function TienBangChu(ByVal sSoTien As String) As String
        Dim DonVi() As String = {"", "nghìn ", "triệu ", "tỷ ", "nghìn ", "triệu "}
        Dim so As String
        Dim chuoi As String = ""
        Dim temp As String
        Dim id As Byte

        If sSoTien = 0 Then
            Return ("Không")
        End If
        sSoTien = sSoTien.Replace(",", "")
        If sSoTien.EndsWith(".00") Then
            sSoTien = Mid(sSoTien, 1, sSoTien.Length - 3)
        End If
        Do While (Not sSoTien.Equals(""))
            If sSoTien.Length <> 0 Then
                so = getNum(sSoTien)
                sSoTien = Strings.Left(sSoTien, sSoTien.Length - so.Length)
                temp = setNum(so)
                so = temp
                If Not so.Equals("") Then
                    temp = temp + DonVi(id)
                    chuoi = temp + chuoi
                End If
                id = id + 1
            End If
        Loop
        temp = UCase(Strings.Left(chuoi, 1))

        Return temp & Strings.Right(chuoi, Len(chuoi) - 1)
    End Function
    Private Function getNum(ByVal sSoTien As String) As String
        Dim so As String

        If sSoTien.Length >= 3 Then
            so = Strings.Right(sSoTien, 3)
        Else
            so = Strings.Right(sSoTien, sSoTien.Length)
        End If
        Return so
    End Function
    Private Function setNum(ByVal sSoTien As String) As String
        Dim chuoi As String = ""
        Dim flag0 As Boolean
        Dim flag1 As Boolean
        Dim temp As String

        temp = sSoTien
        Dim kyso() As String = {"không ", "một ", "hai ", "ba ", "bốn ", "năm ", "sáu ", "bảy ", "tám ", "chín "}
        'Xet hang tram
        If sSoTien.Length = 3 Then
            If Not (Strings.Left(sSoTien, 1) = 0 And Strings.Left(Strings.Right(sSoTien, 2), 1) = 0 And Strings.Right(sSoTien, 1) = 0) Then
                chuoi = kyso(Strings.Left(sSoTien, 1)) + "trăm "
            End If
            sSoTien = Strings.Right(sSoTien, 2)
        End If
        'Xet hang chuc
        If sSoTien.Length = 2 Then
            If Strings.Left(sSoTien, 1) = 0 Then
                If Strings.Right(sSoTien, 1) <> 0 Then
                    chuoi = chuoi + "linh "
                End If
                flag0 = True
            Else
                If Strings.Left(sSoTien, 1) = 1 Then
                    chuoi = chuoi + "mười "
                Else
                    chuoi = chuoi + kyso(Strings.Left(sSoTien, 1)) + "mươi "
                    flag1 = True
                End If
            End If
            sSoTien = Strings.Right(sSoTien, 1)
        End If
        'Xet hang don vi
        If Strings.Right(sSoTien, 1) <> 0 Then
            If Strings.Left(sSoTien, 1) = 5 And Not flag0 Then
                If temp.Length = 1 Then
                    chuoi = chuoi + "năm "
                Else
                    chuoi = chuoi + "lăm "
                End If
            Else
                If Strings.Left(sSoTien, 1) = 1 And Not (Not flag1 Or flag0) And chuoi <> "" Then
                    chuoi = chuoi + "mốt "
                Else
                    chuoi = chuoi + kyso(Strings.Left(sSoTien, 1)) + ""
                End If
            End If
        Else
        End If
        Return chuoi
    End Function
    Public Function ReformatVietnameseNumber(decValue As Decimal) As String
        Dim arrBreaks As String() = decValue.ToString.Split(".")
        Dim strUnits As String = arrBreaks(0)
        Dim strDecimals As String = String.Empty
        'Dim i As Integer
        Dim strResult As String = ""
        If arrBreaks.Length > 1 Then
            strDecimals = arrBreaks(1)
        End If

        Do While strUnits.Length > 3
            strResult = "." & Strings.Right(strUnits, 3) & strResult
            strUnits = Mid(strUnits, 1, strUnits.Length - 3)
        Loop
        strResult = strUnits & strResult
        If strDecimals <> "" Then
            strResult = strResult & "," & strDecimals
        End If
        Return strResult
    End Function
    Public Function AddFutureYear(ByVal FromDate As Date, ByVal CheckedDate As String) As Date
        ' them nam cho ngay dang DDMMM bang cach so sanh voi ngay chuan
        ' ngay moi trong khoang truoc ngay chuan 3 ngay va sau ngay chuan 361 ngay

        Dim bytCheckedMonth As Byte, bytCheckedDay As String
        Select Case Len(CheckedDate)
            Case 5
                If Not IsNumeric(Left(CheckedDate, 2)) Then GoTo ErrHandler
                If InStr("JAN,FEB,MAR,APR,MAY,JUN,JUL,AUG,SEP,OCT,NOV,DEC", UCase(Right(CheckedDate, 3))) = 0 Then
                    GoTo ErrHandler
                End If
            Case Else
                GoTo ErrHandler
        End Select
        '    If Not IsDate(FromDate) And Len(FromDate) = 7 Then
        '        FromDate = ToVbDate(FromDate)
        '    End If
        bytCheckedDay = Left(CheckedDate, 2)
        Select Case UCase(Right(CheckedDate, 3))
            Case "JAN"
                bytCheckedMonth = 1
            Case "FEB"
                bytCheckedMonth = 2
            Case "MAR"
                bytCheckedMonth = 3
            Case "APR"
                bytCheckedMonth = 4
            Case "MAY"
                bytCheckedMonth = 5
            Case "JUN"
                bytCheckedMonth = 6
            Case "JUL"
                bytCheckedMonth = 7
            Case "AUG"
                bytCheckedMonth = 8
            Case "SEP"
                bytCheckedMonth = 9
            Case "OCT"
                bytCheckedMonth = 10
            Case "NOV"
                bytCheckedMonth = 11
            Case "DEC"
                bytCheckedMonth = 12
        End Select
        If Month(FromDate) < bytCheckedMonth Then
            AddFutureYear = DateSerial(Year(FromDate), bytCheckedMonth, bytCheckedDay)
        End If
        If Month(FromDate) > bytCheckedMonth Then
            AddFutureYear = DateSerial(Year(FromDate) + 1, bytCheckedMonth, bytCheckedDay)
        End If
        If Month(FromDate) = bytCheckedMonth Then
            Select Case bytCheckedDay - DatePart(DateInterval.Day, FromDate)
                Case Is < -3
                    AddFutureYear = DateSerial(Year(FromDate) + 1, bytCheckedMonth, bytCheckedDay)
                Case Else
                    AddFutureYear = DateSerial(Year(FromDate), bytCheckedMonth, bytCheckedDay)
            End Select
        End If

        Exit Function
ErrHandler:
        MsgBox("Invalid Date. Unable to add year for:" & CheckedDate)
        On Error GoTo 0
    End Function
    Public Function AddEqualConditionCombo(ByRef strQuerry As String, ByRef cboCondition As ComboBox _
                                , Optional ByVal strColumnName As String = "" _
                                , Optional ByVal blnUnicode As Boolean = False _
                                , Optional strPrefix As String = "and") As Boolean
        If cboCondition.Text = "" Then
            Return True
        End If
        If strColumnName = "" Then
            strQuerry = strQuerry & " " & strPrefix & " " & Mid(cboCondition.Name, 4) _
                & "=" & IIf(blnUnicode, "N", "") & "'" & cboCondition.Text & "'"
        Else
            strQuerry = strQuerry & " " & strPrefix & " " & strColumnName & "=" _
                & IIf(blnUnicode, "N", "") & "'" & cboCondition.Text & "'"
        End If

        Return True
    End Function

    Public Function AddEqualConditionText(ByRef strQuerry As String _
                    , ByRef txtCondition As System.Windows.Forms.TextBox _
                    , Optional strPrefix As String = "and") As Boolean
        If txtCondition.Text = "" Then
            Return True
        End If
        strQuerry = strQuerry & " " & strPrefix & " " & Mid(txtCondition.Name, 4) & "='" & txtCondition.Text & "'"
        Return True
    End Function
    Public Function AddEqualConditionCheck(ByRef strQuerry As String _
                    , ByRef chkCondition As System.Windows.Forms.CheckBox _
                    , Optional strPrefix As String = "and") As Boolean
        If chkCondition.CheckState = CheckState.Indeterminate Then
            Exit Function
        End If
        strQuerry = strQuerry & " " & strPrefix & " " & Mid(chkCondition.Name, 4) & "='" & chkCondition.CheckState & "'"
        AddEqualConditionCheck = True
    End Function
    Public Function AddLikeConditionText(ByRef strQuerry As String _
                    , ByRef txtCondition As System.Windows.Forms.TextBox _
                    , Optional ByVal strColumnName As String = "" _
                    , Optional strPrefix As String = "and", Optional blnUnicode As Boolean = False) As Boolean
        Dim strLike As String
        If blnUnicode Then
            strLike = " like N'%"
        Else
            strLike = " like '%"
        End If

        If txtCondition.Text = "" Then
            Return False
        End If
        If strColumnName = "" Then
            strQuerry = strQuerry & " " & strPrefix & " " & Mid(txtCondition.Name, 4) _
                & strLike & txtCondition.Text & "%'"
        Else
            strQuerry = strQuerry & " " & strPrefix & " " & strColumnName & strLike _
                & txtCondition.Text & "%'"
        End If

        Return True
    End Function
    Public Function AddLikeConditionString(ByRef strQuerry As String _
                    , strColumnName As String, strLike As String) As Boolean
        Dim rg As New Regex("\s{2,}")
        Dim arrBreaks As String() = rg.Replace(strLike, " ").Split(" ")
        Dim strFilter As String
        Dim i As Integer

        For i = 0 To arrBreaks.Length - 1
            If arrBreaks(i) <> "" Then
                arrBreaks(i) = strColumnName & "  like N'%" & arrBreaks(i) & "%'"
            End If
        Next
        strQuerry = strQuerry & " and (" & Join(arrBreaks, " or ") & ")"

        Return True
    End Function
    Public Function DeleteGridViewRow(ByRef dbInput As DataGridView, ByVal strQuerry As String _
                                      , objSql As SqlClient.SqlConnection _
                                      , Optional blnCancel As Boolean = False) As Boolean
        Dim strMessage As String
        Dim i As Integer

        If blnCancel Then
            strMessage = "Do you want to cancel the following record?" & vbCrLf
        Else
            strMessage = "Do you want to delete the following record?" & vbCrLf
        End If

        With dbInput
            For i = 0 To dbInput.Columns.Count - 1
                If .Columns.Item(i).Visible Then
                    strMessage = strMessage & .Columns(i).HeaderText & ": " _
                                    & .Rows(0).Cells.Item(i).Value & vbCrLf
                End If
            Next
        End With

        If MsgBox(strMessage, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok _
            AndAlso ExecuteNonQuerry(strQuerry, objSql) Then
            Return True
        Else
            Return False
        End If

    End Function
    'Public Function TableRow2Msg(ByRef objRow As DataRow, strMessage As String) As Boolean
    '    Dim i As Integer

    '    For i = 0 To objRow.Table.Columns.Count
    '        strMessage = strMessage & objRow.Table.Columns(i).HeaderText & ": " _
    '                                & objRows.Item(i).Value & vbCrLf
    '    Next

    '    If MsgBox(strMessage, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then _            
    '        Return True
    '    Else
    '        Return False
    '    End If

    'End Function
    Public Function CheckFormatPlanCode4Abbott(strPlanCode As String) As Boolean
        If strPlanCode = "" Then
            Return False
        Else
            Dim arrBreaks As String() = strPlanCode.Split("-")
            If arrBreaks.Length <> 4 Then
                Return False
            ElseIf arrBreaks(2).Length <> 5 Then
                Return False
            ElseIf Not IsNumeric(Mid(arrBreaks(2), 4, 2)) Then
                Return False
            ElseIf Not IsNumeric(arrBreaks(3)) Then
                Return False
            End If
            Select Case arrBreaks(0).Length
                Case 1, 3, 4
                Case Else
                    Return False
            End Select
        End If
        Return True
    End Function
    Public Function CheckPlanCode4Abbott(strPlanCode As String, intEditedTourId As Integer) As Boolean
        If Not CheckFormatPlanCode4Abbott(strPlanCode) Then
            MsgBox("Invalid format Plan Code for Abbott!")
            Return False
        Else
            Dim strOldTcode As String
            If intEditedTourId = 0 Then
                strOldTcode = ScalarToString("Dutoan_TouR", "top 1 Tcode", "Status<>'XX' and RefNo='" _
                                                       & strPlanCode & "'")
            Else
                strOldTcode = ScalarToString("Dutoan_Tour", "top 1 Tcode", "Status<>'XX' and RefNo='" _
                                                       & strPlanCode & "' and Recid<>" & intEditedTourId)
            End If

            If strOldTcode <> "" Then
                MsgBox("Duplicate Plan Code with Tcode " & strOldTcode)
                Return False
            End If
        End If
        Return True
    End Function
    Public Function CheckEp3Code4Novartis(strEp3Code As String, intEditedTourId As Integer) As Boolean
        strEp3Code = strEp3Code.ToUpper.Trim
        If strEp3Code = "NA" Then Return True

        If Not strEp3Code.StartsWith("VN") Then
            MsgBox("EP3 code must be started by VN")
            Return False
        ElseIf strEp3Code.Length <> 12 Then
            MsgBox("EP3 code length must be 12")
            Return False
        ElseIf Not IsNumeric(Mid(strEp3Code, 3)) Then
            MsgBox("EP3 code format is wrong")
            Return False
        End If

        Dim strOldTcode As String
            If intEditedTourId = 0 Then
            strOldTcode = ScalarToString("Dutoan_TouR", "top 1 Tcode", "Status<>'XX' and RefNo='" _
                                                       & strEp3Code & "'")
        Else
            strOldTcode = ScalarToString("Dutoan_Tour", "top 1 Tcode", "Status<>'XX' and RefNo='" _
                                                       & strEp3Code & "' and Recid<>" & intEditedTourId)
        End If

        If strOldTcode <> "" Then
            MsgBox("Duplicate ep3 Code with Tcode " & strOldTcode)
            Return False
        End If

        Return True
    End Function
    Public Function FormatNumberWithDot(decInput As Decimal) As String
        Return Strings.Replace(Format(decInput, "#,##0"), ",", ".")
    End Function
    Public Function FormatRasTkno(strTkno As String) As String
        Select Case strTkno.Length
            Case 13
                If strTkno.StartsWith("Z") Then
                    Return Mid(strTkno, 1, 3) & " " & Mid(strTkno, 4, 6) & " " & Mid(strTkno, 10)
                Else
                    Return Mid(strTkno, 1, 3) & " " & Mid(strTkno, 4, 4) & " " & Mid(strTkno, 8)
                End If

            Case Else
                Return strTkno
        End Select
    End Function

    Public Function GetVendorBalance(intVendorId As Integer, strCur As String, strLastCutOff As String, strTvc As String) As Decimal

        Return ScalarToDec("VendorBalance", "Sum (Amount)", "Vendorid=" & intVendorId & " And Status='OK'" _
                & " and TrxDate>='" & strLastCutOff & "' and Cur='" & strCur & "' and Tvc='" & strTvc & "'")
    End Function
    Public Function GetLastCutOffDate(intVendorId As Integer, strCur As String) As String
        Return ScalarToString("VendorBalance", "top 1 CONVERT(VARCHAR, [TrxDate], 106)", " where VendorId=" _
                       & intVendorId & " And Status='OK' and FOP='COF' and Cur='" _
                       & strCur & "' order by TrxDate Desc")
    End Function
    Public Function Split_ChargeTV(pChargeD As String, pCurr As String) As Decimal
        Dim Amt As Decimal = 0
        For i As Int16 = 0 To pChargeD.Split("|").Length - 1
            If pChargeD.Split("|")(i).Substring(0, 2) = "TV" Then
                If pChargeD.Split("|")(i).Contains(pCurr) Then
                    Amt = Amt + CDec(pChargeD.Split("|")(i).Split(":")(1).Substring(3))
                End If
            End If
        Next
        Return Amt
    End Function
    Public Function DefineVN_CRS_Type(pTKNO As String) As String
        If ScalarToInt("lib.dbo.MISC", "count(*)", "Status='OK' and cat='BSPSTOCK' and val='" _
                       & pTKNO.Substring(4, 4) & "'") <> 0 Then Return "BSP"
        Return "NonBSP"
    End Function
    Public Function DefindDomInt4Tkt() As Boolean
        Dim tblTkt As System.Data.DataTable
        tblTkt = GetDataTable("Select t.Tkid,t.Itinerary,t.DocType" _
                             & " from tkt t left join ReportData r1 on t.RecId=r1.Tkid" _
                             & " where t.Doi>='10 Jan 20' and t.Qty<>0" _
                             & " and t.Status<>'xx' and t.DomInt=''", Conn)
        For Each objRow As DataRow In tblTkt.Rows
            If objRow("DocType") = "AHC" Then

            End If
        Next
        Return True
    End Function
    Public Function GetRecIDsFromDataGridView(ByRef dgrRecords As DataGridView, Optional strSelectedColumn As String = "") As List(Of String)
        Dim lstResults As New List(Of String)
        For Each objRow As DataGridViewRow In dgrRecords.Rows
            If strSelectedColumn = "" Then
                lstResults.Add(objRow.Cells("RecId").Value)
            ElseIf objRow.Cells(strSelectedColumn).Value Then
                lstResults.Add(objRow.Cells("RecId").Value)
            End If
        Next
        Return lstResults
    End Function
    Public Function IsExchanged(ByRef dgrRecords As DataGridView, Optional strSelectedColumn As String = "") As Boolean
        Dim lstResults As New List(Of String)
        For Each objRow As DataGridViewRow In dgrRecords.Rows
            If objRow.Cells(strSelectedColumn).Value <> "" Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function CheckFormatEmails(strEmails As String, blnAllowEmpty As Boolean) As Boolean
        If blnAllowEmpty AndAlso strEmails = "" Then
            Return True
        Else
            Dim arrBreaks As String() = Split(strEmails, ";")
            For Each strEmail As String In arrBreaks
                If Split(strEmail, "@").Length <> 2 Then
                    Return False
                ElseIf Not Split(strEmail, "@")(1).Contains(".") Then
                    Return False
                End If
            Next
        End If
        Return True
    End Function
    Public Function ExtractDateInTcode(strRawDate As String) As String
        strRawDate = Mid(strRawDate, 1, strRawDate.Length - 2)
        If strRawDate.StartsWith("9") Then
            strRawDate = (Mid(strRawDate, 1, 1) + 2010) & Mid(strRawDate, 2)
        Else
            strRawDate = (Mid(strRawDate, 1, 2) + 2010) & Mid(strRawDate, 3)
        End If
        Select Case Mid(strRawDate, 5, 1)
            Case 1, 2, 3, 4, 5, 6, 7, 8, 9
                strRawDate = Mid(strRawDate, 1, 4) & "0" & Mid(strRawDate, 5)
            Case "A"
                strRawDate = Mid(strRawDate, 1, 4) & "10" & Mid(strRawDate, 6)
            Case "B"
                strRawDate = Mid(strRawDate, 1, 4) & "11" & Mid(strRawDate, 6)
            Case "C"
                strRawDate = Mid(strRawDate, 1, 4) & "12" & Mid(strRawDate, 6)
        End Select
        Dim rgRemoveNonNumeric As New Regex("\D")
        strRawDate = rgRemoveNonNumeric.Replace(strRawDate, "")
        If strRawDate.Length = 7 Then
            strRawDate = Mid(strRawDate, 1, 6) & "0" & Mid(strRawDate, 7)
        End If
        Return Mid(strRawDate, 1, 4) & "-" & Mid(strRawDate, 5, 2) & "-" & Mid(strRawDate, 7, 2)
    End Function
    Public Function GetCustomerAopId(strCustShortName As String) As String
        'Chi dung cho 1 so customer dac biet cua TV
        Select Case strCustShortName
            Case "IB_SGN"
                Return "80000126-1578716238"
            Case "MICE_DAD"
                Return "80000125-1578716204"
            Case "MICE_SGN"
                Return "80000124-1578716162"
            Case "TOURDESK_DAD"
                Return "80000006-1578038251"
            Case "TOURDESK_SGN"
                Return "80000004-1578017737"
            Case Else
                Return ""
        End Select
    End Function
    Public Function GetDueDate4AopNonBsp(dteDOI As Date) As String
        Dim dteDueDate As Date
        Select Case dteDOI.Day
            Case < 8
                dteDueDate = DateSerial(dteDOI.Year, dteDOI.Month, 9)
            Case < 16
                dteDueDate = DateSerial(dteDOI.Year, dteDOI.Month, 16)
            Case < 24
                dteDueDate = DateSerial(dteDOI.Year, dteDOI.Month, 24)
            Case Else
                dteDueDate = DateSerial(dteDOI.Year, dteDOI.Month, 1).AddMonths(1).AddDays(-1)
        End Select
        Return Format(dteDueDate, "yyyy-MM-dd")
    End Function
    Public Function GetDueDate4AopBsp(dteDOI As Date) As String
        Dim dteDueDate As Date
        Select Case dteDOI.Day
            Case < 9
                dteDueDate = DateSerial(dteDOI.Year, dteDOI.Month, 9)
            Case < 16
                dteDueDate = DateSerial(dteDOI.Year, dteDOI.Month, 16)
            Case < 24
                dteDueDate = DateSerial(dteDOI.Year, dteDOI.Month, 24)
            Case Else
                dteDueDate = DateSerial(dteDOI.Year, dteDOI.Month, 1).AddMonths(1).AddDays(-1)
        End Select
        Return Format(dteDueDate, "yyyy-MM-dd")
    End Function
    Public Function CreateAopQueueAir(strRcpNo As String) As Boolean
        Dim tblRcp As DataTable = GetDataTable("Select * from Rcp where Status='ok' and Rcpno='" & strRcpNo & "'")
        Dim blnNoImportBill As Boolean
        If tblRcp.Rows.Count = 0 Then
            MsgBox("Invalid RCPNO " & strRcpNo)
            Return False
        End If
        If tblRcp.Rows(0)("FstUpdate") < "01 Jan 22" Then
            Return True
        End If
        blnNoImportBill = IsInVendorGrp("VENDOR NOT IMPORT AOP", tblRcp.Rows(0)("Vendor"))
        Select Case tblRcp.Rows(0)("Counter")
            Case "CWT"
                Return CreateAopQueueAirCTS(tblRcp.Rows(0), blnNoImportBill)
            Case "GSA"
                If myStaff.City = "SGN" Then
                    Return CreateAopQueueAirGsaSGN(tblRcp.Rows(0), tblRcp.Rows(0)("FstUpdate"), blnNoImportBill)
                ElseIf myStaff.City = "HAN" Then
                    Return CreateAopQueueAirGsaHAN(myStaff.City, tblRcp.Rows(0), tblRcp.Rows(0)("FstUpdate"), blnNoImportBill)
                End If

            Case "TVS"
                If myStaff.City = "SGN" Then
                    Return CreateAopQueueAirTvsSGN(tblRcp.Rows(0), blnNoImportBill)
                Else
                    Return CreateAopQueueAirTvsHAN(tblRcp.Rows(0), blnNoImportBill)
                End If

            Case Else
                Return True
        End Select

    End Function
    Public Function CheckDueDate(dteDueDate As DateTime) As Boolean
        Select Case dteDueDate.Day
            Case 9, 16, 24
                Return True
            Case Else
                If dteDueDate.AddDays(1).Day = 1 Then
                    Return True
                Else
                    Return False
                End If
        End Select
    End Function
    Public Function CreateDateRangeFilterFromDueDate(dteDueDate As DateTime) As String
        'gia dinh DueDate da duoc kiem tra truoc roi
        Dim strResult As String
        Dim dteStartDate As Date
        Dim dteEndDate As Date
        Select Case dteDueDate.Day
            Case 9
                dteStartDate = dteDueDate.AddDays(-8)
                dteEndDate = dteDueDate.AddDays(-1)
            Case 16
                dteStartDate = dteDueDate.AddDays(-7)
                dteEndDate = dteDueDate.AddDays(-1)
            Case 24
                dteStartDate = dteDueDate.AddDays(-8)
                dteEndDate = dteDueDate.AddDays(-1)
            Case Else
                dteStartDate = DateSerial(dteDueDate.Year, dteDueDate.Month, 24)
                dteEndDate = dteDueDate
        End Select
        strResult = " between " & Convert2AopDate(dteStartDate) & " and " & Convert2AopDate(dteEndDate)
        Return strResult
    End Function
    Public Function CreateAopQueueDownload4BSP(dteDueDate As DateTime, strCity As String) As Boolean
        Dim lstQuerries As New List(Of String)
        Dim strQuerry As String
        Dim strUpdateStatus As String
        Dim intNewRecId As Integer = 0
        If Not CheckDueDate(dteDueDate) Then
            MsgBox("Invalid Due Date!")
            Return False
        End If
        strQuerry = "Select *, '" & myStaff.City & "' as City from Bill where (DueDate=" _
                    & Convert2AopDate(dteDueDate) & ")"
        If strCity = "SGN" Then
            strQuerry = strQuerry & " and VendorRefFullName in" _
                & "('BSP [2249]','BSP AERTICKET-37314944 [11499]','VN [2071]','VN DEB [9624]')"
        ElseIf strCity = "HAN" Then
            strQuerry = strQuerry & " and VendorRefFullName in ('IATA HAN (SINCE 16APR) [5262]')"
        End If

        intNewRecId = CreateAopQueueRecord(0, "B", "AOP", "BSP", CreateFromDate(dteDueDate), strQuerry, True, "ODBC", myStaff.City)
        If intNewRecId = 0 Then
            Return False
        Else
            strUpdateStatus = " where RecId in (" & intNewRecId
        End If

        strQuerry = "Select *, '" & myStaff.City & "' as City from VendorCredit where TxnDate " _
                    & CreateDateRangeFilterFromDueDate(dteDueDate)
        If strCity = "SGN" Then
            strQuerry = strQuerry & " and VendorRefFullName in" _
                & "('BSP [2249]','BSP AERTICKET-37314944 [11499]','VN [2071]','VN DEB [9624]')"
        ElseIf strCity = "HAN" Then
            strQuerry = strQuerry & " and VendorRefFullName in ('IATA HAN (SINCE 16APR) [5262]')"
        End If

        intNewRecId = CreateAopQueueRecord(0, "VC", "AOP", "BSP", CreateFromDate(dteDueDate), strQuerry, True, "ODBC", myStaff.City)
        If intNewRecId = 0 Then
            Return False
        Else
            strUpdateStatus = "Update AopQueue Set Status='OK'" & strUpdateStatus & "," & intNewRecId & ")"
        End If

        If Not ExecuteNonQuerry(strUpdateStatus, Conn) Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function CreateAopQueueAirCTS(objRcp As DataRow, Optional blnNoImportBill As Boolean = False) As Boolean

        Dim strQuerry As String
        Dim lstQueueRecIds As New List(Of String)
        Dim tblInvoice As DataTable
        Dim tblBill As DataTable
        Dim strBu As String = String.Empty
        Dim intResult As Integer
        Dim arrQueueRecIds As String()
        Dim strMemo As String
        Dim tblKickBack As DataTable
        Dim objRow As DataRow

        'Tao Invoice
        strQuerry = "select (case when m.RecID is null then 1 else 2 end) as InvCount, r.CustId, r.RcpNo,R.Srv" _
                & ",r.TtlDue as InvAmt, 0 as SvcFee" _
                & ",r.Charge as MerchantFee" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & ",l.CustShortName,l.AOPListID" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS Tkno" _
                & ",r.Roe, v.Cur as VendorCur,r.Vendor,r.Currency as RcpCur" _
                & " from Rcp r" _
                & " left join CustomerList l on l.Recid=r.CustId" _
                & " left join Misc m on r.CustId=m.intVal and m.Cat='CustNameInGroup' and m.VAL='2 INVOICES CUS' and m.Status='OK'" _
                & " left join Vendor v on r.VendorId=v.RecID" _
                & " where r.status='OK' and r.Counter='CWT' and r.RcpNo='" & objRcp("RcpNo") & "'"

        tblInvoice = GetDataTable(strQuerry, Conn)

        If tblInvoice.Rows.Count = 0 Then
            MsgBox("Invalide Rcp:" & objRcp("RcpNo"))
            Return False
        End If
        If tblInvoice.Rows(0)("AOPListID") = "" Then
            MsgBox("You must ask PQT to update AOPListID for the following Customers: " & tblInvoice.Rows(0)("CustShortName"))
            Return False
        End If


        For Each objRow In tblInvoice.Rows
            strBu = "CTS-AIR"

            If objRow("Tkno").ToString.EndsWith("/") Then
                strMemo = Mid(objRow("Tkno"), 1, Len(objRow("Tkno")) - 1)
            Else
                strMemo = objRow("Tkno")
            End If
            If objRow("InvCount") = 1 Then
                Dim decInvoiceAmt As Decimal = objRow("InvAmt")
                Select Case objRow("Vendor")
                    Case "AK TK", "6E INDIGO USD"
                        decInvoiceAmt = decInvoiceAmt * objRow("ROE")
                End Select

                If objRow("SRV") = "S" Then
                    intResult = CreateAopQueueInvoice("SGN", 0, "I", "Air", strBu, objRcp("RcpNo"), decInvoiceAmt, objRow("MerchantFee"), objRow("AOPListid") _
                                                      , objRow("TrxDate"), objRow("RefNumber"), strMemo, "VND")
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                Else
                    intResult = CreateAopQueueCreditMemo(objRow("AOPListid"), strBu, "CUSTOMER RECEIVABLE (VND)", objRow("TrxDate") _
                                                         , objRow("RefNumber"), strMemo, decInvoiceAmt, objRow("MerchantFee"), strMemo _
                                                         , objRcp("RcpNo"), "Air", "REVENUE")
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                End If
            ElseIf objRow("InvCount") = 2 Then
                objRow("SvcFee") = ScalarToDec("tkt", "sum(ChargeTV)", "Status<>'xx' and Rcpno='" & objRow("RcpNo") & "'")
                objRow("InvAmt") = objRow("InvAmt") - objRow("SvcFee")
                Dim decInvoiceAmt As Decimal = objRow("InvAmt")
                Dim decSvcFee As Decimal = objRow("SvcFee")

                Select Case objRow("Vendor")
                    Case "AK TK", "6E INDIGO USD"
                        decInvoiceAmt = decInvoiceAmt * objRow("ROE")
                        decSvcFee = decSvcFee * objRow("ROE")
                End Select

                decInvoiceAmt = RoundNearest(decInvoiceAmt)


                If objRow("SRV") = "S" Then
                    Dim decMerchantFee As Decimal
                    If decInvoiceAmt >= 1 Then       ' bo 1 so tinh huong invoice amount bi am do van de lam tron hoặc nhỏ hơn 1 VND
                        intResult = CreateAopQueueInvoice("SGN", 0, "I", "Air", strBu, objRcp("RcpNo"), decInvoiceAmt, objRow("MerchantFee"), objRow("AOPListid") _
                                                      , objRow("TrxDate"), objRow("RefNumber"), strMemo, "VND")
                        If intResult = 0 Then
                            Return False
                        Else
                            lstQueueRecIds.Add(intResult.ToString)
                        End If
                    End If
                    Select Case objRow("CustShortName")
                        Case "PG VIETNAM", "PG INDOCHINA"
                            decMerchantFee = 0
                        Case Else
                            decMerchantFee = objRow("MerchantFee")
                    End Select

                    If objRow("SvcFee") > 0 Then
                        intResult = CreateAopQueueInvoice("SGN", 0, "I", "Air", strBu, objRcp("RcpNo"), decSvcFee, 0, objRow("AOPListid") _
                                                      , objRow("TrxDate"), objRow("RefNumber"), strMemo, "VND")
                        If intResult = 0 Then
                            Return False
                        Else
                            lstQueueRecIds.Add(intResult.ToString)
                        End If
                    End If


                Else
                    intResult = CreateAopQueueCreditMemo(objRow("AOPListid"), strBu, "CUSTOMER RECEIVABLE (VND)" _
                                                         , objRow("TrxDate"), objRow("RefNumber"), objRow("Tkno"), Math.Abs(decInvoiceAmt) _
                                                         , objRow("MerchantFee"), strMemo, objRcp("RcpNo"), "Air" _
                                                         , "REVENUE")
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                    intResult = CreateAopQueueCreditMemo(objRow("AOPListid"), strBu, "CUSTOMER RECEIVABLE (VND)", objRow("TrxDate") _
                                                         , objRow("RefNumber"), objRow("Tkno"), decSvcFee, objRow("MerchantFee") _
                                                         , strMemo, objRcp("RcpNo"), "Air", "REVENUE")
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                End If

            End If
        Next

        'import KickBack, MiscFee
        strQuerry = "select r.CustId, r.RcpNo,R.Srv" _
                & ",(select sum(KickBackAmt) from Tkt where RcpId=r.RecId and Status<>'XX') as KickBackAmt" _
                & ",(select sum(MiscFeeAmt) from Tkt where RcpId=r.RecId and Status<>'XX') as MiscFeeAmt" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & ",l.CustShortName" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID  ORDER BY TKNO For Xml path('') ) AS Tkno" _
                & " ,AOPListID as CustAOPid" _
                & " from Rcp r" _
                & " left join CustomerList l on l.Recid=r.CustId" _
                & " where r.status='OK' and r.Counter='CWT' and RcpNo='" & objRcp("RcpNo") _
                & "' order by r.FstUpdate"

        tblKickBack = GetDataTable(strQuerry)

        If tblKickBack.Rows.Count > 0 Then
            objRow = tblKickBack.Rows(0)
            If objRow("KickBackAmt") > 0 Then
                If objRow("SRV") = "S" Then
                    strMemo = objRow("Tkno") & " RFD MU"
                    intResult = CreateAopQueueCreditMemo(objRow("CustAOPid"), strBu, "CUSTOMER RECEIVABLE (VND)", objRow("TrxDate") _
                                                      , objRow("RefNumber"), objRow("Tkno"), objRow("KickBackAmt"), 0 _
                                                      , strMemo, "REVENUE", "Air", "REVENUE")
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                ElseIf objRow("SRV") = "R" Then
                    strMemo = objRow("Tkno") & " THU MU"
                    intResult = CreateAopQueueInvoice(myStaff.City, 0, "I", "Air", strBu, objRcp("RcpNo"), objRow("KickBackAmt"), 0, objRow("CustAOPid") _
                                                          , objRow("TrxDate"), objRow("RefNumber"), strMemo,,)
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                End If
            End If
            If objRow("MiscFeeAmt") > 0 Then
                Dim strMiscFeeVendorAop = "8000181B-1648546687"
                If objRow("SRV") = "S" Then
                    intResult = CreateAopQueueBill(myStaff.City, strMiscFeeVendorAop, objRow("CustAOPid"), strBu, objRow("TrxDate") _
                                                                         , objRow("RefNumber"), "COST", objRow("MiscFeeAmt"), 0 _
                                                                         , strMemo, "Air", objRcp("RcpNo"), "VND", "")
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If

                ElseIf objRow("SRV") = "R" Then
                    intResult = CreateAopQueueVendorCredit(strMiscFeeVendorAop, objRow("CustAopId"), strBu, "VENDOR PAYABLE (VND)" _
                                              , objRow("TrxDate"), objRow("RefNumber"), "COST", objRow("MiscFeeAmt"), 0, strMemo, "Air", objRcp("RcpNo"))
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                End If

            End If
        End If


        'Tao bill
        If blnNoImportBill Then GoTo NoImportBill
        strQuerry = "select r.Vendor,c.CustShortName, r.RcpNo,R.Srv" _
                & " ,(select top 1 DOI from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0) AS DOI" _
                & " ,(select sum((NetToAL+Tax2AL)+Charge*t.Qty) from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0) AS BillAmt" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & " ,(select distinct t.DocType+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS DocType" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS Tkno" _
                & ",v.AOPListID as VendorAopId,c.AOPListID as CustAopId,v.Cur as VendorCur " _
                & " from Rcp r" _
                & " left join CustomerList c on c.Recid=r.CustId" _
                & " left join Vendor v on v.Recid=r.VendorId" _
                & " where r.status='OK' and r.Srv<>'V' and r.Counter='CWT' and r.rcpno='" & objRcp("RcpNo") & "'" _
                & " and r.RecId not in (Select RcpId from tkt where DocType='AHC')" _
                & " order by r.FstUpdate"

        tblBill = GetDataTable(strQuerry, Conn)

        For Each objRow In tblBill.Rows
            If objRow("Vendor") = "" Then
                MsgBox("You must ask CTS to update Vendor for " & objRcp("RcpNo"))
                Return False
            ElseIf objRow("VendorAopId") = "" Then
                MsgBox("You must ask PQT to update AOPListID for the following Vendor:  " & objRow("Vendor"))
                Return False
            End If
        Next

        For Each objRow In tblBill.Rows
            Dim strBillCur As String = "VND"

            Select Case objRow("Vendor")
                Case "AK TK", "6E INDIGO USD"
                    strBillCur = "USD"
            End Select

            If objRow("Tkno").ToString.EndsWith("/") Then
                strMemo = Mid(objRow("Tkno"), 1, Len(objRow("Tkno")) - 1)
            Else
                strMemo = objRow("Tkno")
            End If

            If objRow("SRV") = "R" Then
                intResult = CreateAopQueueVendorCredit(objRow("VendorAopId"), objRow("CustAopId"), strBu, "VENDOR PAYABLE (" & strBillCur & ")" _
                              , objRow("TrxDate"), objRow("RefNumber"), "COST", objRow("BillAmt"), 0, strMemo, "Air", objRcp("RcpNo"))
                If intResult = 0 Then
                    Return False
                Else
                    lstQueueRecIds.Add(intResult.ToString)
                End If
            Else
                Dim strDueDate As String = String.Empty

                Select Case objRow("Vendor")
                    Case "VN", "BSP", "VN DEB"
                        If objRow("DocType").ToString.Contains("ETK") Or objRow("DocType").ToString.Contains("EMD") _
                            Or objRow("DocType").ToString.Contains("MCO") Then
                            If objRow("Vendor") = "BSP" Then
                                strDueDate = GetDueDate4AopBsp(objRow("DOI"))
                            Else
                                strDueDate = GetDueDate4AopNonBsp(objRow("DOI"))
                            End If
                        End If
                    Case "QH TK"
                        strDueDate = Format(objRow("DOI"), "yyyy-MM-dd")
                End Select

                intResult = CreateAopQueueBill(myStaff.City, objRow("VendorAopId"), objRow("CustAOPid"), strBu, objRow("TrxDate") _
                                                         , objRow("RefNumber"), "COST", objRow("BillAmt"), 0 _
                                                         , strMemo, "Air", objRcp("RcpNo"), strBillCur, strDueDate)
                If intResult = 0 Then
                    Return False
                Else
                    lstQueueRecIds.Add(intResult.ToString)
                End If
            End If
        Next
NoImportBill:

        ReDim arrQueueRecIds(0 To lstQueueRecIds.Count - 1)
        lstQueueRecIds.CopyTo(arrQueueRecIds)
        Return ExecuteNonQuerry("update AopQueue Set Status='OK' where LinkId in (" & Strings.Join(arrQueueRecIds, ",") & ")", Conn)
    End Function
    Public Function CreateAopQueueAirGsaHAN(strCity As String, objRcp As DataRow, dteTrxDate As Date _
                                         , Optional blnNoImportBill As Boolean = False) As Boolean
        Dim decAopUsdRoe As Decimal = ScalarToDec("Forex", "TOP 1 BSR", "Status='OK' and Currency='USD' and ApplyROETo='AOP'" _
                                                  & " and EffectDate <='" & CreateFromDate(dteTrxDate) & "' and City='" & strCity _
                                                  & "' order by EffectDate")
        Dim tblInvoice As DataTable
        Dim tblBill As DataTable
        Dim objRow As DataRow
        Dim lstQueueRecIds As New List(Of String)
        Dim intResult As Integer
        Dim strBu As String = String.Empty
        Dim strMemo As String
        Dim arrQueueRecIds As String()
        Dim strVendorAopId As String
        Dim strCur As String = ""
        Dim decInvAmt As Decimal
        Dim strBillCur As String = "VND"
        Dim decBillAmt As Decimal
        Dim strItemLineItemRefFullName As String = ""

        If strCity = "SGN" Then
            strItemLineItemRefFullName = "COST"
        ElseIf strCity = "HAN" Then
            strItemLineItemRefFullName = "COGS:Cost"
        End If

        Dim strQuerry As String = "select r.AL, r.CustId, r.RcpNo,R.Srv" _
                & ",r.Roe*r.TtlDue as InvAmt, 0 as SvcFee,r.Currency as RcpCur ,r.TtlDue" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & ",l.CustShortName" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID  ORDER BY TKNO For Xml path('') ) AS Tkno" _
                & " ,AOPListID,r.Roe" _
                & " from Rcp r" _
                & " left join CustomerList l on l.Recid=r.CustId" _
                & " where r.status='OK' and r.Counter='GSA' and RcpNo='" & objRcp("RcpNo") _
                & "' order by r.FstUpdate"

        tblInvoice = GetDataTable(strQuerry)

        If tblInvoice.Rows.Count = 0 Then
            MsgBox("Invalid RCP " & objRcp("RcpNo"))
            Return False
        Else
            objRow = tblInvoice.Rows(0)
        End If

        If objRow("AOPListID") = "" Then
            MsgBox("Missing AOPListID for " & objRow("CustShortName"))
            Return False
        End If

        objRow("tkno") = Mid(objRow("tkno"), 1, Len(objRow("Tkno")) - 1)
        If objRow("RcpCur") = "VND" Then
            strMemo = objRow("tkno")
        Else
            strMemo = objRow("tkno") & " " & objRow("RcpCur") & objRow("TtlDue") & " /" & objRow("ROE")
        End If

        decInvAmt = objRow("InvAmt")

        Select Case objRow("AL")
            Case "4V"
                strVendorAopId = "8000049A-1675738379"
                strBu = "HQ:4V"
            Case "BA"
                strVendorAopId = "800002FE-1653445936"
                strBu = "Travel:PAX:BA"
            Case "GA"
                strVendorAopId = "80000005-1609319348"
                strBu = "Travel:PAX:GA"
            Case "NH"
                strVendorAopId = "80000002-1609319342"
                strBu = "Travel:NH"
            Case "PG"
                strVendorAopId = "80000006-1609319350"
                strBu = "Travel:PAX:PG"
            Case "RJ"
                strVendorAopId = "800002EC-1652775937"
                strBu = "Travel:PAX:RJ"
            Case "UA"
                strVendorAopId = "80000008-1609319354"
                strBu = "Travel:PAX:UA"
            Case "UL"
                strVendorAopId = "8000043B-1665798856"
                strBu = "Travel:PAX:UL"
            Case Else
                Return False
        End Select

        'import Invoice
        If objRow("SRV") = "S" Then
            intResult = CreateAopQueueInvoice(strCity, 0, "I", "Air", strBu, objRcp("RcpNo"), decInvAmt, 0, objRow("AOPListid") _
                                                          , objRow("TrxDate"), objRow("RefNumber"), strMemo, strCur)
            If intResult = 0 Then
                Return False
            Else
                lstQueueRecIds.Add(intResult.ToString)
            End If

        Else    'refund            
            Dim strItemName As String = "REVENUE:Revenue"
            Dim strAccountName As String = "PHAI THU KHACH HANG"
            intResult = CreateAopQueueCreditMemo(objRow("AOPListid"), strBu, strAccountName, objRow("TrxDate") _
                                                , objRow("RefNumber"), strMemo, objRow("InvAmt"), 0, strMemo, objRcp("RcpNo") _
                                                , "Air", strItemName)
            If intResult = 0 Then
                Return False
            Else
                lstQueueRecIds.Add(intResult.ToString)
            End If
        End If


        'IMPORT BILL
        If blnNoImportBill Then GoTo NoImportBill

        strQuerry = "select r.AL,c.CustShortName, r.RcpNo,R.Srv" _
                & " ,(select top 1 DOI from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0) AS DOI" _
                & " ,((select sum((NetToAL+Tax)+Charge*t.Qty) from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0)" _
                & " - (select isnull(sum(Amount),0) from FOP f where f.Status='OK' and f.RCPID=r.RecID and f.FOP='EXC'))*r.Roe  AS BillAmt" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS Tkno" _
                & ",c.AOPListID as CustAopId,r.CustId,r.Currency as RcpCur,r.Roe" _
                & " ,(select sum((NetToAL+Tax)+Charge*t.Qty) from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0)  AS TtlDue" _
                & " from Rcp r" _
                & " left join CustomerList c on c.Recid=r.CustId" _
                & " where r.status='OK' and r.Srv<>'V' and r.Counter='GSA' and r.RcpNo='" & objRcp("RcpNo") & "'"

        tblBill = GetDataTable(strQuerry)
        objRow = tblBill.Rows(0)

        If objRow("CustAopId") = "" Then
            MsgBox("You must ask PQT to update AOPListID for the following Customer: " & objRow("CustShortName"))
            Return False
        ElseIf IsDBNull(objRow("BillAmt")) Then
            GoTo NoImportBill    've XX doi voi Hang
        End If

        objRow("Tkno") = Mid(objRow("tkno"), 1, Len(objRow("Tkno")) - 1)
        If objRow("RcpCur") = "VND" Then
            strMemo = objRow("Tkno")
        Else
            strMemo = objRow("Tkno") & " " & objRow("RcpCur") & objRow("TtlDue") & "/" & objRow("ROE")
        End If

        decBillAmt = objRow("BillAmt")

        Select Case objRow("AL")
            Case "4V"
                strVendorAopId = "8000049A-1675738379"
                strBu = "HQ:4V"
            Case "BA"
                strVendorAopId = "800002FE-1653445936"
                strBu = "Travel:PAX:BA"

            Case "GA"
                strVendorAopId = "80000005-1609319348"
                strBu = "Travel:PAX:GA"

            Case "NH"
                strVendorAopId = "80000002-1609319342"
                strBu = "Travel:NH"

            Case "PG"
                strVendorAopId = "80000006-1609319350"
                strBu = "Travel:PAX:PG"

            Case "RJ"
                strVendorAopId = "800002EC-1652775937"
                strBu = "Travel:PAX:RJ"

            Case "UA"
                strVendorAopId = "80000008-1609319354"
                strBu = "Travel:PAX:UA"

            Case "UL"
                strVendorAopId = "8000043B-1665798856"
                strBu = "Travel:PAX:UL"
            Case Else
                Return False
        End Select


        If objRow("SRV") = "R" Then
            Dim strAccountName As String = "PHAI TRA NGUOI BAN"
            Dim strItemName As String = "COGS:Cost"

            intResult = CreateAopQueueVendorCredit(strVendorAopId, objRow("CustAopId"), strBu, strAccountName _
                              , objRow("TrxDate"), objRow("RefNumber"), strItemName _
                              , decBillAmt, 0, strMemo, "Air", objRcp("RcpNo"))
            If intResult = 0 Then
                Return False
            Else
                lstQueueRecIds.Add(intResult.ToString)
            End If

        ElseIf objRow("BillAmt") > 0 Then       'bo qua ve zero value

            intResult = CreateAopQueueBill(strCity, strVendorAopId, objRow("CustAOPid"), strBu, objRow("TrxDate") _
                                                             , objRow("RefNumber"), strItemLineItemRefFullName _
                                                             , decBillAmt, 0 _
                                                             , strMemo, "Air", objRcp("RcpNo"), strBillCur, "")
            If intResult = 0 Then
                Return False
            Else
                lstQueueRecIds.Add(intResult.ToString)
            End If

        End If


NoImportBill:
        ReDim arrQueueRecIds(0 To lstQueueRecIds.Count - 1)
        lstQueueRecIds.CopyTo(arrQueueRecIds)
        Return ExecuteNonQuerry("update AopQueue Set Status='OK' where LinkId in (" & Strings.Join(arrQueueRecIds, ",") & ")", Conn)


    End Function
    Public Function CreateAopQueueAirGsaSGN(objRcp As DataRow, dteTrxDate As Date _
                                         , Optional blnNoImportBill As Boolean = False) As Boolean
        Dim decAopUsdRoe As Decimal = ScalarToDec("Forex", "TOP 1 BSR", "Status='OK' and Currency='USD' and ApplyROETo='AOP'" _
                                                  & " and EffectDate <='" & CreateFromDate(dteTrxDate) & "' and City='" & myStaff.City _
                                                  & "' order by EffectDate")
        Dim tblInvoice As DataTable
        Dim tblBill As DataTable
        Dim objRow As DataRow
        Dim lstQueueRecIds As New List(Of String)
        Dim intResult As Integer
        Dim strBu As String = String.Empty
        Dim strMemo As String
        Dim arrQueueRecIds As String()
        Dim strVendorAopId As String
        Dim strCur As String
        Dim decInvAmt As Decimal
        Dim strBillCur As String
        Dim decBillAmt As Decimal

        Dim strQuerry As String = "select r.AL, r.CustId, r.RcpNo,R.Srv" _
                & " ,(case (select count (*) from tkt t where t.Status<>'XX' and t.RCPID=r.RecID and T.DocType in ('GRP','MCO')) when 0 then 'FIT' else 'GRP' end) AS GRP" _
                & ",r.Roe*r.TtlDue as InvAmt, 0 as SvcFee,r.Currency as RcpCur ,r.TtlDue" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & ",l.CustShortName" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID  ORDER BY TKNO For Xml path('') ) AS Tkno" _
                & " ,AOPListID" _
                & " from Rcp r" _
                & " left join CustomerList l on l.Recid=r.CustId" _
                & " where r.status='OK' and r.Counter='GSA' and RcpNo='" & objRcp("RcpNo") _
                & "' order by r.FstUpdate"

        tblInvoice = GetDataTable(strQuerry)

        If tblInvoice.Rows.Count = 0 Then
            MsgBox("Invalid RCP " & objRcp("RcpNo"))
            Return False
        Else
            objRow = tblInvoice.Rows(0)
        End If

        If objRow("AOPListID") = "" Then
            MsgBox("Missing AOPListID for " & objRow("CustShortName"))
            Return False
        End If

        objRow("tkno") = Mid(objRow("tkno"), 1, Len(objRow("Tkno")) - 1)
        strMemo = objRow("tkno")
        strCur = "VND"
        Select Case objRow("GRP")
            Case "GRP"
                'Bo khong import tu dong
                Return True

            Case "FIT"
                decInvAmt = objRow("InvAmt")

                Select Case objRow("AL")
                    Case "4V"
                        strVendorAopId = "800020BA-1671783737"
                        strBu = "HQ:4V"
                        strMemo = strMemo & " USD" & objRow("TtlDue")

                    Case "BA"
                        strVendorAopId = "800003C5-1581301723"
                        strBu = "PAX:BA"
                        strMemo = strMemo & " USD" & objRow("TtlDue")
                    Case "NH"
                        strVendorAopId = "800011DE-1602163328"
                        strBu = "NH"
                    Case "GA"
                        strVendorAopId = "80001585-1617852223"
                        strBu = "PAX:GA"
                    Case "PG"
                        strVendorAopId = "800007C3-1591599380"
                        strBu = "PAX:PG"
                    Case "UA"
                        strVendorAopId = "800011E5-1602210655"
                        strBu = "PAX:UA"
                    Case "UL"
                        strVendorAopId = "80001741-1641952099"
                        strBu = "PAX:UL"
                    Case Else
                        Return False
                End Select

                'import Invoice
                If objRow("SRV") = "S" Then
                    intResult = CreateAopQueueInvoice(myStaff.City, 0, "I", "Air", strBu, objRcp("RcpNo"), decInvAmt, 0, objRow("AOPListid") _
                                                                  , objRow("TrxDate"), objRow("RefNumber"), strMemo, strCur)
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If

                Else    'refund
                    intResult = CreateAopQueueCreditMemo(objRow("AOPListid"), strBu, "CUSTOMER RECEIVABLE (" & strCur & ")", objRow("TrxDate") _
                                                        , objRow("RefNumber"), objRow("Tkno"), objRow("InvAmt"), 0, strMemo _
                                                        , objRcp("RcpNo"), "Air", "REVENUE")
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                End If
        End Select

        'IMPORT BILL
        If blnNoImportBill Then GoTo NoImportBill

        strQuerry = "select r.AL,c.CustShortName, r.RcpNo,R.Srv" _
                & " ,(select top 1 DOI from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0) AS DOI" _
                & " ,((select sum((NetToAL+Tax)+Charge*t.Qty) from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0)" _
                & " - (select isnull(sum(Amount),0) from FOP f where f.Status='OK' and f.RCPID=r.RecID and f.FOP='EXC'))*r.Roe  AS BillAmt" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS Tkno" _
                & ",c.AOPListID as CustAopId,r.CustId,r.Currency as RcpCur" _
                & " ,(select sum((NetToAL+Tax)+Charge*t.Qty) from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0)  AS TtlDue" _
                & " from Rcp r" _
                & " left join CustomerList c on c.Recid=r.CustId" _
                & " where r.status='OK' and r.Srv<>'V' and r.Counter='GSA' and r.RcpNo='" & objRcp("RcpNo") & "'"

        tblBill = GetDataTable(strQuerry)
        objRow = tblBill.Rows(0)


        If objRow("CustAopId") = "" Then
            MsgBox("You must ask PQT to update AOPListID for the following Customer: " & objRow("CustShortName"))
            Return False
        ElseIf IsDBNull(objRow("BillAmt")) Then
            GoTo NoImportBill    've XX doi voi Hang

        End If

        objRow("Tkno") = Mid(objRow("tkno"), 1, Len(objRow("Tkno")) - 1)
        strMemo = objRow("Tkno")

        Select Case objRow("AL")
            Case "4V"
                strVendorAopId = "800020BA-1671783737"
                strBu = "HQ:4V"
                strBillCur = "USD"
                decBillAmt = objRow("TtlDue")
                strMemo = strMemo & " USD" & decBillAmt

            Case "BA"
                strVendorAopId = "800003C5-1581301723"
                strBu = "PAX:BA"
                strBillCur = "USD"
                decBillAmt = objRow("TtlDue")
                strMemo = strMemo & " USD" & decBillAmt

            Case "GA"
                strVendorAopId = "80001585-1617852223"
                strBu = "PAX:GA"
                strBillCur = "VND"
                decBillAmt = objRow("BillAmt")

            Case "NH"
                strVendorAopId = "800011DE-1602163328"
                strBu = "NH"
                strBillCur = "VND"
                decBillAmt = objRow("BillAmt")

            Case "PG"
                strVendorAopId = "800007C3-1591599380"
                strBu = "PAX:PG"
                strBillCur = "USD"
                decBillAmt = objRow("TtlDue")
                strMemo = strMemo & " USD" & decBillAmt

            Case "UA"
                strVendorAopId = "800011E5-1602210655"
                strBu = "PAX:UA"
                strBillCur = "VND"
                decBillAmt = objRow("BillAmt")

            Case "UL"
                strVendorAopId = "80001741-1641952099"
                strBu = "PAX:UL"
                strBillCur = "VND"
                decBillAmt = objRow("TtlDue")

            Case Else
                Return False
        End Select


        If objRow("SRV") = "R" Then
            intResult = CreateAopQueueVendorCredit(strVendorAopId, objRow("CustAopId"), strBu, "VENDOR PAYABLE (" & strBillCur & ")" _
                              , objRow("TrxDate"), objRow("RefNumber"), "COST", decBillAmt, 0, strMemo, "Air", objRcp("RcpNo"))
            If intResult = 0 Then
                Return False
            Else
                lstQueueRecIds.Add(intResult.ToString)
            End If

        ElseIf objRow("BillAmt") > 0 Then       'bo qua ve zero value

            intResult = CreateAopQueueBill(myStaff.City, strVendorAopId, objRow("CustAOPid"), strBu, objRow("TrxDate") _
                                                             , objRow("RefNumber"), "COST", decBillAmt, 0 _
                                                             , strMemo, "Air", objRcp("RcpNo"), strBillCur, "")
            If intResult = 0 Then
                Return False
            Else
                lstQueueRecIds.Add(intResult.ToString)
            End If

        End If


NoImportBill:
        ReDim arrQueueRecIds(0 To lstQueueRecIds.Count - 1)
        lstQueueRecIds.CopyTo(arrQueueRecIds)
        Return ExecuteNonQuerry("update AopQueue Set Status='OK' where LinkId in (" & Strings.Join(arrQueueRecIds, ",") & ")", Conn)


    End Function
    Public Function CreateAopQueueAirTvsHAN(objRcp As DataRow, Optional blnNoImportBill As Boolean = False) As Boolean

        Dim tblInvoice As DataTable
        Dim tblBill As DataTable

        Dim objRow As DataRow
        Dim lstQueueRecIds As New List(Of String)
        Dim intResult As Integer
        Dim strBu As String = "Travel:TVS"
        Dim strMemo As String
        Dim arrQueueRecIds As String()

        Dim strQuerry As String = "select r.CustId, r.RcpNo,R.Srv" _
                & " ,(case (select count (*) from tkt t where t.Status<>'XX' and t.RCPID=r.RecID and T.DocType in ('GRP','MCO')) when 0 then 'FIT' else 'GRP' end) AS GRP" _
                & ",'' as AopRecord" _
                & ",r.Roe*r.TtlDue as InvAmt, 0 as SvcFee" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & ",l.CustShortName,'' as TourCode" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID  ORDER BY TKNO For Xml path('') ) AS Tkno,'' as DepDate" _
                & ",'' as Account,'' as AccountName" _
                & ",'' as OriDeposits" _
                & " ,(select distinct t.DocType+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS DocTypes,AOPListID" _
                & ",r.Vendor,r.VendorId" _
                & ",r.TtlDue,r.ROE,r.Currency as RcpCur" _
                & " from Rcp r" _
                & " left join CustomerList l on l.Recid=r.CustId" _
                & " where r.status='OK' and r.Counter='TVS' and RcpNo='" & objRcp("RcpNo") _
                & "' order by r.FstUpdate"

        tblInvoice = GetDataTable(strQuerry)

        If tblInvoice.Rows.Count = 0 Then
            PromtAndLog("Invalid RCP for AOP Queue creation " & objRcp("RcpNo"))
            Return False
        Else
            objRow = tblInvoice.Rows(0)
        End If

        If objRow("AOPListID") = "" Then
            PromtAndLog("Missing AOPListID for " & objRow("CustShortName"))
            Return False
        ElseIf objRow("AopRecord") = "Deposit" Then
            'MsgBox("You must update account for " & objRow("RcpNo"))
            Return False
        End If

        objRow("tkno") = Mid(objRow("tkno"), 1, Len(objRow("Tkno")) - 1)

        If objRow("RcpCur") = "VND" Then
            strMemo = objRow("tkno")
        Else
            strMemo = objRow("tkno") & " " & objRow("RcpCur") & objRow("TtlDue") & " /" & objRow("ROE")
        End If

        Select Case objRow("CustShortName")
            Case "TRANSVIE"
                AddCodeTour2MemoAop(objRow("RcpNo"), strMemo)
        End Select

        'import Invoice
        Select Case objRow("GRP")
            Case "GRP"
                'Bo khong import tu dong

            Case "FIT"

                If objRow("SRV") = "S" Then
                    intResult = CreateAopQueueInvoice("HAN", 0, "I", "Air", strBu, objRcp("RcpNo"), objRow("InvAmt"), 0, objRow("AOPListid") _
                                                      , objRow("TrxDate"), objRow("RefNumber"), strMemo)
                    If intResult = 0 Then
                            Return False
                        Else
                            lstQueueRecIds.Add(intResult.ToString)
                        End If

                    Else    'refund
                    intResult = CreateAopQueueCreditMemo(objRow("AOPListid"), strBu, "PHAI THU KHACH HANG", objRow("TrxDate") _
                                                             , objRow("RefNumber"), strMemo, objRow("InvAmt"), 0, strMemo, objRcp("RcpNo") _
                                                             , "Air", "REVENUE:Revenue")
                    If intResult = 0 Then
                            Return False
                        Else
                            lstQueueRecIds.Add(intResult.ToString)
                        End If
                    End If

                End Select


        'IMPORT BILL
        If blnNoImportBill Then GoTo NoImportBill

        strQuerry = "select r.Vendor,c.CustShortName, r.RcpNo,R.Srv" _
                & " ,(select top 1 DOI from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0) AS DOI" _
                & " ,((select sum((NetToAL+Tax)+Charge*t.Qty) from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0)" _
                & " - (select isnull(sum(Amount),0) from FOP f where f.Status='OK' and f.RCPID=r.RecID and f.FOP='EXC'))*r.Roe  AS BillAmt" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & " ,(select distinct t.DocType+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS DocType" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS Tkno" _
                & ",'' AS TourCode,'' as DepDate,'' as TspCust,'' as TspCustAopId,'' as TspClass" _
                & ",v.AOPListID as VendorAopId,c.AOPListID as CustAopId,r.CustId,r.Currency as RcpCur,v.Cur as VenCur,r.TtlDue,r.Roe " _
                & " from Rcp r" _
                & " left join CustomerList c on c.Recid=r.CustId" _
                & " left join Vendor v on v.Recid=r.VendorId" _
                & " where r.status='OK' and r.Srv<>'V' and r.Counter='TVS' and r.RcpNo='" & objRcp("RcpNo") & "'" _
                & " and r.RecId not in (Select RcpId from tkt where DocType='AHC')"

        tblBill = GetDataTable(strQuerry)
            objRow = tblBill.Rows(0)

            If IsDBNull(objRow("VendorAopId")) Then
                MsgBox("You must ask PQT to update AOPListID for the following Vendor: " & objRow("Vendor"))
                Return False
            End If
            If objRow("CustAopId") = "" Then
                MsgBox("You must ask PQT to update AOPListID for the following Customer: " & objRow("CustShortName"))
                Return False
            End If

            objRow("Tkno") = Mid(objRow("tkno"), 1, Len(objRow("Tkno")) - 1)

        If objRow("RcpCur") = "VND" Then
            strMemo = objRow("tkno")
        Else
            strMemo = objRow("tkno") & " " & objRow("RcpCur") & objRow("TtlDue") & " /" & objRow("ROE")
        End If

        Select Case objRow("CustShortName")
            Case "TRANSVIE"
                AddCodeTour2MemoAop(objRow("RcpNo"), strMemo)
        End Select

        If IsDBNull(objRow("BillAmt")) Then
                GoTo NoImportBill    've XX doi voi Hang
            End If
            Dim strBillCur As String = "VND"
            Dim decBillAmt As Decimal = objRow("BillAmt")

        If objRow("SRV") = "R" Then
            intResult = CreateAopQueueVendorCredit(objRow("VendorAopId"), objRow("CustAopId"), strBu, "PHAI TRA NGUOI BAN" _
                                  , objRow("TrxDate"), objRow("RefNumber"), "COGS:Cost", decBillAmt, 0, strMemo, "Air", objRcp("RcpNo"))
            If intResult = 0 Then
                Return False
            Else
                lstQueueRecIds.Add(intResult.ToString)
            End If

        ElseIf objRow("BillAmt") > 0 Then       'bo qua ve zero value
            Dim strDueDate As String = String.Empty
            Select Case objRow("Vendor")
                Case "IATA HAN (SINCE 16APR)"
                    If objRow("DocType").ToString.Contains("ETK") Or objRow("DocType").ToString.Contains("EMD") _
                                                Or objRow("DocType").ToString.Contains("MCO") Then
                        strDueDate = GetDueDate4AopBsp(objRow("DOI"))
                    End If
                Case "VN HAN"
                    If objRow("DocType").ToString.Contains("ETK") Or objRow("DocType").ToString.Contains("EMD") _
                                                Or objRow("DocType").ToString.Contains("MCO") Then
                        strDueDate = GetDueDate4AopNonBsp(objRow("DOI"))
                    End If
            End Select

            intResult = CreateAopQueueBill("HAN", objRow("VendorAopId"), objRow("CustAOPid"), strBu, objRow("TrxDate") _
                                            , objRow("RefNumber"), "COGS:Cost", decBillAmt, 0 _
                                            , strMemo, "Air", objRcp("RcpNo"), strBillCur, strDueDate)
            If intResult = 0 Then
                Return False
            Else
                lstQueueRecIds.Add(intResult.ToString)
            End If

        End If


NoImportBill:
        ReDim arrQueueRecIds(0 To lstQueueRecIds.Count - 1)
        lstQueueRecIds.CopyTo(arrQueueRecIds)
        Return ExecuteNonQuerry("update AopQueue Set Status='OK' where LinkId in (" & Strings.Join(arrQueueRecIds, ",") & ")", Conn)


    End Function
    Public Function CreateAopQueueAirTvsSGN(objRcp As DataRow, Optional blnNoImportBill As Boolean = False) As Boolean
        Dim decAopUsdRoe As Decimal = ScalarToDec("Forex", "TOP 1 BSR", "Status='OK' and Currency='USD' and ApplyROETo='AOP'" _
                                                  & " and EffectDate <='" & CreateFromDate(objRcp("FstUpdate")) & "' and City='" & myStaff.City _
                                                  & "' order by EffectDate")
        Dim tblInvoice As DataTable
        Dim tblBill As DataTable
        Dim tblKickBack As DataTable

        Dim objRow As DataRow
        Dim lstQueueRecIds As New List(Of String)
        Dim intResult As Integer
        Dim strBu As String = "TVS"
        Dim strMemo As String
        Dim arrQueueRecIds As String()

        Dim strQuerry As String = "select r.CustId, r.RcpNo,R.Srv" _
                & " ,(case (select count (*) from tkt t where t.Status<>'XX' and t.RCPID=r.RecID and T.DocType in ('GRP','MCO')) when 0 then 'FIT' else 'GRP' end) AS GRP" _
                & ",'' as AopRecord" _
                & ",r.Roe*r.TtlDue as InvAmt, 0 as SvcFee" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & ",l.CustShortName,'' as TourCode" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID  ORDER BY TKNO For Xml path('') ) AS Tkno,'' as DepDate" _
                & ",'' as Account,'' as AccountName" _
                & ",'' as OriDeposits" _
                & " ,(select distinct t.DocType+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS DocTypes,AOPListID" _
                & ",r.Vendor,r.VendorId" _
                & ", (case r.CustShortName when 'VYM' then " & decAopUsdRoe & " else 1 end)  as ROE" _
                & " from Rcp r" _
                & " left join CustomerList l on l.Recid=r.CustId" _
                & " where r.status='OK' and r.Counter='TVS' and RcpNo='" & objRcp("RcpNo") _
                & "' order by r.FstUpdate"

        tblInvoice = GetDataTable(strQuerry)

        If tblInvoice.Rows.Count = 0 Then
            MsgBox("Invalid RCP " & objRcp("RcpNo"))
            Return False
        Else
            objRow = tblInvoice.Rows(0)
        End If

        If objRow("AOPListID") = "" Then
            MsgBox("Missing AOPListID for " & objRow("CustShortName"))
            Return False
        ElseIf objRow("AopRecord") = "Deposit" Then
            'MsgBox("You must update account for " & objRow("RcpNo"))
            Return False
        End If

        objRow("tkno") = Mid(objRow("tkno"), 1, Len(objRow("Tkno")) - 1)
        strMemo = objRow("tkno")


        'import Invoice
        Select Case objRow("GRP")
            Case "GRP"
                'Bo khong import tu dong
                Return True

            Case "FIT"
                Select Case objRow("CustShortName")

                    Case "TVHAN"
                        'Bo qua ko nhap

                    Case "TVSGN", "GDSSGN"
                        strMemo = strMemo & GetColumnValuesAsString("FOP", "Document", "WHERE Status='OK' and RcpNO='" & objRcp("RcpNo") & "' and FOP<>'EXC'", "|")
                        Dim decExchangeAmt As Decimal = ScalarToDec("FOP", "Amount", "Status='OK' and FOP='EXC' and RcpNo='" & objRcp("RcpNo") & "'")

                        objRow("InvAmt") = objRow("InvAmt") - decExchangeAmt

                        If objRow("SRV") = "S" Then
                            intResult = CreateAopQueueInvoice(myStaff.City, 0, "I", "Air", strBu, objRcp("RcpNo"), objRow("InvAmt"), 0, objRow("AOPListid") _
                                                          , objRow("TrxDate"), objRow("RefNumber"), strMemo,, "ODBC")
                            If intResult = 0 Then
                                Return False
                            Else
                                lstQueueRecIds.Add(intResult.ToString)
                            End If

                        Else    'refund
                            intResult = CreateAopQueueCreditMemo(objRow("AOPListid"), strBu, "CUSTOMER RECEIVABLE (VND)", objRow("TrxDate") _
                                                             , objRow("RefNumber"), objRow("Tkno"), objRow("InvAmt"), 0, strMemo, objRcp("RcpNo") _
                                                             , "Air", "REVENUE")
                            If intResult = 0 Then
                                Return False
                            Else
                                lstQueueRecIds.Add(intResult.ToString)
                            End If
                        End If
                    Case "VYM" 'VAYMA (TRAVIX)
                        strMemo = objRow("Tkno") & " (" & Format(objRow("InvAmt"), "#,##0") & "/" & Format(objRow("ROE"), "#,##0") & ")"
                        Dim decInvAmt As Decimal = Math.Round(objRow("InvAmt") / objRow("ROE"), 2)
                        If objRow("SRV") = "S" Then
                            intResult = CreateAopQueueInvoice("SGN", 0, "I", "Air", strBu, objRcp("RcpNo"), decInvAmt, 0, objRow("AOPListid") _
                                                          , objRow("TrxDate"), objRow("RefNumber"), strMemo, "USD")
                            If intResult = 0 Then
                                Return False
                            Else
                                lstQueueRecIds.Add(intResult.ToString)
                            End If

                        Else
                            intResult = CreateAopQueueCreditMemo(objRow("AOPListid"), strBu, "CUSTOMER RECEIVABLE (USD)", objRow("TrxDate") _
                                                             , objRow("RefNumber"), objRow("Tkno"), decInvAmt, 0, strMemo, objRcp("RcpNo") _
                                                             , "Air", "REVENUE")
                            If intResult = 0 Then
                                Return False
                            Else
                                lstQueueRecIds.Add(intResult.ToString)
                            End If

                        End If

                    Case Else
                        If objRow("SRV") = "S" Then
                            intResult = CreateAopQueueInvoice(myStaff.City, 0, "I", "Air", strBu, objRcp("RcpNo"), objRow("InvAmt"), 0, objRow("AOPListid") _
                                                          , objRow("TrxDate"), objRow("RefNumber"), strMemo)
                            If intResult = 0 Then
                                Return False
                            Else
                                lstQueueRecIds.Add(intResult.ToString)
                            End If

                        Else    'refund
                            intResult = CreateAopQueueCreditMemo(objRow("AOPListid"), strBu, "CUSTOMER RECEIVABLE (VND)", objRow("TrxDate") _
                                                             , objRow("RefNumber"), objRow("Tkno"), objRow("InvAmt"), 0, strMemo, objRcp("RcpNo") _
                                                             , "Air", "REVENUE")
                            If intResult = 0 Then
                                Return False
                            Else
                                lstQueueRecIds.Add(intResult.ToString)
                            End If
                        End If
                End Select
        End Select


        ' Import for TSP
        Select Case objRow("CustShortName")
            Case "TVHAN"
                Dim tblTourCodes As DataTable = GetTourCodeTableByRcp(objRcp("RcpNo"))
                Dim strJournalCreditLineEntityRefFullName As String = objRow("Vendor") & " [" & objRow("VendorId") & "]"

                If tblTourCodes.Rows.Count > 0 Then
                    strMemo = objRow("Tkno")
                    For Each objToourCodeRow As DataRow In tblTourCodes.Rows
                        strMemo = strMemo & " " & objToourCodeRow("TourCode")
                    Next
                End If

                If objRow("SRV") = "S" Then
                    intResult = CreateAopQueueJournalEntry(strBu, objRcp("RcpNo"), "Air", objRow("TrxDate"), objRow("RefNumber"), "Vietnamese Dong" _
                                                               , "VENDOR PAYABLE (VND)", objRow("InvAmt"), strMemo, strJournalCreditLineEntityRefFullName _
                                                               , "INTERNAL DEBT:AIR HAN", "TVHAN [68612]")
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                Else
                    intResult = CreateAopQueueJournalEntry(strBu, objRcp("RcpNo"), "Air", objRow("TrxDate"), objRow("RefNumber"), "Vietnamese Dong" _
                                                               , "INTERNAL DEBT:AIR HAN", objRow("InvAmt"), strMemo, "TVHAN [68612]" _
                                                               , "VENDOR PAYABLE (VND)", strJournalCreditLineEntityRefFullName)
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                End If

            Case "TVSGN", "GDSSGN"
                Dim tblTourCodes As DataTable = GetTourCodeTableByRcp(objRcp("RcpNo"))

                If tblTourCodes.Rows.Count > 0 Then
                    strMemo = objRow("Tkno")
                    For Each objToourCodeRow As DataRow In tblTourCodes.Rows
                        strMemo = strMemo & " " & objToourCodeRow("TourCode")
                    Next

                    If objRow("SRV") = "S" Then
                        intResult = CreateAopQueueCheck(strBu, objRcp("RcpNo"), "Air", "CASH AIR", "AL TRANSVIET 2020 [10015]", objRow("RefNumber"), objRow("TrxDate") _
                                            , objRow("InvAmt"), "Vietnamese Dong", strMemo, "VENDOR PAYABLE (VND)")
                        If intResult = 0 Then
                            Return False
                        Else
                            lstQueueRecIds.Add(intResult.ToString)
                        End If
                    Else
                        intResult = CreateAopQueueJournalEntry(strBu, objRcp("RcpNo"), "Air", objRow("TrxDate"), objRow("RefNumber"), "Vietnamese Dong" _
                                                               , "VENDOR PAYABLE (VND)", objRow("InvAmt"), strMemo, "AL TRANSVIET 2020 [10015]" _
                                                               , "CASH AIR", "AL TRANSVIET 2020 [10015]")
                        If intResult = 0 Then
                            Return False
                        Else
                            lstQueueRecIds.Add(intResult.ToString)
                        End If
                    End If
                End If
        End Select

        'import KickBack, MiscFee
        strQuerry = "select r.CustId, r.RcpNo,R.Srv" _
                & ",(select sum(KickBackAmt) from Tkt where RcpId=r.RecId and Status<>'XX') as KickBackAmt" _
                & ",(select sum(MiscFeeAmt) from Tkt where RcpId=r.RecId and Status<>'XX') as MiscFeeAmt" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & ",l.CustShortName" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID  ORDER BY TKNO For Xml path('') ) AS Tkno" _
                & " ,AOPListID as CustAOPid" _
                & " from Rcp r" _
                & " left join CustomerList l on l.Recid=r.CustId" _
                & " where r.status='OK' and r.Counter='TVS' and RcpNo='" & objRcp("RcpNo") _
                & "' order by r.FstUpdate"

        tblKickBack = GetDataTable(strQuerry)

        If tblKickBack.Rows.Count > 0 Then
            objRow = tblKickBack.Rows(0)
            If objRow("KickBackAmt") > 0 Then
                If objRow("SRV") = "S" Then
                    strMemo = objRow("Tkno") & " RFD MU"
                    intResult = CreateAopQueueCreditMemo(objRow("CustAOPid"), strBu, "CUSTOMER RECEIVABLE (VND)", objRow("TrxDate") _
                                                      , objRow("RefNumber"), objRow("Tkno"), objRow("KickBackAmt"), 0 _
                                                      , strMemo, "REVENUE", "Air", "REVENUE")
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                ElseIf objRow("SRV") = "R" Then
                    strMemo = objRow("Tkno") & " THU MU"
                    intResult = CreateAopQueueInvoice(myStaff.City, 0, "I", "Air", strBu, objRcp("RcpNo"), objRow("KickBackAmt"), 0, objRow("AOPListid") _
                                                          , objRow("TrxDate"), objRow("RefNumber"), strMemo,,)
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                End If
            End If
            If objRow("MiscFeeAmt") > 0 Then
                Dim strMiscFeeVendorAop = "8000181B-1648546687"
                If objRow("SRV") = "S" Then
                    intResult = CreateAopQueueBill(myStaff.City, strMiscFeeVendorAop, objRow("CustAOPid"), strBu, objRow("TrxDate") _
                                                                         , objRow("RefNumber"), "COST", objRow("MiscFeeAmt"), 0 _
                                                                         , strMemo, "Air", objRcp("RcpNo"), "VND", "")
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If

                ElseIf objRow("SRV") = "R" Then
                    intResult = CreateAopQueueVendorCredit(strMiscFeeVendorAop, objRow("CustAopId"), strBu, "VENDOR PAYABLE (VND)" _
                                              , objRow("TrxDate"), objRow("RefNumber"), "COST", objRow("MiscFeeAmt"), 0, strMemo, "Air", objRcp("RcpNo"))
                    If intResult = 0 Then
                        Return False
                    Else
                        lstQueueRecIds.Add(intResult.ToString)
                    End If
                End If

            End If
        End If

        'IMPORT BILL
        If blnNoImportBill Then GoTo NoImportBill
        If objRow("CustShortName") <> "TVHAN" Then
            strQuerry = "select r.Vendor,c.CustShortName, r.RcpNo,R.Srv" _
                & " ,(select top 1 DOI from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0) AS DOI" _
                & " ,((select sum((NetToAL+Tax)+Charge*t.Qty) from tkt t where t.Status<>'XX' and t.StatusAL<>'XX' and t.RCPID=r.RecID and t.Qty<>0)" _
                & " - (select isnull(sum(Amount),0) from FOP f where f.Status='OK' and f.RCPID=r.RecID and f.FOP='EXC'))*r.Roe  AS BillAmt" _
                & ",substring(r.RcpNo,1,6)+ substring(r.RcpNo,9,4) as RefNumber" _
                & ",CONVERT(VARCHAR,r.FstUpdate,23) as TrxDate" _
                & " ,(select distinct t.DocType+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS DocType" _
                & " ,(select t.Tkno+'/' from tkt t where t.Status<>'XX' and t.RCPID=r.RecID For Xml path('') ) AS Tkno" _
                & ",'' AS TourCode,'' as DepDate,'' as TspCust,'' as TspCustAopId,'' as TspClass" _
                & ",v.AOPListID as VendorAopId,c.AOPListID as CustAopId,r.CustId,r.Currency as RcpCur,v.Cur as VenCur,r.TtlDue " _
                & " from Rcp r" _
                & " left join CustomerList c on c.Recid=r.CustId" _
                & " left join Vendor v on v.Recid=r.VendorId" _
                & " where r.status='OK' and r.Srv<>'V' and r.Counter='TVS' and r.RcpNo='" & objRcp("RcpNo") & "'" _
                & " and r.RecId not in (Select RcpId from tkt where DocType='AHC')"

            tblBill = GetDataTable(strQuerry)
            objRow = tblBill.Rows(0)

            If IsDBNull(objRow("VendorAopId")) Then
                MsgBox("You must ask PQT to update AOPListID for the following Vendor: " & objRow("Vendor"))
                Return False
            End If
            If objRow("CustAopId") = "" Then
                MsgBox("You must ask PQT to update AOPListID for the following Customer: " & objRow("CustShortName"))
                Return False
            End If

            objRow("Tkno") = Mid(objRow("tkno"), 1, Len(objRow("Tkno")) - 1)
            strMemo = objRow("Tkno")

            If IsDBNull(objRow("BillAmt")) Then
                GoTo NoImportBill    've XX doi voi Hang
            End If
            Dim strBillCur As String = "VND"
            Dim decBillAmt As Decimal = objRow("BillAmt")

            Select Case objRow("Vendor")
                Case "AK TK", "6E INDIGO USD"
                    strBillCur = "USD"
                    decBillAmt = objRow("TtlDue")
            End Select

            If objRow("SRV") = "R" Then
                intResult = CreateAopQueueVendorCredit(objRow("VendorAopId"), objRow("CustAopId"), strBu, "VENDOR PAYABLE (" & strBillCur & ")" _
                                  , objRow("TrxDate"), objRow("RefNumber"), "COST", decBillAmt, 0, strMemo, "Air", objRcp("RcpNo"))
                If intResult = 0 Then
                    Return False
                Else
                    lstQueueRecIds.Add(intResult.ToString)
                End If

            ElseIf objRow("BillAmt") > 0 Then       'bo qua ve zero value
                Dim strDueDate As String = String.Empty
                Select Case objRow("Vendor")
                    Case "BSP", "BSP AERTICKET-37314944"
                        If objRow("DocType").ToString.Contains("ETK") Or objRow("DocType").ToString.Contains("EMD") _
                                                Or objRow("DocType").ToString.Contains("MCO") Then
                            strDueDate = GetDueDate4AopBsp(objRow("DOI"))
                        End If
                    Case "VN", "VN DEB"
                        If objRow("DocType").ToString.Contains("ETK") Or objRow("DocType").ToString.Contains("EMD") _
                                                Or objRow("DocType").ToString.Contains("MCO") Then
                            strDueDate = GetDueDate4AopNonBsp(objRow("DOI"))
                        End If
                    Case "QH TK"
                        strDueDate = Format(objRow("DOI"), "yyyy-MM-dd")
                End Select

                intResult = CreateAopQueueBill(myStaff.City, objRow("VendorAopId"), objRow("CustAOPid"), strBu, objRow("TrxDate") _
                                                             , objRow("RefNumber"), "COST", decBillAmt, 0 _
                                                             , strMemo, "Air", objRcp("RcpNo"), strBillCur, strDueDate)
                If intResult = 0 Then
                    Return False
                Else
                    lstQueueRecIds.Add(intResult.ToString)
                End If

            End If
        End If


NoImportBill:
        ReDim arrQueueRecIds(0 To lstQueueRecIds.Count - 1)
        lstQueueRecIds.CopyTo(arrQueueRecIds)
        Return ExecuteNonQuerry("update AopQueue Set Status='OK' where LinkId in (" & Strings.Join(arrQueueRecIds, ",") & ")", Conn)


    End Function
    Public Function CreateAopQueueNonAir(strTcode As String) As Boolean

        Dim strQuerry As String
        'Dim objRow("TrxDate") As String = String.Empty
        Dim intQueueRecId As Integer
        Dim tblInvoice As DataTable
        Dim tblBill As DataTable
        Dim strBu As String = "CTS-NONAIR"
        Dim intResult As Integer
        Dim strTrxDate As String

        'Tao Invoice
        strQuerry = "select t.CustId, t.TCode,t.BillingBy" _
                & ",(select sum(ttltopax) from DuToan_Item where status='ok' and DuToanID=t.RecID ) as InvAmt" _
                & ",(select isnull(sum(ttltopax),0) from DuToan_Item where status='ok' and DuToanID=t.RecID and service='merchant fee') as MerchantFee" _
                & ",Right(tcode,11) as RefNumber" _
                & ",Sdate as TrxDate" _
                & ",t.CustShortName,AOPListID,r.CustType" _
                & " from DuToan_Tour t" _
                & " left join ShortenCustomerName s on t.CustId=s.intVal1" _
                & " left join CustomerList l on t.CustId=l.RecId" _
                & " left join Rcp r on t.RcpId=r.RecId" _
                & " where t.status='RR' and t.Tcode='" & strTcode & "'"

        '& " left join CustomerList l on l.Recid=t.CustId" _
        tblInvoice = GetDataTable(strQuerry, Conn)
        If tblInvoice.Rows.Count = 0 Then
            MsgBox("Invalide Tcode:" & strTcode)
            Return False
        End If


        For Each objRow As DataRow In tblInvoice.Rows
            'Khong import giao dich nam 2021
            If objRow("TrxDate") < "01 Jan 2002" Then
                Continue For
            End If

            If objRow("AOPListID") = "" Then
                MsgBox("Cần báo Khanhnm nhập khách hàng này vào AOP để tránh lỗi:" & objRow("CustShortName"))
                Return False
            End If
            strTrxDate = "{d'" & Format(objRow("TrxDate"), "yyyy-MM-dd") & "'}"

            If objRow("InvAmt") > 0 Then
                strQuerry = "INSERT INTO InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                    & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                    & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & objRow("AOPListid") & "','" & strBu & "','CUSTOMER RECEIVABLE (VND)'," _
                    & strTrxDate & ",'" & objRow("RefNumber") & "','" & objRow("Tcode") & "','" & objRow("Tcode") _
                    & "'," & objRow("InvAmt") & ",'" & objRow("RefNumber") & "',1)"
                intQueueRecId = CreateAopQueueRecord(0, "I", "NonAir", strBu, strTcode, strQuerry, True, "ODBC")

                If intQueueRecId = 0 Then
                    MsgBox("Không tạo được bản ghi AOP Invoice cho Tcode " & strTcode)
                    Return False
                ElseIf Not ExecuteNonQuerry("Update AopQueue Set LinkId=" & intQueueRecId & " where RecId=" & intQueueRecId, Conn) Then
                    MsgBox("Không tạo được bản ghi AOP Invoice cho Tcode " & strTcode)
                    Return False
                End If

                If objRow("MerchantFee") <> 0 Then
                    strQuerry = "INSERT INTO InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                        & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                        & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & objRow("AOPListid") & "','" & strBu & "','CUSTOMER RECEIVABLE (VND)'," _
                        & strTrxDate & ",'" & objRow("RefNumber") & "','" & objRow("Tcode") & "','" & objRow("Tcode") _
                        & "'," & 0 - objRow("MerchantFee") & ",'" & objRow("RefNumber") & "',1)"
                    If CreateAopQueueRecord(intQueueRecId, "I", "NonAir", strBu, strTcode, strQuerry, False, "ODBC") = 0 Then
                        MsgBox("Không tạo được bản ghi AOP Invoice cho Tcode " & strTcode)
                        Return False
                    End If
                End If

                strQuerry = "INSERT INTO Invoice (CustomerRefListID,ClassRefFullName, ARAccountRefFullName, TxnDate, RefNumber" _
                        & ", Memo) VALUES ('" & objRow("AOPListid") & "','" & strBu & "','CUSTOMER RECEIVABLE (VND)'," _
                        & strTrxDate & ",'" & objRow("RefNumber") & "','" & objRow("Tcode") & "')"

                If CreateAopQueueRecord(intQueueRecId, "I", "NonAir", strBu, strTcode, strQuerry, False, "ODBC") = 0 Then
                    MsgBox("Không tạo được bản ghi AOP Invoice cho Tcode " & strTcode)
                    Return False
                End If
            Else    'hoan
                intResult = CreateAopQueueCreditMemo(objRow("AOPListid"), strBu, "CUSTOMER RECEIVABLE (VND)", objRow("TrxDate") _
                                                         , objRow("RefNumber"), strTcode, Math.Abs(objRow("InvAmt")) _
                                        , objRow("MerchantFee"), strTcode, strTcode, "NonAir", strTcode)
                If intResult = 0 Then
                    MsgBox("Không tạo được bản ghi AOP CreditMemo cho Tcode " & strTcode)
                    Return False
                Else

                End If
            End If

        Next

        'Tao Bill
        strQuerry = "select t.CustId, t.TCode,i.Vendor,i.VendorId" _
                & ",l.Cur as VendorCur,Ccurr as ItemCur, Sum(TtlToVendor) as TtlToVendor , Sum(TtlToVendor_NguyenTe) as TtlToVendor_NguyenTe " _
                & ", Right(tcode,11) as RefNumber" _
                & ",SDate as TrxDate" _
                & ",t.CustShortName,l.AOPListID,c.AOPListID as CustAOPListID" _
                & " from DuToan_Tour t" _
                & " left join ShortenCustomerName s on t.CustId=s.intVal1" _
                & " left join Dutoan_item i on i.DutoanId=t.RecId" _
                & " left join Vendor l on l.Recid=i.VendorId" _
                & " left join CustomerList c on t.CustId=c.RecId" _
                & " where t.status='RR' and t.Tcode='" & strTcode _
                & "' and i.Status='OK' and i.VendorId<>2 and l.FOP='PSP'" _
                & " group by t.CustId,t.CustShortName, t.TCode,i.Vendor,i.VendorId,Sdate,strVal2,strVal3,l.AOPListID,l.Cur,i.Ccurr,c.AOPListID" _
                & " order by t.tcode"
        tblBill = GetDataTable(strQuerry, Conn)

        For Each objRow As DataRow In tblBill.Rows
            If objRow("TrxDate") < "01 Jan 2002" Then
                Continue For
            End If
            Dim strAccountName As String = String.Empty
            Dim decBillAmout As Decimal = 0
            strTrxDate = "{d'" & Format(objRow("TrxDate"), "yyyy-MM-dd") & "'}"
            If objRow("AOPListid") = "" Then
                MsgBox("Không tạo được bản ghi AOP Bill cho Trxcode " & strTcode & " vi chua import Vendor " & objRow("Vendor"))
                Return False
            ElseIf objRow("VendorCur") = "" Then
                MsgBox("Không tạo được bản ghi AOP Bill cho Trxcode " & strTcode & " vi chua co Cur cho Vendor " & objRow("Vendor"))
                Return False
            End If
            Select Case objRow("VendorCur")
                Case "USD", "EUR", "JPY"
                    strAccountName = "VENDOR PAYABLE (" & objRow("VendorCur") & ")"
                    decBillAmout = objRow("TtlToVendor_NguyenTe")
                Case Else
                    strAccountName = "VENDOR PAYABLE (VND)"
                    decBillAmout = objRow("TtlToVendor")
            End Select
            If decBillAmout > 0 Then
                strQuerry = "INSERT INTO BillItemLine (ItemLineBillableStatus, ItemLineCustomerRefListID" _
                & ",VendorRefListID,ItemLineClassRefFullName,APAccountRefFullName" _
                & ",TxnDate,RefNumber,ItemLineItemRefFullName, ItemLineDesc" _
                & ",ItemLineAmount,memo,FQSaveToCache) VALUES ('Billable','" & objRow("CustAOPListid") & "','" & objRow("AOPListid") _
                & "','" & strBu & "','" & strAccountName & "'," _
                & strTrxDate & ",'" & objRow("RefNumber") & "','" & objRow("Tcode") & "','" & objRow("Tcode") _
                & "'," & decBillAmout & ",'" & objRow("RefNumber") & "',1)"

                intQueueRecId = CreateAopQueueRecord(0, "B", "NonAir", strBu, strTcode, strQuerry, True, "ODBC")

                If intQueueRecId = 0 Then
                    MsgBox("Không tạo được bản ghi AOP Bill cho Trxcode " & strTcode)
                    Return False
                ElseIf Not ExecuteNonQuerry("Update AopQueue Set LinkId=" & intQueueRecId & " where RecId=" & intQueueRecId, Conn) Then
                    MsgBox("Không tạo được bản ghi AOP Bill cho Tcode " & strTcode)
                    Return False
                End If

                strQuerry = "INSERT INTO Bill (VendorRefListID, APAccountRefFullName, TxnDate, RefNumber" _
                        & ", Memo) VALUES ('" & objRow("AOPListid") & "','" & strAccountName & "'," _
                        & strTrxDate & ",'" & objRow("RefNumber") & "','" & objRow("Tcode") & "')"
                If CreateAopQueueRecord(intQueueRecId, "B", "NonAir", strBu, strTcode, strQuerry, False, "ODBC") = 0 Then
                    MsgBox("Không tạo được bản ghi AOP Bill cho Tcode " & strTcode)
                    Return False
                End If
            ElseIf decBillAmout < 0 Then    'hoan
                intResult = CreateAopQueueVendorCredit(objRow("AOPListID"), objRow("CustAOPListID"), strBu, strAccountName _
                                              , objRow("TrxDate"), objRow("RefNumber"), strTcode, Math.Abs(decBillAmout), 0, strTcode, "NonAir", strTcode)
                If intResult = 0 Then
                    MsgBox("Không tạo được bản ghi AOP VendorCredit cho Tcode " & strTcode)
                    Return False
                End If
            End If

        Next
        Return True
    End Function
    Public Function CreateAopQueueBill(strCity As String, strVendorAOPListid As String, strCustAopId As String, strBu As String _
                                    , strTrxDate As String _
                                   , strRefNumber As String, strItemName As String, decBillAmt As Decimal, decMerchantFee As Decimal _
                                   , strMemo As String, strProd As String, strTrxCode As String, strCur As String, strDueDate As String _
                                   , Optional strCounter As String = "") As Integer

        Dim strQuerry As String
        Dim intQueueRecId As Integer
        Dim intNewRecId As Integer
        Dim strAPAccountRefFullName As String = ""

        If Not strTrxDate.Contains("{d") Then
            strTrxDate = "{d'" & Format(CDate(strTrxDate), "yyyy-MM-dd") & "'}"
        End If

        If strCity = "SGN" Then
            strAPAccountRefFullName = "VENDOR PAYABLE (" & strCur & ")"
        ElseIf strCity = "HAN" Then
            strAPAccountRefFullName = "Phai tra nguoi ban"
        End If

        If strDueDate = "" Then
            strQuerry = "INSERT INTO BillItemLine (ItemLineBillableStatus, ItemLineCustomerRefListID" _
                & ",VendorRefListID,ItemLineClassRefFullName,APAccountRefFullName" _
                & ",TxnDate,RefNumber,ItemLineItemRefFullName, ItemLineDesc" _
                & ",ItemLineAmount,memo,FQSaveToCache) VALUES ('Billable','" & strCustAopId & "','" & strVendorAOPListid _
                & "','" & strBu & "','" & strAPAccountRefFullName & "'," _
                & strTrxDate & ",'" & strRefNumber & "','" & strItemName & "','" & strMemo _
                & "'," & decBillAmt & ",'" & strMemo & "',1)"
        Else
            If Not strDueDate.Contains("{d") Then
                strDueDate = "{d'" & Format(CDate(strDueDate), "yyyy-MM-dd") & "'}"
            End If
            If strRefNumber.Length > 11 Then
                strRefNumber = Mid(strRefNumber, strRefNumber.Length - 10)
            End If

            strQuerry = "INSERT INTO BillItemLine (ItemLineBillableStatus, ItemLineCustomerRefListID" _
                & ",VendorRefListID,ItemLineClassRefFullName,APAccountRefFullName" _
                & ",TxnDate,RefNumber,ItemLineItemRefFullName, ItemLineDesc" _
                & ",ItemLineAmount,memo,DueDate,FQSaveToCache) VALUES ('Billable','" & strCustAopId & "','" & strVendorAOPListid _
                & "','" & strBu & "','" & strAPAccountRefFullName & "'," _
                & strTrxDate & ",'" & strRefNumber & "','" & strItemName & "','" & strMemo _
                & "'," & decBillAmt & ",'" & strMemo & "'," & strDueDate & ",1)"
        End If

        intQueueRecId = CreateAopQueueRecord(intQueueRecId, "B", strProd, strBu, strTrxCode, strQuerry, True, "ODBC", strCounter _
                                             , strRefNumber, decBillAmt, strMemo)

        If intQueueRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP Bill cho Trxcode " & strMemo)
            Return 0
        ElseIf Not ExecuteNonQuerry("Update AopQueue Set LinkId=" & intQueueRecId & " where RecId=" _
                                    & intQueueRecId, Conn) Then
            MsgBox("Không tạo được bản ghi AOP Bill cho TrxCode " & strMemo)
            Return 0
        End If

        strQuerry = "INSERT INTO Bill (VendorRefListID, APAccountRefFullName, TxnDate, RefNumber" _
                & ", Memo) VALUES ('" & strVendorAOPListid & "','" & strAPAccountRefFullName & "'," _
                & strTrxDate & ",'" & strRefNumber & "','" & strMemo & "')"

        intNewRecId = CreateAopQueueRecord(intQueueRecId, "B", strProd, strBu, strTrxCode, strQuerry, True, "ODBC" _
                                           ,, strRefNumber, decBillAmt, strMemo)

        If intNewRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP Bill cho TrxCode " & strMemo)
            Return 0
        End If

        Return intQueueRecId
    End Function
    Public Function CreateAopQueueBillPayment(strTrxCode As String, strRefNumber As String, strCounter As String _
            , strAppliedTxnCode As String, intBillQueueId As Integer, strMemo As String) As Boolean
        Dim strQuerry As String
        Dim tblUnc As DataTable
        Dim strBU As String
        Dim strTrxDate As String = ""
        Dim intPmtId As Integer
        Dim lstQueueIDs As New List(Of String)

        intPmtId = ScalarToInt("UNC_Payments", "top 1 RecId", "Status='OK' and RefNo='" & strTrxCode & "'")

        If intPmtId = 0 Then
            MsgBox("UNC " & strTrxCode & " " & strMemo & " had been deleted. PayBill transaction will NOT be created!")
            ExecuteNonQuerry("Update AopQueue set TxnId='" & strAppliedTxnCode & "' where Status not in ('OK','XX') and RefNumber='" & strRefNumber & "'", Conn)
            Return True
        End If


        If strCounter = "N-A" Then
            strQuerry = " select t.Tcode,u.RefNo,v2.Cur,u.Amount,p.VND" _
                               & " ,v.Shortname as Payee,v.RecId as PayeeId,v.AopTravelListId as PayeeAopId" _
                               & ",v2.AopTravelListId as PayerAopId,m.Val as BankAccountAopId" _
                               & ",u.RecId as UncId, u.LstUpdate as TrxDate" _
                               & " from UNC_Payments u " _
                               & " left join (select PmtId,Tcode,DutoanId,VendorID,sum(VND) as VND from DuToan_Pmt where status='ok' and PmtId=" & intPmtId _
                               & " group by PmtId,DutoanId,TCode,VendorID) p on u.RecId=p.PmtId and p.Tcode='" & strMemo & "'" _
                               & " left join Dutoan_Tour t on p.DutoanId=t.RecID " _
                               & " left join Vendor v on u.PayeeAccountId=v.RecID " _
                               & " left join Vendor v2 on u.PayerAccountId=v2.RecID " _
                               & " left join Misc m on m.CAT='AopBankAccountId2' and m.Val1='RAS' and m.intVal=v2.RecId " _
                               & " where u.RecId=" & intPmtId & " And u.Status='OK'"
        Else
            strQuerry = "select p.Tourcode as Tcode,u.RefNo,v2.Cur,u.Amount,u.Amount as VND" _
                    & ",v.Shortname as Payee,v.RecId as PayeeId,v.AopTravelListId as PayeeAopId" _
                    & ",v2.AopTravelListId as PayerAopId,m.Val as BankAccountAopId" _
                    & ",u.LstUpdate as TrxDate,TspClass,u.RecId as UncId, u.LstUpdate as TrxDate" _
                    & " from UNC_Payments u" _
                    & " left join Vendor v on u.PayeeAccountId=v.RecID" _
                    & " Left join Vendor v2 on u.PayerAccountId=v2.RecID" _
                    & " Left join (select DNTTID,TourCode,sum(Amount) as Amount from TourPmt where Status='OK' and TourCode='" & strMemo _
                    & "' group by TourCode,DNTTID) p on u.RecID=p.DNTTID" _
                    & " left join TourInfo i ON i.TourCode=p.TourCode" _
                    & " left join Misc m on m.CAT='AopBankAccountId2' and m.Val1='OPS' and m.intVal=v2.RecId" _
                    & " where u.recId=" & intPmtId & " And u.Status='OK'"
        End If

        tblUnc = GetDataTable(strQuerry, Conn)

        If tblUnc.Rows.Count = 0 Then
            MsgBox("Requested details had been deleted!")
            Return False
        Else
            Dim lstQueueRecIds As New List(Of String)
            Dim intResult As Integer

            For Each objRow As DataRow In tblUnc.Rows
                If IsDBNull(objRow("BankAccountAopId")) Then
                    MsgBox("Unmatched Bank Account Id for " & objRow("RefNo"))
                    Continue For
                End If
                Select Case strCounter
                    Case "N-A"
                        If objRow("VND") < 0 Then
                            Continue For
                        End If
                        strBU = "CTS-NONAIR"
                    Case Else
                        strBU = objRow("TspClass")
                End Select
                strTrxDate = Format(objRow("TrxDate"), "yyyy-MM-dd")
                intResult = CreateAopQueueBillPaymentCheck(objRow("PayeeAopId"), objRow("BankAccountAopId"), strBU, strAppliedTxnCode, strRefNumber _
                        , objRow("VND"), strTrxDate, "NonAir", strRefNumber, objRow("Cur"), objRow("UncId"), strMemo, myStaff.City)

                lstQueueIDs.Add(intResult)
            Next
        End If
        If lstQueueIDs.Count = 0 Then
            Return False
        Else
            Dim arrQueueIDs(0 To lstQueueIDs.Count - 1) As String
            lstQueueIDs.CopyTo(arrQueueIDs)
            Dim lstQuerries As New List(Of String)
            lstQuerries.Add("Update AopQueue set Status='OK'where LinkId in(" & Join(arrQueueIDs, ",") & ")")
            lstQuerries.Add("Update AopQueue set TxnId='" & strAppliedTxnCode & "' where RecId=" & intBillQueueId)
            If UpdateListOfQuerries(lstQuerries, Conn) Then
                Return True
            Else
                Return False
            End If
        End If

        Return True

    End Function
    Public Function CreateAopQueueBillPaymentCheck(strPayeeAopId As String _
                                    , strBankAccountAopId As String, strBu As String _
                                    , strAppliedTxnId As String, strRefNumber As String _
                                    , decAmt As Decimal, strTrxDate As String _
                                    , strProd As String, strTrxCode As String, strCur As String _
                                    , intUncId As Integer, strMemo As String _
                                    , strCity As String) As Integer

        Dim strQuerry As String
        Dim intQueueRecId As Integer
        'Dim intNewRecId As Integer
        Dim strApName As String = ""

        Select Case strCity
            Case "SGN"
                strApName = "VENDOR PAYABLE (" & strCur & ")"
            Case "HAN"
                strApName = "PHAI TRA NGUOI BAN"
        End Select
        If Not strTrxDate.Contains("{d") Then
            strTrxDate = "{d'" & Format(CDate(strTrxDate), "yyyy-MM-dd") & "'}"
        End If
        If strRefNumber.Length > 11 Then
            strRefNumber = Mid(strRefNumber, strRefNumber.Length - 10)
        End If

        strQuerry = "INSERT INTO BillPaymentCheckLine (PayeeEntityRefListID, BankAccountRefListID, AppliedToTxnTxnID" _
                & ",RefNumber, TxnDate,memo,APAccountRefFullName,AppliedToTxnPaymentAmount, FQSaveToCache) Values ('" & strPayeeAopId _
                & "','" & strBankAccountAopId & "','" & strAppliedTxnId & "','" & strRefNumber & "'," & strTrxDate _
                & ",'" & strMemo & "','" & strApName & "'," & decAmt & ",0)"

        intQueueRecId = CreateAopQueueRecord(intQueueRecId, "P", strProd, strBu, strTrxCode, strQuerry, True, "ODBC",, strRefNumber, decAmt)

        If intQueueRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP BillPaymentCheck cho Trxcode " & strTrxCode)
            Return 0
        ElseIf Not ExecuteNonQuerry("Update AopQueue Set LinkId=" & intQueueRecId & " where RecId=" & intQueueRecId, Conn) Then
            MsgBox("Không tạo được bản ghi AOP BillPaymentCheck cho TrxCode " & strTrxCode)
            Return 0
        End If

        Return intQueueRecId
    End Function
    Private Function CreateAopQueueVendorCredit(strVendorAOPListid As String, strCustAopId As String _
                                   , strBu As String, strAccountName As String, strTrxDate As String _
                                   , strRefNumber As String, strItemName As String, decInvAmt As Decimal, decMerchantFee As Decimal _
                                   , strMemo As String, strProd As String, strTrxCode As String) As Integer
        Dim strQuerry As String
        Dim intQueueRecId As Integer
        Dim intNewRecId As Integer

        If Not strTrxDate.Contains("{d") Then
            strTrxDate = "{d'" & Format(CDate(strTrxDate), "yyyy-MM-dd") & "'}"
        End If
        '^_^20220805 mark by 7643 -b-
        'strQuerry = "INSERT INTO VendorCreditItemLine (VendorRefListID,APAccountRefFullName" _
        '                & ",TxnDate,RefNumber,Memo,ItemLineItemRefFullName,ItemLineDesc" _
        '                & ",ItemLineAmount,ItemLineCustomerRefListID,ItemLineClassRefFullName" _
        '                & ",ItemLineBillableStatus,FQSaveToCache) VALUES ('" _
        '                & strVendorAOPListid & "','" & strAccountName & "'," & strTrxDate & ",'" & strRefNumber & "','" & strMemo _
        '                & "','" & strItemName & "','" & strMemo _
        '                & "'," & decInvAmt & ",'" & strCustAopId & "','" & strBu & "','Billable',1)"
        '^_^20220805 mark by 7643 -e-
        '^_^20220805 modi by 7643 -b-
        strQuerry = "INSERT INTO VendorCreditItemLine (VendorRefListID,APAccountRefFullName" _
                        & ",TxnDate,RefNumber,Memo,ItemLineItemRefFullName,ItemLineDesc" _
                        & ",ItemLineAmount,ItemLineCustomerRefListID,ItemLineClassRefFullName" _
                        & ",ItemLineBillableStatus,FQSaveToCache) VALUES ('" _
                        & strVendorAOPListid & "','" & strAccountName & "'," & strTrxDate & ",'" & strRefNumber & "','" & strMemo _
                        & "','" & strItemName & "','" & strMemo _
                        & "'," & Math.Abs(decInvAmt) & ",'" & strCustAopId & "','" & strBu & "','Billable',1)"
        '^_^20220805 modi by 7643 -e-
        intQueueRecId = CreateAopQueueRecord(0, "B", strProd, strBu, strTrxCode, strQuerry, True, "ODBC",, strRefNumber)

        If intQueueRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP VendorCredit cho TrxCode " & strTrxCode)
            Return 0
        ElseIf Not ExecuteNonQuerry("Update AopQueue Set LinkId=" & intQueueRecId & " where RecId=" & intQueueRecId, Conn) Then
            MsgBox("Không tạo được bản ghi AOP VendorCredit cho TrxCode " & strTrxCode)
            Return 0
        End If

        strQuerry = "INSERT INTO VendorCredit (VendorRefListID,APAccountRefFullName, TxnDate, RefNumber" _
                            & ", Memo) VALUES ('" & strVendorAOPListid & "','" & strAccountName & "'," _
                            & strTrxDate & ",'" & strRefNumber & "','" & strMemo & "')"

        intNewRecId = CreateAopQueueRecord(intQueueRecId, "B", strProd, strBu, strTrxCode, strQuerry, True, "ODBC",, strRefNumber)

        If intNewRecId = 0 Then
            Return 0
        End If

        Return intQueueRecId

    End Function
    Private Function CreateAopQueueCreditMemo(strAOPListid As String, strBU As String, strAccountName As String, strInvDate As String _
                                   , strRefNumber As String, strDesc As String, decInvAmt As Decimal, decMerchantFee As Decimal _
                                   , strMemo As String, strTrxCode As String, strProd As String _
                                   , strItemName As String) As Integer
        Dim intQueueRecId As Integer
        Dim intNewRecId As Integer
        Dim strQuerry As String
        'Dim strItemName As String
        'If strProd = "Air" Then
        '    strItemName = "REVENUE"
        'Else
        '    strItemName = strDesc
        'End If

        If Not strInvDate.Contains("{d") Then
            strInvDate = "{d'" & Format(CDate(strInvDate), "yyyy-MM-dd") & "'}"
        End If
        '^_^20220805 mark by 7643 -b-
        'strQuerry = "INSERT INTO CreditMemoLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
        '                & ",TxnDate,RefNumber,CreditMemoLineItemRefFullName, CreditMemoLineDesc" _
        '                & ",CreditMemoLineAmount,memo,FQSaveToCache) VALUES ('" & strAOPListid _
        '                & "','" & strBU & "','" & strAccountName & "'," _
        '                & strInvDate & ",'" & strRefNumber & "','" & strItemName & "','" & strDesc _
        '                & "'," & decInvAmt & ",'" & strMemo & "',1)"
        '^_^20220805 mark by 7643 -e-
        '^_^20220805 modi by 7643 -b-
        strQuerry = "INSERT INTO CreditMemoLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                        & ",TxnDate,RefNumber,CreditMemoLineItemRefFullName, CreditMemoLineDesc" _
                        & ",CreditMemoLineAmount,memo,FQSaveToCache) VALUES ('" & strAOPListid _
                        & "','" & strBU & "','" & strAccountName & "'," _
                        & strInvDate & ",'" & strRefNumber & "','" & strItemName & "','" & strDesc _
                        & "'," & Math.Abs(decInvAmt) & ",'" & strMemo & "',1)"
        '^_^20220805 modi by 7643 -e-

        intQueueRecId = CreateAopQueueRecord(intQueueRecId, "C", strProd, strBU, strTrxCode, strQuerry, True, "ODBC",, strRefNumber)

        If intQueueRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP CreditMemo cho TrxCode " & strTrxCode)
            Return 0
        ElseIf Not ExecuteNonQuerry("Update AopQueue Set LinkId=" & intQueueRecId & " where RecId=" & intQueueRecId, Conn) Then
            MsgBox("Không tạo được bản ghi AOP CreditMemo cho Tcode " & strTrxCode)
            Return 0
        End If

        If decMerchantFee <> 0 Then
            '^_^20220805 mark by 7643 -b-
            'strQuerry = "INSERT INTO CreditMemoLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
            '            & ",TxnDate,RefNumber,CreditMemoLineItemRefFullName, CreditMemoLineDesc" _
            '            & ",CreditMemoLineAmount,memo,FQSaveToCache) VALUES ('" & strAOPListid & "','" & strBU & "','" & strAccountName & "'," _
            '            & strInvDate & ",'" & strRefNumber & "','" & strItemName & "','" & strDesc _
            '            & "'," & 0 - decMerchantFee & ",'" & strMemo & "',1)"
            '^_^20220805 mark by 7643 -e-
            '^_^20220805 modi by 7643 -b-
            strQuerry = "INSERT INTO CreditMemoLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                        & ",TxnDate,RefNumber,CreditMemoLineItemRefFullName, CreditMemoLineDesc" _
                        & ",CreditMemoLineAmount,memo,FQSaveToCache) VALUES ('" & strAOPListid & "','" & strBU & "','" & strAccountName & "'," _
                        & strInvDate & ",'" & strRefNumber & "','" & strItemName & "','" & strDesc _
                        & "'," & Math.Abs(0 - decMerchantFee) & ",'" & strMemo & "',1)"
            '^_^20220805 modi by 7643 -e-
            intNewRecId = CreateAopQueueRecord(intQueueRecId, "I", strProd, strBU, strTrxCode, strQuerry, True, "ODBC",, strRefNumber)
            If intNewRecId = 0 Then
                MsgBox("Không tạo được bản ghi AOP CreditMemo Phí cà thẻ cho TrxCode" & strTrxCode)
                Return 0
            End If
        End If

        strQuerry = "INSERT INTO CreditMemo (CustomerRefListID,ClassRefFullName, ARAccountRefFullName, TxnDate, RefNumber" _
                            & ", Memo) VALUES ('" & strAOPListid & "','" & strBU & "','" & strAccountName & "'," _
                            & strInvDate & ",'" & strRefNumber & "','" & strMemo & "')"

        intNewRecId = CreateAopQueueRecord(intQueueRecId, "C", strProd, strBU, strTrxCode, strQuerry, True, "ODBC",, strRefNumber)
        If intNewRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP CreditMemo cho TrxCode " & strTrxCode)
            Return 0
        End If
        Return intQueueRecId
    End Function
    Private Function CreateAopQueueCheck(strBU As String, strTrxCode As String, strProd As String _
                                    , strAccountRefFullName As String, strPayeeEntityRefFullName As String _
                                    , strRefNumber As String, strTrxDate As String, decAmount As Decimal _
                                    , strCurrencyRefFullName As String, strMemo As String _
                                    , strExpenseLineAccountRefFullName As String) As Integer
        Dim intQueueRecId As Integer
        Dim intNewRecId As Integer
        Dim strQuerry As String

        If Not strTrxDate.Contains("{d") Then
            strTrxDate = "{d'" & Format(CDate(strTrxDate), "yyyy-MM-dd") & "'}"
        End If

        strQuerry = "INSERT INTO [CheckExpenseLine] (AccountRefFullName,PayeeEntityRefFullName,RefNumber" _
                        & ",TxnDate,Memo,ExpenseLineAccountRefFullName,ExpenseLineAmount" _
                        & " ,ExpenseLineMemo,ExpenseLineCustomerRefFullName,IsToBePrinted,FQSaveToCache) VALUES ('" _
                        & strAccountRefFullName & "','" & strPayeeEntityRefFullName & "','" & strRefNumber & "'," _
                        & strTrxDate & ",'" & strMemo & "','" & strExpenseLineAccountRefFullName _
                        & "'," & decAmount & ",'" & strMemo & "','" & strPayeeEntityRefFullName & "',0,1)"


        intQueueRecId = CreateAopQueueRecord(intQueueRecId, "I", strProd, strBU, strTrxCode, strQuerry, True, "ODBC",, strRefNumber)
        If intQueueRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP CheckExpenseLine cho TrxCode " & strTrxCode)
            Return 0
        ElseIf Not ExecuteNonQuerry("Update AopQueue Set LinkId=" & intQueueRecId & " where RecId=" & intQueueRecId, Conn) Then
            MsgBox("Không tạo được bản ghi AOP Check cho TrxCode " & strTrxCode)
            Return 0
        End If

        strQuerry = "INSERT INTO [Check] (AccountRefFullName,PayeeEntityRefFullName,RefNumber" _
                        & ",TxnDate, Memo,IsToBePrinted) VALUES ('" & strAccountRefFullName & "','" & strPayeeEntityRefFullName & "','" & strRefNumber & "'," _
                        & strTrxDate & ",'" & strMemo & "',0)"

        intNewRecId = CreateAopQueueRecord(intQueueRecId, "I", strProd, strBU, strTrxCode, strQuerry, True, "ODBC",, strRefNumber)
        If intNewRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP Check cho TrxCode " & strTrxCode)
            Return 0
        End If


        Return intQueueRecId
    End Function

    Private Function CreateAopQueueJournalEntry(strBU As String, strTrxCode As String, strProd As String _
                                    , strTrxDate As String, strRefNumber As String, strCurrencyRefFullName As String _
                                    , strJournalCreditLineAccountRefFullName As String, decAmount As Decimal _
                                    , strMemo As String, strJournalCreditLineEntityRefFullName As String _
                                    , strJournalDebitLineAccountRefFullName As String _
                                    , strJournalDebitLineEntityRefFullName As String _
                                    , Optional strJournalDebitLineAccountRefId As String = "") As Integer
        Dim intQueueRecId As Integer
        Dim intNewRecId As Integer
        Dim strQuerry As String

        If Not strTrxDate.Contains("{d") Then
            strTrxDate = "{d'" & Format(CDate(strTrxDate), "yyyy-MM-dd") & "'}"
        End If

        strQuerry = "INSERT INTO JournalEntryCreditLine (TxnDate,RefNumber,CurrencyRefFullName" _
                        & ",JournalCreditLineAccountRefFullName,JournalCreditLineAmount,JournalCreditLineMemo, JournalCreditLineEntityRefFullName,FQSaveToCache) VALUES (" _
                        & strTrxDate & ",'" & strRefNumber & "','" & strCurrencyRefFullName & "','" & strJournalCreditLineAccountRefFullName _
                        & "'," & decAmount & ",'" & strMemo & "','" & strJournalCreditLineEntityRefFullName & "',1)"

        intQueueRecId = CreateAopQueueRecord(intQueueRecId, "I", strProd, strBU, strTrxCode, strQuerry, True, "ODBC",, strRefNumber)

        If intQueueRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP JournalEntryCreditLine cho TrxCode " & strTrxCode)
            Return 0
        ElseIf Not ExecuteNonQuerry("Update AopQueue Set LinkId=" & intQueueRecId & " where RecId=" & intQueueRecId, Conn) Then
            MsgBox("Không tạo được bản ghi AOP JournalEntryCreditLine cho TrxCode " & strTrxCode)
            Return 0
        End If

        If strJournalDebitLineAccountRefId = "" Then
            strQuerry = "INSERT INTO JournalEntryDebitLine (TxnDate,RefNumber,CurrencyRefFullName" _
                        & ",JournalDebitLineAccountRefFullName,JournalDebitLineAmount,JournalDebitLineMemo, JournalDebitLineEntityRefFullName,FQSaveToCache) VALUES (" _
                        & strTrxDate & ",'" & strRefNumber & "','" & strCurrencyRefFullName & "','" & strJournalDebitLineAccountRefFullName _
                        & "'," & decAmount & ",'" & strMemo & "','" & strJournalDebitLineEntityRefFullName & "',0)"
        Else
            strQuerry = "INSERT INTO JournalEntryDebitLine (TxnDate,RefNumber,CurrencyRefFullName" _
                        & ",JournalDebitLineAccountRefListID,JournalDebitLineAmount,JournalDebitLineMemo,FQSaveToCache) VALUES (" _
                        & strTrxDate & ",'" & strRefNumber & "','" & strCurrencyRefFullName & "','" & strJournalDebitLineAccountRefId _
                        & "'," & decAmount & ",'" & strMemo & "',0)"
        End If


        intNewRecId = CreateAopQueueRecord(intQueueRecId, "I", strProd, strBU, strTrxCode, strQuerry, True, "ODBC",, strRefNumber)
        If intNewRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP JournalEntryDebitLine cho TrxCode " & strTrxCode)
            Return 0
        End If


        Return intQueueRecId
    End Function
    Private Function CreateAopQueueJournalEntryByID(strBU As String, strTrxCode As String, strProd As String _
                                    , strTrxDate As String, strRefNumber As String, strCurrencyRefFullName As String _
                                    , strJournalCreditLineAccountRefFullName As String, decAmount As Decimal _
                                    , strMemo As String, strJournalCreditLineEntityRefListID As String _
                                    , strJournalDebitLineAccountRefListId As String, strCounter As String) As Integer
        Dim intQueueRecId As Integer
        Dim intNewRecId As Integer
        Dim strQuerry As String

        If Not strTrxDate.Contains("{d") Then
            strTrxDate = "{d'" & Format(CDate(strTrxDate), "yyyy-MM-dd") & "'}"
        End If

        strQuerry = "INSERT INTO JournalEntryCreditLine (TxnDate,RefNumber,CurrencyRefFullName" _
                        & ",JournalCreditLineAccountRefFullName,JournalCreditLineAmount,JournalCreditLineMemo, JournalCreditLineEntityRefListID,FQSaveToCache) VALUES (" _
                        & strTrxDate & ",'" & strRefNumber & "','" & strCurrencyRefFullName & "','" & strJournalCreditLineAccountRefFullName _
                        & "'," & decAmount & ",'" & strMemo & "','" & strJournalCreditLineEntityRefListID & "',1)"


        intQueueRecId = CreateAopQueueRecord(intQueueRecId, "I", strProd, strBU, strTrxCode, strQuerry, True, "ODBC", strCounter, strRefNumber, decAmount, strMemo)

        If intQueueRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP JournalEntryCreditLine cho TrxCode " & strTrxCode)
            Return 0
        ElseIf Not ExecuteNonQuerry("Update AopQueue Set LinkId=" & intQueueRecId & " where RecId=" & intQueueRecId, Conn) Then
            MsgBox("Không tạo được bản ghi AOP JournalEntryCreditLine cho TrxCode " & strTrxCode)
            Return 0
        End If


        strQuerry = "INSERT INTO JournalEntryDebitLine (TxnDate,RefNumber,CurrencyRefFullName" _
                        & ",JournalDebitLineAccountRefListID,JournalDebitLineAmount,JournalDebitLineMemo,FQSaveToCache) VALUES (" _
                        & strTrxDate & ",'" & strRefNumber & "','" & strCurrencyRefFullName & "','" & strJournalDebitLineAccountRefListId _
                        & "'," & decAmount & ",'" & strMemo & "',0)"

        intNewRecId = CreateAopQueueRecord(intQueueRecId, "I", strProd, strBU, strTrxCode, strQuerry, True, "ODBC", strCounter, strRefNumber, decAmount, strMemo)
        If intNewRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP JournalEntryDebitLine cho TrxCode " & strTrxCode)
            Return 0
        End If


        Return intQueueRecId
    End Function
    Public Function CreateAopQueueInvoice(strCity As String, intLinkId As Integer, strBillInvoice As String, strProd As String, strBU As String, strTrxCode As String _
                                         , decInvAmout As Decimal, decMerchantFee As Decimal _
                                         , strCustIdAop As String, strTrxDate As String, strRefNumber As String, strMemo As String _
                                         , Optional strCur As String = "", Optional strQuerryType As String = "ODBC") As Integer
        Dim intQueueRecId As Integer
        Dim strQuerry As String
        Dim strARAccountRefFullName As String = ""
        Dim strInvoiceLineItemRefFullName As String = ""
        If strCur = "" Then
            strCur = "VND"
        End If
        If Not strTrxDate.Contains("{d") Then
            strTrxDate = "{d'" & Format(CDate(strTrxDate), "yyyy-MM-dd") & "'}"
        End If

        If strCity = "SGN" Then
            strARAccountRefFullName = "CUSTOMER RECEIVABLE (" & strCur & ")"
            strInvoiceLineItemRefFullName = "REVENUE"
        ElseIf strCity = "HAN" Then
            strARAccountRefFullName = "PHAI THU KHACH HANG"
            strInvoiceLineItemRefFullName = "REVENUE:Revenue"
        End If

        Select Case strProd
            Case "NonAir"
                strQuerry = "INSERT INTO InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                    & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                    & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & strCustIdAop & "','" & strBU & "','" & strARAccountRefFullName & "'," _
                    & strTrxDate & ",'" & strRefNumber & "','" & strTrxCode & "','" & strTrxCode _
                    & "'," & decInvAmout & ",'" & strMemo & "',1)"
            Case "Air"
                strQuerry = "INSERT INTO InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                    & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                    & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & strCustIdAop & "','" & strBU & "','" & strARAccountRefFullName & "'," _
                    & strTrxDate & ",'" & strRefNumber & "','" & strInvoiceLineItemRefFullName _
                    & "','" & strMemo _
                    & "'," & decInvAmout & ",'" & strMemo & "',1)"

        End Select

        intQueueRecId = CreateAopQueueRecord(intQueueRecId, "I", strProd, strBU, strTrxCode, strQuerry, True, strQuerryType,, strRefNumber)

        If intQueueRecId = 0 Then
            MsgBox("Không tạo được bản ghi AOP Invoice cho TrxCode " & strTrxCode)
            Return 0
        ElseIf Not ExecuteNonQuerry("Update AopQueue Set LinkId=" & intQueueRecId & " where RecId=" & intQueueRecId, Conn) Then
            MsgBox("Không tạo được bản ghi AOP Invoice cho Tcode " & strTrxCode)
            Return 0
        End If

        If decMerchantFee <> 0 Then
            strQuerry = "INSERT INTO InvoiceLine (CustomerRefListID,ClassRefFullName,ARAccountRefFullName" _
                & ",TxnDate,RefNumber,InvoiceLineItemRefFullName, InvoiceLineDesc" _
                & ",InvoiceLineAmount,memo,FQSaveToCache) VALUES ('" & strCustIdAop & "','" & strBU & "','" & strARAccountRefFullName & "'," _
                & strTrxDate & ",'" & strRefNumber & "','" & strInvoiceLineItemRefFullName & "','" & strMemo _
                & "'," & 0 - decMerchantFee & ",'" & strMemo & "',1)"
            If CreateAopQueueRecord(intQueueRecId, "I", strProd, strBU, strTrxCode, strQuerry, False) = 0 Then
                MsgBox("Không tạo được bản ghi AOP Invoice cho TrxCode " & strTrxCode)
                Return 0
            End If
        End If

        strQuerry = "INSERT INTO Invoice (CustomerRefListID,ClassRefFullName, ARAccountRefFullName, TxnDate, RefNumber" _
                & ", Memo) VALUES ('" & strCustIdAop & "','" & strBU & "','" & strARAccountRefFullName & "'," _
                & strTrxDate & ",'" & strRefNumber & "','" & strMemo & "')"

        If CreateAopQueueRecord(intQueueRecId, "I", strProd, strBU, strTrxCode, strQuerry, False, strQuerryType,, strRefNumber) = 0 Then
            MsgBox("Không tạo được bản ghi AOP Invoice cho TrxCode " & strTrxCode)
            Return 0
        End If
        Return intQueueRecId
    End Function

    Public Function CreateAopQueueRecord(intLinkId As Integer, strBillInvoice As String, strProd As String, strBU As String, strTrxCode As String _
                                         , strQuerry As String, blnGetRecId As Boolean, Optional strQuerryType As String = "ODBC" _
                                         , Optional strCounter As String = "", Optional strRefNumber As String = "" _
                                         , Optional decAmount As Decimal = 0, Optional strMemo As String = "") As Integer
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand

        cmd.CommandText = "insert Into AopQueue (LinkId,B_I, Prod,BU, TrxCode, Querry, FstUser,QuerryType,Counter,RefNumber,Amount,Memo) values (" _
                        & intLinkId & ",'" & strBillInvoice _
                        & "','" & strProd & "','" & strBU & "','" & strTrxCode & "',@Querry,'" & myStaff.SICode & "','" & strQuerryType _
                        & "','" & strCounter & "','" & strRefNumber & "'," & decAmount & ",'" & Mid(strMemo, 1, 64) & "')"

        If blnGetRecId Then
            cmd.CommandText = cmd.CommandText & ";SELECT SCOPE_IDENTITY() AS [RecID]"
        End If
        cmd.Parameters.Add("@Querry", SqlDbType.NVarChar).Value = strQuerry
        Try
            If blnGetRecId Then
                Return cmd.ExecuteScalar
            Else
                Return cmd.ExecuteNonQuery
            End If
        Catch ex As Exception
            Append2TextFile("SQL error:" & ex.Message & vbCrLf & cmd.CommandText & vbCrLf & strQuerry)
            Return 0
        End Try
    End Function

    Public Function AopQueueExist(strTrxCode As String) As Boolean
        Dim strAopQueueStatus As String = ScalarToString("AopQueue", "Status", "Status in ('OK','RR') and TrxCode='" & strTrxCode & "'")

        Select Case strAopQueueStatus
            Case "OK"
                MsgBox("AopQueue exist! Please wait for few more hours!")
                Return True
            Case "RR"
                MsgBox("AopQueue imported in the past for " & strTrxCode)
                Return True
        End Select
        Return False
    End Function
    Public Function GetTourCodeTable(strTourCode) As DataTable
        Dim strQuerry As String = "select t.TourCode, " _
            & " Case When TourType='MICE' then 'MICE_'+t.city else 'IB_SGN' end as TspCust, " _
            & " Case When TourType='MICE' then 'MICE' else 'IB' end as TspClass, SDate " _
            & " from FLX.dbo.TOS_TourCode t join FLX.dbo.tbl_Requests r On t.TourCode=r.TourCode And r.Status='RR'" _
            & " where t.status='OK' and t.city in ('SGN', 'DAD') and t.TourCode='" & strTourCode & "'" _
            & " union " _
            & " select t.TourCode, 'TOURDESK_'+t.city, 'TD', SDate " _
            & " from FLX.dbo.TOS_TourCode t join FLX.dbo.Departure d On t.TourCode=d.Departure And d.Status<>'XX'" _
            & " where t.status='OK' and t.city in ('SGN', 'DAD') and t.TourCode='" & strTourCode & "'"
        Return GetDataTable(strQuerry, Conn_Web)
    End Function
    Public Function GetTourCodeTableByRcp(strRcp As String) As DataTable
        Dim strTourCode As String = GetColumnValuesAsString("FOP", "Document", " where Status='OK' and FOP<>'EXC' and RCPNO='" & strRcp & "'", ",")
        strTourCode = strTourCode.Replace(",", "','")
        Dim strQuerry As String = "Select t.TourCode, " _
            & " Case When TourType='MICE' then 'MICE_'+t.city else 'IB_SGN' end as TspCust, " _
            & " Case When TourType='MICE' then 'MICE' else 'IB' end as TspClass, SDate " _
            & " from FLX.dbo.TOS_TourCode t join FLX.dbo.tbl_Requests r On t.TourCode=r.TourCode And r.Status='RR'" _
            & " where t.status='OK' and t.city in ('SGN', 'DAD','HAN') and t.TourCode in ('" & strTourCode & "')" _
            & " union " _
            & " select t.TourCode, 'TOURDESK_'+t.city, 'TD', SDate " _
            & " from FLX.dbo.TOS_TourCode t join FLX.dbo.Departure d On t.TourCode=d.Departure And d.Status<>'XX'" _
            & " where t.status='OK' and t.city in ('SGN', 'DAD','HAN') and t.TourCode in ('" & strTourCode & "')"
        Return GetDataTable(strQuerry, Conn_Web)
    End Function
    Public Function GetAopRecordNameTVS(strSrv As String, strGrp As String, strCustShortName As String) As String
        If strSrv = "R" Then
            Return "CreditMemo"
        ElseIf strGrp = "FIT" Then
            Return "Invoice"
        ElseIf strGrp = "FIT" Then
            Return "Invoice"
        ElseIf strCustShortName = "TV_SGN" Then
            Return "Invoice"
        ElseIf strCustShortName = "GDS_SGN" Then
            Return "Invoice"
        Else
            Return "Deposit"
        End If
    End Function
    Public Function GetBookerEmail(intCustId As Integer, strBookerName As String) As String
        Return ScalarToString("cwt.dbo.cwt_bookers", "Top 1 Email", "Status<>'xx' and CustId=" & intCustId & " and BookerName='" & strBookerName & "'")

    End Function
    Public Function GetChargeFromChargeDetails(strChargeCode As String, strChargeDetails As String) As String
        Dim decResult As Decimal = 0

        If strChargeDetails <> "" Then
            Dim arrCharges() As String = strChargeDetails.Split("|")
            Dim i As Integer
            For i = 0 To arrCharges.Length - 1
                If Mid(arrCharges(i), 1, strChargeCode.Length) = strChargeCode Then
                    Return Mid(arrCharges(i), strChargeCode.Length + 1)
                End If
            Next
        End If
        Return ""
    End Function
    Public Function GetDates4Quarter(intYear As Integer, intQuarter As Integer, ByRef dteFrom As Date, dteTo As Date) As Boolean
        If Not IsNumeric(intYear) Or Not (IsNumeric(intQuarter)) Then Return False
        Select Case intQuarter
            Case 1
                dteFrom = DateSerial(intYear, 1, 1)
                dteTo = DateSerial(intYear, 3, 31)
            Case 2
                dteFrom = DateSerial(intYear, 4, 1)
                dteTo = DateSerial(intYear, 6, 30)
            Case 3
                dteFrom = DateSerial(intYear, 7, 1)
                dteTo = DateSerial(intYear, 9, 30)
            Case 4
                dteFrom = DateSerial(intYear, 10, 1)
                dteTo = DateSerial(intYear, 12, 31)
        End Select
        dteTo = dteTo.AddHours(23).AddMinutes(59)
        Return True
    End Function
    Public Function CountSegments(strRtg As String) As Integer
        Dim intSegCount As Integer
        strRtg = strRtg.Replace(" ", "")
        intSegCount = strRtg.Length \ 5
        If strRtg.Contains("\\") Then
            intSegCount = intSegCount - strRtg.Split("\\").Length - 1
        End If
        Return intSegCount
    End Function
    Public Function CheckFormatUnc(strRefNo As String) As Boolean
        Dim arrBreaks As String() = Split(strRefNo, "-")
        If arrBreaks.Length <> 2 Then
            If strRefNo.StartsWith("TV") AndAlso IsNumeric(Mid(strRefNo, 3)) Then
                Return True
            Else
                Return False
            End If

        ElseIf Not IsNumeric(arrBreaks(1)) Then
            Return False
        ElseIf Not IsNumeric(Mid(arrBreaks(0), 4)) Then
            Return False
        End If
        Return True
    End Function
    Public Function ImportAopUNC(intPaymentId As Integer, strApp As String, strCounter As String) As Boolean
        Dim strQuerry As String = String.Empty
        Dim strBu As String
        Dim tblUnc As DataTable
        Dim strTrxDate As String = ""
        Dim lstQueueIDs As New List(Of String)
        Dim decBillAmt As Decimal
        Dim strRefNumber As String = ""
        Select Case strApp
            Case "RAS"
                If myStaff.City = "HAN" Then
                    Return True '24Feb23: chưa làm import cho UNC tạo từ RASHAN
                End If
                If strCounter = "N-A" Then
                    strQuerry = " select t.Tcode,u.RefNo,v2.Cur,u.RequestedAmt as Amount,p.VND" _
                    & " ,v.Shortname as Vendor,v.RecId as VendorId,v.AopTravelListId as VendorAopId" _
                    & ",t.CustShortName,l.AopTravelListId as CustAopId,t.Sdate as TrxDate" _
                    & " from UNC_Payments u " _
                    & " left join (select PmtId,Tcode,DutoanId,VendorID,sum(VND) as VND from DuToan_Pmt where status='ok' and PmtID=" & intPaymentId _
                    & " group by PmtId,DutoanId,TCode,VendorID) p on u.RecId=p.PmtId " _
                    & " left join Dutoan_Tour t on p.DutoanId=t.RecID " _
                    & " left join Vendor v on u.PayeeAccountId=v.RecID " _
                    & " left join Vendor v2 on u.PayerAccountId=v2.RecID " _
                    & " left join CustomerList l on l.RecId=t.CustID " _
                    & " where u.RecId=" & intPaymentId & " And u.Status='OK' "

                Else
                    Return False
                End If

            Case "OPS"
                '^_^20230228 mark by 7643 -b-
                'strQuerry = "select p.Tourcode as Tcode,u.RefNo,v2.Cur,u.RequestedAmt as Amount,p.Amount as VND" _
                '    & ",v.Shortname as Vendor,v.RecId as VendorId,v.AopTravelListId as VendorAopId" _
                '    & ",c.CustShortName,c.AOPTravelListID as CustAopId,v.Cat,v.Cur as PayeeCur" _
                '    & ",Sdate as TrxDate,TspClass" _
                '    & " from UNC_Payments u" _
                '    & " left join Vendor v on u.PayeeAccountId=v.RecID" _
                '    & " Left join Vendor v2 on u.PayerAccountId=v2.RecID" _
                '    & " Left join (select DNTTid,TourCode, sum(amount) as amount from TourPmt where DNTTID=" & intPaymentId _
                '    & " group by DNTTID,TourCode) p ON u.RecID=p.DNTTID" _
                '    & " left join TourInfo i ON i.TourCode=p.TourCode" _
                '    & " left join Customer c On i.TspCust=c.CustShortName And c.Status='ok'" _
                '    & " where u.RecId=" & intPaymentId & " And u.Status='OK'"
                '^_^20230228 mark by 7643 -e-
                '^_^20230228 modi by 7643 -b-
                strQuerry = "select p.Tourcode as Tcode,u.RefNo,v2.Cur,u.RequestedAmt as Amount,p.Amount as VND" _
                    & ",v.Shortname as Vendor,v.RecId as VendorId,v.AopTravelListId as VendorAopId" _
                    & ",c.CustShortName,c.AOPTravelListID as CustAopId,v.Cat,v.Cur as PayeeCur" _
                    & ",Sdate as TrxDate,TspClass" _
                    & " from UNC_Payments u" _
                    & " left join Vendor v on u.PayeeAccountId=v.RecID" _
                    & " Left join Vendor v2 on u.PayerAccountId=v2.RecID" _
                    & " Left join (select DNTTid,TourCode, sum(amount) as amount from TourPmt where City='" & myStaff.City & "' and DNTTID=" & intPaymentId _
                    & " group by DNTTID,TourCode) p ON u.RecID=p.DNTTID" _
                    & " left join TourInfo i ON i.TourCode=p.TourCode" _
                    & " left join Customer c On i.TspCust=c.CustShortName And c.Status='ok'" _
                    & " where u.RecId=" & intPaymentId & " And u.Status='OK'"
                '^_^20230228 modi by 7643 -e-
        End Select
        tblUnc = GetDataTable(strQuerry)

        If tblUnc.Rows.Count = 0 Then
            MsgBox("Requested details had been deleted!")
            Return False
        Else
            Dim lstQueueRecIds As New List(Of String)
            Dim intResult As Integer
            For Each objRow As DataRow In tblUnc.Rows
                If IsDBNull(objRow("VendorAopId")) Then
                    MsgBox("Không có AOP ID tương ứng cho vendor " & objRow("VendorId"))
                    Return False
                End If
                If strApp = "OPS" AndAlso objRow("CAT").trim = "GD" AndAlso objRow("Cur") <> objRow("PayeeCur") Then
                    Continue For
                End If
                Select Case strApp
                    Case "RAS"
                        strBu = "CTS-NONAIR"
                        'strTrxDate = ExtractDateInTcode(Mid(objRow("Tcode"), objRow("CustShortName").ToString.Length + 1))

                    Case "OPS"
                        If IsDBNull(objRow("TspClass")) Then
                            MsgBox("Đề nghị báo cho người lập trình RAS là không tìm thấy TSP Class tương ứng cho " & objRow("RefNo"))
                            Return False
                        Else
                            strBu = objRow("TspClass")

                        End If
                End Select
                strTrxDate = Format(objRow("TrxDate"), "yyyy-MM-dd")
                'Khong import giao dich trong nam 2021
                If CDate(strTrxDate) < CDate("01 Jan 22") Then
                    Continue For

                End If

                If objRow("VND") < 0 Then
                    Dim strPayerBankAccountId As String
                    strPayerBankAccountId = ScalarToString("MISC", "VAL", "CAT='AopBankAccountId2' and Status='OK'" _
                                                            & " and VAL1='" & strApp & "' and intVal=" _
                                                            & " (select PayerAccountId from UNC_Payments where RecId=" & intPaymentId & ")")

                    intResult = CreateAopQueueJournalEntryByID("", objRow("RefNo"), "UNC", strTrxDate, strRefNumber, "Vietnamese Dong" _
                                                               , "VENDOR PAYABLE (VND)", Math.Abs(objRow("VND")), objRow("Tcode"), objRow("VendorAopId") _
                                                               , strPayerBankAccountId, strCounter)
                    lstQueueIDs.Add(intResult)

                ElseIf tblUnc.Rows.Count = 1 Then
                    decBillAmt = objRow("Amount")
                Else
                    decBillAmt = objRow("VND")
                End If
                If objRow("RefNo").ToString.Length > 11 Then
                    strRefNumber = Mid(objRow("RefNo"), objRow("RefNo").Length - 10)
                Else
                    strRefNumber = objRow("RefNo")
                End If
                intResult = CreateAopQueueBill(myStaff.City, objRow("VendorAopId"), objRow("CustAopId"), strBu, strTrxDate, strRefNumber _
                    , objRow("Tcode"), decBillAmt, 0, objRow("Tcode"), "UNC", objRow("RefNo"), objRow("Cur"), "", strCounter)

                lstQueueIDs.Add(intResult)
            Next
        End If
        If lstQueueIDs.Count = 0 Then
            Return False
        Else
            Dim arrQueueIDs(0 To lstQueueIDs.Count - 1) As String
            lstQueueIDs.CopyTo(arrQueueIDs)
            If ExecuteNonQuerry("Update AopQueue Set Status='OK' where LinkId in(" & Join(arrQueueIDs, ",") & ")", Conn) Then
                Return True
            Else
                Return False
            End If
        End If

    End Function
    Public Function ImportAopUNC4AdjustedAmt(intPaymentId As Integer, strApp As String, strCounter As String _
                                 , decAdjustedAmt As Decimal) As Boolean
        Dim strQuerry As String = String.Empty
        Dim strBu As String
        Dim tblUnc As DataTable
        Dim strTrxDate As String
        Dim lstQueueIDs As New List(Of String)
        Dim strRefNumber As String
        Dim strMemo As String = String.Empty
        Select Case strApp
            Case "RAS"
                If strCounter = "N-A" Then
                    strQuerry = " select top 1 t.Tcode,u.RefNo,v2.Cur,u.Amount,p.VND" _
                    & " ,v.Shortname as Vendor,v.RecId as VendorId,v.AopTravelListId as VendorAopId" _
                    & ",m.VAL as PayerBankAccountId" _
                    & " from UNC_Payments u " _
                    & " left join (select PmtId,Tcode,DutoanId,VendorID,sum(VND) as VND from DuToan_Pmt where status='ok' and PmtID=" & intPaymentId _
                    & " group by PmtId,DutoanId,TCode,VendorID) p on u.RecId=p.PmtId " _
                    & " left join Dutoan_Tour t on p.DutoanId=t.RecID " _
                    & " left join Vendor v on u.PayeeAccountId=v.RecID " _
                    & " left join Vendor v2 on u.PayerAccountId=v2.RecID " _
                    & " left join Misc m on m.CAT='AopBankAccountId2' and m.Val1='RAS' and m.intVal=v2.RecId " _
                    & " where u.RecId=" & intPaymentId & " And u.Status='OK' "
                Else
                    Return False
                End If

            Case "OPS"
                strQuerry = "select p.Tourcode as Tcode,u.RefNo,v2.Cur,u.Amount,p.Amount as VND" _
                    & ",v.Shortname as Vendor,v.RecId as VendorId,v.AopTravelListId as VendorAopId" _
                    & ",c.CustShortName,c.AOPTravelListID as CustAopId" _
                    & ",Sdate as TrxDate,TspClass" _
                    & " from UNC_Payments u" _
                    & " left join Vendor v on u.PayeeAccountId=v.RecID" _
                    & " Left join Vendor v2 on u.PayerAccountId=v2.RecID" _
                    & " Left join (select DNTTid,TourCode, sum(amount) as amount from TourPmt where DNTTID=" & intPaymentId _
                    & " group by DNTTID,TourCode) p ON u.RecID=p.DNTTID" _
                    & " left join TourInfo i ON i.TourCode=p.TourCode" _
                    & " left join Customer c On i.TspCust=c.CustShortName And c.Status='ok'" _
                    & " where u.RecId=" & intPaymentId & " And u.Status='OK'"
        End Select
        tblUnc = GetDataTable(strQuerry)

        If tblUnc.Rows.Count = 0 Then
            MsgBox("Requested details had been deleted!")
            Return False
        Else
            Dim lstQueueRecIds As New List(Of String)
            Dim intResult As Integer
            For Each objRow As DataRow In tblUnc.Rows
                If objRow("VND") < 0 Then
                    Continue For
                End If
                Select Case strApp
                    Case "RAS"
                        strTrxDate = Format(Now, "yyyy-MM-dd")
                        strMemo = objRow("Tcode")
                    Case "OPS"
                        strTrxDate = Format(objRow("TrxDate"), "yyyy-MM-dd")
                End Select

                If objRow("RefNo").ToString.Length > 11 Then
                    strRefNumber = Mid(objRow("RefNo"), objRow("RefNo").Length - 10)
                Else
                    strRefNumber = objRow("RefNo")
                End If
                intResult = CreateAopQueueJournalEntryByID("", objRow("RefNo"), "UNC", strTrxDate, strRefNumber, "Vietnamese Dong" _
                                                               , "VENDOR PAYABLE (VND)", decAdjustedAmt, strMemo, objRow("VendorAopId") _
                                                               , objRow("PayerBankAccountId"), strCounter)

                lstQueueIDs.Add(intResult)
                Exit For
            Next
        End If
        If lstQueueIDs.Count = 0 Then
            Return False
        Else
            Dim arrQueueIDs(0 To lstQueueIDs.Count - 1) As String
            lstQueueIDs.CopyTo(arrQueueIDs)
            If ExecuteNonQuerry("Update AopQueue set Status='OK' where LinkId in(" & Join(arrQueueIDs, ",") & ")", Conn) Then
                Return True
            Else
                Return False
            End If
        End If

    End Function
    Public Function GetFirstValueInBracket(strCheckedString As String) As String
        Dim separators As Char() = New Char() {"("c, ")"c}
        Dim arrResults As String() = strCheckedString.Split(separators)
        If arrResults.Length > 1 Then
            Return arrResults(1)
        Else
            Return ""
        End If

    End Function
    Public Function CalcVatRate(decVatableAmt As Decimal, decVat As Decimal, strSupplierName As String) As Integer
        Dim intVatRate As Integer
        Select Case decVatableAmt
            Case 0
                If ScalarToString("Supplier", "Address_CountryCode", "Status='OK' and FullName='" & strSupplierName & "'") = "VN" Then
                    intVatRate = 10
                Else
                    intVatRate = 0
                End If
            Case Else
                intVatRate = Math.Round(decVat * 100 / decVatableAmt)
        End Select
        Return intVatRate
    End Function
    Public Function CalcVatPctNearest(decVatableAmt As Decimal, decVat As Decimal) As Integer
        If decVat = 0 Then Return 0
        Return Math.Round(decVat * 100 / decVatableAmt)
    End Function
    Public Function DateTime2Text(ByVal dteInputDate As Date) As String
        'Purpose: chuyen dang date time  thanh DD-MMM-YYYY HH:NN:SS trong VB
        DateTime2Text = Format(dteInputDate, "dd-MMM-yyyy HH:mm:ss")
    End Function
    Public Function RemoveLastChr(ByVal strToBeCut As String, Optional ByVal BytNbrOfChar As Byte = 1) As String
        If Len(strToBeCut) = 0 Then
            RemoveLastChr = ""
            Exit Function
        End If
        RemoveLastChr = Left(strToBeCut, Len(strToBeCut) - BytNbrOfChar)
    End Function
    Public Function RemoveSpecialChrsWebField(ByVal strText As String) As String
        ' bo khoang trong chuoi nhung van giu 1 ky tu trang giua cac phan
        'bo xuong dong

        Dim rgSpace As New Regex("\s{2,}")

        strText = rgSpace.Replace(strText, " ")
        strText = Replace(strText, vbCrLf, "")
        strText = Replace(strText, "&nbsp;", "")
        strText = strText.Replace("#", "")
        strText = strText.Replace(Chr(160), "")

        Return strText.Trim
    End Function
    Public Function ReplaceSpecialChrsTcb(ByVal strText As String) As String

        strText = strText.Replace("_", " ")
        Return strText.Trim
    End Function
    Public Function Date2YYMMDD(ByVal dteDate) As String
        Date2YYMMDD = Format(dteDate, "yyMMdd")
    End Function
    Public Function ReplaceSpecialChar(strText As String) As String
        'Loai cac ky tu dac biet ma Global One khong chap nhan
        Dim i As Integer
        Dim stbResult As New System.Text.StringBuilder("")
        For i = 0 To strText.Length - 1
            Select Case Asc(strText.Chars(i))
                Case 150, 38
                    stbResult.Append(Chr(45))
                Case 160
                    stbResult.Append(Chr(32))
                Case Else
                    stbResult.Append(strText.Chars(i))
            End Select
        Next
        Return stbResult.ToString
    End Function
    Public Function FormatGoText(ByVal strText As String, ByVal intLength As Int16 _
                    , Optional ByVal blnNumeric As Boolean = False) As String
        If strText.Length > intLength Then
            strText = strText.Remove(intLength)
            Return strText
        ElseIf blnNumeric Then
            Return strText.PadLeft(intLength, "0")
        Else
            Return strText.PadRight(intLength)
        End If

    End Function
    Public Function GetRouting4Huntsman(arrDepApts() As String, arrArrApts() As String, arrCars() As String) As String
        Dim i As Integer
        Dim strRtg As String = ""

        For i = 0 To arrCars.Length - 1
            If strRtg = "" Then
                strRtg = arrDepApts(i) & "/" & arrArrApts(i)
            ElseIf arrCars(i) = "" Or arrCars(i) = "//" Then
                strRtg = strRtg & "/XXX/" & arrArrApts(i)
            Else
                strRtg = strRtg & "/" & arrArrApts(i)
            End If
        Next
        Return strRtg
    End Function
    Public Function ConverRbd4Hunstmant(strRBDs As String) As String
        Dim i As Integer
        Dim strResult As String = Mid(strRBDs, 1, 1)
        If strRBDs.Length > 1 Then
            For i = 2 To strRBDs.Length
                strResult = strResult & "/" & Mid(strRBDs, i, 1)
            Next
        End If
        Return strResult

    End Function
    Public Function GetFopNameByCode(strCode As String) As String
        Select Case strCode
            Case "IN"
                Return "Invoice"
            Case "CC"
                Return "Credit Card"
            Case "CA", "CS"
                Return "Cash"
            Case "MP"
                Return "Multi"
            Case Else
                Return "Misc"
        End Select
    End Function
    Public Function Convert2iBankDate(ByVal dteDate As Date) As String
        Return Format(dteDate, "dd/MM/yyyy")
    End Function

    Public Function LinkGoTravelDutoanId4Hotel() As Boolean
        'KHONG DUNG CHO REED MACKAY
        Dim strQuerry As String = "select TravelID,substring(VoucherNbr,9,10) as DutoanId" _
            & " from GO_Hotel h" _
            & " left join GO_Travel t on h.TravelID=t.RecID" _
            & " where SUBSTRING(VoucherNbr,1,8)='dutoanid'" _
            & " and t.DutoanId=0 and t.CustId in " _
            & " (Select CustId From CWT.DBO.GO_CompanyInfo1 where Status='OK' and TMC<>'RM')"

        Dim tblResult As DataTable = pobjTvcs.GetDataTable(strQuerry)

        For Each objRow As DataRow In tblResult.Rows
            pobjTvcs.ExecuteNonQuerry("Update Go_Travel set DutoanId=" & objRow("DutoanId") _
                                      & " where Recid=" & objRow("TravelId"))
        Next
        Return True
    End Function
    'Public Function RelinkTktAir(dteFromDate As Date, dteToDate As Date) As Boolean
    '    Dim strQuerry As String
    '    Dim lstQuerries As New List(Of String)
    '    Dim tblAir As DataTable
    '    Dim intNewTkid As Integer

    '    strQuerry = "select  t.RecID as TktId,air.RecID as AirId,t.Tkno,t.AL,t.Srv" _
    '                & " from GO_Air air" _
    '                & " left join ras12.dbo.tkt t on t.SRV=air.SRV and air.Carrier=t.AL " _
    '                & " and air.Tkno=t.ShortTkno" _
    '                & " where t.Qty<>0 And t.Status='xx'" _
    '                & " and t.RecId not in (select intVal from go_Misc where Cat='CheckedXxTkid')" _
    '                & " and  air.Tkid>0 and air.DOI between '" & CreateFromDate(dteFromDate) _
    '                & "' AND '" & CreateToDate(dteToDate) & "'"

    '    tblAir = pobjTvcs.GetDataTable(strQuerry)
    '    For Each objRow As DataRow In tblAir.Rows
    '        intNewTkid = pobjTvcs.GetScalarAsDecimal("Select top 1 RecId from ras12.dbo.tkt where Status<>'XX' and Tkno='" _
    '                    & objRow("Tkno") & "' and Srv='" & objRow("Srv") & "' and Recid>" & objRow("Tktid") _
    '                    & " order by RecId")

    '        If intNewTkid > 0 Then
    '            lstQuerries.Clear()
    '            lstQuerries.Add("Update GO_Air set Tkid=" & intNewTkid & " where RecId=" & objRow("AirId"))
    '            lstQuerries.Add("insert GO_Misc (Cat,intVal) values ('CheckedXxTkid'," & objRow("Tktid") & ")")
    '            If Not pobjTvcs.UpdateListQuerries(lstQuerries, False) Then
    '                MsgBox("Unable to Relink Tkt in RAS with GO_AIR. Tkno=" & objRow("Tkno"))
    '            End If
    '        End If
    '    Next

    '    Return True
    'End Function
    Public Function RelinkTktNonAir(dteFromDate As Date, dteToDate As Date) As Boolean
        'KHONG AP DUNG CHO REED MACKAY
        Dim strQuerry As String
        Dim lstQuerries As New List(Of String)
        Dim tblAir As DataTable
        Dim intNewTkid As Integer

        strQuerry = "select  t.RecID as TktId,n.RecID as NonAirId,m.RecId as MiscId,Tkno,t.Srv" _
                    & " from Ras12.dbo.dutoan_tour n" _
                    & " left join ras12.dbo.misc m on n.Recid=m.intVal and m.Cat='WzAir'" _
                    & " left join ras12.dbo.tkt t on m.intVal1=t.RecId" _
                    & " where t.Qty<>0 And t.Status='xx' " _
                    & " and  n.Status='RR' and n.lstUpdate between '" & CreateFromDate(dteFromDate) _
                    & "' AND '" & CreateToDate(dteToDate) & "' and n.CustId in " _
                    & " (Select CustId From CWT.DBO.GO_CompanyInfo1 where Status='OK' and TMC<>'RM')"

        tblAir = pobjTvcs.GetDataTable(strQuerry)
        For Each objRow As DataRow In tblAir.Rows
            intNewTkid = pobjTvcs.GetScalarAsDecimal("Select top 1 RecId from ras12.dbo.tkt where Status<>'XX' and Tkno='" _
                        & objRow("Tkno") & "' and Srv='" & objRow("Srv") & "' and Recid>" & objRow("Tktid") _
                        & " order by RecId")
            lstQuerries.Add("Update ras12.dbo.misc set intVal1=" & objRow("TktId") & " where RecId=" & objRow("MiscId"))
        Next

        If lstQuerries.Count > 0 Then
            If Not UpdateListOfQuerries(lstQuerries, Conn) Then
                MsgBox("Unable to Relink Tkt with Non Air in RAS")
                Return False
            End If
        End If

        Return True
    End Function
    'Public Function MapCdrHier(dteFromDate As Date, dteToDate As Date) As Boolean
    '    Dim tblMap As DataTable = pobjTvcs.GetDataTable("select * from GO_CdrHierMap where Status='OK' order by CMC")

    '    For Each objRow As DataRow In tblMap.Rows
    '        pobjTvcs.ExecuteNonQuerry("Update Go_travel set Hierachy" & objRow("Hier") _
    '                                  & "=Ref" & objRow("CDR") & " where DOI between'" _
    '                                  & CreateFromDate(dteFromDate) & "' and '" & CreateToDate(dteToDate) _
    '                                  & "' and CMC='" & objRow("CMC") & "'")
    '    Next
    'End Function
    Public Function LinkTktAir(dteFromDate As Date, dteToDate As Date) As Boolean
        'KHONG AP DUNG CHO REED MACKAY
        Dim strQuerry As String
        Dim lstQuerries As New List(Of String)
        Dim tblAir As DataTable

        strQuerry = "select  t.RecID as TktId,air.RecID as AirId" _
                    & " from GO_Air air" _
                    & " left join ras12.dbo.tkt t on t.SRV=air.SRV and air.Carrier=t.AL " _
                    & " and air.Tkno=replace(substring(t.TKNO,4,12),' ','')" _
                    & " left join ras12.dbo.rcp r on t.RcpId=r.RecId" _
                    & " where t.Qty<>0 And t.Status<>'xx' " _
                    & " and  air.Tkid=0 and air.DOI between '" & CreateFromDate(dteFromDate) _
                    & "' and '" & CreateToDate(dteToDate) & "' and r.CustId in " _
                    & " (Select CustId From CWT.DBO.GO_CompanyInfo1 where Status='OK' and TMC<>'RM')"

        tblAir = pobjTvcs.GetDataTable(strQuerry)
        For Each objRow As DataRow In tblAir.Rows
            lstQuerries.Add("Update GO_Air set Tkid=" & objRow("TktId") & " where RecId=" & objRow("AirId"))
        Next

        If Not pobjTvcs.UpdateListQuerries(lstQuerries, False) Then
            MsgBox("Unable to link Tkt in RAS with GO_AIR")
            Return False
        Else
            Return True
        End If
    End Function
    Public Function AdjustPaxName4GO(strPaxName As String) As String
        'Neu pax name bat dau bang ms, mrs, mr hoac miss thi bo no di

        Dim strPattern As String = "^(?i:mr(s{0,1})\s)|^(?i:ms\s)|^(?i:miss\s)"
        Dim rgTitle As New Regex(strPattern)
        Dim colMatch As MatchCollection

        strPaxName = strPaxName.Replace("'", "*")

        colMatch = rgTitle.Matches(strPaxName)

        If colMatch.Count > 0 Then
            Return Mid(strPaxName, colMatch(0).Length + 1)
        Else
            Return strPaxName
        End If

    End Function
    Public Function VatDiscountable(dteTrxDate As Date) As Boolean
        If dteTrxDate >= "01 NOV 21" AndAlso dteTrxDate <= "31 DEC 21 23:59" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetVatRatio(dteTrxDate As Date) As Decimal
        If dteTrxDate >= "01 NOV 21" AndAlso dteTrxDate <= "31 DEC 21 23:59" Then
            Return 1.07
        ElseIf dteTrxDate >= "01 FEB 22" AndAlso dteTrxDate <= "31 DEC 22 23:59" Then
            Return 1.08
        Else
            Return 1.1
        End If
    End Function

    Public Function GetVatPct(dteTrxDate As Date) As Decimal
        If dteTrxDate >= "01 NOV 21" AndAlso dteTrxDate <= "31 DEC 21 23:59" Then
            Return 7
        ElseIf dteTrxDate >= "01 FEB 22" AndAlso dteTrxDate <= "31 DEC 22 23:59" Then
            Return 8
        Else
            Return 10
        End If
    End Function
    Public Function ComputeVatPctInsurance() As Boolean
        Dim strQuerry As String = "select t.RecID, r.Counter,r.AL, t.DocType, t.tkno,t.SRV, DomInt " _
                                    & ", t.Dependent, t.Fare,t.Charge, t.Tax_D" _
                                    & " from tkt t left join rcp r On t.RCPID=r.RecID" _
                                    & " where t.VatPctComputed='false' and t.DomInt<>'INT' and t.qty<>0" _
                                    & " And t.DOI>='01 NOV 21' and t.Status<>'XX' and Counter<>'GSA' " _
                                    & " and t.DocType ='INS'" _
                                    & " ORDER BY t.RecID"
        Dim tblTkts As DataTable = GetDataTable(strQuerry)
        Dim decVatAmt As Decimal
        Dim decVatPctRaw As Decimal
        Dim decVatPctRounded As Decimal
        Dim strDomInt As String
        For Each objRow As DataRow In tblTkts.Rows
            decVatAmt = GetTaxAmtFromTaxDetails("UE", objRow("Tax_D"))
            If decVatAmt = 0 Then
                strDomInt = DefineDomInt("Dependent")
                ExecuteNonQuerry("Update Tkt set VatAmt=0,VatPctRaw=0,VatPctRounded=0,VatPctComputed='True',DomInt='" & strDomInt & "' where recid=" & objRow("Recid"), Conn)
                Continue For
            End If
            If objRow("SRV") = "S" Then
                decVatPctRaw = Math.Round((decVatAmt / (objRow("Fare") + objRow("Charge"))) * 100, 2)
            ElseIf objRow("SRV") = "R" Then
                decVatPctRaw = Math.Round((decVatAmt / objRow("Fare")) * 100, 2)
            End If
            decVatPctRounded = RoundVatPct(decVatPctRaw)
            Select Case decVatPctRounded
                Case 5, 7, 8, 10
                    strDomInt = "DOM"
                Case Else
                    strDomInt = DefineDomInt("Dependent")
            End Select
            ExecuteNonQuerry("Update Tkt set VatAmt=" & decVatAmt & ",VatPctRaw=" & decVatPctRaw & ",VatPctRounded=" & decVatPctRounded & ",VatPctComputed='True', DomInt='" & strDomInt _
                             & "' where recid=" & objRow("Recid"), Conn)
        Next
    End Function
    Public Function ComputeVatPctAHC() As Boolean
        Dim strQuerry As String = "select t.RecID, r.Counter, t.tkno,t.SRV, DomInt, t.DOI,t.ChargeTV" _
                                    & " from tkt t left join rcp r On t.RCPID=r.RecID" _
                                    & " where t.VatPctComputed='false' and t.qty<>0 and t.DOI>='01 NOV 21' and t.Status<>'XX' and Counter<>'GSA' " _
                                    & " and t.DocType ='AHC'" _
                                    & " ORDER BY t.RecID"
        Dim tblTkts As DataTable = GetDataTable(strQuerry)
        Dim decVatAmt As Decimal
        Dim decFareNoVat As Decimal
        Dim decVatPctRaw As Decimal
        Dim decVatPctRounded As Decimal
        Dim strDomInt As String
        For Each objRow As DataRow In tblTkts.Rows
            decVatPctRaw = GetVatPct(objRow("DOI"))
            decFareNoVat = Math.Round(objRow("ChargeTV") * 100 / (100 + decVatPctRaw))
            decVatAmt = objRow("ChargeTV") - decFareNoVat
            decVatPctRounded = decVatPctRaw
            'strDomInt = "DOM"
            ExecuteNonQuerry("Update Tkt set VatAmt=" & decVatAmt & ",VatPctRaw=" & decVatPctRaw & ",VatPctRounded=" & decVatPctRounded & ",VatPctComputed='True', DomInt='" & strDomInt _
                             & "' where recid=" & objRow("Recid"), Conn)
        Next
    End Function
    Public Function ComputeVat() As Boolean
        ComputeVatPctSfAir()
        ComputeVatPctAir()
        ComputeTaxNoUE()
        ComputeVatPctInsurance()
    End Function

    Public Function ComputeVatPctAir() As Boolean
        Dim strQuerry As String
        strQuerry = "select T.DOI, t.RecID, r.Counter,r.AL, t.DocType, t.tkno,t.SRV, DomInt" _
                    & ", t.Itinerary, t.Fare,t.Charge, t.Tax_D,t.Tax" _
                    & " from tkt t left join rcp r On t.RCPID=r.RecID" _
                    & " where t.VatPctComputed='false' and t.DomInt<>'INT' and t.qty<>0 and t.DOI>='01 NOV 21' and t.Status<>'XX' and Counter<>'GSA' " _
                    & " and substring(tkno,1,3) in ('738','ZVJ','ZQH','978','926','ATK') " _
                    & " ORDER BY t.RecID"
        Dim tblTkts As DataTable = GetDataTable(strQuerry)
        Dim decVatAmt As Decimal
        Dim decTaxNoUE As Decimal
        Dim decVatTaxNoUE As Decimal
        Dim decVatPctRaw As Decimal
        Dim decVatPctRounded As Decimal

        Dim strDomInt As String
        For Each objRow As DataRow In tblTkts.Rows
            decVatPctRaw = 0
            decVatTaxNoUE = 0

            decVatAmt = GetTaxAmtFromTaxDetails("UE", objRow("Tax_D"))
            decTaxNoUE = objRow("Tax") - decVatAmt
            If objRow("SRV") = "R" Then
                If objRow("Fare") = 0 Then
                    Dim dteOldSaleDOI As Date = ScalarToDate("TKT", "TOP 1 DOI", "Status<>'XX' and SRV='S' and TKNO='" _
                                                             & objRow("TKNO") & "' order by RecId")
                    decVatPctRaw = GetVatPct(dteOldSaleDOI)
                Else
                    decVatPctRaw = Math.Round((decVatAmt / objRow("Fare")) * 100, 2)
                End If

            ElseIf decVatAmt = 0 Then
                strDomInt = DefineDomInt(objRow("Itinerary"))
                If strDomInt = "DOM" Then
                    decVatPctRaw = GetVatPct(objRow("DOI"))
                End If
            Else
                If objRow("Fare") > 0 Then
                    decVatPctRaw = Math.Round(decVatAmt / objRow("Fare") * 100, 2)
                Else
                    decVatPctRaw = GetVatPct(objRow("DOI"))
                End If

            End If
            decVatPctRounded = RoundVatPct(decVatPctRaw)
            Select Case decVatPctRounded
                Case 5, 7, 8, 10
                    strDomInt = "DOM"
                Case Else
                    strDomInt = DefineDomInt("Itinerary")
            End Select
            If decVatPctRaw > 15 Then
                MsgBox("có lỗi xác định % VAT cho vé")
            End If
            decVatTaxNoUE = decTaxNoUE - Math.Round(decTaxNoUE * 100 / (100 + decVatPctRounded), 0)

            ExecuteNonQuerry("Update Tkt set VatAmt=" & decVatAmt & ",VatPctRaw=" & decVatPctRaw _
                             & ",VatPctRounded=" & decVatPctRounded _
                             & ",TaxNoUE=" & decTaxNoUE _
                             & ",VatTaxNoUE=" & decVatTaxNoUE _
                             & ",VatPctComputed='True', DomInt='" & strDomInt _
                             & "' where recid=" & objRow("Recid"), Conn)
        Next
    End Function
    Public Function ComputeTaxNoUE() As Boolean
        Dim strQuerry As String
        strQuerry = "select T.DOI, t.RecID, r.Counter,r.AL, t.DocType, t.tkno,t.SRV, DomInt" _
                    & ", t.Itinerary, t.Fare,t.Charge,t.Tax-t.VatAmt as TaxNoUE,t.VatPctRounded" _
                    & " from tkt t left join rcp r On t.RCPID=r.RecID" _
                    & " where t.TaxNoUE is null and t.VatPctComputed='true' and t.DomInt='DOM'" _
                    & " And t.qty<>0 And t.DOI>='01 NOV 21' and t.Status<>'XX' and Counter<>'GSA' " _
                    & " and substring(tkno,1,3) in ('738','ZVJ','ZQH','978','926','ATK') " _
                    & " ORDER BY t.RecID"
        Dim tblTkts As DataTable = GetDataTable(strQuerry)
        Dim decVatTaxNoUE As Decimal

        For Each objRow As DataRow In tblTkts.Rows
            decVatTaxNoUE = objRow("TaxNoUE") - Math.Round(objRow("TaxNoUE") * 100 / (100 + objRow("VatPctRounded")), 0)

            ExecuteNonQuerry("Update Tkt set TaxNoUE=" & objRow("TaxNoUE") _
                             & ",VatTaxNoUE=" & decVatTaxNoUE _
                             & " where recid=" & objRow("Recid"), Conn)
        Next
    End Function
    Public Function ComputeVatPctSfAir() As Boolean
        Dim strQuerry As String
        strQuerry = "select T.DOI, t.RecID,t.ChargeTV" _
                    & " from tkt t left join rcp r On t.RCPID=r.RecID" _
                    & " where t.ChargeTV<>0 and t.qty<>0 and t.DOI>='01 NOV 21' and t.Status<>'XX'" _
                    & " and t.VatPctSf is null" _
                    & " ORDER BY t.RecID"
        ExecuteNonQuerry("Update Tkt set VatPctSf=7, VatAmtSf=0" _
                             & " where ChargeTV=0 and VatPctSf is null" _
                             & " And DOI between '01 NOV 21' and '31 Dec 21 23:59'", Conn)
        ExecuteNonQuerry("Update Tkt set VatPctSf=10, VatAmtSf=0" _
                             & " where ChargeTV=0 and VatPctSf is null" _
                             & " And ((DOI between '01 Jan 22' and '31 Jan 22 23:59')" _
                             & " or (DOI >= '01 Jan 23'))", Conn)
        ExecuteNonQuerry("Update Tkt set VatPctSf=8, VatAmtSf=0" _
                             & " where ChargeTV=0 and VatPctSf is null" _
                             & " And DOI between '01 Feb 22' and '31 Dec 22 23:59'", Conn)
        Dim tblTkts As DataTable = GetDataTable(strQuerry)
        Dim decVatPctSf As Decimal
        Dim decVatAmtSf As Decimal

        For Each objRow As DataRow In tblTkts.Rows
            decVatAmtSf = 0
            decVatPctSf = GetVatPct(objRow("DOI"))
            decVatAmtSf = objRow("ChargeTV") - Math.Round(objRow("ChargeTV") * 100 / (100 + decVatPctSf), 0)
            ExecuteNonQuerry("Update Tkt set VatPctSf=" & decVatPctSf & ", VatAmtSf=" & decVatAmtSf _
                             & " where recid=" & objRow("Recid"), Conn)
        Next
    End Function
    Public Function RoundVatPct(decVatAmt As Decimal) As Decimal
        Select Case decVatAmt
            Case < 0.49
                Return 0
            Case < 1.49
                Return 1
            Case < 5.49
                Return 5
            Case < 7.49
                Return 7
            Case < 8.49
                Return 8
            Case Else
                Return 10
        End Select
    End Function
    Public Function RoundNearest(decRawNumber As Decimal) As Decimal
        Dim decWholeNumber As Decimal = Math.Truncate(decRawNumber)
        Dim decFraction As Decimal = decRawNumber - decWholeNumber
        If decFraction < 0.5 Then
            Return decWholeNumber
        Else
            Return decWholeNumber + 1
        End If
    End Function
    Public Function RoundUp(decRawNumber As Decimal, intNbrBeforeDecimal As Integer) As Decimal
        Dim decWholeNumber As Decimal = Math.Truncate(decRawNumber)
        Dim strZeros As String = "".PadRight(intNbrBeforeDecimal, "0")
        Dim decUpNbr As Decimal
        Dim decFraction As Decimal = decRawNumber - decWholeNumber

        decUpNbr = CDec(1 & strZeros)

        If decWholeNumber = 0 OrElse decWholeNumber.ToString.EndsWith(strZeros) Then
            Return decWholeNumber
        Else
            Return decWholeNumber + decUpNbr - CDec(Mid(decWholeNumber.ToString, decWholeNumber.ToString.Length - (intNbrBeforeDecimal - 1)))
        End If
    End Function
    Private Function DefineDomInt(strRtg As String) As String
        Dim arrCities() As String
        Dim i As Integer
        Dim strRtgType As String = "DOM"
        Dim strCountry As String = "VN"
        arrCities = Split(strRtg, " ")

        For i = 0 To arrCities.Length - 1 Step 2
            If GetCountryCode(arrCities(i)) <> "VN" Then
                strRtgType = "INT"
                Exit For
            End If
        Next

        Return strRtgType
    End Function
    Public Function GetCountryCode(ByVal strAirportCode As String) As String
        'Tim ma nuoc cho ma thanh pho
        'Input: Ma san bay
        'Output: Ma nuoc
        'Pre-requisite: Can ket noi TVCS
        Select Case strAirportCode
            Case "SGN", "HAN", "DAD", "HUI", "NHA", "HPH"
                Return "VN"
            Case Else
                'Dim strQuerry As String = "select Country from CityCode where Airport ='" & strAirportCode & "'"
            Return ScalarToString("CityCode", "Country", "Airport ='" & strAirportCode & "'")
        End Select

    End Function
    Public Function CreateDataErrorMsg(strField As String) As String
        Select Case strField
            Case "DepApts"
                Return "Missing Departure Airports"
            Case "DOF"
                Return "Missing Departure Date for a segment"
            Case "ArrApts"
                Return "Missing Arrival Airports"
            Case "ALs"
                Return "Missing Airlines for segments"
            Case "RBD"
                Return "Missing RBDs for segments"
            Case "DepDates"
                Return "Missing DepartureDates for segments"
            Case "ArrDates"
                Return "Missing ArrivalDates for segments"
            Case "FltNbrs"
                Return "Missing Flight Numbers for segments"
            Case "ETA"
                Return "Missing Departure times for segments"
            Case "FBs"
                Return "Missing Fare Basis for segments"
            Case "Rloc"
                Return "Missing Record Locator for segments"
            Case "InvNo"
                Return "Missing Invoice Nbr"
            Case Else
                Return strField
        End Select
    End Function
    Public Function ConvertGdsDate2iBankDate(ByVal strGdsDate As String, ByVal dteCurrentDate As Date) As String
        Select Case strGdsDate
            Case "OPEN", ""
                Return ""
            Case Else
                Return Format(AddFutureYear(dteCurrentDate, strGdsDate), "dd/MM/yyyy")
        End Select
    End Function
    Public Function GetThisTransaction(intVatDiscount As Integer, intVatPct As Integer) As Boolean

        If intVatDiscount = 30 AndAlso intVatPct = 7 Then
            Return True
        ElseIf intVatDiscount = 0 AndAlso intVatPct <> 7 Then
            Return True
        End If
        Return False
    End Function
    Public Function GetRequiredDataByDataCode(strRequiredData As String, strDataCode As String) As String
        Dim strResult As String = String.Empty
        Dim arrDatas As String() = strRequiredData.Split("|")

        For Each strData As String In arrDatas
            If strData.StartsWith(strDataCode & "/") Then
                strResult = strData.Substring(strDataCode.Length + 1)
            End If
        Next
        Return strResult
    End Function
    Public Function RemoveCarInItinerary(strRtg As String) As String
        Dim rgCar As New Regex("\s\w{2}\s")
        If strRtg = "" Then
            Return ""
        Else
            Return rgCar.Replace(strRtg, " ")
        End If

    End Function
    Public Function SqlConvertValues2InFilter(strValues As String, strSeperator As String) As String
        Dim arrBreaks As String() = Split(strValues, strSeperator)
        Dim strResult As String = " in ('" & Join(arrBreaks, "','") & "') "
        Return strResult
    End Function
    Public Function SqlFilterCustByGroupName(strGrpName As String) As String
        Dim strResult As String = " in (select IntVal from Misc where Cat='CustNameInGroup'" _
                                    & " and Val='" & strGrpName & "' and status='OK')"
        Return strResult
    End Function
    Public Function SqlFilterVendorByGroupName(strGrpName As String) As String
        Dim strResult As String = " in (select IntVal from Misc where Cat='VendorNameInGroup'" _
                                    & " and Val='" & strGrpName & "' and status='OK')"
        Return strResult
    End Function
    Public Function ConverQhFareType2Fb(strFareType As String) As String
        Select Case strFareType
            Case "BUSINESSEXCLUSIVE"
                Return "BE"
            Case "BUSINESSFLEX"
                Return "BF"
            Case "BUSINESSSMART"
                Return "BS"
            Case "ECONOMYFLEX"
                Return "EF"
            Case "ECONOMYSMART"
                Return "EST"
            Case "ECONOMYSAVER"
                Return "ES"
            Case "ECONOMYSAVERMAX"
                Return "ESM"
            Case "PREMIUM ECONOMY"
                Return "PY"
            Case Else
                Return strFareType
        End Select

    End Function
    Public Function ConverQhFareType2BkgCls(strFareType As String) As String
        Select Case strFareType
            Case "BUSINESSEXCLUSIVE"
                Return "J"
            Case "BUSINESSFLEX"
                Return "C"
            Case "BUSINESSSMART"
                Return "I"
            Case "ECONOMYFLEX"
                Return "Y"
            Case "ECONOMYSMART"
                Return "S"
            Case "ECONOMYSAVER"
                Return "O"
            Case "ECONOMYSAVERMAX"
                Return "M"
            Case "PREMIUM ECONOMY"
                Return "W"
            Case Else
                Return "Y"
        End Select
    End Function
    Public Function ConverQhPaxName2Ras(strPaxName As String) As String
        Dim i As Integer
        Dim strResult As String
        Dim arrDotBreaks As String() = Split(strPaxName, ". ")
        Dim arrSpaceBreaks As String() = arrDotBreaks(1).Split(" ")

        strResult = arrSpaceBreaks(arrSpaceBreaks.Length - 1) & "/"
        For i = 0 To arrSpaceBreaks.Length - 2
            strResult = strResult & arrSpaceBreaks(i) & " "
        Next
        Return strResult & arrDotBreaks(0)
    End Function
    Public Function ParseQhDobHtm(strTargetDate As String) As Date
        Dim arrDateParts As String() = strTargetDate.Split(".")
        Return DateSerial(arrDateParts(2), arrDateParts(1), arrDateParts(0))
    End Function
    Public Function ParseQhFltDateHtm(strTargetDate As String) As Date
        If IsDate(strTargetDate) Then Return CDate(strTargetDate)
        Dim arrDates As String() = strTargetDate.Trim.Split(",")
        Dim arrDateParts As String() = Split(arrDates(arrDates.Length - 1), " ")
        Dim i As Integer
        Dim strMonth As String = String.Empty
        For i = 2 To arrDateParts.Length - 2
            Select Case arrDateParts(i)
                Case "Một", "Mười"
                    strMonth = strMonth & "1"
                Case "Hai"
                    strMonth = strMonth & "2"
                Case "Ba"
                    strMonth = strMonth & "3"
                Case "Bốn", "Tư"
                    strMonth = strMonth & "4"
                Case "Năm"
                    strMonth = strMonth & "5"
                Case "Sáu"
                    strMonth = strMonth & "6"
                Case "Bảy"
                    strMonth = strMonth & "7"
                Case "Tám"
                    strMonth = strMonth & "8"
                Case "Chín"
                    strMonth = strMonth & "9"
            End Select
        Next
        Return DateSerial(arrDateParts(arrDateParts.Length - 1), strMonth, arrDateParts(0))
    End Function
    Public Function GetPaxTypeByDOB(dteDepDate As Date, dteDOB As Date) As String
        Select Case DateDiff(DateInterval.Month, dteDOB, dteDepDate)
            Case < 24
                Return "INF"
            Case < 144
                Return "CHD"
            Case Else
                Return "ADL"
        End Select
    End Function
    Public Function ParseVndAmtHtmlQH(strText As String) As Decimal
        Dim decResult As Decimal
        Dim arrBreaks As String() = Replace(strText, vbCrLf, "").Trim.Split(" ")
        For Each strLine As String In arrBreaks
            If IsNumeric(strLine) Then
                decResult = strLine
            End If
        Next

        Return decResult
    End Function
    Public Function Remove2Spaces(strTarget As String) As String
        Dim rg2Spaces As New Regex("\s{2,}")
        Return rg2Spaces.Replace(strTarget, " ")
    End Function
    Public Function LoadComboBooker(intCustID As Integer, ByRef cboInput As ComboBox) As Boolean
        Try
            LoadCmb_MSC(cboInput, "Select BookerName as VAL from cwt.dbo.Cwt_Bookers where Status='OK' and CustId=" _
                        & intCustID)
            If cboInput.Items.Count = 0 Then
                LoadCmb_MSC(cboInput, "select distinct fValue as VAL from SIR where fName+status='BOOKEROK' and custID=" _
                        & intCustID)
            End If
        Catch ex As Exception
            LoadCmb_MSC(cboInput, "select distinct fValue as VAL from SIR where fName+status='BOOKEROK' and custID=" _
                        & intCustID)
        End Try
        Return True
    End Function
    Public Function ConvertEInvStatus(strStatus As String) As String

        Select Case strStatus
            Case "1"
                Return "OK"
            Case "3"
                Return "EX"
            Case "4", "5"
                Return "XX"
            Case Else
                Return strStatus
        End Select

    End Function
    Public Function ConvertVatPctFromVNPT(strText As String) As String
        Dim decResult As Decimal
        strText = Replace(strText.Replace("%", ""), "KHAC:", "")
        If strText = "KCT" Then
            decResult = -1
        ElseIf strText = "KKKNT" Then
            decResult = -2
        Else
            decResult = strText
        End If
        Return decResult
    End Function
    Public Function ConvertVatPctToVNPT(intVatPct As Integer) As String
        Select Case intVatPct
            Case -1
                Return "KCT"
            Case -2
                Return "KKKNT"
            Case 0, 5, 8, 10
                Return intVatPct & "%"
            Case Else
                Return "KHAC:" & intVatPct
        End Select
    End Function
    Private Function AddCodeTour2MemoAop(strRcpNo As String, ByRef strMemo As String) As Boolean
        Dim tblTourCodes As DataTable = GetTourCodeTableByRcp(strRcpNo)
        For Each objRow As DataRow In tblTourCodes.Rows
            strMemo = strMemo & " " & objRow("TourCode")
        Next
        Return True
    End Function
    Public Function GetInvoiceEmail4TV(strCity As String) As String
        If strCity = "SGN" Then
            Return "ketoantransviet.sgn@gmail.com"
        Else
            Return "ketoantransviet.han@gmail.com"
        End If
    End Function
    Public Function CheckVatedRcp(strRcp As String) As Boolean
        Dim tblRcp As DataTable
        Dim strCheckIssuedInv As String = " and (r.Counter in ('TVS','GSA')" _
                    & "or (r.Counter='CWT' and l.RcpId=t.RCPID and l.InvId in " _
                    & "(select invid from lib.dbo.E_Inv78 where status='ok' and MauSo<>'1/001')))"
        tblRcp = GetDataTable("select l.*,t.Tkno from lib.dbo.E_InvLinks78 l" _
                        & " left join Rcp r on r.RecId=l.RcpId" _
                        & " left join tkt t on r.RecId=t.RcpId" _
                        & " where l.Status='OK' and r.Status='OK' " _
                        & " and t.Status<>'XX' and t.RcpNo='" & strRcp _
                        & "' " & strCheckIssuedInv _
                        & " and substring(t.tkno,5,4) in" _
                        & "(select Val from lib.dbo.Misc where status='OK' and cat='BspStock')")
        If tblRcp.Rows.Count = 0 Then
            Return True
        Else
            Dim tblUnlock As DataTable
            tblUnlock = GetDataTable("select top 1 * from LIB.dbo.Misc where cat='UnlockVatedRcp'" _
                                    & " and Status='OK' and intVal=" & tblRcp.Rows(0)("RcpId"))
            If tblUnlock.Rows.Count = 1 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Public Function CheckCcRcpEditable(strRcp As String) As Boolean
        Dim tblFop As DataTable = GetDataTable("select top 1 * from FOP where FOP='CRD' and Status='OK' and RcpNo='" _
                              & strRcp & "'")
        If tblFop.Rows.Count = 0 Then
            Return True
        Else
            Dim tblUnlock As DataTable
            tblUnlock = GetDataTable("select top 1 * from LIB.dbo.Misc where cat='UnlockCcRcp'" _
                                    & " and Status='OK' and intVal=" & tblFop.Rows(0)("RcpId"))
            If tblUnlock.Rows.Count = 1 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Public Function UpdateCcRcpEditable(intRcpId As Integer) As Boolean
        ExecuteNonQuerry(ChangeStatus_ByDK("LIB.dbo.Misc", "RR", "Cat='UnlockCcRcp'" _
                                & " and Status='OK' and intVal=" & intRcpId), Conn)
        Return True
    End Function
    Public Function UpdateVatedRcp(intRcpId As Integer) As Boolean
        If myStaff.Counter = "TVS" Or myStaff.Counter = "GSA" Then
            ExecuteNonQuerry(ChangeStatus_ByDK("LIB.dbo.Misc", "RR", "Cat='UnlockVatedRcp'" _
                                & " and Status='OK' and intVal=" & intRcpId), Conn)
        End If
        Return True
    End Function
    Public Function Convert2AopDate(dteInput As Date) As String
        Return "{d'" & Format(CDate(dteInput), "yyyy-MM-dd") & "'}"
    End Function
    Public Function UpdateSqlComboTextAndId(ByRef strQuerry As String, ByRef cboCondition As ComboBox _
                                , Optional ByVal strColumnName As String = "" _
                                , Optional ByVal blnUnicode As Boolean = False) As Boolean
        Dim intId As Integer
        Integer.TryParse(cboCondition.SelectedValue, intId)

        If strColumnName = "" Then
            strQuerry = strQuerry & "," & Mid(cboCondition.Name, 4) _
                & "=" & IIf(blnUnicode, "N", "") & "'" & cboCondition.Text _
                & "'," & Mid(cboCondition.Name, 4) & "Id=" & intId
        Else
            strQuerry = strQuerry & "," & strColumnName & "=" _
                & IIf(blnUnicode, "N", "") & "'" & cboCondition.Text _
                & "'," & strColumnName & "Id=" & intId
        End If

        Return True
    End Function

    '^_^20220712 add by 7643 -b-
    Public Function CheckedByCounter(xTKNO As String, xIsCheck As Boolean) As Boolean
        Dim mDate, mSQL As String, mMISCID As Integer, mTrans As SqlClient.SqlTransaction

        pobjTvcs.Connect()
        mDate = Format(Now, "yyyyMMdd hh:mm:ss")

        mMISCID = ScalarToInt("MISC", "RecID", "Status='OK' and City='" & myStaff.City & "' AND Cat='CheckedByCounter' and Val1='" & xTKNO & "'")
        If mMISCID = 0 Then
            mSQL = "insert into RAS12..MISC(Status,City,FstUpdate,FstUser,Cat,Val,Val1) " &
                            "values('OK','" & myStaff.City & "','" & mDate & "','" & myStaff.SICode & "','CheckedByCounter','" & xIsCheck.ToString & "','" & xTKNO & "'); " &
                            "insert into RAS12..MISC(Status,City,FstUpdate,FstUser,Cat,intVal,Val1) " &
                            "values('OK','" & myStaff.City & "','" & mDate & "','" & myStaff.SICode & "','CheckedByStaff'," & myStaff.StaffId & ",'" & xTKNO & "'); " &
                            "insert into RAS12..MISC(Status,City,FstUpdate,FstUser,Cat,dteVal,Val1) " &
                            "values('OK','" & myStaff.City & "','" & mDate & "','" & myStaff.SICode & "','CheckOn','" & mDate & "','" & xTKNO & "')"
        Else
            mSQL = "update RAS12..MISC set LstUpdate='" & mDate & "',LstUser='" & myStaff.SICode & "',Val='" & xIsCheck.ToString & "' where RecID=" & mMISCID & ""
            mMISCID = ScalarToInt("MISC", "RecID", "Status='OK' and City='" & myStaff.City & "' AND Cat='CheckedByStaff' and Val1='" & xTKNO & "'")
            mSQL &= "; update RAS12..MISC set LstUpdate='" & mDate & "',LstUser='" & myStaff.SICode & "',IntVal=" & myStaff.StaffId & " where RecID=" & mMISCID & ""
            mMISCID = ScalarToInt("MISC", "RecID", "Status='OK' and City='" & myStaff.City & "' AND Cat='CheckOn' and Val1='" & xTKNO & "'")
            mSQL &= "; update RAS12..MISC set LstUpdate='" & mDate & "',LstUser='" & myStaff.SICode & "',dteVal='" & mDate & "' where RecID=" & mMISCID & ""
        End If

        Try
            mTrans = Conn.BeginTransaction
            cmd.Transaction = mTrans
            cmd.CommandText = mSQL
            cmd.ExecuteNonQuery()
            mTrans.Commit()
        Catch
            mTrans.Rollback()
        End Try
    End Function
    '^_^20220712 add by 7643 -e-
    Public Function LoadCustomerInGroups2Combo(strGroupName As String, cboInput As ComboBox) As Boolean
        Dim strQuerry As String = "select Val1 as Display,intVal as Value" _
                                & " from Misc where Cat='CustNameInGroup'" _
                                & "  and Status='OK' and Val='" & strGroupName _
                                & "' order by Val1"
        LoadComboDisplay(cboInput, strQuerry, Conn)
    End Function

    '^_^20230215 add by 7643 -b-
    Public Sub BeginTrans()
        FCmd.Connection = Conn
        FTrans = Conn.BeginTransaction
        FCmd.Transaction = FTrans
    End Sub

    Public Sub CommitTrans()
        FTrans.Commit()
    End Sub

    Public Sub RollbackTrans()
        FTrans.Rollback()
    End Sub
    '^_^20230215 add by 7643 -e-
End Module
