Imports System.IO
Imports System.Text

Module SoundPlayerModule
    Private CurrentSoundNum As Integer

    'The retVal's value is not used for anything specific in this article.
    Dim _retVal As Integer

    'Will hold the file path
    Dim _filename As String

    Dim returnstring As StringBuilder


    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As StringBuilder, ByVal uReturnLength As Integer, ByVal hwndCallback As IntPtr) As Integer

    Public Sub SoundPlayer(Filebuffer() As Byte)
        'returnstring = New StringBuilder(128)

        'Dim soundnum As Integer = 0

        '_retVal = mciSendString("status songNumber" & soundnum & " mode", returnstring, 128, 0)

        'While Mid(returnstring.ToString, 1, 7) = "playing"
        '    _retVal = mciSendString("status songNumber" & soundnum & " mode", returnstring, 128, 0)
        '    soundnum += 1
        'End While
        'MsgBox("songNumber" & soundnum & " " & Mid(returnstring.ToString, 1, 7))


        _retVal = mciSendString("close songNumber" & CurrentSoundNum, Nothing, 0, 0)

        Dim tname As String = My.Application.Info.DirectoryPath & "\Data\temp\sound" & CurrentSoundNum & ".wav"
        Dim filestream As New FileStream(tname, FileMode.Create)
        Dim binaryst As New BinaryWriter(filestream)


        binaryst.Write(Filebuffer)

        binaryst.Close()
        filestream.Close()


        _filename = Chr(34) & tname & Chr(34)

        _retVal = mciSendString("close songNumber" & CurrentSoundNum, Nothing, 0, 0)

        _retVal = mciSendString("open " & _filename & " type mpegvideo alias songNumber" & CurrentSoundNum, Nothing, 0, 0)

        _retVal = mciSendString("play songNumber" & CurrentSoundNum, Nothing, 0, 0)

        CurrentSoundNum += 1
        If CurrentSoundNum > 9 Then
            CurrentSoundNum = 0
        End If
    End Sub

End Module
