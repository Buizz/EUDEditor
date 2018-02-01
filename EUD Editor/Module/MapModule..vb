Imports System.IO
Imports System.Text

Module MapModule
    Public tilesetname() As String = {"badlands", "platform", "install", "ashworld", "jungle", "desert", "ice", "twilight"}

    Private MOVINGDATA(,) As UInt16 = {{192, 288, 288, 32, 64, 64, 96, 864, 864},'badlands
                                        {288, 192, 3731, 224, 64, 128, 32, 4992, 3424},'platform
                                        {32, 96, 128, 256, 256, 256, 192, 928, 2080},'install
                                        {64, 128, 128, 32, 96, 96, 224, 2080, 2080},'ashworld
                                        {224, 320, 384, 32, 64, 64, 96, 576, 7968},'jungle
                                        {160, 256, 384, 32, 64, 64, 96, 576, 7856},'desert
                                        {96, 256, 384, 32, 288, 288, 192, 352, 7968},'ice
                                        {160, 256, 384, 32, 288, 288, 96, 2818, 7856}} 'twilight
    Private Magaindex() As UInt16


    Public MapPalett(255) As Color


    Public TileSetType As Integer
    Public MapSize As Size
    Public MTXMDATA() As UInt16



    Public TilebitDATA()() As Byte '하나 당 1024



    'cv5, vx4, vr4
    'cv5 = 그룹 정의.
    'vx4 = 각 메가타일에 대한 미니타일 
    'vr4 = 비트맵 데이터
    Public ProjectMTXMDATA() As Byte
    Public ProjectTileUseFile As Boolean
    Public ProjectTileSetFileName As String
    Public ProjectTIleMSet() As Byte
    Public Enum MoveType
        Defacult = 0

        LowO = 1
        MidO = 2
        HighO = 3

        CLowO = 4
        CMidO = 5
        CHighO = 6

        LowX = 7
        MidX = 8
        HighX = 9

    End Enum


    Public ProjectTileSetData As New List(Of CTileSet)
    Public Class CTileSet
        Public Sub New(filename As String, TilesetN As UInteger, ismake As Boolean)
            isMaker = ismake
            TileSetNum = TilesetN


            Dim filestream As New FileStream(filename, FileMode.Open)
            Dim binaryReader As New BinaryReader(filestream)


            filestream.Position = &H436
            For i = 31 To 0 Step -1
                For j = 0 To 31
                    Try
                        TileSetData(i * 32 + j) = binaryReader.ReadByte
                    Catch ex As Exception

                    End Try
                Next
            Next


            binaryReader.Close()
            filestream.Close()
        End Sub
        Public Sub New(filename() As String, TilesetN As UInteger, ismake As Boolean)
            isMaker = ismake
            TileSetNum = TilesetN


            Dim bytebuffer(1024) As Byte
            For i = 0 To 1023
                bytebuffer(i) = filename(i).Trim
            Next

            Dim memstream As New MemoryStream(bytebuffer, FileMode.Open)
            Dim binaryReader As New BinaryReader(memstream)

            For i = 0 To 31
                For j = 0 To 31
                    Try
                        TileSetData(i * 32 + j) = binaryReader.ReadByte
                    Catch ex As Exception

                    End Try
                Next
            Next

            binaryReader.Close()
            memstream.Close()
        End Sub


        Public isMaker As Boolean
        Public TileSetData(1024) As Byte
        Public TileSetNum As UInteger
    End Class


    Public Sub AdJustNewMTXN()
        Dim f As New FileInfo(ProjectSet.InputMap)

        f.CopyTo(My.Application.Info.DirectoryPath & "\Data\temp\" & "map.scx", True)





        Dim hmpq As UInteger
        Dim hfile As UInteger
        Dim buffer() As Byte
        Dim filesize As UInteger
        Dim temptext As String = ""
        Dim size As Integer

        Dim pdwread As IntPtr

        StormLib.SFileOpenArchive(My.Application.Info.DirectoryPath & "\Data\temp\" & "map.scx", 0, 0, hmpq)


        Dim openFilename As String = "staredit\scenario.chk"

        StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)

        If hfile <> 0 Then
            filesize = StormLib.SFileGetFileSize(hfile, filesize)
            ReDim buffer(filesize)
            StormLib.SFileReadFile(hfile, buffer, filesize, pdwread, 0)

            Dim mem As MemoryStream = New MemoryStream(buffer)
            Dim binaryW As BinaryWriter = New BinaryWriter(mem)
            Dim binary As BinaryReader = New BinaryReader(mem)
            Dim stream As StreamReader = New StreamReader(mem, Encoding.ASCII)
            temptext = stream.ReadToEnd



            mem.Position = InStr(temptext, "MTXM") + 3

            size = binary.ReadUInt32

            Dim mtxmfileCreator As New FileStream(My.Application.Info.DirectoryPath & "\Data\temp\" & "MTXM", FileMode.Create)
            Dim mtxmbinaryw As New BinaryWriter(mtxmfileCreator)

            Dim fmtxmfileCreator As New FileStream(My.Application.Info.DirectoryPath & "\Data\temp\" & "FMTXM", FileMode.Create)
            Dim fmtxmbinaryw As New BinaryWriter(fmtxmfileCreator)
            For i = 0 To MTXMDATA.Length - 1
                Dim Group As UInt16 = MTXMDATA(i) \ 16 'Group
                Dim index As Byte = MTXMDATA(i) Mod 16 'index
                mtxmbinaryw.Write(Group << 4 Or index)
                fmtxmbinaryw.Write(Magaindex(MTXMDATA(i)))
                'Magaindex
                If ProjectMTXMDATA(i) = 0 Then
                    Select Case ProjectTIleMSet(MTXMDATA(i))
                        Case 0
                            binaryW.Write(Group << 4 Or index)
                        Case Else
                            If MOVINGDATA(TileSetType, ProjectTIleMSet(MTXMDATA(i)) - 1) = UInt16.MaxValue Then
                                binaryW.Write(Group << 4 Or index)
                            Else
                                binaryW.Write(MOVINGDATA(TileSetType, ProjectTIleMSet(MTXMDATA(i)) - 1))
                            End If
                    End Select
                Else

                    binaryW.Write(MOVINGDATA(TileSetType, ProjectMTXMDATA(i) - 1))
                End If

            Next
            fmtxmbinaryw.Close()
            fmtxmfileCreator.Close()

            mtxmbinaryw.Close()
            mtxmfileCreator.Close()

            'Dim Group As UInt16 = (v And &HFFF0) >> 4
            'Dim index As Byte = v And &HF

            'MTXMDATA(i) = Group * 16 + index
            'MsgBox(CheckFileExist(ProjectTileSetFileName))
            If ProjectTileUseFile = True And (CheckFileExist(ProjectTileSetFileName) = False) Then
                Dim vr4fileCreator As New FileStream(My.Application.Info.DirectoryPath & "\Data\temp\" & "vr4", FileMode.Create)
                Dim vr4binaryw As New BinaryWriter(vr4fileCreator)

                Dim Tilefilestream As New FileStream(ProjectTileSetFileName, FileMode.Open)
                Dim binaryReader As New BinaryReader(Tilefilestream)


                Tilefilestream.Position = 18
                Dim bmpfilesize As New Size
                bmpfilesize.Width = binaryReader.ReadUInt32() \ 32 '가로
                bmpfilesize.Height = binaryReader.ReadUInt32() \ 32 '세로


                For by = 0 To bmpfilesize.Height - 1
                    For bx = 0 To bmpfilesize.Width - 1
                        Dim tilenum As UInteger = bx + by * bmpfilesize.Width
                        Dim tgroup As UInt16 = tilenum \ 16
                        Dim tindex As Byte = tilenum Mod 16


                        For j = 0 To 15
                            For k = 0 To 7 'y
                                Tilefilestream.Position = &H436 + (j Mod 4) * 8 + bx * 32 + (bmpfilesize.Height * 32 - 1 - by * 32 - k - (j \ 4) * 8) * bmpfilesize.Width * 32 ' + (x) + (y) *  bmpfilesize.Width
                                vr4binaryw.Write(binaryReader.ReadBytes(8))
                            Next
                        Next
                    Next
                Next





                binaryReader.Close()
                Tilefilestream.Close()


                vr4binaryw.Close()
                vr4fileCreator.Close()
            End If




            Dim filestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\temp\scenario.chk", FileMode.Create)

            filestream.Write(buffer, 0, buffer.Length - 1)

            filestream.Close()



            stream.Close()
            binary.Close()
            binaryW.Close()
            mem.Close()
            StormLib.SFileCloseFile(hfile)

        End If


        StormLib.SFileCloseArchive(hmpq)
        MPQlib.RemoveFile("staredit\scenario.chk", My.Application.Info.DirectoryPath & "\Data\temp\" & "map.scx")
        MPQlib.AddFile(My.Application.Info.DirectoryPath & "\Data\temp\scenario.chk", "staredit\scenario.chk", My.Application.Info.DirectoryPath & "\Data\temp\" & "map.scx") ' 


    End Sub
    Public Sub SaveTOCHK()
        Dim hmpq As UInteger
        Dim hfile As UInteger
        Dim buffer() As Byte
        Dim filesize As UInteger
        Dim temptext As String = ""
        Dim size As Integer

        Dim pdwread As IntPtr

        StormLib.SFileOpenArchive(ProjectSet.InputMap, 0, 0, hmpq)


        Dim openFilename As String = "staredit\scenario.chk"

        StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)

        If hfile <> 0 Then
            filesize = StormLib.SFileGetFileSize(hfile, filesize)
            ReDim buffer(filesize)
            StormLib.SFileReadFile(hfile, buffer, filesize, pdwread, 0)

            Dim mem As MemoryStream = New MemoryStream(buffer)
            Dim binaryW As BinaryWriter = New BinaryWriter(mem)
            Dim binary As BinaryReader = New BinaryReader(mem)
            Dim stream As StreamReader = New StreamReader(mem, Encoding.ASCII)
            temptext = stream.ReadToEnd



            mem.Position = InStr(temptext, "MTXM") + 3

            size = binary.ReadUInt32
            For i = 0 To MTXMDATA.Length - 1
                Dim Group As UInt16 = MTXMDATA(i) \ 16 'Group
                Dim index As Byte = MTXMDATA(i) Mod 16 'index

                binaryW.Write(Group << 4 Or index)
            Next

            mem.Position = InStr(temptext, "TILE") + 3

            size = binary.ReadUInt32
            For i = 0 To MTXMDATA.Length - 1
                Dim Group As UInt16 = MTXMDATA(i) \ 16 'Group
                Dim index As Byte = MTXMDATA(i) Mod 16 'index

                binaryW.Write(Group << 4 Or index)
            Next

            'Dim Group As UInt16 = (v And &HFFF0) >> 4
            'Dim index As Byte = v And &HF

            'MTXMDATA(i) = Group * 16 + index




            Dim filestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\temp\scenario.chk", FileMode.Create)

            filestream.Write(buffer, 0, buffer.Length - 1)

            filestream.Close()



            stream.Close()
            binary.Close()
            binaryW.Close()
            mem.Close()
            StormLib.SFileCloseFile(hfile)

        End If


        StormLib.SFileCloseArchive(hmpq)
        MPQlib.RemoveFile("staredit\scenario.chk")
        MPQlib.AddFile(My.Application.Info.DirectoryPath & "\Data\temp\scenario.chk", "staredit\scenario.chk") ' 
    End Sub


    Public Sub LoadTILEDATA(Optional isgraphic As Boolean = False, Optional OnlyMTXM As Boolean = False)

        Dim hmpq As UInteger
        Dim hfile As UInteger
        Dim buffer() As Byte
        Dim filesize As UInteger
        Dim temptext As String = ""
        Dim size As Integer

        Dim pdwread As IntPtr

        StormLib.SFileOpenArchive(ProjectSet.InputMap, 0, 0, hmpq)


        Dim openFilename As String = "staredit\scenario.chk"

        StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)

        If hfile <> 0 Then
            filesize = StormLib.SFileGetFileSize(hfile, filesize)
            ReDim buffer(filesize)

            StormLib.SFileReadFile(hfile, buffer, filesize, pdwread, 0)

            Dim mem As MemoryStream = New MemoryStream(buffer)
            Dim binary As BinaryReader = New BinaryReader(mem)
            Dim stream As StreamReader = New StreamReader(mem, Encoding.ASCII)
            temptext = stream.ReadToEnd



            mem.Position = InStr(temptext, "ERA ") + 3

            size = binary.ReadUInt32

            TileSetType = binary.ReadUInt16

            '00 = Badlands                  08 10 18 . . . . => 0 = 8
            '01 = Space Platform         09 11 19 . . . . => 1 = 9
            '02 = Installation                0A 12 1A . . . . => 2 = A
            '03 = Ash World                 0B 13 1B . . . .=> 3 = B
            '04 = Jungle World             0C 14 1C . . . .=> 4 = C
            '05 = Desert                       0D 15 1D . . . .=> 5 = D
            '06 = Ice                             0E 16 1E . . . .=> 6 = E
            '07 = Twilight                      0F 17 1F . . . . => 7 = F

            MapLoadPalette(TileSetType)




            mem.Position = InStr(temptext, "DIM ") + 3

            size = binary.ReadUInt32

            MapSize.Width = binary.ReadUInt16() '가로
            MapSize.Height = binary.ReadUInt16() '세로
            'MsgBox(MapSize.Width & ", " & MapSize.Height)

            If isgraphic = False Or OnlyMTXM = True Then
                mem.Position = InStr(temptext, "MTXM") + 3

                size = binary.ReadUInt32

                ReDim MTXMDATA(MapSize.Width * MapSize.Height - 1)


                For i = 0 To MTXMDATA.Count - 1
                    Dim v As UInt16 = binary.ReadUInt16()


                    Dim Group As UInt16 = (v And &HFFF0) >> 4
                    Dim index As Byte = v And &HF

                    MTXMDATA(i) = Group * 16 + index
                Next
                'MsgBox(Group & ", " & index)

                'MsgBox(MapSize.Width & ", " & MapSize.Height)
            End If


            If OnlyMTXM = False Then




                Dim mpq As New SFMpq


                Dim cv5() As Byte = mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & ".cv5")
                Dim cv5mem As MemoryStream = New MemoryStream(cv5)
                Dim cv5binary As BinaryReader = New BinaryReader(cv5mem)
                Dim cv5binaryw As BinaryWriter = New BinaryWriter(cv5mem)


                'ReDim TilebitDATA(63)
                Dim vx4() As Byte = mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & ".vx4")
                Dim vf4() As Byte = mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & ".vf4")
                Dim vr4() As Byte = mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & ".vr4")


                Dim vx4mem As MemoryStream = New MemoryStream(vx4)
                Dim vx4binary As BinaryReader = New BinaryReader(vx4mem)
                Dim vx4binaryw As BinaryWriter = New BinaryWriter(vx4mem)


                Dim vf4mem As MemoryStream = New MemoryStream(vf4)
                Dim vf4binary As BinaryReader = New BinaryReader(vf4mem)
                Dim vf4binaryw As BinaryWriter = New BinaryWriter(vf4mem)


                Dim vr4mem As MemoryStream = New MemoryStream(vr4)
                Dim vr4binary As BinaryReader = New BinaryReader(vr4mem)
                Dim vr4binaryw As BinaryWriter = New BinaryWriter(vr4mem)
                '미리 읽기
                If ProjectTileUseFile = False Then
                    For i = 0 To ProjectTileSetData.Count - 1
                        With ProjectTileSetData(i)
                            If .isMaker = True Then
                                Dim tgroup As UInt16 = .TileSetNum \ 16
                                Dim tindex As Byte = .TileSetNum Mod 16
                                cv5mem.Position = &H14 + 52 * tgroup + 2 * tindex

                                cv5binaryw.Write(CUShort(.TileSetNum))


                                vx4mem.Position = 32 * .TileSetNum
                                For j = 0 To 15
                                    Dim vr4index As UInt16 = .TileSetNum * 16 + j
                                    vx4binaryw.Write(vr4index << 1)


                                    Dim ptr As Integer = 0

                                    For k = 0 To 7 'y
                                        For p = 0 To 7 'x
                                            vr4(ptr + vr4index * 64) = .TileSetData(p + k * 32 + 8 * (j Mod 4) + 256 * (j \ 4))
                                            'vr4(ptr + vr4index * 64) = .TileSetData(p + k * 32 + 8 * (j Mod 4) + 256 * (j \ 4))

                                            ptr += 1
                                        Next
                                    Next
                                Next



                            Else
                                Dim tgroup As UInt16 = .TileSetNum \ 16
                                Dim tindex As Byte = .TileSetNum Mod 16
                                cv5mem.Position = &H14 + 52 * tgroup + 2 * tindex




                                vx4mem.Position = 32 * cv5binary.ReadUInt16()
                                For j = 0 To 15
                                    Dim tvalue As UInt16 = vx4binary.ReadUInt16()

                                    Dim flipflag As Boolean = tvalue And 1
                                    Dim vr4index As UInt16 = (tvalue And 65534) \ 2


                                    Dim ptr As Integer = 0
                                    If flipflag = True Then
                                        For k = 0 To 7 'y
                                            For p = 0 To 7 'x
                                                vr4(ptr + vr4index * 64) = .TileSetData(7 - p + k * 32 + 8 * (j Mod 4) + 256 * (j \ 4))
                                                'vr4(ptr + vr4index * 64) = .TileSetData(7 - p + k * 32 + 8 * (j Mod 4) + 256 * (j \ 4))
                                                ptr += 1
                                            Next
                                        Next
                                    Else
                                        For k = 0 To 7 'y
                                            For p = 0 To 7 'x
                                                vr4(ptr + vr4index * 64) = .TileSetData(p + k * 32 + 8 * (j Mod 4) + 256 * (j \ 4))
                                                'vr4(ptr + vr4index * 64) = .TileSetData(p + k * 32 + 8 * (j Mod 4) + 256 * (j \ 4))

                                                ptr += 1
                                            Next
                                        Next
                                    End If
                                Next

                            End If
                        End With

                    Next
                ElseIf CheckFileExist(ProjectTileSetFileName) = False Then

                    Dim filestream As New FileStream(ProjectTileSetFileName, FileMode.Open)
                    Dim binaryReader As New BinaryReader(filestream)


                    filestream.Position = 18
                    Dim bmpfilesize As New Size
                    bmpfilesize.Width = binaryReader.ReadUInt32() \ 32 '가로
                    bmpfilesize.Height = binaryReader.ReadUInt32() \ 32 '세로


                    For by = 0 To bmpfilesize.Height - 1
                        For bx = 0 To bmpfilesize.Width - 1
                            Dim tilenum As UInteger = bx + by * bmpfilesize.Width
                            Dim tgroup As UInt16 = tilenum \ 16
                            Dim tindex As Byte = tilenum Mod 16
                            cv5mem.Position = &H14 + 52 * tgroup + 2 * tindex

                            cv5binaryw.Write(CUShort(tilenum))


                            vx4mem.Position = 32 * tilenum

                            For j = 0 To 15
                                Dim vr4index As UInt16 = tilenum * 16 + j
                                vx4binaryw.Write(vr4index << 1)

                                vr4mem.Position = vr4index * 64
                                For k = 0 To 7 'y
                                    filestream.Position = &H436 + (j Mod 4) * 8 + bx * 32 + (bmpfilesize.Height * 32 - 1 - by * 32 - k - (j \ 4) * 8) * bmpfilesize.Width * 32 ' + (x) + (y) *  bmpfilesize.Width
                                    vr4binaryw.Write(binaryReader.ReadBytes(8))
                                Next
                            Next
                        Next
                    Next





                    binaryReader.Close()
                    filestream.Close()

                End If






                If ProjectTIleMSet Is Nothing Then
                    ReDim ProjectTIleMSet((cv5mem.Length \ 52) * 16)
                ElseIf ProjectTIleMSet.Length <> (cv5mem.Length \ 52) * 16 + 1 Then
                    ReDim ProjectTIleMSet((cv5mem.Length \ 52) * 16)
                End If
                If ProjectMTXMDATA Is Nothing Then
                    ReDim ProjectMTXMDATA(MapSize.Width * MapSize.Height - 1)
                ElseIf ProjectMTXMDATA.Length <> MapSize.Width * MapSize.Height Then
                    ReDim ProjectMTXMDATA(MapSize.Width * MapSize.Height - 1)
                End If

                ReDim Magaindex((cv5mem.Length \ 52) * 16)
                ReDim TilebitDATA((cv5mem.Length \ 52) * 16)
                'MsgBox((cv5mem.Length \ 52) * 16)

                For Groupnum = 0 To cv5mem.Length \ 52 - 1
                    Dim cv5Flag As Boolean = False

                    cv5mem.Position = 2 + 52 * Groupnum
                    Dim cv5Flags As Byte = cv5binary.ReadByte()
                    'MsgBox(cv5Flags And &HF0)

                    '0, 4, 8
                    'B, C, U
                    If ((cv5Flags And &HF0) \ 16 = 0) Then
                        cv5Flag = True
                    End If


                    cv5mem.Position = &H14 + 52 * Groupnum
                    For megnum = 0 To 15
                        Dim megeindex As UInt16 = cv5binary.ReadUInt16() '메가인덱스 캐싱하자.
                        Magaindex(Groupnum * 16 + megnum) = megeindex

                        ReDim TilebitDATA(megnum + Groupnum * 16)(1023)

                        Dim tilebitStream As New MemoryStream(TilebitDATA(megnum + Groupnum * 16))
                        Dim tilebitbinw As New BinaryWriter(tilebitStream)



                        vx4mem.Position = 32 * megeindex
                        vf4mem.Position = 32 * megeindex
                        For i = 0 To 15
                            Dim flag As UInt16 = vf4binary.ReadUInt16()
                            '1 Walk
                            '2 mid
                            '4 High
                            If True Then '((flag And 7) = 4) Then 'And cv5Flag = True Then

                                Dim tvalue As UInt16 = vx4binary.ReadUInt16()

                                Dim flipflag As Boolean = tvalue And 1
                                Dim vr4index As UInt16 = (tvalue And 65534) \ 2


                                Dim ptr As Integer = 0
                                vr4mem.Position = vr4index * 64
                                If flipflag = True Then
                                    For k = 0 To 7 'y
                                        For p = 0 To 7 'x
                                            TilebitDATA(megnum + Groupnum * 16)(7 - p + k * 32 + 8 * (i Mod 4) + 256 * (i \ 4)) = vr4(ptr + vr4index * 64)
                                            ptr += 1
                                        Next
                                    Next
                                Else

                                    For k = 0 To 7 'y
                                        tilebitStream.Position = k * 32 + 8 * (i Mod 4) + 256 * (i \ 4)
                                        tilebitbinw.Write(vr4binary.ReadBytes(8))
                                        'For p = 0 To 7 'x
                                        '    TilebitDATA(megnum + Groupnum * 16)(p + k * 32 + 8 * (i Mod 4) + 256 * (i \ 4)) = vr4(ptr + vr4index * 64)

                                        '    ptr += 1
                                        'Next
                                    Next
                                End If

                            Else
                                Dim color() As Byte = {&H6F, &HA5, &H9F, &HA4, &H9C, &H13, &H54, &H87, &HB9}

                                Dim colors As Byte


                                If ((cv5Flags And &HF0) \ 16 = 8) Then '건설 불가능.
                                    If ((flag And 7) = 1) Then
                                        colors = color(0)
                                    End If
                                    If ((flag And 7) = 3) Then
                                        colors = color(1)
                                    End If
                                    If ((flag And 7) = 5) Then
                                        colors = color(2)
                                    End If
                                End If
                                If ((cv5Flags And &HF0) \ 16 = 0) Then '건설 가능.
                                    If ((flag And 7) = 1) Then
                                        colors = color(3)
                                    End If
                                    If ((flag And 7) = 3) Then
                                        colors = color(4)
                                    End If
                                    If ((flag And 7) = 5) Then
                                        colors = color(5)
                                    End If
                                End If
                                If ((flag And 7) = 0) Then
                                    colors = color(6)
                                End If
                                If ((flag And 7) = 2) Then
                                    colors = color(7)
                                End If
                                If ((flag And 7) = 4) Then
                                    colors = color(8)
                                End If


                                For k = 0 To 7 'y
                                    For p = 0 To 7 'x


                                        TilebitDATA(megnum + Groupnum * 16)(p + k * 32 + 8 * (i Mod 4) + 256 * (i \ 4)) = colors
                                    Next
                                Next
                            End If
                        Next
                        tilebitbinw.Close()
                        tilebitStream.Close()
                    Next
                Next











                'i mod 4 = i의 X좌표
                'i \ 4  = i 의 Y좌표

                vr4binary.Close()
                vr4binaryw.Close()
                vr4mem.Close()

                cv5binaryw.Close()
                cv5binary.Close()
                cv5mem.Close()

                vx4binaryw.Close()
                vx4binary.Close()
                vx4mem.Close()

                vf4binaryw.Close()
                vf4binary.Close()
                vf4mem.Close()

            End If


            '로딩 끝
            stream.Close()
            binary.Close()
            mem.Close()


            StormLib.SFileCloseFile(hfile)
        Else
            Exit Sub
        End If

        StormLib.SFileCloseArchive(hmpq)
    End Sub

    Public Function MapLoadPalette(PalletNum As Byte)


        Dim Filename As String = ""

        Filename = My.Application.Info.DirectoryPath & "\Data\Palletes\" & tilesetname(TileSetType) & ".wpe"
        '00 = Badlands                  08 10 18 . . . . => 0 = 8
        '01 = Space Platform         09 11 19 . . . . => 1 = 9
        '02 = Installation                0A 12 1A . . . . => 2 = A
        '03 = Ash World                 0B 13 1B . . . .=> 3 = B
        '04 = Jungle World             0C 14 1C . . . .=> 4 = C
        '05 = Desert                       0D 15 1D . . . .=> 5 = D
        '06 = Ice                             0E 16 1E . . . .=> 6 = E
        '07 = Twilight                      0F 17 1F . . . . => 7 = F

        Dim filestream As New FileStream(Filename, FileMode.Open)
        Dim binaryreader As New BinaryReader(filestream)

        If filestream.Length = 256 * 3 Then
            For i = 0 To 255
                Dim r, g, b As Byte
                r = binaryreader.ReadByte()
                g = binaryreader.ReadByte()
                b = binaryreader.ReadByte()
                MapPalett(i) = Color.FromArgb(r, g, b)
            Next
        Else
            For i = 0 To 255
                Dim r, g, b As Byte
                r = binaryreader.ReadByte()
                g = binaryreader.ReadByte()
                b = binaryreader.ReadByte()
                binaryreader.ReadByte()


                MapPalett(i) = Color.FromArgb(r, g, b)
            Next
        End If


        binaryreader.Close()
        filestream.Close()





        Return 0
    End Function

End Module
