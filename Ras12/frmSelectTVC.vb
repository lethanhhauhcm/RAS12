Public Class frmSelectTVC

    Private mobjSelectedRow As DataGridViewRow
    Private mblnFirstLoadCompleted As Boolean
    Private mstrInvSettingTable As String = "lib.dbo.E_invSettings"
    Private mstrInvoiceTable As String = "lib.dbo.E_Inv"
    Private mstrInvDetailTable As String = "lib.dbo.E_InvDetails"
    Private mstrInvLinkTable As String = "lib.E_InvLinks"
    Private mstrMauSo As String
    Private mstrAl As String
    Public Sub New(Optional strTVC As String = "", Optional strMauSo As String = "" _
                   , Optional strAL As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim strQuerry As String
        mstrMauSo = strMauSo
        mstrAl = strAL
        If pblnTT78 Then
            mstrInvoiceTable = mstrInvoiceTable & "78"
            mstrInvDetailTable = mstrInvDetailTable & "78"
            mstrInvLinkTable = mstrInvLinkTable & "78"
            mstrInvSettingTable = mstrInvSettingTable & "78"
        End If
        strQuerry = "Select distinct TVC as value from " & mstrInvSettingTable _
            & " where status='OK'"

        If strTVC <> "" Then
            strQuerry = strQuerry & " and Tvc='" & strTVC & "'"
        End If

        If strMauSo <> "" Then
            If strMauSo.EndsWith("001") Then
                strQuerry = strQuerry & " and right(MauSo,3)='001'"
            Else
                strQuerry = strQuerry & " and right(MauSo,3)<>'001'"
            End If
        End If
        If mstrAl <> "" Then
            strQuerry = strQuerry & " and AL='" & strAL & "'"
        End If


        LoadCombo(cboTVC, strQuerry & " order by TVC", Conn)
            cboTVC.SelectedIndex = 0
        'Else
        '    cboTVC.Items.Add(strTVC)
        '    cboTVC.SelectedIndex = 0
        'End If



    End Sub

    Private Sub frmShowTableContent_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Search()
        mblnFirstLoadCompleted = True
    End Sub

    Private Function Search() As Boolean
        Dim strQuerry As String = "Select TVC, Biz, AL, Template, MauSo, KyHieu" _
                                            & " from " & mstrInvSettingTable & " where Status='OK'"
        AddEqualConditionCombo(strQuerry, cboTVC)
        If mstrMauSo <> "" Then
            If mstrMauSo.EndsWith("001") Then
                strQuerry = strQuerry & " and right(MauSo,3)='001'"
            Else
                strQuerry = strQuerry & " and right(MauSo,3)<>'001'"
            End If
        End If
        If mstrAl <> "" Then
            strQuerry = strQuerry & " and AL='" & mstrAl & "'"
        End If
        strQuerry = strQuerry & " order by TVC,MauSo,KyHieu,Biz,AL"
        LoadDataGridView(dgrInput, strQuerry, Conn)
        If dgrInput.Rows.Count = 1 Then
            SelectTvc()
        End If
        Return True
    End Function

    Public Property SelectedRow As DataGridViewRow
        Get
            Return mobjSelectedRow
        End Get
        Set(value As DataGridViewRow)
            mobjSelectedRow = value
        End Set
    End Property

    Private Sub cboTVC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTVC.SelectedIndexChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub

    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        SelectTvc()
    End Sub
    Private Function SelectTvc() As Boolean
        mobjSelectedRow = dgrInput.CurrentRow
        Me.DialogResult = DialogResult.OK
        Return True
    End Function
End Class