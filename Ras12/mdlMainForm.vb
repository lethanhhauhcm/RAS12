Imports RAS12.MySharedFunctionsWzConn
Module mdlMainForm
    Private cmd As SqlClient.SqlCommand = Conn.CreateCommand

    Public Sub getTip()
        Try
            Dim fileReader As System.IO.StreamReader
            Dim stringReader As String = "", i As Int16 = 0, j As Int16 = 0
            fileReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\TipOfDay.txt")
            Randomize()
            Do While Not fileReader.EndOfStream
                fileReader.ReadLine()
                i = i + 1
            Loop
            fileReader.Close()
            j = Rnd() * i
            i = 0
            fileReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\TipOfDay.txt")
            Do While Not fileReader.EndOfStream
                stringReader = fileReader.ReadLine()
                i = i + 1
                If i = j Then Exit Do
            Loop
            fileReader.Close()
            frmMain.lblTip.Text = stringReader
        Catch ex As Exception
        End Try
    End Sub
    Public Sub HideFrame()
        'frmMain.PnlLogIn.Visible = False
        frmMain.PnlReport.Visible = False

    End Sub
    Public Sub InitPanel()
        Dim PosTop As Int16 = 25, PosLeft As Int16 = 1
        'frmMain.PnlLogIn.Top = PosTop
        'frmMain.PnlLogIn.Height = 80
        'frmMain.PnlLogIn.Left = PosLeft
        'frmMain.PnlLogIn.BackColor = pubVarBackColor

        frmMain.PnlReport.Top = PosTop
        frmMain.PnlReport.Left = PosLeft
        frmMain.PnlReport.BackColor = pubVarBackColor

        'frmMain.PnlChangePSW.Top = PosTop
        'frmMain.PnlChangePSW.Left = PosLeft
        'frmMain.PnlChangePSW.BackColor = pubVarBackColor
        getTip()
    End Sub
    Public Sub SetAllMenuStatus(ByVal varStatus As Boolean)
        Dim mnu As Object, MnuI As ToolStripItem
        For Each mnu In frmMain.MenuStrip1.Items
            mnu.Enabled = varStatus
            For Each MnuI In mnu.DropDownItems
                MnuI.Enabled = varStatus
            Next
        Next
        frmMain.PadSystem.Enabled = True
        frmMain.BarLogIn.Enabled = True
        frmMain.BarQuickRef.Enabled = True
    End Sub
    Public Sub LogInOut(ByVal parSIcode As String)
        Dim dTable As DataTable
        Dim mnu As Object, MnuI As ToolStripItem
        Dim lstDisableMenu As New List(Of String)
        If parSIcode = "" Then
            SetAllMenuStatus(False)
            frmMain.BarLogIn.Text = "Log In"
            cmd.CommandText = "update tbluser set status='OK' where status='ON' and sicode='" & myStaff.SICode & "'"
            cmd.ExecuteNonQuery()
        Else
            SetAllMenuStatus(True)
            frmMain.BarLogIn.Text = "Log Out"

            dTable = GetDataTable("select * from tblRight where SICode='" & parSIcode & "' and status='OK' and upper(Object)='MENU' ")
            For i As Int16 = 0 To dTable.Rows.Count - 1
                lstDisableMenu.Add(dTable.Rows(i)("SubObject").ToString.ToUpper)
            Next
            For Each mnu In frmMain.MenuStrip1.Items
                If lstDisableMenu.Contains(mnu.name.ToString.ToUpper) Then
                    mnu.enabled = False
                Else
                    mnu.enabled = True
                    For Each MnuI In mnu.DropDownItems
                        'If MnuI.Name = "BarApproveITP" Then
                        '    MsgBox("")
                        'End If
                        If lstDisableMenu.Contains(MnuI.Name.ToString.ToUpper) Then
                            MnuI.Enabled = False
                        End If
                    Next
                End If
            Next
        End If
        HideFrame()
    End Sub
    Public Sub GenComboValueMain()
        'LoadCmb_MSC(frmMain.CmbCity, "City")
        'LoadCmb_MSC(frmMain.CmbLocation, "Location")
        'LoadCmb_MSC(frmMain.CmbBiz, "BIZ")
        'frmMain.CmbBiz.SelectedIndex = frmMain.CmbBiz.Items.Count - 1
        'LoadCmb_MSC(frmMain.CmbCounter, "Counter")
        CutOverDatePPD = ScalarToDate("MISC", "VAL1", "CAT='CUTOVER' and VAL='PPD' ")
        CutOverDatePSP = ScalarToDate("MISC", "VAL1", "CAT='CUTOVER' and VAL='PSP' ")
        CutOverDateCloseRPT = ScalarToDate("MISC", "VAL1", "CAT='CUTOVER' and VAL='CloseRPT' ")
    End Sub
    Public Sub CopyDataGridViewToClipboard(ByRef dgv As DataGridView)
        Dim s As String = ""
        Dim oCurrentCol As DataGridViewColumn    'Get header
        oCurrentCol = dgv.Columns.GetFirstColumn(DataGridViewElementStates.Visible)
        Do
            s &= oCurrentCol.HeaderText & Chr(Keys.Tab)
            oCurrentCol = dgv.Columns.GetNextColumn(oCurrentCol,
               DataGridViewElementStates.Visible, DataGridViewElementStates.None)
        Loop Until oCurrentCol Is Nothing
        s = s.Substring(0, s.Length - 1)
        s &= Environment.NewLine    'Get rows
        For Each row As DataGridViewRow In dgv.Rows
            oCurrentCol = dgv.Columns.GetFirstColumn(DataGridViewElementStates.Visible)
            Do
                If row.Cells(oCurrentCol.Index).Value IsNot Nothing Then
                    s &= row.Cells(oCurrentCol.Index).Value.ToString
                End If
                s &= Chr(Keys.Tab)
                oCurrentCol = dgv.Columns.GetNextColumn(oCurrentCol,
                      DataGridViewElementStates.Visible, DataGridViewElementStates.None)
            Loop Until oCurrentCol Is Nothing
            s = s.Substring(0, s.Length - 1)
            s &= Environment.NewLine
        Next    'Put to clipboard
        Dim o As New DataObject
        o.SetText(s)
        Clipboard.SetDataObject(o, True)
    End Sub
End Module
