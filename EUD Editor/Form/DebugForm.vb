Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Threading


Public Class DebugForm
    Dim CountTimer As UInteger

    Private BrushMode As BrushT
    Enum BrushT
        UnitControl = 0
        UnitCreate = 1
        UnitMove = 2
        LocationMove = 3

    End Enum

    Private Sub BtnRefresh()
        Select Case BrushMode
            Case 0
                ToolStripButton13.Checked = True
                ToolStripButton14.Checked = False
            Case 1
                ToolStripButton14.Checked = True
                ToolStripButton13.Checked = False
        End Select
    End Sub




    Public GameData As New CGameData
    Private bmp As Bitmap
    Public bmpsize As New Size
    Private pixels() As Byte
    Public scoll As New Point

    Private unitColors(256 - 1, 7) As Byte
    Private MiniPalett(255) As Byte

    Dim loadingcomp As Boolean = False

    Public Sub LoadGameDATA()
        Dim filestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "Colorunit.dat", FileMode.Open)
        Dim binaryreader As New BinaryReader(filestream)


        For i = 0 To 255
            For j = 0 To 7
                unitColors(i, j) = binaryreader.ReadByte()
            Next
        Next


        binaryreader.Close()
        filestream.Close()


        filestream = New FileStream(My.Application.Info.DirectoryPath & "\Data\" & "Colorminimap.dat", FileMode.Open)
        binaryreader = New BinaryReader(filestream)


        For i = 0 To 255
            MiniPalett(i) = binaryreader.ReadByte()
        Next


        binaryreader.Close()
        filestream.Close()



        DrawTimer.Enabled = False
        If WinAPI.CheckProcess = False Then
            DrawTimer.Enabled = False
            MsgBox("StarCraft 메모리 읽기에 실패했습니다." _
                                  , MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            DialogResult = DialogResult.Abort
            Me.Close()
            Exit Sub
        End If

        '맵의 실행 중 여부를 파악합니다
        Dim gamemode As Byte = WinAPI.ReadValue(&H596904, 2)
        If gamemode <> 3 Then
            DrawTimer.Enabled = False
            Me.Close()
            MsgBox("게임 실행중이 아닙니다." _
                       , MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            DialogResult = DialogResult.Abort
            Exit Sub
        End If

        If GameData.CheckAuthority Then
            DrawTimer.Enabled = False
            MsgBox("해당 맵에 권한이 없습니다." _
                       , MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            DialogResult = DialogResult.Abort
            Me.Close()
            Exit Sub
        End If


        'Thread.Sleep(1000)



        ToolStripComboBox1.SelectedIndex = 0
        BrushMode = BrushT.UnitControl



        For i = 0 To GameData.SpriteList.Count - 1
            GameData.SpriteList(i) = New List(Of CGameData.Cimage)
        Next


        'For i = 0 To 998
        '    GameData.LoadGRP(i)
        'Next


        ListView1.LargeImageList = DatEditForm.IMAGELIST
        ListView1.Clear()

        Dim flingyNum, SpriteNum, ImageNum As Integer
        For i = 0 To 227
            ListView1.Items.Add("")
            flingyNum = DatEditDATA(DTYPE.units).ReadValue("Graphics", i)
            SpriteNum = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", flingyNum)
            ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", SpriteNum)


            ListView1.Items(i).ImageIndex = ImageNum
            ListView1.Items(i).Tag = i
        Next

        GameData.lit()



        RefreshBMP()
        DrawTimer.Enabled = True
        GetMinimapImage()
        BtnRefresh()

        loadingcomp = True
    End Sub


    Private Sub DebugForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProjectSet.LoadCHKdata()
        LoadGameDATA()
    End Sub

    Private Sub DebugForm_closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        DrawTimer.Enabled = False

        DethesViewerForm.Close()
        GamedataForm.Close()
        PlayerdataForm.Close()
        SwitchViewerForm.Close()


        GameData.Close()
        'ebugData
        'StarCraftVisibleForm.Close()
    End Sub


    Private Sub Map_SizeChanged(sender As Object, e As EventArgs) Handles Map.SizeChanged
        If loadingcomp Then
            RefreshBMP()
        End If
    End Sub
    Private Sub RefreshBMP()
        bmp = New Bitmap(Map.Size.Width, Map.Size.Height, PixelFormat.Format8bppIndexed)
        Dim CPalette As Imaging.ColorPalette
        CPalette = bmp.Palette
        For i = 0 To 255
            If 15 >= i And i >= 8 Then
                CPalette.Entries(i) = GameData.MapPalett(i) '흔들리는 색
            Else
                CPalette.Entries(i) = GameData.MapPalett(i)
            End If

        Next
        bmp.Palette = CPalette


        Map.Image = bmp


        HScrollBar1.Maximum = GameData.MapSize.Width * 32 - 3
        VScrollBar1.Maximum = GameData.MapSize.Height * 32 - 1

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
    End Sub

    Private Sub MiniMap_Click(sender As Object, e As MouseEventArgs) Handles MiniMap.Click
        If e.Button = MouseButtons.Right Then
            Dim x As Single = e.X
            Dim y As Single = e.Y

            If GameData.MapSize.Width <> GameData.MapSize.Height Then
                If GameData.MapSize.Width > GameData.MapSize.Height Then
                    y -= (GameData.MapSize.Width - GameData.MapSize.Height) / 4
                Else
                    x -= (GameData.MapSize.Height - GameData.MapSize.Width) / 4
                End If
            End If

            Dim maxsize As Integer = Math.Max(GameData.MapSize.Width, GameData.MapSize.Height)
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


            GameData.OrderUnit(CGameData.order.attack_move, New Point(x, y))
        End If
    End Sub





    Private Sub MiniMap_MouseMove(sender As Object, e As MouseEventArgs) Handles MiniMap.MouseMove, MiniMap.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim x As Single = e.X
            Dim y As Single = e.Y
            Dim width As Integer = Map.Size.Width / 32
            Dim height As Integer = Map.Size.Height / 32

            If GameData.MapSize.Width <> GameData.MapSize.Height Then
                If GameData.MapSize.Width > GameData.MapSize.Height Then
                    y -= (GameData.MapSize.Width - GameData.MapSize.Height) / 4
                Else
                    x -= (GameData.MapSize.Height - GameData.MapSize.Width) / 4
                End If
            End If

            Dim maxsize As Integer = Math.Max(GameData.MapSize.Width, GameData.MapSize.Height)
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
    Private Sub DrawMiniMapRect()
        Dim bmp As New Bitmap(128, 128)
        Dim grp As Graphics = Graphics.FromImage(bmp)


        Dim x As Single
        Dim y As Single
        Dim width As Integer = Map.Size.Width / 32
        Dim height As Integer = Map.Size.Height / 32



        If GameData.MapSize.Width <> GameData.MapSize.Height Then
            If GameData.MapSize.Width > GameData.MapSize.Height Then
                y += (GameData.MapSize.Width - GameData.MapSize.Height) / 2
            Else
                x += (GameData.MapSize.Height - GameData.MapSize.Width) / 2
            End If
        End If


        '줌 정도와 미니맵 위치.
        Dim maxsize As Integer = Math.Max(GameData.MapSize.Width, GameData.MapSize.Height)
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


        For i = 0 To GameData.UnitNode.Count - 1
            x = GameData.UnitNode(i).pos.X / 32
            y = GameData.UnitNode(i).pos.Y / 32

            x = (x * zoomdegree) / 6
            y = (y * zoomdegree) / 6

            Dim brushl As New SolidBrush(GameData.MapPalett(MiniPalett(GameData.UnitNode(i).plyer)))

            '
            grp.FillRectangle(brushl, x - 1, y - 1, 2, 2)
        Next

        x = scoll.X / 32
        y = scoll.Y / 32

        x = (x * zoomdegree) / 6
        y = (y * zoomdegree) / 6
        width = (width * zoomdegree) / 6
        height = (height * zoomdegree) / 6


        grp.DrawRectangle(Pens.White, x, y, width, height)



        MiniMap.Image = bmp
    End Sub





    Private Sub ScrollMap(x As Integer, y As Integer)
        If (0 < x) And (x < (HScrollBar1.Maximum - Map.Size.Width)) Then
            HScrollBar1.Value = x
            scoll.X = HScrollBar1.Value
        ElseIf (x <= 0) Then
            HScrollBar1.Value = 0
            scoll.X = HScrollBar1.Value
        ElseIf ((HScrollBar1.Maximum - Map.Size.Width) <= x) Then
            HScrollBar1.Value = HScrollBar1.Maximum - Map.Size.Width
            scoll.X = HScrollBar1.Value
        End If

        If (0 < y) And (y < (VScrollBar1.Maximum - Map.Size.Height)) Then
            VScrollBar1.Value = y
            scoll.Y = VScrollBar1.Value
        ElseIf (y <= 0) Then
            VScrollBar1.Value = 0
            scoll.Y = VScrollBar1.Value
        ElseIf ((VScrollBar1.Maximum - Map.Size.Height) <= y) Then
            VScrollBar1.Value = VScrollBar1.Maximum - Map.Size.Height
            scoll.Y = VScrollBar1.Value
        End If
    End Sub

    Private Sub GetMinimapImage()
        Dim bmp As New Bitmap(GameData.MapSize.Width, GameData.MapSize.Height, PixelFormat.Format8bppIndexed)
        Dim CPalette As Imaging.ColorPalette
        CPalette = bmp.Palette
        For i = 0 To 255
            CPalette.Entries(i) = GameData.MapPalett(i)
        Next


        bmp.Palette = CPalette


        Dim bmd As New BitmapData
        bmd = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, Imaging.PixelFormat.Format8bppIndexed)



        Dim scan0 As IntPtr = bmd.Scan0
        Dim stride As Integer = bmd.Stride




        Dim pixels(bmp.Width * bmp.Height - 1) As Byte



        For j = 0 To GameData.MapSize.Height - 1
            For i = 0 To GameData.MapSize.Width - 1
                pixels(i + j * GameData.MapSize.Width) = GameData.Minimap(i, j)
            Next
        Next




        Marshal.Copy(pixels, 0, scan0, pixels.Length)

        bmp.UnlockBits(bmd)




        MiniMap.BackgroundImage = bmp
    End Sub


    Private Sub DrawMap()
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



        Dim memstream As New MemoryStream(pixels)
        Dim binarystream As New BinaryWriter(memstream)

        For i = 0 To bmpsize.Height - 1
            binarystream.Write(GameData.Bitdata, scoll.X + (i + scoll.Y) * GameData.MapSize.Width * 32, bmpsize.Width)
        Next


        If ToolStripButton11.Checked Then
            For j = 0 To GameData.SpriteList.Count - 1
                For i = GameData.SpriteList(j).Count - 1 To 0 Step -1
                    With GameData.SpriteList(j)(i)
                        DrawGRP(New Point(.pos.X - scoll.X, .pos.Y - scoll.Y), .imagenum, .DrawFunction, .Remapping, .frameindex, .color, .flipflag) '.grpHead 83503932
                    End With
                Next
            Next
        End If



        ''로케이션==============================
        'If ToolStripButton7.Checked Then

        '    Dim ScreenL As Integer = scoll.X
        '    Dim ScreenU As Integer = scoll.Y
        '    Dim ScreenR As Integer = scoll.X + bmpsize.Width
        '    Dim ScreenD As Integer = scoll.Y + bmpsize.Height

        '    For i = 0 To GameData.LocaationTable.Count - 1
        '        Try
        '            With GameData.LocaationTable(i)
        '                If .Left + .Up + .Right + .Down <> 0 Then
        '                    If Not (ScreenR < .Left Or ScreenL > .Right Or
        '               ScreenD < .Up Or ScreenU > .Down) Then '화면 왼쪽에 있을 경우.
        '                        Dim posx1 As Integer = .Left - scoll.X
        '                        Dim posx2 As Integer = .Right - scoll.X
        '                        Dim posy1 As Integer = .Up - scoll.Y
        '                        Dim posy2 As Integer = .Down - scoll.Y


        '                        If posx1 < 0 Then
        '                            posx1 = 0
        '                        End If
        '                        If bmpsize.Width <= posx2 Then
        '                            posx2 = bmpsize.Width - 1
        '                        End If

        '                        If posy1 < 0 Then
        '                            posy1 = 0
        '                        End If
        '                        If bmpsize.Height <= posy2 Then
        '                            posy2 = bmpsize.Height - 1
        '                        End If

        '                        Dim bytes(0) As Byte
        '                        If posx2 - posx1 <> 0 Then
        '                            ReDim bytes(posx2 - posx1 - 1)
        '                        End If
        '                        For k = 0 To bytes.Count - 1
        '                            bytes(k) = 165
        '                        Next


        '                        If (0 < posx1 + posy1 * bmpsize.Width) And (posx1 + posy1 * bmpsize.Width < memstream.Length - bytes.Count) Then
        '                            memstream.Position = posx1 + posy1 * bmpsize.Width
        '                            binarystream.Write(bytes)
        '                        End If

        '                        If (0 < posx1 + posy2 * bmpsize.Width) And (posx1 + posy2 * bmpsize.Width < memstream.Length - bytes.Count) Then
        '                            memstream.Position = posx1 + posy2 * bmpsize.Width
        '                            binarystream.Write(bytes)
        '                        End If

        '                        For k = 0 To posy2 - posy1 - 1
        '                            If (0 < posx1 + posy2 * bmpsize.Width) And (posx1 + posy2 * bmpsize.Width < memstream.Length - 1) Then
        '                                memstream.Position = posx1 + (posy1 + k) * bmpsize.Width
        '                                binarystream.Write(CByte(165))
        '                            End If

        '                            If (0 < posx2 - 1 + (posy1 + k) * bmpsize.Width) And (posx2 - 1 + (posy1 + k) * bmpsize.Width < memstream.Length - 1) Then
        '                                memstream.Position = posx2 - 1 + (posy1 + k) * bmpsize.Width
        '                                binarystream.Write(CByte(165))
        '                            End If
        '                        Next

        '                    End If
        '                End If
        '            End With

        '            'Left가 화면 오른쪽일 경우
        '            'Right가 화면 왼쪽일 경우
        '            'Up이가 화면 아래쪽일 경우
        '            'Down이 화면 위쪽일 경우



        '            'GameData.LocaationTable(i).Left
        '        Catch ex As Exception

        '        End Try
        '    Next

        'End If
        ''로케이션==============================


        '유닛==============================
        If ToolStripButton11.Checked Then
            For i = 0 To GameData.UnitNode.Count - 1
                If GameData.SelectUnits.Contains(GameData.UnitNode(i).dataNum) Then
                    With GameData.UnitNode(i)
                        Dim unitx As Integer = .pos.X - scoll.X
                        Dim unity As Integer = .pos.Y - scoll.Y

                        If 0 < unitx And unitx < bmpsize.Width And
                       0 < unity And unity < bmpsize.Height Then
                            DrawGRP(New Point(unitx, unity), 573, 0, 0, 0, 0, False) '.grpHead 83503932
                        End If
                    End With
                End If

                With GameData.UnitNode(i)
                    Dim unitx As Integer = .pos.X - scoll.X
                    Dim unity As Integer = .pos.Y - scoll.Y

                    If 0 < unitx And unitx < bmpsize.Width And
                   0 < unity And unity < bmpsize.Height Then

                        'DrawRect(New Point(unitx, unity), .rect)
                    End If
                End With
            Next
        End If
        '유닛==============================



        If ismousemown Then
            Dim mousx1 As Integer = Math.Min(selectpos1.X, selectpos2.X) - scoll.X
            Dim mousx2 As Integer = Math.Max(selectpos1.X, selectpos2.X) - scoll.X
            Dim mousy1 As Integer = Math.Min(selectpos1.Y, selectpos2.Y) - scoll.Y
            Dim mousy2 As Integer = Math.Max(selectpos1.Y, selectpos2.Y) - scoll.Y

            If mousx1 < 0 Then
                mousx1 = 0
            End If
            If mousy1 < 0 Then
                mousy1 = 0
            End If
            If bmpsize.Width < mousx2 Then
                mousx2 = bmpsize.Width
            End If
            If bmpsize.Height < mousy2 Then
                mousy2 = bmpsize.Height
            End If

            Dim bytes(mousx2 - mousx1 - 1) As Byte
            For k = 0 To bytes.Count - 1
                bytes(k) = 117
            Next

            memstream.Position = mousx1 + (mousy1) * bmpsize.Width
            binarystream.Write(bytes)

            memstream.Position = mousx1 + (mousy2 - 1) * bmpsize.Width
            binarystream.Write(bytes)

            For k = 0 To mousy2 - mousy1 - 1
                memstream.Position = mousx1 + (mousy1 + k) * bmpsize.Width
                binarystream.Write(CByte(117))


                memstream.Position = mousx2 - 1 + (mousy1 + k) * bmpsize.Width
                binarystream.Write(CByte(117))
            Next
        End If



        If BrushMode = BrushT.UnitCreate Then
            Dim flingyNum, SpriteNum, ImageNum As Integer

            flingyNum = DatEditDATA(DTYPE.units).ReadValue("Graphics", lastSelectindex)
            SpriteNum = DatEditDATA(DTYPE.flingy).ReadValue("Sprite", flingyNum)
            ImageNum = DatEditDATA(DTYPE.sprites).ReadValue("Image File", SpriteNum)
            DrawGRP(mousepos, ImageNum, 0, 0, 0, ToolStripComboBox1.SelectedIndex, False) '.grpHead 83503932


        End If





        binarystream.Close()
        memstream.Close()

        Marshal.Copy(pixels, 0, scan0, pixels.Length)

        bmp.UnlockBits(bmd)



        Map.Image = bmp
    End Sub

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
            Case Keys.Delete
                GameData.OrderUnit(CGameData.order.die, New Point(0, 0))
            Case Keys.Enter
                If GameData.SelectUnits.Count <> 0 Then
                    DebugViewUnitForm.ShowDialog()
                End If
        End Select

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        GameData.SelectUnits.Clear()
        For i = 0 To GameData.UnitNode.Count - 1
            GameData.SelectUnits.Add(GameData.UnitNode(i).dataNum)
        Next
        DebugViewUnitForm.ShowDialog()


        GameData.SelectUnits.Clear()
    End Sub


    Private Sub VScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles VScrollBar1.Scroll
        scoll.Y = e.NewValue
    End Sub


    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar1.Scroll
        scoll.X = e.NewValue
    End Sub

    Dim lastpos As Point
    Private Sub DrawTimer_Tick(sender As Object, e As EventArgs) Handles DrawTimer.Tick
        If WinAPI.CheckProcess = False Then
            DrawTimer.Enabled = False

            Me.Hide()
            MsgBox("StarCraft 메모리 읽기에 실패했습니다." _
                                  , MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            DialogResult = DialogResult.Abort

            Exit Sub
        End If

        '맵의 실행 중 여부를 파악합니다
        Dim gamemode As Byte = WinAPI.ReadValue(&H596904, 2)
        If gamemode <> 3 Then
            DrawTimer.Enabled = False

            Me.Hide()

            MsgBox("게임 실행중이 아닙니다." _
                       , MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            DialogResult = DialogResult.Abort

            Exit Sub
        End If


        If ToolStripButton1.Checked Then
            ScrollMap(WinAPI.ReadValue(&H628448, 4), WinAPI.ReadValue(&H628470, 4))
        End If


        If BrushMode = BrushT.UnitCreate And iscreatedrag And lastpos <> mousepos Then
            lastpos = mousepos
            GameData.CreateUnit(mousepos.X + scoll.X, mousepos.Y + scoll.Y, lastSelectindex, ToolStripComboBox1.SelectedIndex)
        End If
        CountTimer = WinAPI.ReadValue(&H58D6F4, 4)

        GameData.ReadUnit()
        GameData.ReadLocation()
        GameData.ReadImage()
        DrawMiniMapRect()
        DrawMap()
        If ToolStripButton7.Checked Or ToolStripButton12.Checked Then
            DrawOthers()
        End If
        'DrawOthers()
    End Sub

    Private Sub DrawOthers()
        bmp = New Bitmap(Map.Size.Width, Map.Size.Height, PixelFormat.Format32bppArgb)


        bmp.Palette = Map.Image.Palette

        Dim grp As Graphics
        grp = Graphics.FromImage(bmp)



        grp.DrawImageUnscaled(Map.Image, 0, 0)


        Dim LocationBrush As New SolidBrush(Color.FromArgb(&H226B66FF))
        Dim UnitBrush As New SolidBrush(Color.FromArgb(&H551DDB16))
        '로케이션==============================
        If ToolStripButton7.Checked Then

            Dim ScreenL As Integer = scoll.X
            Dim ScreenU As Integer = scoll.Y
            Dim ScreenR As Integer = scoll.X + bmpsize.Width
            Dim ScreenD As Integer = scoll.Y + bmpsize.Height

            For i = 0 To GameData.LocaationTable.Count - 1
                Try
                    With GameData.LocaationTable(i)
                        If .Left + .Up + .Right + .Down <> 0 Then
                            If Not (ScreenR < .Left Or ScreenL > .Right Or
                       ScreenD < .Up Or ScreenU > .Down) Then '화면 왼쪽에 있을 경우.
                                Dim posx1 As Integer = .Left - scoll.X
                                Dim posx2 As Integer = .Right - scoll.X
                                Dim posy1 As Integer = .Up - scoll.Y
                                Dim posy2 As Integer = .Down - scoll.Y


                                If posx1 < 0 Then
                                    posx1 = 0
                                End If
                                If bmpsize.Width <= posx2 Then
                                    posx2 = bmpsize.Width - 1
                                End If

                                If posy1 < 0 Then
                                    posy1 = 0
                                End If
                                If bmpsize.Height <= posy2 Then
                                    posy2 = bmpsize.Height - 1
                                End If


                                grp.FillRectangle(LocationBrush, New Rectangle(posx1, posy1, posx2 - posx1, posy2 - posy1))
                                grp.DrawRectangle(Pens.White, New Rectangle(posx1, posy1, posx2 - posx1, posy2 - posy1))

                                If ProjectSet.CHKLOCATIONNAME.Count <> 0 Then
                                    grp.DrawString(ProjectSet.CHKSTRING(ProjectSet.CHKLOCATIONNAME(i) - 1), Me.Font, Brushes.Lime, posx1, posy1)
                                End If

                                'If (0 < posx1 + posy1 * bmpsize.Width) And (posx1 + posy1 * bmpsize.Width < memstream.Length - bytes.Count) Then
                                '    memstream.Position = posx1 + posy1 * bmpsize.Width
                                '    binarystream.Write(bytes)
                                'End If

                                'If (0 < posx1 + posy2 * bmpsize.Width) And (posx1 + posy2 * bmpsize.Width < memstream.Length - bytes.Count) Then
                                '    memstream.Position = posx1 + posy2 * bmpsize.Width
                                '    binarystream.Write(bytes)
                                'End If

                                'For k = 0 To posy2 - posy1 - 1
                                '    If (0 < posx1 + posy2 * bmpsize.Width) And (posx1 + posy2 * bmpsize.Width < memstream.Length - 1) Then
                                '        memstream.Position = posx1 + (posy1 + k) * bmpsize.Width
                                '        binarystream.Write(CByte(165))
                                '    End If

                                '    If (0 < posx2 - 1 + (posy1 + k) * bmpsize.Width) And (posx2 - 1 + (posy1 + k) * bmpsize.Width < memstream.Length - 1) Then
                                '        memstream.Position = posx2 - 1 + (posy1 + k) * bmpsize.Width
                                '        binarystream.Write(CByte(165))
                                '    End If
                                'Next

                            End If
                        End If
                    End With

                    'Left가 화면 오른쪽일 경우
                    'Right가 화면 왼쪽일 경우
                    'Up이가 화면 아래쪽일 경우
                    'Down이 화면 위쪽일 경우
                    '0058D6F4	1.16.1	Win	Countdown Timer	4	1



                    'GameData.LocaationTable(i).Left
                Catch ex As Exception

                End Try
            Next

        End If
        '로케이션==============================






        If ToolStripButton12.Checked Then
            For i = 0 To GameData.UnitNode.Count - 1
                With GameData.UnitNode(i)
                    Dim unitx As Integer = .pos.X - scoll.X
                    Dim unity As Integer = .pos.Y - scoll.Y

                    If 0 < unitx And unitx < bmpsize.Width And
                   0 < unity And unity < bmpsize.Height Then
                        grp.FillRectangle(UnitBrush, New Rectangle(.rect.X + unitx, .rect.Y + unity, .rect.Width, .rect.Height))
                        grp.DrawRectangle(Pens.LightGreen, New Rectangle(.rect.X + unitx, .rect.Y + unity, .rect.Width, .rect.Height))

                        DrawRect(New Point(unitx, unity), .rect)
                    End If
                End With
            Next
        End If


        'Dim Hour As UInt16 = ((CountTimer \ 60) \ 60)
        'Dim Minute As Byte = (CountTimer \ 60) Mod 60
        'Dim Second As Byte = CountTimer Mod 60


        'Dim mfont As New Font(Font.FontFamily, 12, Font.Style)


        ''57F0F0

        ''57F120
        'grp.DrawString(Format(Hour, "00") & " : " & Format(Minute, "00") & " : " & Format(Second, "00"), mfont, Brushes.White, 0, bmpsize.Height - 20)

        'Dim afont As New Font(Font.FontFamily, 8, Font.Style)

        'Dim buffer() As Byte = WinAPI.ReadValue(&H57F0F0, 4 * 12 * 2)
        'Dim memstream As New MemoryStream(buffer)

        'Dim binStream As New BinaryReader(memstream)

        'For i = 0 To 7
        '    grp.DrawString(binStream.ReadInt32, afont, Brushes.LimeGreen, 0, bmpsize.Height - 40 - (7 - i) * 10)

        'Next

        'memstream.Position = 4 * 12
        'For i = 0 To 7
        '    grp.DrawString(binStream.ReadInt32, afont, Brushes.LimeGreen, 70, bmpsize.Height - 40 - (7 - i) * 10)

        'Next

        'binStream.Close()
        'memstream.Close()



        Map.Image = bmp
    End Sub





    Enum ERemapping
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
    'GRP출력 모듈
    Private Sub DrawGRP(screenpos As Point, imagenum As UShort, drawFuc As Byte, remapping As Byte, grpframe As UShort, color As Byte, flipflag As Boolean)

        If GameData.CheckGRP(imagenum) = False Then
            GameData.LoadGRP(imagenum)
        End If
        Dim grpdata As GRP = GameData.ExportGRP(imagenum)

        'MsgBox(grpframe & " : 을 그리기 시작합니다." & vbCrLf & grpdata.GRPFrame.Count & " : 만큼의 프레임을 가지고 있습니다.")

        grpframe = grpframe Mod grpdata.framecount

        Dim xoffset As Short = screenpos.X + grpdata.GRPFrame(grpframe).frameXOffset - grpdata.grpWidth \ 2
        Dim xfoffset As Short = screenpos.X + grpdata.grpWidth \ 2 - grpdata.GRPFrame(grpframe).frameXOffset - grpdata.GRPFrame(grpframe).frameWidth
        Dim yoffset As Short = screenpos.Y + grpdata.GRPFrame(grpframe).frameYOffset - grpdata.grpHeight \ 2

        For y = 0 To grpdata.GRPFrame(grpframe).frameHeight - 1
            For x = 0 To grpdata.GRPFrame(grpframe).frameWidth - 1
                Dim pixeld As Byte = grpdata.GRPFrame(grpframe).Image(x + y * grpdata.GRPFrame(grpframe).frameWidth4)

                Dim xpos, ypos As Integer

                If flipflag Then
                    xpos = grpdata.GRPFrame(grpframe).frameWidth - 1 - x + xfoffset
                Else
                    xpos = x + xoffset
                End If
                ypos = y + yoffset





                'DrawFunction = 9
                'ofire.pcx = 1
                'gfire.pcx = 2
                'bfire.pcx = 3
                'bexpl.pcx = 4
                'trans50.pcx = 5


                'DrawFunction = 5 trans50.pcx
                'DrawFunction = 6 trans50.pcx
                'DrawFunction = 7 trans50.pcx
                'DrawFunction = 10 dark.pcx
                'DrawFunction = 16 shift.pcx



                If 0 <= xpos And xpos < bmpsize.Width And
                   0 <= ypos And ypos < bmpsize.Height Then
                    If pixeld <> 0 Then
                        If 561 <= imagenum And imagenum <= 580 Then
                            pixeld = 117
                        End If

                        If 15 >= pixeld And pixeld >= 8 Then
                            pixeld = unitColors(color, pixeld - 8)
                        End If


                        Dim orginpixeldata As Byte

                        orginpixeldata = pixels(xpos + ypos * bmpsize.Width)

                        Select Case drawFuc
                            Case 5 To 7
                                If pixeld <> 0 Then
                                    pixeld = GameData.RemappingPallet(ERemapping.trans50).GetPixel(orginpixeldata, pixeld)
                                End If
                            Case 9
                                Select Case remapping
                                    Case 1 To 5
                                        pixeld = GameData.RemappingPallet(remapping - 1).GetPixel(orginpixeldata, pixeld)

                                End Select
                            Case 10
                                If pixeld <> 0 Then
                                    pixeld = GameData.RemappingPallet(ERemapping.dark).GetPixel(orginpixeldata, 20)
                                End If
                            Case 16
                                pixeld = GameData.RemappingPallet(ERemapping.shift).GetPixel(pixeld, 0)
                        End Select




                        pixels(xpos + ypos * bmpsize.Width) = pixeld
                    End If


                    'If ToolStripButton12.Checked = True Then
                    '    If y = 0 Or y = grpdata.GRPFrame(grpframe).frameHeight - 1 Or x = 0 Or x = grpdata.GRPFrame(grpframe).frameWidth - 1 Then
                    '        pixels(xpos + ypos * bmpsize.Width) = 111
                    '    End If
                    'End If
                End If
            Next
        Next




        'MsgBox(grpframe & " : 을 그리기를 끝냅니다." & vbCrLf & grpdata.GRPFrame.Count & " : 만큼의 프레임을 가지고 있습니다.")
    End Sub


    Private Sub DrawRect(screenpos As Point, rect As Rectangle)
        'MsgBox(grpframe & " : 을 그리기 시작합니다." & vbCrLf & grpdata.GRPFrame.Count & " : 만큼의 프레임을 가지고 있습니다.")

        Dim xoffset As Short = screenpos.X - rect.Width \ 2
        Dim yoffset As Short = screenpos.Y - rect.Height \ 2

        For y = 0 To rect.Height - 1
            For x = 0 To rect.Width - 1
                Dim xpos, ypos As Integer

                xpos = x + xoffset
                ypos = y + yoffset


                If 0 <= xpos And xpos < bmpsize.Width And
                   0 <= ypos And ypos < bmpsize.Height Then


                    If ToolStripButton12.Checked = True Then
                        If y = 0 Or y = rect.Height - 1 Or x = 0 Or x = rect.Width - 1 Then
                            pixels(xpos + ypos * bmpsize.Width) = 111
                        End If
                    End If
                End If
            Next
        Next




        'MsgBox(grpframe & " : 을 그리기를 끝냅니다." & vbCrLf & grpdata.GRPFrame.Count & " : 만큼의 프레임을 가지고 있습니다.")
    End Sub

    Dim selectpos1 As Point
    Dim selectpos2 As Point
    Dim ismousemown As Boolean

    Private Sub Map_MouseClick(sender As Object, e As MouseEventArgs) Handles Map.MouseClick
        If e.Button = MouseButtons.Right Then
            GameData.OrderUnit(CGameData.order.attack_move, e.Location + scoll)
            BrushMode = BrushT.UnitControl
            BtnRefresh()
        End If
    End Sub

    Dim OldmapMouspos As Point
    Dim isdrag As Boolean = False
    Dim iscreatedrag As Boolean = False
    Private Sub Map_MouseDown(sender As Object, e As MouseEventArgs) Handles Map.MouseDown
        If e.Button = MouseButtons.Left Then
            If BrushMode = BrushT.UnitControl Then

                selectpos1 = e.Location + scoll
                selectpos2 = e.Location + scoll
                ismousemown = True

            ElseIf BrushMode = BrushT.UnitCreate Then
                GameData.CreateUnit(e.X + scoll.X, e.Y + scoll.Y, lastSelectindex, ToolStripComboBox1.SelectedIndex)
                '#0x58DBF0 = 트리거 명령코드

                '#0x58DBF4 = 트리거 X
                '#0x58DBF8 = 트리거 Y
                '#0x58DBFC = 유닛 종류
                '#0x58DC00 = 플레이어
                iscreatedrag = True
            End If
        End If


        If e.Button = MouseButtons.Middle Then
            OldmapMouspos = e.Location
            OldmapMouspos.X += scoll.X
            OldmapMouspos.Y += scoll.Y
            Map.Cursor = Cursors.NoMove2D
            isdrag = True
        End If
    End Sub

    Dim mousepos As Point
    Private Sub Map_MouseMove(sender As Object, e As MouseEventArgs) Handles Map.MouseMove
        mousepos = e.Location
        If e.Button = MouseButtons.Left Then
            If BrushMode = BrushT.UnitControl Then
                selectpos2 = e.Location + scoll
            End If
        End If


        If isdrag Then
            ScrollMap(OldmapMouspos.X - e.X, OldmapMouspos.Y - e.Y)
        End If
    End Sub

    Private Sub Map_MouseUp(sender As Object, e As MouseEventArgs) Handles Map.MouseUp
        If BrushMode = BrushT.UnitControl Then
            If e.Button = MouseButtons.Left Then
                selectpos2 = e.Location + scoll

                Dim mousx1 As Integer = Math.Min(selectpos1.X, selectpos2.X)
                Dim mousx2 As Integer = Math.Max(selectpos1.X, selectpos2.X)
                Dim mousy1 As Integer = Math.Min(selectpos1.Y, selectpos2.Y)
                Dim mousy2 As Integer = Math.Max(selectpos1.Y, selectpos2.Y)
                GameData.SelectUnits.Clear()
                For i = 0 To GameData.UnitNode.Count - 1
                    With GameData.UnitNode(i)
                        If mousx1 < .pos.X And .pos.X < mousx2 And
                           mousy1 < .pos.Y And .pos.Y < mousy2 Then
                            GameData.SelectUnits.Add(.dataNum)
                        End If
                    End With
                Next


                ismousemown = False
            End If
        End If
        iscreatedrag = False


        If isdrag Then
            Map.Cursor = Cursors.Default
            isdrag = False
        End If

    End Sub

    Private Sub ToolStripButton13_Click(sender As Object, e As EventArgs) Handles ToolStripButton13.Click
        BrushMode = BrushT.UnitControl
        BtnRefresh()
    End Sub

    Private Sub ToolStripButton14_Click(sender As Object, e As EventArgs) Handles ToolStripButton14.Click
        BrushMode = BrushT.UnitCreate
        GameData.SelectUnits.Clear()
        BtnRefresh()
    End Sub

    Dim lastSelectindex As UInt16 = 0
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        BrushMode = BrushT.UnitCreate
        GameData.SelectUnits.Clear()
        BtnRefresh()
        If ListView1.SelectedItems.Count <> 0 Then
            lastSelectindex = ListView1.SelectedItems(0).Tag
        Else

            'ListView1.Items(lastSelectindex).Selected = True
        End If
        TextBox1.Focus()
    End Sub
    Private Sub ListView1_GotFocus(sender As Object, e As EventArgs) Handles ListView1.GotFocus
        TextBox1.Focus()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        DebugCheatForm.Show()
        DebugCheatForm.Location = Location + New Point(505, 76)
    End Sub


    Private Sub 데스값ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 데스값ToolStripMenuItem.Click
        DethesViewerForm.Show()
    End Sub

    Private Sub 스위치상태ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 스위치상태ToolStripMenuItem.Click
        SwitchViewerForm.Show()
    End Sub

    Private Sub 플레이어정보ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 플레이어정보ToolStripMenuItem.Click
        PlayerdataForm.Show()
    End Sub

    Private Sub 게임정보ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 게임정보ToolStripMenuItem.Click
        GamedataForm.Show()
    End Sub
End Class