Imports System.Data.SqlClient
Module LibStr
    Private FProgressForm, FDgvForm As Form

    Public Property LDGVForm As Form
        Get
            Return FDgvForm
        End Get
        Set(value As Form)
            FDgvForm = value
        End Set
    End Property

    Public Sub FillDatagridview(ByRef xDs As DataSet, ByRef xSda As SqlDataAdapter, xSql As String, xDgv As DataGridView, xClsTvcs As clsTvcs)
        xSda = New SqlDataAdapter(xSql, xClsTvcs.Connection)
        xSda.Fill(xDs)
        xDgv.DataSource = xDs.Tables(0)
    End Sub

    Public Sub PressInteger(e As KeyPressEventArgs)
        If (Not Char.IsControl(e.KeyChar) _
            AndAlso (Not Char.IsDigit(e.KeyChar))) Then
            e.Handled = True
        End If
    End Sub

    Public Sub IniProgress(xTitle As String, xMax As Integer)
        Dim mProgress As New ProgressBar

        FProgressForm = New Form
        FProgressForm.Text = xTitle
        FProgressForm.Size = New Size(800, 60)
        FProgressForm.StartPosition = FormStartPosition.CenterScreen

        mProgress = New ProgressBar
        mProgress.Parent = FProgressForm
        mProgress.Dock = DockStyle.Fill
        mProgress.Value = mProgress.Minimum
        mProgress.Maximum = xMax
        mProgress.Step = 1
    End Sub

    Public Sub RunProgress()
        Dim mPb As New ProgressBar

        mPb = CType(FProgressForm.Controls(0), ProgressBar)
        mPb.PerformStep()
        FProgressForm.Show()

        If mPb.Value = mPb.Maximum Then
            FProgressForm.Close()
        End If
    End Sub

    Public Sub IniDgv(xTitle As String, xColumns() As String, xFormat() As String)
        Dim mDgv As New DataGridView
        Dim i As Integer

        FDgvForm = New Form
        FDgvForm.Text = xTitle
        FDgvForm.Size = New Size(500, 600)
        FDgvForm.StartPosition = FormStartPosition.CenterScreen

        mDgv.Parent = FDgvForm
        mDgv.Dock = DockStyle.Fill
        mDgv.AllowUserToAddRows = False
        mDgv.AllowUserToDeleteRows = False
        mDgv.ReadOnly = True
        For i = 0 To xColumns.Length - 1
            mDgv.Columns.Add(xColumns(i), xColumns(i))
        Next

        For i = 0 To xFormat.Length - 1
            If xFormat(i) = "N" Then
                mDgv.Columns(i).DefaultCellStyle.Format = DgvNumberFormat(mDgv, mDgv.Columns(i).Name)
            ElseIf xFormat(i) = "D" Then
                mDgv.Columns(i).DefaultCellStyle.Format = "dd/MM/yyyy"
            ElseIf xFormat(i) = "DT" Then
                mDgv.Columns(i).DefaultCellStyle.Format = "dd/MM/yyyy hh:mm:ss"
            End If
        Next
    End Sub

    Public Sub CommitDataset(xDs As DataSet, xSda As SqlDataAdapter, xUpdateFields() As String, xValues() As Object)
        Dim mDs As New DataSet
        Dim mScb As New SqlCommandBuilder
        Dim i As Integer

        mDs = xDs.GetChanges()
        If mDs IsNot Nothing Then
            For i = 0 To mDs.Tables(0).Rows.Count - 1
                If mDs.Tables(0).Rows(i).RowState = DataRowState.Deleted Then
                    mDs.Tables(0).Rows(i).RejectChanges()
                    mDs.Tables(0).Rows(i).SetModified()

                    mDs.Tables(0).Rows(i).Item(xUpdateFields(0)) = xValues(0)
                    mDs.Tables(0).Rows(i).Item(xUpdateFields(1)) = xValues(1)
                    mDs.Tables(0).Rows(i).Item("Status") = "XX"
                End If
            Next

            mScb = New SqlCommandBuilder(xSda)
            xSda.Update(mDs)
            xDs.AcceptChanges()
        End If
    End Sub

    Public Function DefaultID(xField As String, xTable As String, xclsTvcs As clsTvcs) As String
        Dim mSQL As String
        Dim mSeq As Integer

        mSQL = "select isnull(max(" & xField & "),0) MID " &
               "from " & xTable & " TmpTable "
        mSeq = Integer.Parse(xclsTvcs.GetScalarAsString(mSQL)) + 1

        Return mSeq.ToString
    End Function

    Public Sub DefaultControl(xControl As Control, Optional xDgv As DataGridView = Nothing, Optional xZero As Boolean = False)
        Dim i As Integer
        Dim mName As String

        If (xDgv Is Nothing) OrElse (xDgv.CurrentRow Is Nothing) Then
            For i = 0 To xControl.Controls.Count - 1
                If TypeOf xControl.Controls(i) Is TextBox Then
                    CType(xControl.Controls(i), TextBox).Text = IIf(xZero = True, "0", "")
                ElseIf TypeOf xControl.Controls(i) Is DateTimePicker Then
                    CType(xControl.Controls(i), DateTimePicker).Value = Now
                ElseIf TypeOf xControl.Controls(i) Is CheckBox Then
                    CType(xControl.Controls(i), CheckBox).Checked = False
                ElseIf TypeOf xControl.Controls(i) Is ComboBox Then
                    If CType(xControl.Controls(i), ComboBox).Items.Count > 0 Then
                        CType(xControl.Controls(i), ComboBox).SelectedIndex = 0
                    End If
                End If
            Next
        Else
            For i = 0 To xControl.Controls.Count - 1
                mName = ""
                If TypeOf xControl.Controls(i) Is TextBox Then
                    mName = Replace(xControl.Controls(i).Name, "txt", "")
                    If xDgv.CurrentRow.Cells(mName).Value IsNot Nothing Then
                        If IsNumeric(xDgv.CurrentRow.Cells(mName).Value) Then
                            CType(xControl.Controls(i), TextBox).Text = Format(xDgv.CurrentRow.Cells(mName).Value, "#,##0")
                        Else
                            CType(xControl.Controls(i), TextBox).Text = xDgv.CurrentRow.Cells(mName).Value
                        End If
                    End If
                ElseIf TypeOf xControl.Controls(i) Is DateTimePicker Then
                    mName = Replace(xControl.Controls(i).Name, "dtp", "")
                    If xDgv.CurrentRow.Cells(mName).Value IsNot Nothing Then
                        CType(xControl.Controls(i), DateTimePicker).Value = xDgv.CurrentRow.Cells(mName).Value
                    End If
                ElseIf TypeOf xControl.Controls(i) Is CheckBox Then
                    mName = Replace(xControl.Controls(i).Name, "chk", "")
                    If xDgv.CurrentRow.Cells(mName).Value IsNot Nothing Then
                        CType(xControl.Controls(i), CheckBox).Checked = xDgv.CurrentRow.Cells(mName).Value
                    End If
                ElseIf TypeOf xControl.Controls(i) Is ComboBox Then
                    mName = Replace(xControl.Controls(i).Name, "cbo", "")
                    If (CType(xControl.Controls(i), ComboBox).Items.Count > 0) AndAlso (xDgv.CurrentRow.Cells(mName).Value IsNot Nothing) Then
                        CType(xControl.Controls(i), ComboBox).SelectedValue = xDgv.CurrentRow.Cells(mName).Value
                    End If
                End If
            Next
        End If
    End Sub

    Public Function DgvNumberFormat(xDgv As DataGridView, xField As String) As String
        xDgv.Columns(xField).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        xDgv.Columns(xField).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        Return "#,##0"
    End Function

    Public Enum Action
        Initialize
        Add
        Edit
        Search
        SelectChange
    End Enum

    Public Enum TabPageList
        List
        Input
        Search
    End Enum

    Public Function GetUser() As String()
        Dim mFile, mArrStr() As String

        mFile = "D:\7643\VB Project\General\DefaultUser.txt"
        If My.Computer.FileSystem.FileExists(mFile) Then
            mArrStr = Split(My.Computer.FileSystem.ReadAllText(mFile), vbCrLf)
        End If

        Return mArrStr
    End Function
End Module
