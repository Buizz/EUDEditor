Imports System.IO
Imports System.Text

Module ImageToGRPModule
    Enum pixelmode
        Zeroloop = 0
        Numloop = 1
        Others = 2
    End Enum
    Public Function ImageToGRP(filename As String) As String


        Dim returnstrung As New StringBuilder

        Dim Filestream As New FileStream(filename, FileMode.Open)
        Dim FileReader As New BinaryReader(Filestream)


        Filestream.Position = 18
        Dim bmpfilesize As New Size
        bmpfilesize.Width = FileReader.ReadUInt32() '가로
        bmpfilesize.Height = FileReader.ReadUInt32()  '세로

        Dim realwidth As UInteger = bmpfilesize.Width

        If realwidth Mod 4 <> 0 Then
            realwidth = realwidth - realwidth Mod 4 + 4
        End If


        Dim grpmetadata(bmpfilesize.Height - 1) As List(Of List(Of Byte))


        For y = 0 To bmpfilesize.Height - 1
            grpmetadata(y) = New List(Of List(Of Byte))

            Filestream.Position = &H436 + (bmpfilesize.Height - 1 - y) * realwidth


            Dim codetype As pixelmode = -1

            Dim len As UInteger = 0
            grpmetadata(y).Add(New List(Of Byte))

            Dim oldcode As Byte
            Dim code As Byte


            code = FileReader.ReadByte()
            len += 1

            grpmetadata(y)(grpmetadata(y).Count - 1).Add(code)
            'returnstrung.Append("," & code)
            While True
                oldcode = code
                code = FileReader.ReadByte()
                len += 1


                'MsgBox("줄 길이 : " & len & "  OldCode : " & oldcode & "  Code : " & code)
                If oldcode = code Then
                    'grpmetadata(y).Add(New List(Of Byte))
                Else
                    grpmetadata(y).Add(New List(Of Byte))
                End If


                grpmetadata(y)(grpmetadata(y).Count - 1).Add(code)

                'returnstrung.Append("," & code)

                If len >= bmpfilesize.Width Then
                    Exit While
                End If

            End While
            '중복 여부 확인. 중복이 되면 1, 2번으로 전환
            '안되면 3번을 통해 계속 전진

            '태세가 있음. 

            '3번으로 전진 하는 법.
            '만약 남은 거리가 0x39보다 크다면 0x39하고 그대로 출력
            '0x39보다 적다면 거리 만큼 적고 그대로 출력.

            'Byte >= 0x80 : (byte - 0x80)만큼 0을 출력
            '최대 거리 128

            '0x80 > byte >= 0x40 : (byte - 0x40)만큼 다음 바이트를 반복해서 출력(0x43 0x5이면 0x5 0x5 0x5)
            '최대거리 64

            '0x40 > byte : 다음 byte만큼의 byte를 그대로 출력( Ex 0x3 0x4 0x3 0x5 이면  0x4 0x3 0x5 이렇게 됨.)
            '최대 거리 57
        Next


        Dim LineData As New List(Of List(Of Byte))
        For Line = 0 To grpmetadata.Count - 1 'x줄
            LineData.Add(New List(Of Byte))


            returnstrung.AppendLine("Line : " & Line)


            Dim isothers As Boolean = False

            Dim counter As UShort = 0

            Dim bitdata As New List(Of Byte)



            For Codes = 0 To grpmetadata(Line).Count - 1 '코드

                For Content = 0 To grpmetadata(Line)(Codes).Count - 1 '중복 코드들
                    bitdata.Add(grpmetadata(Line)(Codes)(Content))
                    'returnstrung.Append("," & grpmetadata(Line)(Codes)(Content))
                Next

                'returnstrung.AppendLine(grpmetadata(Line)(Codes).Count)

                If grpmetadata(Line)(Codes).Count = 1 Then
                    isothers = True
                    counter += 1
                Else
                    isothers = False
                    counter = grpmetadata(Line)(Codes).Count
                End If

                Dim isdataend As Boolean = False
                If isothers Then
                    If grpmetadata(Line).Count < Codes + 2 Then '마지막 일 경우.
                        isdataend = True
                    ElseIf grpmetadata(Line)(Codes + 1).Count > 1 Then
                        isdataend = True
                    End If
                Else
                    isdataend = True
                End If



                If isdataend Then
                    If bitdata.Count > 1 Then
                        If bitdata(0) = bitdata(1) Then
                            If bitdata(0) = 0 Then
                                'Byte >= 0x80 : (byte - 0x80 + 1)만큼 0을 출력
                                '최대 거리 128



                                Dim bitdataLen As UInt16 = bitdata.Count



                                While bitdataLen > 0
                                    If bitdataLen > 128 Then
                                        LineData(Line).Add(&HFF)
                                        returnstrung.Append("FF ")
                                        bitdataLen -= 128
                                    Else
                                        LineData(Line).Add(bitdataLen + 128)
                                        returnstrung.Append(Hex(bitdataLen + 128).PadLeft(2, "0") & " ")
                                        bitdataLen = 0
                                    End If
                                End While
                            Else
                                '0x80 > byte >= 0x40 : (byte - 0x40)만큼 다음 바이트를 반복해서 출력(0x43 0x5이면 0x5 0x5 0x5)
                                '최대거리 64

                                Dim bitdataLen As UInt16 = bitdata.Count



                                While bitdataLen > 0
                                    If bitdataLen > 64 Then
                                        LineData(Line).Add(&H7F)
                                        LineData(Line).Add(bitdata(0))
                                        returnstrung.Append("7F " & Hex(bitdata(0)).PadLeft(2, "0") & " ")
                                        bitdataLen -= 64
                                    Else
                                        LineData(Line).Add(bitdataLen + 64)
                                        LineData(Line).Add(bitdata(0))
                                        returnstrung.Append(Hex(bitdataLen + 64).PadLeft(2, "0") & " " & Hex(bitdata(0)).PadLeft(2, "0") & " ")
                                        bitdataLen = 0
                                    End If
                                End While
                            End If
                        Else
                            '0x40 > byte : 다음 byte만큼의 byte를 그대로 출력( Ex 0x3 0x4 0x3 0x5 이면  0x4 0x3 0x5 이렇게 됨.)
                            '최대 거리 57

                            Dim bitdataLen As UInt16 = bitdata.Count



                            While bitdataLen > 0
                                If bitdataLen > 63 Then
                                    LineData(Line).Add(&H3F)
                                    returnstrung.Append("3F ")
                                    For i = 1 To 63
                                        LineData(Line).Add(bitdata(0))
                                        returnstrung.Append(Hex(bitdata(0)).PadLeft(2, "0") & " ")
                                        bitdata.RemoveAt(0)
                                    Next
                                    bitdataLen -= 63
                                Else
                                    LineData(Line).Add(bitdataLen)
                                    returnstrung.Append(Hex(bitdataLen).PadLeft(2, "0") & " ")
                                    For i = 1 To bitdataLen
                                        LineData(Line).Add(bitdata(0))
                                        returnstrung.Append(Hex(bitdata(0)).PadLeft(2, "0") & " ")
                                        bitdata.RemoveAt(0)
                                    Next
                                    bitdataLen = 0
                                End If
                            End While

                        End If
                    Else
                        LineData(Line).Add(1)
                        LineData(Line).Add(bitdata(0))
                        returnstrung.Append("01 " & Hex(bitdata(0)).PadLeft(2, "0") & " ")
                    End If




                    'For k = 0 To bitdata.Count - 1
                    '    returnstrung.Append(Hex(bitdata(k)).PadLeft(2, "0") & " ")
                    'Next

                    'returnstrung.AppendLine("Content : " & counter)
                    counter = 0
                    bitdata.Clear()
                End If




                'grpmetadata(i)(j)의 길이를 판단. 1이면 중복되는 게 없고 2이상이면 중복이 됨.

                '1이라면 2개 나오기 전까지의 카운트를 기억해둠.
            Next

            returnstrung.AppendLine("====================================LineEND====================================")
        Next


        'LineData.Count = 전체 라인 수.
        'LineData.Count * 2 만큼이 라인 오프셋들.

        'LineData(k).Count = 데이터 수

        Dim lineOffset As UInt16 = LineData.Count * 2

        For k = 0 To LineData.Count - 1
            returnstrung.Append(Hex(lineOffset Mod 256).PadLeft(2, "0") & " " & Hex(lineOffset \ 256).PadLeft(2, "0") & " ")


            lineOffset += LineData(k).Count
        Next



        For k = 0 To LineData.Count - 1
            For i = 0 To LineData(k).Count - 1
                returnstrung.Append(Hex(LineData(k)(i)).PadLeft(2, "0") & " ")
            Next
        Next



        FileReader.Close()
        Filestream.Close()
        Return returnstrung.ToString
    End Function


    ' count = 0
    '                While (True)
    '                    opcode = binaryreader.ReadByte()

    '                    'MsgBox(count & " before " & TempGRP.frameWidth & " " & Format(opcode, "X"))
    '                    If opcode >= &H80 Then
    'For k As Integer = 1 To (opcode - &H80)
    '                            tempimage(tempimageindex) = 0
    '                            tempimageindex += 1
    '                            count += 1
    '                        Next
    'ElseIf &H80 > opcode And opcode >= &H40 Then
    '                        nextcode = binaryreader.ReadByte()
    '                        For k As Integer = 1 To (opcode - &H40)
    '                            tempimage(tempimageindex) = nextcode
    '                            tempimageindex += 1
    '                            count += 1
    '                        Next
    'ElseIf &H40 > opcode Then
    'For k As Integer = 1 To opcode
    '                            tempimage(tempimageindex) = binaryreader.ReadByte()
    '                            tempimageindex += 1
    '                            count += 1
    '                        Next
    'End If
    ''MsgBox(count & " after " & TempGRP.frameWidth)
    'If count = TempGRP.frameWidth Then

    'Exit While
    'End If
    'End While
End Module
