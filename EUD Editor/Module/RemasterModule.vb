Module RemasterModule
    Public Sub CheckCompatiblity()
        'DatEdit = 0
        'FireGraft = 1
        'BinEditor = 2
        'TileSet = 3
        'BtnSet = 4
        'GRP = 5
        'Struct = 6
        'Plugin = 7
        'filemanager = 8



        Dim BlackList() As Byte = {2, 3, 5}
        Dim Message As String = ""
        Dim count As Byte = 0

        For i = 0 To 8
            If BlackList.Contains(i) = True Then
                If ProjectSet.UsedSetting(i) = True Then
                    ProjectSet.UsedSetting(i) = False
                    count += 1

                End If
            End If
        Next

        If ProjectSet.EUDEditorDebug = True Then
            ProjectSet.EUDEditorDebug = False
            count += 1
        End If



        Message = "호환성으로 문제로 " & count & "개의 옵션이 사용안함 처리되었습니다."
        If count <> 0 Then
            MsgBox(Message, MsgBoxStyle.Information)
        End If
    End Sub
End Module
