'맵에 삽입은 공통 사용.
'euddraft사용 체크하면 py넣고 아니면 맵에 직접 넣는 거지.

'Size1 423, 212
'Size2 489, 212
'Size3 423, 159


Imports System.IO

Public Class Main
    Public Sub refreshSet()
        If ProjectSet.isload = True Then
            If ProgramSet.StarVersion = "Remastered" Then
                CheckCompatiblity()
            End If
        End If


        buttonResetting()
        menuResetting()
        nameResetting()
    End Sub

    Public Sub buttonResetting()
        If ProgramSet.StarVersion = "1.16.1" Then
            Me.MinimumSize = New Size(423 + 66, 212)
            Me.Size = New Size(423 + 66, 212)
        Else
            Me.MinimumSize = New Size(423 + 66 + 65, 159)
            Me.Size = New Size(423 + 66 + 65, 159)
        End If


        If ProjectSet.isload = True Then '로드 되어 있을 경우
            FlowLayoutPanel2.Enabled = True
            Button2.Enabled = ProjectSet.UsedSetting(0)
            Button3.Enabled = ProjectSet.UsedSetting(1)


            If ProgramSet.StarVersion = "1.16.1" Then
                FlowLayoutPanel4.AutoSize = True

                FlowLayoutPanel2.Enabled = False

                MpainjectWToolStripMenuItem.Enabled = True
                EDDOpenDToolStripMenuItem.Enabled = True
                Button14.Enabled = True
                Button17.Enabled = True
                Button18.Enabled = True
                ToolStripMenuItem1.Enabled = True


                Button5.Enabled = ProjectSet.UsedSetting(2) 'binEdit
                Button6.Enabled = ProjectSet.UsedSetting(3) 'tileSet
                Button7.Enabled = ProjectSet.UsedSetting(4)
                Button8.Enabled = ProjectSet.UsedSetting(5) 'GRP
                Button9.Enabled = ProjectSet.UsedSetting(6)
                Button10.Enabled = ProjectSet.UsedSetting(7) 'Plugin
                Button15.Enabled = ProjectSet.UsedSetting(8) 'FileManger
                Button12.Enabled = True
                Button13.Enabled = True
                Button13.Visible = True

                Button11.Visible = True
                Button11.Enabled = False
                'Button11.Enabled = True
                Button12.Visible = True

                Button5.Visible = True
                Button6.Visible = True
                Button8.Visible = True
                'Button7.Enabled = ProjectSet.UsedSetting(4)

                Button13.Enabled = True
            Else
                FlowLayoutPanel4.AutoSize = True

                FlowLayoutPanel2.Enabled = False

                MpainjectWToolStripMenuItem.Enabled = True
                EDDOpenDToolStripMenuItem.Enabled = True
                Button14.Enabled = True
                Button17.Enabled = True
                Button18.Enabled = True
                ToolStripMenuItem1.Enabled = True

                Button5.Enabled = False 'binEdit
                Button5.Visible = False

                Button6.Enabled = False 'tileSet
                Button6.Visible = False

                Button7.Enabled = ProjectSet.UsedSetting(4)
                Button8.Enabled = False 'GRP
                Button8.Visible = False

                Button9.Enabled = ProjectSet.UsedSetting(6)
                Button10.Enabled = ProjectSet.UsedSetting(7) 'Plugin
                Button15.Enabled = ProjectSet.UsedSetting(8) 'FileManger
                Button12.Enabled = False
                Button13.Enabled = False
                Button13.Visible = False

                Button11.Visible = True
                Button11.Enabled = False
                'Button11.Enabled = True
                Button12.Visible = False

                'Button7.Enabled = ProjectSet.UsedSetting(4)

                Button13.Enabled = True
            End If

        Else '로드 안되어 있을 경우
            Button2.Enabled = False
            Button3.Enabled = False


            If ProgramSet.StarVersion = "1.16.1" Then
                FlowLayoutPanel4.AutoSize = True

                FlowLayoutPanel2.Enabled = False

                MpainjectWToolStripMenuItem.Enabled = False
                EDDOpenDToolStripMenuItem.Enabled = False
                Button14.Enabled = False
                Button17.Enabled = False
                Button18.Enabled = False
                ToolStripMenuItem1.Enabled = False


                Button5.Enabled = False
                Button6.Enabled = False
                Button7.Enabled = False
                Button8.Enabled = False
                Button9.Enabled = False
                Button10.Enabled = False
                Button15.Enabled = False
                Button12.Enabled = False
                Button13.Enabled = False
                Button13.Visible = True

                Button11.Visible = True
                Button11.Enabled = False
                Button12.Visible = True

                Button5.Visible = True
                Button6.Visible = True
                Button8.Visible = True
                'Button7.Enabled = ProjectSet.UsedSetting(4)

                Button13.Enabled = False
            Else
                FlowLayoutPanel4.AutoSize = True

                FlowLayoutPanel2.Enabled = False

                MpainjectWToolStripMenuItem.Enabled = False
                EDDOpenDToolStripMenuItem.Enabled = False
                Button14.Enabled = False
                Button17.Enabled = False
                Button18.Enabled = False
                ToolStripMenuItem1.Enabled = False

                Button5.Enabled = False 'binEdit
                Button5.Visible = False

                Button6.Enabled = False 'tileSet
                Button6.Visible = False

                Button7.Enabled = False
                Button8.Enabled = False 'GRP
                Button8.Visible = False

                Button9.Enabled = False
                Button10.Enabled = False
                Button15.Enabled = False
                Button12.Enabled = False
                Button13.Visible = False

                Button11.Visible = True
                Button11.Enabled = False
                Button12.Visible = False

                'Button7.Enabled = ProjectSet.UsedSetting(4)

                Button13.Enabled = False
            End If
        End If
    End Sub

    Public Sub menuResetting()
        SaveToolStripMenuItem.Enabled = ProjectSet.isload
        SaveasToolStripMenuItem.Enabled = ProjectSet.isload
        ProCloseToolStripMenuItem.Enabled = ProjectSet.isload

        PyViewVToolStripMenuItem.Enabled = ProjectSet.isload
        EdsViewEToolStripMenuItem.Enabled = ProjectSet.isload


        btn_Save.Enabled = ProjectSet.isload
        btn_close.Enabled = ProjectSet.isload
    End Sub




    Public Sub nameResetting()
        Dim issaved As String

        If ProjectSet.saveStatus Then
            issaved = "  "
        Else
            issaved = " *"
        End If

        If ProjectSet.isload = True Then
            If ProjectSet.filename = "" Then
                Me.Text = "제목 없음 " & issaved & " -  EUD Editor 2 " & ProgramSet.Version & "." & ProgramSet.StarVersion
                DatEditForm.Text = ProgramSet.DatEditName & issaved & " " & ProgramSet.Version

                FireGraftForm.Text = ProgramSet.FireGraftName & issaved & " " & ProgramSet.Version
            Else
                Dim name As String = ProjectSet.filename.Split("\").Last

                Me.Text = name & issaved & " -  EUD Editor 2 " & ProgramSet.Version & "." & ProgramSet.StarVersion
                DatEditForm.Text = name & issaved & " - " & ProgramSet.DatEditName & " " & ProgramSet.Version

                FireGraftForm.Text = name & issaved & " - " & ProgramSet.FireGraftName & " " & ProgramSet.Version
            End If
        Else
            Me.Text = "EUD Editor 2 " & ProgramSet.Version & "." & ProgramSet.StarVersion
        End If

    End Sub



    Private Sub Main_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        If ProgramSet.StarVersion = "1.16.1" Then
            If Size.Height > (313 - 52) Then '313
                Button12.Size = New Size(65, 50)
                TableLayoutPanel2.ColumnStyles.Item(0).Width = 67
            Else
                Button12.Size = New Size(65, 102)
                TableLayoutPanel2.ColumnStyles.Item(0).Width = 135
            End If
        Else
            If Size.Height > (313 - 52 - 52) Then '313
                Button12.Size = New Size(65, 50)
                TableLayoutPanel2.ColumnStyles.Item(0).Width = 67
            Else
                Button12.Size = New Size(65, 102)
                TableLayoutPanel2.ColumnStyles.Item(0).Width = 135
            End If
        End If
    End Sub

    Dim ShutDown As Boolean = False
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetMenu(Me, MenuStrip1)
        Lan.SetLangage(Me)

        SaveFileDialog1.Filter = Lan.GetText(Me.Name, "SaveFilter")
        OpenFileDialog1.Filter = Lan.GetText(Me.Name, "OpenFilter")


        'My.Settings.Reset()
        If init() = False Then
            ShutDown = True
            Me.Close()
        End If



        refreshSet()
    End Sub



    Private Sub Main_Closed(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        If ShutDown = False Then
            'Dim filename As String = My.Application.Info.DirectoryPath & "\Data\temp"
            'System.IO.Directory.Delete(path, False)

            'Try
            '    Dim path As String = My.Application.Info.DirectoryPath & "\Data\temp"
            '    Dim di As New IO.DirectoryInfo(path)
            '    di.Delete(True)
            'Catch ex As Exception

            'End Try



            If ProjectSet.Close() Then
                'StarCraftVisibleForm.Close()

                My.Settings.StarDirec = ProgramSet.StarDirec
                My.Settings.euddraftDirec = ProgramSet.euddraftDirec
                My.Settings.StarVersion = ProgramSet.StarVersion

                My.Settings.DatEditColor1 = ProgramSet.FORECOLOR
                My.Settings.DatEditColor2 = ProgramSet.BACKCOLOR
                My.Settings.DatEditColor3 = ProgramSet.CHANGECOLOR
                My.Settings.DatEditColor4 = ProgramSet.LISTCOLOR

                My.Settings.mpqDirec = String.Join(",", ProgramSet.DatMPQDirec)

                My.Settings.Save()
            Else
                e.Cancel = True
            End If
        End If
    End Sub


    Private Sub Setting_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SettingForm.PreSizeSet()
        SettingForm.ShowDialog()


        ProjectSet.LoadCHKdata()
        DatEditForm.ReloadCHK()
        refreshSet()
        'My.Forms.SettingForm.Location = Me.Location + Button1.Location + New Point(0, 105)
    End Sub

    Private Sub 새로만들기NToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewNToolStripMenuItem.Click
        새로만들기()
    End Sub
    Private Sub 새로만들기()
        If ProjectSet.Close() = True Then
            ProjectSet.loading = True

            ProjectSet.Reset()
            SettingForm.PreSizeSet()
            SettingForm.ShowDialog()


            If ProjectSet.loading = True Then
                ProjectSet.isload = True
                ProjectSet.saveStatus = True
            Else '취소 할 경우
                ProjectSet.Close()
            End If

            ProjectSet.LoadCHKdata()
            LoadFileimportable()
            ProjectSet.loading = False
            refreshSet()
        End If
    End Sub

    Private Sub 프로젝트닫기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProCloseToolStripMenuItem.Click
        프로젝트닫기()
    End Sub
    Private Sub 프로젝트닫기()
        ProjectSet.Close()

        refreshSet()
    End Sub

    Private Sub 다른이름으로저장ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveasToolStripMenuItem.Click
        다른이름으로저장()
    End Sub
    Private Sub 다른이름으로저장()
        Dim Dialog As DialogResult

        If ProjectSet.filename = "" Then
            SaveFileDialog1.FileName = "제목 없음"
        Else
            Dim name As String = ProjectSet.filename.Split("\").Last

            SaveFileDialog1.FileName = Mid(name, 1, name.Length - 4)
            SaveFileDialog1.InitialDirectory = ProjectSet.filename.Replace(ProjectSet.filename.Split("\").Last, "")
        End If


        Dialog = SaveFileDialog1.ShowDialog()

        If Dialog = DialogResult.Cancel Then
        Else
            ProjectSet.Save(SaveFileDialog1.FileName)
        End If

        refreshSet()
    End Sub

    Private Sub 열기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        열기()
    End Sub
    Private Sub 열기()
        Dim Dialog As DialogResult



        Dialog = OpenFileDialog1.ShowDialog()
        If Dialog = DialogResult.Cancel Then
        Else
            If ProjectSet.Close() = True Then
                ProjectSet.Load(OpenFileDialog1.FileName)
                CheckMapFile()
            End If
        End If



        refreshSet()

    End Sub

    Private Sub 저장ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        저장()
    End Sub
    Public Sub 저장()
        'Dim ise2s As Boolean = False
        'Try
        '    If Mid(ProjectSet.filename, ProjectSet.filename.Length - 3) <> ".e2s" Then
        '        ise2s = True
        '    End If
        'Catch ex As Exception

        'End Try


        If ProjectSet.filename = "" Then 'Or ise2s Then
            Dim Dialog As DialogResult


            If ProjectSet.filename = "" Then
                SaveFileDialog1.FileName = "제목 없음"
            Else
                SaveFileDialog1.FileName = Mid(ProjectSet.filename, 1, ProjectSet.filename.Length - 4)
            End If


            Dialog = SaveFileDialog1.ShowDialog()
            If Dialog = DialogResult.Cancel Then
            Else
                ProjectSet.Save(SaveFileDialog1.FileName)
            End If
        Else
            ProjectSet.Save(ProjectSet.filename)
        End If

        refreshSet()
    End Sub
    Private Sub 끝내기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        If ProjectSet.Close() Then
            Me.Close()
        End If
    End Sub

    Private Sub MPQFormOpen(sender As Object, e As EventArgs) Handles Button13.Click
        'MPQForm.Location = Me.Location
        My.Forms.Main.Visible = False
        MPQForm.ShowDialog()
        My.Forms.Main.Visible = True

    End Sub

    Private Sub GRPFormOpen(sender As Object, e As EventArgs) Handles Button8.Click
        'GRPForm.Location = Me.Location
        'Size = New Size(221, 438)
        GRPForm.Size = New Size(806, 438)
        My.Forms.Main.Visible = False
        GRPForm.ShowDialog()
        My.Forms.Main.Visible = True
        nameResetting()
    End Sub

    Private Sub DatEditFormOpen(sender As Object, e As EventArgs) Handles Button2.Click
        'DatEditForm.Location = Me.Location
        DatEditForm.Timer1.Enabled = True

        My.Forms.Main.Visible = False

        DatEditForm.Show()
        DatEditForm.RefreshForm()
        My.Forms.DatEditForm.Activate()
    End Sub

    Private Sub FireGraftFormOpen(sender As Object, e As EventArgs) Handles Button3.Click
        My.Forms.Main.Visible = False
        FireGraftForm.Show()
        FireGraftForm.RefreshForm()
        My.Forms.DatEditForm.Activate()
    End Sub

    Private Sub TriggerViewFormOpen(sender As Object, e As EventArgs) Handles Button4.Click
        My.Forms.Main.Visible = False
        TriggerViewerForm.ShowDialog()
        My.Forms.Main.Visible = True
        nameResetting()
    End Sub

    Private Sub plugin_Click(sender As Object, e As EventArgs) Handles Button10.Click
        ProjectSet.saveStatus = False

        My.Forms.Main.Visible = False
        PluginForm.ShowDialog()
        My.Forms.Main.Visible = True
        nameResetting()
        LoadFileimportable()
        ProjectSet.LoadCHKdata()
    End Sub

    Private Sub FileManager_Click(sender As Object, e As EventArgs) Handles Button15.Click
        ProjectSet.saveStatus = False

        My.Forms.Main.Visible = False
        FileManagerForm.ShowDialog()
        My.Forms.Main.Visible = True
        nameResetting()
        LoadFileimportable()
        DatEditForm.Loadstattxt()
    End Sub


    Private Sub binEditor_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ProjectSet.saveStatus = False

        My.Forms.Main.Visible = False
        binEditorForm.ShowDialog()
        My.Forms.Main.Visible = True
        nameResetting()
    End Sub


    Private Sub TileSet_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ProjectSet.saveStatus = False

        LoadTILEDATA(False, True)
        My.Forms.Main.Visible = False
        TileSetForm.ShowDialog()
        My.Forms.Main.Visible = True
        nameResetting()
    End Sub


    Private Sub DebugFormOpen(sender As Object, e As EventArgs) Handles Button12.Click

        My.Forms.Main.Visible = False
        DebugForm.ShowDialog()
        My.Forms.Main.Visible = True
    End Sub



    Private Sub btn_close_Click(sender As Object, e As EventArgs) Handles btn_close.Click
        프로젝트닫기()
    End Sub
    Private Sub Btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        저장()
    End Sub
    Private Sub Btn_OpenFile_Click(sender As Object, e As EventArgs) Handles btn_OpenFile.Click
        열기()
    End Sub
    Private Sub Btn_NewFile_Click(sender As Object, e As EventArgs) Handles btn_NewFile.Click
        새로만들기()
    End Sub


    Private Sub EDD켜기DToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EDDOpenDToolStripMenuItem.Click
        LoadTILEDATA(False, True)
        eudplib.Toflie(True)
    End Sub

    Private Sub EDD켜기_Click(sender As Object, e As EventArgs) Handles Button17.Click
        LoadTILEDATA(False, True)
        eudplib.Toflie(True)
    End Sub
    Private Sub 맵에삽입_Click(sender As Object, e As EventArgs) Handles Button14.Click
        LoadTILEDATA(False, True)
        eudplib.Toflie()
    End Sub

    Private Sub 맵에삽입WToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MpainjectWToolStripMenuItem.Click
        LoadTILEDATA(False, True)
        eudplib.Toflie()
    End Sub
    Private Sub TriggerCopy_Click(sender As Object, e As EventArgs) Handles Button16.Click
        My.Computer.Clipboard.SetText(TriggerViewerForm.RedrawText())
        MsgBox("복사가 완료되었습니다.", MsgBoxStyle.OkOnly, "EUD Editor")
    End Sub


    Private Sub 트리거보기TToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TriggerviewTToolStripMenuItem.Click
        TriggerViewerForm.ShowDialog()
        nameResetting()
    End Sub

    Private Sub 클립보드로EToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToclipEToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(TriggerViewerForm.RedrawText())
        MsgBox("복사가 완료되었습니다.", MsgBoxStyle.OkOnly, "EUD Editor")
    End Sub

    Private Sub Py파일보기VToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PyViewVToolStripMenuItem.Click
        Previewer.ispyfile = True
        Previewer.FCTB.Text = eudplib.GetPYtext
        Previewer.ShowDialog()
    End Sub

    Private Sub Eds파일보기EToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EdsViewEToolStripMenuItem.Click
        Previewer.ispyfile = True
        Previewer.FCTB.Text = eudplib.Getedstext

        Previewer.ShowDialog()
    End Sub

    Private Sub 도구TToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolTToolStripMenuItem.DropDownOpened
        If ProjectSet.euddraftuse = True And ProjectSet.isload = True Then
            TriggerviewTToolStripMenuItem.Enabled = False
            MpainjectWToolStripMenuItem.Enabled = True
            EDDOpenDToolStripMenuItem.Enabled = True
            ToclipEToolStripMenuItem.Enabled = False
        Else
            TriggerviewTToolStripMenuItem.Enabled = False 'True
            MpainjectWToolStripMenuItem.Enabled = False
            EDDOpenDToolStripMenuItem.Enabled = False
            ToclipEToolStripMenuItem.Enabled = False 'True
        End If
    End Sub

    Private Sub 블로그설명서ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BlogHelpToolStripMenuItem.Click
        HelpForm.Show()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ProjectSet.saveStatus = False

        If ProjectSet.LoadFromCHK = False Then
            MsgBox(Lan.GetText(Me.Name, "CHKMsg"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
        Else
            My.Forms.Main.Visible = False
            TrigEditorForm.ShowDialog()
            My.Forms.Main.Visible = True
            nameResetting()
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Try
            Dim process As New Process
            Dim startInfo As New ProcessStartInfo


            startInfo.FileName = ProjectSet.InputMap
            process.StartInfo = startInfo
            process.Start()
        Catch ex As Exception
            MsgBox(Lan.GetText(Me.Name, "MapisNotExit"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
        End Try

    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Try
            Dim process As New Process
            Dim startInfo As New ProcessStartInfo


            startInfo.FileName = ProjectSet.InputMap
            process.StartInfo = startInfo
            process.Start()
        Catch ex As Exception
            MsgBox(Lan.GetText(Me.Name, "MapisNotExit"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
        End Try
    End Sub

    Private LastData As Date
    Private Sub CheckMapWrite_Tick(sender As Object, e As EventArgs) Handles CheckMapWrite.Tick
        If ProjectSet.isload = True Then
            If CheckFileExist(ProjectSet.InputMap) = False Then
                Dim fileinfo As New FileInfo(ProjectSet.InputMap)
                If LastData < fileinfo.LastWriteTime Then
                    LastData = fileinfo.LastWriteTime

                    ProjectSet.LoadCHKdata()
                End If
            End If
        End If


    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ProjectSet.saveStatus = False

        My.Forms.Main.Visible = False
        SoundPlayerForm.ShowDialog()
        My.Forms.Main.Visible = True
        nameResetting()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        ProjectSet.saveStatus = False

        My.Forms.Main.Visible = False
        FileSettingForm.ShowDialog()
        My.Forms.Main.Visible = True
        nameResetting()
    End Sub
End Class
