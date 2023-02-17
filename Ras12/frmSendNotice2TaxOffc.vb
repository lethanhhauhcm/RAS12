Public Class frmSendNotice2TaxOffc
    Private mstrTvc As String
    Private mblnSend As Boolean
    Private mstrResponse As String
    Public Sub New(strTvc As String, lstInvoices As List(Of DataGridViewRow), tlbInvoices As DataTable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim tblTvc As DataTable
        If lstInvoices IsNot Nothing Then

            mstrTvc = strTvc
            mblnSend = True
            tblTvc = GetDataTable("select * from Lib.Dbo.E_InvTvc78 where TVC='" & strTvc & "'", Conn)
            If tblTvc.Rows.Count > 0 Then
                txtTaxPayer.Text = tblTvc.Rows(0)("TaxPayer")
                txtTaxOffice.Text = tblTvc.Rows(0)("TaxOffice")
                txtTaxCode.Text = tblTvc.Rows(0)("TaxCode")
                txtLocation.Text = tblTvc.Rows(0)("Location")
            End If
            txtNoticeDate.Text = Format(Now, "yyyy-MM-dd")
            cboNoticeType.SelectedIndex = 0
            dtpNoticeDateTaxOffc.MaxDate = Now.Date

            For Each objRow As DataGridViewRow In lstInvoices
                With objRow
                    dgrInvoices.Rows.Add({ .Cells("MaCoQuanThue").Value, .Cells("RecId").Value, .Cells("MauSo").Value _
                                     , .Cells("KyHieu").Value, .Cells("InvoiceNo").Value, .Cells("DOI").Value _
                                     , .Cells("invType").Value, .Cells("InvFkey").Value, .Cells("Action").Value _
                                     , .Cells("Reason").Value})
                End With
            Next
            Me.Text = strTvc & " - THÔNG BÁO HÓA ĐƠN ĐIỆN TỬ CÓ SAI SÓT"
        Else
            With tlbInvoices
                mstrTvc = .Rows(0)("TVC")
                mstrResponse = .Rows(0)("Response")
                tblTvc = GetDataTable("select * from Lib.Dbo.E_InvTvc78 where TVC='" & strTvc & "'", Conn)
                If tblTvc.Rows.Count > 0 Then
                    txtTaxPayer.Text = tblTvc.Rows(0)("TaxPayer")
                    txtTaxOffice.Text = tblTvc.Rows(0)("TaxOffice")
                    txtTaxCode.Text = tblTvc.Rows(0)("TaxCode")
                    txtLocation.Text = tblTvc.Rows(0)("Location")
                End If
                txtNoticeDate.Text = Format(.Rows(0)("SentDate"), "yyyy-MM-dd")
                cboNoticeType.SelectedIndex = cboNoticeType.FindStringExact(.Rows(0)("NoticeType"))
                If Not IsDBNull(.Rows(0)("TaxOffcNoticeDate")) Then
                    dtpNoticeDateTaxOffc.Value = .Rows(0)("TaxOffcNoticeDate")
                End If

                txtTaxOffcNoticeNbr.Text = .Rows(0)("TaxOffcNoticeNbr")
            End With
            For Each objRow As DataRow In tlbInvoices.Rows

                dgrInvoices.Rows.Add({objRow("MaCoQuanThue"), objRow("RecId"), objRow("MauSo") _
                                     , objRow("KyHieu"), objRow("InvoiceNo"), objRow("DOI") _
                                     , objRow("InvType"), objRow("InvFkey"), objRow("Action"), objRow("Reason")})

            Next
            Me.Text = strTvc & " - KIỂM TRA TÌNH TRẠNG THÔNG BÁO HÓA ĐƠN ĐIỆN TỬ CÓ SAI SÓT"
        End If

    End Sub

    Private Sub frmSendNotice2TaxOffc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim objConnect As New clsE_InvConnect(pblnTT78, mstrTvc)
        Dim objE_Invoice As New clsE_Invoice

        If Not mblnSend Then
            Receive(CreateNotice, mstrResponse)
        End If
    End Sub

    Private Sub cboNoticeType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNoticeType.SelectedIndexChanged
        If cboNoticeType.Text.StartsWith(2) Then
            txtTaxOffcNoticeNbr.Visible = True
            dtpNoticeDateTaxOffc.Visible = True
        Else
            txtTaxOffcNoticeNbr.Visible = False
            dtpNoticeDateTaxOffc.Visible = False
        End If
    End Sub
    Public Function CreateNotice() As XElement
        If Not CheckInputValues() Then Return Nothing
        Dim objNotice As XElement

        objNotice = <DLTBao>
                        <TNNT><%= txtTaxPayer.Text %></TNNT>
                        <TCQT><%= txtTaxOffice.Text %></TCQT>
                        <NTBao><%= txtNoticeDate.Text %></NTBao>
                        <DDanh><%= txtLocation.Text %></DDanh>
                        <Loai><%= Mid(cboNoticeType.Text, 1, 1) %></Loai>
                    </DLTBao>
        If Mid(cboNoticeType.Text, 1, 1) = "2" Then
            objNotice.Add(<So><%= txtTaxOffcNoticeNbr.Text %></So>)
            objNotice.Add(<NTBCCQT><%= Format(dtpNoticeDateTaxOffc.Value, "dd/MM/yyyy") %></NTBCCQT>)
        End If

        Dim objDSHDon As XElement = <DSHDon></DSHDon>
        For Each objRow As DataGridViewRow In dgrInvoices.Rows
            With objRow
                objDSHDon.Add(GenerateInvoiceNoticeLine(.Index + 1, .Cells("MaCoQuanThue").Value, .Cells("MauSo").Value _
                                                        , .Cells("KyHieu").Value, .Cells("InvoiceNo").Value, .Cells("DOI").Value _
                                                        , Mid(.Cells("TinhChat").Value, 1, 1), .Cells("Reason").Value _
                                                        , .Cells("InvId").Value, .Cells("InvType").Value))
            End With
        Next
        objNotice.Add(objDSHDon)
        Return objNotice
    End Function
    Public Function Send(objNotice As XElement, strpattern As String) As Boolean
        Dim objConnect As New clsE_InvConnect(pblnTT78, mstrTvc)
        Dim objE_Invoice As New clsE_Invoice
        Dim strSerialCert As String = ""

        If mstrTvc = "123" Then
            strSerialCert = pstrSerialCert123
        End If

        If strSerialCert = "" Then
            If objE_Invoice.SendInvNoticeErrors(objConnect.WsUrl, objConnect.UserName, objConnect.UserPass _
                                                , objConnect.AccountName, objConnect.AccountPass, strpattern, objNotice) Then
                Dim lstQuerries As New List(Of String)
                Dim strResponse As String = objE_Invoice.ReponseCode.Split(":")(1)
                For Each objRow As DataGridViewRow In dgrInvoices.Rows
                    With objRow
                        lstQuerries.Add("Update lib.dbo.E_InvNotice set Reason=N'" & .Cells("Reason").Value _
                                        & "',SentDate=getdate(),SentUser='" & myStaff.StaffId & "',Response='" & strResponse _
                                        & "' where RecId=" & .Cells("RecId").Value)
                    End With
                Next
                If UpdateListOfQuerries(lstQuerries, Conn) Then
                    Me.DialogResult = DialogResult.OK
                Else
                    MsgBox("Đề nghị thông báo cho người lập trình RAS:" & vbNewLine & "Đã gửi được thông báo cho Cơ quan thuế nhưng không cập nhật được vào RAS!")
                End If
            Else
                MsgBox("Không gửi được thông báo cho Cơ quan thuế!")
            End If
        Else
            Dim lstQuerries As New List(Of String)
            For Each objRow As DataGridViewRow In dgrInvoices.Rows
                If Not objE_Invoice.GetHashInvNoticeErrors(objConnect.WsUrl _
                                    , objConnect.UserName, objConnect.UserPass _
                                    , objConnect.AccountName, objConnect.AccountPass _
                                    , objRow.Cells("MauSo").Value, strSerialCert, objNotice) Then
                    MsgBox("Không lấy được Hash Value cho dòng " & objRow.Index + 1)
                ElseIf Not objE_Invoice.SendInvNoticeErrorsWidthToken(objConnect.WsUrl _
                                    , objConnect.UserName, objConnect.UserPass _
                                    , objConnect.AccountName, objConnect.AccountPass _
                                    , objRow.Cells("MauSo").Value _
                                    , objE_Invoice.ReponseCode, strSerialCert) Then
                    MsgBox("Không gửi được thông báo cho Cơ quan thuế dòng " & objRow.Index + 1)
                Else
                    Dim strResponse As String = objE_Invoice.ReponseCode.Split(":")(1)
                    With objRow
                        If Not ExecuteNonQuerry("Update lib.dbo.E_InvNotice set Reason=N'" & .Cells("Reason").Value _
                                & "',SentDate=getdate(),SentUser='" & myStaff.StaffId & "',Response='" & strResponse _
                                & "' where RecId=" & .Cells("RecId").Value, Conn) Then
                            MsgBox("Đề nghị thông báo cho người lập trình RAS:" & vbNewLine & "Đã gửi được thông báo cho Cơ quan thuế nhưng không cập nhật được vào RAS cho dòng " & objRow.Index + 1)
                        End If
                    End With
                End If
            Next


        End If

        Return True
    End Function
    Public Function Receive(objNotice As XElement, strResponse As String) As Boolean
        Dim objConnect As New clsE_InvConnect(pblnTT78, mstrTvc)
        Dim objE_Invoice As New clsE_Invoice
        If objE_Invoice.ReceivedInvoiceErrors(objConnect.WsUrl, objConnect.UserName, objConnect.UserPass _
                                            , objConnect.AccountName, objConnect.AccountPass, strResponse, objNotice) Then
            Dim lstQuerries As New List(Of String)
            Dim strStatus As String = objE_Invoice.ReponseCode.Split(":")(1)
            For Each objRow As DataGridViewRow In dgrInvoices.Rows
                With objRow
                    lstQuerries.Add("Update lib.dbo.E_InvNotice set ProcessStatus='" & strStatus _
                                    & "' where RecId=" & .Cells("RecId").Value)
                End With
            Next
            If UpdateListOfQuerries(lstQuerries, Conn) Then
                Me.DialogResult = DialogResult.OK
            Else
                MsgBox("Đề nghị thông báo cho người lập trình RAS:" & vbNewLine _
                       & "Đã nhận được Process status từ Cơ quan thuế nhưng không cập nhật được vào RAS!")
            End If

        Else
            MsgBox("Không nhận được Process status từ Cơ quan thuế!")
        End If
        Return True
    End Function
    Private Sub lbkSend_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSend.LinkClicked

        Dim strPattern As String = String.Empty
        strPattern = dgrInvoices.CurrentRow.Cells("MauSo").Value
        If mblnSend Then
            If Send(CreateNotice, strPattern) Then
                Me.DialogResult = DialogResult.OK
            End If
        End If

    End Sub
    Private Function CheckInputValues() As Boolean
        If cboNoticeType.Text.StartsWith("2") Then
            If txtTaxOffcNoticeNbr.Text.Trim = "" Then
                MsgBox("Cần điền Số thông báo của CQT")
                Return False
            End If
        End If
        For Each objRow As DataGridViewRow In dgrInvoices.Rows
            If objRow.Cells("Reason").Value = "" Then
                MsgBox("Cần điền lý do cho Recid " & objRow.Cells("RecId").Value)
                Return False
            ElseIf objRow.Cells("TinhChat").Value = "" Then
                MsgBox("Cần chọn Tính chât cho Recid " & objRow.Cells("RecId").Value)
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub lbkDeleteRow_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkDeleteRow.LinkClicked
        dgrInvoices.Rows.Remove(dgrInvoices.CurrentRow)
    End Sub
End Class