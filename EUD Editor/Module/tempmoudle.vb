Imports System.IO

Module tempmoudle
    '[
    '  {
    '    "Name": "RawCode",
    '    "Text": "코드 : $RawString$",
    '    "CodeText": "$RawString$",
    '    "ToolTip": "None",
    '    "ValuesDef": [
    '      "RawString"
    '    ]
    '  }
    ']
    Public Sub OutPutEUDTriggers()
        Dim filestrema As New FileStream("C:\Users\skslj\Desktop\EUD.txt", FileMode.Create)
        Dim strwriter As New StreamWriter(filestrema)

        'DatEditDATA(0).data(0)(0)
        'For i = 0 To 10
        '    strwriter.WriteLine(DatEditDATA(0).keyDic.Keys(i).ToString())
        '    strwriter.WriteLine(DatEditDATA(0).)
        'Nex

        strwriter.WriteLine("[")
        For k = 0 To 0 'DatEditDATA.Count - 1
            For i = 0 To 5 'DatEditDATA(k).data.Count - 1
                Dim Offsetname As String = DatEditDATA(k).typeName & "_" & DatEditDATA(k).keyDic.Keys.ToList(i)
                Dim typeName As String = DatEditDATA(k).keyDic.Keys.ToList(i)


                Dim _size As Integer = DatEditDATA(k).keyINFO(i).realSize

                Dim _offset As Long = Val("&H" & ReadOffset(Offsetname))


                '유닛의 땡떙을 뭐로 설정합니다.
                Dim name As String = typeName
                Dim text As String = "$" & DatEditDATA(k).typeName & "$의 $벨류$를 $Modifier$합니다."
                Dim codetext As String = "dwwrite(0x666666)"
                Dim tooltip As String = ""

                strwriter.WriteLine("   {")
                strwriter.WriteLine("       ""Name"": """ & name & """,")
                strwriter.WriteLine("       ""Text"": """ & text & """,")
                strwriter.WriteLine("       ""CodeText"": """ & codetext & """,")
                strwriter.WriteLine("       ""ToolTip"": """ & tooltip & """,")
                strwriter.WriteLine("       ""ValuesDef"": [,")
                strwriter.WriteLine("           """ & DatEditDATA(k).typeName & """,")
                strwriter.WriteLine("           ""벨류"",")
                strwriter.WriteLine("           ""Modifier""")
                strwriter.WriteLine("       ]")
                strwriter.WriteLine("   },")
            Next
        Next


        strwriter.WriteLine("]")


        strwriter.Close()
        filestrema.Close()

        MsgBox("끝")
        Main.Close()
    End Sub
End Module
