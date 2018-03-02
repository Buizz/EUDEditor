Imports System.IO
Imports System.IO.Compression

Namespace Zip
    Module ZipFileModule

        Public Function GetFile(filename As String) As MemoryStream
            If filename <> "" Then
                Using zipToOpen As FileStream = New FileStream(ProjectSet.filename, FileMode.Open)
                    Using archive As ZipArchive = New ZipArchive(zipToOpen, ZipArchiveMode.Update)
                        MsgBox(filename)
                        Dim readmeEntry As ZipArchiveEntry = archive.GetEntry(filename)
                        Dim _Stream As Stream = readmeEntry.Open()
                        Dim returnmemstr As New MemoryStream()

                        Dim buffer(_Stream.Length) As Byte
                        _Stream.Read(buffer, 1, _Stream.Length)

                        returnmemstr.Write(buffer, 1, _Stream.Length)

                        Return returnmemstr
                    End Using
                End Using
            Else
                Return Nothing
            End If
        End Function


        '파일 추가
        Public Sub AddFile(filename As String)

        End Sub

        '파일 교체
        Public Sub ChangeFile(filename As String, _data As DataName)


            'Using zipToOpen As FileStream = New FileStream(ProjectSet.filename, FileMode.Open)
            '    Using archive As ZipArchive = New ZipArchive(zipToOpen, ZipArchiveMode.Update)
            '        Dim readmeEntry As ZipArchiveEntry = archive.GetEntry("e2sfile")


            '        Using writer As StreamWriter = New StreamWriter(readmeEntry.Open())
            '            writer.Flush()
            '            writer.Write(_stringbdl.ToString)
            '        End Using
            '    End Using
            'End Using
        End Sub
        '파일 교체하는거 까지 함.
        'tbl먼저 테스트 해보고 맵 내장연습.




        'Public Function CheckZipFile(filename As String) As Boolean
        '    Using zipToOpen As FileStream = New FileStream(ProjectSet.filename, FileMode.Open)
        '        Using archive As ZipArchive = New ZipArchive(zipToOpen, ZipArchiveMode.Update)
        '            Dim readmeEntry As ZipArchiveEntry = archive.GetEntry(filename)
        '            Dim _Stream As Stream = readmeEntry.Open()
        '            Dim returnmemstr As New MemoryStream()

        '            Dim buffer(_Stream.Length) As Byte
        '            _Stream.Read(buffer, 1, _Stream.Length)

        '            returnmemstr.Write(buffer, 1, _Stream.Length)

        '            Return returnmemstr
        '        End Using
        '    End Using
        'End Function
    End Module
End Namespace

