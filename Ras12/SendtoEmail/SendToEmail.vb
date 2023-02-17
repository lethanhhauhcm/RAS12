Imports RAS12.MySharedFunctions
Imports System.IO
Imports System.Text.RegularExpressions
Public Class SendToEmail
    Private cmd As SqlClient.SqlCommand = Conn.CreateCommand
    Private Sub SendToEmail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Conn.State <> ConnectionState.Open Then Conn.Open()
        CbbWhereEmail.DataSource = GetDataTable("select distinct RMK from FT.dbo.MISC where CAT='EMAILTO'")
        CbbWhereEmail.DisplayMember = "RMK"
        CbbWhereEmail.Text = myStaff.City
        LoadGridEmail(CbbWhereEmail.Text)

    End Sub

    Private Sub CbbWhereEmail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbbWhereEmail.SelectedIndexChanged
        LoadGridEmail(CbbWhereEmail.Text)
    End Sub

    Private Sub LoadGridEmail(RMK As String)
        GridEmail.DataSource = GetDataTable("select RecID, CAT, VAL, Val1 as Notify, Details as Email , RMK as City , LstRun, convert(varchar,LstRun, 103) Times from FT.dbo.MISC where CAT='EMAILTO' and RMK = '" & RMK & "'")
        GridEmail.Columns("VAL").Width = "35"
        GridEmail.Columns("City").Width = "35"
        GridEmail.Columns("Notify").Width = "275"
        GridEmail.Columns("Times").Width = "110"
        GridEmail.Columns("Email").Width = "275"
        GridEmail.Columns("RecID").Visible = False
        GridEmail.Columns("Cat").Visible = False
        GridEmail.Columns("LstRun").Visible = False
    End Sub


    Private Sub GridEmail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridEmail.CellClick
        Dim Index As Integer = GridEmail.CurrentCell.RowIndex
        If tbVal.Text.ToString() = "" Or tbVal.Text.ToString() <> GridEmail.Rows(Index).Cells("Val").Value.ToString() Then
            tbVal.Text = GridEmail.Rows(Index).Cells("Val").Value.ToString()
            TbDetails.Text = GridEmail.Rows(Index).Cells("Email").Value.ToString()
            TbNotify.Text = GridEmail.Rows(Index).Cells("Notify").Value.ToString()
            cbCity.Text = GridEmail.Rows(Index).Cells("City").Value.ToString()

        Else
            GridEmail.Rows(Index).Selected = False
            tbVal.Text = ""
            TbDetails.Text = ""
            TbNotify.Text = ""
            cbCity.Text = CbbWhereEmail.Text
        End If
    End Sub

    Function Check_error(IsVal As Integer, IsDetails As Integer, Optional Button As Integer = 0)
        If Button = 0 Then
            If IsVal = 1 Then
                Dim n As Integer
                If tbVal.Text.ToString() = "" Then
                    Return 0
                End If
                If Not Integer.TryParse(tbVal.Text.ToString(), n) Then
                    MsgBox("Dữ liệu phải được định dạng số ")
                    tbVal.Select()
                ElseIf Integer.Parse(tbVal.Text.ToString()) > 0 Then
                    MsgBox("Dữ liệu phải nhỏ hơn 0 ")
                    tbVal.Select()
                End If
            End If
            If IsDetails = 1 Then
#Disable Warning RE0001 ' Regex issue: {0}
                Dim pattern As String = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                Dim email As New Regex(pattern)
