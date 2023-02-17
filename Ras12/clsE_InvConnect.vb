Public Class clsE_InvConnect
    Private mstrWsUrl As String
    Private mstrUserName As String
    Private mstrUserPass As String
    Private mstrAccountName As String
    Private mstrAccountPass As String
    Private mstrTvc As String
    Private mstrPortalServiceUrl As String
    Private mstrBusinessServiceUrl As String
    Public Sub New(blnTt78 As Boolean, strTvc As String)
        If blnTt78 Then
            If pblnTestInv Then
                If myStaff.City = "SGN" Then
                    Select Case strTvc
                        Case "123"
                            mstrWsUrl = "https://0317295909-tt78democadmin.vnpt-invoice.com.vn/PublishService.asmx"
                            mstrBusinessServiceUrl = "https://0317295909-tt78democadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                            mstrPortalServiceUrl = "https://0317295909-tt78democadmin.vnpt-invoice.com.vn/PortalService.asmx"
                            mstrAccountName = "0317295909_admin_demo"
                            mstrAccountPass = "Einv@oi@vn#pt20"
                            mstrUserPass = "Einv@oi@vn#pt20"
                            mstrUserName = "0317295909_service"
                        Case "GDS"
                            mstrWsUrl = "https://phanphoitoancau_hcm-tt78admindemo.vnpt-invoice.com.vn/PublishService.asmx"
                            mstrBusinessServiceUrl = "https://phanphoitoancau_hcm-tt78admindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                            mstrPortalServiceUrl = "https://phanphoitoancau_hcm-tt78admindemo.vnpt-invoice.com.vn/PortalService.asmx"
                            mstrAccountName = "phanphoitoancau_hcmadmin"
                            mstrAccountPass = "Einv@oi@vn#pt20"
                            mstrUserPass = "Einv@oi@vn#pt20"
                            mstrUserName = "phanphoitoancauhcmservice"
                        Case "TVPM SGN"
                            mstrWsUrl = "https://tranvietphat_hcm-tt78admindemo.vnpt-invoice.com.vn/PublishService.asmx"
                            mstrBusinessServiceUrl = "https://tranvietphat_hcm-tt78admindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                            mstrPortalServiceUrl = "https://tranvietphat_hcm-tt78admindemo.vnpt-invoice.com.vn/PortalService.asmx"
                            mstrAccountName = "phanphoitoancau_hcmadmin"
                            mstrAccountPass = "Einv@oi@vn#pt20"
                            mstrUserPass = "Einv@oi@vn#pt20"
                            mstrUserName = "phanphoitoancauhcmservice"
                        Case Else
                            mstrWsUrl = "https://tranviethcm-tt78admindemo.vnpt-invoice.com.vn/PublishService.asmx"
                            mstrBusinessServiceUrl = "https://tranviethcm-tt78admindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                            mstrPortalServiceUrl = "https://tranviethcm-tt78admindemo.vnpt-invoice.com.vn/PortalService.asmx"
                            mstrAccountName = "tranviethcmadmin"
                            mstrAccountPass = "Einv@oi@vn#pt20"
                            mstrUserPass = "Einv@oi@vn#pt20"
                            mstrUserName = "tranviethcmservice"
                    End Select
                Else
                    mstrWsUrl = "https://cntranviet-hanoi-tt78admindemo.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrBusinessServiceUrl = "https://cntranviet-hanoi-tt78admindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrPortalServiceUrl = "https://cntranviet-hanoi-tt78admindemo.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrAccountName = "cntranviet-hanoiadmin"
                    mstrAccountPass = "Einv@oi@vn#pt20"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrUserName = "cntranviethanoiservice"
                End If
            Else
                Select Case strTvc
                    Case "123"
                        mstrWsUrl = "https://0317295909-tt78cadmin.vnpt-invoice.com.vn/PublishService.asmx"
                        mstrBusinessServiceUrl = "https://0317295909-tt78cadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                        mstrPortalServiceUrl = "https://0317295909-tt78cadmin.vnpt-invoice.com.vn/PortalService.asmx"
                        mstrAccountName = "0317295909_admin"
                        mstrAccountPass = "Einv@oi@vn#pt20"
                        mstrUserPass = "Einv@oi@vn#pt20"
                        mstrUserName = "0317295909_service"
                    Case "APG"
                        mstrWsUrl = "https://airpromotion-tt78admin.vnpt-invoice.com.vn/PublishService.asmx"
                        mstrBusinessServiceUrl = "https://airpromotion-tt78admin.vnpt-invoice.com.vn/BusinessService.asmx"
                        mstrPortalServiceUrl = "https://airpromotion-tt78admin.vnpt-invoice.com.vn/PortalService.asmx"
                        mstrAccountName = "0316678722_admin"
                        mstrAccountPass = "Einv@oi@vn#pt20"
                        mstrUserName = "airpromotionservice"
                        mstrUserPass = "Einv@oi@vn#pt20"
                    Case "GDS"
                        mstrWsUrl = "https://phanphoitoancau_hcm-tt78admin.vnpt-invoice.com.vn/PublishService.asmx"
                        mstrBusinessServiceUrl = "https://phanphoitoancau_hcm-tt78admin.vnpt-invoice.com.vn/BusinessService.asmx"
                        mstrPortalServiceUrl = "https://phanphoitoancau_hcm-tt78admin.vnpt-invoice.com.vn/PortalService.asmx"
                        mstrAccountName = "phanphoitoancau_hcmadmin"
                        mstrAccountPass = "Einv@oi@vn#pt20"
                        mstrUserName = "phanphoitoancauhcmservice"
                        mstrUserPass = "Einv@oi@vn#pt20"
                    Case "GDS HAN"
                        mstrWsUrl = "https://phanphoitoancau-hn-tt78admin.vnpt-invoice.com.vn/PublishService.asmx"
                        mstrBusinessServiceUrl = "https://phanphoitoancau-hn-tt78admin.vnpt-invoice.com.vn/BusinessService.asmx"
                        mstrPortalServiceUrl = "https://phanphoitoancau-hn-tt78admin.vnpt-invoice.com.vn/PortalService.asmx"
                        mstrAccountName = "phanphoitoancau-hnadmin"
                        mstrAccountPass = "Einv@oi@vn#pt20"
                        mstrUserName = "phanphoitoancauhnservice"
                        mstrUserPass = "Einv@oi@vn#pt20"
                    Case "TVTR"
                        mstrWsUrl = "https://tranviethcm-tt78admin.vnpt-invoice.com.vn/PublishService.asmx"
                        mstrBusinessServiceUrl = "https://tranviethcm-tt78admin.vnpt-invoice.com.vn/BusinessService.asmx"
                        mstrPortalServiceUrl = "https://tranviethcm-tt78admin.vnpt-invoice.com.vn/PortalService.asmx"
                        mstrAccountName = "tranviethcmadmin"
                        mstrAccountPass = "Einv@oi@vn#pt20"
                        mstrUserName = "tranviethcmservice"
                        mstrUserPass = "Einv@oi@vn#pt20"
                    Case "TVTR HAN"
                        mstrWsUrl = "https://tranviet-hanoi-tt78admin.vnpt-invoice.com.vn/PublishService.asmx"
                        mstrBusinessServiceUrl = "https://tranviet-hanoi-tt78admin.vnpt-invoice.com.vn/BusinessService.asmx"
                        mstrPortalServiceUrl = "https://tranviet-hanoi-tt78admin.vnpt-invoice.com.vn/PortalService.asmx"
                        mstrAccountName = "tranviet-hanoiadmin"
                        mstrAccountPass = "Einv@oi@vn#pt20"
                        mstrUserName = "tranviethanoiwebservice"
                        mstrUserPass = "Einv@oi@vn#pt20"
                    Case "VLM"
                        mstrWsUrl = "https://vietlienminh_hcm-tt78admin.vnpt-invoice.com.vn/PublishService.asmx"
                        mstrBusinessServiceUrl = "https://vietlienminh_hcm-tt78admin.vnpt-invoice.com.vn/BusinessService.asmx"
                        mstrPortalServiceUrl = "https://vietlienminh_hcm-tt78admin.vnpt-invoice.com.vn/PortalService.asmx"
                        mstrAccountName = "vietlienminh_hcmadmin"
                        mstrAccountPass = "Einv@oi@vn#pt20"
                        mstrUserName = "vietlienminhhcmservice"
                        mstrUserPass = "Einv@oi@vn#pt20"
                    Case "VLM HAN"
                        mstrWsUrl = "https://vietlienminh_hanoi-tt78admin.vnpt-invoice.com.vn/PublishService.asmx"
                        mstrBusinessServiceUrl = "https://vietlienminh_hanoi-tt78admin.vnpt-invoice.com.vn/BusinessService.asmx"
                        mstrPortalServiceUrl = "https://vietlienminh_hanoi-tt78admin.vnpt-invoice.com.vn/PortalService.asmx"
                        mstrAccountName = "vietlienminh_hanoiadmin"
                        mstrAccountPass = "Einv@oi@vn#pt20"
                        mstrUserName = "vietlienminhhanoiservice"
                        mstrUserPass = "Einv@oi@vn#pt20"
                    Case "TVPM"
                        mstrWsUrl = "https://0100362645-tt78cadmin.vnpt-invoice.com.vn/PublishService.asmx"
                        mstrBusinessServiceUrl = "https://0100362645-tt78cadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                        mstrPortalServiceUrl = "https://0100362645-tt78cadmin.vnpt-invoice.com.vn/PortalService.asmx"
                        mstrAccountName = "0100362645_admin"
                        mstrAccountPass = "Einv@oi@vn#pt20"
                        mstrUserName = "0100362645service"
                        mstrUserPass = "Einv@oi@vn#pt20"

                    Case "TVPM SGN"
                        mstrWsUrl = "https://tranvietphat-hcm-tt78admin.vnpt-invoice.com.vn/PublishService.asmx"
                        mstrBusinessServiceUrl = "https://tranvietphat-hcm-tt78admin.vnpt-invoice.com.vn/BusinessService.asmx"
                        mstrPortalServiceUrl = "https://tranvietphat-hcm-tt78admin.vnpt-invoice.com.vn/PortalService.asmx"
                        mstrAccountName = "tranvietphat-hcmadmin"
                        mstrAccountPass = "Einv@oi@vn#pt20"
                        mstrUserName = "tranvietphathcmservice"
                        mstrUserPass = "Einv@oi@vn#pt20"

                    Case Else
                        MsgBox("Chưa có thông tin kết nối VNPT!")
                End Select

            End If
        Else
            Select Case strTvc
                Case "ACR"
                    mstrWsUrl = "https://0312484155-democadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://0312484155-democadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrBusinessServiceUrl = "https://0312484155-democadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrUserName = "0312484155_service"
                    mstrUserPass = "123456aA@"
                    mstrAccountName = "0312484155_admin_demo"
                    mstrAccountPass = "123456aA@"
                Case "APG"
                    mstrWsUrl = "https://0316678722-cadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://0316678722-cadmin.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrBusinessServiceUrl = "https://0316678722-cadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrUserName = "0316678722_service"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrAccountName = "0316678722_admin"
                    mstrAccountPass = "Einv@oi@vn#pt20"

                Case "GDS"
                    mstrWsUrl = "https://phanphoitoancau_hcmadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://phanphoitoancau_hcmadmin.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrBusinessServiceUrl = "https://phanphoitoancau_hcmadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrUserName = "phanphoitoancauhcmservice"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrAccountName = "phanphoitoancau_hcmadmin"
                    mstrAccountPass = "Einv@oi@vn#pt20"
                'mstrWsUrl = "https://phanphoitoancau_hcmadmindemo.vnpt-invoice.com.vn/PublishService.asmx"
                'mstrPortalServiceUrl = "https://phanphoitoancau_hcmadmindemo.vnpt-invoice.com.vn/PortalService.asmx"
                'mstrBusinessServiceUrl = "https://phanphoitoancau_hcmadmindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                'mstrUserName = "phanphoitoancauhcmservice"
                'mstrUserPass = "123456aA@"
                'mstrAccountName = "phanphoitoancau_hcmadmin"
                'mstrAccountPass = "123456aA@"
                Case "GDS HAN"
                    mstrWsUrl = "https://phanphoitoancau-hnadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://phanphoitoancau-hnadmin.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrBusinessServiceUrl = "https://phanphoitoancau-hnadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrUserName = "phanphoitoancauhnservice"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrAccountName = "phanphoitoancau-hnadmin"
                    mstrAccountPass = "Einv@oi@vn#pt20"
                'mstrWsUrl = "https://phanphoitoancau-hnadmindemo.vnpt-invoice.com.vn/PublishService.asmx"
                'mstrPortalServiceUrl = "https://phanphoitoancau-hnadmindemo.vnpt-invoice.com.vn/PortalService.asmx"
                'mstrBusinessServiceUrl = "https://phanphoitoancau-hnadmindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                ''mstrUserName = "phanphoitoancau-hnservice"
                'mstrUserName = "ras"
                'mstrUserPass = "123456aA@"
                'mstrAccountName = "phanphoitoancau-hnadmin"
                'mstrAccountPass = "123456aA@"
                Case "TVPM"
                    'mstrWsUrl = "https://tranvietphat_hanoiadmindemo.vnpt-invoice.com.vn/PublishService.asmx"
                    'mstrPortalServiceUrl = "https://tranvietphat_hanoiadmindemo.vnpt-invoice.com.vn/PortalService.asmx"
                    'mstrBusinessServiceUrl = "https://tranvietphat_hanoiadmindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                    'mstrUserName = "TVPMHANWS"
                    ''mstrUserName = "tranvietphat_hanoiservice"
                    'mstrUserPass = "123456aA@"
                    'mstrAccountName = "tranvietphat_hanoiadmin"
                    'mstrAccountPass = "123456aA@"

                    mstrWsUrl = "https://tranvietphat_hanoiadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://tranvietphat_hanoiadmin.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrBusinessServiceUrl = "https://tranvietphat_hanoiadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrUserName = "tranvietphat_hanoiservice"
                    'mstrUserName = "tranvietphat_hanoiservice"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrAccountName = "tranvietphat_hanoiadmin"
                    mstrAccountPass = "Einv@oi@vn#pt20"
                Case "TVPM SGN"

                    mstrWsUrl = "https://tranvietphat-hcmadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://tranvietphat-hcmadmin.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrBusinessServiceUrl = "https://tranvietphat-hcmadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrUserName = "tranvietphathcmservice"
                    'mstrUserName = "COS"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrAccountName = "tranvietphat-hcmadmin"
                    mstrAccountPass = "Einv@oi@vn#pt20"

                'mstrWsUrl = "https://tranvietphat-hcmadmindemo.vnpt-invoice.com.vn/PublishService.asmx"
                'mstrPortalServiceUrl = "https://tranvietphat-hcmadmindemo.vnpt-invoice.com.vn/PortalService.asmx"
                'mstrBusinessServiceUrl = "https://tranvietphat-hcmadmindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                'mstrUserName = "tranvietphathcmservice"
                'mstrUserPass = "123456aA@"
                'mstrAccountName = "tranvietphat-hcmadmin"
                'mstrAccountPass = "123456aA@"

                Case "TVTR"
                    mstrWsUrl = "https://tranviethcmadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://tranviethcmadmin.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrBusinessServiceUrl = "https://tranviethcmadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrUserName = "tranviethcmservice"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrAccountName = "tranviethcmadmin"
                    mstrAccountPass = "Einv@oi@vn#pt20"
                'mstrWsUrl = "https://tranviethcmadmindemo.vnpt-invoice.com.vn/PublishService.asmx"
                'mstrPortalServiceUrl = "https://tranviethcmadmindemo.vnpt-invoice.com.vn/PortalService.asmx"
                'mstrBusinessServiceUrl = "https://tranviethcmadmindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                'mstrUserName = "tranviethcmservice"
                'mstrUserPass = "123456aA@"
                'mstrAccountName = "tranviethcmadmin"
                'mstrAccountPass = "123456aA@"
                Case "TVTR DAD"
                    mstrWsUrl = "https://tranviet-danangadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://tranviet-danangadmin.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrBusinessServiceUrl = "https://tranviet-danangadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrUserName = "tranvietdanangservice"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrAccountName = "tranviet-danangadmin"
                    mstrAccountPass = "Einv@oi@vn#pt20"
                'mstrWsUrl = "https://tranviet-danangadmindemo.vnpt-invoice.com.vn/PublishService.asmx"
                'mstrPortalServiceUrl = "https://tranviet-danangadmindemo.vnpt-invoice.com.vn/PortalService.asmx"
                'mstrBusinessServiceUrl = "https://tranviet-danangadmindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                'mstrUserName = "tranvietdanangservice"
                'mstrUserPass = "123456aA@"
                'mstrAccountName = "tranviet-danangadmin"
                'mstrAccountPass = "123456aA@"
                Case "TVTR HAN"
                    mstrWsUrl = "https://tranviet-hanoiadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://tranviet-hanoiadmin.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrBusinessServiceUrl = "https://tranviet-hanoiadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrUserName = "tranviethanoiservice"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrAccountName = "tranviet-hanoiadmin"
                    mstrAccountPass = "Einv@oi@vn#pt20"
                'mstrWsUrl = "https://tranviet-hanoiadmindemo.vnpt-invoice.com.vn/PublishService.asmx"
                'mstrPortalServiceUrl = "https://tranviet-hanoiadmindemo.vnpt-invoice.com.vn/PortalService.asmx"
                'mstrBusinessServiceUrl = "https://tranviet-hanoiadmindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                'mstrUserName = "TranvietService"
                ''mstrUserName = "tranviet-hanoiservice"
                'mstrUserPass = "123456aA@"
                'mstrAccountName = "tranviet-hanoiadmin"
                'mstrAccountPass = "123456aA@"
                Case "VLM"
                    mstrWsUrl = "https://vietlienminh_hcmadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://vietlienminh_hcmadmin.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrBusinessServiceUrl = "https://vietlienminh_hcmadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrUserName = "vietlienminh_hcmservice"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrAccountName = "vietlienminh_hcmadmin"
                    mstrAccountPass = "Einv@oi@vn#pt20"
                'mstrWsUrl = "https://vietlienminh_hcmadmindemo.vnpt-invoice.com.vn/PublishService.asmx"
                'mstrPortalServiceUrl = "https://vietlienminh_hcmadmindemo.vnpt-invoice.com.vn/PortalService.asmx"
                'mstrBusinessServiceUrl = "https://vietlienminh_hcmadmindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                'mstrUserName = "vietlienminh_hcmservice"
                'mstrUserPass = "123456aA@"
                'mstrAccountName = "vietlienminh_hcmadmin"
                'mstrAccountPass = "123456aA@"
                Case "VLM HAN"
                    mstrWsUrl = "https://vietlienminh_hanoiadmin.vnpt-invoice.com.vn/PublishService.asmx"
                    mstrPortalServiceUrl = "https://vietlienminh_hanoiadmin.vnpt-invoice.com.vn/PortalService.asmx"
                    mstrBusinessServiceUrl = "https://vietlienminh_hanoiadmin.vnpt-invoice.com.vn/BusinessService.asmx"
                    mstrUserName = "vietlienminh_hanoiservice"
                    mstrUserPass = "Einv@oi@vn#pt20"
                    mstrAccountName = "vietlienminh_hanoiadmin"
                    mstrAccountPass = "Einv@oi@vn#pt20"
                    'mstrWsUrl = "https://vietlienminh_hanoiadmindemo.vnpt-invoice.com.vn/PublishService.asmx"
                    'mstrPortalServiceUrl = "https://vietlienminh_hanoiadmindemo.vnpt-invoice.com.vn/PortalService.asmx"
                    'mstrBusinessServiceUrl = "https://vietlienminh_hanoiadmindemo.vnpt-invoice.com.vn/BusinessService.asmx"
                    'mstrUserName = "ras"
                    'mstrUserPass = "123456aA@"
                    'mstrAccountName = "vietlienminh_hanoiadmin"
                    'mstrAccountPass = "123456aA@"
            End Select

        End If
        mstrTvc = strTvc
    End Sub

    Public Property WsUrl As String
        Get
            Return mstrWsUrl
        End Get
        Set(value As String)
            mstrWsUrl = value
        End Set
    End Property
    Public Property UserName As String
        Get
            Return mstrUserName
        End Get
        Set(value As String)
            mstrUserName = value
        End Set
    End Property
    Public Property UserPass As String
        Get
            Return mstrUserPass
        End Get
        Set(value As String)
            mstrUserPass = value
        End Set
    End Property
    Public Property AccountName As String
        Get
            Return mstrAccountName
        End Get
        Set(value As String)
            mstrAccountName = value
        End Set
    End Property
    Public Property AccountPass As String
        Get
            Return mstrAccountPass
        End Get
        Set(value As String)
            mstrAccountPass = value
        End Set
    End Property

    Public Property Tvc As String
        Get
            Return mstrTvc
        End Get
        Set(value As String)
            mstrTvc = value
        End Set
    End Property
    Public Property PortalServiceUrl As String
        Get
            Return mstrPortalServiceUrl
        End Get
        Set(value As String)
            mstrPortalServiceUrl = value
        End Set
    End Property
    Public Property BusinessServiceUrl As String
        Get
            Return mstrBusinessServiceUrl
        End Get
        Set(value As String)
            mstrBusinessServiceUrl = value
        End Set
    End Property
End Class
