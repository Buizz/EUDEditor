Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Namespace eudplib

    Module eudplibModule
        Private Function GetGRPValue(tstring As String)
            Dim returnstring As String
            returnstring = tstring.Replace(".grp", "")


            Dim length As Integer = returnstring.Split("\").Count - 1
            returnstring = returnstring.Split("\")(length)


            returnstring = returnstring.Replace(" ", "")

            returnstring = "G" & returnstring
            Return returnstring
        End Function
        Private Function GetFileName(tstring As String)
            Dim returnstring As String
            returnstring = tstring.Replace("\", "\\")

            Return returnstring
        End Function

        Public Function Getedstext() As String
            Dim returntext As New StringBuilder



            returntext.AppendLine("[main]")
            returntext.AppendLine()

            If ProjectSet.UsedSetting(ProjectSet.Settingtype.TileSet) = True Then
                AdJustNewMTXN()
                returntext.AppendLine("input: " & My.Application.Info.DirectoryPath & "\Data\temp\" & "map.scx")
            Else
                If ProjectSet.filename.EndsWith(".e2p") Then
                    returntext.AppendLine("input: ..\Map\" & GetSafeName(ProjectSet.InputMap))
                Else
                    returntext.AppendLine("input: " & ProjectSet.InputMap)
                End If
            End If '..\Map\(2)Twilight Struggle.scx

            returntext.AppendLine("output: " & ProjectSet.OutputMap)

            If ProjectSet.epTraceDebug Then
                returntext.AppendLine("debug: 1")
            End If


            returntext.AppendLine()
            returntext.AppendLine("[EUDEditor.py]")
            returntext.AppendLine()


            If ProjectSet.UsedSetting(ProjectSet.Settingtype.Struct) = True Then
                If ProjectSet.LoadFromCHK = False Then
                    MsgBox("CHK데이터 읽어오기가 활성화 되어 있지 않아 TriggerEditor옵션이 해제됩니다.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                    ProjectSet.UsedSetting(ProjectSet.Settingtype.Struct) = False
                    Main.buttonResetting()
                Else
                    returntext.AppendLine("[TriggerEditor.eps]")
                    returntext.AppendLine()
                End If
            End If

            '플러그인 비활성화시 DataDumper사용 원천차단하기.
            'If ProjectSet.UsedSetting(ProjectSet.Settingtype.Plugin) = False Then
            '    If (ProjectSet.UsedSetting(ProjectSet.Settingtype.BinEditor) Or
            '        ProjectSet.UsedSetting(ProjectSet.Settingtype.FireGraft) Or
            '        ProjectSet.UsedSetting(ProjectSet.Settingtype.TileSet)) Then

            '    End If

            '    Dim flag As Boolean = False
            '    returntext.AppendLine("[dataDumper]")
            '    If ProjectSet.UsedSetting(ProjectSet.Settingtype.FireGraft) = True Then
            '        flag = True
            '        RepDataToFile()
            '        returntext.Append(My.Application.Info.DirectoryPath.Replace(":", "\:") & "\Data\temp\" & "RequireData : 0x" & ReadOffset("Vanilla"))
            '        returntext.Append(", copy")
            '        returntext.AppendLine()
            '    End If

            '    If ProjectSet.UsedSetting(ProjectSet.Settingtype.TileSet) = True Then
            '        flag = True
            '        returntext.Append(My.Application.Info.DirectoryPath.Replace(":", "\:") & "\Data\temp\" & "MTXM : 0x" & ReadOffset("mtxm"))
            '        returntext.Append(", copy")
            '        returntext.AppendLine()
            '        returntext.Append(My.Application.Info.DirectoryPath.Replace(":", "\:") & "\Data\temp\" & "FMTXM : 0x" & ReadOffset("fmtxm"))
            '        returntext.Append(", copy")
            '        returntext.AppendLine()
            '        If ProjectTileUseFile = True And (CheckFileExist(ProjectTileSetFileName) = False) Then
            '            returntext.Append(My.Application.Info.DirectoryPath.Replace(":", "\:") & "\Data\temp\" & "vr4 : 0x" & ReadOffset("vr4"))
            '            returntext.Append(", copy")
            '            returntext.AppendLine()
            '        End If
            '    End If

            '    If ProjectSet.UsedSetting(ProjectSet.Settingtype.BinEditor) = True Then
            '        If ProjectSet.PlayerRace <> 255 Then
            '            flag = True
            '            returntext.Append(My.Application.Info.DirectoryPath.Replace(":", "\:") & "\Data\temp\" & "STransDat : 0x" & Hex(("&H" & ReadOffset("Vanilla")) + 4))
            '            returntext.Append(", copy")
            '            returntext.AppendLine()
            '        End If
            '    End If

            '    If flag = False Then
            '        returntext.Replace("[dataDumper]", "")
            '    End If
            'End If

            returntext.AppendLine("[dataDumper]")
            If ProjectSet.UsedSetting(ProjectSet.Settingtype.filemanager) = True And stattextdic.Count <> 0 Then
                returntext.Append(Getstattextbin.Replace(":", "\:") & " : 0x" & ReadOffset("stat_txt.tbl"))
                returntext.Append(", copy")
                returntext.AppendLine()
            End If
            If ProjectSet.UsedSetting(ProjectSet.Settingtype.FireGraft) = True Then
                RepDataToFile()
                '호환성버그고치기
                returntext.Append(My.Application.Info.DirectoryPath.Replace(":", "\:") & "\Data\temp\" & "RequireData : 0x" & ReadOffset("Vanilla"))
                returntext.Append(", copy")
                returntext.AppendLine()
            End If
            If ProjectSet.UsedSetting(ProjectSet.Settingtype.Plugin) = True Then
                If nqcuse = True Then
                    returntext.AppendLine("[MSQC]")
                    Try
                        Dim num As Integer = nqcunit
                        If num <> 58 Then
                            returntext.AppendLine("QCUnitID : " & nqcunit)

                        End If
                    Catch ex As Exception
                    End Try

                    Dim temp1string() As String = nqccommands.Split({"\"}, StringSplitOptions.RemoveEmptyEntries)

                    Dim _values As New List(Of String)
                    For i = 0 To CODE(0).Count - 1
                        If DatEditDATA(DTYPE.units).ReadValue("Unit Map String", i) = 0 Then
                            _values.Add(CODE(0)(i))
                        Else
                            Try
                                _values.Add(ProjectSet.CHKSTRING(-1 + ProjectSet.CHKUNITNAME(i)))
                            Catch ex As Exception
                                _values.Add(CODE(0)(i))
                            End Try

                        End If
                    Next


                    For i = 0 To temp1string.Count - 1
                        Dim temp2string() As String = temp1string(i).Split("#")
                        Try
                            Dim temp As UInteger = temp2string(1).Trim
                            returntext.AppendLine(temp2string(0).Trim & " : " & _values(temp2string(1).Trim) & ", " & temp2string(2).Trim)
                        Catch ex As Exception
                            returntext.AppendLine(temp2string(0).Trim & " : " & temp2string(1).Trim)
                        End Try
                    Next
                    If ProjectSet.SCDBUse Then
                        returntext.AppendLine("val, 0x58F524 ; Memory(0x58F520, Exactly, 1) :" & 437 + (SCDBDataSize + 3) \ 12)
                        returntext.AppendLine("val, 0x58F528 ; Memory(0x58F520, Exactly, 1) :" & 438 + (SCDBDataSize + 3) \ 12)
                        returntext.AppendLine("val, 0x58F52C ; Memory(0x58F520, Exactly, 1) :" & 439 + (SCDBDataSize + 3) \ 12)
                        returntext.AppendLine("val, 0x58F530 ; Memory(0x58F520, Exactly, 1) :" & 440 + (SCDBDataSize + 3) \ 12)

                        For i = 0 To SCDBDataSize - 1
                            returntext.AppendLine("val, 0x" & Hex(&H58F534 + i * 4) & " ; Memory(0x58F520, Exactly, 1) :" & 441 + i + (SCDBDataSize + 3) \ 12)
                        Next
                    End If



                    If nqclocs.Split(",").Count = 8 Then
                        Dim locs() As String = nqclocs.Split(",")
                        Dim _flag As Boolean = True
                        For i = 0 To 7
                            If Val(locs(i)) = 0 Then
                                _flag = False
                            End If
                        Next
                        If _flag = True Then
                            _values.Clear()
                            _values.Add("None")
                            For i = 0 To 254
                                If ProjectSet.CHKLOCATIONNAME(i) <> 0 Then
                                    _values.Add(ProjectSet.CHKSTRING(ProjectSet.CHKLOCATIONNAME(i) - 1))
                                Else
                                    _values.Add("Location " & i)
                                End If
                            Next

                            returntext.Append("마우스 : " & _values(Val(locs(0))))
                            For i = 1 To 7
                                returntext.Append(", " & _values(Val(locs(i))))
                            Next
                            returntext.AppendLine()
                        End If
                    End If
                End If


                'Public dataDumper_grpwire As String
                'Public dataDumper_tranwire As String
                'Public dataDumper_wirefram As String
                'Public dataDumper_cmdicons As String
                'Public dataDumper_stat_txt As String
                'Public dataDumper_AIscript As String
                'Public dataDumper_iscript As String

                'Public dataDumper_grpwire_f As Byte
                'Public dataDumper_tranwire_f As Byte
                'Public dataDumper_wirefram_f As Byte
                'Public dataDumper_cmdicons_f As Byte
                'Public dataDumper_stat_txt_f As Byte
                'Public dataDumper_AIscript_f As Byte
                'Public dataDumper_iscript_f As Byte

                'DataDumper옵션 사용체크해야지 자동으로 삽입되는 형식.




                If dataDumperuse = True Then
                    If ProjectSet.UsedSetting(ProjectSet.Settingtype.BinEditor) = True Then
                        If ProjectSet.PlayerRace <> 255 Then
                            returntext.Append(My.Application.Info.DirectoryPath.Replace(":", "\:") & "\Data\temp\" & "STransDat : 0x" & Hex(("&H" & ReadOffset("Vanilla")) + 4))
                            returntext.Append(", copy")
                            returntext.AppendLine()
                        End If
                    End If
                    If ProjectSet.UsedSetting(ProjectSet.Settingtype.TileSet) = True Then
                        returntext.Append(My.Application.Info.DirectoryPath.Replace(":", "\:") & "\Data\temp\" & "MTXM : 0x" & ReadOffset("mtxm"))
                        returntext.Append(", copy")
                        returntext.AppendLine()
                        returntext.Append(My.Application.Info.DirectoryPath.Replace(":", "\:") & "\Data\temp\" & "FMTXM : 0x" & ReadOffset("fmtxm"))
                        returntext.Append(", copy")
                        returntext.AppendLine()
                        If ProjectTileUseFile = True And (CheckFileExist(ProjectTileSetFileName) = False) Then
                            returntext.Append(My.Application.Info.DirectoryPath.Replace(":", "\:") & "\Data\temp\" & "vr4 : 0x" & ReadOffset("vr4"))
                            returntext.Append(", copy")
                            returntext.AppendLine()
                        End If
                    End If
                    If dataDumperuse = True Then
                        If dataDumper_grpwire_f <> 0 Then
                            returntext.Append(dataDumper_grpwire.Replace(":", "\:") & " : 0x" & ReadOffset("grpwire.grp"))
                            Select Case dataDumper_grpwire_f
                                Case 2
                                    returntext.Append(", copy")
                                Case 3
                                    returntext.Append(", unpatchable")
                            End Select
                            returntext.AppendLine()
                        End If
                        If dataDumper_tranwire_f <> 0 Then
                            returntext.Append(dataDumper_tranwire.Replace(":", "\:") & " : 0x" & ReadOffset("tranwire.grp"))
                            Select Case dataDumper_tranwire_f
                                Case 2
                                    returntext.Append(", copy")
                                Case 3
                                    returntext.Append(", unpatchable")
                            End Select
                            returntext.AppendLine()
                        End If
                        If dataDumper_wirefram_f <> 0 Then
                            returntext.Append(dataDumper_wirefram.Replace(":", "\:") & " : 0x" & ReadOffset("wirefram.grp"))
                            Select Case dataDumper_wirefram_f
                                Case 2
                                    returntext.Append(", copy")
                                Case 3
                                    returntext.Append(", unpatchable")
                            End Select
                            returntext.AppendLine()
                        End If
                        If dataDumper_cmdicons_f <> 0 Then
                            returntext.Append(dataDumper_cmdicons.Replace(":", "\:") & " : 0x" & ReadOffset("cmdicons.grp"))
                            Select Case dataDumper_cmdicons_f
                                Case 2
                                    returntext.Append(", copy")
                                Case 3
                                    returntext.Append(", unpatchable")
                            End Select
                            returntext.AppendLine()
                        End If
                        If dataDumper_AIscript_f <> 0 Then
                            returntext.Append(dataDumper_AIscript.Replace(":", "\:") & " : 0x" & ReadOffset("Aiscript.bin"))
                            Select Case dataDumper_AIscript_f
                                Case 2
                                    returntext.Append(", copy")
                                Case 3
                                    returntext.Append(", unpatchable")
                            End Select
                            returntext.AppendLine()
                        End If
                        If dataDumper_iscript_f <> 0 Then
                            returntext.Append(dataDumper_iscript.Replace(":", "\:") & " : 0x" & ReadOffset("iscript.bin"))
                            Select Case dataDumper_iscript_f
                                Case 2
                                    returntext.Append(", copy")
                                Case 3
                                    returntext.Append(", unpatchable")
                            End Select
                            returntext.AppendLine()
                        End If
                        returntext.Append(dataDumper_user.Replace("##", vbCrLf))

                        returntext.AppendLine()
                        returntext.AppendLine()
                    End If
                End If


                If grpinjectoruse = True Then
                    returntext.AppendLine("[grpinjector]")
                    If grpinjector_arrow <> "" Then
                        returntext.AppendLine(grpinjector_arrow.Replace(":", "\:") & " : 0x" & ReadOffset("arrow.grp"))
                    End If
                    If grpinjector_drag <> "" Then
                        returntext.AppendLine(grpinjector_drag.Replace(":", "\:") & " : 0x" & ReadOffset("draw.grp"))
                    End If
                    If grpinjector_illegal <> "" Then
                        returntext.AppendLine(grpinjector_illegal.Replace(":", "\:") & " : 0x" & ReadOffset("illegal.grp"))
                    End If


                    returntext.AppendLine("")
                End If





                If iscriptPatcheruse = True Then
                    returntext.AppendLine("[iscriptPatcher]" & vbCrLf &
                                                      "iscript : " & iscriptPatcher & vbCrLf)
                End If

                If unpatcheruse = True Then
                    returntext.AppendLine("[unpatcher]" & vbCrLf &
                                                  "resetCond : [" & unpatcher.Replace("##", ", ") & "]" & vbCrLf)
                End If


                If soundstopper = True Then
                    returntext.AppendLine("[soundstopper]" & vbCrLf)
                End If
                If scmloader = True Then
                    returntext.AppendLine("[scmloader]" & vbCrLf)
                End If
                If noAirCollision = True Then
                    returntext.AppendLine("[noAirCollision]" & vbCrLf)
                End If
                If unlimiter = True Then
                    returntext.AppendLine("[unlimiter]" & vbCrLf)
                End If
                If keepSTR = True Then
                    returntext.AppendLine("[keepSTR]" & vbCrLf)
                End If
                If eudTurbo = True Then
                    returntext.AppendLine("[eudTurbo]" & vbCrLf)
                End If


                Dim textraedssetting As String = extraedssetting
                Extractextraedssetting(returntext, textraedssetting, "[dataDumper]")
                Extractextraedssetting(returntext, textraedssetting, "[grpinjector]")



                returntext.AppendLine(textraedssetting)
            End If


            If ProjectSet.EUDEditorDebug = True Then
                returntext.AppendLine("[EUDEditorDebug.py]" & vbCrLf)
            End If


            Return returntext.ToString
        End Function
        Private Sub Extractextraedssetting(ByRef mainstr As StringBuilder, ByRef extraeds As String, contentname As String)
            Dim tstrarr() As String = extraeds.Split(vbCrLf, Integer.MaxValue, StringSplitOptions.RemoveEmptyEntries)

            Dim resultextraeds As String = ""


            Dim contents As String = contentname & vbCrLf

            Dim ishaveheader As Boolean = False
            For i = 0 To tstrarr.Count - 1
                If tstrarr(i) = contentname And ishaveheader = False Then
                    ishaveheader = True
                ElseIf ishaveheader = True Then
                    If InStr(tstrarr(i), "[") <> 0 Then
                        ishaveheader = False
                    Else
                        contents = contents & tstrarr(i) & vbCrLf
                    End If

                End If


                If ishaveheader = False Then
                    resultextraeds = resultextraeds & tstrarr(i) & vbCrLf
                End If
            Next
            contents = contents.Trim





            mainstr = New StringBuilder(Replace(mainstr.ToString, contentname, contents))

            extraeds = resultextraeds
        End Sub



        Public Function GetPYtext() As String
            Dim returntext As New StringBuilder()

            'returntext.AppendLine("# coding=utf-8")
            returntext.AppendLine("from eudplib import *")
            returntext.AppendLine()

            'GRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRP
            'GRP 변수 선언하기.
            If ProjectSet.UsedSetting(ProjectSet.Settingtype.GRP) = True Then
                'returntext.AppendLine("# grp영역 시작")
                'grpptr = EUDGrp("D:\\스타크래프트 맵만들기\\SCX관리\\mpqmaster_1.3.2b_43\\archon0000.grp")
                Dim mpq As New SFMpq
                For i = 0 To GRPEditorDATA.Count - 1
                    If GRPEditorDATA(i).IsExternal = True Then
                        returntext.AppendLine(GetGRPValue(GRPEditorDATA(i).Filename) & " = EUDVariable()")
                        'returntext.AppendLine(GetGRPValue(GRPEditorDATA(i).SafeFilename) & " = EUDGrp(""" & GetFileName(GRPEditorDATA(i).Filename) & """)")
                    Else
                        Dim grpnum As Integer = 0
                        For k = 0 To CODE(DTYPE.grpfile).Count - 1
                            If CODE(DTYPE.grpfile)(k).Replace("<0>", "") = GRPEditorDATA(i).Filename.Replace("unit\", "") Then
                                grpnum = k
                                Exit For
                            End If
                        Next
                        returntext.AppendLine(GetGRPValue(GRPEditorDATA(i).Filename) & " = EUDVariable()")
                    End If
                Next
                'returntext.AppendLine("# grp영역 끝")
            End If
            'GRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRP


            returntext.AppendLine("def onPluginStart():")

            If ProjectSet.UsedSetting(ProjectSet.Settingtype.BtnSet) = True Then

                Dim tempfoluder As String = My.Application.Info.DirectoryPath & "\Data\temp\"

                For i = 0 To Soundlist.Count - 1
                    Dim output As String = tempfoluder & "M" & i & "_"

                    Dim index As Integer = 0

                    While True
                        Dim _temp As String = output & index & ".ogg"
                        If CheckFileExist(_temp) Then
                            Exit While
                        End If

                        returntext.AppendLine("    MPQAddFile(""M" & i & "_" & index & ".ogg"" ,open(""" & _temp.Replace("\", "\\") & """, ""rb"").read() ,False);")
                        index += 1
                    End While
                Next
            End If


            'FileManager
            If ProjectSet.UsedSetting(ProjectSet.Settingtype.filemanager) = True Then
                '바뀐게 있는지 체크.
                Dim checkflag As Boolean = False
                For i = 0 To wireframData.Count - 1
                    If wireframData(i) <> i Then
                        checkflag = True
                        Exit For
                    End If
                Next
                If checkflag = False Then
                    For i = 0 To grpwireData.Count - 1
                        If grpwireData(i) <> i Then
                            checkflag = True
                            Exit For
                        End If
                    Next
                    If checkflag = False Then
                        For i = 0 To tranwireData.Count - 1
                            If tranwireData(i) <> i Then
                                checkflag = True
                                Exit For
                            End If
                        Next
                    End If
                End If


                If checkflag Then
                    Dim mpq As New SFMpq

                    Dim fStream As FileStream
                    Dim memStream As MemoryStream
                    Dim binaryReader As BinaryReader
                    Dim binaryWriter As BinaryWriter

                    Dim grpframecount As UInt16
                    If dataDumper_wirefram_f <> 0 Then
                        fStream = New FileStream(dataDumper_wirefram, FileMode.Open)
                        binaryReader = New BinaryReader(fStream)
                        binaryWriter = New BinaryWriter(fStream)
                    Else
                        memStream = New MemoryStream(mpq.ReaddatFile("unit\wirefram\wirefram.grp"))
                        binaryReader = New BinaryReader(memStream)
                        binaryWriter = New BinaryWriter(memStream)
                    End If
                    grpframecount = binaryReader.ReadUInt16
                    Dim grpdata(grpframecount - 1) As UInt64
                    For i = 0 To grpframecount - 1
                        If wireframData(i) <> i Then
                            binaryReader.BaseStream.Position = 6 + 8 * wireframData(i)

                            grpdata(i) = binaryReader.ReadUInt64
                        End If
                    Next

                    For i = 0 To grpframecount - 1
                        If wireframData(i) <> i Then
                            binaryReader.BaseStream.Position = 6 + 8 * i
                            binaryWriter.Write(grpdata(i))
                        End If
                    Next


                    returntext.AppendLine("    WireOffset = f_dwread_epd(EPD(0x" & ReadOffset("wirefram.grp") & "))")
                    returntext.AppendLine("    DoActions([")
                    For i = 0 To grpframecount - 1
                        If wireframData(i) <> i Then
                            binaryReader.BaseStream.Position = 4 + 8 * i
                            returntext.AppendLine("    SetMemory(WireOffset + " & 4 + 8 * i & ", SetTo, " & binaryReader.ReadUInt32 & "),")
                            returntext.AppendLine("    SetMemory(WireOffset + " & 8 + 8 * i & ", SetTo, " & binaryReader.ReadUInt32 & "),")
                            returntext.AppendLine("    SetMemory(WireOffset + " & 12 + 8 * i & ", SetTo, " & binaryReader.ReadUInt32 & "),")
                        End If
                    Next
                    returntext.AppendLine("    ])")


                    binaryReader.BaseStream.Close()
                    binaryReader.Close()
                    binaryWriter.Close()






                    If dataDumper_grpwire_f <> 0 Then
                        fStream = New FileStream(dataDumper_grpwire, FileMode.Open)
                        binaryReader = New BinaryReader(fStream)
                        binaryWriter = New BinaryWriter(fStream)
                    Else
                        memStream = New MemoryStream(mpq.ReaddatFile("unit\wirefram\grpwire.grp"))
                        binaryReader = New BinaryReader(memStream)
                        binaryWriter = New BinaryWriter(memStream)
                    End If
                    grpframecount = binaryReader.ReadUInt16
                    ReDim grpdata(grpframecount - 1)
                    For i = 0 To grpframecount - 1
                        If grpwireData(i) <> i Then
                            binaryReader.BaseStream.Position = 6 + 8 * grpwireData(i)

                            grpdata(i) = binaryReader.ReadUInt64
                        End If
                    Next

                    For i = 0 To grpframecount - 1
                        If grpwireData(i) <> i Then
                            binaryReader.BaseStream.Position = 6 + 8 * i
                            binaryWriter.Write(grpdata(i))
                        End If
                    Next


                    returntext.AppendLine("    GrpOffset = f_dwread_epd(EPD(0x" & ReadOffset("grpwire.grp") & "))")
                    returntext.AppendLine("    DoActions([")
                    For i = 0 To grpframecount - 1
                        If grpwireData(i) <> i Then
                            binaryReader.BaseStream.Position = 4 + 8 * i
                            returntext.AppendLine("    SetMemory(GrpOffset + " & 4 + 8 * i & ", SetTo, " & binaryReader.ReadUInt32 & "),")
                            returntext.AppendLine("    SetMemory(GrpOffset + " & 8 + 8 * i & ", SetTo, " & binaryReader.ReadUInt32 & "),")
                            returntext.AppendLine("    SetMemory(GrpOffset + " & 12 + 8 * i & ", SetTo, " & binaryReader.ReadUInt32 & "),")
                        End If
                    Next
                    returntext.AppendLine("    ])")


                    binaryReader.BaseStream.Close()
                    binaryReader.Close()
                    binaryWriter.Close()



                    If dataDumper_tranwire_f <> 0 Then
                        fStream = New FileStream(dataDumper_tranwire, FileMode.Open)
                        binaryReader = New BinaryReader(fStream)
                        binaryWriter = New BinaryWriter(fStream)
                    Else
                        memStream = New MemoryStream(mpq.ReaddatFile("unit\wirefram\grpwire.grp"))
                        binaryReader = New BinaryReader(memStream)
                        binaryWriter = New BinaryWriter(memStream)
                    End If
                    grpframecount = binaryReader.ReadUInt16
                    ReDim grpdata(grpframecount - 1)
                    For i = 0 To grpframecount - 1
                        If tranwireData(i) <> i Then
                            binaryReader.BaseStream.Position = 6 + 8 * tranwireData(i)

                            grpdata(i) = binaryReader.ReadUInt64
                        End If
                    Next

                    For i = 0 To grpframecount - 1
                        If tranwireData(i) <> i Then
                            binaryReader.BaseStream.Position = 6 + 8 * i
                            binaryWriter.Write(grpdata(i))
                        End If
                    Next


                    returntext.AppendLine("    tranOffset = f_dwread_epd(EPD(0x" & ReadOffset("tranwire.grp") & "))")
                    returntext.AppendLine("    DoActions([")
                    For i = 0 To grpframecount - 1
                        If tranwireData(i) <> i Then
                            binaryReader.BaseStream.Position = 4 + 8 * i
                            returntext.AppendLine("    SetMemory(tranOffset + " & 4 + 8 * i & ", SetTo, " & binaryReader.ReadUInt32 & "),")
                            returntext.AppendLine("    SetMemory(tranOffset + " & 8 + 8 * i & ", SetTo, " & binaryReader.ReadUInt32 & "),")
                            returntext.AppendLine("    SetMemory(tranOffset + " & 12 + 8 * i & ", SetTo, " & binaryReader.ReadUInt32 & "),")
                        End If
                    Next
                    returntext.AppendLine("    ])")


                    binaryReader.BaseStream.Close()
                    binaryReader.Close()
                    binaryWriter.Close()


                    'If dataDumper_tranwire_f <> 0 Then
                    '    fStream = New FileStream(dataDumper_tranwire, FileMode.Open)
                    '    binaryReader = New BinaryReader(fStream)
                    'Else
                    '    memStream = New MemoryStream(mpq.ReaddatFile("unit\wirefram\tranwire.grp"))
                    '    binaryReader = New BinaryReader(memStream)
                    'End If


                End If
            End If
                'FileManager

                If ProjectSet.UsedSetting(ProjectSet.Settingtype.BinEditor) = True Then
                If ProjectSet.PlayerRace = 255 Then
                    MsgBox("플레이어의 종족이 올바르지 않습니다." & vbCrLf & "BinEidt 옵션이 해제됩니다." & vbCrLf & "(이는 심각한 에러는 아니지만 콘솔 변경이 적용되지 않습니다.)", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                    ProjectSet.UsedSetting(ProjectSet.Settingtype.BinEditor) = False
                    Main.buttonResetting()
                Else
                    ProjectSet.LoadCHKdata()
                    returntext.AppendLine(GetPlibText(0, ProjectSet.PlayerRace))
                End If
            End If

            'GRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRP
            'GRP 변수 불러오기
            If ProjectSet.UsedSetting(ProjectSet.Settingtype.GRP) = True Then
                'returntext.AppendLine("# grp영역 시작")
                'grpptr = EUDGrp("D:\\스타크래프트 맵만들기\\SCX관리\\mpqmaster_1.3.2b_43\\archon0000.grp")
                Dim mpq As New SFMpq
                For i = 0 To GRPEditorDATA.Count - 1
                    If GRPEditorDATA(i).IsExternal = True Then
                        returntext.AppendLine("    " & GetGRPValue(GRPEditorDATA(i).Filename) & " <<EUDGrp(""" & GetFileName(GRPEditorDATA(i).Filename) & """)")
                        'returntext.AppendLine(GetGRPValue(GRPEditorDATA(i).SafeFilename) & " = EUDGrp(""" & GetFileName(GRPEditorDATA(i).Filename) & """)")
                    Else


                        Dim imagenum As Integer = 0
                        For k = 0 To CODE(DTYPE.images).Count - 1
                            If CODE(DTYPE.grpfile)(DatEditDATA(DTYPE.images).ReadValue("GRP File", k)).Replace("<0>", "") = GRPEditorDATA(i).Filename.Replace("unit\", "") Then
                                imagenum = k
                                Exit For
                            End If
                        Next
                        returntext.AppendLine("    " & GetGRPValue(GRPEditorDATA(i).SafeFilename) & " << " & "f_dwread_epd(EPD(0x51CED0 + 0x" & Hex(imagenum * 4) & "))")
                    End If
                Next
                returntext.AppendLine("    DoActions([")
                If GRPEditorUsingDATA IsNot Nothing Then
                    For i = 0 To GRPEditorUsingDATA.Count - 1
                        If GRPEditorUsingDATA(i) <> "" Then
                            returntext.AppendLine("        SetMemory(0x51CED0 + 0x" & Hex(i * 4) & ", SetTo, " & GetGRPValue(GRPEditorUsingDATA(i)) & "),")
                        End If
                    Next
                End If

                If GRPEditorUsingindexDATA IsNot Nothing Then
                    For i = 0 To GRPEditorUsingindexDATA.Count - 1
                        If GRPEditorUsingindexDATA(i) <> "" Then
                            returntext.AppendLine("        SetMemory(0x57D754 - 0x" & Hex(i * 64) & ", SetTo, " & GetGRPValue(GRPEditorUsingindexDATA(i)) & "),")
                        End If
                    Next
                End If
                returntext.AppendLine("    ])")
                'returntext.AppendLine("# grp영역 끝")
            End If
            'GRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRPGRP

            'MsgBox("파이어그래프트 빌드시작")
            '==================================BtnSet===============================
            If ProjectSet.UsedSetting(ProjectSet.Settingtype.FireGraft) = True Then
                For i = 0 To ProjectBtnUSE.Count - 1
                    'Dim passflag As Boolean = False
                    '모든 버튼 정보를 조사해서 같은 게 있으면 몽땅 어드레스 등록하기.
                    '후방을 조사해서 같은거 모두 적는다.
                    '전방에 같은게 있다면 패스한다.
                    'If ProjectBtnUSE(i) = True Then

                    '    For k = 0 To i - 1 '전방검사 검사
                    '        If ProjectBtnUSE(k) = True Then
                    '            'MsgBox(i & " 전방검사 : " & k)
                    '            If GetTextValue(i) = GetTextValue(k) Then
                    '                'MsgBox("같음") '패스하기
                    '                passflag = True
                    '            End If
                    '        End If
                    '    Next
                    'End If

                    'If passflag = False Then
                    '    If ProjectBtnUSE(i) = True Then

                    '        returntext.AppendLine("    bytebuffer = bytearray([" & GetTextValue(i) & "])")
                    '        returntext.AppendLine("    btnptr = Db(bytebuffer)")

                    '        returntext.AppendLine("    DoActions([")
                    '        For k = i To ProjectBtnUSE.Count - 1 '후방 검사
                    '            If ProjectBtnUSE(k) = True Then
                    '                'MsgBox(i & " 후방검사 : " & k)
                    '                If GetTextValue(i) = GetTextValue(k) Then
                    '                    'MsgBox("같음") '같이적기
                    '                    returntext.AppendLine("        SetMemory(0x" & Hex(Val("&H" & ReadOffset("FG_BtnAddress")) + 12 * k) & ", SetTo, btnptr),")

                    '                End If
                    '            End If
                    '        Next
                    '        returntext.AppendLine("    ])")
                    '    End If
                    'End If
                    If ProjectBtnUSE(i) = True Then

                        returntext.AppendLine("    bytebuffer = bytearray([" & GetTextValue(i) & "])")
                        returntext.AppendLine("    btnptr = Db(bytebuffer)")

                        returntext.AppendLine("    DoActions([")
                        returntext.AppendLine("        SetMemory(0x" & Hex(Val("&H" & ReadOffset("FG_BtnAddress")) + 12 * i) & ", SetTo, btnptr),")
                        returntext.AppendLine("    ])")
                    End If
                Next


                returntext.AppendLine("    DoActions([")
                For i = 0 To ProjectBtnUSE.Count - 1
                    If ProjectBtnUSE(i) = True Then
                        returntext.AppendLine("        SetMemory(0x" & Hex(Val("&H" & ReadOffset("FG_BtnNum")) + 12 * i) & ", SetTo, " & ProjectBtnData(i).Count & "),")
                    End If
                Next
                returntext.AppendLine("    ])")
            End If
            '==================================BtnSet===============================
            'MsgBox("파이어그래프트 빌드끝")



            '==================================TileSet===============================
            If ProjectSet.UsedSetting(ProjectSet.Settingtype.TileSet) = True Then
                Dim mpq As New SFMpq


                Dim cv5() As Byte = mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & ".cv5")
                Dim cv5mem As MemoryStream = New MemoryStream(cv5)
                Dim cv5binary As BinaryReader = New BinaryReader(cv5mem)


                'ReDim TilebitDATA(63)
                Dim vx4() As Byte = mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & ".vx4")
                Dim vr4() As Byte = mpq.ReaddatFile("tileset\" & tilesetname(TileSetType) & ".vr4")


                Dim vx4mem As MemoryStream = New MemoryStream(vx4)
                Dim vx4binary As BinaryReader = New BinaryReader(vx4mem)
                Dim vx4binaryw As BinaryWriter = New BinaryWriter(vx4mem)
                'C:\Users\skslj\Desktop\jungle.vr4

                'returntext.AppendLine("    bptr = Db(open('C:\\Users\skslj\Desktop\jungle.vr4', 'rb').read())")
                'returntext.AppendLine("    DoActions([")
                'returntext.AppendLine("       SetMemory(0x628444, SetTo, bptr),")
                'returntext.AppendLine("    ])")

                returntext.AppendLine("    cv5offset = f_epdread_epd(EPD(0x6D5EC8))")
                returntext.AppendLine("    vr4offset = f_epdread_epd(EPD(0x628444))")
                returntext.AppendLine("    vx4offset = f_epdread_epd(EPD(0x628458))")
                returntext.AppendLine("    DoActions([")

                If ProjectTileUseFile = False Then
                    For i = 0 To ProjectTileSetData.Count - 1
                        With ProjectTileSetData(i)
                            Dim ptData As MemoryStream = New MemoryStream(.TileSetData)
                            Dim ptBinary As BinaryReader = New BinaryReader(ptData)

                            If .isMaker = True Then
                                Dim tgroup As UInt16 = .TileSetNum \ 16
                                Dim tindex As Byte = .TileSetNum Mod 16
                                cv5mem.Position = &H14 + 52 * tgroup + 2 * tindex
                                returntext.AppendLine("    ])")
                                returntext.AppendLine("    DoActions([")
                                'cv5binaryw.Write(CUShort(.TileSetNum))
                                If cv5mem.Position Mod 4 = 0 Then
                                    'returntext.AppendLine("       value = ")
                                    returntext.AppendLine("       SetDeaths(cv5offset + " & (cv5mem.Position \ 4) & ", SetTo, f_dwbreak(f_dwread_epd(cv5offset + " & (cv5mem.Position \ 4) & "))[1] * 65536 + " & (.TileSetNum) & ", 0),")
                                Else
                                    'returntext.AppendLine("       value = f_dwbreak(f_dwread_epd(cv5offset + " & (cv5mem.Position \ 4) & "))[0]")
                                    returntext.AppendLine("       SetDeaths(cv5offset + " & (cv5mem.Position \ 4) & ", SetTo, f_dwbreak(f_dwread_epd(cv5offset + " & (cv5mem.Position \ 4) & "))[0] +" & (.TileSetNum * 65536) & ", 0),")
                                End If
                                returntext.AppendLine("    ])")
                                returntext.AppendLine("    DoActions([")

                                vx4mem.Position = 32 * .TileSetNum
                                For j = 0 To 15
                                    Dim vr4index As UInt16 = .TileSetNum * 16 + j
                                    'vx4binaryw.Write(vr4index << 1)

                                    returntext.AppendLine("    ])")
                                    returntext.AppendLine("    DoActions([")
                                    If vx4mem.Position Mod 4 = 0 Then
                                        'returntext.AppendLine("       value = ")
                                        returntext.AppendLine("       SetDeaths(vx4offset + " & (vx4mem.Position \ 4) & ", SetTo, f_dwbreak(f_dwread_epd(vx4offset + " & (vx4mem.Position \ 4) & "))[1] * 65536 + " & (vr4index << 1) & ", 0),")
                                    Else
                                        'returntext.AppendLine("       value = f_dwbreak(f_dwread_epd(vx4offset + " & (vx4mem.Position \ 4) & "))[0]")
                                        returntext.AppendLine("       SetDeaths(vx4offset + " & (vx4mem.Position \ 4) & ", SetTo, f_dwbreak(f_dwread_epd(vx4offset + " & (vx4mem.Position \ 4) & "))[0] +" & ((vr4index << 1) * 65536) & ", 0),")
                                    End If
                                    returntext.AppendLine("    ])")
                                    returntext.AppendLine("    DoActions([")

                                    vx4mem.Position += 2

                                    Dim ptr As Integer = 0


                                    returntext.AppendLine("       SetMemory(0x6509B0, SetTo, vr4offset + " & (vr4index * 64) \ 4 & "),")
                                    For k = 0 To 7 'y
                                        ptData.Position = k * 32 + 8 * (j Mod 4) + 256 * (j \ 4)

                                        returntext.AppendLine("       SetDeaths(CurrentPlayer , SetTo, " & ptBinary.ReadUInt32() & ", 0),")
                                        returntext.AppendLine("       SetMemory(0x6509B0, Add, 1),")
                                        returntext.AppendLine("       SetDeaths(CurrentPlayer , SetTo, " & ptBinary.ReadUInt32() & ", 0),")
                                        returntext.AppendLine("       SetMemory(0x6509B0, Add, 1),")


                                    Next
                                Next

                            Else
                                Dim tgroup As UInt16 = .TileSetNum \ 16
                                Dim tindex As Byte = .TileSetNum Mod 16
                                cv5mem.Position = &H14 + 52 * tgroup + 2 * tindex




                                vx4mem.Position = 32 * cv5binary.ReadUInt16()
                                For j = 0 To 15
                                    Dim tvalue As UInt16 = vx4binary.ReadUInt16()

                                    Dim flipflag As Boolean = tvalue And 1
                                    Dim vr4index As UInt16 = (tvalue And 65534) \ 2


                                    Dim ptr As Integer = 0
                                    If flipflag = True Then
                                        returntext.AppendLine("       SetMemory(0x6509B0, SetTo, vr4offset + " & (vr4index * 64) \ 4 & "),")
                                        For k = 0 To 7 'y
                                            Dim tvalue2 As UInt32 = 0
                                            For p = 0 To 3
                                                tvalue2 += .TileSetData(7 - p + k * 32 + 8 * (j Mod 4) + 256 * (j \ 4)) * 256 ^ p
                                            Next
                                            returntext.AppendLine("       SetDeaths(CurrentPlayer , SetTo, " & tvalue2 & ", 0),")
                                            returntext.AppendLine("       SetMemory(0x6509B0, Add, 1),")


                                            tvalue2 = 0
                                            For p = 4 To 7
                                                tvalue2 += .TileSetData(7 - p + k * 32 + 8 * (j Mod 4) + 256 * (j \ 4)) * 256 ^ (p - 4)

                                            Next
                                            returntext.AppendLine("       SetDeaths(CurrentPlayer , SetTo, " & tvalue2 & ", 0),")
                                            returntext.AppendLine("       SetMemory(0x6509B0, Add, 1),")

                                        Next
                                    Else

                                        returntext.AppendLine("       SetMemory(0x6509B0, SetTo, vr4offset + " & (vr4index * 64) \ 4 & "),")
                                        For k = 0 To 7 'y
                                            ptData.Position = k * 32 + 8 * (j Mod 4) + 256 * (j \ 4)

                                            returntext.AppendLine("       SetDeaths(CurrentPlayer , SetTo, " & ptBinary.ReadUInt32() & ", 0),")
                                            returntext.AppendLine("       SetMemory(0x6509B0, Add, 1),")
                                            returntext.AppendLine("       SetDeaths(CurrentPlayer , SetTo, " & ptBinary.ReadUInt32() & ", 0),")
                                            returntext.AppendLine("       SetMemory(0x6509B0, Add, 1),")


                                        Next
                                    End If
                                Next

                            End If



                            ptData.Close()
                            ptBinary.Close()
                        End With

                    Next
                ElseIf (CheckFileExist(ProjectTileSetFileName) = False) Then
                    Dim filestream As New FileStream(ProjectTileSetFileName, FileMode.Open)
                    Dim binaryReader As New BinaryReader(filestream)


                    filestream.Position = 18
                    Dim bmpfilesize As New Size
                    bmpfilesize.Width = binaryReader.ReadUInt32() \ 32 '가로
                    bmpfilesize.Height = binaryReader.ReadUInt32() \ 32 '세로

                    binaryReader.Close()
                    filestream.Close()


                    For TileSetNum = 0 To bmpfilesize.Width * bmpfilesize.Height - 1
                        Dim tgroup As UInt16 = TileSetNum \ 16
                        Dim tindex As Byte = TileSetNum Mod 16

                        Dim tval As UInt32

                        If TileSetNum Mod 2 = 0 Then
                            tval = TileSetNum
                        Else
                            tval += TileSetNum * 65536
                            returntext.AppendLine("       SetDeaths(cv5offset + " & ((&H14 + 52 * tgroup + 2 * tindex) \ 4) & ", SetTo, " & tval & ", 0),")
                        End If

                        For j = 0 To 15
                            Dim vr4index As UInt16 = TileSetNum * 16 + j

                            Dim tval2 As UInt32
                            If j Mod 2 = 0 Then
                                tval2 = (vr4index << 1)
                            Else
                                tval2 += (vr4index << 1) * 65536
                                returntext.AppendLine("       SetDeaths(vx4offset + " & ((32 * TileSetNum) \ 4) + j \ 2 & ", SetTo, " & tval2 & ", 0),")
                            End If

                            'returntext.AppendLine("       value = ")

                        Next
                    Next


                End If


                returntext.AppendLine("    ])")



                cv5binary.Close()
                cv5mem.Close()

                vx4binaryw.Close()
                vx4binary.Close()
                vx4mem.Close()
            End If

            '==================================TileSet===============================



            If ProjectSet.UsedSetting(ProjectSet.Settingtype.DatEdit) = True Or ProjectSet.UsedSetting(ProjectSet.Settingtype.FireGraft) = True Then
                returntext.AppendLine("    DoActions([")
            End If

            If ProjectSet.UsedSetting(ProjectSet.Settingtype.DatEdit) = True Then

                For k = 0 To DatEditDATA.Count - 1
                    For i = 0 To DatEditDATA(k).projectdata.Count - 1
                        For j = 0 To DatEditDATA(k).projectdata(i).Count - 1
                            If DatEditDATA(k).projectdata(i)(j) <> 0 Then ' 하나라도 0이 아니라면. 즉 하나라도 수정되어있다면.
                                Dim Offsetname As String = DatEditDATA(k).typeName & "_" & DatEditDATA(k).keyDic.Keys.ToList(i)
                                Dim typeName As String = DatEditDATA(k).keyDic.Keys.ToList(i)

                                Dim _size As Integer = DatEditDATA(k).keyINFO(i).realSize

                                Dim _value As Long = DatEditDATA(k).projectdata(i)(j)
                                Dim _oldvalue As Long = DatEditDATA(k).data(i)(j)

                                Dim _lastvalue As Long = 0

                                Dim _offsetNum As Long = Val("&H" & ReadOffset(Offsetname)) + _size * j



                                Dim _byte2 As Long = (j * _size) Mod 4
                                Dim _byte As Long = _offsetNum Mod 4
                                _offsetNum = _offsetNum - _byte

                                Dim _offset As String = Hex(_offsetNum)

                                Dim temptext As String = "       SetMemory(0x" & _offset & ", "

                                _lastvalue = _value * 256 ^ _byte
                                If _lastvalue > 0 Then
                                    temptext = temptext & "Add"
                                Else
                                    temptext = temptext & "Subtract"
                                    _lastvalue = _lastvalue * -1
                                End If
                                temptext = temptext & ", " & _lastvalue & "),"


                                returntext.AppendLine(temptext)
                            End If
                        Next
                    Next
                Next
            End If


            If ProjectSet.UsedSetting(ProjectSet.Settingtype.FireGraft) = True Then
                For i = 0 To ProjectUnitStatusFn1.Count - 1
                    Dim narr() As String = {"FG_Debug", "FG_Status", "FG_Display"}

                    For sname = 0 To 2
                        Dim checkvalue As Long
                        Dim _lastvalue As Long

                        Select Case sname
                            Case 0
                                checkvalue = ProjectDebugID(i)
                                _lastvalue = ProjectDebugID(i)
                            Case 1
                                checkvalue = ProjectUnitStatusFn1(i)
                                _lastvalue = CLng(statusFn1(ProjectUnitStatusFn1(i) + UnitStatusFn1(i))) - statusFn1(UnitStatusFn1(i))

                            Case 2
                                checkvalue = ProjectUnitStatusFn2(i)
                                _lastvalue = CLng(statusFn2(ProjectUnitStatusFn2(i) + UnitStatusFn2(i))) - statusFn2(UnitStatusFn2(i))
                        End Select



                        Dim _offsetNum As Long = Val("&H" & ReadOffset(narr(sname))) + 12 * i
                        Dim _offset As String = Hex(_offsetNum)


                        Dim _modifier As String


                        If _lastvalue > 0 Then
                            _modifier = "Add"
                        Else
                            _modifier = "Subtract"
                            _lastvalue = _lastvalue * -1
                        End If



                        If checkvalue <> 0 Then





                            returntext.AppendLine("       SetMemory(" & "0x" & _offset & ", " & _modifier & ", " & _lastvalue & "),")

                        End If
                    Next

                Next



                returntext.AppendLine("       SetMemory(0x" & ReadOffset("Vanilla") & ", SetTo, 0x" & ReadOffset("FG_ReqUnit") & "),")
            End If
            'returntext.AppendLine("       SetMemory(0x6647C0, Add, 255),")

            If ProjectSet.UsedSetting(ProjectSet.Settingtype.DatEdit) = True Or ProjectSet.UsedSetting(ProjectSet.Settingtype.FireGraft) = True Then

                returntext.AppendLine("    ])")
            End If

            returntext.AppendLine()






            If ProjectSet.UsedSetting(ProjectSet.Settingtype.FireGraft) = True Then
                '호환성버그고치기
                returntext.AppendLine("def beforeTriggerExec():")
                returntext.AppendLine(RepDataToTrigger())
                returntext.AppendLine()
            End If





            Return returntext.ToString
        End Function

        '.pos가 가장 작은 순서대로.

        Private Function GetTextValue(i As Integer) As String
            If ProjectBtnData(i).Count <> 0 Then
                Dim sort As New List(Of UInteger)
                Dim data As New List(Of UInteger)

                For j = 0 To ProjectBtnData(i).Count - 1
                    sort.Add(j)
                    data.Add(ProjectBtnData(i)(j).pos)
                Next
                With ProjectBtnData(i)
                    For j = 0 To .Count - 1 'pos를 기준으로 정렬.
                        'j번째 데이터를 선택.
                        '

                        For p = j + 1 To .Count - 1  'pos를 기준으로 정렬.
                            If data(j) > data(p) Then
                                Dim temp As UInteger = sort(p)
                                sort(p) = sort(j)
                                sort(j) = temp

                                temp = data(p)
                                data(p) = data(j)
                                data(j) = temp
                            End If
                        Next
                    Next
                End With





                Dim bytearr As String = ""
                For j = 0 To ProjectBtnData(i).Count - 1
                    With ProjectBtnData(i)(sort(j))
                        bytearr = bytearr & ValueTostring(.pos) & "," & ValueTostring(.icon) & "," _
                              & ValueTostring(.con) & "," & ValueTostring(.act) & "," _
                              & ValueTostring(.conval) & "," & ValueTostring(.actval) & "," _
                              & ValueTostring(.enaStr) & "," & ValueTostring(.disStr) & ","
                    End With
                Next
                Return Mid(bytearr, 1, bytearr.Length - 1)
            Else
                Return 0
            End If

        End Function
        Private Function ValueTostring(value As Object) As String
            Dim rstr As String = ""

            Dim memstr As New MemoryStream(4)
            Dim binwriter As New BinaryWriter(memstr)

            binwriter.Write(value)

            memstr.Position = 0
            Select Case value.GetType.ToString
                Case "System.UInt16"
                    rstr = rstr & memstr.ReadByte & ","
                    rstr = rstr & memstr.ReadByte
                Case "System.UInt32"
                    rstr = rstr & memstr.ReadByte & ","
                    rstr = rstr & memstr.ReadByte & ","
                    rstr = rstr & memstr.ReadByte & ","
                    rstr = rstr & memstr.ReadByte
            End Select



            binwriter.Close()
            memstr.Close()
            Return rstr
        End Function



        Sub DeleteFilesFromFolder(Folder As String)
            If Directory.Exists(Folder) Then
                For Each _file As String In Directory.GetFiles(Folder)
                    File.Delete(_file)
                Next
                For Each _folder As String In Directory.GetDirectories(Folder)

                    DeleteFilesFromFolder(_folder)
                Next
            End If
        End Sub

        'Somewhere you call


        Public Sub Toflie(Optional isedd As Boolean = False, Optional isotherWindows As Boolean = False)
            Dim basefolder As String = My.Application.Info.DirectoryPath & "\Data"
            If ProjectSet.filename.EndsWith(".e2p") Then
                '집 파일이면 
                basefolder = ProjectSet.filename.Replace("\" & GetSafeName(ProjectSet.filename), "")
            End If

            DeleteFilesFromFolder(basefolder & "\temp")

            Dim filestream As FileStream
            Dim streamwriter As StreamWriter

            Dim filename As String = basefolder & "\eudplibdata\EUDEditor.eds"


            filestream = New FileStream(filename, FileMode.Create)
            streamwriter = New StreamWriter(filestream) ', Encoding.GetEncoding("ks_c_5601-1987"))

            streamwriter.Write(Getedstext)
            streamwriter.Close()
            filestream.Close()

            'MsgBox("테러태스트")
            If isedd = True Then
                filename = basefolder & "\eudplibdata\EUDEditor.edd"
                filestream = New FileStream(filename, FileMode.Create)
                streamwriter = New StreamWriter(filestream, Encoding.GetEncoding("ks_c_5601-1987"))

                streamwriter.Write(Getedstext)
                streamwriter.Close()
                filestream.Close()
            End If

            Try

            Catch ex As Exception
                MsgBox("맵이 실행 중입니다!. 맵을 끄고 삽입하세요.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                streamwriter.Close()
                filestream.Close()
                Exit Sub
            End Try


            If ProjectSet.UsedSetting(ProjectSet.Settingtype.BtnSet) = True Then

                filename = basefolder & "\eudplibdata\BGMPlayer.eps"
                filestream = New FileStream(filename, FileMode.Create)
                streamwriter = New StreamWriter(filestream) ', Encoding.GetEncoding("ks_c_5601-1987"))

                streamwriter.Write(GetBGMPlyereps)

                streamwriter.Close()
                filestream.Close()
            End If


            filename = basefolder & "\eudplibdata\EUDEditor.py"
            filestream = New FileStream(filename, FileMode.Create)
            streamwriter = New StreamWriter(filestream) ', Encoding.GetEncoding("ks_c_5601-1987"))

            streamwriter.Write(GetPYtext)

            streamwriter.Close()
            filestream.Close()



            If ProjectSet.UsedSetting(ProjectSet.Settingtype.Struct) = True Then
                filename = basefolder & "\eudplibdata\tempcustomText.py"
                filestream = New FileStream(filename, FileMode.Create)
                streamwriter = New StreamWriter(filestream) ', Encoding.GetEncoding("ks_c_5601-1987"))

                streamwriter.Write(My.Resources.customText)

                streamwriter.Close()
                filestream.Close()


                filename = basefolder & "\eudplibdata\punitloop.py"
                filestream = New FileStream(filename, FileMode.Create)
                streamwriter = New StreamWriter(filestream) ', Encoding.GetEncoding("ks_c_5601-1987"))

                streamwriter.Write(My.Resources.punitloop)

                streamwriter.Close()
                filestream.Close()


                filename = basefolder & "\eudplibdata\TriggerEditor.eps"
                filestream = New FileStream(filename, FileMode.Create)
                streamwriter = New StreamWriter(filestream) ', Encoding.GetEncoding("ks_c_5601-1987"))

                streamwriter.Write(TriggerToEPS(True))

                streamwriter.Close()
                filestream.Close()

                If ProjectSet.SCDBUse = True Then
                    filename = basefolder & "\eudplibdata\SCDB.eps"
                    filestream = New FileStream(filename, FileMode.Create)
                    streamwriter = New StreamWriter(filestream) ', Encoding.GetEncoding("ks_c_5601-1987"))

                    streamwriter.Write(ToScdbeps)

                    streamwriter.Close()
                    filestream.Close()

                    If nqcuse = False Or ProjectSet.UsedSetting(ProjectSet.Settingtype.Plugin) = False Then
                        nqcuse = True
                        If ProjectSet.UsedSetting(ProjectSet.Settingtype.Plugin) = False Then
                            ProjectSet.UsedSetting(ProjectSet.Settingtype.Plugin) = True
                            Main.refreshSet()
                        End If

                        MsgBox(Lan.GetMsgText("MSQCOn"), MsgBoxStyle.Information, ProgramSet.ErrorFormMessage)
                    End If
                End If
            End If

            CreateDebugpy()

            Dim process As New Process
            Dim startInfo As New ProcessStartInfo


            If isedd = True Then
                filename = basefolder & "\eudplibdata\EUDEditor.edd"

                startInfo.FileName = ProgramSet.euddraftDirec
                startInfo.Arguments = """" & filename & """"

                'startInfo.RedirectStandardOutput = True
                'startInfo.RedirectStandardInput = True
                'startInfo.WindowStyle = ProcessWindowStyle.Hidden
                'startInfo.CreateNoWindow = True

                'startInfo.UseShellExecute = False



                process.StartInfo = startInfo


                ''process.
                Try
                    process.Start() ' 여기서 프로그램이 실행됩니다.
                Catch ex As System.ComponentModel.Win32Exception
                    MsgBox("euddraft실행 파일이 누락되었습니다.! 다시 설정해 주세요.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                    SettingForm.ShowDialog()
                    Exit Sub
                End Try

                My.Forms.Main.Visible = False
                process.WaitForExit()
                My.Forms.Main.Visible = True

                DeledtDebugpy()
            Else
                BulidForm.isotherWindows = isotherWindows

                BulidForm.Show()

                BulidForm.CompileStart(basefolder)
                'filename = My.Application.Info.DirectoryPath & "\Data\eudplibdata\EUDEditor.eds"

                'startInfo.FileName = ProgramSet.euddraftDirec
                'startInfo.Arguments = """" & filename & """"

                '==================================================
                'startInfo.RedirectStandardOutput = True
                'startInfo.RedirectStandardInput = True
                'startInfo.WindowStyle = ProcessWindowStyle.Hidden
                'startInfo.CreateNoWindow = True

                'startInfo.UseShellExecute = False
                '==================================================


                'process.StartInfo = startInfo


                ''process.
                'Try
                '    process.Start() ' 여기서 프로그램이 실행됩니다.
                'Catch ex As System.ComponentModel.Win32Exception
                '    MsgBox("euddraft실행 파일이 누락되었습니다.! 다시 설정해 주세요.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                '    SettingForm.ShowDialog()
                '    Exit Sub
                'End Try

                '==================================================
                'process.StandardInput.Write(vbCrLf)


                'Dim console As String = process.StandardOutput.ReadToEnd()
                'My.Computer.Clipboard.SetText(console)
                ''Threading.Thread.Sleep(1000)
                'process.WaitForExit() ' 프로세스가 종료될때까지 기다립니다.


                'Try
                '    Dim lasttext As String = console.Split(vbCrLf)(console.Split(vbCrLf).Length - 2).Trim
                '    If InStr(lasttext, "[Error]") <> 0 Then
                '        Dim rgx As Regex = New Regex("v([0-9.]+)", RegexOptions.IgnoreCase)

                '        Dim ttemp As String = Mid(console, InStr(console, "version"))
                '        Dim version As String = rgx.Match(console).Value

                '        MsgBox("euddraft적용에 실패했습니다. 자세한 건 다음을 참고하세요." & vbCrLf & "(다음 내용이 자동으로 클립보드에 복사됩니다.)" & vbCrLf & console, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

                '        My.Computer.Clipboard.SetText(console)
                '        'MsgBox(lasttext)


                '    Else
                '        Dim rgx As Regex = New Regex("v([0-9.]+)", RegexOptions.IgnoreCase)

                '        Dim ttemp As String = Mid(console, InStr(console, "version"))
                '        Dim version As String = rgx.Match(console).Value

                '        'euddraft v([0-9.]+) : Simple eudplib plugin system
                '        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                '        MsgBox("적용에 성공했습니다." & vbCrLf & lasttext, MsgBoxStyle.OkOnly, "euddraft " & version) 'Mid(ttemp, 1, InStr(ttemp, vbCrLf)))


                '    End If
                'Catch ex As Exception
                '    Dim rgx As Regex = New Regex("v([0-9.]+)", RegexOptions.IgnoreCase)

                '    'Dim ttemp As String = Mid(console, InStr(console, "version"))
                '    Dim version As String = 0 'rgx.Match(console).Value

                '    MsgBox("euddraft적용에 실패했습니다. 자세한 건 다음을 참고하세요." & vbCrLf & "(다음 내용이 자동으로 클립보드에 복사됩니다.)" & vbCrLf & console, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

                '    My.Computer.Clipboard.SetText(console)
                '    'MsgBox(lasttext)



                'End Try
                'process.Close()
                '==================================================


                'If isotherWindows = False Then
                '    My.Forms.Main.Visible = False
                'End If

                'process.WaitForExit()

                'If isotherWindows = False Then
                '    My.Forms.Main.Visible = True
                'End If








                'filename = My.Application.Info.DirectoryPath & "\Data\eudplibdata\EUDEditor.eds"

                'startInfo.FileName = ProgramSet.euddraftDirec
                'startInfo.Arguments = """" & filename & """"
                'startInfo.RedirectStandardOutput = True
                'startInfo.RedirectStandardInput = True
                'startInfo.WindowStyle = ProcessWindowStyle.Hidden
                'startInfo.CreateNoWindow = True

                'startInfo.UseShellExecute = False



                'process.StartInfo = startInfo


                ''process.
                'Try
                '    process.Start() ' 여기서 프로그램이 실행됩니다.
                'Catch ex As System.ComponentModel.Win32Exception
                '    MsgBox("euddraft실행 파일이 누락되었습니다.! 다시 설정해 주세요.", MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)
                '    SettingForm.ShowDialog()
                '    Exit Sub
                'End Try


                '' 실행된 서브프로세스가 종료되어야 메인프로그램을 쓰게 하고 싶다면 아래 내용을 진행합니다.


                'process.StandardInput.Write(vbCrLf)

                'Dim console As String = process.StandardOutput.ReadToEnd()
                ''My.Computer.Clipboard.SetText(console)
                '' process.WaitForExit() ' 프로세스가 종료될때까지 기다립니다.


                'Try


                '    Dim lasttext As String = console.Split(vbCrLf)(console.Split(vbCrLf).Length - 2).Trim
                '    If InStr(lasttext, "[Error]") <> 0 Then
                '        Dim rgx As Regex = New Regex("v([0-9.]+)", RegexOptions.IgnoreCase)

                '        Dim ttemp As String = Mid(console, InStr(console, "version"))
                '        Dim version As String = rgx.Match(console).Value

                '        MsgBox("euddraft적용에 실패했습니다. 자세한 건 다음을 참고하세요." & vbCrLf & "(다음 내용이 자동으로 클립보드에 복사됩니다.)" & vbCrLf & console, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

                '        My.Computer.Clipboard.SetText(console)
                '        'MsgBox(lasttext)


                '        Dim data As String
                '        Try
                '            With CreateObject("WinHttp.WinHttpRequest.5.1")
                '                .Open("GET", "http://blog.naver.com/PostView.nhn?blogId=sksljh2091&logNo=220883526276")
                '                .Send
                '                .WaitForResponse

                '                data = .ResponseText

                '                ' Dim asda As FileStream = New FileStream("C:\Users\skslj\Desktop\새 텍스트 문서.txt", FileMode.Create)
                '                'Dim strstrie As StreamWriter = New StreamWriter(asda)

                '                Dim ttempstr As String = Mid(data, InStr(data, "euddraftversion"))
                '                'Mid(data, InStr(data, "euddraftversion"))

                '                Dim currentversion As String = Mid(ttempstr, 16, InStr(ttempstr, "]") - 16)

                '                '[euddraftversionv0.6]
                '                '[euddraftadresshttp://cafe.naver.com/edac/50107]

                '                ttempstr = Mid(data, InStr(data, "euddraftaddress"))
                '                'Mid(data, InStr(data, "euddraftversion"))
                '                Dim currentaddress As String = Mid(ttempstr, 16, InStr(ttempstr, "]") - 16)

                '                If currentversion <> version Then
                '                    MsgBox("euddraft버전이 최신버전이 아니라서 발생한 오류일 수 있습니다." & vbCrLf & "euddraft최신버전 : " & currentversion, MsgBoxStyle.Critical, "euddraft " & version) 'Mid(ttemp, 1, InStr(ttemp, vbCrLf)))
                '                    'Process.Start(currentaddress)
                '                End If
                '            End With
                '        Catch ex As Exception

                '        End Try

                '    Else
                '        Dim rgx As Regex = New Regex("v([0-9.]+)", RegexOptions.IgnoreCase)

                '        Dim ttemp As String = Mid(console, InStr(console, "version"))
                '        Dim version As String = rgx.Match(console).Value

                '        'euddraft v([0-9.]+) : Simple eudplib plugin system
                '        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                '        MsgBox("적용에 성공했습니다." & vbCrLf & lasttext, MsgBoxStyle.OkOnly, "euddraft " & version) 'Mid(ttemp, 1, InStr(ttemp, vbCrLf)))


                '        Dim data As String
                '        Try
                '            With CreateObject("WinHttp.WinHttpRequest.5.1")
                '                .Open("GET", "http://blog.naver.com/PostView.nhn?blogId=sksljh2091&logNo=220883526276")
                '                .Send
                '                .WaitForResponse

                '                data = .ResponseText


                '                Dim ttempstr As String = Mid(data, InStr(data, "euddraftversion"))
                '                'Mid(data, InStr(data, "euddraftversion"))

                '                Dim currentversion As String = Mid(ttempstr, 16, InStr(ttempstr, "]") - 16)

                '                '[euddraftversionv0.6]
                '                '[euddraftadresshttp://cafe.naver.com/edac/50107]

                '                ttempstr = Mid(data, InStr(data, "euddraftaddress"))
                '                'Mid(data, InStr(data, "euddraftversion"))
                '                Dim currentaddress As String = Mid(ttempstr, 16, InStr(ttempstr, "]") - 16)

                '                If currentversion <> version Then
                '                    MsgBox("euddraft버전이 최신버전이 아닙니다!" & vbCrLf & "오류가 발생 할 수 있습니다." & vbCrLf & "euddraft최신버전 : " & currentversion, MsgBoxStyle.Critical, "euddraft " & version) 'Mid(ttemp, 1, InStr(ttemp, vbCrLf)))
                '                    'Process.Start(currentaddress)
                '                    'MsgBox(currentversion)
                '                    'MsgBox(currentaddress)
                '                End If
                '            End With
                '        Catch ex As Exception

                '        End Try

                '    End If
                'Catch ex As Exception
                '    Dim rgx As Regex = New Regex("v([0-9.]+)", RegexOptions.IgnoreCase)

                '    'Dim ttemp As String = Mid(console, InStr(console, "version"))
                '    Dim version As String = 0 'rgx.Match(console).Value

                '    MsgBox("euddraft적용에 실패했습니다. 자세한 건 다음을 참고하세요." & vbCrLf & "(다음 내용이 자동으로 클립보드에 복사됩니다.)" & vbCrLf & console, MsgBoxStyle.Critical, ProgramSet.ErrorFormMessage)

                '    My.Computer.Clipboard.SetText(console)
                '    'MsgBox(lasttext)



                'End Try


                'process.Close()
            End If
        End Sub
    End Module
End Namespace


