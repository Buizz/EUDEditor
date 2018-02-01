Imports System.Text
Imports System.IO


Module BinEditModule
    Private Function GetMaskingData(race As Byte, SceneNum As UInteger) As String
        Dim racetype() As Integer = {0, 6, 13}

        Dim returntext As New StringBuilder()



        Dim bmp As New Bitmap(641, 480, Imaging.PixelFormat.Format32bppArgb) 'My.Application.Info.DirectoryPath & "\Data\bin\" & "pconsole.bmp")

        Dim grp As Graphics = Graphics.FromImage(bmp)

        'grp.DrawRectangle(Pens.Red, mousepos.X, mousepos.Y, 100, 100)

        Dim dialogcount As Byte
        Select Case race
            Case 0
                dialogcount = 11
            Case 1
                dialogcount = 12
            Case 2
                dialogcount = 13
        End Select
        For i = 0 To dialogcount - 1
            Dim dialogNum As Integer = i


            If dialogNum > 4 Then
                Select Case race
                    Case 0
                        dialogNum += racetype(race)
                    Case 1
                        dialogNum += racetype(race)
                    Case 2
                        dialogNum += racetype(race)
                End Select
            End If
            Dim tpos As Point = PjcutData(SceneNum).binData(dialogNum).pos
            Dim tsize As Size = PjcutData(SceneNum).binData(dialogNum).size

            If CheckFileExist(PjcutData(SceneNum).binData(dialogNum).imagename) Then
                Select Case dialogNum
                    Case 0
                    Case 1 To 4
                        Select Case race
                            Case 0
                                grp.DrawImage(DefaultBinBitmap(dialogNum).pbmp, tpos.X, tpos.Y)
                            Case 1
                                grp.DrawImage(DefaultBinBitmap(dialogNum).tbmp, tpos.X, tpos.Y)
                            Case 2
                                grp.DrawImage(DefaultBinBitmap(dialogNum).zbmp, tpos.X, tpos.Y)
                        End Select
                    Case Else
                        grp.DrawImage(DefaultBinBitmap(dialogNum).bmp, tpos.X, tpos.Y)

                End Select
            Else
                'tsize = New Bitmap(projectsceneData(selectedsceneNum).binData(dialogNum).imagename).Size
                Dim mybitmap As New Bitmap(PjcutData(SceneNum).binData(dialogNum).imagename)
                mybitmap.MakeTransparent(Color.Black)
                grp.DrawImage(mybitmap, tpos.X, tpos.Y)


                returntext.AppendLine(DrawImage(binfileptr(dialogNum), PjcutData(SceneNum).binData(dialogNum).imagename))
            End If


        Next
        bmp.MakeTransparent(Color.Black)




        Dim Sdata As New List(Of Byte)

        Dim x, count As Integer
        For y = 0 To 479
            x = 0
            While True
                count = 0
                While (bmp.GetPixel(x, y).ToArgb = 0)
                    If x = 640 Then
                        Exit While
                    End If
                    If count = 255 Then
                        Exit While
                    End If
                    count = count + 1
                    x = x + 1
                End While
                Sdata.Add(count)
                count = 0
                While (bmp.GetPixel(x, y).ToArgb <> 0)
                    If x = 640 Then
                        Exit While
                    End If

                    If count = 255 Then
                        Exit While
                    End If
                    count = count + 1
                    x = x + 1
                End While

                Sdata.Add(count)
                If x = 640 Then
                    Exit While
                End If
            End While
            Sdata.Add(0)
            Sdata.Add(0)
            '(00 00)가 한 단위. 만약 한 줄 쓰면 (00 00)을 쓴다.


            'RGBA = 0인 동안 계속 진행한다.
            '만약 255가 된다면
            '다음으로 넘어간다.
            'RGBA = 0일 때 까지 1씩 더한다.
            '만약 255가 된다면
            '다음으로 넘어간다.
            '640을 다 쓰면 00 00 을 출력하고 다음 줄로 넘어간다.

        Next


        'Dim value As UInteger

        'returntext.AppendLine("    offset = f_epdread_epd(f_epdread_epd(EPD(0x6D5E14)) + 2)")
        'returntext.AppendLine("    DoActions([")
        'returntext.AppendLine("        SetMemory(0x6509B0, SetTo, offset),")
        'For i = 0 To Sdata.Count - 4 Step 4
        '    value = Sdata.Item(i) + Sdata.Item(i + 1) * 2 ^ 8 + Sdata.Item(i + 2) * 2 ^ 16 + Sdata.Item(i + 3) * 2 ^ 24
        '    returntext.AppendLine("        SetDeaths(CurrentPlayer , SetTo, " & value & ", 0),")
        '    returntext.AppendLine("        SetMemory(0x6509B0, Add, 1),")
        '    ' SetFile.Write("    f_dwwrite_epd(offset + " & i \ 4 & ", " & value & ")" & vbCrLf)
        'Next
        'returntext.AppendLine("        SetMemory(0x6509B0, SetTo, 0),")
        'returntext.AppendLine("    ])")


        returntext.AppendLine("    DoActions([")
        returntext.AppendLine("        SetMemory(0x" & Hex(("&H" & ReadOffset("Vanilla")) + 4) & " , SetTo, f_dwread_epd(f_epdread_epd(EPD(0x6D5E14)) + 2)),")
        returntext.AppendLine("    ])")

        Dim Stransfilestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\temp\STransDat", FileMode.Create)
        Dim StransBinaryWriter As New BinaryWriter(Stransfilestream)


        StransBinaryWriter.Write(Sdata.ToArray)




        StransBinaryWriter.Close()
        Stransfilestream.Close()

        Return returntext.ToString
    End Function


    Private Function GetImageArray(FileName As String) As ArrayList
        Dim temp As New ArrayList

        Dim filestream As New FileStream(FileName, FileMode.Open)
        Dim binaryReader As New BinaryReader(filestream)


        filestream.Position = 18
        Dim bmpfilesize As New Size
        bmpfilesize.Width = binaryReader.ReadUInt32() '가로
        bmpfilesize.Height = binaryReader.ReadUInt32()  '세로

        Dim realWidth As UInteger
        If bmpfilesize.Width Mod 4 = 0 Then
            realWidth = bmpfilesize.Width
        Else
            realWidth = bmpfilesize.Width - bmpfilesize.Width Mod 4 + 4
        End If


        For y = bmpfilesize.Height - 1 To 0 Step -1
            filestream.Position = &H436 + y * realWidth
            For x = 0 To bmpfilesize.Width - 1
                temp.Add(binaryReader.ReadByte)
            Next
        Next



        binaryReader.Close()
        filestream.Close()

        Return temp
    End Function
    Private Function DrawImage(pointer As String, FileName As String) As String
        Dim returntext As New StringBuilder()

        Dim pointerv As UInteger = "&H" & pointer

        returntext.AppendLine("    offset = f_epdread_epd(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 4 & ")")
        returntext.AppendLine("    DoActions([" & vbCrLf)
        returntext.AppendLine("        SetMemory(0x6509B0, SetTo, offset),")

        Dim temp As ArrayList
        temp = GetImageArray(FileName)

        For k = 0 To temp.Count - 4 Step 4
            Dim value As UInteger
            value = temp.Item(k) + temp.Item(k + 1) * 2 ^ 8 + temp.Item(k + 2) * 2 ^ 16 + temp.Item(k + 3) * 2 ^ 24
            returntext.AppendLine("        SetDeaths(CurrentPlayer , SetTo, " & value & ", 0),")
            returntext.AppendLine("        SetMemory(0x6509B0, Add, 1),")
        Next
        returntext.AppendLine("        SetMemory(0x6509B0, SetTo, 0),")
        returntext.AppendLine("    ])" & vbCrLf)


        Return returntext.ToString
    End Function
    Public Function GetPlibText(ScenNum As UInteger, race As Byte) As String
        Dim returntext As New StringBuilder()

        returntext.AppendLine(GetMaskingData(race, ScenNum))


        For dianum = 0 To 25
            Dim racehock As Boolean = False
            'For i = 0 To 4
            '    ListBox2.Items.Add(binfilename(i))
            'Next

            Select Case race
                Case 0
                    Select Case dianum
                        Case 0 To 4
                            racehock = True
                        Case 5 To 10
                            racehock = True
                    End Select
                Case 1
                    Select Case dianum
                        Case 0 To 4
                            racehock = True
                        Case 11 To 17
                            racehock = True
                    End Select
                Case 2
                    Select Case dianum
                        Case 0 To 4
                            racehock = True
                        Case 18 To 25
                            racehock = True
                    End Select
            End Select



            If racehock = True Then
                'MsgBox(binfilename(dianum))

                If PjcutData(ScenNum).binData(dianum).pos.X <> BinfileData.binData(dianum).pos.X Or
               PjcutData(ScenNum).binData(dianum).pos.Y <> BinfileData.binData(dianum).pos.Y Or
               PjcutData(ScenNum).binData(dianum).size.Width <> BinfileData.binData(dianum).size.Width Or
               PjcutData(ScenNum).binData(dianum).size.Height <> BinfileData.binData(dianum).size.Height Then
                    With PjcutData(ScenNum).binData(dianum)
                        returntext.AppendLine(MovePoint(binfileptr(dianum), 0, .pos.X, .pos.Y, .size.Width, .size.Height, BinfileData.binData(dianum).size.Width, BinfileData.binData(dianum).size.Height))
                    End With
                End If
                'f_dwwrite_epd(f_epdread_epd(EPD(0x68C234)) + 1, 8388714)
                'f_dwwrite_epd(f_epdread_epd(EPD(0x68C234)) + 2, 9634317)
                'DoActions([
                '    SetDeaths(f_epdread_epd(EPD(0x68C234)) + 13, Subtract, 1048576, 0),
                '    SetDeaths(f_epdread_epd(EPD(0x68C234)) + 14, Subtract, 18, 0),
                '])
                For objnum = 0 To PjcutData(ScenNum).binData(dianum).ObjDlg.Count - 1
                    If PjcutData(ScenNum).binData(dianum).ObjDlg(objnum).pos.X <> BinfileData.binData(dianum).ObjDlg(objnum).pos.X Or
                       PjcutData(ScenNum).binData(dianum).ObjDlg(objnum).pos.Y <> BinfileData.binData(dianum).ObjDlg(objnum).pos.Y Then
                        With PjcutData(ScenNum).binData(dianum).ObjDlg(objnum)
                            returntext.AppendLine(MovePoint(binfileptr(dianum), objnum + 1, .pos.X, .pos.Y, .size.Width, .size.Height, BinfileData.binData(dianum).ObjDlg(objnum).pos.X, BinfileData.binData(dianum).ObjDlg(objnum).pos.Y))
                        End With
                    End If
                Next
            End If

        Next
        'binfileptr(i) 포인터
        '포인터 + 86 * 오브제아이디
        '            PjcutData(ScenNum).binData(i)
        '뒷 내용에 65536

        Return returntext.ToString
    End Function

    Private Function MovePoint(pointer As String, diaNum As Byte, l As Short, u As Short, width As Short, height As Short, oldwidth As Short, oldheight As Short) As String
        Dim returntext As New StringBuilder()
        'dignum이 0이거나 1이거나 경우. 그냥 전부다 Add, Subtraft 쓰자.


        Dim pointerv As UInteger = "&H" & pointer

        If diaNum = 0 Then
            '왼쪽, 위쪽
            returntext.AppendLine("    f_dwwrite_epd(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 1 & ", " & l + u * 65536 & ")")


            '오른쪽, 아래
            returntext.AppendLine("    f_dwwrite_epd(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 2 & ", " & l + width - 1 + (u + height - 1) * 65536 & ")")


            '넓이, 높이
            returntext.AppendLine("    f_dwwrite_epd(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 3 & ", " & width + (height) * 65536 & ")")





            returntext.AppendLine("    DoActions([")
            Dim modi As String = "Add"


            If (width - oldwidth) * 65536 > 0 Then
                modi = "Add"
            Else
                modi = "Subtract"
            End If
            returntext.AppendLine("        SetDeaths(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 13 & ", " & modi & ", " & Math.Abs((width - oldwidth) * 65536) & ", 0),")


            If (height - oldheight) * 65536 > 0 Then
                modi = "Add"
            Else
                modi = "Subtract"
            End If
            returntext.AppendLine("        SetDeaths(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 14 & ", " & modi & ", " & Math.Abs(height - oldheight) & ", 0),")
            returntext.AppendLine("    ])")
        Else
            If diaNum Mod 2 = 0 Then
                '왼쪽, 위쪽
                returntext.AppendLine("    f_dwwrite_epd(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 1 + ((diaNum) * 86) \ 4 & ", " & l + u * 65536 & ")")


                '오른쪽, 아래
                returntext.AppendLine("    f_dwwrite_epd(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 2 + ((diaNum) * 86) \ 4 & ", " & l + width - 1 + (u + height - 1) * 65536 & ")")

            Else
                returntext.AppendLine("    DoActions([")
                Dim modi As String = "Add"


                If (l - oldwidth) * 65536 > 0 Then
                    modi = "Add"
                Else
                    modi = "Subtract"
                End If
                returntext.AppendLine("        SetDeaths(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 1 + ((diaNum) * 86) \ 4 & ", " & modi & ", " & Math.Abs((l - oldwidth) * 65536) & ", 0),")





                returntext.AppendLine("        SetDeaths(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 2 + ((diaNum) * 86) \ 4 & ", SetTo, " & u + (l + width - 1) * 65536 & ", 0),")




                If (u - oldwidth) * 65536 > 0 Then
                    modi = "Add"
                Else
                    modi = "Subtract"
                End If
                returntext.AppendLine("        SetDeaths(f_epdread_epd(" & GetEPD(pointerv) & ") + " & 3 + ((diaNum) * 86) \ 4 & ", " & modi & ", " & Math.Abs(u - oldheight) & ", 0),")
                returntext.AppendLine("    ])")
            End If
        End If

        Return returntext.ToString
    End Function

    Private Function GetEPD(Pointer As UInteger) As Integer
        Return (Pointer - &H58A364) \ 4
    End Function


End Module
