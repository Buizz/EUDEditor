Imports System.IO
Imports System.Drawing.Image
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices


Module SMKModule
    Public Class SMK
        Private Palett(255) As Color


        Public framecount As UInteger
        Private grpWidth As UInteger
        Private grpHeight As UInteger
        Public GRPFrame As New List(Of GRPFrameData)

        Structure GRPFrameData
            Public frameXOffset As Byte
            Public frameYOffset As Byte
            Public frameWidth As Byte
            Public frameHeight As Byte
            Public Image() As Byte
            Public IineTableOffset As UInteger
        End Structure


        Public Function LoadGRP(Filename As String)
            Dim filestream As New FileStream(Filename, FileMode.Open)
            Dim binaryreader As New BinaryReader(filestream)

            filestream.Position = 0
            framecount = binaryreader.ReadUInt16()
            grpWidth = binaryreader.ReadUInt16()
            grpHeight = binaryreader.ReadUInt16()
            For i = 0 To framecount - 1
                Dim TempGRP As New GRPFrameData
                TempGRP.frameXOffset = binaryreader.ReadByte()
                TempGRP.frameYOffset = binaryreader.ReadByte()
                TempGRP.frameWidth = binaryreader.ReadByte()
                TempGRP.frameHeight = binaryreader.ReadByte()
                TempGRP.IineTableOffset = binaryreader.ReadUInt32()
                GRPFrame.Add(TempGRP)
            Next


            binaryreader.Close()
            filestream.Close()
            Return 0
        End Function

        Public Function LoadGRP(buffer As Byte())
            Dim memreader As New MemoryStream(buffer)
            Dim binaryreader As New BinaryReader(memreader)


            memreader.Position = 0
            framecount = binaryreader.ReadUInt16()
            grpWidth = binaryreader.ReadUInt16()
            grpHeight = binaryreader.ReadUInt16()

            'MsgBox(framecount)
            Dim framePos As UInteger = memreader.Position
            For i = 0 To framecount - 1 '프레임 수만큼.
                Dim TempGRP As GRPFrameData
                memreader.Position = framePos
                TempGRP.frameXOffset = binaryreader.ReadByte()
                TempGRP.frameYOffset = binaryreader.ReadByte()
                TempGRP.frameWidth = binaryreader.ReadByte()
                TempGRP.frameHeight = binaryreader.ReadByte()
                TempGRP.IineTableOffset = binaryreader.ReadUInt32()
                framePos = memreader.Position



                Dim tempimage() As Byte

                Dim size As UInteger
                If TempGRP.frameWidth Mod 4 = 0 Then
                    size = CInt(TempGRP.frameWidth) * CInt(TempGRP.frameHeight) - 1
                Else
                    size = CInt(TempGRP.frameWidth) * CInt(TempGRP.frameHeight) - 1 + (4 - (TempGRP.frameWidth Mod 4)) * CInt(TempGRP.frameHeight)
                End If

                ReDim tempimage(size)

                Dim tempimageindex As Integer
                tempimageindex = 0

                Dim opcode As UInteger
                Dim nextcode As UInteger
                Dim count As Integer
                Dim temp As UInteger
                For j = 0 To TempGRP.frameHeight - 1 '가로 줄 수.
                    If TempGRP.frameWidth Mod 4 <> 0 Then
                        tempimageindex = (TempGRP.frameWidth + 4 - (TempGRP.frameWidth Mod 4)) * j
                    End If

                    'MsgBox()
                    memreader.Position = TempGRP.IineTableOffset + j * 2
                    temp = binaryreader.ReadUInt16() '상대적 좌표.
                    memreader.Position = TempGRP.IineTableOffset + temp '실제 라인 데이터
                    'IineTableOffset으로 부터 Y만큼 2바이트씩 그게.각 가로줄 하나.
                    '가로줄 하나는 세로줄의 길이를 가지고 있음.
                    'Grpdata(TempGRP.IineTableOffset)

                    'Byte >= 0x80 : (byte - 0x80)만큼 0을 출력
                    '0x80 > byte >= 0x40 : (byte - 0x40)만큼 다음 바이트를 반복해서 출력
                    '0x40 > byte : 다음 byte만큼의 byte를 그대로 출력

                    'MsgBox("Line " & j)
                    count = 0
                    While (True)
                        opcode = binaryreader.ReadByte()

                        'MsgBox(count & " before " & TempGRP.frameWidth & " " & Format(opcode, "X"))
                        If opcode >= &H80 Then
                            For k As Integer = 1 To (opcode - &H80)
                                tempimage(tempimageindex) = 0
                                tempimageindex += 1
                                count += 1
                            Next
                        ElseIf &H80 > opcode And opcode >= &H40 Then
                            nextcode = binaryreader.ReadByte()
                            For k As Integer = 1 To (opcode - &H40)
                                tempimage(tempimageindex) = nextcode
                                tempimageindex += 1
                                count += 1
                            Next
                        ElseIf &H40 > opcode Then
                            For k As Integer = 1 To opcode
                                tempimage(tempimageindex) = binaryreader.ReadByte()
                                tempimageindex += 1
                                count += 1
                            Next
                        End If
                        'MsgBox(count & " after " & TempGRP.frameWidth)
                        If count = TempGRP.frameWidth Then

                            Exit While
                        End If
                    End While

                Next

                TempGRP.Image = tempimage


                GRPFrame.Add(TempGRP)
            Next

            memreader.Close()
            binaryreader.Close()

            Return 0
        End Function

        Public Function DrawGRP(frame As Integer, Optional Unitcolor As Integer = 0, Optional FileBackGround As Boolean = False) As Bitmap
            'GRPFrame(frame).frameWidth
            'GRPFrame(frame).frameHeight
            Dim bm As New Bitmap(GRPFrame(frame).frameWidth, GRPFrame(frame).frameHeight, Imaging.PixelFormat.Format8bppIndexed)



            Dim bmd As New BitmapData
            bmd = bm.LockBits(New Rectangle(0, 0, bm.Width, bm.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format8bppIndexed)

            Dim scan0 As IntPtr = bmd.Scan0
            Dim stride As Integer = bmd.Stride

            Dim pixels(GRPFrame(frame).Image.Length - 1) As Byte
            Marshal.Copy(scan0, pixels, 0, pixels.Length)

            ' MsgBox(pixels.Length & " " & GRPFrame(frame).Image.Length)


            '138이 남는다.

            pixels = GRPFrame(frame).Image


            Marshal.Copy(pixels, 0, scan0, pixels.Length)

            bm.UnlockBits(bmd)


            Dim CPalette As Imaging.ColorPalette
            CPalette = bm.Palette
            For i = 0 To 255
                CPalette.Entries(i) = Palett(i)

            Next
            bm.Palette = CPalette

            If FileBackGround = True Then
                bm.MakeTransparent(Color.Black)
            End If

            Return bm
        End Function


        Public Sub DrawToPictureBox(ByRef pictureBox As PictureBox, frame As Integer, Optional Unitcolor As Integer = 0)
            Dim bitmap As New Bitmap(grpWidth, grpHeight, System.Drawing.Imaging.PixelFormat.Format32bppRgb)
            Dim grp As Graphics
            grp = Graphics.FromImage(bitmap)

            Dim tempbmp As Bitmap = DrawGRP(frame)

            Dim temppoint As New Point((grpWidth - tempbmp.Width) \ 2, (grpHeight - tempbmp.Height) \ 2)
            grp.DrawImage(tempbmp, temppoint)

            pictureBox.Image = bitmap

        End Sub



        'Public Function DrawGRP(frame As Integer, Optional Unitcolor As Integer = 0) As Bitmap
        '    'GRPFrame(frame).frameWidth
        '    'GRPFrame(frame).frameHeight

        '    Dim bm As New Bitmap(GRPFrame(frame).frameWidth, GRPFrame(frame).frameHeight, Imaging.PixelFormat.Format8bppIndexed)



        '    Dim bmd As New BitmapData
        '    bmd = bm.LockBits(New Rectangle(0, 0, bm.Width, bm.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format8bppIndexed)

        '    Dim scan0 As IntPtr = bmd.Scan0
        '    Dim stride As Integer = bmd.Stride

        '    Dim pixels(GRPFrame(frame).Image.Length - 1) As Byte
        '    Marshal.Copy(scan0, pixels, 0, pixels.Length)

        '    ' MsgBox(pixels.Length & " " & GRPFrame(frame).Image.Length)


        '    '138이 남는다.

        '    pixels = GRPFrame(frame).Image


        '    Marshal.Copy(pixels, 0, scan0, pixels.Length)

        '    bm.UnlockBits(bmd)


        '    Dim CPalette As Imaging.ColorPalette
        '    CPalette = bm.Palette
        '    For i = 0 To 255
        '        CPalette.Entries(i) = Palett(i)

        '    Next
        '    bm.Palette = CPalette



        '    Return bm
        'End Function



        Public Function Reset()
            framecount = 0
            grpWidth = 0
            grpHeight = 0
            GRPFrame.Clear()

            Return 0
        End Function

    End Class
End Module
