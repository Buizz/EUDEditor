
Public Class PluginForm
    '이곳에는 무엇을 하냐면.
    'eds파일 작성 도우미.

    'dataDumper
    '파일명 : 오프셋
    'copy

    'unpatchable



    'grpwire.grp
    'tranwire.grp
    'wirefram.grp
    'cmdicons.grp
    'stat_txt.tbl

    'AIscript.bin
    'iscript.bin

    'arrow.grp
    'drag.grp
    'illegal.grp



    'grpinjector

    '[iscriptPatcher]
    'iscript : 파일명


    'unpatcher
    'resetCond : []


    'soundstopper
    'scmloader
    'noAirColsion
    'unlimiter
    'keepSTR
    'eudTurbo

    Dim loadstatus As Boolean = False
    Dim type() As String = {"사용안함", "사용(기본)", "사용(copy)", "사용(unpatcher)"}



    Private Sub PluginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)
        'REMASTER 체크
        If ProgramSet.StarVersion = "Remastered" Then
            MsgBox("일부 기타 플러그인이 작동하지 않을 수 있습니다.", MsgBoxStyle.Exclamation)

            GroupBox1.Visible = False
            GroupBox2.Visible = False
            GroupBox3.Visible = False
            'GroupBox4.Visible = False
        Else
            GroupBox1.Visible = True
            GroupBox2.Visible = True
            GroupBox3.Visible = True
            'GroupBox4.Visible = True
        End If



        RichTextBox1.Text = extraedssetting

        DataGridView2.Rows.Clear()
        DataGridView2.Rows.Add("grpwire.grp", dataDumper_grpwire.Split("\").Last)
        DataGridView2.Item(2, 0).Value = type(dataDumper_grpwire_f)
        DataGridView2.Rows.Add("tranwire.grp", dataDumper_tranwire.Split("\").Last)
        DataGridView2.Item(2, 1).Value = type(dataDumper_tranwire_f)
        DataGridView2.Rows.Add("wirefram.grp", dataDumper_wirefram.Split("\").Last)
        DataGridView2.Item(2, 2).Value = type(dataDumper_wirefram_f)
        DataGridView2.Rows.Add("cmdicons.grp", dataDumper_cmdicons.Split("\").Last)
        DataGridView2.Item(2, 3).Value = type(dataDumper_cmdicons_f)
        DataGridView2.Rows.Add("stat_txt.tbl", dataDumper_stat_txt.Split("\").Last)
        If dataDumper_stat_txt_f <> 0 Then
            dataDumper_stat_txt_f = 2
        End If


        DataGridView2.Item(2, 4).Value = type(dataDumper_stat_txt_f)

        DataGridView2.Rows.Add("AIscript.bin", dataDumper_AIscript.Split("\").Last)
        DataGridView2.Item(2, 5).Value = type(dataDumper_AIscript_f)
        DataGridView2.Rows.Add("iscript.bin", dataDumper_iscript.Split("\").Last)
        DataGridView2.Item(2, 6).Value = type(dataDumper_iscript_f)


        DataGridView1.Rows.Clear()
        DataGridView1.Rows.Add("arrow.grp", grpinjector_arrow.Split("\").Last, "삭제")
        DataGridView1.Rows.Add("drag.grp", grpinjector_drag.Split("\").Last, "삭제")
        DataGridView1.Rows.Add("illegal.grp", grpinjector_illegal.Split("\").Last, "삭제")


        'dataDumper_grpwire = FindSetting(Section_PluginSET, "dataDumper_grpwire")
        'dataDumper_tranwire = FindSetting(Section_PluginSET, "dataDumper_tranwire")
        'dataDumper_wirefram = FindSetting(Section_PluginSET, "dataDumper_wirefram")
        'dataDumper_cmdicons = FindSetting(Section_PluginSET, "dataDumper_cmdicons")
        'dataDumper_stat_txt = FindSetting(Section_PluginSET, "dataDumper_stat_txt")
        'dataDumper_AIscript = FindSetting(Section_PluginSET, "dataDumper_AIscript")
        'dataDumper_iscript = FindSetting(Section_PluginSET, "dataDumper_iscript")

        'dataDumper_grpwire_f = FindSetting(Section_PluginSET, "dataDumper_grpwire_f")
        'dataDumper_tranwire_f = FindSetting(Section_PluginSET, "dataDumper_tranwire_f")
        'dataDumper_wirefram_f = FindSetting(Section_PluginSET, "dataDumper_wirefram_f")
        'dataDumper_cmdicons_f = FindSetting(Section_PluginSET, "dataDumper_cmdicons_f")
        'dataDumper_stat_txt_f = FindSetting(Section_PluginSET, "dataDumper_stat_txt_f")
        'dataDumper_AIscript_f = FindSetting(Section_PluginSET, "dataDumper_AIscript_f")
        'dataDumper_iscript_f = FindSetting(Section_PluginSET, "dataDumper_iscript_f")


        EasyCompletionComboBox1.Items.Clear()
        EasyCompletionComboBox1.Items.AddRange(CODE(DTYPE.units).ToArray)
        Try
            Dim num As Integer = nqcunit
            EasyCompletionComboBox1.SelectedIndex = nqcunit
        Catch ex As Exception
            EasyCompletionComboBox1.SelectedIndex = 58
        End Try


        ListBox1.AutoSize = True

        ListBox1.Items.Clear()
        ListBox1.Items.AddRange(unpatcher.Split(New String() {"##"}, StringSplitOptions.RemoveEmptyEntries))


        ListBox2.AutoSize = True

        ListBox2.Items.Clear()
        ListBox2.Items.AddRange(dataDumper_user.Split(New String() {"##"}, StringSplitOptions.RemoveEmptyEntries))



        ListBox3.Items.Clear()


        Dim unitlist As New List(Of String)
        For i = 0 To CODE(0).Count - 1
            If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i) = 0 Then
                unitlist.Add(CODE(0)(i))
            Else
                Try
                    unitlist.Add(ProjectSet.CHKSTRING(-1 + ProjectSet.CHKUNITNAME(i)) & "(" & CODE(0)(i) & ")")
                Catch ex As Exception
                    unitlist.Add(CODE(0)(i))
                End Try

            End If
        Next

        Dim nqcstring() As String = nqccommands.Split({"\"}, StringSplitOptions.RemoveEmptyEntries)

        For i = 0 To nqcstring.Count - 1
            Dim tstring() As String = nqcstring(i).Split("#")

            ListBox3.Items.Add(tstring(0) & " : " & unitlist(tstring(1)) & ", " & tstring(2))
        Next



        ListBox3.Size = New Size(ListBox3.Size.Width, 4 + 15 * ListBox3.Items.Count)


        loadstatus = True
        objectStatusReset()
    End Sub
    Private Sub PluginForm_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        extraedssetting = RichTextBox1.Text

        DataGridView2.EndEdit()

        unpatcher = ""

        For i = 0 To ListBox1.Items.Count - 1
            unpatcher = unpatcher & ListBox1.Items(i) & "##"
        Next

        If ListBox1.Items.Count > 0 Then
            unpatcher = Mid(unpatcher, 1, unpatcher.Length - 2)
        End If

        dataDumper_user = ""

        For i = 0 To ListBox2.Items.Count - 1
            dataDumper_user = dataDumper_user & ListBox2.Items(i) & "##"
        Next

        If ListBox2.Items.Count > 0 Then
            dataDumper_user = Mid(dataDumper_user, 1, dataDumper_user.Length - 2)
        End If
    End Sub

    Private Sub objectStatusReset()
        TextBox2.Text = iscriptPatcher

        CheckedListBox1.SetItemChecked(0, soundstopper)
        CheckedListBox1.SetItemChecked(1, scmloader)
        CheckedListBox1.SetItemChecked(2, noAirCollision)
        CheckedListBox1.SetItemChecked(3, unlimiter)
        CheckedListBox1.SetItemChecked(4, keepSTR)
        CheckedListBox1.SetItemChecked(5, eudTurbo)


        CheckBox2.Checked = iscriptPatcheruse
        FlowLayoutPanel2.Visible = iscriptPatcheruse


        CheckBox1.Checked = unpatcheruse
        FlowLayoutPanel1.Visible = unpatcheruse


        CheckBox3.Checked = dataDumperuse
        FlowLayoutPanel4.Visible = dataDumperuse

        CheckBox4.Checked = grpinjectoruse
        FlowLayoutPanel5.Visible = grpinjectoruse

        If nqcuse Then
            If CheckFileExist(ProgramSet.euddraftDirec.Replace("euddraft.exe", "") & "plugins\MurakamiShiinaQC.py") Then
                MsgBox("다음 파일이 존재하지 않습니다." & vbCrLf & ProgramSet.euddraftDirec.Replace("euddraft.exe", "") & "plugins\MurakamiShiinaQC.py" & vbCrLf & "다운로드 버튼을 눌러 해당 위치에 파일을 넣으세요.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                nqcuse = False
            End If
        End If
        CheckBox5.Checked = nqcuse
        FlowLayoutPanel10.Visible = nqcuse
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dialog As DialogResult
        dialog = OpenFileDialog1.ShowDialog()
        If dialog = DialogResult.OK Then
            iscriptPatcher = OpenFileDialog1.FileName
            objectStatusReset()
        End If
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        Select Case e.Index
            Case 0
                soundstopper = e.NewValue
            Case 1
                scmloader = e.NewValue
            Case 2
                noAirCollision = e.NewValue
            Case 3
                unlimiter = e.NewValue
            Case 4
                keepSTR = e.NewValue
            Case 5
                eudTurbo = e.NewValue
        End Select
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        iscriptPatcheruse = CheckBox2.Checked
        objectStatusReset()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        unpatcheruse = CheckBox1.Checked
        objectStatusReset()
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        dataDumperuse = CheckBox3.Checked
        objectStatusReset()
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        grpinjectoruse = CheckBox4.Checked
        objectStatusReset()
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        nqcuse = CheckBox5.Checked
        objectStatusReset()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PluginTriggerAddForm.TriggerString = ""


        Dim dialog As DialogResult = PluginTriggerAddForm.ShowDialog()

        If dialog = DialogResult.OK Then
            ListBox1.Items.Add(PluginTriggerAddForm.TriggerString)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox1.SelectedIndex <> -1 Then
            PluginTriggerAddForm.TriggerString = ListBox1.Items(ListBox1.SelectedIndex)
            Dim dialog As DialogResult = PluginTriggerAddForm.ShowDialog()


            If dialog = DialogResult.OK Then
                ListBox1.Items(ListBox1.SelectedIndex) = PluginTriggerAddForm.TriggerString
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ListBox1.SelectedIndex <> -1 Then
            Dim lastindex As UInteger = ListBox1.SelectedIndex

            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)

            If ListBox1.Items.Count <= lastindex Then
                ListBox1.SelectedIndex = ListBox1.Items.Count - 1
            Else
                ListBox1.SelectedIndex = lastindex
            End If

            ListBox1.Height = 0
        End If
    End Sub




    WithEvents cb As ComboBox
    Private Sub DataGridView2_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView2.EditingControlShowing
        cb = TryCast(e.Control, ComboBox)

    End Sub

    Private Sub cb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb.SelectedIndexChanged
        If Dgrid2point.X = 2 Then

            Select Case Dgrid2point.Y
                Case 0 'grpwire.grp
                    dataDumper_grpwire_f = cb.SelectedIndex
                    If cb.SelectedIndex = 0 Then
                        DataGridView2.Item(1, Dgrid2point.Y).Value = ""
                        dataDumper_grpwire = ""
                    ElseIf dataDumper_grpwire = "" Then
                        If grpopen(Dgrid2point.X, Dgrid2point.Y) = DialogResult.Cancel Then
                            DataGridView2.Item(2, Dgrid2point.Y).Value = type(0)
                            cb.SelectedIndex = 0
                        End If
                    End If
                Case 1 'tranwire.grp
                    dataDumper_tranwire_f = cb.SelectedIndex
                    If cb.SelectedIndex = 0 Then
                        DataGridView2.Item(1, Dgrid2point.Y).Value = ""
                        dataDumper_tranwire = ""
                    ElseIf dataDumper_tranwire = "" Then
                        If grpopen(Dgrid2point.X, Dgrid2point.Y) = DialogResult.Cancel Then
                            DataGridView2.Item(2, Dgrid2point.Y).Value = type(0)
                            cb.SelectedIndex = 0
                        End If
                    End If

                Case 2 'wirefram.grp
                    dataDumper_wirefram_f = cb.SelectedIndex
                    If cb.SelectedIndex = 0 Then
                        DataGridView2.Item(1, Dgrid2point.Y).Value = ""
                        dataDumper_wirefram = ""
                    ElseIf dataDumper_wirefram = "" Then
                        If grpopen(Dgrid2point.X, Dgrid2point.Y) = DialogResult.Cancel Then
                            DataGridView2.Item(2, Dgrid2point.Y).Value = type(0)
                            cb.SelectedIndex = 0
                        End If
                    End If
                Case 3 'cmdicons.grp
                    dataDumper_cmdicons_f = cb.SelectedIndex
                    If cb.SelectedIndex = 0 Then
                        DataGridView2.Item(1, Dgrid2point.Y).Value = ""
                        dataDumper_cmdicons = ""
                    ElseIf dataDumper_cmdicons = "" Then
                        If grpopen(Dgrid2point.X, Dgrid2point.Y) = DialogResult.Cancel Then
                            DataGridView2.Item(2, Dgrid2point.Y).Value = type(0)
                            cb.SelectedIndex = 0
                        End If
                    End If
                Case 4 'stat_txt.tbl
                    dataDumper_stat_txt_f = cb.SelectedIndex
                    If cb.SelectedIndex = 0 Then
                        DataGridView2.Item(1, Dgrid2point.Y).Value = ""
                        dataDumper_stat_txt = ""
                        EmptyMemory(DataName.stat_txt)
                    ElseIf dataDumper_stat_txt = "" Then
                        If grpopen(Dgrid2point.X, Dgrid2point.Y) = DialogResult.Cancel Then
                            DataGridView2.Item(2, Dgrid2point.Y).Value = type(0)
                            cb.SelectedIndex = 0
                        End If
                    End If
                    If dataDumper_stat_txt_f <> 0 Then
                        dataDumper_stat_txt_f = 2
                        cb.SelectedIndex = 2
                        DataGridView2.Item(2, Dgrid2point.Y).Value = type(2)
                    End If
                Case 5 'AIscript.bin
                    dataDumper_AIscript_f = cb.SelectedIndex
                    If cb.SelectedIndex = 0 Then
                        DataGridView2.Item(1, Dgrid2point.Y).Value = ""
                        dataDumper_AIscript = ""
                    ElseIf dataDumper_AIscript = "" Then
                        If grpopen(Dgrid2point.X, Dgrid2point.Y) = DialogResult.Cancel Then
                            DataGridView2.Item(2, Dgrid2point.Y).Value = type(0)
                            cb.SelectedIndex = 0
                        End If
                    End If
                Case 6 'iscript.bin
                    dataDumper_iscript_f = cb.SelectedIndex
                    If cb.SelectedIndex = 0 Then
                        DataGridView2.Item(1, Dgrid2point.Y).Value = ""
                        dataDumper_iscript = ""
                    ElseIf dataDumper_iscript = "" Then
                        If grpopen(Dgrid2point.X, Dgrid2point.Y) = DialogResult.Cancel Then
                            DataGridView2.Item(2, Dgrid2point.Y).Value = type(0)
                            cb.SelectedIndex = 0
                        End If
                    End If
            End Select


        End If


        '   DataGridView2.Columns.


    End Sub


    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellEnter
        Dgrid2point = New Point(e.ColumnIndex, e.RowIndex)
    End Sub

    Dim Dgrid2point As Point

    Private Function grpopen(colum As Byte, row As Byte) As DialogResult
        Dim dialog As DialogResult


        Select Case row
            Case 0 'grpwire.grp
                OpenFileDialog2.Filter = "GRP File|*.grp"
                dialog = OpenFileDialog2.ShowDialog

                If dialog = DialogResult.OK Then
                    If CheckGRPFile(OpenFileDialog2.FileName) = False Then
                        Return DialogResult.Cancel
                    End If

                    dataDumper_grpwire = OpenFileDialog2.FileName
                    If dataDumper_grpwire_f = 0 Then
                        dataDumper_grpwire_f = 1
                        DataGridView2.Item(2, row).Value = type(1)
                    End If
                End If
            Case 1 'tranwire.grp
                OpenFileDialog2.Filter = "GRP File|*.grp"
                dialog = OpenFileDialog2.ShowDialog

                If dialog = DialogResult.OK Then
                    If CheckGRPFile(OpenFileDialog2.FileName) = False Then
                        Return DialogResult.Cancel
                    End If

                    dataDumper_tranwire = OpenFileDialog2.FileName
                    If dataDumper_tranwire_f = 0 Then
                        dataDumper_tranwire_f = 1
                        DataGridView2.Item(2, row).Value = type(1)
                    End If
                End If
            Case 2 'wirefram.grp
                OpenFileDialog2.Filter = "GRP File|*.grp"
                dialog = OpenFileDialog2.ShowDialog

                If dialog = DialogResult.OK Then
                    If CheckGRPFile(OpenFileDialog2.FileName) = False Then
                        Return DialogResult.Cancel
                    End If

                    dataDumper_wirefram = OpenFileDialog2.FileName
                    If dataDumper_wirefram_f = 0 Then
                        dataDumper_wirefram_f = 1
                        DataGridView2.Item(2, row).Value = type(1)
                    End If
                End If
            Case 3 'cmdicons.grp
                OpenFileDialog2.Filter = "GRP File|*.grp"
                dialog = OpenFileDialog2.ShowDialog

                If dialog = DialogResult.OK Then
                    If CheckGRPFile(OpenFileDialog2.FileName) = False Then
                        Return DialogResult.Cancel
                    End If

                    dataDumper_cmdicons = OpenFileDialog2.FileName
                    If dataDumper_cmdicons_f = 0 Then
                        dataDumper_cmdicons_f = 1
                        DataGridView2.Item(2, row).Value = type(1)
                    End If
                End If
            Case 4 'stat_txt.tbl
                OpenFileDialog2.Filter = "tbl 텍스트파일|*.tbl"
                dialog = OpenFileDialog2.ShowDialog

                If dialog = DialogResult.OK Then
                    dataDumper_stat_txt = OpenFileDialog2.FileName

                    '데이터 메모리로 로딩.
                    LoadNewfile(dataDumper_stat_txt, DataName.stat_txt)

                    If dataDumper_stat_txt_f = 0 Then
                        dataDumper_stat_txt_f = 2
                        DataGridView2.Item(2, row).Value = type(2)
                    End If
                End If
            Case 5 'AIscript.bin
                OpenFileDialog2.Filter = "AIScript|*.bin"
                dialog = OpenFileDialog2.ShowDialog

                If dialog = DialogResult.OK Then
                    dataDumper_AIscript = OpenFileDialog2.FileName
                    If dataDumper_AIscript_f = 0 Then
                        dataDumper_AIscript_f = 1
                        DataGridView2.Item(2, row).Value = type(1)
                    End If
                End If
            Case 6 'iscript.bin
                OpenFileDialog2.Filter = "이미지스크립트|*.bin"
                dialog = OpenFileDialog2.ShowDialog

                If dialog = DialogResult.OK Then
                    dataDumper_iscript = OpenFileDialog2.FileName
                    If dataDumper_iscript_f = 0 Then
                        dataDumper_iscript_f = 1
                        DataGridView2.Item(2, row).Value = type(1)
                    End If
                End If
        End Select
        If dialog = DialogResult.OK Then
            DataGridView2.Item(1, row).Value = OpenFileDialog2.FileName.Split("\") _
              (OpenFileDialog2.FileName.Split("\").Count - 1)
        End If

        Return dialog
    End Function
    Private Function grpopen2(colum As Byte, row As Byte) As DialogResult
        Dim dialog As DialogResult

        OpenFileDialog2.Filter = "GRP File|*.grp"
        dialog = OpenFileDialog2.ShowDialog

        If dialog = DialogResult.OK Then
            Select Case row
                Case 0
                    grpinjector_arrow = OpenFileDialog2.FileName
                Case 1
                    grpinjector_drag = OpenFileDialog2.FileName
                Case 2
                    grpinjector_illegal = OpenFileDialog2.FileName
            End Select


            DataGridView1.Item(1, row).Value = OpenFileDialog2.FileName.Split("\") _
              (OpenFileDialog2.FileName.Split("\").Count - 1)
        End If
        Return dialog
    End Function

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseDown

        If e.ColumnIndex = 1 And e.RowIndex >= 0 Then
            grpopen(e.ColumnIndex, e.RowIndex)
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDown

        If e.ColumnIndex = 1 And e.RowIndex >= 0 Then
            grpopen2(e.ColumnIndex, e.RowIndex)
            'System.Threading.Thread.Sleep(500)
        ElseIf e.ColumnIndex = 2 And e.RowIndex >= 0 Then

            Select Case e.RowIndex
                Case 0
                    grpinjector_arrow = ""
                Case 1
                    grpinjector_drag = ""
                Case 2
                    grpinjector_illegal = ""
            End Select
            DataGridView1.Item(1, e.RowIndex).Value = ""
        End If
    End Sub


    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    PluginTriggerAddForm.TriggerString = ""


    '    Dim dialog As DialogResult = PluginTriggerAddForm.ShowDialog()

    '    If dialog = DialogResult.OK Then
    '        ListBox1.Items.Add(PluginTriggerAddForm.TriggerString)
    '    End If
    'End Sub

    'Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
    '    If ListBox1.SelectedIndex <> -1 Then
    '        PluginTriggerAddForm.TriggerString = ListBox1.Items(ListBox1.SelectedIndex)
    '        Dim dialog As DialogResult = PluginTriggerAddForm.ShowDialog()


    '        If dialog = DialogResult.OK Then
    '            ListBox1.Items(ListBox1.SelectedIndex) = PluginTriggerAddForm.TriggerString
    '        End If
    '    End If
    'End Sub

    'Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
    '    If ListBox1.SelectedIndex <> -1 Then
    '        Dim lastindex As UInteger = ListBox1.SelectedIndex

    '        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)

    '        If ListBox1.Items.Count <= lastindex Then
    '            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
    '        Else
    '            ListBox1.SelectedIndex = lastindex
    '        End If

    '        ListBox1.Height = 0
    '    End If
    'End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        dataDumperAddForm.datadumperstring = ""

        Dim dialog As DialogResult = dataDumperAddForm.ShowDialog()

        If dialog = DialogResult.OK Then
            ListBox2.Items.Add(dataDumperAddForm.datadumperstring)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If ListBox2.SelectedIndex <> -1 Then
            dataDumperAddForm.datadumperstring = ListBox2.Items(ListBox2.SelectedIndex)
            Dim dialog As DialogResult = dataDumperAddForm.ShowDialog()


            If dialog = DialogResult.OK Then
                ListBox2.Items(ListBox2.SelectedIndex) = dataDumperAddForm.datadumperstring
            End If
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If ListBox2.SelectedIndex <> -1 Then
            Dim lastindex As UInteger = ListBox2.SelectedIndex

            ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)

            If ListBox2.Items.Count <= lastindex Then
                ListBox2.SelectedIndex = ListBox2.Items.Count - 1
            Else
                ListBox2.SelectedIndex = lastindex
            End If

            ListBox2.Height = 0
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Shell(Environ("windir") & "\explorer.exe ""http://cafe.naver.com/edac/62525", AppWinStyle.MaximizedFocus)
    End Sub

    Private Sub EasyCompletionComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EasyCompletionComboBox1.SelectedIndexChanged
        nqcunit = EasyCompletionComboBox1.SelectedIndex
    End Sub

    '생성
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        NQCForm.returnstring = ""
        If NQCForm.ShowDialog() = DialogResult.OK Then
            nqccommands = nqccommands & "\" & NQCForm.returnstring


            Dim unitlist As New List(Of String)
            For i = 0 To CODE(0).Count - 1
                If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i) = 0 Then
                    unitlist.Add(CODE(0)(i))
                Else
                    Try
                        unitlist.Add(ProjectSet.CHKSTRING(-1 + ProjectSet.CHKUNITNAME(i)) & "(" & CODE(0)(i) & ")")
                    Catch ex As Exception
                        unitlist.Add(CODE(0)(i))
                    End Try

                End If
            Next


            Dim tstring() As String = NQCForm.returnstring.Split("#")

            ListBox3.Items.Add(tstring(0) & " : " & unitlist(tstring(1)) & ", " & tstring(2))






            ListBox3.Size = New Size(ListBox3.Size.Width, 4 + 15 * ListBox3.Items.Count)
        End If
    End Sub

    '수정
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If ListBox3.SelectedIndex <> -1 Then
            Dim currentstring As String = nqccommands.Split({"\"}, StringSplitOptions.RemoveEmptyEntries)(ListBox3.SelectedIndex)
            NQCForm.returnstring = currentstring
            If NQCForm.ShowDialog() = DialogResult.OK Then
                nqccommands = nqccommands.Replace(currentstring, NQCForm.returnstring)

                Dim unitlist As New List(Of String)
                For i = 0 To CODE(0).Count - 1
                    If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i) = 0 Then
                        unitlist.Add(CODE(0)(i))
                    Else
                        Try
                            unitlist.Add(ProjectSet.CHKSTRING(-1 + ProjectSet.CHKUNITNAME(i)) & "(" & CODE(0)(i) & ")")
                        Catch ex As Exception
                            unitlist.Add(CODE(0)(i))
                        End Try

                    End If
                Next


                Dim tstring() As String = NQCForm.returnstring.Split(",")



                ListBox3.Items(ListBox3.SelectedIndex) = tstring(0) & " : " & unitlist(tstring(1)) & ", " & tstring(2)






            End If
        End If
    End Sub

    '삭제
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If ListBox3.SelectedIndex <> -1 Then
            Dim currentstring As String = nqccommands.Split({"\"}, StringSplitOptions.RemoveEmptyEntries)(ListBox3.SelectedIndex)


            nqccommands = nqccommands.Replace("\" & currentstring, "")

            Dim lastselect As Integer = ListBox3.SelectedIndex
            ListBox3.Items.RemoveAt(ListBox3.SelectedIndex)
            Try
                ListBox3.SelectedIndex = lastselect
            Catch ex As Exception
                ListBox3.SelectedIndex = ListBox3.Items.Count - 1
            End Try



            ListBox3.Size = New Size(ListBox3.Size.Width, 4 + 15 * ListBox3.Items.Count)
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        LocationSetForm.rawstring = nqclocs
        If LocationSetForm.ShowDialog() = DialogResult.OK Then
            Dim lists() As String = LocationSetForm.lists.ToArray()
            nqclocs = lists(0).Trim

            For i = 1 To lists.Count - 1
                nqclocs = nqclocs & ", " & lists(i).Trim
            Next
        End If
    End Sub
End Class