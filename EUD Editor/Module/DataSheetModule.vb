Imports System.IO

Module DataSheetModule
    Private memory_stat_txt As MemoryStream

    '프로그램 시작시 메모리를 불러온다.
    'e2z면 집파일 안에서 불러온다
    'e2s면 참조 좌표로 부터 불러온다
    'ess도 참조 좌표로 부터 불러온다.
    Public Sub LoadFileToMemory()
        If ProjectSet.filename.EndsWith(".e2z") Then
            memory_stat_txt = Zip.GetFile(GetSafeName(dataDumper_stat_txt))
        Else
            LoadNewfile(dataDumper_stat_txt, DataName.stat_txt)
        End If
    End Sub


    Public Enum DataName
        stat_txt = 0

    End Enum


    '데이터들을 불러온다. 적절한 스트링을 적어주면 된다.
    Public Function LoadMemory(_data As DataName) As MemoryStream
        Select Case _data
            Case DataName.stat_txt
                memory_stat_txt.Position = 0
                Return memory_stat_txt
        End Select
        Return Nothing
    End Function

    Public Function CheckMemory(_data As DataName) As Boolean
        Select Case _data
            Case DataName.stat_txt
                If memory_stat_txt IsNot Nothing Then
                    If memory_stat_txt.CanRead Then
                        Return True
                    End If
                End If
        End Select
        Return False
    End Function
    Public Sub LoadNewfile(filename As String, _data As DataName)
        If CheckFileExist(filename) = False Then
            Select Case _data
                Case DataName.stat_txt
                    Dim filestream As New FileStream(filename, FileMode.Open)

                    Dim memorystream As New MemoryStream()

                    Dim buffer(filestream.Length) As Byte
                    filestream.Read(buffer, 1, filestream.Length)

                    memorystream.Write(buffer, 1, filestream.Length)


                    memory_stat_txt = memorystream

                    filestream.Close()
            End Select
        End If
    End Sub

    Public Sub EmptyMemory(_data As DataName)
        Select Case _data
            Case DataName.stat_txt
                memory_stat_txt.Close()
                memory_stat_txt = Nothing
        End Select
    End Sub

    Public Sub MakeFileFromMemory(_data As DataName, savefilename As String)
        Select Case _data
            Case DataName.stat_txt

                memory_stat_txt.Position = 0
                Dim buffer(memory_stat_txt.Length) As Byte
                memory_stat_txt.Read(buffer, 1, memory_stat_txt.Length)


                Dim file As FileStream = New FileStream(savefilename, FileMode.Create, FileAccess.Write)

                file.Write(buffer, 1, memory_stat_txt.Length)
                file.Close()
        End Select
    End Sub


    'dataDumper_user:
    'dataDumper_grpwire:
    'dataDumper_tranwire:
    'dataDumper_wirefram:
    'dataDumper_cmdicons:
    'dataDumper_stat_txt:
    'dataDumper_AIscript:
    'dataDumper_iscript:
    'grpinjector_arrow:
    'grpinjector_drag:
    'grpinjector_illegal:



    '파일을 불러올 때 마다 재 로드 한다.



    '프로그램이 끝날 경우 메모리를 필요에 따라 해제해야 할 수 있음.
End Module
