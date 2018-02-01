Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging

Public Class LoadTileSetSingleForm

    Private Sub Map_SizeChanged(sender As Object, e As EventArgs) Handles PaintPal.SizeChanged
        RefreshBMP()
    End Sub
    Private Sub LoadTileSetSingleForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lan.SetLangage(Me)

        dbool = False
        TabMove.SelectedIndex = 3
        TabHeight.SelectedIndex = 3
        dbool = True


        LoadImagedata()


        'ProjectTileSetData.Clear()
        'ProjectTileSetData.Add(New CTileSet("C:\Users\skslj\Desktop\DarkBlock.bmp", 0, True))
        'ProjectTileSetData.Add(New CTileSet("C:\Users\skslj\Desktop\Siver.bmp", 1, True))
        'ProjectTileSetData.Add(New CTileSet("C:\Users\skslj\Desktop\Dark2.bmp", 2, True))
        'ProjectTileSetData.Add(New CTileSet("C:\Users\skslj\Desktop\Light2.bmp", 3, True))
        'ProjectTileSetData.Add(New CTileSet("C:\Users\skslj\Desktop\LightBlock.bmp", 4, True))
        RefreshBMP()
    End Sub



    Public SelectPal1 As UInteger
    Private palbmp As Bitmap
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

                Dim movingFlag As Byte = ProjectTIleMSet(tilenum + 8 * (VScrollBar2.Value \ 32))
                Select Case movingFlag
                    Case 0
                        For y = 0 To 31
                            If 0 <= (y - transY + j * 32) And (y - transY + j * 32) < palbmp.Height Then
                                pixelsstream.Position = (y - transY) * 32 * 8 + i * 32 + j * 32 * 32 * 8
                                memstream.Position = y * 32
                                binarywriter.Write(binaryreader.ReadBytes(32))
                            End If
                        Next
                    Case Else
                        Dim colorb As Byte
                        Select Case (movingFlag - 1) \ 3
                            Case 0
                                colorb = 255
                            Case 1
                                colorb = 117
                            Case 2
                                colorb = 111
                        End Select



                        Dim tab(32) As Byte
                        For p = 0 To tab.Length - 1
                            tab(p) = colorb
                        Next

                        For y = 0 To 31
                            If 0 <= (y - transY + j * 32) And (y - transY + j * 32) < palbmp.Height Then
                                If y = 0 Or y = 31 Then
                                    pixelsstream.Position = (y - transY) * 32 * 8 + i * 32 + j * 32 * 32 * 8
                                    binarywriter.Write(tab)
                                Else
                                    pixelsstream.Position = (y - transY) * 32 * 8 + i * 32 + j * 32 * 32 * 8
                                    memstream.Position = y * 32 + 1
                                    binarywriter.Write(CByte(colorb))
                                    binarywriter.Write(binaryreader.ReadBytes(30))
                                    binarywriter.Write(CByte(colorb))
                                End If
                            End If
                        Next


                        For in2 = 0 To ((movingFlag - 1) Mod 3)
                            Dim x, y As UInteger
                            For ind = 0 To 10
                                x = 10 + in2 * 3
                                y = 10 + ind
                                If 0 <= (y - transY + j * 32) And (y - transY + j * 32) < palbmp.Height Then
                                    pixelsstream.Position = (y - transY) * 32 * 8 + i * 32 + j * 32 * 32 * 8 + x
                                    binarywriter.Write(CUShort(colorb + colorb * 256))

                                End If
                            Next
                        Next


                End Select

                binaryreader.Close()
                memstream.Close()
                tilenum += 1
            Next


        Next




        Dim MaxX As Integer = (SelectPal1 Mod 8) * 32
        Dim MaxY As Integer = (SelectPal1 \ 8) * 32 - VScrollBar2.Value

        Dim MinX As Integer = (SelectPal1 Mod 8) * 32
        Dim MinY As Integer = (SelectPal1 \ 8) * 32 - VScrollBar2.Value

        Dim a(MaxX - MinX + 32 - 1) As Byte
        For i = 0 To a.Length - 1
            a(i) = 255
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
                    pixels(MinX + (i + MinY) * 32 * 8 + j) = 255

                    pixels(MaxX + 31 - j + (i + MinY) * 32 * 8) = 255
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


    Private Sub RefreshBMP()

        palbmp = New Bitmap(8 * 32, PaintPal.Height, PixelFormat.Format8bppIndexed)






        Dim CPalette As Imaging.ColorPalette
        CPalette = palbmp.Palette
        For i = 0 To 255
            If 15 >= i And i >= 8 Then
                CPalette.Entries(i) = MapPalett(i) '흔들리는 색
            Else
                CPalette.Entries(i) = MapPalett(i)
            End If

        Next
        palbmp.Palette = CPalette






        VScrollBar2.Maximum = (TilebitDATA.Count \ 8) * 32

        VScrollBar2.LargeChange = PaintPal.Size.Height


        If (VScrollBar2.Value + VScrollBar2.LargeChange) > VScrollBar2.Maximum Then
            VScrollBar2.Value = VScrollBar2.Maximum - VScrollBar2.LargeChange
        End If


        GetPalletImage()
    End Sub



    Dim ispaldarg As Boolean
    Private Sub PaintPal_MouseDown(sender As Object, e As MouseEventArgs) Handles PaintPal.MouseDown
        If e.Button = MouseButtons.Left Then
            SelectPal1 = (e.X \ 32) + ((e.Y + VScrollBar2.Value) \ 32) * 8

            If TabMove.SelectedIndex <> 3 Then
                ProjectTIleMSet(SelectPal1) = (TabMove.SelectedIndex) * 3 + (TabHeight.SelectedIndex) + 1
                'MsgBox(SelectPal1 & ", " & ProjectTIleMSet(SelectPal1))
            Else
                ProjectTIleMSet(SelectPal1) = 0
            End If
            'SelectPal
            GetPalletImage()
            PalletTimer.Enabled = True
            ispaldarg = True
        ElseIf e.Button = MouseButtons.right Then
            SelectPal1 = (e.X \ 32) + ((e.Y + VScrollBar2.Value) \ 32) * 8
            'SelectPal
            GetPalletImage()

        End If
        LoadImagedata()
    End Sub
    Private Sub PaintPal_MouseMove(sender As Object, e As MouseEventArgs) Handles PaintPal.MouseMove
        If e.Button = MouseButtons.Left Then

            SelectPal1 = (e.X \ 32) + ((e.Y + VScrollBar2.Value) \ 32) * 8
            'SelectPal
            If TabMove.SelectedIndex <> 3 Then
                ProjectTIleMSet(SelectPal1) = (TabMove.SelectedIndex) * 3 + (TabHeight.SelectedIndex) + 1
                'MsgBox(SelectPal1 & ", " & ProjectTIleMSet(SelectPal1))
            Else
                ProjectTIleMSet(SelectPal1) = 0
            End If
            LoadImagedata()
        End If

    End Sub
    Private Sub PaintPal_MouseUp(sender As Object, e As MouseEventArgs) Handles PaintPal.MouseUp
        If e.Button = MouseButtons.Left Then

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

    Dim dbool As Boolean = True
    Private Sub TabDefacult_Click(sender As Object, e As EventArgs) Handles TabDefacult.Click
        dbool = False
        TabMove.SelectedIndex = 3
        TabHeight.SelectedIndex = 3
        dbool = True
    End Sub

    Private Sub TabMove_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabMove.SelectedIndexChanged
        If dbool = True Then
            If TabHeight.SelectedIndex = 3 Then
                TabHeight.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub TabHeight_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabHeight.SelectedIndexChanged
        If dbool = True Then
            If TabMove.SelectedIndex = 3 Then
                TabMove.SelectedIndex = 0
            End If
        End If
    End Sub

    Dim comp1 As Boolean = True
    Private Sub LoadImagedata()
        comp1 = False
        TextBox1.Text = ProjectTileSetFileName
        If ProjectTileUseFile = True Then
            RadioButton1.Checked = True
            GroupBox1.Enabled = True
            Panel2.Enabled = False
        Else
            RadioButton2.Checked = True
            GroupBox1.Enabled = False
            Panel2.Enabled = True


            Dim isexist As Boolean = False
            For i = 0 To ProjectTileSetData.Count - 1
                If ProjectTileSetData(i).TileSetNum = SelectPal1 Then
                    GroupBox6.Enabled = True
                    Button3.Enabled = True
                    RadioButton3.Checked = ProjectTileSetData(i).isMaker

                    RadioButton4.Checked = Not ProjectTileSetData(i).isMaker

                    isexist = True
                    Exit For
                End If
            Next
            If isexist = False Then
                Button3.Enabled = False
                GroupBox6.Enabled = False
            End If
        End If
        comp1 = True
    End Sub
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If comp1 = True Then
            ProjectTileUseFile = RadioButton1.Checked
            LoadImagedata()
            LoadTILEDATA()
            GetPalletImage()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If comp1 = True Then
            ProjectTileUseFile = RadioButton1.Checked
            LoadImagedata()
            LoadTILEDATA()
            GetPalletImage()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dialog As DialogResult
        dialog = OpenBMP.ShowDialog

        If dialog = DialogResult.OK Then
            ProjectTileSetFileName = OpenBMP.FileName
            TextBox1.Text = OpenBMP.FileName
            LoadTILEDATA()
            GetPalletImage()
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If comp1 = True Then
            For i = 0 To ProjectTileSetData.Count - 1
                If ProjectTileSetData(i).TileSetNum = SelectPal1 Then
                    ProjectTileSetData(i).isMaker = RadioButton3.Checked
                    Exit For
                End If
            Next

            LoadTILEDATA()
            GetPalletImage()
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If comp1 = True Then
            For i = 0 To ProjectTileSetData.Count - 1
                If ProjectTileSetData(i).TileSetNum = SelectPal1 Then
                    ProjectTileSetData(i).isMaker = RadioButton3.Checked
                    Exit For
                End If
            Next

            LoadTILEDATA()
            GetPalletImage()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For i = 0 To ProjectTileSetData.Count - 1
            If ProjectTileSetData(i).TileSetNum = SelectPal1 Then
                ProjectTileSetData.RemoveAt(i)
                Exit For
            End If
        Next
        LoadImagedata()
        LoadTILEDATA()
        GetPalletImage()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dialog As DialogResult
        dialog = OpenBMP.ShowDialog

        If dialog = DialogResult.OK Then
            ProjectTileSetData.Add(New CTileSet(OpenBMP.FileName, SelectPal1, False))

            LoadImagedata()
            LoadTILEDATA()
            GetPalletImage()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim dialog As DialogResult
        dialog = SaveFileDialog1.ShowDialog

        If dialog = DialogResult.OK Then
            Dim fileCreator As New FileStream(SaveFileDialog1.FileName, FileMode.Create)
            Dim binaryw As New BinaryWriter(fileCreator)

            binaryw.Write({&H42, &H4D, &H38, &H8, &H0, &H0, &H0, &H0, &H0, &H0, &H36, &H4, &H0, &H0, &H28, &H0, &H0, &H0, &H20, &H0, &H0, &H0, &H20, &H0, &H0, &H0, &H1, &H0, &H8, &H0, &H0, &H0, &H0, &H0, &H2, &H4, &H0, &H0, &H12, &HB, &H0, &H0, &H12, &HB, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0})

            For i = 0 To 255
                binaryw.Write(palbmp.Palette.Entries(i).ToArgb)
            Next

            For y = 31 To 0 Step -1
                For x = 0 To 31
                    binaryw.Write(TilebitDATA(SelectPal1)(x + y * 32))
                Next
            Next
            binaryw.Write(CUShort(0))

            binaryw.Close()
            fileCreator.Close()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ProjectTileSetFileName = ""
        TextBox1.Text = ""
        LoadTILEDATA()
        GetPalletImage()
    End Sub
End Class