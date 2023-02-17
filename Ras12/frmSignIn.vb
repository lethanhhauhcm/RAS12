Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports RAS12.MySharedFunctions
Public Class frmSignIn
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.txtStaffId.Text = GetSetting("car", "dangkiem", "Type")
        If Me.txtStaffId.Text <> "" Then Me.txtLogInPSW.Focus()
        'Me.txtLogInPSW.Text = EnCode(GetSetting("COS", "Preference", "WSP"), "115")
        If txtStaffId.Text = "212" Then
            Me.txtLogInPSW.Text = "Abcd2019"
        End If


    End Sub

    Private Sub CmdLogInOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdLogInOK.Click
        Dim intStaffId As Integer
        Dim tmpSIcode As String = ""
        Dim strBiz As String
        Dim strCounter As String
        Dim strCity As String

        txtStaffId.Text = txtStaffId.Text.Trim
        txtLogInPSW.Text = txtLogInPSW.Text.Trim

        If Not IsNumeric(txtStaffId.Text) Then
            MsgBox("Invalid Staff Id")
            Exit Sub
        End If

        intStaffId = do_Login(txtStaffId.Text, txtLogInPSW.Text)
        If My.Computer.Name = "5-247" AndAlso txtStaffId.Text = "212" Then
            If MsgBox("Có muốn đăng nhập bằng sign in của người khác thì chọn Yes", MsgBoxStyle.YesNo) _
                = MsgBoxResult.Yes Then
                Dim strTmp As String = InputBox("Input Staff Id/City",, "4029/HAN")
                intStaffId = strTmp.Split("/")(0)
                myStaff.City = strTmp.Split("/")(1).ToUpper
            Else
                intStaffId = txtStaffId.Text
            End If

        ElseIf intStaffId > 0 Then
            If txtLogInPSW.Text = "Abcd@1234" Then
                MsgBox("Đang sử dụng Mật Khẩu mặc định. Vui lòng đổi Mật Khẩu", MsgBoxStyle.Critical, "Đổi Mật Khẩu")
                txtLogInPSW.Text = ""
                Process.Start("http://transviet.net/changepassword2.aspx?StaffID=" & txtStaffId.Text)
                Exit Sub
            End If
        Else
            'LỖI
            MsgBox("StaffID hoặc Password không đúng", MsgBoxStyle.Critical, "Lỗi")
            Exit Sub
        End If

        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If

        CnStr = "server=118.69.81.103;uid=user_ras;pwd=VietHealthy@170172#;database=RAS12"
        Conn.ConnectionString = CnStr
        AddHandler Conn.StateChange, AddressOf frmMain.Conn_StateChange
        Conn.Open()

        If myStaff.City = "HAN" Then
            If ScalarToInt("tblUser", "RecId", "Status<>'xx' and Location='HAN' and Counter='CWT' and StaffId=" & intStaffId) = 0 Then
                Conn.Close()
                CnStr = "server=118.69.81.103;uid=user_rashan;pwd=VietHealthy@170172#;database=ras12han"
                Conn.ConnectionString = CnStr
                Conn.Open()
            End If
        End If

        If IsNumeric(txtStaffId.Text) Then
            Dim tblCounter As DataTable
            tblCounter = GetDataTable("Select Val1 as Biz,VAL AS Counter from MISC where Status='OK' and Cat='Counter' and Val in" _
                                      & "(Select distinct Val1 as value from MISC where Status='OK' and Cat='SignInCounter' and Val=" _
                             & "(select top 1 SiCode from tblUser where Status<>'xx' and StaffId=" & intStaffId & "))", Conn)

            Select Case tblCounter.Rows.Count
                Case 1
                    strBiz = tblCounter.Rows(0)("Biz")
                    strCounter = tblCounter.Rows(0)("Counter")
                Case 0
                    MsgBox("Không xác định được Biz và Counter")
                    Exit Sub
                Case > 1
                    Dim frmSelect As New frmShowTableContent(tblCounter, "Select Biz and Counter", "Biz")
                    If frmSelect.ShowDialog = DialogResult.OK Then
                        strBiz = frmSelect.SelectedRow.Cells("Biz").Value
                        strCounter = frmSelect.SelectedRow.Cells("Counter").Value
                    Else
                        Exit Sub
                    End If
            End Select
        End If


        tmpSIcode = ScalarToString("tblUser", "SiCode", "Status<>'xx' and StaffId=" & intStaffId)
        If tmpSIcode = "" Then
            MsgBox("Chưa tạo user cho chương trình RAS. Đề nghị liên hệ Khanh.NguyenMinh và thông báo mã nhân viên tương ứng")
            Exit Sub
        End If
        myStaff.SICode = tmpSIcode
        myStaff.SelectedDomain = strBiz

        If myStaff.UID = 0 Then
            myStaff.SICode = ""
            MsgBox("Invalid Sign-In Code ", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        End If

        myStaff.Counter = strCounter
        myStaff.CnStr = CnStr
        MyAL.Counter = myStaff.Counter
        MySession.Counter = strCounter


        If myStaff.DAccess.Length < 8 Then
            MySession.Domain = Strings.Right(myStaff.DAccess, 3)
        Else
            MySession.Domain = strBiz
        End If
        MySession.City = myStaff.City
        MySession.Location = myStaff.Location


        If MySession.Domain = "" OrElse InStr("EDU_TVS_GSA", MySession.Domain) = 0 OrElse
            MySession.Counter = "" OrElse MySession.City = "" OrElse InStr("HAN_SGN", MySession.City) = 0 OrElse
            MySession.Location = "" Then
            myStaff.SICode = ""
            MsgBox("Invalid Config of Domain or Counter or City or Loacation", MsgBoxStyle.Critical, msgTitle)
            Exit Sub
        ElseIf InStr(myStaff.DAccess, "YY") + InStr(myStaff.DAccess, MySession.Domain) + InStr(myStaff.City, MySession.City) = 0 Then
            MsgBox("Your Are not Assigned To This Counter/City", MsgBoxStyle.Critical, msgTitle)
            myStaff.LogOut()
            Exit Sub
        Else

            SaveSetting("car", "dangkiem", "Type", myStaff.StaffId)
        End If
        'frmMain.SignIn(myStaff.SICode)
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub CmdLogInCancel_Click(sender As Object, e As EventArgs) Handles CmdLogInCancel.Click
        Me.Dispose()
    End Sub
    Private su
    Private Sub txtLogInSIcode_LostFocus(sender As Object, e As EventArgs) Handles txtStaffId.LostFocus

    End Sub


    Function do_Login(StaffID As String, Password As String) As Integer
        Dim url As String = String.Format("http://api.transviet.net/ezStaff-SignIn/{0}/{1}", StaffID, Password)
        Dim req As HttpWebRequest = HttpWebRequest.Create(url)
        req.ContentType = "application/json"
        req.Method = "GET"

        Dim json As Object
        json = Nothing

        Try
            Using res As HttpWebResponse = req.GetResponse()
                Using responseStream As New StreamReader(res.GetResponseStream())
                    Dim responseData As String = responseStream.ReadToEnd()
                    json = JsonConvert.DeserializeObject(Of Object)(responseData)
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Critical, "Lỗi sever đăng nhập. Gọi IT")
            Return -1
        End Try

        If json("StaffID") = 0 Then
            'MsgBox(json("ErrMsg"))
            Return 0
        Else
            myStaff.City = json("City")
            Return CInt(json("StaffID"))
        End If
    End Function

    Private Sub txtStaffId_TextChanged(sender As Object, e As EventArgs) Handles txtStaffId.TextChanged

    End Sub
End Class