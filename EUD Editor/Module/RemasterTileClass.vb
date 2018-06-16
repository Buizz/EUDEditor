Imports System.IO
Imports Microsoft.Xna.Framework.Graphics


Public Class RemasterTileClass
    Public Function GetProgress() As Integer
        Return CurrentSize / MaxSize * 100
    End Function

    Private MaxSize As Integer
    Private CurrentSize As Integer

    Public isloadcmp As Boolean = False
    Public TileSets As New List(Of Texture2D)
    Private Datapath As String = ProgramSet.StarDirec.Replace(ProgramSet.StarDirec.Split("\").Last, "Data\data")

    Public Shared Function BitmapToTexture2D(GraphicsDevice As GraphicsDevice, image As Bitmap) As Texture2D
        Dim bufferSize As Integer = image.Height * image.Width * 4
        Dim memoryStream As New System.IO.MemoryStream(bufferSize)

        image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png)

        memoryStream.Seek(0, SeekOrigin.Begin)
        Dim texture As Texture2D = Texture2D.FromStream(GraphicsDevice, memoryStream, image.Height, image.Width, False)

        memoryStream.Close()
        Return texture
    End Function



    Public Sub Init(obj As GraphicsDevice)
        TileSetLoad(obj)

        isloadcmp = True
    End Sub


    Private Sub TileSetLoad(obj As GraphicsDevice)
        TileSets.Clear()

        Dim hStorage As IntPtr
        Dim hfile As IntPtr

        Dim memstream As New MemoryStream

        Dim bytewriter As New BinaryWriter(memstream)
        Dim bytereader As New BinaryReader(memstream)
        Dim Buffer(1024) As Byte

        Dim framecount As UInt16


        CascLib.CascOpenStorage(Datapath, &H200, hStorage)
        CascLib.CascOpenFile(hStorage, tilesetnameCasc(TileSetType), 0, &H1, hfile)


        While (True)
            Dim dwBytesRead As UInteger = 0
            CascLib.CascReadFile(hfile, Buffer, Buffer.Length, dwBytesRead)
            If (dwBytesRead = 0) Then
                Exit While
            End If
            bytewriter.Write(Buffer)

        End While
        memstream.Position = 0

        'Header:
        ' u32 filesize
        ' u16 frame count
        ' u16 unknown(File version?) - -value appears to always be 0x1001 in the files I've seen.

        'which Is immediately followed by a series of File Entries

        'File Entry
        ' u32 unk - -always zero?
        ' u16 width
        ' u16 height
        ' u32 Size
        ' u8[Size] DDS file

        bytereader.ReadUInt32()
        framecount = bytereader.ReadUInt16
        bytereader.ReadUInt16()
        MaxSize = framecount
        For i = 0 To framecount - 1
            CurrentSize = i

            bytereader.ReadUInt32()
            bytereader.ReadUInt16()
            bytereader.ReadUInt16()

            Dim size As UInt32 = bytereader.ReadUInt32()


            Dim filestream As New FileStream(My.Application.Info.DirectoryPath & "\Data\temp\tile" & i, FileMode.Create)

            filestream.Write(bytereader.ReadBytes(size), 0, size)

            filestream.Close()

            Dim tbmp As Bitmap = DevIL.DevIL.LoadBitmap(My.Application.Info.DirectoryPath & "\Data\temp\tile" & i)
            TileSets.Add(BitmapToTexture2D(obj, tbmp))

            File.Delete(My.Application.Info.DirectoryPath & "\Data\temp\tile" & i)
        Next




        CascLib.CascCloseFile(hfile)

        CascLib.CascCloseStorage(hStorage)

        bytereader.Close()
        bytewriter.Close()
        memstream.Close()
    End Sub






End Class
