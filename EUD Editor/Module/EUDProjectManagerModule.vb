Imports System.IO

Module EUDProjectManagerModule
    Public Enum Foldername
        Resource = 0
        Map = 1
        eudplibData = 2
        grp = 3
        sound = 4
        temp = 5
    End Enum
    Public Sub MoveFileAll(foldername As String)
        MoveFile(foldername, EUDProjectManagerModule.Foldername.Map, ProjectSet.InputMap)

        MoveFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_grpwire)
        MoveFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_tranwire)
        MoveFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_wirefram)
        MoveFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_cmdicons)
        MoveFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_stat_txt)
        MoveFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_AIscript)
        MoveFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_iscript)

        For i = 0 To GRPEditorDATA.Count - 1
            MoveFile(foldername, EUDProjectManagerModule.Foldername.grp, GRPEditorDATA(i).Filename)
        Next

        For i = 0 To Soundlist.Count - 1
            MoveBGM(foldername, EUDProjectManagerModule.Foldername.sound, Soundlist(i), i)
        Next
    End Sub

    Public Sub MoveBGM(targetname As String, foldername As Foldername, filename As String, listnum As Integer)
        Dim folderstr As String = GetFolderName(foldername)

        Dim oldname As String = filename
        Dim tempname As String = targetname & "\" & GetFolderName(Foldername.temp) & "\" & "temp.wav"
        Dim newname As String = targetname & "\" & folderstr & "\" & GetSafeName(filename).Split(".").First & ".ogg"
        If filename <> "" And filename <> newname Then
            Dim proc As New Process
            proc.StartInfo.FileName = IO.Path.Combine(Application.StartupPath, "ffmpeg.exe")
            With proc.StartInfo
                .Arguments = "-i " & Chr(34) & oldname & Chr(34) & " -y " & Chr(34) & tempname & Chr(34)
                .WindowStyle = ProcessWindowStyle.Hidden

            End With
            proc.Start()
            proc.WaitForExit()

            With proc.StartInfo
                .Arguments = "-i " & Chr(34) & tempname & Chr(34) & " -y " & Chr(34) & newname & Chr(34)
                .WindowStyle = ProcessWindowStyle.Hidden
            End With
            proc.Start()
            proc.WaitForExit()
            Soundlist(listnum) = newname
        End If
    End Sub
    Public Sub RenameFileAll()
        Dim foldername As String = ProjectSet.filename.Replace("\" & GetSafeName(ProjectSet.filename), "")

        RenameFile(foldername, EUDProjectManagerModule.Foldername.Map, ProjectSet.InputMap)

        RenameFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_grpwire)
        RenameFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_tranwire)
        RenameFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_wirefram)
        RenameFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_cmdicons)
        RenameFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_stat_txt)
        RenameFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_AIscript)
        RenameFile(foldername, EUDProjectManagerModule.Foldername.Resource, dataDumper_iscript)

        For i = 0 To GRPEditorDATA.Count - 1
            RenameFile(foldername, EUDProjectManagerModule.Foldername.grp, GRPEditorDATA(i).Filename)
        Next

        For i = 0 To Soundlist.Count - 1
            RenameFile(foldername, EUDProjectManagerModule.Foldername.sound, Soundlist(i))
        Next
    End Sub
    Public Sub DeleteDumpFileAll()
        Dim foldername As String = ProjectSet.filename.Replace(GetSafeName(ProjectSet.filename), "")

        For Each _file As String In Directory.GetFiles(foldername & GetFolderName(EUDProjectManagerModule.Foldername.Map))
            If _file <> ProjectSet.InputMap Then
                My.Computer.FileSystem.DeleteFile(_file)
            End If
        Next

        Dim filelist As New List(Of String)
        filelist.Add(dataDumper_grpwire)
        filelist.Add(dataDumper_tranwire)
        filelist.Add(dataDumper_wirefram)
        filelist.Add(dataDumper_cmdicons)
        filelist.Add(dataDumper_stat_txt)
        filelist.Add(dataDumper_AIscript)
        filelist.Add(dataDumper_iscript)


        For Each _file As String In Directory.GetFiles(foldername & GetFolderName(EUDProjectManagerModule.Foldername.Resource))
            If filelist.Contains(_file) = False Then
                My.Computer.FileSystem.DeleteFile(_file)
            End If
        Next
        filelist.Clear()

        For i = 0 To GRPEditorDATA.Count - 1
            filelist.Add(GRPEditorDATA(i).Filename)
        Next

        For Each _file As String In Directory.GetFiles(foldername & GetFolderName(EUDProjectManagerModule.Foldername.grp))
            If filelist.Contains(_file) = False Then
                My.Computer.FileSystem.DeleteFile(_file)
            End If
        Next

        filelist.Clear()

        For i = 0 To Soundlist.Count - 1
            filelist.Add(Soundlist(i))
        Next

        For Each _file As String In Directory.GetFiles(foldername & GetFolderName(EUDProjectManagerModule.Foldername.sound))
            If filelist.Contains(_file) = False Then
                My.Computer.FileSystem.DeleteFile(_file)
            End If
        Next
    End Sub


    Private Function GetFolderName(foldername As Foldername)
        Select Case foldername
            Case Foldername.Resource
                Return "Resource"
            Case Foldername.Map
                Return "Map"
            Case Foldername.eudplibData
                Return "eudplibdata"
            Case Foldername.grp
                Return "Grp"
            Case Foldername.sound
                Return "Sound"
            Case Foldername.temp
                Return "temp"
        End Select
        Return 0
    End Function

    Public Sub RenameFile(targetname As String, foldername As Foldername, ByRef filename As String)
        If filename <> "" Then
            Dim folderstr As String = GetFolderName(foldername)
            filename = targetname & "\" & folderstr & "\" & GetSafeName(filename)
        End If
    End Sub
    Public Sub MoveFile(targetname As String, foldername As Foldername, ByRef filename As String)
        If filename <> "" Then
            Dim folderstr As String = GetFolderName(foldername)
            Try
                My.Computer.FileSystem.CopyFile(filename, targetname & "\" & folderstr & "\" & GetSafeName(filename), True)
                filename = targetname & "\" & folderstr & "\" & GetSafeName(filename)
            Catch ex As Exception

            End Try

        End If
    End Sub
End Module
