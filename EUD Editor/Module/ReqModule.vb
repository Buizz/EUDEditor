Imports System.IO
Imports System.Text

Module ReqModule
    Public Sub RepDataToFile()
        Dim Ptr() As UInt16 = {1096, 840, 320, 688, 1316}

        Dim fileCreator As New FileStream(My.Application.Info.DirectoryPath & "\Data\temp\" & "RequireData", FileMode.Create)
        Dim filebinaryw As New BinaryWriter(fileCreator)


        For i = 0 To 4
            Dim StartOffset As UInteger = 0
            fileCreator.Position = 0
            For j = 0 To i - 1
                fileCreator.Position += Ptr(j)
            Next
            StartOffset = fileCreator.Position \ 2

            filebinaryw.Write(CUShort(0))


            For k = 0 To RequireData(i).Count - 1

                Select Case ProjectRequireDataUSE(i)(k)
                    Case 0 '기본값
                        If RequireData(i)(k).pos <> 0 Then


                            filebinaryw.Write(CUShort(k))

                            RequireData(i)(k).pos = fileCreator.Position \ 2 - StartOffset


                            For p = 0 To RequireData(i)(k).Code.Count - 1
                                filebinaryw.Write(RequireData(i)(k).Code(p))
                            Next
                            filebinaryw.Write(CUShort(&HFFFF))
                        End If
                    Case 1 '사용안함
                        ProjectRequireData(i)(k).pos = 0
                    Case 2 '무조건 허용
                        filebinaryw.Write(CUShort(k))
                        ProjectRequireData(i)(k).pos = fileCreator.Position \ 2 - StartOffset
                        filebinaryw.Write(CUShort(&HFFFF))
                    Case 3 '사용자정의
                        filebinaryw.Write(CUShort(k))
                        ProjectRequireData(i)(k).pos = fileCreator.Position \ 2 - StartOffset

                        For p = 0 To ProjectRequireData(i)(k).Code.Count - 1
                            filebinaryw.Write(ProjectRequireData(i)(k).Code(p))
                        Next
                        filebinaryw.Write(CUShort(&HFFFF))
                End Select


            Next



            filebinaryw.Write(CUShort(&HFFFF))
        Next




        filebinaryw.Close()
        fileCreator.Close()
    End Sub

    Public Function RepDataToTrigger() As String
        Dim pointers() As String = {"FG_PReqUnit", "FG_PReqUpg", "FG_PReqTechUpg", "FG_PReqTechUse", "FG_PReqOrder"}

        Dim returntext As New StringBuilder
        returntext.AppendLine("    DoActions([")
        For i = 0 To 4
            Dim pointer As String = ReadOffset(pointers(i))

            Dim value As UInteger = 0
            For k = 0 To RequireData(i).Count - 1

                If ProjectRequireDataUSE(i)(k) = 0 Then
                    If k Mod 2 = 0 Then
                        value = RequireData(i)(k).pos
                    Else
                        value += RequireData(i)(k).pos * 65536
                    End If

                Else
                    If k Mod 2 = 0 Then
                        value = ProjectRequireData(i)(k).pos
                    Else
                        value += ProjectRequireData(i)(k).pos * 65536
                    End If
                End If
                If k Mod 2 = 1 Then
                    returntext.AppendLine("       SetMemory(" & "0x" & pointer & " + " & (k * 2) - 2 & ", SetTo, " & value & "),")
                End If

            Next
        Next


        returntext.AppendLine("    ])")
        Return returntext.ToString()
    End Function
End Module
