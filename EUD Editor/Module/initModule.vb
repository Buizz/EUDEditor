Imports System.IO
Imports Microsoft.Win32

Module initModule
    Public DirectoryPath As String

    Sub DeleteFilesFromFolder(Folder As String)
        If Directory.Exists(Folder) Then
            For Each _file As String In Directory.GetFiles(Folder)
                File.Delete(_file)
            Next
            For Each _folder As String In Directory.GetDirectories(Folder)

                DeleteFilesFromFolder(_folder)
            Next
        End If
    End Sub

    Public Function init() As Boolean
        Dim filename As String
        filename = My.Application.Info.DirectoryPath & "\Data\Langage\한국어(Korean)"
        If System.IO.Directory.Exists(filename) Then
            DeleteFilesFromFolder(filename)
            System.IO.Directory.Delete(filename)
        End If
        filename = My.Application.Info.DirectoryPath & "\Data\Langage\English(English)"
        If System.IO.Directory.Exists(filename) Then
            DeleteFilesFromFolder(filename)
            System.IO.Directory.Delete(filename)
        End If
        filename = My.Application.Info.DirectoryPath & "\TE함수"
        If System.IO.Directory.Exists(filename) Then
            DeleteFilesFromFolder(filename)
            System.IO.Directory.Delete(filename)
        End If

        filename = My.Application.Info.DirectoryPath & "\Data\Langage\" & My.Settings.Langage
        If System.IO.Directory.Exists(filename) = False Then
            My.Settings.Langage = "Korean"
        End If

        Dim hyperLink As Boolean = False
        Dim testmode As Boolean = False
        Dim mainControlMode As Boolean = False
        'My.Settings.Reset()

        If mainControlMode Then
            Dim data As String
            Try
                With CreateObject("WinHttp.WinHttpRequest.5.1")
                    .Open("GET", "http://blog.naver.com/PostView.nhn?blogId=sksljh2091&logNo=220883526276")
                    .Send
                    .WaitForResponse

                    data = .ResponseText

                    Dim Searcher As String = ProgramSet.Version
                    Dim ttempstr As String = Mid(data, InStr(data, Searcher) + 1)
                    'Mid(data, InStr(data, "euddraftversion"))

                    Dim currentversion As String = Mid(ttempstr, Searcher.Length, InStr(ttempstr, "]") - Searcher.Length)


                    If currentversion = False Then
                        MsgBox("사용기간이 지났습니다. 강제로 종료됩니다.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                        End
                    End If
                End With
            Catch ex As Exception
                MsgBox("인터넷 연결이 끊켜 실행 가능 여부를 판단 할 수 없습니다. 강제 종료됩니다.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                End
            End Try
        End If



        'Test======================
        If testmode = True Then
            Try
                Dim lastMonth2 As String = "Jan"
                Dim lastMonth As String = "Feb"
                Dim lastday As Integer = 29
                Dim lastyear As Integer = 2017

                Dim data As String
                With CreateObject("WinHttp.WinHttpRequest.5.1")
                    .Open("GET", "http://map.naver.com/common2/checkNds.nhn")
                    .Send
                    data = Replace$(Replace$(Split(.ResponseText, vbNewLine)(6), vbTab, ""), ". ", "-")
                End With

                Dim monthandday As String = Mid(data, 1, InStr(data, ",") - 1)

                Dim Month As String = monthandday.Split(" ")(0)
                Dim day As String = monthandday.Split(" ")(1)
                Dim year As String = data.Split(" ")(2)
                data = year & "." & Month & "." & day
                MsgBox("현재 날짜는 " & data & "입니다." & vbCrLf & lastyear & "." & lastMonth & "." & lastday & "까지 사용 가능합니다.", MsgBoxStyle.Exclamation, ProgramSet.AlterFormMessage)

                If year > lastyear Or (Month <> lastMonth And Month <> lastMonth2) Or day > lastday Then
                    MsgBox("사용기간이 지났습니다. 강제로 종료됩니다.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                    End
                End If
            Catch ex As Exception
                MsgBox("인터넷 연결이 끊켜 날짜 확인이 불가능 합니다.. 강제로 종료됩니다.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                End
            End Try
        End If
        'Test======================

        If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
            MsgBox(Lan.GetMsgText("notoverStart"), MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, ProgramSet.ErrorFormMessage)
            End
        End If



        If ProgramSet.Version.Contains("TEST") Then
            MsgBox(Lan.GetText("Msgbox", "TESTAlter"), MsgBoxStyle.Exclamation, ProgramSet.AlterFormMessage)
        End If




        'System.IO.Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\Data\temp")



        ProgramSet.StarDirec = My.Settings.StarDirec
        ProgramSet.euddraftDirec = My.Settings.euddraftDirec
        ProgramSet.StarVersion = My.Settings.StarVersion
        ProgramSet.isAutoCompile = My.Settings.AutoCompile

        If My.Settings.mpqDirec.Split(",").Count = 4 Then
            ProgramSet.DatMPQDirec = My.Settings.mpqDirec.Split(",")
        End If


        If My.Settings.DatEditColor1 = Nothing Then
            ProgramSet.FORECOLOR = Color.White

            ProgramSet.BACKCOLOR = Color.FromArgb(&HFF193333)
            ProgramSet.CHANGECOLOR = Color.DarkSlateBlue

            ProgramSet.LISTCOLOR = Color.FromArgb(&HFF538585) ''FromArgb(&HFF4D9999)
        Else
            ProgramSet.FORECOLOR = My.Settings.DatEditColor1
            ProgramSet.BACKCOLOR = My.Settings.DatEditColor2
            ProgramSet.CHANGECOLOR = My.Settings.DatEditColor3
            ProgramSet.LISTCOLOR = My.Settings.DatEditColor4
        End If


        If ProgramSet.StarDirec = "" Or ProgramSet.euddraftDirec = "" Then
            SettingForm.StartPosition = FormStartPosition.CenterScreen
            SettingForm.TopMost = True
            SettingForm.PreSizeSet()
            SettingForm.ComboBox1.SelectedIndex = 1
            If SettingForm.ShowDialog() = DialogResult.Cancel Then
                Return False
            End If
            SettingForm.StartPosition = FormStartPosition.CenterParent
            SettingForm.TopMost = False
        End If


        Dim DatMPQ() As String = {"Patch_rt.mpq", "BrooDat.mpq", "BroodWar.mpq", "StarDat.mpq"}

        For i = 0 To 3
            If CheckFileExist(ProgramSet.DatMPQDirec(i)) = True Then
                ProgramSet.DatMPQDirec(i) = ProgramSet.StarDirec.Replace("StarCraft.exe", "") & DatMPQ(i)
            End If
        Next


        For i = 0 To DatMPQ.Count - 1
            If CheckFileExist(ProgramSet.DatMPQDirec(i)) = True Then
                MsgBox(Lan.GetMsgText("enMPQ"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                SetMPQForm.ShowDialog()
                Exit For
            End If
        Next


        If My.User.IsInRole("administrators") = False Then
            ' MsgBox(Lan.GetMsgText("notAdmin"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            'End
        End If



        '데이터 읽어오기
        DataLoad()




        DirectoryPath = My.Application.Info.DirectoryPath '실행파일 위치
        If My.Application.CommandLineArgs.Count <> 0 Then '연결로 실행되었을 경우.
            ProjectSet.Reset()
            My.Forms.SettingForm.StartPosition = FormStartPosition.CenterScreen
            ProjectSet.Load(My.Application.CommandLineArgs(0))
            'MsgBox(My.Application.CommandLineArgs(0))
            My.Forms.SettingForm.StartPosition = FormStartPosition.CenterParent
            CheckMapFile()
        Else
            If hyperLink Then
                ProjectSet.Reset()
                My.Forms.SettingForm.StartPosition = FormStartPosition.CenterScreen
                ProjectSet.Load("C:\Users\skslj\Desktop\EUD에디터Save1.e2s") 'C:\Users\skslj\Desktop\EUD에디터Save1.e2s") '"C:\Users\skslj\Desktop\EUD에디터Save1.e2s")
                My.Forms.SettingForm.StartPosition = FormStartPosition.CenterParent
                CheckMapFile()
            End If
        End If




        DataLoadAfterProgramLoad()

        'Test======================
        'If testmode = True Then

        ' End If
        'Test======================


        'OutPutEUDTriggers()

        '

        Dim classes As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Classes", RegistryKeyPermissionCheck.ReadWriteSubTree)




        classes.CreateSubKey(".e2s").SetValue("",
          "e2s", Microsoft.Win32.RegistryValueKind.String)
        classes.CreateSubKey("e2s\shell\open\command").SetValue("",
        Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
        classes.CreateSubKey("e2s\DefaultIcon").SetValue("",
       My.Application.Info.DirectoryPath & "\Data\icons\e2s.ico" & ",0", Microsoft.Win32.RegistryValueKind.String)


        classes.CreateSubKey(".ees").SetValue("",
          "ees", Microsoft.Win32.RegistryValueKind.String)
        classes.CreateSubKey("ees\shell\open\command").SetValue("",
        Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
        classes.CreateSubKey("ees\DefaultIcon").SetValue("",
        My.Application.Info.DirectoryPath & "\Data\icons\ees.ico" & ",0", Microsoft.Win32.RegistryValueKind.String)


        classes.CreateSubKey(".mem").SetValue("",
          "mem", Microsoft.Win32.RegistryValueKind.String)
        classes.CreateSubKey("mem\shell\open\command").SetValue("",
        Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
        classes.CreateSubKey("mem\DefaultIcon").SetValue("",
       My.Application.Info.DirectoryPath & "\Data\icons\mem.ico" & ",0", Microsoft.Win32.RegistryValueKind.String)


        classes.CreateSubKey(".e2p").SetValue("",
          "e2p", Microsoft.Win32.RegistryValueKind.String)
        classes.CreateSubKey("e2p\shell\open\command").SetValue("",
        Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
        classes.CreateSubKey("e2p\DefaultIcon").SetValue("",
       My.Application.Info.DirectoryPath & "\Data\icons\e2p.ico" & ",0", Microsoft.Win32.RegistryValueKind.String)

        'TextEditor.ShowDialog()
        'Main.Close()

        If My.Settings.IgnoreUpdate = False And CheckUpdateAble() = True Then
            CheckUpdateForm.ShowDialog()
        End If

        Return True
    End Function
End Module
