Imports System.IO

Namespace MPQlib
    Module MPQModule
        Public Function ReadListfile() As String()
            Dim list As New List(Of String)

            Dim hmpq As UInteger
            Dim hfile As UInteger
            Dim buffer(0) As Byte
            Dim filesize As UInteger
            Dim temptext As String = ""

            Dim pdwread As IntPtr

            StormLib.SFileOpenArchive(ProjectSet.InputMap, 0, 0, hmpq)


            Dim openFilename As String = "(listfile)"

            StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)

            If hfile <> 0 Then
                filesize = StormLib.SFileGetFileSize(hfile, filesize)
                ReDim buffer(filesize)

                StormLib.SFileReadFile(hfile, buffer, filesize, pdwread, 0)

                Dim mem As MemoryStream = New MemoryStream(buffer)
                Dim stream As StreamReader = New StreamReader(mem, System.Text.Encoding.Default)


                temptext = stream.ReadToEnd

                StormLib.SFileCloseFile(hfile)

                stream.Close()
                mem.Close()
            End If

            StormLib.SFileCloseArchive(hmpq)


            For i = 0 To temptext.Split(vbCrLf).Count - 1
                If temptext.Split(vbCrLf)(i).Trim <> "staredit\scenario.chk" Then
                    list.Add(temptext.Split(vbCrLf)(i).Trim)
                End If
            Next


            Return list.ToArray
        End Function

        Public Function ReadListfile(ByRef VListbox As ListBox) As Boolean
            VListbox.Items.Clear()

            Dim hmpq As UInteger
            Dim hfile As UInteger
            Dim buffer(0) As Byte
            Dim filesize As UInteger
            Dim temptext As String = ""

            Dim pdwread As IntPtr

            StormLib.SFileOpenArchive(ProjectSet.InputMap, 0, 0, hmpq)


            Dim openFilename As String = "(listfile)"

            StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)

            If hfile <> 0 Then
                filesize = StormLib.SFileGetFileSize(hfile, filesize)
                ReDim buffer(filesize)

                StormLib.SFileReadFile(hfile, buffer, filesize, pdwread, 0)

                Dim mem As MemoryStream = New MemoryStream(buffer)
                Dim stream As StreamReader = New StreamReader(mem, System.Text.Encoding.Default)


                temptext = stream.ReadToEnd

                StormLib.SFileCloseFile(hfile)

                stream.Close()
                mem.Close()
            Else
                Return False
            End If

            StormLib.SFileCloseArchive(hmpq)


            For i = 0 To temptext.Split(vbCrLf).Count - 1
                If temptext.Split(vbCrLf)(i).Trim <> "staredit\scenario.chk" Then
                    VListbox.Items.Add(temptext.Split(vbCrLf)(i).Trim)
                End If
            Next

            VListbox.Items.RemoveAt(VListbox.Items.Count - 1)
            Return True
        End Function


        Public Function ReadFile(openFilename As String, Optional MapName As String = "D") As Byte()
            Dim buffer(0) As Byte
            Dim hfile As UInteger
            Dim filesize As UInteger
            Dim pdwread As IntPtr

            Dim hmpq As UInteger

            If MapName = "D" Then
                MapName = ProjectSet.InputMap
            End If
            StormLib.SFileOpenArchive(MapName, 0, 0, hmpq)


            StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)

            If hfile <> 0 Then
                filesize = StormLib.SFileGetFileSize(hfile, filesize)
                ReDim buffer(filesize)

                StormLib.SFileReadFile(hfile, buffer, filesize, pdwread, 0)



                StormLib.SFileCloseFile(hfile)
            Else
                Return {0}
            End If


            StormLib.SFileCloseArchive(hmpq)
            Return buffer
        End Function


        Public Sub AddFile(Filename As String, ArchivedFilename As String, Optional MapName As String = "D")
            Dim hmpq As UInteger
            Filename = Filename.Trim

            If MapName = "D" Then
                MapName = ProjectSet.InputMap
            End If
            StormLib.SFileOpenArchive(MapName, 0, 0, hmpq)

            StormLib.SFileAddFile(hmpq, Filename, ArchivedFilename, StormLib.MPQ_FILE_REPLACEEXISTING)
            StormLib.SFileCloseArchive(hmpq)
        End Sub


        Public Sub AddFileSound(Filename As String, ArchivedFilename As String, Optional MapName As String = "D")
            Dim hmpq As UInteger
            Filename = Filename.Trim

            If MapName = "D" Then
                MapName = ProjectSet.InputMap
            End If
            StormLib.SFileOpenArchive(MapName, 0, 0, hmpq)

            StormLib.SFileAddWave(hmpq, Filename, ArchivedFilename, StormLib.MPQ_FILE_REPLACEEXISTING, StormLib.MPQ_WAVE_QUALITY_MEDIUM)
            StormLib.SFileCloseArchive(hmpq)
        End Sub

        Public Sub Rename(oldFilename As String, newFilename As String, Optional MapName As String = "D")
            Dim hmpq As UInteger
            oldFilename = oldFilename.Trim

            If MapName = "D" Then
                MapName = ProjectSet.InputMap
            End If
            StormLib.SFileOpenArchive(MapName, 0, 0, hmpq)
            StormLib.SFileRenameFile(hmpq, oldFilename, newFilename)
            StormLib.SFileCloseArchive(hmpq)
        End Sub
        Public Sub RemoveFile(Filename As String, Optional MapName As String = "D")
            Dim hmpq As UInteger
            Filename = Filename.Trim

            If MapName = "D" Then
                MapName = ProjectSet.InputMap
            End If
            StormLib.SFileOpenArchive(MapName, 0, 0, hmpq)
            StormLib.SFileRemoveFile(hmpq, Filename, 0)
            StormLib.SFileCloseArchive(hmpq)
        End Sub
    End Module

End Namespace
