Imports System.Net
Imports System.IO
Imports System.Threading


Module UpdateModule
    Dim status As Integer = 0
    Public lastver As String
    Dim lastverurl As String
    Dim currrentver As String

    Dim folder As String = My.Application.Info.DirectoryPath


    Public Function GetPatchNote() As String
        Dim returnstr As String
        Try
            Download("https://github.com/armoha/EUDEditor/raw/master/version/PatchNote", "\Data\temp\PatchNote")
            Dim versionfile As FileStream = File.Open(folder & "\Data\temp\PatchNote", FileMode.Open)
            Dim streamreader As New StreamReader(versionfile)

            returnstr = streamreader.ReadToEnd()

            streamreader.Close()
            versionfile.Close()
        Catch ex As Exception
            returnstr = "Error"
        End Try



        Return returnstr
    End Function

    Public Function CheckUpdateAble() As Boolean
        Try
            Download("https://github.com/armoha/EUDEditor/raw/master/version/version", "\Data\temp\Lastversion")

            Dim versionfile As FileStream = File.Open(folder & "\Data\temp\Lastversion", FileMode.Open)
            Dim streamreader As New StreamReader(versionfile)



            lastver = streamreader.ReadLine()
            lastverurl = streamreader.ReadLine()

            streamreader.Close()
            versionfile.Close()
        Catch ex As Exception
            lastver = ProgramSet.Version
        End Try


        If ProgramSet.Version = lastver Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Sub UpdateEUDEditor()
        ProjectSet.Close()
        If ProjectSet.isload = False Then
            My.Computer.FileSystem.CopyFile(My.Application.Info.DirectoryPath & "\Data\EUDEditorUpdate.exe", My.Application.Info.DirectoryPath & "\EUDEditorUpdate.exe", True)
            Process.Start(My.Application.Info.DirectoryPath & "\EUDEditorUpdate.exe")
            Main.Close()
        End If
    End Sub

    Dim Client As New WebClient
    Private Sub Download(filename As String, savedfilename As String)

        Dim StrDownUrl As String = filename
        Dim StrDownFolder As String = folder & savedfilename


        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        '파일다운로드
        '다른 다운로드 명령이 있으나 진행율을 표시하려면 DownloadFileAsync 을 사용해야 함
        Dim thread As New Thread(AddressOf Downloadtimer)

        Client.DownloadFileAsync(New Uri(StrDownUrl), StrDownFolder)
        thread.Start()
        While (thread.IsAlive = True)

        End While


        'downok 이라는 이름으로 이벤트 생성
        ' AddHandler Client.DownloadFileCompleted, AddressOf downok
    End Sub


    Private Sub Downloadtimer()
        Dim timer As DateTime = Now

        Dim span As TimeSpan
        While (Client.IsBusy) 'Client.IsBusy
            span = timer.Subtract(Now)
            If span.Seconds < -5 Then
                MsgBox(Lan.GetMsgText("failTodownload"), MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                If Client.IsBusy Then
                    Client.CancelAsync()
                End If
                Exit Sub
            End If
        End While
    End Sub

End Module
