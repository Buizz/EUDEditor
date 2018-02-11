Imports System.Text

Module BGMPlayerModule
    Public Function GetBGMPlyereps() As String
        Dim str As New StringBuilder

        '폼을 열어서 사운드 길이 구하는 거야.
        SoundLenForm.ShowDialog()

        '1050ms당 데스값
        Dim intervaldeath As Double = 24
        '100/4.2

        str.AppendLine("import tempcustomText as tct;")
        str.AppendLine()

        str.AppendLine("const musicFrame = [0, 0, 0, 0, 0, 0, 0, 0];")
        str.AppendLine("const musicnum = [0, 0, 0, 0, 0, 0, 0, 0];")
        str.AppendLine("const musicflag = [0, 0, 0, 0, 0, 0, 0, 0];")
        str.AppendLine("const musicisplay = [0, 0, 0, 0, 0, 0, 0, 0];")

        str.AppendLine("var intervaldeath = 0;")
        'str.AppendLine()
        'str.AppendLine("Const intervalDeath = " & Soundinterval & ";")
        str.AppendLine()

        str.AppendLine("function Player() {")
        str.AppendLine("    if(")
        str.AppendLine("        ElapsedTime(AtMost, 10)")
        str.AppendLine("    ){")
        str.AppendLine("        intervaldeath = dwread_epd(EPD(0x58CE20));")
        str.AppendLine("        if (intervaldeath == 0) {")
        str.AppendLine("            intervaldeath = 25;")
        str.AppendLine("        }")
        'str.AppendLine("        intervaldeath = intervaldeath - intervaldeath / 20;")
        str.AppendLine("    }")

        str.AppendLine("    var currentid = getcurpl();")
        str.AppendLine("    for (var i = 0 ; i < 8 ; i++) {")
        str.AppendLine("        if (musicisplay[i] == 1 && musicflag[i] != 2) {")
        str.AppendLine("            SetCurrentPlayer(i);")
        For musiccount = 0 To Soundlist.Count - 1


            str.AppendLine("            if (musicnum[i] == " & musiccount & ") {")

            '최대치 구하기.
            Dim index As Integer = 0

            While True
                Dim tempfoluder As String = My.Application.Info.DirectoryPath & "\Data\temp\"
                Dim output As String = tempfoluder & "Music" & musiccount & "_"

                Dim _temp As String = output & index & ".ogg"
                If CheckFileExist(_temp) Then
                    Exit While
                End If


                str.AppendLine("                if (musicFrame[i] == intervaldeath * " & Math.Floor(Soundinterval * index) & ") {")
                str.AppendLine("                    //웨이브 재생")
                str.AppendLine("                    tct.makeText('Music" & musiccount & "_" & index & ".ogg');")
                str.AppendLine("                    PlayWAV(2);")
                'str.AppendLine("                    PlayWAV('Music" & musiccount & "_" & index & ".ogg');")
                str.AppendLine("                }")

                index += 1
            End While


            str.AppendLine("                if(musicFrame[i] > intervaldeath * " & Math.Floor(Soundinterval * index) & ") {")
            str.AppendLine("                    //사운드 재생이 끝났을 경우.")
            str.AppendLine("                    if (musicflag[i] == 1) {")
            str.AppendLine("                        //반복 재생일 경우")
            str.AppendLine("                        musicFrame[i] = 0;")
            str.AppendLine("                    }else if (musicflag[i] == 0) {")
            str.AppendLine("                        musicflag[i] = 2;")
            str.AppendLine("                    }")
            str.AppendLine("                }")
            str.AppendLine("            }")

        Next
        str.AppendLine("            musicFrame[i] = musicFrame[i] + 1;")
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
        str.AppendLine("            musicFrame[p] = 0;")
        str.AppendLine("        }")
        str.AppendLine("        musicnum[p] = BGMNum;")
        str.AppendLine("        musicflag[p] = flag;")
        str.AppendLine("        musicisplay[p] = 1;")
        str.AppendLine("    }else {")
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
        str.AppendLine("function CurrentPlayMusic(tplayer, musuicnum) {")
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
