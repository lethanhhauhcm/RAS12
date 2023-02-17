Public Class frmThirdPartyEdit
    Public Sub New(intPartyID As Integer, blnClone As Boolean)

        ' This call is required by the designer.
        InitializeComponent()
        Dim strCustTypes As String = String.Empty
        Select Case myStaff.Counter
            Case "TVS"
                strCustTypes = " And Val in ('TA','TO','CA','WK')"
            Case "CWT"
                strCustTypes = " And Val in ('LC','CS')"
        End Select

        ' Add any initialization after the InitializeComponent() call.
        LoadComboDisplay(cboCustShortName, "select distinct RecId as value,CustShortName as display" _
                                            & " from CustomerList where status='OK' and RecId in" _
                                            & " (select CustId from Cust_Detail where status='OK' and CAT='Channel' " _
                                            & strCustTypes & ")" _  '20220623 add 'WK' by 7643
                                            & " order by CustShortName", Conn)

        txtCounter.Text = myStaff.Counter

        If intPartyID = 0 Then
            cboCAT.SelectedIndex = 0
            cboCustShortName.SelectedIndex = -1
        Else
            Dim tblParty As DataTable = GetDataTable("Select top 1 * from Lib.dbo.ThirdParty where RecId=" & intPartyID)
            cboCAT.SelectedIndex = cboCAT.FindStringExact(tblParty.Rows(0)("CAT"))
            cboCAT.Enabled = False
            cboCustShortName.SelectedIndex = cboCustShortName.FindStringExact(tblParty.Rows(0)("CustShortName"))
            txtFullName.Text = tblParty.Rows(0)("FullName")
            txtBankName.Text = tblParty.Rows(0)("BankName")
            txtBankBranch.Text = tblParty.Rows(0)("BankBranch")
            txtBankAccount.Text = tblParty.Rows(0)("BankAccount")
            txtPhone.Text = tblParty.Rows(0)("Phone")
            txtEmail.Text = tblParty.Rows(0)("Email")

            If blnClone Then
                txtRecId.Text = 0
            Else
                txtRecId.Text = intPartyID
                txtFullName.Enabled = False
            End If
            If cboCAT.Text = "MF" Then
                cboCustShortName.Enabled = False
                cboPayType.Enabled = False  '20220623 add by 7643
            End If
        End If
        cboPayType.SelectedIndex = 0  '20220623 add by 7643
    End Sub

    Private Function CheckInputValues() As Boolean
        If Not CheckFormatTextBox(txtFullName,, 4, ) Then
            Return False
        ElseIf Not CheckFormatComboBox(cboCAT,, 2, 2) Then
            Return False
        ElseIf txtEmail.Text <> "" AndAlso Not txtEmail.Text.Contains("@") Then
            MsgBox("Invalid email!")
            txtEmail.Focus()
            Return False
        End If
        If cboCAT.Text = "KB" AndAlso Not CheckFormatComboBox(cboCustShortName,, 3, 64) Then
            MsgBox("Must Select Customer!")
            Return False
        ElseIf cboCAT.Text = "MF" AndAlso cboCustShortName.Text <> "" Then
            MsgBox("Must Not Select Customer!")
            Return False

        End If

        Return True
    End Function
    Private Sub lbkSave_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSave.LinkClicked
        If Not CheckInputValues() Then Exit Sub
        Dim lstQuerries As New List(Of String)
        Dim intCustId As Integer
        Dim strCustShortName As String = String.Empty
        Dim mPayType As String = ""  '20220623 add by 7643
        If cboCAT.Text = "KB" Then
            intCustId = cboCustShortName.SelectedValue
            strCustShortName = cboCustShortName.Text
            mPayType = cboPayType.Text  '20220623 add by 7643
        End If

        If txtRecId.Text = "0" Then
            lstQuerries.Add("insert into Lib.dbo.ThirdParty (CAT, FullName, CustId, CustShortName" _
                            & ", BankName, BankBranch, BankAccount" _
                            & ", Phone, Email, FstUser, City,PayType,Counter) values ('" _
                            & cboCAT.Text & "','" & txtFullName.Text _  '20220623 add PayType by 7643
                            & "'," & intCustId & ",'" & strCustShortName _
                            & "','" & txtBankName.Text & "','" & txtBankBranch.Text & "','" & txtBankAccount.Text _
                            & "','" & txtPhone.Text & "','" & txtEmail.Text _
                            & "','" & myStaff.SICode & "','" & myStaff.City & "','" & mPayType _
                            & "','" & txtCounter.Text & "')")  '20220623 add mPayType by 7643
        Else
            lstQuerries.Add("update lib.dbo.ThirdParty set LstUpdate=getdate(),LstUser='" & myStaff.SICode _
                            & "',FullName='" & txtFullName.Text & "',CustId=" & cboCustShortName.SelectedValue _
                            & ",CustShortName='" & cboCustShortName.Text & "',BankName='" & txtBankName.Text _
                            & "', BankBranch='" & txtBankBranch.Text & "', BankAccount='" & txtBankAccount.Text _
                            & "', Phone='" & txtPhone.Text & "', Email='" & txtEmail.Text _
                            & "', FstUser='" & myStaff.SICode & "', City='" & myStaff.City & "'" _
                            & ", PayType='" & cboPayType.Text & "',Counter='" & txtCounter.Text _
                            & "' where RecId=" & txtRecId.Text)
        End If
        If Not UpdateListOfQuerries(lstQuerries, Conn) Then
            MsgBox("Unable to update ThirdParty!")
            Exit Sub
        Else
            Me.DialogResult = DialogResult.OK
        End If

    End Sub

    Private Sub blkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles blkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub cboPayType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPayType.SelectedIndexChanged
        '20220623 add by 7643 -b-
        If cboPayType.SelectedIndex = 0 Then
            txtBankName.ReadOnly = False
            txtBankBranch.ReadOnly = False
            txtBankAccount.ReadOnly = False
        Else
            txtBankName.Text = ""
            txtBankBranch.Text = ""
            txtBankAccount.Text = ""

            txtBankName.ReadOnly = True
            txtBankBranch.ReadOnly = True
            txtBankAccount.ReadOnly = True
        End If
        '20220623 add by 7643 -e-
    End Sub
End Class