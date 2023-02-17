Public Class frmGetBankProvince
    Private mstrSelectedProvince As String
    Private mstrAddress As String
    Private mstrUsedBy As String
    Private mblnFirstLoadCompleted As Boolean

    Public Sub New(strUsedBy As String, strAddress As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mstrAddress = strAddress.Trim
        mstrUsedBy = strUsedBy
    End Sub

    Private Sub lbkSelect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkSelect.LinkClicked
        If dgrProvinceMap.CurrentRow IsNot Nothing Then
            mstrSelectedProvince = dgrProvinceMap.CurrentRow.Cells("Province").Value
            Me.DialogResult = DialogResult.OK
        End If

    End Sub

    Private Sub lbkCancel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbkCancel.LinkClicked
        Me.Dispose()
    End Sub

    Private Sub frmGetBankProvince_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Search()
        mblnFirstLoadCompleted = True
    End Sub
    Private Function Search() As Boolean
        Dim strQuerry As String = "Select 'N/A' as Province  union Select distinct b.Province from lib.dbo.BankBranches b left join lib.dbo.BankIDs i on b.BankId=i.RecId where i.Status='OK' and b.Status='OK'" _
                                & " and i.UsedBy='" & mstrUsedBy & "'"
        If Not chkShowAll.Checked Then
            Dim arrAddressBreaks As String() = mstrAddress.Split(" ")
            If arrAddressBreaks.Length > 2 Then
                mstrAddress = String.Join(" ", arrAddressBreaks, arrAddressBreaks.Length - 2, 2)
            End If
            AddLikeConditionString(strQuerry, "Province", mstrAddress)
        End If
        strQuerry = strQuerry & " order by Province"
        LoadDataGridView(dgrProvinceMap, strQuerry, Conn)
    End Function

    Private Sub chkShowAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowAll.CheckedChanged
        If mblnFirstLoadCompleted Then
            Search()
        End If
    End Sub
    Public Property SelectedProvince As String
        Get
            Return mstrSelectedProvince
        End Get
        Set(value As String)
            mstrSelectedProvince = value
        End Set
    End Property

End Class