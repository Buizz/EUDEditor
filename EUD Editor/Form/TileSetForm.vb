Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text


'원본 MTXM과 가짜 MTXM이 있다.
'가짜 MTXM은 게임 시작 후 덮어 쒸어지는 거로 외형만 결정한다.

'타일은 통행 설정용 타일을 가진다.
'ex TIleNUM 123의 타일은 실제 이동 정보를 0타일에서 가져옴.
'   에디터에서는 123타일로 보이고 맵에 삽입시 123타일을 0으로 바꾼 후 MTXM에 넣어 새 맵을 만들어 적용 한 다음
'   게임 시작시 MTXM수정으로 123타일로 교체한다.



Public Class TileSetForm
    Private workflow As New Cworkflow
    '0번 인덱스에 가장 최근에 작업한 것이 있음.
    '뒤로 할 때 마다 뒷 인덱스를 불러옴.
    '앞으로 하면 다시 그 앞 인덱스를 차례대로 불러옴.

    '갱신되면 현재 인덱스 앞에 있는 정보가 모두 날라감.
    Private Class Cworkflow
        Public CurrentIndex As Integer = 0
        Public Sub refreshButton()
            If works.Count = 0 Then
                TileSetForm.ToolStripButton2.Enabled = False '취소
                TileSetForm.ToolStripButton3.Enabled = False '다시
            Else
                If CurrentIndex = 0 Then '앞으로 갈 수 없다는 걸 의미함.
                    TileSetForm.ToolStripButton2.Enabled = True
                    TileSetForm.ToolStripButton3.Enabled = False
                Else
                    If CurrentIndex < works.Count Then ' 둘다 가능함.
                        TileSetForm.ToolStripButton2.Enabled = True
                        TileSetForm.ToolStripButton3.Enabled = True
                    Else '다시 갈 수 없는 상황을 의미
                        TileSetForm.ToolStripButton2.Enabled = False
                        TileSetForm.ToolStripButton3.Enabled = True
                    End If
                End If
                End If

        End Sub
        Public Sub AddList()
            works.Insert(0, New Cworkflow.Cworks)

            refreshButton()
        End Sub
        Public Sub Cancle()
            LoadWorkDataC()
            CurrentIndex += 1
            refreshButton()
        End Sub
        Public Sub Retry()
            CurrentIndex -= 1
            LoadWorkDataR()
            refreshButton()
        End Sub
        Public works As New List(Of Cworks)
        Public Class Cworks
            '브러시 모드 T일 경우 타일, F일 경우 통행
            Public isBrushMode As Boolean


            '작업된 타일 들의 이전 값

            Public Sub AddAction(tindex As Integer, aftervalue As UInt16)
                Dim isexist As Boolean = False
                For i = 0 To CancleTiles.Count - 1
                    If CancleTiles(i).index = tindex Then
                        isexist = True
                        Exit For
                    End If
                Next
                If isexist = False Then
                    If isBrushMode Then 'ToMTXM
                        CancleTiles.Add(New Cworkflow.Cworks.Tileusedata(tindex, MTXMDATA(tindex)))
                    Else
                        CancleTiles.Add(New Cworkflow.Cworks.Tileusedata(tindex, ProjectMTXMDATA(tindex)))
                    End If
                    RetryTiles.Add(New Cworkflow.Cworks.Tileusedata(tindex, aftervalue))
                End If

            End Sub
            Public CancleTiles As New List(Of Tileusedata)
            Public RetryTiles As New List(Of Tileusedata)
            Public Structure Tileusedata
                Public index As Integer
                Public brush As UInt16
                Public Sub New(tindex As Integer, tbrush As UInt16)
                    index = tindex
                    brush = tbrush
                End Sub
            End Structure
        End Class


        Public Sub LoadWorkDataC()
            If works(CurrentIndex).isBrushMode Then
                For i = 0 To works(CurrentIndex).CancleTiles.Count - 1
                    MTXMDATA(works(CurrentIndex).CancleTiles(i).index) = works(CurrentIndex).CancleTiles(i).brush
                Next
            Else
                For i = 0 To works(CurrentIndex).CancleTiles.Count - 1
                    ProjectMTXMDATA(works(CurrentIndex).CancleTiles(i).index) = works(CurrentIndex).CancleTiles(i).brush
                Next
            End If




            'CurrentIndex
            'MsgBox("작업 갯수 : " & works.Count & vbCrLf & "현재 작업 페이지 : " & CurrentIndex)
            'MsgBox("작업타입 : " & works(CurrentIndex).isBrushMode & vbCrLf & "작업량 : " & works(CurrentIndex).CancleTiles.Count)
        End Sub
        Public Sub LoadWorkDataR()
            If works(CurrentIndex).isBrushMode Then
                For i = 0 To works(CurrentIndex).RetryTiles.Count - 1
                    MTXMDATA(works(CurrentIndex).RetryTiles(i).index) = works(CurrentIndex).RetryTiles(i).brush
                Next
            Else
                For i = 0 To works(CurrentIndex).RetryTiles.Count - 1
                    ProjectMTXMDATA(works(CurrentIndex).RetryTiles(i).index) = works(CurrentIndex).RetryTiles(i).brush
                Next
            End If




            'CurrentIndex
            'MsgBox("작업 갯수 : " & works.Count & vbCrLf & "현재 작업 페이지 : " & CurrentIndex)
            'MsgBox("작업타입 : " & works(CurrentIndex).isBrushMode & vbCrLf & "작업량 : " & works(CurrentIndex).CancleTiles.Count)
        End Sub

        Public Sub ClearWorkData()
            For i As Integer = 1 To CurrentIndex
                works.RemoveAt(1)
            Next
            CurrentIndex = 0
            refreshButton()
        End Sub
        '작업 종류
        '작업한것


    End Class


    Private SaveStatus As Boolean = True

    Private scoll As New Point
    Private bmp As Bitmap
    Private bmpsize As Size
    Private pixels() As Byte


    Private SelectTIle As Integer
    Private SelectTIle1 As Integer
    Private SelectTIle2 As Integer

    Private TileBrush(,) As UInteger
    Private TileBrushSize As Size

    Private SelectPal1 As Integer
    Private SelectPal2 As Integer



    Private Function GetintValue(b1 As Byte, b2 As Byte, b3 As Byte, b4 As Byte)
        Dim tempv As UInteger = CUInt(b1) * 16777216 + b2 * 65536 + b3 * 256 + b4

        If tempv > 2147483647 Then
            Return tempv - 4294967295
        Else
            Return tempv
        End If
    End Function

    Dim Grid(31) As Byte
    Private Sub DrawRect(x As Integer, y As Integer, TileNum As Integer)
        'MTXMDATA(i + scoll.X \ 32 + (j + scoll.Y \ 32) * MapSize.Width)


        Dim isgrid As Boolean = True
        Dim isSelect As Boolean = False


        Dim MaxX As UInteger = Math.Max((SelectPal1 Mod 8), (SelectPal2 Mod 8))
        Dim MaxY As UInteger = Math.Max((SelectPal1 \ 8), (SelectPal2 \ 8))

        Dim MinX As UInteger = Math.Min((SelectPal1 Mod 8), (SelectPal2 Mod 8))
        Dim MinY As UInteger = Math.Min((SelectPal1 \ 8), (SelectPal2 \ 8))

        Dim TWidth As UInteger = MaxX - MinX
        Dim THeight As UInteger = MaxY - MinY

        Dim SelectTilePos As New Point(SelectTIle Mod MapSize.Width, SelectTIle \ MapSize.Width)
        Dim TileNumPos As New Point(TileNum Mod MapSize.Width, TileNum \ MapSize.Width)

        Dim PalTileNum As Integer
        If BrushMode = BMode.CopyPaste Or CtrlHokey Then
            If ismapDarwC Or CtrlHokey Then
                Dim MaxX2 As Integer = Math.Max((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MaxY2 As Integer = Math.Max((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                Dim MinX2 As Integer = Math.Min((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MinY2 As Integer = Math.Min((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                If (MinX2 <= TileNumPos.X) And (TileNumPos.X <= MaxX2) And
                    (MinY2 <= TileNumPos.Y) And (TileNumPos.Y <= MaxY2) Then
                    isSelect = True
                    PalTileNum = MTXMDATA(TileNum)

                    'TileBrush(MaxX2 - TileNumPos.X, MaxY2 - TileNumPos.Y) = MTXMDATA(TileNum)
                    'PalTileNum = MinX - SelectTilePos.X + TileNumPos.X + TWidth \ 2 + (MinY - SelectTilePos.Y + TileNumPos.Y + THeight \ 2) * 8
                End If
            ElseIf ismapDarwS Or ShiftHokey Then
                Dim MaxX2 As Integer = Math.Max((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MaxY2 As Integer = Math.Max((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                Dim MinX2 As Integer = Math.Min((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MinY2 As Integer = Math.Min((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                If (MinX2 <= TileNumPos.X) And (TileNumPos.X <= MaxX2) And
                (MinY2 <= TileNumPos.Y) And (TileNumPos.Y <= MaxY2) Then
                    isSelect = True
                    PalTileNum = TileBrush((Math.Abs(TileNumPos.X - MinX2)) Mod (TileBrushSize.Width + 1), ((Math.Abs(TileNumPos.Y - MinY2)) Mod (TileBrushSize.Height + 1)))

                    'TileBrush(MaxX2 - TileNumPos.X, MaxY2 - TileNumPos.Y) = MTXMDATA(TileNum)
                    'PalTileNum = MinX - SelectTilePos.X + TileNumPos.X + TWidth \ 2 + (MinY - SelectTilePos.Y + TileNumPos.Y + THeight \ 2) * 8
                End If
            Else
                If (((SelectTilePos.X - TileBrushSize.Width \ 2) <= TileNumPos.X) And (TileNumPos.X <= (SelectTilePos.X + TileBrushSize.Width \ 2 + TileBrushSize.Width Mod 2))) And
              (((SelectTilePos.Y - TileBrushSize.Height \ 2) <= TileNumPos.Y) And (TileNumPos.Y <= (SelectTilePos.Y + TileBrushSize.Height \ 2 + TileBrushSize.Height Mod 2))) Then
                    isSelect = True
                    PalTileNum = TileBrush(TileNumPos.X - (SelectTilePos.X - TileBrushSize.Width \ 2), TileNumPos.Y - (SelectTilePos.Y - TileBrushSize.Height \ 2))

                End If
            End If
        ElseIf BrushMode = BMode.Pallet Then
            If ismapDarwS Or ShiftHokey Then

                Dim MaxX2 As Integer = Math.Max((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MaxY2 As Integer = Math.Max((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                Dim MinX2 As Integer = Math.Min((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MinY2 As Integer = Math.Min((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                If (MinX2 <= TileNumPos.X) And (TileNumPos.X <= MaxX2) And
                    (MinY2 <= TileNumPos.Y) And (TileNumPos.Y <= MaxY2) Then
                    isSelect = True
                    PalTileNum = MinX + (Math.Abs(TileNumPos.X - MinX2)) Mod (TWidth + 1) + (MinY + (Math.Abs(TileNumPos.Y - MinY2)) Mod (THeight + 1)) * 8

                    'TileBrush(MaxX2 - TileNumPos.X, MaxY2 - TileNumPos.Y) = MTXMDATA(TileNum)
                    'PalTileNum = MinX - SelectTilePos.X + TileNumPos.X + TWidth \ 2 + (MinY - SelectTilePos.Y + TileNumPos.Y + THeight \ 2) * 8
                End If
            Else
                If (((SelectTilePos.X - TWidth \ 2) <= TileNumPos.X) And (TileNumPos.X <= (SelectTilePos.X + TWidth \ 2 + TWidth Mod 2))) And
                    (((SelectTilePos.Y - THeight \ 2) <= TileNumPos.Y) And (TileNumPos.Y <= (SelectTilePos.Y + THeight \ 2 + THeight Mod 2))) Then
                    isSelect = True
                    PalTileNum = MinX - SelectTilePos.X + TileNumPos.X + TWidth \ 2 + (MinY - SelectTilePos.Y + TileNumPos.Y + THeight \ 2) * 8
                End If
            End If
        Else
            If ShiftHokey Then
                Dim MaxX2 As Integer = Math.Max((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MaxY2 As Integer = Math.Max((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                Dim MinX2 As Integer = Math.Min((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MinY2 As Integer = Math.Min((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                If (MinX2 <= TileNumPos.X) And (TileNumPos.X <= MaxX2) And
                (MinY2 <= TileNumPos.Y) And (TileNumPos.Y <= MaxY2) Then
                    isSelect = True
                    PalTileNum = MTXMDATA(TileNum)
                    'TileBrush(MaxX2 - TileNumPos.X, MaxY2 - TileNumPos.Y) = MTXMDATA(TileNum)
                    'PalTileNum = MinX - SelectTilePos.X + TileNumPos.X + TWidth \ 2 + (MinY - SelectTilePos.Y + TileNumPos.Y + THeight \ 2) * 8
                End If
            Else
                If SelectTIle = TileNum Then

                    isSelect = True
                    PalTileNum = MTXMDATA(TileNum)
                End If
            End If

        End If
        'MinX + SelectTilePos.X -TileNumPos.X + TWidth \ 2 상대 좌표
        'MinY + SelectTilePos.Y -TileNumPos.Y + THeight \ 2 상대 좌표


        'y = (y \ 4) * 4









        Dim memstream As MemoryStream
        If isSelect = True Then
            memstream = New MemoryStream(TilebitDATA(PalTileNum))
            'If ismapDarwL = True Then
            'MTXMDATA(TileNum) = PalTileNum
            'End If
        Else
            memstream = New MemoryStream(TilebitDATA(MTXMDATA(TileNum)))
        End If

        Dim binaryreader As New BinaryReader(memstream)

        Dim pixelsstream As New MemoryStream(pixels)
        Dim binarywriter As New BinaryWriter(pixelsstream)


        For i = 0 To 31
            Dim pos As Integer = (y + i) * bmpsize.Width + x
            If pos >= 0 Then
                memstream.Position = i * 32
                pixelsstream.Position = pos

                Dim tlen As Integer

                If (x + 32 < bmpsize.Width) Then
                    tlen = 32
                Else
                    tlen = bmpsize.Width - x
                End If

                If x < 0 Then
                    tlen = 32 + x
                    pixelsstream.Position -= x
                    memstream.Position -= x
                End If


                If isSelect = True Then
                    If i = 0 Or i = 1 Or i = 30 Or i = 31 Then
                        For j = 0 To (tlen \ 4) - 1
                            binarywriter.Write(&H6F6F6F6F)
                        Next
                    Else
                        binarywriter.Write(CByte(&H6F))
                        binarywriter.Write(CByte(&H6F))
                        memstream.Position += 2
                        binarywriter.Write(binaryreader.ReadBytes(tlen - 4))
                        binarywriter.Write(CByte(&H6F))
                        binarywriter.Write(CByte(&H6F))
                    End If
                Else
                    If isgrid = False Or i <> 0 Then

                        If isgrid = True Then
                            pixelsstream.Position += 1
                            'binarywriter.Write(CByte(0))
                            memstream.Position += 1
                            binarywriter.Write(binaryreader.ReadBytes(tlen - 1))
                        Else

                            binarywriter.Write(binaryreader.ReadBytes(tlen))
                        End If
                    End If
                End If



            End If

        Next
        Dim movingFlag As Byte = ProjectMTXMDATA(TileNum)
        If movingFlag <> 0 Then



            Dim colorb As Byte
            Select Case (movingFlag - 1) \ 3
                Case 0
                    colorb = 255
                Case 1
                    colorb = 117
                Case 2
                    colorb = 111
            End Select

            Dim tlen As Integer

            If (x + 32 < bmpsize.Width) Then
                tlen = 32
            Else
                tlen = bmpsize.Width - x - 1
            End If

            If x < 0 Then
                tlen = 32 + x
                x = 0
            End If

            Dim tab(tlen - 1) As Byte
            For p = 0 To tab.Length - 1
                tab(p) = colorb
            Next

            For yf = 0 To 31
                If 0 <= (y + yf) And (y + yf) < bmpsize.Height Then
                    Dim pos As Integer = (y + yf) * bmpsize.Width + x
                    If pos >= 0 And pos < pixelsstream.Length Then
                        pixelsstream.Position = pos

                        If yf = 0 Or yf = 31 Then
                            binarywriter.Write(tab)
                        Else
                            binarywriter.Write(colorb)
                            pixelsstream.Position += tlen - 2

                            binarywriter.Write(colorb)
                        End If
                    End If

                End If

            Next


            For in2 = 0 To ((movingFlag - 1) Mod 3)
                Dim x2, y2 As UInteger
                For ind = 0 To 10
                    x2 = 10 + in2 * 3
                    y2 = 10 + ind

                    'If 0 <= (y2 - transY + j * 32) And (y2 - transY + j * 32) < palbmp.Height Then
                    If 0 <= y + y2 And y + y2 < bmpsize.Height And 0 <= x + x2 And x + x2 < bmpsize.Width Then
                        pixelsstream.Position = (y + y2) * bmpsize.Width + x + x2
                        binarywriter.Write(CUShort(colorb + colorb * 256))
                    End If

                    'End If
                Next
            Next
        End If

        'For i = 0 To 31
        '    For j = 0 To 31 Step 4
        '        If (0 <= x + j) And (x + j < bmp.Width) And (0 <= y + i) And (y + i < bmp.Height) Then
        '            pixels(GetOffset(x + j, y + i)) = binaryreader.ReadInt32
        '            If isgrid = True Then
        '                If i = 0 Then
        '                    pixels(GetOffset(x + j, y + i)) = pixels(GetOffset(x + j, y + i)) And &H0
        '                End If
        '                If j = 0 Then
        '                    pixels(GetOffset(x + j, y + i)) = pixels(GetOffset(x + j, y + i)) And &HFFFFFF00
        '                End If
        '            End If
        '        Else
        '            binaryreader.ReadInt32()
        '        End If
        '    Next
        'Next


        binarywriter.Close()
        pixelsstream.Close()


        binaryreader.Close()
        memstream.Close()

    End Sub

    Private Sub DrawMiniMapRect()
        Dim bmp As New Bitmap(128, 128)
        Dim grp As Graphics = Graphics.FromImage(bmp)


        Dim x As Single = scoll.X / 32
        Dim y As Single = scoll.Y / 32
        Dim width As Integer = Map.Size.Width / 32
        Dim height As Integer = Map.Size.Height / 32



        If MapSize.Width <> MapSize.Height Then
            If MapSize.Width > MapSize.Height Then
                y += (MapSize.Width - MapSize.Height) / 2
            Else
                x += (MapSize.Height - MapSize.Width) / 2
            End If
        End If


        '줌 정도와 미니맵 위치.
        Dim maxsize As Integer = Math.Max(MapSize.Width, MapSize.Height)
        Dim zoomdegree As Integer
        Select Case maxsize
            Case 64
                zoomdegree = 12
                ' *2
            Case 96
                zoomdegree = 8
                ' * 1.5
            Case 128
                zoomdegree = 6
                '줌 없음
            Case 192
                zoomdegree = 4
                '/ 1.5
            Case 256
                zoomdegree = 3
                '/ 2
        End Select

        x = (x * zoomdegree) / 6
        y = (y * zoomdegree) / 6
        width = (width * zoomdegree) / 6
        height = (height * zoomdegree) / 6


        grp.DrawRectangle(Pens.White, x, y, width, height)

        MiniMap.Image = bmp
    End Sub

    Private Sub MiniMap_MouseMove(sender As Object, e As MouseEventArgs) Handles MiniMap.MouseMove, MiniMap.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim x As Single = e.X
            Dim y As Single = e.Y
            Dim width As Integer = Map.Size.Width / 32
            Dim height As Integer = Map.Size.Height / 32

            If MapSize.Width <> MapSize.Height Then
                If MapSize.Width > MapSize.Height Then
                    y -= (MapSize.Width - MapSize.Height) / 4
                Else
                    x -= (MapSize.Height - MapSize.Width) / 4
                End If
            End If

            Dim maxsize As Integer = Math.Max(MapSize.Width, MapSize.Height)
            Dim zoomdegree As Integer
            Select Case maxsize
                Case 64
                    zoomdegree = 12
                ' /2
                Case 96
                    zoomdegree = 8
                ' / 1.5
                Case 128
                    zoomdegree = 6
                '줌 없음
                Case 192
                    zoomdegree = 4
                '* 1.5
                Case 256
                    zoomdegree = 3
                    '* 2
            End Select

            x = ((x / zoomdegree) * 6) * 32
            y = ((y / zoomdegree) * 6) * 32
            width = ((width / zoomdegree) * 6) * 32
            height = ((height / zoomdegree) * 6) * 32

            ScrollMap(x - width / 2, y - height / 2)
        End If
    End Sub

    Private palbmp As Bitmap
    Private Sub GetMinimapImage()
        Dim bmp As New Bitmap(MapSize.Width, MapSize.Height, PixelFormat.Format8bppIndexed)
        Dim CPalette As Imaging.ColorPalette
        CPalette = bmp.Palette
        For i = 0 To 255
            CPalette.Entries(i) = MapPalett(i)
        Next


        bmp.Palette = CPalette


        Dim bmd As New BitmapData
        bmd = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, Imaging.PixelFormat.Format8bppIndexed)



        Dim scan0 As IntPtr = bmd.Scan0
        Dim stride As Integer = bmd.Stride




        Dim pixels(bmp.Width * bmp.Height - 1) As Byte



        For j = 0 To MapSize.Height - 1
            For i = 0 To MapSize.Width - 1
                pixels(i + j * MapSize.Width) = TilebitDATA(MTXMDATA(i + (j * MapSize.Width)))(1)
            Next
        Next




        Marshal.Copy(pixels, 0, scan0, pixels.Length)

        bmp.UnlockBits(bmd)






        MiniMap.BackgroundImage = bmp
    End Sub


    Private Sub GetPalletImage()


        Dim bmd As New BitmapData
        bmd = palbmp.LockBits(New Rectangle(0, 0, palbmp.Width, palbmp.Height), ImageLockMode.ReadWrite, Imaging.PixelFormat.Format8bppIndexed)



        Dim scan0 As IntPtr = bmd.Scan0
        Dim stride As Integer = bmd.Stride


        'VScrollBar2

        Dim pixels(palbmp.Width * palbmp.Height - 1) As Byte




        Dim pixelsstream As New MemoryStream(pixels)
        Dim binarywriter As New BinaryWriter(pixelsstream)



        Dim tilenum As UInteger = 0

        Dim transY As Integer = VScrollBar2.Value Mod 32
        For j = 0 To palbmp.Height \ 32  '(TilebitDATA.Count \ 8) - 1
            For i = 0 To 7
                Dim memstream As New MemoryStream(TilebitDATA(tilenum + 8 * (VScrollBar2.Value \ 32)))
                Dim binaryreader As New BinaryReader(memstream)

                For y = 0 To 31
                    If 0 <= (y - transY + j * 32) And (y - transY + j * 32) < palbmp.Height Then
                        pixelsstream.Position = (y - transY) * 32 * 8 + i * 32 + j * 32 * 32 * 8
                        memstream.Position = y * 32
                        binarywriter.Write(binaryreader.ReadBytes(32))
                    End If


                Next

                binaryreader.Close()
                memstream.Close()
                tilenum += 1
            Next
        Next


        Dim MaxX As Integer = Math.Max((SelectPal1 Mod 8) * 32, (SelectPal2 Mod 8) * 32)
        Dim MaxY As Integer = Math.Max((SelectPal1 \ 8) * 32, (SelectPal2 \ 8) * 32) - VScrollBar2.Value

        Dim MinX As Integer = Math.Min((SelectPal1 Mod 8) * 32, (SelectPal2 Mod 8) * 32)
        Dim MinY As Integer = Math.Min((SelectPal1 \ 8) * 32, (SelectPal2 \ 8) * 32) - VScrollBar2.Value

        Dim a(MaxX - MinX + 32 - 1) As Byte
        Dim SelectColor As Byte = 111
        For i = 0 To a.Length - 1
            a(i) = SelectColor
        Next


        'grp.DrawRectangle(pens, MinX, MinY, MaxX - MinX +32, MaxY - MinY + 32)
        If 0 <= MinY And MinY < palbmp.Height Then
            For i = 0 To 3
                pixelsstream.Position = (MinY + i) * 32 * 8 + MinX
                binarywriter.Write(a)
            Next
        End If

        If 0 <= MaxY And MaxY < palbmp.Height - 32 Then
            For i = 0 To 3
                pixelsstream.Position = (MaxY + 32 - i) * 32 * 8 + MinX
                binarywriter.Write(a)
            Next
        End If





        For i = 0 To MaxY - MinY + 32 - 1
            If 0 <= (i + MinY) And (i + MinY) < palbmp.Height Then
                For j = 0 To 3
                    pixels(MinX + (i + MinY) * 32 * 8 + j) = SelectColor

                    pixels(MaxX + 31 - j + (i + MinY) * 32 * 8) = SelectColor
                Next
            End If
        Next





        binarywriter.Close()
        pixelsstream.Close()




        Marshal.Copy(pixels, 0, scan0, pixels.Length)

        palbmp.UnlockBits(bmd)








        'PaintPal.Size = bmp.Size

        PaintPal.Image = palbmp
        'PaintPal.BackgroundImage = palbmp
    End Sub
    Private Sub GetMapImage()
        'bmp.width 가로사이즈
        'bmp.height 세로사이즈

        '(y * (bmpsize.Width \ 4) + x)

        'DrawRect(scoll.X, scoll.Y)
        ' For i = 0 To bmp.Width \ 32
        ' For j = 0 To bmp.Height \ 32
        'DrawRect(scoll.X Mod 32 + i * 32, scoll.Y Mod 32 + j * 32)
        '
        ' Next
        '  Next

        'scoll.X, scoll.Y
        For j = 0 To bmp.Height \ 32 + 1
            For i = 0 To bmp.Width \ 32 + 1
                Try
                    'DrawRect(i * 32, j * 32, MTXMDATA(0))
                    DrawRect(i * 32 - scoll.X Mod 32, j * 32 - scoll.Y Mod 32, i + scoll.X \ 32 + (j + scoll.Y \ 32) * MapSize.Width)
                Catch ex As Exception

                End Try

            Next
        Next
        '

        'For j = 0 To bmpsize.Width \ 32 - 1

        '    For i = 0 To bmpsize.Height - 1
        '        Dim x As Integer = 100 'j * 8 '+ (scoll.X \ 4) Mod 8
        '        Dim y As Integer = 100 'i ' + (scoll.Y \ 4) Mod 8
        '        pixels(GetOffset(x, y)) = &HFF
        '    Next
        'Next


    End Sub



    Private Sub DrawMap(Optional refresh As Boolean = True)
        DrawMiniMapRect()
        'Dim grp As Graphics = Graphics.FromImage(bmp)
        'GRP.Clear(Color.Black)

        'Dim bm As New Bitmap(bmp.Width, bmp.Height, Imaging.PixelFormat.Format8bppIndexed)

        '8~15





        Dim bmd As New BitmapData
        bmd = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, Imaging.PixelFormat.Format8bppIndexed)



        Dim scan0 As IntPtr = bmd.Scan0
        Dim stride As Integer = bmd.Stride



        Dim size As UInteger
        If bmp.Width Mod 4 = 0 Then
            size = CInt(bmp.Width) * CInt(bmp.Height) - 1
            bmpsize.Width = bmp.Width
            bmpsize.Height = bmp.Height
        Else
            size = CInt(bmp.Width) * CInt(bmp.Height) - 1 + (4 - (bmp.Width Mod 4)) * CInt(bmp.Height)
            bmpsize.Width = bmp.Width + 4 - (bmp.Width Mod 4)
            bmpsize.Height = bmp.Height
        End If


        ReDim pixels(size)

        If refresh = False Then
            Marshal.Copy(scan0, pixels, 0, pixels.Length)
            GetMapImage()
        Else
            GetMapImage()
        End If


        ' MsgBox(pixels.Length & " " & GRPFrame(frame).Image.Length)



        ' Dim b1 As Byte = (i * 4) Mod 256
        'Dim b2 As Byte = (i * 4 + 1) Mod 256
        'Dim b3 As Byte = (i * 4 + 2) Mod 256
        'Dim b4 As Byte = (i * 4 + 3) Mod 256


        'pixels(i) = GetPixel(b1, b2, b3, b4)
        'pixels(i) = &H80000000 'Val("&H" & "80000000")



        '138이 남는다.
        'pixels = GRPFrame(frame).Image
        'For i = 0 To bmp.Width - 1
        'For j = 0 To bmp.Height - 1
        'If bmp.Width Mod 4 = 0 Then
        'pixels(i + j * bmp.Width) = Rnd() Mod 256
        'Else
        'pixels(i + j * (bmp.Width + 4 - (bmp.Width Mod 4))) = Rnd() Mod 256
        'End If
        'Next
        'Next


        Marshal.Copy(pixels, 0, scan0, pixels.Length)

        bmp.UnlockBits(bmd)


        'Dim pixels(size) As Byte
        'Marshal.Copy(scan0, pixels, 0, pixels.Length)

        '' MsgBox(pixels.Length & " " & GRPFrame(frame).Image.Length)


        ''138이 남는다.
        ''pixels = GRPFrame(frame).Image
        'For i = 0 To bmp.Width - 1
        '    For j = 0 To bmp.Height - 1
        '        If bmp.Width Mod 4 = 0 Then
        '            pixels(i + j * bmp.Width) = Rnd() Mod 256
        '        Else
        '            pixels(i + j * (bmp.Width + 4 - (bmp.Width Mod 4))) = Rnd() Mod 256
        '        End If
        '    Next
        'Next


        'Marshal.Copy(pixels, 0, scan0, pixels.Length)

        'bmp.UnlockBits(bmd)



        'Dim CPalette As Imaging.ColorPalette
        'CPalette = bmp.Palette
        'For i = 0 To 255
        '    If 15 >= i And i >= 8 Then
        '        CPalette.Entries(i) = MapPalett(0) '흔들리는 색
        '    Else
        '        CPalette.Entries(i) = MapPalett(i)
        '    End If

        'Next


        'bmp.Palette = CPalette


        'Map.Image = bmp

        Map.Image = bmp
    End Sub







    Private Sub RefreshBMP()

        palbmp = New Bitmap(8 * 32, PaintPal.Height, PixelFormat.Format8bppIndexed)







        bmp = New Bitmap(Map.Size.Width, Map.Size.Height, PixelFormat.Format8bppIndexed)
        Dim CPalette As Imaging.ColorPalette
        CPalette = bmp.Palette
        For i = 0 To 255
            If 15 >= i And i >= 8 Then
                CPalette.Entries(i) = MapPalett(i) '흔들리는 색
            Else
                CPalette.Entries(i) = MapPalett(i)
            End If

        Next
        palbmp.Palette = CPalette

        bmp.Palette = CPalette

        Map.Image = bmp
        HScrollBar1.Maximum = MapSize.Width * 32
        VScrollBar1.Maximum = MapSize.Height * 32

        HScrollBar1.LargeChange = Map.Size.Width
        VScrollBar1.LargeChange = Map.Size.Height


        If (HScrollBar1.Value + HScrollBar1.LargeChange) > HScrollBar1.Maximum Then
            HScrollBar1.Value = HScrollBar1.Maximum - HScrollBar1.LargeChange
        End If
        If (VScrollBar1.Value + VScrollBar1.LargeChange) > VScrollBar1.Maximum Then
            VScrollBar1.Value = VScrollBar1.Maximum - VScrollBar1.LargeChange
        End If
        scoll.X = HScrollBar1.Value
        scoll.Y = VScrollBar1.Value




        VScrollBar2.Maximum = (TilebitDATA.Count \ 8) * 32

        VScrollBar2.LargeChange = PaintPal.Size.Height


        If (VScrollBar2.Value + VScrollBar2.LargeChange) > VScrollBar2.Maximum Then
            VScrollBar2.Value = VScrollBar2.Maximum - VScrollBar2.LargeChange
        End If


        GetPalletImage()
        DrawMap()
    End Sub
    Private Sub TileSetForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetTooltip(Me, ToolStrip1)


        workflow.works.Clear()
        workflow.CurrentIndex = 0
        ToolStripButton2.Enabled = False
        ToolStripButton2.Enabled = False
        SaveStatus = True
        HScrollBar1.Value = 0
        VScrollBar1.Value = 0
        scoll.X = 0
        scoll.Y = 0

        BrushMode = 0
        workflow.refreshButton()
        PalletRefresh()
        RefreshBMP()
        GetMinimapImage()
        'DrawPalletIRect()
    End Sub
    Private Sub TileSetForm_Closed(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        Dim Dialog As DialogResult
        If SaveStatus = False Then
            Dialog = MsgBox("변경된 지형을 저장하시겠습니까?", MsgBoxStyle.YesNoCancel, "EUD Editor")

            If Dialog = DialogResult.Yes Then
                SaveStatus = True
                SaveTOCHK()
            ElseIf Dialog = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub


    Private Sub PalletRefresh()
        ToolStripButton11.Checked = False
        ToolStripButton12.Checked = False
        ToolStripButton4.Checked = False
        ToolStripButton5.Checked = False
        ToolStripButton6.Checked = False
        ToolStripButton7.Checked = False
        ToolStripButton8.Checked = False
        ToolStripButton9.Checked = False
        ToolStripButton10.Checked = False
        Select Case BrushMode
            Case BMode.Pallet
                ToolStripButton11.Checked = True

            Case BMode.CopyPaste

                ToolStripButton12.Checked = True
            Case BMode.hDefault
                ToolStripButton4.Checked = True
            Case BMode.LPassing
                ToolStripButton8.Checked = True
                ToolStripButton5.Checked = True
            Case BMode.MPassing
                ToolStripButton9.Checked = True
                ToolStripButton5.Checked = True
            Case BMode.HPassing
                ToolStripButton10.Checked = True
                ToolStripButton5.Checked = True
            Case BMode.LCreate
                ToolStripButton8.Checked = True
                ToolStripButton6.Checked = True
            Case BMode.MCreate
                ToolStripButton9.Checked = True
                ToolStripButton6.Checked = True
            Case BMode.HCreate
                ToolStripButton10.Checked = True
                ToolStripButton6.Checked = True
            Case BMode.LBlock
                ToolStripButton8.Checked = True
                ToolStripButton7.Checked = True
            Case BMode.MBlock
                ToolStripButton9.Checked = True
                ToolStripButton7.Checked = True
            Case BMode.HBlock
                ToolStripButton10.Checked = True
                ToolStripButton7.Checked = True
        End Select


        '11
        '12
        '4
        '5
        '6
        '7
        '8
        '9
    End Sub


    Dim drawing As Boolean
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If drawing = True Then
            DrawMap()
            drawing = False
        Else
            Timer1.Enabled = False
        End If
        If ismapDarwL Then
            If mapmousePos.X = 0 Then
                ScrollMap(HScrollBar1.Value - 4, VScrollBar1.Value)

            End If
            If mapmousePos.X = Map.Width - 1 Then
                ScrollMap(HScrollBar1.Value + 4, VScrollBar1.Value)

            End If

            If mapmousePos.Y = 0 Then
                ScrollMap(HScrollBar1.Value, VScrollBar1.Value - 4)

            End If
            If mapmousePos.Y = Map.Height Then
                ScrollMap(HScrollBar1.Value, VScrollBar1.Value + 4)

            End If
            SelectTIle = (scoll.X + mapmousePos.X) \ 32 + ((scoll.Y + mapmousePos.Y) \ 32) * MapSize.Width
        End If


        'scoll.X += 1
        'scoll.Y += 1
    End Sub

    Private Sub Map_SizeChanged(sender As Object, e As EventArgs) Handles Map.SizeChanged
        RefreshBMP()
    End Sub



    Private Sub VScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles VScrollBar1.Scroll
        drawing = True
        Timer1.Enabled = True
        scoll.Y = e.NewValue
    End Sub


    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar1.Scroll
        drawing = True
        Timer1.Enabled = True
        scoll.X = e.NewValue
    End Sub


    Private Sub ScrollMap(x As Integer, y As Integer)
        If (0 < x) And (x < (HScrollBar1.Maximum - Map.Size.Width)) Then
            HScrollBar1.Value = x
            drawing = True
            Timer1.Enabled = True
            scoll.X = HScrollBar1.Value
        ElseIf (x <= 0) Then
            HScrollBar1.Value = 0
            drawing = True
            Timer1.Enabled = True
            scoll.X = HScrollBar1.Value
        ElseIf ((HScrollBar1.Maximum - Map.Size.Width) <= x) Then
            HScrollBar1.Value = HScrollBar1.Maximum - Map.Size.Width
            drawing = True
            Timer1.Enabled = True
            scoll.X = HScrollBar1.Value
        End If

        If (0 < y) And (y < (VScrollBar1.Maximum - Map.Size.Height)) Then
            VScrollBar1.Value = y
            drawing = True
            Timer1.Enabled = True
            scoll.Y = VScrollBar1.Value
        ElseIf (y <= 0) Then
            VScrollBar1.Value = 0
            drawing = True
            Timer1.Enabled = True
            scoll.Y = VScrollBar1.Value
        ElseIf ((VScrollBar1.Maximum - Map.Size.Height) <= y) Then
            VScrollBar1.Value = VScrollBar1.Maximum - Map.Size.Height
            drawing = True
            Timer1.Enabled = True
            scoll.Y = VScrollBar1.Value
        End If
    End Sub

    Dim ShiftHokey As Boolean
    Dim CtrlHokey As Boolean
    Private Sub KeyBoard_Click(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Dim scrollSpeed As Integer = 32

        Select Case e.KeyCode
            Case Keys.Down
                ScrollMap(HScrollBar1.Value, VScrollBar1.Value + scrollSpeed)
            Case Keys.Up
                ScrollMap(HScrollBar1.Value, VScrollBar1.Value - scrollSpeed)
            Case Keys.Left
                ScrollMap(HScrollBar1.Value - scrollSpeed, VScrollBar1.Value)
            Case Keys.Right
                ScrollMap(HScrollBar1.Value + scrollSpeed, VScrollBar1.Value)
            Case Keys.ShiftKey
                If ShiftHokey = False Then
                    drawing = True
                    Timer1.Enabled = True
                End If
                ShiftHokey = True
                Map.Cursor = Cursors.Cross
            Case Keys.ControlKey
                If CtrlHokey = False Then
                    drawing = True
                    Timer1.Enabled = True
                End If
                CtrlHokey = True
                Map.Cursor = Cursors.Hand
        End Select

    End Sub
    Private Sub KeyBoard_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        Select Case e.KeyCode

            Case Keys.ShiftKey
                If ShiftHokey = True Then
                    drawing = True
                    Timer1.Enabled = True
                End If

                ShiftHokey = False
                If Map.Cursor = Cursors.Cross Then

                    Map.Cursor = Cursors.Default
                End If
            Case Keys.ControlKey
                If CtrlHokey = True Then
                    drawing = True
                    Timer1.Enabled = True
                End If


                CtrlHokey = False
                If Map.Cursor = Cursors.Hand Then

                    Map.Cursor = Cursors.Default
                End If
        End Select
    End Sub

    Dim mapmousePos As Point
    Dim OldmapMouspos As Point
    Dim ismapDarwR As Boolean = False
    Dim ismapDarwL As Boolean = False
    Dim ismapDarwC As Boolean = False
    Dim ismapDarwS As Boolean = False

    Private BrushMode As BMode
    Private Enum BMode
        Pallet = 0
        CopyPaste = 1
        hDefault = 2
        LPassing = 3
        MPassing = 4
        HPassing = 5


        LCreate = 6
        MCreate = 7
        HCreate = 8


        LBlock = 9
        MBlock = 10
        HBlock = 11

    End Enum


    Private isDragBrushMode As Boolean
    Private Sub Map_MouseDown(sender As Object, e As MouseEventArgs) Handles Map.MouseDown



        If e.Button = MouseButtons.Right Then
            OldmapMouspos = mapmousePos
            OldmapMouspos.X += scoll.X
            OldmapMouspos.Y += scoll.Y
            Map.Cursor = Cursors.NoMove2D
            ismapDarwR = True
        ElseIf e.Button = MouseButtons.Left Then
            If CtrlHokey Then

                BrushMode = 1
                SelectTIle1 = (scoll.X + mapmousePos.X) \ 32 + ((scoll.Y + mapmousePos.Y) \ 32) * MapSize.Width
                SelectTIle2 = (scoll.X + mapmousePos.X) \ 32 + ((scoll.Y + mapmousePos.Y) \ 32) * MapSize.Width
                drawing = True
                Timer1.Enabled = True
                ismapDarwC = True
            ElseIf ShiftHokey Then
                workflow.AddList()
                Map.Cursor = Cursors.Cross
                isDragBrushMode = True
                SelectTIle1 = (scoll.X + mapmousePos.X) \ 32 + ((scoll.Y + mapmousePos.Y) \ 32) * MapSize.Width
                SelectTIle2 = (scoll.X + mapmousePos.X) \ 32 + ((scoll.Y + mapmousePos.Y) \ 32) * MapSize.Width
                drawing = True
                Timer1.Enabled = True
                ismapDarwS = True

            Else
                workflow.AddList()
                lastSelectTile = SelectTIle
                BrushToTile()
                ismapDarwL = True
                drawing = True
                Timer1.Enabled = True
            End If
        End If
        PalletRefresh()
    End Sub
    Private Sub Map_MouseUP(sender As Object, e As MouseEventArgs) Handles Map.MouseUp
        Map.Cursor = Cursors.Default

        If e.Button = MouseButtons.Right Then
            ismapDarwR = False
        ElseIf e.Button = MouseButtons.Left Then
            If ismapDarwC Then

                Dim MaxX As Integer = Math.Max((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MaxY As Integer = Math.Max((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                Dim MinX As Integer = Math.Min((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MinY As Integer = Math.Min((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))


                ReDim TileBrush(MaxX - MinX, MaxY - MinY)
                TileBrushSize = New Size(MaxX - MinX, MaxY - MinY)


                For y As UInteger = 0 To MaxY - MinY
                    For x As UInteger = 0 To MaxX - MinX
                        TileBrush(x, y) = MTXMDATA(MinX + x + (MinY + y) * MapSize.Width)
                    Next
                Next

                'MsgBox(MaxX - MinX)
                BrushMode = 1
                ismapDarwC = False
            ElseIf ismapDarwS Then
                BrushToTile()



                isDragBrushMode = False
                ismapDarwS = False
            End If

            ismapDarwL = False

            GetMinimapImage()
        End If

        PalletRefresh()
        DrawMap()
    End Sub

    Dim lastSelectTile As Integer = -1
    Private Sub BrushToTile()
        workflow.ClearWorkData()

        If ismapDarwS Then
            If BrushMode = BMode.CopyPaste Then
                workflow.works(0).isBrushMode = True
                Dim MaxX As Integer = Math.Max((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MaxY As Integer = Math.Max((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                Dim MinX As Integer = Math.Min((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MinY As Integer = Math.Min((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                For y As UInteger = 0 To MaxY - MinY
                    For x As UInteger = 0 To MaxX - MinX
                        Dim tx, ty As Integer
                        tx = MinX + x
                        ty = MinY + y
                        If 0 <= tx And tx <= MapSize.Width And 0 <= ty And ty < MapSize.Height Then
                            workflow.works(0).AddAction(tx + ty * MapSize.Width, TileBrush(x Mod (TileBrushSize.Width + 1), (y Mod (TileBrushSize.Height + 1))))
                            MTXMDATA(tx + ty * MapSize.Width) = TileBrush(x Mod (TileBrushSize.Width + 1), (y Mod (TileBrushSize.Height + 1)))
                        End If
                    Next
                Next
                SaveStatus = False
            ElseIf BrushMode = BMode.Pallet Then
                workflow.works(0).isBrushMode = True
                Dim MaxX2 As UInteger = Math.Max((SelectPal1 Mod 8), (SelectPal2 Mod 8))
                Dim MaxY2 As UInteger = Math.Max((SelectPal1 \ 8), (SelectPal2 \ 8))

                Dim MinX2 As UInteger = Math.Min((SelectPal1 Mod 8), (SelectPal2 Mod 8))
                Dim MinY2 As UInteger = Math.Min((SelectPal1 \ 8), (SelectPal2 \ 8))

                Dim TWidth As UInteger = MaxX2 - MinX2
                Dim THeight As UInteger = MaxY2 - MinY2

                Dim SelectTilePos As New Point(SelectTIle Mod MapSize.Width, SelectTIle \ MapSize.Width)





                Dim MaxX As Integer = Math.Max((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MaxY As Integer = Math.Max((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                Dim MinX As Integer = Math.Min((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MinY As Integer = Math.Min((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                For y As UInteger = 0 To MaxY - MinY
                    For x As UInteger = 0 To MaxX - MinX
                        Dim tx, ty As Integer
                        tx = MinX + x
                        ty = MinY + y
                        If 0 <= tx And tx <= MapSize.Width And 0 <= ty And ty < MapSize.Height Then
                            workflow.works(0).AddAction(tx + ty * MapSize.Width, MinX2 + x Mod (TWidth + 1) + (MinY2 + y Mod (THeight + 1)) * 8)
                            MTXMDATA(tx + ty * MapSize.Width) = MinX2 + x Mod (TWidth + 1) + (MinY2 + y Mod (THeight + 1)) * 8
                        End If
                    Next
                Next
                SaveStatus = False
            Else
                workflow.works(0).isBrushMode = False
                Dim MaxX As Integer = Math.Max((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MaxY As Integer = Math.Max((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                Dim MinX As Integer = Math.Min((SelectTIle1 Mod MapSize.Width), (SelectTIle2 Mod MapSize.Width))
                Dim MinY As Integer = Math.Min((SelectTIle1 \ MapSize.Width), (SelectTIle2 \ MapSize.Width))

                For y As UInteger = 0 To MaxY - MinY
                    For x As UInteger = 0 To MaxX - MinX
                        Dim tx, ty As Integer
                        tx = MinX + x
                        ty = MinY + y
                        If 0 <= tx And tx <= MapSize.Width And 0 <= ty And ty < MapSize.Height Then
                            workflow.works(0).AddAction(tx + ty * MapSize.Width, BrushMode - 2)
                            ProjectMTXMDATA(tx + ty * MapSize.Width) = BrushMode - 2
                        End If
                    Next
                Next

            End If

        Else
            If BrushMode = BMode.CopyPaste Then
                workflow.works(0).isBrushMode = True
                Dim TWidth As UInteger = TileBrushSize.Width
                Dim THeight As UInteger = TileBrushSize.Height

                Dim SelectTilePos As New Point(SelectTIle Mod MapSize.Width, SelectTIle \ MapSize.Width)


                For y As UInteger = 0 To THeight
                    For x As UInteger = 0 To TWidth
                        If MapSize.Width > SelectTilePos.X - (TWidth \ 2) + x And SelectTilePos.X - (TWidth \ 2) + x >= 0 And MapSize.Height > (y + SelectTilePos.Y - THeight \ 2) And (y + SelectTilePos.Y - THeight \ 2 >= 0) Then
                            workflow.works(0).AddAction(SelectTilePos.X - (TWidth \ 2) + x + (y + SelectTilePos.Y - THeight \ 2) * MapSize.Width, TileBrush(x, y))
                            MTXMDATA(SelectTilePos.X - (TWidth \ 2) + x + (y + SelectTilePos.Y - THeight \ 2) * MapSize.Width) = TileBrush(x, y)
                        End If
                    Next
                Next
                SaveStatus = False
            ElseIf BrushMode = BMode.Pallet Then
                workflow.works(0).isBrushMode = True
                Dim MaxX As UInteger = Math.Max((SelectPal1 Mod 8), (SelectPal2 Mod 8))
                Dim MaxY As UInteger = Math.Max((SelectPal1 \ 8), (SelectPal2 \ 8))

                Dim MinX As UInteger = Math.Min((SelectPal1 Mod 8), (SelectPal2 Mod 8))
                Dim MinY As UInteger = Math.Min((SelectPal1 \ 8), (SelectPal2 \ 8))

                Dim TWidth As UInteger = MaxX - MinX
                Dim THeight As UInteger = MaxY - MinY

                Dim SelectTilePos As New Point(SelectTIle Mod MapSize.Width, SelectTIle \ MapSize.Width)


                For y As UInteger = 0 To THeight
                    For x As UInteger = 0 To TWidth
                        If MapSize.Width > SelectTilePos.X - (TWidth \ 2) + x And SelectTilePos.X - (TWidth \ 2) + x >= 0 And MapSize.Height > (y + SelectTilePos.Y - THeight \ 2) And (y + SelectTilePos.Y - THeight \ 2 >= 0) Then
                            workflow.works(0).AddAction(SelectTilePos.X - (TWidth \ 2) + x + (y + SelectTilePos.Y - THeight \ 2) * MapSize.Width, MinX + x + (MinY + y) * 8)
                            MTXMDATA(SelectTilePos.X - (TWidth \ 2) + x + (y + SelectTilePos.Y - THeight \ 2) * MapSize.Width) = MinX + x + (MinY + y) * 8
                        End If
                    Next
                Next
                '+ TWidth \ 2 + TWidth Mod 2
                SaveStatus = False
            Else
                workflow.works(0).isBrushMode = False
                workflow.works(0).AddAction(SelectTIle, BrushMode - 2)
                ProjectMTXMDATA(SelectTIle) = BrushMode - 2
            End If
        End If



    End Sub
    Private Sub MapMouseHover(sender As Object, e As MouseEventArgs) Handles Map.MouseMove
        mapmousePos.X = e.X
        If 0 > e.X Then
            mapmousePos.X = 0
        End If
        If Map.Width < e.X Then
            mapmousePos.X = Map.Width - 1
        End If

        mapmousePos.Y = e.Y
        If 0 > e.Y Then
            mapmousePos.Y = 0
        End If
        If Map.Height < e.Y Then
            mapmousePos.Y = Map.Height
        End If


        If ismapDarwC Or ismapDarwS Then
            SelectTIle2 = (scoll.X + mapmousePos.X) \ 32 + ((scoll.Y + mapmousePos.Y) \ 32) * MapSize.Width
        Else
            SelectTIle1 = (scoll.X + mapmousePos.X) \ 32 + ((scoll.Y + mapmousePos.Y) \ 32) * MapSize.Width
            SelectTIle2 = (scoll.X + mapmousePos.X) \ 32 + ((scoll.Y + mapmousePos.Y) \ 32) * MapSize.Width

        End If

        SelectTIle = (scoll.X + mapmousePos.X) \ 32 + ((scoll.Y + mapmousePos.Y) \ 32) * MapSize.Width
        If ismapDarwL Then
            If lastSelectTile <> SelectTIle Then
                BrushToTile()
                lastSelectTile = SelectTIle
            End If
        End If
        drawing = True
        Timer1.Enabled = True
        If ismapDarwR = True Then
            ScrollMap(OldmapMouspos.X - mapmousePos.X, OldmapMouspos.Y - mapmousePos.Y)
        End If
    End Sub

    Private Shared CTileset() As Byte = {1, 0, 0, 2, 1, 3, 3, 3}
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles ColorCycle.Tick
        Dim CPalette As Imaging.ColorPalette
        CPalette = bmp.Palette

        Select Case CTileset(TileSetType)
            Case 1
                PalletSwap(1, 6, CPalette)
                PalletSwap(7, 13, CPalette)
                PalletSwap(248, 254, CPalette)
            Case 2
                PalletSwap(1, 4, CPalette)
                PalletSwap(5, 8, CPalette)
                PalletSwap(9, 13, CPalette)
            Case 3
                PalletSwap(1, 13, CPalette)
                PalletSwap(248, 254, CPalette)
        End Select



        bmp.Palette = CPalette
        Map.Image.Palette = bmp.Palette


        Map.Image = bmp
    End Sub
    Private Sub PalletSwap(sstart As Byte, sstop As Byte, ByRef pal As ColorPalette)
        Dim tcolor As Color = pal.Entries(sstart)
        For i = sstart To sstop - 1
            pal.Entries(i) = pal.Entries(i + 1) '흔들리는 색
        Next
        pal.Entries(sstop) = tcolor
    End Sub

    Dim ispaldarg As Boolean
    Dim PalmousePos As Point
    Private Sub PaintPal_MouseClick(sender As Object, e As MouseEventArgs) Handles PaintPal.MouseClick

        If e.Button = MouseButtons.Right Then
            SelectPal1 = (PalmousePos.X \ 32) + ((PalmousePos.Y + VScrollBar2.Value) \ 32) * 8
            SelectPal2 = (PalmousePos.X \ 32) + ((PalmousePos.Y + VScrollBar2.Value) \ 32) * 8
            BrushMode = 0
            PalletRefresh()

            LoadTileSetSingleForm.SelectPal1 = SelectPal1
            LoadTileSetSingleForm.VScrollBar2.Value = VScrollBar2.Value

            'SaveTOCHK()
            LoadTileSetSingleForm.ShowDialog()


            LoadTILEDATA(True)

            GetPalletImage()
            DrawMap()
            GetMinimapImage()
        End If

    End Sub

    Private Sub PaintPal_MouseDown(sender As Object, e As MouseEventArgs) Handles PaintPal.MouseDown
        If e.Button = MouseButtons.Left Then
            SelectPal1 = (PalmousePos.X \ 32) + ((PalmousePos.Y + VScrollBar2.Value) \ 32) * 8
            SelectPal2 = (PalmousePos.X \ 32) + ((PalmousePos.Y + VScrollBar2.Value) \ 32) * 8
            BrushMode = 0
            PalletRefresh()
            'SelectPal
            GetPalletImage()
            PalletTimer.Enabled = True
            ispaldarg = True
        End If

    End Sub
    Private Sub PaintPal_MouseMove(sender As Object, e As MouseEventArgs) Handles PaintPal.MouseMove
        PalmousePos.X = e.X
        If 0 > e.X Then
            PalmousePos.X = 0
        End If
        If PaintPal.Width < e.X Then
            PalmousePos.X = PaintPal.Width
        End If

        PalmousePos.Y = e.Y
        If 0 > e.Y Then
            PalmousePos.Y = 0
        End If
        If PaintPal.Height < e.Y Then
            PalmousePos.Y = PaintPal.Height
        End If




        If e.Button = MouseButtons.Left Then
            Dim temp As Integer = (PalmousePos.X \ 32) + ((PalmousePos.Y + VScrollBar2.Value) \ 32) * 8
            If temp <> SelectPal2 Then

                SelectPal2 = (PalmousePos.X \ 32) + ((PalmousePos.Y + VScrollBar2.Value) \ 32) * 8
                BrushMode = 0
                PalletRefresh()
            End If
            'SelectPal

        End If

    End Sub
    Private Sub PaintPal_MouseUp(sender As Object, e As MouseEventArgs) Handles PaintPal.MouseUp
        If e.Button = MouseButtons.Left Then
            SelectPal2 = (PalmousePos.X \ 32) + ((PalmousePos.Y + VScrollBar2.Value) \ 32) * 8

            If SelectPal1 > SelectPal2 Then
                Dim temp As Integer = SelectPal2
                SelectPal2 = SelectPal1
                SelectPal1 = temp
            End If
            BrushMode = 0
            PalletRefresh()
            'SelectPal
            GetPalletImage()
            ispaldarg = False
        End If

    End Sub

    Private Sub PalletTimer_Tick(sender As Object, e As EventArgs) Handles PalletTimer.Tick
        If ispaldarg = True Then
            GetPalletImage()
        Else
            PalletTimer.Enabled = False

        End If
    End Sub

    Private Sub VScrollBar2_Scroll(sender As Object, e As ScrollEventArgs) Handles VScrollBar2.Scroll
        VScrollBar2.Value = e.NewValue
        GetPalletImage()
    End Sub

    Private Sub PaintPal_MouseWheel(sender As Object, e As MouseEventArgs) Handles PaintPal.MouseWheel
        Dim tvalue As Integer = VScrollBar2.Value
        tvalue -= e.Delta


        If 0 > tvalue Then
            tvalue = 0
        End If
        If tvalue > VScrollBar2.Maximum - VScrollBar2.LargeChange Then
            tvalue = VScrollBar2.Maximum - VScrollBar2.LargeChange
        End If
        VScrollBar2.Value = tvalue
        GetPalletImage()

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        SaveTOCHK()
        SaveStatus = True
    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click
        BrushMode = BMode.Pallet
        PalletRefresh()
    End Sub

    Private Sub ToolStripButton12_Click(sender As Object, e As EventArgs) Handles ToolStripButton12.Click
        BrushMode = BMode.CopyPaste
        If TileBrush Is Nothing Then
            ReDim TileBrush(0, 0)
            TileBrushSize = New Size(0, 0)
        End If
        PalletRefresh()
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        BrushMode = BMode.hDefault

        PalletRefresh()
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        If BrushMode = 0 Or BrushMode = 1 Or BrushMode = 2 Then
            BrushMode = BMode.LPassing
        Else
            BrushMode = 3 + (BrushMode - 3) Mod 3
        End If
        PalletRefresh()
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        If BrushMode = 0 Or BrushMode = 1 Or BrushMode = 2 Then
            BrushMode = BMode.LCreate
        Else
            BrushMode = 6 + (BrushMode - 3) Mod 3
        End If
        PalletRefresh()
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        If BrushMode = 0 Or BrushMode = 1 Or BrushMode = 2 Then
            BrushMode = BMode.LBlock
        Else
            BrushMode = 9 + (BrushMode - 3) Mod 3
        End If
        PalletRefresh()
    End Sub



    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        If BrushMode = 0 Or BrushMode = 1 Or BrushMode = 2 Then
            BrushMode = BMode.LPassing
        Else
            BrushMode = 3 + ((BrushMode - 3) \ 3) * 3
        End If
        PalletRefresh()
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        If BrushMode = 0 Or BrushMode = 1 Or BrushMode = 2 Then
            BrushMode = BMode.MPassing
        Else
            BrushMode = 4 + ((BrushMode - 3) \ 3) * 3
        End If
        PalletRefresh()
    End Sub

    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ToolStripButton10.Click
        If BrushMode = 0 Or BrushMode = 1 Or BrushMode = 2 Then
            BrushMode = BMode.HPassing
        Else
            BrushMode = 5 + ((BrushMode - 3) \ 3) * 3
        End If
        PalletRefresh()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        workflow.Cancle()
        DrawMap()
        GetMinimapImage()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        workflow.Retry()
        DrawMap()
        GetMinimapImage()
    End Sub

    Private Sub 실행취소UToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 실행취소UToolStripMenuItem.Click
        If ToolStripButton2.Enabled = True Then
            workflow.Cancle()
            DrawMap()
            GetMinimapImage()
        End If
    End Sub

    Private Sub 다시실행RToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 다시실행RToolStripMenuItem.Click
        If ToolStripButton3.Enabled = True Then
            workflow.Retry()
            DrawMap()
            GetMinimapImage()
        End If
    End Sub

    Private Sub 저장SToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 저장SToolStripMenuItem.Click
        SaveTOCHK()
        SaveStatus = True
    End Sub
    
End Class

'CTables.Add(New CTable() {New CTable(1, 6), New CTable(7, 13), New CTable(248, 254)})
'CTables.Add(New CTable() {New CTable(1, 4), New CTable(5, 8), New CTable(9, 13)})
'CTables.Add(New CTable() {New CTable(1, 13), New CTable(248, 254)})