#Enable Warning RE0001 ' Regex issue: {0}
                If TbDetails.Text.ToString() = "" Then
                    Return 0
                End If
                Dim Details As String = TbDetails.Text.ToString()
                Do While Details.IndexOf(";") >= 0
                    If Not email.IsMatch(Details.Substring(0, Details.IndexOf(";"))) Then
                        MsgBox("Định dạng Email không đúng")
                        TbDetails.Select()
                        Return 0
                    End If
                    Details = Details.Substring(Details.IndexOf(";") + 1, Details.Length - Details.IndexOf(";") - 1)
                Loop
                If Not email.IsMatch(Details) Then
                    MsgBox("Định dạng Email không đúng")
                    TbDetails.Select()
                    Return 0
                End If
            End If
        Else
            If tbVal.Text.ToString() = "" Then
                MsgBox("Dữ liệu cột VAL không được để trống")
                tbVal.Select()
                Return 0
            End If
            If TbDetails.Text.ToString() = "" Then
                MsgBox("Dữ liệu cột Email không được để trống")
                TbDetails.Select()
                Return 0
            End If
            If GetDataTable("select * from FT.dbo.MISC where CAT = 'EMAILTO' and Val = " & tbVal.Text.ToString() & " and RMK = '" & cbCity.Text.ToString() & "'").Rows.Count = 0 Then
                Return 1
            Else Return 2
            End If
        End If
    End Function

    Private Sub tbVal_Leave(sender As Object, e As EventArgs) Handles tbVal.Leave
        Check_error(1, 0, 0)
    End Sub

    Private Sub TbDetails_Leave(sender As Object, e As EventArgs) Handles TbDetails.Leave
        Check_error(0, 1, 0)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
        Dim n As Integer = Check_error(0, 0, 1)
        If n = 1 Then
            cmd.CommandText = "insert into FT.dbo.MISC(CAT,Val1,Val,Details,RMK,LstRun) values('EMAILTO',@Notify,@Val,@Details,@RMK,GETDATE())"
            cmd.Parameters.Add("@Val", SqlDbType.Int).Value = Integer.Parse(tbVal.Text.ToString())
            cmd.Parameters.Add("@Details", SqlDbType.VarChar).Value = TbDetails.Text.ToString()
            cmd.Parameters.Add("@RMK", SqlDbType.VarChar).Value = cbCity.Text.ToString()
            cmd.Parameters.Add("@Notify", SqlDbType.VarChar).Value = TbNotify.Text.ToString()
            cmd.ExecuteNonQuery()
            MsgBox("Thêm thành công")
            LoadGridEmail(CbbWhereEmail.Text)
            tbVal.Text = ""
            TbDetails.Text = ""
            TbNotify.Text = ""
            cbCity.Text = CbbWhereEmail.Text
        ElseIf n = 2 Then
            MsgBox("Trùng dữ liệu VAL")
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim n As Integer = Check_error(0, 0, 1)
        If n = 1 Then
            MsgBox("Không có dữ liệu tương ứng để xóa")
            Return
        Else
            Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
            cmd.CommandText = "delete FT.dbo.MISC where Val = @Val And CAT = 'EMAILTO' and RMK = @RMK"
            cmd.Parameters.Add("@Val", SqlDbType.Int).Value = Integer.Parse(tbVal.Text.ToString())
            cmd.Parameters.Add("@RMK", SqlDbType.VarChar).Value = cbCity.Text.ToString()
            cmd.ExecuteNonQuery()
            MsgBox("Xóa thành công")
            LoadGridEmail(CbbWhereEmail.Text)
            tbVal.Text = ""
            TbDetails.Text = ""
            TbNotify.Text = ""
            cbCity.Text = CbbWhereEmail.Text
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim n As Integer = Check_error(0, 0, 1)
        If n = 1 Then
            MsgBox("Không có dữ liệu tương ứng để cập nhật")
            Return
        ElseIf n = 2 Then
            Dim cmd As SqlClient.SqlCommand = Conn.CreateCommand
            cmd.CommandText = "update FT.dbo.MISC Set Val1 = @Notify,Details = @Details where Val = @Val And CAT = 'EMAILTO' and RMK = @RMK"
            cmd.Parameters.Add("@Val", SqlDbType.Int).Value = Integer.Parse(tbVal.Text.ToString())
            cmd.Parameters.Add("@Details", SqlDbType.VarChar).Value = TbDetails.Text.ToString()
            cmd.Parameters.Add("@RMK", SqlDbType.VarChar).Value = cbCity.Text.ToString()
            cmd.Parameters.Add("@Notify", SqlDbType.VarChar).Value = TbNotify.Text.ToString()
            cmd.ExecuteNonQuery()
            MsgBox("Đã chỉnh sửa")
            LoadGridEmail(CbbWhereEmail.Text)
            tbVal.Text = ""
            TbDetails.Text = ""
            TbNotify.Text = ""
            cbCity.Text = CbbWhereEmail.Text
        End If
    End Sub
End Class
