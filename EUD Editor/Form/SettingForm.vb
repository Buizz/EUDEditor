Public Class SettingForm
    Dim FormStatus As Integer = 0

    Enum Satus
        newfile = 0
        programset = 1
        projectset = 2
    End Enum



    '맨 처음 시작 327, 176
    '208, 106
    Dim size1 As New Size(317, 106 + 59)  '프로그램 세팅   106 + 59
    Dim size2 As New Size(317, 372 + 59 - 75)  '프로젝트 세팅   208 + 59
    Dim size3 As New Size(317, 106 + 372 + 59 - 75)  '둘다            314 + 59
    Public Sub PreSizeSet()
        If ProjectSet.loading = False Then
            If ProjectSet.isload = False Then
                Button6.DialogResult = DialogResult.None
                'Me.Size = size1
            Else
                Button6.DialogResult = DialogResult.Cancel
                'Me.Size = size3
            End If
        Else
            Button6.DialogResult = DialogResult.Cancel
            'Me.Size = size2
        End If
    End Sub

    Private Function CheckMakeButton() As Boolean
        If ProjectSet.InputMap <> "" And ProjectSet.OutputMap <> "" Then
            Button6.Visible = False
            Button5.Size = New Size(310, 30)
            Return True
        Else
            Button6.Visible = True
            Button5.Size = New Size(154, 30)
            'Button6.Location = New Point(162, 213)
            Return False
        End If
    End Function

    Private Sub CheckButton5()
        If ProgramSet.StarDirec = "" Or ProgramSet.euddraftDirec = "" Then
            Button5.Enabled = False
            Button5.Size = New Size(154, 30)

            'Button5.Size = New Size(304, 23)
            ' Button6.Location = New Point(162, 110)

            Button6.Visible = True
        Else
            Button5.Enabled = True
            Button5.Size = New Size(310, 30)
            ' Button6.Location = New Point(162, 110)

            Button6.Visible = False
        End If
    End Sub

    Private Sub SettingForm_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        If ProgramSet.StarVersion = "Remastered" Then
            ' CheckCompatiblity()
        End If


        '0 DatEdit
        '1 FireGraft
        '2 BinEditor
        '3 TileSet
        '4 BtnSet(Unused)
        '5 GRP
        '6 TriggerEditor(Unused)
        '7 Plugin
        '8 FlieManager
    End Sub



    Private Sub SettingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Lan.GetLangage(Me)
        Lan.SetLangage(Me)
        ComboBox3.Items.Clear()
        Dim Folder As String = My.Application.Info.DirectoryPath & "\Data\Langage"
        For Each _file As String In IO.Directory.GetDirectories(Folder)
            ComboBox3.Items.Add(_file.Split("\").Last)
        Next
        For i = 0 To ComboBox3.Items.Count - 1
            If My.Settings.Langage = ComboBox3.Items(i) Then
                ComboBox3.SelectedIndex = i
            End If
        Next




        ComboBox1.SelectedItem = ProgramSet.StarVersion
        TextBox1.Text = ProgramSet.StarDirec.Split("\").Last
        TextBox2.Text = ProgramSet.euddraftDirec.Split("\").Last

        CheckBox2.Checked = ProjectSet.TriggerSetTouse
        ComboBox2.SelectedIndex = ProjectSet.TriggerPlayer
        CheckBox3.Checked = ProjectSet.EUDEditorDebug

        If ProjectSet.euddraftuse = True Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If

        CheckBox1.Checked = ProjectSet.LoadFromCHK
        For i = 5 To 8
            CheckedListBox1.SetItemChecked(i - 5, ProjectSet.UsedSetting(i))
        Next
        For i = 0 To 4
            CheckedListBox2.SetItemChecked(i, ProjectSet.UsedSetting(i))
        Next
        '0 DatEdit
        '1 FireGraft
        '2 BinEditor
        '3 TileSet
        '4 BtnSet(Unused)
        '5 GRP
        '6 TriggerEditor(Unused)
        '7 Plugin
        '8 FlieManager


        '0 DatEdit
        '1 FireGraft
        '2 TriggerEditor(Unused)
        '3 Plugin
        '4 FlieManager
        '5 GRP
        '6 BinEditor
        '7 TileSet
        '8 BtnSet(Unused)



        'CheckBox3.Checked = ProjectRequire

        If ProjectSet.loading = False Then
            CheckButton5()

            If ProjectSet.isload = False Then '프로그램 세팅만 함.
                FormStatus = Satus.programset
                CheckButton5()
                '304
                '  Button5.Location = New Point(3, 110)

                Button5.Visible = True



                GroupBox2.Visible = True
                ' GroupBox2.Location = New Point(3, 2)
                GroupBox2.Enabled = True

                '  GroupBox1.Location = New Point(3, 115)
                GroupBox1.Enabled = False
                GroupBox1.Visible = False
                'Me.Size = size1 ' (327, 139)
            Else '만들기 이후 부를때
                FormStatus = Satus.projectset
                TextBox3.Text = ProjectSet.InputMap.Split("\").Last
                TextBox4.Text = ProjectSet.OutputMap.Split("\").Last
                '   Button5.Location = New Point(3, 324)

                Button5.Visible = True
                Button5.Size = New Size(310, 30)
                Button6.Visible = False

                GroupBox2.Visible = True
                '   GroupBox2.Location = New Point(3, 2)
                GroupBox2.Enabled = True

                'GroupBox1.Location = New Point(3, 115)
                GroupBox1.Enabled = True
                GroupBox1.Visible = True
                'Me.Size = size3
            End If
        Else '게임 새로 만들기
            FormStatus = Satus.newfile
            If ProjectSet.InputMap = "" Then
                TextBox3.Text = ". . ."
            Else
                TextBox3.Text = ProjectSet.InputMap.Split("\").Last
            End If


            If ProjectSet.OutputMap = "" Then
                TextBox4.Text = ". . ."
            Else
                TextBox4.Text = ProjectSet.OutputMap.Split("\").Last
            End If


            'Me.ControlBox = False
            'Button5.Location = New Point(3, 213)
            Button5.Enabled = False 'CheckMakeButton()

            Button5.Visible = True

            GroupBox2.Visible = False
            'GroupBox1.Location = New Point(3, 2)
            GroupBox2.Enabled = False
            GroupBox1.Enabled = True
            GroupBox1.Visible = True
            'Me.Size = size2
            CheckMakeButton()
        End If
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub


    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        ProjectSet.euddraftuse = True
        CheckedListBox1.Visible = True
        ProjectSet.saveStatus = False
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        ProjectSet.euddraftuse = False
        CheckedListBox1.Visible = False
        ProjectSet.saveStatus = False
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        ProjectSet.UsedSetting(e.Index + 5) = e.NewValue
        ProjectSet.saveStatus = False
    End Sub

    Private Sub CheckedListBox2_SelectedIndexChanged(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox2.ItemCheck
        ProjectSet.UsedSetting(e.Index) = e.NewValue

        ProjectSet.saveStatus = False
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ProgramSet.StarVersion = ComboBox1.SelectedItem
        ProjectSet.saveStatus = False

        If ComboBox1.SelectedItem = "1.16.1" Then
            GroupBox4.Visible = True

            'CheckedListBox1.Enabled = True
        Else
            GroupBox4.Visible = False

            'CheckedListBox1.Enabled = False
        End If
    End Sub





    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dialog As DialogResult
        StarCraftOFD.InitialDirectory = ProgramSet.StarDirec.Replace("StarCraft.exe", "")

        dialog = StarCraftOFD.ShowDialog()

        If dialog = DialogResult.OK Then
            ProgramSet.StarDirec = StarCraftOFD.FileName
            TextBox1.Text = ProgramSet.StarDirec.Split("\").Last
        End If
        CheckButton5()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dialog As DialogResult
        euddraftOFD.InitialDirectory = ProgramSet.euddraftDirec.Replace("euddraft.exe", "")

        dialog = euddraftOFD.ShowDialog()

        If dialog = DialogResult.OK Then
            ProgramSet.euddraftDirec = euddraftOFD.FileName
            TextBox2.Text = ProgramSet.euddraftDirec.Split("\").Last
        End If
        CheckButton5()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SetInputMap()
    End Sub
    Public Function SetInputMap() As Boolean
        Dim returnval As Boolean = True
        Dim dialog As DialogResult
        OpenFileDialog1.FileName = ProjectSet.InputMap.Split("\").Last

        OpenFileDialog1.InitialDirectory = ProgramSet.StarDirec.Replace("StarCraft.exe", "") & "maps\"
retry:
        dialog = OpenFileDialog1.ShowDialog()

        If dialog = DialogResult.OK Then
            If ProjectSet.OutputMap = OpenFileDialog1.FileName Then
                MsgBox("맵이름은 같을 수 없습니다." & vbCrLf & "LoadMap :" & OpenFileDialog1.FileName.Split("\").Last & vbCrLf & "SaveMap :" & ProjectSet.OutputMap.Split("\").Last, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                GoTo retry
            Else
                ProjectSet.InputMap = OpenFileDialog1.FileName
                TextBox3.Text = ProjectSet.InputMap.Split("\").Last
            End If
        Else
            returnval = False
        End If

        ProjectSet.saveStatus = False
        Button5.Enabled = CheckMakeButton()

        Return returnval
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SetOutputMap()
    End Sub
    Public Function SetOutputMap() As Boolean
        Dim returnval As Boolean = True
        Dim dialog As DialogResult

        SaveFileDialog1.InitialDirectory = ProgramSet.StarDirec.Replace("StarCraft.exe", "") & "maps\"
        SaveFileDialog1.FileName = ProjectSet.OutputMap.Split("\").Last
retry:
        dialog = SaveFileDialog1.ShowDialog()

        If dialog = DialogResult.OK Then
            If ProjectSet.InputMap = SaveFileDialog1.FileName Then
                MsgBox("맵이름은 같을 수 없습니다." & vbCrLf & "LoadMap :" & ProjectSet.InputMap.Split("\").Last & vbCrLf & "SaveMap :" & SaveFileDialog1.FileName.Split("\").Last, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                GoTo retry
            Else
                ProjectSet.OutputMap = SaveFileDialog1.FileName
                TextBox4.Text = ProjectSet.OutputMap.Split("\").Last
            End If
        Else
            returnval = False
        End If

        ProjectSet.saveStatus = False
        Button5.Enabled = CheckMakeButton()

        Return returnval
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        ProjectSet.LoadFromCHK = CheckBox1.Checked
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Select Case FormStatus
            Case Satus.newfile
                ProjectSet.loading = False
                Close()
            Case Satus.programset
                Dim dialog As DialogResult
                dialog = MsgBox("EUD에디터를 종료하시겠습니까?", MsgBoxStyle.OkCancel, ProgramSet.AlterFormMessage)

                If dialog = DialogResult.OK Then
                    Me.Close()
                End If
            Case Satus.projectset
                Close()
        End Select
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        ProjectSet.TriggerSetTouse = CheckBox2.Checked
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        ProjectSet.TriggerPlayer = ComboBox2.SelectedIndex
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        ProjectSet.EUDEditorDebug = CheckBox3.Checked
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        My.Settings.Langage = ComboBox3.SelectedItem
        Lan.SetLangage(Me)
    End Sub






    'Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs)
    '    ProjectRequire = CheckBox3.Checked
    'End Sub
End Class