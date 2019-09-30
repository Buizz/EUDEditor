Imports System.IO

Module CheckSumMoudle
    Public Sub StartCheckSum()
        Dim hmpq As UInteger
        Dim hfile As UInteger
        Dim buffer() As Byte
        Dim filesize As UInteger
        Dim pdwread As IntPtr



        Dim openFilename As String = "staredit\scenario.chk"



        StormLib.SFileOpenArchive(ProjectSet.OutputMap, 0, 0, hmpq)


        StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)
        filesize = StormLib.SFileGetFileSize(hfile, filesize)
        ReDim buffer(filesize)
        'MsgBox("파일 크기 : " & filesize)
        StormLib.SFileReadFile(hfile, buffer, filesize, pdwread, 0)


        Dim pos1 As UInteger = 0
        Dim pos2 As UInteger = 0


        Dim mem As New MemoryStream(buffer)
        Dim br As New BinaryReader(mem)
        Dim bw As New BinaryWriter(mem)

        Dim value As UInteger = 0


        While (mem.Length > mem.Position)
            value = br.ReadUInt32()
            If value = &HABCDEFEDUI Then

                pos1 = mem.Position - 4
            End If
            If value = &HCBABACDEUI Then
                pos2 = mem.Position - 4
            End If
            If pos1 <> 0 And pos2 <> 0 Then
                Exit While
            End If

            mem.Position -= 3
        End While
        'MsgBox("루프탈출")

        mem.Position = pos1
        'MsgBox(pos2 & "/" & mem.Length)
        bw.Write(pos2)


        mem.Position = pos2
        bw.Write(0)


        Dim crc32 As New CRC32
        Dim checksumv As UInteger = crc32.GetCRC32(mem.ToArray)
        'MsgBox("체크섬 값 : " & checksumv)


        mem.Position = pos2
        bw.Write(checksumv)



        Dim filestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\temp\scenario.chk", FileMode.Create)
        filestream.Write(mem.ToArray, 0, mem.ToArray.Length - 1)
        filestream.Close()

        br.Close()
        bw.Close()
        mem.Close()


        StormLib.SFileRemoveFile(hmpq, openFilename, 0)
        StormLib.SFileAddFile(hmpq, My.Application.Info.DirectoryPath & "\Data\temp\scenario.chk", openFilename, StormLib.MPQ_FILE_COMPRESS)

        StormLib.SFileCloseFile(hfile)
        StormLib.SFileCloseArchive(hmpq)





        'MsgBox("완료")
    End Sub
End Module
