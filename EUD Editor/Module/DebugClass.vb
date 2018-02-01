Imports System.IO

Public Class CGameData
    Enum GameMode
        ingame = 3
    End Enum

    Public RemappingPallet(6) As CPCX
    Public Class CPCX
        Private PCXdata(,) As Byte 'Byte(,)
        Public Function GetPixel(OrigP As Byte, RemapP As Byte) As Byte
            If RemapP < (PCXdata.Length \ 256) Then
                If RemapP > 0 Then
                    Return PCXdata(RemapP - 1, OrigP)
                Else
                    Return PCXdata(RemapP, OrigP)
                End If
            Else
                Return PCXdata(0, OrigP)
            End If
        End Function

        Public Sub LoadPCX(buffer() As Byte)
            Dim memStream As New MemoryStream(buffer)
            Dim binaReader As New BinaryReader(memStream)

            memStream.Position = &H8
            Dim pwidth As UInt16 = binaReader.ReadUInt16()
            Dim pheight As UInt16 = binaReader.ReadUInt16()
            ReDim PCXdata(pheight, pwidth)

            memStream.Position = &H80

            Dim xpos As UInt16

            Dim opcode As Byte
            'MsgBox(pheight & " Start")


            For ypos = 0 To pheight
                'If buffer.Length = 9455 Then
                '    MsgBox(ypos & "," & pheight)
                'End If
                xpos = 0
                While xpos <= pwidth
                    opcode = binaReader.ReadByte()
                    'If buffer.Length = 9455 Then
                    '    MsgBox(xpos & "," & pwidth & "," & (opcode And 192) & "," & (opcode And 63))
                    'End If
                    If opcode >= &HC0 Then '만큼 다음 바이트 출력
                        Dim newxtcode As Byte = binaReader.ReadByte()

                        For i = 0 To opcode - &HC1
                            If xpos <= pwidth Then
                                PCXdata(ypos, xpos) = newxtcode
                            End If
                            xpos += 1
                        Next
                    Else
                        PCXdata(ypos, xpos) = opcode
                        xpos += 1
                    End If
                End While
            Next
            'MsgBox(pheight & " End")



            'Dim str As String = ""
            'For i = 0 To 0
            '    If pheight = 0 Then
            '        For j = 0 To pwidth
            '            str = str & Hex(PCXdata(i, j)) & " "
            '        Next
            '        str = str & vbCrLf
            '        MsgBox(str)
            '    End If
            'Next




            binaReader.Close()
            memStream.Close()
        End Sub
    End Class


    Enum Remapping
        ofire = 0
        gfire = 1
        bfire = 2
        bexpl = 3
        trans50 = 4

        dark = 5
        shift = 6

        'DrawFunction = 5 trans50.pcx
        'DrawFunction = 6 trans50.pcx
        'DrawFunction = 7 trans50.pcx
    End Enum

    Private STRSectionSize As UInteger

    Public UnitNodeBuffer() As Byte

    Public MapPalett() As Color
    Public Bitdata() As Byte
    Public MapSize As Size
    Private MTXMDATA() As UShort

    Public Minimap(,) As Byte

    Private GRPdata(998) As GRP

    Private UnitSize(228) As Rectangle

    Public SelectUnits As New List(Of UInt16)
    Public UnitNode As New List(Of CUnit)
    Public Class CUnit
        Public dataNum As UInt16
        '일단 위치정보만.
        Public pos As Point
        Public UnitCode As Byte
        Public plyer As Byte
        Public rect As Rectangle

        Public Sub New(tdatanum As UInt16, tPos As Point, tUnitCode As Byte, tplyer As Byte, trect As Rectangle)
            pos = tPos
            plyer = tplyer
            UnitCode = tUnitCode
            dataNum = tdatanum
            rect = trect
        End Sub
    End Class

    Public SpriteList(20) As List(Of Cimage)
    Public LocaationTable As New List(Of Location)
    Public Structure Location
        Public Left As UInteger
        Public Up As UInteger
        Public Right As UInteger
        Public Down As UInteger

        Public Sub New(tleft As UInteger, tUp As UInteger, tRight As UInteger, tDown As UInteger)
            Left = tleft
            Up = tUp
            Right = tRight
            Down = tDown
        End Sub
    End Structure


    Public Sub CreateUnit(PosX As UInteger, PosY As UInteger, Unit As UInteger, Player As UInteger)
        Dim bytes(19) As Byte
        Dim memstream As New MemoryStream(bytes)
        Dim BinWriter As New BinaryWriter(memstream)

        BinWriter.Write(CUInt(1))

        BinWriter.Write(CUInt(PosX))
        BinWriter.Write(CUInt(PosY))
        BinWriter.Write(CUInt(Unit))
        BinWriter.Write(CUInt(Player))


        '#0x58DBF0 = 트리거 명령코드

        '#0x58DBF4 = 트리거 X
        '#0x58DBF8 = 트리거 Y
        '#0x58DBFC = 유닛 종류
        '#0x58DC00 = 플레이어

        BinWriter.Close()
        memstream.Close()

        WinAPI.WriteValue(&H58DBF0, bytes)
    End Sub


    Public Imagelist As New List(Of Cimage)
    Public Class Cimage
        Public Sub New(timagenum As UShort, tpos As Point, tframeindex As UShort, tpallet As Byte, tcolor As Byte, tflag As Boolean)
            imagenum = timagenum
            pos = tpos
            frameindex = tframeindex
            flipflag = tflag
            color = tcolor

            Remapping = DatEditDATA(DTYPE.images).ReadValue("Remapping", imagenum)
            DrawFunction = tpallet

            'RLE_NORMAL = 0
            'RLE_CLOAK = 5
            'RLE_CLOAKED = 6
            'RLE_DECLOAK = 7
            'RLE_EFFECT = 9
            'RLE_SHADOW = 10(official)
            'RLE_HPFLOATDRAW = 11(official)
            'RLE_WARP_IN = 12
            'RLE_OUTLINE = 13
            'RLE_PLAYER_SIDE = 14(official)
            'RLE_SHIFT = 16
            'RLE_FIRE = 17
        End Sub
        Public pos As Point

        Public imagenum As UShort

        Public frameindex As UShort

        Public color As Byte

        Public flipflag As Boolean

        Public Remapping As Byte
        Public DrawFunction As Byte
        '        /*0x1A*/ u16          frameIndex;
        '/*0x1C*/ BW:Position mapPosition;
        '/*0x20*/ BW:Position screenPosition;
        '/*0x24*/ rect         grpBounds;      // Bounds for GRP frame, only different from normal when part of graphic Is out of bounds.
        '/*0x2C*/ grpHead*     GRPFile;
    End Class


    Public Function CheckAuthority() As Boolean
        Dim MainOffset As UInteger = WinAPI.ReadValue(&H58DBEC, 4)
        Dim CheckString() As String = StringToBinary(ProjectSet.OutputMap, ",").Split(",")


        Dim bytelen As UInteger = CheckString.Length
        Dim bytes(bytelen - 1) As Byte

        For i = 0 To bytes.Length - 1
            bytes(i) = CByte(CheckString(i))
        Next


        Dim Mapbytes() As Byte = WinAPI.ReadValue(MainOffset, bytelen)

        For i = 0 To bytes.Length - 1
            If Mapbytes(i) <> bytes(i) Then
                Return True
            End If
        Next



        Return False
    End Function

    Public Sub LoadGRP(imageNUM As UShort)
        GRPdata(imageNUM) = New GRP

        If DatEditForm.GRPHock(imageNUM).ToString = "System.Byte[]" Then
            GRPdata(imageNUM).LoadGRP(CType(DatEditForm.GRPHock(imageNUM), Byte()))
        Else
            GRPdata(imageNUM).LoadGRP(CType(DatEditForm.GRPHock(imageNUM), String))
        End If
    End Sub

    Public Function CheckGRP(imageNUM As UShort) As Boolean
        If GRPdata(imageNUM) Is Nothing Then
            Return False
        Else
            Return True
        End If

    End Function


    Public Function ExportGRP(imagenum As UShort)
        Return GRPdata(imagenum)
    End Function

    Enum order
        die = 0
        attack_move = 1
    End Enum
    Public Sub OrderUnit(OrderType As order, pos As Point)
        Dim bytes(1) As Byte




        For i = 0 To UnitNode.Count - 1
            If UnitNode(i).UnitCode <> 4 And UnitNode(i).UnitCode <> 6 And UnitNode(i).UnitCode <> 18 And UnitNode(i).UnitCode <> 24 And UnitNode(i).UnitCode <> 26 Then
                If SelectUnits.Contains(UnitNode(i).dataNum) Then
                    Select Case OrderType
                        Case [order].die
                            WinAPI.Write(CUInt(&H59CCA8 + UnitNode(i).dataNum * 336 + &H4D), CByte(0))
                        Case [order].attack_move
                            Dim bytes2(7) As Byte

                            Dim memstream As New MemoryStream(bytes2)
                            Dim binaryWriter As New BinaryWriter(memstream)

                            binaryWriter.Write(CUShort(pos.X))
                            binaryWriter.Write(CUShort(pos.Y))
                            WinAPI.WriteValue(&H59CCA8 + UnitNode(i).dataNum * 336 + &H58, bytes2)

                            binaryWriter.Close()
                            memstream.Close()

                            ReDim bytes2(15)

                            memstream = New MemoryStream(bytes2)
                            binaryWriter = New BinaryWriter(memstream)

                            memstream.Position = 0

                            binaryWriter.Write(CUShort(pos.X))
                            binaryWriter.Write(CUShort(pos.Y))
                            binaryWriter.Write(CUInt(0))
                            binaryWriter.Write(CUShort(pos.X))
                            binaryWriter.Write(CUShort(pos.Y))
                            binaryWriter.Write(CUShort(pos.X))
                            binaryWriter.Write(CUShort(pos.Y))
                            WinAPI.WriteValue(&H59CCA8 + UnitNode(i).dataNum * 336 + &H10, bytes2)



                            '10
                            'WinAPI.Write(&H59CCA8 + UnitNode(i).dataNum * 336 + &H20, CByte(2))
                            'WinAPI.Write(&H59CCA8 + UnitNode(i).dataNum * 336 + &H97, CByte(25))

                            binaryWriter.Close()
                            memstream.Close()


                            bytes(0) = 14
                            bytes(1) = 1
                            WinAPI.WriteValue(&H59CCA8 + UnitNode(i).dataNum * 336 + &H4D, bytes)
                    End Select
                End If
            End If
        Next
    End Sub

    Public Sub ReadUnit()
        UnitNodeBuffer = WinAPI.ReadValue(&H59CCA8, 1700 * 336 - 1)


        UnitNode.Clear()

        Dim memstr As New MemoryStream(UnitNodeBuffer)
        Dim binreader As New BinaryReader(memstr)

        For i = 0 To 1699
            memstr.Position = i * 336 + &HC
            If binreader.ReadUInt32() <> 0 Then

                memstr.Position = i * 336 + &H28
                Dim pos As New Point(binreader.ReadUInt16(), binreader.ReadUInt16())

                memstr.Position = i * 336 + &H64
                Dim UnitID As UInt16 = binreader.ReadUInt16()

                memstr.Position = i * 336 + &H4C
                Dim player As Byte = binreader.ReadByte()

                memstr.Position = i * 336 + &H108



                Dim rect As Rectangle = UnitSize(UnitID)




                UnitNode.Add(New CUnit(i, pos, UnitID, player, rect))
            End If

        Next

        binreader.Close()
        memstr.Close()
    End Sub
    Public Sub ReadLocation()
        Dim Locationarray() As Byte = WinAPI.ReadValue(&H58DC60, 255 * 20 - 1)


        LocaationTable.Clear()

        Dim memstr As New MemoryStream(Locationarray)
        Dim binreader As New BinaryReader(memstr)

        For i = 0 To 254
            LocaationTable.Add(New Location(binreader.ReadUInt32(), binreader.ReadUInt32(), binreader.ReadUInt32(), binreader.ReadUInt32()))
            binreader.ReadUInt32()
        Next
        LocaationTable.RemoveAt(63)

        binreader.Close()
        memstr.Close()
    End Sub


    'Private Sub GetStringSize()
    '    Dim Buffer() As Byte
    '    Dim hmpq As UInteger
    '    Dim hfile As UInteger
    '    Dim filesize As UInteger
    '    Dim temptext As String = ""

    '    Dim pdwread As IntPtr

    '    StormLib.SFileOpenArchive(ProjectSet.OutputMap, 0, 0, hmpq)


    '    Dim openFilename As String = "staredit\scenario.chk"

    '    StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)

    '    If hfile <> 0 Then
    '        filesize = StormLib.SFileGetFileSize(hfile, filesize)
    '        ReDim Buffer(filesize)

    '        StormLib.SFileReadFile(hfile, Buffer, filesize, pdwread, 0)

    '        Dim mem As MemoryStream = New MemoryStream(Buffer)
    '        Dim binary As BinaryReader = New BinaryReader(mem)
    '        Dim stream As StreamReader = New StreamReader(mem, Text.Encoding.ASCII)
    '        temptext = stream.ReadToEnd


    '        mem.Position = InStr(temptext, "STR ") + 3

    '        STRSectionSize = binary.ReadUInt32 '문자열 수
    '        MsgBox(STRSectionSize)

    '        StormLib.SFileCloseFile(hfile)
    '    Else
    '        MsgBox("실패")
    '        Exit Sub
    '    End If

    '    StormLib.SFileCloseArchive(hmpq)
    'End Sub

    Public Sub ReadImage()
        'Frist Array Pointer 6C2318 'Next 만 읽어서 0이 나올때까지 읽는 다.

        Dim unlimiterSpritearray() As Byte
        Dim unlimiterimagearray() As Byte

        Dim unlimiterimagememstr As new MemoryStream
        Dim unlimiterimagebinreader As New BinaryReader(unlimiterimagememstr)

        Dim unlimiterSpritememstr As New MemoryStream
        Dim unlimiterSpritebinreader As New BinaryReader(unlimiterSpritememstr)

        Dim spriteoffset As Integer
        Dim imageoffset As Integer
        If unlimiter Then
            spriteoffset = WinAPI.ReadValue(&H63FE30, 4) - 65536 * 36
            imageoffset = WinAPI.ReadValue(&H57EB68, 4) - 65536 * 64

            unlimiterSpritearray = WinAPI.ReadValue(spriteoffset, 65536 * 36)
            unlimiterimagearray = WinAPI.ReadValue(imageoffset, 65536 * 64)


            unlimiterimagememstr = New MemoryStream(unlimiterimagearray)
            unlimiterimagebinreader = New BinaryReader(unlimiterimagememstr)


            unlimiterSpritememstr = New MemoryStream(unlimiterSpritearray)
            unlimiterSpritebinreader = New BinaryReader(unlimiterSpritememstr)

            'connectDList(0x64EED8, 0x64EEDC, 112, 8192)  # 총알 갯수 패치
            'connectDList(0x63FE30, 0x63FE34, 36, 65536)  # 스프라이트 갯수 패치
            'connectDList(0x57EB68, 0x57EB70, 64, 65536)  # 이미지 갯수 패치
        End If



        Imagelist.Clear()

        'Dim zerobuffer(4 * 2500 - 1) As Byte
        'WinAPI.WriteValue(&H6C2318, zerobuffer)
        Dim SortSpritearray() As Byte = WinAPI.ReadValue(&H6C2318, 4 * 2500)


        Dim SortSpritememstr As New MemoryStream(SortSpritearray)
        Dim SortSpritebinreader As New BinaryReader(SortSpritememstr)


        Dim Spritearray() As Byte = WinAPI.ReadValue(&H629D98, 2500 * 36)


        Dim Spritememstr As New MemoryStream(Spritearray)
        Dim Spritebinreader As New BinaryReader(Spritememstr)



        Dim imagememstr As MemoryStream
        Dim imagebinreader As BinaryReader
        Dim imagearray() As Byte = WinAPI.ReadValue(&H52F568, 6816 * 64)

        imagememstr = New MemoryStream(imagearray)
        imagebinreader = New BinaryReader(imagememstr)
        '&H57D6E8

        'Dim pnext As UInteger

        For i = 0 To SpriteList.Count - 1
            SpriteList(i).Clear()
        Next



        For i = 0 To 2499
            Dim spriteptr As UInteger = &H629D98 + 36 * i

            If unlimiter Then
                    If spriteptr > &H7FFFFF Then
                        unlimiterSpritememstr.Position = spriteptr - spriteoffset

                        AddSpriteList(unlimiterSpritebinreader.ReadBytes(36), imagememstr, imagebinreader, unlimiterimagememstr, unlimiterimagebinreader, imageoffset)
                    Else
                        Spritememstr.Position = spriteptr - &H629D98
                        AddSpriteList(Spritebinreader.ReadBytes(36), imagememstr, imagebinreader, unlimiterimagememstr, unlimiterimagebinreader, imageoffset)
                    End If
                Else
                    Spritememstr.Position = spriteptr - &H629D98
                    AddSpriteList(Spritebinreader.ReadBytes(36), imagememstr, imagebinreader, unlimiterimagememstr, unlimiterimagebinreader, imageoffset)
                End If

        Next

        'MsgBox(spriteNext)



        If unlimiter Then
            unlimiterSpritebinreader.Close()
            unlimiterSpritememstr.Close()



            unlimiterimagebinreader.Close()
            unlimiterimagememstr.Close()
        End If

        SortSpritebinreader.Close()
        SortSpritememstr.Close()


        Spritebinreader.Close()
        Spritememstr.Close()



        imagebinreader.Close()
        imagememstr.Close()

    End Sub

    Private Sub AddSpriteList(ByVal buffer() As Byte, ByVal imagememstr As MemoryStream, ByVal imagebinreader As BinaryReader, ByVal unlimitimagememstr As MemoryStream, ByVal unlimitimagebinreader As BinaryReader, imageoffset As UInteger)
        Dim Spritememstr As New MemoryStream(buffer)
        Dim Spritebinreader As New BinaryReader(Spritememstr)

        Dim imageptr As UInteger = 0

        'SpriteList
        '/*0x0D*/ u8         elevationLevel;


        Spritememstr.Position = &HD
        Dim elevationLevel As Byte = Spritebinreader.ReadByte()

        Spritememstr.Position = &H14
        Dim pos As Point = New Point(Spritebinreader.ReadUInt16, Spritebinreader.ReadUInt16)
        '

        Spritememstr.Position = &H8
        Dim spriteID As UInt16 = Spritebinreader.ReadUInt16()
        If spriteID <> 321 And spriteID <> 318 Then
            Dim Color As Byte = Spritebinreader.ReadByte()

            Spritememstr.Position = &H18
            Dim pImagePrimary As UInt32 = Spritebinreader.ReadUInt32()
            Dim pImageHead As UInt32 = Spritebinreader.ReadUInt32()
            Dim pImageTail As UInt32 = Spritebinreader.ReadUInt32()

            If pImageHead <> 0 Then
                Dim imagenodeNext As UInt32 = pImageHead
                Try
                    While (imagenodeNext <> 0)
                        If (DebugForm.scoll.X - 256 < pos.X) And (pos.X < DebugForm.scoll.X + DebugForm.bmpsize.Width + 256) And
                                     (DebugForm.scoll.Y - 256 < pos.Y) And (pos.Y < DebugForm.scoll.Y + DebugForm.bmpsize.Height + 256) Then

                            If imagenodeNext > &H7FFFFF Then
                                unlimitimagememstr.Position = imagenodeNext - imageoffset '&H52F568
                                AddImageList(unlimitimagebinreader.ReadBytes(64), pos, Color, elevationLevel)
                            Else
                                imagememstr.Position = imagenodeNext - &H52F568
                                AddImageList(imagebinreader.ReadBytes(64), pos, Color, elevationLevel)
                            End If
                        End If

                        If imagenodeNext > &H7FFFFF Then
                            unlimitimagememstr.Position = imagenodeNext + 4 - imageoffset
                            imagenodeNext = unlimitimagebinreader.ReadUInt32()
                        Else
                            imagememstr.Position = imagenodeNext + 4 - &H52F568
                            imagenodeNext = imagebinreader.ReadUInt32()
                        End If
                    End While
                Catch ex As Exception
                    'imagememstr.Position = pImagePrimary - imageoffset
                    'AddImageList(imagebinreader.ReadBytes(64), pos, Color, elevationLevel)
                End Try
            End If

        End If

        Spritebinreader.Close()
        Spritememstr.Close()
    End Sub


    Private Sub AddImageList(ByVal bytes() As Byte, pos As Point, Color As Byte, elevationLevel As Byte)
        Dim imagememstr As New MemoryStream(bytes)
        Dim imagebinreader As New BinaryReader(imagememstr)


        imagememstr.Position = &H8
        Dim imagenum As UInt16 = imagebinreader.ReadUInt16
        If imagenum <> 560 Then
            imagememstr.Position = &HA
            Dim paletteType As Byte = imagebinreader.ReadByte


            imagememstr.Position = &HC
            Dim flag As UInt16 = imagebinreader.ReadUInt16

            imagememstr.Position = &H3C
            Dim ParrentSprite As UInt32 = imagebinreader.ReadUInt32

            imagememstr.Position = &HE
            Dim temppos As Point = New Point(imagebinreader.ReadSByte, imagebinreader.ReadSByte)
            pos = pos + temppos

            imagememstr.Position = &H1A
            Dim frameindex As UShort = imagebinreader.ReadUInt16

            imagememstr.Position = &H2C
            Dim GRPHeader As UInt32 = imagebinreader.ReadUInt32

            Dim flipflag As Boolean = flag And &H2



            If ParrentSprite <> 0 And ((flag And &H40) = 0) And GRPHeader <> 0 Then
                SpriteList(elevationLevel).Add(New Cimage(imagenum, pos, frameindex, paletteType, Color, flipflag))
            End If
        End If




        imagebinreader.Close()
        imagememstr.Close()
    End Sub


    Public Sub Palletrefresh()
        Dim memstr As MemoryStream
        Dim binreader As BinaryReader

        ReDim MapPalett(255)
        Dim mempallet() As Byte = WinAPI.ReadValue(&H1505E670, 1024)
        memstr = New MemoryStream(mempallet)
        binreader = New BinaryReader(memstr)

        For i = 0 To MapPalett.Count - 1
            Dim r, g, b As Byte
            r = binreader.ReadByte()
            g = binreader.ReadByte()
            b = binreader.ReadByte()
            binreader.ReadByte()


            MapPalett(i) = Color.FromArgb(r, g, b)
        Next

        binreader.Close()
        memstr.Close()
    End Sub


    Public Sub lit()
        Dim memstr As MemoryStream
        Dim binreader As BinaryReader




        Dim buffer() As Byte = WinAPI.ReadValue(&H6617C8, 8 * 228, True)
        memstr = New MemoryStream(buffer)
        binreader = New BinaryReader(memstr)

        For i = 0 To 227
            Dim L, U, R, D As UInt16
            L = binreader.ReadUInt16
            U = binreader.ReadUInt16
            R = binreader.ReadUInt16
            D = binreader.ReadUInt16
            'If i > 39 Then
            '    MsgBox("시작 " & i)
            '    MsgBox(L & "," & U & "," & R & "," & D)

            '    MsgBox(-L & "," & -U & "," & L + R & "," & U + D)
            'End If


            UnitSize(i) = New Rectangle(-L, -U, L + R, U + D)

            'If i > 39 Then
            '    MsgBox("끝 " & i)
            'End If
        Next



        binreader.Close()
        memstr.Close()


        '맵 크기를 읽습니다.
        MapSize = New Size(WinAPI.ReadValue(&H57F1D4, 2), WinAPI.ReadValue(&H57F1D6, 2))



        '팔레트를 불러옵니다.
        Palletrefresh()



        '그래픽 파일들을 미리 그립니다.(MTXM, cv5,vx4,vr4 등.)
        ReDim Bitdata(MapSize.Width * MapSize.Height * 1024 - 1)

        'MTXM읽기
        Dim mtxm() As Byte = WinAPI.ReadValue(WinAPI.ReadValue(&H5993C4, 4), MapSize.Width * MapSize.Height * 2)
        memstr = New MemoryStream(mtxm)
        binreader = New BinaryReader(memstr)


        ReDim MTXMDATA(MapSize.Width * MapSize.Height - 1)


        For i = 0 To MTXMDATA.Count - 1
            Dim v As UInt16 = binreader.ReadUInt16()


            Dim Group As UInt16 = (v And &HFFF0) \ 16
            Dim index As Byte = v And &HF
            'If 24000 < (Group * 16 + index) Then
            '    MsgBox(v & "," & Group * 16 + index)
            'End If
            MTXMDATA(i) = Group * 16 + index
        Next
        binreader.Close()
        memstr.Close()


        Dim mpq As New SFMpq
        Dim TileSetType As Byte = WinAPI.ReadValue(&H57F1DC, 2)

        Dim pcxstr() As String = {"ofire", "gfire", "bfire", "bexpl", "trans50", "dark", "shift"}
        'RemappingPallet(0) = New CPCX
        'RemappingPallet(0).LoadPCX(mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & "\" & pcxstr(0) & ".pcx"))


        For i = 0 To 6
            RemappingPallet(i) = New CPCX
            RemappingPallet(i).LoadPCX(mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & "\" & pcxstr(i) & ".pcx"))
        Next


        '    RemappingPallet

        '    ofire = 0
        '    gfire = 1
        '    bfire = 2
        '    bexpl = 3
        '    trans50 = 4

        '    dark = 5
        '    shift = 6








        Dim cv5size As UInt32 = mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & ".cv5").Length
        Dim vx4size As UInt32 = mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & ".vx4").Length
        Dim vr4size As UInt32 = mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & ".vr4").Length



        Dim cv5() As Byte = WinAPI.ReadValue(WinAPI.ReadValue(&H6D5EC8, 4), cv5size)
        Dim cv5mem As MemoryStream = New MemoryStream(cv5)
        Dim cv5binary As BinaryReader = New BinaryReader(cv5mem)
        Dim cv5binaryw As BinaryWriter = New BinaryWriter(cv5mem)


        'ReDim TilebitDATA(63)
        Dim vx4() As Byte = WinAPI.ReadValue(WinAPI.ReadValue(&H628458, 4), vx4size)
        Dim vr4() As Byte = WinAPI.ReadValue(WinAPI.ReadValue(&H628444, 4), vr4size)


        Dim vx4mem As MemoryStream = New MemoryStream(vx4)
        Dim vx4binary As BinaryReader = New BinaryReader(vx4mem)




        Dim vr4mem As MemoryStream = New MemoryStream(vr4)
        Dim vr4binary As BinaryReader = New BinaryReader(vr4mem)



        Dim TilebitDATA()() As Byte

        ReDim TilebitDATA((cv5mem.Length \ 52) * 16)
        For Groupnum = 0 To cv5mem.Length \ 52 - 1

            cv5mem.Position = &H14 + 52 * Groupnum
            For megnum = 0 To 15
                Dim megeindex As UInt16 = cv5binary.ReadUInt16()

                ReDim TilebitDATA(megnum + Groupnum * 16)(1023)

                Dim tilebitStream As New MemoryStream(TilebitDATA(megnum + Groupnum * 16))
                Dim tilebitbinw As New BinaryWriter(tilebitStream)



                vx4mem.Position = 32 * megeindex
                For i = 0 To 15
                    '1 Walk
                    '2 mid
                    '4 High
                    '((flag And 7) = 4) Then 'And cv5Flag = True Then

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


                Next
                tilebitbinw.Close()
                tilebitStream.Close()
            Next
        Next

        Dim ErrorCount As UInteger
        ReDim Minimap(MapSize.Width, MapSize.Height)
        For mapy = 0 To MapSize.Height - 1
            'MsgBox(mapy)
            For mapx = 0 To MapSize.Width - 1 'MTXMDATA.Length - 1
                Try
                    Minimap(mapx, mapy) = TilebitDATA(MTXMDATA(mapx + mapy * MapSize.Width))(1)


                    For y = 0 To 31
                        For x = 0 To 31
                            Bitdata(x + mapx * 32 + ((y + mapy * 32) * MapSize.Width * 32)) = TilebitDATA(MTXMDATA(mapx + mapy * MapSize.Width))(x + (y * 32))

                        Next
                    Next
                Catch ex As Exception
                    ErrorCount += 1
                End Try
            Next
        Next

        If ErrorCount <> 0 Then
            MsgBox(ErrorCount & "개의 잘못된 지형배치가 발견 되었습니다.", MsgBoxStyle.Exclamation, ProgramSet.ErrorFormMessage)
        End If







        vr4binary.Close()
        vr4mem.Close()

        cv5binaryw.Close()
        cv5binary.Close()
        cv5mem.Close()

        vx4binary.Close()
        vx4mem.Close()


        '5993C4 MTXM
        '57F1DC 타일 셋

        '51CED0 GRP포인터
        '52F568 이미지 포인터 4

        '57F1D4 맵 사이즈 4

        '5993D0 vf4 4
        '6D5EC8 cv5 4
        '628458 vx4 4
        '628444 vr4 4
        '5994E0 wpe 1024





        '59CCA8	1.16.1	Win	Unitnode Table	336	1700

        '58DC60	1.16.1	Win	Location Table  20	255
        '58DC40	1.16.1	Win	Switch Table    32	255
        '58D6F4	1.16.1	Win	Countdown Timer 4
        '58D718	1.16.1	Win	#'s of Game Pauses 1 8

        '58A364	1.16.1	Win	Death Table Start  4	2736

        '매 프레임마다 모든 유닛 정보를 읽고, 이미지 정보를 읽고 출력합니다.



    End Sub

    Public Sub Close()

    End Sub
End Class
