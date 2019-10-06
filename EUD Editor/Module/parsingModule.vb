Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Module parsingModule
    Public TEErrorText As String
    Public TEErrorText2 As String


    '[Error 6298] Module "TriggerEditor" Line 18 : Block Not terminated properly.
    '[Error 6298] Module "TriggerEditor" Line 31 : Block Not terminated properly.
    '[Error 6974] Module "TriggerEditor" Line 35 : Error while parsing statement




    Public Function ValueString(str As String) As String()
        Dim index As Integer
        Dim lastindex As Integer = 1
        Dim returnval As New List(Of String)

        Dim stack As New Stack(Of String)


        While (index < str.Length)
            Select Case str(index)
                Case "("
                    stack.Push(str(index))
                Case ")"
                    stack.Pop()
                Case ","
                    If stack.Count = 0 Then
                        index += 1
                        returnval.Add(Mid(str, lastindex, index - lastindex))
                        lastindex = index + 1
                    End If
            End Select


            index += 1
        End While
        returnval.Add(Mid(str, lastindex, index - lastindex + 1))




        Return returnval.ToArray
    End Function




    Public Function GettblLen(str As String, index As Byte) As Byte
        Dim rawstr As String = replacetext(str)
        Dim i As Byte = 0
        Dim codeindex As Integer = 0
        Dim shopindex As Byte = 0
        Dim returnvar As Byte
        Dim shoplen As Byte = 0
        While (i < rawstr.Count)
            shoplen = 0
            If rawstr(i) = "#" Then
                returnvar = codeindex
                i += 1
                shoplen += 1
                codeindex += 1
                While (rawstr(i) = "#")
                    shoplen += 1
                    i += 1
                    codeindex += 1
                End While
                shopindex += 1
            End If

            If shopindex = index Then
                Exit While
            End If

            If AscW(rawstr(i)) <= &HFF Then
                codeindex += 1
            Else
                codeindex += 2
            End If
            i += 1
        End While
        Return shoplen
    End Function
    Public Function GettblStart(str As String, index As Byte) As Byte
        Dim rawstr As String = replacetext(str)
        Dim i As Byte = 0
        Dim codeindex As Integer = 0
        Dim shopindex As Byte = 0
        Dim returnvar As Byte
        Dim shoplen As Byte = 0
        While (i < rawstr.Count)
            If rawstr(i) = "#" Then
                returnvar = codeindex
                i += 1
                codeindex += 1
                While (rawstr(i) = "#")
                    shoplen += 1
                    i += 1
                    codeindex += 1
                End While
                shopindex += 1
            End If

            If shopindex = index Then
                Exit While
            End If

            If AscW(rawstr(i)) <= &HFF Then
                codeindex += 1
            Else
                codeindex += 2
            End If
            i += 1
        End While
        Return returnvar
    End Function





    Public Function GetSafeName(str As String) As String
        Return str.Split("\").Last
    End Function



    Public Function CheckGRPFile(grpname As String) As Boolean
        Dim checkgrp As New GRP
        If checkgrp.LoadGRP(grpname) = False Then
            MsgBox("정상적인 GRP파일이 아닙니다.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            Return False
        End If
        Return True
    End Function


    Public Function parsereqCode(opcode As UInt16, Optional vtype As Byte = 0) As String


        Dim returnvalue As String

        If opcode > &HFF Then
            If opcode = &HFFFF Then
                returnvalue = reqOpcode(0)
            Else
                Try
                    returnvalue = reqOpcode(opcode - &HFF00)
                Catch ex As Exception
                    returnvalue = "해석할 수 없는 OPcode입니다."
                End Try
            End If
        Else
            Try
                returnvalue = CODE(vtype)(opcode)
            Catch ex As Exception
                returnvalue = CODE(vtype)(0)
            End Try
        End If
        Return returnvalue
    End Function





    Public Sub CheckMapFile()
        If ProjectSet.isload = True Then
            If CheckFileExist(ProjectSet.InputMap) Then
                MsgBox("다음 맵은 존재하지 않습니다! 다시 설정해 주세요." & vbCrLf & ProjectSet.InputMap, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                While True
                    If SettingForm.SetInputMap() Then
                        Exit While
                    End If
                End While
            End If

            Dim len As UInteger = ProjectSet.OutputMap.Split("\").Count
            Dim filename As String = ProjectSet.OutputMap.Split("\")(len - 1)
            filename = Replace(ProjectSet.OutputMap, filename, "")
            filename = Mid(filename, 1, filename.Length - 1)


            If System.IO.Directory.Exists(filename) = False Then
                MsgBox("다음 맵은 존재하지 않습니다! 다시 설정해 주세요." & vbCrLf & ProjectSet.OutputMap, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                While True
                    If SettingForm.SetOutputMap() Then
                        Exit While
                    End If
                End While
            End If



            Dim str As String = "다음에 해당하는 데이터들이 존재하지 않습니다. 모두 사용안함으로 전환됩니다." & vbCrLf
            Dim isNoexist As Boolean = False

            If CheckFileExist(ProjectTileSetFileName) And ProjectTileSetFileName <> "" Then

                str = str & "TileSet : " & vbCrLf & ProjectTileSetFileName & vbCrLf
                ProjectTileSetFileName = ""
                ProjectTileUseFile = False
                LoadTILEDATA()

                isNoexist = True
            End If
            If CheckFileExist(dataDumper_grpwire) And dataDumper_grpwire_f <> 0 Then
                str = str & "dataDumper_grpwire : " & vbCrLf & dataDumper_grpwire & vbCrLf
                dataDumper_grpwire = ""
                dataDumper_grpwire_f = 0
                isNoexist = True
            End If
            If CheckFileExist(dataDumper_tranwire) And dataDumper_tranwire_f <> 0 Then
                str = str & "dataDumper_tranwire : " & vbCrLf & dataDumper_tranwire & vbCrLf
                dataDumper_tranwire = ""
                dataDumper_tranwire_f = 0
                isNoexist = True
            End If
            If CheckFileExist(dataDumper_wirefram) And dataDumper_wirefram_f <> 0 Then
                str = str & "dataDumper_wirefram : " & vbCrLf & dataDumper_wirefram & vbCrLf
                dataDumper_wirefram = ""
                dataDumper_wirefram_f = 0
                isNoexist = True
            End If
            If CheckFileExist(dataDumper_cmdicons) And dataDumper_cmdicons_f <> 0 Then
                str = str & "dataDumper_cmdicons : " & vbCrLf & dataDumper_cmdicons & vbCrLf
                dataDumper_cmdicons = ""
                dataDumper_wirefram_f = 0
                isNoexist = True
            End If
            If CheckFileExist(dataDumper_stat_txt) And dataDumper_stat_txt_f <> 0 Then
                str = str & "dataDumper_stat_txt : " & vbCrLf & dataDumper_stat_txt & vbCrLf
                dataDumper_stat_txt = ""
                dataDumper_stat_txt_f = 0
                isNoexist = True
            End If
            If CheckFileExist(dataDumper_AIscript) And dataDumper_AIscript_f <> 0 Then
                str = str & "dataDumper_AIscript : " & vbCrLf & dataDumper_AIscript & vbCrLf
                dataDumper_AIscript = ""
                dataDumper_AIscript_f = 0
                isNoexist = True
            End If
            If CheckFileExist(dataDumper_iscript) And dataDumper_iscript_f <> 0 Then
                str = str & "dataDumper_iscript : " & vbCrLf & dataDumper_iscript & vbCrLf
                dataDumper_iscript = ""
                dataDumper_iscript_f = 0
                isNoexist = True
            End If
            If CheckFileExist(iscriptPatcher) And iscriptPatcheruse = True Then
                str = str & "iscriptPatcher : " & vbCrLf & iscriptPatcher & vbCrLf
                iscriptPatcher = ""
                iscriptPatcheruse = False
                isNoexist = True
            End If


            If isNoexist Then
                MsgBox(str, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            End If


            str = "개의 GRP들이 존재하지 않습니다. 모두 사용안함으로 전환됩니다." & vbCrLf
            isNoexist = False

            Dim grpcount As UInt32

            Dim num As UInt32
            For k = 0 To GRPEditorDATA.Count - 1
                If CheckFileExist(GRPEditorDATA(num).Filename) And GRPEditorDATA(num).IsExternal = True Then
                    If grpcount < 11 Then
                        str = str & "GRP " & k & " : " & vbCrLf & GRPEditorDATA(num).Filename & vbCrLf
                    End If


                    For i = 0 To GRPEditorUsingindexDATA.Count - 1
                        If GRPEditorUsingindexDATA(i) = GRPEditorDATA(num).Filename Then
                            GRPEditorUsingindexDATA(i) = ""
                        End If
                    Next

                    For i = 0 To GRPEditorDATA(num).usingimage.Count - 1
                        GRPEditorUsingDATA(GRPEditorDATA(num).usingimage(i)) = ""
                    Next
                    GRPEditorDATA.RemoveAt(num)

                    grpcount += 1
                    isNoexist = True
                Else
                    num = +1
                End If
            Next

            If isNoexist Then
                If grpcount >= 11 Then
                    str = grpcount & str & vbCrLf & "이하 " & grpcount - 11 & "개의 GRP"
                End If
                MsgBox(str, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
            End If

            'For i = 0 To GRPEditorUsingindexDATA.Count - 1
            '    If GRPEditorUsingindexDATA(i) = GRPEditorDATA(num).Filename Then
            '        GRPEditorUsingindexDATA(i) = ""
            '    End If
            'Next


            'ListBox1.Items.RemoveAt((index))

            'For i = 0 To GRPEditorDATA(num).usingimage.Count - 1
            '    GRPEditorUsingDATA(GRPEditorDATA(num).usingimage(i)) = ""
            'Next

            'GRPEditorDATA.RemoveAt(num)



        End If
        ProjectSet.LoadCHKdata()
    End Sub


    Public Function CheckFileExist(Filename As String) As Boolean
        If Filename <> "" Then
            Dim f As New FileInfo(Filename)

            If f.Exists = False Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If

    End Function

    Public Function FindSection(base As String, key As String) As String
        Try
            Dim length = InStr(base, "E_" & key) - InStr(base, "S_" & key) - key.Count - 2

            Return Mid(base, InStr(base, "S_" & key) + key.Count + 4, length - 4)
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function FindSetting(base As String, key As String, Optional strflag As Boolean = False) As String
        Try
            Dim start As Integer = InStr(base, key & " ")
            If start > 0 Then
                Dim text As String = Mid(base, start)
                'If key = "extraedssetting" Then
                '    MsgBox(InStr(text, vbCrLf) - key.Count - 4)
                'End If
                If InStr(text, vbCrLf) = 0 Then
                    Return Mid(text, key.Count + 4)
                Else
                    Return Mid(text, key.Count + 4, InStr(text, vbCrLf) - key.Count - 4)
                End If
            ElseIf strflag Then
                Return ""
            Else
                Return "0"
            End If

        Catch ex As Exception
            'If key = "extraedssetting" Then
            '    MsgBox("base : " & base & vbCrLf & " key : " & key)

            '    Dim text = Mid(base, InStr(base, key & " "))
            '    Return Mid(text, key.Count + 4, InStr(text, vbCrLf) - key.Count - 4)
            'End If
            If strflag Then
                Return ""
            Else
                Return "0"
            End If

        End Try
    End Function

    Public Function ReadValDef(OffsetKey As String) As String
        OffsetKey = "[" & OffsetKey & "="
        Dim filename As String = My.Application.Info.DirectoryPath & "\Data\" & "ValueDef.txt"
        Dim filestr As FileStream = New FileStream(filename, FileMode.Open)
        Dim strstream As StreamReader = New StreamReader(filestr, Text.Encoding.GetEncoding("ks_c_5601-1987"))
        Dim strbinary As BinaryReader = New BinaryReader(filestr, Text.Encoding.GetEncoding("ks_c_5601-1987"))
        Dim offsettext As String = strstream.ReadToEnd
        strbinary.Close()
        strstream.Close()

        Dim _temp As String = Mid(offsettext, InStr(offsettext, OffsetKey) + OffsetKey.Length)
        _temp = Mid(_temp, 1, _temp.IndexOf("["))


        Return _temp.Trim
    End Function
    Public Function ReadOffset(OffsetKey As String) As String
        Dim StarVersion As String = ProgramSet.StarVersion

        OffsetKey = "[" & OffsetKey & "="
        Dim filename As String = My.Application.Info.DirectoryPath & "\Data\" & "Offset" & StarVersion & ".txt"
        Dim filestr As FileStream = New FileStream(filename, FileMode.Open)
        Dim strstream As StreamReader = New StreamReader(filestr, Text.Encoding.GetEncoding("ks_c_5601-1987"))
        Dim strbinary As BinaryReader = New BinaryReader(filestr, Text.Encoding.GetEncoding("ks_c_5601-1987"))
        Dim offsettext As String = strstream.ReadToEnd
        strbinary.Close()
        strstream.Close()


        Return Mid(offsettext, InStr(offsettext, OffsetKey) + OffsetKey.Length + 2, 6)
    End Function
    Public Function ReadName(Offset As String) As String
        Dim StarVersion As String = ProgramSet.StarVersion

        Offset = "=" & Offset
        Dim filename As String = My.Application.Info.DirectoryPath & "\Data\" & "Offset" & StarVersion & ".txt"
        Dim filestr As FileStream = New FileStream(filename, FileMode.Open)
        Dim strstream As StreamReader = New StreamReader(filestr, Text.Encoding.GetEncoding("ks_c_5601-1987"))
        Dim strbinary As BinaryReader = New BinaryReader(filestr, Text.Encoding.GetEncoding("ks_c_5601-1987"))
        Dim offsettext As String = strstream.ReadToEnd
        strbinary.Close()
        strstream.Close()



        Dim startoffset As Long = InStr(offsettext, Offset)
        Dim check As String = offsettext(startoffset)
        While ((check <> Chr(13)) And (startoffset > 0))
            check = offsettext(startoffset)
            startoffset -= 1
        End While
        If (startoffset <= 0) Then
            startoffset -= 2
        End If

        Return Mid(offsettext, startoffset + 3, InStr(offsettext, Offset) - startoffset - 3).Trim
    End Function


    Private Function replacetext(tstring As String) As String
        Dim rstring As String = tstring


        For i = 0 To 255
            rstring = rstring.Replace("<" & i & ">", Chr(i))
        Next


        If Mid(rstring, rstring.Length, 1) <> Chr(0) Then
            rstring = rstring & Chr(0)
        End If
        Return rstring
    End Function

    Private encoding As String = "ks_c_5601-1987" 'ks_c_5601-1987

    Public Function Getstattextbin() As String '파일로 쓰는 함수
        Dim filename As String = My.Application.Info.DirectoryPath & "\Data\temp\" & "stat_txt.tbl"

        Try
            My.Computer.FileSystem.DeleteFile(filename)
        Catch ex As Exception

        End Try
        Dim filestr As FileStream = New FileStream(filename, FileMode.CreateNew)
        Dim strstream As StreamWriter = New StreamWriter(filestr, Text.Encoding.GetEncoding(encoding))
        Dim strbinary As BinaryWriter = New BinaryWriter(filestr, Text.Encoding.GetEncoding(encoding)) 'Ge

        Dim strpoint As UInteger = stat_txt.Count * 2 + 2

        Dim pointpos As UInteger = 0


        LoadFileimportable()

        If ProjectSet.UsedSetting(8) Then
            For i = 0 To stattextdic.Count - 1
                stat_txt(stattextdic.Keys(i)) = stattextdic(stattextdic.Keys(i))
            Next
        End If

        strbinary.Write(CUShort(stat_txt.Count))
        pointpos += 2

        filestr.SetLength(stat_txt.Count * 2 + 2)


        For i = 0 To stat_txt.Count - 1
            filestr.Position = pointpos
            strbinary.Write(CUShort(strpoint))
            pointpos = filestr.Position


            filestr.Seek(0, SeekOrigin.End)


            strstream.Write(replacetext(stat_txt(i)))

            Dim memstram As MemoryStream = New MemoryStream()
            Dim strw As StreamWriter = New StreamWriter(memstram, Text.Encoding.GetEncoding(encoding))
            'MsgBox(memstram.Length)
            strw.Write(replacetext(stat_txt(i)))
            strw.AutoFlush = True
            strpoint += memstram.Length
            'MsgBox(filestr.Position)
            strw.Close()
            memstram.Close()
        Next

        strstream.Close()
        strbinary.Close()
        filestr.Close()


        Return filename 'dataDumper_stat_txt
    End Function


    Public Function Readstat_txtfile(Optional is32string As Boolean = False, Optional formmpq As Boolean = True) As String()
        If dataDumper_stat_txt_f = 0 Then '데이터 덤퍼를 사용하지 않을 경우
            Dim filename As String = My.Application.Info.DirectoryPath & "\Data\"
            filename = filename & statlangname(statlang)

            Return Readtblfile(filename, is32string, formmpq)
        Else '데이터 덤퍼를 사용 할 경우
            Return Readtblfile(dataDumper_stat_txt, is32string, formmpq)
        End If


        'Dim mpq As New SFMpq

        'Dim size As Integer

        ''If LoadMemory(DataName.stat_txt) Is Nothing Then
        ''    Getstattextbin()
        ''    Exit Function
        ''End If

        'Dim filestr As FileStream = New FileStream(dataDumper_stat_txt, FileMode.Open)

        ''Dim strmem As MemoryStream = New MemoryStream(mpq.ReaddatFile(filename))
        'Dim strstream As StreamReader = New StreamReader(filestr, Text.Encoding.GetEncoding(encoding)) 'Text.Encoding.GetEncoding("ks_c_5601-1987")
        'Dim strbinary As BinaryReader = New BinaryReader(filestr, Text.Encoding.GetEncoding(encoding)) 'GetEncoding("ks_c_5601-1987")

        'Dim tempindex As Integer
        'Dim nextpos As Long
        'Dim tempindex2 As Integer
        'Dim tempstring As String = ""
        'Dim strcount As Long = 0

        'Dim returnstring() As String = {}


        'size = strbinary.ReadUInt16
        'ReDim returnstring(size - 1)
        'For i = 0 To size - 1 '문자열 갯수
        '    filestr.Position = 2 + i * 2

        '    tempindex = strbinary.ReadUInt16()
        '    nextpos = strbinary.ReadUInt16()

        '    filestr.Position = tempindex

        '    strcount = 0
        '    tempindex2 = strbinary.ReadByte

        '    While (tempindex2 <> &H0)
        '        tempindex2 = strbinary.ReadByte


        '        strcount += 1
        '    End While

        '    If i = size - 1 Then
        '        strcount = filestr.Length - tempindex
        '    Else
        '        strcount = nextpos - tempindex
        '    End If

        '    filestr.Position = tempindex

        '    tempstring = ""

        '    Dim strlen As Integer = 0
        '    Dim lastposition = strbinary.BaseStream.Position - 1
        '    While (strbinary.BaseStream.Position < lastposition + strcount)
        '        Dim tempchar As String = ""
        '        Dim isendstrema As Byte = strbinary.ReadByte()
        '        filestr.Position -= 1
        '        tempchar = strbinary.ReadChar()

        '        If is32string = True Then
        '            If isendstrema < &H20 Then
        '                tempchar = "<" & isendstrema & ">"
        '            End If
        '            'If Asc(tempchar) < &H20 Then
        '            '    tempchar = "<" & CStr(Asc(tempchar)) & ">"
        '            'End If
        '            If isendstrema = 0 And strlen > 1 Then
        '                Exit While
        '            End If
        '        Else
        '            If Asc(tempchar) < &H20 Then
        '                tempchar = ""
        '            End If
        '        End If
        '        strlen += 1
        '        tempstring = tempstring & tempchar
        '    End While
        '    'For jk = 0 To strcount - 1


        '    returnstring(i) = tempstring
        '    'returnstring(i) = strbinary.ReadChars(strcount)
        'Next



        ''strbinary.Close()
        ''strstream.Close()
        ''strmem.Close()
        ''filestr.Close()

        'Return returnstring
    End Function



    Public Function Readtblfile(filename As String, Optional is32string As Boolean = False, Optional formmpq As Boolean = True) As String()
        Dim mpq As New SFMpq

        Dim size As Integer

        'If LoadMemory(DataName.stat_txt) Is Nothing Then
        '    Getstattextbin()
        '    Exit Function
        'End If

        Dim filestr As New FileStream(filename, FileMode.Open)

        'Dim filestr As MemoryStream = LoadMemory(DataName.stat_txt)
        'Dim strmem As MemoryStream = New MemoryStream(mpq.ReaddatFile(filename))
        Dim strstream As StreamReader = New StreamReader(filestr, Text.Encoding.GetEncoding(encoding)) 'Text.Encoding.GetEncoding("ks_c_5601-1987")
        Dim strbinary As BinaryReader = New BinaryReader(filestr, Text.Encoding.GetEncoding(encoding)) 'GetEncoding("ks_c_5601-1987")

        Dim tempindex As Integer
        Dim nextpos As Long
        Dim tempindex2 As Integer
        Dim tempstring As String = ""
        Dim strcount As Long = 0

        Dim returnstring() As String = {}


        size = strbinary.ReadUInt16
        ReDim returnstring(size - 1)
        For i = 0 To size - 1 '문자열 갯수
            filestr.Position = 2 + i * 2

            tempindex = strbinary.ReadUInt16()
            nextpos = strbinary.ReadUInt16()

            filestr.Position = tempindex

            strcount = 0
            tempindex2 = strbinary.ReadByte

            While (tempindex2 <> &H0)
                tempindex2 = strbinary.ReadByte


                strcount += 1
            End While

            If i = size - 1 Then
                strcount = filestr.Length - tempindex
            Else
                strcount = nextpos - tempindex
            End If

            filestr.Position = tempindex

            tempstring = ""

            Dim strlen As Integer = 0
            Dim lastposition = strbinary.BaseStream.Position - 1
            While (strbinary.BaseStream.Position < lastposition + strcount)
                Dim tempchar As String = ""
                Dim isendstrema As Byte = strbinary.ReadByte()
                filestr.Position -= 1
                tempchar = strbinary.ReadChar()

                If is32string = True Then
                    If isendstrema < &H20 Then
                        tempchar = "<" & isendstrema & ">"
                    End If
                    'If Asc(tempchar) < &H20 Then
                    '    tempchar = "<" & CStr(Asc(tempchar)) & ">"
                    'End If
                    If isendstrema = 0 And strlen > 1 Then
                        Exit While
                    End If
                Else
                    If Asc(tempchar) < &H20 Then
                        tempchar = ""
                    End If
                End If
                strlen += 1
                tempstring = tempstring & tempchar
            End While

            'For jk = 0 To strcount - 1

            'Next


            returnstring(i) = tempstring
            'returnstring(i) = strbinary.ReadChars(strcount)
        Next



        strbinary.Close()
        strstream.Close()
        filestr.Close()
        Return returnstring
    End Function


    Public Function Readtextfile(filename As String) As List(Of String)
        Dim _filename = My.Application.Info.DirectoryPath & "\Data\Langage\" & My.Settings.Langage & "\" & filename & ".txt"
        Dim _values As New List(Of String)
        Dim filestream As New FileStream(_filename, FileMode.Open)
        Dim strreader As New StreamReader(filestream, Text.Encoding.Default)

        While (strreader.EndOfStream = False)
            _values.Add(strreader.ReadLine.Trim)
        End While

        strreader.Close()
        filestream.Close()

        Return _values
    End Function
End Module
