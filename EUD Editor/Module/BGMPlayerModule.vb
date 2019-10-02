Imports System.Text

Module BGMPlayerModule
    Public Function GetBGMPlyereps() As String
        Dim interval As Double = 1050
        Dim dealy As Double = 70

        Dim str As New StringBuilder

        '폼을 열어서 사운드 길이 구하는 거야.
        SoundLenForm.ShowDialog()


        str.AppendLine("import tempcustomText as tct;")
        str.AppendLine()

        str.AppendLine("const musicLastTime = [0, 0, 0, 0, 0, 0, 0, 0];")
        str.AppendLine("const musicFrame = [0, 0, 0, 0, 0, 0, 0, 0];")
        str.AppendLine("const musicnum = [0, 0, 0, 0, 0, 0, 0, 0];")
        str.AppendLine("const musicflag = [0, 0, 0, 0, 0, 0, 0, 0];")
        str.AppendLine("const musicisplay = [0, 0, 0, 0, 0, 0, 0, 0];")

        'str.AppendLine()
        'str.AppendLine("Const intervalDeath = " & Soundinterval & ";")
        str.AppendLine()
        str.AppendLine("function Player() {")
        str.AppendLine("    var currentid = getcurpl();")
        str.AppendLine("    for (var i = 0 ; i < 8 ; i++) {")
        str.AppendLine("        if (musicisplay[i] == 1 && musicflag[i] != 2) {")
        str.AppendLine("            var playtime = musicLastTime[i] - dwread_epd(EPD(0x51CE8C));")
        str.AppendLine("            var LastFrame;")
        str.AppendLine("            const music_No = musicnum[i];")
        For musiccount = 0 To Soundlist.Count - 1
            str.AppendLine("        if (music_No == " & musiccount & ") {")
            '최대치 구하기.
            Dim index As Integer = 0
            While True
                Dim tempfoluder As String = My.Application.Info.DirectoryPath & "\Data\temp\"
                Dim output As String = tempfoluder & "M" & musiccount & "_"

                Dim _temp As String = output & index & ".ogg"
                If CheckFileExist(_temp) Then
                    Exit While
                End If

                index += 1
            End While
            str.AppendLine("            LastFrame = " & index & ";")
            str.AppendLine("        }")
        Next


        str.AppendLine("            if (musicFrame[i] < LastFrame) {")
        str.AppendLine("                if (playtime > " & Soundinterval * interval - dealy & ") {")
        str.AppendLine("                    tct.makeText('M');")
        str.AppendLine("")
        str.AppendLine("                    //뮤직 이름")
        str.AppendLine("                    const music_name = musicnum[i];")
        str.AppendLine("                    tct.addText(music_name);")
        str.AppendLine("")
        str.AppendLine("                    tct.addText('_');")
        str.AppendLine("")
        str.AppendLine("                    //세퍼레이트 이름")
        str.AppendLine("                    const music_frame = musicFrame[i];")
        str.AppendLine("                    tct.addText(music_frame);")
        str.AppendLine("")
        str.AppendLine("                    //마지막으로 재생된 playtime")
        str.AppendLine("")
        str.AppendLine("                    tct.addText('.ogg');")
        str.AppendLine("                    SetCurrentPlayer(i);")

        'str.AppendLine("                    DisplayText(2);")


        str.AppendLine("                    // PlayWAV(tct.strBuffer);")

        For musiccount = 0 To Soundlist.Count - 1
            str.AppendLine("                    if (music_name == " & musiccount & ") {")
            '최대치 구하기.
            Dim index As Integer = 0
            While True
                Dim tempfoluder As String = My.Application.Info.DirectoryPath & "\Data\temp\"
                Dim output As String = tempfoluder & "M" & musiccount & "_"

                Dim _temp As String = output & index & ".ogg"
                If CheckFileExist(_temp) Then
                    Exit While
                End If
                str.AppendLine("                    if(music_frame == " & index & ") {")
                str.AppendLine("                        PlayWAV('M" & musiccount & "_" & index & ".ogg');")
                str.AppendLine("                    }")
                index += 1
            End While
            str.AppendLine("                    }")
        Next

        str.AppendLine("                    musicFrame[i] = musicFrame[i] + 1;")
        str.AppendLine("                    musicLastTime[i] = dwread_epd(EPD(0x51CE8C));")
        str.AppendLine("                }")
        str.AppendLine("")
        str.AppendLine("            } else {")
        str.AppendLine("                //사운드 재생이 끝났을 경우")
        str.AppendLine("                if (musicflag[i] == 1) {")
        str.AppendLine("                    //반복 재생일 경우")
        str.AppendLine("                    musicFrame[i] = 0;")
        str.AppendLine("                    musicLastTime[i] = dwread_epd(EPD(0x51CE8C));")
        str.AppendLine("                }else if (musicflag[i] == 0) {")
        str.AppendLine("                    musicflag[i] = 2;")
        str.AppendLine("                }")
        str.AppendLine("            } ")
        str.AppendLine("        }")
        str.AppendLine("    }")
        str.AppendLine("    SetCurrentPlayer(currentid);")
        str.AppendLine("}")



        str.AppendLine()
        str.AppendLine("function parsePlayer(tplayer) {")
        str.AppendLine("    if (tplayer < 8) {")
        str.AppendLine("        return tplayer;")
        str.AppendLine("    }else {")
        str.AppendLine("        return getcurpl();")
        str.AppendLine("    }")
        str.AppendLine("}")
        str.AppendLine()
        str.AppendLine("function Play(tplayer,BGMNum, flag) {")
        str.AppendLine("    var p = parsePlayer(tplayer);")
        str.AppendLine("    if (flag == 0 && musicflag[p] == 0) {")
        str.AppendLine("        if (BGMNum != musicnum[p]) {")
        str.AppendLine("            musicLastTime[p] = dwread_epd(EPD(0x51CE8C)) + " & Soundinterval * interval & " ;")
        str.AppendLine("            musicFrame[p] = 0;")
        str.AppendLine("        }")
        str.AppendLine("        musicnum[p] = BGMNum;")
        str.AppendLine("        musicflag[p] = flag;")
        str.AppendLine("        musicisplay[p] = 1;")
        str.AppendLine("    }else {")
        str.AppendLine("        musicLastTime[p] = dwread_epd(EPD(0x51CE8C)) + " & Soundinterval * interval & ";")
        str.AppendLine("        musicFrame[p] = 0;")
        str.AppendLine("        musicnum[p] = BGMNum;")
        str.AppendLine("        musicflag[p] = flag;")
        str.AppendLine("        musicisplay[p] = 1;")
        str.AppendLine("    }")
        str.AppendLine("}")
        str.AppendLine()
        str.AppendLine("function Stop(tplayer) {")
        str.AppendLine("    var p = parsePlayer(tplayer);")
        str.AppendLine("    musicisplay[p] = 0;")
        str.AppendLine("}")
        str.AppendLine()
        str.AppendLine("function Resume(tplayer) {")
        str.AppendLine("    var p = parsePlayer(tplayer);")
        str.AppendLine("    musicisplay[p] = 1;")
        str.AppendLine("}")
        str.AppendLine()
        str.AppendLine("function CurrentPlayMusic(tplayer) {")
        str.AppendLine("    var p = parsePlayer(tplayer);")
        str.AppendLine("    return musicnum[p];")
        str.AppendLine("}")
        str.AppendLine()
        str.AppendLine("function isplaying(tplayer) {")
        str.AppendLine("    var p = parsePlayer(tplayer);")
        str.AppendLine("    return musicflag[p];")
        str.AppendLine("}")



        Return str.ToString
    End Function
End Module
