Module mdlLocalCorp
    Public Sub AddRequiredDataFields(ByVal colData As Collection, ByVal strMandatory As String _
                            , ByVal intCustId As Integer, ByVal flpRequiredData As FlowLayoutPanel)

        For Each objRequiredData As clsRequiredData In colData
            With objRequiredData
                If .CheckValues Then
                    Dim ucRqData As New ucRqDataCombo
                    ucRqData.Tag = .DataCode
                    If strMandatory = "M" Then
                        ucRqData.lblName.Text = .NameByCustomer & "(*)"
                    ElseIf strMandatory = "C" Then
                        ucRqData.lblName.Text = .NameByCustomer & "(" _
                                                & .ConditionOfUse & ")"
                    End If
                    pobjTvcs.LoadComboDisplay(ucRqData.cboValue, "Select Value" _
                            & ",Description as Display from GO_RequiredDataValues" _
                            & " where Status='OK' and CustId=" & intCustId _
                            & " and DataCode='" & .DataCode & "'")
                    'ucRqData.cboValue.Text = ""
                    ucRqData.cboValue.SelectedIndex = -1
                    flpRequiredData.Controls.Add(ucRqData)
                Else
                    Dim ucRqData As New ucRqDataText
                    ucRqData.Tag = .DataCode
                    If strMandatory = "M" Then
                        ucRqData.lblName.Text = .NameByCustomer & "(*)"
                    ElseIf strMandatory = "C" Then
                        ucRqData.lblName.Text = .NameByCustomer & "(" _
                                                & .ConditionOfUse & ")"
                    End If

                    flpRequiredData.Controls.Add(ucRqData)
                End If
            End With
        Next
    End Sub
    Public Sub EnableFlp(ByVal flpObj As FlowLayoutPanel, ByVal blnEnable As Boolean)
        For Each objControl As Control In flpObj.Controls
            objControl.Enabled = blnEnable
        Next
    End Sub
    'Public Function CheckRequiredData(ByVal flpRequiredData As FlowLayoutPanel _
    '    , ByVal intCustId As Integer, ByVal colMandatoryData As Collection _
    '    , blnAllowBypass As Boolean, Optional intTravelId As Integer = 0) As Boolean

    '    Dim colDataErr As New Collection
    '    Dim strErrMsg As String
    '    Dim colAvaiConditionalData As New Collection
    '    Dim colAvaiMandatoryData As New Collection
    '    colAvaiMandatoryData.Clear()
    '    colAvaiMandatoryData.Clear()
    '    colDataErr.Clear()

    '    For Each objCtrl As Control In flpRequiredData.Controls
    '        With objCtrl
    '            If .Name = "ucRqDataCombo" Then
    '                Dim ucRd As ucRqDataCombo = objCtrl
    '                Dim objAvaiData As New clsAvailableData
    '                objAvaiData.DataCode = ucRd.Tag
    '                objAvaiData.DataValue = ucRd.cboValue.SelectedValue
    '                If ucRd.lblName.Text.EndsWith("(*)") Then
    '                    colAvaiMandatoryData.Add(objAvaiData, objAvaiData.DataCode)
    '                ElseIf ucRd.cboValue.Text <> "" Then
    '                    colAvaiMandatoryData.Add(objAvaiData, objAvaiData.DataCode)
    '                End If

    '            ElseIf .Name = "ucRqDataText" Then
    '                Dim ucRd As ucRqDataText = objCtrl
    '                Dim objAvaiData As New clsAvailableData
    '                objAvaiData.DataCode = ucRd.Tag
    '                objAvaiData.DataValue = ucRd.txtValue.Text
    '                If ucRd.lblName.Text.EndsWith("(*)") Then
    '                    colAvaiMandatoryData.Add(objAvaiData, objAvaiData.DataCode)
    '                ElseIf ucRd.txtValue.Text <> "" Then
    '                    colAvaiMandatoryData.Add(objAvaiData, objAvaiData.DataCode)
    '                End If
    '            End If
    '        End With
    '    Next
    '    colDataErr = pobjTvcs.CheckData(colMandatoryData, colAvaiMandatoryData, intCustId, Now)
    '    If colDataErr.Count = 0 Then
    '        Return True
    '    ElseIf blnAllowBypass Then
    '        Dim frmBypass As New frmSosBypassSelection(colDataErr, "REQUIRED_DATA", intTravelId)
    '        If frmBypass.ShowDialog = DialogResult.OK Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Else
    '        strErrMsg = pobjTvcs.DataErr2Msg(colDataErr)
    '        MsgBox(strErrMsg)
    '        Return False
    '    End If


    'End Function
    Public Function ConvertRequiredDataString2Collection(ByVal strRequiredData As String) As Collection
        Dim colResult As New Collection
        Dim i As Integer
        Dim arrRqData() As String

        If strRequiredData <> "" Then
            arrRqData = Split(strRequiredData, "|")
            For i = 0 To arrRqData.Length - 1
                Dim objRqData As New clsAvailableData
                Dim arrBreak() As String
                arrBreak = Split(arrRqData(i), "/", 2)
                With objRqData
                    .DataCode = arrBreak(0)
                    .DataValue = arrBreak(1)
                    colResult.Add(objRqData, .DataCode)
                End With
            Next
        End If
        Return colResult
    End Function
    Public Function ConvertRequiredDataString2List(ByVal strRequiredData As String) As List(Of clsAvailableData)
        Dim lstResult As New List(Of clsAvailableData)
        Dim i As Integer
        Dim arrRqData() As String

        If strRequiredData <> "" Then
            arrRqData = Split(strRequiredData, "|")
            For i = 0 To arrRqData.Length - 1
                Dim objRqData As New clsAvailableData
                Dim arrBreak() As String
                arrBreak = Split(arrRqData(i), "/", 2)
                With objRqData
                    .DataCode = arrBreak(0)
                    .DataValue = arrBreak(1)
                    lstResult.Add(objRqData)
                End With
            Next
        End If
        Return lstResult
    End Function
    Public Function GetTripType4Hunstman(ByVal strDepApts As String, ByVal strArrApts As String) As String
        Dim arrDepApts() As String
        Dim arrArrApts() As String
        Dim i As Integer
        arrDepApts = Split(strDepApts, "_")
        arrArrApts = Split(strArrApts, "_")
        Dim strFirstCountry As String
        Dim strFirstTC As String
        Dim strTripType As String = "Domestic"

        strFirstCountry = pobjTvcs.GetCountryCode(arrDepApts(0))
        strFirstTC = pobjTvcs.GetIataTcCode(arrDepApts(0))

        If arrDepApts.Length > 1 Then
            For i = 1 To arrDepApts.Length - 1
                If pobjTvcs.GetCountryCode(arrDepApts(i)) <> strFirstCountry Then
                    strTripType = "Continental"
                    If pobjTvcs.GetIataTcCode(arrDepApts(i)) <> strFirstTC Then
                        strTripType = "Intercontinental"
                        Return strTripType
                    End If
                End If
            Next
        End If

        For i = 0 To arrArrApts.Length - 1
            If pobjTvcs.GetCountryCode(arrArrApts(i)) <> strFirstCountry Then
                strTripType = "Continental"
                If pobjTvcs.GetIataTcCode(arrArrApts(i)) <> strFirstTC Then
                    strTripType = "Intercontinental"
                    Return strTripType
                End If
            End If
        Next

        Return strTripType
    End Function
    Public Function GetRoutingType4Hunstman(ByVal arrDepApts() As String, ByVal arrArrApts() As String, arrCars() As String) As String
        
        Dim i As Integer
        Dim strFirstCountry As String
        Dim strLastCountry As String
        Dim strRtgType As String = "One Way"

        If arrDepApts.Length = 1 Then
            Return strRtgType
        End If

        strFirstCountry = pobjTvcs.GetCountryCode(arrDepApts(0))
        strLastCountry = pobjTvcs.GetCountryCode(arrArrApts(arrArrApts.Length - 1))

        If strFirstCountry <> strLastCountry Then
            Return strRtgType
        ElseIf arrDepApts(0) <> arrArrApts(arrArrApts.Length - 1) Then
            strRtgType = "Open Jaw"
            Return strRtgType
        Else
            For i = 0 To arrCars.Length - 1
                If arrCars(i) = "" Or arrCars(i) = "//" Then
                    strRtgType = "Open Jaw"
                    Return strRtgType
                End If
            Next
            strRtgType = "Round Trip"
            Return strRtgType
        End If

    End Function
    Public Function IdentifyDomInlt4iBank(ByVal strDepApts As String, ByVal strArrApts As String) As String
        Dim arrDepApts() As String
        Dim arrArrApts() As String
        Dim i As Integer
        arrDepApts = Split(strDepApts, "_")
        arrArrApts = Split(strArrApts, "_")

        For i = 0 To arrDepApts.Length - 1
            If pobjTvcs.GetCountryCode(arrDepApts(i)) <> "VN" Then
                Return "I"
            End If
        Next

        For i = 0 To arrArrApts.Length - 1
            If pobjTvcs.GetCountryCode(arrArrApts(i)) <> "VN" Then
                Return "I"
            End If
        Next

        Return "D"
    End Function
    Public Function GetAvailableDataFromFlp(ByVal flpRqData As FlowLayoutPanel) As String
        Dim strResult As String = ""
        For Each objCtrl As Control In flpRqData.Controls
            With objCtrl
                If .Name = "ucRqDataCombo" Then
                    Dim ucRd As ucRqDataCombo = objCtrl
                    If ucRd.cboValue.SelectedValue <> "" Then
                        strResult = strResult & ucRd.Tag & "/" & ucRd.cboValue.SelectedValue & "|"
                    End If

                ElseIf .Name = "ucRqDataText" Then
                    Dim ucRd As ucRqDataText = objCtrl
                    Dim objAvaiData As New clsAvailableData
                    If ucRd.txtValue.Text <> "" Then
                        strResult = strResult & ucRd.Tag & "/" & ucRd.txtValue.Text & "|"
                    End If
                End If
            End With
        Next
        If strResult.Length = 0 Then
            Return ""
        Else

            Return Mid(strResult, 1, strResult.Length - 1)
        End If
    End Function
    'Public Function GetTs24SfTextByPaxType(lstTs24Sfs As List(Of clsTs24Sf), strPaxType As String)
    '    Dim strResult As String = String.Empty
    '    For Each objTs24Sf As clsTs24Sf In lstTs24Sfs
    '        If objTs24Sf.PaxType = strPaxType Then
    '            strResult = "TS24/" & objTs24Sf.Cur & "/" & objTs24Sf.Amt
    '        End If
    '    Next
    '    Return strResult
    'End Function
End Module
